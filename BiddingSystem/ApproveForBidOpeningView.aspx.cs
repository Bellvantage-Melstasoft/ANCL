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
    public partial class ApproveForBidOpeningView : System.Web.UI.Page
    {
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        PR_FileUploadController pR_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_Replace_FileUploadController pR_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
        PR_SupportiveDocumentController pR_SupportiveDocumentController = ControllerFactory.CreatePR_SupportiveDocumentController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();

        private int UserId = 0;
        private int CompanyId = 0;
        private string PRId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
               // ((BiddingAdmin)Page.Master).subTabValue = "ApproveForBidOpening.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "approveforBidOpeninggLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = int.Parse(Session["UserId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(UserId, CompanyId, 6, 2) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
          
            lblDateNow.Text = LocalTime.Today.ToString("dd-MM-yyyy");
            PRId = Request.QueryString.Get("PrId");

            if (!IsPostBack)
            {
                try
                {
                   PR_Master _PR_Master =  pr_MasterController.FetchApprovePRDataByPRId(int.Parse(PRId));
                   lblRef.Text = _PR_Master.OurReference;
                   lblPRCode.Text = _PR_Master.PrCode;
                   lblRequestedDate.Text = _PR_Master.DateOfRequest.ToString("dd/MM/yyyy hh:mm tt");
                   lblDeptName.Text = companyDepartmentController.GetDepartmentByDepartmentId((CompanyId)).DepartmentName;
                   lblRequesterName.Text = _PR_Master.RequestedBy;
                   BindGV();
                   
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            else { 
            
            }
        }

        //----------Bind GV
        private void BindGV()
        {
            try
            {
                //List<PR_Details> pr_Details = pr_DetailController.FetchNotSubmitedItemsToSupplierPortalView(int.Parse(PRId)).Where(x=>x.SubmitForBid == 1 && x.BidTypeMaualOrBid == 1).ToList();
                List<PR_Details> pr_Details = pr_DetailController.FetchNotSubmitedItemsToSupplierPortalView(int.Parse(PRId), int.Parse(Session["CompanyId"].ToString())).Where(x => x.SubmitForBid == 1 ).ToList();
                if (pr_Details.Count != 0)
                {
                    gvPRView.DataSource = pr_Details;
                    gvPRView.DataBind();
                }
                else {
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    btnRejectPR.Visible = false;
                    divBidForApproval.Visible = false;
                }
                BindGVRejectedOrApprove();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------Bind GV
        private void BindGVRejectedOrApprove()
        {
            try
            {
                //List<PR_Details> pr_Details = pr_DetailController.FetchtSubmitedItemsToSupplierPortalView(int.Parse(PRId)).Where(x=>x.IsApproveToViewInSupplierPortal ==1 || x.IsApproveToViewInSupplierPortal==2 && (x.BidTypeMaualOrBid ==1)).ToList();
                List<PR_Details> pr_Details = pr_DetailController.FetchtSubmitedItemsToSupplierPortalView(int.Parse(PRId), int.Parse(Session["CompanyId"].ToString())).Where(x => x.IsApproveToViewInSupplierPortal == 1 || x.IsApproveToViewInSupplierPortal == 2 || x.IsApproveToViewInSupplierPortal == 4).ToList();
                if (pr_Details.Count != 0)
                {
                    divVisibity.Visible = true;
                    GridView1.DataSource = pr_Details;
                    GridView1.DataBind();
                }
               else {
                   divVisibity.Visible = false;
                }
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
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);
                List<PR_BillOfMeterial> pr_BillOfMeterial = pr_BillOfMeterialController.GetList(int.Parse(PRId), itemId);
                gvBOMDate.DataSource = pr_BillOfMeterial;
                gvBOMDate.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal2').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void lblViewSettings_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);
                lblItemNAme.Text = gvPRView.Rows[x].Cells[2].Text;
                gvSettingsTableModal.DataSource = biddingController.GetBiddingGeneralSettings(int.Parse(PRId), itemId);
                gvSettingsTableModal.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal3').modal('show'); });   </script>", false);

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
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);
                List<PR_FileUpload> pr_FileUpload = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvUploadFiles.DataSource = pr_FileUpload;
                gvUploadFiles.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected void btnBOM_ClickGv(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(GridView1.Rows[x].Cells[1].Text);
                List<PR_BillOfMeterial> pr_BillOfMeterial = pr_BillOfMeterialController.GetList(int.Parse(PRId), itemId);
                gvBOMDate.DataSource = pr_BillOfMeterial;
                gvBOMDate.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal2').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void lblViewSettings_ClickGv(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(GridView1.Rows[x].Cells[1].Text);
                lblItemNAme.Text = GridView1.Rows[x].Cells[2].Text;
                gvSettingsTableModal.DataSource = biddingController.GetBiddingGeneralSettings(int.Parse(PRId), itemId);
                gvSettingsTableModal.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal3').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnView_ClickGv(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(GridView1.Rows[x].Cells[1].Text);
                List<PR_FileUpload> pr_FileUpload = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvUploadFiles.DataSource = pr_FileUpload;
                gvUploadFiles.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal').modal('show'); });   </script>", false);

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
                List<BidSubmittedData> bidSubmittedData = new List<BidSubmittedData>();

                foreach (GridViewRow gvr in this.gvPRView.Rows)
                {
                    if (((CheckBox)gvr.FindControl("CheckBox1")).Checked == true)
                    {
                        bidSubmittedData.Add(new BidSubmittedData(int.Parse(gvr.Cells[1].Text), gvr.Cells[2].Text, gvr.Cells[3].Text, gvr.Cells[4].Text, decimal.Parse(gvr.Cells[5].Text), int.Parse(gvr.Cells[1].Text), gvr.Cells[6].Text, int.Parse(gvr.Cells[13].Text)));
                    }
                }

                int OnlineBidOpeningItemItemCount = 0;
                int TotalCount = 0;
                foreach (var item in bidSubmittedData)
                {
                    if (item.ManualOrOnlineBid == 1) {
                        int openBid = biddingController.UpdateBidRejectOrApproveStatus(int.Parse(PRId), item.ItemId, 1, "", item.BidOpeningId);
                        if(openBid > 0){
                            TotalCount = OnlineBidOpeningItemItemCount + 1;
                        }
                        
                    }
                    if (item.ManualOrOnlineBid == 0) //4 means Manual Bid Approval
                    {
                        biddingController.UpdateBidRejectOrApproveStatus(int.Parse(PRId), item.ItemId, 4, "", item.BidOpeningId);
                    }
                    //---------------Submit For Bidding Table 2
                    //pr_DetailController.UpdateUpdateForBid(int.Parse(PRId), item.ItemId, 2);GridView1
                }

                if(TotalCount > 0){
                       FCMPushNotification fcmPush = new FCMPushNotification();
                        fcmPush.SendNotification("New Bids Submittes", "Chachika", TotalCount.ToString());
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('successMessage').innerHTML = \"New Bids Submitted\"; $('#SuccessAlert').modal('show'); });   </script>", false);

                }

                BindGV(); 
               // BindGVRejectedOrApprove();
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
                List<BidSubmittedData> bidSubmittedDataReject = new List<BidSubmittedData>();

                foreach (GridViewRow gvr in this.gvPRView.Rows)
                {
                    if (((CheckBox)gvr.FindControl("CheckBox1")).Checked == true)
                    {
                        bidSubmittedDataReject.Add(new BidSubmittedData(int.Parse(gvr.Cells[1].Text), gvr.Cells[2].Text, gvr.Cells[3].Text, gvr.Cells[4].Text, decimal.Parse(gvr.Cells[5].Text), int.Parse(gvr.Cells[1].Text), gvr.Cells[6].Text, int.Parse(gvr.Cells[13].Text)));
                    }
                }

                foreach (var item in bidSubmittedDataReject)
                {
                    int rejcount = 0;
                    biddingController.UpdateBidRejectOrApproveStatus(int.Parse(PRId), item.ItemId, 2, txtRejectReason.Text,item.BidOpeningId);
                    int rejectedCount = pr_DetailController.FetchPR_DetailsByPrIdAndItemId(int.Parse(PRId),item.ItemId).PrIsRejectedCount;
                    if (rejectedCount == 0) {
                        rejcount = 1;
                    }
                    if (rejectedCount >= 1)
                    {
                        rejcount = rejectedCount + 1;
                    }
                    pr_DetailController.UpdateUpdateForBid(int.Parse(PRId), item.ItemId, 2, rejcount);

                }
                Response.Redirect("ApproveForBidOpeningView.aspx?PrId="+PRId+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#modalApprove').modal('show'); });   </script>", false);
        }
        protected void confirmationToReject_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#modalReject').modal('show'); });   </script>", false);
        }

        protected void btnRejectPR_Click(object sender, EventArgs e)
        {
            rejectedReason.Visible = true;
        }

        public class BidSubmittedData
        {
            public BidSubmittedData(int ItemId, string ItemName, string ItemDescription, string Purpose, decimal ItemQuantity, int Replacement, string BidOpeningId, int ManualOrOnlineBid)
            {
                itemId = ItemId;
                itemName = ItemName;
                itemDescription = ItemDescription;
                purpose = Purpose;
                itemQuantity = ItemQuantity;
                replacement = Replacement;
                bidOpeningId = BidOpeningId;
                manualOrOnlineBid = ManualOrOnlineBid;
            }

            private int itemId;
            private string itemName;
            private string itemDescription;
            private string purpose;
            private decimal itemQuantity;
            private int replacement;
            private string bidOpeningId;
            private int manualOrOnlineBid;

            public int ItemId
            {
                get { return itemId; }
                set { itemId = value; }
            }

            public string ItemName
            {
                get { return itemName; }
                set { itemName = value; }
            }

            public string ItemDescription
            {
                get { return itemDescription; }
                set { itemDescription = value; }
            }

            public string Purpose
            {
                get { return purpose; }
                set { purpose = value; }
            }

            public decimal ItemQuantity
            {
                get { return itemQuantity; }
                set { itemQuantity = value; }
            }

            public int Replacement
            {
                get { return replacement; }
                set { replacement = value; }
            }

            public string BidOpeningId
            {
                get { return bidOpeningId; }
                set { bidOpeningId = value; }
            }

            public int ManualOrOnlineBid
            {
                get { return manualOrOnlineBid; }
                set { manualOrOnlineBid = value; }
            }
        }

        protected void btnViewUploadPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);
                List<PR_FileUpload> GetTempPrFiles = pR_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvUploadedPhotos.DataSource = GetTempPrFiles;
                gvUploadedPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalUploadedPhotos').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {
            
            }
        }

        protected void btnViewUploadPhotosGv_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(GridView1.Rows[x].Cells[1].Text);
                List<PR_FileUpload> GetTempPrFiles = pR_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvUploadedPhotos.DataSource = GetTempPrFiles;
                gvUploadedPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalUploadedPhotos').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnViewReplacementPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);
                List<PR_Replace_FileUpload> GetTempPrFiles = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvReplacementPhotos.DataSource = GetTempPrFiles;
                gvReplacementPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalReplacementPhotos').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
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

        protected void btnViewSupportiveDocuments_Click1(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);
                List<PR_SupportiveDocument> GetSupportiveDocuments = pR_SupportiveDocumentController.FtechUploadeSupporiveFiles(int.Parse(PRId), itemId);
                gvSupportiveDocuments.DataSource = GetSupportiveDocuments;
                gvSupportiveDocuments.DataBind();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalSupportiveDocuments').modal('show'); });   </script>", false);

            }
            catch (Exception)
            {

            }
           
        }
    }
}