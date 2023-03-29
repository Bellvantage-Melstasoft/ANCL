using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface StockOverrideBatchLogController {
        List<StockOverrideBatchLog> getOverriddenBathes(int LogId);
    }

    public class StockOverrideBatchLogControllerImpl : StockOverrideBatchLogController {
        

            public List<StockOverrideBatchLog> getOverriddenBathes(int LogId) {
                DBConnection dbConnection = new DBConnection();
                try {
                StockOverrideBatchLogDAO stockOverrideBatchLog = DAOFactory.CreateStockOverrideBatchLogDAO();
                    return stockOverrideBatchLog.getOverriddenBathes(LogId, dbConnection);
                }
                catch (Exception ex) {
                    dbConnection.RollBack();
                    throw;
                }
                finally {
                    if (dbConnection.con.State == System.Data.ConnectionState.Open)
                        dbConnection.Commit();
                }
            }
        }
    }
