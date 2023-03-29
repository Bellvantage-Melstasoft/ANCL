using CLibrary.Common;
using CLibrary.Domain;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface PrSupportiveDocumentsDAOV2 {
        int Save(PrSupportiveDocumentV2 prSupportiveDocument, DBConnection dbConnection);
        int Delete(int fileId, DBConnection dbConnection);
        List<PrSupportiveDocumentV2> GetPrSupportiveDocumentsForEdit(int prdId, DBConnection dbConnection);
    }
    class PrSupportiveDocumentsDAOV2Impl : PrSupportiveDocumentsDAOV2 {
        public int Delete(int fileId, DBConnection dbConnection) {
            string sql = "DELETE FROM PR_SUPPORTIVE_DOCUMENTS WHERE FILE_ID=" + fileId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PrSupportiveDocumentV2> GetPrSupportiveDocumentsForEdit(int prdId, DBConnection dbConnection) {

            string sql = "SELECT FILE_ID,PRD_ID,FILE_NAME,FILE_PATH FROM PR_SUPPORTIVE_DOCUMENTS WHERE PRD_ID=" + prdId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<PrSupportiveDocumentV2>(dbConnection.dr);
            }
        }

        public int Save(PrSupportiveDocumentV2 prSupportiveDocument, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [PR_SUPPORTIVE_DOCUMENTS] \n");
            sql.Append("([PRD_ID],[FILE_NAME],[FILE_PATH],[FILE_DATA]) \n");
            sql.Append("VALUES \n");
            sql.Append("(" + prSupportiveDocument.PrdId + ",'" + prSupportiveDocument.FileName.ProcessString() + "','" + prSupportiveDocument.FilePath.ProcessString() + "',@FILE_DATA)");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.Parameters.AddWithValue("@FILE_DATA", prSupportiveDocument.FileData);
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
