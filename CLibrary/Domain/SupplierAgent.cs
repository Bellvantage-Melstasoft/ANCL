using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
   public class SupplierAgent
    {
        private int supplierId;
        private int agentId;
        private string agentName;
        private string address;
        private string email;
        private string contactNo;

        [DBField("SUPPLIER_ID")]
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        [DBField("AGENT_ID")]
        public int AgentId
        {
            get { return agentId; }
            set { agentId = value; }
        }

        [DBField("AGENT_NAME")]
        public string AgentName
        {
            get { return agentName; }
            set { agentName = value; }
        }

        [DBField("ADDRESS")]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        [DBField("CONTACT_NO")]
        public string ContactNo
        {
            get { return contactNo; }
            set { contactNo = value; }
        }

        [DBField("EMAIL")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        
    
    }
}
