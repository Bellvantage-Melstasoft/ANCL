using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLibrary.Infrastructure;
using CLibrary.Infrastructue;

namespace CLibrary.Common
{
    public class DAOFactory
    {
        /// <summary>
        /// 2018-10-11 - 2018-10-12 
        /// </summary>
        /// <returns></returns>
        ///--------------------------SQL SERVER implmenttation DAO class objects
        public static DepartmentWarehouseDAO createDepartmentWarehouseDAO()
        {
            DepartmentWarehouseDAO departmentWarehouseDAO = new DepartmentWarehouseDAOSQLImpl();
            return departmentWarehouseDAO;
        }
        public static MrnMasterDAOV2 CreateMrnMasterDAOV2()
        {
            MrnMasterDAOV2 DAO = new MrnMasterDAOV2Impl();
            return DAO;
        }

        public static MrnDetailsDAOV2 CreateMrnDetailsDAOV2()
        {
            MrnDetailsDAOV2 DAO = new MrnDetailsDAOV2Impl();
            return DAO;
        }

        public static MrnBomDAOV2 CreateMrnBomDAOV2()
        {
            MrnBomDAOV2 DAO = new MrnBomDAOV2Impl();
            return DAO;
        }
        //stock varificatio function by Pasindu 2020/05/04
        public static PhysicalStockVerificationMasterDAO createUserPhysicalStockVerificationMasterDAO()
        {
            PhysicalStockVerificationMasterDAO physicalStockVerificationMasterDAO = new PhysicalStockVerificationMasterDAOSQLImpl();
            return physicalStockVerificationMasterDAO;
        }
        //stock varificatio function by Pasindu 2020/05/04
        public static PhysicalstockVerificationDetailsDAO createPhysicalstockVerificationDetailsDAO()
        {
            PhysicalstockVerificationDetailsDAO physicalstockVerificationDetailsDAO = new PhysicalstockVerificationDetailsDAOSQLImpl();
            return physicalstockVerificationDetailsDAO;
        }

        public static MrnFileUploadDAOV2 CreateMrnFileUploadDAOV2()
        {
            MrnFileUploadDAOV2 DAO = new MrnFileUploadDAOV2Impl();
            return DAO;
        }

        public static MrnReplacementFileUploadDAOV2 CreateMrnReplacementFileUploadDAOV2()
        {
            MrnReplacementFileUploadDAOV2 DAO = new MrnReplacementFileUploadDAOV2Impl();
            return DAO;
        }

        public static MrnSupportiveDocumentsDAOV2 CreateMrnSupportiveDocumentsDAOV2()
        {
            MrnSupportiveDocumentsDAOV2 DAO = new MrnSupportiveDocumentsDAOV2Impl();
            return DAO;
        }

        public static MrnCapexDocDAO CreateMrnCapexDocDAO()
        {
            MrnCapexDocDAO DAO = new MrnCapexDocDAOImpl();
            return DAO;
        }

        public static PrMasterDAOV2 CreatePrMasterDAOV2()
        {
            PrMasterDAOV2 DAO = new PrMasterDAOV2Impl();
            return DAO;
        }

        public static PrDetailsDAOV2 CreatePrDetailsDAOV2()
        {
            PrDetailsDAOV2 DAO = new PrDetailsDAOV2Impl();
            return DAO;
        }

        public static PrBomDAOV2 CreatePrBomDAOV2()
        {
            PrBomDAOV2 DAO = new PrBomDAOV2Impl();
            return DAO;
        }

        public static PrFileUploadDAOV2 CreatePrFileUploadDAOV2()
        {
            PrFileUploadDAOV2 DAO = new PrFileUploadDAOV2Impl();
            return DAO;
        }

        public static PrReplacementFileUploadDAOV2 CreatePrReplacementFileUploadDAOV2()
        {
            PrReplacementFileUploadDAOV2 DAO = new PrReplacementFileUploadDAOV2Impl();
            return DAO;
        }


        public static PrSupportiveDocumentsDAOV2 CreatePrSupportiveDocumentsDAOV2()
        {
            PrSupportiveDocumentsDAOV2 DAO = new PrSupportiveDocumentsDAOV2Impl();
            return DAO;
        }

        public static PrCapexDocDAO CreatePrCapexDocDAO()
        {
            PrCapexDocDAO DAO = new PrCapexDocDAOImpl();
            return DAO;
        }

        public static PrUpdateLogDAO CreatePrUpdateLogDAO()
        {
            PrUpdateLogDAO DAO = new PrUpdateLogDAOImpl();
            return DAO;
        }

        internal static MRNDetailsStatusLogDAO CreateMrnDetailStatusLogDAO()
        {
            MRNDetailsStatusLogDAO DAO = new MRNDetailsStatusLogDAOImpl();
            return DAO;
        }

        public static MrnUpdateLogDAO CreateMrnUpdateLogDAO()
        {
            MrnUpdateLogDAO DAO = new MrnUpdateLogDAOImpl();
            return DAO;
        }

        public static DashboardDAO CreateDashboardDAO()
        {
            DashboardDAO DAO = new DashboardDAOImpl();
            return DAO;
        }

        public static TabulationDetailDAO CreateTabulationDetailDAO()
        {
            TabulationDetailDAO DAO = new TabulationDetailDAOImpl();
            return DAO;
        }
        public static UserSubDepartmentDAO createUserSubDepartmentDAO()
        {
            UserSubDepartmentDAO userSubDepartmentDAO = new UserSubDepartmentDAOSQLImpl();
            return userSubDepartmentDAO;
        }
        public static UserWarehouseDAO createUserWarehouseDAO()
        {
            UserWarehouseDAO userWarehouseDAO = new UserWarehouseDAOSQLImpl();
            return userWarehouseDAO;
        }

        public static SubCategoryStoreKeeperDAO createSubCategoryStoreKeeperDAO()
        {
            SubCategoryStoreKeeperDAO subCategoryStoreKeeperDAO = new SubCategoryStoreKeeperDAOSQLImpl();
            return subCategoryStoreKeeperDAO;
        }

        internal static POImportDetailDAO CreatePOImportDetailDAO()
        {
            POImportDetailDAO DAO = new POAImportDetailDAOImpl();
            return DAO;
        }

        public static TabulationMasterDAO CreateTabulationMasterDAO()
        {
            TabulationMasterDAO DAO = new TabulationMasterDAOImpl();
            return DAO;
        }

