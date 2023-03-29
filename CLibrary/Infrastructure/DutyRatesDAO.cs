using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface DutyRatesDAO {
        int SaveCurrencyRates(string HSCode, decimal XID, decimal CID, decimal PAL, decimal EIC, string HsCodeName, DBConnection DbConnection);
        DutyRates GetRatesByHSCode(string HsCode, DBConnection dbConnection);
        List<DutyRates> GetRates(DBConnection dbConnection);
        int SaveCountry(string Country, DBConnection dbConnection);
        int HsCOdeAvailability(string HsId, DBConnection dbConnection);
        List<DutyRates> GetRatesByHsCodesList(List<string> HsCodes, DBConnection dbConnection);
        int InsertDutyRatesHistory(string HsId, string HsIdName, decimal EIC, decimal CID, decimal PAL, decimal XID, DBConnection dbConnection);
    }
    public class DutyRatesDAOImpl : DutyRatesDAO {
        public int SaveCurrencyRates(string HSCode, decimal XID, decimal CID, decimal PAL, decimal EIC, string HsCodeName, DBConnection DbConnection) {
            DbConnection.cmd.Parameters.Clear();
            DbConnection.cmd.CommandText = "SELECT COUNT(*) FROM DUTY_RATES WHERE HS_ID = '"+ HSCode + "' ";
            int count = int.Parse(DbConnection.cmd.ExecuteScalar().ToString());

            if (count > 0) {

                DbConnection.cmd.CommandText = "UPDATE DUTY_RATES SET DATE = '" + LocalTime.Now + "', XID = " + XID + ", CID = " + CID + ", PAL = " + PAL + ", EIC = " + EIC + " WHERE HS_ID = '" + HSCode + "' ";
               
            }
            else {
                DbConnection.cmd.CommandText = "INSERT INTO DUTY_RATES VALUES ('" + HSCode + "', '" + LocalTime.Now + "', " + XID + ", " + CID + ", " + PAL + ", " + EIC + ", '"+ HsCodeName + "')";
            }
            DbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return DbConnection.cmd.ExecuteNonQuery();
        }

        public DutyRates GetRatesByHSCode(string HsCode, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM DUTY_RATES WHERE HS_ID = '"+ HsCode + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<DutyRates>(dbConnection.dr);
            }
        }
        public int SaveCountry(string Country, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "INSERT INTO COUNTRY VALUES((SELECT MAX(ID) FROM COUNTRY )+1,'" + Country + "') ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();

        }

        public int InsertDutyRatesHistory(string HsId, string HsIdName, decimal EIC, decimal CID, decimal PAL, decimal XID, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "INSERT INTO DUTY_RATES_HISTORY VALUES('"+ HsId + "', '" + LocalTime.Now + "', "+ XID + ", " + CID + ", " + PAL + ","+EIC+", '" + HsIdName + "') ";
            dbConnection.cmd.CommandText += "DELETE FROM DUTY_RATES WHERE HS_ID = '"+ HsId + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();

        }

        public int HsCOdeAvailability(string HsId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(HS_ID) FROM DUTY_RATES WHERE HS_ID = '" + HsId+"' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

        }
        public List<DutyRates> GetRates(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM DUTY_RATES ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<DutyRates>(dbConnection.dr);
            }
        }
        public List<DutyRates> GetRatesByHsCodesList(List<string> HsCodes,DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            for (int i = 0; i < HsCodes.Count; i++) {
                HsCodes[i] = "'" + HsCodes[i] + "'";
            }
             dbConnection.cmd.CommandText += "SELECT * FROM DUTY_RATES WHERE HS_ID IN (" + string.Join(",", HsCodes) + ")  ";
           
            
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<DutyRates>(dbConnection.dr);
            }
        }

    }
}
