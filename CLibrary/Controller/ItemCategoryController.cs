using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;
using System.Data;

namespace CLibrary.Controller
{
    public interface ItemCategoryController
    {
        int SaveItemCategory(int companyId, int categoryid, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int UpdateItemCategory(int companyId, int CategoryId, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int DeleteItemCategory(int companyId, int CategoryId);
        List<ItemCategory> FetchItemCategoryList(int companyId);
        List<ItemCategory> FetchItemCategoryById(int CategoryId);
        ItemCategory FetchItemCategoryListByIdObj(int companyId, int CategoryId);
        int UpdateItemCategoryStatus(int companyId, int CategoryId, int status);
        DataTable FetchItemCategoryApprovalLimits(int categoryId);
        DataTable FetchItemCategoryApprovalLimitsAuthority(int v, int approvalLimitId);


        ItemCategory FetchItemByItemId(int ItemId , int companyId);
        int GetPurchasingOfficer(int CategoryId);
        int GetApprovalLimits(int CategoryId, int LimitFor, decimal MaxValue, decimal MinValue);
        DataTable FetchItemCategoryApprovalLimitsImport(int categoryId);
    }

    public class ItemCategoryControllerImpl : ItemCategoryController
    {
        public int SaveItemCategory(int companyId, int categoryid, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.SaveItemCategory(companyId, categoryid, CategoryName, CategorDate, CreatedBy, UpdatedDate, UpdatedBy, IsActive, dbConnection);
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

        public int UpdateItemCategory(int companyId, int CategoryId, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.UpdateItemCategory(companyId, CategoryId, CategoryName, CategorDate, CreatedBy, UpdatedDate, UpdatedBy, IsActive, dbConnection);
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

        public int DeleteItemCategory(int companyId, int CategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.DeleteItemCategory(companyId, CategoryId, dbConnection);
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

        public List<ItemCategory> FetchItemCategoryList(int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.FetchItemCategoryList(companyId, dbConnection);
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

        public List<ItemCategory> FetchItemCategoryById(int CategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.FetchItemCategoryById(CategoryId, dbConnection);
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

        public ItemCategory FetchItemCategoryListByIdObj(int companyId, int CategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.FetchItemCategoryListByIdObj(companyId, CategoryId, dbConnection);
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

        public int UpdateItemCategoryStatus(int companyId, int CategoryId, int status)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.UpdateItemCategoryStatus( companyId,  CategoryId,  status, dbConnection);
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

        public DataTable FetchItemCategoryApprovalLimits(int categoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.FetchItemCategoryApprovalLimits(categoryId, dbConnection);
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

        public DataTable FetchItemCategoryApprovalLimitsImport(int categoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.FetchItemCategoryApprovalLimitsImport(categoryId, dbConnection);
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


        public DataTable FetchItemCategoryApprovalLimitsAuthority(int companyId, int approvalLimitId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.FetchItemCategoryApprovalLimitsAuthority(companyId, approvalLimitId, dbConnection);
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

        public ItemCategory FetchItemByItemId(int ItemId , int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.FetchItemByItemId(ItemId, companyId, dbConnection);
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

        public int GetPurchasingOfficer(int CategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.GetPurchasingOfficer(CategoryId, dbConnection);
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

        public int GetApprovalLimits(int CategoryId, int LimitFor, decimal MaxValue, decimal MinValue) {
            DBConnection dbConnection = new DBConnection();
            try {
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                return itemCategoryDAO.GetApprovalLimits(CategoryId, LimitFor, MaxValue, MinValue, dbConnection);
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
