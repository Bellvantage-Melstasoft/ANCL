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
    public partial class ApproveTR : System.Web.UI.Page
    {
        MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        EmailController emailController = ControllerFactory.CreateEmailController();
        TRMasterController tRMasterController = ControllerFactory.CreateTRMasterController();
        TRDetailsController TRDetailsController = ControllerFactory.CreateTRDetailsController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefTR";
                ((BiddingAdmin)Page.Master).subTabTitle = "hrefTR";
                ((BiddingAdmin)Page.Master).subTabValue = "ApproveTR.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ApproveTRLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 13, 4) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Count() > 0)
                {
                    try
                    {
                        gvTR.DataSource = tRMasterController.fetchTRListforApproval((Session["UserWarehouses"] as List<UserWarehouse>).Where(w => w.UserType == 1).Select(w => w.WrehouseId).ToList());
                        gvTR.DataBind();


                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You must be a Warehouse Head to view this page', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                }
            }
        }
        protected void gvTR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int TrId = int.Parse(gvTR.DataKeys[e.Row.RowIndex].Value.ToString());
                    GridView gvTRDetails = e.Row.FindControl("gvTRD") as GridView;

                    gvTRDetails.DataSource = TRDetailsController.fetchTRDList(TrId, int.Parse(Session["CompanyId"].ToString())).Where(l => l.IsTerminated == 0);
                    gvTRDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void lbtnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                int trID = int.Parse(hdnTRID.Value);


                if (tRMasterController.ApproveOrRejectTR(trID, 1, int.Parse(Session["UserId"].ToString()), hdnRemarks.Value.ProcessString()) > 0)
                {

                    //   List<string> emails = emailController.WHHeadandMRNCreatorEmails(trID);

                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);

                    gvTR.DataSource = tRMasterController.fetchTRListforApproval((Session["UserWarehouses"] as List<UserWarehouse>).Where(w => w.UserType == 1).Select(w => w.WrehouseId).ToList());
                    gvTR.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on approving TR\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnModify_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(((Button)sender).NamingContainer);
            int trId = int.Parse(row.Cells[1].Text);

            Response.Redirect("EditTR.aspx?id=" + trId);
        }


        protected void lbtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                int trID = int.Parse(hdnTRID.Value);

                if (tRMasterController.ApproveOrRejectTR(trID, 2, int.Parse(Session["UserId"].ToString()), hdnRemarks.Value.ProcessString()) > 0)
                {

                    //  List<string> emails = emailController.WHHeadandMRNCreatorEmails(trID);
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    gvTR.DataSource = tRMasterController.fetchTRListforApproval((Session["UserWarehouses"] as List<UserWarehouse>).Where(w => w.UserType == 1).Select(w => w.WrehouseId).ToList());
                    gvTR.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on rejecting TR\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}