using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;



namespace CLibrary.Infrastructure
{
   public interface TempMRN_FileUploadReplacementDAO
    {
       int SaveTempImageUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName, DBConnection dbConnection);

       List<TempMRN_FileUploadReplacement> GetTempMrnFiles(int ItemId, int MrnId, DBConnection dbConnection);
     
       int DeleteTempDataFileUploadCompanyId(int DepartmentId, DBConnection dbConnection);

       List<TempMRN_FileUploadReplacement> GetMRNUpoadFilesListByMRNIdItemId(int DepartmentId, int MrnId, int ItemId, DBConnection dbConnection);

       int DeleteTempMrnDetailFileUpload(int MrnId, int DepartmentId, int ItemId, DBConnection dbConnection);

       int DeleteTempDataFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection);

       int GetTempMrnFilesTemp(int ItemId, int MrnId, string FilePath, DBConnection dbConnection);
    }

   public class TempMRN_FileUploadReplacementDAOImpl : TempMRN_FileUploadReplacementDAO
   {
       string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

       public int SaveTempImageUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".TempMRN_FileUploadReplacement (DEPARTMENT_ID, ITEM_ID, MRN_ID, FILE_PATH,FILE_NAME) VALUES ( " + DepartmentId + ", " + ItemId + " , " + MrnId + ", '" + FilePath + "', '" + FileName + "');";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;
           return dbConnection.cmd.ExecuteNonQuery();
       }

       public List<TempMRN_FileUploadReplacement> GetTempMrnFiles(int ItemId, int MrnId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();

           dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempMRN_FileUploadReplacement  WHERE  ITEM_ID=" + ItemId + " AND MRN_ID=" + MrnId + ";";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

           using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
           {
               DataAccessObject dataAccessObject = new DataAccessObject();
               return dataAccessObject.ReadCollection<TempMRN_FileUploadReplacement>(dbConnection.dr);
           }
       }

       public int DeleteTempDataFileUploadCompanyId(int DepartmentId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();
           dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempMRN_FileUploadReplacement  WHERE DEPARTMENT_ID = " + DepartmentId + ";";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;
           return dbConnection.cmd.ExecuteNonQuery();
       }

       public List<TempMRN_FileUploadReplacement> GetPrUpoadFilesListByPrIdItemId(int DepartmentId, int MrnId, int ItemId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();

           dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempMRN_FileUploadReplacement  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND MRN_ID=" + MrnId + ";";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

           using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
           {
               DataAccessObject dataAccessObject = new DataAccessObject();
               return dataAccessObject.ReadCollection<TempMRN_FileUploadReplacement>(dbConnection.dr);
           }
       }

       public int DeleteTempMrnDetailFileUpload(int MrnId, int DepartmentId, int ItemId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();
           dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempMRN_FileUploadReplacement  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND MRN_ID=" + MrnId + ";";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;
           return dbConnection.cmd.ExecuteNonQuery();
       }
       public int DeleteTempDataFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();
           dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempMRN_FileUploadReplacement  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + ";";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;
           return dbConnection.cmd.ExecuteNonQuery();
       }

       public List<TempPR_FileUploadReplacement> GetPrUpoadFilesListByMrnIdItemId(int DepartmentId, int MrnId, int ItemId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();

           dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempMRN_FileUploadReplacement  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND MRN_ID=" + MrnId + ";";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

           using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
           {
               DataAccessObject dataAccessObject = new DataAccessObject();
               return dataAccessObject.ReadCollection<TempPR_FileUploadReplacement>(dbConnection.dr);
           }
       }
       public List<TempMRN_FileUploadReplacement> GetMRNUpoadFilesListByMRNIdItemId(int DepartmentId, int MrnId, int ItemId, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();

           dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempMRN_FileUploadReplacement  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND MRN_ID=" + MrnId + ";";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

           using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
           {
               DataAccessObject dataAccessObject = new DataAccessObject();
               return dataAccessObject.ReadCollection<TempMRN_FileUploadReplacement>(dbConnection.dr);
           }
       }


       public int GetTempMrnFilesTemp(int ItemId, int MrnId, string FilePath, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();

           dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_FileUploadReplacement  WHERE  ITEM_ID=" + ItemId + " AND MRN_ID=" + MrnId + " AND FILE_PATH='" + FilePath + "';";

           dbConnection.cmd.CommandType = System.Data.CommandType.Text;
           return dbConnection.cmd.ExecuteNonQuery();
       }
   }
}
