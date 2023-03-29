using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class CreateTR : System.Web.UI.Page
    {
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        //MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        UnitMeasurementController unitMeasurementController = ControllerFactory.CreateUnitMeasurementController();
        UserSubDepartmentController userSubDepartment = ControllerFactory.CreateUserSubDepartment();
        DepartmentWarehouseController departmentWarehouseController = ControllerFactory.CreateDepartmentWarehouse();
        UserWarehouseController userWarehouseController = ControllerFactory.CreateUserWarehouse();
        WarehouseControllerInterface warehouseController = ControllerFactory.CreateWarehouseController();
        TRMasterController tRMasterController = ControllerFactory.CreateTRMasterController();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtWarehouseHeads.ReadOnly = true;
            if (Session["CompanyId"] != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefTR";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabTR";
                ((BiddingAdmin)Page.Master).subTabValue = "CreateTR.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "CreateTRLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 13, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            msg.Visible = false;
            if (!IsPostBack)
            {
                if (ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(Session["CompanyId"].ToString())).Count > 0)
                {
                    if (Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Count > 0)
                    {
                        try
                        {
                            if ((Session["UserDepartments"] as List<UserSubDepartment>).Count == 1)
                            {
                                ViewState["SubDepartmentId"] = (Session["UserDepartments"] as List<UserSubDepartment>)[0].DepartmentId;
                                divDepartment.Attributes.Add("class", "hidden");
                            }
                            else
                            {
                                LoadUserWarehouses();
                                divDepartment.Attributes.Add("class", "");
                            }
                            LoadWarehouses();

                            ViewState["WarehouseId"] = int.Parse(ddlStock.SelectedValue.ToString());
                            LoadDDLMainCatregory();

                            if (Session["TR"] != null)
                            {
                                DataTable dt = new DataTable();
                                TR_Master tr = Session["TR"] as TR_Master;
                                txtTrDescription.Text = tr.Description;
                                ddlStock.SelectedValue = tr.FromWarehouseId.ToString();
                                ViewState["WarehouseId"] = tr.FromWarehouseId.ToString();
                                dtExpectedDate.Text = LocalTime.ToLocalTime(tr.ExpectedDate.ToLocalTime()).ToString("MM/dd/yyyy");

                                LoadWarehouses();
                                LoadDDLMainCatregory();
                                //LoadMeasurement();
                                ddlWarehouse.SelectedValue = tr.ToWarehouseId.ToString();
                                ViewState["ToWarehouse"] = int.Parse(ddlWarehouse.SelectedValue);

                                dt.Columns.Add("MainCategoryId");
                                dt.Columns.Add("MainCategoryName");
                                dt.Columns.Add("SubCategoryId");
                                dt.Columns.Add("SubcategoryName");
                                dt.Columns.Add("ItemId");
                                dt.Columns.Add("ItemName");
                                dt.Columns.Add("ItemQuantity");
                                dt.Columns.Add("ShortName");
                                dt.Columns.Add("ItemDescription");
                                dt.Columns.Add("MeasurementId");

                                for (int i = 0; i < tr.TRDetails.Count; i++)
                                {
                                    DataRow NewRow = dt.NewRow();
                                    NewRow[0] = tr.TRDetails[i].CategoryID.ToString();
                                    NewRow[1] = tr.TRDetails[i].CategoryName.ToString();
                                    NewRow[2] = tr.TRDetails[i].SubCategoryID.ToString();
                                    NewRow[3] = tr.TRDetails[i].SubCategoryName.ToString();
                                    NewRow[4] = tr.TRDetails[i].ItemID.ToString();
                                    NewRow[5] = tr.TRDetails[i].ItemName.ToString();
                                    NewRow[6] = tr.TRDetails[i].RequestedQTY;
                                    NewRow[7] = tr.TRDetails[i].ShortName;
                                    NewRow[8] = tr.TRDetails[i].Description;
                                    NewRow[9] = tr.TRDetails[i].MeasurementId.ToString();
                                    dt.Rows.Add(NewRow);
                                }
                                gvDatataTable.DataSource = dt;
                                gvDatataTable.DataBind();

                                Session["TR"] = null;

                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You are not assigned to a department yet.', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'Please add atleast one Warehouse to continue', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                }
            }

        }
        private void LoadWarehouses()
        {
            int warehouseId = int.Parse(ddlStock.SelectedValue);
            ddlWarehouse.DataSource = warehouseController.getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
            ddlWarehouse.DataValueField = "WarehouseID";
            ddlWarehouse.DataTextField = "Location";
            ddlWarehouse.DataBind();

        }

        private void LoadUserWarehouses()
        {
            ddlStock.DataSource = ControllerFactory.CreateUserWarehouse().GetWarehousesByUserId(int.Parse(ViewState["UserId"].ToString()));
            ddlStock.DataValueField = "WrehouseId";
            ddlStock.DataTextField = "Location";
            ddlStock.DataBind();
            // ViewState["WrehouseId"] = ddlStock.Items[0].Value.ToString();
        }

        private void LoadMeasurement()
        {
            try
            {
                ddlMeasurement.DataSource = ControllerFactory.CreateMeasurementDetailController().GetMeasurementDetailsOfItem(int.Parse(ddlItemName.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()));
                ddlMeasurement.DataValueField = "DetailId";
                ddlMeasurement.DataTextField = "ShortCode";
                ddlMeasurement.DataBind();

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LoadDDLMainCatregory()
        {
            try
            {
                ddlMainCateGory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString())).OrderBy(C => C.CategoryName);
                ddlMainCateGory.DataValueField = "CategoryId";
                ddlMainCateGory.DataTextField = "CategoryName";
                ddlMainCateGory.DataBind();
                ddlMainCateGory.Items.Insert(0, new ListItem("Select Main Category", ""));
            }
            catch (Exception ex)
            {
                throw;
            }
            List<UserWarehouse> warehouseHeads = userWarehouseController.GetWarehouseHeadsByWarehouseId(int.Parse(ViewState["WarehouseId"].ToString()));
            txtWarehouseHeads.Text = warehouseHeads.Count > 0 ? string.Join("/", warehouseHeads.Select(w => w.UserName)) : "Not Assigned";

            //  List<UserSubDepartment> departmentDetails = userSubDepartment.getDepartmentHeads(int.Parse(ViewState["SubDepartmentId"].ToString()));
            //   txtDepartmentHeads.Text = departmentDetails.Count > 0 ? string.Join("/ ", departmentDetails.Select(d => d.FirstName)) : "Not Assigned";

        }
        protected void ddlMainCateGory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlMainCateGory.SelectedIndex != 0 && ddlMainCateGory.SelectedValue != "")
                {
                    int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                    // ddlItemName.Items.Clear();
                    LoadSubCategoryDDL(mainCategoryId);
                }
                else
                {
                    ddlSubCategory.Items.Clear();
                    ddlItemName.Items.Clear();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void LoadSubCategoryDDL(int SubCatId)
        {
            try
            {
                ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(SubCatId, int.Parse(Session["CompanyId"].ToString())).OrderBy(C => C.SubCategoryName);
                ddlSubCategory.DataTextField = "SubCategoryName";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataBind();
                ddlSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSubCategory.SelectedIndex != 0 && ddlSubCategory.SelectedValue != "")
                {
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
                else
                {
                    ddlItemName.Items.Clear();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSavePR_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(ddlStock.SelectedValue) != int.Parse(ddlWarehouse.SelectedValue))
                {
                    if (dtExpectedDate.Text != "")
                    {
                        if (gvDatataTable.Rows.Count > 0)
                        {
                            TR_Master TRM = new TR_Master
                            {
                                CompanyId = int.Parse(Session["CompanyId"].ToString()),
                                FromWarehouseId = int.Parse(ViewState["WarehouseId"].ToString()),
                                Description = txtTrDescription.Text,
                                ExpectedDate = DateTime.Parse(dtExpectedDate.Text),
                                CreatedBy = int.Parse(Session["UserId"].ToString()),
                                // ToWarehouseId = int.Parse(ViewState["ToWarehouse"].ToString())
                                ToWarehouseId = int.Parse(ddlWarehouse.SelectedValue.ToString())


                            };
                            TRM.TRDetails = new List<TR_Details>();
                            foreach (GridViewRow row in gvDatataTable.Rows)
                            {
                                TR_Details TRD = new TR_Details();
                                TRD.CategoryID = int.Parse(row.Cells[0].Text.ToString());
                                TRD.SubCategoryID = int.Parse(row.Cells[2].Text.ToString());
                                TRD.ItemID = int.Parse(row.Cells[4].Text.ToString());
                                TRD.Description = row.Cells[7].Text.ToString();
                                TRD.RequestedQTY = decimal.Parse(row.Cells[6].Text.ToString());
                                TRD.MeasurementId = int.Parse(row.Cells[9].Text.ToString());
                                TRM.TRDetails.Add(TRD);
                            }

                            int TRCode = tRMasterController.saveTR(TRM);


                            if (TRCode > 0)
                            {

                                Thread th = new Thread(new ThreadStart(sendEmails));
                                th.Start();
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',html:'Transfer Request Created with the code <b>TR" + TRCode + "</b>'}); });   </script>", false);
                                //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#SuccessAlert').modal('show'); });   </script>", false);
                                clearAll();
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error On Creating Transfer Request\"; $('#errorAlert').modal('show'); });   </script>", false);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Please add atleast one item\"; $('#errorAlert').modal('show'); });   </script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Please Fill Fileds Marked with *\"; $('#errorAlert').modal('show'); });   </script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Can not Request from your Warehouse *\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ddlItemName.Enabled = true;
            ddlSubCategory.Enabled = true;
            ddlMainCateGory.Enabled = true;
            clearFields();
        }
        private void sendEmails()
        {
            //  List<string> emails = companyLoginController.GetUserEmailsByAccess(12, 3, int.Parse(Session["CompanyId"].ToString())).Distinct().ToList();

            List<string> emails = companyLoginController.GetWarehouseHeadsEmails(int.Parse(ViewState["WarehouseId"].ToString())).ToList();

            if (emails.Count > 0)
            {
                string to = string.Join(",", emails);
                string subject = "New Transfer Requests for Approval";
                string message = "Dear User,\n\nYou have new Transfer requests pending for approval. Please login to your account at admin.ezbidlanka.lk and provide your approval to proceed further.\n\n" +
                            "Thanks and Regards,\nTeam EzBidLanka.";
                var x = "";

              //  EmailGenerator.SendEmail(to,emails, subject, message,false);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["ToWarehouse"] = int.Parse(ddlWarehouse.SelectedValue);
                if (btnAdd.Text == "Add Item")
                {
                    if (txtQty.Text != "" && ddlMainCateGory.SelectedIndex != 0 && ddlSubCategory.SelectedIndex != 0 && ddlItemName.SelectedIndex != 0 && txtQty.Text != "")
                    {

                        DataTable dt = new DataTable();

                        dt.Columns.Add("MainCategoryId");
                        dt.Columns.Add("MainCategoryName");
                        dt.Columns.Add("SubCategoryId");
                        dt.Columns.Add("SubcategoryName");
                        dt.Columns.Add("ItemId");
                        dt.Columns.Add("ItemName");
                        dt.Columns.Add("ItemQuantity");
                        dt.Columns.Add("ShortName");
                        dt.Columns.Add("ItemDescription");
                        dt.Columns.Add("MeasurementId");
                        bool hasItem = false;

                        if (gvDatataTable.Rows.Count > 0)
                        {
                            foreach (GridViewRow row in gvDatataTable.Rows)
                            {
                                var dataRow = dt.NewRow();

                                if (row.Cells[4].Text.ToString() == ddlItemName.SelectedValue.ToString())
                                {
                                    hasItem = true;
                                    break;
                                }
                                else
                                {
                                    for (int i = 0; i < gvDatataTable.Columns.Count - 2; i++)
                                    {
                                        if (row.Cells[i].Text.ToString() != "&nbsp;")
                                        {
                                            dataRow[i] = row.Cells[i].Text.ToString();
                                        }
                                        else
                                        {
                                            dataRow[i] = "";
                                        }

                                    }
                                    dt.Rows.Add(dataRow);
                                }
                            }
                        }

                        if (!hasItem)
                        {
                            DataRow NewRow = dt.NewRow();
                            NewRow[0] = ddlMainCateGory.SelectedValue.ToString();
                            NewRow[1] = ddlMainCateGory.SelectedItem.ToString();
                            NewRow[2] = ddlSubCategory.SelectedValue.ToString();
                            NewRow[3] = ddlSubCategory.SelectedItem.ToString();
                            NewRow[4] = ddlItemName.SelectedValue.ToString();
                            NewRow[5] = ddlItemName.SelectedItem.ToString();
                            NewRow[6] = txtQty.Text;
                            NewRow[7] = ddlMeasurement.SelectedItem.ToString();
                            NewRow[8] = txtTrdDescription.Text;
                            NewRow[9] = ddlMeasurement.SelectedValue.ToString();
                            dt.Rows.Add(NewRow);
                            gvDatataTable.DataSource = dt;
                            gvDatataTable.DataBind();

                            LoadDDLMainCatregory();
                            ddlMainCateGory.SelectedIndex = 0;
                            ddlSubCategory.SelectedIndex = 0;
                            ddlItemName.SelectedIndex = 0;
                            txtTrdDescription.Text = "";
                            txtQty.Text = "";
                            ddlMainCateGory.Enabled = true;
                            ddlSubCategory.Enabled = true;
                            ddlItemName.Enabled = true;

                            clearFields();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Item Already exists\"; $('#errorAlert').modal('show'); });   </script>", false);
                        }


                    }

                }
                if (btnAdd.Text == "Update Item")
                {
                    if (txtQty.Text != "" && ddlMainCateGory.SelectedIndex != 0 && ddlSubCategory.SelectedIndex != 0 && ddlItemName.SelectedIndex != 0 && txtQty.Text != "")
                    {

                        DataTable dt = new DataTable();

                        dt.Columns.Add("MainCategoryId");
                        dt.Columns.Add("MainCategoryName");
                        dt.Columns.Add("SubCategoryId");
                        dt.Columns.Add("SubcategoryName");
                        dt.Columns.Add("ItemId");
                        dt.Columns.Add("ItemName");
                        dt.Columns.Add("ItemQuantity");
                        dt.Columns.Add("ShortName");
                        dt.Columns.Add("ItemDescription");
                        dt.Columns.Add("MeasurementId");


                        if (gvDatataTable.Rows.Count > 0)
                        {
                            foreach (GridViewRow row in gvDatataTable.Rows)
                            {
                                if (row.RowIndex != int.Parse(ViewState["EditRowIndex"].ToString()))
                                {
                                    var dataRow = dt.NewRow();
                                    for (int i = 0; i < gvDatataTable.Columns.Count - 2; i++)
                                    {
                                        if (row.Cells[i].Text.ToString() != "&nbsp;")
                                        {
                                            dataRow[i] = row.Cells[i].Text.ToString();
                                        }
                                        else
                                        {
                                            dataRow[i] = "";
                                        }
                                    }
                                    dt.Rows.Add(dataRow);
                                }
                            }
                        }



                        DataRow NewRow = dt.NewRow();
                        NewRow[0] = ddlMainCateGory.SelectedValue.ToString();
                        NewRow[1] = ddlMainCateGory.SelectedItem.ToString();
                        NewRow[2] = ddlSubCategory.SelectedValue.ToString();
                        NewRow[3] = ddlSubCategory.SelectedItem.ToString();
                        NewRow[4] = ddlItemName.SelectedValue.ToString();
                        NewRow[5] = ddlItemName.SelectedItem.ToString();
                        NewRow[6] = txtQty.Text;
                        NewRow[7] = ddlMeasurement.SelectedItem.ToString();
                        NewRow[8] = txtTrdDescription.Text;
                        NewRow[9] = ddlMeasurement.SelectedValue.ToString();
                        dt.Rows.Add(NewRow);
                        gvDatataTable.DataSource = dt;
                        gvDatataTable.DataBind();

                        LoadDDLMainCatregory();
                        ddlMainCateGory.SelectedIndex = 0;
                        ddlSubCategory.SelectedIndex = 0;
                        ddlItemName.SelectedIndex = 0;
                        txtTrdDescription.Text = "";
                        txtQty.Text = "";
                    }
                    ddlMainCateGory.Enabled = true;
                    ddlSubCategory.Enabled = true;
                    ddlItemName.Enabled = true;

                    clearFields();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
        }
        private void DisplayMessage(string message, bool isError)
        {
            msg.Visible = true;
            if (isError)
            {
                lbMessage.CssClass = "failMessage";
                msg.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else
            {
                lbMessage.CssClass = "successMessage";
                msg.Attributes["class"] = "alert alert-success alert-dismissable";
            }

            lbMessage.Text = message;

        }
        private void clearFields()
        {
            txtQty.Text = "";
            LoadDDLMainCatregory();
            ddlMainCateGory.SelectedIndex = 0;
            ddlItemName.Enabled = true;
            ddlSubCategory.Enabled = true;
            ddlMainCateGory.Enabled = true;
            btnAdd.Text = "Add Item";
            ddlItemName.Items.Clear();
            ddlSubCategory.Items.Clear();
            ddlItemName.Items.Clear();
            txtStock.Text = "";
            ddlMeasurement.Items.Clear();
            // ddlStock.Items.Clear();
            //ddlWarehouse.Items.Clear();




        }
        private void clearAll()
        {
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
            txtStock.Text = "";
            ddlItemName.Items.Clear();
            txtWarehouseHeads.Text = "";
            txtTrdDescription.Text = "";
            ddlMeasurement.Items.Clear();

            gvDatataTable.DataSource = null;
            gvDatataTable.DataBind();

            if ((Session["UserDepartments"] as List<UserSubDepartment>).Count == 1)
            {
                ViewState["SubDepartmentId"] = (Session["UserDepartments"] as List<UserSubDepartment>)[0].DepartmentId;
                divDepartment.Attributes.Add("class", "hidden");
            }
            else
            {
                LoadUserWarehouses();
                divDepartment.Attributes.Add("class", "");
            }

            LoadWarehouses();


        }
        protected void btnEditItem_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ViewState["EditRowIndex"] = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                GridViewRow row = gvDatataTable.Rows[int.Parse(ViewState["EditRowIndex"].ToString())];

                LoadDDLMainCatregory();
                ddlMainCateGory.SelectedValue = row.Cells[0].Text.ToString();
                ddlMainCateGory_SelectedIndexChanged(null, null);
                ddlSubCategory.SelectedValue = row.Cells[2].Text.ToString();
                ddlSubCategory_SelectedIndexChanged(null, null);
                ddlItemName.SelectedValue = row.Cells[4].Text.ToString();
                txtQty.Text = row.Cells[6].Text.ToString();
                if (row.Cells[8].Text.ToString() != "&nbsp;")
                {
                    txtTrdDescription.Text = row.Cells[8].Text.ToString();
                }
                else
                {
                    txtTrdDescription.Text = "";
                }

                LoadMeasurement();
                ddlMeasurement.SelectedValue = row.Cells[9].Text.ToString();

                if (ddlItemName.SelectedValue != null && ddlItemName.SelectedValue != "" && ddlWarehouse.SelectedValue != null && ddlWarehouse.SelectedValue != "")
                {
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
                else
                {
                    txtStock.Text = "";
                }

                btnAdd.Text = "Update Item";
                ddlMainCateGory.Enabled = false;
                ddlSubCategory.Enabled = false;
                ddlItemName.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                clearFields();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void btnDeleteItem_Click1(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("MainCategoryId");
                dt.Columns.Add("MainCategoryName");
                dt.Columns.Add("SubCategoryId");
                dt.Columns.Add("SubcategoryName");
                dt.Columns.Add("ItemId");
                dt.Columns.Add("ItemName");
                dt.Columns.Add("ItemQuantity");
                dt.Columns.Add("ShortName");
                dt.Columns.Add("ItemDescription");
                dt.Columns.Add("MeasurementId");

                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                if (gvDatataTable.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvDatataTable.Rows)
                    {
                        var dataRow = dt.NewRow();
                        for (int i = 0; i < gvDatataTable.Columns.Count - 2; i++)
                        {
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
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void gvDatataTable_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void ddlStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["WarehouseId"] = ddlStock.SelectedValue.ToString();
            LoadWarehouses();
            //List<UserSubDepartment> departmentDetails = userSubDepartment.getDepartmentHeads(int.Parse(ddlStock.SelectedValue.ToString()));
            //txtDepartmentHeads.Text = departmentDetails.Count>0? string.Join("/ ", departmentDetails.Select(d=> d.FirstName)): "Not Assigned";

            List<UserWarehouse> warehouseHeads = userWarehouseController.GetWarehouseHeadsByWarehouseId(int.Parse(ddlStock.SelectedValue.ToString()));
            txtWarehouseHeads.Text = warehouseHeads.Count > 0 ? string.Join("/", warehouseHeads.Select(w => w.UserName)) : "Not Assigned";
        }
        protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedValue != null && ddlItemName.SelectedValue != "" && ddlWarehouse.SelectedValue != null && ddlWarehouse.SelectedValue != "")
            {
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
            else
            {
                txtStock.Text = "";
            }
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedValue != null && ddlItemName.SelectedValue.ToString() != "")
            {
                LoadMeasurement();
                MeasurementDetail stockMaintainingMeasurement = ControllerFactory.CreateMeasurementDetailController().GetStockMaintainingMeasurement(int.Parse(ddlItemName.SelectedValue.ToString()), int.Parse(Session["CompanyId"].ToString()));

                ViewState["StockMaintainingUOM"] = serializer.Serialize(stockMaintainingMeasurement);

                ddlMeasurement.SelectedValue = stockMaintainingMeasurement.DetailId.ToString();

                if (ddlWarehouse.SelectedValue != null && ddlWarehouse.SelectedValue != "")
                {

                    txtStock.Text = ControllerFactory.CreateInventoryController()
                        .GetWarehouseInventoryForItem(int.Parse(ddlWarehouse.SelectedValue.ToString()), int.Parse(ddlItemName.SelectedValue.ToString())).ToString("0.##########") + " " + stockMaintainingMeasurement.ShortCode;
                }
                else
                {
                    txtStock.Text = "";
                }
            }
            else
            {
                ddlMeasurement.Items.Clear();
                txtStock.Text = "";
            }
        }
        protected void ddlMeasurement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedValue != null && ddlItemName.SelectedValue != "" && ddlWarehouse.SelectedValue != null && ddlWarehouse.SelectedValue != "")
            {
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
            else
            {
                txtStock.Text = "";
            }
        }

    }
}