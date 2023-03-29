using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiddingSystem.ViewModels.CS
{
    [Serializable]
    public class BOMDetails
    {
        public string Material { get; set; }
        public string Description { get; set; }
        public string ItemId { get; set; }
    }
}