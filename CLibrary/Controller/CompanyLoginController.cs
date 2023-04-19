using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Infrastructure;


namespace CLibrary.Controller
{
    public interface CompanyLoginController
    {
        int SaveCompanyLogin(int departmentid, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedby, int isActive, int subDepartmentID, List<UserSubDepartment> usersubdepartment, List<UserWarehouse> warehouse, string contactNo);
        int UpdateCompanyLogin(int userID, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime updatedDate, string updatedby, int isActive, int designationId, List<UserSubDepartment> usersubdepartment, List<UserWarehouse> warehouse, String contactNo);
        int SaveCompanyLogin(int departmentid, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedby, int isActive, int designationId);
        int UpdateCompanyLogin(int userID, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime updatedDate, string updatedby, int isActive, int designationId);
        CompanyLogin GetCompanyLogin(string username, string password);
        List<CompanyLogin> GetUserListByDepartmentid(int Departmentid);
        CompanyLogin GetUserbyuserId(int userId);
        List<CompanyLogin> GetAllUserList();

        List<CompanyLogin> GetAllUserListByDesignation(int Desgination);
        int UpdateInactiveUsers(int userID, int isActive);
        int ChangePassword(string UserName, string OldPassword, string NewPassword);
        List<CompanyLogin> GetStorekeepers(int warehouseId);
        CompanyLogin GetUserDetailsbyCatergoryId(int categoryId, string type);

        //Modified for GRN New
        List<string> GetUserEmailsForApprovalbyWarehouseId(int FunctionId, int CategoryId, decimal Sum, int CompanyId, int SysDivisionId, int SysActionId, int warehouseId);
        List<string> GetEmailsByUserId(int userId);
        List<string> GetWarehouseHeadsEmails(int WarehouseId);
        CompanyLogin GetUserbyPOId(int PoId);
        List<CompanyLogin> GetUserListByName(int Departmentid, string text);

    }
    public class CompanyLoginControllerImpl : CompanyLoginController
    {
        public List<string> GetWarehouseHeadsEmails(int WarehouseId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.GetWarehouseHeadsEmails(WarehouseId, dbConnection);
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
        public List<CompanyLogin> GetAllUserList()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.GetAllUserList(dbConnection);
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
        //Get User List By Designation
        public List<CompanyLogin> GetAllUserListByDesignation(int Desgination)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.GetAllUserListByDesignation(Desgination, dbConnection);
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

        public CompanyLogin GetCompanyLogin(string username, string password)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.GetCompanyLogin(username, password, dbConnection);
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

        public CompanyLogin GetUserbyPOId(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.GetUserbyPOId(PoId, dbConnection);
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

        public CompanyLogin GetUserbyuserId(int userId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                CompanyLogin companyLoginObj = companyLoginDAO.GetUserbyuserId(userId, dbConnection);
                companyLoginObj._CompanyDepartment = companyDepartmentDAO.GetDepartmentByDepartmentId(companyLoginObj.DepartmentId, dbConnection);
                return companyLoginObj;

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

        public List<CompanyLogin> GetUserListByDepartmentid(int Departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.GetUserListByDepartmentid(Departmentid, dbConnection);
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
        public List<CompanyLogin> GetUserListByName(int Departmentid, string text)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.GetUserListByName(Departmentid, text, dbConnection);
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

        public int SaveCompanyLogin(int departmentid, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedby, int isActive, int subDepartmentID, List<UserSubDepartment> usersubdepartment, List<UserWarehouse> warehouse, string contactNo)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.SaveCompanyLogin(departmentid, username, empNo, password, userType, firstname, emailAddress, createdDate, createdBy, updatedDate, updatedby, isActive, subDepartmentID, usersubdepartment, warehouse, contactNo, dbConnection);
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

        public int UpdateCompanyLogin(int userID, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime updatedDate, string updatedby, int isActive, int designationId, List<UserSubDepartment> usersubdepartment, List<UserWarehouse> warehouse, string contactNo)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.UpdateCompanyLogin(userID, username, empNo, password, userType, firstname, emailAddress, updatedDate, updatedby, isActive, designationId, usersubdepartment, warehouse, contactNo, dbConnection);
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

        public int SaveCompanyLogin(int departmentid, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedby, int isActive, int designationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.SaveCompanyLogin(departmentid, username, empNo, password, userType, firstname, emailAddress, createdDate, createdBy, updatedDate, updatedby, isActive, designationId, dbConnection);
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

        public int UpdateCompanyLogin(int userID, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime updatedDate, string updatedby, int isActive, int designationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.UpdateCompanyLogin(userID, username, empNo, password, userType, firstname, emailAddress, updatedDate, updatedby, isActive, designationId, dbConnection);
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


        public int UpdateInactiveUsers(int userID, int isActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.UpdateInactiveUsers(userID, isActive, dbConnection);
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

        public int ChangePassword(string UserName, string OldPassword, string NewPassword)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.ChangePassword(UserName, OldPassword, NewPassword, dbConnection);
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

        public List<CompanyLogin> GetStorekeepers(int warehouseId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.GetStorekeepers(warehouseId, dbConnection);
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

        public CompanyLogin GetUserDetailsbyCatergoryId(int categoryId, string type)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.GetUserDetailsbyCatergoryId(categoryId, type, dbConnection);
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

        public List<string> GetUserEmailsForApprovalbyWarehouseId(int FunctionId, int CategoryId, decimal Sum, int CompanyId, int SysDivisionId, int SysActionId, int warehouseId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.GetUserEmailsForApprovalbyWarehouseId(FunctionId, CategoryId, Sum, CompanyId, SysDivisionId, SysActionId, warehouseId, dbConnection);
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

        public List<string> GetEmailsByUserId(int userId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyLoginDAO companyLoginDAO = DAOFactory.createCompanyLoginDAO();
                return companyLoginDAO.GetEmailsByUserId(userId, dbConnection);
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
