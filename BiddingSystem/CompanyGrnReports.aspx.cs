using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class CompanyGrnReports : System.Web.UI.Page
    {
        static string UserId = string.Empty;
        int CompanyId = 0;
        GrnController grnController = ControllerFactory.CreateGrnController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();


        protected void Page_Load(object sender, EventArgs e)
        {
            ((BiddingAdmin)Page.Master).mainTabValue = "hrefReports";
            ((BiddingAdmin)Page.Master).subTabTitle = "subTabReports";
            ((BiddingAdmin)Page.Master).subTabValue = "CompanyGrnReports.aspx";
            ((BiddingAdmin)Page.Master).subTabId = "grnReportsLink";

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 8, 3) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                if (int.Parse(UserId) != 0)
                {
                    try
                    {
                        //List<GrnMaster> GrnMasterListByCompanyId = new List<GrnMaster>();
                        //GrnMasterListByCompanyId = grnController.GetGRNmasterListByDepartmentId(CompanyId);

                        //gvPurchaseOrder.DataSource = GrnMasterListByCompanyId;
                        //gvPurchaseOrder.DataBind();


                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int GrnId = 0;
                int PoID = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                GrnId = int.Parse(gvPurchaseOrder.Rows[x].Cells[0].Text);
                PoID = int.Parse(gvPurchaseOrder.Rows[x].Cells[1].Text);
                Session["GrnID"] = GrnId;
                Session["PoID"] = PoID;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('CompanyGrnReportView.aspx?PoID=" + PoID + "&GrnId=" + GrnId + "');", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void btnGrnCodeSearch_Click(object sender, EventArgs e)
        {
            string grnCode = txtGrnCode.Text;

            if (txtGrnCode.Text != "")
            {
                List<GrnMaster> GrnMasterListByCompanyid = new List<GrnMaster>();
                GrnMasterListByCompanyid = grnController.GetGRNmasterListBygrnCode(CompanyId, grnCode);

                gvPurchaseOrder.DataSource = GrnMasterListByCompanyid;
                gvPurchaseOrder.DataBind();
            }
            

        }

        protected void btnPoCodeSearch_Click(object sender, EventArgs e) {
            string poCode = txtPOCode.Text;

            //string newString = Regex.Replace(txtPOCode.Text, "[^.0-9]", "");
            //int poCode = int.Parse(newString);

            if (txtPOCode.Text != "") {
                List<GrnMaster> GrnMasterListByCompanyid = new List<GrnMaster>();
                GrnMasterListByCompanyid = grnController.GetGRNmasterListByPOCode(CompanyId, poCode);

                gvPurchaseOrder.DataSource = GrnMasterListByCompanyid;
                gvPurchaseOrder.DataBind();
            }


        }

        protected void btnGrnStatusSearch_Click(object sender, EventArgs e)
        {
            string status = ddlStatus.SelectedValue;
            List<GrnMaster> GrnMasterListByCompanyId = new List<GrnMaster>();
            GrnMasterListByCompanyId = grnController.GetGRNmasterListByDepartmentId(CompanyId).Where(x => x.IsApproved == int.Parse(status)).ToList();

            gvPurchaseOrder.DataSource = GrnMasterListByCompanyId;
            gvPurchaseOrder.DataBind();

        }
        //protected void btnGrnDateSearch_Click(object sender, EventArgs e)
        //{
        //    string status = ddlStatus.SelectedValue;
        //    List<GrnMaster> GrnMasterListByCompanyId = new List<GrnMaster>();
        //    GrnMasterListByCompanyId = grnController.GetgrnMasterListByByDateRange(CompanyId, DateTime.Parse(txtStartDate.Text.ProcessString()), DateTime.Parse(txtEndDate.Text.ProcessString()));
        //    gvPurchaseOrder.DataSource = GrnMasterListByCompanyId;
        //    gvPurchaseOrder.DataBind();

        //}
    }
}