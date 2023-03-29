using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface PR_SupportiveDocumentController
    {
        int SaveSupporiveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName);
        List<PR_SupportiveDocument> FtechUploadeSupporiveFiles(int PrId, int ItemId);
        int DeleteFileUpload(int PrId, int itemId);
        int DeleteParticularSupporiveFile(int PrId, int itemId, string imagepath);
        List<PR_SupportiveDocument> FtechUploadeSupportiveDocmentsRejected(int PrId);
    }

    public class PR_SupportiveDocumentControllerImpl : PR_SupportiveDocumentController
    {
        public int DeleteFileUpload(int PrId, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_SupportiveDocumentDAO pR_SupportiveDocumentDAO = DAOFactory.CreatePR_SupportiveDocumentDAO();
                return pR_SupportiveDocumentDAO.DeleteFileUpload( PrId,  itemId, dbConnection);
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

        public int DeleteParticularSupporiveFile(int PrId, int itemId, string imagepath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_SupportiveDocumentDAO pR_SupportiveDocumentDAO = DAOFactory.CreatePR_SupportiveDocumentDAO();
                return pR_SupportiveDocumentDAO.DeleteParticularSupporiveFile( PrId,  itemId,  imagepath, dbConnection);
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

        public List<PR_SupportiveDocument> FtechUploadeSupporiveFiles(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_SupportiveDocumentDAO pR_SupportiveDocumentDAO = DAOFactory.CreatePR_SupportiveDocumentDAO();
                return pR_SupportiveDocumentDAO.FtechUploadeSupporiveFiles( PrId,  ItemId, dbConnection);
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

        public List<PR_SupportiveDocument> FtechUploadeSupportiveDocmentsRejected(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_SupportiveDocumentDAO pR_SupportiveDocumentDAO = DAOFactory.CreatePR_SupportiveDocumentDAO();
                return pR_SupportiveDocumentDAO.FtechUploadeSupportiveDocmentsRejected( PrId, dbConnection);
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

        public int SaveSupporiveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_SupportiveDocumentDAO pR_SupportiveDocumentDAO = DAOFactory.CreatePR_SupportiveDocumentDAO();
                return pR_SupportiveDocumentDAO.SaveSupporiveFileUpload( DepartmentId,  ItemId,  PrId,  FilePath,  FileName, dbConnection);
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
