using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;


namespace CLibrary.Controller
{
     public interface Bid_Bond_Details_Controller
    {
         int SaveBid_Bond_Details(int bidId, int bondTypeTd, int isRequired, decimal amount, decimal percentage, DateTime fromDate, DateTime toDate,string enteredUser, string EnteredDate);
    }

     public class Bid_Bond_Details_ControllerImpl : Bid_Bond_Details_Controller
     {
         public int SaveBid_Bond_Details(int bidId, int bondTypeTd, int isRequired, decimal amount, decimal percentage, DateTime fromDate, DateTime toDate,string enteredUser, string EnteredDate)
         {
             DBConnection dbConnection = new DBConnection();
             try
             {
                 Bid_Bond_Details_DAO bid_Bond_Details_DAO = DAOFactory.createBid_Bond_Details_DAO();
                 return bid_Bond_Details_DAO.SaveBid_Bond_Details(bidId, bondTypeTd, isRequired, amount, percentage, fromDate,toDate,enteredUser,EnteredDate, dbConnection);
             }
             catch (Exception ex)
             {
                 dbConnection.RollBack();
                 throw;
             }
             finally
             {
                 if (dbConnection.con.State == System.Data.ConnectionState.Open)
                     dbConnection.Commit();
             }
         }
     
     }
}
