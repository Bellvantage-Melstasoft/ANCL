using CLibrary.Common;
using CLibrary.Domain;
using System.Collections.Generic;
using System.Text;
using System;
using System.Text.RegularExpressions;

namespace CLibrary.Infrastructure {
    public interface PrMasterDAOV2 {
        List<int> SavePr(PrMasterV2 pr, DBConnection dbConnection);
        int UpdatePr(PrMasterV2 pr, DBConnection dbConnection);
        PrMasterV2 GetPrForEditing(int PrId, DBConnection dbConnection);
        int ClonePR(int prId, int clonedBy, DBConnection dbConnection);
        List<PrMasterV2> FetchMyPrByBasicSearchByMonth(int createdBy, DateTime date, DBConnection dbConnection);
        PrMasterV2 FetchMyPrByBasicSearchByPrCode(int createdBy, string prCode, DBConnection dbConnection);
        List<PrMasterV2> FetchMyPrByAdvanceSearch(int createdBy, List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int prType, int mainCatergoryId, int subCatergoryId, DBConnection dbConnection);
        List<PrDetailsV2> FetchDetailsList(int prId, int companyId, DBConnection dbConnection);
        PrMasterV2 GetPrMasterToView(int prId, int companyId, DBConnection dbConnection);
        List<PrMasterV2> FetchPrListforApproval(List<int> warehouseIds, int purchaseType, DBConnection dbConnection);
        int TerminatePR(int prID, int mrnId, int userId, string remarks, DBConnection dbConnection);
        int ApproveOrRejectPr(int expenseType, int prId, int isApproved, int isExpenseApproved, int userId, string remark, DBConnection dbConnection);
        List<PrMasterV2> FetchPrListForAvailabiltyExpenseApproval(DBConnection dbConnection);
        PrMasterV2 getPrMasterByPrId(int prId, DBConnection dbConnection);
        List<PrDetailsV2> FetchPrDetailsList(int prId, int companyId, DBConnection dbConnection);
        int UpdatePrItemWarehouseStock(int prId, int prdId, decimal warhouseStock, int itemId, int companyId, int fromUnit, int toUnit, DBConnection dbConnection);
        void ApproveOrRejectPrExpense(int prId, int isApproved, string remark, int userId, DateTime approvedDate, DBConnection dbConnection);
        void UpdatePrExpense(int prId, int expenseType, int isBudget, decimal budgetAmount, string budgetRemark, string budgetInformation, int userId, DBConnection dbConnection);
        List<PrMasterV2> FetchAllPrByBasicSearchByMonth(DateTime date, DBConnection dbConnection);
        PrMasterV2 FetchAllPrByBasicSearchByPrCode(string prCode, DBConnection dbConnection);
        List<PrMasterV2> FetchAllPrByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int prType, int mainCatergoryId, int subCatergoryId, DBConnection dbConnection);
        PrMasterV2 GetPrSubmittedBid(int prId, int companyId, DBConnection dbConnection);
        PrMasterV2 GetPRs(int PrId, int CompanyId, DBConnection dbConnection);
        List<PrMasterV2> GetPRListForPrInquiry(int CompanyId, DBConnection dbConnection);
        List<PrMasterV2> FetchPrListforApprovalByDate(List<int> warehouseIds, int purchaseType, DateTime date, DBConnection dbConnection);
        PrMasterV2 FetchPrListforApprovalByCode(List<int> warehouseIds, int purchaseType, string code, DBConnection dbConnection);
        List<PrMasterV2> FetchPrListForAvailabiltyExpenseApprovalByPRCode(string PRCode, DBConnection dbConnection);
        List<PrMasterV2> FetchPrListForAvailabiltyExpenseApprovalByDate(DateTime date, DBConnection dbConnection);
        List<PrDetailsV2> FetchPrDetailsListNew(int prId, int companyId, int warehouseId, DBConnection dbConnection);
        PrMasterV2 getPrIdForPRCode(string code, DBConnection dbConnection);
        List<PrMasterV2> FetchPRByPRCode(string prCode, int CompanyId,  DBConnection dbConnection);
        List<PrMasterV2> GetPrMasterList(int MrnID, int CompanyId, DBConnection dbConnection);
        List<PrDetailsV2> FetchDetails(List<int> prId, int companyId, DBConnection dbConnection);
        List<PrMasterV2> FetchMyPr(int createdBy, DBConnection dbConnection);
    }

    class PrMasterDAOV2Impl : PrMasterDAOV2 {

        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public PrMasterV2 GetPrForEditing(int PrId, DBConnection dbConnection) {

            string sql = "SELECT * FROM PR_MASTER WHERE PR_ID=" + PrId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<int> SavePr(PrMasterV2 pr, DBConnection dbConnection) {
            List<int> PrIdAndCode = new List<int>();

            StringBuilder sql = new StringBuilder();
            string getCode = pr.PurchaseType == 1 ? "(SELECT CONCAT('LCL',COUNT(*)+1) FROM PR_MASTER WHERE PURCHASE_TYPE=1 AND COMPANY_ID=" + pr.CompanyId + ")" : "(SELECT CONCAT('IMP',COUNT(*)+1) FROM PR_MASTER WHERE PURCHASE_TYPE=2 AND COMPANY_ID=" + pr.CompanyId + ")";


            sql.Append("DECLARE @PR_ID TABLE(ID INT) \n");
            sql.Append("DECLARE @PR_CODE INT \n");
            sql.Append(" \n");
            //sql.Append("SELECT @PR_CODE = ISNULL(MAX(PR_CODE),0)+1 FROM PR_MASTER WHERE COMPANY_ID=" + pr.CompanyId + " \n");
            sql.Append("SELECT @PR_CODE = (COUNT(*)+1) FROM PR_MASTER WHERE COMPANY_ID=" + pr.CompanyId + " AND PURCHASE_TYPE = " + pr.PurchaseType + " \n");

            sql.Append(" \n");
            sql.Append("INSERT INTO [PR_MASTER] \n");
            sql.Append("([PR_CODE],[COMPANY_ID],[WAREHOUSE_ID],[PR_TYPE],[PURCHASE_TYPE],[EXPECTED_DATE],[PURCHASE_PROCEDURE],[REQUIRED_FOR],[PR_CATEGORY_ID], \n");
            sql.Append("[PR_SUB_CATEGORY_ID],[EXPENSE_TYPE],[EXPENSE_REMARKS],[IS_BUDGET],[BUDGET_AMOUNT],[BUDGET_INFO],[CREATED_BY],[CREATED_DATETIME],[IS_PR_APPROVED], \n");
            sql.Append("[IS_EXPENSE_APPROVED],[IS_ACTIVE],[CURRENT_STATUS], [IMPORT_ITEM_TYPE]) \n");
            sql.Append("OUTPUT inserted.PR_ID INTO @PR_ID \n");
            sql.Append("VALUES \n");
            sql.Append("(" + getCode + "," + pr.CompanyId + "," + pr.WarehouseId + "," + pr.PrType + "," + pr.PurchaseType + ",'" + pr.ExpectedDate + "'," + pr.PurchaseProcedure + ",'" + pr.RequiredFor.ProcessString() + "'," + pr.PrCategoryId + ", \n");
            sql.Append("" + pr.PrSubCategoryId + "," + pr.ExpenseType + ",'" + pr.ExpenseRemarks.ProcessString() + "'," + pr.ISBudget + "," + pr.BudgetAmount + ",'" + pr.BudgetInfo.ProcessString() + "'," + pr.CreatedBy + ",'" + LocalTime.Now + "',0,  \n");
            sql.Append("0,1,(SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='APR') , " + pr.ImportItemType + ") \n");
            sql.Append(" \n");
            sql.Append("SELECT * FROM @PR_ID \n");
            sql.Append("UNION ALL \n");
            sql.Append("SELECT @PR_CODE \n");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                if (dbConnection.dr.HasRows) {
                    while (dbConnection.dr.Read()) {
                        PrIdAndCode.Add(int.Parse(dbConnection.dr[0].ToString()));
                    }
                }
            }

            return PrIdAndCode;
        }

        public int UpdatePr(PrMasterV2 pr, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();

            dbConnection.cmd.CommandText = "SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='MDFD' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());


            sql.Append("UPDATE [PR_MASTER] \n");
            sql.Append("   SET [PR_TYPE] = " + pr.PrType + " \n");
            sql.Append("      ,[PURCHASE_TYPE] = " + pr.PurchaseType + " \n");
            sql.Append("      ,[EXPECTED_DATE] = '" + pr.ExpectedDate + "' \n");
            sql.Append("      ,[PURCHASE_PROCEDURE] = " + pr.PurchaseProcedure + " \n");
            sql.Append("      ,[REQUIRED_FOR] = '" + pr.RequiredFor.ProcessString() + "' \n");
            sql.Append("      ,[PR_CATEGORY_ID] = " + pr.PrCategoryId + " \n");
            sql.Append("      ,[PR_SUB_CATEGORY_ID] =" + pr.PrSubCategoryId + " \n");
            sql.Append("      ,[EXPENSE_TYPE] = " + pr.ExpenseType + " \n");
            sql.Append("      ,[EXPENSE_REMARKS] = '" + pr.ExpenseRemarks.ProcessString() + "' \n");
            sql.Append("      ,[IS_BUDGET] = " + pr.ISBudget + " \n");
            sql.Append("      ,[BUDGET_AMOUNT] = " + pr.BudgetAmount + " \n");
            sql.Append("      ,[BUDGET_INFO] = '" + pr.BudgetInfo.ProcessString() + "' \n");
            sql.Append("      ,[CURRENT_STATUS] = '" + Status + "' \n");
            sql.Append("      ,[WAREHOUSE_ID] = '" + pr.WarehouseId + "' \n");
            sql.Append("      ,[IMPORT_ITEM_TYPE] = '" + pr.ImportItemType + "' \n");
            sql.Append(" WHERE PR_ID=" + pr.PrId);

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int ClonePR(int prId, int clonedBy, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "CLONE_PR";

            dbConnection.cmd.Parameters.AddWithValue("@PR_ID", prId);
            dbConnection.cmd.Parameters.AddWithValue("@CLONED_BY", clonedBy);
            dbConnection.cmd.Parameters.AddWithValue("@CLONED_ON", LocalTime.Now);

            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            int X =  int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            return X;
        }

        public List<PrMasterV2> FetchMyPrByBasicSearchByMonth(int createdBy, DateTime date, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = PR.PR_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = PR.PR_SUB_CATEGORY_ID " +
                "WHERE PR.CREATED_BY =" + createdBy + " AND PR.IS_ACTIVE=1 AND MONTH(PR.CREATED_DATETIME) =" + date.Month + " AND YEAR(PR.CREATED_DATETIME)=" + date.Year + " " +
                " ORDER BY PR.CREATED_DATETIME ASC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> FetchMyPr(int createdBy, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = PR.PR_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = PR.PR_SUB_CATEGORY_ID " +
                "WHERE PR.CREATED_BY =" + createdBy + " AND PR.IS_ACTIVE=1 " +
                " ORDER BY PR.CREATED_DATETIME ASC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }


        public PrMasterV2 FetchMyPrByBasicSearchByPrCode(int createdBy, string prCode, DBConnection dbConnection)
        {
            //var regex = new Regex("PR", RegexOptions.IgnoreCase);
            //int code = Convert.ToInt32(regex.Replace(prCode, ""));
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = PR.PR_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = PR.PR_SUB_CATEGORY_ID " +
                "WHERE PR.CREATED_BY =" + createdBy + " AND PR.IS_ACTIVE=1 AND PR.PR_CODE  ='" + prCode + "'  ORDER BY PR.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> FetchMyPrByAdvanceSearch(int createdBy, List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int prType, int mainCatergoryId, int subCatergoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            string sql = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                         "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                         "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +                         
                         "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                         "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                         "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = PR.PR_CATEGORY_ID " +
                         "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = PR.PR_SUB_CATEGORY_ID " +
                         "WHERE PR.CREATED_BY =" + createdBy + " AND PR.IS_ACTIVE=1  ";


            if (departmentIds.Count > 0)
            {
                string departments = string.Join<int>(",", departmentIds);
                sql += " AND MRNM.SUB_DEPARTMENT_ID  IN ( " + departments + " ) ";
            }

            if (wareHouseIds.Count > 0)
            {
                string wareHouses = string.Join<int>(",", wareHouseIds);
                sql += " AND PR.WAREHOUSE_ID IN ( " + wareHouses + " ) ";
            }
            if (purchaseType != 0)
            {
                sql += " AND PR.PURCHASE_TYPE =  " + purchaseType + "";
            }
            if (purchaseProcedure != 0)
            {
                sql += " AND PR.PURCHASE_PROCEDURE =  " + purchaseProcedure + "";
            }
            if (createdFromDate != "" && createdToDate != "")
            {
                sql += " AND PR.CREATED_DATETIME  BETWEEN '" + Convert.ToDateTime(createdFromDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(createdToDate).ToString("yyyy-MM-dd") + "'";
            }
            if (expectedFromDate != "" && expectedToDate != "")
            {
                sql += " AND PR.EXPECTED_DATE BETWEEN  '" + Convert.ToDateTime(expectedFromDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(expectedToDate).ToString("yyyy-MM-dd") + "'";
            }
            if (expenseType != 0)
            {
                sql += " AND PR.EXPENSE_TYPE =  " + expenseType + "";
            }
            if (prType != 0)
            {
                sql += " AND PR.PR_TYPE =  " + prType + "";
            }
            if (mainCatergoryId != 0 && subCatergoryId != 0)
            {
                sql += " AND PR.PR_CATEGORY_ID =  " + mainCatergoryId + " AND PR.PR_SUB_CATEGORY_ID =  " + subCatergoryId + "";
            }
            sql += " ORDER BY  PR.CREATED_DATETIME ASC";

            dbConnection.cmd.CommandText = sql;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrDetailsV2> FetchDetailsList(int prId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_DETAIL AS PRD " +
                "INNER JOIN (SELECT PR_ID, WAREHOUSE_ID,PURCHASE_TYPE,IMPORT_ITEM_TYPE FROM PR_MASTER) AS PMA ON PMA.PR_ID = PRD.PR_ID " +
                "INNER JOIN (SELECT ITEM_ID,SUB_CATEGORY_ID,ITEM_NAME, MEASUREMENT_ID FROM " + dbLibrary + ".ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON PRD.ITEM_ID=AIM.ITEM_ID " +
                "INNER JOIN (SELECT SUB_CATEGORY_ID,SUB_CATEGORY_NAME,CATEGORY_ID FROM " + dbLibrary + ". ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + companyId + ") AS ISCM ON AIM.SUB_CATEGORY_ID=ISCM.SUB_CATEGORY_ID " +
                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.MEASUREMENT_ID \n" +
                "INNER JOIN (SELECT CATEGORY_ID,CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY WHERE COMPANY_ID=" + companyId + ") ICM ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                "LEFT JOIN (SELECT PR_DETAILS_STATUS_ID, STATUS_NAME FROM DEF_PR_DETAILS_STATUS) AS STAT ON STAT.PR_DETAILS_STATUS_ID = PRD.CURRENT_STATUS \n" +
                "WHERE PRD.PR_ID =" + prId + " AND PRD.IS_ACTIVE=1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrDetailsV2>(dbConnection.dr);
            }
        }

        public List<PrDetailsV2> FetchDetails(List<int> prId, int companyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_DETAIL AS PRD " +
                "INNER JOIN (SELECT PR_ID, WAREHOUSE_ID, PR_CODE FROM PR_MASTER) AS PMA ON PMA.PR_ID = PRD.PR_ID " +
                "INNER JOIN (SELECT ITEM_ID,SUB_CATEGORY_ID,ITEM_NAME, MEASUREMENT_ID FROM " + dbLibrary + ".ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON PRD.ITEM_ID=AIM.ITEM_ID " +
                "INNER JOIN (SELECT SUB_CATEGORY_ID,SUB_CATEGORY_NAME,CATEGORY_ID FROM " + dbLibrary + ". ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + companyId + ") AS ISCM ON AIM.SUB_CATEGORY_ID=ISCM.SUB_CATEGORY_ID " +
                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.MEASUREMENT_ID \n" +
                "INNER JOIN (SELECT CATEGORY_ID,CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY WHERE COMPANY_ID=" + companyId + ") ICM ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                "LEFT JOIN (SELECT PR_DETAILS_STATUS_ID, STATUS_NAME FROM DEF_PR_DETAILS_STATUS) AS STAT ON STAT.PR_DETAILS_STATUS_ID = PRD.CURRENT_STATUS \n" +
                "WHERE PRD.PR_ID IN (" + String.Join(",", prId) + ") AND PRD.IS_ACTIVE=1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrDetailsV2>(dbConnection.dr);
            }
        }

        public PrMasterV2 GetPrMasterToView(int prId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT PR.* ,MRNM.SUB_DEPARTMENT_ID as SUB_DEPARTMENT_ID " +
                                            ",IC.CATEGORY_NAME AS PR_CATEGORY_NAME , ISC.SUB_CATEGORY_NAME AS PR_SUB_CATEGORY_NAME ,SD.DEPARTMENT_NAME " +
                                           " ,CLA.APPROVED_BY_NAME AS PR_APPROVAL_BY_NAME ,CLC.CREATED_BY_NAME,CLT.TERMINATED_BY_NAME,SD.PHONE_NO AS DEP_PNO " +
                                           ", CLZ.EXPENSE_APPROVAL_BY AS EXPENSE_APPROVAL_BY_NAME , STAT.STATUS_NAME " +
                                           " FROM PR_MASTER AS PR " +
                                           " LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                                           " LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                           " LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVED_BY_NAME FROM COMPANY_LOGIN) AS CLA ON PR.PR_APPROVAL_BY = CLA.USER_ID " +
                                           " LEFT JOIN (SELECT USER_ID, FIRST_NAME AS EXPENSE_APPROVAL_BY FROM COMPANY_LOGIN) AS CLZ ON PR.EXPENSE_APPROVAL_BY = CLZ.USER_ID " +
                                           " LEFT JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS CLC ON PR.CREATED_BY = CLC.USER_ID " +
                                           " LEFT JOIN(SELECT USER_ID, FIRST_NAME AS TERMINATED_BY_NAME FROM COMPANY_LOGIN) AS CLT ON PR.TERMINATED_BY = CLT.USER_ID " +
                                           " INNER JOIN ITEM_CATEGORY AS IC ON IC.CATEGORY_ID = PR.PR_CATEGORY_ID " +
                                            " LEFT JOIN(SELECT PR_STATUS_ID, STATUS_NAME FROM DEF_PR_STATUS) AS STAT ON STAT.PR_STATUS_ID = PR.CURRENT_STATUS " +
                                           " INNER JOIN ITEM_SUB_CATEGORY AS ISC ON ISC.SUB_CATEGORY_ID = PR.PR_SUB_CATEGORY_ID " +
                                           " WHERE PR.COMPANY_ID = " + companyId + " AND PR.PR_ID =  " + prId + " ";


            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> FetchPrListforApproval(List<int> warehouseIds,int purchaseType, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                                           "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                                           "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                                           "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                           "WHERE PR.WAREHOUSE_ID IN (" + string.Join(",", warehouseIds) + ") AND PR.IS_ACTIVE=1 AND PR.IS_PR_APPROVED = 0 AND PR.IS_TERMINATED=0 ORDER BY PR.CREATED_DATETIME ASC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public int TerminatePR(int prID,int mrnId, int userId, string remarks, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='TER' ";
            int PRMStatus = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            dbConnection.cmd.CommandText = "SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='TERM' ";
            int PRDStatus = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            string sql = "UPDATE " + dbLibrary + ".PR_MASTER " +
                         "SET IS_TERMINATED=1, TERMINATED_BY=" + userId + ", TERMINATED_ON='" + LocalTime.Now + "', TERMINATION_REMARKS='" + remarks.ProcessString() + "', CURRENT_STATUS="+ PRMStatus + " " +
                         "WHERE PR_ID = "+ prID + " ";
            sql+= "UPDATE " + dbLibrary + ".PR_DETAIL "+
                  "SET IS_TERMINATED=1, TERMINATED_BY=" + userId + ", TERMINATED_ON='" + LocalTime.Now + "', TERMINATION_REMARKS='" + remarks.ProcessString() + "', CURRENT_STATUS=" + PRDStatus + " " +
                   "WHERE PR_ID = " + prID + " ";


            if(mrnId != 0)

            dbConnection.cmd.CommandText = "SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='TER' ";
            int MStatus = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='TERM' ";
            int DStatus = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            {
                sql += "UPDATE " + dbLibrary + ".MRN_MASTER " +
                      "SET IS_TERMINATED=1, TERMINATED_BY=" + userId + ", TERMINATED_ON='" + LocalTime.Now + "', TERMINATION_REMARKS='" + remarks.ProcessString() + "', STATUS="+ MStatus + " " +
                      "WHERE MRN_ID = " + mrnId + " ";
                sql += "UPDATE " + dbLibrary + ".MRN_DETAILS " +
                      "SET IS_TERMINATED=1, TERMINATED_BY=" + userId + ", TERMINATED_ON='" + LocalTime.Now + "', TERMINATION_REMARKS='" + remarks.ProcessString() + "', STATUS=" + DStatus + "  " +
                      "WHERE MRN_ID = " + mrnId + " ";
            }
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int ApproveOrRejectPr(int expenseType, int prId, int isApproved, int isExpenseApproved, int userId, string remark, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            string sql = "";
            if (expenseType == 2) {

                if (isApproved == 1) {
                    sql = "UPDATE " + dbLibrary + ".PR_MASTER " +
                            " SET IS_PR_APPROVED= " + isApproved + ", PR_APPROVAL_BY=" + userId + ", " +
                            " PR_APPROVAL_ON='" + LocalTime.Now + "', PR_APPROVAL_REMARKS = '" + remark + "' ,  " +
                            " IS_EXPENSE_APPROVED = " + isExpenseApproved + " , CURRENT_STATUS = (SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='APRD') " +
                            " WHERE PR_ID=" + prId + "; \n";

                    sql += "UPDATE PR_DETAIL SET CURRENT_STATUS = (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='CRTBID') WHERE PR_ID = " + prId + " ";

                    sql += "INSERT INTO PR_DETAIL_STATUS_LOG \n";
                    sql += "SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='APRD'),'" + LocalTime.Now + "', " + userId + "  FROM PR_DETAIL WHERE PR_ID = " + prId + " \n";

                }
                else if (isApproved == 2) {
                    sql = "UPDATE " + dbLibrary + ".PR_MASTER " +
                               " SET IS_PR_APPROVED= " + isApproved + ", PR_APPROVAL_BY=" + userId + ", " +
                               " PR_APPROVAL_ON='" + LocalTime.Now + "', PR_APPROVAL_REMARKS = '" + remark + "' ,  " +
                               " IS_EXPENSE_APPROVED = " + isExpenseApproved + " , CURRENT_STATUS = (SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='RJCTD') " +
                               " WHERE PR_ID=" + prId + "; \n";

                    sql += "UPDATE PR_DETAIL SET CURRENT_STATUS = (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='RJCTD') WHERE PR_ID = " + prId + " ";

                    sql += "INSERT INTO PR_DETAIL_STATUS_LOG \n";
                    sql += "SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='RJCTD'),'" + LocalTime.Now + "', " + userId + "  FROM PR_DETAIL WHERE PR_ID = " + prId + " \n";

                }
                dbConnection.cmd.CommandText = sql;
            }

            if (expenseType == 1) {

                if (isApproved == 1) {
                    sql = "UPDATE " + dbLibrary + ".PR_MASTER " +
                            " SET IS_PR_APPROVED= " + isApproved + ", PR_APPROVAL_BY=" + userId + ", " +
                            " PR_APPROVAL_ON='" + LocalTime.Now + "', PR_APPROVAL_REMARKS = '" + remark + "' ,  " +
                            " IS_EXPENSE_APPROVED = " + isExpenseApproved + " , CURRENT_STATUS = (SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='EXP_APR') " +
                            " WHERE PR_ID=" + prId + "; \n";

                    sql += "UPDATE PR_DETAIL SET CURRENT_STATUS = (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='EXP_APR') WHERE PR_ID = " + prId + " ";

                    sql += "INSERT INTO PR_DETAIL_STATUS_LOG \n";
                    sql += "SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='APRD'),'" + LocalTime.Now + "', " + userId + "  FROM PR_DETAIL WHERE PR_ID = " + prId + " \n";

                }
                else if (isApproved == 2) {
                    sql = "UPDATE " + dbLibrary + ".PR_MASTER " +
                               " SET IS_PR_APPROVED= " + isApproved + ", PR_APPROVAL_BY=" + userId + ", " +
                               " PR_APPROVAL_ON='" + LocalTime.Now + "', PR_APPROVAL_REMARKS = '" + remark + "' ,  " +
                               " IS_EXPENSE_APPROVED = " + isExpenseApproved + " , CURRENT_STATUS = (SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='RJCTD') " +
                               " WHERE PR_ID=" + prId + "; \n";

                    sql += "UPDATE PR_DETAIL SET CURRENT_STATUS = (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='RJCTD') WHERE PR_ID = " + prId + " ";

                    sql += "INSERT INTO PR_DETAIL_STATUS_LOG \n";
                    sql += "SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='RJCTD'),'" + LocalTime.Now + "', " + userId + "  FROM PR_DETAIL WHERE PR_ID = " + prId + " \n";

                }
                dbConnection.cmd.CommandText = sql;
            }
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PrMasterV2> FetchPrListForAvailabiltyExpenseApproval(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                "LEFT JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                "WHERE  PR.IS_ACTIVE=1 AND PR.IS_PR_APPROVED = 1 AND PR.IS_TERMINATED=0 AND PR.IS_EXPENSE_APPROVED = 0 ORDER BY PR.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> FetchPrListForAvailabiltyExpenseApprovalByDate(DateTime date, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                "LEFT JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                "WHERE  PR.IS_ACTIVE=1 AND PR.IS_PR_APPROVED = 1 AND MONTH(PR.CREATED_DATETIME) =" + date.Month + " AND YEAR(PR.CREATED_DATETIME)=" + date.Year + " AND  PR.IS_TERMINATED=0 AND PR.IS_EXPENSE_APPROVED = 0 ORDER BY PR.CREATED_DATETIME ASC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> FetchPrListForAvailabiltyExpenseApprovalByPRCode(string PRCode, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                "LEFT JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                "WHERE  PR.IS_ACTIVE=1 AND PR.IS_PR_APPROVED = 1 AND PR_CODE = '"+PRCode+"' AND PR.IS_TERMINATED=0 AND PR.IS_EXPENSE_APPROVED = 0 ORDER BY PR.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public PrMasterV2 getPrMasterByPrId(int prId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                                           "WHERE  PR.PR_ID  =" + prId + " ";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrDetailsV2> FetchPrDetailsList(int prId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_DETAIL AS PRD " +
                 "INNER JOIN (SELECT PR_ID, WAREHOUSE_ID FROM " + dbLibrary + ".PR_MASTER) AS PM ON PM.PR_ID=PRD.PR_ID " +
                "INNER JOIN (SELECT ITEM_ID,SUB_CATEGORY_ID,ITEM_NAME, MEASUREMENT_ID FROM " + dbLibrary + ".ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON PRD.ITEM_ID=AIM.ITEM_ID " +
                "INNER JOIN (SELECT SUB_CATEGORY_ID,SUB_CATEGORY_NAME,CATEGORY_ID FROM " + dbLibrary + ". ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + companyId + ") AS ISCM ON AIM.SUB_CATEGORY_ID=ISCM.SUB_CATEGORY_ID " +
                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.MEASUREMENT_ID \n" +
                "LEFT JOIN (SELECT DETAIL_ID AS DETAIL_ID_ITEM, SHORT_CODE AS WAREHOUSE_UNIT FROM MEASUREMENT_DETAIL) AS U ON U.DETAIL_ID_ITEM = AIM.MEASUREMENT_ID \n" +
                "INNER JOIN (SELECT CATEGORY_ID,CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY WHERE COMPANY_ID=" + companyId + ") ICM ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                "WHERE PRD.PR_ID =" + prId + " AND PRD.IS_ACTIVE=1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrDetailsV2>(dbConnection.dr);
            }
        }

        public List<PrDetailsV2> FetchPrDetailsListNew(int prId, int companyId, int warehouseId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_DETAIL AS PRD " +
                 "INNER JOIN (SELECT PR_ID, WAREHOUSE_ID FROM " + dbLibrary + ".PR_MASTER) AS PM ON PM.PR_ID=PRD.PR_ID " +
                "INNER JOIN (SELECT ITEM_ID,SUB_CATEGORY_ID,ITEM_NAME, MEASUREMENT_ID, STOCK_MAINTAINING_TYPE FROM " + dbLibrary + ".ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON PRD.ITEM_ID=AIM.ITEM_ID " +
                "INNER JOIN (SELECT SUB_CATEGORY_ID,SUB_CATEGORY_NAME,CATEGORY_ID FROM " + dbLibrary + ". ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + companyId + ") AS ISCM ON AIM.SUB_CATEGORY_ID=ISCM.SUB_CATEGORY_ID " +
                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.MEASUREMENT_ID \n" +
                "LEFT JOIN (SELECT DETAIL_ID AS DETAIL_ID_ITEM, SHORT_CODE AS WAREHOUSE_UNIT FROM MEASUREMENT_DETAIL) AS U ON U.DETAIL_ID_ITEM = AIM.MEASUREMENT_ID \n" +
                "INNER JOIN (SELECT CATEGORY_ID,CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY WHERE COMPANY_ID=" + companyId + ") ICM ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                "LEFT JOIN (SELECT ITEM_ID, WAREHOUSE_ID, AVAILABLE_QTY FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + warehouseId + ") AS WIM ON WIM.ITEM_ID = AIM.ITEM_ID \n" +
                "WHERE PRD.PR_ID =" + prId + " AND PRD.IS_ACTIVE=1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrDetailsV2>(dbConnection.dr);
            }
        }

        public int UpdatePrItemWarehouseStock(int prId, int prdId, decimal warhouseStock, int itemId, int companyId, int fromUnit, int toUnit, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            decimal convertedValue = ControllerFactory.CreateConversionController().DoConversion(itemId, companyId, warhouseStock, fromUnit, toUnit);
            decimal RconvertedValue = Math.Round(convertedValue, 2);

            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_DETAIL " +
                         " SET WAREHOUSE_STOCK= " + RconvertedValue + " " +
                         " WHERE PR_ID=" + prId + " AND PRD_ID=" + prdId + " ";

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public void ApproveOrRejectPrExpense(int prId, int isApproved, string remark, int userId, DateTime approvedDate, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            

            if (isApproved == 1)
            {
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_MASTER " +
                                           " SET IS_EXPENSE_APPROVED= " + isApproved + " , EXPENSE_APPROVAL_BY =" + userId + " , EXPENSE_APPROVAL_ON ='" + approvedDate.ToString("yyyy-MM-dd") + "' ," +
                                           " EXPENSE_APPROVAL_REMARKS='" + remark + "', CURRENT_STATUS = (SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='APRD') " +
                                           " WHERE PR_ID = " + prId + " ";

                dbConnection.cmd.CommandText += "UPDATE PR_DETAIL SET CURRENT_STATUS = (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='CRTBID') WHERE PR_ID = " + prId + " ";

               dbConnection.cmd.CommandText += "INSERT INTO PR_DETAIL_STATUS_LOG \n" +
                                                 "SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='EXP_APRD'),'" + LocalTime.Now + "', " + userId + "  FROM PR_DETAIL WHERE PR_ID = " + prId + " \n";

            }
            else if (isApproved == 2)
            {
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_MASTER " +
                                           " SET IS_EXPENSE_APPROVED= " + isApproved + " , EXPENSE_APPROVAL_BY =" + userId + " , EXPENSE_APPROVAL_ON ='" + approvedDate.ToString("yyyy-MM-dd") + "' ," +
                                           " EXPENSE_APPROVAL_REMARKS='" + remark + "', CURRENT_STATUS = (SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='EXP_RJCTD') " +
                                           " WHERE PR_ID = " + prId + " ";

                dbConnection.cmd.CommandText += "UPDATE PR_DETAIL SET CURRENT_STATUS = (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='EXP_RJCTD') WHERE PR_ID = " + prId + " ";

                dbConnection.cmd.CommandText += "INSERT INTO PR_DETAIL_STATUS_LOG \n" +
                                                "SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='EXP_RJCTD'),'" + LocalTime.Now + "', " + userId + "  FROM PR_DETAIL WHERE PR_ID = " + prId + " \n";

            }
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();
        }

        public void UpdatePrExpense(int prId, int expenseType, int isBudget, decimal budgetAmount, string budgetRemark, string budgetInformation, int userId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_MASTER SET  IS_BUDGET = " + isBudget + " , " +
                                         " EXPENSE_TYPE =" + expenseType + " ,  BUDGET_AMOUNT = " + budgetAmount + " , EXPENSE_REMARKS = '" + budgetRemark + "', " +
                                         " BUDGET_INFO = '" + budgetInformation + "'   " +
                                         " WHERE PR_ID = " + prId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PrMasterV2> FetchAllPrByBasicSearchByMonth(DateTime date, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = PR.PR_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = PR.PR_SUB_CATEGORY_ID " +
                "WHERE PR.IS_ACTIVE=1 AND MONTH(PR.CREATED_DATETIME) =" + date.Month + " AND YEAR(PR.CREATED_DATETIME)=" + date.Year + " " +
                " ORDER BY PR.CREATED_DATETIME ASC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public PrMasterV2 FetchAllPrByBasicSearchByPrCode(string prCode, DBConnection dbConnection)
        {
            //var regex = new Regex("PR", RegexOptions.IgnoreCase);
            //int code = Convert.ToInt32(regex.Replace(prCode, ""));
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = PR.PR_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = PR.PR_SUB_CATEGORY_ID " +
                "WHERE PR.IS_ACTIVE=1 AND PR.PR_CODE  ='" + prCode + "'  ORDER BY PR.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> FetchAllPrByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int prType, int mainCatergoryId, int subCatergoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            string sql = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                         "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                         "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                         "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                         "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                         "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = PR.PR_CATEGORY_ID " +
                         "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = PR.PR_SUB_CATEGORY_ID " +
                         "WHERE  PR.IS_ACTIVE=1  ";


            if (departmentIds.Count > 0)
            {
                string departments = string.Join<int>(",", departmentIds);
                sql += " AND MRNM.SUB_DEPARTMENT_ID  IN ( " + departments + " ) ";
            }

            if (wareHouseIds.Count > 0)
            {
                string wareHouses = string.Join<int>(",", wareHouseIds);
                sql += " AND PR.WAREHOUSE_ID IN ( " + wareHouses + " ) ";
            }
            if (purchaseType != 0)
            {
                sql += " AND PR.PURCHASE_TYPE =  " + purchaseType + "";
            }
            if (purchaseProcedure != 0)
            {
                sql += " AND PR.PURCHASE_PROCEDURE =  " + purchaseProcedure + "";
            }
            if (createdFromDate != "" && createdToDate != "")
            {
                sql += " AND PR.CREATED_DATETIME  BETWEEN '" + Convert.ToDateTime(createdFromDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(createdToDate).ToString("yyyy-MM-dd") + "'";
            }
            if (expectedFromDate != "" && expectedToDate != "")
            {
                sql += " AND PR.EXPECTED_DATE BETWEEN  '" + Convert.ToDateTime(expectedFromDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(expectedToDate).ToString("yyyy-MM-dd") + "'";
            }
            if (expenseType != 0)
            {
                sql += " AND PR.EXPENSE_TYPE =  " + expenseType + "";
            }
            if (prType != 0)
            {
                sql += " AND PR.PR_TYPE =  " + prType + "";
            }
            if (mainCatergoryId != 0 && subCatergoryId != 0)
            {
                sql += " AND PR.PR_CATEGORY_ID =  " + mainCatergoryId + " AND PR.PR_SUB_CATEGORY_ID =  " + subCatergoryId + "";
            }
            sql += " ORDER BY  PR.CREATED_DATETIME ASC";

            dbConnection.cmd.CommandText = sql;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public PrMasterV2 GetPrSubmittedBid(int prId, int companyId, DBConnection dbConnection)
        {
            PrMasterV2 prMaster = null;

            //getting PR Master
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME,SUB.SUB_CATEGORY_NAME as PR_SUB_CATEGORY_NAME  " +
                                            " FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "LEFT JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN ITEM_SUB_CATEGORY AS SUB ON SUB.SUB_CATEGORY_ID = PM.PR_SUB_CATEGORY_ID " +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.PR_ID=" + prId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }


            if (prMaster != null)
            {
                //getting PR Details for PR Master 
                prMaster.PrDetails = DAOFactory.CreatePrMasterDAOV2().FetchPrDetailsList(prId, companyId, dbConnection);

                for (int i = 0; i < prMaster.PrDetails.Count; i++)
                {
                    //getting BOMS of PR Detail
                    prMaster.PrDetails[i].PrBoms = DAOFactory.CreatePrBomDAOV2().GetPrdBomForEdit(prMaster.PrDetails[i].PrdId, dbConnection);
                    //getting Replacement Images of PR Detail
                    prMaster.PrDetails[i].PrReplacementFileUploads = DAOFactory.CreatePrReplacementFileUploadDAOV2().GetPrReplacementFileUploadForEdit(prMaster.PrDetails[i].PrdId, dbConnection);
                    //getting Standard Images of PR Detail
                    prMaster.PrDetails[i].PrFileUploads = DAOFactory.CreatePrFileUploadDAOV2().GetPrFileUploadForEdit(prMaster.PrDetails[i].PrdId, dbConnection);
                    //getting Supportive Docs of PR Detail
                    prMaster.PrDetails[i].PrSupportiveDocuments = DAOFactory.CreatePrSupportiveDocumentsDAOV2().GetPrSupportiveDocumentsForEdit(prMaster.PrDetails[i].PrdId, dbConnection);
                }

                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForBidSubmission(prMaster.PrId, dbConnection);

                for (int i = 0; i < prMaster.Bids.Count; i++)
                {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[i].BidId, companyId, dbConnection);
                    //getting bid plans for bids in Pr Master
                    prMaster.Bids[i].BiddingPlan = DAOFactory.createBiddingPlanDAO().GetBiddingPlanByID(prMaster.Bids[i].BidId, dbConnection);

                    for (int j = 0; j < prMaster.Bids[i].BiddingPlan.Count; j++)
                    {
                        prMaster.Bids[i].BiddingPlan[j].biddingPlanFileUpload = DAOFactory.createBiddingPlanDAO().GetPalanfiles(prMaster.Bids[i].BiddingPlan[j].BidId, prMaster.Bids[i].BiddingPlan[j].PlanId, dbConnection);

                    }
                }
            }
            return prMaster;
        }
        public PrMasterV2 GetPRs(int PrId, int CompanyId, DBConnection dbConnection) {

            PrDetailsDAOV2 pR_DetailDAO = DAOFactory.CreatePrDetailsDAOV2();
            PRDetailsStatusLogDAO prDetailsStatusLogDAO = DAOFactory.CreatePRDetailsStatusLogDAO();
            BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
            BiddingItemDAO BiddingItemDAO = DAOFactory.CreateBiddingItemDAO();
            MrnDetailsDAOV2 mrnDetailsDAOV2Impl = DAOFactory.CreateMrnDetailsDAOV2();

            dbConnection.cmd.Parameters.Clear();
            PrMasterV2 PR;
            dbConnection.cmd.CommandText = "SELECT * FROM PR_MASTER AS PM\n" +
                "LEFT JOIN (SELECT USER_NAME as CREATED_BY_NAME, USER_ID FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = PM.CREATED_BY\n" +
                "LEFT JOIN (SELECT MRN_ID, MRN_CODE FROM MRN_MASTER) AS MRN ON MRN.MRN_ID = PM.MRN_ID " +
                "LEFT JOIN (SELECT STATUS_NAME, PR_STATUS_ID FROM DEF_PR_STATUS) AS PRS ON PRS.PR_STATUS_ID = PM.CURRENT_STATUS " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME AS PR_CATEGORY_NAME FROM ITEM_CATEGORY_MASTER) AS CM ON CM.CATEGORY_ID = PM.PR_CATEGORY_ID " +
                "LEFT JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME AS PR_SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY_MASTER) AS SCM ON SCM.SUB_CATEGORY_ID = PM.PR_SUB_CATEGORY_ID " +
                 "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION AS WAREHOUSE_NAME FROM WAREHOUSE ) AS W ON W. WAREHOUSE_ID = PM.WAREHOUSE_ID " +
                "WHERE COMPANY_ID =" + CompanyId + " and PR_ID = " + PrId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PR = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }

            
            PR.Items = pR_DetailDAO.GetPrItemDetails(PR.PrId, dbConnection);
            PR.Bids = biddingDAO.FetchBidInfo(PR.PrId, dbConnection);
            PR.MRNDetail = mrnDetailsDAOV2Impl.GetMrnDetailsForPR(PR.MrnId, dbConnection);
            PR.GRNsCreated = new List<GrnMaster>();
            PR.POsCreated = DAOFactory.createPOMasterDAO().GetAllPosByPrId(PR.PrId, dbConnection);
            PR.POsCreated.ForEach(poc => {
                poc.PoDetails = DAOFactory.createPODetailsDAO().GetPODetailsToViewPo(poc.PoID, CompanyId, dbConnection);
                PR.GRNsCreated.AddRange(DAOFactory.createGrnDAO().GetGeneratedGRNsForPo(poc.PoID, dbConnection));
            });
            PR.GRNsCreated.ForEach(grnc => grnc.GrnDetailsList = DAOFactory.createGRNDetailsDAO().GetGrnDetailsForPrInquiry(grnc.GrnId, CompanyId, dbConnection));

            for (int j=0; j<PR.Items.Count; j++) {
                PR.LogDetails = prDetailsStatusLogDAO.GetPrDStatusByPrDId(PR.Items[j].PrdId, dbConnection);
            }
            
            for (int i = 0; i < PR.Bids.Count; i++ ) {
                
                PR.Bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().FechBiddingItems(PR.Bids[i].BidId, dbConnection);
                DAOFactory.CreateBiddingItemDAO().GetLastPurchaseDetails(PR.Bids[i].BiddingItems, CompanyId, dbConnection);

                PR.Bids[i].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(PR.Bids[i].BidId, dbConnection);
                PR.Bids[i].SupplierQuotations.ForEach(sq => sq.SupplierRating = DAOFactory.createSupplierRatingDAO().GetSupplierRatingBySupplierIdAndCompanyId(sq.SupplierId, CompanyId, dbConnection));


            for (int k = 0; k < PR.Bids[i].BiddingItems.Count; k++) {
            //        //getting quotation items for BiddingItems
                    PR.Bids[i].BiddingItems[k].SupplierQuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItemsByBidItemId(PR.Bids[i].BiddingItems[k].BiddingItemId, CompanyId, dbConnection);
            }

                PR.Bids[i].Tabulations = DAOFactory.CreateTabulationMasterDAO().GetTabulationsByBidId(PR.Bids[i].BidId, dbConnection);
                if (PR.Bids[i].Tabulations != null) {
                    for (int c = 0; c < PR.Bids[i].Tabulations.Count; c++) {
                        PR.Bids[i].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsByTabulationId(PR.Bids[i].Tabulations[c].TabulationId, dbConnection);
                    }
                }
                //}
            }
            return PR;
        }
        public List<PrMasterV2> GetPRListForPrInquiry(int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PR_ID,PR_CODE ,CONCAT('PR', PR_CODE) AS PR_CODE_TEXT FROM PR_MASTER\n" + 
                "WHERE COMPANY_ID =" + CompanyId + " ORDER BY PR_ID ASC;";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> FetchPrListforApprovalByDate(List<int> warehouseIds,int purchaseType, DateTime date, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                                           "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                                           "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                                           "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            //"WHERE PR.WAREHOUSE_ID IN (" + string.Join(",", warehouseIds) + ") AND PR.IS_ACTIVE=1 AND PR.IS_PR_APPROVED = 0 AND PR.PURCHASE_TYPE=" + purchaseType + " AND PR.IS_TERMINATED=0  AND MONTH(PR.CREATED_DATETIME) =" + date.Month + " AND YEAR(PR.CREATED_DATETIME)=" + date.Year + "  ORDER BY PR.CREATED_DATETIME ASC";
                                            "WHERE PR.WAREHOUSE_ID IN (" + string.Join(",", warehouseIds) + ") AND PR.IS_ACTIVE=1 AND PR.IS_PR_APPROVED = 0  AND PR.IS_TERMINATED=0  AND MONTH(PR.CREATED_DATETIME) =" + date.Month + " AND YEAR(PR.CREATED_DATETIME)=" + date.Year + "  ORDER BY PR.CREATED_DATETIME ASC";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public PrMasterV2 FetchPrListforApprovalByCode(List<int> warehouseIds, int purchaseType, string code, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                                           "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON PR.CREATED_BY=COLOG.USER_ID " +
                                           "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON PR.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                                           "LEFT JOIN MRN_MASTER AS MRNM ON PR.MRN_ID = MRNM.MRN_ID " +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                           "WHERE PR.WAREHOUSE_ID IN (" + string.Join(",", warehouseIds) + ") AND PR.IS_ACTIVE=1 AND PR.IS_PR_APPROVED = 0 AND PR.IS_TERMINATED=0  AND PR.PR_CODE = '"+code+"'  ORDER BY PR.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }
        }

        public PrMasterV2 getPrIdForPRCode(string code, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                                           "WHERE PR.PR_CODE = '" + code + "' ";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }
        }
        public List<PrMasterV2> FetchPRByPRCode(string prCode, int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PR " +
                "LEFT JOIN (SELECT MRN_ID,  MRN_CODE FROM MRN_MASTER) AS MRNM ON MRNM.MRN_ID = PR.MRN_ID " +
                "WHERE PR.PR_CODE = '" + prCode + "' AND COMPANY_ID =" + CompanyId + " ";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> GetPrMasterList(int MrnID, int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PR.*, WH.LOCATION AS WAREHOUSE_NAME, CL.USER_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME, SUB.SUB_CATEGORY_NAME AS PR_SUB_CATEGORY_NAME FROM " + dbLibrary + ".PR_MASTER AS PR " +
                "LEFT JOIN (SELECT MRN_ID,  MRN_CODE FROM MRN_MASTER) AS MRNM ON MRNM.MRN_ID = PR.MRN_ID " +
                 "INNER JOIN WAREHOUSE AS WH ON PR.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                  "INNER JOIN COMPANY_LOGIN AS CL ON PR.CREATED_BY = CL.USER_ID \n" +
                  "LEFT JOIN ITEM_CATEGORY_MASTER AS ICM ON PR.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                  "LEFT JOIN ITEM_SUB_CATEGORY AS SUB ON SUB.SUB_CATEGORY_ID = PR.PR_SUB_CATEGORY_ID " +
                "WHERE PR.MRN_ID = " + MrnID + " AND PR.COMPANY_ID =" + CompanyId + " ";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }
    }
}
