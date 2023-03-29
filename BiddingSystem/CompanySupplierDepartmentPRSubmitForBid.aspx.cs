using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Web.Services;

namespace BiddingSystem
{
    public partial class CompanySupplierDepartmentPRSubmitForBid : System.Web.UI.Page
    {
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
        PR_FileUploadController pR_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_Replace_FileUploadController pr_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
        PR_Replace_FileUploadController pR_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
        PR_SupportiveDocumentController pR_SupportiveDocumentController = ControllerFactory.CreatePR_SupportiveDocumentController();
        PR_DetailHistoryController pR_DetailHistoryController = ControllerFactory.CreatePR_DetailHistoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();


        static string UserId = string.Empty;
        public string PRId = string.Empty;
        string msgError = string.Empty;
        private string UserDept = string.Empty;
        private string OurRef = string.Empty;
        private string PrCode = string.Empty;
        private string RequestedDate = string.Empty;
        private string UserRef = string.Empty;
        private string RequesterName = string.Empty;
        private int CompanyId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //UserId = Request.QueryString.Get("UserId");
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                CompanyId = int.Parse(Session["CompanyId"].ToString());
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                // ((BiddingAdmin)Page.Master).subTabValue = "CompanySupplierDepViewPR.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "submitForBiddingLink";

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), CompanyId, 6, 1) && companyLogin.Usertype != "S") || companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            GeneralSetting generalSettings = generalSettingsController.FetchGeneralSettingsListByIdObj(CompanyId);

            
            PRId = Request.QueryString.Get("PrId");
            msg.Visible = false;
            if (!IsPostBack)
            {
                try
                {
                    txtStartDate.Attributes.Add("min", DateTime.Now.ToString("MM/dd/yyyy"));
                    txtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    lblDateNow.Text = DateTime.Today.ToString("dd-MM-yyyy");

                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    lblDeptName.Enabled = true;
                    PR_Master prmaster = pr_MasterController.FetchApprovePRDataByDeptIdAndPRId(CompanyId, int.Parse(PRId));
                    lblPRCode.Text = prmaster.PrCode;
                    lblRequesterName.Text = prmaster.RequestedBy;
                    lblRef.Text = prmaster.OurReference;
                    lblDeptName.Text = companyDepartmentController.GetDepartmentByDepartmentId(CompanyId).DepartmentName;
                    lblRequestedDate.Text = prmaster.DateOfRequest.ToString("dd/MM/yyyy hh:mm tt");
                    BindGV();
                    BindRejetedGv();
                    GvSubmittedBind();
                    if (generalSettings.DepartmentId != 0)
                    {
                        if (generalSettings.BidOnlyRegisteredSupplier == 1)
                        {
                            chkRegSupYes.Checked = true;
                        }
                        else
                        {
                            chkRegSupNo.Checked = true;
                        }

                        if (generalSettings.CanOverride == 1)
                        {
                            chkBidOpenYes.Checked = true;
                            txtNoOfDay.Enabled = true;
                        }
                        else
                        {
                            chkBidOpenNo.Checked = true;
                            txtNoOfDay.Enabled = false;
                            //txtStartDate.Text = DateTime.Now.ToString();
                            //lblFromTime.Text = 
                        }

                        if (generalSettings.ViewBidsOnlineUponPrCreation == 1)
                        {
                            chkViewBidsYes.Checked = true;
                        }
                        else
                        {
                            chkViewBidsNo.Checked = true;
                        }

                        txtNoOfDay.Text = generalSettings.BidOpeningPeriod.ToString();

                    }
                }
                catch (Exception ex)
                {

                }

            }

            if (Session["isBindGV"] != null && Session["isBindGV"].ToString() == "1")
            {
                BindGV();
                BindRejetedGv();
                Session["isBindGV"] = null;
                Session.Remove("isBindGV");

            }
            //ScriptManager.RegisterStartupScript(Updatepanel1, this.Updatepanel1.GetType(), "YourUniqueScriptKey", "InitClient();", true);
        }

        //----------Bind GV
        private void BindGV()
        {
            try
            {
                List<PR_Details> pr_Details = pr_DetailController.FetchPR_DetailsByDeptIdAndPrIdIsApproved(int.Parse(PRId), int.Parse(Session["CompanyId"].ToString())).Where(x => x.SubmitForBid == 0 && x.IsActive == 1).ToList();

                foreach (var item in pr_Details)
                {
                    item.noOfRepacementImages = pR_Replace_FileUploadController.FtechUploadeFiles(item.PrId, item.ItemId).Count();
                    item.noOfStanardImages = pr_FileUploadController.FtechUploadeFiles(item.PrId, item.ItemId).Count();
                }

                gvPRView.DataSource = pr_Details;
                gvPRView.DataBind();
                GvSubmittedBind();
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
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);

                gvReplacementPhotos.DataSource = pr_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId).OrderByDescending(m => m.isDefaultReplaceImage).ToList();
                gvReplacementPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalReplacementPhotos').modal('show'); });   </script>", false);
            }
            catch (Exception ex)
            {

            }
        }


        private void BindRejetedGv()
        {
            try
            {
                List<PR_Details> pr_Details = pr_DetailController.FetchPR_DetailsByDeptIdAndPrIdIsRejected(int.Parse(PRId), int.Parse(Session["CompanyId"].ToString()) ).Where(x => x.SubmitForBid == 2 && x.IsActive == 1).ToList();
                //List<Bidding> _bidding = new List<Bidding>();
                //_bidding = biddingController.FetchRejectedAndApprovedBids(int.Parse(PRId)).Where(x => x.IsApproveToViewInSupplierPortal == 2).OrderByDescending(z => z.BiddingOrderId).ToList();

                //var tempFileName = _bidding
                //             .Where(x => x.IsApproveToViewInSupplierPortal == 2)
                //             .OrderByDescending(x => x.BiddingOrderId)
                //             .Take(1);


                if (pr_Details.Count != 0)
                {

                    foreach (var item in pr_Details)
                    {
                        item.noOfRepacementImages = pR_Replace_FileUploadController.FtechUploadeFiles(item.PrId, item.ItemId).Count();
                        item.noOfStanardImages = pr_FileUploadController.FtechUploadeFiles(item.PrId, item.ItemId).Count();
                    }

                    divResubmissionForbid.Visible = true;

                    gvRejectedBidsSubmitAgain.DataSource = pr_Details;
                    gvRejectedBidsSubmitAgain.DataBind();
                    GvSubmittedBind();
                }
                else
                {
                    divResubmissionForbid.Visible = false;
                }

            }
            catch (Exception ex)
            {

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

            }
        }

        protected void btnViewRjt_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);

                gvReplacementPhotos.DataSource = pr_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvReplacementPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalReplacementPhotos').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnViewzReplacementPhotosPending_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvSubmittedBids.Rows[x].Cells[1].Text);

                gvViewReplacementImages.DataSource = pr_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvViewReplacementImages.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalViewReplacemetPhotos').modal('show'); });   </script>", false);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnViewUploadPhotosPending_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvSubmittedBids.Rows[x].Cells[1].Text);
                List<PR_FileUpload> GetTempPrFiles = pR_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvUploadedPhotos.DataSource = GetTempPrFiles;
                gvUploadedPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalUploadedPhotos').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnViewSub_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvSubmittedBids.Rows[x].Cells[1].Text);
                List<PR_FileUpload> pr_FileUpload = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvUploadFiles.DataSource = pr_FileUpload;
                gvUploadFiles.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {

            }
        }
        
        //---------------Bid Opening = 1 ; Bide Reject = 2
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                

                int gvChecked = 0;
                int BidType = 0;
                decimal itemQuantity = 0;
                List<BidSubmittedData> bidSubmittedData = new List<BidSubmittedData>();

                if (txtStartDate.Text != "")
                {

                    if (Session["update"] != null)
                    {


                        foreach (GridViewRow gvr in this.gvPRView.Rows)
                        {
                            if (((CheckBox)gvr.FindControl("CheckBox1")).Checked == true)
                            {
                                int selectedImageFromValue = 0;

                                RadioButton selectDefaultImage = (RadioButton)gvr.FindControl("rdoDefaultImage");
                                RadioButton selectStandardImage = (RadioButton)gvr.FindControl("rdoStandardImage");
                                RadioButton selectReplacementImage = (RadioButton)gvr.FindControl("rdoReplacementImage");

                                RadioButton selectRdoManual = (RadioButton)gvr.FindControl("rdoManual");
                                RadioButton selectRdoBid = (RadioButton)gvr.FindControl("rdoBid");

                                if (selectDefaultImage.Checked)
                                {
                                    selectedImageFromValue = 0;
                                }
                                if (selectStandardImage.Checked)
                                {
                                    selectedImageFromValue = 1;
                                }
                                if (selectReplacementImage.Checked)
                                {
                                    selectedImageFromValue = 2;
                                }

                                if (selectRdoManual.Checked)
                                {
                                    BidType = 0;
                                }
                                if (selectRdoBid.Checked)
                                {
                                    BidType = 1;
                                }
                                string selectvalue = selectDefaultImage.Text;

                                int noOfReplacementImages = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), int.Parse(gvr.Cells[1].Text)).Where(v => v.isDefaultReplaceImage == 1).Count();
                                int noOfStandardImages = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), int.Parse(gvr.Cells[1].Text)).Where(u => u.isDefaultStandardImage == 1).Count();


                                TextBox txtEditedQuantity = (TextBox)gvr.FindControl("txtItemQuantity");
                                if (decimal.Parse(gvr.Cells[5].Text) == decimal.Parse(txtEditedQuantity.Text))
                                {
                                    itemQuantity = decimal.Parse(gvr.Cells[5].Text);
                                }
                                else
                                {
                                    itemQuantity = decimal.Parse(txtEditedQuantity.Text);
                                    int updateItemQuantity = pr_DetailController.UpdateItemQuantityFromBidSubmitting(int.Parse(PRId), int.Parse(gvr.Cells[1].Text), itemQuantity);
                                    int updateQuantityChageHistory = pR_DetailHistoryController.SavePRHistoryDetails(int.Parse(PRId), int.Parse(gvr.Cells[1].Text), Session["UserId"].ToString(), DateTime.Now, 1, itemQuantity);
                                }


                                if (selectedImageFromValue == 0 || (selectedImageFromValue == 1 && noOfStandardImages == 1) || (selectedImageFromValue == 2 && noOfReplacementImages == 1))
                                {
                                    bidSubmittedData.Add(new BidSubmittedData(int.Parse(gvr.Cells[1].Text), gvr.Cells[2].Text, gvr.Cells[3].Text, gvr.Cells[4].Text, itemQuantity, int.Parse(gvr.Cells[1].Text), selectedImageFromValue, BidType));
                                    gvChecked = 1;
                                }
                                else
                                {

                                    if (selectedImageFromValue == 1)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Select default Image for " + gvr.Cells[2].Text + " from Standard Image collections\"; $('#errorAlert').modal('show'); });   </script>", false);
                                        //DisplayMessage("Select default Image for " + gvr.Cells[2].Text + " from Standard Image collections ", true);
                                    }
                                    if (selectedImageFromValue == 2)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Select default Image for " + gvr.Cells[2].Text + " from Replacement Image collections\"; $('#errorAlert').modal('show'); });   </script>", false);
                                        //DisplayMessage("Select default Image for " + gvr.Cells[2].Text + " from Replacement Image collections ", true);
                                    }

                                    break;
                                }
                            }
                        }

                        if (gvChecked == 1)
                        {
                            int chkbidOpen = 0;
                            int chkRegSupp = 0;
                            int chkOverride = 0;

                            if (chkBidOpenYes.Checked)
                            {
                                chkOverride = 1;
                            }
                            if (chkBidOpenNo.Checked)
                            {
                                chkOverride = 0;
                            }
                            if (chkRegSupYes.Checked)
                            {
                                chkRegSupp = 1;
                            }
                            if (chkRegSupNo.Checked)
                            {
                                chkRegSupp = 0;
                            }
                            if (chkViewBidsYes.Checked)
                            {
                                chkbidOpen = 1;
                            }
                            if (chkViewBidsNo.Checked)
                            {
                                chkbidOpen = 0;
                            }

                            foreach (var item in bidSubmittedData)
                            {
                                List<PR_Replace_FileUpload> fetchReplacementImages = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), item.ItemId).Where(r => r.isDefaultReplaceImage == 1).ToList();
                                List<PR_FileUpload> fetchStandardImages = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), item.ItemId).Where(k => k.isDefaultStandardImage == 1).ToList();
                                if (item.DefaultImageFrom == 0)
                                {
                                    string imageURl = addItemController.FetchItemObj(item.ItemId).ImagePath;
                                    if (imageURl == null)
                                    {
                                        imageURl = "";
                                    }
                                    biddingController.SaveBidding(int.Parse(PRId), item.ItemId, item.ItemId, int.Parse(PRId), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtStartDate.Text).AddDays(int.Parse(txtNoOfDay.Text)), DateTime.Now, UserId, 1, 1, CompanyId, decimal.Parse(txtNoOfDay.Text), chkOverride, chkRegSupp, chkbidOpen, txtCondition.Text, item.DefaultImageFrom, imageURl);
                                    int rejectedCount = pr_DetailController.FetchPR_DetailsBidComparion(int.Parse(PRId)).PrIsRejectedCount;
                                    pr_DetailController.UpdateUpdateForBid(int.Parse(PRId), item.ItemId, 1, rejectedCount);
                                    pr_DetailController.UpdateUpdateForBidType(int.Parse(PRId), item.ItemId, item.BidTypeManualOrBid);

                                }
                                else if (item.DefaultImageFrom == 2 && fetchReplacementImages.Count() == 1)
                                {
                                    string imageURl = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), item.ItemId).Where(d => d.isDefaultReplaceImage == 1).FirstOrDefault().FilePath;
                                    if (imageURl == null)
                                    {
                                        imageURl = "";
                                    }
                                    biddingController.SaveBidding(int.Parse(PRId), item.ItemId, item.ItemId, int.Parse(PRId), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtStartDate.Text).AddDays(int.Parse(txtNoOfDay.Text)), DateTime.Now, UserId, 1, 1, CompanyId, decimal.Parse(txtNoOfDay.Text), chkOverride, chkRegSupp, chkbidOpen, txtCondition.Text, item.DefaultImageFrom, imageURl);
                                    int rejectedCount = pr_DetailController.FetchPR_DetailsBidComparion(int.Parse(PRId)).PrIsRejectedCount;
                                    pr_DetailController.UpdateUpdateForBid(int.Parse(PRId), item.ItemId, 1, rejectedCount);
                                    pr_DetailController.UpdateUpdateForBidType(int.Parse(PRId), item.ItemId, item.BidTypeManualOrBid);

                                }
                                else if (item.DefaultImageFrom == 1 && fetchStandardImages.Count() == 1)
                                {
                                    string imageURl = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), item.ItemId).Where(d => d.isDefaultStandardImage == 1).FirstOrDefault().FilePath;
                                    if (imageURl == null)
                                    {
                                        imageURl = "";
                                    }
                                    biddingController.SaveBidding(int.Parse(PRId), item.ItemId, item.ItemId, int.Parse(PRId), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtStartDate.Text).AddDays(int.Parse(txtNoOfDay.Text)), DateTime.Now, UserId, 1, 1, CompanyId, decimal.Parse(txtNoOfDay.Text), chkOverride, chkRegSupp, chkbidOpen, txtCondition.Text, item.DefaultImageFrom, imageURl);
                                    int rejectedCount = pr_DetailController.FetchPR_DetailsBidComparion(int.Parse(PRId)).PrIsRejectedCount;
                                    pr_DetailController.UpdateUpdateForBid(int.Parse(PRId), item.ItemId, 1, rejectedCount);
                                    pr_DetailController.UpdateUpdateForBidType(int.Parse(PRId), item.ItemId, item.BidTypeManualOrBid);
                                }
                                else
                                {
                                    if (item.DefaultImageFrom == 1)
                                    {
                                        msgError += "Please set the Default image for " + item.ItemName + " from Standard image collections\n";

                                    }
                                    if (item.DefaultImageFrom == 2)
                                    {
                                        msgError += "Please set the Default image for " + item.ItemName + " from Replacement image collections\n";

                                    }

                                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"" + msgError + "\"; $('#errorAlert').modal('show'); });   </script>", false);

                                }

                            }
                            BindGV();
                            BindRejetedGv();
                        }

                        foreach (GridViewRow gvr in this.gvRejectedBidsSubmitAgain.Rows)
                        {
                            if (((CheckBox)gvr.FindControl("CheckBox3")).Checked == true)
                            {

                                int selectedImageFromValue = 0;

                                RadioButton selectDefaultImage = (RadioButton)gvr.FindControl("rdoRejectedDefaultImage");
                                RadioButton selectStandardImage = (RadioButton)gvr.FindControl("rdoRejectedStandardImage");
                                RadioButton selectReplacementImage = (RadioButton)gvr.FindControl("rdoRejectedReplacementImage");


                                if (selectDefaultImage.Checked)
                                {
                                    selectedImageFromValue = 0;
                                }
                                if (selectStandardImage.Checked)
                                {
                                    selectedImageFromValue = 1;
                                }
                                if (selectReplacementImage.Checked)
                                {
                                    selectedImageFromValue = 2;
                                }

                                RadioButton selectRdoManual = (RadioButton)gvr.FindControl("rdoManualRjt");
                                RadioButton selectRdoBid = (RadioButton)gvr.FindControl("rdoBidRjt");

                                if (selectRdoManual.Checked)
                                {
                                    BidType = 0;
                                }
                                if (selectRdoBid.Checked)
                                {
                                    BidType = 1;
                                }



                                int noOfReplacementImages = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), int.Parse(gvr.Cells[1].Text)).Where(v => v.isDefaultReplaceImage == 1).Count();
                                int noOfStandardImages = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), int.Parse(gvr.Cells[1].Text)).Where(u => u.isDefaultStandardImage == 1).Count();

                                if (selectedImageFromValue == 0 || (selectedImageFromValue == 1 && noOfStandardImages == 1) || (selectedImageFromValue == 2 && noOfReplacementImages == 1))
                                {
                                    bidSubmittedData.Add(new BidSubmittedData(int.Parse(gvr.Cells[1].Text), gvr.Cells[2].Text, gvr.Cells[3].Text, gvr.Cells[4].Text, decimal.Parse(gvr.Cells[5].Text), int.Parse(gvr.Cells[1].Text), selectedImageFromValue, BidType));
                                    gvChecked = 2;
                                }
                                else
                                {

                                    if (selectedImageFromValue == 1)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Select default Image for " + gvr.Cells[2].Text + " from Standard Image collections\"; $('#errorAlert').modal('show'); });   </script>", false);
                                        //DisplayMessage("Select default Image for " + gvr.Cells[2].Text + " from Standard Image collections ", true);
                                    }
                                    if (selectedImageFromValue == 2)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Select default Image for " + gvr.Cells[2].Text + " from Replacement Image collections\"; $('#errorAlert').modal('show'); });   </script>", false);
                                        //DisplayMessage("Select default Image for " + gvr.Cells[2].Text + " from Replacement Image collections ", true);
                                    }

                                    break;
                                }
                            }
                        }

                        if (gvChecked == 2)
                        {

                            int chkbidOpen = 0;
                            int chkRegSupp = 0;
                            int chkOverride = 0;

                            if (chkBidOpenYes.Checked)
                            {
                                chkOverride = 1;
                            }
                            if (chkBidOpenNo.Checked)
                            {
                                chkOverride = 0;
                            }
                            if (chkRegSupYes.Checked)
                            {
                                chkRegSupp = 1;
                            }
                            if (chkRegSupNo.Checked)
                            {
                                chkRegSupp = 0;
                            }
                            if (chkViewBidsYes.Checked)
                            {
                                chkbidOpen = 1;
                            }
                            if (chkViewBidsNo.Checked)
                            {
                                chkbidOpen = 0;
                            }

                            foreach (var item in bidSubmittedData)
                            {
                                List<PR_Replace_FileUpload> fetchReplacementImages = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), item.ItemId).Where(r => r.isDefaultReplaceImage == 1).ToList();
                                List<PR_FileUpload> fetchStandardImages = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), item.ItemId).Where(k => k.isDefaultStandardImage == 1).ToList();

                                if (item.DefaultImageFrom == 0)
                                {
                                    string imageURl = addItemController.FetchItemListByIdObj(item.ItemId).ImagePath;
                                    if (imageURl == null)
                                    {
                                        imageURl = "";
                                    }
                                    biddingController.SaveBidding(int.Parse(PRId), item.ItemId, item.ItemId, int.Parse(PRId), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtStartDate.Text).AddDays(int.Parse(txtNoOfDay.Text)), DateTime.Now, UserId, 1, 1, CompanyId, decimal.Parse(txtNoOfDay.Text), chkOverride, chkRegSupp, chkbidOpen, txtCondition.Text, 0, imageURl);
                                    int rejectedCount = pr_DetailController.FetchPR_DetailsByPrIdAndItemId(int.Parse(PRId), item.ItemId).PrIsRejectedCount;
                                    pr_DetailController.UpdateRejectUpdateForBid(int.Parse(PRId), item.ItemId, 1);
                                    pr_DetailController.UpdateUpdateForBidType(int.Parse(PRId), item.ItemId, BidType);

                                }
                                else if (item.DefaultImageFrom == 2 && fetchReplacementImages.Count() == 1)
                                {
                                    string imageURl = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), item.ItemId).Where(d => d.isDefaultReplaceImage == 1).FirstOrDefault().FilePath;
                                    if (imageURl == null)
                                    {
                                        imageURl = "";
                                    }
                                    biddingController.SaveBidding(int.Parse(PRId), item.ItemId, item.ItemId, int.Parse(PRId), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtStartDate.Text).AddDays(int.Parse(txtNoOfDay.Text)), DateTime.Now, UserId, 1, 1, CompanyId, decimal.Parse(txtNoOfDay.Text), chkOverride, chkRegSupp, chkbidOpen, txtCondition.Text, 0, imageURl);
                                    int rejectedCount = pr_DetailController.FetchPR_DetailsByPrIdAndItemId(int.Parse(PRId), item.ItemId).PrIsRejectedCount;
                                    pr_DetailController.UpdateRejectUpdateForBid(int.Parse(PRId), item.ItemId, 1);
                                    pr_DetailController.UpdateUpdateForBidType(int.Parse(PRId), item.ItemId, BidType);

                                }

                                else if (item.DefaultImageFrom == 1 && fetchStandardImages.Count() == 1)
                                {
                                    string imageURl = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), item.ItemId).Where(d => d.isDefaultStandardImage == 1).FirstOrDefault().FilePath;

                                    if (imageURl == null)
                                    {
                                        imageURl = "";
                                    }
                                    biddingController.SaveBidding(int.Parse(PRId), item.ItemId, item.ItemId, int.Parse(PRId), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtStartDate.Text).AddDays(int.Parse(txtNoOfDay.Text)), DateTime.Now, UserId, 1, 1, CompanyId, decimal.Parse(txtNoOfDay.Text), chkOverride, chkRegSupp, chkbidOpen, txtCondition.Text, 0, imageURl);
                                    int rejectedCount = pr_DetailController.FetchPR_DetailsByPrIdAndItemId(int.Parse(PRId), item.ItemId).PrIsRejectedCount;
                                    pr_DetailController.UpdateRejectUpdateForBid(int.Parse(PRId), item.ItemId, 1);
                                    pr_DetailController.UpdateUpdateForBidType(int.Parse(PRId), item.ItemId, BidType);
                                }
                                else
                                {
                                    if (item.DefaultImageFrom == 1)
                                    {
                                        msgError += "Please set the Default image for " + item.ItemName + " from Standard image collections\n";

                                    }
                                    if (item.DefaultImageFrom == 2)
                                    {
                                        msgError += "Please set the Default image for " + item.ItemName + " from Replacement image collections\n";

                                    }

                                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"" + msgError + "\"; $('#errorAlert').modal('show'); });   </script>", false);
                                    //DisplayMessage(msgError, true);
                                }

                                BindGV();
                                BindRejetedGv();
                                //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#SuccessAlert').modal('show'); });   </script>", false);

                            }
                        }

                        Session["update"] = null;
                    }

                    if (msgError == "")
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'CompanySupplierDepViewPR.aspx'}); });   </script>", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please Select Bid Starting Day', showConfirmButton: false,timer: 1500}); });   </script>", false);
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
        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompnayPurchaseRequestNote.aspx");
        }
        protected void btnBOMRjt_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvRejectedBidsSubmitAgain.Rows[x].Cells[1].Text);
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

        protected void btnBOMSub_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvSubmittedBids.Rows[x].Cells[1].Text);
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

        protected void lblSettings_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvSubmittedBids.Rows[x].Cells[1].Text);
                gvSettingsTableModal.DataSource = biddingController.GetBiddingGeneralSettings(int.Parse(PRId), itemId);
                gvSettingsTableModal.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal3').modal('show'); });   </script>", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lblSettingsRjt_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvRejectedBidsSubmitAgain.Rows[x].Cells[1].Text);
                gvSettingsTableModal.DataSource = biddingController.GetBiddingGeneralSettings(int.Parse(PRId), itemId);
                gvSettingsTableModal.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal3').modal('show'); });   </script>", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lnkBtnEditRjt_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                int itemid = int.Parse(gvRejectedBidsSubmitAgain.Rows[x].Cells[1].Text);
                string biddingOrderId = (gvRejectedBidsSubmitAgain.Rows[x].Cells[7].Text);
                List<Bidding> _biddingSttings = biddingController.GetBiddingGeneralSettings(int.Parse(PRId), itemid).Where(v => v.BiddingOrderId == biddingOrderId).ToList();
                foreach (var item in _biddingSttings)
                {
                    if (item.CanOverride == 1)
                    {
                        chkBidOpenYes.Checked = true;
                    }
                    if (item.CanOverride == 0)
                    {
                        chkBidOpenNo.Checked = true;
                    }
                    if (item.BidOnlyRegisteredSupplier == 1)
                    {
                        chkRegSupYes.Checked = true;
                    }
                    if (item.BidOnlyRegisteredSupplier == 0)
                    {
                        chkRegSupNo.Checked = true;
                    }
                    if (item.ViewBidsOnlineUponPrCreation == 1)
                    {
                        chkViewBidsYes.Checked = true;
                    }
                    if (item.ViewBidsOnlineUponPrCreation == 0)
                    {
                        chkViewBidsYes.Checked = true;
                    }
                    txtNoOfDay.Text = item.BidOpeningPeriod.ToString("N0");
                    txtCondition.Text = item.BidTermsAndConditions;
                }

            }
            catch (Exception ex)
            {


            }
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("CompanySupplierDepViewPR.aspx");
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

        private void GvSubmittedBind()
        {
            try
            {
                List<Bidding> _bidding = new List<Bidding>();
                // _bidding = biddingController.FetchRejectedAndApprovedBids(int.Parse(PRId)).Where(x=>x.IsApproveToViewInSupplierPortal == 1 || x.IsApproveToViewInSupplierPortal == 2 || x.IsApproveToViewInSupplierPortal == 0 && x.BidTypeMaualOrBid == 1).ToList();
                _bidding = biddingController.FetchRejectedAndApprovedBids(int.Parse(PRId),1).Where(x => x.IsApproveToViewInSupplierPortal == 1 || x.IsApproveToViewInSupplierPortal == 2 || x.IsApproveToViewInSupplierPortal == 0).ToList();
                //List<PR_Details> pr_DetailsSubmited = new List<PR_Details>();
                //pr_DetailsSubmited = pr_DetailController.FetchPR_DetailsByDeptIdAndPrIdIsApproved(int.Parse(PRId)).Where((x => x.SubmitForBid == 1 || x.SubmitForBid==2 && x.IsActive == 1)).ToList();
                if (_bidding.Count == 0 || _bidding.Count < 0)
                {
                    lblMsgSubmitedBids.Visible = true;
                }
                else
                {
                    lblMsgSubmitedBids.Visible = false;
                    gvSubmittedBids.DataSource = _bidding;
                    gvSubmittedBids.DataBind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public class BidSubmittedData
        {
            public BidSubmittedData(int ItemId, string ItemName, string ItemDescription, string Purpose, decimal ItemQuantity, int Replacement, int DefaultImageFrom, int BidTypeManualOrBid)
            {
                itemId = ItemId;
                itemName = ItemName;
                itemDescription = ItemDescription;
                purpose = Purpose;
                itemQuantity = ItemQuantity;
                replacement = Replacement;
                defaultImageFrom = DefaultImageFrom;
                bidTypeManualOrBid = BidTypeManualOrBid;
            }

            private int itemId;
            private string itemName;
            private string itemDescription;
            private string purpose;
            private decimal itemQuantity;
            private int replacement;
            private int defaultImageFrom;
            private int bidTypeManualOrBid;

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

            public int DefaultImageFrom
            {
                get { return defaultImageFrom; }
                set { defaultImageFrom = value; }
            }

            public int BidTypeManualOrBid
            {
                get { return bidTypeManualOrBid; }
                set { bidTypeManualOrBid = value; }
            }
        }

        protected void btnViewUploadPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);
                List<PR_FileUpload> GetTempPrFiles = pR_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvUploadedPhotos.DataSource = GetTempPrFiles.OrderByDescending(v => v.isDefaultStandardImage).ToList();
                gvUploadedPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalUploadedPhotos').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAddReplacementPhotos_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnReplacementImageUpload_Click(object sender, EventArgs e)
        {
            try
            {

                string NewDirectory = HttpContext.Current.Server.MapPath("PrReplacementFiles/" + PRId);
                int returnType = CreateDirectoryIfNotExists(NewDirectory);
                string folderFilePath = string.Empty;
                int ItemId = int.Parse(hdnItemIdForReplacementImage.Value);
                HttpFileCollection uploads = HttpContext.Current.Request.Files;

                for (int i = 0; i < uploads.Count; i++)
                {

                    if (uploads.AllKeys[i] == "fileReplace[]")
                    {
                        HttpPostedFile postedFile = uploads[i];


                        List<PR_Replace_FileUpload> GetFilesByPrItemId = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), ItemId);
                        int maxNumber = 0;

                        foreach (var item in GetFilesByPrItemId)
                        {
                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                            if (CalNumber > maxNumber)
                                maxNumber = CalNumber;
                        }

                        string CreateFileName = PRId + "_" + ItemId.ToString() + "_" + (maxNumber + 1).ToString();
                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                        string FileName = Path.GetFileName(postedFile.FileName);
                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                        if (postedFile.ContentLength > 0)
                        {
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/PrReplacementFiles/" + PRId + "/" + filename01)))
                            {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/PrReplacementFiles/" + PRId + "/" + filename01));
                            }
                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PrReplacementFiles/" + PRId + "/" + filename01));
                            folderFilePath = "~/PrReplacementFiles/" + PRId + "/" + filename01;
                            int saveReplacementFileUpload = pR_Replace_FileUploadController.SaveFileUpload(CompanyId, ItemId, int.Parse(PRId), folderFilePath, filename01);
                            int replacementYesStatus = pr_DetailController.updateReplacementImageStatus(int.Parse(PRId), ItemId, 1);
                        }
                    }
                }
                BindGV();
                BindRejetedGv();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int CreateDirectoryIfNotExists(string NewDirectory)
        {
            try
            {
                int returnType = 0;
                // Checking the existance of directory
                if (!Directory.Exists(NewDirectory))
                {
                    //delete
                    //If No any such directory then creates the new one
                    Directory.CreateDirectory(NewDirectory);
                    returnType = 1;
                }
                else
                {
                    //Label1.Text = "Directory Exist";
                    returnType = 0;
                }
                return returnType;
            }
            catch (IOException _err)
            {
                throw _err;
                //Label1.Text = _err.Message; ;
            }
        }

        [WebMethod]
        public static string deleteReplacementImage(string prId, string itemId, string fileName)
        {
            PR_Replace_FileUploadController pR_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
            PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();

            int returnStatus = pR_Replace_FileUploadController.DeleteParticularReplaceFile(int.Parse(prId), int.Parse(itemId), fileName);
            if (returnStatus > 0)
            {
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(fileName));
                List<PR_Replace_FileUpload> prReplaceImages = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(prId), int.Parse(itemId));
                if (prReplaceImages.Count() == 0)
                {
                    pr_DetailController.updateReplacementImageStatus(int.Parse(prId), int.Parse(itemId), 0);
                }
                HttpContext.Current.Session["isBindGV"] = "1";
                return "1";
            }
            else
            {
                return "0";
            }

        }


        [WebMethod]
        public static string updateDefaultReplacementImage(string prId, string itemId, string fileName, string check)
        {
            string result = "";
            PR_Replace_FileUploadController pR_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
            PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
            PR_FileUploadController pR_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
            BiddingController biddingController = ControllerFactory.CreateBiddingController();
            int updateReeaseAllImageStatus = pR_Replace_FileUploadController.updateReleaseImageDefault(int.Parse(prId), int.Parse(itemId));
            int updateReleaseImageUrlStatus = biddingController.updateDisplayImageUrl("", int.Parse(prId), int.Parse(itemId));
            if (updateReeaseAllImageStatus > 0)
            {
                result = "1";
            }
            if (check == "1")
            {
                int ReleaseStanardImageStatus = pR_FileUploadController.updateReleaseImageDefault(int.Parse(prId), int.Parse(itemId));
                int updateDefaultImageStatus = pR_Replace_FileUploadController.updateDefaultReplaceImage(int.Parse(prId), int.Parse(itemId), fileName, 1);
                int updateDisplayImageUrlStatus = biddingController.updateDisplayImageUrl(fileName, int.Parse(prId), int.Parse(itemId));
                if (updateDefaultImageStatus > 0)
                {
                    result = "1";
                }
                else
                {
                    result = "0";
                }
            }
            return result;
        }

        [WebMethod]
        public static string updateDefaultStandardImage(string prId, string itemId, string fileName, string check)
        {
            string result = "";
            PR_FileUploadController pR_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
            PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
            PR_Replace_FileUploadController pR_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
            BiddingController biddingController = ControllerFactory.CreateBiddingController();
            int updateReeaseDisplayImageUrlStatus = biddingController.updateDisplayImageUrl("", int.Parse(prId), int.Parse(itemId));
            int updateReeaseAllImageStatus = pR_FileUploadController.updateReleaseImageDefault(int.Parse(prId), int.Parse(itemId));

            if (updateReeaseAllImageStatus > 0)
            {
                result = "1";
            }
            if (check == "1")
            {
                int ReleaseReplaceImageStatus = pR_Replace_FileUploadController.updateReleaseImageDefault(int.Parse(prId), int.Parse(itemId));
                int updateDefaultStandardImageStatus = pR_FileUploadController.updateDefaultStanardImage(int.Parse(prId), int.Parse(itemId), fileName, 1);
                int updateDisplayImageUrlStatus = biddingController.updateDisplayImageUrl(fileName, int.Parse(prId), int.Parse(itemId));
                if (updateDefaultStandardImageStatus > 0)
                {
                    result = "1";
                }
                else
                {
                    result = "0";
                }
            }
            return result;
        }
        private void DisplayMessage(string message, bool isError)
        {
            msg.Visible = true;
            if (isError)
            {
                lbMessage.CssClass = "failMessage";
                msg.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else
            {
                lbMessage.CssClass = "successMessage";
                msg.Attributes["class"] = "alert alert-success alert-dismissable";
            }

            lbMessage.Text = message;

        }

        protected void rdoDefaultImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((RadioButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);
                int releaseReplaceImages = pr_Replace_FileUploadController.updateReleaseImageDefault(int.Parse(PRId), itemId);
                int releaseStanardImages = pR_FileUploadController.updateReleaseImageDefault(int.Parse(PRId), itemId);
                AddItemController addItemController = ControllerFactory.CreateAddItemController();
                AddItem addItemObj = addItemController.FetchItemListByIdObj(itemId);
                if (addItemObj.ImagePath != null)
                {
                    int updateReeaseDisplayImageUrlStatus = biddingController.updateDisplayImageUrl(addItemObj.ImagePath, int.Parse(PRId), itemId);
                }
            }
            catch (Exception)
            {


            }

        }

        protected void rdoStandardImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((RadioButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);
                int releaseReplaceImages = pr_Replace_FileUploadController.updateReleaseImageDefault(int.Parse(PRId), itemId);
            }
            catch (Exception)
            {


            }
        }

        protected void rdoReplacementImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((RadioButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);
                int releaseStanardImages = pR_FileUploadController.updateReleaseImageDefault(int.Parse(PRId), itemId);
            }
            catch (Exception)
            {


            }
        }

        protected void btnVieRejectReplacementPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvRejectedBidsSubmitAgain.Rows[x].Cells[1].Text);

                gvReplacementPhotos.DataSource = pr_Replace_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId).OrderByDescending(m => m.isDefaultReplaceImage).ToList();
                gvReplacementPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalReplacementPhotos').modal('show'); });   </script>", false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnViewRejectUploadPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvRejectedBidsSubmitAgain.Rows[x].Cells[1].Text);

                List<PR_FileUpload> GetTempPrFiles = pR_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvUploadedPhotos.DataSource = GetTempPrFiles.OrderByDescending(v => v.isDefaultStandardImage).ToList();
                gvUploadedPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalUploadedPhotos').modal('show'); });   </script>", false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void rdoRejectedDefaultImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((RadioButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvRejectedBidsSubmitAgain.Rows[x].Cells[1].Text);
                int releaseReplaceImages = pr_Replace_FileUploadController.updateReleaseImageDefault(int.Parse(PRId), itemId);
                int releaseStanardImages = pR_FileUploadController.updateReleaseImageDefault(int.Parse(PRId), itemId);
                int updateReleaseDisplayImageUrlStatus = biddingController.updateDisplayImageUrl("", int.Parse(PRId), itemId);
                AddItemController addItemController = ControllerFactory.CreateAddItemController();
                AddItem addItemObj = addItemController.FetchItemListByIdObj(itemId);

                if (addItemObj.ImagePath != null)
                {
                    int updateDisplayImageUrlStatus = biddingController.updateDisplayImageUrl(addItemObj.ImagePath, int.Parse(PRId), itemId);
                }
            }
            catch (Exception)
            {


            }
        }

        protected void rdoRejectedStandardImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((RadioButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvRejectedBidsSubmitAgain.Rows[x].Cells[1].Text);
                int releaseReplaceImages = pr_Replace_FileUploadController.updateReleaseImageDefault(int.Parse(PRId), itemId);
                int updateReleaseDisplayImageUrlStatus = biddingController.updateDisplayImageUrl("", int.Parse(PRId), itemId);
            }
            catch (Exception)
            {


            }
        }

        protected void rdoRejectedReplacementImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((RadioButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPRView.Rows[x].Cells[1].Text);
                int releaseStanardImages = pR_FileUploadController.updateReleaseImageDefault(int.Parse(PRId), itemId);
                int updateReleaseDisplayImageUrlStatus = biddingController.updateDisplayImageUrl("", int.Parse(PRId), itemId);
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

        //void Page_PreRender(object obj, EventArgs e)
        //{
        //    ViewState["update"] = Session["update"];
        //}
    }
}