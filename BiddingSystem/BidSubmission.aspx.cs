using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Web.Script.Serialization;
using System.IO;

namespace BiddingSystem
{
    public partial class BidSubmission : System.Web.UI.Page
    {
        string PrId = string.Empty;
        string ItemId = string.Empty;
        string QueryStringVal = string.Empty;
        string QueryStringAlreadyBidded = string.Empty;
        string Status = string.Empty;
        int supplierId = 0;
        public string EndTime = string.Empty;
        public string BidTermsAndConditions = string.Empty;
        public decimal itemQuontity = 0;
        int QuotationId = 0;
        public string ImagePath = string.Empty;
        public string alert = string.Empty;
        public string BidOrderId = string.Empty;
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        SupplierBOMController supplierBOMController = ControllerFactory.CreatesupplierBOMController();
        SupplierBiddingFileUploadController supplierBiddingFileUploadController = ControllerFactory.CreateSupplierBiddingFileUploadController();
        BidHistoryController bidHistoryController = ControllerFactory.CreateBidHistoryController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["supplierId"] != null && Session["supplierId"].ToString() != "")
            {
                supplierId = int.Parse(Session["supplierId"].ToString());
            }
            else
            {
                Response.Redirect("LoginPageSupplier.aspx");
            }

            QueryStringVal = Request.QueryString.Get("Info");
            QueryStringAlreadyBidded = Request.QueryString.Get("Status");
            if (QueryStringAlreadyBidded != "")
            {
                Status = QueryStringAlreadyBidded;
            }

            if (QueryStringVal != "")
            {
                string[] value = QueryStringVal.Split('_');
                PrId = value[0];
                ItemId = value[1];
                BidOrderId = value[2];
            }

