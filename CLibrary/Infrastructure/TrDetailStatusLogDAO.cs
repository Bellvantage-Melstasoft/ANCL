using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;

namespace CLibrary.Infrastructure
{
    public interface TrDetailStatusLogDAO
    {
        int UpdateTRLog(int trdId, int userId, int Status, DBConnection dbConnection);
        List<TrDetailStatusLog> TRLogDetails(int TRDId, DBConnection dbConnection);
    }

    public class TrDetailStatusLogDAOSQLImpl : TrDetailStatusLogDAO
    {
        public int UpdateTRLog(int trdId, int userId, int Status, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            String sql = "INSERT INTO TR_DETAIL_STATUS_LOG (TRD_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + trdId + ", " + Status + ", '" + LocalTime.Now + "', " + userId + ")";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TrDetailStatusLog> TRLogDetails(int TRDId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT SL.*, CL.FIRST_NAME FROM TR_DETAIL_STATUS_LOG AS SL " +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON SL.USER_ID = CL.USER_ID " +
                                             "WHERE TRD_ID = " + TRDId + " ORDER BY LOGGED_DATE";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TrDetailStatusLog>(dbConnection.dr);
            }
        }
    }

    }
