using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;
using System.Data;

namespace CLibrary.Infrastructure {
    public interface PR_MasterDAO {
        int SavePRMaster(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string prProcedure, string purchaseType, DateTime requiredDate, string MRNReferenceNo, int itemCatId, DBConnection dbConnection);
        string FetchPRCode(int DepartmentId, DBConnection dbConnection);
        List<PR_Master> FetchApprovePRDataByDeptId(int DepartmentId, DBConnection dbConnection);
        PR_Master FetchApprovePRDataByDeptIdAndPRId(int DepartmentId, int PrId, DBConnection dbConnection);
        int UpdateIsApprovePR(int DepartmentId, int PrId, int Status, int PRApprovedUserId, int isActive, string RejectedReason, DBConnection dbConnection);
        List<PR_Master> FetchApprovePRDataByDeptIdApproved(int DepartmentId, DBConnection dbConnection);
        int UpdateOprnForBid(int DepartmentId, int PrId, int BidOpeningStatus, string BidTermCondition, int ApprovedBy, DBConnection dbConnection);
        PR_Master FetchApprovePRDataByPRId(int PrId, DBConnection dbConnection);
        List<PR_Master> GetPrMasterListByDaterange(int departmentid, DateTime startdate, DateTime enddate, DBConnection dbConnection);
        int UpdatePORaised(int PrId, DBConnection dbConnection);
        int UpdatePRMaster(int PrId, int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string prProcedure, string purchaseType, DateTime requiredDate, string MRNReferenceNo, DBConnection dbConnection);
        List<PR_Master> FetchRejectedPr(int DepartmentId, DBConnection dbConnection);
        PR_Master FetchRejectPR(int PrId, DBConnection dbConnection);
        List<PR_Master> FetchDetailsToEdit(int DepartmentId, DBConnection dbConnection);
        List<PR_Master> FetchYetSubmitPR(int Department, DBConnection dbConnection);
        List<PR_Master> FetchTotalBidforChart(int Department, int year, DBConnection dbConnection);
        List<PR_Master> FetchTotalPR(int Department, DBConnection dbConnection);
        string SavePRMasterAtCloning(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string prProcedure, string purchaseType, DateTime requiredDate, string MRNReferenceNo, DBConnection dbConnection);

