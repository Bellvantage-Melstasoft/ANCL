using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface POImportDetailController
    {
        List<POImportReferenceDefinition> GetImportReferenceDef();
        List<POImportPaymentModeDef> GetImportPaymentModeDef();
        List<POImportTransportModeDef> GetImportTransportModeDef();
        List<POImportPriceTerms> GetImportPriceTermsDef();
        List<POImportReferenceDefinition> GetImportOtherReferenesDef();
        List<POImportChargesDefinition> GetImportPOImportChargesDef();
        List<POImportChargesDefinition> GetImportPOImportCustomChargesDef();
        List<POImportChargesDefinition> GetImportPOImportShippingAgentDef();
    }
    public class POImportDetailControllerImpl : POImportDetailController
    {
        public List<POImportReferenceDefinition> GetImportReferenceDef()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POImportDetailDAO poImportDetailDAO = DAOFactory.CreatePOImportDetailDAO();
                return poImportDetailDAO.GetImportReferenceDef(dbConnection);
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

        public List<POImportPaymentModeDef> GetImportPaymentModeDef()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POImportDetailDAO poImportDetailDAO = DAOFactory.CreatePOImportDetailDAO();
                return poImportDetailDAO.GetImportPaymentModeDef(dbConnection);
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

        public List<POImportTransportModeDef> GetImportTransportModeDef()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POImportDetailDAO poImportDetailDAO = DAOFactory.CreatePOImportDetailDAO();
                return poImportDetailDAO.GetImportTransportModeDef(dbConnection);
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

        public List<POImportPriceTerms> GetImportPriceTermsDef()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POImportDetailDAO poImportDetailDAO = DAOFactory.CreatePOImportDetailDAO();
                return poImportDetailDAO.GetImportPriceTermsDef(dbConnection);
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

        public List<POImportReferenceDefinition> GetImportOtherReferenesDef()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POImportDetailDAO poImportDetailDAO = DAOFactory.CreatePOImportDetailDAO();
                return poImportDetailDAO.GetImportOtherReferenceDef(dbConnection);
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

        public List<POImportChargesDefinition> GetImportPOImportChargesDef()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POImportDetailDAO poImportDetailDAO = DAOFactory.CreatePOImportDetailDAO();
                return poImportDetailDAO.GetImportPOImportChargesDef(dbConnection);
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

        public List<POImportChargesDefinition> GetImportPOImportCustomChargesDef()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POImportDetailDAO poImportDetailDAO = DAOFactory.CreatePOImportDetailDAO();
                return poImportDetailDAO.GetImportPOImportCustomChargesDef(dbConnection);
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

        public List<POImportChargesDefinition> GetImportPOImportShippingAgentDef()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POImportDetailDAO poImportDetailDAO = DAOFactory.CreatePOImportDetailDAO();
                return poImportDetailDAO.GetImportPOImportCustomChargesDef(dbConnection);
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
