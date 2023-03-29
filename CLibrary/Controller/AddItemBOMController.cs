using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface AddItemBOMController
    {
        int SaveAddItemBOM(int companyId, int itemId, int seqNo, string meterial, string description, DateTime createdDate, DateTime updateDate, string createdBy, string updatedBy, int isactive);
        // int GetNextPrIdObj(int companyId);
        // List<AddItemBOM> GetListById(int companyId, int ItemId);
        int DeleteTempData(int companyId, int ItemId);
        int DeleteTempDataByCompanyId(int companyId);
        int UpdateAddItemBOM(int companyId, int itemid, int seqno, string meterial, string description, DateTime updateDate, string updatedBy, int isactive);
        List<AddItemBOM> GetBOMListByItemId(int companyId, int ItemId);
        int DeleteBOMByItemId(int companyId, int ItemId);
        List<AddItemBOM> GetbyItemName(int MainCategoryId, int SubCategoryId, int CompanyId, string ItemName);
        int DeleteBOMByItemDet(AddItemBOM objBom);
        int SaveItemSpecDetails(int companyId, int ItemId, List<ArraySpec>SpecList, DateTime createdDate, DateTime updateDate, string createdBy, string updatedBy,int isactive);
        int UpdateItemBoms(List<AddItemBOM> List, int itemid, int companyId, int UserId);
    }
    public class AddItemBOMImpl: AddItemBOMController
    {
        public int SaveAddItemBOM(int companyId, int itemId, int seqNo, string meterial, string description, DateTime createdDate, DateTime updateDate, string createdBy, string updatedBy, int isactive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
                return addItemBOMDAO.SaveAddItemBOM(companyId, itemId, seqNo, meterial, description, createdDate, updateDate, createdBy, updatedBy, isactive, dbConnection);
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

        public int UpdateAddItemBOM(int companyId, int itemid, int seqno, string meterial, string description, DateTime updateDate, string updatedBy, int isactive)
        {
            DBConnection dbconnection = new DBConnection();
            try
            {
                AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
                return addItemBOMDAO.UpdateAddItemBOM(companyId, itemid, seqno, meterial, description, updateDate, updatedBy, isactive, dbconnection);
            }
            catch (Exception)
            {
                dbconnection.RollBack();
                throw;
            }
            finally
            {
                if (dbconnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbconnection.Commit();
                }
            }
        }
        //public int GetNextPrIdObj(int DepartmentId)
        //{
        //    DBConnection dbConnection = new DBConnection();
        //    try
        //    {
        //        AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
        //        return tempBOMDAO.GetNextPrIdObj(DepartmentId, dbConnection);
        //    }
        //    catch (Exception)
        //    {
        //        dbConnection.RollBack();
        //        throw;
        //    }
        //    finally
        //    {
        //        if (dbConnection.con.State == System.Data.ConnectionState.Open)
        //        {
        //            dbConnection.Commit();
        //        }
        //    }
        //}

        //public List<TempBOM> GetListById(int companyId, int ItemId)
        //{
        //    DBConnection dbConnection = new DBConnection();
        //    try
        //    {
        //        AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
        //        return addItemBOMDAO.(companyId, ItemId, dbConnection);
        //    }
        //    catch (Exception)
        //    {
        //        dbConnection.RollBack();
        //        throw;
        //    }
        //    finally
        //    {
        //        if (dbConnection.con.State == System.Data.ConnectionState.Open)
        //        {
        //            dbConnection.Commit();
        //        }
        //    }
        //}

        public int DeleteTempData(int companyId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
                return addItemBOMDAO.DeleteTempData(companyId, ItemId, dbConnection);
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

        public int DeleteTempDataByCompanyId(int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
                return addItemBOMDAO.DeleteTempDataByCompanyId(companyId, dbConnection);
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

        public List<AddItemBOM> GetBOMListByItemId(int companyId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
                return addItemBOMDAO.GetBOMListByItemId(companyId, ItemId, dbConnection);
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

        public int DeleteBOMByItemId(int companyId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
                return addItemBOMDAO.DeleteBOMByItemId(companyId,ItemId, dbConnection);
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
        public List<AddItemBOM> GetbyItemName(int MainCategoryId, int SubCategoryId, int CompanyId, string ItemName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
                return addItemBOMDAO.GetbyItemName(MainCategoryId, SubCategoryId, CompanyId, ItemName, dbConnection);
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

        public int DeleteBOMByItemDet(AddItemBOM objBom)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
                return addItemBOMDAO.DeleteBOMByItemDet(objBom, dbConnection);
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

        public int SaveItemSpecDetails(int companyId, int ItemId,List<ArraySpec>SpecList, DateTime createdDate, DateTime updateDate, string createdBy, string updatedBy, int isactive)
        {
            int result = 0;
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
                
                foreach(ArraySpec item in SpecList)
                {
                    result = addItemBOMDAO.SaveAddItemBOMinEdit(companyId, ItemId, item.Metirial, item.Description, createdDate, updateDate, createdBy, updatedBy, isactive, dbConnection);
                    result++;
                }

                return result;

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

        public int UpdateItemBoms(List<AddItemBOM> List, int itemid, int companyId, int UserId) {
            DBConnection dbconnection = new DBConnection();
            try {
                AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
                return addItemBOMDAO.UpdateItemBoms(List, itemid, companyId,UserId, dbconnection);
            }
            catch (Exception) {
                dbconnection.RollBack();
                throw;
            }
            finally {
                if (dbconnection.con.State == System.Data.ConnectionState.Open) {
                    dbconnection.Commit();
                }
            }
        }
    }
}
