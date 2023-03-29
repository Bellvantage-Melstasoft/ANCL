using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Helper;

namespace CLibrary.Domain
{
    public class MRNDIssueNote
    {
        private int mrndInID, mrndID, itemID, warehouseID,  issuedBy,deliveredBy, receivedBy, status,subDepartmentID, mrnID;
        public decimal issuedQty, stockValue;
        private DateTime issuedOn,deliveredOn, receivedOn;
        private string warehouseLocation,subDepartmentName,itemName,deliveredUser,receivedUser,issuedUser, measurementshortName, mrnCode;

        [DBField("MRND_IN_ID")]
        public int MrndInID
        {
            get
            {
                return mrndInID;
            }

            set
            {
                mrndInID = value;
            }
        }

        [DBField("MRND_ID")]
        public int MrndID
        {
            get
            {
                return mrndID;
            }

            set
            {
                mrndID = value;
            }
        }

        [DBField("MRN_ID")]
        public int MrnID {
            get {
                return mrnID;
            }

            set {
                mrnID = value;
            }
        }

        [DBField("ITEM_ID")]
        public int ItemID
        {
            get
            {
                return itemID;
            }

            set
            {
                itemID = value;
            }
        }

        [DBField("WAREHOUSE_ID")]
        public int WarehouseID
        {
            get
            {
                return warehouseID;
            }

            set
            {
                warehouseID = value;
            }
        }

        [DBField("ISSUED_QTY")]
        public decimal IssuedQty
        {
            get
            {
                return issuedQty;
            }

            set
            {
                issuedQty = value;
            }
        }

        [DBField("MRN_CODE")]
        public string MrnCode {
            get {
                return mrnCode;
            }

            set {
                mrnCode = value;
            }
        }

        [DBField("ISSUED_BY")]
        public int IssuedBy
        {
            get
            {
                return issuedBy;
            }

            set
            {
                issuedBy = value;
            }
        }

        [DBField("RECEIVED_BY")]
        public int ReceivedBy
        {
            get
            {
                return receivedBy;
            }

            set
            {
                receivedBy = value;
            }
        }

        [DBField("STATUS")]
        public int Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        [DBField("ISSUED_ON")]
        public DateTime IssuedOn
        {
            get
            {
                return issuedOn;
            }

            set
            {
                issuedOn = value;
            }
        }

        [DBField("RECEIVED_ON")]
        public DateTime ReceivedOn
        {
            get
            {
                return receivedOn;
            }

            set
            {
                receivedOn = value;
            }
        }

        [DBField("LOCATION")]
        public string WarehouseLocation
        {
            get
            {
                return warehouseLocation;
            }

            set
            {
                warehouseLocation = value;
            }
        }

        [DBField("SUB_DEPARTMENT_ID")]
        public int SubDepartmentID
        {
            get
            {
                return subDepartmentID;
            }

            set
            {
                subDepartmentID = value;
            }
        }

        [DBField("DEPARTMENT_NAME")]
        public string SubDepartmentName
        {
            get
            {
                return subDepartmentName;
            }

            set
            {
                subDepartmentName = value;
            }
        }

        [DBField("DELIVERED_BY")]
        public int DeliveredBy
        {
            get
            {
                return deliveredBy;
            }

            set
            {
                deliveredBy = value;
            }
        }

        [DBField("DELIVERED_ON")]
        public DateTime DeliveredOn
        {
            get
            {
                return deliveredOn;
            }

            set
            {
                deliveredOn = value;
            }
        }

        [DBField("ITEM_NAME")]
        public string ItemName
        {
            get
            {
                return itemName;
            }

            set
            {
                itemName = value;
            }
        }

        [DBField("DELIVERED_USER")]
        public string DeliveredUser
        {
            get
            {
                return deliveredUser;
            }

            set
            {
                deliveredUser = value;
            }
        }

        [DBField("RECEIVED_USER")]
        public string ReceivedUser
        {
            get
            {
                return receivedUser;
            }

            set
            {
                receivedUser = value;
            }
        }

        

        [DBField("ISSUED_USER")]
        public string IssuedUser
        {
            get
            {
                return issuedUser;
            }

            set
            {
                issuedUser = value;
            }
        }
        [DBField("ISSUED_STOCK_VALUE")]
        public decimal StockValue
        {
            get
            {
                return stockValue;
            }

            set
            {
                stockValue = value;
            }
        }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string measurementShortName {
            get {
                return measurementshortName;
            }

            set {
                measurementshortName = value;
            }
        }

        [DBField("ISSUED_STOCK_VALUE")]
        public decimal IssuedStockValue { get; set; }


        [DBField("STOCK_MAINTAINING_TYPE")]
        public int StockMaintainingType { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int MeasurementId { get; set; }

        public int RequestedMeasurementId { get; set; }

        public List<IssuedInventoryBatches> IssuedBatches { get; set; }
        public List<MrndIssueNoteBatches> MrndIssueNoteBatches { get; set; }

        
             [DBField("SHORT_CODE")]
        public string ShortCode { get; set; }

        [DBField("USER_NAME")]
        public string UserName { get; set; }

        [DBField("STOCK_VALUE")]
        public decimal StValue { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        [DBField("RECEIVE_CONFIRMD_BY")]
        public int ConfirmReceive { get; set; }

        [DBField("CONFIRMED_USER")]
        public string ConfirmedUser { get; set; }

        [DBField("RECEIVE_CONFIRMED_ON")]
        public DateTime ConfirmedOn { get; set; }

        [DBField("REJECTED_USER")]
        public string RejectedUser { get; set; }

        [DBField("REJECTED_ON")]
        public DateTime RejectedOn { get; set; }

        [DBField("REJECTED_BY")]
        public int RejectedBy { get; set; }

        [DBField("RETURN_QTY")]
        public decimal ReturnQty { get; set; }

        
    }
}
