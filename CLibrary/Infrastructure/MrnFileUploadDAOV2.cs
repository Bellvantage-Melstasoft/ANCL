using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface MrnFileUploadDAOV2 {
        int Save(MrnFileUploadV2 mrnFileUpload, DBConnection dbConnection);
        int Delete(int fileId, DBConnection dbConnection);
        List<MrnFileUploadV2> GetMrnFileUploadForEdit(int mrndId, DBConnection dbConnection);
    }
    class MrnFileUploadDAOV2Impl : MrnFileUploadDAOV2 {
        public int Save(MrnFileUploadV2 mrnFileUpload, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [MRN_FILE_UPLOAD] \n");
            sql.Append("([MRND_ID],[FILE_NAME],[FILE_PATH],[FILE_DATA]) \n");
            sql.Append("VALUES \n");
            sql.Append("(" + mrnFileUpload.MrndId + ",'" + mrnFileUpload.FileName.ProcessString() + "','" + mrnFileUpload.FilePath.ProcessString() + "',@FILE_DATA)");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.Parameters.AddWithValue("@FILE_DATA", mrnFileUpload.FileData);
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int Delete(int fileId, DBConnection dbConnection) {
            string sql = "DELETE FROM MRN_FILE_UPLOAD WHERE FILE_ID=" + fileId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MrnFileUploadV2> GetMrnFileUploadForEdit(int mrndId, DBConnection dbConnection) {

            string sql = "SELECT FILE_ID,MRND_ID,FILE_NAME,FILE_PATH FROM MRN_FILE_UPLOAD WHERE MRND_ID=" + mrndId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<MrnFileUploadV2>(dbConnection.dr);
            }
        }
    }
}
