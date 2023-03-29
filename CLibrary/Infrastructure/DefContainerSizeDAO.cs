using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface DefContainerSizeDAO {

        List<DefContainerSize> FetchDefContainerSizeList(DBConnection dbConnection);
    }

    public class DefContainerSizeDAOSQLImpl : DefContainerSizeDAO {

        public List<DefContainerSize> FetchDefContainerSizeList(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM DEF_CONTAINER_SIZE";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<DefContainerSize>(dbConnection.dr);
            }
        }
    }
    }
