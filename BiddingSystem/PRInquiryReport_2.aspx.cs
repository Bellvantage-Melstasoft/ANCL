using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class PRInquiryReport_2 : System.Web.UI.Page
    {
        PrControllerV2 PrMasterController = ControllerFactory.CreatePrControllerV2();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        public string PRCode;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "PRInquiryReport_2.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "PRInquiryReport_2Link";

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 5, 7) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                // LoadPRs();
            }

            PRCode = Request.QueryString["PRCode"];

            if (PRCode != null)
            {
                txtPrCode.Text = PRCode;
                btnSearch_Click(sender, new EventArgs());
            }
        }
        //private void LoadPRs() {

        //    try {
        //        List<PrMasterV2> prMaster = PrMasterController.GetPRListForPrInquiry(int.Parse(Session["CompanyId"].ToString()));

        //        ddlPr.DataSource = prMaster;
        //        ddlPr.DataValueField = "PrId";
        //        ddlPr.DataTextField = "PrCodeText";
        //        ddlPr.DataBind();
        //        ddlPr.Items.Insert(0, new ListItem("Select A PR", ""));
        //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.select2').select2(); });   </script>", false);

        //    }
        //    catch (Exception ex) {
        //        throw;
        //    }
        //}
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtPrCode.Text != "")
            {

                //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                //int prcode = int.Parse(newString);
                string prcode = txtPrCode.Text;

                PrMasterV2 pr = PrMasterController.getPrIdForPRCode(prcode);
                PrMasterV2 PrMaster = PrMasterController.GetPRs(pr.PrId, int.Parse(Session["CompanyId"].ToString()));
                ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
                //var prCode = ddlPr.SelectedValue;

                gvPurchaseRequest.DataSource = new List<PrMasterV2> { PrMaster };
                gvPurchaseRequest.DataBind();

                gvBids.DataSource = PrMaster.Bids;
                gvBids.DataBind();

                gvPO.DataSource = PrMaster.POsCreated;
                gvPO.DataBind();

                List<GrnMaster> grns = PrMaster.GRNsCreated;

                int grnId = 0;
                for (int i = 0; i < grns.Count; i++)
                {

                    if (i > 0)
                    {
                        grnId = grns[i - 1].GrnId;
                        if (grns[i].GrnId == grnId)
                        {
                            grns.Remove(grns[i]);
                        }
                    }

                }
                gvGrn.DataSource = grns;
                gvGrn.DataBind();

                if (PrMaster.MrnId == 0)
                {

                    gvPurchaseRequest.Columns[3].Visible = false;

                }
                else
                {
                    gvPurchaseRequest.Columns[3].Visible = true;
                }
            }
        }

        protected void gvGrn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int grnId = int.Parse(gvGrn.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvGrnItems = e.Row.FindControl("gvGrnItems") as GridView;

                gvGrnItems.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).GRNsCreated.Find(grn => grn.GrnId == grnId).GrnDetailsList;
                gvGrnItems.DataBind();
            }
        }

        protected void gvPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int poId = int.Parse(gvPO.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvPoItems = e.Row.FindControl("gvPoItems") as GridView;

                gvPoItems.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).POsCreated.Find(po => po.PoID == poId).PoDetails;
                gvPoItems.DataBind();
            }

        }

        protected void gvPurchaseRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView gvPrDetails = e.Row.FindControl("gvPrDetails") as GridView;
                PrMasterV2 PR = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (PR.MrnCode == null)
                    {
                        e.Row.Cells[4].Style["display"] = "none";
                    }
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (PR.MrnCode == null)
                    {
                        e.Row.Cells[4].Style["display"] = "none";
                    }
                    gvPrDetails.DataSource = PR.Items;
                    gvPrDetails.DataBind();



                    GridView gvMrnDetails = e.Row.FindControl("gvMrnDetails") as GridView;

                    List<MrnDetailsV2> details = PR.MRNDetail;
                    gvMrnDetails.DataSource = details;
                    gvMrnDetails.DataBind();

                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void gvPrDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvStatusLog = e.Row.FindControl("gvStatusLog") as GridView;
                PrMasterV2 PR = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                int prDId = int.Parse(e.Row.Cells[1].Text);

                gvStatusLog.DataSource = ControllerFactory.CreatePRDetailsStatusLogController().PrLogDetails(prDId);
                gvStatusLog.DataBind();


            }
        }

        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;
                    int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());

                    PrMasterV2 PRM = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                    List<BiddingItem> bidItems = PRM.Bids.Find(b => b.BidId == bidId).BiddingItems;
                    bidItems.ForEach(bi => bi.QuotationCount = bi.SupplierQuotationItems.Count);



                    gvBidItems.DataSource = bidItems;
                    gvBidItems.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        protected void gvBidItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PrMasterV2 PrMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());

                int BidId = int.Parse(e.Row.Cells[3].Text);
                int BidItemId = int.Parse(e.Row.Cells[2].Text);
                GridView gvQuotationItems = e.Row.FindControl("gvQuotationItems") as GridView;



                DataTable dt = new DataTable();

                dt.Columns.Add("QuotationItemId");
                dt.Columns.Add("QuotationId");
                dt.Columns.Add("BiddingItemId");
                dt.Columns.Add("SupplierId");
                dt.Columns.Add("SupplierName");
                dt.Columns.Add("Ratings");
                dt.Columns.Add("Description");
                dt.Columns.Add("UnitPrice");
                dt.Columns.Add("ReqQty");
                dt.Columns.Add("SubTotal");
                dt.Columns.Add("NbtAmount");
                dt.Columns.Add("VatAmount");
                dt.Columns.Add("NetTotal");
                dt.Columns.Add("SpecComply");
                dt.Columns.Add("Actions");
                dt.Columns.Add("ShowReject");
                dt.Columns.Add("IsBidItemSelected");
                dt.Columns.Add("IsSelected");
                dt.Columns.Add("ApprovalRemark");

                Bidding bid = PrMaster.Bids.Find(b => b.BidId == BidId);

                List<SupplierQuotationItem> quotationItems = bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).SupplierQuotationItems;

                quotationItems = quotationItems.OrderBy(q => q.UnitPrice).ToList();

                for (int i = 0; i < quotationItems.Count; i++)
                {
                    SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == quotationItems[i].QuotationId);


                    DataRow newRow = dt.NewRow();
                    newRow["QuotationItemId"] = quotationItems[i].QuotationItemId;
                    newRow["QuotationId"] = quotationItems[i].QuotationId;
                    newRow["BiddingItemId"] = quotationItems[i].BiddingItemId;
                    newRow["SupplierId"] = quotation.SupplierId;
                    newRow["SupplierName"] = quotation.SupplierName;

                    if (quotation.SupplierRating.Rating == 0)
                        newRow["Ratings"] = "☆☆☆☆☆";
                    else if (quotation.SupplierRating.Rating == 1)
                        newRow["Ratings"] = "★☆☆☆☆";
                    else if (quotation.SupplierRating.Rating == 2)
                        newRow["Ratings"] = "★★☆☆☆";
                    else if (quotation.SupplierRating.Rating == 3)
                        newRow["Ratings"] = "★★★☆☆";
                    else if (quotation.SupplierRating.Rating == 4)
                        newRow["Ratings"] = "★★★★☆";
                    else if (quotation.SupplierRating.Rating == 5)
                        newRow["Ratings"] = "★★★★★";

                    newRow["Description"] = quotationItems[i].Description;
                    newRow["UnitPrice"] = quotationItems[i].UnitPrice.ToString("N2");
                    newRow["ReqQty"] = quotationItems[i].TQty.ToString("N2");
                    newRow["SubTotal"] = quotationItems[i].TSubTot.ToString("N2");
                    newRow["NbtAmount"] = quotationItems[i].NbtAmount.ToString("N2");
                    newRow["VatAmount"] = quotationItems[i].TVat.ToString("N2");
                    newRow["NetTotal"] = quotationItems[i].TNetTot.ToString("N2");


                    List<SupplierBOM> boms = quotationItems[i].SupplierBOMs;

                    if (boms == null || (boms != null && boms.Count == 0))
                    {
                        newRow["SpecComply"] = "No Specs";
                    }
                    else if (boms.Count == boms.Count(b => b.Comply == 1))
                    {
                        newRow["SpecComply"] = "Yes";
                    }
                    else if (boms.Count == boms.Count(b => b.Comply == 0))
                    {
                        newRow["SpecComply"] = "No";
                    }
                    else
                    {
                        newRow["SpecComply"] = "Some";
                    }

                    newRow["IsBidItemSelected"] = bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1 ? "1" : "0";
                    newRow["IsSelected"] = quotationItems[i].SelectedQuotation == 1 ? "1" : "0";
                    newRow["ApprovalRemark"] = quotationItems[i].SupplierApprovalRemark;

                    if (i == 0)
                    {
                        if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1)
                            newRow["Actions"] = "0";
                        else
                            newRow["Actions"] = "1";
                    }
                    else
                    {
                        newRow["Actions"] = "0";
                    }

                    if (quotationItems.Count == 1)
                    {
                        newRow["ShowReject"] = "0";
                    }
                    else
                    {
                        if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1)
                            newRow["ShowReject"] = "0";
                        else
                            newRow["ShowReject"] = "1";
                    }

                    if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsTerminated == 1)
                    {
                        newRow["Actions"] = "0";
                        newRow["ShowReject"] = "0";

                    }
                    dt.Rows.Add(newRow);

                }


                gvQuotationItems.DataSource = dt;
                gvQuotationItems.DataBind();
            }
        }




    }
}