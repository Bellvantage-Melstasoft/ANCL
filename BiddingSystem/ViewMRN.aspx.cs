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
    public partial class ViewMRN : System.Web.UI.Page
    {

        static string UserId = string.Empty;
        static int SubDepartmentID = 0;
        int CompanyId = 0;
        MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewMRN.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewMRNLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 12, 2) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                    if (Session["SubDepartmentID"] != null && Session["SubDepartmentID"].ToString() != "")
                    {
                        SubDepartmentID = int.Parse(Session["SubDepartmentID"].ToString());
                        try
                        {
                            gvMRN.DataSource = mrnController.fetchMrnList(SubDepartmentID).Where(mrn => mrn.IsApproved == 0).ToList();
                            gvMRN.DataBind();

                            gvApprovRejectMRN.DataSource = mrnController.fetchMrnList(SubDepartmentID).Where(mrn => mrn.IsApproved != 0).ToList();
                            gvApprovRejectMRN.DataBind();


                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You are not assigned to a department yet.', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                    }
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int mrnID = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                mrnID = int.Parse(gvMRN.Rows[x].Cells[1].Text);
                Response.Redirect("EditMRN.aspx?id=" + mrnID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        


        protected void gvApprovRejectMRN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int mrnID = int.Parse(gvApprovRejectMRN.DataKeys[e.Row.RowIndex].Value.ToString());
                    GridView gvMRNDetails = e.Row.FindControl("gvApprovRejectMRND") as GridView;

                    gvMRNDetails.DataSource = mrnController.fetchMrnDList(mrnID);
                    gvMRNDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

                    gvMRNDetails.DataSource=mrnController.fetchMrnDList(mrnID);
                    gvMRNDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}