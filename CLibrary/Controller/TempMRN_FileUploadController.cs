using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface TempMRN_FileUploadController
    {
        int SaveTempImageUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName);
        List<TempMRN_FileUpload> GetTempMrnFiles(int ItemId, int MrnId);
        int DeleteTempDataFileUploadDepartmentId(int DepartmentId);
        List<TempMRN_FileUpload> GetPrUpoadFilesListByMRNIdItemId(int DepartmentId, int MrnId, int itemId);
        int DeleteTempDataFileUpload(int DepartmentId, int ItemId);
        int DeleteTempMrnDetailFileUpload(int MrnId, int DepartmentId, int ItemId);
        int GetTempMrnFilesTemp(int ItemId, int MrnId, string FilePath);

    }

    public class TempMRN_FileUploadControllerImpl : TempMRN_FileUploadController
    {
        public int SaveTempImageUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_FileUploadDAO tempMRN_FileUploadDAO = DAOFactory.CreateTempMRN_FileUploadDAO();
                return tempMRN_FileUploadDAO.SaveTempImageUpload(DepartmentId, ItemId, MrnId, FilePath, FileName, dbConnection);
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
        public List<TempMRN_FileUpload> GetTempMrnFiles(int ItemId, int MrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_FileUploadDAO tempMRN_FileUploadDAO = DAOFactory.CreateTempMRN_FileUploadDAO();
                return tempMRN_FileUploadDAO.GetTempMrnFiles(ItemId, MrnId, dbConnection);
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
        public int DeleteTempDataFileUploadDepartmentId(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_FileUploadDAO tempMRN_FileUploadDAO = DAOFactory.CreateTempMRN_FileUploadDAO();
                return tempMRN_FileUploadDAO.DeleteTempDataFileUploadCompanyId(DepartmentId, dbConnection);
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
        public List<TempMRN_FileUpload> GetPrUpoadFilesListByMRNIdItemId(int DepartmentId, int MrnId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_FileUploadDAO tempMRN_FileUploadDAO = DAOFactory.CreateTempMRN_FileUploadDAO();
                return tempMRN_FileUploadDAO.GetPrUpoadFilesListByMRNIdItemId(DepartmentId, MrnId, ItemId, dbConnection);
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
        public int DeleteTempDataFileUpload(int DepartmentId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_FileUploadDAO tempMRN_FileUploadDAO = DAOFactory.CreateTempMRN_FileUploadDAO();
                return tempMRN_FileUploadDAO.DeleteTempDataFileUpload(DepartmentId, ItemId, dbConnection);
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
        public int DeleteTempMrnDetailFileUpload(int MrnId, int DepartmentId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_FileUploadDAO tempMRN_FileUploadDAO = DAOFactory.CreateTempMRN_FileUploadDAO();
                return tempMRN_FileUploadDAO.DeleteTempMrnDetailFileUpload(MrnId, DepartmentId, ItemId, dbConnection);
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
        public int GetTempMrnFilesTemp(int ItemId, int MrnId, string FilePath)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_FileUploadDAO tempMRN_FileUploadDAO = DAOFactory.CreateTempMRN_FileUploadDAO();
                return tempMRN_FileUploadDAO.GetTempMrnFilesTemp(ItemId, MrnId, FilePath, dbConnection);
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
