using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface SupplierQuotationDAO
    {
        int SaveQuatation(int itemId, int prId, int supplierId, decimal itemPrice, int isSelected, string remarks, decimal vatAmount, decimal nbtAmount, decimal totatlAmount, int isRejected, string reason, int approve, string SupplierTermsConditions, string BidOrderingNo, int isVatIncluded, DBConnection dBConnection);
        int UpdateQuatationByQuotationId(int quotationId, decimal itemPrice, string remarks, DBConnection dBConnection);
        List<SupplierQuotation> GetQuotationListByItemId(int itemId, DBConnection dBConnection);
        List<SupplierQuotation> GetPendingBids(DBConnection dBConnection);
        List<SupplierQuotation> GetPendingBidsForDashboard(int departmentID, DBConnection dBConnection);
        List<SupplierQuotation> GetCompletedBidForDashboard(int departmentID, DBConnection dBConnection);
        List<SupplierQuotation> GetCompletedBid(DBConnection dBConnection);
        List<SupplierQuotation> GetDetailsBidComparison(int PrId, int companyId, DBConnection dBConnection);
        List<SupplierQuotation> GetBidSupplierListForItem(int PRid, int itemId, DBConnection dBConnection);
        int UpdateIsRejectedSupplier(int PrId, int ItemId, int SupplierId, string Reason, decimal CustomizeAmount, int selectedCount, int rejectedCount, DBConnection dBConnection);
        int UpdateIsApproveSupplier(int PrId, int ItemId, int SupplierId, decimal CustomizeAmount, int selectedCount, int rejectedCount, DBConnection dBConnection);
        List<SupplierQuotation> GetNecessaryDataForPO(int PrId, DBConnection dBConnection);
        List<SupplierQuotation> GetIsApprovedCount(int PrId, DBConnection dBConnection);
        List<SupplierQuotation> GetSupplierPendingBids(int SupplierId, DBConnection dBConnection);
        int UpdatePendingBids(int quotationId, decimal PerItemPrice, decimal VatAmount, decimal NbtAmount, decimal TotalAmount, string TermsandConditions, int isVatInclude, DBConnection dBConnection);
        List<SupplierQuotation> GetPendingCountOfSupplier(int PrId, int ItemId, int SupplierId, DBConnection dBConnection);
        List<SupplierQuotation> GetAlreadyBidCountOfSupplier(int PrId, int ItemId, int SupplierId, DBConnection dBConnection);
        int resetSelecteingSuppliert(int PrId, int ItemId, DBConnection dbConnection);
        int UpdateIsRaisedPO(int PrId, int ItemId, int SupplierId, int isPoRaised, DBConnection dbConnection);
        int UpdateIsPOApproved(int PrId, int ItemId, int SupplierId, int isPOApproved, DBConnection dbConnection);
        List<SupplierQuotation> GetDetailsSubmitPO(int PrId, DBConnection dBConnection);
        int UpdateIsRaisedPOAndIsPOApproved(int PrId, int ItemId, int SupplierId, int isPOApproved, DBConnection dbConnection);
        List<SupplierQuotation> GetPendingBidsAndNotBid(DBConnection dBConnection);
        SupplierQuotation GetGivenQuotatios(int PrId, int ItemId, int SupplierId, DBConnection dbConnection);
        //---------po reject
        List<SupplierQuotation> GetSuppliersList(int PRid, int itemId, DBConnection dBConnection);
        int UpdateIsRaisedPOReject(int PrId, int ItemId, int SupplierId, int isPoRaised, string rejectedReason, int rejectedCount, DBConnection dBConnection);
        List<SupplierQuotation> GetSupplierPrIdItemId(int PrId, int ItemId, DBConnection dBConnection);
        int UpdateResetPoReject(int prId, int itemId, DBConnection dBConnection);
        List<SupplierQuotation> GetManualPendingBidsAndNotBid(DBConnection dBConnection);
        List<SupplierQuotation> GetCompletedManualBid(DBConnection dBConnection);

        SupplierQuotation GetSupplierEditBidDetails(int PrId, int ItemId, int SupplierId, DBConnection dBConnection);


        List<SupplierQuotation> GetBidSupplierForItem(int PRid, int itemId, DBConnection dBConnection);
        int UpdateNegotiateAmount(int PrId, int ItemId, int SupplierId, decimal CustomizeAmount, int selectedCount, int rejectedCount, decimal VatAmount, decimal NbtAmount, decimal TotalAmount, DBConnection dBConnection);


        //New Methods By Salman created on 2019-01-17
        List<SupplierQuotation> GetSupplierQuotations(int BidId, DBConnection dBConnection);
        int SaveSupplierQuotation(SupplierQuotation quotation, ImportQuotation importQuotation , int PurchaseType, DBConnection dBConnection);
        int UpdateSupplierQuotation(SupplierQuotation quotation, DBConnection dBConnection);
        int UpdateSupplierPendingQuotation(SupplierQuotation quotation, DBConnection dBConnection);
        int SelectSupplierQuotationAtSelection(int QuotationId, string Remarks, int BidId, int UserId, DBConnection dBConnection);
        int SelectSupplierQuotationAtApproval(int QuotationId, string Remarks, DBConnection dBConnection);
        int SelectSupplierQuotationAtConfirmation(int QuotationId, string Remarks, DBConnection dBConnection);
        int ResetSelections(int BidId, DBConnection dBConnection);
        int RejectSupplierQuotationAtSelection(int QuotationId, string Remarks, int UserId, DBConnection dBConnection);
        int RejectSupplierQuotationsAtApproval(int QuotationId, string Remarks, int BidId, DBConnection dBConnection);
        int RejectSupplierQuotationsAtConfirmation(int QuotationId, string Remarks, int BidId, DBConnection dBConnection);
        int ApproveSupplierQuotation(int QuotationId, string Remarks, DBConnection dBConnection);
        int ConfirmSupplierQuotation(int QuotationId, string Remarks, DBConnection dBConnection);
        SupplierQuotation GetSupplierQuotationForABid(int BidId, int SupplierId, int CompanyId, DBConnection dBConnection);
        SupplierQuotation GetSelectedQuotation(int BidId, DBConnection dBConnection);
        List<int> GetSelectableQuotationIdsForLoggedInUser(int UserId, int DesignationId, int CompanyId, DBConnection dBConnection);
        List<int> GetSelectionPendingQuotationIdsForLoggedInUser(int UserId, int DesignationId, int CompanyId, DBConnection dBConnection);
        int PopulateRecommendation(int QuotationId, int CategoryId, decimal NetTotal,int UserId, int DesignationId,string Remarks, DBConnection dBConnection);
        List<int> GetRecommendableQuotations(int UserId, int DesignationId, DBConnection dBConnection);
        int OverrideRecommendation(int QuotationId, int UserId, int DesignationId, string Remarks, int Status, int CategoryId, decimal Amount, DBConnection dBConnection);
        List<int> GetApprovableQuotations(int UserId, int DesignationId, DBConnection dBConnection);
        int OverrideApproval(int QuotationId, int UserId, int DesignationId, string Remarks, int Status, DBConnection dBConnection);

        SupplierQuotation GetSupplierQuotationbyQutationId(int QutationId, DBConnection dBConnection);
        int DeleteSubmittedSupplierQuotation(int quotationId, int itemId, int quotationItemId, decimal subTotal, decimal vatAmount, decimal nbtAmount, decimal netTotal, DBConnection dbConnection);
        SupplierQuotation GetImportDetails(int poId, int CompanyId, DBConnection dBConnection);
        List<SupplierQuotation> ConfirmRates(int BidId, DBConnection dBConnection);
        List<SupplierQuotation> GetImportDetailsListForTabulationReview(int BidId, DBConnection dBConnection);
        List<int> GetQuotationsByBidId(int bidId, DBConnection dBConnection);
        int UpdateSupplierImports(int QuotationId, DBConnection dBConnection);
        List<SupplierQuotation> GetSupplierQuotationsImports(int BidId, DBConnection dBConnection);
        int UpdateSupplierQuotationImports(SupplierQuotation quotation, DBConnection dBConnection);
        SupplierQuotation GetSupplierQuotationsForCoveringPR(int BidId, DBConnection dBConnection);
    }

    public class SupplierQuotationDAOSQLImpl : SupplierQuotationDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int resetSelecteingSuppliert(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_QUOTATION  SET   IS_APPROVED = " + 0 + " , IS_REJECTED = " + 0 + " , REASON ='" + "" + "' WHERE  PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public List<SupplierQuotation> GetQuotationListByItemId(int itemId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_QUOTATION  WHERE ITEM_ID = " + itemId + "";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public int SaveQuatation(int itemId, int prId, int supplierId, decimal itemPrice, int isSelected, string remarks, decimal vatAmount, decimal nbtAmount, decimal totatlAmount, int isRejected, string reason, int approve, string SupplierTermsConditions, string BidOrderingNo, int isVatIncluded, DBConnection dBConnection)
        {
            int quotationId = 0;

            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_QUOTATION";
            var count = decimal.Parse(dBConnection.cmd.ExecuteScalar().ToString());
            if (count == 0)
            {
                quotationId = 40000;
            }
            else
            {
                dBConnection.cmd.CommandText = "SELECT MAX (QUOTATION_NO) + 1 AS MAXid FROM " + dbLibrary + ".SUPPLIER_QUOTATION";
                quotationId = int.Parse(dBConnection.cmd.ExecuteScalar().ToString());
            }
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_QUOTATION (ITEM_ID , PR_ID , SUPPLIER_ID , QUOTATION_NO, PER_ITEM_PRICE,IS_SELECTED,REMARKS, VAT_AMOUNT,NBT_AMOUNT,TOTAL_AMOUNT,IS_REJECTED,REASON, IS_APPROVED,SUPPLIER_TERMS_CONDITIONS,BID_OPENING_ID,SUPPLIER_SELECTED_COUNT,SUPPLIER_REJECTED_COUNT,IS_PO_RAISED,IS_PO_REJECTED,IS_PO_APPROVED,IS_VAT_INCLUDED ) VALUES (" + itemId + "," + prId + "," + supplierId + "," + quotationId + "," + itemPrice + "," + isSelected + ",'" + remarks + "'," + vatAmount + "," + nbtAmount + ", " + totatlAmount + "," + isRejected + ",'" + reason + "'," + approve + ",'" + SupplierTermsConditions + "','" + BidOrderingNo + "'," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + isVatIncluded + ")";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;
            dBConnection.cmd.ExecuteNonQuery();
            return quotationId;
        }

        public int UpdateQuatationByQuotationId(int quotationId, decimal itemPrice, string remarks, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_QUOTATION  SET   PER_ITEM_PRICE = " + itemPrice + " , REMARKS = '" + remarks + "' WHERE  QUOTATION_NO = " + quotationId + "";
            return dBConnection.cmd.ExecuteNonQuery();

        }

        public List<SupplierQuotation> GetPendingBids(DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = " SELECT SQ.ITEM_ID, SQ.PR_ID,AI.ITEM_NAME," +
                                           " sum(SQ.IS_SELECTED) as pending, " +
                                           " (COUNT(SQ.IS_SELECTED) - sum(SQ.IS_SELECTED)) as submitted, " +
                                           " COUNT(SQ.IS_SELECTED) as total, " +
                                           " PM.PR_CODE, " +
                                           " BI.START_DATE, " +
                                           " BI.END_DATE " +
                                           " from  " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ " +
                                           " inner join " + dbLibrary + ".BIDDING AS BI ON SQ.ITEM_ID = BI.ITEM_ID AND " +
                                           " SQ.PR_ID = BI.PR_ID " +
                                           " inner join  " + dbLibrary + ".PR_MASTER AS PM  ON PM.PR_ID = BI.PR_ID " +
                                           " inner join  " + dbLibrary + ".ADD_ITEMS AS AI ON SQ.ITEM_ID = AI.ITEM_ID " +
                                           " group by  SQ.ITEM_ID, SQ.PR_ID,AI.ITEM_NAME ,BI.START_DATE,BI.END_DATE,PM.PR_CODE;";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }
        public List<SupplierQuotation> GetPendingBidsForDashboard(int departmentID, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            //dBConnection.cmd.CommandText = " SELECT *" +
            //    " from " + dbLibrary + ".PR_MASTER AS PM" +
            //    " inner join " + dbLibrary + ".PR_DETAIL AS PD ON PM.PR_ID = PD.PR_ID  " +
            //    " inner join " + dbLibrary + ".ADD_ITEMS AS AI ON PD.ITEM_ID = AI.ITEM_ID  " +
            //    " left join  " + dbLibrary + ".BIDDING  AS BI ON BI.ITEM_ID = PD.ITEM_ID  " +
            //    " and BI.PR_ID = PD.PR_ID  " +
            //    " left join " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ ON SQ.ITEM_ID = BI.ITEM_ID  " +
            //    " AND SQ.PR_ID = BI.PR_ID  " +
            //    " where PD.SUBMIT_FOR_BID = 1 AND PD.BID_TYPE_MANUAL_BID=1 AND BI.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL=1 AND  PM.DEPARTMENT_ID=" + departmentID + " AND PM.CREATED_DATETIME like '%" + LocalTime.Today.Year + "%'; ";
            dBConnection.cmd.CommandText = " SELECT *" +
                                           " from " + dbLibrary + ".BIDDING AS BI " +
                                           " inner join " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = BI.PR_ID " +
                                           " WHERE (BI.END_DATE > '" +  LocalTime.Now + "')  and BI.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL =1 AND  PM.DEPARTMENT_ID=" + departmentID + " AND PM.CREATED_DATETIME like '%" + LocalTime.Today.Year + "%'; ";

            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public List<SupplierQuotation> GetCompletedBid(DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = " SELECT SQ.ITEM_ID, SQ.PR_ID, AI.ITEM_NAME,BI.START_DATE,BI.END_DATE," +
                                           " sum(SQ.IS_SELECTED) as pending," +
                                           " COUNT(SQ.IS_SELECTED) as total, " +
                                           " ( COUNT(SQ.IS_SELECTED)- sum(SQ.IS_SELECTED)) as submitted,PM.PR_CODE " +
                                           " from " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ " +
                                           " INNER JOIN " + dbLibrary + ".BIDDING AS BI ON (BI.PR_ID = SQ.PR_ID)" +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON (SQ.ITEM_ID = AI.ITEM_ID) AND (BI.ITEM_ID = SQ.ITEM_ID) " +
                                           " inner join " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = BI.PR_ID " +
                                           " WHERE (BI.END_DATE < '" +  LocalTime.Now + "')  and BI.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL =1 " +
                                           " group by  SQ.ITEM_ID, SQ.PR_ID,AI.ITEM_NAME,BI.START_DATE,BI.END_DATE,PM.PR_CODE";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }
        public List<SupplierQuotation> GetCompletedBidForDashboard(int departmentID, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            //dBConnection.cmd.CommandText = " SELECT *" +
            //                               " from " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ " +
            //                               " INNER JOIN " + dbLibrary + ".BIDDING AS BI ON (BI.PR_ID = SQ.PR_ID)" +
            //                               " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON (SQ.ITEM_ID = AI.ITEM_ID) AND (BI.ITEM_ID = SQ.ITEM_ID) " +
            //                               " inner join " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = BI.PR_ID " +
            //                               " WHERE (BI.END_DATE < '" +  LocalTime.Now + "')  and BI.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL =1 AND  PM.DEPARTMENT_ID=" + departmentID + " AND PM.CREATED_DATETIME like '%" + LocalTime.Today.Year + "%'; ";
            dBConnection.cmd.CommandText = " SELECT *" +
                                           " from " + dbLibrary + ".BIDDING AS BI " +
                                           " inner join " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = BI.PR_ID " +
                                           " WHERE (BI.END_DATE < '" +  LocalTime.Now + "')  and BI.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL =1 AND  PM.DEPARTMENT_ID=" + departmentID + " AND PM.CREATED_DATETIME like '%" + LocalTime.Today.Year + "%'; ";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        //-----------Bid Comparison
        //-----------Bid Comparison
        public List<SupplierQuotation> GetDetailsBidComparison(int PrId, int companyId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = " SELECT SQ.ITEM_ID, SQ.PR_ID,\n" +
                                            " AI.ITEM_NAME,PD.ITEM_QUANTITY,SQ.BID_OPENING_ID,\n " +
                                            " sum(SQ.IS_SELECTED) as pending,\n " +
                                            " sum(SQ.IS_APPROVED) as selectSupplier,\n " +
                                            " sum(SQ.IS_REJECTED) as bidedSupplierRejected,\n " +
                                            " sum(SQ.IS_PO_RAISED) as PoApproved,\n " +
                                            " COUNT(SQ.IS_SELECTED) as total\n " +
                                            " from " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ\n " +
                                            " INNER JOIN " + dbLibrary + ".BIDDING AS BI ON (BI.PR_ID = SQ.PR_ID)\n " +
                                            " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON (SQ.ITEM_ID = AI.ITEM_ID)\n " +
                                            " AND (BI.ITEM_ID = SQ.ITEM_ID)\n " +
                                            " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PD.PR_ID = SQ.PR_ID\n  " +
                                            " AND SQ.ITEM_ID = PD.ITEM_ID\n " +
                                            " WHERE (BI.END_DATE < '" +  LocalTime.Now + "') AND SQ.TOTAL_AMOUNT != 0\n " +
                                            " AND SQ.PR_ID =  " + PrId + " AND AI.COMPANY_ID = " + companyId + "\n" +
                                            " group by  SQ.ITEM_ID, SQ.PR_ID,AI.ITEM_NAME,PD.ITEM_QUANTITY,SQ.BID_OPENING_ID   HAVING sum(SQ.IS_APPROVED) >= 0 and sum(SQ.IS_PO_RAISED)= 0 ";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public List<SupplierQuotation> GetBidSupplierListForItem(int PRid, int itemId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = " SELECT SQ.PR_ID,SQ.ITEM_ID,SU.SUPPLIER_ID,SQ.BID_OPENING_ID, SQ.IS_VAT_INCLUDED, " +
                                           " SU.SUPPLIER_NAME,SQ.PER_ITEM_PRICE,PD.ITEM_QUANTITY, " +
                                           " (SQ.PER_ITEM_PRICE * PD.ITEM_QUANTITY) AS AMOUNT, " +
                                           " ((SQ.PER_ITEM_PRICE * 2.00)/98.00) AS NBT, " +
                                           " ((SQ.PER_ITEM_PRICE * PD.ITEM_QUANTITY) + ((SQ.PER_ITEM_PRICE * 2.00)/98.00)) AS EXPENSE, " +
                                           " (((SQ.PER_ITEM_PRICE * PD.ITEM_QUANTITY) + ((SQ.PER_ITEM_PRICE * 2.00)/98.00))* 0.15) AS VAT, " +
                                           " ((SQ.PER_ITEM_PRICE * PD.ITEM_QUANTITY) + ((SQ.PER_ITEM_PRICE * 2.00)/98.00) + (((SQ.PER_ITEM_PRICE * PD.ITEM_QUANTITY) + ((SQ.PER_ITEM_PRICE * 2.00)/98.00))* 0.15) ) AS TOTALPRICE " +
                                           " FROM " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ INNER JOIN " + dbLibrary + ".BIDDING AS BI ON SQ.PR_ID = BI.PR_ID AND SQ.TOTAL_AMOUNT > 0 " +
                                           " AND BI.END_DATE < '" +  LocalTime.Now + "' INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PD.PR_ID = BI.PR_ID " +
                                           " AND PD.ITEM_ID = BI.ITEM_ID AND BI.ITEM_ID = SQ.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID = SQ.SUPPLIER_ID where PD.PR_ID = " + PRid + " AND SQ.ITEM_ID=" + itemId + " AND SQ.IS_REJECTED= '0' ORDER BY (TOTALPRICE) DESC";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public int UpdateIsRejectedSupplier(int PrId, int ItemId, int SupplierId, string Reason, decimal CustomizeAmount, int selectedCount, int rejectedCount, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_QUOTATION SET IS_REJECTED = 1, REASON= '" + Reason + "', CUSTOMIZE_AMOUNT=" + CustomizeAmount + ",SUPPLIER_SELECTED_COUNT=" + selectedCount + ",SUPPLIER_REJECTED_COUNT=" + rejectedCount + "   WHERE PR_ID = " + PrId + " AND ITEM_ID= " + ItemId + " AND SUPPLIER_ID = " + SupplierId + ";";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierQuotation> GetNecessaryDataForPO(int PrId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ " +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON SQ.PR_ID = PD.PR_ID AND SQ.ITEM_ID = PD.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = SQ.PR_ID " +
                                           " WHERE SQ.IS_APPROVED =1 AND SQ.IS_PO_RAISED =0 AND SQ.PR_ID =" + PrId + "";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public int UpdateIsApproveSupplier(int PrId, int ItemId, int SupplierId, decimal CustomizeAmount, int selectedCount, int rejectedCount, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "update " + dbLibrary + ".SUPPLIER_QUOTATION SET IS_REJECTED = 0, REASON = '', IS_APPROVED = 1,CUSTOMIZE_AMOUNT=" + CustomizeAmount + ",SUPPLIER_SELECTED_COUNT=" + selectedCount + ",SUPPLIER_REJECTED_COUNT=" + rejectedCount + "  WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + " AND SUPPLIER_ID = " + SupplierId + " ";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierQuotation> GetIsApprovedCount(int PrId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_QUOTATION WHERE IS_APPROVED = 1 AND PR_ID = " + PrId + "";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public List<SupplierQuotation> GetSupplierPendingBids(int SupplierId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT DISTINCT * FROM " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ" +
                                           " INNER JOIN " + dbLibrary + ".BIDDING AS BI ON BI.ITEM_ID = SQ.ITEM_ID AND BI.PR_ID = SQ.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS_MASTER AS AI ON AI.ITEM_ID = SQ.ITEM_ID  " +
                                           " INNER JOIN " + dbLibrary + ".ITEM_IMAGE_UPLOAD AS IIU ON IIU.ITEM_ID = AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID  = SQ.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON CD.DEPARTMENT_ID = PM.DEPARTMENT_ID " +
                                           " WHERE SQ.IS_SELECTED = '1'  " +
                                           " AND SQ.TOTAL_AMOUNT = '0' AND SQ.SUPPLIER_ID =" + SupplierId + " " +
                                           " AND BI.END_DATE > '" +  LocalTime.Now + "'";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public int UpdatePendingBids(int quotationId, decimal PerItemPrice, decimal VatAmount, decimal NbtAmount, decimal TotalAmount, string TermsandConditions, int isVatInclude, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = " UPDATE " + dbLibrary + ".SUPPLIER_QUOTATION " +
                                           " SET PER_ITEM_PRICE = " + PerItemPrice + "  , IS_SELECTED = 0 , VAT_AMOUNT = " + VatAmount + ", NBT_AMOUNT =" + NbtAmount + ", " +
                                           " TOTAL_AMOUNT = " + TotalAmount + " ,SUPPLIER_TERMS_CONDITIONS = '" + TermsandConditions + "', IS_STAYED_AS_LATER_BID =1 , IS_VAT_INCLUDED  = " + isVatInclude + "" +
                                           " WHERE QUOTATION_NO = " + quotationId + "  ";
            int updateStatus = dBConnection.cmd.ExecuteNonQuery();
            if (updateStatus > 0)
            {
                return quotationId;
            }
            else
            {
                return 0;
            }

        }

        public List<SupplierQuotation> GetPendingCountOfSupplier(int PrId, int ItemId, int SupplierId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = " SELECT * FROM  " + dbLibrary + ".SUPPLIER_QUOTATION " +
                                           " WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + " AND IS_SELECTED = 1 AND SUPPLIER_ID = " + SupplierId + " ";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public List<SupplierQuotation> GetAlreadyBidCountOfSupplier(int PrId, int ItemId, int SupplierId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = " SELECT * FROM  " + dbLibrary + ".SUPPLIER_QUOTATION " +
                                           " WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + " AND IS_SELECTED = 0 AND SUPPLIER_ID = " + SupplierId + " AND " +
                                           " TOTAL_AMOUNT > 0 ";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public int UpdateIsRaisedPO(int PrId, int ItemId, int SupplierId, int isPoRaised, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".SUPPLIER_QUOTATION SET IS_PO_RAISED = " + isPoRaised + "   WHERE PR_ID = " + PrId + " AND ITEM_ID= " + ItemId + " AND SUPPLIER_ID = " + SupplierId + ";";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateIsPOApproved(int PrId, int ItemId, int SupplierId, int isPOApproved, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".SUPPLIER_QUOTATION SET IS_PO_RAISED = " + isPOApproved + "   WHERE PR_ID = " + PrId + " AND ITEM_ID= " + ItemId + " AND SUPPLIER_ID = " + SupplierId + ";";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierQuotation> GetDetailsSubmitPO(int PrId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = " SELECT SQ.ITEM_ID, SQ.PR_ID, " +
                                            " AI.ITEM_NAME,PD.ITEM_QUANTITY,SQ.BID_OPENING_ID, " +
                                            " sum(SQ.IS_SELECTED) as pending, " +
                                            " sum(SQ.IS_APPROVED) as selectSupplier, " +
                                            " sum(SQ.IS_PO_RAISED) as poRaisedSq, " +
                                            " sum(SQ.IS_PO_APPROVED) as isPoApprovedSq, " +
                                            " sum(SQ.IS_SELECTED) as total, " +
                                            " sum(PD.IS_PO_REJECTED_COUNT) as rejCount " +
                                            " from " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ" +
                                            " INNER JOIN " + dbLibrary + ".PO_MASTER AS POM  ON POM.BASED_PR = SQ.PR_ID " +
                                            " INNER JOIN " + dbLibrary + ".BIDDING AS BI ON (BI.PR_ID = SQ.PR_ID) " +
                                            " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON (SQ.ITEM_ID = AI.ITEM_ID) " +
                                            " AND (BI.ITEM_ID = SQ.ITEM_ID) " +
                                            " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PD.PR_ID = SQ.PR_ID  " +
                                            " AND SQ.ITEM_ID = PD.ITEM_ID " +
                                            " WHERE (BI.END_DATE < '" +  LocalTime.Now + "') AND SQ.TOTAL_AMOUNT != 0 " +
                                            " AND SQ.PR_ID =  " + PrId + " AND AI.COMPANY_ID = " + 1 + "" +
                                            " group by  SQ.ITEM_ID, SQ.PR_ID,AI.ITEM_NAME,PD.ITEM_QUANTITY,SQ.BID_OPENING_ID " +
                                            " HAVING sum(SQ.IS_APPROVED) >= 0 and sum(SQ.IS_PO_RAISED) =  0  and sum(PD.IS_PO_REJECTED_COUNT) > 0 ";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public int UpdateIsRaisedPOAndIsPOApproved(int PrId, int ItemId, int SupplierId, int isPOApproved, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".SUPPLIER_QUOTATION SET IS_PO_APPROVED = " + isPOApproved + "   WHERE PR_ID = " + PrId + " AND ITEM_ID= " + ItemId + " AND SUPPLIER_ID = " + SupplierId + ";";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierQuotation> GetSuppliersList(int PRid, int itemId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM   " + dbLibrary + ".SUPPLIER_QUOTATION WHERE PR_ID=" + PRid + " AND ITEM_ID=" + itemId + " AND (IS_REJECTED =1 OR IS_APPROVED = 1) ";

            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public int UpdateIsRaisedPOReject(int PrId, int ItemId, int SupplierId, int isPoRaised, string rejectedReason, int rejectedCount, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".SUPPLIER_QUOTATION SET IS_PO_RAISED = " + isPoRaised + ",REASON='" + rejectedReason + "', SUPPLIER_REJECTED_COUNT=" + rejectedCount + "   WHERE PR_ID = " + PrId + " AND ITEM_ID= " + ItemId + " AND SUPPLIER_ID = " + SupplierId + ";";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierQuotation> GetSupplierPrIdItemId(int PrId, int ItemId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = " SELECT * FROM  " + dbLibrary + ".SUPPLIER_QUOTATION " +
                                           " WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + "  AND IS_APPROVED = 1";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public List<SupplierQuotation> GetPendingBidsAndNotBid(DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText =
                " SELECT PD.ITEM_ID, PD.PR_ID,AI.ITEM_NAME, " +
                " COALESCE(sum(SQ.IS_SELECTED),0) as pending,  " +
                " COALESCE((COUNT(SQ.IS_SELECTED) - sum(SQ.IS_SELECTED)),0) as submitted,  " +
                " COUNT(SQ.IS_SELECTED) as total,  " +
                " PM.PR_CODE, BI.START_DATE,BI.END_DATE  " +
                " from " + dbLibrary + ".PR_MASTER AS PM" +
                " inner join " + dbLibrary + ".PR_DETAIL AS PD ON PM.PR_ID = PD.PR_ID  " +
                " inner join " + dbLibrary + ".ADD_ITEMS AS AI ON PD.ITEM_ID = AI.ITEM_ID  " +
                " left join  " + dbLibrary + ".BIDDING  AS BI ON BI.ITEM_ID = PD.ITEM_ID  " +
                " and BI.PR_ID = PD.PR_ID  " +
                " left join " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ ON SQ.ITEM_ID = BI.ITEM_ID  " +
                " AND SQ.PR_ID = BI.PR_ID  " +
                " where PD.SUBMIT_FOR_BID = 1 " +
                " group by  PD.ITEM_ID, PD.PR_ID,AI.ITEM_NAME ,BI.START_DATE,  " +
                " BI.END_DATE,PM.PR_CODE order by  PD.PR_ID desc";

            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public SupplierQuotation GetGivenQuotatios(int PrId, int ItemId, int SupplierId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = " SELECT * FROM  " + dbLibrary + ".SUPPLIER_QUOTATION " +
                                           " WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + "  AND SUPPLIER_ID = " + SupplierId + "";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SupplierQuotation>(dBConnection.dr);
            }
        }

        public int UpdateResetPoReject(int prId, int itemId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".SUPPLIER_QUOTATION SET IS_SELECTED = 0, IS_REJECTED = 0, REASON = '" + "" + "', CUSTOMIZE_AMOUNT =0, IS_APPROVED= 0 ,SUPPLIER_SELECTED_COUNT= 0 ,SUPPLIER_REJECTED_COUNT= 0  , IS_PO_RAISED = 0,  IS_PO_REJECTED = 0,  IS_PO_APPROVED = 0   WHERE PR_ID =  " + prId + " AND  ITEM_ID = " + itemId + " ";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierQuotation> GetManualPendingBidsAndNotBid(DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText =
                " SELECT PD.ITEM_ID, PD.PR_ID,AI.ITEM_NAME, " +
                " COALESCE(sum(SQ.IS_SELECTED),0) as pending,  " +
                " COALESCE((COUNT(SQ.IS_SELECTED) - sum(SQ.IS_SELECTED)),0) as submitted,  " +
                " COUNT(SQ.IS_SELECTED) as total,  " +
                " PM.PR_CODE, BI.START_DATE,BI.END_DATE  " +
                " from " + dbLibrary + ".PR_MASTER AS PM" +
                " inner join " + dbLibrary + ".PR_DETAIL  AS PD ON PM.PR_ID = PD.PR_ID  " +
                " inner join " + dbLibrary + ".ADD_ITEMS AS AI ON PD.ITEM_ID = AI.ITEM_ID  " +
                " left join " + dbLibrary + ".BIDDING AS BI ON BI.ITEM_ID = PD.ITEM_ID  " +
                " left join " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ ON SQ.ITEM_ID = BI.ITEM_ID  " +
                " AND SQ.PR_ID = BI.PR_ID  " +
                " where PD.SUBMIT_FOR_BID = 1 AND PD.BID_TYPE_MANUAL_BID=0 AND BI.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL=4 " +
                " group by  PD.ITEM_ID, PD.PR_ID,AI.ITEM_NAME ,BI.START_DATE,  " +
                " BI.END_DATE,PM.PR_CODE ";

            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public List<SupplierQuotation> GetCompletedManualBid(DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT SQ.ITEM_ID, SQ.PR_ID, AI.ITEM_NAME,BI.START_DATE,BI.END_DATE," +
                                           " sum(SQ.IS_SELECTED) as pending," +
                                           " COUNT(SQ.IS_SELECTED) as total, " +
                                           " ( COUNT(SQ.IS_SELECTED)- sum(SQ.IS_SELECTED)) as submitted, PM.PR_CODE " +
                                           " from  " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ " +
                                           " INNER JOIN " + dbLibrary + ".BIDDING AS BI ON (BI.PR_ID = SQ.PR_ID)" +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON (SQ.ITEM_ID = AI.ITEM_ID) " +
                                           " AND (BI.ITEM_ID = SQ.ITEM_ID) " +
                                           " inner  join  " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = BI.PR_ID " +
                                           " WHERE (BI.END_DATE < '" +  LocalTime.Now + "')  and BI.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL =4 " +
                                           " group by  SQ.ITEM_ID, SQ.PR_ID,AI.ITEM_NAME,BI.START_DATE,BI.END_DATE,PM.PR_CODE";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public SupplierQuotation GetSupplierEditBidDetails(int PrId, int ItemId, int SupplierId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".SUPPLIER_QUOTATION " +
                                           " WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + " AND IS_SELECTED = 0 AND SUPPLIER_ID= " + SupplierId + " AND " +
                                           " TOTAL_AMOUNT > 0 ";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SupplierQuotation>(dBConnection.dr);
            }
        }

        public List<SupplierQuotation> GetBidSupplierForItem(int PRid, int itemId, DBConnection dBConnection)
        {

            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = " SELECT SQ.PR_ID,SQ.ITEM_ID,SU.SUPPLIER_ID,SQ.BID_OPENING_ID, SQ.IS_VAT_INCLUDED, " +
                                           " SU.SUPPLIER_NAME,SQ.PER_ITEM_PRICE,PD.ITEM_QUANTITY, " +
                                           " (SQ.PER_ITEM_PRICE * PD.ITEM_QUANTITY) AS AMOUNT, " +
                                           " SQ.NBT_AMOUNT AS NBT, " +
                                           " SQ.VAT_AMOUNT AS VAT, " +
                                           " TOTAL_AMOUNT AS TOTALPRICE " +
                                           " FROM " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ INNER JOIN " + dbLibrary + ".BIDDING AS BI ON SQ.PR_ID = BI.PR_ID AND SQ.TOTAL_AMOUNT > 0 " +
                                           " AND BI.END_DATE < '" +  LocalTime.Now + "' INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PD.PR_ID = BI.PR_ID " +
                                           " AND PD.ITEM_ID = BI.ITEM_ID AND BI.ITEM_ID = SQ.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID = SQ.SUPPLIER_ID where PD.PR_ID = " + PRid + " AND SQ.ITEM_ID=" + itemId + " AND SQ.IS_REJECTED= '0' ORDER BY (TOTALPRICE) DESC";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public int UpdateNegotiateAmount(int PrId, int ItemId, int SupplierId, decimal CustomizeAmount, int selectedCount, int rejectedCount, decimal VatAmount, decimal NbtAmount, decimal TotalAmount, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "update " + dbLibrary + ".SUPPLIER_QUOTATION SET IS_REJECTED = 0, REASON = '', IS_APPROVED = 1,CUSTOMIZE_AMOUNT=" + CustomizeAmount + ",SUPPLIER_SELECTED_COUNT=" + selectedCount + ",SUPPLIER_REJECTED_COUNT=" + rejectedCount + ", VAT_AMOUNT = " + VatAmount + " , NBT_AMOUNT = " + NbtAmount + " , TOTAL_AMOUNT = " + TotalAmount + "  WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + " AND SUPPLIER_ID = " + SupplierId + " ";
            return dBConnection.cmd.ExecuteNonQuery();
        }





        //New Methods By Salman created on 2019-01-17
        public List<SupplierQuotation> GetSupplierQuotations(int BidId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION AS SQ\n" +
                "INNER JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS S ON SQ.SUPPLIER_ID= S.SUPPLIER_ID\n" +
                "WHERE SQ.BID_ID = " + BidId;
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        //for Covering PR
        public SupplierQuotation GetSupplierQuotationsForCoveringPR(int BidId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION AS SQ\n" +
                "WHERE SQ.BID_ID = " + BidId;
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SupplierQuotation>(dBConnection.dr);
            }
        }

        public List<SupplierQuotation> GetSupplierQuotationsImports(int BidId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION AS SQ\n" +
                "INNER JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS S ON SQ.SUPPLIER_ID= S.SUPPLIER_ID\n" +
                "LEFT JOIN IMPORT_QUOTATION AS IQ ON IQ.QUOTATION_ID = SQ.QUOTATION_ID " +
                "WHERE SQ.BID_ID = " + BidId;
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public List<SupplierQuotation> ConfirmRates(int BidId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT SQ.BID_ID, CT.CURRENCY_NAME, CT.CURRENCY_TYPE_ID, CR.SELLING_RATE FROM SUPPLIER_QUOTATION AS SQ\n" +
                "LEFT JOIN (SELECT QUOTATION_ID, CURRENCY_TYPE_ID FROM IMPORT_QUOTATION) AS IQ ON IQ.QUOTATION_ID = SQ.QUOTATION_ID\n" +
                "LEFT JOIN (SELECT CURRENCY_TYPE_ID, CURRENCY_NAME FROM DEF_CURRENCY_TYPE ) AS CT ON CT.CURRENCY_TYPE_ID = IQ.CURRENCY_TYPE_ID \n" +
                "LEFT JOIN (SELECT CURRENCY_TYPE_ID, SELLING_RATE FROM CURRENCY_RATE) AS CR ON CR.CURRENCY_TYPE_ID = CT.CURRENCY_TYPE_ID\n" +
                "where BID_ID = "+ BidId + " GROUP BY SQ.BID_ID, CT.CURRENCY_NAME, CT.CURRENCY_TYPE_ID, CR.SELLING_RATE\n";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }
        public List<SupplierQuotation> GetImportDetailsListForTabulationReview(int BidId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION AS SQ \n" +
                "INNER JOIN SUPPLIER_QUOTATION_ITEM AS SQI ON SQI.QUOTATION_ID = SQ.QUOTATION_ID\n" +
                "INNER JOIN IMPORT_QUOTATION AS IQ ON IQ.QUOTATION_ID = SQ.QUOTATION_ID\n" +
                "INNER JOIN IMPORT_QUOTATION_ITEM AS IQI ON IQI.QUOTATION_ITEM_ID = SQI.QUOTATION_ITEM_ID\n" +
                "INNER JOIN (SELECT SUPPLIER_NAME, SUPPLIER_ID FROM SUPPLIER) AS SUP ON SUP.SUPPLIER_ID = SQ.SUPPLIER_ID\n" +
                "INNER JOIN (SELECT SUPPLIER_NAME AS SUPPLIER_AGENT, SUPPLIER_ID FROM SUPPLIER) AS SA ON SA.SUPPLIER_ID = IQ.AGENT_ID\n" +
                "INNER JOIN COUNTRY AS CO ON CO.ID = IQ.COUNTRY\n" +
                "INNER JOIN CURRENCY AS CU ON CU.ID = IQ.CURRENCY_TYPE_ID\n" +
                "INNER JOIN IMPORTS_HISTORY AS IH ON IH.HISTORY_ID = IQI.HISTORY\n" +
                "INNER JOIN DEF_PRICE_TERMS AS TER ON TER.TERM_ID = IQI.TERM\n" +
                "WHERE SQ.BID_ID = " + BidId + "\n";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotation>(dBConnection.dr);
            }
        }

        public int SaveSupplierQuotation(SupplierQuotation quotation, ImportQuotation importQuotation , int PurchaseType,DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "Select COUNT(*) FROM " + dbLibrary + ".SUPPLIER_QUOTATION  " +
                                          " WHERE  BID_ID ="+ quotation.BidId + " AND QUOTATION_REFERENCE_CODE = '" + quotation.QuotationReferenceCode + "'  ";
            var count = decimal.Parse(dBConnection.cmd.ExecuteScalar().ToString());
            if (count == 0)
            {
                StringBuilder sql = new StringBuilder();

                //Saving Quotation ID to use in Quotation Item insertion
                sql.AppendLine("DECLARE @QUOTATION_TABLE TABLE (ID INT); ");

                //Inserting Supplier Quotation
                sql.AppendLine("INSERT INTO SUPPLIER_QUOTATION ([BID_ID],[SUPPLIER_ID],[SUB_TOTAL],[VAT_AMOUNT],[NBT_AMOUNT],[NET_TOTAL],[SUPPLIER_TERMS_CONDITIONS],[IS_STAYED_AS_LATER_BID],[QUOTATION_REFERENCE_CODE])");
                sql.AppendLine("OUTPUT INSERTED.[QUOTATION_ID] INTO @QUOTATION_TABLE(ID)");
                sql.AppendLine("VALUES(" + quotation.BidId + "," + quotation.SupplierId + "," + quotation.SubTotal + "," + quotation.VatAmount + "," + quotation.NbtAmount + "," + quotation.NetTotal + ",'" + quotation.TermsAndCondition.ProcessString() + "'," + quotation.IsStayedAsLaterBid + " ,'" + quotation.QuotationReferenceCode + "');");
                

                //end if the quotation is stayed as later else continue
                if (quotation.IsStayedAsLaterBid == 0)
                {

                    //Inserting Quotation images for Quotation
                    for (int i = 0; i < quotation.QuotationImages.Count; i++)
                    {
                        sql.AppendLine("INSERT INTO [QUOTATION_IMAGES] ([QUOTATION_ID],[IMAGE_PATH]) VALUES ((SELECT MAX(ID) FROM @QUOTATION_TABLE),'" + quotation.QuotationImages[i].ImagePath.ProcessString() + "');");
                    }

                    //Inserting Files uploaded for Quotation
                    for (int i = 0; i < quotation.UploadedFiles.Count; i++)
                    {
                        sql.AppendLine("INSERT INTO [SUPPLIER_BIDDING_FILE_UPLOAD] ([QUOTATION_ID],[FILE_NAME],[FILE_PATH]) VALUES ((SELECT MAX(ID) FROM @QUOTATION_TABLE),'" + quotation.UploadedFiles[i].FileName.ProcessString() + "','" + quotation.UploadedFiles[i].FilePath.ProcessString() + "');");
                    }

                    //saving quotation item Id to use in quotation image, bom and file insertions
                    sql.AppendLine("DECLARE @QUOTATION_ITEM_TABLE TABLE (ID INT);");

                    for (int i = 0; i < quotation.QuotationItems.Count; i++)
                    {
                        //Inserting Supplier quotation id

                        //sql.AppendLine("INSERT INTO SUPPLIER_QUOTATION_ITEM ([QUOTATION_ID],[BIDDING_ITEM_ID],[ITEM_ID],[QTY],[ESTIMATED_PRICE],[UNIT_PRICE],[SUB_TOTAL],[HAS_VAT],[VAT_AMOUNT],[HAS_NBT],[NBT_CALCULATION_TYPE],[NBT_AMOUNT],[NET_TOTAL],[DESCRIPTION],[ITEM_REFERENCE_CODE],[CURRENCY_ID],[EXCHANGE_RATE] ,[AGENT_ID] ,[COUNTRY],[BRAND],[DUTYPAL],[CLEARING_COST],[TERMS],[CIF],[OTHER_COST],[HISTORY],[VALIDITY],[REFNO],[ESTDELIVERY],[REMARK], [SUPPLIER_MENTIONED_ITEM_NAME])");
                        //sql.AppendLine("OUTPUT INSERTED.[QUOTATION_ITEM_ID] INTO @QUOTATION_ITEM_TABLE(ID)");
                        //sql.AppendLine("VALUES ((SELECT MAX(ID) FROM @QUOTATION_TABLE)," + quotation.QuotationItems[i].BiddingItemId + "," + quotation.QuotationItems[i].ItemId + "," + quotation.QuotationItems[i].Qty + "," + quotation.QuotationItems[i].EstimatedPrice + "," + quotation.QuotationItems[i].UnitPrice + "," + quotation.QuotationItems[i].SubTotal + "," + quotation.QuotationItems[i].HasVat + "," + quotation.QuotationItems[i].VatAmount + "," + quotation.QuotationItems[i].HasNbt + "," + quotation.QuotationItems[i].NbtCalculationType + "," + quotation.QuotationItems[i].NbtAmount + "," + quotation.QuotationItems[i].TotalAmount + " ,'" + quotation.QuotationItems[i].Description + "' ,'" + quotation.QuotationItems[i].ItemReferenceCode + "' ," + quotation.QuotationItems[i].CurrencyId + ", " + quotation.QuotationItems[i].ExchangeRate + " ," + quotation.QuotationItems[i].AgentId + ",'" + quotation.QuotationItems[i].Country + "','" + quotation.QuotationItems[i].Brand + "'," + quotation.QuotationItems[i].Dutypal + "," + quotation.QuotationItems[i].Clearingcost + ",'" + quotation.QuotationItems[i].Terms + "'," + quotation.QuotationItems[i].CIF + "," + quotation.QuotationItems[i].Other + ",'" + quotation.QuotationItems[i].History + "','" + quotation.QuotationItems[i].Validity + "','" + quotation.QuotationItems[i].Refno + "','" + quotation.QuotationItems[i].Estdelivery + "','" + quotation.QuotationItems[i].Remark + "','" + quotation.QuotationItems[i].SupplierMentionedItemName + "');");

                        sql.AppendLine("INSERT INTO SUPPLIER_QUOTATION_ITEM ([QUOTATION_ID],[BIDDING_ITEM_ID],[ITEM_ID],[QTY],[ESTIMATED_PRICE],[UNIT_PRICE],[SUB_TOTAL],[HAS_VAT],[VAT_AMOUNT],[HAS_NBT],[NBT_CALCULATION_TYPE],[NBT_AMOUNT],[NET_TOTAL],[DESCRIPTION],[ITEM_REFERENCE_CODE],[REFNO], [SUPPLIER_MENTIONED_ITEM_NAME], [COUNTRY])");
                        sql.AppendLine("OUTPUT INSERTED.[QUOTATION_ITEM_ID] INTO @QUOTATION_ITEM_TABLE(ID)");
                        sql.AppendLine("VALUES ((SELECT MAX(ID) FROM @QUOTATION_TABLE)," + quotation.QuotationItems[i].BiddingItemId + "," + quotation.QuotationItems[i].ItemId + "," + quotation.QuotationItems[i].Qty + "," + quotation.QuotationItems[i].EstimatedPrice + "," + quotation.QuotationItems[i].UnitPrice + "," + quotation.QuotationItems[i].SubTotal + "," + quotation.QuotationItems[i].HasVat + "," + quotation.QuotationItems[i].VatAmount + "," + quotation.QuotationItems[i].HasNbt + "," + quotation.QuotationItems[i].NbtCalculationType + "," + quotation.QuotationItems[i].NbtAmount + "," + quotation.QuotationItems[i].TotalAmount + " ,'" + quotation.QuotationItems[i].Description + "' ,'" + quotation.QuotationItems[i].ItemReferenceCode + "' ,'" + quotation.QuotationItems[i].Refno + "','" + quotation.QuotationItems[i].SupplierMentionedItemName + "','" + quotation.QuotationItems[i].Country + "');");

                        //Inserting BOM for Quotation items
                        for (int j = 0; j < quotation.QuotationItems[i].SupplierBOMs.Count; j++)
                        {
                            sql.AppendLine("INSERT INTO [SUPPLIER_BOM] ([QUOTATION_ITEM_ID],[MATERIAL],[DESCRIPTION],[COMPLY],[REMARKS]) VALUES ((SELECT MAX(ID) FROM @QUOTATION_ITEM_TABLE),'" + quotation.QuotationItems[i].SupplierBOMs[j].Material.ProcessString() + "','" + quotation.QuotationItems[i].SupplierBOMs[j].Description.ProcessString() + "'," + quotation.QuotationItems[i].SupplierBOMs[j].Comply + ",'" + quotation.QuotationItems[i].SupplierBOMs[j].Remarks.ProcessString() + "');");
                        }
                       
                    }

                    //Inserting Quotations for Imports
                    if (PurchaseType == 2) {
                        sql.AppendLine("INSERT INTO IMPORT_QUOTATION([QUOTATION_ID], [AGENT_ID], [CURRENCY_TYPE_ID], [PAYMENT_MODE_ID], [TERM_ID], [TRANSPORT_MODE_ID], [CONTAINER_SIZE_ID],[SUPPLIER_AGENT],[COUNTRY], [NO_OF_DAYS_PAYEMENT_MODE])");
                        sql.AppendLine("VALUES((SELECT MAX(ID) FROM @QUOTATION_TABLE)," + importQuotation.AgentId + "," + importQuotation.CurrencyTypeId + "," + importQuotation.PaymentModeId + "," + importQuotation.TermId + "," + importQuotation.TransportModeId + "," + importQuotation.ContainerSizeId + " ," + importQuotation.SupplierAgentId + "," + importQuotation.Country + "," + importQuotation.PaymentModeDays + ");");

                        for (int i = 0; i < importQuotation.ImportQuotationItemList.Count; i++) {
                            sql.AppendLine("INSERT INTO IMPORT_QUOTATION_ITEM([QUOTATION_ID], [QUOTATION_ITEM_ID], [BRAND], [EXCHANGE_RATE], [CIF], [DUTY_PAL], [CLEARING_COST], [OTHER], [HISTORY], [VALIDITY], [EST_DELIVERY], [TOTAL], [REMARK], [TERM], [HS_ID], [MILL], [EXCANGE_RATE_VALUE],[XID_RATE],[CID_RATE],[PAL_RATE],[EIC_RATE],[AIR_FREIGHT],[INSURENCE],[XID_VALUE],[CID_VALUE],[PAL_VALUE],[EIC_VALUE],[VAT_RATE],[VAT_VALUE])");
                            sql.AppendLine("VALUES((SELECT MAX(ID) FROM @QUOTATION_TABLE),(SELECT QUOTATION_ITEM_ID FROM SUPPLIER_QUOTATION_ITEM WHERE QUOTATION_ID = (SELECT MAX(ID) FROM @QUOTATION_TABLE) AND ITEM_ID = " + importQuotation.ImportQuotationItemList[i].ItemId + " ),'" + importQuotation.ImportQuotationItemList[i].Brand + "'," + importQuotation.ImportQuotationItemList[i].ExchangeRate + "," + importQuotation.ImportQuotationItemList[i].CIF + "," + importQuotation.ImportQuotationItemList[i].DutyPal + "," + importQuotation.ImportQuotationItemList[i].ClearingCost + "," + importQuotation.ImportQuotationItemList[i].Other + "," + importQuotation.ImportQuotationItemList[i].History + ",'" + importQuotation.ImportQuotationItemList[i].Validity + "','" + importQuotation.ImportQuotationItemList[i].EstDelivery + "'," + importQuotation.ImportQuotationItemList[i].Total + ",'" + importQuotation.ImportQuotationItemList[i].Remark + "','" + importQuotation.ImportQuotationItemList[i].Term + "', '" + importQuotation.ImportQuotationItemList[i].HsId + "',  '" + importQuotation.ImportQuotationItemList[i].Mill + "', " + importQuotation.ImportQuotationItemList[i].ExchangeRateValue + ", " + importQuotation.ImportQuotationItemList[i].XIDRate + ", " + importQuotation.ImportQuotationItemList[i].CIDRate + ", " + importQuotation.ImportQuotationItemList[i].PALRate + ", " + importQuotation.ImportQuotationItemList[i].EICRate + ", " + importQuotation.ImportQuotationItemList[i].AirFreight + ", " + importQuotation.ImportQuotationItemList[i].Insurance + ", " + importQuotation.ImportQuotationItemList[i].XIDValue + ", " + importQuotation.ImportQuotationItemList[i].CIDValue + ", " + importQuotation.ImportQuotationItemList[i].PALValue + ", " + importQuotation.ImportQuotationItemList[i].EICValue + ", " + importQuotation.ImportQuotationItemList[i].VATRate + ", " + importQuotation.ImportQuotationItemList[i].VATValue + ");");
                            //sql.AppendLine("VALUES((SELECT MAX(ID) FROM @QUOTATION_TABLE),(SELECT MAX(ID) FROM @QUOTATION_ITEM_TABLE),'" + importQuotation.ImportQuotationItemList[i].Brand + "'," + importQuotation.ImportQuotationItemList[i].ExchangeRate + "," + importQuotation.ImportQuotationItemList[i].CIF + "," + importQuotation.ImportQuotationItemList[i].DutyPal + "," + importQuotation.ImportQuotationItemList[i].ClearingCost + "," + importQuotation.ImportQuotationItemList[i].Other + "," + importQuotation.ImportQuotationItemList[i].History + ",'" + importQuotation.ImportQuotationItemList[i].Validity + "','" + importQuotation.ImportQuotationItemList[i].EstDelivery + "'," + importQuotation.ImportQuotationItemList[i].Total + ",'" + importQuotation.ImportQuotationItemList[i].Remark + "','" + importQuotation.ImportQuotationItemList[i].Term + "', '" + importQuotation.ImportQuotationItemList[i].HsId + "',  '" + importQuotation.ImportQuotationItemList[i].Mill + "', " + importQuotation.ImportQuotationItemList[i].ExchangeRateValue + ", " + importQuotation.ImportQuotationItemList[i].XIDRate + ", " + importQuotation.ImportQuotationItemList[i].CIDRate + ", " + importQuotation.ImportQuotationItemList[i].PALRate + ", " + importQuotation.ImportQuotationItemList[i].EICRate + ", " + importQuotation.ImportQuotationItemList[i].AirFreight + ", " + importQuotation.ImportQuotationItemList[i].Insurance + ", " + importQuotation.ImportQuotationItemList[i].XIDValue + ", " + importQuotation.ImportQuotationItemList[i].CIDValue + ", " + importQuotation.ImportQuotationItemList[i].PALValue + ", " + importQuotation.ImportQuotationItemList[i].EICValue + ", " + importQuotation.ImportQuotationItemList[i].VATRate + ", " + importQuotation.ImportQuotationItemList[i].VATValue + ");");

                        }

                    }

                }
                dBConnection.cmd.Parameters.Clear();
                dBConnection.cmd.CommandText = sql.ToString();
                return dBConnection.cmd.ExecuteNonQuery();
            }else
            {
                return -1;
            }
        }

        /// <summary>
        /// Used to update supplier quotation after fully submitting. means not stayed as later bid
        /// </summary>
        /// <param name="quotation"></param>
        /// <param name="dBConnection"></param>
        /// <returns></returns>
        public int UpdateSupplierQuotation(SupplierQuotation quotation, DBConnection dBConnection)
        {
            StringBuilder sql = new StringBuilder();

            //updating supplier quotation details
            sql.AppendLine("UPDATE SUPPLIER_QUOTATION SET [SUB_TOTAL]=" + quotation.SubTotal + ",[VAT_AMOUNT]=" + quotation.VatAmount + ",[NBT_AMOUNT]=" + quotation.NbtAmount + ",[NET_TOTAL]=" + quotation.NetTotal + ",[SUPPLIER_TERMS_CONDITIONS]='" + quotation.TermsAndCondition.ProcessString() + "',[IS_STAYED_AS_LATER_BID]=" + quotation.IsStayedAsLaterBid + " ,[QUOTATION_REFERENCE_CODE] = '"+quotation.QuotationReferenceCode+"'  WHERE [QUOTATION_ID]=" + quotation.QuotationId + ";");


            //Inserting quotation images if they were newly added or deleting images if they were deleted by user
            for (int i = 0; i < quotation.QuotationImages.Count; i++)
            {
                if (quotation.QuotationImages[i].RecordStatus == 1)
                    sql.AppendLine("INSERT INTO [QUOTATION_IMAGES] ([QUOTATION_ID],[IMAGE_PATH]) VALUES (" + quotation.QuotationId + ",'" + quotation.QuotationImages[i].ImagePath.ProcessString() + "');");
                else if (quotation.QuotationImages[i].RecordStatus == 2)
                    sql.AppendLine("DELETE FROM [QUOTATION_IMAGES] WHERE [QUOTATION_IMAGE_ID]=" + quotation.QuotationImages[i].QuotationImageId + ";");
            }

            //Inserting quotation files if they were newly added or deleting files if they were deleted by user
            for (int i = 0; i < quotation.UploadedFiles.Count; i++)
            {
                if (quotation.UploadedFiles[i].RecordStatus == 1)
                    sql.AppendLine("INSERT INTO [SUPPLIER_BIDDING_FILE_UPLOAD] ([QUOTATION_ID],[FILE_NAME],[FILE_PATH]) VALUES (" + quotation.QuotationId + ",'" + quotation.UploadedFiles[i].FileName.ProcessString() + "','" + quotation.UploadedFiles[i].FilePath.ProcessString() + "');");
                else if (quotation.UploadedFiles[i].RecordStatus == 2)
                    sql.AppendLine("DELETE FROM [SUPPLIER_BIDDING_FILE_UPLOAD] WHERE [QUOTATION_FILE_ID]=" + quotation.UploadedFiles[i].QuotationFileId + ";");
            }

            
            for (int i = 0; i < quotation.QuotationItems.Count; i++)
            {
                DateTime value;
                if(DateTime.TryParse(quotation.QuotationItems[i].Validity.ToString(),out value))
                {
                    quotation.QuotationItems[i].Validity = LocalTime.Now;
                }
                //updating supplier quotation item details
                sql.AppendLine("UPDATE SUPPLIER_QUOTATION_ITEM SET [QUOTATION_ID]=" + quotation.QuotationItems[i].QuotationId + ",[QTY]=" + quotation.QuotationItems[i].Qty + ",[DESCRIPTION] = '"+quotation.QuotationItems[i].Description+"' , [UNIT_PRICE]=" + quotation.QuotationItems[i].UnitPrice + ",[SUB_TOTAL]=" + quotation.QuotationItems[i].SubTotal + ",[HAS_VAT]=" + quotation.QuotationItems[i].HasVat + ",[VAT_AMOUNT]=" + quotation.QuotationItems[i].VatAmount + ",[HAS_NBT]=" + quotation.QuotationItems[i].HasNbt + ",[NBT_CALCULATION_TYPE]=" + quotation.QuotationItems[i].NbtCalculationType + ",[NBT_AMOUNT]=" + quotation.QuotationItems[i].NbtAmount + ",[NET_TOTAL]=" + quotation.QuotationItems[i].TotalAmount + " ,[ITEM_REFERENCE_CODE] = '"+quotation.QuotationItems[i].ItemReferenceCode+ "',[BRAND]='" + quotation.QuotationItems[i].Brand + "',[DUTYPAL]=" + quotation.QuotationItems[i].Dutypal + ",[CLEARING_COST]=" + quotation.QuotationItems[i].Clearingcost + ",[TERMS]='" + quotation.QuotationItems[i].Terms + "',[CIF]=" + quotation.QuotationItems[i].CIF + " ,[OTHER_COST]=" + quotation.QuotationItems[i].Other + ",[HISTORY]='" + quotation.QuotationItems[i].History + "',[VALIDITY]='" +quotation.QuotationItems[i].Validity+ "',[REFNO]='" + quotation.QuotationItems[i].Refno + "' , [IS_QUOTATION_ITEM_APPROVED] = 0 ,[IS_QUOTATION_ITEM_APPROVAL_REMARK] ='' WHERE [QUOTATION_ITEM_ID]=" + quotation.QuotationItems[i].QuotationItemId + ";");

                sql.AppendLine("DELETE FROM [SUPPLIER_BOM] WHERE [QUOTATION_ITEM_ID]=" + quotation.QuotationItems[i].QuotationItemId + ";");


                //updating supplier quotation item BOM
                for (int j = 0; j < quotation.QuotationItems[i].SupplierBOMs.Count; j++)
                {
                    sql.AppendLine("INSERT INTO [SUPPLIER_BOM] ([QUOTATION_ITEM_ID],[MATERIAL],[DESCRIPTION],[COMPLY],[REMARKS]) VALUES (" + quotation.QuotationItems[i].QuotationItemId + ",'" + quotation.QuotationItems[i].SupplierBOMs[j].Material.ProcessString() + "','" + quotation.QuotationItems[i].SupplierBOMs[j].Description.ProcessString() + "'," + quotation.QuotationItems[i].SupplierBOMs[j].Comply + ",'" + quotation.QuotationItems[i].SupplierBOMs[j].Remarks.ProcessString() + "');");
                }
            }
            
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = sql.ToString();
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateSupplierQuotationImports(SupplierQuotation quotation, DBConnection dBConnection) {
            StringBuilder sql = new StringBuilder();

            //updating supplier quotation details
            sql.AppendLine("UPDATE SUPPLIER_QUOTATION SET [SUB_TOTAL]=" + quotation.SubTotal + ",[VAT_AMOUNT]=" + quotation.VatAmount + ",[NBT_AMOUNT]=" + quotation.NbtAmount + ",[NET_TOTAL]=" + quotation.NetTotal + ",[SUPPLIER_TERMS_CONDITIONS]='" + quotation.TermsAndCondition.ProcessString() + "',[QUOTATION_REFERENCE_CODE]='" + quotation.QuotationReferenceCode.ProcessString() + "',[IS_STAYED_AS_LATER_BID]=" + quotation.IsStayedAsLaterBid + "   WHERE [QUOTATION_ID]=" + quotation.QuotationId + ";");
            sql.AppendLine("UPDATE IMPORT_QUOTATION SET [TRANSPORT_MODE_ID]=" + quotation.TransportModeId + ",[CONTAINER_SIZE_ID]=" + quotation.ContainersizeId + ",[SUPPLIER_AGENT]=" + quotation.SupplierAgentId + ",[PAYMENT_MODE_ID]=" + quotation.PaymentModeId + ",[NO_OF_DAYS_PAYEMENT_MODE]=" + quotation.PaymentModeDays + " WHERE [QUOTATION_ID]=" + quotation.QuotationId + ";");


            //Inserting quotation images if they were newly added or deleting images if they were deleted by user
            for (int i = 0; i < quotation.QuotationImages.Count; i++) {
                if (quotation.QuotationImages[i].RecordStatus == 1)
                    sql.AppendLine("INSERT INTO [QUOTATION_IMAGES] ([QUOTATION_ID],[IMAGE_PATH]) VALUES (" + quotation.QuotationId + ",'" + quotation.QuotationImages[i].ImagePath.ProcessString() + "');");
                else if (quotation.QuotationImages[i].RecordStatus == 2)
                    sql.AppendLine("DELETE FROM [QUOTATION_IMAGES] WHERE [QUOTATION_IMAGE_ID]=" + quotation.QuotationImages[i].QuotationImageId + ";");
            }

            //Inserting quotation files if they were newly added or deleting files if they were deleted by user
            for (int i = 0; i < quotation.UploadedFiles.Count; i++) {
                if (quotation.UploadedFiles[i].RecordStatus == 1)
                    sql.AppendLine("INSERT INTO [SUPPLIER_BIDDING_FILE_UPLOAD] ([QUOTATION_ID],[FILE_NAME],[FILE_PATH]) VALUES (" + quotation.QuotationId + ",'" + quotation.UploadedFiles[i].FileName.ProcessString() + "','" + quotation.UploadedFiles[i].FilePath.ProcessString() + "');");
                else if (quotation.UploadedFiles[i].RecordStatus == 2)
                    sql.AppendLine("DELETE FROM [SUPPLIER_BIDDING_FILE_UPLOAD] WHERE [QUOTATION_FILE_ID]=" + quotation.UploadedFiles[i].QuotationFileId + ";");
            }


            for (int i = 0; i < quotation.QuotationItems.Count; i++) {
                DateTime value;
                if (DateTime.TryParse(quotation.QuotationItems[i].Validity.ToString(), out value)) {
                    quotation.QuotationItems[i].Validity = LocalTime.Now;
                }
                //updating supplier quotation item details
                sql.AppendLine("UPDATE SUPPLIER_QUOTATION_ITEM SET [QUOTATION_ID]=" + quotation.QuotationItems[i].QuotationId + ",[QTY]=" + quotation.QuotationItems[i].Qty + ",[DESCRIPTION] = '" + quotation.QuotationItems[i].Description + "' , [UNIT_PRICE]=" + quotation.QuotationItems[i].UnitPrice + ",[SUB_TOTAL]=" + quotation.QuotationItems[i].SubTotal + ",[HAS_VAT]=" + quotation.QuotationItems[i].HasVat + ",[VAT_AMOUNT]=" + quotation.QuotationItems[i].VatAmount + ",[HAS_NBT]=" + quotation.QuotationItems[i].HasNbt + ",[NBT_CALCULATION_TYPE]=" + quotation.QuotationItems[i].NbtCalculationType + ",[NBT_AMOUNT]=" + quotation.QuotationItems[i].NbtAmount + ",[NET_TOTAL]=" + quotation.QuotationItems[i].TotalAmount + " ,[ITEM_REFERENCE_CODE] = '" + quotation.QuotationItems[i].ItemReferenceCode + "',[BRAND]='" + quotation.QuotationItems[i].Brand + "',[DUTYPAL]=" + quotation.QuotationItems[i].Dutypal + ",[CLEARING_COST]=" + quotation.QuotationItems[i].Clearingcost + ",[TERMS]='" + quotation.QuotationItems[i].Terms + "',[CIF]=" + quotation.QuotationItems[i].CIF + " ,[OTHER_COST]=" + quotation.QuotationItems[i].Other + ",[HISTORY]='" + quotation.QuotationItems[i].History + "',[VALIDITY]='" + quotation.QuotationItems[i].Validity + "',[REFNO]='" + quotation.QuotationItems[i].Refno + "' , [IS_QUOTATION_ITEM_APPROVED] = 0 ,[IS_QUOTATION_ITEM_APPROVAL_REMARK] ='', [SUPPLIER_MENTIONED_ITEM_NAME] = '" + quotation.QuotationItems[i].SupplierMentionedItemName + "' WHERE [QUOTATION_ITEM_ID]=" + quotation.QuotationItems[i].QuotationItemId + ";");
                sql.AppendLine("UPDATE IMPORT_QUOTATION_ITEM SET [BRAND]='" + quotation.QuotationItems[i].Brand + "',[EXCHANGE_RATE]=" + quotation.QuotationItems[i].ExchangeRate + ",[CIF] = '" + quotation.QuotationItems[i].CIF + "' , [DUTY_PAL]=" + quotation.QuotationItems[i].Dutypal + ",[CLEARING_COST]=" + quotation.QuotationItems[i].Clearingcost + ",[OTHER]=" + quotation.QuotationItems[i].Other + ",[HISTORY]=" + quotation.QuotationItems[i].History + ",[VALIDITY]='" + quotation.QuotationItems[i].Validity + "',[EST_DELIVERY]='" + quotation.QuotationItems[i].Estdelivery + "',[REMARK]='" + quotation.QuotationItems[i].Remark + "',[TERM]='" + quotation.QuotationItems[i].Term + "' ,[HS_ID] = '" + quotation.QuotationItems[i].HsId + "',[MILL]='" + quotation.QuotationItems[i].Mill + "',[XID_RATE]=" + quotation.QuotationItems[i].xid + ",[CID_RATE]=" + quotation.QuotationItems[i].cid + ",[PAL_RATE]=" + quotation.QuotationItems[i].pal + ",[EIC_RATE]=" + quotation.QuotationItems[i].eic + " ,[AIR_FREIGHT]=" + quotation.QuotationItems[i].AirFreight + ",[INSURENCE]=" + quotation.QuotationItems[i].Insurance + ",[XID_VALUE]=" + quotation.QuotationItems[i].XIDValue + ",[CID_VALUE]=" + quotation.QuotationItems[i].CIDValue + " , [PAL_VALUE] = " + quotation.QuotationItems[i].PALValue + " ,[EIC_VALUE] =" + quotation.QuotationItems[i].EICValue + " , [VAT_RATE] = " + quotation.QuotationItems[i].VatRate + ", [VAT_VALUE] = " + quotation.QuotationItems[i].VatAmount + " WHERE [QUOTATION_ITEM_ID]=" + quotation.QuotationItems[i].QuotationItemId + ";");

                sql.AppendLine("DELETE FROM [SUPPLIER_BOM] WHERE [QUOTATION_ITEM_ID]=" + quotation.QuotationItems[i].QuotationItemId + ";");


                //updating supplier quotation item BOM
                for (int j = 0; j < quotation.QuotationItems[i].SupplierBOMs.Count; j++) {
                    sql.AppendLine("INSERT INTO [SUPPLIER_BOM] ([QUOTATION_ITEM_ID],[MATERIAL],[DESCRIPTION],[COMPLY],[REMARKS]) VALUES (" + quotation.QuotationItems[i].QuotationItemId + ",'" + quotation.QuotationItems[i].SupplierBOMs[j].Material.ProcessString() + "','" + quotation.QuotationItems[i].SupplierBOMs[j].Description.ProcessString() + "'," + quotation.QuotationItems[i].SupplierBOMs[j].Comply + ",'" + quotation.QuotationItems[i].SupplierBOMs[j].Remarks.ProcessString() + "');");
                }
            }

            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = sql.ToString();
            return dBConnection.cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Used to update supplier quotation after partially submitting. means stayed as later bid
        /// </summary>
        /// <param name="quotation"></param>
        /// <param name="dBConnection"></param>
        /// <returns></returns>
        public int UpdateSupplierPendingQuotation(SupplierQuotation quotation, DBConnection dBConnection)
        {
            StringBuilder sql = new StringBuilder();

            //updating supplier quotation details
            sql.AppendLine("UPDATE SUPPLIER_QUOTATION SET [SUB_TOTAL]=" + quotation.SubTotal + ",[VAT_AMOUNT]=" + quotation.VatAmount + ",[NBT_AMOUNT]=" + quotation.NbtAmount + ",[NET_TOTAL]=" + quotation.NetTotal + ",[SUPPLIER_TERMS_CONDITIONS]='" + quotation.TermsAndCondition.ProcessString() + "',[IS_STAYED_AS_LATER_BID]=" + quotation.IsStayedAsLaterBid + " WHERE [QUOTATION_ID]=" + quotation.QuotationId + ";");


            //Inserting Quotation images for Quotation
            for (int i = 0; i < quotation.QuotationImages.Count; i++)
            {
                sql.AppendLine("INSERT INTO [QUOTATION_IMAGES] ([QUOTATION_ID],[IMAGE_PATH]) VALUES (" + quotation.QuotationId + ",'" + quotation.QuotationImages[i].ImagePath.ProcessString() + "');");
            }

            //Inserting Files uploaded for Quotation
            for (int i = 0; i < quotation.UploadedFiles.Count; i++)
            {
                sql.AppendLine("INSERT INTO [SUPPLIER_BIDDING_FILE_UPLOAD] ([QUOTATION_ID],[FILE_NAME],[FILE_PATH]) VALUES (" + quotation.QuotationId + ",'" + quotation.UploadedFiles[i].FileName.ProcessString() + "','" + quotation.UploadedFiles[i].FilePath.ProcessString() + "');");
            }


            //saving quotation item Id to use in quotation image, bom and file insertions
            sql.AppendLine("DECLARE @QUOTATION_ITEM_TABLE TABLE (ID INT);");

            for (int i = 0; i < quotation.QuotationItems.Count; i++)
            {
                //Inserting quotation items for quotation
                sql.AppendLine("INSERT INTO SUPPLIER_QUOTATION_ITEM ([QUOTATION_ID],[BIDDING_ITEM_ID],[ITEM_ID],[QTY],[UNIT_PRICE],[VAT_AMOUNT],[NBT_AMOUNT],[TOTAL_AMOUNT])");
                sql.AppendLine("VALUES (" + quotation.QuotationId + ", " + quotation.QuotationItems[i].BiddingItemId + "," + quotation.QuotationItems[i].ItemId + "," + quotation.QuotationItems[i].Qty + "," + quotation.QuotationItems[i].UnitPrice + "," + quotation.QuotationItems[i].VatAmount + "," + quotation.QuotationItems[i].NbtAmount + "," + quotation.QuotationItems[i].TotalAmount + ");");


                //Inserting BOM for Quotation items
                for (int j = 0; j < quotation.QuotationItems[i].SupplierBOMs.Count; j++)
                {
                    sql.AppendLine("INSERT INTO [SUPPLIER_BOM] ([QUOTATION_ITEM_ID],[MATERIAL],[DESCRIPTION],[COMPLY],[REMARKS]) VALUES ((SELECT MAX(ID) FROM @QUOTATION_ITEM_TABLE),'" + quotation.QuotationItems[i].SupplierBOMs[j].Material.ProcessString() + "','" + quotation.QuotationItems[i].SupplierBOMs[j].Description.ProcessString() + "'," + quotation.QuotationItems[i].SupplierBOMs[j].Comply + ",'" + quotation.QuotationItems[i].SupplierBOMs[j].Remarks.ProcessString() + "');");
                }
            }
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = sql.ToString();
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int SelectSupplierQuotationAtSelection(int QuotationId, string Remarks, int BidId, int UserId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            string sql = "UPDATE SUPPLIER_QUOTATION SET IS_SELECTED = 1, SELECTION_REMARKS='" + Remarks.ProcessString() + "', SELECTED_BY=" + UserId + ",SELECTED_ON='" +  LocalTime.Now + "' WHERE QUOTATION_ID = " + QuotationId + "; ";
            sql += "UPDATE SUPPLIER_QUOTATION SET IS_SELECTED = 2 WHERE QUOTATION_ID != " + QuotationId + " AND BID_ID =" + BidId + "; ";
            sql += "UPDATE BIDDING SET IS_QUOTATION_SELECTED = 1,QUOTATION_SELECTED_BY=" + UserId + ",QUOTATION_SELECTION_DATE='" +  LocalTime.Now + "' WHERE BID_ID = " + BidId + "; ";

            sql += "UPDATE PR_DETAIL SET CURRENT_STATUS= 5 WHERE PRD_ID IN(SELECT PRD_ID FROM BIDDING_ITEM WHERE BID_ID=" + BidId + "); ";

            sql += "INSERT INTO PR_DETAIL_STATUS_LOG SELECT PRD_ID,6,'" +  LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BID_ID=" + BidId + "; ";

            dBConnection.cmd.CommandText = sql;
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int ResetSelections(int BidId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            string sql = "UPDATE SUPPLIER_QUOTATION SET IS_SELECTED = 0, SELECTION_REMARKS=NULL WHERE BID_ID = " + BidId + "; ";
            sql += "UPDATE BIDDING SET IS_QUOTATION_SELECTED = 0,IS_QUOTATION_APPROVED = 0, QUOTATION_APPROVAL_REMARKS = NULL,IS_QUOTATION_CONFIRMED = 0, QUOTATION_cONFIRMATION_REMARKS = NULL WHERE BID_ID = " + BidId + "; ";
            dBConnection.cmd.CommandText = sql;
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int RejectSupplierQuotationAtSelection(int QuotationId, string Remarks, int UserId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE SUPPLIER_QUOTATION SET IS_SELECTED = 2, SELECTION_REMARKS='" + Remarks.ProcessString() + "', SELECTED_BY=" + UserId + ",SELECTED_ON='" +  LocalTime.Now + "' WHERE QUOTATION_ID = " + QuotationId + "; ";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int SelectSupplierQuotationAtApproval(int QuotationId, string Remarks, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            string sql = "UPDATE SUPPLIER_QUOTATION SET IS_SELECTED = 1, SELECTION_REMARKS='" + Remarks.ProcessString() + "' WHERE QUOTATION_ID = " + QuotationId + "; ";
            sql += "UPDATE SUPPLIER_QUOTATION SET IS_SELECTED = 2 WHERE QUOTATION_ID != " + QuotationId + "; ";
            dBConnection.cmd.CommandText = sql;
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int SelectSupplierQuotationAtConfirmation(int QuotationId, string Remarks, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            string sql = "UPDATE SUPPLIER_QUOTATION SET IS_SELECTED = 1, IS_APPROVED= 1, SELECTION_REMARKS='" + Remarks.ProcessString() + "' WHERE QUOTATION_ID = " + QuotationId + "; ";
            sql += "UPDATE SUPPLIER_QUOTATION SET IS_SELECTED = 2 AND IS_APPROVED= 2 WHERE QUOTATION_ID != " + QuotationId + "; ";
            dBConnection.cmd.CommandText = sql;
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public SupplierQuotation GetSupplierQuotationForABid(int BidId, int SupplierId, int CompanyId, DBConnection dBConnection)
        {
            SupplierQuotation quotation;
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION WHERE SUPPLIER_ID=" + SupplierId + " AND BID_ID = " + BidId;
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                quotation = dataAccessObject.GetSingleOject<SupplierQuotation>(dBConnection.dr);
            }

            if (quotation != null)
            {
                //getting quotation items for quotations
                quotation.QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(quotation.QuotationId, CompanyId, dBConnection);

                quotation.QuotationImages = DAOFactory.createQuotationImageDAO().GetQuotationImages(quotation.QuotationId, dBConnection);
                quotation.UploadedFiles = DAOFactory.CreateSupplierBiddingFileUploadDAO().GetUploadedFiles(quotation.QuotationId, dBConnection);

                //getting boms, images and files uploaded for quotation items
                for (int d = 0; d < quotation.QuotationItems.Count; d++)
                {
                    quotation.QuotationItems[d].SupplierBOMs = DAOFactory.CreateSupplierBOMDAO().GetSupplierBom(quotation.QuotationItems[d].QuotationItemId, dBConnection);
                }
            }

            return quotation;

        }




        public List<int> GetSelectableQuotationIdsForLoggedInUser(int UserId, int DesignationId, int CompanyId, DBConnection dBConnection)
        {
            List<int> ids = new List<int>();

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[GET_SELECTABLE_QUOTATIONS]";
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@COMPANY_ID", CompanyId);

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

        public List<int> GetQuotationsByBidId(int bidId, DBConnection dBConnection) {
            List<int> ids = new List<int>();

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT QUOTATION_ID FROM SUPPLIER_QUOTATION WHERE BID_ID = "+ bidId + " ";
            
            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                if (dBConnection.dr.HasRows) {
                    while (dBConnection.dr.Read()) {
                        ids.Add(int.Parse(dBConnection.dr[0].ToString()));
                    }
                }
            }

            return ids;
        }

        public List<int> GetSelectionPendingQuotationIdsForLoggedInUser(int UserId, int DesignationId, int CompanyId, DBConnection dBConnection)
        {
            List<int> ids = new List<int>();

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[GET_SELECTION_PENDING_QUOTATIONS]";
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@COMPANY_ID", CompanyId);

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

        public int ApproveSupplierQuotation(int QuotationId, string Remarks, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            string sql = "UPDATE SUPPLIER_QUOTATION SET IS_APPROVED = 1,APPROVAL_REMARKS='" + Remarks.ProcessString() + "' WHERE QUOTATION_ID = " + QuotationId + ";";
            dBConnection.cmd.CommandText = sql;
            return dBConnection.cmd.ExecuteNonQuery();
        }

        

        public int RejectSupplierQuotationsAtApproval(int QuotationId, string Remarks, int BidId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            string sql = "UPDATE SUPPLIER_QUOTATION SET IS_SELECTED = 0,IS_APPROVED = 2,APPROVAL_REMARKS='" + Remarks.ProcessString() + "',IS_CONFIRMED_APPROVAL=0 WHERE QUOTATION_ID = " + QuotationId + "; ";
            sql += "UPDATE " + dbLibrary + ".BIDDING SET IS_QUOTATION_SELECTED = 0 WHERE BID_ID = " + BidId + "; ";
            dBConnection.cmd.CommandText = sql;
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int ConfirmSupplierQuotation(int QuotationId, string Remarks, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            string sql = "UPDATE SUPPLIER_QUOTATION SET IS_CONFIRMED = 1,CONFIRMATION_REMARKS='" + Remarks.ProcessString() + "' WHERE QUOTATION_ID = " + QuotationId + " ; ";
            dBConnection.cmd.CommandText = sql;
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateSupplierImports(int QuotationId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();
            string sql = "UPDATE SUPPLIER_QUOTATION SET SUB_TOTAL = (SELECT SUM(SUB_TOTAL) FROM SUPPLIER_QUOTATION_ITEM WHERE QUOTATION_ID = "+ QuotationId + "), " +
                " VAT_AMOUNT = (SELECT SUM(VAT_AMOUNT) FROM SUPPLIER_QUOTATION_ITEM WHERE QUOTATION_ID = " + QuotationId + "),  " +
                " NET_TOTAL = (SELECT SUM(NET_TOTAL) FROM SUPPLIER_QUOTATION_ITEM WHERE QUOTATION_ID = " + QuotationId + ")  " +
                " WHERE QUOTATION_ID = " + QuotationId + "  ";
            dBConnection.cmd.CommandText = sql;
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int RejectSupplierQuotationsAtConfirmation(int QuotationId, string Remarks, int BidId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            string sql = "UPDATE SUPPLIER_QUOTATION SET IS_SELECTED = 0,IS_CONFIRMED_APPROVAL=2,CONFIRMATION_REMARKS='" + Remarks.ProcessString() + "' WHERE QUOTATION_ID = " + QuotationId + "; ";
            sql += "UPDATE " + dbLibrary + ".BIDDING SET IS_QUOTATION_SELECTED = 0 WHERE BID_ID = " + BidId + "; ";
            dBConnection.cmd.CommandText = sql;
            return dBConnection.cmd.ExecuteNonQuery(); ;
        }

        public SupplierQuotation GetSelectedQuotation(int BidId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION AS SQ\n" +
                "INNER JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS S ON SQ.SUPPLIER_ID= S.SUPPLIER_ID\n" +
                "WHERE SQ.IS_SELECTED= 1 AND  SQ.IS_APPROVED= 1 AND SQ.IS_ADDED_TO_PO =0 AND SQ.BID_ID = " + BidId;
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SupplierQuotation>(dBConnection.dr);
            }
        }

        public int PopulateRecommendation(int QuotationId, int CategoryId, decimal NetTotal, int UserId, int DesignationId, string Remarks, DBConnection dBConnection)
        {

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[POPULATE_RECOMMENDATION]";
            dBConnection.cmd.Parameters.AddWithValue("@QUOTATION_ID", QuotationId);
            dBConnection.cmd.Parameters.AddWithValue("@CATEGORY_ID", CategoryId);
            dBConnection.cmd.Parameters.AddWithValue("@AMOUNT", NetTotal);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@SELECTION_REMARKS", Remarks);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<int> GetRecommendableQuotations(int UserId, int DesignationId, DBConnection dBConnection)
        {
            List<int> ids = new List<int>();

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[GET_RECOMMENDABLE_QUOTATIONS]";
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

        public int OverrideRecommendation(int QuotationId, int UserId, int DesignationId, string Remarks, int Status, int CategoryId, decimal Amount, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[OVERRIDE_QUOTATION_RECOMMENDATION_MASTER]";
            dBConnection.cmd.Parameters.AddWithValue("@QUOTATION_ID", QuotationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);
            dBConnection.cmd.Parameters.AddWithValue("@STATUS", Status);
            dBConnection.cmd.Parameters.AddWithValue("@CATEGORY_ID", CategoryId);
            dBConnection.cmd.Parameters.AddWithValue("@AMOUNT", Amount);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }


        public int OverrideApproval(int QuotationId, int UserId, int DesignationId, string Remarks, int Status, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[OVERRIDE_QUOTATION_APPRVOAL_MASTER]";
            dBConnection.cmd.Parameters.AddWithValue("@QUOTATION_ID", QuotationId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_ID", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@LOGGED_IN_USER_DESIGNATION", DesignationId);
            dBConnection.cmd.Parameters.AddWithValue("@REMARKS", Remarks);
            dBConnection.cmd.Parameters.AddWithValue("@STATUS", Status);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<int> GetApprovableQuotations(int UserId, int DesignationId, DBConnection dBConnection)
        {
            List<int> ids = new List<int>();

            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[GET_APPROVABLE_QUOTATIONS]";
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

        public SupplierQuotation GetSupplierQuotationbyQutationId(int QutationId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION AS SQ\n" +
                "INNER JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS S ON SQ.SUPPLIER_ID= S.SUPPLIER_ID\n" +
                "WHERE SQ.QUOTATION_ID = " + QutationId;
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SupplierQuotation>(dBConnection.dr);
            }
        }

        public int DeleteSubmittedSupplierQuotation(int quotationId, int itemId, int quotationItemId, decimal subTotal, decimal vatAmount, decimal nbtAmount, decimal netTotal, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "Select COUNT(*) FROM " + dbLibrary + ".SUPPLIER_QUOTATION_ITEM  " +
                                          " WHERE  QUOTATION_ID = " + quotationId + "  ";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".SUPPLIER_QUOTATION_ITEM  " +
                                           " WHERE QUOTATION_ITEM_ID = "+ quotationItemId + " AND QUOTATION_ID = " + quotationId + " AND ITEM_ID = " + itemId + " ";

            dbConnection.cmd.CommandText += "DELETE FROM " + dbLibrary + ".IMPORT_QUOTATION_ITEM  " +
                                          " WHERE QUOTATION_ITEM_ID = " + quotationItemId + " AND QUOTATION_ID = " + quotationId + " ";
       
            dbConnection.cmd.CommandText += "UPDATE SUPPLIER_QUOTATION SET SUB_TOTAL = SUB_TOTAL-"+subTotal+", VAT_AMOUNT = VAT_AMOUNT-"+vatAmount+", NBT_AMOUNT = NBT_AMOUNT-"+nbtAmount+", NET_TOTAL = NET_TOTAL-"+netTotal+" WHERE QUOTATION_ID = " + quotationId + " ";

            dbConnection.cmd.ExecuteNonQuery();
            if (count == 1)
            {
                dbConnection.cmd.CommandText = "Select COUNT(*) FROM " + dbLibrary + ".SUPPLIER_QUOTATION_ITEM  " +
                                              " WHERE  QUOTATION_ID = " + quotationId + "  ";

                var exist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (exist == 0)
                {
                    dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".SUPPLIER_QUOTATION  " +
                                                   " WHERE QUOTATION_ID = " + quotationId + " ";
                    dbConnection.cmd.CommandText += "DELETE FROM " + dbLibrary + ".IMPORT_QUOTATION  " +
                                                   " WHERE QUOTATION_ID = " + quotationId + " ";
                    dbConnection.cmd.ExecuteNonQuery();
                }
            }
            return 1;
        }

        public SupplierQuotation GetImportDetails(int poId, int CompanyId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT PM.QUOTATION_ID, DPT.TERM_NAME, DPM.PAYMENT_MODE, CT.CURRENCY_SHORT_NAME FROM PO_MASTER AS PM \n" +
                                                "LEFT JOIN(SELECT QUOTATION_ID, CURRENCY_TYPE_ID FROM IMPORT_QUOTATION) AS SQC ON PM.QUOTATION_ID = SQC.QUOTATION_ID \n" +
                                                "LEFT JOIN(SELECT QUOTATION_ID, PAYMENT_MODE_ID FROM IMPORT_QUOTATION) AS SQP ON PM.QUOTATION_ID = SQP.QUOTATION_ID \n" +
                                                "LEFT JOIN(SELECT QUOTATION_ID, TERM_ID FROM IMPORT_QUOTATION) AS SQT ON PM.QUOTATION_ID = SQT.QUOTATION_ID \n" +
                                                "LEFT JOIN(SELECT CURRENCY_SHORT_NAME, CURRENCY_TYPE_ID FROM DEF_CURRENCY_TYPE) AS CT ON CT.CURRENCY_TYPE_ID = SQC.CURRENCY_TYPE_ID \n" +
                                                "LEFT JOIN(SELECT PAYMENT_MODE, PAYMENT_MODE_ID FROM DEF_PAYMENT_MODE) AS DPM ON DPM.PAYMENT_MODE_ID = SQP.PAYMENT_MODE_ID \n" +
                                                "LEFT JOIN(SELECT TERM_NAME, TERM_ID FROM DEF_PRICE_TERMS) AS DPT ON DPT.TERM_ID = SQT.TERM_ID \n" +
                                                "WHERE PM.PO_ID = "+ poId + " AND PM.DEPARTMENT_ID = " + CompanyId + " \n";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SupplierQuotation>(dBConnection.dr);
            }
        }
    }
}

