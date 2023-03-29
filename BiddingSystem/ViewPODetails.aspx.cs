using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Domain;
using CLibrary.Common;

namespace BiddingSystem
{
    public partial class ViewPODetails : System.Web.UI.Page
    {
        int CompanyId = 0;
        string userId = string.Empty;
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                CompanyId = int.Parse(Session["CompanyId"].ToString());
                userId = Session["UserId"].ToString();
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            if(!IsPostBack)
            {
                gvPurchaseOrders.DataSource = pOMasterController.GetPoMasterListByDepartmentId(CompanyId);
                gvPurchaseOrders.DataBind();

            }
        }
    }
}