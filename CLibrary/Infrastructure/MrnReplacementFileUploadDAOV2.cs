using CLibrary.Common;
using CLibrary.Domain;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface MrnReplacementFileUploadDAOV2 {
        int Save(MrnReplacementFileUploadV2 mrnReplacementFileUpload, DBConnection dbConnection);
        int Delete(int fileId, DBConnection dbConnection);
        List<MrnReplacementFileUploadV2> GetMrnReplacementFileUploadForEdit(int mrndId, DBConnection dbConnection);
    }
    class MrnReplacementFileUploadDAOV2Impl : MrnReplacementFileUploadDAOV2 {
        public int Save(MrnReplacementFileUploadV2 mrnReplacementFileUpload, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [MRN_REPLACE_FILE_UPLOAD] \n");
            sql.Append("([MRND_ID],[FILE_NAME],[FILE_PATH],[FILE_DATA]) \n");
            sql.Append("VALUES \n");
            sql.Append("(" + mrnReplacementFileUpload.MrndId + ",'" + mrnReplacementFileUpload.FileName.ProcessString() + "','" + mrnReplacementFileUpload.FilePath.ProcessString() + "',@FILE_DATA)");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.Parameters.AddWithValue("@FILE_DATA", mrnReplacementFileUpload.FileData);
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int Delete(int fileId, DBConnection dbConnection) {
            string sql = "DELETE FROM MRN_REPLACE_FILE_UPLOAD WHERE FILE_ID=" + fileId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MrnReplacementFileUploadV2> GetMrnReplacementFileUploadForEdit(int mrndId, DBConnection dbConnection) {

            string sql = "SELECT FILE_ID,MRND_ID,FILE_NAME,FILE_PATH FROM MRN_REPLACE_FILE_UPLOAD WHERE MRND_ID=" + mrndId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<MrnReplacementFileUploadV2>(dbConnection.dr);
            }
        }
    }
}
