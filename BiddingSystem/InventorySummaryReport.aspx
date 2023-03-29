<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="InventorySummaryReport.aspx.cs" Inherits="BiddingSystem.InventorySummaryReport" %>

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
       Inventory Summary
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Inventory Summary</li>
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
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Select Warehouse</label>
                                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlWarehouse" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                  <asp:DropDownList ID="ddlWarehouse" runat="server" CssClass="form-control">
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
                  <h3 class="box-title" >Report</h3>
                </div>
                <div class="box-body">
                    
                  <div class="row">
                    <div class="col-md-12">
                    <div class="table-responsive">
                      <asp:GridView runat="server" ID="gvItems" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" EmptyDataText="No records Found"  onrowdatabound="gvItems_RowDataBound" >
                            <Columns>
                                <asp:TemplateField>
                                <ItemTemplate>
                                    <img alt="" style="cursor: pointer;margin-top: -6px;" src="images/plus.png"  class='<%# Eval("StockMaintainingType").ToString() =="1"?"hidden":"" %>'/>
                                    <asp:Panel ID="pnlbBatchDetails" runat="server" Style="display: none" >
                                        <asp:Gridview ID="gvBatchDetails" runat="server" AutoGenerateColumns="false"  emptydatatext="No records Found" caption="Issue Note Inventory Batches"
                                            CssClass="ChildGrid" Width="100%">
                                            <Columns>
                                                <asp:BoundField DataField="BatchchId" HeaderText="Btach Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                
                                                <asp:TemplateField HeaderText="Batch Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblBatchCode"
                                                            Text='<%# "Batch-"+Eval("BatchCode").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="AvailableStock" HeaderText="Available Qty" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="HoldedQty" HeaderText="Holded Qty" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="StockValue" HeaderText="Stock Value" DataFormatString="{0:N2}"/>

                                                 <asp:TemplateField HeaderText="Expiry Date">
                                                <ItemTemplate>
                                                    <%# (DateTime)Eval("ExpiryDate") == DateTime.MinValue ? "Not Found" : string.Format("{0:MM-dd-yyyy}", (DateTime)Eval("ExpiryDate")) %>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                            </Columns>
                                    </asp:Gridview>
                                        </asp:Panel>
                                </ItemTemplate>
                                     </asp:TemplateField>
                                <asp:BoundField DataField="ItemID" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField DataField="WarehouseID" HeaderText="WarehouseId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField DataField="ItemName"  HeaderText="Item Name" />
                                <asp:BoundField DataField="Location"  HeaderText="Location" />
                                <asp:BoundField DataField="measurementShortName"  HeaderText="Unit of Measurement" NullDisplayText="Not Found"  />
                                <asp:BoundField DataField="AvailableQty"  HeaderText="Available" DataFormatString="{0:N2}"/>
                                <asp:BoundField DataField="HoldedQty"  HeaderText="Holded"  DataFormatString="{0:N2}"/>
                                <asp:BoundField DataField="StockValue" HeaderText="Stock Value" DataFormatString="{0:N2}"/>              
                                <asp:BoundField DataField="ReorderLevel"  HeaderText="Reorder Level"  DataFormatString="{0:N2}"/>
                            </Columns>
                        </asp:GridView>
                        </div>
                    </div>         
                  </div>
                </div>
                <div class ="box-footer no-print">
            <div>
                <asp:button id="btnPrint" runat="server" text="Print" class="btn btn-success btnprintcl pull-right" OnClientClick="printPage()"/>
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
