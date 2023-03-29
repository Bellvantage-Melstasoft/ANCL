using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Helper;

namespace CLibrary.Infrastructure {
    public interface InventoryDAOInterface {
        int addNewCompanyStock(int companyID, int itemID, int reOderedLevel, int addedBy, DBConnection dbConnection);
        List<Inventory> fetchWarehouseInventory(int companyID, int itemID, DBConnection dbConnection);

        int updateCompanyStockAfterIssue(int companyID, int itemID, decimal issuedQty, int updatedBy, decimal IssuedValue, DBConnection dbConnection);
        int updateWarehouseStockAfterIssue(List<Inventory> inventoryObjList, DBConnection dbConnection);
        int updateWarehouseStockAfterDelivered(int mrndInID, int warehouseID, int itemID, decimal DeliveredQty, int updatedBy, decimal deliveredStockValue, List<IssuedInventoryBatches> Batches, DBConnection dbConnection);
        int releaseStockFromWarehouseToWarehouse(WarehouseInventoryRelease inventoryObj, DBConnection dbConnection);
        int raiseCompanyStockFromGRN(int companyID, int itemID, int receivedQty, decimal stockValue, int raisedBy, DBConnection dbConnection);
        int raiseWarehouseStockFromGRN(List<WarehouseInventoryRaise> inventoryList, DBConnection dbConnection);
        List<WarehouseInventory> fetchWarehouseItems(int warehouseID, DBConnection dbConnection);
        List<Inventory> fetchWarehouseInventorybyWarehouseId(int companyID, int warehouseId, int itemID, DBConnection dbConnection);
        int raiseWarehouseStockInMrn(WarehouseInventoryRaise inventory, DBConnection dbConnection);
        int raiseCompanyStockInMrn(int companyID, int itemID, decimal receivedQty, decimal stockValue, int raisedBy, DBConnection dbConnection);
        int updateWarehouseStockAfterIssuesformonewarehouse(Inventory inventoryObjList, DBConnection dbConnection);
        int raiseWarehouseStockInMrnManual(WarehouseInventoryRaise inventory, DateTime expDate, DBConnection dbConnection);
        decimal GetWarehouseInventoryForItem(int warehouseId, int ItemId, DBConnection dbConnection);
        void updateCompanyStock(int warehouseId, MrnDetailsV2 mrnDetail, int updatedBy, DBConnection dbConnection);
        int addWarehouseStock(List<WarehouseInventory> inventoryList, int UserId, int ItemId, int CompanyId, string Remarks, DBConnection dbConnection);
        int updateWarehouseStock(List<WarehouseInventory> inventoryList, int UserId, int ItemId, int CompanyId, string Remarks, List<WarehouseInventoryBatches> deletedBatches, int StockMAintainingType, List<WarehouseInventoryBatches> addedBatches, List<WarehouseInventoryBatches> batchList, DBConnection dbConnection);
        WarehouseInventoryDetail GetWarehouseInventoryDetailToIssue(int ItemId, int WarehouseId, int CompanyId, DBConnection dbConnection);
        int updateWarehouseStockAfterTRIssue(List<Inventory> inventoryObjList, DBConnection dbConnection);
        int updateWarehouseStockAfterTRDelivered(int trdInID, int warehouseID, int itemID, decimal DeliveredQty, int updatedBy, decimal deliveredStockValue, DBConnection dbConnection);
        //Reorder function stock by Pasindu 2020/04/29
        List<WarehouseInventory> GetWarehouseLowInventory(int companyid, int warehouseid, DBConnection dbConnection);
        //stock varificatio function by Pasindu 2020/05/04
        List<DailyStockSummary> GetMonthEndStock(int companyid, int warehouseid, DateTime date, DBConnection dbConnection);

        List<Inventory> fetchWarehouseInventoryTR(int TRId, int itemID, DBConnection dbConnection);
        List<IssuedInventoryBatches> GetIssuedInventoryBatches(int WarehouseId, int ItemId, decimal IssuedQty, int StockType, DBConnection dbConnection);
        int UpdateStock(int ItemId, int WarehouseId, decimal Qty, int UserId, int companyId, decimal up, DateTime expDate, DBConnection dbConnection);
        Inventory getStockValues(int ItemId, int WarehouseId, DBConnection dbConnection);
        Inventory getAvailableStockValues(int ItemId, int WarehouseId, DBConnection dbConnection);
        Inventory getBatchStockValues(int ItemId, int batchId, int WarehouseId, DBConnection dbConnection);
        List<WarehouseInventory> FetchItemListDetailed(int companyid, int warehouseid, int itemid, int maincategoryid, int subcategoryid, DBConnection dbConnection);
        List<WarehouseInventory> GetInventoryByItemId(int itemID, DBConnection dbConnection);
        int UpdateInventoryMasterForGrnReturn(int warehouseId, int itemID, int Userid, decimal qty, decimal total, DBConnection dbConnection);
        int UpdateInventoryDetailForGrnReturn(int warehouseId, int itemID, int Userid, decimal qty, decimal total, int GrndId, DBConnection dbConnection);
        int SaveEditedMasterInventory(int warehouseId, int ItemId, decimal newQty, decimal newStockValue, int updatedBy, DBConnection dbConnection);
        int SaveEditedBatchInventory(int WarehouseId, int ItemId, int UserId, int BatchId, decimal newQtyBatch, decimal newStockValueBatch, DBConnection dbConnection);
        int ReturnMasterStock(int ItemID, int WarehouseID, decimal IssuedQty, decimal StockValue, int UserId, DBConnection dbConnection);
        int ReturnBatchStock(int BatchId, decimal IssuedQty, decimal IssuedStockValue, int WarehouseID, int ItemID, int UserId, DBConnection dbConnection);
        WarehouseInventory GetInventoryByItemIdAndWarehouseId(int itemID, int warehouseId, int CompayId, DBConnection dbConnection);
        Inventory InventoryBatchesByBatchId(int BatchId, int warehouseId, int itemID, DBConnection dbConnection);

    }
        class InventoryDAO : InventoryDAOInterface {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
        public List<IssuedInventoryBatches> GetIssuedInventoryBatches(int WarehouseId, int ItemId, decimal IssuedQty, int StockType, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "GET_ISSUED_STOCK_BATCHES";

            dbConnection.cmd.Parameters.AddWithValue("@WAREHOUSE_ID", WarehouseId);
            dbConnection.cmd.Parameters.AddWithValue("@ITEM_ID", ItemId);
            dbConnection.cmd.Parameters.AddWithValue("@ISSUED_QTY", IssuedQty);
            dbConnection.cmd.Parameters.AddWithValue("@STOCK_TYPE", StockType);

            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<IssuedInventoryBatches>(dbConnection.dr);

            }
        }

