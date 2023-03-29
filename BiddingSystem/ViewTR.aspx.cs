using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class ViewTR : System.Web.UI.Page {
        MRNControllerInterface mRNControllerInterface = ControllerFactory.CreateMRNController();
        MRNDIssueNoteControllerInterface mrndinController = ControllerFactory.CreateMRNDIssueNoteController();
        TRMasterController tRMasterController = ControllerFactory.CreateTRMasterController();
        TRDetailsController tRDetailsController = ControllerFactory.CreateTRDetailsController();
        TrDetailStatusLogController trDetailStatusLogController = ControllerFactory.CreateTrDetailStatusLogController();
        TRDIssueNoteController tRDIssueNoteController = ControllerFactory.CreateTRDIssueNoteController();

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] == null) {
                Response.Redirect("LoginPage.aspx");
           
            }
            if (!IsPostBack) {
              
                TR_Master TRMaster = tRMasterController.GetTRMasterToView(int.Parse(Request.QueryString.Get("TrId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["TRCode"] = "TR"+ TRMaster.TrCode;
                ViewState["CreatedBy"] = TRMaster.CreatedBy;

                lblWarehouseName.Text = TRMaster.ToLocation;
                lblWarehouseAddress.Text = TRMaster.ToWarehouseAddress;
                lblWarehouseContact.Text = TRMaster.ToWarehousePNo;

                lblExpectedDate.Text = (TRMaster.ExpectedDate).ToString("dd/MM/yyyy");
                lbltrCode.Text = "TR" + (TRMaster.TrCode).ToString();

                lblFromWarehouseName.Text = TRMaster.FromLocation;
                lblFromWarehouseContact.Text =  TRMaster.FromWarehousePNo;
                lbiFromWarehouseAddress.Text = TRMaster.FromWarehouseAddress;

                lblCreatedByName.Text = TRMaster.CreatedByName;
                lblCreatedDate.Text = TRMaster.CreatedDatetime.ToString("dd/MM/yyyy");


                if (TRMaster.IsApproved == 0) {
                    btnTerminateTR.Visible = TRMaster.IsTerminated == 0 ? true : false;
                    lblPending.Visible = true;
                    btnModify.Visible = true;
                }
                else if (TRMaster.IsApproved == 1) {

                    btnTerminateTR.Visible = false;
                    lblApproved.Visible = true;
                    btnModify.Visible = false;
                }
                else {

                    btnTerminateTR.Visible = false;
                    lblRejected.Visible = true;
                    btnModify.Visible = false;
                }

                if (TRMaster.IsApproved != 0) {
                    pnlApprovedByDetails.Visible = true;
                   
                    lblApprovedByName.Text = TRMaster.ApprovedByName;
                    lblApprovedDate.Text = TRMaster.ApprovedDate.ToString("dd/MM/yyyy");
                    if (File.Exists(HttpContext.Current.Server.MapPath(TRMaster.ApprovedSignature)))
                        imgApprovedBySignature.ImageUrl = TRMaster.ApprovedSignature;
                    else
                        imgApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                        lblRemark.Text = TRMaster.ApprovalRemarks;
                }

                if (TRMaster.IsTerminated == 1) {
                    pnlTermination.Visible = true;
                    lblTerMinatedByName.Text = TRMaster.TerminatedByName;
                    lblTerminatedDate.Text = TRMaster.TerminatedDate.ToString("dd/MM/yyyy");
                    if (File.Exists(HttpContext.Current.Server.MapPath(TRMaster.TerminatedBySignature)))
                        imgTerminatedBySignature.ImageUrl = TRMaster.TerminatedBySignature;
                    else
                        imgTerminatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                    lblTerminationRemarks.Text = TRMaster.TerminatedReason;

                    btnTerminateTR.Visible = false;
                    btnModify.Visible=false;
                }

                if (TRMaster.IsApproved == 1) {
                    lblApprovalText.InnerHtml = "Approved By";

                }
                else if (TRMaster.IsApproved == 2) {
                    lblApprovalText.InnerHtml = "Rejected By";
                }

                List<TR_Details> trDetails = tRDetailsController.fetchTRDList(int.Parse(Request.QueryString.Get("TrId")), int.Parse(Session["CompanyId"].ToString()));


                gvTRItems.DataSource = trDetails;
                gvTRItems.DataBind();
                
                if (File.Exists(HttpContext.Current.Server.MapPath(TRMaster.CreatedSignature)))
                    imgCreatedBySignature.ImageUrl = TRMaster.CreatedSignature;
                else
                    imgCreatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";

                TRMaster.TRDetails = trDetails;
                ViewState["TR"] = new JavaScriptSerializer().Serialize(TRMaster);

                if (TRMaster.Status == 0) {
                    lblCompletionPending.Visible = true;
                }
                else if (TRMaster.Status == 1) {
                    lblComplete.Visible = true;
                }
                else if (TRMaster.Status == 2) {
                    lblTerminated.Visible = true;
                }

               
            }
        }

        protected void btnModify_Click(object sender, EventArgs e) {

            Response.Redirect("EditTR.aspx?id=" + Request.QueryString.Get("TrId"));
        }

        protected void btnClone_Click(object sender, EventArgs e) {
            try
            {
                TR_Master trr = new JavaScriptSerializer().Deserialize<TR_Master>(ViewState["TR"].ToString());
                Session["TR"] = trr;
                Response.Redirect("CreateTR.aspx");
            }
            catch(Exception ex)
            {

            }

        }

        protected void lbtnMore_Click(object sender, EventArgs e) {
            int TRdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);

            var trd = tRDetailsController.GetTrdTerminationDetails(TRdId);

            trdTerminatedByName.Text = trd.TerminatedByName;
            trdTerminatedDate.Text = trd.TerminatedDate.ToString("dd/MM/yyyy");
            if (File.Exists(HttpContext.Current.Server.MapPath(trd.TerminatedBySignature)))
                imgTRdTerminatedBySignature.ImageUrl = trd.TerminatedBySignature;
            else
                imgTRdTerminatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";

            lblTRdTerminationRemarks.Text = trd.TerminatedReason;

            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlTerminationDetails').modal('show'); });   </script>", false);
        }

        protected void lbtnLog_Click(object sender, EventArgs e) {
            int TRdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            gvStatusLog.DataSource = trDetailStatusLogController.TRLogDetails(TRdId);
            gvStatusLog.DataBind();

            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlLog').modal('show'); });   </script>", false);
        }
        protected void lbtnIssueNote_Click(object sender, EventArgs e) {
            int TrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            gvIssueNote.DataSource = tRDIssueNoteController.fetchIssueNoteDetails(TrdId);
            gvIssueNote.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlViewIssueNote').modal('show'); });   </script>", false);
        }
        protected void btnTerminate_Click(object sender, EventArgs e) {
            int result = tRMasterController.TerminateTR(int.Parse(Request.QueryString.Get("TrId")), int.Parse(Session["UserId"].ToString()), hdnRemarks.Value);

            if (result > 0) {


                if (ViewState["CreatedBy"].ToString() != Session["UserId"].ToString()) {

                    string email = ControllerFactory.CreateEmailController().GetTRCreatorEmail(int.Parse(Request.QueryString.Get("TrId")));

                    string subject = "Transfer Request Note Terminated";

                    StringBuilder message = new StringBuilder();

                    message.AppendLine("Dear User,");
                    message.AppendLine("<br>");
                    message.AppendLine("<br>");
                    message.AppendLine("Please be advised that your Transfer Request Note, <b>" + ViewState["TRCode"].ToString() + "</b> has been terminated by <b>" + Session["FirstName"].ToString() + "</b> with the Remark: " + hdnRemarks.Value + ".");
                    message.AppendLine("<br>");
                    message.AppendLine("<br>");
                    message.AppendLine("Thanks and Regards,");
                    message.AppendLine("<br>");
                    message.AppendLine("Team EzBidLanka.");

                    EmailGenerator.SendEmail(new List<string> { email }, subject, message.ToString(), true);
                }

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); window.location='ViewTR.aspx?TrId=" + Request.QueryString.Get("TrId") + "'; });   </script>", false);
             
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on Terminating TR\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
        }
    }
}