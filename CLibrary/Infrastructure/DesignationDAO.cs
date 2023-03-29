using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
   public interface DesignationDAO
    {
       int SaveDesignation(string designationName, string enteredUser, DateTime EnteredDate, DBConnection dbConnection);
       List<Designation> GetDesignationList(DBConnection dbConnection);

       int UpdateDesignation(int designationId, string designationName, string enteredUser, DateTime EnteredDate, DBConnection dbConnection);
    }
   public class DesignationDAOImpl : DesignationDAO
   {
       string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

       public int SaveDesignation( string designationName, string enteredUser, DateTime EnteredDate, DBConnection dbConnection)
       {
           int designationId = 0;
           dbConnection.cmd.Parameters.Clear();
           dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".DESIGNATION WHERE DESIGNATION_NAME = '" + designationName + "'";
           var countExistName = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

           if (countExistName == 0)
           {
               dbConnection.cmd.Parameters.Clear();
               dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".DESIGNATION";
               var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
               if (count == 0)
               {
                   designationId = 1;
               }
               else
               {
                   dbConnection.cmd.CommandText = "SELECT MAX (DESIGNATION_ID) + 1 AS MAXid FROM " + dbLibrary + ".DESIGNATION";
                   designationId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
               }
               dbConnection.cmd.Parameters.Clear();
               dbConnection.cmd.CommandText = "INSERT INTO  " + dbLibrary + ".DESIGNATION (DESIGNATION_ID , DESIGNATION_NAME , ENTERED_USER , ENTERED_DATE ) VALUES " +
                   "(" + designationId + ",'" + designationName + "','" + enteredUser + "','" + EnteredDate + "')";
               dbConnection.cmd.CommandType = System.Data.CommandType.Text;
               dbConnection.cmd.ExecuteNonQuery();
               return designationId;
           }
           else
           {
               return -1;
           }
       }

       public List<Designation> GetDesignationList(DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();
           dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".DESIGNATION ORDER BY DESIGNATION_NAME";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

           using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
           {
               DataAccessObject dataAccessObject = new DataAccessObject();
               return dataAccessObject.ReadCollection<Designation>(dbConnection.dr);
           }
       }

       public int  UpdateDesignation(int designationId, string designationName, string enteredUser, DateTime EnteredDate, DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();
           dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".DESIGNATION SET DESIGNATION_NAME = '" + designationName + "' , ENTERED_USER = '" + enteredUser + "' , ENTERED_DATE = '" + EnteredDate + "'   WHERE DESIGNATION_ID = " + designationId + " ";
           return dbConnection.cmd.ExecuteNonQuery();
       }
   }
}
