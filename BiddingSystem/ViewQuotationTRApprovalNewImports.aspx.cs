using BiddingSystem.Helpers;
using BiddingSystem.ViewModels.CS;
using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class ViewQuotationTRApprovalNewImports : System.Web.UI.Page
    {

        #region Controllers
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        SupplierQuotationItemController supplierQuotationItemController = ControllerFactory.CreateSupplierQuotationItemController();
        BiddingItemController biddingItemController = ControllerFactory.CreateBiddingItemController();
        PODetailsController pODetailsController = ControllerFactory.CreatePODetailsController();
        PR_DetailController pR_DetailController = ControllerFactory.CreatePR_DetailController();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewQuotationTRApproval.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewPrForTabulationSheetApprovalLink";


                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 5) && companyLogin.Usertype != "S")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }

                Session["PrID"] = Convert.ToInt32(Request.QueryString.Get("PrId"));

                Session["PRDetails"] = pr_MasterController.getPRMasterDetailByPrId(Convert.ToInt32(Session["PrID"].ToString()));
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
                        

                        var PrId = int.Parse(Request.QueryString.Get("PrId"));
                        BindBasicDetails(PrId, Convert.ToInt32(Session["CompanyId"].ToString()));
                        LoadGV();

                       
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
            }
        }


        #region Events

        protected void btnView_Click(object sender, EventArgs e)
        {
            int BidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
            ViewState["BidId"] = BidId;
            Session["BidId"] = BidId;

            LoadBidItems(gvItems);
            //LoadBidQuotations();
            LoadBidItemSup(gvQuotationItemsSup);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotations').modal('show'); });   </script>", false);

        }
        
            protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvQuotationItems = e.Row.FindControl("gvQuotationItems") as GridView;
                int BidItemId = int.Parse(gvItems.DataKeys[e.Row.RowIndex].Value.ToString());
                Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));
                TabulationCommon.LoadBidItemsToGrid(BidItemId, gvQuotationItems,bid);

            }

        }

        private void LoadBidItemSup(GridView gridView) {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(Session["BidId"].ToString()));


            List<SupplierQuotation> items = quotationController.GetSupplierQuotations(bid.BidId);
            gridView.DataSource = items;
            gridView.DataBind();


        }
        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());

                List<BiddingItem> listBiddingitems = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == bidId).BiddingItems;
                listBiddingitems = TabulationCommon.RemoveNull(listBiddingitems);
                gvBidItems.DataSource = listBiddingitems;
                gvBidItems.DataBind();
            }
        }

        protected void btnTerminate_Click(object sender, EventArgs e)
        {
            var PrMaster = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString());
            foreach (GridViewRow row in gvBids.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckRow = (row.Cells[0].FindControl("CheckBoxG1") as CheckBox);
                    if (CheckRow.Checked)
                    {
                       
                        int bidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);

                        int result = biddingController.TerminateBid(bidId, int.Parse(Session["UserId"].ToString()), hdnRemarks.Value.ProcessString());

                        if (result > 0)
                        {
                            PrMaster.Bids.Find(b => b.BidId == bidId).IsTerminated = 1;
                            PrMaster.Bids.Find(b => b.BidId == bidId).BiddingItems.ForEach(bi => bi.IsTerminated = 1);
                            Session["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
                            LoadGV();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on Terminating Bid', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        }
                    }
                }
            }
        }

        //Method For Reject Item Wise or Supplierwise
        [WebMethod]
        public static string RejectItemClick(SupplierQuotationItem SupQutItem)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    Page page = (Page)HttpContext.Current.Handler;
                    HiddenField hdnQuotationCount = (HiddenField)page.FindControl("hdnRejectedQuotationCount");
                    string ListCount = "";
                    ResultVM response = new ResultVM();
                  
                    int result = 0;
                    try
                    {
                        if (HttpContext.Current.Session["RejectedQuotationItems"] == null)
                        {
                            List<SupplierQuotationItem> quotationList = new List<SupplierQuotationItem>();

                            SupplierQuotationItem quotation = new SupplierQuotationItem();

                            quotation.QuotationItemId = SupQutItem.QuotationItemId;
                            quotation.IsQuotationItemApprovalRemark = SupQutItem.Remark;

                            quotationList.Add(quotation);
                            HttpContext.Current.Session["RejectedQuotationItems"] = new JavaScriptSerializer().Serialize(quotationList);



                            ListCount = quotationList.Count.ToString();

                            result = 1;
                        }
                        else
                        {
                            List<SupplierQuotationItem> quotationList = new JavaScriptSerializer().Deserialize<List<SupplierQuotationItem>>(HttpContext.Current.Session["RejectedQuotationItems"].ToString());
                            SupplierQuotationItem quotation = new SupplierQuotationItem();

                            if(quotationList.Where(x=> x.QuotationItemId == SupQutItem.QuotationItemId).ToList().Count == 0)
                            {
                               
                                quotation.QuotationItemId = SupQutItem.QuotationItemId;
                                quotation.IsQuotationItemApprovalRemark = SupQutItem.Remark;

                                quotationList.Add(quotation);

                                HttpContext.Current.Session["RejectedQuotationItems"] = new JavaScriptSerializer().Serialize(quotationList);

                                ListCount = quotationList.Count.ToString();

                                result = 1;
                            }
                            else
                            {
                                result = 2;
                            }
                           
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        result = 0;
                    }
                    
                   
                   
                    if (result == 1)
                    {

                        response.Status = 200;
                        response.Data = ListCount;
                    }
                    else if(result == 2)
                    {
                        response.Status = 500;
                        response.Data = "Error \n Record is Already Selected...!";
                    }
                    else
                    {
                        response.Status = 500;
                        response.Data = "Error";
                    }


                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    return SessionExpired();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }

        //Method For Reject PR
        [WebMethod]
        public static string RejectClick(PrMasterV2 prMaster)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    var PrMasterDetails = ControllerFactory.CreatePR_MasterController().getPRMasterDetailByPrId(Convert.ToInt32(HttpContext.Current.Session["PrID"].ToString()));
                    ResultVM response = new ResultVM();
                    int reject = 0;
                    if (HttpContext.Current.Session["RejectedQuotationItems"] != null)
                    {
                        List<SupplierQuotationItem> quotationList = new JavaScriptSerializer().Deserialize<List<SupplierQuotationItem>>(HttpContext.Current.Session["RejectedQuotationItems"].ToString());
                        int bidId = int.Parse(HttpContext.Current.Session["BidId"].ToString());
                        reject = ControllerFactory.CreatePR_MasterController().RejectBid(bidId, int.Parse(HttpContext.Current.Session["CompanyId"].ToString()), prMaster.TerminationRemarks, quotationList);
                       
                        List<BiddingItem> Biddinglist = ControllerFactory.CreateBiddingController().GetPrdIdByBidId(bidId);
                        if (reject > 0) {
                            for (int i = 0; i < Biddinglist.Count; i++) {
                                int prdId = Biddinglist[i].PrdId;
                                ControllerFactory.CreatePR_DetailController().UpdatePRStatuss(int.Parse(HttpContext.Current.Session["UserId"].ToString()), prdId, "TABREVRJCTD", "TABREVRJCTD");

                            }
                        }
                    }
                    else
                    {
                        List<SupplierQuotationItem> quotationList = new List<SupplierQuotationItem>();
                        int test = Convert.ToInt32(HttpContext.Current.Session["PrID"]);


                        reject = ControllerFactory.CreatePR_MasterController().RejectBid(int.Parse(HttpContext.Current.Session["PrID"].ToString()), int.Parse(HttpContext.Current.Session["CompanyId"].ToString()), prMaster.TerminationRemarks, quotationList);
                        int bidId = int.Parse(HttpContext.Current.Session["BidId"].ToString());
                        List<BiddingItem> Biddinglist = ControllerFactory.CreateBiddingController().GetPrdIdByBidId(bidId);
                        if (reject > 0) {
                            for (int i = 0; i < Biddinglist.Count; i++) {
                                int prdId = Biddinglist[i].PrdId;
                                ControllerFactory.CreatePR_DetailController().UpdatePRStatuss(int.Parse(HttpContext.Current.Session["UserId"].ToString()), prdId, "TABREVRJCTD", "TABREVRJCTD");

                            }
                        }
                    }

                    if (reject > 0)
                    {

                        response.Status = 200;
                        response.Data = "PR " + PrMasterDetails.PrCode;
                    }
                    else
                    {
                        response.Status = 500;
                        response.Data = "Error";
                    }


                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    return SessionExpired();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }


        protected void gvQuotationItemsSup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvItemSupllier = (GridView)e.Row.FindControl("gvItemSupllier");//add this

                //DataRowView dr = (DataRowView)e.Row.DataItem;
                //string field1 = dr[0].ToString();
                //int QuotationId = Convert.ToInt32(dr[1]);
                int QuotationId = int.Parse(e.Row.Cells[3].Text);
                LoadBidItemsSupplier(gvItemSupllier, QuotationId);

            }

            
        }

        protected void btnAttachMentsItem_Click(object sender, EventArgs e) {
            var QuotationId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);
            int BidID = Convert.ToInt32(ViewState["BidId"].ToString());
            var Attachementlist = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == BidID).SupplierQuotations.Find(q => q.QuotationId == int.Parse(QuotationId.ToString()));

            gvDocs.DataSource = Attachementlist.UploadedFiles;
            gvDocs.DataBind();

            gvImages.DataSource = Attachementlist.QuotationImages;
            gvImages.DataBind();
            txtTermsAndConditions.Text = Attachementlist.TermsAndCondition;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove(); $('#mdlQuotations').modal('hide'); $('#mdlAttachments').modal('show') });   </script>", false);

        }

        protected void btnAttachMents_Click(object sender, EventArgs e)
        {
            var QuotationId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[3].Text);
            int BidID = Convert.ToInt32(ViewState["BidId"].ToString());
            var Attachementlist = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == BidID).SupplierQuotations.Find(q => q.QuotationId == int.Parse(QuotationId.ToString()));

            gvDocs.DataSource = Attachementlist.UploadedFiles;
            gvDocs.DataBind();

            gvImages.DataSource = Attachementlist.QuotationImages;
            gvImages.DataBind();
            txtTermsAndConditions.Text = Attachementlist.TermsAndCondition;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove(); $('#mdlQuotations').modal('hide'); $('#mdlAttachments').modal('show') });   </script>", false);

        }


        protected void btnsupplerview_Click(object sender, EventArgs e)
        {
            var SupplierId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[4].Text);
            Response.Redirect("CompanyUpdatingAndRatingSupplier.aspx?ID=" + SupplierId);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>window.open ('CompanyUpdatingAndRatingSupplier.aspx?ID=" + SupplierId + "','_blank');</script>",false);
        }
        

        protected void btnPurchased_Click(object sender, EventArgs e)
        {
            var ItemId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[4].Text);

            List<PODetails> listDetails = pODetailsController.GetPUrchasedItems(ItemId, int.Parse(Session["CompanyId"].ToString()));
            gvPurchasedItems.DataSource = listDetails;
            gvPurchasedItems.DataBind();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItems').modal('show'); });   </script>", false);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove(); $('#mdlQuotations').modal('hide'); $('#mdlItems').modal('show'); });   </script>", false);


        }
        protected void btnPurchasedItem_Click(object sender, EventArgs e) {
            var ItemId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[5].Text);

            List<PODetails> listDetails = pODetailsController.GetPUrchasedItems(ItemId, int.Parse(Session["CompanyId"].ToString()));
            gvPurchasedItems.DataSource = listDetails;
            gvPurchasedItems.DataBind();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItems').modal('show'); });   </script>", false);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove(); $('#mdlQuotations').modal('hide'); $('#mdlItems').modal('show'); });   </script>", false);


        }

        [WebMethod]
        public static string ApproveClick(SupplierQuotationItem SupQutItem)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    string ProceedRemark = SupQutItem.ProceedRemark;
                    var PrMasterDetails = ControllerFactory.CreatePR_MasterController().getPRMasterDetailByPrId(Convert.ToInt32(HttpContext.Current.Session["PrID"].ToString()));
                    string ListCount = "";
                    ResultVM response = new ResultVM();
                    int approve = 0;
                    try
                    {
                        //int bidId = int.Parse(HttpContext.Current.Session["BidId"].ToString());
                        int bidId = SupQutItem.BidId;
                        approve = ControllerFactory.CreatePR_MasterController().ApproveBidTab(bidId, int.Parse(HttpContext.Current.Session["CompanyId"].ToString()), SupQutItem.Remark, ProceedRemark);
                        // List<PR_Details> prDetailList = ControllerFactory.CreatePR_DetailController().GetPrDetailsByPrId(int.Parse(HttpContext.Current.Session["PrID"].ToString()), int.Parse(HttpContext.Current.Session["CompanyId"].ToString()));
                        
                        List <BiddingItem> Biddinglist = ControllerFactory.CreateBiddingController().GetPrdIdByBidId(bidId);
                        if (approve > 0) {
                            for (int i = 0; i < Biddinglist.Count; i++) {
                                int prdId = Biddinglist[i].PrdId;
                                ControllerFactory.CreatePR_DetailController().UpdatePRStatuss(int.Parse(HttpContext.Current.Session["UserId"].ToString()), prdId, "BIDCMPRSN", "TABREVAPPROVD");

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        approve = 0;
                    }



                    if (approve > 0)
                    {

                        response.Status = 200;
                        response.Data = "PR " + PrMasterDetails.PrCode;
                    }
                    else
                    {
                        response.Status = 500;
                        response.Data = "Error";
                    }


                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    return SessionExpired();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewPrForTabulationsheetApprovals.aspx");
        }
        #endregion


        #region Methods

        public void BindBasicDetails(int prId,int CompanyId)
        {
            var PrMaster = pr_MasterController.GetPrForQuotationComparison(prId, CompanyId);

            lblPRNo.Text = "PR-"+ PrMaster.PrCode;
            lblCreatedOn.Text = PrMaster.CreatedDateTime.ToString("dd-MM-yyyy");
            lblCreatedBy.Text = PrMaster.CreatedByName;
            lblRequestBy.Text = PrMaster.CreatedByName;
            lblMRNApprovedBy.Text = PrMaster.ApprovedByName == null? "Not from MRN" : PrMaster.ApprovedByName;
            lblMrnCreatedBy.Text = PrMaster.MRNCreatedByName == null ? "Not from MRN" : PrMaster.MRNCreatedByName;
            lblMrnId.Text = PrMaster.MrnCode != null ? "MRN-" + PrMaster.MrnCode : "Not From MRN";
            lblRequestFor.Text = PrMaster.RequiredFor;
            lblExpenseType.Text = (PrMaster.expenseType == "1") ? "Capital Expense" : "Operational Expense";
            lblWhAddress.Text = PrMaster.Address;
            lblWhContactNo.Text = PrMaster.PhoneNo;
            lblWhName.Text = PrMaster.Location;
            lblPurchaseType.Text = int.Parse(PrMaster.PurchaseType) == 1 ? "Local" : "Import";
            if (PrMaster.Location != null)
            {
                pnlWarehouse.Visible = true;
                pnlNotFound.Visible = false;
            }
            else
            {
                pnlNotFound.Visible = true;
                pnlWarehouse.Visible = false;
            }

            ViewState["PrId"] = prId;
            Session["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
        }

        private void LoadGV()
        {
            try
            {
                var PrMaster = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString());
                PrMaster.Bids.ForEach(b => { b.NoOfQuotations = b.SupplierQuotations.Count; });

                if ((Session["IsHeadOfProcurement"] != null && Session["IsHeadOfProcurement"].ToString() == "1")) {
                    PrMaster.Bids.ForEach(b => { b.Visibility = 1; });
                }

                    List<Bidding> BiddingList = PrMaster.Bids; 
                for (int i = 0; i < BiddingList.Count; i++) {
                    DateTime Startdate =  BiddingList[i].StartDate;
                    DateTime Newstartdate = Startdate.AddMinutes(330);
                    BiddingList[i].StartDate = Newstartdate;

                    DateTime EndDate = BiddingList[i].EndDate;
                    DateTime NewEndDate = EndDate.AddMinutes(330);
                    BiddingList[i].EndDate = NewEndDate;
                }


                gvBids.DataSource = PrMaster.Bids.Where(b => b.IsQuotationApproved == 0 && b.IsQuotationConfirmed == 0 && b.IsTabulationReviewApproved != 1 && b.IsTabulationReviewApproved != 2);
                gvBids.DataBind();

              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       


        private static string SessionExpired()
        {
            return
                JsonConvert.SerializeObject(
                new ResultVM()
                {
                    Status = 401,
                    Data = "Session Expired"
                });
        }

        private static string ServerError()
        {
            return
                JsonConvert.SerializeObject(
                new ResultVM()
                {
                    Status = 500,
                    Data = "Server Error Occured"
                });
        }


        private void LoadBidItems(GridView gridView)
        {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));

            List<BiddingItem> items =   TabulationCommon.GetBidItems(bid);
            gridView.DataSource = items;
            gridView.DataBind();

           
        }

        private void LoadBidItemsSupplier(GridView gridView, int QuotationId)
        {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));
            //int TabId = int.Parse(ViewState["tabulationId"].ToString());
            int TabId = 0;
            List<BiddingItem> items = TabulationCommon.GetBidItemsSupplierForReview(bid, QuotationId, TabId);
            gridView.DataSource = items.Where(x => x.QuotedPrice != 0);
            gridView.DataBind();


        }

        public void LoadBidQuotations()
        {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));

            List<BiddingItem> items = TabulationCommon.GetBidItems(bid);
            foreach (BiddingItem item in items)
            {
                TabulationCommon.LoadBidItemsToGrid(item.BiddingItemId, gvQuotationItemsSup, bid);
            }



        }
        #endregion

        protected void btnReopenBid_Click(object sender, EventArgs e)
        {
           
        }

       

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ReOpenBidClick(SupplierQuotationItem SupQutItem) {
            try {
                if (HttpContext.Current.Session["UserId"] != null) {
                    //var PrMasterDetails = new JavaScriptSerializer().Deserialize<PrMasterV2>(HttpContext.Current.Session["PRDetails"].ToString());
                    ResultVM response = new ResultVM();
                    int result = 0;
                    var PrMaster = new JavaScriptSerializer().Deserialize<PR_Master>(HttpContext.Current.Session["PrMaster"].ToString());
                    int bidId = SupQutItem.BidId;



                    result = ControllerFactory.CreateBiddingController().ResetSelections(bidId);

                    if (result > 0) {
                        result = ControllerFactory.CreateBiddingController().ReOpenBid(bidId, int.Parse(HttpContext.Current.Session["UserId"].ToString()), LocalTime.Now.AddDays(int.Parse(SupQutItem.Days.ProcessString())), SupQutItem.Remark);

                        if (result > 0) {
                            PrMaster.Bids.Remove(PrMaster.Bids.Find(b => b.BidId == bidId));
                            HttpContext.Current.Session["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
                            List<BiddingItem> Biddinglist = ControllerFactory.CreateBiddingController().GetPrdIdByBidId(bidId);

                            for (int i = 0; i < Biddinglist.Count; i++) {
                                int prdId = Biddinglist[i].PrdId;
                                ControllerFactory.CreatePR_DetailController().UpdatePRStatuss(int.Parse(HttpContext.Current.Session["UserId"].ToString()), prdId, "QTATNCOLECTN", "BDREOPND");

                            }
                        }

                    }

                    if (result > 0) {

                        response.Status = 200;
                        response.Data = "Bid Re Opened..!";
                    }
                    else {
                        response.Status = 500;
                        response.Data = "Error on Reopening Bid";
                    }


                    return JsonConvert.SerializeObject(response);
                }
                else {
                    return SessionExpired();
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return ServerError();
            }
        }
        public string ProcessMyDataItem(object myValue)
        {
            if (myValue == null)
            {
                return "0";
            }

            return myValue.ToString();
        }

    }
}