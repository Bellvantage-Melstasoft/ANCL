using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface QuotationRecommendationDAO
    {
        List<QuotationRecommendation> GetQuotationRecommendations(int QuoationId, DBConnection dbConnection);
        int RecommendQuotation(int CategoryId, int QuotationId, decimal Amount, int UserId, int DesignationId, string Remarks, DBConnection dBConnection);
        int RejectAtRecommendation (int QuotationId, int UserId, int DesignationId, string Remarks, DBConnection dBConnection);
        int OverrideAndRecommend(int RecommendationId, int CategoryId, int QuotationId, decimal Amount, int UserId, int DesignationId, string Remarks,int Status, DBConnection dBConnection);
    }

    class QuotationRecommendationDAOImpl : QuotationRecommendationDAO
    {
        public List<QuotationRecommendation> GetQuotationRecommendations(int QuoationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM [dbo].[QUOTATION_RECOMMENDATION] QR\n" +
                                            "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME FROM DESIGNATION) AS D ON QR.DESIGNATION_ID = D.DESIGNATION_ID\n" +
                                            "LEFT JOIN(SELECT USER_ID, FIRST_NAME AS RECOMMENDED_BY_NAME FROM COMPANY_LOGIN) AS CL ON QR.RECOMMENDED_BY = CL.USER_ID\n" +
                                            "WHERE QR.QUOTATION_ID = " + QuoationId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<QuotationRecommendation>(dbConnection.dr);
            }
        }

        public int OverrideAndRecommend(int RecommendationId,int CategoryId, int QuotationId, decimal Amount, int UserId, int DesignationId, string Remarks, int Status, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[OVERRIDE_QUOTATION_RECOMMENDATION_DETAILS]";
            dBConnection.cmd.Parameters.AddWithValue("@RECOMMENDATION_ID", RecommendationId);
            dBConnection.cmd.Parameters.AddWithValue("@CATEGORY_ID", CategoryId);
            dBConnection.cmd.Parameters.AddWithValue("@QUOTATION_ID", QuotationId);
            dBConnection.cmd.Parameters.AddWithValue("@AMOUNT", Amount);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);
            dBConnection.cmd.Parameters.AddWithValue("@STATUS", Status);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int RecommendQuotation(int CategoryId, int QuotationId, decimal Amount, int UserId, int DesignationId, string Remarks, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[RECOMMEND_QUOTATION]";
            dBConnection.cmd.Parameters.AddWithValue("@CATEGORY_ID", CategoryId);
            dBConnection.cmd.Parameters.AddWithValue("@QUOTATION_ID", QuotationId);
            dBConnection.cmd.Parameters.AddWithValue("@AMOUNT", Amount);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int RejectAtRecommendation(int QuotationId, int UserId, int DesignationId, string Remarks, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[REJECT_QUOTATION_AT_RECOMMENDATION]";
            dBConnection.cmd.Parameters.AddWithValue("@QUOTATION_ID", QuotationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }
    }
}
