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
    public partial class WarehouseIssuNote : System.Web.UI.Page
    {
       // int PRId =0;
       // static string UserId = string.Empty;
       // int CompanyId = 0;
        //static int warehouseID = 0;
       // public List<string> PrBomDetails = new List<string>();
       // static int MrndsnID = 0;
       // public string reprint;

        MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        MRNDIssueNoteControllerInterface mrndinController = ControllerFactory.CreateMRNDIssueNoteController();
        InventoryControllerInterface inventoryController = ControllerFactory.CreateInventoryController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
               // CompanyId = int.Parse(Session["CompanyId"].ToString());
               // UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                //if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 10, 3) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                //{
                //    Response.Redirect("AdminDashboard.aspx");
                    
                //}
               
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
                    //if (Session["WarehouseID"] != null && Session["WarehouseID"].ToString() != "0")
                    if (Session["UserWarehouses"] != null ) 

                        {
                        //int warehouseID = int.Parse(Session["WarehouseID"].ToString());
                        //ViewState["warehouseID"] = new JavaScriptSerializer().Serialize(warehouseID);
                        try
                        {
                            if (Request.QueryString["MrnissueId"] != null)
                            {
                               int MrndsnID = int.Parse(Request.QueryString["MrnissueId"].ToString());
                                ViewState["MrndsnID"] = new JavaScriptSerializer().Serialize(MrndsnID);
                            }
                            if(Request.QueryString["TYPE"] !=null)
                            {
                                string reprint;
                                if (Request.QueryString["TYPE"].ToString()=="R")
                                {
                                    reprint = "(REPRINT)";
                                }
                                else
                                {
                                    reprint = "";
                                }
                               
                            }

                            gvReceivedInventory.DataSource = mrndinController.FetchforIssueNote((Session["UserWarehouses"] as List<UserWarehouse>).Select(d => d.WrehouseId).ToList(), int.Parse(ViewState["MrndsnID"].ToString()));
                            gvReceivedInventory.DataBind();

                            lblMrnCode.Text = "MRN-" + mrndinController.FetchMRNREFNo(int.Parse(ViewState["MrndsnID"].ToString())).ToString();
                            lblDeptName.Text = companyDepartmentController.GetDepartmentByDepartmentId(int.Parse(Session["CompanyId"].ToString())).DepartmentName;
                           

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You must be a Warehouse Head or Store Keeper to view this page', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                    }
                    //Response.Redirect("AdminDashboard.aspx");
                  

                  
                  
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

        

    }
}