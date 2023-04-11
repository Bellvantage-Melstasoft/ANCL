using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLibrary.Controller;


namespace CLibrary.Common
{
    public class ControllerFactory
    {
        public static TrDetailStatusLogController CreateTrDetailStatusLogController()
        {
            TrDetailStatusLogController controller = new TrDetailStatusLogControllerImpl();
            return controller;
        }
        public static DashboardController CreateDashboardController()
        {
            DashboardController controller = new DashboardControllerImpl();
            return controller;
        }
        public static MrnControllerV2 CreateMrnControllerV2()
        {
            MrnControllerV2 controller = new MrnControllerV2Impl();
            return controller;
        }
        public static IMeasurementDetailController CreateMeasurementDetailController()
        {
            IMeasurementDetailController controller = new MeasurementDetailController();
            return controller;
        }
        public static PrControllerV2 CreatePrControllerV2()
        {
            PrControllerV2 controller = new PrControllerV2Impl();
            return controller;
        }
        public static UserWarehouseController CreateUserWarehouse()
        {
            UserWarehouseController controller = new UserWarehouseControllerImpl();
            return controller;
        }

        public static MrnDetailsStatusLogController CreateMrnDetailStatusLogController()
        {
            MrnDetailsStatusLogController controller = new MrnDetailsStatusLogControllerImpl();
            return controller;
        }

        public static SubCategoryStoreKeeperController CreateSubCategoryStoreKeeperController()
        {
            SubCategoryStoreKeeperController controller = new SubCategoryStoreKeeperControllerImpl();
            return controller;
        }


        public static TabulationMasterController CreateTabulationMasterController()
        {
            TabulationMasterController controller = new TabulationMasterControllerImpl();
            return controller;
        }

        public static UserSubDepartmentController CreateUserSubDepartment()
        {
            UserSubDepartmentController controller = new UserSubDepartmentControllerImpl();
            return controller;
        }
        public static DepartmentWarehouseController CreateDepartmentWarehouse()
        {
            DepartmentWarehouseController controller = new DepartmentWarehouseControllerImpl();
            return controller;
        }


        public static TabulationDetailController CreateTabulationDetailController()
        {
            TabulationDetailController controller = new TabulationDetailControllerImpl();
            return controller;
        }

        public static POImportDetailController CreatePOImportDetailController()
        {
            POImportDetailController controller = new POImportDetailControllerImpl();
            return controller;
        }

        public static TabulationApprovalController CreateTabulationApprovalController()
        {
            TabulationApprovalController controller = new TabulationApprovalControllerImpl();
            return controller;
        }

        public static TabulationRecommendationController CreateTabulationRecommendationController()
        {
            TabulationRecommendationController controller = new TabulationRecommendationControllerImpl();
            return controller;
        }

        public static PRStockDepartmentController CreatePRStockDepartmentController()
        {
            PRStockDepartmentController controller = new PRStockDepartmentControllerImpl();
            return controller;
        }

        public static MRNStockDepartmentController CreateMRNStockDepartmentController()
        {
            MRNStockDepartmentController controller = new MRNStockDepartmentControllerImpl();
            return controller;
        }

        public static QuotationApprovalController CreateQuotationApprovalController()
        {
            QuotationApprovalController controller = new QuotationApprovalControllerImpl();
            return controller;
        }

        public static QuotationRecommendationController CreateQuotationRecommendationController()
        {
            QuotationRecommendationController controller = new QuotationRecommendationControllerImpl();
            return controller;
        }

        public static PRDetailsStatusLogController CreatePRDetailsStatusLogController()
        {
            PRDetailsStatusLogController controller = new PRDetailsStatusLogControllerImpl();
            return controller;
        }

        public static DesignationController CreateDesignationController()
        {
            DesignationController designationController = new DesignationControllerImpl();
            return (DesignationController)designationController;
        }
        public static PRexpenseController CreatePRexpenseController()
        {
            PRexpenseController prexpenseController = new PRexpenseControllerImpl();
            return (PRexpenseController)prexpenseController;
        }

        public static PRDStockInfoLogController CreatePRDStockInfoLogController()
        {
            PRDStockInfoLogController proStockInfoController = new PRDStockInfoLogControllerImpl();
            return (PRDStockInfoLogController)proStockInfoController;
        }


