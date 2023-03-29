using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
   public interface FunctionActionDAO
    {
        int SaveFunctionAction(string actionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int UpdateFunctionAction(int actionId, string actionName,  DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int DeleteFunctionAction(int actionId, DBConnection dbConnection);
        List<FunctionAction> FetchFunctionAction(DBConnection dbConnection);
        FunctionAction FetchFunctionActionObjByActionoid(int actionId,DBConnection dbConnection);
    }
    public class FunctionActionDAOImpl : FunctionActionDAO
    {
        public int DeleteFunctionAction(int actionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE public.\"FUNCTION_ACTION\" SET  \"IS_ACTIVE\" = 0 WHERE \"FUNCTION_ID\" = " + actionId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<FunctionAction> FetchFunctionAction(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"FUNCTION_ACTION\"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<FunctionAction>(dbConnection.dr);
            }
        }

        public FunctionAction FetchFunctionActionObjByActionoid(int actionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"FUNCTION_ACTION\" WHERE \"FUNCTION_ID\" = " + actionId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<FunctionAction>(dbConnection.dr);
            }
        }

        public int SaveFunctionAction(string actionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            int actionId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"FUNCTION_ACTION\" WHERE \"FUNCTION_NAME\" = '" + actionName + "'";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"FUNCTION_ACTION\" ";
                var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    actionId = 1;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (\"FUNCTION_ID\")+1 AS MAXid FROM public.\"FUNCTION_ACTION\" ";
                    actionId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO  public.\"FUNCTION_ACTION\" (\"FUNCTION_ID\" , \"FUNCTION_NAME\" , \"CREATED_DATE\" , \"CREATED_BY\", \"UPDATED_DATE\", \"UPDATED_BY\", \"IS_ACTIVE\") VALUES (" + actionId + ",'" + actionName + "','" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                dbConnection.cmd.ExecuteNonQuery();
                return actionId;
            }
            else
            {
                return -1;
            }
        }

        public int UpdateFunctionAction(int actionId, string actionName, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"FUNCTION_ACTION\" WHERE \"FUNCTION_NAME\" = '" + actionName + "' AND \"FUNCTION_ID\" != " + actionId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE public.\"FUNCTION_ACTION\" SET \"FUNCTION_NAME\" = '" + actionName + "' , \"UPDATED_DATE\" = '" + UpdatedDate + "', \"UPDATED_BY\" = '" + UpdatedBy + "', \"IS_ACTIVE\" = " + IsActive + " WHERE \"FUNCTION_ID\" = " + actionId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }
    }

    public class FunctionActionDAOSQLImpl : FunctionActionDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int DeleteFunctionAction(int actionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".FUNCTION_ACTION SET  IS_ACTIVE = 0 WHERE FUNCTION_ID = " + actionId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<FunctionAction> FetchFunctionAction(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".FUNCTION_ACTION";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<FunctionAction>(dbConnection.dr);
            }
        }

        public FunctionAction FetchFunctionActionObjByActionoid(int actionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".FUNCTION_ACTION WHERE FUNCTION_ID = " + actionId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<FunctionAction>(dbConnection.dr);
            }
        }

        public int SaveFunctionAction(string actionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            int actionId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".FUNCTION_ACTION WHERE FUNCTION_NAME = '" + actionName + "'";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".FUNCTION_ACTION ";
                var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    actionId = 1;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (FUNCTION_ID)+1 AS MAXid FROM " + dbLibrary + ".FUNCTION_ACTION ";
                    actionId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".FUNCTION_ACTION (FUNCTION_ID , FUNCTION_NAME , CREATED_DATE , CREATED_BY, UPDATED_DATE, UPDATED_BY, IS_ACTIVE) VALUES (" + actionId + ",'" + actionName + "','" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                dbConnection.cmd.ExecuteNonQuery();
                return actionId;
            }
            else
            {
                return -1;
            }
        }

        public int UpdateFunctionAction(int actionId, string actionName, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".FUNCTION_ACTION WHERE FUNCTION_NAME = '" + actionName + "' AND FUNCTION_ID != " + actionId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".FUNCTION_ACTION SET FUNCTION_NAME = '" + actionName + "' , UPDATED_DATE = '" + UpdatedDate + "', UPDATED_BY = '" + UpdatedBy + "', IS_ACTIVE = " + IsActive + " WHERE FUNCTION_ID = " + actionId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }
    }
}
