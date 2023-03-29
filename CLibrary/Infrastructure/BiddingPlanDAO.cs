using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface BiddingPlanDAO
    {
        int SaveBiddingPlan(int bidId, int PlanId, DateTime startdate, DateTime endDate, string enteredUser, string EnteredDate, DBConnection dbConnection);
        List<BiddingPlan> GetBiddingPlanByID(int bidId, DBConnection dbConnection);
        int SavePalanfiles(List<BiddingPlanFileUpload> details, DBConnection dbConnection);
        List<BiddingPlanFileUpload> GetPalanfiles(int bidId, int planId, DBConnection dbConnection);
         int UpdateIsComplted(int bidId, int planId, int iscomplted, DateTime actualdate, DBConnection dbConnection);
        int UpdateBiddingplan(int bidId, int planId, DateTime startdate, DateTime enddate, int UserId, DBConnection dbConnection);
        List<BiddingPlan> GetBiddingPlanByIDPrint(List<int> bidId, DBConnection dbConnection);
    }

    public class BiddingPlanDAOImpl : BiddingPlanDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveBiddingPlan(int bidId, int PlanId, DateTime startdate, DateTime endDate, string enteredUser, string EnteredDate, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".BIDDING_PLAN (BID_ID, PLAN_ID, START_DATE, END_DATE, ENTERED_USER, ENTERED_DATE) VALUES ( " + bidId + ", " + PlanId + " , '" + startdate + "', '" + endDate + "', '" + enteredUser + "', '" + EnteredDate + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<BiddingPlan> GetBiddingPlanByID(int bidId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT BP.*,PPT.PLAN_NAME , PPT.WITH_TIME FROM  " + dbLibrary + ".BIDDING_PLAN BP "+
                                            " INNER JOIN " + dbLibrary + ".PROCUMENT_PLAN_TYPE PPT ON (BP.PLAN_ID=PPT.PLAN_ID)  WHERE BID_ID= " + bidId + " ;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr=dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<BiddingPlan>(dbConnection.dr);
            }
            
        }


        public List<BiddingPlan> GetBiddingPlanByIDPrint(List<int> bidId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            
            dbConnection.cmd.CommandText = "SELECT BP.*,PPT.PLAN_NAME , PPT.WITH_TIME, BID.BID_CODE FROM  " + dbLibrary + ".BIDDING_PLAN BP " +
                                            "INNER JOIN (SELECT BID_ID, BID_CODE FROM BIDDING) AS BID ON BID.BID_ID = BP.BID_ID " +
                                            " INNER JOIN " + dbLibrary + ".PROCUMENT_PLAN_TYPE PPT ON (BP.PLAN_ID=PPT.PLAN_ID)  WHERE BP.BID_ID IN ("+string.Join(",",bidId) +") ;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<BiddingPlan>(dbConnection.dr);
            }

        }

        public int SavePalanfiles(List<BiddingPlanFileUpload> details, DBConnection dbConnection)
        {
            int count = 0;
            foreach (var item in details)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".BIDDING_PLAN_FILE_UPLOAD (BID_ID, PLAN_ID, SEQ_ID, FILE_PATH, FILE_NAME) VALUES ( " + item.BidId + ", " + item.PlanId + " , " + item.sequenceId + ", '" + item.filepath + "', '" + item.filename + "');";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
               var Issaved= dbConnection.cmd.ExecuteNonQuery();
                if (Issaved>0)
                {
                    count = ++count;
                }
               
            }

            if (details.Count==count)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            
        }

        public List<BiddingPlanFileUpload> GetPalanfiles(int bidId, int planId, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".BIDDING_PLAN_FILE_UPLOAD WHERE BID_ID=" + bidId + " AND PLAN_ID=" + planId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<BiddingPlanFileUpload>(dbConnection.dr);
            }


        }

        public int  UpdateIsComplted(int bidId, int planId, int iscomplted,DateTime actualdate ,DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".BIDDING_PLAN SET IS_COMPLETED = " + iscomplted + ",ACTUAL_DATE='"+ actualdate + "' WHERE BID_ID=" + bidId + " AND PLAN_ID=" + planId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateBiddingplan(int bidId, int planId, DateTime startdate, DateTime enddate,int UserId, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".BIDDING_PLAN SET START_DATE = '" + startdate + "',END_DATE='" + enddate + "',UPDATED_USER="+ UserId + ",UPDATED_DATE='"+LocalTime.Now+"' WHERE BID_ID=" + bidId + " AND PLAN_ID=" + planId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }


    }
}
