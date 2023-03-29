﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class PR_BillOfMeterial
    {
        [DBField("PR_ID")]
        public int PrId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("SEQ_NO")]
        public int SeqId { get; set; }

        [DBField("MATERIAL")]
        public string Meterial { get; set; }

        [DBField("DESCRIPTION")]
        public string Description { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDateTime { get; set; }

        [DBField("CREATED_BY")]
        public string CreatedBy { get; set; }

        [DBField("UPDATED_DATETIME")]
        public DateTime UpdatedDateTime { get; set; }

        [DBField("UPDATED_BY")]
        public string UpdatedBy { get; set; }

        [DBField("COMPLY")]
        public string comply { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }
    }
}