        public List<Inventory> fetchWarehouseInventoryTR(int TRId, int itemID, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT WIM.WAREHOUSE_ID,WIM.ITEM_ID,WIM.AVAILABLE_QTY,WIM.HOLDED_QTY,WIM.STOCK_VALUE,WIM.IS_ACTIVE,W.LOCATION,AIM.ITEM_NAME FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER AS WIM " +
                                            "INNER JOIN (SELECT WAREHOUSE_ID, LOCATION, COMPANY_ID FROM " + dbLibrary + ".WAREHOUSE) AS W ON WIM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME,STOCK_MAINTAINING_TYPE FROM " + dbLibrary + ".ADD_ITEMS WHERE COMPANY_ID =(SELECT TO_WAREHOUSE_ID FROM TR_MASTER WHERE TR_ID=" + TRId + ")) AS AIM " +
                                            "ON WIM.ITEM_ID = AIM.ITEM_ID " +
                                            "WHERE W.WAREHOUSE_ID = (SELECT TO_WAREHOUSE_ID FROM TR_MASTER WHERE TR_ID=" + TRId + ") AND WIM.ITEM_ID = " + itemID + " AND WIM.AVAILABLE_QTY > 0";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Inventory>(dbConnection.dr);
            }
        }
        public int addNewCompanyStock(int companyID, int itemID, decimal reOderedLevel, int addedBy, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".COMPANY_INVENTORY_MASTER (COMPANY_ID,ITEM_ID,AVAILABLE_QTY,STOCK_VALUE,REORDER_LEVEL,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                                           "VALUES (" + companyID + "," + itemID + ",0,0," + reOderedLevel + ",'" + LocalTime.Now + "'," + addedBy + ",1)";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int addNewCompanyStock(int companyID, int itemID, int reOderedLevel, int addedBy, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".COMPANY_INVENTORY_MASTER (COMPANY_ID,ITEM_ID,AVAILABLE_QTY,STOCK_VALUE,REORDER_LEVEL,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                                           "VALUES (" + companyID + "," + itemID + ",0,0," + reOderedLevel + ",'" + LocalTime.Now + "'," + addedBy + ",1)";
            return dbConnection.cmd.ExecuteNonQuery();
        }
        //Reorder function stock by Pasindu 2020/04/29
        public List<WarehouseInventory> GetWarehouseLowInventory(int companyid, int warehouseid, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            string sql = "SELECT W.WAREHOUSE_ID,W.LOCATION,AI.ITEM_NAME, AI.ITEM_ID, WIM.AVAILABLE_QTY,WIM.HOLDED_QTY, UN.SHORT_CODE AS MEASUREMENT_SHORT_NAME, WIM.REORDER_LEVEL, IC.CATEGORY_NAME, IC.CATEGORY_ID, ISC.SUB_CATEGORY_NAME, ISC.SUB_CATEGORY_ID FROM WAREHOUSE_INVENTORY_MASTER AS WIM  \n" +
                            "INNER JOIN ADD_ITEMS AS AI ON WIM.ITEM_ID = AI.ITEM_ID \n" +
                            "INNER JOIN ITEM_SUB_CATEGORY AS ISC ON ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID  \n" +
                             "INNER JOIN ITEM_CATEGORY AS IC ON IC.CATEGORY_ID = ISC.CATEGORY_ID \n" +
                            "INNER JOIN WAREHOUSE AS W ON WIM.WAREHOUSE_ID = W.WAREHOUSE_ID \n" +
                            "LEFT JOIN MEASUREMENT_DETAIL AS UN ON UN.DETAIL_ID = AI.MEASUREMENT_ID \n" +
                            "WHERE WIM.AVAILABLE_QTY <= WIM.REORDER_LEVEL AND AI.COMPANY_ID = " + companyid + " AND IC.COMPANY_ID = " + companyid + " AND ISC.COMPANY_ID = " + companyid + " AND W.IS_ACTIVE = 1 AND IC.CATEGORY_NAME != 'Undefined' AND W.WAREHOUSE_ID = " + warehouseid + " ORDER BY AI.ITEM_NAME ";


            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<WarehouseInventory>(dbConnection.dr);
            }
        }
        //stock varificatio function by Pasindu 2020/05/04
        public List<DailyStockSummary> GetMonthEndStock(int companyid, int warehouseid, DateTime date, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            string sql = "SELECT * FROM DAILY_STOCK_SUMMARY AS DS  \n" +
                            "INNER JOIN (SELECT ITEM_ID, ITEM_NAME, REFERENCE_NO, MEASUREMENT_ID FROM ADD_ITEMS WHERE COMPANY_ID = " + companyid + ") AS AI ON AI.ITEM_ID = DS.ITEM_ID \n" +
                            "LEFT JOIN (SELECT SHORT_CODE AS MEASUREMENT_SHORT_NAME,DETAIL_ID AS MEASUREMENT_ID FROM MEASUREMENT_DETAIL) AS UM ON UM.MEASUREMENT_ID = AI.MEASUREMENT_ID  \n" +
                             "WHERE DS.WAREHOUSE_ID = " + warehouseid + " AND MONTH(DS.DATE)= " + date.Month + " AND YEAR(DS.DATE)= " + date.Year + " AND DAY(DS.DATE) = DAY(EOMONTH('" + date + "')) \n" +
                             "ORDER BY AI.ITEM_NAME ";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<DailyStockSummary>(dbConnection.dr);
            }
        }

        public int addWarehouseStock(List<WarehouseInventory> inventoryList, int UserId, int ItemId, int CompanyId, string Remarks, DBConnection dbConnection) {
            string sql = "";

            for (int i = 0; i < inventoryList.Count; i++) {
                sql += "INSERT INTO WAREHOUSE_INVENTORY_MASTER \n" +
                        "VALUES \n" +
                        "(" + inventoryList[i].WarehouseID + "," + ItemId + "," + inventoryList[i].AvailableQty + ",0," + inventoryList[i].StockValue + ",'" + LocalTime.Now + "'," + UserId + ",1, " + inventoryList[i].ReorderLevel + ",0) \n";

                sql += "INSERT INTO [STOCK_OVERRIDE_LOG] \n" +
                        "VALUES \n" +
                        "(" + ItemId + "," + inventoryList[i].WarehouseID + ",0,0," + inventoryList[i].AvailableQty + "," + inventoryList[i].StockValue + "," + UserId + ",'" + LocalTime.Now + "',1,NULL,'" + Remarks.ProcessString() + "') \n";
            }

            sql += "UPDATE COMPANY_INVENTORY_MASTER SET AVAILABLE_QTY =" + inventoryList.Sum(il => il.AvailableQty) + ", STOCK_VALUE=" + inventoryList.Sum(il => il.StockValue) + ", REORDER_LEVEL =" + inventoryList.Sum(il => il.ReorderLevel) + " WHERE COMPANY_ID =" + CompanyId + " AND ITEM_ID=" + ItemId + " \n";


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public List<Inventory> fetchWarehouseInventory(int companyID, int itemID, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT WIM.WAREHOUSE_ID,WIM.ITEM_ID,WIM.AVAILABLE_QTY,WIM.HOLDED_QTY,WIM.IS_ACTIVE,W.LOCATION,AIM.ITEM_NAME FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER AS WIM " +
                                            "INNER JOIN (SELECT WAREHOUSE_ID, LOCATION, COMPANY_ID FROM " + dbLibrary + ".WAREHOUSE) AS W ON WIM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME FROM " + dbLibrary + ".ADD_ITEMS_MASTER) AS AIM " +
                                            "ON WIM.ITEM_ID = AIM.ITEM_ID " +
                                            "WHERE W.COMPANY_ID = " + companyID + " AND WIM.ITEM_ID = " + itemID + " AND WIM.AVAILABLE_QTY > 0";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Inventory>(dbConnection.dr);
            }
        }

        public List<WarehouseInventory> GetInventoryByItemId(int itemID, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = "+ itemID + " AND AVAILABLE_QTY > 0 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<WarehouseInventory>(dbConnection.dr);
            }
        }

        public WarehouseInventory GetInventoryByItemIdAndWarehouseId(int itemID, int warehouseId, int CompayId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + itemID + "AND WAREHOUSE_ID = "+ warehouseId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<WarehouseInventory>(dbConnection.dr);
            }
        }

        public List<WarehouseInventory> fetchWarehouseItems(int warehouseID, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT *,(NULLIF(WIM.STOCK_VALUE,0)/NULLIF(WIM.AVAILABLE_QTY,0)) AS UNIT_PRICE FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER AS WIM " +
                                            "INNER JOIN (SELECT WAREHOUSE_ID, LOCATION FROM " + dbLibrary + ".WAREHOUSE) AS W " +
                                            "ON WIM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME, SUB_CATEGORY_ID FROM " + dbLibrary + ".ADD_ITEMS_MASTER) AS AIM " +
                                            "ON WIM.ITEM_ID = AIM.ITEM_ID " +
                                            "INNER JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME, CATEGORY_ID FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER) AS ISCM " +
                                            "ON AIM.SUB_CATEGORY_ID = ISCM.SUB_CATEGORY_ID " +
                                            "INNER JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER) AS ICM " +
                                            "ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                                            "WHERE WIM.WAREHOUSE_ID = " + warehouseID + " AND WIM.AVAILABLE_QTY > 0";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<WarehouseInventory>(dbConnection.dr);
            }
        }

        public int raiseCompanyStockFromGRN(int companyID, int itemID, int receivedQty, decimal stockValue, int raisedBy, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF EXISTS (SELECT * FROM " + dbLibrary + ".COMPANY_INVENTORY_MASTER WHERE COMPANY_ID = " + companyID + " AND ITEM_ID = " + itemID + ") " +
                                                "UPDATE " + dbLibrary + ".COMPANY_INVENTORY_MASTER SET STOCK_VALUE= STOCK_VALUE+" + stockValue + ", AVAILABLE_QTY= AVAILABLE_QTY+" + receivedQty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + raisedBy + " WHERE COMPANY_ID = " + companyID + " AND ITEM_ID = " + itemID + " " +
                                            "ELSE " +
                                                "INSERT INTO " + dbLibrary + ".COMPANY_INVENTORY_MASTER (COMPANY_ID,ITEM_ID,AVAILABLE_QTY,STOCK_VALUE,REORDER_LEVEL,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                                                "VALUES (" + companyID + "," + itemID + "," + receivedQty + "," + stockValue + ",0,'" + LocalTime.Now + "'," + raisedBy + ",1)";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateInventoryMasterForGrnReturn(int warehouseId, int itemID, int Userid, decimal qty, decimal total,  DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE WAREHOUSE_INVENTORY_MASTER SET AVAILABLE_QTY = AVAILABLE_QTY - "+ qty + ", STOCK_VALUE = STOCK_VALUE - "+ total + ", LAST_UPDATED_BY = "+Userid+", LAST_UPDATED_DATE = '"+LocalTime.Now+"' WHERE WAREHOUSE_ID = "+ warehouseId + " AND ITEM_ID = "+ itemID + " ";
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int UpdateInventoryDetailForGrnReturn(int warehouseId, int itemID, int Userid, decimal qty, decimal total, int GrndId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE WAREHOUSE_INVENTORY_BATCHES SET AVAILABLE_QTY = AVAILABLE_QTY - " + qty + ", STOCK_VALUE = STOCK_VALUE - " + total + ", LAST_UPDATED_BY = " + Userid + ", LAST_UPADATED_DATE = '" + LocalTime.Now + "' WHERE WAREHOUSE_ID = " + warehouseId + " AND ITEM_ID = " + itemID + " AND GRND_ID = "+GrndId+" ";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int raiseWarehouseStockFromGRN(List<WarehouseInventoryRaise> inventoryList, DBConnection dbConnection) {
            string sql = "";

            foreach (WarehouseInventoryRaise inventoryObj in inventoryList) {
                sql += "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_RAISE (WAREHOUSE_ID,ITEM_ID,RAISED_QTY,RAISED_DATE,RAISED_BY,RAISED_TYPE,GRND_ID) " +
                        "VALUES (" + inventoryObj.WarehouseID + "," + inventoryObj.ItemID + "," + inventoryObj.RaisedQty + ",'" + LocalTime.Now + "'," + inventoryObj.RaisedBy + ",1," + inventoryObj.GrndID + ");" +
                        "IF EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + inventoryObj.WarehouseID + " AND ITEM_ID = " + inventoryObj.ItemID + ") " +
                            "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER SET STOCK_VALUE= STOCK_VALUE+" + inventoryObj.StockValue + ", AVAILABLE_QTY=AVAILABLE_QTY+" + inventoryObj.RaisedQty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + inventoryObj.RaisedBy + " WHERE WAREHOUSE_ID = " + inventoryObj.WarehouseID + " AND ITEM_ID = " + inventoryObj.ItemID + " " +
                        "ELSE " +
                            "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER (WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                            "VALUES (" + inventoryObj.WarehouseID + "," + inventoryObj.ItemID + "," + inventoryObj.RaisedQty + ",0," + inventoryObj.StockValue + ",'" + LocalTime.Now + "'," + inventoryObj.RaisedBy + ",1); ";
            }

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.Parameters.Clear();
            return dbConnection.cmd.ExecuteNonQuery();
        }


        public int releaseStockFromWarehouseToWarehouse(WarehouseInventoryRelease inventoryObj, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_RELEASE (WAREHOUSE_ID,ITEM_ID,RELEASED_QTY,RELEASED_DATE,RELEASED_BY,RELEASED_TYPE,RELEASED_WAREHOUSE_ID,DESCRIPTION) " +
                "VALUES (" + inventoryObj.WarehouseID + "," + inventoryObj.ItemID + "," + inventoryObj.ReleasedQty + ",'" + LocalTime.Now + "'," + inventoryObj.ReleasedBy + ",2," + inventoryObj.ReleasedWarehouseID + ",'" + inventoryObj.Description + "');" +
                " " +
                "IF EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + inventoryObj.WarehouseID + " AND ITEM_ID = " + inventoryObj.ItemID + ") " +
                    "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER SET STOCK_VALUE= CASE WHEN (STOCK_VALUE/(AVAILABLE_QTY+HOLDED_QTY))*((AVAILABLE_QTY+HOLDED_QTY)-" + inventoryObj.ReleasedQty + ")<0 THEN 0 ELSE (STOCK_VALUE/(AVAILABLE_QTY+HOLDED_QTY))*((AVAILABLE_QTY+HOLDED_QTY)-" + inventoryObj.ReleasedQty + ") END, AVAILABLE_QTY=AVAILABLE_QTY-" + inventoryObj.ReleasedQty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + inventoryObj.ReleasedBy + " WHERE WAREHOUSE_ID = " + inventoryObj.WarehouseID + " AND ITEM_ID = " + inventoryObj.ItemID + " " +
                "ELSE " +
                    "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER (WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                    "VALUES (" + inventoryObj.WarehouseID + "," + inventoryObj.ItemID + ",0," + inventoryObj.ReleasedQty + ",0,'" + LocalTime.Now + "'," + inventoryObj.ReleasedBy + ",1); " +
                " " +
                " " +
                "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_RAISE (WAREHOUSE_ID,ITEM_ID,RAISED_QTY,RAISED_DATE,RAISED_BY,RAISED_TYPE,RAISED_WAREHOUSE_ID,DESCRIPTION) " +
                "VALUES (" + inventoryObj.ReleasedWarehouseID + "," + inventoryObj.ItemID + "," + inventoryObj.ReleasedQty + ",'" + LocalTime.Now + "'," + inventoryObj.ReleasedBy + ",2," + inventoryObj.WarehouseID + ",'" + inventoryObj.Description + "');" +
                " " +
                "IF EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + inventoryObj.ReleasedWarehouseID + " AND ITEM_ID = " + inventoryObj.ItemID + ") " +
                    "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER SET STOCK_VALUE= CASE WHEN (STOCK_VALUE/(AVAILABLE_QTY+HOLDED_QTY))*((AVAILABLE_QTY+HOLDED_QTY)+" + inventoryObj.ReleasedQty + ")<0 THEN 0 ELSE (STOCK_VALUE/(AVAILABLE_QTY+HOLDED_QTY))*((AVAILABLE_QTY+HOLDED_QTY)+" + inventoryObj.ReleasedQty + ") END, AVAILABLE_QTY=AVAILABLE_QTY+" + inventoryObj.ReleasedQty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + inventoryObj.ReleasedBy + " WHERE WAREHOUSE_ID = " + inventoryObj.ReleasedWarehouseID + " AND ITEM_ID = " + inventoryObj.ItemID + " " +
                "ELSE " +
                    "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER (WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                    "VALUES (" + inventoryObj.ReleasedWarehouseID + "," + inventoryObj.ItemID + ",0," + inventoryObj.ReleasedQty + ",0,'" + LocalTime.Now + "'," + inventoryObj.ReleasedBy + ",1); "; ;
            return dbConnection.cmd.ExecuteNonQuery();
        }




        public int updateWarehouseStockAfterIssue(List<Inventory> inventoryObjList, DBConnection dbConnection) {
            string sql = "";
            foreach (Inventory inventoryObj in inventoryObjList) {
                sql += "IF EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + inventoryObj.WarehouseID + " AND ITEM_ID = " + inventoryObj.ItemID + ") " +
                       "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER SET AVAILABLE_QTY=AVAILABLE_QTY-" + inventoryObj.IssuedQty + ",HOLDED_QTY=HOLDED_QTY+" + inventoryObj.IssuedQty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + inventoryObj.LastUpdatedBy + " WHERE WAREHOUSE_ID = " + inventoryObj.WarehouseID + " AND ITEM_ID = " + inventoryObj.ItemID + " " +
                       "ELSE " +
                       "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER (WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                       "VALUES (" + inventoryObj.WarehouseID + "," + inventoryObj.ItemID + ",0," + inventoryObj.IssuedQty + ",0,'" + LocalTime.Now + "'," + inventoryObj.LastUpdatedBy + ",1); ";

                foreach (var batch in inventoryObj.IssuedBatches) {
                    sql += $@"UPDATE WAREHOUSE_INVENTORY_BATCHES SET AVAILABLE_QTY-={batch.IssuedQty}, HOLDED_QTY+={batch.IssuedQty},LAST_UPDATED_BY={inventoryObj.LastUpdatedBy},LAST_UPADATED_DATE='{ LocalTime.Now}' WHERE BATCH_ID={batch.BatchId} ";
                }


            }
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Inventory> fetchWarehouseInventorybyWarehouseId(int companyID, int warehouseId, int itemID, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT WIM.WAREHOUSE_ID,WIM.ITEM_ID,WIM.AVAILABLE_QTY,WIM.HOLDED_QTY,WIM.IS_ACTIVE,W.LOCATION,AIM.ITEM_NAME FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER AS WIM " +
                                            "INNER JOIN (SELECT WAREHOUSE_ID, LOCATION, COMPANY_ID FROM " + dbLibrary + ".WAREHOUSE) AS W ON WIM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME FROM " + dbLibrary + ".ADD_ITEMS_MASTER) AS AIM " +
                                            "ON WIM.ITEM_ID = AIM.ITEM_ID " +
                                            "WHERE W.COMPANY_ID = " + companyID + "AND WIM.WAREHOUSE_ID=" + warehouseId + " AND WIM.ITEM_ID = " + itemID + " AND WIM.AVAILABLE_QTY > 0";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Inventory>(dbConnection.dr);
            }
        }

        public int raiseWarehouseStockInMrn(WarehouseInventoryRaise inventory, DBConnection dbConnection) {

            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_RAISE (WAREHOUSE_ID,ITEM_ID,RAISED_QTY,RAISED_DATE,RAISED_BY,RAISED_TYPE,RAISED_WAREHOUSE_ID) " +
                        "VALUES (" + inventory.WarehouseID + "," + inventory.ItemID + "," + inventory.RaisedQty + ",'" + LocalTime.Now + "'," + inventory.RaisedBy + ",2," + inventory.RaisedWarehouseID + ");" +
                        "IF EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + inventory.WarehouseID + " AND ITEM_ID = " + inventory.ItemID + ") " +
                            "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER SET STOCK_VALUE= STOCK_VALUE+" + inventory.StockValue + ", AVAILABLE_QTY=AVAILABLE_QTY+" + inventory.RaisedQty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + inventory.RaisedBy + " WHERE WAREHOUSE_ID = " + inventory.WarehouseID + " AND ITEM_ID = " + inventory.ItemID + " " +
                        "ELSE " +
                            "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER (WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                            "VALUES (" + inventory.WarehouseID + "," + inventory.ItemID + "," + inventory.RaisedQty + ",0," + inventory.StockValue + ",'" + LocalTime.Now + "'," + inventory.RaisedBy + ",1); ";
            dbConnection.cmd.Parameters.Clear();
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int raiseCompanyStockInMrn(int companyID, int itemID, decimal receivedQty, decimal stockValue, int raisedBy, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF EXISTS (SELECT * FROM " + dbLibrary + ".COMPANY_INVENTORY_MASTER WHERE COMPANY_ID = " + companyID + " AND ITEM_ID = " + itemID + ") " +
                                                "UPDATE " + dbLibrary + ".COMPANY_INVENTORY_MASTER SET STOCK_VALUE= STOCK_VALUE+" + stockValue + ", AVAILABLE_QTY= AVAILABLE_QTY+" + receivedQty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + raisedBy + " WHERE COMPANY_ID = " + companyID + " AND ITEM_ID = " + itemID + " " +
                                            "ELSE " +
                                                "INSERT INTO " + dbLibrary + ".COMPANY_INVENTORY_MASTER (COMPANY_ID,ITEM_ID,AVAILABLE_QTY,STOCK_VALUE,REORDER_LEVEL,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                                                "VALUES (" + companyID + "," + itemID + "," + receivedQty + "," + stockValue + ",0,'" + LocalTime.Now + "'," + raisedBy + ",1)";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateWarehouseStockAfterIssuesformonewarehouse(Inventory inventoryObjList, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + inventoryObjList.WarehouseID + " AND ITEM_ID = " + inventoryObjList.ItemID + ") " +
                       "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER SET AVAILABLE_QTY=AVAILABLE_QTY-" + inventoryObjList.IssuedQty + ",HOLDED_QTY=HOLDED_QTY+" + inventoryObjList.IssuedQty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + inventoryObjList.LastUpdatedBy + " WHERE WAREHOUSE_ID = " + inventoryObjList.WarehouseID + " AND ITEM_ID = " + inventoryObjList.ItemID + " " +
                       "ELSE " +
                       "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER (WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                       "VALUES (" + inventoryObjList.WarehouseID + "," + inventoryObjList.ItemID + ",0," + inventoryObjList.IssuedQty + ",0,'" + LocalTime.Now + "'," + inventoryObjList.LastUpdatedBy + ",1); ";


            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int raiseWarehouseStockInMrnManual(WarehouseInventoryRaise inventory, DateTime expDate, DBConnection dbConnection) {

            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_RAISE (WAREHOUSE_ID,ITEM_ID,RAISED_QTY,RAISED_DATE,RAISED_BY,RAISED_TYPE,RAISED_WAREHOUSE_ID,IS_MANUAL_UPDATE) " +
                        "VALUES (" + inventory.WarehouseID + "," + inventory.ItemID + "," + inventory.RaisedQty + ",'" + LocalTime.Now + "'," + inventory.RaisedBy + ",2," + inventory.RaisedWarehouseID + ",1);" +
                        "IF EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + inventory.WarehouseID + " AND ITEM_ID = " + inventory.ItemID + ") " +
                            "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER SET STOCK_VALUE= STOCK_VALUE+" + inventory.StockValue + ", AVAILABLE_QTY=AVAILABLE_QTY+" + inventory.RaisedQty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + inventory.RaisedBy + " WHERE WAREHOUSE_ID = " + inventory.WarehouseID + " AND ITEM_ID = " + inventory.ItemID + " " +
                        "ELSE " +
                            "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER (WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                            "VALUES (" + inventory.WarehouseID + "," + inventory.ItemID + "," + inventory.RaisedQty + ",0," + inventory.StockValue + ",'" + LocalTime.Now + "'," + inventory.RaisedBy + ",1); ";

            if (inventory.StockMaintainigType != 1) {
                dbConnection.cmd.CommandText += "INSERT INTO WAREHOUSE_INVENTORY_BATCHES (BATCH_CODE, WAREHOUSE_ID, ITEM_ID, COMPANY_ID, EXPIRY_DATE,AVAILABLE_QTY,HOLDED_QTY, STOCK_VALUE, LAST_UPADATED_DATE, LAST_UPDATED_BY,  IS_ACTIVE, SELLING_PRICE) " +
                                                "VALUES ((SELECT ISNULL(MAX(BATCH_CODE),0)+1 FROM WAREHOUSE_INVENTORY_BATCHES WHERE ITEM_ID = " + inventory.ItemID + " AND WAREHOUSE_ID = " + inventory.WarehouseID + "), " + inventory.WarehouseID + ", " + inventory.ItemID + ", " + inventory.CompanyId + ",'" + expDate + "'," + inventory.RaisedQty + ",0," + inventory.StockValue + ",'" + LocalTime.Now + "', " + inventory.RaisedBy + ", 1, 0) ";
            }

            dbConnection.cmd.Parameters.Clear();
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public void updateCompanyStock(int warehouseId, MrnDetailsV2 mrnDetail, int updatedBy, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF EXISTS (SELECT * FROM " + dbLibrary + ".COMPANY_INVENTORY_MASTER WHERE  ITEM_ID = " + mrnDetail.ItemId + ") " +
                                            "UPDATE " + dbLibrary + ".COMPANY_INVENTORY_MASTER " +
                                            " SET STOCK_VALUE= CASE WHEN AVAILABLE_QTY>0 AND((STOCK_VALUE/(AVAILABLE_QTY))*(AVAILABLE_QTY-" + mrnDetail.IssuedQty + ")>=0) " +
                                            " THEN (STOCK_VALUE/(AVAILABLE_QTY))*(AVAILABLE_QTY-" + mrnDetail.IssuedQty + ") " +
                                            " ELSE 0 END, " +
                                            " AVAILABLE_QTY= CASE WHEN AVAILABLE_QTY>0 THEN AVAILABLE_QTY-" + mrnDetail.IssuedQty + " ELSE 0 END, " +
                                            " LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + updatedBy + " " +
                                            " WHERE  ITEM_ID = " + mrnDetail.ItemId + " " +
                                            "ELSE " +
                                            "INSERT INTO " + dbLibrary + ".COMPANY_INVENTORY_MASTER (COMPANY_ID,ITEM_ID,AVAILABLE_QTY,STOCK_VALUE,REORDER_LEVEL,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                                            "VALUES (6," + mrnDetail.ItemId + ",0,0,0,'" + LocalTime.Now + "'," + updatedBy + ",1)";
            dbConnection.cmd.ExecuteNonQuery();
        }

       

                public int updateWarehouseStock(List<WarehouseInventory> inventoryList, int UserId, int ItemId, int CompanyId, string Remarks, List<WarehouseInventoryBatches> deletedBatches, int StockMAintainingType, List<WarehouseInventoryBatches> addedBatches, List<WarehouseInventoryBatches> batchList, DBConnection dbConnection) {

            try {

                string sql = "";
                if (StockMAintainingType == 1) {
                    for (int i = 0; i < inventoryList.Count; i++) {

                        sql += "IF EXISTS(SELECT * FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID=" + inventoryList[i].WarehouseID + ") \n";
                        sql += "BEGIN \n";
                        sql += "    IF NOT EXISTS(SELECT * FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID=" + inventoryList[i].WarehouseID + " AND AVAILABLE_QTY=" + inventoryList[i].AvailableQty + " AND STOCK_VALUE=" + inventoryList[i].StockValue + ") \n";
                        sql += "        INSERT INTO [STOCK_OVERRIDE_LOG] \n" +
                               "        SELECT " + ItemId + "," + inventoryList[i].WarehouseID + ",AVAILABLE_QTY,STOCK_VALUE," + inventoryList[i].AvailableQty + "," + inventoryList[i].StockValue + "," + UserId + ",'" + LocalTime.Now + "',1,NULL,'" + Remarks.ProcessString() + "' FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID=" + inventoryList[i].WarehouseID + " \n";
                        sql += "END \n";
                        sql += "ELSE \n";
                        sql += "BEGIN \n";
                        sql += "    INSERT INTO [STOCK_OVERRIDE_LOG] \n" +
                               "    VALUES \n" +
                               "    (" + ItemId + "," + inventoryList[i].WarehouseID + ",0,0," + inventoryList[i].AvailableQty + "," + inventoryList[i].StockValue + "," + UserId + ",'" + LocalTime.Now + "',1,NULL,'" + Remarks.ProcessString() + "') \n";
                        sql += "END \n";
                    }

                    if (inventoryList.Count > 0) {
                        sql += "        INSERT INTO [STOCK_OVERRIDE_LOG] \n" +
                               "        SELECT " + ItemId + ",WAREHOUSE_ID,AVAILABLE_QTY,STOCK_VALUE,0,0," + UserId + ",'" + LocalTime.Now + "',1,NULL,'" + Remarks.ProcessString() + "' FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID NOT IN (" + string.Join(",", inventoryList.Select(win => win.WarehouseID)) + ") \n";
                    }
                    else {
                        sql += "        INSERT INTO [STOCK_OVERRIDE_LOG] \n" +
                               "        SELECT " + ItemId + ",WAREHOUSE_ID,AVAILABLE_QTY,STOCK_VALUE,0,0," + UserId + ",'" + LocalTime.Now + "',1,NULL,'" + Remarks.ProcessString() + "' FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + " \n";
                    }

                    sql += "DELETE FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID=" + ItemId + " AND WAREHOUSE_ID IN (SELECT WAREHOUSE_ID FROM WAREHOUSE WHERE COMPANY_ID=" + CompanyId + ")";

                    for (int i = 0; i < inventoryList.Count; i++) {

                        sql += " \n\n INSERT INTO WAREHOUSE_INVENTORY_MASTER(WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE,REORDER_LEVEL) \n" +
                                "VALUES \n" +
                                "(" + inventoryList[i].WarehouseID + "," + ItemId + "," + inventoryList[i].AvailableQty + ",0," + inventoryList[i].StockValue + ", '" + LocalTime.Now + "'," + UserId + ",1, " + inventoryList[i].ReorderLevel + ") ";
                    }

                    sql += "IF EXISTS (SELECT * FROM COMPANY_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + ") "
                                  + "UPDATE COMPANY_INVENTORY_MASTER SET AVAILABLE_QTY =" + inventoryList.Sum(il => il.AvailableQty) + ", STOCK_VALUE=" + inventoryList.Sum(il => il.StockValue) + ", REORDER_LEVEL=" + inventoryList.Sum(il => il.ReorderLevel) + " WHERE COMPANY_ID =" + CompanyId + " AND ITEM_ID=" + ItemId + " \n";

                    sql += "ELSE " +
                                "INSERT INTO " + dbLibrary + ".COMPANY_INVENTORY_MASTER(COMPANY_ID, ITEM_ID, AVAILABLE_QTY, STOCK_VALUE, REORDER_LEVEL, LAST_UPDATED_DATE, LAST_UPDATED_BY, IS_ACTIVE) " +
                                "VALUES (" + CompanyId + "," + ItemId + "," + inventoryList.Sum(il => il.AvailableQty) + ", " + inventoryList.Sum(il => il.StockValue) + "," + inventoryList.Sum(il => il.ReorderLevel) + ",'" + LocalTime.Now + "'," + UserId + ",1)";

                    //sql += "UPDATE COMPANY_INVENTORY_MASTER SET AVAILABLE_QTY =" + inventoryList.Sum(il => il.AvailableQty) + ", STOCK_VALUE=" + inventoryList.Sum(il => il.StockValue) + " WHERE COMPANY_ID =" + CompanyId + " AND ITEM_ID=" + ItemId + " \n";
                }
                else {
                    sql += " DECLARE @OVERRIDE_LOG_ID TABLE(OVERRIDE_LOG_ID INT) \n";
                    sql += "DECLARE @BATCH_ID TABLE(BATCH_ID INT) \n";
                    for (int i = 0; i < inventoryList.Count; i++) {
                        sql += "IF EXISTS(SELECT * FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID=" + inventoryList[i].WarehouseID + ") \n";
                        sql += "BEGIN \n";
                        sql += "    IF NOT EXISTS(SELECT * FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID=" + inventoryList[i].WarehouseID + " AND AVAILABLE_QTY=" + inventoryList[i].AvailableQty + " AND STOCK_VALUE=" + inventoryList[i].StockValue + ") \n";

                        sql += "        INSERT INTO STOCK_OVERRIDE_LOG (ITEM_ID,WAREHOUSE_ID, EXISTED_QTY, EXISTED_STOCK_VALUE,OVERRIDING_QTY, OVERRIDING_STOCK_VALUE,OVERRIDDEN_BY, OVERRIDDEN_ON, OVERRIDING_TYPE,PSVD_ID,REMARKS)\n" +
                            "OUTPUT INSERTED.OVERRIDE_LOG_ID INTO @OVERRIDE_LOG_ID VALUES  (" + ItemId + "," + inventoryList[i].WarehouseID + ", (SELECT AVAILABLE_QTY  FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID=" + inventoryList[i].WarehouseID + " ), (SELECT STOCK_VALUE  FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID=" + inventoryList[i].WarehouseID + " )," + inventoryList[i].AvailableQty + "," + inventoryList[i].StockValue + "," + UserId + ",'" + LocalTime.Now + "',1,NULL,'" + Remarks.ProcessString() + "') \n";
                               //"        SELECT " + ItemId + "," + inventoryList[i].WarehouseID + ",AVAILABLE_QTY,STOCK_VALUE," + inventoryList[i].AvailableQty + "," + inventoryList[i].StockValue + "," + UserId + ",'" + LocalTime.Now + "',1,NULL,'" + Remarks.ProcessString() + "' FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID=" + inventoryList[i].WarehouseID + " \n";
                        
                        
                        for (int k = 0; k < addedBatches.Count; k++) {
                            if (inventoryList[i].WarehouseID == addedBatches[k].WarehouseID) {

                                
                                sql +=  "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES \n" +
                                       "OUTPUT INSERTED.BATCH_ID INTO @BATCH_ID VALUES \n" +
                                       "((SELECT ISNULL(MAX(BATCH_CODE),0) + 1  FROM WAREHOUSE_INVENTORY_BATCHES WHERE ITEM_ID = "+ ItemId + " AND WAREHOUSE_ID = "+ addedBatches[k].WarehouseID+ ")," + addedBatches[k].WarehouseID + "," + ItemId + ", " + CompanyId + ", '" + addedBatches[k].ExpiryDate + "'," + addedBatches[k].AvailableStock + ",0 , " + addedBatches[k].StockValue + " ,'" + LocalTime.Now + "' , " + UserId + ",1,0,0) \n";

                                sql += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] ) " +
                                                       " VALUES(" + ItemId + ",(SELECT MAX(BATCH_ID) FROM @BATCH_ID)," + addedBatches[k].WarehouseID + ",0,0," + addedBatches[k].AvailableStock + "," + addedBatches[k].StockValue + ",(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";
                            }
                        }
                        for (int j = 0; j < deletedBatches.Count; j++) {
                            if (inventoryList[i].WarehouseID == deletedBatches[j].WarehouseID) {

                                sql += " UPDATE WAREHOUSE_INVENTORY_BATCHES SET IS_ACTIVE = 0 WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID = " + deletedBatches[j].WarehouseID + " AND BATCH_ID =" + deletedBatches[j].BatchchId + "  ";
                                sql += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] ) " +
                                                   " VALUES(" + ItemId + "," + deletedBatches[j].BatchchId + "," + deletedBatches[j].WarehouseID + "," + deletedBatches[j].AvailableStock + "," + deletedBatches[j].StockValue + ", 0, 0,(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";
                            }
                        }
                        sql += "END \n";
                        sql += "ELSE \n";
                        sql += "BEGIN \n";
                       // sql +=
                            //" INSERT INTO [STOCK_OVERRIDE_LOG] VALUES\n" +
                            //   "    (" + ItemId + "," + inventoryList[i].WarehouseID + ",0,0," + inventoryList[i].AvailableQty + "," + inventoryList[i].StockValue + "," + UserId + ",'" + LocalTime.Now + "',1,NULL,'" + Remarks.ProcessString() + "') \n";
                            sql += "        INSERT INTO STOCK_OVERRIDE_LOG (ITEM_ID,WAREHOUSE_ID, EXISTED_QTY, EXISTED_STOCK_VALUE,OVERRIDING_QTY, OVERRIDING_STOCK_VALUE,OVERRIDDEN_BY, OVERRIDDEN_ON, OVERRIDING_TYPE,PSVD_ID,REMARKS)\n" +
                            "OUTPUT INSERTED.OVERRIDE_LOG_ID INTO @OVERRIDE_LOG_ID VALUES  (" + ItemId + "," + inventoryList[i].WarehouseID + ", 0,0," + inventoryList[i].AvailableQty + "," + inventoryList[i].StockValue + ", " + UserId + ",'" + LocalTime.Now + "',1,NULL,'" + Remarks.ProcessString() + "') \n";

                        
                        
                        for (int k = 0; k < addedBatches.Count; k++) {
                            if (inventoryList[i].WarehouseID == addedBatches[k].WarehouseID) {

                                sql += "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES \n" +
                                      "OUTPUT INSERTED.BATCH_ID INTO @BATCH_ID VALUES \n" +
                                      "((SELECT ISNULL(MAX(BATCH_CODE),0) + 1  FROM WAREHOUSE_INVENTORY_BATCHES WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID = " + addedBatches[k].WarehouseID + ")," + addedBatches[k].WarehouseID + "," + ItemId + ", " + CompanyId + ", '" + addedBatches[k].ExpiryDate + "'," + addedBatches[k].AvailableStock + ",0 , " + addedBatches[k].StockValue + " ,'" + LocalTime.Now + "' , " + UserId + ",1,0,0) \n";


                                sql += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] ) " +
                                                   " VALUES(" + ItemId + ",(SELECT MAX(BATCH_ID) FROM @BATCH_ID)," + addedBatches[k].WarehouseID + ",0,0," + addedBatches[k].AvailableStock + "," + addedBatches[k].StockValue + ",(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";
                            }
                        }

                        for (int j = 0; j < deletedBatches.Count; j++) {
                            if (inventoryList[i].WarehouseID == deletedBatches[j].WarehouseID) {
                                sql += " UPDATE WAREHOUSE_INVENTORY_BATCHES SET IS_ACTIVE = 0 WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID = " + deletedBatches[j].WarehouseID + " AND BATCH_ID =" + deletedBatches[j].BatchchId + "  ";
                                sql += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] ) " +
                                                   " VALUES(" + ItemId + "," + deletedBatches[j].BatchchId + "," + deletedBatches[j].WarehouseID + "," + deletedBatches[j].AvailableStock + "," + deletedBatches[j].StockValue + ", 0, 0,(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";
                            }
                        }
                        sql += "END \n";
                    }

                    //if (inventoryList.Count > 0) {
                    //    sql +=
                    //        "        INSERT INTO [STOCK_OVERRIDE_LOG] \n" +
                    //           "        SELECT " + ItemId + ",WAREHOUSE_ID,AVAILABLE_QTY,STOCK_VALUE,0,0," + UserId + ",'" + LocalTime.Now + "',1,NULL,'" + Remarks.ProcessString() + "' FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID NOT IN (" + string.Join(",", inventoryList.Select(win => win.WarehouseID)) + ") \n";
                    //    for (int j = 0; j < deletedBatches.Count; j++) {
                    //        sql += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] ) " +
                    //                               " VALUES(" + ItemId + "," + deletedBatches[j].BatchchId + "," + deletedBatches[j].WarehouseID + "," + deletedBatches[j].AvailableStock + "," + deletedBatches[j].StockValue + ", 0, 0,(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";

                    //    }
                    //    for (int k = 0; k < addedBatches.Count; k++) {
                    //        sql += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] ) " +
                    //                               " VALUES(" + ItemId + ",SELECT ISNULL(MAX(BATCH_ID),0) FROM WAREHOUSE_INVENTORY_BATCHES WHERE WAREHOUSE_ID = " + addedBatches[k].WarehouseID + " AND ITEM_ID = " + ItemId + "," + addedBatches[k].WarehouseID + ",0,0," + addedBatches[k].AvailableStock + "," + addedBatches[k].StockValue + ",(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";

                    //    }

                    //}
                    //else {
                    //    sql +=
                    //        "        INSERT INTO [STOCK_OVERRIDE_LOG] \n" +
                    //           "        SELECT " + ItemId + ",WAREHOUSE_ID,AVAILABLE_QTY,STOCK_VALUE,0,0," + UserId + ",'" + LocalTime.Now + "',1,NULL,'" + Remarks.ProcessString() + "' FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + " \n";

                    //    for (int j = 0; j < deletedBatches.Count; j++) {
                    //        sql += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] ) " +
                    //                               " VALUES(" + ItemId + "," + deletedBatches[j].BatchchId + "," + deletedBatches[j].WarehouseID + "," + deletedBatches[j].AvailableStock + "," + deletedBatches[j].StockValue + ", 0, 0,(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";

                    //    }
                    //    for (int k = 0; k < addedBatches.Count; k++) {
                    //        sql += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] ) " +
                    //                               " VALUES(" + ItemId + ",SELECT ISNULL(MAX(BATCH_ID),0) FROM WAREHOUSE_INVENTORY_BATCHES WHERE WAREHOUSE_ID = " + addedBatches[k].WarehouseID + " AND ITEM_ID = " + ItemId + "," + addedBatches[k].WarehouseID + ",0,0," + addedBatches[k].AvailableStock + "," + addedBatches[k].StockValue + ",(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";

                    //    }
                    //}

                    sql += "DELETE FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID=" + ItemId + " AND WAREHOUSE_ID IN (SELECT WAREHOUSE_ID FROM WAREHOUSE WHERE COMPANY_ID=" + CompanyId + ")";

                    for (int i = 0; i < inventoryList.Count; i++) {
                        
                            sql += " \n\n INSERT INTO WAREHOUSE_INVENTORY_MASTER(WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE,REORDER_LEVEL) \n" +
                                    "VALUES \n" +
                                    "(" + inventoryList[i].WarehouseID + "," + ItemId + "," + inventoryList[i].AvailableQty + ",0," + inventoryList[i].StockValue + ", '" + LocalTime.Now + "'," + UserId + ",1, " + inventoryList[i].ReorderLevel + ") ";
                       
                    }

                    //if (batchList.Count > 0) {
                    //    for (int j = 0; j < batchList.Count; j++) {
                    //        sql += "UPDATE WAREHOUSE_INVENTORY_BATCHES SET AVAILABLE_QTY = " + batchList[j].AvailableStock + " WHERE ITEM_ID = " + ItemId + " AND BATCH_ID = "+batchList[j].BatchchId+" ";
                    //    }
                    //}
                    sql += "IF EXISTS (SELECT * FROM COMPANY_INVENTORY_MASTER WHERE ITEM_ID = " + ItemId + ") "
                                  + "UPDATE COMPANY_INVENTORY_MASTER SET AVAILABLE_QTY =" + inventoryList.Sum(il => il.AvailableQty) + ", STOCK_VALUE=" + inventoryList.Sum(il => il.StockValue) + ", REORDER_LEVEL=" + inventoryList.Sum(il => il.ReorderLevel) + " WHERE COMPANY_ID =" + CompanyId + " AND ITEM_ID=" + ItemId + " \n";

                    sql += "ELSE " +
                                "INSERT INTO " + dbLibrary + ".COMPANY_INVENTORY_MASTER(COMPANY_ID, ITEM_ID, AVAILABLE_QTY, STOCK_VALUE, REORDER_LEVEL, LAST_UPDATED_DATE, LAST_UPDATED_BY, IS_ACTIVE) " +
                                "VALUES (" + CompanyId + "," + ItemId + "," + inventoryList.Sum(il => il.AvailableQty) + ", " + inventoryList.Sum(il => il.StockValue) + "," + inventoryList.Sum(il => il.ReorderLevel) + ",'" + LocalTime.Now + "'," + UserId + ",1)";

                }

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = sql;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            catch (Exception ex) {

                throw;
            }

        }


        public decimal GetWarehouseInventoryForItem(int warehouseId, int ItemId, DBConnection dbConnection) {
            String sql = "SELECT ISNULL(AVAILABLE_QTY,0) AS AVAILABLE_QTY FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID =" + warehouseId + " AND ITEM_ID=" + ItemId + ";";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                if (dbConnection.dr.HasRows) {
                    dbConnection.dr.Read();
                    return decimal.Parse(dbConnection.dr["AVAILABLE_QTY"].ToString());
                }
                else {
                    return 0;
                }
            }
        }

        public int updateCompanyStockAfterIssue(int companyID, int itemID, decimal issuedQty, int updatedBy, decimal IssuedValue, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF EXISTS (SELECT * FROM " + dbLibrary + ".COMPANY_INVENTORY_MASTER WHERE COMPANY_ID = " + companyID + " AND ITEM_ID = " + itemID + ") " +
                                                "UPDATE " + dbLibrary + ".COMPANY_INVENTORY_MASTER SET STOCK_VALUE= CASE WHEN AVAILABLE_QTY>0 AND (STOCK_VALUE-" + IssuedValue + ")>=0 THEN STOCK_VALUE-" + IssuedValue + " ELSE 0 END, AVAILABLE_QTY= CASE WHEN AVAILABLE_QTY>0 THEN AVAILABLE_QTY-" + issuedQty + " ELSE 0 END,LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + updatedBy + " WHERE COMPANY_ID = " + companyID + " AND ITEM_ID = " + itemID + " " +
                                            "ELSE \n" +
                                                "INSERT INTO " + dbLibrary + ".COMPANY_INVENTORY_MASTER (COMPANY_ID,ITEM_ID,AVAILABLE_QTY,STOCK_VALUE,REORDER_LEVEL,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                                                "VALUES (" + companyID + "," + itemID + ",0,0,0,'" + LocalTime.Now + "'," + updatedBy + ",1)";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int SaveEditedMasterInventory(int warehouseId, int ItemId, decimal newQty, decimal newStockValue, int updatedBy, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + warehouseId + "  AND ITEM_ID = " + ItemId + ") " +
                                            "UPDATE WAREHOUSE_INVENTORY_MASTER SET AVAILABLE_QTY = " + newQty + " ,STOCK_VALUE = " + newStockValue + " WHERE WAREHOUSE_ID = " + warehouseId + "  AND ITEM_ID = " + ItemId + " "+
                                            "ELSE \n" +
                                                "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER (WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,REORDER_LEVEL,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                                                "VALUES (" + warehouseId + "," + ItemId + ","+ newQty + ",0, "+ newStockValue + ",0,'" + LocalTime.Now + "'," + updatedBy + ",1)";

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int SaveEditedBatchInventory(int WarehouseId, int ItemId, int UserId, int BatchId,decimal newQtyBatch,decimal newStockValueBatch, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE WAREHOUSE_INVENTORY_BATCHES SET AVAILABLE_QTY = " + newQtyBatch + " ,STOCK_VALUE = " + newStockValueBatch + " WHERE WAREHOUSE_ID = " + WarehouseId + "  AND ITEM_ID = " + ItemId + " AND BATCH_ID = "+ BatchId + " ";
                                           
            return dbConnection.cmd.ExecuteNonQuery();
        }


        public int updateWarehouseStockAfterDelivered(int mrndInID, int warehouseID, int itemID, decimal DeliveredQty, int updatedBy, decimal deliveredStockValue, List<IssuedInventoryBatches> Batches, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_RELEASE (WAREHOUSE_ID,ITEM_ID,RELEASED_QTY,RELEASED_DATE,RELEASED_BY,RELEASED_TYPE,RELEASED_MRND_IN_ID,RELEASED_STOCK_VALUE) " +
                "VALUES (" + warehouseID + "," + itemID + "," + DeliveredQty + ",'" + LocalTime.Now + "'," + updatedBy + ",1," + mrndInID + "," + deliveredStockValue + ");" +
                "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER SET STOCK_VALUE= CASE WHEN (STOCK_VALUE-" + deliveredStockValue + ")<0 THEN 0 ELSE (STOCK_VALUE-" + deliveredStockValue + ") END, HOLDED_QTY=HOLDED_QTY-" + DeliveredQty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + updatedBy + " WHERE WAREHOUSE_ID = " + warehouseID + " AND ITEM_ID = " + itemID + "; \n\n";

            foreach (var batch in Batches) {
                dbConnection.cmd.CommandText += $@"UPDATE WAREHOUSE_INVENTORY_BATCHES SET HOLDED_QTY-={batch.IssuedQty}, STOCK_VALUE-={batch.IssuedStockValue},LAST_UPDATED_BY={updatedBy},LAST_UPADATED_DATE='{ LocalTime.Now}' WHERE BATCH_ID={batch.BatchId} ";
            }


            return dbConnection.cmd.ExecuteNonQuery();
        }

        public WarehouseInventoryDetail GetWarehouseInventoryDetailToIssue(int ItemId, int WarehouseId, int CompanyId, DBConnection dbConnection) {
            WarehouseInventoryDetail detail;

            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = $@"SELECT AI.ITEM_ID,AI.ITEM_NAME,AI.REFERENCE_NO,AI.MEASUREMENT_ID,AI.STOCK_MAINTAINING_TYPE,W.LOCATION,WIM.AVAILABLE_QTY,WIM.HOLDED_QTY,WIM.STOCK_VALUE FROM WAREHOUSE_INVENTORY_MASTER AS WIM
                                                INNER JOIN ADD_ITEMS AS AI ON WIM.ITEM_ID=AI.ITEM_ID
                                                INNER JOIN WAREHOUSE AS W ON WIM.WAREHOUSE_ID = W.WAREHOUSE_ID
                                                WHERE WIM.ITEM_ID={ItemId} AND WIM.WAREHOUSE_ID={WarehouseId} AND AI.COMPANY_ID={CompanyId}";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                detail = dataAccessObject.GetSingleOject<WarehouseInventoryDetail>(dbConnection.dr);

            }

            if (detail != null && detail.ItemId != 0) {
                if (detail.StockMaintainingType != 1) {

                    dbConnection.cmd.Parameters.Clear();

                    dbConnection.cmd.CommandText = $@"SELECT * FROM WAREHOUSE_INVENTORY_BATCHES
                                                    WHERE ITEM_ID={ItemId} AND WAREHOUSE_ID={WarehouseId} AND AVAILABLE_QTY>0 AND IS_ACTIVE=1 ";
                    if (detail.StockMaintainingType == 2) {
                        dbConnection.cmd.CommandText += "ORDER BY BATCH_ID ASC";
                    }
                    else {
                        dbConnection.cmd.CommandText += "ORDER BY BATCH_ID DESC";
                    }

                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                    using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                        DataAccessObject dataAccessObject = new DataAccessObject();
                        detail.Batches = dataAccessObject.ReadCollection<WarehouseInventoryBatches>(dbConnection.dr);

                    }
                }
                else {
                    detail.Batches = new List<WarehouseInventoryBatches>();
                }
            }

            return detail;
        }

        public Inventory getAvailableStockValues(int ItemId, int WarehouseId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();

            //dbConnection.cmd.CommandText = "SELECT IM.*, AI.ITEM_NAME, MD.SHORT_CODE FROM WAREHOUSE_INVENTORY_MASTER AS IM " +
            //    "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,MEASUREMENT_ID FROM ADD_ITEMS) AS AI ON AI.ITEM_ID = IM.ITEM_ID " +
            //    "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID = AI.MEASUREMENT_ID "+
            //    "WHERE IM.ITEM_ID =" + ItemId + " AND WAREHOUSE_ID = " + WarehouseId + " ";

            dbConnection.cmd.CommandText = "SELECT * FROM ADD_ITEMS AS AI " +
                                            "LEFT JOIN(SELECT ITEM_ID, WAREHOUSE_ID, AVAILABLE_QTY FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + WarehouseId + ") AS IM ON IM.ITEM_ID = AI.ITEM_ID " +
                                            "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID = AI.MEASUREMENT_ID "+
                                            "WHERE AI.ITEM_ID = "+ ItemId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Inventory>(dbConnection.dr);
            }
        }

        public int updateWarehouseStockAfterTRIssue(List<Inventory> inventoryObjList, DBConnection dbConnection) {
            string sql = "";
            foreach (Inventory inventoryObj in inventoryObjList) {
                sql += "INSERT INTO WAREHOUSE_INVENTORY_RAISE(WAREHOUSE_ID, ITEM_ID, RAISED_QTY, RAISED_DATE, RAISED_TYPE, GRND_ID, RAISED_WAREHOUSE_ID, RAISED_BY, DESCRIPTION, RAISED_STOCK_VALUE) " +
                        "VALUES(" + inventoryObj.FromWarehouseId + ", " + inventoryObj.ItemID + ", " + inventoryObj.IssuedQty + ", '" + LocalTime.Now + "', 2, 0, " + inventoryObj.WarehouseID + ", " + inventoryObj.LastUpdatedBy + ", '', " + inventoryObj.IssuedStockValue + ") " +

                        "IF EXISTS(SELECT * FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + inventoryObj.FromWarehouseId + " AND ITEM_ID = " + inventoryObj.ItemID + ") " +
                        "UPDATE WAREHOUSE_INVENTORY_MASTER SET AVAILABLE_QTY = AVAILABLE_QTY + " + inventoryObj.IssuedQty + ", LAST_UPDATED_DATE = '" + LocalTime.Now + "', LAST_UPDATED_BY = " + inventoryObj.LastUpdatedBy + " , STOCK_VALUE = STOCK_VALUE+" + inventoryObj.IssuedStockValue + " WHERE WAREHOUSE_ID = " + inventoryObj.FromWarehouseId + " AND ITEM_ID = " + inventoryObj.ItemID + " " +
                        "ELSE " +
                        "INSERT INTO WAREHOUSE_INVENTORY_MASTER(WAREHOUSE_ID, ITEM_ID, AVAILABLE_QTY, HOLDED_QTY, STOCK_VALUE, LAST_UPDATED_DATE, LAST_UPDATED_BY, IS_ACTIVE) " +
                        "VALUES(" + inventoryObj.FromWarehouseId + ", " + inventoryObj.ItemID + ", " + inventoryObj.IssuedQty + ", 0 , " + inventoryObj.IssuedStockValue + ", '" + LocalTime.Now + "', " + inventoryObj.LastUpdatedBy + ", 1) ";

                for (int i = 0; i < inventoryObj.TrdIssueNoteBatches.Count; i++) {
                    var note = inventoryObj.TrdIssueNoteBatches[i];

                    sql += $@"UPDATE WAREHOUSE_INVENTORY_BATCHES
                                SET
                                HOLDED_QTY-={note.IssuedQty}, STOCK_VALUE={note.IssuedStockValue} WHERE BATCH_ID={note.BatchId};

                            INSERT INTO WAREHOUSE_INVENTORY_BATCHES
                            VALUES
                            ({note.BatchCode},{inventoryObj.WarehouseID},{inventoryObj.ItemID},{inventoryObj.CompanyID},
                            (SELECT EXPIRY_DATE FROM WAREHOUSE_INVENTORY_BATCHES WHERE BATCH_ID={note.BatchId}),
                            {note.IssuedQty},0,{note.IssuedStockValue},'{LocalTime.Now}',{inventoryObj.LastUpdatedBy},1)";
                }
            }
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int updateWarehouseStockAfterTRDelivered(int trdInID, int warehouseID, int itemID, decimal DeliveredQty, int updatedBy, decimal deliveredStockValue, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO WAREHOUSE_INVENTORY_RELEASE (WAREHOUSE_ID,ITEM_ID,RELEASED_QTY,RELEASED_DATE,RELEASED_BY,RELEASED_TYPE,RELEASED_STOCK_VALUE, RELEASED_TRND_IN_ID) " +
                "VALUES (" + warehouseID + "," + itemID + "," + DeliveredQty + ",'" + LocalTime.Now + "'," + updatedBy + ",3," + deliveredStockValue + ", " + trdInID + ");" +
                "UPDATE WAREHOUSE_INVENTORY_MASTER SET STOCK_VALUE= CASE WHEN (STOCK_VALUE-" + deliveredStockValue + ")<0 THEN 0 ELSE (STOCK_VALUE-" + deliveredStockValue + ") END, HOLDED_QTY=HOLDED_QTY-" + DeliveredQty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + updatedBy + " WHERE WAREHOUSE_ID = " + warehouseID + " AND ITEM_ID = " + itemID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public Inventory getStockValues(int ItemId, int WarehouseId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            try {
                dbConnection.cmd.CommandText = "SELECT STOCK_VALUE FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID =" + ItemId + " AND WAREHOUSE_ID = " + WarehouseId + " ";
                decimal Stock = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (Stock > 0) {
                    dbConnection.cmd.CommandText = "SELECT AVAILABLE_QTY, (STOCK_VALUE/(AVAILABLE_QTY+HOLDED_QTY)) AS UNIT_PRICE, STOCK_VALUE FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID =" + ItemId + " AND WAREHOUSE_ID = " + WarehouseId + " ";
                }
            }
            catch (Exception ex) {

            }

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Inventory>(dbConnection.dr);
            }
        }

        public Inventory getBatchStockValues(int ItemId, int batchId, int WarehouseId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            try {
                dbConnection.cmd.CommandText = "SELECT (STOCK_VALUE / (AVAILABLE_QTY + HOLDED_QTY)) AS UNIT_PRICE, AVAILABLE_QTY, (AVAILABLE_QTY + HOLDED_QTY) AS TOTAL_QUANTITY, STOCK_VALUE " +
                                                         "FROM WAREHOUSE_INVENTORY_BATCHES WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " AND BATCH_ID = " + batchId + " ";


            }
            catch (Exception ex) {

            }

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Inventory>(dbConnection.dr);
            }
        }

        public int UpdateStock(int ItemId, int WarehouseId, decimal Qty, int UserId, int companyId, decimal up, DateTime expDate, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            Inventory W_Inventory = DAOFactory.CreateInventoryDAO().getStockValues(ItemId, WarehouseId, dbConnection);
            AddItem Item = DAOFactory.CreateAddItemDAO().FetchItemByItemId(ItemId, dbConnection);

            decimal AvalableQty = W_Inventory.AvailableQty;
            decimal AvailableStockValue = W_Inventory.StockValue;
            decimal UnitPrice = 0;
            int addToEmptyStock = 0;
            if (AvalableQty > 0) {
                UnitPrice = W_Inventory.UnitPrice;
            }
            else {
                UnitPrice = up;
                addToEmptyStock = 1;
            }
            decimal NewMasterStock = Qty * UnitPrice;
            int StockMaintaingType = Item.StockMaintainingType;
            decimal BatchUnitPrice = 0;
            

            if (Qty > AvalableQty) {
                decimal raisedQty = Qty - AvalableQty;
                if (StockMaintaingType != 1) {

                    if (addToEmptyStock == 0) {
                        dbConnection.cmd.CommandText = "SELECT MAX(BATCH_ID) FROM WAREHOUSE_INVENTORY_BATCHES WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " AND AVAILABLE_QTY > 0 ";
                        int batchId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                        //dbConnection.cmd.CommandText = "SELECT (STOCK_VALUE / (AVAILABLE_QTY + HOLDED_QTY)) AS UNIT_PRICE, AVAILABLE_QTY, (AVAILABLE_QTY + HOLDED_QTY) AS TOTAL_QUANTITY, STOCK_VALUE " +
                        //                                "FROM WAREHOUSE_INVENTORY_BATCHES WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " AND BATCH_ID = " + batchId + " ";
                        //decimal UnitPriceBatch = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                        //dbConnection.cmd.CommandText = "SELECT STOCK_VALUE " +
                        //                                "FROM WAREHOUSE_INVENTORY_BATCHES WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " AND BATCH_ID = " + batchId + " ";
                        //decimal AvailaleBatchStock = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                        //dbConnection.cmd.CommandText = "SELECT AVAILABLE_QTY  " +
                        //                                "FROM WAREHOUSE_INVENTORY_BATCHES WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " AND BATCH_ID = " + batchId + " ";
                        //decimal AvailableQty = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                        Inventory batches = DAOFactory.CreateInventoryDAO().getBatchStockValues(ItemId, batchId, WarehouseId, dbConnection);

                        decimal UnitPriceBatch = batches.UnitPrice;
                        decimal AvailaleBatchStock = batches.StockValue;
                        decimal AvailableQty = batches.AvailableQty;

                        decimal AddedQty = Qty - AvalableQty;
                        decimal NewBatchStock = AddedQty * UnitPriceBatch;
                        decimal fullqty = AddedQty + AvailableQty;
                        decimal fullstock = AvailaleBatchStock + NewBatchStock;

                        dbConnection.cmd.CommandText = "UPDATE WAREHOUSE_INVENTORY_BATCHES SET AVAILABLE_QTY = AVAILABLE_QTY + " + AddedQty + ", STOCK_VALUE = STOCK_VALUE + " + NewBatchStock + ", LAST_UPADATED_DATE = '" + LocalTime.Now + "', LAST_UPDATED_BY = " + UserId + " WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " AND BATCH_ID = " + batchId + "  ";

                        dbConnection.cmd.CommandText += "UPDATE WAREHOUSE_INVENTORY_MASTER SET AVAILABLE_QTY = " + Qty + ", STOCK_VALUE = STOCK_VALUE+" + NewBatchStock + ", LAST_UPDATED_DATE = '" + LocalTime.Now + "', LAST_UPDATED_BY = " + UserId + " WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " ";

                        //log master

                        dbConnection.cmd.CommandText += "DECLARE @OVERRIDE_LOG_ID TABLE(OVERRIDE_LOG_ID INT) \n" +
                        "INSERT INTO STOCK_OVERRIDE_LOG(ITEM_ID,WAREHOUSE_ID, EXISTED_QTY, EXISTED_STOCK_VALUE,OVERRIDING_QTY, OVERRIDING_STOCK_VALUE,OVERRIDDEN_BY, OVERRIDDEN_ON, OVERRIDING_TYPE,REMARKS) " +
                        "OUTPUT INSERTED.OVERRIDE_LOG_ID INTO @OVERRIDE_LOG_ID VALUES (" + ItemId + ", " + WarehouseId + "," + AvalableQty + "," + AvailableStockValue + "," + Qty + ",(SELECT STOCK_VALUE FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " )," + UserId + ",'" + LocalTime.Now + "'," + 3 + ",'From PR Expense')";

                        dbConnection.cmd.CommandText += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] ) " +
                       " VALUES(" + ItemId + ","+ batchId + ", " + WarehouseId + ", " + AvailableQty + "," + AvailaleBatchStock + "," + fullqty + "," + fullstock + ",(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";

                    }
                    else {

                        dbConnection.cmd.CommandText += "INSERT INTO WAREHOUSE_INVENTORY_BATCHES (BATCH_CODE, WAREHOUSE_ID, ITEM_ID, COMPANY_ID, EXPIRY_DATE,AVAILABLE_QTY,HOLDED_QTY, STOCK_VALUE, LAST_UPADATED_DATE, LAST_UPDATED_BY,  IS_ACTIVE, SELLING_PRICE) " +
                                                    "VALUES ((SELECT ISNULL(MAX(BATCH_CODE),0)+1 FROM WAREHOUSE_INVENTORY_BATCHES WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID = " + WarehouseId + "), " + WarehouseId + ", " + ItemId + ", " + companyId + ",'" + expDate + "'," + Qty + ",0," + NewMasterStock + ",'" + LocalTime.Now + "', " + UserId + ", 1, 0) ";

                        
                        dbConnection.cmd.CommandText +=  "IF EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + ") " +
                            "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER SET STOCK_VALUE= STOCK_VALUE+" + NewMasterStock + ", AVAILABLE_QTY=AVAILABLE_QTY+" + Qty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + UserId + " WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " " +
                        "ELSE " +
                            "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER (WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                            "VALUES (" + WarehouseId + "," + ItemId + "," + Qty + ",0," + NewMasterStock + ",'" + LocalTime.Now + "'," + UserId + ",1); ";

                        //log master

                        dbConnection.cmd.CommandText += "DECLARE @OVERRIDE_LOG_ID TABLE(OVERRIDE_LOG_ID INT) \n" +
                        "INSERT INTO STOCK_OVERRIDE_LOG(ITEM_ID,WAREHOUSE_ID, EXISTED_QTY, EXISTED_STOCK_VALUE,OVERRIDING_QTY, OVERRIDING_STOCK_VALUE,OVERRIDDEN_BY, OVERRIDDEN_ON, OVERRIDING_TYPE,REMARKS) " +
                        "OUTPUT INSERTED.OVERRIDE_LOG_ID INTO @OVERRIDE_LOG_ID VALUES (" + ItemId + ", " + WarehouseId + "," + AvalableQty + "," + AvailableStockValue + "," + Qty + "," + NewMasterStock + "," + UserId + ",'" + LocalTime.Now + "'," + 3 + ",'From PR Expense')";


                        dbConnection.cmd.CommandText += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] ) " +
                       " VALUES(" + ItemId + ",(SELECT MAX(BATCH_ID) FROM WAREHOUSE_INVENTORY_BATCHES WHERE ITEM_ID = " + ItemId + " AND WAREHOUSE_ID = " + WarehouseId + "), " + WarehouseId + ", 0,0," + Qty + "," + NewMasterStock + ",(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";

                    }
                    
                }
                else {
                    if (addToEmptyStock == 0) {
                        dbConnection.cmd.CommandText = "UPDATE WAREHOUSE_INVENTORY_MASTER SET AVAILABLE_QTY = " + Qty + ", STOCK_VALUE = " + NewMasterStock + ", LAST_UPDATED_DATE = '" + LocalTime.Now + "', LAST_UPDATED_BY = " + UserId + " WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " ";

                        dbConnection.cmd.CommandText += "DECLARE @OVERRIDE_LOG_ID TABLE(OVERRIDE_LOG_ID INT) \n" +
                        "INSERT INTO STOCK_OVERRIDE_LOG(ITEM_ID,WAREHOUSE_ID, EXISTED_QTY, EXISTED_STOCK_VALUE,OVERRIDING_QTY, OVERRIDING_STOCK_VALUE,OVERRIDDEN_BY, OVERRIDDEN_ON, OVERRIDING_TYPE,REMARKS) " +
                        "OUTPUT INSERTED.OVERRIDE_LOG_ID INTO @OVERRIDE_LOG_ID VALUES (" + ItemId + ", " + WarehouseId + "," + AvalableQty + "," + AvailableStockValue + "," + Qty + "," + NewMasterStock + "," + UserId + ",'" + LocalTime.Now + "'," + 3 + ",'From PR Expense')";

                    }
                    else {
                        dbConnection.cmd.CommandText +=  "IF EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + ") " +
                            "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER SET STOCK_VALUE= STOCK_VALUE+" + NewMasterStock + ", AVAILABLE_QTY=AVAILABLE_QTY+" + Qty + ",LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + UserId + " WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " " +
                        "ELSE " +
                            "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER (WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                            "VALUES (" + WarehouseId + "," + ItemId + "," + Qty + ",0," + NewMasterStock + ",'" + LocalTime.Now + "'," + UserId + ",1); ";

                        dbConnection.cmd.CommandText += "DECLARE @OVERRIDE_LOG_ID TABLE(OVERRIDE_LOG_ID INT) \n" +
                        "INSERT INTO STOCK_OVERRIDE_LOG(ITEM_ID,WAREHOUSE_ID, EXISTED_QTY, EXISTED_STOCK_VALUE,OVERRIDING_QTY, OVERRIDING_STOCK_VALUE,OVERRIDDEN_BY, OVERRIDDEN_ON, OVERRIDING_TYPE,REMARKS) " +
                        "OUTPUT INSERTED.OVERRIDE_LOG_ID INTO @OVERRIDE_LOG_ID VALUES (" + ItemId + ", " + WarehouseId + "," + AvalableQty + "," + AvailableStockValue + "," + Qty + "," + NewMasterStock + "," + UserId + ",'" + LocalTime.Now + "'," + 3 + ",'From PR Expense')";

                    }
                }

                dbConnection.cmd.CommandText += "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_RAISE (WAREHOUSE_ID,ITEM_ID,RAISED_QTY,RAISED_DATE,RAISED_BY,RAISED_TYPE,RAISED_WAREHOUSE_ID,IS_MANUAL_UPDATE) " +
                       "VALUES (" + WarehouseId + "," + ItemId + "," + raisedQty + ",'" + LocalTime.Now + "'," + UserId + ",2,0,3);";


            }

            else {
                decimal NewQty = AvalableQty - Qty;
                decimal releaedQty = AvalableQty - Qty;
                if (StockMaintaingType == 2) {
                    string sql = "";
                    decimal ReducedStock = 0;
                    decimal ReducedStockForLog = 0;

                    List<WarehouseInventoryBatches> batches = DAOFactory.CreateWarehouseInventoryBatchesDAO().getWarehouseInventoryBatchesListByWarehouseId(WarehouseId, ItemId, companyId, dbConnection);
                    // Insert record into STOCK_OVERRIDE_LOG table and update stock value after batches are updated
                    sql += "DECLARE @OVERRIDE_LOG_ID TABLE(OVERRIDE_LOG_ID INT) \n" +
                                                "INSERT INTO STOCK_OVERRIDE_LOG(ITEM_ID,WAREHOUSE_ID, EXISTED_QTY, EXISTED_STOCK_VALUE,OVERRIDING_QTY, OVERRIDING_STOCK_VALUE,OVERRIDDEN_BY, OVERRIDDEN_ON, OVERRIDING_TYPE,REMARKS) " +
                                                "OUTPUT INSERTED.OVERRIDE_LOG_ID INTO @OVERRIDE_LOG_ID VALUES (" + ItemId + ", " + WarehouseId + "," + AvalableQty + "," + AvailableStockValue + "," + Qty + ",0," + UserId + ",'" + LocalTime.Now + "'," + 3 + ",'From PR Expense')";

                    for (int i = 0; i < batches.Count; i++) {
                        decimal BatchAvailableQty = batches[i].AvailableStock;
                        decimal BatchStockVaue = batches[i].StockValue;
                        decimal BatchHoldedQTY = batches[i].HoldedQty;
                        int batchId = batches[i].BatchchId;
                        
                        if (BatchStockVaue != 0 && BatchAvailableQty != 0) {
                            BatchUnitPrice = (BatchStockVaue / (BatchAvailableQty + BatchHoldedQTY));
                        }

                        
                            
                        if (BatchAvailableQty >= NewQty) {
                            decimal NewStockQty = BatchAvailableQty - NewQty;
                            decimal NewStockValue = BatchStockVaue - (BatchUnitPrice * NewQty);
                            ReducedStock = ReducedStock + (BatchUnitPrice * NewQty);
                            NewQty = 0;

                            sql += "UPDATE WAREHOUSE_INVENTORY_BATCHES SET AVAILABLE_QTY = " + NewStockQty + ", STOCK_VALUE = " + NewStockValue + ", LAST_UPADATED_DATE = '" + LocalTime.Now + "', LAST_UPDATED_BY = " + UserId + " WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " AND BATCH_ID = " + batchId + "  ";
                            sql += "UPDATE WAREHOUSE_INVENTORY_MASTER SET AVAILABLE_QTY = " + Qty + ", STOCK_VALUE = STOCK_VALUE - " + ReducedStock + ", LAST_UPDATED_DATE = '" + LocalTime.Now + "', LAST_UPDATED_BY = " + UserId + " WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " ";
                            sql += "UPDATE STOCK_OVERRIDE_LOG SET  OVERRIDING_STOCK_VALUE = (SELECT STOCK_VALUE FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " ) WHERE OVERRIDE_LOG_ID = (SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID) ";

                            //log master

                            sql += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] )  VALUES(" + ItemId + ","+ batchId + ", " + WarehouseId + ", "+ BatchAvailableQty + ","+ BatchStockVaue + "," + NewStockQty + "," + NewStockValue + ",(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";

                            break;
                        }
                        else {
                            ReducedStock = ReducedStock + BatchStockVaue;
                            decimal NewStockQty = 0;
                            decimal NewStockValue = 0;
                            NewQty = NewQty - BatchAvailableQty;

                            sql += "UPDATE WAREHOUSE_INVENTORY_BATCHES SET AVAILABLE_QTY = " + NewStockQty + ", STOCK_VALUE = " + NewStockValue + ", LAST_UPADATED_DATE = '" + LocalTime.Now + "', LAST_UPDATED_BY = " + UserId + " WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " AND BATCH_ID = " + batchId + "  ";
                            
           
                            sql += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] )  VALUES(" + ItemId + "," + batchId + ", " + WarehouseId + ", " + BatchAvailableQty + "," + BatchStockVaue + "," + NewStockQty + "," + NewStockValue + ",(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";
                        }

                    }
                    dbConnection.cmd.CommandText = sql;
                }
                    else if (StockMaintaingType == 3) {
                    string sql = "";
                    decimal ReducedStock = 0;
                    decimal ReducedStockForLog = 0;
                    List<WarehouseInventoryBatches> batches = DAOFactory.CreateWarehouseInventoryBatchesDAO().getWarehouseInventoryBatchesListByWarehouseId(WarehouseId, ItemId, companyId, dbConnection);
                    
                    // Insert record into STOCK_OVERRIDE_LOG table and update stock value after batches are updated
                    sql += "DECLARE @OVERRIDE_LOG_ID TABLE(OVERRIDE_LOG_ID INT) \n" +
                        "INSERT INTO STOCK_OVERRIDE_LOG(ITEM_ID,WAREHOUSE_ID, EXISTED_QTY, EXISTED_STOCK_VALUE,OVERRIDING_QTY, OVERRIDING_STOCK_VALUE,OVERRIDDEN_BY, OVERRIDDEN_ON, OVERRIDING_TYPE,REMARKS) " +
                        "OUTPUT INSERTED.OVERRIDE_LOG_ID INTO @OVERRIDE_LOG_ID VALUES (" + ItemId + ", " + WarehouseId + "," + AvalableQty + "," + AvailableStockValue + "," + Qty + ",0," + UserId + ",'" + LocalTime.Now + "'," + 3 + ",'From PR Expense')";

                    for (int i = batches.Count-1; i >= 0; i--) {
                        decimal BatchAvailableQty = batches[i].AvailableStock;
                        decimal BatchStockVaue = batches[i].StockValue;
                        decimal BatchHoldedQTY = batches[i].HoldedQty;
                        int batchId = batches[i].BatchchId;
                        

                        if (BatchStockVaue != 0 && BatchAvailableQty != 0) {
                            BatchUnitPrice = (BatchStockVaue / (BatchAvailableQty + BatchHoldedQTY));
                        }
                        
                        if (BatchAvailableQty >= NewQty) {
                            decimal NewStockQty = BatchAvailableQty - NewQty;
                            decimal NewStockValue = BatchStockVaue - (BatchUnitPrice * NewQty);
                            ReducedStock = ReducedStock + (BatchUnitPrice * NewQty);
                            NewQty = 0;

                            sql += "UPDATE WAREHOUSE_INVENTORY_BATCHES SET AVAILABLE_QTY = " + NewStockQty + ", STOCK_VALUE = " + NewStockValue + ", LAST_UPADATED_DATE = '" + LocalTime.Now + "', LAST_UPDATED_BY = " + UserId + " WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " AND BATCH_ID = " + batchId + "  ";
                            sql += "UPDATE WAREHOUSE_INVENTORY_MASTER SET AVAILABLE_QTY = " + Qty + ", STOCK_VALUE = STOCK_VALUE - " + ReducedStock + ", LAST_UPDATED_DATE = '" + LocalTime.Now + "', LAST_UPDATED_BY = " + UserId + " WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " ";
                            sql += "UPDATE STOCK_OVERRIDE_LOG SET  OVERRIDING_STOCK_VALUE = (SELECT STOCK_VALUE FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " ) WHERE OVERRIDE_LOG_ID = (SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID) ";

                            sql += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE],[OVERRIDE_LOG_ID] )  VALUES(" + ItemId + "," + batchId + ", " + WarehouseId + ", " + BatchAvailableQty + "," + BatchStockVaue + "," + NewStockQty + "," + NewStockValue + ",(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";
                            break;
                        }
                        else {
                            ReducedStock = ReducedStock + BatchStockVaue;
                            decimal NewStockQty = 0;
                            decimal NewStockValue = 0;
                            NewQty = NewQty - BatchAvailableQty;

                            sql += "UPDATE WAREHOUSE_INVENTORY_BATCHES SET AVAILABLE_QTY = " + NewStockQty + ", STOCK_VALUE = " + NewStockValue + ", LAST_UPADATED_DATE = '" + LocalTime.Now + "', LAST_UPDATED_BY = " + UserId + " WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " AND BATCH_ID = " + batchId + "  ";
                            sql += "INSERT INTO STOCK_OVERRIDE_BATCH_LOG([ITEM_ID],[BATCH_ID], [WAREHOUSE_ID], [EXISTED_QTY],[EXISTED_STOCK_VALUE], [OVERRIDING_QTY] ,[OVERRIDING_STOCK_VALUE], [OVERRIDE_LOG_ID] )  VALUES(" + ItemId + "," + batchId + ", " + WarehouseId + ", " + BatchAvailableQty + "," + BatchStockVaue + "," + NewStockQty + "," + NewStockValue + ",(SELECT MAX(OVERRIDE_LOG_ID) FROM @OVERRIDE_LOG_ID)) ";

                        }

                    }
                    dbConnection.cmd.CommandText = sql;
                }

                

                else {
                    dbConnection.cmd.CommandText = "UPDATE WAREHOUSE_INVENTORY_MASTER SET AVAILABLE_QTY = " + Qty + ", STOCK_VALUE = " + NewMasterStock + ", LAST_UPDATED_DATE = '" + LocalTime.Now + "', LAST_UPDATED_BY = " + UserId + " WHERE WAREHOUSE_ID = " + WarehouseId + " AND ITEM_ID = " + ItemId + " ";
                    dbConnection.cmd.CommandText += "INSERT INTO STOCK_OVERRIDE_LOG(ITEM_ID,WAREHOUSE_ID, EXISTED_QTY, EXISTED_STOCK_VALUE,OVERRIDING_QTY, OVERRIDING_STOCK_VALUE,OVERRIDDEN_BY, OVERRIDDEN_ON, OVERRIDING_TYPE,REMARKS) " +
                   "VALUES (" + ItemId + ", " + WarehouseId + "," + AvalableQty + "," + AvailableStockValue + "," + Qty + "," + NewMasterStock + "," + UserId + ",'" + LocalTime.Now + "'," + 3 + ",'From PR Expense')";

                }
                dbConnection.cmd.CommandText += "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_RELEASE (WAREHOUSE_ID,ITEM_ID,RELEASED_QTY,RELEASED_DATE,RELEASED_BY,RELEASED_TYPE) " +
                "VALUES (" + WarehouseId + "," + ItemId + "," + releaedQty + ",'" + LocalTime.Now + "'," + UserId + ",2);";

            }

            
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<WarehouseInventory> FetchItemListDetailed(int companyid, int warehouseid, int itemid, int maincategoryid, int subcategoryid, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            string sql = "SELECT W.LOCATION,AI.ITEM_NAME,WIM.AVAILABLE_QTY,WIM.HOLDED_QTY,WIM.STOCK_VALUE, AI.ITEM_ID,W.WAREHOUSE_ID, AI.STOCK_MAINTAINING_TYPE, UN.MEASUREMENT_SHORT_NAME, WIM.REORDER_LEVEL FROM WAREHOUSE_INVENTORY_MASTER AS WIM \n" +
                            "INNER JOIN ADD_ITEMS AS AI ON WIM.ITEM_ID = AI.ITEM_ID \n" +
                            "INNER JOIN WAREHOUSE AS W ON WIM.WAREHOUSE_ID = W.WAREHOUSE_ID \n" +
                            "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = AI.MEASUREMENT_ID \n" +
                            "WHERE AI.COMPANY_ID = " + companyid + " AND W.IS_ACTIVE = 1";

            if (maincategoryid != 0) {
                sql += " AND AI.CATEGORY_ID =  " + maincategoryid + "";
            }

            if (subcategoryid != 0) {
                sql += " AND AI.SUB_CATEGORY_ID = " + subcategoryid + "";
            }
            if (itemid != 0) {
                sql += " AND AI.ITEM_ID =  " + itemid + "";
            }
            if (warehouseid != 0) {
                sql += " AND W.WAREHOUSE_ID =  " + warehouseid + "";
            }

            sql += " ORDER BY AI.ITEM_NAME";


            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<WarehouseInventory>(dbConnection.dr);
            }
        }

        public int ReturnMasterStock(int ItemID, int WarehouseID,decimal IssuedQty, decimal StockValue, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE WAREHOUSE_INVENTORY_MASTER SET LAST_UPDATED_BY = " + UserId + ", LAST_UPDATED_DATE = '" + LocalTime.Now + "', AVAILABLE_QTY = AVAILABLE_QTY + " + IssuedQty + ", STOCK_VALUE =STOCK_VALUE + " + StockValue + " " +
                "WHERE ITEM_ID = " + ItemID + " AND WAREHOUSE_ID =" + WarehouseID + " ";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int ReturnBatchStock(int BatchId, decimal IssuedQty, decimal IssuedStockValue, int WarehouseID, int ItemID, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE WAREHOUSE_INVENTORY_BATCHES SET LAST_UPDATED_BY = " + UserId + ", LAST_UPADATED_DATE = '" + LocalTime.Now + "', AVAILABLE_QTY = AVAILABLE_QTY + " + IssuedQty + ", STOCK_VALUE =STOCK_VALUE + " + IssuedStockValue + " " +
                 "WHERE ITEM_ID = " + ItemID + " AND WAREHOUSE_ID =" + WarehouseID + " AND BATCH_ID = "+ BatchId + " ";
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public Inventory InventoryBatchesByBatchId(int BatchId, int warehouseId, int itemID, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM WAREHOUSE_INVENTORY_BATCHES WHERE WAREHOUSE_ID = "+ warehouseId + " AND iTEM_ID = "+ itemID + " AND BATCH_ID = "+ BatchId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Inventory>(dbConnection.dr);
            }
        }
    }


}
