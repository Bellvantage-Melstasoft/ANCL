<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="EditPR_V2.aspx.cs" Inherits="BiddingSystem.EditPR_V2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <link href="AdminResources/css/Wizard.css?version=<%DateTime.Now.ToString(); %>" rel="stylesheet" />
    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />


    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/select2.full.min.js"></script>
    <%--<script src="https://cdn.ckeditor.com/4.11.4/basic/ckeditor.js"></script>--%>
    <script src="AdminResources/js/ckeditor.js"></script>
   <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>


    <div class="modal fade" id="mdlItemSpec" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">ITEM SPECIFICATIONS</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>SPECIFICATION</label><span class="required"> *</span>
                                        <input id="txtSpec" type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>DESCRIPTION</label><span class="required"> *</span>
                                        <input id="txtSpecDescription" type="text" class="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="pull-right btn btn-info btn-styled" onclick="addSpec();">Add</button>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead style="background-color: #00c0ef; color: white; font-weight: bold;">
                                                <tr>
                                                    <th>Specification</th>
                                                    <th>Description</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody style="background-color: #f3f3f3" id="tbSpecs">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>


    <div class="modal fade" id="mdlItemSpecFromList" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">ITEM SPECIFICATIONS</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead style="background-color: #00c0ef; color: white; font-weight: bold;">
                                                <tr>
                                                    <th>Specification</th>
                                                    <th>Description</th>
                                                </tr>
                                            </thead>
                                            <tbody style="background-color: #f3f3f3" id="tbSpecsFromList">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>


    <div class="modal fade" id="mdlReplacementImages" role="dialog">
        <div class="modal-dialog" style="width: 63%;">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">REPLACEMENT IMAGES</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12" id="dvReplacementImages">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="mdlFileUpload" role="dialog">
        <div class="modal-dialog" style="width: 63%;">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">STANDARD IMAGES</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12" id="dvStandardImages">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="mdlSupportiveDocs" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">SUPPORTIVE DOCUMENTS</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table class="table table-hover">
                                            <thead style="background-color: #00c0ef; color: white; font-weight: bold;">
                                                <tr>
                                                    <th>File Name</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody style="background-color: #f3f3f3" id="tbSupportiveDocs">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="mdlCapexDocs" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">CAPITAL EXPENSE DOCUMENTS</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table class="table table-hover">
                                            <thead style="background-color: #00c0ef; color: white; font-weight: bold;">
                                                <tr>
                                                    <th>File Name</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody style="background-color: #f3f3f3" id="tbCapexDocs">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <section class="content-header">
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Purchase Request</li>
      </ol>
    </section>
    <br />

    <section class="content">
        <div class="container-fluid">
            <div class="panel wizard">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <h4>
                                <b>EDIT</b> PURCHASE REQUEST NOTE <br>
                                <hr>
                            </h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="process">
                                <div class="process-row nav nav-tabs">
                                    <div class="process-step">
                                        <button type="button" id="btnBasic" class="btn btn-info btn-circle" data-toggle="tab"
                                            href="#basicPane" onclick="window.scrollTo({ top: 0, behavior: 'smooth' });"><i class="fa fa-info fa-3x"></i></button>
                                        <p><b>BASIC</b></p>
                                    </div>
                                    <div class="process-step">
                                        <button type="button" id="btnItem"  class="btn btn-default btn-circle" data-toggle="tab"
                                            href="#itemPane" onclick="window.scrollTo({ top: 0, behavior: 'smooth' });"><i class="fa fa-list-ul fa-3x"></i></button>
                                        <p><b>ITEMS</b></p>
                                    </div>
                                    <div class="process-step">
                                        <button type="button" id="btnExpense"  class="btn btn-default btn-circle" data-toggle="tab"
                                            href="#expensePane" onclick="window.scrollTo({ top: 0, behavior: 'smooth' });"><i class="fas fa-wallet fa-3x"></i></button>
                                        <p><b>BUDGET</b></p>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-content">
                                <div id="basicPane" class="tab-pane fade active in">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group refreshable">
                                                        <label>WAREHOUSE</label>
                                                        <label class="label label-info pull-right lbl-refresh" onclick="loadWarehouse();">Refresh</label>
                                                        <label id="warehouseLoad"  class="label label-info pull-right hidden"><i class="fa fa-spinner fa-spin"></i></label>
                                                        <span class="required basicRequired">
                                                            *</span>
                                                        <select id="ddlWarehouse" class="form-control select2" onchange="updateStock();">
                                                        </select>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>PR TYPE</label><span class="required basicRequired">
                                                            *</span>
                                                        <%--<select id="ddlPRType" class="form-control select2" onchange="loadItem();">--%>
                                                        <select id="ddlPRType" class="form-control select2" onchange="PrTypeOnchange();">
                                                            <option value="1" selected>Stock</option>
                                                            <option value="2">Non-Stock</option>
                                                        </select>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>REQUIRED DATE</label><span
                                                            class="required basicRequired"> *</span>
                                                        <input id="txtRequiredDate" class="form-control date1" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>PURCHASING TYPE</label><span
                                                            class="required basicRequired"> *</span>
                                                        <select id="ddlPurchasingType" class="form-control select2" onchange="PurchaseTypeOnchange();">
                                                            <option value="1" selected>Local</option>
                                                            <option value="2">Import</option>
                                                        </select>
                                                    </div>

                                                    <div class="hidden" id="dvImportItem">
                                                    <div class="form-group">
                                                        <label>IMPORT ITEM TYPE</label><span
                                                            class="required basicRequired"> *</span>
                                                        <select id="ddlItemType" class="form-control select2" onchange="ImportItemTypeOnchange();">
                                                            <option value="0">Select Value</option>
                                                            <option value="1">Spare Parts</option>
                                                            <option value="2">Material</option>
                                                        </select>
                                                    </div>
                                                        </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>PURCHASING PROCEDURE</label><span
                                                                    class="required basicRequired"> *</span>

                                                                <div class="input-group">
                                                                    <span class="input-group-addon">
                                                                        <input type="radio" id="rdoProcedureNormal"
                                                                            name="procedure" checked />
                                                                    </span>
                                                                    <input type="text" class="form-control" value="Normal" disabled />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label style="visibility:hidden;">PURCHASING PROCEDURE</label>
                                                                <div class="input-group">
                                                                    <span class="input-group-addon">
                                                                        <input type="radio" id="rdoProcedureCovering"
                                                                            name="procedure" />
                                                                    </span>
                                                                    <input type="text" class="form-control" value="Covering" disabled />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>EXPENSE TYPE</label><span
                                                                    class="required expenseRequired"> *</span>

                                                                <div class="input-group">
                                                                    <span class="input-group-addon">
                                                                        <input type="radio" id="rdoCapex"
                                                                            name="expenseType" checked />
                                                                    </span>
                                                                    <input type="text" class="form-control" value="Capital Expense" disabled />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label style="visibility:hidden;">EXPENSE TYPE</label>
                                                                <div class="input-group">
                                                                    <span class="input-group-addon">
                                                                        <input type="radio" id="rdoOpex"
                                                                            name="expenseType" />
                                                                    </span>
                                                                    <input type="text" class="form-control" value="Operational Expense" disabled />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row hidden" id="dvCapexDoc">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>CAPITAL EXPENSE DOCUMENTS</label><span id="spnCapexDocsCount"
                                                            style="color:#00a65a; font-weight:bold;"> 0</span>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 col-md-push-6">
                                                            <div class="form-group">
                                                                <input id="flCapexDoc" type="file" multiple class="hidden" onchange="capexDocUploaded(this);" />
                                                                <button class="btn btn-success btn-styled" onclick=" $('#flCapexDoc').click();" >Upload</button>
                                                                <button class="btn btn-info btn-styled" onclick="viewCapexDocs();">View</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>REQUIRED FOR</label>
                                                        <textarea id="txtRequiredFor" class="form-control" rows="5" cols="300"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                
                                                <div class="col-md-6">
                                                    <div class="form-group refreshable">
                                                        <label>PR CATEGORY</label>
                                                        <label class="label label-info pull-right lbl-refresh" onclick="loadCategory();">Refresh</label>
                                                        <label id="categoryLoad"  class="label label-info pull-right hidden"><i class="fa fa-spinner fa-spin"></i></label>
                                                        <label id="categoryNotAllowed"  class="label label-danger pull-right hidden">Not Allowed</label>
                                                        <span
                                                            class="required basicRequired"> *</span>
                                                        <select id="ddlPRCategory" class="form-control select2" onchange="loadSubCategory();">
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group refreshable">
                                                        <label>PR SUBCATEGORY</label>
                                                        <label class="label label-info pull-right lbl-refresh" onclick="loadSubCategory();">Refresh</label>
                                                        <label id="subCategoryLoad"  class="label label-info pull-right hidden"><i class="fa fa-spinner fa-spin"></i></label>
                                                        <label id="subCategoryNotAllowed"  class="label label-danger pull-right hidden">Not Allowed</label>
                                                        <span
                                                            class="required basicRequired"> *</span>
                                                        <select id="ddlPRSubCategory" class="form-control select2" onchange="loadItem();">
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-footer">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <button type="button"
                                                        class="btn btn-md btn-info pull-right next-step">Next&nbsp;<i
                                                            class="fa fa-chevron-right"></i></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="itemPane" class="tab-pane fade">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group refreshable">
                                                        <label>ITEM NAME</label>
                                                        <label class="label label-info pull-right lbl-refresh" onclick="loadItem();">Refresh</label>
                                                        <label id="itemLoad"  class="label label-info pull-right hidden" style="margin-left:5px;"><i class="fa fa-spinner fa-spin"></i></label>
                                                        <label class="label label-success pull-right lbl-refresh" onclick="loadPurchaseHistory();">Purchase History</label>
                                                        <label id="itemNotAllowed"  class="label label-danger pull-right hidden">Not Allowed</label>
                                                        <span
                                                            class="required itemRequired"> *</span>
                                                        <select id="ddlItem" class="form-control select2" onchange="ItemClick();">
                                                        </select>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>ITEM DESCRIPTION</label>
                                                        <textarea id="txtDescription" class="form-control" rows="6" cols="300"></textarea>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-5">
                                                            <div class="form-group">
                                                                <label>ESTIMATED UNIT PRICE</label><span
                                                            class="required itemRequired"> *</span>
                                                                <input id="txtUnitPrice" type="number" class="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4" style="padding-right:2px;">
                                                            <div class="form-group">
                                                                <label>QUANTITY</label><span
                                                            class="required itemRequired"> *</span>
                                                                <input id="txtQty" type="number" class="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3" style="padding-left:2px;">
                                                            <div class="form-group">
                                                                <label>MEASUREMENT</label>
                                                                <%--<input id="txtMeasurement" type="text" class="form-control" disabled/>--%>
                                                                 <select id="ddlMeasurement" class="form-control select2"  onchange="changeAvaStock();"></select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>WAREHOUSE STOCK</label><span
                                                    class="required itemRequired"> *</span>
                                                        <input id="txtWarehouseStock" type="text" class="form-control" disabled/>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>ITEM SPECIFICATIONS</label><span id="spnSpecCount"
                                                            style="color:#00a65a; font-weight:bold;"> 0</span>
                                                        <button id="btnSpec" class="btn btn-success form-control" style="border: 2px; border-radius:20px; font-weight:bold;" onclick="viewItemSpec();">Click To Add</button>
                                                    </div>
                                                    <div  id="dvSparepart">
                                                     <div class="form-group">
                                                          
                                                        <label>SPARE PART ID</label><span id="spnPartId"
                                                            style="color:#00a65a; font-weight:bold;"></span>
                                                              <%-- <input type="text" id="txtSparePart"  class="form-control"/>--%>
                                                         <textarea id="txtSparePart" class="form-control" rows="1" ></textarea>
                                                             </div>
                                                           </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>FILE/SAMPLE PROVIDED</label><span
                                                                    class="required itemRequired"> *</span>

                                                                <div class="input-group">
                                                                    <span class="input-group-addon">
                                                                        <input type="radio" id="rdoFileSampleYes"
                                                                            name="fileSample" checked />
                                                                    </span>
                                                                    <input type="text" class="form-control" value="Yes" disabled />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label style="visibility:hidden;">FILE/SAMPLE PROVIDED</label>
                                                                <div class="input-group">
                                                                    <span class="input-group-addon">&nbsp;
                                                                        <input type="radio" id="rdoFileSampleNo"
                                                                            name="fileSample" />
                                                                    </span>
                                                                    <input type="text" class="form-control" value="No" disabled />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>REPLACEMENT</label><span
                                                                    class="required itemRequired"> *</span>

                                                                <div class="input-group">
                                                                    <span class="input-group-addon">
                                                                        <input type="radio" id="rdoReplacementYes"
                                                                            name="replacement" checked />
                                                                    </span>
                                                                    <input type="text" class="form-control" value="Yes" disabled />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label style="visibility:hidden;">PURCHASING PROCEDURE</label>
                                                                <div class="input-group">
                                                                    <span class="input-group-addon">
                                                                        <input type="radio" id="rdoReplacementNo"
                                                                            name="replacement" />
                                                                    </span>
                                                                    <input type="text" class="form-control" value="No" disabled />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>REPLACEMENT IMAGES</label><span id="spnReplacementImgCount"
                                                            style="color:#00a65a; font-weight:bold;"> 0</span>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 col-md-push-6">
                                                            <div class="form-group">
                                                                <input id="flReplacementImages" type="file" multiple accept="image/*" class="hidden" onchange="replacementFileUploaded(this);" />
                                                                <button class="btn btn-success btn-styled" onclick=" $('#flReplacementImages').click();" >Upload</button>
                                                                <button class="btn btn-info btn-styled" onclick="viewReplacementImages();">View</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>STANDARD IMAGES</label><span id="spnStandardImgCount"
                                                            style="color:#00a65a; font-weight:bold;"> 0</span>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 col-md-push-6">
                                                            <div class="form-group">
                                                                <input id="flFileUpload" type="file" multiple accept="image/*" class="hidden" onchange="standardImageUploaded(this);" />
                                                                <button class="btn btn-success btn-styled" onclick=" $('#flFileUpload').click();" >Upload</button>
                                                                <button class="btn btn-info btn-styled" onclick="viewStandardImages();">View</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>SUPPORTIVE DOCUMENTS</label><span id="spnSupportiveDocCount"
                                                            style="color:#00a65a; font-weight:bold;"> 0</span>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6 col-md-push-6">
                                                            <div class="form-group">
                                                                <input id="flSupportiveDocs" type="file" multiple class="hidden" onchange="supportiveDocsUploaded(this);"  />
                                                                <button class="btn btn-success btn-styled" onclick=" $('#flSupportiveDocs').click();" >Upload</button>
                                                                <button class="btn btn-info btn-styled" onclick="viewsupportiveDocs();">View</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>REMARKS</label>
                                                        <textarea id="txtRemarks" class="form-control" rows="5" cols="300"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                    <button type="button"
                                                        class="btn btn-md btn-danger pull-right btn-styled" onclick="clearItemFields();">Clear</button>
                                                    <button type="button" style="margin-right:5px;" id="btnAddItem"
                                                        class="btn btn-success pull-right btn-styled" onclick="addItem();">Add To List</button>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h4><b>ADDED ITEMS LIST</b></h4>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="table-responsive">
                                                                        <table class="table table-hover">
                                                                            <thead style="background-color:#00c0ef; color:white; font-weight:bold;">
                                                                                <tr>
                                                                                    <th>Item Name</th>
                                                                                    <th>Quantity</th>
                                                                                    <th>Measurement</th>
                                                                                    <th>Est. Unit Price</th>
                                                                                    <th>Description</th>
                                                                                    <th>Other Details</th>
                                                                                    <th>More Info</th>
                                                                                    <th>Actions</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody id="tbAddedItems">
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </div>
                                        <div class="panel-footer">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <button type="button"
                                                        class="btn btn-md btn-info pull-right next-step">Next&nbsp;<i
                                                            class="fa fa-chevron-right"></i></button>
                                                    <button type="button"
                                                        class="btn btn-warning pull-right prev-step"><i
                                                            class="fa fa-chevron-left"></i>&nbsp;Back</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="expensePane" class="tab-pane fade">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12 text-right">
                                                    <label>TOTAL ESTIMATED AMOUNT</label>
                                                    <h2 id="totalEstAmount" class="text-red text-bold" style="margin-top:5px;">0.00</h2>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>BUDGET</label><span id="spnBudgetRequired"
                                                                    class="required expenseRequired"> *</span>

                                                                <div class="input-group">
                                                                    <span class="input-group-addon">
                                                                        <input type="radio" id="rdoBudgetYes"
                                                                            name="budget" checked />
                                                                    </span>
                                                                    <input type="text" class="form-control" value="Yes" disabled />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label style="visibility:hidden;">BUDGET</label>
                                                                <div class="input-group">
                                                                    <span class="input-group-addon">
                                                                        <input type="radio" id="rdoBudgetNo"
                                                                            name="budget" />
                                                                    </span>
                                                                    <input type="text" class="form-control" value="No" disabled />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>BUDGET AMOUNT</label><span
                                                                    class="required budgetRequired"> *</span>
                                                        <input type="number" id="txtBudgetAmount" class="form-control" />
                                                    </div>
                                                    <div class="form-group">
                                                        <label>BUDGET INFORMATION</label><span
                                                                    class="required budgetRequired"> *</span>
                                                        <textarea id="txtBudgetInformation" class="form-control" rows="3" cols="300"></textarea>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>REMARKS</label><span
                                                                    class="required expenseRemarksRequired"> *</span>
                                                        <textarea id="txtExpenseRemarks" class="form-control" rows="10" cols="300"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-footer">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label  id="iWait"  class="text-green pull-right hidden" style="padding:5px;"><i class="fa fa-spinner fa-spin"></i> Please Wait</label>
                                                    <button id="btnSave" type="button" class="btn btn-success pull-right btn-done" onclick="save();">
                                                        <i id="iDefault" class="fa fa-check"></i>&nbsp;
                                                        Save</button>
                                                    <button type="button"
                                                        class="btn btn-warning pull-right prev-step" >
                                                        <i class="fa fa-chevron-left"></i>&nbsp;Back</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div id="base64ReplacementImageStorage" class="hidden"></div>
        <div id="base64StandardImageStorage" class="hidden"></div>
        <div id="base64SupportiveDocsStorage" class="hidden"></div>
        <div id="base64CapexDocsStorage" class="hidden"></div>
    </section>
    
    <script src="Scripts/LoginService.js?v=<%DateTime.Now.ToString();%>"></script>
    <script src="ViewModels/JS/PrViewModel.js?v=<%DateTime.Now.ToString();%>"></script>
    <script>
        var prMaster = <%= GetPr() %>;
    </script>
    <script src="Scripts/EditPr.js?v=<%DateTime.Now.ToString();%>"></script>
</asp:Content>
