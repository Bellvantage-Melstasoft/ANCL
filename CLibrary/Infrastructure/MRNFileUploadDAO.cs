using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public  interface MRNFileUploadDAO
    {
        int SaveFileUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName, DBConnection dbConnection);

        List<MRNFileUpload> FtechUploadeFiles(int Mrnid, int ItemId, DBConnection dbConnection);

        int DeleteFileUpload(int Mrnid, int itemId, DBConnection dbConnection);

        int DeleteParticularFile(int Mrnid, int itemId, string imagepath, DBConnection dbConnection);
    }

    public class MRNFileUploadDAOImpl : MRNFileUploadDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveFileUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".MRN_FILE_UPLOAD (DEPARTMENT_ID, ITEM_ID, MRN_ID, FILE_PATH,FILE_NAME,IS_ACTIVE) VALUES ( " + DepartmentId + ", " + ItemId + " , " + MrnId + ", '" + FilePath + "', '" + FileName + "', " + 1 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MRNFileUpload> FtechUploadeFiles(int Mrnid, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_FILE_UPLOAD WHERE MRN_ID = " + Mrnid + " AND ITEM_ID = " + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNFileUpload>(dbConnection.dr);
            }
        }

        public int DeleteFileUpload(int Mrnid, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_FILE_UPLOAD SET IS_ACTIVE=0 WHERE MRN_ID=" + Mrnid + " AND ITEM_ID = " + itemId + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int DeleteParticularFile(int Mrnid, int itemId, string imagepath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "delete from " + dbLibrary + ".MRN_FILE_UPLOAD  WHERE MRN_ID=" + Mrnid + " AND ITEM_ID = " + itemId + " AND FILE_PATH='" + imagepath + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
