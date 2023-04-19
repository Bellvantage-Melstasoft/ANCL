﻿using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class AgeAnalysisReport : System.Web.UI.Page
    {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        AgeAnalysisController ageAnalysisController = ControllerFactory.CreateAgeAnalysisController();


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
                    BindDataDropDown();
                    BindSupplier();

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void BindDataSource()
        {
            List<AgeAnalysis> ageAnalysis = new List<AgeAnalysis>();
            ageAnalysis = ageAnalysisController.GetAgeAnalysis();
            gvAgeAnalysis.DataSource = ageAnalysis;
            gvAgeAnalysis.DataBind();
        }

        private void BindDataDropDown()
        {
            List<SubDepartment> departments = new List<SubDepartment>();
            SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();

            departments = subDepartmentController.getAllDepartmentList(int.Parse(Session["CompanyId"].ToString()));

            ddlSubdep.DataSource = departments;
            ddlSubdep.DataValueField = "SubDepartmentID";
            ddlSubdep.DataTextField = "SubDepartmentName";
            ddlSubdep.DataBind();
            ddlSubdep.Items.Insert(0, new ListItem("-Select Department-", ""));

        }

        private void BindSupplier()
        {
            List<Supplier> supplierList = new List<Supplier>();
            SupplierController supplierController = ControllerFactory.CreateSupplierController();
            supplierList = supplierController.GetAllSupplierList();
            ddlsupplier.DataSource = supplierList;
            ddlsupplier.DataTextField = "supplierName";
            ddlsupplier.DataValueField = "supplierId";
            ddlsupplier.DataBind();
            ddlsupplier.Items.Insert(0, new ListItem("-Select-", ""));
        }

        protected void gvAgeAnalysis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToInt32(e.Row.Cells[7].Text) > 0)
                {
                    DateTime date = DateTime.Parse(e.Row.Cells[1].Text);

                    // Calculate the difference in days
                    TimeSpan diff = DateTime.Now.Date - date;
                    int days = diff.Days;
                    e.Row.Cells[8].Text = days.ToString() + " Days";
                }
                else
                {
                    e.Row.Cells[8].Text = "No waiting";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<AgeAnalysis> ageAnalysis = new List<AgeAnalysis>();
            ageAnalysis = ageAnalysisController.GetAgeAnalysis();

            bool flag = false;

            if (txtEndDate.Text != "" && txtStartDate.Text != "")
            {
                ageAnalysis = ageAnalysis.Where(x => x.CreatedDate <= DateTime.Parse(txtEndDate.Text) && x.CreatedDate >= DateTime.Parse(txtStartDate.Text)).ToList();
                flag = true;
            }
            if (txtPoCode.Text != "")
            {
                ageAnalysis = ageAnalysis.Where(x => x.POCode == txtPoCode.Text).ToList();
                flag = true;
            }

            if (ddlsupplier.SelectedValue != "")
            {
                ageAnalysis = ageAnalysis.Where(x => x.SupplierId == Convert.ToInt32(ddlsupplier.SelectedValue)).ToList();
                flag = true;
            }

            if (ddlSubdep.SelectedValue != "")
            {
                ageAnalysis = ageAnalysis.Where(x => x.SubDepartmentId == Convert.ToInt32(ddlSubdep.SelectedValue)).ToList();
                flag = true;
            }

            if (flag == true)
            {
                gvAgeAnalysis.DataSource = ageAnalysis;

            }
            else
            {
                gvAgeAnalysis.DataSource = null;

            }
            gvAgeAnalysis.DataBind();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            BindDataSource();
        }
    }
}