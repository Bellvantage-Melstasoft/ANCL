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

    public partial class PhysicalStockVerification : System.Web.UI.Page {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        PhysicalStockverificationDetailsController PhysicalSVDetailsController = ControllerFactory.CreatePhysicalStockverificationDetails();
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
                ((BiddingAdmin)Page.Master).subTabValue = "PhysicalStockVerification.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "StockVerifcationLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();



                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()),10, 5) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
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
                    btnSave.Visible = false;
                    ViewState["PSVMId"] = stockVerificationMaster.PSVMId;

                    pnlCreatedBy.Visible = true;
                    lblCreatedByName.Text = stockVerificationMaster.CreatedByname;
                    lblCreatedDate.Text = stockVerificationMaster.CreatedDate.ToString("dd/MM/yyyy");

                    if (File.Exists(HttpContext.Current.Server.MapPath(stockVerificationMaster.CreatedSignature)))
                        imgCreatedBySignature.ImageUrl = stockVerificationMaster.CreatedSignature;
                    else
                        imgCreatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";

                    if (stockVerificationMaster.Approvalstatus != 0) {
                        pnlApprovedBy.Visible = true;
                        pnlRemarks.Visible = true;
                       // btnPrint.Visible = true;
                        btnUpdate.Visible = false;
                        lblApprovedByName.Text = stockVerificationMaster.ApprovedByname;
                        lblApprovedDate.Text = stockVerificationMaster.ApprovedDate.ToString("dd/MM/yyyy");
                        lblRemarks.Text = stockVerificationMaster.ApprovalRemarks;
                        if (File.Exists(HttpContext.Current.Server.MapPath(stockVerificationMaster.ApprovedSignature)))
                            imgApprovedBySignature.ImageUrl = stockVerificationMaster.ApprovedSignature;
                        else
                            imgApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                    }
                    else {
                        //btnPrint.Visible = true;
                        btnUpdate.Visible = true;
                    }

                    if (stockVerificationMaster.Approvalstatus == 1) {
                        lblApprovalText.InnerHtml = "Approved By";

                        btnResubmit.Visible = false;
                    }
                    else if (stockVerificationMaster.Approvalstatus == 2) {
                        lblApprovalText.InnerHtml = "Rejected By";

                        btnResubmit.Visible = true;
                    }
                    else {

                        btnResubmit.Visible = false;
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
                        if (stockVerificationMaster.Approvalstatus == 0) {
                            newRow["CanEdit"] = "1";
                        }
                        else {
                            newRow["CanEdit"] = "0";
                        }
                        dt.Rows.Add(newRow);
                    }

                    gvStock.DataSource = dt;
                    gvStock.DataBind();
                }
                else {

                    List<DailyStockSummary> inventory = InventoryControllerInterface.GetMonthEndStock(int.Parse(Session["CompanyId"].ToString()), warehouseId, date);

                    for (int i = 0; i < inventory.Count; i++) {
                        DataRow newRow = dt.NewRow();

                        newRow["ItemId"] = inventory[i].ItemId.ToString();
                        newRow["ReferenceNo"] = inventory[i].ReferenceNo.ToString();
                        newRow["ItemName"] = inventory[i].ItemName;
                        newRow["MeasurementShortName"] = inventory[i].MeasurementShortName;
                        newRow["AvailbleQty"] = inventory[i].AvailableQty.ToString();
                        newRow["StockValue"] = inventory[i].StockValue.ToString();
                        newRow["CanEdit"] = "1";

                        dt.Rows.Add(newRow);
                    }

                    gvStock.DataSource = dt;
                    gvStock.DataBind();

                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    btnResubmit.Visible = false;
                }


            }
            catch (Exception ex) {
                throw ex;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e) {
            try {
                int warehouseId = int.Parse(ddlWarehouse.SelectedValue);
                DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);

                PhysicalstockVerificationMaster physicalstMaster = new PhysicalstockVerificationMaster();

                physicalstMaster.WarehouseId = warehouseId;
                physicalstMaster.Month = date.ToString();
                physicalstMaster.CreatedDate = LocalTime.Now;
                physicalstMaster.CreatedBy = int.Parse(Session["UserId"].ToString());
                physicalstMaster.Approvalstatus = 0;

                physicalstMaster.PSVDetails = new List<physicalStockVerificationDetails>();

                for (int i = 0; i < gvStock.Rows.Count; i++) {
                    physicalStockVerificationDetails physicalStDetails = new physicalStockVerificationDetails();

                    physicalStDetails.PSVMId = physicalstMaster.PSVMId;
                    physicalStDetails.ItemId = int.Parse(gvStock.Rows[i].Cells[0].Text);
                    physicalStDetails.SysAvailableQty = decimal.Parse(gvStock.Rows[i].Cells[4].Text); 
                    physicalStDetails.SysStockValue = decimal.Parse(gvStock.Rows[i].Cells[5].Text); 

                    if ((gvStock.Rows[i].Cells[6].FindControl("txtPhysicalStQty") as TextBox).Text == "") {
                        physicalStDetails.IsModified = 0;
                    }
                    else {
                        physicalStDetails.PhysicalAvailableQty = decimal.Parse((gvStock.Rows[i].Cells[6].FindControl("txtPhysicalStQty") as TextBox).Text);
                        physicalStDetails.PhysicalstockValue = decimal.Parse((gvStock.Rows[i].Cells[7].FindControl("txtPhysicalStValue") as TextBox).Text);
                        physicalStDetails.Remarks = (gvStock.Rows[i].Cells[8].FindControl("txtRemarks") as TextBox).Text;
                        physicalStDetails.IsModified = 1;
                    }


                    physicalstMaster.PSVDetails.Add(physicalStDetails);
                }
                int result = physicalStockVerificationMasterController.SaveStock(physicalstMaster);

                if (result > 0) {
                    //success
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'GRN Created Successfully.', showConfirmButton: false,timer: 1500}).then((result) => {window.location = 'PhysicalStockVerification.aspx'}); });   </script>", false);
                }
                else {
                    //failed
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on Creating Stock'}); });   </script>", false);
                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e) {
            try {
                int warehouseId = int.Parse(ddlWarehouse.SelectedValue);
                DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);

                PhysicalstockVerificationMaster physicalstMaster = new PhysicalstockVerificationMaster();

                physicalstMaster.PSVMId = int.Parse(ViewState["PSVMId"].ToString());

                physicalstMaster.PSVDetails = new List<physicalStockVerificationDetails>();


                for (int i = 0; i < gvStock.Rows.Count; i++) {
                    physicalStockVerificationDetails physicalStDetails = new physicalStockVerificationDetails();

                    physicalStDetails.PSVDId = int.Parse(gvStock.Rows[i].Cells[9].Text);
                    physicalStDetails.ItemId = int.Parse(gvStock.Rows[i].Cells[0].Text);
                    physicalStDetails.SysAvailableQty = decimal.Parse(gvStock.Rows[i].Cells[4].Text);
                    physicalStDetails.SysStockValue = decimal.Parse(gvStock.Rows[i].Cells[5].Text);
                    physicalStDetails.IsModified = int.Parse(gvStock.Rows[i].Cells[10].Text);


                    if ((gvStock.Rows[i].Cells[6].FindControl("txtPhysicalStQty") as TextBox).Text == "") {
                        physicalStDetails.IsModified = 0;
                    }
                    else {
                        physicalStDetails.PhysicalAvailableQty = decimal.Parse((gvStock.Rows[i].Cells[6].FindControl("txtPhysicalStQty") as TextBox).Text);
                        physicalStDetails.PhysicalstockValue = decimal.Parse((gvStock.Rows[i].Cells[7].FindControl("txtPhysicalStValue") as TextBox).Text);
                        physicalStDetails.Remarks = (gvStock.Rows[i].Cells[8].FindControl("txtRemarks") as TextBox).Text;
                        physicalStDetails.IsModified = 1;
                    }


                    physicalstMaster.PSVDetails.Add(physicalStDetails);

                }
                int result = physicalStockVerificationMasterController.UpdateStock(physicalstMaster);

                if (result > 0) {
                    //success
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'GRN updated Successfully.', showConfirmButton: false,timer: 1500}).then((result) => {window.location = 'PhysicalStockVerification.aspx'}); });   </script>", false);
                }
                else {
                    //failed
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on updating Stock'}); });   </script>", false);
                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void btnResubmit_Click(object sender, EventArgs e) {
            try {

                List<physicalStockVerificationDetails> psvDetails = PhysicalSVDetailsController.GetPSVDetails(int.Parse(ViewState["PSVMId"].ToString()));

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

                for (int i = 0; i < psvDetails.Count; i++) {
                    DataRow newRow = dt.NewRow();

                    newRow["ItemId"] = psvDetails[i].ItemId.ToString();
                    newRow["ReferenceNo"] = psvDetails[i].ReferenceNo.ToString();
                    newRow["ItemName"] = psvDetails[i].ItemName;
                    newRow["MeasurementShortName"] = psvDetails[i].MeasurementShortName;
                    newRow["AvailbleQty"] = psvDetails[i].SysAvailableQty.ToString();
                    newRow["StockValue"] = psvDetails[i].PhysicalstockValue.ToString();
                    newRow["PhysicalAvailableQty"] = psvDetails[i].PhysicalAvailableQty.ToString();
                    newRow["PhysicalstockValue"] = psvDetails[i].PhysicalstockValue.ToString();
                    newRow["Remarks"] = psvDetails[i].Remarks.ToString();
                    newRow["IsModified"] = psvDetails[i].IsModified.ToString();
                    newRow["PSVDId"] = psvDetails[i].PSVDId.ToString();
                    newRow["CanEdit"] = "1";

                    dt.Rows.Add(newRow);
                }

                gvStock.DataSource = dt;
                gvStock.DataBind();

                btnResubmit.Visible = false;
                btnUpdate.Visible = true;
            

            }
            catch (Exception ex) {
                throw ex;
            }

        }
    }
}
