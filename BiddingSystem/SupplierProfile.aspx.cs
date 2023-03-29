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
using System.Text.RegularExpressions;

namespace BiddingSystem
{
    public partial class SupplierProfile : System.Web.UI.Page
    {
        int supplierId = 0;
        Supplier SupplierObj = new Supplier();
        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        SuplierImageUploadController suplierImageUploadController = ControllerFactory.CreateSuplierImageUploadController();
        SupplierLoginController supplierLoginController = ControllerFactory.CreateSupplierLoginController();
        SupplierCategoryController supplierCategoryController = ControllerFactory.CreatesupplierCategoryController();
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        NaturseOfBusinessController naturseOfBusinessController = ControllerFactory.CreateNaturseOfBusinessController();
        ItemCategoryMasterController itemCategoryMasterController = ControllerFactory.CreateItemCategoryMasterController();

        string supplierLogoPath = string.Empty;
        string supplierDocpath = string.Empty;
        int startCode = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

          
            if (Session["supplierId"] != null && Session["supplierId"].ToString() != "")
            {
                supplierId = int.Parse(Session["supplierId"].ToString());
            }
            else
            {
                Response.Redirect("LoginPageSupplier.aspx");
            }
            
            if(!IsPostBack)
            {
                ddlBusinessCategory.DataSource = naturseOfBusinessController.FetchBusinessCategoryList();
                ddlBusinessCategory.DataTextField = "BusinessCategoryName";
                ddlBusinessCategory.DataValueField = "BusinessCategoryId";
                ddlBusinessCategory.DataBind();
                ddlBusinessCategory.Items.Insert(0, new ListItem("Select Business Category", "0"));

                //lstCompanyList.DataSource = companyDepartmentController.GetDepartmentList().Where(x => x.IsActive == 1).ToList();
                //lstCompanyList.DataTextField = "departmentName";
                //lstCompanyList.DataValueField = "DepartmentID";
                //lstCompanyList.DataBind();

                lstCategory.DataSource = itemCategoryMasterController.FetchItemCategoryList().Where(x => x.IsActive == 1).ToList();
                lstCategory.DataTextField = "CategoryName";
                lstCategory.DataValueField = "CategoryId";
                lstCategory.DataBind();



                supplierId = int.Parse(Session["supplierId"].ToString());
                SupplierObj = supplierController.GetSupplierBySupplierId(supplierId);
                if (SupplierObj.SupplierId != 0)
                {
                    txtSupplierName.Text = SupplierObj.SupplierName;
                    txtUserName.Text = SupplierObj._SupplierLogin.Username;
                    txtAddress1.Text = SupplierObj.Address1;
                    txtAddress2.Text = SupplierObj.Address2;
                    txtEmailAddress.Text = SupplierObj.Email;
                    txtBusinesRegNo.Text = SupplierObj.BusinessRegistrationNumber;
                    txtMobileNo.Text = SupplierObj.PhoneNo;
                    txtOfficeContactNo.Text = SupplierObj.OfficeContactNo;
                    txtVatRegNo.Text = SupplierObj.VatRegistrationNumber;
                    ddlBusinessCategory.SelectedValue = SupplierObj.BusinessCatecory.ToString();
                    ddlCompanyType.SelectedValue = SupplierObj.CompanyType.ToString();
                    hdnFileUpdate.Value = SupplierObj.SupplierLogo;

                    if (SupplierObj.SupplierLogo != "" && System.IO.File.Exists(HttpContext.Current.Server.MapPath(SupplierObj.SupplierLogo)))
                    {
                        imageid.Src = SupplierObj.SupplierLogo + "?" + LocalTime.Now.Ticks.ToString(); ;
                    }
                    else
                    {
                        imageid.Src = "~/LoginResources/images/noPerson.png" + "?" + LocalTime.Now.Ticks.ToString();
                    }

                    if (SupplierObj._SuplierImageUploadList.Count() > 0)
                    {
                        gvUserDocuments.DataSource = SupplierObj._SuplierImageUploadList;
                        gvUserDocuments.DataBind();
                    }
                    if (SupplierObj._SupplierCategory.Count() > 0)
                    {
                        foreach (ListItem item in lstCategory.Items)
                        {
                            if (SupplierObj._SupplierCategory.Where(x => x.CategoryId == int.Parse(item.Value)).Count() > 0)
                            {
                                item.Selected = true;
                            }
                        }
                    }

                    //List<SupplierAssigneToCompany> AssignedCompanies = supplierAssigneToCompanyController.GetCompanyListBySupplierId(supplierId);

                    //foreach (ListItem company in lstCompanyList.Items)
                    //{
                    //    foreach (var  Assignedcompany in AssignedCompanies)
                    //    {
                    //        if (int.Parse(company.Value) == Assignedcompany.CompanyId && Assignedcompany.SupplierFollowing==1)
                    //        {
                    //            company.Selected = true;
                    //        }

                    //    }
                    //}

                    }
            }
            if(hdnClearLogo.Value == "1")
            {
                imageid.Src = "~/LoginResources/images/noPerson.png" + "?" + LocalTime.Now.Ticks.ToString();
            }
            msg.Visible = false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileUploadLogo.PostedFile != null && fileUploadLogo.PostedFile.FileName != "")
                {
                    string nameOfUploadedFile = supplierId + "_1";
                    string UploadedFileName = nameOfUploadedFile.Replace(" ", String.Empty);
                    string FileName = Path.GetFileName(fileUploadLogo.PostedFile.FileName);
                    string filename1 = UploadedFileName + "." + FileName.Split('.').Last();
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Supplier/Logo/" + filename1)))
                    {
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Supplier/Logo/" + filename1));
                    }

                    fileUploadLogo.SaveAs(HttpContext.Current.Server.MapPath("~/Supplier/Logo/" + UploadedFileName + '.' + FileName.Split('.').Last()));

                     hdnFileUpdate.Value = "~/Supplier/Logo/" + filename1;
                }

