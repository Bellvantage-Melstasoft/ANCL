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
    public partial class ViewQuotations : System.Web.UI.Page {
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
                ((BiddingAdmin)Page.Master).subTabValue = "ViewQuotations.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewPrForTabulationSheetApprovalLink";


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
                        pnlTabulationSheet.Visible = false;
                        hdnRejectedQuotationCount.Value = "0";
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
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
        private void LoadGV() {
            try {
                PrMasterV2 pr = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                pr.Bids.ForEach(b => { b.NoOfQuotations = b.SupplierQuotations.Count; b.NoOfRejectedQuotations = b.SupplierQuotations.Count(sq => sq.IsSelected == 2); });
                
                gvBids.DataSource = pr.Bids.Where(b => b.IsQuotationApproved == 0 && b.IsQuotationConfirmed == 0);
                gvBids.DataBind();
                
            }
            catch (Exception ex) {
                throw ex;
            }
        }



        protected void btnView_Click(object sender, EventArgs e) {
            int bidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
            int prid = int.Parse(ViewState["PrId"].ToString());
            ViewState["bidId"] = bidId;
            //Response.Redirect("RejectQuotations.aspx?PrId=" + prid + "&bidId=" + bidId);

            pnlTabulationSheet.Visible = true;
            pnlPR.Visible = false;
            LoadBidItems();
        }



        protected void btnBack_Click(object sender, EventArgs e) {
            pnlTabulationSheet.Visible = false;
            pnlPR.Visible = true;
            btnapprove1.Visible = true;
            btnReject2.Visible = true;
        }


        protected void lbtnApprove_Click(object sender, EventArgs e) {
            string remark = hdnRemarks.Value;

            int approve = pr_MasterController.ApproveBid(int.Parse(Request.QueryString.Get("PrId")), int.Parse(Session["CompanyId"].ToString()), remark);

            if (approve > 0) {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }


        }
        protected void lbtnReject_Click(object sender, EventArgs e) {
            string remark = hdnRemarks.Value;

            if (ViewState["RejectedQuotationItems"] != null) {
                List<SupplierQuotationItem> quotationList = new JavaScriptSerializer().Deserialize<List<SupplierQuotationItem>>(ViewState["RejectedQuotationItems"].ToString());

                int reject = pr_MasterController.RejectBid(int.Parse(Request.QueryString.Get("PrId")), int.Parse(Session["CompanyId"].ToString()), remark, quotationList);

                if (reject > 0) {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForTabulationSheetApproval.aspx' }); });   </script>", false);
                }
            }
            else {
                List<SupplierQuotationItem> quotationList = new List<SupplierQuotationItem>();

                int reject = pr_MasterController.RejectBid(int.Parse(Request.QueryString.Get("PrId")), int.Parse(Session["CompanyId"].ToString()), remark, quotationList);

                if (reject > 0) {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForTabulationSheetApproval.aspx' }); });   </script>", false);
                }

            }
           
        }


        
        private void LoadBidItems() {
            
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["bidId"].ToString()));
            List<BiddingItem> items = bid.BiddingItems;

            //required when reviewing quotations
            items.ForEach(itms => itms.QuotationCount = 0);

            for (int i = 0; i < items.Count; i++) {

                for (int j = 0; j < bid.SupplierQuotations.Count; j++) {
                    if ((bid.SupplierQuotations[j].QuotationItems.FirstOrDefault(sqi => sqi.BiddingItemId == items[i].BiddingItemId)) != null)
                        items[i].QuotationCount += 1;
                }

            }

            gvItems.DataSource = items;
            gvItems.DataBind();
        }


        protected void btnReject_Click(object sender, EventArgs e) {
            try {
                if (ViewState["RejectedQuotationItems"] == null) {
                    List<SupplierQuotationItem> quotationList = new List<SupplierQuotationItem>();

                    SupplierQuotationItem quotation = new SupplierQuotationItem();

                    quotation.QuotationItemId = int.Parse(hdnQuotationItemId.Value);
                    quotation.IsQuotationItemApprovalRemark = hdnQuotationItemRejectRemark.Value;

                    quotationList.Add(quotation);
                    ViewState["RejectedQuotationItems"] = new JavaScriptSerializer().Serialize(quotationList);

                    hdnRejectedQuotationCount.Value = quotationList.Count.ToString();
                }

                else {

                    List<SupplierQuotationItem> quotationList = new JavaScriptSerializer().Deserialize<List<SupplierQuotationItem>>(ViewState["RejectedQuotationItems"].ToString());
                    SupplierQuotationItem quotation = new SupplierQuotationItem();

                    quotation.QuotationItemId = int.Parse(hdnQuotationItemId.Value);
                    quotation.IsQuotationItemApprovalRemark = hdnQuotationItemRejectRemark.Value;

                    quotationList.Add(quotation);

                    ViewState["RejectedQuotationItems"] = new JavaScriptSerializer().Serialize(quotationList);

                    hdnRejectedQuotationCount.Value = quotationList.Count.ToString();
                }



            }
            catch (Exception ex) {

            }

        }

        protected void btnsupplerview_Click(object sender, EventArgs e) {
            var SupplierId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[4].Text);
            Response.Redirect("CompanyUpdatingAndRatingSupplier.aspx?ID=" + SupplierId);
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
                dt.Columns.Add("SupplierName");
                dt.Columns.Add("ReferenceNo");
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

                Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["bidId"].ToString()));

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

            }

        }


        
        protected void btnViewAttachments_Click(object sender, EventArgs e) {
            var QuotationId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);

            gvDocs.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["bidId"].ToString())).SupplierQuotations.Find(q => q.QuotationId == int.Parse(QuotationId.ToString())).UploadedFiles;
            gvDocs.DataBind();

            gvImages.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["bidId"].ToString())).SupplierQuotations.Find(q => q.QuotationId == int.Parse(QuotationId.ToString())).QuotationImages;
            gvImages.DataBind();
            txtTermsAndConditions.Text = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["bidId"].ToString())).SupplierQuotations.Find(q => q.QuotationId == int.Parse(QuotationId.ToString())).TermsAndCondition;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlAttachments').modal('show') });   </script>", false);


        }

        protected void btnViewItems_Click(object sender, EventArgs e)
        {


            var ItemId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[5].Text);

            gvPurchasedItems.DataSource = pODetailsController.GetPUrchasedItems(ItemId, int.Parse(Session["CompanyId"].ToString()));
            gvPurchasedItems.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItems').modal('show'); });   </script>", false);

        }

    }
}