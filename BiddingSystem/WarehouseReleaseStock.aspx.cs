using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Controller;
using System.IO;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class WarehouseReleaseStock : System.Web.UI.Page
    {

        #region Properies
        int CompanyId = 0;
        static int subDepartmentID = 0;
        string userId = string.Empty;
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        public List<string> departmentList= new List<string>();
        List<SubDepartment> departments = new List<SubDepartment>();
        SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();
        AddItemController addItemsController = ControllerFactory.CreateAddItemController();
        StockReleaseControllerInterface stockReleaseController = ControllerFactory.CreateStockReleaseController();
        InventoryControllerInterface inventoryController = ControllerFactory.CreateInventoryController();
        WarehouseControllerInterface warehouseController = ControllerFactory.CreateWarehouseController();
        static List<WarehouseInventory> inventory = new List<WarehouseInventory>();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefStock";
                    ((BiddingAdmin)Page.Master).subTabTitle = "StockTab";
                    ((BiddingAdmin)Page.Master).subTabValue = "WarehouseReleaseStock.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "warehouseReleaseStockLink";

                    CompanyId = int.Parse(Session["CompanyId"].ToString());
                    userId = Session["UserId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(userId), CompanyId, 10, 2) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                    LoadDDLWarehouses();
                    LoadDDLReleasedTo();
                    //LoadGV();
                }
                msg.Visible = false;
            }

            catch (Exception)
            {

            }
        }

        private void LoadDDLWarehouses()
        {
            try
            {
                ddlWarehouse.DataSource = warehouseController.getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
                ddlWarehouse.DataValueField = "WarehouseID";
                ddlWarehouse.DataTextField = "Location";
                ddlWarehouse.DataBind();
                ddlWarehouse.Items.Insert(0, new ListItem("Select A Warehouse", ""));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        private void LoadDDLItems(int warehouseID)
        {
            try
            {
                inventory= inventoryController.fetchWarehouseItems(warehouseID);
                ddlItems.DataSource = inventory;
                ddlItems.DataValueField = "ItemID";
                ddlItems.DataTextField = "ItemName";
                ddlItems.DataBind();
                ddlItems.Items.Insert(0, new ListItem("Select An Item", ""));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LoadDDLReleasedTo()
        {
            try
            {
                ddlReleasedTo.DataSource = warehouseController.getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
                ddlReleasedTo.DataValueField = "WarehouseID";
                ddlReleasedTo.DataTextField = "Location";
                ddlReleasedTo.DataBind();
                ddlReleasedTo.Items.Insert(0, new ListItem("Select A Warehouse", ""));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    WarehouseInventoryRelease inventoryObj = new WarehouseInventoryRelease();

                    inventoryObj.WarehouseID = int.Parse(ddlWarehouse.SelectedValue.ToString());
                    inventoryObj.ItemID = int.Parse(ddlItems.SelectedValue.ToString());
                    inventoryObj.ReleasedQty = int.Parse(txtQty.Text);
                    inventoryObj.ReleasedBy = int.Parse(Session["UserId"].ToString());
                    inventoryObj.ReleasedType = 2;
                    inventoryObj.ReleasedWarehouseID = int.Parse(ddlReleasedTo.SelectedValue.ToString());
                    inventoryObj.Description = txtDescription.Text;

                    int result = inventoryController.releaseStockFromWarehouseToWarehouse(inventoryObj);

                    if (result > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('successMessage').innerHTML = \"Inventory Transfered Successfully\"; $('#SuccessAlert').modal('show'); });   </script>", false);
                        //DisplayMessage("Department has been created successfully", false);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error On Transferring Inventory\"; $('#errorAlert').modal('show'); });   </script>", false);
                        //DisplayMessage("Department Name already exists", true);
                    }
                    
                }



                
                clearFields();
                //LoadGV();

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void DisplayMessage(string message, bool isError)
        {
            msg.Visible = true;
            if (isError)
            {
                lbMessage.CssClass = "failMessage";
                msg.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else
            {
                lbMessage.CssClass = "successMessage";
                msg.Attributes["class"] = "alert alert-success alert-dismissable";
            }

            lbMessage.Text = message;

        }

        

        private void clearFields()
        {
            txtQty.Text = "";
            txtDescription.Text = "";
            LoadDDLWarehouses();
            LoadDDLReleasedTo();
            ddlWarehouse.SelectedIndex = 0;
            ddlItems.SelectedIndex = 0;
            ddlReleasedTo.SelectedIndex = 0;
            btnSave.Text = "Save";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                clearFields();
            }
            catch (Exception)
            {

                throw;
            }
        }
        

        protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlWarehouse.SelectedIndex != 0 && ddlWarehouse.SelectedValue != "")
                {
                    int warehouseID = int.Parse(ddlWarehouse.SelectedValue);
                    // ddlItemName.Items.Clear();
                    LoadDDLReleasedTo();
                    LoadDDLItems(warehouseID);
                    if (ddlReleasedTo.Items.FindByValue(ddlWarehouse.SelectedValue.ToString()) != null)
                    {
                        ddlReleasedTo.Items.Remove(ddlReleasedTo.Items.FindByValue(ddlWarehouse.SelectedValue.ToString()));
                    }
                }
                else
                {
                    ddlItems.Items.Clear();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlItems.SelectedIndex != 0 && ddlItems.SelectedValue != "")
                {
                    WarehouseInventory inventoryObj = inventory.Where(i => i.ItemID == int.Parse(ddlItems.SelectedValue)).First();
                    txtQty.Attributes.Add("max", inventoryObj.AvailableQty.ToString());
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            
        }
    }
}