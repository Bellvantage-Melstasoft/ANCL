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
using System.Globalization;

namespace BiddingSystem {
    public partial class CompanyAddMainCategoryOwners2 : System.Web.UI.Page {
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemCategoryOwnerController itemCategoryOwnerController = ControllerFactory.CreateItemCategoryOwnerController();
        ItemCategoryOwners2Controller itemCategoryOwners2Controller = ControllerFactory.CreateItemCategoryOwners2Controller();
        // public static List<ItemCategory> getAllMaincategoryList = new List<ItemCategory>();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        // public static List<CompanyLogin> CompanyLoginUserList = new List<CompanyLogin>();
        // static int UserId;
        // int CompanyId = 0;
        // public static List<OwnerType> listOwnerType = new List<OwnerType>();

        protected void Page_Load(object sender, EventArgs e) {
            try {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefItemCategory";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabItemCategory";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyAddMainCategoryOwners2.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "addMainCategoryOwnerLink";

                    // CompanyId = int.Parse(Session["CompanyId"].ToString());
                    //   UserId = Convert.ToInt32(Session["UserId"].ToString());

                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(Convert.ToInt32(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 4, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else {
                    Response.Redirect("LoginPage.aspx");
                }

                msg.Visible = false;
                if (!IsPostBack) {
                    var CompanyLoginUserList = companyLoginController.GetAllUserList();
                    ViewState["CompanyLoginUserList"] = new JavaScriptSerializer().Serialize(CompanyLoginUserList);
                    LoadOwnerType();
                    // LoadCategoryDetails();
                    var getAllMaincategoryList = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString()));
                    ViewState["getAllMaincategoryList"] = new JavaScriptSerializer().Serialize(getAllMaincategoryList);
                    //  LoadMainCategoryDetails(getAllMaincategoryList);
                    LoadCatergoryDropdown(getAllMaincategoryList);
                    LoadUsers(CompanyLoginUserList);
                    //   LoadCategoryDetails();
                    int categoryId = int.Parse(ddlCategoryList.SelectedValue);
                    List<ItemCategoryOwners2> CategoryOwners = itemCategoryOwners2Controller.FetchCategoryOwners(categoryId);
                    gvCategoryOwnerDetail.DataSource = CategoryOwners;
                    gvCategoryOwnerDetail.DataBind();

                    gvPurchaseOfficerDetails.DataSource = itemCategoryOwners2Controller.FetchPurchaseOfficers(categoryId);
                    gvPurchaseOfficerDetails.DataBind();
                    CurrentCO();
                    CurrentPO();

                    //lblCurrentCO.Text = string.Join(",", CurrentCO);
                }
            }
            catch (Exception ex) {

            }
        }

        private void CurrentCO() {
            int categoryId = int.Parse(ddlCategoryList.SelectedValue);
            List<ItemCategoryOwners2> CategoryOwners = itemCategoryOwners2Controller.FetchCategoryOwnerNames(categoryId);
            //var userName = CategoryOwners.Where(x => x.IsActive == 1 && x.EffectiveDate <= LocalTime.Now).OrderByDescending(T => T.EffectiveDate).Take(1).Select(d => d.UserName);
            List<String> Names = new List<String>();
            for (int i = 0; i < CategoryOwners.Count; i++) {
                String UserName = CategoryOwners[i].UserName;
                Names.Add(UserName);
            }

            lblCurrentCO.Text = string.Join(", ", Names);
        }

        private void CurrentPO() {
            int categoryId = int.Parse(ddlCategoryList.SelectedValue);
            List<ItemCategoryOwners2> PurchaseOfficers = itemCategoryOwners2Controller.FetchPurchaseOfficerNames(categoryId);
            //var userName = CategoryOwners.Where(x => x.IsActive == 1 && x.EffectiveDate <= LocalTime.Now).OrderByDescending(T => T.EffectiveDate).Take(1).Select(d => d.UserName);
            List<String> Names = new List<String>();
            for (int i = 0; i < PurchaseOfficers.Count; i++) {
                String UserName = PurchaseOfficers[i].UserName;
                Names.Add(UserName);
            }

            lblCurrentPO.Text = string.Join(", ", Names);
        }

