using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface WarehouseControllerInterface
    {
        int saveWarehouse(string location, string phoneNo, int companyID, string address, int isActive, List<int> UserIds);
        int updateWarehouse(int warehouseID, string location, string phoneNo, int companyID, string address, int isActive, List<int> UserIds);
        int deleteWarehouse(int warehouseID);
        List<Warehouse> getWarehouseList(int companyID);
        Warehouse getWarehouseByID(int warehouseID);
        int isUserHeadOfWarehouse(int userID);

        List<Warehouse> getWarehouseList1();
        List<WarehouseInventory> FetchItemAvailableStock(int warehouseId, IEnumerable<int> itemIds);
        List<WarehouseInventory> getWarehouseListAtAddItems(int companyID);
        List<WarehouseInventory> getWarehouseDetailsByWarehouseId(List<int> warehouseID);
        List<WarehouseInventory> getWarehouseListAtAddItemsForUpdate(int companyID, int ItemId);
    }
    public class WarehouseController : WarehouseControllerInterface
    {
        public int saveWarehouse(string location, string phoneNo, int companyID, string address, int isActive, List<int> UserIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                return warehouseDAO.saveWarehouse(location, phoneNo,companyID, address, isActive,UserIds, dbConnection);
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

        public int updateWarehouse(int warehouseID, string location, string phoneNo, int companyID, string address, int isActive, List<int> UserIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                return warehouseDAO.updateWarehouse(warehouseID,location, phoneNo, companyID, address, isActive,UserIds, dbConnection);
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

        public int deleteWarehouse(int warehouseID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                return warehouseDAO.deleteWarehouse(warehouseID, dbConnection);
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

        public List<Warehouse> getWarehouseList(int companyID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                return warehouseDAO.getWarehouseList(companyID,dbConnection);
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

        public Warehouse getWarehouseByID(int warehouseID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                return warehouseDAO.getWarehouseByID(warehouseID, dbConnection);
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

        public int isUserHeadOfWarehouse(int userID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                return warehouseDAO.isUserHeadOfWarehouse(userID, dbConnection);
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

        public List<Warehouse> getWarehouseList1()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                return warehouseDAO.getWarehouseList1(dbConnection);
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
        

        public List<WarehouseInventory> FetchItemAvailableStock(int warehouseId, IEnumerable<int> itemIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                return warehouseDAO.FetchItemAvailableStock(warehouseId, itemIds, dbConnection);
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


        public List<WarehouseInventory> getWarehouseListAtAddItems(int companyID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                return warehouseDAO.getWarehouseListAtAddItems(companyID, dbConnection);
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

        public List<WarehouseInventory> getWarehouseListAtAddItemsForUpdate(int companyID, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                return warehouseDAO.getWarehouseListAtAddItemsForUpdate(companyID, ItemId, dbConnection);
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

        public List<WarehouseInventory> getWarehouseDetailsByWarehouseId(List<int> warehouseID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                return warehouseDAO.getWarehouseDetailsByWarehouseId(warehouseID, dbConnection);
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
    }


}
