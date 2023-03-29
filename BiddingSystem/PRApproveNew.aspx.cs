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

namespace BiddingSystem
{
    public partial class PRApproveNew : System.Web.UI.Page
    {
        PrControllerV2 prController = ControllerFactory.CreatePrControllerV2();
        PRDetailsStatusLogController prDetailsStatusLogController = ControllerFactory.CreatePRDetailsStatusLogController();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        PrCapexController prCapexController = ControllerFactory.CreatePrCapexController();

        protected void Page_Load(object sender, EventArgs e)
        {
            serializer.MaxJsonLength = Int32.MaxValue;
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewApprovePr.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "approvePRLink";
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack)
            {
                PrMasterV2 prMaster = prController.GetPrMasterToView(int.Parse(Request.QueryString.Get("PrId")), int.Parse(Session["CompanyId"].ToString()));
                prMaster.PrDetails = prController.FetchPrDetailsList(int.Parse(Request.QueryString.Get("PrId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(prMaster);
                ViewState["PrCode"] = "PR" + prMaster.PrCode;
                ViewState["CreatedBy"] = prMaster.CreatedBy;
                ViewState["PrId"] = Request.QueryString.Get("PrId");
                lblWarehouseName.Text = prMaster.Warehouse.Location;
                lblWarehouseAddress.Text = prMaster.Warehouse.Address;
                lblWarehouseContact.Text = prMaster.Warehouse.PhoneNo;

                lblCategory.Text  = prMaster.PrCategoryName;
                lblSubCategory.Text = prMaster.PrSubCategoryName;
                lblExpectedDate.Text = (prMaster.ExpectedDate).ToString("dd/MM/yyyy");
                lblPRCode.Text = "PR-" + (prMaster.PrCode).ToString();
                lblPRType.Text = prMaster.PrType == 1 ? "Stock" : "Non-stock";
                lblExpenseType.Text = prMaster.ExpenseType == 1 ? "Capital Expense" : "Operational Expense";
                lblPurchaseType.Text = prMaster.PurchaseType == 1 ? "Local" : "Import";

                lblDepartmentName.Text = "Stores";

                if (prMaster.ExpenseType == 1) {
                    btnCapexDocs.Visible = true;
                }
                else {
                    btnCapexDocs.Visible = false;
                }

                if (prMaster.SubDepartment.SubDepartmentID != 0)
                {
                    lblDepartmentName.Text = prMaster.SubDepartment.SubDepartmentName;
                    lblDepartmentContact.Text = prMaster.SubDepartment.PhoneNo;
                }
                //if (prMaster.MrnMaster.MrnCode != 0)
                if (prMaster.MrnMaster.MrnCode != null) {
                    pnlMrnApprovedByDetails.Visible = true;
                    lblMrnCreatedByName.Text = prMaster.MrnMaster.CreatedByName;
                    lblMrnCreatedDate.Text = prMaster.MrnMaster.CreatedDate.ToString("dd/MM/yyyy");
                    divPRReferenceCode.Visible = true;
                    lblMrnReferenceCode.Text = prMaster.MrnMaster.MrnCode.ToString() != "" ? "MRN-" + prMaster.MrnMaster.MrnCode.ToString() : "No";
                    if (File.Exists(HttpContext.Current.Server.MapPath(prMaster.MrnMaster.ApprovedSignature)))
                        imgCreatedBySignatureMrn.ImageUrl = prMaster.MrnMaster.ApprovedSignature;
                    else
                        imgCreatedBySignatureMrn.ImageUrl = "UserSignature/NoSign.jpg";
                }

                lblPrCreatedByName.Text = prMaster.CreatedByName;
                lblPrCreatedDate.Text = prMaster.CreatedDate.ToString("dd/MM/yyyy");
                if (File.Exists(HttpContext.Current.Server.MapPath(prMaster.ApprovedSignature)))
                    imgCreatedBySignaturePr.ImageUrl = prMaster.ApprovedSignature;
                else
                    imgCreatedBySignaturePr.ImageUrl = "UserSignature/NoSign.jpg";


                if (prMaster.IsPrApproved == 0)
                {
                    lblPending.Visible = true;
                }
                else if (prMaster.IsPrApproved == 1)
                {
                    lblApproved.Visible = true;
                }
                else
                {
                    lblRejected.Visible = true;
                }

               
                    lblInfo.Text = prMaster.StatusName;
                
                gvPRItems.DataSource = prMaster.PrDetails;
                gvPRItems.DataBind();    
            }
        }
        

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                int prId = int.Parse(ViewState["PrId"].ToString());
                string remarks = hdnRemarks.Value.ProcessString();
                PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                int isExpenseApprove = 0;
                isExpenseApprove = prMaster.ExpenseType == 1 ? 0 : 1;  // if opex then expsen type will be approved
                int expenseType = prMaster.ExpenseType;
                if (prMaster.IsExpenseApproved == 1)
                {// this happens when PR derived from MRN
                    isExpenseApprove = 1;
                }                

                if (prController.ApproveOrRejectPr(expenseType, prId, 1, isExpenseApprove, int.Parse(Session["UserId"].ToString()), remarks) > 0)
                {
                    List<string> emailAddress = ControllerFactory.CreateEmailController().WHHeadandMRNCreatorEmails(prId);
                    //string subject = "Purchase Request Note Approved";
                    //StringBuilder message = new StringBuilder();
                    //message.AppendLine("Dear User,");
                    //message.AppendLine("<br>");
                    //message.AppendLine("<br>");
                    //message.AppendLine("Please be advised that your Material Request Note, <b>" + ViewState["PrCode"].ToString() + "</b> has been Approved by <b>" + Session["UserNameA"].ToString() + "</b> with the Remark: " + remarks + ".");
                    //message.AppendLine("<br>");
                    //message.AppendLine("<br>");
                    //message.AppendLine("Thanks and Regards,");
                    //message.AppendLine("<br>");
                    //message.AppendLine("Team EzBidLanka.");
                    //EmailGenerator.SendEmailV2(emailAddress, subject, message.ToString(), true);
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewApprovePR.aspx'}); });   </script>", false);
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
            try
            {
                PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                int expenseType = prMaster.ExpenseType;
                int prId = int.Parse(ViewState["PrId"].ToString());
                string remarks = hdnRemarks.Value.ProcessString();
                if (prController.ApproveOrRejectPr(expenseType, prId, 2, 0, int.Parse(Session["UserId"].ToString()), remarks) > 0)
                {
                    List<string> emailAddress = ControllerFactory.CreateEmailController().WHHeadandMRNCreatorEmails(prId);
                    //string subject = "Material Request Note Rejected";
                    //StringBuilder message = new StringBuilder();
                    //message.AppendLine("Dear User,");
                    //message.AppendLine("<br>");
                    //message.AppendLine("<br>");
                    //message.AppendLine("Please be advised that your Material Request Note, <b>" + ViewState["PrCode"].ToString() + "</b> has been rejected by <b>" + Session["UserNameA"].ToString() + "</b> with the Remark: " + remarks + ".");
                    //message.AppendLine("<br>");
                    //message.AppendLine("<br>");
                    //message.AppendLine("Thanks and Regards,");
                    //message.AppendLine("<br>");
                    //message.AppendLine("Team EzBidLanka.");
                    //EmailGenerator.SendEmailV2(emailAddress, subject, message.ToString(), true);
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewApprovePR.aspx'}); });   </script>", false);
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
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            gvStatusLog.DataSource = ControllerFactory.CreatePRDetailsStatusLogController().PrLogDetails(PrdId);
            gvStatusLog.DataBind();

            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlLog').modal('show'); });   </script>", false);
        }

        protected void lkRemark_Click(object sender, EventArgs e)
        {
            string remark = "Not Found";
            string type = "";
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            PrDetailsV2 prDetails = prMaster.PrDetails.Find(t => t.PrdId == PrdId);
            if (prDetails.Remarks != "" && prDetails.Remarks != null)
            {
                remark = prDetails.Remarks;
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
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            PrDetailsV2 prDetails = prMaster.PrDetails.Find(t => t.PrdId == PrdId);
            gvViewReplacementImages.DataSource = prDetails.PrReplacementFileUploads;
            gvViewReplacementImages.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlReplacementImages').modal('show'); });   </script>", false);
        }

        protected void lkStandardImages_Click(object sender, EventArgs e)
        {
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            PrDetailsV2 prDetails = prMaster.PrDetails.Find(t => t.PrdId == PrdId);
            gvStandardImages.DataSource = prDetails.PrFileUploads;
            gvStandardImages.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlFileUpload').modal('show'); });   </script>", false);
        }

        protected void lkSupportiveDocument_Click(object sender, EventArgs e)
        {
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            PrDetailsV2 prDetails = prMaster.PrDetails.Find(t => t.PrdId == PrdId);
            gvSupportiveDocuments.DataSource = prDetails.PrSupportiveDocuments;
            gvSupportiveDocuments.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlSupportiveDocs').modal('show'); });   </script>", false);
        }

        protected void lkItemSpecification_Click(object sender, EventArgs e)
        {
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            PrDetailsV2 prDetails = prMaster.PrDetails.Find(t => t.PrdId == PrdId);
            gvBOMDate.DataSource = prDetails.PrBoms;
            gvBOMDate.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItemSpec').modal('show'); });   </script>", false);
        }
        protected void btnCapexDocs_Click(object sender, EventArgs e) {
            int prId = int.Parse(Request.QueryString.Get("PrId").ToString());
            PrMasterV2 prMaster = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());

            gvCapexDocs.DataSource = prCapexController.GetPrCapexDocs(prId);
            gvCapexDocs.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlCapexDocs').modal('show'); });   </script>", false);
        }
        protected void lkWarehouseStock_Click(object sender, EventArgs e)
        {
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            PrDetailsV2 prDetails = prMaster.PrDetails.Find(t => t.PrdId == PrdId);
            gvWarehouseStock.DataSource = new List<PrDetailsV2> { prDetails };
            gvWarehouseStock.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlWarehouseStock').modal('show'); });   </script>", false);
        }
    }
}