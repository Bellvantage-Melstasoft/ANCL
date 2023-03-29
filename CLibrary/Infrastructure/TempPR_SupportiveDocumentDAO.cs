using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface TempPR_SupportiveDocumentDAO
    {
        int SaveTempSupportiveUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection);
        List<TempPR_SupportiveDocument> GetTempPrSupportiveFiles(int ItemId, int PrId, DBConnection dbConnection);
        int DeleteTempSupporiveFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection);
        int DeleteTempSupporiveFileUploadCompanyId(int DepartmentId, DBConnection dbConnection);
        int DeleteTempPrDetailSupporiveUpload(int PrId, int DepartmentId, int ItemId, DBConnection dbConnection);
        List<TempPR_SupportiveDocument> GetPrSupporiveUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId, DBConnection dbConnection);

        int GetTempPrSupportiveFilesTemp(int ItemId, int PrId, string FilePath, DBConnection dbConnection);
    }

    public class TempPR_SupportiveDocumentDAOImpl : TempPR_SupportiveDocumentDAO
    {
        public int SaveTempSupportiveUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO public.\"TempPR_SupportiveDocument\" (\"DEPARTMENT_ID\", \"ITEM_ID\", \"PR_ID\", \"FILE_PATH\",\"FILE_NAME\") VALUES ( " + DepartmentId + ", " + ItemId + " , " + PrId + ", '" + FilePath + "', '" + FileName + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempPR_SupportiveDocument> GetTempPrSupportiveFiles(int ItemId, int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"TempPR_SupportiveDocument\"  WHERE  \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\"=" + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempPR_SupportiveDocument>(dbConnection.dr);
            }
        }

        public int DeleteTempSupporiveFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempPR_SupportiveDocument\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempSupporiveFileUploadCompanyId(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempPR_SupportiveDocument\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempPrDetailSupporiveUpload(int PrId, int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempPR_SupportiveDocument\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\"=" + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempPR_SupportiveDocument> GetPrSupporiveUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"TempPR_SupportiveDocument\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\"=" + prId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempPR_SupportiveDocument>(dbConnection.dr);
            }
        }

        public int GetTempPrSupportiveFilesTemp(int ItemId, int PrId, string FilePath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempPR_SupportiveDocument\"  WHERE  \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\"=" + PrId + " AND \"FILE_PATH\"='" + FilePath + "';";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }

    public class TempPR_SupportiveDocumentDAOSQLImpl : TempPR_SupportiveDocumentDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveTempSupportiveUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".TempPR_SupportiveDocument (DEPARTMENT_ID, ITEM_ID, PR_ID, FILE_PATH,FILE_NAME) VALUES ( " + DepartmentId + ", " + ItemId + " , " + PrId + ", '" + FilePath + "', '" + FileName + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempPR_SupportiveDocument> GetTempPrSupportiveFiles(int ItemId, int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempPR_SupportiveDocument  WHERE  ITEM_ID=" + ItemId + " AND PR_ID=" + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempPR_SupportiveDocument>(dbConnection.dr);
            }
        }

        public int DeleteTempSupporiveFileUpload(int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_SupportiveDocument  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempSupporiveFileUploadCompanyId(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_SupportiveDocument  WHERE DEPARTMENT_ID = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempPrDetailSupporiveUpload(int PrId, int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_SupportiveDocument  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND PR_ID=" + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempPR_SupportiveDocument> GetPrSupporiveUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempPR_SupportiveDocument  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND PR_ID=" + prId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempPR_SupportiveDocument>(dbConnection.dr);
            }
        }

        public int GetTempPrSupportiveFilesTemp(int ItemId, int PrId, string FilePath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempPR_SupportiveDocument  WHERE  ITEM_ID=" + ItemId + " AND PR_ID=" + PrId + " AND FILE_PATH='" + FilePath + "';";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
