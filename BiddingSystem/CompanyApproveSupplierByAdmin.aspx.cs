using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class CompanyApproveSupplierByAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string requestId = Request.QueryString.Get("RequestId");

            txtGroupName.Text = requestId;

            txtGroupName.Enabled = false;
        }
    }
}