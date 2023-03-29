using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
   public interface PRDStockInfoLogDAO
    {
         PRDStockInfo GetProStockInfoLogById(int prDId, DBConnection dbConnection);

        void savePRDStockInfoLog(int PRD_Id, decimal stockBalance, decimal lastPaurchasePrice, int supplierId, DateTime? LastPurchaseDate, decimal avgConsumption, string enteredUser, DateTime enteredDate, DBConnection dbConnection);

        PRDStockInfo GetProStockInfoLogByItemId(int itemId, DBConnection dbConnection);

        void upadtePRDStockInfoLog(int prID, int itemId, decimal stockBalance, decimal avgConsumption, string enteredUser, DateTime enteredDate, DBConnection dbConnection);
    }

   public class PRDStockInfoLogDAOImpl : PRDStockInfoLogDAO
   {
       string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
       
       public PRDStockInfo GetProStockInfoLogById(int prDId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();

           dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".PRD_STOCK_LOG_INFO WHERE PRD_ID = " + prDId + " ";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

           using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
           {
               DataAccessObject dataAccessObject = new DataAccessObject();
               return dataAccessObject.GetSingleOject<PRDStockInfo>(dbConnection.dr);
           }
       }

       public void savePRDStockInfoLog(int PRD_Id, decimal stockBalance, decimal lastPaurchasePrice, int supplierId, DateTime? LastPurchaseDate, decimal avgConsumption,string enteredUser , DateTime enteredDate, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();
           dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PRD_STOCK_LOG_INFO (PRD_ID, STOCK_BALANCE, LAST_PURCHASE_PRICE, SUPPLIER_ID, LAST_PURCHASE_DATE,AVG_CONSUMPTION ,ENTERED_USER ,ENTERED_DATE) VALUES" +
                      "( " + PRD_Id + ", " + stockBalance + " , " + lastPaurchasePrice + ", " + supplierId + ", '" + LastPurchaseDate + "', " + avgConsumption + " , '" + enteredUser + "' ,'"+ enteredDate + "');";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;
           dbConnection.cmd.ExecuteNonQuery();

       }

       public PRDStockInfo GetProStockInfoLogByItemId(int itemId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();

           dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".PRD_STOCK_LOG_INFO as a INNER JOIN  " + dbLibrary + ".PR_DETAIL as b"+
                                          " ON a.PRD_ID =  b.PRD_ID "+
                                          " WHERE ITEM_ID = " + itemId + " ";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

           using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
           {
               DataAccessObject dataAccessObject = new DataAccessObject();
               return dataAccessObject.GetSingleOject<PRDStockInfo>(dbConnection.dr);
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
