using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface ItemCategoryMasterDAO
    {
        int SaveItemMasterCategory(string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int UpdateItemCategory(int CategoryId, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int DeleteItemCategory(int CategoryId, DBConnection dbConnection);
        List<ItemCategoryMaster> FetchItemCategoryList(DBConnection dbConnection);
        List<ItemCategoryMaster> FetchItemCategoryById(int CategoryId, DBConnection dbConnection);
        ItemCategoryMaster FetchItemCategoryListByIdObj(int CategoryId, DBConnection dbConnection);
        List<ItemCategoryMaster> searchCategoryName(string categoryName, int companyId, DBConnection dbConnection);
        List<ItemCategoryMaster> FetchItemCategoryfORSubCategoryCreationList(int companyId, DBConnection dbConnection);
    }

    public class ItemCategoryMasterDAOImpl : ItemCategoryMasterDAO
    {
        public int SaveItemMasterCategory(string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            decimal CategoryId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_CATEGORY_MASTER\" WHERE \"CATEGORY_NAME\" = '" + CategoryName + "'";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_CATEGORY_MASTER\" ";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    CategoryId = 001;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (\"CATEGORY_ID\")+1 AS MAXid FROM public.\"ITEM_CATEGORY_MASTER\" ";
                    CategoryId = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO  public.\"ITEM_CATEGORY_MASTER\" (\"CATEGORY_ID\" , \"CATEGORY_NAME\" , \"CRAETED_DATE\" , \"CREATED_BY\", \"UPDATED_DATE\", \"UPDATED_BY\", \"IS_ACTIVE\") VALUES (" + CategoryId + ",'" + CategoryName + "','" + CategorDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int UpdateItemCategory(int CategoryId, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_CATEGORY_MASTER\" WHERE \"CATEGORY_NAME\" = '" + CategoryName + "' AND \"CATEGORY_ID\" != "+CategoryId+"";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE public.\"ITEM_CATEGORY_MASTER\" SET \"CATEGORY_NAME\" = '" + CategoryName + "' , \"CRAETED_DATE\" = '" + CategorDate + "', \"CREATED_BY\" = '" + CreatedBy + "', \"UPDATED_DATE\" = '" + UpdatedDate + "', \"UPDATED_BY\" = '" + UpdatedBy + "', \"IS_ACTIVE\" = " + IsActive + " WHERE \"CATEGORY_ID\" = " + CategoryId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int DeleteItemCategory(int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE public.\"ITEM_CATEGORY_MASTER\" SET  \"IS_ACTIVE\" = 0 WHERE \"CATEGORY_ID\" = " + CategoryId +"";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemCategoryMaster> FetchItemCategoryList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_CATEGORY_MASTER\" ORDER BY  public.\"ITEM_CATEGORY\".\"CATEGORY_NAME\"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryMaster>(dbConnection.dr);
            }
        }

        public List<ItemCategoryMaster> FetchItemCategoryById(int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_CATEGORY_MASTER\"  WHERE CATEGORY_ID =" + CategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryMaster>(dbConnection.dr);
            }
        }

        public ItemCategoryMaster FetchItemCategoryListByIdObj(int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_CATEGORY_MASTER\"  WHERE CATEGORY_ID =" + CategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemCategoryMaster>(dbConnection.dr);
            }
        }

        public List<ItemCategoryMaster> searchCategoryName(string categoryName, int companyId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<ItemCategoryMaster> FetchItemCategoryfORSubCategoryCreationList(int companyId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        
    }

    public class ItemCategoryMasterDAOSQLImpl : ItemCategoryMasterDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveItemMasterCategory(string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            int CategoryId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER WHERE CATEGORY_NAME = '" + CategoryName + "'";
            var countExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER ";
                var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    CategoryId = 001;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (CATEGORY_ID)+1 AS MAXid FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER ";
                    CategoryId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO  " + dbLibrary + ".ITEM_CATEGORY_MASTER (CATEGORY_ID , CATEGORY_NAME , CRAETED_DATE , CREATED_BY, UPDATED_DATE, UPDATED_BY, IS_ACTIVE) VALUES (" + CategoryId + ",'" + CategoryName + "','" + CategorDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.ExecuteNonQuery();
                return CategoryId;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT CATEGORY_ID FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER WHERE CATEGORY_NAME = '" + CategoryName + "'";
                return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }
        }

        public int UpdateItemCategory(int CategoryId, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER WHERE CATEGORY_NAME = '" + CategoryName + "' AND CATEGORY_ID = " + CategoryId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 1)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ITEM_CATEGORY_MASTER "+
                                               " SET CATEGORY_NAME = '" + CategoryName + "' ,  UPDATED_DATE = '" + UpdatedDate + "', UPDATED_BY = '" + UpdatedBy + "', IS_ACTIVE = " + IsActive + " "+
                                               " WHERE CATEGORY_ID = " + CategoryId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int DeleteItemCategory(int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE " + dbLibrary + ".ITEM_CATEGORY_MASTER WHERE CATEGORY_ID = " + CategoryId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemCategoryMaster> FetchItemCategoryList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER ORDER BY  " + dbLibrary + ".ITEM_CATEGORY_MASTER.CATEGORY_NAME";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryMaster>(dbConnection.dr);
            }
        }

        public List<ItemCategoryMaster> FetchItemCategoryById(int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER  WHERE CATEGORY_ID =" + CategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryMaster>(dbConnection.dr);
            }
        }

        public ItemCategoryMaster FetchItemCategoryListByIdObj(int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER  WHERE CATEGORY_ID =" + CategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemCategoryMaster>(dbConnection.dr);
            }
        }

        public List<ItemCategoryMaster> searchCategoryName(string categoryName, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER  WHERE LOWER( CATEGORY_NAME ) LIKE '" + "%"+ categoryName.ToLower() +"%"+ "' AND CATEGORY_ID NOT IN (SELECT CATEGORY_ID FROM ITEM_CATEGORY WHERE COMPANY_ID = "+companyId+") ORDER BY CATEGORY_NAME ";
           
            
            // dbConnection.cmd.CommandText = "SELECT ICM.CATEGORY_ID, ICM.CATEGORY_NAME, ICM.CRAETED_DATE, ICM.CREATED_BY, ICM.UPDATED_DATE, ICM.UPDATED_BY, ICM.IS_ACTIVE,IC.COMPANY_ID,CURRNT_COMPANY_ID = "+ companyId + " FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER AS ICM LEFT JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON (ICM.CATEGORY_ID = IC.CATEGORY_ID) WHERE LOWER( ICM.CATEGORY_NAME ) LIKE '" + "%"+ categoryName.ToLower() +"%"+ "'ORDER BY  ICM.CATEGORY_NAME ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryMaster>(dbConnection.dr);
            }
        }

        public List<ItemCategoryMaster> FetchItemCategoryfORSubCategoryCreationList(int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER AS ICM INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON (ICM.CATEGORY_ID = IC.CATEGORY_ID) WHERE IC.COMPANY_ID = "+ companyId + "   ORDER BY ICM.CATEGORY_NAME";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryMaster>(dbConnection.dr);
            }
        }

      
    }
}
