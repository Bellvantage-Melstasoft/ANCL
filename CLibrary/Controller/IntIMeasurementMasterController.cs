using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface IMeasurementMasterController
    {
        List<MeasurementMaster> GetMeasurementMasters();
    }
    public class MeasurementMasterController : IMeasurementMasterController
    {
        public List<MeasurementMaster> GetMeasurementMasters()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IMeasurementMasterDAO DAO = DAOFactory.CreateMeasurementMasterDAO();
                return DAO.GetMeasurementMasters(dbConnection);
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
    }
}
