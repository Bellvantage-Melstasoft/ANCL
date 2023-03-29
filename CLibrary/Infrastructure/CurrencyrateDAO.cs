using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {

    public interface CurrencyRateDAO {
        CurrencyRate FetchCurrencyRatesByMaxDate(int CurrencyTypeId, DBConnection dbConnection);
        int SaveCurrencyRates(int CurrencyType, DateTime Date, decimal Buyingrate, decimal Sellingrate, DBConnection DbConnection);
        int UpdateRates(List<CurrencyRate> CurrencyRateList, DBConnection DbConnection);
        decimal GetRateByID(int CurrencyId, DBConnection DbConnection);
        List<CurrencyRate> fetchCurrencyDetails(DBConnection dbConnection);
        List<CurrencyRate> fetchCountry(DBConnection dbConnection);
        }

        public class CurrencyRateDAOSQLImpl : CurrencyRateDAO {

        public CurrencyRate FetchCurrencyRatesByMaxDate(int CurrencyTypeId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT TOP(1)* FROM CURRENCY_RATE WHERE DATE <= '"+LocalTime.Now+"' AND CURRENCY_TYPE_ID = "+ CurrencyTypeId + " ORDER BY DATE DESC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CurrencyRate>(dbConnection.dr);
            }
        }

        public int SaveCurrencyRates(int CurrencyType, DateTime Date, decimal Buyingrate, decimal Sellingrate, DBConnection DbConnection) {
            DbConnection.cmd.Parameters.Clear();

            DbConnection.cmd.CommandText = "SELECT COUNT(*) FROM CURRENCY_RATE WHERE CURRENCY_TYPE_ID = " + CurrencyType + " ";
            int Count = int.Parse(DbConnection.cmd.ExecuteScalar().ToString());

            if (Count > 0) {
                DbConnection.cmd.CommandText = "UPDATE CURRENCY_RATE SET BUYING_RATE = "+ Buyingrate + ", SELLING_RATE = "+ Sellingrate + ", DATE = '"+ Date + "' WHERE CURRENCY_TYPE_ID = "+ CurrencyType + " ";
            }
            else {
                DbConnection.cmd.CommandText = "INSERT INTO CURRENCY_RATE VALUES (" + CurrencyType + ", '" + Date + "', " + Buyingrate + ", " + Sellingrate + ")";
            }
            DbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return DbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateRates(List<CurrencyRate> CurrencyRateList, DBConnection DbConnection) {
            DbConnection.cmd.Parameters.Clear();

            for (int i = 0; i < CurrencyRateList.Count; i++) {
                DbConnection.cmd.CommandText += "UPDATE CURRENCY_RATE SET DATE = '"+LocalTime.Now+"', SELLING_RATE = "+ CurrencyRateList[i].SellingRate +" WHERE CURRENCY_TYPE_ID = "+ CurrencyRateList[i].CurrencyTypeId+ " ";

            }

            DbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return DbConnection.cmd.ExecuteNonQuery();
        }

        public decimal GetRateByID(int CurrencyId, DBConnection DbConnection) {
            DbConnection.cmd.Parameters.Clear();
            decimal Rate = 0;
            if (CurrencyId > 0) {
                DbConnection.cmd.CommandText = "SELECT SELLING_RATE FROM  CURRENCY_RATE WHERE CURRENCY_TYPE_ID = " + CurrencyId + " ";
                DbConnection.cmd.CommandType = System.Data.CommandType.Text;

                 Rate = decimal.Parse(DbConnection.cmd.ExecuteScalar().ToString());

            }
            

            return Rate;
        }
        public List<CurrencyRate> fetchCurrencyDetails(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM  DEF_CURRENCY_TYPE AS DCT " +
                                            "LEFT JOIN CURRENCY_RATE AS CR ON DCT.CURRENCY_TYPE_ID = CR.CURRENCY_TYPE_ID "; ;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CurrencyRate>(dbConnection.dr);
            }
        }

        public List<CurrencyRate> fetchCountry(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM  COUNTRY AS CT ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CurrencyRate>(dbConnection.dr);
            }
        }
    }
}
