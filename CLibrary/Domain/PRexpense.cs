using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace CLibrary.Domain
{
    public class PRexpense
    {
        [DBField("PR_ID")]
        public int PrId { get; set; }

        [DBField("IS_BUDGET")]
        public int IsBudget { get; set; }

        [DBField("BUDGET_AMOUNT")]
        public decimal BudgetAmount { get; set; }

        [DBField("REMARKS")]
        public string Remarks { get; set; }

        [DBField("BUDGET_INFORMATION")]
        public string BudgetInfo { get; set; }
        
    }
}
