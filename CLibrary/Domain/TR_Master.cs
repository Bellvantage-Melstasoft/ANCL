using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class TR_Master {

        [DBField("TR_ID")]
        public int TRId { get; set; }

        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }

        [DBField("FROM_WAREHOUSE_ID")]
        public int FromWarehouseId { get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDatetime { get; set; }

        [DBField("DESCRIPTION")]
        public string Description { get; set; }

        [DBField("EXPECTED_DATE")]
        public DateTime ExpectedDate { get; set; }

        [DBField("CREATED_BY")]
        public int CreatedBy { get; set; }

        [DBField("STATUS")]
        public int Status { get; set; }

        [DBField("IS_APPROVED")]
        public int IsApproved { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("TR_CODE")]
        public int TrCode { get; set; }

        [DBField("APPROVED_BY")]
        public int ApprovedBy { get; set; }

        [DBField("APPROVED_DATE")]
        public DateTime ApprovedDate { get; set; }

        [DBField("IS_TERMINATED")]
        public int IsTerminated { get; set; }

        [DBField("TERMINATED_BY")]
        public int TerminatedBy { get; set; }

        [DBField("TO_WAREHOUSE_ID")]
        public int ToWarehouseId { get; set; }

        [DBField("TERMINATED_DATE")]
        public DateTime TerminatedDate { get; set; }

        [DBField("TERMINATED_REASON")]
        public string TerminatedReason { get; set; }

        [DBField("APPROVAL_REMARKS")]
        public string ApprovalRemarks { get; set; }

        [DBField("FROM_WAREHOUSE_NAME")]
        public string FromWarehouse { get; set; }

        [DBField("TO_WAREHOUSE_NAME")]
        public string ToWarehouse { get; set; }

        [DBField("CREATED_BY_NAME")]
        public string CreatedByName { get; set; }

        [DBField("FROM_LOCATION")]
        public string FromLocation { get; set; }

        [DBField("TO_LOCATION")]
        public string ToLocation { get; set; }

        [DBField("TO_WAREHOUSE_ADDRESS")]
        public string ToWarehouseAddress { get; set; }

        [DBField("FROM_WAREHOUSE_ADDRESS")]
        public string FromWarehouseAddress { get; set; }

        [DBField("FROM_WAREHOUSE_PNO")]
        public string FromWarehousePNo { get; set; }

        [DBField("TO_WAREHOUSE_PNO")]
        public string ToWarehousePNo { get; set; }

        [DBField("APPROVED_SIGNATURE")]
        public string ApprovedSignature { get; set; }

        [DBField("CREATED_SIGNATURE")]
        public string CreatedSignature { get; set; }

        [DBField("APPROVED_BY_NAME")]
        public string ApprovedByName { get; set; }

        [DBField("ITEM_COUNT")]
        public int ItemCount { get; set; }

        [DBField("TERMINATED_BY_NAME")]
        public string TerminatedByName { get; set; }

        [DBField("TERMINATED_BY_SIGNATURE")]
        public string TerminatedBySignature { get; set; }

        [DBField("CATEGORT_ID")]
        public int MainCategoryId { get; set; }

        [DBField("CATEGORY_NAME")]
        public string MainCategoryName { get; set; }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }

        public List<TR_Details> TRDetails { get; set; }

    }
}
