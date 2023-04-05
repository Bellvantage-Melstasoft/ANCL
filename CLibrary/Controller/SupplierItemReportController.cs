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
    public interface SupplierItemReportController
    {
        List<SupplierAddItemReport> GetSuppliers();
    }

    public class SupplierItemReportControllerImpl : SupplierItemReportController
    {
        public List<SupplierAddItemReport> GetSuppliers()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierItemReportDAO supplierItemReportDAO = DAOFactory.CreatesupplierItemReportDAO();
                return supplierItemReportDAO.GetAll(dbConnection);
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
