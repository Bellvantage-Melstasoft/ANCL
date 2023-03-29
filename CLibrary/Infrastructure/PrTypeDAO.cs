using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface PrTypeDAO
    {
        List<PrType> FetchPRTypesByCompanyId(int CompanyId, DBConnection dbConnection);
        List<PrType> FetchAllPRTypes(DBConnection dbConnection);

        int SavePRTypes(int CompanyId, string PrTypeName, int IsActive, DBConnection dbConnection);
        int UpdatePRTypes(int PrTypeId, int CompanyId, string PrTypeName, int IsActive, DBConnection dbConnection);
        int DeletePRTypes(int PrTypeId,int CompanyId, DBConnection dbConnection);
    }

    public class PrTypeDAOImpl : PrTypeDAO
    {
        public List<PrType> FetchPRTypesByCompanyId(int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_TYPE\"  WHERE \"IS_ACTIVE\"=1 AND \"COMPANY_ID\" = " + CompanyId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrType>(dbConnection.dr);
            }
        }

        public List<PrType> FetchAllPRTypes(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_TYPE\" INNER JOIN public.\"COMPANY_DEPARTMENT\" ON public.\"PR_TYPE\".\"COMPANY_ID\" = public.\"COMPANY_DEPARTMENT\".\"DEPARTMENT_ID\" ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrType>(dbConnection.dr);
            }
        }

        public int SavePRTypes(int CompanyId, string PrTypeName, int IsActive, DBConnection dbConnection)
        {
            decimal PrTypeIdNo = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"PR_TYPE\" WHERE  \"PR_TYPE_NAME\" = '" + PrTypeName + "' AND  \"COMPANY_ID\" = " + CompanyId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"PR_TYPE\" ";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    PrTypeIdNo = 001;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (\"PR_TYPE_ID\")+1 AS MAXid FROM public.\"PR_TYPE\" ";
                    PrTypeIdNo = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO  public.\"PR_TYPE\" (\"COMPANY_ID\" , \"PR_TYPE_ID\" , \"PR_TYPE_NAME\" , \"IS_ACTIVE\") VALUES (" + CompanyId + "," + PrTypeIdNo + ",'" + PrTypeName + "'," + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();

            }
            else
            {
                return -1;
            }
        }

        public int UpdatePRTypes(int PrTypeId, int CompanyId, string PrTypeName, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"PR_TYPE\" WHERE  \"PR_TYPE_NAME\" = '" + PrTypeName + "' AND  \"COMPANY_ID\" = " + CompanyId + " AND \"IS_ACTIVE\"=1 ";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.CommandText = "UPDATE public.\"PR_TYPE\" SET \"PR_TYPE_NAME\" = '" + PrTypeName + "' , \"IS_ACTIVE\" = " + IsActive + "  WHERE  \"PR_TYPE_ID\" = " + PrTypeId + " AND  \"COMPANY_ID\" = " + CompanyId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int DeletePRTypes(int PrTypeId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE public.\"PR_TYPE\" SET  \"IS_ACTIVE\" = 0  WHERE \"PR_TYPE_ID\" = " + PrTypeId + " AND \"COMPANY_ID\" = " + CompanyId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }

    public class PrTypeDAOSQLImpl : PrTypeDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<PrType> FetchPRTypesByCompanyId(int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_TYPE  WHERE IS_ACTIVE=1 AND COMPANY_ID = " + CompanyId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrType>(dbConnection.dr);
            }
        }

        public List<PrType> FetchAllPRTypes(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_TYPE AS PT INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON PT.COMPANY_ID = CD.DEPARTMENT_ID ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrType>(dbConnection.dr);
            }
        }

        public int SavePRTypes(int CompanyId, string PrTypeName, int IsActive, DBConnection dbConnection)
        {
            decimal PrTypeIdNo = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_TYPE WHERE  PR_TYPE_NAME = '" + PrTypeName + "' AND  COMPANY_ID = " + CompanyId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_TYPE ";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    PrTypeIdNo = 001;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (PR_TYPE_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_TYPE ";
                    PrTypeIdNo = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO  " + dbLibrary + ".PR_TYPE (COMPANY_ID , PR_TYPE_ID , PR_TYPE_NAME , IS_ACTIVE) VALUES (" + CompanyId + "," + PrTypeIdNo + ",'" + PrTypeName + "'," + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();

            }
            else
            {
                return -1;
            }
        }

        public int UpdatePRTypes(int PrTypeId, int CompanyId, string PrTypeName, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_TYPE WHERE  PR_TYPE_NAME = '" + PrTypeName + "' AND  COMPANY_ID = " + CompanyId + " AND IS_ACTIVE=1 ";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_TYPE SET PR_TYPE_NAME = '" + PrTypeName + "' , IS_ACTIVE = " + IsActive + "  WHERE  PR_TYPE_ID = " + PrTypeId + " AND  COMPANY_ID = " + CompanyId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int DeletePRTypes(int PrTypeId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_TYPE SET  IS_ACTIVE = 0  WHERE PR_TYPE_ID = " + PrTypeId + " AND COMPANY_ID = " + CompanyId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
