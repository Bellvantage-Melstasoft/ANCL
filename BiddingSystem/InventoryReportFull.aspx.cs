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
    public partial class InventoryReportFull : System.Web.UI.Page
    {
      //  private string script = "";
      //  static string UserId = string.Empty;
       // int CompanyId = 0;
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {

                 //   UserId = Session["UserId"].ToString();
                 //   CompanyId = int.Parse(Session["CompanyId"].ToString());
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 4, 3) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
       
        public void search(int cat, int sub, int level)
        {
            List<AddItem> List = new List<AddItem>();
            AddItemController addItemController = ControllerFactory.CreateAddItemController();
            List = addItemController.ItemsInInventoryByCatagory(cat, sub, level);
           
            gvReport.DataSource = List;
            gvReport.DataBind();
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

        protected void ddlSubCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {

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


        protected void btnSearchSubCatagory_Click(object sender, EventArgs e)
        {
            search(int.Parse(ddlCatagory.SelectedValue), int.Parse(ddlSubCatagory.SelectedValue), 0);
        }


    }
}