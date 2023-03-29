using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface TabulationRecommendationDAO
    {
        List<TabulationRecommendation> GetTabulationRecommendations(int TabulationId, DBConnection dbConnection);
        int RecommendTabulation(int CategoryId, int TabulationId, decimal Amount, int UserId, int DesignationId, string Remarks, int PurchaseType, DBConnection dBConnection);
        int RejectAtRecommendation (int TabulationId, int UserId, int DesignationId, string Remarks, DBConnection dBConnection);
        int OverrideAndRecommend(int RecommendationId, int CategoryId, int TabulationId, decimal Amount, int UserId, int DesignationId, string Remarks,int Status, DBConnection dBConnection);
        List<TabulationRecommendation> tabulationIdList(int CategoryId, decimal minValue, decimal maxValue, DBConnection dBConnection);
       // int UpdateApprovalLimitForRecommandation(int CategoryId, int UserId, int DesignationId, decimal minValue, decimal maxValue, DBConnection dBConnection);
        int DeleteTabulations(int tabulationId, DBConnection dBConnection);
        List<TabulationRecommendation> tabulationIdListForPurchadeRequisitionReport(int TabulationId, DBConnection dBConnection);
        int HoldRecommendation(int TabulationId, int reccommendationId, string Remarks, int UserId, DBConnection dBConnection);
    }

    class TabulationRecommendationDAOImpl : TabulationRecommendationDAO
    {
        public List<TabulationRecommendation> GetTabulationRecommendations(int TabulationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM [dbo].[TABULATION_RECOMMENDATION] TR\n" +
                                            "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME FROM DESIGNATION) AS D ON TR.DESIGNATION_ID = D.DESIGNATION_ID\n" +
                                            "LEFT JOIN(SELECT USER_ID, FIRST_NAME AS RECOMMENDED_BY_NAME FROM COMPANY_LOGIN) AS CL ON TR.RECOMMENDED_BY = CL.USER_ID\n" +
                                            "WHERE TR.TABULATION_ID = " + TabulationId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationRecommendation>(dbConnection.dr);
            }
        }

        public int OverrideAndRecommend(int RecommendationId,int CategoryId, int TabulationId, decimal Amount, int UserId, int DesignationId, string Remarks, int Status, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[OVERRIDE_TABULATION_RECOMMENDATION_DETAILS]";
            dBConnection.cmd.Parameters.AddWithValue("@RECOMMENDATION_ID", RecommendationId);
            dBConnection.cmd.Parameters.AddWithValue("@CATEGORY_ID", CategoryId);
            dBConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", TabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@AMOUNT", Amount);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);
            dBConnection.cmd.Parameters.AddWithValue("@STATUS", Status);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int RecommendTabulation(int CategoryId, int TabulationId, decimal Amount, int UserId, int DesignationId, string Remarks, int PurchaseType, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT IS_RECOMMENDED FROM TABULATION_MASTER WHERE TABULATION_ID =" + TabulationId + "  ";
            decimal IsRecommended = decimal.Parse(dBConnection.cmd.ExecuteScalar().ToString());

            if (IsRecommended == 1) {
                dBConnection.cmd.CommandText = " UPDATE TABULATION_RECOMMENDATION SET IS_RECOMMENDED=1, RECOMMENDED_BY= " + UserId + ", RECOMMENDED_DATE = '" + LocalTime.Now + "',WAS_OVERIDDEN=0,REMARKS = '" + Remarks + "' " +
                                               "WHERE RECOMMENDATION_ID= (SELECT TOP 1 RECOMMENDATION_ID FROM TABULATION_RECOMMENDATION WHERE TABULATION_ID="+ TabulationId + " AND DESIGNATION_ID="+ DesignationId + " AND IS_RECOMMENDED=0 ORDER BY [SEQUENCE] ASC)";
            }
            else {
                dBConnection.cmd.CommandText = "[RECOMMEND_TABULATION]";
                dBConnection.cmd.Parameters.AddWithValue("@CATEGORY_ID", CategoryId);
                dBConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", TabulationId);
                dBConnection.cmd.Parameters.AddWithValue("@AMOUNT", Amount);
                dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
                dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
                dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);
                dBConnection.cmd.Parameters.AddWithValue("@PURCHASE_TYPE", PurchaseType);

                dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            }
                return dBConnection.cmd.ExecuteNonQuery();
            
        }
        public int HoldRecommendation(int TabulationId, int reccommendationId, string Remarks, int UserId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "UPDATE TABULATION_RECOMMENDATION SET REMARKS = '"+ Remarks + "', IS_RECOMMENDED = 3, RECOMMENDED_BY = "+ UserId + ", RECOMMENDED_DATE = '"+LocalTime.Now+"' WHERE TABULATION_ID = "+ TabulationId + " AND RECOMMENDATION_ID = "+ reccommendationId + "  ";

            return dBConnection.cmd.ExecuteNonQuery();
        }
        public int RejectAtRecommendation(int TabulationId, int UserId, int DesignationId, string Remarks, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[REJECT_TABULATION_AT_RECOMMENDATION]";
            dBConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", TabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<TabulationRecommendation> tabulationIdList(int CategoryId, decimal minValue, decimal maxValue, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            //dBConnection.cmd.CommandText = "SELECT DISTINCT(TABULATION_ID) FROM TABULATION_RECOMMENDATION WHERE IS_RECOMMENDED != 1 AND TABULATION_ID IN (SELECT TABULATION_ID FROM TABULATION_MASTER WHERE PR_ID IN (SELECT PR_ID FROM PR_MASTER WHERE PR_CATEGORY_ID = (" + CategoryId + ")))";

            dBConnection.cmd.CommandText = "SELECT TABULATION_ID FROM TABULATION_MASTER AS TM " +
                                           "INNER JOIN(SELECT PR_ID, PR_CATEGORY_ID FROM PR_MASTER) AS PM ON PM.PR_ID = TM.PR_ID " +
                                           "WHERE PR_CATEGORY_ID = " + CategoryId + " AND IS_RECOMMENDED != 1 AND NET_TOTAL >= " + minValue + " AND NET_TOTAL <=" + maxValue + " ";

            //dBConnection.cmd.CommandText += "DELETE FROM TABULATION_RECOMMENDATION WHERE IS_RECOMMENDED != 1 AND TABULATION_ID IN(SELECT TABULATION_ID FROM TABULATION_MASTER WHERE PR_ID IN(SELECT PR_ID FROM PR_MASTER WHERE PR_CATEGORY_ID = (" + CategoryId + "))NET_TOTAL >= " + minValue + " AND NET_TOTAL <=" + maxValue + " )";

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationRecommendation>(dBConnection.dr);
            }
        }
        public int DeleteTabulations(int tabulationId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "DELETE FROM TABULATION_RECOMMENDATION WHERE IS_RECOMMENDED != 1 AND TABULATION_ID = " + tabulationId + " ";

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<TabulationRecommendation> tabulationIdListForPurchadeRequisitionReport(int TabulationId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM TABULATION_RECOMMENDATION AS TR  " +
                                           "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS RECOMMENDED_BY_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = TR.RECOMMENDED_BY " +
                                           "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME FROM DESIGNATION) AS D ON D.DESIGNATION_ID = TR.DESIGNATION_ID " +
                                           "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME AS OVERIDING_DESIGNATION_NAME FROM DESIGNATION) AS OD ON OD.DESIGNATION_ID = TR.OVERIDING_DESIGNATION " +
                                           "WHERE TR.TABULATION_ID = " + TabulationId + " ";
            
            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationRecommendation>(dBConnection.dr);
            }
        }
        //public int UpdateApprovalLimitForRecommandation( int CategoryId, int UserId, int DesignationId, decimal minValue, decimal maxValue, DBConnection dBConnection) {
        //    dBConnection.cmd.Parameters.Clear();

        //    List<TabulationRecommendation> tabulationIdList = DAOFactory.CreateTabulationRecommendationDAO().tabulationIdList(CategoryId, minValue, maxValue, dBConnection);
        //    int result = 0;

        //    for (int i = 0; i < tabulationIdList.Count; i++) {
        //        TabulationMaster tabulationMaster = DAOFactory.CreateTabulationMasterDAO().GetTabulationsByTabulationId(tabulationIdList[i].TabulationId, dBConnection);
        //        result = DAOFactory.CreateTabulationMasterDAO().PopulateRecommendation(tabulationMaster.TabulationId, CategoryId, tabulationMaster.NetTotal, UserId, DesignationId, tabulationMaster.SelectionRemarks, dBConnection);
        //        if (result <= 0) {
        //            dBConnection.RollBack();
        //            return -1;
        //        }
        //    }

        //    return result;
        //}
    }
}
