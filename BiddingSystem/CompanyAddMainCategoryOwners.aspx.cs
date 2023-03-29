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
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Data;

namespace BiddingSystem
{
    public partial class CompanyAddMainCategoryOwners : System.Web.UI.Page
    {
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemCategoryOwnerController itemCategoryOwnerController = ControllerFactory.CreateItemCategoryOwnerController();
        public static List<ItemCategory> getAllMaincategoryList = new List<ItemCategory>();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        public static List<CompanyLogin> CompanyLoginUserList = new List<CompanyLogin>();
       // static int UserId;
       // int CompanyId = 0;
        public static List<OwnerType> listOwnerType = new List<OwnerType>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefItemCategory";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabItemCategory";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyAddMainCategoryOwners.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "addMainCategoryOwnerLink";

                   // CompanyId = int.Parse(Session["CompanyId"].ToString());
                 //   UserId = Convert.ToInt32(Session["UserId"].ToString());

                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(Convert.ToInt32(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 4, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                    CompanyLoginUserList = companyLoginController.GetAllUserList();
                    LoadOwnerType();
                    getAllMaincategoryList = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString()));
                    LoadMainCategoryDetails(getAllMaincategoryList);
                    LoadCatergoryDropdown(getAllMaincategoryList);
                    LoadUsers(CompanyLoginUserList);
                  
                    
                }
            }
            catch (Exception)
            {

            }
        }

        private void LoadOwnerType()
        {
            listOwnerType = new List<OwnerType>();
            //listOwnerType.Add(new OwnerType { Id = "SK", Name = "Store Keeper" });
            listOwnerType.Add(new OwnerType { Id = "PO", Name = "Purchase Officer" });
            listOwnerType.Add(new OwnerType { Id = "CO", Name = "Catergory Owner" });
            //ddlOwnerType.Items.Add(new ListItem("Store Keeper", "SK"));
            //ddlOwnerType.Items.Add(new ListItem("Purchase Officer", "PO"));
            //ddlOwnerType.Items.Add(new ListItem("Catergory Owner", "CO"));
            ddlOwnerType.DataSource = listOwnerType;
            ddlOwnerType.DataValueField = "Id";
            ddlOwnerType.DataTextField = "Name";
            ddlOwnerType.DataBind();
        }

        private void LoadCatergoryDropdown(List<ItemCategory> getAllMaincategoryList)
        {
            ddlCategoryList.DataSource = getAllMaincategoryList;
            ddlCategoryList.DataValueField = "CategoryId";
            ddlCategoryList.DataTextField = "CategoryName";
            ddlCategoryList.DataBind();
        }

        private void LoadUsers(List<CompanyLogin> Users)
        {
            try
            {
                ddlUserId.DataSource = Users;
                ddlUserId.DataValueField = "UserId";
                ddlUserId.DataTextField = "FirstName";
                ddlUserId.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //----------------------Save Main Category
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                int save = 0;

                if (effectiveDate.Text == "")
                {
                    DisplayMessage("Error occured - Please fill all details", true);
                }
                else
                {
                    if (hndAction.Value == "Save")
                    {
                        save = itemCategoryOwnerController.ManageItemCategoryOwners(0, Convert.ToInt32(ddlCategoryList.SelectedValue), ddlOwnerType.SelectedValue, Convert.ToInt32(ddlUserId.SelectedValue), Convert.ToDateTime(effectiveDate.Text), Convert.ToInt32(Session["UserId"].ToString()), LocalTime.Now, "Save");
                        if (save > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                                "none",
                                "<script>    "+
                                "$(document).ready(function () { "+
                                " swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });  "+
                                " </script>",
                                false);
                            RefreshGrid();
                            ClearFields();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during saving ', showConfirmButton: true,timer: 4000}); });   </script>", false);

                        }
                    }
                    else
                    {
                        save = itemCategoryOwnerController.ManageItemCategoryOwners(Convert.ToInt32(hndEditRowId.Value), Convert.ToInt32(ddlCategoryList.SelectedValue), ddlOwnerType.SelectedValue, Convert.ToInt32(ddlUserId.SelectedValue), Convert.ToDateTime(effectiveDate.Text), Convert.ToInt32(Session["UserId"].ToString()), LocalTime.Now, "Update");
                        if (save > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                                "none",
                                "<script> "+
                                " $(document).ready(function () { "+
                                " swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); }); "+
                                "</script>",
                                false);
                            RefreshGrid();
                            ClearFields();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during saving ', showConfirmButton: true,timer: 4000}); });   </script>", false);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        private void RefreshGrid()
        {
            getAllMaincategoryList = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString()));
            LoadMainCategoryDetails(getAllMaincategoryList);
        }

        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int save = 0;
                save = itemCategoryOwnerController.DeleteItemCategoryOwners(Convert.ToInt32(hndDeleteRowId.Value));
                if (save > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                        "none",
                        "<script> "+
                        " $(document).ready(function () { " +
                        " swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500});    }); " +
                        " </script>",
                        false);
                    RefreshGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during deleting ', showConfirmButton: true,timer: 4000}); });   </script>", false);

                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //-----------------------Load Gv Data
        private void LoadMainCategoryDetails(List<ItemCategory> itemCategory)
        {
            try
            {
                List<ItemCategoryOwners> itemCategoryOwnerList = new List<ItemCategoryOwners>();
                gvMainCategory.DataSource = itemCategory;
                gvMainCategory.DataBind();
                for (int i = 0; i < gvMainCategory.Rows.Count; i++)
                {
                    int purchasingOfficerId = 0;
                    int catergoryOwnerId = 0;
                    ItemCategoryOwners obj2 = itemCategoryOwnerController.FetchItemCategoryOwnersByCategoryId(itemCategory[i].CategoryId, "PO");
                    ItemCategoryOwners obj3 = itemCategoryOwnerController.FetchItemCategoryOwnersByCategoryId(itemCategory[i].CategoryId, "CO");
                    purchasingOfficerId = obj2.UserId;
                    catergoryOwnerId = obj3.UserId;
                    ((Literal)((GridViewRow)gvMainCategory.Rows[i]).FindControl("ltPurchasingOfficer")).Text = purchasingOfficerId.ToString();
                    ((Literal)((GridViewRow)gvMainCategory.Rows[i]).FindControl("ltCatergoryOwner")).Text = catergoryOwnerId.ToString();
                    string purchasingOwner = string.Empty;
                    string catergoryOwner = string.Empty;
                    purchasingOwner = CompanyLoginUserList.Find(x => x.UserId == purchasingOfficerId) != null ? CompanyLoginUserList.Find(x => x.UserId == purchasingOfficerId).Username : "";
                    catergoryOwner = CompanyLoginUserList.Find(x => x.UserId == catergoryOwnerId) != null ? CompanyLoginUserList.Find(x => x.UserId == catergoryOwnerId).Username : "";                 
                    
                    ((Literal)((GridViewRow)gvMainCategory.Rows[i]).FindControl("ltPurchasingOfficerName")).Text = purchasingOwner;
                    if (obj2.EffectiveDate != DateTime.MinValue)
                    {
                        ((Literal)((GridViewRow)gvMainCategory.Rows[i]).FindControl("ltPOEffectiveDate")).Text = obj2.EffectiveDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                    }
                     ((Literal)((GridViewRow)gvMainCategory.Rows[i]).FindControl("ltCatergoryOwnerName")).Text = catergoryOwner;
                    if (obj3.EffectiveDate != DateTime.MinValue)
                    {
                        ((Literal)((GridViewRow)gvMainCategory.Rows[i]).FindControl("ltCOEffectiveDate")).Text = obj3.EffectiveDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While loading gridview', showConfirmButton: true,timer: 4000}); });   </script>", false);
                throw ex;
            }
        }


        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string categoryId = gvMainCategory.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvCategoryOwnerHistory = e.Row.FindControl("gvCategoryOwnerHistory") as GridView;
                gvCategoryOwnerHistory.DataSource = GetCategoryOwnerById(Convert.ToInt32(categoryId));
                gvCategoryOwnerHistory.DataBind();
            }
        }

        private List<ItemCategoryOwners> GetCategoryOwnerById(int categoryId)
        {
            try
            {
                //DataTable dt = new DataTable();
                //dt.Columns.AddRange(new DataColumn[4] { new DataColumn("APPROVAL_LIMIT_ID"), new DataColumn("MINIMUM_AMOUNT"), new DataColumn("MAXIMUM_AMOUNT"), new DataColumn("TPTYPE") });
                return itemCategoryOwnerController.FetchItemCategoryOwnersByCategoryId(categoryId);
                // return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        protected void gvMainCategory_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvMainCategory.PageIndex = e.NewPageIndex;
                gvMainCategory.DataSource = getAllMaincategoryList;
                gvMainCategory.DataBind();
            }
            catch (Exception) {

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            hndAction.Value = "Save";
            effectiveDate.Text = "";
            hndEditRowId.Value = "";
        }
    }

    [Serializable()]
    public class OwnerType
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }



}