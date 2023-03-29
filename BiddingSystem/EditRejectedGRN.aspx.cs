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
    public partial class EditRejectedGRN : System.Web.UI.Page
    {
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        PODetailsController pODetailsController = ControllerFactory.CreatePODetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        GrnController grnController = ControllerFactory.CreateGrnController();
        GRNDetailsController gRNDetailsController = ControllerFactory.CreateGRNDetailsController();

       // static string UserId = string.Empty;
       // private string PRId = string.Empty;

       // private string UserDept = string.Empty;
       // private string OurRef = string.Empty;
       // private string PrCode = string.Empty;
       // private string RequestedDate = string.Empty;
        //private string UserRef = string.Empty;
       // private string RequesterName = string.Empty;

       // int CompanyId = 0;
      //  int GrnID = 0;
       // int PoID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
               // CompanyId = int.Parse(Session["CompanyId"].ToString());
              //  UserId = Session["UserId"].ToString();

                if (Session["GrnID"] != null)
                {
                  ViewState["GrnID"] = int.Parse(Session["GrnID"].ToString());
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

            lblDateNow.Text = LocalTime.Today.ToString("dd-MM-yyyy");


            if (!IsPostBack)
            {
                try
                {
                    GrnMaster grnMaster = grnController.GetGrnMasterByGrnID(int.Parse(ViewState["GrnID"].ToString()),int.Parse( ViewState["PoID"].ToString()));
                    CompanyDepartment companyDepartment = companyDepartmentController.GetDepartmentByDepartmentId(int.Parse(Session["CompanyId"].ToString()));

                    lblPOCode.Text = grnMaster._POMaster.POCode;
                    lblSupplierName.Text = grnMaster._Supplier.SupplierName; ;
                    lblAddress.Text = grnMaster._Supplier.Address1 + "," + grnMaster._Supplier.Address2;
                    
                    lblSubtotal.Text = grnMaster.TotalAmount.ToString("n");
                    lblVatTotal.Text = grnMaster._POMaster.VatAmount.ToString("n");
                    lblNbtTotal.Text = grnMaster._POMaster.NBTAmount.ToString("n");
                    lblTotal.Text = grnMaster._POMaster.TotalAmount.ToString("n");
              
                    lblCompanyName.Text = companyDepartment.DepartmentName;
                    lblVatNo.Text = companyDepartment.VatNo;
                    lblPhoneNo.Text = companyDepartment.PhoneNO;
                    lblFaxNo.Text = companyDepartment.FaxNO;
                    lblCompanyAddress.Text = companyDepartment.Address1;
                    lblCity.Text = companyDepartment.City;
                    lblCountry.Text = companyDepartment.Country + ".";
                    txtGRNote.Text = grnMaster.GrnNote;
                    hdnReceivedDate.Value = grnMaster.GoodReceivedDate.ToString();
                    GoodreceivedDate.Text = grnMaster.GoodReceivedDate.ToString("yyyy/MM/dd HH:mm");

                    gvPurchaseOrderItems.DataSource = grnMaster._POMaster._PODetails; 
                    gvPurchaseOrderItems.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

      

        //protected void btnUpdateGrn_Click1(object sender, EventArgs e)
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerViewApprovedPurchaseOrder.aspx");
        }

        //-----------2 : Rejected 1: Approved
        protected void btnUpdateGrn_Click(object sender, EventArgs e)
        {
            try
            {
                   int UpdateRejectedGrnStatus = grnController.UpdateRejectedGrn(int.Parse(ViewState["GrnID"].ToString()), int.Parse(ViewState["PoID"].ToString()), DateTime.Parse(GoodreceivedDate.Text), txtGRNote.Text);
                    if (UpdateRejectedGrnStatus > 0)
                    {
                        Response.Redirect("CustomerViewApprovedPurchaseOrder.aspx");
                    }
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }

        //------------get date 
        public string GetDateTime() {
            var DataList = GoodreceivedDate.Text;
            return(new JavaScriptSerializer()).Serialize(DataList);
        }
       
    }
}