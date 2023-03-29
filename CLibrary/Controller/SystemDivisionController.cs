using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public interface SystemDivisionController
    {
        int SaveSystemDivision(string systemDivisionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int UpdateSystemDivision(int systemDivisionId, string systemDivisionName, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int assignSystemDivisionWithFunction(int systemDivisionId, int functionId, string functionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy);
        int SaveSystemDivisionfunction(int systemDivisionId, string systemDivisionfunctionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy);
        int updateSystemDivisionWithFunction(int systemDivisionId, int oldfunctionId, int newfunctionId, string newfunctionName, DateTime UpdatedDate, string UpdatedBy);
        int DeleteSystemDivision(int systemDivisionId);

        int DeleteSystemDivisionFunction(int systemDivisionId, int functionId);
        List<SystemDivision> FetchSystemDivision();
        List<SystemDivisionFunction> FetchSystemDivisionFunctionsBySystemDivisionId(int systemDivisionId);
        SystemDivision FetchSystemDivisionBySystemDivisionId(int systemDivisionId);
        SystemDivisionFunction FetchSystemDivisionFunctionsBySystemDivisionIdandFinctionId(int systemDivisionId, int functionId);
        int updateSystemDivisionFunction(int systemDivisionId, int functionId, string functionName, DateTime UpdatedDate, string UpdatedBy);
    }
    public class SystemDivisionControllerImpl : SystemDivisionController
    {
        public int assignSystemDivisionWithFunction(int systemDivisionId, int functionId, string functionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SystemDivisionDAO systemDivisionDAO = DAOFactory.CreateSystemDivisionDAO();
                return systemDivisionDAO.assignSystemDivisionWithFunction( systemDivisionId,  functionId,functionName,  CreatedDate,  CreatedBy,  UpdatedDate,  UpdatedBy, dbConnection);
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

        public int DeleteSystemDivision(int systemDivisionId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SystemDivisionDAO systemDivisionDAO = DAOFactory.CreateSystemDivisionDAO();
                return systemDivisionDAO.DeleteSystemDivision( systemDivisionId, dbConnection);
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

        public int DeleteSystemDivisionFunction(int systemDivisionId, int functionId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SystemDivisionDAO systemDivisionDAO = DAOFactory.CreateSystemDivisionDAO();
                return systemDivisionDAO.DeleteSystemDivisionFunction(systemDivisionId,functionId, dbConnection);
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

        public List<SystemDivision> FetchSystemDivision()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SystemDivisionDAO systemDivisionDAO = DAOFactory.CreateSystemDivisionDAO();
                return systemDivisionDAO.FetchSystemDivision(dbConnection);
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

        public SystemDivision FetchSystemDivisionBySystemDivisionId(int systemDivisionId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SystemDivisionDAO systemDivisionDAO = DAOFactory.CreateSystemDivisionDAO();
                return systemDivisionDAO.FetchSystemDivisionBySystemDivisionId( systemDivisionId, dbConnection);
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

        public SystemDivisionFunction FetchSystemDivisionFunctionsBySystemDivisionIdandFinctionId(int systemDivisionId, int functionId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SystemDivisionDAO systemDivisionDAO = DAOFactory.CreateSystemDivisionDAO();
                return systemDivisionDAO.FetchSystemDivisionFunctionsBySystemDivisionIdandFinctionId(systemDivisionId,functionId, dbConnection);
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

        public List<SystemDivisionFunction> FetchSystemDivisionFunctionsBySystemDivisionId(int systemDivisionId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SystemDivisionDAO systemDivisionDAO = DAOFactory.CreateSystemDivisionDAO();
                return systemDivisionDAO.FetchSystemDivisionFunctionsBySystemDivisionId( systemDivisionId, dbConnection);
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

        public int SaveSystemDivision(string systemDivisionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SystemDivisionDAO systemDivisionDAO = DAOFactory.CreateSystemDivisionDAO();
                return systemDivisionDAO.SaveSystemDivision( systemDivisionName,  CreatedDate,  CreatedBy,  UpdatedDate,  UpdatedBy,  IsActive, dbConnection);
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

        public int SaveSystemDivisionfunction(int systemDivisionId, string systemDivisionfunctionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SystemDivisionDAO systemDivisionDAO = DAOFactory.CreateSystemDivisionDAO();
                return systemDivisionDAO.SaveSystemDivisionfunction(systemDivisionId ,systemDivisionfunctionName,  CreatedDate,  CreatedBy,  UpdatedDate,  UpdatedBy, dbConnection);
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

        public int UpdateSystemDivision(int systemDivisionId, string systemDivisionName, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SystemDivisionDAO systemDivisionDAO = DAOFactory.CreateSystemDivisionDAO();
                return systemDivisionDAO.UpdateSystemDivision( systemDivisionId,  systemDivisionName, UpdatedDate,  UpdatedBy,  IsActive, dbConnection);
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

        public int updateSystemDivisionWithFunction(int systemDivisionId, int oldfunctionId, int newfunctionId, string newfunctionName, DateTime UpdatedDate, string UpdatedBy)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SystemDivisionDAO systemDivisionDAO = DAOFactory.CreateSystemDivisionDAO();
                return systemDivisionDAO.updateSystemDivisionWithFunction( systemDivisionId, oldfunctionId, newfunctionId,  newfunctionName,  UpdatedDate,  UpdatedBy, dbConnection);
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

        public int updateSystemDivisionFunction(int systemDivisionId, int functionId, string functionName, DateTime UpdatedDate, string UpdatedBy)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SystemDivisionDAO systemDivisionDAO = DAOFactory.CreateSystemDivisionDAO();
                return systemDivisionDAO.updateSystemDivisionFunction(systemDivisionId, functionId, functionName, UpdatedDate, UpdatedBy, dbConnection);
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
