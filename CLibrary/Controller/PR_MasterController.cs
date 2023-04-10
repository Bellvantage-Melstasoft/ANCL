using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;
using System.Data;

namespace CLibrary.Controller
{
    public interface PR_MasterController
    {
        int SavePRMaster(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string prProcedure, string purchaseType, DateTime requiredDate, string MRNReferenceNo, int itemCatId);
        string FetchPRCode(int DepartmentId);

        //Fetch ALL
        List<PrMasterV2> FetchPrALl();

        //End Of Fetch All
        List<PR_Master> FetchApprovePRDataByDeptId(int DepartmentId);
        PR_Master FetchApprovePRDataByDeptIdAndPRId(int DepartmentId, int PrId);
        int UpdateIsApprovePR(int DepartmentId, int PrId, int Status, int PRApprovedUserId, int isActive, string RejectedReason);
        List<PR_Master> FetchApprovePRDataByDeptIdApproved(int DepartmentId);
        int UpdateOprnForBid(int DepartmentId, int PrId, int BidOpeningStatus, string BidTermCondition, int ApprovedBy);
        PR_Master FetchApprovePRDataByPRId(int PrId);
        List<PR_Master> GetPrMasterListByDaterange(int departmentid, DateTime startdate, DateTime enddate);
        int UpdatePORaised(int PrId);
        int UpdatePRMaster(int PrId, int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string prProcedure, string purchaseType, DateTime requiredDate, string MRNReferenceNo);
        List<PR_Master> FetchPRToEdit(int companyId, int userId);
        List<PR_Master> FetchRejectedPr(int DepartmentId);
        PR_Master FetchRejectPR(int PrId);
        List<PR_Master> FetchDetailsToEdit(int DepartmentId);
        List<PR_Master> FetchYetSubmitPR(int Department);
        List<PR_Master> FetchTotalBidforChart(int Department, int year);
        List<PR_Master> FetchTotalPR(int Department);
        int countTotalPr(int DepartmentId);
        int countApprovedPr(int DepartmentId);
        int countRejectedPr(int DepartmentId);
        int countPendingPr(int DepartmentId);
        List<PR_Master> countTotalPrtochart(int DepartmentId, int year);

        int GetDetailsByPrCode(int DepartmentId, string PrCode);


        //--------2018-09-17 PR Reports View
        List<PR_Master> FetchApprovePRDataByDeptIdReports(int DepartmentId);

        List<PR_Master> FetchApprovedPRForConfirmation(int Department);
        List<PR_Master> FetchApprovedPR(int companyId);
        int ConfirmOrDenyPRApproval(int prId, int confirm);

        string SavePRMasterAtCloning(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string prProcedure, string purchaseType, DateTime requiredDate, string MRNReferenceNo);


        //New Methods By Salman created on 2019-01-17
        List<PrMasterV2> GetPrListForBidSubmission(int CompanyId);
        List<PrMasterV2> GetPrListForBidSubmissionWithItem(int CompanyId);
        PrMasterV2 GetPrForBidSubmission(int PrId, int CompanyId);
        List<PrMasterV2> GetPrListForBidApproval(int CompanyId, int LoggedInUser);
        PrMasterV2 GetPrForBidApproval(int PrId, int CompanyId);
        List<PR_Master> GetPrListForQuotationComparison(int CompanyId);
        PrMasterV2 GetPrForQuotationComparison(int PrId, int CompanyId, List<int> SelectionPendingBidIds);
        List<PrMasterV2> GetPrListForQuotationApproval(int CompanyId, List<int> TabulationIds);
        PrMasterV2 GetPrForQuotationApproval(int PrId, int CompanyId, List<int> TabulationIds, int UserId, int DesignationId);
        List<PrMasterV2> GetPrListForQuotationConfirmation(int CompanyId, List<int> TabulationIds);
        PrMasterV2 GetPrForQuotationConfirmation(int PrId, int CompanyId, List<int> TabulationIds, int UserId, int DesignationId);
        List<PrMasterV2> GetPrListForManualQuotationSubmission(int CompanyId, int UserId);
        List<PrMasterV2> GetPrListForManualQuotationSubmissionWithItem(int CompanyId, int UserId);
        List<PrMasterV2> GetPrListForPoCreation(int CompanyId, int UserId);
        PrMasterV2 GetPrForPoCreation(int PrId, int CompanyId, int UserId);
        string SaveMRNtoPR(int mrnId, int userId, int companyId);
        List<PrMasterV2> GetPrListForViewAllPr(int CompanyId);
        List<PrMasterV2> GetPrListForViewMyPr(int CompanyId, int UserId);

