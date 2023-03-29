using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;

namespace CLibrary.Controller
{
    public interface PR_DetailHistoryController
    {
        int SavePRHistoryDetails(int PrId, int ItemId, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, decimal ItemQuantity);

        List<PR_Details> FetchPRHistoryDetailsByDeptIdAndPrId(int PrId);
        int DeletePrHistoryDetailByPrIDAndItemId(int prID, int ItemId);
        int UpdateItemQuantityFromBidSubmitting(int prID, int ItemId, decimal itemQuantity);
    }

    public class PR_DetailHistoryControllerImpl : PR_DetailHistoryController
    {
        public int DeletePrHistoryDetailByPrIDAndItemId(int prID, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailHistoryDAO pR_DetailHistoryDAO = DAOFactory.CreatePR_DetailHistoryDAO();
                return pR_DetailHistoryDAO.DeletePrHistoryDetailByPrIDAndItemId( prID,  ItemId, dbConnection);
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

        public List<PR_Details> FetchPRHistoryDetailsByDeptIdAndPrId(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailHistoryDAO pR_DetailHistoryDAO = DAOFactory.CreatePR_DetailHistoryDAO();
                return pR_DetailHistoryDAO.FetchPRHistoryDetailsByDeptIdAndPrId( PrId, dbConnection);
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

        public int SavePRHistoryDetails(int PrId, int ItemId, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, decimal ItemQuantity)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailHistoryDAO pR_DetailHistoryDAO = DAOFactory.CreatePR_DetailHistoryDAO();
                return pR_DetailHistoryDAO.SavePRHistoryDetails( PrId,  ItemId,  ItemUpdatedBy,  ItemUpdatedDateTime,  IsActive,  ItemQuantity, dbConnection);
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

        public int UpdateItemQuantityFromBidSubmitting(int prID, int ItemId, decimal itemQuantity)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailHistoryDAO pR_DetailHistoryDAO = DAOFactory.CreatePR_DetailHistoryDAO();
                return pR_DetailHistoryDAO.UpdateItemQuantityFromBidSubmitting( prID,  ItemId,  itemQuantity, dbConnection);
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
