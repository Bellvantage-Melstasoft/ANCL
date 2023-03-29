using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Data;
using System.IO;

using SelectPdf;
using System.Net.Mail;
using System.Net.Mime;

namespace BiddingSystem
{
    public partial class POPrintNew : System.Web.UI.Page
    {

        PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        PODetailsController pODetailsController = ControllerFactory.CreatePODetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        
       // private string PRId = string.Empty;
        
        //private string OurRef = string.Empty;
        //private string PrCode = string.Empty;
        //private string RequestedDate = string.Empty;
        //private string UserRef = string.Empty;
        //private string RequesterName = string.Empty;

      //  int CompanyId = 0;
       // int PoId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            

           

            if (!IsPostBack)
            {
               


                

            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}