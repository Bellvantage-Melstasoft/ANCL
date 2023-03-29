using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface DefCurrencyTypeDAO {
        List<DefCurrencyType> FetchDefCurrencyTypeList(DBConnection dbConnection);
        int SaveCurrencyType(string CurrencyName, string CurrencyShortName, DBConnection DbConnection);
    }

    public class DefCurrencyTypeDAOSQLImpl : DefCurrencyTypeDAO {

        public List<DefCurrencyType> FetchDefCurrencyTypeList(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM DEF_CURRENCY_TYPE";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<DefCurrencyType>(dbConnection.dr);
            }
        }

        public int SaveCurrencyType(string CurrencyName, string CurrencyShortName, DBConnection DbConnection) {
            DbConnection.cmd.Parameters.Clear();
            DbConnection.cmd.CommandText = "INSERT INTO DEF_CURRENCY_TYPE VALUES ('"+ CurrencyName + "', '"+ CurrencyShortName + "')";

            DbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return DbConnection.cmd.ExecuteNonQuery();
        }
    }
    }
