using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Controller
{
    public interface ComparisionToLastYearPOReportController
    {
        List<ComparisionToLastYearPOReport> GetComparisionToLastYearPOReports();
    }
    public class ComparisionToLastYearPOReportControllerImpl : ComparisionToLastYearPOReportController
    {
        public List<ComparisionToLastYearPOReport> GetComparisionToLastYearPOReports()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ComparisionToLastYearPOReportDAO comparisionToLastYearPOReportDAO = DAOFactory.CreateComparisionToLastYearPOReportDAO();
                return comparisionToLastYearPOReportDAO.GetComparisionToLastYearPOReports(dbConnection);
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
