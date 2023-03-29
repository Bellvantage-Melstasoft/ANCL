using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;


namespace BiddingSystem
{
    public partial class ViewMyPurchaseRequests : System.Web.UI.Page
    {
        static int UserId = 1005;
        static int CompanyId = 4;

        static List<PrMasterV2> PrMasters;
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewMyPurchaseRequests.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewMyPrStatusLink";


                UserId = int.Parse(Session["UserId"].ToString());
                CompanyId = int.Parse(Session["CompanyId"].ToString());

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(UserId, CompanyId, 5, 6) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                if (UserId != 0)
                {
                    try
                    {
                        PrMasters = pr_MasterController.GetPrListForViewMyPr(CompanyId, UserId);
                        BindGV();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void BindGV()
        {

            gvPurchaseRequest.DataSource = PrMasters;
            gvPurchaseRequest.DataBind();
        }

        protected void gvPurchaseRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PrId = int.Parse(gvPurchaseRequest.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvPrDetails = e.Row.FindControl("gvPrDetails") as GridView;

                gvPrDetails.DataSource = PrMasters.Find(pr => pr.PrId == PrId).PrDetails;
                gvPrDetails.DataBind();
            }

        }

        protected void gvPrDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gvStatusLog = e.Row.FindControl("gvStatusLog") as GridView;
                GridView gvPrDetail = (gvStatusLog.NamingContainer as GridViewRow).NamingContainer as GridView;

                int PrId = int.Parse(gvPurchaseRequest.DataKeys[((e.Row.NamingContainer as GridView).NamingContainer as GridViewRow).RowIndex].Value.ToString());

                int prdId = int.Parse(gvPrDetail.DataKeys[e.Row.RowIndex].Value.ToString());

                gvStatusLog.DataSource = PrMasters.Find(pr => pr.PrId == PrId).PrDetails.Find(p => p.PrdId == prdId).PrDetailsStatusLogs;
                gvStatusLog.DataBind();
            }
        }

        protected void gvPurchaseRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                gvPurchaseRequest.PageIndex = e.NewPageIndex;
                BindGV();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}