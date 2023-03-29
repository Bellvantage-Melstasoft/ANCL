using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain {
    public class GrnReturnMaster {

        [DBField("GRN_RETURN_ID")]
        public int GrnReturnId { get; set; }

        [DBField("GRN_ID")]
        public int GrnId { get; set; }

        [DBField("GRN_CODE")]
        public string GrnCode { get; set; }

        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }

        [DBField("SUPPLIER_ID")]
        public int SupplierId { get; set; }

      
        [DBField("REMARK")]
        public string Remark { get; set; }

        [DBField("RETURNED_BY")]
        public int ReturnedBy { get; set; }

        [DBField("SUPPLIER_RETURN_OPTION")]
        public int SupplierReturnOption { get; set; }

        [DBField("RETURNED_ON")]
        public DateTime ReturnedOn { get; set; }

        [DBField("VAT_TOTAL_VALUE")]
        public decimal VatTotalvalue { get; set; }

        [DBField("SUB_TOTAL_VALUE")]
        public decimal SubTotalValue { get; set; }

        [DBField("NET_TOTAL_VALUE")]
        public decimal NetTotalValue { get; set; }

        [DBField("RETURNED_USER_NAME")]
        public string ReturnedUserName { get; set; }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName { get; set; }

        
        [DBField("WAREHOUSE_NAME")]
        public string WarehouseName { get; set; }

    }
}
