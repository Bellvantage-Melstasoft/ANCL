using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class PhysicalStockApproval : System.Web.UI.Page {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        PhysicalStockVerificationMasterController physicalStockVerificationMasterController = ControllerFactory.CreatePhysicalStockVerificationMaster();
        InventoryControllerInterface InventoryControllerInterface = ControllerFactory.CreateInventoryController();
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["UserId"] == null) {
                if (ViewState["UserId"] == null || ViewState["UserId"].ToString() == string.Empty) {

                    Response.Redirect("LoginPage.aspx");
                }
                else {
                    Session["CompanyId"] = ViewState["CompanyId"].ToString();
                    Session["UserId"] = ViewState["UserId"].ToString();
                }
            }

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefWarehouse";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabWarehouse";
                ((BiddingAdmin)Page.Master).subTabValue = "PhysicalStockApproval.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "StockVerifcationApprovalLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();



                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 10,6) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Count() > 0) {

                    try {
                        ddlWarehouse.DataSource = ControllerFactory.CreateWarehouseController().getWarehouseDetailsByWarehouseId((Session["UserWarehouses"] as List<UserWarehouse>).Select(d => d.WrehouseId).ToList());
                        ddlWarehouse.DataValueField = "WarehouseID";
                        ddlWarehouse.DataTextField = "Location";
                        ddlWarehouse.DataBind();
                        ddlWarehouse.Items.Insert(0, new ListItem("Select a Warehouse", ""));

                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }

                else {
                    try {
                        ddlWarehouse.DataSource = ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
                        ddlWarehouse.DataValueField = "WarehouseID";
                        ddlWarehouse.DataTextField = "Location";
                        ddlWarehouse.DataBind();
                        ddlWarehouse.Items.Insert(0, new ListItem("Select a Warehouse", ""));


                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }

                txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");

            }


        }

        protected void btnRefresh_Click(object sender, EventArgs e) {

            try {
                DataTable dt = new DataTable();

                dt.Columns.Add("ItemId");
                dt.Columns.Add("ReferenceNo");
                dt.Columns.Add("ItemName");
                dt.Columns.Add("MeasurementShortName");
                dt.Columns.Add("AvailbleQty");
                dt.Columns.Add("StockValue");
                dt.Columns.Add("PhysicalAvailableQty");
                dt.Columns.Add("PhysicalstockValue");
                dt.Columns.Add("Remarks");
                dt.Columns.Add("IsModified");
                dt.Columns.Add("PSVDId");
                dt.Columns.Add("CanEdit");

                int warehouseId = int.Parse(ddlWarehouse.SelectedValue);
                DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);

                PhysicalstockVerificationMaster stockVerificationMaster = physicalStockVerificationMasterController.GetCreatedApprovedDetails(warehouseId, date);

                if (stockVerificationMaster != null) {

                    ViewState["PSVMId"] = stockVerificationMaster.PSVMId;

                    pnlCreatedBy.Visible = true;
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                    lblCreatedByName.Text = stockVerificationMaster.CreatedByname;
                    lblCreatedDate.Text = stockVerificationMaster.CreatedDate.ToString("dd/MM/yyyy");

                    if (File.Exists(HttpContext.Current.Server.MapPath(stockVerificationMaster.CreatedSignature)))
                        imgCreatedBySignature.ImageUrl = stockVerificationMaster.CreatedSignature;
                    else
                        imgCreatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";

                    if (stockVerificationMaster.Approvalstatus != 0) {
                        pnlApprovedBy.Visible = true;
                        pnlRemarks.Visible = true;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        // btnExport.Visible = true;


                        lblApprovedByName.Text = stockVerificationMaster.ApprovedByname;
                        lblApprovedDate.Text = stockVerificationMaster.ApprovedDate.ToString("dd/MM/yyyy");
                        lblRemarks.Text = stockVerificationMaster.ApprovalRemarks;
                        if (File.Exists(HttpContext.Current.Server.MapPath(stockVerificationMaster.ApprovedSignature)))
                            imgApprovedBySignature.ImageUrl = stockVerificationMaster.ApprovedSignature;
                        else
                            imgApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                    }
                    else {
                        // btnExport.Visible = true;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;

                    }

                    if (stockVerificationMaster.Approvalstatus == 1) {
                        lblApprovalText.InnerHtml = "Approved By";
                    }
                    else if (stockVerificationMaster.Approvalstatus == 2) {
                        lblApprovalText.InnerHtml = "Rejected By";
                    }


                    for (int i = 0; i < stockVerificationMaster.PSVDetails.Count; i++) {
                        DataRow newRow = dt.NewRow();

                        newRow["ItemId"] = stockVerificationMaster.PSVDetails[i].ItemId.ToString();
                        newRow["ReferenceNo"] = stockVerificationMaster.PSVDetails[i].ReferenceNo.ToString();
                        newRow["ItemName"] = stockVerificationMaster.PSVDetails[i].ItemName;
                        newRow["MeasurementShortName"] = stockVerificationMaster.PSVDetails[i].MeasurementShortName;
                        newRow["AvailbleQty"] = stockVerificationMaster.PSVDetails[i].SysAvailableQty.ToString();
                        newRow["StockValue"] = stockVerificationMaster.PSVDetails[i].SysStockValue.ToString();
                        newRow["PhysicalAvailableQty"] = stockVerificationMaster.PSVDetails[i].PhysicalAvailableQty.ToString();
                        newRow["PhysicalstockValue"] = stockVerificationMaster.PSVDetails[i].PhysicalstockValue.ToString();
                        newRow["Remarks"] = stockVerificationMaster.PSVDetails[i].Remarks;
                        newRow["PSVDId"] = stockVerificationMaster.PSVDetails[i].PSVDId.ToString();
                        newRow["IsModified"] = stockVerificationMaster.PSVDetails[i].IsModified;
                        newRow["CanEdit"] = "0";

                        dt.Rows.Add(newRow);
                    }

                    gvStock.DataSource = dt;
                    gvStock.DataBind();
                }
                else {
                    gvStock.DataSource = null;
                    gvStock.DataBind();

                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }
                //else {

                //    List<DailyStockSummary> inventory = InventoryControllerInterface.GetMonthEndStock(int.Parse(Session["CompanyId"].ToString()), warehouseId, date);

                //    for (int i = 0; i < inventory.Count; i++) {
                //        DataRow newRow = dt.NewRow();

                //        newRow["ItemId"] = inventory[i].ItemId.ToString();
                //        newRow["ReferenceNo"] = inventory[i].ReferenceNo.ToString();
                //        newRow["ItemName"] = inventory[i].ItemName;
                //        newRow["MeasurementShortName"] = inventory[i].MeasurementShortName;
                //        newRow["AvailbleQty"] = inventory[i].AvailableQty.ToString();
                //        newRow["StockValue"] = inventory[i].StockValue.ToString();
                //        newRow["CanEdit"] = "1";

                //        dt.Rows.Add(newRow);
                //    }

                //    gvStock.DataSource = dt;
                //    gvStock.DataBind();


                //}


            }
            catch (Exception ex) {
                throw ex;
            }

        }
        protected void btnApprove_Click(object sender, EventArgs e) {
            try {
                int result = physicalStockVerificationMasterController.ApproveStock(int.Parse(ViewState["PSVMId"].ToString()), (int.Parse(Session["CompanyId"].ToString())), (int.Parse(Session["UserId"].ToString())), LocalTime.Now, hdnRemarks.Value);

                if (result > 0) {
                    //success
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'GRN approved Successfully.', showConfirmButton: false,timer: 1500}).then((result) => {window.location = 'PhysicalStockVerification.aspx'}); });   </script>", false);
                }
                else {
                    //failed
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on approving Stock'}); });   </script>", false);
                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }
        protected void btnReject_Click(object sender, EventArgs e) {
            try {

                int result = physicalStockVerificationMasterController.RejectStock(int.Parse(ViewState["PSVMId"].ToString()), 2, (int.Parse(Session["UserId"].ToString())), LocalTime.Now, hdnRemarks.Value);

                if (result > 0) {
                    //success
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'GRN approved Successfully.', showConfirmButton: false,timer: 1500}).then((result) => {window.location = 'PhysicalStockVerification.aspx'}); });   </script>", false);
                }
                else {
                    //failed
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on approving Stock'}); });   </script>", false);
                }

            }
            catch (Exception ex) {
                throw ex;
            }


        }
    }
}