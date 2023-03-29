using CLibrary.Common;
using System;
using System.Collections.Generic;

namespace CLibrary.Domain {
    public class PhysicalstockVerificationMaster {
        [DBField("PSVM_ID")]
        public int PSVMId { get; set; }

        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }

        [DBField("MONTH")]
        public string Month { get; set; }

        [DBField("CREATED_BY")]
        public int CreatedBy { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("APPROVAL_STATUS")]
        public int Approvalstatus { get; set; }

        [DBField("APPROVED_BY")]
        public int ApprovedBy { get; set; }

        [DBField("APPROVED_DATE")]
        public DateTime ApprovedDate { get; set; }

        [DBField("APPROVAL_REMARKS")]
        public string ApprovalRemarks { get; set; }

        [DBField("CREATED_USER_NAME")]
        public string CreatedByname { get; set; }

        [DBField("APPROVED_USER_NAME")]
        public string ApprovedByname { get; set; }


        [DBField("APPROVED_SIGNATURE")]
        public string ApprovedSignature { get; set; }

        [DBField("CREATED_SIGNATURE")]
        public string CreatedSignature { get; set; }

        public List<physicalStockVerificationDetails> PSVDetails { get; set; }


    }
}
