using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Text.RegularExpressions;

namespace BiddingSystem {
    public partial class ViewPurchaseRequsition : System.Web.UI.Page {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        WarehouseControllerInterface WarehouseController = ControllerFactory.CreateWarehouseController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        SubDepartmentControllerInterface subDepartmentControllerInterface = ControllerFactory.CreateSubDepartmentController();
        MrnControllerV2 mrnControllerV2 = ControllerFactory.CreateMrnControllerV2();
        MrnDetailsStatusLogController mrnDetailStatusLogController = ControllerFactory.CreateMrnDetailStatusLogController();
        PrControllerV2 prControllerV2 = ControllerFactory.CreatePrControllerV2();

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefReports";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabReports";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPurchaseRequsition.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewPurchaseRequsitionLink";
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {

                

            }

        }


        protected void lBtnPR_Click(object sender, EventArgs e) {
            int PRId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[1].Text);
            Response.Redirect("PurchaseRequisitionPrReport.aspx?PrId=" + PRId);
        }

        protected void lBtnMrn_Click(object sender, EventArgs e) {
            int MrnId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            Response.Redirect("PurchaseRequisionReportNew.aspx?MrnId=" + MrnId);
        }

        protected void btnBasicSearch_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (rdbMrn.Checked)
                {
                    //string newString = Regex.Replace(txtMrnCode.Text, "[^.0-9]", "");
                    //int mrnCode = int.Parse(newString);
                    string mrnCode = txtMrnCode.Text;

                    gvMrnPR.DataSource = mrnControllerV2.FetchMRNByMRNCode(mrnCode, int.Parse(Session["CompanyId"].ToString()));
                    gvMrnPR.DataBind();
                }
                else
                {
                    //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                    //int prCode = int.Parse(newString);
                    string prCode = txtPrCode.Text;

                    gvMrnPR.DataSource = prControllerV2.FetchPRByPRCode(prCode, int.Parse(Session["CompanyId"].ToString()));
                    gvMrnPR.DataBind();
                }
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }

      
    }
}