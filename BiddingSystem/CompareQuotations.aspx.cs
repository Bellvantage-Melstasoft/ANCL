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

namespace BiddingSystem {
    public partial class CompareQuotations : System.Web.UI.Page {
        #region properties
        //  static int PrId = 0;
        // static int BidId = 0;
        //  static string PrCode ;
        // static string BidCode ;
        // static string ReprintPrCode;
        // static string ReprintBidCode;
        // static int UserId = 0;
        //  static int DesignationId = 0;
        //  static int CompanyId = 0;
        // static PrMasterV2 PrMaster;
        //  static List<int> SelectionPendingBidIds;
        //static int Itemcount=0;
        // static int reprintItemcount = 0;
        // static int tabulationId = 0;
        // static TabulationMaster tabulationMaster;
        #endregion

        #region controllers
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();
        TabulationMasterController tabulationMasterController = ControllerFactory.CreateTabulationMasterController();
        TabulationDetailController tabulationDetailController = ControllerFactory.CreateTabulationDetailController();
        CommitteeController committeeController = ControllerFactory.CreateProcurementCommitteeController();
        SubDepartmentControllerInterface departmentController = ControllerFactory.CreateSubDepartmentController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        PODetailsController pODetailsController = ControllerFactory.CreatePODetailsController();
        #endregion

        protected void Page_Load(object sender, EventArgs e) {
            
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "") {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForQuotationComparison.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "bidComparrisionLink";


                // UserId = int.Parse(Session["UserId"].ToString());
                //  DesignationId = int.Parse(Session["DesignationId"].ToString());
                // CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 5) && companyLogin.Usertype != "S") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack) {
                if (int.Parse(Session["UserId"].ToString()) != 0) {
                    try {
                        var SelectionPendingBidIds = Session["SelectionPendingBidIds"] as List<int>;
                        ViewState["SelectionPendingBidIds"] = SelectionPendingBidIds;

                        ViewState["PrId"] = int.Parse(Request.QueryString.Get("PrId"));
                        var PrMaster = pr_MasterController.GetPrForQuotationComparison(int.Parse(ViewState["PrId"].ToString()), int.Parse(Session["CompanyId"].ToString()), SelectionPendingBidIds);
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

                        Session["MesurementID"] = PrMaster.MesurementId;

                        pnlQuotationSelection.Visible = false;
                        pnlView.Visible = true;
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
            }
        }

        private void LoadGV() {
            try {
                PrMasterV2 pr = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                pr.Bids.ForEach(b => { b.NoOfQuotations = b.SupplierQuotations.Count; b.NoOfRejectedQuotations = b.SupplierQuotations.Count(sq => sq.IsSelected == 2); });
                PurchaseType.Value = pr.PurchaseType.ToString();
                gvBids.DataSource = pr.Bids.Where(b => b.IsQuotationApproved == 0 && b.IsQuotationConfirmed == 0);
                gvBids.DataBind();

                gvRejectedBidsAtApproval.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Where(b => b.IsQuotationSelected == 1 && b.IsQuotationApproved == 2 && b.IsQuotationConfirmed == 0);
                gvRejectedBidsAtApproval.DataBind();

                gvRejectedBidsAtConfirmation.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Where(b => b.IsQuotationSelected == 1 && b.IsQuotationApproved == 1 && b.IsQuotationConfirmed == 2);
                gvRejectedBidsAtConfirmation.DataBind();

                //gvRejectedTabulations.DataSource =  tabulationMasterController.GetTabulationsRejectedTabulationsByPrId(int.Parse(ViewState["PrId"].ToString()));
                //gvRejectedTabulations.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e) {

            if (e.Row.RowType == DataControlRowType.DataRow) {

                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());

                gvBidItems.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();
            }
        }

        protected void gvRejectedBidsAtApproval_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {

                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                int bidId = int.Parse(gvRejectedBidsAtApproval.DataKeys[e.Row.RowIndex].Value.ToString());

                gvBidItems.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();
            }
        }

        protected void gvRejectedBidsAtConfirmation_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {

                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                int bidId = int.Parse(gvRejectedBidsAtConfirmation.DataKeys[e.Row.RowIndex].Value.ToString());

                gvBidItems.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();
            }
        }

