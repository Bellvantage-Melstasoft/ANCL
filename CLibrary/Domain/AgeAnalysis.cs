using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Domain
{
    [Serializable]
    public class AgeAnalysis
    {
        [DBField("PO_CODE")]
        public string POCode { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("GOOD_RECEIVED_DATE")]
        public DateTime GoodReceivedDate { get; set; }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("ITEM_PRICE")]
        public float ItemPrice { get; set; }

        [DBField("QUANTITY")]
        public int Quantity { get; set; }

        [DBField("RECEIVED_QTY")]
        public int ReceivedQuantity { get; set; }

        [DBField("WAITING_QTY")]
        public int WaitingQuantity { get; set; }

        [DBField("TOTAL_AMOUNT")]
        public float TotalAmount { get; set; }

        [DBField("SUB_DEPARTMENT_ID")]
        public int SubDepartmentId { get; set; }
    }

}
