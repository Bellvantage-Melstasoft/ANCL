using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class ApproveMRN : System.Web.UI.Page
    {

        static string UserId = string.Empty;
        static int SubDepartmentID = 0;
        int CompanyId = 0;
        MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        MRNmasterController mRNmasterController = ControllerFactory.CreateMRNmasterController();
        public static List<MrnMaster> listMrnMaster = new List<MrnMaster>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "ApproveMRN.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "approveMRNLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 12, 3) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                    if (Session["IsHeadOfDepartment"] != null && Session["IsHeadOfDepartment"].ToString() == "1")
                    {

                        SubDepartmentID = int.Parse(Session["SubDepartmentID"].ToString());
                        try
                        {
                            ddlDepartments.DataSource = Utils.getallDepartments(CompanyId);
                            ddlDepartments.DataTextField = "SubDepartmentName";
                            ddlDepartments.DataValueField = "SubDepartmentID";
                            ddlDepartments.DataBind();
                            ddlDepartments.Items.Insert(0, new ListItem("Select Department", "0"));
                            ddlDepartments.SelectedValue = SubDepartmentID.ToString();
                            ddlDepartments.Enabled = false;

                            ddlCategories.DataSource = Utils.getallCtegories(CompanyId);
                            ddlCategories.DataTextField = "CategoryName";
                            ddlCategories.DataValueField = "CategoryId";
                            ddlCategories.DataBind();
                            ddlCategories.Items.Insert(0, new ListItem("Select a Categoty", "0"));

                            listMrnMaster = mrnController.fetchMrnList(SubDepartmentID).Where(mrn => mrn.IsApproved == 0).OrderByDescending(x => x.CreatedDate).ToList();
                            gvMRN.DataSource = listMrnMaster;
                            gvMRN.DataBind();


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







        protected void gvMRN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int mrnID = int.Parse(gvMRN.DataKeys[e.Row.RowIndex].Value.ToString());
                    GridView gvMRNDetails = e.Row.FindControl("gvMRND") as GridView;

                    gvMRNDetails.DataSource = mrnController.fetchMrnDList(mrnID);
                    gvMRNDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                int mrnID = 0;
                int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
                mrnID = int.Parse(gvMRN.Rows[x].Cells[1].Text);
                int marntype = int.Parse(gvMRN.Rows[x].Cells[8].Text);
                int CategoryId = int.Parse(gvMRN.Rows[x].Cells[9].Text);

                if (mrnController.ApproveOrRejectMrn(mrnID, 1) > 0)
                {
                    if (marntype==7 && CategoryId>0)
                    {
                        
                        mRNmasterController.AutoAssignStorekerper(mrnID, CategoryId);
                    }
                  //  gvMRN.DataSource = mrnController.fetchMrnList(SubDepartmentID).Where(mrn => mrn.IsApproved == 0).ToList();
                  //  gvMRN.DataBind();

                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); }); window.location.reload();   </script>", false);
                    
                   
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on approving MRN\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                int mrnID = 0;
                int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
                mrnID = int.Parse(gvMRN.Rows[x].Cells[1].Text);
               

                if (mrnController.ApproveOrRejectMrn(mrnID, 2) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    gvMRN.DataSource = listMrnMaster;
                    gvMRN.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on rejecting MRN\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvMRN.DataSource = mrnController.AdvanceSearch(CompanyId, int.Parse(ddlCategories.SelectedValue), int.Parse(ddlDepartments.SelectedValue), int.Parse(ddlAdvancesearchitems.SelectedValue), txtSearch.Text, "S");
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

        protected void ddlAdvancesearchitems_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            if (ddlAdvancesearchitems.SelectedIndex != 0)
            {
               
                txtSearch.Enabled = true;
                if (ddlAdvancesearchitems.SelectedValue=="3")
                {                  
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", 
                        "<script>    $(document).ready(function () { " +
                        " $( '#ContentSection_txtSearch' ).addClass( 'customDate' );"+
                        " $( '#ContentSection_txtSearch' ).attr('onchange', 'dateChange(this)');  " +
                        " $( '#ContentSection_txtSearch' ).prop('type', 'date') ;  " +
                        " $( '#ContentSection_txtSearch' ).attr('data-date', '');  " +
                        " $( '#ContentSection_txtSearch' ).attr('data-date-format', 'DD MMMM YYYY');  " +
                         " }); </script>", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", 
                        "<script>    $(document).ready(function () { "+
                        " $( '#ContentSection_txtSearch' ).removeClass( 'customDate' ); " +
                        "$( '#ContentSection_txtSearch' ).prop('type', 'text'); "+
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ddlAdvancesearchitems.SelectedIndex = 0;
            ddlCategories.SelectedIndex = 0;
            ddlDepartments.SelectedIndex = 0;
            gvMRN.DataSource = listMrnMaster;
            gvMRN.DataBind();
        }
    }
}