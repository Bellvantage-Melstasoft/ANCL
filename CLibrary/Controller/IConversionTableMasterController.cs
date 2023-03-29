using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface IConversionTableMasterController
    {
        ConversionTableMaster GetConversionToBase(int FromId);
    }

    public class ConversionTableMasterController : IConversionTableMasterController
    {
        public ConversionTableMaster GetConversionToBase(int FromId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IConversionTableMasterDAO DAO = DAOFactory.CreateConversionTableMasterDAO();
                return DAO.GetConversionToBase(FromId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
    }
}
