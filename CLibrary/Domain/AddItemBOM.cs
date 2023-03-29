using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    [Serializable]
    public class AddItemBOM
    {

        [DBField("COMPANY_ID")]
        public int companyId { get; set; }

        [DBField("ITEM_ID")]
        public int itemId { get; set; }

        [DBField("SEQ_NO")]
        public int SeqNo { get; set; }

        [DBField("MATERIAL")]
        public string Material { get; set; }

        [DBField("DESCRIPTION")]
        public string Description { get; set; }

        [DBField("IS_ACTIVE")]
        public int isActive { get; set; }

       [DBField("CREATED_DATETIME")]
        public DateTime createdDate { get; set; }

       [DBField("CREATED_BY")]
        public string createdBy { get; set; }

       [DBField("UPDATED_DATETIME")]
        public DateTime updateDate { get; set; }

       [DBField("UPDATED_BY")]
       public string updatedBy { get; set; }

        public string Todo { get; set; }

        //public int ItemId { get; set; }
        public int IsAdded { get; set; }
        public int num { get; set; }

        public string MrndId { get; set; }
    }
}
