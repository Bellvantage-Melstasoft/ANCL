using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface SubDepartmentControllerInterface
    {
        int saveDepartment(string departmentName, string phoneNo, int companyID,  int isActive, List<int> UserIds);
        int updateSubDepartment(int subDepartmentID, string departmentName, string phoneNo, int companyID, int isActive, List<int> UserIds);
        int deleteSubDepartment(int subDepartmentID);
        List<SubDepartment> getDepartmentList(int companyID);
        SubDepartment getDepartmentByID(int departmentID);
        int isUserHeadOfDepartment(int userID);
        int getsubDepartment(int companyID, int userID);
        List<SubDepartment> getAllDepartmentList(int companyID);
        int isUserHeadOfProcurement(int userId);
        List<SubDepartment> getUserSubDepartmentByUserId(int UserId);
    }
    public class SubDepartmentController : SubDepartmentControllerInterface
    {
        public int saveDepartment(string departmentName, string phoneNo, int companyID, int isActive, List<int> UserIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SubDepartmentDAOInterface subDepartmentDAO = DAOFactory.CreateSubDepartmentDAO();
                return subDepartmentDAO.saveSubDepartment(departmentName, phoneNo, companyID, isActive, UserIds, dbConnection);
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

        public int updateSubDepartment(int subDepartmentID, string departmentName, string phoneNo, int companyID, int isActive, List<int> UserIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SubDepartmentDAOInterface subDepartmentDAO = DAOFactory.CreateSubDepartmentDAO();
                return subDepartmentDAO.updateSubDepartment(subDepartmentID,departmentName, phoneNo, companyID, isActive, UserIds, dbConnection);
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

        public int deleteSubDepartment(int subDepartmentID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SubDepartmentDAOInterface subDepartmentDAO = DAOFactory.CreateSubDepartmentDAO();
                return subDepartmentDAO.deleteSubDepartment(subDepartmentID, dbConnection);
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

        public List<SubDepartment> getDepartmentList(int companyID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SubDepartmentDAOInterface subDepartmentDAO = DAOFactory.CreateSubDepartmentDAO();
                return subDepartmentDAO.getDepartmentList(companyID,dbConnection);
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

        public SubDepartment getDepartmentByID(int departmentID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SubDepartmentDAOInterface subDepartmentDAO = DAOFactory.CreateSubDepartmentDAO();
                return subDepartmentDAO.getDepartmentByID(departmentID,dbConnection);
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

        public int isUserHeadOfDepartment(int userID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SubDepartmentDAOInterface subDepartmentDAO = DAOFactory.CreateSubDepartmentDAO();
                return subDepartmentDAO.isUserHeadOfDepartment(userID, dbConnection);
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

        public int getsubDepartment(int companyID, int userID)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                SubDepartmentDAOInterface subDepartmentDAO = DAOFactory.CreateSubDepartmentDAO();
                return subDepartmentDAO.getsubDepartment(companyID,userID, dbConnection);
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

        public List<SubDepartment> getAllDepartmentList(int companyID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SubDepartmentDAOInterface subDepartmentDAO = DAOFactory.CreateSubDepartmentDAO();
                return subDepartmentDAO.getAllDepartmentList(companyID, dbConnection);
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

        public int isUserHeadOfProcurement(int userId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SubDepartmentDAOInterface subDepartmentDAO = DAOFactory.CreateSubDepartmentDAO();
                return subDepartmentDAO.isUserHeadOfProcurement(userId, dbConnection);
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

        public List<SubDepartment> getUserSubDepartmentByUserId(int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SubDepartmentDAOInterface subDepartmentDAO = DAOFactory.CreateSubDepartmentDAO();
                return subDepartmentDAO.getUserSubDepartmentByUserId(UserId, dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }
    }


}
