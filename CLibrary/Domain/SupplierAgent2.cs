using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
   public class SupplierAgent2
    {
        private int supplierId;
        private int supplierAgentId;
        private int isActive;

        [DBField("SUPPLIER_ID")]
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        [DBField("SUPPLIER_AGENT_ID")]
        public int SupplierAgentId
        {
            get { return supplierAgentId; }
            set { supplierAgentId = value; }
        }

        [DBField("IS_ACTIVE")]
        public int IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        
    }
}
