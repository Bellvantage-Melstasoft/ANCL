using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data;
using System.Web.Script.Serialization;

namespace BiddingSystem {
    public partial class ViewApprovePOPrint : System.Web.UI.Page {
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        PODetailsController pODetailsController = ControllerFactory.CreatePODetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        QuotationImageController quotationImageController = ControllerFactory.CreateQuotationImageController();
        SupplierBiddingFileUploadController supplierBiddingFileUploadController = ControllerFactory.CreateSupplierBiddingFileUploadController();
        InvoiceDetailsController invoiceDetailsController = ControllerFactory.CreateInvoiceDetailsController();

        // static string UserId = string.Empty;
        // private string PRId = string.Empty;

        // private string UserDept = string.Empty;
        //  private string OurRef = string.Empty;
        // private string PrCode = string.Empty;
        // private string RequestedDate = string.Empty;
        //  private string UserRef = string.Empty;
        // private string RequesterName = string.Empty;
        // private int basePr = 0;
        //int CompanyId = 0;
        // int PoId = 0;
        //  static int quationid = 0;
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewApprovePOPrint.aspx";
                // ((BiddingAdmin)Page.Master).subTabId = "ApprovePOLink";

                // CompanyId = int.Parse(Session["CompanyId"].ToString());
                // UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 30) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }

                //if (Session["PoId"] != null) {
                //    ViewState["PoId"] = int.Parse(Session["PoId"].ToString());
                //}
                //else
                //{
                //    Response.Redirect("CusromerPOView.aspx");
                //}
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack) {
                POMaster poMaster = pOMasterController.GetPoMasterToViewPO(int.Parse(Request.QueryString.Get("PoId")), int.Parse(Session["CompanyId"].ToString()));
                SupplierQuotation ImportDetails = supplierQuotationController.GetImportDetails(int.Parse(Request.QueryString.Get("PoId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["PurchaseType"] = poMaster.PurchaseType;

                lblsupplierName.Text = poMaster._Supplier.SupplierName;
                lblSupplierAddress.Text = poMaster._Supplier.Address1;
                lblSupplierContact.Text = poMaster._Supplier.OfficeContactNo + " / " + poMaster._Supplier.PhoneNo;
                txtRemarks.Text = poMaster.Remarks == null ? "-" : poMaster.Remarks;

                lblWarehouseName.Text = poMaster._Warehouse.Location;
                lblWarehouseAddress.Text = poMaster._Warehouse.Address;
                lblWarehouseContact.Text = poMaster._Warehouse.PhoneNo;
                lblStoreKeeper.Text = poMaster.StoreKeeper;

                lblPOCode.Text = poMaster.POCode;
                lblPrCode.Text = "PR-" + poMaster.PrCode;
                // lblQuotationFor.Text = poMaster.QuotationFor;

                int VarPoPurchaseType = 0;
                string VarSupplierAgentName = "";
                //gvPoItems.DataSource = poMaster.PoDetails;
                //gvPoItems.DataBind();

                if (poMaster.PurchaseType == 2) {
                    PanenImports.Visible = true;
                    pnlLogo.Visible = true;
                    lblCurrency.Text = ImportDetails.CurrencyShortname;
                    //lblPriceTerms.Text = ImportDetails.TermName;
                    lblPaymentMode.Text = ImportDetails.PaymentMode;
                }

                for (int i = 0; i < poMaster.PoDetails.Count; i++) {
                    VarPoPurchaseType = poMaster.PoDetails[i].PoPurchaseType;
                    VarSupplierAgentName = poMaster.PoDetails[i].SupplierAgentName;
                    if (poMaster.PurchaseType == 2 && poMaster.PoDetails[i].PoPurchaseType == 1) {
                        PanenImports.Visible = false;
                        pnlLogo.Visible = false;
                    }
                }

                if (poMaster.PaymentMethod != null && poMaster.PaymentMethod != "") {
                    if (VarPoPurchaseType == 1) {
                        lblPaymentMethod.Text = poMaster.PaymentMethod == "1" ? "Cash Payment" : poMaster.PaymentMethod == "2" ? "Cheque Payment" : "Credit";
                    }
                    else if (VarPoPurchaseType == 2) {
                        lblPaymentMethod.Text = poMaster.PaymentMethod == "1" ? "Advance" : poMaster.PaymentMethod == "2" ? "On Arrival" : poMaster.PaymentMethod == "3" ? "LC at sight" : poMaster.PaymentMethod == "4" ? "L/C usance" : poMaster.PaymentMethod == "5" ? "D/A" : poMaster.PaymentMethod == "6" ? "D/P" : "-";

                    }
                    pnlPaymentMethod.Visible = true;
                    // ddlPaymentMethod.SelectedValue = poMaster.PaymentMethod;
                }
                lblPoPurchaseType.Text = VarPoPurchaseType == 1 ? "Local" : "Import";
                lblAgentName.Text = VarSupplierAgentName;

                //tdSubTotal.InnerHtml = (poMaster.TotalAmount - poMaster.NBTAmount - poMaster.VatAmount).ToString("N2");
                ////tdNbt.InnerHtml = poMaster.NBTAmount.ToString("N2");
                //tdVat.InnerHtml = poMaster.VatAmount.ToString("N2");
                //tdNetTotal.InnerHtml = poMaster.TotalAmount.ToString("N2");

                if (poMaster.PurchaseType == 2) {
                    if (poMaster.ImportItemType == 2) {
                        gvPoItems.Columns[16].Visible = false;
                    }
                }
                if (poMaster.PurchaseType == 1) {
                    gvPoItems.Columns[15].Visible = false;
                    gvPoItems.Columns[16].Visible = false;
                }

                if (poMaster.PurchaseType == 2) {
                    gvPoItems.Columns[12].Visible = false;
                }

               
               
                if (poMaster.IsDerived == 0) {
                    lblGeneral.Visible = true;
                    ViewState["PoType"] = "0";  //General
                    hdnPoType.Value = "0";
                  //  pnlSelectPaymentMethod.Visible = true;
                }
                else if (poMaster.IsDerived == 1 && poMaster.IsDerivedType == 1) {
                    lblModified.Visible = true;
                    ViewState["PoType"] = "1";  //Modified
                    hdnPoType.Value = "1";
                    //pnlSelectPaymentMethod.Visible = true;
                }
                else {
                    lblCovering.Visible = true;
                    ViewState["PoType"] = "2";  //Covering
                    hdnPoType.Value = "2";
                }

                //if (poMaster.IsApproved == 0) {
                //lblPending.Visible = true;
                // }
                // else if (poMaster.IsApproved == 1) {
                //   lblApproved.Visible = true;
                // }
                // else {
                //    lblRejected.Visible = true;
                //}

                //lblCreatedByName.Text = poMaster.CreatedByName;
                lblCreatedByDesignation.Text = poMaster.CreatedDesignationName;
                lblCreatedDate.Text = poMaster.CreatedDate.ToString("dd/MM/yyyy");

                List<InvoiceDetails> Invoices = invoiceDetailsController.GetPreviousInvoices(int.Parse(Request.QueryString.Get("PoId")));
                if (Invoices.Count > 0) {
                    pnlInv.Visible = true;
                    gvPrevInvoices.DataSource = Invoices;
                    gvPrevInvoices.DataBind();
                }

                List<FollowUpRemark> remarks = ControllerFactory.CreateFollowUpRemarksController().GetRemarks(int.Parse(Request.QueryString.Get("PoId")), int.Parse(Session["CompanyId"].ToString()));
                if (remarks.Count > 0) {
                    pnlRemarks.Visible = true;
                    gvRemarks.DataSource = remarks;
                    gvRemarks.DataBind();
                }

                if (File.Exists(HttpContext.Current.Server.MapPath(poMaster.CreatedSignature)))
                    imgCreatedBySignature.ImageUrl = poMaster.CreatedSignature;
                else
                    imgCreatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";


                if (poMaster.IsDerived == 1) {
                    //if (poMaster.DerivedFromPOs.Count > 0) {
                    //    gvDerivedFrom.DataSource = poMaster.DerivedFromPOs;
                    //    gvDerivedFrom.DataBind();
                    //    pnlDerivedFrom.Visible = true;
                    //}
                    //lblRemarks.Text = poMaster.DerivingReason;
                    //pnlReason.Visible = true;
                }

                //if (poMaster.DerivedPOs.Count > 0) {
                //    gvDerivedPOs.DataSource = poMaster.DerivedPOs;
                //    gvDerivedPOs.DataBind();

                //    pnlDerrivedPOs.Visible = true;
                //}

                //if (poMaster.GeneratedGRNs.Count > 0) {
                //    gvGRNs.DataSource = poMaster.GeneratedGRNs;
                //    gvGRNs.DataBind();

                //    pnlGeneratedGRNs.Visible = true;
                //}

                

                if (poMaster.IsDerived == 1 && poMaster.IsApprovedByParentApprovedUser == 0) {
                    ViewState["ApproverType"] = "1";    //ParentApprover
                }
                else {
                    ViewState["ApproverType"] = "2";    //LimitApprover
                }

                lblCompName.Text = poMaster._companyDepartment.DepartmentName;
                lblCompVatNo.Text = poMaster._companyDepartment.VatNo;
                lblTpNo.Text = poMaster._companyDepartment.PhoneNO;
                lblFax.Text = poMaster._companyDepartment.FaxNO;
                lblcompAdd.Text = poMaster._companyDepartment.Address1;
                // lblRefNo.Text = pr_MasterController.FetchApprovePRDataByPRId(basePr).Ref01;
                lblDepartment.Text = poMaster.subdepartment;
                lblPurchaseType.Text = poMaster.PurchaseType == 1 ? "Local" : "Import";
                if (poMaster.IsApproved != 0) {
                    pnlApprovedBy.Visible = true;
                    //lblApprovedByName.Text = poMaster.ApprovedByName;
                    lblApprovedByDesignation.Text = poMaster.ApprovedDesignationName;
                    lblApprovedDate.Text = poMaster.ApprovedDate.ToString("dd/MM/yyyy");
                    lblApprovalRemarks.Text = poMaster.ApprovalRemarks;
                    if (File.Exists(HttpContext.Current.Server.MapPath(poMaster.ApprovedSignature)))
                        imgApprovedBySignature.ImageUrl = poMaster.ApprovedSignature;
                    else
                        imgApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                }

                if (poMaster.IsApproved == 1) {
                    lblApprovalText.InnerHtml = "Approved By";
                }
                else if (poMaster.IsApproved == 2) {
                    lblApprovalText.InnerHtml = "Rejected By";
                }

                /*if (poMaster.IsDerived == 1 && poMaster.IsApprovedByParentApprovedUser != 0 && poMaster.ParentApprovedByName != "" && poMaster.ParentApprovedByName != null) {
                    pnlParentApprovedByDetails.Visible = true;
                    lblParentApprovalRemarks.Text = poMaster.ParentApprovedUserApprovalRemarks;
                    lblParentApprovedByName.Text = poMaster.ParentApprovedByName;
                    lblParentApprovedDate.Text = poMaster.ParentApprovedUserApprovalDate.ToString("dd/MM/yyyy");

                    if (File.Exists(HttpContext.Current.Server.MapPath(poMaster.ParentApprovedBySignature)))
                        imgParentApprovedBySignature.ImageUrl = poMaster.ParentApprovedBySignature;
                    else
                        imgParentApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                }

                if (poMaster.IsDerived == 1 && poMaster.IsApprovedByParentApprovedUser == 1 && poMaster.ParentApprovedByName != "" && poMaster.ParentApprovedByName != null) {
                    lblParentApprovalText.InnerHtml = "Parent Approved User: APPROVED";
                }
                else if (poMaster.IsDerived == 1 && poMaster.IsApprovedByParentApprovedUser == 2 && poMaster.ParentApprovedByName != "" && poMaster.ParentApprovedByName != null) {
                    lblParentApprovalText.InnerHtml = "Parent Approved User: REJECTED";
                }*/
                decimal Total = 0;
                for (int i = 0; i < poMaster.PoDetails.Count; i++) {
                    if (poMaster.PurchaseType == 2) {
                        if (poMaster.PoDetails[i].PoPurchaseType == 1) {
                            PanenImports.Visible = false;
                            pnlLogo.Visible = false;
                        }

                        if (poMaster.PoDetails[i].PoPurchaseType == 2) {

                            poMaster.PoDetails[i].SubTotal = poMaster.PoDetails[i].UnitPriceForeign * poMaster.PoDetails[i].Quantity;
                            poMaster.PoDetails[i].VatAmount = 0;
                            poMaster.PoDetails[i].TotalAmount = poMaster.PoDetails[i].SubTotal + poMaster.PoDetails[i].VatAmount;
                            poMaster.VatAmount = 0;
                            Total = Total + poMaster.PoDetails[i].SubTotal;
                            poMaster.TotalAmount = Total;
                        }

                    }
                }
                gvPoItems.DataSource = poMaster.PoDetails;
                gvPoItems.DataBind();

                tdSubTotal.InnerHtml = poMaster.PoDetails.Sum(pd => pd.SubTotal).ToString("N2");
                //tdNbt.InnerHtml = poMaster.NBTAmount.ToString("N2");
                tdVat.InnerHtml = poMaster.VatAmount.ToString("N2");
                tdNetTotal.InnerHtml = poMaster.TotalAmount.ToString("N2");
                ViewState["PoMaster"] = new JavaScriptSerializer().Serialize(poMaster);
                ViewState["PoMaster"] = new JavaScriptSerializer().Serialize(poMaster);
            }
        }
        protected void gvPOItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Header) {
                if (ViewState["PurchaseType"].ToString() != "2") {

                    e.Row.Cells[4].CssClass = "hidden";
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow) {
                if (ViewState["PurchaseType"].ToString() != "2") {

                    e.Row.Cells[4].CssClass = "hidden";
                }
            }
        }
        protected void lbtnViewPO_Click(object sender, EventArgs e) {
            int PoId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);

            Response.Redirect("ViewPO.aspx?PoId=" + PoId);
        }

