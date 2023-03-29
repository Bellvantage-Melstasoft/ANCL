using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CLibrary.Controller
{
    public interface DesignationController
    {
        int SaveDesignation( string designationName, string enteredUser, DateTime EnteredDate);
        List<Designation> GetDesignationList();
        int UpdateDesignation(int designationId,string designationName, string enteredUser, DateTime EnteredDate);

    }
    public class DesignationControllerImpl : DesignationController
    {
        public int SaveDesignation( string designationName, string enteredUser, DateTime EnteredDate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                DesignationDAO designationDAO = DAOFactory.CreateDesignationDAO();
                return designationDAO.SaveDesignation(designationName, enteredUser,EnteredDate, dbConnection);
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
        public int UpdateDesignation(int designationId,string designationName, string enteredUser, DateTime EnteredDate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                DesignationDAO designationDAO = DAOFactory.CreateDesignationDAO();
                return designationDAO.UpdateDesignation(designationId,designationName, enteredUser, EnteredDate, dbConnection);
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
        public List<Designation> GetDesignationList()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                DesignationDAO designationDAO = DAOFactory.CreateDesignationDAO();
                return designationDAO.GetDesignationList(dbConnection);
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
