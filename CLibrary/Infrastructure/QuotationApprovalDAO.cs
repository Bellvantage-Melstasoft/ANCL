using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface QuotationApprovalDAO
    {
        List<QuotationApproval> GetQuotationApprovals(int QuoationId, DBConnection dbConnection);
        int ApproveQuotation(int QuotationId, int UserId, int DesignationId, string Remarks, DBConnection dBConnection);
        int RejectAtApproval(int QuotationId, int UserId, int DesignationId, string Remarks, DBConnection dBConnection);
        int OverrideAndApprove(int ApprovalId, int QuotationId, int UserId, int DesignationId, string Remarks, int Status, DBConnection dBConnection);
    }

    class QuotationApprovalDAOImpl : QuotationApprovalDAO
    {
        public int ApproveQuotation(int QuotationId, int UserId, int DesignationId, string Remarks, DBConnection dBConnection)
        {

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[APPROVE_QUOTATION]";
            dBConnection.cmd.Parameters.AddWithValue("@QUOTATION_ID", QuotationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<QuotationApproval> GetQuotationApprovals(int QuoationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM [dbo].[QUOTATION_APPROVAL] QA\n" +
                                            "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME FROM DESIGNATION) AS D ON QA.DESIGNATION_ID = D.DESIGNATION_ID\n" +
                                            "LEFT JOIN(SELECT USER_ID, FIRST_NAME AS APPROVED_BY_NAME FROM COMPANY_LOGIN) AS CL ON QA.APPROVED_BY = CL.USER_ID\n" +
                                            "WHERE QA.QUOTATION_ID = " + QuoationId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<QuotationApproval>(dbConnection.dr);
            }
        }

        public int OverrideAndApprove(int ApprovalId, int QuotationId, int UserId, int DesignationId, string Remarks, int Status, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[OVERRIDE_QUOTATION_APPROVAL_DETAILS]";
            dBConnection.cmd.Parameters.AddWithValue("@APPROVAL_ID", ApprovalId);
            dBConnection.cmd.Parameters.AddWithValue("@QUOTATION_ID", QuotationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);
            dBConnection.cmd.Parameters.AddWithValue("@STATUS", Status);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int RejectAtApproval(int QuotationId, int UserId, int DesignationId, string Remarks, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[REJECT_QUOTATION_AT_APPROVAL]";
            dBConnection.cmd.Parameters.AddWithValue("@QUOTATION_ID", QuotationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }
    }
}
