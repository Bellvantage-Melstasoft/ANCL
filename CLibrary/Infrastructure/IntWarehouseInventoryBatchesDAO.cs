using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface WarehouseInventoryBatchesDAO
    {

        int saveWarehouseInventoryBatches(List<WarehouseInventoryBatches> batches, DBConnection dbConnection);
        int deleteWarehouseInventoryBatches(int warehouseId, int itemId, int batchId, int companyId, int BatchId, DBConnection dbConnection);
        List<WarehouseInventoryBatches> getAllWarehouseInventoryBatchesList(int itemId, int companyID, DBConnection dbConnection);
        List<WarehouseInventoryBatches> getWarehouseInventoryBatches(int itemId, int companyId, DBConnection dbConnection);
        List<WarehouseInventoryBatches> getWarehouseInventoryBatchesListByWarehouseId(int warehouseId, int itemId, int companyId, DBConnection dbConnection);
        int UpdateItemBatches(int itemId, int companyId, string warhouseIds, int stockMaintainingType, List<WarehouseInventoryBatches> batches, DBConnection dbConnection);
        List<WarehouseInventoryBatches> getWarehouseInventoryBatchesForInventory(int itemId, int companyId, int WarehouseId, DBConnection dbConnection);
        int DeleteStockMaintainingTypeChanges(int itemId, int companyId, DBConnection dbConnection);
        int AddBAtchesMaintainingTypeChanges(List<WarehouseInventory> InventoryList, int UserId, int companyId, DBConnection dbConnection);
        List<WarehouseInventoryBatches> getWarehouseInventoryBatchesForInventoryEdit(int itemId, int companyId, int WarehouseId, int BatchId, DBConnection dbConnection);
    }
    public class WarehouseInventoryBatchesDAOImpl : WarehouseInventoryBatchesDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int saveWarehouseInventoryBatches(List<WarehouseInventoryBatches> batches, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "Select Count(*) as cnt  from " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES";
            int BatchCode = 0;
            if (int.Parse(dbConnection.cmd.ExecuteScalar().ToString()) == 0)
            {
                BatchCode = 1;
            }
            else
            {
                dbConnection.cmd.CommandText = "Select MAX(BATCH_CODE) + 1  from " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES";
                BatchCode = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            string sql = "";

            for (int i = 0; i < batches.Count; i++)
            {
                sql += "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES \n" +
                        "VALUES \n" +
                        "(" + BatchCode + "," + batches[i].WarehouseID + "," + batches[i].ItemID + ", " + batches[i].CompanyId + ", '" + batches[i].ExpiryDate + "'," + batches[i].AvailableStock + "," + batches[i].HoldedQty + " , " + batches[i].StockValue + " ,'" + LocalTime.Now + "' , " + batches[i].LastUpdatedBy + ",1,0, 0) \n";
                ++BatchCode;
            }
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }



        public int deleteWarehouseInventoryBatches(int warehouseId, int itemId, int batchId, int companyId, int BatchId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES SET IS_ACTIVE=0 " +
                                           " WHERE WAREHOUSE_ID=" + warehouseId + " AND COMPANY_ID =" + companyId + " AND  ITEM_ID=" + itemId + " AND BATCH_ID="+ BatchId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int AddBAtchesMaintainingTypeChanges(List<WarehouseInventory> InventoryList, int UserId, int companyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            string sql = "";
            for (int i = 0; i < InventoryList.Count; i++) {


                sql += "INSERT INTO WAREHOUSE_INVENTORY_BATCHES ( [BATCH_CODE], [WAREHOUSE_ID], [ITEM_ID], [COMPANY_ID], [EXPIRY_DATE], [AVAILABLE_QTY], [HOLDED_QTY], [STOCK_VALUE], [LAST_UPADATED_DATE], [LAST_UPDATED_BY], [IS_ACTIVE], [SELLING_PRICE]) " +
                                            " VALUES ( ((SELECT ISNULL(MAX(BATCH_CODE),0)+1 FROM WAREHOUSE_INVENTORY_BATCHES WHERE ITEM_ID = " + InventoryList[i].ItemID + " AND WAREHOUSE_ID = " + InventoryList[i].WarehouseID + ")), " + InventoryList[i].WarehouseID + ", " + InventoryList[i].ItemID + ", "+companyId+ ", '',  " + InventoryList[i].AvailableQty + "," + InventoryList[i].HoldedQty + "," + InventoryList[i].StockValue + ",'"+LocalTime.Now+"', "+UserId+", 1, 0 )";
            }
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteStockMaintainingTypeChanges( int itemId,  int companyId,  DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES SET IS_ACTIVE=0 " +
                                           " WHERE COMPANY_ID =" + companyId + " AND  ITEM_ID=" + itemId + " ";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<WarehouseInventoryBatches> getAllWarehouseInventoryBatchesList(int itemId, int companyID, DBConnection dbConnection)
        {
            List<WarehouseInventoryBatches> warehouseInventoryBatches;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES " +
                                           "WHERE COMPANY_ID=" + companyID + "  AND ITEM_ID=" + itemId + " AND IS_ACTIVE=1 " +
                                           " ORDER BY BATCH_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                warehouseInventoryBatches = dataAccessObject.ReadCollection<WarehouseInventoryBatches>(dbConnection.dr);
            }

            return warehouseInventoryBatches;
        }

        public List<WarehouseInventoryBatches> getWarehouseInventoryBatches(int itemId, int companyId, DBConnection dbConnection)
        {

            List<WarehouseInventoryBatches> warehouseInventoryBatches;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM WAREHOUSE_INVENTORY_BATCHES " +
                                           " WHERE IS_ACTIVE = 1 AND ITEM_ID = " + itemId + "  AND COMPANY_ID=" + companyId + " AND AVAILABLE_QTY > 0 " +
                                            " ORDER BY BATCH_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                warehouseInventoryBatches = dataAccessObject.ReadCollection<WarehouseInventoryBatches>(dbConnection.dr);
            }

            return warehouseInventoryBatches;
        }

        public List<WarehouseInventoryBatches> getWarehouseInventoryBatchesListByWarehouseId(int warehouseId, int itemId, int companyId, DBConnection dbConnection)
        {
            List<WarehouseInventoryBatches> warehouseInventoryBatches;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES " +
                                           "WHERE WAREHOUSE_ID=" + warehouseId + " AND COMPANY_ID=" + companyId + "  AND ITEM_ID=" + itemId + " AND IS_ACTIVE=1 AND AVAILABLE_QTY > 0 " +
                                           " ORDER BY BATCH_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                warehouseInventoryBatches = dataAccessObject.ReadCollection<WarehouseInventoryBatches>(dbConnection.dr);
            }

            return warehouseInventoryBatches;
        }


        public int UpdateItemBatches(int itemId, int companyId, string warhouseIds, int stockMaintainingType, List<WarehouseInventoryBatches> batches, DBConnection dbConnection)
        {
            string query = string.Empty;

            if (warhouseIds != "")
            {

                query = "UPDATE WAREHOUSE_INVENTORY_BATCHES SET IS_ACTIVE = 0 WHERE COMPANY_ID =" + companyId + "  AND ITEM_ID=" + itemId + " AND WAREHOUSE_ID IN (" + warhouseIds + ") ";
                dbConnection.cmd.CommandText = query;
                dbConnection.cmd.ExecuteNonQuery();

            }
            if (stockMaintainingType != 1)
            {
                for (int i = 0; i < batches.Count; ++i)
                {
                    dbConnection.cmd.CommandText = "Select Count(*) as cnt  from " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES " +
                                                    "WHERE COMPANY_ID =" + companyId + " AND WAREHOUSE_ID=" + batches[i].WarehouseID + "  AND ITEM_ID=" + itemId + " AND AVAILABLE_QTY=" + batches[i].AvailableStock + "  AND STOCK_VALUE =" + batches[i].StockValue + " AND IS_ACTIVE=1  ";
                    if (int.Parse(dbConnection.cmd.ExecuteScalar().ToString()) == 0)
                    {
                        dbConnection.cmd.CommandText = "Select Count(*) as cnt  from " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES";
                        int BatchCode = 0;
                        if (int.Parse(dbConnection.cmd.ExecuteScalar().ToString()) == 0)
                        {
                            BatchCode = 1;
                        }
                        else
                        {
                            dbConnection.cmd.CommandText = "Select MAX(BATCH_CODE) + 1  from " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES";
                            BatchCode = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                        }
                        dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES(BATCH_CODE,WAREHOUSE_ID,ITEM_ID,COMPANY_ID,EXPIRY_DATE,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPADATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) \n" +
                                                       "VALUES \n" +
                                                        "(" + BatchCode + "," + batches[i].WarehouseID + "," + itemId + ", " + batches[i].CompanyId + ", '" + LocalTime.Now + "'," + batches[i].AvailableStock + "," + batches[i].HoldedQty + " , " + batches[i].StockValue + " ,'" + LocalTime.Now + "' , " + batches[i].LastUpdatedBy + ",1) \n";
                        dbConnection.cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_BATCHES SET IS_ACTIVE=1 " +
                                                       " WHERE WAREHOUSE_ID=" + batches[i].WarehouseID + " AND COMPANY_ID =" + companyId + " AND  ITEM_ID=" + itemId + " ";
                        dbConnection.cmd.ExecuteNonQuery();
                    }
                }
            }
            return 1;
        }

        public List<WarehouseInventoryBatches> getWarehouseInventoryBatchesForInventory(int itemId, int companyId,int WarehouseId, DBConnection dbConnection) {

            List<WarehouseInventoryBatches> warehouseInventoryBatches;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM WAREHOUSE_INVENTORY_BATCHES " +
                                           " WHERE IS_ACTIVE = 1 AND ITEM_ID = " + itemId + "  AND COMPANY_ID=" + companyId + " AND WAREHOUSE_ID = "+ WarehouseId + " " +
                                            " ORDER BY BATCH_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                warehouseInventoryBatches = dataAccessObject.ReadCollection<WarehouseInventoryBatches>(dbConnection.dr);
            }

            return warehouseInventoryBatches;
        }

        public List<WarehouseInventoryBatches> getWarehouseInventoryBatchesForInventoryEdit(int itemId, int companyId, int WarehouseId, int BatchId, DBConnection dbConnection) {

            List<WarehouseInventoryBatches> warehouseInventoryBatches;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM WAREHOUSE_INVENTORY_BATCHES " +
                                           " WHERE IS_ACTIVE = 1 AND ITEM_ID = " + itemId + "  AND COMPANY_ID=" + companyId + "AND BATCH_ID = "+ BatchId + " AND WAREHOUSE_ID = " + WarehouseId + " " +
                                            " ORDER BY BATCH_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                warehouseInventoryBatches = dataAccessObject.ReadCollection<WarehouseInventoryBatches>(dbConnection.dr);
            }

            return warehouseInventoryBatches;
        }
    }
}