        public static ItemCategoryController CreateItemCategoryController()
        {
            ItemCategoryController itemCategoryController = new ItemCategoryControllerImpl();
            return (ItemCategoryController)itemCategoryController;
        }

        public static MRNControllerInterface CreateMRNController()
        {
            MRNControllerInterface mrnControllerInterface = new MRNController();
            return mrnControllerInterface;
        }

        public static SupplierBidBondDetailsController CreateSupplierBidBondDetailsController()
        {
            SupplierBidBondDetailsController supplierBidBondDetailsController = new SupplierBidBondDetailsControllerImpl();
            return (SupplierBidBondDetailsController)supplierBidBondDetailsController;
        }

        public static InventoryControllerInterface CreateInventoryController()
        {
            InventoryControllerInterface controller = new InventoryController();
            return controller;
        }

        public static GRNDIssueNoteControllerInterface CreateGRNDIssueNoteController()
        {
            GRNDIssueNoteControllerInterface controller = new GRNDIssueNoteController();
            return controller;
        }

        public static CommitteeController CreateProcurementCommitteeController()
        {
            CommitteeController procurementCommitteeController = new CommitteeControllerImpl();
            return (CommitteeController)procurementCommitteeController;
        }

        public static MRNDIssueNoteControllerInterface CreateMRNDIssueNoteController()
        {
            MRNDIssueNoteControllerInterface controller = new MRNDIssueNoteController();
            return controller;
        }

        public static StockRaiseControllerInterface CreateStockRaiseController()
        {
            StockRaiseControllerInterface stockRaiseController = new StockRaiseController();
            return stockRaiseController;
        }

        public static StockReleaseControllerInterface CreateStockReleaseController()
        {
            StockReleaseControllerInterface stockReleaseController = new StockReleaseController();
            return stockReleaseController;
        }

        public static SubDepartmentControllerInterface CreateSubDepartmentController()
        {
            SubDepartmentControllerInterface subDepartmentController = new SubDepartmentController();
            return subDepartmentController;
        }

        public static WarehouseControllerInterface CreateWarehouseController()
        {
            WarehouseControllerInterface warehouseController = new WarehouseController();
            return warehouseController;
        }

        public static StockMasterControllerInterface CreateStockMasterController()
        {
            StockMasterControllerInterface stockMasterController = new StockMasterController();
            return stockMasterController;
        }

        public static ItemSubCategoryController CreateItemSubCategoryController()
        {
            ItemSubCategoryController itemSubCategoryController = new ItemSubCategoryControllerImpl();
            return (ItemSubCategoryController)itemSubCategoryController;
        }

        public static ItemImageUploadController CreateItemImageUploadController()
        {
            ItemImageUploadController itemImageUploadController = new ItemImageUploadControllerImpl();
            return (ItemImageUploadController)itemImageUploadController;
        }

        public static AddItemController CreateAddItemController()
        {
            AddItemController addItemController = new AddItemControllerImpl();
            return (AddItemController)addItemController;
        }
        public static PrCapexController CreatePrCapexController()
        {
            PrCapexController prCapexController = new PrCapexControllerImpl();
            return (PrCapexController)prCapexController;
        }
        public static MRNCapexDocController CreateMRNCapexDocController()
        {
            MRNCapexDocController mRNCapexDocController = new MRNCapexDocControllerImpl();
            return (MRNCapexDocController)mRNCapexDocController;
        }


        //public static MrnUpdateLogController CreateMrnUpdateLogController() {
        //    MrnUpdateLogController mrnUpdateLogController = new MrnUpdateLogControllerImpl();
        //    return (MrnUpdateLogController)mrnUpdateLogController;
        //}

        public static GeneralSettingsController CreateGeneralSettingsController()
        {
            GeneralSettingsController generalSettingsController = new GeneralSettingsControllerImpl();
            return (GeneralSettingsController)generalSettingsController;
        }

        public static PR_MasterController CreatePR_MasterController()
        {
            PR_MasterController pr_MasterController = new PR_MasterControllerImpl();
            return (PR_MasterController)pr_MasterController;
        }

        public static PR_DetailController CreatePR_DetailController()
        {
            PR_DetailController pr_DetailController = new PR_DetailControllerImpl();
            return (PR_DetailController)pr_DetailController;
        }

