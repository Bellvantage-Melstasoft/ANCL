using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace CLibrary.Domain
{
    public class MRNStockDepartment
    {
        [DBField("ID")]
        public int Id { get; set; }

        [DBField("MRN_ID")]
        public int MrnId { get; set; }       

        [DBField("MRND_ID")]
        public int MrnDId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("REQUESTED_QTY")]
        public decimal RequestedQty { get; set; }

        [DBField("AVAILABLE_QTY")]
        public decimal AvailableQty { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int MeasurementId { get; set; }

        [DBField("ENTERED_USER")]
        public int EnteredUserId { get; set; }

        [DBField("ENTERED_DATE")]
        public DateTime EnteredDate { get; set; }
    }
}
