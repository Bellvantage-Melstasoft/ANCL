using CLibrary.Common;
using CLibrary.Domain;
using System.Collections.Generic;
using System.Text;
using System;
using System.Text.RegularExpressions;

namespace CLibrary.Infrastructure {
    public interface MrnMasterDAOV2 {
        List<int> SaveMrn(MrnMasterV2 mrn, DBConnection dbConnection);
        int UpdateMrn(MrnMasterV2 mrn, DBConnection dbConnection);
        MrnMasterV2 GetMrnForEditing(int MrnId, DBConnection dbConnection);
        int CloneMRN(int mrnId, int clonedBy, DBConnection dbConnection);
        List<MrnMasterV2> FetchMyMrnByBasicSearchByMonth(int createdBy, DateTime date, DBConnection dbConnection);
        MrnMasterV2 FetchMyMrnByBasicSearchByMrnCode(int createdBy, string mrnCode, DBConnection dbConnection);
        List<MrnMasterV2> FetchMyMrnByAdvanceSearch(int createdBy, List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate,string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId, DBConnection dbConnection);
        MrnMasterV2 GetMRNMasterToView(int mrnId, int companyId, DBConnection dbConnection);
        List<MrnDetailsV2> FetchMrnDetailsList(int mrnId, int companyId, DBConnection dbConnection);
        MrnDetailsV2 GetMrndTerminationDetails(int mrndId, DBConnection dbConnection);
        int TerminateMRN(int mrnID, int terminatedBy, string remarks, DBConnection dbConnection);
        List<MrnMasterV2> FetchMrnListforApproval(List<int> subDepartmentIds, DBConnection dbConnection);
        int ApproveOrRejectMrn(int expenseType, int mrnId, int isApproved, int isExpenseApproved, int userId, string remark, DBConnection dbConnection);
        List<MrnMasterV2> FetchAllMrnByBasicSearchByMonth(DateTime date, DBConnection dbConnection);
        MrnMasterV2 FetchAllMrnByBasicSearchByMrId(int mrnId, DBConnection dbConnection);
        List<MrnMasterV2> FetchAllMrnByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId, DBConnection dbConnection);
        List<MrnMasterV2> FetchMrnListForAvailabiltyExpenseApproval(DBConnection dbConnection);
        void UpdateMrnItemDepartmentStock(int mrnId, int mrndId, decimal departmentStock, DBConnection dbConnection);
        void ApproveOrRejectMRNExpense(int mrnId, int isApproved, string remark, int userId, DateTime approvedDate, DBConnection dbConnection);
        void UpdateMRNExpense(int mrnId, int expenseType ,int isBudget, decimal budgetAmount, string budgetRemark, string budgetInformation, int userId, DBConnection dbConnection);
        List<MrnMasterV2> FetchMrnByCompanyIdToAssignStoreKeeper(int companyId, DBConnection dbConnection);
        //List<MrnMasterV2> FetchAssignedMrnForStoreKeeper(int storekeeperId, DBConnection dbConnection);
        int UpdateStoreKeeperToMRN(int storeKeeperId, int MRNId, DBConnection dbConnection);
        List<MrnMasterV2> FetchMrnByWarehouseIdToAssignStoreKeeper(List<int> warehouseId, DBConnection dbConnection);
        int UpdateMrnStatus(int mrnId,int status, DBConnection dbConnection);
        List<MrnDetailsV2> FetchMrnDetailsListWithoutTerminated(int mrnId, int companyId, int WarehouseId, DBConnection dbConnection);
        string AddMRNtoPR(MrnMasterV2 mrnMaster, int userId, int companyId,out int PrId, DBConnection dbConnection);
        int UpdateMRNItemDepartmentStock(int mrnId, int mrndId, decimal departmentStock, DBConnection dbConnection);
        MrnMasterV2 getMrnMasterByMrId(int mrnId, DBConnection dbConnection);
        MrnMasterV2 FetchAllMrnByBasicSearchByMrnCode(string mrnId, DBConnection dbConnection);
        MrnMasterV2 FetchApprovedMrnByBasicSearchByMrnCode(string mrnId, List<int> warehouseId, DBConnection dbConnection);
        List<MrnMasterV2> FetchApprovedMrnByBasicSearchByMonth(DateTime date, List<int> warehouseId, DBConnection dbConnection);
        List<MrnMasterV2> FetchApprovedMrnByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId, DBConnection dbConnection);
        int updateMrnMasterAfterClone(int mrnId, DBConnection dbConnection);
        MrnMasterV2 FetchMrnForExpAppByBasicSearchByMrnCode(string mrnId, DBConnection dbConnection);
        List<MrnMasterV2> FetchMrnForExpAppByBasicSearchByMonth(DateTime date, DBConnection dbConnection);
        List<MrnMasterV2> FetchMrnForExpAppByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId, DBConnection dbConnection);
        MrnMasterV2 FetchAssignedMrnForStoreKeeperByMrnCode(int storekeeperId, string mrnId, DBConnection dbConnection);
        List<MrnMasterV2> FetchAssignedMrnForStoreKeeperByDate(int storekeeperId, DateTime date, DBConnection dbConnection);
        List<MrnMasterV2> FetchMrnByCompanyIdToAssignStoreKeeperByMrnCode(int companyId, string mrnCode, DBConnection dbConnection);
        List<MrnMasterV2> FetchMrnByCompanyIdToAssignStoreKeeperByDate(int companyId, DateTime date, DBConnection dbConnection);
        List<MrnMasterV2> FetchMrnListforApprovalByDate(List<int> subDepartmentIds, DateTime date, DBConnection dbConnection);
        List<MrnMasterV2> FetchMrnListforApprovalByMrnCode(List<int> subDepartmentIds, string MrnCode, DBConnection dbConnection);
        List<MrnMasterV2> FetchMRNByMRNCode(string MrnCode, int Companyid, DBConnection dbConnection);
        MrnMasterV2 GetMRNMasterToViewRequisitionReport(int mrnId, int companyId, DBConnection dbConnection);
        List<MrnMasterV2> FetchMyMrn(int createdBy, DBConnection dbConnection);
        List<MrnMasterV2> FetchMrnForExpApp(DBConnection dbConnection);
        List<MrnMasterV2> FetchAllMrnh(DBConnection dbConnection);
        List<MrnMasterV2> FetchApprovedMrnByBasicSearch(List<int> warehouseId, DBConnection dbConnection);
        List<MrnMasterV2> FetchAssignedMrnForStoreKeeper(int storekeeperId, DBConnection dbConnection);
    }

    class MrnMasterDAOV2Impl : MrnMasterDAOV2 {

        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public MrnMasterV2 GetMrnForEditing(int MrnId, DBConnection dbConnection) {

            string sql = "SELECT * FROM MRN_MASTER WHERE MRN_ID=" + MrnId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.GetSingleOject<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<int> SaveMrn(MrnMasterV2 mrn, DBConnection dbConnection) {
            List<int> MrnIdAndCode = new List<int>();

            StringBuilder sql = new StringBuilder();
            
            string getCode = mrn.PurchaseType == 1 ? "(SELECT CONCAT('LCL',COUNT(*)+1) FROM MRN_MASTER WHERE PURCHASE_TYPE=1 AND COMPANY_ID=" + mrn.CompanyId + ")" : "(SELECT CONCAT('IMP',COUNT(*)+1) FROM MRN_MASTER WHERE PURCHASE_TYPE=2 AND COMPANY_ID=" + mrn.CompanyId + ")";


            sql.Append("DECLARE @MRN_ID TABLE(ID INT) \n");
            sql.Append("DECLARE @MRN_CODE INT \n");
            sql.Append(" \n");
            //sql.Append("SELECT @MRN_CODE = ISNULL(MAX(MRN_CODE),0)+1 FROM MRN_MASTER WHERE COMPANY_ID=" + mrn.CompanyId + " AND PURCHASE_TYPE = "+mrn.PurchaseType+" \n");
            sql.Append("SELECT @MRN_CODE = (COUNT(*)+1) FROM MRN_MASTER WHERE COMPANY_ID=" + mrn.CompanyId + " AND PURCHASE_TYPE = " + mrn.PurchaseType + " \n");

            sql.Append(" \n");
            sql.Append("INSERT INTO [MRN_MASTER] \n");
            sql.Append("([MRN_CODE],[COMPANY_ID],[SUB_DEPARTMENT_ID],[WAREHOUSE_ID],[MRN_TYPE],[PURCHASE_TYPE],[EXPECTED_DATE],[PURCHASE_PROCEDURE],[REQUIRED_FOR],[MRN_CATEGORY_ID], \n");
            sql.Append("[MRN_SUB_CATEGORY_ID],[EXPENSE_TYPE],[EXPENSE_REMARKS],[IS_BUDGET],[BUDGET_AMOUNT],[BUDGET_INFO],[CREATED_BY],[CREATED_DATETIME],[IS_MRN_APPROVED], \n");
            sql.Append("[IS_EXPENSE_APPROVED],[IS_ACTIVE],[STATUS],[STORE_KEEPER_ID],[IMPORT_ITEM_TYPE]) \n");
            sql.Append("OUTPUT inserted.MRN_ID INTO @MRN_ID \n");
            sql.Append("VALUES \n");
            sql.Append("("+ getCode + "," + mrn.CompanyId + "," + mrn.SubDepartmentId + "," + mrn.WarehouseId + "," + mrn.MrnType + "," + mrn.PurchaseType + ",'" + mrn.ExpectedDate + "'," + mrn.PurchaseProcedure + ",'" + mrn.RequiredFor.ProcessString() + "'," + mrn.MrnCategoryId + ", \n");
            sql.Append("" + mrn.MrnSubCategoryId + "," + mrn.ExpenseType + ",'" + mrn.ExpenseRemarks.ProcessString() + "'," + mrn.ISBudget + "," + mrn.BudgetAmount + ",'" + mrn.BudgetInfo.ProcessString() + "'," + mrn.CreatedBy + ",'" + LocalTime.Now + "',0, \n");
            sql.Append("0,1,(SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='APR')," + mrn.StoreKeeperId + ", "+mrn.ImportItemType+") \n");
            sql.Append(" \n");
            sql.Append("SELECT * FROM @MRN_ID \n");
            sql.Append("UNION ALL \n");
            sql.Append("SELECT @MRN_CODE \n");

            


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                if (dbConnection.dr.HasRows) {
                    while (dbConnection.dr.Read()) {
                        MrnIdAndCode.Add(int.Parse(dbConnection.dr[0].ToString()));
                    }
                }
            }

            return MrnIdAndCode;
            
        }

        public int UpdateMrn(MrnMasterV2 mrn, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();

            dbConnection.cmd.CommandText = "SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='MDFD' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());


            sql.Append("UPDATE [MRN_MASTER] \n");
            sql.Append("   SET [MRN_TYPE] = " + mrn.MrnType + " \n");
            sql.Append("      ,[PURCHASE_TYPE] = " + mrn.PurchaseType + " \n");
            sql.Append("      ,[EXPECTED_DATE] = '" + mrn.ExpectedDate + "' \n");
            sql.Append("      ,[PURCHASE_PROCEDURE] = " + mrn.PurchaseProcedure + " \n");
            sql.Append("      ,[REQUIRED_FOR] = '" + mrn.RequiredFor.ProcessString() + "' \n");
            sql.Append("      ,[MRN_CATEGORY_ID] = " + mrn.MrnCategoryId + " \n");
            sql.Append("      ,[MRN_SUB_CATEGORY_ID] =" + mrn.MrnSubCategoryId + " \n");
            sql.Append("      ,[EXPENSE_TYPE] = " + mrn.ExpenseType + " \n");
            sql.Append("      ,[EXPENSE_REMARKS] = '" + mrn.ExpenseRemarks.ProcessString() + "' \n");
            sql.Append("      ,[IS_BUDGET] = " + mrn.ISBudget + " \n");
            sql.Append("      ,[BUDGET_AMOUNT] = " + mrn.BudgetAmount + " \n");
            sql.Append("      ,[BUDGET_INFO] = '" + mrn.BudgetInfo.ProcessString() + "' \n");
            sql.Append("      ,[STATUS] = '" + Status + "' \n");
            sql.Append("      ,[IMPORT_ITEM_TYPE] = " + mrn.ImportItemType + " \n");
            sql.Append(" WHERE MRN_ID=" + mrn.MrnId);

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateStoreKeeperToMRN(int storeKeeperId, int MRNId,  DBConnection dbConnection) {
            
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " UPDATE MRN_MASTER SET STORE_KEEPER_ID = " + storeKeeperId + " WHERE MRN_ID = " + MRNId + "; ";
            dbConnection.cmd.CommandText += " UPDATE MRN_DETAILS SET STATUS = (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='ISU_PROC') WHERE MRN_ID = " + MRNId + "; ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }


        public List<MrnMasterV2> FetchMyMrnByBasicSearchByMonth(int createdBy, DateTime date, DBConnection dbConnection)
        {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                "WHERE MRNM.CREATED_BY =" + createdBy + " AND MRNM.IS_ACTIVE=1 AND MONTH(MRNM.CREATED_DATETIME) =" + date.Month + " AND YEAR(MRNM.CREATED_DATETIME)=" + date.Year + " ORDER BY MRNM.CREATED_DATETIME ASC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchMyMrn(int createdBy, DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                "WHERE MRNM.CREATED_BY =" + createdBy + " AND MRNM.IS_ACTIVE=1 ORDER BY MRNM.CREATED_DATETIME ASC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchMRNByMRNCode(string MrnCode, int Companyid, DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "LEFT JOIN (SELECT PR_ID, PR_CODE, MRN_ID FROM PR_MASTER) AS PR ON PR.MRN_ID = MRNM.MRN_ID " +
                "WHERE MRNM.MRN_CODE = '" + MrnCode + "' AND COMPANY_ID ="+ Companyid + " ";
                
                using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchMrnByCompanyIdToAssignStoreKeeper(int companyId, DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRN_MASTER AS MRNM " +
                                            "INNER JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY = COLOG.USER_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION AS WAREHOUSE_NAME FROM WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID = SUBDEP.SUB_DEPARTMENT_ID " +
                                            "LEFT JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                                            "LEFT JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                                            "WHERE ((EXPENSE_TYPE = 1 AND IS_MRN_APPROVED = 1 AND IS_EXPENSE_APPROVED = 1) OR(EXPENSE_TYPE = 2 AND IS_MRN_APPROVED = 1))"+
                                            " AND IS_TERMINATED != 1 AND STORE_KEEPER_ID IS NULL AND COMPANY_ID = " + companyId + " ORDER BY MRNM.CREATED_DATETIME DESC ";
            
                using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchMrnByCompanyIdToAssignStoreKeeperByDate(int companyId, DateTime date, DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRN_MASTER AS MRNM " +
                                            "INNER JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY = COLOG.USER_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION AS WAREHOUSE_NAME FROM WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID = SUBDEP.SUB_DEPARTMENT_ID " +
                                            "LEFT JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                                            "LEFT JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                                            "WHERE ((EXPENSE_TYPE = 1 AND IS_MRN_APPROVED = 1 AND IS_EXPENSE_APPROVED = 1) OR(EXPENSE_TYPE = 2 AND IS_MRN_APPROVED = 1))" +
                                            " AND IS_TERMINATED != 1 AND MONTH(MRNM.CREATED_DATETIME) =" + date.Month + " AND YEAR(MRNM.CREATED_DATETIME)=" + date.Year + " AND STORE_KEEPER_ID IS NULL AND COMPANY_ID = " + companyId + " ORDER BY MRNM.CREATED_DATETIME ASC";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchMrnByCompanyIdToAssignStoreKeeperByMrnCode(int companyId, string mrnCode,  DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRN_MASTER AS MRNM " +
                                            "INNER JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY = COLOG.USER_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION AS WAREHOUSE_NAME FROM WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID = SUBDEP.SUB_DEPARTMENT_ID " +
                                            "LEFT JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                                            "LEFT JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                                            "WHERE ((EXPENSE_TYPE = 1 AND IS_MRN_APPROVED = 1 AND IS_EXPENSE_APPROVED = 1) OR(EXPENSE_TYPE = 2 AND IS_MRN_APPROVED = 1))" +
                                            " AND IS_TERMINATED != 1 AND MRNM.MRN_CODE = '"+ mrnCode + "' AND STORE_KEEPER_ID IS NULL AND COMPANY_ID = " + companyId + " ";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchMrnByWarehouseIdToAssignStoreKeeper(List<int> warehouseId, DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRN_MASTER AS MRNM " +
                                            "INNER JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY = COLOG.USER_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION AS WAREHOUSE_NAME FROM WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID = SUBDEP.SUB_DEPARTMENT_ID " +
                                            "LEFT JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                                            "LEFT JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                                            "WHERE ((EXPENSE_TYPE =1 AND IS_MRN_APPROVED = 1 AND IS_EXPENSE_APPROVED = 1) OR (EXPENSE_TYPE = 2 AND IS_MRN_APPROVED = 1))  AND IS_TERMINATED != 1 AND MRNM.WAREHOUSE_ID IN (" + String.Join(",", warehouseId) + ") AND STORE_KEEPER_ID IS NULL ";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }
        
        public MrnMasterV2 FetchMyMrnByBasicSearchByMrnCode(int createdBy, string mrnCode, DBConnection dbConnection)
        {
            //var regex = new Regex("MRN", RegexOptions.IgnoreCase);
            string code = mrnCode.Replace(mrnCode, " "+mrnCode);
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                "WHERE MRNM.CREATED_BY =" + createdBy + " AND MRNM.IS_ACTIVE=1 AND MRNM.MRN_CODE  ='" + mrnCode + "'  ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchMyMrnByAdvanceSearch(int createdBy, List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate,
            string expectedFromDate,string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            string sql = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                         "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                         "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                         "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                         "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                         "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                         "WHERE MRNM.CREATED_BY =" + createdBy + " AND MRNM.IS_ACTIVE=1  ";


            if (departmentIds.Count > 0)
            {
                string departments = string.Join<int>(",", departmentIds);
                sql += " AND MRNM.SUB_DEPARTMENT_ID  IN ( " + departments + " ) ";
            }

            if (wareHouseIds.Count > 0)
            {
                string wareHouses = string.Join<int>(",", wareHouseIds);
                sql += " AND MRNM.WAREHOUSE_ID IN ( " + wareHouses + " ) ";
            }
            if (purchaseType != 0)
            {
                sql += " AND MRNM.PURCHASE_TYPE =  " + purchaseType + "";
            }
            if (purchaseProcedure != 0)
            {
                sql += " AND MRNM.PURCHASE_PROCEDURE =  " + purchaseProcedure + "";
            }
            if (createdFromDate != "" && createdToDate !="")
            {
                sql += " AND MRNM.CREATED_DATETIME  BETWEEN '"+ Convert.ToDateTime(createdFromDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(createdToDate).ToString("yyyy-MM-dd") + "'";
            }
            if (expectedFromDate !="" && expectedToDate != "")
            {
                sql += " AND MRNM.EXPECTED_DATE BETWEEN  '" + Convert.ToDateTime(expectedFromDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(expectedToDate).ToString("yyyy-MM-dd") + "'";
            }
            if (expenseType != 0)
            {
                sql += " AND MRNM.EXPENSE_TYPE =  " + expenseType + "";
            }
            if (mrnType != 0)
            {
                sql += " AND MRNM.MRN_TYPE =  " + mrnType + "";
            }
            if (mainCatergoryId != 0 && subCatergoryId !=0)
            {
                sql += " AND MRNM.MRN_CATEGORY_ID =  " + mainCatergoryId + " AND MRNM.MRN_SUB_CATEGORY_ID =  " + subCatergoryId + "";
            }
            sql += " ORDER BY  MRNM.CREATED_DATETIME ASC";

            dbConnection.cmd.CommandText = sql;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public MrnMasterV2 GetMRNMasterToView(int mrnId, int companyId, DBConnection dbConnection)
        {
            MrnMasterV2 mrnMaster = new MrnMasterV2();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT MRN.*,IC.CATEGORY_NAME AS MRN_CATEGORY_NAME , ISC.SUB_CATEGORY_NAME AS MRN_SUB_CATEGORY_NAME ,SD.DEPARTMENT_NAME, "+
                                           " CLA.APPROVED_BY_NAME AS MRN_APPROVAL_BY_NAME ,CLC.CREATED_BY_NAME,CLT.TERMINATED_BY_NAME,SD.PHONE_NO AS DEP_PNO, DMS.STATUS_NAME, " +
                                           " W.LOCATION, W.ADDRESS AS WAREHOUSE_ADDRESS, W.PHONE_NO AS WAREHOUSE_PNO , CLZ.EXPENSE_APPROVAL_BY AS EXPENSE_APPROVAL_BY_NAME  " +
                                           " FROM MRN_MASTER AS MRN " +
                                           " INNER JOIN SUB_DEPARTMENT AS SD ON MRN.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                           " LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVED_BY_NAME FROM COMPANY_LOGIN) AS CLA ON MRN.MRN_APPROVAL_BY = CLA.USER_ID " +
                                           " LEFT JOIN (SELECT USER_ID, FIRST_NAME AS EXPENSE_APPROVAL_BY FROM COMPANY_LOGIN) AS CLZ ON MRN.EXPENSE_APPROVAL_BY = CLZ.USER_ID " +
                                           " LEFT JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS CLC ON MRN.CREATED_BY = CLC.USER_ID " +
                                           " LEFT JOIN(SELECT USER_ID, FIRST_NAME AS TERMINATED_BY_NAME FROM COMPANY_LOGIN) AS CLT ON MRN.TERMINATED_BY = CLT.USER_ID " +
                                           " INNER JOIN ITEM_CATEGORY AS IC ON IC.CATEGORY_ID = MRN.MRN_CATEGORY_ID "+
                                           " INNER JOIN ITEM_SUB_CATEGORY AS ISC ON ISC.SUB_CATEGORY_ID = MRN.MRN_SUB_CATEGORY_ID "+
                                           " LEFT JOIN(SELECT MRN_STATUS_ID, STATUS_NAME FROM DEF_MRN_STATUS) AS DMS ON DMS.MRN_STATUS_ID = MRN.STATUS " +
                                           " LEFT JOIN WAREHOUSE AS W ON W.WAREHOUSE_ID = MRN.WAREHOUSE_ID WHERE MRN.COMPANY_ID = " + companyId + " AND MRN_ID =  " + mrnId + " ";


            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMaster = dataAccessObject.GetSingleOject<MrnMasterV2>(dbConnection.dr);
            }
            return mrnMaster;
        }
        public MrnMasterV2 GetMRNMasterToViewRequisitionReport(int mrnId, int companyId, DBConnection dbConnection) {
            MrnMasterV2 mrnMaster = new MrnMasterV2();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT MRN.*,IC.CATEGORY_NAME AS MRN_CATEGORY_NAME , ISC.SUB_CATEGORY_NAME AS MRN_SUB_CATEGORY_NAME ,SD.DEPARTMENT_NAME, " +
                                           " CLA.APPROVED_BY_NAME AS MRN_APPROVAL_BY_NAME ,CLC.CREATED_BY_NAME,CLT.TERMINATED_BY_NAME,SD.PHONE_NO AS DEP_PNO, DMS.STATUS_NAME, " +
                                           " W.LOCATION, W.ADDRESS AS WAREHOUSE_ADDRESS, W.PHONE_NO AS WAREHOUSE_PNO , CLZ.EXPENSE_APPROVAL_BY AS EXPENSE_APPROVAL_BY_NAME, PRM.PR_ID  " +
                                           " FROM MRN_MASTER AS MRN " +
                                           " INNER JOIN SUB_DEPARTMENT AS SD ON MRN.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                           " LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVED_BY_NAME FROM COMPANY_LOGIN) AS CLA ON MRN.MRN_APPROVAL_BY = CLA.USER_ID " +
                                           " LEFT JOIN (SELECT USER_ID, FIRST_NAME AS EXPENSE_APPROVAL_BY FROM COMPANY_LOGIN) AS CLZ ON MRN.EXPENSE_APPROVAL_BY = CLZ.USER_ID " +
                                           " LEFT JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS CLC ON MRN.CREATED_BY = CLC.USER_ID " +
                                           " LEFT JOIN(SELECT USER_ID, FIRST_NAME AS TERMINATED_BY_NAME FROM COMPANY_LOGIN) AS CLT ON MRN.TERMINATED_BY = CLT.USER_ID " +
                                           " INNER JOIN ITEM_CATEGORY AS IC ON IC.CATEGORY_ID = MRN.MRN_CATEGORY_ID " +
                                           " INNER JOIN ITEM_SUB_CATEGORY AS ISC ON ISC.SUB_CATEGORY_ID = MRN.MRN_SUB_CATEGORY_ID " +
                                            " LEFT JOIN PR_MASTER AS PRM ON PRM.MRN_ID = MRN.MRN_ID " +
                                           " LEFT JOIN(SELECT MRN_STATUS_ID, STATUS_NAME FROM DEF_MRN_STATUS) AS DMS ON DMS.MRN_STATUS_ID = MRN.STATUS " +
                                           " LEFT JOIN WAREHOUSE AS W ON W.WAREHOUSE_ID = MRN.WAREHOUSE_ID WHERE MRN.COMPANY_ID = " + companyId + " AND MRN.MRN_ID =  " + mrnId + " ";


            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMaster = dataAccessObject.GetSingleOject<MrnMasterV2>(dbConnection.dr);
            }
            return mrnMaster;
        }

        public List<MrnDetailsV2> FetchMrnDetailsList(int mrnId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_DETAILS AS MRND " +
                "LEFT JOIN (SELECT MRN_ID,PURCHASE_TYPE, IMPORT_ITEM_TYPE FROM MRN_MASTER ) AS MRNM ON MRNM.MRN_ID = MRND.MRN_ID "+
                "INNER JOIN (SELECT ITEM_ID,SUB_CATEGORY_ID,ITEM_NAME, MEASUREMENT_ID FROM " + dbLibrary + ".ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON MRND.ITEM_ID=AIM.ITEM_ID " +
                "INNER JOIN (SELECT SUB_CATEGORY_ID,SUB_CATEGORY_NAME,CATEGORY_ID FROM " + dbLibrary + ". ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + companyId + ") AS ISCM ON AIM.SUB_CATEGORY_ID=ISCM.SUB_CATEGORY_ID " +
                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = MRND.MEASUREMENT_ID \n" +
                "LEFT JOIN (SELECT MRN_DETAILS_STATUS_ID, STATUS_NAME FROM DEF_MRN_DETAILS_STATUS) AS STA ON STA.MRN_DETAILS_STATUS_ID = MRND.STATUS \n" +
                "INNER JOIN (SELECT CATEGORY_ID,CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY WHERE COMPANY_ID=" + companyId + ") ICM ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                "WHERE MRND.MRN_ID =" + mrnId + " AND MRND.IS_ACTIVE=1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnDetailsV2>(dbConnection.dr);
            }
        }

        public MrnDetailsV2 GetMrndTerminationDetails(int mrndId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT MRND.*,CLT.TERMINATED_BY_NAME,CLT.TERMINATED_BY_SIGNATURE FROM MRN_DETAILS AS MRND " +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS TERMINATED_BY_NAME, DIGITAL_SIGNATURE AS TERMINATED_BY_SIGNATURE FROM COMPANY_LOGIN) AS CLT ON MRND.TERMINATED_BY = CLT.USER_ID\n" +
                                            "WHERE MRND.MRND_ID = " + mrndId;


            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MrnDetailsV2>(dbConnection.dr);
            }
        }

        public int TerminateMRN(int mrnID, int terminatedBy, string remarks, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            //sql.Append("INSERT INTO MRN_DETAIL_STATUS_LOG \n");
            //sql.Append("SELECT MRND_ID,7,'" + LocalTime.Now + "'," + terminatedBy + " FROM MRN_DETAILS WHERE IS_TERMINATED=0 AND MRN_ID =" + mrnID + " \n");

            dbConnection.cmd.CommandText = "SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='TER' ";
            int MStatus = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='TERM' ";
            int DStatus = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            
            sql.Append("UPDATE MRN_MASTER SET IS_TERMINATED=1, TERMINATED_BY=" + terminatedBy + ", TERMINATED_ON='" + LocalTime.Now + "', TERMINATION_REMARKS='" + remarks.ProcessString() + "', STATUS="+ MStatus + " WHERE MRN_ID=" + mrnID + " \n");
            sql.Append(" \n");
            sql.Append("UPDATE MRN_DETAILS SET IS_TERMINATED=1, TERMINATED_BY=" + terminatedBy + ", TERMINATED_ON='" + LocalTime.Now + "', TERMINATION_REMARKS='" + remarks.ProcessString() + "',STATUS=" + DStatus + " WHERE MRN_ID=" + mrnID + " \n");
            sql.Append(" \n");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MrnMasterV2> FetchMrnListforApproval(List<int> subDepartmentIds, DBConnection dbConnection)
        {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "WHERE MRNM.SUB_DEPARTMENT_ID IN (" + string.Join(",", subDepartmentIds) + ") AND MRNM.IS_ACTIVE=1 AND MRNM.IS_MRN_APPROVED = 0 AND MRNM.IS_TERMINATED=0 ORDER BY MRNM.CREATED_DATETIME ASC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);

            }
            return mrnMasters;
        }

        public List<MrnMasterV2> FetchMrnListforApprovalByDate(List<int> subDepartmentIds,DateTime date, DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "WHERE MRNM.SUB_DEPARTMENT_ID IN (" + string.Join(",", subDepartmentIds) + ") AND MRNM.IS_ACTIVE=1 AND MRNM.IS_MRN_APPROVED = 0 AND MONTH(MRNM.CREATED_DATETIME) =" + date.Month + " AND YEAR(MRNM.CREATED_DATETIME)=" + date.Year + " AND MRNM.IS_TERMINATED=0 ORDER BY MRNM.CREATED_DATETIME ASC ";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);

            }
            return mrnMasters;
        }

        public List<MrnMasterV2> FetchMrnListforApprovalByMrnCode(List<int> subDepartmentIds, string MrnCode, DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "WHERE MRNM.SUB_DEPARTMENT_ID IN (" + string.Join(",", subDepartmentIds) + ") AND MRNM.IS_ACTIVE=1 AND MRN_CODE = '"+ MrnCode + "' AND MRNM.IS_MRN_APPROVED = 0 AND MRNM.IS_TERMINATED=0 ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);

            }
            return mrnMasters;
        }

        public int ApproveOrRejectMrn(int expenseType, int mrnId, int isApproved, int isExpenseApproved, int userId, string remark, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            string sql = "";
            if (expenseType == 2) {
               
                if (isApproved == 1) {

                    sql = "UPDATE " + dbLibrary + ".MRN_MASTER " +
                             " SET IS_MRN_APPROVED= " + isApproved + ", MRN_APPROVAL_BY=" + userId + ", " +
                             " MRN_APPROVAL_ON='" + LocalTime.Now + "', MRN_APPROVAL_REMARKS = '" + remark + "' , STATUS = (SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='APRD'), " +
                             " IS_EXPENSE_APPROVED = " + isExpenseApproved + " " +
                             " WHERE MRN_ID=" + mrnId + "; \n";

                    sql += "INSERT INTO MRN_DETAIL_STATUS_LOG \n";
                    sql += "SELECT MRND_ID,(SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE='APRD'),'" + LocalTime.Now + "', " + userId + "  FROM MRN_DETAILS WHERE MRN_ID = " + mrnId + " \n";

                    sql += "UPDATE MRN_DETAILS SET STATUS= (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='ASSGNWH') WHERE MRN_ID = " + mrnId + " ";

                }
                else if (isApproved == 2) {

                    sql = "UPDATE " + dbLibrary + ".MRN_MASTER " +
                             " SET IS_MRN_APPROVED= " + isApproved + ", MRN_APPROVAL_BY=" + userId + ", " +
                             " MRN_APPROVAL_ON='" + LocalTime.Now + "', MRN_APPROVAL_REMARKS = '" + remark + "' , STATUS = (SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='RJCTD'), " +
                             " IS_EXPENSE_APPROVED = " + isExpenseApproved + " " +
                             " WHERE MRN_ID=" + mrnId + "; \n";

                    sql += "INSERT INTO MRN_DETAIL_STATUS_LOG \n";
                    sql += "SELECT MRND_ID,(SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE='RJCTD'),'" + LocalTime.Now + "', " + userId + "  FROM MRN_DETAILS WHERE MRN_ID = " + mrnId + " \n";

                    sql += "UPDATE MRN_DETAILS SET STATUS= (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='RJCTD') WHERE MRN_ID = " + mrnId + " ";
                }

                dbConnection.cmd.CommandText = sql;
            }

            if (expenseType == 1) {

                if (isApproved == 1) {

                    sql = "UPDATE " + dbLibrary + ".MRN_MASTER " +
                             " SET IS_MRN_APPROVED= " + isApproved + ", MRN_APPROVAL_BY=" + userId + ", " +
                             " MRN_APPROVAL_ON='" + LocalTime.Now + "', MRN_APPROVAL_REMARKS = '" + remark + "' , STATUS = (SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='EXP_APR'), " +
                             " IS_EXPENSE_APPROVED = " + isExpenseApproved + " " +
                             " WHERE MRN_ID=" + mrnId + "; \n";

                    sql += "INSERT INTO MRN_DETAIL_STATUS_LOG \n";
                    sql += "SELECT MRND_ID,(SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE='APRD'),'" + LocalTime.Now + "', " + userId + "  FROM MRN_DETAILS WHERE MRN_ID = " + mrnId + " \n";

                    sql += "UPDATE MRN_DETAILS SET STATUS= (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='EXP_APR') WHERE MRN_ID = " + mrnId + " ";

                }
                else if (isApproved == 2) {

                    sql = "UPDATE " + dbLibrary + ".MRN_MASTER " +
                             " SET IS_MRN_APPROVED= " + isApproved + ", MRN_APPROVAL_BY=" + userId + ", " +
                             " MRN_APPROVAL_ON='" + LocalTime.Now + "', MRN_APPROVAL_REMARKS = '" + remark + "' , STATUS = (SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='RJCTD'), " +
                             " IS_EXPENSE_APPROVED = " + isExpenseApproved + " " +
                             " WHERE MRN_ID=" + mrnId + "; \n";

                    sql += "INSERT INTO MRN_DETAIL_STATUS_LOG \n";
                    sql += "SELECT MRND_ID,(SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE='RJCTD'),'" + LocalTime.Now + "', " + userId + "  FROM MRN_DETAILS WHERE MRN_ID = " + mrnId + " \n";

                    sql += "UPDATE MRN_DETAILS SET STATUS= (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='RJCTD') WHERE MRN_ID = " + mrnId + " ";
                }

                dbConnection.cmd.CommandText = sql;
            }
            return dbConnection.cmd.ExecuteNonQuery();
            
        }

        public List<MrnMasterV2> FetchAllMrnByBasicSearchByMonth(DateTime date, DBConnection dbConnection)
        {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
            //    "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
            //    "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
            //    "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
            //    "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
            //    "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
            //    "WHERE MRNM.IS_ACTIVE=1 AND MONTH(MRNM.CREATED_DATETIME) =" + date.Month + " AND YEAR(MRNM.CREATED_DATETIME)=" + date.Year + " ORDER BY MRNM.CREATED_DATETIME ASC";
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
               "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
               "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
               "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
               "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
               "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
               "LEFT JOIN(SELECT MRN_STATUS_ID, STATUS_NAME FROM DEF_MRN_STATUS) AS DMS ON DMS.MRN_STATUS_ID = MRNM.STATUS " +
               "WHERE MRNM.IS_ACTIVE=1 AND MONTH(MRNM.CREATED_DATETIME) =" + date.Month + " AND YEAR(MRNM.CREATED_DATETIME)=" + date.Year + " ORDER BY MRNM.CREATED_DATETIME ASC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchAllMrnh(DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                "WHERE MRNM.IS_ACTIVE=1 ORDER BY MRNM.CREATED_DATETIME ASC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchMrnForExpAppByBasicSearchByMonth(DateTime date, DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                "WHERE MRNM.IS_ACTIVE=1 AND MRNM.EXPENSE_TYPE = 1 AND IS_EXPENSE_APPROVED != 1 AND MRNM.IS_MRN_APPROVED = 1 AND MONTH(MRNM.CREATED_DATETIME) =" + date.Month + " AND YEAR(MRNM.CREATED_DATETIME)=" + date.Year + " ORDER BY MRNM.CREATED_DATETIME ASC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchMrnForExpApp( DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                "WHERE MRNM.IS_ACTIVE=1 AND MRNM.EXPENSE_TYPE = 1 AND IS_EXPENSE_APPROVED != 1 AND MRNM.IS_MRN_APPROVED = 1 ORDER BY MRNM.CREATED_DATETIME ASC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public MrnMasterV2 FetchAllMrnByBasicSearchByMrId(int mrnId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                "WHERE MRNM.IS_ACTIVE=1 AND MRNM.MRN_ID  =" + mrnId + "  ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MrnMasterV2>(dbConnection.dr);
            }
        }

        public MrnMasterV2 FetchAllMrnByBasicSearchByMrnCode(string mrnId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
            //    "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
            //    "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
            //    "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
            //    "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
            //    "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
            //    "WHERE MRNM.IS_ACTIVE=1 AND MRNM.MRN_CODE  ='" + mrnId + "'  ORDER BY MRNM.CREATED_DATETIME DESC";
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                "LEFT JOIN(SELECT MRN_STATUS_ID, STATUS_NAME FROM DEF_MRN_STATUS) AS DMS ON DMS.MRN_STATUS_ID = MRNM.STATUS " +
                "WHERE MRNM.IS_ACTIVE=1 AND MRNM.MRN_CODE  ='" + mrnId + "'  ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MrnMasterV2>(dbConnection.dr);
            }
        }

        public MrnMasterV2 FetchMrnForExpAppByBasicSearchByMrnCode(string mrnId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                "WHERE MRNM.IS_ACTIVE=1 AND MRNM.EXPENSE_TYPE = 1 AND IS_EXPENSE_APPROVED != 1 AND MRNM.IS_MRN_APPROVED = 1 AND MRNM.MRN_CODE  ='" + mrnId + "'  ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchAllMrnByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            //string sql = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
            //             "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
            //             "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
            //             "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
            //             "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
            //             "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
            //             "WHERE MRNM.IS_ACTIVE=1  ";

            string sql = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                         "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                         "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                         "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                         "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                         "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                         "LEFT JOIN(SELECT MRN_STATUS_ID, STATUS_NAME FROM DEF_MRN_STATUS) AS DMS ON DMS.MRN_STATUS_ID = MRNM.STATUS  " +
                         "WHERE MRNM.IS_ACTIVE=1  ";


            if (departmentIds.Count > 0)
            {
                string departments = string.Join<int>(",", departmentIds);
                sql += " AND MRNM.SUB_DEPARTMENT_ID  IN ( " + departments + " ) ";
            }

            if (wareHouseIds.Count > 0)
            {
                string wareHouses = string.Join<int>(",", wareHouseIds);
                sql += " AND MRNM.WAREHOUSE_ID IN ( " + wareHouses + " ) ";
            }
            if (purchaseType != 0)
            {
                sql += " AND MRNM.PURCHASE_TYPE =  " + purchaseType + "";
            }
            if (purchaseProcedure != 0)
            {
                sql += " AND MRNM.PURCHASE_PROCEDURE =  " + purchaseProcedure + "";
            }
            if (createdFromDate != "" && createdToDate != "")
            {
                sql += " AND MRNM.CREATED_DATETIME  BETWEEN '" + Convert.ToDateTime(createdFromDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(createdToDate).ToString("yyyy-MM-dd") + "'";
            }
            if (expectedFromDate != "" && expectedToDate != "")
            {
                sql += " AND MRNM.EXPECTED_DATE BETWEEN  '" + Convert.ToDateTime(expectedFromDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(expectedToDate).ToString("yyyy-MM-dd") + "'";
            }
            if (expenseType != 0)
            {
                sql += " AND MRNM.EXPENSE_TYPE =  " + expenseType + "";
            }
            if (mrnType != 0)
            {
                sql += " AND MRNM.MRN_TYPE =  " + mrnType + "";
            }
            if (mainCatergoryId != 0 && subCatergoryId != 0)
            {
                sql += " AND MRNM.MRN_CATEGORY_ID =  " + mainCatergoryId + " AND MRNM.MRN_SUB_CATEGORY_ID =  " + subCatergoryId + "";
            }
            sql += " ORDER BY  MRNM.CREATED_DATETIME ASC";

            dbConnection.cmd.CommandText = sql;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchMrnForExpAppByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            string sql = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                         "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                         "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                         "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                         "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                         "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                         "WHERE MRNM.IS_ACTIVE=1 AND MRNM.EXPENSE_TYPE = 1 IS_EXPENSE_APPROVED != 1 AND AND IS_MRN_APPROVED = 1 ";


            if (departmentIds.Count > 0) {
                string departments = string.Join<int>(",", departmentIds);
                sql += " AND MRNM.SUB_DEPARTMENT_ID  IN ( " + departments + " ) ";
            }

            if (wareHouseIds.Count > 0) {
                string wareHouses = string.Join<int>(",", wareHouseIds);
                sql += " AND MRNM.WAREHOUSE_ID IN ( " + wareHouses + " ) ";
            }
            if (purchaseType != 0) {
                sql += " AND MRNM.PURCHASE_TYPE =  " + purchaseType + "";
            }
            if (purchaseProcedure != 0) {
                sql += " AND MRNM.PURCHASE_PROCEDURE =  " + purchaseProcedure + "";
            }
            if (createdFromDate != "" && createdToDate != "") {
                sql += " AND MRNM.CREATED_DATETIME  BETWEEN '" + Convert.ToDateTime(createdFromDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(createdToDate).ToString("yyyy-MM-dd") + "'";
            }
            if (expectedFromDate != "" && expectedToDate != "") {
                sql += " AND MRNM.EXPECTED_DATE BETWEEN  '" + Convert.ToDateTime(expectedFromDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(expectedToDate).ToString("yyyy-MM-dd") + "'";
            }
            if (expenseType != 0) {
                sql += " AND MRNM.EXPENSE_TYPE =  " + expenseType + "";
            }
            if (mrnType != 0) {
                sql += " AND MRNM.MRN_TYPE =  " + mrnType + "";
            }
            if (mainCatergoryId != 0 && subCatergoryId != 0) {
                sql += " AND MRNM.MRN_CATEGORY_ID =  " + mainCatergoryId + " AND MRNM.MRN_SUB_CATEGORY_ID =  " + subCatergoryId + "";
            }
            sql += " ORDER BY  MRNM.CREATED_DATETIME ";

            dbConnection.cmd.CommandText = sql;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchMrnListForAvailabiltyExpenseApproval(DBConnection dbConnection)
        {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "WHERE  MRNM.IS_ACTIVE=1 AND MRNM.IS_MRN_APPROVED = 1 AND MRNM.IS_TERMINATED=0 AND MRNM.IS_EXPENSE_APPROVED = 0 ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
            return mrnMasters;
        }

        public void UpdateMrnItemDepartmentStock(int mrnId, int mrndId, decimal departmentStock, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            string sql = "UPDATE " + dbLibrary + ".MRN_DETAILS "+
                         " SET DEPARTMENT_STOCK= "+ departmentStock + " "+
                         " WHERE MRN_ID=" + mrnId + " AND MRND_ID="+mrndId+" ";
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateMRNItemDepartmentStock(int mrnId, int mrndId, decimal departmentStock, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_DETAILS " +
                         " SET DEPARTMENT_STOCK= " + departmentStock + " " +
                         " WHERE MRN_ID=" + mrnId + " AND MRND_ID=" + mrndId + " ";

            return dbConnection.cmd.ExecuteNonQuery();
        }
       
        public void ApproveOrRejectMRNExpense(int mrnId, int isApproved, string remark, int userId, DateTime approvedDate, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            

            if (isApproved == 1) {
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_MASTER " +
                                           " SET IS_EXPENSE_APPROVED= " + isApproved + " , EXPENSE_APPROVAL_BY =" + userId + " , EXPENSE_APPROVAL_ON ='" + approvedDate.ToString("yyyy-MM-dd") + "' ," +
                                           " EXPENSE_APPROVAL_REMARKS='" + remark + "', STATUS =(SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='APRD') " +
                                           " WHERE MRN_ID = " + mrnId + " ";

                dbConnection.cmd.CommandText += "INSERT INTO MRN_DETAIL_STATUS_LOG \n"+
                                                 "SELECT MRND_ID,(SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE='EXP_APRD'),'" + LocalTime.Now + "', " + userId + "  FROM MRN_DETAILS WHERE MRN_ID = " + mrnId + " \n";

                dbConnection.cmd.CommandText += "UPDATE MRN_DETAILS \n" +
                                                "SET STATUS = (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='ASSGNWH') WHERE MRN_ID = " + mrnId + " ";


            }
            else if (isApproved == 2) {
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_MASTER " +
                                           " SET IS_EXPENSE_APPROVED= " + isApproved + " , EXPENSE_APPROVAL_BY =" + userId + " , EXPENSE_APPROVAL_ON ='" + approvedDate.ToString("yyyy-MM-dd") + "' ," +
                                           " EXPENSE_APPROVAL_REMARKS='" + remark + "', STATUS =(SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='EXP_RJCTD') " +
                                           " WHERE MRN_ID = " + mrnId + " ";

                dbConnection.cmd.CommandText += "INSERT INTO MRN_DETAIL_STATUS_LOG \n"+
                                                "SELECT MRND_ID,(SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE='EXP_RJCTD'),'" + LocalTime.Now + "', " + userId + "  FROM MRN_DETAILS WHERE MRN_ID = " + mrnId + " \n";

                dbConnection.cmd.CommandText += "UPDATE MRN_DETAILS \n" +
                                                "SET STATUS = (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='EXP_RJCTD') WHERE MRN_ID = " + mrnId + " ";

            }
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();
        }

        public void UpdateMRNExpense(int mrnId, int expenseType ,int isBudget, decimal budgetAmount, string budgetRemark, string budgetInformation, int userId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_MASTER SET  IS_BUDGET = " + isBudget + " , " +
                                         " EXPENSE_TYPE ="+expenseType+" ,  BUDGET_AMOUNT = " + budgetAmount + " , EXPENSE_REMARKS = '" + budgetRemark + "', " +
                                         " BUDGET_INFO = '" + budgetInformation + "'   " +
                                         " WHERE MRN_ID = " + mrnId + ";";

            //dbConnection.cmd.CommandText += "INSERT INTO MRN_UPDATE_LOG (MRN_ID, UPDATED_BY, UPDATED_DATE, UPDATE_REMARKS) \n" +
            //                                 "VALUES (" + mrnId + " ,"+ userId + " , '"+ LocalTime.Now +"' , '"+ budgetRemark +"')";
                                          

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();
        }

        public int CloneMRN(int mrnId, int clonedBy, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "CLONE_MRN";
            dbConnection.cmd.CommandText = "CLONE_MRN";

            dbConnection.cmd.CommandText = "CLONE_MRN";

            dbConnection.cmd.Parameters.AddWithValue("@MRN_ID", mrnId);
            dbConnection.cmd.Parameters.AddWithValue("@CLONED_BY", clonedBy);
            dbConnection.cmd.Parameters.AddWithValue("@CLONED_ON", LocalTime.Now);

            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public List<MrnMasterV2> FetchAssignedMrnForStoreKeeperByDate(int storekeeperId, DateTime date, DBConnection dbConnection) {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT MRNM.*,WH.LOCATION AS WAREHOUSE_NAME,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME,CLC.FIRST_NAME AS CREATED_BY_NAME FROM MRN_MASTER AS MRNM \n");
            sql.Append("INNER JOIN WAREHOUSE AS WH ON MRNM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n");
            sql.Append("INNER JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n");
            sql.Append("INNER JOIN COMPANY_LOGIN AS CLC ON MRNM.CREATED_BY = CLC.USER_ID \n");
            sql.Append("INNER JOIN COMPANY_LOGIN AS CLA ON MRNM.MRN_APPROVAL_BY =CLA.USER_ID \n");
            sql.Append("WHERE \n");
            sql.Append("((MRNM.IS_MRN_APPROVED=1 AND MRNM.EXPENSE_TYPE =1 AND MRNM.IS_EXPENSE_APPROVED=1) \n");
            sql.Append("	OR (MRNM.IS_MRN_APPROVED=1 AND MRNM.EXPENSE_TYPE=2)) \n");
            sql.Append("AND \n");
            sql.Append("MRNM.IS_TERMINATED=0 AND MRNM.STATUS != (SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='COMP') AND MONTH(MRNM.CREATED_DATETIME) =" + date.Month + " AND YEAR(MRNM.CREATED_DATETIME)=" + date.Year + " AND  MRNM.STORE_KEEPER_ID = " + storekeeperId + " \n");
            sql.Append("AND (SELECT COUNT(MRND_ID) FROM MRN_DETAILS WHERE MRN_ID= MRNM.MRN_ID AND ISNULL(IS_TERMINATED,0) !=1) > 0 \n");
            //sql.Append("AND (SELECT COUNT(MRND_ID) FROM MRN_DETAILS WHERE MRN_ID= MRNM.MRN_ID AND STATUS != (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='ADD_PR')) > 0 \n");

            sql.Append("ORDER BY MRNM.CREATED_DATETIME ASC");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchAssignedMrnForStoreKeeper(int storekeeperId,  DBConnection dbConnection) {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT MRNM.*,WH.LOCATION AS WAREHOUSE_NAME,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME,CLC.FIRST_NAME AS CREATED_BY_NAME FROM MRN_MASTER AS MRNM \n");
            sql.Append("INNER JOIN WAREHOUSE AS WH ON MRNM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n");
            sql.Append("INNER JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n");
            sql.Append("INNER JOIN COMPANY_LOGIN AS CLC ON MRNM.CREATED_BY = CLC.USER_ID \n");
            sql.Append("INNER JOIN COMPANY_LOGIN AS CLA ON MRNM.MRN_APPROVAL_BY =CLA.USER_ID \n");
            sql.Append("WHERE \n");
            sql.Append("((MRNM.IS_MRN_APPROVED=1 AND MRNM.EXPENSE_TYPE =1 AND MRNM.IS_EXPENSE_APPROVED=1) \n");
            sql.Append("	OR (MRNM.IS_MRN_APPROVED=1 AND MRNM.EXPENSE_TYPE=2)) \n");
            sql.Append("AND \n");
            sql.Append("MRNM.IS_TERMINATED=0 AND MRNM.STATUS != (SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='COMP') AND MRNM.STORE_KEEPER_ID = " + storekeeperId + " \n");
            sql.Append("AND (SELECT COUNT(MRND_ID) FROM MRN_DETAILS WHERE MRN_ID= MRNM.MRN_ID AND ISNULL(IS_TERMINATED,0) !=1) > 0 \n");
            //sql.Append("AND (SELECT COUNT(MRND_ID) FROM MRN_DETAILS WHERE MRN_ID= MRNM.MRN_ID AND STATUS != (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='ADD_PR')) > 0 \n");

            sql.Append("ORDER BY MRNM.CREATED_DATETIME DESC");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public MrnMasterV2 FetchAssignedMrnForStoreKeeperByMrnCode(int storekeeperId, string mrnId, DBConnection dbConnection) {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT MRNM.*,WH.LOCATION AS WAREHOUSE_NAME,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME,CLC.FIRST_NAME AS CREATED_BY_NAME FROM MRN_MASTER AS MRNM \n");
            sql.Append("INNER JOIN WAREHOUSE AS WH ON MRNM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n");
            sql.Append("INNER JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n");
            sql.Append("INNER JOIN COMPANY_LOGIN AS CLC ON MRNM.CREATED_BY = CLC.USER_ID \n");
            sql.Append("INNER JOIN COMPANY_LOGIN AS CLA ON MRNM.MRN_APPROVAL_BY =CLA.USER_ID \n");
            sql.Append("WHERE \n");
            sql.Append("((MRNM.IS_MRN_APPROVED=1 AND MRNM.EXPENSE_TYPE =1 AND MRNM.IS_EXPENSE_APPROVED=1) \n");
            sql.Append("	OR (MRNM.IS_MRN_APPROVED=1 AND MRNM.EXPENSE_TYPE=2)) \n");
            sql.Append("AND \n");
            sql.Append("MRNM.IS_TERMINATED=0 AND MRNM.STATUS != (SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='COMP') AND MRNM.MRN_CODE  ='" + mrnId + "' AND  MRNM.STORE_KEEPER_ID = " + storekeeperId + " \n");
            sql.Append("AND (SELECT COUNT(MRND_ID) FROM MRN_DETAILS WHERE MRN_ID= MRNM.MRN_ID AND ISNULL(IS_TERMINATED,0) !=1) > 0 \n");
           // sql.Append("AND (SELECT COUNT(MRND_ID) FROM MRN_DETAILS WHERE MRN_ID= MRNM.MRN_ID AND STATUS != (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='ADD_PR')) > 0 \n");

            sql.Append("ORDER BY MRNM.CREATED_DATETIME DESC");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MrnMasterV2>(dbConnection.dr);
            }
        }

        public int UpdateMrnStatus(int mrnId, int status, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_MASTER "+
                                           " SET STATUS=" + status + "  "+
                                           " WHERE MRN_ID=" + mrnId +" ";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MrnDetailsV2> FetchMrnDetailsListWithoutTerminated(int mrnId, int companyId, int WarehouseId, DBConnection dbConnection)
        {
            List<MrnDetailsV2> mrnDetails = new List<MrnDetailsV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_DETAILS AS MRND " +
                "INNER JOIN (SELECT ITEM_ID,SUB_CATEGORY_ID,ITEM_NAME, MEASUREMENT_ID,STOCK_MAINTAINING_TYPE FROM " + dbLibrary + ".ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON MRND.ITEM_ID=AIM.ITEM_ID " +
                "INNER JOIN (SELECT SUB_CATEGORY_ID,SUB_CATEGORY_NAME,CATEGORY_ID FROM " + dbLibrary + ". ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + companyId + ") AS ISCM ON AIM.SUB_CATEGORY_ID=ISCM.SUB_CATEGORY_ID " +
                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = MRND.MEASUREMENT_ID \n" +
                "LEFT JOIN (SELECT DETAIL_ID AS DETAIL_ID1, SHORT_CODE AS ITEM_UNIT FROM MEASUREMENT_DETAIL) AS U ON U.DETAIL_ID1 = AIM.MEASUREMENT_ID \n" +
                "INNER JOIN (SELECT CATEGORY_ID,CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY WHERE COMPANY_ID=" + companyId + ") ICM ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                "LEFT JOIN (SELECT AVAILABLE_QTY AS WAREHOUSE_QTY,WAREHOUSE_ID, ITEM_ID FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + WarehouseId+ " ) AS INV ON INV.ITEM_ID = MRND.ITEM_ID \n" +
                " LEFT JOIN(SELECT MRN_DETAILS_STATUS_ID, STATUS_NAME FROM DEF_MRN_DETAILS_STATUS) AS DMS ON DMS.MRN_DETAILS_STATUS_ID = MRND.STATUS " +
                "WHERE MRND.MRN_ID =" + mrnId + " AND MRND.IS_ACTIVE=1 AND ( MRND.IS_TERMINATED !=1 or MRND.IS_TERMINATED is null) ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnDetails = dataAccessObject.ReadCollection<MrnDetailsV2>(dbConnection.dr);
            }
            return mrnDetails;
        }

        public string AddMRNtoPR(MrnMasterV2 mrn, int userId, int companyId, out int PrId, DBConnection dbConnection)
        {
            PrId = 1;
            string PrCode = "1";
            string PRCode = "PR" + 1;

            dbConnection.cmd.Parameters.Clear();
            
            string getCode = "";
            if (mrn.PurchaseType == 1) {
                 getCode =  "(SELECT CONCAT('LCL',COUNT(*)+1) FROM PR_MASTER WHERE COMPANY_ID=" + companyId + "  AND PURCHASE_TYPE = 1)";

            }
            else if (mrn.PurchaseType == 2) {
                 getCode = "(SELECT CONCAT('IMP',COUNT(*)+1) FROM PR_MASTER WHERE COMPANY_ID=" + companyId + "  AND PURCHASE_TYPE = 2)";

            }
            //dbConnection.cmd.CommandText = getCode;
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER ";
            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            //Set null date to the operational expenses
            DateTime? expenseApprovedDate = null;
            if(mrn.ExpenseApproalOn.ToShortDateString() == "1/1/0001")
            {
                expenseApprovedDate = null;
            }
            else
            {
                expenseApprovedDate = mrn.ExpenseApproalOn;
            }
            if (count != 0)
            {
                //dbConnection.cmd.CommandText = "SELECT MAX(PR_CODE)+1  FROM " + dbLibrary + ".PR_MASTER ";
                dbConnection.cmd.CommandText = getCode;
                PrCode = dbConnection.cmd.ExecuteScalar().ToString();
                PRCode = "PR-" + PrCode;
                dbConnection.cmd.CommandText = "SELECT MAX(PR_ID)+1  FROM " + dbLibrary + ".PR_MASTER ";
                PrId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }
            dbConnection.cmd.CommandText = "SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='APR' ";
            int MStatus = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            dbConnection.cmd.CommandText = "SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE = 'APR' ";
            int DStatus = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            dbConnection.cmd.CommandText = "SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE = 'ADD_PR' ";
            int lOGStatus = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            string sql = "DECLARE @PR_ID INT; DECLARE @PRtable table (PR_ID int) ;DECLARE @PRD_ID INT;";
                 sql += "INSERT INTO PR_MASTER(PR_CODE,COMPANY_ID,WAREHOUSE_ID,PR_TYPE,PURCHASE_TYPE,EXPECTED_DATE,PURCHASE_PROCEDURE,REQUIRED_FOR,PR_CATEGORY_ID,PR_SUB_CATEGORY_ID,EXPENSE_TYPE "
                  + ",EXPENSE_REMARKS,IS_BUDGET,BUDGET_AMOUNT,BUDGET_INFO,CREATED_BY,CREATED_DATETIME,IS_PR_APPROVED, IS_TERMINATED ,IS_ACTIVE,MRN_ID,STORE_KEEPER_ID,IS_EXPENSE_APPROVED,EXPENSE_APPROVAL_BY, CURRENT_STATUS, IMPORT_ITEM_TYPE)"
                  + " OUTPUT inserted.PR_ID INTO @PRtable "
                  + "VALUES ('" + PrCode + "'," + companyId + "," + mrn.WarehouseId + "," + mrn.MrnType + "," + mrn.PurchaseType + ",'" + mrn.ExpectedDate + "'," + mrn.PurchaseProcedure + ",'" + mrn.RequiredFor + "', " 
                  +"" + mrn.MrnCategoryId + ","+mrn.MrnSubCategoryId+","+mrn.ExpenseType+",'"+mrn.ExpenseRemarks+"',"+mrn.ISBudget+","+mrn.BudgetAmount+",'"+mrn.BudgetInfo+"',"+userId+" ," 
                  +" '"+ LocalTime.Now + "',0, 0 ,1 ," +mrn.MrnId+","+mrn.StoreKeeperId+","+mrn.IsExpenseApproved+","+mrn.ExpenseApprovalBy+ "," + MStatus + ", " + mrn.ImportItemType + ");";

            sql += "set @PR_ID = (SELECT * from  @PRtable);";

            for (int x = 0; x < mrn.MrnCapexDocs.Count; ++x)
            {
                sql += "INSERT INTO PR_CAPEX_DOC(PR_ID,FILE_NAME,FILE_PATH,FILE_DATA)"
                       + "VALUES( @PR_ID  , '" + mrn.MrnCapexDocs[x].FileName + "','" + mrn.MrnCapexDocs[x].FilePath + "' ,'" + mrn.MrnCapexDocs[x].FileData + "');";
            }

            for (int x = 0; x < mrn.MrnDetails.Count; ++x)
            {
                sql += "DECLARE @table" + x + " table (PRD_ID int)";
                sql += "INSERT INTO PR_DETAIL(PR_ID,ITEM_ID,DESCRIPTION,ESTIMATED_AMOUNT,REQUESTED_QTY,WAREHOUSE_STOCK,FILE_SAMPLE_PROVIDED,REPLACEMENT,REMARKS,"
                    + "MRND_ID,IS_ACTIVE, MEASUREMENT_ID, CURRENT_STATUS, SPARE_PART_NUMBER) OUTPUT inserted.PRD_ID INTO @table" + x + " "
                    + "VALUES(@PR_ID," + mrn.MrnDetails[x].ItemId + ",'" + mrn.MrnDetails[x].Description + "'," + mrn.MrnDetails[x].EstimatedAmount + "," + mrn.MrnDetails[x].RequestedQty + ", "
                    + " " + mrn.MrnDetails[x].WarehouseAvailableQty + "," + mrn.MrnDetails[x].FileSampleProvided + "," + mrn.MrnDetails[x].Replacement + ",'" + mrn.MrnDetails[x].Remarks + "' ," 
                    + " " + mrn.MrnDetails[x].MrndId + ",1, " + mrn.MrnDetails[x].MeasurementId + ", " + DStatus + ", '" + mrn.MrnDetails[x].SparePartNo + "');";

                sql += "set @PRD_ID = (SELECT * from  @table" + x + ");";
                sql += "INSERT INTO " + dbLibrary + ".PR_DETAIL_STATUS_LOG (PRD_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (@PRD_ID, " + lOGStatus + ", '" + LocalTime.Now + "', " + userId + ")";
 
                //BOMS of PR Detail
                for (int t = 0;t < mrn.MrnDetails[x].MrnBoms.Count; ++t)
                {
                    sql += "INSERT INTO PR_BOM(PRD_ID,MATERIAL,DESCRIPTION)"
                        + "VALUES( @PRD_ID, '" + mrn.MrnDetails[x].MrnBoms[t].Material + "','" + mrn.MrnDetails[x].MrnBoms[t].Description + "');";
                }

                //Replacement Images of PR Detail
                for (int t = 0; t < mrn.MrnDetails[x].MrnReplacementFileUploads.Count; ++t)
                {
                    sql += "INSERT INTO PR_REPLACE_FILE_UPLOAD(PRD_ID,FILE_NAME,FILE_PATH,FILE_DATA)"
                        + "VALUES(  @PRD_ID  , '" + mrn.MrnDetails[x].MrnReplacementFileUploads[t].FileName + "','" + mrn.MrnDetails[x].MrnReplacementFileUploads[t].FilePath + "' ,'"+ mrn.MrnDetails[x].MrnReplacementFileUploads[t].FileData + "');";
                }

                //Standard Images of PR Detail
                for (int t = 0; t < mrn.MrnDetails[x].MrnFileUploads.Count; ++t)
                {
                    sql += "INSERT INTO PR_FILE_UPLOAD(PRD_ID,FILE_NAME,FILE_PATH,FILE_DATA)"
                        + "VALUES( @PRD_ID  , '" + mrn.MrnDetails[x].MrnFileUploads[t].FileName + "','" + mrn.MrnDetails[x].MrnFileUploads[t].FilePath + "' ,'" + mrn.MrnDetails[x].MrnFileUploads[t].FileData + "');";
                }

                //Supportive Docs of PR Detail
                for (int t = 0; t < mrn.MrnDetails[x].MrnSupportiveDocuments.Count; ++t)
                {
                    sql += "INSERT INTO PR_SUPPORTIVE_DOCUMENTS(PRD_ID,FILE_NAME,FILE_PATH,FILE_DATA)"
                        + "VALUES( @PRD_ID  , '" + mrn.MrnDetails[x].MrnSupportiveDocuments[t].FileName + "','" + mrn.MrnDetails[x].MrnSupportiveDocuments[t].FilePath + "' ,'" + mrn.MrnDetails[x].MrnSupportiveDocuments[t].FileData + "');";
                }

                // Update status in mrnDetails table
                sql += "UPDATE " + dbLibrary + ".MRN_DETAILS SET STATUS = (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='ADD_PR') WHERE MRND_ID = " + mrn.MrnDetails[x].MrndId;
               
            }


            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.ExecuteNonQuery();
            return PRCode;
        }

        public MrnMasterV2 getMrnMasterByMrId(int mrnId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                "WHERE MRNM.IS_ACTIVE=1 AND MRNM.MRN_ID  =" + mrnId + "  ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchApprovedMrnByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            string sql = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                         "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                         "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                         "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                         "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                         "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                         "WHERE MRNM.IS_ACTIVE=1 AND IS_MRN_APPROVED = 1 AND IS_EXPENSE_APPROVED = 1 ";


            if (departmentIds.Count > 0) {
                string departments = string.Join<int>(",", departmentIds);
                sql += " AND MRNM.SUB_DEPARTMENT_ID  IN ( " + departments + " ) ";
            }

            if (wareHouseIds.Count > 0) {
                string wareHouses = string.Join<int>(",", wareHouseIds);
                sql += " AND MRNM.WAREHOUSE_ID IN ( " + wareHouses + " ) ";
            }
            if (purchaseType != 0) {
                sql += " AND MRNM.PURCHASE_TYPE =  " + purchaseType + "";
            }
            if (purchaseProcedure != 0) {
                sql += " AND MRNM.PURCHASE_PROCEDURE =  " + purchaseProcedure + "";
            }
            if (createdFromDate != "" && createdToDate != "") {
                sql += " AND MRNM.CREATED_DATETIME  BETWEEN '" + Convert.ToDateTime(createdFromDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(createdToDate).ToString("yyyy-MM-dd") + "'";
            }
            if (expectedFromDate != "" && expectedToDate != "") {
                sql += " AND MRNM.EXPECTED_DATE BETWEEN  '" + Convert.ToDateTime(expectedFromDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(expectedToDate).ToString("yyyy-MM-dd") + "'";
            }
            if (expenseType != 0) {
                sql += " AND MRNM.EXPENSE_TYPE =  " + expenseType + "";
            }
            if (mrnType != 0) {
                sql += " AND MRNM.MRN_TYPE =  " + mrnType + "";
            }
            if (mainCatergoryId != 0 && subCatergoryId != 0) {
                sql += " AND MRNM.MRN_CATEGORY_ID =  " + mainCatergoryId + " AND MRNM.MRN_SUB_CATEGORY_ID =  " + subCatergoryId + "";
            }
            sql += " ORDER BY  MRNM.CREATED_DATETIME DESC";

            dbConnection.cmd.CommandText = sql;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }


        public MrnMasterV2 FetchApprovedMrnByBasicSearchByMrnCode(string mrnId, List<int> warehouseId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
            //    "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
            //    "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
            //    "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
            //    "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
            //    "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
            //    "WHERE MRNM.IS_ACTIVE=1 AND IS_MRN_APPROVED = 1 AND IS_EXPENSE_APPROVED = 1 AND MRNM.STATUS !=6 AND MRNM.IS_TERMINATED !=1 AND MRNM.MRN_CODE  =" + mrnId + "  ORDER BY MRNM.CREATED_DATETIME DESC";

            dbConnection.cmd.CommandText = "SELECT * FROM MRN_MASTER AS MRNM " +
                                            "INNER JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY = COLOG.USER_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION AS WAREHOUSE_NAME FROM WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID = SUBDEP.SUB_DEPARTMENT_ID " +
                                            "LEFT JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                                            "LEFT JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                                            "WHERE ((EXPENSE_TYPE =1 AND IS_MRN_APPROVED = 1 AND IS_EXPENSE_APPROVED = 1) OR (EXPENSE_TYPE = 2 AND IS_MRN_APPROVED = 1)) AND MRNM.MRN_CODE  ='" + mrnId + "' AND IS_TERMINATED != 1 AND MRNM.WAREHOUSE_ID IN (" + String.Join(",", warehouseId) + ") AND (STORE_KEEPER_ID IS NULL OR STORE_KEEPER_ID= 0) ";


            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MrnMasterV2>(dbConnection.dr);
            }
        }
        public List<MrnMasterV2> FetchApprovedMrnByBasicSearchByMonth(DateTime date, List<int> warehouseId, DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
            //    "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
            //    "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
            //    "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
            //    "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
            //    "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
            //    "WHERE MRNM.IS_ACTIVE=1 AND IS_MRN_APPROVED = 1 AND IS_EXPENSE_APPROVED = 1 AND MRNM.STATUS !=6 AND MRNM.IS_TERMINATED !=1 AND MONTH(MRNM.CREATED_DATETIME) =" + date.Month + " AND YEAR(MRNM.CREATED_DATETIME)=" + date.Year + " ORDER BY MRNM.CREATED_DATETIME DESC";

            dbConnection.cmd.CommandText = "SELECT * FROM MRN_MASTER AS MRNM " +
                                            "INNER JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY = COLOG.USER_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION AS WAREHOUSE_NAME FROM WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID = SUBDEP.SUB_DEPARTMENT_ID " +
                                            "LEFT JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                                            "LEFT JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                                            "WHERE ((EXPENSE_TYPE =1 AND IS_MRN_APPROVED = 1 AND IS_EXPENSE_APPROVED = 1) OR (EXPENSE_TYPE = 2 AND IS_MRN_APPROVED = 1)) AND MONTH(MRNM.CREATED_DATETIME) =" + date.Month + " AND YEAR(MRNM.CREATED_DATETIME)=" + date.Year + " AND IS_TERMINATED != 1 AND MRNM.WAREHOUSE_ID IN (" + String.Join(",", warehouseId) + ") AND (STORE_KEEPER_ID IS NULL OR STORE_KEEPER_ID= 0)  ORDER BY MRNM.CREATED_DATETIME ASC";


            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public List<MrnMasterV2> FetchApprovedMrnByBasicSearch( List<int> warehouseId, DBConnection dbConnection) {
            List<MrnMasterV2> mrnMasters = new List<MrnMasterV2>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRN_MASTER AS MRNM " +
                                            "INNER JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY = COLOG.USER_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION AS WAREHOUSE_NAME FROM WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID = SUBDEP.SUB_DEPARTMENT_ID " +
                                            "LEFT JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                                            "LEFT JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                                            "WHERE ((EXPENSE_TYPE =1 AND IS_MRN_APPROVED = 1 AND IS_EXPENSE_APPROVED = 1) OR (EXPENSE_TYPE = 2 AND IS_MRN_APPROVED = 1)) AND  IS_TERMINATED != 1 AND MRNM.WAREHOUSE_ID IN (" + String.Join(",", warehouseId) + ") AND (STORE_KEEPER_ID IS NULL OR STORE_KEEPER_ID= 0)  ORDER BY MRNM.CREATED_DATETIME ASC";


            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMasterV2>(dbConnection.dr);
            }
        }

        public int updateMrnMasterAfterClone(int mrnId, DBConnection dbConnection) {
            
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "";
            //dbConnection.cmd.CommandText = "UPDATE MRN_MASTER SET STATUS =(SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='CLN') WHERE MRN_ID =" + mrnId + " ";
            //dbConnection.cmd.CommandText += "UPDATE MRN_DETAILS SET STATUS =(SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='CLN') WHERE MRN_ID =" + mrnId + " ";
            //return dbConnection.cmd.ExecuteNonQuery();
            return 1;
        
        }
    }
}
