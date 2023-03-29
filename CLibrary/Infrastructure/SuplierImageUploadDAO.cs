using System;
using System.Collections.Generic;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructue
{
   public interface SuplierImageUploadDAO
    {
        int saveUploadedSupplierFile(int supplierId, string supplierImagePath, string filename, string fileId, int isActive, DBConnection dbConnection);
        int saveUploadedSupplierFile(List<SuplierImageUpload> list, DBConnection dbConnection);
        int updateUploadedSupplierFile(int supplierId, string supplierImagePath, string filename, string fileId, int isActive, DBConnection dbConnection);
        int deleteUploadedSupplierFile(string fileId, DBConnection dbConnection);
        List<SuplierImageUpload> GetSupplierImagesBySupplierId(int supplierId, DBConnection dbConnection);
        List<SuplierImageUpload> GetSupplierComplianDocumentBySupplierId(int supplierId, DBConnection dbConnection);
        int saveUploadedSupplierComplianDoc(int supplierId, string supplierImagePath, string filename, string imageId, int isActive, DBConnection dbConnection);
        int deleteUploadedComplianFile(string fileId, DBConnection dbConnection);
    }
    
    public class SuplierImageUploadDAOSQLImpl : SuplierImageUploadDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int deleteUploadedSupplierFile(string fileId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_IMAGE_UPLOAD  SET IS_ACTIVE = 0  WHERE IMAGE_ID = '" + fileId + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<SuplierImageUpload> GetSupplierComplianDocumentBySupplierId(int supplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_COMPLAIN_DOC_UPLOAD "+
                                           " WHERE SUPPLIER_ID = " + supplierId + " AND IS_ACTIVE = 1 ORDER BY IMAGE_ID DESC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SuplierImageUpload>(dbConnection.dr);
            }
        }

        public List<SuplierImageUpload> GetSupplierImagesBySupplierId(int supplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_IMAGE_UPLOAD "+
                                           " WHERE SUPPLIER_ID = " + supplierId + " AND IS_ACTIVE =1 "+
                                           " ORDER BY IMAGE_ID DESC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SuplierImageUpload>(dbConnection.dr);
            }
        }

        public int saveUploadedSupplierFile(List<SuplierImageUpload> list, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            string query = "INSERT INTO " + dbLibrary + ".SUPPLIER_IMAGE_UPLOAD (SUPPLIER_ID , IMAGE_PATH , IS_ACTIVE ,FILE_NAME,IMAGE_ID) VALUES ";
            for(int i=0; i<list.Count;i++)
            {
                if (i == list.Count - 1)
                    query += "(" + list[i].SupplierId + ",'" + list[i].SupplierImagePath + "'," + list[i].IsActive + ",'" + list[i].ImageFileName + "','" + list[i].ImageId + "')";
                else
                    query += "(" + list[i].SupplierId + ",'" + list[i].SupplierImagePath + "'," + list[i].IsActive + ",'" + list[i].ImageFileName + "','" + list[i].ImageId + "'),";
            }
            dbConnection.cmd.CommandText = query;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int saveUploadedSupplierFile(int supplierId, string supplierImagePath, string filename, string fileId, int isActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_IMAGE_UPLOAD (SUPPLIER_ID , IMAGE_PATH , IS_ACTIVE ,FILE_NAME,IMAGE_ID) VALUES (" + supplierId + ",'" + supplierImagePath + "'," + isActive + ",'" + filename + "','" + fileId + "')";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();

        }

        public int updateUploadedSupplierFile(int supplierId, string supplierImagePath, string filename, string fileId, int isActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_IMAGE_UPLOAD  SET   IMAGE_PATH = '" + supplierImagePath + "' , IS_ACTIVE = " + isActive + "  WHERE SUPPLIER_ID = " + supplierId + " ";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int saveUploadedSupplierComplianDoc(int supplierId, string supplierImagePath, string filename, string fileId, int isActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_COMPLAIN_DOC_UPLOAD (SUPPLIER_ID , IMAGE_PATH , IS_ACTIVE ,FILE_NAME,IMAGE_ID) "+
                                            " VALUES (" + supplierId + ",'" + supplierImagePath + "'," + isActive + ",'" + filename + "','" + fileId + "')";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();

        }

        public int deleteUploadedComplianFile(string fileId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_COMPLAIN_DOC_UPLOAD  SET IS_ACTIVE = 0  WHERE IMAGE_ID = '" + fileId + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }

}