        public static PR_BillOfMeterialController CreatePR_BillOfMeterialController()
        {
            PR_BillOfMeterialController pr_BillOfMeterialController = new PR_BillOfMeterialControllerImpl();
            return (PR_BillOfMeterialController)pr_BillOfMeterialController;
        }

        public static TempBOMController CreateTempBOMController()
        {
            TempBOMController tempBOMController = new TempBOMControllerImpl();
            return (TempBOMController)tempBOMController;
        }

        public static TempPrFileUploadController CreateTempPrFileUploadController()
        {
            TempPrFileUploadController tempPrFileUploadController = new TempPrFileUploadControllerImpl();
            return (TempPrFileUploadController)tempPrFileUploadController;
        }

        public static PR_FileUploadController CreatePR_FileUploadController()
        {
            PR_FileUploadController pr_FileUploadController = new PR_FileUploadControllerImpl();
            return (PR_FileUploadController)pr_FileUploadController;
        }

        public static SupplierCategoryController CreateSupplierCategoryController()
        {
            SupplierCategoryController supplierCategoryController = new SupplierCategoryControllerImpl();
            return (SupplierCategoryController)supplierCategoryController;
        }

        public static CompanyDepartmentController CreateCompanyDepartmentController()
        {
            CompanyDepartmentController companyDepartmentController = new CompanyDepartmentControllerImpl();
            return (CompanyDepartmentController)companyDepartmentController;
        }

        public static SuplierImageUploadController CreateSuplierImageUploadController()
        {
            SuplierImageUploadController suplierImageUploadController = new SuplierImageUploadControllerImpl();
            return (SuplierImageUploadController)suplierImageUploadController;
        }

        public static SupplierCategoryController CreatesupplierCategoryController()
        {
            SupplierCategoryController supplierCategoryController = new SupplierCategoryControllerImpl();
            return (SupplierCategoryController)supplierCategoryController;
        }

        public static SupplierOutbxController CreateSupplierOutbxController()
        {
            SupplierOutbxController supplierOutbxController = new SupplierOutbxControllerImpl();
            return (SupplierOutbxController)supplierOutbxController;
        }

        public static SupplierRatingController CreateSupplierRatingController()
        {
            SupplierRatingController supplierRatingController = new SupplierRatingControllerImpl();
            return (SupplierRatingController)supplierRatingController;
        }
        public static SupplierController CreateSupplierController()
        {
            SupplierController supplierController = new SupplierControllerImpl();
            return (SupplierController)supplierController;
        }
        public static SupplierLoginController CreateSupplierLoginController()
        {
            SupplierLoginController supplierLoginController = new SupplierLoginControllerImpl();
            return (SupplierLoginController)supplierLoginController;
        }

        public static SupplierAssigneToCompanyController CreateSupplierAssigneToCompanyController()
        {
            SupplierAssigneToCompanyController supplierAssigneToCompanyController = new SupplierAssigneToCompanyControllerImpl();
            return (SupplierAssigneToCompanyController)supplierAssigneToCompanyController;
        }
        public static CompanyLoginController CreateCompanyLoginController()
        {
            CompanyLoginController companyLoginController = new CompanyLoginControllerImpl();
            return (CompanyLoginController)companyLoginController;
        }

        public static SuperAdminController CreateSuperAdminController()
        {
            SuperAdminController superAdminController = new SuperAdminControllerImpl();
            return (SuperAdminController)superAdminController;
        }

        public static BiddingController CreateBiddingController()
        {
            BiddingController biddingController = new BiddingControllerImpl();
            return (BiddingController)biddingController;
        }

        public static SupplierQuotationController CreateSupplierQuotationController()
        {
            SupplierQuotationController supplierQuotationController = new SupplierQuotationControllerImpl();
            return (SupplierQuotationController)supplierQuotationController;
        }

        public static QuotationImageController CreateQuotationImageController()
        {
            QuotationImageController quotationImageController = new QuotationImageControllerImpl();
            return (QuotationImageController)quotationImageController;
        }

        public static POMasterController CreatePOMasterController()
        {
            POMasterController pOMasterController = new POMasterControllerImpl();
            return (POMasterController)pOMasterController;
        }