        private void LoadOwnerType() {
            List<OwnerType> listOwnerType = new List<OwnerType>();
            //listOwnerType.Add(new OwnerType { Id = "SK", Name = "Store Keeper" });
            listOwnerType.Add(new OwnerType { Id = "PO", Name = "Purchase Officer" });
            listOwnerType.Add(new OwnerType { Id = "CO", Name = "Catergory Owner" });

            ViewState["listOwnerType"] = listOwnerType;
            //ddlOwnerType.Items.Add(new ListItem("Store Keeper", "SK"));
            //ddlOwnerType.Items.Add(new ListItem("Purchase Officer", "PO"));
            //ddlOwnerType.Items.Add(new ListItem("Catergory Owner", "CO"));
            ddlOwnerType.DataSource = listOwnerType;
            ddlOwnerType.DataValueField = "Id";
            ddlOwnerType.DataTextField = "Name";
            ddlOwnerType.DataBind();


        }


        private void LoadCatergoryDropdown(List<ItemCategory> getAllMaincategoryList) {
            ddlCategoryList.DataSource = getAllMaincategoryList;
            ddlCategoryList.DataValueField = "CategoryId";
            ddlCategoryList.DataTextField = "CategoryName";
            ddlCategoryList.DataBind();
        }

        private void LoadUsers(List<CompanyLogin> Users) {
            try {
                ddlUser.DataSource = Users;
                ddlUser.DataValueField = "UserId";
                ddlUser.DataTextField = "FirstName";
                ddlUser.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }


        //----------------------Save Main Category
        protected void btnSave_Click(object sender, EventArgs e) {

            try {

                List<int> UserIds = new List<int>();

                for (int i = 0; i < ddlUser.Items.Count; i++) {
                    if (ddlUser.Items[i].Selected) {
                        UserIds.Add(int.Parse(ddlUser.Items[i].Value));
                    }
                }


                int save = 0;

                if (effectiveDate.Text == "") {
                    DisplayMessage("Error occured - Please fill all details", true);
                }
                else {
                    if (hndAction.Value == "Save") {
                        save = itemCategoryOwners2Controller.ManageItemCategoryOwners2(0, Convert.ToInt32(ddlCategoryList.SelectedValue), ddlOwnerType.SelectedValue, UserIds, Convert.ToDateTime(effectiveDate.Text), Convert.ToInt32(Session["UserId"].ToString()), LocalTime.Now, "Save");
                        if (save > 0) {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                                "none",
                                "<script>    " +
                                "$(document).ready(function () { " +
                                " swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });  " +
                                " </script>",
                                false);
                                RefreshGrid();
                            ClearFields();
                            //  LoadCategoryDetails();

                            int categoryId = int.Parse(ddlCategoryList.SelectedValue);
                            gvCategoryOwnerDetail.DataSource = itemCategoryOwners2Controller.FetchCategoryOwners(categoryId);
                            gvCategoryOwnerDetail.DataBind();

                            gvPurchaseOfficerDetails.DataSource = itemCategoryOwners2Controller.FetchPurchaseOfficers(categoryId);
                            gvPurchaseOfficerDetails.DataBind();
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please delete existing user to insert new user', showConfirmButton: true,timer: 4000}); });   </script>", false);

                        }
                    }

                    else if (hndAction.Value == "Update") {
                        DateTime PrevEffectveDate =  DateTime.Parse(ViewState["PrevEffectveDate"].ToString());
                        save = itemCategoryOwners2Controller.UpdateCategoryOwner(Convert.ToInt32(ddlCategoryList.SelectedValue), Convert.ToInt32(ddlUser.SelectedValue), Convert.ToDateTime(effectiveDate.Text), ddlOwnerType.SelectedValue, UserIds, Convert.ToInt32(Session["UserId"].ToString()), PrevEffectveDate);
                        if (save > 0) {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                                "none",
                                "<script>    " +
                                "$(document).ready(function () { " +
                                " swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });  " +
                                " </script>",
                                false);
                                 RefreshGrid();
                            ClearFields();
                           // LoadCategoryDetails();
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during saving ', showConfirmButton: true,timer: 4000}); });   </script>", false);

                        }
                    }

                    //else {
                    //    save = itemCategoryOwners2Controller.ManageItemCategoryOwners2(Convert.ToInt32(hndEditRowId.Value), Convert.ToInt32(ddlCategoryList.SelectedValue), ddlOwnerType.SelectedValue, UserIds, Convert.ToDateTime(effectiveDate.Text), Convert.ToInt32(Session["UserId"].ToString()), LocalTime.Now, "Update");
                    //    if (save > 0) {
                    //        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                    //            "none",
                    //            "<script> " +
                    //            " $(document).ready(function () { " +
                    //            " swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); }); " +
                    //            "</script>",
                    //            false);
                    //        //   RefreshGrid();
                    //        ClearFields();
                    //    }
                    //    else {
                    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during saving ', showConfirmButton: true,timer: 4000}); });   </script>", false);

                    //    }
                    //}


                }
            }
            catch (Exception ex) {
                throw;
            }
        }

