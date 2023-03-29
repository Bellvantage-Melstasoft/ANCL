using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
   public interface SuperAdminDAO
    {
        SuperAdmin GetSuperAdminLogin(string username, string password, DBConnection dbConnection);
    }
    public class SuperAdminDAOImpl : SuperAdminDAO
    {
        public SuperAdmin GetSuperAdminLogin(string username, string password, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"SUPER_ADMIN\"  WHERE \"USER_NAME\" = '" + username + "' AND \"PASSWORD\" = '" + password + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SuperAdmin>(dbConnection.dr);
            }
        }
    }

    public class SuperAdminDAOSQLImpl : SuperAdminDAO 
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public SuperAdmin GetSuperAdminLogin(string username, string password, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPER_ADMIN  WHERE USER_NAME = '" + username + "' AND PASSWORD = '" + password + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SuperAdmin>(dbConnection.dr);
            }
        }
    }
}
