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
    public partial class CompanyAddFunctionAction : System.Web.UI.Page
    {
        FunctionActionController functionActionController = ControllerFactory.CreateFunctionActionController();
        List<FunctionAction> getAllActionsList = new List<FunctionAction>();
        FunctionAction functionObj = new FunctionAction();
        int adminId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["AdminId"] != null && Session["AdminId"].ToString() != "")
                {
                    adminId = int.Parse(Session["AdminId"].ToString());
                }
                else
                {
                    Response.Redirect("LoginPageAdmin.aspx");
                }
                msg.Visible = false;

                if (!IsPostBack)
               {
                LoadFunctionActions();
                getAllActionsList = functionActionController.FetchFunctionAction();
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
                  int SaveMainItem = functionActionController.SaveFunctionAction(txtActionName.Text, LocalTime.Now, adminId.ToString(), LocalTime.Now, adminId.ToString(), chkBoxIsActive);
                    if (SaveMainItem != -1)
                    {
                        if (SaveMainItem > 0)
                        {
                            LoadFunctionActions();
                            ClearFiled();
                            DisplayMessage("Action has been Created Successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Error on Create Action", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("Action name already exists", true);
                    }
              }
              if (btnSave.Text == "Update")
              {
                  int UpdateMainItem = functionActionController.UpdateFunctionAction(int.Parse(HiddenField1.Value), txtActionName.Text,LocalTime.Now, adminId.ToString(), chkBoxIsActive);
                    if (UpdateMainItem != -1)
                    {
                        if (UpdateMainItem > 0)
                        {
                            LoadFunctionActions();
                            ClearFiled();
                            DisplayMessage("Action has been Updated Successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Error on Update Action", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("Action name already exists", true);
                    }
              }
	        }
	        catch (Exception)
	        {
	        	
	        	throw;
	        }
        }

        //-----------------------Load Gv Data
        private void LoadFunctionActions() 
        {
            try
            {
                gvMainCategory.DataSource = functionActionController.FetchFunctionAction();
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
                int actionId = int.Parse(gvMainCategory.Rows[x].Cells[0].Text);
                functionObj = functionActionController.FetchFunctionActionObjByActionoid(actionId);
                if(functionObj.functionActionId !=0)
                {
                    txtActionName.Text = functionObj.functionActionName;
                    HiddenField1.Value = functionObj.functionActionId.ToString();
                    if (functionObj.IsActive == 1)
                    {
                        chkIsavtive.Checked = true;
                    }
                    else
                    {
                        chkIsavtive.Checked = false;
                    }
                    btnSave.Text = "Update";
                }
               
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
                //int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                //categoryId = int.Parse(gvMainCategory.Rows[x].Cells[0].Text);
                //categoryName = gvMainCategory.Rows[x].Cells[1].Text;
                //txtMainCategoryName.Text = categoryName;
                //isActive = int.Parse(gvMainCategory.Rows[x].Cells[6].Text);
                //if (isActive == 1)
                //{
                //    chkIsavtive.Checked = true;
                //}
                //else {
                //    chkIsavtive.Checked = false;
                //}
                //int deleteItem = itemCategoryController.DeleteItemCategory(categoryId);

                //if (deleteItem == 1)
                //{

                //}
                //else { }
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
            txtActionName.Text = "";
            adminId = 0;
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
        
    }
}