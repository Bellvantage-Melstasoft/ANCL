using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface BiddingItemDAO
    {
        //New Methods By Salman created on 2019-01-17
        List<BiddingItem> GetAllBidItems(int BidId, int CompanyId, DBConnection dbConnection);
        List<BiddingItem> FechBiddingItems(int BidId, DBConnection dbConnection);
        void GetLastPurchaseDetails(List<BiddingItem> items, int CompanyId, DBConnection dbConnection);
        List<BiddingItem> GetRejectedBidsItems(int bidId,int companyId,  DBConnection dbConnection);
        int TerminateBidItem(int BidItemId, int UserId, string Remarks, DBConnection dbConnection);
        int TerminateBidItems(List<int> BidItemIds, int UserId, string Remarks, DBConnection dbConnection);
        List<BiddingItem> GetAllBidItemsByMasterTables(int BidId, DBConnection dbConnection);
        List<BiddingItem> GetBiddingItems(List<int> BidIds, DBConnection dbConnection);
        List<BiddingItem> GetBiddingItemsList(int BidIds, DBConnection dbConnection);
    }
    class BiddingItemDAOImpl : BiddingItemDAO
    {
        public List<BiddingItem> GetAllBidItems(int BidId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            //original code plz uncommit
            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING_ITEM AS BI\n" +
                                          "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID, HS_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON BI.ITEM_ID = AIM.ITEM_ID\n" +
                                          "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                          "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                          "INNER JOIN (SELECT PRD_ID,DESCRIPTION,REMARKS,MEASUREMENT_ID,SPARE_PART_NUMBER FROM PR_DETAIL) AS PRD ON PRD.PRD_ID= BI.PRD_ID\n" +
                                          "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.MEASUREMENT_ID\n" +
                                          "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS TERMINATED_BY_NAME FROM COMPANY_LOGIN) AS TERMI_BY ON BI.TERMINATED_BY = TERMI_BY.USER_ID\n" +
                                          //"LEFT JOIN (SELECT COUNT(QUOTATION_ID) AS QUOTATION_COUNT FROM QUOTATION_ITEMS) AS QIT ON QIT.BID_ID = BI.BID_ID "+
                                          "WHERE BI.BID_ID = " + BidId;
            //plz remove this 
            //dbConnection.cmd.CommandText = "SELECT * FROM BIDDING_ITEM AS BI\n" +
            //                            "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID, HS_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON BI.ITEM_ID = AIM.ITEM_ID\n" +
            //                            "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
            //                            "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
            //                            "INNER JOIN (SELECT PRD_ID,DESCRIPTION,REMARKS,MEASUREMENT_ID FROM PR_DETAIL) AS PRD ON PRD.PRD_ID= BI.PRD_ID\n" +
            //                            "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.MEASUREMENT_ID\n" +
            //                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS TERMINATED_BY_NAME FROM COMPANY_LOGIN) AS TERMI_BY ON BI.TERMINATED_BY = TERMI_BY.USER_ID\n" +
            //                            //"LEFT JOIN (SELECT COUNT(QUOTATION_ID) AS QUOTATION_COUNT FROM QUOTATION_ITEMS) AS QIT ON QIT.BID_ID = BI.BID_ID "+
            //                            "WHERE BI.BID_ID = " + BidId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<BiddingItem>(dbConnection.dr);
            }
        }

        public List<BiddingItem> FechBiddingItems(int BidId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING_ITEM AS BI " +
                                            "INNER JOIN (SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS) AS AI ON AI.ITEM_ID = BI.ITEM_ID " +
                                            "INNER JOIN (SELECT PRD_ID, MEASUREMENT_ID FROM PR_DETAIL ) AS PRD ON PRD.PRD_ID = BI.PRD_ID "+
                                            "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID = PRD.MEASUREMENT_ID " +
                                            "WHERE BID_ID = " + BidId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<BiddingItem>(dbConnection.dr);
            }
        }



        public void GetLastPurchaseDetails(List<BiddingItem> items, int CompanyId, DBConnection dbConnection) {
            for (int i = 0; i < items.Count; i++) {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT TOP 1 POM.PO_ID,POM.SUPPLIER_ID,SUP.SUPPLIER_NAME,POD.ITEM_PRICE FROM PO_MASTER AS POM\n" +
                                                "INNER JOIN(SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) SUP ON POM.SUPPLIER_ID = SUP.SUPPLIER_ID\n" +
                                                "INNER JOIN(SELECT PO_ID, ITEM_ID, ITEM_PRICE FROM PO_DETAILS WHERE ITEM_ID =  " + items[i].ItemId + ") AS POD ON POM.PO_ID = POD.PO_ID\n" +
                                                "WHERE POM.PO_ID IN(SELECT PO_ID FROM PO_DETAILS WHERE ITEM_ID = " + items[i].ItemId + ") AND POM.IS_APPROVED = 1 AND DEPARTMENT_ID = " + CompanyId + "\n" +
                                                "ORDER BY POM.CREATED_DATE DESC";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                    if (dbConnection.dr.HasRows) {
                        dbConnection.dr.Read();

                        items[i].LastPoId = int.Parse(dbConnection.dr[0].ToString());
                        items[i].LSupplierId = int.Parse(dbConnection.dr[1].ToString());
                        items[i].LastSupplierName = dbConnection.dr[2].ToString();
                        items[i].LPurchasedPrice = decimal.Parse(dbConnection.dr[3].ToString());
                    }
                }
            }
        }

        public List<BiddingItem> GetRejectedBidsItems(int bidId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING_ITEM AS BI "+
                                            "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON BI.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + companyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + companyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +                                            
                                            "WHERE BI.BID_ID = " + bidId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<BiddingItem>(dbConnection.dr);
            }
        }

        public int TerminateBidItem(int BidItemId, int UserId, string Remarks, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE BIDDING_ITEM SET IS_TERMINATED = 1, TERMINATED_BY=" + UserId + ",TERMINATED_DATE= '" + LocalTime.Now + "',TERMINATION_REMARKS ='" + Remarks.ProcessString() + "' WHERE BIDDING_ITEM_ID=" + BidItemId + "; ");

            sql.AppendLine("UPDATE PR_DETAIL SET CURRENT_STATUS= 12 WHERE PRD_ID =(");
            sql.AppendLine("SELECT PRD_ID FROM BIDDING_ITEM WHERE BIDDING_ITEM_ID =" + BidItemId + ");");

            //Inserting PR_DETAIL Status Update log
            sql.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG");
            sql.AppendLine("SELECT PRD_ID,17,'" + LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BIDDING_ITEM_ID=" + BidItemId + ";");

            dbConnection.cmd.CommandText = sql.ToString();
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int TerminateBidItems(List<int> BidItemIds, int UserId, string Remarks, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE BIDDING_ITEM SET IS_TERMINATED = 1, TERMINATED_BY=" + UserId + ",TERMINATED_DATE= '" + LocalTime.Now + "',TERMINATION_REMARKS ='" + Remarks.ProcessString() + "' WHERE BIDDING_ITEM_ID IN(" + string.Join(",", BidItemIds) + "); ");

            sql.AppendLine("UPDATE PR_DETAIL SET CURRENT_STATUS= 12 WHERE PRD_ID IN (");
            sql.AppendLine("SELECT PRD_ID FROM BIDDING_ITEM WHERE BIDDING_ITEM_ID IN(" + string.Join(",", BidItemIds) + "));");

            //Inserting PR_DETAIL Status Update log
            sql.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG");
            sql.AppendLine("SELECT PRD_ID,17,'" + LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BIDDING_ITEM_ID IN(" + string.Join(",", BidItemIds) + ");");

            dbConnection.cmd.CommandText = sql.ToString();
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public List<BiddingItem> GetBiddingItems(List<int> BidIds, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING_ITEM AS BI " +
                                                "WHERE BID_ID IN (" + String.Join(",", BidIds) + ") ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<BiddingItem>(dbConnection.dr);
            }
        }

        public List<BiddingItem> GetBiddingItemsList(int BidIds, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING_ITEM AS BI " +
                                                "WHERE BID_ID = "+ BidIds + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<BiddingItem>(dbConnection.dr);
            }
        }
        public List<BiddingItem> GetAllBidItemsByMasterTables(int BidId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING_ITEM AS BI\n" +
                                             "INNER JOIN (SELECT ITEM_ID, ITEM_NAME, SUB_CATEGORY_ID FROM ADD_ITEMS_MASTER) AS AIM ON BI.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME, CATEGORY_ID FROM ITEM_SUB_CATEGORY_MASTER) AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT CATEGORY_ID, CATEGORY_NAME,CATEGORY_IMAGE FROM ITEM_CATEGORY_MASTER) AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT PRD_ID,ITEM_DESCRIPTION,PURPOSE,UNIT FROM PR_DETAIL) AS PRD ON PRD.PRD_ID= BI.PRD_ID\n" +
                                             "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.UNIT  " +
                                             "WHERE BI.BID_ID = " + BidId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<BiddingItem>(dbConnection.dr);
            }
        }
    }
}
