using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface AddItemBOMDAO
    {
        int SaveAddItemBOM(int companyId, int ItemId, int SeqNo, string Meterial, string Description,DateTime createdDate, DateTime updateDate, string createdBy,string updatedBy,int isactive, DBConnection dbConnection);
       // int GetNextPrIdObj(int companyId);
       // List<AddItemBOM> GetListById(int companyId, int ItemId);
        int DeleteTempData(int companyId, int ItemId, DBConnection dbConnection);
        int DeleteTempDataByCompanyId(int companyId, DBConnection dbConnection);
        int UpdateAddItemBOM(int companyId, int ItemId, int SeqNo, string Meterial, string Description,DateTime updateDate,string updatedBy,int isactive, DBConnection dbConnection);
        List<AddItemBOM> GetBOMListByItemId(int companyId, int ItemId, DBConnection dbConnection);
        int DeleteBOMByItemId(int companyId, int ItemId, DBConnection dbConnection);
        List<AddItemBOM> GetbyItemName(int MainCategoryId, int SubCategoryId, int CompanyId, string ItemName, DBConnection dbConnection);
        int DeleteBOMByItemDet(AddItemBOM objBom, DBConnection dbConnection);
        int SaveAddItemBOMinEdit(int companyId, int ItemId, string Meterial, string Description, DateTime createdDate, DateTime updateDate, string createdBy, string updatedBy, int isactive, DBConnection dbConnection);
        int UpdateItemBoms(List<AddItemBOM> List, int itemid, int companyId, int UserId, DBConnection dbConnection);
    }
    public class AddItemBOMDAOImpl : AddItemBOMDAO
    {
         string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveAddItemBOM(int companyId, int ItemId, int SeqNo, string Meterial, string Description,DateTime createdDate, DateTime updateDate, string createdBy,string updatedBy,int isactive, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO  " + dbLibrary + ".ADD_ITEMS_BOM(COMPANY_ID,ITEM_ID,SEQ_NO,MATERIAL,DESCRIPTION,IS_ACTIVE,CREATED_DATETIME,CREATED_BY,UPDATED_DATETIME,UPDATED_BY )" +
            "VALUES ( " + companyId + " , " + ItemId + ", " + SeqNo + ", '" + Meterial + "', '" + Description + "'," + isactive + ", '" + createdDate + "','" + createdBy + "', '" + updateDate + "','" + updatedBy + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int SaveAddItemBOMinEdit(int companyId, int ItemId, string Meterial, string Description, DateTime createdDate, DateTime updateDate, string createdBy, string updatedBy, int isactive, DBConnection dbConnection)
        {


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT ISNULL(MAX(SEQ_NO),0) from " + dbLibrary + ".ADD_ITEMS_BOM WHERE COMPANY_ID = " + companyId + " AND ITEM_ID = " + ItemId;
            int Seq = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            int SeqNo = Seq + 1;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO  " + dbLibrary + ".ADD_ITEMS_BOM(COMPANY_ID,ITEM_ID,SEQ_NO,MATERIAL,DESCRIPTION,IS_ACTIVE,CREATED_DATETIME,CREATED_BY,UPDATED_DATETIME,UPDATED_BY )" +
            "VALUES ( " + companyId + " , " + ItemId + ", " + SeqNo + ", '" + Meterial + "', '" + Description + "'," + isactive + ", '" + createdDate + "','" + createdBy + "', '" + updateDate + "','" + updatedBy + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        //public int GetNextPrIdObj(int DepartmentId, DBConnection dbConnection)
        //{
        //    int PrId = 0;

        //    dbConnection.cmd.Parameters.Clear();
        //    dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"PR_MASTER\"";

        //    var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        //    if (count == 0)
        //    {
        //        PrId = 001;
        //    }
        //    else
        //    {
        //        dbConnection.cmd.CommandText = "SELECT MAX (\"PR_ID\")+1 AS MAXid FROM public.\"PR_MASTER\"";
        //        PrId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        //    }

        //    dbConnection.cmd.CommandType = System.Data.CommandType.Text;

        //    using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
        //    {
        //        DataAccessObject dataAccessObject = new DataAccessObject();
        //        return PrId;
        //    }
        //}

        //public List<TempBOM> GetListById(int DepartmentId, int ItemId, DBConnection dbConnection)
        //{
        //    dbConnection.cmd.Parameters.Clear();

        //    dbConnection.cmd.CommandText = "SELECT * FROM public.\"ADD_ITEMS_BOM\"  WHERE \"COMPANY_ID\" = " + companyId + " AND \"ITEM_ID\"=" + ItemId + ";";
        //    dbConnection.cmd.CommandType = System.Data.CommandType.Text;

        //    using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
        //    {
        //        DataAccessObject dataAccessObject = new DataAccessObject();
        //        return dataAccessObject.ReadCollection<TempBOM>(dbConnection.dr);
        //    }
        //}

        public int DeleteTempData(int companyId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM \"ADD_ITEMS_BOM\"  WHERE \"COMPANY_ID\" = " + companyId + " AND \"ITEM_ID\"=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempDataByCompanyId(int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM \"ADD_ITEMS_BOM\"  WHERE \"COMPANY_ID\" = " + companyId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateAddItemBOM(int companyId, int ItemId, int SeqNo, string Meterial, string Description,DateTime updateDate, string updatedBy,int isactive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ADD_ITEMS_BOM SET MATERIAL = '" + Meterial + "',DESCRIPTION = '" + Description + "',IS_ACTIVE = '" + isactive + "',UPDATED_DATETIME = '" + updateDate + "',UPDATED_BY = '" + updatedBy + "' WHERE COMPANY_ID = " + companyId + " AND ITEM_ID = " + ItemId + " AND SEQ_NO = " + SeqNo + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<AddItemBOM> GetBOMListByItemId(int companyId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM ADD_ITEMS_BOM  WHERE COMPANY_ID = " + companyId + " AND ITEM_ID=" + ItemId + " AND IS_ACTIVE = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItemBOM>(dbConnection.dr);
            }
        }

        public int DeleteBOMByItemId( int companyId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM \"ADD_ITEMS_BOM\"  WHERE \"COMPANY_ID\" = " + companyId + " AND \"ITEM_ID\"=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<AddItemBOM> GetbyItemName(int MainCategoryId, int SubCategoryId, int CompanyId, string ItemName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT ITEM_ID FROM " + dbLibrary + ".ADD_ITEMS  WHERE CATEGORY_ID = " + MainCategoryId + " AND SUB_CATEGORY_ID =" + SubCategoryId + " AND COMPANY_ID=" + CompanyId + " AND ITEM_NAME LIKE '%" + ItemName + "%';";
            var itemID = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS_BOM  WHERE COMPANY_ID = " + CompanyId + " AND ITEM_ID=" + itemID + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItemBOM>(dbConnection.dr);
            }
        }

        public int DeleteBOMByItemDet(AddItemBOM objBom, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM ADD_ITEMS_BOM  WHERE COMPANY_ID  = " + objBom.companyId + " AND ITEM_ID=" + objBom.itemId + " AND MATERIAL='"+ objBom .Material+ "' AND DESCRIPTION='"+ objBom.Description+ "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateItemBoms(List<AddItemBOM> List, int itemid, int companyId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM ADD_ITEMS_BOM  WHERE COMPANY_ID  = " + companyId + " AND ITEM_ID=" + itemid + " ";

            for (int i = 0; i < List.Count; i++) {
                int seqNo = i + 1;
                dbConnection.cmd.CommandText += "INSERT INTO ADD_ITEMS_BOM VALUES ("+ companyId + ", " + itemid + "," + seqNo + ",'" + List[i].Material + "','" + List[i].Description + "',1,'" + LocalTime.Now + "'," + UserId + ",'" + LocalTime.Now + "'," + UserId + ")";
            }
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }

}