        public static PRStockDepartmentDAO CreatePRStockDepartmentDAO()
        {
            PRStockDepartmentDAO DAO = new PRStockDepartmentDAOImpl();
            return DAO;
        }

        public static TabulationApprovalDAO CreateTabulationApprovalDAO()
        {
            TabulationApprovalDAO DAO = new TabulationApprovalDAOImpl();
            return DAO;
        }

        public static TabulationRecommendationDAO CreateTabulationRecommendationDAO()
        {
            TabulationRecommendationDAO DAO = new TabulationRecommendationDAOImpl();
            return DAO;
        }

        public static QuotationApprovalDAO CreateQuotationApprovalDAO()
        {
            QuotationApprovalDAO DAO = new QuotationApprovalDAOImpl();
            return DAO;
        }

        public static QuotationRecommendationDAO CreateQuotationRecommendationDAO()
        {
            QuotationRecommendationDAO DAO = new QuotationRecommendationDAOImpl();
            return DAO;
        }

        public static PRDetailsStatusLogDAO CreatePRDetailsStatusLogDAO()
        {
            PRDetailsStatusLogDAO DAO = new PRDetailsStatusLogDAOSqlImpl();
            return DAO;
        }

        public static PRexpenseDAO CreatePRexpenseDAO()
        {
            PRexpenseDAO DAO = new PRexpenseDAOImpl();
            return DAO;

        }
        public static PRDStockInfoLogDAO CreatePRDStockInfoLogDAO()
        {
            PRDStockInfoLogDAO DAO = new PRDStockInfoLogDAOImpl();
            return DAO;
        }

        internal static ProcurementCommitteeDAO CreateProcurementCommitteeDAO()
        {
            ProcurementCommitteeDAO procurementCommitteeDAO = new ProcurementCommitteeDAOImpl();
            return (ProcurementCommitteeDAO)procurementCommitteeDAO;
        }

        internal static ItemCategoryOwnerDAO CreateItemCategoryOwnerDAO()
        {
            ItemCategoryOwnerDAO DAO = new ItemCategoryOwnerDAOImpl();
            return DAO;
        }
        internal static ItemCategoryOwners2DAO CreateItemCategoryOwners2DAO()
        {
            ItemCategoryOwners2DAO DAO = new ItemCategoryOwners2DAOImpl();
            return DAO;
        }
        public static SupplierQuotationItemDAO CreateSupplierQuotationItemDAO()
        {
            SupplierQuotationItemDAO DAO = new SupplierQuotationItemDAOImpl();
            return DAO;
        }

        public static BiddingItemDAO CreateBiddingItemDAO()
        {
            BiddingItemDAO DAO = new BiddingItemDAOImpl();
            return DAO;
        }

        public static AddItemDAO CreateAddItemDAO()
        {
            AddItemDAO addItemDAO = new AddItemsSQLImpl();
            return (AddItemDAO)addItemDAO;
        }

        public static MrnDAOInterface CreateMRNDAO()
        {
            MrnDAOInterface mrnDAO = new MrnDAO();
            return mrnDAO;
        }

        public static MRNDIssueNoteDAOInterface CreateMRNDIssueNoteDAO()
        {
            MRNDIssueNoteDAOInterface DAO = new MRNDIssueNoteDAO();
            return DAO;
        }

        public static GRNDIssueNoteDAOInterface CreateGRNDIssueNoteDAO()
        {
            GRNDIssueNoteDAOInterface DAO = new GRNDIssueNoteDAO();
            return DAO;
        }

        public static InventoryDAOInterface CreateInventoryDAO()
        {
            InventoryDAOInterface DAO = new InventoryDAO();
            return DAO;
        }


        public static SubDepartmentDAOInterface CreateSubDepartmentDAO()
        {
            SubDepartmentDAOInterface subDepartmentDAO = new SubDepartmentDAO();
            return subDepartmentDAO;
        }

        public static WarehouseDAOInterface CreateWarehouseDAO()
        {
            WarehouseDAOInterface warehouseDAO = new WarehouseDAO();
            return warehouseDAO;
        }

        public static StockRaiseDAOInterface CreateStockRaiseDAO()
        {
            StockRaiseDAOInterface stockRaiseDAO = new StockRaiseDAO();
            return stockRaiseDAO;
        }
        public static StockReleaseDAOInterface CreateStockReleaseDAO()
        {
            StockReleaseDAOInterface stockReleaseDAO = new StockReleaseDAO();
            return stockReleaseDAO;
        }

        public static StockMasterDAOInterface CreateStockMasterDAO()
        {
            StockMasterDAOInterface stockMasterDAO = new StockMasterDAO();
            return stockMasterDAO;
        }

        public static BiddingDAO CreateBiddingDAO()
        {
            BiddingDAO biddingDAO = new BiddingDAOSQLImpl();
            return (BiddingDAO)biddingDAO;
        }

        public static BidHistoryDAO CreateBidHistoryDAO()
        {
            BidHistoryDAO bidHistoryDAO = new BidHistoryDAOSQLImpl();
            return (BidHistoryDAO)bidHistoryDAO;
        }

        public static CompanyDepartmentDAO createCompanyDepartment()
        {
            CompanyDepartmentDAO companyDepartmentDAO = new CompanyDepartmentDAOSQLImpl();
            return (CompanyDepartmentDAO)companyDepartmentDAO;
        }

        public static CompanyLoginDAO createCompanyLoginDAO()
        {
            CompanyLoginDAO companyLoginDAO = new CompanyLoginDAOSQLImpl();
            return (CompanyLoginDAO)companyLoginDAO;
        }

        public static CompanyUserAccessDAO CreateCompanyUserAccessDAO()
        {
            CompanyUserAccessDAO companyUserAccessDAO = new CompanyUserAccessDAOSQLImpl();
            return (CompanyUserAccessDAO)companyUserAccessDAO;
        }

        public static FunctionActionDAO CreateFunctionActionDAO()
        {
            FunctionActionDAO functionActionDAO = new FunctionActionDAOSQLImpl();
            return (FunctionActionDAO)functionActionDAO;
        }

        public static GeneralSettingsDAO CreateGeneralSettingsDAO()
        {
            GeneralSettingsDAO generalSettingsDAO = new GeneralSettingsDAOSQLImpl();
            return (GeneralSettingsDAO)generalSettingsDAO;
        }

        public static GrnDAO createGrnDAO()
        {
            GrnDAO grnDAO = new GrnDAOSQLImpl();
            return (GrnDAO)grnDAO;
        }

