using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
   public interface MRNStockDepartmentDAO
    {
        MRNStockDepartment GetMRNStockDepartmentMrnId(int mrnId, DBConnection dbConnection);
        int saveMRNStockDepartment(int mrnId, int mrnd_Id, int itemId, decimal requestedQty, decimal availableQty, int MeasurementId , int userId, DateTime enteredDate, DBConnection dbConnection);
        MRNStockDepartment GetMRNStockDepartmentMrnItemId(int mrnid ,int itemId, DBConnection dbConnection);
    }

   public class MRNStockDepartmentDAOImpl : MRNStockDepartmentDAO
    {
       string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
       
       public MRNStockDepartment GetMRNStockDepartmentMrnId(int mrnId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();

           dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".MRN_STOCK_DEPARTMENT WHERE MRN_ID = " + mrnId + " ";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

           using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
           {
               DataAccessObject dataAccessObject = new DataAccessObject();
               return dataAccessObject.GetSingleOject<MRNStockDepartment>(dbConnection.dr);
           }
       }

        public MRNStockDepartment GetMRNStockDepartmentMrnItemId(int mrnId , int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".MRN_STOCK_DEPARTMENT WHERE MRN_ID = " + mrnId + " AND ITEM_ID = " + itemId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MRNStockDepartment>(dbConnection.dr);
            }
        }

        public int saveMRNStockDepartment(int mrnId, int mrnd_Id, int itemId, decimal requestedQty, decimal availableQty,int measurementId, int userId, DateTime enteredDate, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "SELECT Count(*)  FROM " + dbLibrary + ".MRN_STOCK_DEPARTMENT WHERE MRN_ID = " + mrnId + " AND ITEM_ID = "+ itemId + " ";
            if(Convert.ToDecimal(dbConnection.cmd.ExecuteScalar().ToString()) != 0)
            {
                dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".MRN_STOCK_DEPARTMENT WHERE MRN_ID = " + mrnId + " AND ITEM_ID = " + itemId + " ";
                dbConnection.cmd.ExecuteNonQuery();

                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".MRN_STOCK_DEPARTMENT (MRN_ID, MRND_ID, ITEM_ID, REQUESTED_QTY, AVAILABLE_QTY, MEASUREMENT_ID ,ENTERED_USER ,ENTERED_DATE) VALUES" +
                      "( " + mrnId + ", " + mrnd_Id + " , " + itemId + ", " + requestedQty + ", " + availableQty + ","+ measurementId + " , " + userId + " , '" + enteredDate + "');";

                return dbConnection.cmd.ExecuteNonQuery();
            }else
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".MRN_STOCK_DEPARTMENT (MRN_ID, MRND_ID, ITEM_ID, REQUESTED_QTY, AVAILABLE_QTY, MEASUREMENT_ID ,ENTERED_USER ,ENTERED_DATE) VALUES" +
                      "( " + mrnId + ", " + mrnd_Id + " , " + itemId + ", " + requestedQty + ", " + availableQty + "," + measurementId + " , " + userId + " , '" + enteredDate + "');";

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
