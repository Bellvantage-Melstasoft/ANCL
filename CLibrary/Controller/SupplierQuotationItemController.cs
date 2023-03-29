using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface SupplierQuotationItemController
    {
        List<SupplierQuotationItem> GetAllQuotationItems(int QuotationId, int CompanyId);
        List<SupplierQuotationItem> GetAllQuotationItemsByBidItemId(int BidItemId, int CompanyId);
        SupplierQuotationItem GetSelectedQuotationItem(int QuotationId, int CompanyId);
        int SelectQuotationItem(int QuotationItemId, int UserId, string Remarks);
        int RejectQuotationItem(int QuotationItemId, int UserId, string Remarks);
        int GetCount(int biddingItemId);
        int CreateCoveringPrQuotations(int ParentPrId, decimal TotalVat, decimal TotalSubTotal, decimal TotalNetTotal, int UserId);
        List<SupplierQuotationItem> GetAllQuotationItemsForCoveringPR(int QuotationId);
    }
    class SupplierQuotationItemControllerImpl : SupplierQuotationItemController
    {
        public List<SupplierQuotationItem> GetAllQuotationItems(int QuotationId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationItemDAO DAO = DAOFactory.CreateSupplierQuotationItemDAO();
                return DAO.GetAllQuotationItems(QuotationId, CompanyId, dbConnection);
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

        public List<SupplierQuotationItem> GetAllQuotationItemsForCoveringPR(int QuotationId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierQuotationItemDAO DAO = DAOFactory.CreateSupplierQuotationItemDAO();
                return DAO.GetAllQuotationItemsForCoveringPR(QuotationId, dbConnection);
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

        public List<SupplierQuotationItem> GetAllQuotationItemsByBidItemId(int BidItemId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationItemDAO DAO = DAOFactory.CreateSupplierQuotationItemDAO();
                return DAO.GetAllQuotationItemsByBidItemId(BidItemId, CompanyId, dbConnection);
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

        public int GetCount(int biddingItemId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierQuotationItemDAO DAO = DAOFactory.CreateSupplierQuotationItemDAO();
                return DAO.GetCount(biddingItemId, dbConnection);
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

        public int CreateCoveringPrQuotations(int ParentPrId,decimal TotalVat, decimal TotalSubTotal, decimal TotalNetTotal, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierQuotationItemDAO DAO = DAOFactory.CreateSupplierQuotationItemDAO();
                return DAO.CreateCoveringPrQuotations(ParentPrId, TotalVat, TotalSubTotal, TotalNetTotal, UserId, dbConnection);
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

        public SupplierQuotationItem GetSelectedQuotationItem(int QuotationId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationItemDAO DAO = DAOFactory.CreateSupplierQuotationItemDAO();
                return DAO.GetSelectedQuotationItem(QuotationId, CompanyId, dbConnection);
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

        public int RejectQuotationItem(int QuotationItemId, int UserId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationItemDAO DAO = DAOFactory.CreateSupplierQuotationItemDAO();
                return DAO.RejectQuotationItem(QuotationItemId, UserId, Remarks, dbConnection);
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

        public int GetCount(int QuotationItemId, int UserId, string Remarks) {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierQuotationItemDAO DAO = DAOFactory.CreateSupplierQuotationItemDAO();
                return DAO.RejectQuotationItem(QuotationItemId, UserId, Remarks, dbConnection);
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

        public int SelectQuotationItem(int QuotationItemId, int UserId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationItemDAO DAO = DAOFactory.CreateSupplierQuotationItemDAO();
                return DAO.SelectQuotationItem(QuotationItemId, UserId, Remarks, dbConnection);
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
