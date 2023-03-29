using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface PrDetailsDAOV2 {
        int Save(PrDetailsV2 prDetail, DBConnection dbConnection);
        int Update(PrDetailsV2 prDetail, DBConnection dbConnection);
        int Delete(int prdId, DBConnection dbConnection);
        List<PrDetailsV2> GetPrDetailsForEdit(int prId, DBConnection dbConnection);
        PrDetailsV2 GetPrDetails(int PrId, DBConnection dbConnection);
        List<PrDetailsV2> GetPrItemDetails(int PrId, DBConnection dbConnection);
        int DeleteFromPO(int prId, List<int> itemIds, int UserId, DBConnection dbConnection);
        int UpdatePrDetailsStatus(List<int> TabulationDetailIds, DBConnection dbConnection);
        List<TabulationDetail> GetItemIds(List<int> TabulationDetailIds, DBConnection dbConnection);
        List<PrDetailsV2> GetPrItemS(int PrId, DBConnection dbConnection);
    }
    class PrDetailsDAOV2Impl : PrDetailsDAOV2 {

        public int Save(PrDetailsV2 prDetail, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO [PR_DETAIL] \n");
            sql.Append("([PR_ID],[ITEM_ID],[DESCRIPTION],[ESTIMATED_AMOUNT],[REQUESTED_QTY],[WAREHOUSE_STOCK],[FILE_SAMPLE_PROVIDED],[REPLACEMENT],[MEASUREMENT_ID], \n");
            sql.Append("[REMARKS],[IS_ACTIVE],[CURRENT_STATUS],[SPARE_PART_NUMBER]) \n");
            sql.Append("OUTPUT inserted.PRD_ID \n");
            sql.Append("VALUES \n");
            sql.Append("(" + prDetail.PrId + "," + prDetail.ItemId + ",'" + prDetail.Description.ProcessString() + "'," + prDetail.EstimatedAmount + "," + prDetail.RequestedQty + "," + prDetail.WarehouseStock + "," + prDetail.FileSampleProvided + "," + prDetail.Replacement + "," + prDetail.DetailId + ", \n");
            sql.Append("'" + prDetail.Remarks.ProcessString() + "',1,(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='APR'), '" + prDetail.SparePartNo.ProcessString() + "')");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public int Update(PrDetailsV2 prDetail, DBConnection dbConnection) {

            dbConnection.cmd.CommandText = "SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='MDFD' ";
            int Status = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE [PR_DETAIL] \n");
            sql.Append("   SET [DESCRIPTION] ='" + prDetail.Description.ProcessString() + "' \n");
            sql.Append("      ,[ESTIMATED_AMOUNT] =" + prDetail.EstimatedAmount + " \n");
            sql.Append("      ,[REQUESTED_QTY] =" + prDetail.RequestedQty + " \n");
            sql.Append("      ,[WAREHOUSE_STOCK] =" + prDetail.WarehouseStock + " \n");
            sql.Append("      ,[FILE_SAMPLE_PROVIDED] =" + prDetail.FileSampleProvided + " \n");
            sql.Append("      ,[REPLACEMENT] =" + prDetail.Replacement + " \n");
            sql.Append("      ,[MEASUREMENT_ID] =" + prDetail.DetailId + " \n");
            sql.Append("      ,[REMARKS] ='" + prDetail.Remarks.ProcessString() + "' \n");
            sql.Append("      ,[CURRENT_STATUS] ='" + Status + "' \n");
            sql.Append("      ,[SPARE_PART_NUMBER] ='" + prDetail.SparePartNo.ProcessString() + "' \n");
            sql.Append(" WHERE PRD_ID=" + prDetail.PrdId);

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int Delete(int prdId, DBConnection dbConnection) {
            string sql = "DELETE FROM [PR_DETAIL] WHERE PRD_ID=" + prdId;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PrDetailsV2> GetPrDetailsForEdit(int prId, DBConnection dbConnection) {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT PRD.*,AIM.ITEM_NAME,ISNULL(MD.SHORT_CODE,'Not Found') AS MEASUREMENT_SHORT_NAME FROM [PR_DETAIL] AS PRD \n");
            sql.Append("INNER JOIN ADD_ITEMS_MASTER AS AIM ON PRD.ITEM_ID=AIM.ITEM_ID \n");
            sql.Append("INNER JOIN MEASUREMENT_DETAIL AS MD ON PRD.MEASUREMENT_ID=MD.DETAIL_ID \n");
            sql.Append("WHERE PRD.PR_ID="+ prId);

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();

                return dataAccessObject.ReadCollection<PrDetailsV2>(dbConnection.dr);
            }
        }
        public PrDetailsV2 GetPrDetails(int PrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM PR_DETAIL AS PD\n" +
                                             "WHERE PD.PR_ID = " + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PrDetailsV2>(dbConnection.dr);
            }
        }
        public List<TabulationDetail> GetItemIds(List<int> TabulationDetailIds, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT DISTINCT(ITEM_ID), TABULATION_DETAIL_ID, TABULATION_ID FROM TABULATION_DETAIL AS TD\n" +
                                             "WHERE TD.TABULATION_DETAIL_ID IN (" + string.Join(",", TabulationDetailIds) + ") ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationDetail>(dbConnection.dr);
            }
        }

        public int UpdatePrDetailsStatus(List<int> TabulationDetailIds, DBConnection dbConnection) {
            //dbConnection.cmd.Parameters.Clear();
            List<TabulationDetail> detailsList = DAOFactory.CreatePrDetailsDAOV2().GetItemIds(TabulationDetailIds, dbConnection);

            for (int i = 0; i< detailsList.Count; i++) {
                int itemId = detailsList[i].ItemId;
                int tabDetailId = detailsList[i].TabulationDetailId;
                int tabulationId = detailsList[i].TabulationId;

                //dbConnection.cmd.CommandText = "IF NOT EXISTS (SELECT * FROM TABULATION_DETAIL WHERE  ITEM_ID = " + itemId + " AND IS_TERMINATED = 0 AND TABULATION_ID = " + tabulationId + " ) " +
                //                                "BEGIN " +
                //                                "UPDATE PR_DETAIL SET CURRENT_STATUS= (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='TERM') WHERE PRD_ID IN " +
                //                                "(SELECT PRD_ID FROM BIDDING_ITEM WHERE BIDDING_ITEM_ID IN (SELECT BIDDING_ITEM_ID FROM SUPPLIER_QUOTATION_ITEM WHERE QUOTATION_ITEM_ID IN(SELECT QUOTATION_ITEM_ID FROM TABULATION_DETAIL WHERE TABULATION_DETAIL_ID = " + tabDetailId + " )))" +
                //                                 "END ";
                dbConnection.cmd.CommandText = "SELECT COUNT(ITEM_ID) FROM TABULATION_DETAIL WHERE ITEM_ID ="+ itemId + " AND TABULATION_ID = "+ tabulationId + " AND IS_SELECTED = 1 ";
                int Count1 = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                dbConnection.cmd.CommandText = "SELECT COUNT(ITEM_ID) FROM TABULATION_DETAIL WHERE ITEM_ID =" + itemId + " AND TABULATION_ID = " + tabulationId + " AND IS_TERMINATED =1 ";
                int Count2 = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (Count1 == Count2) {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText += "UPDATE PR_DETAIL SET CURRENT_STATUS= (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='TERM') WHERE PRD_ID IN " +
                                                "(SELECT PRD_ID FROM BIDDING_ITEM WHERE BIDDING_ITEM_ID IN (SELECT BIDDING_ITEM_ID FROM SUPPLIER_QUOTATION_ITEM WHERE QUOTATION_ITEM_ID IN(SELECT QUOTATION_ITEM_ID FROM TABULATION_DETAIL WHERE TABULATION_DETAIL_ID = " + tabDetailId + " )))";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    dbConnection.cmd.ExecuteNonQuery();
                }
                

            }

            return 1;

            //return dbConnection.cmd.ExecuteNonQuery();
        }

        

        public List<PrDetailsV2> GetPrItemDetails(int PrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM PR_DETAIL AS PD "+
                                            "LEFT JOIN(SELECT ITEM_ID, CATEGORY_ID, SUB_CATEGORY_ID, ITEM_NAME FROM ADD_ITEMS) AS AI ON AI.ITEM_ID = PD.ITEM_ID " +
                                             "LEFT JOIN(SELECT PR_ID, EXPENSE_TYPE FROM PR_MASTER) AS PM ON PD.PR_ID = PM.PR_ID " +
                                            "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY_MASTER) AS CM ON CM.CATEGORY_ID = AI.CATEGORY_ID " +
                                            "LEFT JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY_MASTER) AS SCM ON SCM.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID " +
                                            "LEFT JOIN(SELECT STATUS_NAME, PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS) AS PRDS ON PRDS.PR_DETAILS_STATUS_ID = PD.CURRENT_STATUS " +
                                            "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS MDE ON MDE.DETAIL_ID = PD.MEASUREMENT_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID, ITEM_ID, AVAILABLE_QTY FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = (SELECT WAREHOUSE_ID FROM PR_MASTER WHERE PR_ID = "+ PrId + " )) AS WIM ON WIM.ITEM_ID = AI.ITEM_ID "+
                                            "WHERE PD.PR_ID = " +PrId+" ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrDetailsV2>(dbConnection.dr);
            }
        }

        public List<PrDetailsV2> GetPrItemS(int PrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM PR_DETAIL AS PD " +
                                            "WHERE PD.PR_ID = " + PrId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrDetailsV2>(dbConnection.dr);
            }
        }
        public int DeleteFromPO(int prId, List<int> itemIds, int UserId, DBConnection dbConnection) {
            //dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "UPDATE PR_DETAIL SET  CURRENT_STATUS = 14  WHERE PRD_ID IN(" + String.Join(",", PrdIds) + "); ";
            //for (int i = 0; i < PrdIds.Count; i++) {
            //    dbConnection.cmd.CommandText += "INSERT INTO PR_DETAIL_STATUS_LOG VALUES (" + PrdIds[i] + ",22,'" + LocalTime.Now + "'," + UserId + ");";
            //}
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG");
            sql.AppendLine("SELECT PRD_ID,17,'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL WHERE PR_ID = " + prId + " AND ITEM_ID  IN(" + String.Join(",", itemIds) + ")");

            sql.Append("	UPDATE PR_DETAIL SET CURRENT_STATUS=12 \n");
            sql.Append("	WHERE PR_ID = " + prId + " AND \n");
            sql.Append("	ITEM_ID IN(" + String.Join(",", itemIds) + ")");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
