using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {

    public interface DefTransportModeDAO {
        List<DefTransportMode> FetchDefTransportModeList(DBConnection dbConnection);
    }

    public class DefTransportModeDAOSQLImpl : DefTransportModeDAO {
        public List<DefTransportMode> FetchDefTransportModeList(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM DEF_TRANSPORT_MODE";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<DefTransportMode>(dbConnection.dr);
            }
        }
    }
}
