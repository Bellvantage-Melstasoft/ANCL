using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    [Serializable]
    public class MrnDetailsV2
    {

        private int measurementId;

        [DBField("MRND_ID")]
        public int MrndId { get; set; }
        [DBField("MRN_ID")]
        public int MrnId { get; set; }
        [DBField("ITEM_ID")]
        public int ItemId { get; set; }
        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }
        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }
        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }
        [DBField("DESCRIPTION")]
        public string Description { get; set; }
        [DBField("ESTIMATED_AMOUNT")]
        public decimal EstimatedAmount { get; set; }
        [DBField("REQUESTED_QTY")]
        public decimal RequestedQty { get; set; }
        [DBField("SHORT_CODE")]
        public string MeasurementShortName { get; set; }
        [DBField("DEPARTMENT_STOCK")]
        public decimal DepartmentStock { get; set; }
        [DBField("FILE_SAMPLE_PROVIDED")]
        public int FileSampleProvided { get; set; }
        [DBField("REPLACEMENT")]
        public int Replacement { get; set; }
        [DBField("REMARKS")]
        public string Remarks { get; set; }
        [DBField("ISSUED_QTY")]
        public decimal IssuedQty { get; set; }
        [DBField("RECEIVED_QTY")]
        public decimal ReceivedQty { get; set; }
        [DBField("IS_TERMINATED")]
        public int IsTerminated { get; set; }
        [DBField("TERMINATED_BY")]
        public int TerminatedBy { get; set; }
        [DBField("TERMINATED_BY_NAME")]
        public string TerminatedByName { get; set; }
        [DBField("TERMINATED_ON")]
        public DateTime TerminatedOn { get; set; }
        [DBField("TERMINATION_REMARKS")]
        public string TerminationRemarks { get; set; }
        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }
        [DBField("STATUS")]
        public int Status { get; set; }
        [DBField("MEASUREMENT_ID")]
        public int DetailId { get; set; }
        [DBField("SHORT_CODE")]
        public string MeasurementShortCode { get; set; }

        [DBField("STATUS_NAME")]
        public string StatusName { get; set; }




        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }
        [DBField("MEASUREMENT_ID")]
        public int MeasurementId
        {
            get
            {
                return measurementId;
            }

            set
            {
                measurementId = value;
            }
        }
        [DBField("STOCK_MAINTAINING_TYPE")]
        public int StockMaintainingType { get; set; }
        /// <summary>
        /// 0 = Do Nothing,
        /// 1 = Insert,
        /// 2 = Update,
        /// 3 = Delete
        /// </summary>
        public int Todo { get; set; }



        public List<MrnBomV2> MrnBoms { get; set; }
        public List<MrnFileUploadV2> MrnFileUploads { get; set; }
        public List<MrnReplacementFileUploadV2> MrnReplacementFileUploads { get; set; }
        public List<MrnSupportiveDocumentV2> MrnSupportiveDocuments { get; set; }


        public decimal AvailableQty { get; set; }

        [DBField("WAREHOUSE_QTY")]
        public decimal WarehouseAvailableQty { get; set; }

        [DBField("ITEM_UNIT")]
        public string ItemUnit { get; set; }


        [DBField("SPARE_PART_NUMBER")]
        public string SparePartNo { get; set; }

        [DBField("PURCHASE_TYPE")]
        public int PurchaseType { get; set; }

        [DBField("IMPORT_ITEM_TYPE")]
        public int ImportItemType { get; set; }

    }
}
