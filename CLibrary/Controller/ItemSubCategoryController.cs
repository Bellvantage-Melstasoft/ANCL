using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface ItemSubCategoryController
    {
        int SaveItemSubCategory(int companyId, int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int UpdateItemSubCategory(int companyId, int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int DeleteItemSubCategory(int companyId, int SubCategoryId);
        List<ItemSubCategory> FetchItemSubCategoryList(int companyId);
        List<ItemSubCategory> FetchItemSubCategoryById(int SubCategoryId);
        ItemSubCategory FetchItemSubCategoryListByIdObj(int SubCategoryId);
        List<ItemSubCategory> FetchItemSubCategoryListWithMainCategory();
        List<ItemSubCategory> FetchItemSubCategoryByCategoryId(int CategoryId, int companyid);
        int UpdateItemSubCategoryStatus(int companyId, int categoryID, int subCategoryId, int status);
        List<ItemSubCategory> getStoreKeeperList(int CompanyId); 
        List<ItemSubCategory> SearchSubCategoryList(int companyId, string text);
    }

    public class ItemSubCategoryControllerImpl : ItemSubCategoryController {
        public int SaveItemSubCategory(int companyId, int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemSubCategoryDAO itemSubCategoryDAO = DAOFactory.CreateItemSubCategoryDAO();
                return itemSubCategoryDAO.SaveItemSubCategory(companyId, SubCategoryId, SubCategoryName, CategoryId, CreatedDateTime, CreatedBy, UpdatedDate, UpdatedBy, IsActive, dbConnection);
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

        public int UpdateItemSubCategory(int companyId, int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemSubCategoryDAO itemSubCategoryDAO = DAOFactory.CreateItemSubCategoryDAO();
                return itemSubCategoryDAO.UpdateItemSubCategory(companyId, SubCategoryId, SubCategoryName, CategoryId, CreatedDateTime, CreatedBy, UpdatedDate, UpdatedBy, IsActive, dbConnection);
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

        public int DeleteItemSubCategory(int companyId, int SubCategoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemSubCategoryDAO itemSubCategoryDAO = DAOFactory.CreateItemSubCategoryDAO();
                return itemSubCategoryDAO.DeleteItemSubCategory(companyId, SubCategoryId, dbConnection);
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

        public List<ItemSubCategory> FetchItemSubCategoryList(int companyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemSubCategoryDAO itemSubCategoryDAO = DAOFactory.CreateItemSubCategoryDAO();
                return itemSubCategoryDAO.FetchItemSubCategoryList(companyId, dbConnection);
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

        public List<ItemSubCategory> FetchItemSubCategoryById(int SubCategoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemSubCategoryDAO itemSubCategoryDAO = DAOFactory.CreateItemSubCategoryDAO();
                return itemSubCategoryDAO.FetchItemSubCategoryById(SubCategoryId, dbConnection);
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

        public ItemSubCategory FetchItemSubCategoryListByIdObj(int SubCategoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemSubCategoryDAO itemSubCategoryDAO = DAOFactory.CreateItemSubCategoryDAO();
                return itemSubCategoryDAO.FetchItemSubCategoryListByIdObj(SubCategoryId, dbConnection);
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


        public List<ItemSubCategory> FetchItemSubCategoryListWithMainCategory() {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemSubCategoryDAO itemSubCategoryDAO = DAOFactory.CreateItemSubCategoryDAO();
                return itemSubCategoryDAO.FetchItemSubCategoryListWithMainCategory(dbConnection);
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

        public List<ItemSubCategory> FetchItemSubCategoryByCategoryId(int CategoryId, int companyid) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemSubCategoryDAO itemSubCategoryDAO = DAOFactory.CreateItemSubCategoryDAO();
                return itemSubCategoryDAO.FetchItemSubCategoryByCategoryId(CategoryId, companyid, dbConnection);
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

        public int UpdateItemSubCategoryStatus(int companyId, int categoryID, int subCategoryId, int status) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemSubCategoryDAO itemSubCategoryDAO = DAOFactory.CreateItemSubCategoryDAO();
                return itemSubCategoryDAO.UpdateItemSubCategoryStatus(companyId, categoryID, subCategoryId, status, dbConnection);
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

        public List<ItemSubCategory> getStoreKeeperList(int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemSubCategoryDAO itemSubCategoryDAO = DAOFactory.CreateItemSubCategoryDAO();
                return itemSubCategoryDAO.getStoreKeeperList(CompanyId, dbConnection);
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
        
        public List<ItemSubCategory> SearchSubCategoryList(int companyId, string text) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemSubCategoryDAO itemSubCategoryDAO = DAOFactory.CreateItemSubCategoryDAO();
                return itemSubCategoryDAO.SearchSubCategoryList(companyId, text, dbConnection);
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


