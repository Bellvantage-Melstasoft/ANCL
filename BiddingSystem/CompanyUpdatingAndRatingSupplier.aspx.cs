using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.IO;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class CompanyUpdatingAndRatingSupplier : System.Web.UI.Page
    {
        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        SuplierImageUploadController suplierImageUploadController = ControllerFactory.CreateSuplierImageUploadController();
        SupplierLoginController supplierLoginController = ControllerFactory.CreateSupplierLoginController();
        SupplierCategoryController supplierCategoryController = ControllerFactory.CreateSupplierCategoryController();
        SupplierRatingController supplierRatingController = ControllerFactory.CreateSupplierRatingController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        NaturseOfBusinessController naturseOfBusinessController = ControllerFactory.CreateNaturseOfBusinessController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        ItemCategoryMasterController itemCategoryMasterController = ControllerFactory.CreateItemCategoryMasterController();
        //  public static List<SupplierAssignedToCompany> listSuppliers = new List<SupplierAssignedToCompany>();
        CompanyTypeController companyTypeController = ControllerFactory.CreateCompanyTypeController();
        SupplierTypeController supplierTypeController = ControllerFactory.CreateSupplierTypeController();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefSupplier";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabSupplier";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyUpdatingAndRatingSupplier.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "editSupplierLink";

                    ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                    ViewState["userId"] = Session["UserId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["userId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 3, 3) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                    ddlBusinessCategory.Items.Insert(0, new ListItem("Select Business Category", "0"));

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

                    var listSuppliers = supplierAssigneToCompanyController.GetSupplierRequestsByCompanyId(int.Parse(Session["CompanyId"].ToString())).Where(c => c.IsApproved == 1).ToList();
                    ViewState["listSuppliers"] = new JavaScriptSerializer().Serialize(listSuppliers);
                    gvSupplierList.DataSource = listSuppliers;
                    gvSupplierList.DataBind();

                    if (Request.QueryString.Get("ID") != null)
                    {
                        EditFromQueryString();
                    }
                    imageid.Src = "~/LoginResources/images/noPerson.png" + "?" + LocalTime.Now.Ticks.ToString();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void EditFromQueryString()
        {
            ViewState["supplierId"] = int.Parse(Request.QueryString.Get("ID"));
            SupplierAssignedToCompany supplierAssigneToCompanyObj = supplierAssigneToCompanyController.GetSupplierOfCompanyObj(int.Parse(ViewState["supplierId"].ToString()), int.Parse(Session["CompanyId"].ToString()));

            if (supplierAssigneToCompanyObj.SupplierId != 0)
            {
                txtSupplierId.Text = supplierAssigneToCompanyObj.SupplierId.ToString();
                txtSupplierName.Text = supplierAssigneToCompanyObj.SupplierName;
                txtAddress1.Text = supplierAssigneToCompanyObj.Address1;
                txtAddress2.Text = supplierAssigneToCompanyObj.Address2;
                txtMobileNo.Text = supplierAssigneToCompanyObj.PhoneNo;
                txtOfficeContactNo.Text = supplierAssigneToCompanyObj.OfficeContactNo;
                txtEmailAddress.Text = supplierAssigneToCompanyObj.Email;
                txtVatRegNo.Text = supplierAssigneToCompanyObj.VatRegistrationNumber;
                txtBusinesRegNo.Text = supplierAssigneToCompanyObj.BusinessRegistrationNumber;
                ddlCompanyType.SelectedValue = supplierAssigneToCompanyObj.CompanyType.ToString();
                ddlBusinessCategory.DataSource = naturseOfBusinessController.FetchBusinessCategoryList();
                ddlBusinessCategory.DataTextField = "BusinessCategoryName";
                ddlBusinessCategory.DataValueField = "BusinessCategoryId";
                ddlBusinessCategory.DataBind();
                ddlBusinessCategory.Items.Insert(0, new ListItem("Select Business Category", "0"));

                if (supplierAssigneToCompanyObj.SupplierFollowing == 1)
                {
                    rdoActive.Checked = true;
                    rdoInActive.Checked = false;
                }
                else
                {
                    rdoInActive.Checked = true;
                    rdoActive.Checked = false;
                }

                ddlBusinessCategory.SelectedValue = supplierAssigneToCompanyObj.BusinessCatecory;
                if (supplierAssigneToCompanyObj.SupplierLogo != "")
                {
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(supplierAssigneToCompanyObj.SupplierLogo)))
                    {
                        imageid.Src = supplierAssigneToCompanyObj.SupplierLogo + "?" + LocalTime.Now.Ticks.ToString();
                    }
                    else
                    {
                        imageid.Src = "~/LoginResources/images/noPerson.png" + "?" + LocalTime.Now.Ticks.ToString();
                    }
                    hdnSuplierLogoEdit.Value = imageid.Src.Replace("~/", "");
                    ViewState["supplierLogoPath"] = supplierAssigneToCompanyObj.SupplierLogo;
                }

                gvUserDocuments.DataSource = supplierAssigneToCompanyObj._SuplierImageUploadList;
                gvUserDocuments.DataBind();

                lstCompanyCategory.DataSource = itemCategoryMasterController.FetchItemCategoryList().Where(r => r.IsActive == 1).ToList();
                lstCompanyCategory.DataTextField = "CategoryName";
                lstCompanyCategory.DataValueField = "CategoryId";
                lstCompanyCategory.DataBind();

                if (supplierAssigneToCompanyObj._SupplierCategory.Count() > 0)
                {
                    foreach (ListItem item1 in lstCompanyCategory.Items)
                    {
                        if (supplierAssigneToCompanyObj._SupplierCategory.Where(y => y.CategoryId == int.Parse(item1.Value)).Count() > 0)
                        {
                            item1.Selected = true;
                        }
                    }
                }

                SupplierRatings supplierRatingObj = new SupplierRatings();
                supplierRatingObj = supplierRatingController.GetSupplierRatingBySupplierIdAndCompanyId((int.Parse(ViewState["supplierId"].ToString())), int.Parse(Session["CompanyId"].ToString()));
                if (supplierRatingObj.Companyid != 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {  Decide(" + supplierRatingObj.Rating + "); });   </script>", false);
                    if (supplierRatingObj.Isbalcklist == 1)
                        rdoBlockYes.Checked = true;
                    else
                        rdoBlockNo.Checked = true;

                    txtRemarks.Text = supplierRatingObj.Remarks;
                }

            }
        }

        protected void lbtnItem_Click(object sender, EventArgs e) {
            try {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int SupplierId = int.Parse(gvSupplierList.Rows[x].Cells[0].Text);

                //    gvItemCategories.DataSource = supplierAssigneToCompanyController.GetSupplierRequestsByCompanyId(int.Parse(Session["CompanyId"].ToString())).Where(c => c.IsApproved == 1 && c.SupplierId == SupplierId).ToList();
                List<SupplierAssignedToCompany> supplierAssignedToCompany = new JavaScriptSerializer().Deserialize<List<SupplierAssignedToCompany>>(ViewState["listSuppliers"].ToString());
                gvItemCategories.DataSource = supplierAssignedToCompany.Where(c => c.SupplierId == SupplierId).ToList();
                gvItemCategories.DataBind();

                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItemCategories').modal('show');});   </script>", false);


            }
            catch (Exception ex) {

            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if ((int.Parse(ViewState["supplierId"].ToString())) != 0)
                {        
                    HttpPostedFile logo = fileUploadLogo.PostedFile;
                    IList<HttpPostedFile> docs = fileUploadDocs.PostedFiles;

                    if (fileUploadLogo.PostedFile != null && fileUploadLogo.PostedFile.FileName != "")
                    {
                        string nameOfUploadedFile = (int.Parse(ViewState["supplierId"].ToString())) + "_1";
                        string UploadedFileName = nameOfUploadedFile.Replace(" ", String.Empty);
                        string FileName = Path.GetFileName(fileUploadLogo.PostedFile.FileName);
                        string filename1 = UploadedFileName + "." + FileName.Split('.').Last();
                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(ViewState["supplierLogoPath"].ToString())))
                        {
                            System.IO.File.Delete(HttpContext.Current.Server.MapPath(ViewState["supplierLogoPath"].ToString()));
                        }

                        fileUploadLogo.SaveAs(HttpContext.Current.Server.MapPath("~/Supplier/Logo/" + UploadedFileName + '.' + FileName.Split('.').Last()));

                        ViewState["supplierLogoPath"] = "~/Supplier/Logo/" + filename1;
                    }

                    for (int i = 0; i < docs.Count; i++)
                    {
                        if (docs[i].ContentLength > 0)
                        {
                            string nameOfUploadedFile = (int.Parse(ViewState["supplierId"].ToString())) + "_" + i.ToString();
                            string UploadedFileName = nameOfUploadedFile.Replace(" ", String.Empty);
                            string FileName = Path.GetFileName(docs[i].FileName);
                            string filename1 = UploadedFileName + "." + FileName.Split('.').Last();
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Supplier/Documents/" + filename1)))
                            {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Supplier/Documents/" + filename1));
                            }

                            docs[i].SaveAs(HttpContext.Current.Server.MapPath("~/Supplier/Documents/" + UploadedFileName + '.' + FileName.Split('.').Last()));

                            ViewState["supplierDocpath"] = "~/Supplier/Documents/" + filename1;
                            int saveFilePath = suplierImageUploadController.saveUploadedSupplierImage((int.Parse(ViewState["supplierId"].ToString())), ViewState["supplierDocpath"].ToString(), FileName, nameOfUploadedFile, 1);
                        }
                    }

                    supplierCategoryController.deleteSupplierCategoryBySupplierid((int.Parse(ViewState["supplierId"].ToString())));
                    foreach (ListItem item in lstCompanyCategory.Items)
                    {
                        if (item.Selected)
                        {
                            supplierCategoryController.saveSupplierCategory((int.Parse(ViewState["supplierId"].ToString())), int.Parse(item.Value), 1);
                        }
                    }

                    int result = supplierController.updateSupplier(int.Parse(ViewState["supplierId"].ToString()), txtSupplierName.Text, txtAddress1.Text,
                        txtAddress2.Text, txtOfficeContactNo.Text, txtMobileNo.Text, txtBusinesRegNo.Text, txtVatRegNo.Text,
                        int.Parse(ddlCompanyType.SelectedValue), int.Parse(ddlBusinessCategory.SelectedValue), ViewState["supplierLogoPath"] == null ? "": ViewState["supplierLogoPath"].ToString(), 0, 1, 1, 1, txtEmailAddress.Text, int.Parse(ddlSupplierType.SelectedValue), chkRegSup.Checked == true?1:0, txtSupRegNo.Text);

                    
                    IList<HttpPostedFile> complainDoc = ComplianFileUpload.PostedFiles;
                    for (int i = 0; i < complainDoc.Count; i++)
                    {
                        if (complainDoc[i].ContentLength > 0)
                        {
                            string nameOfUploadedFile = (int.Parse(ViewState["supplierId"].ToString())) + "_" + i.ToString();
                            string UploadedFileName = nameOfUploadedFile.Replace(" ", String.Empty);
                            string FileName = Path.GetFileName(complainDoc[i].FileName);
                            string filename1 = UploadedFileName + "." + FileName.Split('.').Last();

                            if(!System.IO.Directory.Exists(Server.MapPath("~/Supplier/ComplianDocuments")))
                            System.IO.Directory.CreateDirectory(Server.MapPath("~/Supplier/ComplianDocuments"));

                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Supplier/ComplianDocuments/" + filename1)))
                            {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Supplier/ComplianDocuments/" + filename1));
                            }

                            complainDoc[i].SaveAs(HttpContext.Current.Server.MapPath("~/Supplier/ComplianDocuments/" + UploadedFileName + '.' + FileName.Split('.').Last()));

                            ViewState["supplierComplianDocpath"] = "~/Supplier/ComplianDocuments/" + filename1;
                            int saveFilePath = suplierImageUploadController.saveUploadedSupplierComplianDoc((int.Parse(ViewState["supplierId"].ToString())), ViewState["supplierComplianDocpath"].ToString(), FileName, nameOfUploadedFile, 1);
                        }
                    }


                    if (result > 0)
                    {
                        result = supplierAssigneToCompanyController.FollowActiveSupplierByCompanyId((int.Parse(ViewState["supplierId"].ToString())), int.Parse(Session["CompanyId"].ToString()), rdoActive.Checked ? 1 : 0);
                        if (result > 0)
                        {
                            result = supplierRatingController.SupplierRating((int.Parse(ViewState["supplierId"].ToString())), int.Parse(Session["CompanyId"].ToString()), int.Parse(hdnSupplierRate.Value == "" ? "0" : hdnSupplierRate.Value), rdoBlockYes.Checked == true ? 1 : 0, rdoActive.Checked == true ? 1 : 0, LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), txtRemarks.Text);

                            List<int> supplierIds = new List<int>();
                            for (int i = 0; i < ddlAgentSupplier.Items.Count; i++)
                            {
                                if (ddlAgentSupplier.Items[i].Selected)
                                {
                                    supplierIds.Add(int.Parse(ddlAgentSupplier.Items[i].Value));
                                }
                            }
                            supplierController.UpdateSupplierAgent2(int.Parse(ViewState["supplierId"].ToString()), supplierIds);
                            if (result > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                                ClearFields();
                                gvSupplierList.DataSource = supplierAssigneToCompanyController.GetSupplierRequestsByCompanyId(int.Parse(Session["CompanyId"].ToString())).Where(c => c.IsApproved == 1).ToList();
                                gvSupplierList.DataBind();
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on saving ratings', showConfirmButton: false,timer: 1500}); });   </script>", false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on saving ratings', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on saving ratings', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please Select Supplier to Edit', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }
                
            }
            catch (Exception ex)
            {

            }

        }
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();            
        }        

        protected void lbtnview_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvUserDocuments.Rows[x].Cells[1].Text;
                filepath = filepath.Replace("~/", string.Empty);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { window.open('" + filepath + "','_blank'); });   </script>", false);

            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'File Not Found', showConfirmButton: true,timer: 4000}); });   </script>", false);
            }

        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                ViewState["supplierId"] = int.Parse(gvSupplierList.Rows[x].Cells[0].Text);
                SupplierAssignedToCompany supplierAssigneToCompanyObj = supplierAssigneToCompanyController.GetSupplierOfCompanyObj((int.Parse(ViewState["supplierId"].ToString())), int.Parse(Session["CompanyId"].ToString()));
                if (supplierAssigneToCompanyObj.SupplierId != 0)
                {
                    txtSupplierId.Text = supplierAssigneToCompanyObj.SupplierId.ToString();
                    txtSupplierName.Text = supplierAssigneToCompanyObj.SupplierName;
                    txtAddress1.Text = supplierAssigneToCompanyObj.Address1;
                    txtAddress2.Text = supplierAssigneToCompanyObj.Address2;
                    txtMobileNo.Text = supplierAssigneToCompanyObj.PhoneNo;
                    txtOfficeContactNo.Text = supplierAssigneToCompanyObj.OfficeContactNo;
                    txtEmailAddress.Text = supplierAssigneToCompanyObj.Email;
                    txtVatRegNo.Text = supplierAssigneToCompanyObj.VatRegistrationNumber;
                    txtBusinesRegNo.Text = supplierAssigneToCompanyObj.BusinessRegistrationNumber;
                    ddlCompanyType.SelectedValue = supplierAssigneToCompanyObj.CompanyType.ToString();
                    ddlBusinessCategory.DataSource = naturseOfBusinessController.FetchBusinessCategoryList();
                    ddlBusinessCategory.DataTextField = "BusinessCategoryName";
                    ddlBusinessCategory.DataValueField = "BusinessCategoryId";
                    ddlBusinessCategory.DataBind();
                    ddlBusinessCategory.Items.Insert(0, new ListItem("Select Business Category", "0"));
                    ddlSupplierType.SelectedValue = supplierAssigneToCompanyObj.SupplierType.ToString();
                    txtSupRegNo.Text = supplierAssigneToCompanyObj.SupplierRegistration;
                    chkRegSup.Checked = supplierAssigneToCompanyObj.IsRegisteredSupplier == 1 ? true : false;
                    if (supplierAssigneToCompanyObj.SupplierFollowing == 1)
                    {
                        rdoActive.Checked = true;
                        rdoInActive.Checked = false;
                    }
                    else
                    {
                        rdoInActive.Checked = true;
                        rdoActive.Checked = false;
                    }
                    ddlBusinessCategory.SelectedValue = supplierAssigneToCompanyObj.BusinessCatecory;
                    if (supplierAssigneToCompanyObj.SupplierLogo != "")
                    {
                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(supplierAssigneToCompanyObj.SupplierLogo)))
                        {
                            imageid.Src = supplierAssigneToCompanyObj.SupplierLogo + "?" + LocalTime.Now.Ticks.ToString();
                        }
                        else
                        {
                            imageid.Src = "~/LoginResources/images/noPerson.png" + "?" + LocalTime.Now.Ticks.ToString();
                        }

                        hdnSuplierLogoEdit.Value = imageid.Src.Replace("~/", "");
                        ViewState["supplierLogoPath"] = supplierAssigneToCompanyObj.SupplierLogo;
                    }
                    else
                    {

                    }

                    gvUserDocuments.DataSource = supplierAssigneToCompanyObj._SuplierImageUploadList;
                    gvUserDocuments.DataBind();

                    lstCompanyCategory.DataSource = itemCategoryMasterController.FetchItemCategoryList().Where(r => r.IsActive == 1).ToList();
                    lstCompanyCategory.DataTextField = "CategoryName";
                    lstCompanyCategory.DataValueField = "CategoryId";
                    lstCompanyCategory.DataBind();

                    if (supplierAssigneToCompanyObj._SupplierCategory.Count() > 0)
                    {
                        foreach (ListItem item1 in lstCompanyCategory.Items)
                        {
                            if (supplierAssigneToCompanyObj._SupplierCategory.Where(y => y.CategoryId == int.Parse(item1.Value)).Count() > 0)
                            {
                                item1.Selected = true;
                            }
                        }
                    }                   

                    SupplierRatings supplierRatingObj = new SupplierRatings();
                    supplierRatingObj = supplierRatingController.GetSupplierRatingBySupplierIdAndCompanyId((int.Parse(ViewState["supplierId"].ToString())), int.Parse(Session["CompanyId"].ToString()));
                    if (supplierRatingObj.Companyid != 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {  Decide(" + supplierRatingObj.Rating + "); });   </script>", false);
                        if (supplierRatingObj.Isbalcklist == 1)
                            rdoBlockYes.Checked = true;
                        else
                            rdoBlockNo.Checked = true;
                        txtRemarks.Text = supplierRatingObj.Remarks;
                    }
                    //List<Supplier> suppliers = supplierController.GetSupplierList();
                    ddlAgentSupplier.DataSource = supplierController.GetSupplierList();
                    ddlAgentSupplier.DataValueField = "SupplierId";
                    ddlAgentSupplier.DataTextField = "SupplierName";
                    ddlAgentSupplier.DataBind();
                    ddlAgentSupplier.Items.Insert(0, new ListItem("Select Supplier", ""));

                    List<SupplierAgent2> supplierAgentId = supplierController.getSupplierAgent2(int.Parse(ViewState["supplierId"].ToString()));
                    for (int i = 0; i < supplierAgentId.Count; i++) {
                        if (ddlAgentSupplier.Items.FindByValue(supplierAgentId[i].SupplierAgentId.ToString()) != null) {
                            ddlAgentSupplier.Items.FindByValue(supplierAgentId[i].SupplierAgentId.ToString()).Selected = true;
                        }
                    }

                    gvComplainDocument.DataSource = supplierAssigneToCompanyObj.SupplierComplainDocument;
                    gvComplainDocument.DataBind();

                    btnUpdate.Enabled = true;

                   
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ClearFields()
        {
            txtSupplierId.Text = "";
            txtSupplierName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtMobileNo.Text = "";
            txtOfficeContactNo.Text = "";
            txtEmailAddress.Text = "";
            txtVatRegNo.Text = "";
            txtBusinesRegNo.Text = "";
            ddlBusinessCategory.SelectedIndex = 0;
            ddlCompanyType.SelectedIndex = 0;
            imageid.Src = "~/LoginResources/images/noPerson.png" + "?" + LocalTime.Now.Ticks.ToString();
            gvUserDocuments.DataSource = new List<SupplierImageDetails>();
            gvUserDocuments.DataBind();
            lstCompanyCategory.Items.Clear();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {  Decide(" + 0 + "); });   </script>", false);

            rdoBlockYes.Checked = false;
            rdoBlockNo.Checked = false;
            rdoActive.Checked = false;
            rdoInActive.Checked = false;
            txtRemarks.Text = "";

            gvComplainDocument.DataSource= new List<SuplierImageUpload>();
            gvComplainDocument.DataBind();
            ddlAgentSupplier.Items.Clear();
            btnUpdate.Enabled = false;
            ddlSupplierType.SelectedIndex = 0;
            txtSupRegNo.Text = "";
            chkRegSup.Checked = false;
        }        

        protected void SearchAll_Click(object sender, EventArgs e)
        {
            var listSuppliers = supplierAssigneToCompanyController.GetSupplierRequestsByName(int.Parse(Session["CompanyId"].ToString()), txtSearch.Text).Where(c => c.IsApproved == 1).ToList();

            gvSupplierList.DataSource = listSuppliers;
            gvSupplierList.DataBind();
        }

        protected void lbtnComplainDocument_Click(object sender, EventArgs e)
        {
            int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string filepath = gvComplainDocument.Rows[x].Cells[1].Text;
            if (!string.IsNullOrEmpty(filepath) && File.Exists(HttpContext.Current.Server.MapPath(filepath)))
            {
                filepath = filepath.Replace("~/", string.Empty);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { window.open('" + filepath + "','Download'); });   </script>", false);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(filepath));
                HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath(filepath));
                HttpContext.Current.Response.End();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'File Not Found', showConfirmButton: true,timer: 4000}); });   </script>", false);
            }
        }
        

        protected void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
            int supplierId = Convert.ToInt32(hndSupplierId.Value);
            // inactive
            int status = supplierController.deleteSupplier(supplierId);
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                List<SupplierAssignedToCompany> listSuppliers = supplierAssigneToCompanyController.GetSupplierRequestsByCompanyId(int.Parse(Session["CompanyId"].ToString())).Where(c => c.IsApproved == 1).ToList();
                ViewState["SupplierList"] = new JavaScriptSerializer().Serialize(listSuppliers);
                gvSupplierList.DataSource = listSuppliers;
                gvSupplierList.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(), "none", "<script>   $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured while deleting', showConfirmButton: true,timer: 4000}); });   </script>", false);
            }
        }

        protected void lbtnDeleteUploadedFile_Click(object sender, EventArgs e)
        {            
            string fileId = hndUploadedFileId.Value;
            int deleteStatus = suplierImageUploadController.deleteUploadedSupplierFile(fileId);
            gvUserDocuments.DataSource = suplierImageUploadController.GetSupplierImagesBySupplierId((int.Parse(ViewState["supplierId"].ToString())));
            gvUserDocuments.DataBind();
        }

        protected void lbtnDeleteUploadedComplainFile_Click(object sender, EventArgs e)
        {
            string fileId = hndUploadedComplainFileId.Value;
            int deleteStatus = suplierImageUploadController.deleteUploadedComplianFile(fileId);
            gvComplainDocument.DataSource = suplierImageUploadController.GetSupplierComplianDocumentBySupplierId((int.Parse(ViewState["supplierId"].ToString())));
            gvComplainDocument.DataBind();
        }

        public class SupplierImageDetails
        {
            public SupplierImageDetails(int Id, int SupplieCode)
            {

                id = Id;
                supplierCode = SupplieCode;

            }

            private int id;
            private int supplierCode;

            public int Id
            {
                get { return id; }
                set { id = value; }
            }

            public int SupplieCode
            {
                get { return supplierCode; }
                set { supplierCode = value; }
            }
        }

        
    }
}