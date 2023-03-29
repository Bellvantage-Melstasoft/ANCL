using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface StockMasterDAOInterface
    {
        int saveStock(int subDepartmentID,int itemID,int qty, DBConnection dbConnection);
        int getStock(int subDepartmentID, int itemID, DBConnection dbConnection);
    }

    class StockMasterDAO : StockMasterDAOInterface
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int saveStock(int subDepartmentID, int itemID, int qty, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF EXISTS (SELECT * FROM " + dbLibrary + ".STOCK_MASTER WHERE SUB_DEPARTMENT_ID = " + subDepartmentID+ " AND ITEM_ID = " + itemID + ") " +
                                                "UPDATE " + dbLibrary + ".STOCK_MASTER SET STOCK=STOCK+" + qty+ " WHERE SUB_DEPARTMENT_ID = " + subDepartmentID + " AND ITEM_ID = " + itemID + ") " +
                                            "ELSE " +
                                                "INSERT INTO " + dbLibrary + ".STOCK_MASTER (SUB_DEPARTMENT_ID,ITEM_ID,STOCK) VALUES (" + subDepartmentID + "," + itemID + "," + qty + ")";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int getStock(int subDepartmentID, int itemID,DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT STOCK FROM " + dbLibrary + ".STOCK_MASTER WHERE SUB_DEPARTMENT_ID = " + subDepartmentID + " AND ITEM_ID = " + itemID;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                return int.Parse(dbConnection.dr["STOCK"].ToString());
            }
        }
    }
}
