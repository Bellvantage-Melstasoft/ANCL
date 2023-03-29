using CLibrary.Common;
using CLibrary.Domain;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface MrnCapexDocDAO {
        int Save(MrnCapexDoc mrnReplacementFileUpload, DBConnection dbConnection);
        int Delete(int fileId, DBConnection dbConnection);
        List<MrnCapexDoc> GetMrnCapexDocsForEdit(int mrndId, DBConnection dbConnection);
        List<MrnCapexDoc> GetMrnCapexDocs(int mrndId, DBConnection dbConnection);
    }
    class MrnCapexDocDAOImpl : MrnCapexDocDAO {
        public int Save(MrnCapexDoc doc, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [MRN_CAPEX_DOC] \n");
            sql.Append("([MRN_ID],[FILE_NAME],[FILE_PATH],[FILE_DATA]) \n");
            sql.Append("VALUES \n");
            sql.Append("(" + doc.MrnId + ",'" + doc.FileName.ProcessString() + "','" + doc.FilePath.ProcessString() + "',@FILE_DATA)");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.Parameters.AddWithValue("@FILE_DATA", doc.FileData);
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int Delete(int fileId, DBConnection dbConnection) {
            string sql = "DELETE FROM MRN_CAPEX_DOC WHERE FILE_ID=" + fileId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MrnCapexDoc> GetMrnCapexDocsForEdit(int mrndId, DBConnection dbConnection) {

            string sql = "SELECT FILE_ID,MRN_ID,FILE_NAME,FILE_PATH FROM MRN_CAPEX_DOC WHERE MRN_ID=" + mrndId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<MrnCapexDoc>(dbConnection.dr);
            }
        }

        public List<MrnCapexDoc> GetMrnCapexDocs(int mrndId, DBConnection dbConnection) {

            string sql = "SELECT FILE_ID,MRN_ID,FILE_NAME,FILE_PATH FROM MRN_CAPEX_DOC WHERE MRN_ID=" + mrndId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<MrnCapexDoc>(dbConnection.dr);
            }
        }
    }
}
