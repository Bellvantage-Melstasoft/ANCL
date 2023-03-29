using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface DefPaymentModeController {
        List<DefPaymentMode> FetchDefPaymentModeList();
    }

    public class DefPaymentModeControllerImpl : DefPaymentModeController {

        public List<DefPaymentMode> FetchDefPaymentModeList() {
            DBConnection dbConnection = new DBConnection();
            try {
                DefPaymentModeDAO defPaymentMode = DAOFactory.CreateDefPaymentModeDAO();
                return defPaymentMode.FetchDefPaymentModeList(dbConnection);
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
