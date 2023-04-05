using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Domain
{
    [Serializable]
    public class SupplierItemReport
    {
        public int SupplierId { get; set; }

        public String SupplierName { get; set; }

        public string ItemName { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public string GrnCode { get; set; }

        public int IsApproved { get; set; }
    }
}