        public static GRNDetailsDAO createGRNDetailsDAO()
        {
            GRNDetailsDAO gRNDetailsDAO = new GRNDetailsDAOSQLImpl();
            return (GRNDetailsDAO)gRNDetailsDAO;
        }

        public static ItemCategoryDAO CreateItemCategoryDAO()
        {
            ItemCategoryDAO itemCategoryDAO = new ItemCategoryDAOSQLImpl();
            return (ItemCategoryDAO)itemCategoryDAO;
        }

        public static ItemImageUploadDAO CreateItemImageUploadDAO()
        {
            ItemImageUploadDAO itemImageUploadDAO = new ItemImageUploadDAOSQLImpl();
            return (ItemImageUploadDAO)itemImageUploadDAO;
        }

        public static ItemSubCategoryDAO CreateItemSubCategoryDAO()
        {
            ItemSubCategoryDAO itemSubCategoryDAO = new ItemSubCategoryDAOSQImpl();
            return (ItemSubCategoryDAO)itemSubCategoryDAO;
        }

        public static NaturseOfBusinessDAO CreateNaturseOfBusinessDAO()
        {
            NaturseOfBusinessDAO naturseOfBusinessDAO = new NaturseOfBusinessDAOSQLImpl();
            return (NaturseOfBusinessDAO)naturseOfBusinessDAO;
        }

        public static PODetailsDAO createPODetailsDAO()
        {
            PODetailsDAO pODetailsDAO = new PODetailsDAOSQLImpl();
            return (PODetailsDAO)pODetailsDAO;
        }

        public static POMasterDAO createPOMasterDAO()
        {
            POMasterDAO pOMasterDAO = new POMasterDAOSQLImpl();
            return (POMasterDAO)pOMasterDAO;
        }

        public static PR_BillOfMeterialDAO CreatePR_BillOfMeterialDAO()
        {
            PR_BillOfMeterialDAO pr_BillOfMeterialDAO = new PR_BillOfMeterialDAOSQLImpl();
            return (PR_BillOfMeterialDAO)pr_BillOfMeterialDAO;
        }

        public static PR_DetailDAO CreatePR_DetailDAO()
        {
            PR_DetailDAO pr_DetailDAO = new PR_DetailDAOSQLImpl();
            return (PR_DetailDAO)pr_DetailDAO;
        }

        public static PR_DetailHistoryDAO CreatePR_DetailHistoryDAO()
        {
            PR_DetailHistoryDAO pR_DetailHistoryDAO = new PR_DetailHistoryDAOSQLImpl();
            return (PR_DetailHistoryDAO)pR_DetailHistoryDAO;
        }

        public static PR_FileUploadDAO CreatePR_FileUploadDAO()
        {
            PR_FileUploadDAO pr_FileUploadDAO = new PR_FileUploadDAOSQLImpl();
            return (PR_FileUploadDAO)pr_FileUploadDAO;
        }

        public static PR_MasterDAO CreatePR_MasterDAO()
        {
            PR_MasterDAO pr_MasterDAO = new PR_MasterDAOSQLImpl();
            return (PR_MasterDAO)pr_MasterDAO;
        }

        public static PR_Replace_FileUploadDAO CreatePR_Replace_FileUploadDAO()
        {
            PR_Replace_FileUploadDAO pr_Replace_FileUploadDAO = new PR_Replace_FileUploadDAOSQLImpl();
            return (PR_Replace_FileUploadDAO)pr_Replace_FileUploadDAO;
        }

        public static PR_SupportiveDocumentDAO CreatePR_SupportiveDocumentDAO()
        {
            PR_SupportiveDocumentDAO pR_SupportiveDocumentDAO = new PR_SupportiveDocumentDAOSQLImpl();
            return (PR_SupportiveDocumentDAO)pR_SupportiveDocumentDAO;
        }

        public static PrAdditionalColumnDAO CreatePrAdditionalColumnDAO()
        {
            PrAdditionalColumnDAO prAdditionalColumnDAO = new PrAdditionalColumnDAOSQLImpl();
            return (PrAdditionalColumnDAO)prAdditionalColumnDAO;
        }

        public static PrCompanyPrTypeMappingDAO CreatePrCompanyPrTypeMappingDAO()
        {
            PrCompanyPrTypeMappingDAO prCompanyPrTypeMappingDAO = new PrCompanyPrTypeMappingDAOSQLImpl();
            return (PrCompanyPrTypeMappingDAO)prCompanyPrTypeMappingDAO;
        }

        public static PrTypeDAO CreatePrTypeDAO()
        {
            PrTypeDAO prTypeDAO = new PrTypeDAOSQLImpl();
            return (PrTypeDAO)prTypeDAO;
        }

        public static QuotationImageDAO createQuotationImageDAO()
        {
            QuotationImageDAO quotationImageDAO = new QuotationImageDAOSQLImpl();
            return (QuotationImageDAO)quotationImageDAO;
        }

        public static RoleFunctionDAO CreateRoleFunctionDAO()
        {
            RoleFunctionDAO roleFunctionDAO = new RoleFunctionDAOSQLImpl();
            return (RoleFunctionDAO)roleFunctionDAO;
        }

        public static SuperAdminDAO createSuperAdminDAO()
        {
            SuperAdminDAO superAdminDAO = new SuperAdminDAOSQLImpl();
            return (SuperAdminDAO)superAdminDAO;
        }

        public static SuplierImageUploadDAO createSuplierImageUploadDAO()
        {
            SuplierImageUploadDAO suplierImageUploadDAO = new SuplierImageUploadDAOSQLImpl();
            return (SuplierImageUploadDAO)suplierImageUploadDAO;
        }

        public static SupplierAssigneToCompanyDAO createSupplierAssigneToCompanyDAO()
        {
            SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = new SupplierAssigneToCompanyDAOSQLImpl();
            return (SupplierAssigneToCompanyDAO)supplierAssigneToCompanyDAO;
        }

        public static SupplierBiddingFileUploadDAO CreateSupplierBiddingFileUploadDAO()
        {
            SupplierBiddingFileUploadDAO supplierBiddingFileUploadDAO = new SupplierBiddingFileUploadDAOSQLImpl();
            return (SupplierBiddingFileUploadDAO)supplierBiddingFileUploadDAO;
        }

        public static SupplierBOMDAO CreateSupplierBOMDAO()
        {
            SupplierBOMDAO supplierBOMDAO = new SupplierBOMDAOSQLImpl();
            return (SupplierBOMDAO)supplierBOMDAO;
        }