        private void RefreshGrid() {
            //var getAllMaincategoryList = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString()));
            //ViewState["getAllMaincategoryList"] = new JavaScriptSerializer().Serialize(getAllMaincategoryList);
            //  LoadMainCategoryDetails(getAllMaincategoryList);
            int categoryId = int.Parse(ddlCategoryList.SelectedValue);
            gvCategoryOwnerDetail.DataSource = itemCategoryOwners2Controller.FetchCategoryOwners(categoryId);
            gvCategoryOwnerDetail.DataBind();

            gvPurchaseOfficerDetails.DataSource = itemCategoryOwners2Controller.FetchPurchaseOfficers(categoryId);
            gvPurchaseOfficerDetails.DataBind();
            CurrentCO();
            CurrentPO();
        }

        protected void lnkBtnDelete_Click(object sender, EventArgs e) {
            try {
                int result = 0;

                int MainCategoryId = 0;
                String OwnerType = string.Empty;
                int userId = 0;

                GridViewRow row = ((sender as Button).NamingContainer as GridViewRow);
                MainCategoryId = int.Parse(row.Cells[0].Text);
                OwnerType = row.Cells[2].Text;
                userId = int.Parse(row.Cells[1].Text);
                DateTime EffectiveDate = DateTime.Parse(row.Cells[4].Text);

                result = itemCategoryOwners2Controller.DeleteCategoryOwners(MainCategoryId, OwnerType, userId, EffectiveDate);
                if (result > 0) {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                        "none",
                        "<script> " +
                        " $(document).ready(function () { " +
                        " swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500});    }); " +
                        " </script>",
                        false);
                    RefreshGrid();
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during deleting ', showConfirmButton: true,timer: 4000}); });   </script>", false);

                }

            }
            catch (Exception ex) {
                throw;
            }
        }

        //-----------------------Load Gv Data

        //private void LoadCategoryDetails() {
        //    try {

        //        gvMainCategory.DataSource = itemCategoryOwners2Controller.GetCompanyOwnersandPurchaseOfficersbyCompanyId(int.Parse(Session["CompanyId"].ToString()));
        //        gvMainCategory.DataBind();



        //    }
        //    catch (Exception ex) {
        //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While loading gridview', showConfirmButton: true,timer: 4000}); });   </script>", false);
        //        throw ex;
        //    }
        //}
        //private void LoadMainCategoryDetails(List<ItemCategory> itemCategory) {
        //    try {
        //        List<ItemCategoryOwners2> LIST = itemCategoryOwners2Controller.GetCompanyOwnersandPurchaseOfficersbyCompanyId(int.Parse(Session["CompanyId"].ToString()));

        //        gvMainCategory.DataSource = LIST;
        //       // gvMainCategory.DataSource = itemCategoryOwners2Controller.GetCompanyOwnersandPurchaseOfficersbyCompanyId(int.Parse(Session["CompanyId"].ToString()));
        //        gvMainCategory.DataBind();

        //List<ItemCategoryOwners> itemCategoryOwnerList = new List<ItemCategoryOwners>();
        //gvMainCategory.DataSource = itemCategory;
        //gvMainCategory.DataBind();
        //for (int i = 0; i < gvMainCategory.Rows.Count; i++) {
        //    int purchasingOfficerId = 0;
        //    int catergoryOwnerId = 0;
        //    ItemCategoryOwners obj2 = itemCategoryOwnerController.FetchItemCategoryOwnersByCategoryId(itemCategory[i].CategoryId, "PO");
        //    ItemCategoryOwners obj3 = itemCategoryOwnerController.FetchItemCategoryOwnersByCategoryId(itemCategory[i].CategoryId, "CO");
        //    purchasingOfficerId = obj2.UserId;
        //    catergoryOwnerId = obj3.UserId;
        //    ((Literal)((GridViewRow)gvMainCategory.Rows[i]).FindControl("ltPurchasingOfficer")).Text = purchasingOfficerId.ToString();
        //    ((Literal)((GridViewRow)gvMainCategory.Rows[i]).FindControl("ltCatergoryOwner")).Text = catergoryOwnerId.ToString();
        //    string purchasingOwner = string.Empty;
        //    string catergoryOwner = string.Empty;
        //    var CompanyLoginUserList = new JavaScriptSerializer().Deserialize<List<CompanyLogin>>(ViewState["CompanyLoginUserList"].ToString());
        //    purchasingOwner = CompanyLoginUserList.Find(x => x.UserId == purchasingOfficerId) != null ? CompanyLoginUserList.Find(x => x.UserId == purchasingOfficerId).Username : "";
        //    catergoryOwner = CompanyLoginUserList.Find(x => x.UserId == catergoryOwnerId) != null ? CompanyLoginUserList.Find(x => x.UserId == catergoryOwnerId).Username : "";

        //    ((Literal)((GridViewRow)gvMainCategory.Rows[i]).FindControl("ltPurchasingOfficerName")).Text = purchasingOwner;
        //    if (obj2.EffectiveDate != DateTime.MinValue) {
        //        ((Literal)((GridViewRow)gvMainCategory.Rows[i]).FindControl("ltPOEffectiveDate")).Text = obj2.EffectiveDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
        //    }
        //     ((Literal)((GridViewRow)gvMainCategory.Rows[i]).FindControl("ltCatergoryOwnerName")).Text = catergoryOwner;
        //    if (obj3.EffectiveDate != DateTime.MinValue) {
        //        ((Literal)((GridViewRow)gvMainCategory.Rows[i]).FindControl("ltCOEffectiveDate")).Text = obj3.EffectiveDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
        //    }
        //}
        //    }
        //    catch (Exception ex) {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While loading gridview', showConfirmButton: true,timer: 4000}); });   </script>", false);
        //        throw ex;
        //    }
        //}
        protected void ddlCategoryList_OnSelectedIndexChanged(object sender, EventArgs e) {
            int categoryId = int.Parse(ddlCategoryList.SelectedValue);

            gvCategoryOwnerDetail.DataSource = itemCategoryOwners2Controller.FetchCategoryOwners(categoryId);
            gvCategoryOwnerDetail.DataBind();

            gvPurchaseOfficerDetails.DataSource = itemCategoryOwners2Controller.FetchPurchaseOfficers(categoryId);
            gvPurchaseOfficerDetails.DataBind();

            CurrentCO();
            CurrentPO();
        }
            protected void btnEdit_Click(object sender, EventArgs e) {
            try {
                int MainCategoryId = 0;
                String OwnerType = string.Empty;
                int userId = 0;

                GridViewRow row = ((sender as Button).NamingContainer as GridViewRow);
                MainCategoryId = int.Parse(row.Cells[0].Text);
                OwnerType = row.Cells[2].Text;
                userId = int.Parse(row.Cells[1].Text);
                DateTime PrevEffectveDate = DateTime.Parse(row.Cells[4].Text);
                ViewState["PrevEffectveDate"] = PrevEffectveDate;

                ItemCategoryOwners2 CategoryOwners = itemCategoryOwners2Controller.FetchItemCategoryOwnerDetails(MainCategoryId, OwnerType, userId, int.Parse(Session["CompanyId"].ToString()));


                ddlCategoryList.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString()));
                ddlCategoryList.DataValueField = "CategoryId";
                ddlCategoryList.DataTextField = "CategoryName";
                ddlCategoryList.DataBind();
                ddlCategoryList.SelectedValue = CategoryOwners.CategoryId.ToString();


                List<OwnerType> listOwnerType = new List<OwnerType>();
                listOwnerType.Add(new OwnerType { Id = "PO", Name = "Purchase Officer" });
                listOwnerType.Add(new OwnerType { Id = "CO", Name = "Catergory Owner" });

                ViewState["listOwnerType"] = listOwnerType;

                ddlOwnerType.DataSource = listOwnerType;
                ddlOwnerType.DataValueField = "Id";
                ddlOwnerType.DataTextField = "Name";
                ddlOwnerType.DataBind();
                ddlOwnerType.SelectedValue = CategoryOwners.OwnerType.ToString();

                // ddlUser.DataSource = companyLoginController.GetAllUserList();
                ddlUser.DataSource = new List<CompanyLogin> { new CompanyLogin() { UserId = CategoryOwners.UserId, FirstName = CategoryOwners.UserName } };
                ddlUser.DataValueField = "UserId";
                ddlUser.DataTextField = "FirstName";
                ddlUser.DataBind();
                ddlUser.SelectedValue = CategoryOwners.UserId.ToString();

                effectiveDate.Text = CategoryOwners.EffectiveDate.ToString("yyyy-MM-dd");
                btnSave.Text = "Update";
                //hndAction.Value = "Update";

                ddlCategoryList.Enabled = false;
                ddlOwnerType.Enabled = false;
                
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    

        //protected void OnRowDataBound(object sender, GridViewRowEventArgs e) {
        //    try {
        //        if (e.Row.RowType == DataControlRowType.DataRow) {
        //            GridView gvDates = e.Row.FindControl("gvDates") as GridView;
        //            int CategoryId = int.Parse(gvMainCategory.DataKeys[e.Row.RowIndex].Value.ToString());
        //            GridViewRow row = gvDates.Parent.Parent.Parent as GridViewRow;
                    
        //            // string type = row.Cells[3].Text;
        //            string type = (row.FindControl("HdnOwnerType") as HiddenField).Value;
        //            List<ItemCategoryOwners2> obj = itemCategoryOwners2Controller.FetchDates(CategoryId, type);


        //            if (obj.Count > 0)
        //                obj[0].IsFirst = 1;

        //            gvDates.DataSource = obj;
        //            gvDates.DataBind();




        //        }
        //    }
        //    catch (Exception ex) {
        //        throw ex;
        //    }
        //}

        protected void OnRowDataBoundDate(object sender, GridViewRowEventArgs e) {
            try {

                if (e.Row.RowType == DataControlRowType.DataRow) {


                    GridView gvUsers = e.Row.FindControl("gvUsers") as GridView;
                   /// int CategoryId = int.Parse(gvMainCategory.DataKeys[e.Row.RowIndex].Value.ToString());
                    GridViewRow row = gvUsers.Parent.Parent.Parent as GridViewRow;
                    int categoryId = int.Parse(row.Cells[1].Text);
                    int isFirst = int.Parse(row.Cells[4].Text);
                    string type = (row.FindControl("HdnOwnerType") as HiddenField).Value;

                  //  DateTime date = DateTime.ParseExact((row.Cells[4].Text), "dd-mm-yyyy", CultureInfo.InvariantCulture);
                     DateTime date = Convert.ToDateTime(row.Cells[3].Text);
                    // DateTime date = Convert.ToDateTime(row.Cells[4].Text);
                    List<ItemCategoryOwners2> obj = itemCategoryOwners2Controller.FetchCompanyOwnerHistory(categoryId, type, date);

                    if (isFirst == 1) {
                        if (obj.Count > 0)
                            obj[0].IsFirst = 1;
                    }
                    gvUsers.DataSource = obj;
                    gvUsers.DataBind();

                }
                }
            catch (Exception ex) {
                throw ex;
            }
        }

        

        //private List<ItemCategoryOwners> GetCategoryOwnerById(int categoryId) {
        //    try {
        //        //DataTable dt = new DataTable();
        //        //dt.Columns.AddRange(new DataColumn[4] { new DataColumn("APPROVAL_LIMIT_ID"), new DataColumn("MINIMUM_AMOUNT"), new DataColumn("MAXIMUM_AMOUNT"), new DataColumn("TPTYPE") });
        //        return itemCategoryOwners2Controller.GetCompanyOwnersandPurchaseOfficersbyCompanyId(int.Parse(Session["CompanyId"].ToString()))
        //        // return dt;
        //    }
        //    catch (Exception ex) {
        //        throw ex;
        //    }
        //}



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

        protected void gvMainCategory_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                //gvMainCategory.PageIndex = e.NewPageIndex;
                //gvMainCategory.DataSource = new JavaScriptSerializer().Deserialize<List<ItemCategory>>(ViewState["getAllMaincategoryList"].ToString()); 
                //gvMainCategory.DataBind();
            }
            catch (Exception) {

            }
        }

        protected void btnClear_Click(object sender, EventArgs e) {
            ClearFields();
        }

        private void ClearFields() {
            hndAction.Value = "Save";
            effectiveDate.Text = "";
            hndEditRowId.Value = "";
            ddlCategoryList.Enabled = true;
            ddlOwnerType.Enabled = true;
            ddlCategoryList.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString()));
            ddlCategoryList.DataValueField = "CategoryId";
            ddlCategoryList.DataTextField = "CategoryName";
            ddlCategoryList.DataBind();

            LoadOwnerType();

            var CompanyLoginUserList = companyLoginController.GetAllUserList();
            LoadUsers(CompanyLoginUserList);
            RefreshGrid();

        }
    }

    //public class OwnerType {
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //}



}