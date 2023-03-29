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
    public partial class DepartmentReturnApproval : System.Web.UI.Page
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
        MrndIssueNoteBatchController mrndIssueNoteBatchController = ControllerFactory.CreateMrndIssueNoteBatchController();
        MRNDetailController mRNDetailController = ControllerFactory.CreateMRNDetailController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "ReceivedIvetory.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ReceivedIvetoryLink";

               ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                ViewState["UserId"] = Session["UserId"].ToString();

                

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 12, 11) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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

                        gvDeliveredInventory.DataSource = mrndinController.fetchConfirmedMrndINListForRetur((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                        gvDeliveredInventory.DataBind();

                        
                    }
                    catch (Exception ex) {
                        throw ex;
                    }

                }

                else {
                    gvDeliveredInventory.DataSource = mrndinController.fetchConfirmedMrndINListByCompanyIdForReturn(int.Parse(Session["CompanyId"].ToString()));
                    gvDeliveredInventory.DataBind();

                }

                
            }
        }

        protected void btnMrn_Click(object sender, EventArgs e) {
            int MrnId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[3].Text);

            Response.Redirect("ViewMRNNew.aspx?MrnId=" + MrnId);
        }

        protected void gvDeliveredInventory_RowDataBound(object sender, GridViewRowEventArgs e) {
            try {
                if (e.Row.RowType == DataControlRowType.DataRow) {
                    GridView gvIssuedBatches = e.Row.FindControl("gvIssuedBatches") as GridView;
                    int MrndInId = int.Parse(e.Row.Cells[1].Text);

                    gvIssuedBatches.DataSource = mrndIssueNoteBatchController.getMrnIssuedInventoryBatches(MrndInId);
                    gvIssuedBatches.DataBind();

                }
            }
            catch (Exception ex) {
                throw;
            }
        }

        protected void btnReturnStock_Click(object sender, EventArgs e) {

            int MrndInId = int.Parse(hdnMrndInID.Value);
            int mrndId = int.Parse(hdnMrndID.Value);
            decimal returnQty = decimal.Parse(hdnQty.Value);
            int warehouseId = int.Parse(hdnWarehouseID.Value);
            int mrnId = int.Parse(hdnMrnID.Value);
            int ItemId = int.Parse(hdnItemID.Value);
            decimal IssuesQty = decimal.Parse(hdnIssuesQty.Value);
            //decimal returnQty = decimal.Parse(txtReturnQty.Text);
            int StockMaintainingType = int.Parse(hdnStockMaintainingType.Value);
            decimal PrevreturnQty = decimal.Parse(hdnPrevReturnedQty.Value);

            decimal MasterStockQty = 0;
            decimal MasterUnitPrice = 0;
            decimal ReturnedMasterStock = 0;
            MrnDetails mrnDetail = mRNDetailController.GetMrnDetailsByMrndId(mrndId);
            decimal receivedQty = mrnDetail.ReceivedQty;
            int Mrndstatus = 0;

            if (receivedQty > returnQty) {
                Mrndstatus = 12;
            }
            else {
                Mrndstatus = 5;
            }
            decimal maxQty = PrevreturnQty + returnQty;

            //int StockMaintainingType = addItemController.GetStockMaintaininType(ItemId, int.Parse(Session["CompanyId"].ToString()));
            if (maxQty > IssuesQty) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Cannot Return more than Issued Qty', showConfirmButton: true,timer: 4000}); });   </script>", false);

            }
            else {

                if (StockMaintainingType == 1) {
                    WarehouseInventory warehouseInventory = inventoryControllerInterface.GetInventoryByItemIdAndWarehouseId(ItemId, warehouseId, int.Parse(Session["CompanyId"].ToString()));
                    MasterStockQty = warehouseInventory.AvailableQty + warehouseInventory.HoldedQty;

                    if (MasterStockQty > 0) {
                        MasterUnitPrice = warehouseInventory.StockValue / MasterStockQty;
                        ReturnedMasterStock = MasterUnitPrice * returnQty;

                        int result = inventoryControllerInterface.ReturnMasterStock(ItemId, warehouseId, returnQty, ReturnedMasterStock, int.Parse(Session["UserId"].ToString()), Mrndstatus, MrndInId, mrndId, IssuesQty, PrevreturnQty, StockMaintainingType);
                        if (result > 0) {

                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Successfully returned stock', showConfirmButton: false,timer: 1500}); });   </script>", false);
                            mrnDetailsStatusLogController.InsertStockReturned(mrndId, int.Parse(Session["UserId"].ToString()));
                            if ((Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Count() > 0)) {
                                gvDeliveredInventory.DataSource = mrndinController.fetchConfirmedMrndINListForRetur((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                                gvDeliveredInventory.DataBind();

                            }

                            else {
                                gvDeliveredInventory.DataSource = mrndinController.fetchConfirmedMrndINListByCompanyIdForReturn(int.Parse(Session["CompanyId"].ToString()));
                                gvDeliveredInventory.DataBind();

                            }
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Return Stock failed', showConfirmButton: true,timer: 4000}); });   </script>", false);

                        }
                    }
                }

                else {
                    int BatchId = int.Parse(hdnBatchId.Value);
                    int result = inventoryControllerInterface.StockReturn(ItemId, warehouseId, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), MrndInId, returnQty, Mrndstatus, mrndId, IssuesQty, BatchId, PrevreturnQty, StockMaintainingType);
                    if (result > 0) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Successfully returned stock', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        mrnDetailsStatusLogController.InsertStockReturned(mrndId, int.Parse(Session["UserId"].ToString()));

                        if ((Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Count() > 0)) {
                            gvDeliveredInventory.DataSource = mrndinController.fetchConfirmedMrndINListForRetur((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                            gvDeliveredInventory.DataBind();

                        }

                        else {
                            gvDeliveredInventory.DataSource = mrndinController.fetchConfirmedMrndINListByCompanyIdForReturn(int.Parse(Session["CompanyId"].ToString()));
                            gvDeliveredInventory.DataBind();

                        }

                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Return Stock failed', showConfirmButton: true,timer: 4000}); });   </script>", false);

                    }
                }
            }
        }

        protected void gvDeliveredInventory_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvDeliveredInventory.PageIndex = e.NewPageIndex;
                if ((Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Count() > 0)) {

                    try {

                        gvDeliveredInventory.DataSource = mrndinController.fetchConfirmedMrndINListForRetur((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                        gvDeliveredInventory.DataBind();


                    }
                    catch (Exception ex) {
                        throw ex;
                    }

                }

                else {
                    gvDeliveredInventory.DataSource = mrndinController.fetchConfirmedMrndINListByCompanyIdForReturn(int.Parse(Session["CompanyId"].ToString()));
                    gvDeliveredInventory.DataBind();

                }


            }
            catch (Exception) {

                throw;
            }
        }


    }
}