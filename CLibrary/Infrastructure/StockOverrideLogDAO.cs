using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface StockOverrideLogDAO
    {
        List<StockOverrideLog> GetStockLog(int ItemId, DBConnection dbConnection);
        List<StockOverrideLog> GetOVerRiddenLog(int WarehouseId, int companyId, int itemid, int maincategoryid, int subcategoryid, DateTime to, DateTime from, DBConnection dbConnection);
            
        }

        public class StockOverrideLogDAODAOSQLImpl : StockOverrideLogDAO
    {
        public List<StockOverrideLog> GetStockLog(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT SOL.*, WH.LOCATION, CL.FIRST_NAME FROM STOCK_OVERRIDE_LOG AS SOL " +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = SOL.OVERRIDDEN_BY " +
                                            "INNER JOIN (SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE) AS WH ON WH.WAREHOUSE_ID = SOL.WAREHOUSE_ID " +
                                            "WHERE SOL.ITEM_ID = " + ItemId + " ORDER BY SOL.OVERRIDDEN_ON DESC ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<StockOverrideLog>(dbConnection.dr);
            }
        }

        public List<StockOverrideLog> GetOVerRiddenLog(int WarehouseId, int companyId, int itemid, int maincategoryid, int subcategoryid, DateTime to, DateTime from, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            string sql = "SELECT * FROM STOCK_OVERRIDE_LOG AS STLOG " +
                                             "INNER JOIN (SELECT ITEM_ID,ITEM_NAME, MEASUREMENT_ID,CATEGORY_ID, SUB_CATEGORY_ID,STOCK_MAINTAINING_TYPE FROM ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON STLOG.ITEM_ID=AIM.ITEM_ID  " +
                                            "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY_MASTER) AS ICTM ON ICTM.CATEGORY_ID = AIM.CATEGORY_ID   " +
                                            "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS ISC ON ISC.SUB_CATEGORY_ID = AIM.SUB_CATEGORY_ID   " +
                                             "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE) AS W ON W.WAREHOUSE_ID = STLOG.WAREHOUSE_ID   " +
                                            "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = AIM.MEASUREMENT_ID " +
                                             "LEFT JOIN (SELECT USER_ID,FIRST_NAME AS UPDATED_USER FROM COMPANY_LOGIN) AS CL ON STLOG.OVERRIDDEN_BY = CL.USER_ID   " +
                                              "WHERE  STLOG.WAREHOUSE_ID = " + WarehouseId + " AND STLOG.OVERRIDDEN_ON >= '" + from + "' AND STLOG.OVERRIDDEN_ON <=  DATEADD(DAY, 1,'" + to + "') ";

            if (maincategoryid != 0) {
                sql += " AND AIM.CATEGORY_ID =  " + maincategoryid + "";
            }

            if (subcategoryid != 0) {
                sql += " AND AIM.SUB_CATEGORY_ID = " + subcategoryid + "";
            }
            if (itemid != 0) {
                sql += " AND AIM.ITEM_ID =  " + itemid + "";
            }

            sql += " ORDER BY STLOG.OVERRIDDEN_ON DESC";


            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<StockOverrideLog>(dbConnection.dr);
            }
        }
    }
}