        public static SupplierCategoryDAO createSupplierCategoryDAO()
        {
            SupplierCategoryDAO supplierCategoryDAO = new SupplierCategoryDAOSQLImpl();
            return (SupplierCategoryDAO)supplierCategoryDAO;
        }

        public static SupplierDAO createSupplierDAO()
        {
            SupplierDAO supplierDAO = new SupplierDAOSQLImpl();
            return (SupplierDAO)supplierDAO;
        }

        public static SupplierLoginDAO createSupplierLoginDAO()
        {
            SupplierLoginDAO supplierLoginDAO = new SupplierLoginDAOSQLImpl();
            return (SupplierLoginDAO)supplierLoginDAO;
        }

        public static SupplierOutbxDAO createSupplierOutbxDAO()
        {
            SupplierOutbxDAO supplierOutbxDAO = new SupplierOutbxDAOSQLImpl();
            return (SupplierOutbxDAO)supplierOutbxDAO;
        }

        public static SupplierQuotationDAO createSupplierQuotationDAO()
        {
            SupplierQuotationDAO supplierQuotationDAO = new SupplierQuotationDAOSQLImpl();
            return (SupplierQuotationDAO)supplierQuotationDAO;
        }

        public static SupplierRatingDAO createSupplierRatingDAO()
        {
            SupplierRatingDAO supplierRatingDAO = new SupplierRatingDAOSQLImpl();
            return (SupplierRatingDAO)supplierRatingDAO;
        }

        public static SystemDivisionDAO CreateSystemDivisionDAO()
        {
            SystemDivisionDAO systemDivisionDAO = new SystemDivisionDAOSQLImpl();
            return (SystemDivisionDAO)systemDivisionDAO;
        }

        public static TempBOMDAO CreateTempBOMDAO()
        {
            TempBOMDAO tempBOMDAO = new TempBOMDAOSQLImpl();
            return (TempBOMDAO)tempBOMDAO;
        }

        public static TempPR_FileUploadReplacementDAO CreateTempPR_FileUploadReplacementDAO()
        {
            TempPR_FileUploadReplacementDAO tempPR_FileUploadReplacementDAO = new TempPR_FileUploadReplacementDAOSQLImpl();
            return (TempPR_FileUploadReplacementDAO)tempPR_FileUploadReplacementDAO;
        }

        public static TempPR_SupportiveDocumentDAO CreateTempPR_SupportiveDocumentDAO()
        {
            TempPR_SupportiveDocumentDAO tempPR_SupportiveDocumentDAO = new TempPR_SupportiveDocumentDAOSQLImpl();
            return (TempPR_SupportiveDocumentDAO)tempPR_SupportiveDocumentDAO;
        }

        public static TempPrFileUploadDAO CreateTempPrFileUploadDAO()
        {
            TempPrFileUploadDAO tempPrFileUploadDAO = new TempPrFileUploadDAOSQLImpl();
            return (TempPrFileUploadDAO)tempPrFileUploadDAO;
        }

        public static UserRoleDAO CreateUserRoleDAO()
        {
            UserRoleDAO userRoleDAO = new UserRoleDAOSQLImp();
            return (UserRoleDAO)userRoleDAO;
        }


        public static UnitMeasurementDAO CreateUnitMeasurementDAO()
        {
            UnitMeasurementDAO unitMeasurementDAO = new UnitMeasurementDAOSQLImpl();
            return (UnitMeasurementDAO)unitMeasurementDAO;
        }

        public static MRNDetailsStatusLogDAO CreateMRNDetailsStatusLogDAO()
        {
            MRNDetailsStatusLogDAO DAO = new MRNDetailsStatusLogDAOImpl();
            return DAO;
        }

        public static MRNStockDepartmentDAO CreateMRNStockDepartmentDAO()
        {
            MRNStockDepartmentDAO DAO = new MRNStockDepartmentDAOImpl();
            return DAO;
        }

        //--------------------------Postgers SQL implmenttation DAO class objects
        //public static ItemCategoryDAO CreateItemCategoryDAO()
        //{
        //    ItemCategoryDAO itemCategoryDAO = new ItemCategoryDAOImpl();
        //    return (ItemCategoryDAO)itemCategoryDAO;
        //}

        //public static ItemSubCategoryDAO CreateItemSubCategoryDAO()
        //{
        //    ItemSubCategoryDAO itemSubCategoryDAO = new ItemSubCategoryDAOImpl();
        //    return (ItemSubCategoryDAO)itemSubCategoryDAO;
        //}

        //public static ItemImageUploadDAO CreateItemImageUploadDAO()
        //{
        //    ItemImageUploadDAO itemImageUploadDAO = new ItemImageUploadDAOImpl();
        //    return (ItemImageUploadDAO)itemImageUploadDAO;
        //}

        //public static AddItemDAO CreateAddItemDAO()
        //{
        //    AddItemDAO addItemDAO = new AddItemDAOImpl();
        //    return (AddItemDAO)addItemDAO;
        //}

        //public static GeneralSettingsDAO CreateGeneralSettingsDAO()
        //{
        //    GeneralSettingsDAO generalSettingsDAO = new GeneralSettingsDAOImpl();
        //    return (GeneralSettingsDAO)generalSettingsDAO;
        //}

        //public static PR_MasterDAO CreatePR_MasterDAO()
        //{
        //    PR_MasterDAO pr_MasterDAO = new PR_MasterDAOImpl();
        //    return (PR_MasterDAO)pr_MasterDAO;
        //}

        //public static PR_DetailDAO CreatePR_DetailDAO()
        //{
        //    PR_DetailDAO pr_DetailDAO = new PR_DetailDAOImpl();
        //    return (PR_DetailDAO)pr_DetailDAO;
        //}

        //public static PR_BillOfMeterialDAO CreatePR_BillOfMeterialDAO()
        //{
        //    PR_BillOfMeterialDAO pr_BillOfMeterialDAO = new PR_BillOfMeterialDAOImpl();
        //    return (PR_BillOfMeterialDAO)pr_BillOfMeterialDAO;
        //}

        //public static TempBOMDAO CreateTempBOMDAO()
        //{
        //    TempBOMDAO tempBOMDAO = new TempBOMDAOImpl();
        //    return (TempBOMDAO)tempBOMDAO;
        //}

        //public static TempPrFileUploadDAO CreateTempPrFileUploadDAO()
        //{
        //    TempPrFileUploadDAO tempPrFileUploadDAO = new TempPrFileUploadDAOImpl();
        //    return (TempPrFileUploadDAO)tempPrFileUploadDAO;
        //}

