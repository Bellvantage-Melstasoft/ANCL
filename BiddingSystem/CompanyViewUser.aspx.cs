using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace BiddingSystem {
    public partial class CompanyViewUser : System.Web.UI.Page {
        #region Properties
        //int CompanyId = 0;
        //static int viewUserId = 0;
        //int userId = 0;
        //static int companyUserID = 0;

        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
       // public static List<CompanyLogin> getUserListByCompanyId = new List<CompanyLogin>();
       // CompanyLogin userobj = new CompanyLogin();
        UserRoleController userRoleController = ControllerFactory.CreateUserRoleController();
        RoleFunctionController roleFunctionController = ControllerFactory.CreateRoleFunctionController();
        SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();
        DesignationController designationController = ControllerFactory.CreateDesignationController();
        WarehouseControllerInterface warehouseController = ControllerFactory.CreateWarehouseController();
     //   public static GridView gridView;
      //  public static List<UserRole> companyUserRole = new List<UserRole>();
        UserWarehouseController userWarehouseController = ControllerFactory.CreateUserWarehouse();
        UserSubDepartmentController userSubDepartmentController = ControllerFactory.CreateUserSubDepartment();

        public static List<Designation> listDesignation = new List<Designation>();
        #endregion

        private void DisableFields(Boolean value) {
            txtName.Enabled = value;
            txtEmilAddress.Enabled = value;
            txtEmpNo.Enabled = value;
            //ddlDepartment.Enabled = value;
            chkIsActive.Enabled = value;
            btnUdate.Enabled = value;
            btnCancel.Enabled = value;
            ddlUserRoles.Enabled = value;
            ddlDesignation.Enabled = value;
            btnCreateNewDesignation.Enabled = value;
           // btnEditDesignation.Enabled = value;

        }

        protected void Page_Load(object sender, EventArgs e) {
            try {


                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefUserCreation";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabUserCreation";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyViewUser.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "editUserlink";

                    ViewState["CompanyId"] = Session["CompanyId"].ToString();
                    int CompanyId = int.Parse(Session["CompanyId"].ToString());
                    int userId = int.Parse(Session["UserId"].ToString());
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    if ((!companyUserAccessController.isAvilableAccess(userId, CompanyId, 2, 2) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else {
                    Response.Redirect("LoginPage.aspx");
                }

                msg.Visible = false;
                if (!IsPostBack) {
                    loadDesignations();
                    Bindusers();
                    //divEditUser.Visible = false;
                    DisableFields(false);
                    LoadUserRoles();
                    //   loadDepartments();
                    // loadwarehouse();
                    pwdPanel.Visible = false;
                    GridView gridView = gvCompanyUsers;
                }
            }
            catch (Exception) {

            }
        }
        private void loadDesignations() {
            try {
              //  List<Designation> listDesignation = new List<Designation>();
                listDesignation = designationController.GetDesignationList();
             //   ViewState["listDesignation"] = new JavaScriptSerializer().Serialize(listDesignation);
              //  var listDesignation1 = new JavaScriptSerializer().Deserialize<List<Designation>>(ViewState["listDesignation"].ToString());
                ddlDesignation.DataSource = listDesignation;
                ddlDesignation.DataValueField = "DesignationId";
                ddlDesignation.DataTextField = "DesignationName";
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, new ListItem("--Select Designation--", "0"));
            }
            catch (Exception ex) {
                throw;
            }
        }

        //private void loadwarehouse() {
        //    try {
        //        ddlwarehouse.DataSource = warehouseController.getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
        //        ddlwarehouse.DataValueField = "WarehouseID";
        //        ddlwarehouse.DataTextField = "Location";
        //        ddlwarehouse.DataBind();
        //        ddlwarehouse.Items.Insert(0, new ListItem("Select A Warehouse", ""));
        //    }
        //    catch (Exception) {

        //        throw;
        //    }
        //}
      
        protected void lbtnEdit_Click(object sender, EventArgs e) {
            try {
                //divEditUser.Visible = true;
                DisableFields(true);

              //  loadDepartments();
                txtName.Enabled = true;
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;

                ViewState["ViewUserId"] = int.Parse(gvCompanyUsers.Rows[x].Cells[1].Text);

                //  loadDepartments();
                pwdPanel.Visible = true;

                loadDepartments();
                loadWarehouses();

                CompanyLogin userobj = new CompanyLogin();

                userobj = companyLoginController.GetUserbyuserId(int.Parse(ViewState["ViewUserId"].ToString()));
                ViewState["userobj"] = new JavaScriptSerializer().Serialize(userobj);
                if (userobj.UserId != 0) {
                    List<CompanyUserAccess> GetCompanyUserRole = companyUserAccessController.FetchCompanyUserAccessByUserId(int.Parse(ViewState["ViewUserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));

                    if (GetCompanyUserRole.Count() > 0) {
                        int userRoleId = GetCompanyUserRole.FirstOrDefault().userRoleID;
                        if (userRoleId > 0) {

                            ddlUserRoles.SelectedValue = userRoleId.ToString();
                        }

                    }
                    //if (userobj.WarehouseID != 0) {
                    //    ddlwarehouse.Enabled = true;
                    //    ddlwarehouse.SelectedValue = userobj.WarehouseID.ToString();
                    //}
                    //else {
                    //    ddlwarehouse.Enabled = false;
                    //    ddlwarehouse.SelectedIndex = 0;
                    //}
                    txtName.Text = userobj.FirstName;
                    txtusername.Text = userobj.Username;

                    //txtpassword.Text = userobj.Password.Decrypt();
                    //txtConfirmPassword.Text = userobj.Password.Decrypt();

                    txtpassword.Attributes.Add("value", userobj.Password.Decrypt());
                    txtConfirmPassword.Attributes.Add("value", userobj.Password.Decrypt());

                    txtEmilAddress.Text = userobj.EmailAddress;
                    txtEmpNo.Text = userobj.EmployeeNo.ToString();
                    txtContactNo.Text = userobj.ContactNo;
                    ddlDesignation.SelectedValue = userobj.DesignationId.ToString();
                    //ddluserType.SelectedValue = userobj.Usertype;
                    if (userobj.IsActive == 1) {
                        chkIsActive.Checked = true;
                    }
                    else {
                        chkIsActive.Checked = false;
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#divEditUser1').scrollIntoView(); });   </script>", false);

                   // ddlDepartment.SelectedValue = userobj.SubDepartmentID.ToString();
                    ddlDesignation.Enabled = true;

                  



                }

            }
            catch (Exception ex) {
            }

        }



        private void loadDepartments() {
            try {
                

                List<UserSubDepartment> bindingList = new List<UserSubDepartment>();
                List<SubDepartment> companyDepartments = subDepartmentController.getDepartmentList(int.Parse(Session["CompanyId"].ToString()));

                List<UserSubDepartment> userDepartment = userSubDepartmentController.getUserSubDepartmentdetails(int.Parse(ViewState["ViewUserId"].ToString()));




                for (int i = 0; i < companyDepartments.Count; i++) {

                    if (userDepartment.Exists(d => d.DepartmentId == companyDepartments[i].SubDepartmentID)) {

                        UserSubDepartment dep = userDepartment.Find(d => d.DepartmentId == companyDepartments[i].SubDepartmentID);
                        dep.IsSelected = 1;

                        dep.DepartmentName = companyDepartments[i].SubDepartmentName;
                        bindingList.Add(dep);
                    }
                    else {
                        UserSubDepartment dep = new UserSubDepartment {
                            DepartmentId = companyDepartments[i].SubDepartmentID,
                            DepartmentName = companyDepartments[i].SubDepartmentName,
                            IsSelected = 0,
                            IsHead = 0
                        };

                        bindingList.Add(dep);
                    }
                }

                gvdepartments.DataSource = bindingList;
                gvdepartments.DataBind();
            }
            catch (Exception ex) {
                throw;
            }
        }



        private void loadWarehouses() {

            List<UserWarehouse> bindingList = new List<UserWarehouse>();

            List<UserWarehouse> userWarehouse = userWarehouseController.getUserWarehousedetails(int.Parse(ViewState["ViewUserId"].ToString()));

            List<Warehouse> warehouses = ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
            

            for (int i = 0; i < warehouses.Count; i++) {

                if (userWarehouse.Exists(d => d.WrehouseId == warehouses[i].WarehouseID)) {

                    UserWarehouse wh = userWarehouse.Find(d => d.WrehouseId == warehouses[i].WarehouseID);
                    wh.IsSelected = 1;
                    
                    wh.Location = warehouses[i].Location;
                    

                    bindingList.Add(wh);
                }
                else {
                    UserWarehouse wh = new UserWarehouse {
                        WrehouseId = warehouses[i].WarehouseID,
                        Location = warehouses[i].Location,
                        IsSelected = 0,
                        UserType = 0
                       
                       
                    };
                    
                    bindingList.Add(wh);
                }
            }
            ViewState["UserType"] = new JavaScriptSerializer().Serialize(bindingList); 
            gvwarehouse.DataSource = bindingList;
            gvwarehouse.DataBind();
        }

        protected void gvwarehouse_RowDataBound(object sender, GridViewRowEventArgs e) {

            if (e.Row.RowType == DataControlRowType.DataRow) {
                List<UserWarehouse> User = new JavaScriptSerializer().Deserialize<List<UserWarehouse>>(ViewState["UserType"].ToString());

                DropDownList ddl = e.Row.Cells[3].FindControl("ddlUserType") as DropDownList;

                ddl.SelectedValue = User.Find(u => u.WrehouseId == int.Parse(e.Row.Cells[1].Text)).UserType.ToString();
            }

            
        }


        protected void confirmation_Click(object sender, EventArgs e) {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
        }

        //private void loadDepartments() {
        //    try {
        //        ddlDepartment.DataSource = subDepartmentController.getDepartmentList(int.Parse(Session["CompanyId"].ToString()));
        //        ddlDepartment.DataValueField = "SubDepartmentID";
        //        ddlDepartment.DataTextField = "SubDepartmentName";
        //        ddlDepartment.DataBind();
        //        ddlDepartment.Items.Insert(0, new ListItem("Select A Department", ""));
        //    }
        //    catch (Exception ex) {
        //        throw;
        //    }
        //}

        private void LoadUserRoles() {
            try {
                List<UserRole> companyUserRole = new List<UserRole>();
                companyUserRole = userRoleController.FetchUserRole(int.Parse(Session["CompanyId"].ToString()));
                ddlUserRoles.DataSource = companyUserRole;
                ddlUserRoles.DataTextField = "userRoleName";
                ddlUserRoles.DataValueField = "userRoleId";
                ddlUserRoles.DataBind();
                ddlUserRoles.Items.Insert(0, new ListItem("Select User Role", "0"));
            }
            catch (Exception ex) {

            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e) {
            try {

                int userId = int.Parse(Session["UserId"].ToString());
                List<UserSubDepartment> UsersubDepartment = new List<UserSubDepartment>();

                for (int i = 0; i < gvdepartments.Rows.Count; i++) {
                    if (((CheckBox)gvdepartments.Rows[i].FindControl("chkDepartments")).Checked) {
                        UserSubDepartment subDepartment = new UserSubDepartment();
                        subDepartment.DepartmentId = int.Parse(gvdepartments.Rows[i].Cells[1].Text);
                        subDepartment.IsHead = ((CheckBox)gvdepartments.Rows[i].FindControl("chkIsHead")).Checked == true ? 1 : 0;
                        UsersubDepartment.Add(subDepartment);

                    }
                }

                List<UserWarehouse> UserWarehouse = new List<UserWarehouse>();

                for (int i = 0; i < gvwarehouse.Rows.Count; i++) {

                    if (((CheckBox)gvwarehouse.Rows[i].FindControl("chkwarehouse")).Checked) {
                        UserWarehouse warehouse = new UserWarehouse();

                        warehouse.WrehouseId = int.Parse(gvwarehouse.Rows[i].Cells[1].Text);
                        warehouse.UserType = int.Parse(((DropDownList)gvwarehouse.Rows[i].FindControl("ddlUserType")).SelectedValue);
                        UserWarehouse.Add(warehouse);


                    }

                }






                //if (viewUserId != 0) {
                //    int Updateuser = 0;
                //    //if (ddlDepartment.SelectedIndex == 0) {
                //    //Updateuser = companyLoginController.UpdateCompanyLogin(viewUserId, txtusername.Text, txtEmpNo.Text, txtpassword.Text.Encrypt(), "U", txtName.Text, txtEmilAddress.Text, LocalTime.Now, userId.ToString(), chkIsActive.Checked ? 1 : 0, int.Parse(ddlDesignation.SelectedValue));
                //    Updateuser = companyLoginController.UpdateCompanyLogin(viewUserId, txtusername.Text, txtEmpNo.Text, txtpassword.Text.Encrypt(), "S", txtName.Text, txtEmilAddress.Text, LocalTime.Now, userId.ToString(), chkIsActive.Checked ? 1 : 0, int.Parse(ddlDesignation.SelectedValue));
                //}
                //else {
                //    var warehouseId = 0;
                //    if (ddlwarehouse.SelectedIndex > 0) {
                //        warehouseId = int.Parse(ddlwarehouse.SelectedValue);
                //    }
                //    //Updateuser = companyLoginController.UpdateCompanyLogin(viewUserId, txtusername.Text, txtEmpNo.Text, txtpassword.Text.Encrypt(), "U", txtName.Text, txtEmilAddress.Text, LocalTime.Now, userId.ToString(), chkIsActive.Checked ? 1 : 0, int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlDesignation.SelectedValue), warehouseId);
                //    Updateuser = companyLoginController.UpdateCompanyLogin(viewUserId, txtusername.Text, txtEmpNo.Text, txtpassword.Text.Encrypt(), "S", txtName.Text, txtEmilAddress.Text, LocalTime.Now, userId.ToString(), chkIsActive.Checked ? 1 : 0, int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlDesignation.SelectedValue), warehouseId);
                //}



                //CompanyLogin userobj = companyLoginController.GetUserbyuserId(int.Parse(ViewState["ViewUserId"].ToString()));
                var userobj = new JavaScriptSerializer().Deserialize<CompanyLogin>(ViewState["userobj"].ToString());
                    if (userobj != null) {
                        int Updateuser = 0;


                        Updateuser = companyLoginController.UpdateCompanyLogin(int.Parse(ViewState["ViewUserId"].ToString()), txtusername.Text, txtEmpNo.Text, txtpassword.Text.Encrypt(), "S", txtName.Text, txtEmilAddress.Text, LocalTime.Now, userId.ToString(), chkIsActive.Checked ? 1 : 0, int.Parse(ddlDesignation.SelectedValue), UsersubDepartment, UserWarehouse, txtContactNo.Text);
                    
                    if (Updateuser != -1) {
                        if (Updateuser > 0) {



                            List<RoleFunction> GetRoleFunctionByRoleId = new List<RoleFunction>();
                            GetRoleFunctionByRoleId = roleFunctionController.FetchRoleFunctionByRoleId(int.Parse(ddlUserRoles.SelectedValue));

                            int deeteUserAccess = companyUserAccessController.DeleteCompanyUserAccessByUserId(int.Parse(ViewState["ViewUserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                            if (GetRoleFunctionByRoleId.Count() > 0) {
                                foreach (var item in GetRoleFunctionByRoleId) {

                                    int saveUserAccess = companyUserAccessController.SaveCompanyUserAccess(int.Parse(ViewState["ViewUserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), int.Parse(ddlUserRoles.SelectedValue), item.systemDivisionId, item.functionId, LocalTime.Now, userId.ToString(), LocalTime.Now, userId.ToString());
                                }

                            }
                            ScriptManager.RegisterClientScriptBlock(this, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                            //ScriptManager.RegisterClientScriptBlock(this, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('successMessage').innerHTML = \"User details has been updated successfully\"; $('#SuccessAlert').modal('show'); });   </script>", false);
                            //DisplayMessage("User details has been updated successfully", false);
                            ClearFields();
                            //viewUserId = 0;
                            //divEditUser.Visible = false;
                            DisableFields(false);
                            Bindusers();
                            gvdepartments.DataSource = null;
                            gvdepartments.DataBind();

                            gvwarehouse.DataSource = null;
                            gvwarehouse.DataBind();
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on Update User', showConfirmButton: true,timer: 4000}); });   </script>", false);
                            //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on Update User\"; $('#errorAlert').modal('show'); });   </script>", false);
                            //DisplayMessage("Error on Update User", true);
                        }
                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'This username / Email Address already exists', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"This username / Email Address already exists\"; $('#errorAlert').modal('show'); });   </script>", false);
                        //DisplayMessage("This username / Email Address already exists", true);
                    }

                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please select User', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Please select User\"; $('#errorAlert').modal('show'); });   </script>", false);
                    //DisplayMessage("Please select User", true);

                }

            }
            catch (Exception ex) {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            ClearFields();
            //viewUserId = 0;
            //divEditUser.Visible = false;
            DisableFields(false);
            gvdepartments.DataSource = null;
            gvdepartments.DataBind();

            gvwarehouse.DataSource = null;
            gvwarehouse.DataBind();
        }

        private void DisplayMessage(string message, bool isError) {
            msg.Visible = true;
            if (isError) {
                lbMessage.CssClass = "failMessage";
                msg.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else {
                lbMessage.CssClass = "successMessage";
                msg.Attributes["class"] = "alert alert-success alert-dismissable";
            }

            lbMessage.Text = message;

        }

        protected void ClearFields() {
            ddlDesignation.SelectedValue = "0";
            txtName.Text = "";
            txtEmilAddress.Text = "";
            txtpassword.Text = "";
            txtEmpNo.Text = "";
            chkIsActive.Checked = false;
            //viewUserId = 0;
            ddlUserRoles.SelectedValue = "0";
            //ddluserType.SelectedIndex = 0;
            txtusername.Text = "";
            txtContactNo.Text = "";
            txtConfirmPassword.Text = "";
            pwdPanel.Visible = false;
            //loadDepartments();
            //loadwarehouse();

        }

        protected void Bindusers() {
            try {

                //getUserListByCompanyId = companyLoginController.GetUserListByDepartmentid(CompanyId);

                //if (getUserListByCompanyId.Count() > 0)
                //{
                //    gvCompanyUsers.DataSource = getUserListByCompanyId;
                //    gvCompanyUsers.DataBind();

                //}

                gvCompanyUsers.DataSource = companyLoginController.GetUserListByDepartmentid(int.Parse(ViewState["CompanyId"].ToString()));
                gvCompanyUsers.DataBind();

            }
            catch (Exception) {

            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e) {
            try {
                //int userId = companyUserID;
                int userId = int.Parse(ViewState["companyUserID"].ToString());

                int DeleteStatus = companyLoginController.UpdateInactiveUsers(userId, 0);
                if (DeleteStatus > 0) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('successMessage').innerHTML = \"User Has been Delete Successfully\"; $('#SuccessAlert').modal('show'); });   </script>", false);
                    //DisplayMessage("User Has been Delete Successfully", false);
                    Bindusers();
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error On Delete User', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error On Delete User\"; $('#errorAlert').modal('show'); });   </script>", false);
                    //DisplayMessage("Error On Delete User", true);
                }


            }
            catch (Exception) {

            }
        }

        protected void gvCompanyUsers_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvCompanyUsers.PageIndex = e.NewPageIndex;
                gvCompanyUsers.DataSource = companyLoginController.GetUserListByDepartmentid(int.Parse(ViewState["CompanyId"].ToString()));
                gvCompanyUsers.DataBind();

            }
            catch (Exception) {

                throw;
            }
        }

        protected void lbtnDelete_Click1(object sender, ImageClickEventArgs e) {
            int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
            //companyUserID = int.Parse(gvCompanyUsers.Rows[x].Cells[1].Text);
            ViewState["companyUserID"] = int.Parse(gvCompanyUsers.Rows[x].Cells[1].Text);


            ScriptManager.RegisterClientScriptBlock(Updatepanel1,
                this.Updatepanel1.GetType(),
                "none",
                "<script>    $(document).ready(function () {" +
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

        protected void btnCreateNewDesignation_Click(object sender, EventArgs e) {
            btnCreateNewDesignation.Visible = true;
            //btnEditDesignation.Visible = false;
            btnSaveDesignation.Text = "Save Designation";
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $('#myModalAddDesignation').modal('show'); </script>", false);
            txtDesignationName.Text = "";


        }

        protected void btnEditDesignation_Click(object sender, EventArgs e) {
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $('#myModalAddDesignation').modal('show'); </script>", false);

            btnSaveDesignation.Visible = false;
            btnUpdateDesignation.Visible = true;
            txtDesignationName.Text = ddlDesignation.SelectedItem.ToString().Replace(",", "");
            btnSaveDesignation.Text = "Update Designation";

        }

        protected void btnSaveDesignation_Click(object sender, EventArgs e) {

            string newDesignation = txtDesignationName.Text.Replace(",", "");
            string userId = Session["UserId"].ToString();
            designationController.SaveDesignation(newDesignation, userId, LocalTime.Now);
            DisplayMessage("Designation Added Succesfully", false);

            loadDesignations();
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>      $('.modal-backdrop').remove();  </script>", false);
           // ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove(); $('#myModalAddDesignation').modal('hide');  });   </script>", false);

        }

        protected void btnUpdateDesignation_Click(object sender, EventArgs e) {

            string newDesignation = txtDesignationName.Text.Replace(",", "");
            int designationId = int.Parse(ddlDesignation.SelectedValue);
            string userId = Session["UserId"].ToString();
            designationController.UpdateDesignation(designationId, newDesignation, userId, LocalTime.Now);
            DisplayMessage("Designation Updated Succesfully", false);
            var userobj = new JavaScriptSerializer().Deserialize<CompanyLogin>(ViewState["userobj"].ToString());
            loadDesignations();
            userobj = companyLoginController.GetUserbyuserId(int.Parse(ViewState["ViewUserId"].ToString()));
            ddlDesignation.SelectedValue = userobj.DesignationId.ToString();
            //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>     $('.modal-backdrop').remove();  </script>", false);
            //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();  });   </script>", false);


        }
        //protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e) {
        //    if (ddlDesignation.SelectedValue == "18") {
        //        ddlwarehouse.Enabled = true;


        //    }
        //    else {
        //        ddlwarehouse.Enabled = false;
        //        ddlwarehouse.SelectedIndex = 0;
        //    }
        //}

        protected void SearchAll_Click(object sender, EventArgs e) {
            if (txtSearch.Text != "") {
                //List<CompanyLogin> getUserListByCompanyId = new List<CompanyLogin>();
                //getUserListByCompanyId = companyLoginController.GetUserListByDepartmentid(int.Parse(Session["CompanyId"].ToString()));
                //gvCompanyUsers.DataSource = getUserListByCompanyId.Where(x => x.FirstName.StartsWith(txtSearch.Text.ToString())).ToList();
                //gvCompanyUsers.DataBind();

                List<CompanyLogin> getUserListByCompanyId = new List<CompanyLogin>();
                getUserListByCompanyId = companyLoginController.GetUserListByName(int.Parse(Session["CompanyId"].ToString()), txtSearch.Text);
                gvCompanyUsers.DataSource = getUserListByCompanyId;
                gvCompanyUsers.DataBind();
            }
            else {
                Bindusers();
            }
        }

        protected void gvCompanyUsers_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                CompanyUserAccess companyUserAccess = companyUserAccessController.FetchUserRoleIdByUserId(Convert.ToInt32(e.Row.Cells[1].Text), int.Parse(Session["CompanyId"].ToString()));
                (e.Row.FindControl("lblUserRole") as Literal).Text = companyUserAccess.userRoleName;
            }
        }
    }
}

