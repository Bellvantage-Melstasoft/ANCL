using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;

namespace BiddingSystem
{
    public partial class ViewPrForPoCreation : System.Web.UI.Page
    {
      //  static string UserId = string.Empty;
       // int CompanyId = 0;
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForPoCreation.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "createPoLink";

              //  UserId = Session["UserId"].ToString();
             //   CompanyId = int.Parse(Session["CompanyId"].ToString());

                

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 15) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
                //else if (Session["DesignationId"] == null || Session["DesignationId"].ToString() != "25")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You must be Purchasing Officer to view this page', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                //}
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
                        List<PrMasterV2> pr_Master = pr_MasterController.GetPrListForPoCreation(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));

                        gvPurchaseRequest.DataSource = pr_Master.ToList();
                        gvPurchaseRequest.DataBind();
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
                int prid = 0;
                int PurchaseType = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                prid = int.Parse(gvPurchaseRequest.Rows[x].Cells[0].Text);
                PurchaseType = int.Parse(gvPurchaseRequest.Rows[x].Cells[15].Text);

                if (PurchaseType == 1)
                {
                    Response.Redirect("CreatePONew.aspx?PrId=" + prid);
                }
                else {

                    Response.Redirect("CreatePONewImports.aspx?PrId=" + prid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
    }
}