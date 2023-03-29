using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using System.Data;

namespace CLibrary.Infrastructure
{
    public interface ItemCategoryOwnerDAO
    {
        List<ItemCategoryOwners> FetchItemCategoryOwnersByCategoryId(int categoryId, DBConnection dbConnection);
        ItemCategoryOwners FetchItemCategoryOwnersByCategoryId(int categoryId, string ownerType, DBConnection dbConnection);
        int SaveItemCategoryOwners2(int id, int categoryId, string ownerType, List<int> UserIds, DateTime effectiveDate, int userId2, DateTime now, DBConnection dbConnection);
        int SaveItemCategoryOwners(int id , int categoryId, string ownerType, int userId1, DateTime effectiveDate, int userId2, DateTime now, DBConnection dbConnection);
        int UpdateItemCategoryOwners(int id, int categoryId, string ownerType, int userId1, DateTime effectiveDate, int userId2, DateTime now, DBConnection dbConnection);
        int DeleteItemCategoryOwners(int id, DBConnection dbConnection);
        List<ItemCategoryOwners> FetchAllItemCategoryOwners(string ownerType, DBConnection dbConnection);
        CompanyLogin GetCurrentPurchasingOfficer(int CategoryId, DBConnection dbConnection);
    }

   

    public class ItemCategoryOwnerDAOImpl : ItemCategoryOwnerDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int DeleteItemCategoryOwners(int id, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".ITEM_CATEGORY_OWNERS " +
                                            " WHERE ID =" + id + " ";

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemCategoryOwners> FetchItemCategoryOwnersByCategoryId(int categoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            string now = LocalTime.Now.ToString("yyyy-MM-dd");
            dbConnection.cmd.CommandText = " select * from " + dbLibrary + ".ITEM_CATEGORY_OWNERS a " +
                                           " where a.CATEGORY_ID = " + categoryId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryOwners>(dbConnection.dr);
            }
        }

