using System;
using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CLibrary.Controller
{
    public interface AfterPOController
    {
        DataTable getDateOptions();
        DataTable getCustomChargesTypes();
        DataTable getChargesTypes();
        DataTable getShippingChargesType();
        bool SaveAfterPODetails(AfterPO objAfterPO);
        List<AfterPO> getAfterPODetails();
        List<AfterPO> getAfterPODetailsByMonth(DateTime date);
    }

    public class AfterPOControllerImp : AfterPOController
    {
        public List<AfterPO> getAfterPODetails()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AfterPO_DAO afterPO_DAO = DAOFactory.createAfterPO_DAO();
                return afterPO_DAO.GetAfterPODetails(dbConnection);
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

        public List<AfterPO> getAfterPODetailsByMonth(DateTime date)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AfterPO_DAO afterPO_DAO = DAOFactory.createAfterPO_DAO();
                return afterPO_DAO.GetAfterPODetailsByMonth(date,dbConnection);
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

        public DataTable getChargesTypes()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AfterPO_DAO afterPO_DAO = DAOFactory.createAfterPO_DAO();
                return afterPO_DAO.getChargesTypes(dbConnection);
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

        public DataTable getCustomChargesTypes()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AfterPO_DAO afterPO_DAO = DAOFactory.createAfterPO_DAO();
                return afterPO_DAO.getCustomChargesTypes(dbConnection);
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

        public DataTable getDateOptions()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AfterPO_DAO afterPO_DAO = DAOFactory.createAfterPO_DAO();
                return afterPO_DAO.getDateOptions(dbConnection);
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

        public DataTable getShippingChargesType()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AfterPO_DAO afterPO_DAO = DAOFactory.createAfterPO_DAO();
                return afterPO_DAO.getShippingChargesType(dbConnection);
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

        public bool SaveAfterPODetails(AfterPO objAfterPO)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AfterPO_DAO afterPO_DAO = DAOFactory.createAfterPO_DAO();
                return afterPO_DAO.SaveAfterPODetails(objAfterPO,dbConnection);
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
    }
}
