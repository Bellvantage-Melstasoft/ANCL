using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface TabulationRecommendationController
    {
        List<TabulationRecommendation> GetTabulationRecommendations(int TabulationId);
        int RecommendTabulation(int CategoryId, int TabulationId, decimal Amount, int UserId, int DesignationId, string Remarks, int PurchaseType);
        int RejectAtRecommendation(int TabulationId, int UserId, int DesignationId, string Remarks);
        int OverrideAndRecommend(int RecommendationId, int CategoryId, int TabulationId, decimal Amount, int UserId, int DesignationId, string Remarks, int Status);
        List<TabulationRecommendation> tabulationIdListForPurchadeRequisitionReport(int TabulationId);
        int HoldRecommendation(int TabulationId,  int recommendationId, string Remarks, int UserId);
    }

    class TabulationRecommendationControllerImpl : TabulationRecommendationController
    {
        public List<TabulationRecommendation> GetTabulationRecommendations(int TabulationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationRecommendationDAO DAO = DAOFactory.CreateTabulationRecommendationDAO();
                return DAO.GetTabulationRecommendations(TabulationId, dbConnection);
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
        public List<TabulationRecommendation> tabulationIdListForPurchadeRequisitionReport(int TabulationId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationRecommendationDAO DAO = DAOFactory.CreateTabulationRecommendationDAO();
                return DAO.tabulationIdListForPurchadeRequisitionReport(TabulationId, dbConnection);
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
        public int OverrideAndRecommend(int RecommendationId, int CategoryId, int TabulationId, decimal Amount, int UserId, int DesignationId, string Remarks, int Status)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationRecommendationDAO DAO = DAOFactory.CreateTabulationRecommendationDAO();
                return DAO.OverrideAndRecommend(RecommendationId, CategoryId, TabulationId, Amount, UserId, DesignationId, Remarks, Status, dbConnection);
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

        public int RecommendTabulation(int CategoryId, int TabulationId, decimal Amount, int UserId, int DesignationId, string Remarks, int PurchaseType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationRecommendationDAO DAO = DAOFactory.CreateTabulationRecommendationDAO();
                return DAO.RecommendTabulation(CategoryId, TabulationId, Amount, UserId, DesignationId, Remarks, PurchaseType, dbConnection);
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

        public int RejectAtRecommendation(int TabulationId, int UserId, int DesignationId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationRecommendationDAO DAO = DAOFactory.CreateTabulationRecommendationDAO();
                return DAO.RejectAtRecommendation(TabulationId, UserId, DesignationId, Remarks, dbConnection);
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

        public int HoldRecommendation(int TabulationId, int recommendationId, string Remarks, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationRecommendationDAO DAO = DAOFactory.CreateTabulationRecommendationDAO();
                return DAO.HoldRecommendation(TabulationId, recommendationId, Remarks, UserId, dbConnection);
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
