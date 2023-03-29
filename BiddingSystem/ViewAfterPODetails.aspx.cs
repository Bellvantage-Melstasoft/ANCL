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
using System.Globalization;

namespace BiddingSystem
{
    public partial class ViewAfterPODetails : System.Web.UI.Page
    {
        AfterPOController afterPOController = ControllerFactory.createAfterPOController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewAfterPODetails.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewAfterPOLink";

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
                BindGV();
            }
        }

        private void BindGV()
        {
            List<AfterPO> afterPOList = new List<AfterPO>();
            afterPOList = afterPOController.getAfterPODetails();
            gvAfterPO.DataSource = afterPOList;
            gvAfterPO.DataBind();
        }

        protected void btnBasicSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                List<AfterPO> afterPOList = new List<AfterPO>();
                afterPOList = afterPOController.getAfterPODetailsByMonth(date);
                gvAfterPO.DataSource = afterPOList;
                gvAfterPO.DataBind();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }
    }
}