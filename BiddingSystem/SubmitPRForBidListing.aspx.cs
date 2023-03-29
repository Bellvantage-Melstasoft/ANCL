using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Data;
using System.Threading;
using System.Web.Script.Serialization;
using CLibrary.ViewModels;

namespace BiddingSystem
{
    public partial class SubmitPRForBidListing : System.Web.UI.Page
    {       
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController PrMataterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController PrDetailsController = ControllerFactory.CreatePR_DetailController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
        PR_FileUploadController pR_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_Replace_FileUploadController pr_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
        PR_Replace_FileUploadController pR_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
        PR_SupportiveDocumentController pR_SupportiveDocumentController = ControllerFactory.CreatePR_SupportiveDocumentController();
        PR_DetailHistoryController pR_DetailHistoryController = ControllerFactory.CreatePR_DetailHistoryController();
        AddItemController itemController = ControllerFactory.CreateAddItemController();
        BiddingController bidController = ControllerFactory.CreateBiddingController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        Procument_Plan_Type_Controller procumentPlanTypeController = ControllerFactory.CreateProcument_Plan_Type_Controller();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemCategoryOwnerController itemCategoryOwnerController = ControllerFactory.CreateItemCategoryOwnerController();
        BiddingMethodController biddingMethodController = ControllerFactory.CreateBiddingMethodController();
        PrCapexController prCapexController = ControllerFactory.CreatePrCapexController();
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabId = "submitForBiddingLink";
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
                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[4] { new DataColumn("PlanId"), new DataColumn("PlanName"), new DataColumn("StartDate"), new DataColumn("EndDate") });

                        ViewState["ProcumentPlan"] = dt;
                        this.BindGrid();

                        LoadBiddingMethod();
                        GetProcumentPlan();
                        SetDateValidations();

                        GeneralSetting generalSettings = generalSettingsController.FetchGeneralSettingsListByIdObj(int.Parse(Session["CompanyId"].ToString()));
                        if (generalSettings.DepartmentId != 0)
                        {
                            txtBidOpenedFor.Text = generalSettings.BidOpeningPeriod.ToString("N0");
                            if (generalSettings.BidOnlyRegisteredSupplier == 1)
                            {
                                ddlOpenBidsTo.SelectedIndex = 0;
                            }
                            else
                            {
                                ddlOpenBidsTo.SelectedIndex = 1;
                            }

                            if (generalSettings.CanOverride == 1)
                            {
                                dtStartDate.Enabled = true;
                                ddlOpenBidsTo.Enabled = true;
                            }
                            else
                            {
                                dtStartDate.Enabled = false;
                                ddlOpenBidsTo.Enabled = false;
                            }
                        }
                        txtAmount.Enabled = false;
                        txtPercentage.Enabled = false;
                        txtPeriodFrom.Enabled = false;
                        txtPeriodTo.Enabled = false;
                        txtAmountRP.Enabled = false;
                        txtPercentageRP.Enabled = false;
                        txtPeriodfrom1.Enabled = false;
                        txtPeriodTo1.Enabled = false;
                        ViewState["PrId"] = int.Parse(Request.QueryString.Get("PrId"));
                        LoadData();
                        LoadPurchasingOfficer();

