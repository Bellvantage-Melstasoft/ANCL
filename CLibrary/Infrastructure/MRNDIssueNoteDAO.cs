using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface MRNDIssueNoteDAOInterface
    {
        int addNewIssueNote(List<MRNDIssueNote> notes, DBConnection dbConnection);
        int updateIssueNoteAfterDelivered(MRNDIssueNote note, DBConnection dbConnection);
        int updateIssueNoteAfterReceived(MRNDIssueNote note, DBConnection dbConnection);
        List<MRNDIssueNote> fetchDeliveredMrndINList(List<int> DepartmentIds, DBConnection dbConnection);
        List<MRNDIssueNote> fetchReceivedMrndINList(List<int> DepartmentIds, DBConnection dbConnection);
        List<MRNDIssueNote> fetchIssuedMrndINList(List<int> warehouseID, DBConnection dbConnection);
        List<MRNDIssueNote> fetchDeliveredMrndINListWarehouse(List<int> warehouseID, DBConnection dbConnection);
        int addNewIssueNoteonewarehouse(MRNDIssueNote note, DBConnection dbConnection);
        int updateIssueNoteAfterDeliveredmrnissue(MRNDIssueNote note, DBConnection dbConnection);
        List<MRNDIssueNote> FetchforIssueNote(List<int> warehouseID, int issunoteId, DBConnection dbConnection);
        string FetchMRNREFNo(int issunoteId, DBConnection dbConnection);
        List<MRNDIssueNote> FetchIssueNoteDetailsByMrnDetailsId(int mrndId, DBConnection dbConnection);
        int addNewIssueNote(MRNDIssueNote notes, DBConnection dbConnection);
        List<MRNDIssueNote> fetchDeliveredMrndINListByCompanyId(int CompanyId, DBConnection dbConnection);
        List<MRNDIssueNote> fetchReceivedMrndINListByCompanyId(int CompnyId, DBConnection dbConnection);
        List<MRNDIssueNote> IssueNoteDetails(int WarehouseId, List<int> DepartmentIds, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid, DBConnection dbConnection);
        List<MRNDIssueNote> ReceivedMRNDetails(int CompanyId, List<int> WarehouseId, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid, DBConnection dbConnection);
        List<MRNDIssueNote> fetchIssuedMrndINListByMrnCode(List<int> warehouseID, string Code, DBConnection dbConnection);
        List<MRNDIssueNote> fetchIssuedMrndINListByMonth(List<int> warehouseID, DateTime date, DBConnection dbConnection);
        List<MRNDIssueNote> fetchDeliveredMrndINListWarehouseByMonth(List<int> warehouseID, DateTime date, DBConnection dbConnection);
        List<MRNDIssueNote> fetchDeliveredMrndINListWarehouseByMrnCode(List<int> warehouseID, string code, DBConnection dbConnection);
        int updateIssueNoteBeforeConfirmation(MRNDIssueNote note, DBConnection dbConnection);
        List<MRNDIssueNote> fetchDeliveredMrndINListToConfirm(List<int> DepartmentIds, DBConnection dbConnection);
        List<MRNDIssueNote> fetchDeliveredMrndINListToConfirmByCompanyId(int CompanyId, DBConnection dbConnection);
        List<MRNDIssueNote> fetchConfirmedMrndINList(List<int> DepartmentIds, DBConnection dbConnection);
        List<MRNDIssueNote> fetchConfirmedMrndINListByCompanyId(int CompnyId, DBConnection dbConnection);
        int updateIssueNoteAfterRejected(MRNDIssueNote note, int status, DBConnection dbConnection);
        List<MRNDIssueNote> fetchRejectedMrndINListToConfirm(List<int> DepartmentIds, DBConnection dbConnection);
        List<MRNDIssueNote> fetchRejectedMrndINListToConfirmByCompanyId(int CompanyId, DBConnection dbConnection);
        MRNDIssueNote FetcMrndIssueNoteByMrndInId(int mrndInId, DBConnection dbConnection);
        int updateIssueNoteAfterStockRetured(int MrndInID, DBConnection dbConnection);
        List<MRNDIssueNote> fetchReturnedStockTowarehouse(List<int> DepartmentIds, DBConnection dbConnection);
        List<MRNDIssueNote> fetchReturnedStockByCompanyId(int CompanyId, DBConnection dbConnection);
        int updateIssueNoteDepartmentReturn(int Mrnstatus, int RejectedBy, int MrndInID, decimal ReturnQty, int MrndID, decimal IssuesQty, decimal PrevreturnQty, int StockMaitaiinType, DBConnection dbConnection);
        List<MRNDIssueNote> fetchConfirmedMrndINListForRetur(List<int> DepartmentIds, DBConnection dbConnection);
        List<MRNDIssueNote> fetchConfirmedMrndINListByCompanyIdForReturn(int CompnyId, DBConnection dbConnection);
        decimal SumIssuesQty(int MrndInId, DBConnection dbConnection);
        int updateIssueNoteForApproval(int MrndInID, DBConnection dbConnection);
        List<MRNDIssueNote> fetchRejectedMrndINListToApproveByCompanyId(int CompanyId, DBConnection dbConnection);
        List<MRNDIssueNote> fetchRejectedMrndINListToApprove(DBConnection dbConnection);

    }

    class MRNDIssueNoteDAO : MRNDIssueNoteDAOInterface
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int addNewIssueNote(List<MRNDIssueNote> notes, DBConnection dbConnection)
        {
            string sql = "";
            //foreach(MRNDIssueNote note in notes)
            //{
            //    sql+= "INSERT INTO " + dbLibrary + ".MRND_ISSUE_NOTE (MRND_ID,ITEM_ID,WAREHOUSE_ID,ISSUED_QTY,ISSUED_BY,ISSUED_ON,STATUS,MEASUREMENT_ID, STOCK_VALUE) VALUES " +
            //          "(" + note.MrndID + "," + note.ItemID + "," + note.WarehouseID + "," + note.IssuedQty + "," + note.IssuedBy + ",'" +  LocalTime.Now + "'," + note.Status + "," + note.MeasurementId + "," + note.StValue + "); ";

            //}
            foreach (MRNDIssueNote note in notes)
            {
                sql += "DECLARE @MRNDIN_ID TABLE(MRND_IN_ID INT) \n";
                sql += "INSERT INTO " + dbLibrary + ".MRND_ISSUE_NOTE (MRND_ID,ITEM_ID,WAREHOUSE_ID,ISSUED_QTY,ISSUED_BY,ISSUED_ON,STATUS,MEASUREMENT_ID, STOCK_VALUE) OUTPUT INSERTED.MRND_IN_ID INTO @MRNDIN_ID VALUES " +
                      "(" + note.MrndID + "," + note.ItemID + "," + note.WarehouseID + "," + note.IssuedQty + "," + note.IssuedBy + ",'" + LocalTime.Now + "'," + note.Status + "," + note.MeasurementId + "," + note.StValue + "); ";

                foreach (var batch in note.IssuedBatches)
                {
                    sql += $@"INSERT INTO MRND_ISSUE_NOTE_BATCHES
                                VALUES
                                ((SELECT MAX(MRND_IN_ID) FROM @MRNDIN_ID),{batch.BatchId},{batch.IssuedQty},{batch.IssuedStockValue},{note.MeasurementId}) ";
                }

                sql += "SELECT MAX(MRND_IN_ID) FROM @MRNDIN_ID; \n";
            }
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MRNDIssueNote> fetchDeliveredMrndINList(List<int> DepartmentIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN (SELECT MRN_ID, SUB_DEPARTMENT_ID,MRN_CODE FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN (SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) CL ON MRNDIN.ISSUED_BY = CL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) DCL ON MRNDIN.DELIVERED_BY = DCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) ICL ON MRNDIN.ISSUED_BY = ICL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) RCL ON MRNDIN.RECEIVED_BY = RCL.USER_ID " +
                                            "WHERE MRNDIN.STATUS = 2 AND SD.SUB_DEPARTMENT_ID IN(" + string.Join(",", DepartmentIds) + ") ORDER BY MRNDIN.DELIVERED_ON ASC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }

        public List<MRNDIssueNote> fetchDeliveredMrndINListToConfirm(List<int> DepartmentIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN (SELECT MRN_ID, SUB_DEPARTMENT_ID,MRN_CODE FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN (SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) CL ON MRNDIN.ISSUED_BY = CL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) DCL ON MRNDIN.DELIVERED_BY = DCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) ICL ON MRNDIN.ISSUED_BY = ICL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) RCL ON MRNDIN.RECEIVED_BY = RCL.USER_ID " +
                                            "WHERE MRNDIN.STATUS = 4 AND SD.SUB_DEPARTMENT_ID IN(" + string.Join(",", DepartmentIds) + ") ORDER BY MRNDIN.DELIVERED_ON ASC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }

        public List<MRNDIssueNote> fetchRejectedMrndINListToConfirm(List<int> DepartmentIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN (SELECT MRN_ID, SUB_DEPARTMENT_ID,MRN_CODE FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN (SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) CL ON MRNDIN.ISSUED_BY = CL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) DCL ON MRNDIN.DELIVERED_BY = DCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) ICL ON MRNDIN.ISSUED_BY = ICL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) RCL ON MRNDIN.RECEIVED_BY = RCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS REJECTED_USER FROM COMPANY_LOGIN) RECL ON MRNDIN.REJECTED_BY = RECL.USER_ID " +
                                            "WHERE MRNDIN.STATUS = 5 AND SD.SUB_DEPARTMENT_ID IN(" + string.Join(",", DepartmentIds) + ") ORDER BY MRNDIN.REJECTED_ON ASC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public List<MRNDIssueNote> fetchRejectedMrndINListToApprove(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN (SELECT MRN_ID, SUB_DEPARTMENT_ID,MRN_CODE FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN (SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) CL ON MRNDIN.ISSUED_BY = CL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) DCL ON MRNDIN.DELIVERED_BY = DCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) ICL ON MRNDIN.ISSUED_BY = ICL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) RCL ON MRNDIN.RECEIVED_BY = RCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS REJECTED_USER FROM COMPANY_LOGIN) RECL ON MRNDIN.REJECTED_BY = RECL.USER_ID " +
                                            "WHERE MRNDIN.STATUS = 7  ORDER BY MRNDIN.REJECTED_ON ASC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }




        public List<MRNDIssueNote> fetchReturnedStockTowarehouse(List<int> DepartmentIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN (SELECT MRN_ID, SUB_DEPARTMENT_ID,MRN_CODE FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN (SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) CL ON MRNDIN.ISSUED_BY = CL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) DCL ON MRNDIN.DELIVERED_BY = DCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) ICL ON MRNDIN.ISSUED_BY = ICL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) RCL ON MRNDIN.RECEIVED_BY = RCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS REJECTED_USER FROM COMPANY_LOGIN) RECL ON MRNDIN.REJECTED_BY = RECL.USER_ID " +
                                            "WHERE MRNDIN.STATUS = 6 AND SD.SUB_DEPARTMENT_ID IN(" + string.Join(",", DepartmentIds) + ") ORDER BY MRNDIN.REJECTED_ON ASC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }

        public List<MRNDIssueNote> fetchDeliveredMrndINListByCompanyId(int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "SELECT * FROM MRND_ISSUE_NOTE AS MRNDIN "+
            //                                "INNER JOIN(SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID = AIM.ITEM_ID "+
            //                                "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
            //                                "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID, COMPANY_ID,MRN_CODE FROM MRN_MASTER WHERE COMPANY_ID = " + CompanyId+") AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID "+
            //                                "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID "+
            //                                "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID "+
            //                                "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
            //                               "LEFT JOIN(SELECT USER_ID, USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) DCL ON MRNDIN.DELIVERED_BY = DCL.USER_ID " +
            //                                "LEFT JOIN(SELECT USER_ID, USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) ICL ON MRNDIN.ISSUED_BY = ICL.USER_ID " +
            //                                "LEFT JOIN(SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) RCL ON MRNDIN.RECEIVED_BY = RCL.USER_ID ASC" +

            //                                "WHERE MRNDIN.STATUS = 2 ORDER BY MRNDIN.DELIVERED_ON  ";

            dbConnection.cmd.CommandText = "SELECT * FROM MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID = AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID, COMPANY_ID,MRN_CODE FROM MRN_MASTER WHERE COMPANY_ID = " + CompanyId + ") AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                           "LEFT JOIN(SELECT USER_ID, USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) DCL ON MRNDIN.DELIVERED_BY = DCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) ICL ON MRNDIN.ISSUED_BY = ICL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) RCL ON MRNDIN.RECEIVED_BY = RCL.USER_ID " +

                                            "WHERE MRNDIN.STATUS = 2 ORDER BY MRNDIN.DELIVERED_ON  ASC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }

        public List<MRNDIssueNote> fetchDeliveredMrndINListToConfirmByCompanyId(int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID = AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID, COMPANY_ID,MRN_CODE FROM MRN_MASTER WHERE COMPANY_ID = " + CompanyId + ") AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                           "LEFT JOIN(SELECT USER_ID, USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) DCL ON MRNDIN.DELIVERED_BY = DCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) ICL ON MRNDIN.ISSUED_BY = ICL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) RCL ON MRNDIN.RECEIVED_BY = RCL.USER_ID ASC" +

                                            "WHERE MRNDIN.STATUS = 4 ORDER BY MRNDIN.DELIVERED_ON  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public List<MRNDIssueNote> fetchRejectedMrndINListToConfirmByCompanyId(int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID = AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID, COMPANY_ID,MRN_CODE FROM MRN_MASTER WHERE COMPANY_ID = " + CompanyId + ") AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                           "LEFT JOIN(SELECT USER_ID, USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) DCL ON MRNDIN.DELIVERED_BY = DCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) ICL ON MRNDIN.ISSUED_BY = ICL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) RCL ON MRNDIN.RECEIVED_BY = RCL.USER_ID ASC" +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS REJECTED_USER FROM COMPANY_LOGIN) RECL ON MRNDIN.REJECTED_BY = RECL.USER_ID " +
                                            "WHERE MRNDIN.STATUS = 5 ORDER BY MRNDIN.REJECTED_ON  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public List<MRNDIssueNote> fetchRejectedMrndINListToApproveByCompanyId(int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID = AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID, COMPANY_ID,MRN_CODE FROM MRN_MASTER WHERE COMPANY_ID = " + CompanyId + ") AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                           "LEFT JOIN(SELECT USER_ID, USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) DCL ON MRNDIN.DELIVERED_BY = DCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) ICL ON MRNDIN.ISSUED_BY = ICL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) RCL ON MRNDIN.RECEIVED_BY = RCL.USER_ID ASC" +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS REJECTED_USER FROM COMPANY_LOGIN) RECL ON MRNDIN.REJECTED_BY = RECL.USER_ID " +
                                            "WHERE MRNDIN.STATUS = 7 ORDER BY MRNDIN.REJECTED_ON  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public List<MRNDIssueNote> fetchReturnedStockByCompanyId(int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID = AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID, COMPANY_ID,MRN_CODE FROM MRN_MASTER WHERE COMPANY_ID = " + CompanyId + ") AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                           "LEFT JOIN(SELECT USER_ID, USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) DCL ON MRNDIN.DELIVERED_BY = DCL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) ICL ON MRNDIN.ISSUED_BY = ICL.USER_ID " +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) RCL ON MRNDIN.RECEIVED_BY = RCL.USER_ID ASC" +
                                            "LEFT JOIN(SELECT USER_ID, USER_NAME AS REJECTED_USER FROM COMPANY_LOGIN) RECL ON MRNDIN.REJECTED_BY = RECL.USER_ID " +
                                            "WHERE MRNDIN.STATUS = 6 ORDER BY MRNDIN.REJECTED_ON  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public List<MRNDIssueNote> fetchDeliveredMrndINListWarehouse(List<int> warehouseID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.DELIVERED_BY=CL.USER_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, MRN_CODE, SUB_DEPARTMENT_ID FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "WHERE MRNDIN.STATUS = 2 AND W.WAREHOUSE_ID IN (" + string.Join(",", warehouseID) + ")  ORDER BY MRNDIN.DELIVERED_ON ASC ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }

        public List<MRNDIssueNote> fetchDeliveredMrndINListWarehouseByMonth(List<int> warehouseID, DateTime date, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.DELIVERED_BY=CL.USER_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, MRN_CODE, SUB_DEPARTMENT_ID FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "WHERE MRNDIN.STATUS = 2 AND MONTH(MRNDIN.DELIVERED_ON) =" + date.Month + " AND YEAR(MRNDIN.DELIVERED_ON)=" + date.Year + " AND W.WAREHOUSE_ID IN (" + string.Join(",", warehouseID) + ")  ORDER BY MRNDIN.DELIVERED_ON ASC ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public List<MRNDIssueNote> fetchDeliveredMrndINListWarehouseByMrnCode(List<int> warehouseID, string code, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.DELIVERED_BY=CL.USER_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, MRN_CODE, SUB_DEPARTMENT_ID FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "WHERE MRNDIN.STATUS = 2 AND MRNM.MRN_CODE = '" + code + "' AND W.WAREHOUSE_ID IN (" + string.Join(",", warehouseID) + ")  ORDER BY MRNDIN.DELIVERED_ON ASC ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public List<MRNDIssueNote> fetchIssuedMrndINList(List<int> warehouseID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.ISSUED_BY=CL.USER_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, MRN_CODE, SUB_DEPARTMENT_ID FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "WHERE MRNDIN.STATUS = 1 AND MRNDIN.WAREHOUSE_ID IN (" + string.Join(",", warehouseID) + ") ORDER BY MRNDIN.ISSUED_ON ASC ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public List<MRNDIssueNote> fetchIssuedMrndINListByMrnCode(List<int> warehouseID, string Code, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.ISSUED_BY=CL.USER_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, MRN_CODE, SUB_DEPARTMENT_ID FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "WHERE MRNDIN.STATUS = 1 AND MRNM.MRN_CODE = '" + Code + "' AND MRNDIN.WAREHOUSE_ID IN (" + string.Join(",", warehouseID) + ") ORDER BY MRNDIN.ISSUED_ON ASC ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public List<MRNDIssueNote> fetchIssuedMrndINListByMonth(List<int> warehouseID, DateTime date, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.ISSUED_BY=CL.USER_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, MRN_CODE, SUB_DEPARTMENT_ID FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "WHERE MRNDIN.STATUS = 1 AND MONTH(MRNDIN.ISSUED_ON) =" + date.Month + " AND YEAR(MRNDIN.ISSUED_ON)=" + date.Year + " AND MRNDIN.WAREHOUSE_ID IN (" + string.Join(",", warehouseID) + ") ORDER BY MRNDIN.ISSUED_ON ASC ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public List<MRNDIssueNote> fetchReceivedMrndINList(List<int> DepartmentIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.RECEIVED_BY=CL.USER_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID,MRN_CODE FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "WHERE MRNDIN.STATUS = 4 AND SD.SUB_DEPARTMENT_ID IN(" + string.Join(",", DepartmentIds) + ") ORDER BY MRNDIN.RECEIVED_ON ASC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }

        public List<MRNDIssueNote> fetchConfirmedMrndINList(List<int> DepartmentIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME, STOCK_MAINTAINING_TYPE FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.RECEIVED_BY=CL.USER_ID " +
                                            "LEFT JOIN (SELECT USER_ID,USER_NAME AS CONFIRMED_USER FROM COMPANY_LOGIN) CNL ON MRNDIN.RECEIVE_CONFIRMED_BY=CNL.USER_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID,MRN_CODE FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "WHERE MRNDIN.STATUS = 3 AND SD.SUB_DEPARTMENT_ID IN(" + string.Join(",", DepartmentIds) + ") ORDER BY MRNDIN.RECEIVED_ON ASC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public List<MRNDIssueNote> fetchConfirmedMrndINListForRetur(List<int> DepartmentIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT MRNDIN.*, AIM.ITEM_ID, AIM.ITEM_NAME, AIM.STOCK_MAINTAINING_TYPE, CL.RECEIVED_USER, CNL.CONFIRMED_USER,MRND.MRND_ID, MRND.MRN_ID, " +
                                            "MRND.MEASUREMENT_ID , MRNM.SUB_DEPARTMENT_ID,MRNM.MRN_CODE, SD.DEPARTMENT_NAME ,W.LOCATION, M.SHORT_CODE,SUM(DR.RETURN_QTY) AS RETURN_QTY FROM ANCL_BID_QA.dbo.MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME, STOCK_MAINTAINING_TYPE FROM ADD_ITEMS) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.RECEIVED_BY=CL.USER_ID " +
                                            "LEFT JOIN (SELECT USER_ID,USER_NAME AS CONFIRMED_USER FROM COMPANY_LOGIN) CNL ON MRNDIN.RECEIVE_CONFIRMED_BY=CNL.USER_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID,MRN_CODE FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "LEFT JOIN(SELECT RETURN_QTY, MRND_IN_ID FROM DEPARTMENT_RETURN) AS DR ON DR.MRND_IN_ID = MRNDIN.MRND_IN_ID " +
                                            "WHERE MRNDIN.STATUS = 3 AND SD.SUB_DEPARTMENT_ID IN(" + string.Join(",", DepartmentIds) + ") " +
                                           " GROUP BY MRNDIN.MRND_IN_ID, MRNDIN.DELIVERED_BY,MRNDIN.DELIVERED_ON, MRNDIN.ISSUED_BY, MRNDIN.ISSUED_ON, MRNDIN.ISSUED_QTY, MRNDIN.MRND_ID, MRNDIN.ITEM_ID, " +
                                           "MRNDIN.WAREHOUSE_ID, MRNDIN.RECEIVED_BY, MRNDIN.RECEIVED_ON, MRNDIN.STATUS, MRNDIN.MEASUREMENT_ID, MRNDIN.STOCK_VALUE, MRNDIN.RECEIVE_CONFIRMED_BY, MRNDIN.RECEIVE_CONFIRMED_ON, " +
                                           "MRNDIN.REJECTED_BY, MRNDIN.REJECTED_ON,AIM.ITEM_ID, AIM.ITEM_NAME, AIM.STOCK_MAINTAINING_TYPE, CL.RECEIVED_USER, CNL.CONFIRMED_USER,MRND.MRND_ID, MRND.MRN_ID, " +
                                          "MRND.MEASUREMENT_ID , MRNM.SUB_DEPARTMENT_ID,MRNM.MRN_CODE, SD.DEPARTMENT_NAME ,W.LOCATION, M.SHORT_CODE " +
                                           "ORDER BY MRNDIN.RECEIVED_ON ASC ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }

        public List<MRNDIssueNote> fetchReceivedMrndINListByCompanyId(int CompnyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "SELECT * FROM MRND_ISSUE_NOTE AS MRNDIN "+
            //                                "INNER JOIN(SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID = AIM.ITEM_ID "+
            //                                "INNER JOIN (SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.RECEIVED_BY = CL.USER_ID "+
            //                                "LEFT JOIN (SELECT USER_ID,USER_NAME AS CONFIRMED_USER FROM COMPANY_LOGIN) CNL ON MRNDIN.RECEIVE_CONFIRMED_BY=CNL.USER_ID " +
            //                                "INNER JOIN(SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
            //                                "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID, COMPANY_ID, MRN_CODE FROM MRN_MASTER WHERE COMPANY_ID = 6) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID "+
            //                                "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID "+
            //                               "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
            //                                "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID ASC" +
            //                                "WHERE MRNDIN.STATUS = 4 ORDER BY MRNDIN.RECEIVED_ON ";
            dbConnection.cmd.CommandText = "SELECT * FROM MRND_ISSUE_NOTE AS MRNDIN " +
                "INNER JOIN(SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID = AIM.ITEM_ID " +
                "INNER JOIN (SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.RECEIVED_BY = CL.USER_ID " +
                "LEFT JOIN (SELECT USER_ID,USER_NAME AS CONFIRMED_USER FROM COMPANY_LOGIN) CNL ON MRNDIN.RECEIVE_CONFIRMED_BY=CNL.USER_ID " +
                "INNER JOIN(SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID, COMPANY_ID, MRN_CODE FROM MRN_MASTER WHERE COMPANY_ID = 6) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE) AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                "WHERE MRNDIN.STATUS = 4 ORDER BY MRNDIN.RECEIVED_ON ASC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }

        public List<MRNDIssueNote> fetchConfirmedMrndINListByCompanyId(int CompnyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME, STOCK_MAINTAINING_TYPE FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID = AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.RECEIVED_BY = CL.USER_ID " +
                                            "INNER JOIN(SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID, COMPANY_ID, MRN_CODE FROM MRN_MASTER WHERE COMPANY_ID = 6) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                           "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE) AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "WHERE MRNDIN.STATUS = 3 ORDER BY MRNDIN.RECEIVED_ON ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public List<MRNDIssueNote> fetchConfirmedMrndINListByCompanyIdForReturn(int CompnyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT MRNDIN.*, AIM.ITEM_ID, AIM.ITEM_NAME, AIM.STOCK_MAINTAINING_TYPE, CL.RECEIVED_USER, CNL.CONFIRMED_USER,MRND.MRND_ID, MRND.MRN_ID, " +
                                            "MRND.MEASUREMENT_ID , MRNM.SUB_DEPARTMENT_ID,MRNM.MRN_CODE, SD.DEPARTMENT_NAME ,W.LOCATION, M.SHORT_CODE,SUM(DR.RETURN_QTY) AS RETURN_QTY FROM ANCL_BID_QA.dbo.MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME, STOCK_MAINTAINING_TYPE FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID = AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT USER_ID, USER_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) CL ON MRNDIN.RECEIVED_BY = CL.USER_ID " +
                                            "INNER JOIN(SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID, COMPANY_ID, MRN_CODE FROM MRN_MASTER WHERE COMPANY_ID = 6) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                           "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID ASC" +
                                            "LEFT JOIN(SELECT RETURN_QTY, MRND_IN_ID FROM DEPARTMENT_RETURN) AS DR ON DR.MRND_IN_ID = MRNDIN.MRND_IN_ID " +
                                            "WHERE MRNDIN.STATUS = 3 " +
                                            " GROUP BY MRNDIN.MRND_IN_ID, MRNDIN.DELIVERED_BY,MRNDIN.DELIVERED_ON, MRNDIN.ISSUED_BY, MRNDIN.ISSUED_ON, MRNDIN.ISSUED_QTY, MRNDIN.MRND_ID, MRNDIN.ITEM_ID, " +
                                           "MRNDIN.WAREHOUSE_ID, MRNDIN.RECEIVED_BY, MRNDIN.RECEIVED_ON, MRNDIN.STATUS, MRNDIN.MEASUREMENT_ID, MRNDIN.STOCK_VALUE, MRNDIN.RECEIVE_CONFIRMED_BY, MRNDIN.RECEIVE_CONFIRMED_ON, " +
                                           "MRNDIN.REJECTED_BY, MRNDIN.REJECTED_ON,AIM.ITEM_ID, AIM.ITEM_NAME, AIM.STOCK_MAINTAINING_TYPE, CL.RECEIVED_USER, CNL.CONFIRMED_USER,MRND.MRND_ID, MRND.MRN_ID, " +
                                          "MRND.MEASUREMENT_ID , MRNM.SUB_DEPARTMENT_ID,MRNM.MRN_CODE, SD.DEPARTMENT_NAME ,W.LOCATION, M.SHORT_CODE " +
                                           "ORDER BY MRNDIN.RECEIVED_ON ASC ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }
        public int updateIssueNoteAfterDelivered(MRNDIssueNote note, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRND_ISSUE_NOTE SET DELIVERED_BY=" + note.DeliveredBy + ",DELIVERED_ON='" + LocalTime.Now + "',STATUS=2 WHERE MRND_IN_ID=" + note.MrndInID;
            dbConnection.cmd.CommandText += "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + note.MrndID + ", (SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE='DLVRD') , '" + LocalTime.Now + "', " + note.DeliveredBy + ")";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateIssueNoteAfterReceived(MRNDIssueNote note, DBConnection dbConnection)
        {//confirm after receive
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRND_ISSUE_NOTE SET RECEIVE_CONFIRMED_BY=" + note.ReceivedBy + ",RECEIVE_CONFIRMED_ON='" + LocalTime.Now + "',STATUS=3 WHERE MRND_IN_ID=" + note.MrndInID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateIssueNoteAfterRejected(MRNDIssueNote note, int status, DBConnection dbConnection)
        {//reject stock in second approval
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRND_ISSUE_NOTE SET REJECTED_BY=" + note.RejectedBy + ",REJECTED_ON='" + LocalTime.Now + "',STATUS=5 WHERE MRND_IN_ID=" + note.MrndInID;
            dbConnection.cmd.CommandText += "UPDATE " + dbLibrary + ".MRN_DETAILS SET ISSUED_QTY =ISSUED_QTY-" + note.IssuedQty + " , STATUS= " + status + " WHERE MRND_ID=" + note.MrndID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateIssueNoteAfterStockRetured(int MrndInID, DBConnection dbConnection)
        {//stock retured to warehouse approval
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRND_ISSUE_NOTE SET STATUS=6 WHERE MRND_IN_ID=" + MrndInID;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int updateIssueNoteForApproval(int MrndInID, DBConnection dbConnection)
        {//stock retured to warehouse
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRND_ISSUE_NOTE SET STATUS=7 WHERE MRND_IN_ID=" + MrndInID;
            return dbConnection.cmd.ExecuteNonQuery();
        }


        public int updateIssueNoteDepartmentReturn(int Mrnstatus, int RejectedBy, int MrndInID, decimal ReturnQty, int MrndID, decimal IssuesQty, decimal PrevreturnQty, int StockMaitaiinType, DBConnection dbConnection)
        {//return stock from department
            dbConnection.cmd.Parameters.Clear();
            if (StockMaitaiinType == 1)
            {
                if (IssuesQty == PrevreturnQty + ReturnQty)
                {
                    dbConnection.cmd.CommandText = " UPDATE " + dbLibrary + ".MRND_ISSUE_NOTE SET STATUS=6 WHERE MRND_IN_ID=" + MrndInID;
                }
            }
            else
            {
                decimal SumReturn = DAOFactory.CreateDepartmentReturnDAO().SumReturedQty(MrndInID, dbConnection);
                decimal SumIssuesQty = DAOFactory.CreateMRNDIssueNoteDAO().SumIssuesQty(MrndInID, dbConnection);
                if (SumIssuesQty == SumReturn + ReturnQty)
                {
                    dbConnection.cmd.CommandText = " UPDATE " + dbLibrary + ".MRND_ISSUE_NOTE SET STATUS=6 WHERE MRND_IN_ID=" + MrndInID;
                }
            }
            dbConnection.cmd.CommandText += " UPDATE " + dbLibrary + ".MRND_ISSUE_NOTE SET REJECTED_BY=" + RejectedBy + ",REJECTED_ON='" + LocalTime.Now + "' WHERE MRND_IN_ID=" + MrndInID;
            dbConnection.cmd.CommandText += " UPDATE " + dbLibrary + ".MRN_DETAILS SET RECEIVED_QTY =RECEIVED_QTY-" + ReturnQty + " ,ISSUED_QTY =ISSUED_QTY-" + ReturnQty + ", STATUS= " + Mrnstatus + " WHERE MRND_ID=" + MrndID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public decimal SumIssuesQty(int MrndInId, DBConnection dbConnection)
        {
            decimal SumIssuesQty = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT ISSUED_QTY  FROM MRND_ISSUE_NOTE WHERE MRND_IN_ID = " + MrndInId + "";
            SumIssuesQty = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            return SumIssuesQty;
        }

        public int updateIssueNoteBeforeConfirmation(MRNDIssueNote note, DBConnection dbConnection)
        {
            //Receive before Confirmation
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRND_ISSUE_NOTE SET RECEIVED_BY=" + note.ReceivedBy + ",RECEIVED_ON='" + LocalTime.Now + "',STATUS=4 WHERE MRND_IN_ID=" + note.MrndInID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int addNewIssueNoteonewarehouse(MRNDIssueNote note, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".MRND_ISSUE_NOTE (MRND_ID,ITEM_ID,WAREHOUSE_ID,ISSUED_QTY,ISSUED_BY,ISSUED_ON,STATUS) VALUES " +
                      "(" + note.MrndID + "," + note.ItemID + "," + note.WarehouseID + "," + note.IssuedQty + "," + note.IssuedBy + ",'" + LocalTime.Now + "'," + note.Status + "); SELECT SCOPE_IDENTITY(); ";


            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public int updateIssueNoteAfterDeliveredmrnissue(MRNDIssueNote note, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRND_ISSUE_NOTE SET DELIVERED_BY=" + note.DeliveredBy + ",DELIVERED_ON='" + LocalTime.Now + "',STATUS=2 WHERE MRND_IN_ID=" + note.MrndInID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MRNDIssueNote> FetchforIssueNote(List<int> warehouseID, int issunoteId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME FROM ADD_ITEMS_MASTER) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) DCL ON MRNDIN.DELIVERED_BY=DCL.USER_ID " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS ISSUED_USER FROM COMPANY_LOGIN) ICL ON MRNDIN.ISSUED_BY=ICL.USER_ID " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID, MEASUREMENT_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MRND ON MRNDIN.MRND_ID = MRND.MRND_ID " +
                                            "INNER JOIN(SELECT MRN_ID, SUB_DEPARTMENT_ID FROM " + dbLibrary + ".MRN_MASTER) AS MRNM ON MRND.MRN_ID = MRNM.MRN_ID " +
                                            "INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT)AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE)AS W ON MRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT DETAIL_ID, SHORT_CODE FROM " + dbLibrary + ".MEASUREMENT_DETAIL)AS M ON M.DETAIL_ID = MRND.MEASUREMENT_ID " +
                                            "WHERE (MRNDIN.STATUS = 2 OR MRNDIN.STATUS = 3) AND W.WAREHOUSE_ID IN(" + string.Join(",", warehouseID) + ") AND MRNDIN.MRND_IN_ID=" + issunoteId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }

        public string FetchMRNREFNo(int issunoteId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT MM.MRN_CODE FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT MRN_ID,MRND_ID FROM " + dbLibrary + ".MRN_DETAILS) AS MD ON MRNDIN.MRND_ID=MD.MRND_ID " +
                                            "INNER JOIN (SELECT MRN_ID,MRN_CODE FROM " + dbLibrary + ".MRN_MASTER) AS MM ON MM.MRN_ID=MD.MRN_ID " +
                                            "WHERE (MRNDIN.STATUS = 2 OR MRNDIN.STATUS = 3) AND MRNDIN.MRND_IN_ID=" + issunoteId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteScalar().ToString();
        }

        public List<MRNDIssueNote> FetchIssueNoteDetailsByMrnDetailsId(int mrndId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT ITEM_ID, MEASUREMENT_ID  FROM ADD_ITEMS) AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID  " +
                                            "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = AIM.MEASUREMENT_ID \n" +
                                            "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS ISSUED_USER FROM COMPANY_LOGIN) AS ICL ON MRNDIN.ISSUED_BY = ICL.USER_ID  " +

                                            "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) AS CL ON MRNDIN.DELIVERED_BY = CL.USER_ID  " +
                                            "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) AS COL ON MRNDIN.RECEIVED_BY = COL.USER_ID  " +
                                            "WHERE MRND_ID = " + mrndId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }

        public int addNewIssueNote(MRNDIssueNote note, DBConnection dbConnection)
        {
            string sql = "DECLARE @IDs TABLE(ID INT);" +
                        "INSERT INTO " + dbLibrary + ".MRND_ISSUE_NOTE (MRND_ID,ITEM_ID,WAREHOUSE_ID,ISSUED_QTY,ISSUED_BY,ISSUED_ON,STATUS, MEASUREMENT_ID) " +
                        " OUTPUT INSERTED.MRND_IN_ID INTO @IDs(ID)" +
                        " VALUES " +
                        "(" + note.MrndID + "," + note.ItemID + "," + note.WarehouseID + "," + note.IssuedQty + "," + note.IssuedBy + ",'" + LocalTime.Now + "'," + note.Status + "," + note.MeasurementId + "); " +
                        "SELECT ID FROM @IDs;";


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public List<MRNDIssueNote> IssueNoteDetails(int WarehouseId, List<int> DepartmentIds, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            string sql = "SELECT * FROM MRND_ISSUE_NOTE AS MRNDIN " +
                                             "INNER JOIN (SELECT ITEM_ID,ITEM_NAME, MEASUREMENT_ID,CATEGORY_ID, SUB_CATEGORY_ID,STOCK_MAINTAINING_TYPE FROM ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID  " +
                                             "INNER JOIN (SELECT MRND_ID, MRN_ID FROM MRN_DETAILS)  AS MRND ON MRND.MRND_ID = MRNDIN.MRND_ID \n" +
                                             "INNER JOIN (SELECT MRN_ID, MRN_CODE, SUB_DEPARTMENT_ID, WAREHOUSE_ID FROM MRN_MASTER) AS MRM ON MRM.MRN_ID = MRND.MRN_ID " +
                                             "INNER JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT WHERE SUB_DEPARTMENT_ID IN (" + string.Join(",", DepartmentIds) + ")) AS SUB ON SUB.SUB_DEPARTMENT_ID = MRM.SUB_DEPARTMENT_ID  " +
                                            //  "INNER JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT WHERE SUB_DEPARTMENT_ID ="+ DepartmentId + ") AS SUB ON SUB.SUB_DEPARTMENT_ID = MRM.SUB_DEPARTMENT_ID  " +
                                            "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = MRNDIN.MEASUREMENT_ID " +
                                             "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) AS CL ON MRNDIN.DELIVERED_BY = CL.USER_ID   " +
                                             "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) AS COL ON MRNDIN.RECEIVED_BY = COL.USER_ID  " +
                                              "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS ISSUED_USER FROM COMPANY_LOGIN) AS IUCL ON MRNDIN.ISSUED_BY = IUCL.USER_ID   " +
                                               "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY_MASTER) AS ICTM ON ICTM.CATEGORY_ID = AIM.CATEGORY_ID   " +
                                               "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE) AS W ON W.WAREHOUSE_ID = MRM.WAREHOUSE_ID   " +
                                             "WHERE MRNDIN.WAREHOUSE_ID = " + WarehouseId + " AND ISSUED_ON >= '" + fromDate + "' AND ISSUED_ON <=  DATEADD(DAY, 1,'" + toDate + "') ";

            if (maincategoryid != 0)
            {
                sql += " AND AIM.CATEGORY_ID =  " + maincategoryid + "";
            }

            if (subcategoryid != 0)
            {
                sql += " AND AIM.SUB_CATEGORY_ID = " + subcategoryid + "";
            }
            if (itemid != 0)
            {
                sql += " AND AIM.ITEM_ID =  " + itemid + "";
            }

            sql += " ORDER BY MRNDIN.ISSUED_ON DESC ";


            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }

        public List<MRNDIssueNote> ReceivedMRNDetails(int CompanyId, List<int> WarehouseId, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            string sql = "SELECT * FROM MRND_ISSUE_NOTE AS MRNDIN " +
                                            "INNER JOIN (SELECT WAREHOUSE_ID, lOCATION FROM WAREHOUSE) AS W ON W.WAREHOUSE_ID = MRNDIN.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME, MEASUREMENT_ID,CATEGORY_ID, SUB_CATEGORY_ID,STOCK_MAINTAINING_TYPE  FROM ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON MRNDIN.ITEM_ID=AIM.ITEM_ID  " +
                                            "INNER JOIN (SELECT MRND_ID, MRN_ID FROM MRN_DETAILS)  AS MRND ON MRND.MRND_ID = MRNDIN.MRND_ID \n" +
                                            "INNER JOIN (SELECT MRN_ID, MRN_CODE, SUB_DEPARTMENT_ID, WAREHOUSE_ID FROM MRN_MASTER) AS MRM ON MRM.MRN_ID = MRND.MRN_ID " +
                                            "INNER JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT WHERE SUB_DEPARTMENT_ID = " + CompanyId + ") AS SUB ON SUB.SUB_DEPARTMENT_ID = MRM.SUB_DEPARTMENT_ID  " +
                                            "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = MRNDIN.MEASUREMENT_ID " +
                                            "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) AS CL ON MRNDIN.DELIVERED_BY = CL.USER_ID   " +
                                            "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) AS COL ON MRNDIN.RECEIVED_BY = COL.USER_ID  " +
                                            "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS ISSUED_USER FROM COMPANY_LOGIN) AS IU ON MRNDIN.ISSUED_BY = IU.USER_ID  " +
                                            "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY_MASTER) AS ICTM ON ICTM.CATEGORY_ID = AIM.CATEGORY_ID   " +
                                            "INNER JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SU ON SU.SUB_DEPARTMENT_ID = MRM.SUB_DEPARTMENT_ID  " +
                                            "WHERE MRNDIN.WAREHOUSE_ID IN (" + string.Join(",", WarehouseId) + ") AND MRNDIN.STATUS = 3 AND ISSUED_ON >= '" + fromDate + "' AND ISSUED_ON <= DATEADD(DAY, 1,'" + toDate + "') ";

            if (maincategoryid != 0)
            {
                sql += " AND AIM.CATEGORY_ID =  " + maincategoryid + "";
            }

            if (subcategoryid != 0)
            {
                sql += " AND AIM.SUB_CATEGORY_ID = " + subcategoryid + "";
            }
            if (itemid != 0)
            {
                sql += " AND AIM.ITEM_ID =  " + itemid + "";
            }

            sql += " ORDER BY MRNDIN.ISSUED_ON DESC";


            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDIssueNote>(dbConnection.dr);
            }
        }

        public MRNDIssueNote FetcMrndIssueNoteByMrndInId(int mrndInId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRND_ISSUE_NOTE AS MRNDIN " +
                                           "WHERE MRND_IN_ID = " + mrndInId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MRNDIssueNote>(dbConnection.dr);
            }
        }
    }
}
