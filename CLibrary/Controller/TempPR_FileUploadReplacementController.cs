using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface TempPR_FileUploadReplacementController
    {
        int SaveTempImageUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName);
        List<TempPR_FileUploadReplacement> GetTempPrFiles(int ItemId, int PrId);
        int DeleteTempDataFileUpload(int DepartmentId, int ItemId);
        int DeleteTempDataFileUploadCompanyId(int DepartmentId);
        int DeleteTempPrDetailFileUpload(int PrId, int DepartmentId, int ItemId);
        List<TempPR_FileUploadReplacement> GetPrUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId);

        int GetTempPrFilesTemp(int ItemId, int PrId, string FilePath);
    }

    public class TempPR_FileUploadReplacementControllerImpl : TempPR_FileUploadReplacementController
    {
        public int SaveTempImageUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPR_FileUploadReplacementDAO tempPR_FileUploadReplacementDAO = DAOFactory.CreateTempPR_FileUploadReplacementDAO();
                return tempPR_FileUploadReplacementDAO.SaveTempImageUpload(DepartmentId, ItemId, PrId, FilePath, FileName, dbConnection);
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

        public List<TempPR_FileUploadReplacement> GetTempPrFiles(int ItemId, int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPR_FileUploadReplacementDAO tempPR_FileUploadReplacementDAO = DAOFactory.CreateTempPR_FileUploadReplacementDAO();
                return tempPR_FileUploadReplacementDAO.GetTempPrFiles(ItemId, PrId, dbConnection);
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
                TempPR_FileUploadReplacementDAO tempPR_FileUploadReplacementDAO = DAOFactory.CreateTempPR_FileUploadReplacementDAO();
                return tempPR_FileUploadReplacementDAO.DeleteTempDataFileUpload(DepartmentId, ItemId, dbConnection);
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
                TempPR_FileUploadReplacementDAO tempPR_FileUploadReplacementDAO = DAOFactory.CreateTempPR_FileUploadReplacementDAO();
                return tempPR_FileUploadReplacementDAO.DeleteTempDataFileUploadCompanyId(DepartmentId, dbConnection);
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
                TempPR_FileUploadReplacementDAO tempPR_FileUploadReplacementDAO = DAOFactory.CreateTempPR_FileUploadReplacementDAO();
                return tempPR_FileUploadReplacementDAO.DeleteTempPrDetailFileUpload(PrId, DepartmentId, ItemId, dbConnection);
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

        public List<TempPR_FileUploadReplacement> GetPrUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPR_FileUploadReplacementDAO tempPR_FileUploadReplacementDAO = DAOFactory.CreateTempPR_FileUploadReplacementDAO();
                return tempPR_FileUploadReplacementDAO.GetPrUpoadFilesListByPrIdItemId(DepartmentId, prId, ItemId, dbConnection);
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
                TempPR_FileUploadReplacementDAO tempPR_FileUploadReplacementDAO = DAOFactory.CreateTempPR_FileUploadReplacementDAO();
                return tempPR_FileUploadReplacementDAO.GetTempPrFilesTemp(ItemId, PrId, FilePath, dbConnection);
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
