using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class PR_DetailsHistory
    {
        [DBField("PR_ID")]
        public int PrId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

      
        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }


        [DBField("ITEM_UPDATED_BY")]
        public string ItemUpdatedBy { get; set; }

        [DBField("ITEM_UPDATED_DATETIME")]
        public DateTime ItemUpdatedDateTime { get; set; }
        
        [DBField("ITEM_QUANTITY")]
        public decimal ItemQuantity { get; set; }

        

       
    }
}
