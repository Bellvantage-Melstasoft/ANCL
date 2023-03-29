using CLibrary.Common;
using CLibrary.Domain;
using System.Collections.Generic;

namespace CLibrary.Infrastructure {
    public interface TRDIssueNoteDAO {
       // int addNewIssueNote(List<TRDIssueNote> notes, DBConnection dbConnection);
        List<TRDIssueNote> fetchDeliveredTRdINList(List<int> warehouseIds, DBConnection dbConnection);
        List<TRDIssueNote> fetchReceivedTrdINList(List<int> warehouseIds, DBConnection dbConnection);
        int updateIssueNoteAfterReceived(TRDIssueNote note, DBConnection dbConnection);
        List<TRDIssueNote> fetchIssueNoteDetails(int TrdId, DBConnection dbConnection);
        List<TRDIssueNote> IssueNoteDetails(int WarehouseId, List<int> WarehouseIds, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid, DBConnection dbConnection);
        List<TRDIssueNote> ReceivedTRDetails(int ReceivedToWarehouse, List<int> WarehouseId, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid, DBConnection dbConnection);
        int addNewIssueNote(List<TRDIssueNote> notes, DBConnection dbConnection);
    }

    public class TRDIssueNoteSQLImpl : TRDIssueNoteDAO {

        public int addNewIssueNote(List<TRDIssueNote> notes, DBConnection dbConnection)
        {
            string sql = "";
            foreach (TRDIssueNote note in notes)
            {
                sql += "DECLARE @TRDIN_ID TABLE(TRD_IN_ID INT) \n";
                sql += "INSERT INTO TRD_ISSUE_NOTE (TRD_ID,ITEM_ID,WAREHOUSE_ID,ISSUED_QTY,ISSUED_BY,ISSUED_ON,DELIVERED_BY, DELIVERED_ON,STATUS,ISSUED_STOCK_VALUE,MEASUREMENT_ID) OUTPUT INSERTED.TRD_IN_ID INTO @TRDIN_ID VALUES " +
                      "(" + note.TRDId + "," + note.ItemId + "," + note.WarehouseId + "," + note.IssuedQTY + "," + note.IssuedBy + ",'" + LocalTime.Now + "'," + note.IssuedBy + ",'" + LocalTime.Now + "', 2," + note.IssuedStockValue + "," + note.MeasurementId + ");  \n\n";

                foreach (var batch in note.IssuedBatches)
                {
                    sql += $@"INSERT INTO MRND_ISSUE_NOTE_BATCHES
                                VALUES
                                ((SELECT MAX(TRD_IN_ID) FROM @TRDIN_ID),{batch.BatchId},{batch.IssuedQty},{batch.IssuedStockValue},{note.MeasurementId}) \n\n";
                }

                sql += "SELECT MAX(TRD_IN_ID) FROM @TRDIN_ID; \n";
            }
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }
        //public int addNewIssueNote(List<TRDIssueNote> notes, DBConnection dbConnection) {
        //    string sql = "";
        //    foreach (TRDIssueNote note in notes) {
        //        sql += "DECLARE @TRDIN_ID TABLE(TRD_IN_ID INT) \n";
        //        sql += "INSERT INTO TRD_ISSUE_NOTE (TRD_ID,ITEM_ID,WAREHOUSE_ID,ISSUED_QTY,ISSUED_BY,ISSUED_ON,DELIVERED_BY, DELIVERED_ON,STATUS,ISSUED_STOCK_VALUE) OUTPUT INSERTED.TRD_IN_ID INTO @TRDIN_ID VALUES " +
        //              "(" + note.TRDId + "," + note.ItemId + "," + note.WarehouseId + "," + note.IssuedQTY + "," + note.IssuedBy + ",'" + LocalTime.Now + "'," + note.IssuedBy + ",'" + LocalTime.Now + "', 2," + note.IssuedStockValue + ");  \n\n";

        //        foreach (var batch in note.IssuedBatches) {
        //            sql += $@"INSERT INTO MRND_ISSUE_NOTE_BATCHES
        //                        VALUES
        //                        ((SELECT MAX(TRD_IN_ID) FROM @TRDIN_ID),{batch.BatchId},{batch.IssuedQty},{batch.IssuedStockValue}) \n\n";
        //        }

        //        sql += "SELECT MAX(TRD_IN_ID) FROM @TRDIN_ID; \n";
        //    }
        //    dbConnection.cmd.Parameters.Clear();
        //    dbConnection.cmd.CommandText = sql;
        //    return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        //}

