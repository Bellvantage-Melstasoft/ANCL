using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.IO;
using System.Drawing;
using System.Globalization;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class RecommendQuotations : System.Web.UI.Page
    {
        #region properties
       // static int PrId = 0;
      //  static int BidId = 0;
       // static int UserId = 0;
       // static string PrCode;
       // static string BidCode;
       // static string ReprintPrCode;
       // static string ReprintBidCode;
        //static int DesignationId = 0;
        //static int CompanyId = 0;
      // static int viewedQuotation = 0;
      //  static PrMasterV2 PrMaster;
       // static List<int> RecommendableTabulationIds;
       // static int Itemcount = 0;
       // static int reprintItemcount = 0;
       // static TabulationMaster tabulationMaster;
        #endregion

        #region controllers
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        QuotationRecommendationController quotationRecommendationController = ControllerFactory.CreateQuotationRecommendationController();
        TabulationRecommendationController tabulationRecommendationController = ControllerFactory.CreateTabulationRecommendationController();
        TabulationMasterController tabulationMasterController = ControllerFactory.CreateTabulationMasterController();
        TabulationDetailController tabulationDetailController = ControllerFactory.CreateTabulationDetailController();
        CommitteeController committeeController = ControllerFactory.CreateProcurementCommitteeController();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefApproval";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabApproval";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForQuotationRecommendation.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "quotationApprovalLink";


               // UserId = int.Parse(Session["UserId"].ToString());
              //  DesignationId = int.Parse(Session["DesignationId"].ToString());
              //  CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 13) && companyLogin.Usertype != "S")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack)
            {
                if (int.Parse(Session["UserId"].ToString()) != 0)
                {
                    try
                    {
                        var RecommendableTabulationIds = Session["RecommendableTabulationIds"] as List<int>;
                        ViewState["RecommendableTabulationIds"] = new JavaScriptSerializer().Serialize(RecommendableTabulationIds);

                      ViewState["PrId"] = int.Parse(Request.QueryString.Get("PrId"));
                       var PrMaster = pr_MasterController.GetPrForQuotationApproval(int.Parse(ViewState["PrId"].ToString()), int.Parse(Session["CompanyId"].ToString()), RecommendableTabulationIds, int.Parse(Session["UserId"].ToString()), int.Parse(Session["DesignationId"].ToString()));
                        ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);

                        ViewState["PrCode"] = "PR" + PrMaster.PrCode;

                        lblPRNo.Text = "PR" + PrMaster.PrCode;
                        lblCreatedOn.Text = PrMaster.CreatedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                        lblCreatedBy.Text = PrMaster.CreatedByName;
                        lblRequestBy.Text = PrMaster.CreatedByName;
                        lblRequestFor.Text = PrMaster.RequiredFor;
                        lblExpenseType.Text = (PrMaster.ExpenseType == 1) ? "Capital Expense" : "Operational Expense";
                        lblDepartment.Text = !String.IsNullOrEmpty(PrMaster.SubDepartmentName) ? PrMaster.SubDepartmentName : "Not Found";
                        lblWarehouse.Text = PrMaster.WarehouseName;
                        lblMrnId.Text = PrMaster.MrnId != 0 ? "MRN" + PrMaster.MrnCode : "Not From MRN";

                        LoadGV();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void LoadGV()
        {
            try
            {
                new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.ForEach(b => { b.NoOfQuotations = b.SupplierQuotations.Count; b.NoOfRejectedQuotations = b.SupplierQuotations.Count(sq => sq.IsSelected == 2); });
                gvBids.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids;
                gvBids.DataBind();

                PurchaseType.Value = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PurchaseType.ToString();

                gvRejectedTabulations.DataSource = tabulationMasterController.GetTabulationsRejectedTabulationsByPrId(int.Parse(ViewState["PrId"].ToString()));
                gvRejectedTabulations.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());

                gvBidItems.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();
            }
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            ViewState["BidId"] = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
            ViewState["BidCode"]  = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Where(x => x.BidId == int.Parse(ViewState["BidId"].ToString())).FirstOrDefault().BidCode.ToString();
            LoadQuotations();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotations').modal('show'); });   </script>", false);

        }

        private void LoadQuotations()
        {
            try
            {
                if (PurchaseType.Value == "1")
                {
                    gvQuotations.Visible = true;
                   var tabulationMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Where(q => (q.IsSelected == 1) && (q.IsCurrent == 1)).FirstOrDefault();
                    ViewState["tabulationMaster"] = new JavaScriptSerializer().Serialize(tabulationMaster);

                    hdnTabulationId.Value = tabulationMaster.TabulationId.ToString();
                    txtareaRemark.Text = tabulationMaster.SelectionRemarks;
                    List<TabulationDetail> tabulationDetails = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Find(q => q.TabulationId == tabulationMaster.TabulationId).TabulationDetails.Where(x => (x.TabulationId == tabulationMaster.TabulationId)).ToList();
                    //viewedQuotation = 1;
                    //List<SupplierQuotation> quotations;

                    //DataTable dt = new DataTable();

                    //dt.Columns.Add("QuotationId");
                    //dt.Columns.Add("BidId");
                    //dt.Columns.Add("SupplierId");
                    //dt.Columns.Add("SupplierName");
                    //dt.Columns.Add("SubTotal");
                    //dt.Columns.Add("NbtAmount");
                    //dt.Columns.Add("VatAmount");
                    //dt.Columns.Add("NetTotal");
                    //dt.Columns.Add("TermsAndCondition");
                    //dt.Columns.Add("IsSelected");
                    //dt.Columns.Add("SelectionRemarks");
                    //dt.Columns.Add("RequiredRecommendationCount");
                    //dt.Columns.Add("RecommendedCount");
                    //dt.Columns.Add("CanLoggedInUserOverrideRecommendation");

                    //quotations = PrMaster.Bids.Find(b => b.BidId == BidId).SupplierQuotations.OrderBy(q => q.NetTotal).ToList();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Refno");
                    dt.Columns.Add("TabulationId");
                    dt.Columns.Add("QuotationId");
                    dt.Columns.Add("BidId");
                    dt.Columns.Add("SupplierId");
                    dt.Columns.Add("SupplierName");
                    var biddingItems = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).BiddingItems;
                    var Itemcount = 0;
                    foreach (var item in biddingItems)
                    {
                        dt.Columns.Add("Item Description-" + item.ItemName);
                        dt.Columns.Add("Item Code-" + item.ItemName);
                        dt.Columns.Add("Item Price -" + item.ItemName + "-QTY-" + item.Qty.ToString());
                        Itemcount++;
                        ViewState["Itemcount"] = Itemcount;
                    }
                    //dt.Columns.Add("Total Without Tax");
                    //dt.Columns.Add("Nbt Amount");
                    //dt.Columns.Add("Vat Amount");
                    //dt.Columns.Add("Total With Tax");

                    int qutaitoid = 0;

                    for (int i = 0; i < tabulationDetails.Count; i++)
                    {

                        if (qutaitoid != tabulationDetails[i].QuotationId)
                        {
                            DataRow newRow = dt.NewRow();
                            newRow["Refno"] = tabulationDetails[i].Refno;
                            newRow["TabulationId"] = tabulationDetails[i].TabulationId.ToString();
                            newRow["QuotationId"] = tabulationDetails[i].QuotationId.ToString();
                            newRow["BidId"] = tabulationMaster.BidId.ToString();
                            newRow["SupplierId"] = tabulationDetails[i].SupplierId.ToString();
                            newRow["SupplierName"] = tabulationDetails[i].SupplierName;
                            foreach (var item in biddingItems)
                            {
                                foreach (var item2 in tabulationDetails)
                                {
                                    if (item.ItemId == item2.ItemId && tabulationDetails[i].QuotationId == item2.QuotationId)
                                    {
                                        newRow["Item Description-" + item.ItemName] = item2.Description == null || item2.Description == "" ? "No Description" + " : " : item2.Description;
                                        newRow["Item Code-" + item.ItemName] = item.ItemId.ToString();
                                        if (item2.IsSelected == 1)
                                        {

                                            newRow["Item Price -" + item.ItemName + "-QTY-" + item.Qty.ToString()] = item2.UnitPrice.ToString("#,##0.00") + "*" + item2.Qty.ToString();
                                        }
                                        else
                                        {
                                            newRow["Item Price -" + item.ItemName + "-QTY-" + item.Qty.ToString()] = item2.UnitPrice.ToString("#,##0.00");
                                        }
                                    }
                                }



                            }
                            //newRow["SubTotal"] = tabulationDetails[i].SubTotal.ToString();
                            //newRow["NbtAmount"] = tabulationDetails[i].NbtAmount.ToString();
                            //newRow["VatAmount"] = tabulationDetails[i].VAtAmount.ToString();
                            //newRow["Total With Tax"] = tabulationDetails[i].NetTotal.ToString();
                            qutaitoid = tabulationDetails[i].QuotationId;
                            dt.Rows.Add(newRow);

                        }

                    }





                    gvQuotations.DataSource = dt;
                    gvQuotations.DataBind();

                    hdnIsrejected.Value = "";

                    for (int i = gvQuotations.Rows.Count - 1; i > 0; i--)
                    {
                        GridViewRow row = gvQuotations.Rows[i];
                        GridViewRow previousRow = gvQuotations.Rows[i - 1];
                        for (int j = 6; j < 7; j++)
                        {
                            if (row.Cells[j].Text == previousRow.Cells[j].Text || row.Cells[j].Text == "")
                            {
                                if (previousRow.Cells[j].RowSpan == 0)
                                {
                                    if (row.Cells[j].RowSpan == 0)
                                    {
                                        previousRow.Cells[j].RowSpan += 2;
                                    }
                                    else
                                    {
                                        previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                                    }
                                    row.Cells[j].CssClass = "hidden";
                                }
                            }


                        }
                    }
                }
                else
                {
                    var tabulationMaster = new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString());
                    gvImpotsQuotations.Visible = true;
                    tabulationMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Where(q => (q.IsSelected == 1) && (q.IsCurrent == 1)).FirstOrDefault();
                    hdnTabulationId.Value = tabulationMaster.TabulationId.ToString();
                    txtareaRemark.Text = tabulationMaster.SelectionRemarks;
                    List<TabulationDetail> tabulationDetails = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Find(q => q.TabulationId == tabulationMaster.TabulationId).TabulationDetails.Where(x => (x.TabulationId == tabulationMaster.TabulationId)).OrderBy(x=>x.UnitPrice).ToList();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Refno");
                    dt.Columns.Add("TabulationId");
                    dt.Columns.Add("QuotationId");
                    dt.Columns.Add("BidId");
                    dt.Columns.Add("SupplierId");
                    dt.Columns.Add("SupplierName");
                    dt.Columns.Add("Agent");
                    dt.Columns.Add("Currency");
                    dt.Columns.Add("Country");
                    dt.Columns.Add("Brand");
                    dt.Columns.Add("Term");
                    var biddingItems = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).BiddingItems;
                    var Itemcount = 0;
                    foreach (var item in biddingItems)
                    {
                        dt.Columns.Add("Item Description");
                        dt.Columns.Add("Item Code");
                        dt.Columns.Add("Item Price");
                        dt.Columns.Add("Item Price (LKR) -QTY-" + item.Qty.ToString());
                        Itemcount++;
                        ViewState["Itemcount"] = Itemcount;
                    }

                    dt.Columns.Add("CIF");
                    dt.Columns.Add("CIF LKR");
                    dt.Columns.Add("Duty & PAL LKR");
                    dt.Columns.Add("Est Clearing /Mt LKR");
                    dt.Columns.Add("Other Cost LKR");
                    dt.Columns.Add("Import History");
                    dt.Columns.Add("Validity");
                    dt.Columns.Add("EST. Delivery");
                    dt.Columns.Add("Remarks");

                    int qutaitoid = 0;
                    var itemname = string.Empty;
                    for (int i = 0; i < tabulationDetails.Count; i++)
                    {
                       
                        if (qutaitoid != tabulationDetails[i].QuotationId)
                        {
                            DataRow newRow = dt.NewRow();
                            newRow["Refno"] = tabulationDetails[i].Refno;
                            newRow["TabulationId"] = tabulationDetails[i].TabulationId.ToString();
                            newRow["QuotationId"] = tabulationDetails[i].QuotationId.ToString();
                            newRow["BidId"] = tabulationMaster.BidId.ToString();
                            newRow["SupplierId"] = tabulationDetails[i].SupplierId.ToString();
                            newRow["SupplierName"] = tabulationDetails[i].SupplierName;
                            newRow["Agent"] = tabulationDetails[i].AgentName;
                            newRow["Currency"] = tabulationDetails[i].CurrencyName;
                            newRow["Country"] = tabulationDetails[i].CountryName;
                            newRow["Brand"] = tabulationDetails[i].Brand;
                            foreach (var item in biddingItems)
                            {
                                foreach (var item2 in tabulationDetails)
                                {
                                    if (item.ItemId == item2.ItemId && tabulationDetails[i].QuotationId == item2.QuotationId)
                                    {

                                        newRow["Item Description"] = item2.Description == null || item2.Description == "" ? "No Description" + " : " : item2.Description;
                                        newRow["Item Code"] = item.ItemId.ToString();
                                        newRow["Item Price"] = (item2.UnitPrice / item2.ExchangeRate).ToString("#,##0.00");
                                        if (item2.IsSelected == 1)
                                        {
                                            newRow["Item Price (LKR) -QTY-" + item.Qty.ToString()] = item2.UnitPrice.ToString("#,##0.00") + "*" + item2.Qty.ToString();
                                        }
                                        else
                                        {
                                            newRow["Item Price (LKR) -QTY-" + item.Qty.ToString()] = item2.UnitPrice.ToString("#,##0.00");
                                        }

                                            itemname = item.ItemName;


                                    }
                                }
                             
                            }

                            newRow["Term"] = tabulationDetails[i].Terms;
                            newRow["CIF"] = tabulationDetails[i].CIF.ToString("#,##0.00");
                            newRow["CIF LKR"] = (tabulationDetails[i].CIF * tabulationDetails[i].ExchangeRate).ToString("#,##0.00");
                            newRow["Duty & PAL LKR"] = (tabulationDetails[i].Dutypal).ToString("#,##0.00");
                            newRow["Est Clearing /Mt LKR"] = (tabulationDetails[i].Clearingcost).ToString("#,##0.00");
                            newRow["Other Cost LKR"] = tabulationDetails[i].Other.ToString("#,##0.00");
                            newRow["Import History"] = tabulationDetails[i].History;
                            newRow["Validity"] = tabulationDetails[i].Validity.ToString("yyyy-MMM-dd");
                            newRow["EST. Delivery"] = tabulationDetails[i].Estdelivery;
                            newRow["Remarks"] = tabulationDetails[i].Remark;
                            qutaitoid = tabulationDetails[i].QuotationId;
                            dt.Rows.Add(newRow);

                        }

                    }
                    hdnApprovalRemarks.Value = "";
                    hdnRejectRemarks.Value = "";


                    gvImpotsQuotations.DataSource = dt;
                    gvImpotsQuotations.DataBind();

                    gvImpotsQuotations.Caption = itemname;

                    for (int i = gvImpotsQuotations.Rows.Count - 1; i > 0; i--)
                    {
                        GridViewRow row = gvImpotsQuotations.Rows[i];
                        GridViewRow previousRow = gvImpotsQuotations.Rows[i - 1];
                        for (int j = 6; j < 7; j++)
                        {
                            if (row.Cells[j].Text == previousRow.Cells[j].Text || row.Cells[j].Text == "")
                            {
                                if (previousRow.Cells[j].RowSpan == 0)
                                {
                                    if (row.Cells[j].RowSpan == 0)
                                    {
                                        previousRow.Cells[j].RowSpan += 2;
                                    }
                                    else
                                    {
                                        previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                                    }
                                    row.Cells[j].CssClass = "hidden";
                                }
                            }


                        }
                    }

                    //var isUploaded = committeeController.IsDocsUploaded(BidId, tabulationMaster.TabulationId,"T");
                    //if (isUploaded>0)
                    //{
                    //    btnuplodDock.Visible = false;
                    //}
                    //else
                    //{
                    //    btnuplodDock.Visible = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvQuotations_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                for (int i = 14; i < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); i += 3)
                {
                    if (e.Row.Cells[i + 3].Text == "&nbsp;")
                    {
                        e.Row.Cells[i + 3].CssClass = "";
                    }
                    else if (e.Row.Cells[i + 3].Text.Split('*').Length > 1)
                    {
                        e.Row.Cells[i + 3].CssClass = "CellClick greenBg";
                    }
                    else
                    {
                        e.Row.Cells[i + 3].CssClass = "CellClick";
                    }

                }

                for (int i = 1; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Font.Bold = true;
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                var tabulationMaster = new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString());
                e.Row.Cells[0].Text = "Total Without Tax: <br/> Nbt Amount: <br/> Vat Amount: <br/> Total With Tax: ";
                e.Row.Cells[6].Text = tabulationMaster.SubTotal.ToString("#,##0.00") + "<br/>" + tabulationMaster.NbtAmount.ToString("#,##0.00") + "<br/>" + tabulationMaster.VatAmount.ToString("#,##0.00") + "<br/>" + tabulationMaster.NetTotal.ToString("#,##0.00");
              
                e.Row.CssClass = "footer-font";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            }
            for (int i = 14; i < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); i += 3)
            {
               
                    e.Row.Cells[i + 2].CssClass = "hidden";
                
            }
        }
        protected void gvImpotsQuotations_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[20].Visible = false;
            e.Row.Cells[21].Visible = false;
            e.Row.Cells[22].Visible = false;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                for (int i = 23; i < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); i += 4)
                {
                    if (e.Row.Cells[i + 4].Text == "&nbsp;")
                    {
                        e.Row.Cells[i + 4].CssClass = "";
                    }
                    else if (e.Row.Cells[i + 4].Text.Split('*').Length > 1)
                    {
                        e.Row.Cells[i + 4].CssClass = "CellClick greenBg alignright";
                    }
                    else
                    {
                        e.Row.Cells[i + 4].CssClass = "CellClick alignright";
                    }

                }

                for (int i = 1; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Font.Bold = true;
                }
            }
           


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                for (int i = 24 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); i < e.Row.Cells.Count - 2; i++)
                {


                    e.Row.Cells[i].CssClass = "alignright";

                }

                for (int i = 23; i < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); i += 4)
                {
                    e.Row.Cells[i + 3].CssClass = "alignright";
                }

            }

            for (int i = 23; i < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); i += 4)
            {
                e.Row.Cells[i + 1].CssClass = "hidden";
                e.Row.Cells[i + 2].CssClass = "hidden";


            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                var tabulationMaster = new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString());
                e.Row.Cells[0].Text = "Total Without Tax: <br/> Nbt Amount: <br/> Vat Amount: <br/> Total With Tax: ";
                e.Row.Cells[6].Text = tabulationMaster.SubTotal.ToString("#,##0.00") + "<br/>" + tabulationMaster.NbtAmount.ToString("#,##0.00") + "<br/>" + tabulationMaster.VatAmount.ToString("#,##0.00") + "<br/>" + tabulationMaster.NetTotal.ToString("#,##0.00");

                e.Row.CssClass = "footer-font";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            }

        }
        //protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        GridView gvSpecs = e.Row.FindControl("gvSpecs") as GridView;
        //        GridView gvQuotationItems = (gvSpecs.NamingContainer as GridViewRow).NamingContainer as GridView;

        //        int quotationId = int.Parse(gvQuotations.DataKeys[((e.Row.NamingContainer as GridView).NamingContainer as GridViewRow).RowIndex].Value.ToString());
        //        int bidId = int.Parse(((e.Row.NamingContainer as GridView).NamingContainer as GridViewRow).Cells[2].Text);
        //        int quotationItemId = int.Parse(gvQuotationItems.DataKeys[e.Row.RowIndex].Value.ToString());

        //        DataTable dt = new DataTable();

        //        dt.Columns.Add("Material");
        //        dt.Columns.Add("Description");
        //        dt.Columns.Add("Comply");

        //        List<SupplierBOM> boms = PrMaster.Bids.Find(b => b.BidId == bidId).SupplierQuotations.Find(sq => sq.QuotationId == quotationId).QuotationItems.Find(qi => qi.QuotationItemId == quotationItemId).SupplierBOMs;

        //        for (int i = 0; i < boms.Count; i++)
        //        {
        //            DataRow newRow = dt.NewRow();

        //            newRow["Material"] = boms[i].Material;
        //            newRow["Description"] = boms[i].Description;
        //            newRow["Comply"] = boms[i].Comply.ToString();

        //            dt.Rows.Add(newRow);
        //        }

        //        gvSpecs.DataSource = dt;
        //        gvSpecs.DataBind();
        //    }
        //}

        [WebMethod]
        public static List<SupplierQuotation> GetQuotations(string BidId)
        {
            return new JavaScriptSerializer().Deserialize<PrMasterV2>(HttpContext.Current.Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(BidId)).SupplierQuotations;
        }

        protected void btnViewAttachments_Click(object sender, EventArgs e)
        {
            var x = hdnQuotationId.Value;
            gvDocs.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).SupplierQuotations.Find(q => q.QuotationId == int.Parse(hdnQuotationId.Value)).UploadedFiles;
            gvDocs.DataBind();

            gvImages.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).SupplierQuotations.Find(q => q.QuotationId == int.Parse(hdnQuotationId.Value)).QuotationImages;
            gvImages.DataBind();
            txtTermsAndConditions.Text = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).SupplierQuotations.Find(q => q.QuotationId == int.Parse(hdnQuotationId.Value)).TermsAndCondition;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlAttachments').modal('show') });   </script>", false);
        }



        protected void btnOverrideReject_Click(object sender, EventArgs e)
        {
            TabulationMaster tabulation = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Find(q => q.TabulationId ==int.Parse(hdnTabulationId.Value));


            int result = tabulationMasterController.OverrideRecommendation(int.Parse(hdnRecommendationId.Value), int.Parse(hdnTabulationId.Value), int.Parse(Session["UserId"].ToString()), int.Parse(Session["DesignationId"].ToString()), hdnRejectRemarks.Value,2, new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCategoryId, tabulation.NetTotal);

            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForQuotationRecommendation.aspx' });; });   </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on approving quotation'}); });   </script>", false);
            }
            
        }

        protected void btnOverrideApprove_Click(object sender, EventArgs e)
        {
            TabulationMaster tabulation = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Find(q => q.TabulationId == int.Parse(hdnTabulationId.Value));


            int result = tabulationMasterController.OverrideRecommendation(int.Parse(hdnRecommendationId.Value), int.Parse(hdnTabulationId.Value), int.Parse(Session["UserId"].ToString()), int.Parse(Session["DesignationId"].ToString()), hdnApprovalRemarks.Value, 1, new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCategoryId, tabulation.NetTotal);

            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForQuotationRecommendation.aspx' });; });   </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on approving quotation'}); });   </script>", false);
            }
        }

        protected void btnViewRecommendations_Click(object sender, EventArgs e)
        {

            int TabulationId = int.Parse(hdnTabulationId.Value);
           var isUploaded=committeeController.IsDocsUploaded(int.Parse(ViewState["BidId"].ToString()), TabulationId,"T");

            if (isUploaded>0)
            {
                gvRecommenations.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Find(t => t.TabulationId == TabulationId).TabulationRecommendations;
                gvRecommenations.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlRecommendations').modal('show'); });   </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();swal({ type: 'error',title: 'ERROR',text:'Please Upload Document First!', showConfirmButton: true,timer: 4000}).then((result) => { $('#mdlQuotations').modal('show') }); });   </script>", false);
            }
           
        }


        protected void btnReject_Click(object sender, EventArgs e)
        {

            int result = tabulationRecommendationController.RejectAtRecommendation(int.Parse(hdnTabulationId.Value), int.Parse(Session["UserId"].ToString()), int.Parse(Session["DesignationId"].ToString()), hdnRejectRemarks.Value);

            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForQuotationRecommendation.aspx' });; });   </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on approving quotation'}); });   </script>", false);
            }

        }


        protected void btnApprove_Click(object sender, EventArgs e)
        {
            TabulationMaster tabulation = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Find(q => q.TabulationId == int.Parse(hdnTabulationId.Value));

            int PurchaseType = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PurchaseType;

            int result = tabulationRecommendationController.RecommendTabulation(new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCategoryId,int.Parse(hdnTabulationId.Value), tabulation.NetTotal, int.Parse(Session["UserId"].ToString()), int.Parse(Session["DesignationId"].ToString()), hdnApprovalRemarks.Value, PurchaseType);

            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForQuotationRecommendation.aspx' });; });   </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on approving quotation'}); });   </script>", false);
            }

        }

        protected void btnOverrideRecommendationApprove_Click(object sender, EventArgs e)
        {
            TabulationMaster tabulation = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Find(q => q.TabulationId == int.Parse(hdnTabulationId.Value));


            int result = tabulationRecommendationController.OverrideAndRecommend(int.Parse(hdnRecommendationId.Value), new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCategoryId, int.Parse(hdnTabulationId.Value), tabulation.NetTotal, int.Parse(Session["UserId"].ToString()), int.Parse(Session["DesignationId"].ToString()), hdnApprovalRemarks.Value,1);

            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForQuotationRecommendation.aspx' });; });   </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on approving quotation'}); });   </script>", false);
            }
        }

        protected void btnOverrideRecommendationReject_Click(object sender, EventArgs e)
        {
            TabulationMaster tabulation = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Find(q => q.TabulationId == int.Parse(hdnTabulationId.Value));


            int result = tabulationRecommendationController.OverrideAndRecommend(int.Parse(hdnRecommendationId.Value), new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCategoryId, int.Parse(hdnTabulationId.Value), tabulation.NetTotal, int.Parse(Session["UserId"].ToString()), int.Parse(Session["DesignationId"].ToString()), hdnRejectRemarks.Value, 2);

            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForQuotationRecommendation.aspx' });; });   </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on approving quotation'}); });   </script>", false);
            }

        }
        protected void gvQuotations_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 14; i < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); i += 3)
                {

                    e.Row.Cells[i + 3].CssClass = "CellClick";


                }

            }
           
        }
        protected void gvImpotsQuotations_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 23; i < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); i += 4)
                {

                    e.Row.Cells[i + 4].CssClass = "CellClick";


                }

            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {

            if (PurchaseType.Value == "1")
            {
                printExcel(gvQuotations, "Tabulation Sheet", "Tabulation Sheet " + LocalTime.Now.ToString("yyyy/MMM/dd"));
            }
            else
            {
                printExcel(gvImpotsQuotations, "Tabulation Sheet", "Tabulation Sheet " + LocalTime.Now.ToString("yyyy/MMM/dd"));
            }
        }

        protected void btnsupplerview_Click(object sender, EventArgs e)
        {
            var SupplierId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[5].Text);
            Response.Redirect("CompanyUpdatingAndRatingSupplier.aspx?ID=" + SupplierId);
        }

        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            //required to avoid the run time error 'Grid View' must be placed inside a form tag with runat=server."  
        }

        protected void printExcel(GridView gridview, string filename, string header)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = filename + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                string headerTable = @"<Table><tr><td  colspan='3'><center><h4>" + header + " For PR Code: " + ViewState["PrCode"] + " Bid No:B" + ViewState["BidCode"] + " </h4></center> </td></tr></Table> <hr>";
                Response.Write(headerTable);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                gridview.GridLines = GridLines.Both;
                gridview.HeaderRow.Cells[2].Visible = false;
                gridview.HeaderRow.Cells[3].Visible = false;
                gridview.HeaderRow.Cells[4].Visible = false;
                gridview.HeaderRow.Cells[5].Visible = false;
               

                gridview.FooterRow.Cells[2].Visible = false;
                gridview.FooterRow.Cells[3].Visible = false;
                gridview.FooterRow.Cells[4].Visible = false;
                gridview.FooterRow.Cells[5].Visible = false;
              
                for (int i = 0; i < gridview.Rows.Count; i++)
                {
                    gridview.Rows[i].Cells[6].RowSpan=0;
                }


                if (PurchaseType.Value == "1")
                {
                    for (int i = 0; i < gridview.Rows.Count; i++)
                    {
                        gridview.Rows[i].Cells[2].Visible = false;
                        gridview.Rows[i].Cells[3].Visible = false;
                        gridview.Rows[i].Cells[4].Visible = false;
                        gridview.Rows[i].Cells[5].Visible = false;

                        gridview.Rows[i].Cells[7].Visible = false;
                        gridview.Rows[i].Cells[8].Visible = false;

                        gridview.HeaderRow.Cells[7].Visible = false;
                        gridview.HeaderRow.Cells[8].Visible = false;

                        gridview.FooterRow.Cells[7].Visible = false;
                        gridview.FooterRow.Cells[8].Visible = false;

                        gridview.Rows[i].Cells[i].Font.Bold = true;

                        if (hdnSlectedQutations.Value != "")
                        {
                            string[] SelectedArray = hdnSlectedQutations.Value.ToString().Split(',');
                            for (int k = 0; k < SelectedArray.Length; k += 6)
                            {
                                for (int j = 14; j < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); j += 3)
                                {
                                    if (gridview.Rows[i].Cells[3].Text == SelectedArray[k + 1] && gridview.Rows[i].Cells[j + 1].Text == SelectedArray[k + 4])
                                    {

                                        gridview.Rows[i].Cells[j + 3].BackColor = Color.Green;
                                        var txt = gridview.Rows[i].Cells[j + 3].Text;
                                        if (txt.Split('*').Length > 1)
                                        {
                                            gridview.Rows[i].Cells[j + 3].Text = txt.Split('*')[0] + "- QTY(" + SelectedArray[k + 5] + ")";
                                        }
                                        else
                                        {
                                            gridview.Rows[i].Cells[j + 3].Text = txt + "- QTY(" + SelectedArray[k + 5] + ")";
                                        }

                                    }
                                    gridview.Rows[i].Cells[j + 3].HorizontalAlign = HorizontalAlign.Right;
                                }




                            }


                        }
                        if (hdnSelectedChanged.Value != "")
                        {
                            string[] SelectedArray = hdnSelectedChanged.Value.ToString().Split(',');
                            for (int k = 0; k < SelectedArray.Length; k += 6)
                            {
                                for (int j = 14; j < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); j += 3)
                                {
                                    if (gridview.Rows[i].Cells[3].Text == SelectedArray[k + 1] && gridview.Rows[i].Cells[j + 2].Text == SelectedArray[k + 4])
                                    {

                                        gridview.Rows[i].Cells[j + 3].BackColor = Color.Green;
                                        var txt = gridview.Rows[i].Cells[j + 3].Text;
                                        gridview.Rows[i].Cells[j + 3].Text = txt.Split('*')[0] + "- QTY(" + SelectedArray[k + 5] + ")";
                                    }

                                    gridview.Rows[i].Cells[j + 3].HorizontalAlign = HorizontalAlign.Right;
                                }




                            }
                        }
                        if (hdnrejected.Value != "")
                        {
                            string[] SelectedArray = hdnrejected.Value.ToString().Split(',');
                            for (int k = 0; k < SelectedArray.Length; k += 6)
                            {
                                for (int j = 14; j < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); j += 3)
                                {
                                    if (gridview.Rows[i].Cells[3].Text == SelectedArray[k + 1] && gridview.Rows[i].Cells[j + 2].Text == SelectedArray[k + 4])
                                    {

                                        gridview.Rows[i].Cells[j + 3].BackColor = Color.Transparent;
                                        var txt = gridview.Rows[i].Cells[j + 3].Text;
                                        gridview.Rows[i].Cells[j + 3].Text = txt.Split('*')[0];
                                    }

                                    gridview.Rows[i].Cells[j + 3].HorizontalAlign = HorizontalAlign.Right;
                                }




                            }
                        }



                        for (int j = 14; j < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); j += 3)
                        {
                            if (gridview.Rows[i].Cells[j + 3].Text.Split('*').Length > 1)
                            {
                                gridview.Rows[i].Cells[j + 3].BackColor = Color.Green;
                                var txt = gridview.Rows[i].Cells[j + 3].Text;
                                gridview.Rows[i].Cells[j + 3].Text = txt.Split('*')[0] + "- QTY(" + txt.Split('*')[1] + ")";

                            }
                            gridview.Rows[i].Cells[j + 3].HorizontalAlign = HorizontalAlign.Right;
                        }


                        for (int k = 14; k < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); k += 3)
                        {

                            gridview.Rows[i].Cells[k + 2].Visible = false;
                        }

                    }

                    for (int k = 14; k < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); k += 3)
                    {


                        gridview.HeaderRow.Cells[k + 2].Visible = false;


                    }

                    for (int k = 14; k < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); k += 3)
                    {


                        gridview.FooterRow.Cells[k + 2].Visible = false;



                    }

                    for (int i = gridview.Rows.Count - 1; i > 0; i--)
                    {
                        GridViewRow row = gridview.Rows[i];
                        GridViewRow previousRow = gridview.Rows[i - 1];
                        for (int j = 6; j < 7; j++)
                        {
                            if (row.Cells[j].Text == previousRow.Cells[j].Text || row.Cells[j].Text == "")
                            {
                                if (previousRow.Cells[j].RowSpan == 0)
                                {
                                    if (row.Cells[j].RowSpan == 0)
                                    {
                                        previousRow.Cells[j].RowSpan += 2;
                                    }
                                    else
                                    {
                                        previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                                    }
                                    row.Cells[j].Visible = false;
                                }
                            }


                        }


                    }
                }
                else
                {
                    gridview.HeaderRow.Cells[11].Visible = false;
                    gridview.HeaderRow.Cells[12].Visible = false;

                    gridview.FooterRow.Cells[7].Visible = false;
                    gridview.FooterRow.Cells[8].Visible = false;

                    for (int i = 0; i < gridview.Rows.Count; i++)
                    {
                        gridview.Rows[i].Cells[2].Visible = false;
                        gridview.Rows[i].Cells[3].Visible = false;
                        gridview.Rows[i].Cells[4].Visible = false;
                        gridview.Rows[i].Cells[5].Visible = false;

                        gridview.Rows[i].Cells[11].Visible = false;
                        gridview.Rows[i].Cells[12].Visible = false;

                        gridview.Rows[i].Cells[i].Font.Bold = true;



                        if (hdnSlectedQutations.Value != "")
                        {
                            string[] SelectedArray = hdnSlectedQutations.Value.ToString().Split(',');
                            for (int k = 0; k < SelectedArray.Length; k += 6)
                            {
                                for (int j = 23; j < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); j += 4)
                                {
                                    if (gridview.Rows[i].Cells[3].Text == SelectedArray[k + 1] && gridview.Rows[i].Cells[j + 1].Text == SelectedArray[k + 4])
                                    {

                                        gridview.Rows[i].Cells[j + 4].BackColor = Color.Green;
                                        var txt = gridview.Rows[i].Cells[j + 4].Text;
                                        if (txt.Split('*').Length > 1)
                                        {
                                            gridview.Rows[i].Cells[j + 4].Text = txt.Split('*')[0] + "- QTY(" + SelectedArray[k + 5] + ")";
                                        }
                                        else
                                        {
                                            gridview.Rows[i].Cells[j + 4].Text = txt + "- QTY(" + SelectedArray[k + 5] + ")";
                                        }

                                    }
                                    gridview.Rows[i].Cells[j + 4].HorizontalAlign = HorizontalAlign.Right;
                                }




                            }


                        }
                        if (hdnSelectedChanged.Value != "")
                        {
                            string[] SelectedArray = hdnSelectedChanged.Value.ToString().Split(',');
                            for (int k = 0; k < SelectedArray.Length; k += 6)
                            {
                                for (int j = 23; j < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); j += 4)
                                {
                                    if (gridview.Rows[i].Cells[3].Text == SelectedArray[k + 1] && gridview.Rows[i].Cells[j + 2].Text == SelectedArray[k + 4])
                                    {

                                        gridview.Rows[i].Cells[j + 4].BackColor = Color.Green;
                                        var txt = gridview.Rows[i].Cells[j + 4].Text;
                                        gridview.Rows[i].Cells[j + 4].Text = txt.Split('*')[0] + "- QTY(" + SelectedArray[k + 5] + ")";
                                    }

                                    gridview.Rows[i].Cells[j + 4].HorizontalAlign = HorizontalAlign.Right;
                                }




                            }
                        }
                        if (hdnrejected.Value != "")
                        {
                            string[] SelectedArray = hdnrejected.Value.ToString().Split(',');
                            for (int k = 0; k < SelectedArray.Length; k += 6)
                            {
                                for (int j = 23; j < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); j += 4)
                                {
                                    if (gridview.Rows[i].Cells[3].Text == SelectedArray[k + 1] && gridview.Rows[i].Cells[j + 2].Text == SelectedArray[k + 4])
                                    {

                                        gridview.Rows[i].Cells[j + 4].BackColor = Color.Transparent;
                                        var txt = gridview.Rows[i].Cells[j + 4].Text;
                                        gridview.Rows[i].Cells[j + 4].Text = txt.Split('*')[0];
                                    }

                                    gridview.Rows[i].Cells[j + 3].HorizontalAlign = HorizontalAlign.Right;
                                }




                            }
                        }



                        for (int j = 23; j < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); j += 4)
                        {
                            if (gridview.Rows[i].Cells[j + 4].Text.Split('*').Length > 1)
                            {
                                gridview.Rows[i].Cells[j + 4].BackColor = Color.Green;
                                var txt = gridview.Rows[i].Cells[j + 4].Text;
                                gridview.Rows[i].Cells[j + 4].Text = txt.Split('*')[0] + "- QTY(" + txt.Split('*')[1] + ")";

                            }
                            gridview.Rows[i].Cells[j + 4].HorizontalAlign = HorizontalAlign.Right;
                        }


                        for (int k = 23; k < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); k += 4)
                        {
                            gridview.Rows[i].Cells[k + 1].Visible = false;
                            gridview.Rows[i].Cells[k + 2].Visible = false;
                        }

                    }

                    for (int k = 23; k < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); k += 4)
                    {

                        gridview.HeaderRow.Cells[k + 1].Visible = false;
                        gridview.HeaderRow.Cells[k + 2].Visible = false;


                    }

                    for (int k = 23; k < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); k += 4)
                    {

                        gridview.FooterRow.Cells[k + 1].Visible = false;
                        gridview.FooterRow.Cells[k + 2].Visible = false;



                    }

                    for (int i = gridview.Rows.Count - 1; i > 0; i--)
                    {
                        GridViewRow row = gridview.Rows[i];
                        GridViewRow previousRow = gridview.Rows[i - 1];
                        for (int j = 7; j < 8; j++)
                        {
                            if (row.Cells[j].Text == previousRow.Cells[j].Text || row.Cells[j].Text == "")
                            {
                                if (previousRow.Cells[j].RowSpan == 0)
                                {
                                    if (row.Cells[j].RowSpan == 0)
                                    {
                                        previousRow.Cells[j].RowSpan += 2;
                                    }
                                    else
                                    {
                                        previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                                    }
                                    row.Cells[j].Visible = false;
                                }
                            }


                        }

                    }
                }
                gridview.FooterRow.BackColor = Color.YellowGreen;
                gridview.HeaderStyle.Font.Bold = true;
                gridview.RenderControl(htmltextwrtter);
                Response.Write(strwritter.ToString());
                Response.End();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        protected void btnUpdatefinish_Click(object sender, EventArgs e)
        {

            if ((hdnSelectedChanged.Value != "" && hdnSelectedChanged.Value != "" )|| (hdnrejected.Value!=null && hdnrejected.Value != "") || (hdnSlectedQutations.Value != "" && hdnSlectedQutations != null))
            {
                if (hdnSelectedChanged.Value != "" && hdnSelectedChanged.Value != "")
                {

                    string[] SelectedArray = hdnSelectedChanged.Value.ToString().Split(',');
                    for (int i = 0; i < SelectedArray.Length; i += 6)
                    {
                        tabulationDetailController.UpdateTabulationDetails(int.Parse(SelectedArray[i]), int.Parse(SelectedArray[i + 1]), int.Parse(SelectedArray[i + 2]), int.Parse(SelectedArray[i + 3]), int.Parse(SelectedArray[i + 4]), int.Parse(SelectedArray[i + 5]));
                    }

                  

                }
                if (hdnrejected.Value != null && hdnrejected.Value != "")
                {
                    string[] SelectedArray = hdnrejected.Value.ToString().Split(',');
                    for (int i = 0; i < SelectedArray.Length; i += 6)
                    {
                        tabulationDetailController.UpdateUnselectedTabulationDetails(int.Parse(SelectedArray[i]), int.Parse(SelectedArray[i + 1]), int.Parse(SelectedArray[i + 2]), int.Parse(SelectedArray[i + 3]), int.Parse(SelectedArray[i + 4]), int.Parse(SelectedArray[i + 5]));
                    }
                }
                if (hdnSlectedQutations.Value != "" && hdnSlectedQutations != null)
                {
                    string[] SelectedArray = hdnSlectedQutations.Value.ToString().Split(',');
                    for (int i = 0; i < SelectedArray.Length; i += 6)
                    {
                        tabulationDetailController.UpdateTabulationDetails(int.Parse(SelectedArray[i]), int.Parse(SelectedArray[i + 1]), int.Parse(SelectedArray[i + 2]), int.Parse(SelectedArray[i + 3]), int.Parse(SelectedArray[i + 4]), int.Parse(SelectedArray[i + 5]));
                    }
                }

                var IsUpdated = tabulationMasterController.UpdateTabulationMasterNetTotalAfterUpdate(int.Parse(hdnTabulationId.Value), int.Parse(ViewState["PrId"].ToString()), int.Parse(ViewState["BidId"].ToString()), int.Parse(Session["UserId"].ToString()), txtareaRemark.Text);
                if (IsUpdated > 0)
                {
                    var PrMaster = pr_MasterController.GetPrForQuotationApproval(int.Parse(ViewState["PrId"].ToString()), int.Parse(Session["CompanyId"].ToString()), new JavaScriptSerializer().Deserialize<List<int>>(ViewState["RecommendableTabulationIds"].ToString()), int.Parse(Session["UserId"].ToString()), int.Parse(Session["DesignationId"].ToString()));
                    ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
                    LoadQuotations();
                    txtareaRemark.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {$('div').removeClass('modal-backdrop'); $('#mdlQuotations').modal('show'); });   </script>", false);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('div').removeClass('modal-backdrop');  swal({ type: 'error',title: 'ERROR',text:'Please Select'}).then((result) => { $('#mdlQuotations').modal('show') }); });   </script>", false);
            }


        }

        protected void btnuplodDock_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlviewdocsuplod').modal('show');});   </script>", false);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if ((fileUpload1.PostedFile != null && fileUpload1.PostedFile.ContentLength > 0))
            {
                int seq = 0;
                var tabulationMaster = new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString());
                var isUploaded = committeeController.IsDocsUploaded(int.Parse(ViewState["BidId"].ToString()), tabulationMaster.TabulationId, "T");
                if (isUploaded > 0)
                {
                    seq= committeeController.GetUploedasequence(int.Parse(ViewState["BidId"].ToString()), tabulationMaster.TabulationId, "T");
                }
                
                List<TecCommitteeFileUpload> Committeefiles = new List<TecCommitteeFileUpload>();
              
                HttpFileCollection uploadedFiles = Request.Files;
                for (int i = 0; i < uploadedFiles.Count; i++)
                {

                    HttpPostedFile userPostedFile = uploadedFiles[i];
                    string path = "";
                    var extention = System.IO.Path.GetExtension(userPostedFile.FileName);
                    string filenameWithoutPath = Path.GetFileName(userPostedFile.FileName);
                    string imagename = LocalTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + filenameWithoutPath;
                    path = "~/TechCommitteeDocs/" + imagename;
                    userPostedFile.SaveAs(Server.MapPath("TechCommitteeDocs") + "\\" + imagename);
                    seq = seq + 1;
                    TecCommitteeFileUpload Committeefile = new TecCommitteeFileUpload();
                    Committeefile.BidId = int.Parse(ViewState["BidId"].ToString());
                    Committeefile.tabulationId = int.Parse(hdnTabulationId.Value);
                    Committeefile.filename = imagename;
                    Committeefile.filepath = path;
                    Committeefile.sequenceId = seq;
                    Committeefile.commiteetype = "T";
                    Committeefiles.Add(Committeefile);


                }

                var isSaved = committeeController.SaveUploadedtechcommitteefiles(Committeefiles);
                if (isSaved > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();swal({ type: 'success',title: 'Your Documents have been saved', showConfirmButton: false,timer: 1500}).then((result) => { $('#mdlQuotations').modal('show') }); });   </script>", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();swal({ type: 'error',title: 'ERROR',text:'Error Sving files', showConfirmButton: true,timer: 4000}).then((result) => { $('#mdlviewdocsuplod').modal('show') }); });   </script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();swal({ type: 'error',title: 'ERROR',text:'Empty files or Invalid fomat', showConfirmButton: true,timer: 4000}).then((result) => { $('#mdlviewdocsuplod').modal('show') }); $('.modal-backdrop').remove(); });   </script>", false);
            }
        }

        protected void btnTechdocView_Click(object sender, EventArgs e)
        {
            try
            {

                List<TecCommitteeFileUpload> bidingplandoc = committeeController.Gettechcommitteefiles(int.Parse(ViewState["BidId"].ToString()), int.Parse(hdnTabulationId.Value),"T");
                gvbddifiles.DataSource = bidingplandoc;
                gvbddifiles.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlviewdocstechCommitee').modal('show');});   </script>", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void lbtnDownload_Click(object sender, EventArgs e)
        {
            try
            {


                var row = ((GridViewRow)((LinkButton)sender).NamingContainer);
                string path = row.Cells[1].Text;
                path = path.Remove(0, 2);
                string Filpath = Server.MapPath(path);
                string fileName = row.Cells[0].Text;
                var response = System.Web.HttpContext.Current;

                System.IO.FileInfo file = new System.IO.FileInfo(Filpath);
                if (file.Exists)
                {

                    response.Response.ClearContent();
                    response.Response.Clear();
                    string ext = Path.GetExtension(path); //get file extension
                    string type = "";

                    //set known types based on file extension
                    if (ext != null)
                    {
                        switch (ext.ToLower())
                        {
                            case ".htm":
                            case ".html":
                                type = "text/HTML";
                                break;

                            case ".txt":
                                type = "text/plain";
                                break;

                            case ".GIF":
                                type = "image/GIF";
                                break;

                            case ".pdf":
                                type = "Application/pdf";
                                break;

                            case ".doc":
                            case ".rtf":
                                type = "Application/msword";
                                break;
                            case ".jpg":
                                type = "image/jpeg";
                                break;
                            case ".csv":
                                type = "text/csv";
                                break;
                            case ".jpeg":
                            case ".xls":
                                type = "application/vnd.xls";
                                break;
                            case ".zip":
                                type = "application/zip";
                                break;
                            case ".ppt":
                                type = "application/vnd.ms-powerpoint";
                                break;
                            case ".png":
                                type = "image/png";
                                break;

                        }
                    }
                    response.Response.ContentType = type;
                    response.Response.AddHeader("Content-Disposition",
                                       "attachment; filename=" + fileName + ";");
                    response.Response.TransmitFile(Server.MapPath(path));

                    response.Response.ContentType = "application/octet-stream";

                    response.Response.WriteFile(Server.MapPath(path));
                    response.Response.Flush();
                    response.Response.End();
                   
                    //  ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { window.open('/" + path + "');$('.modal-backdrop').remove(); $('#mdlviewdocs').modal('show');});   </script>", false);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

           
        }

        protected void btnRejectedView_Click(object sender, EventArgs e)
        {
            var TabulationID = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[0].Text);
            var rejectedBidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
            ViewState["ReprintBidCode"] = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == rejectedBidId).BidCode.ToString();
            ViewState["ReprintPrCode"] = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCode;
            var tabulationMaster = tabulationMasterController.GetTabulationsByTabulationId(TabulationID);
            ViewState["tabulationMaster"] = new JavaScriptSerializer().Serialize(tabulationMaster);
            List<TabulationDetail> tabulationDetails = tabulationDetailController.GetTabulationDetailsByTabulationId(TabulationID);

            if (PurchaseType.Value == "1")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Refno");
                dt.Columns.Add("TabulationId");
                dt.Columns.Add("QuotationId");
                dt.Columns.Add("BidId");
                dt.Columns.Add("SupplierId");
                dt.Columns.Add("SupplierName");
                var biddingItems = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == rejectedBidId).BiddingItems;
                ViewState["BidId"] = rejectedBidId;
                var reprintItemcount = 0;
                foreach (var item in biddingItems)
                {
                    dt.Columns.Add("Item Description-" + item.ItemName);
                    dt.Columns.Add("Item Code-" + item.ItemName);
                    dt.Columns.Add("Item Price -" + item.ItemName + "-QTY-" + item.Qty.ToString());
                    reprintItemcount++;
                    ViewState["reprintItemcount"] = reprintItemcount;
                }

                int qutaitoid = 0;

                for (int i = 0; i < tabulationDetails.Count; i++)
                {

                    if (qutaitoid != tabulationDetails[i].QuotationId)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["Refno"] = tabulationDetails[i].Refno;
                        newRow["TabulationId"] = tabulationDetails[i].TabulationId.ToString();
                        newRow["QuotationId"] = tabulationDetails[i].QuotationId.ToString();
                        newRow["BidId"] = tabulationMaster.BidId.ToString();
                        newRow["SupplierId"] = tabulationDetails[i].SupplierId.ToString();
                        newRow["SupplierName"] = tabulationDetails[i].SupplierName;
                        foreach (var item in biddingItems)
                        {
                            foreach (var item2 in tabulationDetails)
                            {
                                if (item.ItemId == item2.ItemId && tabulationDetails[i].QuotationId == item2.QuotationId)
                                {
                                    newRow["Item Description-" + item.ItemName] = item2.Description == null || item2.Description == "" ? "No Description" + " : " : item2.Description;
                                    newRow["Item Code-" + item.ItemName] = item.ItemId.ToString();
                                    if (item2.IsSelected == 1)
                                    {

                                        newRow["Item Price -" + item.ItemName + "-QTY-" + item.Qty.ToString()] = item2.UnitPrice.ToString("#,##0.00") + "*" + item2.Qty.ToString();
                                    }
                                    else
                                    {
                                        newRow["Item Price -" + item.ItemName + "-QTY-" + item.Qty.ToString()] = item2.UnitPrice.ToString("#,##0.00");
                                    }
                                }
                            }



                        }

                        qutaitoid = tabulationDetails[i].QuotationId;
                        dt.Rows.Add(newRow);

                    }

                }





                gvrjectedTabulationsheet.DataSource = dt;
                gvrjectedTabulationsheet.DataBind();
            }

            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Refno");
                dt.Columns.Add("TabulationId");
                dt.Columns.Add("QuotationId");
                dt.Columns.Add("BidId");
                dt.Columns.Add("SupplierId");
                dt.Columns.Add("SupplierName");
                dt.Columns.Add("Agent");
                dt.Columns.Add("Currency");
                dt.Columns.Add("Country");
                dt.Columns.Add("Brand");
                dt.Columns.Add("Term");
                var biddingItems = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == rejectedBidId).BiddingItems;
                ViewState["BidId"] = rejectedBidId;
               var reprintItemcount = 0;
                foreach (var item in biddingItems)
                {
                    dt.Columns.Add("Item Description");
                    dt.Columns.Add("Item Code");
                    dt.Columns.Add("Item Price");
                    dt.Columns.Add("Item Price (LKR) -QTY-" + item.Qty.ToString());
                    reprintItemcount++;
                    ViewState["reprintItemcount"] = reprintItemcount;
                }

                dt.Columns.Add("CIF");
                dt.Columns.Add("CIF LKR");
                dt.Columns.Add("Duty & PAL LKR");
                dt.Columns.Add("Est Clearing /Mt LKR");
                dt.Columns.Add("Other Cost LKR");
                dt.Columns.Add("Import History");
                dt.Columns.Add("Validity");
                dt.Columns.Add("EST. Delivery");
                dt.Columns.Add("Remarks");
                int qutaitoid = 0;
                var itemname = string.Empty;
                for (int i = 0; i < tabulationDetails.Count; i++)
                {

                    if (qutaitoid != tabulationDetails[i].QuotationId)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["Refno"] = tabulationDetails[i].Refno;
                        newRow["TabulationId"] = tabulationDetails[i].TabulationId.ToString();
                        newRow["QuotationId"] = tabulationDetails[i].QuotationId.ToString();
                        newRow["BidId"] = tabulationMaster.BidId.ToString();
                        newRow["SupplierId"] = tabulationDetails[i].SupplierId.ToString();
                        newRow["SupplierName"] = tabulationDetails[i].SupplierName;
                        newRow["Agent"] = tabulationDetails[i].AgentName;
                        newRow["Currency"] = tabulationDetails[i].CurrencyName;
                        newRow["Country"] = tabulationDetails[i].CountryName;
                        newRow["Brand"] = tabulationDetails[i].Brand;

                        foreach (var item in biddingItems)
                        {
                            foreach (var item2 in tabulationDetails)
                            {
                                if (item.ItemId == item2.ItemId && tabulationDetails[i].QuotationId == item2.QuotationId)
                                {

                                    newRow["Item Description"] = item2.Description == null || item2.Description == "" ? "No Description" + " : " : item2.Description;
                                    newRow["Item Code"] = item.ItemId.ToString();
                                    newRow["Item Price"] = (item2.UnitPrice / item2.ExchangeRate).ToString("#,##0.00");
                                    if (item2.IsSelected == 1)
                                    {
                                        newRow["Item Price (LKR) -QTY-" + item.Qty.ToString()] = item2.UnitPrice.ToString("#,##0.00") + "*" + item2.Qty.ToString();
                                    }
                                    else
                                    {
                                        newRow["Item Price (LKR) -QTY-" + item.Qty.ToString()] = item2.UnitPrice.ToString("#,##0.00");
                                    }

                                    itemname = item.ItemName;


                                }
                            }

                        }

                        newRow["Term"] = tabulationDetails[i].Terms;
                        newRow["CIF"] = tabulationDetails[i].CIF.ToString("#,##0.00");
                        newRow["CIF LKR"] = (tabulationDetails[i].CIF * tabulationDetails[i].ExchangeRate).ToString("#,##0.00");
                        newRow["Duty & PAL LKR"] = (tabulationDetails[i].Dutypal).ToString("#,##0.00");
                        newRow["Est Clearing /Mt LKR"] = (tabulationDetails[i].Clearingcost).ToString("#,##0.00");
                        newRow["Other Cost LKR"] = tabulationDetails[i].Other.ToString("#,##0.00");
                        newRow["Import History"] = tabulationDetails[i].History;
                        newRow["Validity"] = tabulationDetails[i].Validity.ToString("yyyy-MMM-dd");
                        newRow["EST. Delivery"] = tabulationDetails[i].Estdelivery;
                        newRow["Remarks"] = tabulationDetails[i].Remark;
                        qutaitoid = tabulationDetails[i].QuotationId;
                        dt.Rows.Add(newRow);

                    }
                }

                gvrjectedTabulationsheet.DataSource = dt;
                gvrjectedTabulationsheet.DataBind();
                gvrjectedTabulationsheet.Width = 1500;

                gvrjectedTabulationsheet.Caption = itemname;

            }




            for (int i = gvrjectedTabulationsheet.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = gvrjectedTabulationsheet.Rows[i];
                GridViewRow previousRow = gvrjectedTabulationsheet.Rows[i - 1];
                for (int j = 6; j <7 ; j++)
                {
                    if (row.Cells[j].Text == previousRow.Cells[j].Text || row.Cells[j].Text == "")
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].CssClass = "hidden";
                        }
                    }


                }
            }

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlRejectedTabulations').modal('show') });   </script>", false);

        }

        protected void gvrjectedTabulationsheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (PurchaseType.Value == "1")
            {
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;


                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    for (int i = 14; i < 14 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 3); i += 3)
                    {
                        if (e.Row.Cells[i + 3].Text == "&nbsp;")
                        {
                            e.Row.Cells[i + 3].CssClass = "";
                        }
                        else if (e.Row.Cells[i + 3].Text.Split('*').Length > 1)
                        {
                            e.Row.Cells[i + 3].CssClass = "greenBg";
                        }
                        else
                        {
                            e.Row.Cells[i + 3].CssClass = "";
                        }

                        e.Row.Cells[i + 3].HorizontalAlign = HorizontalAlign.Right;

                    }

                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    var tabulationMaster = new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString());
                    e.Row.Cells[0].Text = "Total Without Tax: <br/> Nbt Amount: <br/> Vat Amount: <br/> Total With Tax: ";
                    e.Row.Cells[6].Text = tabulationMaster.SubTotal.ToString("#,##0.00") + "<br/>" + tabulationMaster.NbtAmount.ToString("#,##0.00") + "<br/>" + tabulationMaster.VatAmount.ToString("#,##0.00") + "<br/>" + tabulationMaster.NetTotal.ToString("#,##0.00");

                    e.Row.CssClass = "footer-font";
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                }
                for (int i = 14; i < 14 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 3); i += 3)
                {

                    e.Row.Cells[i + 2].CssClass = "hidden";

                }


            }
            else
            {

                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[15].Visible = false;


                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    for (int i = 19; i < 19 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); i += 4)
                    {
                        if (e.Row.Cells[i + 4].Text == "&nbsp;")
                        {
                            e.Row.Cells[i + 4].CssClass = "";
                        }
                        else if (e.Row.Cells[i + 4].Text.Split('*').Length > 1)
                        {
                            e.Row.Cells[i + 4].CssClass = "alignright greenBg";
                        }
                        else
                        {
                            e.Row.Cells[i + 4].CssClass = "";
                        }

                        e.Row.Cells[i + 4].HorizontalAlign = HorizontalAlign.Right;

                    }

                    for (int i = 20 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); i < e.Row.Cells.Count - 2; i++)
                    {
                        e.Row.Cells[i].CssClass = "alignright";

                    }
                    for (int i = 1; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Font.Bold = true;
                    }

                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    var tabulationMaster = new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString());
                    e.Row.Cells[0].Text = "Total Without Tax: <br/> Nbt Amount: <br/> Vat Amount: <br/> Total With Tax: ";
                    e.Row.Cells[6].Text = tabulationMaster.SubTotal.ToString("#,##0.00") + "<br/>" + tabulationMaster.NbtAmount.ToString("#,##0.00") + "<br/>" + tabulationMaster.VatAmount.ToString("#,##0.00") + "<br/>" + tabulationMaster.NetTotal.ToString("#,##0.00");

                    e.Row.CssClass = "footer-font";
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                }
                for (int i = 19; i < 19 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); i += 4)
                {
                    e.Row.Cells[i + 1].CssClass = "hidden";
                    e.Row.Cells[i + 2].CssClass = "hidden";

                }
            }

        }


        protected void ReprintExcel(GridView gridview, string filename, string header)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = filename + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                string headerTable = @"<Table><tr><td  colspan='3'><center><h4>" + header + " For PR Code: " + ViewState["ReprintPrCode"] + " Bid No:B" + ViewState["ReprintBidCode"] + " </h4></center> </td></tr></Table> <hr>";
                Response.Write(headerTable);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                gridview.GridLines = GridLines.Both;
                gridview.HeaderRow.Cells[2].Visible = false;
                gridview.HeaderRow.Cells[3].Visible = false;
                gridview.HeaderRow.Cells[4].Visible = false;
                gridview.HeaderRow.Cells[5].Visible = false;
                gridview.HeaderRow.Cells[7].Visible = false;
                gridview.HeaderRow.Cells[8].Visible = false;

                gridview.FooterRow.Cells[2].Visible = false;
                gridview.FooterRow.Cells[3].Visible = false;
                gridview.FooterRow.Cells[4].Visible = false;
                gridview.FooterRow.Cells[5].Visible = false;
                gridview.FooterRow.Cells[7].Visible = false;
                gridview.FooterRow.Cells[8].Visible = false;



                for (int i = 0; i < gridview.Rows.Count; i++)
                {
                    gridview.Rows[i].Cells[6].RowSpan = 0;
                }

                if (PurchaseType.Value == "1")
                {


                    for (int i = 0; i < gridview.Rows.Count; i++)
                    {
                        gridview.Rows[i].Cells[2].Visible = false;
                        gridview.Rows[i].Cells[3].Visible = false;
                        gridview.Rows[i].Cells[4].Visible = false;
                        gridview.Rows[i].Cells[5].Visible = false;

                        gridview.Rows[i].Cells[7].Visible = false;
                        gridview.Rows[i].Cells[8].Visible = false;



                        for (int j = 14; j < 14 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 3); j += 3)
                        {
                            if (gridview.Rows[i].Cells[j + 3].Text.Split('*').Length > 1)
                            {
                                gridview.Rows[i].Cells[j + 3].BackColor = Color.Green;
                                var txt = gridview.Rows[i].Cells[j + 3].Text;
                                gridview.Rows[i].Cells[j + 3].Text = txt.Split('*')[0] + "- QTY(" + txt.Split('*')[1] + ")";

                            }

                        }


                        for (int k = 14; k < 14 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 3); k += 3)
                        {

                            gridview.Rows[i].Cells[k + 2].Visible = false;
                        }

                    }

                    for (int k = 14; k < 14 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 3); k += 3)
                    {


                        gridview.HeaderRow.Cells[k + 2].Visible = false;


                    }

                    for (int k = 14; k < 14 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 3); k += 3)
                    {


                        gridview.FooterRow.Cells[k + 2].Visible = false;


                    }




                }
                else
                {
                    for (int i = 0; i < gridview.Rows.Count; i++)
                    {
                        gridview.Rows[i].Cells[2].Visible = false;
                        gridview.Rows[i].Cells[3].Visible = false;
                        gridview.Rows[i].Cells[4].Visible = false;
                        gridview.Rows[i].Cells[5].Visible = false;

                        gridview.Rows[i].Cells[7].Visible = false;
                        gridview.Rows[i].Cells[8].Visible = false;



                        for (int j = 19; j < 19 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); j += 4)
                        {
                            if (gridview.Rows[i].Cells[j + 4].Text.Split('*').Length > 1)
                            {
                                gridview.Rows[i].Cells[j + 4].BackColor = Color.Green;
                                var txt = gridview.Rows[i].Cells[j + 4].Text;
                                gridview.Rows[i].Cells[j + 4].Text = txt.Split('*')[0] + "- QTY(" + txt.Split('*')[1] + ")";

                            }

                        }


                        for (int k = 19; k < 19 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); k += 4)
                        {
                            gridview.Rows[i].Cells[k + 1].Visible = false;
                            gridview.Rows[i].Cells[k + 2].Visible = false;
                        }

                    }

                    for (int k = 19; k < 19 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); k += 4)
                    {

                        gridview.HeaderRow.Cells[k + 1].Visible = false;
                        gridview.HeaderRow.Cells[k + 2].Visible = false;


                    }

                    for (int k = 19; k < 19 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); k += 4)
                    {

                        gridview.FooterRow.Cells[k + 1].Visible = false;
                        gridview.FooterRow.Cells[k + 2].Visible = false;


                    }
                }

                for (int i = gridview.Rows.Count - 1; i > 0; i--)
                {
                    GridViewRow row = gridview.Rows[i];
                    GridViewRow previousRow = gridview.Rows[i - 1];
                    for (int j = 6; j < 7; j++)
                    {
                        if (row.Cells[j].Text == previousRow.Cells[j].Text || row.Cells[j].Text == "")
                        {
                            if (previousRow.Cells[j].RowSpan == 0)
                            {
                                if (row.Cells[j].RowSpan == 0)
                                {
                                    previousRow.Cells[j].RowSpan += 2;
                                }
                                else
                                {
                                    previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                                }
                                row.Cells[j].Visible = false;
                            }
                        }


                    }


                }
                gridview.FooterRow.BackColor = Color.YellowGreen;
                gridview.HeaderStyle.Font.Bold = true;
                gridview.RenderControl(htmltextwrtter);
                Response.Write(strwritter.ToString());
                Response.End();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btnreprint_Click(object sender, EventArgs e)
        {
            ReprintExcel(gvrjectedTabulationsheet, "Tabulation Sheet-Reprint", "Tabulation Sheet-Reprint " + LocalTime.Now.ToString("yyyy/MMM/dd"));
        }

        

        protected void btnRecDocs_Click(object sender, EventArgs e)
        {
            try
            {
                var TabulationID = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[0].Text);
                var rejectedBidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
                List<TecCommitteeFileUpload> bidingplandoc = committeeController.Gettechcommitteefiles(rejectedBidId, TabulationID, "T");
                gvViewdocsrejected.DataSource = bidingplandoc;
                gvViewdocsrejected.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlviewdocs').modal('show');});   </script>", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnProcDoc_Click(object sender, EventArgs e)
        {
            try
            {

                var TabulationID = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[0].Text);
                var rejectedBidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
                List<TecCommitteeFileUpload> bidingplandoc = committeeController.Gettechcommitteefiles(rejectedBidId, TabulationID, "T");
                gvViewdocsrejected.DataSource = bidingplandoc;
                gvViewdocsrejected.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlviewdocs').modal('show');});   </script>", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}