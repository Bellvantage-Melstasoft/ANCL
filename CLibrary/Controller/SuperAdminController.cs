using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public interface SuperAdminController
    {
        SuperAdmin GetSuperAdminLogin(string username, string password);
    }
    public class SuperAdminControllerImpl : SuperAdminController
    {
        public SuperAdmin GetSuperAdminLogin(string username, string password)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
              
                SuperAdminDAO superAdminDAO = DAOFactory.createSuperAdminDAO();
                return superAdminDAO.GetSuperAdminLogin(username, password, dbConnection);
              
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
