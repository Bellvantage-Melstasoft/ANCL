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
    public partial class PendingManualBid : System.Web.UI.Page
    {
        static string UserId = string.Empty;
        int CompanyId = 0;

        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefManualBids";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabManualBids";
                    ((BiddingAdmin)Page.Master).subTabValue = "PendingManualBids.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "pendingManualBidsidlnk";


                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }


                if (!IsPostBack)
                {
                    GVBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GVBind()
        {
            try
            {
                //---------Submitted Bids
                List<SupplierQuotation> supplierQuotationSubmitted = supplierQuotationController.GetManualPendingBidsAndNotBid().Where(c => c.EndDate > DateTime.Now).OrderBy(x => x.PrID).ToList();
                gvPendingBids.DataSource = supplierQuotationSubmitted;
                gvPendingBids.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}