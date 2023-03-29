using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Domain;
using CLibrary.Common;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class afterPO : System.Web.UI.Page
    {
        AfterPOController afterPOController = ControllerFactory.createAfterPOController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "AddAfterPO.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "addAfterPOLink";

                ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                ViewState["userId"] = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 3, 2) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                /*Drop down Dates Binding*/
                ddlDateType.DataSource = afterPOController.getDateOptions();
                ddlDateType.DataTextField = "Name";
                ddlDateType.DataValueField = "ID";
                ddlDateType.DataBind();
                ddlDateType.Items.Insert(0, "--Select--");

                /*Drop down Custom Charges*/
                ddlCustomCharges.DataSource = afterPOController.getCustomChargesTypes();
                ddlCustomCharges.DataTextField = "NAME";
                ddlCustomCharges.DataValueField = "ID";
                ddlCustomCharges.DataBind();
                ddlCustomCharges.Items.Insert(0, "--Select--");

                /*Drop down shipping charges*/
                ddlShippingCharges.DataSource = afterPOController.getShippingChargesType();
                ddlShippingCharges.DataTextField = "NAME";
                ddlShippingCharges.DataValueField = "ID";
                ddlShippingCharges.DataBind();
                ddlShippingCharges.Items.Insert(0, "--Select--");

                /*Drop down charges*/
                ddlCharges.DataSource = afterPOController.getChargesTypes();
                ddlCharges.DataTextField = "NAME";
                ddlCharges.DataValueField = "ID";
                ddlCharges.DataBind();
                ddlCharges.Items.Insert(0, "--Select--");
            }
            else
            {
                //Dynamically bind Feilds of Date
                List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("txtDateDynamic")).ToList();
                int i = 1;
                foreach (string key in keys)
                {
                    this.CreateTextBox("txtDateDynamic" + i);
                    i++;
                }

                List<string> ddlKey = Request.Form.AllKeys.Where(key => key.Contains("ddlDateTypeDynamic")).ToList();
                int j = 1;
                foreach (string key in ddlKey)
                {
                    this.CreateDropDownList("ddlDateTypeDynamic" + j);
                    j++;
                }
                /*
                 Dynamic TextBoxes and DDL
                 */
                //Dynamically Bind feilds of Custom Charges
                List<string> customKey = Request.Form.AllKeys.Where(key => key.Contains("txtCustomDynamic")).ToList();
                int cTextcounter = 1;
                foreach (string key in customKey)
                {
                    this.CreateCustomTextBox("txtCustomDynamic" + cTextcounter);
                    cTextcounter++;
                }

                List<string> customDDL = Request.Form.AllKeys.Where(key => key.Contains("ddlCustomDynamic")).ToList();
                int cDDlCounter = 1;
                foreach (string key in customDDL)
                {
                    this.CreateCustomDDL("ddlCustomDynamic" + cDDlCounter);
                    cDDlCounter++;
                }

                //Dynamically Bind feilds of Ship Charges
                List<string> shipKey = Request.Form.AllKeys.Where(key => key.Contains("txtShippngDynamic")).ToList();
                int sTextCounter = 1;
                foreach (string key in shipKey)
                {
                    this.CreateShippingTextBox("txtShippngDynamic" + sTextCounter);
                    sTextCounter++;
                }

                List<string> shipDDL = Request.Form.AllKeys.Where(key => key.Contains("ddlShippingDynamic")).ToList();
                int sDDlCounter = 1;
                foreach (string key in shipDDL)
                {
                    this.CreateShippingDLL("ddlShippingDynamic" + sDDlCounter);
                    sDDlCounter++;
                }

                // Dynamically Bind feilds of Charges
                List<string> chargesKey = Request.Form.AllKeys.Where(key => key.Contains("txtChargesDynamic")).ToList();
                int chargesTextCounter = 1;
                foreach (string key in chargesKey)
                {
                    this.CreateChargesTextBox("txtChargesDynamic" + chargesTextCounter);
                    chargesTextCounter++;
                }

                List<string> chargesDDl = Request.Form.AllKeys.Where(key => key.Contains("ddlChargesDynamic")).ToList();
                int chargesDDlCounter = 1;
                foreach (string key in chargesDDl)
                {
                    this.CreateChargesDDL("ddlChargesDynamic" + chargesDDlCounter);
                    chargesDDlCounter++;
                }
            }


        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            AfterPO objAfterPO = new AfterPO();
            List<int> dynamicDates = new List<int>();
            List<int> dynamicCustom = new List<int>();
            List<int> dynamicCharges = new List<int>();
            List<int> dynamicShipCharges = new List<int>();

            objAfterPO.HyperLoan = ddlHyperLoan.SelectedValue;
            objAfterPO.InsuranceCompany = txtInsuranceCompanyname.Text;
            //objAfterPO.InsuranceCharge = Convert.ToDouble(txtCustomCharges.Text);
            objAfterPO.PolicyNo = txtPolicyNo.Text;
            objAfterPO.ShippingAgent = txtShippingAgent.Text;

            if (txtSLPA.Text != "")
            {
                objAfterPO.SLPA = Convert.ToDouble(txtSLPA.Text);
            }

            objAfterPO.Vessel = txtVessel.Text;
            objAfterPO.PerformanceDate = txtPerformanceBondDate.Text;
            objAfterPO.PerformanceBondNo = txtPerformanceBondNo.Text;

            if(txtClearingcharges.Text != "")
            {
                objAfterPO.ClearingAndTransportCharges = Convert.ToDouble(txtClearingcharges.Text);
            }
            
            objAfterPO.InsuranceDate = txtInsuranceDate.Text;

            int dateType = 0;
            dateType = Convert.ToInt32(ddlDateType.SelectedValue);
            switch (dateType){
                case 1:
                    objAfterPO.LC_Opening = txtDates.Text;
                    break;
                case 2:
                    objAfterPO.LDS = txtDates.Text;
                    break;
                case 3:
                    objAfterPO.Expiry = txtDates.Text;
                    break;
                case 4:
                    objAfterPO.ETD = txtDates.Text;
                    break;
                case 5:
                    objAfterPO.ETA = txtDates.Text;
                    break;
            }

            /*Get Values from Static Views*/
            int chargeType = 0;   

            if(txtCharges.Text != "")
            {
                chargeType = Convert.ToInt32(ddlCharges.Text);
                switch (chargeType)
                {
                    case 1:
                        objAfterPO.ChargeValue = Convert.ToInt32(txtCharges.Text);
                        break;
                    case 2:
                        objAfterPO.InsuranceCharge = Convert.ToInt32(txtCharges.Text);
                        break;
                    case 3:
                        objAfterPO.Freight = Convert.ToInt32(txtCharges.Text);
                        break;
                }
            }
            

            

            if(txtCustomCharges.Text != "")
            {
                int customChargesType = Convert.ToInt32(ddlCustomCharges.SelectedValue);
                switch (customChargesType)
                {
                    case 1:
                        objAfterPO.Duty = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 2:
                        objAfterPO.CustomDuty = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 3:
                        objAfterPO.CESS = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 4:
                        objAfterPO.PAL = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 5:
                        objAfterPO.VAT = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 6:
                        objAfterPO.NBT = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 7:
                        objAfterPO.OtherCustomCharges = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 8:
                        objAfterPO.RANK_CONTAINER = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 9:
                        objAfterPO.WeighingCharges = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 10:
                        objAfterPO.TERMINAL = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                }
            }
             

            if(txtShippingCharges.Text != "")
            {
                int shipmentCharges = Convert.ToInt32(ddlCustomCharges.SelectedValue);
                switch (shipmentCharges)
                {
                    case 1:
                        objAfterPO.AgentDoCharges = Convert.ToInt32(txtShippingCharges.Text);
                        break;
                    case 2:
                        objAfterPO.ContainerDeposit = Convert.ToInt32(txtShippingCharges.Text);
                        break;
                    case 3:
                        objAfterPO.WashingCharges = Convert.ToInt32(txtShippingCharges.Text);
                        break;
                }
            }
            

            /*Binding values from the dynamically created feilds to the object*/
            foreach(DropDownList dropDown in ddlDatePanel.Controls.OfType<DropDownList>())
            {
                dynamicDates.Add(Convert.ToInt32(dropDown.SelectedValue));
            }

            foreach (TextBox textBox in datePanel.Controls.OfType<TextBox>())
            {
                int index = 0;
                int selected = dynamicDates[index];

                switch (selected)
                {
                    case 1:
                        objAfterPO.LC_Opening = textBox.Text;
                        break;
                    case 2:
                        objAfterPO.LDS = textBox.Text;
                        break;
                    case 3:
                        objAfterPO.Expiry = textBox.Text;
                        break;
                    case 4:
                        objAfterPO.ETD = textBox.Text;
                        break;
                    case 5:
                        objAfterPO.ETA = textBox.Text;
                        break;
                }
                index++;
            }

            foreach (DropDownList dropDown in panelChargesDDL.Controls.OfType<DropDownList>())
            {
                dynamicCharges.Add(Convert.ToInt32(dropDown.SelectedValue));
            }

            foreach (TextBox textBox in panelChargesText.Controls.OfType<TextBox>())
            {
                int index = 0;
                int selected = dynamicCharges[index];

                switch (selected)
                {
                    case 1:
                        objAfterPO.ChargeValue = Convert.ToInt32(txtCharges.Text);
                        break;
                    case 2:
                        objAfterPO.InsuranceCharge = Convert.ToInt32(txtCharges.Text);
                        break;
                    case 3:
                        objAfterPO.Freight = Convert.ToInt32(txtCharges.Text);
                        break;
                }
                index++;
            }

            foreach (DropDownList dropDown in panelDllCustom.Controls.OfType<DropDownList>())
            {
                dynamicCustom.Add(Convert.ToInt32(dropDown.SelectedValue));
            }

            foreach (TextBox textBox in panelCustomText.Controls.OfType<TextBox>())
            {
                int index = 0;
                int selected = dynamicCustom[index];

                switch (selected)
                {
                    case 1:
                        objAfterPO.Duty = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 2:
                        objAfterPO.CustomDuty = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 3:
                        objAfterPO.CESS = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 4:
                        objAfterPO.PAL = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 5:
                        objAfterPO.VAT = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 6:
                        objAfterPO.NBT = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 7:
                        objAfterPO.OtherCustomCharges = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 8:
                        objAfterPO.RANK_CONTAINER = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 9:
                        objAfterPO.WeighingCharges = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                    case 10:
                        objAfterPO.TERMINAL = Convert.ToInt32(txtCustomCharges.Text);
                        break;
                }
                index++;
            }

            foreach (DropDownList dropDown in pannelShippingDDL.Controls.OfType<DropDownList>())
            {
                dynamicShipCharges.Add(Convert.ToInt32(dropDown.SelectedValue));
            }

            foreach (TextBox textBox in panelShippingText.Controls.OfType<TextBox>())
            {
                int index = 0;
                int selected = dynamicShipCharges[index];

                switch (selected)
                {
                    case 1:
                        objAfterPO.AgentDoCharges = Convert.ToInt32(txtShippingCharges.Text);
                        break;
                    case 2:
                        objAfterPO.ContainerDeposit = Convert.ToInt32(txtShippingCharges.Text);
                        break;
                    case 3:
                        objAfterPO.WashingCharges = Convert.ToInt32(txtShippingCharges.Text);
                        break;
                }
                index++;
            }

            //Save into DB
            bool result = afterPOController.SaveAfterPODetails(objAfterPO);
            if(result == true)
            {
                Reset();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on Adding Details', showConfirmButton: true,timer: 4000}); });   </script>", false);
            }

        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txtDates.Text = "";
            txtCharges.Text = "";
            txtClearingcharges.Text = "";
            txtCustomCharges.Text = "";
            txtInsuranceCompanyname.Text = "";
            txtInsuranceDate.Text = "";
            txtPerformanceBondDate.Text = "";
            txtPerformanceBondNo.Text = "";
            txtPolicyNo.Text = "";
            txtShippingAgent.Text = "";
            txtShippingCharges.Text = "";
            txtSLPA.Text = "";
            txtVessel.Text = "";

            panelChargesDDL.Controls.Clear();
            panelChargesText.Controls.Clear();
            panelCustomText.Controls.Clear();
            panelDllCustom.Controls.Clear();
            panelShippingText.Controls.Clear();
            pannelShippingDDL.Controls.Clear();
            datePanel.Controls.Clear();
            ddlDatePanel.Controls.Clear();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int indexOfTextBox = datePanel.Controls.OfType<TextBox>().ToList().Count + 1;
            int indexOfddl = ddlDatePanel.Controls.OfType<DropDownList>().ToList().Count + 1;

            this.CreateTextBox("txtDateDynamic" + indexOfTextBox);
            this.CreateDropDownList("ddlDateTypeDynamic" + indexOfddl);
        }

        private void CreateTextBox(string id)
        {
            TextBox textBox = new TextBox();
            textBox.ID = id;
            textBox.CssClass = "form-control col-sm-4 dynamicStyle";
            datePanel.Controls.Add(textBox);
            Literal literal = new Literal();
            literal.Text = "</br>";
            datePanel.Controls.Add(literal);
        }

        private void CreateDropDownList(string id)
        {
            DropDownList ddlDateType = new DropDownList();
            ddlDateType.ID = id;
            ddlDateType.CssClass = "form-control col-sm-4 dynamicStyle";
            ddlDatePanel.Controls.Add(ddlDateType);
            Literal literal = new Literal();
            literal.Text = "</br>";
            ddlDateType.DataSource = afterPOController.getDateOptions();
            ddlDateType.DataTextField = "Name";
            ddlDateType.DataValueField = "ID";
            ddlDateType.DataBind();
            ddlDateType.Items.Insert(0, "--Select--");
            ddlDatePanel.Controls.Add(literal);
        }

        protected void btnAddCustomCharges_Click(object sender, EventArgs e)
        {
            int indexOfTextBox = panelCustomText.Controls.OfType<TextBox>().ToList().Count + 1;
            int indexOfddl = panelDllCustom.Controls.OfType<DropDownList>().ToList().Count + 1;

            this.CreateCustomTextBox("txtCustomDynamic" + indexOfTextBox);
            this.CreateCustomDDL("ddlCustomDynamic" + indexOfddl);
        }

        private void CreateCustomTextBox(string id)
        {
            TextBox textBox = new TextBox();
            textBox.ID = id;
            textBox.CssClass = "form-control col-sm-4 dynamicStyle";
            panelCustomText.Controls.Add(textBox);
            Literal literal = new Literal();
            literal.Text = "</br>";
            panelCustomText.Controls.Add(literal);
        }

        private void CreateCustomDDL(string id)
        {
            DropDownList ddlCustomCharges = new DropDownList();
            ddlCustomCharges.ID = id;
            ddlCustomCharges.CssClass = "form-control col-sm-4 dynamicStyle";
            panelDllCustom.Controls.Add(ddlCustomCharges);
            Literal literal = new Literal();
            literal.Text = "</br>";
            ddlCustomCharges.DataSource = afterPOController.getCustomChargesTypes();
            ddlCustomCharges.DataTextField = "Name";
            ddlCustomCharges.DataValueField = "ID";
            ddlCustomCharges.DataBind();
            ddlCustomCharges.Items.Insert(0, "--Select--");
            panelDllCustom.Controls.Add(literal);
        }

        protected void btnAddShipping_Click(object sender, EventArgs e)
        {
            int indexOfTextBox = panelShippingText.Controls.OfType<TextBox>().ToList().Count + 1;
            int indexOfddl = pannelShippingDDL.Controls.OfType<DropDownList>().ToList().Count + 1;

            this.CreateShippingTextBox("txtShippngDynamic" + indexOfTextBox);
            this.CreateShippingDLL("ddlShippingDynamic" + indexOfddl);
        }

        private void CreateShippingTextBox(string id)
        {
            TextBox textBox = new TextBox();
            textBox.ID = id;
            textBox.CssClass = "form-control col-sm-4 dynamicStyle";
            panelShippingText.Controls.Add(textBox);
            Literal literal = new Literal();
            literal.Text = "</br>";
            panelShippingText.Controls.Add(literal);
        }

        private void CreateShippingDLL(string id)
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = id;
            ddl.CssClass = "form-control col-sm-4 dynamicStyle";
            pannelShippingDDL.Controls.Add(ddl);
            Literal literal = new Literal();
            literal.Text = "</br>";
            ddl.DataSource = afterPOController.getShippingChargesType();
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.Items.Insert(0, "--Select--");
            pannelShippingDDL.Controls.Add(literal);
        }

        protected void btnAddCharges_Click(object sender, EventArgs e)
        {
            int indexOfTextBox = panelChargesText.Controls.OfType<TextBox>().ToList().Count + 1;
            int indexOfddl = panelChargesDDL.Controls.OfType<DropDownList>().ToList().Count + 1;

            this.CreateChargesTextBox("txtChargesDynamic" + indexOfTextBox);
            this.CreateChargesDDL("ddlChargesDynamic" + indexOfddl);
        }

        private void CreateChargesTextBox(string id)
        {
            TextBox textBox = new TextBox();
            textBox.ID = id;
            textBox.CssClass = "form-control col-sm-4 dynamicStyle";
            panelChargesText.Controls.Add(textBox);
            Literal literal = new Literal();
            literal.Text = "</br>";
            panelChargesText.Controls.Add(literal);
        }

        private void CreateChargesDDL(string id)
        {
            DropDownList ddl = new DropDownList();
            ddl.ID = id;
            ddl.CssClass = "form-control col-sm-4 dynamicStyle";
            panelChargesDDL.Controls.Add(ddl);
            Literal literal = new Literal();
            literal.Text = "</br>";
            ddl.DataSource = afterPOController.getChargesTypes();
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.Items.Insert(0, "--Select--");
            panelChargesDDL.Controls.Add(literal);
        }
    }
    
}