        //public static PR_FileUploadDAO CreatePR_FileUploadDAO()
        //{
        //    PR_FileUploadDAO pr_FileUploadDAO = new PR_FileUploadDAOImpl();
        //    return (PR_FileUploadDAO)pr_FileUploadDAO;
        //}

        //public static SupplierCategoryDAO createSupplierCategoryDAO()
        //{
        //    SupplierCategoryDAO supplierCategoryDAO = new SupplierCategoryDAOImpl();
        //    return (SupplierCategoryDAO)supplierCategoryDAO;
        //}

        //public static CompanyDepartmentDAO createCompanyDepartment()
        //{
        //    CompanyDepartmentDAO companyDepartmentDAO = new CompanyDepartmentDAOImpl();
        //    return (CompanyDepartmentDAO)companyDepartmentDAO;
        //}

        //public static SuplierImageUploadDAO createSuplierImageUploadDAO()
        //{
        //    SuplierImageUploadDAO suplierImageUploadDAO = new SuplierImageUploadDAOImpl();
        //    return (SuplierImageUploadDAO)suplierImageUploadDAO;
        //}

        //public static SupplierDAO createSupplierDAO()
        //{
        //    SupplierDAO supplierDAO = new SupplierDAOImpl();
        //    return (SupplierDAO)supplierDAO;
        //}

        //public static SupplierOutbxDAO createSupplierOutbxDAO()
        //{
        //    SupplierOutbxDAO supplierOutbxDAO = new SupplierOutbxDAOImpl();
        //    return (SupplierOutbxDAO)supplierOutbxDAO;
        //}

        //public static SupplierRatingDAO createSupplierRatingDAO()
        //{
        //    SupplierRatingDAO supplierRatingDAO = new SupplierRatingDAOImpl();
        //    return (SupplierRatingDAO)supplierRatingDAO;
        //}

        //public static SupplierLoginDAO createSupplierLoginDAO()
        //{
        //    SupplierLoginDAO supplierLoginDAO = new SupplierLoginDAOImpl();
        //    return (SupplierLoginDAO)supplierLoginDAO;
        //}

        //public static SupplierAssigneToCompanyDAO createSupplierAssigneToCompanyDAO()
        //{
        //    SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = new SupplierAssigneToCompanyDAOImpl();
        //    return (SupplierAssigneToCompanyDAO)supplierAssigneToCompanyDAO;
        //}

        //public static CompanyLoginDAO createCompanyLoginDAO()
        //{
        //    CompanyLoginDAO companyLoginDAO = new CompanyLoginDAOImpl();
        //    return (CompanyLoginDAO)companyLoginDAO;
        //}

        //public static SuperAdminDAO createSuperAdminDAO()
        //{
        //    SuperAdminDAO superAdminDAO = new SuperAdminDAOImpl();
        //    return (SuperAdminDAO)superAdminDAO;
        //}

        //public static BiddingDAO CreateBiddingDAO()
        //{
        //    BiddingDAO biddingDAO = new BiddingDAOImpl();
        //    return (BiddingDAO)biddingDAO;
        //}

        //public static SupplierQuotationDAO createSupplierQuotationDAO()
        //{
        //    SupplierQuotationDAO supplierQuotationDAO = new SupplierQuotationDAOImpl();
        //    return (SupplierQuotationDAO)supplierQuotationDAO;
        //}

        //public static QuotationImageDAO createQuotationImageDAO()
        //{
        //    QuotationImageDAO quotationImageDAO = new QuotationImageDAOImpl();
        //    return (QuotationImageDAO)quotationImageDAO;
        //}

        //public static POMasterDAO createPOMasterDAO()
        //{
        //    POMasterDAO pOMasterDAO = new POMasterDAOImpl();
        //    return (POMasterDAO)pOMasterDAO;
        //}

        //public static PODetailsDAO createPODetailsDAO()
        //{
        //    PODetailsDAO pODetailsDAO = new PODetailsDAOImpl();
        //    return (PODetailsDAO)pODetailsDAO;
        //}

        //public static GrnDAO createGrnDAO()
        //{
        //    GrnDAO grnDAO = new GrnDAOImpl();
        //    return (GrnDAO)grnDAO;
        //}

        //public static GRNDetailsDAO createGRNDetailsDAO()
        //{
        //    GRNDetailsDAO gRNDetailsDAO = new GRNDetailsDAOImpl();
        //    return (GRNDetailsDAO)gRNDetailsDAO;
        //}

        //public static SupplierBOMDAO CreateSupplierBOMDAO()
        //{
        //    SupplierBOMDAO supplierBOMDAO = new SupplierBOMDAOImpl();
        //    return (SupplierBOMDAO)supplierBOMDAO;
        //}

        //public static SupplierBiddingFileUploadDAO CreateSupplierBiddingFileUploadDAO()
        //{
        //    SupplierBiddingFileUploadDAO supplierBiddingFileUploadDAO = new SupplierBiddingFileUploadDAOImpl();
        //    return (SupplierBiddingFileUploadDAO)supplierBiddingFileUploadDAO;
        //}

        //public static NaturseOfBusinessDAO CreateNaturseOfBusinessDAO()
        //{
        //    NaturseOfBusinessDAO naturseOfBusinessDAO = new NaturseOfBusinessDAOImpl();
        //    return (NaturseOfBusinessDAO)naturseOfBusinessDAO;
        //}

        //public static FunctionActionDAO CreateFunctionActionDAO()
        //{
        //    FunctionActionDAO functionActionDAO = new FunctionActionDAOImpl();
        //    return (FunctionActionDAO)functionActionDAO;
        //}

        //public static RoleFunctionDAO CreateRoleFunctionDAO()
        //{
        //    RoleFunctionDAO roleFunctionDAO = new RoleFunctionDAOImpl();
        //    return (RoleFunctionDAO)roleFunctionDAO;
        //}

        //public static SystemDivisionDAO CreateSystemDivisionDAO()
        //{
        //    SystemDivisionDAO systemDivisionDAO = new SystemDivisionDAOImpl();
        //    return (SystemDivisionDAO)systemDivisionDAO;
        //}

        //public static UserRoleDAO CreateUserRoleDAO()
        //{
        //    UserRoleDAO userRoleDAO = new UserRoleDAOImp();
        //    return (UserRoleDAO)userRoleDAO;
        //}

