using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class ViewPrForManualQuotationSubmission : System.Web.UI.Page
    {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
       // CompanyLogin companyLogin = new CompanyLogin();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefManualBids";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabManualBids";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForManualQuotationSubmission.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ManualBidsLink";

                ViewState["UserId"] = int.Parse(Session["UserId"].ToString());
                ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
               var companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                ViewState["companyLogin"] = new JavaScriptSerializer().Serialize(companyLogin);

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 7, 3) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                        //var PrMasters = pr_MasterController.GetPrListForManualQuotationSubmission(int.Parse(Session["CompanyId"].ToString()),int.Parse(Session["UserId"].ToString()));                       
                        var PrMasters = pr_MasterController.GetPrListForManualQuotationSubmissionWithItem(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));

                        ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(PrMasters); 
                        gvPurchaseRequest.DataSource = PrMasters;
                        gvPurchaseRequest.DataBind();
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
                GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
                int BidId = int.Parse(row.Cells[1].Text);
                Response.Redirect("manualBidSubmission.aspx?BidId="+ BidId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnViewNew_Click(object sender, EventArgs e) {
            try {
                GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
                int BidId = int.Parse(row.Cells[1].Text);
                int PurchaseTpe = int.Parse(row.Cells[13].Text);
                if (PurchaseTpe == 1) {
                    Response.Redirect("manualBidSubmissionNew.aspx?BidId=" + BidId);
                }
                else if (PurchaseTpe == 2) {
                    Response.Redirect("manualBidSubmissionNewImports.aspx?BidId=" + BidId);
                }
            }
            catch (Exception ex) {
                throw ex;
            }

        }


        protected void gvPurchaseRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PrId = int.Parse(gvPurchaseRequest.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvBids = e.Row.FindControl("gvBids") as GridView;
                List<PrMasterV2> PrMasters = new JavaScriptSerializer().Deserialize<List<PrMasterV2>>(ViewState["PrMaster"].ToString());

                gvBids.DataSource = PrMasters.Find(pr => pr.PrId == PrId).Bids;
                gvBids.DataBind();
            }
        }

        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;
                GridView Bids = (gvBidItems.NamingContainer as GridViewRow).NamingContainer as GridView;
                int PrId = int.Parse(gvPurchaseRequest.DataKeys[((e.Row.NamingContainer as GridView).NamingContainer as GridViewRow).RowIndex].Value.ToString());
                int bidId = int.Parse(Bids.DataKeys[e.Row.RowIndex].Value.ToString());
                List<PrMasterV2> PrMasters = new JavaScriptSerializer().Deserialize<List<PrMasterV2>>(ViewState["PrMaster"].ToString());
                gvBidItems.DataSource = PrMasters.Find(pr => pr.PrId == PrId).Bids.Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();
            }
        }
    }
}