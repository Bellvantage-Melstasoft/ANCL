using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface MRsupportiveDocumentDAO
    {
        int SaveSupporiveFileUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName, DBConnection dbConnection);

        List<MRNSupportiveDocument> FtechUploadeSupporiveFiles(int Mrnid, int itemId, DBConnection dbConnection);

        int DeleteParticularSupporiveFile(int Mrnid, int itemId, string imagepath, DBConnection dbConnection);
    }

    public class MRsupportiveDocumentDAOImpl : MRsupportiveDocumentDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveSupporiveFileUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".MRN_SUPPORTIVE_DOCUMENTS (DEPARTMENT_ID, ITEM_ID, MRN_ID, FILE_PATH,FILE_NAME,IS_ACTIVE) VALUES ( " + DepartmentId + ", " + ItemId + " , " + MrnId + ", '" + FilePath + "', '" + FileName + "', " + 1 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }


        public List<MRNSupportiveDocument> FtechUploadeSupporiveFiles(int Mrnid, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_SUPPORTIVE_DOCUMENTS WHERE MRN_ID = " + Mrnid + " AND ITEM_ID = " + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MRNSupportiveDocument>(dbConnection.dr);
            }
        }
        public int DeleteParticularSupporiveFile(int Mrnid, int itemId, string imagepath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "delete from " + dbLibrary + ".MRN_SUPPORTIVE_DOCUMENTS  WHERE MRN_ID=" + Mrnid + " AND ITEM_ID = " + itemId + " AND FILE_PATH='" + imagepath + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
