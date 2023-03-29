using CLibrary.Common;
using CLibrary.Domain;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface PrReplacementFileUploadDAOV2 {
        int Save(PrReplacementFileUploadV2 prReplacementFileUpload, DBConnection dbConnection);
        int Delete(int fileId, DBConnection dbConnection);
        List<PrReplacementFileUploadV2> GetPrReplacementFileUploadForEdit(int prdId, DBConnection dbConnection);
    }
    class PrReplacementFileUploadDAOV2Impl : PrReplacementFileUploadDAOV2 {
        public int Save(PrReplacementFileUploadV2 prReplacementFileUpload, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [PR_REPLACE_FILE_UPLOAD] \n");
            sql.Append("([PRD_ID],[FILE_NAME],[FILE_PATH],[FILE_DATA]) \n");
            sql.Append("VALUES \n");
            sql.Append("(" + prReplacementFileUpload.PrdId + ",'" + prReplacementFileUpload.FileName.ProcessString() + "','" + prReplacementFileUpload.FilePath.ProcessString() + "',@FILE_DATA)");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.Parameters.AddWithValue("@FILE_DATA", prReplacementFileUpload.FileData);
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int Delete(int fileId, DBConnection dbConnection) {
            string sql = "DELETE FROM PR_REPLACE_FILE_UPLOAD WHERE FILE_ID=" + fileId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PrReplacementFileUploadV2> GetPrReplacementFileUploadForEdit(int prdId, DBConnection dbConnection) {

            string sql = "SELECT FILE_ID,PRD_ID,FILE_NAME,FILE_PATH FROM PR_REPLACE_FILE_UPLOAD WHERE PRD_ID=" + prdId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<PrReplacementFileUploadV2>(dbConnection.dr);
            }
        }
    }
}
