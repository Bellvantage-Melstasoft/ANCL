using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiddingSystem.ViewModels.CS
{
    [Serializable()]
    public class AddedMeasurementsVM
    {
        public int MeasurementId { get; set; }
        public string MeasurementName { get; set; }
        public int MasterId { get; set; }
        public int IsActive { get; set; }
        public int IsBase { get; set; }
        public int IsStandard { get; set; }
        public int ConversionToId { get; set; }
        public decimal Multiplier { get; set; }
        public int Status { get; set; }
    }
}