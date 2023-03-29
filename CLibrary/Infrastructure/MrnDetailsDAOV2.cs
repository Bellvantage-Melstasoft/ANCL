using CLibrary.Common;
using CLibrary.Domain;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface MrnDetailsDAOV2 {
        int Save(MrnDetailsV2 mrnDetail, DBConnection dbConnection);
        int Update(MrnDetailsV2 mrnDetail, DBConnection dbConnection);
        int Delete(int mrndId, DBConnection dbConnection);
        List<MrnDetailsV2> GetMrnDetailsForEdit(int mrnId, DBConnection dbConnection);
        List<MrnDetailsV2> GetMrnDetails(int mrnId, DBConnection dbConnection);
        List<MrnDetailsV2> GetMrnDetailsForPR(int mrnId, DBConnection dbConnection);
    }
    class MrnDetailsDAOV2Impl : MrnDetailsDAOV2 {

        public int Save(MrnDetailsV2 mrnDetail, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [MRN_DETAILS] \n");
            sql.Append("([MRN_ID],[ITEM_ID],[DESCRIPTION],[ESTIMATED_AMOUNT],[REQUESTED_QTY],[DEPARTMENT_STOCK],[FILE_SAMPLE_PROVIDED],[REPLACEMENT],[MEASUREMENT_ID], \n");
            sql.Append("[REMARKS],[ISSUED_QTY],[RECEIVED_QTY],[IS_ACTIVE],[STATUS], [SPARE_PART_NUMBER]) \n");
            sql.Append("OUTPUT inserted.MRND_ID \n");
            sql.Append("VALUES \n");
            sql.Append("(" + mrnDetail.MrnId + "," + mrnDetail.ItemId + ",'" + mrnDetail.Description.ProcessString() + "'," + mrnDetail.EstimatedAmount + "," + mrnDetail.RequestedQty + "," + mrnDetail.DepartmentStock + "," + mrnDetail.FileSampleProvided + "," + mrnDetail.Replacement + "," + mrnDetail.DetailId + ", \n");
            sql.Append("'" + mrnDetail.Remarks.ProcessString() + "',0,0,1,(SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='APR'), '" + mrnDetail.SparePartNo.ProcessString() + "')");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public int Update(MrnDetailsV2 mrnDetail, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();

            dbConnection.cmd.CommandText = "SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='MDFD' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            sql.Append("UPDATE [MRN_DETAILS] \n");
            sql.Append("   SET [DESCRIPTION] ='" + mrnDetail.Description.ProcessString() + "' \n");
            sql.Append("      ,[ESTIMATED_AMOUNT] =" + mrnDetail.EstimatedAmount + " \n");
            sql.Append("      ,[REQUESTED_QTY] =" + mrnDetail.RequestedQty + " \n");
            sql.Append("      ,[DEPARTMENT_STOCK] =" + mrnDetail.DepartmentStock + " \n");
            sql.Append("      ,[FILE_SAMPLE_PROVIDED] =" + mrnDetail.FileSampleProvided + " \n");
            sql.Append("      ,[REPLACEMENT] =" + mrnDetail.Replacement + " \n");
            sql.Append("      ,[MEASUREMENT_ID] =" + mrnDetail.DetailId + " \n");
            sql.Append("      ,[REMARKS] ='" + mrnDetail.Remarks.ProcessString() + "' \n");
            sql.Append("      ,[STATUS] ='" + Status + "' \n");
            sql.Append("      ,[SPARE_PART_NUMBER] ='" + mrnDetail.SparePartNo + "' \n");
            sql.Append(" WHERE MRND_ID=" + mrnDetail.MrndId);

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int Delete(int mrndId, DBConnection dbConnection) {
            string sql = "DELETE FROM [MRN_DETAILS] WHERE MRND_ID=" + mrndId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MrnDetailsV2> GetMrnDetailsForEdit(int mrnId, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT MRND.*,MD.SHORT_CODE,AIM.ITEM_NAME,ISNULL(UM.MEASUREMENT_SHORT_NAME,'Not Found') AS MEASUREMENT_SHORT_NAME FROM [MRN_DETAILS] AS MRND \n");
            sql.Append("INNER JOIN ADD_ITEMS_MASTER AS AIM ON MRND.ITEM_ID=AIM.ITEM_ID \n");
            sql.Append("INNER JOIN MEASUREMENT_DETAIL AS MD ON MRND.MEASUREMENT_ID=MD.DETAIL_ID \n");
            sql.Append("LEFT JOIN UNIT_MEASUREMENT AS UM ON AIM.MEASUREMENT_ID = UM.MEASUREMENT_ID \n");
            sql.Append("WHERE MRND.MRN_ID="+ mrnId);

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<MrnDetailsV2>(dbConnection.dr);
            }
        }

        public List<MrnDetailsV2> GetMrnDetails(int mrnId, DBConnection dbConnection) {
            

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRN_DETAILS WHERE MRN_ID = " + mrnId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<MrnDetailsV2>(dbConnection.dr);
            }
        }
        public List<MrnDetailsV2> GetMrnDetailsForPR(int mrnId, DBConnection dbConnection) {


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRN_DETAILS AS MRN "+
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME, SUB_CATEGORY_ID, CATEGORY_ID FROM ADD_ITEMS) AS AI ON AI.ITEM_ID = MRN.ITEM_ID "+
                                            "INNER JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = AI.CATEGORY_ID "+
                                            "INNER JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS ISC ON ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID "+
                                            "INNER JOIN(SELECT MRN_DETAILS_STATUS_ID, STATUS_NAME FROM DEF_MRN_DETAILS_STATUS) AS MRNDL ON MRNDL.MRN_DETAILS_STATUS_ID = MRN.STATUS " +
                                            "INNER JOIN(SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS MDE ON MDE.DETAIL_ID = MRN.MEASUREMENT_ID " +
                                            "WHERE MRN_ID = " + mrnId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<MrnDetailsV2>(dbConnection.dr);
            }
        }
    }
}
