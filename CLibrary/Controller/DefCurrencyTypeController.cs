using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface DefCurrencyTypeController {
        List<DefCurrencyType> FetchDefCurrencyTypeList();
        int SaveCurrencyType(String CurrencyName, string CurrencyShortName);
    }

    public class DefCurrencyTypeControllerImpl : DefCurrencyTypeController {
        public List<DefCurrencyType> FetchDefCurrencyTypeList() {
            DBConnection dbConnection = new DBConnection();
            try {
                DefCurrencyTypeDAO defCurrencyType = DAOFactory.CreateDefCurrencyTypeDAO();
                return defCurrencyType.FetchDefCurrencyTypeList(dbConnection);
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

        public int SaveCurrencyType(String CurrencyName, string CurrencyShortName) {
            DBConnection dbConnection = new DBConnection();
            try {
                DefCurrencyTypeDAO defCurrencyType = DAOFactory.CreateDefCurrencyTypeDAO();
                return defCurrencyType.SaveCurrencyType(CurrencyName, CurrencyShortName, dbConnection);
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
