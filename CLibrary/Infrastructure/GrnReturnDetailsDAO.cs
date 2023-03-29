using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface GrnReturnDetailsDAO {
        int SaveGrnDetails(int GrnId, int GrndId, int ItemId, int MeasurementId, decimal ReturnedQty, decimal SubTotal, decimal VatValue, decimal NetTotal, decimal UnitPrice, DBConnection dBConnection);
        List<GrnReturnDetails> GetReturndetails(int GetReturndetails, DBConnection dbConnection);
        int UpdateWaitingQty(int ItemId, int PodId, decimal ReturnedQty, DBConnection dBConnection);
        int UpdateReceivedQty(int ItemId, int PodId, decimal ReturnedQty, DBConnection dBConnection);
    }

    public class GrnReturnDetailsDAOImpl : GrnReturnDetailsDAO {
        public int SaveGrnDetails( int GrnId, int GrndId, int ItemId, int MeasurementId, decimal ReturnedQty, decimal SubTotal, decimal VatValue, decimal NetTotal, decimal UnitPrice,  DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = " INSERT INTO GRN_RETURN_DETAILS VALUES(" + GrnId + ", " + GrndId + ", " + ItemId + ", " + MeasurementId + ", " + ReturnedQty + ", " + SubTotal + " , " + VatValue + ", " + NetTotal + ", " + UnitPrice + ", (select MAX(GRN_RETURN_ID) from GRN_RETURN_MASTER where GRN_ID = "+ GrnId + " ) )";
            dBConnection.cmd.CommandText += " UPDATE GRN_DETAILS SET TOTAL_AMOUNT = TOTAL_AMOUNT-" + NetTotal + ", VAT_AMOUNT = VAT_AMOUNT-" + VatValue + ", QUANTITY =QUANTITY-"+ ReturnedQty + " WHERE GRND_ID = " + GrndId + "  ";

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<GrnReturnDetails> GetReturndetails(int GetReturndetails, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM GRN_RETURN_DETAILS AS RGRN  " +
                "LEFT JOIN (SELECT ITEM_NAME, ITEM_ID FROM ADD_ITEMS) AS AI ON AI.ITEM_ID = RGRN.ITEM_ID " +
                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID = RGRN.MEASUREMENT_ID " +
                "LEFT JOIN (SELECT GRND_ID, SUPPLIER_MENTIONED_ITEM_NAME FROM GRN_DETAILS) AS GD ON GD.GRND_ID = RGRN.GRND_ID " +
                "WHERE RGRN.GRN_RETURN_ID = " + GetReturndetails + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnReturnDetails>(dbConnection.dr);
            }
        }

        public int UpdateWaitingQty(int ItemId, int PodId, decimal ReturnedQty, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = " UPDATE PO_DETAILS SET WAITING_QTY = WAITING_QTY-" + ReturnedQty + " WHERE ITEM_ID = " + ItemId + " AND PO_DETAILS_ID = "+ PodId + " ";

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateReceivedQty(int ItemId, int PodId, decimal ReturnedQty, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = " UPDATE PO_DETAILS SET RECEIVED_QTY = RECEIVED_QTY-" + ReturnedQty + " WHERE ITEM_ID = " + ItemId + " AND PO_DETAILS_ID = " + PodId + " ";

            return dBConnection.cmd.ExecuteNonQuery();
        }
    }
    }
