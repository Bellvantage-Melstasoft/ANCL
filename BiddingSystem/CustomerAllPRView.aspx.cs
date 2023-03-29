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
    public partial class CustomerAllPRView : System.Web.UI.Page
    {
        static string UserId = string.Empty;
        int CompanyId = 0;
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();

        protected void Page_Load(object sender, EventArgs e)
        {
            //UserId = Request.QueryString.Get("UserId");
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "CustomerALLPRView.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "adminApprovePRLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                
                if (!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 4, 3) && companyLogin.Usertype != "S")
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
                if (int.Parse(UserId) != 0)
                {
                    try
                    {
                        bindCompanies();
                        BindPR();


                    }
                    catch (Exception ex)
                    {
                   
                    }
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int prid = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                prid = int.Parse(gvPurchaseRequest.Rows[x].Cells[0].Text);
                string departmentId = gvPurchaseRequest.Rows[x].Cells[2].Text;
                Session["departmentId"] = departmentId;
                Response.Redirect("CustomerViewAllPurchaseRequisition.aspx?PrId=" + prid); 
            }
            catch (Exception ex)
            {
             
            }
            
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(ddlCompany.SelectedIndex == 0)
                {
                    List<PR_Master> pr_Master = new List<PR_Master>();
                    pr_Master = pr_MasterController.FetchALApprovePRData().OrderByDescending(x => x.PrId).ToList();
                    gvPurchaseRequest.DataSource = pr_Master;
                    gvPurchaseRequest.DataBind();
                }
                else
                {
                    List<PR_Master> pr_Master = new List<PR_Master>();
                    pr_Master = pr_MasterController.FetchALApprovePRData().Where(v=>v.DepartmentId==int.Parse(ddlCompany.SelectedValue)).OrderByDescending(x => x.PrId).ToList();
                    gvPurchaseRequest.DataSource = pr_Master;
                    gvPurchaseRequest.DataBind();
                }
            }
            catch (Exception)
            {

               
            }
        }

        private void bindCompanies()
        {
            try
            {
                ddlCompany.DataSource = companyDepartmentController.GetDepartmentList();
                ddlCompany.DataValueField = "DepartmentID";
                ddlCompany.DataTextField = "DepartmentName";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Select Department", "0"));
            }
            catch (Exception)
            {

             
            }
        }

        protected void gvPurchaseRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if(ddlCompany.SelectedIndex != 0)
                {
                    List<PR_Master> pr_Master = new List<PR_Master>();
                    pr_Master = pr_MasterController.FetchALApprovePRData().Where(d => d.DepartmentId == int.Parse(ddlCompany.SelectedValue)).OrderByDescending(x => x.PrId).ToList();
                    gvPurchaseRequest.DataSource = pr_Master;
                    gvPurchaseRequest.PageIndex = e.NewPageIndex;
                    gvPurchaseRequest.DataBind();
                }
                else
                {
                    List<PR_Master> pr_Master = new List<PR_Master>();
                    pr_Master = pr_MasterController.FetchALApprovePRData().OrderByDescending(x => x.PrId).ToList();
                    gvPurchaseRequest.DataSource = pr_Master;
                    gvPurchaseRequest.PageIndex = e.NewPageIndex;
                    gvPurchaseRequest.DataBind();
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void BindPR()
        {
            try
            {
                List<PR_Master> pr_Master = new List<PR_Master>();
                pr_Master = pr_MasterController.FetchALApprovePRData().OrderByDescending(x => x.PrId).ToList();
                gvPurchaseRequest.DataSource = pr_Master;
                gvPurchaseRequest.DataBind();
            }
            catch (Exception)
            {

            }
        }
    }
}