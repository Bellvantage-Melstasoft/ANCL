using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
   public interface Bid_Bond_Details_DAO
    {
       int SaveBid_Bond_Details(int bidId, int bondTypeTd, int isRequired, decimal amount, decimal percentage, DateTime fromDate, DateTime toDate, string enteredUser, string EnteredDate, DBConnection dbConnection);

    }

   public class Bid_Bond_Details_DAOImpl : Bid_Bond_Details_DAO
   {
       string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

       public int SaveBid_Bond_Details(int bidId, int bondTypeTd, int isRequired, decimal amount, decimal percentage, DateTime fromDate, DateTime toDate, string enteredUser, string EnteredDate, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".BID_BOND_DETAILS (BID_ID, BOND_TYPE_ID, IS_REQUIRED, AMOUNT, PERCENTAGE, FROM_DATE,TO_DATE,ENTERED_DATE,ENTERED_USER) VALUES ( " + bidId + ", " + bondTypeTd + " , " + isRequired + ", '" + amount + "', '" + percentage + "', '" + fromDate + "', '" + toDate + "', '" + EnteredDate + "', '" + enteredUser + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
   
   }
}
