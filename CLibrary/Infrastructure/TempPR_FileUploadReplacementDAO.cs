using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface TempPR_FileUploadReplacementDAO
    {
        int SaveTempImageUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection);
        List<TempPR_FileUploadReplacement> GetTempPrFiles(int ItemId, int PrId, DBConnection dbConnection);
        int DeleteTempDataFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection);
        int DeleteTempDataFileUploadCompanyId(int DepartmentId, DBConnection dbConnection);
        int DeleteTempPrDetailFileUpload(int PrId, int DepartmentId, int ItemId, DBConnection dbConnection);
        List<TempPR_FileUploadReplacement> GetPrUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId, DBConnection dbConnection);
        int GetTempPrFilesTemp(int ItemId, int PrId, string FilePath, DBConnection dbConnection);
    }

    public class TempPR_FileUploadReplacementDAOImpl : TempPR_FileUploadReplacementDAO
    {
        public int SaveTempImageUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO public.\"TempPR_FileUploadReplacement\" (\"DEPARTMENT_ID\", \"ITEM_ID\", \"PR_ID\", \"FILE_PATH\",\"FILE_NAME\") VALUES ( " + DepartmentId + ", " + ItemId + " , " + PrId + ", '" + FilePath + "', '" + FileName + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempPR_FileUploadReplacement> GetTempPrFiles(int ItemId, int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"TempPR_FileUploadReplacement\"  WHERE  \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\"=" + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempPR_FileUploadReplacement>(dbConnection.dr);
            }
        }

        public int DeleteTempDataFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempPR_FileUploadReplacement\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempDataFileUploadCompanyId(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempPR_FileUploadReplacement\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempPrDetailFileUpload(int PrId, int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempPR_FileUploadReplacement\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\"=" + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempPR_FileUploadReplacement> GetPrUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"TempPR_FileUploadReplacement\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\"=" + prId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempPR_FileUploadReplacement>(dbConnection.dr);
            }
        }

        public int GetTempPrFilesTemp(int ItemId, int PrId, string FilePath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempPR_FileUploadReplacement\"  WHERE  \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\"=" + PrId + " AND \"FILE_PATH\"='" + FilePath + "';";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }

    public class TempPR_FileUploadReplacementDAOSQLImpl : TempPR_FileUploadReplacementDAO
    {
         string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

         public int SaveTempImageUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection)
         {
             dbConnection.cmd.Parameters.Clear();
             dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".TempPR_FileUploadReplacement (DEPARTMENT_ID, ITEM_ID, PR_ID, FILE_PATH,FILE_NAME) VALUES ( " + DepartmentId + ", " + ItemId + " , " + PrId + ", '" + FilePath + "', '" + FileName + "');";
             dbConnection.cmd.CommandType = System.Data.CommandType.Text;
             return dbConnection.cmd.ExecuteNonQuery();
         }

         public List<TempPR_FileUploadReplacement> GetTempPrFiles(int ItemId, int PrId, DBConnection dbConnection)
         {
             dbConnection.cmd.Parameters.Clear();

             dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempPR_FileUploadReplacement  WHERE  ITEM_ID=" + ItemId + " AND PR_ID=" + PrId + ";";
             dbConnection.cmd.CommandType = System.Data.CommandType.Text;

             using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
             {
                 DataAccessObject dataAccessObject = new DataAccessObject();
                 return dataAccessObject.ReadCollection<TempPR_FileUploadReplacement>(dbConnection.dr);
             }
         }

         public int DeleteTempDataFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection)
         {
             dbConnection.cmd.Parameters.Clear();
             dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_FileUploadReplacement  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + ";";
             dbConnection.cmd.CommandType = System.Data.CommandType.Text;
             return dbConnection.cmd.ExecuteNonQuery();
         }

         public int DeleteTempDataFileUploadCompanyId(int DepartmentId, DBConnection dbConnection)
         {
             dbConnection.cmd.Parameters.Clear();
             dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_FileUploadReplacement  WHERE DEPARTMENT_ID = " + DepartmentId + ";";
             dbConnection.cmd.CommandType = System.Data.CommandType.Text;
             return dbConnection.cmd.ExecuteNonQuery();
         }

         public int DeleteTempPrDetailFileUpload(int PrId, int DepartmentId, int ItemId, DBConnection dbConnection)
         {
             dbConnection.cmd.Parameters.Clear();
             dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_FileUploadReplacement  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND PR_ID=" + PrId + ";";
             dbConnection.cmd.CommandType = System.Data.CommandType.Text;
             return dbConnection.cmd.ExecuteNonQuery();
         }

         public List<TempPR_FileUploadReplacement> GetPrUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId, DBConnection dbConnection)
         {
             dbConnection.cmd.Parameters.Clear();

             dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempPR_FileUploadReplacement  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND PR_ID=" + prId + ";";
             dbConnection.cmd.CommandType = System.Data.CommandType.Text;

             using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
             {
                 DataAccessObject dataAccessObject = new DataAccessObject();
                 return dataAccessObject.ReadCollection<TempPR_FileUploadReplacement>(dbConnection.dr);
             }
         }

         public int GetTempPrFilesTemp(int ItemId, int PrId, string FilePath, DBConnection dbConnection)
         {
             dbConnection.cmd.Parameters.Clear();

             dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_FileUploadReplacement  WHERE  ITEM_ID=" + ItemId + " AND PR_ID=" + PrId + " AND FILE_PATH='" + FilePath + "';";

             dbConnection.cmd.CommandType = System.Data.CommandType.Text;
             return dbConnection.cmd.ExecuteNonQuery();
         }
    }
}
