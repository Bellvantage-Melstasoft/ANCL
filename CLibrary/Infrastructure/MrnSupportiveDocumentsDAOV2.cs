using CLibrary.Common;
using CLibrary.Domain;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface MrnSupportiveDocumentsDAOV2 {
        int Save(MrnSupportiveDocumentV2 mrnSupportiveDocument, DBConnection dbConnection);
        int Delete(int fileId, DBConnection dbConnection);
        List<MrnSupportiveDocumentV2> GetMrnSupportiveDocumentsForEdit(int mrndId, DBConnection dbConnection);
    }
    class MrnSupportiveDocumentsDAOV2Impl : MrnSupportiveDocumentsDAOV2 {
        public int Delete(int fileId, DBConnection dbConnection) {
            string sql = "DELETE FROM MRN_SUPPORTIVE_DOCUMENTS WHERE FILE_ID=" + fileId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MrnSupportiveDocumentV2> GetMrnSupportiveDocumentsForEdit(int mrndId, DBConnection dbConnection) {

            string sql = "SELECT FILE_ID,MRND_ID,FILE_NAME,FILE_PATH,FILE_DATA FROM MRN_SUPPORTIVE_DOCUMENTS WHERE MRND_ID=" + mrndId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<MrnSupportiveDocumentV2>(dbConnection.dr);
            }
        }

        public int Save(MrnSupportiveDocumentV2 mrnSupportiveDocument, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [MRN_SUPPORTIVE_DOCUMENTS] \n");
            sql.Append("([MRND_ID],[FILE_NAME],[FILE_PATH],[FILE_DATA]) \n");
            sql.Append("VALUES \n");
            sql.Append("(" + mrnSupportiveDocument.MrndId + ",'" + mrnSupportiveDocument.FileName.ProcessString() + "','" + mrnSupportiveDocument.FilePath.ProcessString() + "',@FILE_DATA)");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.Parameters.AddWithValue("@FILE_DATA", mrnSupportiveDocument.FileData);
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
