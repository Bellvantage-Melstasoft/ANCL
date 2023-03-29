using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class PrDetailsV2 {
        [DBField("PRD_ID")]
        public int PrdId { get; set; }
        [DBField("PR_ID")]
        public int PrId { get; set; }
        [DBField("ITEM_ID")]
        public int ItemId { get; set; }
        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }
        [DBField("DESCRIPTION")]
        public string Description { get; set; }
        [DBField("ESTIMATED_AMOUNT")]
        public decimal EstimatedAmount { get; set; }
        [DBField("REQUESTED_QTY")]
        public decimal RequestedQty { get; set; }

        [DBField("ISSUED_QTY")]
        public decimal IssuedQty { get; set; }

        [DBField("RECEIVED_QTY")]
        public decimal ReceivedQty { get; set; }
        [DBField("MEASUREMENT_SHORT_NAME")]
        public string MeasurementShortName { get; set; }

        [DBField("WAREHOUSE_STOCK")]
        public decimal WarehouseStock { get; set; }
        [DBField("FILE_SAMPLE_PROVIDED")]
        public int FileSampleProvided { get; set; }
        [DBField("REPLACEMENT")]
        public int Replacement { get; set; }
        [DBField("REMARKS")]
        public string Remarks { get; set; }
        [DBField("EXPENSE_TYPE")]
        public int ExpenseType { get; set; }
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
       
        [DBField("EXPECTED_DATE")]
        public DateTime ExpectedDate { get; set; }
        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }
        [DBField("SUBMIT_FOR_BID")]
        public int SubmitForBid { get; set; }
        [DBField("MRND_ID")]
        public int MrndId { get; set; }
        [DBField("CURRENT_STATUS")]
        public int CurrentStatus { get; set; }
        [DBField("MEASUREMENT_ID")]
        public int DetailId { get; set; }
        #region Required When Joining Tables
        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }
        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }
        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }
        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }
        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDateTime { get; set; }
        [DBField("CREATED_BY")]
        public int CreatedBy { get; set; }
        [DBField("UPDATED_DATETIME")]
        public DateTime UpdatedDateTime { get; set; }
        [DBField("UPDATED_BY")]
        public string UpdatedBy { get; set; }
        [DBField("REQUIRED_FOR")]
        public string RequiredFor { get; set; }
        #endregion

        /// <summary>
        /// 0 = Do Nothing,
        /// 1 = Insert,
        /// 2 = Update,
        /// 3 = Delete
        /// </summary>
        public int Todo { get; set; }

        [DBField("STATUS")]
        public int Status { get; set; }

        public List<PrBomV2> PrBoms { get; set; }
        public List<PrFileUploadV2> PrFileUploads { get; set; }
        public List<PrReplacementFileUploadV2> PrReplacementFileUploads { get; set; }
        public List<PrSupportiveDocumentV2> PrSupportiveDocuments { get; set; }
        public List<PRDetailsStatusLog> PrDetailsStatusLogs { get; set; }

        //helpers
        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }


        [DBField("WAREHOUSE_UNIT")]
        public string WarehouseUnit { get; set; }

        [DBField("AVAILABLE_QTY")]
        public decimal AvailableQty { get; set; }

        [DBField("STATUS_NAME")]
        public string StatusName { get; set; }
        

        [DBField("DETAIL_ID_ITEM")]
        public int DetailIdWHItem { get; set; }

        [DBField("QTY")]
        public decimal Qty { get; set; }

        [DBField("SHORT_CODE")]
        public string ShortCode { get; set; }

        [DBField("PR_CODE")]
        public int PrCode { get; set; }

        [DBField("STOCK_MAINTAINING_TYPE")]
        public int StockMaintainingType { get; set; }


        public Inventory AvailableWarehouseStock { get; set; }

        [DBField("IS_PO_RAISED")]
        public int IsPORaised { get; set; }

        [DBField("SPARE_PART_NUMBER")]
        public string SparePartNo { get; set; }

       
        [DBField("PURCHASE_TYPE")]
        public int PurchaseType { get; set; }

        [DBField("IMPORT_ITEM_TYPE")]
        public int ImportItemType { get; set; }
    }
}
