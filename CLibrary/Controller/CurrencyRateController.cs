using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;


namespace CLibrary.Controller {
    public interface CurrencyRateController {
        CurrencyRate FetchCurrencyRatesByMaxDate(int CurrencyTypeId);
        int SaveCurrencyRates(int CurrencyType, DateTime Date, decimal Buyingrate, decimal Sellingrate);
        int UpdateRates(List<CurrencyRate> CurrencyRateList);
        decimal GetRateByID(int CurrencyId);
        List<CurrencyRate> fetchCurrencyDetails();
        List<CurrencyRate> fetchCountry();
    }

    public class CurrencyRateControllerImpl : CurrencyRateController {

        public CurrencyRate FetchCurrencyRatesByMaxDate(int CurrencyTypeId) {
            DBConnection dbConnection = new DBConnection();
            try {
                CurrencyRateDAO currencyRateDAO = DAOFactory.CreateCurrencyRateDAO();
                return currencyRateDAO.FetchCurrencyRatesByMaxDate(CurrencyTypeId, dbConnection);
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

        public int SaveCurrencyRates(int CurrencyType, DateTime Date, decimal Buyingrate, decimal Sellingrate) {
            DBConnection dbConnection = new DBConnection();
            try {
                CurrencyRateDAO currencyRateDAO = DAOFactory.CreateCurrencyRateDAO();
                return currencyRateDAO.SaveCurrencyRates(CurrencyType, Date, Buyingrate, Sellingrate, dbConnection);
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

        public int UpdateRates(List<CurrencyRate> CurrencyRateList) {
            DBConnection dbConnection = new DBConnection();
            try {
                CurrencyRateDAO currencyRateDAO = DAOFactory.CreateCurrencyRateDAO();
                return currencyRateDAO.UpdateRates(CurrencyRateList, dbConnection);
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

        public decimal GetRateByID(int CurrencyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                CurrencyRateDAO currencyRateDAO = DAOFactory.CreateCurrencyRateDAO();
                return currencyRateDAO.GetRateByID(CurrencyId, dbConnection);
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

        public List<CurrencyRate> fetchCurrencyDetails() {
            DBConnection dbConnection = new DBConnection();
            try {
                CurrencyRateDAO currencyRateDAO = DAOFactory.CreateCurrencyRateDAO();
                return currencyRateDAO.fetchCurrencyDetails(dbConnection);
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

        public List<CurrencyRate> fetchCountry() {
            DBConnection dbConnection = new DBConnection();
            try {
                CurrencyRateDAO currencyRateDAO = DAOFactory.CreateCurrencyRateDAO();
                return currencyRateDAO.fetchCountry(dbConnection);
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
