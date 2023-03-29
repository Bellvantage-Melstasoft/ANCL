using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace BiddingSystem
{
    public partial class CompanyClosedBids : System.Web.UI.Page
    {
       // static string UserId = string.Empty;
       // static int CompanyId = 0;
      //  static List<Bidding> Bids;

        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CompanyClosedBids.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "closedBidsLink";

            //    UserId = Session["UserId"].ToString();
            //    CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 4) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                //var Bids = biddingController.GetClosedBids(int.Parse(Session["CompanyId"].ToString()));
                //ViewState["Bids"] = new JavaScriptSerializer().Serialize(Bids);
                //GVBind();
            }

        }

        //---------Bind GV
        private void GVBind()
        {
            try
            {
                gvBids.DataSource = new JavaScriptSerializer().Deserialize<List<Bidding>>(ViewState["Bids"].ToString());
                gvBids.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnBasicSearch_Click(object sender, EventArgs e) {
            try {
                if (rdbPr.Checked) {

                    //string newString = Regex.Replace(txtPR.Text, "[^.0-9]", "");
                    //int prCode = int.Parse(newString);
                    string prCode = txtPR.Text;

                    var Bids = biddingController.GetClosedBidsByPrCode(int.Parse(Session["CompanyId"].ToString()), prCode);
                    ViewState["Bids"] = new JavaScriptSerializer().Serialize(Bids);

                    gvBids.DataSource = Bids;
                    gvBids.DataBind();
                }
                else {
                    string newString = Regex.Replace(txtBidCode.Text, "[^.0-9]", "");
                    int bidCode = int.Parse(newString);
                    var Bids = biddingController.GetClosedBidsByBidCode(int.Parse(Session["CompanyId"].ToString()), bidCode);
                    ViewState["Bids"] = new JavaScriptSerializer().Serialize(Bids);
                    gvBids.DataSource = Bids;
                    gvBids.DataBind();
                }

            }
            catch (Exception ex) {
                // ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }
        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());

                gvBidItems.DataSource = new JavaScriptSerializer().Deserialize<List<Bidding>>(ViewState["Bids"].ToString()).Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();
            }
        }
    }
}