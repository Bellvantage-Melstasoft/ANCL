using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class CustomerGRNView : System.Web.UI.Page
    {

        GrnController grnController = ControllerFactory.CreateGrnController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CustomerGRNView.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "grnApprovalLink";

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 10) && companyLogin.Usertype != "S")
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

                        List<GrnMaster> grnForApproval = new List<GrnMaster>();
                        grnForApproval = grnController.grnForApproval(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));

                        if (Session["UserWarehouses"] != null)
                        {
                            if ((Session["UserWarehouses"] as List<UserWarehouse>).Count > 0)
                            {
                                grnForApproval = grnForApproval.Where(grn => (Session["UserWarehouses"] as List<UserWarehouse>).Any(w => w.WrehouseId == grn.WarehouseId)).ToList();
                            }
                        }

                        gvPurchaseOrder.DataSource = grnForApproval.OrderBy(x => x.CreatedDate);
                        gvPurchaseOrder.DataBind();



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
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                GrnId = int.Parse(gvPurchaseOrder.Rows[x].Cells[0].Text);
                Response.Redirect("ApproveGRN.aspx?GrnId=" + GrnId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int GrnId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                GrnId = int.Parse(gvPurchaseOrder.Rows[x].Cells[0].Text);
                Response.Redirect("ApproveGRN.aspx?GrnId=" + GrnId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}