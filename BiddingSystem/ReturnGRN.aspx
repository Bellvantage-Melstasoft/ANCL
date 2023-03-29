<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ReturnGRN.aspx.cs" Inherits="BiddingSystem.ReturnGRN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
        .centerHeaderText {
            text-align: center;
        }

        .rightHeaderText {
            text-align: right;
        }

        body {
        }


        @media print {
            body {
            }
        }

        .tablegv {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .tablegv td, .tablegv th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            .tablegv tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .tablegv tr:hover {
                background-color: #ddd;
            }

            .tablegv th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #3C8DBC;
                color: white;
            }

        .successMessage {
            color: #1B6B0D;
            font-size: medium;
        }

        .failMessage {
            color: #C81A34;
            font-size: medium;
        }

        table, td, tr {
            border-color: black;
        }
        /*th{

          background-color:lightgray;
      }*/

        /*@page{
          size:A4;
          margin:0;
          size:portrait;
          -webkit-print-color-adjust:exact !important
      }*/
    </style>

    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <section class="content-header">
    <h1>
       GOOD RECEIVED NOTE
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">View GRN</li>
      </ol>
    </section>
    <br />
    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <div class="content" style="position: relative; background: #fff; overflow: hidden; border: 1px solid #f4f4f4; padding: 20px; margin: 10px 25px;" id="divPrintPo">
                    <!-- Main content -->


                    <div class="row">
                        <div class="col-xs-12">
                            <h2 class="page-header" style="text-align: center;">
                                <i class="fa fa-envelope"></i>Good Return Note
                            </h2>
                        </div>
                        <!-- /.col -->
                    </div>


                    <div class="row invoice-info">

                        <div class="row">
                            <div class="col-xs-4">
                                <strong>SUPPLIER: </strong>
                                <br>
                                <asp:Label runat="server" ID="lblsupplierName"></asp:Label><br>
                                <asp:Label runat="server" ID="lblSupplierAddress"></asp:Label><br>
                                <asp:Label runat="server" ID="lblSupplierContact"></asp:Label>
                            </div>
                            <div class="col-xs-4">
                                <strong>DELIVERING WAREHOUSE: </strong>
                                <br>
                                <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label>

                                <br>
                                <strong>STORE KEEPER: </strong>
                                <asp:Label runat="server" ID="lblStoreKeeper"></asp:Label><br>
                                <br>
                            </div>
                            <div class="col-xs-4">
                                <strong>GRN CODE: </strong>
                                <asp:Label runat="server" ID="lblGrnCode"></asp:Label><br>
                                <strong>PO CODE: </strong>
                                <asp:Label runat="server" ID="lblPOCode"></asp:Label><br>
                                <strong>BASED PR: </strong>
                                <asp:Label runat="server" ID="lblPrCode"></asp:Label><br>
                                <strong>QUOTATION FOR: </strong>
                                <asp:Label runat="server" ID="lblquotationfor"></asp:Label><br>
                                <strong>RECEIVED DATE: </strong>
                                <asp:Label runat="server" ID="lblReceiveddate"></asp:Label><br>
                                <strong>PAYMENT TYPE: </strong>
                                <asp:Label runat="server" ID="lblPaymenttype"></asp:Label><br>
                                <strong>APPROVAL STATUS: </strong>
                                <asp:Label runat="server" ID="lblPending" CssClass="label label-warning" Visible="false" Text="Pending"></asp:Label><br>
                                <asp:Label runat="server" ID="lblApproved" CssClass="label label-success" Visible="false" Text="Approved"></asp:Label><br>
                                <asp:Label runat="server" ID="lblRejected" CssClass="label label-danger" Visible="false" Text="Rejected"></asp:Label><br>
                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="co-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive"
                                        AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
                                        <Columns>
                                            <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <%--  <asp:BoundField DataField="QuotationId" HeaderText="Quotation Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>--%>
                                            <asp:BoundField DataField="ItemId" HeaderText="Item No" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="ReferenceNo" HeaderText="Item Code" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                            <asp:BoundField DataField="SupplierMentionedItemName" HeaderText="Supplier Mentioned Item Name" NullDisplayText="Not Found" />
                                            <asp:BoundField DataField="MeasurementShortName" HeaderText="Measurement" NullDisplayText="Not Found" />
                                            <%--<asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerHeaderText" />--%>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <%--<asp:TextBox ID="txtQty" runat="server" Text='<%# Eval("Quantity").ToString()%>'  CssClass="txtQuantityCl"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Eval("Quantity").ToString() %>' CssClass="txtQuantityCl"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FreeQty" HeaderText="Free Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerHeaderText" />
                                            <asp:TemplateField HeaderText="Expiry Date">
                                                <ItemTemplate>
                                                    <%# (DateTime)Eval("ExpiryDate") == DateTime.MinValue ? "Not Defined" : string.Format("{0:MM-dd-yyyy}", (DateTime)Eval("ExpiryDate")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                            <%--<asp:BoundField DataField="SubTotal" HeaderText="Sub Total" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                            <asp:BoundField DataField="VatAmount" HeaderText="Vat Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                            --%>
                                            <asp:TemplateField HeaderText="Sub Total" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%--<asp:TextBox ID="txtSubTotal" runat="server" Text='<%# Eval("SubTotal", "{0:N2}").ToString()%>'  CssClass="txtSubTotalCl" Enabled="false"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtSubTotal" runat="server" Text='<%# Eval("SubTotal", "{0:n2}").ToString() %>' CssClass="txtSubTotalCl" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vat" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%--<asp:TextBox ID="txtVat" runat="server" Text='<%# Eval("VatAmount","{0:N2}").ToString()%>'  CssClass="txtVatCl" Enabled="false"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtVat" runat="server" Text='<%# Eval("VatAmount","{0:N2}").ToString() %>' CssClass="txtVatCl" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <%-- <asp:TextBox ID="txtTotal" runat="server" Text='<%# Eval("TotalAmount","{0:N2}").ToString()%>'  CssClass="txtTotalCl" Enabled="false"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtTotal" runat="server" Text='<%# Eval("TotalAmount").ToString() %>' CssClass="txtTotalCl" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                            <%--<asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />--%>
                                            <asp:BoundField DataField="HasVat" HeaderText="Has VAT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="GrndId" HeaderText="GrndId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="GrnId" HeaderText="GrnId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="MeasurementId" HeaderText="Measurement Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="WarehouseId" HeaderText="WarehouseId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:TemplateField HeaderText="Sub Total">
                                                <ItemTemplate>
                                                    <%--<asp:TextBox ID="txtSubTotal" runat="server" Text='<%# Eval("SubTotal", "{0:N2}").ToString()%>'  CssClass="txtSubTotalCl" Enabled="false"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtSubTotalNew" runat="server" Text='<%# Eval("SubTotal" , "{0:N2}").ToString() %>' CssClass="txtSubTotalClNew"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vat">
                                                <ItemTemplate>
                                                    <%--<asp:TextBox ID="txtVat" runat="server" Text='<%# Eval("VatAmount","{0:N2}").ToString()%>'  CssClass="txtVatCl" Enabled="false"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtVatNew" runat="server" Text='<%# Eval("VatAmount","{0:N2}").ToString() %>' CssClass="txtVatClNew"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <%-- <asp:TextBox ID="txtTotal" runat="server" Text='<%# Eval("TotalAmount","{0:N2}").ToString()%>'  CssClass="txtTotalCl" Enabled="false"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtTotalNew" runat="server" Text='<%# Eval("TotalAmount","{0:N2}").ToString() %>' CssClass="txtTotalClNew"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="StockMaintainingType" HeaderText="StockMaintainingType" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="IsApproved" HeaderText="IsApproved" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="AvailableMasterStock" HeaderText="Available Master Stock" />
                                            <asp:BoundField DataField="AvailableDetailStock" HeaderText="Available Detail Stock" />
                                            <asp:BoundField DataField="podId" HeaderText="PodId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                            <%--<asp:BoundField DataField="ItemPrice" HeaderText="Unit Price" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                            --%>

                                            <%-- <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%--<asp:Button ID="btnReturn" runat="server" CssClass="btn btn-warning btnReturn" Text="Return Stock" OnClientClick='<%#"Return(event,"+Eval("GrndId").ToString()+", "+Eval("GrnId").ToString()+", "+Eval("Quantity").ToString()+", "+Eval("MeasurementId").ToString()+", "+Eval("WarehouseId").ToString()+", "+Eval("SubTotal").ToString()+", "+Eval("VatAmount").ToString()+", "+Eval("TotalAmount").ToString()+", , "+Eval("ItemId").ToString()+", , "+Eval("IsApproved").ToString()+", , "+Eval("StockMaintainingType").ToString()+") " %>'></asp:Button>--%>
                                            <%-- <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnReturn" Text="Return Stock" OnClick="btnReturnStock_Click"></asp:Button>
                                              
                                                    
                                                 </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">

                                    <div class="form-group">
                                        <label>Supplier Return Option : </label>
                                        <br>
                                        <asp:DropDownList ID="ddlSupplierOption" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">--Select Option--</asp:ListItem>
                                            <asp:ListItem Value="1">Return Item</asp:ListItem>
                                            <asp:ListItem Value="2">Return Cash</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Panel ID="Remarks" runat="server">
                                        <div class="form-group">
                                            <label>Remarks : </label>
                                            <br>
                                            <asp:TextBox TextMode="MultiLine" Rows="6" runat="server" ID="txtRemarks" Width="100%"></asp:TextBox>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-4 col-md-push-2" style="text-align: right; font-size: 16px;">
                                    <div class="col-xs-6">
                                        <label class="summary-title">Sub Total</label>
                                    </div>
                                    <div class="col-xs-6">
                                        <label id="lblSubTotal" class="summary-data">0.00</label>
                                    </div>
                                    <%--<div class="col-xs-6">
                                                <label class="summary-title" >Total NBT</label>
                                            </div>
                                            <div class="col-xs-6">
                                                <label id="lblNbtTotal" class="summary-data" >0.00</label>
                                            </div>--%>
                                    <div class="col-xs-6">
                                        <label class="summary-title">Total VAT</label>
                                    </div>
                                    <div class="col-xs-6">
                                        <%--<label id="lblVatTotal" class="summary-data">0.00</label>--%>
                                        <label id="lblVatTotalNew" class="summary-data">0.00</label>
                                    </div>
                                    <div class="col-xs-6">
                                        <label class="summary-title">Net Total</label>
                                    </div>
                                    <div class="col-xs-6">
                                        <%--                                                <label id="lblNetTotal" class="summary-data">0.00</label>--%>
                                        <label id="lblNetTotalNew" class="summary-data">0.00</label>
                                    </div>
                                </div>

                            </div>


                        </div>

                        <%-- <hr />
                        <div class="row">
                            <div class="col-xs-4 col-sm-4  text-center">

                                <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height: 50px;" runat="server" /><br />
                                <asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />
                                <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                <b>PO Created By</b>
                            </div>

                            <div class="col-xs-4 col-sm-4  text-center">
                                <asp:Panel ID="pnlApprovedBy" runat="server">
                                    <asp:Image ID="imgApprovedBySignature" Style="width: 100px; height: 50px;" runat="server" /><br />
                                    <asp:Label runat="server" ID="lblApprovedByName"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblApprovedDate"></asp:Label><br />
                                    <b>PO Approved By</b>
                                </asp:Panel>
                            </div>


                            <div class="col-xs-4 col-sm-4">
                                <asp:Panel ID="pnlRemark" runat="server">
                                    <div style="height: 50px; width: 100px;"></div>
                                    <strong>Remarks: </strong>
                                    <asp:Label runat="server" ID="lblgrnComment" Text=""></asp:Label>
                                </asp:Panel>
                            </div>
                        </div>--%>

                        <hr />
                        <div class="row no-print">
                            <div class="col-xs-12">
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning pull-right" Text="Return Stock" OnClientClick="Return();"></asp:Button>

                                <%--<asp:Button runat="server" CssClass="btn btn-danger btnRejectCl" Text="Reject GRN" />--%>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:HiddenField ID="hdnRemarks" runat="server" />
                <asp:HiddenField ID="hdnCanApprove" runat="server" />
                <asp:Button ID="btnReturnStock" runat="server" OnClick="btnReturnStock_Click" CssClass="hidden" />
                <asp:HiddenField ID="hdnVatRate" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNbtRate1" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNbtRate2" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnCalculate" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnQty" runat="server" />

                <asp:HiddenField runat="server" ID="hdnSubTotal" />
                <asp:HiddenField runat="server" ID="hdnNbt" />
                <asp:HiddenField runat="server" ID="hdnVat" />
                <asp:HiddenField runat="server" ID="hdnNetTotal" />
                <asp:HiddenField runat="server" ID="hdnGrndId" />
                <asp:HiddenField runat="server" ID="hdnGrnId" />
                <asp:HiddenField runat="server" ID="hdnWarehouseId" />
                <asp:HiddenField runat="server" ID="hdnMeasurementId" />
                <asp:HiddenField runat="server" ID="hdnItemId" />
                <asp:HiddenField runat="server" ID="hdnIsApproved" />
                <asp:HiddenField runat="server" ID="hdnStockMaintainingType" />

            </ContentTemplate>
            <Triggers>

                <asp:PostBackTrigger ControlID="btnReturnStock" />
            </Triggers>
        </asp:UpdatePanel>
    </form>


    <script type="text/javascript">

        Sys.Application.add_load(function () {

            $(function () {
                var TotSubSum = 0;
                var TotVatSum = 0;
                var TotNetTotSum = 0;

                var grid = document.getElementById("<%=gvPurchaseOrderItems.ClientID%>");

                for (var i = 1; i < grid.rows.length; i++) {
                    var TotSub = $(grid.rows[i]).find('.txtSubTotalCl').val().replace(/,/g, '');
                    var TotVat = $(grid.rows[i]).find('.txtVatCl').val().replace(/,/g, '');
                    var TotNetTot = $(grid.rows[i]).find('.txtTotalCl').val().replace(/,/g, '');

                    if (TotSub == "") {
                        TotSub = "0.00";
                    }
                    if (TotVat == "") {
                        TotVat = "0.00";
                    }
                    if (TotNetTot == "") {
                        TotNetTot = "0.00";
                    }
                    TotSubSum = parseFloat(TotSubSum) + parseFloat(TotSub);
                    TotVatSum = parseFloat(TotVatSum) + parseFloat(TotVat);
                    TotNetTotSum = parseFloat(TotNetTotSum) + parseFloat(TotNetTot);

                }

                $('#lblVatTotalNew').html(TotVatSum.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
                $('#lblSubTotal').html(TotSubSum.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
                $('#lblNetTotalNew').html(TotNetTotSum.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));

            });
        });

        function Return() {

            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to <b>return</b> this Stock?</br></br>",
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

                    $('#ContentSection_btnReturnStock').click();
                }
            });
        }


        $(function () {

            $('.txtQuantityCl').on({
                keyup: function () {
                    calculate(this);
                },
                change: function () {
                    calculate(this);
                }
            })

            function calculate(elmt) {

                var row = $(elmt).closest('tr').find('td');

                var qty = 0;
                if ($(elmt).val() != "") {
                    qty = parseFloat($(elmt).val());
                }
                var MasterStock = $(row).eq(24).html();
                var BatchStock = $(row).eq(25).html();
                var GrnQuantity = $(row).eq(21).html();

                if ($(row).eq(23).html() == "1") {

                    if ($(row).eq(22).html() == "1") {
                        if (qty > MasterStock) {
                            $(row).eq(6).find('.txtQuantityCl').val(MasterStock);
                        }
                    }

                    else {
                        if (qty > BatchStock) {
                            $(row).eq(6).find('.txtQuantityCl').val(BatchStock);
                        }
                    }
                }

                else {
                    if (qty > GrnQuantity) {
                        $(row).eq(6).find('.txtQuantityCl').val(GrnQuantity);
                    }
                }


                var itemPrice = parseFloat($(row).eq(9).html().replace(',', ''))

                var subTotal = qty * itemPrice;

                var nbt = 0;
                var vat = 0;
                var vatRate = $('#ContentSection_hdnVatRate').val();
                var nbtRate1 = $('#ContentSection_hdnNbtRate1').val();
                var nbtRate2 = $('#ContentSection_hdnNbtRate2').val();


                if ($(row).eq(13).html() != "0") {

                    vat = parseFloat((subTotal) * vatRate);
                }

                $(row).eq(10).find('.txtSubTotalCl').val(subTotal.toFixed(2));
                $(row).eq(11).find('.txtVatCl').val(vat.toFixed(2));
                $(row).eq(12).find('.txtTotalCl').val((subTotal + vat).toFixed(2));

                $(row).eq(18).find('.txtSubTotalClNew').val(subTotal.toFixed(2));
                $(row).eq(19).find('.txtVatClNew').val(vat.toFixed(2));
                $(row).eq(20).find('.txtTotalClNew').val((subTotal + vat).toFixed(2));

                $('#ContentSection_hdnSubTotal').val(subTotal.toFixed(2));
                $('#ContentSection_hdnVat').val(vat.toFixed(2));
                $('#ContentSection_hdnNetTotal').val((subTotal + vat).toFixed(2));
                $('#ContentSection_hdnQty').val(qty.toFixed(2));
                $('#ContentSection_hdnCalculate').val("1");

                var TotSubSum = 0;
                var TotVatSum = 0;
                var TotNetTotSum = 0;

                var grid = document.getElementById("<%=gvPurchaseOrderItems.ClientID%>");

                for (var i = 1; i < grid.rows.length; i++) {
                    var TotSub = $(grid.rows[i]).find('.txtSubTotalCl').val().replace(/,/g, '');
                    var TotVat = $(grid.rows[i]).find('.txtVatCl').val().replace(/,/g, '');
                    var TotNetTot = $(grid.rows[i]).find('.txtTotalCl').val().replace(/,/g, '');

                    if (TotSub == "") {
                        TotSub = "0.00";
                    }
                    if (TotVat == "") {
                        TotVat = "0.00";
                    }
                    if (TotNetTot == "") {
                        TotNetTot = "0.00";
                    }
                    TotSubSum = parseFloat(TotSubSum) + parseFloat(TotSub);
                    TotVatSum = parseFloat(TotVatSum) + parseFloat(TotVat);
                    TotNetTotSum = parseFloat(TotNetTotSum) + parseFloat(TotNetTot);

                }

                $('#lblVatTotalNew').html(TotVatSum.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
                $('#lblSubTotal').html(TotSubSum.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
                $('#lblNetTotalNew').html(TotNetTotSum.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));

            }
        });
    </script>
</asp:Content>
