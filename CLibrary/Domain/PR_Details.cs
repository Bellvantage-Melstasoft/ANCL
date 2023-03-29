using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    [Serializable]
    public class PR_Details
    {
        

        [DBField("PRD_ID")]
        public int PrdId { get; set; }

        [DBField("PR_ID")]
        public int PrId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("UNIT")]
        public int Unit { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("ITEM_DESCRIPTION")]
        public string ItemDescription { get; set; }

        [DBField("DESCRIPTION")]
        public string Description { get; set; }

        [DBField("SHORT_CODE")]
        public string MeasurementShortName { get; set; }

        [DBField("ITEM_UPDATED_BY")]
        public string ItemUpdatedBy { get; set; }

        [DBField("ITEM_UPDATED_DATETIME")]
        public DateTime ItemUpdatedDateTime { get; set; }



        [DBField("REPLACEMENT")]
        public int Replacement { get; set; }

        [DBField("ITEM_QUANTITY")]
        public decimal ItemQuantity { get; set; }

        [DBField("PURPOSE")]
        public string Purpose { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("PR_CODE")]
        public string PrCode { get; set; }

        [DBField("END_DATE")]
        public DateTime EndDate { get; set; }

        [DBField("AMOUNT")]
        public decimal Amount { get; set; }

        [DBField("NBT")]
        public decimal Nbt { get; set; }

        [DBField("VAT")]
        public decimal Vat { get; set; }


        //Added By Nava
        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        //Added By Nava
        [DBField("CATEGORY_ID")]
        public int MainCategoryId { get; set; }

        [DBField("CATEGORY_NAME")]
        public string MainCategoryName { get; set; }

        //Edited 2018-06-20
        [DBField("SUBMIT_FOR_BID")]
        public int SubmitForBid { get; set; }

        [DBField("DEPARTMENT_ID")]
        public int DepartmentId { get; set; }

        [DBField("DATE_OF_REQUEST")]
        public DateTime DateOfRequest { get; set; }

        [DBField("QUOTATION_FOR")]
        public string QuotationFor { get; set; }

        [DBField("OUR_REFERENCE")]
        public string OurReference { get; set; }

        [DBField("REQUESTED_BY")]
        public string RequestedBy { get; set; }

        [DBField("REQUESTED_QTY")]
        public decimal RequestedQty { get; set; }

        [DBField("AVAILABLE_QTY")]
        public decimal AvailableQty { get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDateTime { get; set; }

        [DBField("CREATED_BY")]
        public string CreatedBy { get; set; }

        //Edited 2018-06-21
        [DBField("PR_IS_REJECTED_COUNT")]
        public int PrIsRejectedCount { get; set; }

        //Edited 2018-06-22
        [DBField("IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL")]
        public int IsApproveToViewInSupplierPortal { get; set; }

        [DBField("REASON_TO_REJECT_BID_OPENING")]
        public string ReasonToRejectBidOpening { get; set; }

        [DBField("BIDDING_ORDER_ID")]
        public string BiddingOrderId { get; set; }

        // Edited 2018-06-24
        [DBField("IS_PO_RAISED")]
        public int IsPORaised { get; set; }

        [DBField("IS_PO_REJECTED_COUNT")]
        public int IsPoRejectedCount { get; set; }

        //Edited 2018-06-25
        [DBField("IS_PO_APPROVED")]
        public int IsPOApproved { get; set; }

        //--Changes 2018-08-27
        [DBField("ESTIMATED_AMOUNT")]
        public decimal EstimatedAmount { get; set; }
        public int noOfStanardImages { get; set; }
        public int noOfRepacementImages { get; set; }

        //---Changes 2018-09-18
        [DBField("BID_TYPE_MANUAL_BID")]
        public int BidTypeMaualOrBid { get; set; }


        [DBField("PR_IS_APPROVED_FOR_BID")]
        public int PrIsApprovedForBid { get; set; }

        [DBField("SAMPLE_PROVIDED")]
        public int SampleProvided { get; set; }

        [DBField("REMARKS")]
        public string Remarks { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int MeasurmentId { get; set; }

        [DBField("CURRENT_STATUS")]
        public int CurrentStatus { get; set; }

        
        /// <summary>
        /// changes done by salman on 2019-01-17
        /// </summary>
        public List<BiddingItem> BiddingItems { get; set; }
        public PR_Master PrMaster { get; set; }
        public List<PR_BillOfMeterial> Boms { get; set; }
        public List<PR_Replace_FileUpload> ReplacementImages { get; set; }
        public List<PR_SupportiveDocument> SupportiveDocs { get; set; }
        public List<PR_FileUpload> StandardImages { get; set; }
        public List<PRDetailsStatusLog> PrDetailsStatusLogs { get; set; }
        public PRStockDepartment prStockDepartment { get; set; }
    }
}
