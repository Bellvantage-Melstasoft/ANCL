using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface MRsupportiveDocumentController
    {
        int SaveSupporiveFileUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName);

        List<MRNSupportiveDocument> FtechUploadeSupporiveFiles(int Mrnid, int itemId);
        int DeleteParticularSupporiveFile(int Mrnid, int itemId, string imagepath);

    }

    public class MRsupportiveDocumentControllerImpl : MRsupportiveDocumentController
    {
        public int SaveSupporiveFileUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRsupportiveDocumentDAO mrsupportiveDocumentDAO = DAOFactory.CreateMRsupportiveDocumentDAO();
                return mrsupportiveDocumentDAO.SaveSupporiveFileUpload(DepartmentId, ItemId, MrnId, FilePath, FileName, dbConnection);
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
        public List<MRNSupportiveDocument> FtechUploadeSupporiveFiles(int Mrnid, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRsupportiveDocumentDAO mrsupportiveDocumentDAO = DAOFactory.CreateMRsupportiveDocumentDAO();
                return mrsupportiveDocumentDAO.FtechUploadeSupporiveFiles(Mrnid, itemId, dbConnection);
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


        public int DeleteParticularSupporiveFile(int Mrnid, int itemId, string imagepath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRsupportiveDocumentDAO mrsupportiveDocumentDAO = DAOFactory.CreateMRsupportiveDocumentDAO();
                return mrsupportiveDocumentDAO.DeleteParticularSupporiveFile(Mrnid, itemId, imagepath, dbConnection);
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
