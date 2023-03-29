using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface ImportsHistoryController {
        List<ImportsHistory> GetHistoryForQuotationSubmission();
    }

    public class ImportsHistoryControllerImpl : ImportsHistoryController {

        public List<ImportsHistory> GetHistoryForQuotationSubmission() {
            DBConnection dbConnection = new DBConnection();
            try {
               ImportsHistoryDAO importsHistoryDAO = DAOFactory.CreateImportsHistoryDAO();
                return importsHistoryDAO.GetHistoryForQuotationSubmission(dbConnection);
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
