using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface ItemCategoryOwnerController
    {
        List<ItemCategoryOwners> FetchItemCategoryOwnersByCategoryId(int categoryId);
        ItemCategoryOwners FetchItemCategoryOwnersByCategoryId(int categoryId, string ownerType );
        int ManageItemCategoryOwners2(int id, int categoryId, string ownerType, List<int> UserIds, DateTime effectiveDate, int userId2, DateTime now, string action);
        int ManageItemCategoryOwners(int id ,int categoryId, string ownerType, int UserId, DateTime effectiveDate, int userId2, DateTime now ,string action);
        int DeleteItemCategoryOwners(int id);
        List<ItemCategoryOwners> FetchAllItemCategoryOwners(string ownerType);
        CompanyLogin GetCurrentPurchasingOfficer(int CategoryId);
    }

    public class ItemCategoryOwnerControllerImpl : ItemCategoryOwnerController
    {
        public int DeleteItemCategoryOwners(int id)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryOwnerDAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwnerDAO();
                return itemCategoryDAO.DeleteItemCategoryOwners(id , dbConnection);
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

      
        public List<ItemCategoryOwners> FetchItemCategoryOwnersByCategoryId(int categoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryOwnerDAO itemCategoryOwnerDAO = DAOFactory.CreateItemCategoryOwnerDAO();
                return itemCategoryOwnerDAO.FetchItemCategoryOwnersByCategoryId(categoryId, dbConnection);
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

        public ItemCategoryOwners FetchItemCategoryOwnersByCategoryId(int categoryId, string ownerType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryOwnerDAO itemCategoryOwnerDAO = DAOFactory.CreateItemCategoryOwnerDAO();
                return itemCategoryOwnerDAO.FetchItemCategoryOwnersByCategoryId(categoryId, ownerType, dbConnection);
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
        public int ManageItemCategoryOwners(int id , int categoryId, string ownerType, int UserId, DateTime effectiveDate, int userId2, DateTime now, string action)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                if (action == "Save")
                {// Insert
                    ItemCategoryOwnerDAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwnerDAO();
                    // id will be not used when inserting
                    return itemCategoryDAO.SaveItemCategoryOwners(id ,categoryId, ownerType, UserId, effectiveDate, userId2, now, dbConnection);
                }else
                {
                    // update
                    // ItemCategoryOwnerDAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwnerDAO();
                    //  return itemCategoryDAO.UpdateItemCategoryOwners(id , categoryId, ownerType, UserIds, effectiveDate, userId2, now, dbConnection);
                    return 0;
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
        public int ManageItemCategoryOwners2(int id, int categoryId, string ownerType, List<int> UserIds, DateTime effectiveDate, int userId2, DateTime now, string action) {
            DBConnection dbConnection = new DBConnection();
            try {
                if (action == "Save") {// Insert
                    ItemCategoryOwnerDAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwnerDAO();
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


        public List<ItemCategoryOwners> FetchAllItemCategoryOwners(string ownerType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryOwnerDAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwnerDAO();
                return itemCategoryDAO.FetchAllItemCategoryOwners(ownerType, dbConnection);
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

        public CompanyLogin GetCurrentPurchasingOfficer(int CategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryOwnerDAO itemCategoryDAO = DAOFactory.CreateItemCategoryOwnerDAO();
                return itemCategoryDAO.GetCurrentPurchasingOfficer(CategoryId, dbConnection);
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
