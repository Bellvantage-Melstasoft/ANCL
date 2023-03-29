using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface PhysicalstockVerificationDetailsDAO {
        List<physicalStockVerificationDetails> GetPSVDbyPSVMId(int PSVMId, DBConnection dbConnection);

    }

    public class PhysicalstockVerificationDetailsDAOSQLImpl : PhysicalstockVerificationDetailsDAO {



        public List<physicalStockVerificationDetails> GetPSVDbyPSVMId(int PSVMId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM PHYSICAL_STOCK_VERIFICATION_DETAIL AS PSVD " +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME, REFERENCE_NO, MEASUREMENT_ID FROM ADD_ITEMS) AS AI ON AI.ITEM_ID = PSVD.ITEM_ID " +
                                            "LEFT JOIN (SELECT SHORT_CODE AS MEASUREMENT_SHORT_NAME, DETAIL_ID AS MEASUREMENT_ID FROM MEASUREMENT_DETAIL) AS UM ON UM.MEASUREMENT_ID = AI.MEASUREMENT_ID " +
                                            "WHERE PSVM_ID = " + PSVMId + " ORDER BY AI.ITEM_NAME ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<physicalStockVerificationDetails>(dbConnection.dr);
            }
        }


    }

}


