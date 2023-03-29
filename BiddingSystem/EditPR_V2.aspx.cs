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
    public partial class EditPR_V2 : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

            if (!IsPostBack) {
                if (Session["CompanyId"] != null) {
                    CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
                    CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 5, 2) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else {
                    Response.Redirect("LoginPage.aspx");
                }
            }
        }


        public string GetPr() {

            return JsonConvert.SerializeObject(ControllerFactory.CreatePrControllerV2().GetPrForEditing(int.Parse(HttpContext.Current.Request.QueryString.Get("PrId"))));
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

        //get all stock-pasindu-2020/04/21
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string getStock(string ddlItemName, string ddlWarehouse, string ddlMeasurement)
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

                    string txtStock = convertedValue.ToString("0.##########") + " " + ddlMeasurementUnit.ToString();

                    response.Status = 200;
                    response.Data = txtStock;

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
        public static string GetItems(int CategoryId, int SubCategoryId, int ItemType) {
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
        public static string Save(PrMasterV2 prMaster) {
            try {
                if (HttpContext.Current.Session["UserId"] != null) {

                    ResultVM response = new ResultVM();

                    prMaster.PrUpdateLog.PrId = prMaster.PrId;
                    prMaster.PrUpdateLog.UpdatedBy = int.Parse(HttpContext.Current.Session["UserId"].ToString());

                    int result = ControllerFactory.CreatePrControllerV2().UpdatePr(prMaster);
                    string StatusCode = "UPDTD";

                    if (result > 0) {

                        for (int i = 0; i < prMaster.PrDetails.Count; i++) {
                            ControllerFactory.CreatePRDetailsStatusLogController().InsertLog(prMaster.PrDetails[i].PrdId, int.Parse(HttpContext.Current.Session["UserId"].ToString()), StatusCode);
                        }


                        response.Status = 200;
                        response.Data = "Success";
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
    }
}