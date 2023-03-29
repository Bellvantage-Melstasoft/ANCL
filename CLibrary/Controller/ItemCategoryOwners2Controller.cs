using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller {
    public interface ItemCategoryOwners2Controller {
        List<ItemCategoryOwners2> GetCompanyOwnersandPurchaseOfficersbyCompanyId(int companyId);
        ItemCategoryOwners2 FetchItemCategoryOwnerDetails(int categoryId, string type, int UserId, int CompanyId);
        int ManageItemCategoryOwners2(int id, int categoryId, string ownerType, List<int> UserIds, DateTime effectiveDate, int userId2, DateTime now, string action);
        int UpdateCategoryOwner(int CategoryId, int UserId, DateTime effectiveDate, string ownerType, List<int> UserIds, int userId2, DateTime PrevEffectveDate);
        List<ItemCategoryOwners2> FetchCompanyOwnerHistory(int CategoryId, string type, DateTime date);
        List<ItemCategoryOwners2> FetchDates(int categoryId, string type);
        int DeleteCategoryOwners(int categoryId, string ownerType, int UserId, DateTime EffectiveDate);
        List<ItemCategoryOwners2> FetchCategoryOwners(int CategoryId);
        List<ItemCategoryOwners2> FetchPurchaseOfficers(int CategoryId);
        List<ItemCategoryOwners2> FetchCategoryOwnerNames(int categoryId);
        List<ItemCategoryOwners2> FetchPurchaseOfficerNames(int categoryId);
    }

    public class ItemCategoryOwners2ControllerImpl : ItemCategoryOwners2Controller {
        public int ManageItemCategoryOwners2(int id, int categoryId, string ownerType, List<int> UserIds, DateTime effectiveDate, int userId2, DateTime now, string action) {
            DBConnection dbConnection = new DBConnection();
            try {
                if (action == "Save") {// Insert
                    ItemCategoryOwners2DAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwners2DAO();
                    // id will be not used when inserting
                    return itemCategoryDAO.SaveItemCategoryOwners2(id, categoryId, ownerType, UserIds, effectiveDate, userId2, now, dbConnection);
                }
                else {
                    // update
                    // ItemCategoryOwnerDAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwnerDAO();
                    //  return itemCategoryDAO.UpdateItemCategoryOwners(id , categoryId, ownerType, UserIds, effectiveDate, userId2, now, dbConnection);
                    return 0;
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

        public List<ItemCategoryOwners2> GetCompanyOwnersandPurchaseOfficersbyCompanyId(int companyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemCategoryOwners2DAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwners2DAO();
                return itemCategoryDAO.GetCompanyOwnersandPurchaseOfficersbyCompanyId(companyId, dbConnection);
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

        public List<ItemCategoryOwners2> FetchDates(int categoryId, string type) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemCategoryOwners2DAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwners2DAO();
                return itemCategoryDAO.FetchDates(categoryId, type, dbConnection);
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

        public List<ItemCategoryOwners2> FetchCompanyOwnerHistory(int CategoryId, string type, DateTime date) {
            DBConnection dbConnection = null;
            try {
                dbConnection  = new DBConnection();
                ItemCategoryOwners2DAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwners2DAO();
                return itemCategoryDAO.FetchCompanyOwnerHistory(CategoryId,type,date, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                dbConnection.con.Close();
                //if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                //    dbConnection.Commit();
                //}
            }
        }
        public int UpdateCategoryOwner(int CategoryId, int UserId, DateTime effectiveDate, string ownerType, List<int> UserIds, int userId2, DateTime PrevEffectveDate) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemCategoryOwners2DAO ItemCategoryOwners2DAO = DAOFactory.CreateItemCategoryOwners2DAO();
                return ItemCategoryOwners2DAO.UpdateCategoryOwner(CategoryId, UserId, effectiveDate, ownerType, UserIds, userId2, PrevEffectveDate, dbConnection);
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

        public int DeleteCategoryOwners(int categoryId, string ownerType, int UserId, DateTime EffectiveDate) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemCategoryOwners2DAO ItemCategoryOwners2DAO = DAOFactory.CreateItemCategoryOwners2DAO();
                return ItemCategoryOwners2DAO.DeleteCategoryOwners(categoryId, ownerType, UserId, EffectiveDate, dbConnection);
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


        public ItemCategoryOwners2 FetchItemCategoryOwnerDetails(int categoryId, string type, int UserId, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemCategoryOwners2DAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwners2DAO();
                return itemCategoryDAO.FetchItemCategoryOwnerDetails(categoryId, type, UserId, CompanyId, dbConnection);
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

        public List<ItemCategoryOwners2> FetchCategoryOwners(int categoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemCategoryOwners2DAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwners2DAO();
                return itemCategoryDAO.FetchCategoryOwners(categoryId, dbConnection);
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

        public List<ItemCategoryOwners2> FetchPurchaseOfficers(int CategoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemCategoryOwners2DAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwners2DAO();
                return itemCategoryDAO.FetchPurchaseOfficers(CategoryId, dbConnection);
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

        public List<ItemCategoryOwners2> FetchCategoryOwnerNames(int categoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemCategoryOwners2DAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwners2DAO();
                return itemCategoryDAO.FetchCategoryOwnerNames(categoryId, dbConnection);
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

        public List<ItemCategoryOwners2> FetchPurchaseOfficerNames(int categoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemCategoryOwners2DAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwners2DAO();
                return itemCategoryDAO.FetchPurchaseOfficerNames(categoryId, dbConnection);
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
