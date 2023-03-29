using BiddingSystem.Helpers;
using BiddingSystem.ViewModels.CS;
using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace BiddingSystem {
    public partial class CreateMRN_V2 : System.Web.UI.Page {
        
        protected void Page_Load(object sender, EventArgs e) {

            if (!IsPostBack) {
                if (Session["CompanyId"] != null) {
                    CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
                    CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                    ((BiddingAdmin)Page.Master).subTabValue = "CreateMRN_V2.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "createMRNLink";

                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 12, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else {
                    Response.Redirect("LoginPage.aspx");
                }
            }
        }

        private static string SessionExpired() {
            return
                JsonConvert.SerializeObject(
                new ResultVM() {
                    Status = 401,
                    Data = "Session Expired"
                });
        }

        private static string ServerError() {
            return
                JsonConvert.SerializeObject(
                new ResultVM() {
                    Status = 500,
                    Data = "Server Error Occured"
                });
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetDepartments() {
            try {
                if (HttpContext.Current.Session["UserId"] != null) {
                    if (HttpContext.Current.Session["UserDepartments"] != null && (HttpContext.Current.Session["UserDepartments"] as List<UserSubDepartment>).Count > 0) {
                        return
                            JsonConvert.SerializeObject(
                            new {
                                Status = 200,
                                Data = HttpContext.Current.Session["UserDepartments"] as List<UserSubDepartment>
                            });
                    }
                    else {
                        return
                            JsonConvert.SerializeObject(
                            new {
                                Status = 200,
                                Data = ControllerFactory.CreateSubDepartmentController().getDepartmentList(int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Select(sd => new {
                                    DepartmentId = sd.SubDepartmentID,
                                    DepartmentName = sd.SubDepartmentName,
                                    IsHead = 0
                                })
                            });
                    }
                }
                else {
                    return SessionExpired();
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return ServerError();
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetWarehouses() {
            try {
                if (HttpContext.Current.Session["UserId"] != null) {
                    return
                        JsonConvert.SerializeObject(
                        new {
                            Status = 200,
                            Data = ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Select(w => new {
                                w.WarehouseID,
                                WarehouseName = w.Location
                            })
                        });
                }
                else {
                    return SessionExpired();
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return ServerError();
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetCategories() {
            try {
                if (HttpContext.Current.Session["UserId"] != null) {
                    return
                        JsonConvert.SerializeObject(
                        new {
                            Status = 200,
                            Data = ControllerFactory.CreateItemCategoryController().FetchItemCategoryList(int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Select(obj => new {
                                obj.CategoryId,
                                obj.CategoryName
                            })
                        });
                }
                else {
                    return SessionExpired();
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return ServerError();
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetSubCategories(int CategoryId) {
            try {
                if (HttpContext.Current.Session["UserId"] != null) {
                    return
                        JsonConvert.SerializeObject(
                        new {
                            Status = 200,
                            Data = ControllerFactory.CreateItemSubCategoryController().FetchItemSubCategoryByCategoryId(CategoryId, int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Select(obj => new {
                                obj.SubCategoryId,
                                obj.SubCategoryName
                            })
                        });
                }
                else {
                    return SessionExpired();
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return ServerError();
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetItemDescription(int ItemId)
        {
           
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    return
                        JsonConvert.SerializeObject(
                        new
                        {
                            Status = 200,
                            Data = ControllerFactory.CreateAddItemBOMController().GetBOMListByItemId( int.Parse(HttpContext.Current.Session["CompanyId"].ToString()), ItemId).Select(obj => new {
                                obj.Material,
                                obj.Description
                                
                            })
                        });
                }
                else
                {
                    return SessionExpired();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetItems(int CategoryId, int SubCategoryId, int ItemType)
        {
            try {
                if (HttpContext.Current.Session["UserId"] != null) {
                    return
                        JsonConvert.SerializeObject(
                        new {
                            Status = 200,
                            Data = ControllerFactory.CreateAddItemController().GetItemsForMrnAndPr(CategoryId, SubCategoryId, ItemType, int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Select(obj => new {
                                obj.ItemId,
                                obj.ItemName,
                                obj.MeasurementShortName
                            })
                        });
                }
                else {
                    return SessionExpired();
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return ServerError();
            }
        }

        [WebMethod]
        public static string Save(MrnMasterV2 mrnMaster) {
            try {
                if (HttpContext.Current.Session["UserId"] != null) {

                    ResultVM response = new ResultVM();

                    mrnMaster.CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                    mrnMaster.CreatedBy = int.Parse(HttpContext.Current.Session["UserId"].ToString());

                    List<UserWarehouse> List = ControllerFactory.CreateUserWarehouse().GetWarehouseKeeperForMRN(mrnMaster.WarehouseId, mrnMaster.MrnSubCategoryId);
                    UserWarehouse SK = List.FirstOrDefault(t => t.SubCategoryId != 0);
                    if (SK != null) {
                        mrnMaster.StoreKeeperId = SK.UserId;
                    }

                    int result = ControllerFactory.CreateMrnControllerV2().SaveMrn(mrnMaster);

                    if (result > 0) {
                        response.Status = 200;
                        if (mrnMaster.PurchaseType == 1) {
                            response.Data = "MRN-LCL" + result.ToString();
                        }
                        else {
                            response.Data = "MRN-IMP" + result.ToString();
                        }
                    }
                    else {
                        response.Status = 500;
                        response.Data = "Error";
                    }
                    return JsonConvert.SerializeObject(response);
                }
                else {
                    return SessionExpired();
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return ServerError();
            }
        }
        //get all measuremnts-pasindu-2020/04/21
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string LoadMeasurement(int itemId)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    return
                        JsonConvert.SerializeObject(
                        new
                        {
                            Status = 200,
                            Data = ControllerFactory.CreateMeasurementDetailController().GetMeasurementDetailsOfItem(itemId, int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Select(obj => new {
                                obj.DetailId,
                                obj.ShortCode
                            })
                        });
                    
                    //ddlMeasurement.DataSource = ControllerFactory.CreateMeasurementDetailController().GetMeasurementDetailsOfItem(int.Parse(ddlItem.Value.ToString()), int.Parse(Session["CompanyId"].ToString()));
                    //ddlMeasurement.DataValueField = "DetailId";
                    //ddlMeasurement.DataTextField = "ShortCode";
                    //ddlMeasurement.DataBind();
                }else
                {
                    return SessionExpired();
                }
               

            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }

        //get all stock-pasindu-2020/04/21
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string getStock(string ddlItemName,string ddlWarehouse, string ddlMeasurement)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    ResultVM response = new ResultVM();

                    MeasurementDetail stockMaintainingMeasurement = ControllerFactory.CreateMeasurementDetailController().GetStockMaintainingMeasurement(int.Parse(ddlItemName.ToString()), int.Parse(HttpContext.Current.Session["CompanyId"].ToString()));

                    HttpContext.Current.Session["StockMaintainingUOM"] = serializer.Serialize(stockMaintainingMeasurement);
                    ddlMeasurement = stockMaintainingMeasurement.DetailId.ToString();
                    string txtStock;
                    if (ddlWarehouse != null && ddlWarehouse != "")
                    {
                         txtStock = ControllerFactory.CreateInventoryController()
                            .GetWarehouseInventoryForItem(int.Parse(ddlWarehouse.ToString()), int.Parse(ddlItemName.ToString())).ToString("0.##########") + " " + stockMaintainingMeasurement.ShortCode;
                    }
                    else
                    {
                         txtStock = "";
                    }
                    response.Status = 200;
                    response.Data = txtStock;
                    return JsonConvert.SerializeObject(response);
                    //return
                    //    JsonConvert.SerializeObject(
                    //    new
                    //    {
                    //        Status = 200,
                    //        Data = txtStock
                    //    });

                }
                else
                {
                    return SessionExpired();
                }


            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }

        //unit conversion-pasindu-2020/04/21
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string UnitConversion(string ddlItemName, string ddlWarehouse, string ddlMeasurement, string ddlMeasurementUnit)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    ResultVM response = new ResultVM();

                    getStock(ddlItemName, ddlWarehouse, ddlMeasurement);
                    MeasurementDetail stockMaintainingUOM = serializer.Deserialize<MeasurementDetail>(HttpContext.Current.Session["StockMaintainingUOM"].ToString());
                    decimal availableStock = ControllerFactory.CreateInventoryController()
                        .GetWarehouseInventoryForItem(int.Parse(ddlWarehouse), int.Parse(ddlItemName));

                    decimal convertedValue = ControllerFactory.CreateConversionController().DoConversion(
                        int.Parse(ddlItemName),
                        int.Parse(HttpContext.Current.Session["CompanyId"].ToString()),
                        availableStock,
                        stockMaintainingUOM.DetailId,
                        int.Parse(ddlMeasurement));

                    //string txtStock = convertedValue.ToString("0.##########") + " " + ddlMeasurementUnit.ToString();
                    string txtStock = convertedValue.ToString("0.##########");

                    response.Status = 200;
                    response.Data = txtStock;

                    return JsonConvert.SerializeObject(response);
                    //return
                    //    JsonConvert.SerializeObject(
                    //    new
                    //    {
                    //        Status = 200,
                    //        Data= txtStock

                           
                    //    });

                    //if (ddlItemName.SelectedValue != null && ddlItemName.SelectedValue != "" && ddlWarehouse.SelectedValue != null && ddlWarehouse.SelectedValue != "")
                    //{
                    //    MeasurementDetail stockMaintainingUOM = serializer.Deserialize<MeasurementDetail>(ViewState["StockMaintainingUOM"].ToString());
                    //    decimal availableStock = ControllerFactory.CreateInventoryController()
                    //        .GetWarehouseInventoryForItem(int.Parse(ddlWarehouse.SelectedValue.ToString()), int.Parse(ddlItemName.SelectedValue.ToString()));

                    //    decimal convertedValue = ControllerFactory.CreateConversionController().DoConversion(
                    //        int.Parse(ddlItemName.SelectedValue),
                    //        int.Parse(ViewState["CompanyId"].ToString()),
                    //        availableStock,
                    //        stockMaintainingUOM.DetailId,
                    //        int.Parse(ddlMeasurement.SelectedValue));

                    //    txtStock.Text = convertedValue.ToString("0.##########") + " " + ddlMeasurement.SelectedItem.ToString();
                    //}
                    //else
                    //{
                    //    txtStock.Text = "";
                    //}

                    //ddlMeasurement.DataSource = ControllerFactory.CreateMeasurementDetailController().GetMeasurementDetailsOfItem(int.Parse(ddlItem.Value.ToString()), int.Parse(Session["CompanyId"].ToString()));
                    //ddlMeasurement.DataValueField = "DetailId";
                    //ddlMeasurement.DataTextField = "ShortCode";
                    //ddlMeasurement.DataBind();
                }
                else
                {
                    return SessionExpired();
                }


            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }
        [WebMethod]
        public static string DeleteItemSpecClick(BOMDetails mrnBomDetails)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {

                    ResultVM response = new ResultVM();

                    AddItemBOM addItemBOM = new AddItemBOM();
                    addItemBOM.Material = mrnBomDetails.Material;
                    addItemBOM.Description = mrnBomDetails.Description;
                    addItemBOM.itemId = Convert.ToInt32(mrnBomDetails.ItemId);

                    addItemBOM.companyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());

                    int result = ControllerFactory.CreateAddItemBOMController().DeleteBOMByItemDet(addItemBOM);

                    if (result > 0)
                    {
                        response.Status = 200;
                        response.Data = result.ToString();
                    }
                    else
                    {
                        response.Status = 500;
                        response.Data = "Error";
                    }
                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    return SessionExpired();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }
    }
}