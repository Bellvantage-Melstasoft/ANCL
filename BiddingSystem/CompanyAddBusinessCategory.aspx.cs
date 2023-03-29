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
    public partial class CompanyAddBusinessCategory : System.Web.UI.Page
    {
        NaturseOfBusinessController naturseOfBusinessController = ControllerFactory.CreateNaturseOfBusinessController();
        static  string AdminId = string.Empty;
        static int categoryId = 0;
        private string categoryName = string.Empty;
        private DateTime createdDate;
        private string createdBy = string.Empty;
        private DateTime updatedDate;
        private string updatedBy = string.Empty;
        private int isActive = 0;
        List<NaturseOfBusiness> getAllBusinesscategoryList = new List<NaturseOfBusiness>();
        public List<string> CategoryList = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            if (Session["AdminId"] != null)
            {
                AdminId = Session["AdminId"].ToString();
            }
            else
            {
                Response.Redirect("LoginPageAdmin.aspx");
            }
                msg.Visible = false;
                if (!IsPostBack)
            {
                LoadMainCategoryDetails();
                    getAllBusinesscategoryList = naturseOfBusinessController.FetchBusinessCategoryList();
                    foreach (var item in getAllBusinesscategoryList)
                    {
                        CategoryList.Add(item.BusinessCategoryName);
                    }
            }
            }
            catch (Exception)
            {

            }
        }

        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
        }

        //----------------------Save Main Category
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            try 
	        {	   
              int chkBoxIsActive = 0;

              if (chkIsavtive.Checked)
	          {
	              chkBoxIsActive = 1;	 
	          }

              if (btnSave.Text == "Save")
              {
                  int SaveMainItem = naturseOfBusinessController.SaveBusinessCategory(txtBusinessCategoryName.Text, LocalTime.Now, AdminId, LocalTime.Now, AdminId, chkBoxIsActive);
                    if (SaveMainItem != -1)
                    {
                        if (SaveMainItem > 0)
                        {
                            LoadMainCategoryDetails();
                            ClearFiled();
                            DisplayMessage("Business Category has been Created Successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Error on Create Business Category", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("Business Category name already exists", true);
                    }
              }
              if (btnSave.Text == "Update")
              {
                  int UpdateMainItem = naturseOfBusinessController.UpdateBusinessCategory(int.Parse(HiddenField1.Value), txtBusinessCategoryName.Text, createdDate, createdBy, LocalTime.Now, AdminId, chkBoxIsActive);
                    if (UpdateMainItem != -1)
                    {
                        if (UpdateMainItem > 0)
                        {
                            LoadMainCategoryDetails();
                            ClearFiled();
                            DisplayMessage("Business Category has been updated successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Error on update category", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("Business Category name already exists", true);
                    }
              }
	        }
	        catch (Exception)
	        {
	        	
	        	throw;
	        }
        }

        //-----------------------Load Gv Data
        private void LoadMainCategoryDetails() 
        {
            try
            {
                gvMainCategory.DataSource = naturseOfBusinessController.FetchBusinessCategoryList();
                gvMainCategory.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------Edit Saved Data
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                categoryId = int.Parse(gvMainCategory.Rows[x].Cells[1].Text);
                categoryName = gvMainCategory.Rows[x].Cells[2].Text;
                createdDate = DateTime.Parse(gvMainCategory.Rows[x].Cells[3].Text);
                createdBy = gvMainCategory.Rows[x].Cells[4].Text;
                updatedDate = DateTime.Parse(gvMainCategory.Rows[x].Cells[5].Text);
                updatedBy = gvMainCategory.Rows[x].Cells[6].Text;
                isActive = int.Parse(gvMainCategory.Rows[x].Cells[7].Text);
                txtBusinessCategoryName.Text = categoryName;
                HiddenField1.Value = categoryId.ToString();
                if (isActive == 1)
                {
                    chkIsavtive.Checked = true;
                }
                else
                {
                    chkIsavtive.Checked = false;
                }
                btnSave.Text = "Update";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {  document.body.scrollTop = 100; document.documentElement.scrollTop = 100;});   </script>", false);

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
                // int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                //categoryId = int.Parse(gvMainCategory.Rows[x].Cells[0].Text);
                string categoryId = hdnBusinessCategoryId.Value;
                if (categoryId != "" && categoryId != null)
                {


                    int deleteItem = naturseOfBusinessController.DeleteBusinessCategory(int.Parse(categoryId));

                    if (deleteItem == 1)
                    {
                        DisplayMessage("Business Category has been Deleted Successfully", false);
                        LoadMainCategoryDetails();
                    }
                    else
                    {
                        DisplayMessage("Error on Delete Business Category", true);
                    }
                }
                else
                {
                    DisplayMessage("Pease Select Business Category to Delete!!", true);
                }
            }

            catch (Exception ex)
            {

            }
        }
        

       

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFiled();
        }

        private void ClearFiled() {
            txtBusinessCategoryName.Text = "";
            AdminId = string.Empty;
            chkIsavtive.Checked = true;
            btnSave.Text = "Save";
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

        public string getJsonCategoryList()
        {
            var DataList = CategoryList;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }

        protected void gvMainCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMainCategory.PageIndex = e.NewPageIndex;
                LoadMainCategoryDetails();
            }
            catch (Exception)
            {
 
            }
        }
    }
}