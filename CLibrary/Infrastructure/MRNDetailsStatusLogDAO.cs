using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface MRNDetailsStatusLogDAO
    {
        List<MRNDetailsStatusLog> GetMrnDStatusByMrnDId(int MrnDId, DBConnection dbConnection);
        int InsertLog(int MrndId, int UserId, int Status, DBConnection dbConnection);
        int UpdateMRNLog(int mrndId, int userId, int Status, DBConnection dbConnection);
        int InsertLogTerminate(int MrndId, int UserId, DBConnection dbConnection);
        int InsertLogAfterClone(int MrndId, int UserId, DBConnection dbConnection);
        int InsertLogModified(int MrndId, int UserId, DBConnection dbConnection);
        int InsertLogAddToPR(int MrndId, int UserId, DBConnection dbConnection);
        int InsertLogAddStock(int MrndId, int UserId, DBConnection dbConnection);
        int InsertLogIssueStock(int MrndId, int UserId, DBConnection dbConnection);
        int InsertLogReceive(int MrndId, int UserId, DBConnection dbConnection);
        int InsertLogConfirmation(int MrndId, int UserId, DBConnection dbConnection);
        int InsertLogRejection(int MrndId, int UserId, DBConnection dbConnection);
        int InsertStockReturned(int MrndId, int UserId, DBConnection dbConnection);
        int InsertStockReturnForApproval(int MrndId, int UserId, DBConnection dbConnection);
        int InsertDepartmetStockReturned(int MrndId, int UserId, DBConnection dbConnection);
    }

    public class MRNDetailsStatusLogDAOImpl : MRNDetailsStatusLogDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
        public List<MRNDetailsStatusLog> GetMrnDStatusByMrnDId(int MrnDId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRN_DETAIL_STATUS_LOG AS MRNDSL " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS USER_NAME FROM COMPANY_LOGIN) AS CL ON MRNDSL.USER_ID = CL.USER_ID " +
                "INNER JOIN (SELECT MRN_DETAILS_LOG_ID,LOG_NAME FROM DEF_MRN_DETAILS_LOG) AS LOG ON LOG.MRN_DETAILS_LOG_ID = MRNDSL.STATUS " +
                "WHERE MRNDSL.MRND_ID=" + MrnDId + " ORDER BY LOGGED_DATE ASC;";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNDetailsStatusLog>(dbConnection.dr);
            }
        }

        public int InsertLog(int MrndId, int UserId, int Status, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();


            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'CRTD' ";
            Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            String sql = "INSERT INTO "+ dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + Status + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int InsertLogAddToPR(int MrndId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();


            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'ADD_PR' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            String sql = "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + Status + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int InsertLogTerminate(int MrndId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();


            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'TERM' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            String sql = "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + Status + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        //change
        public int UpdateMRNLog(int mrndId, int userId, int Status, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            String sql = "INSERT INTO MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + mrndId + ", " + Status + ", '" + LocalTime.Now + "', " + userId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int InsertLogModified(int MrndId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();


            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'MDFD' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());


            String sql = "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + Status + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int InsertLogAfterClone(int MrndId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            
            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'CLN' ";
            int LogStatus = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            
            String sql = "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + LogStatus + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int InsertLogAddStock(int MrndId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'ADDSTCK' ";
            int LogStatus = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());


            String sql = "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + LogStatus + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int InsertLogIssueStock(int MrndId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();


            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'STK_ISD' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            String sql = "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + Status + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int InsertLogReceive(int MrndId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'STK_RECVD' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            String sql = "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + Status + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int InsertLogConfirmation(int MrndId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'STK_CMFRMD' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            String sql = "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + Status + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int InsertLogRejection(int MrndId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'STCK_RJCTED' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            String sql = "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + Status + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int InsertStockReturned(int MrndId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'STCK_RETRN_APPROVED' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            String sql = "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + Status + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int InsertDepartmetStockReturned(int MrndId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'DEPT_STCK_RETRN_APPROVED' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            String sql = "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + Status + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int InsertStockReturnForApproval(int MrndId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_LOG_ID FROM DEF_MRN_DETAILS_LOG WHERE LOG_CODE = 'STCK_RETRNED' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            String sql = "INSERT INTO " + dbLibrary + ".MRN_DETAIL_STATUS_LOG (MRND_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + MrndId + ", " + Status + ", '" + LocalTime.Now + "', " + UserId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
