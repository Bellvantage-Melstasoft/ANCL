using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller {
    public interface UserWarehouseController {
        List<UserWarehouse> getUserWarehousedetails(int UserId);
        List<UserWarehouse> GetWarehouseHeadsByWarehouseId(int WarehouseId);
        List<UserWarehouse> GetWarehouseKeeperForMRN(int WarehouseId, int subCategoryId);
        List<UserWarehouse> GetWarehousesByUserId(int UserId);
    }

    public class UserWarehouseControllerImpl : UserWarehouseController {
        public List<UserWarehouse> getUserWarehousedetails(int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                UserWarehouseDAO userWarehouseDAO = DAOFactory.createUserWarehouseDAO();
                return userWarehouseDAO.getUserWarehousedetails(UserId, dbConnection);
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

        public List<UserWarehouse> GetWarehouseHeadsByWarehouseId(int WarehouseId) {
            DBConnection dbConnection = new DBConnection();
            try {
                UserWarehouseDAO userWarehouseDAO = DAOFactory.createUserWarehouseDAO();
                return userWarehouseDAO.GetWarehouseHeadsByWarehouseId(WarehouseId, dbConnection);
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

        public List<UserWarehouse> GetWarehouseKeeperForMRN(int WarehouseId, int subCategoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                UserWarehouseDAO userWarehouseDAO = DAOFactory.createUserWarehouseDAO();
                return userWarehouseDAO.GetWarehouseKeeperForMRN(WarehouseId, subCategoryId, dbConnection);
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

        public List<UserWarehouse> GetWarehousesByUserId(int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                UserWarehouseDAO userWarehouseDAO = DAOFactory.createUserWarehouseDAO();
                return userWarehouseDAO.GetWarehousesByUserId(UserId, dbConnection);
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
