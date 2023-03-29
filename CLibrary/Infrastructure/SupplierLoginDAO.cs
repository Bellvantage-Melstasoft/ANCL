using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructue
{
   public  interface SupplierLoginDAO
    {
        int saveSupplierLogin(int supplierId , string userName, string password, string email, int isApproved, int isActive, DBConnection dbConnection);
        int updateSupplierLogin(int supplierid, string userName, string password, string email, int isApproved, int isActive, DBConnection dbConnection);
        SupplierLogin GetSupplierLoginBySupplierId(int supplierId, DBConnection dbConnection);
        SupplierLogin SupplierLogin(string username, string password, DBConnection dbConnection);
        int resetPassword(string emailAddress, string password, DBConnection dbConnection);
        SupplierLogin SupplierLoginByEmailAddress(string emailAddress,  DBConnection dbConnection);
     
    }

    public class SupplierLoginDAOSQLImpl : SupplierLoginDAO
    {
         string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

         public SupplierLogin GetSupplierLoginBySupplierId(int supplierId, DBConnection dbConnection)
         {
             dbConnection.cmd.Parameters.Clear();
             dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_LOGIN WHERE SUPPLIER_ID = " + supplierId + "";
             dbConnection.cmd.CommandType = System.Data.CommandType.Text;

             using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
             {
                 DataAccessObject dataAccessObject = new DataAccessObject();
                 return dataAccessObject.GetSingleOject<SupplierLogin>(dbConnection.dr);
             }
         }

         public int resetPassword(string emailAddress, string password, DBConnection dbConnection)
         {
             dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_LOGIN SET   PASSWORD  ='" + password + "'  WHERE  EMAIL_ADDRESS  ='" + emailAddress + "'  ";
             dbConnection.cmd.CommandType = System.Data.CommandType.Text;
             return dbConnection.cmd.ExecuteNonQuery();
         }

         public int saveSupplierLogin(int supplierId , string userName, string password, string email, int isApproved, int isActive, DBConnection dbConnection)
         {            
            int status = 0;
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_LOGIN WHERE  SUPPLIER_ID = " + supplierId + "";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (existCount == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_LOGIN (SUPPLIER_ID , USR_NAME , PASSWORD  , EMAIL_ADDRESS  , IS_APPROVED  , IS_ACTIVE )" +
                                               " VALUES (" + supplierId + ",'" + userName + "','" + password + "','" + email + "'," + isApproved + "," + isActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }else
            {
                return status;
            }

        }

        public SupplierLogin SupplierLogin(string username, string password, DBConnection dbConnection)
         {
             dbConnection.cmd.Parameters.Clear();
             dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_LOGIN WHERE (USR_NAME = '" + username + "' OR   EMAIL_ADDRESS = '" + username + "') AND PASSWORD = '" + password + "' ";
             dbConnection.cmd.CommandType = System.Data.CommandType.Text;

             using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
             {
                 DataAccessObject dataAccessObject = new DataAccessObject();
                 return dataAccessObject.GetSingleOject<SupplierLogin>(dbConnection.dr);
             }
         }

         public SupplierLogin SupplierLoginByEmailAddress(string emailAddress, DBConnection dbConnection)
         {
             dbConnection.cmd.Parameters.Clear();
             dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_LOGIN  AS sl INNER JOIN " + dbLibrary + ".SUPPLIER AS s ON s.SUPPLIER_ID = sl.SUPPLIER_ID  WHERE sl.EMAIL_ADDRESS = '" + emailAddress + "'";
             dbConnection.cmd.CommandType = System.Data.CommandType.Text;

             using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
             {
                 DataAccessObject dataAccessObject = new DataAccessObject();
                 return dataAccessObject.GetSingleOject<SupplierLogin>(dbConnection.dr);
             }
         }

         public int updateSupplierLogin(int supplierid, string userName, string password, string email, int isApproved, int isActive, DBConnection dbConnection)
         {
             dbConnection.cmd.Parameters.Clear();

             dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_LOGIN WHERE   SUPPLIER_ID !=" + supplierid + " AND (USR_NAME  ='" + userName + "' OR EMAIL_ADDRESS ='" + email + "')";
             var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

             if (existCount == 0)
             {

                 dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_LOGIN SET  USR_NAME  ='" + userName + "', PASSWORD  ='" + password + "' , EMAIL_ADDRESS  ='" + email + "' , IS_APPROVED  =" + isApproved + " , IS_ACTIVE  =" + isActive + " WHERE SUPPLIER_ID =" + supplierid + " ";
                 dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                 return dbConnection.cmd.ExecuteNonQuery();
             }
             else
             {
                 return -1;
             }
         }

   

       
    }

}
