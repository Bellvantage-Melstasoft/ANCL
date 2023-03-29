using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;

namespace CLibrary.Controller
{
    public interface TempPrFileUploadController
    {
        int SaveTempImageUpload(int DepartmentId, int ItemId, int PrId, string FilePath,string FileName);
        List<TempPrFileUpload> GetTempPrFiles(int ItemId, int PrId);
        int DeleteTempDataFileUpload(int DepartmentId, int ItemId);
        int DeleteTempDataFileUploadCompanyId(int DepartmentId);
        int DeleteTempPrDetailFileUpload(int PrId, int DepartmentId, int ItemId);
        List<TempPrFileUpload> GetPrUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId);

        int GetTempPrFilesTemp(int ItemId, int PrId, string FilePath);
    }

    public class TempPrFileUploadControllerImpl : TempPrFileUploadController
    {
        public int SaveTempImageUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPrFileUploadDAO tempPrFileUploadDAO = DAOFactory.CreateTempPrFileUploadDAO();
                return tempPrFileUploadDAO.SaveTempImageUpload(DepartmentId, ItemId, PrId, FilePath,FileName, dbConnection);
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

        public List<TempPrFileUpload> GetTempPrFiles(int ItemId, int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPrFileUploadDAO tempPrFileUpload = DAOFactory.CreateTempPrFileUploadDAO();
                return tempPrFileUpload.GetTempPrFiles(ItemId, PrId,dbConnection);
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
                TempPrFileUploadDAO tempPrFileUpload = DAOFactory.CreateTempPrFileUploadDAO();
                return tempPrFileUpload.DeleteTempDataFileUpload(DepartmentId, ItemId, dbConnection);
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

        public int DeleteTempDataFileUploadCompanyId(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPrFileUploadDAO tempPrFileUpload = DAOFactory.CreateTempPrFileUploadDAO();
                return tempPrFileUpload.DeleteTempDataFileUploadCompanyId(DepartmentId, dbConnection);
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

        public int DeleteTempPrDetailFileUpload(int PrId, int DepartmentId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPrFileUploadDAO tempPrFileUploadDAO = DAOFactory.CreateTempPrFileUploadDAO();
                return tempPrFileUploadDAO.DeleteTempPrDetailFileUpload(PrId, DepartmentId, ItemId, dbConnection);
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

        public List<TempPrFileUpload> GetPrUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPrFileUploadDAO tempPrFileUploadDAO = DAOFactory.CreateTempPrFileUploadDAO();
                return tempPrFileUploadDAO.GetPrUpoadFilesListByPrIdItemId( DepartmentId,  prId,  ItemId, dbConnection);
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

        public int GetTempPrFilesTemp(int ItemId, int PrId, string FilePath)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPrFileUploadDAO tempPrFileUpload = DAOFactory.CreateTempPrFileUploadDAO();
                return tempPrFileUpload.GetTempPrFilesTemp(ItemId, PrId, FilePath, dbConnection);
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
