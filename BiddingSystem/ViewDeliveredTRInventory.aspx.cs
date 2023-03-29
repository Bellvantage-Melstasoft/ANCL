using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class ViewDeliveredTRInventory : System.Web.UI.Page {

        //MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        //MRNDIssueNoteControllerInterface mrndinController = ControllerFactory.CreateMRNDIssueNoteController();
        EmailController emailController = ControllerFactory.CreateEmailController();
        InventoryControllerInterface inventoryController = ControllerFactory.CreateInventoryController();
        TRDIssueNoteController tRDIssueNoteController = ControllerFactory.CreateTRDIssueNoteController();
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefTR";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabTR";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewDeliveredTRInventory.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewDeliveredTRInventoryLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 13, 6) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                    if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Count() > 0)
                    {

                        try
                        {
                        gvDeliveredInventory.DataSource = tRDIssueNoteController.fetchDeliveredTRdINList((Session["UserWarehouses"] as List<UserWarehouse>).Where(w => w.UserType == 1).Select(w => w.WrehouseId).ToList());
                        gvDeliveredInventory.DataBind();

                        gvReceivedInventory.DataSource = tRDIssueNoteController.fetchReceivedTrdINList((Session["UserWarehouses"] as List<UserWarehouse>).Where(w => w.UserType == 1).Select(w => w.WrehouseId).ToList());
                        gvReceivedInventory.DataBind();


                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You must be a Warehouse Head to view this page', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                }


            }
        }
        
        protected void btnTR_Click(object sender, EventArgs e) {
            int TrId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[3].Text);

            Response.Redirect("ViewTR.aspx?TrId=" + TrId);
        }


        protected void btnReceivedTR_Click(object sender, EventArgs e) {
            int TrId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[2].Text);

            Response.Redirect("ViewTR.aspx?TrId=" + TrId);
        }

        protected void btnReceive_Click(object sender, EventArgs e) {
            try {
                GridViewRow row = (GridViewRow)(((Button)sender).NamingContainer);

                List<TRDIssueNote> notes = new List<TRDIssueNote>();
                List<Inventory> inventoryObjList = new List<Inventory>();
                Inventory inventoryObj = new Inventory();

                TRDIssueNote trdin = new TRDIssueNote();
                trdin.TRDInId = int.Parse(row.Cells[0].Text);
                trdin.TRDId = int.Parse(row.Cells[1].Text);
                trdin.ReceivedBy = int.Parse(Session["UserId"].ToString());
                trdin.FromWarehouseId = int.Parse(row.Cells[13].Text);
                trdin.ItemId = int.Parse(row.Cells[2].Text);
                trdin.IssuedQTY = decimal.Parse(row.Cells[8].Text);
                trdin.WarehouseId = int.Parse(row.Cells[6].Text);
                trdin.IssuedStockValue = decimal.Parse(row.Cells[14].Text);
                trdin.MeasurementId = int.Parse(row.Cells[15].Text);



                inventoryObj.ItemID = int.Parse(row.Cells[2].Text); 
                inventoryObj.WarehouseID = int.Parse(row.Cells[6].Text);
                inventoryObj.IssuedQty = decimal.Parse(row.Cells[8].Text); 
                inventoryObj.LastUpdatedBy = int.Parse(Session["UserId"].ToString());
                inventoryObj.IssuedStockValue = decimal.Parse(row.Cells[14].Text);
                inventoryObj.FromWarehouseId = int.Parse(row.Cells[13].Text);
                inventoryObj.CompanyID = int.Parse(Session["CompanyId"].ToString());

                inventoryObj.TrdIssueNoteBatches = ControllerFactory.CreateTrdIssueNoteBatchController().getTrIssuedInventoryBatches(trdin.TRDInId);


                int CompanyId = int.Parse(Session["CompanyId"].ToString());

                IConversionController conveter = ControllerFactory.CreateConversionController();
                IMeasurementDetailController uomController = ControllerFactory.CreateMeasurementDetailController();

                MeasurementDetail stockUom = uomController.GetStockMaintainingMeasurement(int.Parse(row.Cells[2].Text), CompanyId);

                if(stockUom.DetailId != int.Parse(row.Cells[15].Text)) {
                    inventoryObj.IssuedQty = conveter.DoConversion(inventoryObj.ItemID, CompanyId, inventoryObj.IssuedQty, int.Parse(row.Cells[15].Text), stockUom.DetailId);

                    for (int i = 0; i < inventoryObj.TrdIssueNoteBatches.Count; i++) {
                        inventoryObj.TrdIssueNoteBatches[i].IssuedQty = conveter.DoConversion(inventoryObj.ItemID, CompanyId, inventoryObj.TrdIssueNoteBatches[i].IssuedQty, int.Parse(row.Cells[15].Text), stockUom.DetailId);
                    }
                }

                inventoryObjList.Add(inventoryObj);
                notes.Add(trdin);

                decimal receivedQty = decimal.Parse(row.Cells[8].Text);

                TR_Details Trd = ControllerFactory.CreateTRDetailsController().GetTrd(int.Parse(row.Cells[1].Text));

                if(Trd.MeasurementId != int.Parse(row.Cells[15].Text)) {
                    receivedQty = conveter.DoConversion(inventoryObj.ItemID, CompanyId, receivedQty, int.Parse(row.Cells[15].Text), Trd.MeasurementId);
                }

                int result = inventoryController.ReceiveDeliveredTRItems(trdin, int.Parse(row.Cells[1].Text), receivedQty, trdin.ReceivedBy, inventoryObjList, notes, int.Parse(row.Cells[2].Text));

                if (result > 0) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);

                    gvDeliveredInventory.DataSource = tRDIssueNoteController.fetchDeliveredTRdINList((Session["UserWarehouses"] as List<UserWarehouse>).Where(w => w.UserType == 1).Select(w => w.WrehouseId).ToList());
                    gvDeliveredInventory.DataBind();

                    gvReceivedInventory.DataSource = tRDIssueNoteController.fetchReceivedTrdINList((Session["UserWarehouses"] as List<UserWarehouse>).Where(w => w.UserType == 1).Select(w => w.WrehouseId).ToList());
                    gvReceivedInventory.DataBind();

                    //int mrndID = 0;
                    //int mrndInId = 0;

                    //int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
                    //mrndID = int.Parse(gvDeliveredInventory.Rows[x].Cells[1].Text);
                    //mrndInId = int.Parse(gvDeliveredInventory.Rows[x].Cells[0].Text);

                    //List<string> emails = emailController.MRNCreatorDeliveredAndissuedPersonEmails(mrndID, mrndInId);
                    
                }
                else if (result == -1) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Transfer Request Note\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
                else if (result == -2) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Transfer Request Issue Note\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
                else if (result == -3) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating TR Log \"; $('#errorAlert').modal('show'); });   </script>", false);
                }
                else if (result == -4)
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Warehouse Stock \"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex) {

            }
        }
    }
}