using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface IMeasurementMasterDAO
    {
        List<MeasurementMaster> GetMeasurementMasters(DBConnection dbConnection);
    }
    public class MeasurementMasterDAO : IMeasurementMasterDAO
    {
        public List<MeasurementMaster> GetMeasurementMasters(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "SELECT * FROM [MEASUREMENT_MASTER]";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MeasurementMaster>(dbConnection.dr);

            }
        }
    }
}
