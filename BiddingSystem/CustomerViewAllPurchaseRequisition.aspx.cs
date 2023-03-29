using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class CustomerViewAllPurchaseRequisition : System.Web.UI.Page
    {
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
        PR_FileUploadController pR_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_Replace_FileUploadController pr_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
        TempBOMController tempBOMController = ControllerFactory.CreateTempBOMController();
        TempPR_FileUploadReplacementController tempPR_FileUploadReplacementController = ControllerFactory.CreateTempPR_FileUploadReplacementController();
        PR_SupportiveDocumentController pR_SupportiveDocumentController = ControllerFactory.CreatePR_SupportiveDocumentController();
        static string UserId = string.Empty;
        private string PRId = string.Empty;

        private string UserDept = string.Empty;
        private string OurRef = string.Empty;
        private string PrCode = string.Empty;
        private string RequestedDate = string.Empty;
        private string UserRef = string.Empty;
        private string RequesterName = string.Empty;
        private int CompanyId = 0;
        private int departmentId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
               // ((BiddingAdmin)Page.Master).subTabValue = "CompanyPurchaseRequestNote.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "adminApprovePRLink";

                if (Session["departmentId"] != null || Request.QueryString.Get("PrId") != null)
                {
                    lblDateNow.Text = LocalTime.Today.ToString("dd-MM-yyyy");
                    PRId = Request.QueryString.Get("PrId");
                    departmentId = int.Parse(Session["departmentId"].ToString());
                }
             
                else
                {
                    Response.Redirect("CustomerAllPRView.aspx");
                }
                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

           
            

            if(!IsPostBack){
                try
                {
                    lblDeptName.Enabled = true;
                    PR_Master prmaster = pr_MasterController.FetchApprovePRDataByDeptIdAndPRId(departmentId, int.Parse(PRId));
                    lblPRCode.Text = prmaster.PrCode;
                    lblRequesterName.Text = prmaster.RequestedBy;
                    lblRef.Text = prmaster.OurReference;
                    lblDeptName.Text = companyDepartmentController.GetDepartmentByDepartmentId(departmentId).DepartmentName;
                    lblRequestedDate.Text = prmaster.DateOfRequest.ToString("dd/MM/yyyy hh:mm tt");
                    BindGV();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }

        //----------Bind GV
        private void BindGV() {
            try
            {
                List<PR_Details> pr_Details = pr_DetailController.FetchPR_DetailsByDeptIdAndPrId(int.Parse(PRId),1);
                gvPRView.DataSource = pr_Details;
                gvPRView.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[0].Text);
                List<PR_FileUpload> pr_FileUpload = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvUploadFiles.DataSource = pr_FileUpload;
                gvUploadFiles.DataBind();

               ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected void btnBOM_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[0].Text);
                List<PR_BillOfMeterial> pr_BillOfMeterial = pr_BillOfMeterialController.GetList(int.Parse(PRId), itemId);
                gvBOMDate.DataSource = pr_BillOfMeterial;
                gvBOMDate.DataBind();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal2').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnEditPRMode_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("CustomerPREdit.aspx?PrCode=" + lblPRCode.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnViewzReplacementPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[0].Text);

                gvReplacementPhotos.DataSource = pr_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvReplacementPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalReplacementPhotos').modal('show'); });   </script>", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getPRDetailData()
        {
            try
            {
                string data = "";
                List<PR_Details> pr_Details = pr_DetailController.FetchPR_DetailsByDeptIdAndPrId(int.Parse(PRId),1);
                if (pr_Details.Count > 0)
                {
                    foreach (var item in pr_Details)
                    {
                        int itemId = item.ItemId;
                        string Description = item.ItemDescription;
                        string Purpose = item.Purpose;
                        decimal Quantity = item.ItemQuantity;
                        string Replacement = "No";
                        if (item.Replacement == 1)
                        {
                            Replacement = "Yes";
                        }
                        data += "<tr><td>" + itemId + "</td><td>" + Description + "</td><td>" + "<input type='button' value='View Attachments' class='btn btn-info' />" + "</td><td>" + Purpose + "</td><td>" + Quantity + "</td><td>" + Replacement + "</td></tr>";
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        //-----------2 : Rejected 1: Approved
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            rejectedReason.Visible = false;
            try
            {
                pr_MasterController.UpdateIsApprovePR(departmentId, int.Parse(PRId), 1, int.Parse(UserId), 1, "");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#SuccessAlert').modal('show'); });   </script>", false);
               
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerALLPRView.aspx?UserId=" + UserId);
        }
        protected void btnRejectPR_Click(object sender, EventArgs e)
        {
            rejectedReason.Visible = true;
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                pr_MasterController.UpdateIsApprovePR(departmentId, int.Parse(PRId), 2, int.Parse(UserId), 0,txtRejectReason.Text);
                Response.Redirect("CustomerALLPRView.aspx?UserId=" + UserId, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalApprove').modal('show'); });   </script>", false);
        }
        protected void confirmationToReject_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalReject').modal('show'); });   </script>", false);
        }

        protected void btnViewUploadPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[0].Text);
                List<PR_FileUpload> GetTempPrFiles = pR_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvUploadedPhotos.DataSource = GetTempPrFiles;
                gvUploadedPhotos.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalUploadedPhotos').modal('show'); });   </script>", false);
             

            }
            catch (Exception)
            {

            }
        }

        protected void btnViewSupportiveDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[0].Text);
                List<PR_SupportiveDocument> GetSupportiveDocuments = pR_SupportiveDocumentController.FtechUploadeSupporiveFiles(int.Parse(PRId), itemId);
                gvSupportiveDocuments.DataSource = GetSupportiveDocuments;
                gvSupportiveDocuments.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalSupportiveDocuments').modal('show'); });   </script>", false);
                
            }
            catch (Exception)
            {

            }
            
        }

        protected void lbtnViewUploadSupporiveDocument_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvSupportiveDocuments.Rows[x].Cells[1].Text;
                System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(filepath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}