        int ApproveBid(int BidId, int CompanyId, string remark);
        int RejectBid(int BidId, int CompanyId, string remark, List<SupplierQuotationItem> quotationList);
        int ApproveBidTabImports(int BidId, int CompanyId, string ProceedRemark);


        PrMasterV2 getPRMasterDetailByPrId(int PrID);

        PrMasterV2 GetPrForBidSubmissionView(int PrId, int CompanyId);
        List<PR_Master> FetchAllPR(int Department);
        PR_Master SearchPRForInquiryByPrId(int PrId, int CompanyId);
        //   List<PR_Master> GetPRListForPrInquiry(int CompanyId);

        List<PR_Master> GetPRtobeApprovedForAdvanceSearch(int companyId, int categoryId, int subDepartmentID, int serchtype, string searchkey, string usertype);
        List<PR_Master> GetPRtobeApprovedforBiddingForAdvanceSearch(int companyId, int categoryId, int subDepartmentID, int serchtype, string searchkey, string usertype);
        List<PR_Master> GetQuotationBidddingrAdvanceSearch(int companyId, int categoryId, int supplierId, int serchtype, string searchkey, string usertype);
        List<PR_Master> GetPrListForDocumentUploadPoCreation(int CompanyId);
        int CheckfileUploadedofteccommit(int bidId, int qutationId);

        List<PR_Master> AdvanceSearchPRForInquiry(int companyId, int searchBy, int categoryId, int subdepartmentId, string searchText);

        List<int> GetPRCountForDashboard(int CompanyId, int yearsearch, int purchaseType);

        DataTable GetPRCountForDashBoard();
        List<PrMasterV2> GetPrListForBidSubmited(int CompanyId, int UserId);
        List<PR_Details> GetPRDetails(int PrId, int CompanyId);
        List<PR_Details> GetOnlyPRDetails(int PrId, int CompanyId);

        List<PrMasterV2> GetPrRejectedQuotationTabulationSheet(List<int> selectionPendingBidIds, int companyId, int userId);
        void UpdateTabulationReviewApproval(int BidId, int companyId);

        List<PrMasterV2> GetPrListForQuotationApproval(List<int> SelectionPendingBidIds);

        PR_Master GetPrForQuotationComparison(int PrId, int CompanyId);
        List<PrMasterV2> GetPrListForQuotationComparisonOld(List<int> SelectionPendingBidIds);//Reorder function stock by Pasindu 2020/04/29
        List<string> GetPrCodesByPrIds(List<int> PrIds);
        int SavePRMasterV2(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string terms, int SubDepartmentId, int CategoryId, int warehouseId);
        List<PrMasterV2> GetPrListForQuotationComparisonBid(List<int> SelectionPendingBidIds);

