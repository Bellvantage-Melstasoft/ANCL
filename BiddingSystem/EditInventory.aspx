<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="EditInventory.aspx.cs" Inherits="BiddingSystem.EditInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <script src="AdminResources/js/jquery1.8.min.js"></script>



    <style type="text/css">
        .select2-container--default .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            color: black;
        }

        @media print {

            @page {
                size: A4;
                margin: 5mm 5mm 5mm 5mm;
            }

            .print-count {
                display: block;
            }
        }

        @media screen {
            .print-count {
                display: none;
            }
        }

        .paddingTop {
            margin-top: 25px;
        }
    </style>

    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />



    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <!-- bootstrap datepicker -->
    <section class="content-header">
      <h1>
       Edit Inventory 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Edit Inventory </li>
      </ol>
    </section>
    <br />
    <section class="content" id="divPrintPo">
        <form id="form1" runat="server">

            <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-default no-print">
                <div class="panel-body">
                    <div class="row">
                         <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Select Warehouse</label>
                                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlWarehouse" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                  <asp:DropDownList ID="ddlWarehouse" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Select Category</label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlMainCateGory" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                  <asp:DropDownList ID="ddlMainCateGory" runat="server" CssClass="form-control"  
                                    AutoPostBack="true" 
                                    onselectedindexchanged="ddlMainCateGory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Select SubCategory</label>
                                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlSubCategory" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                  <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control"  
                                    AutoPostBack="true" 
                                    onselectedindexchanged="ddlSubCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Select Item</label>
                                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlItem" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                  <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                       
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button runat="server" ID="btnSearch" ValidationGroup="btnSearch" CssClass="btn btn-primary pull-right btnSearch" Text="Search" OnClick="btnSearch_Click" />
                        <img id="loader" alt="" src="UserRersourses/assets/img/loader-info.gif" class="pull-right hidden paddingTop" style="margin-right:10px; max-height:30px;" />
                             
                        </div>
                    </div>
                </div>
            </div>

            <div class="box box-info">
                <div class="box-header with-border">
                  <h3 class="box-title" >Edit Inventory</h3>
                </div>
                <div class="box-body">
                    
                    <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Panel ID="pnlBatch" runat="server">
                                <label for="exampleInputEmail1">Select Batch</label>
                                  <asp:DropDownList ID="ddlBatches" runat="server" CssClass="form-control" AutoPostBack="true"  onselectedindexchanged="ddlBatches_SelectedIndexChanged" >
                                </asp:DropDownList>
                                    </asp:Panel>
                            </div>
                        </div>
                        </div>
                        </div>
                    <asp:Panel ID="pnlItem" runat="server">
                  <div class="row">
                    <div class="col-md-12">
                    <div class="table-responsive">
                      <asp:GridView runat="server" ID="gvItems" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" EmptyDataText="No records Found"   >
                            <Columns>
                             
                                <asp:BoundField DataField="ItemID" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField DataField="WarehouseID" HeaderText="WarehouseId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField DataField="ItemName"  HeaderText="Item Name" />
                                <asp:BoundField DataField="Location"  HeaderText="Location" />
                                <asp:BoundField DataField="measurementShortName"  HeaderText="Unit of Measurement" NullDisplayText="Not Found"  />
                               
                                 <asp:TemplateField HeaderText="Available Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtItem"  CssClass="clItem form-control"
                                                            Text='<%# Eval("AvailableQty", "{0:N2}").ToString() %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Holded Qty">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblHoldedQty" CssClass="clHoldedQty form-control"
                                                            Text='<%# Eval("HoldedQty", "{0:N2}").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Stock Value">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStockValue" CssClass="clStockValue form-control"
                                                            Text='<%# Eval("StockValue").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="ReorderLevel"  HeaderText="Reorder Level"  DataFormatString="{0:N2}"/>
                            <asp:TemplateField HeaderText="Available Qty" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="lblItemOld"  CssClass="clItemOld form-control"
                                                            Text='<%# Eval("AvailableQty").ToString() %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock Value"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStockValueOld" CssClass="clStockValueOld form-control"
                                                            Text='<%# Eval("StockValue").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock Value" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtStockValue" CssClass="txtclStockValue form-control"
                                                            Text='<%# Eval("StockValue").ToString() %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>
                    </div>  
                      
                  </div>
                        </asp:Panel>
                     <asp:Panel ID="pnlBatchItem" runat="server">
                    <div class="row">
                    <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="gvBatchDetails" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" EmptyDataText="No records Found"   >
                       
                                            <Columns>
                                                <asp:BoundField DataField="BatchchId" HeaderText="Btach Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                
                                                <asp:TemplateField HeaderText="Batch Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblBatchCode"
                                                            Text='<%# "Batch-"+Eval("BatchCode").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Available Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtBatchItem"  CssClass="clBatchItem form-control"
                                                            Text='<%# Eval("AvailableStock", "{0:N2}").ToString() %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Holded Qty">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblHoldedQtyB" CssClass="clBatchHoldedQty form-control"
                                                            Text='<%# Eval("HoldedQty", "{0:N2}").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Stock Value">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStockValueB" CssClass="clBatchStockValue form-control"
                                                            Text='<%# Eval("StockValue").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="Expiry Date">
                                                <ItemTemplate>
                                                    <%# (DateTime)Eval("ExpiryDate") == DateTime.MinValue ? "Not Found" : string.Format("{0:MM-dd-yyyy}", (DateTime)Eval("ExpiryDate")) %>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Available Qty" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="lblBatchItemOld"  CssClass="clBatchItemOld form-control"
                                                            Text='<%# Eval("AvailableStock", "{0:N2}").ToString() %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Stock Value" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStockValueBOld" CssClass="clBatchStockValueOld form-control"
                                                            Text='<%# Eval("StockValue").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stock Value" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtStockValueB" CssClass="txtBatchStockValue form-control"
                                                            Text='<%# Eval("StockValue").ToString() %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                            </Columns>
                                    </asp:Gridview>
                        </div>
                    </div>  
                      
                  </div>
                         </asp:Panel>

                </div>
                <div class ="box-footer no-print">
            <div>
                <%--<asp:button id="btnPrint" runat="server" text="Print" class="btn btn-success btnprintcl pull-right" OnClientClick="printPage()"/>--%>
                <asp:button id="btnSave" runat="server" text="Save" class="btn btn-info pull-right" OnClick="btnSave_Click"/>
            </div>
             </div>
            </div>

            
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
         </form>
    </section>
    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>
    <script src="AdminResources/js/select2.full.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(function () {

            $(function () {
                $('.btnSearch').on({
                    click: function () {
                        $('#loader').removeClass('hidden');
                    }
                })
            });


            $('.clItem').on({

                keyup: function () {
                    var Qty = $(this).closest('tr').find('.clItem').val();
                    var OldQty = $(this).closest('tr').find('.clItemOld').val();
                    var HoldedQty = $(this).closest('tr').find('.clHoldedQty').text();
                    var StockValue = $(this).closest('tr').find('.clStockValueOld').text();

                    if (Qty > 0 && Qty != "") {
                        var TotalStock = parseFloat(OldQty) + parseFloat(HoldedQty);
                        var UnitPrice = parseFloat(StockValue) / parseFloat(TotalStock);
                        var NewStockValue = parseFloat(UnitPrice) * parseFloat(Qty);

                        $(this).closest('tr').find('.clStockValue').text(NewStockValue.toFixed(2));
                        $(this).closest('tr').find('.txtclStockValue').val(NewStockValue.toFixed(2));
                        
                    }
                    else {
                        $(this).closest('tr').find('.clStockValue').text("0.00");
                         $(this).closest('tr').find('.txtclStockValue').val("0.00");
                    }
                    
                }
            });

             $('.clBatchItem').on({

                 keyup: function () {
                     var BQty = $(this).closest('tr').find('.clBatchItem').val();
                     var BOldQty = $(this).closest('tr').find('.clBatchItemOld').val();
                     var BHoldedQty = $(this).closest('tr').find('.clBatchHoldedQty').text();
                     var BStockValue = $(this).closest('tr').find('.clBatchStockValueOld').text();

                     if (BQty > 0 && BQty != "") {
                        var BTotalStock = parseFloat(BOldQty) + parseFloat(BHoldedQty);
                        var BUnitPrice = parseFloat(BStockValue) / parseFloat(BTotalStock);
                        var BNewStockValue = parseFloat(BUnitPrice) * parseFloat(BQty);

                         $(this).closest('tr').find('.clBatchStockValue').text(BNewStockValue.toFixed(2));
                          $(this).closest('tr').find('.txtBatchStockValue').val(BNewStockValue.toFixed(2));
                    }
                    else {
                         $(this).closest('tr').find('.clBatchStockValue').text("0.00");
                          $(this).closest('tr').find('.txtBatchStockValue').val("0.00");
                    }

                    
                }
            });


        });




    </script>
    <script type="text/javascript">
        function printPage() {
            var date = new Date();
            var val = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
            $('#ContentSection_lblPrintDate').text(val);

            window.print();





        }




    </script>
</asp:Content>
