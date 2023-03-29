using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface PR_FileUploadDAO
    {
        int SaveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection);
        List<PR_FileUpload> FtechUploadeFiles(int PrId, int ItemId, DBConnection dbConnection);
        List<PR_FileUpload> FtechUploadeFilesRejected(int PrId, DBConnection dbConnection);
        int DeleteFileUpload(int PrId, int itemId, DBConnection dbConnection);
        int DeleteParticularFile(int PrId, int itemId,string imagepath, DBConnection dbConnection);
        int updateDefaultStanardImage(int PrId, int itemId, string imagepath, int isDefault, DBConnection dbConnection);
        int updateReleaseImageDefault(int PrId, int itemId, DBConnection dbConnection);
        PR_FileUpload fetchPr_FileuploadObjForDefaultImage(int PrId, int itemId, DBConnection dbConnection);

    }
    

    public class PR_FileUploadDAOSQLImpl : PR_FileUploadDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_FILE_UPLOAD (DEPARTMENT_ID, ITEM_ID, PR_ID, FILE_PATH,FILE_NAME,IS_ACTIVE) VALUES ( " + DepartmentId + ", " + ItemId + " , " + PrId + ", '" + FilePath + "', '" + FileName + "', " + 1 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PR_FileUpload> FtechUploadeFiles(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_FILE_UPLOAD WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_FileUpload>(dbConnection.dr);
            }
        }

        public List<PR_FileUpload> FtechUploadeFilesRejected(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_FILE_UPLOAD WHERE PR_ID = " + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_FileUpload>(dbConnection.dr);
            }
        }

        public int DeleteFileUpload(int PrId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_FILE_UPLOAD SET IS_ACTIVE=0 WHERE PR_ID=" + PrId + " AND ITEM_ID = " + itemId + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteParticularFile(int PrId, int itemId, string imagepath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "delete from " + dbLibrary + ".PR_FILE_UPLOAD  WHERE PR_ID=" + PrId + " AND ITEM_ID = " + itemId + " AND FILE_PATH='" + imagepath + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateDefaultStanardImage(int PrId, int itemId, string imagepath, int isDefault, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_FILE_UPLOAD SET IS_DEFAULT_STANDARD_IMAGE=  " + isDefault + " WHERE PR_ID=" + PrId + " AND ITEM_ID = " + itemId + " AND FILE_PATH='" + imagepath + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateReleaseImageDefault(int PrId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_FILE_UPLOAD SET IS_DEFAULT_STANDARD_IMAGE=  0  WHERE PR_ID=" + PrId + " AND ITEM_ID = " + itemId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public PR_FileUpload fetchPr_FileuploadObjForDefaultImage(int PrId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_FILE_UPLOAD WHERE PR_ID = " + PrId + " AND ITEM_ID = " + itemId + " AND  IS_DEFAULT_STANDARD_IMAGE= 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PR_FileUpload>(dbConnection.dr);
            }
        }
    }
}
