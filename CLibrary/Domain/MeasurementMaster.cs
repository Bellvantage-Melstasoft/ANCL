using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    [Serializable()]
    public class MeasurementMaster
    {


        [DBField("ID")]
        public int Id { get; set; }

        [DBField("MEASUREMENT_NAME")]
        public string MeasurementName { get; set; }

        [DBField("IS_STANDARD")]
        public int IsStandard { get; set; }

        public List<MeasurementDetail> MeasurementDetails { get; set; }
    }
}
