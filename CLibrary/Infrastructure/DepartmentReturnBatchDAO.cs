using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface DepartmentReturnBatchDAO {
        int InsertReturnDepartmentBatchDetail(int Result, int BatchId, decimal returnQty, decimal ReturnBatchStock,int mrndInId, DBConnection dBConnection);
    }

    public class DepartmentReturnBatchDAOSQLImpl : DepartmentReturnBatchDAO {

        public int InsertReturnDepartmentBatchDetail(int Result, int BatchId, decimal returnQty, decimal ReturnBatchStock, int mrndInId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = " INSERT INTO DEPARTMENT_RETURN_BATCH ([DEPARTMENT_RETURN_ID], [BATCH_ID], [RETURN_QTY], [RETURN_STOCK], [MRND_IN_ID]) " +
                " VALUES(" + Result + ", " + BatchId + ", " + returnQty + ", " + ReturnBatchStock + ", " + mrndInId + "  )";
            
            return dBConnection.cmd.ExecuteNonQuery();
        }
    }
    }
