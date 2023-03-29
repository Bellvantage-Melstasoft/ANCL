using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.IO;
using System.Web;
using System.Web.UI;

namespace BiddingSystem
{
    public partial class ApproveGRN : System.Web.UI.Page
    {
        GrnController grnController = ControllerFactory.CreateGrnController();
        GRNDetailsController gRNDetailsController = ControllerFactory.CreateGRNDetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();

        protected void Page_Load(object sender, EventArgs e)
        {
            int CompanyId, GrnID = 0;
            string UserId;

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CustomerGRNView.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "grnApprovalLink";

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();

                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 10) && companyLogin.Usertype != "S")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

            if (Request.QueryString["GrnId"] != null)
            {
                GrnID = int.Parse(Request.QueryString["GrnId"].ToString());
            }
            else
            {
                Response.Redirect("CustomerGRNView.aspx");
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
                    var PrId = grnMaster.PoId;
                    lblReceiveddate.Text = grnMaster.GoodReceivedDate.ToString("yyyy-MM-dd");
                    lblgrnComment.Text = grnMaster.ApprovalRemaks;
                    lblgrnNote.Text = grnMaster.GrnNote;
                    lblPaymenttype.Text = grnMaster._POMaster.PaymentMethod == "1" ? "Cash" : grnMaster._POMaster.PaymentMethod == "2" ? "Cheque" : grnMaster._POMaster.PaymentMethod == "3" ? "Credit" : grnMaster._POMaster.PaymentMethod == "4" ? "Advanced Payment" : "-";


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
                    //lblquotationfor.Text = grnMaster.QuotationFor;
                    lblPurchaseType.Text = grnMaster._POMaster.PurchaseType == 1 ? "Local" : "Import";
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
                    }
                    else
                    {
                        lblRejected.Visible = true;
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

                    hdnCanApprove.Value = grnController.ValidateGrnBeforeApprove(grnMaster.GrnId).ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {

            int ItemId = 0;
            for (int i = 0; i < gvPurchaseOrderItems.Rows.Count; i++) {
                ItemId = int.Parse(gvPurchaseOrderItems.Rows[0].Cells[1].Text);
            }

            // int ClonedPrId = pR_MasterController.GetParentPRId(int.Parse(Request.QueryString["GrnId"].ToString()), ItemId);
            GrnMaster Grn = grnController.GetGrnMasterByGrnID(int.Parse(Request.QueryString["GrnId"].ToString()));
            int ClonedPrId = Grn.ClonedCoveringPR;
            if (ClonedPrId > 0) {
                int PoId = pOMasterController.GetPoId(ClonedPrId);
                if (PoId > 0) {
                    int count = pOMasterController.ApprovedCoveringPOCount(ClonedPrId);
                    if (count > 0) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'You have a coverig PO for this GRN', showConfirmButton: false,timer: 3000}); });   </script>", false);

                    }
                    else {
                        //int result = 0;
                        int result = grnController.ApproveGrn(int.Parse(Request.QueryString["GrnId"].ToString()), int.Parse(Session["UserId"].ToString()), hdnRemarks.Value);
                        if (result > 0) {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved'}).then((result) => { window.location = 'CustomerGRNView.aspx' }); });   </script>", false);
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Approving GRN', showConfirmButton: false,timer: 3000}); });   </script>", false);
                        }
                    }

                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'You have a coverig PR for this GRN', showConfirmButton: false,timer: 3000}); });   </script>", false);

                }


            }

            else {
                //int result = 0;
                int result = grnController.ApproveGrn(int.Parse(Request.QueryString["GrnId"].ToString()), int.Parse(Session["UserId"].ToString()), hdnRemarks.Value);
                if (result > 0) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved'}).then((result) => { window.location = 'CustomerGRNView.aspx' }); });   </script>", false);
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Approving GRN', showConfirmButton: false,timer: 3000}); });   </script>", false);
                }
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            int ItemId = 0;
            for (int i = 0; i < gvPurchaseOrderItems.Rows.Count; i++) {
                ItemId = int.Parse(gvPurchaseOrderItems.Rows[0].Cells[1].Text);
            }

            int ClonedPrId = pR_MasterController.GetParentPRId(int.Parse(Request.QueryString["GrnId"].ToString()), ItemId);

            if (ClonedPrId > 0) {
                int PoId = pOMasterController.GetPoId(ClonedPrId);
                if (PoId > 0) {
                    int count = pOMasterController.ApprovedCoveringPOCount(ClonedPrId);
                    if (count > 0) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'You have a coverig PO for this GRN', showConfirmButton: false,timer: 3000}); });   </script>", false);

                    }
                    else {
                        int result = grnController.RejectGrn(int.Parse(Request.QueryString["GrnId"].ToString()), int.Parse(Session["UserId"].ToString()), hdnRemarks.Value);
                        if (result > 0) {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved'}).then((result) => { window.location = 'CustomerGRNView.aspx' }); });   </script>", false);
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Rejecting GRN', showConfirmButton: false,timer: 3000}); });   </script>", false);
                        }
                    }

                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'You have a coverig PR for this GRN', showConfirmButton: false,timer: 3000}); });   </script>", false);

                }


            }

            else {
                int result = grnController.RejectGrn(int.Parse(Request.QueryString["GrnId"].ToString()), int.Parse(Session["UserId"].ToString()), hdnRemarks.Value);
                if (result > 0) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved'}).then((result) => { window.location = 'CustomerGRNView.aspx' }); });   </script>", false);
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Rejecting GRN', showConfirmButton: false,timer: 3000}); });   </script>", false);
                }
            }
            

        }
    }
}