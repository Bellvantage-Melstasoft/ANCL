using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class SubDepartment
    {
        private int subDepartmentID, isActive, companyID,headOfDepartmentID;
        private string subDepartmentName, phoneNo,headOfDepartmentName;

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


        [DBField("PHONE_NO")]
        public string PhoneNo
        {
            get
            {
                return phoneNo;
            }

            set
            {
                phoneNo = value;
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

        [DBField("HEAD_OF_DEPARTMENT")]
        public int HeadOfDepartmentID
        {
            get
            {
                return headOfDepartmentID;
            }

            set
            {
                headOfDepartmentID = value;
            }
        }

        [DBField("USER_NAME")]
        public string HeadOfDepartmentName
        {
            get
            {
                return headOfDepartmentName;
            }

            set
            {
                headOfDepartmentName = value;
            }
        }
    }
}