        public static PODetailsController CreatePODetailsController()
        {
            PODetailsController pODetailsController = new PODetailsControllerImpl();
            return (PODetailsController)pODetailsController;
        }

        public static GrnController CreateGrnController()
        {
            GrnController grnController = new GrnControllerImpl();
            return (GrnController)grnController;
        }
        public static GRNDetailsController CreateGRNDetailsController()
        {
            GRNDetailsController gRNDetailsController = new GRNDetailsControllerImpl();
            return (GRNDetailsController)gRNDetailsController;
        }

        public static SupplierBOMController CreatesupplierBOMController()
        {
            SupplierBOMController supplierBOMController = new SupplierBOMControllerImpl();
            return (SupplierBOMController)supplierBOMController;
        }

        public static SupplierBiddingFileUploadController CreateSupplierBiddingFileUploadController()
        {
            SupplierBiddingFileUploadController supplierBiddingFileUploadController = new SupplierBiddingFileUploadControllerImpl();
            return (SupplierBiddingFileUploadController)supplierBiddingFileUploadController;
        }

        public static NaturseOfBusinessController CreateNaturseOfBusinessController()
        {
            NaturseOfBusinessController naturseOfBusinessController = new NaturseOfBusinessControllerImpl();
            return (NaturseOfBusinessController)naturseOfBusinessController;
        }

        public static FunctionActionController CreateFunctionActionController()
        {
            FunctionActionController functionActionController = new FunctionActionControllerImpl();
            return (FunctionActionController)functionActionController;
        }

        public static RoleFunctionController CreateRoleFunctionController()
        {
            RoleFunctionController roleFunctionController = new RoleFunctionControllerImp();
            return (RoleFunctionController)roleFunctionController;
        }

        public static SystemDivisionController CreateSystemDivisionController()
        {
            SystemDivisionController systemDivisionController = new SystemDivisionControllerImpl();
            return (SystemDivisionController)systemDivisionController;
        }

        public static UserRoleController CreateUserRoleController()
        {
            UserRoleController userRoleController = new UserRoleControllerImpl();
            return (UserRoleController)userRoleController;
        }

        public static CompanyUserAccessController CreateCompanyUserAccessController()
        {
            CompanyUserAccessController companyUserAccessController = new CompanyUserAccessControllerImpl();
            return (CompanyUserAccessController)companyUserAccessController;
        }

        public static PR_Replace_FileUploadController CreatePR_Replace_FileUploadController()
        {
            PR_Replace_FileUploadController pr_Replace_FileUploadController = new PR_Replace_FileUploadControllerImpl();
            return (PR_Replace_FileUploadController)pr_Replace_FileUploadController;
        }

        public static TempPR_FileUploadReplacementController CreateTempPR_FileUploadReplacementController()
        {
            TempPR_FileUploadReplacementController tempPR_FileUploadReplacementController = new TempPR_FileUploadReplacementControllerImpl();
            return (TempPR_FileUploadReplacementController)tempPR_FileUploadReplacementController;
        }

        public static PrTypeController CreatePrTypeController()
        {
            PrTypeController prTypeController = new PrTypeControllerImpl();
            return (PrTypeController)prTypeController;
        }

        public static PrAdditionalColumnController CreatePrAdditionalColumnController()
        {
            PrAdditionalColumnController prAdditionalColumnController = new PrAdditionalColumnControllerImpl();
            return (PrAdditionalColumnController)prAdditionalColumnController;
        }

        public static PrCompanyPrTypeMappingController CreatePrCompanyPrTypeMappingController()
        {
            PrCompanyPrTypeMappingController prCompanyPrTypeMappingController = new PrCompanyPrTypeMappingControllerImpl();
            return (PrCompanyPrTypeMappingController)prCompanyPrTypeMappingController;
        }

        public static BidHistoryController CreateBidHistoryController()
        {
            BidHistoryController bidHistoryController = new BidHistoryControllerImpl();
            return (BidHistoryController)bidHistoryController;
        }
        public static PR_SupportiveDocumentController CreatePR_SupportiveDocumentController()
        {
            PR_SupportiveDocumentController pR_SupportiveDocumentController = new PR_SupportiveDocumentControllerImpl();
            return (PR_SupportiveDocumentController)pR_SupportiveDocumentController;
        }
        public static TempPR_SupportiveDocumentController CreateTempPR_SupportiveDocumentController()
        {
            TempPR_SupportiveDocumentController tempPR_SupportiveDocumentController = new TempPR_SupportiveDocumentControllerImpl();
            return (TempPR_SupportiveDocumentController)tempPR_SupportiveDocumentController;
        }

