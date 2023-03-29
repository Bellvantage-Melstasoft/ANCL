using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class ViewPoForAddImportDetails : System.Web.UI.Page
    {
      //  static string UserId = string.Empty;
     //   int CompanyId = 0;
        //int DesignationId = 0;
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        ItemCategoryApprovalController itemCategoryApprovalController = ControllerFactory.CreateItemCategoryApprovalController();
      //  public static List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPoForAddImportDetails.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "createPoAddImportLink";

              //  CompanyId = int.Parse(Session["CompanyId"].ToString());
             //   UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
               ViewState["DesignationId"]  = companyLogin.DesignationId;
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 7) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                        // Later change this to give access only to Head Of procurement to approve PO                        
                        // pOMasterListByDepartmentid = pOMasterController.GetPoMasterListWithImport(CompanyId).Where(W => W.IsApproved == 0).OrderByDescending(r => r.CreatedDate).ThenBy(i => i.POCode).ToList();
                       var pOMasterListByDepartmentid = pOMasterController.GetPoMasterListWithImport(int.Parse(Session["CompanyId"].ToString())).Where(W => W.IsApproved == 1).OrderByDescending(r => r.CreatedDate).ThenBy(i => i.POCode).ToList();
                       // ViewState["pOMasterListByDepartmentid"] = new JavaScriptSerializer().Serialize(pOMasterListByDepartmentid);
                        if (Request.QueryString.Get("UserId") != null)
                        {
                           gvPurchaseOrder.DataSource = pOMasterListByDepartmentid.OrderBy(x => x.CreatedDate).ToList();
                           gvPurchaseOrder.DataBind();
                        }
                        else
                        {
                            gvPurchaseOrder.DataSource = pOMasterListByDepartmentid.OrderBy(x => x.CreatedDate).ToList();
                            gvPurchaseOrder.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        protected void btnAddImportDetail_Click(object sender, EventArgs e)
        {
            try
            {
                int PoId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                PoId = int.Parse(gvPurchaseOrder.Rows[x].Cells[0].Text);
                Session["PrId"] = gvPurchaseOrder.Rows[x].Cells[2].Text;
                Session["PrCode"] = gvPurchaseOrder.Rows[x].Cells[3].Text;
                Session["PoCode"] = gvPurchaseOrder.Rows[x].Cells[1].Text;
                Response.Redirect("AddImportDetails.aspx?PoId="+ PoId + "");
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
                gvPurchaseOrder.DataSource = pOMasterListByDepartmentid.OrderBy(x => x.CreatedDate).ToList();
                gvPurchaseOrder.DataBind();
            }
            else
            {
                gvPurchaseOrder.PageIndex = e.NewPageIndex;
                gvPurchaseOrder.DataSource = pOMasterListByDepartmentid.OrderBy(x => x.CreatedDate).ToList();
                gvPurchaseOrder.DataBind();
            }
        }
    }
}