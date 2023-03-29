using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class ClosedManualBids : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefManualBids";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabManualBids";
                    ((BiddingAdmin)Page.Master).subTabValue = "ClosedManualBids.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "closebidsManuallnk";


                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }


                if (!IsPostBack)
                {

                }
            }
            catch (Exception)
            {

            }
        }
    }
}