        public static ItemCategoryOwnerController CreateItemCategoryOwnerController()
        {
            ItemCategoryOwnerController itemCategoryOwnerController = new ItemCategoryOwnerControllerImpl();
            return (ItemCategoryOwnerController)itemCategoryOwnerController;
        }
        public static ItemCategoryOwners2Controller CreateItemCategoryOwners2Controller()
        {
            ItemCategoryOwners2Controller itemCategoryOwners2Controller = new ItemCategoryOwners2ControllerImpl();
            return (ItemCategoryOwners2Controller)itemCategoryOwners2Controller;
        }

        public static PR_DetailHistoryController CreatePR_DetailHistoryController()
        {
            PR_DetailHistoryController pR_DetailHistoryController = new PR_DetailHistoryControllerImpl();
            return (PR_DetailHistoryController)pR_DetailHistoryController;
        }

        public static UnitMeasurementController CreateUnitMeasurementController()
        {
            UnitMeasurementController unitMeasurementController = new UnitMeasurementControllerImpl();
            return (UnitMeasurementController)unitMeasurementController;
        }

        public static ItemCategoryMasterController CreateItemCategoryMasterController()
        {
            ItemCategoryMasterController itemCategoryMasterController = new ItemCategoryMasterControllerImpl();
            return (ItemCategoryMasterController)itemCategoryMasterController;
        }

        public static ItemSubCategoryMasterController CreateItemSubCategoryMasterController()
        {
            ItemSubCategoryMasterController itemSubCategoryMasterController = new ItemSubCategoryMasterControllerImpl();
            return (ItemSubCategoryMasterController)itemSubCategoryMasterController;
        }

        public static AddItemMasterController CreateAddItemMasterController()
        {
            AddItemMasterController addItemMasterController = new AddItemMasterControllerImpl();
            return (AddItemMasterController)addItemMasterController;
        }
        public static AddItemBOMController CreateAddItemBOMController()
        {
            AddItemBOMController addItemBOMController = new AddItemBOMImpl();
            return (AddItemBOMController)addItemBOMController;
        }

        public static ItemCategoryApprovalController CreateItemCategoryApprovalController()
        {
            ItemCategoryApprovalController itemCategoryApprovalController = new ItemCategoryApprovalControllerImpl();
            return (ItemCategoryApprovalController)itemCategoryApprovalController;
        }

        public static CommitteeController CreateApprovalCategoryTypeController()
        {
            CommitteeController approvalCategoryTypeontroller = new CommitteeControllerImpl();
            return (CommitteeController)approvalCategoryTypeontroller;
        }

        public static HSController CreateHScodeController()
        {
            HSController hsController = new HSControllerImpl();
            return (HSController)hsController;
        }
        public static MRNmasterController CreateMRNmasterController()
        {
            MRNmasterController mrnmasterController = new MRNmasterControllerImpl();
            return (MRNmasterController)mrnmasterController;
        }

        public static MRexpenseController CreateMRexpenseController()
        {
            MRexpenseController mrexpenseController = new MRexpenseControllerImpl();
            return (MRexpenseController)mrexpenseController;
        }

        public static MRNDetailController CreateMRNDetailController()
        {
            MRNDetailController mrnDetailController = new MRNDetailControllerImpl();
            return (MRNDetailController)mrnDetailController;
        }

        public static MRNBomController CreateMRNBomController()
        {
            MRNBomController mrnBomController = new MRNBomControllerImpl();
            return (MRNBomController)mrnBomController;
        }

        public static MRNFileUploadController CreateMRNFileUploadController()
        {
            MRNFileUploadController mrnFileUploadController = new MRNFileUploadControllerImpl();
            return (MRNFileUploadController)mrnFileUploadController;
        }

        public static MRsupportiveDocumentController CreateMRsupportiveDocumentController()
        {
            MRsupportiveDocumentController mrsupportiveDocumentController = new MRsupportiveDocumentControllerImpl();
            return (MRsupportiveDocumentController)mrsupportiveDocumentController;
        }

