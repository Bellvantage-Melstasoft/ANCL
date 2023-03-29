using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
   public class Procument_PlanType
    {

        [DBField("PLAN_ID")]
        public int PlanId { get; set; }

        [DBField("PLAN_NAME")]
        public string PlanName { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("ENTERED_USER")]
        public string EnteredUser { get; set; }

        [DBField("ENTERED_DATE")]
        public DateTime EnteredDate { get; set; }

        [DBField("WITH_TIME")]
        public int WithTime { get; set; }
    }
}
