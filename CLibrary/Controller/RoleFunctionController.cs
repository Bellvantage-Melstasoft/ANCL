using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public interface RoleFunctionController
    {
        int SaveRoleFunction(int roleId, int sysDivisionid, int functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int UpdateRoleFunction(int roleId, int oldFunctionId, int newFunctionId, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int DeleteRoleFunction(int roleId, int functionId);
        List<RoleFunction> FetchRoleFunctionByRoleId(int roleId);
        int DeleteRoleFunctionByRoleId(int roleId);
        List<RoleFunction> FetchRoleFunctionByRoleIdAndDivId(int roleId, int sysDivisionid);
        List<RoleFunction> FetchRoledevisionByRoleIdforgrid(int roleId);
        List<RoleFunction> FetchRoleFunctionByRoleIdforgrid(int roleId, int catagory);
        int DeleteRolewithdevisionFunction(int roleId, int sysDivisionid, int functionId);
        List<RoleFunction> FetchAccessdevisionByUseridforgrid(int roleId, int userId);
        List<RoleFunction> FetchAccessFunctionByUseridforgrid(int roleId, int catagory, int userId);

        int DeleteSysDivisonsByRoleIdAndDivId(int roleId, int sysDivisionId);
    }
    public class RoleFunctionControllerImp : RoleFunctionController
    {
        public int DeleteRoleFunction(int roleId, int functionId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.DeleteRoleFunction( roleId,  functionId, dbConnection);
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

        public int DeleteRoleFunctionByRoleId(int roleId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.DeleteRoleFunctionByRoleId(roleId, dbConnection);
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

        public int DeleteSysDivisonsByRoleIdAndDivId(int roleId, int sysDivisionId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.DeleteSysDivisonsByRoleIdAndDivId( roleId,  sysDivisionId, dbConnection);
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

        public List<RoleFunction> FetchRoleFunctionByRoleId(int roleId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.FetchRoleFunctionByRoleId( roleId, dbConnection);
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

        public List<RoleFunction> FetchRoledevisionByRoleIdforgrid(int roleId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.FetchRoledevisionByRoleIdforgrid(roleId, dbConnection);
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

        public List<RoleFunction> FetchRoleFunctionByRoleIdforgrid(int roleId,int catagory)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.FetchRoleFunctionByRoleIdforgrid(roleId,catagory,dbConnection);
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

        public List<RoleFunction> FetchRoleFunctionByRoleIdAndDivId(int roleId, int sysDivisionid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.FetchRoleFunctionByRoleIdAndDivId( roleId,  sysDivisionid, dbConnection);
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

        public int SaveRoleFunction(int roleId, int sysDivisionid, int functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.SaveRoleFunction( roleId, sysDivisionid, functionId,  CreatedDate,  CreatedBy,  UpdatedDate,  UpdatedBy,  IsActive, dbConnection);
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


        public int DeleteRoleFunction(int roleId, int sysDivisionid, int functionId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.DeleteSysDivisonsByRoleIdAndDivId(roleId, sysDivisionid, dbConnection);
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

        public int UpdateRoleFunction(int roleId, int oldFunctionId, int newFunctionId, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.UpdateRoleFunction( roleId,  oldFunctionId,  newFunctionId,  UpdatedDate,  UpdatedBy,  IsActive, dbConnection);
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

        public int DeleteRolewithdevisionFunction(int roleId, int sysDivisionid, int functionId) 
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.DeleteRolewithdevisionFunction(roleId, sysDivisionid,functionId,dbConnection);
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


        public List<RoleFunction> FetchAccessdevisionByUseridforgrid(int roleId, int userId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.FetchAccessdevisionByUseridforgrid(roleId,userId, dbConnection);
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

        public List<RoleFunction> FetchAccessFunctionByUseridforgrid(int roleId, int catagory, int userId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                return roleFunctionDAO.FetchAccessFunctionByUseridforgrid(roleId, catagory, userId, dbConnection);
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
