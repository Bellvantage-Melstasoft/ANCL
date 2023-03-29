using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface PRDetailsStatusLogDAO
    {
        List<PRDetailsStatusLog> GetPrDStatusByPrDId(int PrDId, DBConnection dbConnection);
        int InsertLog(int prdId, int userId, string StatusCode, DBConnection dbConnection);
        int UpdatePRStatusLog(int userId, int prdId, string LogStatus, DBConnection dbConnection);
        int UpdatePRStatusLogForPoCancel(int userId, string LogStatus, int itemId, int PrId, DBConnection dbConnection);
    }

    class PRDetailsStatusLogDAOSqlImpl : PRDetailsStatusLogDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<PRDetailsStatusLog> GetPrDStatusByPrDId(int PrDId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM PR_DETAIL_STATUS_LOG AS PDSL "+
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS USER_NAME FROM COMPANY_LOGIN) AS CL ON PDSL.USER_ID=CL.USER_ID " +
                "INNER JOIN (SELECT PR_DETAILS_LOG_ID,LOG_NAME FROM DEF_PR_DETAILS_LOG) AS LOG ON LOG.PR_DETAILS_LOG_ID=PDSL.STATUS " +
                "WHERE PDSL.PRD_ID=" + PrDId+ " ORDER BY LOGGED_DATE ASC;";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PRDetailsStatusLog>(dbConnection.dr);
            }
        }

        public int InsertLog(int prdId, int userId, string StatusCode, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE = '" + StatusCode+"' ";
            int status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            
            String sql = "INSERT INTO " + dbLibrary + ".PR_DETAIL_STATUS_LOG (PRD_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + prdId + ", " + status + ", '" + LocalTime.Now + "', " + userId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int UpdatePRStatusLog(int userId, int prdId,  string LogStatus, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO PR_DETAIL_STATUS_LOG (PRD_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + prdId + ", (SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='" + LogStatus + "'), '" + LocalTime.Now + "', " + userId + ") ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdatePRStatusLogForPoCancel(int userId, string LogStatus, int itemId, int PrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO PR_DETAIL_STATUS_LOG (PRD_ID, STATUS, LOGGED_DATE, USER_ID) VALUES ((SELECT PRD_ID FROM PR_DETAIL WHERE PR_ID = "+ PrId + " AND ITEM_ID = "+ itemId + "), (SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='" + LogStatus + "'), '" + LocalTime.Now + "', " + userId + ") ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }


}
