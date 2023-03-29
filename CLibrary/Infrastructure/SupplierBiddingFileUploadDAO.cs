using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface SupplierBiddingFileUploadDAO
    {
        int SaveFiles(List<SupplierBiddingFileUpload> list, DBConnection dbConnection);
        int SaveFiles(int SupplierId, int QuotationId, int PrId, int ItemId, string FileName, string FilePath, DBConnection dbConnection);
        List<SupplierBiddingFileUpload> GetFilesByQuotationId(int quotationId, DBConnection dbConnection);
        List<SupplierBiddingFileUpload> GetFilesByQuotationId(int PrId, int ItemId, int SupplierId, DBConnection dbConnection);
        int DeleteFileUploads(int PrId, int ItemId, int SupplierId, string FilePath, DBConnection dbConnection);
        int DeleteFileUploads(string fileName, DBConnection dbConnection);


        //New Methods By Salman created on 2019-01-17
        List<SupplierBiddingFileUpload> GetUploadedFiles(int QuotationId, DBConnection dbConnection);
    }
    

    public class SupplierBiddingFileUploadDAOSQLImpl : SupplierBiddingFileUploadDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveFiles(int SupplierId, int QuotationId, int PrId, int ItemId, string FileName, string FilePath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_BIDDING_FILE_UPLOAD (SUPPLIER_ID, QUOTATION_ID, PR_ID, ITEM_ID, FILE_NAME, FILE_PATH) VALUES ( " + SupplierId + ", " + QuotationId + " , " + PrId + ", " + ItemId + ", '" + FileName + "', '" + FilePath + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        //public int SaveFiles(List<SupplierBiddingFileUpload> list, DBConnection dbConnection)
        //{
        //    dbConnection.cmd.Parameters.Clear();
        //    string query = "INSERT INTO " + dbLibrary + ".SUPPLIER_BIDDING_FILE_UPLOAD (SUPPLIER_ID , QUOTATION_ID , PR_ID ,ITEM_ID,FILE_NAME,FILE_PATH) VALUES ";
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        if (i == list.Count - 1)
        //            query += "(" + list[i].SupplierId + ",'" + list[i].QuotationId + "'," + list[i].PrId + ",'" + list[i].ItemId + "','" + list[i].FileName + "','" + list[i].FilePath + "')";
        //        else
        //            query += "(" + list[i].SupplierId + ",'" + list[i].QuotationId + "'," + list[i].PrId + ",'" + list[i].ItemId + "','" + list[i].FileName + "','" + list[i].FilePath + "'),";
        //    }
        //    dbConnection.cmd.CommandText = query;
        //    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
        //    return dbConnection.cmd.ExecuteNonQuery();
        //}


        public List<SupplierBiddingFileUpload> GetFilesByQuotationId(int PrId, int ItemId, int SupplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "select * from " + dbLibrary + ".SUPPLIER_BIDDING_FILE_UPLOAD WHERE SUPPLIER_ID = " + SupplierId + " AND PR_ID=" + PrId + " AND ITEM_ID=" + ItemId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierBiddingFileUpload>(dbConnection.dr);
            }
        }

        public List<SupplierBiddingFileUpload> GetFilesByQuotationId(int quotationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "select * from " + dbLibrary + ".SUPPLIER_BIDDING_FILE_UPLOAD WHERE QUOTATION_ID =" + quotationId + " ORDER BY FILE_NAME DESC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierBiddingFileUpload>(dbConnection.dr);
            }
        }

        public int DeleteFileUploads(int PrId, int ItemId, int SupplierId, string FilePath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".SUPPLIER_BIDDING_FILE_UPLOAD WHERE SUPPLIER_ID = " + SupplierId + " AND PR_ID=" + PrId + " AND ITEM_ID=" + ItemId + " AND FILE_NAME ='" + FilePath + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteFileUploads(string fileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".SUPPLIER_BIDDING_FILE_UPLOAD WHERE FILE_NAME ='" + fileName + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierBiddingFileUpload> GetUploadedFiles(int QuotationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_BIDDING_FILE_UPLOAD WHERE QUOTATION_ID =" + QuotationId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierBiddingFileUpload>(dbConnection.dr);
            }
        }

        public int SaveFiles(List<SupplierBiddingFileUpload> list, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}
