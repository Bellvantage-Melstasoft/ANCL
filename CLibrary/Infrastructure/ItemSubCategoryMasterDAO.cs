using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface ItemSubCategoryMasterDAO
    {
        int SaveItemSubCategory(string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int UpdateItemSubCategory(int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int DeleteItemSubCategory(int SubCategoryId, DBConnection dbConnection);
        List<ItemSubCategoryMaster> FetchItemSubCategoryList(DBConnection dbConnection);
        List<ItemSubCategoryMaster> FetchItemSubCategoryById(int SubCategoryId, DBConnection dbConnection);
        ItemSubCategoryMaster FetchItemSubCategoryListByIdObj(int SubCategoryId, DBConnection dbConnection);
        List<ItemSubCategoryMaster> FetchItemSubCategoryListWithMainCategory(DBConnection dbConnection);
        List<ItemSubCategoryMaster> FetchItemSubCategoryByCategoryId(int CategoryId, DBConnection dbConnection);
        List<ItemSubCategoryMaster> searchSubCategoryNameList(string subcategoryName, int categoryId, int companyId, DBConnection dbConnection);

        List<ItemSubCategoryMaster> FetchItemSubCategoryListForInitialFrontView(DBConnection dbConnection);
    }

    public class ItemSubCategoryMasterDAOImpl : ItemSubCategoryMasterDAO
    {
        public int SaveItemSubCategory(string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            decimal SubCategoryId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_SUB_CATEGORY_MASTER\" WHERE  \"SUB_CATEGORY_NAME\" = '" + SubCategoryName+"' AND  \"CATEGORY_ID\" = "+CategoryId+"";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_SUB_CATEGORY_MASTER\" ";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    SubCategoryId = 001;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (\"SUB_CATEGORY_ID\")+1 AS MAXid FROM public.\"ITEM_SUB_CATEGORY_MASTER\" ";
                    SubCategoryId = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO  public.\"ITEM_SUB_CATEGORY_MASTER\" (\"SUB_CATEGORY_ID\" , \"SUB_CATEGORY_NAME\" , \"CATEGORY_ID\" , \"CREATED_DATETIME\", \"CREATED_BY\", \"UPDATED_DATETIME\", \"UPDATED_BY\", \"IS_ACTIVE\") VALUES (" + SubCategoryId + ",'" + SubCategoryName + "'," + CategoryId + ",'" + CreatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDate + "','" + UpdatedBy + "' ," + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();

            }
            else
            {
                return -1;
            }
        }

        public int UpdateItemSubCategory(int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_SUB_CATEGORY_MASTER\" WHERE  \"SUB_CATEGORY_NAME\" = '" + SubCategoryName + "' AND  \"CATEGORY_ID\" = " + CategoryId + " AND \"SUB_CATEGORY_ID\" != "+SubCategoryId+"";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.CommandText = "UPDATE public.\"ITEM_SUB_CATEGORY_MASTER\" SET \"SUB_CATEGORY_NAME\" = '" + SubCategoryName + "' , \"CATEGORY_ID\" = " + CategoryId + ", \"CREATED_DATETIME\" = '" + CreatedDateTime + "', \"CREATED_BY\" = '" + CreatedBy + "', \"UPDATED_DATETIME\" = '" + UpdatedDate + "', \"UPDATED_BY\" = '" + UpdatedBy + "', \"IS_ACTIVE\" = " + IsActive + " WHERE \"SUB_CATEGORY_ID\" = " + SubCategoryId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int DeleteItemSubCategory(int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE public.\"ITEM_SUB_CATEGORY_MASTER\" SET  \"IS_ACTIVE\" = 0 WHERE \"SUB_CATEGORY_ID\" = " + SubCategoryId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemSubCategoryMaster> FetchItemSubCategoryList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_SUB_CATEGORY_MASTER\" WHERE \"IS_ACTIVE\" = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategoryMaster>(dbConnection.dr);
            }
        }

        public List<ItemSubCategoryMaster> FetchItemSubCategoryById(int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_SUB_CATEGORY_MASTER\"  WHERE \"SUB_CATEGORY_ID\" =" + SubCategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategoryMaster>(dbConnection.dr);
            }
        }

        public ItemSubCategoryMaster FetchItemSubCategoryListByIdObj(int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_SUB_CATEGORY_MASTER\"  WHERE \"SUB_CATEGORY_ID\" =" + SubCategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemSubCategoryMaster>(dbConnection.dr);
            }
        }


        public List<ItemSubCategoryMaster> FetchItemSubCategoryListWithMainCategory(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_SUB_CATEGORY_MASTER\" INNER JOIN public.\"ITEM_CATEGORY\" ON public.\"ITEM_CATEGORY\".\"CATEGORY_ID\" = public.\"ITEM_SUB_CATEGORY\".\"CATEGORY_ID\" WHERE public.\"ITEM_CATEGORY\".\"IS_ACTIVE\"= 1 ORDER BY public.\"ITEM_SUB_CATEGORY\".\"SUB_CATEGORY_NAME\"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategoryMaster>(dbConnection.dr);
            }
        }

        public List<ItemSubCategoryMaster> FetchItemSubCategoryByCategoryId(int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_SUB_CATEGORY_MASTER\" INNER JOIN public.\"ITEM_CATEGORY\" ON public.\"ITEM_CATEGORY\".\"CATEGORY_ID\" = public.\"ITEM_SUB_CATEGORY\".\"CATEGORY_ID\" WHERE public.\"ITEM_CATEGORY\".\"CATEGORY_ID\"= " + CategoryId+" AND public.\"ITEM_SUB_CATEGORY\".\"IS_ACTIVE\" =1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategoryMaster>(dbConnection.dr);
            }
        }

        public List<ItemSubCategoryMaster> searchSubCategoryNameList(string subcategoryName, int categoryId, int companyId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }
        public List<ItemSubCategoryMaster> FetchItemSubCategoryListForInitialFrontView(DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }

    public class ItemSubCategoryMasterDAOSQLImpl: ItemSubCategoryMasterDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveItemSubCategory(string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            int  SubCategoryId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM "+ dbLibrary + ".ITEM_SUB_CATEGORY_MASTER WHERE  SUB_CATEGORY_NAME = '" + SubCategoryName + "' AND  CATEGORY_ID = " + CategoryId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER ";
                var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    SubCategoryId = 001;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (SUB_CATEGORY_ID)+1 AS MAXid FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER ";
                    SubCategoryId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO  " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER (SUB_CATEGORY_ID , SUB_CATEGORY_NAME , CATEGORY_ID , CREATED_DATETIME, CREATED_BY, UPDATED_DATETIME, UPDATED_BY, IS_ACTIVE) VALUES (" + SubCategoryId + ",'" + SubCategoryName + "'," + CategoryId + ",'" + CreatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDate + "','" + UpdatedBy + "' ," + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.ExecuteNonQuery();
                return SubCategoryId;

            }
            else
            {
                return -1;
            }
        }

        public int UpdateItemSubCategory(int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER WHERE  SUB_CATEGORY_NAME = '" + SubCategoryName + "' AND  CATEGORY_ID = " + CategoryId + " AND SUB_CATEGORY_ID != " + SubCategoryId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER SET SUB_CATEGORY_NAME = '" + SubCategoryName + "' , CATEGORY_ID = " + CategoryId + ", CREATED_DATETIME = '" + CreatedDateTime + "', CREATED_BY = '" + CreatedBy + "', UPDATED_DATETIME = '" + UpdatedDate + "', UPDATED_BY = '" + UpdatedBy + "', IS_ACTIVE = " + IsActive + " WHERE SUB_CATEGORY_ID = " + SubCategoryId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int DeleteItemSubCategory(int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER SET  IS_ACTIVE = 0 WHERE SUB_CATEGORY_ID = " + SubCategoryId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemSubCategoryMaster> FetchItemSubCategoryList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER WHERE IS_ACTIVE = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategoryMaster>(dbConnection.dr);
            }
        }

        public List<ItemSubCategoryMaster> FetchItemSubCategoryById(int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER  WHERE SUB_CATEGORY_ID =" + SubCategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategoryMaster>(dbConnection.dr);
            }
        }

        public ItemSubCategoryMaster FetchItemSubCategoryListByIdObj(int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER  WHERE SUB_CATEGORY_ID =" + SubCategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemSubCategoryMaster>(dbConnection.dr);
            }
        }


        public List<ItemSubCategoryMaster> FetchItemSubCategoryListWithMainCategory(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER  AS ISCM INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON " + dbLibrary + ".IC.CATEGORY_ID = public.ITEM_SUB_CATEGORY.CATEGORY_ID WHERE public.ITEM_CATEGORY.IS_ACTIVE = 1 ORDER BY public.TEM_SUB_CATEGORY.SUB_CATEGORY_NAME";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategoryMaster>(dbConnection.dr);
            }
        }

        public List<ItemSubCategoryMaster> FetchItemSubCategoryByCategoryId(int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER AS ISM INNER JOIN " + dbLibrary + ".ITEM_CATEGORY_MASTER AS ICM ON (ISM.CATEGORY_ID = ICM.CATEGORY_ID ) WHERE ISM.CATEGORY_ID = " + CategoryId + " AND ISM.IS_ACTIVE =1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategoryMaster>(dbConnection.dr);
            }
        }

        public List<ItemSubCategoryMaster> searchSubCategoryNameList(string subcategoryName, int categoryId,int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            
            dbConnection.cmd.CommandText = " SELECT * FROM  " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER    AS ISCM INNER JOIN  " + dbLibrary + ".ITEM_CATEGORY_MASTER    AS ICM ON( ISCM.CATEGORY_ID = ICM.CATEGORY_ID ) WHERE  LOWER ( ISCM.SUB_CATEGORY_NAME  ) LIKE '" + "%" + subcategoryName.ToLower() + "%" + "' AND ISCM.CATEGORY_ID = "+categoryId+" AND ISCM.SUB_CATEGORY_ID  NOT IN ( SELECT ISC.SUB_CATEGORY_ID FROM "+dbLibrary+".ITEM_SUB_CATEGORY AS ISC WHERE ISC.COMPANY_ID = "+companyId+")  ORDER BY ISCM.SUB_CATEGORY_NAME";

        //dbConnection.cmd.CommandText = "SELECT ISM.SUB_CATEGORY_ID, ISM.SUB_CATEGORY_NAME, ISM.CATEGORY_ID, ISM.CREATED_DATETIME, ISM.CREATED_BY, ISM.UPDATED_DATETIME, ISM.UPDATED_BY, ISM.IS_ACTIVE, ISC.COMPANY_ID, CURENT_COMPANY_ID = "+ companyId + " FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER AS ISM LEFT JOIN " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC ON ( ISM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID) WHERE LOWER ( ISM.SUB_CATEGORY_NAME  ) LIKE '" + "%" + subcategoryName.ToLower() + "%" + "' AND ISM.CATEGORY_ID = " + categoryId + " ORDER BY ISM.SUB_CATEGORY_NAME";
        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategoryMaster>(dbConnection.dr);
            }
        }
        public List<ItemSubCategoryMaster> FetchItemSubCategoryListForInitialFrontView(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER  AS ISC INNER JOIN " + dbLibrary + ".ITEM_CATEGORY_MASTER  AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID  WHERE ISC.IS_ACTIVE = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategoryMaster>(dbConnection.dr);
            }
        }
    }
}
