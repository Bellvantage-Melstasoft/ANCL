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
    public partial class GenerateGRNNewImport : System.Web.UI.Page {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        GrnController GrnController = ControllerFactory.CreateGrnController();
        POMasterController poMasterController = ControllerFactory.CreatePOMasterController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        InvoiceDetailsController invoiceDetailsController = ControllerFactory.CreateInvoiceDetailsController();


        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                //((BiddingAdmin)Page.Master).subTabValue = "CustomerViewApprovedPurchaseOrder.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "generateGRNLink";

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 9) && companyLogin.Usertype != "S") {
                    Response.Redirect("AdminDashboard.aspx");
                }

                if (Request.QueryString.Get("PoId") == null) {
                    Response.Redirect("CustomerViewApprovedPurchaseOrder.aspx");
                }

            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                Populate();

                VAT_NBT LatestVatNbt = generalSettingsController.getLatestVatNbt();
                var vatValue = LatestVatNbt.VatRate;
                var nbtVal1 = LatestVatNbt.NBTRate1;
                var nbtVal2 = LatestVatNbt.NBTRate2;

                hdnVatRate.Value = vatValue.ToString();
                hdnNbtRate1.Value = nbtVal1.ToString();
                hdnNbtRate2.Value = nbtVal2.ToString();
            }
            else {
                if (hdnSubTotal.Value != "" || hdnSubTotal.Value != null) {
                    decimal sub = decimal.Parse(hdnSubTotal.Value);
                    decimal vat = decimal.Parse(hdnVat.Value);
                    decimal net = decimal.Parse(hdnNetTotal.Value);

                    tdSubTotal.InnerHtml = sub.ToString("N2");
                    tdVat.InnerHtml = vat.ToString("N2");
                    tdNetTotal.InnerHtml = net.ToString("N2");
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
                //btnDone.Visible = true;

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
                txtremark.Text = "";
                txtNewDate.Text = "";
            }
            catch (Exception ex) {

            }

        }


        private void Populate() {
            VAT_NBT LatestVatNbt = generalSettingsController.getLatestVatNbt();
            var vatValue = LatestVatNbt.VatRate;
            var nbtVal1 = LatestVatNbt.NBTRate1;
            var nbtVal2 = LatestVatNbt.NBTRate2;

            POMaster poMaster = ControllerFactory.CreatePOMasterController().GetPoMasterToGenerateGRN(int.Parse(Request.QueryString.Get("PoId")), int.Parse(Session["CompanyId"].ToString()), nbtVal1, nbtVal2, vatValue);

            lblDate.Text = LocalTime.Now.ToString("yyyy/MM/dd");
            lblPOCode.Text = poMaster.POCode;
            lblPrCode.Text = "PR-" + poMaster.PrCode;
            ViewState["SupplierId"] = poMaster._Supplier.SupplierId;
            ViewState["PoApprovedBy"] = poMaster.ApprovedBy;
            ViewState["BasedPr"] = poMaster.BasePr;
            ViewState["WarehouseId"] = poMaster.DeliverToWarehouse;
            ViewState["PaymentMethod"] = poMaster.PaymentMethod;
            lblsupplierName.Text = poMaster._Supplier.SupplierName;
            lblSupplierAddress.Text = poMaster._Supplier.Address1;
            lblSupplierContact.Text = poMaster._Supplier.OfficeContactNo + " / " + poMaster._Supplier.PhoneNo;
           // lblQuotationFor.Text = poMaster.QuotationFor;
            lblPaymenttype.Text = poMaster.PaymentMethod == "1" ? "Cash" : poMaster.PaymentMethod == "2" ? "Cheque" : poMaster.PaymentMethod == "3" ? "Credit" : poMaster.PaymentMethod == "4" ? "Advanced Payment" : "-";
            ViewState["PaymentMethod"] = poMaster.PaymentMethod;

            if (poMaster.PurchaseType == 2) {
                //gvPoItems.Columns[17].Visible = false;
                pnlwarehouse.Visible = false;
            }

            lblPurchaseType.Text = poMaster.PurchaseType == 1 ? "Local" : "Import";

            for (int i = 0; i < poMaster.PoDetails.Count; i++) {
                poMaster.PoDetails[i].VatAmountDisplay = Math.Round((poMaster.PoDetails[i].VatAmount),2);
            }
            gvPoItems.DataSource = poMaster.PoDetails;
            gvPoItems.DataBind();



            tdSubTotal.InnerHtml = poMaster.SubTotal.ToString("N2");
            //tdNbt.InnerHtml = poMaster.NBTAmount.ToString("N2");
            tdVat.InnerHtml = poMaster.VatAmount.ToString("N2");
            tdNetTotal.InnerHtml = poMaster.TotalAmount.ToString("N2");

            hdnSubTotal.Value = poMaster.SubTotal.ToString("N2");
            hdnNbt.Value = poMaster.NBTAmount.ToString("N2");
            hdnVat.Value = poMaster.VatAmount.ToString("N2");
            hdnNetTotal.Value = poMaster.TotalAmount.ToString("N2");

            lblWarehouseName.Text = poMaster.Warehouse.Location;
            lblWarehouseAddress.Text = poMaster.Warehouse.Address;
            lblWarehouseContact.Text = poMaster.Warehouse.PhoneNo;
            lblStoreKeeper.Text = poMaster.StoreKeeper;
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
            ViewState["invoiceImages"] = new JavaScriptSerializer().Serialize(invoiceImages);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('div').removeClass('modal-backdrop'); $('#mdlInvDetails').modal('hide'); });   </script>", false);


            //    List<InvoiceDetails> invoiceDetails = new JavaScriptSerializer().Deserialize<List<InvoiceDetails>>(ViewState["InvoiceDetails"].ToString());
            //    for (int i = 0; i < invoiceDetails.Count; i++) {
            //        invoiceDetails[i].RemarkOn = invoiceDetails[i].RemarkOn.AddMinutes(330);
            //        invoiceDetails[i].InvoiceDate = invoiceDetails[i].InvoiceDate.AddMinutes(330);
            //    }

            //    int paymentType = int.Parse(ddlPaymentMethod.SelectedValue);
            //    int GrnId = int.Parse(Request.QueryString.Get("GrnId"));
            //    int PoId = int.Parse(ViewState["POId"].ToString());

            //    int Result = 0;
            //    for (int i = 0; i < invoiceDetails.Count; i++) {

            //        Result = invoiceDetailsController.SaveInvoiceDetailsInPO(PoId, GrnId, invoiceDetails[i].PaymentType, invoiceDetails[i].InvoiceNo, invoiceDetails[i].InvoiceDate, invoiceDetails[i].InvoiceAmount, invoiceDetails[i].VatNo, invoiceDetails[i].IsPaymentSettled, invoiceDetails[i].Remark, invoiceDetails[i].RemarkOn);
            //    }
            //    if (Result > 0) {
            //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('div').removeClass('modal-backdrop'); $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved'}).then((result) => { window.location = '" + Request.QueryString.Get("ViewApprovePO.aspx") + "' }); });   </script>", false);
            //        //gvAddedInvDetails.DataSource = null;
            //        //gvAddedInvDetails.DataBind();

            //        //ViewState["InvoiceDetails"] = null;
            //        //btnDone.Visible = false;
            //    }


        }


        protected void btnGenerate_Click(object sender, EventArgs e) {
            try {
                VAT_NBT LatestVatNbt = generalSettingsController.getLatestVatNbt();
                var vatValue = LatestVatNbt.VatRate;
                var nbtVal1 = LatestVatNbt.NBTRate1;
                var nbtVal2 = LatestVatNbt.NBTRate2;

                List<InvoiceImages> invoiceImages = new List<InvoiceImages>();
                if (ViewState["invoiceImages"] != null) {
                    invoiceImages = new JavaScriptSerializer().Deserialize<List<InvoiceImages>>(ViewState["invoiceImages"].ToString());
                }
                IList<HttpPostedFile> docs = fuDocs.PostedFiles;
                List<PODetails> CoveringPoDetails = new List<PODetails>();

                GrnMaster grnMaster = new GrnMaster {
                    TotalAmount = decimal.Parse(hdnNetTotal.Value),
                    TotalNbt = decimal.Parse(hdnNbt.Value),
                    TotalVat = hdnVat.Value ==""?0: decimal.Parse(hdnVat.Value),
                    GoodReceivedDate = DateTime.Parse(txtReceivedDate.Text),
                    GrnNote = txtRemarks.Text,
                    CreatedBy = Session["UserId"].ToString(),
                    CreatedDate = LocalTime.Now,
                    CompanyId = int.Parse(Session["CompanyId"].ToString()),
                    Supplierid = int.Parse(ViewState["SupplierId"].ToString()),
                    WarehouseId = int.Parse(ViewState["WarehouseId"].ToString()),
                    PoIds = new List<int>()
                };
                grnMaster.PoIds.Add(int.Parse(Request.QueryString.Get("PoId")));

                grnMaster.GrnDetailsList = new List<GrnDetails>();

                for (int i = 0; i < gvPoItems.Rows.Count; i++) {
                   // bool IsChecked = (gvPoItems.Rows[i].Cells[0].FindControl("CheckBox1") as CheckBox).Checked;

                    if ((gvPoItems.Rows[i].Cells[6].FindControl("txtQuantity") as TextBox).Text != "" && decimal.Parse((gvPoItems.Rows[i].Cells[7].FindControl("txtQuantity") as TextBox).Text) > 0) {
                        GrnDetails grnDetail = new GrnDetails {
                            PodId = int.Parse(gvPoItems.Rows[i].Cells[1].Text),
                            ItemId = int.Parse(gvPoItems.Rows[i].Cells[2].Text),
                            ItemPrice = decimal.Parse(gvPoItems.Rows[i].Cells[22].Text),
                            Quantity = decimal.Parse((gvPoItems.Rows[i].Cells[8].FindControl("txtQuantity") as TextBox).Text),
                            NbtAmount = decimal.Parse((gvPoItems.Rows[i].Cells[15].FindControl("txtNbt") as TextBox).Text),
                            VatAmount = (gvPoItems.Rows[i].Cells[17].FindControl("txtVat") as TextBox).Text == "" ? 0: decimal.Parse((gvPoItems.Rows[i].Cells[17].FindControl("txtVat") as TextBox).Text),
                            TotalAmount = decimal.Parse((gvPoItems.Rows[i].Cells[18].FindControl("txtTotal") as TextBox).Text),
                            //FreeQty = decimal.Parse((gvPoItems.Rows[i].Cells[8].FindControl("txtFreeQuantity") as TextBox).Text),
                            FreeQty = !string.IsNullOrEmpty((gvPoItems.Rows[i].Cells[9].FindControl("txtFreeQuantity") as TextBox).Text) ? decimal.Parse((gvPoItems.Rows[i].Cells[8].FindControl("txtFreeQuantity") as TextBox).Text) : 0,
                            //ExpiryDate = DateTime.Parse((gvPoItems.Rows[i].Cells[9].FindControl("txtExpiryDate") as TextBox).Text)
                            ExpiryDate = !string.IsNullOrEmpty((gvPoItems.Rows[i].Cells[10].FindControl("txtExpiryDate") as TextBox).Text) ? DateTime.Parse((gvPoItems.Rows[i].Cells[9].FindControl("txtExpiryDate") as TextBox).Text) : DateTime.MinValue,
                            MeasurementId = int.Parse(gvPoItems.Rows[i].Cells[19].Text),
                            SupplierMentionedItemName = gvPoItems.Rows[i].Cells[4].Text
                            
                        };

                        if (grnDetail.Quantity > decimal.Parse(gvPoItems.Rows[i].Cells[7].Text)) {
                            grnDetail.WaitingQty = decimal.Parse(gvPoItems.Rows[i].Cells[7].Text);

                            PODetails poDetail = new PODetails {
                                PodId = int.Parse(gvPoItems.Rows[i].Cells[1].Text),
                                ItemId = int.Parse(gvPoItems.Rows[i].Cells[2].Text),
                                ItemPrice = decimal.Parse(gvPoItems.Rows[i].Cells[22].Text),
                                Quantity = grnDetail.Quantity - decimal.Parse(gvPoItems.Rows[i].Cells[7].Text),
                                MeasurementId = int.Parse(gvPoItems.Rows[i].Cells[19].Text),
                                SupplierMentionedItemName = gvPoItems.Rows[i].Cells[4].Text
                            };

                            if (gvPoItems.Rows[i].Cells[13].Text == "1") {

                                if (gvPoItems.Rows[i].Cells[14].Text == "1") {
                                    poDetail.NbtAmount = (poDetail.ItemPrice * poDetail.Quantity) * nbtVal1;
                                }
                                else {
                                    poDetail.NbtAmount = (poDetail.ItemPrice * poDetail.Quantity) * nbtVal2;
                                }

                            }

                            if (gvPoItems.Rows[i].Cells[16].Text == "1") {
                                //poDetail.VatAmount = ((poDetail.ItemPrice * poDetail.Quantity) + poDetail.NbtAmount) * vatValue;
                                poDetail.VatAmount = (poDetail.ItemPrice * poDetail.Quantity) * vatValue;
                            }

                            // poDetail.TotalAmount = (poDetail.ItemPrice * poDetail.Quantity) + poDetail.NbtAmount + poDetail.VatAmount
                            poDetail.TotalAmount = (poDetail.ItemPrice * poDetail.Quantity) + poDetail.VatAmount;

                            CoveringPoDetails.Add(poDetail);
                        }
                        else {
                            grnDetail.WaitingQty = grnDetail.Quantity;
                        }

                        grnMaster.GrnDetailsList.Add(grnDetail);
                    }
                }



                if (grnMaster.GrnDetailsList.Count > 0) {
                    if (hdnHasMore.Value == "1") {
                        POMaster poMaster = new POMaster {
                            SupplierId = int.Parse(ViewState["SupplierId"].ToString()),
                            CreatedBy = Session["UserId"].ToString(),
                            IsDerivedFromPo = int.Parse(Request.QueryString.Get("PoId")),
                            DepartmentId = int.Parse(Session["CompanyId"].ToString()),
                            NBTAmount = CoveringPoDetails.Sum(pd => pd.NbtAmount),
                            VatAmount = CoveringPoDetails.Sum(pd => pd.VatAmount),
                            TotalAmount = CoveringPoDetails.Sum(pd => pd.TotalAmount),
                            BasePr = int.Parse(ViewState["BasedPr"].ToString()),
                            ParentApprovedUser = int.Parse(ViewState["PoApprovedBy"].ToString()),
                            DeliverToWarehouse = int.Parse(ViewState["WarehouseId"].ToString()),
                            PaymentMethod = ViewState["PaymentMethod"].ToString(),
                            PoDetails = CoveringPoDetails
                        };

                        int CoveringPoId = ControllerFactory.CreatePOMasterController().GenerateCoveringPO(poMaster);

                        grnMaster.PoIds.Add(CoveringPoId);

                        grnMaster.GrnNote = "Exceeded Ordered Quantity. Remarks Given: " + txtRemarks.Text;
                    }


                    grnMaster.UploadedFiles = new List<GrnFiles>();

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("/GrnFiles"))) {

                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/GrnFiles"));
                    }
                    else {
                        for (int i = 0; i < docs.Count; i++) {

                            if (docs[i].ContentLength > 0) {

                                string filePath = "/GrnFiles/" + i + "_" + LocalTime.Now.Ticks + "_" + docs[i].FileName;

                                docs[i].SaveAs(HttpContext.Current.Server.MapPath(filePath));

                                GrnFiles doc = new GrnFiles() {
                                    FileName = docs[i].FileName,
                                    Location = "~" + filePath
                                };

                                grnMaster.UploadedFiles.Add(doc);
                            }

                        }
                    }

                    List<InvoiceDetails> invoiceDetails = new List<InvoiceDetails>();
                    if (ViewState["InvoiceDetails"] != null) {
                        invoiceDetails = new JavaScriptSerializer().Deserialize<List<InvoiceDetails>>(ViewState["InvoiceDetails"].ToString());
                        for (int i = 0; i < invoiceDetails.Count; i++) {
                            invoiceDetails[i].RemarkOn = invoiceDetails[i].RemarkOn.AddMinutes(330);
                            invoiceDetails[i].InvoiceDate = invoiceDetails[i].InvoiceDate.AddMinutes(330);
                        }
                    }
                    int paymentType = int.Parse(ViewState["PaymentMethod"].ToString());


                    
                    int result = ControllerFactory.CreateGrnController().GenerateGRN(grnMaster, int.Parse(Request.QueryString.Get("PoId")), int.Parse(ViewState["BasedPr"].ToString()), int.Parse(Session["UserId"].ToString()), invoiceDetails, invoiceImages);

                    if (result > 0) {
                        //sendEmails("GRN" + result, int.Parse(Session["CompanyId"].ToString()));

                        if (grnMaster.PoIds.Count > 1) {
                            //sendEmailToApproveCoveringPO("GRN" + result, grnMaster.PoIds[1], int.Parse(Session["CompanyId"].ToString()));
                        }
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',html:'Your GRN has been created with the code <b>GRN" + result + "</b>.', showConfirmButton: false,timer: 3000}).then((result)=> {window.location='CustomerViewApprovedPurchaseOrder.aspx';}); });   </script>", false);
                        gvAddedInvDetails.DataSource = null;
                        gvAddedInvDetails.DataBind();

                        ViewState["InvoiceDetails"] = null;
                        //btnDone.Visible = false;
                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on creating GRN'}); });   </script>", false);
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Provide Quantity for atleast one item'}); });   </script>", false);
                }

            }

            catch (Exception ex) {
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
        protected void btnBack_Click(object sender, EventArgs e) {
            try {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('div').removeClass('modal-backdrop'); $('#mdlPrevInvoices').modal('hide'); $('#mdlInvDetails').modal('show'); });   </script>", false);
                gvPrevInvoices.DataSource = null;
                gvPrevInvoices.DataBind();
            }
            catch (Exception EX) {

            }
        }
        protected void btnPrevInv_Click(object sender, EventArgs e) {
            try {
                gvPrevInvoices.DataSource = invoiceDetailsController.GetPreviousInvoices(int.Parse(Request.QueryString.Get("PoId")));
                gvPrevInvoices.DataBind();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>   $('div').removeClass('modal-backdrop'); $(document).ready(function () { $('#mdlPrevInvoices').modal('show'); });   </script>", false);
            }
            catch (Exception EX) {

            }
        }

        private void sendEmails(string GrnCode, int CompanyId) {

            var GRNMaster = GrnController.getGrnDetailsByGrnCode(GrnCode, CompanyId);

            List<string> emails = companyLoginController.GetUserEmailsForApprovalbyWarehouseId(6, GRNMaster.CategoryId, GRNMaster.TotalAmount, int.Parse(Session["CompanyId"].ToString()), 6, 10, GRNMaster.WarehouseId);

            if (emails.Count > 0) {
                StringBuilder message = new StringBuilder();

                message.Append("<html>");
                message.Append("	<head>	");
                message.Append("		<style>		");
                message.Append("			table {		");
                message.Append("			  font-family: arial, sans-serif;		");
                message.Append("			  border-collapse: collapse;		");
                message.Append("			  width: 100%;		");
                message.Append("			  }		");
                message.Append("			  ");
                message.Append("			  td {	");
                message.Append("			  border: 1px solid #dddddd;	");
                message.Append("			  text-align: left;		");
                message.Append("			  padding: 8px;		");
                message.Append("			  }		");
                message.Append("			  ");
                message.Append("			  th {	");
                message.Append("			  border: 1px solid #dddddd;	");
                message.Append("			  text-align: left;		");
                message.Append("			  padding: 8px;		");
                message.Append("			  background-color: #dddddd;	");
                message.Append("			  }		");
                message.Append("			 ");
                message.Append("			  div{		");
                message.Append("			  background-color:white	");
                message.Append("			  }	");
                message.Append("			  ");
                message.Append("			  ");
                message.Append("		</style>");
                message.Append("	</head>");
                message.Append("	");
                message.Append("	<body>  ");
                message.Append("	");
                message.Append("        <div>");
                message.Append("    <span>");
                message.Append("            Dear User,");
                message.Append("            <br>");
                message.Append("            <br>");
                message.Append("            You have new Goods Received Note pending for approval. Please login to your account at admin.ezbidlanka.lk and provide your");
                message.Append("            approval to proceed further.");
                message.Append("            <br>");
                message.Append("            <br>");
                message.Append("    </span>");
                message.Append("        </div>");
                message.Append("		<div style=\"width:100%\">");
                message.Append("			<table style = \"width:100%\">");
                message.Append("			<tbody>");
                message.Append("			<tr>");
                message.Append("				<h1 align=\"center\"> Goods Recieved Note (GRN)</h1>          ");
                message.Append("			</tr>");
                message.Append("			</tbody>");
                message.Append("			</table>");
                message.Append("		</div>");
                message.Append("		");
                message.Append("		<div>	");
                message.Append("			<table style = \"border:none; width :100%\">");
                message.Append("			<tbody>");
                message.Append("				<tr>");
                message.Append("					<td width = 30% rowspan=\"5\"> <b>SUPPLIER:</b> <br>");
                message.Append("													" + GRNMaster._Supplier.SupplierName + " <br>");
                message.Append("													" + GRNMaster._Supplier.Address1 + " <br>");
                message.Append("													" + GRNMaster._Supplier.OfficeContactNo + "/ " + GRNMaster._Supplier.PhoneNo + " ");
                message.Append("					</td>");
                message.Append("					");
                message.Append("					<td width = 30% rowspan=\"5\"> <b>DELIVERING WAREHOUSE:</b> <br>");
                message.Append("													" + GRNMaster._Warehouse.Location + "<br>");
                message.Append("													" + GRNMaster._Warehouse.Address + "<br>");
                message.Append("													" + GRNMaster._Warehouse.PhoneNo + "<br>");
                message.Append("					</td>");
                message.Append("					");
                message.Append("					<td width = 30% rowspan=\"5\"> <b>GRN CODE:</b>" + GRNMaster.GrnCode + "<br> ");
                message.Append("												 <b>PO CODE:</b> " + GRNMaster.POCode + "  <br>");
                message.Append("												 <b>BASED PR:</b>" + GRNMaster.PrCode + " <br>");
                message.Append("												 <b>RECEIVED DATE:</b> " + GRNMaster.GoodReceivedDate + "");
                message.Append("					");
                message.Append("					</td>");
                message.Append("				</tr>");
                message.Append("				");
                message.Append("			");
                message.Append("				");
                message.Append("				</tbody>");
                message.Append("			</table>");
                message.Append("		</div>	");
                message.Append("		</br>");
                message.Append("		<div style=\"width:100%\">	");
                message.Append("		  <table style=\"width:100%\">");
                message.Append("		   <tbody>");
                message.Append("			  <tr>	");
                message.Append("				 	");
                message.Append("				  <th>Item Name</th>	");
                message.Append("				  <th>Measurement</th>	");
                message.Append("				  <th>Quantity</th>");
                message.Append("				  <th style = \"text-align:right\">Unit Price</th>");
                message.Append("				  <th style = \"text-align:right\">Sub Total</th>");
                message.Append("				  <th style = \"text-align:right\">Vat Amount</th>		");
                message.Append("				  <th style = \"text-align:right\">NBT Amount</th>		");
                message.Append("				  <th style = \"text-align:right\">Total Amount</th>		");
                message.Append("			  </tr>		");
                for (int j = 0; j < GRNMaster.GrnDetailsList.Count; j++) {
                    message.Append("			  <tr>			");
                    message.Append("				  	");
                    message.Append("				  <td>" + GRNMaster.GrnDetailsList[j].ItemName + "</td>	");
                    message.Append("				  <td>" + GRNMaster.GrnDetailsList[j].MeasurementShortName + "</td>		");
                    message.Append("				  <td>" + GRNMaster.GrnDetailsList[j].Quantity + "</td>	");
                    message.Append("				  <td style = \"text-align:right\">" + GRNMaster.GrnDetailsList[j].ItemPrice + "</td>		");
                    message.Append("				  <td style = \"text-align:right\">" + (GRNMaster.GrnDetailsList[j].TotalAmount - (GRNMaster.GrnDetailsList[j].NbtAmount + GRNMaster.GrnDetailsList[j].VatAmount)) + "</td>	");
                    message.Append("				  <td style = \"text-align:right\">" + GRNMaster.GrnDetailsList[j].VatAmount + "</td>			");
                    message.Append("				  <td style = \"text-align:right\">" + GRNMaster.GrnDetailsList[j].NbtAmount + "</td>		");
                    message.Append("				  <td style = \"text-align:right\">" + GRNMaster.GrnDetailsList[j].TotalAmount + "</td>		");
                    message.Append("			  </tr>");
                }
                message.Append("			</tbody>	");
                message.Append("		  </table>");
                message.Append("		  ");
                message.Append("		</div>	");
                message.Append("		<br>");
                message.Append("		<div>		");
                message.Append("			 <table>");
                message.Append("				<tbody>");
                message.Append("				<tr>");
                message.Append("					<td width = 70%><b>GRN Note : </b> " + GRNMaster.GrnNote + " </td>");
                message.Append("									");
                message.Append("					<td rowspan=\"2\"><h2>Amount Details</h2> ");
                message.Append("				  ");
                message.Append("				  <table style=\"width:100%\" >	");
                message.Append("					  <tr>				");
                message.Append("						  <td>Sub Total</td>		");
                message.Append("						  <td style = \"text-align:right\">" + (GRNMaster.TotalAmount - (GRNMaster.TotalNbt + GRNMaster.TotalVat)) + "</td>	");
                message.Append("					  </tr>		");
                message.Append("					  <tr>				");
                message.Append("						  <td>VAT Total</td>	");
                message.Append("						  <td style = \"text-align:right\">" + GRNMaster.TotalVat + " </td>		");
                message.Append("					  </tr>			");
                message.Append("					  <tr>			");
                message.Append("						  <td>NBT Total </td>	");
                message.Append("						  <td style = \"text-align:right\">" + GRNMaster.TotalNbt + " </td>		");
                message.Append("					  </tr>			");
                message.Append("					  <tr>				");
                message.Append("						  <td><b>Total </b> </td>		");
                message.Append("						  <td style = \"text-align:right\"><b>" + GRNMaster.TotalAmount + " </b></td>	");
                message.Append("					  </tr>					");
                message.Append("				  </table>");
                message.Append("					");
                message.Append("					</td>");
                message.Append("			 	</tr>");
                message.Append("				");
                message.Append("				<tr>");
                message.Append("				<td width = 70%><b>QUOTATION FOR:</b>" + GRNMaster.QuotationFor + "<br> </b> </td>");
                message.Append("				</tr>");
                message.Append("				");
                message.Append("				</tbody>");
                message.Append("				");
                message.Append("			  </table>");
                message.Append("		</div> ");
                message.Append("		<br>		");
                message.Append("		<hr>	");
                message.Append("	");
                message.Append("		  <div>	");
                message.Append("			  " + GRNMaster.CreatedByName + "  <br>	");
                message.Append("			  " + GRNMaster.CreatedDate + "<br>	");
                message.Append("			  GRN Created By	");
                message.Append("		  </div> ");
                message.Append("		  ");
                message.Append("	  <hr> ");
                message.Append("		  <div style =\"text-align:center\">	");
                message.Append("			This is a Computer Generated Good Received Note");
                message.Append("		  </div>	");
                message.Append("	  <hr>");
                message.Append("    <br>");
                message.Append("    <br>");
                message.Append("    <div>");
                message.Append("    <span>");
                message.Append("    Thanks and Regards,<br>");
                message.Append("    Team EzBidLanka.");
                message.Append("    </span>");
                message.Append("    </div>");
                message.Append("	  ");
                message.Append("		</body>");
                message.Append("  </html>");
                message.Append("  <br>");
                message.Append("  <br>");





                string subject = "New Goods Received Note";

                EmailGenerator.SendEmailV2(emails, subject, message.ToString(), true);
            }
        }
        private void sendEmailToApproveCoveringPO(string GrnCode, int CPoID, int CompanyId) {

            var GRNMaster = GrnController.getGrnDetailsByGrnCode(GrnCode, CompanyId);
            var coveringPo = poMasterController.GetPoMasterToViewPO(CPoID, CompanyId);
            var parentPo = poMasterController.GetPoMasterToViewPO(coveringPo.IsDerivedFromPo, CompanyId);
            List<string> emails = companyLoginController.GetEmailsByUserId(int.Parse(parentPo.ApprovedBy));
            if (emails.Count > 0) {
                StringBuilder message = new StringBuilder();

                message.Append("<html>");
                message.Append("	<body>");
                message.Append("	<div style = \"text-align: justify\">");
                message.Append("		Dear User,");
                message.Append("		<br>");
                message.Append("		<br>");
                message.Append("		You have Covering Purchase Order pending for approval. Please login to your account at admin.ezbidlanka.lk and provide your approval to proceed further.");
                message.Append("		<br>");
                message.Append("		<br>");
                message.Append("	</div>");
                message.Append("	</body>");
                message.Append("</html>");

                message.Append("<html>");
                message.Append("	<head>	");
                message.Append("		<style>		");
                message.Append("			table {		");
                message.Append("			  font-family: arial, sans-serif;		");
                message.Append("			  border-collapse: collapse;		");
                message.Append("			  width: 100%;		");
                message.Append("			  }		");
                message.Append("			  ");
                message.Append("			  td {	");
                message.Append("			  border: 1px solid #dddddd;	");
                message.Append("			  text-align: left;		");
                message.Append("			  padding: 8px;		");
                message.Append("			  }		");
                message.Append("			  ");
                message.Append("			  th {	");
                message.Append("			  border: 1px solid #dddddd;	");
                message.Append("			  text-align: left;		");
                message.Append("			  padding: 8px;		");
                message.Append("			  background-color: #dddddd;	");
                message.Append("			  }		");
                message.Append("			 ");
                message.Append("			  div{		");
                message.Append("			  background-color:white	");
                message.Append("			  }	");
                message.Append("			  ");
                message.Append("			  ");
                message.Append("		</style>");
                message.Append("	</head>");
                message.Append("	");
                message.Append("	<body>  ");
                message.Append("	");
                message.Append("		<div style=\"width:100%\">");
                message.Append("			<table style = \"width:100%\">");
                message.Append("			<tbody>");
                message.Append("			<tr>");
                message.Append("				<h1 align=\"center\"> Covering Purchase Order (PO)</h1>          ");
                message.Append("			</tr>");
                message.Append("			</tbody>");
                message.Append("			</table>");
                message.Append("		</div>");
                message.Append("		");
                message.Append("		<div>	");
                message.Append("			<table style = \"border:none; width :100%\">");
                message.Append("			<tbody>");
                message.Append("				<tr>");
                message.Append("					<td width = 30% rowspan=\"5\"> <b>SUPPLIER:</b> <br>");
                message.Append("													" + coveringPo._Supplier.SupplierName + " <br>");
                message.Append("													" + coveringPo._Supplier.Address1 + " <br>");
                message.Append("													" + coveringPo._Supplier.OfficeContactNo + "/ " + coveringPo._Supplier.PhoneNo + " ");
                message.Append("					</td>");
                message.Append("					");
                message.Append("					<td width = 30% rowspan=\"5\"> <b>DELIVERING WAREHOUSE:</b> <br>");
                message.Append("													" + coveringPo.Warehouse.Location + "<br>");
                message.Append("													" + coveringPo.Warehouse.Address + "<br>");
                message.Append("													" + coveringPo.Warehouse.PhoneNo + "<br>");
                message.Append("					</td>");
                message.Append("					");
                message.Append("												 <b>PO CODE:</b> " + coveringPo.POCode + "  <br>");
                message.Append("												 <b>BASED PR:</b>" + coveringPo.PrCode + " <br>");
                message.Append("					");
                message.Append("					</td>");
                message.Append("				</tr>");
                message.Append("				");
                message.Append("			");
                message.Append("				");
                message.Append("				</tbody>");
                message.Append("			</table>");
                message.Append("		</div>	");
                message.Append("		</br>");
                message.Append("		<div style=\"width:100%\">	");
                message.Append("		  <table style=\"width:100%\">");
                message.Append("		   <tbody>");
                message.Append("			  <tr>	");
                message.Append("				 	");
                message.Append("				  <th>Item Name</th>	");
                message.Append("				  <th>Measurement</th>	");
                message.Append("				  <th>Quantity</th>");
                message.Append("				  <th style = \"text-align:right\">Unit Price</th>");
                message.Append("				  <th style = \"text-align:right\">Sub Total</th>");
                message.Append("				  <th style = \"text-align:right\">Vat Amount</th>		");
                message.Append("				  <th style = \"text-align:right\">NBT Amount</th>		");
                message.Append("				  <th style = \"text-align:right\">Total Amount</th>		");
                message.Append("			  </tr>		");
                for (int j = 0; j < coveringPo.PoDetails.Count; j++) {
                    message.Append("			  <tr>			");
                    message.Append("				  	");
                    message.Append("				  <td>" + coveringPo.PoDetails[j].ItemName + "</td>	");
                    message.Append("				  <td>" + coveringPo.PoDetails[j].MeasurementShortName + "</td>		");
                    message.Append("				  <td>" + coveringPo.PoDetails[j].Quantity + "</td>	");
                    message.Append("				  <td style = \"text-align:right\">" + coveringPo.PoDetails[j].ItemPrice + "</td>		");
                    message.Append("				  <td style = \"text-align:right\">" + (coveringPo.PoDetails[j].TotalAmount - (coveringPo.PoDetails[j].NbtAmount + coveringPo.PoDetails[j].VatAmount)) + "</td>	");
                    message.Append("				  <td style = \"text-align:right\">" + coveringPo.PoDetails[j].VatAmount + "</td>			");
                    message.Append("				  <td style = \"text-align:right\">" + coveringPo.PoDetails[j].NbtAmount + "</td>		");
                    message.Append("				  <td style = \"text-align:right\">" + coveringPo.PoDetails[j].TotalAmount + "</td>		");
                    message.Append("			  </tr>");
                }
                message.Append("			</tbody>	");
                message.Append("		  </table>");
                message.Append("		  ");
                message.Append("		</div>	");
                message.Append("		<br>");
                message.Append("		<div>		");
                message.Append("			 <table>");
                message.Append("				<tbody>");
                message.Append("				<tr>");
                message.Append("					<td width = 70%><b>Quotation For : </b> " + coveringPo.QuotationFor + " </td>");
                message.Append("									");
                message.Append("					<td rowspan=\"2\"><h2>Amount Details</h2> ");
                message.Append("				  ");
                message.Append("				  <table style=\"width:100%\" >	");
                message.Append("					  <tr>				");
                message.Append("						  <td>Sub Total</td>		");
                message.Append("						  <td style = \"text-align:right\">" + (coveringPo.TotalAmount - (coveringPo.NBTAmount + coveringPo.VatAmount)) + "</td>	");
                message.Append("					  </tr>		");
                message.Append("					  <tr>				");
                message.Append("						  <td>VAT Total</td>	");
                message.Append("						  <td style = \"text-align:right\">" + coveringPo.VatAmount + " </td>		");
                message.Append("					  </tr>			");
                message.Append("					  <tr>			");
                message.Append("						  <td>NBT Total </td>	");
                message.Append("						  <td style = \"text-align:right\">" + coveringPo.NBTAmount + " </td>		");
                message.Append("					  </tr>			");
                message.Append("					  <tr>				");
                message.Append("						  <td><b>Total </b> </td>		");
                message.Append("						  <td style = \"text-align:right\"><b>" + coveringPo.TotalAmount + " </b></td>	");
                message.Append("					  </tr>					");
                message.Append("				  </table>");
                message.Append("					");
                message.Append("					</td>");
                message.Append("			 	</tr>");
                message.Append("				</tbody>");
                message.Append("				");
                message.Append("			  </table>");
                message.Append("		</div> ");
                message.Append("		<br>		");
                message.Append("		<hr>	");
                message.Append("		  <div style =\"text-align:center\">	");
                message.Append("			This is a Computer Generated Purchase Order");
                message.Append("		  </div>	");
                message.Append("	  <hr>");
                message.Append("    <br>");
                message.Append("    <br>");
                message.Append("		</body>");
                message.Append("  </html>");
                message.Append("  <br>");
                message.Append("  <br>");


                message.Append("<html>");
                message.Append("	<head>	");
                message.Append("		<style>		");
                message.Append("			table {		");
                message.Append("			  font-family: arial, sans-serif;		");
                message.Append("			  border-collapse: collapse;		");
                message.Append("			  width: 100%;		");
                message.Append("			  }		");
                message.Append("			  ");
                message.Append("			  td {	");
                message.Append("			  border: 1px solid #dddddd;	");
                message.Append("			  text-align: left;		");
                message.Append("			  padding: 8px;		");
                message.Append("			  }		");
                message.Append("			  ");
                message.Append("			  th {	");
                message.Append("			  border: 1px solid #dddddd;	");
                message.Append("			  text-align: left;		");
                message.Append("			  padding: 8px;		");
                message.Append("			  background-color: #dddddd;	");
                message.Append("			  }		");
                message.Append("			 ");
                message.Append("			  div{		");
                message.Append("			  background-color:white	");
                message.Append("			  }	");
                message.Append("			  ");
                message.Append("			  ");
                message.Append("		</style>");
                message.Append("	</head>");
                message.Append("	");
                message.Append("	<body>  ");
                message.Append("		<div style=\"width:100%\">");
                message.Append("			<table style = \"width:100%\">");
                message.Append("			<tbody>");
                message.Append("			<tr>");
                message.Append("				<h1 align=\"center\"> Goods Recieved Note (GRN)</h1>          ");
                message.Append("			</tr>");
                message.Append("			</tbody>");
                message.Append("			</table>");
                message.Append("		</div>");
                message.Append("		");
                message.Append("		<div>	");
                message.Append("			<table style = \"border:none; width :100%\">");
                message.Append("			<tbody>");
                message.Append("				<tr>");
                message.Append("					<td width = 30% rowspan=\"5\"> <b>SUPPLIER:</b> <br>");
                message.Append("													" + GRNMaster._Supplier.SupplierName + " <br>");
                message.Append("													" + GRNMaster._Supplier.Address1 + " <br>");
                message.Append("													" + GRNMaster._Supplier.OfficeContactNo + "/ " + GRNMaster._Supplier.PhoneNo + " ");
                message.Append("					</td>");
                message.Append("					");
                message.Append("					<td width = 30% rowspan=\"5\"> <b>DELIVERING WAREHOUSE:</b> <br>");
                message.Append("													" + GRNMaster._Warehouse.Location + "<br>");
                message.Append("													" + GRNMaster._Warehouse.Address + "<br>");
                message.Append("													" + GRNMaster._Warehouse.PhoneNo + "<br>");
                message.Append("					</td>");
                message.Append("					");
                message.Append("					<td width = 30% rowspan=\"5\"> <b>GRN CODE:</b>" + GRNMaster.GrnCode + "<br> ");
                message.Append("												 <b>PO CODE:</b> " + GRNMaster.POCode + "  <br>");
                message.Append("												 <b>BASED PR:</b>" + GRNMaster.PrCode + " <br>");
                message.Append("												 <b>RECEIVED DATE:</b> " + GRNMaster.GoodReceivedDate + "");
                message.Append("					");
                message.Append("					</td>");
                message.Append("				</tr>");
                message.Append("				");
                message.Append("			");
                message.Append("				");
                message.Append("				</tbody>");
                message.Append("			</table>");
                message.Append("		</div>	");
                message.Append("		</br>");
                message.Append("		<div style=\"width:100%\">	");
                message.Append("		  <table style=\"width:100%\">");
                message.Append("		   <tbody>");
                message.Append("			  <tr>	");
                message.Append("				 	");
                message.Append("				  <th>Item Name</th>	");
                message.Append("				  <th>Measurement</th>	");
                message.Append("				  <th>Quantity</th>");
                message.Append("				  <th style = \"text-align:right\">Unit Price</th>");
                message.Append("				  <th style = \"text-align:right\">Sub Total</th>");
                message.Append("				  <th style = \"text-align:right\">Vat Amount</th>		");
                message.Append("				  <th style = \"text-align:right\">NBT Amount</th>		");
                message.Append("				  <th style = \"text-align:right\">Total Amount</th>		");
                message.Append("			  </tr>		");
                for (int j = 0; j < GRNMaster.GrnDetailsList.Count; j++) {
                    message.Append("			  <tr>			");
                    message.Append("				  	");
                    message.Append("				  <td>" + GRNMaster.GrnDetailsList[j].ItemName + "</td>	");
                    message.Append("				  <td>" + GRNMaster.GrnDetailsList[j].MeasurementShortName + "</td>		");
                    message.Append("				  <td>" + GRNMaster.GrnDetailsList[j].Quantity + "</td>	");
                    message.Append("				  <td style = \"text-align:right\">" + GRNMaster.GrnDetailsList[j].ItemPrice + "</td>		");
                    message.Append("				  <td style = \"text-align:right\">" + (GRNMaster.GrnDetailsList[j].TotalAmount - (GRNMaster.GrnDetailsList[j].NbtAmount + GRNMaster.GrnDetailsList[j].VatAmount)) + "</td>	");
                    message.Append("				  <td style = \"text-align:right\">" + GRNMaster.GrnDetailsList[j].VatAmount + "</td>			");
                    message.Append("				  <td style = \"text-align:right\">" + GRNMaster.GrnDetailsList[j].NbtAmount + "</td>		");
                    message.Append("				  <td style = \"text-align:right\">" + GRNMaster.GrnDetailsList[j].TotalAmount + "</td>		");
                    message.Append("			  </tr>");
                }
                message.Append("			</tbody>	");
                message.Append("		  </table>");
                message.Append("		  ");
                message.Append("		</div>	");
                message.Append("		<br>");
                message.Append("		<div>		");
                message.Append("			 <table>");
                message.Append("				<tbody>");
                message.Append("				<tr>");
                message.Append("					<td width = 70%><b>GRN Note : </b> " + GRNMaster.GrnNote + " </td>");
                message.Append("									");
                message.Append("					<td rowspan=\"2\"><h2>Amount Details</h2> ");
                message.Append("				  ");
                message.Append("				  <table style=\"width:100%\" >	");
                message.Append("					  <tr>				");
                message.Append("						  <td>Sub Total</td>		");
                message.Append("						  <td style = \"text-align:right\">" + (GRNMaster.TotalAmount - (GRNMaster.TotalNbt + GRNMaster.TotalVat)) + "</td>	");
                message.Append("					  </tr>		");
                message.Append("					  <tr>				");
                message.Append("						  <td>VAT Total</td>	");
                message.Append("						  <td style = \"text-align:right\">" + GRNMaster.TotalVat + " </td>		");
                message.Append("					  </tr>			");
                message.Append("					  <tr>			");
                message.Append("						  <td>NBT Total </td>	");
                message.Append("						  <td style = \"text-align:right\">" + GRNMaster.TotalNbt + " </td>		");
                message.Append("					  </tr>			");
                message.Append("					  <tr>				");
                message.Append("						  <td><b>Total </b> </td>		");
                message.Append("						  <td style = \"text-align:right\"><b>" + GRNMaster.TotalAmount + " </b></td>	");
                message.Append("					  </tr>					");
                message.Append("				  </table>");
                message.Append("					");
                message.Append("					</td>");
                message.Append("			 	</tr>");
                message.Append("				");
                message.Append("				<tr>");
                message.Append("				<td width = 70%><b>QUOTATION FOR:</b>" + GRNMaster.QuotationFor + "<br> </b> </td>");
                message.Append("				</tr>");
                message.Append("				");
                message.Append("				</tbody>");
                message.Append("				");
                message.Append("			  </table>");
                message.Append("		</div> ");
                message.Append("		<br>		");
                message.Append("		<hr>	");
                message.Append("	");
                message.Append("		  <div>	");
                message.Append("			  " + GRNMaster.CreatedByName + "  <br>	");
                message.Append("			  " + GRNMaster.CreatedDate + "<br>	");
                message.Append("			  GRN Created By	");
                message.Append("		  </div> ");
                message.Append("		  ");
                message.Append("	  <hr> ");
                message.Append("		  <div style =\"text-align:center\">	");
                message.Append("			This is a Computer Generated Goods Received Note");
                message.Append("		  </div>	");
                message.Append("	  <hr>");
                message.Append("		</body>");
                message.Append("  </html>");
                message.Append("  <br>");
                message.Append("  <br>");


                message.Append("<html>");
                message.Append("	<head>	");
                message.Append("		<style>		");
                message.Append("			table {		");
                message.Append("			  font-family: arial, sans-serif;		");
                message.Append("			  border-collapse: collapse;		");
                message.Append("			  width: 100%;		");
                message.Append("			  }		");
                message.Append("			  ");
                message.Append("			  td {	");
                message.Append("			  border: 1px solid #dddddd;	");
                message.Append("			  text-align: left;		");
                message.Append("			  padding: 8px;		");
                message.Append("			  }		");
                message.Append("			  ");
                message.Append("			  th {	");
                message.Append("			  border: 1px solid #dddddd;	");
                message.Append("			  text-align: left;		");
                message.Append("			  padding: 8px;		");
                message.Append("			  background-color: #dddddd;	");
                message.Append("			  }		");
                message.Append("			 ");
                message.Append("			  div{		");
                message.Append("			  background-color:white	");
                message.Append("			  }	");
                message.Append("			  ");
                message.Append("			  ");
                message.Append("		</style>");
                message.Append("	</head>");
                message.Append("	");
                message.Append("	<body>  ");
                message.Append("	");
                message.Append("		<div style=\"width:100%\">");
                message.Append("			<table style = \"width:100%\">");
                message.Append("			<tbody>");
                message.Append("			<tr>");
                message.Append("				<h1 align=\"center\"> Parent Purchase Order (PO)</h1>          ");
                message.Append("			</tr>");
                message.Append("			</tbody>");
                message.Append("			</table>");
                message.Append("		</div>");
                message.Append("		");
                message.Append("		<div>	");
                message.Append("			<table style = \"border:none; width :100%\">");
                message.Append("			<tbody>");
                message.Append("				<tr>");
                message.Append("					<td width = 30% rowspan=\"5\"> <b>SUPPLIER:</b> <br>");
                message.Append("													" + parentPo._Supplier.SupplierName + " <br>");
                message.Append("													" + parentPo._Supplier.Address1 + " <br>");
                message.Append("													" + parentPo._Supplier.OfficeContactNo + "/ " + parentPo._Supplier.PhoneNo + " ");
                message.Append("					</td>");
                message.Append("					");
                message.Append("					<td width = 30% rowspan=\"5\"> <b>DELIVERING WAREHOUSE:</b> <br>");
                message.Append("													" + parentPo.Warehouse.Location + "<br>");
                message.Append("													" + parentPo.Warehouse.Address + "<br>");
                message.Append("													" + parentPo.Warehouse.PhoneNo + "<br>");
                message.Append("					</td>");
                message.Append("					");
                message.Append("												 <b>PO CODE:</b> " + parentPo.POCode + "  <br>");
                message.Append("												 <b>BASED PR:</b>" + parentPo.PrCode + " <br>");
                message.Append("					");
                message.Append("					</td>");
                message.Append("				</tr>");
                message.Append("				");
                message.Append("			");
                message.Append("				");
                message.Append("				</tbody>");
                message.Append("			</table>");
                message.Append("		</div>	");
                message.Append("		</br>");
                message.Append("		<div style=\"width:100%\">	");
                message.Append("		  <table style=\"width:100%\">");
                message.Append("		   <tbody>");
                message.Append("			  <tr>	");
                message.Append("				 	");
                message.Append("				  <th>Item Name</th>	");
                message.Append("				  <th>Measurement</th>	");
                message.Append("				  <th>Quantity</th>");
                message.Append("				  <th style = \"text-align:right\">Unit Price</th>");
                message.Append("				  <th style = \"text-align:right\">Sub Total</th>");
                message.Append("				  <th style = \"text-align:right\">Vat Amount</th>		");
                message.Append("				  <th style = \"text-align:right\">NBT Amount</th>		");
                message.Append("				  <th style = \"text-align:right\">Total Amount</th>		");
                message.Append("			  </tr>		");
                for (int j = 0; j < parentPo.PoDetails.Count; j++) {
                    message.Append("			  <tr>			");
                    message.Append("				  	");
                    message.Append("				  <td>" + parentPo.PoDetails[j].ItemName + "</td>	");
                    message.Append("				  <td>" + parentPo.PoDetails[j].MeasurementShortName + "</td>		");
                    message.Append("				  <td>" + parentPo.PoDetails[j].Quantity + "</td>	");
                    message.Append("				  <td style = \"text-align:right\">" + parentPo.PoDetails[j].ItemPrice + "</td>		");
                    message.Append("				  <td style = \"text-align:right\">" + (parentPo.PoDetails[j].TotalAmount - (parentPo.PoDetails[j].NbtAmount + parentPo.PoDetails[j].VatAmount)) + "</td>	");
                    message.Append("				  <td style = \"text-align:right\">" + parentPo.PoDetails[j].VatAmount + "</td>			");
                    message.Append("				  <td style = \"text-align:right\">" + parentPo.PoDetails[j].NbtAmount + "</td>		");
                    message.Append("				  <td style = \"text-align:right\">" + parentPo.PoDetails[j].TotalAmount + "</td>		");
                    message.Append("			  </tr>");
                }
                message.Append("			</tbody>	");
                message.Append("		  </table>");
                message.Append("		  ");
                message.Append("		</div>	");
                message.Append("		<br>");
                message.Append("		<div>		");
                message.Append("			 <table>");
                message.Append("				<tbody>");
                message.Append("				<tr>");
                message.Append("					<td width = 70%><b>Quotation For : </b> " + parentPo.QuotationFor + " </td>");
                message.Append("									");
                message.Append("					<td rowspan=\"2\"><h2>Amount Details</h2> ");
                message.Append("				  ");
                message.Append("				  <table style=\"width:100%\" >	");
                message.Append("					  <tr>				");
                message.Append("						  <td>Sub Total</td>		");
                message.Append("						  <td style = \"text-align:right\">" + (parentPo.TotalAmount - (parentPo.NBTAmount + parentPo.VatAmount)) + "</td>	");
                message.Append("					  </tr>		");
                message.Append("					  <tr>				");
                message.Append("						  <td>VAT Total</td>	");
                message.Append("						  <td style = \"text-align:right\">" + parentPo.VatAmount + " </td>		");
                message.Append("					  </tr>			");
                message.Append("					  <tr>			");
                message.Append("						  <td>NBT Total </td>	");
                message.Append("						  <td style = \"text-align:right\">" + parentPo.NBTAmount + " </td>		");
                message.Append("					  </tr>			");
                message.Append("					  <tr>				");
                message.Append("						  <td><b>Total </b> </td>		");
                message.Append("						  <td style = \"text-align:right\"><b>" + parentPo.TotalAmount + " </b></td>	");
                message.Append("					  </tr>					");
                message.Append("				  </table>");
                message.Append("					");
                message.Append("					</td>");
                message.Append("			 	</tr>");
                message.Append("				</tbody>");
                message.Append("				");
                message.Append("			  </table>");
                message.Append("		</div> ");
                message.Append("		<br>		");
                message.Append("		<hr>	");
                message.Append("		  <div style =\"text-align:center\">	");
                message.Append("			This is a Computer Generated Purchase Order");
                message.Append("		  </div>	");
                message.Append("	  <hr>");
                message.Append("    <br>");
                message.Append("    <br>");
                message.Append("    <div>");
                message.Append("    <span>");
                message.Append("    Thanks and Regards,<br>");
                message.Append("    Team EzBidLanka.");
                message.Append("    </span>");
                message.Append("    </div>");
                message.Append("	  ");
                message.Append("		</body>");
                message.Append("  </html>");



                string subject = "New Goods Received Note";

                EmailGenerator.SendEmailV2(emails, subject, message.ToString(), true);
            }
        }

        protected void btnTerminate_Click(object sender, EventArgs e) {
            List<int> PoDIds = new List<int>();

            for (int i = 0; i < gvPoItems.Rows.Count; i++) {
                if ((gvPoItems.Rows[i].FindControl("CheckBox1") as CheckBox).Checked) {
                    PoDIds.Add(int.Parse(gvPoItems.Rows[i].Cells[1].Text));
                }
            }

            if (PoDIds.Count > 0) {
                int result = ControllerFactory.CreatePODetailsController().TerminatePoDetail(int.Parse(Request.QueryString.Get("PoId")), PoDIds, int.Parse(Session["UserId"].ToString()), hdnRemarks.Value);
                if (result > 0) {
                    Populate();
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',html:'Items have been Terminated', showConfirmButton: false,timer: 3000}); });   </script>", false);
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on Terminating Items'}); });   </script>", false);
                }
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please Select Atleast One Item to Terminate'}); });   </script>", false);
            }
        }
    }
}