        //public static CompanyUserAccessDAO CreateCompanyUserAccessDAO()
        //{
        //    CompanyUserAccessDAO companyUserAccessDAO = new CompanyUserAccessDAOImpl();
        //    return (CompanyUserAccessDAO)companyUserAccessDAO;
        //}

        //public static PR_Replace_FileUploadDAO CreatePR_Replace_FileUploadDAO()
        //{
        //    PR_Replace_FileUploadDAO pr_Replace_FileUploadDAO = new PR_Replace_FileUploadDAOImpl();
        //    return (PR_Replace_FileUploadDAO)pr_Replace_FileUploadDAO;
        //}

        //public static TempPR_FileUploadReplacementDAO CreateTempPR_FileUploadReplacementDAO()
        //{
        //    TempPR_FileUploadReplacementDAO tempPR_FileUploadReplacementDAO = new TempPR_FileUploadReplacementDAOImpl();
        //    return (TempPR_FileUploadReplacementDAO)tempPR_FileUploadReplacementDAO;
        //}

        //public static PrTypeDAO CreatePrTypeDAO()
        //{
        //    PrTypeDAO prTypeDAO = new PrTypeDAOImpl();
        //    return (PrTypeDAO)prTypeDAO;
        //}

        //public static PrAdditionalColumnDAO CreatePrAdditionalColumnDAO()
        //{
        //    PrAdditionalColumnDAO prAdditionalColumnDAO = new PrAdditionalColumnDAOImpl();
        //    return (PrAdditionalColumnDAO)prAdditionalColumnDAO;
        //}

        //public static PrCompanyPrTypeMappingDAO CreatePrCompanyPrTypeMappingDAO()
        //{
        //    PrCompanyPrTypeMappingDAO prCompanyPrTypeMappingDAO = new PrCompanyPrTypeMappingDAOImpl();
        //    return (PrCompanyPrTypeMappingDAO)prCompanyPrTypeMappingDAO;
        //}

        //public static BidHistoryDAO CreateBidHistoryDAO()
        //{
        //    BidHistoryDAO bidHistoryDAO = new BidHistoryDAOImpl();
        //    return (BidHistoryDAO)bidHistoryDAO;
        //}

        //public static PR_SupportiveDocumentDAO CreatePR_SupportiveDocumentDAO()
        //{
        //    PR_SupportiveDocumentDAO pR_SupportiveDocumentDAO = new PR_SupportiveDocumentDAOImpl();
        //    return (PR_SupportiveDocumentDAO)pR_SupportiveDocumentDAO;
        //}

        //public static TempPR_SupportiveDocumentDAO CreateTempPR_SupportiveDocumentDAO()
        //{
        //    TempPR_SupportiveDocumentDAO tempPR_SupportiveDocumentDAO = new TempPR_SupportiveDocumentDAOImpl();
        //    return (TempPR_SupportiveDocumentDAO)tempPR_SupportiveDocumentDAO;
        //}

        //public static PR_DetailHistoryDAO CreatePR_DetailHistoryDAO()
        //{
        //    PR_DetailHistoryDAO pR_DetailHistoryDAO = new PR_DetailHistoryDAOImpl();
        //    return (PR_DetailHistoryDAO)pR_DetailHistoryDAO;
        //}

        public static ItemCategoryMasterDAO CreateItemCategoryMasterDAO()
        {
            ItemCategoryMasterDAO itemCategoryMasterDAO = new ItemCategoryMasterDAOSQLImpl();
            return (ItemCategoryMasterDAO)itemCategoryMasterDAO;
        }

        public static ItemSubCategoryMasterDAO CreateItemSubCategoryMasterDAO()
        {
            ItemSubCategoryMasterDAO itemSubCategoryMasterDAO = new ItemSubCategoryMasterDAOSQLImpl();
            return (ItemSubCategoryMasterDAO)itemSubCategoryMasterDAO;
        }

        public static AddItemMasterDAO CreateAddItemMasterDAO()
        {
            AddItemMasterDAO addItemMasterDAO = new AddItemMasterDAOSQLImpl();
            return (AddItemMasterDAO)addItemMasterDAO;

        }
        public static AddItemBOMDAO CreateAddItemBOMDAO()
        {
            AddItemBOMDAO addItemBOMDAO = new AddItemBOMDAOImpl();
            return (AddItemBOMDAO)addItemBOMDAO;
        }

        internal static DesignationDAO CreateDesignationDAO()
        {
            DesignationDAO designationDAO = new DesignationDAOImpl();
            return (DesignationDAO)designationDAO;
        }

        internal static HScodeDAO CreateHScodeDAO()
        {
            HScodeDAO hscodeDAO = new HScodeDAOImpl();
            return (HScodeDAO)hscodeDAO;
        }

        public static ItemCategoryApprovalDAO CreateItemCategoryApprovalDAO()
        {
            ItemCategoryApprovalDAO itemCategoryApprovalDAO = new ItemCategoryApprovalDAOImpl();
            return (ItemCategoryApprovalDAO)itemCategoryApprovalDAO;
        }

        public static CommitteeDAO CreateCommitteeDAO()
        {
            CommitteeDAO committeedDAO = new CommitteeDAOImpl();
            return (CommitteeDAO)committeedDAO;
        }
        internal static MRN_MasterDAO CreateMRN_MasterDAO()
        {
            MRN_MasterDAO mrn_MasterDAO = new MRN_MasterDAOImpl();
            return (MRN_MasterDAO)mrn_MasterDAO;
        }


        internal static MRexpenseDAO CreateMRexpenseDAO()
        {
            MRexpenseDAO mrexpenseDAO = new MRexpenseDAOImpl();
            return (MRexpenseDAO)mrexpenseDAO;
        }

        internal static MRNDetailDAO CreateMRNDetailDAO()
        {
            MRNDetailDAO mrnDetailDAO = new MRNDetailDAOImpl();
            return (MRNDetailDAO)mrnDetailDAO;
        }

        internal static MRNBomDAO CreateMRNBomDAO()
        {
            MRNBomDAO mrnbomDAO = new MRNBomDAOImpl();
            return (MRNBomDAO)mrnbomDAO;
        }

        internal static MRNFileUploadDAO CreateMRNFileUploadDAO()
        {
            MRNFileUploadDAO mrnFileUploadDAO = new MRNFileUploadDAOImpl();
            return (MRNFileUploadDAO)mrnFileUploadDAO;
        }

