using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace CLibrary.Domain
{
    public class PRStockDepartment
    {
        [DBField("ID")]
        public int Id { get; set; }

        [DBField("PR_ID")]
        public int PrId { get; set; }

        [DBField("PRD_ID")]
        public int PrdId { get; set; }

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