        public static TempMRN_FileUploadReplacementController CreateTempMRN_FileUploadReplacementController()
        {
            TempMRN_FileUploadReplacementController tempMRN_FileUploadReplacementController = new TempMRN_FileUploadReplacementControllerImpl();
            return (TempMRN_FileUploadReplacementController)tempMRN_FileUploadReplacementController;
        }

        public static TempMRN_SupportiveDocumentController CreateTempMRN_SupportiveDocumentController()
        {
            TempMRN_SupportiveDocumentController tempMRN_SupportiveDocumentController = new TempMRN_SupportiveDocumentControllerImpl();
            return (TempMRN_SupportiveDocumentController)tempMRN_SupportiveDocumentController;
        }


        public static TempMRN_BOMController CreateTempMRN_BOMController()
        {
            TempMRN_BOMController tempMRN_BOMController = new TempMRN_BOMControllerImpl();
            return (TempMRN_BOMController)tempMRN_BOMController;
        }

        public static TempMRN_FileUploadController CreateTempMRN_FileUploadController()
        {
            TempMRN_FileUploadController tempMRN_FileUploadController = new TempMRN_FileUploadControllerImpl();
            return (TempMRN_FileUploadController)tempMRN_FileUploadController;
        }



        public static TempMRN_FileUploadReplacementController TempMRN_FileUploadReplacementController()
        {

            TempMRN_FileUploadReplacementController tempMRN_FileUploadReplacementController = new TempMRN_FileUploadReplacementControllerImpl();
            return (TempMRN_FileUploadReplacementController)tempMRN_FileUploadReplacementController;
        }

        public static BiddingMethodController CreateBiddingMethodController()
        {

            BiddingMethodController biddingMethodController = new BiddingMethodControllerImpl();
            return (BiddingMethodController)biddingMethodController;
        }

        public static Procument_Plan_Type_Controller CreateProcument_Plan_Type_Controller()
        {

            Procument_Plan_Type_Controller procument_Plan_Type_Controller = new Procument_Plan_Type_ControllerImpl();
            return (Procument_Plan_Type_Controller)procument_Plan_Type_Controller;
        }


        public static BiddingPlanController CreateBiddingPlanController()
        {

            BiddingPlanController biddingPlanController = new BiddingPlanControllerImpl();
            return (BiddingPlanController)biddingPlanController;
        }


        public static Bid_Bond_Details_Controller CreateBid_Bond_Details_Controller()
        {

            Bid_Bond_Details_Controller bid_Bond_Details_Controller = new Bid_Bond_Details_ControllerImpl();
            return (Bid_Bond_Details_Controller)bid_Bond_Details_Controller;
        }

        public static Mrn_Replace_FileUpload_Controller CreateMrn_Replace_FileUpload_Controller()
        {
            Mrn_Replace_FileUpload_Controller mrn_Replace_FileUpload_Controller = new Mrn_Replace_FileUpload_ControllerImpl();
            return (Mrn_Replace_FileUpload_Controller)mrn_Replace_FileUpload_Controller;
        }

        public static EmailController CreateEmailController()
        {
            EmailController controller = new EmailControllerImpl();
            return controller;
        }

        public static IMeasurementMasterController CreateMeasurementMasterController()
        {
            IMeasurementMasterController controller = new MeasurementMasterController();
            return controller;
        }

        public static IConversionTableMasterController CreateConversionTableMasterController()
        {
            IConversionTableMasterController controller = new ConversionTableMasterController();
            return controller;
        }
        public static WarehouseInventoryBatchesController CreateWarehouseInventoryBatchesController()
        {

            WarehouseInventoryBatchesController cont = new WarehouseInventoryBatchesControllerImp();
            return (WarehouseInventoryBatchesController)cont;
        }

        public static IItemMeasurementController CreateItemMeasurementController()
        {
            IItemMeasurementController controller = new ItemMeasurementController();
            return controller;
        }
        public static IConversionController CreateConversionController()
        {
            IConversionController controller = new ConversionController();
            return controller;
        }

        public static StockOverrideLogController CreateStockOverrideLogController()
        {
            StockOverrideLogController controller = new StockOverrideLogControllerImpl();
            return controller;
        }

