using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
   public interface PRStockDepartmentDAO
    {
        PRStockDepartment GetPRStockDepartmentMrnId(int prId, DBConnection dbConnection);
        int savePRStockDepartment(int prId, int prd_Id, int itemId, decimal requestedQty, decimal availableQty, int MeasurementId , int userId, DateTime enteredDate, DBConnection dbConnection);
        PRStockDepartment GetPRStockDepartmentMrnItemId(int prId ,int itemId, DBConnection dbConnection);
    }

   public class PRStockDepartmentDAOImpl : PRStockDepartmentDAO
    {
       string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
       
       public PRStockDepartment GetPRStockDepartmentMrnId(int prId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();

           dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".PR_STOCK_DEPARTMENT WHERE PR_ID = " + prId + " ";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

           using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
           {
               DataAccessObject dataAccessObject = new DataAccessObject();
               return dataAccessObject.GetSingleOject<PRStockDepartment>(dbConnection.dr);
           }
       }

        public PRStockDepartment GetPRStockDepartmentMrnItemId(int prId , int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".PR_STOCK_DEPARTMENT WHERE PR_ID = " + prId + " AND ITEM_ID = " + itemId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PRStockDepartment>(dbConnection.dr);
            }
        }

        public int savePRStockDepartment(int prId, int prd_Id, int itemId, decimal requestedQty, decimal availableQty,int measurementId, int userId, DateTime enteredDate, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "SELECT Count(*)  FROM " + dbLibrary + ".PR_STOCK_DEPARTMENT WHERE PR_ID = " + prId + " AND ITEM_ID = "+ itemId + " ";
            if(Convert.ToDecimal(dbConnection.cmd.ExecuteScalar().ToString()) != 0)
            {
                dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".PR_STOCK_DEPARTMENT WHERE PR_ID = " + prId + " AND ITEM_ID = " + itemId + " ";
                dbConnection.cmd.ExecuteNonQuery();

                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_STOCK_DEPARTMENT (PR_ID, PRD_ID, ITEM_ID, REQUESTED_QTY, AVAILABLE_QTY, MEASUREMENT_ID ,ENTERED_USER ,ENTERED_DATE) VALUES" +
                      "( " + prId + ", " + prd_Id + " , " + itemId + ", " + requestedQty + ", " + availableQty + ","+ measurementId + " , " + userId + " , '" + enteredDate + "');";

                return dbConnection.cmd.ExecuteNonQuery();
            }else
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_STOCK_DEPARTMENT (PR_ID, PRD_ID, ITEM_ID, REQUESTED_QTY, AVAILABLE_QTY, MEASUREMENT_ID ,ENTERED_USER ,ENTERED_DATE) VALUES" +
                      "( " + prId + ", " + prd_Id + " , " + itemId + ", " + requestedQty + ", " + availableQty + "," + measurementId + " , " + userId + " , '" + enteredDate + "');";

                return dbConnection.cmd.ExecuteNonQuery();
            }          

       }

     

       public void upadtePRDStockInfoLog(int prID, int itemId, decimal stockBalance, decimal avgConsumption, string enteredUser, DateTime enteredDate ,DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();
           dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".PRD_STOCK_LOG_INFO "+
                                         " SET  STOCK_BALANCE = " + stockBalance + " "+
                                         ", AVG_CONSUMPTION = " + avgConsumption + " " +
                                         ", ENTERED_USER = '" + enteredUser + "' " +
                                         ", ENTERED_DATE = '" + enteredDate + "' " +
                                          " WHERE  PRD_ID = (SELECT PRD_ID FROM " + dbLibrary + ".PR_DETAIL WHERE  PR_ID = " + prID + " AND ITEM_ID =" + itemId + ") ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
           dbConnection.cmd.ExecuteNonQuery();
       }
   }
}
