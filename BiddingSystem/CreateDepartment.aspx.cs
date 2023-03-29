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
    public partial class CreateDepartment : System.Web.UI.Page
    {
        #region Properties

        int adminId = 0;
        static int departmentId = 0;
        public List<string> companyList= new List<string>();
        List<CompanyDepartment> companyDepartments = new List<CompanyDepartment>();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();

        #endregion
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


                companyDepartments = companyDepartmentController.GetDepartmentList();
                foreach (var item in companyDepartments)
                {
                    companyList.Add(item.DepartmentName);

                }



                if (!IsPostBack)
                {
                    imageid.Src = "~/LoginResources/images/NoLogo.jpg" + "?" + LocalTime.Now.Ticks.ToString();
                    LoadGV();
                }
                msg.Visible = false;
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
                int departmentid = companyDepartmentController.saveDepartment(txtCompanyName.Text, LocalTime.Now, "Admin", LocalTime.Now, "Admin", chkIsavtive.Checked == true ? 1 : 0,"",txtAddress1.Text,"",txtCity.Text,txtCountry.Text,txtPhoneNo.Text, txtMobileNo.Text,txtFaxNo.Text,txtVatNo.Text);
                if (departmentid != -1)
                {
                    if (departmentid > 0)
                    {
                        if (fileUpload1.PostedFile != null && fileUpload1.PostedFile.FileName != "")
                        {
                            string nameOfUploadedFile = departmentid + "_1";
                            string UploadedFileName = nameOfUploadedFile.Replace(" ", String.Empty);
                            string FileName = Path.GetFileName(fileUpload1.PostedFile.FileName);
                            string filename1 = UploadedFileName + "." + FileName.Split('.').Last();
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Department/Logo/" + filename1)))
                            {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Department/Logo/" + filename1));
                            }

                            fileUpload1.SaveAs(HttpContext.Current.Server.MapPath("~/Department/Logo/" + UploadedFileName + '.' + FileName.Split('.').Last()));

                            string deptLogo = "~/Department/Logo/" + filename1;
                            int updateLogo = companyDepartmentController.updateDepartmentLogo(departmentid, deptLogo);
                        }


                            if (fileUpload2.PostedFile != null && fileUpload2.PostedFile.FileName != "")
                            {
                                string nameOfUploadedFile = departmentid + "_1";
                                string UploadedFileName = nameOfUploadedFile.Replace(" ", String.Empty);
                                string FileName = Path.GetFileName(fileUpload2.PostedFile.FileName);
                                string filename1 = UploadedFileName + "." + FileName.Split('.').Last();
                                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Department/TermsAndConditions/" + filename1)))
                                {
                                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Department/TermsAndConditions/" + filename1));
                                }

                                fileUpload2.SaveAs(HttpContext.Current.Server.MapPath("~/Department/TermsAndConditions/" + UploadedFileName + '.' + FileName.Split('.').Last()));

                                string depttermsConditions = "~/Department/TermsAndConditions/" + filename1;
                                int updateLogo = companyDepartmentController.updateDepartmentTermsConditions(departmentid, depttermsConditions);
                            }

                            DisplayMessage("Company has been created successfully", false);
                    }
                    else
                    {
                        DisplayMessage("Company Creation problem", true);
                    }
                }
                else
                {
                    DisplayMessage("Company Name already exists", true);
                }
            }



            if(btnSave.Text=="Update")
            {
                if (departmentId != 0)
                {

                    if (fileUpload1.PostedFile != null && fileUpload1.PostedFile.FileName != "")
                    {
                        string nameOfUploadedFile = departmentId + "_1";
                        string UploadedFileName = nameOfUploadedFile.Replace(" ", String.Empty);
                        string FileName = Path.GetFileName(fileUpload1.PostedFile.FileName);
                        string filename1 = UploadedFileName + "." + FileName.Split('.').Last();
                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Department/Logo/" + filename1)))
                        {
                            System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Department/Logo/" + filename1));
                        }

                        fileUpload1.SaveAs(HttpContext.Current.Server.MapPath("~/Department/Logo/" + UploadedFileName + '.' + FileName.Split('.').Last()));

                        hdnImgPathEdit.Value = "~/Department/Logo/" + filename1;

                    }

                        if (fileUpload2.PostedFile != null && fileUpload2.PostedFile.FileName != "")
                        {
                            string nameOfUploadedFile = departmentId + "_1";
                            string UploadedFileName = nameOfUploadedFile.Replace(" ", String.Empty);
                            string FileName = Path.GetFileName(fileUpload2.PostedFile.FileName);
                            string filename1 = UploadedFileName + "." + FileName.Split('.').Last();
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Department/TermsAndConditions/" + filename1)))
                            {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Department/TermsAndConditions/" + filename1));
                            }

                            fileUpload2.SaveAs(HttpContext.Current.Server.MapPath("~/Department/TermsAndConditions/" + UploadedFileName + '.' + FileName.Split('.').Last()));

                            hdnTGermsPathEdit.Value = "~/Department/TermsAndConditions/" + filename1;

                        }


                        int updateDepartmentStatus = companyDepartmentController.updateDepartment(departmentId, txtCompanyName.Text, LocalTime.Now, "Admin", chkIsavtive.Checked == true ? 1 : 0, hdnImgPathEdit.Value,txtAddress1.Text,"",txtCity.Text,txtCountry.Text,txtPhoneNo.Text, txtMobileNo.Text,txtFaxNo.Text,txtVatNo.Text, hdnTGermsPathEdit.Value);

                    if (updateDepartmentStatus != -1)
                    {
                        if (updateDepartmentStatus > 0)
                        {
                            DisplayMessage("Company has been updated successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Company update Problem", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("Company Name already exists", true);
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
                    gvDepartments.DataSource = companyDepartmentController.GetDepartmentList();
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
                departmentId = int.Parse(gvDepartments.Rows[x].Cells[1].Text);
                CompanyDepartment companyDepartmentObj = companyDepartmentController.GetDepartmentByDepartmentId(departmentId);
                if (companyDepartmentObj.DepartmentID != 0)
                {
                    txtCompanyName.Text = companyDepartmentObj.DepartmentName;
                    hdnImgPathEdit.Value = companyDepartmentObj.ImagePath;
                    hdnTGermsPathEdit.Value = companyDepartmentObj.TermConditionpath;
                    txtAddress1.Text = companyDepartmentObj.Address1;
                    txtCity.Text = companyDepartmentObj.City;
                    txtCountry.Text = companyDepartmentObj.Country;
                    txtPhoneNo.Text = companyDepartmentObj.PhoneNO;
                    txtMobileNo.Text = companyDepartmentObj.MobileNo;
                    txtFaxNo.Text = companyDepartmentObj.FaxNO;
                    txtVatNo.Text = companyDepartmentObj.VatNo;
                    spanTremCondition.Visible = true;
                    lbtnTermCondition.Text = companyDepartmentObj.DepartmentName + " Terms & Conditions";
                    if (companyDepartmentObj.IsActive == 1)
                    {
                        chkIsavtive.Checked = true;
                    }
                    else
                    {
                        chkIsavtive.Checked = false;
                    }
                    if (companyDepartmentObj.ImagePath != "" && companyDepartmentObj.ImagePath !=null)
                    {
                        imageid.Src = companyDepartmentObj.ImagePath + "?" + LocalTime.Now.Ticks.ToString();
                    }
                    else
                    {
                        imageid.Src = "~/LoginResources/images/NoLogo.jpg" + "?" + LocalTime.Now.Ticks.ToString();
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
                string companyId = hdnCompanyId.Value;
                if (companyId != "" && companyId != null)
                {
                        int deleteCompanyStatus = companyDepartmentController.deleteCompany(int.Parse(companyId));
                        if (deleteCompanyStatus > 0)
                        {
                            DisplayMessage("Company has been Deleted Successfully", false);
                            clearFields();
                            LoadGV();

                        }
                        else
                        {
                            DisplayMessage("Error on Delete Company", true);
                        }
                }
                else
                {
                    DisplayMessage("Please Select Company to Delete", true);
                }





               
                }
            catch (Exception)
            {

                throw;
            }
        }

        public string getJsonComanyList()
        {
            var DataList = companyList;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }

        private void clearFields()
        {
            txtCompanyName.Text = "";
            txtAddress1.Text = "";
            txtCity.Text = "";
            txtCountry.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtVatNo.Text = "";
            txtFaxNo.Text = "";
            btnSave.Text = "Save";
            imageid.Src = "~/LoginResources/images/NoLogo.jpg" + "?" + LocalTime.Now.Ticks.ToString();
            spanTremCondition.Visible = false;
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
    }
}