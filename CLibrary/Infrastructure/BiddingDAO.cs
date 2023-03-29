using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;
using System.Data.SqlClient;

namespace CLibrary.Infrastructure
{
    public interface BiddingDAO
    {
        int SaveBidding(int PrId, int ItemId, int BiddingItemId, int BiddingPrId, DateTime StartDateTime, DateTime EndDateTime, DateTime CreatedDateTime, string CreatedUser, int IsActive, int SeqIdBidOpening, int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, string BidTermsAndConditions, int defaultDisplayImage, string imageURL, DBConnection dbConnection);
        int UpdateBidding(int PrId, int ItemId, DBConnection dbConnection);
        List<Bidding> GetAllBiddingForRegisteredSupplier(int SupplierId, DBConnection dbConnection);
        List<Bidding> GetAllBiddingForNonRegisteredSupplier(int SupplierId, DBConnection dbConnection);
        Bidding GetBiddingDetails(int PrId, int ItemId, DBConnection dbConnection);

        Bidding GetBiddingOrderid(int PrId, int ItemId, DBConnection dbConnection);
        List<Bidding> GetAllBiddingDetaileWise(int SupplierId, DBConnection dbConnection);
        List<Bidding> GetAllLatestBidsForRegisteredSupplier(int SupplierId, DBConnection dbConnection);
        List<Bidding> GetAllLatestBidsForNonRegisteredSupplier(int SupplierId, DBConnection dbConnection);
        Bidding GetBiddingDetailsExisting(int PrId, int ItemId, int SupplierId, DBConnection dbConnection);
        List<Bidding> GetBiddingGeneralSettings(int PrId, int ItemId, DBConnection dbConnection);

        //IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 2 means (Admin Rejected this item from Bid Opening Supplier Portal)
        int UpdateBidRejectOrApproveStatus(int PrId, int ItemId, int IsApproveToviewInSupplierPortal, string RejectedReason, string BidOpeningId, DBConnection dbConnection);
        List<Bidding> FetchRejectedAndApprovedBids(int PrId, int companyId, DBConnection dbConnection);
        int UpdateBiddingPORaised(int PrId, int ItemId, int isPoRaiseFalg, DBConnection dbConnection);
        List<PreviousPurchase> GetLastPurchaseSupplier(int itemId, int departmentId, DBConnection dbConnection);

        //test purose
        int UpdateExpierBids(int departmentId, DBConnection dbConnection);

        List<Bidding> GetProgressPR(int departmentId, int isRegisteredSupplier, DBConnection dbConnection);
        List<Bidding> GetProgressPR(int supplierId, int departmentId, int isRegisteredSupplier, DBConnection dbConnection);
        List<Bidding> GetProgressPRItemsByPrId(int PRId, int departmentId, int supplierid, int isRegisteredSupplier, DBConnection dbConnection);

        //2018-08-28 Supplier Portal View PO
        List<Bidding> GetRaisedPOSupplier(int supplierId, DBConnection dbConnection);

        //2018-08-31 Serevice Use insted of [GetBiddingDetails] method query
        Bidding GetBiddingDetailsSvc(int PrId, int ItemId, string BiddingId, DBConnection dbConnection);

        List<Bidding> GetAllBiddingForRegisteredSupplierInitialBinding(DBConnection dbConnection);
        List<Bidding> GetAllBiddingForNonRegisteredSupplierInitialBinding(DBConnection dbConnection);

        List<Bidding> GetBiddingDetailsSvcInitialLoad(DBConnection dbConnection);
        List<Bidding> GetBiddingDetailsLatestInitialLoad(DBConnection dbConnection);
        List<Bidding> GetProgressPRItemsByPrIdManualBid(int PRId, int departmentId, int supplierid, int isRegisteredSupplier, DBConnection dbConnection);
        int updateDisplayImageUrl(string imageUrl, int PrId, int ItemId, DBConnection dbConnection);
        List<Bidding> GetAllBiddingForRegisteredSupplierNew(int SupplierId, DBConnection dbConnection);
        List<Bidding> GetAllBiddingForNonRegisteredSupplierNew(int SupplierId, DBConnection dbConnection);
        List<Bidding> GetAllLatestBidsForRegisteredSupplierNew(int SupplierId, DBConnection dbConnection);
        List<Bidding> GetAllLatestBidsForNonRegisteredSupplierNew(int SupplierId, DBConnection dbConnection);
        List<Bidding> GetAllLatestBidsOuter(DBConnection dbConnection);
        List<Bidding> GetAllBidsOuter(DBConnection dbConnection);


        //New Methods By Salman created on 2019-01-17

        List<Bidding> GetAllBidsForBidSubmission(int PrId, DBConnection dbConnection);
        List<Bidding> GetAllBidsForBidApproval(int PrId, DBConnection dbConnection);
        List<Bidding> GetAllBidsForQuotationComparison(int PrId, List<int> SelectionPendingBidIds, DBConnection dbConnection);
        List<Bidding> GetAllBidsForQuotationApproval(int PrId, List<int> TabulationIds, DBConnection dbConnection);
        List<Bidding> GetAllBidsForQuotationConfirmation(int PrId, List<int> TabulationIds, DBConnection dbConnection);
        List<int> SaveBids(List<Bidding> Bids, DBConnection dbConnection);
        //int SaveBids(List<Bidding> Bids, DBConnection dbConnection);
        int ApproveBids(List<int> BidIds,string Remarks, int UserId, DBConnection dbConnection);
        int RejecteBids(List<int> BidIds, List<int> prdIds, string Remarks, int UserId, DBConnection dbConnection);
        List<Bidding> GetBidsForQuotationSubmission(int PrId, DBConnection dbConnection);
        Bidding GetBidDetailsForQuotationSubmission(int BidId, int CompanyId, DBConnection dbConnection);
        int ApproveOrRejectSelectedQuotation(int BidId, int Status, string Remarks, int UserId, DBConnection dbConnection);
        int ConfirmOrRejectSelectedQuotation(int BidId, int Status, string Remarks, int UserId, DBConnection dbConnection);
        List<Bidding> GetAllBidsForPoCreation(int PrId, int UserId, DBConnection dbConnection);
        List<Bidding> GetInProgressBids(int CompanyId, DBConnection dbConnection);
        List<Bidding> GetClosedBids(int CompanyId, DBConnection dbConnection);
        List<Bidding> GetManualInProgressBids(int CompanyId,int userId, DBConnection dbConnection);
        List<Bidding> GetManualInProgressBidsWithItem(int CompanyId, int userId, DBConnection dbConnection);
        List<Bidding> GetManualClosedBids(int CompanyId, DBConnection dbConnection);
        int ExpireBid(int BidId, DBConnection dbConnection);
        Bid_Bond_Details GetBidBondDetailByBidId(int bidId, DBConnection dbConnection);
        List<Bidding> GetAllBidsForPRInquiry(int PrId, DBConnection dbConnection);
        List<int> GetBidCountForDashboard(int CompanyId,int yearsearch, int purchaseType, DBConnection dbConnection);
        List<int> GetSelectionPendingBidIds(int UserId, int DesignationId, int CompanyId, DBConnection dbConnection);
        Bidding GetBiddingDetailsByBiddingId(int BiddingId, DBConnection dbConnection);
        int ReOpenBid(int BidId, int UserId, DateTime date, string remark, DBConnection dbConnection);
        int ResetSelections(int BidId, DBConnection dBConnection);
        List<Bidding> FetchBidInfo(int PrId, DBConnection dbConnection);
        List<Bidding> GetRejectedBids(int prId,int companyId, DBConnection dbConnection);

        //new method  for terminate-Pasindu 2020/04/25
        int TerminateBid(int BidId, int UserId, string Remarks, DBConnection dbConnection);
        //By Adee on 24.04.2020
        List<Bidding> GetAllBidsForQuotationComparison(int PrId, DBConnection dbConnection);

         List<BiddingItem> GetPrdIdByBidId(int BidId, DBConnection dbConnection);
        Bidding GetBidDetailsByBidId(int BidId, DBConnection dbConnection);
        List<int> GetSelectionRejectedBidIds(int UserId, int DesignationId, int CompanyId, DBConnection dbConnection);
        List<int> GetSelectionPendingBidIdsForTabulationApproval(int UserId, int DesignationId, int CompanyId, DBConnection dbConnection);
        List<Bidding> GetManualClosedBidsByBidCode(int CompanyId, int BidCode, DBConnection dbConnection);
        List<Bidding> GetManualClosedBidsByPrCode(int CompanyId, string PrCode, DBConnection dbConnection);
        List<Bidding> GetClosedBidsByPrCode(int CompanyId, string PrCode, DBConnection dbConnection);
        List<Bidding> GetClosedBidsByBidCode(int CompanyId, int BidCode, DBConnection dbConnection);
        int UpdateEmailStatus(List<int> BidIds, int prId, DBConnection dbConnection);
        List<Bidding> FetchBidInfoForPRRequisitionReport(List<int> PrId, DBConnection dbConnection);
        List<Bidding> GetAllBidsForQuotationApprovalRej(int PrId, DBConnection dbConnection);
        List<Bidding> GetAllBidsForQuotationConfirmationRej(int PrId,  DBConnection dbConnection);
        Bidding GetBidDetailsForQuotationSubmissionImports(int BidId, int CompanyId, DBConnection dbConnection);
        List<Bidding> GetAllBidsForQuotationRejected(int PrId, List<int> TabulationIds, DBConnection dbConnection);
    }


