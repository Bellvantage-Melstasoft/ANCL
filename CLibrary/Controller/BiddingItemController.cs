using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface BiddingItemController
    {
        //New Methods By - created on 2019-01-17
        List<BiddingItem> GetAllBidItems(int BidId, int CompanyId);
        void GetLastPurchaseDetails(List<BiddingItem> items, int CompanyId);
        int TerminateBidItem(int BidItemId, int UserId, string Remarks);
        int TerminateBidItems(List<int> BidItemIds, int UserId, string Remarks);
        List<BiddingItem> GetBiddingItems(List<int> BidIds);
        List<BiddingItem> GetBiddingItemsList(int BidIds);
        List<BiddingItem> FechBiddingItems(int BidId);
    }

    class BiddingItemControllerImpl : BiddingItemController
    {
        public List<BiddingItem> GetAllBidItems(int BidId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingItemDAO DAO = DAOFactory.CreateBiddingItemDAO();
                return DAO.GetAllBidItems(BidId, CompanyId, dbConnection);
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

        public List<BiddingItem> GetBiddingItems(List<int> BidIds) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingItemDAO DAO = DAOFactory.CreateBiddingItemDAO();
                return DAO.GetBiddingItems(BidIds, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();

                }
            }
        }


        public List<BiddingItem> FechBiddingItems(int BidId) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingItemDAO DAO = DAOFactory.CreateBiddingItemDAO();
                return DAO.FechBiddingItems(BidId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();

                }
            }
        }

        public List<BiddingItem> GetBiddingItemsList(int BidIds) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingItemDAO DAO = DAOFactory.CreateBiddingItemDAO();
                return DAO.GetBiddingItemsList(BidIds, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();

                }
            }
        }
        public void GetLastPurchaseDetails(List<BiddingItem> items, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingItemDAO DAO = DAOFactory.CreateBiddingItemDAO();
                DAO.GetLastPurchaseDetails(items, CompanyId, dbConnection);
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

        public int TerminateBidItem(int BidItemId, int UserId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingItemDAO DAO = DAOFactory.CreateBiddingItemDAO();
                return DAO.TerminateBidItem(BidItemId, UserId, Remarks, dbConnection);
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

        public int TerminateBidItems(List<int> BidItemIds, int UserId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingItemDAO DAO = DAOFactory.CreateBiddingItemDAO();
                return DAO.TerminateBidItems(BidItemIds, UserId, Remarks, dbConnection);
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
