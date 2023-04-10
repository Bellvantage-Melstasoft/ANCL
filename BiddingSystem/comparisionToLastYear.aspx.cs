using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class comparisionToLastYear : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<ComparisionToLastYearPOReport> comparisionToLastYearPOReport = new List<ComparisionToLastYearPOReport>();
                ComparisionToLastYearPOReportController comparisionToLastYearPOReportController = ControllerFactory.CreateComparisionToLastYearPOReportController();

                comparisionToLastYearPOReport = comparisionToLastYearPOReportController.GetComparisionToLastYearPOReports();

            }

        }
    }
}