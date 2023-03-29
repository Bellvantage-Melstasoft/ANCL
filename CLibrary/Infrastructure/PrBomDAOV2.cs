using CLibrary.Common;
using CLibrary.Domain;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface PrBomDAOV2 {
        int Save(PrBomV2 prBom, DBConnection dbConnection);
        int Delete(int bomId, DBConnection dbConnection);
        List<PrBomV2> GetPrdBomForEdit(int prdId, DBConnection dbConnection);
    }
    class PrBomDAOV2Impl : PrBomDAOV2 {
        public int Save(PrBomV2 prBom, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [PR_BOM] \n");
            sql.Append("    ([PRD_ID],[MATERIAL],[DESCRIPTION]) \n");
            sql.Append("VALUES \n");
            sql.Append("    (" + prBom.PrdId + ",'" + prBom.Material.ProcessString() + "','" + prBom.Description.ProcessString() + "')");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int Delete(int bomId, DBConnection dbConnection) {
            string sql = "DELETE FROM [PR_BOM] WHERE BOM_ID=" + bomId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PrBomV2> GetPrdBomForEdit(int prdId, DBConnection dbConnection) {

            string sql = "SELECT * FROM PR_BOM WHERE PRD_ID=" + prdId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<PrBomV2>(dbConnection.dr);
            }
        }
    }
}
