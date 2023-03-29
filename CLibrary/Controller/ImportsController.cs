using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface ImportsController
    {
        int InsertImportRates(List<Imports> importRates);
        int UpdateRates(List<CurrencyRate> listCurrency);
        ImportCalucationDetails GetImportDetails(int QuotationId);
        CurrencyRate GetExchangeRate(int currenctyTypeId);
        ImportCalucationDetails GetExchangeRateSelected(int QuotationId, int TabulationId, int QuotationItemId);
        List<ImportQuotationItem> GetImportDetailsListForTabulationReview(int BidId, int BidItemId);
        int UpdateImportValues(List<ImportQuotationItem> ImportQuotationItemList, List<int> QuotationIds);
        List<ImportQuotationItem> GetAllImportQuotations(int QuotationId);
    }
    public class ImportsControllerImp : ImportsController
    {
        public CurrencyRate GetExchangeRate(int currenctyTypeId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ImportsDAO importsDAO = DAOFactory.createImportsDAO();
                return importsDAO.GetExchangeRate(currenctyTypeId, dbConnection);
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

        public ImportCalucationDetails GetExchangeRateSelected(int QuotationId, int TabulationId, int QuotationItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ImportsDAO importsDAO = DAOFactory.createImportsDAO();
                return importsDAO.GetExchangeRateSelected(QuotationId,  TabulationId, QuotationItemId, dbConnection);
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

        public ImportCalucationDetails GetImportDetails(int QuotationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ImportsDAO importsDAO = DAOFactory.createImportsDAO();
                return importsDAO.GetImportDetails(QuotationId, dbConnection);
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
        public List<ImportQuotationItem> GetImportDetailsListForTabulationReview(int BidId, int BidItemId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ImportsDAO importsDAO = DAOFactory.createImportsDAO();
                return importsDAO.GetImportDetailsListForTabulationReview(BidId, BidItemId, dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<ImportQuotationItem> GetAllImportQuotations(int QuotationId) {
            DBConnection dbConnection = new DBConnection();
            try {
                ImportsDAO importsDAO = DAOFactory.createImportsDAO();
                return importsDAO.GetAllImportQuotations(QuotationId, dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int InsertImportRates(List<Imports> importRates)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ImportsDAO importsDAO = DAOFactory.createImportsDAO();
                return importsDAO.InsertImportsRates(importRates,dbConnection);
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

        public int UpdateImportValues(List<ImportQuotationItem> ImportQuotationItemList, List<int> QuotationIds) {
            DBConnection dbConnection = new DBConnection();
            try {
                ImportsDAO importsDAO = DAOFactory.createImportsDAO();
                SupplierQuotationDAO SupplierQuotationDAO = DAOFactory.createSupplierQuotationDAO();

               int result = importsDAO.UpdateImportValues(ImportQuotationItemList, dbConnection);
                if (result > 0) {
                    for (int i = 0; i < QuotationIds.Count; i++) {
                        result = SupplierQuotationDAO.UpdateSupplierImports(QuotationIds[i], dbConnection);
                    }
                   
                    if (result > 0) {
                        return 1;
                    }
                    else {
                        dbConnection.RollBack();
                        return -2;
                    }
                }
                else {
                    dbConnection.RollBack();
                    return -1;
                }



            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int UpdateRates(List<CurrencyRate> listCurrency)
        {
            int result = 0;
            DBConnection dbConnection = new DBConnection();
            try
            {
                
                ImportsDAO importsDAO = DAOFactory.createImportsDAO();

                foreach(CurrencyRate objCurrency in listCurrency)
                {
                    result= importsDAO.UpdateRates(objCurrency, dbConnection);
                }

                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
                dbConnection.RollBack();
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }

            return result;
        }
    }
}