        protected void btnView_Click(object sender, EventArgs e) {
            var QutationCount = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[10].Text);
            if (QutationCount > 0) {

                ViewState["BidId"] = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
                ViewState["BidCode"] = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Where(x => x.BidId == int.Parse(ViewState["BidId"].ToString())).FirstOrDefault().BidCode.ToString();
                ViewState["tabulationId"] = tabulationMasterController.InsertTabulationMaster(int.Parse(ViewState["PrId"].ToString()), int.Parse(ViewState["BidId"].ToString()), int.Parse(Session["UserId"].ToString()), Convert.ToInt32(Session["MesurementID"]));
                PrMasterV2 prMaster = pr_MasterController.GetPrForQuotationComparison(int.Parse(ViewState["PrId"].ToString()), int.Parse(Session["CompanyId"].ToString()), ViewState["SelectionPendingBidIds"] as List<int>);
                ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(prMaster);
                // tabulationdetailsid=tabulationDetailController.InsertTabulationDetails(tabulationId, BidId);
                if (int.Parse(ViewState["tabulationId"].ToString()) > 0) {
                    LoadQuotations();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotations').modal('show'); });   </script>", false);
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on selecting quotation'}); });   </script>", false);

                }
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'No Qutations'}); });   </script>", false);
            }


        }

        private void LoadBidItems() {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));
            List<BiddingItem> items = bid.BiddingItems;

            //required when reviewing quotations
            items.ForEach(itms => itms.QuotationCount = 0);

            for (int i = 0; i < items.Count; i++) {

                for (int j = 0; j < bid.SupplierQuotations.Count - 1; j++) {
                    if ((bid.SupplierQuotations[j].QuotationItems.FirstOrDefault(sqi => sqi.BiddingItemId == items[i].BiddingItemId)) != null)
                        items[i].QuotationCount += 1;

                    decimal qty = items[i].Qty;
                }

            }

            gvItems.DataSource = items;
            gvItems.DataBind();
        }



        protected void btnSelectionQuotation_Click(object sender, EventArgs e) {
            try {


                ViewState["BidId"] = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
                ViewState["tabulationId"] = tabulationMasterController.InsertTabulationMaster(int.Parse(ViewState["PrId"].ToString()), int.Parse(ViewState["BidId"].ToString()), int.Parse(Session["UserId"].ToString()),Convert.ToInt32(Session["MesurementID"]));


                LoadBidItems();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotationsNew').modal('show'); });   </script>", false);
                pnlQuotationSelection.Visible = true;
                pnlView.Visible = false;

            }
            catch (Exception ex) {

            }
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                GridView gvQuotationItems = e.Row.FindControl("gvQuotationItems") as GridView;

                int BidItemId = int.Parse(gvItems.DataKeys[e.Row.RowIndex].Value.ToString());

                List<SupplierQuotationItem> quotationItems = new List<SupplierQuotationItem>();

                DataTable dt = new DataTable();

                dt.Columns.Add("QuotationItemId");
                dt.Columns.Add("QuotationId");
                dt.Columns.Add("BiddingItemId");
                dt.Columns.Add("SupplierId");
                dt.Columns.Add("ReferenceNo");
                dt.Columns.Add("SupplierName");
                dt.Columns.Add("Description");
                dt.Columns.Add("UnitPrice");
                dt.Columns.Add("SubTotal");
                dt.Columns.Add("NbtAmount");
                dt.Columns.Add("VatAmount");
                dt.Columns.Add("NetTotal");
                dt.Columns.Add("SpecComply");
                dt.Columns.Add("Actions");
                dt.Columns.Add("ShowReject");
                dt.Columns.Add("IsBidItemSelected");
                dt.Columns.Add("IsSelected");
                dt.Columns.Add("HasVat");
                dt.Columns.Add("HasNbt");
                dt.Columns.Add("NbtType");
                dt.Columns.Add("RequestedTotalQty");
                dt.Columns.Add("ItemId");
                dt.Columns.Add("TabulationId");

                Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));
                //List<TabulationMaster> tabulationMaster = bid.Tabulations;
                //int tabId = 0;
                //for (int k=0; k< tabulationMaster.Count; k++) {
                //     tabId = tabulationMaster[k].TabulationId;
                //}
                int tabId = int.Parse(ViewState["tabulationId"].ToString());
                bid.SupplierQuotations.ForEach(
                    sq => quotationItems.AddRange(sq.QuotationItems.Where(sqi => sqi.BiddingItemId == BidItemId && sqi.IsSelected != 2)));

                quotationItems = quotationItems.OrderBy(q => q.UnitPrice).Take(3).ToList();

                for (int i = 0; i < quotationItems.Count; i++) {


                    SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == quotationItems[i].QuotationId);


                    DataRow newRow = dt.NewRow();
                    newRow["QuotationItemId"] = quotationItems[i].QuotationItemId;
                    newRow["QuotationId"] = quotationItems[i].QuotationId;
                    newRow["BiddingItemId"] = quotationItems[i].BiddingItemId;
                    newRow["SupplierId"] = quotation.SupplierId;
                    newRow["SupplierName"] = quotation.SupplierName;
                    newRow["ReferenceNo"] = quotation.QuotationReferenceCode;
                    newRow["Description"] = quotationItems[i].Description;
                    newRow["UnitPrice"] = quotationItems[i].UnitPrice;
                    newRow["SubTotal"] = quotationItems[i].SubTotal;
                    newRow["NbtAmount"] = quotationItems[i].NbtAmount;
                    newRow["VatAmount"] = quotationItems[i].VatAmount;
                    newRow["NetTotal"] = quotationItems[i].TotalAmount;
                    newRow["HasVat"] = quotationItems[i].HasVat;
                    newRow["HasNbt"] = quotationItems[i].HasNbt;
                    newRow["NbtType"] = quotationItems[i].NbtCalculationType;

                    ViewState["SubTotal"] = quotationItems[i].SubTotal;
                    ViewState["NbtAmount"] = quotationItems[i].NbtAmount;
                    ViewState["VatAmount"] = quotationItems[i].VatAmount;
                    ViewState["TotalAmount"] = quotationItems[i].TotalAmount;
                    newRow["RequestedTotalQty"] = quotationItems[i].Qty;
                    newRow["ItemId"] = quotationItems[i].ItemId;
                    newRow["TabulationId"] = tabId;


                    List<SupplierBOM> boms = quotationItems[i].SupplierBOMs;

                    if (boms == null || (boms != null && boms.Count == 0)) {
                        newRow["SpecComply"] = "No Specs";
                    }
                    else if (boms.Count == boms.Count(b => b.Comply == 1)) {
                        newRow["SpecComply"] = "Yes";
                    }
                    else if (boms.Count == boms.Count(b => b.Comply == 0)) {
                        newRow["SpecComply"] = "No";
                    }
                    else {
                        newRow["SpecComply"] = "Some";
                    }


                    newRow["IsBidItemSelected"] = bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1 ? "1" : "0";
                    newRow["IsSelected"] = quotationItems[i].IsSelected == 1 ? "1" : "0";

                    if (i == 0) {
                        if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1)
                            newRow["Actions"] = "0";
                        else
                            newRow["Actions"] = "1";

                    }
                    else {
                        newRow["Actions"] = "0";
                    }

                    if (quotationItems.Count == 1) {
                        newRow["ShowReject"] = "0";
                    }
                    else {
                        if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1)
                            newRow["ShowReject"] = "0";
                        else
                            newRow["ShowReject"] = "1";
                    }

                    if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsTerminated == 1) {
                        newRow["Actions"] = "0";
                        newRow["ShowReject"] = "0";

                    }


                    dt.Rows.Add(newRow);

                }

                gvQuotationItems.DataSource = dt;
                gvQuotationItems.DataBind();

                for (int j = 0; j <= gvQuotationItems.Rows.Count - 1; j++) {

                    TextBox RequestingQty = (gvQuotationItems.Rows[j].FindControl("txtRequestingQty")) as TextBox;
                    Label SubTotal = (gvQuotationItems.Rows[j].FindControl("lblSubTotal")) as Label;
                    Label Nbt = (gvQuotationItems.Rows[j].FindControl("lblNbt")) as Label;
                    Label Vat = (gvQuotationItems.Rows[j].FindControl("lblVat")) as Label;
                    Label NetTotal = (gvQuotationItems.Rows[j].FindControl("lblNetTotal")) as Label;


                    TextBox TSubTotal = (gvQuotationItems.Rows[j].FindControl("txtSubTotal")) as TextBox;
                    TextBox TNbt = (gvQuotationItems.Rows[j].FindControl("txtNbt")) as TextBox;
                    TextBox TVat = (gvQuotationItems.Rows[j].FindControl("txtVat")) as TextBox;
                    TextBox TNetTotal = (gvQuotationItems.Rows[j].FindControl("txtNetTotal")) as TextBox;

                    if (j == 0) {

                        decimal qty = bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).Qty;
                        RequestingQty.Text = qty.ToString();

                        for (int i = 0; i < quotationItems.Count; i++) {
                            SubTotal.Text = quotationItems[0].SubTotal.ToString();
                            Nbt.Text = quotationItems[0].NbtAmount.ToString();
                            Vat.Text = quotationItems[0].VatAmount.ToString();

                            decimal subTotal = decimal.Parse(quotationItems[0].SubTotal.ToString());
                            decimal nbt = decimal.Parse(quotationItems[0].NbtAmount.ToString());
                            decimal vat = decimal.Parse(quotationItems[0].VatAmount.ToString());
                            NetTotal.Text = (subTotal + nbt + vat).ToString();

                            TSubTotal.Text = quotationItems[0].SubTotal.ToString();
                            TNbt.Text = quotationItems[0].NbtAmount.ToString();
                            TVat.Text = quotationItems[0].VatAmount.ToString();
                            TNetTotal.Text = (subTotal + nbt + vat).ToString();
                        }

                        //SubTotal.Text = ViewState["SubTotal"].ToString();
                        //        Nbt.Text = ViewState["NbtAmount"].ToString();
                        //    Vat.Text = ViewState["VatAmount"].ToString();
                        //    NetTotal.Text = ViewState["TotalAmount"].ToString();
                    }
                    else {
                        RequestingQty.Text = "0.00";
                        SubTotal.Text = "0.00";
                        Nbt.Text = "0.00";
                        Vat.Text = "0.00";
                        NetTotal.Text = "0.00";

                        TSubTotal.Text = "0.00";
                        TNbt.Text = "0.00";
                        TVat.Text = "0.00";
                        TNetTotal.Text = "0.00";
                    }

                }

            }

        }


        protected void BtnItemApprove_Click(object sender, EventArgs e) {
            List<TabulationDetail> TabulationDetailsList;
            if (ViewState["SelectedQuotations"] == null) {
                TabulationDetailsList = new List<TabulationDetail>();
            }
            else {
                TabulationDetailsList = new JavaScriptSerializer().Deserialize<List<TabulationDetail>>(ViewState["SelectedQuotations"].ToString());
            }

            TabulationDetail NewTabulationDetail = new TabulationDetail();

            NewTabulationDetail.TotQty = decimal.Parse(hdnQty.Value);
            NewTabulationDetail.VAtAmount = decimal.Parse(hdnVatVal.Value);
            NewTabulationDetail.NbtAmount = decimal.Parse(hdnNbtVal.Value);
            NewTabulationDetail.SubTotal = decimal.Parse(hdnSubTot.Value);
            NewTabulationDetail.NetTotal = decimal.Parse(hdnNetTot.Value);
            NewTabulationDetail.ApprovalRemark = hdnQuotationRemark.Value;
            
            NewTabulationDetail.QuotationId = int.Parse(hdnQuotationId.Value);
            NewTabulationDetail.TabulationId = int.Parse(hdnTabulationId.Value);
            NewTabulationDetail.SupplierId = int.Parse(hdnSupplierId.Value);
            NewTabulationDetail.ItemId = int.Parse(hdnItemId.Value);
            



            TabulationDetailsList.Add(NewTabulationDetail);
            ViewState["SelectedQuotations"] = new JavaScriptSerializer().Serialize(TabulationDetailsList);

            //Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));

            //SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == int.Parse(hdnQuotationId.Value));
            //quotation.IsApproved = 1;
            hdnSelected.Value = "1";
            //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "Calculation(selectElm)", true);
        }


        private void LoadQuotations() {
            try {
                if (PurchaseType.Value == "1") {
                    gvQuotations.Visible = true;
                    TabulationMaster tabulationMaster;
                    PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                    tabulationMaster = prMaster.Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Where(q => q.TabulationId == int.Parse(ViewState["tabulationId"].ToString())).FirstOrDefault();
                    List<TabulationDetail> tabulationDetails = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Find(q => q.TabulationId == int.Parse(ViewState["tabulationId"].ToString())).TabulationDetails.Where(x => (x.TabulationId == int.Parse(ViewState["tabulationId"].ToString()))).OrderBy(x => x.QuotationId).ToList();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Refno");
                    dt.Columns.Add("TabulationId");
                    dt.Columns.Add("QuotationId");
                    dt.Columns.Add("BidId");
                    dt.Columns.Add("SupplierId");
                    dt.Columns.Add("SupplierName");
                    var biddingItems = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).BiddingItems;
                    var Itemcount = 0;
                    foreach (var item in biddingItems) {
                        dt.Columns.Add("Item Description-" + item.ItemName);
                        dt.Columns.Add("Item Code-" + item.ItemName);
                        dt.Columns.Add("Item Price -" + item.ItemName + "-QTY-" + item.Qty.ToString());
                        Itemcount++;

                        ViewState["Itemcount"] = Itemcount;
                    }

                    //dt.Columns.Add("SubTotal");
                    //dt.Columns.Add("NbtAmount");
                    //dt.Columns.Add("VatAmount");
                    //dt.Columns.Add("Total With Tax");



                    int qutaitoid = 0;

                    for (int i = 0; i < tabulationDetails.Count; i++) {

                        if (qutaitoid != tabulationDetails[i].QuotationId) {
                            DataRow newRow = dt.NewRow();
                            newRow["Refno"] = tabulationDetails[i].Refno;
                            newRow["TabulationId"] = tabulationDetails[i].TabulationId.ToString();
                            newRow["QuotationId"] = tabulationDetails[i].QuotationId.ToString();
                            newRow["BidId"] = tabulationMaster.BidId.ToString();
                            newRow["SupplierId"] = tabulationDetails[i].SupplierId.ToString();
                            newRow["SupplierName"] = tabulationDetails[i].SupplierName;
                            foreach (var item in biddingItems) {
                                foreach (var item2 in tabulationDetails) {
                                    if (item.ItemId == item2.ItemId && tabulationDetails[i].QuotationId == item2.QuotationId) {
                                        newRow["Item Description-" + item.ItemName] = item2.Description == null || item2.Description == "" ? "No Description" + " : " : item2.Description;
                                        newRow["Item Code-" + item.ItemName] = item.ItemId.ToString();
                                        newRow["Item Price -" + item.ItemName + "-QTY-" + item.Qty.ToString()] = item2.UnitPrice.ToString("#,##0.00");
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
                    for (int i = gvQuotations.Rows.Count - 1; i > 0; i--) {
                        GridViewRow row = gvQuotations.Rows[i];
                        GridViewRow previousRow = gvQuotations.Rows[i - 1];
                        for (int j = 6; j < 7; j++) {
                            if (row.Cells[j].Text == previousRow.Cells[j].Text || row.Cells[j].Text == "") {
                                if (previousRow.Cells[j].RowSpan == 0) {
                                    if (row.Cells[j].RowSpan == 0) {
                                        previousRow.Cells[j].RowSpan += 2;
                                    }
                                    else {
                                        previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                                    }
                                    row.Cells[j].CssClass = "hidden";
                                }
                            }


                        }
                    }
                }
                else {
                    gvImpotsQuotations.Visible = true;
                    //btnfileUpload.Visible = true;
                    TabulationMaster tabulationMaster;
                    tabulationMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Where(q => q.TabulationId == int.Parse(ViewState["tabulationId"].ToString())).FirstOrDefault();
                    List<TabulationDetail> tabulationDetails = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).Tabulations.Find(q => q.TabulationId == int.Parse(ViewState["tabulationId"].ToString())).TabulationDetails.Where(x => (x.TabulationId == int.Parse(ViewState["tabulationId"].ToString()))).OrderBy(x => x.UnitPrice).ToList();
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
                    foreach (var item in biddingItems) {
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
                    for (int i = 0; i < tabulationDetails.Count; i++) {

                        if (qutaitoid != tabulationDetails[i].QuotationId) {
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
                            foreach (var item in biddingItems) {
                                foreach (var item2 in tabulationDetails) {
                                    if (item.ItemId == item2.ItemId && tabulationDetails[i].QuotationId == item2.QuotationId) {
                                        newRow["Item Description"] = item2.Description == null || item2.Description == "" ? "No Description" + " : " : item2.Description;
                                        newRow["Item Code"] = item.ItemId.ToString();
                                        newRow["Item Price"] = (item2.UnitPrice / item2.ExchangeRate).ToString("#,##0.00");
                                        newRow["Item Price (LKR) -QTY-" + item.Qty.ToString()] = item2.UnitPrice.ToString("#,##0.00");

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

                    gvImpotsQuotations.DataSource = dt;
                    gvImpotsQuotations.DataBind();
                    gvImpotsQuotations.Caption = itemname;
                    for (int i = gvImpotsQuotations.Rows.Count - 1; i > 0; i--) {
                        GridViewRow row = gvImpotsQuotations.Rows[i];
                        GridViewRow previousRow = gvImpotsQuotations.Rows[i - 1];
                        for (int j = 6; j < 7; j++) {
                            if (row.Cells[j].Text == previousRow.Cells[j].Text || row.Cells[j].Text == "") {
                                if (previousRow.Cells[j].RowSpan == 0) {
                                    if (row.Cells[j].RowSpan == 0) {
                                        previousRow.Cells[j].RowSpan += 2;
                                    }
                                    else {
                                        previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                                    }
                                    row.Cells[j].CssClass = "hidden";
                                }
                            }


                        }
                    }
                }

                hdnSelectRemarks.Value = "";
                hdnRejectRemarks.Value = "";


            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void gvQuotations_RowDataBound(object sender, GridViewRowEventArgs e) {


            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;

            if (e.Row.RowType == DataControlRowType.Header) {
                for (int i = 1; i < e.Row.Cells.Count; i++) {
                    e.Row.Cells[i].Width = 125;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow) {


                for (int i = 14; i < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); i += 3) {
                    if (e.Row.Cells[i + 3].Text == "&nbsp;") {
                        e.Row.Cells[i + 3].CssClass = "";
                    }

                }

            }

            for (int i = 14; i < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); i += 3) {

                e.Row.Cells[i + 2].CssClass = "hidden";

            }
        }
        protected void gvImpotsQuotations_RowDataBound(object sender, GridViewRowEventArgs e) {



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



            if (e.Row.RowType == DataControlRowType.DataRow) {
                for (int i = 1; i < e.Row.Cells.Count; i++) {
                    e.Row.Cells[i].Width = Unit.Pixel(200);
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow) {


                for (int i = 23; i < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); i += 4) {
                    if (e.Row.Cells[i + 4].Text == "&nbsp;") {
                        e.Row.Cells[i + 4].CssClass = "";
                    }

                }

                for (int i = 24 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); i < e.Row.Cells.Count - 2; i++) {


                    e.Row.Cells[i].CssClass = "alignright";

                }

                for (int i = 23; i < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); i += 4) {
                    e.Row.Cells[i + 3].CssClass = "alignright";
                }

            }

            for (int i = 23; i < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); i += 4) {
                e.Row.Cells[i + 1].CssClass = "hidden";
                e.Row.Cells[i + 2].CssClass = "hidden";


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
        public static List<SupplierQuotation> GetQuotations(string BidId) {
            return new JavaScriptSerializer().Deserialize<PrMasterV2>(HttpContext.Current.Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(BidId)).SupplierQuotations;
        }

        protected void btnViewAttachments_Click(object sender, EventArgs e) {

            gvDocs.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).SupplierQuotations.Find(q => q.QuotationId == int.Parse(hdnQuotationId.Value)).UploadedFiles;
            gvDocs.DataBind();

            gvImages.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).SupplierQuotations.Find(q => q.QuotationId == int.Parse(hdnQuotationId.Value)).QuotationImages;
            gvImages.DataBind();
            txtTermsAndConditions.Text = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).SupplierQuotations.Find(q => q.QuotationId == int.Parse(hdnQuotationId.Value)).TermsAndCondition;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlAttachments').modal('show') });   </script>", false);
        }

        //protected void btnSelect_Click(object sender, EventArgs e)
        //{
        //    SupplierQuotation quotation = PrMaster.Bids.Find(b => b.BidId == BidId).SupplierQuotations.Find(q => q.QuotationId == int.Parse(hdnQuotationId.Value));
        //    PrMaster.Bids.Find(b => b.BidId == BidId).IsQuotationSelected = 1;
        //    quotation.IsSelected = 1;

        //    if (decimal.Parse(hdnSubTotal.Value) != quotation.SubTotal)
        //    {
        //        GridViewRow quotationRow = gvQuotations.Rows[0];

        //        quotation.SubTotal = decimal.Parse(hdnSubTotal.Value);
        //        quotation.NbtAmount = decimal.Parse(hdnNbtTotal.Value);
        //        quotation.VatAmount = decimal.Parse(hdnVatTotal.Value);
        //        quotation.NetTotal = decimal.Parse(hdnNetTotal.Value);

        //        GridView gvQuotationItems = quotationRow.FindControl("gvQuotationItems") as GridView;

        //        for (int i = 0; i < gvQuotationItems.Rows.Count; i++)
        //        {
        //            SupplierQuotationItem quotationItem = quotation.QuotationItems.Find(qi => qi.QuotationItemId == int.Parse(gvQuotationItems.Rows[i].Cells[0].Text));
        //            quotationItem.Qty = int.Parse((gvQuotationItems.Rows[i].FindControl("txtQty") as TextBox).Text);
        //            quotationItem.UnitPrice = decimal.Parse((gvQuotationItems.Rows[i].FindControl("txtNegotiatePrice") as TextBox).Text);
        //            quotationItem.SubTotal = decimal.Parse((gvQuotationItems.Rows[i].FindControl("txtSubTotal") as TextBox).Text);
        //            quotationItem.NbtAmount = decimal.Parse((gvQuotationItems.Rows[i].FindControl("txtNbt") as TextBox).Text);
        //            quotationItem.VatAmount = decimal.Parse((gvQuotationItems.Rows[i].FindControl("txtVat") as TextBox).Text);
        //            quotationItem.TotalAmount = decimal.Parse((gvQuotationItems.Rows[i].FindControl("txtNetTotal") as TextBox).Text);
        //        }

        //        int result = quotationController.UpdateSupplierQuotation(quotation);

        //        if (result > 0)
        //        {
        //            result = quotationController.SelectSupplierQuotationAtSelection(int.Parse(hdnQuotationId.Value), hdnSelectRemarks.Value, BidId, UserId);



        //            if (result > 0)
        //            {
        //                LoadGV();
        //                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);

        //            }
        //            else
        //            {
        //                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on selecting quotation'}); });   </script>", false);
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on updating quotation'}); });   </script>", false);
        //        }

        //    }
        //    else
        //    {
        //        int result = quotationController.SelectSupplierQuotationAtSelection(int.Parse(hdnQuotationId.Value), hdnSelectRemarks.Value, BidId, UserId);

        //        tabulationMasterController.PopulateRecommendation(int.Parse(hdnTabulationId.Value), PrMaster.PrCategoryId, decimal.Parse(hdnSubTotal.Value), UserId, DesignationId, hdnSelectRemarks.Value);

        //        if (result > 0)
        //        {
        //            LoadGV();
        //            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);

        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on selecting quotation'}); });   </script>", false);
        //        }
        //    }


        //}


        //protected void btnReject_Click(object sender, EventArgs e)
        //{
        //    PrMaster.Bids.Find(b => b.BidId == BidId).SupplierQuotations.Find(q => q.QuotationId == int.Parse(hdnQuotationId.Value)).IsSelected=2;

        //    int result = quotationController.RejectSupplierQuotationAtSelection(int.Parse(hdnQuotationId.Value), hdnRejectRemarks.Value, UserId);

        //    if (result > 0)
        //    {
        //        LoadGV();
        //        LoadQuotations();
        //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { $('#mdlQuotations').modal('show') });; });   </script>", false);

        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on rejecting quotation'}); });   </script>", false);
        //    }
        //}

        //protected void btnReset_Click(object sender, EventArgs e)
        //{
        //    BidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);

        //    Bidding bid = PrMaster.Bids.Find(b => b.BidId == BidId);

        //    bid.IsQuotationSelected = 0;
        //    bid.IsQuotationApproved = 0;
        //    bid.IsQuotationConfirmed = 0;
        //    bid.QuotationApprovalRemarks = null;
        //    bid.QuotationConfirmationRemarks = null;

        //    PrMaster.Bids.Find(b => b.BidId == BidId).SupplierQuotations.ForEach(q => { q.IsSelected = 0; q.SelectionRemarks = ""; });

        //    int result = quotationController.ResetSelections(BidId);

        //    if (result > 0)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
        //        LoadGV();
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on resetting selection'}); });   </script>", false);
        //    }

        //}

        //public override void VerifyRenderingInServerForm(System.Web.UI.Control control) {
        //    //required to avoid the run time error 'Grid View' must be placed inside a form tag with runat=server."  
        //}

        protected void btnreprint_Click(object sender, EventArgs e) {
            ReprintExcel(gvrjectedTabulationsheet, "Tabulation Sheet-Reprint", "Tabulation Sheet-Reprint " + LocalTime.Now.ToString("yyyy/MMM/dd"));
        }

        protected void printExcel(GridView gridview, string filename, string header) {
            try {
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


                for (int i = 0; i < gridview.Rows.Count; i++) {
                    gridview.Rows[i].Cells[6].RowSpan = 0;
                }

                if (PurchaseType.Value == "1") {

                    gridview.HeaderRow.Cells[7].Visible = false;
                    gridview.HeaderRow.Cells[8].Visible = false;
                    for (int i = 0; i < gridview.Rows.Count; i++) {
                        gridview.Rows[i].Cells[2].Visible = false;
                        gridview.Rows[i].Cells[3].Visible = false;
                        gridview.Rows[i].Cells[4].Visible = false;
                        gridview.Rows[i].Cells[5].Visible = false;

                        gridview.Rows[i].Cells[7].Visible = false;
                        gridview.Rows[i].Cells[8].Visible = false;



                        if (hdnSlectedQutations.Value != "") {
                            string[] SelectedArray = hdnSlectedQutations.Value.ToString().Split(',');
                            for (int k = 0; k < SelectedArray.Length; k += 6) {
                                for (int j = 14; j < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); j += 3) {
                                    if (gridview.Rows[i].Cells[3].Text == SelectedArray[k + 1] && gridview.Rows[i].Cells[j + 2].Text == SelectedArray[k + 4]) {

                                        gridview.Rows[i].Cells[j + 3].BackColor = Color.Green;
                                        var txt = gridview.Rows[i].Cells[j + 3].Text;
                                        gridview.Rows[i].Cells[j + 3].Text = txt + "- QTY(" + SelectedArray[k + 5] + ")";
                                    }

                                    gridview.Rows[i].Cells[j + 3].HorizontalAlign = HorizontalAlign.Right;
                                }




                            }


                        }



                        for (int k = 14; k < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); k += 3) {

                            gridview.Rows[i].Cells[k + 2].Visible = false;
                        }




                    }


                    for (int k = 14; k < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); k += 3) {


                        gridview.HeaderRow.Cells[k + 2].Visible = false;


                    }


                    for (int i = gridview.Rows.Count - 1; i > 0; i--) {
                        GridViewRow row = gridview.Rows[i];
                        GridViewRow previousRow = gridview.Rows[i - 1];
                        for (int j = 6; j < 7; j++) {
                            if (row.Cells[j].Text == previousRow.Cells[j].Text || row.Cells[j].Text == "") {
                                if (previousRow.Cells[j].RowSpan == 0) {
                                    if (row.Cells[j].RowSpan == 0) {
                                        previousRow.Cells[j].RowSpan += 2;
                                    }
                                    else {
                                        previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                                    }
                                    row.Cells[j].Visible = false;
                                }
                            }


                        }


                    }
                }
                else {

                    gridview.HeaderRow.Cells[11].Visible = false;
                    gridview.HeaderRow.Cells[12].Visible = false;
                    for (int i = 0; i < gridview.Rows.Count; i++) {
                        gridview.Rows[i].Cells[2].Visible = false;
                        gridview.Rows[i].Cells[3].Visible = false;
                        gridview.Rows[i].Cells[4].Visible = false;
                        gridview.Rows[i].Cells[5].Visible = false;

                        gridview.Rows[i].Cells[11].Visible = false;
                        gridview.Rows[i].Cells[12].Visible = false;

                        gridview.Rows[i].Cells[i].Font.Bold = true;


                        if (hdnSlectedQutations.Value != "") {
                            string[] SelectedArray = hdnSlectedQutations.Value.ToString().Split(',');
                            for (int k = 0; k < SelectedArray.Length; k += 6) {
                                for (int j = 23; j < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); j += 4) {
                                    if (gridview.Rows[i].Cells[3].Text == SelectedArray[k + 1] && gridview.Rows[i].Cells[j + 2].Text == SelectedArray[k + 4]) {

                                        gridview.Rows[i].Cells[j + 4].BackColor = Color.Green;
                                        var txt = gridview.Rows[i].Cells[j + 4].Text;
                                        gridview.Rows[i].Cells[j + 4].Text = txt + "- QTY(" + SelectedArray[k + 5] + ")";
                                    }

                                    gridview.Rows[i].Cells[j + 4].HorizontalAlign = HorizontalAlign.Right;
                                }



                            }


                        }



                        for (int k = 23; k < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); k += 4) {

                            gridview.Rows[i].Cells[k + 2].Visible = false;
                            gridview.Rows[i].Cells[k + 1].Visible = false;
                            gridview.Rows[i].Cells[k + 3].HorizontalAlign = HorizontalAlign.Right;
                        }


                        for (int k = 24 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); k < gridview.Rows[i].Cells.Count; k++) {


                            gridview.HeaderRow.Cells[k].HorizontalAlign = HorizontalAlign.Right;


                        }

                    }


                    for (int k = 23; k < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); k += 4) {

                        gridview.HeaderRow.Cells[k + 1].Visible = false;
                        gridview.HeaderRow.Cells[k + 2].Visible = false;


                    }




                    for (int i = gridview.Rows.Count - 1; i > 0; i--) {
                        GridViewRow row = gridview.Rows[i];
                        GridViewRow previousRow = gridview.Rows[i - 1];
                        for (int j = 6; j < 7; j++) {
                            if (row.Cells[j].Text == previousRow.Cells[j].Text || row.Cells[j].Text == "") {
                                if (previousRow.Cells[j].RowSpan == 0) {
                                    if (row.Cells[j].RowSpan == 0) {
                                        previousRow.Cells[j].RowSpan += 2;
                                    }
                                    else {
                                        previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                                    }
                                    row.Cells[j].Visible = false;
                                }
                            }


                        }


                    }
                }
                gridview.HeaderStyle.Font.Bold = true;
                gridview.RenderControl(htmltextwrtter);
                Response.Write(strwritter.ToString());
                Response.End();
            }



            catch (Exception ex) {

            }
        }

        protected void btnPrint_Click(object sender, EventArgs e) {
            if (PurchaseType.Value == "1") {
                printExcel(gvItems, "Tabulation Sheet", "Tabulation Sheet " + LocalTime.Now.ToString("yyyy/MMM/dd"));
            }
            else {
                printExcel(gvImpotsQuotations, "Tabulation Sheet", "Tabulation Sheet " + LocalTime.Now.ToString("yyyy/MMM/dd"));
            }
        }

        protected void btnPrint1_Click(object sender, EventArgs e) {

            GridView gv = new GridView();
            gv.AutoGenerateColumns = false;


            gv.Columns.Add(new BoundField() { DataField = "ItemName", HeaderText = "Item Name" });
            gv.Columns.Add(new BoundField() { DataField = "Supplier", HeaderText = "Supplier" });
            gv.Columns.Add(new BoundField() { DataField = "Refno", HeaderText = "Reference No" });
            gv.Columns.Add(new BoundField() { DataField = "Description", HeaderText = "Description" });
            gv.Columns.Add(new BoundField() { DataField = "Quantity", HeaderText = "Quantity" });
            gv.Columns.Add(new BoundField() { DataField = "UnitPrice", HeaderText = "Unit Price" });
            gv.Columns.Add(new BoundField() { DataField = "SubTotal", HeaderText = "SubTotal" });
            gv.Columns.Add(new BoundField() { DataField = "NbtAmount", HeaderText = "NBT" });
            gv.Columns.Add(new BoundField() { DataField = "VatAmount", HeaderText = "VAT" });
            gv.Columns.Add(new BoundField() { DataField = "NetTotal", HeaderText = "NetTotal" });
            ////gv.Columns.Add(new BoundField() { DataField = "IsSelected", HeaderText = "Action Status" });

            DataTable dt = new DataTable();

            dt.Columns.Add("ItemName");
            dt.Columns.Add("Supplier");
            dt.Columns.Add("Refno");
            dt.Columns.Add("Description");
            dt.Columns.Add("Quantity", typeof(string));
            dt.Columns.Add("UnitPrice", typeof(string));
            dt.Columns.Add("SubTotal", typeof(string));
            dt.Columns.Add("NbtAmount", typeof(string));
            dt.Columns.Add("VatAmount", typeof(string));
            dt.Columns.Add("NetTotal", typeof(string));
            ////dt.Columns.Add("IsSelected");

            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));

            List<SupplierQuotationItem> Items = new List<SupplierQuotationItem>();

            bid.SupplierQuotations.ForEach(sq => Items.AddRange(sq.QuotationItems));

            Items = Items.OrderBy(qi => qi.ItemId).ToList();

            List<int> ItemIds = Items.Select(qi => qi.ItemId).Distinct().OrderBy(i => i).ToList();

            for (int i = 0; i < ItemIds.Count; i++) {
                List<SupplierQuotationItem> quotationItems = Items.Where(item => item.ItemId == ItemIds[i]).OrderBy(item => item.SubTotal).ToList();

                for (int j = 0; j < quotationItems.Count; j++) {
                    SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == quotationItems[j].QuotationId);

                    DataRow newRow = dt.NewRow();

                    newRow["ItemName"] = quotationItems[j].ItemName;
                    newRow["Supplier"] = quotation.SupplierName;
                    newRow["Refno"] = quotation.QuotationReferenceCode;
                    newRow["Description"] = quotationItems[j].Description;
                    newRow["Quantity"] = String.Format("{0:0,0.00}", quotationItems[j].Qty);
                    newRow["UnitPrice"] = String.Format("{0:0,0.00}", quotationItems[j].UnitPrice);
                    newRow["SubTotal"] = String.Format("{0:0,0.00}", quotationItems[j].SubTotal);
                    newRow["NbtAmount"] = String.Format("{0:0,0.00}", quotationItems[j].NbtAmount);
                    newRow["VatAmount"] = String.Format("{0:0,0.00}", quotationItems[j].VatAmount);
                    newRow["NetTotal"] = String.Format("{0:0,0.00}", quotationItems[j].TotalAmount);
                    ////newRow["IsSelected"] = quotationItems[j].IsSelected == 0 ? "No Action Taken" : quotationItems[j].IsSelected == 1 ? "Selected" : "Rejected";

                    dt.Rows.Add(newRow);
                }
            }

            gv.DataSource = dt;
            gv.DataBind();

            GridViewRow FirstRow = gv.Rows[0];

            for (int i = 1; i < gv.Rows.Count; i++) {

                GridViewRow currentRow = gv.Rows[i];
                GridViewRow previousRow = gv.Rows[i - 1];

                if (currentRow.Cells[0].Text == previousRow.Cells[0].Text) {
                    if (FirstRow.Cells[0].RowSpan == 0)
                        FirstRow.Cells[0].RowSpan += 2;
                    else
                        FirstRow.Cells[0].RowSpan += 1;
                    currentRow.Cells[0].Visible = false;
                }
                else {
                    FirstRow = gv.Rows[i];
                }

            }

            Response.Clear();
            Response.Buffer = true;
            string FileName = "B" + bid.BidCode + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
            string headerTable = @"<Table><tr><td  colspan='10'><center><h4>Quotation Tabulation For Bid No: B" + bid.BidCode + " </h4></center> </td></tr></Table> <hr>";
            Response.Write(headerTable);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter()) {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                
                gv.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gv.HeaderRow.Cells) {
                    cell.BackColor = gv.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gv.Rows) {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells) {
                        if (row.RowIndex % 2 == 0) {
                            cell.BackColor = gv.AlternatingRowStyle.BackColor;
                        }
                        else {
                            cell.BackColor = gv.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gv.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        
        //protected void btnPrint11_Click(object sender, EventArgs e) {
        //    try {
        //        GridView gv = new GridView();
        //        gv.AutoGenerateColumns = false;


        //        gv.Columns.Add(new BoundField() { DataField = "ItemName", HeaderText = "Item Name" });
        //        gv.Columns.Add(new BoundField() { DataField = "Supplier", HeaderText = "Supplier" });
        //        gv.Columns.Add(new BoundField() { DataField = "Description", HeaderText = "Description" });
        //        //gv.Columns.Add(new BoundField() { DataField = "UnitPrice", HeaderText = "Unit Price" });
        //        //gv.Columns.Add(new BoundField() { DataField = "SubTotal", HeaderText = "SubTotal" });
        //        //gv.Columns.Add(new BoundField() { DataField = "NbtAmount", HeaderText = "NBT" });
        //        //gv.Columns.Add(new BoundField() { DataField = "VatAmount", HeaderText = "VAT" });
        //        //gv.Columns.Add(new BoundField() { DataField = "NetTotal", HeaderText = "NetTotal" });
        //        ////gv.Columns.Add(new BoundField() { DataField = "IsSelected", HeaderText = "Action Status" });

        //        DataTable dt = new DataTable();

        //        dt.Columns.Add("ItemName");
        //        dt.Columns.Add("Supplier");
        //        dt.Columns.Add("Description");
        //        //dt.Columns.Add("UnitPrice", typeof(string));
        //        //dt.Columns.Add("SubTotal", typeof(string));
        //        //dt.Columns.Add("NbtAmount", typeof(string));
        //        //dt.Columns.Add("VatAmount", typeof(string));
        //        //dt.Columns.Add("NetTotal", typeof(string));
        //        ////dt.Columns.Add("IsSelected");

        //        Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));

        //        List<SupplierQuotationItem> Items = new List<SupplierQuotationItem>();

        //        bid.SupplierQuotations.ForEach(sq => Items.AddRange(sq.QuotationItems));

        //        Items = Items.OrderBy(qi => qi.ItemId).ToList();

        //        List<int> ItemIds = Items.Select(qi => qi.ItemId).Distinct().OrderBy(i => i).ToList();

        //        for (int i = 0; i < ItemIds.Count; i++) {
        //            List<SupplierQuotationItem> quotationItems = Items.Where(item => item.ItemId == ItemIds[i]).OrderBy(item => item.SubTotal).ToList();

        //            for (int j = 0; j < quotationItems.Count; j++) {
        //                SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == quotationItems[j].QuotationId);

        //                DataRow newRow = dt.NewRow();

        //                newRow["ItemName"] = quotationItems[j].ItemName;
        //                newRow["Supplier"] = quotation.SupplierName;
        //                newRow["Description"] = quotationItems[j].Description;
        //                //newRow["UnitPrice"] = String.Format("{0:0,0.00}", quotationItems[j].UnitPrice);
        //                //newRow["SubTotal"] = String.Format("{0:0,0.00}", quotationItems[j].SubTotal);
        //                //newRow["NbtAmount"] = String.Format("{0:0,0.00}", quotationItems[j].NbtAmount);
        //                //newRow["VatAmount"] = String.Format("{0:0,0.00}", quotationItems[j].VatAmount);
        //                //newRow["NetTotal"] = String.Format("{0:0,0.00}", quotationItems[j].TotalAmount);
        //                ////newRow["IsSelected"] = quotationItems[j].IsSelected == 0 ? "No Action Taken" : quotationItems[j].IsSelected == 1 ? "Selected" : "Rejected";

        //                dt.Rows.Add(newRow);
        //            }
        //        }

        //        gv.DataSource = dt;
        //        gv.DataBind();

        //        GridViewRow FirstRow = gv.Rows[0];

        //        for (int i = 1; i < gv.Rows.Count; i++) {

        //            GridViewRow currentRow = gv.Rows[i];
        //            GridViewRow previousRow = gv.Rows[i - 1];

        //            if (currentRow.Cells[0].Text == previousRow.Cells[0].Text) {
        //                if (FirstRow.Cells[0].RowSpan == 0)
        //                    FirstRow.Cells[0].RowSpan += 2;
        //                else
        //                    FirstRow.Cells[0].RowSpan += 1;
        //                currentRow.Cells[0].Visible = false;
        //            }
        //            else {
        //                FirstRow = gv.Rows[i];
        //            }
        //        }

        //        //Response.ClearContent();
        //        //Response.AddHeader("Content-Disposition", "attachment;filename= Name");
        //        //Response.ContentType = "application/vnd.ms-excel";

        //        //StringWriter strwritter1 = new StringWriter();
        //        //HtmlTextWriter htmltextwrtter1 = new HtmlTextWriter(strwritter1);
        //        //gv.RenderControl(htmltextwrtter1);
        //        //Response.Write(strwritter1.ToString());
        //        //HttpContext.Current.ApplicationInstance.CompleteRequest();

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.ClearContent();
        //        Response.ClearHeaders();
        //        Response.Charset = "";
        //        string FileName = "B" + bid.BidCode + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
        //        StringWriter strwritter1 = new StringWriter();
        //        HtmlTextWriter htmltextwrtter1 = new HtmlTextWriter(strwritter1);
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        string headerTable = @"<Table><tr><td  colspan='4'><center><h4>Quotation Tabulation For Bid No: B" + bid.BidCode + " </h4></center> </td></tr></Table> <hr>";
        //        Response.Write(headerTable);
        //        Response.ContentType = "application/vnd.ms-excel";
        //        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        //        gv.GridLines = GridLines.Both;
        //        gv.HeaderStyle.Font.Bold = true;
        //        gv.RenderControl(htmltextwrtter1);
        //        Response.Write(strwritter1.ToString());
        //        HttpContext.Current.ApplicationInstance.CompleteRequest();
        //    }
        //    catch (Exception ex) {
        //        throw ex;
        //    }

        //}


        protected void btnsupplerview_Click(object sender, EventArgs e) {
            var SupplierId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[4].Text);
            Response.Redirect("CompanyUpdatingAndRatingSupplier.aspx?ID=" + SupplierId);
        }

        protected void gvQuotations_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                for (int i = 14; i < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); i += 3) {

                    e.Row.Cells[i + 3].CssClass = "CellClick";


                }

            }
        }
        protected void gvImpotsQuotations_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                for (int i = 23; i < 23 + (int.Parse(ViewState["Itemcount"].ToString()) * 4); i += 4) {

                    e.Row.Cells[i + 4].CssClass = "CellClick";


                }

            }
        }
        protected void btnclear_Click(object sender, EventArgs e) {
            hdnSlectedQutations.Value = "";
            txtareaRemark.Text = "";
            LoadQuotations();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('div').removeClass('modal-backdrop'); $('#mdlQuotations').modal('show'); });   </script>", false);
        }

        protected void btnfinish_Click(object sender, EventArgs e) {
            try {

                if (hdnSlectedQutations.Value != "" && hdnSlectedQutations != null) {

                    string[] SelectedArray = hdnSlectedQutations.Value.ToString().Split(',');
                    for (int i = 0; i < SelectedArray.Length; i += 6) {
                        tabulationDetailController.UpdateTabulationDetails(int.Parse(SelectedArray[i]), int.Parse(SelectedArray[i + 1]), int.Parse(SelectedArray[i + 2]), int.Parse(SelectedArray[i + 3]), int.Parse(SelectedArray[i + 4]), int.Parse(SelectedArray[i + 5]));
                    }
                    //Check whether Approval Limit Exist
                    bool approvalLimitExist = tabulationMasterController.CheckApprovalLimitExist(int.Parse(ViewState["tabulationId"].ToString()), new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCategoryId);
                    if (approvalLimitExist) {
                        var IsUpdated = tabulationMasterController.UpdateTabulationMasterNetTotal(int.Parse(ViewState["tabulationId"].ToString()), int.Parse(ViewState["PrId"].ToString()), int.Parse(ViewState["BidId"].ToString()), int.Parse(Session["UserId"].ToString()), txtareaRemark.Text);
                        if (IsUpdated > 0) {
                            hdnSlectedQutations.Value = "";
                            var tabmaseter = tabulationMasterController.GetTabulationsByBidId(int.Parse(ViewState["BidId"].ToString()));

                            var Ispoulated = tabulationMasterController.PopulateRecommendation(int.Parse(ViewState["tabulationId"].ToString()), new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCategoryId, tabmaseter.Where(x => x.TabulationId == int.Parse(ViewState["tabulationId"].ToString())).FirstOrDefault().SubTotal, int.Parse(Session["UserId"].ToString()), int.Parse(Session["DesignationId"].ToString()), txtareaRemark.Text, 1);

                            if (Ispoulated > 0) {
                                LoadGV();
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location.href = 'ViewPrForQuotationComparison.aspx'; });  });   </script>", false);

                            }
                        }

                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Approval Limits Not Found ! . Please Add Approval Limits Both for Recommendation & Approval For Item Category'})' });   </script>", false);
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please Select'}).then((result) => { $('#mdlQuotations').modal('show') }); });   </script>", false);
                }


            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void btnRejectedView_Click(object sender, EventArgs e) {
            var TabulationID = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[0].Text);
            var rejectedBidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
            ViewState["ReprintBidCode"] = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == rejectedBidId).BidCode.ToString();
            ViewState["ReprintPrCode"] = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCode;
            var tabulationMaster = tabulationMasterController.GetTabulationsByTabulationId(TabulationID);

            ViewState["tabulationMaster"] = new JavaScriptSerializer().Serialize(tabulationMaster);

            List<TabulationDetail> tabulationDetails = tabulationDetailController.GetTabulationDetailsByTabulationId(TabulationID);

            if (PurchaseType.Value == "1") {
                DataTable dt = new DataTable();
                dt.Columns.Add("TabulationId");
                dt.Columns.Add("QuotationId");
                dt.Columns.Add("BidId");
                dt.Columns.Add("SupplierId");
                dt.Columns.Add("SupplierName");
                var biddingItems = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == rejectedBidId).BiddingItems;
                ViewState["BidId"] = rejectedBidId;
                var reprintItemcount = 0;
                foreach (var item in biddingItems) {
                    dt.Columns.Add("Item Description-" + item.ItemName);
                    dt.Columns.Add("Item Code-" + item.ItemName);
                    dt.Columns.Add("Item Price -" + item.ItemName + "-QTY-" + item.Qty.ToString());
                    reprintItemcount++;

                    ViewState["reprintItemcount"] = reprintItemcount;
                }

                int qutaitoid = 0;

                for (int i = 0; i < tabulationDetails.Count; i++) {

                    if (qutaitoid != tabulationDetails[i].QuotationId) {
                        DataRow newRow = dt.NewRow();

                        newRow["TabulationId"] = tabulationDetails[i].TabulationId.ToString();
                        newRow["QuotationId"] = tabulationDetails[i].QuotationId.ToString();
                        newRow["BidId"] = tabulationMaster.BidId.ToString();
                        newRow["SupplierId"] = tabulationDetails[i].SupplierId.ToString();
                        newRow["SupplierName"] = tabulationDetails[i].SupplierName;
                        foreach (var item in biddingItems) {
                            foreach (var item2 in tabulationDetails) {
                                if (item.ItemId == item2.ItemId && tabulationDetails[i].QuotationId == item2.QuotationId) {
                                    newRow["Item Description-" + item.ItemName] = item2.Description == null || item2.Description == "" ? "No Description" + " : " : item2.Description;
                                    newRow["Item Code-" + item.ItemName] = item.ItemId.ToString();
                                    if (item2.IsSelected == 1) {

                                        newRow["Item Price -" + item.ItemName + "-QTY-" + item.Qty.ToString()] = item2.UnitPrice.ToString("#,##0.00") + "*" + item2.Qty.ToString();
                                    }
                                    else {
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

            else {
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
                foreach (var item in biddingItems) {
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
                for (int i = 0; i < tabulationDetails.Count; i++) {

                    if (qutaitoid != tabulationDetails[i].QuotationId) {
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
                        foreach (var item in biddingItems) {
                            foreach (var item2 in tabulationDetails) {
                                if (item.ItemId == item2.ItemId && tabulationDetails[i].QuotationId == item2.QuotationId) {

                                    newRow["Item Description"] = item2.Description == null || item2.Description == "" ? "No Description" + " : " : item2.Description;
                                    newRow["Item Code"] = item.ItemId.ToString();
                                    newRow["Item Price"] = (item2.UnitPrice / item2.ExchangeRate).ToString("#,##0.00");
                                    if (item2.IsSelected == 1) {
                                        newRow["Item Price (LKR) -QTY-" + item.Qty.ToString()] = item2.UnitPrice.ToString("#,##0.00") + "*" + item2.Qty.ToString();
                                    }
                                    else {
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




            for (int i = gvrjectedTabulationsheet.Rows.Count - 1; i > 0; i--) {
                GridViewRow row = gvrjectedTabulationsheet.Rows[i];
                GridViewRow previousRow = gvrjectedTabulationsheet.Rows[i - 1];
                for (int j = 5; j < 6; j++) {
                    if (row.Cells[j].Text == previousRow.Cells[j].Text || row.Cells[j].Text == "") {
                        if (previousRow.Cells[j].RowSpan == 0) {
                            if (row.Cells[j].RowSpan == 0) {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].CssClass = "hidden";
                        }
                    }


                }
            }

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlRejectedTabulations').modal('show') });   </script>", false);

        }

        protected void gvrjectedTabulationsheet_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (PurchaseType.Value == "1") {
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;


                if (e.Row.RowType == DataControlRowType.DataRow) {

                    for (int i = 14; i < 14 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 3); i += 3) {
                        if (e.Row.Cells[i + 3].Text == "&nbsp;") {
                            e.Row.Cells[i + 3].CssClass = "";
                        }
                        else if (e.Row.Cells[i + 3].Text.Split('*').Length > 1) {
                            e.Row.Cells[i + 3].CssClass = "greenBg";
                        }
                        else {
                            e.Row.Cells[i + 3].CssClass = "";
                        }

                        e.Row.Cells[i + 3].HorizontalAlign = HorizontalAlign.Right;

                    }

                }
                if (e.Row.RowType == DataControlRowType.Footer) {

                    e.Row.Cells[0].Text = "Total Without Tax: <br/> Nbt Amount: <br/> Vat Amount: <br/> Total With Tax: ";
                    e.Row.Cells[6].Text = new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString()).SubTotal.ToString("#,##0.00") + "<br/>" + new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString()).NbtAmount.ToString("#,##0.00") + "<br/>" + new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString()).VatAmount.ToString("#,##0.00") + "<br/>" + new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString()).NetTotal.ToString("#,##0.00");

                    e.Row.CssClass = "footer-font";
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                }
                for (int i = 14; i < 14 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 3); i += 3) {

                    e.Row.Cells[i + 2].CssClass = "hidden";

                }


            }
            else {

                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;


                if (e.Row.RowType == DataControlRowType.DataRow) {

                    for (int i = 19; i < 19 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); i += 4) {
                        if (e.Row.Cells[i + 4].Text == "&nbsp;") {
                            e.Row.Cells[i + 4].CssClass = "";
                        }
                        else if (e.Row.Cells[i + 4].Text.Split('*').Length > 1) {
                            e.Row.Cells[i + 4].CssClass = "greenBg";
                        }
                        else {
                            e.Row.Cells[i + 4].CssClass = "";
                        }

                        e.Row.Cells[i + 4].HorizontalAlign = HorizontalAlign.Right;

                    }

                    for (int i = 20 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); i < e.Row.Cells.Count - 2; i++) {
                        e.Row.Cells[i].CssClass = "alignright";

                    }
                    for (int i = 1; i < e.Row.Cells.Count; i++) {
                        e.Row.Cells[i].Font.Bold = true;
                    }

                }
                if (e.Row.RowType == DataControlRowType.Footer) {

                    e.Row.Cells[0].Text = "Total Without Tax: <br/> Nbt Amount: <br/> Vat Amount: <br/> Total With Tax: ";
                    e.Row.Cells[6].Text = new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString()).SubTotal.ToString("#,##0.00") + "<br/>" + new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString()).NbtAmount.ToString("#,##0.00") + "<br/>" + new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString()).VatAmount.ToString("#,##0.00") + "<br/>" + new JavaScriptSerializer().Deserialize<TabulationMaster>(ViewState["tabulationMaster"].ToString()).NetTotal.ToString("#,##0.00");

                    e.Row.CssClass = "footer-font";
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                }
                for (int i = 19; i < 19 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); i += 4) {
                    e.Row.Cells[i + 1].CssClass = "hidden";
                    e.Row.Cells[i + 2].CssClass = "hidden";

                }
            }

        }


        protected void ReprintExcel(GridView gridview, string filename, string header) {
            try {
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
                gridview.HeaderRow.Cells[8].Visible = false;
                gridview.HeaderRow.Cells[9].Visible = false;

                gridview.FooterRow.Cells[2].Visible = false;
                gridview.FooterRow.Cells[3].Visible = false;
                gridview.FooterRow.Cells[4].Visible = false;
                gridview.FooterRow.Cells[5].Visible = false;
                gridview.FooterRow.Cells[7].Visible = false;
                gridview.FooterRow.Cells[8].Visible = false;



                for (int i = 0; i < gridview.Rows.Count; i++) {
                    gridview.Rows[i].Cells[6].RowSpan = 0;
                }

                if (PurchaseType.Value == "1") {


                    for (int i = 0; i < gridview.Rows.Count; i++) {
                        gridview.Rows[i].Cells[1].Visible = false;
                        gridview.Rows[i].Cells[2].Visible = false;
                        gridview.Rows[i].Cells[3].Visible = false;
                        gridview.Rows[i].Cells[4].Visible = false;

                        gridview.Rows[i].Cells[6].Visible = false;
                        gridview.Rows[i].Cells[7].Visible = false;



                        for (int j = 14; j < 14 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 3); j += 3) {
                            if (gridview.Rows[i].Cells[j + 3].Text.Split('*').Length > 1) {
                                gridview.Rows[i].Cells[j + 3].BackColor = Color.Green;
                                var txt = gridview.Rows[i].Cells[j + 3].Text;
                                gridview.Rows[i].Cells[j + 3].Text = txt.Split('*')[0] + "- QTY(" + txt.Split('*')[1] + ")";

                            }

                        }


                        for (int k = 14; k < 14 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 3); k += 3) {

                            gridview.Rows[i].Cells[k + 2].Visible = false;
                        }

                    }

                    for (int k = 14; k < 14 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 3); k += 3) {


                        gridview.HeaderRow.Cells[k + 2].Visible = false;


                    }

                    for (int k = 14; k < 14 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 3); k += 3) {


                        gridview.FooterRow.Cells[k + 2].Visible = false;


                    }




                }
                else {
                    for (int i = 0; i < gridview.Rows.Count; i++) {
                        gridview.Rows[i].Cells[2].Visible = false;
                        gridview.Rows[i].Cells[3].Visible = false;
                        gridview.Rows[i].Cells[4].Visible = false;
                        gridview.Rows[i].Cells[5].Visible = false;

                        gridview.Rows[i].Cells[7].Visible = false;
                        gridview.Rows[i].Cells[8].Visible = false;



                        for (int j = 19; j < 19 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); j += 4) {
                            if (gridview.Rows[i].Cells[j + 4].Text.Split('*').Length > 1) {
                                gridview.Rows[i].Cells[j + 4].BackColor = Color.Green;
                                var txt = gridview.Rows[i].Cells[j + 4].Text;
                                gridview.Rows[i].Cells[j + 4].Text = txt.Split('*')[0] + "- QTY(" + txt.Split('*')[1] + ")";

                            }

                        }


                        for (int k = 19; k < 19 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); k += 4) {
                            gridview.Rows[i].Cells[k + 1].Visible = false;
                            gridview.Rows[i].Cells[k + 2].Visible = false;
                        }

                    }

                    for (int k = 19; k < 19 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); k += 4) {

                        gridview.HeaderRow.Cells[k + 1].Visible = false;
                        gridview.HeaderRow.Cells[k + 2].Visible = false;


                    }

                    for (int k = 19; k < 19 + (int.Parse(ViewState["reprintItemcount"].ToString()) * 4); k += 4) {

                        gridview.FooterRow.Cells[k + 1].Visible = false;
                        gridview.FooterRow.Cells[k + 2].Visible = false;


                    }
                }

                for (int i = gridview.Rows.Count - 1; i > 0; i--) {
                    GridViewRow row = gridview.Rows[i];
                    GridViewRow previousRow = gridview.Rows[i - 1];
                    for (int j = 6; j < 7; j++) {
                        if (row.Cells[j].Text == previousRow.Cells[j].Text || row.Cells[j].Text == "") {
                            if (previousRow.Cells[j].RowSpan == 0) {
                                if (row.Cells[j].RowSpan == 0) {
                                    previousRow.Cells[j].RowSpan += 2;
                                }
                                else {
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

            catch (Exception ex) {
                throw ex;
            }
        }

        protected void btnRecDocs_Click(object sender, EventArgs e) {
            try {
                var TabulationID = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[0].Text);
                var rejectedBidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
                List<TecCommitteeFileUpload> bidingplandoc = committeeController.Gettechcommitteefiles(rejectedBidId, TabulationID, "T");
                gvViewdocsrejected.DataSource = bidingplandoc;
                gvViewdocsrejected.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlviewdocs').modal('show');});   </script>", false);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        protected void btnProcDoc_Click(object sender, EventArgs e) {
            try {

                var TabulationID = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[0].Text);
                var rejectedBidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
                List<TecCommitteeFileUpload> bidingplandoc = committeeController.Gettechcommitteefiles(rejectedBidId, TabulationID, "P");
                gvViewdocsrejected.DataSource = bidingplandoc;
                gvViewdocsrejected.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlviewdocs').modal('show');});   </script>", false);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        protected void lbtnDownload_Click(object sender, EventArgs e) {
            try {


                var row = ((GridViewRow)((LinkButton)sender).NamingContainer);
                string path = row.Cells[1].Text;
                path = path.Remove(0, 2);
                string Filpath = Server.MapPath(path);
                string fileName = row.Cells[0].Text;
                var response = System.Web.HttpContext.Current;

                System.IO.FileInfo file = new System.IO.FileInfo(Filpath);
                if (file.Exists) {

                    response.Response.ClearContent();
                    response.Response.Clear();
                    string ext = Path.GetExtension(path); //get file extension
                    string type = "";

                    //set known types based on file extension
                    if (ext != null) {
                        switch (ext.ToLower()) {
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
            catch (Exception ex) {

                throw ex;
            }


        }

        protected void btnfileUpload_Click(object sender, EventArgs e) {
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlviewdocsuplod').modal('show');});   </script>", false);
        }

        protected void btnReopenBid_Click(object sender, EventArgs e) {
            var PrMaster = new JavaScriptSerializer().Deserialize<PR_Master>(ViewState["PrMaster"].ToString());
            int bidId = int.Parse(hdnBidId.Value);



            int result = biddingController.ResetSelections(bidId);

            if (result > 0) {
                result = biddingController.ReOpenBid(bidId, int.Parse(Session["UserId"].ToString()), LocalTime.Now.AddDays(int.Parse(hdnReOpenDays.Value.ProcessString())), hdnReopenRemarks.Value);

                if (result > 0) {
                    PrMaster.Bids.Remove(PrMaster.Bids.Find(b => b.BidId == bidId));
                    ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
                    LoadGV();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on Reopening Bid', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }
            }
            else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on Resetting Bid', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }
        }

        protected void btnViewAttachmentsnew_Click(object sender, EventArgs e) {
            var QuotationId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);

            gvDocs.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).SupplierQuotations.Find(q => q.QuotationId == int.Parse(QuotationId.ToString())).UploadedFiles;
            gvDocs.DataBind();

            gvImages.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).SupplierQuotations.Find(q => q.QuotationId == int.Parse(QuotationId.ToString())).QuotationImages;
            gvImages.DataBind();
            txtTermsAndConditions.Text = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString())).SupplierQuotations.Find(q => q.QuotationId == int.Parse(QuotationId.ToString())).TermsAndCondition;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlAttachments').modal('show') });   </script>", false);


        }

        protected void btnViewItems_Click(object sender, EventArgs e) {


            var ItemId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[5].Text);

            gvPurchasedItems.DataSource = pODetailsController.GetPUrchasedItems(ItemId, int.Parse(Session["CompanyId"].ToString()));
            gvPurchasedItems.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItems').modal('show'); });   </script>", false);


        }

        protected void SelectQuotation_Click(object sender, EventArgs e) {

            List<TabulationDetail> TabulationDetailsList = new JavaScriptSerializer().Deserialize<List<TabulationDetail>>(ViewState["SelectedQuotations"].ToString());

            decimal totVAt = 0;
            decimal totNbt = 0;
            decimal totNetTot = 0;
            decimal totSubTot = 0;
            int TabulationId = 0;

            string finalizedRemark = hdnFinalizeRemark.Value;

            for (int i = 0; i < TabulationDetailsList.Count; i++) {

                totVAt = totVAt + TabulationDetailsList[i].VAtAmount;
                totNbt = totNbt + TabulationDetailsList[i].NbtAmount;
                totNetTot = totNetTot + TabulationDetailsList[i].NetTotal;
                totSubTot = totSubTot + TabulationDetailsList[i].SubTotal;
                TabulationId = TabulationDetailsList[i].TabulationId;

            }
            int categoryId = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCategoryId;

            int result = tabulationMasterController.UpdateTabulationDetails(int.Parse(Session["UserId"].ToString()), totVAt, totNbt, totNetTot, totSubTot, int.Parse(ViewState["BidId"].ToString()), TabulationId, int.Parse(ViewState["PrId"].ToString()), categoryId, int.Parse(Session["DesignationId"].ToString()), finalizedRemark, TabulationDetailsList, 1);
            if (result > 0) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}) });   </script>", false);

            }

        }

        protected void Back_Click(object sender, EventArgs e) {
            pnlQuotationSelection.Visible = false;
            pnlView.Visible = true;
        }

        protected void btnUpload_Click(object sender, EventArgs e) {

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();swal({ type: 'success',title: 'Your Documents have been saved', showConfirmButton: false,timer: 1500}).then((result) => { $('#mdlQuotations').modal('show') }); });   </script>", false);
            //if ((fileUpload1.PostedFile != null && fileUpload1.PostedFile.ContentLength > 0))
            //{
            //    int seq = 0;
            //    var isUploaded = committeeController.IsDocsUploaded(BidId, tabulationMaster.TabulationId, "T");
            //    if (isUploaded > 0)
            //    {
            //        seq = committeeController.GetUploedasequence(BidId, tabulationMaster.TabulationId, "T");
            //    }

            //    List<TecCommitteeFileUpload> Committeefiles = new List<TecCommitteeFileUpload>();

            //    HttpFileCollection uploadedFiles = Request.Files;
            //    for (int i = 0; i < uploadedFiles.Count; i++)
            //    {

            //        HttpPostedFile userPostedFile = uploadedFiles[i];
            //        string path = "";
            //        var extention = System.IO.Path.GetExtension(userPostedFile.FileName);
            //        string filenameWithoutPath = Path.GetFileName(userPostedFile.FileName);
            //        string imagename = LocalTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + filenameWithoutPath;
            //        path = "~/TechCommitteeDocs/" + imagename;
            //        fileUpload1.SaveAs(Server.MapPath("TechCommitteeDocs") + "\\" + imagename);
            //        seq = seq + 1;
            //        TecCommitteeFileUpload Committeefile = new TecCommitteeFileUpload();
            //        Committeefile.BidId = BidId;
            //        Committeefile.tabulationId = int.Parse(hdnTabulationId.Value);
            //        Committeefile.filename = imagename;
            //        Committeefile.filepath = path;
            //        Committeefile.sequenceId = seq;
            //        Committeefile.commiteetype = "T";
            //        Committeefiles.Add(Committeefile);


            //    }

            //    var isSaved = committeeController.SaveUploadedtechcommitteefiles(Committeefiles);
            //    if (isSaved > 0)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();swal({ type: 'success',title: 'Your Documents have been saved', showConfirmButton: false,timer: 1500}).then((result) => { $('#mdlQuotations').modal('show') }); });   </script>", false);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();swal({ type: 'error',title: 'ERROR',text:'Error Sving files', showConfirmButton: true,timer: 4000}).then((result) => { $('#mdlviewdocsuplod').modal('show') }); });   </script>", false);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();swal({ type: 'error',title: 'ERROR',text:'Empty files or Invalid fomat', showConfirmButton: true,timer: 4000}).then((result) => { $('#mdlviewdocsuplod').modal('show') }); $('.modal-backdrop').remove(); });   </script>", false);
            //}
        }
    }

}