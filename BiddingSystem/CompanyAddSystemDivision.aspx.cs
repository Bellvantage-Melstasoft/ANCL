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
using System.Configuration;

namespace BiddingSystem
{
    public partial class CompanyAddSystemDivision : System.Web.UI.Page
    {
        SystemDivisionController systemDivisionController = ControllerFactory.CreateSystemDivisionController();
        FunctionActionController functionActionController = ControllerFactory.CreateFunctionActionController();
        List<SystemDivision> getAllSystemDivisionList = new List<SystemDivision>();
        List<SystemDivisionFunction> GetSysyemDivisionFunctions = new List<SystemDivisionFunction>();
        SystemDivision systemDivisionObj = new SystemDivision();
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
                msg2.Visible = false;
                if (!IsPostBack)
               {
                LoadSystemDivisions();
                LoadDropDownSystemDivisions();
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
                  int SaveMainItem = systemDivisionController.SaveSystemDivision(txtSystemDivision.Text, LocalTime.Now, adminId.ToString(), LocalTime.Now, adminId.ToString(), chkBoxIsActive);
                    if (SaveMainItem != -1)
                    {
                        if (SaveMainItem > 0)
                        {
                            LoadSystemDivisions();
                            ClearFiled();
                            DisplayMessage("Parent node has been Created Successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Error on Create Parent node", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("Parent node Name already exists", true);
                    }
              }
              if (btnSave.Text == "Update")
              {
                  int UpdateMainItem = systemDivisionController.UpdateSystemDivision(int.Parse(HiddenField1.Value), txtSystemDivision.Text,LocalTime.Now, adminId.ToString(), chkBoxIsActive);
                    if (UpdateMainItem != -1)
                    {
                        if (UpdateMainItem > 0)
                        {
                            LoadSystemDivisions();
                            ClearFiled();
                            DisplayMessage("Parent node has been Updated Successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Error on Update Parent node", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("Parent node Name already exists", true);
                    }
              }
	        }
	        catch (Exception)
	        {
	        	
	        	throw;
	        }
        }

        private void LoadSystemDivisions() 
        {
            try
            {
                gvSystemDivisions.DataSource = systemDivisionController.FetchSystemDivision();
                gvSystemDivisions.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void LoadDropDownSystemDivisions()
        {
            try
            {
                ddlSystemDivisions.DataSource = systemDivisionController.FetchSystemDivision();
                ddlSystemDivisions.DataTextField = "systemDivisionName";
                ddlSystemDivisions.DataValueField = "systemDivisionId";
                ddlSystemDivisions.DataBind();
                ddlSystemDivisions.Items.Insert(0,new ListItem("Select Parent node", "0"));
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
                int sysDivisionId = int.Parse(gvSystemDivisions.Rows[x].Cells[0].Text);
                systemDivisionObj = systemDivisionController.FetchSystemDivisionBySystemDivisionId(sysDivisionId);
                if(systemDivisionObj.systemDivisionId !=0)
                {
                    txtSystemDivision.Text = systemDivisionObj.systemDivisionName;
                    HiddenField1.Value = systemDivisionObj.systemDivisionId.ToString();
                    if (systemDivisionObj.IsActive == 1)
                    {
                        chkIsavtive.Checked = true;
                    }
                    else
                    {
                        chkIsavtive.Checked = false;
                    }
                    btnSave.Text = "Update";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {  document.body.scrollTop = 50; document.documentElement.scrollTop = 50;});   </script>", false);

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
            txtSystemDivision.Text = "";
            adminId = 0;
            chkIsavtive.Checked = true;
            btnSave.Text = "Save";
        }
      

        protected void btnAddFunctionToDivision_Click(object sender, EventArgs e)
        {
            
                int saveSystemDivisionFunction = systemDivisionController.assignSystemDivisionWithFunction(int.Parse(ddlSystemDivisions.SelectedValue), int.Parse(txtFunctionSeqience.Text), txtDivisionFunction.Text, LocalTime.Now, adminId.ToString(), LocalTime.Now, adminId.ToString());
                if (saveSystemDivisionFunction>0)
                {
                    DisplayMessageTwo("Child node has been Created Successfully", false);
                bindFunctionsGridview();
                }
                else
                {
                    DisplayMessageTwo("Error on Create Child node", true);
                }
           
        }
        
     
        protected void ddlSystemDivisions_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindFunctionsGridview();
        }
        protected void gvFunctions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridView gvFunctions = sender as GridView;
                int Id = Convert.ToInt32(gvFunctions.DataKeys[e.RowIndex].Value);
                this.DeleteSystemFunctionDelete(Id);
              
            }
            catch (Exception)
            {

              
            }
        }    

        protected void lbtnDeleteSystemFunction_Click(object sender, EventArgs e)
        {
            try
            {
                string sequenceId = hdnSystemDivisionId.Value;
                if (sequenceId != "" && sequenceId != null)
                {
                    DeleteSystemFunctionDelete(int.Parse(sequenceId));
                }
                else
                {
                    DisplayMessageTwo("Pease select Child node to Delete!! ", true);
                }
                }
            catch (Exception)
            {

                throw;
            }

        }
        protected void gvFunctions_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvFunctions.EditIndex = e.NewEditIndex;
                bindFunctionsGridview();
            }
            catch (Exception)
            {

               
            }
        }
        protected void gvFunctions_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string oldFunctionId = gvFunctions.DataKeys[e.RowIndex].Value.ToString();

