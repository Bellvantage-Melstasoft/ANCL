using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class CustomerApproveSupplier : System.Web.UI.Page
    {
        int CompanyId = 0;
        static int supplierId = 0;
        string supplierLogoPath = string.Empty;
        string supplierDocpath = string.Empty;
        Supplier supplierObj = new Supplier();
        int startCode = 0;
        string userId = string.Empty;
        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        SuplierImageUploadController suplierImageUploadController = ControllerFactory.CreateSuplierImageUploadController();
        SupplierLoginController supplierLoginController = ControllerFactory.CreateSupplierLoginController();
        SupplierCategoryController supplierCategoryController = ControllerFactory.CreateSupplierCategoryController();
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        ItemCategoryMasterController itemCategoryMasterController = ControllerFactory.CreateItemCategoryMasterController();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefSupplier";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabSupplier";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyInitialRequest.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "approveSuppierLink";

                    CompanyId = int.Parse(Session["CompanyId"].ToString());
                    userId = Session["UserId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(userId), CompanyId, 3, 1) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                    {
                        Response.Redirect("AdminDashboard.aspx");
                    }

                    CompanyId = int.Parse(Session["CompanyId"].ToString());

                    if (Session["supplierId"].ToString() != null && Session["supplierId"].ToString() != "")
                    {
                        supplierId = int.Parse(Session["supplierId"].ToString());
                    }
                    else
                    {
                        Response.Redirect("CompanyInitialRequest.aspx");
                    }

                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }

                if (!IsPostBack)
                {
                    lstCompanyCategory.DataSource = itemCategoryMasterController.FetchItemCategoryList();
                    lstCompanyCategory.DataTextField = "CategoryName";
                    lstCompanyCategory.DataValueField = "CategoryId";
                    lstCompanyCategory.DataBind();

                    supplierObj = supplierController.GetSupplierBySupplierId(supplierId);
                        if (supplierObj.SupplierId != 0)
                        {
                            txtSupplierId.Text = supplierObj.SupplierId.ToString();
                            txtSupplierName.Text = supplierObj.SupplierName;
                            txtAddress1.Text = supplierObj.Address1;
                            txtAddress2.Text = supplierObj.Address2;
                            txtMobileNo.Text = supplierObj.PhoneNo;
                            txtOfficeContactNo.Text = supplierObj.OfficeContactNo;
                            txtRequestedDate.Text = supplierObj.RequestedDate;
                            txtEmailAddress.Text = supplierObj.Email;
                            txtUserName.Text = supplierObj._SupplierLogin.Username;
                            txtPassword.Text = supplierObj._SupplierLogin.Password;
                            txtConfirmPassword.Text = supplierObj._SupplierLogin.Password;
                            txtVatRegNo.Text = supplierObj.VatRegistrationNumber;
                            txtBusinesRegNo.Text = supplierObj.BusinessRegistrationNumber;
                            txtBusinessCategory.Text = supplierObj.NatureOfBusiness.ToString();
                        ddlCompanyType.SelectedValue = supplierObj.CompanyType.ToString();
                        if (supplierObj.SupplierLogo != "")
                        {
                            imageid.Src = supplierObj.SupplierLogo + "?" + LocalTime.Now.Ticks.ToString();
                        }
                        else
                        {
                            imageid.Src = "~/LoginResources/images/noPerson.png" + "?" + LocalTime.Now.Ticks.ToString();
        
                        }
                            

                            if (supplierObj._SuplierImageUploadList.Count() > 0)
                            {
                                gvUserDocuments.DataSource = supplierObj._SuplierImageUploadList;
                                gvUserDocuments.DataBind();
                            }
                            if (supplierObj._SupplierCategory.Count() > 0)
                            {
                                foreach (ListItem item in lstCompanyCategory.Items)
                                {
                                    if (supplierObj._SupplierCategory.Where(x => x.CategoryId == int.Parse(item.Value)).Count() > 0)
                                    {
                                        item.Selected = true;
                                    }
                                }
                            }

                        }
                   
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {

            int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string fileId = gvUserDocuments.Rows[x].Cells[0].Text;
            string filepath = gvUserDocuments.Rows[x].Cells[1].Text;
            int deleteStatus = suplierImageUploadController.deleteUploadedSupplierFile(fileId);
            if (deleteStatus > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
            }
            else
            {

            }
            gvUserDocuments.DataSource = suplierImageUploadController.GetSupplierImagesBySupplierId(supplierId);
            gvUserDocuments.DataBind();
        }

        protected void lbtnDownload_Click(object sender, EventArgs e)
        {
            int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string filepath = gvUserDocuments.Rows[x].Cells[1].Text;
            if (!string.IsNullOrEmpty(filepath) && File.Exists(HttpContext.Current.Server.MapPath(filepath)))
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(filepath));
                HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath(filepath));
                HttpContext.Current.Response.End();

            }
            else
            {
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.Write("File not be found!");


            }
        }

        protected void btnApprovee_Click(object sender, EventArgs e)
        {
            int approveSupplierStatus = supplierAssigneToCompanyController.ApproveSupplierByCompanyId(supplierId, CompanyId);
            if (approveSupplierStatus > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                Response.Redirect("CompanyInitialRequest.aspx");
                //DisplayMessage("Supplier has been approved successfully", false);
            }
            else
            {
                DisplayMessage("Supplier approval Failed", true);

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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompanyInitialRequest.aspx");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                int rejectStatus = supplierAssigneToCompanyController.RejectSupplierByCompanyId(supplierId, CompanyId);
                if (rejectStatus > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    Response.Redirect("CompanyInitialRequest.aspx");
                    //DisplayMessage("Supplier has been Rejected successfully", false);
                }
                else
                {
                    DisplayMessage("Supplier Rejection Failed", true);

                }
            }
            catch (Exception)
            {

               
            }
        }

        protected void lbtnview_Click(object sender, EventArgs e)
        {
           
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvUserDocuments.Rows[x].Cells[1].Text;
                if (!string.IsNullOrEmpty(filepath) && File.Exists(HttpContext.Current.Server.MapPath(filepath))) 
                {
                    filepath = filepath.Replace("~/", string.Empty);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { window.open('" + filepath + "','_blank'); });   </script>", false);

                }
                else
                {
                    DisplayMessage("Supplier document has been Missed", true);
                }

             
            }
            catch (Exception)
            {

            }
        }
        
    }
}