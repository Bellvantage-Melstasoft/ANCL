using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Controller
{
     public interface BiddingPlanController
    {
         int SaveBiddingPlan(int bidId, int PlanId, DateTime startdate, DateTime endDate, string enteredUser, string EnteredDate);
        List<BiddingPlan> GetBiddingPlanByID(int bidId);
        int SavePalanfiles(List<BiddingPlanFileUpload> details);
        List<BiddingPlanFileUpload> GetPalanfiles(int bidId, int planId);
        int UpdateIsComplted(int bidId, int planId, int iscomplted, DateTime actualdate);
        int UpdateBiddingplan(int bidId, int planId, DateTime startdate, DateTime enddate, int UserId);
        List<BiddingPlan> GetBiddingPlanByIDPrint(List<int> bidId);
    }
     public class BiddingPlanControllerImpl : BiddingPlanController
     {
        public List<BiddingPlan> GetBiddingPlanByID(int bidId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingPlanDAO biddingPlanDAO = DAOFactory.createBiddingPlanDAO();
                return biddingPlanDAO.GetBiddingPlanByID(bidId, dbConnection);
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
        
            public List<BiddingPlan> GetBiddingPlanByIDPrint(List<int> bidId) {
                DBConnection dbConnection = new DBConnection();
                try {
                    BiddingPlanDAO biddingPlanDAO = DAOFactory.createBiddingPlanDAO();
                    return biddingPlanDAO.GetBiddingPlanByIDPrint(bidId, dbConnection);
                }
                catch (Exception ex) {
                    dbConnection.RollBack();
                    throw;
                }
                finally {
                    if (dbConnection.con.State == System.Data.ConnectionState.Open)
                        dbConnection.Commit();
                }
            }



            public int SaveBiddingPlan(int bidId, int PlanId, DateTime startdate, DateTime endDate, string enteredUser, string EnteredDate)
         {
             DBConnection dbConnection = new DBConnection();
             try
             {
                 BiddingPlanDAO biddingPlanDAO = DAOFactory.createBiddingPlanDAO();
                 return biddingPlanDAO.SaveBiddingPlan(bidId, PlanId, startdate,endDate, enteredUser, EnteredDate, dbConnection);
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

        public int SavePalanfiles(List<BiddingPlanFileUpload> details)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingPlanDAO biddingPlanDAO = DAOFactory.createBiddingPlanDAO();
                return biddingPlanDAO.SavePalanfiles(details, dbConnection);
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

        public List<BiddingPlanFileUpload> GetPalanfiles(int bidId, int planId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingPlanDAO biddingPlanDAO = DAOFactory.createBiddingPlanDAO();
                return biddingPlanDAO.GetPalanfiles(bidId, planId, dbConnection);
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

        public int UpdateIsComplted(int bidId, int planId, int iscomplted,DateTime actualdate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingPlanDAO biddingPlanDAO = DAOFactory.createBiddingPlanDAO();
                return biddingPlanDAO.UpdateIsComplted(bidId, planId, iscomplted,  actualdate, dbConnection);
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

        public int UpdateBiddingplan(int bidId, int planId, DateTime startdate, DateTime enddate, int UsecrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingPlanDAO biddingPlanDAO = DAOFactory.createBiddingPlanDAO();
                return biddingPlanDAO.UpdateBiddingplan(bidId, planId, startdate, enddate, UsecrId, dbConnection);
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
