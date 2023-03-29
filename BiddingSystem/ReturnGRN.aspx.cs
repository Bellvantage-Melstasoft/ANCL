using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class ReturnGRN : System.Web.UI.Page
    {
        GrnController grnController = ControllerFactory.CreateGrnController();
        GRNDetailsController gRNDetailsController = ControllerFactory.CreateGRNDetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();




        protected void Page_Load(object sender, EventArgs e)
        {
            int CompanyId, GrnID = 0;
            string UserId;

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ReturnGRN.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "CustomerGRNReturnLink";

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();

                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 29) && companyLogin.Usertype != "S")
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

                    
                    VAT_NBT LatestVatNbt = generalSettingsController.getLatestVatNbt();
                    var vatValue = LatestVatNbt.VatRate;
                    var nbtVal1 = LatestVatNbt.NBTRate1;
                    var nbtVal2 = LatestVatNbt.NBTRate2;

                    hdnVatRate.Value = vatValue.ToString();
                    hdnNbtRate1.Value = nbtVal1.ToString();
                    hdnNbtRate2.Value = nbtVal2.ToString();

                    GrnMaster grnMaster = grnController.GetGrnMasterByGrnID(GrnID);

                    lblsupplierName.Text = grnMaster._Supplier.SupplierName;
                    lblSupplierAddress.Text = grnMaster._Supplier.Address1;
                    lblSupplierContact.Text = grnMaster._Supplier.OfficeContactNo + " / " + grnMaster._Supplier.PhoneNo;
                    ViewState["SupplierId"] = grnMaster._Supplier.SupplierId;
                    ViewState["IsApproved"] = grnMaster.IsApproved;

                    lblWarehouseName.Text = grnMaster._Warehouse.Location;
                    lblWarehouseAddress.Text = grnMaster._Warehouse.Address;
                    lblWarehouseContact.Text = grnMaster._Warehouse.PhoneNo;
                    lblStoreKeeper.Text = grnMaster._POMaster.StoreKeeper;
                    ViewState["WarehouseId"] = grnMaster._Warehouse.WarehouseID;

                    lblGrnCode.Text = grnMaster.GrnCode;
                    lblPOCode.Text = grnMaster.POCode;
                    lblPrCode.Text = grnMaster.PrCode;
                    lblReceiveddate.Text = grnMaster.GoodReceivedDate.ToString("yyyy-MM-dd");
                    lblPaymenttype.Text = grnMaster._POMaster.PaymentMethod == "1" ? "Cash" : grnMaster._POMaster.PaymentMethod == "2" ? "Cheque" : grnMaster._POMaster.PaymentMethod == "3" ? "Credit" : grnMaster._POMaster.PaymentMethod == "4" ? "Advanced Payment" : "-";
                    
                    decimal subTotal = grnMaster.TotalAmount - (grnMaster.TotalVat);
                    
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

                    List<GrnDetails> grnDetails = grnMaster._GrnDetailsList;
                    
                    
                    for (int i = 0; i < grnDetails.Count; i++) {
                        grnDetails[i].HasVat = 0;
                        if (grnDetails[i].VatAmount != 0) {
                            grnDetails[i].HasVat = 1;
                        }

                    }
                    gvPurchaseOrderItems.DataSource = grnDetails;
                    gvPurchaseOrderItems.DataBind();

                    hdnCanApprove.Value = grnController.ValidateGrnBeforeApprove(grnMaster.GrnId).ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        protected void btnReturnStock_Click(object sender, EventArgs e)
        {

            decimal SumSubTotal = 0;
            decimal SumVatTotal = 0;
            decimal SumNetTotal = 0;
          
            int MeasurementId = 0;
            int StockMaintainingType = 0;
            int IsApproved = 0;
            IsApproved = int.Parse(ViewState["IsApproved"].ToString());

            string Remark = txtRemarks.Text;
            List<GrnReturnDetails> GrnReturnDetailslist = new List<GrnReturnDetails>();

            for (int i = 0; i < gvPurchaseOrderItems.Rows.Count; i++) {
                GrnReturnDetails newList = new GrnReturnDetails();

                newList.ReturnedQty = decimal.Parse((gvPurchaseOrderItems.Rows[i].FindControl("txtQty") as TextBox).Text);
                newList.VatValue = decimal.Parse((gvPurchaseOrderItems.Rows[i].FindControl("txtVatNew") as TextBox).Text);
                newList.SubTotal = decimal.Parse((gvPurchaseOrderItems.Rows[i].FindControl("txtSubTotalNew") as TextBox).Text);
                newList.NetTotal = decimal.Parse((gvPurchaseOrderItems.Rows[i].FindControl("txtTotalNew") as TextBox).Text);
                newList.UnitPrice = decimal.Parse(gvPurchaseOrderItems.Rows[i].Cells[9].Text);
                newList.GrnId = int.Parse(gvPurchaseOrderItems.Rows[i].Cells[15].Text);
                newList.GrndId = int.Parse(gvPurchaseOrderItems.Rows[i].Cells[14].Text);
                newList.ItemId = int.Parse(gvPurchaseOrderItems.Rows[i].Cells[1].Text);
                newList.MeasurementId = int.Parse(gvPurchaseOrderItems.Rows[i].Cells[16].Text);
                newList.StockMaintainingType = int.Parse(gvPurchaseOrderItems.Rows[i].Cells[22].Text);
                newList.IsApproved = int.Parse(gvPurchaseOrderItems.Rows[i].Cells[23].Text);
                newList.podId = int.Parse(gvPurchaseOrderItems.Rows[i].Cells[26].Text);

                SumSubTotal = SumSubTotal + newList.SubTotal;
                SumVatTotal = SumVatTotal + newList.VatValue;
                SumNetTotal = SumNetTotal + newList.NetTotal;
                GrnReturnDetailslist.Add(newList);
            }

            int Result = 0;
            int StResult = 0;
            Result = ControllerFactory.CreateGrndReturnNoteController().UpdateReturnGrnNote(GrnReturnDetailslist, Remark, int.Parse(Session["UserId"].ToString()), int.Parse(ViewState["SupplierId"].ToString()), int.Parse(ViewState["WarehouseId"].ToString()), SumSubTotal, SumVatTotal, SumNetTotal, int.Parse(Request.QueryString["GrnId"].ToString()), int.Parse(ddlSupplierOption.SelectedValue));
             
            if (Result > 0) {
                if (IsApproved == 1) {
                    StResult  = ControllerFactory.CreateInventoryController().UpdateInventoryForGrnReturn(int.Parse(ViewState["WarehouseId"].ToString()), int.Parse(Session["UserId"].ToString()), GrnReturnDetailslist);

                    if (StResult > 0) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'CustomerGRNReturn.aspx' });;});   </script>", false);
                    }

                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'CustomerGRNReturn.aspx' });;});   </script>", false);

                }

            }

        }


    }
}