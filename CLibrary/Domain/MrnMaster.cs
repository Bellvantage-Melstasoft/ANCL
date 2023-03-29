using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain
{
    public class MrnMaster
    {
        private int mrnID, subDepartmentID, createdBy, status,isApproved,companyID,isActive, storekeeperid,prtype, itemCategoryId;
        private DateTime createdDate, expectedDate;
        private string description,subDepartmentName,createdByName, fullname;

        public List<MrnDetails> mrnDetails = new List<MrnDetails>();

        [DBField("MRN_ID")]
        public int MrnID
        {
            get
            {
                return mrnID;
            }

            set
            {
                mrnID = value;
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

        [DBField("CREATED_BY")]
        public int CreatedBy
        {
            get
            {
                return createdBy;
            }

            set
            {
                createdBy = value;
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

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }

            set
            {
                createdDate = value;
            }
        }

        public List<MrnDetails> MrnDetails
        {
            get
            {
                return mrnDetails;
            }

            set
            {
                mrnDetails = value;
            }
        }

        [DBField("QUOTATION_FOR")]
        public string QuotationFor { get; set; }

        [DBField("IS_APPROVED")]
        public int IsApproved
        {
            get
            {
                return isApproved;
            }

            set
            {
                isApproved = value;
            }
        }

        [DBField("COMPANY_ID")]
        public int CompanyID
        {
            get
            {
                return companyID;
            }

            set
            {
                companyID = value;
            }
        }

        [DBField("EXPECTED_DATE")]
        public DateTime ExpectedDate
        {
            get
            {
                return expectedDate;
            }

            set
            {
                expectedDate = value;
            }
        }

        [DBField("DESCRIPTION")]
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        [DBField("IS_ACTIVE")]
        public int IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
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

        [DBField("USER_NAME")]
        public string CreatedByName
        {
            get
            {
                return createdByName;
            }

            set
            {
                createdByName = value;
            }
        }

        [DBField("STORE_KEEPER_ID")]
        public int StoreKeeperId
        {
            get
            {
                return storekeeperid;
            }

            set
            {
                storekeeperid = value;
            }
        }

        [DBField("PR_TYPE_ID")]
        public int MrntypeId
        {
            get
            {
                return prtype;
            }

            set
            {
                prtype = value;
            }
        }

        [DBField("ITEM_CATEGORY_ID")]
        public int ItemCatrgoryId
        {
            get
            {
                return itemCategoryId;
            }

            set
            {
                itemCategoryId = value;
            }
        }

        [DBField("FIRST_NAME")]
        public string Fullname
        {
            get
            {
                return fullname;
            }

            set
            {
                fullname = value;
            }
        }
        [DBField("MRN_CODE")]
        public string MrnCode { get; set; }

        [DBField("LOCATION")]
        public string Location { get; set; }

        [DBField("REQUIRED_FOR")]
        public string RequiredFor { get; set; }

        [DBField("STATUS_CODE")]
        public string StatusName { get; set; }

        [DBField("PURCHASE_TYPE")]
        public int PurchaseType { get; set; }

        [DBField("IMPORT_ITEM_TYPE")]
        public int ImportItemType { get; set; }

        [DBField("SPARE_PART_NUMBER")]
        public string SparePartNo { get; set; }


        // For MR History Details
        public PR_Master prMaster { get; set; }
    }
}
