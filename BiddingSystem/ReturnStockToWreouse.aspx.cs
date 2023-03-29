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
    public partial class ReturnStockToWreouse : System.Web.UI.Page
    {

       // static string UserId = string.Empty;
        static int SubDepartmentID = 0;
       // int CompanyId = 0;
        MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        MRNDIssueNoteControllerInterface mrndinController = ControllerFactory.CreateMRNDIssueNoteController();
        SubDepartmentControllerInterface subdepartmentController = ControllerFactory.CreateSubDepartmentController();
        MrnDetailsStatusLogController mrnDetailsStatusLogController = ControllerFactory.CreateMrnDetailStatusLogController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        InventoryControllerInterface inventoryControllerInterface = ControllerFactory.CreateInventoryController();
        DepartmentReturnController departmentReturnController = ControllerFactory.CreateDepartmentReturnController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefWarehouse";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabWarehouse";
                ((BiddingAdmin)Page.Master).subTabValue = "ReturnStockToWreouse.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ReturnStockToWreouseLink";

               ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                ViewState["UserId"] = Session["UserId"].ToString();

                

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 10, 6) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack)
            {
                if ((Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Count() > 0)) {

                    try {
                        gvDeliveredInventory.DataSource = mrndinController.fetchReturnedStockTowarehouse((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                        gvDeliveredInventory.DataBind();

                        
                    }
                    catch (Exception ex) {
                        throw ex;
                    }

                }

                else {
                    gvDeliveredInventory.DataSource = mrndinController.fetchReturnedStockByCompanyId(int.Parse(Session["CompanyId"].ToString()));
                    gvDeliveredInventory.DataBind();

                }
                gvDepartmetStock.DataSource = departmentReturnController.FetchApprovedStock();
                gvDepartmetStock.DataBind();

            }
        }
        protected void gvDepartmetStock_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvDepartmetStock.PageIndex = e.NewPageIndex;

                gvDepartmetStock.DataSource = departmentReturnController.FetchApprovedStock();
                gvDepartmetStock.DataBind();

            }
            catch (Exception) {

                throw;
            }
        }
        protected void btnMrn_Click(object sender, EventArgs e) {
            int MrnId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[3].Text);

            Response.Redirect("ViewMRNNew.aspx?MrnId=" + MrnId);
        }

        protected void gvDeliveredInventory_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvDeliveredInventory.PageIndex = e.NewPageIndex;
                gvDeliveredInventory.DataSource = mrndinController.fetchReturnedStockTowarehouse((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                gvDeliveredInventory.DataBind();

            }
            catch (Exception) {

                throw;
            }
        }
        protected void btnPrintDepartmentReturn_Click(object sender, EventArgs e) {
             ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { var printWindow = window.open('PrintDepartmentReturn.aspx'); printWindow.print(); printWindow.onafterprint = window.close; });   </script>", false);
        }
        protected void btnReturnMRN_Click(object sender, EventArgs e) {
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { var printWindow = window.open('PrintMRNReturn.aspx'); printWindow.print(); printWindow.onafterprint = window.close; });   </script>", false);
        }
    }
}