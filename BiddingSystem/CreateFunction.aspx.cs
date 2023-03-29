using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Controller;
using System.IO;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class CreateFunction : System.Web.UI.Page
    {
        #region Properties

        int adminId = 0;
        static int roleId = 0;
        string UserId = string.Empty;
        string CompanyId = string.Empty;
        static int MainFunctiontId = 0;
        GridView gvSubcatgry;
        public List<string> companyList= new List<string>();
        List<CompanyDepartment> companyDepartments = new List<CompanyDepartment>();
        SystemDivisionController systemDivisionController = ControllerFactory.CreateSystemDivisionController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefUserCreation";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabUserCreation";
                    ((BiddingAdmin)Page.Master).subTabValue = "CreateFunction.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "createFunctionlink";

                    UserId = Session["UserId"].ToString();
                    CompanyId = Session["CompanyId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if (!companyUserAccessController.isAvilableAccess(int.Parse(UserId), int.Parse(CompanyId), 5, 4) && companyLogin.Usertype != "S")
                    {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }


                if (!IsPostBack)
                {
                    ddlSystrmDivisionDataBind();
                    LoadGV();
                }
                msg.Visible = false;
                txtSubfunction.Enabled = false;
            }
             
            catch (Exception)
            {

            }
}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
            if (btnSave.Text == "Save")
            {
                int mainFunctiontId = systemDivisionController.SaveSystemDivision(txtCatagory.Text, DateTime.Now, UserId, DateTime.Now, UserId, chkIsavtive.Checked == true ? 1 : 0);
                if (mainFunctiontId != -1)
                {
                    if (mainFunctiontId > 0)
                    {
                        
                         DisplayMessage("Main Function Category has been created successfully", false);
                    }
                    else
                    {
                        DisplayMessage("Main Function Category Creation problem", true);
                    }
                }
                else
                {
                    DisplayMessage("Main Function Category Name already exists", true);
                }
            }



            if(btnSave.Text=="Update")
            {
                if (MainFunctiontId != 0)
                {

                    int updateMainFunctiontStatus = systemDivisionController.UpdateSystemDivision(MainFunctiontId, txtCatagory.Text, DateTime.Now, UserId, chkIsavtive.Checked == true ? 1 : 0);
                    
                    if (updateMainFunctiontStatus != -1)
                    {
                        if (updateMainFunctiontStatus > 0)
                        {
                            clearFields2();
                            DisplayMessage("Main Function Category has been updated successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Main Function Category update Problem", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("Main Function Category Name already exists", true);
                    }
                }
                else
                {
                    DisplayMessage("Please Select Department", true);
                }
            }
            clearFields();
            LoadGV();

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

        private void LoadGV()
        {
            try
            {
                    gvDepartments.DataSource = systemDivisionController.FetchSystemDivision();
                    gvDepartments.DataBind();
                   
                
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                MainFunctiontId = int.Parse(gvDepartments.Rows[x].Cells[2].Text);
                SystemDivision sysDevisionObj = systemDivisionController.FetchSystemDivisionBySystemDivisionId(MainFunctiontId);
                if (sysDevisionObj.systemDivisionId != 0)
                {
                    txtCatagory.Text = sysDevisionObj.systemDivisionName;
                    if (sysDevisionObj.IsActive == 1)
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
            catch (Exception)
            {

                throw;
            }
        }

        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string devision = hdnmainCatogory.Value;
                string fId=hdnfunction.Value;
                string sId = hdnmaincatfunction.Value;
                if (fId != "" && fId != null) {

                    int deleteCompanyStatus = systemDivisionController.DeleteSystemDivisionFunction(int.Parse(sId), int.Parse(fId));
                    if (deleteCompanyStatus>0)
                    {
                        DisplayMessage("Function has been Deleted Successfully", false);
                        clearFields2();
                        LoadGV();
                    }

                    else
                    {
                        DisplayMessage("Error on Delete Main Function Category", true);
                    }
                }
                
                if (devision != "" && devision != null)
                {
                    
                    int deleteCompanyStatus = systemDivisionController.DeleteSystemDivision(int.Parse(devision));
                        if (deleteCompanyStatus > 0)
                        {
                            DisplayMessage("Main Function Category has been Deleted Successfully", false);
                            clearFields();
                            LoadGV();

                        }
                        else
                        {
                            DisplayMessage("Error on Delete Main Function Category", true);
                        }
                }


               
                }
            catch (Exception)
            {

                throw;
            }
        }

       
        private void clearFields()
        {
            txtCatagory.Text = "";
            btnSave.Text = "Save";
           
        }

        private void clearFields2()
        {
            txtSubfunction.Text = "";
            ddlSystrmDivision.SelectedIndex = 0;
            btnSave2.Text = "Save";

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                clearFields();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnClear2_Click(object sender, EventArgs e)
        {
            clearFields2();
        }

        protected void gvDepartments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDepartments.PageIndex = e.NewPageIndex;
                LoadGV();
            }
            catch (Exception)
            {

            }
        }
       

        protected void ddlSystrmDivisionDataBind()
        {
            ddlSystrmDivision.DataSource = systemDivisionController.FetchSystemDivision();
            ddlSystrmDivision.DataTextField = "systemDivisionName";
            ddlSystrmDivision.DataValueField = "systemDivisionId";
            ddlSystrmDivision.DataBind();
            ddlSystrmDivision.Items.Insert(0, new ListItem("Select System Division", "0"));
        }
        protected void ddlSystrmDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(int.Parse(ddlSystrmDivision.SelectedValue)!=0)
            {

                txtSubfunction.Enabled = true;
            }
        }

        protected void btnSave2_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave2.Text == "Save")
                {
                    int functionid = systemDivisionController.SaveSystemDivisionfunction(int.Parse(ddlSystrmDivision.SelectedValue) ,txtSubfunction.Text,DateTime.Now, UserId, DateTime.Now, UserId);
                    if (functionid != -1)
                    {
                        if (functionid > 0)
                        {

                            DisplayMessage("Function has been created successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Function Creation problem", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("Function Name already exists", true);
                    }
                    clearFields2();
                }



                if (btnSave2.Text == "Update")
                {
                    if (MainFunctiontId != 0 && hdnEdit.Value != "" && hdnEdit.Value != null)
                    {

                        int updateDepartmentStatus = systemDivisionController.updateSystemDivisionFunction(MainFunctiontId, int.Parse(hdnEdit.Value), txtSubfunction.Text, DateTime.Now, UserId);

                        if (updateDepartmentStatus != -1)
                        {
                            if (updateDepartmentStatus > 0)
                            {
                                DisplayMessage("Function has been updated successfully", false);
                            }
                            else
                            {
                                DisplayMessage("Function update Problem", true);
                            }
                        }
                        else
                        {
                            DisplayMessage("Function Name already exists", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("Please Select Function", true);
                    }
                }
                clearFields2();
                LoadGV();

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        protected void lnkBtnEditSubcategory_Click(object sender, EventArgs e)
        {
            try
            {
                //int functionid=int.Parse(hdnEdit.Value);
                GridViewRow gvfunstionRow = (GridViewRow)((ImageButton)sender).NamingContainer;
                GridView Childgrid = (GridView)(gvfunstionRow.Parent.Parent);
                int x = gvfunstionRow.RowIndex;
               // int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                int fid = int.Parse(Childgrid.Rows[x].Cells[0].Text);
                MainFunctiontId = int.Parse(Childgrid.Rows[x].Cells[1].Text);
                SystemDivisionFunction sysDevisionObj = systemDivisionController.FetchSystemDivisionFunctionsBySystemDivisionIdandFinctionId(MainFunctiontId, fid);
                if (sysDevisionObj.systemDivisionId != 0)
                {
                    ddlSystrmDivision.SelectedValue = (MainFunctiontId.ToString());
                    txtSubfunction.Text = sysDevisionObj.functionName;
                    txtSubfunction.Enabled = true;
                    hdnEdit.Value = fid.ToString();
                    btnSave2.Text = "Update";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string catogoryId = gvDepartments.DataKeys[e.Row.RowIndex].Value.ToString();
                     gvSubcatgry = e.Row.FindControl("gvSubcatgry") as GridView;

                    gvSubcatgry.DataSource = systemDivisionController.FetchSystemDivisionFunctionsBySystemDivisionId(int.Parse(catogoryId));
                    gvSubcatgry.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lnkBtndeleteSubcategory_Click(object sender, EventArgs e)
        {
            //int functionid=int.Parse(hdnEdit.Value);
            GridViewRow gvfunstionRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            GridView Childgrid = (GridView)(gvfunstionRow.Parent.Parent);
            int x = gvfunstionRow.RowIndex;
            // int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
           hdnfunction.Value= Childgrid.Rows[x].Cells[0].Text;
           hdnmaincatfunction.Value=Childgrid.Rows[x].Cells[1].Text;
           ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {showModal();});   </script>", false);
            
        }


    }
}