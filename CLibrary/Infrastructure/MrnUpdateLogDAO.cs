using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface MrnUpdateLogDAO {
        int Save(MrnUpdateLog log, DBConnection dbConnection);
        List<MrnUpdateLog> FetchMrnUpdateLog(int mrnId, DBConnection dbConnection);
    }
    class MrnUpdateLogDAOImpl : MrnUpdateLogDAO {

        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<MrnUpdateLog> FetchMrnUpdateLog(int mrnId, DBConnection dbConnection)
        {
            List<MrnUpdateLog> mrnUpdateLog = new List<MrnUpdateLog>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_UPDATE_LOG AS MRNUP " +
                                           " INNER JOIN  " + dbLibrary + ".MRN_MASTER as MRN ON MRN.MRN_ID = MRNUP.MRN_ID  "+
                                           " LEFT JOIN(SELECT USER_ID, FIRST_NAME AS UPDATED_BY_NAME FROM COMPANY_LOGIN) AS CLC ON MRNUP.UPDATED_BY = CLC.USER_ID " +
                                           "WHERE MRNUP.MRN_ID =" + mrnId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnUpdateLog = dataAccessObject.ReadCollection<MrnUpdateLog>(dbConnection.dr);
            }
            return mrnUpdateLog;
        }

        public int Save(MrnUpdateLog log, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [MRN_UPDATE_LOG] \n");
            sql.Append("    ([MRN_ID] \n");
            sql.Append("    ,[UPDATED_BY] \n");
            sql.Append("    ,[UPDATED_DATE] \n");
            sql.Append("    ,[UPDATE_REMARKS]) \n");
            sql.Append("VALUES \n");
            sql.Append("    ("+ log.MrnId + "," + log.UpdatedBy + ",'" + LocalTime.Now + "','" + log.UpdateRemarks.ToString() + "')");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
