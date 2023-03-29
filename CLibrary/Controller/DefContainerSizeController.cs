using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface DefContainerSizeController {
        List<DefContainerSize> FetchDefContainerSizeList();
    }

    public class DefContainerSizeControllerImpl : DefContainerSizeController {

        public List<DefContainerSize> FetchDefContainerSizeList() {
            DBConnection dbConnection = new DBConnection();
            try {
                DefContainerSizeDAO defContainerSizeDAO = DAOFactory.CreateDefContainerSizeDAO();
                return defContainerSizeDAO.FetchDefContainerSizeList(dbConnection);
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
