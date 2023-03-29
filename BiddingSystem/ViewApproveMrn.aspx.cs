using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Text.RegularExpressions;

namespace BiddingSystem {
    public partial class ViewApproveMrn : System.Web.UI.Page {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        WarehouseControllerInterface WarehouseController = ControllerFactory.CreateWarehouseController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        SubDepartmentControllerInterface subDepartmentControllerInterface = ControllerFactory.CreateSubDepartmentController();
        MrnControllerV2 mrnControllerV2 = ControllerFactory.CreateMrnControllerV2();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewApproveMrn.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "approveMRNLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 12, 2) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");
                int SearchStatus = 0;
                ViewState["SearchStatus"] = SearchStatus;
              
                if(Session["UserDepartments"] != null)
                {
                    List<MrnMasterV2> mrnMaster = mrnControllerV2.FetchMrnListforApproval((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                    gvMrn.DataSource = mrnMaster;
                    gvMrn.DataBind();
                }
                else
                {
                    gvMrn.DataSource = null;
                    gvMrn.DataBind();
                }
                //List<MrnMasterV2> mrnMaster = mrnControllerV2.FetchMrnListforApprovalByDate((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList(), LocalTime.Now);
                
                //if (Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Count() > 0)
                //{
                //    {
                //        try
                //        {
                //            List<MrnMasterV2> mrnMaster = mrnControllerV2.FetchMrnListforApproval((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                //            gvMrn.DataSource = mrnMaster;
                //            gvMrn.DataBind();
                //        }
                //        catch (Exception ex)
                //        {
                //            throw ex;
                //        }
                //    }
                //}

            }
        }
        protected void btnBasicSearch_Click(object sender, EventArgs e) {
            try {
                int SearchStatus = 1;
                ViewState["SearchStatus"] = SearchStatus;
                if (rdbMonth.Checked) {
                    DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                    if (Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Count() > 0) {
                        {
                            try {
                                List<MrnMasterV2> mrnMaster = mrnControllerV2.FetchMrnListforApprovalByDate((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList(), date);
                                gvMrn.DataSource = mrnMaster;
                                gvMrn.DataBind();
                            }
                            catch (Exception ex) {
                                throw ex;
                            }
                        }
                    }

                }
                else {
                    //string newString = Regex.Replace(txtMrnCode.Text, "[^.0-9]", "");
                    //int mrnCode = int.Parse(newString);
                    String mrnCode = txtMrnCode.Text;
                    if (Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Count() > 0) {
                        {
                            try {
                                List<MrnMasterV2> mrnMaster = mrnControllerV2.FetchMrnListforApprovalByMrnCode((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList(), mrnCode);
                                gvMrn.DataSource = mrnMaster;
                                gvMrn.DataBind();
                            }
                            catch (Exception ex) {
                                throw ex;
                            }
                        }
                    }

                }
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }
        protected void gvMrnApp_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvMrn.PageIndex = e.NewPageIndex;
                if (ViewState["SearchStatus"].ToString() == "1" && ViewState["SearchStatus"] != null) {


                    if (rdbMonth.Checked) {
                        DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                        if (Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Count() > 0) {
                            {
                                try {
                                    List<MrnMasterV2> mrnMaster = mrnControllerV2.FetchMrnListforApprovalByDate((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList(), date);
                                    gvMrn.DataSource = mrnMaster;
                                    gvMrn.DataBind();
                                }
                                catch (Exception ex) {
                                    throw ex;
                                }
                            }
                        }

                    }
                    else {
                        //string newString = Regex.Replace(txtMrnCode.Text, "[^.0-9]", "");
                        //int mrnCode = int.Parse(newString);
                        string mrnCode = txtMrnCode.Text;
                        if (Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Count() > 0) {
                            {
                                try {
                                    List<MrnMasterV2> mrnMaster = mrnControllerV2.FetchMrnListforApprovalByMrnCode((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList(), mrnCode);
                                    gvMrn.DataSource = mrnMaster;
                                    gvMrn.DataBind();
                                }
                                catch (Exception ex) {
                                    throw ex;
                                }
                            }
                        }

                    }
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);

                }
                else if (ViewState["SearchStatus"].ToString() == "0" && ViewState["SearchStatus"] != null) {
                    List<MrnMasterV2> mrnMaster = mrnControllerV2.FetchMrnListforApproval((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                    gvMrn.DataSource = mrnMaster;
                    gvMrn.DataBind();
                }
            }
            catch (Exception) {

                throw;
            }
        }
        protected void lbtnView_Click(object sender, EventArgs e)
        {
            int MrnId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            Response.Redirect("MRNApproveNew.aspx?MrnId=" + MrnId);
        }
    }
}