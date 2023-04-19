<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerViewApprovedPurchaseOrder.aspx.cs" Inherits="BiddingSystem.CustomerViewApprovedPurchaseOrder" %>

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
        <h1>Approved Purchase Orders (PO) 
        <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Purchase Orders </li>
        </ol>
    </section>
    <br />

    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content">
                    <!-- SELECT2 EXAMPLE -->
                    <div class="box box-info" id="panelPurchaseRequset" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">View Purchase Orders</h3>

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
                                        <asp:GridView runat="server" ID="gvPurchaseOrder" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" DataKeyNames="PoID" OnRowDataBound="OnRowDataBound" EmptyDataText="No records Found">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer; margin-top: -6px;" src="images/plus.png" />
                                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                            <asp:GridView ID="gvPoDetails" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid">
                                                                <Columns>
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="PoID" HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category Name" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName" HeaderText="Sub Category Name" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="ItemName" HeaderText="Item Name" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="SupplierMentionedItemName" HeaderText="Supplier Mentioned Item Name" NullDisplayText="Not Found" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="VatAmount" HeaderText="Vat Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="NbtAmount" HeaderText="NBT Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CustomizedVat" HeaderText="Customized Vat" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CustomizedNbt" HeaderText="Customized Nbt" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="CustomizedTotalAmount" HeaderText="Customized Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                                                    <asp:BoundField DataField="CustomizedAmount" HeaderText="CustomizedAmount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField DataField="MeasurementShortName" HeaderText="Measurement" NullDisplayText="Not Found" />
                                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" DataFormatString="{0:N2}" />
                                                                    <asp:BoundField DataField="ReceivedQty" HeaderText="Recived Quantity" DataFormatString="{0:N2}" />
                                                                    <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="txtApproved" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedAmount","{0:N2}") : Eval("ItemPrice","{0:N2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="IsCustomizedAmount" HeaderText="IsCustomizedAmount" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                                                    <asp:TemplateField HeaderText="Sub Total" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="txtSubT" Text='<%#Eval("SubTotal", "{0:N2}")%>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Vat Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="txtApprovedVat" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedVat","{0:N2}") : Eval("VatAmount","{0:N2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="NBT Amount" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" ItemStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="txtApprovedNbt" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedNbt","{0:N2}") : Eval("NbtAmount","{0:N2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="txtApprovedAmount" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedTotalAmount","{0:N2}") : Eval("TotalAmount","{0:N2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="PoID" HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <%--<asp:BoundField DataField="POCode"  HeaderText="PO Code" />--%>
                                                <asp:TemplateField HeaderText="PO Code">
                                                    <ItemTemplate>
                                                        <a href='<%# "ViewPO.aspx?PoId=" + Eval("PoID") %>'><%# Eval("POCode") %></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="PRCode"  HeaderText="PR Code" />--%>
                                                <asp:TemplateField HeaderText="Based PR Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# "PR-"+Eval("PrCode").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="QuotationFor" HeaderText="Quotation For" />
                                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
                                                <asp:BoundField DataField="WarehouseName" HeaderText="Delivering Warehouse" NullDisplayText="Not Found" />
                                                <asp:BoundField DataField="PurchaseType" HeaderText="Purchase Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <%--<asp:BoundField DataField="ItemCount"  HeaderText="Item Count" />--%>
                                                <%--    <asp:BoundField DataField="PrCode"  HeaderText="Based PR Code" />--%>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtnView" Text="Generate GRN" OnClick="btnView_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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


                <section class="content hidden">
                    <!-- SELECT2 EXAMPLE -->
                    <div class="box box-info " id="Div1" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Rejected GRNs</h3>

                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvRejectedGrns" GridLines="None" EmptyDataText="No records Found" CssClass="table table-responsive"
                                            AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="GrnId" HeaderText="GrnId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="PoID" HeaderText="PO ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="_POMaster.PoCode" HeaderText="PO Code" />
                                                <asp:BoundField DataField="_Supplier.SupplierName" HeaderText="Supplier Name" />
                                                <asp:BoundField DataField="_POMaster.VatAmount" HeaderText="Vat Amount" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="_POMaster.NBTAmount" HeaderText="NBT Amount" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="GoodReceivedDate" HeaderText="Good Received Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                                <asp:BoundField DataField="_POMaster.TotalAmount" HeaderText="Total Amount" DataFormatString="{0:N2}" />

                                                <asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>
                                                        <asp:Label CssClass="activePhase" runat="server" ID="lblStatus" Text="Rejected" BackColor="Red" Font-Bold="true" ForeColor="White"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtnEdit" Text="Edit" OnClick="btnEdit_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <!-- /.box-header -->
                        <%-- <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
             <asp:GridView runat="server" ID="gvRejectedGrns" GridLines="None" EmptyDataText="No records Found" CssClass="table table-responsive"
                    AutoGenerateColumns="false">
                    <Columns>
                         <asp:BoundField DataField="GrnId"  HeaderText="GrnId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PoID"  HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                           <asp:BoundField DataField="_POMaster.PoCode"  HeaderText="PO Code"/>
                      <asp:BoundField DataField="_Supplier.SupplierName"  HeaderText="Supplier Name" />
                        <asp:BoundField DataField="_POMaster.VatAmount"  HeaderText="Vat Amount" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="_POMaster.NBTAmount"  HeaderText="NBT Amount" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="GoodReceivedDate"  HeaderText="Good Received Date" DataFormatString="{0:dd/MM/yyyy HH:mm tt}"/>
                        <asp:BoundField DataField="_POMaster.TotalAmount"  HeaderText="Total Amount" DataFormatString="{0:N2}"/>
                         
                           <asp:TemplateField HeaderText="Status">
                         <ItemTemplate >
                             <asp:Label CssClass="activePhase" runat="server" ID="lblStatus" Text="Rejected" BackColor="Red"  Font-Bold="true"  ForeColor="White" ></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                       <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnEdit" Text="Edit" OnClick="btnEdit_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
            </div>         
          </div>
         
        </div>--%>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>


</asp:Content>
