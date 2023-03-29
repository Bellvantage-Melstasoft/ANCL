﻿using System;
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
    public partial class PrintDepartmentReturn : System.Web.UI.Page
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
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        InventoryControllerInterface inventoryControllerInterface = ControllerFactory.CreateInventoryController();
        DepartmentReturnController departmentReturnController = ControllerFactory.CreateDepartmentReturnController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefWarehouse";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabWarehouse";
                ((BiddingAdmin)Page.Master).subTabValue = "ReturnStockToWreouse.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ReturnStockToWreouseLink";

               ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                ViewState["UserId"] = Session["UserId"].ToString();

                

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 10, 6) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                
                gvDepartmetStock.DataSource = departmentReturnController.FetchApprovedStock();
                gvDepartmetStock.DataBind();

            }
        }
       
        

    }
}