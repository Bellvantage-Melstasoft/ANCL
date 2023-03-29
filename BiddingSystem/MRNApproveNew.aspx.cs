using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class MRNApproveNew : System.Web.UI.Page
    {
        MrnControllerV2 mrnController = ControllerFactory.CreateMrnControllerV2();
        MRNDIssueNoteControllerInterface mrndIssueNoteController = ControllerFactory.CreateMRNDIssueNoteController();
        MrnDetailsStatusLogController mrnDetailsStatusLogController = ControllerFactory.CreateMrnDetailStatusLogController();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        MRNCapexDocController mRNCapexDocController = ControllerFactory.CreateMRNCapexDocController();

        protected void Page_Load(object sender, EventArgs e) {
            serializer.MaxJsonLength = Int32.MaxValue;
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewApproveMrn.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "approveMRNLink";
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack)
            {
                MrnMasterV2 mrnMaster = mrnController.GetMRNMasterToView(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()));
                mrnMaster.MrnDetails = mrnController.FetchMrnDetailsList(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["MrnMaster"] = serializer.Serialize(mrnMaster);
                ViewState["MrnCode"] = "MRN" + mrnMaster.MrnCode;
                ViewState["CreatedBy"] = mrnMaster.CreatedBy;
                ViewState["MrnId"] = Request.QueryString.Get("MrnId");
                lblWarehouseName.Text = mrnMaster.Warehouse.Location;
                lblWarehouseAddress.Text = mrnMaster.Warehouse.Address;
                lblWarehouseContact.Text = mrnMaster.Warehouse.PhoneNo;

                lblCategory.Text  = mrnMaster.MrnCategoryName;
                lblSubCategory.Text = mrnMaster.MrnSubCategoryName;
                lblExpectedDate.Text = (mrnMaster.ExpectedDate).ToString("dd/MM/yyyy");
                lblMrnCode.Text = "MRN-" + (mrnMaster.MrnCode).ToString();
                lblMrnType.Text = mrnMaster.MrnType == 1 ? "Stock" : "Non-stock";
                lblExpenseType.Text = mrnMaster.ExpenseType == 1 ? "Capital Expense" : "Operational Expense";

                lblDepartmentName.Text = mrnMaster.SubDepartment.SubDepartmentName;
                lblDepartmentContact.Text = mrnMaster.SubDepartment.PhoneNo;

                lblCreatedByName.Text = mrnMaster.CreatedByName;
                lblCreatedDate.Text = mrnMaster.CreatedDate.ToString("dd/MM/yyyy");


                if (mrnMaster.ExpenseType == 1) {
                    btnCapexDocs.Visible = true;
                }
                else {
                    btnCapexDocs.Visible = false;
                }

                    if (mrnMaster.IsMrnApproved == 0)
                {
                    lblPending.Visible = true;
                }
                else if (mrnMaster.IsMrnApproved == 1)
                {
                    lblApproved.Visible = true;
                }
                else
                {
                    lblRejected.Visible = true;
                }

                
                    lblInfo.Text = mrnMaster.StatusName;
                lblPurchaseType.Text = mrnMaster.PurchaseType == 1 ? "Local" : "Import";
                gvMRNItems.DataSource = mrnMaster.MrnDetails;
                gvMRNItems.DataBind();    
            }
        }

        protected void lbtnIssueNote_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            gvIssueNote.DataSource = mrndIssueNoteController.FetchIssueNoteDetailsByMrnDetailsId(MrndId);
            gvIssueNote.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlViewIssueNote').modal('show'); });   </script>", false);
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                int mrnId = int.Parse(ViewState["MrnId"].ToString());
                string remarks = hdnRemarks.Value.ProcessString();
                MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
                int isExpenseApprove = mrnMaster.ExpenseType == 1 ? 0 : 1;  // if opex then expsen type will be approved
                int expenseType = mrnMaster.ExpenseType;
                if (mrnController.ApproveOrRejectMrn(expenseType, mrnId, 1, isExpenseApprove, int.Parse(Session["UserId"].ToString()), remarks) > 0)
                {
                    List<string> emailAddress = ControllerFactory.CreateEmailController().WHHeadandMRNCreatorEmails(mrnId);
                    string subject = "Material Request Note Approved";
                    StringBuilder message = new StringBuilder();
                    message.AppendLine("Dear User,");
                    message.AppendLine("<br>");
                    message.AppendLine("<br>");
                    message.AppendLine("Please be advised that your Material Request Note, <b>" + ViewState["MrnCode"].ToString() + "</b> has been Approved by <b>" + Session["UserNameA"].ToString() + "</b> with the Remark: " + remarks + ".");
                    message.AppendLine("<br>");
                    message.AppendLine("<br>");
                    message.AppendLine("Thanks and Regards,");
                    message.AppendLine("<br>");
                    message.AppendLine("Team EzBidLanka.");
                    EmailGenerator.SendEmailV2(emailAddress, subject, message.ToString(), true);
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewApproveMrn.aspx'}); });   </script>", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on approving MRN\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try {
                MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
                 int expenseType = mrnMaster.ExpenseType;
                int mrnId = int.Parse(ViewState["MrnId"].ToString());
                string remarks = hdnRemarks.Value.ProcessString();
                if (mrnController.ApproveOrRejectMrn(expenseType, mrnId, 2, 0, int.Parse(Session["UserId"].ToString()), remarks) > 0)
                {
                    List<string> emailAddress = ControllerFactory.CreateEmailController().WHHeadandMRNCreatorEmails(mrnId);
                    string subject = "Material Request Note Rejected";
                    StringBuilder message = new StringBuilder();
                    message.AppendLine("Dear User,");
                    message.AppendLine("<br>");
                    message.AppendLine("<br>");
                    message.AppendLine("Please be advised that your Material Request Note, <b>" + ViewState["MrnCode"].ToString() + "</b> has been rejected by <b>" + Session["UserNameA"].ToString() + "</b> with the Remark: " + remarks + ".");
                    message.AppendLine("<br>");
                    message.AppendLine("<br>");
                    message.AppendLine("Thanks and Regards,");
                    message.AppendLine("<br>");
                    message.AppendLine("Team EzBidLanka.");
                    EmailGenerator.SendEmailV2(emailAddress, subject, message.ToString(), true);
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewApproveMrn.aspx'}); });   </script>", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on approving MRN\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnLog_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            gvStatusLog.DataSource = ControllerFactory.CreateMrnDetailStatusLogController().MrnLogDetails(MrndId);
            gvStatusLog.DataBind();

            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlLog').modal('show'); });   </script>", false);
        }

        protected void lkRemark_Click(object sender, EventArgs e)
        {
            string remark = "Not Found";
            string type = "";
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            if (mrnDetails.Remarks != "" && mrnDetails.Remarks != null)
            {
                remark = mrnDetails.Remarks;
                type = "type: 'info',";
            }

            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none",
                "<script>    $(document).ready(function () {  "+
                " swal({  "+
                "    title: 'Remarks',  "+
                type +
                "    text: '" + remark + "',  "+
                "    confirmButtonClass: 'btn btn-info btn-styled',  "+
                "    confirmButtonText: 'Close',  "+
                "    buttonsStyling: false "+
                " }); " +
                
                " });   </script>"

                , false);
        }

        protected void lkRepalacementImages_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            gvViewReplacementImages.DataSource = mrnDetails.MrnReplacementFileUploads;
            gvViewReplacementImages.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlReplacementImages').modal('show'); });   </script>", false);
        }

        protected void lkStandardImages_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            gvStandardImages.DataSource = mrnDetails.MrnFileUploads;
            gvStandardImages.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlFileUpload').modal('show'); });   </script>", false);
        }

        protected void lkSupportiveDocument_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            gvSupportiveDocuments.DataSource = mrnDetails.MrnSupportiveDocuments;
            gvSupportiveDocuments.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlSupportiveDocs').modal('show'); });   </script>", false);
        }
        protected void btnCapexDocs_Click(object sender, EventArgs e) {
            int MrnId = int.Parse(Request.QueryString.Get("MrnId").ToString());
            gvCapexDocs.DataSource = ControllerFactory.CreateMRNCapexDocController().GetMrnCapexDocs(MrnId);
            gvCapexDocs.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlCapexDocs').modal('show'); });   </script>", false);
        }

        protected void lkItemSpecification_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            gvBOMDate.DataSource = mrnDetails.MrnBoms;
            gvBOMDate.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItemSpec').modal('show'); });   </script>", false);
        }

    }
}