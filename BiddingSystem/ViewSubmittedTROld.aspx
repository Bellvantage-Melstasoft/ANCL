<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewSubmittedTROld.aspx.cs" Inherits="BiddingSystem.ViewSubmittedTROld" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <script src="AdminResources/js/jquery1.8.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>

    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />

    <style type="text/css">
        .ChildGrid td {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
        }

        .ChildGrid th {
            color: White;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #67778e !important;
            color: white;
        }
    </style>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <section class="content-header">
    <h1>
      View Submitted Transfer Request Notes 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Transfer Request Notes </li>
      </ol>
    </section>
    <br />


    <form runat="server">




        <asp:ScriptManager runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>

                <div id="mdlIssueAvg" class="modal fade">
                    <div class="modal-dialog" style="width: 60%">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;"></h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="panel panel-default text-center">
                                            <b>ITEM DETAIL</b><br />
                                            <p id="mdlAvgItem" class="text-uppercase"></p>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="panel panel-default text-center">
                                            <b>WAREHOUSE DETAIL</b><br />
                                            <p id="mdlAvgWarehouse" class="text-uppercase "></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-md-push-3 text-center">
                                        <div class="form-group">
                                            <label>SELECT MEASUREMENT</label><br />
                                            <select id="ddlAvgMeasurement" class="form-control select2" style="width: 100%;" onchange="measurementChanged(this);"></select>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>REQUESTED QTY</b><br />
                                                <p id="mdlAvgRequested">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>ISSUED QTY</b>
                                                <p id="mdlAvgIssued">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>PENDING QTY</b>
                                                <p id="mdlAvgPending">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>AVAILABLE QTY </b>
                                                <p id="mdlAvgAvailable">20</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6 col-md-push-3 text-center">
                                        <div class="form-group">
                                            <label>ISSUING QTY</label>
                                            <input id="mdlAvgIssuingQty" type="number" class="form-control" min="0" step="0.00001" onkeyup="checkAvailableQty()" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button id="btnMdlAvgSubmit" class="btn btn-primary pull-right" style="margin-right: 10px" onclick="issueInventory(event);">SUBMIT</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="mdlIssueBatch" class="modal fade">
                    <div class="modal-dialog" style="width: 60%">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">ISSUE INVENTORY</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="panel panel-default text-center">
                                            <b>ITEM DETAIL</b><br />
                                            <p id="mdlBatchItem" class="text-uppercase"></p>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="panel panel-default text-center">
                                            <b>WAREHOUSE DETAIL</b><br />
                                            <p id="mdlBatchWarehouse" class="text-uppercase "></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-md-push-3 text-center">
                                        <div class="form-group">
                                            <label>SELECT MEASUREMENT</label><br />
                                            <select id="ddlBatchMeasurement" class="form-control select2" style="width: 100%;" onchange="measurementChanged(this);"></select>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>REQUESTED QTY</b><br />
                                                <p id="mdlBatchRequested">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>ISSUED QTY</b>
                                                <p id="mdlBatchIssued">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>PENDING QTY</b>
                                                <p id="mdlBatchPending">0</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <b>AVAILABLE QTY </b>
                                                <p id="mdlBatchAvailable">0</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12 text-center">
                                                <b>AVAILABLE BATCHES</b>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <span id="spnBatchOrder" style="color: #808080"></span>
                                                <br />
                                                <div class="table-responsive">
                                                    <table id="tblBatches" class="table">
                                                        <thead style="background-color: #3C8DBC; color: white; font-weight: bold;">
                                                            <tr>
                                                                <td class="hidden">BATCH ID</td>
                                                                <td>BATCH CODE</td>
                                                                <td>EXPIRY DATE</td>
                                                                <td>AVAILABLE QTY</td>
                                                                <td>ISSUING QTY</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tbBatches">
                                                        </tbody>
                                                        <tfoot style="background-color: #efefef; color: black; font-weight: bold;">
                                                            <tr>
                                                                <td class="text-right" colspan="3">TOTAL ISSUING QTY</td>
                                                                <td>
                                                                    <label id="lblTotalIssue" style="margin-left: 13px;">0</label>
                                                                </td>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button id="btnMdlBatchSubmit" class="btn btn-primary pull-right" style="margin-right: 10px" onclick="issueInventory(event);">SUBMIT</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="mdlIssueStock" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">Issue Stock from Warehouse</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-3 text-center">
                                        <b>Requested QTY </b>
                                        <p id="requestedQtyShow"></p>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <b>Issued QTY </b>
                                        <p id="issuedQtyShow"></p>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <b>Pending QTY </b>
                                        <p id="pendingQtyShow"></p>
                                    </div>
                                    <div class="col-sm-3 text-center">
                                        <b>Available QTY </b>
                                        <p id="availableQtyShow"></p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="co-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvWarehouseInventory" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Records Found" AllowPaging="true" PageSize="20">
                                                <Columns>
                                                    <asp:BoundField DataField="WarehouseID" HeaderText="Warehouse ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemID" HeaderText="ItemID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="Location" HeaderText="Location" />
                                                    <asp:BoundField DataField="AvailableQty" HeaderText="Avilable Qty" />
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="IssuedQty" CssClass="IssuedQty" OnKeyUp="issuedQuantity(event)" min="0" step=".01"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="FromWarehouseId" HeaderText=" From Warehouse ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" Text="Issue" CssClass="btn btn-primary pull-right btnIssueCl" Style="margin-right: 10px" />
                            </div>
                        </div>
                    </div>
                </div>


                <div id="SuccessAlert" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">Success</h4>
                            </div>
                            <div class="modal-body">
                                <p id="successMessage" style="font-weight: bold; font-size: medium;"></p>
                            </div>
                            <div class="modal-footer">
                                <span class="btn btn-info" data-dismiss="modal" aria-label="Close">OK</span>
                                <%--<button id="btnOki" class="btn btn-success">OK</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="errorAlert" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #ff0000;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">ERROR</h4>
                            </div>
                            <div class="modal-body">
                                <p id="errorMessage" style="font-weight: bold; font-size: medium;"></p>
                            </div>
                            <div class="modal-footer">
                                <span class="btn btn-danger" data-dismiss="modal" aria-label="Close">OK</span>
                                <%--<button id="btnOki" class="btn btn-success">OK</button>--%>
                            </div>
                        </div>
                    </div>
                </div>




                <section class="content">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelApprovRejectTR" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >Submitted Transfer Requests</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
              <asp:GridView runat="server" ID="gvApprovRejectTR" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" DataKeyNames="TRId" OnRowDataBound="gvApprovRejectTR_RowDataBound" EmptyDataText="No records Found">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                            <img alt = "" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                            <asp:Panel ID="pnlApprovRejectTRD" runat="server" Style="display: none">
                                <asp:GridView ID="gvApprovRejectTR" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                                    <Columns>
                                        <asp:BoundField DataField="TRDId" HeaderText="TRD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category"/>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName"  HeaderText="Sub-Category"  />
                                        <asp:BoundField DataField="ItemID" HeaderText="Item ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="ItemName"  HeaderText="Item"  />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="RequestedQTY"  HeaderText="Requested Qty" DataFormatString="{0:N2}"  />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="IssuedQty"  HeaderText="Issued Qty"  DataFormatString="{0:N2}" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="ReceivedQty"  HeaderText="Received Qty"  DataFormatString="{0:N2}" />
                                        <asp:BoundField ItemStyle-Width="150px" DataField="AvailableQty"  HeaderText="Available Stock"   DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />  
                                        <asp:BoundField ItemStyle-Width="150px" DataField="ShortName"  HeaderText="Unit" NullDisplayText="Not Found"  />  
                                        <asp:BoundField ItemStyle-Width="150px" DataField="Description"  HeaderText="Description"  /> 
                                        <asp:BoundField DataField="CategoryID" HeaderText="categoryID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="SubCategoryID" HeaderText="subCategoryID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="UnitPrice" HeaderText="Estimated Unit Price" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="TRId" HeaderText="TR Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />    
                                        <asp:BoundField DataField="MeasurementId" HeaderText="MeasurementId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />    
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                                         <ItemTemplate>
                                             <asp:Label runat="server" ID="txtTRDStatus" Text='<%#Eval("Status").ToString()=="0"? "Pending" :Eval("Status").ToString()=="1"?"Added to PR": Eval("Status").ToString()=="2"?"Partially-Issued": Eval("Status").ToString()=="3"?"Fully-Issued": Eval("Status").ToString()=="4"?"Delivered":Eval("Status").ToString()=="5"?"Received":"Terminated" %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btnIssueFromStock" CssClass="btn btn-xs btn-info" style="margin:5px" Width="120px" Text="Issue from Stock" 
                                                    OnClientClick='<%# "issueFromInventory(event,"+Eval("ItemId").ToString()+","+Eval("ToWarehouseId").ToString()+","+Eval("MeasurementId").ToString()+","+Eval("StockMaintainingType").ToString()+","+Eval("RequestedQty").ToString()+","+Eval("IssuedQty").ToString()+","+Eval("TRId").ToString()+","+Eval("TRDId").ToString()+","+Eval("FromWarehouseId").ToString()+")" %>'
                                                    Visible='<%#Eval("Status").ToString()=="0" && decimal.Parse(Eval("AvailableQty").ToString())>0? true :Eval("Status").ToString()=="1"&& decimal.Parse(Eval("AvailableQty").ToString())>0?true: Eval("Status").ToString()=="2" && decimal.Parse(Eval("AvailableQty").ToString())>0?true: false %>'></asp:Button>
                                                <asp:Button runat="server" ID="btnAddToPRHidden" CssClass="hidden btnAddToPRHiddenCl"
                                                    OnClick="btnAddToPR_Click"/>
                                                <asp:Button runat="server" ID="btnAddToPR" 
                                                    CssClass="btn btn-xs btn-success" Width="120px" style="margin:5px" Text="Add to PR" 
                                                    OnClientClick='<%# "addToPrClicked(this,event,"+Eval("ItemId").ToString()+","+Eval("MeasurementId").ToString()+")" %>'
                                                    Visible='<%#Eval("Status").ToString() =="1"?false:Eval("Status").ToString()=="0" && decimal.Parse(Eval("AvailableQty").ToString())==0? true : Eval("Status").ToString()=="2" && decimal.Parse(Eval("AvailableQty").ToString())==0?true: false %>'></asp:Button>
                                                <asp:Button runat="server" ID="btnTerminateTRD" CssClass="btn btn-xs btn-danger" Width="120px" style="margin:5px" Text="Terminate" Visible='<%# Eval("Status").ToString() !="3" && Eval("Status").ToString() !="6"?true:false %>' OnClientClick='<%#"terminateTRD(event,"+Eval("TRDID").ToString()+","+Eval("TRID").ToString()+",\""+Eval("ItemName").ToString()+"\")" %>' ></asp:Button>
                                                <asp:Label runat="server" Text="Terminated" ForeColor="Red" Visible='<%# Eval("Status").ToString() =="6"?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                       
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="TRId"  HeaderText="TR ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:TemplateField HeaderText="TR Code">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTRCode" Text='<%# "TR"+Eval("TRCode").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ToWarehouse"  HeaderText="To Warehouse" />
                         <asp:BoundField DataField="FromWarehouse"  HeaderText="From Warehouse" />
                        <asp:BoundField DataField="CreatedByName"  HeaderText="Created By" />
                        <asp:BoundField DataField="CreatedDatetime"  HeaderText="Created On" DataFormatString="{0:dd-MM-yyyy hh:mm tt}"/>
                        <asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date" DataFormatString="{0:dd-MM-yyyy}"/>
                        <asp:BoundField DataField="Description"  HeaderText="Description" />
                        <asp:BoundField DataField="ToWarehouseId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:TemplateField HeaderText="Status">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtTRStatus" Text='<%#Eval("Status").ToString()=="0"? "Pending":"Completed" %>' ForeColor='<%#Eval("Status").ToString()=="0"? System.Drawing.Color.Orange:System.Drawing.Color.Green%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Approved/Rejected">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtTRIsApproved" Text='<%#Eval("IsApproved").ToString()=="1"? "Approved":"Rejected"%>' ForeColor='<%#Eval("IsApproved").ToString()=="1"? System.Drawing.Color.Green:System.Drawing.Color.Red%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnTerminateTR" CssClass="btn btn-xs btn-danger" Width="120px" style="margin:5px" Text="Terminate" Visible='<%# Eval("ItemCount").ToString() =="0"?true:false %>' OnClientClick='<%#"terminateTR(event,"+Eval("TRId").ToString()+")" %>' ></asp:Button>
                                <asp:Label runat="server" Text="Please Terminate Item Wise" Visible='<%# Eval("ItemCount").ToString() !="0"?true:false %>' ForeColor="Red"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FromWarehouseId"  HeaderText="FromWarehouse" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                    </Columns>
                </asp:GridView>
                </div>
            </div>         
          </div>
         
        </div>
        <!-- /.box-body -->
      </div>
      <!-- /.box -->
    </section>



                <section class="content">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="Div1" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >Items Added to PR</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
              <asp:GridView runat="server" ID="gvAddToPR" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" EmptyDataText="No records Found">
                    <Columns>

                                <asp:BoundField DataField="MainCategoryId" HeaderText="MainCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="MainCategoryName" HeaderText="Category Name" />
                                    <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="SubcategoryName" HeaderText="Sub Category Name" />
                                    <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                    <asp:BoundField DataField="ItemQuantity" HeaderText="Quantity"  DataFormatString="{0:N2}"/>
                                <asp:BoundField DataField="MeasurementName" HeaderText="Measurement" />
                                    <asp:BoundField DataField="EstimatedAmount" HeaderText="Estimated Unit Price" DataFormatString="{0:N2}"/> 
                                <asp:BoundField DataField="MeasurementId" HeaderText="MeasurementId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />  
                                    <asp:BoundField DataField="TRCodes" HeaderText="TR Code" />
                            <asp:BoundField DataField="WarehouseName" HeaderText="For Warehouse" />
                    </Columns>
                </asp:GridView>
                </div>
            </div>         
          </div>
         
        </div>
        <!-- /.box-body -->

          <div class="box-footer">
        <div class="form-group">
              <asp:Button ID="btnCreatePR"  runat="server" Text="Create PR" CssClass="btn btn-primary pull-right" OnClick="btnCreatePR_Click"></asp:Button>
          </div>
        </div>
      </div>
      <!-- /.box -->
    </section>

                <asp:HiddenField runat="server" ID="hiddenpending" />
                <asp:HiddenField runat="server" ID="hiddenavaiability" />
                <asp:Button ID="btnIssue" runat="server" OnClick="btnIssue_Click" CssClass="hidden" />
                <asp:Button ID="btnTerminateTR" runat="server" OnClick="btnTerminateTR_Click" CssClass="hidden" />
                <asp:Button ID="btnTerminateTRD" runat="server" OnClick="btnTerminateTRD_Click" CssClass="hidden" />
                <asp:HiddenField runat="server" ID="hdnTRID" />
                <asp:HiddenField runat="server" ID="hdnTRDID" />
                <asp:HiddenField runat="server" ID="hdnItemName" />
                <asp:HiddenField runat="server" ID="hdnRemarks" />
                <asp:HiddenField runat="server" ID="hdnMeasurementId" />
                <asp:HiddenField runat="server" ID="hdnMeasurementName" />
                <asp:HiddenField runat="server" ID="hdnReqMeasurementId" />


            </ContentTemplate>

        </asp:UpdatePanel>
    </form>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <script src="AdminResources/js/select2.full.min.js"></script>




    <script type="text/javascript">

        var _ItemId, _MeasurementId, _WarehouseId, _StockMaintainingType, _RequestedQty, _IssuedQty, _TrId, _TrdId, _IventoryDetails, _FromWarehouseId;

        Sys.Application.add_load(function () {
            $(function () {
                $('.btnIssueCl').on({
                    click: function () {
                        event.preventDefault();

                        $('#mdlIssueStock').modal('hide');

                        swal.fire({
                            title: 'Confirmation',
                            html: "Are you sure you want to continue?",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes',
                            cancelButtonText: 'No',
                            allowOutsideClick: false
                        }
                        ).then((result) => {
                            if (result.value) {

                                $('#ContentSection_btnIssue').click();
                            } else if (result.dismiss === Swal.DismissReason.cancel) {

                                $('#mdlIssueStock').modal('show');
                            }
                        });
                    }
                })


            });
        });

        function issueFromInventory(e, itemId, warehouseId, measurementId, stockMaintainingType, requestedQty, issuedQty, trId, trdId, fromWarehouseId) {
            e.preventDefault();

            _ItemId = itemId;
            _MeasurementId = measurementId;
            _WarehouseId = warehouseId;
            _StockMaintainingType = stockMaintainingType;
            _RequestedQty = requestedQty;
            _IssuedQty = issuedQty;
            _TrId = trId;
            _TrdId = trdId;
            _FromWarehouseId = fromWarehouseId;

            populateMeasurements(itemId, measurementId, stockMaintainingType);
            populateInventory(measurementId);

            if (stockMaintainingType == 1) {
                $('#mdlIssueAvg').modal('show');
            }
            else {
                $('#mdlIssueBatch').modal('show');
            }
        }

        function populateMeasurements(itemId, measurementId, stockMaintainingType) {
            $.ajax({
                type: "GET",
                url: 'ViewSubmittedTR.aspx/GetItemMeasurements?ItemId=' + itemId,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    response = JSON.parse(response.d);
                    if (response.Status == 200) {
                        var htmlText = ``;
                        for (var i = 0; i < response.Data.length; i++) {
                            if (response.Data[i].Id == measurementId) {
                                htmlText += `<option value='` + response.Data[i].Id + `' selected>` + response.Data[i].Name + `</option>`;
                            }
                            else {
                                htmlText += `<option value='` + response.Data[i].Id + `'>` + response.Data[i].Name + `</option>`;
                            }
                        }

                        if (stockMaintainingType == 1) {
                            $('#ddlAvgMeasurement').html(htmlText);
                        }
                        else {
                            $('#ddlBatchMeasurement').html(htmlText);
                        }

                        $('.select2').select2();
                    }
                },
                error: function (error) {
                }
            });
        }

        function populateInventory(measurementId) {
            $.ajax({
                type: "GET",
                url: 'ViewSubmittedTR.aspx/GetStockInfo?ItemId=' + _ItemId + '&WarehouseId=' + _WarehouseId + '&MeasurementId=' + measurementId + '&RequestedQty=' + _RequestedQty + '&IssuedQty=' + _IssuedQty + '&RequestedMeasurement=' + _MeasurementId,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    response = JSON.parse(response.d);
                    _IventoryDetails = response.Data;

                    if (response.Status == 200) {
                        var htmlText = ``;

                        if (_StockMaintainingType == 1) {
                            $('#mdlAvgItem').html(response.Data.ItemName + " : " + response.Data.ItemCode);
                            $('#mdlAvgWarehouse').html(response.Data.WarehouseName);

                            $('#mdlAvgRequested').html(response.Data.RequestedQty);
                            $('#mdlAvgIssued').html(response.Data.IssuedQty);
                            $('#mdlAvgPending').html(response.Data.RequestedQty - response.Data.IssuedQty);
                            $('#mdlAvgAvailable').html(response.Data.AvailableQty);
                            $('#mdlAvgIssuingQty').val('');
                        }
                        else {
                            $('#mdlBatchItem').html(response.Data.ItemName + " : " + response.Data.ItemCode);
                            $('#mdlBatchWarehouse').html(response.Data.WarehouseName);

                            $('#mdlBatchRequested').html(response.Data.RequestedQty);
                            $('#mdlBatchIssued').html(response.Data.IssuedQty);
                            $('#mdlBatchPending').html(response.Data.RequestedQty - response.Data.IssuedQty);
                            $('#mdlBatchAvailable').html(response.Data.AvailableQty);

                            for (var i = 0; i < response.Data.Batches.length; i++) {
                                htmlText += `<tr>
                                                <td class="hidden batchIdCl">`+ response.Data.Batches[i].BatchchId + `</td>
                                                <td>`+ response.Data.Batches[i].BatchCode + `</td>
                                                <td>`+ response.Data.Batches[i].ExpiryDate.replace("T00:00:00", "") + `</td>
                                                <td class="availableQtyCl">`+ response.Data.Batches[i].AvailableStock + `</td>
                                                <td>
                                                    <input type="number" class="form-control txtQtyCl" onkeyup="calculateIssues();" min="0" step="0.00001" />
                                                </td>
                                            </tr>`
                            }

                            if (_StockMaintainingType == 2) {
                                $('#spnBatchOrder').html('Batches Ordered as per FIFO method');
                            }
                            else {
                                $('#spnBatchOrder').html('Batches Ordered as per FILO method');
                            }

                            $('#tbBatches').html(htmlText);
                            $('#lblTotalIssue').html('');
                        }
                    }
                },
                error: function (error) {
                }
            });
        }

        function measurementChanged(elm) {
            var measurementId = $(elm).find('option:selected').val();
            populateInventory(measurementId);
        }

        function checkAvailableQty() {
            if ($('#mdlAvgIssuingQty').val() != '') {
                if (parseFloat($('#mdlAvgIssuingQty').val()) > parseFloat($('#mdlAvgAvailable').text())) {
                    $('#mdlAvgIssuingQty').val($('#mdlAvgAvailable').text());
                }
            }
        }

        function issueInventory(e) {
            e.preventDefault();

            if (_StockMaintainingType == 1) {
                $('#mdlIssueAvg').modal('hide');
            }
            else {
                $('#mdlIssueBatch').modal('hide');
            }

            var issueNote;
            var trdStatus;

            if (_StockMaintainingType == 1) {
                issueNote = {
                    TRDId: _TrdId,
                    ItemId: _ItemId,
                    WarehouseId: _WarehouseId,
                    IssuedQTY: parseFloat($('#mdlAvgIssuingQty').val()),
                    IssuedStockValue: (_IventoryDetails.StockValue / (_IventoryDetails.AvailableQty + _IventoryDetails.HoldedQty)) * parseFloat($('#mdlAvgIssuingQty').val()),
                    MeasurementId: $('#ddlAvgMeasurement option:selected').val(),
                    RequestedMeasurementId: _MeasurementId,
                    IssuedBatches: []
                }

                if (issueNote.IssuedQTY < parseFloat($('#mdlAvgPending').html()))
                    trdStatus = 2;
                else
                    trdStatus = 3;
            }
            else {
                issueNote = {
                    TRDId: _TrdId,
                    ItemId: _ItemId,
                    WarehouseId: _WarehouseId,
                    IssuedQTY: 0,
                    IssuedStockValue: 0,
                    MeasurementId: $('#ddlBatchMeasurement option:selected').val(),
                    RequestedMeasurementId: _MeasurementId,
                    IssuedBatches: []
                }

                var totalIssuedQty = 0;
                var totalIssuedStockValue = 0;
                var rows = $('#tbBatches').find('tr');

                for (var i = 0; i < rows.length; i++) {
                    if ($(rows).eq(i).find('.txtQtyCl').val() != '') {
                        let issuedBatch = {
                            BatchId: parseInt($(rows).eq(i).find('.batchIdCl').text()),
                            IssuedQty: parseFloat($(rows).eq(i).find('.txtQtyCl').val()),
                            IssuedStockValue: (_IventoryDetails.Batches[i].StockValue / (_IventoryDetails.Batches[i].HoldedQty + _IventoryDetails.Batches[i].AvailableStock)) * parseFloat($(rows).eq(i).find('.txtQtyCl').val())
                        }
                        totalIssuedQty += parseFloat($(rows).eq(i).find('.txtQtyCl').val());

                        totalIssuedStockValue += issuedBatch.IssuedStockValue;
                        issueNote.IssuedBatches.push(issuedBatch);
                    }
                }

                issueNote.IssuedQTY = totalIssuedQty;
                issueNote.IssuedStockValue = totalIssuedStockValue;

                if (issueNote.IssuedQTY < parseFloat($('#mdlBatchPending').html()))
                    trdStatus = 2;
                else
                    trdStatus = 3;

            }

            var postData = {
                TrdId: _TrdId,
                TrdStatus: trdStatus,
                TrId: _TrId,
                Note: issueNote,
                FromWarehouseId: _FromWarehouseId
            };

            debugger;

            $.ajax({
                type: "POST",
                url: 'ViewSubmittedTr.aspx/IssueInventory',
                data: JSON.stringify(postData),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    response = JSON.parse(response.d);
                    if (response.Status == 200) {
                        swal({ type: 'success', title: 'Material Issued', text: 'Material Issued Successfully', showConfirmButton: true, closeOnConfirm: true }).then((result) => { window.location = 'ViewSubmittedTr.aspx' });
                    }
                },
                error: function (error) {
                }
            });
        }

        function calculateIssues() {
            var totalIssue = 0;
            var rows = $('#tbBatches').find('tr');
            for (var i = 0; i < rows.length; i++) {
                if ($(rows).eq(i).find('.txtQtyCl').val() != '') {
                    if (parseFloat($(rows).eq(i).find('.txtQtyCl').val()) > parseFloat($(rows).eq(i).find('.availableQtyCl').text())) {
                        parseFloat($(rows).eq(i).find('.txtQtyCl').val($(rows).eq(i).find('.availableQtyCl').text()));
                    }

                    totalIssue += parseFloat($(rows).eq(i).find('.txtQtyCl').val());
                }
            }
            $('#lblTotalIssue').html(totalIssue);
        }


        function addToPrClicked(elm, e, itemId, measurementId) {
            e.preventDefault();

            $('#ContentSection_hdnReqMeasurementId').val(measurementId);

            var htmlText = ``;

            $.ajax({
                type: "GET",
                url: 'ViewSubmittedTR.aspx/GetItemMeasurements?ItemId=' + itemId,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    response = JSON.parse(response.d);
                    if (response.Status == 200) {

                        for (var i = 0; i < response.Data.length; i++) {
                            if (response.Data[i].Id == measurementId) {
                                htmlText += `<option value='` + response.Data[i].Id + `' selected>` + response.Data[i].Name + `</option>`;
                            }
                            else {
                                htmlText += `<option value='` + response.Data[i].Id + `'>` + response.Data[i].Name + `</option>`;
                            }
                        }

                    }
                },
                error: function (error) {
                }
            }).then((result) => {
                swal.fire({
                    //title: 'Are you sure?',
                    html: `<div class="row">
                                <div class="col-md-12">
                                    <strong id='qq'>Please Select a Measurement</strong></br></br>
                                    <select id='pp' class ='form-control'>
                                    `+ htmlText + `
                                    </select>
                                </div>
                           </div>`,

                    type: 'warning',
                    cancelButtonColor: '#d33',
                    showCancelButton: true,
                    showConfirmButton: true,
                    confirmButtonText: 'Ok',
                    cancelButtonText: 'Cancel',
                    allowOutsideClick: false
                }).then((result) => {
                    if (result.value) {
                        debugger;
                        $('#ContentSection_hdnMeasurementId').val($('#pp option:selected').val());
                        $('#ContentSection_hdnMeasurementName').val($('#pp option:selected').html());
                        $(elm).closest('tr').find('.btnAddToPRHiddenCl').click();

                    }
                });
            });
        }

        function terminateTR(e, TRId) {
            e.preventDefault();
            $('#ContentSection_hdnTRID').val(TRId);

            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want Terminate this TR?</br></br>"
                    + "<strong id='dd'>Remarks</strong>"
                    + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                    if ($('#ss').val() == '') {
                        $('#dd').prop('style', 'color:red');
                        swal.showValidationError('Remarks Required');
                        return false;
                    }
                    else {
                        $('#ContentSection_hdnRemarks').val($('#ss').val());
                    }

                }
            }
            ).then((result) => {
                if (result.value) {
                    $('#ContentSection_btnTerminateTR').click();
                }
            });
        }

        function terminateTRD(e, trdId, trId, itemName) {
            e.preventDefault();
            $('#ContentSection_hdnTRDID').val(trdId);
            $('#ContentSection_hdnTRID').val(trId);
            $('#ContentSection_hdnItemName').val(itemName);

            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want Terminate this TR Item?</br></br>"
                    + "<strong id='dd'>Remarks</strong>"
                    + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                    if ($('#ss').val() == '') {
                        $('#dd').prop('style', 'color:red');
                        swal.showValidationError('Remarks Required');
                        return false;
                    }
                    else {
                        $('#ContentSection_hdnRemarks').val($('#ss').val());
                    }

                }
            }
            ).then((result) => {
                if (result.value) {
                    $('#ContentSection_btnTerminateTRD').click();
                }
            });
        }

        function issuedQuantity(e) {
            debugger;

            if (e.key != '.') {
                var rows = $('#ContentSection_gvWarehouseInventory').find('tr');

                var pending = $(document).find('#ContentSection_hiddenpending').val();
                var available = $(document).find('#ContentSection_hiddenavaiability').val();


                for (let i = 1; i < rows.length; i++) {

                    let row = $(rows).eq(i).find('td');
                    var txt = $(row).eq(4).find(".IssuedQty");



                    if (txt.val() != "0") {
                        var qty = 0;
                        if (txt.val() != "")
                            qty = parseFloat(txt.val());
                        else {
                            break;
                        }

                        var x = parseFloat($(row).eq(3).html());

                        if (qty >= parseFloat($(row).eq(3).html()))
                            qty = parseFloat($(row).eq(3).html());


                        if (pending > 0) {
                            if (qty <= available) {
                                if (qty <= pending) {
                                    txt.val(qty);
                                    available -= qty;
                                    pending -= qty;
                                }
                                else {

                                    txt.val(pending);
                                    available -= pending;
                                    pending = 0;
                                }
                            }
                            else {
                                if (pending > available) {
                                    if (qty <= pending) {
                                        txt.val(qty);
                                        available = 0;
                                        pending -= qty;
                                    }
                                    else {
                                        txt.val(pending);
                                        available = 0;
                                        pending = 0;
                                    }
                                }
                                else {
                                    if (qty <= pending) {
                                        txt.val(qty);
                                        available -= qty;
                                        pending -= qty;
                                    }
                                    else {
                                        txt.val(pending);
                                        available -= pending;
                                        pending = 0;
                                    }
                                }
                            }
                        }
                        else {
                            if (txt.val() != '')
                                txt.val('0');
                        }


                    }

                }
            }



        }

    </script>





</asp:Content>