        protected void lbtnViewGrn_Click(object sender, EventArgs e) {
            int GrnId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);

            Response.Redirect("CompanyGrnReportView.aspx?PoID=0&GrnId=" + GrnId);
        }

        protected void btnApprove_Click(object sender, EventArgs e) {
            POMaster newPo = new JavaScriptSerializer().Deserialize<POMaster>(ViewState["PoMaster"].ToString());
            string Remark = txtRemarks.Text;
            int result = 0;

            if (newPo.IsDerived == 0) {
              //  result = pOMasterController.ApproveGeneralPO(newPo.PoID, int.Parse(Session["UserId"].ToString()), hdnRemarks.Value.ProcessString(), ddlPaymentMethod.SelectedValue != "" ? int.Parse(ddlPaymentMethod.SelectedValue) : 0, Remark);
            }
            else {
               // result = pOMasterController.ParentApprovePO(newPo.PoID, hdnRemarks.Value.ProcessString(), ddlPaymentMethod.SelectedValue != "" ? int.Parse(ddlPaymentMethod.SelectedValue) : 0, int.Parse(ViewState["PoType"].ToString()), int.Parse(Session["UserId"].ToString()), newPo.IsApprovedByParentApprovedUser, Remark);

            }


            if (result > 0) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved'}).then((result) => { window.location = '" + Request.QueryString.Get("Redirect") + ".aspx' }); });   </script>", false);
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Approving PO', showConfirmButton: false,timer: 3000}); });   </script>", false);
            }

        }

        protected void btnReject_Click(object sender, EventArgs e) {
            POMaster po = new JavaScriptSerializer().Deserialize<POMaster>(ViewState["PoMaster"].ToString());
            int result = 0;

            if (po.IsDerived == 0) {
                result = pOMasterController.RejectGeneralPO(po.PoID, int.Parse(Session["UserId"].ToString()), hdnRemarks.Value.ProcessString(), 0);
            }
            else {
               // result = pOMasterController.ParentRejectPO(po.PoID, hdnRemarks.Value.ProcessString(), ddlPaymentMethod.SelectedValue != "" ? int.Parse(ddlPaymentMethod.SelectedValue) : 0, int.Parse(ViewState["PoType"].ToString()), int.Parse(Session["UserId"].ToString()), po.IsApprovedByParentApprovedUser, hdnRejectionAction.Value != "" ? int.Parse(hdnRejectionAction.Value) : 0, po.IsDerivedFromPo);

            }
            if (result > 0) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved'}).then((result) => { window.location = '" + Request.QueryString.Get("Redirect") + ".aspx' }); });   </script>", false);
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Approving PO', showConfirmButton: false,timer: 3000}); });   </script>", false);
            }

        }

        //protected void ddlPaymentMethod_SelectedIndexChanged(object sender, EventArgs e) {
        //    if (ddlPaymentMethod.SelectedValue == "4") {
        //        btnInvoice.Visible = true;
        //    }
        //    else {
        //        btnInvoice.Visible = false;
        //    }
        //}

        protected void btnInvoice_Click(object sender, EventArgs e) {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlInvDetails').modal('show'); });   </script>", false);

        }

        protected void btnAdd_Click(object sender, EventArgs e) {
            try {
                Random r = new Random();
                if (ViewState["InvoiceDetails"] == null) {
                    List<InvoiceDetails> invoiceDetails = new List<InvoiceDetails>();
                    
                    InvoiceDetails newDetail = new InvoiceDetails();
                    newDetail.num = r.Next();
                    //newDetail.InvoiceNo = txtInvNo.Text;
                    //newDetail.InvoiceDate = DateTime.Parse(txtDate.Text);
                    //newDetail.InvoiceAmount = decimal.Parse(txtAmount.Text);
                    //newDetail.IsPaymentSettled = ChkPayment.Checked == true ? 1 : 0;
                    //newDetail.VatNo = txtVatNo.Text;

                    invoiceDetails.Add(newDetail);
                    ViewState["InvoiceDetails"] = new JavaScriptSerializer().Serialize(invoiceDetails);
                }
                else {
                    List<InvoiceDetails> invoiceDetails = new JavaScriptSerializer().Deserialize<List<InvoiceDetails>>(ViewState["InvoiceDetails"].ToString());
                    InvoiceDetails newDetail = new InvoiceDetails();

                    newDetail.num = r.Next();
                    //newDetail.InvoiceNo = txtInvNo.Text;
                    //newDetail.InvoiceDate = DateTime.Parse(txtDate.Text);
                    //newDetail.InvoiceAmount = decimal.Parse(txtAmount.Text);
                    //newDetail.IsPaymentSettled = ChkPayment.Checked == true ? 1 : 0;
                    //newDetail.VatNo = txtVatNo.Text;

                    invoiceDetails.Add(newDetail);
                    ViewState["InvoiceDetails"] = new JavaScriptSerializer().Serialize(invoiceDetails);
                }

                //gvAddedInvDetails.Visible = true;
                //btnDone.Visible = true;
                
                //gvAddedInvDetails.DataSource = new JavaScriptSerializer().Deserialize<List<InvoiceDetails>>(ViewState["InvoiceDetails"].ToString());
                //gvAddedInvDetails.DataBind();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {$('div').removeClass('modal-backdrop'); $('#mdlInvDetails').modal('show'); });   </script>", false);

                //txtInvNo.Text = "";
                //txtDate.Text = "";
                //txtAmount.Text = "";
                //txtVatNo.Text = "";
                //ChkPayment.Checked = false;
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

            
            //gvAddedInvDetails.DataSource = invoiceDetails.Where(x => x.status != 2);
            //gvAddedInvDetails.DataBind();
            ViewState["InvoiceDetails"] = new JavaScriptSerializer().Serialize(invoiceDetails);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {$('div').removeClass('modal-backdrop'); $('#mdlInvDetails').modal('show'); });   </script>", false);

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
        protected void btnDone_Click(object sender, EventArgs e) {
            //string InvoiceNo = txtInvNo.Text;
            //DateTime Date = DateTime.Parse(txtDate.Text);
            //decimal Amount = decimal.Parse(txtAmount.Text);
            //string vatNo = txtVatNo.Text;
            //int IsPayementSettled = ChkPayment.Checked == true ? 1 : 0;
            List<InvoiceDetails> invoiceDetails = new JavaScriptSerializer().Deserialize<List<InvoiceDetails>>(ViewState["InvoiceDetails"].ToString());

            //int paymentType = int.Parse(ddlPaymentMethod.SelectedValue);
            int PoId = int.Parse(Request.QueryString.Get("PoId"));

            int Result = 0;
            for (int i = 0; i < invoiceDetails.Count; i++) {
             //Result = invoiceDetailsController.SaveInvoiceDetailsInPO(PoId, 0, paymentType, invoiceDetails[i].InvoiceNo, invoiceDetails[i].InvoiceDate, invoiceDetails[i].InvoiceAmount, invoiceDetails[i].VatNo, invoiceDetails[i].IsPaymentSettled); 
            }

            if (Result > 0) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('div').removeClass('modal-backdrop'); $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved'}) });   </script>", false);
                //ddlPaymentMethod.SelectedValue = paymentType.ToString();
                //gvAddedInvDetails.DataSource = null;
                //gvAddedInvDetails.DataBind();

                ViewState["InvoiceDetails"] = null;
                //btnDone.Visible = false;
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