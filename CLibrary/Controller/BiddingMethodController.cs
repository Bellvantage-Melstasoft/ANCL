using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
   public interface BiddingMethodController
    {
       List<BiddingMethod> GetBiddingMethodList();
    }

   public class BiddingMethodControllerImpl : BiddingMethodController
   {

       public List<BiddingMethod> GetBiddingMethodList()
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               BiddingMethodDAO biddingMethodDAO = DAOFactory.CreateBiddingMethodDAO();
               return biddingMethodDAO.GetBiddingMethodList(dbConnection);
           }
           catch (Exception)
           {
               dbConnection.RollBack();
               throw;
           }
           finally
           {
               if (dbConnection.con.State == System.Data.ConnectionState.Open)
               {
                   dbConnection.Commit();
               }
           }
       }

   }
}
