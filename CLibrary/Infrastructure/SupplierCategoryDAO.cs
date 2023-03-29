using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructue
{
   public interface SupplierCategoryDAO
    {
        int saveSupplierCategory(int supplierId, int categoryId, int isActive, DBConnection dbConnection);
        int updateSupplierCategory(int supplierId, int categoryId, int isActive, DBConnection dbConnection);
        List<SupplierCategory> GetSupplierCategoryBySupplierId(int supplierId, DBConnection dbConnection);
        int deleteSupplierCategoryBySupplierid(int supplierId, DBConnection dbConnection);
        List<SupplierCategory> GetSupplierCategoryWithCategoryName(int supplierId, DBConnection dbConnection);
        List<SupplierCategory> GetSupplierCategoryAndSubCategory(int supplierId, DBConnection dbConnection);
    }
    
    public class SupplierCategoryDAOSQLImpl : SupplierCategoryDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int deleteSupplierCategoryBySupplierid(int supplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".SUPPLIER_CATEGORY WHERE SUPPLIER_ID = " + supplierId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierCategory> GetSupplierCategoryBySupplierId(int supplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_CATEGORY AS sc INNER JOIN " + dbLibrary + ".ITEM_CATEGORY_MASTER AS ic ON  sc.CATEGORY_ID = ic.CATEGORY_ID  WHERE sc.SUPPLIER_ID = " + supplierId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierCategory>(dbConnection.dr);
            }
        }

        public int saveSupplierCategory(int supplierId, int categoryId, int isActive, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_CATEGORY WHERE SUPPLIER_ID = " + supplierId + " AND CATEGORY_ID = " + categoryId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_CATEGORY (SUPPLIER_ID , CATEGORY_ID , IS_ACTIVE ) VALUES (" + supplierId + "," + categoryId + "," + isActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return 0;
            }

        }

        public int updateSupplierCategory(int supplierId, int categoryId, int isActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_CATEGORY  SET   CATEGORY_ID = " + categoryId + " , IS_ACTIVE = " + isActive + "  WHERE SUPPLIER_ID = " + supplierId + " ";
            dbConnection.cmd.Parameters.AddWithValue("@supplierId", supplierId);
            dbConnection.cmd.Parameters.AddWithValue("@categoryId", categoryId);
            dbConnection.cmd.Parameters.AddWithValue("@isActive", isActive);
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierCategory> GetSupplierCategoryWithCategoryName(int supplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT DISTINCT * FROM " + dbLibrary + ".SUPPLIER_CATEGORY AS SC" +
                                           " INNER JOIN " + dbLibrary + ".ITEM_CATEGORY_MASTER AS IC ON SC.CATEGORY_ID = IC.CATEGORY_ID " +
                                           " WHERE SC.SUPPLIER_ID = " + supplierId + "  AND IC.IS_ACTIVE = 1 ORDER BY IC.CATEGORY_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierCategory>(dbConnection.dr);
            }
        }

        public List<SupplierCategory> GetSupplierCategoryAndSubCategory(int supplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".SUPPLIER_CATEGORY AS SC" +
                                           " INNER JOIN " + dbLibrary + ".ITEM_CATEGORY_MASTER AS IC ON SC.CATEGORY_ID = IC.CATEGORY_ID " +
                                           " INNER JOIN " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER AS ISC ON IC.CATEGORY_ID = ISC.CATEGORY_ID " +
                                           " WHERE SC.SUPPLIER_ID = " + supplierId + " AND ISC.IS_ACTIVE = 1 ORDER BY IC.CATEGORY_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierCategory>(dbConnection.dr);
            }
        }
    }
}
