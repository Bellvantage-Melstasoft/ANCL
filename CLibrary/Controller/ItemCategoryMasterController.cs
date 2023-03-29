using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface ItemCategoryMasterController
    {
        int SaveItemCategory(string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int UpdateItemCategory(int CategoryId, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int DeleteItemCategory(int companyId , int CategoryId);
        List<ItemCategoryMaster> FetchItemCategoryList();
        List<ItemCategoryMaster> FetchItemCategoryById(int CategoryId);
        ItemCategoryMaster FetchItemCategoryListByIdObj(int CategoryId);
        List<ItemCategoryMaster> searchCategoryName(string categoryName, int companyId);
        List<ItemCategoryMaster> FetchItemCategoryfORSubCategoryCreationList(int companyId);

        int ManageItemCategory(int catergoryId, string categoryName, DateTime createdDate, string userId, DateTime updatedDate, string UpdatedBy, int IsActive, int companyId,  string action);
    }

    public class ItemCategoryMasterControllerImpl : ItemCategoryMasterController
    {
        public int SaveItemCategory(string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryMasterDAO itemCategoryMasterDAO = DAOFactory.CreateItemCategoryMasterDAO();
                return itemCategoryMasterDAO.SaveItemMasterCategory(CategoryName, CategorDate, CreatedBy, UpdatedDate, UpdatedBy, IsActive, dbConnection);
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

        public int UpdateItemCategory(int CategoryId, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryMasterDAO itemCategoryMasterDAO = DAOFactory.CreateItemCategoryMasterDAO();
                return itemCategoryMasterDAO.UpdateItemCategory(CategoryId, CategoryName, CategorDate, CreatedBy, UpdatedDate, UpdatedBy, IsActive, dbConnection);
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

        public int DeleteItemCategory(int companyId ,int categoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                int status = 0;
                ItemCategoryMasterDAO itemCategoryMasterDAO = DAOFactory.CreateItemCategoryMasterDAO();
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                ItemCategoryApprovalDAO itemCategoryApprovalDAO = DAOFactory.CreateItemCategoryApprovalDAO();
                itemCategoryApprovalDAO.DeleteItemCategoryApprovalTypes(companyId, categoryId, dbConnection);
                itemCategoryApprovalDAO.DeleteItemCategoryApprovalLimit(companyId, categoryId, dbConnection);
                int deleteItemCategory = itemCategoryDAO.DeleteItemCategory(companyId, categoryId, dbConnection);
                if (deleteItemCategory > 0)
                {
                    status = itemCategoryMasterDAO.DeleteItemCategory(categoryId, dbConnection);
                }
                    return status;
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

        public List<ItemCategoryMaster> FetchItemCategoryList()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryMasterDAO itemCategoryMasterDAO = DAOFactory.CreateItemCategoryMasterDAO();
                return itemCategoryMasterDAO.FetchItemCategoryList(dbConnection);
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

        public List<ItemCategoryMaster> FetchItemCategoryById(int CategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryMasterDAO itemCategoryMasterDAO = DAOFactory.CreateItemCategoryMasterDAO();
                return itemCategoryMasterDAO.FetchItemCategoryById(CategoryId, dbConnection);
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

        public ItemCategoryMaster FetchItemCategoryListByIdObj(int CategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryMasterDAO itemCategoryMasterDAO = DAOFactory.CreateItemCategoryMasterDAO();
                return itemCategoryMasterDAO.FetchItemCategoryListByIdObj(CategoryId, dbConnection);
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

        public List<ItemCategoryMaster> searchCategoryName(string categoryName, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryMasterDAO itemCategoryMasterDAO = DAOFactory.CreateItemCategoryMasterDAO();
                return itemCategoryMasterDAO.searchCategoryName(categoryName, companyId, dbConnection);
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

        public List<ItemCategoryMaster> FetchItemCategoryfORSubCategoryCreationList(int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryMasterDAO itemCategoryMasterDAO = DAOFactory.CreateItemCategoryMasterDAO();
                return itemCategoryMasterDAO.FetchItemCategoryfORSubCategoryCreationList(companyId, dbConnection);
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

        public int ManageItemCategory(int categoryId, string categoryName, DateTime createdDate, string userId, DateTime updatedDate, string UpdatedBy, int IsActive, int companyId,  string action)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryMasterDAO itemCategoryMasterDAO = DAOFactory.CreateItemCategoryMasterDAO();
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                ItemCategoryApprovalDAO itemCategoryApprovalDAO = DAOFactory.CreateItemCategoryApprovalDAO();
                if (action == "Save")
                {
                    // when inserting catergoryId is not used
                    categoryId = itemCategoryMasterDAO.SaveItemMasterCategory(categoryName, createdDate, userId, updatedDate, UpdatedBy, IsActive, dbConnection);
                    if (categoryId != -1)
                    {
                        if (categoryId > 0)
                        {
                            //ITEM_CATEGORY
                            itemCategoryDAO.SaveItemCategory(companyId, categoryId, categoryName, createdDate, userId, updatedDate, userId, IsActive, dbConnection);                            
                        }
                    }
                }
                else if (action == "Update")
                {                    
                    int updateItemCategory = itemCategoryDAO.UpdateItemCategory(companyId, categoryId, categoryName, createdDate, userId, updatedDate, UpdatedBy, IsActive, dbConnection);
                    if (updateItemCategory > 0)
                    {
                        categoryId = itemCategoryMasterDAO.UpdateItemCategory(categoryId, categoryName, createdDate, userId, updatedDate, UpdatedBy, IsActive, dbConnection);
                    }
                }
                return categoryId;
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
    }
}
