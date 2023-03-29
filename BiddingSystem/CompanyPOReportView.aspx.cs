using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class CompanyPOReportView : System.Web.UI.Page
    {
        static string UserId = string.Empty;
        int CompanyId = 0;
        int poId = 0;
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();

                ((BiddingAdmin)Page.Master).mainTabValue = "hrefReports";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabReports";
                //((BiddingAdmin)Page.Master).subTabValue = "CompanyPoReports.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "poReportsLink";

                if (Request.QueryString["PoId"] != null)
                {
                    poId = int.Parse(Request.QueryString["PoId"].ToString());
                }
                else
                {
                    Response.Redirect("CompanyPoReports.aspx");
                }
               
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

            lblDateNow.Text = LocalTime.Today.ToString("dd-MM-yyyy");
         

            if(!IsPostBack){
                try
                {

                    POMaster pOMaster = pOMasterController.GetPoMasterObjByPoId(poId);
                    lblPOCode.Text = pOMaster.POCode;
                    lblSupplierName.Text = pOMaster._Supplier.SupplierName; ;
                    lblAddress.Text = pOMaster._Supplier.Address1 + "," + pOMaster._Supplier.Address2;
                    lblCompanyName.Text = pOMaster._companyDepartment.DepartmentName;
                    lblVatNo.Text = pOMaster._companyDepartment.VatNo;
                    lblPhoneNo.Text = pOMaster._companyDepartment.PhoneNO;
                    lblFaxNo.Text = pOMaster._companyDepartment.FaxNO;
                    lblSubtotal.Text = pOMaster.TotalAmount.ToString("n");
                    lblVatTotal.Text = pOMaster.VatAmount.ToString("n");
                    lblNbtTotal.Text = pOMaster.NBTAmount.ToString("n");
                    lblTotal.Text = pOMaster.TotalAmount.ToString("n");
                    gvPurchaseOrderItems.DataSource = pOMaster._PODetails;
                    gvPurchaseOrderItems.DataBind();

                    gvPurchaseOrderItems.DataSource = pOMaster._PODetails;
                    gvPurchaseOrderItems.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }
        
    }
}