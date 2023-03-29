using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using System.Web.Script.Serialization;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class BiddingSupplier : System.Web.UI.MasterPage
    {
        public int supplierId = 0;
        public string PendingBidCount = string.Empty;
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        SupplierCategoryController supplierCategoryController = ControllerFactory.CreatesupplierCategoryController();
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        SupplierController supplierController = ControllerFactory.CreateSupplierController();

        public List<string> supplierMainCategoryList = new List<string>();
        public List<string> supplierMainCategoryListWithSubCategory = new List<string>();
        public List<string> supplierAssignedCompany = new List<string>();

        public string SupplierName = string.Empty;
        public string SupplierLogo = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["supplierId"] != null && Session["supplierId"].ToString() != "")
            {
                supplierId = int.Parse(Session["supplierId"].ToString());
                Supplier supplierDetails = supplierController.GetSupplierBySupplierId(supplierId);
                SupplierName =  supplierDetails.SupplierName;
                SupplierLogo = supplierDetails.SupplierLogo;
            }
            else
            {
                Response.Redirect("LoginPageSupplier.aspx");
            }

            if(!IsPostBack){
                try
                {
                    PendingBidCount = supplierQuotationController.GetSupplierPendingBids(supplierId).Count.ToString();
                    List<SupplierCategory> _supplierCategory = supplierCategoryController.GetSupplierCategoryWithCategoryName(supplierId);

                    foreach (var item in _supplierCategory)
                    {
                        supplierMainCategoryList.Add(item.CategoryId +"-"+ item.CategoryName +"-"+ item.SupplierId);
                    }

                    List<SupplierCategory> _supplierCategoryAndSubCategory = supplierCategoryController.GetSupplierCategoryAndSubCategory(supplierId);

                    foreach (var itemWithSub in _supplierCategoryAndSubCategory)
                    {
                        supplierMainCategoryListWithSubCategory.Add(itemWithSub.CategoryId +"-" + itemWithSub.SubCategoryId + "-" + itemWithSub.SubCategoryName);
                    }

                    List<SupplierAssignedToCompany> supplierAssigneToCompany = supplierAssigneToCompanyController.GetSupplierAssignedCompanies(supplierId);

                    foreach (var itemCompany in supplierAssigneToCompany)
                    {
                        supplierAssignedCompany.Add(itemCompany.DepartmentID +"-"+ itemCompany.DepartmentName+"-"+ itemCompany.ImagePath);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else{
            
            }
        }

        public string getJsonSupplierName()
        {
            var supName = SupplierName;
            return (new JavaScriptSerializer()).Serialize(supName);
        }

        public string getJsonSupplierLogo()
        {
            var supLogo = SupplierLogo;
            return (new JavaScriptSerializer()).Serialize(supLogo);
        }

        public string getJsonPendingBidCount()
        {
            var PendingCount = PendingBidCount;
            return (new JavaScriptSerializer()).Serialize(PendingCount);
        }

        public string getJsonSupplierMainCategory()
        {
            var SupplierMainCategory = supplierMainCategoryList;
            return (new JavaScriptSerializer()).Serialize(SupplierMainCategory);
        }

        public string getJsonSupplierMainCategoryAndSubCat()
        {
            var SupplierMainCategorySub = supplierMainCategoryListWithSubCategory;
            return (new JavaScriptSerializer()).Serialize(SupplierMainCategorySub);
        }

        public string getJsonComapanyList()
        {
            var SupplierCompanyList = supplierAssignedCompany;
            return (new JavaScriptSerializer()).Serialize(SupplierCompanyList);
        }
    }
}