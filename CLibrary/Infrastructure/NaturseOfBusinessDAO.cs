using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
   public interface NaturseOfBusinessDAO
    {
        int SaveBusinessCategory(string BusinessCategoryName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int UpdateBusinessCategory(int BusinessCategoryId, string BusinessCategoryName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int DeleteBusinessCategory(int BusinessCategoryId, DBConnection dbConnection);
        List<NaturseOfBusiness> FetchBusinessCategoryList(DBConnection dbConnection);
    }
    public class NaturseOfBusinessDAOImpl : NaturseOfBusinessDAO
    {
        public int DeleteBusinessCategory(int BusinessCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE public.\"NATURE_OF_BUSINESS\" SET  \"IS_ACTIVE\" = 0 WHERE \"BUSINESS_CATEGORY_ID\" = " + BusinessCategoryId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<NaturseOfBusiness> FetchBusinessCategoryList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"NATURE_OF_BUSINESS\" ORDER BY \"BUSINESS_CATEGORY_NAME\"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<NaturseOfBusiness>(dbConnection.dr);
            }
        }

        public int SaveBusinessCategory(string BusinessCategoryName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            decimal CategoryId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"NATURE_OF_BUSINESS\" WHERE \"BUSINESS_CATEGORY_NAME\" = '" + BusinessCategoryName + "'";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"NATURE_OF_BUSINESS\" ";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    CategoryId = 1;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (\"BUSINESS_CATEGORY_ID\")+1 AS MAXid FROM public.\"NATURE_OF_BUSINESS\" ";
                    CategoryId = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO  public.\"NATURE_OF_BUSINESS\" (\"BUSINESS_CATEGORY_ID\" , \"BUSINESS_CATEGORY_NAME\" , \"CREATED_DATE\" , \"CREATED_USER\", \"UPDATED_DATE\", \"UPDATED_USER\", \"IS_ACTIVE\") VALUES (" + CategoryId + ",'" + BusinessCategoryName + "','" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int UpdateBusinessCategory(int BusinessCategoryId, string BusinessCategoryName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"NATURE_OF_BUSINESS\" WHERE \"BUSINESS_CATEGORY_NAME\" = '" + BusinessCategoryName + "' AND \"BUSINESS_CATEGORY_ID\" != " + BusinessCategoryId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE public.\"NATURE_OF_BUSINESS\" SET \"BUSINESS_CATEGORY_NAME\" = '" + BusinessCategoryName + "' , \"UPDATED_DATE\" = '" + UpdatedDate + "', \"UPDATED_USER\" = '" + UpdatedBy + "', \"IS_ACTIVE\" = " + IsActive + " WHERE \"BUSINESS_CATEGORY_ID\" = " + BusinessCategoryId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }
    }

    public class NaturseOfBusinessDAOSQLImpl : NaturseOfBusinessDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int DeleteBusinessCategory(int BusinessCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".NATURE_OF_BUSINESS SET  IS_ACTIVE = 0 WHERE BUSINESS_CATEGORY_ID = " + BusinessCategoryId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<NaturseOfBusiness> FetchBusinessCategoryList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".NATURE_OF_BUSINESS ORDER BY BUSINESS_CATEGORY_NAME";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<NaturseOfBusiness>(dbConnection.dr);
            }
        }

        public int SaveBusinessCategory(string BusinessCategoryName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            decimal CategoryId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".NATURE_OF_BUSINESS WHERE BUSINESS_CATEGORY_NAME = '" + BusinessCategoryName + "'";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".NATURE_OF_BUSINESS ";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    CategoryId = 1;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (BUSINESS_CATEGORY_ID)+1 AS MAXid FROM " + dbLibrary + ".NATURE_OF_BUSINESS ";
                    CategoryId = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".NATURE_OF_BUSINESS (BUSINESS_CATEGORY_ID , BUSINESS_CATEGORY_NAME , CREATED_DATE , CREATED_USER, UPDATED_DATE, UPDATED_USER, IS_ACTIVE) VALUES (" + CategoryId + ",'" + BusinessCategoryName + "','" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int UpdateBusinessCategory(int BusinessCategoryId, string BusinessCategoryName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".NATURE_OF_BUSINESS WHERE BUSINESS_CATEGORY_NAME = '" + BusinessCategoryName + "' AND BUSINESS_CATEGORY_ID != " + BusinessCategoryId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".NATURE_OF_BUSINESS SET BUSINESS_CATEGORY_NAME = '" + BusinessCategoryName + "' , UPDATED_DATE = '" + UpdatedDate + "', UPDATED_USER = '" + UpdatedBy + "', IS_ACTIVE = " + IsActive + " WHERE BUSINESS_CATEGORY_ID = " + BusinessCategoryId + " ";
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