        public static GrnFilesController CreateGrnFilesController()
        {
            GrnFilesController controller = new GrnFilesControllerImpl();
            return controller;
        }
        public static SupplierQuotationItemController CreateSupplierQuotationItemController()
        {
            SupplierQuotationItemController controller = new SupplierQuotationItemControllerImpl();
            return controller;
        }
        //stock varificatio function by Pasindu 2020/05/04
        public static PhysicalStockVerificationMasterController CreatePhysicalStockVerificationMaster()
        {
            PhysicalStockVerificationMasterController controller = new PhysicalStockVerificationMasterControllerImpl();
            return controller;
        }
        //stock varificatio function by Pasindu 2020/05/04
        public static PhysicalStockverificationDetailsController CreatePhysicalStockverificationDetails()
        {
            PhysicalStockverificationDetailsController controller = new PhysicalStockverificationDetailsControllerImpl();
            return controller;
        }

        public static BiddingItemController CreateBiddingItemController()
        {
            BiddingItemController controller = new BiddingItemControllerImpl();
            return controller;
        }


        public static DefCurrencyTypeController CreateDefCurrencyTypeController()
        {
            DefCurrencyTypeController defCurrencyTypeController = new DefCurrencyTypeControllerImpl();
            return (DefCurrencyTypeController)defCurrencyTypeController;
        }

        public static DefPaymentModeController CreateDefPaymentModeController()
        {
            DefPaymentModeController defPaymentModeController = new DefPaymentModeControllerImpl();
            return (DefPaymentModeController)defPaymentModeController;
        }

        public static DefTransportModeController CreateDefTransportModeController()
        {
            DefTransportModeController defTransportModeController = new DefTransportModeControllerImpl();
            return (DefTransportModeController)defTransportModeController;
        }

        public static DefPriceTermsController CreateDefPriceTermsController()
        {
            DefPriceTermsController defPriceTermsController = new DefPriceTermsControllerImpl();
            return (DefPriceTermsController)defPriceTermsController;
        }

        public static DefContainerSizeController CreateDefContainerSizeController()
        {
            DefContainerSizeController defContainerSizeController = new DefContainerSizeControllerImpl();
            return (DefContainerSizeController)defContainerSizeController;
        }

        public static CurrencyRateController CreateCurrencyRateController()
        {
            CurrencyRateController currencyRateController = new CurrencyRateControllerImpl();
            return (CurrencyRateController)currencyRateController;
        }

        public static TRMasterController CreateTRMasterController()
        {
            TRMasterController tRMasterController = new TRMasterControllerImpl();
            return (TRMasterController)tRMasterController;
        }

        public static TRDetailsController CreateTRDetailsController()
        {
            TRDetailsController tRDetailsController = new TRDetailsControllerImpl();
            return (TRDetailsController)tRDetailsController;
        }

        public static TRDIssueNoteController CreateTRDIssueNoteController()
        {
            TRDIssueNoteController tRDIssueNoteController = new TRDIssueNoteControllerImpl();
            return (TRDIssueNoteController)tRDIssueNoteController;
        }

        public static TrdIssueNoteBatchController CreateTrdIssueNoteBatchController()
        {
            TrdIssueNoteBatchController trdIssueNoteBatchController = new TrdIssueNoteBatchControllerImpl();
            return (TrdIssueNoteBatchController)trdIssueNoteBatchController;
        }

        public static ImportsHistoryController CreateImportsHistoryController()
        {
            ImportsHistoryController importsHistoryController = new ImportsHistoryControllerImpl();
            return (ImportsHistoryController)importsHistoryController;
        }

        public static ImportsController createImportsController()
        {
            ImportsController controller = new ImportsControllerImp();
            return controller;
        }

        public static AfterPOController createAfterPOController()
        {
            AfterPOController controller = new AfterPOControllerImp();
            return controller;
        }

        public static MrndIssueNoteBatchController CreateMrndIssueNoteBatchController()
        {
            MrndIssueNoteBatchController mrndIssueNoteBatchController = new MrndIssueNoteBatchControllerImpl();
            return (MrndIssueNoteBatchController)mrndIssueNoteBatchController;
        }