        public ItemCategoryOwners FetchItemCategoryOwnersByCategoryId(int categoryId, string ownerType, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " select * from " + dbLibrary + ".ITEM_CATEGORY_OWNERS a " + 
                                           " where a.CATEGORY_ID = " + categoryId + " " +
                                           " and a.OWNER_TYPE = '" + ownerType + "' "+ 
                                           " and a.effective_date =  (select max(effective_date) from ITEM_CATEGORY_OWNERS b "+
                                           " where b.category_id = a.category_id and a.owner_type = b.owner_type " +
                                           " AND effective_date <= '" +  LocalTime.Now + "' )";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemCategoryOwners>(dbConnection.dr);
            }
        }
        public int SaveItemCategoryOwners2(int id, int categoryId, string ownerType, List<int> UserIds, DateTime effectiveDate, int userId2, DateTime now, DBConnection dbConnection) {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) as cnt FROM " + dbLibrary + ".ITEM_CATEGORY_OWNERS" +
                                          " WHERE CATEGORY_ID =" + categoryId + " " +
                                          " AND OWNER_TYPE = '" + ownerType + "' " +
                                          " AND EFFECTIVE_DATE = '" + effectiveDate + "'";
            if (decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString()) != 1) {
                //dbConnection.cmd.CommandText = "DECLARE @CATEGORY_IDS TABLE(CATEGORY_ID INT) \n";
                //dbConnection.cmd.CommandText += "INSERT INTO " + dbLibrary + ".ITEM_CATEGORY_OWNERS (CATEGORY_ID,OWNER_TYPE,EFFECTIVE_DATE,CREATED_USER,CREATED_DATE)" +
                //                               " OUTPUT INSERTED.CATEGORY_ID INTO @CATEGORY_IDS VALUES (" + categoryId + ", '" + ownerType + "','" + effectiveDate + "'," + userId2 + " , '" + effectiveDate + "')";

                for (int i = 0; i < UserIds.Count; i++) {
                   
                        dbConnection.cmd.CommandText += "INSERT INTO [ITEM_CATEGORY_OWNERS2]([CATEGORY_ID],[OWNER_TYPE],[USER_ID],[EFFECTIVE_DATE],[CREATED_USER],[CREATED_DATE]) VALUES(" + categoryId + ", '" + ownerType + "', " + UserIds[i] + ",'" + effectiveDate + "', " + userId2 + ", '" + LocalTime.Now + "'); \n";
                 
                }
            }
            else {
                return -1;
            }
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int SaveItemCategoryOwners(int id ,int categoryId, string ownerType, int userId1, DateTime effectiveDate, int userId2, DateTime now, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) as cnt FROM " + dbLibrary + ".ITEM_CATEGORY_OWNERS" +
                                          " WHERE CATEGORY_ID =" + categoryId + " " +
                                          " AND OWNER_TYPE = '" + ownerType + "' " +
                                          " AND EFFECTIVE_DATE = '" + effectiveDate + "'";
            if (decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString()) != 1)
            {

                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".ITEM_CATEGORY_OWNERS (CATEGORY_ID,OWNER_TYPE,USER_ID,EFFECTIVE_DATE,CREATED_USER,CREATED_DATE)" +
                                               " VALUES (" + categoryId + ", '" + ownerType + "', " + userId1 + " ,'" + effectiveDate + "'," + userId2 + " , '" + effectiveDate + "')";
                
            }else
            {
                return -1;
            }
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateItemCategoryOwners(int id, int categoryId, string ownerType, int userId1, DateTime effectiveDate, int userId2, DateTime now, DBConnection dbConnection)
        {
            //dbConnection.cmd.CommandText = "SELECT COUNT(*) as cnt FROM " + dbLibrary + ".ITEM_CATEGORY_OWNERS" +
            //                             " WHERE CATEGORY_ID =" + categoryId + " " +
            //                             " AND OWNER_TYPE = '" + ownerType + "' " +
            //                             " AND EFFECTIVE_DATE = '" + effectiveDate + "'";
            //if (decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString()) == 1)
            //{
            //    // if found
            //    return -1;
            //}
            //else
            //{
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ITEM_CATEGORY_OWNERS " +
                                            " SET USER_ID = " + userId1 + " " +
                                            " , EFFECTIVE_DATE = '" + effectiveDate + " ' " +
                                            " , CREATED_USER= " + userId2 + " " +
                                            " , CREATED_DATE ='" + now + "' " +
                                            ", OWNER_TYPE ='"+ ownerType + "'"+
                                            " WHERE ID =" + id + " ";

                return dbConnection.cmd.ExecuteNonQuery();
           // }
        }

        public List<ItemCategoryOwners> FetchAllItemCategoryOwners(string ownerType, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " select distinct a.USER_ID,(SELECT FIRST_NAME FROM " + dbLibrary + ".COMPANY_LOGIN WHERE USER_ID=a.USER_ID ) as FIRST_NAME from " + dbLibrary + ".ITEM_CATEGORY_OWNERS a " +
                                           " where a.OWNER_TYPE = '" + ownerType + "' " +
                                           " and a.effective_date =  (select max(effective_date) from ITEM_CATEGORY_OWNERS b " +
                                           " where b.category_id = a.category_id and a.owner_type = b.owner_type " +
                                           " AND effective_date <= '" +  LocalTime.Now + "' )";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryOwners>(dbConnection.dr);
            }
        }

        public CompanyLogin GetCurrentPurchasingOfficer(int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT TOP 1 ICO.USER_ID,CL.FIRST_NAME FROM ITEM_CATEGORY_OWNERS2 AS ICO\n" + 
                "INNER JOIN (SELECT USER_ID, FIRST_NAME, CONTACT_NO FROM COMPANY_LOGIN) AS CL ON CL.USER_ID= ICO.USER_ID\n"+
                "WHERE ICO.CATEGORY_ID=" + CategoryId + " AND ICO.OWNER_TYPE='PO' AND ICO.EFFECTIVE_DATE <= '" +  LocalTime.Now + "' ORDER BY ICO.EFFECTIVE_DATE DESC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CompanyLogin>(dbConnection.dr);
            }
        }
    }
}

