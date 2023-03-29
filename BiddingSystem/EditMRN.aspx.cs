using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using System.Text;
using CLibrary.Domain;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Data;

namespace BiddingSystem
{
    public partial class EditMRN : System.Web.UI.Page
    {
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();


        static string UserId = string.Empty;
        public List<string> BomStringList = new List<string>();

        static int CompanyId = 0;
        public int editRowIndex = 0;
        public static MrnMaster MRN = null;
        string[] Tes1ItemTextBoxValue;
        string[] Tes2ItemTextBoxValue;
        DateTime Requested;
        int prid = 0;
        static int itemId = 0;
        public int ItemIdFilterd = 0;
        //string[] Tes3ItemTextBoxValue;
        public enum ReplacementRdo : int { No=1, Yes}

        protected void Page_Load(object sender, EventArgs e)
        {
           // HiddenField2.Value = "test";
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ViewState["CompanyIdData"] = Session["CompanyId"].ToString();
                ViewState["UserIdData"] = Session["UserId"].ToString();
            }

            if (ViewState["CompanyIdData"] != null && ViewState["UserIdData"].ToString() != "")
            {
                Session["CompanyId"] = ViewState["CompanyIdData"].ToString();
                Session["UserId"] = ViewState["UserIdData"].ToString();

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 12, 2) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                try
                {
                    LoadDDLMainCatregory();
                    MRN = mrnController.getMRNM(int.Parse(Request["id"].ToString()));

                    if(MRN != null)
                    {
                        dtExpectedDate.Text = MRN.ExpectedDate.ToString("MM/dd/yyyy");
                        txtMrnDescription.Text = MRN.Description;

                        DataTable dt = new DataTable();

                        dt.Columns.Add("MrndID");
                        dt.Columns.Add("MainCategoryId");
                        dt.Columns.Add("MainCategoryName");
                        dt.Columns.Add("SubCategoryId");
                        dt.Columns.Add("SubcategoryName");
                        dt.Columns.Add("ItemId");
                        dt.Columns.Add("ItemName");
                        dt.Columns.Add("ItemQuantity");
                        dt.Columns.Add("ItemDescription");

                        foreach (MrnDetails MRND in MRN.MrnDetails)
                        {
                            var dataRow = dt.NewRow();
                            dataRow[0] = MRND.Mrnd_ID;
                         //   dataRow[1] = MRND.CategoryIDca;
                            dataRow[1] = MRND.CategoryName;
                            //dataRow[3] = MRND.SubCategoryIDs;
                           // dataRow[4] = MRND.SubCategoryName;
                            dataRow[2] = MRND.ItemId;
                            //dataRow[6] = MRND.ItemName;
                            dataRow[3] = MRND.RequestedQty;
                            dataRow[4] = MRND.Description;

                            dt.Rows.Add(dataRow);
                        }

                        gvDatataTable.DataSource = dt;
                        gvDatataTable.DataBind();
                    }
                    dtExpectedDate.Text = MRN.ExpectedDate.ToString("MM/dd/yyyy");
                    txtMrnDescription.Text = MRN.Description;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }
        

        private void LoadDDLMainCatregory()
        {
            try
            {
                ddlMainCateGory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString()));
                ddlMainCateGory.DataValueField = "CategoryId";
                ddlMainCateGory.DataTextField = "CategoryName";
                ddlMainCateGory.DataBind();
                ddlMainCateGory.Items.Insert(0, new ListItem("Select Main Category", ""));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //---------------Load Sub Category DDL
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
                ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(SubCatId, int.Parse(Session["CompanyId"].ToString()));
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

        //---------------Load Items DDL
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

                    ddlItemName.DataSource = addItemController.FetchItemsByCategories(categoryId, subCategoryId, CompanyId).OrderBy(y => y.ItemId).ToList();
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
        
        

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateMRN.aspx");
        }

