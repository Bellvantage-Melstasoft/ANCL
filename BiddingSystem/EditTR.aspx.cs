using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class EditTR : System.Web.UI.Page {
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        UnitMeasurementController unitMeasurementController = ControllerFactory.CreateUnitMeasurementController();
        UserSubDepartmentController userSubDepartment = ControllerFactory.CreateUserSubDepartment();
        DepartmentWarehouseController departmentWarehouseController = ControllerFactory.CreateDepartmentWarehouse();
        TRMasterController tRMasterController = ControllerFactory.CreateTRMasterController();
        TRDetailsController tRDetailsController = ControllerFactory.CreateTRDetailsController();
        UserWarehouseController userWarehouseController = ControllerFactory.CreateUserWarehouse();
        WarehouseControllerInterface warehouseController = ControllerFactory.CreateWarehouseController();
        JavaScriptSerializer serializer = new JavaScriptSerializer();


        protected void Page_Load(object sender, EventArgs e) {
            // HiddenField2.Value = "test";
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "") {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefTR";
                ((BiddingAdmin)Page.Master).subTabTitle = "hrefTR";
                ((BiddingAdmin)Page.Master).subTabValue = "EditTR.aspx";
               // ((BiddingAdmin)Page.Master).subTabId = "viewMRNLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                //if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 12, 2) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                //    Response.Redirect("AdminDashboard.aspx");
                //}
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            msg.Visible = false;
            if (!IsPostBack) {
                try {
                    LoadWarehouses();
                    LoadDDLMainCatregory();
                    var TR = tRMasterController.getTRM(int.Parse(Request["id"].ToString()));

                    ViewState["TR"] = new JavaScriptSerializer().Serialize(TR);
                    if (TR != null) {
                        dtExpectedDate.Text = TR.ExpectedDate.ToString("MM/dd/yyyy");
                        txtTrDescription.Text = TR.Description;
                        txtTrCode.Text = "TR" + TR.TrCode;

                        DataTable dt = new DataTable();

                        dt.Columns.Add("TRDId");
                        dt.Columns.Add("MainCategoryId");
                        dt.Columns.Add("MainCategoryName");
                        dt.Columns.Add("SubCategoryId");
                        dt.Columns.Add("SubcategoryName");
                        dt.Columns.Add("ItemId");
                        dt.Columns.Add("ItemName");
                        dt.Columns.Add("ItemQuantity");
                        dt.Columns.Add("ItemDescription");
                        dt.Columns.Add("MeasurementId");

                        foreach (TR_Details TRD in TR.TRDetails) {
                            var dataRow = dt.NewRow();
                            dataRow[0] = TRD.TRDId;
                            dataRow[1] = TRD.CategoryID;
                            dataRow[2] = TRD.CategoryName;
                            dataRow[3] = TRD.SubCategoryID;
                            dataRow[4] = TRD.SubCategoryName;
                            dataRow[5] = TRD.ItemID;
                            dataRow[6] = TRD.ItemName;
                            dataRow[7] = TRD.RequestedQTY;
                            dataRow[8] = TRD.Description;
                            dataRow[9] = TRD.MeasurementId;

                            dt.Rows.Add(dataRow);
                        }

                        gvDatataTable.DataSource = dt;
                        gvDatataTable.DataBind();
                    }
                    dtExpectedDate.Text = TR.ExpectedDate.ToString("MM/dd/yyyy");
                    txtTrDescription.Text = TR.Description;
                    ddlWarehouse.SelectedValue = TR.ToWarehouseId.ToString();
                    ViewState["WarehouseId"] = ddlWarehouse.SelectedValue.ToString();

                    txtWarehouseHeads.ReadOnly = true;
                    List<UserWarehouse> warehouseHeads = userWarehouseController.GetWarehouseHeadsByWarehouseId(int.Parse(ViewState["WarehouseId"].ToString()));
                    txtWarehouseHeads.Text = warehouseHeads.Count > 0 ? string.Join("/", warehouseHeads.Select(w => w.UserName)) : "Not Assigned";


                }
                catch (Exception ex) {
                    throw ex;
                }

            }
        }


        private void LoadWarehouses()
        {
            ddlWarehouse.DataSource = warehouseController.getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
            ddlWarehouse.DataValueField = "WarehouseID";
            ddlWarehouse.DataTextField = "Location";
            ddlWarehouse.DataBind();
            //ViewState["WarehouseId"] = ddlWarehouse.Items[0].Value.ToString();
        }

        private void LoadMeasurement() {
            try {
                ddlMeasurement.DataSource = ControllerFactory.CreateMeasurementDetailController().GetMeasurementDetailsOfItem(int.Parse(ddlItemName.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()));
                ddlMeasurement.DataValueField = "DetailId";
                ddlMeasurement.DataTextField = "ShortCode";
                ddlMeasurement.DataBind();

            }
            catch (Exception) {

                throw;
            }
        }

        private void LoadDDLMainCatregory() {
            try {
                ddlMainCateGory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString())).OrderBy(C => C.CategoryName);
                ddlMainCateGory.DataValueField = "CategoryId";
                ddlMainCateGory.DataTextField = "CategoryName";
                ddlMainCateGory.DataBind();
                ddlMainCateGory.Items.Insert(0, new ListItem("Select Main Category", ""));
            }
            catch (Exception ex) {
                throw;
            }
        }

        //---------------Load Sub Category DDL
        protected void ddlMainCateGory_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (ddlMainCateGory.SelectedIndex != 0 && ddlMainCateGory.SelectedValue != "") {
                    int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                    // ddlItemName.Items.Clear();
                    LoadSubCategoryDDL(mainCategoryId);
                }
                else {
                    ddlSubCategory.Items.Clear();
                    ddlItemName.Items.Clear();
                }

            }
            catch (Exception ex) {

                throw ex;
            }
        }
        private void LoadSubCategoryDDL(int SubCatId) {
            try {
                ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(SubCatId, int.Parse(Session["CompanyId"].ToString())).OrderBy(C => C.SubCategoryName);
                ddlSubCategory.DataTextField = "SubCategoryName";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataBind();
                ddlSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        //---------------Load Items DDL
        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (ddlSubCategory.SelectedIndex != 0 && ddlSubCategory.SelectedValue != "") {
                    //Session["ItemNameLists"])
                    Session["MainCategoryId"] = ddlMainCateGory.SelectedValue;
                    Session["SubCategoryId"] = ddlSubCategory.SelectedValue;
                    int categoryId = int.Parse(ddlMainCateGory.SelectedValue);
                    int subCategoryId = int.Parse(ddlSubCategory.SelectedValue);

                    ddlItemName.DataSource = addItemController.FetchItemsByCategories(categoryId, subCategoryId, int.Parse(Session["CompanyId"].ToString())).OrderBy(y => y.ItemName).ToList();
                    ddlItemName.DataTextField = "ItemName";
                    ddlItemName.DataValueField = "ItemId";
                    ddlItemName.DataBind();
                    ddlItemName.Items.Insert(0, new ListItem("Select Item", ""));
                }
                else {
                    ddlItemName.Items.Clear();
                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }



        protected void btnOK_Click(object sender, EventArgs e) {
            Response.Redirect("CreateTR.aspx");
        }

        //--------------------Proceed PR
        protected void btnSavePR_Click(object sender, EventArgs e) {
            try {
                if (dtExpectedDate.Text != "") {
                    if (gvDatataTable.Rows.Count > 0) {
                        var tr = new JavaScriptSerializer().Deserialize<TR_Master>(ViewState["TR"].ToString());
                        tr.Description = txtTrDescription.Text;
                        tr.ExpectedDate = DateTime.Parse(dtExpectedDate.Text);
                        tr.ToWarehouseId = int.Parse(ddlWarehouse.SelectedValue.ToString());

                        if (tRMasterController.updateTR(tr) > 0) {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                            //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#SuccessAlert').modal('show'); });   </script>", false);
                            clearAll();
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error On Updating Transfer Request\"; $('#errorAlert').modal('show'); });   </script>", false);
                        }

                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Please add atleast one item\"; $('#errorAlert').modal('show'); });   </script>", false);
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Please Fill Fileds Marked with *\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            ddlItemName.Enabled = true;
            ddlSubCategory.Enabled = true;
            ddlMainCateGory.Enabled = true;
        }


        //----------------Dynamically Load To grid view PR Detail Data
        protected void btnAdd_Click(object sender, EventArgs e) {
            try {
                var TR = new JavaScriptSerializer().Deserialize<TR_Master>(ViewState["TR"].ToString());

                if (btnAdd.Text == "Add Item") {
                    if (txtQty.Text != "" && ddlMainCateGory.SelectedIndex != 0 && ddlSubCategory.SelectedIndex != 0 && ddlItemName.SelectedIndex != 0 && txtQty.Text != "") {


                        DataTable dt = new DataTable();
                        dt.Columns.Add("TRDId");
                        dt.Columns.Add("MainCategoryId");
                        dt.Columns.Add("MainCategoryName");
                        dt.Columns.Add("SubCategoryId");
                        dt.Columns.Add("SubcategoryName");
                        dt.Columns.Add("ItemId");
                        dt.Columns.Add("ItemName");
                        dt.Columns.Add("ItemQuantity");
                        dt.Columns.Add("ItemDescription");
                        dt.Columns.Add("MeasurementId");
                        bool hasItem = false;
                        if (gvDatataTable.Rows.Count > 0) {
                            foreach (GridViewRow row in gvDatataTable.Rows) {
                                var dataRow = dt.NewRow();

                                if (row.Cells[5].Text.ToString() == ddlItemName.SelectedValue.ToString()) {
                                    hasItem = true;
                                    break;
                                }
                                else {
                                    for (int i = 0; i < gvDatataTable.Columns.Count - 2; i++) {
                                        if (row.Cells[i].Text.ToString() != "&nbsp;") {
                                            dataRow[i] = row.Cells[i].Text.ToString();
                                        }
                                        else {
                                            dataRow[i] = "";
                                        }


                                    }
                                    dt.Rows.Add(dataRow);
                                }
                            }
                        }

                        if (!hasItem) {
                            TR_Details TRdetails = new TR_Details();
                            TRdetails.TRId = TR.TRId;
                            TRdetails.ItemID = int.Parse(ddlItemName.SelectedValue.ToString());
                            TRdetails.RequestedQTY = decimal.Parse(txtQty.Text);
                            TRdetails.Description = txtTRDDescription.Text;
                            TRdetails.MeasurementId = int.Parse(ddlMeasurement.SelectedValue);
                            int result = tRDetailsController.addTRD(TRdetails);
                            if (result > 0) {
                                DataRow NewRow = dt.NewRow();
                                NewRow[0] = result.ToString();
                                NewRow[1] = ddlMainCateGory.SelectedValue.ToString();
                                NewRow[2] = ddlMainCateGory.SelectedItem.ToString();
                                NewRow[3] = ddlSubCategory.SelectedValue.ToString();
                                NewRow[4] = ddlSubCategory.SelectedItem.ToString();
                                NewRow[5] = ddlItemName.SelectedValue.ToString();
                                NewRow[6] = ddlItemName.SelectedItem.ToString();
                                NewRow[7] = txtQty.Text;
                                NewRow[8] = txtTRDDescription.Text;
                                NewRow[9] = ddlMeasurement.SelectedValue.ToString();


                                dt.Rows.Add(NewRow);
                                gvDatataTable.DataSource = dt;
                                gvDatataTable.DataBind();

                                LoadDDLMainCatregory();
                                ddlMainCateGory.SelectedIndex = 0;
                                ddlSubCategory.SelectedIndex = 0;
                                ddlItemName.SelectedIndex = 0;
                                txtTRDDescription.Text = "";
                                txtQty.Text = "";
                                ddlMainCateGory.Enabled = true;
                                ddlSubCategory.Enabled = true;
                                ddlItemName.Enabled = true;

                                clearFields();
                            }
                            else {
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error On Adding Items\"; $('#errorAlert').modal('show'); });   </script>", false);
                            }
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Item Already exists\"; $('#errorAlert').modal('show'); });   </script>", false);
                        }


                    }

                }
                if (btnAdd.Text == "Update Item") {
                    if (txtQty.Text != "" && ddlMainCateGory.SelectedIndex != 0 && ddlSubCategory.SelectedIndex != 0 && ddlItemName.SelectedIndex != 0 && txtQty.Text != "") {
                        TR_Details tr_Details = new TR_Details();
                        tr_Details.TRDId = int.Parse(gvDatataTable.Rows[int.Parse(ViewState["EditRowIndex"].ToString())].Cells[0].Text.ToString());
                        tr_Details.TRId = TR.TRId;
                        tr_Details.ItemID = int.Parse(ddlItemName.SelectedValue.ToString());
                        tr_Details.RequestedQTY = decimal.Parse(txtQty.Text);
                        tr_Details.Description = txtTRDDescription.Text;
                        tr_Details.MeasurementId = int.Parse(ddlMeasurement.SelectedValue);

                        if (tRDetailsController.updateTRD(tr_Details) > 0) {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("TRDId");
                            dt.Columns.Add("MainCategoryId");
                            dt.Columns.Add("MainCategoryName");
                            dt.Columns.Add("SubCategoryId");
                            dt.Columns.Add("SubcategoryName");
                            dt.Columns.Add("ItemId");
                            dt.Columns.Add("ItemName");
                            dt.Columns.Add("ItemQuantity");
                            dt.Columns.Add("ItemDescription");
                            dt.Columns.Add("MeasurementId");



                            if (gvDatataTable.Rows.Count > 0) {
                                foreach (GridViewRow row in gvDatataTable.Rows) {
                                    if (row.RowIndex != int.Parse(ViewState["EditRowIndex"].ToString())) {
                                        var dataRow = dt.NewRow();
                                        for (int i = 0; i < gvDatataTable.Columns.Count - 2; i++) {
                                            if (row.Cells[i].Text.ToString() != "&nbsp;") {
                                                dataRow[i] = row.Cells[i].Text.ToString();
                                            }
                                            else {
                                                dataRow[i] = "";
                                            }
                                        }
                                        dt.Rows.Add(dataRow);
                                    }
                                }
                            }


                            DataRow NewRow = dt.NewRow();
                            NewRow[0] = tr_Details.TRDId.ToString();
                            NewRow[1] = ddlMainCateGory.SelectedValue.ToString();
                            NewRow[2] = ddlMainCateGory.SelectedItem.ToString();
                            NewRow[3] = ddlSubCategory.SelectedValue.ToString();
                            NewRow[4] = ddlSubCategory.SelectedItem.ToString();
                            NewRow[5] = ddlItemName.SelectedValue.ToString();
                            NewRow[6] = ddlItemName.SelectedItem.ToString();
                            NewRow[7] = txtQty.Text;
                            NewRow[8] = txtTRDDescription.Text;
                            NewRow[9] = ddlMeasurement.SelectedValue.ToString();
                            dt.Rows.Add(NewRow);
                            gvDatataTable.DataSource = dt;
                            gvDatataTable.DataBind();

                            LoadDDLMainCatregory();
                            ddlMainCateGory.SelectedIndex = 0;
                            ddlSubCategory.SelectedIndex = 0;
                            ddlItemName.SelectedIndex = 0;
                            txtTRDDescription.Text = "";
                            txtQty.Text = "";

                            ddlMainCateGory.Enabled = true;
                            ddlSubCategory.Enabled = true;
                            ddlItemName.Enabled = true;

                            clearFields();

                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error On Updating Items\"; $('#errorAlert').modal('show'); });   </script>", false);
                        }
                    }
                }

                DataTable dt1 = new DataTable();

                dt1.Columns.Add("TRDId");
                dt1.Columns.Add("MainCategoryId");
                dt1.Columns.Add("MainCategoryName");
                dt1.Columns.Add("SubCategoryId");
                dt1.Columns.Add("SubcategoryName");
                dt1.Columns.Add("ItemId");
                dt1.Columns.Add("ItemName");
                dt1.Columns.Add("ItemQuantity");
                dt1.Columns.Add("ItemDescription");
                dt1.Columns.Add("MeasurementId");

                List<TR_Details> TRDetails = tRDetailsController.fetchTRDList(TR.TRId, int.Parse(Session["CompanyId"].ToString()));
                foreach (TR_Details trd in TRDetails) {
                    var dataRow = dt1.NewRow();
                    dataRow[0] = trd.TRDId;
                    dataRow[1] = trd.CategoryID;
                    dataRow[2] = trd.CategoryName;
                    dataRow[3] = trd.SubCategoryID;
                    dataRow[4] = trd.SubCategoryName;
                    dataRow[5] = trd.ItemID;
                    dataRow[6] = trd.ItemName;
                    dataRow[7] = trd.RequestedQTY;
                    dataRow[8] = trd.Description;
                    dataRow[9] = trd.MeasurementId;

                    dt1.Rows.Add(dataRow);
                }

                gvDatataTable.DataSource = dt1;
                gvDatataTable.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }



        protected void confirmation_Click(object sender, EventArgs e) {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
        }

        private void DisplayMessage(string message, bool isError) {
            msg.Visible = true;
            if (isError) {
                lbMessage.CssClass = "failMessage";
                msg.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else {
                lbMessage.CssClass = "successMessage";
                msg.Attributes["class"] = "alert alert-success alert-dismissable";
            }

            lbMessage.Text = message;

        }

        private void clearFields() {
            txtQty.Text = "";
            LoadDDLMainCatregory();
            ddlMainCateGory.SelectedIndex = 0;
            ddlItemName.Enabled = true;
            ddlSubCategory.Enabled = true;
            ddlMainCateGory.Enabled = true;
            btnAdd.Text = "Add Item";
            ddlItemName.Items.Clear();
            ddlSubCategory.Items.Clear();
            ddlMeasurement.Items.Clear();

            txtStock.Text = "";


        }

        private void clearAll() {
            dtExpectedDate.Text = "";
            txtTrDescription.Text = "";
            txtQty.Text = "";
            LoadDDLMainCatregory();
            ddlMainCateGory.SelectedIndex = 0;
            ddlItemName.Enabled = true;
            ddlSubCategory.Enabled = true;
            ddlMainCateGory.Enabled = true;
            btnAdd.Text = "Add Item";
            ddlItemName.Items.Clear();
            ddlSubCategory.Items.Clear();
            txtTrCode.Text = "";

            gvDatataTable.DataSource = null;
            gvDatataTable.DataBind();
            ddlMeasurement.Items.Clear();

            txtStock.Text = "";
        }


        protected void btnEditItem_Click(object sender, ImageClickEventArgs e) {
            try {
                ViewState["EditRowIndex"] = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                GridViewRow row = gvDatataTable.Rows[int.Parse(ViewState["EditRowIndex"].ToString())];

                LoadDDLMainCatregory();
                ddlMainCateGory.SelectedValue = row.Cells[1].Text.ToString();
                ddlMainCateGory_SelectedIndexChanged(null, null);
                ddlSubCategory.SelectedValue = row.Cells[3].Text.ToString();
                ddlSubCategory_SelectedIndexChanged(null, null);
                ddlItemName.SelectedValue = row.Cells[5].Text.ToString();
                txtQty.Text = row.Cells[7].Text.ToString();
                if (row.Cells[7].Text.ToString() != "&nbsp;") {
                    txtTRDDescription.Text = row.Cells[8].Text.ToString();
                }
                else {
                    txtTRDDescription.Text = "";
                }

                LoadMeasurement();
                ddlMeasurement.SelectedValue = row.Cells[9].Text.ToString();

                if (ddlItemName.SelectedValue != null && ddlItemName.SelectedValue != "" && ddlWarehouse.SelectedValue != null && ddlWarehouse.SelectedValue != "") {
                    MeasurementDetail stockMaintainingUOM = ControllerFactory.CreateMeasurementDetailController().GetStockMaintainingMeasurement(int.Parse(ddlItemName.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()));

                    ViewState["StockMaintainingUOM"] = serializer.Serialize(stockMaintainingUOM);

                    decimal availableStock = ControllerFactory.CreateInventoryController()
                        .GetWarehouseInventoryForItem(int.Parse(ddlWarehouse.SelectedValue.ToString()), int.Parse(ddlItemName.SelectedValue.ToString()));

                    decimal convertedValue = ControllerFactory.CreateConversionController().DoConversion(
                        int.Parse(ddlItemName.SelectedValue),
                        int.Parse(ViewState["CompanyId"].ToString()),
                        availableStock,
                        stockMaintainingUOM.DetailId,
                        int.Parse(ddlMeasurement.SelectedValue));

                    txtStock.Text = convertedValue.ToString("0.##########") + " " + ddlMeasurement.SelectedItem.ToString();
                }
                else {
                    txtStock.Text = "";
                }
                
                btnAdd.Text = "Update Item";
                ddlMainCateGory.Enabled = false;
                ddlSubCategory.Enabled = false;
                ddlItemName.Enabled = false;

            }
            catch (Exception ex) {
                throw ex;
            }
        }



        protected void btnClear_Click(object sender, EventArgs e) {
            try {
                clearFields();
            }
            catch (Exception ex) {
                throw ex;
            }
        }



        protected void btnDeleteItem_Click1(object sender, ImageClickEventArgs e) {
            try {

                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;

                if (tRDetailsController.DeleteTRD(int.Parse(gvDatataTable.Rows[x].Cells[0].Text.ToString())) > 0) {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("TRDId");
                    dt.Columns.Add("MainCategoryId");
                    dt.Columns.Add("MainCategoryName");
                    dt.Columns.Add("SubCategoryId");
                    dt.Columns.Add("SubcategoryName");
                    dt.Columns.Add("ItemId");
                    dt.Columns.Add("ItemName");
                    dt.Columns.Add("ItemQuantity");
                    dt.Columns.Add("ItemDescription");
                    dt.Columns.Add("MeasurementId");

                    if (gvDatataTable.Rows.Count > 0) {
                        foreach (GridViewRow row in gvDatataTable.Rows) {
                            var dataRow = dt.NewRow();
                            for (int i = 0; i < gvDatataTable.Columns.Count - 2; i++) {
                                dataRow[i] = row.Cells[i].Text.ToString();
                            }
                            dt.Rows.Add(dataRow);
                        }
                    }
                    dt.Rows.RemoveAt(x);
                    gvDatataTable.DataSource = dt;
                    gvDatataTable.DataBind();
                    clearFields();
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error On Deleting Items\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex) {

                throw ex;
            }

        }

        protected void gvDatataTable_RowDeleting(object sender, GridViewDeleteEventArgs e) {

        }

        protected void btnOK_Click1(object sender, EventArgs e) {
            Response.Redirect("ViewTR.aspx");
        }

        protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddlItemName.SelectedValue != null && ddlItemName.SelectedValue != "" && ddlWarehouse.SelectedValue != null && ddlWarehouse.SelectedValue != "") {
                MeasurementDetail stockMaintainingUOM = serializer.Deserialize<MeasurementDetail>(ViewState["StockMaintainingUOM"].ToString());
                decimal availableStock = ControllerFactory.CreateInventoryController()
                    .GetWarehouseInventoryForItem(int.Parse(ddlWarehouse.SelectedValue.ToString()), int.Parse(ddlItemName.SelectedValue.ToString()));

                decimal convertedValue = ControllerFactory.CreateConversionController().DoConversion(
                    int.Parse(ddlItemName.SelectedValue),
                    int.Parse(ViewState["CompanyId"].ToString()),
                    availableStock,
                    stockMaintainingUOM.DetailId,
                    int.Parse(ddlMeasurement.SelectedValue));

                txtStock.Text = convertedValue.ToString("0.##########") + " " + ddlMeasurement.SelectedItem.ToString();
            }
            else {
                txtStock.Text = "";
            }
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddlItemName.SelectedValue != null && ddlItemName.SelectedValue.ToString() != "") {
                LoadMeasurement();
                MeasurementDetail stockMaintainingMeasurement = ControllerFactory.CreateMeasurementDetailController().GetStockMaintainingMeasurement(int.Parse(ddlItemName.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()));

                ViewState["StockMaintainingUOM"] = serializer.Serialize(stockMaintainingMeasurement);

                ddlMeasurement.SelectedValue = stockMaintainingMeasurement.DetailId.ToString();

                if (ddlWarehouse.SelectedValue != null && ddlWarehouse.SelectedValue != "") {

                    txtStock.Text = ControllerFactory.CreateInventoryController()
                        .GetWarehouseInventoryForItem(int.Parse(ddlWarehouse.SelectedValue.ToString()), int.Parse(ddlItemName.SelectedValue.ToString())).ToString("0.##########") + " " + stockMaintainingMeasurement.ShortCode;
                }
                else {
                    txtStock.Text = "";
                }
            }
            else {
                ddlMeasurement.Items.Clear();
                txtStock.Text = "";
            }
        }

        protected void ddlMeasurement_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddlItemName.SelectedValue != null && ddlItemName.SelectedValue != "" && ddlWarehouse.SelectedValue != null && ddlWarehouse.SelectedValue != "") {
                MeasurementDetail stockMaintainingUOM = serializer.Deserialize<MeasurementDetail>(ViewState["StockMaintainingUOM"].ToString());
                decimal availableStock = ControllerFactory.CreateInventoryController()
                    .GetWarehouseInventoryForItem(int.Parse(ddlWarehouse.SelectedValue.ToString()), int.Parse(ddlItemName.SelectedValue.ToString()));

                decimal convertedValue = ControllerFactory.CreateConversionController().DoConversion(
                    int.Parse(ddlItemName.SelectedValue),
                    int.Parse(ViewState["CompanyId"].ToString()),
                    availableStock,
                    stockMaintainingUOM.DetailId,
                    int.Parse(ddlMeasurement.SelectedValue));

                txtStock.Text = convertedValue.ToString("0.##########") + " " + ddlMeasurement.SelectedItem.ToString();
            }
            else {
                txtStock.Text = "";
            }
        }
    }
}