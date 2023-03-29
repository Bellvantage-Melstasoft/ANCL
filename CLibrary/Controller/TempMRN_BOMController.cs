using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;

namespace CLibrary.Controller
{
    public interface TempMRN_BOMController
    {
        int SaveTempBOM(int DepartmentId, int Mrnid, int ItemId, string Meterial, string Description);
        List<TempMRN_BOM> GetListById(int DepartmentId, int ItemId);
        int DeleteTempDataByDeptId(int DepartmentId);

        int GetNextMrnIdObj(int DepartmentId);

        int DeleteBOMByMrnId(int MrnId, int DepartmentId, int ItemId);

       List<TempMRN_BOM> GetBOMListByMrnIdItemId(int DepartmentId, int MrnId, int itemId);

       List<TempMRN_BOM> GetItemspecification(int itemID);

       int DeleteTempData(int DepartmentId, int ItemId);
    }
    public class TempMRN_BOMControllerImpl : TempMRN_BOMController
    {
        public int SaveTempBOM(int DepartmentId, int Mrnid, int ItemId, string Meterial, string Description)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_BOM_DAO tempMRN_BOM_DAO = DAOFactory.CreateTempMRN_BOM_DAO();
                return tempMRN_BOM_DAO.SaveTempBOM(DepartmentId, Mrnid, ItemId, Meterial, Description, dbConnection);
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

        public List<TempMRN_BOM> GetListById(int DepartmentId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_BOM_DAO tempMRN_BOM_DAO = DAOFactory.CreateTempMRN_BOM_DAO();
                return tempMRN_BOM_DAO.GetListById(DepartmentId, ItemId, dbConnection);
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

        public int DeleteTempDataByDeptId(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_BOM_DAO tempMRN_BOM_DAO = DAOFactory.CreateTempMRN_BOM_DAO();
                return tempMRN_BOM_DAO.DeleteTempDataByDeptId(DepartmentId, dbConnection);
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

        public int GetNextMrnIdObj(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_BOM_DAO tempMRN_BOM_DAO = DAOFactory.CreateTempMRN_BOM_DAO();
                return tempMRN_BOM_DAO.GetNextMrnIdObj(DepartmentId, dbConnection);
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

        public int DeleteTempData(int DepartmentId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_BOM_DAO tempMRN_BOM_DAO = DAOFactory.CreateTempMRN_BOM_DAO();
                return tempMRN_BOM_DAO.DeleteTempData(DepartmentId, ItemId, dbConnection);
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
        public int DeleteBOMByMrnId(int MrnId, int DepartmentId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_BOM_DAO tempMRN_BOM_DAO = DAOFactory.CreateTempMRN_BOM_DAO();
                return tempMRN_BOM_DAO.DeleteBOMByMrnId(MrnId, DepartmentId, ItemId, dbConnection);
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

        public List<TempMRN_BOM> GetBOMListByMrnIdItemId(int DepartmentId, int MrnId, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_BOM_DAO tempMRN_BOM_DAO = DAOFactory.CreateTempMRN_BOM_DAO();
                return tempMRN_BOM_DAO.GetBOMListByMrnItemId(DepartmentId, MrnId, itemId, dbConnection);
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
        public List<TempMRN_BOM> GetItemspecification(int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_BOM_DAO tempMRN_BOM_DAO = DAOFactory.CreateTempMRN_BOM_DAO();
                return tempMRN_BOM_DAO.GetItemspecification(ItemId, dbConnection);
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

        //public int DeleteTempData(int DepartmentId, int ItemId)
        //{
        //    DBConnection dbConnection = new DBConnection();
        //    try
        //    {
        //        TempMRN_BOM_DAO tempMRN_BOM_DAO = DAOFactory.CreateTempMRN_BOM_DAO();
        //        return tempMRN_BOM_DAO.DeleteTempData(DepartmentId, ItemId, dbConnection);
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
    }
}
