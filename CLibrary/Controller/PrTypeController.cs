using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface PrTypeController
    {
        List<PrType> FetchPRTypesByCompanyId(int CompanyId);
        List<PrType> FetchAllPRTypes();

        int SavePRTypes(int CompanyId, string PrTypeName, int IsActive);
        int UpdatePRTypes(int PrTypeId, int CompanyId, string PrTypeName, int IsActive);
        int DeletePRTypes(int PrTypeId, int CompanyId);
    }

    public class PrTypeControllerImpl : PrTypeController
    {
        public List<PrType> FetchPRTypesByCompanyId(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrTypeDAO prTypeDAO = DAOFactory.CreatePrTypeDAO();
                return prTypeDAO.FetchPRTypesByCompanyId(CompanyId, dbConnection);
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

        public List<PrType> FetchAllPRTypes()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrTypeDAO prTypeDAO = DAOFactory.CreatePrTypeDAO();
                return prTypeDAO.FetchAllPRTypes(dbConnection);
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

        public int SavePRTypes(int CompanyId, string PrTypeName, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrTypeDAO prTypeDAO = DAOFactory.CreatePrTypeDAO();
                return prTypeDAO.SavePRTypes(CompanyId, PrTypeName, IsActive, dbConnection);
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

        public int UpdatePRTypes(int PrTypeId, int CompanyId, string PrTypeName, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrTypeDAO prTypeDAO = DAOFactory.CreatePrTypeDAO();
                return prTypeDAO.UpdatePRTypes(PrTypeId, CompanyId, PrTypeName, IsActive, dbConnection);
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

        public int DeletePRTypes(int PrTypeId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrTypeDAO prTypeDAO = DAOFactory.CreatePrTypeDAO();
                return prTypeDAO.DeletePRTypes(PrTypeId, CompanyId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw ex;
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
