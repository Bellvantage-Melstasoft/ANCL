using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using System.IO;
using CLibrary.Domain;


namespace BiddingSystem
{
    public partial class InventoryReport : System.Web.UI.Page
    {
        private string script = "";
        static string UserId = string.Empty;
        int CompanyId = 0;
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {

                    UserId = Session["UserId"].ToString();
                    CompanyId = int.Parse(Session["CompanyId"].ToString());
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 10, 4) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                    {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }


            }
            catch (Exception)
            {
            }


            ScriptManager.RegisterStartupScript(Updatepanel1, this.Updatepanel1.GetType(), "YourUniqueScriptKey", script + "InitClient();", true);


        }

        protected void btnSearchBy_Click(object sender, EventArgs e)
        {


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearchLevel_Click(object sender, EventArgs e)
        {
            if (txtLevel.Text != string.Empty)
            {
                search(0, 0, int.Parse(txtLevel.Text));
            }
        }

        protected void btnSearchCatagory_Click(object sender, EventArgs e)
        {
            search(int.Parse(ddlCatagory.SelectedValue), 0, 0);
        }

        protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlSearchBy.SelectedIndex == 2)
            {
                dvLevel.Visible = true;
                dvCatagory.Visible = false;
                dvSubCatagory.Visible = false;
            }
            else if (ddlSearchBy.SelectedIndex == 0)
            {
                dvLevel.Visible = false;
                dvCatagory.Visible = false;
                dvSubCatagory.Visible = false;
            }
            else if (ddlSearchBy.SelectedIndex == 1)
            {
                LoadCategory();
                dvCatagory.Visible = true;
                dvSubCatagory.Visible = true;
                dvLevel.Visible = false;
            }

        }

        protected void btnGrnStatusSearch_Click(object sender, EventArgs e)
        {

        }

        private void LoadCategory()
        {
            try
            {

                ItemCategoryMasterController itemCategoryMasterController = ControllerFactory.CreateItemCategoryMasterController();
                ddlCatagory.DataSource = itemCategoryMasterController.FetchItemCategoryfORSubCategoryCreationList(int.Parse(Session["CompanyId"].ToString()));
                ddlCatagory.DataValueField = "CategoryId";
                ddlCatagory.DataTextField = "CategoryName";
                ddlCatagory.DataBind();
                ddlCatagory.Items.Insert(0, new ListItem("Select Category", ""));
            }
            catch (Exception ex)
            {

            }
        }


        private void LoadProduct()
        {
            try
            {
                ItemSubCategoryMasterController itemSubCategoryMasterController = ControllerFactory.CreateItemSubCategoryMasterController();
                ddlSubCatagory.DataSource = itemSubCategoryMasterController.FetchItemSubCategoryByCategoryId(int.Parse(ddlCatagory.SelectedValue));
                ddlSubCatagory.DataValueField = "SubCategoryId";
                ddlSubCatagory.DataTextField = "SubCategoryName";
                ddlSubCatagory.DataBind();
                ddlSubCatagory.Items.Insert(0, new ListItem("Select Sub Category", ""));
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubCatagory.SelectedIndex != 0)
            {
                LoadProduct();
            }
            else
            {
                ddlSubCatagory.Items.Clear();
            }

        }

        protected void ddlSubCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearchSubCatagory_Click(object sender, EventArgs e)
        {
            search(int.Parse(ddlCatagory.SelectedValue), int.Parse(ddlSubCatagory.SelectedValue), 0);
        }

        public void search(int cat, int sub, int level)
        {
            List<AddItem> List = new List<AddItem>();
            AddItemController addItemController = ControllerFactory.CreateAddItemController();
            List = addItemController.ItemsInInventoryByCatagory(cat, sub, level);
            if (List.Count > 0 )
            {
                btnCreatePR.Visible = true;
            }
            else {
                btnCreatePR.Visible = false;
            }
            gvReport.DataSource = List;
            gvReport.DataBind();
        }

        protected void lbtnViewDetails_Click(object sender, EventArgs e)
        {
            try
            {
                //view Specification

                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvReport.Rows[x].Cells[7].Text);
                int companyId = int.Parse(gvReport.Rows[x].Cells[8].Text);
                AddItemBOMController addItemBOMController = ControllerFactory.CreateAddItemBOMController();
                List<AddItemBOM> BOMList = addItemBOMController.GetBOMListByItemId(companyId, itemId);
                gvTempBoms.DataSource = BOMList;
                gvTempBoms.DataBind();
                //  imageid.Src = "~/LoginResources/images/noItem.png" + "?" + LocalTime.Now.Ticks.ToString();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $('#myModalViewBom').modal('show'); </script>", false);
                Session["CompanyId"] = companyId;
                Session["UserId"] = UserId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {

        }


        public string ValidateDataGrid()
        {

            string errorMessage1 = "";
            string errorMessage2 = "";

            for (int i = 0; i < gvReport.Rows.Count; i++)
            {
                TextBox quantity = (TextBox)gvReport.Rows[i].FindControl("txtQuantity");

                CheckBox itemSelect = (CheckBox)gvReport.Rows[i].FindControl("chkSelect");

                if ((quantity.Text != "") && !(itemSelect.Checked))
                {
                    errorMessage1 = "Please select the product before entering Quantities</br>";
                }
                else if ((quantity.Text == "") && (itemSelect.Checked))
                {
                    errorMessage2 = "Qty cannot be zero </br>";
                }

            }

            btnCreatePR.Enabled = true;

            return errorMessage1 + errorMessage2;
        }


        public List<CompanyPurchaseRequestNote.TempDataSet> GetLineItems()
        {
            List<CompanyPurchaseRequestNote.TempDataSet> tempDataSetlist = new List<CompanyPurchaseRequestNote.TempDataSet>();

            //go through all line items
            for (int i = 0; i < gvReport.Rows.Count; i++)
            {
                if (((CheckBox)gvReport.Rows[i].FindControl("chkSelect")).Checked)
                {

                    int itemId = 0;
                    int qty = 0;


                    if (((TextBox)gvReport.Rows[i].FindControl("txtQuantity")).Text != "")
                    {
                        qty = int.Parse(((TextBox)gvReport.Rows[i].FindControl("txtQuantity")).Text);
                    }

                    itemId = int.Parse(gvReport.Rows[i].Cells[7].Text);

                    if (qty != 0 || itemId != 0)
                    {
                        // add line item to the list
                        CompanyPurchaseRequestNote.TempDataSet tempDataSet = new CompanyPurchaseRequestNote.TempDataSet();
                        AddItemController addItemController = ControllerFactory.CreateAddItemController();
                        AddItem item = addItemController.FetchItemObj(itemId);
                        tempDataSet.MainCategoryId = item.CategoryId;
                        tempDataSet.MainCategoryName = item.CategoryName;
                        tempDataSet.SubCategoryId = item.SubCategoryId;
                        tempDataSet.SubcategoryName = item.SubCategoryName;
                        tempDataSet.ItemId = itemId;
                        tempDataSet.ItemName = item.ItemName;
                        tempDataSet.ItemQuantity = qty;
                        tempDataSet.EstimatedAmount = 0;

                        tempDataSetlist.Add(tempDataSet);
                    }
                }
            }

            return tempDataSetlist;

        }


        protected void btnCreatePR_Click(object sender, EventArgs e)
        {

            List<int> itemIdList = new List<int>();
            AddItemController addItemController = ControllerFactory.CreateAddItemController();

            List<CompanyPurchaseRequestNote.TempDataSet> tempDataSetlist = new List<CompanyPurchaseRequestNote.TempDataSet>();

            if (ValidateDataGrid() == "")
            {
                tempDataSetlist = GetLineItems();
                Session["FinalListPRCreate"] = tempDataSetlist;
                Response.Redirect("CompanyPurchaseRequestNote.aspx");
            }
            else
            {
              //  lbMessage.Text = ValidateDataGrid();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"" + ValidateDataGrid() +" \"; $('#errorAlert').modal('show'); });   </script>", false);

                btnCreatePR.Enabled = true;

            }

        }

    }
}