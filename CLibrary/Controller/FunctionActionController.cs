using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public interface FunctionActionController
    {
        int SaveFunctionAction(string actionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int UpdateFunctionAction(int actionId, string actionName, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int DeleteFunctionAction(int actionId);
        List<FunctionAction> FetchFunctionAction();
        FunctionAction FetchFunctionActionObjByActionoid(int actionId);
    }
    public class FunctionActionControllerImpl : FunctionActionController
    {
        public int DeleteFunctionAction(int actionId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                FunctionActionDAO functionActionDAO = DAOFactory.CreateFunctionActionDAO();
                return functionActionDAO.DeleteFunctionAction(actionId, dbConnection);
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

        public List<FunctionAction> FetchFunctionAction()
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                FunctionActionDAO functionActionDAO = DAOFactory.CreateFunctionActionDAO();
                return functionActionDAO.FetchFunctionAction(dbConnection);
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

        public FunctionAction FetchFunctionActionObjByActionoid(int actionId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                FunctionActionDAO functionActionDAO = DAOFactory.CreateFunctionActionDAO();
                return functionActionDAO.FetchFunctionActionObjByActionoid( actionId,dbConnection);
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

        public int SaveFunctionAction(string actionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                FunctionActionDAO functionActionDAO = DAOFactory.CreateFunctionActionDAO();
                return functionActionDAO.SaveFunctionAction(actionName, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, IsActive, dbConnection);
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

        public int UpdateFunctionAction(int actionId, string actionName, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                FunctionActionDAO functionActionDAO = DAOFactory.CreateFunctionActionDAO();
                return functionActionDAO.UpdateFunctionAction(actionId,actionName, UpdatedDate, UpdatedBy, IsActive, dbConnection);
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
