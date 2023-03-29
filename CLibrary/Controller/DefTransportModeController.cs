using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface DefTransportModeController {
        List<DefTransportMode> FetchDefTransportModeList();
    }

    public class DefTransportModeControllerImpl : DefTransportModeController {
        public List<DefTransportMode> FetchDefTransportModeList() {
            DBConnection dbConnection = new DBConnection();
            try {
                DefTransportModeDAO defTransportMode = DAOFactory.CreateDefTransportModeDAO();
                return defTransportMode.FetchDefTransportModeList(dbConnection);
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
