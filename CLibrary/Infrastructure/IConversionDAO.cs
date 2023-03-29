using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public  interface IConversionDAO
    {
        decimal DoConversion(int ItemId, int CompanyId, decimal Qty, int FromUOM, int ToUOM, DBConnection dbConnection);
        int AddConversion(List<Conversion> Conversions, int ItemId, int CompanyId, DBConnection dbConnection);
        List<Conversion> GetItemConversions(int ItemId, int CompanyId, DBConnection dbConnection);
        List<Conversion> GetCustomConversions(int ItemId, int CompanyId, DBConnection dbConnection);
    }

    public class ConversionDAO : IConversionDAO
    {
        public int AddConversion(List<Conversion> Conversions, int ItemId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "";
            for (int i = 0; i < Conversions.Count; i++)
            {
                dbConnection.cmd.CommandText += $@"INSERT INTO CONVERSION (FROM_ID,TO_ID,MULTIPLIER,ITEM_ID,COMPANY_ID,IS_ACTIVE)
                                                    VALUES
                                                    ({Conversions[i].FromId},{Conversions[i].ToId},{Conversions[i].Multiplier},{ItemId},{CompanyId},{Conversions[i].IsActive}); 
                                                    ";
            }

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public decimal DoConversion(int ItemId, int CompanyId, decimal Qty, int FromUOM, int ToUOM, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            dbConnection.cmd.CommandText = "DO_CONVERSION";

            dbConnection.cmd.Parameters.AddWithValue("@COUNT", Qty);
            dbConnection.cmd.Parameters.AddWithValue("@FROM_VAL_TYPE", FromUOM);
            dbConnection.cmd.Parameters.AddWithValue("@TO_VAL_TYPE", ToUOM);
            dbConnection.cmd.Parameters.AddWithValue("@ITEM_ID", ItemId);
            dbConnection.cmd.Parameters.AddWithValue("@COMPANY_ID", CompanyId);

            return decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public List<Conversion> GetItemConversions(int ItemId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = $"SELECT * FROM CONVERSION	WHERE COMPANY_ID = {CompanyId} AND ITEM_ID={ItemId}";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Conversion>(dbConnection.dr);

            }
        }

        public List<Conversion> GetCustomConversions(int ItemId, int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "SELECT * FROM CONVERSION AS C "+
                                            "INNER JOIN (SELECT MASTER_ID, DETAIL_ID FROM MEASUREMENT_DETAIL WHERE MASTER_ID = 5) AS D ON D.DETAIL_ID = C.FROM_ID "+
                                            "INNER JOIN (SELECT DETAIL_ID, MEASUREMENT_NAME AS FROM_NAME FROM MEASUREMENT_DETAIL ) AS FMD ON FMD.DETAIL_ID = C.FROM_ID "+
                                            "INNER JOIN (SELECT DETAIL_ID, MEASUREMENT_NAME AS TO_NAME FROM MEASUREMENT_DETAIL ) AS TMD ON TMD.DETAIL_ID = C.TO_ID " +
                                            "WHERE ITEM_ID = " + ItemId + " AND COMPANY_ID = "+ CompanyId + " ";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Conversion>(dbConnection.dr);

            }
        }
    }
}
