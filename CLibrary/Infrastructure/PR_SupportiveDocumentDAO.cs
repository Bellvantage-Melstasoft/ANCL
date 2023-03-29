using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface PR_SupportiveDocumentDAO
    {
        int SaveSupporiveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection);
        List<PR_SupportiveDocument> FtechUploadeSupporiveFiles(int PrId, int ItemId, DBConnection dbConnection);
        int DeleteFileUpload(int PrId, int itemId, DBConnection dbConnection);
        int DeleteParticularSupporiveFile(int PrId, int itemId, string imagepath, DBConnection dbConnection);
        List<PR_SupportiveDocument> FtechUploadeSupportiveDocmentsRejected(int PrId, DBConnection dbConnection);
    }

    public class PR_SupportiveDocumentDAOImpl : PR_SupportiveDocumentDAO
    {
        public int SaveSupporiveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO public.\"PR_SUPPORTIVE_DOCUMENTS\" (\"DEPARTMENT_ID\", \"ITEM_ID\", \"PR_ID\", \"FILE_PATH\",\"FILE_NAME\",\"IS_ACTIVE\") VALUES ( " + DepartmentId + ", " + ItemId + " , " + PrId + ", '" + FilePath + "', '" + FileName + "', " + 1 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PR_SupportiveDocument> FtechUploadeSupporiveFiles(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_SUPPORTIVE_DOCUMENTS\" WHERE \"PR_ID\" = " + PrId + " AND \"ITEM_ID\" = " + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_SupportiveDocument>(dbConnection.dr);
            }
        }

        public int DeleteFileUpload(int PrId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE public.\"PR_SUPPORTIVE_DOCUMENTS\" SET \"IS_ACTIVE\"= 0 WHERE \"PR_ID\"=" + PrId + " AND \"ITEM_ID\" = " + itemId + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteParticularSupporiveFile(int PrId, int itemId, string imagepath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "delete from public.\"PR_SUPPORTIVE_DOCUMENTS\"  WHERE \"PR_ID\"=" + PrId + " AND \"ITEM_ID\" = " + itemId + " AND \"FILE_PATH\"='" + imagepath + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PR_SupportiveDocument> FtechUploadeSupportiveDocmentsRejected(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_SUPPORTIVE_DOCUMENTS\" WHERE \"PR_ID\" = " + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_SupportiveDocument>(dbConnection.dr);
            }
        }
    }

    public class PR_SupportiveDocumentDAOSQLImpl : PR_SupportiveDocumentDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveSupporiveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_SUPPORTIVE_DOCUMENTS (DEPARTMENT_ID, ITEM_ID, PR_ID, FILE_PATH,FILE_NAME,IS_ACTIVE) VALUES ( " + DepartmentId + ", " + ItemId + " , " + PrId + ", '" + FilePath + "', '" + FileName + "', " + 1 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PR_SupportiveDocument> FtechUploadeSupporiveFiles(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_SUPPORTIVE_DOCUMENTS WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_SupportiveDocument>(dbConnection.dr);
            }
        }

        public int DeleteFileUpload(int PrId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_SUPPORTIVE_DOCUMENTS SET IS_ACTIVE= 0 WHERE PR_ID=" + PrId + " AND ITEM_ID = " + itemId + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteParticularSupporiveFile(int PrId, int itemId, string imagepath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "delete from " + dbLibrary + ".PR_SUPPORTIVE_DOCUMENTS  WHERE PR_ID=" + PrId + " AND ITEM_ID = " + itemId + " AND FILE_PATH='" + imagepath + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PR_SupportiveDocument> FtechUploadeSupportiveDocmentsRejected(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_SUPPORTIVE_DOCUMENTS WHERE PR_ID = " + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_SupportiveDocument>(dbConnection.dr);
            }
        }
    }
}
