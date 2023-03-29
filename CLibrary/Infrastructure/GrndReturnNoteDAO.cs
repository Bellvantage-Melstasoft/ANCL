using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface GrndReturnNoteDAO {
        int UpdateReturnGrnNote(int GrndId, int ItemId, int warehouseId, decimal qty, decimal total, int UserId, int MeasurementId, string Remark, DBConnection dBConnection);

    }

    public class GrndReturnNoteDAOImpl : GrndReturnNoteDAO {

        public int UpdateReturnGrnNote(int GrndId, int ItemId, int warehouseId, decimal qty, decimal total, int UserId, int MeasurementId, string Remark, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = " INSERT INTO GRND_RETURN_NOTE VALUES("+ GrndId + ", " + ItemId + ", " + warehouseId + ", " + qty + ", " + total + ", " + UserId + ", '"+LocalTime.Now+"' , " + MeasurementId + ", '"+ Remark + "' )";
            return dBConnection.cmd.ExecuteNonQuery();
        }
    }
    }
