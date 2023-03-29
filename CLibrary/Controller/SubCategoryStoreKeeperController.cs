using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller {
    public interface SubCategoryStoreKeeperController {
        int SaveStoreKeeper(int SubCategoryId, List<int> UserIds, DateTime effectiveDate);
        List<SubCategoryStoreKeeper> FetchStoreKeeper(int SubCategoryId);
        SubCategoryStoreKeeper FetchStoreKeeperDetails(int SubCategoryId, int userId);
        int UpdateStoreKeeper(int SubCategoryId, int UserId, DateTime effectiveDate);
        int DeleteStoreKeeper(int SubCategoryId, int userId, DateTime date);

    }

    public class SubCategoryStoreKeeperControllerImpl : SubCategoryStoreKeeperController {
        public int SaveStoreKeeper(int SubCategoryId, List<int> UserIds, DateTime effectiveDate) {
            DBConnection dbConnection = new DBConnection();
            try {
                SubCategoryStoreKeeperDAO subCategoryStoreKeeperDAO = DAOFactory.createSubCategoryStoreKeeperDAO();
                return subCategoryStoreKeeperDAO.SaveStoreKeeper(SubCategoryId, UserIds, effectiveDate, dbConnection);
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
        public int DeleteStoreKeeper(int SubCategoryId, int userId, DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                SubCategoryStoreKeeperDAO subCategoryStoreKeeperDAO = DAOFactory.createSubCategoryStoreKeeperDAO();
                return subCategoryStoreKeeperDAO.DeleteStoreKeeper(SubCategoryId, userId, date,dbConnection);
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
        public int UpdateStoreKeeper(int SubCategoryId,int UserId, DateTime effectiveDate) {
            DBConnection dbConnection = new DBConnection();
            try {
                SubCategoryStoreKeeperDAO subCategoryStoreKeeperDAO = DAOFactory.createSubCategoryStoreKeeperDAO();
                return subCategoryStoreKeeperDAO.UpdateStoreKeeper(SubCategoryId, UserId, effectiveDate, dbConnection);
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


        public List<SubCategoryStoreKeeper> FetchStoreKeeper(int SubCategoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SubCategoryStoreKeeperDAO subCategoryStoreKeeperDAO = DAOFactory.createSubCategoryStoreKeeperDAO();
                return subCategoryStoreKeeperDAO.FetchStoreKeeper(SubCategoryId, dbConnection);
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

        public SubCategoryStoreKeeper FetchStoreKeeperDetails(int SubCategoryId, int userId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SubCategoryStoreKeeperDAO subCategoryStoreKeeperDAO = DAOFactory.createSubCategoryStoreKeeperDAO();
                return subCategoryStoreKeeperDAO.FetchStoreKeeperDetails(SubCategoryId, userId, dbConnection);
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
