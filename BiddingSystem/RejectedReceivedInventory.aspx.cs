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
    public partial class RejectedReceivedInventory : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "RejectedReceivedInventory.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "RejectedReceivedInventoryLink";

               ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                ViewState["UserId"] = Session["UserId"].ToString();

                

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 12, 10) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                        gvDeliveredInventory.DataSource = mrndinController.fetchRejectedMrndINListToConfirm((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                        gvDeliveredInventory.DataBind();

                        
                    }
                    catch (Exception ex) {
                        throw ex;
                    }

                }

                else {
                    gvDeliveredInventory.DataSource = mrndinController.fetchRejectedMrndINListToConfirmByCompanyId(int.Parse(Session["CompanyId"].ToString()));
                    gvDeliveredInventory.DataBind();

                }

                
            }
        }

        protected void btnMrn_Click(object sender, EventArgs e) {
            int MrnId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[3].Text);

            Response.Redirect("ViewMRNNew.aspx?MrnId=" + MrnId);
        }

        protected void btnReturnStock_Click(object sender, EventArgs e) {
            int MrndInID = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            int MrndID = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[1].Text);
            int ItemID = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[2].Text);
            int MrnID = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[3].Text);
            int WarehouseID = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[15].Text);

            int StockMaintainingType = addItemController.GetStockMaintaininType(ItemID, int.Parse(Session["CompanyId"].ToString()));
            int ReturnStock = ControllerFactory.CreateMRNDIssueNoteController().updateIssueNoteForApproval(MrndInID);
            //int ReturnStock = inventoryControllerInterface.ReturnStock(MrndInID, ItemID, WarehouseID, StockMaintainingType, int.Parse(Session["UserId"].ToString()));
            if (ReturnStock > 0) {
                mrnDetailsStatusLogController.InsertStockReturnForApproval(MrndID, int.Parse(Session["UserId"].ToString()));
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Successfully returned stock', showConfirmButton: false,timer: 1500}); });   </script>", false);
                if ((Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Count() > 0)) {

                    try {
                        gvDeliveredInventory.DataSource = mrndinController.fetchRejectedMrndINListToConfirm((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                        gvDeliveredInventory.DataBind();
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }

                else {
                    gvDeliveredInventory.DataSource = mrndinController.fetchRejectedMrndINListToConfirmByCompanyId(int.Parse(Session["CompanyId"].ToString()));
                    gvDeliveredInventory.DataBind();
                }

            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Return Stock failed', showConfirmButton: true,timer: 4000}); });   </script>", false);

            }

        }
        protected void gvDeliveredInventory_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvDeliveredInventory.PageIndex = e.NewPageIndex;
                if ((Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Count() > 0)) {

                    try {
                        gvDeliveredInventory.DataSource = mrndinController.fetchRejectedMrndINListToConfirm((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                        gvDeliveredInventory.DataBind();


                    }
                    catch (Exception ex) {
                        throw ex;
                    }

                }

                else {
                    gvDeliveredInventory.DataSource = mrndinController.fetchRejectedMrndINListToConfirmByCompanyId(int.Parse(Session["CompanyId"].ToString()));
                    gvDeliveredInventory.DataBind();

                }


            }
            catch (Exception) {

                throw;
            }
        }

    }
}