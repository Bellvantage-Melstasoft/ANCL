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
    public partial class AddUnitMeasurement : System.Web.UI.Page
    {
        
       // int CompanyId = 0;
      //  static string UserId = string.Empty;
       // static int measurementId = 0;
        
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        IMeasurementDetailController unitMeasurementController = ControllerFactory.CreateMeasurementDetailController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        ItemImageUploadController itemImageUploadController = ControllerFactory.CreateItemImageUploadController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefMeasurement";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabMeasurement";
                    ((BiddingAdmin)Page.Master).subTabValue = "AddUnitMeasurement.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "AddUnitMeasurementLink";

                    //CompanyId = int.Parse(Session["CompanyId"].ToString());
                  var  UserId = Session["UserId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                        if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), int.Parse(Session["CompanyId"].ToString()), 1, 1) && companyLogin.Usertype != "S" )&& companyLogin.Usertype != "GA")
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
                    bindMeasurements();
            }
           
            msg.Visible = false;
            }
            catch (Exception)
            {
            }


           // ScriptManager.RegisterStartupScript(Updatepanel1, this.Updatepanel1.GetType(), "YourUniqueScriptKey", "GetButtonCick();", true);

        }


        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                

                if (btnSave.Text == "Save")
                {
                    int saveMeasurement = unitMeasurementController.CreateCustomMeasurement(txtMeasurementName.Text.ProcessString(), txtMeasuremenCode.Text.ProcessString(), int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()), chkIsavtive.Checked ? 1 : 0);
                    if (saveMeasurement > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("Measurement has been created successfully", false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Measurement already exists!'}); });   </script>", false);
                        //DisplayMessage("Error on creating Measurement", true);
                    }
                }
                bindMeasurements();
                clearFieds();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearFieds();
        }

        protected void gvUnitMeasuremnts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        //protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
        //        ViewState["measurementId"] =int.Parse( gvUnitMeasuremnts.Rows[x].Cells[1].Text);
        //        UnitMeasurement unitMeasurementObj = unitMeasurementController.fetchMeasurementObjByMeasurementId(int.Parse(ViewState["measurementId"].ToString()));

        //        if (unitMeasurementObj.measurentId > 0)
        //        {
        //            txtMeasuremenCode.Text = unitMeasurementObj.measurementShortName;
        //            txtMeasurementName.Text = unitMeasurementObj.measurementName;
        //            if (unitMeasurementObj.isActive == 1)
        //            {
        //                chkIsavtive.Checked = true;
        //            }
        //            else
        //            {
        //                chkIsavtive.Checked = false;
        //            }
        //            btnSave.Text = "Update";
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}



        protected void btnDelete_Click1(object sender, EventArgs e)
        {
            try
            {
                if (hdMeasurementId.Value != "")
                {

                    int saveMeasurement = unitMeasurementController.UpdateCustomMeasurementActiveStatus(int.Parse(hdMeasurementId.Value), 0);
                    if (saveMeasurement > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Measurement has been deleted successfully', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("Measurement has been deleted successfully", false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on deleting Measurement'}); });   </script>", false);
                        //DisplayMessage("Error on deleting Measurement", true);
                    }
                    bindMeasurements();
                }
            }
            catch (Exception)
            {

                throw;
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

        private void bindMeasurements()
        {
            try
            {
                gvUnitMeasuremnts.DataSource = unitMeasurementController.GetAllCustomMeasurementsOfCompany(int.Parse(Session["CompanyId"].ToString()));
                gvUnitMeasuremnts.DataBind();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void clearFieds()
        {
            txtMeasuremenCode.Text = "";
            txtMeasurementName.Text = "";
            chkIsavtive.Checked = false;
            btnSave.Text = "Save";

        }

        protected void btnRestore_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (hdMeasurementId.Value != "")
                {

                    int saveMeasurement = unitMeasurementController.UpdateCustomMeasurementActiveStatus(int.Parse(hdMeasurementId.Value), 1);
                    if (saveMeasurement > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Measurement has been restored successfully', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("Measurement has been deleted successfully", false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on restoring Measurement'}); });   </script>", false);
                        //DisplayMessage("Error on deleting Measurement", true);
                    }
                    bindMeasurements();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}