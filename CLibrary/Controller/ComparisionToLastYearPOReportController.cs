using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Controller
{
    public interface ComparisionToLastYearPOReportController
    {
        List<ComparisionToLastYearPOReport> GetComparisionToLastYearPOReports();

        DataTable GetComparisionToLastYearSupplierReports();

        DataTable GetComparisionToLastYearItemReports();
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

        public DataTable GetComparisionToLastYearSupplierReports()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ComparisionToLastYearPOReportDAO comparisionToLastYearPOReportDAO = DAOFactory.CreateComparisionToLastYearPOReportDAO();
                return comparisionToLastYearPOReportDAO.GetComparisionToLastYearSupplierReports(dbConnection);
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

        public DataTable GetComparisionToLastYearItemReports()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ComparisionToLastYearPOReportDAO comparisionToLastYearPOReportDAO = DAOFactory.CreateComparisionToLastYearPOReportDAO();
                return comparisionToLastYearPOReportDAO.GetComparisionToLastYearItemReports(dbConnection);
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
