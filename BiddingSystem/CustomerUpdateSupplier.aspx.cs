using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class CustomerUpdateSupplier : System.Web.UI.Page
    {
        int CompanyId = 0;
        static int supplierId = 0;
        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        List<Supplier> getSupplierList = new List<Supplier>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["CompanyId"].ToString() != "")
            {
                CompanyId = int.Parse(Session["CompanyId"].ToString());
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }


            if (!IsPostBack)
            {
                getSupplierList = supplierController.GetSupplierList();
                if (getSupplierList.Count() > 0)
                {
                    gvSupplierList.DataSource = getSupplierList;
                    gvSupplierList.DataBind();
                }
            }
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                supplierId =int.Parse( gvSupplierList.Rows[x].Cells[0].Text);
                Session["supplierId"] = supplierId;
                Response.Redirect("CompanyUpdatingAndRatingSupplier.aspx");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}