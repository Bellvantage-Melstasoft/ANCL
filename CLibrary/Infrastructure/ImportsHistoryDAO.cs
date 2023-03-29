using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {

    public interface ImportsHistoryDAO {
        List<ImportsHistory> GetHistoryForQuotationSubmission(DBConnection dbConnection);
    }

    public class ImportsHistoryDAOSQLImpl : ImportsHistoryDAO {

        public List<ImportsHistory> GetHistoryForQuotationSubmission(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM IMPORTS_HISTORY";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ImportsHistory>(dbConnection.dr);
            }
        }

    }
}
