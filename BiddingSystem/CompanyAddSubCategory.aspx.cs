using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class CompanyAddSubCategory : System.Web.UI.Page
    {

       // int CompanyId = 0;
      //  static string UserId = string.Empty;
       // static int SubCategoryId = 0;
       // static string SubCategoryName = string.Empty;
       // static int categoryId = 0;
        //static string categoryName = string.Empty;
        //private DateTime createdDate;
        //private string createdBy = string.Empty;
        //private DateTime updatedDate;
        //private string updatedBy = string.Empty;
        //private int isActive = 0;
       
       
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemCategoryMasterController itemCategoryMasterController = ControllerFactory.CreateItemCategoryMasterController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        ItemSubCategoryMasterController itemSubCategoryMasterController = ControllerFactory.CreateItemSubCategoryMasterController();

       // public static List<ItemSubCategory> ListSubCategory = new List<ItemSubCategory>();
       // List<ItemSubCategory> getAllSubcategoryList = new List<ItemSubCategory>();
     //   public List<string> subCategoryList = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefItemCategory";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabItemCategory";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyAddSubCategory.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "addSubCategoryLink";

                 //   CompanyId = int.Parse(Session["CompanyId"].ToString());
                  //  UserId = Session["UserId"].ToString();

                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 3, 2) && companyLogin.Usertype != "S" )&& companyLogin.Usertype != "GA")
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
                    GvLloadDetails();
                    LoadMasterDDLMainCatregory();
                    LoadDDLMainCatregory();
                }
            }
            catch (Exception)
            {


            }
        }

        //---------------------Load Main Category DDL
        private void LoadDDLMainCatregory()
        {
            try
            {
                ddlMainCategory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString())).Where(x => x.IsActive == 1).ToList();
                ddlMainCategory.DataValueField = "CategoryId";
                ddlMainCategory.DataTextField = "CategoryName";
                ddlMainCategory.DataBind();
                ddlMainCategory.Items.Insert(0, new ListItem("Select Main Category", ""));
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadMasterDDLMainCatregory()
        {
            try
            {
                ddlItemMasterCategory.DataSource = itemCategoryMasterController.FetchItemCategoryfORSubCategoryCreationList(int.Parse(Session["CompanyId"].ToString()));
                ddlItemMasterCategory.DataValueField = "CategoryId";
                ddlItemMasterCategory.DataTextField = "CategoryName";
                ddlItemMasterCategory.DataBind();
                ddlItemMasterCategory.Items.Insert(0, new ListItem("Select Master Main Category", ""));
            }
            catch (Exception ex)
            {

            }
        }
        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
        }
        //-----------------------------Load Grid View
        private void GvLloadDetails()
        {
            try
            {
                List<ItemSubCategory> ListSubCategory = itemSubCategoryController.FetchItemSubCategoryList(int.Parse(Session["CompanyId"].ToString()));
                ViewState["ListSubCategory"] = new JavaScriptSerializer().Serialize(ListSubCategory);
                gvSubCategory.DataSource = ListSubCategory;
                gvSubCategory.DataBind();
            }
            catch (Exception ex)
            {


            }
        }
        

            protected void search_click(object sender, EventArgs e) {
            string text = txtSearch.Text;

            List<ItemSubCategory> SearchListSubCategory = itemSubCategoryController.SearchSubCategoryList(int.Parse(Session["CompanyId"].ToString()), text);
            gvSubCategory.DataSource = SearchListSubCategory;
            gvSubCategory.DataBind();
        }


            //---------------------------Save Sub Category Data
            protected void btnSave_Click(object sender, EventArgs e)
        {

            int chkactive = 0;
            if (chkIsavtive.Checked)
            {
                chkactive = 1;
            }
            try
            {
                if (btnSave.Text == "Save")
                {
                    int masterSubCategoryId = itemSubCategoryMasterController.SaveItemSubCategory(txtSubCategoryName.Text, int.Parse(ddlMainCategory.SelectedValue), LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), chkactive);

                    if (masterSubCategoryId != -1)
                    {
                        if (masterSubCategoryId > 0)
                        {
                            int saveItemSubCategory = itemSubCategoryController.SaveItemSubCategory(int.Parse(Session["CompanyId"].ToString()), masterSubCategoryId, txtSubCategoryName.Text, int.Parse(ddlMainCategory.SelectedValue), LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), chkactive);

                            if (saveItemSubCategory > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                                //DisplayMessage("Sub Category has been Created Successfully", false);
                                GvLloadDetails();
                                ClearFields();
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on create Sub Category', showConfirmButton: true,timer: 4000}); });   </script>", false);
                                //DisplayMessage("Error on create Sub Category", true);
                            }


                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on create Master Sub Category', showConfirmButton: true,timer: 4000}); });   </script>", false);
                            //DisplayMessage("Error on create Master Sub Category", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Sub Category name already exists', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        //DisplayMessage("Sub Category name already exists", true);
                    }
                }
                if (btnSave.Text == "Update")
                {
                    int updateItemSubCategory = itemSubCategoryController.UpdateItemSubCategory(int.Parse(Session["CompanyId"].ToString()), int.Parse(ViewState["SubCategoryId"].ToString()), txtSubCategoryName.Text, int.Parse(ddlMainCategory.SelectedValue), LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), chkactive);
                    if (updateItemSubCategory != -1)
                    {
                        if (updateItemSubCategory > 0)
                        {
                            GvLloadDetails();
                            ClearFields();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                            //DisplayMessage("Sub Category has been Updated Successfully", false);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on update Sub Category', showConfirmButton: true,timer: 4000}); });   </script>", false);
                            //DisplayMessage("Error on update Sub Category", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Sub Category name already exists', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        //DisplayMessage("Sub Category name already exists", true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------Claer Fields
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        //-------------------Edit Saved Data
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDDLMainCatregory();
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                ViewState["SubCategoryId"] = int.Parse(gvSubCategory.Rows[x].Cells[1].Text);
                ViewState["SubCategoryName"] = gvSubCategory.Rows[x].Cells[2].Text;
                ViewState["categoryId"] = int.Parse(gvSubCategory.Rows[x].Cells[3].Text);
                ViewState["categoryName"] = gvSubCategory.Rows[x].Cells[4].Text;
                ViewState["createdDate"] = DateTime.Parse(gvSubCategory.Rows[x].Cells[5].Text);
                ViewState["createdBy"] = gvSubCategory.Rows[x].Cells[6].Text;
                ViewState["updatedDate"] = DateTime.Parse(gvSubCategory.Rows[x].Cells[7].Text);
                ViewState["updatedBy"] = gvSubCategory.Rows[x].Cells[8].Text;
                ViewState["isActive"] = int.Parse(gvSubCategory.Rows[x].Cells[9].Text);
                ddlMainCategory.SelectedValue = ViewState["categoryId"].ToString();
                txtSubCategoryName.Text = ViewState["SubCategoryName"].ToString();
                HiddenField1.Value = ViewState["SubCategoryId"].ToString();
                btnSave.Text = "Update";
                if (int.Parse(ViewState["isActive"].ToString()) == 1)
                {
                    chkIsavtive.Checked = true;
                }
                else
                {
                    chkIsavtive.Checked = false;
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { document.body.scrollTop = 50; document.documentElement.scrollTop = 50;});   </script>", false);

            }
            catch (Exception ex)
            {


            }
        }

        //-----------------Delete Saved Data
        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int updateStatus = 0;

                string subcategoryId = hdnSubCategoryId.Value;
                string categoryId = hdnCategoryid.Value;

                if (subcategoryId != "" && subcategoryId != null && categoryId != null && categoryId != "" && hdnStatus.Value != "" && hdnStatus.Value != null)
                {
                    if (hdnStatus.Value == "Yes")
                    {
                        updateStatus = itemSubCategoryController.UpdateItemSubCategoryStatus(int.Parse(Session["CompanyId"].ToString()), int.Parse(categoryId), int.Parse(subcategoryId), 0);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("Subcategory has been Deactivated successfully", false);
                    }
                    if (hdnStatus.Value == "No")
                    {
                        updateStatus = itemSubCategoryController.UpdateItemSubCategoryStatus(int.Parse(Session["CompanyId"].ToString()), int.Parse(categoryId), int.Parse(subcategoryId), 1);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("Subcategory has been Activated successfully", false);
                    }

                    if (updateStatus > 0)
                    {
                        GvLloadDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on Delete Subcategory', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        //DisplayMessage("Error on Delete Subcategory", true);
                    }
                    ClearFields();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please Select Subcategory to Delete', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    //DisplayMessage("Please Select Subcategory to Delete", true);
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void ClearFields()
        {
            txtSubCategoryName.Text = "";
            ddlMainCategory.SelectedIndex = 0;
            chkIsavtive.Checked = true;
            btnSave.Text = "Save";
            hdnCategoryid.Value = null;
            hdnSubCategoryId.Value = null;
            hdnStatus.Value = null;
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

        protected void ddlMainCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                List<ItemSubCategory> getAllSubcategoryList = itemSubCategoryController.FetchItemSubCategoryByCategoryId(int.Parse(ddlMainCategory.SelectedValue), int.Parse(Session["CompanyId"].ToString()));
                foreach (var item in getAllSubcategoryList)
                {
                  //  subCategoryList.Add(item.SubCategoryName);
                }
            }
            catch (Exception)
            {

            }
        }
        public string getJsonSubCategoryList()
        {
            List<string> subCategoryList = new List<string>();
            var DataList = subCategoryList;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }

        protected void gvSubCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                List<ItemSubCategory> ListSubCategory = new JavaScriptSerializer().Deserialize<List<ItemSubCategory>>(ViewState["ListSubCategory"].ToString());
                gvSubCategory.PageIndex = e.NewPageIndex;
                gvSubCategory.DataSource = ListSubCategory;
                gvSubCategory.DataBind();
            }
            catch (Exception)
            {


            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<ItemSubCategoryMaster> fetchMasterSubCategories = itemSubCategoryMasterController.searchSubCategoryNameList(txtFindCategortName.Text, int.Parse(ddlItemMasterCategory.SelectedValue), int.Parse(Session["CompanyId"].ToString()));

                gvMasterSubCategoryList.DataSource = fetchMasterSubCategories;
                gvMasterSubCategoryList.DataBind();

                if (gvMasterSubCategoryList.Rows.Count == 0)
                {
                    ddlMainCategory.SelectedValue = ddlItemMasterCategory.SelectedValue;
                    txtSubCategoryName.Text = txtFindCategortName.Text;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnTake_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int categoryId = int.Parse(gvMasterSubCategoryList.Rows[x].Cells[0].Text);
                int subcategoryId = int.Parse(gvMasterSubCategoryList.Rows[x].Cells[1].Text);

                ItemSubCategoryMaster itemSubCategoryMaster = itemSubCategoryMasterController.FetchItemSubCategoryListByIdObj(subcategoryId);
                if (itemSubCategoryMaster.SubCategoryId > 0)
                {
                    int saveItemSubCategory = itemSubCategoryController.SaveItemSubCategory(int.Parse(Session["CompanyId"].ToString()), itemSubCategoryMaster.SubCategoryId, itemSubCategoryMaster.SubCategoryName, itemSubCategoryMaster.CategoryId, LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), 1);
                    if (saveItemSubCategory > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("Sub category has been clone successfully", false);
                        GvLloadDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on cloning Sub category', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        //DisplayMessage("Error on cloning Sub category", true);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnTake_Click1(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int categoryId = int.Parse(gvMasterSubCategoryList.Rows[x].Cells[0].Text);
                int subCategoryId = int.Parse(gvMasterSubCategoryList.Rows[x].Cells[1].Text);

                ItemSubCategoryMaster itemSubCategoryMasterObj = itemSubCategoryMasterController.FetchItemSubCategoryListByIdObj(subCategoryId);
                if (itemSubCategoryMasterObj.SubCategoryId > 0)
                {
                    int saveItemSubCategory = itemSubCategoryController.SaveItemSubCategory(int.Parse(Session["CompanyId"].ToString()), itemSubCategoryMasterObj.SubCategoryId, itemSubCategoryMasterObj.SubCategoryName, categoryId, LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), 1);
                    if (saveItemSubCategory > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("Sub Category has been clone successfully", false);
                        GvLloadDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on cloning Sub category', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        //DisplayMessage("Error on cloning Sub category", true);
                    }
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void gvMasterSubCategoryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvSubCategory_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            try {
                List<ItemSubCategory> ListSubCategory = new JavaScriptSerializer().Deserialize<List<ItemSubCategory>>(ViewState["ListSubCategory"].ToString());
                gvSubCategory.PageIndex = e.NewPageIndex;
                gvSubCategory.DataSource = ListSubCategory;
                gvSubCategory.DataBind();
            }
            catch (Exception)
            {


            }
        }
    }
}