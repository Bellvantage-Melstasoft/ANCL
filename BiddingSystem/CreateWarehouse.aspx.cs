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
    public partial class CreateWarehouse : System.Web.UI.Page
    {

        #region Properies
        //int CompanyId = 0;
        //static int warehouseID = 0;
        //string userId = string.Empty;
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
       // public List<string> warehouseList= new List<string>();
       // List<Warehouse> warehouses = new List<Warehouse>();
        WarehouseControllerInterface warehouseController = ControllerFactory.CreateWarehouseController();
        UserWarehouseController userWarehouseController = ControllerFactory.CreateUserWarehouse();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {



                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefWarehouse";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabWarehouse";
                    ((BiddingAdmin)Page.Master).subTabValue = "CreateWarehouse.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "CreateWarehouseLink";

                   // CompanyId = int.Parse(Session["CompanyId"].ToString());
                   // userId = Session["UserId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 10, 1) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                    {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }

                List<Warehouse> warehouses = new List<Warehouse>();
                warehouses = warehouseController.getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
                List<string> warehouseList = new List<string>();
                foreach (var item in warehouses)
                {
                    warehouseList.Add(item.Location);

                }
                ViewState["warehouseList"] = new JavaScriptSerializer().Serialize(warehouseList);


                if (!IsPostBack)
                {

                    ddlHeadOfWarehouse.DataSource = companyLoginController.GetUserListByDepartmentid(int.Parse(Session["CompanyId"].ToString()));
                    ddlHeadOfWarehouse.DataValueField = "UserId";
                    ddlHeadOfWarehouse.DataTextField = "Username";
                    ddlHeadOfWarehouse.DataBind();
                    ddlHeadOfWarehouse.Items.Insert(0, new ListItem("Select Head of Department", ""));

                    LoadGV();
                }
                msg.Visible = false;
            }
             
            catch (Exception ex)
            {
                throw ex; 
            }
}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    List<int> UserIds = new List<int>();

                    for (int i = 0; i < ddlHeadOfWarehouse.Items.Count; i++) {
                        if (ddlHeadOfWarehouse.Items[i].Selected) {
                            UserIds.Add(int.Parse(ddlHeadOfWarehouse.Items[i].Value));
                        }
                    }

                    int result = warehouseController.saveWarehouse(txtLocation.Text, txtPhoneNo.Text, int.Parse(Session["CompanyId"].ToString()), txtAddress.Text, chkIsavtive.Checked == true ? 1 : 0, UserIds);
                    if (result !=-1)
                    {
                        if (result > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                            //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('successMessage').innerHTML = \"Warehouse has been created successfully\"; $('#SuccessAlert').modal('show'); });   </script>", false);
                            //DisplayMessage("Department has been created successfully", false);
                            clearFields();

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on creating Warehouse\"; $('#errorAlert').modal('show'); });   </script>", false);
                            //DisplayMessage("Department Name already exists", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Warehouse location already exists\"; $('#errorAlert').modal('show'); });   </script>", false);
                        //DisplayMessage("Department Name already exists", true);
                    }
                }



                if (btnSave.Text == "Update")
                {
                    if (int.Parse(ViewState["warehouseID"].ToString()) != 0)
                    {
                        List<int> UserIds = new List<int>();
                        for (int i = 0; i < ddlHeadOfWarehouse.Items.Count; i++) {
                            if (ddlHeadOfWarehouse.Items[i].Selected) {
                                UserIds.Add(int.Parse(ddlHeadOfWarehouse.Items[i].Value));
                            }
                        }
                        int result = warehouseController.updateWarehouse(int.Parse(ViewState["warehouseID"].ToString()), txtLocation.Text, txtPhoneNo.Text, int.Parse(Session["CompanyId"].ToString()), txtAddress.Text, chkIsavtive.Checked == true ? 1 : 0, UserIds);
                        if (result != -1)
                        {
                            if (result > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                                //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('successMessage').innerHTML = \"Warehouse has been updated successfully\"; $('#SuccessAlert').modal('show'); });   </script>", false);
                                //DisplayMessage("Company has been updated successfully", false);
                                clearFields();
                                
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Updating Warehouse failed\"; $('#errorAlert').modal('show'); });   </script>", false);
                                //DisplayMessage("Company update Problem", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Warehouse name already exists\"; $('#errorAlert').modal('show'); });   </script>", false);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Please select a Warehouse\"; $('#errorAlert').modal('show'); });   </script>", false);
                        //DisplayMessage("Please Select Department", true);
                    }
                }
                clearFields();
                LoadGV();

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

        private void LoadGV()
        {
            try
            {
                    gvWarehouses.DataSource = warehouseController.getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
                    gvWarehouses.DataBind();
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                ViewState["warehouseID"] = int.Parse(gvWarehouses.Rows[x].Cells[1].Text);
                Warehouse warehouseObj = warehouseController.getWarehouseByID(int.Parse(ViewState["warehouseID"].ToString()));
                if (warehouseObj.WarehouseID != 0)
                {
                    txtLocation.Text = warehouseObj.Location;
                    txtPhoneNo.Text = warehouseObj.PhoneNo;
                    if (warehouseObj.IsActive == 1)
                    {
                        chkIsavtive.Checked = true;
                    }
                    else
                    {
                        chkIsavtive.Checked = false;
                    }
                    ddlHeadOfWarehouse.DataSource = companyLoginController.GetUserListByDepartmentid(int.Parse(Session["CompanyId"].ToString()));
                    ddlHeadOfWarehouse.DataValueField = "UserId";
                    ddlHeadOfWarehouse.DataTextField = "Username";
                    ddlHeadOfWarehouse.DataBind();
                    ddlHeadOfWarehouse.Items.Insert(0, new ListItem("Select Head of Department", ""));
                

                    List<UserWarehouse> warehouseHeads = userWarehouseController.GetWarehouseHeadsByWarehouseId(warehouseObj.WarehouseID);

                    for (int i = 0; i < warehouseHeads.Count; i++) {

                        if (ddlHeadOfWarehouse.Items.FindByValue(warehouseHeads[i].UserId.ToString()) != null) {

                            var s = ddlHeadOfWarehouse.Items.FindByValue(warehouseHeads[i].UserId.ToString());
                            ddlHeadOfWarehouse.Items.FindByValue(warehouseHeads[i].UserId.ToString()).Selected = true;
                        }

                    }

                    txtAddress.Text = warehouseObj.Address;
                    btnSave.Text = "Update";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                int status = warehouseController.deleteWarehouse(int.Parse(ViewState["warehouseID"].ToString()));
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", true);
                    //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('successMessage').innerHTML = \"Warehouse has been deleted successfully\"; $('#SuccessAlert').modal('show'); });   </script>", false);
                    //DisplayMessage("Company has been Deleted Successfully", false);
                    clearFields();
                    LoadGV();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Deleting Warehouse failed\"; $('#errorAlert').modal('show'); });   </script>", false);
                    //DisplayMessage("Error on Delete Company", true);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string getJsonWarehouseList()
        {
            var DataList = new JavaScriptSerializer().Deserialize<List<string>>(ViewState["warehouseList"].ToString());
            return (new JavaScriptSerializer()).Serialize(DataList);
        }

        private void clearFields()
        {
            txtLocation.Text = "";
            txtPhoneNo.Text = "";
            txtAddress.Text = "";
            ddlHeadOfWarehouse.DataSource = companyLoginController.GetUserListByDepartmentid(int.Parse(Session["CompanyId"].ToString()));
            ddlHeadOfWarehouse.DataValueField = "UserId";
            ddlHeadOfWarehouse.DataTextField = "Username";
            ddlHeadOfWarehouse.DataBind();
            ddlHeadOfWarehouse.Items.Insert(0, new ListItem("Select Head of Department", ""));
            btnSave.Text = "Save";
            chkIsavtive.Checked = false;
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

        protected void gvDepartments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvWarehouses.PageIndex = e.NewPageIndex;
                LoadGV();
            }
            catch (Exception)
            {

            }
        }

        protected void btnDeleteCompany_Click(object sender, ImageClickEventArgs e)
        {
            int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
            ViewState["warehouseID"] = int.Parse(gvWarehouses.Rows[x].Cells[1].Text);

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {" +
                                                                                                                    "swal({" +
                                                                                                                    "title: 'Are you sure?'," +
                                                                                                                            "text: \"You will be able to revert this\"," +
                                                                                                                           "type: 'warning'," +
                                                                                                                            "showCancelButton: true," +
                                                                                                                            "confirmButtonColor: '#3085d6'," +
                                                                                                                            "cancelButtonColor: '#d33'," +
                                                                                                                            "confirmButtonText: 'Yes, delete it!'" +
                                                                                                                        "}).then((result) => {" +
                                                                                                                        "if (result.value)" +
                                                                                                                        "{ document.getElementById('" + btnDelete.ClientID + "').click();}}); " +
                                                                                                                        "}); " +
                                                                                                                        "</script>", false);
        }
    }
}