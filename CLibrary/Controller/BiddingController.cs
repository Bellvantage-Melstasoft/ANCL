using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;
using System.Web;
using System.Web.UI.WebControls;

namespace CLibrary.Controller
{
    public interface BiddingController
    {
        int SaveBidding(int PrId, int ItemId, int BiddingItemId, int BiddingPrId, DateTime StartDateTime, DateTime EndDateTime, DateTime CreatedDateTime, string CreatedUser, int IsActive, int SeqIdBidOpening, int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, string BidTermsAndConditions, int defaultDisplayImage, string imageURL);
        int UpdateBidding(int PrId, int ItemId);
        List<Bidding> GetAllBidding(int SupplierId);
        Bidding GetBiddingDetails(int PrId, int ItemId);
        List<Bidding> GetAllBiddingDetaileWise(int SupplierId);
        List<Bidding> GetAllLatestBids(int SupplierId);
        Bidding GetBiddingDetailsExisting(int PrId, int ItemId,int SupplierId);
        List<Bidding> GetBiddingGeneralSettings(int PrId, int ItemId);
        int UpdateBidRejectOrApproveStatus(int PrId, int ItemId, int IsApproveToviewInSupplierPortal, string RejectedReason, string BidOpeningId);
        List<Bidding> FetchRejectedAndApprovedBids(int PrId, int companyId);
        int UpdateBiddingPORaised(int PrId, int ItemId,int isPoRaiseFalg);
        int UpdateExpierBids(int departmentId);
        List<PreviousPurchase> GetLastPurchaseSupplier(int itemId, int departmentId);
        int ReOpenBid(int BidId, int UserId, DateTime date, string remark);
        List<Bidding> GetProgressPR(int departmentId, int isRegisteredSupplier);
        List<Bidding> GetProgressPR(int supplierId, int departmentId, int isRegisteredSupplier);

        List<Bidding> GetProgressPRItemsByPrId(int PRId, int departmentId, int supplierid, int isRegisteredSupplier);

        Bidding GetBiddingOrderid(int PrId, int ItemId);

        List<Bidding> GetRaisedPOSupplier(int supplierId);

        Bidding GetBiddingDetailsSvc(int PrId, int ItemId, string BiddingId);

        List<Bidding> GetAllBiddingInitialLoading();

        List<Bidding> GetBiddingDetailsSvcInitialLoad();

        List<Bidding> GetBiddingDetailsLatestInitialLoad();

        List<Bidding> GetProgressPRItemsByPrIdManualBid(int PRId, int departmentId, int supplierid, int isRegisteredSupplier);
        int updateDisplayImageUrl(string imageUrl, int PrId, int ItemId);
        List<Bidding> GetAllBiddingNew(int SupplierId);
        List<Bidding> GetAllLatestBidsOuter();
        List<Bidding> GetAllBidsOuter();



        List<Bidding> GetAllBidsForApproval(int CompanyId);
        List<Bidding> GetAllBidsForConfirmation(int CompanyId);


        //New Methods By Salman created on 2019-01-17

        List<Bidding> GetAllBidsForBidSubmission(int PrId);
        List<Bidding> GetAllBidsForBidApproval(int PrId);
        List<Bidding> GetAllBidsForQuotationComparison(int PrId);
        List<Bidding> GetAllBidsForQuotationApproval(int PrId);
        List<Bidding> GetAllBidsForQuotationConfirmation(int PrId);
        List<int> SaveBids(List<Bidding> Bids);
       // int SaveBids(List<Bidding> Bids);
        int ApproveBids(List<int> BidIds, string Remarks, int UserId);
        int RejecteBids(List<int> BidIds, List<int> prdIds, string Remarks, int UserId);
        List<Bidding> GetBidsForQuotationSubmission(int PrId);
        Bidding GetBidDetailsForQuotationSubmission(int BidId, int CompanyId);
        int ApproveOrRejectSelectedQuotation(int BidId, int Status, string Remarks, int UserId);
        int ConfirmOrRejectSelectedQuotation(int BidId, int Status, string Remarks, int UserId);
        List<Bidding> GetInProgressBids(int CompanyId);
        List<Bidding> GetClosedBids(int CompanyId);
        List<Bidding> GetManualInProgressBids(int CompanyId, int userID);
        List<Bidding> GetManualInProgressBidsWithItem(int CompanyId, int userId);
        List<Bidding> GetManualClosedBids(int CompanyId);
        int ExpireBid(int BidId);
        List<int> GetBidCountForDashboard(int CompanyId,int yearsearch,int purchaseType);
        
