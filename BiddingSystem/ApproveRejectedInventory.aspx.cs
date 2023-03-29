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
    public partial class ApproveRejectedInventory : System.Web.UI.Page
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
                ((BiddingAdmin)Page.Master).subTabValue = "ApproveRejectedInventory.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ApproveRejectedInventoryLink";

               ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                ViewState["UserId"] = Session["UserId"].ToString();

                

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 10, 7) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                
                    try {
                        gvDeliveredInventory.DataSource = mrndinController.fetchRejectedMrndINListToApprove();
                        gvDeliveredInventory.DataBind();

                        gvDepartmetStock.DataSource = departmentReturnController.fetchReturnedStock();
                        gvDepartmetStock.DataBind();


                    }
                    catch (Exception ex) {
                        throw ex;
                    }

                
                    

               

                
            }
        }

        protected void btnMrn_Click(object sender, EventArgs e) {
            int MrnId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[3].Text);

            Response.Redirect("ViewMRNNew.aspx?MrnId=" + MrnId);
        }

        protected void btnDeprtmentReturn_Click(object sender, EventArgs e) {
            int StockMaitainingType = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[5].Text);
            int ItemId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[1].Text);
            int warehouseId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[6].Text);
            decimal returnQty = decimal.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[3].Text);
            decimal ReturnedMasterStock = decimal.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[4].Text);
            int mrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[7].Text);
            int BatchId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[8].Text);
            int DepartmentReturnId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[9].Text);

            if (StockMaitainingType == 1) {
                int result = inventoryControllerInterface.ReturnMasterStockApprove(ItemId, warehouseId, returnQty, ReturnedMasterStock, int.Parse(Session["UserId"].ToString()), DepartmentReturnId);
                if (result > 0) {

                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Successfully Approved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    mrnDetailsStatusLogController.InsertDepartmetStockReturned(mrndId, int.Parse(Session["UserId"].ToString()));

                    gvDepartmetStock.DataSource = departmentReturnController.fetchReturnedStock();
                    gvDepartmetStock.DataBind();
                }
            }
            else {
                int sresult = inventoryControllerInterface.StockReturnApprove(ItemId, warehouseId, int.Parse(Session["UserId"].ToString()), returnQty, BatchId, DepartmentReturnId);
                if (sresult > 0) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Successfully returned stock', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    mrnDetailsStatusLogController.InsertDepartmetStockReturned(mrndId, int.Parse(Session["UserId"].ToString()));

                    gvDepartmetStock.DataSource = departmentReturnController.fetchReturnedStock();
                    gvDepartmetStock.DataBind();
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Return Stock failed', showConfirmButton: true,timer: 4000}); });   </script>", false);

                }
            }
            
        }

        protected void btnReturnStock_Click(object sender, EventArgs e) {
            int MrndInID = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            int MrndID = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[1].Text);
            int ItemID = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[2].Text);
            int MrnID = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[3].Text);
            int WarehouseID = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[15].Text);

            int StockMaintainingType = addItemController.GetStockMaintaininType(ItemID, int.Parse(Session["CompanyId"].ToString()));

            int ReturnStock = inventoryControllerInterface.ReturnStock(MrndInID, ItemID, WarehouseID, StockMaintainingType, int.Parse(Session["UserId"].ToString()));
            if (ReturnStock > 0) {
                mrnDetailsStatusLogController.InsertStockReturned(MrndID, int.Parse(Session["UserId"].ToString()));
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Successfully returned stock', showConfirmButton: false,timer: 1500}); });   </script>", false);
                
                    try {
                        gvDeliveredInventory.DataSource = mrndinController.fetchRejectedMrndINListToApprove();
                        gvDeliveredInventory.DataBind();
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
               

            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Return Stock failed', showConfirmButton: true,timer: 4000}); });   </script>", false);

            }

        }
        protected void gvDeliveredInventory_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvDeliveredInventory.PageIndex = e.NewPageIndex;
                
                    try {
                        gvDeliveredInventory.DataSource = mrndinController.fetchRejectedMrndINListToApprove();
                        gvDeliveredInventory.DataBind();


                    }
                    catch (Exception ex) {
                        throw ex;
                    }

                


            }
            catch (Exception) {

                throw;
            }
        }


        protected void gvDepartmetStock_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvDepartmetStock.PageIndex = e.NewPageIndex;

                gvDepartmetStock.DataSource = departmentReturnController.fetchReturnedStock();
                gvDepartmetStock.DataBind();

            }
            catch (Exception) {

                throw;
            }
        }

    }
}