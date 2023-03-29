using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface IItemMeasurementController
    {
        int AddItemMeasurement(List<ItemMeasurement> Measurements, int ItemId, int CompanyId);
        List<ItemMeasurement> GetItemMeasurements(int ItemId, int CompanyId);
    }

    public class ItemMeasurementController : IItemMeasurementController
    {
        public int AddItemMeasurement(List<ItemMeasurement> Measurements, int ItemId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IItemMeasurementDAO DAO = DAOFactory.CreateItemMeasurementDAO();
                return DAO.AddItemMeasurement(Measurements, ItemId, CompanyId, dbConnection);
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

        public List<ItemMeasurement> GetItemMeasurements(int ItemId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IItemMeasurementDAO DAO = DAOFactory.CreateItemMeasurementDAO();
                return DAO.GetItemMeasurements(ItemId, CompanyId, dbConnection);
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
