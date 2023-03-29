using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface QuotationRecommendationController
    {
        List<QuotationRecommendation> GetQuotationRecommendations(int QuoationId);
        int RecommendQuotation(int CategoryId, int QuotationId, decimal Amount, int UserId, int DesignationId, string Remarks);
        int RejectAtRecommendation(int QuotationId, int UserId, int DesignationId, string Remarks);
        int OverrideAndRecommend(int RecommendationId, int CategoryId, int QuotationId, decimal Amount, int UserId, int DesignationId, string Remarks, int Status);
    }

    class QuotationRecommendationControllerImpl : QuotationRecommendationController
    {
        public List<QuotationRecommendation> GetQuotationRecommendations(int QuoationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                QuotationRecommendationDAO DAO = DAOFactory.CreateQuotationRecommendationDAO();
                return DAO.GetQuotationRecommendations(QuoationId, dbConnection);
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

        public int OverrideAndRecommend(int RecommendationId, int CategoryId, int QuotationId, decimal Amount, int UserId, int DesignationId, string Remarks, int Status)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                QuotationRecommendationDAO DAO = DAOFactory.CreateQuotationRecommendationDAO();
                return DAO.OverrideAndRecommend(RecommendationId, CategoryId, QuotationId, Amount, UserId, DesignationId, Remarks, Status, dbConnection);
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

        public int RecommendQuotation(int CategoryId, int QuotationId, decimal Amount, int UserId, int DesignationId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                QuotationRecommendationDAO DAO = DAOFactory.CreateQuotationRecommendationDAO();
                return DAO.RecommendQuotation(CategoryId, QuotationId, Amount, UserId, DesignationId, Remarks, dbConnection);
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

        public int RejectAtRecommendation(int QuotationId, int UserId, int DesignationId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                QuotationRecommendationDAO DAO = DAOFactory.CreateQuotationRecommendationDAO();
                return DAO.RejectAtRecommendation(QuotationId, UserId, DesignationId, Remarks, dbConnection);
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
