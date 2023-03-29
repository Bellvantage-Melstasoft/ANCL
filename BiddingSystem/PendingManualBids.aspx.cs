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
    public partial class PendingManualBids : System.Web.UI.Page
    {
       // static string UserId = string.Empty;
        //static int CompanyId = 0;
       // static List<Bidding> Bids;

        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        MRNmasterController MrnMasterController = ControllerFactory.CreateMRNmasterController();
        PR_DetailController pR_DetailController = ControllerFactory.CreatePR_DetailController();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefManualBids";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabManualBids";
                    ((BiddingAdmin)Page.Master).subTabValue = "PendingManualBids.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "pendingManualBidsidlnk";

                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                   // UserId = Session["UserId"].ToString();
                 //   CompanyId = int.Parse(Session["CompanyId"].ToString());

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 7, 1) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                    GVBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //---------Bind GV
        private void GVBind()
        {
            try
            {
                //var Bids = biddingController.GetManualInProgressBids(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));
                var Bids = biddingController.GetManualInProgressBidsWithItem(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));
                ViewState["Bids"] = new  JavaScriptSerializer().Serialize(Bids);
                foreach (var item in Bids)
                {
                    PrMasterV2 ob = pr_MasterController.getPRMasterDetailByPrId(item.PrId);
                    item.MRNRefNumber = ob.MrnId.ToString();
                    if(item.MRNRefNumber != null && item.MRNRefNumber!= "" && item.MRNRefNumber != "0")
                    {
                        item.subDepartmentName = MrnMasterController.GetMRNMasterByMrnId(Convert.ToInt32(item.MRNRefNumber)).SubDepartmentName;
                    }
                }
                gvBids.DataSource = Bids.OrderBy(x => x.EndDate);
                gvBids.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
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

                foreach (GridViewRow row in gvBidItems.Rows) {
                    ViewState["prdId"] = Convert.ToInt32(row.Cells[2].Text);

                }
            }
        }

        protected void btnTerminate_Click(object sender, EventArgs e) {

            int result = biddingController.TerminateBid(int.Parse(hdnBidId.Value), int.Parse(Session["UserId"].ToString()), hdnRemarks.Value);
            List<BiddingItem> Biddinglist = biddingController.GetPrdIdByBidId(int.Parse(hdnBidId.Value));
            Bidding bid = biddingController.GetBidDetailsByBidId(int.Parse(hdnBidId.Value));


            if (result > 0) {
                int update = 0;
                

                for (int i = 0; i < Biddinglist.Count; i++) {
                    int prdId = Biddinglist[i].PrdId;
                    update = pR_DetailController.UpdateterminatedStatus(int.Parse(Session["UserId"].ToString()), prdId);
                }
                if (update > 0) {
                    int prId = bid.PrId;
                    pr_MasterController.UpdateTerminatedPRMaster(prId);
                }
                GVBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }
            else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on expiring Bid', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }
        }

        protected void btnExpire_Click(object sender, EventArgs e)
        {
            //int bidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
            int bidId = int.Parse(hdnBidId.Value);
            int result = biddingController.ExpireBid(bidId);
            List<BiddingItem> Biddinglist = biddingController.GetPrdIdByBidId(bidId);
            if (result > 0)
            {
                
                for (int i = 0; i < Biddinglist.Count; i++) {
                    int prdId = Biddinglist[i].PrdId;
                    //biddingController.ExpireBid(Biddinglist[i].BidId);
                    pR_DetailController.UpdateStatus(int.Parse(Session["UserId"].ToString()), prdId);

                }
                GVBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on expiring Bid', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }
        }
    }
}