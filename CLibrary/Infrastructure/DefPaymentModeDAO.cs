using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface DefPaymentModeDAO {
        List<DefPaymentMode> FetchDefPaymentModeList(DBConnection dbConnection);
    }

    public class DefPaymentModeDAOSQLImpl : DefPaymentModeDAO {

        public List<DefPaymentMode> FetchDefPaymentModeList(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM DEF_PAYMENT_MODE";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<DefPaymentMode>(dbConnection.dr);
            }
        }
    }
}
