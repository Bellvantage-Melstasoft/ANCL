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
    public partial class SupplierRequestCompanyInner : System.Web.UI.Page
    {
        int supplierId = 0;
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        List<CompanyDepartment> companyDepartmentList = new List<CompanyDepartment>();
        List<SupplierAssignedToCompany> supplierAssigneToCompanyList = new List<SupplierAssignedToCompany>();
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

            if (!IsPostBack)
            {
                loadCompanyRequests();
            }
        }

        protected void btnSaveRequests_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvRequestCompany.Rows)
                {
                    int departmentId = int.Parse(gvRequestCompany.Rows[row.RowIndex].Cells[1].Text);
                    bool isSelected = (row.FindControl("chkSelect") as CheckBox).Checked;
                    if (isSelected == true)
                    {
                        int assignCompany = supplierAssigneToCompanyController.saveAssigneSupplierWithCompanyByCompany(supplierId, departmentId, LocalTime.Now, 0, 1, 1);
                    }
                    else
                    {
                        int assignCompany = supplierAssigneToCompanyController.unFollowCompanyBySupplier(supplierId, departmentId);
                    }

                    DisplayMessage("Details has been submitted successfully", false);
                    loadCompanyRequests();
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


        protected void btnFollow_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
                string companyId = gvRequestCompany.Rows[x].Cells[1].Text;
                int updateStatus = supplierAssigneToCompanyController.updateUnfollowSupplier(supplierId, int.Parse(companyId), 0);
                loadCompanyRequests();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void loadCompanyRequests()
        {
            companyDepartmentList = companyDepartmentController.GetDepartmentList();
            supplierAssigneToCompanyList = supplierAssigneToCompanyController.GetCompanyListBySupplierIdforRequest(supplierId);
            foreach (var item in companyDepartmentList)
            {
                foreach (var supplieritem in supplierAssigneToCompanyList)
                {
                    if (item.DepartmentID == supplieritem.DepartmentID)
                    {
                        item.isApproved = supplieritem.IsApproved;
                        item.isSupplierFollow = supplieritem.SupplierFollowing;
                        item.isTermsAgreed = supplieritem.IsAgreedTerms;
                    }
                }
            }
            gvRequestCompany.DataSource = companyDepartmentList;
            gvRequestCompany.DataBind();
        }

    }
}
