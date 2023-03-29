using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace BiddingSystem
{
    public partial class CompanyGrnReportView : System.Web.UI.Page
    {
        GrnController grnController = ControllerFactory.CreateGrnController();
        GRNDetailsController gRNDetailsController = ControllerFactory.CreateGRNDetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        GrnFilesController grnFilesController = ControllerFactory.CreateGrnFilesController();
        InvoiceDetailsController invoiceDetailsController = ControllerFactory.CreateInvoiceDetailsController();


        protected void Page_Load(object sender, EventArgs e)
        {
            int CompanyId, GrnID = 0, PoID;
            string UserId;

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();

                if (Request.QueryString["PoID"] != null && Request.QueryString["GrnId"] != null)
                {
                    GrnID = int.Parse(Request.QueryString["GrnId"].ToString());
                    PoID = int.Parse(Request.QueryString["PoID"].ToString());
                }
                else
                {
                    Response.Redirect("CompanyGrnReports.aspx");
                }

            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

            //   lblDateNow.Text = LocalTime.Today.ToString("dd-MM-yyyy");


            if (!IsPostBack)
            {
                try
                {

                    //GrnMaster grnMaster = grnController.GetGrnMasterByGrnID(GrnID);
                    //CompanyDepartment companyDepartment = companyDepartmentController.GetDepartmentByDepartmentId(CompanyId);
                    //lblPOCode.Text = grnMaster._POMaster.POCode;
                    //lblSupplierName.Text = grnMaster._Supplier.SupplierName; ;
                    //lblAddress.Text = grnMaster._Supplier.Address1 + "," + grnMaster._Supplier.Address2;

                    //lblCompanyName.Text = companyDepartment.DepartmentName;
                    //lblVatNo.Text = companyDepartment.VatNo;
                    //lblPhoneNo.Text = companyDepartment.PhoneNO;
                    //lblFaxNo.Text = companyDepartment.FaxNO;
                    //lblCompanyAddress.Text = companyDepartment.Address1.ToString();
                    //lblCity.Text = companyDepartment.City;
                    //lblCountry.Text = companyDepartment.Country + ".";
                    GrnMaster grnMaster = grnController.GetGrnMasterByGrnID(GrnID);
                    int poId = grnMaster._POMaster.PoID;
                    int paymentMethod = int.Parse(grnMaster._POMaster.PaymentMethod);
                    ViewState["POId"] = poId;
                    ViewState["PaymentMethod"] = paymentMethod;

                    lblsupplierName.Text = grnMaster._Supplier.SupplierName;
                    lblSupplierAddress.Text = grnMaster._Supplier.Address1;
                    lblSupplierContact.Text = grnMaster._Supplier.OfficeContactNo + " / " + grnMaster._Supplier.PhoneNo;

                    lblWarehouseName.Text = grnMaster._Warehouse.Location;
                    lblWarehouseAddress.Text = grnMaster._Warehouse.Address;
                    lblWarehouseContact.Text = grnMaster._Warehouse.PhoneNo;
                    lblStoreKeeper.Text = grnMaster._POMaster.StoreKeeper;

                    lblGrnCode.Text = grnMaster.GrnCode;
                    lblPOCode.Text = grnMaster.POCode;
                    lblPrCode.Text = "PR-" + grnMaster.PrCode;
                    lblReceiveddate.Text = grnMaster.GoodReceivedDate.ToString("yyyy-MM-dd");
                    lblgrnComment.Text = grnMaster.ApprovalRemaks;
                    lblgrnNote.Text = grnMaster.GrnNote;
                   // lblQuotationfor.Text = grnMaster.QuotationFor;
                    lblPurchaseType.Text = grnMaster._POMaster.PurchaseType == 1 ? "Local" : "Import";
                    //decimal subTotal = grnMaster.TotalAmount - (grnMaster.TotalVat + grnMaster.TotalNbt);
                    decimal subTotal = grnMaster.TotalAmount - (grnMaster.TotalVat);
                    // lblSubtotal.Text = grnMaster.TotalAmount.ToString("n");
                    lblSubtotal.Text = subTotal.ToString("n");
                    lblVatTotal.Text = grnMaster.TotalVat.ToString("n");
                    //lblNbtTotal.Text = grnMaster.TotalNbt.ToString("n");
                    lblTotal.Text = grnMaster.TotalAmount.ToString("n");


                    lblApprovedByName.Text = grnMaster.ApprovedByName;
                    lblCreatedByName.Text = grnMaster.CreatedByName;
                    lblApprovedDate.Text = grnMaster.ApprovedDate.ToString("dd/MM/yyyy");
                    lblCreatedDate.Text = grnMaster.CreatedDate.ToString("dd/MM/yyyy");
                    lblPaymentm.Text = grnMaster._POMaster.PaymentMethod == "1" ? "Cash" : grnMaster._POMaster.PaymentMethod == "2" ? "Cheque" : grnMaster._POMaster.PaymentMethod == "3" ? "Credit" : grnMaster._POMaster.PaymentMethod == "4" ? "Advanced Payment" : "-";
                    if (grnMaster._POMaster.PurchaseType == 2) {
                        //gvPurchaseOrderItems.Columns[11].Visible = false;
                        PnlWarehouse.Visible = false;
                    }

                    if (grnMaster.IsApproved == 0)
                    {
                        lblPending.Visible = true;
                    }
                    else if (grnMaster.IsApproved == 1)
                    {
                        lblApproved.Visible = true;
                        grnApprovalText.InnerHtml = "GRN Approved By";
                    }
                    else
                    {
                        lblRejected.Visible = true;
                        grnApprovalText.InnerHtml = "GRN Rejected By";
                    }

                    //  gvPurchaseOrderItems.DataSource = grnMaster._POMaster._PODetails;
                    gvPurchaseOrderItems.DataSource = grnMaster._GrnDetailsList;
                    // lblgrnComment.InnerText = grnMaster.GrnNote;
                    // lblReceiveddate.InnerText = grnMaster.GoodReceivedDate.ToString("yyyy-MM-dd");
                    gvPurchaseOrderItems.DataBind();

                    if (File.Exists(HttpContext.Current.Server.MapPath(grnMaster.ApprovedSignature)))
                        imgApprovedBySignature.ImageUrl = grnMaster.ApprovedSignature;
                    else
                        imgApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";


                    if (File.Exists(HttpContext.Current.Server.MapPath(grnMaster.CreatedSignature)))
                        imgCreatedBySignature.ImageUrl = grnMaster.CreatedSignature;
                    else
                        imgCreatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";

                    if (grnMaster.IsApproved != 0)
                    {
                        pnlApprovedBy.Visible = true;
                        pnlRemark.Visible = true;
                    }
                    else
                    {
                        pnlApprovedBy.Visible = false;
                        pnlRemark.Visible = false;
                    }

                    if (grnMaster.IsApproved == 1) {
                        btnInvoice.Visible = true;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        protected void btnEdit_Click(object sender, EventArgs e) {
            try {
                Response.Redirect("ViewInvoices.aspx");
            }
            catch (Exception EX) {

            }
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                gvFiles.DataSource = grnFilesController.GetGrnFilesByGrnId(int.Parse(Request.QueryString["GrnId"].ToString()));
                gvFiles.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlGrnFiles').modal('show'); });   </script>", false);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnInvoice_Click(object sender, EventArgs e) {
            try {

                ddlPaymentMethod.SelectedValue = ViewState["PaymentMethod"].ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlInvDetails').modal('show'); });   </script>", false);

            }
            catch (Exception ex) {
                throw ex;
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e) {
            try {
                Random r = new Random();
                if (ViewState["InvoiceDetails"] == null) {
                    List<InvoiceDetails> invoiceDetails = new List<InvoiceDetails>();

                    InvoiceDetails newDetail = new InvoiceDetails();

                    newDetail.num = r.Next();
                    newDetail.InvoiceNo = txtInvNo.Text;
                    newDetail.InvoiceDate = DateTime.Parse(txtDate.Text);
                    newDetail.InvoiceAmount = decimal.Parse(txtAmount.Text);
                    newDetail.IsPaymentSettled = ChkPayment.Checked == true ? 1 : 0;
                    newDetail.VatNo = txtVatNo.Text;
                    newDetail.PaymentType = int.Parse(ddlPaymentMethod.SelectedValue);
                    newDetail.Remark = txtremark.Text;
                    newDetail.RemarkOn = DateTime.Parse(txtNewDate.Text);

                    invoiceDetails.Add(newDetail);
                    ViewState["InvoiceDetails"] = new JavaScriptSerializer().Serialize(invoiceDetails);
                }
                else {
                    List<InvoiceDetails> invoiceDetails = new JavaScriptSerializer().Deserialize<List<InvoiceDetails>>(ViewState["InvoiceDetails"].ToString());
                    InvoiceDetails newDetail = new InvoiceDetails();

                    newDetail.num = r.Next();
                    newDetail.InvoiceNo = txtInvNo.Text;
                    newDetail.InvoiceDate = DateTime.Parse(txtDate.Text);
                    newDetail.InvoiceAmount = decimal.Parse(txtAmount.Text);
                    newDetail.IsPaymentSettled = ChkPayment.Checked == true ? 1 : 0;
                    newDetail.VatNo = txtVatNo.Text;
                    newDetail.PaymentType = int.Parse(ddlPaymentMethod.SelectedValue);
                    newDetail.Remark = txtremark.Text;
                    newDetail.RemarkOn = DateTime.Parse(txtNewDate.Text);

                    invoiceDetails.Add(newDetail);
                    ViewState["InvoiceDetails"] = new JavaScriptSerializer().Serialize(invoiceDetails);
                }

                gvAddedInvDetails.Visible = true;
                btnDone.Visible = true;

                List<InvoiceDetails> invoiceDetailsList = new JavaScriptSerializer().Deserialize<List<InvoiceDetails>>(ViewState["InvoiceDetails"].ToString());
                for (int i = 0; i < invoiceDetailsList.Count; i++) {
                    invoiceDetailsList[i].RemarkOn = invoiceDetailsList[i].RemarkOn.AddMinutes(330);
                    invoiceDetailsList[i].InvoiceDate = invoiceDetailsList[i].InvoiceDate.AddMinutes(330);
                }

                gvAddedInvDetails.DataSource = invoiceDetailsList;
                gvAddedInvDetails.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {$('div').removeClass('modal-backdrop'); $('#mdlInvDetails').modal('show'); });   </script>", false);

                txtInvNo.Text = "";
                txtDate.Text = "";
                txtAmount.Text = "";
                txtVatNo.Text = "";
                ChkPayment.Checked = false;
            }
            catch (Exception ex) {

            }

        }

        protected void btnDelete_Click(object sender, EventArgs e) {
            List<InvoiceDetails> invoiceDetails = new JavaScriptSerializer().Deserialize<List<InvoiceDetails>>(ViewState["InvoiceDetails"].ToString());
            int Rnum = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[0].Text);

            for (int i = 0; i < invoiceDetails.Count; i++) {
                InvoiceDetails DInvoiceDetails = invoiceDetails[i];
                if (invoiceDetails[i].num == Rnum) {
                    invoiceDetails[i].status = 2;
                    invoiceDetails.Remove(DInvoiceDetails);
                }

            }


            gvAddedInvDetails.DataSource = invoiceDetails.Where(x => x.status != 2);
            gvAddedInvDetails.DataBind();
            ViewState["InvoiceDetails"] = new JavaScriptSerializer().Serialize(invoiceDetails);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {$('div').removeClass('modal-backdrop'); $('#mdlInvDetails').modal('show'); });   </script>", false);

        }


        protected void btnDone_Click(object sender, EventArgs e) {
            //string InvoiceNo = txtInvNo.Text;
            //DateTime Date = DateTime.Parse(txtDate.Text);
            //decimal Amount = decimal.Parse(txtAmount.Text);
            //string vatNo = txtVatNo.Text;
            //int IsPayementSettled = ChkPayment.Checked == true ? 1 : 0;

            IList<HttpPostedFile> images = fileImages.PostedFiles;
            List<InvoiceImages> invoiceImages = new List<InvoiceImages>();
            for (int i = 0; i < images.Count; i++) {
                if (images[i].ContentLength > 0) {
                    string filePath = "/InvoiceImages/" + i + "_" + LocalTime.Now.Ticks + "_" + images[i].FileName;
                    images[i].SaveAs(HttpContext.Current.Server.MapPath(filePath));
                    InvoiceImages image = new InvoiceImages() {
                        ImagePath = "~" + filePath
                    };
                    invoiceImages.Add(image);
                }
            }

            List<InvoiceDetails> invoiceDetails = new JavaScriptSerializer().Deserialize<List<InvoiceDetails>>(ViewState["InvoiceDetails"].ToString());
            for (int i = 0; i < invoiceDetails.Count; i++) {
                invoiceDetails[i].RemarkOn = invoiceDetails[i].RemarkOn.AddMinutes(330);
                invoiceDetails[i].InvoiceDate = invoiceDetails[i].InvoiceDate.AddMinutes(330);
            }

            int paymentType = int.Parse(ddlPaymentMethod.SelectedValue);
            int GrnId = int.Parse(Request.QueryString.Get("GrnId"));
            int PoId = int.Parse(ViewState["POId"].ToString());

            int Result = 0;
            for (int i = 0; i < invoiceDetails.Count; i++) {
                
                Result = invoiceDetailsController.SaveInvoiceDetailsInPO(PoId, GrnId, invoiceDetails[i].PaymentType, invoiceDetails[i].InvoiceNo, invoiceDetails[i].InvoiceDate, invoiceDetails[i].InvoiceAmount, invoiceDetails[i].VatNo, invoiceDetails[i].IsPaymentSettled, invoiceDetails[i].Remark, invoiceDetails[i].RemarkOn, invoiceImages); 
            }
            if (Result > 0) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('div').removeClass('modal-backdrop'); $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved'}).then((result) => { window.location = '" + Request.QueryString.Get("ViewApprovePO.aspx") + "' }); });   </script>", false);
                gvAddedInvDetails.DataSource = null;
                gvAddedInvDetails.DataBind();

                ViewState["InvoiceDetails"] = null;
                btnDone.Visible = false;
            }


        }

        protected void btnPrevInv_Click(object sender, EventArgs e) {
            try {
                gvPrevInvoices.DataSource = invoiceDetailsController.GetPreviousInvoices(int.Parse(ViewState["POId"].ToString()));
                gvPrevInvoices.DataBind();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>   $('div').removeClass('modal-backdrop'); $(document).ready(function () { $('#mdlPrevInvoices').modal('show'); });   </script>", false);
            }
            catch (Exception EX) {

            }
        }

        protected void btnBack_Click(object sender, EventArgs e) {
            try {
               
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('div').removeClass('modal-backdrop'); $('#mdlPrevInvoices').modal('hide'); $('#mdlInvDetails').modal('show'); });   </script>", false);
                gvPrevInvoices.DataSource = null;
                gvPrevInvoices.DataBind();
            }
            catch (Exception EX) {

            }
        }
    }
}