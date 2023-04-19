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
    public partial class AgeAnalysisReport : System.Web.UI.Page
    {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        AgeAnalysisController ageAnalysisController = ControllerFactory.CreateAgeAnalysisController();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefSupplier";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabSupplier";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyUpdatingAndRatingSupplier.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "editSupplierLink";

                    ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                    ViewState["userId"] = Session["UserId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["userId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 3, 3) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                    BindDataSource();

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void BindDataSource()
        {
            List<AgeAnalysis> ageAnalysis = new List<AgeAnalysis>();
            ageAnalysis = ageAnalysisController.GetAgeAnalysis();
            gvAgeAnalysis.DataSource = ageAnalysis;
            gvAgeAnalysis.DataBind();
        }

        protected void gvAgeAnalysis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToInt32(e.Row.Cells[7].Text) > 0)
                {
                    DateTime date = DateTime.Parse(e.Row.Cells[1].Text);

                    // Calculate the difference in days
                    TimeSpan diff = DateTime.Now.Date - date;
                    int days = diff.Days;
                    e.Row.Cells[8].Text = days.ToString() + " Days";
                }
                else
                {
                    e.Row.Cells[8].Text = "No waiting";
                }
            }
        }
    }
}