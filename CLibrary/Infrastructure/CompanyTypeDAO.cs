using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface CompanyTypeDAO {
        List<CompanyType> FetchCompanyTypeList(DBConnection dbConnection);
    }

    public class CompanyTypeDAOSQLImpl : CompanyTypeDAO {

        public List<CompanyType> FetchCompanyTypeList(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM COMPANY_TYPE";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyType>(dbConnection.dr);
            }
        }

    }
}
