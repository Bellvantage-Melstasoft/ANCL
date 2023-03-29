using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
   public interface BiddingMethodDAO
    {

       List<BiddingMethod> GetBiddingMethodList(DBConnection dbConnection);
    }
   public class BiddingMethodDAOImpl : BiddingMethodDAO
   {
            string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
        //    public List<BiddingMethod> GetBiddingMethodList(DBConnection dbConnection)
        //    {
        //        dbConnection.cmd.Parameters.Clear();

        //        dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".BIDDING_METHOD ";
        //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

        //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
        //        {
        //            DataAccessObject dataAccessObject = new DataAccessObject();
        //            return dataAccessObject.ReadCollection<BiddingMethod>(dbConnection.dr);
        //        }
        //    }


       
        public List<BiddingMethod> GetBiddingMethodList(DBConnection dbConnection) {
            
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".BIDDING_METHOD ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return  dataAccessObject.ReadCollection<BiddingMethod>(dbConnection.dr);
            }

            
        }
    }


}
