using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface ItemSubCategoryMasterController
    {
        int SaveItemSubCategory(string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int UpdateItemSubCategory(int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int DeleteItemSubCategory(int SubCategoryId);
        List<ItemSubCategoryMaster> FetchItemSubCategoryList();
        List<ItemSubCategoryMaster> FetchItemSubCategoryById(int SubCategoryId);
        ItemSubCategoryMaster FetchItemSubCategoryListByIdObj(int SubCategoryId);
        List<ItemSubCategoryMaster> FetchItemSubCategoryListWithMainCategory();
        List<ItemSubCategoryMaster> FetchItemSubCategoryByCategoryId(int CategoryId);
        List<ItemSubCategoryMaster> searchSubCategoryNameList(string subcategoryName, int categoryId, int companyId);
        List<ItemSubCategoryMaster> FetchItemSubCategoryListForInitialFrontView();
    }

    public class ItemSubCategoryMasterControllerImpl : ItemSubCategoryMasterController
    {
        public int SaveItemSubCategory(string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemSubCategoryMasterDAO itemSubCategoryMasterDAO = DAOFactory.CreateItemSubCategoryMasterDAO();
                return itemSubCategoryMasterDAO.SaveItemSubCategory(SubCategoryName, CategoryId, CreatedDateTime, CreatedBy, UpdatedDate, UpdatedBy, IsActive, dbConnection);
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

        public int UpdateItemSubCategory(int SubCategoryId, string SubCategoryName, int CategoryId, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemSubCategoryMasterDAO itemSubCategoryMasterDAO = DAOFactory.CreateItemSubCategoryMasterDAO();
                return itemSubCategoryMasterDAO.UpdateItemSubCategory(SubCategoryId, SubCategoryName, CategoryId, CreatedDateTime, CreatedBy, UpdatedDate, UpdatedBy, IsActive, dbConnection);
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

        public int DeleteItemSubCategory(int SubCategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemSubCategoryMasterDAO itemSubCategoryMasterDAO = DAOFactory.CreateItemSubCategoryMasterDAO();
                return itemSubCategoryMasterDAO.DeleteItemSubCategory(SubCategoryId, dbConnection);
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

        public List<ItemSubCategoryMaster> FetchItemSubCategoryList()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemSubCategoryMasterDAO itemSubCategoryMasterDAO = DAOFactory.CreateItemSubCategoryMasterDAO();
                return itemSubCategoryMasterDAO.FetchItemSubCategoryList(dbConnection);
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

        public List<ItemSubCategoryMaster> FetchItemSubCategoryById(int SubCategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemSubCategoryMasterDAO itemSubCategoryMasterDAO = DAOFactory.CreateItemSubCategoryMasterDAO();
                return itemSubCategoryMasterDAO.FetchItemSubCategoryById(SubCategoryId, dbConnection);
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

        public ItemSubCategoryMaster FetchItemSubCategoryListByIdObj(int SubCategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemSubCategoryMasterDAO itemSubCategoryMasterDAO = DAOFactory.CreateItemSubCategoryMasterDAO();
                return itemSubCategoryMasterDAO.FetchItemSubCategoryListByIdObj(SubCategoryId, dbConnection);
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


        public List<ItemSubCategoryMaster> FetchItemSubCategoryListWithMainCategory()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemSubCategoryMasterDAO itemSubCategoryMasterDAO = DAOFactory.CreateItemSubCategoryMasterDAO();
                return itemSubCategoryMasterDAO.FetchItemSubCategoryListWithMainCategory(dbConnection);
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

        public List<ItemSubCategoryMaster> FetchItemSubCategoryByCategoryId(int CategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemSubCategoryMasterDAO itemSubCategoryMasterDAO = DAOFactory.CreateItemSubCategoryMasterDAO();
                return itemSubCategoryMasterDAO.FetchItemSubCategoryByCategoryId(CategoryId, dbConnection);
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

        public List<ItemSubCategoryMaster> searchSubCategoryNameList(string subcategoryName, int categoryId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemSubCategoryMasterDAO itemSubCategoryMasterDAO = DAOFactory.CreateItemSubCategoryMasterDAO();
                return itemSubCategoryMasterDAO.searchSubCategoryNameList( subcategoryName,  categoryId, companyId, dbConnection);
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
        public List<ItemSubCategoryMaster> FetchItemSubCategoryListForInitialFrontView()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemSubCategoryMasterDAO itemSubCategoryMasterDAO = DAOFactory.CreateItemSubCategoryMasterDAO();
                return itemSubCategoryMasterDAO.FetchItemSubCategoryListForInitialFrontView(dbConnection);
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
    }
}

