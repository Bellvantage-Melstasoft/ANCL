using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class ItemMeasurement
    {

        [DBField("ITEM_ID")]
        public int DetailId { get; set; }

        [DBField("MEASUREMENT_DETAIL_ID")]
        public int MeasurementDetailId { get; set; }

        [DBField("MASTER_ID")]
        public int MasterId { get; set; }

        [DBField("MEASUREMENT_NAME")]
        public string MeasurementName { get; set; }

        [DBField("SHORT_CODE")]
        public string ShortCode { get; set; }

        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }

        [DBField("IS_BASE")]
        public int IsBase { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }
    }
}
