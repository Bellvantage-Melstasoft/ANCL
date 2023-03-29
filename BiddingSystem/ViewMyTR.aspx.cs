using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class ViewMyTR : System.Web.UI.Page
    {
        //MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        //MrnDetailStatusLogController mrnDetailStatusLogController = ControllerFactory.CreateMrnDetailStatusLogController();
        TRMasterController tRMasterController = ControllerFactory.CreateTRMasterController();
        TrDetailStatusLogController trDetailStatusLogController = ControllerFactory.CreateTrDetailStatusLogController();
        TRDetailsController tRDetailsController = ControllerFactory.CreateTRDetailsController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefTR";
                ((BiddingAdmin)Page.Master).subTabTitle = "hrefTR";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewMyTR.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewMyTRLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 13, 2) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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

                txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");
                try
                {
                    gvTR.DataSource = tRMasterController.FetchTRsByCreatedUser(int.Parse(ViewState["UserId"].ToString()), LocalTime.Now);
                    gvTR.DataBind();

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
            gvTR.DataSource = tRMasterController.FetchTRsByCreatedUser(int.Parse(ViewState["UserId"].ToString()), date);
            gvTR.DataBind();
        }

        protected void lbtnView_Click(object sender, EventArgs e)
        {
            int TRId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);

            Response.Redirect("ViewTR.aspx?TrId=" + TRId);
        }

        protected void gvTRDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int TRdId = int.Parse(e.Row.Cells[2].Text);
                GridView gvStatusLog = e.Row.FindControl("gvStatusLog") as GridView;

                gvStatusLog.DataSource = trDetailStatusLogController.TRLogDetails(TRdId);
                gvStatusLog.DataBind();
            }

        }
        protected void btnLog_Click(object sender, EventArgs e)
        {
            int TRId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            List<TR_Details> trDetails = tRDetailsController.fetchTRDList(TRId, int.Parse(Session["CompanyId"].ToString()));


            gvTRDetails.DataSource = trDetails;
            gvTRDetails.DataBind();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlViewTR').modal('show'); });   </script>", false);

        }

    }
}