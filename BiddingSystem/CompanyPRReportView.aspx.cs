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
    public partial class CompanyPRReportView : System.Web.UI.Page
    {
       // int PRId =0;
      //  static string UserId = string.Empty;
      //  int CompanyId = 0;

     //   public List<string> PrBomDetails = new List<string>();

        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_BillOfMeterialController pr_BillOfMeterial = ControllerFactory.CreatePR_BillOfMeterialController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
               // CompanyId = int.Parse(Session["CompanyId"].ToString());
              //  UserId = Session["UserId"].ToString();
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

           // lblDateNow.Text = LocalTime.Today.ToString("dd-MM-yyyy");


            if (!IsPostBack)
            {
                try
                {
                    //Response.Redirect("AdminDashboard.aspx");
                    if (Request.QueryString["PrId"] != null)
                    {
                       ViewState["PRId"]  = int.Parse(Request.QueryString["PrId"].ToString());
                    }

                    List<PR_BillOfMeterial> prBom = pr_BillOfMeterial.GetListRejected(int.Parse(ViewState["PRId"].ToString())).OrderBy(c => c.ItemId).ToList();

                    foreach (var item in prBom)
                    {
                       List<string> PrBomDetails = new List<string>();
                        PrBomDetails.Add(item.ItemId + "-/" + item.SeqId + "-/" + item.Meterial);
                    }

                    lblDeptName.Enabled = true;
                    PR_Master prmaster = pr_MasterController.FetchApprovePRDataByDeptIdAndPRId(int.Parse(Session["CompanyId"].ToString()), int.Parse(ViewState["PRId"].ToString()));
                    lblPRCode.Text = prmaster.PrCode;
                    lblRequesterName.Text = prmaster.RequestedBy;
                    lblRef.Text = prmaster.OurReference;
                    lblDeptName.Text = companyDepartmentController.GetDepartmentByDepartmentId(int.Parse(Session["CompanyId"].ToString())).DepartmentName;
                    lblRequestedDate.Text = prmaster.DateOfRequest.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);

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
                List<PR_Details> pr_Details = pr_DetailController.FetchPR_DetailsByDeptIdAndPrId(int.Parse(ViewState["PRId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                gvPRView.DataSource = pr_Details;
                gvPRView.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}