        //--------------------Proceed PR
        protected void btnSavePR_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtExpectedDate.Text!="")
                {
                    if (gvDatataTable.Rows.Count > 0)
                    {
                        MRN.Description = txtMrnDescription.Text;
                        MRN.ExpectedDate = DateTime.Parse(dtExpectedDate.Text);
                        

                        if (mrnController.updateMRN(MRN) > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); window.location = 'ViewMRN.aspx'; });   </script>", false);
                            //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#SuccessAlert').modal('show'); });   </script>", false);
                            clearAll();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error On Updating Material Request\"; $('#errorAlert').modal('show'); });   </script>", false);
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
            catch (Exception ex)
            {
                throw ex;
            }
            ddlItemName.Enabled = true;
            ddlSubCategory.Enabled = true;
            ddlMainCateGory.Enabled = true;
        }
        

        //----------------Dynamically Load To grid view PR Detail Data
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAdd.Text == "Add Item")
                {
                    if (txtQty.Text != "" && ddlMainCateGory.SelectedIndex != 0 && ddlSubCategory.SelectedIndex != 0 && ddlItemName.SelectedIndex != 0 && txtQty.Text != "")
                    {


                        DataTable dt = new DataTable();
                        dt.Columns.Add("MrndID");
                        dt.Columns.Add("MainCategoryId");
                        dt.Columns.Add("MainCategoryName");
                        dt.Columns.Add("SubCategoryId");
                        dt.Columns.Add("SubcategoryName");
                        dt.Columns.Add("ItemId");
                        dt.Columns.Add("ItemName");
                        dt.Columns.Add("ItemQuantity");
                        dt.Columns.Add("ItemDescription");
                        bool hasItem = false;
                        if (gvDatataTable.Rows.Count > 0)
                        {
                            foreach (GridViewRow row in gvDatataTable.Rows)
                            {
                                var dataRow = dt.NewRow();

                                if (row.Cells[5].Text == ddlItemName.SelectedValue.ToString())
                                {
                                    hasItem = true;
                                    break;
                                }
                                else
                                {
                                    for (int i = 0; i < gvDatataTable.Columns.Count - 2; i++)
                                    {
                                        dataRow[i] = row.Cells[i].Text;


                                    }
                                    dt.Rows.Add(dataRow);
                                }
                            }
                        }

                        if (!hasItem)
                        {
                            MrnDetails mrnDetails = new MrnDetails();
                            mrnDetails.MrnId = MRN.MrnID;
                            mrnDetails.ItemId = int.Parse(ddlItemName.SelectedValue.ToString());
                            mrnDetails.RequestedQty = int.Parse(txtQty.Text);
                            mrnDetails.Description = txtMRNDDescription.Text;
                            int result = mrnController.addMRND(mrnDetails);
                            if (result > 0)
                            {
                                DataRow NewRow = dt.NewRow();
                                NewRow[0] = result.ToString();
                                NewRow[1] = ddlMainCateGory.SelectedValue.ToString();
                                NewRow[2] = ddlMainCateGory.SelectedItem.ToString();
                                NewRow[3] = ddlSubCategory.SelectedValue.ToString();
                                NewRow[4] = ddlSubCategory.SelectedItem.ToString();
                                NewRow[5] = ddlItemName.SelectedValue.ToString();
                                NewRow[6] = ddlItemName.SelectedItem.ToString();
                                NewRow[7] = txtQty.Text;
                                NewRow[8] = txtMRNDDescription.Text;
                                dt.Rows.Add(NewRow);
                                gvDatataTable.DataSource = dt;
                                gvDatataTable.DataBind();

                                LoadDDLMainCatregory();
                                ddlMainCateGory.SelectedIndex = 0;
                                ddlSubCategory.SelectedIndex = 0;
                                ddlItemName.SelectedIndex = 0;
                                txtMRNDDescription.Text = "";
                                txtQty.Text = "";
                                ddlMainCateGory.Enabled = true;
                                ddlSubCategory.Enabled = true;
                                ddlItemName.Enabled = true;

                                clearFields();
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error On Adding Items\"; $('#errorAlert').modal('show'); });   </script>", false);
                            }
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
                        MrnDetails mrnDetails = new MrnDetails();
                        mrnDetails.Mrnd_ID = int.Parse(gvDatataTable.Rows[editRowIndex].Cells[0].Text);
                        mrnDetails.MrnId = MRN.MrnID;
                        mrnDetails.ItemId = int.Parse(ddlItemName.SelectedValue.ToString());
                        mrnDetails.RequestedQty = int.Parse(txtQty.Text);
                        mrnDetails.Description = txtMRNDDescription.Text;

                        if (mrnController.updateMRND(mrnDetails) > 0)
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("MrndID");
                            dt.Columns.Add("MainCategoryId");
                            dt.Columns.Add("MainCategoryName");
                            dt.Columns.Add("SubCategoryId");
                            dt.Columns.Add("SubcategoryName");
                            dt.Columns.Add("ItemId");
                            dt.Columns.Add("ItemName");
                            dt.Columns.Add("ItemQuantity");
                            dt.Columns.Add("ItemDescription");

                            if (gvDatataTable.Rows.Count > 0)
                            {
                                gvDatataTable.DeleteRow(editRowIndex);
                                gvDatataTable.DataBind();

                                if (gvDatataTable.Rows.Count > 0)
                                {
                                    foreach (GridViewRow row in gvDatataTable.Rows)
                                    {
                                        var dataRow = dt.NewRow();
                                        for (int i = 0; i < gvDatataTable.Columns.Count - 2; i++)
                                        {
                                            dataRow[i] = row.Cells[i].Text;
                                        }
                                        dt.Rows.Add(dataRow);
                                    }
                                }
                            }


                            DataRow NewRow = dt.NewRow();
                            NewRow[0] = mrnDetails.Mrnd_ID.ToString();
                            NewRow[1] = ddlMainCateGory.SelectedValue.ToString();
                            NewRow[2] = ddlMainCateGory.SelectedItem.ToString();
                            NewRow[3] = ddlSubCategory.SelectedValue.ToString();
                            NewRow[4] = ddlSubCategory.SelectedItem.ToString();
                            NewRow[5] = ddlItemName.SelectedValue.ToString();
                            NewRow[6] = ddlItemName.SelectedItem.ToString();
                            NewRow[7] = txtQty.Text;
                            NewRow[8] = txtMRNDDescription.Text;
                            dt.Rows.Add(NewRow);
                            gvDatataTable.DataSource = dt;
                            gvDatataTable.DataBind();

                            LoadDDLMainCatregory();
                            ddlMainCateGory.SelectedIndex = 0;
                            ddlSubCategory.SelectedIndex = 0;
                            ddlItemName.SelectedIndex = 0;
                            txtMRNDDescription.Text = "";
                            txtQty.Text = "";

                            ddlMainCateGory.Enabled = true;
                            ddlSubCategory.Enabled = true;
                            ddlItemName.Enabled = true;

                            clearFields();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error On Updating Items\"; $('#errorAlert').modal('show'); });   </script>", false);
                        }
                    }
                }

                DataTable dt1 = new DataTable();

                dt1.Columns.Add("MrndID");
                dt1.Columns.Add("MainCategoryId");
                dt1.Columns.Add("MainCategoryName");
                dt1.Columns.Add("SubCategoryId");
                dt1.Columns.Add("SubcategoryName");
                dt1.Columns.Add("ItemId");
                dt1.Columns.Add("ItemName");
                dt1.Columns.Add("ItemQuantity");
                dt1.Columns.Add("ItemDescription");
                List<MrnDetails> mrndetails = mrnController.fetchMrnDList(MRN.MrnID);
                foreach (MrnDetails MRND in mrndetails)
                {
                    var dataRow = dt1.NewRow();
                    dataRow[0] = MRND.Mrnd_ID;
                    //   dataRow[1] = MRND.CategoryIDca;
                    dataRow[1] = MRND.CategoryName;
                    //dataRow[3] = MRND.SubCategoryIDs;
                    // dataRow[4] = MRND.SubCategoryName;
                    dataRow[2] = MRND.ItemId;
                    //dataRow[6] = MRND.ItemName;
                    dataRow[3] = MRND.RequestedQty;
                    dataRow[4] = MRND.Description;

                    dt1.Rows.Add(dataRow);
                }

                gvDatataTable.DataSource = dt1;
                gvDatataTable.DataBind();
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


        }

        private void clearAll()
        {
            dtExpectedDate.Text = "";
            txtMrnDescription.Text = "";
            txtQty.Text = "";
            LoadDDLMainCatregory();
            ddlMainCateGory.SelectedIndex = 0;
            ddlItemName.Enabled = true;
            ddlSubCategory.Enabled = true;
            ddlMainCateGory.Enabled = true;
            btnAdd.Text = "Add Item";
            ddlItemName.Items.Clear();
            ddlSubCategory.Items.Clear();

            gvDatataTable.DataSource = null;
            gvDatataTable.DataBind();

        }


        protected void btnEditItem_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                editRowIndex = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                GridViewRow row = gvDatataTable.Rows[editRowIndex];

                LoadDDLMainCatregory();
                ddlMainCateGory.SelectedValue = row.Cells[1].Text.ToString();
                ddlMainCateGory_SelectedIndexChanged(null, null);
                ddlSubCategory.SelectedValue = row.Cells[3].Text.ToString();
                ddlSubCategory_SelectedIndexChanged(null, null);
                ddlItemName.SelectedValue = row.Cells[5].Text.ToString();
                txtQty.Text = row.Cells[7].Text.ToString();
                txtMRNDDescription.Text = row.Cells[8].Text.ToString();
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

                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;

                if (mrnController.DeleteMRND(int.Parse(gvDatataTable.Rows[x].Cells[0].Text)) > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("MrndID");
                    dt.Columns.Add("MainCategoryId");
                    dt.Columns.Add("MainCategoryName");
                    dt.Columns.Add("SubCategoryId");
                    dt.Columns.Add("SubcategoryName");
                    dt.Columns.Add("ItemId");
                    dt.Columns.Add("ItemName");
                    dt.Columns.Add("ItemQuantity");
                    dt.Columns.Add("ItemDescription");

                    if (gvDatataTable.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvDatataTable.Rows)
                        {
                            var dataRow = dt.NewRow();
                            for (int i = 0; i < gvDatataTable.Columns.Count - 2; i++)
                            {
                                dataRow[i] = row.Cells[i].Text;
                            }
                            dt.Rows.Add(dataRow);
                        }
                    }
                    dt.Rows.RemoveAt(x);
                    gvDatataTable.DataSource = dt;
                    gvDatataTable.DataBind();
                    clearFields();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error On Deleting Items\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void gvDatataTable_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnOK_Click1(object sender, EventArgs e)
        {
            Response.Redirect("ViewMRN.aspx");
        }
    }
}