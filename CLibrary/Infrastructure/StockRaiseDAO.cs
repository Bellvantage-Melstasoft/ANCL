using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface StockRaiseDAOInterface
    {
        int raiseStock(int subDepartmentID, int itemID, int raisedQty,int raisedBy, DBConnection dbConnection);

    }

    class StockRaiseDAO : StockRaiseDAOInterface
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int raiseStock(int subDepartmentID, int itemID, int raisedQty, int raisedBy, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".STOCK_RAISE " +
                                                "(SUB_DEPARTMENT_ID, ITEM_ID, RAISED_QTY, RAISED_DATE, RAISED_BY) " +
                                            "VALUES " +
                                                "(" + subDepartmentID + "," + itemID + ", " + raisedQty + ", '" +  LocalTime.Now + "', " + raisedBy + "); " +
                                            "IF EXISTS (SELECT * FROM " + dbLibrary + ".STOCK_MASTER WHERE SUB_DEPARTMENT_ID = " + subDepartmentID + " AND ITEM_ID = " + itemID + "); " +
                                                "UPDATE " + dbLibrary + ".STOCK_MASTER SET STOCK=STOCK+" + raisedQty + " WHERE SUB_DEPARTMENT_ID = " + subDepartmentID + " AND ITEM_ID = " + itemID + ") " +
                                            "ELSE " +
                                                "INSERT INTO " + dbLibrary + ".STOCK_MASTER (SUB_DEPARTMENT_ID,ITEM_ID,STOCK) VALUES (" + subDepartmentID + "," + itemID + "," + raisedQty + ")";
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
