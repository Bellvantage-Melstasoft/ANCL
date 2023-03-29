using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface TabulationMasterDAO
    {
        List<TabulationMaster> GetTabulationsByBidId(int BidId, DBConnection dbConnection);
        int PopulateRecommendation(int TabulationId, int CategoryId, decimal NetTotal, int UserId, int DesignationId, string Remarks, int Purchasetype, DBConnection dBConnection);
        List<int> GetRecommendableTabulations(int UserId, int DesignationId, DBConnection dBConnection);
        int OverrideTabulationRecommandation(int TabulationId, int UserId, int DesignationId, string Remarks, int Status, int CategoryId, decimal Amount, int PurchaseType, DBConnection dBConnection);
        List<int> GetApprovableTabulations(int UserId, int DesignationId, DBConnection dBConnection);
        int OverrideApproval(int TabulationId, int UserId, int DesignationId, string Remarks, int Status, DBConnection dBConnection);
        int InsertTabulationMaster(int prid, int bidId, int userId,int MesurementId, DBConnection dBConnection);
        int UpdateTabulationMasterNetTotal(int tabulationId, int prid, int bidId, int userId, string remarks, DBConnection dBConnection);
        TabulationMaster GetApprovedTabulation(int BidId, DBConnection dBConnection);
        int UpdateTabulationMasterNetTotalAfterUpdate(int tabulationId, int prid, int bidId, int userId, string remarks, DBConnection dBConnection);
        TabulationMaster GetTabulationsByTabulationId(int TabulationId, DBConnection dbConnection);
        List<TabulationMaster> GetTabulationsRejectedTabulationsByPrId(int PrId, DBConnection dbConnection);
        bool CheckApprovalLimitExist(int tabulationId, int itemCategoryId, DBConnection dbConnection);
        int UpdateTabulationMaster(decimal UserId, decimal totVAt, decimal totNbt, decimal totNetTot, decimal totSubTot, int BidId, int TabulationId, int PrId, string finalizedRemark, DBConnection dbConnection);
        int UpdateTabulationMasterNew(decimal UserId, decimal totVAt, decimal totNbt, decimal totNetTot, decimal totSubTot, int BidId, int TabulationId, int PrId, int Finalized, DBConnection dbConnection);
        int DeleteTabulationMaster(decimal UserId, int BidId, int TabulationId, int PrId, int Finalized, DBConnection dbConnection);
        int PopulateApproval(int TabulationId, int CategoryId, decimal NetTotal, int UserId, int DesignationId, string Remarks, DBConnection dBConnection);
        int OverrideRecommendation(int RecommandationId, int TabulationId, int UserId, int DesignationId, string Remarks, int Status, int CategoryId, decimal Amount, DBConnection dBConnection);
         int UpdateDeletedTabulationMaster(int TabulationId, int PrId, decimal subTotal, decimal Vat, decimal nbt, decimal netTotal, int BidId, DBConnection dbConnection);
        List<TabulationMaster> GetTabulationsByBidIdForPurchaseRequisitionReport(List<int> BidId, DBConnection dbConnection);
        List<int> GetRejectedRecommendableTabulations(int UserId, int DesignationId, DBConnection dBConnection);
        List<int> GetRejectedApprovableTabulations(int UserId, int DesignationId, DBConnection dBConnection);
        int CloneBid(int bidId, int PrId, int Userid,int openDays, DBConnection dBConnection);
        int TerminaeBid(int bidId, DBConnection dbConnection);
        int UpdateTabulationMasterNewImports(decimal UserId, decimal totVAt, decimal totNbt, decimal totNetTot, decimal totSubTot, int BidId, int TabulationId, int PrId, DBConnection dbConnection);
        int DeleteTabulationMasterNewImports(decimal UserId, decimal totVAt, decimal totNbt, decimal totNetTot, decimal totSubTot, int BidId, int TabulationId, int PrId, DBConnection dbConnection);
        bool CheckApprovalImportLimitExist(int tabulationId, int itemCategoryId, DBConnection dbConnection);
        int UpdateTabulationMasterImport(decimal UserId, int BidId, int TabulationId, int PrId, string finalizedRemark, DBConnection dbConnection);
        int CloneBidImport(int bidId, int PrId, int Userid, int openDays, DBConnection dBConnection);
        int UpdateTabulationMasterForCoveringPR(SupplierQuotation SupplierQuotation, int UserId, string Remark, DBConnection dbConnection);
        int UpdateBiddingForCoveringPR(decimal UserId, int BidId, DBConnection dbConnection);
    }

        class TabulationMasterDAOImpl : TabulationMasterDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<TabulationMaster> GetTabulationsByBidId(int BidId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT TABULATION_ID FROM TABULATION_MASTER WHERE BID_ID=" + BidId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            var Isexixt = dbConnection.cmd.ExecuteScalar();
            if (Isexixt != null)
            {
                dbConnection.cmd.Parameters.Clear(); 

                dbConnection.cmd.CommandText = "SELECT TM.*, CLS.SELECTED_BY_NAME, CL.RECOMMENDATION_OVERRIDDEN_BY_NAME, BID.BID_CODE, CLO.APPROVAL_OVERRIDDEN_BY_NAME FROM TABULATION_MASTER AS TM\n" +
                                                "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS CLC ON TM.CREATED_BY = CLC.USER_ID\n" +
                                                 "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS RECOMMENDATION_OVERRIDDEN_BY_NAME FROM COMPANY_LOGIN) AS CL ON TM.RECOMMENDATION_OVERRIDDEN_BY = CL.USER_ID\n" +
                                                "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVAL_OVERRIDDEN_BY_NAME FROM COMPANY_LOGIN) AS CLO ON TM.APPROVAL_OVERRIDDEN_BY = CLO.USER_ID\n" +

                                                 "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS SELECTED_BY_NAME FROM COMPANY_LOGIN) AS CLS ON TM.SELECTED_BY = CLS.USER_ID\n" +
                                                "LEFT JOIN (SELECT BID_ID, BID_CODE FROM BIDDING) AS BID ON BID.BID_ID = TM.BID_ID\n" +
                                                "WHERE TM.BID_ID=" + BidId;
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
                {
                    DataAccessObject dataAccessObject = new DataAccessObject();
                    return dataAccessObject.ReadCollection<TabulationMaster>(dbConnection.dr);
                }
            }
            else
            {
                return null;
            }

        }

        public int UpdateTabulationMasterNewImports(decimal UserId, decimal totVAt, decimal totNbt, decimal totNetTot, decimal totSubTot, int BidId, int TabulationId, int PrId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            string sql = "UPDATE TABULATION_MASTER SET SUB_TOTAL = SUB_TOTAL+" + totSubTot + ", VAT_AMOUNT = VAT_AMOUNT+" + totVAt + ", NBT_AMOUNT = NBT_AMOUNT+" + totNbt + " , NET_TOTAL = NET_TOTAL+" + totNetTot + " , IS_SELECTED = 1 , SELECTED_BY = " + UserId + " , SELECTED_ON = '" + LocalTime.Now + "' " +
                                           "WHERE TABULATION_ID = " + TabulationId + " AND BID_ID = " + BidId + " AND PR_ID = " + PrId + "  ";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();


        }

        public int UpdateTabulationMasterForCoveringPR(SupplierQuotation SupplierQuotation, int UserId, string Remark, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            int TabulationId = 0;
            dbConnection.cmd.CommandText = "SELECT TABULATION_ID FROM TABULATION_MASTER WHERE BID_ID = " + SupplierQuotation.BidId + "  ";
            TabulationId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (TabulationId > 0) {
                dbConnection.cmd.CommandText = "UPDATE TABULATION_MASTER SET SUB_TOTAL = " + SupplierQuotation.SubTotal + ", VAT_AMOUNT = " + SupplierQuotation.VatAmount + ", NBT_AMOUNT = " + SupplierQuotation.NbtAmount + " , NET_TOTAL = " + SupplierQuotation.NetTotal + " , IS_SELECTED = 1 ,SELECTION_REMARKS='"+ Remark + "', SELECTED_BY = " + UserId + " , SELECTED_ON = '" + LocalTime.Now + "' " +
                                               "WHERE BID_ID = " + SupplierQuotation.BidId + "  ";
            }
            
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            //return dbConnection.cmd.ExecuteNonQuery();
            return TabulationId;

        }


        public int DeleteTabulationMasterNewImports(decimal UserId, decimal totVAt, decimal totNbt, decimal totNetTot, decimal totSubTot, int BidId, int TabulationId, int PrId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            string sql = "UPDATE TABULATION_MASTER SET SUB_TOTAL = SUB_TOTAL-" + totSubTot + ", VAT_AMOUNT = VAT_AMOUNT-" + totVAt + ", NBT_AMOUNT = NBT_AMOUNT-" + totNbt + " , NET_TOTAL = NET_TOTAL-" + totNetTot + " , IS_SELECTED = 0 , SELECTED_BY = " + UserId + " , SELECTED_ON = '" + LocalTime.Now + "' " +
                                           "WHERE TABULATION_ID = " + TabulationId + " AND BID_ID = " + BidId + " AND PR_ID = " + PrId + "  ";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();


        }


        public List<TabulationMaster> GetTabulationsByBidIdForPurchaseRequisitionReport(List<int> BidId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT TABULATION_ID FROM TABULATION_MASTER WHERE BID_ID IN ("+string.Join (",", BidId) +") ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            var Isexixt = dbConnection.cmd.ExecuteScalar();
            if (Isexixt != null) {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT TM.*,OD.APPROVAL_OVERIDING_DESIGNATION_NAME,RD.RECOMMENDATION_OVERIDING_DESIGNATION_NAME, CLS.SELECTED_BY_NAME, CL.RECOMMENDATION_OVERRIDDEN_BY_NAME, BID.BID_CODE, CLO.APPROVAL_OVERRIDDEN_BY_NAME FROM TABULATION_MASTER AS TM\n" +
                                                "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS CLC ON TM.CREATED_BY = CLC.USER_ID\n" +
                                                 "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS RECOMMENDATION_OVERRIDDEN_BY_NAME FROM COMPANY_LOGIN) AS CL ON TM.RECOMMENDATION_OVERRIDDEN_BY = CL.USER_ID\n" +
                                                "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVAL_OVERRIDDEN_BY_NAME FROM COMPANY_LOGIN) AS CLO ON TM.APPROVAL_OVERRIDDEN_BY = CLO.USER_ID\n" +
                                                 "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME AS APPROVAL_OVERIDING_DESIGNATION_NAME FROM DESIGNATION) AS OD ON OD.DESIGNATION_ID = TM.APPROVAL_OVERIDING_DESIGNATION " +
                                                 "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME AS RECOMMENDATION_OVERIDING_DESIGNATION_NAME FROM DESIGNATION) AS RD ON RD.DESIGNATION_ID = TM.RECOMMENDATION_OVERIDING_DESIGNATION " +

                                                 "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS SELECTED_BY_NAME FROM COMPANY_LOGIN) AS CLS ON TM.SELECTED_BY = CLS.USER_ID\n" +
                                                "LEFT JOIN (SELECT BID_ID, BID_CODE FROM BIDDING) AS BID ON BID.BID_ID = TM.BID_ID\n" +
                                                "WHERE TM.BID_ID IN (" + string.Join(",", BidId) + ") ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                    DataAccessObject dataAccessObject = new DataAccessObject();
                    return dataAccessObject.ReadCollection<TabulationMaster>(dbConnection.dr);
                }
            }
            else {
                return null;
            }

        }

        public int PopulateRecommendation(int TabulationId, int CategoryId, decimal NetTotal, int UserId, int DesignationId, string Remarks, int Purchasetype, DBConnection dBConnection)
        {

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[POPULATE_RECOMMENDATION]";
            dBConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", TabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@CATEGORY_ID", CategoryId);
            dBConnection.cmd.Parameters.AddWithValue("@AMOUNT", NetTotal);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@SELECTION_REMARKS", Remarks);
            dBConnection.cmd.Parameters.AddWithValue("@PURCHASE_TYPE", Purchasetype);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int CloneBid(int bidId, int PrId, int Userid,int openDays, DBConnection dBConnection) {

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[CLONE_BID_IN_RECOMMANDATION]";
            dBConnection.cmd.Parameters.AddWithValue("@BID_ID", bidId);
            dBConnection.cmd.Parameters.AddWithValue("@PR_ID", PrId);
            dBConnection.cmd.Parameters.AddWithValue("@USER_ID", Userid);
            dBConnection.cmd.Parameters.AddWithValue("@COLNED_ON", LocalTime.Now);
            dBConnection.cmd.Parameters.AddWithValue("@EXPIRED_ON", LocalTime.Now.AddDays(openDays));

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int CloneBidImport(int bidId, int PrId, int Userid, int openDays, DBConnection dBConnection) {

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[CLONE_BID_IN_RECOMMANDATION_IMPORTS]";
            dBConnection.cmd.Parameters.AddWithValue("@BID_ID", bidId);
            dBConnection.cmd.Parameters.AddWithValue("@PR_ID", PrId);
            dBConnection.cmd.Parameters.AddWithValue("@USER_ID", Userid);
            dBConnection.cmd.Parameters.AddWithValue("@COLNED_ON", LocalTime.Now);
            dBConnection.cmd.Parameters.AddWithValue("@EXPIRED_ON", LocalTime.Now.AddDays(openDays));

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }


        public int TerminaeBid(int bidId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            string sql = "UPDATE BIDDING SET IS_REJECTED_BID_TERMINATED = 1 WHERE BID_ID = "+ bidId + " ";
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int PopulateApproval(int TabulationId, int CategoryId, decimal NetTotal, int UserId, int DesignationId, string Remarks, DBConnection dBConnection) {

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[POPULATE_APPROVAL]";
            dBConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", TabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@CATEGORY_ID", CategoryId);
            dBConnection.cmd.Parameters.AddWithValue("@AMOUNT", NetTotal);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@SELECTION_REMARKS", Remarks);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<int> GetRecommendableTabulations(int UserId, int DesignationId, DBConnection dBConnection)
        {
            List<int> ids = new List<int>();

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[GET_RECOMMENDABLE_TABULATIONS]";
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_DESIGNATION", DesignationId);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                if (dBConnection.dr.HasRows)
                {
                    while (dBConnection.dr.Read())
                    {
                        ids.Add(int.Parse(dBConnection.dr[0].ToString()));
                    }
                }
            }

            return ids;
        }

        public List<int> GetRejectedRecommendableTabulations(int UserId, int DesignationId, DBConnection dBConnection) {
            List<int> ids = new List<int>();

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[GET_REJECTED_RECOMMANDATION_TABULATIONS]";
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_DESIGNATION", DesignationId);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                if (dBConnection.dr.HasRows) {
                    while (dBConnection.dr.Read()) {
                        ids.Add(int.Parse(dBConnection.dr[0].ToString()));
                    }
                }
            }

            return ids;
        }


        public int OverrideTabulationRecommandation(int TabulationId, int UserId, int DesignationId, string Remarks, int Status, int CategoryId, decimal Amount, int PurchaseType,DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[OVERRIDE_TABULATION_RECOMMENDATION_MASTER]";
            dBConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", TabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);
            dBConnection.cmd.Parameters.AddWithValue("@STATUS", Status);
            dBConnection.cmd.Parameters.AddWithValue("@CATEGORY_ID", CategoryId);
            dBConnection.cmd.Parameters.AddWithValue("@AMOUNT", Amount);
            dBConnection.cmd.Parameters.AddWithValue("@PURCHASE_TYPE", PurchaseType);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int OverrideRecommendation(int RecommandationId, int TabulationId, int UserId, int DesignationId, string Remarks, int Status, int CategoryId, decimal Amount, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[OVERRIDE_TABULATION_RECOMMENDATION_DETAILS]";
            dBConnection.cmd.Parameters.AddWithValue("@RECOMMENDATION_ID", RecommandationId);
            dBConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", TabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);
            dBConnection.cmd.Parameters.AddWithValue("@STATUS", Status);
            dBConnection.cmd.Parameters.AddWithValue("@CATEGORY_ID", CategoryId);
            dBConnection.cmd.Parameters.AddWithValue("@AMOUNT", Amount);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }


        public int OverrideApproval(int TabulationId, int UserId, int DesignationId, string Remarks, int Status, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[OVERRIDE_TABULATION_APPRVOAL_MASTER]";
            dBConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", TabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);
            dBConnection.cmd.Parameters.AddWithValue("@STATUS", Status);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<int> GetApprovableTabulations(int UserId, int DesignationId, DBConnection dBConnection)
        {
            List<int> ids = new List<int>();

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[GET_APPROVABLE_TABULATIONS]";
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_DESIGNATION", DesignationId);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                if (dBConnection.dr.HasRows)
                {
                    while (dBConnection.dr.Read())
                    {
                        ids.Add(int.Parse(dBConnection.dr[0].ToString()));
                    }
                }
            }

            return ids;
        }

        public List<int> GetRejectedApprovableTabulations(int UserId, int DesignationId, DBConnection dBConnection) {
            List<int> ids = new List<int>();

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[GET_REJECTED_APPROVABLE_TABULATIONS]";
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_DESIGNATION", DesignationId);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                if (dBConnection.dr.HasRows) {
                    while (dBConnection.dr.Read()) {
                        ids.Add(int.Parse(dBConnection.dr[0].ToString()));
                    }
                }
            }

            return ids;
        }

        public int InsertTabulationMaster(int prid, int bidId, int userId,int MesurementId, DBConnection dBConnection)
        {

            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "[usp_InsertTabulationMaster]";
            dBConnection.cmd.Parameters.AddWithValue("@prid", prid);
            dBConnection.cmd.Parameters.AddWithValue("@bidId", bidId);
            dBConnection.cmd.Parameters.AddWithValue("@userId", userId);
            dBConnection.cmd.Parameters.AddWithValue("@mesurementId", MesurementId);
            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return int.Parse(dBConnection.cmd.ExecuteScalar().ToString());
        }
        public int UpdateTabulationMasterNetTotal(int tabulationId, int prid, int bidId, int userId, string remarks, DBConnection dBConnection)
        {

            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "[usp_UpdateTabulationMasterNetTotal]";
            dBConnection.cmd.Parameters.AddWithValue("@tabulationId", tabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@prid", prid);
            dBConnection.cmd.Parameters.AddWithValue("@bidId", bidId);
            dBConnection.cmd.Parameters.AddWithValue("@userId", userId);
            dBConnection.cmd.Parameters.AddWithValue("@remark", remarks);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return int.Parse(dBConnection.cmd.ExecuteScalar().ToString());
        }
        public TabulationMaster GetApprovedTabulation(int BidId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM TABULATION_MASTER AS TM\n" +
                "WHERE TM.IS_SELECTED= 1 AND TM.IS_APPROVED= 1 AND TM.BID_ID = " + BidId;
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<TabulationMaster>(dBConnection.dr);
            }
        }

        public int UpdateTabulationMasterNetTotalAfterUpdate(int tabulationId, int prid, int bidId, int userId, string remarks, DBConnection dBConnection)
        {

            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "[usp_UpdateTabulationMasterNetTotalAfterUpdate]";
            dBConnection.cmd.Parameters.AddWithValue("@tabulationId", tabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@prid", prid);
            dBConnection.cmd.Parameters.AddWithValue("@bidId", bidId);
            dBConnection.cmd.Parameters.AddWithValue("@userId", userId);
            dBConnection.cmd.Parameters.AddWithValue("@remark", remarks);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return int.Parse(dBConnection.cmd.ExecuteScalar().ToString());
        }

        public TabulationMaster GetTabulationsByTabulationId(int TabulationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM TABULATION_MASTER AS TM\n" +
                "WHERE TM.TABULATION_ID = " + TabulationId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<TabulationMaster>(dbConnection.dr);
            }
        }

        public List<TabulationMaster> GetTabulationsRejectedTabulationsByPrId(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM TABULATION_MASTER AS TM\n" +
                "INNER JOIN (SELECT USER_ID ,FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON  COLOG.USER_ID=TM.CREATED_BY\n" +
                "INNER JOIN (SELECT BID_CODE,BID_ID FROM BIDDING) AS BID ON  BID.BID_ID=TM.BID_ID\n" +
                "WHERE (TM.IS_RECOMMENDED= 2 OR TM.IS_APPROVED= 2) AND TM.PR_ID = " + PrId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationMaster>(dbConnection.dr);
            }
        }

        public bool CheckApprovalLimitExist(int tabulationId, int itemCategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "SELECT SUM(SUB_TOTAL) FROM " + dbLibrary + ".TABULATION_DETAIL " +
                                           "WHERE TABULATION_ID = " + tabulationId + "  ";
            if(dbConnection.cmd.ExecuteScalar().ToString() != "")
            { 
                decimal netTotal = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            
                // Checking Recommendation
                dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM APPROVAL_LIMITS " +
                                               " WHERE CATEGORY_ID=" + itemCategoryId + " " +
                                               " AND  MINIMUM_AMOUNT<= "+ netTotal + " AND MAXIMUM_AMOUNT >="+ netTotal + " "+
                                               " AND LIMIT_FOR=1 AND LIMIT_TYPE = 1 ";
                
                var isExist1 = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (isExist1 != 0)
                {
                    //I am seeing here because for every approval there is two recommend and approval (see the admin page Item catergory Approval Limit)
                    // Checking Approval
                    dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM APPROVAL_LIMITS " +
                                              " WHERE CATEGORY_ID=" + itemCategoryId + " " +
                                              " AND  MINIMUM_AMOUNT<= " + netTotal + " AND MAXIMUM_AMOUNT >=" + netTotal + " " +
                                              " AND LIMIT_FOR=2 AND LIMIT_TYPE = 1 ";
                    var isExist2 = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                    if (isExist2 != 0)
                    {
                        return true;
                    }else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }else
            {
                return false;
            }
        }
        public bool CheckApprovalImportLimitExist(int tabulationId, int itemCategoryId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "SELECT SUM(SUB_TOTAL) FROM " + dbLibrary + ".TABULATION_DETAIL " +
                                           "WHERE TABULATION_ID = " + tabulationId + "  ";
            if (dbConnection.cmd.ExecuteScalar().ToString() != "") {
                decimal netTotal = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                // Checking Recommendation
                dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM APPROVAL_LIMITS " +
                                               " WHERE CATEGORY_ID=" + itemCategoryId + " " +
                                               " AND  MINIMUM_AMOUNT<= " + netTotal + " AND MAXIMUM_AMOUNT >=" + netTotal + " " +
                                               " AND LIMIT_FOR=1 AND LIMIT_TYPE = 2 ";

                var isExist1 = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (isExist1 != 0) {
                    //I am seeing here because for every approval there is two recommend and approval (see the admin page Item catergory Approval Limit)
                    // Checking Approval
                    dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM APPROVAL_LIMITS " +
                                              " WHERE CATEGORY_ID=" + itemCategoryId + " " +
                                              " AND  MINIMUM_AMOUNT<= " + netTotal + " AND MAXIMUM_AMOUNT >=" + netTotal + " " +
                                              " AND LIMIT_FOR=2 AND LIMIT_TYPE = 2 ";
                    var isExist2 = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                    if (isExist2 != 0) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        public int UpdateTabulationMaster(decimal UserId, decimal totVAt, decimal totNbt, decimal totNetTot, decimal totSubTot, int BidId, int TabulationId, int PrId, string finalizedRemark, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            string sql = "UPDATE TABULATION_MASTER SET IS_SELECTED = 1 , SELECTED_BY = " + UserId + " , SELECTED_ON = '" + LocalTime.Now + "', SELECTION_REMARKS ='" + finalizedRemark + "' " +
                                           "WHERE TABULATION_ID = " + TabulationId + " AND BID_ID = " + BidId + " AND PR_ID = " + PrId + "  ";
            sql += "UPDATE BIDDING SET IS_QUOTATION_SELECTED = 1, IS_QUOTATION_APPROVED = 0, IS_QUOTATION_CONFIRMED = 0, QUOTATION_SELECTED_BY = " + UserId + ", QUOTATION_SELECTION_DATE = '" + LocalTime.Now + "' WHERE  BID_ID = " + BidId + " ";
            sql += "UPDATE PR_DETAIL SET CURRENT_STATUS= 5 WHERE PRD_ID IN(SELECT PRD_ID FROM BIDDING_ITEM WHERE BID_ID= " + BidId + " );  ";
            //sql += "INSERT INTO PR_DETAIL_STATUS_LOG SELECT PRD_ID,6,'" + LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BID_ID=" + BidId + " ";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();


        }

        public int UpdateBiddingForCoveringPR(decimal UserId, int BidId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
           string sql = "UPDATE BIDDING SET IS_QUOTATION_SELECTED = 1, IS_QUOTATION_APPROVED = 0, IS_QUOTATION_CONFIRMED = 0, QUOTATION_SELECTED_BY = " + UserId + ", QUOTATION_SELECTION_DATE = '" + LocalTime.Now + "' WHERE  BID_ID = " + BidId + " ";
            sql += "UPDATE PR_DETAIL SET CURRENT_STATUS= 5 WHERE PRD_ID IN(SELECT PRD_ID FROM BIDDING_ITEM WHERE BID_ID= " + BidId + " );  ";
           
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();


        }
        public int UpdateTabulationMasterImport(decimal UserId, int BidId, int TabulationId, int PrId, string finalizedRemark, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            string sql = "UPDATE TABULATION_MASTER SET IS_SELECTED = 1 , SELECTED_BY = " + UserId + " , SELECTED_ON = '" + LocalTime.Now + "', SELECTION_REMARKS ='" + finalizedRemark + "' " +
                                           "WHERE TABULATION_ID = " + TabulationId + " AND BID_ID = " + BidId + " AND PR_ID = " + PrId + "  ";
            sql += "UPDATE BIDDING SET IS_QUOTATION_SELECTED = 1, IS_QUOTATION_APPROVED = 0, IS_QUOTATION_CONFIRMED = 0, QUOTATION_SELECTED_BY = " + UserId + ", QUOTATION_SELECTION_DATE = '" + LocalTime.Now + "' WHERE  BID_ID = " + BidId + " ";
            sql += "UPDATE PR_DETAIL SET CURRENT_STATUS= 5 WHERE PRD_ID IN(SELECT PRD_ID FROM BIDDING_ITEM WHERE BID_ID= " + BidId + " );  ";
            //sql += "INSERT INTO PR_DETAIL_STATUS_LOG SELECT PRD_ID,6,'" + LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BID_ID=" + BidId + " ";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();


        }



        public int UpdateTabulationMasterNew(decimal UserId, decimal totVAt, decimal totNbt, decimal totNetTot, decimal totSubTot, int BidId, int TabulationId, int PrId, int Finalized, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            string sql = "UPDATE TABULATION_MASTER SET SUB_TOTAL = SUB_TOTAL+" + totSubTot + ", VAT_AMOUNT = VAT_AMOUNT+" + totVAt + ", NBT_AMOUNT = NBT_AMOUNT+" + totNbt + " , NET_TOTAL = NET_TOTAL+" + totNetTot + " , IS_SELECTED = 1 , SELECTED_BY = " + UserId + " , SELECTED_ON = '" + LocalTime.Now + "',IS_FINALIZED=" + Finalized + "" +
                                           "WHERE TABULATION_ID = " + TabulationId + " AND BID_ID = " + BidId + " AND PR_ID = " + PrId + "  ";
         
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();


        }


        public int DeleteTabulationMaster(decimal UserId, int BidId, int TabulationId, int PrId, int Finalized, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            string sql = "UPDATE TABULATION_MASTER SET  IS_SELECTED = 0 , SELECTED_BY = " + UserId + " , SELECTED_ON = '" + LocalTime.Now + "',IS_FINALIZED=" + Finalized + "" +
                                           "WHERE TABULATION_ID = " + TabulationId + " AND BID_ID = " + BidId + " AND PR_ID = " + PrId + "  ";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();


        }
        public int UpdateDeletedTabulationMaster(int TabulationId, int PrId, decimal subTotal, decimal Vat, decimal nbt, decimal netTotal, int BidId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            string sql = "UPDATE TABULATION_MASTER SET SUB_TOTAL =SUB_TOTAL-"+ subTotal + ", VAT_AMOUNT =VAT_AMOUNT-"+ Vat + ",   NBT_AMOUNT=NBT_AMOUNT-" + nbt + ", NET_TOTAL=NET_TOTAL-" + netTotal + "  " +
                                           "WHERE TABULATION_ID = " + TabulationId + " AND BID_ID = " + BidId + " AND PR_ID = " + PrId + "  ";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();


        }

    }
}