        PrMasterV2 GetPrForQuotationComparisonImports(int PrId, int CompanyId, List<int> SelectionPendingBidIds);
        int UpdateTerminatedPRMaster(int prId);
        List<PrMasterV2> GetPrListForBidSubmissionByPrCode(int CompanyId, string PrCode);
        List<PrMasterV2> GetPrListForBidSubmissionByDate(int CompanyId, DateTime Date);
        List<PrMasterV2> GetPrListForBidApprovalByDate(int CompanyId, int LoggedInUser, DateTime date);
        List<PrMasterV2> GetPrListForBidApprovalByPrCode(int CompanyId, int LoggedInUser, string PrCode);
        List<PrMasterV2> GetPrListForBidSubmitedByPrCode(int CompanyId, int UserId, string PrCode);
        List<PrMasterV2> GetPrListForBidSubmitedByDate(int CompanyId, int UserId, DateTime date);
        List<PrMasterV2> GetPrListForRejectedBids(int CompanyId, int UserId);
        List<PrMasterV2> FetchPrByPrCode(int CompanyId, string prCode);
        List<PrMasterV2> FetchPrByDate(int companyId, DateTime ToDate, DateTime FromDate);
        int SavePRMaster(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string terms, int SubDepartmentId, int CategoryId, int warehouseId);
        List<PrMasterV2> GetPrListForQuotationComparisonReviw(int CompanyId);
        List<PrMasterV2> GetPrListForQuotationComparisonReviwWithItem(int CompanyId);
        List<PrMasterV2> GetPrListForQuotationRejected(int CompanyId);
        List<PrMasterV2> GetPrListForQuotationAppRejected(int CompanyId);
        PrMasterV2 GetPrForQuotationApprovalRej(int PrId, int CompanyId, int UserId, int DesignationId);
        PrMasterV2 GetPrForQuotationConfirmationRej(int PrId, int CompanyId, int UserId, int DesignationId);
        List<PrMasterV2> GetPrListForQuotationComparisonReviwByDate(int CompanyId, DateTime Date);
        List<PrMasterV2> GetPrListForQuotationComparisonReviwByPrCode(int CompanyId, string Code);
        int ApproveBidTab(int BidId, int CompanyId, string remark, string ProceedRemark);
        PR_Master GetPrForImportTabulationReview(int PrId, int CompanyId);
        int GetParentPrforCoverinPr(int PoId);
        int CreateCoveringPr(int PoId, int ParentPrId, int UserId, int SupplierId, int QuotationId);
        int GetParentPRId(int GrnId, int itemId);
        PrMasterV2 GetPrForQuotationRejected(int PrId, int CompanyId, List<int> TabulationIds, int UserId, int DesignationId);
    }

    public class PR_MasterControllerImpl : PR_MasterController
    {

