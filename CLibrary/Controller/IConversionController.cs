using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface IConversionController
    {

        decimal DoConversion(int ItemId, int CompanyId, decimal Qty, int FromUOM, int ToUOM);
        int AddConversion(List<Conversion> Conversions, int ItemId, int CompanyId);
        List<Conversion> GetItemConversions(int ItemId, int CompanyId);
        List<Conversion> GetCustomConversions(int ItemId, int CompanyId);
    }
    public class ConversionController : IConversionController
    {
        public int AddConversion(List<Conversion> Conversions, int ItemId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IConversionDAO DAO = DAOFactory.CreateConversionDAO();
                return DAO.AddConversion(Conversions, ItemId, CompanyId, dbConnection);
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

        public decimal DoConversion(int ItemId, int CompanyId, decimal Qty, int FromUOM, int ToUOM)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IConversionDAO DAO = DAOFactory.CreateConversionDAO();
                return DAO.DoConversion(ItemId, CompanyId, Qty, FromUOM, ToUOM, dbConnection);
            }
            catch (Exception ex)
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

        public List<Conversion> GetItemConversions(int ItemId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IConversionDAO DAO = DAOFactory.CreateConversionDAO();
                return DAO.GetItemConversions(ItemId, CompanyId, dbConnection);
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

        public List<Conversion> GetCustomConversions(int ItemId, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                IConversionDAO DAO = DAOFactory.CreateConversionDAO();
                return DAO.GetCustomConversions(ItemId, CompanyId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
    }
}

