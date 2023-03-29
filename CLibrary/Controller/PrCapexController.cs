
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface PrCapexController {

        List<PrCapexDoc> GetPrCapexDocs(int prId);
    }
    public class PrCapexControllerImpl : PrCapexController {
        public List<PrCapexDoc> GetPrCapexDocs(int prId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrCapexDocDAO prCapexDocDAO = DAOFactory.CreatePrCapexDocDAO();
                return prCapexDocDAO.GetPrCapexDocs(prId,dbConnection);
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