using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
  public interface TempMRN_SupportiveDocumentDAO
    {
      int SaveTempSupportiveUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName, DBConnection dbConnection);
      List<TempMRN_SupportiveDocument> GetTempMrnSupportiveFiles(int ItemId, int MrnId, DBConnection dbConnection);

      int DeleteTempSupporiveFileUploadCompanyId(int DepartmentId, DBConnection dbConnection);

      List<TempMRN_SupportiveDocument> GetMRNSupporiveUpoadFilesListByMRNIdItemId(int DepartmentId, int MrnId, int ItemId, DBConnection dbConnection);

      int DeleteTempSupporiveFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection);

      int DeleteTempMrnDetailSupporiveUpload(int MrnId, int DepartmentId, int ItemId, DBConnection dbConnection);

      int GetTempMrnSupportiveFilesTemp(int ItemId, int MrnId, string FilePath, DBConnection dbConnection);

    }
  public class TempMRN_SupportiveDocumentDAOImpl : TempMRN_SupportiveDocumentDAO
  {
      string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];


      public List<TempMRN_SupportiveDocument> GetTempMrnSupportiveFiles(int ItemId, int MrnId, DBConnection dbConnection)
      {
          dbConnection.cmd.Parameters.Clear();

          dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempMRN_SupportiveDocument  WHERE  ITEM_ID=" + ItemId + " AND MRN_ID=" + MrnId + ";";
          dbConnection.cmd.CommandType = System.Data.CommandType.Text;

          using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
          {
              DataAccessObject dataAccessObject = new DataAccessObject();
              return dataAccessObject.ReadCollection<TempMRN_SupportiveDocument>(dbConnection.dr);
          }
      }


      public int SaveTempSupportiveUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName, DBConnection dbConnection)
      {
          dbConnection.cmd.Parameters.Clear();
           
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".TempMRN_SupportiveDocument (DEPARTMENT_ID, ITEM_ID, MRN_ID, FILE_PATH,FILE_NAME) VALUES ( " + DepartmentId + ", " + ItemId + " , " + MrnId + ", '" + FilePath + "', '" + FileName + "');";
          dbConnection.cmd.CommandType = System.Data.CommandType.Text;
          return dbConnection.cmd.ExecuteNonQuery();
      }

      public List<TempMRN_SupportiveDocument> GetTempPrSupportiveFiles(int ItemId, int MrnId, DBConnection dbConnection)
      {
          dbConnection.cmd.Parameters.Clear();

          dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempMRN_SupportiveDocument  WHERE  ITEM_ID=" + ItemId + " AND MRN_ID=" + MrnId + ";";
          dbConnection.cmd.CommandType = System.Data.CommandType.Text;

          using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
          {
              DataAccessObject dataAccessObject = new DataAccessObject();
              return dataAccessObject.ReadCollection<TempMRN_SupportiveDocument>(dbConnection.dr);
          }
      }

      public int DeleteTempSupporiveFileUploadCompanyId(int DepartmentId, DBConnection dbConnection)
      {
          dbConnection.cmd.Parameters.Clear();
          dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempMRN_SupportiveDocument  WHERE DEPARTMENT_ID = " + DepartmentId + ";";
          dbConnection.cmd.CommandType = System.Data.CommandType.Text;
          return dbConnection.cmd.ExecuteNonQuery();
      }


      public List<TempMRN_SupportiveDocument> GetMRNSupporiveUpoadFilesListByMRNIdItemId(int DepartmentId, int MrnId, int ItemId, DBConnection dbConnection)
      {
          dbConnection.cmd.Parameters.Clear();

          dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempMRN_SupportiveDocument  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND MRN_ID=" + MrnId + ";";
          dbConnection.cmd.CommandType = System.Data.CommandType.Text;

          using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
          {
              DataAccessObject dataAccessObject = new DataAccessObject();
              return dataAccessObject.ReadCollection<TempMRN_SupportiveDocument>(dbConnection.dr);
          }
      }

      public int DeleteTempSupporiveFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection)
      {
          dbConnection.cmd.Parameters.Clear();
          dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempMRN_SupportiveDocument  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + ";";
          dbConnection.cmd.CommandType = System.Data.CommandType.Text;
          return dbConnection.cmd.ExecuteNonQuery();
      }

      public int DeleteTempMrnDetailSupporiveUpload(int MrnId, int DepartmentId, int ItemId, DBConnection dbConnection)
      {
          dbConnection.cmd.Parameters.Clear();
          dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempMRN_SupportiveDocument  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND MRN_ID=" + MrnId + ";";
          dbConnection.cmd.CommandType = System.Data.CommandType.Text;
          return dbConnection.cmd.ExecuteNonQuery();
      }

      public int GetTempMrnSupportiveFilesTemp(int ItemId, int MrnId, string FilePath, DBConnection dbConnection)
      {
          dbConnection.cmd.Parameters.Clear();

          dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempMRN_SupportiveDocument  WHERE  ITEM_ID=" + ItemId + " AND MRN_ID=" + MrnId + " AND FILE_PATH='" + FilePath + "';";

          dbConnection.cmd.CommandType = System.Data.CommandType.Text;
          return dbConnection.cmd.ExecuteNonQuery();
      }
  }
}
