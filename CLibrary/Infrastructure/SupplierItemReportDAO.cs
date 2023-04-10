using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Infrastructure
{
    public interface SupplierItemReportDAO
    {
        List<SupplierAddItemReport> GetAll(DBConnection dbConnection);
    }
    public class SupplierItemReportDAOImpl : SupplierItemReportDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<SupplierAddItemReport> GetAll(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT a.SUPPLIER_ID,a.SUPPLIER_NAME,d.ITEM_NAME,d.CREATED_DATETIME,d.CATEGORY_ID,d.SUB_CATEGORY_ID, b.GRN_CODE,b.IS_APPROVED FROM " + dbLibrary + ".SUPPLIER a " +
                "INNER JOIN GRN_MASTER b ON a.SUPPLIER_ID=b.SUPPLIER_ID " +
                "INNER JOIN GRN_DETAILS c ON b.GRN_ID=c.GRN_ID " +
                "INNER JOIN ADD_ITEMS d ON c.ITEM_ID=d.ITEM_ID ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierAddItemReport>(dbConnection.dr);
            }
        }
    }
}
