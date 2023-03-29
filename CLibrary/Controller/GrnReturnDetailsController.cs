using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CLibrary.Controller {
    public interface GrnReturnDetailsController {
        List<GrnReturnDetails> GetReturndetails(int ReturnGrndId);
    }

    public class GrnReturnDetailsControllerImpl : GrnReturnDetailsController {

        public List<GrnReturnDetails> GetReturndetails(int ReturnGrndId) {
            DBConnection dbConnection = new DBConnection();
            try {
                GrnReturnDetailsDAO grnReturnDetailsDAO = DAOFactory.CreateGrnReturnDetailsDAO();
                return grnReturnDetailsDAO.GetReturndetails(ReturnGrndId, dbConnection);
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
