﻿using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class CompanyPrReports : System.Web.UI.Page
    {

        // static string UserId = string.Empty;
        //   int CompanyId = 0;
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((BiddingAdmin)Page.Master).mainTabValue = "hrefReports";
            ((BiddingAdmin)Page.Master).subTabTitle = "subTabReports";
            ((BiddingAdmin)Page.Master).subTabValue = "CompanyPrReports.aspx";
            ((BiddingAdmin)Page.Master).subTabId = "prReportsLink";

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                //  CompanyId = int.Parse(Session["CompanyId"].ToString());
                //  UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 8, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                if (int.Parse(Session["UserId"].ToString()) != 0)
                {
                    try
                    {
                        //List<PR_Master> pr_Master = new List<PR_Master>();
                        //pr_Master = pr_MasterController.FetchApprovePRDataByDeptIdReports(int.Parse(Session["CompanyId"].ToString())).OrderByDescending(x => x.PrId).ToList();
                        //gvPurchaseRequest.DataSource = pr_Master;
                        //gvPurchaseRequest.DataBind();

                        BindDataDropDown();


                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void BindDataDropDown()
        {
            List<SubDepartment> departments = new List<SubDepartment>();
            SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();

            departments = subDepartmentController.getAllDepartmentList(int.Parse(Session["CompanyId"].ToString()));

            ddlDepartment.DataSource = departments;
            ddlDepartment.DataValueField = "SubDepartmentID";
            ddlDepartment.DataTextField = "SubDepartmentName";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));

        }

        private void BindDataSource()
        {
            List<PrMasterV2> pr_Master = new List<PrMasterV2>();
            pr_Master = pr_MasterController.FetchPrALl();
            gvPurchaseRequest.DataSource = pr_Master;
            gvPurchaseRequest.DataBind();
        }

        protected void btnPoCodeSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // ddlStatus.SelectedIndex = 0;
                //txtStartDate.Text = "";
                //txtEndDate.Text = "";
                //string prCode = txtPoCode.Text;
                //List<PR_Master> pr_Master = new List<PR_Master>();
                //pr_Master = pr_MasterController.FetchApprovePRDataByDeptIdReports(int.Parse(Session["CompanyId"].ToString())).Where(x => x.PrCode.Contains(prCode)).ToList();
                if (txtPoCode.Text != "")
                {
                    //string newString = Regex.Replace(txtPoCode.Text, "[^.0-9]", "");
                    //int PrCode = int.Parse(newString);
                    string PrCode = txtPoCode.Text;
                    List<PrMasterV2> pr_Master = pr_MasterController.FetchPrByPrCode(int.Parse(Session["CompanyId"].ToString()), PrCode);

                    ViewState["pr_Master"] = pr_Master;

                    gvPurchaseRequest.DataSource = pr_Master;
                    gvPurchaseRequest.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //protected void btnPoStatusSearch_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtPoCode.Text = "";
        //        txtStartDate.Text = "";
        //        txtEndDate.Text = "";
        //       // string status = ddlStatus.SelectedValue;
        //        List<PR_Master> pr_Master = new List<PR_Master>();
        //        pr_Master = pr_MasterController.FetchApprovePRDataByDeptIdReports(int.Parse(Session["CompanyId"].ToString())).Where(x => x.PrIsApproved == int.Parse(status)).ToList();
        //        gvPurchaseRequest.DataSource = pr_Master;
        //        gvPurchaseRequest.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        protected void btnPoDateSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //  ddlStatus.SelectedIndex = 0;
                txtPoCode.Text = "";
                //  string status = ddlStatus.SelectedValue;
                //List<PR_Master> prMasterListByDepartmentid = new List<PR_Master>();
                //prMasterListByDepartmentid = pr_MasterController.GetPrMasterListByDaterange(int.Parse(Session["CompanyId"].ToString()), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text));
                //gvPurchaseRequest.DataSource = prMasterListByDepartmentid;
                //gvPurchaseRequest.DataBind();
                //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $(document).ready(function () {  $('#ContentSection_txtStartDate').attr('data-date', moment($('#ContentSection_txtStartDate').val(), 'YYYY-MM-DD').format($('#ContentSection_txtStartDate').attr('data-date-format')));$('#ContentSection_txtEndDate').attr('data-date', moment($('#ContentSection_txtEndDate').val(), 'YYYY-MM-DD').format($('#ContentSection_txtEndDate').attr('data-date-format'))); });   </script>", false);

                if (txtStartDate.Text != "" && txtEndDate.Text != "")
                {
                    List<PrMasterV2> pr_Master = pr_MasterController.FetchPrByDate(int.Parse(Session["CompanyId"].ToString()), DateTime.Parse(txtEndDate.Text), DateTime.Parse(txtStartDate.Text));
                    ViewState["pr_Master"] = pr_Master;
                    gvPurchaseRequest.DataSource = pr_Master;
                    gvPurchaseRequest.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int PrId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                PrId = int.Parse(gvPurchaseRequest.Rows[x].Cells[0].Text);
                Session["PrId"] = PrId;
                Response.Redirect("ViewPRReport.aspx?PrId=" + PrId);
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('CompanyPRReportView.aspx?PrId="+ PrId + "');", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            BindDataSource();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<PrMasterV2> pr_Master = new List<PrMasterV2>();
            List<PrMasterV2> pr_Master2 = new List<PrMasterV2>();
            pr_Master = pr_MasterController.FetchPrALl();

            ViewState["pr_Master"] = pr_Master;

            if (ddlStatus.SelectedValue != "")
            {
                pr_Master = pr_Master.Where(x => x.IsPrApproved == Convert.ToInt32(ddlStatus.SelectedValue)).ToList();

            }
            if (ddlPRType.SelectedValue != "")
            {
                pr_Master = pr_Master.Where(x => x.PrType == Convert.ToInt32(ddlPRType.SelectedValue)).ToList();
            }

            if (ddlPurchasingType.SelectedValue != "")
            {
                pr_Master = pr_Master.Where(x => x.PurchaseType == Convert.ToInt32(ddlPurchasingType.SelectedValue)).ToList();

            }

            if (txtPoCode.Text != "")
            {
                pr_Master = pr_Master.Where(x => x.PrCode == txtPoCode.Text).ToList();
            }

            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                pr_Master = pr_Master.Where(x => x.CreatedDate <= DateTime.Parse(txtEndDate.Text) && x.CreatedDate >= DateTime.Parse(txtStartDate.Text)).ToList();
            }


            gvPurchaseRequest.DataSource = pr_Master;
            gvPurchaseRequest.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }


        protected void btnRun_ServerClick1(object sender, EventArgs e)
        {
            BindDataSource();

            // Remove the column you want to exclude
            int columnIndexToRemove = 10; // Specify the index of the column to remove (zero-based)
            gvPurchaseRequest.Columns[columnIndexToRemove].Visible = false;


            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Company PR Report" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvPurchaseRequest.GridLines = GridLines.Both;
            //tblTaSummary.HeaderStyle.Font.Bold = true;
            gvPurchaseRequest.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

            gvPurchaseRequest.Columns[columnIndexToRemove].Visible = true;
        }

        //protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    List<PrMasterV2> pr_Master = new List<PrMasterV2>();
        //    List<PrMasterV2> pr_Master2 = new List<PrMasterV2>();
        //    pr_Master = (List<PrMasterV2>)ViewState["pr_Master"];
        //    pr_Master = (List<PrMasterV2>)ViewState["pr_Master"];
        //    pr_Master2 = (List<PrMasterV2>)ViewState["pr_Master"];

        //    if (ddlStatus.SelectedValue != "")
        //    {
        //        pr_Master = pr_Master.Where(x => x.PrIsApproved == Convert.ToInt32(ddlStatus.SelectedValue)).ToList();
        //        ViewState["pr_Master"] = pr_Master;
        //        gvPurchaseRequest.DataSource = pr_Master;

        //    }
        //    else
        //    {
        //        gvPurchaseRequest.DataSource = pr_Master2;

        //    }



        //    gvPurchaseRequest.DataBind();
        //}

        //protected void ddlPRType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    List<PrMasterV2> pr_Master = new List<PrMasterV2>();
        //    List<PrMasterV2> pr_Master2 = new List<PrMasterV2>();
        //    pr_Master = (List<PrMasterV2>)ViewState["pr_Master"];
        //    pr_Master2 = (List<PrMasterV2>)ViewState["pr_Master"];

        //    if (ddlPRType.SelectedValue != "")
        //    {
        //        pr_Master = pr_Master.Where(x => x.PrType == Convert.ToInt32(ddlPRType.SelectedValue)).ToList();
        //        ViewState["pr_Master"] = pr_Master;
        //        gvPurchaseRequest.DataSource = pr_Master;

        //    }
        //    else
        //    {
        //        gvPurchaseRequest.DataSource = pr_Master2;


        //    }


        //    gvPurchaseRequest.DataBind();
        //}

        //protected void ddlPurchasingType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    List<PrMasterV2> pr_Master = new List<PrMasterV2>();
        //    List<PrMasterV2> pr_Master2 = new List<PrMasterV2>();

        //    pr_Master = (List<PrMasterV2>)ViewState["pr_Master"];
        //    pr_Master2 = (List<PrMasterV2>)ViewState["pr_Master"];


        //    if (ddlPurchasingType.SelectedValue != "")
        //    {
        //        pr_Master = pr_Master.Where(x => x.PurchaseType == Convert.ToInt32(ddlPurchasingType.SelectedValue)).ToList();
        //        ViewState["pr_Master"] = pr_Master;
        //        gvPurchaseRequest.DataSource = pr_Master;
        //    }
        //    else
        //    {
        //        gvPurchaseRequest.DataSource = pr_Master2;

        //    }



        //    gvPurchaseRequest.DataBind();
        //}
    }
}