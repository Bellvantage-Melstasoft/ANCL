using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class CompanyMRReportView : System.Web.UI.Page
    {
       // int MrnId =0;
      //  static string UserId = string.Empty;
       // int CompanyId = 0;

      //  public List<string> PrBomDetails = new List<string>();

        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_BillOfMeterialController pr_BillOfMeterial = ControllerFactory.CreatePR_BillOfMeterialController();

        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        MRNmasterController mrnmasterController = ControllerFactory.CreateMRNmasterController();
        SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                //CompanyId = int.Parse(Session["CompanyId"].ToString());
               // UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 8, 1) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                try
                {
                    if (Request.QueryString["MrId"] != null)
                    {
                      ViewState["MrnId"] = int.Parse(Request.QueryString["MrId"].ToString());
                    }
                    
                    MRN_Master mrMaster = mrnmasterController.FetchMRNByMrnId(int.Parse(ViewState["MrnId"].ToString()));
                    lblMRCode.Text = mrMaster.MrnId.ToString();
                    lblCompanyName.Text = companyDepartmentController.GetDepartmentByDepartmentId(int.Parse(Session["CompanyId"].ToString())).DepartmentName;
                    lblCreatedDate.Text = mrMaster.CreatedDateTime.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                    lblRequestedDate.Text =  mrMaster.ExpectedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                    lblRequesterName.Text = companyLoginController.GetUserbyuserId(mrMaster.CreatedBy).FirstName;
                    lblSubDepName.Text = subDepartmentController.getDepartmentByID(mrMaster.SubDepartmentId).SubDepartmentName;
                    BindGV();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public string getJsonReportPRBom()
        {
            List<string> PrBomDetails = new List<string>();
            var DataList = PrBomDetails;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }

        private void BindGV()
        {
            try
            {
                List<MrnDetails> mrn_Details = mrnmasterController.FetchMRNItemDetails(int.Parse(ViewState["MrnId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                gvMRView.DataSource = mrn_Details;
                gvMRView.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}