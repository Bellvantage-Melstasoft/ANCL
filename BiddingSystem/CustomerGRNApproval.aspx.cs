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
    public partial class CustomerGRNApproval : System.Web.UI.Page
    {
        PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
        GrnController grnController = ControllerFactory.CreateGrnController();
        GRNDetailsController gRNDetailsController = ControllerFactory.CreateGRNDetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
      //  static string UserId = string.Empty;
      //  private string PRId = string.Empty;

       // private string UserDept = string.Empty;
        //private string OurRef = string.Empty;
        //private string PrCode = string.Empty;
        //private string RequestedDate = string.Empty;
        //private string UserRef = string.Empty;
        //private string RequesterName = string.Empty;

       // int CompanyId = 0;
      //  int GrnID = 0;
       // int PoID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CustomerGRNView.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "grnApprovalLink";


               // CompanyId = int.Parse(Session["CompanyId"].ToString());
              //  UserId = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 9) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }


                if (Session["GrnID"] != null)
                {
                   ViewState["GrnID"]  = int.Parse(Session["GrnID"].ToString());
                    ViewState["PoID"] = int.Parse(Session["PoID"].ToString());
                }
                else
                {
                    Response.Redirect("CustomerGRNView.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

            lblDateNow.Text = LocalTime.Today.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
         

            if(!IsPostBack){
                try
                {
                   
                    GrnMaster grnMaster = grnController.GetGrnMasterByGrnID(int.Parse(ViewState["GrnID"].ToString()), int.Parse(ViewState["PoID"].ToString()));
                    PR_Master prMaster = pR_MasterController.FetchApprovePRDataByPRId(grnMaster._POMaster.BasePr);
                    CompanyDepartment companyDepartment = companyDepartmentController.GetDepartmentByDepartmentId(int.Parse(Session["CompanyId"].ToString()));
                    lblPOCode.Text = grnMaster._POMaster.POCode;
                    lblSupplierName.Text = grnMaster._Supplier.SupplierName; ;
                    lblAddress.Text = grnMaster._Supplier.Address1 +"," + grnMaster._Supplier.Address2;
                    lblInvoiceNo.Text = grnMaster.InvoiceNo;
                    lblCompanyName.Text = companyDepartment.DepartmentName;
                    lblRefNo.Text = prMaster.OurReference;

                    decimal subtotal = 0;
                    decimal vatTotal = 0;
                    decimal nbtTotal = 0;
                    decimal totalAmount = 0;


                    foreach (var item in grnMaster._GrnDetailsList.Where(x=>x.IsGrnApproved==0).ToList())
                    {
                        subtotal += item.Quantity * item.ItemPrice;
                        vatTotal += item.VatAmount;
                        nbtTotal += item.NbtAmount;
                        totalAmount += (item.Quantity * item.ItemPrice) + item.VatAmount + item.NbtAmount;
                    }
                    lblSubtotal.Text = subtotal.ToString("n");
                    lblVatTotal.Text = vatTotal.ToString("n");
                    lblNbtTotal.Text = nbtTotal.ToString("n");
                    lblTotal.Text = totalAmount.ToString("n");
                    lblgrnComment.InnerText = grnMaster.GrnNote;
                    lblReceiveddate.InnerText = grnMaster.GoodReceivedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                  
                    lblCompanyAddress.Text = grnMaster._companyDepartment.Address2 != "" ? grnMaster._companyDepartment.Address1 + ",</br>" + grnMaster._companyDepartment.Address2 + "," : grnMaster._companyDepartment.Address1 + ",";
                    lblCity.Text = grnMaster._companyDepartment.City != "" ? grnMaster._companyDepartment.City + "," : grnMaster._companyDepartment.City;
                    lblCountry.Text = grnMaster._companyDepartment.Country != "" ? grnMaster._companyDepartment.Country + "." : grnMaster._companyDepartment.Country;

                    gvPurchaseOrderItems.DataSource = gRNDetailsController.GrnDetialsGrnApproved(int.Parse(ViewState["GrnID"].ToString()), int.Parse(ViewState["PoID"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                    gvPurchaseOrderItems.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }

      

        //protected void btnView_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        //        int itemId = int.Parse(gvPurchaseOrderItems.Rows[x].Cells[0].Text);
        //        List<PR_FileUpload> pr_FileUpload = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
        //        gvUploadFiles.DataSource = pr_FileUpload;
        //        gvUploadFiles.DataBind();

        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal').modal('show'); });   </script>", false);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

       // public string getPRDetailData()
       // {
            //try
          //  {
                //string data = "";
                //List<PR_Details> pr_Details = pr_DetailController.FetchPR_DetailsByDeptIdAndPrId(int.Parse(PRId));
                //if (pr_Details.Count > 0)
                //{
                //    foreach (var item in pr_Details)
                //    {
                //        int itemId = item.ItemId;
                //        string Description = item.ItemDescription;
                //        string Purpose = item.Purpose;
                //        decimal Quantity = item.ItemQuantity;
                //        string Replacement = "No";
                //        if (item.Replacement == 1)
                //        {
                //            Replacement = "Yes";
                //        }
                //        data += "<tr><td>" + itemId + "</td><td>" + Description + "</td><td>" + "<input type='button' value='View Attachments' class='btn btn-info' />" + "</td><td>" + Purpose + "</td><td>" + Quantity + "</td><td>" + Replacement + "</td></tr>";
                //    }
                //}
                //return data;
           // }
          //  catch (Exception ex)
           // {
           //     throw ex;
           //}
            
       // }
        //-----------2 : Rejected 1: Approved
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                GrnMaster _grnMaster = new GrnMaster();
                _grnMaster = grnController.CheckGrnExistMasterByGrnID(int.Parse(ViewState["GrnID"].ToString()), int.Parse(ViewState["PoID"].ToString()));
                int grnApproveStatus = 0;

                if (_grnMaster.GrnId > 0)
                {
                    GrnMaster _grnMasterExisting = new GrnMaster();

                    int NewgrnId = grnController.GetMaxGrnCode(int.Parse(Session["CompanyId"].ToString())) + 1;
                    grnController.SaveGrnMasterDup(NewgrnId, int.Parse(ViewState["PoID"].ToString()), _grnMaster.CompanyId, _grnMaster.Supplierid, _grnMaster.CreatedDate, _grnMaster.TotalAmount, _grnMaster.CreatedBy, _grnMaster.CreatedDate, _grnMaster.GrnNote, _grnMaster.GrnId, _grnMaster.InvoiceNo);

                    foreach (GridViewRow gvr in this.gvPurchaseOrderItems.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("CheckBox1")).Checked == true)
                        {
                            grnApproveStatus = gRNDetailsController.UpdateApprovedGrnNewGrnId(int.Parse(gvr.Cells[1].Text), int.Parse(gvr.Cells[2].Text), int.Parse(gvr.Cells[4].Text), 1, int.Parse(Session["UserId"].ToString()), LocalTime.Now, "", NewgrnId);
                        }
                    }

                    string grncode = string.Empty;

                    if (NewgrnId == 0)
                    {
                        grncode = "GRN" + 1;
                    }
                    else
                    {
                        int grnid = NewgrnId - 1;
                        grncode = "GRN" + (grnid);
                    }

                    int grnApproveStatusGrnMaster = grnController.grnMasterApproval(NewgrnId, 1, Session["UserId"].ToString(), int.Parse(Session["CompanyId"].ToString()), grncode);
                }

                else
                {   
                    foreach (GridViewRow gvr in this.gvPurchaseOrderItems.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("CheckBox1")).Checked == true)
                        {
                            grnApproveStatus = gRNDetailsController.UpdateApprovedGrn(int.Parse(gvr.Cells[1].Text), int.Parse(gvr.Cells[2].Text), int.Parse(gvr.Cells[4].Text), 1, int.Parse(Session["UserId"].ToString()), LocalTime.Now, "");
                        }
                    }

                    int NewgrnId = grnController.GetMaxGrnCode(int.Parse(Session["CompanyId"].ToString()));
                    string grncode = string.Empty;

                    if (NewgrnId == 0)
                    {
                        grncode = "GRN" + 1;
                    }
                    else {
                        int grnid = NewgrnId;
                        grncode = "GRN" + (grnid + 1);
                    }
                    
                    int grnApproveStatusGrnMaster = grnController.grnMasterApproval(int.Parse(ViewState["GrnID"].ToString()), 1, Session["UserId"].ToString(), int.Parse(Session["CompanyId"].ToString()), grncode);

                }
                if (grnApproveStatus > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title:'SUCCESS',text: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'CustomerGRNView.aspx'}); });   </script>", false);

                    //Response.Redirect("CustomerGRNView.aspx", false);
                }
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                rejectedReason.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnRejectBtn_Click(object sender, EventArgs e)
        {
            int grnApproveStatus = 0;
            try
            {
                GrnMaster _grnMaster = new GrnMaster();
                _grnMaster = grnController.CheckGrnExistMasterByGrnID(int.Parse(ViewState["GrnID"].ToString()), int.Parse(ViewState["PoID"].ToString()));

                if (_grnMaster.GrnId > 0)
                {

                }

                else {
                    int grnid = _grnMaster.GrnId;

                    int grnRejectStatus = grnController.grnMasterApproval(int.Parse(ViewState["GrnID"].ToString()), 2, Session["UserId"].ToString(), int.Parse(Session["CompanyId"].ToString()), "");

                    foreach (GridViewRow gvr in this.gvPurchaseOrderItems.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("CheckBox1")).Checked == true)
                        {
                            grnApproveStatus = gRNDetailsController.UpdateApprovedGrn(int.Parse(gvr.Cells[1].Text), int.Parse(gvr.Cells[2].Text), int.Parse(gvr.Cells[4].Text), 2, int.Parse(Session["UserId"].ToString()), LocalTime.Now, txtRejectReason.Text);
                            gRNDetailsController.UpdateGrnReectIsPoRaised(int.Parse(gvr.Cells[1].Text), int.Parse(gvr.Cells[2].Text), int.Parse(gvr.Cells[4].Text), 2);
                        }

                        gRNDetailsController.UpdateGrnReectAddToGrnCount(int.Parse(gvr.Cells[1].Text), int.Parse(gvr.Cells[2].Text), int.Parse(gvr.Cells[4].Text),1);
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title:'SUCCESS',text: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'CustomerGRNView.aspx'}); });   </script>", false);
                //Response.Redirect("CustomerGRNView.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}