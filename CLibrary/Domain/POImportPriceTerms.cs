
using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
   public class POImportPriceTerms
    {
        [DBField("PRICE_TERMS_ID")]
        public int PriceTermId { get; set; }

        [DBField("NAME")]
        public string Name { get; set; }


    }
}
