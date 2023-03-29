using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface QuotationApprovalController
    {
        List<QuotationApproval> GetQuotationApprovals(int QuoationId);
        int ApproveQuotation(int QuotationId, int UserId, int DesignationId, string Remarks);
        int RejectAtApproval(int QuotationId, int UserId, int DesignationId, string Remarks);
        int OverrideAndApprove(int ApprovalId, int QuotationId, int UserId, int DesignationId, string Remarks, int Status);
    }

    class QuotationApprovalControllerImpl : QuotationApprovalController
    {
        public int ApproveQuotation(int QuotationId, int UserId, int DesignationId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                QuotationApprovalDAO DAO = DAOFactory.CreateQuotationApprovalDAO();
                return DAO.ApproveQuotation(QuotationId, UserId, DesignationId, Remarks, dbConnection);
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

        public List<QuotationApproval> GetQuotationApprovals(int QuoationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                QuotationApprovalDAO DAO = DAOFactory.CreateQuotationApprovalDAO();
                return DAO.GetQuotationApprovals(QuoationId, dbConnection);
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

        public int OverrideAndApprove(int ApprovalId, int QuotationId, int UserId, int DesignationId, string Remarks, int Status)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                QuotationApprovalDAO DAO = DAOFactory.CreateQuotationApprovalDAO();
                return DAO.OverrideAndApprove(ApprovalId, QuotationId, UserId, DesignationId, Remarks, Status, dbConnection);
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

        public int RejectAtApproval(int QuotationId, int UserId, int DesignationId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                QuotationApprovalDAO DAO = DAOFactory.CreateQuotationApprovalDAO();
                return DAO.RejectAtApproval(QuotationId, UserId, DesignationId, Remarks, dbConnection);
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
