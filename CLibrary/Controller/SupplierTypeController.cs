using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface SupplierTypeController {
        List<SupplierType> FetchSupplierTypeList();
    }

    public class SupplierTypeControllerImpl : SupplierTypeController {

        public List<SupplierType> FetchSupplierTypeList() {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierTypeDAO supplierTypeDAO = DAOFactory.CreateSupplierTypeDAO();
                return supplierTypeDAO.FetchSupplierTypeList(dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
    }
}
