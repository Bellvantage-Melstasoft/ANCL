using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface DashboardController
    {
        List<int> GetCountForDashboard(int CompanyId, int yearsearch,int DesignationId);
        List<MrnMaster> fetchMrnList(int subDepartmentID);
        List<PR_Master> FetchApprovePRDataByDeptId(int DepartmentId);
        List<PR_Master> GetPrListForBidSubmission(int CompanyId);
        List<PR_Master> GetPrListForBidApproval(int CompanyId);
        List<POMaster> GetPoMasterListByDepartmentId(int Departmentid);
    }

    public class DashboardControllerImpl : DashboardController
    {
        
        public List<int> GetCountForDashboard(int CompanyId, int yearsearch,int DesignationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                DashboardDAO dashboardDAO = DAOFactory.CreateDashboardDAO();
                return dashboardDAO.GetCountForDashboard(CompanyId, yearsearch, DesignationId, dbConnection);
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

        public List<MrnMaster> fetchMrnList(int subDepartmentID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                DashboardDAO dashboardDAO = DAOFactory.CreateDashboardDAO();
                return dashboardDAO.fetchMrnList(subDepartmentID, dbConnection);

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

        public List<PR_Master> FetchApprovePRDataByDeptId(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                DashboardDAO dashboardDAO = DAOFactory.CreateDashboardDAO();
                return dashboardDAO.FetchApprovePRDataByDeptId(DepartmentId, dbConnection);
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

        public List<PR_Master> GetPrListForBidSubmission(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                DashboardDAO dashboardDAO = DAOFactory.CreateDashboardDAO();
                return dashboardDAO.GetPrListForBidSubmission(CompanyId, dbConnection);
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

        public List<PR_Master> GetPrListForBidApproval(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                DashboardDAO dashboardDAO = DAOFactory.CreateDashboardDAO();
                return dashboardDAO.GetPrListForBidApproval(CompanyId, dbConnection);
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

        public List<POMaster> GetPoMasterListByDepartmentId(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                DashboardDAO dashboardDAO = DAOFactory.CreateDashboardDAO();
                return dashboardDAO.GetPoMasterListByDepartmentId(departmentid, dbConnection);
            }
            catch (Exception ex)
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
