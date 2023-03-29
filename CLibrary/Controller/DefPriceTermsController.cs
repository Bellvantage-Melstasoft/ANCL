using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface DefPriceTermsController {
        List<DefPriceTerms> FetchDefPriceTermsList();
    }

    public class DefPriceTermsControllerImpl : DefPriceTermsController {

        public List<DefPriceTerms> FetchDefPriceTermsList() {
            DBConnection dbConnection = new DBConnection();
            try {
                DefPriceTermsDAO defPriceTermsDAO = DAOFactory.CreateDefPriceTermsDAO();
                return defPriceTermsDAO.FetchDefPriceTermsList(dbConnection);
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
