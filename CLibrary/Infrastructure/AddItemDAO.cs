using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface AddItemDAO
    {
        int SaveItems(int itemid,int CategoryId,int SubCategoryId, string ItemName,int IsActive, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy,string ReferenceNo, int CompnayId,string HsId, string Model , string PartId, int measurementid, int ItemType,int stockMaintainingType, DBConnection dbConnection);
        int UpdateItems(int ItemId, int CategoryId, int SubCategoryId, string ItemName, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int CompnayId, int measurementid, string hsId, string model, string partId, int ItemType, int ReorderLevel, int stockMaintainingType, DBConnection dbConnection);
        int DeleteItems(int ItemId, DBConnection dbConnection);
        List<AddItem> FetchItemList(DBConnection dbConnection);
        List<AddItem> FetchItemListById(int ItemId, DBConnection dbConnection);
        AddItem FetchItemListByIdObj(int ItemId, DBConnection dbConnection);
        List<AddItem> FetchItemListDetailed(int companyid, DBConnection dbConnection);
        int DeleteInActiveItems(int ItemId, int CategoryId, int SubCategoryId, DBConnection dbConnection);
        List<AddItem> SearchedItemName(int MainCategoryId, int SubCategoryId, int CompanyId, string ItemName, DBConnection dbConnection);
        int GetIdByItemName(int CompanyId, string ItemName, DBConnection dbConnection);
        List<AddItem> FetchItemsByCategories(int MainCategoryId, int SubCategoryId, int CompanyId, DBConnection dbConnection);
        AddItem FetchItemObj(int ItemId, DBConnection dbConnection);
        int UpdateItemStatus(int companyId, int CategoryId, int subCategoryId, int itemid, int status, DBConnection dbConnection);
        AddItem FetchItemByItemId(int ItemId, DBConnection dbConnection);
        List<AddItem> ItemsInInventoryByCatagory(int MainCategoryId, int SubCategoryId, int level, DBConnection dbConnection);
        List<AddItem> GetItemsForMrnAndPr(int MainCategoryId, int SubCategoryId, int ItemType, int companyId, DBConnection dbConnection);
        List<AddItem> FetchItemListDetailedFilter(int companyid, int mainCatId, int subCatId, DBConnection dbConnection);
        List<AddItem> FetchItemByItemName(int companyid, string ItemName, DBConnection dbConnection);
        List<AddItem> FetchItemByItemCode(int companyid, string ItemCode, DBConnection dbConnection);

        int GetMeasurementIdOfItem(int ItemId, int CompanyId, DBConnection dbConnection);
        AddItem FetchItemByItemId(int ItemId, int companyid, DBConnection dbConnection);
        int GetStockMaintaininType(int ItemId, int CompanyId, DBConnection dbConnection);
    }

    public class AddItemDAOImpl : AddItemDAO
    {
        public int SaveItems(int itemid,int CategoryId, int SubCategoryId, string ItemName, int IsActive, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int CompnayId,string HsId, string Model , string PartId, int measurementid, int ItemType,int stockMaintainingType, DBConnection dbConnection)
        {
            int ItemId = 0;

            
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ADD_ITEMS\" WHERE  \"COMPANY_ID\" = " + CompnayId + " AND \"REFERENCE_NO\" = '"+ReferenceNo+"' ";
            var Refnumber = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if(Refnumber == 0){

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ADD_ITEMS\" WHERE \"CATEGORY_ID\" = " + CategoryId + " AND  \"SUB_CATEGORY_ID\" = " + SubCategoryId + " AND  \"ITEM_NAME\" = '" + ItemName + "' AND  \"COMPANY_ID\" = " + CompnayId + " ";
                var countExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (countExist == 0)
                {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ADD_ITEMS\"";
                var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    ItemId = 001;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (\"ITEM_ID\")+1 AS MAXid FROM public.\"ADD_ITEMS\"";
                    ItemId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO public.\"ADD_ITEMS\" (\"ITEM_ID\", \"CATEGORY_ID\", \"SUB_CATEGORY_ID\", \"ITEM_NAME\", \"IS_ACTIVE\", \"CREATED_DATETIME\", \"CREATED_BY\", \"UPDATED_DATETIME\", \"UPDATED_BY\",\"REFERENCE_NO\",\"COMPANY_ID\") VALUES ( " + ItemId + ", " + CategoryId + " , " + SubCategoryId + ", '" + ItemName + "', " + IsActive + ", '" + CreatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDateTime + "', '" + UpdatedBy + "','" + ReferenceNo + "', " + CompnayId + " );";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.ExecuteNonQuery();

                return ItemId;

                }

                else
                {
                    return -1;
                }
            }
            else{
                return -2;
            }
            
            
        }

        public int UpdateItems(int ItemId, int CategoryId, int SubCategoryId, string ItemName, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int CompnayId, int measurementid, string hsId, string model, string partId, int ItemType, int ReorderLevel, int stockMaintainingType, DBConnection dbConnection)
        {
             dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ADD_ITEMS\" WHERE  \"COMPANY_ID\" = " + CompnayId + " AND \"REFERENCE_NO\" = '"+ReferenceNo+"' AND \"ITEM_ID\" != " + ItemId + " ";
            var Refnumber = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (Refnumber == 0)
            {

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ADD_ITEMS\" WHERE \"CATEGORY_ID\" = " + CategoryId + " AND  \"SUB_CATEGORY_ID\" = " + SubCategoryId + " AND  \"ITEM_ID\" != " + ItemId + " AND \"ITEM_NAME\" = '" + ItemName + "' AND \"COMPANY_ID\"=" + CompnayId + "";
                var countExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (countExist == 0)
                {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = "UPDATE public.\"ADD_ITEMS\" SET \"CATEGORY_ID\" = " + CategoryId + ", \"SUB_CATEGORY_ID\" = " + SubCategoryId + " , \"ITEM_NAME\" = '" + ItemName + "', \"CREATED_DATETIME\" = '" + CreatedDateTime + "', \"CREATED_BY\" = '" + CreatedBy + "', \"UPDATED_DATETIME\" = '" + UpdatedDateTime + "', \"UPDATED_BY\" = '" + UpdatedBy + "', \"REFERENCE_NO\" = '" + ReferenceNo + "'  WHERE \"ITEM_ID\" = " + ItemId + ";";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                    return dbConnection.cmd.ExecuteNonQuery();
                }
                else
                {
                    return -1;  // item name exist
                }
            }
            else
            {
                return -2; // reference no exist
            }
        }

        public int DeleteItems(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM public.\"ADD_ITEMS\"  WHERE \"ITEM_ID\" = "+ItemId+";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<AddItem> FetchItemList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ADD_ITEMS\" ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public List<AddItem> FetchItemListById(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ADD_ITEMS\"  WHERE \"ITEM_ID\" = "+ItemId+";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public AddItem FetchItemListByIdObj(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ADD_ITEMS\"  WHERE \"ITEM_ID\" = "+ItemId+";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<AddItem>(dbConnection.dr);
            }
        }

        public List<AddItem> FetchItemListDetailed(int companyid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ADD_ITEMS\" " +
                "INNER JOIN public.\"ITEM_CATEGORY\" ON public.\"ITEM_CATEGORY\".\"CATEGORY_ID\" = public.\"ADD_ITEMS\".\"CATEGORY_ID\" " +
                "INNER JOIN public.\"ITEM_IMAGE_UPLOAD\" ON public.\"ITEM_IMAGE_UPLOAD\".\"ITEM_ID\" = public.\"ADD_ITEMS\".\"ITEM_ID\" " +
                " INNER JOIN public.\"ITEM_SUB_CATEGORY\" ON public.\"ITEM_SUB_CATEGORY\".\"SUB_CATEGORY_ID\" = public.\"ADD_ITEMS\".\"SUB_CATEGORY_ID\" AND public.\"ITEM_SUB_CATEGORY\".\"CATEGORY_ID\" = public.\"ITEM_CATEGORY\".\"CATEGORY_ID\" AND public.\"ITEM_SUB_CATEGORY\".\"IS_ACTIVE\" =1" +
                " WHERE  public.\"ITEM_SUB_CATEGORY\".\"IS_ACTIVE\" = 1 AND public.\"ITEM_CATEGORY\".\"IS_ACTIVE\" = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public int DeleteInActiveItems(int ItemId, int CategoryId, int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE public.\"ADD_ITEMS\" SET \"IS_ACTIVE\" = " + 0 + " WHERE  \"SUB_CATEGORY_ID\" = " + SubCategoryId + " AND  \"ITEM_ID\" = " + ItemId + "  AND  \"CATEGORY_ID\" = " + CategoryId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int GetIdByItemName(int CompanyId, string ItemName, DBConnection dbConnection)
        {
            int ItemId = 0;

            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT \"ITEM_ID\" FROM public.\"ADD_ITEMS\" WHERE \"COMPANY_ID\"=" + CompanyId + " AND \"ITEM_NAME\"='"+ItemName+"'";
            ItemId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return ItemId;
            }
        }

        public List<AddItem> SearchedItemName(int MainCategoryId, int SubCategoryId, int CompanyId, string ItemName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ADD_ITEMS\"  WHERE \"CATEGORY_ID\" = " + MainCategoryId + " AND \"SUB_CATEGORY_ID\"=" + SubCategoryId + " AND \"COMPANY_ID\"=" + CompanyId + " AND \"ITEM_NAME\" LIKE '%" + ItemName + "%';";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public List<AddItem> FetchItemsByCategories(int MainCategoryId, int SubCategoryId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ADD_ITEMS\"  WHERE \"CATEGORY_ID\" = " + MainCategoryId + " AND \"SUB_CATEGORY_ID\" = " + SubCategoryId + " AND \"COMPANY_ID\" = " + CompanyId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public AddItem FetchItemObj(int ItemId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int UpdateItemStatus(int companyId, int CategoryId, int subCategoryId, int itemid, int status, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<AddItem> ItemsInInventoryByCatagory(int MainCategoryId, int SubCategoryId, int level, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            string query = "SELECT W.LOCATION AS CREATED_BY, B.AVAILABLE_QTY AS IS_ACTIVE, B.ITEM_NAME AS UPDATED_BY, c.REORDER_LEVEL AS COMPANY_ID " +
 " FROM  (SELECT M.WAREHOUSE_ID, M.AVAILABLE_QTY, ADD_ITEMS.ITEM_ID, ADD_ITEMS.CATEGORY_ID, ADD_ITEMS.SUB_CATEGORY_ID, ADD_ITEMS.ITEM_NAME, ADD_ITEMS.COMPANY_ID " +
 "  FROM  ADD_ITEMS INNER JOIN WAREHOUSE_INVENTORY_MASTER AS M ON ADD_ITEMS.ITEM_ID = M.ITEM_ID " +
 "  WHERE      ";

            if (SubCategoryId != null)
            {
                query += "(ADD_ITEMS.SUB_CATEGORY_ID =  " + SubCategoryId + ")";
            }
            else
            {
                query += "(ADD_ITEMS.CATEGORY_ID =  " + MainCategoryId + ")";
            }

            query += ") AS B INNER JOIN " +
            " COMPANY_INVENTORY_MASTER AS c ON B.COMPANY_ID = c.COMPANY_ID AND B.ITEM_ID = c.ITEM_ID INNER JOIN " +
            " WAREHOUSE AS W ON B.WAREHOUSE_ID = W.WAREHOUSE_ID ";

            query += "WHERE (c.REORDER_LEVEL = " + level + " )";

            dbConnection.cmd.CommandText = query;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public AddItem FetchItemByItemId(int ItemId, DBConnection dbConnection) {
            throw new NotImplementedException();
        }

        public List<AddItem> GetItemsForMrnAndPr(int MainCategoryId, int SubCategoryId, int ItemType, int companyId, DBConnection dbConnection) {
            throw new NotImplementedException();
        }

        public List<AddItem> FetchItemListDetailedFilter(int companyid, int mainCatId, int subCatId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<AddItem> FetchItemByItemName(int companyid, string ItemName, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<AddItem> FetchItemByItemCode(int companyid, string ItemCode, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int GetMeasurementIdOfItem(int ItemId, int CompanyId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public AddItem FetchItemByItemId(int ItemId, int companyid, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int GetStockMaintaininType(int ItemId, int CompanyId, DBConnection dbConnection) {
            throw new NotImplementedException();
        }
    }

    public class AddItemsSQLImpl : AddItemDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveItems(int itemid, int CategoryId, int SubCategoryId, string ItemName, int IsActive, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int CompnayId,string HsId, string Model , string PartId, int measurementid, int ItemType, int stockMaintainingType,DBConnection dbConnection)
        {
          
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM "+ dbLibrary +".ADD_ITEMS  WHERE  COMPANY_ID= " + CompnayId + " AND REFERENCE_NO = '" + ReferenceNo + "' ";
            var Refnumber = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (Refnumber == 0)
            {

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + " .ADD_ITEMS WHERE CATEGORY_ID = " + CategoryId + " AND  SUB_CATEGORY_ID = " + SubCategoryId + " AND  ITEM_NAME = '" + ItemName + "' AND  COMPANY_ID = " + CompnayId + " ";
                var countExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (countExist == 0)
                {

                    dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + " .ADD_ITEMS (ITEM_ID, CATEGORY_ID, SUB_CATEGORY_ID, ITEM_NAME, IS_ACTIVE, CREATED_DATETIME, CREATED_BY, UPDATED_DATETIME, UPDATED_BY,REFERENCE_NO,COMPANY_ID,HS_ID,MODEL,PART_ID, MEASUREMENT_ID,ITEM_TYPE,STOCK_MAINTAINING_TYPE) VALUES ( " + itemid + ", " + CategoryId + " , " + SubCategoryId + ", '" + ItemName + "', " + IsActive + ", '" + CreatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDateTime + "', '" + UpdatedBy + "','" + ReferenceNo + "', " + CompnayId + " ,'" + HsId + "','" + Model + "','" + PartId + "', " + measurementid + ", " + ItemType + ","+ stockMaintainingType + ");";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    dbConnection.cmd.ExecuteNonQuery();

                    return itemid;

                }

                else
                {
                    return -1;
                }
            }
            else
            {
                return -2;
            }


        }

        public int UpdateItems(int ItemId, int CategoryId, int SubCategoryId, string ItemName, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int CompnayId, int measurementid, string hsId, string model, string partId, int ItemType, int ReorderLevel, int stockMaintainingType, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ADD_ITEMS WHERE  COMPANY_ID = " + CompnayId + " AND REFERENCE_NO = '" + ReferenceNo + "' AND ITEM_ID != " + ItemId + " ";
            var Refnumber = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (Refnumber == 0)
            {

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ADD_ITEMS WHERE CATEGORY_ID = " + CategoryId + " AND  SUB_CATEGORY_ID = " + SubCategoryId + " AND  ITEM_ID != " + ItemId + " AND ITEM_NAME = '" + ItemName + "' AND COMPANY_ID=" + CompnayId + "";
                var countExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (countExist == 0)
                {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ADD_ITEMS  SET CATEGORY_ID = " + CategoryId + ", SUB_CATEGORY_ID = " + SubCategoryId + " , ITEM_NAME = '" + ItemName + "', CREATED_DATETIME = '" + CreatedDateTime + "', CREATED_BY = '" + CreatedBy + "', UPDATED_DATETIME = '" + UpdatedDateTime + "', UPDATED_BY = '" + UpdatedBy + "', REFERENCE_NO = '" + ReferenceNo + "' , MEASUREMENT_ID = " + measurementid + ",  HS_ID = '" + hsId + "',  MODEL = '" + model + "',  PART_ID = '" + partId + "',  ITEM_TYPE = " + ItemType + ", STOCK_MAINTAINING_TYPE =" + stockMaintainingType+"  WHERE ITEM_ID = " + ItemId + ";";
                    dbConnection.cmd.CommandText += "UPDATE " + dbLibrary + ".ADD_ITEMS_MASTER  SET CATEGORY_ID = " + CategoryId + ", SUB_CATEGORY_ID = " + SubCategoryId + " , ITEM_NAME = '" + ItemName + "', CREATED_DATETIME = '" + CreatedDateTime + "', CREATED_BY = '" + CreatedBy + "', UPDATED_DATETIME = '" + UpdatedDateTime + "', UPDATED_BY = '" + UpdatedBy + "', REFERENCE_NO = '" + ReferenceNo + "' , MEASUREMENT_ID = " + measurementid + ",  HS_ID = '" + hsId + "',  MODEL = '" + model + "',  PART_ID = '" + partId + "',  ITEM_TYPE = " + ItemType + " WHERE ITEM_ID = " + ItemId + ";";
                    dbConnection.cmd.CommandText += "UPDATE " + dbLibrary + ".COMPANY_INVENTORY_MASTER  SET REORDER_LEVEL = " + ReorderLevel + " WHERE ITEM_ID = " + ItemId + ";";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                    return dbConnection.cmd.ExecuteNonQuery();
                }
                else
                {
                    return -1;  // item name exist
                }
            }
            else
            {
                return -2; // reference no exist
            }
        }

        public int DeleteItems(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".ADD_ITEMS  WHERE ITEM_ID = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<AddItem> FetchItemList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM "+dbLibrary+".ADD_ITEMS" ;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public List<AddItem> FetchItemListById(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS  WHERE ITEM_ID = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public AddItem FetchItemListByIdObj(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM "+ dbLibrary + ".ADD_ITEMS  WHERE ITEM_ID = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<AddItem>(dbConnection.dr);
            }
        }

        public AddItem FetchItemByItemId(int ItemId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT AI.*, IC.CATEGORY_NAME,ISC.SUB_CATEGORY_NAME, CIM.REORDER_LEVEL, IMG.ITEM_MASTER_IMAGE_PATH FROM ADD_ITEMS AS AI " +
            "LEFT JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = AI.CATEGORY_ID " +
            "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS ISC ON ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID " +
            "LEFT JOIN(SELECT MEASUREMENT_ID, MEASUREMENT_SHORT_NAME FROM UNIT_MEASUREMENT) AS UM ON UM.MEASUREMENT_ID = AI.MEASUREMENT_ID " +
            "LEFT JOIN(SELECT HS_ID, HS_NAME FROM HS_CODE) AS HS ON HS.HS_ID = AI.HS_ID "+
            "LEFT JOIN(SELECT ITEM_ID, REORDER_LEVEL FROM COMPANY_INVENTORY_MASTER) AS CIM ON CIM.ITEM_ID = AI.ITEM_ID " +
            "LEFT JOIN(SELECT ITEM_MASTER_ID, ITEM_MASTER_IMAGE_PATH FROM ITEM_MASTER_IMAGE_UPLOAD) AS IMG ON IMG.ITEM_MASTER_ID = AI.ITEM_ID " +
            "WHERE AI.ITEM_ID = " + ItemId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<AddItem>(dbConnection.dr);
            }
        }

        public List<AddItem> FetchItemListDetailed(int companyid,DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT DISTINCT * FROM " + dbLibrary + ".ADD_ITEMS AS A " +
                                            "INNER JOIN (SELECT CATEGORY_ID AS CAT, CATEGORY_NAME, IS_ACTIVE FROM " + dbLibrary + ".ITEM_CATEGORY) AS B " +
                                            "ON A.CATEGORY_ID = B.CAT " +
                                            "INNER JOIN (SELECT SUB_CATEGORY_ID AS SUB, SUB_CATEGORY_NAME, IS_ACTIVE FROM " + dbLibrary + ".ITEM_SUB_CATEGORY) AS C " +
                                            "ON A.SUB_CATEGORY_ID = C.SUB " +
                                            "LEFT JOIN (SELECT ITEM_MASTER_ID,ITEM_MASTER_IMAGE_PATH as IMAGE_PATH FROM " + dbLibrary + ".ITEM_MASTER_IMAGE_UPLOAD) AS D " +
                                            "ON D.ITEM_MASTER_ID = A.ITEM_ID " +
                                            "WHERE COMPANY_ID = " + companyid + " " +
                                            "AND B.IS_ACTIVE = 1 " +
                                            "AND C.IS_ACTIVE = 1";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public int DeleteInActiveItems(int ItemId, int CategoryId, int SubCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ADD_ITEMS SET IS_ACTIVE = " + 0 + " WHERE  SUB_CATEGORY_ID = " + SubCategoryId + " AND  ITEM_ID = " + ItemId + "  AND  CATEGORY_ID = " + CategoryId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int GetIdByItemName(int CompanyId, string ItemName, DBConnection dbConnection)
        {
            int ItemId = 0;

            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT ITEM_ID FROM "+ dbLibrary +".ADD_ITEMS WHERE COMPANY_ID =" + CompanyId + " AND ITEM_NAME ='" + ItemName + "'";
            if (dbConnection.cmd.ExecuteScalar() != null)
            {
                ItemId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            }

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return ItemId;
            }
        }

        public List<AddItem> SearchedItemName(int MainCategoryId, int SubCategoryId, int CompanyId, string ItemName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS  WHERE CATEGORY_ID = " + MainCategoryId + " AND SUB_CATEGORY_ID =" + SubCategoryId + " AND COMPANY_ID=" + CompanyId + " AND ITEM_NAME LIKE '%" + ItemName + "%';";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public List<AddItem> FetchItemsByCategories(int MainCategoryId, int SubCategoryId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS  WHERE CATEGORY_ID = " + MainCategoryId + " AND SUB_CATEGORY_ID = " + SubCategoryId + " AND COMPANY_ID = " + CompanyId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public AddItem FetchItemObj(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS AS AI LEFT JOIN ITEM_IMAGE_UPLOAD AS ITU ON AI.ITEM_ID = ITU.ITEM_ID  WHERE AI.ITEM_ID = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<AddItem>(dbConnection.dr);
            }
        }

        public int UpdateItemStatus(int companyId, int CategoryId, int subCategoryId, int itemid, int status, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ADD_ITEMS SET IS_ACTIVE = " + status + " WHERE  SUB_CATEGORY_ID = " + subCategoryId + " AND  ITEM_ID = " + itemid + "  AND  CATEGORY_ID = " + CategoryId + " AND COMPANY_ID = "+companyId+";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<AddItem> ItemsInInventoryByCatagory(int MainCategoryId, int SubCategoryId, int level, DBConnection dbConnection)
        {

            string query = "SELECT W.LOCATION AS CREATED_BY, B.AVAILABLE_QTY AS IS_ACTIVE, B.ITEM_NAME AS UPDATED_BY, c.REORDER_LEVEL AS SUB_CATEGORY_ID, B.COMPANY_ID, B.ITEM_ID  " +
 " FROM  (SELECT M.WAREHOUSE_ID, M.AVAILABLE_QTY, ADD_ITEMS.ITEM_ID, ADD_ITEMS.CATEGORY_ID, ADD_ITEMS.SUB_CATEGORY_ID, ADD_ITEMS.ITEM_NAME, ADD_ITEMS.COMPANY_ID " +
 "  FROM  ADD_ITEMS INNER JOIN WAREHOUSE_INVENTORY_MASTER AS M ON ADD_ITEMS.ITEM_ID = M.ITEM_ID " +
 "        ";

            if (SubCategoryId != 0)
            {
                query += " WHERE (ADD_ITEMS.SUB_CATEGORY_ID =  " + SubCategoryId + ")";
            }
            else if (MainCategoryId != 0)
            {
                query += " WHERE (ADD_ITEMS.CATEGORY_ID =  " + MainCategoryId + ")";
            }

            query += ") AS B INNER JOIN " +
            " COMPANY_INVENTORY_MASTER AS c ON B.COMPANY_ID = c.COMPANY_ID AND B.ITEM_ID = c.ITEM_ID INNER JOIN " +
            " WAREHOUSE AS W ON B.WAREHOUSE_ID = W.WAREHOUSE_ID ";

            if (level != 0)
            {
                query += "WHERE (c.REORDER_LEVEL < " + level + " )";
            }

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = query;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public List<AddItem> GetItemsForMrnAndPr(int MainCategoryId, int SubCategoryId, int ItemType, int companyId, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT AI.ITEM_ID,AI.ITEM_NAME,ISNULL(UM.MEASUREMENT_SHORT_NAME,'Not Found') AS MEASUREMENT_SHORT_NAME FROM ADD_ITEMS AS AI \n");
            sql.Append("LEFT JOIN UNIT_MEASUREMENT AS UM ON AI.MEASUREMENT_ID =UM.MEASUREMENT_ID \n");
            sql.Append("WHERE AI.CATEGORY_ID="+MainCategoryId+ " AND AI.SUB_CATEGORY_ID=" + SubCategoryId + " AND AI.IS_ACTIVE = 1 AND AI.ITEM_TYPE=" + ItemType + " AND AI.COMPANY_ID=" + companyId);

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }
        public List<AddItem> FetchItemListDetailedFilter(int companyid, int mainCatId, int subCatId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            string sql = "SELECT DISTINCT * FROM ADD_ITEMS AS A\n" +
                                                "LEFT JOIN (SELECT REORDER_LEVEL,ITEM_ID FROM COMPANY_INVENTORY_MASTER) AS D\n" +
                                                "ON A.ITEM_ID = D.ITEM_ID\n" +
                                                "INNER JOIN (SELECT CATEGORY_ID AS CAT, CATEGORY_NAME, IS_ACTIVE FROM ITEM_CATEGORY WHERE COMPANY_ID=" + companyid + ") AS B\n" +
                                                "ON A.CATEGORY_ID = B.CAT\n" +
                                                "INNER JOIN (SELECT SUB_CATEGORY_ID AS SUB, SUB_CATEGORY_NAME, IS_ACTIVE FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + companyid + ") AS C\n" +
                                                "ON A.SUB_CATEGORY_ID = C.SUB\n" +
                                                "WHERE COMPANY_ID = " + companyid + "\n " +
                                                "AND B.IS_ACTIVE = 1 \n" +
                                                "AND C.IS_ACTIVE = 1 ";

            if (mainCatId != 0)
            {
                sql += " AND A.CATEGORY_ID = " + mainCatId + " ";
            }

            if (subCatId != 0)
            {
                sql += " AND A.SUB_CATEGORY_ID = " + subCatId + " ";
            }


            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public List<AddItem> FetchItemByItemName(int companyid, string ItemName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT DISTINCT * FROM ADD_ITEMS AS A\n" +
                                                "LEFT JOIN (SELECT REORDER_LEVEL,ITEM_ID FROM COMPANY_INVENTORY_MASTER) AS D\n" +
                                                "ON A.ITEM_ID = D.ITEM_ID\n" +
                                                "INNER JOIN (SELECT CATEGORY_ID AS CAT, CATEGORY_NAME, IS_ACTIVE FROM ITEM_CATEGORY WHERE COMPANY_ID=" + companyid + ") AS B\n" +
                                                "ON A.CATEGORY_ID = B.CAT\n" +
                                                "INNER JOIN (SELECT SUB_CATEGORY_ID AS SUB, SUB_CATEGORY_NAME, IS_ACTIVE FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + companyid + ") AS C\n" +
                                                "ON A.SUB_CATEGORY_ID = C.SUB\n" +
                                                "WHERE COMPANY_ID = " + companyid + "\n " +
                                                "AND B.IS_ACTIVE = 1 \n" +
                                                "AND C.IS_ACTIVE = 1 AND A.ITEM_NAME LIKE '%" + ItemName + "%' ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public List<AddItem> FetchItemByItemCode(int companyid, string ItemCode, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT DISTINCT * FROM ADD_ITEMS AS A\n" +
                                                "LEFT JOIN (SELECT REORDER_LEVEL,ITEM_ID FROM COMPANY_INVENTORY_MASTER) AS D\n" +
                                                "ON A.ITEM_ID = D.ITEM_ID\n" +
                                                "INNER JOIN (SELECT CATEGORY_ID AS CAT, CATEGORY_NAME, IS_ACTIVE FROM ITEM_CATEGORY WHERE COMPANY_ID=" + companyid + ") AS B\n" +
                                                "ON A.CATEGORY_ID = B.CAT\n" +
                                                "INNER JOIN (SELECT SUB_CATEGORY_ID AS SUB, SUB_CATEGORY_NAME, IS_ACTIVE FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + companyid + ") AS C\n" +
                                                "ON A.SUB_CATEGORY_ID = C.SUB\n" +
                                                "WHERE COMPANY_ID = " + companyid + "\n " +
                                                "AND B.IS_ACTIVE = 1 \n" +
                                                "AND C.IS_ACTIVE = 1 AND A.REFERENCE_NO LIKE '%" + ItemCode + "%' ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }


        public int GetMeasurementIdOfItem(int ItemId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT ISNULL(MEASUREMENT_ID,0) AS MEASUREMENT_ID FROM ADD_ITEMS WHERE ITEM_ID=" + ItemId + " AND COMPANY_ID=" + CompanyId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    dbConnection.dr.Read();
                    return int.Parse(dbConnection.dr["MEASUREMENT_ID"].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }


        public int GetStockMaintaininType(int ItemId, int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT STOCK_MAINTAINING_TYPE FROM ADD_ITEMS WHERE ITEM_ID=" + ItemId + " AND COMPANY_ID=" + CompanyId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                if (dbConnection.dr.HasRows) {
                    dbConnection.dr.Read();
                    return int.Parse(dbConnection.dr["STOCK_MAINTAINING_TYPE"].ToString());
                }
                else {
                    return 0;
                }
            }
        }

        public AddItem FetchItemByItemId(int ItemId, int companyid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT AI.*,IMG.ITEM_MASTER_IMAGE_PATH FROM ADD_ITEMS AS AI " +
                                            " INNER JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID = " + companyid + " ) AS IC ON IC.CATEGORY_ID = AI.CATEGORY_ID " +
                                            "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID = " + companyid + ") AS ISC ON ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID " +
                                            "LEFT JOIN(SELECT MEASUREMENT_ID, MEASUREMENT_NAME FROM UNIT_MEASUREMENT) AS UM ON UM.MEASUREMENT_ID = AI.MEASUREMENT_ID " +
                                            "LEFT JOIN(SELECT ITEM_MASTER_ID, ITEM_MASTER_IMAGE_PATH FROM ITEM_MASTER_IMAGE_UPLOAD) AS IMG ON IMG.ITEM_MASTER_ID = AI.ITEM_ID " +
                                            "WHERE AI.ITEM_ID = " + ItemId + " AND AI.COMPANY_ID = " + companyid + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<AddItem>(dbConnection.dr);
            }
        }
    }
}