        //
        Bid_Bond_Details GetBidBondDetailByBidId(int BidId);
        List<int> GetSelectionPendingBidIds(int UserId, int DesignationId, int CompanyId);
        Bidding GetBiddingDetailsByBiddingId(int biddingId);
        //new method  for terminate-Pasindu 2020/04/25
        int TerminateBid(int BidId, int UserId, string Remarks);

        int SendEmailToSuppliers(int status , string createdUserEmail ,PrMasterV2 prMaster, List<Supplier> listSuppliers, List<SupplierBidEmailContact> unRegisteredSuppliers, List<SupplierBidEmailContact> officerContacts, List<Bidding> bids, HttpContext current, GridView gvStandardImageAttachment, GridView gvSupportiveDocumentAttachment, GridView gvReplacementImageAttachment);
        int ResetSelections(int BidId);
        List<BiddingItem> GetPrdIdByBidId(int bidId);
        Bidding GetBidDetailsByBidId(int bidId);
        List<int> GetSelectionRejectedBidIds(int UserId, int DesignationId, int CompanyId);
        List<int> GetSelectionPendingBidIdsForTabulationApproval(int UserId, int DesignationId, int CompanyId);
        List<Bidding> GetManualClosedBidsByPrCode(int CompanyId, string prCode);
        List<Bidding> GetManualClosedBidsByBidCode(int CompanyId, int bidCode);
        List<Bidding> GetClosedBidsByBidCode(int CompanyId, int BidCode);
        List<Bidding> GetClosedBidsByPrCode(int CompanyId, String PRcode);
        int UpdateEmailStatus(List<int> BidIds, int prId);
         List<Bidding> FetchBidInfo(int PrId);
        List<Bidding> FetchBidInfoForPRRequisitionReport(List<int> PrId);
        Bidding GetBidDetailsForQuotationSubmissionImports(int BidId, int CompanyId);
    }

        public class BiddingControllerImpl : BiddingController
    {

        public int SaveBidding(int PrId, int ItemId, int BiddingItemId, int BiddingPrId, DateTime StartDateTime, DateTime EndDateTime, DateTime CreatedDateTime, string CreatedUser, int IsActive, int SeqIdBidOpening, int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, string BidTermsAndConditions, int defaultDisplayImage, string imageURL)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.SaveBidding(PrId, ItemId, BiddingItemId, BiddingPrId, StartDateTime, EndDateTime, CreatedDateTime, CreatedUser, IsActive,SeqIdBidOpening, DepartmentId, BidOpeningPeriod, CanOverride, BidOnlyRegisteredSupplier, ViewBidsOnlineUponPrCreation,BidTermsAndConditions, defaultDisplayImage,  imageURL, dbConnection);
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

