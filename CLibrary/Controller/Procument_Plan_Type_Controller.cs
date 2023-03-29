using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface Procument_Plan_Type_Controller
    {
        List<Procument_PlanType> FetchAllProcument_Plan_Type();
        Procument_PlanType FetchAllProcumentPlanTypeByPlanId(int planId);
    }
    public class Procument_Plan_Type_ControllerImpl : Procument_Plan_Type_Controller
    { 
      public List<Procument_PlanType> FetchAllProcument_Plan_Type()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                Procument_Plan_Type_DAO procument_Plan_Type_DAO = DAOFactory.createProcument_Plan_Type_DAO();
                return procument_Plan_Type_DAO.FetchAllProcument_Plan_Type( dbConnection);
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

        public Procument_PlanType FetchAllProcumentPlanTypeByPlanId(int planId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                Procument_Plan_Type_DAO procument_Plan_Type_DAO = DAOFactory.createProcument_Plan_Type_DAO();
                return procument_Plan_Type_DAO.FetchAllProcumentPlanTypeByPlanId(planId,dbConnection);
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