        internal static MRsupportiveDocumentDAO CreateMRsupportiveDocumentDAO()
        {
            MRsupportiveDocumentDAO mrsupportiveDocumentDAO = new MRsupportiveDocumentDAOImpl();
            return (MRsupportiveDocumentDAO)mrsupportiveDocumentDAO;
        }

        internal static TempMRN_BOM_DAO CreateTempMRN_BOM_DAO()
        {
            TempMRN_BOM_DAO tempMRN_BOM_DAO = new TempMRN_BOM_DAOImpl();
            return (TempMRN_BOM_DAO)tempMRN_BOM_DAO;
        }

        internal static TempMRN_FileUploadDAO CreateTempMRN_FileUploadDAO()
        {
            TempMRN_FileUploadDAO tmpMRN_FileUploadDAO = new TempMRN_FileUploadDAOImpl();
            return (TempMRN_FileUploadDAO)tmpMRN_FileUploadDAO;
        }

        internal static TempMRN_FileUploadReplacementDAO CreateTempMRN_FileUploadReplacementDAO()
        {
            TempMRN_FileUploadReplacementDAO tempMRN_FileUploadReplacementDAO = new TempMRN_FileUploadReplacementDAOImpl();
            return (TempMRN_FileUploadReplacementDAO)tempMRN_FileUploadReplacementDAO;
        }

        internal static TempMRN_SupportiveDocumentDAO CreateTempMRN_SupportiveDocumentDAO()
        {
            TempMRN_SupportiveDocumentDAO tempMRN_SupportiveDocumentDAO = new TempMRN_SupportiveDocumentDAOImpl();
            return (TempMRN_SupportiveDocumentDAO)tempMRN_SupportiveDocumentDAO;
        }

        internal static BiddingMethodDAO CreateBiddingMethodDAO()
        {
            BiddingMethodDAO biddingMethodDAO = new BiddingMethodDAOImpl();
            return (BiddingMethodDAO)biddingMethodDAO;
        }


        public static SupplierBidBondDetailsDAO CreateSupplierBidBondDetailsDAO()
        {
            SupplierBidBondDetailsDAO supplierBidBondDetailsDAO = new SupplierBidBondDetailsDAOImpl();
            return (SupplierBidBondDetailsDAO)supplierBidBondDetailsDAO;
        }

        internal static Procument_Plan_Type_DAO createProcument_Plan_Type_DAO()
        {
            Procument_Plan_Type_DAO procument_Plan_Type_DAO = new Procument_Plan_Type_DAOImpl();
            return (Procument_Plan_Type_DAO)procument_Plan_Type_DAO;
        }
        internal static BiddingPlanDAO createBiddingPlanDAO()
        {
            BiddingPlanDAO biddingPlanDAO = new BiddingPlanDAOImpl();
            return (BiddingPlanDAO)biddingPlanDAO;
        }
        internal static Bid_Bond_Details_DAO createBid_Bond_Details_DAO()
        {
            Bid_Bond_Details_DAO bid_Bond_Details_DAO = new Bid_Bond_Details_DAOImpl();
            return (Bid_Bond_Details_DAO)bid_Bond_Details_DAO;
        }

        internal static Mrn_Replace_File_Upload_DAO CreateMrn_Replace_File_Upload_DAO()
        {
            Mrn_Replace_File_Upload_DAO mrn_Replace_File_Upload_DAO = new Mrn_Replace_File_Upload_DAOImpl();
            return (Mrn_Replace_File_Upload_DAO)mrn_Replace_File_Upload_DAO;
        }
        public static EmailDAO CreateEmailDAO()
        {
            EmailDAO DAO = new EmailDAOSQLImpl();
            return DAO;
        }

        public static IMeasurementDetailDAO CreateMeasurementDetailDAO()
        {
            IMeasurementDetailDAO DAO = new MeasurementDetailDAO();
            return DAO;
        }

        public static IMeasurementMasterDAO CreateMeasurementMasterDAO()
        {
            IMeasurementMasterDAO DAO = new MeasurementMasterDAO();
            return DAO;
        }

        public static IConversionTableMasterDAO CreateConversionTableMasterDAO()
        {
            IConversionTableMasterDAO DAO = new ConversionTableMasterDAO();
            return DAO;
        }
        internal static WarehouseInventoryBatchesDAO CreateWarehouseInventoryBatchesDAO()
        {
            WarehouseInventoryBatchesDAO DAO = new WarehouseInventoryBatchesDAOImpl();
            return DAO;
        }

        public static IConversionDAO CreateConversionDAO()
        {
            IConversionDAO DAO = new ConversionDAO();
            return DAO;
        }


        public static IItemMeasurementDAO CreateItemMeasurementDAO()
        {
            IItemMeasurementDAO DAO = new ItemMeasurementDAO();
            return DAO;
        }
        public static StockOverrideLogDAO CreateStockOverrideLogDAO()
        {
            StockOverrideLogDAO DAO = new StockOverrideLogDAODAOSQLImpl();
            return DAO;
        }

        public static GrnFilesDAO CreateGrnFilesDAO()
        {
            GrnFilesDAO DAO = new GrnFilesDAOSQLImpl();
            return DAO;
        }

        public static DefCurrencyTypeDAO CreateDefCurrencyTypeDAO()
        {
            DefCurrencyTypeDAO defCurrencyTypeDAO = new DefCurrencyTypeDAOSQLImpl();
            return (DefCurrencyTypeDAO)defCurrencyTypeDAO;
        }

        public static DefPaymentModeDAO CreateDefPaymentModeDAO()
        {
            DefPaymentModeDAO defPaymentModeDAO = new DefPaymentModeDAOSQLImpl();
            return (DefPaymentModeDAO)defPaymentModeDAO;
        }

        public static DefPriceTermsDAO CreateDefPriceTermsDAO()
        {
            DefPriceTermsDAO defPriceTermsDAO = new DefPriceTermsDAOSQLImpl();
            return (DefPriceTermsDAO)defPriceTermsDAO;
        }

        public static DefTransportModeDAO CreateDefTransportModeDAO()
        {
            DefTransportModeDAO defTransportModeDAO = new DefTransportModeDAOSQLImpl();
            return (DefTransportModeDAO)defTransportModeDAO;
        }

        public static DefContainerSizeDAO CreateDefContainerSizeDAO()
        {
            DefContainerSizeDAO defContainerSizeDAO = new DefContainerSizeDAOSQLImpl();
            return (DefContainerSizeDAO)defContainerSizeDAO;
        }

