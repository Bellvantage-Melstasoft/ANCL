using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public  interface SupplierQuotationController
    {
       int SaveQuatation(int itemId, int prId, int supplierId, decimal itemPrice, int isSelected, string remarks, decimal vatAmount, decimal nbtAmount, decimal totatlAmount, int isRejected, string reason, int approve, string SupplierTermsConditions, string BidOrderingNo, int isVatIncluded);
        int UpdateQuatationByQuotationId(int quotationId, decimal itemPrice, string remarks);
        List<SupplierQuotation> GetQuotationListByItemId(int itemId);
        List<SupplierQuotation> GetPendingBids();
        List<SupplierQuotation> GetCompletedBid();
        List<SupplierQuotation> GetPendingBidsForDashboard(int departmentID);
        List<SupplierQuotation> GetCompletedBidForDashboard(int departmentID);
        List<SupplierQuotation> GetDetailsBidComparison(int PrId, int companyId);
        List<SupplierQuotation> GetBidSupplierListForItem(int PRid,int itemId);
        int UpdateIsRejectedSupplier(int PrId, int ItemId, int SupplierId, string Reason, decimal CustomizeAmount, int selectedCount, int rejectedCount);
        List<SupplierQuotation> GetNecessaryDataForPO(int PrId);
        int UpdateIsApproveSupplier(int PrId, int ItemId, int SupplierId, decimal CustomizeAmount, int selectedCount, int rejectedCount);
        List<SupplierQuotation> GetIsApprovedCount(int PrId);
        List<SupplierQuotation> GetSupplierPendingBids(int SupplierId);
        int UpdatePendingBids(int quotationId, decimal PerItemPrice, decimal VatAmount, decimal NbtAmount, decimal TotalAmount, string TermsandConditions, int isVatInclude);
        List<SupplierQuotation> GetPendingCountOfSupplier(int PrId, int ItemId, int SupplierId);
        List<SupplierQuotation> GetAlreadyBidCountOfSupplier(int PrId, int ItemId, int SupplierId);
        int resetSelecteingSuppliert(int PrId, int ItemId);
        int UpdateIsRaisedPO(int PrId, int ItemId, int SupplierId, int isPoRaised);
        int UpdateIsPOApproved(int PrId, int ItemId, int SupplierId, int isPOApproved);
        List<SupplierQuotation> GetDetailsSubmitPO(int PrId);
        int UpdateIsRaisedPOAndIsPOApproved(int PrId, int ItemId, int SupplierId, int isPOApproved);
        List<SupplierQuotation> GetPendingBidsAndNotBid();

        SupplierQuotation GetGivenQuotatios(int PrId, int ItemId, int SupplierId);

       //--Reject PR
        List<SupplierQuotation> GetSuppliersList(int PRid, int itemId);
        int UpdateIsRaisedPOReject(int PrId, int ItemId, int SupplierId, int isPoRaised, string rejectedReason, int rejectedCount);
        List<SupplierQuotation> GetSupplierPrIdItemId(int PrId, int ItemId);
        int UpdateResetPoReject(int prId, int itemId);
        List<SupplierQuotation> GetManualPendingBidsAndNotBid();

        List<SupplierQuotation> GetCompletedManualBid();
        SupplierQuotation GetSupplierEditBidDetails(int PrId, int ItemId, int SupplierId);
        List<SupplierQuotation> GetBidSupplierForItem(int PRid, int itemId);
       int UpdateNegotiateAmount(int PrId, int ItemId, int SupplierId, decimal CustomizeAmount, int selectedCount, int rejectedCount, decimal VatAmount, decimal NbtAmount, decimal TotalAmount);

        

        //New Methods By Salman created on 2019-01-17
        List<SupplierQuotation> GetSupplierQuotations(int BidId);
        int SaveSupplierQuotation(SupplierQuotation quotation, ImportQuotation importQuotation ,int PurchaseType);
        int UpdateSupplierQuotation(SupplierQuotation quotation);
        int UpdateSupplierPendingQuotation(SupplierQuotation quotation);
        int SelectSupplierQuotationAtSelection(int QuotationId, string Remarks, int BidId, int UserId);
        int SelectSupplierQuotationAtApproval(int QuotationId, string Remarks);
        int SelectSupplierQuotationAtConfirmation(int QuotationId, string Remarks);
        int ResetSelections(int BidId);
        int RejectSupplierQuotationAtSelection(int QuotationId, string Remarks, int UserId);
        int RejectSupplierQuotationsAtApproval(int QuotationId, string Remarks, int BidId);
        int RejectSupplierQuotationsAtConfirmation(int QuotationId, string Remarks, int BidId);
        int ApproveSupplierQuotation(int QuotationId, string Remarks);
        int ConfirmSupplierQuotation(int QuotationId, string Remarks);
        SupplierQuotation GetSupplierQuotationForABid(int BidId, int SupplierId, int CompanyId);
        List<int> GetSelectableQuotationIdsForLoggedInUser(int UserId, int DesignationId, int CompanyId);
        List<int> GetSelectionPendingQuotationIdsForLoggedInUser(int UserId, int DesignationId, int CompanyId);
        int PopulateRecommendation(int QuotationId, int CategoryId, decimal NetTotal, int UserId, int DesignationId, string Remarks);
        List<int> GetRecommendableQuotations(int UserId, int DesignationId);
        int OverrideRecommendation(int QuotationId, int UserId, int DesignationId, string Remarks, int Status, int CategoryId, decimal Amount);
        List<int> GetApprovableQuotations(int UserId, int DesignationId);
        int OverrideApproval(int QuotationId, int UserId, int DesignationId, string Remarks, int Status);

        SupplierQuotationItem GetQuotationItemsByQuotationItemId(int QuotationItemId);
        SupplierQuotation GetSupplierQuotationbyQutationId(int QutationId);
        int DeleteSubmittedSupplierQuotation(int quotationId, int itemId, int quotationItemId, decimal subTotal, decimal vatAmount, decimal nbtAmount, decimal netTotal); 
        SupplierQuotation GetImportDetails(int poId, int CompanyId);
        List<SupplierQuotation> ConfirmRates(int BidId);
        List<SupplierQuotation> GetImportDetailsListForTabulationReview(int BidId);
        List<int> GetQuotationsByBidId(int BidId);
        int UpdateSupplierQuotationImports(SupplierQuotation quotation);
        SupplierQuotation GetSupplierQuotationsForCoveringPR(int BidId);
        List<SupplierQuotation> GetSupplierQuotationsImports(int BidId);
        }
        public class SupplierQuotationControllerImpl : SupplierQuotationController
    {

        public int resetSelecteingSuppliert(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.resetSelecteingSuppliert(PrId, ItemId, dbConnection);
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

        public List<SupplierQuotation> GetQuotationListByItemId(int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetQuotationListByItemId(itemId, dbConnection);
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

        public int SaveQuatation(int itemId, int prId, int supplierId, decimal itemPrice, int isSelected, string remarks, decimal vatAmount, decimal nbtAmount, decimal totatlAmount, int isRejected, string reason, int approve, string SupplierTermsConditions, string BidOrderingNo, int isVatIncluded)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.SaveQuatation(itemId, prId, supplierId, itemPrice, isSelected, remarks, vatAmount, nbtAmount, totatlAmount, isRejected, reason, approve,SupplierTermsConditions,BidOrderingNo,  isVatIncluded, dbConnection);
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

        public int UpdateQuatationByQuotationId(int quotationId, decimal itemPrice, string remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdateQuatationByQuotationId( quotationId,  itemPrice,  remarks,dbConnection);
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

        public List<SupplierQuotation> GetPendingBids()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetPendingBids(dbConnection);
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

        public List<SupplierQuotation> GetPendingBidsForDashboard(int departmentID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetPendingBidsForDashboard(departmentID,dbConnection);
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

        public List<SupplierQuotation> GetCompletedBid()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetCompletedBid(dbConnection);
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
        public List<SupplierQuotation> GetCompletedBidForDashboard(int departmentID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return null;// supplierQuotationDAO.GetCompletedBidForDashboard(departmentID,dbConnection);
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

        public List<SupplierQuotation> GetDetailsBidComparison(int PrId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetDetailsBidComparison(PrId, companyId,dbConnection);
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
        public List<SupplierQuotation> GetSupplierQuotationsImports(int BidId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetSupplierQuotationsImports(BidId,  dbConnection);
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

        public List<SupplierQuotation> GetBidSupplierListForItem(int PRid,int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetBidSupplierListForItem(PRid, itemId,dbConnection);
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

        public int UpdateIsRejectedSupplier(int PrId, int ItemId, int SupplierId, string Reason, decimal CustomizeAmount, int selectedCount, int rejectedCount)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdateIsRejectedSupplier(PrId, ItemId, SupplierId, Reason,CustomizeAmount,selectedCount,rejectedCount, dbConnection);
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

        public List<SupplierQuotation> GetNecessaryDataForPO(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetNecessaryDataForPO(PrId, dbConnection);
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

        public int UpdateIsApproveSupplier(int PrId, int ItemId, int SupplierId, decimal CustomizeAmount, int selectedCount, int rejectedCount)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdateIsApproveSupplier(PrId, ItemId,SupplierId,CustomizeAmount, selectedCount, rejectedCount, dbConnection);
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

        public List<SupplierQuotation> GetIsApprovedCount(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetIsApprovedCount(PrId, dbConnection);
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

        public List<SupplierQuotation> GetSupplierPendingBids(int SupplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetSupplierPendingBids(SupplierId, dbConnection);
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

        public int UpdatePendingBids(int quotationId, decimal PerItemPrice, decimal VatAmount, decimal NbtAmount, decimal TotalAmount, string TermsandConditions, int isVatInclude)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdatePendingBids(quotationId, PerItemPrice, VatAmount, NbtAmount, TotalAmount, TermsandConditions,  isVatInclude, dbConnection);
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

        public List<SupplierQuotation> GetPendingCountOfSupplier(int PrId, int ItemId, int SupplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetPendingCountOfSupplier(PrId,ItemId,SupplierId, dbConnection);
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

        public List<SupplierQuotation> GetAlreadyBidCountOfSupplier(int PrId, int ItemId, int SupplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetAlreadyBidCountOfSupplier(PrId, ItemId, SupplierId, dbConnection);
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

        public List<SupplierQuotation> ConfirmRates(int BidId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.ConfirmRates(BidId, dbConnection);
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

        public List<SupplierQuotation> GetImportDetailsListForTabulationReview(int BidId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetImportDetailsListForTabulationReview(BidId, dbConnection);
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

        public int UpdateIsRaisedPO(int PrId, int ItemId, int SupplierId, int isPoRaised)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdateIsRaisedPO(PrId,  ItemId,  SupplierId,  isPoRaised, dbConnection);
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

        public int UpdateIsPOApproved(int PrId, int ItemId, int SupplierId, int isPOApproved)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdateIsPOApproved(PrId, ItemId, SupplierId, isPOApproved, dbConnection);
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

        public List<SupplierQuotation> GetDetailsSubmitPO(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetDetailsSubmitPO(PrId, dbConnection);
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

        public SupplierQuotation GetSupplierQuotationsForCoveringPR(int BidId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetSupplierQuotationsForCoveringPR(BidId, dbConnection);
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

        public int UpdateIsRaisedPOAndIsPOApproved(int PrId, int ItemId, int SupplierId, int isPOApproved)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdateIsRaisedPOAndIsPOApproved(PrId, ItemId, SupplierId, isPOApproved, dbConnection);
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

        public List<SupplierQuotation> GetSuppliersList(int PRid, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetSuppliersList(PRid, itemId, dbConnection);
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

        public int UpdateIsRaisedPOReject(int PrId, int ItemId, int SupplierId, int isPoRaised, string rejectedReason, int rejectedCount)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdateIsRaisedPOReject(PrId, ItemId, SupplierId, isPoRaised, rejectedReason, rejectedCount, dbConnection);
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

        public List<SupplierQuotation> GetSupplierPrIdItemId(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetSupplierPrIdItemId(PrId, ItemId, dbConnection);
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

        public List<SupplierQuotation> GetPendingBidsAndNotBid()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetPendingBidsAndNotBid(dbConnection);
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

        public SupplierQuotation GetGivenQuotatios(int PrId, int ItemId, int SupplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetGivenQuotatios(PrId, ItemId, SupplierId, dbConnection);
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

        public int UpdateResetPoReject(int prId, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdateResetPoReject( prId,  itemId, dbConnection);
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

        public List<SupplierQuotation> GetManualPendingBidsAndNotBid()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetManualPendingBidsAndNotBid(dbConnection);
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

        public List<SupplierQuotation> GetCompletedManualBid()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetCompletedManualBid(dbConnection);
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

        public SupplierQuotation GetSupplierEditBidDetails(int PrId, int ItemId, int SupplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetSupplierEditBidDetails( PrId,  ItemId,  SupplierId, dbConnection);
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

        public List<SupplierQuotation> GetBidSupplierForItem(int PRid, int itemId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetBidSupplierForItem( PRid,  itemId, dbConnection);
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

        public int UpdateNegotiateAmount(int PrId, int ItemId, int SupplierId, decimal CustomizeAmount, int selectedCount, int rejectedCount, decimal VatAmount, decimal NbtAmount, decimal TotalAmount)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdateNegotiateAmount( PrId,  ItemId,  SupplierId,  CustomizeAmount,  selectedCount,  rejectedCount,  VatAmount,  NbtAmount,  TotalAmount, dbConnection);
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
        

        public int SaveSupplierQuotation(SupplierQuotation quotation, ImportQuotation importQuotation , int PurchaseType)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.SaveSupplierQuotation(quotation, importQuotation , PurchaseType, dbConnection);
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

        public int UpdateSupplierQuotation(SupplierQuotation quotation)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdateSupplierQuotation(quotation, dbConnection);                
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

        public int UpdateSupplierQuotationImports(SupplierQuotation quotation) {

            DBConnection dbConnection = new DBConnection();
            try {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdateSupplierQuotationImports(quotation, dbConnection);
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

        public List<SupplierQuotation> GetSupplierQuotations(int BidId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetSupplierQuotations(BidId, dbConnection);
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

        public int UpdateSupplierPendingQuotation(SupplierQuotation quotation)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.UpdateSupplierPendingQuotation(quotation, dbConnection);
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

        public int SelectSupplierQuotationAtSelection(int QuotationId, string Remarks, int BidId, int UserId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.SelectSupplierQuotationAtSelection(QuotationId, Remarks, BidId, UserId, dbConnection);
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

        public int SelectSupplierQuotationAtApproval(int QuotationId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.SelectSupplierQuotationAtApproval(QuotationId, Remarks, dbConnection);
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

        public int SelectSupplierQuotationAtConfirmation(int QuotationId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.SelectSupplierQuotationAtConfirmation(QuotationId, Remarks, dbConnection);
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

        public int ResetSelections(int BidId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.ResetSelections(BidId, dbConnection);
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

        public int RejectSupplierQuotationAtSelection(int QuotationId, string Remarks, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.RejectSupplierQuotationAtSelection(QuotationId, Remarks, UserId, dbConnection);
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

        public int RejectSupplierQuotationsAtApproval(int QuotationId, string Remarks, int BidId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.RejectSupplierQuotationsAtApproval(QuotationId, Remarks, BidId, dbConnection);
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

        public int RejectSupplierQuotationsAtConfirmation(int QuotationId, string Remarks, int BidId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.RejectSupplierQuotationsAtConfirmation(QuotationId, Remarks, BidId, dbConnection);
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

        public int ApproveSupplierQuotation(int QuotationId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.ApproveSupplierQuotation(QuotationId, Remarks, dbConnection);
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

        public int ConfirmSupplierQuotation(int QuotationId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.ConfirmSupplierQuotation(QuotationId, Remarks, dbConnection);
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

        public SupplierQuotation GetSupplierQuotationForABid(int BidId, int SupplierId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetSupplierQuotationForABid(BidId, SupplierId, CompanyId, dbConnection);
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

        public List<int> GetSelectableQuotationIdsForLoggedInUser(int UserId, int DesignationId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetSelectableQuotationIdsForLoggedInUser(UserId, DesignationId, CompanyId, dbConnection);
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

        public int PopulateRecommendation(int QuotationId, int CategoryId, decimal NetTotal, int UserId, int DesignationId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.PopulateRecommendation(QuotationId, CategoryId, NetTotal, UserId, DesignationId, Remarks, dbConnection);
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

        public List<int> GetSelectionPendingQuotationIdsForLoggedInUser(int UserId, int DesignationId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetSelectionPendingQuotationIdsForLoggedInUser(UserId, DesignationId, CompanyId, dbConnection);
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

        public List<int> GetRecommendableQuotations(int UserId, int DesignationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetRecommendableQuotations(UserId, DesignationId, dbConnection);
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

        public int OverrideRecommendation(int QuotationId, int UserId, int DesignationId, string Remarks, int Status, int CategoryId, decimal Amount)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.OverrideRecommendation(QuotationId, UserId, DesignationId, Remarks, Status, CategoryId, Amount, dbConnection);
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

        public int OverrideApproval(int QuotationId, int UserId, int DesignationId, string Remarks, int Status)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.OverrideApproval(QuotationId, UserId, DesignationId, Remarks, Status, dbConnection);
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

        public List<int> GetApprovableQuotations(int UserId, int DesignationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetApprovableQuotations(UserId, DesignationId, dbConnection);
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

        public List<int> GetQuotationsByBidId(int BidId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetQuotationsByBidId(BidId, dbConnection);
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

        public SupplierQuotationItem GetQuotationItemsByQuotationItemId(int QuotationItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationItemDAO supplierQuotationItemDAO = DAOFactory.CreateSupplierQuotationItemDAO();
                return supplierQuotationItemDAO.GetQuotationItemsByQuotationItemId(QuotationItemId, dbConnection);
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

        public SupplierQuotation GetSupplierQuotationbyQutationId(int QutationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetSupplierQuotationbyQutationId(QutationId, dbConnection);
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

        public int DeleteSubmittedSupplierQuotation(int quotationId, int itemId, int quotationItemId, decimal subTotal, decimal vatAmount, decimal nbtAmount, decimal netTotal)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.DeleteSubmittedSupplierQuotation(quotationId, itemId, quotationItemId, subTotal,  vatAmount, nbtAmount, netTotal, dbConnection);
            }
            catch (Exception )
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

        public SupplierQuotation GetImportDetails(int poId, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierQuotationDAO supplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();
                return supplierQuotationDAO.GetImportDetails(poId, CompanyId, dbConnection);
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
