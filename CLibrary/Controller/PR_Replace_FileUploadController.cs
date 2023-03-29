using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface PR_Replace_FileUploadController
    {
        int SaveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName);
        List<PR_Replace_FileUpload> FtechUploadeFiles(int PrId, int ItemId);
        List<PR_Replace_FileUpload> FtechUploadeFilesRejected(int PrId);
        int DeleteFileUpload(int PrId, int itemId);
        int DeleteParticularReplaceFile(int PrId, int itemId, string imagepath);
        int updateDefaultReplaceImage(int PrId, int itemId, string imagepath, int isDefault);
        int updateReleaseImageDefault(int PrId, int itemId);
        PR_Replace_FileUpload fetchPR_Replace_FileUploadObjForDefaultImage(int PrId, int itemId);
    }

    public class PR_Replace_FileUploadControllerImpl : PR_Replace_FileUploadController
    {
        public int SaveFileUpload(int DepartmentId, int ItemId, int PrId, string FilePath, string FileName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_Replace_FileUploadDAO pr_Replace_FileUploadDAO = DAOFactory.CreatePR_Replace_FileUploadDAO();
                return pr_Replace_FileUploadDAO.SaveFileUpload(DepartmentId, ItemId, PrId, FilePath, FileName, dbConnection);
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

        public List<PR_Replace_FileUpload> FtechUploadeFiles(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_Replace_FileUploadDAO pr_Replace_FileUploadDAO = DAOFactory.CreatePR_Replace_FileUploadDAO();
                return pr_Replace_FileUploadDAO.FtechUploadeFiles(PrId, ItemId, dbConnection);
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

        public List<PR_Replace_FileUpload> FtechUploadeFilesRejected(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_Replace_FileUploadDAO pr_Replace_FileUploadDAO = DAOFactory.CreatePR_Replace_FileUploadDAO();
                return pr_Replace_FileUploadDAO.FtechUploadeFilesRejected(PrId, dbConnection);
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
                PR_Replace_FileUploadDAO pr_Replace_FileUploadDAO = DAOFactory.CreatePR_Replace_FileUploadDAO();
                return pr_Replace_FileUploadDAO.DeleteFileUpload(PrId, itemId, dbConnection);
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

        public int DeleteParticularReplaceFile(int PrId, int itemId, string imagepath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_Replace_FileUploadDAO pr_Replace_FileUploadDAO = DAOFactory.CreatePR_Replace_FileUploadDAO();
                return pr_Replace_FileUploadDAO.DeleteParticularReplaceFile(PrId, itemId, imagepath, dbConnection);
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

        public int updateDefaultReplaceImage(int PrId, int itemId, string imagepath, int isDefault)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_Replace_FileUploadDAO pr_Replace_FileUploadDAO = DAOFactory.CreatePR_Replace_FileUploadDAO();
                return pr_Replace_FileUploadDAO.updateDefaultReplaceImage(PrId, itemId, imagepath, isDefault, dbConnection);
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
                PR_Replace_FileUploadDAO pr_Replace_FileUploadDAO = DAOFactory.CreatePR_Replace_FileUploadDAO();
                return pr_Replace_FileUploadDAO.updateReleaseImageDefault(PrId, itemId, dbConnection);
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

        public PR_Replace_FileUpload fetchPR_Replace_FileUploadObjForDefaultImage(int PrId, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_Replace_FileUploadDAO pr_Replace_FileUploadDAO = DAOFactory.CreatePR_Replace_FileUploadDAO();
                return pr_Replace_FileUploadDAO.fetchPR_Replace_FileUploadObjForDefaultImage(PrId, itemId, dbConnection);
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