        int countTotalPr(int DepartmentId, DBConnection dbConnection);
        int countApprovedPr(int DepartmentId, DBConnection dbConnection);
        int countRejectedPr(int DepartmentId, DBConnection dbConnection);
        int countPendingPr(int DepartmentId, DBConnection dbConnection);
        List<PR_Master> countTotalPrtochart(int DepartmentId, int year, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForQuotationApproval(List<int> SelectionPendingBidIds, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForManualQuotationSubmissionWithItem(int CompanyId, int UserId, DBConnection dbConnection);

        int GetDetailsByPrCode(int DepartmentId, string PrCode, DBConnection dbConnection);

        //-----2018-09-17 PR Rports View
        List<PR_Master> FetchApprovePRDataByDeptIdReports(int DepartmentId, DBConnection dbConnection);

        List<PR_Master> FetchApprovedPRForConfirmation(int Department, DBConnection dbConnection);
        int ConfirmOrDenyPRApproval(int prId, int confirm, DBConnection dbConnection);

        //New Methods By Salman created on 2019-01-17
        List<PrMasterV2> GetPrListForBidSubmission(int CompanyId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForBidSubmissionWithItem(int CompanyId, DBConnection dbConnection);
        PrMasterV2 GetPrForBidSubmission(int PrId, int CompanyId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForBidApproval(int CompanyId, int LoggedInUser, DBConnection dbConnection);
        PrMasterV2 GetPrForBidApproval(int PrId, int CompanyId, DBConnection dbConnection);
        List<PR_Master> GetPrListForQuotationComparison(int CompanyId, DBConnection dbConnection);
        PrMasterV2 GetPrForQuotationComparison(int PrId, int CompanyId, List<int> SelectionPendingBidIds, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForQuotationApproval(int CompanyId, List<int> TabulationIds, DBConnection dbConnection);
        PrMasterV2 GetPrForQuotationApproval(int PrId, int CompanyId, List<int> TabulationIds, int UserId, int DesignationId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForQuotationConfirmation(int CompanyId, List<int> TabulationIds, DBConnection dbConnection);
        PrMasterV2 GetPrForQuotationConfirmation(int PrId, int CompanyId, List<int> TabulationIds, int UserId, int DesignationId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForManualQuotationSubmission(int CompanyId, int UserId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForPoCreation(int CompanyId, int UserId, DBConnection dbConnection);
        PrMasterV2 GetPrForPoCreation(int PrId, int CompanyId, int UserId, DBConnection dbConnection);
        string SaveMRNtoPR(int mrnId, int userId, int companyId, DBConnection dbConnection);

        PrMasterV2 getPRMasterDetailByPrId(int prID, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForViewAllPr(int CompanyId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForViewMyPr(int CompanyId, int UserId, DBConnection dbConnection);

        PrMasterV2 GetPrForBidSubmissionView(int PrId, int CompanyId, DBConnection dbConnection);
        List<PR_Master> FetchAllPR(int Department, DBConnection dbConnection);
        //List<PR_Master> GetPRListForPrInquiry(int CompanyId, DBConnection dbConnection);
        PR_Master SearchPRForInquiryByPrId(int PrId, int CompanyId, DBConnection dbConnection);
        List<PR_Master> GetPRtobeApprovedForAdvanceSearch(int companyId, int categoryId, int subDepartmentID, int serchtype, string searchkey, string usertype, DBConnection dbConnection);
        List<PR_Master> GetPRtobeApprovedforBiddingForAdvanceSearch(int companyId, int categoryId, int subDepartmentID, int serchtype, string searchkey, string usertype, DBConnection dbConnection);
        List<PR_Master> GetQuotationBidddingrAdvanceSearch(int companyId, int categoryId, int supplierId, int serchtype, string searchkey, string usertype, DBConnection dbConnection);
        List<PR_Master> GetPrListForDocumentUploadPoCreation(int CompanyId, DBConnection dbConnection);
        int CheckfileUploadedofteccommit(int bidId, int qutationId, DBConnection dbConnection);

        List<int> GetPRCountForDashboard(int CompanyId, int yearsearch, int purchaseType, DBConnection dbConnection);

        DataTable GetPRCountForDashBoard(DBConnection dbConnection);
        List<PrMasterV2> GetPrListForBidSubmited(int CompanyId, int UserId, DBConnection dbConnection);
        List<PR_Master> FetchPRToEdit(int companyId, int userId, DBConnection dbConnection);
        List<PR_Details> GetPRDetails(int prId, int companyId, DBConnection dbConnection);
        List<PR_Details> GetOnlyPRDetails(int prId, int companyId, DBConnection dbConnection);
        List<PR_Master> FetchApprovedPR(int companyId, DBConnection dbConnection);
        List<PR_Master> AdvanceSearchPRForInquiry(int companyId, int searchBy, int categoryId, int subdepartmentId, string searchText, DBConnection dbConnection);
        //  PR_Master GetPRs(int PrId, int CompanyId, DBConnection dbConnection);
        int ApproveBid(int BidId, int CompanyId, string remark, DBConnection dbConnection);
        int RejectBid(int PrId, int CompanyId, string remark, DBConnection dbConnection);
        List<PrMasterV2> GetPrRejectedQuotationTabulationSheet(List<int> selectionPendingBidIds, int companyId, int userId, DBConnection dbConnection);
        void UpdateTabulationReviewApproval(int bidId, int companyId, DBConnection dbConnection);
        string GetQuotationForbyPrCode(int companyId, string prCode, DBConnection dbConnection);
        //Reorder function stock by Pasindu 2020/04/29
        List<string> GetPrCodesByPrIds(List<int> PrIds, DBConnection dbConnection);
        int SavePRMasterV2(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string terms, int SubDepartmentId, int CategoryId, int warehouseId, DBConnection dbConnection);


        // By Adee  on24.04.2020
        PR_Master GetPrForQuotationComparison(int PrId, int CompanyId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForQuotationComparisonOld(List<int> SelectionPendingBidIds, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForQuotationComparisonBid(List<int> SelectionPendingBidIds, DBConnection dbConnection);
        PrMasterV2 GetPrForQuotationComparisonImports(int PrId, int CompanyId, List<int> SelectionPendingBidIds, DBConnection dbConnection);
        int UpdateTerminatedPRMaster(int prId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForBidSubmissionByPrCode(int CompanyId, string prCode, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForBidSubmissionByDate(int CompanyId, DateTime date, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForBidApprovalByPrCode(int CompanyId, int LoggedInUser, string PrCode, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForBidApprovalByDate(int CompanyId, int LoggedInUser, DateTime date, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForBidSubmitedByDate(int CompanyId, int UserId, DateTime date, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForBidSubmitedByPrCode(int CompanyId, int UserId, string prCode, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForRejectedBids(int CompanyId, int UserId, DBConnection dbConnection);
        List<PrMasterV2> FetchPrByDate(int companyId, DateTime ToDate, DateTime FromDate, DBConnection dbConnection);
        List<PrMasterV2> FetchPrByPrCode(int CompanyId, string prCode, DBConnection dbConnection);
        int SavePRMaster(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string terms, int SubDepartmentId, int CategoryId, int warehouseId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForQuotationComparisonReviw(int CompanyId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForQuotationComparisonReviwWithItem(int CompanyId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForQuotationRejected(int CompanyId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForQuotationAppRejected(int CompanyId, DBConnection dbConnection);
        PrMasterV2 GetPrForQuotationApprovalRej(int PrId, int CompanyId, int UserId, int DesignationId, DBConnection dbConnection);
        PrMasterV2 GetPrForQuotationConfirmationRej(int PrId, int CompanyId, int UserId, int DesignationId, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForQuotationComparisonReviwByPrCode(int CompanyId, string Code, DBConnection dbConnection);
        List<PrMasterV2> GetPrListForQuotationComparisonReviwByDate(int CompanyId, DateTime Date, DBConnection dbConnection);
        int ApproveBidTab(int BidId, int CompanyId, string remark, string ProceedRemark, DBConnection dbConnection);
        PR_Master GetPrForImportTabulationReview(int PrId, int CompanyId, DBConnection dbConnection);
        int ApproveBidTabImports(int BidId, int CompanyId, string ProceedRemark, DBConnection dbConnection);
        int GetParentPrforCoverinPr(int PoId, DBConnection dbConnection);
        int CreateCoveringPr(int PoId, int ParentPrId, int UserId, int SupplierId, int QuotationId, DBConnection dbConnection);

        int GetParentPRId(int GrnId, int itemId, DBConnection dbConnection);
        PrMasterV2 GetPrForQuotationRejected(int PrId, int CompanyId, List<int> TabulationIds, int UserId, int DesignationId, DBConnection dbConnection);
    }


    public class PR_MasterDAOSQLImpl : PR_MasterDAO {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<PR_Master> GetPrMasterListByDaterange(int departmentid, DateTime startdate, DateTime enddate, DBConnection dbConnection) {
            List<PR_Master> GetPRMasterList = new List<PR_Master>();
            PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER  WHERE DEPARTMENT_ID =" + departmentid + " AND  ( DATE_OF_REQUEST BETWEEN  '" + startdate + "' AND  '" + enddate + "')";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetPRMasterList = dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
            return GetPRMasterList;
        }
        //Reorder function stock by Pasindu 2020/04/29
        public List<string> GetPrCodesByPrIds(List<int> PrIds, DBConnection dbConnection) {
            List<string> codes = new List<string>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PR_CODE FROM " + dbLibrary + ".PR_MASTER WHERE PR_ID IN(" + string.Join(",", PrIds) + ")";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                if (dbConnection.dr.HasRows) {
                    while (dbConnection.dr.Read()) {
                        codes.Add(dbConnection.dr[0].ToString());
                    }
                }
            }
            return codes;
        }
        //Reorder function stock by Pasindu 2020/04/29
        public int SavePRMasterV2(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string terms, int SubDepartmentId, int CategoryId, int warehouseId, DBConnection dbConnection) {
            int PrId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER ";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0) {
                PrId = 001;
            }
            else {
                dbConnection.cmd.CommandText = "SELECT MAX (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER ";
                PrId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            string PRCode = string.Empty;

            if (count == 0) {
                PRCode = "PR" + 1;
            }
            else {
                dbConnection.cmd.CommandText = "SELECT COUNT (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER WHERE DEPARTMENT_ID = " + DepartmentId + "";
                var count01 = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                PRCode = "PR" + count01;
            }

            //dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_MASTER (PR_ID, PR_CODE, DEPARTMENT_ID, DATE_OF_REQUEST, QUOTATION_FOR, OUR_REFERENCE, REQUESTED_BY, CREATED_DATETIME, CREATED_BY, UPDATED_DATETIME, UPDATED_BY, IS_ACTIVE, PR_IS_APPROVED, PR_IS_APPROVED_OR_REJECT_BY, PR_IS_APPROVED_OR_REJECT_DATE, PR_IS_APPROVED_FOR_BID, PR_IS_APPROVED_OR_REJECT_FOR_BID_BY,PR_IS_APPROVED_OR_REJECT_FOR_BID_DATE,IS_PO_RAISED,BASE_PR_ID, PR_TYPE_ID, EXPENSE_TYPE, REF_01, REF_02, REF_03, REF_04, REF_05, REF_06,BID_TERMS_CONDITION,SUB_DEPARTMENT_ID) VALUES" +
            //            "( " + PrId + ", '" + PRCode + "' , " + DepartmentId + ", '" + DateOfRequest + "', '" + QuotationFor + "', '" + OurReference + "', '" + RequestedBy + "', '" + CraeatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDateTime + "', '" + UpdatedBy + "', " + IsActive + ", " + PRAppoved + ", '" + PRApprovedOrRejectedBy + "', '" + PRApprovedOrRejectedDate + "', " + PRIsApproveForBid + ", '" + PRIsApprovedOrRejectedBy + "', '" + PRIsApprovedOrRejectedDate + "'," + 0 + "," + BasePrid + ", " + prTypeId + ",'" + expenseType + "','" + refNo01 + "','" + refNo02 + "','" + refNo03 + "','" + refNo04 + "','" + refNo05 + "','" + refNo06 + "','" + terms.ProcessString() + "'," + SubDepartmentId + ");";
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_MASTER (PR_ID, PR_CODE, DEPARTMENT_ID, DATE_OF_REQUEST, QUOTATION_FOR, OUR_REFERENCE, REQUESTED_BY, CREATED_DATETIME, CREATED_BY, UPDATED_DATETIME, UPDATED_BY, IS_ACTIVE, PR_IS_APPROVED, PR_IS_APPROVED_OR_REJECT_BY, PR_IS_APPROVED_OR_REJECT_DATE, PR_IS_APPROVED_FOR_BID, PR_IS_APPROVED_OR_REJECT_FOR_BID_BY,PR_IS_APPROVED_OR_REJECT_FOR_BID_DATE,IS_PO_RAISED,BASE_PR_ID, PR_TYPE_ID, EXPENSE_TYPE, REF_01, REF_02, REF_03, REF_04, REF_05, REF_06,BID_TERMS_CONDITION,CATEGORY_ID, WAREHOUSE_ID) VALUES" +
                        "( " + PrId + ", '" + PRCode + "' , " + DepartmentId + ", '" + DateOfRequest + "', '" + QuotationFor + "', '" + OurReference + "', '" + RequestedBy + "', '" + CraeatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDateTime + "', '" + UpdatedBy + "', " + IsActive + ", " + PRAppoved + ", '" + PRApprovedOrRejectedBy + "', '" + PRApprovedOrRejectedDate + "', " + PRIsApproveForBid + ", '" + PRIsApprovedOrRejectedBy + "', '" + PRIsApprovedOrRejectedDate + "'," + 0 + "," + BasePrid + ", " + prTypeId + ",'" + expenseType + "','" + refNo01 + "','" + refNo02 + "','" + refNo03 + "','" + refNo04 + "','" + refNo05 + "','" + refNo06 + "','" + terms.ProcessString() + "'," + CategoryId + "," + warehouseId + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();

            return PrId;
        }


        public int SavePRMaster(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string prProcedure, string purchaseType, DateTime requiredDate, string MRNReferenceNo, int itemCatId, DBConnection dbConnection) {
            int PrId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER ";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0) {
                PrId = 001;
            }
            else {
                dbConnection.cmd.CommandText = "SELECT MAX (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER ";
                PrId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            string PRCode = string.Empty;

            if (count == 0) {
                PRCode = "PR" + 1;
            }
            else {
                dbConnection.cmd.CommandText = "SELECT COUNT (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER WHERE DEPARTMENT_ID = " + DepartmentId + "";
                var count01 = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                PRCode = "PR" + count01;
            }

            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_MASTER (PR_ID, PR_CODE, DEPARTMENT_ID, DATE_OF_REQUEST, QUOTATION_FOR, OUR_REFERENCE, REQUESTED_BY, CREATED_DATETIME, CREATED_BY, UPDATED_DATETIME, UPDATED_BY, IS_ACTIVE, PR_IS_APPROVED, PR_IS_APPROVED_OR_REJECT_BY, PR_IS_APPROVED_OR_REJECT_DATE, PR_IS_APPROVED_FOR_BID, PR_IS_APPROVED_OR_REJECT_FOR_BID_BY,PR_IS_APPROVED_OR_REJECT_FOR_BID_DATE,IS_PO_RAISED,BASE_PR_ID, PR_TYPE_ID, EXPENSE_TYPE, REF_01, REF_02, REF_03, REF_04, REF_05, REF_06,PR_PROCEDURE,PURCHASE_TYPE,REQUIRED_DATE,MRNREFERENCE_NO,ITEM_CATEGORY_ID,STORE_KEEPER_ID) VALUES" +
                        "( " + PrId + ", '" + PRCode + "' , " + DepartmentId + ", '" + DateOfRequest + "', '" + QuotationFor + "', '" + OurReference + "', '" + RequestedBy + "', '" + CraeatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDateTime + "', '" + UpdatedBy + "', " + IsActive + ", " + PRAppoved + ", '" + PRApprovedOrRejectedBy + "', '" + PRApprovedOrRejectedDate + "', " + PRIsApproveForBid + ", '" + PRIsApprovedOrRejectedBy + "', '" + PRIsApprovedOrRejectedDate + "'," + 0 + "," + BasePrid + ", " + prTypeId + ",'" + expenseType + "','" + refNo01 + "','" + refNo02 + "','" + refNo03 + "','" + refNo04 + "','" + refNo05 + "','" + refNo06 + "' , '" + prProcedure + "' , '" + purchaseType + "' , '" + requiredDate + "' ,'" + MRNReferenceNo + "' , " + itemCatId + "," + CreatedBy + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();

            return PrId;
        }

        public string SavePRMasterAtCloning(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string prProcedure, string purchaseType, DateTime requiredDate, string MRNReferenceNo, DBConnection dbConnection) {
            int PrId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER ";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0) {
                PrId = 001;
            }
            else {
                dbConnection.cmd.CommandText = "SELECT MAX (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER ";
                PrId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            string PRCode = string.Empty;

            if (count == 0) {
                PRCode = "PR" + 1;
            }
            else {
                dbConnection.cmd.CommandText = "SELECT COUNT (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER WHERE DEPARTMENT_ID = " + DepartmentId + "";
                var count01 = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                PRCode = "PR" + count01;
            }

            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_MASTER (PR_ID, PR_CODE, DEPARTMENT_ID, DATE_OF_REQUEST, QUOTATION_FOR, OUR_REFERENCE, REQUESTED_BY, CREATED_DATETIME, CREATED_BY, UPDATED_DATETIME, UPDATED_BY, IS_ACTIVE, PR_IS_APPROVED, PR_IS_APPROVED_OR_REJECT_BY, PR_IS_APPROVED_OR_REJECT_DATE, PR_IS_APPROVED_FOR_BID, PR_IS_APPROVED_OR_REJECT_FOR_BID_BY,PR_IS_APPROVED_OR_REJECT_FOR_BID_DATE,IS_PO_RAISED,BASE_PR_ID, PR_TYPE_ID, EXPENSE_TYPE, REF_01, REF_02, REF_03, REF_04, REF_05, REF_06,PR_PROCEDURE,PURCHASE_TYPE,REQUIRED_DATE,MRNREFERENCE_NO) VALUES" +
                        "( " + PrId + ", '" + PRCode + "' , " + DepartmentId + ", '" + DateOfRequest + "', '" + QuotationFor + "', '" + OurReference + "', '" + RequestedBy + "', '" + CraeatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDateTime + "', '" + UpdatedBy + "', " + IsActive + ", " + PRAppoved + ", '" + PRApprovedOrRejectedBy + "', '" + PRApprovedOrRejectedDate + "', " + PRIsApproveForBid + ", '" + PRIsApprovedOrRejectedBy + "', '" + PRIsApprovedOrRejectedDate + "'," + 0 + "," + BasePrid + ", " + prTypeId + ",'" + expenseType + "','" + refNo01 + "','" + refNo02 + "','" + refNo03 + "','" + refNo04 + "','" + refNo05 + "','" + refNo06 + "' , '" + prProcedure + "' , '" + purchaseType + "' , '" + requiredDate + "' ,'" + MRNReferenceNo + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            int result = dbConnection.cmd.ExecuteNonQuery();
            if (result > 0)
                return PRCode;
            else
                return "";
        }

        public string FetchPRCode(int DepartmentId, DBConnection dbConnection) {
            int PrId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER ";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0) {
                PrId = 001;
            }
            else {
                dbConnection.cmd.CommandText = "SELECT MAX (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER";
                PrId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            string PRCode = string.Empty;

            if (count == 0) {
                PRCode = "PR" + 1;
            }
            else {
                dbConnection.cmd.CommandText = "SELECT COUNT (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER WHERE COMPANY_ID = " + DepartmentId + "";
                var count01 = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                PRCode = "PR" + count01;
            }

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return PRCode;
            }
        }

        public List<PR_Master> FetchApprovePRDataByDeptId(int DepartmentId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER PM\n" +
                 "INNER JOIN (SELECT SUB_DEPARTMENT_ID, USER_ID FROM COMPANY_LOGIN ) AS CL ON PM.CREATED_BY = CL.USER_ID\n" +
                 "INNER JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON CL.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                " WHERE PM.IS_ACTIVE = '1' AND PM.DEPARTMENT_ID=" + DepartmentId + " AND PM.PR_IS_APPROVED= 0 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }

        public PR_Master FetchApprovePRDataByDeptIdAndPRId(int DepartmentId, int PrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS prm INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS cd ON ( prm.DEPARTMENT_ID = cd.DEPARTMENT_ID ) WHERE  prm.IS_ACTIVE = 1 AND prm.DEPARTMENT_ID=" + DepartmentId + " AND  prm.PR_ID=" + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PR_Master>(dbConnection.dr);
            }
        }

        public int UpdateIsApprovePR(int DepartmentId, int PrId, int Status, int PRApprovedUserId, int isActive, string RejectedReason, DBConnection dbConnection) {
            string sql = "";
            if (Status == 1) {
                sql += "UPDATE " + dbLibrary + ".PR_MASTER SET PR_IS_APPROVED= " + Status + ", PR_IS_APPROVED_OR_REJECT_BY=" + PRApprovedUserId + ", IS_ACTIVE=" + isActive + ", REJECTED_REASON='" + RejectedReason + "', CURRENT_STATUS = 1 WHERE PR_ID = " + PrId + " AND DEPARTMENT_ID = " + DepartmentId + ";\n";
                sql += "UPDATE PR_DETAIL SET CURRENT_STATUS = 1 WHERE PR_ID=" + PrId + ";\n";
                sql += "INSERT INTO PR_DETAIL_STATUS_LOG SELECT PRD_ID,1,'" + LocalTime.Now + "'," + PRApprovedUserId + " FROM PR_DETAIL WHERE PR_ID=" + PrId + ";";
            }
            else {
                sql += "UPDATE " + dbLibrary + ".PR_MASTER SET PR_IS_APPROVED= " + Status + ", PR_IS_APPROVED_OR_REJECT_BY=" + PRApprovedUserId + ", IS_ACTIVE=" + isActive + ", REJECTED_REASON='" + RejectedReason + "', CURRENT_STATUS = 2 WHERE PR_ID = " + PrId + " AND DEPARTMENT_ID = " + DepartmentId + ";\n";
                sql += "UPDATE PR_DETAIL SET CURRENT_STATUS = 2 WHERE PR_ID=" + PrId + ";\n";
                sql += "INSERT INTO PR_DETAIL_STATUS_LOG SELECT PRD_ID,2,'" + LocalTime.Now + "'," + PRApprovedUserId + " FROM PR_DETAIL WHERE PR_ID=" + PrId + ";";

            }

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PR_Master> FetchApprovePRDataByDeptIdApproved(int DepartmentId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER WHERE IS_ACTIVE = '1' AND DEPARTMENT_ID=" + DepartmentId + " AND PR_IS_APPROVED= 1  AND PR_IS_APPROVED_FOR_BID=0 OR PR_IS_APPROVED_FOR_BID=2 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }

        public int UpdateOprnForBid(int DepartmentId, int PrId, int BidOpeningStatus, string BidTermCondition, int ApprovedBy, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_MASTER SET PR_IS_APPROVED_FOR_BID=" + BidOpeningStatus + ", BID_TERMS_CONDITION='" + BidTermCondition + "', PR_IS_APPROVED_OR_REJECT_FOR_BID_BY=" + ApprovedBy + ",PR_IS_APPROVED_OR_REJECT_FOR_BID_DATE='" + LocalTime.Now + "' WHERE PR_ID = " + PrId + " AND DEPARTMENT_ID = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public PR_Master FetchApprovePRDataByPRId(int PrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER " +
                                            "WHERE IS_ACTIVE = 1  AND PR_ID=" + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PR_Master>(dbConnection.dr);
            }
        }

        public int UpdatePORaised(int PrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_MASTER SET IS_PO_RAISED= 1  WHERE PR_ID = " + PrId + " ;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdatePRMaster(int PrId, int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string prProcedure, string purchaseType, DateTime requiredDate, string MRNReferenceNo, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_MASTER SET  DATE_OF_REQUEST = '" + DateOfRequest + "' , QUOTATION_FOR = '" + QuotationFor + "' , OUR_REFERENCE = '" + OurReference + "', REQUESTED_BY = '" + RequestedBy + "', UPDATED_DATETIME = '" + UpdatedDateTime + "' , UPDATED_BY = '" + UpdatedBy + "' , IS_ACTIVE = " + IsActive + " , PR_IS_APPROVED = " + PRAppoved + " , PR_IS_APPROVED_OR_REJECT_BY = '" + PRApprovedOrRejectedBy + "' , PR_IS_APPROVED_OR_REJECT_DATE = '" + PRApprovedOrRejectedDate + "' , PR_IS_APPROVED_FOR_BID = " + PRIsApproveForBid + " , PR_IS_APPROVED_OR_REJECT_FOR_BID_BY = '" + PRIsApprovedOrRejectedBy + "' , PR_IS_APPROVED_OR_REJECT_FOR_BID_DATE = '" + PRIsApprovedOrRejectedDate + "' , IS_PO_RAISED = " + 0 + ", PR_TYPE_ID = " + prTypeId + ", EXPENSE_TYPE = '" + expenseType + "' , REF_01 = '" + refNo01 + "',REF_02 = '" + refNo02 + "',REF_03 = '" + refNo03 + "',REF_04 = '" + refNo04 + "',REF_05 = '" + refNo05 + "',REF_06 = '" + refNo06 + "' , PR_PROCEDURE = '" + prProcedure + "' , PURCHASE_TYPE = '" + purchaseType + "' , REQUIRED_DATE = '" + requiredDate + "', MRNREFERENCE_NO = '" + MRNReferenceNo + "' WHERE PR_ID = " + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PR_Master> FetchRejectedPr(int DepartmentId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "select * from " + dbLibrary + ".PR_MASTER where PR_IS_APPROVED = 2 AND DEPARTMENT_ID = " + DepartmentId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }

        public PR_Master FetchRejectPR(int PrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER WHERE PR_ID=" + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PR_Master>(dbConnection.dr);
            }
        }

        public List<PR_Master> FetchDetailsToEdit(int DepartmentId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".PR_MASTER " +
                                           " WHERE (IS_ACTIVE = '1' AND DEPARTMENT_ID=" + DepartmentId + " AND PR_IS_APPROVED= '0') " +
                                           " OR (IS_ACTIVE = '0' AND DEPARTMENT_ID=" + DepartmentId + " AND PR_IS_APPROVED= '3' AND BASE_PR_ID > 0)";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }

        public List<PR_Master> FetchYetSubmitPR(int Department, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            //dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".PR_MASTER AS PM" +
            //                               " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PD.PR_ID = PM.PR_ID  " +
            //                               " WHERE PM.IS_ACTIVE = '1' AND  PM.DEPARTMENT_ID=" + Department + "  " +
            //                               " AND PM.PR_IS_APPROVED= 1 AND PM.PR_IS_CONFIRMED_APPROVAL= 1  AND (PD.SUBMIT_FOR_BID = 0 OR PD.SUBMIT_FOR_BID = 2); ";

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".PR_MASTER AS PM" +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PD.PR_ID = PM.PR_ID  " +
                                           " WHERE PM.IS_ACTIVE = '1' AND  PM.DEPARTMENT_ID=" + Department + "  " +
                                           " AND PM.PR_IS_APPROVED= 1  AND (PD.SUBMIT_FOR_BID = 0 OR PD.SUBMIT_FOR_BID = 2); ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }

        }
        public List<PR_Master> FetchTotalPR(int Department, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".PR_MASTER AS PM" +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PD.PR_ID = PM.PR_ID  " +
                                           " WHERE PM.DEPARTMENT_ID=" + Department + " AND (PD.SUBMIT_FOR_BID = 0 OR PD.SUBMIT_FOR_BID = 1 OR PD.SUBMIT_FOR_BID = 2) AND PR_IS_APPROVED != '0' AND PM.IS_ACTIVE=1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }

        public List<PR_Master> FetchTotalBidforChart(int Department, int year, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT {fn MONTHNAME(PM.PR_IS_APPROVED_OR_REJECT_FOR_BID_DATE)} AS MounthName ,COUNT(PM.PR_ID) AS PR_Count FROM " + dbLibrary + ".PR_MASTER AS PM" +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PD.PR_ID = PM.PR_ID  " +
                                           " WHERE PM.IS_ACTIVE = '1' AND  PM.DEPARTMENT_ID=" + Department + "  " +
                                           " AND PM.PR_IS_APPROVED= 1 AND PM.PR_IS_APPROVED_OR_REJECT_FOR_BID_DATE like '%" + year + "%' GROUP BY  {fn MONTHNAME(PM.PR_IS_APPROVED_OR_REJECT_FOR_BID_DATE)} ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }

        public int countTotalPr(int DepartmentId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER WHERE  DEPARTMENT_ID = " + DepartmentId + "AND \"CREATED_DATETIME\" like '%" + LocalTime.Today.Year + "%'";
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }
        public List<PR_Master> countTotalPrtochart(int DepartmentId, int year, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT {fn MONTHNAME(CREATED_DATETIME)} AS MounthName ,COUNT(PR_ID) AS PR_Count FROM \"PR_MASTER\" WHERE  \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"CREATED_DATETIME\" like '%" + year + "%'" +
                "GROUP BY  {fn MONTHNAME(CREATED_DATETIME)}";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }

        public int countApprovedPr(int DepartmentId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER WHERE PR_IS_APPROVED= '1' AND   DEPARTMENT_ID = " + DepartmentId + " AND \"CREATED_DATETIME\" like '%" + LocalTime.Today.Year + "%'";
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public int countRejectedPr(int DepartmentId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER WHERE PR_IS_APPROVED= '2' AND  DEPARTMENT_ID = " + DepartmentId + " AND \"CREATED_DATETIME\" like '%" + LocalTime.Today.Year + "%' ";
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public int countPendingPr(int DepartmentId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER WHERE PR_IS_APPROVED= '0' AND  DEPARTMENT_ID = " + DepartmentId + " AND \"CREATED_DATETIME\" like '%" + LocalTime.Today.Year + "%' ";
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public int GetDetailsByPrCode(int DepartmentId, string PrCode, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER WHERE PR_CODE= '" + PrCode + "' AND  COMPANY_ID = " + DepartmentId + " ";
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public int GetParentPRId(int GrnId,int itemId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            int Id = 0;
            dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM PR_MASTER AS PM " +
                    "INNER JOIN (SELECT * FROM PR_DETAIL WHERE ITEM_ID = "+ itemId + ") AS PD ON PD.PR_ID = PM.PR_ID " +
                "WHERE PURCHASE_PROCEDURE = 2 AND CLONED_FROM_PR = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = (SELECT PO_ID FROM PO_GRN WHERE GRN_ID= " + GrnId + ")) ";
            int count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count > 0) {
                dbConnection.cmd.CommandText = "SELECT PM.PR_ID FROM PR_MASTER AS PM " +
                    "INNER JOIN (SELECT * FROM PR_DETAIL WHERE ITEM_ID = " + itemId + ") AS PD ON PD.PR_ID = PM.PR_ID " +
                    "WHERE PURCHASE_PROCEDURE = 2 AND CLONED_FROM_PR = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = (SELECT PO_ID FROM PO_GRN WHERE GRN_ID= " + GrnId + ")) ";
                Id = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }
            return Id;
            
        }

        public List<PR_Master> FetchApprovePRDataByDeptIdReports(int DepartmentId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER WHERE IS_ACTIVE = '1' AND COMPANY_ID=" + DepartmentId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }

        public List<PR_Master> FetchApprovedPRForConfirmation(int Department, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER WHERE IS_ACTIVE = '1' AND DEPARTMENT_ID=" + Department + " AND PR_IS_APPROVED= 1 AND PR_IS_CONFIRMED_APPROVAL=0 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }

        public int ConfirmOrDenyPRApproval(int prId, int confirm, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_MASTER SET PR_IS_CONFIRMED_APPROVAL=" + confirm + " WHERE PR_ID=" + prId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        //New Methods By Salman created on 2019-01-17

        public List<PrMasterV2> GetPrListForBidSubmission(int CompanyId, DBConnection dbConnection) {
            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.COMPANY_ID = " + CompanyId + " AND ((PM.EXPENSE_TYPE =1 AND PM.IS_PR_APPROVED= 1 AND PM.IS_EXPENSE_APPROVED =1) OR (PM.EXPENSE_TYPE =2 AND PM.IS_PR_APPROVED= 1)) AND PM.IS_ACTIVE = 1 AND PM.IS_TERMINATED !=1 AND \n" +
                                            "PM.PR_ID IN\n" +
                                            "(SELECT PR_ID FROM PR_DETAIL WHERE SUBMIT_FOR_BID = 0 AND IS_TERMINATED !=1 AND CURRENT_STATUS !=12 GROUP BY PR_ID) \n" +        // CURRENT_STATUS 12 means procurement has ended. might need to change the number based on new numbers
                                            "ORDER BY PM.PR_ID";

            dbConnection.cmd.CommandType = CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> GetPrListForBidSubmissionWithItem(int CompanyId, DBConnection dbConnection)
        {
            //getting PR Masters
            List<PrMasterV2>  PrMasters;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT WH.LOCATION AS ITEM_NAME, PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.COMPANY_ID = " + CompanyId + " AND ((PM.EXPENSE_TYPE =1 AND PM.IS_PR_APPROVED= 1 AND PM.IS_EXPENSE_APPROVED =1) OR (PM.EXPENSE_TYPE =2 AND PM.IS_PR_APPROVED= 1)) AND PM.IS_ACTIVE = 1 AND PM.IS_TERMINATED !=1 AND \n" +
                                            "PM.PR_ID IN\n" +
                                            "(SELECT PR_ID FROM PR_DETAIL WHERE SUBMIT_FOR_BID = 0 AND IS_TERMINATED !=1 AND CURRENT_STATUS !=12 GROUP BY PR_ID) \n" +        // CURRENT_STATUS 12 means procurement has ended. might need to change the number based on new numbers
                                            "ORDER BY PM.PR_ID";

            dbConnection.cmd.CommandType = CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrMasters =  dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

            for (int i = 0; i < PrMasters.Count; i++)
            {
                //string items = DAOFactory.CreatePR_DetailDAO().GetItemsByPrID(PrMasters[i].PrId, dbConnection).Select(x => x.ItemName);
                PrMasters[i].ItemName = "";
                PrMasters[i].ItemName = string.Join(", ",
                    DAOFactory.CreatePR_DetailDAO().GetItemsByPrID(PrMasters[i].PrId, dbConnection).Select(x => x.ItemName));
            }
            return PrMasters;
        }

        public List<PrMasterV2> GetPrListForBidSubmissionByDate(int CompanyId, DateTime date, DBConnection dbConnection) {
            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.COMPANY_ID = " + CompanyId + " AND MONTH(PM.CREATED_DATETIME) =" + date.Month + " AND YEAR(PM.CREATED_DATETIME)=" + date.Year + " AND ((PM.EXPENSE_TYPE =1 AND PM.IS_PR_APPROVED= 1 AND PM.IS_EXPENSE_APPROVED =1) OR (PM.EXPENSE_TYPE =2 AND PM.IS_PR_APPROVED= 1)) AND PM.IS_ACTIVE = 1 AND PM.IS_TERMINATED !=1 AND \n" +
                                            "PM.PR_ID IN\n" +
                                            "(SELECT PR_ID FROM PR_DETAIL WHERE SUBMIT_FOR_BID = 0 AND IS_TERMINATED !=1 AND CURRENT_STATUS !=12 GROUP BY PR_ID) \n" +        // CURRENT_STATUS 12 means procurement has ended. might need to change the number based on new numbers
                                            "ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.CommandType = CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> GetPrListForBidSubmissionByPrCode(int CompanyId, string prCode, DBConnection dbConnection) {
            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.COMPANY_ID = " + CompanyId + " AND PM.PR_CODE = '" + prCode + "' AND ((PM.EXPENSE_TYPE =1 AND PM.IS_PR_APPROVED= 1 AND PM.IS_EXPENSE_APPROVED =1) OR (PM.EXPENSE_TYPE =2 AND PM.IS_PR_APPROVED= 1)) AND PM.IS_ACTIVE = 1 AND PM.IS_TERMINATED !=1 AND \n" +
                                            "PM.PR_ID IN\n" +
                                            "(SELECT PR_ID FROM PR_DETAIL WHERE SUBMIT_FOR_BID = 0 AND IS_TERMINATED !=1 AND CURRENT_STATUS !=12 GROUP BY PR_ID) \n" +        // CURRENT_STATUS 12 means procurement has ended. might need to change the number based on new numbers
                                            "ORDER BY PM.PR_ID";

            dbConnection.cmd.CommandType = CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public PrMasterV2 GetPrForBidSubmission(int PrId, int CompanyId, DBConnection dbConnection) {
            PrMasterV2 prMaster = null;

            //getting PR Master
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME, CLA.APPROVED_BY_NAME,ISC.SUB_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME, MRNM.EXPECTED_DATE AS MRN_EXPECTED_DATE FROM PR_MASTER AS PM \n" +
                                            //"LEFT JOIN (SELECT MRN_ID,EXPECTED_DATE AS MRN_EXPECTED_DATE FROM MRN_MASTER) AS MRNME ON PM.MRN_ID = MRNME.MRN_ID \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVED_BY_NAME FROM COMPANY_LOGIN) AS CLA ON PM.PR_APPROVAL_BY = CLA.USER_ID " +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "INNER JOIN ITEM_SUB_CATEGORY AS ISC ON PM.PR_SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.PR_ID=" + PrId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }


            if (prMaster != null) {
                //getting PR Details for PR Master
                prMaster.PrDetails = DAOFactory.CreatePR_DetailDAO().GetPrDetailsForBidSubmission(prMaster.PrId, CompanyId, prMaster.WarehouseId, dbConnection);

                for (int i = 0; i < prMaster.PrDetails.Count; i++) {
                    //getting BOMS of PR Detail
                    prMaster.PrDetails[i].PrBoms = DAOFactory.CreatePrBomDAOV2().GetPrdBomForEdit(prMaster.PrDetails[i].PrdId, dbConnection);
                    //getting Replacement Images of PR Detail
                    prMaster.PrDetails[i].PrReplacementFileUploads = DAOFactory.CreatePrReplacementFileUploadDAOV2().GetPrReplacementFileUploadForEdit(prMaster.PrDetails[i].PrdId, dbConnection);
                    //getting Standard Images of PR Detail
                    prMaster.PrDetails[i].PrFileUploads = DAOFactory.CreatePrFileUploadDAOV2().GetPrFileUploadForEdit(prMaster.PrDetails[i].PrdId, dbConnection);
                    //getting Supportive Docs of PR Detail
                    prMaster.PrDetails[i].PrSupportiveDocuments = DAOFactory.CreatePrSupportiveDocumentsDAOV2().GetPrSupportiveDocumentsForEdit(prMaster.PrDetails[i].PrdId, dbConnection);
                }

                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForBidSubmission(prMaster.PrId, dbConnection);

                for (int i = 0; i < prMaster.Bids.Count; i++) {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[i].BidId, CompanyId, dbConnection);

                }
            }


            return prMaster;
        }

        public List<PrMasterV2> GetPrListForBidApproval(int CompanyId, int LoggedInUser, DBConnection dbConnection) {
            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.COMPANY_ID = " + CompanyId + " AND PM.IS_ACTIVE = 1 AND PM.IS_TERMINATED !=1 AND\n" +
                                            "PM.PR_ID IN (SELECT PR_ID FROM " + dbLibrary + ".BIDDING WHERE IS_APPROVED = 0 AND PURCHASING_OFFICER =" + LoggedInUser + " GROUP BY PR_ID) \n" +
                                            "ORDER BY PM.PR_ID DESC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

        }

        public List<PrMasterV2> GetPrListForBidApprovalByDate(int CompanyId, int LoggedInUser, DateTime date, DBConnection dbConnection) {
            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.COMPANY_ID = " + CompanyId + " AND MONTH(PM.CREATED_DATETIME) =" + date.Month + " AND YEAR(PM.CREATED_DATETIME)=" + date.Year + " AND PM.IS_ACTIVE = 1 AND PM.IS_TERMINATED !=1 AND\n" +
                                            "PM.PR_ID IN (SELECT PR_ID FROM " + dbLibrary + ".BIDDING WHERE IS_APPROVED = 0 AND PURCHASING_OFFICER =" + LoggedInUser + " GROUP BY PR_ID) \n" +
                                            "ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

        }

        public List<PrMasterV2> GetPrListForBidApprovalByPrCode(int CompanyId, int LoggedInUser, string PrCode, DBConnection dbConnection) {
            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.COMPANY_ID = " + CompanyId + "AND PM.PR_CODE = '" + PrCode + "' AND PM.IS_ACTIVE = 1 AND PM.IS_TERMINATED !=1 AND\n" +
                                            "PM.PR_ID IN (SELECT PR_ID FROM " + dbLibrary + ".BIDDING WHERE IS_APPROVED = 0 AND PURCHASING_OFFICER =" + LoggedInUser + " GROUP BY PR_ID) \n" +
                                            "ORDER BY PM.PR_ID DESC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

        }

        public PrMasterV2 GetPrForBidApproval(int PrId, int CompanyId, DBConnection dbConnection) {
            PrMasterV2 prMaster = null;

            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME,UM.MEASUREMENT_SHORT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "LEFT JOIN PR_DETAIL AS PRD ON PM.PR_ID = PRD.PR_ID \n" +
                                            "LEFT JOIN UNIT_MEASUREMENT AS UM ON UM.MEASUREMENT_ID = PRD.MEASUREMENT_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.PR_ID=" + PrId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }

            if (prMaster != null) {

                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForBidApproval(prMaster.PrId, dbConnection);

                for (int a = 0; a < prMaster.Bids.Count; a++) {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[a].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[a].BidId, CompanyId, dbConnection);
                }

            }

            return prMaster;
        }

        public List<PrMasterV2> GetPrListForQuotationComparisonOld(List<int> SelectionPendingBidIds, DBConnection dbConnection) {
            string sql = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        //"LEFT JOIN PR_DETAIL AS PRD ON PM.PR_ID=PRD.PR_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PM.IS_TABULATION_REVIEW_APPROVED =0 AND PM.PR_ID IN (\n" +
                         "SELECT PR_ID FROM BIDDING WHERE BID_ID IN(" + string.Join(",", SelectionPendingBidIds) + ") GROUP BY PR_ID )  ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

        }
        public List<PrMasterV2> GetPrListForQuotationComparisonReviw(int CompanyId, DBConnection dbConnection) {
            string sql = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN BIDDING AS BID ON PM.PR_ID = BID.PR_ID \n" +
                        //"LEFT JOIN PR_DETAIL AS PRD ON PM.PR_ID=PRD.PR_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE BID.IS_TABULATION_APPROVAL =0 AND PM.PR_ID IN (\n" +
                         "SELECT PR_ID FROM BIDDING WHERE END_DATE<= '" + LocalTime.Now + "' GROUP BY PR_ID )  AND PM.COMPANY_ID = " + CompanyId + " ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

        }

        public List<PrMasterV2> GetPrListForQuotationComparisonReviwWithItem(int CompanyId, DBConnection dbConnection)
        {
            List<PrMasterV2> PrMasters;
            string sql = "SELECT WH.LOCATION AS ITEM_NAME, PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN BIDDING AS BID ON PM.PR_ID = BID.PR_ID \n" +
                        //"LEFT JOIN PR_DETAIL AS PRD ON PM.PR_ID=PRD.PR_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE BID.IS_TABULATION_APPROVAL =0 AND PM.PR_ID IN (\n" +
                         "SELECT PR_ID FROM BIDDING WHERE END_DATE<= '" + LocalTime.Now + "' GROUP BY PR_ID )  AND PM.COMPANY_ID = " + CompanyId + " ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrMasters = dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

            //for (int i = 0; i < PrMasters.Count; i++)
            //{
            //    PrMasters[i].ItemName = "";
            //    PrMasters[i].ItemName = string.Join(", ",
            //        DAOFactory.CreateAddItemDAO().FetchItemsByCategories(PrMasters[i].PrCategoryId, PrMasters[i].PrSubCategoryId, CompanyId, dbConnection).Select(x => x.ItemName));
            //}

            for (int i = 0; i < PrMasters.Count; i++)
            {
                //string items = DAOFactory.CreatePR_DetailDAO().GetItemsByPrID(PrMasters[i].PrId, dbConnection).Select(x => x.ItemName);
                PrMasters[i].ItemName = "";
                PrMasters[i].ItemName = string.Join(", ",
                    DAOFactory.CreatePR_DetailDAO().GetItemsByPrID(PrMasters[i].PrId, dbConnection).Select(x => x.ItemName));
            }

            return PrMasters;

        }

        public List<PrMasterV2> GetPrListForQuotationComparisonReviwByDate(int CompanyId, DateTime Date, DBConnection dbConnection) {
            string sql = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN BIDDING AS BID ON PM.PR_ID = BID.PR_ID \n" +
                        //"LEFT JOIN PR_DETAIL AS PRD ON PM.PR_ID=PRD.PR_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE BID.IS_TABULATION_APPROVAL =0 AND BID.BID_WAS_CLONED = 0 AND MONTH(PM.CREATED_DATETIME) =" + Date.Month + " AND YEAR(PM.CREATED_DATETIME)=" + Date.Year + "  AND PM.PR_ID IN (\n" +
                         "SELECT PR_ID FROM BIDDING WHERE END_DATE<= '" + LocalTime.Now + "' AND BID_WAS_CLONED = 0 GROUP BY PR_ID )  AND PM.COMPANY_ID = " + CompanyId + " ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

        }

        public List<PrMasterV2> GetPrListForQuotationComparisonReviwByPrCode(int CompanyId, string Code, DBConnection dbConnection) {
            string sql = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN BIDDING AS BID ON PM.PR_ID = BID.PR_ID \n" +
                        //"LEFT JOIN PR_DETAIL AS PRD ON PM.PR_ID=PRD.PR_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE BID.IS_TABULATION_APPROVAL =0 AND PM.PR_CODE = '" + Code + "' AND PM.PR_ID IN (\n" +
                         "SELECT PR_ID FROM BIDDING WHERE END_DATE<= '" + LocalTime.Now + "' AND BID_WAS_CLONED = 0  GROUP BY PR_ID )  AND PM.COMPANY_ID = " + CompanyId + " ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

        }
        public List<PrMasterV2> GetPrListForQuotationComparisonBid(List<int> SelectionPendingBidIds, DBConnection dbConnection) {
            string sql = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PM.PR_ID IN (\n" +
                         "SELECT PR_ID FROM BIDDING WHERE BID_ID IN(" + string.Join(",", SelectionPendingBidIds) + ") GROUP BY PR_ID)  ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

        }




        public List<PR_Master> GetPrListForQuotationComparison(int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM PR_MASTER AS PRM " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE) AS W ON PRM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "WHERE COMPANY_ID =" + CompanyId + " AND PR_ID IN(\n" +
                                            "SELECT PR_ID FROM BIDDING WHERE IS_APPROVED = 1 AND END_DATE < '" + LocalTime.Now + "' AND IS_TERMINATED !=1 AND ((IS_QUOTATION_SELECTED = 0) OR (IS_QUOTATION_SELECTED = 1 AND IS_QUOTATION_APPROVED =2) OR (IS_QUOTATION_SELECTED = 1 AND IS_QUOTATION_APPROVED =1 AND IS_QUOTATION_CONFIRMED =2))\n" +
                                            "GROUP BY PR_ID)";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }

        }

        public PrMasterV2 GetPrForQuotationComparison(int PrId, int CompanyId, List<int> SelectionPendingBidIds, DBConnection dbConnection) {
            PrMasterV2 prMaster = null;

            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME,PRD.MEASUREMENT_ID as MEASUREMENT_ID FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN PR_DETAIL AS PRD ON PM.PR_ID=PRD.PR_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.PR_ID=" + PrId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }

            if (prMaster != null) {

                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForQuotationComparison(prMaster.PrId, SelectionPendingBidIds, dbConnection);

                for (int b = 0; b < prMaster.Bids.Count; b++) {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[b].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[b].BidId, CompanyId, dbConnection);

                    DAOFactory.CreateBiddingItemDAO().GetLastPurchaseDetails(prMaster.Bids[b].BiddingItems, CompanyId, dbConnection);

                    DAOFactory.CreateBiddingItemDAO().GetLastPurchaseDetails(prMaster.Bids[b].BiddingItems, CompanyId, dbConnection);

                    //getting quotations submitted for bids in Pr master
                    prMaster.Bids[b].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(prMaster.Bids[b].BidId, dbConnection);

                    //gettig tabulaion for the bids
                    prMaster.Bids[b].Tabulations = DAOFactory.CreateTabulationMasterDAO().GetTabulationsByBidId(prMaster.Bids[b].BidId, dbConnection);
                    if (prMaster.Bids[b].Tabulations != null) {
                        for (int c = 0; c < prMaster.Bids[b].Tabulations.Count; c++) {
                            prMaster.Bids[b].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsByTabulationId(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);
                        }
                    }

                    for (int c = 0; c < prMaster.Bids[b].SupplierQuotations.Count; c++) {
                        //getting supplier details for quotations
                        prMaster.Bids[b].SupplierQuotations[c].SupplierDetails = DAOFactory.createSupplierDAO().GetSupplierBySupplierId(prMaster.Bids[b].SupplierQuotations[c].SupplierId, dbConnection);

                        //getting quotation items for quotations
                        prMaster.Bids[b].SupplierQuotations[c].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(prMaster.Bids[b].SupplierQuotations[c].QuotationId, CompanyId, dbConnection);

                        prMaster.Bids[b].SupplierQuotations[c].QuotationImages = DAOFactory.createQuotationImageDAO().GetQuotationImages(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);
                        prMaster.Bids[b].SupplierQuotations[c].UploadedFiles = DAOFactory.CreateSupplierBiddingFileUploadDAO().GetUploadedFiles(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);

                        //getting boms, images and files uploaded for quotation items
                        for (int d = 0; d < prMaster.Bids[b].SupplierQuotations[c].QuotationItems.Count; d++) {
                            prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].SupplierBOMs = DAOFactory.CreateSupplierBOMDAO().GetSupplierBom(prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].QuotationItemId, dbConnection);

                        }
                    }
                }

            }

            return prMaster;
        }

        public List<PrMasterV2> GetPrListForQuotationApproval(int CompanyId, List<int> TabulationIds, DBConnection dbConnection) {
            string sql = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PR_ID IN (\n" +
                        "SELECT PR_ID FROM BIDDING WHERE IS_APPROVED = 1\n" +
                        "AND END_DATE < '" + LocalTime.Now + "'\n" +
                        "AND IS_QUOTATION_SELECTED = 1\n" +
                        "AND BID_ID IN(\n" +
                        "SELECT BID_ID FROM TABULATION_MASTER WHERE TABULATION_ID IN(" + string.Join(",", TabulationIds) + ") GROUP BY BID_ID) GROUP BY PR_ID) ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> GetPrListForQuotationRejected(int CompanyId, DBConnection dbConnection) {
            string sql = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                     "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                     "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                     "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                     "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                     "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                     "WHERE PR_ID IN (\n" +
                     "SELECT PR_ID FROM BIDDING WHERE BID_ID IN (SELECT BID_ID FROM TABULATION_MASTER where IS_RECOMMENDED = 2 ) AND IS_REJECTED_BID_TERMINATED = 0 AND BID_WAS_CLONED = 0 )\n" +
                     "  ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public PrMasterV2 GetPrForQuotationApproval(int PrId, int CompanyId, List<int> TabulationIds, int UserId, int DesignationId, DBConnection dbConnection) {
            PrMasterV2 prMaster = null;

            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,PRD.IS_PO_RAISED, WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME,UM.SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "LEFT JOIN PR_DETAIL AS PRD ON PM.PR_ID = PRD.PR_ID \n" +
                        "LEFT JOIN MEASUREMENT_DETAIL AS UM ON UM.DETAIL_ID = PRD.MEASUREMENT_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PM.PR_ID=" + PrId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }

            if (prMaster != null) {



                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForQuotationApproval(prMaster.PrId, TabulationIds, dbConnection);

                for (int b = 0; b < prMaster.Bids.Count; b++) {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[b].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[b].BidId, CompanyId, dbConnection);

                    DAOFactory.CreateBiddingItemDAO().GetLastPurchaseDetails(prMaster.Bids[b].BiddingItems, CompanyId, dbConnection);


                    //getting quotations submitted for bids in Pr master
                    prMaster.Bids[b].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(prMaster.Bids[b].BidId, dbConnection);

                    //gettig tabulaion for the bids
                    prMaster.Bids[b].Tabulations = DAOFactory.CreateTabulationMasterDAO().GetTabulationsByBidId(prMaster.Bids[b].BidId, dbConnection);

                    prMaster.Bids[b].Tabulations
                        .ForEach(tb => {
                            if (tb.RecommendationOveridingDesignation == DesignationId) {
                                tb.CanLoggedInUserOverrideRecommendation = 1;
                            }
                            else {
                                tb.CanLoggedInUserOverrideRecommendation = 0;
                            }
                        });




                    for (int c = 0; c < prMaster.Bids[b].Tabulations.Count; c++) {
                        if (prMaster.PurchaseType == 1) {
                            prMaster.Bids[b].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsByTabulationId(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);
                        }
                        if (prMaster.PurchaseType == 2) {
                            prMaster.Bids[b].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsForImportsByTabulationId(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);

                        }
                        prMaster.Bids[b].Tabulations[c].TabulationRecommendations = DAOFactory.CreateTabulationRecommendationDAO().GetTabulationRecommendations(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);

                        if (prMaster.Bids[b].Tabulations[c].IsSelected == 1 && prMaster.Bids[b].Tabulations[c].IsCurrent == 1) {
                            prMaster.Bids[b].Tabulations[c].TabulationRecommendations.ForEach(tbr => {
                                if (tbr.IsRecommended == 0) {
                                    if (tbr.OverridingDesignation == DesignationId)
                                        tbr.CanLoggedInUserOverride = 1;
                                    else
                                        tbr.CanLoggedInUserOverride = 0;

                                    if (tbr.DesignationId == DesignationId || tbr.RecommendedBy == UserId)
                                        tbr.CanLoggedInUserRecommend = 1;
                                    else
                                        tbr.CanLoggedInUserRecommend = 0;
                                }
                            });

                            for (int k = 0; k < prMaster.Bids[b].Tabulations[c].TabulationRecommendations.Count; k++) {
                                if (prMaster.IsPORaised == 1) {
                                    prMaster.Bids[b].Tabulations[c].TabulationRecommendations[k].IsPORaisedforPRDetail = 1;

                                }
                                else {
                                    prMaster.Bids[b].Tabulations[c].TabulationRecommendations[k].IsPORaisedforPRDetail = 0;
                                }
                            }
                        }
                    }



                    for (int c = 0; c < prMaster.Bids[b].SupplierQuotations.Count; c++) {

                        //getting supplier details for quotations
                        prMaster.Bids[b].SupplierQuotations[c].SupplierDetails = DAOFactory.createSupplierDAO().GetSupplierBySupplierId(prMaster.Bids[b].SupplierQuotations[c].SupplierId, dbConnection);

                        //getting quotation items for quotations
                        prMaster.Bids[b].SupplierQuotations[c].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(prMaster.Bids[b].SupplierQuotations[c].QuotationId, CompanyId, dbConnection);

                        prMaster.Bids[b].SupplierQuotations[c].QuotationImages = DAOFactory.createQuotationImageDAO().GetQuotationImages(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);
                        prMaster.Bids[b].SupplierQuotations[c].UploadedFiles = DAOFactory.CreateSupplierBiddingFileUploadDAO().GetUploadedFiles(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);

                        //getting boms, images and files uploaded for quotation items
                        for (int d = 0; d < prMaster.Bids[b].SupplierQuotations[c].QuotationItems.Count; d++) {
                            prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].SupplierBOMs = DAOFactory.CreateSupplierBOMDAO().GetSupplierBom(prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].QuotationItemId, dbConnection);
                        }
                    }
                }

            }

            return prMaster;
        }

        public PrMasterV2 GetPrForQuotationApprovalRej(int PrId, int CompanyId, int UserId, int DesignationId, DBConnection dbConnection) {
            PrMasterV2 prMaster = null;

            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME,UM.SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "LEFT JOIN PR_DETAIL AS PRD ON PM.PR_ID = PRD.PR_ID \n" +
                        "LEFT JOIN MEASUREMENT_DETAIL AS UM ON UM.DETAIL_ID = PRD.MEASUREMENT_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PM.PR_ID=" + PrId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }

            if (prMaster != null) {

                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForQuotationApprovalRej(prMaster.PrId, dbConnection);

                for (int b = 0; b < prMaster.Bids.Count; b++) {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[b].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[b].BidId, CompanyId, dbConnection);

                    DAOFactory.CreateBiddingItemDAO().GetLastPurchaseDetails(prMaster.Bids[b].BiddingItems, CompanyId, dbConnection);


                    //getting quotations submitted for bids in Pr master
                    prMaster.Bids[b].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(prMaster.Bids[b].BidId, dbConnection);

                    //gettig tabulaion for the bids
                    prMaster.Bids[b].Tabulations = DAOFactory.CreateTabulationMasterDAO().GetTabulationsByBidId(prMaster.Bids[b].BidId, dbConnection);

                    prMaster.Bids[b].Tabulations
                        .ForEach(tb => {
                            if (tb.RecommendationOveridingDesignation == DesignationId) {
                                tb.CanLoggedInUserOverrideRecommendation = 1;
                            }
                            else {
                                tb.CanLoggedInUserOverrideRecommendation = 0;
                            }
                        });

                    for (int c = 0; c < prMaster.Bids[b].Tabulations.Count; c++) {
                        if (prMaster.PurchaseProcedure == 1) {
                            prMaster.Bids[b].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsByTabulationId(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);
                        }
                        if (prMaster.PurchaseType == 2) {
                            prMaster.Bids[b].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsForImportsByTabulationId(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);

                        }
                        prMaster.Bids[b].Tabulations[c].TabulationRecommendations = DAOFactory.CreateTabulationRecommendationDAO().GetTabulationRecommendations(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);

                        if (prMaster.Bids[b].Tabulations[c].IsSelected == 1 && prMaster.Bids[b].Tabulations[c].IsCurrent == 1) {
                            prMaster.Bids[b].Tabulations[c].TabulationRecommendations.ForEach(tbr => {
                                if (tbr.IsRecommended == 0) {
                                    if (tbr.OverridingDesignation == DesignationId)
                                        tbr.CanLoggedInUserOverride = 1;
                                    else
                                        tbr.CanLoggedInUserOverride = 0;

                                    if (tbr.DesignationId == DesignationId || tbr.RecommendedBy == UserId)
                                        tbr.CanLoggedInUserRecommend = 1;
                                    else
                                        tbr.CanLoggedInUserRecommend = 0;
                                }
                            });
                        }
                    }



                    for (int c = 0; c < prMaster.Bids[b].SupplierQuotations.Count; c++) {

                        //getting supplier details for quotations
                        prMaster.Bids[b].SupplierQuotations[c].SupplierDetails = DAOFactory.createSupplierDAO().GetSupplierBySupplierId(prMaster.Bids[b].SupplierQuotations[c].SupplierId, dbConnection);

                        //getting quotation items for quotations
                        prMaster.Bids[b].SupplierQuotations[c].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(prMaster.Bids[b].SupplierQuotations[c].QuotationId, CompanyId, dbConnection);

                        prMaster.Bids[b].SupplierQuotations[c].QuotationImages = DAOFactory.createQuotationImageDAO().GetQuotationImages(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);
                        prMaster.Bids[b].SupplierQuotations[c].UploadedFiles = DAOFactory.CreateSupplierBiddingFileUploadDAO().GetUploadedFiles(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);

                        //getting boms, images and files uploaded for quotation items
                        for (int d = 0; d < prMaster.Bids[b].SupplierQuotations[c].QuotationItems.Count; d++) {
                            prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].SupplierBOMs = DAOFactory.CreateSupplierBOMDAO().GetSupplierBom(prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].QuotationItemId, dbConnection);
                        }
                    }
                }

            }

            return prMaster;
        }

        public List<PrMasterV2> GetPrListForQuotationConfirmation(int CompanyId, List<int> TabulationIds, DBConnection dbConnection) {
            string sql = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PM.PR_ID IN (\n" +
                         "SELECT PR_ID FROM BIDDING WHERE IS_APPROVED = 1\n" +
                         "AND END_DATE < '" + LocalTime.Now + "'\n" +
                         "AND IS_QUOTATION_SELECTED = 1 AND IS_QUOTATION_APPROVED =1\n" +
                         "AND BID_ID IN(\n" +
                         "SELECT BID_ID FROM TABULATION_MASTER WHERE TABULATION_ID IN(" + string.Join(",", TabulationIds) + ") GROUP BY BID_ID) GROUP BY PR_ID) ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> GetPrListForQuotationAppRejected(int CompanyId, DBConnection dbConnection) {
            string sql = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PR_ID IN (\n" +
                       "SELECT PR_ID FROM BIDDING WHERE BID_ID IN (SELECT BID_ID FROM TABULATION_MASTER where IS_APPROVED = 2)AND IS_REJECTED_BID_TERMINATED = 0 AND BID_WAS_CLONED = 0 )\n" +
                       "  ORDER BY PM.CREATED_DATETIME ASC";
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public PrMasterV2 GetPrForQuotationConfirmation(int PrId, int CompanyId, List<int> TabulationIds, int UserId, int DesignationId, DBConnection dbConnection) {
            PrMasterV2 prMaster = null;

            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,PRD.IS_PO_RAISED, WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "LEFT JOIN PR_DETAIL AS PRD ON PM.PR_ID = PRD.PR_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PM.PR_ID=" + PrId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }

            if (prMaster != null) {
                //prMaster.Items  = DAOFactory.CreatePrDetailsDAOV2().GetPrItemS(prMaster.PrId, dbConnection);
                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForQuotationConfirmation(prMaster.PrId, TabulationIds, dbConnection);

                for (int b = 0; b < prMaster.Bids.Count; b++) {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[b].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[b].BidId, CompanyId, dbConnection);

                    //getting quotations submitted for bids in Pr master
                    prMaster.Bids[b].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(prMaster.Bids[b].BidId, dbConnection);

                    //gettig tabulaion for the bids
                    prMaster.Bids[b].Tabulations = DAOFactory.CreateTabulationMasterDAO().GetTabulationsByBidId(prMaster.Bids[b].BidId, dbConnection);

                    prMaster.Bids[b].Tabulations
                        .ForEach(tb => {
                            if (tb.ApprovalOveridingDesignation == DesignationId) {
                                tb.CanLoggedInUserOverrideApproval = 1;
                            }
                            else {
                                tb.CanLoggedInUserOverrideApproval = 0;
                            }
                        });

                    for (int c = 0; c < prMaster.Bids[b].Tabulations.Count; c++) {
                        if (prMaster.PurchaseType == 1) {
                            prMaster.Bids[b].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsByTabulationId(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);
                        }
                        if (prMaster.PurchaseType == 2) {
                            prMaster.Bids[b].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsForImportsByTabulationId(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);

                        }
                        prMaster.Bids[b].Tabulations[c].TabulationRecommendations = DAOFactory.CreateTabulationRecommendationDAO().GetTabulationRecommendations(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);
                        prMaster.Bids[b].Tabulations[c].TabulationApprovals = DAOFactory.CreateTabulationApprovalDAO().GetTabulationApprovals(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);

                        if (prMaster.Bids[b].Tabulations[c].IsSelected == 1 && prMaster.Bids[b].Tabulations[c].IsRecommended == 1 && prMaster.Bids[b].Tabulations[c].IsCurrent == 1) {
                            prMaster.Bids[b].Tabulations[c].TabulationApprovals.ForEach(tba => {
                                if (tba.IsApproved == 0) {
                                    if (tba.OverridingDesignation == DesignationId)
                                        tba.CanLoggedInUserOverride = 1;
                                    else
                                        tba.CanLoggedInUserOverride = 0;

                                    if (tba.DesignationId == DesignationId || tba.ApprovedBy == UserId)
                                        tba.CanLoggedInUserApprove = 1;
                                    else
                                        tba.CanLoggedInUserApprove = 0;
                                }
                            });
                            for (int k = 0; k < prMaster.Bids[b].Tabulations[c].TabulationApprovals.Count; k++) {
                                if (prMaster.IsPORaised == 1) {
                                    prMaster.Bids[b].Tabulations[c].TabulationApprovals[k].IsPORaisedforPRDetail = 1;

                                }
                                else {
                                    prMaster.Bids[b].Tabulations[c].TabulationApprovals[k].IsPORaisedforPRDetail = 0;
                                }
                            }
                        }
                    }


                    for (int c = 0; c < prMaster.Bids[b].SupplierQuotations.Count; c++) {

                        //getting supplier details for quotations
                        prMaster.Bids[b].SupplierQuotations[c].SupplierDetails = DAOFactory.createSupplierDAO().GetSupplierBySupplierId(prMaster.Bids[b].SupplierQuotations[c].SupplierId, dbConnection);

                        //getting quotation items for quotations
                        prMaster.Bids[b].SupplierQuotations[c].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(prMaster.Bids[b].SupplierQuotations[c].QuotationId, CompanyId, dbConnection);

                        prMaster.Bids[b].SupplierQuotations[c].QuotationImages = DAOFactory.createQuotationImageDAO().GetQuotationImages(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);
                        prMaster.Bids[b].SupplierQuotations[c].UploadedFiles = DAOFactory.CreateSupplierBiddingFileUploadDAO().GetUploadedFiles(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);

                        //getting boms, images and files uploaded for quotation items
                        for (int d = 0; d < prMaster.Bids[b].SupplierQuotations[c].QuotationItems.Count; d++) {
                            prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].SupplierBOMs = DAOFactory.CreateSupplierBOMDAO().GetSupplierBom(prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].QuotationItemId, dbConnection);
                        }
                    }
                }

            }

            return prMaster;
        }

        public PrMasterV2 GetPrForQuotationRejected(int PrId, int CompanyId, List<int> TabulationIds, int UserId, int DesignationId, DBConnection dbConnection) {
            PrMasterV2 prMaster = null;

            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,PRD.IS_PO_RAISED, WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "LEFT JOIN PR_DETAIL AS PRD ON PM.PR_ID = PRD.PR_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PM.PR_ID=" + PrId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }

            if (prMaster != null) {
                //prMaster.Items  = DAOFactory.CreatePrDetailsDAOV2().GetPrItemS(prMaster.PrId, dbConnection);
                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForQuotationRejected(prMaster.PrId, TabulationIds, dbConnection);

                for (int b = 0; b < prMaster.Bids.Count; b++) {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[b].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[b].BidId, CompanyId, dbConnection);

                    //getting quotations submitted for bids in Pr master
                    prMaster.Bids[b].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(prMaster.Bids[b].BidId, dbConnection);

                    //gettig tabulaion for the bids
                    prMaster.Bids[b].Tabulations = DAOFactory.CreateTabulationMasterDAO().GetTabulationsByBidId(prMaster.Bids[b].BidId, dbConnection);

                    prMaster.Bids[b].Tabulations
                        .ForEach(tb => {
                            if (tb.ApprovalOveridingDesignation == DesignationId) {
                                tb.CanLoggedInUserOverrideApproval = 1;
                            }
                            else {
                                tb.CanLoggedInUserOverrideApproval = 0;
                            }
                        });

                    for (int c = 0; c < prMaster.Bids[b].Tabulations.Count; c++) {
                        if (prMaster.PurchaseType == 1) {
                            prMaster.Bids[b].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsByTabulationId(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);
                        }
                        if (prMaster.PurchaseType == 2) {
                            prMaster.Bids[b].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsForImportsByTabulationId(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);

                        }
                        prMaster.Bids[b].Tabulations[c].TabulationRecommendations = DAOFactory.CreateTabulationRecommendationDAO().GetTabulationRecommendations(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);
                        prMaster.Bids[b].Tabulations[c].TabulationApprovals = DAOFactory.CreateTabulationApprovalDAO().GetTabulationApprovals(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);

                        if (prMaster.Bids[b].Tabulations[c].IsSelected == 1 && prMaster.Bids[b].Tabulations[c].IsRecommended == 1 && prMaster.Bids[b].Tabulations[c].IsCurrent == 1) {
                            prMaster.Bids[b].Tabulations[c].TabulationApprovals.ForEach(tba => {
                                if (tba.IsApproved == 0) {
                                    if (tba.OverridingDesignation == DesignationId)
                                        tba.CanLoggedInUserOverride = 1;
                                    else
                                        tba.CanLoggedInUserOverride = 0;

                                    if (tba.DesignationId == DesignationId || tba.ApprovedBy == UserId)
                                        tba.CanLoggedInUserApprove = 1;
                                    else
                                        tba.CanLoggedInUserApprove = 0;
                                }
                            });
                            for (int k = 0; k < prMaster.Bids[b].Tabulations[c].TabulationApprovals.Count; k++) {
                                if (prMaster.IsPORaised == 1) {
                                    prMaster.Bids[b].Tabulations[c].TabulationApprovals[k].IsPORaisedforPRDetail = 1;

                                }
                                else {
                                    prMaster.Bids[b].Tabulations[c].TabulationApprovals[k].IsPORaisedforPRDetail = 0;
                                }
                            }
                        }
                    }


                    for (int c = 0; c < prMaster.Bids[b].SupplierQuotations.Count; c++) {

                        //getting supplier details for quotations
                        prMaster.Bids[b].SupplierQuotations[c].SupplierDetails = DAOFactory.createSupplierDAO().GetSupplierBySupplierId(prMaster.Bids[b].SupplierQuotations[c].SupplierId, dbConnection);

                        //getting quotation items for quotations
                        prMaster.Bids[b].SupplierQuotations[c].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(prMaster.Bids[b].SupplierQuotations[c].QuotationId, CompanyId, dbConnection);

                        prMaster.Bids[b].SupplierQuotations[c].QuotationImages = DAOFactory.createQuotationImageDAO().GetQuotationImages(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);
                        prMaster.Bids[b].SupplierQuotations[c].UploadedFiles = DAOFactory.CreateSupplierBiddingFileUploadDAO().GetUploadedFiles(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);

                        //getting boms, images and files uploaded for quotation items
                        for (int d = 0; d < prMaster.Bids[b].SupplierQuotations[c].QuotationItems.Count; d++) {
                            prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].SupplierBOMs = DAOFactory.CreateSupplierBOMDAO().GetSupplierBom(prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].QuotationItemId, dbConnection);
                        }
                    }
                }

            }

            return prMaster;
        }

        public PrMasterV2 GetPrForQuotationConfirmationRej(int PrId, int CompanyId, int UserId, int DesignationId, DBConnection dbConnection) {
            PrMasterV2 prMaster = null;

            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PM.PR_ID=" + PrId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }

            if (prMaster != null) {

                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForQuotationConfirmationRej(prMaster.PrId, dbConnection);

                for (int b = 0; b < prMaster.Bids.Count; b++) {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[b].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[b].BidId, CompanyId, dbConnection);

                    //getting quotations submitted for bids in Pr master
                    prMaster.Bids[b].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(prMaster.Bids[b].BidId, dbConnection);

                    //gettig tabulaion for the bids
                    prMaster.Bids[b].Tabulations = DAOFactory.CreateTabulationMasterDAO().GetTabulationsByBidId(prMaster.Bids[b].BidId, dbConnection);

                    prMaster.Bids[b].Tabulations
                        .ForEach(tb => {
                            if (tb.ApprovalOveridingDesignation == DesignationId) {
                                tb.CanLoggedInUserOverrideApproval = 1;
                            }
                            else {
                                tb.CanLoggedInUserOverrideApproval = 0;
                            }
                        });

                    for (int c = 0; c < prMaster.Bids[b].Tabulations.Count; c++) {
                        if (prMaster.PurchaseType == 1) {
                            prMaster.Bids[b].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsByTabulationId(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);
                        }
                        if (prMaster.PurchaseType == 2) {
                            prMaster.Bids[b].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsForImportsByTabulationId(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);

                        }
                        prMaster.Bids[b].Tabulations[c].TabulationRecommendations = DAOFactory.CreateTabulationRecommendationDAO().GetTabulationRecommendations(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);
                        prMaster.Bids[b].Tabulations[c].TabulationApprovals = DAOFactory.CreateTabulationApprovalDAO().GetTabulationApprovals(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);

                        if (prMaster.Bids[b].Tabulations[c].IsSelected == 1 && prMaster.Bids[b].Tabulations[c].IsRecommended == 1 && prMaster.Bids[b].Tabulations[c].IsCurrent == 1) {
                            prMaster.Bids[b].Tabulations[c].TabulationApprovals.ForEach(tba => {
                                if (tba.IsApproved == 0) {
                                    if (tba.OverridingDesignation == DesignationId)
                                        tba.CanLoggedInUserOverride = 1;
                                    else
                                        tba.CanLoggedInUserOverride = 0;

                                    if (tba.DesignationId == DesignationId || tba.ApprovedBy == UserId)
                                        tba.CanLoggedInUserApprove = 1;
                                    else
                                        tba.CanLoggedInUserApprove = 0;
                                }
                            });
                        }
                    }


                    for (int c = 0; c < prMaster.Bids[b].SupplierQuotations.Count; c++) {

                        //getting supplier details for quotations
                        prMaster.Bids[b].SupplierQuotations[c].SupplierDetails = DAOFactory.createSupplierDAO().GetSupplierBySupplierId(prMaster.Bids[b].SupplierQuotations[c].SupplierId, dbConnection);

                        //getting quotation items for quotations
                        prMaster.Bids[b].SupplierQuotations[c].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(prMaster.Bids[b].SupplierQuotations[c].QuotationId, CompanyId, dbConnection);

                        prMaster.Bids[b].SupplierQuotations[c].QuotationImages = DAOFactory.createQuotationImageDAO().GetQuotationImages(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);
                        prMaster.Bids[b].SupplierQuotations[c].UploadedFiles = DAOFactory.CreateSupplierBiddingFileUploadDAO().GetUploadedFiles(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);

                        //getting boms, images and files uploaded for quotation items
                        for (int d = 0; d < prMaster.Bids[b].SupplierQuotations[c].QuotationItems.Count; d++) {
                            prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].SupplierBOMs = DAOFactory.CreateSupplierBOMDAO().GetSupplierBom(prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].QuotationItemId, dbConnection);
                        }
                    }
                }

            }

            return prMaster;
        }

        public List<PrMasterV2> GetPrListForManualQuotationSubmission(int CompanyId, int UserId, DBConnection dbConnection) {
            List<PrMasterV2> PrMasters;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME,Q.SUBMITTED_QUOTATIONS, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "LEFT JOIN(SELECT BID_ID, PR_ID,IS_TERMINATED,IS_TABULATION_APPROVAL FROM BIDDING WHERE (IS_CLONED = 1 AND BID_WAS_CLONED = 0) OR (IS_CLONED = 0 AND BID_WAS_CLONED = 0) ) AS B ON B.PR_ID = PM.PR_ID " +
                                            "LEFT JOIN(SELECT BID_ID, COUNT(QUOTATION_ID) AS SUBMITTED_QUOTATIONS FROM SUPPLIER_QUOTATION " +
                                            "WHERE IS_STAYED_AS_LATER_BID = 0 GROUP BY BID_ID) AS Q ON Q.BID_ID = B.BID_ID " +
                                           " WHERE PM.COMPANY_ID=" + CompanyId + "" +
                                           " AND PM.PR_ID IN(SELECT PR_ID FROM [BIDDING] WHERE IS_APPROVED=1\n" +
                                            "AND IS_QUOTATION_SELECTED = 0\n" +
                                            "AND START_DATE <= '" + LocalTime.Now + "'\n" +
                                            "AND IS_TABULATION_APPROVAL = 0 "+
                                            //"AND END_DATE > '" + LocalTime.Now + "'\n" +
                                            "AND PURCHASING_OFFICER = " + UserId + "\n" +
                                            "GROUP BY PR_ID) AND B.IS_TERMINATED != 1  ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrMasters = dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

            //Fetching bids for PRs
            for (int i = 0; i < PrMasters.Count; i++) {
                PrMasters[i].Bids = DAOFactory.CreateBiddingDAO().GetBidsForQuotationSubmission(PrMasters[i].PrId, dbConnection);

                //Fetching Bid Items for bids
                for (int j = 0; j < PrMasters[i].Bids.Count; j++) {
                    PrMasters[i].Bids[j].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(PrMasters[i].Bids[j].BidId, CompanyId, dbConnection);
                }
            }

            return PrMasters;
        }

        public List<PrMasterV2> GetPrListForManualQuotationSubmissionWithItem(int CompanyId, int UserId, DBConnection dbConnection)
        {
            List<PrMasterV2> PrMasters;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT WH.LOCATION AS ITEM_NAME, PM.*,WH.LOCATION AS WAREHOUSE_NAME,Q.SUBMITTED_QUOTATIONS, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "LEFT JOIN(SELECT BID_ID, PR_ID,IS_TERMINATED,IS_TABULATION_APPROVAL FROM BIDDING WHERE (IS_CLONED = 1 AND BID_WAS_CLONED = 0) OR (IS_CLONED = 0 AND BID_WAS_CLONED = 0) ) AS B ON B.PR_ID = PM.PR_ID " +
                                            "LEFT JOIN(SELECT BID_ID, COUNT(QUOTATION_ID) AS SUBMITTED_QUOTATIONS FROM SUPPLIER_QUOTATION " +
                                            "WHERE IS_STAYED_AS_LATER_BID = 0 GROUP BY BID_ID) AS Q ON Q.BID_ID = B.BID_ID " +
                                           " WHERE PM.COMPANY_ID=" + CompanyId + "" +
                                           " AND PM.PR_ID IN(SELECT PR_ID FROM [BIDDING] WHERE IS_APPROVED=1\n" +
                                            "AND IS_QUOTATION_SELECTED = 0\n" +
                                            "AND START_DATE <= '" + LocalTime.Now + "'\n" +
                                            "AND IS_TABULATION_APPROVAL = 0 " +
                                            //"AND END_DATE > '" + LocalTime.Now + "'\n" +
                                            "AND PURCHASING_OFFICER = " + UserId + "\n" +
                                            "GROUP BY PR_ID) AND B.IS_TERMINATED != 1  ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrMasters = dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

            //Fetching bids for PRs
            for (int i = 0; i < PrMasters.Count; i++)
            {
                PrMasters[i].Bids = DAOFactory.CreateBiddingDAO().GetBidsForQuotationSubmission(PrMasters[i].PrId, dbConnection);

                //Fetching Bid Items for bids
                for (int j = 0; j < PrMasters[i].Bids.Count; j++)
                {
                    PrMasters[i].Bids[j].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(PrMasters[i].Bids[j].BidId, CompanyId, dbConnection);
                }
            }

            //for (int i = 0; i < PrMasters.Count; i++)
            //{
            //    PrMasters[i].ItemName = "";
            //    PrMasters[i].ItemName = string.Join(", ",
            //        DAOFactory.CreateAddItemDAO().FetchItemsByCategories(PrMasters[i].PrCategoryId, PrMasters[i].PrSubCategoryId, CompanyId, dbConnection).Select(x => x.ItemName));
            //}

            for (int i = 0; i < PrMasters.Count; i++)
            {
                //string items = DAOFactory.CreatePR_DetailDAO().GetItemsByPrID(PrMasters[i].PrId, dbConnection).Select(x => x.ItemName);
                PrMasters[i].ItemName = "";
                PrMasters[i].ItemName = string.Join(", ",
                    DAOFactory.CreatePR_DetailDAO().GetItemsByPrID(PrMasters[i].PrId, dbConnection).Select(x => x.ItemName));
            }

            return PrMasters;
        }

        public List<PrMasterV2> GetPrListForRejectedBids(int CompanyId, int UserId, DBConnection dbConnection) {
            List<PrMasterV2> PrMasters;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                           " WHERE PM.COMPANY_ID=" + CompanyId + "" +
                                           " AND PM.PR_ID IN(SELECT PR_ID FROM [BIDDING] WHERE IS_APPROVED=1\n" +
                                            "AND IS_QUOTATION_SELECTED = 0\n" +
                                            //"AND START_DATE <= '" + LocalTime.Now + "'\n" +
                                            //"AND END_DATE > '" + LocalTime.Now + "'\n" +
                                            "AND PURCHASING_OFFICER = " + UserId + "\n" +
                                            "GROUP BY PR_ID);";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrMasters = dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

            //Fetching bids for PRs
            for (int i = 0; i < PrMasters.Count; i++) {
                PrMasters[i].Bids = DAOFactory.CreateBiddingDAO().GetBidsForQuotationSubmission(PrMasters[i].PrId, dbConnection);

                //Fetching Bid Items for bids
                for (int j = 0; j < PrMasters[i].Bids.Count; j++) {
                    PrMasters[i].Bids[j].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(PrMasters[i].Bids[j].BidId, CompanyId, dbConnection);
                }
            }

            return PrMasters;
        }

        public List<PrMasterV2> GetPrListForPoCreation(int CompanyId, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.COMPANY_ID =" + CompanyId + " AND PM.PR_ID IN(\n" +
                                            "SELECT PR_ID FROM BIDDING WHERE IS_APPROVED = 1 AND END_DATE < '" + LocalTime.Now + "' AND BID_ID IN (" +
                                            "SELECT BID_ID FROM TABULATION_MASTER WHERE IS_SELECTED = 1 AND\n" +
                                            "IS_APPROVED = 1 AND TABULATION_ID IN(\n" +
                                            "SELECT TABULATION_ID FROM TABULATION_DETAIL WHERE IS_SELECTED=1 AND IS_TERMINATED !=1 AND IS_ADDED_TO_PO = 0 GROUP BY TABULATION_ID)\n" +
                                            "GROUP BY BID_ID)\n" +
                                            "GROUP BY PR_ID) ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

        }

        public PrMasterV2 GetPrForPoCreation(int PrId, int CompanyId, int UserId, DBConnection dbConnection) {
            PrMasterV2 prMaster = null;

            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PM.PR_ID=" + PrId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }

            if (prMaster != null) {
                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForPoCreation(prMaster.PrId, UserId, dbConnection);

                for (int b = 0; b < prMaster.Bids.Count; b++) {

                    //gettig tabulaion for the bids
                    prMaster.Bids[b].SelectedTabulation = DAOFactory.CreateTabulationMasterDAO().GetApprovedTabulation(prMaster.Bids[b].BidId, dbConnection);

                    if (prMaster.Bids[b].SelectedTabulation != null) {
                        prMaster.Bids[b].SelectedTabulation.TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsByForPoCreation(prMaster.Bids[b].SelectedTabulation.TabulationId, CompanyId, prMaster.Bids[b].BidId, dbConnection);

                        prMaster.Bids[b].SelectedTabulation.TabulationRecommendations = DAOFactory.CreateTabulationRecommendationDAO().GetTabulationRecommendations(prMaster.Bids[b].SelectedTabulation.TabulationId, dbConnection);
                        prMaster.Bids[b].SelectedTabulation.TabulationApprovals = DAOFactory.CreateTabulationApprovalDAO().GetTabulationApprovals(prMaster.Bids[b].SelectedTabulation.TabulationId, dbConnection);
                    }
                }
            }
            return prMaster;
        }

        public string SaveMRNtoPR(int mrnId, int userId, int companyId, DBConnection dbConnection) {
            int PrId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER ";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0) {
                PrId = 001;
            }
            else {
                dbConnection.cmd.CommandText = "SELECT MAX (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER ";
                PrId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            string PRCode = string.Empty;

            if (count == 0) {
                PRCode = "PR" + 1;
            }
            else {
                dbConnection.cmd.CommandText = "SELECT COUNT (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER WHERE DEPARTMENT_ID = " + companyId + "";
                var count01 = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                PRCode = "PR" + count01;
            }



            dbConnection.cmd.CommandText = "[dbo].[usp_InsertMrntoPr]";
            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            dbConnection.cmd.Parameters.AddWithValue("@MRNID", mrnId);
            dbConnection.cmd.Parameters.AddWithValue("@PR_Id", PrId);
            dbConnection.cmd.Parameters.AddWithValue("@PR_Code", PRCode);
            dbConnection.cmd.Parameters.AddWithValue("@User_Id", userId);
            dbConnection.cmd.Parameters.AddWithValue("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            dbConnection.cmd.ExecuteNonQuery();
            var id = Convert.ToInt32(dbConnection.cmd.Parameters["@NewId"].Value);
            if (id > 0) {
                return PRCode;
            }
            else
                return null;


        }

        public PrMasterV2 GetPrForBidSubmissionView(int PrId, int CompanyId, DBConnection dbConnection) {
            PrMasterV2 prMaster = null;

            //getting PR Master
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME,SUB.SUB_CATEGORY_NAME as PR_SUB_CATEGORY_NAME  " +
                                            " FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "LEFT JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN ITEM_SUB_CATEGORY AS SUB ON SUB.SUB_CATEGORY_ID = PM.PR_SUB_CATEGORY_ID " +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.PR_ID=" + PrId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }


            if (prMaster != null) {
                //getting PR Details for PR Master
                prMaster.PrDetails = DAOFactory.CreatePR_DetailDAO().GetPrDetailsForBidSubmission(prMaster.PrId, CompanyId, prMaster.WarehouseId, dbConnection);

                for (int i = 0; i < prMaster.PrDetails.Count; i++) {
                    //getting BOMS of PR Detail
                    prMaster.PrDetails[i].PrBoms = DAOFactory.CreatePrBomDAOV2().GetPrdBomForEdit(prMaster.PrDetails[i].PrdId, dbConnection);
                    //getting Replacement Images of PR Detail
                    prMaster.PrDetails[i].PrReplacementFileUploads = DAOFactory.CreatePrReplacementFileUploadDAOV2().GetPrReplacementFileUploadForEdit(prMaster.PrDetails[i].PrdId, dbConnection);
                    //getting Standard Images of PR Detail
                    prMaster.PrDetails[i].PrFileUploads = DAOFactory.CreatePrFileUploadDAOV2().GetPrFileUploadForEdit(prMaster.PrDetails[i].PrdId, dbConnection);
                    //getting Supportive Docs of PR Detail
                    prMaster.PrDetails[i].PrSupportiveDocuments = DAOFactory.CreatePrSupportiveDocumentsDAOV2().GetPrSupportiveDocumentsForEdit(prMaster.PrDetails[i].PrdId, dbConnection);
                }

                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForBidSubmission(prMaster.PrId, dbConnection);

                for (int i = 0; i < prMaster.Bids.Count; i++) {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[i].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[i].BidId, CompanyId, dbConnection);
                    //getting bid plans for bids in Pr Master
                    prMaster.Bids[i].BiddingPlan = DAOFactory.createBiddingPlanDAO().GetBiddingPlanByID(prMaster.Bids[i].BidId, dbConnection);

                    for (int j = 0; j < prMaster.Bids[i].BiddingPlan.Count; j++) {
                        prMaster.Bids[i].BiddingPlan[j].biddingPlanFileUpload = DAOFactory.createBiddingPlanDAO().GetPalanfiles(prMaster.Bids[i].BiddingPlan[j].BidId, prMaster.Bids[i].BiddingPlan[j].PlanId, dbConnection);

                    }

                }


            }


            return prMaster;
        }

        public List<PR_Master> FetchAllPR(int Department, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".PR_MASTER AS PM" +
                                           " WHERE PM.DEPARTMENT_ID=" + Department + " AND PM.IS_ACTIVE=1 AND PM.PR_IS_APPROVED=1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }






        public PR_Master SearchPRForInquiryByPrId(int PrId, int CompanyId, DBConnection dbConnection) {
            PR_Master PrMaster = null;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM PR_MASTER AS PM\n" +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS CL ON PM.CREATED_BY=CL.USER_ID\n" +
                "WHERE PM.PR_ID =" + PrId + ";";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrMaster = dataAccessObject.GetSingleOject<PR_Master>(dbConnection.dr);
            }


            if (PrMaster != null) {

                PrMaster.PrDetails = DAOFactory.CreatePR_DetailDAO().GetPrDetailsByPRid(PrMaster.PrId, CompanyId, dbConnection);
                PrMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForPRInquiry(PrMaster.PrId, dbConnection);

                for (int j = 0; j < PrMaster.Bids.Count; j++) {
                    //getting bid items for bids in Pr Master
                    PrMaster.Bids[j].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(PrMaster.Bids[j].BidId, CompanyId, dbConnection);

                    PrMaster.Bids[j].POsCreated = new List<POMaster>();
                    PrMaster.Bids[j].GRNsCreated = new List<GrnMaster>();

                    //getting quotations submitted for bids in Pr master
                    PrMaster.Bids[j].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(PrMaster.Bids[j].BidId, dbConnection);


                    for (int k = 0; k < PrMaster.Bids[j].SupplierQuotations.Count; k++) {
                        //getting quotation items for quotations
                        PrMaster.Bids[j].SupplierQuotations[k].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(PrMaster.Bids[j].SupplierQuotations[k].QuotationId, CompanyId, dbConnection);

                        //getting all the pos created for the bid using quotationId
                        for (int l = 0; l < PrMaster.Bids[j].SupplierQuotations.Count; l++) {
                            PrMaster.Bids[j].POsCreated.AddRange(DAOFactory.createPOMasterDAO().GetPoMastersForPrInquiryByQuotationId(PrMaster.Bids[j].SupplierQuotations[l].QuotationId, dbConnection));
                        }

                        //retrieving po details and getting all the grns created for the bid using po id 
                        for (int l = 0; l < PrMaster.Bids[j].POsCreated.Count; l++) {
                            PrMaster.Bids[j].POsCreated[l].PoDetails = DAOFactory.createPODetailsDAO().GetPoDetailsForPrInquiry(PrMaster.Bids[j].POsCreated[l].PoID, CompanyId, dbConnection);

                            PrMaster.Bids[j].GRNsCreated.AddRange(DAOFactory.createGrnDAO().GetGrnMastersForPrInquiryByPoId(PrMaster.Bids[j].POsCreated[l].PoID, dbConnection));
                        }

                        //retrieving grn details
                        for (int l = 0; l < PrMaster.Bids[j].GRNsCreated.Count; l++) {
                            PrMaster.Bids[j].GRNsCreated[l]._GrnDetailsList = DAOFactory.createGRNDetailsDAO().GetGrnDetailsForPrInquiry(PrMaster.Bids[j].GRNsCreated[l].GrnId, CompanyId, dbConnection);
                        }
                    }
                }
            }

            return PrMaster;
        }

        //public List<PR_Master> GetPRListForPrInquiry(int CompanyId, DBConnection dbConnection)
        //{
        //    dbConnection.cmd.Parameters.Clear();
        //    dbConnection.cmd.CommandText = "SELECT PR_ID,PR_CODE FROM PR_MASTER\n" +
        //        "WHERE COMPANY_ID =" + CompanyId + " ORDER BY PR_ID ASC;";

        //    dbConnection.cmd.CommandType = System.Data.CommandType.Text;

        //    using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
        //    {
        //        DataAccessObject dataAccessObject = new DataAccessObject();
        //        return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
        //    }
        //}

        public List<PR_Master> GetPRtobeApprovedForAdvanceSearch(int companyId, int categoryId, int subDepartmentID, int serchtype, string searchkey, string usertype, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "[dbo].[usp_PRApproveAdvanceSearch]";
            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", companyId);
            dbConnection.cmd.Parameters.AddWithValue("@categoryId", categoryId);
            dbConnection.cmd.Parameters.AddWithValue("@subDepartmentId", subDepartmentID);
            dbConnection.cmd.Parameters.AddWithValue("@serchWord", searchkey);
            dbConnection.cmd.Parameters.AddWithValue("@usertype", usertype);
            dbConnection.cmd.Parameters.AddWithValue("@searchtype", serchtype);
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);

            }
        }

        public List<PR_Master> GetPRtobeApprovedforBiddingForAdvanceSearch(int companyId, int categoryId, int subDepartmentID, int serchtype, string searchkey, string usertype, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "[dbo].[usp_PRApproveForBidddingAdvanceSearch]";
            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", companyId);
            dbConnection.cmd.Parameters.AddWithValue("@categoryId", categoryId);
            dbConnection.cmd.Parameters.AddWithValue("@subDepartmentId", subDepartmentID);
            dbConnection.cmd.Parameters.AddWithValue("@serchWord", searchkey);
            dbConnection.cmd.Parameters.AddWithValue("@usertype", usertype);
            dbConnection.cmd.Parameters.AddWithValue("@searchtype", serchtype);
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);

            }
        }

        public List<PR_Master> GetQuotationBidddingrAdvanceSearch(int companyId, int categoryId, int supplierId, int serchtype, string searchkey, string usertype, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "[dbo].[usp_SelectQuotationBidddingAdvanceSearch]";
            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", companyId);
            dbConnection.cmd.Parameters.AddWithValue("@categoryId", categoryId);
            dbConnection.cmd.Parameters.AddWithValue("@supplierId", supplierId);
            dbConnection.cmd.Parameters.AddWithValue("@serchWord", searchkey);
            dbConnection.cmd.Parameters.AddWithValue("@usertype", usertype);
            dbConnection.cmd.Parameters.AddWithValue("@searchtype", serchtype);
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);

            }
        }

        public List<PR_Master> GetPrListForDocumentUploadPoCreation(int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM PR_MASTER WHERE DEPARTMENT_ID =" + CompanyId + " AND PR_ID IN(\n" +
                                            "SELECT PR_ID FROM BIDDING WHERE IS_APPROVED = 1 AND END_DATE < '" + LocalTime.Now + "' AND BID_ID IN (" +
                                            "SELECT BID_ID FROM SUPPLIER_QUOTATION WHERE RECOMMENDATION_TYPE=2 AND IS_RECOMMENDED=1 AND IS_SELECTED = 1 AND\n" +
                                            "IS_APPROVED = 1 AND IS_ADDED_TO_PO = 0 GROUP BY BID_ID)\n" +
                                            "GROUP BY PR_ID)";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }

        }

        public int CheckfileUploadedofteccommit(int bidId, int qutationId, DBConnection dbConnection) {
            var rectype = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT RECOMMENDATION_TYPE FROM SUPPLIER_QUOTATION WHERE IS_RECOMMENDED=1 AND IS_SELECTED = 1 AND\n" +
                                            "IS_APPROVED = 1 AND IS_ADDED_TO_PO = 0 AND BID_ID =" + bidId + " AND QUOTATION_ID=" + qutationId + " GROUP BY BID_ID";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            var obj = dbConnection.cmd.ExecuteScalar().ToString();
            if (obj != null) {
                rectype = int.Parse(obj);
            }

            if (rectype == 2) {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT IS_DOC_UPLOADED FROM SUPPLIER_QUOTATION WHERE IS_RECOMMENDED=1 AND IS_SELECTED = 1 AND\n" +
                                                "IS_APPROVED = 1 AND IS_ADDED_TO_PO = 0 AND BID_ID =" + bidId + " AND QUOTATION_ID=" + qutationId + " GROUP BY BID_ID";

                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                obj = dbConnection.cmd.ExecuteScalar().ToString();
                if (obj != null) {
                    return int.Parse(obj);
                }
                else {
                    return 0;
                }
            }
            else {
                return 1;
            }



        }

        public List<int> GetPRCountForDashboard(int CompanyId, int yearsearch, int purchaseType, DBConnection dbConnection) {
            List<int> PRCount = new List<int>();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(PR_ID) AS cnt FROM " + dbLibrary + ".PR_MASTER WHERE  (DATEPART(yyyy, CREATED_DATETIME)=" + yearsearch + ") AND PURCHASE_TYPE=" + purchaseType + " AND  COMPANY_ID = " + CompanyId + " ";

            PRCount.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(PR_ID) AS cnt FROM " + dbLibrary + ".PR_MASTER WHERE (DATEPART(yyyy, CREATED_DATETIME)=" + yearsearch + ") AND  PURCHASE_TYPE=" + purchaseType + " AND IS_PR_APPROVED= '0' AND  COMPANY_ID = " + CompanyId + " ";

            PRCount.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(PR_ID) AS cnt FROM " + dbLibrary + ".PR_MASTER WHERE (DATEPART(yyyy, CREATED_DATETIME)=" + yearsearch + ") AND PURCHASE_TYPE=" + purchaseType + " AND  IS_PR_APPROVED= '1' AND   COMPANY_ID = " + CompanyId + " ";

            PRCount.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(PR_ID) AS cnt FROM " + dbLibrary + ".PR_MASTER WHERE  (DATEPART(yyyy, CREATED_DATETIME)=" + yearsearch + ") AND PURCHASE_TYPE=" + purchaseType + " AND  IS_PR_APPROVED= '2' AND  COMPANY_ID = " + CompanyId + "  ";

            PRCount.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));



            return PRCount;
        }

        public DataTable GetPRCountForDashBoard(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            string query = "select isnull(sum(new.count), 0) as Count ,  '0-14' as Range from  "
+ " ( select count(PR_ID) as count, CONVERT(date, CREATED_DATETIME) as date, DATEDIFF(day, CREATED_DATETIME, '" + LocalTime.Now + "') AS Days"
+ " from PR_MASTER where CURRENT_STATUS != 3"
+ " group by CONVERT(date, CREATED_DATETIME), DATEDIFF(day, CREATED_DATETIME, '" + LocalTime.Now + "')) as new"
+ " where new.Days between 0 and 14"
+ "  union all"
+ "   select isnull(sum(new.count), 0) as Count ,  '15-30' as Range from"
+ "  ( select count(PR_ID) as count, CONVERT(date, CREATED_DATETIME) as date, DATEDIFF(day, CREATED_DATETIME, '" + LocalTime.Now + "') AS Days"
+ " from PR_MASTER where CURRENT_STATUS != 3"
+ " group by CONVERT(date, CREATED_DATETIME), DATEDIFF(day, CREATED_DATETIME, '" + LocalTime.Now + "')) as new"
+ " where new.Days between 15 and 30"
+ "  union all"
+ "   select isnull(sum(new.count), 0)   as Count ,  '31-60' as Range from"
+ "  ( select count(PR_ID) as count, CONVERT(date, CREATED_DATETIME) as date, DATEDIFF(day, CREATED_DATETIME, '" + LocalTime.Now + "') AS Days"
+ " from PR_MASTER where CURRENT_STATUS != 3"
+ " group by CONVERT(date, CREATED_DATETIME), DATEDIFF(day, CREATED_DATETIME, '" + LocalTime.Now + "')) as new"
+ " where new.Days between 31 and 60"
+ "  union all"
+ "   select isnull(sum(new.count), 0)   as Count  ,  '60 <' as Range from"
+ "  ( select count(PR_ID) as count, CONVERT(date, CREATED_DATETIME) as date, DATEDIFF(day, CREATED_DATETIME, '" + LocalTime.Now + "') AS Days"
+ " from PR_MASTER where CURRENT_STATUS != 3"
+ " group by CONVERT(date, CREATED_DATETIME), DATEDIFF(day, CREATED_DATETIME, '" + LocalTime.Now + "')) as new"
+ " where new.Days > 60";


            dbConnection.cmd.CommandText = query;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(dbConnection.cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        public List<PrMasterV2> GetPrListForBidSubmited(int CompanyId, int UserId, DBConnection dbConnection) {
            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.PR_ID IN\n" +
                                            "(SELECT PR_ID FROM BIDDING WHERE IS_APPROVED = 1 AND PURCHASING_OFFICER=" + UserId + " GROUP BY PR_ID) \n" +
                                            "ORDER BY PM.PR_ID DESC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> GetPrListForBidSubmitedByPrCode(int CompanyId, int UserId, string prCode, DBConnection dbConnection) {
            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.PR_CODE = '" + prCode + "' AND PM.PR_ID IN\n" +
                                            "(SELECT PR_ID FROM BIDDING WHERE IS_APPROVED = 1 AND PURCHASING_OFFICER=" + UserId + " GROUP BY PR_ID) \n" +
                                            "ORDER BY PM.PR_ID DESC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> GetPrListForBidSubmitedByDate(int CompanyId, int UserId, DateTime date, DBConnection dbConnection) {
            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE MONTH(PM.CREATED_DATETIME) =" + date.Month + " AND YEAR(PM.CREATED_DATETIME)=" + date.Year + " AND PM.PR_ID IN\n" +
                                            "(SELECT PR_ID FROM BIDDING WHERE IS_APPROVED = 1 AND PURCHASING_OFFICER=" + UserId + " GROUP BY PR_ID) \n" +
                                            "ORDER BY PM.CREATED_DATETIME ASC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PR_Master> FetchPRToEdit(int companyId, int userId, DBConnection dbConnection) {
            List<PR_Master> PrMasters = null;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PM\n" +
                                           " INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS CL ON PM.CREATED_BY=CL.USER_ID\n" +
                                           " INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP  " +
                                           " ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM " + dbLibrary + ".COMPANY_LOGIN a WHERE a.USER_ID = CL.USER_ID ) " +
                                           " WHERE PM.DEPARTMENT_ID =" + companyId + " AND PM.CREATED_BY=" + userId + " " +
                                           " AND PM.PR_IS_APPROVED = 0  ORDER BY PM.PR_ID DESC;";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrMasters = dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }

            for (int i = 0; i < PrMasters.Count; i++) {
                //getting PR Details for PR Master
                PrMasters[i].PrDetails = DAOFactory.CreatePR_DetailDAO().GetPrDetailsByPRid(PrMasters[i].PrId, companyId, dbConnection);

                for (int j = 0; j < PrMasters[i].PrDetails.Count; j++) {
                    PrMasters[i].PrDetails[j].PrDetailsStatusLogs = DAOFactory.CreatePRDetailsStatusLogDAO().GetPrDStatusByPrDId(PrMasters[i].PrDetails[j].PrdId, dbConnection);
                }

            }

            return PrMasters;
        }

        public List<PR_Details> GetOnlyPRDetails(int prId, int companyId, DBConnection dbConnection) {
            List<PR_Details> PrDetails = DAOFactory.CreatePR_DetailDAO().GetPrDetailsByPRid(prId, companyId, dbConnection);

            return PrDetails;
        }

        public List<PR_Details> GetPRDetails(int prId, int companyId, DBConnection dbConnection) {
            List<PR_Details> PrDetails = DAOFactory.CreatePR_DetailDAO().GetPrDetailsByPRid(prId, companyId, dbConnection);

            for (int j = 0; j < PrDetails.Count; j++) {
                PrDetails[j].PrDetailsStatusLogs = DAOFactory.CreatePRDetailsStatusLogDAO().GetPrDStatusByPrDId(prId, dbConnection);
            }

            return PrDetails;
        }

        public List<PR_Master> FetchApprovedPR(int companyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER as PM" +
                                            " INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS CL ON PM.CREATED_BY=CL.USER_ID\n" +
                                            " INNER JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME as SUB_DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP  " +
                                            " ON SUBDEP.SUB_DEPARTMENT_ID = (select SUB_DEPARTMENT_ID FROM " + dbLibrary + ".COMPANY_LOGIN a WHERE a.USER_ID = CL.USER_ID ) " +
                                            "  INNER JOIN  " + dbLibrary + ".PR_EXPENSE AS PREX ON PM.PR_ID = PREX.PR_ID" +
                                            " WHERE PM.IS_ACTIVE = '1' AND PM.DEPARTMENT_ID=" + companyId + " " +
                                            " AND PM.PR_IS_APPROVED= 1 AND PM.PR_IS_CONFIRMED_APPROVAL=0 AND PM.EXPENSE_TYPE ='Capital Expense' " +
                                            " AND PREX.IS_APPROVED =0";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }

        public List<PR_Master> AdvanceSearchPRForInquiry(int companyId, int searchBy, int categoryId, int subdepartmentId, string searchText, DBConnection dbConnection) {
            List<PR_Master> PrMaster = new List<PR_Master>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "[dbo].[PRAdvanceSearchForInquiry]";
            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", companyId);
            dbConnection.cmd.Parameters.AddWithValue("@searchBy", searchBy);
            dbConnection.cmd.Parameters.AddWithValue("@categoryId", categoryId);
            dbConnection.cmd.Parameters.AddWithValue("@subDepartmentId", subdepartmentId);
            dbConnection.cmd.Parameters.AddWithValue("@searchText", searchText);
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrMaster = dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);

            }
            if (PrMaster.Count > 0) {
                for (int x = 0; x < PrMaster.Count; ++x) {
                    PrMaster[x].PrDetails = DAOFactory.CreatePR_DetailDAO().GetPrDetailsByPRid(PrMaster[x].PrId, companyId, dbConnection);
                    PrMaster[x].Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForPRInquiry(PrMaster[x].PrId, dbConnection);

                    for (int j = 0; j < PrMaster[x].Bids.Count; j++) {
                        //getting bid items for bids in Pr Master
                        PrMaster[x].Bids[j].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(PrMaster[x].Bids[j].BidId, companyId, dbConnection);

                        PrMaster[x].Bids[j].POsCreated = new List<POMaster>();
                        PrMaster[x].Bids[j].GRNsCreated = new List<GrnMaster>();

                        //getting quotations submitted for bids in Pr master
                        PrMaster[x].Bids[j].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(PrMaster[x].Bids[j].BidId, dbConnection);


                        for (int k = 0; k < PrMaster[x].Bids[j].SupplierQuotations.Count; k++) {
                            //getting quotation items for quotations
                            PrMaster[x].Bids[j].SupplierQuotations[k].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(PrMaster[x].Bids[j].SupplierQuotations[k].QuotationId, companyId, dbConnection);

                            //getting all the pos created for the bid using quotationId
                            for (int l = 0; l < PrMaster[x].Bids[j].SupplierQuotations.Count; l++) {
                                PrMaster[x].Bids[j].POsCreated.AddRange(DAOFactory.createPOMasterDAO().GetPoMastersForPrInquiryByQuotationId(PrMaster[x].Bids[j].SupplierQuotations[l].QuotationId, dbConnection));
                            }

                            //retrieving po details and getting all the grns created for the bid using po id 
                            for (int l = 0; l < PrMaster[x].Bids[j].POsCreated.Count; l++) {
                                PrMaster[x].Bids[j].POsCreated[l].PoDetails = DAOFactory.createPODetailsDAO().GetPoDetailsForPrInquiry(PrMaster[x].Bids[j].POsCreated[l].PoID, companyId, dbConnection);

                                PrMaster[x].Bids[j].GRNsCreated.AddRange(DAOFactory.createGrnDAO().GetGrnMastersForPrInquiryByPoId(PrMaster[x].Bids[j].POsCreated[l].PoID, dbConnection));
                            }

                            //retrieving grn details
                            for (int l = 0; l < PrMaster[x].Bids[j].GRNsCreated.Count; l++) {
                                PrMaster[x].Bids[j].GRNsCreated[l]._GrnDetailsList = DAOFactory.createGRNDetailsDAO().GetGrnDetailsForPrInquiry(PrMaster[x].Bids[j].GRNsCreated[l].GrnId, companyId, dbConnection);
                            }
                        }
                    }
                }
            }

            return PrMaster;
        }



        public PrMasterV2 getPRMasterDetailByPrId(int prID, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER WHERE PR_ID = " + prID + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }
        }

        public List<PrMasterV2> GetPrListForViewAllPr(int CompanyId, DBConnection dbConnection) {
            List<PrMasterV2> PrMasters = null;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PM.DEPARTMENT_ID =" + CompanyId + " ORDER BY PM.PR_ID DESC;";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrMasters = dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

            for (int i = 0; i < PrMasters.Count; i++) {
                //getting PR Details for PR Master
                PrMasters[i].PrDetails = DAOFactory.CreatePrDetailsDAOV2().GetPrDetailsForEdit(PrMasters[i].PrId, dbConnection);

                for (int j = 0; j < PrMasters[i].PrDetails.Count; j++) {
                    PrMasters[i].PrDetails[j].PrDetailsStatusLogs = DAOFactory.CreatePRDetailsStatusLogDAO().GetPrDStatusByPrDId(PrMasters[i].PrDetails[j].PrdId, dbConnection);
                }

            }

            return PrMasters;
        }

        public List<PrMasterV2> GetPrListForViewMyPr(int CompanyId, int UserId, DBConnection dbConnection) {
            List<PrMasterV2> PrMasters = null;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                           " WHERE PM.DEPARTMENT_ID =" + CompanyId + " AND PM.CREATED_BY=" + UserId + " ORDER BY PM.PR_ID DESC;";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrMasters = dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

            for (int i = 0; i < PrMasters.Count; i++) {
                //getting PR Details for PR Master
                PrMasters[i].PrDetails = DAOFactory.CreatePrDetailsDAOV2().GetPrDetailsForEdit(PrMasters[i].PrId, dbConnection);

                for (int j = 0; j < PrMasters[i].PrDetails.Count; j++) {
                    PrMasters[i].PrDetails[j].PrDetailsStatusLogs = DAOFactory.CreatePRDetailsStatusLogDAO().GetPrDStatusByPrDId(PrMasters[i].PrDetails[j].PrdId, dbConnection);
                }

            }

            return PrMasters;
        }
        public int ApproveBid(int BidId, int CompanyId, string remark, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE BIDDING SET IS_TABULATION_APPROVAL = 1 , TABULATION_REVIEW_APPROVAL_REMARK = '" + remark + "' WHERE BID_ID = " + BidId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int GetParentPrforCoverinPr(int PoId, DBConnection dbConnection) {
            int PrId = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PR_ID FROM PR_MASTER WHERE PR_ID = (SELECT PR_ID FROM BIDDING WHERE BID_ID = (SELECT BID_ID FROM SUPPLIER_QUOTATION WHERE QUOTATION_ID = (SELECT QUOTATION_ID FROM PO_MASTER WHERE PO_ID = " + PoId + ") ) ) ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            PrId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            return PrId;
        }
        public int ApproveBidTabImports(int BidId, int CompanyId, string ProceedRemark, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE BIDDING SET IS_TABULATION_APPROVAL = 1 , PROCEED_REMARK ='" + ProceedRemark + "'  WHERE BID_ID = " + BidId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int ApproveBidTab(int BidId, int CompanyId, string remark, string ProceedRemark, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE BIDDING SET IS_TABULATION_APPROVAL = 1 , TABULATION_REVIEW_APPROVAL_REMARK = '" + remark + "', PROCEED_REMARK ='" + ProceedRemark + "'  WHERE BID_ID = " + BidId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int RejectBid(int BidId, int CompanyId, string remark, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE BIDDING SET IS_TABULATION_APPROVAL = 2 , TABULATION_REVIEW_APPROVAL_REMARK = '" + remark + "' WHERE BID_ID = " + BidId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PrMasterV2> GetPrRejectedQuotationTabulationSheet(List<int> selectionPendingBidIds, int companyId, int userId, DBConnection dbConnection) {

            List<PrMasterV2> PrMasters = new List<PrMasterV2>();
            string sql = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        " WHERE PM.COMPANY_ID=" + companyId + " " +
                        " AND PM.PR_ID IN(SELECT PR_ID FROM [BIDDING] WHERE BID_ID IN (" + string.Join(",", selectionPendingBidIds) + ") " +
                        "AND PURCHASING_OFFICER = " + userId + "\n" +
                        "GROUP BY PR_ID);";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrMasters = dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

            //Fetching bids for PRs
            for (int i = 0; i < PrMasters.Count; i++) {
                PrMasters[i].Bids = DAOFactory.CreateBiddingDAO().GetRejectedBids(PrMasters[i].PrId, companyId, dbConnection);

                //Fetching Bid Items for bids
                for (int j = 0; j < PrMasters[i].Bids.Count; j++) {
                    PrMasters[i].Bids[j].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetRejectedBidsItems(PrMasters[i].Bids[j].BidId, companyId, dbConnection);
                    PrMasters[i].Bids[j].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(PrMasters[i].Bids[j].BidId, dbConnection);
                    for (int t = 0; t < PrMasters[i].Bids[j].SupplierQuotations.Count; ++t) {
                        PrMasters[i].Bids[j].SupplierQuotations[t].QuotationItems = new List<SupplierQuotationItem>();
                        PrMasters[i].Bids[j].SupplierQuotations[t].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(PrMasters[i].Bids[j].SupplierQuotations[t].QuotationId, companyId, dbConnection);
                        if (PrMasters[i].Bids[j].SupplierQuotations[t].QuotationItems.Where(x => x.IsQuotationItemApproved == 2).ToList().Count > 0) {
                            PrMasters[i].Bids[j].SupplierQuotations[t].IsQuotationTabulationApproved = 2;
                        }
                    }
                }
            }
            return PrMasters;
        }

        public void UpdateTabulationReviewApproval(int bidId, int companyId, DBConnection dbConnection) {
            string sql = "UPDATE " + dbLibrary + ".BIDDING " +
                         " SET IS_TABULATION_APPROVAL = 0, TABULATION_REVIEW_APPROVAL_REMARK = '' " +
                         "WHERE BID_ID = " + bidId + " ";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PrMasterV2> GetPrListForQuotationApproval(List<int> SelectionPendingBidIds, DBConnection dbConnection) {
            string sql = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME FROM PR_MASTER AS PM \n" +
                        "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                        "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                        "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                        "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                        "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                        "WHERE PM.IS_TABULATION_REVIEW_APPROVED = 0 AND PM.PR_ID IN (\n" +
                         "SELECT PR_ID FROM BIDDING WHERE BID_ID IN(" + string.Join(",", SelectionPendingBidIds) + ") GROUP BY PR_ID)";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }

        }

        public string GetQuotationForbyPrCode(int companyId, string prCode, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT REQUIRED_FOR FROM " + dbLibrary + ".PR_MASTER WHERE COMPANY_ID = " + companyId + " AND PR_CODE = '" + prCode + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteScalar().ToString();
        }

        public PR_Master GetPrForQuotationComparison(int PrId, int CompanyId, DBConnection dbConnection) {
            PR_Master prMaster = null;

            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PM\n" +
                "LEFT JOIN (SELECT CREATED_BY, MRN_APPROVAL_BY, MRN_CODE, MRN_ID FROM MRN_MASTER) AS MRN ON MRN.MRN_ID = PM.MRN_ID " +
                "LEFT JOIN (SELECT FIRST_NAME AS MRN_CREATED_BY_NAME, USER_ID FROM COMPANY_LOGIN) AS CCL ON CCL.USER_ID = MRN.CREATED_BY " +
                "LEFT JOIN (SELECT FIRST_NAME AS APPROVED_BY_NAME, USER_ID FROM COMPANY_LOGIN) AS ACL ON ACL.USER_ID = MRN.MRN_APPROVAL_BY " +
                "LEFT JOIN(SELECT WAREHOUSE_ID, LOCATION, PHONE_NO, ADDRESS FROM WAREHOUSE) AS W ON PM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT USER_ID,USER_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS CL ON PM.CREATED_BY=CL.USER_ID\n" +
                "INNER JOIN (SELECT DEPARTMENT_ID,DEPARTMENT_NAME FROM COMPANY_DEPARTMENT) AS CD ON PM.COMPANY_ID=CD.DEPARTMENT_ID\n" +
                "WHERE PM.PR_ID=" + PrId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PR_Master>(dbConnection.dr);
            }

            if (prMaster != null) {

                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForQuotationComparison(prMaster.PrId, dbConnection);

                for (int b = 0; b < prMaster.Bids.Count; b++) {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[b].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[b].BidId, CompanyId, dbConnection);

                    DAOFactory.CreateBiddingItemDAO().GetLastPurchaseDetails(prMaster.Bids[b].BiddingItems, CompanyId, dbConnection);

                    //for (int i = 0; i < prMaster.Bids[b].BiddingItems.Count; i++)
                    //{
                    //    prMaster.Bids[b].BiddingItems[i].SupplierQuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItemsByBidItemId(prMaster.Bids[b].BiddingItems[i].BiddingItemId, CompanyId, dbConnection);
                    //}

                    //getting quotations submitted for bids in Pr master
                    prMaster.Bids[b].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(prMaster.Bids[b].BidId, dbConnection);

                    for (int c = 0; c < prMaster.Bids[b].SupplierQuotations.Count; c++) {
                        //getting supplier details for quotations
                        prMaster.Bids[b].SupplierQuotations[c].SupplierRating = DAOFactory.createSupplierRatingDAO().GetSupplierRatingBySupplierIdAndCompanyId(prMaster.Bids[b].SupplierQuotations[c].SupplierId, CompanyId, dbConnection);


                        //getting quotation items for quotations
                        prMaster.Bids[b].SupplierQuotations[c].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(prMaster.Bids[b].SupplierQuotations[c].QuotationId, CompanyId, dbConnection);


                        prMaster.Bids[b].SupplierQuotations[c].QuotationImages = DAOFactory.createQuotationImageDAO().GetQuotationImages(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);
                        prMaster.Bids[b].SupplierQuotations[c].UploadedFiles = DAOFactory.CreateSupplierBiddingFileUploadDAO().GetUploadedFiles(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);

                        //getting boms, images and files uploaded for quotation items
                        for (int d = 0; d < prMaster.Bids[b].SupplierQuotations[c].QuotationItems.Count; d++) {
                            prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].SupplierBOMs = DAOFactory.CreateSupplierBOMDAO().GetSupplierBom(prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].QuotationItemId, dbConnection);

                        }
                    }
                }

            }

            return prMaster;
        }
        public PR_Master GetPrForImportTabulationReview(int PrId, int CompanyId, DBConnection dbConnection) {
            PR_Master prMaster = null;

            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PM\n" +
                "LEFT JOIN (SELECT CREATED_BY, MRN_APPROVAL_BY, MRN_CODE, MRN_ID FROM MRN_MASTER) AS MRN ON MRN.MRN_ID = PM.MRN_ID " +
                "LEFT JOIN (SELECT FIRST_NAME AS MRN_CREATED_BY_NAME, USER_ID FROM COMPANY_LOGIN) AS CCL ON CCL.USER_ID = MRN.CREATED_BY " +
                "LEFT JOIN (SELECT FIRST_NAME AS APPROVED_BY_NAME, USER_ID FROM COMPANY_LOGIN) AS ACL ON ACL.USER_ID = MRN.MRN_APPROVAL_BY " +
                "LEFT JOIN(SELECT WAREHOUSE_ID, LOCATION, PHONE_NO, ADDRESS FROM WAREHOUSE) AS W ON PM.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT USER_ID,USER_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS CL ON PM.CREATED_BY=CL.USER_ID\n" +
                "INNER JOIN (SELECT DEPARTMENT_ID,DEPARTMENT_NAME FROM COMPANY_DEPARTMENT) AS CD ON PM.COMPANY_ID=CD.DEPARTMENT_ID\n" +
                "WHERE PM.PR_ID=" + PrId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PR_Master>(dbConnection.dr);
            }

            if (prMaster != null) {

                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForQuotationComparison(prMaster.PrId, dbConnection);

                for (int b = 0; b < prMaster.Bids.Count; b++) {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[b].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[b].BidId, CompanyId, dbConnection);

                    DAOFactory.CreateBiddingItemDAO().GetLastPurchaseDetails(prMaster.Bids[b].BiddingItems, CompanyId, dbConnection);

                    //for (int i = 0; i < prMaster.Bids[b].BiddingItems.Count; i++)
                    //{
                    //    prMaster.Bids[b].BiddingItems[i].SupplierQuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItemsByBidItemId(prMaster.Bids[b].BiddingItems[i].BiddingItemId, CompanyId, dbConnection);
                    //}

                    //getting quotations submitted for bids in Pr master
                    prMaster.Bids[b].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(prMaster.Bids[b].BidId, dbConnection);

                    for (int c = 0; c < prMaster.Bids[b].SupplierQuotations.Count; c++) {
                        //getting supplier details for quotations
                        prMaster.Bids[b].SupplierQuotations[c].SupplierRating = DAOFactory.createSupplierRatingDAO().GetSupplierRatingBySupplierIdAndCompanyId(prMaster.Bids[b].SupplierQuotations[c].SupplierId, CompanyId, dbConnection);


                        //getting quotation items for quotations
                        prMaster.Bids[b].SupplierQuotations[c].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationImportItems(prMaster.Bids[b].SupplierQuotations[c].QuotationId, CompanyId, dbConnection);
                        prMaster.Bids[b].SupplierQuotations[c].ImportQuotationItemList = DAOFactory.createImportsDAO().GetAllImportQuotations(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);


                        prMaster.Bids[b].SupplierQuotations[c].QuotationImages = DAOFactory.createQuotationImageDAO().GetQuotationImages(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);
                        prMaster.Bids[b].SupplierQuotations[c].UploadedFiles = DAOFactory.CreateSupplierBiddingFileUploadDAO().GetUploadedFiles(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);

                        //getting boms, images and files uploaded for quotation items
                        for (int d = 0; d < prMaster.Bids[b].SupplierQuotations[c].QuotationItems.Count; d++) {
                            prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].SupplierBOMs = DAOFactory.CreateSupplierBOMDAO().GetSupplierBom(prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].QuotationItemId, dbConnection);

                        }
                    }
                }

            }

            return prMaster;
        }

        public PrMasterV2 GetPrForQuotationComparisonImports(int PrId, int CompanyId, List<int> SelectionPendingBidIds, DBConnection dbConnection) {
            PrMasterV2 prMaster = null;

            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PM.*,WH.LOCATION AS WAREHOUSE_NAME, CL.FIRST_NAME AS CREATED_BY_NAME, ICM.CATEGORY_NAME AS PR_CATEGORY_NAME,MRNM.MRN_CODE,SD.DEPARTMENT_NAME AS SUB_DEPARTMENT_NAME,PRD.MEASUREMENT_ID as MEASUREMENT_ID FROM PR_MASTER AS PM \n" +
                                            "INNER JOIN WAREHOUSE AS WH ON PM.WAREHOUSE_ID = WH.WAREHOUSE_ID \n" +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON PM.CREATED_BY = CL.USER_ID \n" +
                                            "INNER JOIN ITEM_CATEGORY_MASTER AS ICM ON PM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n" +
                                            "LEFT JOIN MRN_MASTER AS MRNM ON PM.MRN_ID = MRNM.MRN_ID \n" +
                                            "LEFT JOIN PR_DETAIL AS PRD ON PM.PR_ID=PRD.PR_ID \n" +
                                            "LEFT JOIN SUB_DEPARTMENT AS SD ON MRNM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID \n" +
                                            "WHERE PM.PR_ID=" + PrId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                prMaster = dataAccessObject.GetSingleOject<PrMasterV2>(dbConnection.dr);
            }

            if (prMaster != null) {

                //getting Bids for PR Master
                prMaster.Bids = DAOFactory.CreateBiddingDAO().GetAllBidsForQuotationComparison(prMaster.PrId, SelectionPendingBidIds, dbConnection);

                for (int b = 0; b < prMaster.Bids.Count; b++) {
                    //getting bid items for bids in Pr Master
                    prMaster.Bids[b].BiddingItems = DAOFactory.CreateBiddingItemDAO().GetAllBidItems(prMaster.Bids[b].BidId, CompanyId, dbConnection);

                    DAOFactory.CreateBiddingItemDAO().GetLastPurchaseDetails(prMaster.Bids[b].BiddingItems, CompanyId, dbConnection);

                    //DAOFactory.CreateBiddingItemDAO().GetLastPurchaseDetails(prMaster.Bids[b].BiddingItems, CompanyId, dbConnection);

                    //getting quotations submitted for bids in Pr master
                    prMaster.Bids[b].SupplierQuotations = DAOFactory.createSupplierQuotationDAO().GetSupplierQuotations(prMaster.Bids[b].BidId, dbConnection);

                    //gettig tabulaion for the bids
                    prMaster.Bids[b].Tabulations = DAOFactory.CreateTabulationMasterDAO().GetTabulationsByBidId(prMaster.Bids[b].BidId, dbConnection);
                    if (prMaster.Bids[b].Tabulations != null) {
                        for (int c = 0; c < prMaster.Bids[b].Tabulations.Count; c++) {
                            prMaster.Bids[b].Tabulations[c].TabulationDetails = DAOFactory.CreateTabulationDetailDAO().GetTabulationDetailsByTabulationId(prMaster.Bids[b].Tabulations[c].TabulationId, dbConnection);
                        }
                    }

                    for (int c = 0; c < prMaster.Bids[b].SupplierQuotations.Count; c++) {
                        //getting supplier details for quotations
                        prMaster.Bids[b].SupplierQuotations[c].SupplierDetails = DAOFactory.createSupplierDAO().GetSupplierBySupplierId(prMaster.Bids[b].SupplierQuotations[c].SupplierId, dbConnection);

                        //getting quotation items for quotations 
                        //prMaster.Bids[b].SupplierQuotations[c].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationItems(prMaster.Bids[b].SupplierQuotations[c].QuotationId, CompanyId, dbConnection);
                        prMaster.Bids[b].SupplierQuotations[c].QuotationItems = DAOFactory.CreateSupplierQuotationItemDAO().GetAllQuotationImportItems(prMaster.Bids[b].SupplierQuotations[c].QuotationId, CompanyId, dbConnection);

                        //Get Currency Details
                        prMaster.Bids[b].SupplierQuotations[c].objCurrencyDetails = DAOFactory.createImportsDAO().GetCurrencyDetailsforSupplier(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);

                        //Get Import Details
                        prMaster.Bids[b].SupplierQuotations[c].objImportCalucationDetails = DAOFactory.createImportsDAO().GetImportDetails(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);

                        prMaster.Bids[b].SupplierQuotations[c].QuotationImages = DAOFactory.createQuotationImageDAO().GetQuotationImages(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);
                        prMaster.Bids[b].SupplierQuotations[c].UploadedFiles = DAOFactory.CreateSupplierBiddingFileUploadDAO().GetUploadedFiles(prMaster.Bids[b].SupplierQuotations[c].QuotationId, dbConnection);

                        //getting boms, images and files uploaded for quotation items
                        for (int d = 0; d < prMaster.Bids[b].SupplierQuotations[c].QuotationItems.Count; d++) {
                            prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].SupplierBOMs = DAOFactory.CreateSupplierBOMDAO().GetSupplierBom(prMaster.Bids[b].SupplierQuotations[c].QuotationItems[d].QuotationItemId, dbConnection);

                        }
                    }
                }

            }

            return prMaster;
        }

        public int UpdateTerminatedPRMaster(int prId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF NOT EXISTS (SELECT * FROM PR_DETAIL WHERE PR_ID= " + prId + " AND CURRENT_STATUS !=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='TERM')) " +
                                            "BEGIN " +

                                            "   UPDATE PR_MASTER SET CURRENT_STATUS = (SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='TER') WHERE PR_ID = " + prId + " " +
                                            "   SELECT 1 " +
                                            "END " +
                                            "ELSE " +
                                            "BEGIN " +
                                            "   SELECT 1 " +
                                            "END ";
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public List<PrMasterV2> FetchPrByPrCode(int CompanyId, string prCode, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".PR_MASTER PRM " +
                "LEFT JOIN (SELECT USER_ID, USER_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN WHERE DEPARTMENT_ID = " + CompanyId + ") AS CL ON PRM.CREATED_BY = CL.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION AS WAREHOUSE_NAME FROM WAREHOUSE ) AS W ON W.WAREHOUSE_ID = PRM.WAREHOUSE_ID " +
                 "LEFT JOIN (SELECT PR_STATUS_ID, STATUS_NAME FROM DEF_PR_STATUS) AS ST ON ST.PR_STATUS_ID = PRM.CURRENT_STATUS " +
                "WHERE PRM.PR_CODE = '" + prCode + "' ";


            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }
        public List<PrMasterV2> FetchPrByDate(int companyId, DateTime ToDate, DateTime FromDate, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();


            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".PR_MASTER PRM " +
                "LEFT JOIN (SELECT USER_ID, USER_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN WHERE DEPARTMENT_ID = " + companyId + ") AS CL ON PRM.CREATED_BY = CL.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID, LOCATION AS WAREHOUSE_NAME FROM WAREHOUSE ) AS W ON W.WAREHOUSE_ID = PRM.WAREHOUSE_ID " +
                 "LEFT JOIN (SELECT PR_STATUS_ID, STATUS_NAME FROM DEF_PR_STATUS) AS ST ON ST.PR_STATUS_ID = PRM.CURRENT_STATUS " +
                "WHERE PRM.CREATED_DATETIME >= '" + FromDate + "' AND PRM.CREATED_DATETIME <= '" + ToDate + "' ";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrMasterV2>(dbConnection.dr);
            }
        }

        public int SavePRMaster(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string terms, int SubDepartmentId, int CategoryId, int warehouseId, DBConnection dbConnection) {
            int PrId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER ";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0) {
                PrId = 001;
            }
            else {
                dbConnection.cmd.CommandText = "SELECT MAX (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER ";
                PrId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            string PRCode = string.Empty;

            if (count == 0) {
                PRCode = "PR" + 1;
            }
            else {
                dbConnection.cmd.CommandText = "SELECT COUNT (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER WHERE DEPARTMENT_ID = " + DepartmentId + "";
                var count01 = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                PRCode = "PR" + count01;
            }

            //dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_MASTER (PR_ID, PR_CODE, DEPARTMENT_ID, DATE_OF_REQUEST, QUOTATION_FOR, OUR_REFERENCE, REQUESTED_BY, CREATED_DATETIME, CREATED_BY, UPDATED_DATETIME, UPDATED_BY, IS_ACTIVE, PR_IS_APPROVED, PR_IS_APPROVED_OR_REJECT_BY, PR_IS_APPROVED_OR_REJECT_DATE, PR_IS_APPROVED_FOR_BID, PR_IS_APPROVED_OR_REJECT_FOR_BID_BY,PR_IS_APPROVED_OR_REJECT_FOR_BID_DATE,IS_PO_RAISED,BASE_PR_ID, PR_TYPE_ID, EXPENSE_TYPE, REF_01, REF_02, REF_03, REF_04, REF_05, REF_06,BID_TERMS_CONDITION,SUB_DEPARTMENT_ID) VALUES" +
            //            "( " + PrId + ", '" + PRCode + "' , " + DepartmentId + ", '" + DateOfRequest + "', '" + QuotationFor + "', '" + OurReference + "', '" + RequestedBy + "', '" + CraeatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDateTime + "', '" + UpdatedBy + "', " + IsActive + ", " + PRAppoved + ", '" + PRApprovedOrRejectedBy + "', '" + PRApprovedOrRejectedDate + "', " + PRIsApproveForBid + ", '" + PRIsApprovedOrRejectedBy + "', '" + PRIsApprovedOrRejectedDate + "'," + 0 + "," + BasePrid + ", " + prTypeId + ",'" + expenseType + "','" + refNo01 + "','" + refNo02 + "','" + refNo03 + "','" + refNo04 + "','" + refNo05 + "','" + refNo06 + "','" + terms.ProcessString() + "'," + SubDepartmentId + ");";
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_MASTER (PR_ID, PR_CODE, DEPARTMENT_ID, DATE_OF_REQUEST, QUOTATION_FOR, OUR_REFERENCE, REQUESTED_BY, CREATED_DATETIME, CREATED_BY, UPDATED_DATETIME, UPDATED_BY, IS_ACTIVE, PR_IS_APPROVED, PR_IS_APPROVED_OR_REJECT_BY, PR_IS_APPROVED_OR_REJECT_DATE, PR_IS_APPROVED_FOR_BID, PR_IS_APPROVED_OR_REJECT_FOR_BID_BY,PR_IS_APPROVED_OR_REJECT_FOR_BID_DATE,IS_PO_RAISED,BASE_PR_ID, PR_TYPE_ID, EXPENSE_TYPE, REF_01, REF_02, REF_03, REF_04, REF_05, REF_06,BID_TERMS_CONDITION,CATEGORY_ID, WAREHOUSE_ID) VALUES" +
                        "( " + PrId + ", '" + PRCode + "' , " + DepartmentId + ", '" + DateOfRequest + "', '" + QuotationFor + "', '" + OurReference + "', '" + RequestedBy + "', '" + CraeatedDateTime + "', '" + CreatedBy + "', '" + UpdatedDateTime + "', '" + UpdatedBy + "', " + IsActive + ", " + PRAppoved + ", '" + PRApprovedOrRejectedBy + "', '" + PRApprovedOrRejectedDate + "', " + PRIsApproveForBid + ", '" + PRIsApprovedOrRejectedBy + "', '" + PRIsApprovedOrRejectedDate + "'," + 0 + "," + BasePrid + ", " + prTypeId + ",'" + expenseType + "','" + refNo01 + "','" + refNo02 + "','" + refNo03 + "','" + refNo04 + "','" + refNo05 + "','" + refNo06 + "','" + terms.ProcessString() + "'," + CategoryId + "," + warehouseId + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();

            return PrId;
        }

        public int CreateCoveringPr(int PoId, int ParentPrId, int UserId, int SupplierId, int QuotationId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "CLONE_COVERING_PR";

            dbConnection.cmd.Parameters.AddWithValue("@PO_ID", PoId);
            dbConnection.cmd.Parameters.AddWithValue("@PARENT_PR_ID", ParentPrId);
            dbConnection.cmd.Parameters.AddWithValue("@CLONED_ON", LocalTime.Now);
            dbConnection.cmd.Parameters.AddWithValue("@CLONED_BY", UserId);
            dbConnection.cmd.Parameters.AddWithValue("@EXPIRED_ON", LocalTime.Now.AddDays(3));
            dbConnection.cmd.Parameters.AddWithValue("@SUPPLIER_ID", SupplierId);
            dbConnection.cmd.Parameters.AddWithValue("@SUP_QUOTATION_ID", QuotationId);

            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            int X = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            return X;
        }

    }

}
