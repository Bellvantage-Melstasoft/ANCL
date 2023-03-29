using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller {


    public interface UserSubDepartmentController {
        List<UserSubDepartment> getUserSubDepartmentdetails(int UserId);
        List<UserSubDepartment> getDepartmentHeads(int subDepartmentId);
        List<UserSubDepartment> DepartmentDetails(int subDepartmentId);
        List<SubDepartment> getDepartmentListByDepartmentIds(List<int> DepartmentIds);
        List<SubDepartment> getDepartmentList(int CompanyId);
    }

        public class UserSubDepartmentControllerImpl : UserSubDepartmentController {
            public List<UserSubDepartment> getUserSubDepartmentdetails(int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                UserSubDepartmentDAO userSubDepartmentDAO = DAOFactory.createUserSubDepartmentDAO();
                return userSubDepartmentDAO.getUserSubDepartmentdetails(UserId, dbConnection);
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

        public List<UserSubDepartment> DepartmentDetails(int subDepartmentId) {
            DBConnection dbConnection = new DBConnection();
            try {
                UserSubDepartmentDAO userSubDepartmentDAO = DAOFactory.createUserSubDepartmentDAO();
                return userSubDepartmentDAO.DepartmentDetails(subDepartmentId, dbConnection);
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


        public List<UserSubDepartment> getDepartmentHeads(int subDepartmentId) {
            DBConnection dbConnection = new DBConnection();
            try {
                UserSubDepartmentDAO userSubDepartmentDAO = DAOFactory.createUserSubDepartmentDAO();
                return userSubDepartmentDAO.getDepartmentHeads(subDepartmentId, dbConnection);
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

        public List<SubDepartment> getDepartmentListByDepartmentIds(List<int> DepartmentIds) {
            DBConnection dbConnection = new DBConnection();
            try {
                SubDepartmentDAOInterface subDepartmentDAO = DAOFactory.CreateSubDepartmentDAO();
                return subDepartmentDAO.getDepartmentListByDepartmentIds(DepartmentIds, dbConnection);
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
        public List<SubDepartment> getDepartmentList(int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SubDepartmentDAOInterface subDepartmentDAO = DAOFactory.CreateSubDepartmentDAO();
                return subDepartmentDAO.getDepartmentList(CompanyId, dbConnection);
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