        public static CurrencyRateDAO CreateCurrencyRateDAO()
        {
            CurrencyRateDAO currencyRateDAO = new CurrencyRateDAOSQLImpl();
            return (CurrencyRateDAO)currencyRateDAO;
        }

        public static TRDetailsDAO CreateTRDetailsDAO()
        {
            TRDetailsDAO tRDetailsDAO = new TRDetailsSQLImpl();
            return (TRDetailsDAO)tRDetailsDAO;
        }

        public static TRMasterDAO CreateTRMasterDAO()
        {
            TRMasterDAO tRMasterDAO = new TRMasterSQLImpl();
            return (TRMasterDAO)tRMasterDAO;
        }

        public static TRDIssueNoteDAO CreateTRDIssueNoteDAO()
        {
            TRDIssueNoteDAO tRDIssueNoteDAO = new TRDIssueNoteSQLImpl();
            return (TRDIssueNoteDAO)tRDIssueNoteDAO;
        }

        public static TrDetailStatusLogDAO CreateTrDetailStatusLogDAO()
        {
            TrDetailStatusLogDAO DAO = new TrDetailStatusLogDAOSQLImpl();
            return DAO;
        }

        public static TrdIssueNoteBatchesDAO CreateTrdIssueNoteBatchesDAO()
        {
            TrdIssueNoteBatchesDAO trdIssueNoteBatchesDAO = new TrdIssueNoteBatchesDAOSQLImpl();
            return (TrdIssueNoteBatchesDAO)trdIssueNoteBatchesDAO;
        }

        public static ImportsHistoryDAO CreateImportsHistoryDAO()
        {
            ImportsHistoryDAO importsHistoryDAO = new ImportsHistoryDAOSQLImpl();
            return (ImportsHistoryDAO)importsHistoryDAO;
        }

        public static ImportsDAO createImportsDAO()
        {
            ImportsDAO DAO = new ImportsDAOImp();
            return DAO;
        }

        public static AfterPO_DAO createAfterPO_DAO()
        {
            AfterPO_DAO DAO = new AfterPO_DAOImp();
            return DAO;
        }

        public static MrndIssueNoteBatchesDAO CreateMrndIssueNoteBatchesDAO()
        {
            MrndIssueNoteBatchesDAO mrndIssueNoteBatchesDAO = new MrndIssueNoteBatchesDAOSQLImpl();
            return (MrndIssueNoteBatchesDAO)mrndIssueNoteBatchesDAO;
        }

        public static StockOverrideBatchLogDAO CreateStockOverrideBatchLogDAO()
        {
            StockOverrideBatchLogDAO stockOverrideBatchLogDAO = new StockOverrideBatchLogDAOImpl();
            return (StockOverrideBatchLogDAO)stockOverrideBatchLogDAO;
        }

        public static InvoiceDetailsDAO CreateInvoiceDetailsDAO()
        {
            InvoiceDetailsDAO invoiceDetailsDAO = new InvoiceDetailsDAOImpl();
            return (InvoiceDetailsDAO)invoiceDetailsDAO;
        }

        public static CompanyTypeDAO CreateCompanyTypeDAO()
        {
            CompanyTypeDAO companyTypeDAO = new CompanyTypeDAOSQLImpl();
            return (CompanyTypeDAO)companyTypeDAO;
        }

        public static SupplierTypeDAO CreateSupplierTypeDAO()
        {
            SupplierTypeDAO supplierTypeDAO = new SupplierTypeDAOSQLImpl();
            return (SupplierTypeDAO)supplierTypeDAO;
        }

        public static GrndReturnNoteDAO CreateGrndReturnNoteDAO()
        {
            GrndReturnNoteDAO grndReturnNoteDAO = new GrndReturnNoteDAOImpl();
            return (GrndReturnNoteDAO)grndReturnNoteDAO;
        }

        public static FollowUpRemarksDAO CreateFollowUpRemarksDAO()
        {
            FollowUpRemarksDAO followUpRemarksDAO = new FollowUpRemarksDAOImpl();
            return (FollowUpRemarksDAO)followUpRemarksDAO;
        }

        public static DutyRatesDAO CreateDutyRatesDAO()
        {
            DutyRatesDAO dutyRatesDAO = new DutyRatesDAOImpl();
            return (DutyRatesDAO)dutyRatesDAO;
        }

        public static InvoiceImagesDAO CreateInvoiceImagesDAO()
        {
            InvoiceImagesDAO invoiceImagesDAO = new InvoiceImagesDAOSQLImpl();
            return (InvoiceImagesDAO)invoiceImagesDAO;
        }

        public static GrnReturnDetailsDAO CreateGrnReturnDetailsDAO()
        {
            GrnReturnDetailsDAO grnReturnDetailsDAO = new GrnReturnDetailsDAOImpl();
            return (GrnReturnDetailsDAO)grnReturnDetailsDAO;
        }

        public static GrnReturnMasterDAO CreateGrnReturnMasterDAO()
        {
            GrnReturnMasterDAO grnReturnMasterDAO = new GrnReturnMasterDAOImpl();
            return (GrnReturnMasterDAO)grnReturnMasterDAO;
        }

        public static DepartmentReturnDAO CreateDepartmentReturnDAO()
        {
            DepartmentReturnDAO departmentReturnDAO = new DepartmentReturnDAOImpl();
            return (DepartmentReturnDAO)departmentReturnDAO;
        }

        public static DepartmentReturnBatchDAO CreateDepartmentReturnBatchDAO()
        {
            DepartmentReturnBatchDAO departmentReturnBatchDAO = new DepartmentReturnBatchDAOSQLImpl();
            return (DepartmentReturnBatchDAO)departmentReturnBatchDAO;
        }

        public static TempCoveringPrDAO CreateTempCoveringPrDAO()
        {
            TempCoveringPrDAO tempCoveringPrDAO = new TempCoveringPrDAOSQLImpl();
            return (TempCoveringPrDAO)tempCoveringPrDAO;
        }

        public static TempQuotationForCoverigPrDAO CreateTempQuotationForCoverigPrDAO()
        {
            TempQuotationForCoverigPrDAO tempQuotationForCoverigPrDAO = new TempQuotationForCoverigPrDAOSQLImpl();
            return (TempQuotationForCoverigPrDAO)tempQuotationForCoverigPrDAO;
        }

        public static SupplierItemReportDAO CreatesupplierItemReportDAO()
        {
            SupplierItemReportDAO supplierItemReportDAO = new SupplierItemReportDAOImpl();
            return (SupplierItemReportDAO)supplierItemReportDAO;
        }


    }
}

