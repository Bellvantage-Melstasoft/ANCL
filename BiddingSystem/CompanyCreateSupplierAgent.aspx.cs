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
    public partial class CompanyCreateSupplierAgent : System.Web.UI.Page
    {
       // int CompanyId = 0;
       // string userId = string.Empty;
        SupplierController supplierController = ControllerFactory.CreateSupplierController();        
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        public static List<SupplierAssignedToCompany> listSupplier = new List<SupplierAssignedToCompany>();
       // List<SupplierAgent> listSupplierAgent = new List<SupplierAgent>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefSupplier";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabSupplier";
                ((BiddingAdmin)Page.Master).subTabValue = "CompanyCreateSupplierAgent.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "createSupplierAgentLink";

                ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                ViewState["userId"]  = Session["UserId"].ToString();
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
            msg.Visible = false;
            if (!IsPostBack)
            {
                LoadSupplierDropdown();
                LoadAgentGridView();
            }
        }

       
        private void LoadSupplierDropdown()
        {
            try
            {
                listSupplier = supplierAssigneToCompanyController.GetSupplierRequestsByCompanyId(int.Parse(Session["CompanyId"].ToString())).Where(c => c.IsApproved == 1).ToList(); 
                ddlSupplier.DataSource = listSupplier;
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataValueField = "SupplierId";
                ddlSupplier.DataBind();
                ddlSupplier.Items.Insert(0, new ListItem("Select Supplier", "0"));

            }
            catch(Exception ex)
            {

            }
        }

        private void LoadAgentGridView()
        {
            try {
                List<SupplierAgent> listSupplierAgent;
            listSupplierAgent = supplierController.getSupplierAgent();
            ViewState["listSupplierAgent"] = new JavaScriptSerializer().Serialize(listSupplierAgent);
            gvSupplierAgentList.DataSource = listSupplierAgent;
            gvSupplierAgentList.DataBind();
            }
            catch (Exception ex)
            {

            }
        }



        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int supplierId = Convert.ToInt32(ddlSupplier.SelectedValue);
                string action = string.Empty;
                if (supplierId > 0)
                {
                    SupplierAgent supplierAgent = new SupplierAgent {
                        SupplierId = supplierId,
                        AgentId = 0,
                        AgentName = txtAgentName.Text,
                        Address = txtAddress.Text,
                        Email = txtEmailAddress.Text,
                        ContactNo = txtOfficeContactNo.Text

                    };
                    if (btnEdit.Value == "0")
                    {
                       action = "Insert";
                    }else if (btnEdit.Value == "1")
                    {
                        action = "Update";
                        supplierAgent.AgentId = Convert.ToInt32(hndAgentId.Value);
                    }
                        int status = supplierController.manageSupplierAgentDetails(supplierAgent, action);
                        if (status > 0)
                        {                        
                            ScriptManager.RegisterClientScriptBlock(updatepanel1, this.updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                            btnSave.Text = "Submit";
                            btnEdit.Value = "0";
                        btnSave.CssClass = "btn btn-primary";

                        ClearFields();
                            LoadAgentGridView();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(updatepanel1, this.updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Creating supplier failed', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(updatepanel1, this.updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Supplier creation failed', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }

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
     
        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int supplierId = int.Parse(gvSupplierAgentList.Rows[x].Cells[0].Text);
                int agentId = int.Parse(gvSupplierAgentList.Rows[x].Cells[1].Text);
                hndAgentId.Value = agentId.ToString();
                ddlSupplier.SelectedValue = gvSupplierAgentList.Rows[x].Cells[0].Text;
                txtAgentName.Text = gvSupplierAgentList.Rows[x].Cells[3].Text;
                txtAddress.Text = gvSupplierAgentList.Rows[x].Cells[4].Text;
                txtEmailAddress.Text = gvSupplierAgentList.Rows[x].Cells[5].Text;
                txtOfficeContactNo.Text = gvSupplierAgentList.Rows[x].Cells[6].Text;
                btnSave.Text = "Update";
                btnSave.CssClass = "btn btn-danger";
                btnEdit.Value = "1";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                "none",
                "<script>    $(document).ready(function () { document.getElementById('DivEdit').scrollIntoView(true);     });   </script>",
                false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int agentId = Convert.ToInt32(hndAgentId.Value);
                int status = 0;
                if (agentId > 0)
                {
                    string action = "Delete";
                    SupplierAgent supplierAgent = new SupplierAgent
                    {
                        SupplierId = 0,
                        AgentId = agentId,
                        AgentName = "",
                        Address ="",
                        Email = "",
                        ContactNo = ""

                    };
                    status = supplierController.manageSupplierAgentDetails(supplierAgent, action);
                    if (status > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(updatepanel1, this.updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        Refresh();
                        LoadAgentGridView();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(updatepanel1, this.updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Creating supplier failed', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(updatepanel1, this.updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Supplier creation failed', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void Refresh() {
            Response.Redirect("CompanyCreateSupplierAgent.aspx");
        }

        private void ClearFields()
        {
            txtAddress.Text = "";
            txtEmailAddress.Text = "";
            txtOfficeContactNo.Text = "";
            txtAgentName.Text = "";
            ddlSupplier.SelectedIndex = 0;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<SupplierAgent> listSupplierAgent;
            int supplierId = Convert.ToInt32(ddlSupplier.SelectedValue);
            listSupplierAgent = supplierController.getSupplierAgentBySupplierId(supplierId);
            ViewState["listSupplierAgent"] = new JavaScriptSerializer().Serialize(listSupplierAgent);
            gvSupplierAgentList.DataSource = listSupplierAgent;
            gvSupplierAgentList.DataBind();
        }

        protected void gvSupplierAgentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvSupplierAgentList.PageIndex = e.NewPageIndex;
                gvSupplierAgentList.DataSource = new JavaScriptSerializer().Deserialize<List<SupplierAgent>>(ViewState["listSupplierAgent"].ToString());
                gvSupplierAgentList.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}