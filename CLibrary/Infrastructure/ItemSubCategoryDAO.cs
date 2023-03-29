using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface ItemSubCategoryDAO
    {
        int SaveItemSubCategory(int companyId, int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int UpdateItemSubCategory(int companyId, int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int DeleteItemSubCategory(int companyId, int SubCategoryId, DBConnection dbConnection);
        List<ItemSubCategory> FetchItemSubCategoryList(int companyId, DBConnection dbConnection);
        List<ItemSubCategory> FetchItemSubCategoryById(int SubCategoryId, DBConnection dbConnection);
        ItemSubCategory FetchItemSubCategoryListByIdObj(int SubCategoryId, DBConnection dbConnection);
        List<ItemSubCategory> FetchItemSubCategoryListWithMainCategory(DBConnection dbConnection);
        List<ItemSubCategory> FetchItemSubCategoryByCategoryId(int CategoryId, int companyid, DBConnection dbConnection);
        int UpdateItemSubCategoryStatus(int companyId,int categoryID, int subCategoryId, int status, DBConnection dbConnection);
        List<ItemSubCategory> getStoreKeeperList(int companyID, DBConnection dbConnection);
        List<ItemSubCategory> SearchSubCategoryList(int companyId, string text, DBConnection dbConnection);
    }
  
    public class ItemSubCategoryDAOImpl : ItemSubCategoryDAO
    {
        public int SaveItemSubCategory(int companyId, int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_SUB_CATEGORY\" WHERE  \"SUB_CATEGORY_NAME\" = '" + SubCategoryName + "' AND  \"CATEGORY_ID\" = " + CategoryId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                //    dbConnection.cmd.Parameters.Clear();

                //    dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_SUB_CATEGORY\" ";
                //    var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                //    if (count == 0)
                //    {
                //        SubCategoryId = 001;
                //    }
                //    else
                //    {
                //        dbConnection.cmd.CommandText = "SELECT MAX (\"SUB_CATEGORY_ID\")+1 AS MAXid FROM public.\"ITEM_SUB_CATEGORY\" ";
                //        SubCategoryId = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                //    }
                dbConnection.cmd.CommandText = "INSERT INTO  public.\"ITEM_SUB_CATEGORY\" (\"COMPANY_ID\",\"SUB_CATEGORY_ID\" , \"SUB_CATEGORY_NAME\" , \"CATEGORY_ID\" , \"CREATED_DATETIME\", \"CREATED_BY\", \"UPDATED_DATETIME\", \"UPDATED_BY\", \"IS_ACTIVE\") VALUES (  " + companyId + "," + SubCategoryId + ",'" + SubCategoryName + "'," + CategoryId + ",'" + CreatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDate + "','" + UpdatedBy + "' ," + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();

            }
            else
            {
                return -1;
            }
        }

        public int UpdateItemSubCategory(int companyId, int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_SUB_CATEGORY\" WHERE  \"SUB_CATEGORY_NAME\" = '" + SubCategoryName + "' AND  \"CATEGORY_ID\" = " + CategoryId + " AND \"SUB_CATEGORY_ID\" != " + SubCategoryId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.CommandText = "UPDATE public.\"ITEM_SUB_CATEGORY\" SET \"SUB_CATEGORY_NAME\" = '" + SubCategoryName + "' , \"CATEGORY_ID\" = " + CategoryId + ", \"CREATED_DATETIME\" = '" + CreatedDateTime + "', \"CREATED_BY\" = '" + CreatedBy + "', \"UPDATED_DATETIME\" = '" + UpdatedDate + "', \"UPDATED_BY\" = '" + UpdatedBy + "', \"IS_ACTIVE\" = " + IsActive + " WHERE \"SUB_CATEGORY_ID\" = " + SubCategoryId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int DeleteItemSubCategory(int companyId, int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE public.\"ITEM_SUB_CATEGORY\" SET  \"IS_ACTIVE\" = 0 WHERE \"SUB_CATEGORY_ID\" = " + SubCategoryId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemSubCategory> FetchItemSubCategoryList(int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_SUB_CATEGORY\" WHERE \"IS_ACTIVE\" = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategory>(dbConnection.dr);
            }
        }

        public List<ItemSubCategory> FetchItemSubCategoryById(int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_SUB_CATEGORY\"  WHERE \"SUB_CATEGORY_ID\" =" + SubCategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategory>(dbConnection.dr);
            }
        }

        public ItemSubCategory FetchItemSubCategoryListByIdObj(int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_SUB_CATEGORY\"  WHERE \"SUB_CATEGORY_ID\" =" + SubCategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemSubCategory>(dbConnection.dr);
            }
        }


        public List<ItemSubCategory> FetchItemSubCategoryListWithMainCategory(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_SUB_CATEGORY\" INNER JOIN public.\"ITEM_CATEGORY\" ON public.\"ITEM_CATEGORY\".\"CATEGORY_ID\" = public.\"ITEM_SUB_CATEGORY\".\"CATEGORY_ID\" WHERE public.\"ITEM_CATEGORY\".\"IS_ACTIVE\"= 1 ORDER BY public.\"ITEM_SUB_CATEGORY\".\"SUB_CATEGORY_NAME\"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategory>(dbConnection.dr);
            }
        }

        public List<ItemSubCategory> FetchItemSubCategoryByCategoryId(int CategoryId, int companyid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_SUB_CATEGORY\" INNER JOIN public.\"ITEM_CATEGORY\" ON public.\"ITEM_CATEGORY\".\"CATEGORY_ID\" = public.\"ITEM_SUB_CATEGORY\".\"CATEGORY_ID\" WHERE public.\"ITEM_CATEGORY\".\"CATEGORY_ID\"= " + CategoryId + " AND public.\"ITEM_SUB_CATEGORY\".\"IS_ACTIVE\" =1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategory>(dbConnection.dr);
            }
        }

        public int UpdateItemSubCategoryStatus(int companyId, int categoryID, int subCategoryId, int status, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<ItemSubCategory> getStoreKeeperList(int companyID, DBConnection dbConnection) {
            throw new NotImplementedException();
        }

        public List<ItemSubCategory> getStoreKeeperHistory(int SubCategoryId, DBConnection dbConnection) {
            throw new NotImplementedException();
        }

        public List<ItemSubCategory> SearchSubCategoryList(int companyId, string text, DBConnection dbConnection) {
            throw new NotImplementedException();
        }
    }


    public class ItemSubCategoryDAOSQImpl : ItemSubCategoryDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
        public int SaveItemSubCategory(int companyId, int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM "+ dbLibrary + ".ITEM_SUB_CATEGORY WHERE  SUB_CATEGORY_NAME = '" + SubCategoryName + "' AND  CATEGORY_ID = " + CategoryId + " AND COMPANY_ID = "+companyId+"";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                //    dbConnection.cmd.Parameters.Clear();

                //    dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_SUB_CATEGORY\" ";
                //    var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                //    if (count == 0)
                //    {
                //        SubCategoryId = 001;
                //    }
                //    else
                //    {
                //        dbConnection.cmd.CommandText = "SELECT MAX (\"SUB_CATEGORY_ID\")+1 AS MAXid FROM public.\"ITEM_SUB_CATEGORY\" ";
                //        SubCategoryId = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                //    }
                dbConnection.cmd.CommandText = "INSERT INTO  " + dbLibrary + ".ITEM_SUB_CATEGORY (COMPANY_ID,SUB_CATEGORY_ID , SUB_CATEGORY_NAME , CATEGORY_ID , CREATED_DATETIME, CREATED_BY, UPDATED_DATETIME, UPDATED_BY, IS_ACTIVE) VALUES (  " + companyId + "," + SubCategoryId + ",'" + SubCategoryName + "'," + CategoryId + ",'" + CreatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDate + "','" + UpdatedBy + "' ," + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();

            }
            else
            {
                return -1;
            }
        }

        public int UpdateItemSubCategory(int companyId, int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ITEM_SUB_CATEGORY WHERE  SUB_CATEGORY_NAME = '" + SubCategoryName + "' AND  CATEGORY_ID = " + CategoryId + " AND SUB_CATEGORY_ID != " + SubCategoryId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ITEM_SUB_CATEGORY SET SUB_CATEGORY_NAME = '" + SubCategoryName + "' , CATEGORY_ID = " + CategoryId + ", CREATED_DATETIME = '" + CreatedDateTime + "', CREATED_BY = '" + CreatedBy + "', UPDATED_DATETIME = '" + UpdatedDate + "', UPDATED_BY = '" + UpdatedBy + "', IS_ACTIVE = " + IsActive + " WHERE SUB_CATEGORY_ID = " + SubCategoryId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int DeleteItemSubCategory(int companyId, int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ITEM_SUB_CATEGORY SET  IS_ACTIVE= 0 WHERE SUB_CATEGORY_ID = " + SubCategoryId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemSubCategory> FetchItemSubCategoryList(int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT DISTINCT A.SUB_CATEGORY_ID, A.SUB_CATEGORY_NAME,A.CATEGORY_ID,B.CATEGORY_NAME, A.CREATED_DATETIME,A.CREATED_BY,A.UPDATED_DATETIME,A.UPDATED_BY,A.IS_ACTIVE " +
                "FROM "+dbLibrary+".ITEM_SUB_CATEGORY AS A "+
                "INNER JOIN (SELECT CATEGORY_ID, CATEGORY_NAME AS CATEGORY_NAME FROM dbo.ITEM_CATEGORY) AS B ON A.CATEGORY_ID = B.CATEGORY_ID "+
                "WHERE A.COMPANY_ID ="+companyId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategory>(dbConnection.dr);
            }
        }

        public List<ItemSubCategory> FetchItemSubCategoryById(int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_SUB_CATEGORY  WHERE SUB_CATEGORY_ID =" + SubCategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategory>(dbConnection.dr);
            }
        }

        public ItemSubCategory FetchItemSubCategoryListByIdObj(int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_SUB_CATEGORY  WHERE SUB_CATEGORY_ID =" + SubCategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemSubCategory>(dbConnection.dr);
            }
        }


        public List<ItemSubCategory> FetchItemSubCategoryListWithMainCategory(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_SUB_CATEGORY INNER JOIN " + dbLibrary + ".ITEM_CATEGORY ON " + dbLibrary + ".ITEM_CATEGORY.CATEGORY_ID = " + dbLibrary + ".ITEM_SUB_CATEGORY.CATEGORY_ID WHERE " + dbLibrary + ".ITEM_CATEGORY.IS_ACTIVE= 1 ORDER BY " + dbLibrary + ".ITEM_SUB_CATEGORY.SUB_CATEGORY_NAME";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategory>(dbConnection.dr);
            }
        }

        public List<ItemSubCategory> FetchItemSubCategoryByCategoryId(int CategoryId,int companyid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON (ISC.CATEGORY_ID = IC.CATEGORY_ID) WHERE ISC.CATEGORY_ID= " + CategoryId + " AND ISC.IS_ACTIVE = 1 AND ISC.COMPANY_ID = " + companyid+ " AND IC.COMPANY_ID = " + companyid + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategory>(dbConnection.dr);
            }
        }

        public int UpdateItemSubCategoryStatus(int companyId, int categoryID, int subCategoryId, int status, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ITEM_SUB_CATEGORY SET  IS_ACTIVE= "+ status + " WHERE SUB_CATEGORY_ID = " + subCategoryId + " AND CATEGORY_ID = "+categoryID+" AND COMPANY_ID = "+companyId+"";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemSubCategory> getStoreKeeperList(int companyID, DBConnection dbConnection) {

            List<ItemSubCategory> subCategory;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM ITEM_SUB_CATEGORY AS ISC " +
                                            "INNER JOIN(SELECT CATEGORY_ID, CATEGORY_NAME, COMPANY_ID FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = ISC.CATEGORY_ID " +
                                            "INNER JOIN (SELECT SUB_CATEGORY_ID FROM SUB_CATEGORY_STORE_KEEPER) AS SCSK ON SCSK.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID " +
                                            "WHERE IC.COMPANY_ID = " + companyID + " AND ISC.COMPANY_ID = " + companyID + "  " +
                                            "GROUP BY ISC.SUB_CATEGORY_ID, SUB_CATEGORY_NAME, ISC.CATEGORY_ID, CREATED_DATETIME, CREATED_BY,UPDATED_DATETIME, UPDATED_BY, IS_ACTIVE, ISC.COMPANY_ID, " +
                                                    "IC.CATEGORY_ID, IC.CATEGORY_NAME, IC.COMPANY_ID,  SCSK.SUB_CATEGORY_ID " +
                                            "ORDER BY CATEGORY_NAME ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                subCategory = dataAccessObject.ReadCollection<ItemSubCategory>(dbConnection.dr);
            }

            for (int i = 0; i < subCategory.Count; i++) {
                subCategory[i].UserName = string.Join(", ",
                    DAOFactory.createSubCategoryStoreKeeperDAO().FetchCurrentStoreKeeper(subCategory[i].SubCategoryId, dbConnection).Select(w => w.UserName));
            }

            return subCategory;
        }



        public List<ItemSubCategory> SearchSubCategoryList(int companyId, string text, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT DISTINCT A.SUB_CATEGORY_ID, A.SUB_CATEGORY_NAME,A.CATEGORY_ID,B.CATEGORY_NAME, A.CREATED_DATETIME,A.CREATED_BY,A.UPDATED_DATETIME,A.UPDATED_BY,A.IS_ACTIVE " +
                "FROM " + dbLibrary + ".ITEM_SUB_CATEGORY AS A " +
                "INNER JOIN (SELECT CATEGORY_ID, CATEGORY_NAME AS CATEGORY_NAME FROM dbo.ITEM_CATEGORY) AS B ON A.CATEGORY_ID = B.CATEGORY_ID " +
                "WHERE A.COMPANY_ID = " + companyId + " AND  A.SUB_CATEGORY_NAME LIKE '%"+ text + "%'";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemSubCategory>(dbConnection.dr);
            }
        }


    }
}
