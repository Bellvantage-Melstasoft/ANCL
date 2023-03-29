using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface PrFileUploadDAOV2 {
        int Save(PrFileUploadV2 prFileUpload, DBConnection dbConnection);
        int Delete(int fileId, DBConnection dbConnection);
        List<PrFileUploadV2> GetPrFileUploadForEdit(int prdId, DBConnection dbConnection);
    }
    class PrFileUploadDAOV2Impl : PrFileUploadDAOV2 {
        public int Save(PrFileUploadV2 prFileUpload, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [PR_FILE_UPLOAD] \n");
            sql.Append("([PRD_ID],[FILE_NAME],[FILE_PATH],[FILE_DATA]) \n");
            sql.Append("VALUES \n");
            sql.Append("(" + prFileUpload.PrdId + ",'" + prFileUpload.FileName.ProcessString() + "','" + prFileUpload.FilePath.ProcessString() + "',@FILE_DATA)");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.Parameters.AddWithValue("@FILE_DATA", prFileUpload.FileData);
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int Delete(int fileId, DBConnection dbConnection) {
            string sql = "DELETE FROM PR_FILE_UPLOAD WHERE FILE_ID=" + fileId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PrFileUploadV2> GetPrFileUploadForEdit(int prdId, DBConnection dbConnection) {

            string sql = "SELECT FILE_ID,PRD_ID,FILE_NAME,FILE_PATH FROM PR_FILE_UPLOAD WHERE PRD_ID=" + prdId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<PrFileUploadV2>(dbConnection.dr);
            }
        }
    }
}
