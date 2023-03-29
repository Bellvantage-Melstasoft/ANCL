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
    public partial class CompanyCreateSupplier : System.Web.UI.Page
    {
        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        SupplierCategoryController supplierCategoryController = ControllerFactory.CreateSupplierCategoryController();
        SuplierImageUploadController suplierImageUploadController = ControllerFactory.CreateSuplierImageUploadController();
        SupplierLoginController supplierLoginController = ControllerFactory.CreateSupplierLoginController();
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        SupplierRatingController supplierRatingController = ControllerFactory.CreateSupplierRatingController();
      //  GenetratePassword genetratePassword = new GenetratePassword();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        NaturseOfBusinessController naturseOfBusinessController = ControllerFactory.CreateNaturseOfBusinessController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        ItemCategoryMasterController itemCategoryMasterController = ControllerFactory.CreateItemCategoryMasterController();
        CompanyTypeController companyTypeController = ControllerFactory.CreateCompanyTypeController();
        SupplierTypeController supplierTypeController = ControllerFactory.CreateSupplierTypeController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefSupplier";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabSupplier";
                ((BiddingAdmin)Page.Master).subTabValue = "CompanyCreateSupplier.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "createSupplierLink";

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
                ddlBusinessCategory.DataSource = naturseOfBusinessController.FetchBusinessCategoryList();
                ddlBusinessCategory.DataTextField = "BusinessCategoryName";
                ddlBusinessCategory.DataValueField = "BusinessCategoryId";
                ddlBusinessCategory.DataBind();
                ddlBusinessCategory.Items.Insert(0, new ListItem("Select Business Category", ""));

                ddlCompanyType.DataSource = companyTypeController.FetchCompanyTypeList();
                ddlCompanyType.DataTextField = "CompanyTypeName";
                ddlCompanyType.DataValueField = "TypeId";
                ddlCompanyType.DataBind();
                ddlCompanyType.Items.Insert(0, new ListItem("Select Company Type", ""));

                ddlSupplierType.DataSource = supplierTypeController.FetchSupplierTypeList();
                ddlSupplierType.DataTextField = "SupplierTypeName";
                ddlSupplierType.DataValueField = "TypeId";
                ddlSupplierType.DataBind();
                ddlSupplierType.Items.Insert(0, new ListItem("Select Supplier Type", ""));

                lstCompanyCategory.DataSource = itemCategoryMasterController.FetchItemCategoryList().Where(r => r.IsActive == 1).ToList();
                lstCompanyCategory.DataTextField = "CategoryName";
                lstCompanyCategory.DataValueField = "CategoryId";
                lstCompanyCategory.DataBind();

                FetchSuppliers();
                imageid.Src = "~/LoginResources/images/noPerson.png" + "?" + LocalTime.Now.Ticks.ToString();
            }
        }

        private void FetchSuppliers()
        {
            List<Supplier> suppliers = supplierController.GetSupplierList();
            ddlAgentSupplier.DataSource = suppliers;
            ddlAgentSupplier.DataValueField = "SupplierId";
            ddlAgentSupplier.DataTextField = "SupplierName";
            ddlAgentSupplier.DataBind();
            ddlAgentSupplier.Items.Insert(0, new ListItem("Select Supplier", ""));
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try {
                GenetratePassword genetratePassword = new GenetratePassword();
                string username = genetratePassword.GetUserName();
                string password = genetratePassword.GetPassword();
                string companyname = companyDepartmentController.GetDepartmentByDepartmentId(int.Parse(Session["CompanyId"].ToString())).DepartmentName;
                Supplier supplier = new Supplier {
                    SupplierName = txtSupplierName.Text,
                    Address1 = txtAddress1.Text,
                    Address2 = txtAddress2.Text,
                    Email = txtEmailAddress.Text,
                    OfficeContactNo = txtOfficeContactNo.Text,
                    PhoneNo = txtMobileNo.Text,
                    RequestedDate = LocalTime.Now.ToString("yyyy-MM-dd"),
                    BusinessRegistrationNumber = txtBusinesRegNo.Text,
                    VatRegistrationNumber = txtVatRegNo.Text,
                    CompanyType = int.Parse(ddlCompanyType.SelectedValue),
                    BusinessCatecory = int.Parse(ddlBusinessCategory.SelectedValue),
                    SupplierLogo = "",
                    IsRequestFromSupplier = 0,
                    IsCreatedByAdmin = 1,
                    IsActive = 1,
                    SupplierType = int.Parse(ddlSupplierType.SelectedValue),
                    IsRegisteredSupplier = chkRegSup.Checked == true ? 1 : 0,
                    SupplierRegistration = txtSupRegNo.Text

                };
                SupplierLogin supplierLogin = new SupplierLogin
                {
                    Username = username,
                    Password = password.Encrypt(),
                    Email = txtEmailAddress.Text,
                    IsApproved = 1,
                    IsActive = 1
                };
                SupplierAssignedToCompany supplierAssignedToCompany = new SupplierAssignedToCompany
                {
                    CompanyId = int.Parse(Session["CompanyId"].ToString()),
                    RequestedDate = LocalTime.Now,
                    IsApproved = 1,
                    IsRequestFromSupplier = 1,
                    IsAgreedTerms = 1
                };                
                var selectedItemCategory = from li in lstCompanyCategory.Items.Cast<ListItem>()
                                           where li.Selected == true
                                           select li;
                HttpFileCollection uploadedFile =null;
                if (fileUploadDocs.HasFile)
                {
                    uploadedFile = Request.Files;
                }
                SupplierRatings supplierRating = new SupplierRatings
                {
                    Companyid = int.Parse(Session["CompanyId"].ToString()),
                    Rating = 0,
                    Isbalcklist = 0,
                    IsActive = 1,
                    CreatedDate = LocalTime.Now,
                    CreatedBy = Session["UserId"].ToString(),
                    UpdatedDate = LocalTime.Now,
                    UpdatedBy = Session["UserId"].ToString(),
                    Remarks = ""
                };
                List<int> supplierIds = new List<int>();
                for (int i = 0; i < ddlAgentSupplier.Items.Count; i++)
                {
                    if (ddlAgentSupplier.Items[i].Selected)
                    {
                        supplierIds.Add(int.Parse(ddlAgentSupplier.Items[i].Value));
                    }
                }
                string errormessage = string.Empty;
                int status = supplierController.manageSaveSupplier(supplier, supplierLogin, supplierAssignedToCompany, selectedItemCategory, fileUploadLogo, uploadedFile, supplierRating, supplierIds, out errormessage);
                if (status > 0)
                {
                    //sendMessage(txtMobileNo.Text, txtSupplierName.Text, companyname, username, password);
                    ScriptManager.RegisterClientScriptBlock(updatepanel1, this.updatepanel1.GetType(), "none", "<script>$(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    ClearFields();
                }else
                {
                    ScriptManager.RegisterClientScriptBlock(updatepanel1, this.updatepanel1.GetType(), "none", "<script>   $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Supplier Name already exists!', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void sendMessage(string phoneNo, string suppliername, string companyname, string username, string password)
        {
            string html = string.Empty;
            string url = @"http://119.235.1.63:4050/Sms.svc/SendSms?phoneNumber=" + phoneNo + "&smsMessage=Dear " + suppliername + ", you're successfully registered by " + companyname + " as a supplier in www.ezibidlanka.lk. Now you can login to our system by using following details\nUsername : " + username + "\nPassword : " + password + "&companyId=BELLADM&pword=BELLADM_123";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            string data = JObject.Parse(html)["Status"].ToString();

            if (data == "200" || data == "\"200\"")
            {
            }
            else
            {

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtBusinesRegNo.Text = "";
            ddlBusinessCategory.SelectedIndex = 0;
            txtEmailAddress.Text = "";
            txtMobileNo.Text = "";
            txtOfficeContactNo.Text = "";
            txtSupplierName.Text = "";
            txtVatRegNo.Text = "";
            ddlCompanyType.SelectedIndex = 0;

            foreach (ListItem item in lstCompanyCategory.Items)
            {
                item.Selected = false;
            }

            fileUploadDocs.Dispose();
            fileUploadLogo.Dispose();
            FetchSuppliers();

            ddlSupplierType.SelectedIndex = 0;
            chkRegSup.Checked = false;
            txtSupRegNo.Text = "";
        }
    }

}