                        dtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        ddlBidOpenType.SelectedValue = "2";  // Manual
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            
        }
        
        private void LoadPurchasingOfficer()
        {
            CompanyLogin cl = itemCategoryOwnerController.GetCurrentPurchasingOfficer(new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCategoryId);
            List<CompanyLogin> Cls = companyLoginController.GetUserListByDepartmentid(int.Parse(Session["CompanyId"].ToString()));

            ddlPurchasingOfficer.DataSource = Cls;
            ddlPurchasingOfficer.DataValueField = "UserId";
            ddlPurchasingOfficer.DataTextField = "FirstName";
            ddlPurchasingOfficer.DataBind();
            if (cl != null)
            {
                ListItem item = ddlPurchasingOfficer.Items.FindByValue(cl.UserId.ToString());
                if (item != null)
                    item.Selected = true;
            }
        }
        
        protected void BindGrid()
        {
            gvProcumentPlan.DataSource = (DataTable)ViewState["ProcumentPlan"];
            gvProcumentPlan.DataBind();
        }

        private void SetDateValidations()
        {
            TextBox[] textboxes = new TextBox[] { dtStartDate, txtStartDate, txtEndDate, txtPeriodfrom1, txtPeriodFrom, txtPeriodTo, txtPeriodTo1 };
            foreach (var item in textboxes) { item.Attributes.Add("min", LocalTime.Now.ToString("yyyy-MM-dd")); }
        }

        private void GetProcumentPlan()
        {
            Procument_Plan_Type_Controller procument_Plan_Type_Controller = ControllerFactory.CreateProcument_Plan_Type_Controller();
            List<Procument_PlanType> typeList = procument_Plan_Type_Controller.FetchAllProcument_Plan_Type();
            ddlProcumentPlan.DataSource = typeList;
            ddlProcumentPlan.DataValueField = "PlanId";
            ddlProcumentPlan.DataTextField = "PlanName";
            ddlProcumentPlan.DataBind();
            ddlProcumentPlan.Items.Insert(0, new ListItem("Select Procument Plan", "0"));
        }

        private void LoadBiddingMethod()
        {
            try
            {
             List<BiddingMethod> biddingMethod = biddingMethodController.GetBiddingMethodList();
                ViewState["biddingMethod"] = new JavaScriptSerializer().Serialize(biddingMethod);

                ddlBiddingMethod.DataSource = biddingMethod;
                ddlBiddingMethod.DataValueField = "BiddingMethodId";
                ddlBiddingMethod.DataTextField = "BiddingMethodName";
                ddlBiddingMethod.DataBind();
                ddlBiddingMethod.Items.Insert(0, new ListItem("Select Bidding Method", "0"));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LoadData()
        {
            var PrMaster = PrMataterController.GetPrForBidSubmission(int.Parse(ViewState["PrId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
            ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
            bool IsImport = PrMaster.PurchaseType == 2 ? true : false;

            if (PrMaster != null)
            {
                lblPRNo.Text = "PR-"+PrMaster.PrCode;
                lblCreatedOn.Text = PrMaster.CreatedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                lblCreatedBy.Text = PrMaster.CreatedByName;
                lblRequestBy.Text = PrMaster.CreatedByName;
                lblRequestFor.Text = PrMaster.RequiredFor;
                lblExpenseType.Text = (PrMaster.ExpenseType == 1) ? "Capital Expense" : "Operational Expense";
                lblDepartment.Text = !String.IsNullOrEmpty(PrMaster.SubDepartmentName) ? PrMaster.SubDepartmentName : "Stores";
                lblWarehouse.Text = PrMaster.WarehouseName;
                lblPurchaseType.Text = PrMaster.PurchaseType == 1 ? "Local":"Import";
                if (PrMaster.MrnId == null || PrMaster.MrnId == 0)
                {
                    lblPRRequestedDate.Text = PrMaster.ExpectedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                    
                }
                else
                {
                    lblPRRequestedDate.Text = PrMaster.MRNExpectedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                }            

                if (PrMaster.MrnCode != null)
                {
                    divMrnReferenceCode.Visible = true;
                    lblMrnReferenceCode.Text = PrMaster.MrnCode.ToString() != "" ? "MRN-" + PrMaster.MrnCode.ToString() : "No";
                    ViewState["MrnId"] = PrMaster.MrnId;
                }
                
                lblApprovedBy.Text = PrMaster.ApprovedByName;
                lblApprovedOn.Text = PrMaster.PrApprovalOn.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]); 
                lblCategory.Text = PrMaster.PrCategoryName;
                lblSubcategory.Text = PrMaster.SubCategoryName;

                if (PrMaster.PrDetails.Count > 0)
                {
                    gvPRView.DataSource = PrMaster.PrDetails;
                    gvPRView.DataBind();

                    divPrDetails.Visible = true;
                    divBidBasicInfo.Visible = true;
                    div1.Visible = true;
                    divBoxFooter.Visible = true;
                }
                else
                {
                    divPrDetails.Visible = false;
                    divBidBasicInfo.Visible = false;
                    div1.Visible = false;
                    divBoxFooter.Visible = false;
                }

                gvBids.DataSource = PrMaster.Bids;
                gvBids.DataBind();

                if (IsImport)
                {
                    ddlBidType.SelectedValue = "1";
                    //ddlBidType.Enabled = false;
                }
            }
        }

        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                gvBidItems.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();
            }

        }

        protected void btnViewzReplacementPhotos_Click(object sender, EventArgs e)
        {
            
            try
            {
                hdnStatus.Value = "1";
                int PrdId = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[1].Text);

                gvViewReplacementImages.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrDetails.Find(prd => prd.PrdId == PrdId).PrReplacementFileUploads;
                gvViewReplacementImages.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBidMoreDetails').modal('hide'); $('#mdlReplacementImages').modal('show'); });   </script>", false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnViewUploadPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                hdnStatus.Value = "1";
                int PrdId = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[1].Text);

                gvUploadedPhotos.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrDetails.Find(prd => prd.PrdId == PrdId).PrFileUploads;
                gvUploadedPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {$('#mdlBidMoreDetails').modal('hide');  $('#mdlFileUpload').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {

            }
        }

        protected void lblViewBom_Click(object sender, EventArgs e)
        {
            try
            {
                hdnStatus.Value = "1";
                int PrdId = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[1].Text);

                gvBOMDate.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrDetails.Find(prd => prd.PrdId == PrdId).PrBoms;
                gvBOMDate.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBidMoreDetails').modal('hide'); $('#mdlItemSpecs').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnViewSupportiveDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                hdnStatus.Value = "1";
                int PrdId = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[1].Text);

                gvSupportiveDocuments.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrDetails.Find(prd => prd.PrdId == PrdId).PrSupportiveDocuments;
                gvSupportiveDocuments.DataBind();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {$('#mdlBidMoreDetails').modal('hide'); $('#mdlSupportiveDocs').modal('show'); });   </script>", false);

            }
            catch (Exception)
            {

            }
        }

        protected void btnSubmitBid_Click(object sender, EventArgs e)
        {
            List<GridViewRow> selectedRows = gvPRView.Rows.OfType<GridViewRow>().Where(r => (r.Cells[0].FindControl("CheckBox1") as CheckBox).Checked == true).ToList();

            if (selectedRows.Count > 0)
            {
                if (ValidateBid() == "")
                {
                    List<Bidding> bids = new List<Bidding>();

                    List<Bid_Bond_Details> bid_Bond_Details = new List<Bid_Bond_Details>();
                    List<BiddingPlan> biddingPlan = new List<BiddingPlan>();

                    List<BiddingPlan> biddingPlanList = PopulateBiddingPlan();
                    List<Bid_Bond_Details> bid_Bond_DetailsList = PopulateBid_Bond_Details();

                    if (ddlBidType.SelectedIndex == 1)
                    {
                        Bidding bid = PopulateBid();

                        for (int i = 0; i < selectedRows.Count; i++)
                        {
                            bid.BiddingItems.Add(populateBidItem(selectedRows[i]));
                        }

                        bid.BiddingPlan = biddingPlanList;
                        bid.BidBondDetails = bid_Bond_DetailsList;

                        bids.Add(bid);
                    }
                    else
                    {
                        for (int i = 0; i < selectedRows.Count; i++)
                        {

                            Bidding bid = PopulateBid();
                            bid.BiddingItems.Add(populateBidItem(selectedRows[i]));

                            bid.BiddingPlan = biddingPlanList;
                            bid.BidBondDetails = bid_Bond_DetailsList;

                            PopulateBid_Bond_Details();
                            bids.Add(bid);
                        }
                    }
                    
                   List<int> result = bidController.SaveBids(bids);
                    //int result = bidController.SaveBids(bids);
                    if (result.Count > 0)
                    {
                        ClearFields();

                       
                        for (int x = 0; x < result.Count; ++x)
                        {
                            int bidCode = bidController.GetBiddingDetailsByBiddingId(result[x]).BidCode;
                            bids[x].BidCode = bidCode;
                        }
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on submitting bids', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: '" + ViewState["errorMessage"] + "', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Please Select At Least One Item', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }


        }

        private Bidding PopulateBid()
        {
            Bidding bid = new Bidding();
            bid.PrId = int.Parse(ViewState["PrId"].ToString());
            bid.CreatedUserId = int.Parse(Session["UserId"].ToString());
            bid.CompanyId = int.Parse(Session["CompanyId"].ToString());
            bid.StartDate = DateTime.Parse(dtStartDate.Text) ;
            bid.EndDate = DateTime.Parse(dtStartDate.Text).AddDays(int.Parse(txtBidOpenedFor.Text));
            bid.BidType = int.Parse(ddlBidType.SelectedValue);
            bid.BidOpenType = int.Parse(ddlBidOpenType.SelectedValue);
            bid.BidOpeningPeriod = int.Parse(txtBidOpenedFor.Text);
            bid.BidOpenTo = int.Parse(ddlOpenBidsTo.SelectedValue);
            bid.TermsAndConditions = txtTermsAndConditions.Text;
            bid.CategoryId = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCategoryId;
            bid.PurchasingOfficer = int.Parse(ddlPurchasingOfficer.SelectedValue);

            bid.BiddingItems = new List<BiddingItem>();
            return bid;
        }

        private List<BiddingPlan> PopulateBiddingPlan()
        {

            List<BiddingPlan> biddingPlan_list = new List<BiddingPlan>();


            DataTable dt = (DataTable)ViewState["ProcumentPlan"];
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    BiddingPlan biddingPlan = new BiddingPlan();
                    biddingPlan.PlanId = int.Parse(row["PlanId"].ToString());
                    biddingPlan.StartDate = DateTime.Parse(row["StartDate"].ToString());
                    biddingPlan.EndDate = DateTime.Parse(row["EndDate"].ToString());
                    biddingPlan.EnteredUser = Session["UserId"].ToString();
                    biddingPlan.EnteredDate = LocalTime.Now;
                    biddingPlan_list.Add(biddingPlan);
                }
            }
            return biddingPlan_list;

        }

        private string ValidateBid()
        {
            string errorMessage1 = "", errorMessage2 = "";

            if (rdoRequired.Checked == true)
            {
                if ((txtAmount.Text == string.Empty && txtPercentage.Text == string.Empty)||( txtPeriodFrom.Text == string.Empty || txtPeriodTo.Text == string.Empty))
                {
                    errorMessage1 = "Bid Bond Details Required</br>";
                }
            }

            if (rdoRequiredPerformance.Checked == true)
            {

                if ( ( txtAmountRP.Text == string.Empty &&  txtPercentageRP.Text == string.Empty) || (txtPeriodfrom1.Text == string.Empty || txtPeriodTo1.Text == string.Empty))
                {
                    errorMessage2 = "Perfomance Bond Details Required</br>";
                }
            }

          string  errorMessage = errorMessage1 + errorMessage2;
            ViewState["errorMessage"] = errorMessage;
            return errorMessage;
        }

        private List<Bid_Bond_Details> PopulateBid_Bond_Details()
        {
            List<Bid_Bond_Details> bid_Bond_Details_list = new List<Bid_Bond_Details>();

            if (rdoRequired.Checked == true)
            {

                Bid_Bond_Details bid_Bond_Details = new Bid_Bond_Details();

                bid_Bond_Details.BondtypeId = 1;
                bid_Bond_Details.IsRequired = 1;
                bid_Bond_Details.Amount = txtAmount.Text!= "" ? decimal.Parse(txtAmount.Text) : 0;
                bid_Bond_Details.Percentage = txtPercentage.Text!="" ? decimal.Parse(txtPercentage.Text) :0 ;
                bid_Bond_Details.FromDate = DateTime.Parse(txtPeriodFrom.Text);
                bid_Bond_Details.ToDate = DateTime.Parse(txtPeriodTo.Text);
                bid_Bond_Details.EnteredDate = LocalTime.Now;
                bid_Bond_Details.EnteredUser = Session["UserId"].ToString();
                bid_Bond_Details_list.Add(bid_Bond_Details);
            }


            else
            {
                Bid_Bond_Details bid_Bond_Details = new Bid_Bond_Details();
                bid_Bond_Details.BondtypeId = 1;
                bid_Bond_Details.IsRequired = 0;
                bid_Bond_Details.EnteredDate = LocalTime.Now;
                bid_Bond_Details.EnteredUser = Session["UserId"].ToString();
                bid_Bond_Details_list.Add(bid_Bond_Details);
            }


            if (rdoRequiredPerformance.Checked == true)
            {


                Bid_Bond_Details bid_Bond_Details = new Bid_Bond_Details();
                bid_Bond_Details.BondtypeId = 2;
                bid_Bond_Details.IsRequired = 1;
                bid_Bond_Details.Amount = txtAmountRP.Text!=""? decimal.Parse(txtAmountRP.Text):0;
                bid_Bond_Details.Percentage = txtPercentageRP.Text!=""? decimal.Parse(txtPercentageRP.Text):0;
                bid_Bond_Details.FromDate = DateTime.Parse(txtPeriodfrom1.Text);
                bid_Bond_Details.ToDate = DateTime.Parse(txtPeriodTo1.Text);
                bid_Bond_Details.EnteredUser = Session["UserId"].ToString();
                bid_Bond_Details_list.Add(bid_Bond_Details);

            }
            else
            {
                Bid_Bond_Details bid_Bond_Details = new Bid_Bond_Details();
                bid_Bond_Details.BondtypeId = 2;
                bid_Bond_Details.IsRequired = 0;
                bid_Bond_Details.EnteredDate = LocalTime.Now;
                bid_Bond_Details.EnteredUser = Session["UserId"].ToString();
                bid_Bond_Details_list.Add(bid_Bond_Details);

            }
            return bid_Bond_Details_list;

        }

        private BiddingItem populateBidItem(GridViewRow row)
        {
            PrDetailsV2 prd = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrDetails.Find(p => p.PrdId == int.Parse(row.Cells[1].Text));

            BiddingItem bidItem = new BiddingItem();

            bidItem.PrdId = prd.PrdId;
            bidItem.ItemId = prd.ItemId;
            bidItem.Qty = decimal.Parse((row.FindControl("txtItemQuantity") as TextBox).Text);
            bidItem.EstimatedPrice = prd.EstimatedAmount;

            return bidItem;
        }

        private void ClearFields()
        {
            LoadData();
            ddlBidOpenType.SelectedIndex = 0;
            ddlProcumentPlan.SelectedIndex = 0;
            ddlBidType.SelectedIndex = 0;
            ddlOpenBidsTo.SelectedIndex = 0;
            txtBidOpenedFor.Text = "3";
            txtTermsAndConditions.Text = "";
            dtStartDate.Text = LocalTime.Now.ToString("yyyy-MM-dd");
        }

        protected void btnMoreBidItemDetails_Click(object sender, EventArgs e)
        {
            try
            {
                int PrdId = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[2].Text);

               var PrDetail = PrDetailsController.GetPrDetails(PrdId, int.Parse(Session["CompanyId"].ToString()));

                ViewState["PrDetail"] = new JavaScriptSerializer().Serialize(PrDetail);
                List<PrDetailsV2> PrDetails = new List<PrDetailsV2>();
                PrDetails.Add(PrDetail);
                gvBidMoreDetails.DataSource = PrDetails;
                gvBidMoreDetails.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBidMoreDetails').modal('show'); });   </script>", false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnViewzReplacementPhotosOfBidItem_Click(object sender, EventArgs e)
        {
            try
            {
                hdnStatus.Value = "2";
                gvViewReplacementImages.DataSource = new JavaScriptSerializer().Deserialize<PrDetailsV2>(ViewState["PrDetail"].ToString()).PrReplacementFileUploads;
                gvViewReplacementImages.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBidMoreDetails').modal('hide'); $('#mdlReplacementImages').modal('show'); });   </script>", false);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected void btnViewUploadPhotosOfBidItem_Click(object sender, EventArgs e)
        {
            try
            {
                hdnStatus.Value = "2";
                gvUploadedPhotos.DataSource = new JavaScriptSerializer().Deserialize<PrDetailsV2>(ViewState["PrDetail"].ToString()).PrFileUploads;
                gvUploadedPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlFileUpload').modal('show'); $('#mdlStandardImages').modal('hide'); });   </script>", false);

            }
            catch (Exception ex)
            {

            }
        }

        protected void lblViewBomOfBidItem_Click(object sender, EventArgs e)
        {
            try
            {
                hdnStatus.Value = "2";
                gvBOMDate.DataSource = new JavaScriptSerializer().Deserialize<PrDetailsV2>(ViewState["PrDetail"].ToString()).PrBoms;
                gvBOMDate.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItemSpecs').modal('show'); $('#mdlBidMoreDetails').modal('hide'); });   </script>", false);

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnViewSupportiveDocumentsOfBidItem_Click(object sender, EventArgs e)
        {
            try
            {
                hdnStatus.Value = "2";
                gvSupportiveDocuments.DataSource = new JavaScriptSerializer().Deserialize<PrDetailsV2>(ViewState["PrDetail"].ToString()).PrSupportiveDocuments;
                gvSupportiveDocuments.DataBind();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBidMoreDetails').modal('hide'); $('#mdlSupportiveDocs').modal('show'); });   </script>", false);

            }
            catch (Exception)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void btnSchedulePlan_Click(object sender, EventArgs e)
        {

            dvProcumentPlan.Visible = true;
        }

        protected void btnAddProcumentPlanTogv_Click(object sender, EventArgs e)
        {

            if (ddlProcumentPlan.SelectedIndex != 0)
            {

                DataTable dt = (DataTable)ViewState["ProcumentPlan"];
                string PlanId = ddlProcumentPlan.SelectedValue;
                string PlanName = ddlProcumentPlan.SelectedItem.ToString();
                string StartDate = Convert.ToDateTime(txtStartDate.Text).ToString("dd-MMM-yyyy"); 
                string EndDate = Convert.ToDateTime(txtEndDate.Text).ToString("dd-MMM-yyyy");

                //string fromT = "12:00 AM";
                //string toT = "11:59 PM";
                //StartDate = StartDate + " " + fromT;
                //EndDate = EndDate + " " + toT;

                Procument_PlanType procumentPlanType = procumentPlanTypeController.FetchAllProcumentPlanTypeByPlanId(Convert.ToInt32(PlanId));
                if (procumentPlanType.WithTime == 1)
                {
                    // Meeting with Time
                    string from = Convert.ToDateTime(txtStartDate.Text).ToString("dd-MMM-yyyy");
                    string fromtime = Convert.ToDateTime(txtStartDTime.Text).ToString("hh:mm tt");
                    StartDate = from +" "+ fromtime;
                    string to = Convert.ToDateTime(txtEndDate.Text).ToString("dd-MMM-yyyy");
                    string totime = Convert.ToDateTime(txtEndDTime.Text).ToString("hh:mm tt");
                    EndDate = to + " " + totime;
                    txtStartDTime.Text = "";
                    txtEndDate.Text = "";
                }
                dt.Rows.Add(PlanId, PlanName, StartDate, EndDate);
                ViewState["ProcumentPlan"] = dt;
                this.BindGrid();
                dvProcumentPlanGrid.Visible = true;
                ddlProcumentPlan_SelectedIndexChanged(sender, e);
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int RowId = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;
            DataTable dt = (DataTable)ViewState["ProcumentPlan"];
            dt.Rows[RowId].Delete();
            gvProcumentPlan.DataSource = dt;
            gvProcumentPlan.DataBind();
        }

        protected void ddlProcumentPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProcumentPlan.SelectedIndex != 0)
            {
                dvProcumentPlan.Visible = true;
                txtEndDate.Text = string.Empty;
                txtStartDate.Text = string.Empty;
                Procument_PlanType procumentPlanType = procumentPlanTypeController.FetchAllProcumentPlanTypeByPlanId(Convert.ToInt32(ddlProcumentPlan.SelectedValue));
                if (procumentPlanType.WithTime == 1)
                {
                    txtStartDTime.Enabled = true;
                    txtEndDTime.Enabled = true;
                    RequiredFieldValidator29.Enabled = true;
                    RequiredFieldValidator779.Enabled = true;
                    txtStartDTime.Text = "";
                    txtEndDTime.Text = "";
                }
                else
                {
                    txtStartDTime.Enabled = false;
                    txtEndDTime.Enabled = false;
                    RequiredFieldValidator29.Enabled = false;
                    RequiredFieldValidator779.Enabled = false;
                    txtStartDTime.Text = "";
                    txtEndDTime.Text = "";
                }
            }
            else
            {
                dvProcumentPlan.Visible = false;
            }
        }

        protected void rdoNotRequired_CheckedChanged(object sender, EventArgs e)
        {
            txtAmount.Enabled = false;
            txtPercentage.Enabled = false;
            txtPeriodFrom.Enabled = false;
            txtPeriodTo.Enabled = false;
        }

        protected void rdoRequired_CheckedChanged(object sender, EventArgs e)
        {
            txtAmount.Enabled = true;
            txtPercentage.Enabled = true;
            txtPeriodFrom.Enabled = true;
            txtPeriodTo.Enabled = true;
        }

        protected void rdoRequiredPerformance_CheckedChanged(object sender, EventArgs e)
        {
            txtAmountRP.Enabled = true;
            txtPercentageRP.Enabled = true;
            txtPeriodfrom1.Enabled = true;
            txtPeriodTo1.Enabled = true;
        }

        protected void rdoNotRequiredPerformance_CheckedChanged(object sender, EventArgs e)
        {
            txtAmountRP.Enabled = false;
            txtPercentageRP.Enabled = false;
            txtPeriodfrom1.Enabled = false;
            txtPeriodTo1.Enabled = false;
        }
        //protected void btnPR_Click(object sender, EventArgs e) {


        //    Response.Redirect("ViewPRNew.aspx?PrId= " + ViewState["PrId"]  );
        //  //  Response.Write("<script>window.open ('ViewPRNew.aspx?PrId= " + ViewState["PrId"] + "','_blank');</script>");

        //}



        protected void btnCapexDocs_Click(object sender, EventArgs e) {
            int prId = int.Parse(Request.QueryString.Get("PrId").ToString());
            List<PrCapexDoc> capexDoc = prCapexController.GetPrCapexDocs(prId);

            gvCapexDocs.DataSource = capexDoc;
            gvCapexDocs.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlCapexDocs').modal('show'); });   </script>", false);
        }

        protected void BtnPurchaseHistory_Click(object sender, EventArgs e) {
            int ItemId = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[2].Text);
            List<ItemPurchaseHistory> purchaseHistory = pOMasterController.GetItemPurchaseHistories(ItemId);

            gvPurchaseHistory.DataSource = purchaseHistory;
            gvPurchaseHistory.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlPurchaseHistory').modal('show'); });   </script>", false);
        }



        protected void ddlBiddingMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBiddingMethod.SelectedValue == "1")
            {
                ddlProcumentPlan.Enabled = true;
            }
            else
            {
                ddlProcumentPlan.Enabled = false;
                dvProcumentPlan.Visible = false;
                ddlProcumentPlan.SelectedValue = "0";
                ViewState["ProcumentPlan"] = null;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("PlanId"), new DataColumn("PlanName"), new DataColumn("StartDate"), new DataColumn("EndDate") });
                ViewState["ProcumentPlan"] = dt;
                gvProcumentPlan.DataSource = null;
                gvProcumentPlan.DataBind();
            }

            //if (ddlBiddingMethod.SelectedValue == "1") {
            //    txtBidOpenedFor.Text = "3";
            //}
            //else if (ddlBiddingMethod.SelectedValue == "2") {
            //    txtBidOpenedFor.Text = "4";
            //}
            //else if (ddlBiddingMethod.SelectedValue == "3") {
            //    txtBidOpenedFor.Text = "5";
            //}

            if (ddlBiddingMethod.SelectedValue == "0") {

                txtBidOpenedFor.Text = "";
            }
            else {
                int id = int.Parse(ddlBiddingMethod.SelectedValue);
                BiddingMethod bidding = new JavaScriptSerializer().Deserialize<List<BiddingMethod>>(ViewState["biddingMethod"].ToString()).Where(x => x.BiddingMethodId == id).Single();
                
                 if (ddlBiddingMethod.SelectedValue == id.ToString()) {

                    txtBidOpenedFor.Text = (bidding.OpenedForDays).ToString();
                   


                }
            }
        }

        }
}