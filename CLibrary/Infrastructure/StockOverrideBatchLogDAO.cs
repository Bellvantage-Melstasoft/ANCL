using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface StockOverrideBatchLogDAO {
        List<StockOverrideBatchLog> getOverriddenBathes(int LogId, DBConnection dbConnection);
    }
    public class StockOverrideBatchLogDAOImpl : StockOverrideBatchLogDAO {

        public List<StockOverrideBatchLog> getOverriddenBathes(int LogId, DBConnection dbConnection) {

            List<StockOverrideBatchLog> mrnIssuedInventoryBatches;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM STOCK_OVERRIDE_BATCH_LOG AS LOG " +
                                            "INNER JOIN(SELECT BATCH_ID, BATCH_CODE,EXPIRY_DATE AS BATCH_EXPIRY_DATE FROM WAREHOUSE_INVENTORY_BATCHES) AS WIB ON WIB.BATCH_ID = LOG.BATCH_ID " +
                                             "INNER JOIN(SELECT ITEM_ID, MEASUREMENT_ID FROM ADD_ITEMS) AS AIM ON AIM.ITEM_ID = LOG.ITEM_ID " +
                                        "INNER JOIN(SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID = AIM.MEASUREMENT_ID " +

                                            "WHERE OVERRIDE_LOG_ID = " + LogId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnIssuedInventoryBatches = dataAccessObject.ReadCollection<StockOverrideBatchLog>(dbConnection.dr);
            }

            return mrnIssuedInventoryBatches;
        }

    }
}
