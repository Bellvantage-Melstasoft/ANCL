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
    public interface PR_DetailController
    {
        int SavePRDetails(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided , string Remarks , int MeasurementId);


        List<PR_Details> FetchPR_DetailsByDeptIdAndPrId(int PrId, int companyId);
        DataTable FetchPR_DetailsByDeptIdAndPrIdDatatable(int PrId, int companyId);
        
        
        List<PR_Details> FetchPR_DetailsByDeptIdAndPrIdIsApproved(int PrId, int companyId);
        List<PR_Details> FetchPR_DetailsByDeptIdAndPrIdIsRejected(int PrId, int companyId);
        List<PR_Details> FetchByPRDetails(int PrId);
        PR_Details FetchPR_DetailsBidComparion(int PrId);
        List<PR_Details> FetchBidCompletedPR(int CompanyId);
        List<PR_Details> GetAllItems(int PrId);
        List<AddItem> GetItemsByPrID(int PrId);
        List<AddItem> GetItemsByMrnID(int MrnId);
        PR_Details FetchPR_DetailsByPrIdAndItemId(int PrId, int itemId);
        int DeletePrDetailByPrIDAndItemId(int prID, int ItemId);
        int UpdatePRDetails(int PrId, int oldItemId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId);
        List<PR_Details> FetchDetailsRejectedPR(int prID, int companyId);
        List<PR_Details> FetchDetailsApprovedPR(int prID, int companyId);
        int UpdateUpdateForBid(int prID, int ItemId, int submitForBidStatus, int RejectedCount);
        List<PR_Details> FetchBidSubmissionDetails();
        List<PR_Details> FetchPR_DetailsByPrIdList(int PrId);
        List<PR_Details> FetchNotSubmitedItemsToSupplierPortalView(int PrId, int companyId);
        List<PR_Details> FetchtSubmitedItemsToSupplierPortalView(int PrId, int companyId);
        int UpdateIsPoRaised(int PrId, int ItemId, int IsPoRaised);
        int UpdateIsPoAproved(int PrId, int ItemId, int IsApproved);

        int UpdateByRejectPO(int PrId, int ItemId);
        int updateReplacementImageStatus(int PrId, int ItemId, int replacementStatus);
        int UpdateIsPoRaisedRejectedCount(int PrId, int ItemId, int RejectedCount);

        //---2018-09-18  Changes Dcsl
        int UpdateUpdateForBidType(int prID, int ItemId, int BidTypeManualOrBid);
        int UpdateRejectUpdateForBid(int prID, int ItemId, int submitForBidStatus);
        int UpdateItemQuantityFromBidSubmitting(int prID, int ItemId, decimal itemQuantity);


        //New Methods By Salman created on 2019-01-17
        List<PrDetailsV2> GetPrDetailsForBidSubmission(int PrId, int CompanyId, int WarehouseId);
        PrDetailsV2 GetPrDetails(int PrdId, int CompanyId);
        List<PR_Details> GetPrDetailsByPrId(int prId, int companyId);
        int UpdateStatus(int userId, int prdId);
        //int DeleteFromPO(int prId, List<int> itemId, int UserId);
        //Reorder function stock by Pasindu 2020/04/29
        int SavePRDetailsV2(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount);
        int UpdateterminatedStatus(int userId, int prdId);
        int UpdatePRStatus(int userId, int prdId, string DetailStatus);
        int SavePRDetails(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount);
        int UpdateLogStatus(int userId, int prdId, string LogStatus);
        int UpdatePRStatuss(int userId, int prdId, string DetailStatus, string LogStatus);
        int UpdatePRStatusFoeCancelledPos(int userId, string DetailStatus, List<int> ItemIds, int PrID);

    }

    public class PR_DetailControllerImpl : PR_DetailController
    {
        public int SavePRDetails(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.SavePRDetails(PrId, ItemId, Unit, ItemDescription, ItemUpdatedBy, ItemUpdatedDateTime, IsActive, Replacement, ItemQuantity, Purpose, EstimatedAmount,FileSampleProvided , Remarks ,  MeasurementId, dbConnection);
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

        public List<PR_Details> FetchPR_DetailsByDeptIdAndPrId(int PrId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchPR_DetailsByDeptIdAndPrId(PrId,  companyId, dbConnection);
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
        public int SavePRDetailsV2(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.SavePRDetailsV2(PrId, ItemId, Unit, ItemDescription, ItemUpdatedBy, ItemUpdatedDateTime, IsActive, Replacement, ItemQuantity, Purpose, EstimatedAmount, dbConnection);
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

        public DataTable FetchPR_DetailsByDeptIdAndPrIdDatatable(int PrId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();

            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchPR_DetailsByDeptIdAndPrIdDatatable(PrId, companyId, dbConnection);
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





        public List<PR_Details> FetchPR_DetailsByDeptIdAndPrIdIsApproved(int PrId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchPR_DetailsByDeptIdAndPrIdIsApproved(PrId, companyId, dbConnection);
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

        public List<PR_Details> FetchPR_DetailsByDeptIdAndPrIdIsRejected(int PrId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchPR_DetailsByDeptIdAndPrIdIsRejected(PrId, companyId, dbConnection);
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
        public PR_Details FetchPR_DetailsByPrIdAndItemId(int PrId, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchPR_DetailsByPrIdAndItemId(PrId, itemId, dbConnection);
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
        public List<PR_Details> FetchByPRDetails(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchByPRDetails(PrId, dbConnection);
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

        public PR_Details FetchPR_DetailsBidComparion(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchPR_DetailsBidComparion(PrId, dbConnection);
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

        public List<PR_Details> FetchBidCompletedPR(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchBidCompletedPR(CompanyId, dbConnection);
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

        public List<PR_Details> GetAllItems(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.GetAllItems(PrId, dbConnection);
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

        public List<AddItem> GetItemsByPrID(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.GetItemsByPrID(PrId, dbConnection);
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

        public List<AddItem> GetItemsByMrnID(int MrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.GetItemsByMrnID(MrnId, dbConnection);
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

        public int DeletePrDetailByPrIDAndItemId(int prID, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.DeletePrDetailByPrIDAndItemId(prID, ItemId, dbConnection);
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
        public int UpdatePRDetails(int PrId, int oldItemId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdatePRDetails(PrId, oldItemId, ItemId, Unit, ItemDescription, ItemUpdatedBy, ItemUpdatedDateTime, IsActive, Replacement, ItemQuantity, Purpose, EstimatedAmount,FileSampleProvided,Remarks,MeasurementId, dbConnection);
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

        public List<PR_Details> FetchDetailsRejectedPR(int prID, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchDetailsRejectedPR(prID, companyId, dbConnection);
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

        public List<PR_Details> FetchDetailsApprovedPR(int prID, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchDetailsApprovedPR(prID, companyId, dbConnection);
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

        public int UpdateUpdateForBid(int prID, int ItemId, int submitForBidStatus, int RejectedCount)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdateUpdateForBid(prID, ItemId, submitForBidStatus, RejectedCount, dbConnection);
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

        public List<PR_Details> FetchBidSubmissionDetails()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchBidSubmissionDetails(dbConnection);
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

        public List<PR_Details> FetchPR_DetailsByPrIdList(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchPR_DetailsByPrIdList(PrId, dbConnection);
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

        public List<PR_Details> FetchNotSubmitedItemsToSupplierPortalView(int PrId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchNotSubmitedItemsToSupplierPortalView(PrId, companyId, dbConnection);
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

        public List<PR_Details> FetchtSubmitedItemsToSupplierPortalView(int PrId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.FetchtSubmitedItemsToSupplierPortalView(PrId, companyId, dbConnection);
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

        public int UpdateIsPoRaised(int PrId, int ItemId, int IsPoRaised)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdateIsPoRaised(PrId, ItemId, IsPoRaised, dbConnection);
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

        public int UpdateIsPoAproved(int PrId, int ItemId, int IsApproved)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdateIsPoAproved(PrId, ItemId, IsApproved, dbConnection);
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

        public int UpdateByRejectPO(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdateByRejectPO(PrId, ItemId, dbConnection);
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

        public int updateReplacementImageStatus(int PrId, int ItemId, int replacementStatus)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.updateReplacementImageStatus(PrId, ItemId, replacementStatus, dbConnection);
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

        public int UpdateIsPoRaisedRejectedCount(int PrId, int ItemId, int RejectedCount)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdateIsPoRaisedRejectedCount(PrId, ItemId, RejectedCount, dbConnection);
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

        public int UpdateUpdateForBidType(int prID, int ItemId, int BidTypeManualOrBid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdateUpdateForBidType(prID, ItemId, BidTypeManualOrBid, dbConnection);
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

        public int UpdateRejectUpdateForBid(int prID, int ItemId, int submitForBidStatus)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdateRejectUpdateForBid(prID, ItemId, submitForBidStatus, dbConnection);
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

        public int UpdateItemQuantityFromBidSubmitting(int prID, int ItemId, decimal itemQuantity)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdateItemQuantityFromBidSubmitting(prID, ItemId, itemQuantity, dbConnection);
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

        public List<PrDetailsV2> GetPrDetailsForBidSubmission(int PrId, int CompanyId, int WarehouseId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.GetPrDetailsForBidSubmission(PrId, CompanyId, WarehouseId, dbConnection);
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

        public PrDetailsV2 GetPrDetails(int PrdId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.GetPrDetails(PrdId, CompanyId, dbConnection);
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
        public int UpdateStatus(int userId, int prdId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdateStatus(userId, prdId, dbConnection);
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
        public int UpdatePRStatuss(int userId, int prdId, string DetailStatus, string LogStatus) {
            DBConnection dbConnection = new DBConnection();
            try {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdatePRStatuss(userId, prdId, DetailStatus, LogStatus, dbConnection);
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
        public int UpdatePRStatus(int userId, int prdId, string DetailStatus) {
            DBConnection dbConnection = new DBConnection();
            try {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdatePRStatus(userId, prdId, DetailStatus,  dbConnection);
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
        public int UpdatePRStatusFoeCancelledPos(int userId, string DetailStatus, List<int> ItemIds, int PrID) {
            DBConnection dbConnection = new DBConnection();
            try {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdatePRStatusFoeCancelledPos(userId, DetailStatus, ItemIds, PrID, dbConnection);
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
        public int UpdateLogStatus(int userId, int prdId,  string LogStatus) {
            DBConnection dbConnection = new DBConnection();
            try {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdateLogStatus(userId, prdId, LogStatus, dbConnection);
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


        public int UpdateterminatedStatus(int userId, int prdId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.UpdateterminatedStatus(userId, prdId, dbConnection);
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
        public List<PR_Details> GetPrDetailsByPrId(int prId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                return pr_DetailDAO.GetPrDetailsByPRid(prId, companyId, dbConnection);
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
        //public int DeleteFromPO(int prId, List<int> itemId, int UserId) {
        //    DBConnection dbConnection = new DBConnection();
        //    try {
        //        PrMasterDAOV2 PrMasterDAOV2 = DAOFactory.CreatePrMasterDAOV2();
        //        return pr_DetailDAO.DeleteFromPO(prId, itemId, UserId, dbConnection);
        //    }
        //    catch (Exception) {
        //        dbConnection.RollBack();
        //        throw;
        //    }
        //    finally {
        //        if (dbConnection.con.State == System.Data.ConnectionState.Open) {
        //            dbConnection.Commit();
        //        }
        //    }
        //}
        
            public int SavePRDetails(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount) {
                DBConnection dbConnection = new DBConnection();
                try {
                    PR_DetailDAO pr_DetailDAO = DAOFactory.CreatePR_DetailDAO();
                    return pr_DetailDAO.SavePRDetails(PrId, ItemId, Unit, ItemDescription, ItemUpdatedBy, ItemUpdatedDateTime, IsActive, Replacement, ItemQuantity, Purpose, EstimatedAmount, dbConnection);
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
