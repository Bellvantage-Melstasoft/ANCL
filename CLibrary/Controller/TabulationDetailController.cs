using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface TabulationDetailController

 

    {
        int InsertTabulationDetails(int tabulationId, int bidid);
        int UpdateTabulationDetails(int tabulationId, int qutationId, int bidid, int supplierId, int itemId, int qty);
        int UpdateUnselectedTabulationDetails(int tabulationId, int qutationId, int bidid, int supplierId, int itemId, int qty);
        List<TabulationDetail> GetTabulationDetailsByTabulationId(int TabulationId);
        List<TabulationDetail> GetTabulationDetailsByQuotationId(List<int> quotationIdList);
        int TerminateItems(List<int> TabulationDetailIds, int UserId, string Remarks);

        int UpdateTabulationDetail(int userID, decimal Qty, decimal VAtAmount, decimal NbtAmount, decimal NetTotal, decimal SubTotal, int ItemId, int TabulationId, int QuotationId, int SupplierId, string remark, string SupMentionedName, int Finalized, int Bidid, int PRID);
        int  DeleteTabulationDetailImports(int ItemId, int TabulationId, int QuotationId, int SupplierId, int Finalized, int userId, int BidId, int PrId);
        List<TabulationDetail> GetSelectedStatus(int QuotationId, int SupplierId, int ItemID, int QuotationItemId);
        int InsertIntoImportDetails(ImportCalucationDetails objImportDetails, int TabulationID, int QuotationItemId);

        int UpdateTabulationDetailImports(int userID, decimal Qty, decimal VAtAmount, decimal NbtAmount, decimal NetTotal, decimal SubTotal, int ItemId, int TabulationId,
            int QuotationId,  string remark, int PRID, int Bidid, decimal UnitPriceForeign, decimal UnitPriceLKR, String PurchaseType, decimal DayNo);

        int DeleteTabulationDetail(int ItemId, int TabulationId, int QuotationId, int SupplierId, int Finalized, int userId, int BidId, int PrId, TabulationDetail TabulationDet);
        List <TabulationDetail> getSelectedQuotationDetails(int ItemId, int TabulationId, int QuotationId, int SupplierId);
        TabulationDetail GetSelectedStatusForTabulation(int QuotationId, int SupplierId, int ItemID, int QuotationItemId);
        List<TabulationDetail> GetTabulationDetail(int TabulationId);
        TabulationDetail GetSelectedQuotation(int QuotationId, int SupplierId, int ItemID, int QuotationItemId);
        int DeleteSelectedQuotation(decimal SubTotal, decimal Nettotal, decimal Vat, decimal Nbt, int QuotationId,  int TabulationId, int ItemId,  int userId, int PrId, int BidId);
        List<TabulationDetail> GetTabulationDetailImports(int TabulationId);
        int updatePayementType(int QuotationId);
        int UpdateUnitPrice(int QuotationId, int QuotationItemId, string term, decimal airFreigt, decimal insuarance);
}

    public class TabulationDetailControllerImpl : TabulationDetailController
    {
        public List<TabulationDetail> GetTabulationDetailsByTabulationId(int TabulationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.GetTabulationDetailsByTabulationId(TabulationId,dbConnection);

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

        public List<TabulationDetail> GetTabulationDetail(int TabulationId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.GetTabulationDetail(TabulationId, dbConnection);

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

        public int InsertTabulationDetails(int tabulationId, int bidid)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.InsertTabulationDetails(tabulationId, bidid, dbConnection);

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


        public int updatePayementType(int QuotationId) {

            DBConnection dbConnection = new DBConnection();
            try {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.updatePayementType(QuotationId, dbConnection);

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
        public int UpdateUnitPrice(int QuotationId, int QuotationItemId, string term, decimal airFreigt, decimal insuarance) {

            DBConnection dbConnection = new DBConnection();
            try {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.UpdateUnitPrice(QuotationId, QuotationItemId, term, airFreigt, insuarance, dbConnection);

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
        public int UpdateTabulationDetails(int tabulationId, int qutationId, int bidid, int supplierId, int itemId, int qty)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.UpdateTabulationDetails( tabulationId,  qutationId,  bidid,  supplierId,  itemId,  qty, dbConnection);

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

        public int UpdateUnselectedTabulationDetails(int tabulationId, int qutationId, int bidid, int supplierId, int itemId, int qty)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.UpdateUnselectedTabulationDetails(tabulationId, qutationId, bidid, supplierId, itemId, qty, dbConnection);

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

        public List<TabulationDetail> GetTabulationDetailsByQuotationId(List<int> quotationIdList) { 
        DBConnection dbConnection = new DBConnection();
            try {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.GetTabulationDetailsByQuotationId(quotationIdList, dbConnection);

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

        public int TerminateItems(List<int> TabulationDetailIds, int UserId, string Remarks) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                PrDetailsDAOV2 prDetailsDAOV2 = DAOFactory.CreatePrDetailsDAOV2();

                int result = tabulationDAO.TerminateItems(TabulationDetailIds, UserId, Remarks, dbConnection);
                if (result > 0) {

                    result = prDetailsDAOV2.UpdatePrDetailsStatus(TabulationDetailIds, dbConnection);
                    if (result > 0) {
                        return 1;
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


        public int UpdateTabulationDetail(int userID,decimal Qty, decimal VAtAmount, decimal NbtAmount, decimal NetTotal, decimal SubTotal, int ItemId, int TabulationId, int QuotationId, int SupplierId, string remark, string SupMentionedName, int Finalized,int Bidid,int PRID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                int Result = 0;
                int Finalresult = 0;
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                TabulationMasterDAO tabulationMasterDAO = DAOFactory.CreateTabulationMasterDAO();
                Result= tabulationDAO.UpdateTabulationDetail(Qty, VAtAmount, NbtAmount, NetTotal, SubTotal, ItemId, TabulationId, QuotationId, SupplierId, remark, SupMentionedName, Finalized, userID, dbConnection);

                if(Result > 0)
                {
                    Finalresult= tabulationMasterDAO.UpdateTabulationMasterNew(userID, VAtAmount, NbtAmount, NetTotal, SubTotal, Bidid, TabulationId, PRID, Finalized, dbConnection);
                }
                return Finalresult;
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

        public int DeleteTabulationDetailImports(int ItemId, int TabulationId, int QuotationId, int SupplierId, int Finalized,int userId,int BidId,int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                int result = 0;
                int finalresult = 0;
                int ImportResult = 0;
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                TabulationMasterDAO tabulationMasterDAO = DAOFactory.CreateTabulationMasterDAO();
                result = tabulationDAO.DeleteTabulationDetail( ItemId, TabulationId, QuotationId, SupplierId, Finalized, dbConnection);
                if(result> 0)
                {
                    finalresult = tabulationMasterDAO.DeleteTabulationMaster(userId, BidId, TabulationId, PrId, Finalized, dbConnection);
                    if(finalresult> 0)
                    {
                        ImportResult = tabulationDAO.DeleteImportDetails(QuotationId, TabulationId, dbConnection);
                    }
                }
                return ImportResult;
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

        public List<TabulationDetail> GetSelectedStatus(int QuotationId, int SupplierId, int ItemID,int QuotationItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.GetSelectedStatus(QuotationId, SupplierId, ItemID, QuotationItemId, dbConnection);

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

        public TabulationDetail GetSelectedQuotation(int QuotationId, int SupplierId, int ItemID, int QuotationItemId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.GetSelectedQuotation(QuotationId, SupplierId, ItemID, QuotationItemId, dbConnection);

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
        public TabulationDetail GetSelectedStatusForTabulation(int QuotationId, int SupplierId, int ItemID, int QuotationItemId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.GetSelectedStatusForTabulation(QuotationId, SupplierId, ItemID, QuotationItemId, dbConnection);

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

        public int InsertIntoImportDetails(ImportCalucationDetails objImportDetails, int TabulationID,int QuotationItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.InsertIntoImportDetails(objImportDetails, TabulationID, QuotationItemId, dbConnection);

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

       

            public int DeleteSelectedQuotation(decimal SubTotal, decimal Nettotal, decimal Vat, decimal Nbt,int QuotationId,  int TabulationId, int ItemId,  int userId, int PrId, int BidId) { 
            DBConnection dbConnection = new DBConnection();
            try {
                int Result = 0;
                int Finalresult = 0;
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                TabulationMasterDAO tabulationMasterDAO = DAOFactory.CreateTabulationMasterDAO();
                Result = tabulationDAO.DeleteTabulationDetailImport(ItemId, TabulationId, QuotationId,  userId, dbConnection);

                if (Result > 0) {
                    Finalresult = tabulationMasterDAO.DeleteTabulationMasterNewImports(userId, Vat, Nbt, Nettotal, SubTotal, BidId, TabulationId, PrId, dbConnection);

                }
                return Finalresult;
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

        public int UpdateTabulationDetailImports(int userID, decimal Qty, decimal VAtAmount, decimal NbtAmount, decimal NetTotal, decimal SubTotal, int ItemId, int TabulationId, 
            int QuotationId,  string remark, int PRID, int Bidid, decimal UnitPriceForeign, decimal UnitPriceLKR, String PurchaseType, decimal DayNo)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                int Result = 0;
                int Finalresult = 0;
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                TabulationMasterDAO tabulationMasterDAO = DAOFactory.CreateTabulationMasterDAO();
                Result = tabulationDAO.UpdateTabulationDetailImport(Qty, VAtAmount, NbtAmount, NetTotal, SubTotal, ItemId, TabulationId, QuotationId, remark, userID, UnitPriceForeign, UnitPriceLKR, PurchaseType, DayNo, dbConnection);

                if (Result > 0)
                {
                    Finalresult = tabulationMasterDAO.UpdateTabulationMasterNewImports(userID, VAtAmount, NbtAmount, NetTotal, SubTotal, Bidid, TabulationId, PRID,  dbConnection);

                    //if(Finalresult > 0)
                    //{
                    //    ImportResult = tabulationDAO.InsertIntoImportDetails(objImportCalDet, TabulationId, QutationItemId, dbConnection);
                    //}
                }
                return Finalresult;
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


        public int DeleteTabulationDetail(int ItemId, int TabulationId, int QuotationId, int SupplierId, int Finalized, int userId, int BidId, int PrId, TabulationDetail TabulationDet)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                int result = 0;
                int finalresult = 0;
                int ImportResult = 0;
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                TabulationMasterDAO tabulationMasterDAO = DAOFactory.CreateTabulationMasterDAO();
                result = tabulationDAO.DeleteTabulationDetail(ItemId, TabulationId, QuotationId, SupplierId, Finalized, dbConnection);
                if (result > 0)
                {
                    //finalresult = tabulationMasterDAO.DeleteTabulationMaster(userId, BidId, TabulationId, PrId, Finalized, dbConnection);
                    finalresult = tabulationMasterDAO.UpdateDeletedTabulationMaster(TabulationId, PrId, TabulationDet.SubTotal, TabulationDet.VAtAmount, TabulationDet.NbtAmount, TabulationDet.NetTotal, BidId, dbConnection);

                }
                return finalresult;
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

        public List<TabulationDetail> getSelectedQuotationDetails(int ItemId, int TabulationId, int QuotationId, int SupplierId)
        {
            DBConnection dbConnection = new DBConnection();
            List<TabulationDetail> result = new List<TabulationDetail>();
            try
            {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                result = tabulationDAO.getSelectedQuotationDetails(ItemId, TabulationId, QuotationId, SupplierId, dbConnection);                
            }
            catch(Exception ex)
            {

            }
            return result;
        }

        public List<TabulationDetail> GetTabulationDetailImports(int TabulationId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TabulationDetailDAO tabulationDAO = DAOFactory.CreateTabulationDetailDAO();
                return tabulationDAO.GetTabulationDetailImports(TabulationId, dbConnection);

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