        public static StockOverrideBatchLogController CreateStockOverrideBatchLogController()
        {
            StockOverrideBatchLogController stockOverrideBatchLogController = new StockOverrideBatchLogControllerImpl();
            return (StockOverrideBatchLogController)stockOverrideBatchLogController;
        }

        public static InvoiceDetailsController CreateInvoiceDetailsController()
        {
            InvoiceDetailsController invoiceDetailsController = new InvoiceDetailsControllerImpl();
            return (InvoiceDetailsController)invoiceDetailsController;
        }

        public static SupplierTypeController CreateSupplierTypeController()
        {
            SupplierTypeController supplierTypeController = new SupplierTypeControllerImpl();
            return (SupplierTypeController)supplierTypeController;
        }

        public static CompanyTypeController CreateCompanyTypeController()
        {
            CompanyTypeController companyTypeController = new CompanyTypeControllerImpl();
            return (CompanyTypeController)companyTypeController;
        }

        public static GrndReturnNoteController CreateGrndReturnNoteController()
        {
            GrndReturnNoteController grndReturnNoteController = new GrndReturnNoteControllerImpl();
            return (GrndReturnNoteController)grndReturnNoteController;
        }

        public static FollowUpRemarksController CreateFollowUpRemarksController()
        {
            FollowUpRemarksController followUpRemarksController = new FollowUpRemarksControllerImpl();
            return (FollowUpRemarksController)followUpRemarksController;
        }

        public static DutyRatesController CreateDutyRatesController()
        {
            DutyRatesController dutyRatesController = new DutyRatesControllerImpl();
            return (DutyRatesController)dutyRatesController;
        }

        public static InvoiceImageController CreateInvoiceImageController()
        {
            InvoiceImageController invoiceImageController = new InvoiceImageControllerImpl();
            return (InvoiceImageController)invoiceImageController;
        }

        public static GrnReturnDetailsController CreateGrnReturnDetailsController()
        {
            GrnReturnDetailsController grnReturnDetailsController = new GrnReturnDetailsControllerImpl();
            return (GrnReturnDetailsController)grnReturnDetailsController;
        }

        public static GrnReturnMasterController CreateGrnReturnMasterController()
        {
            GrnReturnMasterController grnReturnMasterController = new GrnReturnMasterControllerImpl();
            return (GrnReturnMasterController)grnReturnMasterController;
        }

        public static DepartmentReturnController CreateDepartmentReturnController()
        {
            DepartmentReturnController departmentReturnController = new DepartmentReturnControllerImpl();
            return (DepartmentReturnController)departmentReturnController;
        }

        public static DepartmentReturnBatchController CreateDepartmentReturnBatchController()
        {
            DepartmentReturnBatchController departmentReturnBatchController = new DepartmentReturnBatchControllerImpl();
            return (DepartmentReturnBatchController)departmentReturnBatchController;
        }

        public static TempCoveringPrController CreateTempCoveringPrController()
        {
            TempCoveringPrController tempCoveringPrController = new TempCoveringPrControllerImpl();
            return (TempCoveringPrController)tempCoveringPrController;
        }

        public static TempQuotatioForCoveringPrController CreateTempQuotatioForCoveringPrController()
        {
            TempQuotatioForCoveringPrController tempQuotatioForCoveringPrController = new TempQuotatioForCoveringPrControllerImpl();
            return (TempQuotatioForCoveringPrController)tempQuotatioForCoveringPrController;
        }

        public static SupplierItemReportController CreateSupplierItemReportController()
        {
            SupplierItemReportController supplierItemReportController = new SupplierItemReportControllerImpl();
            return (SupplierItemReportController)supplierItemReportController;
        }

        public static AddItemPOReportsController CreateAddItemPOReportsController()
        {
            AddItemPOReportsController addItemPOReportsController = new AddItemPOReportsControllerImpl();
            return (AddItemPOReportsController)addItemPOReportsController;
        }

        public static ComparisionToLastYearPOReportController CreateComparisionToLastYearPOReportController()
        {
            ComparisionToLastYearPOReportController comparisionToLastYearPOReportController = new ComparisionToLastYearPOReportControllerImpl();
            return (ComparisionToLastYearPOReportController)comparisionToLastYearPOReportController;
        }
    }
}
