using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface IItemMeasurementDAO
    {
        int AddItemMeasurement(List<ItemMeasurement> Measurements, int ItemId, int CompanyId, DBConnection dbConnection);
        List<ItemMeasurement> GetItemMeasurements(int ItemId, int CompanyId, DBConnection dbConnection);
    }

    public class ItemMeasurementDAO : IItemMeasurementDAO
    {
        public int AddItemMeasurement(List<ItemMeasurement> Measurements, int ItemId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "";
            for (int i = 0; i < Measurements.Count; i++)
            {
                dbConnection.cmd.CommandText += $@"INSERT INTO ITEM_MEASUREMENT(ITEM_ID,MEASUREMENT_DETAIL_ID,COMPANY_ID,IS_BASE,IS_ACTIVE)
                                                    VALUES({ItemId},{Measurements[i].MeasurementDetailId},{CompanyId},{Measurements[i].IsBase},{Measurements[i].IsActive}); 
                                                    ";
            }

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemMeasurement> GetItemMeasurements(int ItemId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = $@"SELECT IM.*,MD.MEASUREMENT_NAME,MD.SHORT_CODE,MD.MASTER_ID FROM ITEM_MEASUREMENT AS IM
                                                INNER JOIN MEASUREMENT_DETAIL AS MD ON IM.MEASUREMENT_DETAIL_ID = MD.DETAIL_ID
                                                WHERE IM.COMPANY_ID = {CompanyId} AND IM.ITEM_ID ={ItemId}";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemMeasurement>(dbConnection.dr);

            }
        }
    }
}
