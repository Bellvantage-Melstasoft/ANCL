using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface TabulationMasterController
    {
        List<TabulationMaster> GetTabulationsByBidId(int BidId);
        int PopulateRecommendation(int TabulationId, int CategoryId, decimal NetTotal, int UserId, int DesignationId, string Remarks, int Purchasetype);
        List<int> GetRecommendableTabulations(int UserId, int DesignationId);
        int OverrideTabulationRecommandation(int TabulationId, int UserId, int DesignationId, string Remarks, int Status, int CategoryId, decimal Amount,int PurchaseType);
        List<int> GetApprovableTabulations(int UserId, int DesignationId);
        int OverrideApproval(int TabulationId, int UserId, int DesignationId, string Remarks, int Status);
        int InsertTabulationMaster(int prid, int bidId, int userId,int MesurementId);
        int UpdateTabulationMasterNetTotal(int tabulationId, int prid, int bidId, int userId, string remarks);
        int UpdateTabulationMasterNetTotalAfterUpdate(int tabulationId, int prid, int bidId, int userId, string remarks);
        TabulationMaster GetTabulationsByTabulationId(int TabulationId);
        List<TabulationMaster> GetTabulationsRejectedTabulationsByPrId(int PrId);
        bool CheckApprovalLimitExist(int tabulationId, int itemCategoryId);
        int UpdateTabulationDetails(int UserId, decimal totVAt, decimal totNbt, decimal totNetTot, decimal totSubTot, int BidId, int TabulationId, int PrId, int CategoryId, int DesignationId,string finalizedRemark, List<TabulationDetail> TabulationDetailsList, int Purchasetype);
        int OverrideRecommendation(int recommandationId, int TabulationId, int UserId, int DesignationId, string Remarks, int Status, int CategoryId, decimal Amount);
        List<TabulationMaster> GetTabulationsByBidIdForPurchaseRequisitionReport(List<int> BidId);
        List<TabulationRecommendation> tabulationIdListForPurchadeRequisitionReport(int TabulationId);
        List<int> GetRejectedRecommendableTabulations(int UserId, int DesignationId);
        List<int> GetRejectedApprovableTabulations(int UserId, int DesignationId);
        int CloneBid(int bidId, int prId, int Userid, int openDays);
        int TerminaeBid(int bidId);
        int UpdateTabulationDetailsImport(int UserId, int BidId, int TabulationId, int PrId, int CategoryId, int DesignationId, string finalizedRemark, int PurchaseType);
        int CloneBidImport(int bidId, int prId, int Userid, int openDays);
        int UpdateTabulationForCoveringPR(SupplierQuotation supplierQuotation, List<SupplierQuotationItem> supplierQuotationItem, int UserId, string Remark, int BidId, int CategoryId, int PurchaseType, int DesignationId);
    }

        public class TabulationMasterControllerImpl : TabulationMasterController
    {
        public List<int> GetApprovableTabulations(int UserId, int DesignationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.GetApprovableTabulations(UserId, DesignationId, dbConnection);
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
        public List<int> GetRejectedApprovableTabulations(int UserId, int DesignationId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.GetRejectedApprovableTabulations(UserId, DesignationId, dbConnection);
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

        public List<int> GetRecommendableTabulations(int UserId, int DesignationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.GetRecommendableTabulations(UserId, DesignationId, dbConnection);
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

        public List<int> GetRejectedRecommendableTabulations(int UserId, int DesignationId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.GetRejectedRecommendableTabulations(UserId, DesignationId, dbConnection);
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

        public int CloneBid(int bidId, int prId, int Userid, int openDays) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.CloneBid(bidId, prId, Userid, openDays, dbConnection);
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

        public int CloneBidImport(int bidId, int prId, int Userid, int openDays) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.CloneBidImport(bidId, prId, Userid, openDays, dbConnection);
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

        public int TerminaeBid(int bidId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.TerminaeBid(bidId, dbConnection);
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

        public List<TabulationMaster> GetTabulationsByBidIdForPurchaseRequisitionReport(List<int> BidId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.GetTabulationsByBidIdForPurchaseRequisitionReport(BidId, dbConnection);
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

        public List<TabulationMaster> GetTabulationsByBidId(int BidId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.GetTabulationsByBidId(BidId, dbConnection);
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

        public List<TabulationRecommendation> tabulationIdListForPurchadeRequisitionReport(int TabulationId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationRecommendationDAO DAO = DAOFactory.CreateTabulationRecommendationDAO();
                return DAO.tabulationIdListForPurchadeRequisitionReport(TabulationId, dbConnection);
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

        public int OverrideApproval(int TabulationId, int UserId, int DesignationId, string Remarks, int Status)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.OverrideApproval(TabulationId, UserId, DesignationId, Remarks, Status, dbConnection);
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

        public int OverrideTabulationRecommandation(int TabulationId, int UserId, int DesignationId, string Remarks, int Status, int CategoryId, decimal Amount, int PurchaseType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.OverrideTabulationRecommandation(TabulationId, UserId, DesignationId, Remarks, Status, CategoryId, Amount, PurchaseType, dbConnection);
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
        public int OverrideRecommendation(int recommandationId, int TabulationId, int UserId, int DesignationId, string Remarks, int Status, int CategoryId, decimal Amount) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.OverrideRecommendation(recommandationId, TabulationId, UserId, DesignationId, Remarks, Status, CategoryId, Amount, dbConnection);
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

        public int PopulateRecommendation(int TabulationId, int CategoryId, decimal NetTotal, int UserId, int DesignationId, string Remarks,  int Purchasetype)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.PopulateRecommendation(TabulationId, CategoryId, NetTotal, UserId, DesignationId, Remarks, Purchasetype, dbConnection);
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
        public int InsertTabulationMaster(int prid, int bidId, int userId,int MesurementId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationMasterDAO tabulationDAO = DAOFactory.CreateTabulationMasterDAO();
                return tabulationDAO.InsertTabulationMaster(prid, bidId, userId, MesurementId ,dbConnection);

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

        public int UpdateTabulationMasterNetTotal(int tabulationId, int prid, int bidId, int userId, string remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationMasterDAO tabulationDAO = DAOFactory.CreateTabulationMasterDAO();
                return tabulationDAO.UpdateTabulationMasterNetTotal(tabulationId,prid, bidId, userId, remarks, dbConnection);

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

        public int UpdateTabulationMasterNetTotalAfterUpdate(int tabulationId, int prid, int bidId, int userId, string remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationMasterDAO tabulationDAO = DAOFactory.CreateTabulationMasterDAO();
                return tabulationDAO.UpdateTabulationMasterNetTotalAfterUpdate(tabulationId, prid, bidId, userId, remarks, dbConnection);

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

        public int UpdateTabulationForCoveringPR(SupplierQuotation supplierQuotation, List<SupplierQuotationItem> supplierQuotationItem, int UserId, string Remark, int BidId, int CategoryId, int PurchaseType, int DesignationId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationMasterDAO tabulationDAO = DAOFactory.CreateTabulationMasterDAO();
                TabulationDetailDAO tabulationDetailDAO = DAOFactory.CreateTabulationDetailDAO();
                BiddingDAO biddingDAO = DAOFactory.CreateBiddingDAO();

                int TabulationId =  tabulationDAO.UpdateTabulationMasterForCoveringPR(supplierQuotation, UserId, Remark, dbConnection);
                if (TabulationId > 0) {
                    int Result = tabulationDetailDAO.UpdateTabulationDetailForCoveringPR(supplierQuotationItem, UserId, Remark, dbConnection);
                    if (Result > 0) {
                        bool approvalLimitExist = tabulationDAO.CheckApprovalLimitExist(TabulationId, CategoryId, dbConnection);
                        if (approvalLimitExist) {
                            int IsUpdated = tabulationDAO.UpdateBiddingForCoveringPR(UserId, BidId, dbConnection);

                            if (IsUpdated > 0) {
                                var tabmaseter = tabulationDAO.GetTabulationsByBidId(BidId, dbConnection);
                                int Ispoulated = tabulationDAO.PopulateRecommendation(TabulationId, CategoryId, tabmaseter.Where(x => x.TabulationId == TabulationId).FirstOrDefault().NetTotal, UserId, DesignationId, Remark, PurchaseType, dbConnection);

                                if (Ispoulated > 0) {
                                    return 1;
                                }
                                else {
                                    dbConnection.RollBack();
                                    return -5;
                                }
                            }
                            else {
                                dbConnection.RollBack();
                                return -4;
                            }
                        }
                        else {
                            dbConnection.RollBack();
                            return -3;
                        }
                    }
                    else {
                        dbConnection.RollBack();
                        return -2;
                    }
                }
                else {
                    dbConnection.RollBack();
                    return -1;
                }

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

        public TabulationMaster GetTabulationsByTabulationId(int TabulationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationMasterDAO tabulationDAO = DAOFactory.CreateTabulationMasterDAO();
                return tabulationDAO.GetTabulationsByTabulationId(TabulationId, dbConnection);

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

        public List<TabulationMaster> GetTabulationsRejectedTabulationsByPrId(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationMasterDAO tabulationDAO = DAOFactory.CreateTabulationMasterDAO();
                return tabulationDAO.GetTabulationsRejectedTabulationsByPrId(PrId, dbConnection);

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

        public bool CheckApprovalLimitExist(int tabulationId,int itemCategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                 
                   TabulationMasterDAO DAO = DAOFactory.CreateTabulationMasterDAO();
                return DAO.CheckApprovalLimitExist(tabulationId,itemCategoryId, dbConnection);
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

        public int UpdateTabulationDetails(int UserId, decimal totVAt, decimal totNbt, decimal totNetTot, decimal totSubTot, int BidId, int TabulationId, int PrId, int CategoryId, int DesignationId, string finalizedRemark, List<TabulationDetail> TabulationDetailsList, int Purchasetype) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationMasterDAO tabulationMasterDAO = DAOFactory.CreateTabulationMasterDAO();
                TabulationDetailDAO tabulationDetailDAO = DAOFactory.CreateTabulationDetailDAO();

                int result = 0;
                //for (int i = 0; i < TabulationDetailsList.Count; i++) {
                    //result = tabulationDetailDAO.UpdateTabulationDetail(TabulationDetailsList[i].TotQty, TabulationDetailsList[i].VAtAmount, TabulationDetailsList[i].NbtAmount, TabulationDetailsList[i].NetTotal, TabulationDetailsList[i].SubTotal, TabulationDetailsList[i].ItemId, TabulationDetailsList[i].TabulationId, TabulationDetailsList[i].QuotationId, TabulationDetailsList[i].SupplierId, TabulationDetailsList[i].ApprovalRemark, (TabulationDetailsList[i].SupplierMentionedItemName != null ? TabulationDetailsList[i].SupplierMentionedItemName : ""),0, dbConnection);
   
                //}
                //if (result > 0) {
                    bool approvalLimitExist = tabulationMasterDAO.CheckApprovalLimitExist(TabulationId, CategoryId, dbConnection);

                    
                    if (approvalLimitExist) {
                        int IsUpdated = tabulationMasterDAO.UpdateTabulationMaster(UserId, totVAt, totNbt, totNetTot, totSubTot, BidId, TabulationId, PrId, finalizedRemark,dbConnection);

                        if (IsUpdated > 0) {
                            var tabmaseter = tabulationMasterDAO.GetTabulationsByBidId(BidId, dbConnection);
                             int Ispoulated = tabulationMasterDAO.PopulateRecommendation(TabulationId, CategoryId, tabmaseter.Where(x => x.TabulationId == TabulationId).FirstOrDefault().NetTotal, UserId, DesignationId, finalizedRemark, Purchasetype, dbConnection);

                            if (Ispoulated > 0) {
                                return 1;
                            }
                            else {
                                dbConnection.RollBack();
                                return -4;
                            }
                        }
                        else {
                            dbConnection.RollBack();
                            return -3;
                        }
                    }
                    else {
                        dbConnection.RollBack();
                        return -2;
                    }

                //}
                //else {
                //    dbConnection.RollBack();
                //    return -1;
                //}
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
        public int UpdateTabulationDetailsImport(int UserId, int BidId, int TabulationId, int PrId, int CategoryId, int DesignationId, string finalizedRemark,  int Purchasetype) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationMasterDAO tabulationMasterDAO = DAOFactory.CreateTabulationMasterDAO();
                TabulationDetailDAO tabulationDetailDAO = DAOFactory.CreateTabulationDetailDAO();

                int result = 0;
                //for (int i = 0; i < TabulationDetailsList.Count; i++) {
                //result = tabulationDetailDAO.UpdateTabulationDetail(TabulationDetailsList[i].TotQty, TabulationDetailsList[i].VAtAmount, TabulationDetailsList[i].NbtAmount, TabulationDetailsList[i].NetTotal, TabulationDetailsList[i].SubTotal, TabulationDetailsList[i].ItemId, TabulationDetailsList[i].TabulationId, TabulationDetailsList[i].QuotationId, TabulationDetailsList[i].SupplierId, TabulationDetailsList[i].ApprovalRemark, (TabulationDetailsList[i].SupplierMentionedItemName != null ? TabulationDetailsList[i].SupplierMentionedItemName : ""),0, dbConnection);

                //}
                //if (result > 0) {
                bool approvalLimitExist = tabulationMasterDAO.CheckApprovalImportLimitExist(TabulationId, CategoryId, dbConnection);


                if (approvalLimitExist) {
                    int IsUpdated = tabulationMasterDAO.UpdateTabulationMasterImport(UserId, BidId, TabulationId, PrId, finalizedRemark, dbConnection);

                    if (IsUpdated > 0) {
                        var tabmaseter = tabulationMasterDAO.GetTabulationsByBidId(BidId, dbConnection);
                        int Ispoulated = tabulationMasterDAO.PopulateRecommendation(TabulationId, CategoryId, tabmaseter.Where(x => x.TabulationId == TabulationId).FirstOrDefault().NetTotal, UserId, DesignationId, finalizedRemark, Purchasetype, dbConnection);

                        if (Ispoulated > 0) {
                            return 1;
                        }
                        else {
                            dbConnection.RollBack();
                            return -4;
                        }
                    }
                    else {
                        dbConnection.RollBack();
                        return -3;
                    }
                }
                else {
                    dbConnection.RollBack();
                    return -2;
                }

                //}
                //else {
                //    dbConnection.RollBack();
                //    return -1;
                //}
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


    }
}
