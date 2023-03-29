using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
  public  interface TempMRN_FileUploadReplacementController
    {
        int SaveTempImageUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName);

        List<TempMRN_FileUploadReplacement> GetTempMrnFiles(int ItemId, int MrnId);

        int DeleteTempDataFileUploadCompanyId(int CompanyId);

        List<TempMRN_FileUploadReplacement> GetMRNUpoadFilesListByMRNIdItemId(int DepartmentId, int MrnId, int itemId);

        int DeleteTempMrnDetailFileUpload(int MrnId, int DepartmentId, int ItemId);

        int DeleteTempDataFileUpload(int DepartmentId, int ItemId);


        int GetTempMrnFilesTemp(int ItemId, int PrId, string FilePath);
       
    }

    public class TempMRN_FileUploadReplacementControllerImpl : TempMRN_FileUploadReplacementController
    {
        public int SaveTempImageUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_FileUploadReplacementDAO tempMRN_FileUploadReplacementDAO = DAOFactory.CreateTempMRN_FileUploadReplacementDAO();
                return tempMRN_FileUploadReplacementDAO.SaveTempImageUpload(DepartmentId, ItemId, MrnId, FilePath, FileName, dbConnection);
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

        public List<TempMRN_FileUploadReplacement> GetTempMrnFiles(int ItemId, int MrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_FileUploadReplacementDAO tempMRN_FileUploadReplacementDAO = DAOFactory.CreateTempMRN_FileUploadReplacementDAO();
                return tempMRN_FileUploadReplacementDAO.GetTempMrnFiles(ItemId, MrnId, dbConnection);
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
                TempMRN_FileUploadReplacementDAO tempMRN_FileUploadReplacementDAO = DAOFactory.CreateTempMRN_FileUploadReplacementDAO();
                return tempMRN_FileUploadReplacementDAO.DeleteTempDataFileUploadCompanyId(DepartmentId, dbConnection);
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
        public List<TempMRN_FileUploadReplacement> GetMRNUpoadFilesListByMRNIdItemId(int DepartmentId, int MrnId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempMRN_FileUploadReplacementDAO tempMRN_FileUploadReplacementDAO = DAOFactory.CreateTempMRN_FileUploadReplacementDAO();
                return tempMRN_FileUploadReplacementDAO.GetMRNUpoadFilesListByMRNIdItemId(DepartmentId, MrnId, ItemId, dbConnection);
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
                TempMRN_FileUploadReplacementDAO tempMRN_FileUploadReplacementDAO = DAOFactory.CreateTempMRN_FileUploadReplacementDAO();
                return tempMRN_FileUploadReplacementDAO.DeleteTempMrnDetailFileUpload(MrnId, DepartmentId, ItemId, dbConnection);
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
                TempMRN_FileUploadReplacementDAO tempMRN_FileUploadReplacementDAO = DAOFactory.CreateTempMRN_FileUploadReplacementDAO();
                return tempMRN_FileUploadReplacementDAO.DeleteTempDataFileUpload(DepartmentId, ItemId, dbConnection);
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
                TempMRN_FileUploadReplacementDAO tempMRN_FileUploadReplacementDAO = DAOFactory.CreateTempMRN_FileUploadReplacementDAO();
                return tempMRN_FileUploadReplacementDAO.GetTempMrnFilesTemp(ItemId, MrnId, FilePath, dbConnection);
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
