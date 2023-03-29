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
    public interface MRNDetailController
    {
        int SaveMRNDetails(int MrnId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId);
        List<MrnDetails> FetchDetailsRejectedMRN(int MrnId, int companyId);
        List<MrnDetails> FetchMRN_DetailsByMRNIdList(int MrnId);
        int DeleteMRNDetailByMRNIDAndItemId(int MrnId, int itemId);

        int UpdateMRNDetails(int MrnId, int oldItemId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId);
        int StockKeeperIssueQuantity(int WarehouseId ,MrnDetailsV2 mrnDetail, MRNDIssueNote mrnIssueNotes ,int mrnStatus ,int updatedBy);
        int StoreKeeperAddToStock(MrnDetailsV2 mrnDetail,WarehouseInventoryRaise warehouseInventoryRaise, int CompanyId, int UserId, DateTime expDate);
        int StoreKeeperTerminateItem(int mrndId, int itemId, string remark,int updatedBy, int mrnID);
        List<MrnDetails> FetchFullyIssuedMrnDetails(int MrnId);
        MrnDetails GetMrnDetailsByMrndId(int MrndId);
        string GetMrnStatusByStatusId(int statusID);
    }
    public class MRNDetailControllerImpl : MRNDetailController
    {
        public int SaveMRNDetails(int MrnId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDetailDAO mrnDetailDAO = DAOFactory.CreateMRNDetailDAO();
                return mrnDetailDAO.SaveMRNDetails(MrnId, ItemId, Unit, ItemDescription, ItemUpdatedBy, ItemUpdatedDateTime, IsActive, Replacement, ItemQuantity, Purpose, EstimatedAmount, FileSampleProvided, Remarks, MeasurementId, dbConnection);
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


        public List<MrnDetails> FetchDetailsRejectedMRN(int MrnId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDetailDAO mrnDetailDAO = DAOFactory.CreateMRNDetailDAO();
                return mrnDetailDAO.FetchDetailsRejectedMRN(MrnId, companyId, dbConnection);
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

        public List<MrnDetails> FetchMRN_DetailsByMRNIdList(int MrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDetailDAO mrnDetailDAO = DAOFactory.CreateMRNDetailDAO();
                return mrnDetailDAO.FetchMRN_DetailsByMRNIdList(MrnId, dbConnection);
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
        public int DeleteMRNDetailByMRNIDAndItemId(int MrnId, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDetailDAO mrnDetailDAO = DAOFactory.CreateMRNDetailDAO();
                return mrnDetailDAO.DeleteMRNDetailByMRNIDAndItemId(MrnId, itemId, dbConnection);
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

        public int UpdateMRNDetails(int MrnId, int oldItemId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDetailDAO mrnDetailDAO = DAOFactory.CreateMRNDetailDAO();
                return mrnDetailDAO.UpdateMRNDetails(MrnId, oldItemId, ItemId, Unit, ItemDescription, ItemUpdatedBy, ItemUpdatedDateTime, IsActive, Replacement, ItemQuantity, Purpose, EstimatedAmount, FileSampleProvided, Remarks, MeasurementId, dbConnection);
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

        public int StockKeeperIssueQuantity(int WarehouseId, MrnDetailsV2 mrnDetail, MRNDIssueNote mrnIssueNotes, int mrnStatus, int updatedBy)
        {
            DBConnection dbConnection = null;
            try
            {   
                dbConnection = new DBConnection();
                // Insert record to mrnIssueNote
                MRNDIssueNoteDAOInterface mrndIssueNoteDAO = DAOFactory.CreateMRNDIssueNoteDAO();
                int issueNoteId = mrndIssueNoteDAO.addNewIssueNote(mrnIssueNotes , dbConnection);
                //update mrnDetails
                MRNDetailDAO mrnDetailDAO = DAOFactory.CreateMRNDetailDAO();
                mrnDetailDAO.UpdateMrnDetailIssueStock(mrnDetail, dbConnection);
                // Update Warehouse Inventory
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                warehouseDAO.UpdateWarehouseInventory(WarehouseId,mrnDetail, updatedBy, dbConnection);
                // Update Company Stock
                InventoryDAOInterface inventoryDAO = DAOFactory.CreateInventoryDAO();
                inventoryDAO.updateCompanyStock(WarehouseId, mrnDetail, updatedBy, dbConnection);
                //keeing status as 2  - Here when issued its considered as delivered - inserting table WAREHOUSE_INVENTORY_RELEASE
             //inventoryDAO.updateWarehouseStockAfterDelivered(issueNoteId, WarehouseId,mrnDetail.ItemId,mrnDetail.IssuedQty,updatedBy,dbConnection);
                // Update MrnMaster  --> if all mrn detail items are fully issued then mrn master table status is set to one
                MrnMasterDAOV2 mrnMasterDAOV = DAOFactory.CreateMrnMasterDAOV2();
                mrnMasterDAOV.UpdateMrnStatus(mrnDetail.MrnId, mrnStatus, dbConnection);
                return 1;
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                dbConnection.Commit();
            }
        }

        public int StoreKeeperAddToStock(MrnDetailsV2 mrnDetail, WarehouseInventoryRaise warehouseInventoryRaise, int CompanyId, int UserId, DateTime expDate)
        {
            DBConnection dbConnection = null;
            try
            {
                dbConnection = new DBConnection();
                // insert record to WarehouseInventoryRaise - 
                // In same method insert into WAREHOUSE_INVENTORY_MASTER(if record doesnt exist) else update if record exist & avaialable qty is zero.
                InventoryDAOInterface inventoryDAO = DAOFactory.CreateInventoryDAO();
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();

                inventoryDAO.raiseWarehouseStockInMrnManual(warehouseInventoryRaise, expDate, dbConnection);
                // Update Company Stock
                inventoryDAO.raiseCompanyStockInMrn(CompanyId, warehouseInventoryRaise.ItemID, warehouseInventoryRaise.RaisedQty, warehouseInventoryRaise.StockValue, warehouseInventoryRaise.RaisedBy, dbConnection);
                mrnDetailStatusLogDAO.InsertLogAddStock(mrnDetail.MrndId, UserId, dbConnection);

                return 1;
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                dbConnection.Commit();
            }
        }

        public List<MrnDetails> FetchFullyIssuedMrnDetails(int MrnId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailDAO mrnDetailDAO = DAOFactory.CreateMRNDetailDAO();
                return mrnDetailDAO.FetchFullyIssuedMrnDetails(MrnId, dbConnection);
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

        public MrnDetails GetMrnDetailsByMrndId(int MrndId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailDAO mrnDetailDAO = DAOFactory.CreateMRNDetailDAO();
                return mrnDetailDAO.GetMrnDetailsByMrndId(MrndId, dbConnection);
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

        public string GetMrnStatusByStatusId(int statusID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDetailDAO mrnDetailDAO = DAOFactory.CreateMRNDetailDAO();
                return mrnDetailDAO.GetMrnStatusByStatusId(statusID, dbConnection);
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

        public int StoreKeeperTerminateItem(int mrndId, int itemId, string remark, int updatedBy, int mrnID)
        {

            DBConnection dbConnection = null;
            try
            {
                dbConnection = new DBConnection();
                
                MRNDetailDAO mrnDetailDAO = DAOFactory.CreateMRNDetailDAO();
                mrnDetailDAO.TerminateItem(mrndId, itemId, remark, updatedBy, mrnID, dbConnection);              
                return 1;
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                dbConnection.Commit();
            }
        }
    }
}
