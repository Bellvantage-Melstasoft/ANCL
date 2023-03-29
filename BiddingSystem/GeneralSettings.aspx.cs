using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class GeneralSettings : System.Web.UI.Page
    {
        //static string UserId = string.Empty;
       // int CompanyId = 0;
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ((BiddingAdmin)Page.Master).mainTabValue = "hrefGeneralSetting";
            ((BiddingAdmin)Page.Master).subTabTitle = "subGeneralSetting";
            ((BiddingAdmin)Page.Master).subTabValue = "GeneralSettings.aspx";
            ((BiddingAdmin)Page.Master).subTabId = "generalSettingLink";

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
               // CompanyId = int.Parse(Session["CompanyId"].ToString());
              //  UserId = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 9, 1) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            GeneralSetting generalSettings = generalSettingsController.FetchGeneralSettingsListByIdObj(int.Parse(Session["CompanyId"].ToString()));

           

            if (!IsPostBack)
            {
                VAT_NBT nvat = generalSettingsController.getLatestVatNbt();

                txtVAT.Text = nvat.VatRate.ToString();
                txtNBT1.Text = nvat.NBTRate1.ToString();
                txtNBT2.Text = nvat.NBTRate2.ToString();

                if (generalSettings.DepartmentId != 0)
                {
                    btnSave.Text = "Update";
                    if (generalSettings.BidOnlyRegisteredSupplier == 1)
                    {
                        chkRegSupYes.Checked = true;
                    }
                    else
                    {
                        chkRegSupNo.Checked = true;
                    }

                    if (generalSettings.CanOverride == 1)
                    {
                        chkBidOpenYes.Checked = true;
                    }
                    else
                    {
                        chkBidOpenNo.Checked = true;
                    }

                    if (generalSettings.ViewBidsOnlineUponPrCreation == 1)
                    {
                        chkViewBidsYes.Checked = true;
                    }
                    else
                    {
                        chkViewBidsNo.Checked = true;
                    }

                    if (generalSettings.ManualBidAllowsOnlySelectedItems == 0)
                    {
                        rdoManualBidAllowsAllItems.Checked = true;
                    }
                    else
                    {
                        rdoManualBidAllowsSelectedItems.Checked = true;
                    }

                   
                }
                else
                {

                    btnSave.Text = "Save";
                    ClearFields();
                  
                }

            }

        }

        //-------------Save data
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
               
                    int chkbidOpen = 0;
                    int chkRegSupp = 0;
                    int chkOverride = 0;
                    int manualbid = 0;

                    if (chkBidOpenYes.Checked)
                    {
                        chkOverride = 1;
                    }
                    if (chkBidOpenNo.Checked)
                    {
                        chkOverride = 0;
                    }
                    if (chkRegSupYes.Checked)
                    {
                        chkRegSupp = 1;
                    }
                    if (chkRegSupNo.Checked)
                    {
                        chkRegSupp = 0;
                    }
                    if (chkViewBidsYes.Checked)
                    {
                        chkbidOpen = 1;
                    }
                    if (chkViewBidsNo.Checked)
                    {
                        chkbidOpen = 0;
                    }

                    if (rdoManualBidAllowsSelectedItems.Checked)
                    {
                        manualbid = 1;
                    }
                    if (rdoManualBidAllowsAllItems.Checked)
                    {
                        manualbid = 0;
                    }


                     if (btnSave.Text == "Save")
                    {
                        int saveSetting = generalSettingsController.SaveGeneralSettings(int.Parse(Session["CompanyId"].ToString()), decimal.Parse(txtNoOfDay.Text), chkOverride, chkRegSupp, chkbidOpen, manualbid);

                    if (saveSetting > 0)
                    {
                        DisplayMessage("Setting has been Saved successfully", false);
                    }
                    else
                    {
                        DisplayMessage("Setting  Save problem", true);
                    }
                }
                if (btnSave.Text == "Update")
                {
                    int UpdateSetting = generalSettingsController.UpdateGeneralSettings(int.Parse(Session["CompanyId"].ToString()), decimal.Parse(txtNoOfDay.Text), chkOverride, chkRegSupp, chkbidOpen, manualbid);
                    if (UpdateSetting > 0)
                    {
                        DisplayMessage("Setting has been Updated successfully", false);
                    }
                    else
                    {
                        DisplayMessage("Setting  Update problem", true);
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        protected void btnSaveNBTVAT_Click(object sender, EventArgs e) {
            try {
                decimal vat = decimal.Parse(txtVAT.Text);
                decimal nbt1 = decimal.Parse(txtNBT1.Text);
                decimal nbt2 = decimal.Parse(txtNBT2.Text);

                int InsertVatNBT = generalSettingsController.InsertVatNBT(vat, nbt1, nbt2);
                if (InsertVatNBT > 0) {
                    DisplayMessage("VAT and NBT have been Updated successfully", false);
                }
                else {
                    DisplayMessage("VAT and NBT  Update problem", true);
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
}
private void ClearFields()
        {
            chkBidOpenYes.Checked = false;
            chkBidOpenNo.Checked = false;
            chkRegSupNo.Checked = false;
            chkRegSupYes.Checked = false;
            chkViewBidsNo.Checked = false;
            chkViewBidsYes.Checked = false;
            rdoManualBidAllowsAllItems.Checked = false;
            rdoManualBidAllowsSelectedItems.Checked = false;
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