using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;

namespace CLibrary.Controller
{
    public interface MRNFileUploadController
    {
        int SaveFileUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName);

        List<MRNFileUpload> FtechUploadeFiles(int Mrnid, int itemId);

        int DeleteFileUpload(int Mrnid, int itemId);

        int DeleteParticularFile(int Mrnid, int itemId, string imagepath);
    }
    public class MRNFileUploadControllerImpl : MRNFileUploadController
    {

        public int SaveFileUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNFileUploadDAO mrnFileUploadDAO = DAOFactory.CreateMRNFileUploadDAO();
                return mrnFileUploadDAO.SaveFileUpload(DepartmentId, ItemId, MrnId, FilePath, FileName, dbConnection);
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


        public List<MRNFileUpload> FtechUploadeFiles(int Mrnid, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNFileUploadDAO mrnFileUploadDAO = DAOFactory.CreateMRNFileUploadDAO();
                return mrnFileUploadDAO.FtechUploadeFiles(Mrnid, ItemId, dbConnection);
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

        public int DeleteFileUpload(int Mrnid, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNFileUploadDAO mrnFileUploadDAO = DAOFactory.CreateMRNFileUploadDAO();
                return mrnFileUploadDAO.DeleteFileUpload(Mrnid, itemId, dbConnection);
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

        public int DeleteParticularFile(int Mrnid, int itemId, string imagepath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNFileUploadDAO mrnFileUploadDAO = DAOFactory.CreateMRNFileUploadDAO();
                return mrnFileUploadDAO.DeleteParticularFile(Mrnid, itemId, imagepath, dbConnection);
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
