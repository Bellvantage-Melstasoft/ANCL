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
    public partial class ApproveForBidOpening : System.Web.UI.Page
    {
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        List<PR_Details> _PR_DetailsList = new List<PR_Details>();
        static string UserId = string.Empty;
        private int CompanyId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ApproveForBidOpening.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "approveforBidOpeninggLink";

                UserId = Session["UserId"].ToString();
                CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 6, 2) && companyLogin.Usertype != "S" )|| companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

            if(! IsPostBack)
            {
                try
                {
                    _PR_DetailsList = pr_DetailController.FetchBidSubmissionDetails().Distinct().ToList();

                    var objs = (from c in _PR_DetailsList
                                orderby c.PrId
                                select c).GroupBy(g => g.PrId).Select(x => x.FirstOrDefault());

                    gvPurchaseRequest.DataSource = objs.ToList().Where(p=> p.DepartmentId== int.Parse(Session["CompanyId"].ToString())).OrderByDescending(g=>g.PrId).ToList();
                    gvPurchaseRequest.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else{

            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    List<PR_Details> pr_DetailsDup = new List<PR_Details>();
                    //foreach (var item in _PR_DetailsList)
                    //{
                    //    pr_DetailsDup.AddRange(pr_DetailController.FetchDetailsRejectedPR(item.PrId).ToList());
                    //}
                    string prId = gvPurchaseRequest.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gvPRDetails = e.Row.FindControl("gvPRDetails") as GridView;


                    List<Bidding> _bidding = new List<Bidding>();
                    _bidding = biddingController.FetchRejectedAndApprovedBids(int.Parse(prId), int.Parse(Session["CompanyId"].ToString())).Where(x => x.IsApproveToViewInSupplierPortal == 1 || x.IsApproveToViewInSupplierPortal == 2 || x.IsApproveToViewInSupplierPortal == 0 || x.IsApproveToViewInSupplierPortal == 4).ToList();

                    var objs = (from c in _bidding
                                orderby c.ItemId
                                select c).GroupBy(g => g.ItemId).Select(x => x.FirstOrDefault());

                   // pr_DetailsDup.AddRange(pr_DetailController.FetchDetailsRejectedPR(int.Parse(prId)).ToList());
                    gvPRDetails.DataSource = objs.ToList();
                    gvPRDetails.DataBind();
                }
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
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int prid = int.Parse(gvPurchaseRequest.Rows[x].Cells[1].Text);
                Response.Redirect("ApproveForBidOpeningView.aspx?PrId=" + prid);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}