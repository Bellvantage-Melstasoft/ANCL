using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public interface SupplierLoginController
    {
        int saveSupplierLogin(int supplierId ,string userName, string password, string email, int isApproved, int isActive);
        int updateSupplierLogin(int supplierid, string userName, string password, string email, int isApproved, int isActive);
        SupplierLogin SupplierLogin(string username, string password);
        int resetPassword(string emailAddress, string password);
        SupplierLogin SupplierLoginByEmailAddress(string emailAddress);
       
    }

    public class SupplierLoginControllerImpl : SupplierLoginController
    {
       

        public int resetPassword(string emailAddress, string password)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierLoginDAO SupplierLoginDAO = DAOFactory.createSupplierLoginDAO();
                return SupplierLoginDAO.resetPassword( emailAddress,  password, dbConnection);
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

        public int saveSupplierLogin(int supplierId ,string userName, string password, string email, int isApproved, int isActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierLoginDAO SupplierLoginDAO = DAOFactory.createSupplierLoginDAO();
                return SupplierLoginDAO.saveSupplierLogin(supplierId, userName,  password,  email,  isApproved,  isActive, dbConnection);
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

        public SupplierLogin SupplierLogin(string username, string password)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierLoginDAO SupplierLoginDAO = DAOFactory.createSupplierLoginDAO();
                return SupplierLoginDAO.SupplierLogin(username, password, dbConnection);
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

        public SupplierLogin SupplierLoginByEmailAddress(string emailAddress)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierLoginDAO SupplierLoginDAO = DAOFactory.createSupplierLoginDAO();
                return SupplierLoginDAO.SupplierLoginByEmailAddress( emailAddress, dbConnection);
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

        public int updateSupplierLogin(int supplierid, string userName, string password, string email, int isApproved, int isActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierLoginDAO SupplierLoginDAO = DAOFactory.createSupplierLoginDAO();
                return SupplierLoginDAO.updateSupplierLogin( supplierid,  userName,  password,  email,  isApproved,  isActive,dbConnection);
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
