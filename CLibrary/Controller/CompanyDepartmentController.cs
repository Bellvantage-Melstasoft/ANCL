using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Controller
{
   public interface CompanyDepartmentController
    {
        int saveDepartment(string departmentName, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedBy, int isActive, string imagePath, string address1, string address, string city, string country, string phoneNo, string mobileNo, string faxNo, string vatNo);
        int updateDepartment(int departmentId, string departmentName, DateTime updatedDate, string updatedBy, int isActive, string imagePath, string address1, string address, string city, string country, string phoneNo, string mobileNo, string faxNo, string vatNo, string termsPath);
        List<CompanyDepartment> GetDepartmentList();
        CompanyDepartment GetDepartmentByDepartmentId(int departmentId);
        CompanyDepartment GetDepartmentLogin(string username, string password);
        int updateDepartmentLogo(int departmentId, string logo);
        int deleteCompany(int departmentId);
        int updateDepartmentTermsConditions(int departmentId, string termsPath);
        List<CompanyDepartment> AssignSupplierWithCompany(int supplier);



    }

    public class CompanyDepartmentControllerImpl : CompanyDepartmentController
    {
        public List<CompanyDepartment> AssignSupplierWithCompany(int supplier)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                return companyDepartmentDAO.AssignSupplierWithCompany( supplier, dbConnection);
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

        public int deleteCompany(int departmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                return companyDepartmentDAO.deleteCompany(departmentId,dbConnection);
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

        public CompanyDepartment GetDepartmentByDepartmentId(int departmentId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                return companyDepartmentDAO.GetDepartmentByDepartmentId(departmentId, dbConnection);
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
   

        public List<CompanyDepartment> GetDepartmentList()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                return companyDepartmentDAO.GetDepartmentList( dbConnection);
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

        public CompanyDepartment GetDepartmentLogin(string username, string password)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                return companyDepartmentDAO.GetDepartmentLogin(username, password, dbConnection);
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

        public int saveDepartment(string departmentName, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedBy, int isActive, string imagePath, string address1, string address, string city, string country, string phoneNo, string mobileNo, string faxNo, string vatNo)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                return companyDepartmentDAO.saveDepartment( departmentName,  createdDate,  createdBy,  updatedDate,  updatedBy,  isActive, imagePath,  address1,  address,  city,  country,  phoneNo,  mobileNo,  faxNo,  vatNo, dbConnection);
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

        public int updateDepartment(int departmentId, string departmentName, DateTime updatedDate, string updatedBy, int isActive, string imagePath, string address1, string address, string city, string country, string phoneNo, string mobileNo, string faxNo, string vatNo, string termsPath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                return companyDepartmentDAO.updateDepartment( departmentId,  departmentName,  updatedDate,  updatedBy,  isActive, imagePath,  address1,  address,  city,  country,  phoneNo,  mobileNo,  faxNo,  vatNo,  termsPath, dbConnection);
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

        public int updateDepartmentLogo(int departmentId, string logo)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                return companyDepartmentDAO.updateDepartmentLogo(departmentId, logo, dbConnection);
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

        public int updateDepartmentTermsConditions(int departmentId, string termsPath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                return companyDepartmentDAO.updateDepartmentTermsConditions( departmentId,  termsPath, dbConnection);
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
