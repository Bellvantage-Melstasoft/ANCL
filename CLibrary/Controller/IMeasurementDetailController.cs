using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface IMeasurementDetailController
    {
        MeasurementDetail GetStockMaintainingMeasurement(int ItemId, int CompanyId);
        List<MeasurementDetail> GetMeasurementDetailsOfItem(int ItemId, int CompanyId);
        List<MeasurementDetail> GetAllMeasurementsOfCompany(int CompanyId);
        List<MeasurementDetail> GetAllCustomMeasurementsOfCompany(int CompanyId);
        int UpdateCustomMeasurementActiveStatus(int MeasurementId, int IsActive);
        int CreateCustomMeasurement(string MeasurementName, string ShortCode, int CompanyId, int CreatedBy, int IsActive);
        List<MeasurementDetail> GetMeasurementDetailsByMasterId(int MeasurementMasterId, int CompanyId);
    }

    public class MeasurementDetailController : IMeasurementDetailController
    {
        public MeasurementDetail GetStockMaintainingMeasurement(int ItemId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IMeasurementDetailDAO DAO = DAOFactory.CreateMeasurementDetailDAO();
                return DAO.GetStockMaintainingMeasurement(ItemId, CompanyId, dbConnection);
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
        public List<MeasurementDetail> GetMeasurementDetailsOfItem(int ItemId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IMeasurementDetailDAO DAO = DAOFactory.CreateMeasurementDetailDAO();
                return DAO.GetMeasurementDetailsOfItem(ItemId, CompanyId, dbConnection);
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

        public List<MeasurementDetail> GetAllMeasurementsOfCompany(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IMeasurementDetailDAO DAO = DAOFactory.CreateMeasurementDetailDAO();
                return DAO.GetAllMeasurementsOfCompany(CompanyId, dbConnection);
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

        public List<MeasurementDetail> GetAllCustomMeasurementsOfCompany(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IMeasurementDetailDAO DAO = DAOFactory.CreateMeasurementDetailDAO();
                return DAO.GetAllCustomMeasurementsOfCompany(CompanyId, dbConnection);
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

        public int UpdateCustomMeasurementActiveStatus(int MeasurementId, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IMeasurementDetailDAO DAO = DAOFactory.CreateMeasurementDetailDAO();
                return DAO.UpdateCustomMeasurementActiveStatus(MeasurementId, IsActive, dbConnection);
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

        public int CreateCustomMeasurement(string MeasurementName, string ShortCode, int CompanyId, int CreatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IMeasurementDetailDAO DAO = DAOFactory.CreateMeasurementDetailDAO();
                return DAO.CreateCustomMeasurement(MeasurementName, ShortCode, CompanyId, CreatedBy, IsActive, dbConnection);
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

        public List<MeasurementDetail> GetMeasurementDetailsByMasterId(int MeasurementMasterId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                IMeasurementDetailDAO DAO = DAOFactory.CreateMeasurementDetailDAO();
                return DAO.GetMeasurementDetailsByMasterId(MeasurementMasterId, CompanyId, dbConnection);
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
