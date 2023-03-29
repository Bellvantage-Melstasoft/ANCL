using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface IConversionTableMasterDAO
    {
        ConversionTableMaster GetConversionToBase(int FromId, DBConnection dbConnection);
    }
    public class ConversionTableMasterDAO : IConversionTableMasterDAO
    {
        public ConversionTableMaster GetConversionToBase(int FromId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = $"SELECT * FROM CONVERSION_TABLE_MASTER WHERE FROM_ID={FromId}";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ConversionTableMaster>(dbConnection.dr);

            }
        }
    }
}
