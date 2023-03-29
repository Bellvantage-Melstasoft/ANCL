using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using System.IO;
using CLibrary.Domain;
using System.Web.Script.Serialization;
using System.Data;
using System.Web.UI.HtmlControls;

namespace BiddingSystem
{
    public partial class CompanyCustomizeBidding : System.Web.UI.Page
    {
        int CompanyId = 0;
        static string UserId = string.Empty;
        static int categoryId = 0;
        static string imagePath = string.Empty;
        static string categoryName = string.Empty;
        static DateTime createdDate;
        static string createdBy = string.Empty;
        static DateTime updatedDate;
        static string updatedBy = string.Empty;
        static int isActive = 0;
        static string itemName = string.Empty;
        static string refno = string.Empty;
        static int itemId = 0;
        static int SubCategoryId = 0;
        public static string BidOrderId = string.Empty;
        int QuotationId = 0;
        public static decimal itemQuontity = 0;
        static string SubCategoryName = string.Empty;
        public static string EndTime = string.Empty;
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        SupplierBOMController supplierBOMController = ControllerFactory.CreatesupplierBOMController();
        PR_DetailController pR_DetailController = ControllerFactory.CreatePR_DetailController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        BidHistoryController bidHistoryController = ControllerFactory.CreateBidHistoryController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        SupplierBiddingFileUploadController supplierBiddingFileUploadController = ControllerFactory.CreateSupplierBiddingFileUploadController();

        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefManualBids";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabManualBids";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyCustomizeBidding.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "ManualBidsLink";

                    CompanyId = int.Parse(Session["CompanyId"].ToString());
                    UserId = Session["UserId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 7, 3) && companyLogin.Usertype != "S") || companyLogin.Usertype != "GA")
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
                    divItemDetails.Visible = false;
                    ddlSupplier.DataSource = supplierController.GetSupplierListisApproved().ToList();
                    ddlSupplier.DataTextField = "SupplierName";
                    ddlSupplier.DataValueField = "SupplierId";
                    ddlSupplier.DataBind();
                    ddlSupplier.Items.Insert(0, new ListItem("--SELECT SUPPLIER--", "0"));
                    clearDiv();
                    if (ddlProgressPR.SelectedValue !="" || ddlPRItems.SelectedValue != "")
                    {
                        gvUploadFiles.DataSource = pr_FileUploadController.FtechUploadeFiles(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue));
                        gvUploadFiles.DataBind();
                    }
                    

                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        protected void ddlProgressPR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlProgressPR.SelectedIndex != 0)
                {

                    int supplierId = int.Parse(ddlSupplier.SelectedValue);
                    SupplierAssignedToCompany supplierAssigneToCompanyObj = supplierAssigneToCompanyController.GetSupplierOfCompanyObj(supplierId, CompanyId);
                    if (supplierAssigneToCompanyObj.SupplierId > 0)
                    {
                        if (supplierAssigneToCompanyObj.SupplierFollowing == 1)
                        {
                            List<Bidding> progressPRItemsList = biddingController.GetProgressPRItemsByPrIdManualBid(int.Parse(ddlProgressPR.SelectedValue), CompanyId, int.Parse(ddlSupplier.SelectedValue), 1);
                            ddlPRItems.DataSource = progressPRItemsList;
                            ddlPRItems.DataValueField = "ItemId";
                            ddlPRItems.DataTextField = "ItemName";
                            ddlPRItems.DataBind();
                            ddlPRItems.Items.Insert(0, new ListItem("--SELECT ITEM--", "0"));
                        }
                        else if (supplierAssigneToCompanyObj.SupplierFollowing == 0)
                        {
                            List<Bidding> progressPRItemsList = biddingController.GetProgressPRItemsByPrIdManualBid(int.Parse(ddlProgressPR.SelectedValue), CompanyId, int.Parse(ddlSupplier.SelectedValue), 0);
                            ddlPRItems.DataSource = progressPRItemsList;
                            ddlPRItems.DataValueField = "ItemId";
                            ddlPRItems.DataTextField = "ItemName";
                            ddlPRItems.DataBind();
                            ddlPRItems.Items.Insert(0, new ListItem("--SELECT ITEM--", "0"));
                        }
                    }
                }
                else
                {
                    ddlPRItems.Items.Clear();
                }



