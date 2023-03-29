
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class SupplierLoadingWebUIInner : System.Web.UI.MasterPage
    {
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        SupplierLoginController supplierLoginController = ControllerFactory.CreateSupplierLoginController();
        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemCategoryMasterController itemCategoryMasterController = ControllerFactory.CreateItemCategoryMasterController();
        int supplierId = 0;
        public List<string> CategoryListStr = new List<string>();
        public string SupplierName = string.Empty;
        public string SupplierLogo = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["supplierId"] != null && Session["supplierId"].ToString() != "")
            {
                supplierId = int.Parse(Session["supplierId"].ToString());
                Supplier supplierDetails = supplierController.GetSupplierBySupplierId(supplierId);
                SupplierName = supplierDetails.SupplierName;
                SupplierLogo = supplierDetails.SupplierLogo;

            }
            else
            {
                Response.Redirect("SupplierInitialFrontView.aspx");
            }

            if (!IsPostBack)
            {
                LoadCategory();
            }

            else { 
            
            }
        }

        private void LoadCategory()
        {
            List<ItemCategoryMaster> itemCategory = new List<ItemCategoryMaster>();
            itemCategory = itemCategoryMasterController.FetchItemCategoryList().Where(x => x.IsActive == 1).ToList();

            foreach (var item in itemCategory)
            {
                CategoryListStr.Add(item.CategoryId + "-" + item.CategoryName);
            }
        }

        public string getJsonReceived()
        {
            var DataListCategorySet = CategoryListStr;
            return (new JavaScriptSerializer()).Serialize(DataListCategorySet);
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
    }
}