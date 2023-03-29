<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewDeliveredTRInventory.aspx.cs" Inherits="BiddingSystem.ViewDeliveredTRInventory" %>

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
      View Delivered Inventory
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Delivered Inventory</li>
      </ol>
    </section>
    <br />

    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>

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
      <div class="box box-info" id="panel" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >View Delivered Inventory</h3>

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
                
              <asp:GridView runat="server" ID="gvDeliveredInventory" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" DataKeyNames="TRDInId" EmptyDataText="No records Found">
                    <Columns>
                        <asp:BoundField DataField="TRDInId"  HeaderText="TRDIN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="TRDId"  HeaderText="TRD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="ItemId"  HeaderText="Item ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="TRId"  HeaderText="TR ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:TemplateField HeaderText="TR Code">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnTR" Text='<%# "TR"+Eval("TrCode").ToString() %>' OnClick="btnTR_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ItemName"  HeaderText="Item Name" />
                        <asp:BoundField DataField="WarehouseId"  HeaderText="Warehouse ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="Location"  HeaderText="From Warehouse"/>
                        <asp:BoundField DataField="IssuedQTY"  HeaderText="Issued QTY"/>
                        <asp:BoundField DataField="measurementShortName"  HeaderText="Unit" NullDisplayText="Not Found"/>
                        <asp:BoundField DataField="DeliveredUser"  HeaderText="Delivered By"/>
                        <asp:BoundField DataField="DeliveredOn" HeaderText="Delivered On"/>

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnReceive" CssClass="btn btn-sm btn-success" Text="Receive" OnClick="btnReceive_Click"></asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FromWarehouseId"  HeaderText="From Warehouse Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="IssuedStockValue"  HeaderText="Stock Price" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="MeasurementId"  HeaderText="Measurement Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        
                       
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
      <div class="box box-info" id="panelApprovRejectTR" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >View Received Inventory</h3>

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
              <asp:GridView runat="server" ID="gvReceivedInventory" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" EmptyDataText="No records Found">
                    <Columns>
                        <asp:BoundField DataField="TRDInId"  HeaderText="TRDIN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="TRDId"  HeaderText="TRD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="TRId"  HeaderText="TR ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:TemplateField HeaderText="TR Code">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnReceivedTR" Text='<%# "TR"+Eval("TrCode").ToString() %>' OnClick="btnReceivedTR_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ItemID"  HeaderText="Item ID"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="ItemName"  HeaderText="Item Name" />
                        <asp:BoundField DataField="Location"  HeaderText="From Warehouse"/>
                        <asp:BoundField DataField="IssuedQty"  HeaderText="Issued QTY"/>
                        <asp:BoundField DataField="measurementShortName"  HeaderText="Unit" NullDisplayText="Not Found"/>
                        <asp:BoundField DataField="IssuedOn" HeaderText="Issued On"/>
                        <asp:BoundField DataField="ReceivedUser"  HeaderText="Received By"/>
                        <asp:BoundField DataField="ReceivedOn" HeaderText="Received On"/>
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

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>

</asp:Content>

