using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface TabulationApprovalController
    {
        List<TabulationApproval> GetTabulationApprovals(int TabulationId);
        int ApproveTabulation(int TabulationId, int UserId, int DesignationId, string Remarks);
        int RejectAtApproval(int TabulationId, int UserId, int DesignationId, string Remarks);
        int OverrideAndApprove(int ApprovalId, int TabulationId, int UserId, int DesignationId, string Remarks, int Status);
        int TabulationApprovalOverride(int TabulationId, int UserId, int DesignationId, string Remarks, int Status);
        List<TabulationApproval> tabulationIdListForPurchadeRequisitionReportApp(int TabulationId);
        int HoldRecommendation(int TabulationId, int ApprovalId, string Remarks, int UserId);
    }

    class TabulationApprovalControllerImpl : TabulationApprovalController
    {
        public int ApproveTabulation(int TabulationId, int UserId, int DesignationId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationApprovalDAO DAO = DAOFactory.CreateTabulationApprovalDAO();
                return DAO.ApproveTabulation(TabulationId, UserId, DesignationId, Remarks, dbConnection);
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

        public List<TabulationApproval> GetTabulationApprovals(int TabulationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationApprovalDAO DAO = DAOFactory.CreateTabulationApprovalDAO();
                return DAO.GetTabulationApprovals(TabulationId, dbConnection);
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
        public List<TabulationApproval> tabulationIdListForPurchadeRequisitionReportApp(int TabulationId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationApprovalDAO DAO = DAOFactory.CreateTabulationApprovalDAO();
                return DAO.tabulationIdListForPurchadeRequisitionReportApp(TabulationId, dbConnection);
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
        public int OverrideAndApprove(int ApprovalId, int TabulationId, int UserId, int DesignationId, string Remarks, int Status)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationApprovalDAO DAO = DAOFactory.CreateTabulationApprovalDAO();
                return DAO.OverrideAndApprove(ApprovalId, TabulationId, UserId, DesignationId, Remarks, Status, dbConnection);
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
        public int TabulationApprovalOverride( int TabulationId, int UserId, int DesignationId, string Remarks, int Status) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationApprovalDAO DAO = DAOFactory.CreateTabulationApprovalDAO();
                return DAO.TabulationApprovalOverride( TabulationId, UserId, DesignationId, Remarks, Status, dbConnection);
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
        public int RejectAtApproval(int TabulationId, int UserId, int DesignationId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationApprovalDAO DAO = DAOFactory.CreateTabulationApprovalDAO();
                return DAO.RejectAtApproval(TabulationId, UserId, DesignationId, Remarks, dbConnection);
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

        public int HoldRecommendation(int TabulationId, int ApprovalId, string Remarks, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationApprovalDAO DAO = DAOFactory.CreateTabulationApprovalDAO();
                return DAO.HoldRecommendation(TabulationId, ApprovalId, Remarks, UserId, dbConnection);
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
    }
}
