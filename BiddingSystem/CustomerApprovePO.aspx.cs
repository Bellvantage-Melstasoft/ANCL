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
    public partial class CustomerApprovePO : System.Web.UI.Page
    {
       // static string UserId = string.Empty;
       // int CompanyId = 0;
       // int DesignationId = 0;
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        ItemCategoryApprovalController itemCategoryApprovalController = ControllerFactory.CreateItemCategoryApprovalController();
        //public static CompanyLogin companyLogin = new CompanyLogin();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CustomerApprovePO.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ApprovePOLink";

               // CompanyId = int.Parse(Session["CompanyId"].ToString());
              //  UserId = Session["UserId"].ToString();
               var companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                ViewState["DesignationId"] = companyLogin.DesignationId;
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 7) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                        if (Session["IsHeadOfProcurement"] != null && Session["IsHeadOfProcurement"].ToString() == "1") // Head Of Procurment
                        {
                            // Later change this to give access only to Head Of procurement to approve PO
                            List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
                            pOMasterListByDepartmentid = pOMasterController.GetPoMasterListByDepartmentId(int.Parse(Session["CompanyId"].ToString())).Where(W => W.IsApproved == 0).OrderByDescending(r => r.CreatedDate).ThenBy(i => i.POCode).ToList();
                            if (Request.QueryString.Get("UserId") != null)
                            {
                                //int clickedUserId = int.Parse(Request.QueryString.Get("UserId"));
                                //List<ItemCategoryPOApproval> listItemCategoryPOApproval = itemCategoryApprovalController.GetItemCategoryPOApprovalByDesignationId(DesignationId);
                                //pOMasterListByDepartmentid = pOMasterListByDepartmentid.Where(p => listItemCategoryPOApproval.Any(p2 => p2.Po_Id == p.PoID)).ToList();
                                gvPurchaseOrder.DataSource = pOMasterListByDepartmentid.OrderByDescending(x => x.PoID).ToList();
                                gvPurchaseOrder.DataBind();
                            }
                            else
                            {
                                gvPurchaseOrder.DataSource = pOMasterListByDepartmentid.OrderByDescending(x => x.PoID).ToList();
                                gvPurchaseOrder.DataBind();
                            }
                        }else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You have to be Head Of Procurement', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                        }
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
                int PoId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                PoId = int.Parse(gvPurchaseOrder.Rows[x].Cells[0].Text);
                Session["PoId"] = PoId;
                Response.Redirect("CompanyViewAndApprovePO.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void gvPurchaseOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
            if (Request.QueryString.Get("UserId") != null)
            {
                int clickedUserId = int.Parse(Request.QueryString.Get("UserId"));
                List<ItemCategoryPOApproval> listItemCategoryPOApproval = itemCategoryApprovalController.GetItemCategoryPOApprovalByDesignationId(int.Parse(ViewState["DesignationId"].ToString()));
                pOMasterListByDepartmentid = pOMasterListByDepartmentid.Where(p => listItemCategoryPOApproval.Any(p2 => p2.Po_Id == p.PoID)).ToList();
                gvPurchaseOrder.PageIndex = e.NewPageIndex;
                gvPurchaseOrder.DataSource = pOMasterListByDepartmentid.OrderByDescending(x => x.PoID).ToList();
                gvPurchaseOrder.DataBind();
            }
            else
            {
                gvPurchaseOrder.PageIndex = e.NewPageIndex;
                gvPurchaseOrder.DataSource = pOMasterListByDepartmentid.OrderByDescending(x => x.PoID).ToList();
                gvPurchaseOrder.DataBind();
            }
        }
    }
}