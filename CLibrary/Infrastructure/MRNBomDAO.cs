using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;


namespace CLibrary.Infrastructure
{
   public interface MRNBomDAO
    {
       int SaveBillOfMeterial(int MrnId, int ItemId, string Meterial, string Description, int IsActive, DateTime CreatedDatetime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, DBConnection dbConnection);

       List<MRNBom> GetList(int Mrnid, int itemId, DBConnection dbConnection);

       int DeleteMRNBom(int Mrnid, int itemId, DBConnection dbConnection);

       int DeleteMRNBoMTrash(int Mrnid, int ItemId, DBConnection dbConnection);
    }
   public class MRNBomDAOImpl : MRNBomDAO
   {

       string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

       public int SaveBillOfMeterial(int MrnId, int ItemId,  string Meterial, string Description, int IsActive, DateTime CreatedDatetime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, DBConnection dbConnection)
       {

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".MRN_BOM WHERE  MRN_ID = " + MrnId + " AND  ITEM_ID = " + ItemId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            count = count + 1;
            dbConnection.cmd.Parameters.Clear();
           dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".MRN_BOM (MRN_ID, ITEM_ID, SEQ_NO, MATERIAL, DESCRIPTION, IS_ACTIVE, CREATED_DATETIME, CREATED_BY, UPDATED_DATETIME, UPDATED_BY) VALUES ( " + MrnId + ", " + ItemId + " , " + count + ", '" + Meterial + "', '" + Description + "', " + IsActive + ", '" + CreatedDatetime + "', '" + CreatedBy + "' ,'" + UpdatedDateTime + "', '" + UpdatedBy + "');";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;
           return dbConnection.cmd.ExecuteNonQuery();
       }

       public List<MRNBom> GetList(int Mrnid, int itemId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();

           dbConnection.cmd.CommandText = "SELECT * FROM  " + dbLibrary + ".MRN_BOM WHERE MRN_ID = " + Mrnid + " AND ITEM_ID = " + itemId + "";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

           using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
           {
               DataAccessObject dataAccessObject = new DataAccessObject();
               return dataAccessObject.ReadCollection<MRNBom>(dbConnection.dr);
           }
       }

       public int DeleteMRNBom(int Mrnid, int itemId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();
           dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_BOM SET IS_ACTIVE = 0  WHERE MRN_ID = " + Mrnid + " AND ITEM_ID = " + itemId + "";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;
           return dbConnection.cmd.ExecuteNonQuery();
       }

       public int DeleteMRNBoMTrash(int Mrnid, int ItemId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();
           dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".MRN_BOM   WHERE MRN_ID = " + Mrnid + " AND ITEM_ID = " + ItemId + "";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;
           return dbConnection.cmd.ExecuteNonQuery();
       }
   }
}
