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
    public partial class CreateSubDepartment : System.Web.UI.Page
    {

        #region Properies
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        UserSubDepartmentController userSubDepartmentController = ControllerFactory.CreateUserSubDepartment();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
       // public List<string> departmentList = new List<string>();
      //  List<SubDepartment> departments = new List<SubDepartment>();
        SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> departmentList = new List<string>();
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefDepartment";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabDepartment";
                    ((BiddingAdmin)Page.Master).subTabValue = "CreateSubDepartment.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "CreateSubDepartmentLink";
                    
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 11, 1) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                    {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
                List<SubDepartment> departments = new List<SubDepartment>();
                departments = subDepartmentController.getAllDepartmentList(int.Parse(Session["CompanyId"].ToString()));
                foreach (var item in departments)
                {
                    departmentList.Add(item.SubDepartmentName);

                }
                ViewState["departmentList"] = new JavaScriptSerializer().Serialize(departmentList);

                if (!IsPostBack)
                {
                    ddlHeadOfDepartment.DataSource = companyLoginController.GetUserListByDepartmentid(int.Parse(Session["CompanyId"].ToString()));
                    ddlHeadOfDepartment.DataValueField = "UserId";
                    ddlHeadOfDepartment.DataTextField = "Username";
                    ddlHeadOfDepartment.DataBind();
                    ddlHeadOfDepartment.Items.Insert(0, new ListItem("Select Head of Department", ""));
                    LoadGV();
                }
                msg.Visible = false;
            }

            catch (Exception)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {

                    List<int> UserIds = new List<int>();
                    for (int i = 0; i < ddlHeadOfDepartment.Items.Count; i++) {
                        if (ddlHeadOfDepartment.Items[i].Selected) {
                            UserIds.Add(int.Parse(ddlHeadOfDepartment.Items[i].Value));
                        }
                    }

                    int result = subDepartmentController.saveDepartment(txtDepartmentName.Text, txtPhoneNo.Text, int.Parse(Session["CompanyId"].ToString()), chkIsavtive.Checked == true ? 1 : 0, UserIds);
                    if (result != -1)
                    {
                        if (result > 0)
                        {
                            hdnShowSuccess.Value = "1";
                            clearFields();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                            

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on creating department'}); });   </script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Department already exists'}); });   </script>", false);
                    }
                }



                if (btnSave.Text == "Update")

                {
                    if (int.Parse(ViewState["subDepartmentID"].ToString()) != 0)
                    {

                        List<int> UserIds = new List<int>();
                        for (int i = 0; i < ddlHeadOfDepartment.Items.Count; i++) {
                            if (ddlHeadOfDepartment.Items[i].Selected) {
                                UserIds.Add(int.Parse(ddlHeadOfDepartment.Items[i].Value));
                            }
                        }

                        int result = subDepartmentController.updateSubDepartment(int.Parse(ViewState["subDepartmentID"].ToString()), txtDepartmentName.Text, txtPhoneNo.Text, int.Parse(Session["CompanyId"].ToString()), chkIsavtive.Checked == true ? 1 : 0, UserIds);
                        if (result != -1)
                        {
                            if (result > 0) {
                                hdnShowSuccess.Value = "1";
                                clearFields();
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                                
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on creating department'}); });   </script>", false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Department already exists'}); });   </script>", false);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please select a department'}); });   </script>", false);
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
                gvDepartments.DataSource = subDepartmentController.getDepartmentList(int.Parse(Session["CompanyId"].ToString()));
                gvDepartments.DataBind();

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
                ViewState["subDepartmentID"] = int.Parse(gvDepartments.Rows[x].Cells[1].Text);

                SubDepartment subDepartmentObj = subDepartmentController.getDepartmentByID(int.Parse(ViewState["subDepartmentID"].ToString()));
                if (subDepartmentObj.SubDepartmentID != 0)
                {
                    txtDepartmentName.Text = subDepartmentObj.SubDepartmentName;
                    txtPhoneNo.Text = subDepartmentObj.PhoneNo;
                    if (subDepartmentObj.IsActive == 1)
                    {
                        chkIsavtive.Checked = true;
                    }
                    else
                    {
                        chkIsavtive.Checked = false;
                    }

                    ddlHeadOfDepartment.DataSource = companyLoginController.GetUserListByDepartmentid(int.Parse(Session["CompanyId"].ToString()));
                    ddlHeadOfDepartment.DataValueField = "UserId";
                    ddlHeadOfDepartment.DataTextField = "Username";
                    ddlHeadOfDepartment.DataBind();
                    ddlHeadOfDepartment.Items.Insert(0, new ListItem("Select Head of Department", ""));

                    //if (subDepartmentObj.HeadOfDepartmentID != 0)
                    //{
                    //    ddlHeadOfDepartment.SelectedValue = subDepartmentObj.HeadOfDepartmentID.ToString();
                    //}


                    List<UserSubDepartment> subDepartmentHeads = userSubDepartmentController.getDepartmentHeads(subDepartmentObj.SubDepartmentID);

                    for (int i = 0; i < subDepartmentHeads.Count; i++) {

                        if (ddlHeadOfDepartment.Items.FindByValue(subDepartmentHeads[i].UserId.ToString()) != null) {

                            var s = ddlHeadOfDepartment.Items.FindByValue(subDepartmentHeads[i].UserId.ToString());
                            ddlHeadOfDepartment.Items.FindByValue(subDepartmentHeads[i].UserId.ToString()).Selected = true;
                        }

                    }

                    btnSave.Text = "Update";
                }
            }
            catch (Exception  ex)
            {

                throw;
            }
        }

        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                
                int status = subDepartmentController.deleteSubDepartment(int.Parse(hdnSubDepartmentId.Value.ProcessString()));
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('successMessage').innerHTML = \"Department has been deleted successfully\"; $('#SuccessAlert').modal('show'); });   </script>", false);
                    //DisplayMessage("Company has been Deleted Successfully", false);
                    clearFields();
                    LoadGV();
                    hdnShowSuccess.Value = "1";

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Deleting department failed'}); });   </script>", false);
                    //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Deleting department failed\"; $('#errorAlert').modal('show'); });   </script>", false);
                    //DisplayMessage("Error on Delete Company", true);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string getJsonDepartmentList() {
            
            var departmentList = new JavaScriptSerializer().Deserialize<List<string>>(ViewState["departmentList"].ToString());
              var DataList = departmentList;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }

        private void clearFields()
        {
            txtDepartmentName.Text = "";
            txtPhoneNo.Text = "";
            btnSave.Text = "Save";
            chkIsavtive.Checked = false;


            ddlHeadOfDepartment.DataSource = companyLoginController.GetUserListByDepartmentid(int.Parse(Session["CompanyId"].ToString()));
            ddlHeadOfDepartment.DataValueField = "UserId";
            ddlHeadOfDepartment.DataTextField = "Username";
            ddlHeadOfDepartment.DataBind();
            ddlHeadOfDepartment.Items.Insert(0, new ListItem("Select Head of Department", ""));
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
                gvDepartments.PageIndex = e.NewPageIndex;
                LoadGV();
            }
            catch (Exception)
            {

            }
        }

        //        protected void btnDeleteCompany_Click(object sender, ImageClickEventArgs e)
        //        {
        //            int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
        //            ViewState["subDepartmentID"] = int.Parse(gvDepartments.Rows[x].Cells[1].Text);

        //            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, 
        //                this.Updatepanel1.GetType(),
        //                "none", "<script> "
        //                +"  $(document).ready(function () {" +
        //                "swal({" +
        //                "title: 'Are you sure?'," +
        //                        "text: \"You will be able to revert this\"," +
        //                        "type: 'warning'," +
        //                        "showCancelButton: true," +
        //                        "confirmButtonColor: '#3085d6'," +
        //                        "cancelButtonColor: '#d33'," +
        //                        "confirmButtonText: 'Yes, delete it!'" +
        //                    "}).then((result) => {" +
        //                    "if (result.value)" +
        //                    "{ document.getElementById('" + btnDelete.ClientID + "').click();}}); " +
        //                    "}); " +
        //                "</script>", false);


        //        }
        //    }
        //}

        protected void btnDeleteCompany_Click(object sender, ImageClickEventArgs e) {
            int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
            ViewState["SubDepartmentId"] = int.Parse(gvDepartments.Rows[x].Cells[1].Text);

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