using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class POPDF : System.Web.UI.Page {
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

        protected void Page_Load(object sender, EventArgs e) {
            int PoId = int.Parse(Request.QueryString.Get("PoId"));
            int CompanyId = int.Parse(Request.QueryString.Get("CompanyId").Split('?')[0]);


            POMaster pOMaster = pOMasterController.GetPoMasterObjByPoIdRaised(PoId, CompanyId);
            SupplierQuotation ImportDetails = supplierQuotationController.GetImportDetails(PoId, CompanyId);
            ViewState["PurchaseType"] = pOMaster.PurchaseType;

            ViewState["quationid"] = pOMaster.QuotationId;
            ViewState["basePr"] = pOMaster.BasePr;
            
            foreach (PODetails item in pOMaster._PODetails) {
                item.supplierQuotationItem = supplierQuotationController.GetQuotationItemsByQuotationItemId(item.QuotationItemId);
            }
            
            lblCompName.Text = pOMaster._companyDepartment.DepartmentName;
            lblCompVatNo.Text = pOMaster._companyDepartment.VatNo;
            lblTpNo.Text = pOMaster._companyDepartment.PhoneNO;
            lblFax.Text = pOMaster._companyDepartment.FaxNO;
            lblcompAdd.Text = pOMaster._companyDepartment.Address1;

            lblPurchaseType.Text = pOMaster.PurchaseType == 1 ? "Local" : "Import";
           

            lblSupName.Text = pOMaster._Supplier.SupplierName;
            lblSupplierAddress.Text = pOMaster._Supplier.Address1;
            lblSupplierContact.Text = pOMaster._Supplier.OfficeContactNo + " / " + pOMaster._Supplier.PhoneNo;

            lblWarehouseName.Text = pOMaster._Warehouse.Location;
            lblWarehouseAddress.Text = pOMaster._Warehouse.Address;
            lblWarehouseContact.Text = pOMaster._Warehouse.PhoneNo;

            lblPO.Text = pOMaster.POCode;
            lblPrCode.Text = "PR" + pr_MasterController.FetchApprovePRDataByPRId(int.Parse(ViewState["basePr"].ToString())).PrCode;
            lblQuotationFor.Text = pOMaster.Description;
           
            lblDate.Text = LocalTime.Today.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);

            //if (pOMaster.IsApproved == 0) {
            //    lblPending.Visible = true;
            //}
            //else if (pOMaster.IsApproved == 1) {
            //    lblApproved.Visible = true;
            //}
            //else {
            //    lblRejected.Visible = true;
            //}

            //if (pOMaster.IsDerived == 0) {
            //    lblGeneral.Visible = true;
            //}
            //else if (pOMaster.IsDerived == 1 && pOMaster.IsDerivedType == 1) {
            //    lblModified.Visible = true;
            //}
            //else {
            //    lblCovering.Visible = true;
            //}

            if (pOMaster.PaymentMethod != null && pOMaster.PaymentMethod != "") {
                lblPaymentType.Text = pOMaster.PaymentMethod == "1" ? "Cash Payment" : pOMaster.PaymentMethod == "2" ? "Cheque Payment" : "Credit";
                pnlPaymentMethod.Visible = true;
            }

            //tdSubTotal.InnerHtml = pOMaster._PODetails.Sum(pd => pd.SubTotal).ToString("N2");
            ////tdNbt.InnerHtml = pOMaster.NBTAmount.ToString("N2");
            //tdVat.InnerHtml = pOMaster.VatAmount.ToString("N2");
            //tdNetTotal.InnerHtml = pOMaster.TotalAmount.ToString("N2");


            if (pOMaster.PurchaseType == 2) {
                PanenImports.Visible = true;
                pnlLogo.Visible = true;

                lblCurrency.Text = ImportDetails.CurrencyShortname;

                lblPaymentMode.Text = ImportDetails.PaymentMode;
            }


            //lblCreatedByName.Text = pOMaster.CreatedByName;
            lblCreatedByDesignation.Text = pOMaster.CreatedDesignationName;
            lblCreatedDate.Text = pOMaster.CreatedDate.ToString("dd/MM/yyyy");


            if (File.Exists(HttpContext.Current.Server.MapPath(pOMaster.CreatedSignature)))
                imgCreatedBySignature.ImageUrl = pOMaster.CreatedSignature;
            else
                imgCreatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";

            if (pOMaster.IsApproved != 0) {
                pnlApprovedBy.Visible = true;
                lblApprovedByDesignation.Text = pOMaster.ApprovedDesignationName;
                //lblApprovedByName.Text = pOMaster.ApprovedByName;
                lblApprovedDate.Text = pOMaster.ApprovedDate.ToString("dd/MM/yyyy");
                lblApprovalRemarks.Text = pOMaster.ApprovalRemarks;
                if (File.Exists(HttpContext.Current.Server.MapPath(pOMaster.ApprovedSignature)))
                    imgApprovedBySignature.ImageUrl = pOMaster.ApprovedSignature;
                else
                    imgApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
            }

            if (pOMaster.IsApproved == 1) {
                lblApprovalText.InnerHtml = "Approved By";
            }
            else if (pOMaster.IsApproved == 2) {
                lblApprovalText.InnerHtml = "Rejected By";
            }


            if (pOMaster.IsDerived == 1 && pOMaster.IsApprovedByParentApprovedUser != 0 && pOMaster.ParentApprovedByName != "" && pOMaster.ParentApprovedByName != null) {
                pnlParentApprovedByDetails.Visible = true;
                lblParentApprovalRemarks.Text = pOMaster.ParentApprovedUserApprovalRemarks;
                //lblParentApprovedByName.Text = pOMaster.ParentApprovedByName;
                lblParentApprovedByDesignation.Text = pOMaster.ParentApprovedDesignationName;
                lblParentApprovedDate.Text = pOMaster.ParentApprovedUserApprovalDate.ToString("dd/MM/yyyy");

                if (File.Exists(HttpContext.Current.Server.MapPath(pOMaster.ParentApprovedBySignature)))
                    imgParentApprovedBySignature.ImageUrl = pOMaster.ParentApprovedBySignature;
                else
                    imgParentApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
            }

            if (pOMaster.IsDerived == 1 && pOMaster.IsApprovedByParentApprovedUser == 1 && pOMaster.ParentApprovedByName != "" && pOMaster.ParentApprovedByName != null) {
                lblParentApprovalText.InnerHtml = "Parent Approved User: APPROVED";
            }
            else if (pOMaster.IsDerived == 1 && pOMaster.IsApprovedByParentApprovedUser == 2 && pOMaster.ParentApprovedByName != "" && pOMaster.ParentApprovedByName != null) {
                lblParentApprovalText.InnerHtml = "Parent Approved User: REJECTED";
            }

            int VarPoPurchaseType = 0;
            string VarSupplierAgentName = "";

            decimal Total = 0;
            for (int i = 0; i < pOMaster._PODetails.Count; i++) {
                VarPoPurchaseType = pOMaster._PODetails[i].PoPurchaseType;
                VarSupplierAgentName = pOMaster._PODetails[i].SupplierAgentName;

                if (pOMaster.PurchaseType == 2) {
                    if (pOMaster._PODetails[i].PoPurchaseType == 1) {
                        PanenImports.Visible = false;
                        pnlLogo.Visible = false;
                    }
                    if (pOMaster._PODetails[i].PoPurchaseType == 2) {

                        pOMaster._PODetails[i].SubTotal = pOMaster._PODetails[i].UnitPriceForeign * pOMaster._PODetails[i].Quantity;
                        pOMaster._PODetails[i].VatAmount = 0;
                        pOMaster._PODetails[i].TotalAmount = pOMaster._PODetails[i].SubTotal + pOMaster._PODetails[i].VatAmount;
                        pOMaster.VatAmount = 0;
                        Total = Total + pOMaster._PODetails[i].SubTotal;
                        pOMaster.TotalAmount = Total;
                    }
                }
            }
            gvPoItems.DataSource = pOMaster._PODetails;
            gvPoItems.DataBind();

            tdSubTotal.InnerHtml = pOMaster._PODetails.Sum(pd => pd.SubTotal).ToString("N2");
            //tdNbt.InnerHtml = pOMaster.NBTAmount.ToString("N2");
            tdVat.InnerHtml = pOMaster.VatAmount.ToString("N2");
            tdNetTotal.InnerHtml = pOMaster.TotalAmount.ToString("N2");

            lblPoPurchaseType.Text = VarPoPurchaseType == 1 ? "Local" : "Import";
            lblAgentName.Text = VarSupplierAgentName;
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

    }
}