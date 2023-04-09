using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Domain;
using CLibrary.Common;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class BiddingAdmin : System.Web.UI.MasterPage
    {
        int CompanyId = 0;
        public string companyLogo = string.Empty;
        public static string mainTabVal = string.Empty;
        public static string subTabTite = string.Empty;
        public static string subTabIdn = string.Empty;
        public static string subTabVal = string.Empty;

        List<CompanyUserAccess> companyUserAccessList = new List<CompanyUserAccess>();

        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        protected void Page_Load(object sender, EventArgs e)
        {
            mainTabVal = MainTabValue;
            subTabTite = SubTabTitle;
            subTabIdn = subTabId;
            subTabVal = SubTabValue;
            if (Session["UserId"].ToString() != null)
            {
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if (companyLogin.Username == "ANCL")  // Admin
                {

                    liDepartment.Visible = true;
                    liManageDepartments.Visible = true;


                    liWarehouse.Visible = true;
                    liManageWarehouses.Visible = true;
                    //liTransferInventory.Visible = true;
                    liIssuedInventory.Visible = true;
                    //liInventoryReport.Visible = true;


                    liAssignedMRN.Visible = true;
                    liAssignStoreKeeperToMRN.Visible = true;
                    liEditInventory.Visible = true;
                    liReturnStockToWreouse.Visible = true;


                    liUserCreation.Visible = true;
                    createUser.Visible = true;
                    editUser.Visible = true;
                    createRole.Visible = true;
                    defineUserAccess.Visible = true;

                    liSupplier.Visible = true;
                    // approveSuppier.Visible = true;
                    createSupplier.Visible = true;
                    editSupplier.Visible = true;
                    // createSupplierAgent.Visible = true;

                    itemCategory.Visible = true;
                    addMainCategory.Visible = true;
                    addMainCategoryOwners.Visible = true;
                    addSubCategoryStoreKeeper.Visible = true;
                    addAddCommittee.Visible = true;
                    addSubCategory.Visible = true;
                    addItems.Visible = true;


                    liCreateTR.Visible = true;

                    liViewMyTR.Visible = true;

                    liViewAllTR.Visible = true;

                    liApproveTR.Visible = true;

                    liViewSubmittedTR.Visible = true;


                    liMRN.Visible = true;
                    liCreateMRN.Visible = true;
                    liViewMRN.Visible = true;
                    liApproveMRN.Visible = true;
                    //liViewMRNRequestsExpenseApprove.Visible = true;
                    //liSubmittedMRN.Visible = true;
                    liDeliveredMRN.Visible = true;
                    //liViewMyMRNRequests.Visible = true;
                    //mrnInquiryReport.Visible = true;
                    liAvailabilityExpenseMRN.Visible = true;
                    liViewAllMRN.Visible = true;
                    liTR.Visible = true;

                    liPurchaseRequest.Visible = true;
                    createPR.Visible = true;
                    liViewPR.Visible = true;
                    //editPR.Visible = true;
                    approvePR.Visible = true;
                    liViewPRRequestsExpenseApprove.Visible = true;
                    //rejectPR.Visible = true;
                    // viewAllPrStatus.Visible = true;
                    // viewMyPrStatus.Visible = true;
                    // prInquiryReport.Visible = true;
                    prInquiryReport.Visible = true;
                    liViewAllPR.Visible = true;

                    liCreateTR.Visible = true;
                    liViewMyTR.Visible = true;
                    liViewAllTR.Visible = true;
                    liApproveTR.Visible = true;
                    liViewSubmittedTR.Visible = true;

                    liViewDeliveredTrInventory.Visible = true;
                    liConfirmReceivedInventory.Visible = true;
                    liRejectedReceivedInventory.Visible = true;
                    liReceivedIvetory.Visible = true;
                    liApproveRejectedInventory.Visible = true;

                    liPurchasing.Visible = true;
                    submitForBidding.Visible = true;
                    viewSubmittedforBid.Visible = true;
                    approveforBidOpeningg.Visible = true;
                    inProgressBid.Visible = true;
                    closedBids.Visible = true;
                    bidComparrision.Visible = true;
                    approveBidsBeforeComparison.Visible = true;
                    createPo.Visible = true;
                    addImportDetail.Visible = true;
                    //raisePO.Visible = true;
                    //ApprovePO.Visible = true;
                    ViewAllPO.Visible = true;
                    ViewMyPO.Visible = true;
                    ApprovePO.Visible = true;
                    //ApproveModifiedPO.Visible = true;
                    ApprovedPOInvoices.Visible = true;
                    viewPO.Visible = true;
                    generateGRN.Visible = true;
                    grnApproval.Visible = true;
                    companyViewApprovedGRN.Visible = true;
                    CustomerGRNReturn.Visible = true;
                    ViewReturnGRN.Visible = true;
                    ViewInvoices.Visible = true;
                    AddRates.Visible = true;
                    viewCancelledPO.Visible = true;
                    CustomerApprovePOPrint.Visible = true;
                    bidHistory.Visible = true;
                    addImportsRates.Visible = true;
                    addAfterPO.Visible = true;
                    viewAfterPO.Visible = true;
                    //qutationrecommendationfileupload.Visible = true;

                    liApproval.Visible = true;
                    techApproval.Visible = true;
                    procurementApproval.Visible = true;
                    ViewPrForQuotationRejectedRecommendation.Visible = true;
                    ViewPrForQuotationRejectedApprovalNew.Visible = true;






                    liReports.Visible = true;
                    mrReports.Visible = true;
                    prReports.Visible = true;
                    poReports.Visible = true;
                    grnReports.Visible = true;
                    SupplierItemReport.Visible = true;
                    ItemPoreport.Visible = true;
                    //biddingReports.Visible = true;
                    //supplierReports.Visible = true;
                    IssueNoteReport.Visible = true;
                    ReceivedMRNReport.Visible = true;
                    StockOverriddenReport.Visible = true;
                    //InventoryReport.Visible = true;
                    InventoryReportFull.Visible = true;
                    PurchaseRequisitionReport.Visible = true;

                    liGeneralSetting.Visible = true;
                    generalSetting.Visible = true;

                    liManualBids.Visible = true;
                    manualSupplierBids.Visible = true;
                    updateSupplierBidBondDetails.Visible = true;
                    // viewPrRejectedQuotationTabulationSheet.Visible = true;

                    pendingManualBidsid.Visible = true;
                    closebidsManual.Visible = true;
                    //raisePO.Visible = true;
                    liMeasurement.Visible = true;
                    liMeasurement1.Visible = true;


                }
                else
                {

                    companyUserAccessList = companyUserAccessController.FetchCompanyUserAccessByUserId(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                    foreach (var item in companyUserAccessList)
                    {
                        int SystemDivisionId = item.sysDivisionId;
                        int actionId = item.actionId;

                        switch (SystemDivisionId)
                        {

                            case 1:  // Measurement
                                liMeasurement.Visible = true;
                                switch (actionId)
                                {
                                    case 1:
                                        liMeasurement1.Visible = true;
                                        break;
                                }
                                break;

                            case 2:  // User Creation
                                liUserCreation.Visible = true;

                                switch (actionId)
                                {
                                    case 1:
                                        createUser.Visible = true;
                                        break;
                                    case 2:
                                        editUser.Visible = true;
                                        break;
                                    case 3:
                                        defineUserAccess.Visible = true;
                                        break;
                                    case 4:
                                        createRole.Visible = true;
                                        break;
                                }
                                break;

                            case 3:  // Supplier
                                liSupplier.Visible = true;

                                switch (actionId)
                                {
                                    case 1:
                                        //  approveSuppier.Visible = true;
                                        break;
                                    case 2:
                                        createSupplier.Visible = true;
                                        break;
                                    case 3:
                                        editSupplier.Visible = true;
                                        break;
                                }
                                break;

                            case 4:   // Item
                                itemCategory.Visible = true;

                                switch (actionId)
                                {
                                    case 1:
                                        addMainCategory.Visible = true;
                                        break;
                                    case 2:
                                        addSubCategory.Visible = true;
                                        break;
                                    case 3:
                                        addItems.Visible = true;
                                        break;
                                    case 4:
                                        addAddCommittee.Visible = true;
                                        break;
                                    case 5:
                                        addMainCategoryOwners.Visible = true;
                                        break;
                                    case 6:
                                        addSubCategoryStoreKeeper.Visible = true;
                                        break;
                                }
                                break;

                            case 5:  // PR 
                                liPurchaseRequest.Visible = true;

                                switch (actionId)
                                {
                                    case 1:
                                        createPR.Visible = true;
                                        break;
                                    case 2:
                                        liViewPR.Visible = true;
                                        break;
                                    case 3:
                                        approvePR.Visible = true;
                                        break;
                                    case 4:
                                        liViewAllPR.Visible = true;
                                        break;
                                    case 5:
                                        prInquiryReport.Visible = true;
                                        break;
                                    case 6:
                                        liViewPRRequestsExpenseApprove.Visible = true;
                                        break;
                                }
                                break;

                            case 6:   // Purchasing
                                liPurchasing.Visible = true;

                                switch (actionId)
                                {
                                    case 1:
                                        submitForBidding.Visible = true;
                                        break;
                                    case 2:
                                        approveforBidOpeningg.Visible = true;
                                        break;
                                    case 3:
                                        inProgressBid.Visible = true;
                                        break;
                                    case 4:
                                        closedBids.Visible = true;
                                        break;
                                    case 5:
                                        bidComparrision.Visible = true;
                                        break;
                                    case 7:
                                        // ApprovePO.Visible = true;
                                        break;
                                    case 8:
                                        viewPO.Visible = true;
                                        break;
                                    case 9:
                                        generateGRN.Visible = true;
                                        break;
                                    case 10:
                                        grnApproval.Visible = true;
                                        break;
                                    case 11:
                                        companyViewApprovedGRN.Visible = true;
                                        break;
                                    case 12:
                                        bidHistory.Visible = true;
                                        break;
                                    case 15:
                                        createPo.Visible = true;
                                        break;
                                    case 16:  // Send email to supplier
                                        viewSubmittedforBid.Visible = true;
                                        break;
                                    case 17:
                                        addImportDetail.Visible = true;
                                        break;
                                    case 18:
                                        addImportDetail.Visible = true;
                                        break;
                                    case 19:
                                        approveBidsBeforeComparison.Visible = true;
                                        break;
                                    case 20:
                                        ViewAllPO.Visible = true;
                                        break;
                                    case 21:
                                        ViewMyPO.Visible = true;
                                        break;
                                    case 22:
                                        ApprovePO.Visible = true;
                                        break;
                                    case 23:
                                        // ApproveModifiedPO.Visible = true;
                                        break;
                                    case 24:
                                        addImportsRates.Visible = true;
                                        break;
                                    case 25:
                                        addAfterPO.Visible = true;
                                        break;
                                    case 26:
                                        viewAfterPO.Visible = true;
                                        break;
                                    case 27:
                                        ViewInvoices.Visible = true;
                                        break;
                                    case 28:
                                        viewCancelledPO.Visible = true;
                                        break;
                                    case 29:
                                        CustomerGRNReturn.Visible = true;
                                        break;
                                    case 30:
                                        CustomerApprovePOPrint.Visible = true;
                                        break;
                                    case 31:
                                        ApprovedPOInvoices.Visible = true;
                                        break;
                                    case 32:
                                        AddRates.Visible = true;
                                        break;
                                    case 33:
                                        ViewReturnGRN.Visible = true;
                                        break;


                                }
                                break;

                            case 7: // Manual Bids
                                liManualBids.Visible = true;
                                switch (actionId)
                                {
                                    case 1:
                                        pendingManualBidsid.Visible = true;
                                        break;
                                    case 2:
                                        closebidsManual.Visible = true;
                                        break;
                                    case 3:
                                        manualSupplierBids.Visible = true;
                                        break;
                                    case 4:
                                        updateSupplierBidBondDetails.Visible = true;
                                        break;

                                    case 5:
                                        //viewPrRejectedQuotationTabulationSheet.Visible = true;
                                        break;


                                }
                                break;



                            case 8:  // Reports
                                liReports.Visible = true;

                                switch (actionId)
                                {
                                    case 1:
                                        prReports.Visible = true;
                                        break;
                                    case 2:
                                        poReports.Visible = true;
                                        break;
                                    case 3:
                                        grnReports.Visible = true;
                                        break;
                                    case 4:
                                        // biddingReports.Visible = true;
                                        break;
                                    case 5:
                                        //supplierReports.Visible = true;
                                        break;
                                    case 6:
                                        mrReports.Visible = true;
                                        break;
                                    case 7:
                                        InventoryReportFull.Visible = true;
                                        break;
                                    case 8:
                                        IssueNoteReport.Visible = true;
                                        break;
                                    case 9:
                                        ReceivedMRNReport.Visible = true;
                                        break;
                                    case 10:
                                        StockOverriddenReport.Visible = true;
                                        break;
                                    case 11:
                                        PurchaseRequisitionReport.Visible = true;
                                        break;

                                }
                                break;


                            case 9: // General Settings
                                    // liGeneralSetting.Visible = true;
                                switch (actionId)
                                {
                                    case 1:
                                        //  generalSetting.Visible = true;
                                        break;
                                }
                                break;

                            case 10: // Warehouse
                                liWarehouse.Visible = true;
                                switch (actionId)
                                {
                                    case 1:
                                        liManageWarehouses.Visible = true;
                                        break;
                                    case 2:
                                        liAssignStoreKeeperToMRN.Visible = true;
                                        break;
                                    case 3:
                                        liIssuedInventory.Visible = true;
                                        break;
                                    case 4:
                                        liAssignedMRN.Visible = true;
                                        break;
                                    case 5:
                                        liEditInventory.Visible = true;
                                        break;
                                    case 6:
                                        liReturnStockToWreouse.Visible = true;
                                        break;
                                    case 7:
                                        liApproveRejectedInventory.Visible = true;
                                        break;
                                }
                                break;

                            case 11: // Department
                                liDepartment.Visible = true;
                                switch (actionId)
                                {
                                    case 1:
                                        liManageDepartments.Visible = true;
                                        break;
                                }
                                break;

                            case 12: // MRN 
                                liMRN.Visible = true;
                                switch (actionId)
                                {
                                    case 1:
                                        liCreateMRN.Visible = true;
                                        break;
                                    case 2:
                                        liViewMRN.Visible = true;
                                        break;
                                    case 3:
                                        liApproveMRN.Visible = true;
                                        break;
                                    case 4:
                                        liAvailabilityExpenseMRN.Visible = true;
                                        break;
                                    case 5:
                                        liDeliveredMRN.Visible = true;
                                        break;
                                    case 6:
                                        liViewAllMRN.Visible = true;
                                        break;
                                    case 7:
                                        // liViewMyMRNRequests.Visible = true;
                                        break;
                                    case 8:
                                        // mrnInquiryReport.Visible = true;
                                        break;
                                    case 9:
                                        liConfirmReceivedInventory.Visible = true;
                                        break;
                                    case 10:
                                        liRejectedReceivedInventory.Visible = true;
                                        break;
                                    case 11:
                                        liReceivedIvetory.Visible = true;
                                        break;

                                }
                                break;
                            case 13:  // Committee Approval
                                liApproval.Visible = true;
                                switch (actionId)
                                {
                                    case 1:
                                        techApproval.Visible = true;
                                        break;
                                    case 2:
                                        procurementApproval.Visible = true;
                                        break;
                                    case 3:
                                        ViewPrForQuotationRejectedRecommendation.Visible = true;
                                        break;
                                    case 4:
                                        ViewPrForQuotationRejectedApprovalNew.Visible = true;
                                        break;
                                }
                                break;

                            case 14:
                                liTR.Visible = true;
                                switch (actionId)
                                {
                                    case 1:
                                        liCreateTR.Visible = true;
                                        break;
                                    case 2:
                                        liViewMyTR.Visible = true;
                                        break;
                                    case 3:
                                        liViewAllTR.Visible = true;
                                        break;
                                    case 4:
                                        liApproveTR.Visible = true;
                                        break;
                                    case 5:
                                        liViewSubmittedTR.Visible = true;
                                        break;
                                    case 6:
                                        liViewDeliveredTrInventory.Visible = true;
                                        break;


                                }
                                break;
                        }

                    }
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
        }

        public string getJsonMainTab()
        {
            var DataList = mainTabVal;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }
        public string getJsonSubTab()
        {
            var DataList = subTabTite;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }
        public string getJsonSubTabId()
        {
            var DataList = subTabId;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }
        public string getJsonSubTabValue()
        {
            var DataList = subTabVal;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }


        private string MainTabValue;
        private string SubTabTitle;
        private string SubTabId;
        private string SubTabValue;
        public string mainTabValue
        {
            get { return MainTabValue; }
            set { MainTabValue = value; }
        }
        public string subTabTitle
        {
            get { return SubTabTitle; }
            set { SubTabTitle = value; }
        }

        public string subTabId
        {
            get { return SubTabId; }
            set { SubTabId = value; }
        }
        public string subTabValue
        {
            get { return SubTabValue; }
            set { SubTabValue = value; }
        }




        public string passStringValToMasterPage = string.Empty;
    }
}
