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
using BiddingSystem.Helpers;
using System.Text.RegularExpressions;

namespace BiddingSystem
{
    public partial class CompanyClosedManualBidsP : System.Web.UI.Page
    {
       // static string UserId = string.Empty;
      //  static int CompanyId = 0;
      //  static List<Bidding> Bids;

        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefManualBids";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabManualBids";
                ((BiddingAdmin)Page.Master).subTabValue = "CompanyClosedManualBidsP.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "closebidsManuallnk";

             // UserId = Session["UserId"].ToString();
              //  CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 7, 2) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                //var Bids = biddingController.GetManualClosedBids(int.Parse(Session["CompanyId"].ToString()));
                //ViewState["Bids"] = new JavaScriptSerializer().Serialize(Bids);
                //// GVBind();
                //gvBids.DataSource = Bids;
                //gvBids.DataBind();
            }

        }

        //---------Bind GV
        private void GVBind()
        {
            try
            {
                gvBids.DataSource = new JavaScriptSerializer().Deserialize<List<Bidding>>(ViewState["Bids"].ToString()).OrderByDescending(x => x.CreateDate);
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

                    var Bids = biddingController.GetManualClosedBidsByPrCode(int.Parse(Session["CompanyId"].ToString()), prCode);
                    ViewState["Bids"] = new JavaScriptSerializer().Serialize(Bids);

                    gvBids.DataSource = Bids.OrderByDescending(x => x.CreateDate);
                    gvBids.DataBind();
                }
                else if(rdbBid.Checked) {
                    string newString = Regex.Replace(txtBidCode.Text, "[^.0-9]", "");
                    int bidCode = int.Parse(newString);
                    var Bids = biddingController.GetManualClosedBidsByBidCode(int.Parse(Session["CompanyId"].ToString()), bidCode);
                    ViewState["Bids"] = new JavaScriptSerializer().Serialize(Bids);
                    gvBids.DataSource = Bids.OrderByDescending(x => x.CreateDate);
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

                List<BiddingItem> list = new JavaScriptSerializer().Deserialize<List<Bidding>>(ViewState["Bids"].ToString()).Find(b => b.BidId == bidId).BiddingItems;
                list = TabulationCommon.RemoveNull(list);
                gvBidItems.DataSource = list;
                gvBidItems.DataBind();
            }
        }
    }
}