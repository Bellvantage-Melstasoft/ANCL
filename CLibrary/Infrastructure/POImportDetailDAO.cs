using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface POImportDetailDAO
    {
        List<POImportReferenceDefinition> GetImportReferenceDef(DBConnection dbConnection);
        List<POImportPaymentModeDef> GetImportPaymentModeDef(DBConnection dbConnection);
        List<POImportTransportModeDef> GetImportTransportModeDef(DBConnection dbConnection);
        List<POImportPriceTerms> GetImportPriceTermsDef(DBConnection dbConnection);
        List<POImportReferenceDefinition> GetImportOtherReferenceDef(DBConnection dbConnection);
        List<POImportChargesDefinition> GetImportPOImportChargesDef(DBConnection dbConnection);
        List<POImportChargesDefinition> GetImportPOImportCustomChargesDef(DBConnection dbConnection);
    }


    public class POAImportDetailDAOImpl : POImportDetailDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];        

        public List<POImportReferenceDefinition> GetImportReferenceDef(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".IMPORT_REFERENCE_DEFINITION  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POImportReferenceDefinition>(dbConnection.dr);
            }
        }

        public List<POImportPaymentModeDef> GetImportPaymentModeDef(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".IMPORT_PAYMENT_MODE_DEFI  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POImportPaymentModeDef>(dbConnection.dr);
            }
        }

        public List<POImportTransportModeDef> GetImportTransportModeDef(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".IMPORT_TRANSPORT_MODE_DEFINITION  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POImportTransportModeDef>(dbConnection.dr);
            }
        }

        public List<POImportPriceTerms> GetImportPriceTermsDef(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".IMPORT_PRICETERMS_DEFINITION  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POImportPriceTerms>(dbConnection.dr);
            }
        }

        public List<POImportReferenceDefinition> GetImportOtherReferenceDef(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".IMPORT_OTHER_REFERENCE_DEFINITION  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POImportReferenceDefinition>(dbConnection.dr);
            }
        }

        public List<POImportChargesDefinition> GetImportPOImportChargesDef(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".IMPORT_CHARGES_DEFINITION  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POImportChargesDefinition>(dbConnection.dr);
            }
        }

        public List<POImportChargesDefinition> GetImportPOImportCustomChargesDef(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".IMPORT_CUSTOM_CHARGES_DEFINITION  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POImportChargesDefinition>(dbConnection.dr);
            }
        }
    }
}
