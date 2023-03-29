using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface AddItemMasterDAO
    {
        int SaveItems(int CategoryId,int SubCategoryId, string ItemName,int IsActive, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy,string ReferenceNo,string HsId, string Model , string PartId,int measurementId, int ItemType, int stockMaintainingType, DBConnection dbConnection);
        //int UpdateItems(int ItemId, int CategoryId, int SubCategoryId, string ItemName, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int measurementid, DBConnection dbConnection);
        int UpdateItems(int ItemId, int CategoryId, int SubCategoryId, string ItemName, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int CompnayId, int measurementid, DBConnection dbConnection);
        int DeleteItems(int ItemId, DBConnection dbConnection);
        List<AddItemMaster> FetchItemList(DBConnection dbConnection);
        List<AddItemMaster> FetchItemListById(int ItemId, DBConnection dbConnection);
        AddItemMaster FetchItemListByIdObj(int ItemId, DBConnection dbConnection);
        List<AddItemMaster> FetchItemListDetailed(DBConnection dbConnection);
        int DeleteInActiveItems(int ItemId, int CategoryId, int SubCategoryId, DBConnection dbConnection);
        List<AddItemMaster> SearchedItemName(int MainCategoryId, int SubCategoryId, string ItemName, int companyid, DBConnection dbConnection);
        int GetIdByItemName( string ItemName, DBConnection dbConnection);
        List<AddItemMaster> FetchItemsByCategories(int MainCategoryId, int SubCategoryId, DBConnection dbConnection);
        AddItemMaster FetchItemObj(int ItemId, DBConnection dbConnection);
    }

   

    public class AddItemMasterDAOSQLImpl : AddItemMasterDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveItems(int CategoryId, int SubCategoryId, string ItemName, int IsActive, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo,string HsId, string Model , string PartId, int measurementId, int ItemType,int stockMaintainingType, DBConnection dbConnection)
        {
            int ItemId = 0;


            //dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM "+ dbLibrary + ".ADD_ITEMS_MASTER WHERE  REFERENCE_NO = '" + ReferenceNo + "' ";
            //var Refnumber = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            //if (Refnumber == 0)
            //{

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ADD_ITEMS_MASTER WHERE CATEGORY_ID = " + CategoryId + " AND  SUB_CATEGORY_ID = " + SubCategoryId + " AND  ITEM_NAME = '" + ItemName + "' ";
            var countExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ADD_ITEMS_MASTER";
                var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    ItemId = 001;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (ITEM_ID)+1 AS MAXid FROM " + dbLibrary + ".ADD_ITEMS_MASTER";
                    ItemId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".ADD_ITEMS_MASTER (ITEM_ID, CATEGORY_ID, SUB_CATEGORY_ID, ITEM_NAME, IS_ACTIVE, CREATED_DATETIME, CREATED_BY, UPDATED_DATETIME, UPDATED_BY,REFERENCE_NO,HS_ID,MODEL,PART_ID, MEASUREMENT_ID, ITEM_TYPE,STOCK_MAINTAINING_TYPE) VALUES ( " + ItemId + ", " + CategoryId + " , " + SubCategoryId + ", '" + ItemName + "', " + IsActive + ", '" + CreatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDateTime + "', '" + UpdatedBy + "','" + ReferenceNo + "','" + HsId + "','" + Model + "','" + PartId + "', " + measurementId + ", " + ItemType + ", "+ stockMaintainingType + ");";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.ExecuteNonQuery();

                return ItemId;

            }

            else
            {
                return -1;
            }
            //}
            //else
            //{
            //    return -2;
            //}


        }

        public int UpdateItems(int ItemId, int CategoryId, int SubCategoryId, string ItemName, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int measurementid, DBConnection dbConnection)
        {
            //dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".\"ADD_ITEMS_MASTER\" WHERE \"REFERENCE_NO\" = '" + ReferenceNo + "' AND \"ITEM_ID\" != " + ItemId + " ";
            //var Refnumber = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            //if (Refnumber == 0)
            //{

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".\"ADD_ITEMS_MASTER\" WHERE \"CATEGORY_ID\" = " + CategoryId + " AND  \"SUB_CATEGORY_ID\" = " + SubCategoryId + " AND  \"ITEM_ID\" != " + ItemId + " AND \"ITEM_NAME\" = '" + ItemName + "'";
            var countExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".\"ADD_ITEMS_MASTER\" SET \"CATEGORY_ID\" = " + CategoryId + ", \"SUB_CATEGORY_ID\" = " + SubCategoryId + " , \"ITEM_NAME\" = '" + ItemName + "', \"CREATED_DATETIME\" = '" + CreatedDateTime + "', \"CREATED_BY\" = '" + CreatedBy + "', \"UPDATED_DATETIME\" = '" + UpdatedDateTime + "', \"UPDATED_BY\" = '" + UpdatedBy + "', \"REFERENCE_NO\" = '" + ReferenceNo + "'  WHERE \"ITEM_ID\" = " + ItemId + ";";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;  // item name exist
            }
            //}
            //else
            //{
            //    return -2; // reference no exist
            //}
        }

        public int DeleteItems(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".ADD_ITEMS_MASTER WHERE ITEM_ID= " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<AddItemMaster> FetchItemList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS_MASTER ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItemMaster>(dbConnection.dr);
            }
        }

        public List<AddItemMaster> FetchItemListById(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS_MASTER  WHERE ITEM_ID = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItemMaster>(dbConnection.dr);
            }
        }

        public AddItemMaster FetchItemListByIdObj(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS_MASTER  WHERE ITEM_ID = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<AddItemMaster>(dbConnection.dr);
            }
        }

        public List<AddItemMaster> FetchItemListDetailed(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".\"ADD_ITEMS_MASTER\" " +
                "INNER JOIN " + dbLibrary + ".\"ITEM_CATEGORY\" ON " + dbLibrary + ".\"ITEM_CATEGORY\".\"CATEGORY_ID\" = " + dbLibrary + ".\"ADD_ITEMS\".\"CATEGORY_ID\" " +
                "INNER JOIN " + dbLibrary + ".\"ITEM_IMAGE_UPLOAD\" ON " + dbLibrary + ".\"ITEM_IMAGE_UPLOAD\".\"ITEM_ID\" = " + dbLibrary + ".\"ADD_ITEMS\".\"ITEM_ID\" " +
                " INNER JOIN " + dbLibrary + ".\"ITEM_SUB_CATEGORY\" ON " + dbLibrary + ".\"ITEM_SUB_CATEGORY\".\"SUB_CATEGORY_ID\" = " + dbLibrary + ".\"ADD_ITEMS\".\"SUB_CATEGORY_ID\" AND " + dbLibrary + ".\"ITEM_SUB_CATEGORY\".\"CATEGORY_ID\" = " + dbLibrary + ".\"ITEM_CATEGORY\".\"CATEGORY_ID\" AND " + dbLibrary + ".\"ITEM_SUB_CATEGORY\".\"IS_ACTIVE\" =1" +
                " WHERE  " + dbLibrary + ".\"ITEM_SUB_CATEGORY\".\"IS_ACTIVE\" = 1 AND " + dbLibrary + ".\"ITEM_CATEGORY\".\"IS_ACTIVE\" = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItemMaster>(dbConnection.dr);
            }
        }

        public int DeleteInActiveItems(int ItemId, int CategoryId, int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".\"ADD_ITEMS_MASTER\" SET \"IS_ACTIVE\" = " + 0 + " WHERE  \"SUB_CATEGORY_ID\" = " + SubCategoryId + " AND  \"ITEM_ID\" = " + ItemId + "  AND  \"CATEGORY_ID\" = " + CategoryId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int GetIdByItemName(string ItemName, DBConnection dbConnection)
        {
            int ItemId = 0;

            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT \"ITEM_ID\" FROM " + dbLibrary + ".\"ADD_ITEMS_MASTER\" WHERE \"ITEM_NAME\"='" + ItemName + "'";
            ItemId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return ItemId;
            }
        }

        public List<AddItemMaster> SearchedItemName(int MainCategoryId, int SubCategoryId, string ItemName,int companyid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();


            //dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".ADD_ITEMS_MASTER  WHERE LOWER( ITEM_NAME ) LIKE '" + "%" + ItemName.ToLower() + "%" + "' AND ITEM_ID NOT IN (SELECT ITEM_ID FROM ADD_ITEMS WHERE COMPANY_ID = " + companyid + ") AND CATEGORY_ID = "+MainCategoryId+" AND SUB_CATEGORY_ID = "+SubCategoryId+" ORDER BY ITEM_NAME ";

           dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS_MASTER AS IM LEFT JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON ( IM.ITEM_ID = AI.ITEM_ID) WHERE LOWER ( IM.ITEM_NAME  ) LIKE '" + "%" + ItemName.ToLower() + "%" + "' AND IM.CATEGORY_ID = " + MainCategoryId + " AND IM.SUB_CATEGORY_ID = " + SubCategoryId + " ORDER BY IM.ITEM_NAME;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItemMaster>(dbConnection.dr);
            }
        }

        public List<AddItemMaster> FetchItemsByCategories(int MainCategoryId, int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS_MASTER  WHERE CATEGORY_ID = " + MainCategoryId + " AND SUB_CATEGORY_ID = " + SubCategoryId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItemMaster>(dbConnection.dr);
            }
        }

        public AddItemMaster FetchItemObj(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS_MASTER AS AI LEFT JOIN ITEM_IMAGE_UPLOAD AS ITU ON AI.ITEM_ID = ITU.ITEM_ID  WHERE AI.ITEM_ID = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<AddItemMaster>(dbConnection.dr);
            }
        }

        
        public int UpdateItems(int ItemId, int CategoryId, int SubCategoryId, string ItemName, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int CompnayId, int measurementid, DBConnection dbConnection) {
            //dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ADD_ITEMS WHERE  COMPANY_ID = " + CompnayId + " AND REFERENCE_NO = '" + ReferenceNo + "' AND ITEM_ID != " + ItemId + " ";
            //var Refnumber = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            //if (Refnumber == 0) {

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ADD_ITEMS WHERE CATEGORY_ID = " + CategoryId + " AND  SUB_CATEGORY_ID = " + SubCategoryId + " AND  ITEM_ID != " + ItemId + " AND ITEM_NAME = '" + ItemName + "' AND COMPANY_ID=" + CompnayId + "";
                var countExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (countExist == 0) {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ADD_ITEMS_MASTER  SET CATEGORY_ID = " + CategoryId + ", SUB_CATEGORY_ID = " + SubCategoryId + " , ITEM_NAME = '" + ItemName + "', CREATED_DATETIME = '" + CreatedDateTime + "', CREATED_BY = '" + CreatedBy + "', UPDATED_DATETIME = '" + UpdatedDateTime + "', UPDATED_BY = '" + UpdatedBy + "', REFERENCE_NO = '" + ReferenceNo + "' , MEASUREMENT_ID = " + measurementid + " WHERE ITEM_ID = " + ItemId + ";";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                    return dbConnection.cmd.ExecuteNonQuery();
                }
                else {
                    return -1;  // item name exist
                }
            //}
            //else {
            //    return -2; // reference no exist
            //}
        }
    }




}
