using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface PR_Replace_FileUploadDAO
    {
        int SaveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection);
        List<PR_Replace_FileUpload> FtechUploadeFiles(int PrId, int ItemId, DBConnection dbConnection);
        List<PR_Replace_FileUpload> FtechUploadeFilesRejected(int PrId, DBConnection dbConnection);
        int DeleteFileUpload(int PrId, int itemId, DBConnection dbConnection);
        int DeleteParticularReplaceFile(int PrId, int itemId, string imagepath, DBConnection dbConnection);
        int updateDefaultReplaceImage(int PrId, int itemId, string imagepath,int isDefault, DBConnection dbConnection);
        int updateReleaseImageDefault(int PrId, int itemId, DBConnection dbConnection);
        PR_Replace_FileUpload fetchPR_Replace_FileUploadObjForDefaultImage(int PrId, int itemId, DBConnection dbConnection);
    }   

    public class PR_Replace_FileUploadDAOSQLImpl : PR_Replace_FileUploadDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_REPLACE_FILE_UPLOAD (DEPARTMENT_ID, ITEM_ID, PR_ID, FILE_PATH,FILE_NAME,IS_ACTIVE,IS_DEFAULT_IMAGE) VALUES ( " + DepartmentId + ", " + ItemId + " , " + PrId + ", '" + FilePath + "', '" + FileName + "', " + 1 + "," + 0 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PR_Replace_FileUpload> FtechUploadeFiles(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_REPLACE_FILE_UPLOAD WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Replace_FileUpload>(dbConnection.dr);
            }
        }

        public List<PR_Replace_FileUpload> FtechUploadeFilesRejected(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_REPLACE_FILE_UPLOAD WHERE PR_ID = " + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Replace_FileUpload>(dbConnection.dr);
            }
        }

        public int DeleteFileUpload(int PrId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_REPLACE_FILE_UPLOAD SET IS_ACTIVE=0 WHERE PR_ID=" + PrId + " AND ITEM_ID = " + itemId + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteParticularReplaceFile(int PrId, int itemId, string imagepath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "delete from " + dbLibrary + ".PR_REPLACE_FILE_UPLOAD  WHERE PR_ID=" + PrId + " AND ITEM_ID = " + itemId + " AND FILE_PATH='" + imagepath + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateDefaultReplaceImage(int PrId, int itemId, string imagepath, int isDefault, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_REPLACE_FILE_UPLOAD SET IS_DEFAULT_IMAGE=  " + isDefault + " WHERE PR_ID=" + PrId + " AND ITEM_ID = " + itemId + " AND FILE_PATH='" + imagepath + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateReleaseImageDefault(int PrId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_REPLACE_FILE_UPLOAD SET IS_DEFAULT_IMAGE=  0  WHERE PR_ID=" + PrId + " AND ITEM_ID = " + itemId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public PR_Replace_FileUpload fetchPR_Replace_FileUploadObjForDefaultImage(int PrId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_REPLACE_FILE_UPLOAD WHERE PR_ID = " + PrId + " AND ITEM_ID = " + itemId + " AND IS_DEFAULT_IMAGE = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PR_Replace_FileUpload>(dbConnection.dr);
            }
        }
    }
}
