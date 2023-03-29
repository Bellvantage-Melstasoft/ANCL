using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface StockReleaseDAOInterface
    {
        int releaseStock(int subDepartmentID, int itemID, int releasedQty, int releasedBy, int releaseType, int releaseSubDepartmentID, DBConnection dbConnection);

    }

    class StockReleaseDAO : StockReleaseDAOInterface
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int releaseStock(int subDepartmentID, int itemID, int releasedQty, int releasedBy,int releaseType,int releaseSubDepartmentID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".STOCK_RELEASE " +
                                                "(SUB_DEPARTMENT_ID, ITEM_ID, RELEASED_QTY, RELEASED_DATE, RELEASED_BY,RELEASED_TYPE,RELEASED_SUB_DEPARTMENT_ID) " +
                                            "VALUES " +
                                                "(" + subDepartmentID + "," + itemID + ", " + releasedQty + ", '" +  LocalTime.Now + "', " + releasedBy + ", " + releaseType + ", " + releaseSubDepartmentID + "); " +
                                            "IF EXISTS (SELECT * FROM " + dbLibrary + ".STOCK_MASTER WHERE SUB_DEPARTMENT_ID = " + subDepartmentID + " AND ITEM_ID = " + itemID + ") " +
                                                "UPDATE " + dbLibrary + ".STOCK_MASTER SET STOCK=STOCK-" + releasedQty + " WHERE SUB_DEPARTMENT_ID = " + subDepartmentID + " AND ITEM_ID = " + itemID + ") " +
                                            "ELSE " +
                                                "INSERT INTO " + dbLibrary + ".STOCK_MASTER (SUB_DEPARTMENT_ID,ITEM_ID,STOCK) VALUES (" + subDepartmentID + "," + itemID + ",0)";
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
