using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
   public interface Mrn_Replace_FileUpload_Controller
    {
     
       int SaveFileUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName);
       List<Mrn_Replace_File_Upload> FtechUploadeFiles(int MrnId, int ItemId);
       List<Mrn_Replace_File_Upload> FtechUploadeFilesRejected(int MrnId);
       int DeleteFileUpload(int MrnId, int itemId);
       int DeleteParticularReplaceFile(int MrnId, int itemId, string imagepath);
       int updateDefaultReplaceImage(int MrnId, int itemId, string imagepath, int isDefault);
       int updateReleaseImageDefault(int MrnId, int itemId);
       Mrn_Replace_File_Upload fetchMrn_Replace_File_UploadObjForDefaultImage(int MrnId, int itemId);
    }

   public class Mrn_Replace_FileUpload_ControllerImpl : Mrn_Replace_FileUpload_Controller
   {

       public int SaveFileUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               Mrn_Replace_File_Upload_DAO Mrn_Replace_File_Upload_DAO = DAOFactory.CreateMrn_Replace_File_Upload_DAO();
               return Mrn_Replace_File_Upload_DAO.SaveFileUpload(DepartmentId, ItemId, MrnId, FilePath, FileName, dbConnection);
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

       public List<Mrn_Replace_File_Upload> FtechUploadeFiles(int MrnId, int ItemId)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               Mrn_Replace_File_Upload_DAO Mrn_Replace_File_Upload_DAO = DAOFactory.CreateMrn_Replace_File_Upload_DAO();
               return Mrn_Replace_File_Upload_DAO.FtechUploadeFiles(MrnId, ItemId, dbConnection);
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

       public List<Mrn_Replace_File_Upload> FtechUploadeFilesRejected(int MrnId)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               Mrn_Replace_File_Upload_DAO Mrn_Replace_File_Upload_DAO = DAOFactory.CreateMrn_Replace_File_Upload_DAO();
               return Mrn_Replace_File_Upload_DAO.FtechUploadeFilesRejected(MrnId, dbConnection);
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

       public int DeleteFileUpload(int MrnId, int itemId)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               Mrn_Replace_File_Upload_DAO Mrn_Replace_File_Upload_DAO = DAOFactory.CreateMrn_Replace_File_Upload_DAO();
               return Mrn_Replace_File_Upload_DAO.DeleteFileUpload(MrnId, itemId, dbConnection);
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

       public int DeleteParticularReplaceFile(int MrnId, int itemId, string imagepath)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               Mrn_Replace_File_Upload_DAO Mrn_Replace_File_Upload_DAO = DAOFactory.CreateMrn_Replace_File_Upload_DAO();
               return Mrn_Replace_File_Upload_DAO.DeleteParticularReplaceFile(MrnId, itemId, imagepath, dbConnection);
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

       public int updateDefaultReplaceImage(int MrnId, int itemId, string imagepath, int isDefault)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               Mrn_Replace_File_Upload_DAO Mrn_Replace_File_Upload_DAO = DAOFactory.CreateMrn_Replace_File_Upload_DAO();
               return Mrn_Replace_File_Upload_DAO.updateDefaultReplaceImage(MrnId, itemId, imagepath, isDefault, dbConnection);
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

       public int updateReleaseImageDefault(int MrnId, int itemId)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               Mrn_Replace_File_Upload_DAO Mrn_Replace_File_Upload_DAO = DAOFactory.CreateMrn_Replace_File_Upload_DAO();
               return Mrn_Replace_File_Upload_DAO.updateReleaseImageDefault(MrnId, itemId, dbConnection);
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

       public Mrn_Replace_File_Upload fetchMrn_Replace_File_UploadObjForDefaultImage(int MrnId, int itemId)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               Mrn_Replace_File_Upload_DAO Mrn_Replace_File_Upload_DAO = DAOFactory.CreateMrn_Replace_File_Upload_DAO();
               return Mrn_Replace_File_Upload_DAO.fetchMrn_Replace_File_UploadObjForDefaultImage(MrnId, itemId, dbConnection);
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
