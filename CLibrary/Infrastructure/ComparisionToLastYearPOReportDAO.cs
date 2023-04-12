using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Infrastructure
{
    public interface ComparisionToLastYearPOReportDAO
    {
        List<ComparisionToLastYearPOReport> GetComparisionToLastYearPOReports(DBConnection dbConnection);

        DataTable GetComparisionToLastYearSupplierReports(DBConnection dbConnection);
    }
    public class ComparisionToLastYearPOReportDAOSqlImpl : ComparisionToLastYearPOReportDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<ComparisionToLastYearPOReport> GetComparisionToLastYearPOReports(DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + " .PO_MASTER a " +
                "INNER JOIN PR_MASTER b ON a.BASED_PR=b.PR_ID " +
                "INNER JOIN PO_DETAILS c ON c.PO_ID=a.PO_ID " +
                "INNER JOIN MRN_MASTER d ON d.MRN_ID=b.MRN_ID ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ComparisionToLastYearPOReport>(dbConnection.dr);
            }

        }
        public DataTable GetComparisionToLastYearSupplierReports(DBConnection dbConnection)
        {
            DataTable dtSupplierReports = new DataTable();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "Select YEAR(po.CREATED_DATE) AS PURCHASED_YEAR, " +
                "s.SUPPLIER_NAME,po.DEPARTMENT_ID,po.BASED_PR, pr.EXPENSE_TYPE, pd.PO_PURCHASE_TYPE, " +
                "SUM(pd.QUANTITY) AS QUANTITY, SUM(po.TOTAL_AMOUNT) AS AMOUNT " +
                "from SUPPLIER s INNER JOIN PO_MASTER po ON s.SUPPLIER_ID = po.SUPPLIER_ID " +
                "INNER JOIN PO_DETAILS pd ON pd.PO_ID = po.PO_ID INNER JOIN PR_MASTER pr on po.BASED_PR = pr.PR_ID " +
                "GROUP BY YEAR(po.CREATED_DATE), s.SUPPLIER_NAME,po.DEPARTMENT_ID,po.BASED_PR, pd.PO_PURCHASE_TYPE," +
                "pr.EXPENSE_TYPE;";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbConnection.cmd);
            dataAdapter.Fill(dtSupplierReports);

            return dtSupplierReports;
        }
    }


}
