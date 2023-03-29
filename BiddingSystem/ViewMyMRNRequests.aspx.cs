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
    public partial class ViewMyMRNRequests : System.Web.UI.Page
    {
        static int UserId = 1005;
        static int CompanyId = 4;

        static List<MrnMaster> MrnMasters;
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        MRNmasterController mrn_MasterController = ControllerFactory.CreateMRNmasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        static List<PR_Master> prMasterTemp = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewMyMRNRequests.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewMyMRNStatusLink";


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
                        MrnMasters = mrn_MasterController.GetMRNListForViewMyMRN(CompanyId, UserId).OrderByDescending(t=>t.CreatedDate).ToList();
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

            gvMRNRequest.DataSource = MrnMasters;
            gvMRNRequest.DataBind();
            
        }

        protected void gvMRNRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    prMasterTemp = new List<PR_Master>();
                    int MrnID = int.Parse(gvMRNRequest.DataKeys[e.Row.RowIndex].Value.ToString());
                    GridView gvMrnDetails = e.Row.FindControl("gvMrnDetails") as GridView;
                    
                    gvMrnDetails.DataSource = MrnMasters.Find(x => x.MrnID == MrnID).MrnDetails;
                    gvMrnDetails.DataBind();

                    GridView gvPurchaseRequest1 = e.Row.FindControl("gvPurchaseRequest") as GridView;

                    var prMaster = MrnMasters.Find(x => x.MrnID == MrnID).prMaster;
                    if (prMaster.PrId != 0)
                    {
                        List<PR_Master> list = new List<PR_Master> { prMaster };
                        prMasterTemp = list;
                        gvPurchaseRequest1.DataSource = list;
                        gvPurchaseRequest1.DataBind();
                    }
                }catch(Exception ex)
                {

                }
            }

        }

        protected void gvMrnDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }

        protected void gvPurchaseRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try { 
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                    /*     int MrnID = int.Parse(gvMRNRequest.DataKeys[e.Row.RowIndex].Value.ToString());
                         int PrID = MrnMasters.Find(x => x.MrnID == MrnID).prMaster.PrId;
                         GridView gvPrDetails = e.Row.FindControl("gvPrDetails") as GridView;
                             if (PrID != 0)
                             {
                                 List<PR_Details> list = MrnMasters.Find(pr => pr.MrnID == MrnID).prMaster.PrDetails.FindAll(x => x.PrId == PrID);

                                 gvPrDetails.DataSource = list;
                                 gvPrDetails.DataBind();
                             }else
                             {
                                 gvPrDetails.DataSource = null;
                                 gvPrDetails.DataBind();
                             }
                      */
                    GridView gvPrDetails = e.Row.FindControl("gvPrDetails") as GridView;
                    if (prMasterTemp.Count != 0)
                    {
                        gvPrDetails.DataSource = prMasterTemp[0].PrDetails;
                        gvPrDetails.DataBind();
                    }
                    else
                    {
                        gvPrDetails.DataSource = null;
                        gvPrDetails.DataBind();
                    }
                }
                


            }
            catch (Exception ex)
            {

            }
        }

        protected void gvPrDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try { 
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int MRNId = int.Parse(gvMRNRequest.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvStatusLog = e.Row.FindControl("gvStatusLog") as GridView;
                GridView gvPrDetail = (gvStatusLog.NamingContainer as GridViewRow).NamingContainer as GridView;
                int PrID = MrnMasters.Find(x => x.MrnID == MRNId).prMaster.PrId;
                int prdId = int.Parse(gvPrDetail.DataKeys[e.Row.RowIndex].Value.ToString());

                gvStatusLog.DataSource = MrnMasters.Find(pr => pr.MrnID == MRNId).prMaster.PrDetails.Find(p => p.PrdId == prdId).PrDetailsStatusLogs;
                gvStatusLog.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvMRNRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                gvMRNRequest.PageIndex = e.NewPageIndex;
                BindGV();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}