using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class CompanyAddMainCategoryOwnersNew : System.Web.UI.Page {
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        ItemCategoryOwnerController itemCategoryOwnerController = ControllerFactory.CreateItemCategoryOwnerController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
      //  public static List<CompanyLogin> CompanyLoginUserList = new List<CompanyLogin>();
        SubCategoryStoreKeeperController subCategoryStoreKeeperController = ControllerFactory.CreateSubCategoryStoreKeeperController();

      //  static int UserId;
      //  int CompanyId = 0;
        protected void Page_Load(object sender, EventArgs e) {
            try {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefItemCategory";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabItemCategory";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyAddMainCategoryOwnersNew.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "addMainCategoryOwnerNewLink";

                   // CompanyId = int.Parse(Session["CompanyId"].ToString());
                   // UserId = Convert.ToInt32(Session["UserId"].ToString());

                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(Convert.ToInt32(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 4, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else {
                    Response.Redirect("LoginPage.aspx");
                }

             
                if (!IsPostBack) {
                    List<CompanyLogin> CompanyLoginUserList = companyLoginController.GetAllUserList();
                    LoadDDLMainCatregory();
                    LoadUsers(CompanyLoginUserList);
                    LoadGV();

                }
            }
            catch (Exception) {

            }

        }

        private void LoadGV() {
            try {

                gvCurrentSK.DataSource = itemSubCategoryController.getStoreKeeperList(int.Parse(Session["CompanyId"].ToString()));
                gvCurrentSK.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void LoadUsers(List<CompanyLogin> Users) {
            try {
                ddlUsers.DataSource = Users;
                ddlUsers.DataValueField = "UserId";
                ddlUsers.DataTextField = "FirstName";
                ddlUsers.DataBind();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void LoadDDLMainCatregory() {
            try {
                ddlMainCategory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                ddlMainCategory.DataValueField = "CategoryId";
                ddlMainCategory.DataTextField = "CategoryName";
                ddlMainCategory.DataBind();
                ddlMainCategory.Items.Insert(0, new ListItem("Select Main Category", ""));
            }
            catch (Exception ex) {
            }
        }

        protected void ddlMainCategory_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (int.Parse(ddlMainCategory.SelectedValue) != 0 || ddlMainCategory.SelectedValue != "") {
                    int mainCategoryId = int.Parse(ddlMainCategory.SelectedValue);
                    ddlSubcategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(mainCategoryId, int.Parse(Session["CompanyId"].ToString()));
                    ddlSubcategory.DataTextField = "SubCategoryName";
                    ddlSubcategory.DataValueField = "SubCategoryId";
                    ddlSubcategory.DataBind();
                    ddlSubcategory.Items.Insert(0, new ListItem("Select Sub Category", ""));


                }

            }
            catch (Exception ex) {

            }
        }




        protected void btnSave_Click(object sender, EventArgs e) {

            try {
                if (btnSave.Text == "Save") {
                    List<int> UserIds = new List<int>();

                    for (int i = 0; i < ddlUsers.Items.Count; i++) {
                        if (ddlUsers.Items[i].Selected) {
                            UserIds.Add(int.Parse(ddlUsers.Items[i].Value));
                        }
                    }


                    int result = subCategoryStoreKeeperController.SaveStoreKeeper(int.Parse(ddlSubcategory.SelectedValue), UserIds, Convert.ToDateTime(txtEffectiveDate.Text));

                    if (result > 0) {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        LoadGV();
                        ClearFields();


                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on creating Stock Keeper\"; $('#errorAlert').modal('show'); });   </script>", false);

                    }
                }


                if (btnSave.Text == "Update") {
                
                        //List<int> UserIds = new List<int>();

                        //for (int i = 0; i < ddlUsers.Items.Count; i++) {
                        //    if (ddlUsers.Items[i].Selected) {
                        //        UserIds.Add(int.Parse(ddlUsers.Items[i].Value));
                        //    }
                        //}


                        int result = subCategoryStoreKeeperController.UpdateStoreKeeper(int.Parse(ddlSubcategory.SelectedValue), int.Parse(ViewState["Userid"].ToString()), Convert.ToDateTime(txtEffectiveDate.Text));

                        if (result > 0) {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Store keeper details have been updated', showConfirmButton: false,timer: 1500}); });   </script>", false);
                            LoadGV();
                            ClearFields();


                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Stock Keeper\"; $('#errorAlert').modal('show'); });   </script>", false);

                        }
                    }

             


                }
            catch (Exception ex) {
                throw ex;

            }

        }

        protected void btnClear_Click(object sender, EventArgs e) {
        }

        
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e) {
          
            if (e.Row.RowType == DataControlRowType.DataRow) {

                GridView gvUsers = e.Row.FindControl("gvUsers") as GridView;

                int subCategoryId = int.Parse(gvCurrentSK.DataKeys[e.Row.RowIndex].Value.ToString());


                gvUsers.DataSource = subCategoryStoreKeeperController.FetchStoreKeeper(subCategoryId);
                
                gvUsers.DataBind();


            }

        }
        private void ClearFields() {
            txtEffectiveDate.Text = string.Empty;
            List<CompanyLogin> CompanyLoginUserList = companyLoginController.GetAllUserList();
            LoadDDLMainCatregory();
            LoadUsers(CompanyLoginUserList);
            ddlSubcategory.Items.Clear();

        }

        protected void btnDelete_Click(object sender, EventArgs e) {
            try {
                int subCategoryId = 0;
                int UserId = 0;

                GridViewRow row = ((sender as Button).NamingContainer as GridViewRow);
                subCategoryId = int.Parse(row.Cells[0].Text);
                UserId = int.Parse(row.Cells[2].Text);
                string dt = row.Cells[4].Text;
                DateTime date = DateTime.ParseExact(dt, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                
                int result = subCategoryStoreKeeperController.DeleteStoreKeeper(subCategoryId, UserId, date);

                if (result > 0) {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Successfilly deleted record', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    LoadGV();
                   
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on deleting record\"; $('#errorAlert').modal('show'); });   </script>", false);

                }

            }
            catch (Exception ex) {

            }
        }



        protected void btnEdit_Click(object sender, EventArgs e) {
            try {
                int subCategoryId = 0;
                int UserId = 0;
               
                GridViewRow row = ((sender as Button).NamingContainer as GridViewRow);
                subCategoryId = int.Parse(row.Cells[0].Text);
                UserId = int.Parse(row.Cells[2].Text);

                SubCategoryStoreKeeper SK = subCategoryStoreKeeperController.FetchStoreKeeperDetails(subCategoryId, UserId);
                ViewState["Userid"] = SK.UserId.ToString();


                ddlMainCategory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString())).Where(y => y.IsActive == 1);
                ddlMainCategory.DataValueField = "CategoryId";
                ddlMainCategory.DataTextField = "CategoryName";
                ddlMainCategory.DataBind();
                ddlMainCategory.SelectedValue = SK.CategoryId.ToString();

                int mainCategoryId = int.Parse(ddlMainCategory.SelectedValue);
                ddlSubcategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(mainCategoryId, int.Parse(Session["CompanyId"].ToString()));
                ddlSubcategory.DataTextField = "SubCategoryName";
                ddlSubcategory.DataValueField = "SubCategoryId";
                ddlSubcategory.DataBind();
                ddlSubcategory.SelectedValue = SK.SubCategoryId.ToString();
                
                ddlUsers.DataSource = new List<CompanyLogin> { new CompanyLogin() { UserId=SK.UserId,Username= SK.UserName } };
                ddlUsers.DataValueField = "UserId";
                ddlUsers.DataTextField = "UserName";
                ddlUsers.DataBind();
                ddlUsers.SelectedValue = SK.UserId.ToString();
               

               // Convert.ToDateTime(SK.EffectiveDate).ToString("yyyy-MM-dd");
                txtEffectiveDate.Text = SK.EffectiveDate.ToString("yyyy-MM-dd");
                btnSave.Text = "Update";

                ddlMainCategory.Enabled = false;
                ddlSubcategory.Enabled = false;
             
                

            }
            catch (Exception ex) {

            }
        }

        protected void gvMainCategory_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
              
            }
            catch (Exception) {

            }
        }
    }
}