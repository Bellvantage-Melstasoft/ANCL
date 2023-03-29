using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface TempPR_SupportiveDocumentController
    {
        int SaveTempSupportiveUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName);
        List<TempPR_SupportiveDocument> GetTempPrSupportiveFiles(int ItemId, int PrId);
        int DeleteTempSupporiveFileUpload(int DepartmentId, int ItemId);
        int DeleteTempSupporiveFileUploadCompanyId(int DepartmentId);
        int DeleteTempPrDetailSupporiveUpload(int PrId, int DepartmentId, int ItemId);
        List<TempPR_SupportiveDocument> GetPrSupporiveUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId);
        int GetTempPrSupportiveFilesTemp(int ItemId, int PrId, string FilePath);
    }

    public class TempPR_SupportiveDocumentControllerImpl : TempPR_SupportiveDocumentController
    {
        public int DeleteTempPrDetailSupporiveUpload(int PrId, int DepartmentId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPR_SupportiveDocumentDAO tempPR_SupportiveDocumentDAO = DAOFactory.CreateTempPR_SupportiveDocumentDAO();
                return tempPR_SupportiveDocumentDAO.DeleteTempPrDetailSupporiveUpload( PrId,  DepartmentId,  ItemId, dbConnection);
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

        public int DeleteTempSupporiveFileUpload(int DepartmentId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPR_SupportiveDocumentDAO tempPR_SupportiveDocumentDAO = DAOFactory.CreateTempPR_SupportiveDocumentDAO();
                return tempPR_SupportiveDocumentDAO.DeleteTempSupporiveFileUpload( DepartmentId,  ItemId, dbConnection);
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

        public int DeleteTempSupporiveFileUploadCompanyId(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPR_SupportiveDocumentDAO tempPR_SupportiveDocumentDAO = DAOFactory.CreateTempPR_SupportiveDocumentDAO();
                return tempPR_SupportiveDocumentDAO.DeleteTempSupporiveFileUploadCompanyId( DepartmentId, dbConnection);
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

        public List<TempPR_SupportiveDocument> GetPrSupporiveUpoadFilesListByPrIdItemId(int DepartmentId, int prId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPR_SupportiveDocumentDAO tempPR_SupportiveDocumentDAO = DAOFactory.CreateTempPR_SupportiveDocumentDAO();
                return tempPR_SupportiveDocumentDAO.GetPrSupporiveUpoadFilesListByPrIdItemId( DepartmentId,  prId,  ItemId, dbConnection);
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

        public List<TempPR_SupportiveDocument> GetTempPrSupportiveFiles(int ItemId, int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPR_SupportiveDocumentDAO tempPR_SupportiveDocumentDAO = DAOFactory.CreateTempPR_SupportiveDocumentDAO();
                return tempPR_SupportiveDocumentDAO.GetTempPrSupportiveFiles( ItemId,  PrId, dbConnection);
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

        public int GetTempPrSupportiveFilesTemp(int ItemId, int PrId, string FilePath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPR_SupportiveDocumentDAO tempPR_SupportiveDocumentDAO = DAOFactory.CreateTempPR_SupportiveDocumentDAO();
                return tempPR_SupportiveDocumentDAO.GetTempPrSupportiveFilesTemp( ItemId,  PrId,  FilePath, dbConnection);
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

        public int SaveTempSupportiveUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempPR_SupportiveDocumentDAO tempPR_SupportiveDocumentDAO = DAOFactory.CreateTempPR_SupportiveDocumentDAO();
                return tempPR_SupportiveDocumentDAO.SaveTempSupportiveUpload( DepartmentId,  ItemId,  PrId,  FilePath,  FileName, dbConnection);
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
