using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
   public interface Procument_Plan_Type_DAO
    {
       List<Procument_PlanType> FetchAllProcument_Plan_Type(DBConnection dbConnection);
        Procument_PlanType FetchAllProcumentPlanTypeByPlanId(int planId, DBConnection dbConnection);
    }

   public class Procument_Plan_Type_DAOImpl : Procument_Plan_Type_DAO
   {

       string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

       public List<Procument_PlanType> FetchAllProcument_Plan_Type(DBConnection dbConnection)
       {
           dbConnection.cmd.Parameters.Clear();

           dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PROCUMENT_PLAN_TYPE WHERE IS_ACTIVE=1";
           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

           using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
           {
               DataAccessObject dataAccessObject = new DataAccessObject();
               return dataAccessObject.ReadCollection<Procument_PlanType>(dbConnection.dr);
           }
       }

        public Procument_PlanType FetchAllProcumentPlanTypeByPlanId(int planId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PROCUMENT_PLAN_TYPE WHERE IS_ACTIVE=1 AND PLAN_ID= "+ planId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Procument_PlanType>(dbConnection.dr);
            }
        }
    }
}