        public int UpdateBidding(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.UpdateBidding(PrId, ItemId, dbConnection);
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
        public int UpdateEmailStatus(List<int> BidIds, int prId) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.UpdateEmailStatus(BidIds, prId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        
        public List<Bidding> GetAllBidding(int SupplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                List<Bidding> GetAllBidding = new List<Bidding>();
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                List<Bidding> GetRegisteredSupplierBidding = biddingDAO.GetAllBiddingForRegisteredSupplier(SupplierId, dbConnection);
                List<Bidding> GetNonRegisteredSupplierBidding = biddingDAO.GetAllBiddingForNonRegisteredSupplier(SupplierId, dbConnection);
                GetAllBidding = GetRegisteredSupplierBidding.Concat(GetNonRegisteredSupplierBidding).ToList();
                return GetAllBidding;
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

        public Bidding GetBiddingDetails(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBiddingDetails(PrId, ItemId, dbConnection);
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

        public List<Bidding> GetAllBiddingDetaileWise(int SupplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetAllBiddingDetaileWise(SupplierId, dbConnection);
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

        public List<Bidding> FetchBidInfoForPRRequisitionReport(List<int> PrId) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.FetchBidInfoForPRRequisitionReport(PrId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public List<Bidding> FetchBidInfo(int PrId) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.FetchBidInfo(PrId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }


        public List<Bidding> GetAllLatestBids(int SupplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                List<Bidding> GetAllatestBidding = new List<Bidding>();
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                List<Bidding> GetLatestRegisteredSupplierBidding = biddingDAO.GetAllLatestBidsForRegisteredSupplier(SupplierId, dbConnection);
                List<Bidding> GetLatestNonRegisteredSupplierBidding = biddingDAO.GetAllLatestBidsForNonRegisteredSupplier(SupplierId, dbConnection);
                GetAllatestBidding = GetLatestRegisteredSupplierBidding.Concat(GetLatestNonRegisteredSupplierBidding).ToList();
                return GetAllatestBidding;
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

        public Bidding GetBiddingDetailsExisting(int PrId, int ItemId,int SupplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBiddingDetailsExisting(PrId, ItemId,SupplierId, dbConnection);
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

        public List<Bidding> GetBiddingGeneralSettings(int PrId, int ItemId) 
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBiddingGeneralSettings(PrId,ItemId, dbConnection);
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

        public int UpdateBidRejectOrApproveStatus(int PrId, int ItemId, int IsApproveToviewInSupplierPortal, string RejectedReason, string BidOpeningId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.UpdateBidRejectOrApproveStatus(PrId, ItemId,IsApproveToviewInSupplierPortal, RejectedReason,BidOpeningId, dbConnection);
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

        public List<Bidding> FetchRejectedAndApprovedBids(int PrId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.FetchRejectedAndApprovedBids(PrId,  companyId, dbConnection);
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

        public int UpdateBiddingPORaised(int PrId, int ItemId,int isPoRaiseFalg)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.UpdateBiddingPORaised(PrId, ItemId, isPoRaiseFalg, dbConnection);
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

        public int UpdateExpierBids(int departmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.UpdateExpierBids(departmentId, dbConnection);
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

        public List<PreviousPurchase> GetLastPurchaseSupplier(int itemId, int departmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetLastPurchaseSupplier( itemId,  departmentId, dbConnection);
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

        public List<Bidding> GetProgressPR(int departmentId, int isRegisteredSupplier)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetProgressPR( departmentId,  isRegisteredSupplier, dbConnection);
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

        public List<Bidding> GetProgressPR(int supplierId,int departmentId, int isRegisteredSupplier)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetProgressPR(supplierId,departmentId, isRegisteredSupplier, dbConnection);
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

        public List<Bidding> GetProgressPRItemsByPrId(int PRId, int departmentId, int supplierid, int isRegisteredSupplier)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetProgressPRItemsByPrId( PRId,  departmentId,  supplierid,  isRegisteredSupplier, dbConnection);
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

        public Bidding GetBiddingOrderid(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBiddingOrderid( PrId,  ItemId, dbConnection);
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

        public List<Bidding> GetRaisedPOSupplier(int supplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetRaisedPOSupplier(supplierId, dbConnection);
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

        public Bidding GetBiddingDetailsSvc(int PrId, int ItemId, string BiddingId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBiddingDetailsSvc(PrId, ItemId, BiddingId, dbConnection);
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

        public List<Bidding> GetAllBiddingInitialLoading()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                List<Bidding> GetAllBiddingInitialLoading = new List<Bidding>();
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                List<Bidding> GetRegisteredSupplierBidding = biddingDAO.GetAllBiddingForRegisteredSupplierInitialBinding(dbConnection);
                List<Bidding> GetNonRegisteredSupplierBidding = biddingDAO.GetAllBiddingForNonRegisteredSupplierInitialBinding(dbConnection);
                GetAllBiddingInitialLoading = GetRegisteredSupplierBidding.Concat(GetNonRegisteredSupplierBidding).ToList();
                return GetAllBiddingInitialLoading;
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

        public List<Bidding> GetBiddingDetailsSvcInitialLoad()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBiddingDetailsSvcInitialLoad(dbConnection);
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

        public List<Bidding> GetBiddingDetailsLatestInitialLoad()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBiddingDetailsLatestInitialLoad(dbConnection);
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

        public List<Bidding> GetProgressPRItemsByPrIdManualBid(int PRId, int departmentId, int supplierid, int isRegisteredSupplier)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetProgressPRItemsByPrIdManualBid( PRId, departmentId, supplierid,  isRegisteredSupplier, dbConnection);
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

        public int updateDisplayImageUrl(string imageUrl, int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.updateDisplayImageUrl( imageUrl,  PrId,  ItemId, dbConnection);
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

        public List<Bidding> GetAllBiddingNew(int SupplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                List<Bidding> GetAllBidding = new List<Bidding>();
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                List<Bidding> GetRegisteredSupplierBidding = biddingDAO.GetAllBiddingForRegisteredSupplierNew(SupplierId, dbConnection);
                List<Bidding> GetNonRegisteredSupplierBidding = biddingDAO.GetAllBiddingForNonRegisteredSupplierNew(SupplierId, dbConnection);
                GetAllBidding = GetRegisteredSupplierBidding.Concat(GetNonRegisteredSupplierBidding).ToList();
                return GetAllBidding;
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

        public List<Bidding> GetAllLatestBidsOuter()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetAllLatestBidsOuter(dbConnection);
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

        public List<Bidding> GetAllBidsOuter()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetAllBidsOuter(dbConnection);
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

        public List<Bidding> GetAllBidsForApproval(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return null;// biddingDAO.GetAllBidsForApproval(CompanyId,dbConnection);
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

        public List<Bidding> GetAllBidsForConfirmation(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return null;// biddingDAO.GetAllBidsForConfirmation(CompanyId,dbConnection);
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

        public List<Bidding> GetAllBidsForBidSubmission(int PrId)
        {
            throw new NotImplementedException();
        }

        public List<Bidding> GetAllBidsForBidApproval(int PrId)
        {
            throw new NotImplementedException();
        }

        public List<Bidding> GetAllBidsForQuotationComparison(int PrId)
        {
            throw new NotImplementedException();
        }

        public List<Bidding> GetAllBidsForQuotationApproval(int PrId)
        {
            throw new NotImplementedException();
        }

        public List<Bidding> GetAllBidsForQuotationConfirmation(int PrId)
        {
            throw new NotImplementedException();
        }

        //public int SaveBids(List<Bidding> Bids)
        //{
        //    DBConnection dbConnection = new DBConnection();
        //    try
        //    {
        //        BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
        //        return biddingDAO.SaveBids(Bids, dbConnection);
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

        public List<int> SaveBids(List<Bidding> Bids)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.SaveBids(Bids, dbConnection);
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

        public int ApproveBids(List<int> BidIds, string Remarks, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.ApproveBids(BidIds, Remarks, UserId, dbConnection);
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

        public int RejecteBids(List<int> BidIds, List<int> prdIds, string Remarks, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.RejecteBids(BidIds,prdIds, Remarks, UserId, dbConnection);
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

        public List<Bidding> GetBidsForQuotationSubmission(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBidsForQuotationSubmission(PrId, dbConnection);
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

        public Bidding GetBidDetailsForQuotationSubmission(int BidId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBidDetailsForQuotationSubmission(BidId, CompanyId, dbConnection);
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

        public Bidding GetBidDetailsForQuotationSubmissionImports(int BidId, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBidDetailsForQuotationSubmissionImports(BidId, CompanyId, dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }


        public int ApproveOrRejectSelectedQuotation(int BidId, int Status, string Remarks, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.ApproveOrRejectSelectedQuotation(BidId, Status, Remarks, UserId, dbConnection);
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

        public int ConfirmOrRejectSelectedQuotation(int BidId, int Status, string Remarks, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.ConfirmOrRejectSelectedQuotation(BidId, Status, Remarks, UserId, dbConnection);
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

        public List<Bidding> GetInProgressBids(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetInProgressBids(CompanyId, dbConnection);
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

        public List<Bidding> GetClosedBids(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetClosedBids(CompanyId, dbConnection);
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

        public List<Bidding> GetClosedBidsByPrCode(int CompanyId, String PRcode) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetClosedBidsByPrCode(CompanyId, PRcode,dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<Bidding> GetClosedBidsByBidCode(int CompanyId, int BidCode) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetClosedBidsByBidCode(CompanyId, BidCode, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<Bidding> GetManualInProgressBids(int CompanyId,int userId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetManualInProgressBids(CompanyId,userId, dbConnection);
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

        //*****Newly Added********
        public List<Bidding> GetManualInProgressBidsWithItem(int CompanyId, int userId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetManualInProgressBidsWithItem(CompanyId, userId, dbConnection);
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

        public List<Bidding> GetManualClosedBids(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetManualClosedBids(CompanyId, dbConnection);
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

        public int ExpireBid(int BidId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.ExpireBid(BidId, dbConnection);
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

        //terminate button function-pasindu-2020/04/25
        public int TerminateBid(int BidId, int UserId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.TerminateBid(BidId, UserId, Remarks, dbConnection);
            }
            catch (Exception Ex)
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

        public Bid_Bond_Details GetBidBondDetailByBidId(int BidId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBidBondDetailByBidId(BidId, dbConnection);
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

        public List<int> GetBidCountForDashboard(int CompanyId,int yearsearch, int purchaseType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBidCountForDashboard(CompanyId, yearsearch, purchaseType, dbConnection);
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

        public List<int> GetSelectionPendingBidIds(int UserId, int DesignationId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetSelectionPendingBidIds(UserId, DesignationId, CompanyId, dbConnection);
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

        public List<int> GetSelectionRejectedBidIds(int UserId, int DesignationId, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetSelectionRejectedBidIds(UserId, DesignationId, CompanyId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<int> GetSelectionPendingBidIdsForTabulationApproval(int UserId, int DesignationId, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetSelectionPendingBidIdsForTabulationApproval(UserId, DesignationId, CompanyId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public int ReOpenBid(int BidId, int UserId, DateTime date, string remark) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.ReOpenBid(BidId, UserId, date, remark, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();

                }
            }
        }
        public int ResetSelections(int BidId) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.ResetSelections(BidId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();

                }
            }
        }
        public Bidding GetBiddingDetailsByBiddingId(int biddingId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBiddingDetailsByBiddingId(biddingId, dbConnection);
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
        public List<BiddingItem> GetPrdIdByBidId(int bidId) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetPrdIdByBidId(bidId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public Bidding GetBidDetailsByBidId(int bidId) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetBidDetailsByBidId(bidId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }


        public int SendEmailToSuppliers(int status , string createdUserEmail, PrMasterV2 prMaster, List<Supplier> listSuppliers, List<SupplierBidEmailContact> unRegisteredSuppliers, List<SupplierBidEmailContact> officerContacts, List<Bidding> bids, HttpContext current, GridView gvStandardImageAttachment, GridView gvSupportiveDocumentAttachment, GridView gvReplacementImageAttachment)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                for (int i = 0; i < unRegisteredSuppliers.Count; ++i)
                {
                    DAOFactory.createSupplierDAO().SaveUnRegisteredSupplier(prMaster.PrId, unRegisteredSuppliers[i], dbConnection);
                }
                DAOFactory.createSupplierDAO().DeleteSupplierBidEmailContact(prMaster.PrId, dbConnection);
                for (int i = 0; i < officerContacts.Count; i++)
                {
                    DAOFactory.createSupplierDAO().SaveSupplierBidEmailContact(prMaster.PrId, officerContacts[i].ContactOfficer, officerContacts[i].ContactNo, officerContacts[i].Title, dbConnection);
                }

                if (status == 1)
                {
                    string subject = "Bid Creation";
                    string BidCode = string.Empty;
                    for (int x = 0; x < bids.Count; ++x)
                    {
                        BidCode = BidCode + " B" + bids[x].BidCode + " , ";
                    }
                    subject = subject + BidCode;
                    for (int s = 0; s < listSuppliers.Count; ++s)
                    {
                        string message1 = string.Empty;
                        message1 = "<b>" + DateTime.Now.ToString("yyyy.MM.dd") + "</b> " + "<br/><br/> ";
                        message1 = message1 + "<b>" + listSuppliers[s].SupplierName + "</b>.<br/>";
                        message1 = message1 + "Dear  Sir,  <br/><br/> " +
                                   " Please submit your quotation/s for supply of item/s as per detailed specifications given below , <br/>";
                        string procurementPlan = string.Empty;
                        string ContactInfo = string.Empty;
                        string message2 = string.Empty;
                        for (int t = 0; t < bids[0].BiddingPlan.Count; ++t)
                        {
                            Procument_PlanType procumentPlanType = DAOFactory.createProcument_Plan_Type_DAO().FetchAllProcumentPlanTypeByPlanId(bids[0].BiddingPlan[t].PlanId, dbConnection);
                            string fromDate = bids[0].BiddingPlan[t].StartDate.ToString();
                            string toDate = bids[0].BiddingPlan[t].EndDate.ToString();
                            if (procumentPlanType.WithTime != 1)
                            {
                                fromDate = bids[0].BiddingPlan[t].StartDate.ToString("MM/dd/yyyy");
                                toDate = bids[0].BiddingPlan[t].EndDate.ToString("MM/dd/yyyy");
                            }
                            procurementPlan = procurementPlan + "  " + procumentPlanType.PlanName + " on " + fromDate + " to " + toDate;
                            procurementPlan = procurementPlan + "<br/> ";
                        }

                        for (int x = 0; x < bids.Count; ++x)
                        {
                            DAOFactory.createSupplierDAO().SaveSupplierEmailForBidSubmission(prMaster.PrId, bids[x].BidId, listSuppliers[s].SupplierId, listSuppliers[s].SupplierName, listSuppliers[s].Email, dbConnection);
                        }

                        //  message2 = "Following provides item details & it's Specifications " + "<br/>";
                        for (int i = 0; i < prMaster.Bids.Count; ++i)
                        {
                            // message2 = message2 + "B" + PrMaster.Bids[i].BidCode + " refers to  " + PrMaster.Bids[i].BiddingItems.Count + " items<br/>";
                            for (int l = 0; l < prMaster.Bids[i].BiddingItems.Count; ++l)
                            {
                                message2 = message2 + "<b>" + (l+1) + ")</b>" + " " + "<b>" + prMaster.Bids[i].BiddingItems[l].ItemName + "</b><br/>";
                                for (int t = 0; t < prMaster.PrDetails.Count; ++t)
                                {
                                    for (int h = 0; h < prMaster.PrDetails[t].PrBoms.Count; ++h)
                                    {
                                        if (prMaster.PrDetails[t].PrBoms[h].PrdId == prMaster.Bids[i].BiddingItems[l].PrdId)
                                        {
                                            message2 = message2 + "Item Specfication - " + prMaster.PrDetails[t].PrBoms[h].Material + " : " + prMaster.PrDetails[t].PrBoms[h].Description + "<br/>";
                                        }
                                    }
                                }
                            }
                        }

                      
                        message1 = message1 + "<br/>";
                        message1 = message1 + message2 + "<br/>" + "<b>(Please quote your best price)</b> <br/>" + "<b> Please refer the attached document for more details</b> <br/> ";
                        for (int i = 0; i < officerContacts.Count; i++)
                        {
                            ContactInfo = ContactInfo + "  " + officerContacts[i].Title + " " + officerContacts[i].ContactOfficer + " on " + officerContacts[i].ContactNo;
                            ContactInfo = ContactInfo + "<br/>";
                        }

                        string message3 = string.Empty;
                        message3 = "Please Indicate <br/>" +
                                    "   (1) Credit facilities allowed <br/>" +
                                    "   (2)  Validity of offer <br/>" +
                                    "   (3)  Availability ex-stock <br/>" +
                                    "   (4)  If not available ex-stock, probable date it will be available.<br/>" +
                                "Quotations should be sent to reach the undersigned or fax to our fax no.2429455 " +
                                "on or before <b>" + bids[0].EndDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]) + "</b>" +
                                " indicating <b>Company Name </b>on the top left hand corner of the envelope." +
                                " <br/>Quotation should not be submitted on this letter, but should be submitted on your Company Letter Head.";


                        message1 = message1 + message3;
                        message1 = message1 + "<br/> <b>Procurement Plan details are as follows </b> <br/>";
                        message1 = message1 + procurementPlan;
                        message1 = message1 + "<br/> For further clarifications contact below personal <br/> " + "  " + ContactInfo + "<br/>";
                        message1 = message1 + "<b> Email</b> :purchasing@lakehouse.lk <br/>";
                        message1 = message1 + "  <br/> ";
                        message1 = message1 + " Yours Faithfully, <br/> ";
                        message1 = message1 + "The Associated Newspaper of Ceylon LTD </br>";


                        EmailGenerator.SendEmailWithAttachement(listSuppliers[s].Email, subject, message1, current, gvStandardImageAttachment, gvSupportiveDocumentAttachment, gvReplacementImageAttachment, createdUserEmail);

                    }
                }
                else if (status == 2)
                {
                    string subject = "Bid has been updated";
                    string BidCode = string.Empty;
                    for (int x = 0; x < bids.Count; ++x)
                    {
                        BidCode = BidCode + " B" + bids[x].BidCode + " , ";
                    }
                    subject = subject + BidCode;
                    for (int s = 0; s < listSuppliers.Count; ++s)
                    {
                        string message1 = string.Empty;
                        string message2 = string.Empty;
                        string ContactInfo = string.Empty;
                        message1 = "<b>" + DateTime.Now.ToString("yyyy.MM.dd") + "</b> " + "<br/><br/> ";
                        message1 = message1 + "<b>" + listSuppliers[s].SupplierName + "</b>.<br/>";
                        message1 = message1 + "Dear  Sir,  <br/><br/> ";
                        message1 = message1 + "  Based on the request raised by the vendors/ decision of the Committee, chairman procurement committee has approved to change the bid submission of closing date " +
                                    "  and they are as follows <br/>";
                        string procurementPlan = string.Empty;
                        for (int t = 0; t < prMaster.Bids[0].BiddingPlan.Count; ++t)
                        {
                            Procument_PlanType procumentPlanType = DAOFactory.createProcument_Plan_Type_DAO().FetchAllProcumentPlanTypeByPlanId(prMaster.Bids[0].BiddingPlan[t].PlanId, dbConnection);
                            string fromDate = prMaster.Bids[0].BiddingPlan[t].StartDate.ToString();
                            string toDate = prMaster.Bids[0].BiddingPlan[t].EndDate.ToString();
                            if (procumentPlanType.WithTime != 1)
                            {
                                fromDate = Convert.ToDateTime(fromDate).ToString("MM/dd/yyyy");
                                toDate = Convert.ToDateTime(toDate).ToString("MM/dd/yyyy");
                            }
                            procurementPlan = procurementPlan + "  " + procumentPlanType.PlanName + " on " + fromDate + " to " + toDate;
                            procurementPlan = procurementPlan + "<br/> ";
                        }

                        message2 = "Following provides item details & it's Specifications " + "<br/>";
                        for (int i = 0; i < prMaster.Bids.Count; ++i)
                        {
                            // message2 = message2 + "B" + prm.Bids[i].BidCode + " refers to  " + PrMaster.Bids[i].BiddingItems.Count + " items<br/>";
                            for (int l = 0; l < prMaster.Bids[i].BiddingItems.Count; ++l)
                            {
                                message2 = message2 + "<b>" + (l + 1) + ")</b>" + "<b>" + prMaster.Bids[i].BiddingItems[l].ItemName + "</b><br/>";
                                for (int t = 0; t < prMaster.PrDetails.Count; ++t)
                                {
                                    for (int h = 0; h < prMaster.PrDetails[t].PrBoms.Count; ++h)
                                    {
                                        if (prMaster.PrDetails[t].PrBoms[h].PrdId == prMaster.Bids[i].BiddingItems[l].PrdId)
                                        {
                                            message2 = message2 + "Item Specfication - " + prMaster.PrDetails[t].PrBoms[h].Material + " : " + prMaster.PrDetails[t].PrBoms[h].Description + "<br/>";
                                        }
                                    }
                                }
                            }
                        }
                        //message2 = message2 + "<br/>";
                        message1 = message1 + "<br/>";
                        message1 = message1 + message2 + "<br/>" + "<b>(Please quote your best price)</b> <br/>" + "<b>Please refer the attached document for more details</b> <br/> ";

                        for (int i = 0; i < officerContacts.Count; i++)
                        {
                            ContactInfo = ContactInfo + "  " + officerContacts[i].Title + " " + officerContacts[i].ContactOfficer + " on " + officerContacts[i].ContactNo;
                            ContactInfo = ContactInfo + "<br/>";
                        }

                        string message3 = string.Empty;
                        message3 = "Please Indicate <br/>" +
                                    "   (1) Credit facilities allowed <br/>" +
                                    "   (2)  Validity of offer <br/>" +
                                    "   (3)  Availability ex-stock <br/>" +
                                    "   (4)  If not available ex-stock, probable date it will be available.<br/>" +
                             "Quotations should be sent to reach the undersigned or fax to our fax no.2429455 " +
                             "on or before <b>" + bids[0].EndDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]) + "</b>" +
                             " indicating <b>Company Name </b>on the top left hand corner of the envelope." +
                             " <br/>Quotation should not be submitted on this letter, but should be submitted on your Company Letter Head.";

                        message1 = message1 + message3;
                        message1 = message1 + "<br/> <b>Procurement Plan details are as follows </b><br/>";
                        message1 = message1 + procurementPlan;
                        message1 = message1 + "<br/> For further clarifications contact below personal <br/> " + "  " + ContactInfo + "<br/>";
                        message1 = message1 + "<b>Email</b> :purchasing@lakehouse.lk <br/>";
                        message1 = message1 + "<br/>";
                        message1 = message1 + " Yours Faithfully, <br/> ";
                        message1 = message1 + "The Associated Newspaper of Ceylon LTD </br>";

                        EmailGenerator.SendEmailWithAttachement(listSuppliers[s].Email, subject, message1, current, gvStandardImageAttachment, gvSupportiveDocumentAttachment, gvReplacementImageAttachment, createdUserEmail);
                    }
                }
                return 1;
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

        public List<Bidding> GetManualClosedBidsByBidCode(int CompanyId, int bidCode) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetManualClosedBidsByBidCode(CompanyId, bidCode, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<Bidding> GetManualClosedBidsByPrCode(int CompanyId, string prCode) {
            DBConnection dbConnection = new DBConnection();
            try {
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();
                return biddingDAO.GetManualClosedBidsByPrCode(CompanyId, prCode, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
    }
}