        public int SavePRMaster(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string prProcedure, string purchaseType, DateTime requiredDate, string MRNReferenceNo, int itemCatId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.SavePRMaster(DepartmentId, DateOfRequest, QuotationFor, OurReference, RequestedBy, CraeatedDateTime, CreatedBy, UpdatedDateTime, UpdatedBy, IsActive, PRAppoved, PRApprovedOrRejectedBy, PRApprovedOrRejectedDate, PRIsApproveForBid, PRIsApprovedOrRejectedBy, PRIsApprovedOrRejectedDate, BasePrid, prTypeId, expenseType, refNo01, refNo02, refNo03, refNo04, refNo05, refNo06, prProcedure, purchaseType, requiredDate, MRNReferenceNo, itemCatId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        //Reorder function stock by Pasindu 2020/04/29
        public int SavePRMasterV2(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string terms, int SubDepartmentId, int CategoryId, int warehouseId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.SavePRMasterV2(DepartmentId, DateOfRequest, QuotationFor, OurReference, RequestedBy, CraeatedDateTime, CreatedBy, UpdatedDateTime, UpdatedBy, IsActive, PRAppoved, PRApprovedOrRejectedBy, PRApprovedOrRejectedDate, PRIsApproveForBid, PRIsApprovedOrRejectedBy, PRIsApprovedOrRejectedDate, BasePrid, prTypeId, expenseType, refNo01, refNo02, refNo03, refNo04, refNo05, refNo06, terms, SubDepartmentId, CategoryId, warehouseId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        //Reorder function stock by Pasindu 2020/04/29
        public List<string> GetPrCodesByPrIds(List<int> PrIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrCodesByPrIds(PrIds, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();

                }
            }
        }

        public string FetchPRCode(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchPRCode(DepartmentId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> FetchApprovePRDataByDeptId(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchApprovePRDataByDeptId(DepartmentId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PR_Master FetchApprovePRDataByDeptIdAndPRId(int DepartmentId, int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchApprovePRDataByDeptIdAndPRId(DepartmentId, PrId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public int UpdatePRMaster(int PrId, int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string prProcedure, string purchaseType, DateTime requiredDate, string MRNReferenceNo)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.UpdatePRMaster(PrId, DepartmentId, DateOfRequest, QuotationFor, OurReference, RequestedBy, UpdatedDateTime, UpdatedBy, IsActive, PRAppoved, PRApprovedOrRejectedBy, PRApprovedOrRejectedDate, PRIsApproveForBid, PRIsApprovedOrRejectedBy, PRIsApprovedOrRejectedDate, prTypeId, expenseType, refNo01, refNo02, refNo03, refNo04, refNo05, refNo06, prProcedure, purchaseType, requiredDate, MRNReferenceNo, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public int UpdateIsApprovePR(int DepartmentId, int PrId, int Status, int PRApprovedUserId, int isActive, string RejectedReason)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.UpdateIsApprovePR(DepartmentId, PrId, Status, PRApprovedUserId, isActive, RejectedReason, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> FetchApprovePRDataByDeptIdApproved(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchApprovePRDataByDeptIdApproved(DepartmentId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int UpdateOprnForBid(int DepartmentId, int PrId, int BidOpeningStatus, string BidTermCondition, int ApprovedBy)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.UpdateOprnForBid(DepartmentId, PrId, BidOpeningStatus, BidTermCondition, ApprovedBy, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PR_Master FetchApprovePRDataByPRId(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchApprovePRDataByPRId(PrId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> GetPrMasterListByDaterange(int departmentid, DateTime startdate, DateTime enddate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrMasterListByDaterange(departmentid, startdate, enddate, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int UpdatePORaised(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.UpdatePORaised(PrId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> FetchRejectedPr(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchRejectedPr(DepartmentId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PR_Master FetchRejectPR(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchRejectPR(PrId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> FetchDetailsToEdit(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchDetailsToEdit(DepartmentId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> FetchYetSubmitPR(int Department)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchYetSubmitPR(Department, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public List<PR_Master> FetchTotalBidforChart(int Department, int year)

        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchTotalBidforChart(Department, year, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> FetchTotalPR(int Department)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchTotalPR(Department, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }


        public int countTotalPr(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.countTotalPr(DepartmentId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> countTotalPrtochart(int DepartmentId, int year)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.countTotalPrtochart(DepartmentId, year, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int countApprovedPr(int DepartmentId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.countApprovedPr(DepartmentId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int countRejectedPr(int DepartmentId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.countRejectedPr(DepartmentId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int countPendingPr(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.countPendingPr(DepartmentId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int GetDetailsByPrCode(int DepartmentId, string PrCode)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetDetailsByPrCode(DepartmentId, PrCode, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int GetParentPRId(int GrnId, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetParentPRId(GrnId, itemId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> FetchApprovePRDataByDeptIdReports(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchApprovePRDataByDeptIdReports(DepartmentId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> FetchApprovedPRForConfirmation(int Department)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchApprovedPRForConfirmation(Department, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int ConfirmOrDenyPRApproval(int prId, int confirm)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.ConfirmOrDenyPRApproval(prId, confirm, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForBidSubmission(int CompanyId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForBidSubmission(CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForBidSubmissionWithItem(int CompanyId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForBidSubmissionWithItem(CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForBidSubmissionByDate(int CompanyId, DateTime Date)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForBidSubmissionByDate(CompanyId, Date, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForBidSubmissionByPrCode(int CompanyId, string PrCode)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForBidSubmissionByPrCode(CompanyId, PrCode, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 GetPrForBidSubmission(int PrId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrForBidSubmission(PrId, CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForBidApproval(int CompanyId, int LoggedInUser)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForBidApproval(CompanyId, LoggedInUser, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForBidApprovalByDate(int CompanyId, int LoggedInUser, DateTime date)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForBidApprovalByDate(CompanyId, LoggedInUser, date, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForBidApprovalByPrCode(int CompanyId, int LoggedInUser, string PrCode)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForBidApprovalByPrCode(CompanyId, LoggedInUser, PrCode, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 GetPrForBidApproval(int PrId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrForBidApproval(PrId, CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public List<PrMasterV2> GetPrListForQuotationComparisonOld(List<int> SelectionPendingBidIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForQuotationComparisonOld(SelectionPendingBidIds, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public List<PrMasterV2> GetPrListForQuotationComparisonReviw(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForQuotationComparisonReviw(CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        //********Newly Added**********
        public List<PrMasterV2> GetPrListForQuotationComparisonReviwWithItem(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForQuotationComparisonReviwWithItem(CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public List<PrMasterV2> GetPrListForQuotationComparisonReviwByDate(int CompanyId, DateTime Date)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForQuotationComparisonReviwByDate(CompanyId, Date, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public List<PrMasterV2> GetPrListForQuotationComparisonReviwByPrCode(int CompanyId, string Code)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForQuotationComparisonReviwByPrCode(CompanyId, Code, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public List<PR_Master> GetPrListForQuotationComparison(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForQuotationComparison(CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 GetPrForQuotationComparison(int PrId, int CompanyId, List<int> SelectionPendingBidIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrForQuotationComparison(PrId, CompanyId, SelectionPendingBidIds, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForQuotationApproval(int CompanyId, List<int> TabulationIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForQuotationApproval(CompanyId, TabulationIds, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public List<PrMasterV2> GetPrListForQuotationRejected(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForQuotationRejected(CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public PrMasterV2 GetPrForQuotationApproval(int PrId, int CompanyId, List<int> TabulationIds, int UserId, int DesignationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrForQuotationApproval(PrId, CompanyId, TabulationIds, UserId, DesignationId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 GetPrForQuotationApprovalRej(int PrId, int CompanyId, int UserId, int DesignationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrForQuotationApprovalRej(PrId, CompanyId, UserId, DesignationId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForQuotationConfirmation(int CompanyId, List<int> TabulationIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForQuotationConfirmation(CompanyId, TabulationIds, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForQuotationAppRejected(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForQuotationAppRejected(CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 GetPrForQuotationConfirmation(int PrId, int CompanyId, List<int> TabulationIds, int UserId, int DesignationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrForQuotationConfirmation(PrId, CompanyId, TabulationIds, UserId, DesignationId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public PrMasterV2 GetPrForQuotationRejected(int PrId, int CompanyId, List<int> TabulationIds, int UserId, int DesignationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrForQuotationRejected(PrId, CompanyId, TabulationIds, UserId, DesignationId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public PrMasterV2 GetPrForQuotationConfirmationRej(int PrId, int CompanyId, int UserId, int DesignationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrForQuotationConfirmationRej(PrId, CompanyId, UserId, DesignationId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForManualQuotationSubmission(int CompanyId, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForManualQuotationSubmission(CompanyId, UserId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        //******** Newly Added ***********
        public List<PrMasterV2> GetPrListForManualQuotationSubmissionWithItem(int CompanyId, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForManualQuotationSubmissionWithItem(CompanyId, UserId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForRejectedBids(int CompanyId, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForRejectedBids(CompanyId, UserId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForPoCreation(int CompanyId, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForPoCreation(CompanyId, UserId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 GetPrForPoCreation(int PrId, int CompanyId, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrForPoCreation(PrId, CompanyId, UserId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public string SavePRMasterAtCloning(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string prProcedure, string purchaseType, DateTime requiredDate, string MRNReferenceNo)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.SavePRMasterAtCloning(DepartmentId, DateOfRequest, QuotationFor, OurReference, RequestedBy, CraeatedDateTime, CreatedBy, UpdatedDateTime, UpdatedBy, IsActive, PRAppoved, PRApprovedOrRejectedBy, PRApprovedOrRejectedDate, PRIsApproveForBid, PRIsApprovedOrRejectedBy, PRIsApprovedOrRejectedDate, BasePrid, prTypeId, expenseType, refNo01, refNo02, refNo03, refNo04, refNo05, refNo06, prProcedure, purchaseType, requiredDate, MRNReferenceNo, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public string SaveMRNtoPR(int mrnId, int userId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.SaveMRNtoPR(mrnId, userId, companyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 getPRMasterDetailByPrId(int PrID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.getPRMasterDetailByPrId(PrID, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 GetPrForBidSubmissionView(int PrId, int CompanyId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                PrMasterV2 prmaster = pr_MasterDAO.GetPrForBidSubmissionView(PrId, CompanyId, dbConnection);
                return prmaster;
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> FetchAllPR(int Department)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchAllPR(Department, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForViewAllPr(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForViewAllPr(CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForViewMyPr(int CompanyId, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForViewMyPr(CompanyId, UserId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PR_Master SearchPRForInquiryByPrId(int PrId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.SearchPRForInquiryByPrId(PrId, CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();

                }
            };
        }

        //public List<PR_Master> GetPRListForPrInquiry(int CompanyId)
        //{
        //    DBConnection dbConnection = new DBConnection();
        //    try
        //    {
        //        PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
        //        return pr_MasterDAO.GetPRListForPrInquiry(CompanyId, dbConnection);
        //    }
        //    catch (Exception)
        //    {
        //        dbConnection.RollBack();
        //        throw;
        //    }
        //    finally
        //    {
        //        if (dbConnection.con.State == System.Data.ConnectionState.Open)
        //        {
        //            dbConnection.Commit();

        //        }
        //    }
        //}


        public List<PR_Master> GetPRtobeApprovedForAdvanceSearch(int companyId, int categoryId, int subDepartmentID, int serchtype, string searchkey, string usertype)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPRtobeApprovedForAdvanceSearch(companyId, categoryId, subDepartmentID, serchtype, searchkey, usertype, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();

                }
            }

        }

        public List<PR_Master> GetPRtobeApprovedforBiddingForAdvanceSearch(int companyId, int categoryId, int subDepartmentID, int serchtype, string searchkey, string usertype)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPRtobeApprovedforBiddingForAdvanceSearch(companyId, categoryId, subDepartmentID, serchtype, searchkey, usertype, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();

                }
            }
        }

        public List<PR_Master> GetQuotationBidddingrAdvanceSearch(int companyId, int categoryId, int supplierId, int serchtype, string searchkey, string usertype)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetQuotationBidddingrAdvanceSearch(companyId, categoryId, supplierId, serchtype, searchkey, usertype, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();

                }
            }
        }

        public List<PR_Master> GetPrListForDocumentUploadPoCreation(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForDocumentUploadPoCreation(CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int CheckfileUploadedofteccommit(int bidId, int qutationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.CheckfileUploadedofteccommit(bidId, qutationId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }



        public List<int> GetPRCountForDashboard(int CompanyId, int yearsearch, int purchaseType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPRCountForDashboard(CompanyId, yearsearch, purchaseType, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public System.Data.DataTable GetPRCountForDashBoard()
        {
            DBConnection dbConnection = new DBConnection();

            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPRCountForDashBoard(dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<PrMasterV2> GetPrListForBidSubmited(int CompanyId, int UserId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForBidSubmited(CompanyId, UserId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForBidSubmitedByDate(int CompanyId, int UserId, DateTime date)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForBidSubmitedByDate(CompanyId, UserId, date, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForBidSubmitedByPrCode(int CompanyId, int UserId, string PrCode)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForBidSubmitedByPrCode(CompanyId, UserId, PrCode, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> FetchPRToEdit(int companyId, int userId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchPRToEdit(companyId, userId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Details> GetPRDetails(int prId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPRDetails(prId, companyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> FetchApprovedPR(int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchApprovedPR(companyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Details> GetOnlyPRDetails(int PrId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetOnlyPRDetails(PrId, CompanyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PR_Master> AdvanceSearchPRForInquiry(int companyId, int searchBy, int categoryId, int subdepartmentId, string searchText)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.AdvanceSearchPRForInquiry(companyId, searchBy, categoryId, subdepartmentId, searchText, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();

                }
            }
        }

        public int RejectBid(int BidId, int CompanyId, string remark, List<SupplierQuotationItem> quotationList)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                SupplierQuotationItemDAO SupplierQuotationItemDAO = DAOFactory.CreateSupplierQuotationItemDAO();

                int result = pr_MasterDAO.RejectBid(BidId, CompanyId, remark, dbConnection);

                if (result > 0)
                {
                    if (quotationList.Count > 0)
                    {
                        for (int i = 0; i < quotationList.Count; i++)
                        {
                            result = SupplierQuotationItemDAO.RejectQuotationItems(quotationList[i].QuotationItemId, quotationList[i].IsQuotationItemApprovalRemark, dbConnection);
                        }
                        if (result > 0)
                        {
                            return 1;

                        }
                        else
                        {
                            dbConnection.RollBack();
                            return -2;
                        }

                    }
                    return 1;
                }
                else
                {
                    dbConnection.RollBack();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();

                }
            }
        }

        public int ApproveBid(int BidId, int CompanyId, string remark)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.ApproveBid(BidId, CompanyId, remark, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();

                }
            }
        }

        public int CreateCoveringPr(int PoId, int ParentPrId, int UserId, int SupplierId, int QuotationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.CreateCoveringPr(PoId, ParentPrId, UserId, SupplierId, QuotationId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();

                }
            }
        }

        public int GetParentPrforCoverinPr(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetParentPrforCoverinPr(PoId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();

                }
            }
        }

        public int ApproveBidTab(int BidId, int CompanyId, string remark, string ProceedRemark)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.ApproveBidTab(BidId, CompanyId, remark, ProceedRemark, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();

                }
            }
        }
        public int ApproveBidTabImports(int BidId, int CompanyId, string ProceedRemark)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.ApproveBidTabImports(BidId, CompanyId, ProceedRemark, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();

                }
            }
        }
        public List<PrMasterV2> GetPrRejectedQuotationTabulationSheet(List<int> selectionPendingBidIds, int companyId, int userId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrRejectedQuotationTabulationSheet(selectionPendingBidIds, companyId, userId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public void UpdateTabulationReviewApproval(int bidId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                pr_MasterDAO.UpdateTabulationReviewApproval(bidId, companyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public List<PrMasterV2> GetPrListForQuotationApproval(List<int> SelectionPendingBidIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForQuotationApproval(SelectionPendingBidIds, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }


        public PR_Master GetPrForQuotationComparison(int PrId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrForQuotationComparison(PrId, CompanyId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public PR_Master GetPrForImportTabulationReview(int PrId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrForImportTabulationReview(PrId, CompanyId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> GetPrListForQuotationComparisonBid(List<int> SelectionPendingBidIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrListForQuotationComparisonBid(SelectionPendingBidIds, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 GetPrForQuotationComparisonImports(int PrId, int CompanyId, List<int> SelectionPendingBidIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.GetPrForQuotationComparisonImports(PrId, CompanyId, SelectionPendingBidIds, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int UpdateTerminatedPRMaster(int prId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.UpdateTerminatedPRMaster(prId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        // Fetch ALL
        public List<PrMasterV2> FetchPrALl()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchPrALl(dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        // End Fetch ALL
        public List<PrMasterV2> FetchPrByPrCode(int CompanyId, string prCode)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchPrByPrCode(CompanyId, prCode, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> FetchPrByDate(int companyId, DateTime ToDate, DateTime FromDate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.FetchPrByDate(companyId, ToDate, FromDate, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int SavePRMaster(int DepartmentId, DateTime DateOfRequest, string QuotationFor, string OurReference, string RequestedBy, DateTime CraeatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, int IsActive, int PRAppoved, string PRApprovedOrRejectedBy, DateTime PRApprovedOrRejectedDate, int PRIsApproveForBid, string PRIsApprovedOrRejectedBy, DateTime PRIsApprovedOrRejectedDate, int BasePrid, int prTypeId, string expenseType, string refNo01, string refNo02, string refNo03, string refNo04, string refNo05, string refNo06, string terms, int SubDepartmentId, int CategoryId, int warehouseId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
                return pr_MasterDAO.SavePRMaster(DepartmentId, DateOfRequest, QuotationFor, OurReference, RequestedBy, CraeatedDateTime, CreatedBy, UpdatedDateTime, UpdatedBy, IsActive, PRAppoved, PRApprovedOrRejectedBy, PRApprovedOrRejectedDate, PRIsApproveForBid, PRIsApprovedOrRejectedBy, PRIsApprovedOrRejectedDate, BasePrid, prTypeId, expenseType, refNo01, refNo02, refNo03, refNo04, refNo05, refNo06, terms, SubDepartmentId, CategoryId, warehouseId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

    }
}
