using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Helper;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CLibrary.Controller
{
    public interface InventoryControllerInterface
    {
        int addNewCompanyStock(int companyID, int itemID, int reOderedLevel, int addedBy);
        List<Inventory> fetchWarehouseInventory(int companyID, int itemID);
        int updateCompanyStockAfterIssue(int companyID, int itemID, decimal issuedQty, int updatedBy, decimal IssuedValue);
        int updateWarehouseStockAfterIssue(List<Inventory> inventoryObjs);
        //change
        int updateWarehouseStockAfterDelivered(int mrndInID, int warehouseID, int itemID, decimal DeliveredQty, int updatedBy, decimal deliveredStockValue, List<IssuedInventoryBatches> Batches);
        int releaseStockFromWarehouseToWarehouse(WarehouseInventoryRelease inventoryObj);
        int raiseCompanyStockFromGRN(int companyID, int itemID, int receivedQty, decimal stockValue, int raisedBy);
        int raiseWarehouseStockFromGRN(List<WarehouseInventoryRaise> inventoryList);
        List<WarehouseInventory> fetchWarehouseItems(int warehouseID);
        List<Inventory> fetchWarehouseInventorybyWarehouseId(int companyID, int warehouseId, int itemID);
        int raiseWarehouseStockInMrn(WarehouseInventoryRaise inventory);
        int raiseCompanyStockInMrn(int companyID, int itemID, int receivedQty, decimal stockValue, int raisedBy);
        int updateWarehouseStockAfterIssuesformonewarehouse(Inventory inventoryObjList);
        int raiseWarehouseStockInMrnManual(WarehouseInventoryRaise inventory, DateTime expdate);
        int updateWarehouseStock(List<WarehouseInventory> inventoryList, int UserId, int ItemId, int CompanyId, string Remarks, List<WarehouseInventoryBatches> deletedBatches, int StockMAintainingType, List<WarehouseInventoryBatches> addedBatches, List<WarehouseInventoryBatches> batchList);
        decimal GetWarehouseInventoryForItem(int warehouseId, int ItemId);
        //Reorder function stock by Pasindu 2020/04/29
        List<WarehouseInventory> GetWarehouseLowInventory(int companyId, int warehouseid);
        List<DailyStockSummary> GetMonthEndStock(int companyId, int warehouseid, DateTime date);
        WarehouseInventoryDetail GetWarehouseInventoryDetailToIssue(int ItemId, int WarehouseId, int CompanyId);
        int UpdateIssuedItems(int mrndId, decimal issuesdqty, int mrndststus, int companyId, int itemId, int userId, List<Inventory> inventoryObjs, List<MRNDIssueNote> notes, int mrnId);
        int ReceiveDeliveredTRItems(TRDIssueNote note, int trdID, decimal issuedQty, int UserId, List<Inventory> inventoryObjs, List<TRDIssueNote> notes, int itemId);
        List<IssuedInventoryBatches> GetIssuedInventoryBatches(int WarehouseId, int ItemId, decimal IssuedQty, int StockType);

        int UpdateIssuedTRItems(int TrdId, decimal issuesdqty, int trdststus, int companyId, int itemId, int userId, List<Inventory> inventoryObjs, List<TRDIssueNote> notes, int trId);
        List<Inventory> fetchWarehouseInventoryTR(int TRId, int itemID);
        List<WarehouseInventory> FetchItemListDetailed(int companyId, int warehouseid, int itemid, int maincategoryid, int subcategoryid);
        List<WarehouseInventory> GetInventoryByItemId(int itemID);
        int UpdateInventoryForGrnReturn(int warehouseId,  int Userid, List<GrnReturnDetails> GrnReturnDetailslist);
        int SaveEditedMasterInventory(int warehouseId, int ItemId, decimal newQty, decimal newStockValue, int updatedBy);
        int SaveEditedBatchInventory(int WarehouseId, int ItemId, decimal newQty, decimal newStockValue, int UserId, int BatchId, decimal newQtyBatch, decimal newStockValueBatch);
        int ReturnStock(int MrndInID, int ItemID, int WarehouseID, int StockMaintainingType,  int UserId);
        WarehouseInventory GetInventoryByItemIdAndWarehouseId(int itemID, int warehouseId, int CompayId);
        int ReturnMasterStock(int ItemID, int WarehouseID, decimal returnQty, decimal StockValue, int UserId, int Mrnstatus, int MrndInId, int mrndId,  decimal IssuesQty, decimal PrevreturnQty, int StockMaitaiinType);
        int ReturnBatchStock(int BatchId, decimal IssuedQty, decimal IssuedStockValue, int WarehouseID, int ItemID, int UserId);
        Inventory InventoryBatchesByBatchId(int BatchId, int warehouseId, int itemID);
        int StockReturn(int ItemID, int WarehouseID, int UserId, int CompanyId, int MrndInId, decimal returnQty, int Mrnstatus, int mrndId, decimal issuedQty, int BatchId, decimal PrevreturnQty,int StockMaitaiinType);
        int ReturnMasterStockApprove(int ItemID, int WarehouseID, decimal returnQty, decimal StockValue, int UserId,  int DepartmentReturnId);
        int StockReturnApprove(int ItemID, int WarehouseID, int UserId, decimal returnQty, int BatchId, int DepartmentReturnId);
    }

    class InventoryController : InventoryControllerInterface
    {
        public List<Inventory> fetchWarehouseInventoryTR(int TRId, int itemID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.fetchWarehouseInventoryTR(TRId, itemID, dbConnection);

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
        public List<IssuedInventoryBatches> GetIssuedInventoryBatches(int WarehouseId, int ItemId, decimal IssuedQty, int StockType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.GetIssuedInventoryBatches(WarehouseId, ItemId, IssuedQty, StockType, dbConnection);

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

        public List<WarehouseInventory> GetInventoryByItemId( int itemID) {
            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.GetInventoryByItemId(itemID, dbConnection);

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
        public Inventory InventoryBatchesByBatchId(int BatchId, int warehouseId, int itemID) {
            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.InventoryBatchesByBatchId(BatchId, warehouseId, itemID, dbConnection);

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

        public int addNewCompanyStock(int companyID, int itemID, int reOderedLevel, int addedBy)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.addNewCompanyStock(companyID, itemID, reOderedLevel, addedBy, dbConnection);

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

        public int ReturnMasterStock(int ItemID, int WarehouseID, decimal returnQty, decimal StockValue, int UserId, int Mrnstatus, int MrndInId, int mrndId, decimal IssuesQty, decimal PrevreturnQty, int StockMaitaiinType) {
            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                MRNDIssueNoteDAOInterface mRNDIssueNoteDAO = DAOFactory.CreateMRNDIssueNoteDAO();
                DepartmentReturnDAO departmentReturnDAO = DAOFactory.CreateDepartmentReturnDAO();

                int Result = departmentReturnDAO.InsertReturnDepartmentDetail(MrndInId, WarehouseID, ItemID, returnQty, StockValue, UserId, StockMaitaiinType, dbConnection);
                if (Result > 0) {
                    return 1;
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

        public int ReturnMasterStockApprove(int ItemID, int WarehouseID, decimal returnQty, decimal StockValue, int UserId, int DepartmentReturnId) {
            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                MRNDIssueNoteDAOInterface mRNDIssueNoteDAO = DAOFactory.CreateMRNDIssueNoteDAO();
                DepartmentReturnDAO departmentReturnDAO = DAOFactory.CreateDepartmentReturnDAO();


                int Result = DAO.ReturnMasterStock(ItemID, WarehouseID, returnQty, StockValue, UserId, dbConnection);

                if (Result > 0) {
                    Result = departmentReturnDAO.UpdateDepartmentReturnApproval(UserId, DepartmentReturnId, dbConnection);

                    if (Result > 0) {
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

        public int StockReturn(int ItemID, int WarehouseID, int UserId, int CompanyId, int MrndInId, decimal returnQty, int Mrnstatus, int mrndId, decimal issuedQty, int BatchId, decimal PrevreturnQty, int StockMaitaiinType) {
            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                MrndIssueNoteBatchesDAO mrndIssueNoteBatchesDAO = DAOFactory.CreateMrndIssueNoteBatchesDAO();
                InventoryDAOInterface InventoryDAO = DAOFactory.CreateInventoryDAO();
                MRNDIssueNoteDAOInterface mRNDIssueNoteDAO = DAOFactory.CreateMRNDIssueNoteDAO();
                DepartmentReturnDAO departmentReturnDAO = DAOFactory.CreateDepartmentReturnDAO();
                DepartmentReturnBatchDAO departmentReturnBatchDAO = DAOFactory.CreateDepartmentReturnBatchDAO();

                decimal BatchStockQty = 0;
                decimal BatchUnitPrice = 0;
                decimal ReturnBatchStock = 0;
                int Result = 0;
                //List<MrndIssueNoteBatches> mrndIssueNoteBatches = mrndIssueNoteBatchesDAO.getInventoryBatches(MrndInId, dbConnection);
                //for (int i = 0; i < mrndIssueNoteBatches.Count; i++) {
                    Inventory Batches = InventoryDAO.InventoryBatchesByBatchId(BatchId, WarehouseID, ItemID, dbConnection);

                    BatchStockQty = Batches.AvailableQty + Batches.HoldedQty;
                    if (BatchStockQty > 0) {
                        BatchUnitPrice = Batches.StockValue / BatchStockQty;
                        ReturnBatchStock = BatchUnitPrice * returnQty;

                        //Result = InventoryDAO.ReturnBatchStock(BatchId, returnQty, ReturnBatchStock, WarehouseID, ItemID, UserId, dbConnection);
                    }
               // }
                //if (Result > 0) {
                   // Result = InventoryDAO.ReturnMasterStock(ItemID, WarehouseID, returnQty, ReturnBatchStock,  UserId, dbConnection);

                    //if (Result > 0) {
                       // Result = mRNDIssueNoteDAO.updateIssueNoteDepartmentReturn(Mrnstatus, UserId, MrndInId, returnQty, mrndId, issuedQty, PrevreturnQty, StockMaitaiinType, dbConnection);
                        
                            Result = departmentReturnDAO.InsertReturnDepartmentDetail(MrndInId, WarehouseID, ItemID, returnQty, ReturnBatchStock, UserId, StockMaitaiinType, dbConnection);
                            if (Result > 0) {
                                Result = departmentReturnBatchDAO.InsertReturnDepartmentBatchDetail(Result, BatchId, returnQty, ReturnBatchStock, MrndInId, dbConnection);

                                if (Result > 0) {
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
                        
                    //}
                    //else {
                    //    dbConnection.RollBack();
                    //    return -2;
                    //}
                    
                //}
                //else {
                //    dbConnection.RollBack();
                //    return -1;
                //}


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

        public int StockReturnApprove(int ItemID, int WarehouseID, int UserId,  decimal returnQty, int BatchId, int DepartmentReturnId) {
            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                MrndIssueNoteBatchesDAO mrndIssueNoteBatchesDAO = DAOFactory.CreateMrndIssueNoteBatchesDAO();
                InventoryDAOInterface InventoryDAO = DAOFactory.CreateInventoryDAO();
                MRNDIssueNoteDAOInterface mRNDIssueNoteDAO = DAOFactory.CreateMRNDIssueNoteDAO();
                DepartmentReturnDAO departmentReturnDAO = DAOFactory.CreateDepartmentReturnDAO();
                DepartmentReturnBatchDAO departmentReturnBatchDAO = DAOFactory.CreateDepartmentReturnBatchDAO();

                decimal BatchStockQty = 0;
                decimal BatchUnitPrice = 0;
                decimal ReturnBatchStock = 0;
                int Result = 0;
                //List<MrndIssueNoteBatches> mrndIssueNoteBatches = mrndIssueNoteBatchesDAO.getInventoryBatches(MrndInId, dbConnection);
                //for (int i = 0; i < mrndIssueNoteBatches.Count; i++) {
                Inventory Batches = InventoryDAO.InventoryBatchesByBatchId(BatchId, WarehouseID, ItemID, dbConnection);

                BatchStockQty = Batches.AvailableQty + Batches.HoldedQty;
                if (BatchStockQty > 0) {
                    BatchUnitPrice = Batches.StockValue / BatchStockQty;
                    ReturnBatchStock = BatchUnitPrice * returnQty;

                    Result = InventoryDAO.ReturnBatchStock(BatchId, returnQty, ReturnBatchStock, WarehouseID, ItemID, UserId, dbConnection);
                }
                // }
                if (Result > 0) {
                    Result = InventoryDAO.ReturnMasterStock(ItemID, WarehouseID, returnQty, ReturnBatchStock, UserId,  dbConnection);

                    if (Result > 0) {
                        // Result = mRNDIssueNoteDAO.updateIssueNoteDepartmentReturn(Mrnstatus, UserId, MrndInId, returnQty, mrndId, issuedQty, PrevreturnQty, StockMaitaiinType, dbConnection);
                        Result = departmentReturnDAO.UpdateDepartmentReturnApproval(UserId, DepartmentReturnId, dbConnection);

                        if (Result > 0) {
                            return 1;
                            //Result = departmentReturnDAO.InsertReturnDepartmentDetail(MrndInId, WarehouseID, ItemID, returnQty, ReturnBatchStock, UserId, dbConnection);
                            //if (Result > 0) {
                            //    Result = departmentReturnBatchDAO.InsertReturnDepartmentBatchDetail(Result, BatchId, returnQty, ReturnBatchStock, MrndInId, dbConnection);

                            //    if (Result > 0) {
                            //        return 1;
                            //    }
                            //    else {
                            //        dbConnection.RollBack();
                            //        return -5;
                            //    }

                            //}
                            //else {
                            //    dbConnection.RollBack();
                            //    return -4;
                            //}
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


        public int SaveEditedBatchInventory(int WarehouseId, int ItemId, decimal newQty, decimal newStockValue, int UserId, int BatchId,  decimal newQtyBatch, decimal newStockValueBatch) {
            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                int result = DAO.SaveEditedMasterInventory(WarehouseId, ItemId, newQty, newStockValue, UserId, dbConnection);
                if (result > 0) {
                    result = DAO.SaveEditedBatchInventory(WarehouseId, ItemId, UserId, BatchId, newQtyBatch, newStockValueBatch,  dbConnection);
                    return 1;
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

        public int UpdateInventoryForGrnReturn(int warehouseId,  int Userid, List<GrnReturnDetails> GrnReturnDetailslist) {
            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                if (GrnReturnDetailslist.Count > 0) {
                    int result = 0;
                    for (int i = 0; i < GrnReturnDetailslist.Count; i++) {
                        result =  DAO.UpdateInventoryMasterForGrnReturn(warehouseId, GrnReturnDetailslist[i].ItemId, Userid, GrnReturnDetailslist[i].ReturnedQty, GrnReturnDetailslist[i].NetTotal, dbConnection);
                        if (result > 0) {
                            if (GrnReturnDetailslist[i].StockMaintainingType != 1) {
                                result = DAO.UpdateInventoryDetailForGrnReturn(warehouseId, GrnReturnDetailslist[i].ItemId, Userid, GrnReturnDetailslist[i].ReturnedQty, GrnReturnDetailslist[i].NetTotal, GrnReturnDetailslist[i].GrndId, dbConnection);
                            }
                        }
                    }
                    if (result > 0) {
                        return 1;
                    }
                    else {
                        dbConnection.RollBack();
                        return -1;
                    }
                    
                }
                else {
                    dbConnection.RollBack();
                    return -2;
                }
            }
            catch (Exception ) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        //Reorder function stock by Pasindu 2020/04/29
        public List<WarehouseInventory> GetWarehouseLowInventory(int companyId, int warehouseid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface inventoryDAO = DAOFactory.CreateInventoryDAO();
                return inventoryDAO.GetWarehouseLowInventory(companyId, warehouseid, dbConnection);
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
        //stock varificatio function by Pasindu 2020/05/04
        public List<DailyStockSummary> GetMonthEndStock(int companyId, int warehouseid, DateTime date)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface inventoryDAO = DAOFactory.CreateInventoryDAO();
                return inventoryDAO.GetMonthEndStock(companyId, warehouseid, date, dbConnection);
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
        

        public decimal GetWarehouseInventoryForItem(int warehouseId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.GetWarehouseInventoryForItem(warehouseId, ItemId, dbConnection);
               

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

        public int SaveEditedMasterInventory(int warehouseId, int ItemId, decimal newQty, decimal newStockValue, int updatedBy) {
            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.SaveEditedMasterInventory(warehouseId, ItemId, newQty, newStockValue, updatedBy, dbConnection);


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


        public List<Inventory> fetchWarehouseInventory(int companyID, int itemID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.fetchWarehouseInventory(companyID, itemID, dbConnection);

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

       

        public List<WarehouseInventory> fetchWarehouseItems(int warehouseID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.fetchWarehouseItems(warehouseID, dbConnection);

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

        public int raiseCompanyStockFromGRN(int companyID, int itemID, int receivedQty, decimal stockValue, int raisedBy)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.raiseCompanyStockFromGRN(companyID, itemID, receivedQty,stockValue,raisedBy, dbConnection);

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

        public int raiseWarehouseStockFromGRN(List<WarehouseInventoryRaise> inventoryList)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.raiseWarehouseStockFromGRN(inventoryList, dbConnection);

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

        public int releaseStockFromWarehouseToWarehouse(WarehouseInventoryRelease inventoryObj)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.releaseStockFromWarehouseToWarehouse(inventoryObj, dbConnection);

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

        public int updateCompanyStockAfterIssue(int companyID, int itemID, decimal issuedQty, int updatedBy, decimal issuedStockValue)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.updateCompanyStockAfterIssue(companyID, itemID, issuedQty, updatedBy, issuedStockValue, dbConnection);

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

        public int updateWarehouseStockAfterDelivered(int mrndInID, int warehouseID, int itemID, decimal DeliveredQty, int updatedBy, decimal deliveredStockValue, List<IssuedInventoryBatches> Batches)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.updateWarehouseStockAfterDelivered(mrndInID, warehouseID, itemID, DeliveredQty, updatedBy, deliveredStockValue, Batches, dbConnection);

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

        public int updateWarehouseStockAfterIssue(List<Inventory> inventoryObjs)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.updateWarehouseStockAfterIssue(inventoryObjs, dbConnection);

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

        public List<Inventory> fetchWarehouseInventorybyWarehouseId(int companyID, int warehouseId, int itemID)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.fetchWarehouseInventorybyWarehouseId(companyID, warehouseId, itemID, dbConnection);

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

        public int raiseWarehouseStockInMrn(WarehouseInventoryRaise inventory)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.raiseWarehouseStockInMrn(inventory, dbConnection);

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

        public int raiseCompanyStockInMrn(int companyID, int itemID, int receivedQty, decimal stockValue, int raisedBy)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.raiseCompanyStockInMrn(companyID, itemID, receivedQty, stockValue, raisedBy, dbConnection);

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

        public int updateWarehouseStockAfterIssuesformonewarehouse(Inventory inventoryObjList)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.updateWarehouseStockAfterIssuesformonewarehouse(inventoryObjList, dbConnection);

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

        public int raiseWarehouseStockInMrnManual(WarehouseInventoryRaise inventory, DateTime expdate )
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.raiseWarehouseStockInMrnManual(inventory, expdate, dbConnection);

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


        public int updateWarehouseStock(List<WarehouseInventory> inventoryList, int UserId, int ItemId, int CompanyId, string Remarks, List<WarehouseInventoryBatches> deletedBatches, int StockMAintainingType, List<WarehouseInventoryBatches> addedBatches, List<WarehouseInventoryBatches> batchList) {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.updateWarehouseStock(inventoryList, UserId, ItemId, CompanyId, Remarks, deletedBatches, StockMAintainingType, addedBatches, batchList, dbConnection);

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
        public WarehouseInventoryDetail GetWarehouseInventoryDetailToIssue(int ItemId, int WarehouseId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                return DAO.GetWarehouseInventoryDetailToIssue(ItemId, WarehouseId, CompanyId, dbConnection);

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
        //new code for UpdateIssuesItem
        public int UpdateIssuedItems(int mrndId, decimal issuesdqty, int mrndststus, int companyId, int itemId, int userId, List<Inventory> inventoryObjs, List<MRNDIssueNote> notes, int mrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAOInterface = DAOFactory.CreateMRNDAO();
                InventoryDAOInterface inventoryDAOInterface = DAOFactory.CreateInventoryDAO();
                MRNDIssueNoteDAOInterface mRNDIssueNoteDAOInterface = DAOFactory.CreateMRNDIssueNoteDAO();
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();

                int result = mrnDAOInterface.updateMRNDIssuedQty(mrndId, issuesdqty, dbConnection);
                if (result > 0)
                {
                    result = mrnDAOInterface.changeMRNDStaus(mrndId, mrndststus, dbConnection);

                    if (result > 0)
                    {
                        result = inventoryDAOInterface.updateCompanyStockAfterIssue(companyId, itemId, notes[0].IssuedQty, userId, inventoryObjs.Sum(s => s.IssuedStockValue), dbConnection); //REDUCE STOCK AND AVAILABLE QTY FROM COMPANY INVENTORY MASTER 

                        if (result > 0)
                        {
                            result = inventoryDAOInterface.updateWarehouseStockAfterIssue(inventoryObjs, dbConnection);

                            if (result > 0)
                            {
                                result = mRNDIssueNoteDAOInterface.addNewIssueNote(notes, dbConnection);

                                if (result > 0)
                                {
                                    result = inventoryDAOInterface.updateWarehouseStockAfterDelivered(result, inventoryObjs[0].WarehouseID, itemId, inventoryObjs[0].IssuedQty, userId, inventoryObjs[0].IssuedStockValue, inventoryObjs[0].IssuedBatches, dbConnection);

                                    if (result > 0)
                                    {
                                        result = mrnDAOInterface.updateMRNAfterIssue(mrnId, dbConnection);

                                        if (result > 0)
                                        {

                                            result = mrnDetailStatusLogDAO.InsertLogIssueStock(mrndId, userId, dbConnection);
                                            if (result > 0)
                                            {
                                                return 1;
                                            }
                                            else
                                            {
                                                dbConnection.RollBack();
                                                return -8;
                                            }
                                        }

                                        else
                                        {
                                            dbConnection.RollBack();
                                            return -7;
                                        }
                                    }
                                    else
                                    {
                                        dbConnection.RollBack();
                                        return -1;
                                    }
                                }
                                else
                                {
                                    dbConnection.RollBack();
                                    return -2;
                                }
                            }
                            else
                            {
                                dbConnection.RollBack();
                                return -3;
                            }
                        }
                        else
                        {
                            dbConnection.RollBack();
                            return -4;
                        }
                    }
                    else
                    {
                        dbConnection.RollBack();
                        return -5;
                    }
                }
                else
                {
                    dbConnection.RollBack();
                    return -6;
                }

            }
            catch (Exception EX)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int ReceiveDeliveredTRItems(TRDIssueNote note, int trdID, decimal issuedQty, int UserId, List<Inventory> inventoryObjs, List<TRDIssueNote> notes, int itemId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TRDIssueNoteDAO tRDIssueNoteDAO = DAOFactory.CreateTRDIssueNoteDAO();
                TRDetailsDAO tRDetailsDAO = DAOFactory.CreateTRDetailsDAO();
                TrDetailStatusLogDAO trDetailStatusLogDAO = DAOFactory.CreateTrDetailStatusLogDAO();
                InventoryDAOInterface inventoryDAOInterface = DAOFactory.CreateInventoryDAO();


                int result = tRDIssueNoteDAO.updateIssueNoteAfterReceived(note, dbConnection);

                if (result > 0) {
                    result = tRDetailsDAO.updateTRDReceivedQty(trdID, issuedQty, dbConnection);

                    if (result > 0) {
                        result = trDetailStatusLogDAO.UpdateTRLog(trdID, UserId, 8, dbConnection);

                        if (result > 0) {
                            //reciever stock raising
                            result = inventoryDAOInterface.updateWarehouseStockAfterTRIssue(inventoryObjs, dbConnection);

                            if (result > 0) {
                                result = inventoryDAOInterface.updateWarehouseStockAfterTRDelivered(result, notes[0].WarehouseId, itemId, issuedQty, UserId, notes[0].IssuedStockValue, dbConnection); //REMOVE HOLDED QTY AND REDUCE STOCK VALUE INVENTORY MASTER AND ADD TO INVENTORY RELESE

                                if (result > 0) {
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
                        return -1;
                    }
                }
                else {
                    dbConnection.RollBack();
                    return -2;

                }
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

        public int UpdateIssuedTRItems(int TrdId, decimal issuesdqty, int trdststus, int companyId, int itemId, int userId, List<Inventory> inventoryObjs, List<TRDIssueNote> notes, int trId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAOInterface = DAOFactory.CreateMRNDAO();
                InventoryDAOInterface inventoryDAOInterface = DAOFactory.CreateInventoryDAO();
                MRNDIssueNoteDAOInterface mRNDIssueNoteDAOInterface = DAOFactory.CreateMRNDIssueNoteDAO();
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();

                TRDetailsDAO tRDetailsDAO = DAOFactory.CreateTRDetailsDAO();
                TRDIssueNoteDAO tRDIssueNoteDAO = DAOFactory.CreateTRDIssueNoteDAO();
                TrDetailStatusLogDAO trDetailStatusLogDAO = DAOFactory.CreateTrDetailStatusLogDAO();

                int result = tRDetailsDAO.updateTRdIssuedQty(TrdId, issuesdqty, dbConnection);
                if (result > 0)
                {
                    result = tRDetailsDAO.changeTRDStaus(TrdId, trdststus, dbConnection);

                    if (result > 0)
                    {
                        result = inventoryDAOInterface.updateWarehouseStockAfterIssue(inventoryObjs, dbConnection); //REDUCE AVAILABLE QTY AND ADD HOLDED QTY TO WAREHOUSE INVENTORY MASTER

                        if (result > 0)
                        {
                            // result = inventoryDAOInterface.updateWarehouseStockAfterTRIssue(inventoryObjs, dbConnection); 

                            if (result > 0)
                            {
                                result = tRDIssueNoteDAO.addNewIssueNote(notes, dbConnection);

                                if (result > 0)
                                {
                                    //  result = inventoryDAOInterface.updateWarehouseStockAfterTRDelivered(result, notes[0].WarehouseId, itemId, issuesdqty, userId, notes[0].IssuedStockValue, dbConnection); //REMOVE HOLDED QTY AND REDUCE STOCK VALUE INVENTORY MASTER AND ADD TO INVENTORY RELESE

                                    if (result > 0)
                                    {
                                        result = tRDetailsDAO.updateTRAfterIssue(trId, dbConnection);

                                        if (result > 0)
                                        {

                                            result = trDetailStatusLogDAO.UpdateTRLog(TrdId, userId, 7, dbConnection);
                                            if (result > 0)
                                            {
                                                return 1;
                                            }
                                            else
                                            {
                                                dbConnection.RollBack();
                                                return -8;
                                            }
                                        }

                                        else
                                        {
                                            dbConnection.RollBack();
                                            return -7;
                                        }
                                    }
                                    else
                                    {
                                        dbConnection.RollBack();
                                        return -1;
                                    }
                                }
                                else
                                {
                                    dbConnection.RollBack();
                                    return -2;
                                }
                            }
                            else
                            {
                                dbConnection.RollBack();
                                return -3;
                            }
                        }
                        else
                        {
                            dbConnection.RollBack();
                            return -4;
                        }
                    }
                    else
                    {
                        dbConnection.RollBack();
                        return -5;
                    }
                }
                else
                {
                    dbConnection.RollBack();
                    return -6;
                }

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

        public List<WarehouseInventory> FetchItemListDetailed(int companyId, int warehouseid, int itemid, int maincategoryid, int subcategoryid) {
            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface inventoryDAO = DAOFactory.CreateInventoryDAO();
                return inventoryDAO.FetchItemListDetailed(companyId, warehouseid, itemid, maincategoryid, subcategoryid, dbConnection);
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

        public int ReturnBatchStock(int BatchId, decimal IssuedQty, decimal IssuedStockValue, int WarehouseID, int ItemID, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface inventoryDAO = DAOFactory.CreateInventoryDAO();
                return inventoryDAO.ReturnBatchStock(BatchId, IssuedQty, IssuedStockValue, WarehouseID, ItemID, UserId, dbConnection);
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

        public WarehouseInventory GetInventoryByItemIdAndWarehouseId(int itemID, int warehouseId, int CompayId) {
            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface inventoryDAO = DAOFactory.CreateInventoryDAO();
                return inventoryDAO.GetInventoryByItemIdAndWarehouseId(itemID, warehouseId, CompayId, dbConnection);
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
        
        public int ReturnStock(int MrndInID, int ItemID, int WarehouseID, int StockMaintainingType, int UserId) {

            DBConnection dbConnection = new DBConnection();
            try {
                InventoryDAOInterface DAO = DAOFactory.CreateInventoryDAO();
                MRNDIssueNoteDAOInterface mRNDIssueNoteDAO = DAOFactory.CreateMRNDIssueNoteDAO();
                MrndIssueNoteBatchesDAO mrndIssueNoteBatchesDAO = DAOFactory.CreateMrndIssueNoteBatchesDAO();

                MRNDIssueNote mRNDIssueNote = mRNDIssueNoteDAO.FetcMrndIssueNoteByMrndInId(MrndInID, dbConnection);
                int result =  DAO.ReturnMasterStock(ItemID,  WarehouseID, mRNDIssueNote.IssuedQty, mRNDIssueNote.StValue,UserId, dbConnection);

                if (result > 0) {
                    int status = mRNDIssueNoteDAO.updateIssueNoteAfterStockRetured(MrndInID, dbConnection);

                    if (StockMaintainingType != 1) {
                        List<MrndIssueNoteBatches> mrndIssueNoteBatches = mrndIssueNoteBatchesDAO.getInventoryBatches(MrndInID, dbConnection);
                        for (int i = 0; i < mrndIssueNoteBatches.Count; i++) {
                            result = DAO.ReturnBatchStock(mrndIssueNoteBatches[i].BatchId, mrndIssueNoteBatches[i].IssuedQty, mrndIssueNoteBatches[i].IssuedStockValue, WarehouseID, ItemID, UserId, dbConnection);

                        }
                        if (result > 0) {

                            return 1;
                        }
                        else {
                            dbConnection.RollBack();
                            return -2;
                        }
                    }
                    else {
                        return 1;
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
    }
}
