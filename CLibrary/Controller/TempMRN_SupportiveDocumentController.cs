using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
   public interface TempMRN_SupportiveDocumentController
    {
       int SaveTempSupportiveUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName);
       List<TempMRN_SupportiveDocument> GetTempMrnSupportiveFiles(int ItemId, int PrId);

       int DeleteTempSupporiveFileUploadCompanyId(int DepartmentId);

       List<TempMRN_SupportiveDocument> GetMRNSupporiveUpoadFilesListByMRNIdItemId(int DepartmentId, int MrnId, int itemId);

       int DeleteTempSupporiveFileUpload(int DepartmentId, int ItemId);

       int DeleteTempMrnDetailSupporiveUpload(int MrnId, int DepartmentId, int ItemId);
       int GetTempMrnSupportiveFilesTemp(int ItemId, int MrnId, string FilePath);
     
    }
   public class TempMRN_SupportiveDocumentControllerImpl : TempMRN_SupportiveDocumentController
   {
       public int SaveTempSupportiveUpload(int DepartmentId, int ItemId, int MrnId, string FilePath, string FileName)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               TempMRN_SupportiveDocumentDAO tempMRN_SupportiveDocumentDAO = DAOFactory.CreateTempMRN_SupportiveDocumentDAO();
               return tempMRN_SupportiveDocumentDAO.SaveTempSupportiveUpload(DepartmentId, ItemId, MrnId, FilePath, FileName, dbConnection);
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
       public List<TempMRN_SupportiveDocument> GetTempMrnSupportiveFiles(int ItemId, int MrnId)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               TempMRN_SupportiveDocumentDAO tempMRN_SupportiveDocumentDAO = DAOFactory.CreateTempMRN_SupportiveDocumentDAO();
               return tempMRN_SupportiveDocumentDAO.GetTempMrnSupportiveFiles(ItemId, MrnId, dbConnection);
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
       public List<TempMRN_SupportiveDocument> GetMRNSupporiveUpoadFilesListByMRNIdItemId(int DepartmentId, int MrnId, int ItemId)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               TempMRN_SupportiveDocumentDAO tempMRN_SupportiveDocumentDAO = DAOFactory.CreateTempMRN_SupportiveDocumentDAO();
               return tempMRN_SupportiveDocumentDAO.GetMRNSupporiveUpoadFilesListByMRNIdItemId(DepartmentId, MrnId, ItemId, dbConnection);
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
               
               TempMRN_SupportiveDocumentDAO tempMRN_SupportiveDocumentDAO = DAOFactory.CreateTempMRN_SupportiveDocumentDAO();
               return tempMRN_SupportiveDocumentDAO.DeleteTempSupporiveFileUpload(DepartmentId, ItemId, dbConnection);
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
       public int DeleteTempMrnDetailSupporiveUpload(int MrnId, int DepartmentId, int ItemId)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               TempMRN_SupportiveDocumentDAO tempMRN_SupportiveDocumentDAO = DAOFactory.CreateTempMRN_SupportiveDocumentDAO();
               return tempMRN_SupportiveDocumentDAO.DeleteTempMrnDetailSupporiveUpload(MrnId, DepartmentId, ItemId, dbConnection);
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
       public int GetTempMrnSupportiveFilesTemp(int ItemId, int MrnId, string FilePath)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               TempMRN_SupportiveDocumentDAO tempMRN_SupportiveDocumentDAO = DAOFactory.CreateTempMRN_SupportiveDocumentDAO();
               return tempMRN_SupportiveDocumentDAO.GetTempMrnSupportiveFilesTemp(ItemId, MrnId, FilePath, dbConnection);
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
               TempMRN_SupportiveDocumentDAO tempMRN_SupportiveDocumentDAO = DAOFactory.CreateTempMRN_SupportiveDocumentDAO();
               return tempMRN_SupportiveDocumentDAO.DeleteTempSupporiveFileUploadCompanyId(DepartmentId, dbConnection);
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
