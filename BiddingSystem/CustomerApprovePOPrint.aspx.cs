using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BiddingSystem
{
    public partial class CustomerApprovePOPrint : System.Web.UI.Page
    {
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CustomerApprovePOPrint.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "CustomerApprovePOPrintLink";

               // CompanyId = int.Parse(Session["CompanyId"].ToString());
              //  UserId = Session["UserId"].ToString();
               var companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                ViewState["DesignationId"] = companyLogin.DesignationId;
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 30) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
                //else if (Session["DesignationId"] == null || Session["DesignationId"].ToString() != "14")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You must be Head of Procurement to view this page', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
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
                        ////if (Session["IsHeadOfProcurement"] != null && Session["IsHeadOfProcurement"].ToString() == "1") // Head Of Procurment
                        ////{

                            List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
                        pOMasterListByDepartmentid = pOMasterController.GetPosForPrint(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));


                        gvPurchaseOrder.DataSource = pOMasterListByDepartmentid;
                        gvPurchaseOrder.DataBind();

                        ////}
                        ////else {
                        ////    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You have to be Head Of Procurement', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                        ////}
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e) {
            try {
                int PoId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                PoId = int.Parse(gvPurchaseOrder.Rows[x].Cells[0].Text);
                Response.Redirect("ViewApprovePOPrint.aspx?PoId=" + PoId + "&Redirect=CustomerApprovePONew");
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        
        
    }
}