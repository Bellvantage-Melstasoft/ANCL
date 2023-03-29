using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class ItemImageUpload
    {
        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("IMAGE_PATH")]
        public string ImagePath { get; set; }
    }
}
