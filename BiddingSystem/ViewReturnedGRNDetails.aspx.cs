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
    public partial class ViewReturnedGRNDetails : System.Web.UI.Page
    {
        GrnController grnController = ControllerFactory.CreateGrnController();
        GRNDetailsController gRNDetailsController = ControllerFactory.CreateGRNDetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        GrnReturnMasterController grnReturnMasterController = ControllerFactory.CreateGrnReturnMasterController();
        GrnReturnDetailsController grnReturnDetailsController = ControllerFactory.CreateGrnReturnDetailsController();




        protected void Page_Load(object sender, EventArgs e)
        {
            int CompanyId, GrnReturnId = 0;
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

            if (Request.QueryString["GrnReturnId"] != null)
            {
                GrnReturnId = int.Parse(Request.QueryString["GrnReturnId"].ToString());
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

                    GrnReturnMaster GrnReturnMaster = grnReturnMasterController.GetReturnedGrnDetails(GrnReturnId);
                    GrnMaster grnMaster = grnController.GetGrnMasterByGrnID(GrnReturnMaster.GrnId);

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
                    txtRemarks.Text = GrnReturnMaster.Remark;
                    lblSupplierReturnOption.Text = GrnReturnMaster.SupplierReturnOption == 1 ? "Return Item" : "Return Cash";
                    //List<GrnDetails> grnDetails = grnMaster._GrnDetailsList;
                    List<GrnReturnDetails> grnReturnDetailsList = grnReturnDetailsController.GetReturndetails(GrnReturnId);
                    lblSubTotal.Text = GrnReturnMaster.SubTotalValue.ToString();
                    lblNetTotalNew.Text = GrnReturnMaster.NetTotalValue.ToString();
                    lblVatTotalNew.Text = GrnReturnMaster.VatTotalvalue.ToString();


                    //for (int i = 0; i < grnDetails.Count; i++) {
                    //    grnDetails[i].HasVat = 0;
                    //    if (grnDetails[i].VatAmount != 0) {
                    //        grnDetails[i].HasVat = 1;
                    //    }

                    //}
                    gvPurchaseOrderItems.DataSource = grnReturnDetailsList;
                    gvPurchaseOrderItems.DataBind();

                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

       

    }
}