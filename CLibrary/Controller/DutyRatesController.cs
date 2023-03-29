using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;
namespace CLibrary.Controller {
    public interface DutyRatesController {
        int SaveCurrencyRates(string HSCode, decimal XID, decimal CID, decimal PAL, decimal EIC, string HsCodeName);
        DutyRates GetRatesByHSCode(string HsCode);
        List<DutyRates> GetRates();
        int SaveCountry(string Country);
        int HsCOdeAvailability(string HsId);
        List<DutyRates> GetRatesByHsCodesList(List<string> HsCodes);
        int InsertDutyRatesHistory(List<DutyRates> DutyRatesList);
    }
    public class DutyRatesControllerImpl : DutyRatesController {

        public int SaveCurrencyRates(string HSCode, decimal XID, decimal CID, decimal PAL, decimal EIC, string HsCodeName) {
            DBConnection dbConnection = new DBConnection();
            try {
                DutyRatesDAO dutyRatesDAO = DAOFactory.CreateDutyRatesDAO();
                return dutyRatesDAO.SaveCurrencyRates(HSCode, XID, CID, PAL, EIC, HsCodeName, dbConnection);
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
        
        public DutyRates GetRatesByHSCode(string HsCode) {
            DBConnection dbConnection = new DBConnection();
            try {
                DutyRatesDAO dutyRatesDAO = DAOFactory.CreateDutyRatesDAO();
                return dutyRatesDAO.GetRatesByHSCode(HsCode, dbConnection);
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
        public int SaveCountry(string Country) {
            DBConnection dbConnection = new DBConnection();
            try {
                DutyRatesDAO dutyRatesDAO = DAOFactory.CreateDutyRatesDAO();
                return dutyRatesDAO.SaveCountry(Country, dbConnection);
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

        public int InsertDutyRatesHistory(List<DutyRates> DutyRatesList) {
            DBConnection dbConnection = new DBConnection();
            try {
                DutyRatesDAO dutyRatesDAO = DAOFactory.CreateDutyRatesDAO();
                int result = 0;
                for (int i = 0; i < DutyRatesList.Count; i++) {
                     result = dutyRatesDAO.InsertDutyRatesHistory(DutyRatesList[i].HsId, DutyRatesList[i].HsIdName, DutyRatesList[i].EIC, DutyRatesList[i].CID, DutyRatesList[i].PAL, DutyRatesList[i].XID, dbConnection);
                }
                
                return result;
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

        public int HsCOdeAvailability(string HsId) {
            DBConnection dbConnection = new DBConnection();
            try {
                DutyRatesDAO dutyRatesDAO = DAOFactory.CreateDutyRatesDAO();
                return dutyRatesDAO.HsCOdeAvailability(HsId, dbConnection);
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
        public List<DutyRates> GetRates() {
            DBConnection dbConnection = new DBConnection();
            try {
                DutyRatesDAO dutyRatesDAO = DAOFactory.CreateDutyRatesDAO();
                return dutyRatesDAO.GetRates(dbConnection);
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

        public List<DutyRates> GetRatesByHsCodesList(List<string> HsCodes) {
            DBConnection dbConnection = new DBConnection();
            try {
                DutyRatesDAO dutyRatesDAO = DAOFactory.CreateDutyRatesDAO();
                return dutyRatesDAO.GetRatesByHsCodesList(HsCodes,dbConnection);
            }
            catch (Exception ex) {
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
