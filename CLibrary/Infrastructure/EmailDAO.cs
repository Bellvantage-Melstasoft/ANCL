using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface EmailDAO
    {
        List<string> WHHeadandMRNCreatorEmails(int mrnId, DBConnection dbConnection);
        List<string> MRNCreatorandMRNApproverEmails(int mrnId, DBConnection dbConnection);
        List<string> MRNCreatorApproverissuedPersonEmails(int mrndId, int mrndInId, DBConnection dbConnection);
        List<string> MRNCreatorDeliveredAndissuedPersonEmails(int mrndId, int mrndInId, DBConnection dbConnection);
        string GetMRNCreatorEmail(int MrnId, DBConnection dbConnection);
        string GetTRCreatorEmail(int TRId, DBConnection dbConnection);
    }
    public class EmailDAOSQLImpl : EmailDAO
    {
        public List<string> WHHeadandMRNCreatorEmails(int mrnId, DBConnection dbConnection)
        {
            List<string> emails = new List<string>();
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID = " +
                                           "(SELECT CREATED_BY FROM MRN_MASTER WHERE MRN_ID= " + mrnId + ") " +
                                           "UNION ALL " +
                                           "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID =  " +
                                           "(SELECT HEAD_OF_WAREHOUSE FROM WAREHOUSE WHERE WAREHOUSE_ID = " +
                                           "(SELECT WAREHOUSE_ID FROM MRN_MASTER WHERE MRN_ID = " + mrnId + ")) ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        emails.Add(dbConnection.dr[0].ToString());
                    }
                }
            }

            return emails;
        }

        public List<string> MRNCreatorandMRNApproverEmails(int mrnId, DBConnection dbConnection)
        {
            List<string> emails = new List<string>();
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID = " +
                                           "(SELECT CREATED_BY FROM MRN_MASTER WHERE MRN_ID= " + mrnId + ") " +
                                           "UNION ALL " +
                                           "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID =  " +
                                           "(SELECT APPROVED_BY FROM MRN_MASTER WHERE MRN_ID=" + mrnId + ") ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        emails.Add(dbConnection.dr[0].ToString());
                    }
                }
            }

            return emails;
        }

        public List<string> MRNCreatorApproverissuedPersonEmails(int mrndId, int mrndInId, DBConnection dbConnection)
        {
            List<string> emails = new List<string>();
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID = " +
                                           "(SELECT CREATED_BY FROM MRN_MASTER WHERE MRN_ID=(SELECT MRN_ID FROM MRN_DETAILS WHERE MRND_ID = " + mrndId + ")) " +
                                           "UNION ALL " +
                                           "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID = " +
                                           "(SELECT APPROVED_BY FROM MRN_MASTER WHERE MRN_ID=(SELECT MRN_ID FROM MRN_DETAILS WHERE MRND_ID = " + mrndId + ")) " +
                                           "UNION ALL " +
                                           "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID = " +
                                           "(SELECT ISSUED_BY FROM MRND_ISSUE_NOTE WHERE MRND_IN_ID=" + mrndInId + ") ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        emails.Add(dbConnection.dr[0].ToString());
                    }
                }
            }

            return emails;
        }

        public List<string> MRNCreatorDeliveredAndissuedPersonEmails(int mrndId, int mrndInId, DBConnection dbConnection)
        {
            List<string> emails = new List<string>();
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID = " +
                                           "(SELECT CREATED_BY FROM MRN_MASTER WHERE MRN_ID=(SELECT MRN_ID FROM MRN_DETAILS WHERE MRND_ID =" + mrndId + ")) " +
                                           "UNION ALL " +
                                           "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID = " +
                                           "(SELECT DELIVERED_BY FROM MRND_ISSUE_NOTE WHERE MRND_IN_ID=" + mrndInId + ") " +
                                           "UNION ALL " +
                                           "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID = " +
                                           "(SELECT ISSUED_BY FROM MRND_ISSUE_NOTE WHERE MRND_IN_ID=" + mrndInId + ") ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        emails.Add(dbConnection.dr[0].ToString());
                    }
                }
            }

            return emails;
        }

        public string GetMRNCreatorEmail(int MrnId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID = " +
                                           "(SELECT CREATED_BY FROM MRN_MASTER WHERE MRN_ID= " + MrnId + ");";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteScalar().ToString();
        }

        public string GetTRCreatorEmail(int TRId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID = " +
                                           "(SELECT CREATED_BY FROM TR_MASTER WHERE TR_ID= " + TRId + ");";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteScalar().ToString();
        }
    }
}
