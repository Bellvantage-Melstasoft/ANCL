using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface MRN_MasterDAO
    {
        int FetchMRCode(int CompanyId, DBConnection dbConnection);
        int SaveMRMaster(int CompanyId, int SubDeparmentId, DateTime CreatedDate, string description, DateTime ExpectedDate, string CreatedBy, int Status, int isApproved, int isActive, string QuotationFor, int PrTypeId, string ExpenseType, string PrProcedure, string PurchaseType, int WarehouseID, int itemCatId, DBConnection dbConnection);
        List<MRN_Master> FetchDetailsToEdit(int CompanyId, DBConnection dbConnection);
        MRN_Master FetchMRNByMrnId(int Mrnid, DBConnection dbConnection);
        List<MrnDetails> fetchSubmittedMrnDListbywarehouse(int mrnID, int warehouseId, DBConnection dbConnection);
        List<MrnDetails> fetchSubmittedMrnDList(int mrnID, int companyID, DBConnection dbConnection);
        int GetDetailsByMrnCode(int DepartmentId, string MrnCode, DBConnection dbConnection);
        int UpdateMRMaster(int Mrnid, int CompanyId, int SubDeparmentId, string description, DateTime ExpectedDate, int Status, int isActive, int isApproved, int isActive_2, string QuotationFor, int PrTypeId, string ExpenseType, string PrProcedure, string PurchaseType, DBConnection dbConnection);
        List<MrnMaster> fetchSubmittedMrnListbywarehouseId(int companyID, int warehouseId, DBConnection dbConnection);
        int UpdateMRMasterwithStoreKeeper(int mrnId, int userid, DBConnection dbConnection);
        List<MrnMaster> fetchSubmittedMrnList(int companyID, DBConnection dbConnection);
        int updateMRNDIssuedQty(int mrndID, int issuedQty, DBConnection dbConnection);
        int updateMRNDReceivedQty(int mrndID, int receivedQty, DBConnection dbConnection);
        int changeMRNDStaus(int mrndID, int status, DBConnection dbConnection);
        int updateMRNAfterIssue(int mrnID, DBConnection dbConnection);
        int AutoAssignStorekerper(int mrnID, int categoryId, DBConnection dbConnection);
        List<MrnMaster> GetMRNListForViewMyMRN(int companyId, int userId, DBConnection dbConnection);
        List<MrnMaster> GetMRNListForMrnInquiry(int companyId, DBConnection dbConnection);
      //  MrnMaster SearchMRNForInquiryByMrnId(int mrnId, int companyId, DBConnection dbConnection);
        List<MrnMaster> AdvanceSearchMRNForInquiry(int companyId, int searchBy, int categoryId, int subdepartmentId, string searchText, DBConnection dbConnection);

        DataTable GetMRNCountForDashBoard(DBConnection dbConnection);
        List<MrnMaster> FetchMRNToEdit(int companyId,int subDepatmentId, DBConnection dbConnection);
        List<MrnDetails> FetchMRNItemDetails(int mrnID, DBConnection dbConnection);
        List<MrnMaster> fetchSubmittedMrnListForOther(int companyID, DBConnection dbConnection);
        List<MrnMaster> getMrnByDepartment(int companyId, List<int> departmentId, DBConnection dbConnection);
        MrnMaster GetMRNMasterByMrnId(int mrnID, DBConnection dbConnection);
        List<MrnMaster> FetchMrnByMrnCode(int CompanyId, string mrnCode, DBConnection dbConnection);
        List<MrnMaster> FetchMRNByDate(int companyId, DateTime ToDate, DateTime FromDate, DBConnection dbConnection);
    }
    public class MRN_MasterDAOImpl : MRN_MasterDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int FetchMRCode(int CompanyId, DBConnection dbConnection)
        {
            int MrnId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".MRN_MASTER";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0)
            {
                MrnId = 001;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT MAX (MRN_ID)+1 AS MAXid FROM " + dbLibrary + ".MRN_MASTER";
                MrnId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return MrnId;
            }
        }

        public int SaveMRMaster(int CompanyId, int SubDeparmentId, DateTime CreatedDate, string description, DateTime ExpectedDate, string CreatedBy, int Status, int isApproved, int isActive, string QuotationFor, int PrTypeId, string ExpenseType, string PrProcedure, string PurchaseType, int WarehouseID,int itemCatId, DBConnection dbConnection)
        {
            int MrId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".MRN_MASTER ";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0)
            {
                MrId = 001;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT MAX (MRN_ID)+1 AS MAXid FROM " + dbLibrary + ".MRN_MASTER ";
                MrId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }


            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".MRN_MASTER ( COMPANY_ID, SUB_DEPARTMENT_ID, CREATED_DATETIME, DESCRIPTION, EXPECTED_DATE, CREATED_BY, STATUS, IS_APPROVED, IS_ACTIVE, QUOTATION_FOR, PR_TYPE_ID, EXPENSE_TYPE, PR_PROCEDURE, PURCHASE_TYPE,WAREHOUSE_ID,ITEM_CATEGORY_ID) VALUES" +
                        "(  " + CompanyId + " , " + SubDeparmentId + ", '" + CreatedDate + "', '" + description + "', '" + ExpectedDate + "', '" + CreatedBy + "', " + Status + ", " + isApproved + "," + isActive + ", '" + QuotationFor + "', " + PrTypeId + ", '" + ExpenseType + "', '" + PrProcedure + "', '" + PurchaseType + "', '" + WarehouseID + "', " + itemCatId + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();

            return MrId;
        }

        public List<MRN_Master> FetchDetailsToEdit(int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".MRN_MASTER " +
                                           " WHERE (IS_ACTIVE = '1' AND COMPANY_ID=" + CompanyId + " AND IS_APPROVED= '0') " +
                                           " OR (IS_ACTIVE = '0' AND COMPANY_ID=" + CompanyId + " AND IS_APPROVED= '3')";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRN_Master>(dbConnection.dr);
            }
        }

        public int GetDetailsByMrnCode(int CompanyId, string MrnCode, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".MRN_MASTER WHERE MRN_ID= '" + MrnCode + "' AND  COMPANY_ID = " + CompanyId + " ";
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public MRN_Master FetchMRNByMrnId(int Mrnid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER WHERE MRN_ID=" + Mrnid + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MRN_Master>(dbConnection.dr);
            }
        }

        public int UpdateMRMaster(int Mrnid, int CompanyId, int SubDeparmentId, string description, DateTime ExpectedDate, int Status, int isActive, int isApproved, int isActive_2, string QuotationFor, int PrTypeId, string ExpenseType, string PrProcedure, string PurchaseType, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_MASTER SET  COMPANY_ID = " + CompanyId + " , SUB_DEPARTMENT_ID = " + SubDeparmentId + " , DESCRIPTION  = '" + description + "',  EXPECTED_DATE = '" + ExpectedDate + "' , STATUS  = " + Status + " , IS_APPROVED = " + isApproved + " , QUOTATION_FOR = '" + QuotationFor + "' , PR_TYPE_ID = " + PrTypeId + " , EXPENSE_TYPE = '" + ExpenseType + "' , PR_PROCEDURE = '" + PrProcedure + "' , PURCHASE_TYPE = '" + PurchaseType + "'  WHERE MRN_ID = " + Mrnid + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int UpdateMRMasterwithStoreKeeper(int mrnId, int userid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "Update " + dbLibrary + ".MRN_MASTER Set STORE_KEEPER_ID=" + userid + " Where MRN_ID=" + mrnId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public List<MrnDetails> fetchSubmittedMrnDListbywarehouse(int mrnID, int warehouseId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT MRND.MRND_ID,MRND.MRN_ID,MRND.ITEM_ID,MRND.DESCRIPTION,MRND.REQUESTED_QTY,MRND.ISSUED_QTY,MRND.RECEIVED_QTY,MRND.STATUS,MRND.IS_ACTIVE,AIM.ITEM_NAME,CIM.UNIT_PRICE,ISCM.SUB_CATEGORY_ID,ISCM.SUB_CATEGORY_NAME,ICM.CATEGORY_ID,ICM.CATEGORY_NAME,ISNULL(CIM.AVAILABLE_QTY,0) AS A_QTY, (SELECT PR_TYPE_ID FROM " + dbLibrary + ".MRN_MASTER WHERE  MRN_ID=MRND.MRN_ID ) AS PR_TYPE_ID  FROM " + dbLibrary + ".MRN_DETAILS AS MRND " +
                                                "INNER JOIN (SELECT ITEM_ID, SUB_CATEGORY_ID, ITEM_NAME FROM " + dbLibrary + ".ADD_ITEMS_MASTER) AS AIM " +
                                                "ON MRND.ITEM_ID = AIM.ITEM_ID " +
                                                "LEFT JOIN(SELECT AVAILABLE_QTY, ITEM_ID,(NULLIF(STOCK_VALUE,0)/NULLIF(AVAILABLE_QTY,0)) AS UNIT_PRICE FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID =" + warehouseId + ") AS CIM " +
                                                "ON MRND.ITEM_ID = CIM.ITEM_ID " +
                                                "INNER JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME, CATEGORY_ID FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER) AS ISCM " +
                                                "ON AIM.SUB_CATEGORY_ID = ISCM.SUB_CATEGORY_ID " +
                                                "INNER JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER) ICM " +
                                                "ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                                                "WHERE MRND.MRN_ID = " + mrnID + " AND MRND.IS_ACTIVE = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnDetails>(dbConnection.dr);
            }
        }

        public List<MrnDetails> fetchSubmittedMrnDList(int mrnID, int companyID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT MRND.MRND_ID,MRND.MRN_ID,MRND.ITEM_ID,MRND.DESCRIPTION,MRND.REQUESTED_QTY,MRND.ISSUED_QTY,MRND.RECEIVED_QTY,MRND.STATUS,MRND.IS_ACTIVE,AIM.ITEM_NAME,CIM.UNIT_PRICE,ISCM.SUB_CATEGORY_ID,ISCM.SUB_CATEGORY_NAME,ICM.CATEGORY_ID,ICM.CATEGORY_NAME,ISNULL(CIM.AVAILABLE_QTY,0) AS A_QTY FROM " + dbLibrary + ".MRN_DETAILS AS MRND " +
                                            "INNER JOIN (SELECT ITEM_ID, SUB_CATEGORY_ID, ITEM_NAME FROM " + dbLibrary + ".ADD_ITEMS_MASTER) AS AIM " +
                                            "ON MRND.ITEM_ID = AIM.ITEM_ID " +
                                            "LEFT JOIN(SELECT AVAILABLE_QTY, ITEM_ID, COMPANY_ID,(NULLIF(STOCK_VALUE,0)/NULLIF(AVAILABLE_QTY,0)) AS UNIT_PRICE FROM " + dbLibrary + ".COMPANY_INVENTORY_MASTER WHERE COMPANY_ID = " + companyID + ") AS CIM " +
                                            "ON MRND.ITEM_ID = CIM.ITEM_ID " +
                                            "INNER JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME, CATEGORY_ID FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER) AS ISCM " +
                                            "ON AIM.SUB_CATEGORY_ID = ISCM.SUB_CATEGORY_ID " +
                                            "INNER JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER) ICM " +
                                            "ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                                            "WHERE MRND.MRN_ID = " + mrnID + " AND MRND.IS_ACTIVE = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnDetails>(dbConnection.dr);
            }
        }

        public List<MrnMaster> fetchSubmittedMrnListbywarehouseId(int companyID,int warehouseId, DBConnection dbConnection)
        {
            List<MrnMaster> mrnMasters = new List<MrnMaster>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME,USER_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                " INNER JOIN  " + dbLibrary + ".MRN_EXPENSE AS MRNE ON MRNM.MRN_ID = MRNE.MRN_ID" +
                " WHERE MRNM.COMPANY_ID=" + companyID + " AND WAREHOUSE_ID="+ warehouseId + " AND MRNM.IS_ACTIVE=1 AND MRNM.STATUS=0 "+
                " AND MRNM.IS_APPROVED=1 AND MRNE.IS_APPROVED =1  ORDER BY MRNM.EXPECTED_DATE DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);

            }
            return mrnMasters;
        }

        public List<MrnMaster> fetchSubmittedMrnList(int companyID, DBConnection dbConnection)
        {
            List<MrnMaster> mrnMasters = new List<MrnMaster>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME,USER_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                " INNER JOIN  " + dbLibrary + ".MRN_EXPENSE AS MRNE ON MRNM.MRN_ID = MRNE.MRN_ID" +
                " WHERE MRNM.COMPANY_ID=" + companyID + " AND MRNM.IS_ACTIVE=1 AND MRNM.STATUS=0 "+
                " AND MRNM.IS_APPROVED=1 AND  MRNE.IS_APPROVED = 0 "+
                " AND MRNM.EXPENSE_TYPE ='Capital Expense' "+
                " ORDER BY MRNM.EXPECTED_DATE DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);

            }
            
            return mrnMasters;
        }

        public List<MrnMaster> fetchSubmittedMrnListForOther(int companyID, DBConnection dbConnection)
        {
            List<MrnMaster> mrnMasters = new List<MrnMaster>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME,USER_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                " INNER JOIN  " + dbLibrary + ".MRN_EXPENSE AS MRNE ON MRNM.MRN_ID = MRNE.MRN_ID" +
                " WHERE MRNM.COMPANY_ID=" + companyID + " AND MRNM.IS_ACTIVE=1 AND MRNM.STATUS=0 " +
                " AND MRNE.IS_APPROVED =1 AND MRNM.IS_MRN_APPROVED=1 ORDER BY MRNM.EXPECTED_DATE DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);

            }

            return mrnMasters;
        }

        public int updateMRNDIssuedQty(int mrndID, int issuedQty, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_DETAILS SET ISSUED_QTY= ISSUED_QTY+" + issuedQty + " WHERE MRND_ID=" + mrndID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateMRNDReceivedQty(int mrndID, int receivedQty, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_DETAILS SET RECEIVED_QTY= RECEIVED_QTY+" + receivedQty + " WHERE MRND_ID=" + mrndID;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int changeMRNDStaus(int mrndID, int status, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_DETAILS SET STATUS= " + status + " WHERE MRND_ID=" + mrndID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateMRNAfterIssue(int mrnID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF NOT EXISTS (SELECT * FROM MRN_DETAILS WHERE MRN_ID= " + mrnID + " AND STATUS !=3) " +
                                            "UPDATE MRN_MASTER SET STATUS = 1 WHERE MRN_ID = " + mrnID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int AutoAssignStorekerper(int mrnID,int categoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "Update " + dbLibrary + ".MRN_MASTER Set STORE_KEEPER_ID= ( select USER_ID from " + dbLibrary + ".ITEM_CATEGORY_OWNERS a"  + 
                                           " where a.CATEGORY_ID = " + categoryId + " " +
                                           " and a.OWNER_TYPE = 'SK' " +
                                           " and a.effective_date =  (select max(effective_date) from ITEM_CATEGORY_OWNERS b " +
                                           " where b.category_id = a.category_id and a.owner_type = b.owner_type " +
                                           " AND effective_date <= '" +  LocalTime.Now + "' )) WHERE MRN_ID = " + mrnID ;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MrnMaster> GetMRNListForViewMyMRN(int companyId, int userId, DBConnection dbConnection)
        {
            List<MrnMaster> MRNMasters = null;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRN\n" +
                                           " INNER JOIN (SELECT USER_ID,FIRST_NAME AS USER_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS CL ON MRN.CREATED_BY=CL.USER_ID\n" +
                                           " INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRN.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                                           " WHERE MRN.COMPANY_ID =" + companyId + " AND MRN.CREATED_BY=" + userId + " ORDER BY MRN.MRN_ID DESC;";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                MRNMasters = dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);
            }

            for (int i = 0; i < MRNMasters.Count; i++)
            {
                //getting PR Details for PR Master
                MRNMasters[i].mrnDetails = DAOFactory.CreateMRNDetailDAO().GetMrnDetailsByMRNid(MRNMasters[i].MrnID, companyId, dbConnection);

                MRNMasters[i].prMaster = DAOFactory.CreateMRNDetailDAO().GetPRByMRNid(MRNMasters[i].MrnID, companyId, dbConnection);

                if (MRNMasters[i].prMaster.PrId != 0)
                {
                    MRNMasters[i].prMaster.PrDetails = DAOFactory.CreatePR_DetailDAO().GetPrDetailsByPRid(MRNMasters[i].prMaster.PrId, companyId, dbConnection);
                    for (int j = 0; j < MRNMasters[i].prMaster.PrDetails.Count; j++)
                    {
                        MRNMasters[i].prMaster.PrDetails[j].PrDetailsStatusLogs = DAOFactory.CreatePRDetailsStatusLogDAO().GetPrDStatusByPrDId(MRNMasters[i].prMaster.PrDetails[j].PrdId, dbConnection);
                    }
                }

            }

            return MRNMasters;
        }

        public List<MrnMaster> GetMRNListForMrnInquiry(int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT MRN_ID FROM " + dbLibrary + ".MRN_MASTER\n" +
                                           " WHERE COMPANY_ID =" + companyId + " ORDER BY MRN_ID ASC;";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);
            }
        }

      
      

        public DataTable GetMRNCountForDashBoard(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            string query = "select isnull(sum(new.count), 0) as Count ,  '0-14 Days' as Range from ( "
+ " select count(MRN_ID) as count, CONVERT(date, CREATED_DATETIME) as date, DATEDIFF(day, CREATED_DATETIME, '" +  LocalTime.Now + "') AS Days "
+ " from MRN_MASTER where STATUS = 1"
+ " group by CONVERT(date, CREATED_DATETIME), DATEDIFF(day, CREATED_DATETIME, '" +  LocalTime.Now + "'), STATUS) as new"
+ " where new.Days between 0 and 14 union all"
+ "  select isnull(sum(new.count), 0) as Count ,  '15-30 Days' as Range from ("
+ " select count(MRN_ID) as count, CONVERT(date, CREATED_DATETIME) as date, DATEDIFF(day, CREATED_DATETIME, '" +  LocalTime.Now + "') AS Days"
+ " from MRN_MASTER where STATUS = 1"
+ " group by CONVERT(date, CREATED_DATETIME), DATEDIFF(day, CREATED_DATETIME, '" +  LocalTime.Now + "'), STATUS) as new"
+ " where new.Days between 15 and 30"
+ "  union all"
+ "  select isnull(sum(new.count), 0)   as Count ,  '31-60 Days' as Range from ("
+ " select count(MRN_ID) as count, CONVERT(date, CREATED_DATETIME) as date, DATEDIFF(day, CREATED_DATETIME, '" +  LocalTime.Now + "') AS Days"
+ " from MRN_MASTER where STATUS = 1"
+ " group by CONVERT(date, CREATED_DATETIME), DATEDIFF(day, CREATED_DATETIME, '" +  LocalTime.Now + "'), STATUS) as new"
+ " where new.Days between 31 and 60"
+ " union all"
+ "   select isnull(sum(new.count), 0)   as Count  ,  '60 Days <' as Range from ("
+ " select count(MRN_ID) as count, CONVERT(date, CREATED_DATETIME) as date, DATEDIFF(day, CREATED_DATETIME, '" +  LocalTime.Now + "') AS Days"
+ " from MRN_MASTER where STATUS = 1"
+ " group by CONVERT(date, CREATED_DATETIME), DATEDIFF(day, CREATED_DATETIME, '" +  LocalTime.Now + "'), STATUS) as new"
+ " where new.Days > 60";


            dbConnection.cmd.CommandText = query;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(dbConnection.cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        public List<MrnMaster> FetchMRNToEdit(int CompanyId, int subDepatmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".MRN_MASTER as MRNM" +
                                           " INNER JOIN (SELECT USER_ID,FIRST_NAME,USER_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                                           " INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                                           " WHERE ( MRNM.SUB_DEPARTMENT_ID = " + subDepatmentId + " ) AND ( (MRNM.IS_ACTIVE = '1' AND MRNM.COMPANY_ID=" + CompanyId + " AND MRNM.IS_APPROVED= '0') " +
                                           " OR (MRNM.IS_ACTIVE = '0' AND MRNM.COMPANY_ID=" + CompanyId + " AND MRNM.IS_APPROVED= '3') )";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);
            }
        }

        public List<MrnDetails> FetchMRNItemDetails(int mrnID, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<MrnMaster> AdvanceSearchMRNForInquiry(int companyId, int searchBy, int categoryId, int subdepartmentId, string searchText,  DBConnection dbConnection)
        {
            List<MrnMaster> mrnMaster = new List<MrnMaster>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "[dbo].[MRNAdvanceSearchForInquiry]";
            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", companyId);
            dbConnection.cmd.Parameters.AddWithValue("@searchBy", searchBy);
            dbConnection.cmd.Parameters.AddWithValue("@categoryId", categoryId);
            dbConnection.cmd.Parameters.AddWithValue("@subDepartmentId", subdepartmentId);
            dbConnection.cmd.Parameters.AddWithValue("@searchText", searchText);
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMaster = dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);

            }
            if (mrnMaster.Count > 0)
            {
                for (int x = 0; x < mrnMaster.Count; ++x)
                {
                    mrnMaster[x].mrnDetails = DAOFactory.CreateMRNDetailDAO().GetMrnDetailsByMRNid(mrnMaster[x].MrnID, companyId, dbConnection);
                }
            }
            return mrnMaster;

        }

        public List<MrnMaster> getMrnByDepartment(int companyId, List<int> departmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".MRN_MASTER as MRNM" +
                                           " INNER JOIN (SELECT USER_ID,FIRST_NAME,USER_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                                           " INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                                           " WHERE ( MRNM.SUB_DEPARTMENT_ID IN ("  +string.Join(",", departmentId) + " )) AND ( MRNM.COMPANY_ID=" + companyId + ") AND (MRNM.IS_ACTIVE = '1') ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);
            }
        }



        public MrnMaster GetMRNMasterByMrnId(int mrnID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".MRN_MASTER MRNM" +
                                           " LEFT JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME  FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                                           " WHERE (MRN_ID = " + mrnID + ")";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MrnMaster>(dbConnection.dr);
            }
        }

        public List<MrnMaster> FetchMrnByMrnCode(int CompanyId, string mrnCode, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".MRN_MASTER MRNM " +
                "LEFT JOIN (SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN WHERE DEPARTMENT_ID = " + CompanyId + ") AS CL ON MRNM.CREATED_BY = CL.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE ) AS W ON W.WAREHOUSE_ID = MRNM.WAREHOUSE_ID " +
                "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS DEP ON DEP.SUB_DEPARTMENT_ID = MRNM.SUB_DEPARTMENT_ID " +
                 "LEFT JOIN (SELECT MRN_STATUS_ID, STATUS_NAME AS STATUS_CODE FROM DEF_MRN_STATUS) AS ST ON ST.MRN_STATUS_ID = MRNM.STATUS " +
                "WHERE MRNM.MRN_CODE = '" + mrnCode + "' ";


            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);
            }
        }
        public List<MrnMaster> FetchMRNByDate(int companyId, DateTime ToDate, DateTime FromDate, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();


            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".MRN_MASTER MRNM " +
                "LEFT JOIN (SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN WHERE DEPARTMENT_ID = " + companyId + ") AS CL ON MRNM.CREATED_BY = CL.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE ) AS W ON W.WAREHOUSE_ID = MRNM.WAREHOUSE_ID " +
                "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS DEP ON DEP.SUB_DEPARTMENT_ID = MRNM.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT MRN_STATUS_ID, STATUS_NAME AS STATUS_CODE FROM DEF_MRN_STATUS) AS ST ON ST.MRN_STATUS_ID = MRNM.STATUS " +
                "WHERE MRNM.CREATED_DATETIME >= '" + FromDate+ "' AND MRNM.CREATED_DATETIME <= '" + ToDate + "' ORDER BY MRNM.CREATED_DATETIME ";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);
            }
        }
    }
}
