using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using CLibrary.Infrastructue;

namespace CLibrary.Controller
{
     public interface SupplierCategoryController
    {
        int saveSupplierCategory(int supplierId, int categoryId, int isActive);
        int updateSupplierCategory(int supplierId, int categoryId, int isActive);
        List<SupplierCategory> GetSupplierCategoryBySupplierId(int supplierId);
        int deleteSupplierCategoryBySupplierid(int supplierId);
        List<SupplierCategory> GetSupplierCategoryWithCategoryName(int supplierId);
        List<SupplierCategory> GetSupplierCategoryAndSubCategory(int supplierId);
    }

    public class SupplierCategoryControllerImpl : SupplierCategoryController
    {
        public int deleteSupplierCategoryBySupplierid(int supplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierCategoryDAO supplierCategoryDAO = DAOFactory.createSupplierCategoryDAO();
                return supplierCategoryDAO.deleteSupplierCategoryBySupplierid(supplierId, dbConnection);
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

        public List<SupplierCategory> GetSupplierCategoryBySupplierId(int supplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierCategoryDAO supplierCategoryDAO = DAOFactory.createSupplierCategoryDAO();
                return supplierCategoryDAO.GetSupplierCategoryBySupplierId(supplierId, dbConnection);
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

        public int saveSupplierCategory(int supplierId, int categoryId, int isActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierCategoryDAO supplierCategoryDAO = DAOFactory.createSupplierCategoryDAO();
                return supplierCategoryDAO.saveSupplierCategory( supplierId,  categoryId,  isActive, dbConnection);
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

        public int updateSupplierCategory(int supplierId, int categoryId, int isActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierCategoryDAO supplierCategoryDAO = DAOFactory.createSupplierCategoryDAO();
                return supplierCategoryDAO.updateSupplierCategory( supplierId,  categoryId,  isActive, dbConnection);
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

        public List<SupplierCategory> GetSupplierCategoryWithCategoryName(int supplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierCategoryDAO supplierCategoryDAO = DAOFactory.createSupplierCategoryDAO();
                return supplierCategoryDAO.GetSupplierCategoryWithCategoryName(supplierId, dbConnection);
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

        public List<SupplierCategory> GetSupplierCategoryAndSubCategory(int supplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierCategoryDAO supplierCategoryDAO = DAOFactory.createSupplierCategoryDAO();
                return supplierCategoryDAO.GetSupplierCategoryAndSubCategory(supplierId, dbConnection);
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
