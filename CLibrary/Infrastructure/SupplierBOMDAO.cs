using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface SupplierBOMDAO
    {
        int SaveSupplierBOM(int SupplierId,int PrId, int ItemId, int SeqNo, string Meterial, string Description, int IsActive, DateTime CreatedDatetime,int Comply, string Remarks, DBConnection dbConnection);
        int UpdateSupplierBOM(int SupplierId, int PrId, int ItemId, int SeqNo, string Meterial, string Description, int Comply, string Remarks, DBConnection dbConnection);
        List<SupplierBOM> GetSupplierList(int SupplierId, int PrId, int ItemId, DBConnection dbConnection);

        //New Methods By Salman created on 2019-01-17
        List<SupplierBOM> GetSupplierBom(int QuotationItemId, DBConnection dbConnection);
    }
    

    public class SupplierBOMDAOSQLImpl : SupplierBOMDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveSupplierBOM(int SupplierId, int PrId, int ItemId, int SeqNo, string Meterial, string Description, int IsActive, DateTime CreatedDatetime, int Comply, string Remarks, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM " + dbLibrary + ".SUPPLIER_BOM WHERE SUPPLIER_ID = " + SupplierId + " AND PR_ID = " + PrId + " AND  ITEM_ID = " + ItemId + " AND  SEQ_NO = " + SeqNo + "";
            int CountExistBOM = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if(CountExistBOM == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_BOM (SUPPLIER_ID, PR_ID, ITEM_ID, SEQ_NO, MATERIAL, DESCRIPTION, IS_ACTIVE, CREATED_DATETIME, COMPLY, REMARKS) VALUES (" + SupplierId + ", " + PrId + ", " + ItemId + " , " + SeqNo + ", '" + Meterial + "', '" + Description + "', " + IsActive + ", '" + CreatedDatetime + "', " + Comply + ",'" + Remarks + "' );";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_BOM SET  MATERIAL = '" + Meterial + "', DESCRIPTION = '" + Description + "', COMPLY = " + Comply + ", REMARKS = '" + Remarks + "' WHERE SUPPLIER_ID = " + SupplierId + " AND PR_ID = " + PrId + " AND  ITEM_ID = " + ItemId + " AND SEQ_NO = " + SeqNo + "";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
        }

        public int UpdateSupplierBOM(int SupplierId, int PrId, int ItemId, int SeqNo, string Meterial, string Description, int Comply, string Remarks, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_BOM SET  MATERIAL = '" + Meterial + "', DESCRIPTION = '" + Description + "', COMPLY = " + Comply + ", REMARKS = '" + Remarks + "' WHERE SUPPLIER_ID = " + SupplierId + " AND PR_ID = " + PrId + " AND  ITEM_ID = " + ItemId + " AND SEQ_NO = " + SeqNo + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierBOM> GetSupplierList(int SupplierId, int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".SUPPLIER_BOM " +
                                           " WHERE  PR_ID = " + PrId + " AND  ITEM_ID = " + ItemId + " AND  SUPPLIER_ID = " + SupplierId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierBOM>(dbConnection.dr);
            }
        }

        public List<SupplierBOM> GetSupplierBom(int QuotationItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_BOM WHERE QUOTATION_ITEM_ID = " + QuotationItemId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierBOM>(dbConnection.dr);
            }
        }
    }
}
