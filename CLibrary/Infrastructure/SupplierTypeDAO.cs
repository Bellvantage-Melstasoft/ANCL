using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface SupplierTypeDAO {
        List<SupplierType> FetchSupplierTypeList(DBConnection dbConnection);
    }

    public class SupplierTypeDAOSQLImpl : SupplierTypeDAO {

        public List<SupplierType> FetchSupplierTypeList(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_TYPE";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierType>(dbConnection.dr);
            }
        }
    }
    }
