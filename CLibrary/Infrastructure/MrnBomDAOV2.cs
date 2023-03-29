using CLibrary.Common;
using CLibrary.Domain;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface MrnBomDAOV2 {
        int Save(MrnBomV2 mrnBom, DBConnection dbConnection);
        int Delete(int bomId, DBConnection dbConnection);
        List<MrnBomV2> GetMrndBomForEdit(int mrndId, DBConnection dbConnection);
    }
    class MrnBomDAOV2Impl : MrnBomDAOV2 {
        public int Save(MrnBomV2 mrnBom, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [MRN_BOM] \n");
            sql.Append("    ([MRND_ID],[MATERIAL],[DESCRIPTION]) \n");
            sql.Append("VALUES \n");
            sql.Append("    (" + mrnBom.MrndId + ",'" + mrnBom.Material.ProcessString() + "','" + mrnBom.Description.ProcessString() + "')");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int Delete(int bomId, DBConnection dbConnection) {
            string sql = "DELETE FROM [MRN_BOM] WHERE BOM_ID=" + bomId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MrnBomV2> GetMrndBomForEdit(int mrndId, DBConnection dbConnection) {

            string sql = "SELECT * FROM MRN_BOM WHERE MRND_ID=" + mrndId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<MrnBomV2>(dbConnection.dr);
            }
        }
    }
}
