using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface PrUpdateLogDAO {
        int Save(PrUpdateLog log, DBConnection dbConnection);
        List<PrUpdateLog> FetchPrUpdateLog(int prId, DBConnection dbConnection);
    }
    class PrUpdateLogDAOImpl : PrUpdateLogDAO {

        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<PrUpdateLog> FetchPrUpdateLog(int prId, DBConnection dbConnection)
        {
            List<PrUpdateLog> prUpdateLog = new List<PrUpdateLog>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_UPDATE_LOG AS PRUP " +
                                           " INNER JOIN  " + dbLibrary + ".PR_MASTER as PR ON PR.PR_ID = PRUP.PR_ID  "+
                                           " LEFT JOIN(SELECT USER_ID, FIRST_NAME AS UPDATED_BY_NAME FROM COMPANY_LOGIN) AS CLC ON PRUP.UPDATED_BY = CLC.USER_ID " +
                                           "WHERE PRUP.PR_ID =" + prId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prUpdateLog = dataAccessObject.ReadCollection<PrUpdateLog>(dbConnection.dr);
            }
            return prUpdateLog;
        }

        public int Save(PrUpdateLog log, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [PR_UPDATE_LOG] \n");
            sql.Append("    ([PR_ID] \n");
            sql.Append("    ,[UPDATED_BY] \n");
            sql.Append("    ,[UPDATED_DATE] \n");
            sql.Append("    ,[UPDATE_REMARKS]) \n");
            sql.Append("VALUES \n");
            sql.Append("    ("+ log.PrId + "," + log.UpdatedBy + ",'" + LocalTime.Now + "','" + log.UpdateRemarks.ToString() + "')");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
