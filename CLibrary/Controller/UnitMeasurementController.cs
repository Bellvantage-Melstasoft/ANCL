using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public interface UnitMeasurementController
    {
        int saveMeasurement(int companyId, string measurementName, string measurementShortName, string createdBy, DateTime createdDate, string updatedBy, DateTime updatedDate, int isActive);
        int updateMeasurement(int companyId, int measurementId, string measurementName, string measurementShortName, string updatedBy, DateTime updatedDate, int isActive);
        List<UnitMeasurement> fetchMeasurementsByCompanyID(int companyId);
        UnitMeasurement fetchMeasurementObjByMeasurementId(int measurementId);
        int deleteMesurement(int measurementId, int companyId);
        List<Currency> fetchCurrency();
    }
    public class UnitMeasurementControllerImpl : UnitMeasurementController
    {
        public int deleteMesurement(int measurementId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                UnitMeasurementDAO unitMeasurementDAO = DAOFactory.CreateUnitMeasurementDAO();
                return unitMeasurementDAO.deleteMesurement( measurementId,  companyId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<Currency> fetchCurrency()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                UnitMeasurementDAO unitMeasurementDAO = DAOFactory.CreateUnitMeasurementDAO();
                return unitMeasurementDAO.fetchCurrency(dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public UnitMeasurement fetchMeasurementObjByMeasurementId(int measurementId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                UnitMeasurementDAO unitMeasurementDAO = DAOFactory.CreateUnitMeasurementDAO();
                return unitMeasurementDAO.fetchMeasurementObjByMeasurementId( measurementId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)  
                    dbConnection.Commit();
            }
        }

        public List<UnitMeasurement> fetchMeasurementsByCompanyID(int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                UnitMeasurementDAO unitMeasurementDAO = DAOFactory.CreateUnitMeasurementDAO();
                return unitMeasurementDAO.fetchMeasurementsByCompanyID( companyId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int saveMeasurement(int companyId, string measurementName, string measurementShortName, string createdBy, DateTime createdDate, string updatedBy, DateTime updatedDate, int isActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                UnitMeasurementDAO unitMeasurementDAO = DAOFactory.CreateUnitMeasurementDAO();
                return unitMeasurementDAO.saveMeasurement( companyId,  measurementName,  measurementShortName,  createdBy,  createdDate,  updatedBy,  updatedDate,  isActive, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int updateMeasurement(int companyId, int measurementId, string measurementName, string measurementShortName, string updatedBy, DateTime updatedDate, int isActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                UnitMeasurementDAO unitMeasurementDAO = DAOFactory.CreateUnitMeasurementDAO();
                return unitMeasurementDAO.updateMeasurement( companyId,  measurementId,  measurementName,  measurementShortName,  updatedBy,  updatedDate,  isActive, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
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
