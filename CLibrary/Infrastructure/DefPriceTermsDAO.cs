using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;


namespace CLibrary.Infrastructure {
    public interface DefPriceTermsDAO {
        List<DefPriceTerms> FetchDefPriceTermsList(DBConnection dbConnection);
    }

    public class DefPriceTermsDAOSQLImpl : DefPriceTermsDAO {
        public List<DefPriceTerms> FetchDefPriceTermsList(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM DEF_PRICE_TERMS";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<DefPriceTerms>(dbConnection.dr);
            }
        }
    }
}
