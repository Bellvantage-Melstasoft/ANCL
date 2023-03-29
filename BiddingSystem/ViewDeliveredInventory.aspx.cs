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
    public partial class ViewDeliveredInventory : System.Web.UI.Page
    {

       // static string UserId = string.Empty;
        static int SubDepartmentID = 0;
       // int CompanyId = 0;
        MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        MRNDIssueNoteControllerInterface mrndinController = ControllerFactory.CreateMRNDIssueNoteController();
        SubDepartmentControllerInterface subdepartmentController = ControllerFactory.CreateSubDepartmentController();
        MrnDetailsStatusLogController mrnDetailsStatusLogController = ControllerFactory.CreateMrnDetailStatusLogController();
        MRNDetailController mRNDetailController = ControllerFactory.CreateMRNDetailController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewDeliveredInventory.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewDeliveredInventoryLink";

               ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                ViewState["UserId"] = Session["UserId"].ToString();

                

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 12, 5) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                if ((Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Count() > 0)) {

                    try {
                        gvDeliveredInventory.DataSource = mrndinController.fetchDeliveredMrndINList((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                        gvDeliveredInventory.DataBind();

                        gvReceivedInventory.DataSource = mrndinController.fetchReceivedMrndINList((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                        gvReceivedInventory.DataBind();


                    }
                    catch (Exception ex) {
                        throw ex;
                    }

                }

                else {
                    gvDeliveredInventory.DataSource = mrndinController.fetchDeliveredMrndINListByCompanyId(int.Parse(Session["CompanyId"].ToString()));
                    gvDeliveredInventory.DataBind();

                    gvReceivedInventory.DataSource = mrndinController.fetchReceivedMrndINListByCompanyId(int.Parse(Session["CompanyId"].ToString()));
                    gvReceivedInventory.DataBind();

                }




                //if (int.Parse(Session["UserId"].ToString()) != 0)
                //{
                //    if (Session["IsHeadOfDepartment"] != null && Session["IsHeadOfDepartment"].ToString() == "1")
                //    {
                //        SubDepartmentID = int.Parse(Session["SubDepartmentID"].ToString());
                //    }
                //    else
                //    {
                //        SubDepartmentID = subdepartmentController.getsubDepartment(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));
                //    }
                //    try
                //    {
                //        gvDeliveredInventory.DataSource = mrndinController.fetchDeliveredMrndINList(SubDepartmentID);
                //        gvDeliveredInventory.DataBind();

                //        gvReceivedInventory.DataSource = mrndinController.fetchReceivedMrndINList(SubDepartmentID);
                //        gvReceivedInventory.DataBind();


                //    }
                //    catch (Exception ex)
                //    {
                //        throw ex;
                //    }

                //}
            }
        }

        protected void btnReceivedMrn_Click(object sender, EventArgs e) {
            int MrnId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[2].Text);

            Response.Redirect("ViewMRNNew.aspx?MrnId=" + MrnId);
        }
        protected void btnMrn_Click(object sender, EventArgs e) {
            int MrnId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[3].Text);

            Response.Redirect("ViewMRNNew.aspx?MrnId=" + MrnId);
        }
        protected void btnReject_Click(object sender, EventArgs e) {

            MRNDIssueNote mrndin = new MRNDIssueNote();

            mrndin.RejectedBy = int.Parse(Session["UserId"].ToString());
            mrndin.MrndInID = int.Parse(hdnMrndInID.Value);
            mrndin.MrndID = int.Parse(hdnMrndID.Value);
            mrndin.IssuedQty = decimal.Parse(hdnQty.Value);

            MrnDetails mrnDetail = mRNDetailController.GetMrnDetailsByMrndId(int.Parse(hdnMrndID.Value));
            decimal receivedQty = mrnDetail.ReceivedQty;
            int status = 0;

            if (receivedQty > 0) {
                status = 12;
            }
            else {
                status = 5;
            }

            int result = mrndinController.updateIssueNoteAfterRejected(mrndin, status);


            if (result > 0) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                mrnDetailsStatusLogController.InsertLogRejection(int.Parse(hdnMrndID.Value), int.Parse(Session["UserId"].ToString()));

                gvDeliveredInventory.DataSource = mrndinController.fetchDeliveredMrndINList((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                gvDeliveredInventory.DataBind();

                gvReceivedInventory.DataSource = mrndinController.fetchReceivedMrndINList((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                gvReceivedInventory.DataBind();
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Material Request Note\"; $('#errorAlert').modal('show'); });   </script>", false);
            }


        }
        protected void btnReceive_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(((Button)sender).NamingContainer);

            MRNDIssueNote mrndin = new MRNDIssueNote();
            mrndin.MrndInID= int.Parse(row.Cells[0].Text);
            mrndin.ReceivedBy = int.Parse(Session["UserId"].ToString());
            mrndin.MrndID = int.Parse(row.Cells[1].Text);

            int result = mrndinController.updateIssueNoteBeforeConfirmation(mrndin);

            if(result>0)
            {
                //result = mrnController.updateMRNDReceivedQty(int.Parse(row.Cells[1].Text), decimal.Parse(row.Cells[7].Text));
                
                if(result>0)
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('successMessage').innerHTML = \"Inventory updated successfully\"; $('#SuccessAlert').modal('show'); });   </script>", false);
                    mrnDetailsStatusLogController.InsertLogReceive(int.Parse(row.Cells[1].Text),int.Parse(Session["UserId"].ToString()));

                    gvDeliveredInventory.DataSource = mrndinController.fetchDeliveredMrndINList((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                    gvDeliveredInventory.DataBind();

                    gvReceivedInventory.DataSource = mrndinController.fetchReceivedMrndINList((Session["UserDepartments"] as List<UserSubDepartment>).Where(d => d.IsHead == 1).Select(d => d.DepartmentId).ToList());
                    gvReceivedInventory.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Material Request Note\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Material Request Issue Note\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
        }
    }
}