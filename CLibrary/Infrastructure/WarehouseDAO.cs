using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface WarehouseDAOInterface
    {
        int saveWarehouse(string location, string phoneNo, int companyID,string address, int isActive, List<int> UserIds, DBConnection dbConnection);
        int updateWarehouse(int warehouseID, string location, string phoneNo, int companyID, string address, int isActive, List<int> UserIds, DBConnection dbConnection);
        int deleteWarehouse(int warehouseID, DBConnection dbConnection);
        List<Warehouse> getWarehouseList(int companyID,DBConnection dbConnection);
        Warehouse getWarehouseByID(int warehouseID, DBConnection dbConnection);
        int isUserHeadOfWarehouse(int userID, DBConnection dbConnection);
        List<Warehouse> getWarehouseList1(DBConnection dbConnection);
        List<WarehouseInventory> FetchItemAvailableStock(int warehouseId, IEnumerable<int> itemIds, DBConnection dbConnection);
        void UpdateWarehouseInventory(int warehouseId,MrnDetailsV2 mrnDetail,int updatedBy, DBConnection dbConnection);
        List<WarehouseInventory> getWarehouseListAtAddItems(int companyID, DBConnection dbConnection);
        List<WarehouseInventory> getWarehouseDetailsByWarehouseId(List<int> warehouseId, DBConnection dbConnection);
        List<WarehouseInventory> getWarehouseListAtAddItemsForUpdate(int companyID, int ItemId, DBConnection dbConnection);
    }

    class WarehouseDAO : WarehouseDAOInterface
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int saveWarehouse(string location, string phoneNo, int companyID, string address, int isActive, List<int> UserIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DECLARE @WAREHOUSE_IDS TABLE(WAREHOUSE_ID INT) \n";
            dbConnection.cmd.CommandText += "IF NOT EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE WHERE LOCATION = '" + location + "' AND COMPANY_ID=" + companyID + ") " +
                "INSERT INTO " + dbLibrary + ".WAREHOUSE (LOCATION,PHONE_NO,COMPANY_ID,ADDRESS,IS_ACTIVE) OUTPUT INSERTED.WAREHOUSE_ID INTO @WAREHOUSE_IDS VALUES  ('" + location + "','" + phoneNo + "'," + companyID + ",'" + address + "'," + isActive + ")";

            for (int i = 0; i < UserIds.Count; i++) {
                dbConnection.cmd.CommandText += "INSERT INTO USER_WAREHOUSE([USER_ID],WAREHOUSE_ID,[USER_TYPE]) VALUES(" + UserIds[i] + ", (SELECT MAX(WAREHOUSE_ID) FROM @WAREHOUSE_IDS), 1); \n";

            }
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateWarehouse(int warehouseID, string location, string phoneNo, int companyID, string address, int isActive, List<int> UserIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF EXISTS(SELECT * FROM " + dbLibrary + ".WAREHOUSE WHERE WAREHOUSE_ID = " + warehouseID + " AND LOCATION = '" + location + "') " +
                                            "UPDATE " + dbLibrary + ".WAREHOUSE SET LOCATION = '" + location + "',PHONE_NO = '" + phoneNo + "',ADDRESS = '" + address + "',IS_ACTIVE = " + isActive + " " +
                                            "WHERE WAREHOUSE_ID = " + warehouseID + " " +
                                            "ELSE IF NOT EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE WHERE LOCATION = '" + location + "' AND COMPANY_ID=" + companyID + ") " +
                                            "UPDATE " + dbLibrary + ".WAREHOUSE SET LOCATION='" + location + "',PHONE_NO='"+phoneNo+ "',ADDRESS = '" + address + "',IS_ACTIVE=" +isActive+ " WHERE WAREHOUSE_ID=" + warehouseID + "";

            dbConnection.cmd.CommandText += "DELETE FROM USER_WAREHOUSE WHERE WAREHOUSE_ID = " + warehouseID + " ;";

            for (int i = 0; i < UserIds.Count; i++) {
                dbConnection.cmd.CommandText += "INSERT INTO USER_WAREHOUSE([USER_ID],WAREHOUSE_ID,[USER_TYPE]) VALUES(" + UserIds[i] + ", " + warehouseID + ", 1); \n";

            }

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int deleteWarehouse(int warehouseID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".WAREHOUSE SET IS_ACTIVE=0 WHERE WAREHOUSE_ID=" + warehouseID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Warehouse> getWarehouseList(int companyID, DBConnection dbConnection)
        {
             List< Warehouse > warehouses;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".WAREHOUSE AS W " +
                "WHERE W.COMPANY_ID=" + companyID + " AND W.IS_ACTIVE=1 ORDER BY W.LOCATION";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                warehouses = dataAccessObject.ReadCollection<Warehouse>(dbConnection.dr);
            }

            for (int i = 0; i < warehouses.Count; i++) {
                warehouses[i].HeadOfWarehouseName = string.Join(", ",
                    DAOFactory.createUserWarehouseDAO().GetWarehouseHeadsByWarehouseId(warehouses[i].WarehouseID, dbConnection).Select(w => w.UserName));
            }

            return warehouses;
        }

        public Warehouse getWarehouseByID(int warehouseID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".WAREHOUSE WHERE WAREHOUSE_ID=" + warehouseID;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Warehouse>(dbConnection.dr).First();
            }
        }

        public int isUserHeadOfWarehouse(int userID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".WAREHOUSE WHERE HEAD_OF_WAREHOUSE=" + userID;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if(dbConnection.dr.HasRows)
                {
                    dbConnection.dr.Read();
                    return int.Parse(dbConnection.dr["WAREHOUSE_ID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        public List<Warehouse> getWarehouseList1( DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".WAREHOUSE ORDER BY LOCATION";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Warehouse>(dbConnection.dr);
            }
        }

        public List<WarehouseInventory> FetchItemAvailableStock(int warehouseId, IEnumerable<int> itemIds, DBConnection dbConnection)
        {
            List<WarehouseInventory> warehousesInventory = new List<WarehouseInventory>();
            dbConnection.cmd.Parameters.Clear();
            string itemIdString = string.Join(", ", itemIds);
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER AS WIM " +
                                           "WHERE WIM.WAREHOUSE_ID=" + warehouseId + " AND WIM.ITEM_ID IN ("+ itemIdString + ") AND WIM.IS_ACTIVE=1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                warehousesInventory = dataAccessObject.ReadCollection<WarehouseInventory>(dbConnection.dr);
            }            
            return warehousesInventory;
        }

        public void UpdateWarehouseInventory(int warehouseId,MrnDetailsV2 mrnDetail,int updatedBy, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            // Holding qty is not used
            string sql = "IF EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + warehouseId + " AND ITEM_ID = " + mrnDetail.ItemId + ") " +
                         "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER "+
                         " SET AVAILABLE_QTY = AVAILABLE_QTY -" + mrnDetail.IssuedQty + ","+
                         " HOLDED_QTY=HOLDED_QTY+" + mrnDetail.IssuedQty + ","+
                         " LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + updatedBy + " "+
                         " WHERE WAREHOUSE_ID = " + warehouseId + " AND ITEM_ID = " + mrnDetail.ItemId + " " +
                         "ELSE " +
                         "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER (WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
                         "VALUES (" + warehouseId + "," + mrnDetail.ItemId + ",0," + mrnDetail.IssuedQty + ",0,'" + LocalTime.Now + "'," + updatedBy + ",1); ";
            dbConnection.cmd.CommandText = sql;


            //dbConnection.cmd.CommandText = "IF EXISTS (SELECT * FROM " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER  AS WIM  WHERE  WIM.WAREHOUSE_ID=" + warehouseId + " AND WIM.ITEM_ID =" + mrnDetail.ItemId + ") " +
            //                                "UPDATE " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER  "+
            //                                " SET STOCK_VALUE= CASE WHEN AVAILABLE_QTY>0 AND((STOCK_VALUE/(AVAILABLE_QTY))*(AVAILABLE_QTY-" + mrnDetail.IssuedQty + ")>=0) "+
            //                                " THEN (STOCK_VALUE/(AVAILABLE_QTY))*(AVAILABLE_QTY-" + mrnDetail.IssuedQty + ") ELSE 0 END, AVAILABLE_QTY= CASE WHEN AVAILABLE_QTY>0 THEN AVAILABLE_QTY-" + mrnDetail.IssuedQty + " ELSE 0 END " +
            //                                " ,LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=" + updatedBy + " WHERE WAREHOUSE_ID=" + warehouseId + " AND ITEM_ID =" + mrnDetail.ItemId + " " +
            //                                "ELSE " +
            //                                    "INSERT INTO " + dbLibrary + ".WAREHOUSE_INVENTORY_MASTER (COMPANY_ID,ITEM_ID,AVAILABLE_QTY,STOCK_VALUE,REORDER_LEVEL,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) " +
            //                                    "VALUES (" + companyID + "," + itemID + ",0,0,0,'" + LocalTime.Now + "'," + updatedBy + ",1)";
            dbConnection.cmd.ExecuteNonQuery();
        }



        public List<WarehouseInventory> getWarehouseListAtAddItems(int companyID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".WAREHOUSE " +
                "WHERE COMPANY_ID=" + companyID + " AND IS_ACTIVE=1 ORDER BY LOCATION";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<WarehouseInventory>(dbConnection.dr);
            }
        }

        public List<WarehouseInventory> getWarehouseDetailsByWarehouseId(List<int> warehouseId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".WAREHOUSE " +
                "WHERE WAREHOUSE_ID IN (" + string.Join(",", warehouseId) + ") AND IS_ACTIVE=1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<WarehouseInventory>(dbConnection.dr);
            }
        }

        public List<WarehouseInventory> getWarehouseListAtAddItemsForUpdate(int companyID, int ItemId, DBConnection dbConnection)
        {
            String sql = "";
            sql = sql + "SELECT W.WAREHOUSE_ID,W.LOCATION,ISNULL(WIM.AVAILABLE_QTY,0) AS AVAILABLE_QTY,ISNULL(WIM.STOCK_VALUE,0) AS STOCK_VALUE,ISNULL(WIM.REORDER_LEVEL,0) AS REORDER_LEVEL FROM WAREHOUSE AS W " + "\n";
            sql = sql + "LEFT JOIN (SELECT * FROM WAREHOUSE_INVENTORY_MASTER WHERE ITEM_ID=" + ItemId + ") AS WIM ON W.WAREHOUSE_ID = WIM.WAREHOUSE_ID " + "\n";
            sql = sql + "WHERE W.COMPANY_ID = " + companyID + " AND W.IS_ACTIVE=1 \n";
            sql = sql + "ORDER BY W.LOCATION";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<WarehouseInventory>(dbConnection.dr);
            }
        }

    }

}
