using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface TabulationApprovalDAO
    {
        List<TabulationApproval> GetTabulationApprovals(int TabulationId, DBConnection dbConnection);
        int ApproveTabulation(int TabulationId, int UserId, int DesignationId, string Remarks, DBConnection dBConnection);
        int RejectAtApproval(int TabulationId, int UserId, int DesignationId, string Remarks, DBConnection dBConnection);
        int OverrideAndApprove(int ApprovalId, int TabulationId, int UserId, int DesignationId, string Remarks, int Status, DBConnection dBConnection);
        List<TabulationApproval> tabulationIdList(int CategoryId, decimal minValue, decimal maxValue, DBConnection dBConnection);
        //int UpdateApprovalLimitForApproval(int CategoryId, int UserId, int DesignationId, decimal minValue, decimal maxValue, DBConnection dBConnection);
        int DeleteTabulations(int tabulationId, DBConnection dBConnection);
        int TabulationApprovalOverride(int TabulationId, int UserId, int DesignationId, string Remarks, int Status, DBConnection dBConnection);
        List<TabulationApproval> tabulationIdListForPurchadeRequisitionReportApp(int TabulationId, DBConnection dBConnection);
        int HoldRecommendation(int tabulationId, int ApprovalId, string remark, int UserId, DBConnection dBConnection);
    }

    class TabulationApprovalDAOImpl : TabulationApprovalDAO
    {
        public int ApproveTabulation(int TabulationId, int UserId, int DesignationId, string Remarks, DBConnection dBConnection)
        {

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT IS_APPROVED FROM TABULATION_MASTER WHERE TABULATION_ID =" + TabulationId + "  ";
            decimal IsApproved = decimal.Parse(dBConnection.cmd.ExecuteScalar().ToString());

            if (IsApproved == 1) {
                dBConnection.cmd.CommandText = "UPDATE TABULATION_APPROVAL SET IS_APPROVED=1, APPROVED_BY= "+ UserId + ", APPROVED_DATE = '"+LocalTime.Now+"' ,WAS_OVERIDDEN=0, REMARKS = '"+ Remarks + "' " +
                    "WHERE APPROVAL_ID= (SELECT TOP 1 APPROVAL_ID FROM TABULATION_APPROVAL WHERE TABULATION_ID= "+ TabulationId + " AND DESIGNATION_ID="+ DesignationId + " AND IS_APPROVED=0 ORDER BY [SEQUENCE] ASC)";
                    }
            else {

                dBConnection.cmd.CommandText = "[APPROVE_TABULATION]";
                dBConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", TabulationId);
                dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
                dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
                dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);

                dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            }
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<TabulationApproval> GetTabulationApprovals(int TabulationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM [dbo].[TABULATION_APPROVAL] TA\n" +
                                            "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME FROM DESIGNATION) AS D ON TA.DESIGNATION_ID = D.DESIGNATION_ID\n" +
                                            "LEFT JOIN(SELECT USER_ID, FIRST_NAME AS APPROVED_BY_NAME FROM COMPANY_LOGIN) AS CL ON TA.APPROVED_BY = CL.USER_ID\n" +
                                            "WHERE TA.TABULATION_ID = " + TabulationId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationApproval>(dbConnection.dr);
            }
        }

        public int OverrideAndApprove(int ApprovalId, int TabulationId, int UserId, int DesignationId, string Remarks, int Status, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[OVERRIDE_TABULATION_APPROVAL_DETAILS]";
            dBConnection.cmd.Parameters.AddWithValue("@APPROVAL_ID", ApprovalId);
            dBConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", TabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);
            dBConnection.cmd.Parameters.AddWithValue("@STATUS", Status);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }
        public int TabulationApprovalOverride(int TabulationId, int UserId, int DesignationId, string Remarks, int Status, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[OVERRIDE_TABULATION_APPROVAL_MASTER]";
            dBConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", TabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);
            dBConnection.cmd.Parameters.AddWithValue("@STATUS", Status);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int RejectAtApproval(int TabulationId, int UserId, int DesignationId, string Remarks, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[REJECT_TABULATION_AT_APPROVAL]";
            dBConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", TabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }
        public int DeleteTabulations(int tabulationId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "DELETE FROM TABULATION_APPROVAL WHERE IS_APPROVED != 1 AND TABULATION_ID = " + tabulationId + " ";

            return dBConnection.cmd.ExecuteNonQuery();
        }
        public List<TabulationApproval> tabulationIdList(int CategoryId, decimal minValue, decimal maxValue, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            // dBConnection.cmd.CommandText = "SELECT DISTINCT(TABULATION_ID) FROM TABULATION_APPROVAL WHERE IS_APPROVED != 1 AND TABULATION_ID IN (SELECT TABULATION_ID FROM TABULATION_MASTER WHERE PR_ID IN (SELECT PR_ID FROM PR_MASTER WHERE PR_CATEGORY_ID = (" + CategoryId + ")))";
            dBConnection.cmd.CommandText = "SELECT TABULATION_ID FROM TABULATION_MASTER AS TM "+
                                            "INNER JOIN(SELECT PR_ID, PR_CATEGORY_ID FROM PR_MASTER) AS PM ON PM.PR_ID = TM.PR_ID "+
                                            "WHERE PR_CATEGORY_ID = "+ CategoryId + " AND IS_RECOMMENDED = 1 AND IS_APPROVED = 0 AND NET_TOTAL >= "+ minValue + " AND NET_TOTAL <="+maxValue+" ";

            //dBConnection.cmd.CommandText += "DELETE FROM TABULATION_APPROVAL WHERE IS_APPROVED != 1 AND TABULATION_ID IN(SELECT TABULATION_ID FROM TABULATION_MASTER WHERE PR_ID IN(SELECT PR_ID FROM PR_MASTER WHERE PR_CATEGORY_ID = (" + CategoryId + "))AND NET_TOTAL >= " + minValue + " AND NET_TOTAL <=" + maxValue + ")";

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationApproval>(dBConnection.dr);
            }
        }
        public int HoldRecommendation(int tabulationId,int ApprovalId, string remark,int UserId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "UPDATE TABULATION_APPROVAL SET REMARKS = '" + remark + "', IS_APPROVED = 3, APPROVED_BY = "+UserId+", APPROVED_DATE = '"+LocalTime.Now+"' WHERE TABULATION_ID = " + tabulationId + " AND APPROVAL_ID = " + ApprovalId + "  ";

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<TabulationApproval> tabulationIdListForPurchadeRequisitionReportApp(int TabulationId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM TABULATION_APPROVAL AS TR  " +
                                           "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVED_BY_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = TR.APPROVED_BY " +
                                           "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME FROM DESIGNATION) AS D ON D.DESIGNATION_ID = TR.DESIGNATION_ID " +
                                           "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME AS OVERIDING_DESIGNATION_NAME FROM DESIGNATION) AS OD ON OD.DESIGNATION_ID = TR.OVERIDING_DESIGNATION " +
                                           "WHERE TR.TABULATION_ID = " + TabulationId + " ";

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationApproval>(dBConnection.dr);
            }
        }

        //public int UpdateApprovalLimitForApproval(int CategoryId, int UserId, int DesignationId, decimal minValue, decimal maxValue, DBConnection dBConnection) {
        //    dBConnection.cmd.Parameters.Clear();

        //    List<TabulationApproval> tabulationIdList = DAOFactory.CreateTabulationApprovalDAO().tabulationIdList(CategoryId, minValue, maxValue, dBConnection);

        //    int result = 0;


        //    for (int i = 0; i < tabulationIdList.Count; i++) {
        //        TabulationMaster tabulationMaster = DAOFactory.CreateTabulationMasterDAO().GetTabulationsByTabulationId(tabulationIdList[i].TabulationId, dBConnection);
        //        result =DAOFactory.CreateTabulationMasterDAO().PopulateApproval(tabulationMaster.TabulationId, CategoryId, tabulationMaster.NetTotal, UserId, DesignationId, tabulationMaster.SelectionRemarks, dBConnection);

        //        if(result <= 0) {
        //            dBConnection.RollBack();
        //            return -1;
        //        }

        //    }

        //    return result;


        // }
    }
}
