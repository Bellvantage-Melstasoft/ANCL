using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public interface UserRoleController
    {
        int SaveUserRole(string roleName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, int CompanyId);
        int UpdateUserRole(int roleId, string roleName, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int DeleteUserRole(int roleId, int CompanyId);
        List<UserRole> FetchUserRole(int CompanyId);
        UserRole FetchUserRoleObjByRoleId(int roleId);
        List<UserRole> SearchUserRole(string text, int CompanyId);
        int RestoreRole(int roleId, int CompanyId);
    }
    public class UserRoleControllerImpl : UserRoleController
    {
        public int DeleteUserRole(int roleId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                UserRoleDAO userRoleDAO = DAOFactory.CreateUserRoleDAO();
                return userRoleDAO.DeleteUserRole( roleId, CompanyId, dbConnection);
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

        public List<UserRole> FetchUserRole(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                UserRoleDAO userRoleDAO = DAOFactory.CreateUserRoleDAO();
                return userRoleDAO.FetchUserRole(CompanyId,dbConnection);
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

        public UserRole FetchUserRoleObjByRoleId(int roleId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                UserRole userRoleObj = new UserRole();
                RoleFunctionDAO roleFunctionDAO = DAOFactory.CreateRoleFunctionDAO();
                UserRoleDAO userRoleDAO = DAOFactory.CreateUserRoleDAO();
                userRoleObj= userRoleDAO.FetchUserRoleObjByRoleId( roleId,dbConnection);
                userRoleObj._roleFunctionList = roleFunctionDAO.FetchRoleFunctionByRoleId(userRoleObj.userRoleId,dbConnection);
                return userRoleObj;

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

        public int SaveUserRole(string roleName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                UserRoleDAO userRoleDAO = DAOFactory.CreateUserRoleDAO();
                return userRoleDAO.SaveUserRole( roleName,  CreatedDate,  CreatedBy,  UpdatedDate,  UpdatedBy,  IsActive, CompanyId, dbConnection);
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

        public int UpdateUserRole(int roleId, string roleName,  DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                UserRoleDAO userRoleDAO = DAOFactory.CreateUserRoleDAO();
                return userRoleDAO.UpdateUserRole( roleId,  roleName, UpdatedDate,  UpdatedBy,  IsActive, dbConnection);
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

        public List<UserRole> SearchUserRole(string text, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                UserRoleDAO userRoleDAO = DAOFactory.CreateUserRoleDAO();
                return userRoleDAO.SearchUserRole(text, CompanyId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

            public int RestoreRole(int roleId, int CompanyId) 
            {
                DBConnection dbConnection = new DBConnection();
                try {
                    UserRoleDAO userRoleDAO = DAOFactory.CreateUserRoleDAO();
                    return userRoleDAO.RestoreRole(roleId, CompanyId, dbConnection);
                }
                catch (Exception) {
                    dbConnection.RollBack();
                    throw;
                }
                finally {
                    if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                        dbConnection.Commit();
                    }
                }
            }
        
    }
}
