using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    [Serializable()]
    public class MeasurementDetail
    {
        [DBField("DETAIL_ID")]
        public int DetailId { get; set; }

        [DBField("MASTER_ID")]
        public int MasterId { get; set; }

        [DBField("MEASUREMENT_NAME")]
        public string MeasurementName { get; set; }

        [DBField("SHORT_CODE")]
        public string ShortCode { get; set; }

        [DBField("IS_BASE")]
        public int IsBase { get; set; }

        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }

        [DBField("CREATED_BY")]
        public int CreatedBy { get; set; }

        [DBField("CREATED_BY_NAME")]
        public string CreatedByName { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }
    }
}
