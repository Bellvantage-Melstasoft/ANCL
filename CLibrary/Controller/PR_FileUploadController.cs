using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;

namespace CLibrary.Controller
{
    public interface PR_FileUploadController
    {
        int SaveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName);
        List<PR_FileUpload> FtechUploadeFiles(int PrId, int ItemId);
        List<PR_FileUpload> FtechUploadeFilesRejected(int PrId);
        int DeleteFileUpload(int PrId, int itemId);
        int DeleteParticularFile(int PrId, int itemId, string imagepath);
        int updateDefaultStanardImage(int PrId, int itemId, string imagepath, int isDefault);
        int updateReleaseImageDefault(int PrId, int itemId);
        PR_FileUpload fetchPr_FileuploadObjForDefaultImage(int PrId, int itemId);
    }

    public class PR_FileUploadControllerImpl : PR_FileUploadController
    {
        public int SaveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_FileUploadDAO pr_FileUploadDAO = DAOFactory.CreatePR_FileUploadDAO();
                return pr_FileUploadDAO.SaveFileUpload(DepartmentId, ItemId, PrId, FilePath, FileName, dbConnection);
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

        public List<PR_FileUpload> FtechUploadeFiles(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_FileUploadDAO pr_FileUploadDAO = DAOFactory.CreatePR_FileUploadDAO();
                return pr_FileUploadDAO.FtechUploadeFiles(PrId,ItemId, dbConnection);
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

        public List<PR_FileUpload> FtechUploadeFilesRejected(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_FileUploadDAO pr_FileUploadDAO = DAOFactory.CreatePR_FileUploadDAO();
                return pr_FileUploadDAO.FtechUploadeFilesRejected(PrId, dbConnection);
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

        public int DeleteFileUpload(int PrId, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_FileUploadDAO pr_FileUploadDAO = DAOFactory.CreatePR_FileUploadDAO();
                return pr_FileUploadDAO.DeleteFileUpload(PrId, itemId, dbConnection);
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

        public int DeleteParticularFile(int PrId, int itemId, string imagepath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_FileUploadDAO pr_FileUploadDAO = DAOFactory.CreatePR_FileUploadDAO();
                return pr_FileUploadDAO.DeleteParticularFile(PrId, itemId,imagepath, dbConnection);
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

        public int updateDefaultStanardImage(int PrId, int itemId, string imagepath, int isDefault)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_FileUploadDAO pr_FileUploadDAO = DAOFactory.CreatePR_FileUploadDAO();
                return pr_FileUploadDAO.updateDefaultStanardImage( PrId,  itemId,  imagepath,  isDefault, dbConnection);
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

        public int updateReleaseImageDefault(int PrId, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_FileUploadDAO pr_FileUploadDAO = DAOFactory.CreatePR_FileUploadDAO();
                return pr_FileUploadDAO.updateReleaseImageDefault( PrId,  itemId, dbConnection);
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

        public PR_FileUpload fetchPr_FileuploadObjForDefaultImage(int PrId, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_FileUploadDAO pr_FileUploadDAO = DAOFactory.CreatePR_FileUploadDAO();
                return pr_FileUploadDAO.fetchPr_FileuploadObjForDefaultImage( PrId,  itemId, dbConnection);
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