        public List<TRDIssueNote> fetchDeliveredTRdINList(List<int> warehouseIds, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM TRD_ISSUE_NOTE AS TRDIN " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME, MEASUREMENT_ID  FROM ADD_ITEMS) AS AIM ON TRDIN.ITEM_ID = AIM.ITEM_ID " +
                                            "LEFT JOIN(SELECT TRD_ID, TR_ID FROM TR_DETAILS) AS TRD ON TRDIN.TRD_ID = TRD.TRD_ID " +
                                            "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = AIM.MEASUREMENT_ID " +
                                            "INNER JOIN(SELECT TR_ID, TR_CODE, FROM_WAREHOUSE_ID FROM TR_MASTER) AS TRM ON TRD.TR_ID = TRM.TR_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE)AS W ON TRDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT USER_ID, FIRST_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) AS CL ON TRDIN.DELIVERED_BY = CL.USER_ID " +
                                            "WHERE TRDIN.STATUS = 2 AND W.WAREHOUSE_ID IN(" + string.Join(",", warehouseIds) + ")";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TRDIssueNote>(dbConnection.dr);
            }
        }

        public List<TRDIssueNote> fetchReceivedTrdINList(List<int> warehouseIds, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM TRD_ISSUE_NOTE AS TRDIN " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME, MEASUREMENT_ID FROM ADD_ITEMS_MASTER) AS AIM ON TRDIN.ITEM_ID = AIM.ITEM_ID " +
                                            "INNER JOIN(SELECT USER_ID, FIRST_NAME AS RECEIVED_USER FROM COMPANY_LOGIN) CLR ON TRDIN.RECEIVED_BY = CLR.USER_ID " +
                                            "LEFT JOIN(SELECT MEASUREMENT_ID, MEASUREMENT_SHORT_NAME FROM UNIT_MEASUREMENT) AS UN ON UN.MEASUREMENT_ID = AIM.MEASUREMENT_ID " +
                                            "INNER JOIN(SELECT USER_ID, FIRST_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) AS CLD ON TRDIN.DELIVERED_BY = CLD.USER_ID " +
                                            "INNER JOIN(SELECT TRD_ID, TR_ID FROM TR_DETAILS) AS TRD ON TRDIN.TRD_ID = TRD.TRD_ID " +
                                            "INNER JOIN(SELECT TR_ID, TR_CODE, FROM_WAREHOUSE_ID FROM TR_MASTER) AS TRM ON TRD.TR_ID = TRM.TR_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM ONLINE_BIDDING.dbo.WAREHOUSE)AS W ON TRDIN.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "WHERE TRDIN.STATUS = 3 AND W.WAREHOUSE_ID IN(" + string.Join(",", warehouseIds) + ")";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TRDIssueNote>(dbConnection.dr);
            }
        }
        public int updateIssueNoteAfterReceived(TRDIssueNote note, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE TRD_ISSUE_NOTE SET RECEIVED_BY=" + note.ReceivedBy + ",RECEIVED_ON='" + LocalTime.Now + "',STATUS=3 WHERE TRD_IN_ID=" + note.TRDInId;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TRDIssueNote> fetchIssueNoteDetails(int TrdId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM TRD_ISSUE_NOTE AS TRDIN " +
                                            "INNER JOIN (SELECT ITEM_ID, MEASUREMENT_ID  FROM ADD_ITEMS) AS AIM ON TRDIN.ITEM_ID=AIM.ITEM_ID  " +
                                            "LEFT JOIN (SELECT MEASUREMENT_ID, MEASUREMENT_SHORT_NAME FROM UNIT_MEASUREMENT) AS UN ON UN.MEASUREMENT_ID = AIM.MEASUREMENT_ID \n" +
                                            "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) AS CL ON TRDIN.DELIVERED_BY = CL.USER_ID  " +
                                            "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) AS COL ON TRDIN.RECEIVED_BY = COL.USER_ID  " +
                                            "WHERE TRD_ID = " + TrdId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TRDIssueNote>(dbConnection.dr);
            }
        }

        public List<TRDIssueNote> IssueNoteDetails(int WarehouseId, List<int> WarehouseIds, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            string sql = "SELECT * FROM TRD_ISSUE_NOTE AS TRDIN " +
                                             "INNER JOIN (SELECT ITEM_ID,ITEM_NAME, MEASUREMENT_ID,CATEGORY_ID, SUB_CATEGORY_ID, STOCK_MAINTAINING_TYPE FROM ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON TRDIN.ITEM_ID=AIM.ITEM_ID  " +
                                             "INNER JOIN (SELECT TRD_ID, TR_ID FROM TR_DETAILS)  AS TRD ON TRD.TRD_ID = TRDIN.TRD_ID \n" +
                                             "INNER JOIN (SELECT TR_ID, TR_CODE, FROM_WAREHOUSE_ID FROM TR_MASTER) AS TRM ON TRM.TR_ID = TRD.TR_ID " +
                                             "INNER JOIN (SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE WHERE WAREHOUSE_ID IN (" + string.Join(",", WarehouseIds) + ")) AS W ON W.WAREHOUSE_ID = TRM.FROM_WAREHOUSE_ID  " +
                                             "LEFT JOIN (SELECT MEASUREMENT_ID, MEASUREMENT_SHORT_NAME FROM UNIT_MEASUREMENT) AS UN ON UN.MEASUREMENT_ID = AIM.MEASUREMENT_ID " +
                                             "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) AS CL ON TRDIN.DELIVERED_BY = CL.USER_ID   " +
                                             "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) AS COL ON TRDIN.RECEIVED_BY = COL.USER_ID  " +
                                             "WHERE TRDIN.WAREHOUSE_ID = " + WarehouseId + " AND ISSUED_ON >= '" + fromDate + "' AND ISSUED_ON <= '" + toDate + "' ";

            if (maincategoryid != 0) {
                sql += " AND AIM.CATEGORY_ID =  " + maincategoryid + "";
            }

            if (subcategoryid != 0) {
                sql += " AND AIM.SUB_CATEGORY_ID = " + subcategoryid + "";
            }
            if (itemid != 0) {
                sql += " AND AIM.ITEM_ID =  " + itemid + "";
            }

            sql += " ORDER BY AIM.ITEM_NAME";


            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TRDIssueNote>(dbConnection.dr);
            }
        }


        public List<TRDIssueNote> ReceivedTRDetails(int ReceivedToWarehouse, List<int> WarehouseId, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            string sql = "SELECT * FROM TRD_ISSUE_NOTE AS TRDIN " +
                                            "INNER JOIN (SELECT WAREHOUSE_ID, lOCATION FROM WAREHOUSE) AS W ON W.WAREHOUSE_ID = TRDIN.WAREHOUSE_ID " +
                                            "INNER JOIN (SELECT ITEM_ID,ITEM_NAME, MEASUREMENT_ID,CATEGORY_ID, SUB_CATEGORY_ID, STOCK_MAINTAINING_TYPE  FROM ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON TRDIN.ITEM_ID=AIM.ITEM_ID  " +
                                            "INNER JOIN (SELECT TRD_ID, TR_ID FROM TR_DETAILS)  AS TRD ON TRD.TRD_ID = TRDIN.TRD_ID \n" +
                                            "INNER JOIN (SELECT TR_ID, TR_CODE, FROM_WAREHOUSE_ID FROM TR_MASTER) AS TRM ON TRM.TR_ID = TRD.TR_ID " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, lOCATION FROM WAREHOUSE WHERE WAREHOUSE_ID = " + ReceivedToWarehouse + ") AS WH ON WH.WAREHOUSE_ID = TRM.FROM_WAREHOUSE_ID " +
                                            "LEFT JOIN (SELECT MEASUREMENT_ID, MEASUREMENT_SHORT_NAME FROM UNIT_MEASUREMENT) AS UN ON UN.MEASUREMENT_ID = AIM.MEASUREMENT_ID " +
                                            "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) AS CL ON TRDIN.DELIVERED_BY = CL.USER_ID   " +
                                            "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS DELIVERED_USER FROM COMPANY_LOGIN) AS COL ON TRDIN.RECEIVED_BY = COL.USER_ID  " +
                                            "WHERE TRDIN.WAREHOUSE_ID IN (" + string.Join(",", WarehouseId) + ") AND TRDIN.STATUS = 3 AND ISSUED_ON >= '" + fromDate + "' AND ISSUED_ON <= '" + toDate + "' ";

            if (maincategoryid != 0) {
                sql += " AND AIM.CATEGORY_ID =  " + maincategoryid + "";
            }

            if (subcategoryid != 0) {
                sql += " AND AIM.SUB_CATEGORY_ID = " + subcategoryid + "";
            }
            if (itemid != 0) {
                sql += " AND AIM.ITEM_ID =  " + itemid + "";
            }

            sql += " ORDER BY AIM.ITEM_NAME";


            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TRDIssueNote>(dbConnection.dr);
            }
        }

    }
}