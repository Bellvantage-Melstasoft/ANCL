using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public interface CompanyUserAccessController
    {
        int SaveCompanyUserAccess(int userId, int departmemtId, int roleId, int? systemDivisionId,int? functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy);
        int UpdateCompanyUserAccess(int roleId, int oldFunctionId, int newFunctionId, DateTime UpdatedDate, string UpdatedBy);
        int DeleteAllCompanyUserAccessByDividionId(int userId, int departmemtId, int userRoleId);
        int DeleteCompanyUserAccessByDividionId(int userId, int departmemtId);
        int DeleteAllCompanyUserAccessByRoleIdAndDividionId(int userId, int departmemtId, int userRoleId, int sysDivisionId);
        int DeleteCompanyUserAccessByUserId(int userId, int departmemtId);
        List<CompanyUserAccess> FetchCompanyUserAccessByUserId(int userId, int departmemtId);
        List<CompanyUserAccess> FetchCompanyUserAccessByUserIdAndUserRoleIdAndDivId(int userId, int departmemtId, int userRoleId, int sysDivId);
        List<CompanyUserAccess> FetchCompanyUserAccessByDepartentId(int departmemtId);
        List<CompanyUserAccess> GetCompanyUserAccessObjByUserId(int userId, int departmemtId);
        bool isAvilableAccess(int userId, int departmemtId, int systemDivisionId, int functionId);
        int SaveCompanyUserAccessvusename(string username, int departmemtId, int roleId, int? systemDivisionId, int? functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy);
        int DeleteAllCompanyUserAccessByRoleIdAndFunctionId(int userId, int departmemtId, int userRoleId, int sysDivisionId, int functionId);
        CompanyUserAccess FetchUserRoleIdByUserId(int userId, int companyId);
    }
    public class CompanyUserAccessControllerImpl : CompanyUserAccessController
    {
        public int DeleteAllCompanyUserAccessByDividionId(int userId, int departmemtId,  int userRoleId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyUserAccessDAO companyUserAccessDAO = DAOFactory.CreateCompanyUserAccessDAO();
                return companyUserAccessDAO.DeleteAllCompanyUserAccessByDividionId( userId,  departmemtId,  userRoleId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int DeleteAllCompanyUserAccessByRoleIdAndDividionId(int userId, int departmemtId, int userRoleId, int sysDivisionId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyUserAccessDAO companyUserAccessDAO = DAOFactory.CreateCompanyUserAccessDAO();
                return companyUserAccessDAO.DeleteAllCompanyUserAccessByRoleIdAndDividionId( userId,  departmemtId,  userRoleId,  sysDivisionId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int DeleteCompanyUserAccessByDividionId(int userId, int departmemtId)
        {
            throw new NotImplementedException();
        }

        public int DeleteCompanyUserAccessByUserId(int userId, int departmemtId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyUserAccessDAO companyUserAccessDAO = DAOFactory.CreateCompanyUserAccessDAO();
                return companyUserAccessDAO.DeleteCompanyUserAccessByUserId( userId,  departmemtId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<CompanyUserAccess> FetchCompanyUserAccessByDepartentId(int departmemtId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyUserAccessDAO companyUserAccessDAO = DAOFactory.CreateCompanyUserAccessDAO();
                return companyUserAccessDAO.FetchCompanyUserAccessByDepartentId( departmemtId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<CompanyUserAccess> FetchCompanyUserAccessByUserId(int userId, int departmemtId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyUserAccessDAO companyUserAccessDAO = DAOFactory.CreateCompanyUserAccessDAO();
                return companyUserAccessDAO.FetchCompanyUserAccessByUserId( userId,  departmemtId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<CompanyUserAccess> FetchCompanyUserAccessByUserIdAndUserRoleIdAndDivId(int userId, int departmemtId, int userRoleId, int sysDivId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyUserAccessDAO companyUserAccessDAO = DAOFactory.CreateCompanyUserAccessDAO();
                return companyUserAccessDAO.FetchCompanyUserAccessByUserIdAndUserRoleIdAndDivId( userId,  departmemtId,  userRoleId,  sysDivId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<CompanyUserAccess> GetCompanyUserAccessObjByUserId(int userId, int departmemtId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyUserAccessDAO companyUserAccessDAO = DAOFactory.CreateCompanyUserAccessDAO();
                return companyUserAccessDAO.GetCompanyUserAccessObjByUserId( userId,  departmemtId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public bool isAvilableAccess(int userId, int departmemtId, int systemDivisionId, int functionId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyUserAccessDAO companyUserAccessDAO = DAOFactory.CreateCompanyUserAccessDAO();
                return companyUserAccessDAO.isAvilableAccess( userId,  departmemtId,  systemDivisionId,  functionId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int SaveCompanyUserAccess(int userId, int departmemtId, int roleId, int? systemDivisionId, int? functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyUserAccessDAO companyUserAccessDAO = DAOFactory.CreateCompanyUserAccessDAO();
                return companyUserAccessDAO.SaveCompanyUserAccess( userId,  departmemtId,  roleId,  systemDivisionId, functionId,  CreatedDate,  CreatedBy,  UpdatedDate,  UpdatedBy,dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int UpdateCompanyUserAccess(int roleId, int oldFunctionId, int newFunctionId, DateTime UpdatedDate, string UpdatedBy)
        {
            throw new NotImplementedException();
        }

        public int SaveCompanyUserAccessvusename(string username, int departmemtId, int roleId, int? systemDivisionId, int? functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyUserAccessDAO companyUserAccessDAO = DAOFactory.CreateCompanyUserAccessDAO();
                return companyUserAccessDAO.SaveCompanyUserAccessvusename(username, departmemtId, roleId, systemDivisionId, functionId, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int DeleteAllCompanyUserAccessByRoleIdAndFunctionId(int userId, int departmemtId, int userRoleId, int sysDivisionId,int functionId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyUserAccessDAO companyUserAccessDAO = DAOFactory.CreateCompanyUserAccessDAO();
                return companyUserAccessDAO.DeleteAllCompanyUserAccessByRoleIdAndFunctionId(userId, departmemtId, userRoleId, sysDivisionId,functionId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public CompanyUserAccess FetchUserRoleIdByUserId(int userId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyUserAccessDAO companyUserAccessDAO = DAOFactory.CreateCompanyUserAccessDAO();
                return companyUserAccessDAO.FetchUserRoleIdByUserId(userId, companyId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }
    }
}
