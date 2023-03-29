using CLibrary.Common;
using CLibrary.Domain;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface PrCapexDocDAO {
        int Save(PrCapexDoc mrnReplacementFileUpload, DBConnection dbConnection);
        int Delete(int fileId, DBConnection dbConnection);
        List<PrCapexDoc> GetPrCapexDocsForEdit(int mrndId, DBConnection dbConnection);
        List<PrCapexDoc> GetPrCapexDocs(int prId, DBConnection dbConnection);
    }
    class PrCapexDocDAOImpl : PrCapexDocDAO {
        public int Save(PrCapexDoc doc, DBConnection dbConnection) {
            
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.Parameters.AddWithValue("@FILE_DATA", doc.FileData);

            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [PR_CAPEX_DOC] \n");
            sql.Append("([PR_ID],[FILE_NAME],[FILE_PATH],[FILE_DATA]) \n");
            sql.Append("VALUES \n");
            sql.Append("(" + doc.PrId + ",'" + doc.FileName.ProcessString() + "','" + doc.FilePath.ProcessString() + "',@FILE_DATA)");



            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int Delete(int fileId, DBConnection dbConnection) {
            string sql = "DELETE FROM PR_CAPEX_DOC WHERE FILE_ID=" + fileId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PrCapexDoc> GetPrCapexDocsForEdit(int mrndId, DBConnection dbConnection) {

            string sql = "SELECT FILE_ID,PR_ID,FILE_NAME,FILE_PATH FROM PR_CAPEX_DOC WHERE PR_ID=" + mrndId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<PrCapexDoc>(dbConnection.dr);
            }
        }
        public List<PrCapexDoc> GetPrCapexDocs(int prId, DBConnection dbConnection) {

            string sql = "SELECT FILE_ID,PR_ID,FILE_NAME,FILE_PATH FROM PR_CAPEX_DOC WHERE PR_ID=" + prId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<PrCapexDoc>(dbConnection.dr);
            }
        }
    }
}
