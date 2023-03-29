using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;


namespace CLibrary.Infrastructure
{
    public interface Mrn_Replace_File_Upload_DAO
    {
        int SaveFileUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName, DBConnection dbConnection);
        List<Mrn_Replace_File_Upload> FtechUploadeFiles(int MrnId, int ItemId, DBConnection dbConnection);
        List<Mrn_Replace_File_Upload> FtechUploadeFilesRejected(int MrnId, DBConnection dbConnection);
        int DeleteFileUpload(int MrnId, int itemId, DBConnection dbConnection);
        int DeleteParticularReplaceFile(int MrnId, int itemId, string imagepath, DBConnection dbConnection);
        int updateDefaultReplaceImage(int MrnId, int itemId, string imagepath, int isDefault, DBConnection dbConnection);
        int updateReleaseImageDefault(int MrnId, int itemId, DBConnection dbConnection);
        Mrn_Replace_File_Upload fetchMrn_Replace_File_UploadObjForDefaultImage(int MrnId, int itemId, DBConnection dbConnection);
    }

    public class Mrn_Replace_File_Upload_DAOImpl : Mrn_Replace_File_Upload_DAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveFileUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".MRN_REPLACE_FILE_UPLOAD (DEPARTMENT_ID, ITEM_ID, MRN_ID, FILE_PATH,FILE_NAME,IS_ACTIVE,IS_DEFAULT_IMAGE) VALUES ( " + DepartmentId + ", " + ItemId + " , " + MrnId + ", '" + FilePath + "', '" + FileName + "', " + 1 + "," + 0 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Mrn_Replace_File_Upload> FtechUploadeFiles(int MrnId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_REPLACE_FILE_UPLOAD WHERE MRN_ID = " + MrnId + " AND ITEM_ID = " + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Mrn_Replace_File_Upload>(dbConnection.dr);
            }
        }

        public List<Mrn_Replace_File_Upload> FtechUploadeFilesRejected(int MrnId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_REPLACE_FILE_UPLOAD WHERE MRN_ID = " + MrnId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Mrn_Replace_File_Upload>(dbConnection.dr);
            }
        }

        public int DeleteFileUpload(int MrnId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_REPLACE_FILE_UPLOAD SET IS_ACTIVE=0 WHERE MRN_ID=" + MrnId + " AND ITEM_ID = " + itemId + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteParticularReplaceFile(int MrnId, int itemId, string imagepath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "delete from " + dbLibrary + ".MRN_REPLACE_FILE_UPLOAD  WHERE MRN_ID=" + MrnId + " AND ITEM_ID = " + itemId + " AND FILE_PATH='" + imagepath + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateDefaultReplaceImage(int MrnId, int itemId, string imagepath, int isDefault, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_REPLACE_FILE_UPLOAD SET IS_DEFAULT_IMAGE=  " + isDefault + " WHERE MRN_ID=" + MrnId + " AND ITEM_ID = " + itemId + " AND FILE_PATH='" + imagepath + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateReleaseImageDefault(int MrnId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_REPLACE_FILE_UPLOAD SET IS_DEFAULT_IMAGE=  0  WHERE MRN_ID=" + MrnId + " AND ITEM_ID = " + itemId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public Mrn_Replace_File_Upload fetchMrn_Replace_File_UploadObjForDefaultImage(int MrnId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_REPLACE_FILE_UPLOAD WHERE MRN_ID = " + MrnId + " AND ITEM_ID = " + itemId + " AND IS_DEFAULT_IMAGE = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Mrn_Replace_File_Upload>(dbConnection.dr);
            }
        }
    }
}