                Label systemDivisionId = (Label)gvFunctions.Rows[e.RowIndex].FindControl("lblsystemDivisionId");
                TextBox FunctionName = (TextBox)gvFunctions.Rows[e.RowIndex].FindControl("txtfunctionName");
                TextBox newFunctionId = (TextBox)gvFunctions.Rows[e.RowIndex].FindControl("txtfunctionId");

               updateSystemFunction(int.Parse(systemDivisionId.Text),int.Parse( oldFunctionId), int.Parse(newFunctionId.Text), FunctionName.Text);
                bindFunctionsGridview(); 
            }
            catch (Exception)
            {
            }
        }
        protected void gvFunctions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFunctions.EditIndex = -1;
            bindFunctionsGridview();
        }


        private void bindFunctionsGridview()
        {
            try
            {

            if (ddlSystemDivisions.SelectedIndex != 0)
            {
                List<SystemDivisionFunction> GroupedSystemDivisionFunction = new List<SystemDivisionFunction>();
                GetSysyemDivisionFunctions = systemDivisionController.FetchSystemDivisionFunctionsBySystemDivisionId(int.Parse(ddlSystemDivisions.SelectedValue));

                gvFunctions.DataSource = GetSysyemDivisionFunctions;
                gvFunctions.DataBind();

            }

            }
            catch (Exception)
            {
            }
        }
        private void updateSystemFunction(int sysDivisionId, int odfunctionId, int newFunctionId, string newFunctionName)
        {
            try
            {
            int UpdateSystemFunctionBuFunctionId = systemDivisionController.updateSystemDivisionWithFunction(sysDivisionId, odfunctionId, newFunctionId, newFunctionName, LocalTime.Now, adminId.ToString());
            if (UpdateSystemFunctionBuFunctionId != -1)
            {
                if (UpdateSystemFunctionBuFunctionId > 0)
                {
                    DisplayMessageTwo("Child node has been Updated Successfully", false);
                    gvFunctions.EditIndex = -1;
                    this.bindFunctionsGridview();
                }
                else
                {
                    DisplayMessageTwo("Error On Update Child node ", true);
                }
            }
            else
            {
                DisplayMessageTwo("This Child node is already Exists ", true);
            }

            }
            catch (Exception)
            {

            }
        }
        private void DeleteSystemFunctionDelete(int id)
        {
            try
            {

            int DeleteSystemFunctionDeleteStatus = systemDivisionController.DeleteSystemDivisionFunction(int.Parse(ddlSystemDivisions.SelectedValue), id);
            if (DeleteSystemFunctionDeleteStatus > 0)
            {
                DisplayMessageTwo("Child node has been Deleted Successfully", false);
                gvFunctions.EditIndex = -1;
                this.bindFunctionsGridview();
            }
            else
            {
                DisplayMessageTwo("Error On Delete Child node", true);
            }

            }
            catch (Exception)
            {

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

        private void DisplayMessageTwo(string message, bool isError)
        {
            msg2.Visible = true;
            if (isError)
            {
                lbMessageTwo.CssClass = "failMessage";
                msg2.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else
            {
                lbMessageTwo.CssClass = "successMessage";
                msg2.Attributes["class"] = "alert alert-success alert-dismissable";
            }

            lbMessageTwo.Text = message;

        }

        protected void btnCancelFunction_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSystemDivisions.SelectedIndex = 0;
                gvFunctions.DataSource = null;
                gvFunctions.DataBind();
                txtFunctionSeqience.Text = "";
                txtDivisionFunction.Text = "";
            }
            catch (Exception)
            {

            }
        }
    }
}