                clearDiv();


            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSupplier.SelectedIndex != 0)
                {
                    int supplierId = int.Parse(ddlSupplier.SelectedValue);
                    SupplierAssignedToCompany supplierAssigneToCompanyObj = supplierAssigneToCompanyController.GetSupplierOfCompanyObj(supplierId, CompanyId);
                    if (supplierAssigneToCompanyObj.SupplierId > 0)
                    {
                        if (supplierAssigneToCompanyObj.SupplierFollowing == 1)
                        {
                            List<Bidding> progressPRList = biddingController.GetProgressPR(supplierId, CompanyId, 1);
                            ddlProgressPR.DataSource = progressPRList;
                            ddlProgressPR.DataValueField = "PrId";
                            ddlProgressPR.DataTextField = "PrCode";
                            ddlProgressPR.DataBind();
                            ddlProgressPR.Items.Insert(0, new ListItem("--SELECT PR--", "0"));
                        }
                        else if (supplierAssigneToCompanyObj.SupplierFollowing == 0)
                        {
                            List<Bidding> progressPRList = biddingController.GetProgressPR(supplierId, CompanyId, 0);
                            ddlProgressPR.DataSource = progressPRList;
                            ddlProgressPR.DataValueField = "PrId";
                            ddlProgressPR.DataTextField = "PrCode";
                            ddlProgressPR.DataBind();
                            ddlProgressPR.Items.Insert(0, new ListItem("--SELECT PR--", "0"));
                        }
                    }  
                }
                else
                {

                }
                clearDiv();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlPRItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPRItems.SelectedIndex != 0)
                {
                    List<PR_BillOfMeterial> pr_BillOfMeterial = pr_BillOfMeterialController.GetListWithSupplierBOM(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue));
                    Bidding biddingObj = biddingController.GetBiddingDetails(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue));
                    if (biddingObj.PrId != 0)
                    {
                        EndTime = biddingObj.EndDate.ToString();
                        hdnEnddateTime.Value = EndTime;
                       // BidOrderId = biddingObj.BiddingOrderId;
                    }


                    // gvBOM.DataSource = supplierBOMController.GetSupplierList(int.Parse(ddlSupplier.SelectedValue), int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue));
                    gvBOM.DataSource = pr_BillOfMeterial;

                    gvBOM.DataBind();

                    gvUploadFiles.DataSource = pr_FileUploadController.FtechUploadeFiles(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue));
                    gvUploadFiles.DataBind();

                    SupplierBiddingFileUploadController supplierBiddingFileUploadController = ControllerFactory.CreateSupplierBiddingFileUploadController();
                    gvUserDocuments.DataSource = supplierBiddingFileUploadController.GetFilesByQuotationId(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue), int.Parse(ddlSupplier.SelectedValue));
                    gvUserDocuments.DataBind();

                    PR_Details PR_DetailsoBJ = pR_DetailController.FetchPR_DetailsByPrIdAndItemId(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue));

                    if (PR_DetailsoBJ.ItemId != 0)
                    {
                        itemQuontity = PR_DetailsoBJ.ItemQuantity;
                        hdnItemQuantity.Value = itemQuontity.ToString();
                    }
                    divItemDetails.Visible = true;

                    List<SupplierQuotation> _supplierQuotationAlreadyBid = supplierQuotationController.GetAlreadyBidCountOfSupplier(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue), int.Parse(ddlSupplier.SelectedValue)).ToList();
                    foreach (var item in _supplierQuotationAlreadyBid)
                    {
                        txtTermsConditions.Text = item.SupplierTermsConditions;
                        txtUnitPrice.Text = item.PerItemPrice.ToString();
                        txtNBT.Text = item.NbtAmount.ToString();
                        txtVAT.Text = item.VatAmount.ToString();
                        txtTotalPrice.Text = (decimal.Parse(itemQuontity.ToString()) * decimal.Parse(item.PerItemPrice.ToString())).ToString();
                        if (txtNBT.Text != "" && txtVAT.Text != "")
                        {
                            chkVatNbt.Checked = true;
                            txtSubTotal.Text = (decimal.Parse(txtTotalPrice.Text) + decimal.Parse(txtVAT.Text) + decimal.Parse(txtNBT.Text)).ToString();
                        }
                        if (txtNBT.Text == "" && txtVAT.Text == "")
                        {
                            chkVatNbt.Checked = false;
                            txtSubTotal.Text = item.TotalAmount.ToString();
                        }

                        
                        
                    }

                }
                else
                {
                    divItemDetails.Visible = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void lbtnview_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvUserDocuments.Rows[x].Cells[4].Text;
                System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(filepath));
            }
            catch (Exception)
            {

            }
        }

        protected void lbtnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvUserDocuments.Rows[x].Cells[4].Text;
                if (!string.IsNullOrEmpty(filepath) && File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(filepath));
                    HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath(filepath));
                    HttpContext.Current.Response.End();
                }
                else
                {
                    HttpContext.Current.Response.ContentType = "text/plain";
                    HttpContext.Current.Response.Write("File not be found!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvUserDocuments_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvUserDocuments.DataSource = supplierBiddingFileUploadController.GetFilesByQuotationId(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue), int.Parse(ddlSupplier.SelectedValue));
                gvUserDocuments.PageIndex = e.NewPageIndex;
                gvUserDocuments.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvUserDocuments.Rows[x].Cells[4].Text;
                int deleteStatus = supplierBiddingFileUploadController.DeleteFileUploads(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue), int.Parse(ddlSupplier.SelectedValue), filepath);
                if (deleteStatus > 0)
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                }
                else
                {
                }
                gvUserDocuments.DataSource = supplierBiddingFileUploadController.GetFilesByQuotationId(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue), int.Parse(ddlSupplier.SelectedValue));
                gvUserDocuments.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //protected void gvBOM_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        RadioButtonList rbl = (RadioButtonList)e.Row.FindControl("RadioButtonYes");
        //        // Query the DataSource & get the corresponding data....
        //        // ...
        //        // if Read -> then Select 0 else if Edit then Select 1...
                
        //    }
        //}

        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
            
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                txtTotalPrice.Attributes.Add("readonly", "readonly");
                decimal PerItemAmount = decimal.Parse(txtUnitPrice.Text);
                decimal TotalAmount = decimal.Parse(txtTotalPrice.Text);
                decimal VatAmout = decimal.Parse(txtVAT.Text == "" ? "0" : txtVAT.Text);
                decimal NbtAmount = decimal.Parse(txtNBT.Text == "" ? "0" : txtNBT.Text);
                decimal SubTotal = decimal.Parse(txtSubTotal.Text);
                string Conditions = txtTermsConditions.Text;
                int save = 0;

                int existingBidsQuotationNumber = biddingController.GetBiddingDetailsExisting(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue), int.Parse(ddlSupplier.SelectedValue)).QuotationNo;
                if (existingBidsQuotationNumber == 0)
                {
                    Bidding biddingObj = biddingController.GetBiddingOrderid(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue));
                    QuotationId = supplierQuotationController.SaveQuatation(int.Parse(ddlPRItems.SelectedValue), int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlSupplier.SelectedValue), decimal.Parse(PerItemAmount.ToString()), 0, "", VatAmout, NbtAmount, SubTotal, 0, "", 0, Conditions, biddingObj.BiddingOrderId,chkVatNbt.Checked?1:0);

                    if(QuotationId > 0)
                    {
                        int saveBidHistory = bidHistoryController.SaveBidHistory(QuotationId, "A",int.Parse( UserId), PerItemAmount, VatAmout, NbtAmount, SubTotal, DateTime.Now);
                    }
                    if (QuotationId > 0 || existingBidsQuotationNumber > 0)
                    {
                        for (int i = 0; i < gvBOM.Rows.Count; i++)
                        {
                            int prid = int.Parse(gvBOM.Rows[i].Cells[0].Text);
                            int itemid = int.Parse(gvBOM.Rows[i].Cells[1].Text);
                            int seq = int.Parse(gvBOM.Rows[i].Cells[2].Text);
                            string meterial = gvBOM.Rows[i].Cells[3].Text;
                            string description = gvBOM.Rows[i].Cells[4].Text;
                            RadioButton rbYes = (gvBOM.Rows[i].FindControl("RadioButtonYes")) as RadioButton;
                            RadioButton rbNo = (gvBOM.Rows[i].FindControl("RadioButtonNo")) as RadioButton;
                            int comply = 0;
                            if (rbYes.Checked == true)
                            {
                                comply = 1;
                            }
                            if (rbNo.Checked == true)
                            {
                                comply = 0;
                            }
                            TextBox tb = (gvBOM.Rows[i].FindControl("txtRemarks")) as TextBox;
                            string Remarks = tb.Text;

                            save = supplierBOMController.SaveSupplierBOM(int.Parse(ddlSupplier.SelectedValue), prid, itemid, seq, meterial, description, 1, DateTime.Now, comply, Remarks);
                        }
                        SaveFiles();
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("Bid has been submitted successfully", false);
                        clearDiv();
                        ddlProgressPR.Items.Clear();
                        ddlPRItems.Items.Clear();
                        ddlSupplier.SelectedIndex = 0;
                      
                    }
                    else
                    {
                        DisplayMessage("Error on Submit Bid", true);

                    }
                    
                }
                else
                {
                    
                   int updateBiddingStatus = supplierQuotationController.UpdatePendingBids(existingBidsQuotationNumber, PerItemAmount, VatAmout, NbtAmount, SubTotal, Conditions,chkVatNbt.Checked?1:0);
                    if (updateBiddingStatus > 0)
                    {
                        
                            int saveBidHistory = bidHistoryController.SaveBidHistory(existingBidsQuotationNumber, "A",int.Parse( UserId), PerItemAmount, VatAmout, NbtAmount, SubTotal, DateTime.Now);

                        for (int i = 0; i < gvBOM.Rows.Count; i++)
                        {
                            int prid = int.Parse(gvBOM.Rows[i].Cells[0].Text);
                            int itemid = int.Parse(gvBOM.Rows[i].Cells[1].Text);
                            int seq = int.Parse(gvBOM.Rows[i].Cells[2].Text);
                            string meterial = gvBOM.Rows[i].Cells[3].Text;
                            string description = gvBOM.Rows[i].Cells[4].Text;
                            RadioButton rbYes = (gvBOM.Rows[i].FindControl("RadioButtonYes")) as RadioButton;
                            RadioButton rbNo = (gvBOM.Rows[i].FindControl("RadioButtonNo")) as RadioButton;
                            int comply = 0;
                            if (rbYes.Checked == true)
                            {
                                comply = 1;
                            }
                            if (rbNo.Checked == true)
                            {
                                comply = 0;
                            }
                            TextBox tb = (gvBOM.Rows[i].FindControl("txtRemarks")) as TextBox;
                            string Remarks = tb.Text;

                            save = supplierBOMController.UpdateSupplierBOM(int.Parse(ddlSupplier.SelectedValue), prid, itemid, seq, meterial, description, comply, Remarks);
                        }

                        List<SupplierBiddingFileUpload> supplierBidding = supplierBiddingFileUploadController.GetFilesByQuotationId(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue), int.Parse(ddlSupplier.SelectedValue)).ToList();
                        
                        int maxNumber = 0;

                        foreach (var item in supplierBidding)
                        {
                            string CalNumber = item.FileName.Split('.').First().Split('/').Last().Split('/').Last().Split('_').Last();
                            if (int.Parse(CalNumber) > maxNumber)
                                maxNumber = int.Parse(CalNumber);
                        }

                        SupplierQuotation supplierQuo = supplierQuotationController.GetGivenQuotatios(int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue), int.Parse(ddlSupplier.SelectedValue));
                        string folderFilePath = string.Empty;
                        HttpFileCollection hfc = Request.Files;

                        for (int i = 0; i <= hfc.Count - 1; i++)
                        {
                            HttpPostedFile hpf = hfc[i];
                            string CreateFileName = supplierQuo.QuotationNo + "_" + ddlSupplier.SelectedValue + "_" + ddlProgressPR.SelectedValue+ "_" + ddlPRItems.SelectedValue + "_" + (maxNumber + 1).ToString();
                            string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                            string FileName = Path.GetFileName(hpf.FileName);
                            string filename01 = UploadedFileName + "." + FileName.Split('.').Last();
                            

                            if (hpf.ContentLength > 0)
                            {
                                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/SupplierBiddingFileUpload/" + ddlSupplier.SelectedValue + "/" + ddlProgressPR.SelectedValue + "_" + ddlPRItems.SelectedValue + "/" + filename01)))
                                {
                                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/SupplierBiddingFileUpload/" + ddlSupplier.SelectedValue + "/" + ddlProgressPR.SelectedValue + "_" + ddlPRItems.SelectedValue + "/" + filename01));
                                }
                                hpf.SaveAs(HttpContext.Current.Server.MapPath("~/SupplierBiddingFileUpload/" + ddlSupplier.SelectedValue + "/" + ddlProgressPR.SelectedValue + "_" + ddlPRItems.SelectedValue + "/" + filename01));
                                folderFilePath = "~/SupplierBiddingFileUpload/" + ddlSupplier.SelectedValue + "/"  +ddlProgressPR.SelectedValue + "_" + ddlPRItems.SelectedValue + "/" + filename01;
                                int saveFilePath = supplierBiddingFileUploadController.SaveFiles(int.Parse(ddlSupplier.SelectedValue), QuotationId, int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue), folderFilePath, FileName);
                            }
                        }
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("Bid has been updated successfully", false);
                        clearDiv();
                        ddlProgressPR.Items.Clear();
                        ddlPRItems.Items.Clear();
                        ddlSupplier.SelectedIndex = 0;
                    }
                    else
                    {
                        DisplayMessage("Error on Update Bid", true);
                    }
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SaveFiles() {

            string folderFilePath = string.Empty;
            if (FileUpload1.HasFile)
            {
                //New Directory Name in string variable
                string NewDirectory = Server.MapPath("SupplierBiddingFileUpload/" + int.Parse(ddlSupplier.SelectedValue) + "/" + int.Parse(ddlProgressPR.SelectedValue) + "_" + int.Parse(ddlPRItems.SelectedValue));
                int returnType = CreateDirectoryIfNotExists(NewDirectory);
                //if (returnType == 1)
                //{
                    HttpFileCollection hfc = Request.Files;
                    if (hfc.Count <= 10)    // 10 FILES RESTRICTION.
                    {
                        for (int i = 0; i <= hfc.Count - 1; i++)
                        {
                            HttpPostedFile hpf = hfc[i];
                            string CreateFileName = QuotationId + "_" + int.Parse(ddlSupplier.SelectedValue) + "_" + int.Parse(ddlProgressPR.SelectedValue) + "_" + int.Parse(ddlPRItems.SelectedValue) + "_" + (i + 1).ToString();
                            string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                            string FileName = Path.GetFileName(hpf.FileName);
                            string filename01 = UploadedFileName + "." + FileName.Split('.').Last();
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/SupplierBiddingFileUpload/" + int.Parse(ddlSupplier.SelectedValue) + "/" + (int.Parse(ddlProgressPR.SelectedValue) + "_" + int.Parse(ddlPRItems.SelectedValue)) + "/" + filename01)))
                            {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/SupplierBiddingFileUpload/" + int.Parse(ddlSupplier.SelectedValue) + "/" + (int.Parse(ddlProgressPR.SelectedValue) + "_" + int.Parse(ddlPRItems.SelectedValue)) + "/" + filename01));
                            }
                            hpf.SaveAs(HttpContext.Current.Server.MapPath("~/SupplierBiddingFileUpload/" + int.Parse(ddlSupplier.SelectedValue) + "/" + (int.Parse(ddlProgressPR.SelectedValue) + "_" + int.Parse(ddlPRItems.SelectedValue)) + "/" + filename01));
                            folderFilePath = "~/SupplierBiddingFileUpload/" + int.Parse(ddlSupplier.SelectedValue) + "/" + (int.Parse(ddlProgressPR.SelectedValue) + "_" + int.Parse(ddlPRItems.SelectedValue)) + "/" + filename01;
                            int saveFilePath = supplierBiddingFileUploadController.SaveFiles(int.Parse(ddlSupplier.SelectedValue), QuotationId, int.Parse(ddlProgressPR.SelectedValue), int.Parse(ddlPRItems.SelectedValue), folderFilePath, FileName);
                        }
                    }
               // }
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

        public string getJsonItemQuontity()
        {
            var Quontity = itemQuontity;
            return (new JavaScriptSerializer()).Serialize(Quontity);
        }

        public string getJsonEndDateTime()
        {
            var EndDate = EndTime;
            return (new JavaScriptSerializer()).Serialize(EndDate);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                clearDiv();
                ddlProgressPR.Items.Clear();
                ddlPRItems.Items.Clear();
                ddlSupplier.SelectedIndex = 0;

            }
            catch (Exception)
            {

            }
        }

        private void clearDiv()
        {
            divItemDetails.Visible = false;
            txtNBT.Text = "";
            txtSubTotal.Text = "";
            txtTermsConditions.Text = "";
            txtTotalPrice.Text = "";
            txtUnitPrice.Text = "";
            txtVAT.Text = "";

            gvUploadFiles.DataSource = null;
            gvUploadFiles.DataBind();

            gvUserDocuments.DataSource = null;
            gvUserDocuments.DataBind();
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
    }
}