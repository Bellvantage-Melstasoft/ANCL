
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface MRNCapexDocController {
        List<MrnCapexDoc> GetMrnCapexDocs(int mrnId);
    }
    public class MRNCapexDocControllerImpl : MRNCapexDocController {

       
            public List<MrnCapexDoc> GetMrnCapexDocs(int mrnId) {
                DBConnection dbConnection = new DBConnection();
                try {
                MrnCapexDocDAO mrnCapexDocDAO = DAOFactory.CreateMrnCapexDocDAO();
                    return mrnCapexDocDAO.GetMrnCapexDocs(mrnId, dbConnection);
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