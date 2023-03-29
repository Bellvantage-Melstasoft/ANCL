using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain
{
    public class MrnDetails
    {
        [DBField("MRND_ID")]
        public int Mrnd_ID { get; set; }

        [DBField("MRN_ID")]
        public int MrnId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("DESCRIPTION")]
        public string Description { get; set; }

        [DBField("REQUESTED_QTY")]
        public int RequestedQty { get; set; }

        [DBField("ISSUED_QTY")]
        public int IssuesQty { get; set; }

        [DBField("RECEIVED_QTY")]
        public int ReceivedQty { get; set; }

        [DBField("A_QTY")]
        public int AvailableQty { get; set; }

        [DBField("STATUS")]
        public int Status { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("UNIT")]
        public int Unit { get; set; }

        [DBField("REPLACEMENT")]
        public int Replacement { get; set; }

        [DBField("PURPOSE")]
        public string Purpose { get; set; }

        [DBField("ESTIMATED_AMOUNT")]
        public decimal EstimatedAmount { get; set; }

        [DBField("BID_TYPE_MANUAL_BID")]
        public int BidTypeManualBid { get; set; }

        [DBField("SAMPLE_PROVIDED")]
        public int SampleProvided { get; set; }

        [DBField("REMARKS")]
        public string Remarks { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int MeasurementId { get; set; }

        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryID { get; set; }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryID { get; set; }

        [DBField("UNIT_PRICE")]
        public decimal UnitPrice { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }



        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }


        [DBField("CATEGORY_ID")]
        public int MainCategoryId { get; set; }

        [DBField("CATEGORY_NAME")]
        public string MainCategoryName { get; set; }


        [DBField("ITEM_QUANTITY")]
        public decimal ItemQuantity { get; set; }

        [DBField("ITEM_DESCRIPTION")]
        public string ItemDescription { get; set; }

        [DBField("PR_TYPE_ID")]
        public int MrntypeId { get; set; }

        public List<BiddingItem> BiddingItems { get; set; }
        public MRN_Master mrn_Master { get; set; }
        public List<MRNBom> Boms { get; set; }
      //ublic List<PR_Replace_FileUpload> ReplacementImages { get; set; }
        public List<MRNSupportiveDocument> SupportiveDocs { get; set; }
        public List<MRNFileUpload> StandardImages { get; set; }
        public List<MRNDetailsStatusLog> MrnDetailsStatusLogs { get;  set; }
        public MRNStockDepartment mrnStockDepartment { get; set; }

        [DBField("SPARE_PART_NUMBER")]
        public string SparePartNo { get; set; }
    }
}