                int updatesupplier = supplierController.updateSupplier(supplierId, txtSupplierName.Text, txtAddress1.Text, txtAddress2.Text,  txtOfficeContactNo.Text, txtMobileNo.Text, txtBusinesRegNo.Text, txtVatRegNo.Text, int.Parse(ddlCompanyType.SelectedValue), int.Parse(ddlBusinessCategory.SelectedValue), hdnFileUpdate.Value, 0, 1, 1, 1 , "",0 , 0, ""); // since not using Supplier Login temporary email empty send
                if (updatesupplier > 0)
                {

                    int deleteSupplierCategory = supplierCategoryController.deleteSupplierCategoryBySupplierid(supplierId);
                    foreach (ListItem item in lstCategory.Items)
                    {
                        if (item.Selected)
                        {
                            supplierCategoryController.saveSupplierCategory(supplierId, int.Parse(item.Value), 1);
                        }
                    }
                    

                    if (fileUploadDocs.HasFile)
                    {
                        List<SupplierImageDetails> supplierImageDetails = new List<SupplierImageDetails>();
                        String[] ExistingFilesNames = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Supplier/Documents/")).Select(Path.GetFileName).ToArray();
                        int[] supplierCodes;
                        for (int i = 1; i < ExistingFilesNames.Length; i++)
                        {
                            var splitFullFileName = ExistingFilesNames[i].Split('.').Reverse().ToList<string>();
                            string supplierCodeAndSeq = splitFullFileName[1];
                            string[] parts = supplierCodeAndSeq.Split('_');
                            supplierImageDetails.Add(new SupplierImageDetails(int.Parse(parts[0]), int.Parse(parts[1])));

                        }
                        if (supplierImageDetails.Where(r=>r.Id==supplierId).Count() > 0)
                        {
                             startCode = supplierImageDetails.Where(s => s.Id == supplierId).ToList().Max(x => x.SupplieCode);
                        }



                        HttpFileCollection hfc = Request.Files;

                        if (hfc.Count <= 11)    // 10 FILES RESTRICTION.
                        {

                            for (int i = 1; i <= hfc.Count - 1; i++)
                            {


                                HttpPostedFile hpf = hfc[i];
                                string nameOfUploadedFile = supplierId + "_" + (i + startCode).ToString();
                                string UploadedFileName = nameOfUploadedFile.Replace(" ", String.Empty);
                                string FileName = Path.GetFileName(hpf.FileName);
                                string filename1 = UploadedFileName + "." + FileName.Split('.').Last();
                                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Supplier/Documents/" + filename1)))
                                {
                                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Supplier/Documents/" + filename1));
                                }

                                hpf.SaveAs(HttpContext.Current.Server.MapPath("~/Supplier/Documents/" + UploadedFileName + '.' + FileName.Split('.').Last()));

                                supplierDocpath = "~/Supplier/Documents/" + filename1;
                                int saveFilePath = suplierImageUploadController.saveUploadedSupplierImage(supplierId, supplierDocpath, Regex.Replace(FileName, "[@,\\\";'\\\\]", string.Empty) , nameOfUploadedFile, 1);
                            }
                        }


                    }



                    DisplayMessage("Profile has been updated successfully", false);
                hdnClearLogo.Value = "";
                Response.Redirect("SupplierProfile.aspx");
                }
        
            gvUserDocuments.DataSource = suplierImageUploadController.GetSupplierImagesBySupplierId(supplierId);
            gvUserDocuments.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("SupplierIndex.aspx");
            }
            catch (Exception)
            {

            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {

            string imageId = hdnUploadFileId.Value;
            string imagePath = hdnUploadFilePath.Value;

            if (imageId != "" && imageId != null && imagePath != "" && imagePath != null)
            {
                int deleteStatus = suplierImageUploadController.deleteUploadedSupplierFile(imageId);
                if (deleteStatus > 0)
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(imagePath));
                    DisplayMessage("File has been Deleted successfully", false);
                }
                else
                {
                    DisplayMessage("Error on Delete File", true);
                }
                gvUserDocuments.DataSource = suplierImageUploadController.GetSupplierImagesBySupplierId(supplierId);
                gvUserDocuments.DataBind();
            }
            else
            {
                DisplayMessage("Please Select File to Delete", true);
            }
        }

        protected void lbtnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                
            int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string filepath = gvUserDocuments.Rows[x].Cells[3].Text;

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
                DisplayMessage("File not be found", true);
              //  HttpContext.Current.Response.ContentType = "text/plain";
               // HttpContext.Current.Response.Write("File not be found!");
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        protected void lbtnview_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvUserDocuments.Rows[x].Cells[3].Text;
                System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(filepath));
            }
            catch (Exception)
            {

            }
           
        }
        protected void gvUserDocuments_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            gvUserDocuments.DataSource = suplierImageUploadController.GetSupplierImagesBySupplierId(supplierId);
            gvUserDocuments.PageIndex = e.NewPageIndex;
            gvUserDocuments.DataBind();
           
        }
    }
}