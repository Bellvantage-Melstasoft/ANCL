using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Infrastructure
{
    public interface ComparisionToLastYearPOReportDAO
    {
        List<ComparisionToLastYearPOReport> GetComparisionToLastYearPOReports(DBConnection dbConnection);
    }
    public class ComparisionToLastYearPOReportDAOSqlImpl : ComparisionToLastYearPOReportDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<ComparisionToLastYearPOReport> GetComparisionToLastYearPOReports(DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + " .PO_MASTER a " +
                "INNER JOIN PR_MASTER b ON a.BASED_PR=b.PR_ID " +
                "INNER JOIN PO_DETAILS c ON c.PO_ID=a.PO_ID ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ComparisionToLastYearPOReport>(dbConnection.dr);
            }

        }
    }
}
