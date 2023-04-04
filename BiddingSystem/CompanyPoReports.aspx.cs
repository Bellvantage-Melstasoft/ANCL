using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class CompanyPoReports : System.Web.UI.Page
    {
        // static string UserId = string.Empty;
        //  int CompanyId = 0;
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((BiddingAdmin)Page.Master).mainTabValue = "hrefReports";
            ((BiddingAdmin)Page.Master).subTabTitle = "subTabReports";
            ((BiddingAdmin)Page.Master).subTabValue = "CompanyPoReports.aspx";
            ((BiddingAdmin)Page.Master).subTabId = "poReportsLink";

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                //    CompanyId = int.Parse(Session["CompanyId"].ToString());
                //   UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 8, 2) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                        //List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
                        //pOMasterListByDepartmentid = pOMasterController.GetPoMasterListByDepartmentId(int.Parse(Session["CompanyId"].ToString())).OrderByDescending(x => x.PoID).ToList();
                        //gvPurchaseOrder.DataSource = pOMasterListByDepartmentid;
                        //gvPurchaseOrder.DataBind();

                        BindDataDropDown();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int PoId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                PoId = int.Parse(gvPurchaseOrder.Rows[x].Cells[0].Text);


                Session["PoId"] = PoId;
                Response.Redirect("ViewPOReport.aspx?PoId=" + PoId);
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }


        //Bind Department to dropdown
        private void BindDataDropDown()
        {
            List<SubDepartment> departments = new List<SubDepartment>();
            SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();

            departments = subDepartmentController.getAllDepartmentList(int.Parse(Session["CompanyId"].ToString()));

            ddlDepartment.DataSource = departments;
            ddlDepartment.DataValueField = "SubDepartmentID";
            ddlDepartment.DataTextField = "SubDepartmentName";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("-Select Department-", ""));

        }

        protected void btnPoCodeSearch_Click(object sender, EventArgs e)
        {
            ddlStatus.SelectedIndex = 0;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            string poCode = txtPoCode.Text;
            List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
            pOMasterListByDepartmentid = pOMasterController.GetPoMasterListByDepartmentId(int.Parse(Session["CompanyId"].ToString())).Where(x => x.POCode.Contains(poCode)).ToList();
            gvPurchaseOrder.DataSource = pOMasterListByDepartmentid;
            gvPurchaseOrder.DataBind();

        }
        protected void btnPoStatusSearch_Click(object sender, EventArgs e)
        {
            txtPoCode.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            string status = ddlStatus.SelectedValue;
            List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
            pOMasterListByDepartmentid = pOMasterController.GetPoMasterListByDepartmentId(int.Parse(Session["CompanyId"].ToString())).Where(x => x.IsApproved == int.Parse(status)).ToList();
            gvPurchaseOrder.DataSource = pOMasterListByDepartmentid;
            gvPurchaseOrder.DataBind();

        }
        protected void btnPoDateSearch_Click(object sender, EventArgs e)
        {
            ddlStatus.SelectedIndex = 0;
            txtPoCode.Text = "";
            string status = ddlStatus.SelectedValue;
            List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
            pOMasterListByDepartmentid = pOMasterController.GetPoMasterListByByDaterange(int.Parse(Session["CompanyId"].ToString()), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text));
            gvPurchaseOrder.DataSource = pOMasterListByDepartmentid;
            gvPurchaseOrder.DataBind();
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $(document).ready(function () {  $('#ContentSection_txtStartDate').attr('data-date', moment($('#ContentSection_txtStartDate').val(), 'YYYY-MM-DD').format($('#ContentSection_txtStartDate').attr('data-date-format')));$('#ContentSection_txtEndDate').attr('data-date', moment($('#ContentSection_txtEndDate').val(), 'YYYY-MM-DD').format($('#ContentSection_txtEndDate').attr('data-date-format'))); });   </script>", false);
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            List<POMaster> pOMasterList = new List<POMaster>();
            pOMasterList = pOMasterController.GetAllPOMAster();
            gvPurchaseOrder.DataSource = pOMasterList;
            gvPurchaseOrder.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<POMaster> pOMasterList = new List<POMaster>();
            pOMasterList = pOMasterController.GetAllPOMAster();

            if (ddlStatus.SelectedValue != "")
            {
                pOMasterList = pOMasterList.Where(x => x.IsApproved == Convert.ToInt32(ddlStatus.SelectedValue)).ToList();

            }
            //if (ddlPRType.SelectedValue != "")
            //{
            //    pOMasterList = pOMasterList.Where(x => x. == Convert.ToInt32(ddlPRType.SelectedValue)).ToList();
            //}

            //if (ddlPurchasingType.SelectedValue != "")
            //{
            //    pOMasterList = pOMasterList.Where(x => x.PurchaseType == Convert.ToInt32(ddlPurchasingType.SelectedValue)).ToList();

            //}

            if (txtPoCode.Text != "")
            {
                pOMasterList = pOMasterList.Where(x => x.PrCode == txtPoCode.Text).ToList();
            }

            if (ddlDepartment.SelectedValue != "")
            {
                pOMasterList = pOMasterList.Where(x => x.DepartmentId == Convert.ToInt32(ddlDepartment.SelectedValue)).ToList();
            }

            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                pOMasterList = pOMasterList.Where(x => x.CreatedDate <= DateTime.Parse(txtEndDate.Text) && x.CreatedDate >= DateTime.Parse(txtStartDate.Text)).ToList();
            }

            gvPurchaseOrder.DataSource = pOMasterList;
            gvPurchaseOrder.DataBind();
        }
    }
}