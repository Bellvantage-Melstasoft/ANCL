using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class CompanyMrnReports : System.Web.UI.Page
    {
       
     //   static string UserId = string.Empty;
      //  int CompanyId = 0;
      //  int DepartmentId = 0;
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        MRNmasterController mrnmasterController = ControllerFactory.CreateMRNmasterController();
     //   static List<MrnMaster> mrnMaster = new List<MrnMaster>();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((BiddingAdmin)Page.Master).mainTabValue = "hrefReports";
            ((BiddingAdmin)Page.Master).subTabTitle = "subTabReports";
            ((BiddingAdmin)Page.Master).subTabValue = "CompanyMrnReports.aspx";
            ((BiddingAdmin)Page.Master).subTabId = "mrnReportsLink";

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
             //   CompanyId = int.Parse(Session["CompanyId"].ToString());
            //    UserId = Session["UserId"].ToString();
             //   DepartmentId = int.Parse(Session["SubDepartmentID"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 8, 1) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                if (int.Parse(Session["UserId"].ToString()) != 0)
                {
                    try
                    {
                        
                        
                        List<UserSubDepartment> departments = ControllerFactory.CreateUserSubDepartment().getUserSubDepartmentdetails(int.Parse(Session["UserId"].ToString()));
                        List<int> UserDepartmentIds = new List<int>();
                        for (int i = 0; i < departments.Count; i++) {
                            UserDepartmentIds.Add(departments[i].DepartmentId);
                        }

                        //var  mrnMaster = mrnmasterController.getMrnByDepartment(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserDepartments"].ToString())).OrderByDescending(x => x.CreatedDate).ToList();
                        var mrnMaster = mrnmasterController.getMrnByDepartment(int.Parse(Session["CompanyId"].ToString()), UserDepartmentIds).OrderByDescending(x => x.CreatedDate).ToList();
                        ViewState["mrnMaster"] = new JavaScriptSerializer().Serialize(mrnMaster);
                        //List<MrnDetails> mrnDetails = new List<MrnDetails>();
                        //foreach (MrnMaster item in mrnMaster)
                        //{
                        //    mrnDetails = mrnmasterController.FetchMRNItemDetails(item.MrnID, CompanyId);
                        //    item.MrnDetails = mrnDetails;
                        //}
                        //List<MrnMaster> tempmrn = mrnMaster.Where(x => x.MrnDetails.Count == 0).ToList();
                        //mrnMaster = mrnMaster.Where(x => x.MrnDetails.Count > 0).ToList();
                        //mrnMaster = mrnMaster.OrderBy(x => x.MrnDetails.Max(t => t.Status)).ToList();
                        //mrnMaster.AddRange(tempmrn);
                        //gvMRN.DataSource = mrnMaster;
                        //gvMRN.DataBind();
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Assign user To a department', showConfirmButton: false,timer: 1500}); });   </script>", false);

                    }
                }
            }
        }

        
        protected void btnMrnCodeSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //var  mrnMaster = mrnmasterController.getMrnByDepartment(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["SubDepartmentID"].ToString())).OrderByDescending(x => x.CreatedDate).ToList();
                //  ViewState["mrnMaster"] = new JavaScriptSerializer().Serialize(mrnMaster);
                if (txtMrnCode.Text != "") {
                    //string newString = Regex.Replace(txtMrnCode.Text, "[^.0-9]", "");
                    //int MrnCode = int.Parse(newString);
                    string MrnCode = txtMrnCode.Text;

                    //mrnMaster = mrnMaster.FindAll(x => x.MrnCode == MrnCode).ToList();
                    gvMRN.DataSource = mrnmasterController.FetchMrnByMrnCode(int.Parse(Session["CompanyId"].ToString()), MrnCode);
                    gvMRN.DataBind();
                }
                //}else
                //{
                //    gvMRN.DataSource = mrnMaster;
                //    gvMRN.DataBind();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //protected void btnMrnStatusSearch_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int status = int.Parse(ddlStatus.SelectedValue);
        //        //var  mrnMaster = mrnmasterController.getMrnByDepartment(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["SubDepartmentID"].ToString())).OrderByDescending(x => x.CreatedDate).ToList();
        //        //  ViewState["mrnMaster"] = new JavaScriptSerializer().Serialize(mrnMaster);
        //        //  mrnMaster = mrnMaster.FindAll(x => x.IsApproved == Convert.ToInt32(ddlStatus.SelectedValue)).ToList();

        //        List<MrnMaster> mrnMaster = mrnmasterController.FetchMRNByStatus(int.Parse(Session["CompanyId"].ToString()), status);



        //        gvMRN.DataSource = mrnMaster;
        //        gvMRN.DataBind();
        //        txtStartDate.Text = "";
        //        txtEndDate.Text = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        protected void btnMrnDateSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //string status = ddlStatus.SelectedValue;
                //var  mrnMaster = mrnmasterController.getMrnByDepartment(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["SubDepartmentID"].ToString())).OrderByDescending(x => x.CreatedDate).ToList();
                //  if (status != "")
                //  {
                //      mrnMaster = mrnMaster.FindAll(t => t.CreatedDate >= DateTime.ParseExact(txtStartDate.Text, "yyyy-M-dd", null) && t.CreatedDate <= DateTime.ParseExact(txtEndDate.Text, "yyyy-M-dd", null) );
                //      mrnMaster = mrnMaster.FindAll(x=> x.IsApproved == Convert.ToInt32(ddlStatus.SelectedValue)).ToList();
                //  }
                //  else
                //  {
                //      mrnMaster = mrnMaster.FindAll(t => t.CreatedDate >= DateTime.ParseExact(txtStartDate.Text, "yyyy-M-dd", null) && t.CreatedDate <= DateTime.ParseExact(txtEndDate.Text, "yyyy-M-dd", null));
                //  }
                //  gvMRN.DataSource = mrnMaster;
                //  gvMRN.DataBind();


                if (txtStartDate.Text != "" && txtEndDate.Text != "") {
                    //    txtStartDate.Text = Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd");
                    //}
                    //if (txtEndDate.Text != "") {
                    //    txtEndDate.Text = Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd");
                    //}
                    List<MrnMaster> mrnMaster = mrnmasterController.FetchMRNByDate(int.Parse(Session["CompanyId"].ToString()), DateTime.Parse(txtEndDate.Text), DateTime.Parse(txtStartDate.Text));

                    gvMRN.DataSource = mrnMaster;
                    gvMRN.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int MrnId = int.Parse(gvMRN.Rows[x].Cells[0].Text);
                Session["MrnId"] = MrnId;
                Response.Redirect("ViewMRNNReport.aspx?MrnId=" + MrnId);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('ViewMRNNReport.aspx?MrId=" + MrnId + "');", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}