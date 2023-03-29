using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using System.Text;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class MRNInquiryForm : System.Web.UI.Page
    {
        MRNmasterController MrnMasterController = ControllerFactory.CreateMRNmasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        public static  List<SubDepartment> listSubDeparment = new List<SubDepartment>();
        public static List<CompanyLogin> CompanyLoginUserList = new List<CompanyLogin>();
        public static List<ItemCategory> listItemCategory = new List<ItemCategory>();
        static int UserId = 0;
        static int CompanyId = 0;
        static int SubDepartmentID = 0;
        public static List<MrnMaster> MrnMaster = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "MRNInquiryForm.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "MRNInquiryFormLink";


                UserId = int.Parse(Session["UserId"].ToString());
                CompanyId = int.Parse(Session["CompanyId"].ToString());

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                listItemCategory = itemCategoryController.FetchItemCategoryList(companyLogin.DepartmentId);
                listSubDeparment = subDepartmentController.getDepartmentList(companyLogin.DepartmentId);
                CompanyLoginUserList = companyLoginController.GetAllUserList();
                if ((!companyUserAccessController.isAvilableAccess(UserId, CompanyId, 5, 7) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                SubDepartmentID = int.Parse(Session["SubDepartmentID"].ToString());
                LoadCategory();
                LoadDepartment();
            }
            else
            {

            }
        }

        private void LoadCategory()
        {
            try
            {
                ddlCategories.DataSource = listItemCategory;
                ddlCategories.DataValueField = "CategoryId";
                ddlCategories.DataTextField = "CategoryName";
                ddlCategories.DataBind();
                ddlCategories.Items.Insert(0, new ListItem("Select A Categoty", "0"));

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LoadDepartment()
        {
            try
            {
                ddlDepartment.DataSource = listSubDeparment;
                ddlDepartment.DataValueField = "SubDepartmentID";
                ddlDepartment.DataTextField = "SubDepartmentName";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select A Department", "0"));
                ddlDepartment.SelectedValue = SubDepartmentID.ToString();
                ddlDepartment.Enabled = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            MrnMaster = MrnMasterController.AdvanceSearchMRNForInquiry(CompanyId, int.Parse(ddlAdvancesearchitems.SelectedValue), int.Parse(ddlCategories.SelectedValue), int.Parse(ddlDepartment.SelectedValue), txtSearch.Text);
            MrnMaster = MrnMaster.OrderByDescending(x => x.MrnID).ToList();
            gvMRN.DataSource = MrnMaster;
            gvMRN.DataBind();
            if (ddlAdvancesearchitems.SelectedValue == "3")
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                      "<script>    $(document).ready(function () { " +
                      " $( '#ContentSection_txtSearch' ).addClass( 'customDate' );" +
                      " $( '#ContentSection_txtSearch' ).attr('onchange', 'dateChange(this)');  " +
                      " $( '#ContentSection_txtSearch' ).prop('type', 'date') ;  " +
                      " $( '#ContentSection_txtSearch' ).attr('data-date', '');  " +
                      " $( '#ContentSection_txtSearch' ).attr('data-date-format', 'DD MMMM YYYY');  " +
                       " }); </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                        "<script>    $(document).ready(function () { " +
                        " $( '#ContentSection_txtSearch' ).removeClass( 'customDate' ); " +
                        "$( '#ContentSection_txtSearch' ).prop('type', 'text'); " +
                         " $( '#ContentSection_txtSearch' ).removeAttr('onchange');  " +
                         " $( '#ContentSection_txtSearch' ).removeAttr('data-date');  " +
                        " $( '#ContentSection_txtSearch' ). removeAttr('data-date-format'); " +
                        "  });  </script>", false);
            }

            if (ddlAdvancesearchitems.SelectedValue == "3" && txtSearch.Text != "")
            {
                txtSearch.Text = Convert.ToDateTime(txtSearch.Text).ToString("yyyy-MM-dd");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ddlAdvancesearchitems.SelectedIndex = 0;
            ddlCategories.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            MrnMaster = null;
            gvMRN.DataSource = MrnMaster;
            gvMRN.DataBind();
        }
        
        protected void ddlAdvancesearchitems_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            if (ddlAdvancesearchitems.SelectedIndex != 0)
            {
                txtSearch.Enabled = true;
                if (ddlAdvancesearchitems.SelectedValue == "3")
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                      "<script>    $(document).ready(function () { " +
                      " $( '#ContentSection_txtSearch' ).addClass( 'customDate' );" +
                      " $( '#ContentSection_txtSearch' ).attr('onchange', 'dateChange(this)');  " +
                      " $( '#ContentSection_txtSearch' ).prop('type', 'date') ;  " +
                      " $( '#ContentSection_txtSearch' ).attr('data-date', '');  " +
                      " $( '#ContentSection_txtSearch' ).attr('data-date-format', 'DD MMMM YYYY');  " +
                       " }); </script>", false);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                        "<script>    $(document).ready(function () { " +
                        " $( '#ContentSection_txtSearch' ).removeClass( 'customDate' ); " +
                        "$( '#ContentSection_txtSearch' ).prop('type', 'text'); " +
                         " $( '#ContentSection_txtSearch' ).removeAttr('onchange');  " +
                         " $( '#ContentSection_txtSearch' ).removeAttr('data-date');  " +
                        " $( '#ContentSection_txtSearch' ). removeAttr('data-date-format'); " +
                        "  });  </script>", false);
                }
            }
            else
            {
                txtSearch.Enabled = false;
            }
        }

        

        protected void gvMRN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int MrnID = int.Parse(gvMRN.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvMrnDetails = e.Row.FindControl("gvMrnDetails") as GridView;

                gvMrnDetails.DataSource = MrnMaster.Find(x => x.MrnID == MrnID).MrnDetails;
                gvMrnDetails.DataBind();
            }
        }

        protected void gvMRN_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMRN.PageIndex = e.NewPageIndex;
                gvMRN.DataSource = MrnMaster;
                gvMRN.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}