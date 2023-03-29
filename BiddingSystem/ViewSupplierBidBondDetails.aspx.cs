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
using BiddingSystem.Helpers;

namespace BiddingSystem
{
    public partial class ViewSupplierBidBondDetails : System.Web.UI.Page
    {
       // static List<PR_Master> PrMasters;
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefManualBids";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabManualBids";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewSupplierBidBondDetails.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ManualBidsLink";


               //int UserId = int.Parse(Session["UserId"].ToString());
               //int CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

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
                        var PrMasters = pr_MasterController.GetPrListForManualQuotationSubmission(int.Parse(Session["CompanyId"].ToString()),int.Parse(Session["UserId"].ToString()));
                        ViewState["PrMasters"] = new JavaScriptSerializer().Serialize(PrMasters);

                        gvPurchaseRequest.DataSource = PrMasters.OrderBy(pr=> pr.CreatedDate);
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
                Response.Redirect("UpdateSupplierBidBondDetails.aspx?BidId=" + BidId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void gvPurchaseRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PrId = int.Parse(gvPurchaseRequest.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvBids = e.Row.FindControl("gvBids") as GridView;

                gvBids.DataSource = new JavaScriptSerializer().Deserialize<List<PR_Master>>(ViewState["PrMasters"].ToString()).Find(pr => pr.PrId == PrId).Bids.OrderByDescending(x => x.CreateDate);
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

                List<BiddingItem> list = new JavaScriptSerializer().Deserialize<List<PR_Master>>(ViewState["PrMasters"].ToString()).Find(pr => pr.PrId == PrId).Bids.Find(b => b.BidId == bidId).BiddingItems;
                list = TabulationCommon.RemoveNull(list);
                gvBidItems.DataSource = list;
                gvBidItems.DataBind();
            }
        }
    }
}