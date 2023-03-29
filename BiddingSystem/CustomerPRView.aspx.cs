using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Globalization;

namespace BiddingSystem
{
    public partial class CustomerPRView : System.Web.UI.Page
    {
        static string UserId = string.Empty;
        int CompanyId = 0;
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        public static List<PR_Master> pr_Master = new List<PR_Master>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //UserId = Request.QueryString.Get("UserId");
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "CustomerPRView.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "approvePRLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 5, 3) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                if (int.Parse(UserId) != 0)
                {
                    if (Session["IsHeadOfWarehouse"] != null && Session["IsHeadOfWarehouse"].ToString() == "1")
                    {
                        try
                        {
                            ddlDepartments.DataSource = Utils.getallDepartments(CompanyId);
                            ddlDepartments.DataTextField = "SubDepartmentName";
                            ddlDepartments.DataValueField = "SubDepartmentID";
                            ddlDepartments.DataBind();
                            ddlDepartments.Items.Insert(0, new ListItem("Select Department", "0"));


                            ddlCategories.DataSource = Utils.getallCtegories(CompanyId);
                            ddlCategories.DataTextField = "CategoryName";
                            ddlCategories.DataValueField = "CategoryId";
                            ddlCategories.DataBind();
                            ddlCategories.Items.Insert(0, new ListItem("Select a Categoty", "0"));

                          
                            pr_Master = pr_MasterController.FetchApprovePRDataByDeptId(CompanyId).OrderByDescending(x => x.PrId).ToList();
                            foreach(PR_Master item in pr_Master)
                            {
                                item.PrDetails = pr_MasterController.GetPRDetails(item.PrId, CompanyId);
                            }
                            gvPurchaseRequest.DataSource = pr_Master;
                            gvPurchaseRequest.DataBind();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You must be a Department Head to view this page', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                    }
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int prid = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                prid = int.Parse(gvPurchaseRequest.Rows[x].Cells[1].Text);
                Response.Redirect("CustomerViewPurchaseRequisition.aspx?PrId=" + prid);
            }
            catch (Exception ex)
            {
                throw ex;
            }

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
          
            gvPurchaseRequest.DataSource = pr_MasterController.GetPRtobeApprovedForAdvanceSearch(CompanyId, int.Parse(ddlCategories.SelectedValue), int.Parse(ddlDepartments.SelectedValue), int.Parse(ddlAdvancesearchitems.SelectedValue), txtSearch.Text, "S");
            gvPurchaseRequest.DataBind();
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
            ddlDepartments.SelectedIndex = 0;
            gvPurchaseRequest.DataSource = pr_Master;
            gvPurchaseRequest.DataBind();
        }

        protected void gvPurchaseRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PrId = int.Parse(gvPurchaseRequest.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvPrDetails = e.Row.FindControl("gvPrDetails") as GridView;

                gvPrDetails.DataSource = pr_Master.Find(pr => pr.PrId == PrId).PrDetails;
                gvPrDetails.DataBind();
            }
        }

        protected void gvPrDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gvStatusLog = e.Row.FindControl("gvStatusLog") as GridView;
                GridView gvPrDetail = (gvStatusLog.NamingContainer as GridViewRow).NamingContainer as GridView;

                int PrId = int.Parse(gvPurchaseRequest.DataKeys[((e.Row.NamingContainer as GridView).NamingContainer as GridViewRow).RowIndex].Value.ToString());

                int prdId = int.Parse(gvPrDetail.DataKeys[e.Row.RowIndex].Value.ToString());

                gvStatusLog.DataSource = pr_Master.Find(pr => pr.PrId == PrId).PrDetails.Find(p => p.PrdId == prdId).PrDetailsStatusLogs;
                gvStatusLog.DataBind();
            }
        }
    }
}