            if(!IsPostBack){
                try
                {
                    if (Status != "A")
                    {
                         Bidding _biddingList = biddingController.GetBiddingDetails(int.Parse(PrId), int.Parse(ItemId));
                         lblCompanyName.Text = _biddingList.DepartmentName;
                         lblItemName.Text = _biddingList.ItemName;
                         lblQuontity.Text = _biddingList.ItemQuantity.ToString();
                         EndTime = _biddingList.EndDate.ToString();
                         ImagePath = _biddingList.ImagePath;
                         BidTermsAndConditions = _biddingList.BidTermsAndConditions;
                         itemQuontity = _biddingList.ItemQuantity;
                         List<PR_BillOfMeterial> pr_BillOfMeterial = pr_BillOfMeterialController.GetList(int.Parse(PrId), int.Parse(ItemId));
                         gvBOM.DataSource = pr_BillOfMeterial;
                         gvBOM.DataBind();
                    }
                    if (Status == "A")
                    {
                        btnSubmit.Text = "Update";
                        Bidding _biddingList = biddingController.GetBiddingDetails(int.Parse(PrId), int.Parse(ItemId));
                        lblCompanyName.Text = _biddingList.DepartmentName;
                        lblItemName.Text = _biddingList.ItemName;
                        lblQuontity.Text = _biddingList.ItemQuantity.ToString();
                        EndTime = _biddingList.EndDate.ToString();
                        ImagePath = _biddingList.ImagePath;
                        BidTermsAndConditions = _biddingList.BidTermsAndConditions;
                        itemQuontity = _biddingList.ItemQuantity;
                        List<SupplierQuotation> _supplierQuotationAlreadyBid = supplierQuotationController.GetAlreadyBidCountOfSupplier(int.Parse(PrId), int.Parse(ItemId), supplierId).ToList();
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
                            SupplierBiddingFileUploadController supplierBiddingFileUploadController = ControllerFactory.CreateSupplierBiddingFileUploadController();
                            gvUserDocuments.DataSource = supplierBiddingFileUploadController.GetFilesByQuotationId(int.Parse(PrId), int.Parse(ItemId),supplierId);
                            gvUserDocuments.DataBind();
                        }

                        List<PR_BillOfMeterial> pr_BillOfMeterial = pr_BillOfMeterialController.GetListWithSupplierBOM(int.Parse(PrId), int.Parse(ItemId));
                        gvBOM.DataSource = pr_BillOfMeterial;
                        gvBOM.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else{
            
            }
        }

        public string getJsonItemImagePath()
        {
            var Image = ImagePath;
            return (new JavaScriptSerializer()).Serialize(Image);
        }

        public string getJsonEndDateTime()
        {
            var EndDate = EndTime;
            return (new JavaScriptSerializer()).Serialize(EndDate);
        }

        public string getJsonBidTerms()
        {
            var BidTerms = BidTermsAndConditions;
            return (new JavaScriptSerializer()).Serialize(BidTerms);
        }

        public string getJsonItemQuontity()
        {
            var Quontity = itemQuontity;
            return (new JavaScriptSerializer()).Serialize(Quontity);
        }

        public string getJsonSuccessAlert()
        {
            var Alert = alert;
            return (new JavaScriptSerializer()).Serialize(Alert);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                txtTotalPrice.Attributes.Add("readonly", "readonly");
                decimal PerItemAmount = decimal.Parse(txtUnitPrice.Text);
                decimal TotalAmount = decimal.Parse(txtTotalPrice.Text);
                decimal VatAmout = decimal.Parse(txtVAT.Text==""?"0": txtVAT.Text);
                decimal NbtAmount = decimal.Parse(txtNBT.Text==""?"0": txtNBT.Text);
                decimal SubTotal = decimal.Parse(txtSubTotal.Text);
                string Conditions = txtTermsConditions.Text;
                int save = 0;
                int existingBidsQuotationNumber = biddingController.GetBiddingDetailsExisting(int.Parse(PrId), int.Parse(ItemId), supplierId).QuotationNo;
                if (existingBidsQuotationNumber == 0)
                {
                    QuotationId = supplierQuotationController.SaveQuatation(int.Parse(ItemId), int.Parse(PrId), supplierId, int.Parse(PerItemAmount.ToString()), 0, "", VatAmout, NbtAmount, SubTotal, 0, "", 0, Conditions, BidOrderId, chkVatNbt.Checked?1:0);
                    if(QuotationId > 0)
                    {
                        int saveBidHistory = bidHistoryController.SaveBidHistory(QuotationId, "S", supplierId, PerItemAmount, VatAmout, NbtAmount, SubTotal, DateTime.Now);
                    }
                }
                else
                {
                    int updateQuotationId=supplierQuotationController.UpdatePendingBids(existingBidsQuotationNumber, PerItemAmount, VatAmout, NbtAmount, SubTotal, Conditions,chkVatNbt.Checked?1:0);
                    if(updateQuotationId > 0)
                    {
                        int saveBidHistory1 = bidHistoryController.SaveBidHistory(updateQuotationId, "S", supplierId, PerItemAmount, VatAmout, NbtAmount, SubTotal, DateTime.Now);
                    }
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

                        save = supplierBOMController.SaveSupplierBOM(supplierId, prid, itemid, seq, meterial, description, 1, DateTime.Now, comply, Remarks);
                    }
                }
                if(save > 0){
                string folderFilePath = string.Empty;
                if (FileUpload1.HasFile)
                {
                    //New Directory Name in string variable
                    string NewDirectory = Server.MapPath("SupplierBiddingFileUpload/"+supplierId+"/"+ PrId+"_"+ItemId);
                    int returnType = CreateDirectoryIfNotExists(NewDirectory);
                    if (returnType == 1)
                    {
                        HttpFileCollection hfc = Request.Files;
                        if (hfc.Count <= 10)    // 10 FILES RESTRICTION.
                        {
                            for (int i = 0; i <= hfc.Count - 1; i++)
                            {
                                HttpPostedFile hpf = hfc[i];
                                string CreateFileName = QuotationId + "_" + supplierId + "_" + PrId + "_" + ItemId + "_" + (i + 1).ToString();
                                string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                string FileName = Path.GetFileName(hpf.FileName);
                                string filename01 = UploadedFileName + "." + FileName.Split('.').Last();
                                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/SupplierBiddingFileUpload/" + supplierId + "/"+(PrId+"_"+ItemId)+"/"+ filename01)))
                                {
                                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/SupplierBiddingFileUpload/" + supplierId + "/" + (PrId + "_" + ItemId) + "/" + filename01));
                                }
                                hpf.SaveAs(HttpContext.Current.Server.MapPath("~/SupplierBiddingFileUpload/" + supplierId + "/" + (PrId + "_" + ItemId) + "/" + filename01));
                                folderFilePath = "~/SupplierBiddingFileUpload/" + supplierId + "/" + (PrId + "_" + ItemId) + "/" + filename01;
                                int saveFilePath = supplierBiddingFileUploadController.SaveFiles(supplierId, QuotationId, int.Parse(PrId), int.Parse(ItemId), folderFilePath, FileName);
                            }
                        }
                    }
                }
              }
                Bidding _biddingList1 = biddingController.GetBiddingDetails(int.Parse(PrId), int.Parse(ItemId));
                EndTime = _biddingList1.EndDate.ToString();
                ImagePath = _biddingList1.ImagePath;
                //getJsonSuccessAlert("");
                alert = "Bid has been submitted Successfully";
            }
            catch (Exception ex)
            {
                DisplayMessage("Error on Bid Submission", true);
            }

            Bidding _biddingList = biddingController.GetBiddingDetails(int.Parse(PrId), int.Parse(ItemId));
            EndTime = _biddingList.EndDate.ToString();
            ImagePath = _biddingList.ImagePath;
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


        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupplierIndex.aspx");
        }

        protected void gvUserDocuments_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvUserDocuments.DataSource = supplierBiddingFileUploadController.GetFilesByQuotationId(int.Parse(PrId), int.Parse(ItemId), supplierId);
                gvUserDocuments.PageIndex = e.NewPageIndex;
                gvUserDocuments.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
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

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvUserDocuments.Rows[x].Cells[4].Text;
                int deleteStatus = supplierBiddingFileUploadController.DeleteFileUploads(int.Parse(PrId), int.Parse(ItemId), supplierId, filepath);
                if (deleteStatus > 0)
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                }
                else
                {
                }
                gvUserDocuments.DataSource = supplierBiddingFileUploadController.GetFilesByQuotationId(int.Parse(PrId), int.Parse(ItemId), supplierId);
                gvUserDocuments.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}