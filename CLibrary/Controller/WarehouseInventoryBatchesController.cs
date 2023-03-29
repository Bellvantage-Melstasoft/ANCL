using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface WarehouseInventoryBatchesController
    {
        int saveWarehouseInventoryBatches(int warehouseId, int itemId, int companyId, DateTime expiryDate, int avaiblableStock, int avaiblableStockValue);
        int deleteWarehouseInventoryBatches(int warehouseId, int itemId, int batchId, int companyId,int BatchId);
        List<WarehouseInventoryBatches> getAllWarehouseInventoryBatches(int itemId, int companyID);
        List<WarehouseInventoryBatches> getWarehouseInventoryBatches(int itemId, int warehouseID);
        List<WarehouseInventoryBatches> getWarehouseInventoryBatchesListByWarehouseId(int warehouseId, int itemId, int companyId);
        List<WarehouseInventoryBatches> getWarehouseInventoryBatchesForInventory(int itemId, int companyId, int WArehouseId);
        int DeleteStockMaintainingTypeChanges(int itemId, int companyId);
        int AddBAtchesMaintainingTypeChanges(List<WarehouseInventory> InventoryList, int UserId, int companyId);
        List<WarehouseInventoryBatches> getWarehouseInventoryBatchesForInventoryEdit(int itemId, int companyId, int WarehouseId, int BatchId);
    }

    public class WarehouseInventoryBatchesControllerImp : WarehouseInventoryBatchesController
    {
        public int saveWarehouseInventoryBatches(int warehouseId, int itemId, int companyId, DateTime expiryDate, int avaiblableStock, int avaiblableStockValue)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseInventoryBatchesDAO warehouseInventoryBatchesDAO = DAOFactory.CreateWarehouseInventoryBatchesDAO();
                return 0;
                // return warehouseInventoryBatchesDAO.saveWarehouseInventoryBatches(warehouseId, itemId, companyID, expiryDate, avaiblableStock, avaiblableStockValue, dbConnection);
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

        public int deleteWarehouseInventoryBatches(int warehouseId, int itemId, int batchId, int companyId, int BatchId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseInventoryBatchesDAO warehouseInventoryBatchesDAO = DAOFactory.CreateWarehouseInventoryBatchesDAO();
                return warehouseInventoryBatchesDAO.deleteWarehouseInventoryBatches(warehouseId, itemId, batchId, companyId, BatchId, dbConnection);
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

        public List<WarehouseInventoryBatches> getAllWarehouseInventoryBatches(int itemId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseInventoryBatchesDAO warehouseInventoryBatchesDAO = DAOFactory.CreateWarehouseInventoryBatchesDAO();
                return warehouseInventoryBatchesDAO.getAllWarehouseInventoryBatchesList(itemId, companyId, dbConnection);
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

        public List<WarehouseInventoryBatches> getWarehouseInventoryBatches(int itemId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseInventoryBatchesDAO warehouseInventoryBatchesDAO = DAOFactory.CreateWarehouseInventoryBatchesDAO();
                return warehouseInventoryBatchesDAO.getWarehouseInventoryBatches(itemId, companyId, dbConnection);
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


        public List<WarehouseInventoryBatches> getWarehouseInventoryBatchesListByWarehouseId(int warehouseId, int itemId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseInventoryBatchesDAO warehouseInventoryBatchesDAO = DAOFactory.CreateWarehouseInventoryBatchesDAO();
                return warehouseInventoryBatchesDAO.getWarehouseInventoryBatchesListByWarehouseId(warehouseId, itemId, companyId, dbConnection);
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

        public List<WarehouseInventoryBatches> getWarehouseInventoryBatchesForInventory(int itemId, int companyId, int WArehouseId) {
            DBConnection dbConnection = new DBConnection();
            try {
                WarehouseInventoryBatchesDAO warehouseInventoryBatchesDAO = DAOFactory.CreateWarehouseInventoryBatchesDAO();
                return warehouseInventoryBatchesDAO.getWarehouseInventoryBatchesForInventory(itemId, companyId, WArehouseId, dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<WarehouseInventoryBatches> getWarehouseInventoryBatchesForInventoryEdit(int itemId, int companyId, int WarehouseId, int BatchId) {
            DBConnection dbConnection = new DBConnection();
            try {
                WarehouseInventoryBatchesDAO warehouseInventoryBatchesDAO = DAOFactory.CreateWarehouseInventoryBatchesDAO();
                return warehouseInventoryBatchesDAO.getWarehouseInventoryBatchesForInventoryEdit(itemId, companyId, WarehouseId, BatchId, dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int DeleteStockMaintainingTypeChanges( int itemId, int companyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                WarehouseInventoryBatchesDAO warehouseInventoryBatchesDAO = DAOFactory.CreateWarehouseInventoryBatchesDAO();
                return warehouseInventoryBatchesDAO.DeleteStockMaintainingTypeChanges( itemId,  companyId,  dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int AddBAtchesMaintainingTypeChanges(List<WarehouseInventory> InventoryList,int UserId, int companyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                WarehouseInventoryBatchesDAO warehouseInventoryBatchesDAO = DAOFactory.CreateWarehouseInventoryBatchesDAO();
                return warehouseInventoryBatchesDAO.AddBAtchesMaintainingTypeChanges(InventoryList, UserId, companyId, dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }
    }
}
