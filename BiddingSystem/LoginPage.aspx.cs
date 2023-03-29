using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Domain;
using CLibrary.Common;
using System.Web.Services;
using System.Web.Script.Services;
using BiddingSystem.ViewModels.CS;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class LoginPage : System.Web.UI.Page
    {
        static CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        static WarehouseControllerInterface warehouseController = ControllerFactory.CreateWarehouseController();
        static SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();
        static UserSubDepartmentController userSubDepartmentController = ControllerFactory.CreateUserSubDepartment();
        static UserWarehouseController userWarehouseController = ControllerFactory.CreateUserWarehouse();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Abandon();
                if (Request.Cookies["UserNameA"] != null && Request.Cookies["PasswordA"] != null)
                {
                    txtUserName.Text = Request.Cookies["UserNameA"].Value;
                    txtPwd.Attributes["value"] = Request.Cookies["PasswordA"].Value;
                }
                else
                {
                    txtUserName.Text = "";
                    txtPwd.Attributes["value"] = null;
                }
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                CompanyLogin companyLoginObj = companyLoginController.GetCompanyLogin(txtUserName.Text, txtPwd.Text.Encrypt());
                //CompanyLogin companyLoginObj = companyLoginController.GetCompanyLogin(txtUserName.Text, txtPwd.Text);

                if (companyLoginObj.UserId != -1)
                {
                    if (companyLoginObj.UserId != -2)
                    {
                        if (companyLoginObj.UserId != -3)
                        {
                            if (chkRemember.Checked)
                            {
                                Response.Cookies["UserNameA"].Expires = LocalTime.Now.AddDays(30);
                                Response.Cookies["PasswordA"].Expires = LocalTime.Now.AddDays(30);
                            }
                            else
                            {
                                Response.Cookies["UserNameA"].Expires = LocalTime.Now.AddDays(-1);
                                Response.Cookies["PasswordA"].Expires = LocalTime.Now.AddDays(-1);
                            }
                            Response.Cookies["UserNameA"].Value = txtUserName.Text.Trim();
                            Response.Cookies["PasswordA"].Value = txtPwd.Text.Trim();

                            Session["UserNameA"] = txtUserName.Text.Trim();
                            Session["CompanyId"] = companyLoginObj.DepartmentId;
                            Session["UserId"] = companyLoginObj.UserId;
                            Session["SubDepartmentID"] = companyLoginObj.SubDepartmentID;
                            Session["DesignationId"] = companyLoginObj.DesignationId;

                           
                            List<UserWarehouse> userWarehouse = userWarehouseController.getUserWarehousedetails(companyLoginObj.UserId);
                            if (userWarehouse != null && userWarehouse.Count > 0) {
                                Session["UserWarehouses"] = userWarehouse;

                            }
                            else {
                                Session["UserWarehouses"] = null;
                            }

                            List<UserSubDepartment> departments = userSubDepartmentController.getUserSubDepartmentdetails(companyLoginObj.UserId);
                            if (departments != null && departments.Count > 0) {
                                Session["UserDepartments"] = departments;
                            }
                            else {
                                Session["UserDepartments"] = null;
                            }


                            var result = subDepartmentController.isUserHeadOfProcurement(companyLoginObj.UserId);
                            if (result > 0)
                            {
                                Session["IsHeadOfProcurement"] = "1";
                            }
                            else
                            {
                                Session["IsHeadOfProcurement"] = "0";
                            }
                             Response.Redirect("AdminDashboard.aspx", false);
                            //Response.Redirect("CreatePR_V2.aspx", false);
                        }
                        else
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Your Company Has Been Inactivated', showConfirmButton: true,timer: 4000}); });   </script>", false);
                            lblError.Text = "Your Company Has Been Inactivated";
                        }
                    }
                    else
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'You Have Been Inactivated', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        lblError.Text = "You Have Been Inactivated";
                    }
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Invalid Credential', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    lblError.Text = "Invalid Credential";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string LoginViaService(string UserName, string Password) {
            ResultVM response = new ResultVM();
            try {

                CompanyLogin companyLoginObj = ControllerFactory.CreateCompanyLoginController().GetCompanyLogin(UserName, Password.Encrypt());

                if (companyLoginObj.UserId != -1) {
                    if (companyLoginObj.UserId != -2) {
                        if (companyLoginObj.UserId != -3) {

                            HttpContext.Current.Session["UserNameA"] = UserName;
                            HttpContext.Current.Session["CompanyId"] = companyLoginObj.DepartmentId;
                            HttpContext.Current.Session["UserId"] = companyLoginObj.UserId;
                            HttpContext.Current.Session["SubDepartmentID"] = companyLoginObj.SubDepartmentID;
                            HttpContext.Current.Session["DesignationId"] = companyLoginObj.DesignationId;



                            List<UserWarehouse> userWarehouse = userWarehouseController.getUserWarehousedetails(companyLoginObj.UserId);
                            if (userWarehouse != null && userWarehouse.Count > 0) {
                                HttpContext.Current.Session["UserWarehouses"] = userWarehouse;

                            }
                            else {
                                HttpContext.Current.Session["UserWarehouses"] = null;
                            }

                            List<UserSubDepartment> departments = userSubDepartmentController.getUserSubDepartmentdetails(companyLoginObj.UserId);
                            if (departments != null && departments.Count > 0) {
                                HttpContext.Current.Session["UserDepartments"] = departments;
                            }
                            else {
                                HttpContext.Current.Session["UserDepartments"] = null;
                            }


                            var result = subDepartmentController.isUserHeadOfProcurement(companyLoginObj.UserId);
                            if (result > 0) {
                                HttpContext.Current.Session["IsHeadOfProcurement"] = "1";
                            }
                            else {
                                HttpContext.Current.Session["IsHeadOfProcurement"] = "0";
                            }

                            response.Status = 200;
                            response.Data = "Success";

                            return new JavaScriptSerializer().Serialize(response);

                        }
                        else {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Your Company Has Been Inactivated', showConfirmButton: true,timer: 4000}); });   </script>", false);

                            response.Status = 401;
                            response.Data = "Your Company Has Been Inactivated";

                            return new JavaScriptSerializer().Serialize(response);
                        }
                    }
                    else {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'You Have Been Inactivated', showConfirmButton: true,timer: 4000}); });   </script>", false);

                        response.Status = 401;
                        response.Data = "You Have Been Inactivated";

                        return new JavaScriptSerializer().Serialize(response);
                    }
                }
                else {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Invalid Credential', showConfirmButton: true,timer: 4000}); });   </script>", false);

                    response.Status = 401;
                    response.Data = "Invalid Credential";

                    return new JavaScriptSerializer().Serialize(response);
                }
            }
            catch (Exception ex) {
                response.Status = 500;
                response.Data = "Internal Server Error";

                return new JavaScriptSerializer().Serialize(response);
            }
        }
    }
}