    public class BiddingDAOSQLImpl : BiddingDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveBidding(int PrId, int ItemId, int BiddingItemId, int BiddingPrId, DateTime StartDateTime, DateTime EndDateTime, DateTime CreatedDateTime, string CreatedUser, int IsActive, int SeqIdBidOpening, int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, string BidTermsAndConditions, int defaultDisplayImage, string imageURL, DBConnection dbConnection)
        {
            //BIDDING_ORDER_ID
            string bid = string.Empty;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".BIDDING ";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0)
            {
                bid = "B" + 1;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT COUNT (BIDDING_ORDER_ID)+1 AS MAXid FROM " + dbLibrary + ".BIDDING ";
                count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                bid = "B" + count.ToString();
            }

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".BIDDING " +
                "(PR_ID, ITEM_ID, BIDDING_ITEM_ID, BIDDING_PR_ID, START_DATE, END_DATE,CREATED_DATE,CREATED_USER,IS_ACTIVE,SEQ_ID_BID_OPENING,DEPARTMENT_ID, BID_OPENING_PERIOD, CAN_OVERRIDE, BID_ONLY_REGISTERED_SUPPLIER,VIEW_BIDS_ONLINE_UPONPR_CREATION,IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL,BID_TERMS_AND_CONDITIONS,BIDDING_ORDER_ID,IS_PO_RAISED_FLAG,DEFAULT_DISPLAY_IMAGE , DISPLAY_IMAGE_URL) VALUES " +
                "( " + PrId + ", " + ItemId + " , " + BiddingItemId + ", " + BiddingPrId + ", '" + StartDateTime + "', '" + EndDateTime + "','" + CreatedDateTime + "','" + CreatedUser + "'," + IsActive + "," + SeqIdBidOpening + "," + DepartmentId + ", " + BidOpeningPeriod + " , " + CanOverride + ", " + BidOnlyRegisteredSupplier + ", " + ViewBidsOnlineUponPrCreation + ", " + 0 + ", '" + BidTermsAndConditions + "', '" + bid + "', " + 0 + "," + defaultDisplayImage + ",'" + imageURL + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateBidding(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".BIDDING SET IS_ACTIVE = 0 WHERE PR_ID =" + PrId + " AND ITEM_ID =" + ItemId + "; ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        //----------------Supplier Portal Load Data
        public List<Bidding> GetAllBiddingForRegisteredSupplier(int SupplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING AS BI" +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON BI.ITEM_ID =AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY AS SATC ON PM.DEPARTMENT_ID = SATC.COMPANY_ID " +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_CATEGORY AS SC ON SC.CATEGORY_ID = AI.CATEGORY_ID " +
                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "' AND SATC.SUPPLIER_ID = " + SupplierId + " AND SC.SUPPLIER_ID = " + SupplierId + " AND  SATC.SUPPLIER_FOLLOW = BI.BID_ONLY_REGISTERED_SUPPLIER AND BI.BID_ONLY_REGISTERED_SUPPLIER = 1 AND CD.DEPARTMENT_ID = AI.COMPANY_ID;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public Bidding GetBiddingDetails(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING AS BI" +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = BI.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PM.PR_ID = PD.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON AI.ITEM_ID = PD.ITEM_ID " +
                                           " LEFT JOIN " + dbLibrary + ".ITEM_IMAGE_UPLOAD AS IIU ON IIU.ITEM_ID = AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON CD.DEPARTMENT_ID = PM.DEPARTMENT_ID " +
                                           " WHERE PM.PR_ID = " + PrId + " AND PD.ITEM_ID = " + ItemId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllBiddingDetaileWise(int SupplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING AS BI" +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON BI.ITEM_ID = AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PM.PR_ID = PD.PR_ID AND  BI.ITEM_ID = PD.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY AS SATC ON PM.DEPARTMENT_ID = SATC.COMPANY_ID " +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " INNER JOIN " + dbLibrary + ".ITEM_IMAGE_UPLOAD AS IIU ON IIU.ITEM_ID = BI.ITEM_ID " +
                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "' AND SATC.SUPPLIER_ID = " + SupplierId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllLatestBidsForRegisteredSupplier(int SupplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT TOP 10 * FROM " + dbLibrary + ".BIDDING AS BI " +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON BI.ITEM_ID = AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON ( BI.PR_ID = PD.PR_ID AND  BI.ITEM_ID = PD.ITEM_ID )" +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY AS SATC " +
                                           " ON PM.DEPARTMENT_ID = SATC.COMPANY_ID " +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_CATEGORY AS SC ON SC.CATEGORY_ID = AI.CATEGORY_ID " +
                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "'  AND SATC.SUPPLIER_ID = " + SupplierId + " AND SC.SUPPLIER_ID = " + SupplierId + " AND  SATC.SUPPLIER_FOLLOW = BI.BID_ONLY_REGISTERED_SUPPLIER AND BI.BID_ONLY_REGISTERED_SUPPLIER = 1 AND CD.DEPARTMENT_ID = AI.COMPANY_ID ORDER BY BI.END_DATE DESC;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public Bidding GetBiddingDetailsExisting(int PrId, int ItemId, int SupplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING AS BI " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ ON BI.PR_ID = SQ.PR_ID " +
                                           " AND BI.ITEM_ID = SQ.ITEM_ID " +
                                           " WHERE BI.PR_ID = " + PrId + " AND BI.ITEM_ID= " + ItemId + "  " +
                                           " AND SQ.SUPPLIER_ID = " + SupplierId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetBiddingGeneralSettings(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".BIDDING WHERE PR_ID = " + PrId + " AND ITEM_ID= " + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public int UpdateBidRejectOrApproveStatus(int PrId, int ItemId, int IsApproveToviewInSupplierPortal, string RejectedReason, string BidOpeningId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".BIDDING  SET IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = " + IsApproveToviewInSupplierPortal + ",REASON_TO_REJECT_BID_OPENING='" + RejectedReason + "'  WHERE PR_ID=" + PrId + " AND ITEM_ID =" + ItemId + " AND BIDDING_ORDER_ID ='" + BidOpeningId + "'; " +
                "UPDATE " + dbLibrary + ".PR_DETAIL SET PR_IS_APPROVED_FOR_BID=1 WHERE PR_ID=" + PrId + " AND ITEM_ID =" + ItemId + "; ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Bidding> FetchRejectedAndApprovedBids(int PrId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING AS BI " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PD.PR_ID = BI.PR_ID AND PD.ITEM_ID = BI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON BI.ITEM_ID = AI.ITEM_ID  " +
                                           " INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON IC.CATEGORY_ID = AI.CATEGORY_ID " +
                                           " INNER JOIN " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC ON ISC.SUB_CATEGORY_ID  = AI.SUB_CATEGORY_ID WHERE BI.PR_ID = " + PrId + " AND AI.COMPANY_ID = " + companyId + " AND IC.COMPANY_ID =" + companyId + " AND ISC.COMPANY_ID = " + companyId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public int UpdateBiddingPORaised(int PrId, int ItemId, int isPoRaiseFalg, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".BIDDING SET IS_PO_RAISED_FLAG =" + isPoRaiseFalg + "  WHERE PR_ID =" + PrId + " AND ITEM_ID=" + ItemId + " AND IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL=1; ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateExpierBids(int departmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "update " + dbLibrary + ".BIDDING set END_DATE = '" + LocalTime.Now + "' WHERE DEPARTMENT_ID=" + departmentId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PreviousPurchase> GetLastPurchaseSupplier(int itemId, int departmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".GRN_MASTER AS GM INNER JOIN " + dbLibrary + ".GRN_DETAILS AS GD ON GM.GRN_ID = GD.GRN_ID" +
                                            " INNER JOIN " + dbLibrary + ".SUPPLIER AS SUP ON GM.SUPPLIER_ID = SUP.SUPPLIER_ID " +
                                            " INNER JOIN " + dbLibrary + ".SUPPLIER_RATINGS AS SUPR ON SUPR.SUPPLIER_ID = SUP.SUPPLIER_ID " +
                                            "WHERE GD.ITEM_ID = " + itemId + " AND GM.DEPARTMENT_ID= " + departmentId + "  AND GD.IS_GRN_APPROVED= 1 " +
                                            "ORDER BY(GD.GRN_APPROVED_DATE_TIME) DESC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PreviousPurchase>(dbConnection.dr);
            }
        }

        public List<Bidding> GetProgressPR(int departmentId, int isRegisteredSupplier, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            if (isRegisteredSupplier == 1)
            {
                dbConnection.cmd.CommandText = "SELECT DISTINCT pm.PR_ID, pm.PR_CODE FROM " + dbLibrary + ".BIDDING as bd " +
                                                          " INNER JOIN " + dbLibrary + ".PR_MASTER as pm ON bd.PR_ID = pm.PR_ID " +
                                                          " WHERE bd.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 4  AND  '" +  LocalTime.Now + "' < bd.END_DATE AND pm.DEPARTMENT_ID = " + departmentId + "";

            }
            else if (isRegisteredSupplier == 0)
            {
                dbConnection.cmd.CommandText = "SELECT DISTINCT pm.PR_ID, pm.PR_CODE FROM " + dbLibrary + ".BIDDING as bd " +
                                         " INNER JOIN " + dbLibrary + ".PR_MASTER as pm ON bd.PR_ID = pm.PR_ID " +
                                         " WHERE bd.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 4  AND '" +  LocalTime.Now + "' < bd.END_DATE AND pm.DEPARTMENT_ID = " + departmentId + " AND bd.BID_ONLY_REGISTERED_SUPPLIER = " + isRegisteredSupplier + "";

            }
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetProgressPR(int supplierId, int departmentId, int isRegisteredSupplier, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            if (isRegisteredSupplier == 1)
            {
                dbConnection.cmd.CommandText = "SELECT DISTINCT D.PR_ID,D.PR_CODE FROM " + dbLibrary + ".SUPPLIER_CATEGORY AS A " +
                                                    "INNER JOIN(SELECT ITEM_ID, CATEGORY_ID FROM " + dbLibrary + ".ADD_ITEMS)AS B " +
                                                    "ON A.CATEGORY_ID = B.CATEGORY_ID " +
                                                    "INNER JOIN(SELECT PR_ID, ITEM_ID FROM " + dbLibrary + ".PR_DETAIL) AS C " +
                                                    "ON B.ITEM_ID = C.ITEM_ID " +
                                                    "INNER JOIN(SELECT PR_CODE, PR_ID, DEPARTMENT_ID FROM " + dbLibrary + ".PR_MASTER) AS D " +
                                                    "ON C.PR_ID = D.PR_ID " +
                                                    "INNER JOIN(SELECT PR_ID, IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL, END_DATE, BID_ONLY_REGISTERED_SUPPLIER FROM " + dbLibrary + ".BIDDING) AS E " +
                                                    "ON D.PR_ID = E.PR_ID " +
                                                    "WHERE A.SUPPLIER_ID = " + supplierId + " " +
                                                    "AND D.DEPARTMENT_ID = " + departmentId + " " +
                                                    "AND (E.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 4 OR E.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 1) " +
                                                    "AND  '" +  LocalTime.Now + "' < E.END_DATE";

            }
            else if (isRegisteredSupplier == 0)
            {
                dbConnection.cmd.CommandText = "SELECT DISTINCT D.PR_ID,D.PR_CODE FROM " + dbLibrary + ".SUPPLIER_CATEGORY AS A " +
                                                    "INNER JOIN(SELECT ITEM_ID, CATEGORY_ID FROM " + dbLibrary + ".ADD_ITEMS)AS B " +
                                                    "ON A.CATEGORY_ID = B.CATEGORY_ID " +
                                                    "INNER JOIN(SELECT PR_ID, ITEM_ID FROM " + dbLibrary + ".PR_DETAIL) AS C " +
                                                    "ON B.ITEM_ID = C.ITEM_ID " +
                                                    "INNER JOIN(SELECT PR_CODE, PR_ID, DEPARTMENT_ID FROM " + dbLibrary + ".PR_MASTER) AS D " +
                                                    "ON C.PR_ID = D.PR_ID " +
                                                    "INNER JOIN(SELECT PR_ID, IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL, END_DATE, BID_ONLY_REGISTERED_SUPPLIER FROM " + dbLibrary + ".BIDDING) AS E " +
                                                    "ON D.PR_ID = E.PR_ID " +
                                                    "WHERE A.SUPPLIER_ID = " + supplierId + " " +
                                                    "AND D.DEPARTMENT_ID = " + departmentId + " " +
                                                    "AND (E.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 4 OR E.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 1) " +
                                                    "AND  '" +  LocalTime.Now + "' < E.END_DATE " +
                                                    "AND E.BID_ONLY_REGISTERED_SUPPLIER = 0";

            }
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetProgressPRItemsByPrId(int PRId, int departmentId, int supplierid, int isRegisteredSupplier, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            if (isRegisteredSupplier == 1)
            {
                dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".BIDDING as bd" +
                                          " INNER JOIN " + dbLibrary + ".PR_MASTER as pm ON bd.PR_ID = pm.PR_ID" +
                                          " INNER JOIN " + dbLibrary + ".ADD_ITEMS as ai ON bd.ITEM_ID = ai.ITEM_ID " +
                                          " INNER JOIN " + dbLibrary + ".SUPPLIER_CATEGORY AS sc ON sc.CATEGORY_ID = ai.CATEGORY_ID " +
                                          " where bd.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 4   AND '" +  LocalTime.Now + "' < bd.END_DATE AND pm.DEPARTMENT_ID = " + departmentId + " AND BD.PR_ID = " + PRId + "  AND sc.SUPPLIER_ID = " + supplierid + "";

            }
            else if (isRegisteredSupplier == 0)
            {
                dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".BIDDING as bd" +
                                         " INNER JOIN " + dbLibrary + ".PR_MASTER as pm ON bd.PR_ID = pm.PR_ID" +
                                         " INNER JOIN " + dbLibrary + ".ADD_ITEMS as ai ON bd.ITEM_ID = ai.ITEM_ID " +
                                         " INNER JOIN " + dbLibrary + ".SUPPLIER_CATEGORY AS sc ON sc.CATEGORY_ID = ai.CATEGORY_ID " +
                                         " where bd.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 4  AND '" +  LocalTime.Now + "' < bd.END_DATE AND pm.DEPARTMENT_ID = " + departmentId + " AND BD.PR_ID = " + PRId + "  AND sc.SUPPLIER_ID = " + supplierid + " AND bd.BID_ONLY_REGISTERED_SUPPLIER = 0";

            }


            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllBiddingForNonRegisteredSupplier(int SupplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING AS BI " +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON BI.ITEM_ID = AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY AS SATC ON PM.DEPARTMENT_ID = SATC.COMPANY_ID " +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD  ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_CATEGORY AS SC ON SC.CATEGORY_ID = AI.CATEGORY_ID " +
                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "' AND SATC.SUPPLIER_ID = " + SupplierId + " AND SC.SUPPLIER_ID = " + SupplierId + " AND BI.BID_ONLY_REGISTERED_SUPPLIER = 0 AND CD.DEPARTMENT_ID = AI.COMPANY_ID;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllLatestBidsForNonRegisteredSupplier(int SupplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT TOP 10 * FROM " + dbLibrary + ".BIDDING AS BI " +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON BI.ITEM_ID = AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON ( BI.PR_ID = PD.PR_ID AND  BI.ITEM_ID = PD.ITEM_ID )" +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY AS SATC  ON PM.DEPARTMENT_ID = SATC.COMPANY_ID " +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_CATEGORY AS SC ON SC.CATEGORY_ID = AI.CATEGORY_ID " +
                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "'   AND SATC.SUPPLIER_ID = " + SupplierId + " AND SC.SUPPLIER_ID = " + SupplierId + " AND  BI.BID_ONLY_REGISTERED_SUPPLIER = 0 AND CD.DEPARTMENT_ID = AI.COMPANY_ID ORDER BY BI.END_DATE DESC;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public Bidding GetBiddingOrderid(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING AS BI " +
                                           " WHERE  BI.PR_ID = " + PrId + " AND  BI.ITEM_ID = " + ItemId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetRaisedPOSupplier(int supplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT PM.PO_CODE, PM.CREATED_DATE,PM.PO_ID " +
                                           " FROM " + dbLibrary + ".PO_MASTER AS PM INNER JOIN " + dbLibrary + ".PO_DETAILS AS PD " +
                                           " ON PM.PO_ID = PD.PO_ID " +
                                           " WHERE PM.SUPPLIER_ID = " + supplierId + " AND PD.IS_PO_APPROVED = 1 " +
                                           " GROUP BY PM.PO_CODE, PM.CREATED_DATE,PM.PO_ID ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        //2018-08-31 Serevice Use insted of [GetBiddingDetails] method query
        public Bidding GetBiddingDetailsSvc(int PrId, int ItemId, string BiddingId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING  AS BI " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = BI.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PM.PR_ID = PD.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON AI.ITEM_ID = PD.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".ITEM_IMAGE_UPLOAD AS IIU ON IIU.ITEM_ID = AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON CD.DEPARTMENT_ID = PM.DEPARTMENT_ID " +
                                           " WHERE PM.PR_ID = " + PrId + " AND PD.ITEM_ID = " + ItemId + " AND BI.BIDDING_ORDER_ID = '" + BiddingId + "'";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllBiddingForRegisteredSupplierInitialBinding(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING AS BI " +
                                           " INNER JOIN  " + dbLibrary + ".ADD_ITEMS AS AI ON BI.ITEM_ID = AI.ITEM_ID " +
                                           " INNER JOIN  " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
                                           " INNER JOIN  " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY AS SATC ON PM.DEPARTMENT_ID = SATC.COMPANY_ID " +
                                           " INNER JOIN  " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " INNER JOIN  " + dbLibrary + ".ITEM_IMAGE_UPLOAD AS IIU ON IIU.ITEM_ID = BI.ITEM_ID " +
                                           " INNER JOIN  " + dbLibrary + ".SUPPLIER_CATEGORY AS SC ON SC.CATEGORY_ID = AI.CATEGORY_ID " +
                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "'  AND  SATC.SUPPLIER_FOLLOW = BI.BID_ONLY_REGISTERED_SUPPLIER AND BI.BID_ONLY_REGISTERED_SUPPLIER = 1;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllBiddingForNonRegisteredSupplierInitialBinding(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING AS BI " +
                                           " INNER JOIN  " + dbLibrary + ".ADD_ITEMS AS AI ON BI.ITEM_ID = AI.ITEM_ID " +
                                           " INNER JOIN  " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
                                           " INNER JOIN  " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY AS SATC ON PM.DEPARTMENT_ID = SATC.COMPANY_ID " +
                                           " INNER JOIN  " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " INNER JOIN  " + dbLibrary + ".ITEM_IMAGE_UPLOAD  AS IIU ON IIU.ITEM_ID = BI.ITEM_ID " +
                                           " INNER JOIN  " + dbLibrary + ".SUPPLIER_CATEGORY  AS SC ON SC.CATEGORY_ID = AI.CATEGORY_ID " +
                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "'  AND BI.BID_ONLY_REGISTERED_SUPPLIER = 0;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetBiddingDetailsSvcInitialLoad(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText =
                " SELECT BI.BIDDING_ORDER_ID,PM.PR_ID,BI.ITEM_ID, " +
                " BI.START_DATE, BI.END_DATE, AI.ITEM_NAME, " +
                " CD.DEPARTMENT_ID,CD.DEPARTMENT_NAME,CD.DEPARTMENT_IMAGE_PATH,IIU.IMAGE_PATH " +
                " FROM " + dbLibrary + ".BIDDING AS BI " +
                " INNER JOIN  " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = BI.PR_ID " +
                " INNER JOIN  " + dbLibrary + ".PR_DETAIL AS PD ON PM.PR_ID = PD.PR_ID  " +
                " INNER JOIN  " + dbLibrary + ".ADD_ITEMS AS AI ON AI.ITEM_ID = BI.ITEM_ID  " +
                " INNER JOIN  " + dbLibrary + ".ITEM_IMAGE_UPLOAD AS IIU ON IIU.ITEM_ID = BI.ITEM_ID  " +
                " INNER JOIN  " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON CD.DEPARTMENT_ID = BI.DEPARTMENT_ID " +
                " WHERE BI.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL =1 AND BI.END_DATE > '" +  LocalTime.Now + "'  " +
                " GROUP BY BI.BIDDING_ORDER_ID,PM.PR_ID,BI.ITEM_ID, " +
                " BI.START_DATE, BI.END_DATE, AI.ITEM_NAME, " +
                " CD.DEPARTMENT_ID,CD.DEPARTMENT_NAME, " +
                " CD.DEPARTMENT_IMAGE_PATH , IIU.IMAGE_PATH";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetBiddingDetailsLatestInitialLoad(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText =
               " SELECT TOP 10 * FROM " + dbLibrary + ".BIDDING AS BI" +
               " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON BI.ITEM_ID = AI.ITEM_ID " +
               " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
               " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON BI.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
               " INNER JOIN " + dbLibrary + ".ITEM_IMAGE_UPLOAD AS IIU ON IIU.ITEM_ID = BI.ITEM_ID " +
               " WHERE BI.END_DATE > '" +  LocalTime.Now + "' " +
               " AND BI.BID_ONLY_REGISTERED_SUPPLIER = 1 ORDER BY BI.END_DATE DESC";


            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }


        public List<Bidding> GetProgressPRItemsByPrIdManualBid(int PRId, int departmentId, int supplierid, int isRegisteredSupplier, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            if (isRegisteredSupplier == 1)
            {
                dbConnection.cmd.CommandText = "SELECT DISTINCT B.ITEM_ID,B.ITEM_NAME FROM " + dbLibrary + ".SUPPLIER_CATEGORY AS A " +
                                                    "INNER JOIN(SELECT ITEM_ID, CATEGORY_ID, ITEM_NAME FROM " + dbLibrary + ".ADD_ITEMS)AS B " +
                                                    "ON A.CATEGORY_ID = B.CATEGORY_ID " +
                                                    "INNER JOIN(SELECT PR_ID, ITEM_ID FROM " + dbLibrary + ".PR_DETAIL) AS C " +
                                                    "ON B.ITEM_ID = C.ITEM_ID " +
                                                    "INNER JOIN(SELECT PR_CODE, PR_ID, DEPARTMENT_ID FROM " + dbLibrary + ".PR_MASTER) AS D " +
                                                    "ON C.PR_ID = D.PR_ID " +
                                                    "INNER JOIN(SELECT PR_ID, IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL, END_DATE, BID_ONLY_REGISTERED_SUPPLIER FROM " + dbLibrary + ".BIDDING) AS E " +
                                                    "ON D.PR_ID = E.PR_ID " +
                                                    "WHERE A.SUPPLIER_ID = " + supplierid + " " +
                                                    "AND C.PR_ID = " + PRId + " " +
                                                    "AND D.DEPARTMENT_ID = " + departmentId + " " +
                                                    "AND (E.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 4 OR E.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 1) " +
                                                    "AND  '" +  LocalTime.Now + "' < E.END_DATE";

            }
            else if (isRegisteredSupplier == 0)
            {
                dbConnection.cmd.CommandText = "SELECT DISTINCT B.ITEM_ID,B.ITEM_NAME FROM " + dbLibrary + ".SUPPLIER_CATEGORY AS A " +
                                                    "INNER JOIN(SELECT ITEM_ID, CATEGORY_ID, ITEM_NAME FROM " + dbLibrary + ".ADD_ITEMS)AS B " +
                                                    "ON A.CATEGORY_ID = B.CATEGORY_ID " +
                                                    "INNER JOIN(SELECT PR_ID, ITEM_ID FROM " + dbLibrary + ".PR_DETAIL) AS C " +
                                                    "ON B.ITEM_ID = C.ITEM_ID " +
                                                    "INNER JOIN(SELECT PR_CODE, PR_ID, DEPARTMENT_ID FROM " + dbLibrary + ".PR_MASTER) AS D " +
                                                    "ON C.PR_ID = D.PR_ID " +
                                                    "INNER JOIN(SELECT PR_ID, IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL, END_DATE, BID_ONLY_REGISTERED_SUPPLIER FROM " + dbLibrary + ".BIDDING) AS E " +
                                                    "ON D.PR_ID = E.PR_ID " +
                                                    "WHERE A.SUPPLIER_ID = " + supplierid + " " +
                                                    "AND C.PR_ID = " + PRId + " " +
                                                    "AND D.DEPARTMENT_ID = " + departmentId + " " +
                                                    "AND (E.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 4 OR E.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 1) " +
                                                    "AND  '" +  LocalTime.Now + "' < E.END_DATE " +
                                                    "AND E.BID_ONLY_REGISTERED_SUPPLIER = 0";

            }


            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }



        public int updateDisplayImageUrl(string imageUrl, int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".BIDDING SET DISPLAY_IMAGE_URL = '" + imageUrl + "' WHERE PR_ID =" + PrId + " AND ITEM_ID =" + ItemId + "; ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }


        public List<Bidding> GetAllBiddingForRegisteredSupplierNew(int SupplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING AS BI" +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS_MASTER AS AI ON BI.ITEM_ID =AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY AS SATC ON PM.DEPARTMENT_ID = SATC.COMPANY_ID " +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_CATEGORY AS SC ON SC.CATEGORY_ID = AI.CATEGORY_ID " +
                                           " INNER JOIN " + dbLibrary + ".ITEM_CATEGORY_MASTER AS ICM ON (AI.CATEGORY_ID = ICM.CATEGORY_ID)" +
                                           " INNER JOIN " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER AS ISCM ON (AI.SUB_CATEGORY_ID = ISCM.SUB_CATEGORY_ID)" +

                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "' AND SATC.SUPPLIER_ID = " + SupplierId + " AND SC.SUPPLIER_ID = " + SupplierId + " AND  SATC.SUPPLIER_FOLLOW = BI.BID_ONLY_REGISTERED_SUPPLIER AND BI.BID_ONLY_REGISTERED_SUPPLIER = 1;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllBiddingForNonRegisteredSupplierNew(int SupplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING AS BI " +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS_MASTER AS AI ON BI.ITEM_ID = AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY AS SATC ON PM.DEPARTMENT_ID = SATC.COMPANY_ID " +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD  ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_CATEGORY AS SC ON SC.CATEGORY_ID = AI.CATEGORY_ID " +

                                           " INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON (AI.CATEGORY_ID = ICM.CATEGORY_ID)" +
                                           " INNER JOIN ITEM_SUB_CATEGORY_MASTER AS ISCM ON (AI.SUB_CATEGORY_ID = ISCM.SUB_CATEGORY_ID)" +
                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "' AND SATC.SUPPLIER_ID = " + SupplierId + " AND SC.SUPPLIER_ID = " + SupplierId + " AND BI.BID_ONLY_REGISTERED_SUPPLIER = 0;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllLatestBidsForRegisteredSupplierNew(int SupplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT TOP 10 * FROM " + dbLibrary + ".BIDDING AS BI " +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON BI.ITEM_ID = AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON ( BI.PR_ID = PD.PR_ID AND  BI.ITEM_ID = PD.ITEM_ID )" +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY AS SATC " +
                                           " ON PM.DEPARTMENT_ID = SATC.COMPANY_ID " +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_CATEGORY AS SC ON SC.CATEGORY_ID = AI.CATEGORY_ID " +
                                            " INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON (AI.CATEGORY_ID = ICM.CATEGORY_ID)" +
                                           " INNER JOIN ITEM_SUB_CATEGORY_MASTER AS ISCM ON (AI.SUB_CATEGORY_ID = ISCM.SUB_CATEGORY_ID)" +
                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "'  AND SATC.SUPPLIER_ID = " + SupplierId + " AND SC.SUPPLIER_ID = " + SupplierId + " AND  SATC.SUPPLIER_FOLLOW = BI.BID_ONLY_REGISTERED_SUPPLIER AND BI.BID_ONLY_REGISTERED_SUPPLIER = 1 ORDER BY BI.END_DATE DESC;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllLatestBidsForNonRegisteredSupplierNew(int SupplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT TOP 10 * FROM " + dbLibrary + ".BIDDING AS BI " +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON BI.ITEM_ID = AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON ( BI.PR_ID = PD.PR_ID AND  BI.ITEM_ID = PD.ITEM_ID )" +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY AS SATC  ON PM.DEPARTMENT_ID = SATC.COMPANY_ID " +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER_CATEGORY AS SC ON SC.CATEGORY_ID = AI.CATEGORY_ID " +
                                            " INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON (AI.CATEGORY_ID = ICM.CATEGORY_ID)" +
                                           " INNER JOIN ITEM_SUB_CATEGORY_MASTER AS ISCM ON (AI.SUB_CATEGORY_ID = ISCM.SUB_CATEGORY_ID)" +
                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "'   AND SATC.SUPPLIER_ID = " + SupplierId + " AND SC.SUPPLIER_ID = " + SupplierId + " AND  BI.BID_ONLY_REGISTERED_SUPPLIER = 0 ORDER BY BI.END_DATE DESC;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }
        public List<Bidding> GetAllLatestBidsOuter(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT Top 10 * FROM " + dbLibrary + ".BIDDING AS BI" +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS_MASTER AS AI ON BI.ITEM_ID =AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +

                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllBidsOuter(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BIDDING AS BI" +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS_MASTER AS AI ON BI.ITEM_ID =AI.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON BI.PR_ID = PM.PR_ID " +

                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD ON PM.DEPARTMENT_ID = CD.DEPARTMENT_ID " +
                                           " WHERE BI.END_DATE > '" +  LocalTime.Now + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }





        //New Methods By Salman created on 2019-01-17

        public List<Bidding> GetAllBidsForBidSubmission(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING AS BI\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "WHERE BI.PR_ID=" + PrId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        //public int SaveBids(List<Bidding> Bids, DBConnection dbConnection)
        //{
        //    StringBuilder query = new StringBuilder("");

        //    //Saving BID ID to use in BID Item insertion
        //    query.AppendLine("DECLARE @BidIdTable TABLE (BidId INT);");

        //    for (int a = 0; a < Bids.Count; a++)
        //    {
        //        //Inserting Bids
        //        query.AppendLine("INSERT INTO BIDDING(BID_CODE,PR_ID,CREATED_USER,COMPANY_ID,START_DATE,END_DATE,BID_TYPE,BID_OPEN_TYPE,BID_OPENING_PERIOD,OPEN_BID_TO,BID_TERMS_AND_CONDITIONS,CATEGORY_ID,PURCHASING_OFFICER) ");
        //        query.AppendLine("OUTPUT INSERTED.BID_ID INTO @BidIdTable(BidId)");
        //        query.AppendLine("VALUES((SELECT COUNT(BID_CODE)+1 FROM BIDDING WHERE COMPANY_ID=" + Bids[a].CompanyId + ")," + Bids[a].PrId + "," + Bids[a].CreatedUserId + "," + Bids[a].CompanyId + ",'" + Bids[a].StartDate + "','" + Bids[a].EndDate + "'," + Bids[a].BidType + "," + Bids[a].BidOpenType + "," + Bids[a].BidOpeningPeriod + "," + Bids[a].BidOpenTo + ",'" + Bids[a].TermsAndConditions.ProcessString() + "'," + Bids[a].CategoryId + ",(SELECT  TOP 1 USER_ID FROM ITEM_CATEGORY_OWNERS WHERE CATEGORY_ID=" + Bids[a].CategoryId + " AND OWNER_TYPE='PO' AND EFFECTIVE_DATE <= '" +  LocalTime.Now + "' ORDER BY EFFECTIVE_DATE DESC));");

        //        //Inserting Bid Items to the bids
        //        for (int b = 0; b < Bids[a].BiddingItems.Count; b++)
        //        {
        //            query.AppendLine("INSERT INTO BIDDING_ITEM(BID_ID,PRD_ID,ITEM_ID,QTY,ESTIMATED_PRICE,DISPLAY_IMAGE)");
        //            query.AppendLine("VALUES((SELECT MAX(BidId) FROM @BidIdTable)," + Bids[a].BiddingItems[b].PrdId + "," + Bids[a].BiddingItems[b].ItemId + "," + Bids[a].BiddingItems[b].Qty + "," + Bids[a].BiddingItems[b].EstimatedPrice + ",'" + Bids[a].BiddingItems[b].DisplayImage.ProcessString() + "');");
        //            query.AppendLine("UPDATE PR_DETAIL SET SUBMIT_FOR_BID=1, CURRENT_STATUS=3 WHERE PRD_ID=" + Bids[a].BiddingItems[b].PrdId + ";");
        //            query.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG VALUES (" + Bids[a].BiddingItems[b].PrdId + ",3,'" +  LocalTime.Now + "'," + Bids[a].CreatedUserId + ");");
        //        }

        //        //Inserting Bidding plan 
        //        for (int b = 0; b < Bids[a].BiddingPlan.Count; b++)
        //        {
        //            query.AppendLine("INSERT INTO BIDDING_PLAN( BID_ID, PLAN_ID, START_DATE, END_DATE,ENTERED_DATE, ENTERED_USER)");
        //            query.AppendLine("VALUES((SELECT MAX(BidId) FROM @BidIdTable)," + Bids[a].BiddingPlan[b].PlanId + ",'" + Bids[a].BiddingPlan[b].StartDate + "' , '" + Bids[a].BiddingPlan[b].EndDate + "', '" +  LocalTime.Now + "'," + Bids[a].BiddingPlan[b].EnteredUser + ");");
        //            // query.AppendLine("UPDATE PR_DETAIL SET SUBMIT_FOR_BID=1 WHERE PRD_ID=" + Bids[a].BiddingPlan[b].PrdId);
        //        }

        //        //Inserting Bid Bond Details 
        //        for (int b = 0; b < Bids[a].BidBondDetails.Count; b++)
        //        {

        //            if (Bids[a].BidBondDetails[b].IsRequired == 1)
        //            {
        //                query.AppendLine("INSERT INTO BID_BOND_DETAILS( BID_ID, BOND_TYPE_ID, IS_REQUIRED, AMOUNT, PERCENTAGE, FROM_DATE, TO_DATE, ENTERED_DATE, ENTERED_USER)");
        //                query.AppendLine("VALUES((SELECT MAX(BidId) FROM @BidIdTable)," + Bids[a].BidBondDetails[b].BondtypeId + "," + Bids[a].BidBondDetails[b].IsRequired + "," + Bids[a].BidBondDetails[b].Amount + "," + Bids[a].BidBondDetails[b].Percentage + ",'" + Bids[a].BidBondDetails[b].FromDate + "', '" + Bids[a].BidBondDetails[b].ToDate + "', '" +  LocalTime.Now + "'," + Bids[a].BidBondDetails[b].EnteredUser + ");");
        //            }
        //            else
        //            {
        //                query.AppendLine("INSERT INTO BID_BOND_DETAILS( BID_ID, BOND_TYPE_ID, IS_REQUIRED, AMOUNT, PERCENTAGE, ENTERED_DATE, ENTERED_USER)");
        //                query.AppendLine("VALUES((SELECT MAX(BidId) FROM @BidIdTable)," + Bids[a].BidBondDetails[b].BondtypeId + "," + Bids[a].BidBondDetails[b].IsRequired + "," + Bids[a].BidBondDetails[b].Amount + "," + Bids[a].BidBondDetails[b].Percentage + ",'" +  LocalTime.Now + "'," + Bids[a].BidBondDetails[b].EnteredUser + ");");
        //            }

        //        }
        //    }

        //    dbConnection.cmd.Parameters.Clear();
        //    dbConnection.cmd.CommandText = query.ToString();
        //    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
        //    return dbConnection.cmd.ExecuteNonQuery();
        //}

        public List<int> SaveBids(List<Bidding> Bids, DBConnection dbConnection)
        {
            StringBuilder query = new StringBuilder("");

            //Saving BID ID to use in BID Item insertion
            query.AppendLine("DECLARE @BidIdTable TABLE (BidId INT);");
            for (int a = 0; a < Bids.Count; a++)
            {
                //Inserting Bids
                query.AppendLine("INSERT INTO BIDDING(BID_CODE,PR_ID,CREATED_USER,COMPANY_ID,START_DATE,END_DATE,BID_TYPE,BID_OPEN_TYPE,BID_OPENING_PERIOD,OPEN_BID_TO,BID_TERMS_AND_CONDITIONS,CATEGORY_ID,PURCHASING_OFFICER, IS_TABULATION_APPROVAL) ");
                query.AppendLine("OUTPUT INSERTED.BID_ID INTO @BidIdTable(BidId)");
                query.AppendLine("VALUES((SELECT COUNT(BID_CODE)+1 FROM BIDDING WHERE COMPANY_ID=" + Bids[a].CompanyId + ")," + Bids[a].PrId + "," + Bids[a].CreatedUserId + "," + Bids[a].CompanyId + ",'" + Bids[a].StartDate + "','" + Bids[a].EndDate + "'," + Bids[a].BidType + "," + Bids[a].BidOpenType + "," + Bids[a].BidOpeningPeriod + "," + Bids[a].BidOpenTo + ",'" + Bids[a].TermsAndConditions.ProcessString() + "'," + Bids[a].CategoryId + "," + Bids[a].PurchasingOfficer + ", 0);");

                //Inserting Bid Items to the bids
                for (int b = 0; b < Bids[a].BiddingItems.Count; b++)
                {
                    query.AppendLine("INSERT INTO BIDDING_ITEM(BID_ID,PRD_ID,ITEM_ID,QTY,ESTIMATED_PRICE,DISPLAY_IMAGE)");
                    query.AppendLine("VALUES((SELECT MAX(BidId) FROM @BidIdTable)," + Bids[a].BiddingItems[b].PrdId + "," + Bids[a].BiddingItems[b].ItemId + "," + Bids[a].BiddingItems[b].Qty + "," + Bids[a].BiddingItems[b].EstimatedPrice + ",'" + Bids[a].BiddingItems[b].DisplayImage.ProcessString() + "');");
                    query.AppendLine("UPDATE PR_DETAIL SET SUBMIT_FOR_BID=1, CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='APRVBID') WHERE PRD_ID=" + Bids[a].BiddingItems[b].PrdId + ";");
                    query.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG VALUES (" + Bids[a].BiddingItems[b].PrdId + ",(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='CRTDBID'),'" +  LocalTime.Now + "'," + Bids[a].CreatedUserId + ");");
                   
                }

                //Inserting Bidding plan 
                for (int b = 0; b < Bids[a].BiddingPlan.Count; b++)
                {
                    query.AppendLine("INSERT INTO BIDDING_PLAN( BID_ID, PLAN_ID, START_DATE, END_DATE,ENTERED_DATE, ENTERED_USER)");
                    query.AppendLine("VALUES((SELECT MAX(BidId) FROM @BidIdTable)," + Bids[a].BiddingPlan[b].PlanId + ",'" + Bids[a].BiddingPlan[b].StartDate + "' , '" + Bids[a].BiddingPlan[b].EndDate + "', '" +  LocalTime.Now + "'," + Bids[a].BiddingPlan[b].EnteredUser + ");");
                    // query.AppendLine("UPDATE PR_DETAIL SET SUBMIT_FOR_BID=1 WHERE PRD_ID=" + Bids[a].BiddingPlan[b].PrdId);
                }

                //Inserting Bid Bond Details 
                for (int b = 0; b < Bids[a].BidBondDetails.Count; b++)
                {

                    if (Bids[a].BidBondDetails[b].IsRequired == 1)
                    {
                        query.AppendLine("INSERT INTO BID_BOND_DETAILS( BID_ID, BOND_TYPE_ID, IS_REQUIRED, AMOUNT, PERCENTAGE, FROM_DATE, TO_DATE, ENTERED_DATE, ENTERED_USER)");
                        query.AppendLine("VALUES((SELECT MAX(BidId) FROM @BidIdTable)," + Bids[a].BidBondDetails[b].BondtypeId + "," + Bids[a].BidBondDetails[b].IsRequired + "," + Bids[a].BidBondDetails[b].Amount + "," + Bids[a].BidBondDetails[b].Percentage + ",'" + Bids[a].BidBondDetails[b].FromDate + "', '" + Bids[a].BidBondDetails[b].ToDate + "', '" +  LocalTime.Now + "'," + Bids[a].BidBondDetails[b].EnteredUser + ");");
                    }
                    else
                    {
                        query.AppendLine("INSERT INTO BID_BOND_DETAILS( BID_ID, BOND_TYPE_ID, IS_REQUIRED, AMOUNT, PERCENTAGE, ENTERED_DATE, ENTERED_USER)");
                        query.AppendLine("VALUES((SELECT MAX(BidId) FROM @BidIdTable)," + Bids[a].BidBondDetails[b].BondtypeId + "," + Bids[a].BidBondDetails[b].IsRequired + "," + Bids[a].BidBondDetails[b].Amount + "," + Bids[a].BidBondDetails[b].Percentage + ",'" +  LocalTime.Now + "'," + Bids[a].BidBondDetails[b].EnteredUser + ");");
                    }

                }
            }

            query.AppendLine("SELECT BidId FROM @BidIdTable");
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = query.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            List<int> ListbidId = new List<int>();
            SqlDataReader rdr = null;
            rdr = dbConnection.cmd.ExecuteReader();
            while (rdr.Read())
            {
                ListbidId.Add(Convert.ToInt32(rdr["BidId"].ToString()));
            }
            rdr.Close();
            //dbConnection.cmd.ExecuteNonQuery();
            return ListbidId;


        }

        public List<Bidding> GetAllBidsForBidApproval(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING AS BI\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "WHERE BI.PR_ID=" + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public int ApproveBids(List<int> BidIds, string Remarks, int UserId, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("UPDATE BIDDING SET IS_APPROVED = 1, APPROVAL_REMARKS='" + Remarks.ProcessString() + "', APPROVAL_BY=" + UserId + ", APPROVAL_DATE='" +  LocalTime.Now + "' WHERE BID_ID IN (");
            for (int i = 0; i < BidIds.Count; i++)
            {
                if (i != BidIds.Count - 1)
                    sql.Append(BidIds[i].ToString() + ",");
                else
                    sql.AppendLine(BidIds[i].ToString());
            }
            sql.AppendLine(");");

            //Updating PR Detail Current Status
            sql.AppendLine("UPDATE PR_DETAIL SET CURRENT_STATUS= (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='QTATNCOLECTN') WHERE PRD_ID IN(");
            sql.AppendLine("SELECT PRD_ID FROM BIDDING_ITEM WHERE BID_ID IN (");
            for (int i = 0; i < BidIds.Count; i++)
            {
                if (i != BidIds.Count - 1)
                    sql.Append(BidIds[i].ToString() + ",");
                else
                    sql.AppendLine(BidIds[i].ToString());
            }
            sql.AppendLine("));");

            //Inserting PR_DETAIL Status Update log
            sql.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG");
            sql.AppendLine("SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='BDAPPRVD'),'" +  LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BID_ID IN (");
            for (int i = 0; i < BidIds.Count; i++)
            {
                if (i != BidIds.Count - 1)
                    sql.Append(BidIds[i].ToString() + ",");
                else
                    sql.AppendLine(BidIds[i].ToString());
            }
            sql.AppendLine(");");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int RejecteBids(List<int> BidIds, List<int> prdIds, string Remarks, int UserId, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("UPDATE BIDDING SET IS_APPROVED = 2, APPROVAL_REMARKS='" + Remarks.ProcessString() + "', APPROVAL_BY=" + UserId + ", APPROVAL_DATE='" +  LocalTime.Now + "' WHERE BID_ID IN (");
            for (int i = 0; i < BidIds.Count; i++)
            {
                if (i != BidIds.Count - 1)
                    sql.Append(BidIds[i].ToString() + ",");
                else
                    sql.AppendLine(BidIds[i].ToString());
            }
            sql.AppendLine(");");

            sql.AppendLine();

            //Updating PR Detail Current Status and Bid submission status
            sql.AppendLine("UPDATE PR_DETAIL SET SUBMIT_FOR_BID=0, CURRENT_STATUS= 1 WHERE PRD_ID IN(");
            for (int i = 0; i < prdIds.Count; i++)
            {
                if (i != prdIds.Count - 1)
                    sql.Append(prdIds[i].ToString() + ",");
                else
                    sql.AppendLine(prdIds[i].ToString());
            }
            sql.AppendLine(");");

            //Inserting PR_DETAIL Status Update log
            for (int i = 0; i < prdIds.Count; i++)
            {
                sql.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG VALUES (" + prdIds[i] + ",5,'" +  LocalTime.Now + "'," + UserId + ");");
            }


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Bidding> GetAllBidsForQuotationComparison(int PrId, List<int> SelectionPendingBidIds, DBConnection dbConnection)
        {

            string sql = "SELECT * FROM BIDDING AS BI\n" +
            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
            "WHERE BI.PR_ID=" + PrId + " AND BI.BID_ID IN (";

            for (int i = 0; i < SelectionPendingBidIds.Count; i++)
            {
                if (i == 0)
                    sql += SelectionPendingBidIds[i].ToString();
                else
                    sql += "," + SelectionPendingBidIds[i].ToString();
            }

            sql += ");";
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllBidsForQuotationApproval(int PrId, List<int> TabulationIds, DBConnection dbConnection)
        {


            string sql = "SELECT * FROM BIDDING AS BI\n" +
            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
            "WHERE BI.PR_ID=" + PrId + " AND BI.IS_APPROVED=1 AND BI.END_DATE < '" +  LocalTime.Now + "' AND IS_QUOTATION_SELECTED = 1 AND BI.BID_ID IN (\n" +
            "SELECT BID_ID FROM TABULATION_MASTER WHERE TABULATION_ID IN(";

            for (int i = 0; i < TabulationIds.Count; i++)
            {
                if (i == 0)
                    sql += TabulationIds[i].ToString();
                else
                    sql += "," + TabulationIds[i].ToString();
            }

            sql += ") GROUP BY BID_ID);";
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllBidsForQuotationApprovalRej(int PrId,  DBConnection dbConnection) {


            string sql = "SELECT * FROM BIDDING AS BI\n" +
            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
            "WHERE BI.PR_ID=" + PrId + " AND BI.IS_APPROVED=1 AND BI.END_DATE < '" + LocalTime.Now + "' AND IS_QUOTATION_SELECTED = 1 ";
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllBidsForQuotationConfirmation(int PrId, List<int> TabulationIds, DBConnection dbConnection)
        {


            string sql = "SELECT * FROM BIDDING AS BI\n" +
            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
            "WHERE BI.PR_ID=" + PrId + " AND BI.IS_APPROVED=1 AND BI.END_DATE < '" +  LocalTime.Now + "' AND IS_QUOTATION_SELECTED = 1 AND IS_QUOTATION_APPROVED =1 AND BI.BID_ID IN (\n" +
            "SELECT BID_ID FROM TABULATION_MASTER WHERE TABULATION_ID IN(";

            for (int i = 0; i < TabulationIds.Count; i++)
            {
                if (i == 0)
                    sql += TabulationIds[i].ToString();
                else
                    sql += "," + TabulationIds[i].ToString();
            }

            sql += ") GROUP BY BID_ID);";
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllBidsForQuotationRejected(int PrId, List<int> TabulationIds, DBConnection dbConnection) {


            string sql = "SELECT * FROM BIDDING AS BI\n" +
            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
            "WHERE BI.PR_ID=" + PrId + " AND BI.IS_APPROVED=1 AND BI.END_DATE < '" + LocalTime.Now + "' AND IS_QUOTATION_SELECTED = 1 AND BI.BID_ID IN (\n" +
            "SELECT BID_ID FROM TABULATION_MASTER WHERE TABULATION_ID IN(";

            for (int i = 0; i < TabulationIds.Count; i++) {
                if (i == 0)
                    sql += TabulationIds[i].ToString();
                else
                    sql += "," + TabulationIds[i].ToString();
            }

            sql += ") GROUP BY BID_ID);";
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllBidsForQuotationConfirmationRej(int PrId,  DBConnection dbConnection) {

            string sql = "SELECT * FROM BIDDING AS BI\n" +
            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
            "WHERE BI.PR_ID=" + PrId + " AND BI.IS_APPROVED=1 AND BI.END_DATE < '" + LocalTime.Now + "' AND IS_QUOTATION_SELECTED = 1 ";
            dbConnection.cmd.Parameters.Clear();

            //string sql = "SELECT * FROM BIDDING AS BI\n" +
            //"INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
            //"WHERE BI.PR_ID=" + PrId + " AND BI.IS_APPROVED=1 AND BI.END_DATE < '" + LocalTime.Now + "' AND IS_QUOTATION_SELECTED = 1 AND IS_QUOTATION_APPROVED =1 ";
            //dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetBidsForQuotationSubmission(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING AS BI\n" +
                                            "LEFT JOIN (SELECT PURCHASE_TYPE, PR_ID FROM PR_MASTER) AS PM ON PM.PR_ID = BI.PR_ID "+
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "WHERE BI.IS_APPROVED=1\n" +
                                            "AND BI.IS_QUOTATION_SELECTED = 0\n" +
                                            "AND BI.START_DATE <= '" + LocalTime.Now + "'\n" +
                                            //"AND BI.END_DATE > '" + LocalTime.Now + "'\n" +
                                            "AND IS_TABULATION_APPROVAL = 0 "+
                                            "AND BI.PR_ID = " + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public Bidding GetBidDetailsForQuotationSubmission(int BidId, int CompanyId, DBConnection dbConnection)
        {

            Bidding bid;
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING AS BI\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "WHERE BI.BID_ID =" + BidId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                bid = dataAccessObject.GetSingleOject<Bidding>(dbConnection.dr);
            }

            if (bid != null)
            {
                bid.BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(BidId, CompanyId, dbConnection);

                for (int i = 0; i < bid.BiddingItems.Count; i++)
                {
                    bid.BiddingItems[i].PrDetail = DAOFactory.CreatePR_DetailDAO().GetPrDetails(bid.BiddingItems[i].PrdId, CompanyId, dbConnection);

                    //getting BOMS of PR Detail
                    bid.BiddingItems[i].PrDetail.PrBoms = DAOFactory.CreatePrBomDAOV2().GetPrdBomForEdit(bid.BiddingItems[i].PrDetail.PrdId, dbConnection);
                    //getting Replacement Images of PR Detail
                    bid.BiddingItems[i].PrDetail.PrReplacementFileUploads = DAOFactory.CreatePrReplacementFileUploadDAOV2().GetPrReplacementFileUploadForEdit(bid.BiddingItems[i].PrDetail.PrdId, dbConnection);
                    //getting Standard Images of PR Detail
                    bid.BiddingItems[i].PrDetail.PrFileUploads = DAOFactory.CreatePrFileUploadDAOV2().GetPrFileUploadForEdit(bid.BiddingItems[i].PrDetail.PrdId, dbConnection);
                    //getting Supportive Docs of PR Detail
                    bid.BiddingItems[i].PrDetail.PrSupportiveDocuments = DAOFactory.CreatePrSupportiveDocumentsDAOV2().GetPrSupportiveDocumentsForEdit(bid.BiddingItems[i].PrDetail.PrdId, dbConnection);

                }

                //getting quotations submitted for bids in Pr master
                bid.SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(bid.BidId, dbConnection);

                for (int c = 0; c < bid.SupplierQuotations.Count; c++)
                {
                    //getting supplier details for quotations
                    bid.SupplierQuotations[c].SupplierDetails = DAOFactory.createSupplierDAO().GetSupplierBySupplierId(bid.SupplierQuotations[c].SupplierId, dbConnection);

                    //getting quotation items for quotations
                    bid.SupplierQuotations[c].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(bid.SupplierQuotations[c].QuotationId, CompanyId, dbConnection);
                    if (bid.SupplierQuotations[c].QuotationItems.Where(x => x.IsQuotationItemApproved == 2).ToList().Count > 0)
                    {
                        bid.SupplierQuotations[c].IsQuotationTabulationApproved = 2;
                    }

                    bid.SupplierQuotations[c].QuotationImages = DAOFactory.createQuotationImageDAO().GetQuotationImages(bid.SupplierQuotations[c].QuotationId, dbConnection);
                    bid.SupplierQuotations[c].UploadedFiles = DAOFactory.CreateSupplierBiddingFileUploadDAO().GetUploadedFiles(bid.SupplierQuotations[c].QuotationId, dbConnection);


                    //getting boms, images and files uploaded for quotation items
                    for (int d = 0; d < bid.SupplierQuotations[c].QuotationItems.Count; d++)
                    {
                        bid.SupplierQuotations[c].QuotationItems[d].SupplierBOMs = DAOFactory.CreateSupplierBOMDAO().GetSupplierBom(bid.SupplierQuotations[c].QuotationItems[d].QuotationItemId, dbConnection);
                    }
                }
            }

            return bid;
        }

        public Bidding GetBidDetailsForQuotationSubmissionImports(int BidId, int CompanyId, DBConnection dbConnection) {

            Bidding bid;
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING AS BI\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "WHERE BI.BID_ID =" + BidId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                bid = dataAccessObject.GetSingleOject<Bidding>(dbConnection.dr);
            }

            if (bid != null) {
                bid.BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(BidId, CompanyId, dbConnection);

                for (int i = 0; i < bid.BiddingItems.Count; i++) {
                    bid.BiddingItems[i].PrDetail = DAOFactory.CreatePR_DetailDAO().GetPrDetails(bid.BiddingItems[i].PrdId, CompanyId, dbConnection);

                    //getting BOMS of PR Detail
                    bid.BiddingItems[i].PrDetail.PrBoms = DAOFactory.CreatePrBomDAOV2().GetPrdBomForEdit(bid.BiddingItems[i].PrDetail.PrdId, dbConnection);
                    //getting Replacement Images of PR Detail
                    bid.BiddingItems[i].PrDetail.PrReplacementFileUploads = DAOFactory.CreatePrReplacementFileUploadDAOV2().GetPrReplacementFileUploadForEdit(bid.BiddingItems[i].PrDetail.PrdId, dbConnection);
                    //getting Standard Images of PR Detail
                    bid.BiddingItems[i].PrDetail.PrFileUploads = DAOFactory.CreatePrFileUploadDAOV2().GetPrFileUploadForEdit(bid.BiddingItems[i].PrDetail.PrdId, dbConnection);
                    //getting Supportive Docs of PR Detail
                    bid.BiddingItems[i].PrDetail.PrSupportiveDocuments = DAOFactory.CreatePrSupportiveDocumentsDAOV2().GetPrSupportiveDocumentsForEdit(bid.BiddingItems[i].PrDetail.PrdId, dbConnection);

                }

                //getting quotations submitted for bids in Pr master
                bid.SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotationsImports(bid.BidId, dbConnection);

                for (int c = 0; c < bid.SupplierQuotations.Count; c++) {
                    //getting supplier details for quotations
                    bid.SupplierQuotations[c].SupplierDetails = DAOFactory.createSupplierDAO().GetSupplierBySupplierId(bid.SupplierQuotations[c].SupplierId, dbConnection);

                    //getting quotation items for quotations
                    bid.SupplierQuotations[c].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationImportItems(bid.SupplierQuotations[c].QuotationId, CompanyId, dbConnection);
                    if (bid.SupplierQuotations[c].QuotationItems.Where(x => x.IsQuotationItemApproved == 2).ToList().Count > 0) {
                        bid.SupplierQuotations[c].IsQuotationTabulationApproved = 2;
                    }

                    bid.SupplierQuotations[c].QuotationImages = DAOFactory.createQuotationImageDAO().GetQuotationImages(bid.SupplierQuotations[c].QuotationId, dbConnection);
                    bid.SupplierQuotations[c].UploadedFiles = DAOFactory.CreateSupplierBiddingFileUploadDAO().GetUploadedFiles(bid.SupplierQuotations[c].QuotationId, dbConnection);


                    //getting boms, images and files uploaded for quotation items
                    for (int d = 0; d < bid.SupplierQuotations[c].QuotationItems.Count; d++) {
                        bid.SupplierQuotations[c].QuotationItems[d].SupplierBOMs = DAOFactory.CreateSupplierBOMDAO().GetSupplierBom(bid.SupplierQuotations[c].QuotationItems[d].QuotationItemId, dbConnection);
                    }
                }
            }

            return bid;
        }

        public int ApproveOrRejectSelectedQuotation(int BidId, int Status, string Remarks, int UserId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            string sql = "UPDATE BIDDING SET IS_QUOTATION_APPROVED = " + Status + ", QUOTATION_APPROVAL_REMARKS='" + Remarks + "',QUOTATION_APPROVED_BY=" + UserId + ",QUOTATION_APPROVAL_DATE='" +  LocalTime.Now + "' WHERE BID_ID=" + BidId + "; ";

            if (Status == 1)
            {
                sql += "UPDATE PR_DETAIL SET CURRENT_STATUS= 7 WHERE PRD_ID IN(SELECT PRD_ID FROM BIDDING_ITEM WHERE BID_ID=" + BidId + "); ";
                sql += "INSERT INTO PR_DETAIL_STATUS_LOG SELECT PRD_ID,9,'" +  LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BID_ID=" + BidId + "; ";
            }
            else
            {
                sql += "UPDATE PR_DETAIL SET CURRENT_STATUS= 4 WHERE PRD_ID IN(SELECT PRD_ID FROM BIDDING_ITEM WHERE BID_ID=" + BidId + "); ";
                sql += "INSERT INTO PR_DETAIL_STATUS_LOG SELECT PRD_ID,10,'" +  LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BID_ID=" + BidId + "; ";
            }

            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int ConfirmOrRejectSelectedQuotation(int BidId, int Status, string Remarks, int UserId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            string sql = "UPDATE BIDDING SET IS_QUOTATION_CONFIRMED = " + Status + ", QUOTATION_CONFIRMATION_REMARKS='" + Remarks + "',QUOTATION_CONFIRMED_BY=" + UserId + ",QUOTATION_CONFIRMATION_DATE='" +  LocalTime.Now + "' WHERE BID_ID=" + BidId + ";";

            if (Status == 1)
            {
                sql += "UPDATE PR_DETAIL SET CURRENT_STATUS= 7 WHERE PRD_ID IN(SELECT PRD_ID FROM BIDDING_ITEM WHERE BID_ID=" + BidId + "); ";
                sql += "INSERT INTO PR_DETAIL_STATUS_LOG SELECT PRD_ID,9,'" +  LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BID_ID=" + BidId + "; ";
            }
            else
            {
                sql += "UPDATE PR_DETAIL SET CURRENT_STATUS= 4 WHERE PRD_ID IN(SELECT PRD_ID FROM BIDDING_ITEM WHERE BID_ID=" + BidId + "); ";
                sql += "INSERT INTO PR_DETAIL_STATUS_LOG SELECT PRD_ID,10,'" +  LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BID_ID=" + BidId + "; ";
            }
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Bidding> GetAllBidsForPoCreation(int PrId, int UserId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING AS BI\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "WHERE BI.PR_ID=" + PrId + " AND BI.IS_APPROVED=1 AND BI.END_DATE < '" +  LocalTime.Now + "' AND BID_ID IN (" +
                                            "SELECT BID_ID FROM TABULATION_MASTER WHERE IS_SELECTED = 1 AND\n" +
                                            "IS_APPROVED = 1 AND TABULATION_ID IN(\n" +
                                            "SELECT TABULATION_ID FROM TABULATION_DETAIL WHERE IS_SELECTED=1 AND IS_ADDED_TO_PO = 0 AND IS_TERMINATED = 0 GROUP BY TABULATION_ID)\n" +
                                            "GROUP BY BID_ID)";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetInProgressBids(int CompanyId, DBConnection dbConnection)
        {
            List<Bidding> bids;
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT BI.*,CL.USER_ID,CL.CREATED_USER_NAME,ISNULL(SQ_PARTICIPATED.PARTICIPATED,0) AS PARTICIPATED,\n" +
                                            "ISNULL(SQ_SUBMITTED.SUBMITTED_QUOTATIONS, 0) AS SUBMITTED_QUOTATIONS,\n" +
                                            "ISNULL(SQ_PENDING.PENDING_QUOTATIONS, 0) AS PENDING_QUOTATIONS, PRM.PR_CODE FROM BIDDING AS BI\n" +
                                            "INNER JOIN (SELECT PR_ID, PR_CODE FROM PR_MASTER) AS PRM ON PRM.PR_ID = BI.PR_ID\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PARTICIPATED FROM SUPPLIER_QUOTATION GROUP BY BID_ID) \n" +
                                            "AS SQ_PARTICIPATED ON BI.BID_ID = SQ_PARTICIPATED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS SUBMITTED_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 0 GROUP BY BID_ID) AS SQ_SUBMITTED ON BI.BID_ID = SQ_SUBMITTED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PENDING_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 1 GROUP BY BID_ID) AS SQ_PENDING ON BI.BID_ID = SQ_PENDING.BID_ID\n" +
                                            "WHERE BI.END_DATE > '" +  LocalTime.Now + "' AND BI.COMPANY_ID = " + CompanyId + " AND BI.IS_APPROVED=1 AND BI.BID_OPEN_TYPE !=2 AND BI.IS_TERMINATED !=1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                bids = dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }

            for (int i = 0; i < bids.Count; i++)
            {
                //getting bid items for bids in Pr Master
                bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(bids[i].BidId, CompanyId, dbConnection);
            }

            return bids;
        }

        public List<Bidding> GetClosedBids(int CompanyId, DBConnection dbConnection)
        {
            List<Bidding> bids;
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT BI.*,CL.USER_ID,CL.CREATED_USER_NAME,ISNULL(SQ_PARTICIPATED.PARTICIPATED,0) AS PARTICIPATED,\n" +
                                            "ISNULL(SQ_SUBMITTED.SUBMITTED_QUOTATIONS, 0) AS SUBMITTED_QUOTATIONS,\n" +
                                            "ISNULL(SQ_PENDING.PENDING_QUOTATIONS, 0) AS PENDING_QUOTATIONS FROM BIDDING AS BI\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PARTICIPATED FROM SUPPLIER_QUOTATION GROUP BY BID_ID) \n" +
                                            "AS SQ_PARTICIPATED ON BI.BID_ID = SQ_PARTICIPATED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS SUBMITTED_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 0 GROUP BY BID_ID) AS SQ_SUBMITTED ON BI.BID_ID = SQ_SUBMITTED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PENDING_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 1 GROUP BY BID_ID) AS SQ_PENDING ON BI.BID_ID = SQ_PENDING.BID_ID\n" +
                                            "WHERE BI.END_DATE < '" +  LocalTime.Now + "' AND BI.COMPANY_ID = " + CompanyId + " AND BI.IS_APPROVED=1 AND BI.BID_OPEN_TYPE !=2";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                bids = dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }

            for (int i = 0; i < bids.Count; i++)
            {
                //getting bid items for bids in Pr Master
                bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(bids[i].BidId, CompanyId, dbConnection);
            }

            return bids;
        }
       
        public List<Bidding> GetManualInProgressBids(int CompanyId,int userId, DBConnection dbConnection)
        {
            List<Bidding> bids;
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT BI.*,CL.USER_ID,CL.CREATED_USER_NAME,ISNULL(SQ_PARTICIPATED.PARTICIPATED,0) AS PARTICIPATED,\n" +
                                            "ISNULL(SQ_SUBMITTED.SUBMITTED_QUOTATIONS, 0) AS SUBMITTED_QUOTATIONS,\n" +
                                            "ISNULL(SQ_PENDING.PENDING_QUOTATIONS, 0) AS PENDING_QUOTATIONS, PRM.PR_CODE, PRM.PURCHASE_TYPE, IS_TABULATION_APPROVAL FROM BIDDING AS BI\n" +
                                            "LEFT JOIN (SELECT PR_ID, PR_CODE, PURCHASE_TYPE FROM PR_MASTER) AS PRM ON PRM.PR_ID = BI.PR_ID " +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PARTICIPATED FROM SUPPLIER_QUOTATION GROUP BY BID_ID) \n" +
                                            "AS SQ_PARTICIPATED ON BI.BID_ID = SQ_PARTICIPATED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS SUBMITTED_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 0 GROUP BY BID_ID) AS SQ_SUBMITTED ON BI.BID_ID = SQ_SUBMITTED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PENDING_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 1 GROUP BY BID_ID) AS SQ_PENDING ON BI.BID_ID = SQ_PENDING.BID_ID\n" +
                                            "WHERE " +
                                            "BI.END_DATE > '" +  LocalTime.Now.AddDays(-7) + "' AND " +
                                            "BI.IS_TABULATION_APPROVAL = 0 " +
                                            " AND BI.COMPANY_ID = " + CompanyId + " AND BI.IS_APPROVED=1 AND BI.BID_OPEN_TYPE !=1 AND BI.IS_TERMINATED !=1 AND PURCHASING_OFFICER = " + userId+"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                bids = dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }

            for (int i = 0; i < bids.Count; i++)
            {
                //getting bid items for bids in Pr Master
                bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(bids[i].BidId, CompanyId, dbConnection);
            }

            return bids;
        }

        public List<Bidding> GetManualInProgressBidsWithItem(int CompanyId, int userId, DBConnection dbConnection)
        {
            List<Bidding> bids;
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT PRM.PR_CODE AS ITEM_NAME, BI.*,CL.USER_ID,CL.CREATED_USER_NAME,ISNULL(SQ_PARTICIPATED.PARTICIPATED,0) AS PARTICIPATED,\n" +
                                            "ISNULL(SQ_SUBMITTED.SUBMITTED_QUOTATIONS, 0) AS SUBMITTED_QUOTATIONS,\n" +
                                            "ISNULL(SQ_PENDING.PENDING_QUOTATIONS, 0) AS PENDING_QUOTATIONS, PRM.PR_CODE, PRM.PURCHASE_TYPE, IS_TABULATION_APPROVAL FROM BIDDING AS BI\n" +
                                            "LEFT JOIN (SELECT PR_ID, PR_CODE, PURCHASE_TYPE FROM PR_MASTER) AS PRM ON PRM.PR_ID = BI.PR_ID " +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PARTICIPATED FROM SUPPLIER_QUOTATION GROUP BY BID_ID) \n" +
                                            "AS SQ_PARTICIPATED ON BI.BID_ID = SQ_PARTICIPATED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS SUBMITTED_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 0 GROUP BY BID_ID) AS SQ_SUBMITTED ON BI.BID_ID = SQ_SUBMITTED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PENDING_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 1 GROUP BY BID_ID) AS SQ_PENDING ON BI.BID_ID = SQ_PENDING.BID_ID\n" +
                                            "WHERE " +
                                            //"BI.END_DATE > '" +  LocalTime.Now + "' " +
                                            "BI.IS_TABULATION_APPROVAL = 0 " +
                                            " AND BI.COMPANY_ID = " + CompanyId + " AND BI.IS_APPROVED=1 AND BI.BID_OPEN_TYPE !=1 AND BI.IS_TERMINATED !=1 AND PURCHASING_OFFICER = " + userId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                bids = dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }

            for (int i = 0; i < bids.Count; i++)
            {
                //getting bid items for bids in Pr Master
                bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(bids[i].BidId, CompanyId, dbConnection);
            }

            for (int i = 0; i < bids.Count; i++)
            {
                bids[i].ItemName = "";
                bids[i].ItemName = string.Join(", ",
                    DAOFactory.CreatePR_DetailDAO().GetItemsByPrID(bids[i].PrId, dbConnection).Select(x => x.ItemName));
            }

            return bids;
        }

        public List<Bidding> GetManualClosedBids(int CompanyId, DBConnection dbConnection)
        {
            List<Bidding> bids;
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT BI.*,CL.USER_ID,CL.CREATED_USER_NAME,ISNULL(SQ_PARTICIPATED.PARTICIPATED,0) AS PARTICIPATED,\n" +
                                            "ISNULL(SQ_SUBMITTED.SUBMITTED_QUOTATIONS, 0) AS SUBMITTED_QUOTATIONS,\n" +
                                            "ISNULL(SQ_PENDING.PENDING_QUOTATIONS, 0) AS PENDING_QUOTATIONS, UN.MEASUREMENT_SHORT_NAME, PRM.PR_CODE FROM BIDDING AS BI\n" +
                                            "INNER JOIN (SELECT PR_ID, PR_CODE FROM PR_MASTER) AS PRM ON BI.PR_ID = PRM.PR_ID\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PARTICIPATED FROM SUPPLIER_QUOTATION GROUP BY BID_ID) \n" +
                                            "AS SQ_PARTICIPATED ON BI.BID_ID = SQ_PARTICIPATED.BID_ID\n" +
                                            "LEFT JOIN(SELECT BID_ID, PRD_ID FROM  BIDDING_ITEM) AS Q ON Q.BID_ID = BI.BID_ID\n" +
                                            "LEFT JOIN(SELECT PRD_ID, MEASUREMENT_ID FROM PR_DETAIL) AS PRD ON PRD.PRD_ID = Q.PRD_ID\n" +
                                            "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.MEASUREMENT_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS SUBMITTED_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 0 GROUP BY BID_ID) AS SQ_SUBMITTED ON BI.BID_ID = SQ_SUBMITTED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PENDING_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 1 GROUP BY BID_ID) AS SQ_PENDING ON BI.BID_ID = SQ_PENDING.BID_ID\n" +
                                            "WHERE BI.END_DATE < '" +  LocalTime.Now + "' AND BI.COMPANY_ID = " + CompanyId + " AND BI.IS_APPROVED=1 AND BI.BID_OPEN_TYPE !=1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                bids = dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }

            for (int i = 0; i < bids.Count; i++)
            {
                //getting bid items for bids in Pr Master
                bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(bids[i].BidId, CompanyId, dbConnection);
            }

            return bids;
        }

        public int ExpireBid(int BidId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            string sql = "UPDATE BIDDING SET END_DATE= '" +  LocalTime.Now + "'  WHERE BID_ID=" + BidId + ";";
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public Bid_Bond_Details GetBidBondDetailByBidId(int bidId, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".BID_BOND_DETAILS" +
                                           " WHERE BOND_TYPE_ID = 1 AND BID_ID = "+ bidId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Bid_Bond_Details>(dbConnection.dr);
            }
        }

        //Terminate button function by Pasindu-2020/04/25
        public int TerminateBid(int BidId, int UserId, string Remarks, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE BIDDING SET IS_TERMINATED=1,TERMINATED_BY=" + UserId + ",TERMINATED_DATE= '" + LocalTime.Now + "',TERMINATION_REMARKS ='" + Remarks.ProcessString() + "' WHERE BID_ID=" + BidId + "; ");

            sql.AppendLine("UPDATE BIDDING_ITEM SET IS_TERMINATED = 1, TERMINATED_BY=" + UserId + ",TERMINATED_DATE= '" + LocalTime.Now + "',TERMINATION_REMARKS ='" + Remarks.ProcessString() + "' WHERE BID_ID=" + BidId + ";");
            //Updating PR Detail Current Status
            sql.AppendLine("UPDATE PR_DETAIL SET CURRENT_STATUS= 12 WHERE PRD_ID IN(");
            sql.AppendLine("SELECT PRD_ID FROM BIDDING_ITEM WHERE BID_ID =" + BidId + ");");

            //Inserting PR_DETAIL Status Update log
            sql.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG");
            sql.AppendLine("SELECT PRD_ID,17,'" + LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BID_ID=" + BidId + ";");

            dbConnection.cmd.CommandText = sql.ToString();
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Bidding> GetAllBidsForPRInquiry(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING AS BI\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "WHERE BI.PR_ID=" + PrId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<int> GetBidCountForDashboard(int CompanyId,int yearsearch, int purchaseType, DBConnection dbConnection)
        {
            List<int> bidCount = new List<int>();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(BID_ID) FROM BIDDING "+
                                        " INNER JOIN PR_MASTER on BIDDING.PR_ID=PR_MASTER.PR_ID "+
                                        " WHERE PR_MASTER.COMPANY_ID=" + CompanyId+ "  AND PR_MASTER.PURCHASE_TYPE="+ purchaseType + " "+
                                        " AND (DATEPART(yyyy, CREATED_DATE)=" + yearsearch + ")";

            bidCount.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(PR_ID) FROM PR_MASTER WHERE PR_MASTER.PURCHASE_TYPE=" + purchaseType + " AND  (DATEPART(yyyy, CREATED_DATETIME)=" + yearsearch + ") AND PR_ID IN(" +
                                            "SELECT PR_ID FROM PR_DETAIL WHERE SUBMIT_FOR_BID = 0 GROUP BY PR_ID)AND COMPANY_ID = " + CompanyId;

            bidCount.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(BID_ID) FROM BIDDING INNER JOIN PR_MASTER on BIDDING.PR_ID=PR_MASTER.PR_ID WHERE  (DATEPART(yyyy, CREATED_DATE)=" + yearsearch + ")  AND PR_MASTER.PURCHASE_TYPE=" + purchaseType + " AND IS_APPROVED=0 AND PR_MASTER.COMPANY_ID=" + CompanyId;

            bidCount.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(BID_ID) FROM BIDDING INNER JOIN PR_MASTER on BIDDING.PR_ID=PR_MASTER.PR_ID WHERE (DATEPART(yyyy, CREATED_DATE)=" + yearsearch + ")  AND PR_MASTER.PURCHASE_TYPE=" + purchaseType + " AND IS_APPROVED=1 AND END_DATE>'" +  LocalTime.Now + "' AND PR_MASTER.COMPANY_ID=" + CompanyId;


            bidCount.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(BID_ID) FROM BIDDING INNER JOIN PR_MASTER on BIDDING.PR_ID=PR_MASTER.PR_ID WHERE (DATEPART(yyyy, CREATED_DATE)=" + yearsearch + ")  AND PR_MASTER.PURCHASE_TYPE=" + purchaseType + " AND IS_APPROVED=1 AND END_DATE<'" +  LocalTime.Now+ "' AND PR_MASTER.COMPANY_ID=" + CompanyId;

            bidCount.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));


            return bidCount;
        }

        public List<int> GetSelectionPendingBidIds(int UserId, int DesignationId, int CompanyId, DBConnection dbConnection)
        {
            List<int> ids = new List<int>();

            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "[GET_SELECTION_PENDING_BIDS]";
            dbConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_ID", UserId);
            dbConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_DESIGNATION", DesignationId);
            dbConnection.cmd.Parameters.AddWithValue("@COMPANY_ID", CompanyId);

            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        ids.Add(int.Parse(dbConnection.dr[0].ToString()));
                    }
                }
            }

            return ids;
        }

        public List<int> GetSelectionPendingBidIdsForTabulationApproval(int UserId, int DesignationId, int CompanyId, DBConnection dbConnection) {
            List<int> ids = new List<int>();

            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "[GET_SELECTION_PENDING_BIDS_FOR_TABULATION_REVIEW]";
            dbConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_ID", UserId);
            dbConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_DESIGNATION", DesignationId);
            dbConnection.cmd.Parameters.AddWithValue("@COMPANY_ID", CompanyId);

            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                if (dbConnection.dr.HasRows) {
                    while (dbConnection.dr.Read()) {
                        ids.Add(int.Parse(dbConnection.dr[0].ToString()));
                    }
                }
            }

            return ids;
        }

        public List<int> GetSelectionRejectedBidIds(int UserId, int DesignationId, int CompanyId, DBConnection dbConnection) {
            List<int> ids = new List<int>();

            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "[GET_REJECTED_PENDING_BIDS]";
            dbConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_ID", UserId);
            dbConnection.cmd.Parameters.AddWithValue("@LOGGED_USER_DESIGNATION", DesignationId);
            dbConnection.cmd.Parameters.AddWithValue("@COMPANY_ID", CompanyId);

            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                if (dbConnection.dr.HasRows) {
                    while (dbConnection.dr.Read()) {
                        ids.Add(int.Parse(dbConnection.dr[0].ToString()));
                    }
                }
            }

            return ids;
        }
        public int ReOpenBid(int BidId, int UserId, DateTime date, string remark,  DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE BIDDING SET END_DATE='" + date + "', REOPEN_REMARKS = '"+ remark + "', IS_TABULATION_APPROVAL = 0, TABULATION_REVIEW_APPROVAL_REMARK = '' WHERE BID_ID=" + BidId + "; ");

            
            ////Updating PR Detail Current Status
            //sql.AppendLine("UPDATE PR_DETAIL SET CURRENT_STATUS= 6 WHERE PRD_ID IN(");
            //sql.AppendLine("SELECT PRD_ID FROM BIDDING_ITEM WHERE BID_ID =" + BidId + ");");

            ////Inserting PR_DETAIL Status Update log
            //sql.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG");
            //sql.AppendLine("SELECT PRD_ID,20,'" + LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BID_ID=" + BidId + ";");

            dbConnection.cmd.CommandText = sql.ToString();
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int ResetSelections(int BidId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();
            string sql = "UPDATE BIDDING SET IS_QUOTATION_SELECTED = 0,IS_QUOTATION_APPROVED = 0, QUOTATION_APPROVAL_REMARKS = NULL,IS_QUOTATION_CONFIRMED = 0, QUOTATION_CONFIRMATION_REMARKS = NULL WHERE BID_ID = " + BidId + "; ";
          //  sql += "UPDATE BIDDING_ITEM SET IS_TERMINATED = 0, TERMINATED_BY= NULL,TERMINATED_DATE=NULL,TERMINATION_REMARKS=NULL,IS_QUOTATION_SELECTED=0,QUOTATION_SELECTED_BY=NULL,QUOTATION_SELECTION_DATE=NULL,SELECTION_REMARKS=NULL WHERE BID_ID= " + BidId + "; ";
            sql += "UPDATE SUPPLIER_QUOTATION SET IS_SELECTED = 0, SELECTION_REMARKS=NULL WHERE BID_ID = " + BidId + "; ";
           // sql += "UPDATE SUPPLIER_QUOTATION_ITEM SET IS_SELECTED = 0, QUOTATION_SELECTED_BY=NULL,QUOTATION_SELECTION_DATE=NULL,SELECTION_REMARKS=NULL WHERE QUOTATION_ID IN (SELECT QUOTATION_ID FROM SUPPLIER_QUOTATION WHERE BID_ID= " + BidId + "); ";
            sql += "UPDATE TABULATION_MASTER SET IS_CURRENT = 0 WHERE BID_ID= " + BidId + "; ";
            dBConnection.cmd.CommandText = sql;
            return dBConnection.cmd.ExecuteNonQuery();
        }
        public Bidding GetBiddingDetailsByBiddingId(int biddingId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".BIDDING AS a" +
                                           " WHERE a.BID_ID = " + biddingId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Bidding>(dbConnection.dr);
            }
        }
        public List<Bidding> FetchBidInfo(int PrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING AS BID " +
                                            "INNER JOIN (SELECT USER_ID, USER_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = BID.CREATED_USER " +
                                            "LEFT JOIN ( SELECT USER_ID, USER_NAME AS QUOTATION_SELECTED_BY_NAME FROM COMPANY_LOGIN ) AS CLU ON CLU.USER_ID = BID.QUOTATION_SELECTED_BY " +
                                            " WHERE PR_ID = " + PrId + "";
               
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> FetchBidInfoForPRRequisitionReport(List<int> PrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING AS BID " +
                                            "INNER JOIN (SELECT USER_ID, USER_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = BID.CREATED_USER " +
                                            "LEFT JOIN ( SELECT USER_ID, USER_NAME AS QUOTATION_SELECTED_BY_NAME FROM COMPANY_LOGIN ) AS CLU ON CLU.USER_ID = BID.QUOTATION_SELECTED_BY " +
                                            " WHERE PR_ID IN (" +String.Join(",", PrId) +")";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetRejectedBids(int prId,int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING  " +
                                            "WHERE PR_ID = " + prId + " AND IS_TABULATION_APPROVAL = 2 AND COMPANY_ID=" + companyId+"  ;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetAllBidsForQuotationComparison(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING AS BI\n" +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS QUOTATION_SELECTED_BY_NAME FROM COMPANY_LOGIN) AS CLS ON BI.QUOTATION_SELECTED_BY = CLS.USER_ID\n" +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS QUOTATION_APPROVED_BY_NAME  FROM COMPANY_LOGIN) AS CLA ON BI.QUOTATION_APPROVED_BY = CLA.USER_ID\n" +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS QUOTATION_CONFIRMED_BY_NAME  FROM COMPANY_LOGIN) AS CLC ON BI.QUOTATION_SELECTED_BY = CLC.USER_ID\n" +
                                            "WHERE BI.PR_ID=" + PrId + " AND BI.IS_APPROVED=1 AND BI.END_DATE < '" + LocalTime.Now + "' AND BI.IS_TERMINATED !=1 AND ((IS_QUOTATION_SELECTED = 0) OR (IS_QUOTATION_SELECTED = 1 AND IS_QUOTATION_APPROVED =2) OR (IS_QUOTATION_SELECTED = 1 AND IS_QUOTATION_APPROVED =1 AND IS_QUOTATION_CONFIRMED =2));";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }
        }

        public List<BiddingItem> GetPrdIdByBidId(int BidId, DBConnection dbConnection) {
           

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING_ITEM WHERE BID_ID = "+ BidId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<BiddingItem>(dbConnection.dr);
            }

           
        }

        public Bidding GetBidDetailsByBidId(int BidId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM BIDDING " +
                                           " WHERE BID_ID = " + BidId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Bidding>(dbConnection.dr);
            }
        }

        public List<Bidding> GetManualClosedBidsByBidCode(int CompanyId, int BidCode, DBConnection dbConnection) {
            List<Bidding> bids;
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT BI.*,CL.USER_ID,CL.CREATED_USER_NAME,ISNULL(SQ_PARTICIPATED.PARTICIPATED,0) AS PARTICIPATED,\n" +
                                            "ISNULL(SQ_SUBMITTED.SUBMITTED_QUOTATIONS, 0) AS SUBMITTED_QUOTATIONS,\n" +
                                            "ISNULL(SQ_PENDING.PENDING_QUOTATIONS, 0) AS PENDING_QUOTATIONS,COUNT (DISTINCT SUP.SUPPLIER_ID) AS PARTICIPATED_SUPPLIERS, PRM.PR_CODE, PRM.PURCHASE_TYPE FROM BIDDING AS BI\n" +
                                            "LEFT JOIN SUPPLIER_QUOTATION AS SUP ON SUP.BID_ID = BI.BID_ID \n" +
                                            "INNER JOIN (SELECT PR_ID, PR_CODE, PURCHASE_TYPE FROM PR_MASTER) AS PRM ON BI.PR_ID = PRM.PR_ID\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PARTICIPATED FROM SUPPLIER_QUOTATION GROUP BY BID_ID) \n" +
                                            "AS SQ_PARTICIPATED ON BI.BID_ID = SQ_PARTICIPATED.BID_ID\n" +
                                            "LEFT JOIN(SELECT BID_ID, PRD_ID FROM  BIDDING_ITEM) AS Q ON Q.BID_ID = BI.BID_ID\n" +
                                            "LEFT JOIN(SELECT PRD_ID, MEASUREMENT_ID FROM PR_DETAIL) AS PRD ON PRD.PRD_ID = Q.PRD_ID\n" +
                                            "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.MEASUREMENT_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS SUBMITTED_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 0 GROUP BY BID_ID) AS SQ_SUBMITTED ON BI.BID_ID = SQ_SUBMITTED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PENDING_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 1 GROUP BY BID_ID) AS SQ_PENDING ON BI.BID_ID = SQ_PENDING.BID_ID\n" +
                                            "WHERE BI.END_DATE < '" + LocalTime.Now + "' AND BID_CODE = "+ BidCode + " AND BI.COMPANY_ID = " + CompanyId + " AND BI.IS_APPROVED=1 AND BI.BID_OPEN_TYPE !=1 "+
                                             "GROUP BY CL.USER_ID,CL.CREATED_USER_NAME,SQ_PARTICIPATED.PARTICIPATED,SUBMITTED_QUOTATIONS,PENDING_QUOTATIONS,\n" +
                                            " PRM.PR_CODE,PRM.PURCHASE_TYPE,BI.BID_ID ,BI.BID_CODE ,BI.PR_ID ,BI.CREATED_DATE ,BI.CREATED_USER ,BI.COMPANY_ID ,BI.START_DATE ,BI.END_DATE ,\n" +
                                            "BI.BID_TYPE ,BI.BID_OPEN_TYPE ,BI.IS_ACTIVE ,BI.IS_BID_COMPLETED ,BI.BID_OPENING_PERIOD ,BI.OPEN_BID_TO ,BI.IS_APPROVED\n" +
                                            ",BI.APPROVAL_REMARKS ,BI.BID_TERMS_AND_CONDITIONS ,\n" +
                                            "BI.IS_QUOTATION_SELECTED ,BI.IS_QUOTATION_APPROVED ,BI.QUOTATION_APPROVAL_REMARKS ,BI.IS_QUOTATION_CONFIRMED\n" +
                                            ",BI.QUOTATION_CONFIRMATION_REMARKS ,BI.IS_QUOTATION_ADDED_TO_PO ,BI.CONTAINS_REJECTED_ITEMS ,BI.BIDDING_METHOD_ID\n" +
                                             ",BI.IS_OPEN ,BI.APPROVAL_BY ,BI.PROCEED_REMARK, BI.QUOTATION_SELECTED_BY ,BI.QUOTATION_SELECTION_DATE ,BI.QUOTATION_APPROVED_BY\n" +
                                             ",BI.QUOTATION_APPROVAL_DATE ,BI.QUOTATION_CONFIRMED_BY ,BI.QUOTATION_CONFIRMATION_DATE ,BI.PURCHASING_OFFICER ,\n" +
                                              "BI.CATEGORY_ID ,BI.REOPEN_REMARKS ,BI.TERMINATION_REMARKS ,BI.IS_TERMINATED ,BI.TERMINATED_BY\n" +
                                             ",BI.TERMINATED_DATE ,BI.IS_TABULATION_APPROVAL ,BI.TABULATION_REVIEW_APPROVAL_REMARK, BI.APPROVAL_DATE, IS_REJECTED_BID_TERMINATED,IS_CLONED,BID_WAS_CLONED\n";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                bids = dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }

            for (int i = 0; i < bids.Count; i++) {
                //getting bid items for bids in Pr Master
                bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(bids[i].BidId, CompanyId, dbConnection);
            }

            return bids;
        }

        public List<Bidding> GetManualClosedBidsByPrCode(int CompanyId, string PrCode, DBConnection dbConnection) {
            List<Bidding> bids;
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT BI.*,CL.USER_ID,CL.CREATED_USER_NAME,ISNULL(SQ_PARTICIPATED.PARTICIPATED,0) AS PARTICIPATED,\n" +
                                            "ISNULL(SQ_SUBMITTED.SUBMITTED_QUOTATIONS, 0) AS SUBMITTED_QUOTATIONS,\n" +
                                            "ISNULL(SQ_PENDING.PENDING_QUOTATIONS, 0) AS PENDING_QUOTATIONS, COUNT (DISTINCT SUP.SUPPLIER_ID) AS PARTICIPATED_SUPPLIERS, PRM.PR_CODE, PRM.PURCHASE_TYPE FROM BIDDING AS BI\n" +
                                            "LEFT JOIN SUPPLIER_QUOTATION AS SUP ON SUP.BID_ID = BI.BID_ID \n"+
                                            "INNER JOIN (SELECT PR_ID, PR_CODE, PURCHASE_TYPE FROM PR_MASTER) AS PRM ON BI.PR_ID = PRM.PR_ID\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PARTICIPATED FROM SUPPLIER_QUOTATION GROUP BY BID_ID) \n" +
                                            "AS SQ_PARTICIPATED ON BI.BID_ID = SQ_PARTICIPATED.BID_ID\n" +
                                            "LEFT JOIN(SELECT BID_ID, PRD_ID FROM  BIDDING_ITEM) AS Q ON Q.BID_ID = BI.BID_ID\n" +
                                            "LEFT JOIN(SELECT PRD_ID, MEASUREMENT_ID FROM PR_DETAIL) AS PRD ON PRD.PRD_ID = Q.PRD_ID\n" +
                                            "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.MEASUREMENT_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS SUBMITTED_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 0 GROUP BY BID_ID) AS SQ_SUBMITTED ON BI.BID_ID = SQ_SUBMITTED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PENDING_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 1 GROUP BY BID_ID) AS SQ_PENDING ON BI.BID_ID = SQ_PENDING.BID_ID\n" +
                                            "WHERE BI.END_DATE < '" + LocalTime.Now + "' AND PR_CODE = '" + PrCode + "' AND BI.COMPANY_ID = " + CompanyId + " AND BI.IS_APPROVED=1 AND BI.BID_OPEN_TYPE !=1 " +
                                            "GROUP BY CL.USER_ID,CL.CREATED_USER_NAME,SQ_PARTICIPATED.PARTICIPATED,SUBMITTED_QUOTATIONS,PENDING_QUOTATIONS,\n" +
                                            " PRM.PR_CODE,PRM.PURCHASE_TYPE,BI.BID_ID ,BI.BID_CODE ,BI.PR_ID ,BI.CREATED_DATE ,BI.CREATED_USER ,BI.COMPANY_ID ,BI.START_DATE ,BI.END_DATE ,\n" +
                                            "BI.BID_TYPE ,BI.BID_OPEN_TYPE ,BI.IS_ACTIVE ,BI.IS_BID_COMPLETED ,BI.BID_OPENING_PERIOD ,BI.OPEN_BID_TO ,BI.IS_APPROVED\n" +
                                            ",BI.APPROVAL_REMARKS ,BI.BID_TERMS_AND_CONDITIONS ,\n" +
                                            "BI.IS_QUOTATION_SELECTED ,BI.IS_QUOTATION_APPROVED ,BI.QUOTATION_APPROVAL_REMARKS ,BI.IS_QUOTATION_CONFIRMED\n" +
                                            ",BI.QUOTATION_CONFIRMATION_REMARKS ,BI.IS_QUOTATION_ADDED_TO_PO ,BI.CONTAINS_REJECTED_ITEMS ,BI.BIDDING_METHOD_ID\n" +
                                             ",BI.IS_OPEN ,BI.APPROVAL_BY ,BI.QUOTATION_SELECTED_BY ,BI.QUOTATION_SELECTION_DATE ,BI.QUOTATION_APPROVED_BY\n" +
                                             ",BI.QUOTATION_APPROVAL_DATE ,BI.QUOTATION_CONFIRMED_BY ,BI.QUOTATION_CONFIRMATION_DATE ,BI.PURCHASING_OFFICER ,\n" +
                                              "BI.CATEGORY_ID ,BI.REOPEN_REMARKS ,BI.TERMINATION_REMARKS ,BI.IS_TERMINATED ,BI.TERMINATED_BY\n" +
                                             ",BI.TERMINATED_DATE ,BI.PROCEED_REMARK, BI.IS_TABULATION_APPROVAL ,BI.TABULATION_REVIEW_APPROVAL_REMARK, BI.APPROVAL_DATE, IS_REJECTED_BID_TERMINATED,IS_CLONED,BID_WAS_CLONED\n";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                bids = dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }

            for (int i = 0; i < bids.Count; i++) {
                //getting bid items for bids in Pr Master
                bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(bids[i].BidId, CompanyId, dbConnection);
            }

            return bids;
        }

        public List<Bidding> GetClosedBidsByPrCode(int CompanyId, string PrCode, DBConnection dbConnection) {
            List<Bidding> bids;
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT BI.*,CL.USER_ID,CL.CREATED_USER_NAME,ISNULL(SQ_PARTICIPATED.PARTICIPATED,0) AS PARTICIPATED,\n" +
                                            "ISNULL(SQ_SUBMITTED.SUBMITTED_QUOTATIONS, 0) AS SUBMITTED_QUOTATIONS,\n" +
                                            "ISNULL(SQ_PENDING.PENDING_QUOTATIONS, 0) AS PENDING_QUOTATIONS, PRM.PR_CODE FROM BIDDING AS BI\n" +
                                            "INNER JOIN (SELECT PR_ID, PR_CODE FROM PR_MASTER) AS PRM ON BI.PR_ID = PRM.PR_ID\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                           "LEFT JOIN(SELECT BID_ID, PRD_ID FROM  BIDDING_ITEM) AS Q ON Q.BID_ID = BI.BID_ID\n" +
                                            "LEFT JOIN(SELECT PRD_ID, MEASUREMENT_ID FROM PR_DETAIL) AS PRD ON PRD.PRD_ID = Q.PRD_ID\n" +
                                            "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.MEASUREMENT_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PARTICIPATED FROM SUPPLIER_QUOTATION GROUP BY BID_ID) \n" +
                                            "AS SQ_PARTICIPATED ON BI.BID_ID = SQ_PARTICIPATED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS SUBMITTED_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 0 GROUP BY BID_ID) AS SQ_SUBMITTED ON BI.BID_ID = SQ_SUBMITTED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PENDING_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 1 GROUP BY BID_ID) AS SQ_PENDING ON BI.BID_ID = SQ_PENDING.BID_ID\n" +
                                            "WHERE BI.END_DATE < '" + LocalTime.Now + "' AND PR_CODE = '" + PrCode + "' AND BI.COMPANY_ID = " + CompanyId + " AND BI.IS_APPROVED=1 AND BI.BID_OPEN_TYPE !=2 " +
                                            "GROUP BY CL.USER_ID,CL.CREATED_USER_NAME,SQ_PARTICIPATED.PARTICIPATED,SUBMITTED_QUOTATIONS,PENDING_QUOTATIONS,\n" +
                                            "PRM.PR_CODE,BI.BID_ID ,BI.BID_CODE ,BI.PR_ID ,BI.CREATED_DATE ,BI.CREATED_USER ,BI.COMPANY_ID ,BI.START_DATE ,BI.END_DATE ,\n" +
                                            "BI.BID_TYPE ,BI.BID_OPEN_TYPE ,BI.IS_ACTIVE ,BI.IS_BID_COMPLETED ,BI.BID_OPENING_PERIOD ,BI.OPEN_BID_TO ,BI.IS_APPROVED\n" +
                                            ",BI.APPROVAL_REMARKS ,BI.BID_TERMS_AND_CONDITIONS ,\n" +
                                            "BI.IS_QUOTATION_SELECTED ,BI.IS_QUOTATION_APPROVED ,BI.QUOTATION_APPROVAL_REMARKS ,BI.IS_QUOTATION_CONFIRMED\n" +
                                            ",BI.QUOTATION_CONFIRMATION_REMARKS ,BI.IS_QUOTATION_ADDED_TO_PO ,BI.CONTAINS_REJECTED_ITEMS ,BI.BIDDING_METHOD_ID\n" +
                                             ",BI.IS_OPEN ,BI.APPROVAL_BY ,BI.QUOTATION_SELECTED_BY ,BI.QUOTATION_SELECTION_DATE ,BI.QUOTATION_APPROVED_BY\n" +
                                             ",BI.QUOTATION_APPROVAL_DATE ,BI.QUOTATION_CONFIRMED_BY ,BI.QUOTATION_CONFIRMATION_DATE ,BI.PURCHASING_OFFICER ,\n" +
                                              "BI.CATEGORY_ID ,BI.REOPEN_REMARKS ,BI.TERMINATION_REMARKS ,BI.IS_TERMINATED ,BI.TERMINATED_BY\n" +
                                             ",BI.TERMINATED_DATE ,BI.PROCEED_REMARK,BI.IS_TABULATION_APPROVAL ,BI.TABULATION_REVIEW_APPROVAL_REMARK, BI.APPROVAL_DATE, IS_REJECTED_BID_TERMINATED,IS_CLONED,BID_WAS_CLONED\n";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                bids = dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }

            for (int i = 0; i < bids.Count; i++) {
                //getting bid items for bids in Pr Master
                bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(bids[i].BidId, CompanyId, dbConnection);
            }

            return bids;
        }
        public List<Bidding> GetClosedBidsByBidCode(int CompanyId, int BidCode, DBConnection dbConnection) {
            List<Bidding> bids;
            dbConnection.cmd.Parameters.Clear();


            dbConnection.cmd.CommandText = "SELECT BI.*,CL.USER_ID,CL.CREATED_USER_NAME,ISNULL(SQ_PARTICIPATED.PARTICIPATED,0) AS PARTICIPATED,\n" +
                                            "ISNULL(SQ_SUBMITTED.SUBMITTED_QUOTATIONS, 0) AS SUBMITTED_QUOTATIONS,\n" +
                                            "ISNULL(SQ_PENDING.PENDING_QUOTATIONS, 0) AS PENDING_QUOTATIONS,  PRM.PR_CODE FROM BIDDING AS BI\n" +
                                            "INNER JOIN (SELECT PR_ID, PR_CODE FROM PR_MASTER) AS PRM ON BI.PR_ID = PRM.PR_ID\n" +
                                            "INNER JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON BI.CREATED_USER = CL.USER_ID\n" +
                                           "LEFT JOIN(SELECT BID_ID, PRD_ID FROM  BIDDING_ITEM) AS Q ON Q.BID_ID = BI.BID_ID\n" +
                                            "LEFT JOIN(SELECT PRD_ID, MEASUREMENT_ID FROM PR_DETAIL) AS PRD ON PRD.PRD_ID = Q.PRD_ID\n" +
                                            "LEFT JOIN(SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.MEASUREMENT_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PARTICIPATED FROM SUPPLIER_QUOTATION GROUP BY BID_ID) \n" +
                                            "AS SQ_PARTICIPATED ON BI.BID_ID = SQ_PARTICIPATED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS SUBMITTED_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 0 GROUP BY BID_ID) AS SQ_SUBMITTED ON BI.BID_ID = SQ_SUBMITTED.BID_ID\n" +
                                            "LEFT JOIN (SELECT BID_ID, COUNT(QUOTATION_ID)AS PENDING_QUOTATIONS FROM SUPPLIER_QUOTATION\n" +
                                            "WHERE IS_STAYED_AS_LATER_BID = 1 GROUP BY BID_ID) AS SQ_PENDING ON BI.BID_ID = SQ_PENDING.BID_ID\n" +
                                            "WHERE BI.END_DATE < '" + LocalTime.Now + "' AND BID_CODE = " + BidCode + " AND BI.COMPANY_ID = " + CompanyId + " AND BI.IS_APPROVED=1 AND BI.BID_OPEN_TYPE !=2 " +
                                            "GROUP BY CL.USER_ID,CL.CREATED_USER_NAME,SQ_PARTICIPATED.PARTICIPATED,SUBMITTED_QUOTATIONS,PENDING_QUOTATIONS,\n" +
                                            " PRM.PR_CODE,BI.BID_ID ,BI.BID_CODE ,BI.PR_ID ,BI.CREATED_DATE ,BI.CREATED_USER ,BI.COMPANY_ID ,BI.START_DATE ,BI.END_DATE ,\n" +
                                            "BI.BID_TYPE ,BI.BID_OPEN_TYPE ,BI.IS_ACTIVE ,BI.IS_BID_COMPLETED ,BI.BID_OPENING_PERIOD ,BI.OPEN_BID_TO ,BI.IS_APPROVED\n" +
                                            ",BI.APPROVAL_REMARKS ,BI.BID_TERMS_AND_CONDITIONS ,\n" +
                                            "BI.IS_QUOTATION_SELECTED ,BI.IS_QUOTATION_APPROVED ,BI.QUOTATION_APPROVAL_REMARKS ,BI.IS_QUOTATION_CONFIRMED\n" +
                                            ",BI.QUOTATION_CONFIRMATION_REMARKS ,BI.IS_QUOTATION_ADDED_TO_PO ,BI.CONTAINS_REJECTED_ITEMS ,BI.BIDDING_METHOD_ID\n" +
                                             ",BI.IS_OPEN ,BI.APPROVAL_BY ,BI.QUOTATION_SELECTED_BY ,BI.QUOTATION_SELECTION_DATE ,BI.QUOTATION_APPROVED_BY\n" +
                                             ",BI.QUOTATION_APPROVAL_DATE ,BI.QUOTATION_CONFIRMED_BY ,BI.QUOTATION_CONFIRMATION_DATE ,BI.PURCHASING_OFFICER ,\n" +
                                              "BI.CATEGORY_ID ,BI.REOPEN_REMARKS ,BI.TERMINATION_REMARKS ,BI.IS_TERMINATED ,BI.TERMINATED_BY\n" +
                                             ",BI.TERMINATED_DATE ,BI.PROCEED_REMARK,BI.IS_TABULATION_APPROVAL ,BI.TABULATION_REVIEW_APPROVAL_REMARK, BI.APPROVAL_DATE, IS_REJECTED_BID_TERMINATED,IS_CLONED,BID_WAS_CLONED\n";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                bids = dataAccessObject.ReadCollection<Bidding>(dbConnection.dr);
            }

            for (int i = 0; i < bids.Count; i++) {
                //getting bid items for bids in Pr Master
                bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(bids[i].BidId, CompanyId, dbConnection);
            }

            return bids;
        }
        

        public int UpdateEmailStatus(List<int> BidIds, int prId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "UPDATE  BIDDING SET EMAIL_STATUS = 1 WHERE BID_ID IN ("+string.Join (",", BidIds) +") ; ";
            dbConnection.cmd.CommandText = "UPDATE  PR_MASTER SET EMAIL_STATUS = 1 WHERE PR_ID = " + prId + " "; 

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
