using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class UnitMeasurement
    {
        [DBField("MEASUREMENT_ID")]
        public int measurentId { get; set; }
        [DBField("COMPANY_ID")]
        public int companyId { get; set; }
        [DBField("MEASUREMENT_NAME")]
        public string measurementName { get; set; }
        [DBField("MEASUREMENT_SHORT_NAME")]
        public string measurementShortName { get; set; }
        [DBField("CREATED_BY")]
        public string createdBy { get; set; }
        [DBField("CREATED_DATE")]
        public DateTime createdDate { get; set; }
        [DBField("UPDATED_BY")]
        public string updatedBy { get; set; }
        [DBField("UPDATED_DATE")]
        public DateTime updatedDate { get; set; }
        [DBField("IS_ACTIVE")]
        public int isActive { get; set; }

    }
}
