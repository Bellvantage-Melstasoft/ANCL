using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;

namespace CLibrary.Controller
{
   public interface MRNBomController
   {
       int SaveBillOfMeterial(int MrnId, int ItemId,  string Meterial, string Description, int IsActive, DateTime CreatedDatetime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy);

       List<MRNBom> GetList(int Mrnid, int itemId);

       int DeleteMRNBom(int Mrnid, int itemId);

       int DeleteMRNBoMTrash(int Mrnid, int ItemId);
   }
   public class MRNBomControllerImpl : MRNBomController
   {
       public int SaveBillOfMeterial(int MrnId, int ItemId,  string Meterial, string Description, int IsActive, DateTime CreatedDatetime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               MRNBomDAO mrnBomDAO = DAOFactory.CreateMRNBomDAO();
               return mrnBomDAO.SaveBillOfMeterial(MrnId, ItemId, Meterial, Description, IsActive, CreatedDatetime, CreatedBy, UpdatedDateTime, UpdatedBy, dbConnection);
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

       public List<MRNBom> GetList(int Mrnid, int itemId)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               MRNBomDAO mrnBomDAO = DAOFactory.CreateMRNBomDAO();
               return mrnBomDAO.GetList(Mrnid, itemId, dbConnection);
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
       public int DeleteMRNBom(int Mrnid, int itemId)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               MRNBomDAO mrnBomDAO = DAOFactory.CreateMRNBomDAO();
               return mrnBomDAO.DeleteMRNBom(Mrnid, itemId, dbConnection);
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

       public int DeleteMRNBoMTrash(int Mrnid, int ItemId)
       {
           DBConnection dbConnection = new DBConnection();
           try
           {
               MRNBomDAO mrnBomDAO = DAOFactory.CreateMRNBomDAO();
               return mrnBomDAO.DeleteMRNBoMTrash(Mrnid, ItemId, dbConnection);
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
