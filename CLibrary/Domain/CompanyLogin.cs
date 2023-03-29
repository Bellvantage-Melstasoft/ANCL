using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain
{
   public class CompanyLogin
    {

        private int userId;
        private string username;
        private string password, contactNo;
        private string createdBy;
        private DateTime createdDate;
        private string updatedBy;
        private DateTime updatedDate;
        private string usertype;
        private int isActive;
        private int departmentId;
        private string emailAddress;
        private string firstName;
        private string subDepartmentID;
        private string subDepartmentName, employeeNo;
        private int designationId;
        private int warehouseId;


       [DBField("DESIGNATION_ID")]
        public int DesignationId
        {
            get { return designationId; }
            set { designationId = value; }
        }
        
        [DBField("FIRST_NAME")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [DBField("CONTACT_NO")]
        public string ContactNo {
            get { return contactNo; }
            set { contactNo = value; }
        }

        [DBField("EMAIL_ADDRESS")]
        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        [DBField("DEPARTMENT_ID")]
        public int DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value; }
        }


        [DBField("IS_ACTIVE")]
        public int IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        [DBField("EMPLOYEE_NO")]
        public string EmployeeNo
        {
            get { return employeeNo; }
            set { employeeNo = value; }
        }

        [DBField("USER_TYPE")]
        public string Usertype
        {
            get { return usertype; }
            set { usertype = value; }
        }

        [DBField("UPDATED_DATE")]
        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }

        [DBField("UPDATED_BY")]
        public string UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        [DBField("CREATED_BY")]
        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        [DBField("PASSWORD")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [DBField("USER_NAME")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DBField("USER_ID")]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public CompanyDepartment _CompanyDepartment { get; set; }

        [DBField("SUB_DEPARTMENT_ID")]
        public string SubDepartmentID
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

        [DBField("WAREHOUSE_ID")]
        public int WarehouseID
        {
            get
            {
                return warehouseId;
            }

            set
            {
                warehouseId = value;
            }
        }
        public string WarehouseName { get; set; }
        public List<SubDepartment> subDepartment { get; set; }
        public List<Warehouse> warehouse { get; set; }
        public List<UserSubDepartment> DepartmentList { get; set; }
        public List<UserWarehouse> WarehouseList { get; set; }
    }
}
