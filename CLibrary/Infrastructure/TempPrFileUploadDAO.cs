using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface TempPrFileUploadDAO
    {
        int SaveTempImageUpload(int DepartmentId, int ItemId, int PrId, string FilePath,string FileName, DBConnection dbConnection);
        List<TempPrFileUpload> GetTempPrFiles(int ItemId, int PrId, DBConnection dbConnection);
        int DeleteTempDataFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection);
        int DeleteTempDataFileUploadCompanyId(int DepartmentId, DBConnection dbConnection);
        int DeleteTempPrDetailFileUpload(int PrId, int DepartmentId, int ItemId, DBConnection dbConnection);
        List<TempPrFileUpload> GetPrUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId, DBConnection dbConnection);

        int GetTempPrFilesTemp(int ItemId, int PrId, string FilePath, DBConnection dbConnection);
    }

    public class TempPrFileUploadDAOImpl : TempPrFileUploadDAO
    {
        public int SaveTempImageUpload(int DepartmentId, int ItemId, int PrId, string FilePath,string FileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO public.\"TempPR_FileUpload\" (\"DEPARTMENT_ID\", \"ITEM_ID\", \"PR_ID\", \"FILE_PATH\",\"FILE_NAME\") VALUES ( " + DepartmentId + ", " + ItemId + " , " + PrId + ", '" + FilePath + "', '" + FileName + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempPrFileUpload> GetTempPrFiles( int ItemId, int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"TempPR_FileUpload\"  WHERE  \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\"="+PrId+";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempPrFileUpload>(dbConnection.dr);
            }
        }

        public int DeleteTempDataFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempPR_FileUpload\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempDataFileUploadCompanyId(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempPR_FileUpload\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempPrDetailFileUpload(int PrId, int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempPR_FileUpload\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\"=" + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempPrFileUpload> GetPrUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"TempPR_FileUpload\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\"=" + prId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempPrFileUpload>(dbConnection.dr);
            }
        }

        public int GetTempPrFilesTemp(int ItemId, int PrId, string FilePath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempPR_FileUpload\"  WHERE  \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\"=" + PrId + " AND \"FILE_PATH\"='" + FilePath + "';";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }

    public class TempPrFileUploadDAOSQLImpl : TempPrFileUploadDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveTempImageUpload(int DepartmentId, int ItemId, int PrId, string FilePath,string FileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".TempPR_FileUpload (DEPARTMENT_ID, ITEM_ID, PR_ID, FILE_PATH,FILE_NAME) VALUES ( " + DepartmentId + ", " + ItemId + " , " + PrId + ", '" + FilePath + "', '" + FileName + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempPrFileUpload> GetTempPrFiles( int ItemId, int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempPR_FileUpload  WHERE  ITEM_ID=" + ItemId + " AND PR_ID=" + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempPrFileUpload>(dbConnection.dr);
            }
        }

        public int DeleteTempDataFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_FileUpload  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempDataFileUploadCompanyId(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_FileUpload  WHERE DEPARTMENT_ID = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempPrDetailFileUpload(int PrId, int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_FileUpload  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND PR_ID=" + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempPrFileUpload> GetPrUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempPR_FileUpload  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND PR_ID=" + prId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempPrFileUpload>(dbConnection.dr);
            }
        }

        public int GetTempPrFilesTemp(int ItemId, int PrId, string FilePath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_FileUpload  WHERE  ITEM_ID=" + ItemId + " AND PR_ID=" + PrId + " AND FILE_PATH='" + FilePath + "';";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
