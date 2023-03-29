using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface GrnFilesDAO
    {
        List<GrnFiles> GetGrnFilesByGrnId(int GrnId, DBConnection dbConnection);
    }

    public class GrnFilesDAOSQLImpl : GrnFilesDAO
    {
        public List<GrnFiles> GetGrnFilesByGrnId(int GrnId, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM GRN_FILES WHERE GRN_ID = " + GrnId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnFiles>(dbConnection.dr);
            }
        }

    }
}