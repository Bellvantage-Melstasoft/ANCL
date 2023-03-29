using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Common
{
    public static class Utils
    {
        static SubDepartmentControllerInterface subDepartmentControllerInterface = ControllerFactory.CreateSubDepartmentController();
        static ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        static SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();


        public static List<SubDepartment> getallDepartments(int CompanyId)
        {
            return subDepartmentControllerInterface.getDepartmentList(CompanyId);
             
        }

        public static List<ItemCategory> getallCtegories(int CompanyId)
        {
            return itemCategoryController.FetchItemCategoryList(CompanyId);

        }

        public static List<SupplierAssignedToCompany> getallSuppliers(int CompanyId)
        {
            return supplierAssigneToCompanyController.GetSupplierRequestsByCompanyId(CompanyId);

        }
    }
}
