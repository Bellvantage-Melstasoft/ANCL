using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Controller;

namespace BiddingSystem
{
    public partial class SupplierLoadingWebUI : System.Web.UI.MasterPage
    {
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        SupplierLoginController supplierLoginController = ControllerFactory.CreateSupplierLoginController();
        SupplierController supplierController = ControllerFactory.CreateSupplierController();

        int supplierId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["supplierId"] = null;
        }
    }
}