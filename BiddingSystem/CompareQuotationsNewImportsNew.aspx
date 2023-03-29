<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompareQuotationsNewImportsNew.aspx.cs" Inherits="BiddingSystem.CompareQuotationsNewImportsNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style type="text/css">
        .ChildGrid > tbody > tr > td:not(table) {
            background-color: #f5f2f2 !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
            border-bottom: 1px solid #d4d2d2;
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

        .ChildGridTwo > tbody > tr > td:not(table) {
            background-color: #f5f5f5 !important;
            color: black;
            border-bottom: 1px solid #d4d2d2;
        }


        .ChildGridTwo > tbody > tr {
            border: 1px solid #d4d2d2;
        }

        .ChildGridTwo th {
            color: white;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #525252 !important;
        }

        .ChildGridThree td {
            text-align: left;
        }

        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        #ContentSection_gvQuotations > tbody > tr:nth-child(2n+0) {
            background-color: #fbfafa !important;
            text-align: center;
        }

        #ContentSection_gvQuotations th {
            text-align: center;
        }

        #ContentSection_gvBids th, #ContentSection_gvBids td {
            text-align: center;
        }

        .modal {
            overflow: auto !important;
            position: fixed;
        }

        .TabluationBtn {
            font-size: 12px !important;
        }
    </style>
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

    <link href="AdminResources/css/Wizard.css?version=<%DateTime.Now.ToString(); %>" rel="stylesheet" />


    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />



    <script src="AdminResources/js/select2.full.min.js"></script>
    <%--<script src="https://cdn.ckeditor.com/4.11.4/basic/ckeditor.js"></script>--%>
    <script src="AdminResources/js/ckeditor.js"></script>
    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>

    <section class="content-header">
        <h1>
           Compare Quotation / Select Bid - Imports
            <small></small>
                                                                                       
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Bid Comparison - Imports </li>
        </ol>
    </section>
    <br />


    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>


                <!-- Start : Quotations Modal -->
                <div id="mdlQuotations" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 95%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">

                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Submitted Quotations</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <div class="col-xs-12">
                                <div class="process">
                                    <div class="process-row nav nav-tabs">
                                        <div class="process-step">
                                            <button type="button" id="btnBasic" class="btn btn-info btn-circle" data-toggle="tab"
                                                href="#ItemWise" onclick="window.scrollTo({ top: 0, behavior: 'smooth' });">
                                                <i class="fa fa-info fa-3x"></i>
                                            </button>
                                            <p><b>ITEM WISE</b></p>
                                        </div>
                                        <div class="process-step">
                                            <button type="button" id="btnItem" class="btn btn-default btn-circle " data-toggle="tab"
                                                href="#SupplierWise" onclick="window.scrollTo({ top: 0, behavior: 'smooth' });">
                                                <i class="fa fa-list-ul fa-3x"></i>
                                            </button>
                                            <p><b>SUPPLIER WISE</b></p>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="tab-content">
                                <div id="ItemWise" class="tab-pane fade active in">
                                    <div class="panel panel-default">
                                        <!-- Start : Modal Body -->
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <!-- Start : Quotation Table -->
                                                    <div style="color: black; overflow: scroll;">

                                                        <asp:GridView ID="gvItems" runat="server"
                                                            CssClass="table gvItems"
                                                            GridLines="None" AutoGenerateColumns="false" ShowHeader="false" DataKeyNames="BiddingItemId" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" OnRowDataBound="gvItems_RowDataBound">
                                                            <Columns>

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>

                                                                        <tr style="color: White; background-color: #3C8DBC;">
                                                                            <td scope="col">#</td>
                                                                            <td class="hidden" scope="col">BidItemId</td>
                                                                            <td class="hidden" scope="col">BidItemId</td>
                                                                            <td class="hidden" scope="col">PRDId</td>
                                                                            <td class="hidden" scope="col">Item Id</td>
                                                                            <td scope="col">Item Name</td>
                                                                            <td scope="col">Quantity</td>
                                                                            <td scope="col">Estimated Price</td>
                                                                            <td scope="col">Quotations Count</td>
                                                                            <td class="hidden" scope="col">LastSupplierId</td>
                                                                            <td scope="col">Last Purchased Supplier</td>
                                                                            <td scope="col">Last Purchased Price</td>
                                                                            <td scope="col">Purchase </td>
                                                                            <td scope="col"></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true">
                                                                    <ItemTemplate>
                                                                        <span style="font: bold;">
                                                                            <%#Container.DataItemIndex + 1%>
                                                                        </span>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="BiddingItemId"
                                                                    HeaderText="BidItemId"
                                                                    HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="BidId"
                                                                    HeaderText="BidItemId"
                                                                    HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="PrdId"
                                                                    HeaderText="PRDId" HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="ItemId"
                                                                    HeaderText="Item Id"
                                                                    HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="ItemName"
                                                                    HeaderText="Item Name" />
                                                                <asp:TemplateField HeaderText="Quantity">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" Text='<%# decimal.Parse(Eval("Qty").ToString()).ToString() + " " + Eval("UnitShortName").ToString() %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="EstimatedPrice"
                                                                    HeaderText="Estimated Price" DataFormatString="{0:N2}" />
                                                                <asp:BoundField DataField="QuotationCount"
                                                                    HeaderText="Quotations Count" />
                                                                <asp:BoundField DataField="LastSupplierId"
                                                                    HeaderText="LastSupplierId" HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="LastSupplierName" NullDisplayText="Not Found"
                                                                    HeaderText="Last Purchased Supplier" />
                                                                <asp:BoundField DataField="LPurchasedPrice" NullDisplayText="Not Found"
                                                                    HeaderText="Last Purchased Price" DataFormatString="{0:N2}" />

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>

                                                                        <asp:Button CssClass="btn-xs btn-info btn-styled" runat="server"
                                                                            ID="btnPurchased" Text="History"
                                                                            Style="margin-top: 3px; width: 100px;" OnClick="btnPurchased_Click"></asp:Button>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td colspan="100%">
                                                                                <asp:Panel ID="pnlQuotationItems" runat="server" Style="margin-left: 20px; overflow-x:scroll; width: 95% ">
                                                                                    <asp:GridView ID="gvQuotationItems" runat="server"
                                                                                        CssClass="table table-responsive"
                                                                                        GridLines="None" AutoGenerateColumns="false"
                                                                                        Caption="Quotations" EmptyDataText="No Quotations Found" RowStyle-BackColor="#f5f2f2" HeaderStyle-BackColor="#525252" HeaderStyle-ForeColor="White">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true">
                                                                                                <ItemTemplate>
                                                                                                    <span style="font: bold;">
                                                                                                        <%#Container.DataItemIndex + 1%>
                                                                                                    </span>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="QuotationItemId" HeaderText="QuotaionItemId"
                                                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                            <asp:BoundField DataField="QuotationId" HeaderText="QuotationId"
                                                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                            <asp:BoundField DataField="BiddingItemId" HeaderText="BidItemId"
                                                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                            <asp:BoundField DataField="SupplierId" HeaderText="Supplier"
                                                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                            <asp:BoundField DataField="SupplierName" HeaderText="Supplier" NullDisplayText="Unavailable" />
                                                                                            <asp:BoundField DataField="ReferenceNo" HeaderText="Reference Code" NullDisplayText="Unavailable" />
                                                                                            <asp:BoundField DataField="SupplierMentionedItemName" NullDisplayText="Not Found" HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" 
                                                                                                HeaderText="Supplier Mentioned Name" />
                                                                                            <asp:BoundField DataField="SupplierAgent" HeaderText="Supplier Agent" NullDisplayText="Unavailable" />
                                                                                            <asp:BoundField DataField="Country" HeaderText="Country" NullDisplayText="Unavailable" />
<%--                                                                                            <asp:BoundField DataField="CurrencyType" HeaderText="Currency Type" NullDisplayText="Unavailable" />--%>
                                                                                            <asp:TemplateField HeaderText="Currency Type">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" Text='<%# Eval("Term").ToString() == "Local" ? "LKR" :Eval("CurrencyType") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                            <asp:BoundField DataField="Brand" HeaderText="Brand" NullDisplayText="Unavailable" />
                                                                                            <asp:BoundField DataField="Mill" HeaderText="Mill" NullDisplayText="Unavailable" />
                                                                                            <asp:BoundField DataField="Remark" HeaderText="Remark" NullDisplayText="Unavailable" />
                                                                                            <asp:BoundField DataField="History" HeaderText="History" NullDisplayText="Unavailable" />
                                                                                            <asp:BoundField DataField="Vlidity" HeaderText="Vlidity" DataFormatString="{0: dd-MMM-yyyy}" />
                                                                                            <asp:BoundField DataField="EstDelivery" HeaderText="Est Delivery" NullDisplayText="Unavailable" />
                                                                                            <%--<asp:BoundField DataField="ClearingCost" HeaderText="Clearing Cost" NullDisplayText="Unavailable" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                            <asp:BoundField DataField="OtherCost" HeaderText="Other Cost" NullDisplayText="Unavailable" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>--%>
                                                                                            <asp:BoundField DataField="ImpCIF" NullDisplayText="Not Found" HeaderText="Quoted Unit Price(Foreign)" />
                                                                                            <asp:BoundField DataField="UnitPrice(lkr)" NullDisplayText="Not Found" HeaderText="Quoted Unit Price(lkr)" />
                                                                                            <asp:BoundField DataField="Term" HeaderText="Term" NullDisplayText="Unavailable" />

                                                                                            <asp:TemplateField HeaderText="Evaluated Price">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblUnitPriceView" CssClass="lblUnitPriceView" runat="server" Text='<%# Eval("UnitPriceView","{0:N2}").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Requesting qty" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtRequestingQtynew" CssClass="txtRequestingQtynew" runat="server" Text='<%# Eval("ReqQty").ToString() %>' Width="80px" onkeyup="Calculation(this)" Type="number" min="0.00" step="any"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Sub-Total">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblSubTotal" CssClass="lblSubTotal" Text='<%# Eval("SubTotal","{0:N2}").ToString() %>' runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="NBT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblNbt" CssClass="lblNbt" runat="server" Text='<%# Eval("NbtAmount","{0:N2}").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="VAT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblVat" CssClass="lblVat" runat="server" Text='<%# Eval("VatAmount").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Net-Total">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblNetTotal" CssClass="lblNetTotal" runat="server" Text='<%# Eval("NetTotal","{0:N2}").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                             <asp:BoundField DataField="PaymentModeId" NullDisplayText="Not Found" HeaderText="Payment Type"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                             <asp:TemplateField HeaderText="Payment Type" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label runat="server" ID="lblPaymentModeId" Text='<%# Eval("PaymentModeId").ToString() == "1" ? "Advance" : Eval("PaymentModeId").ToString() == "2" ? "On Arrival" : Eval("PaymentModeId").ToString() == "3" ? "LC at sight" : Eval("PaymentModeId").ToString() == "4" ? "L/C usance" : Eval("PaymentModeId").ToString() == "5" ? "D/A" : Eval("PaymentModeId").ToString() == "6" ? "D/P" : "0" %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                             <asp:BoundField DataField="NoOfDaysPaymentMode" NullDisplayText="Not Found" HeaderText="Day No" />
                                                                                            <asp:TemplateField HeaderText="Attachments" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button CssClass="btn btn-info btn-styled btnViewAttachmentsClassA btn-xs" runat="server" ID="btnAttachMents"
                                                                                                        Text="View" OnClick="btnAttachMents_Click"></asp:Button>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>


                                                                                            <%-- <asp:TemplateField HeaderText="Supplier  Import Details" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                                <ItemTemplate>

                                                                                                    <asp:Button CssClass="btn-xs btn-info btn-styled " runat="server"
                                                                                                ID="btnImportDet" Text="View"
                                                                                                Style="margin-top: 3px; width: 100px;" OnClick="btnImportDet_Click"></asp:Button>
                                                                                                   
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>
                                                                                            <asp:TemplateField HeaderText="Has VAT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblHasVat" runat="server" CssClass="lblHasVat" Text='<%# Eval("HasVat").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Has NBT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblHasNbt" runat="server" CssClass="lblHasNbt" Text='<%# Eval("HasNbt").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="NBT Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblNBTType" runat="server" CssClass="lblNBTType" Text='<%# Eval("NbtType").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Sub-Total" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtSubTotal" CssClass="txtSubTotal" runat="server" Text=""></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="NBT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtNbt" CssClass="txtNbt" runat="server" Text=""></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="VAT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtVat" CssClass="txtVat" runat="server" Text=""></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Net-Total" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtNetTotal" CssClass="txtNetTotal" runat="server" Text=""></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Requested Total" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtReqTotQty" CssClass="txtReqTotQty" runat="server" Text='<%# Eval("RequestedTotalQty").ToString() %>'></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Total VAT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtTotVAT" CssClass="txtTotVAT" runat="server" Text='<%# Eval("VatAmount").ToString() %>'></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Total NBT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtTotNBT" CssClass="txtTotNBT" runat="server" Text='<%# Eval("NbtAmount").ToString() %>'></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Is Selected" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtSelectedQ" CssClass="txtSelectedQ" runat="server" Text="0"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="TablationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblTablationId" runat="server" CssClass="lblTablationId" Text='<%# Eval("TabulationId").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblItemId" runat="server" CssClass="lblItemId" Text='<%# Eval("ItemId").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="IsSelectedTB" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblIsSelectedTB" runat="server" CssClass="lblIsSelectedTB" Text='<%# Eval("IsSelectedTB").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="SelectedQuantity" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblSelectedQuantity" runat="server" CssClass="lblSelectedQuantity" Text='<%# Eval("SelectedQuantity").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Term" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblTerm" runat="server" CssClass="lblTerm" Text='<%# Eval("TermId").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Evaluated Price" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblUnitPrice" CssClass="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice").ToString() %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>


                                                                                            <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="280px" ItemStyle-Width="280px">
                                                                                                <ItemTemplate>

                                                                                                    <%--<button type="button" class="btn btn-primary btn-styled  btn-xs pull-left btnSelect" onclick="SelectBidClick(this ,'Items','Imports');" id="btnSelect">
                                                                                                        Select
                                                                                                    </button>
                                                                                                         <asp:ImageButton ID="btnDelete" CssClass="deleteItem pull-right hidden" ImageUrl="~/images/dlt.png" ToolTip="Delete" Style="width: 20px; height: 20px;"
                                                                                                runat="server"  OnClientClick="RemoveBidItemClick(this ,'Items','Imports');"/>--%>

                                                                                                    <asp:Button ID="btnSelect" runat="server" Text="Select" class="btn btn-primary btn-styled  btn-xs pull-left btnSelect " OnClientClick="SelectBidClick(this ,'Items','Imports');" Visible='<%# Eval("IsSelectedTB").ToString() == "1" ? false : true %>' />
                                                                                                    <asp:Label ID="lblSelected" runat="server" Text="SELECTED" Font-Bold="true" ForeColor="SeaGreen" Visible='<%# Eval("IsSelectedTB").ToString() == "1" ? true : false %>'></asp:Label><br>
                                                                                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" class="btn btn-danger btn-styled  btn-xs pull-left btnDelete " OnClientClick="RemoveBidItemClick(this ,'Items','Imports');" Visible='<%# Eval("IsSelectedTB").ToString() == "1" ? true : false %>' />


                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                            </Columns>
                                                        </asp:GridView>


                                                    </div>
                                                    <!-- End : Quotation Table -->
                                                </div>
                                            </div>
                                        </div>
                                        <!-- End : Modal Body -->


                                        <div class="box-footer">

                                            <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="pull-right hidden" style="margin-right: 10px; max-height: 40px;" />


                                            <button type="button" class="btn btn-info btn-styled   pull-right" onclick="CloseModelQuotations();">Back</button>
                                            <%-- <button type="button" class="btn btn-info btn-styled   pull-left" > Print </button>--%>
                                            <asp:Button class="btn btn-info btn-styled" runat="server" ID="btnPrintNew" Text="Print" OnClick="btnPrint_Click"
                                                Style="margin-top: 3px; width: 100px; margin-right: 5px;"></asp:Button>
                                            <button type="button" class="btn  btn-styled btn-success   pull-right " style="width: 150px;" onclick="FinalizeTabulationClick('Imports');">Finalize Tabulation </button>
                                        </div>
                                    </div>
                                </div>
                                <div id="SupplierWise" class="tab-pane fade ">
                                    <!-- Start : Modal Body -->
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-12 ">
                                                <!-- Start : Quotation Table -->
                                                <div style="color: black; overflow: scroll;">

                                                    <asp:GridView ID="gvQuotationItemsSup" runat="server"
                                                        CssClass="table table-responsive"
                                                        GridLines="None" AutoGenerateColumns="false"
                                                        Caption="Supplier " DataKeyNames="QuotationId" ShowHeader="false" EmptyDataText="No Quotations Found" RowStyle-BackColor="#f5f2f2" HeaderStyle-BackColor="#525252" HeaderStyle-ForeColor="White" OnRowDataBound="gvQuotationItemsSup_RowDataBound">
                                                        <Columns>

                                                            <asp:TemplateField>
                                                                <ItemTemplate>

                                                                    <tr style="color: White; background-color: #525252">
                                                                        <td scope="col">#</td>
                                                                        <td class="hidden" scope="col">QuotationItemId</td>
                                                                        <td class="hidden" scope="col">QuotationId</td>
                                                                        <td class="hidden" scope="col">BiddingItemId</td>
                                                                        <td class="hidden" scope="col">SupplierId</td>
                                                                        <td scope="col">Supplier</td>
                                                                        <td class="hidden" scope="col">Quotations Count</td>
                                                                        <td class="hidden" scope="col">Complies Specs </td>
                                                                        <td scope="col">Attachments</td>
                                                                        <td class="hidden" scope="col">Supplier Impoer Details</td>
                                                                        <td class="hidden" scope="col">Requesting qty</td>
                                                                        <td class="hidden" scope="col">QuotationItemId</td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <span style="font: bold;">
                                                                        <%#Container.DataItemIndex + 1%>
                                                                    </span>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                          <%--  <asp:BoundField DataField="QuotationItemId" HeaderText="QuotaionItemId"
                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                                            <asp:BoundField DataField="QuotationId" HeaderText="QuotationId"
                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                            <%--<asp:BoundField DataField="BiddingItemId" HeaderText="BidItemId"
                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                                            <asp:BoundField DataField="SupplierId" HeaderText="Supplier"
                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="SupplierName" HeaderText="Supplier" NullDisplayText="Unavailable" />
                                                            <%--<asp:BoundField DataField="QuotationCount" HeaderText="Quotations Count" NullDisplayText="Unavailable" />--%>
                                                            <%--<asp:BoundField DataField="SpecComply" HeaderText="Complies Specs" />--%>


                                                            <asp:TemplateField HeaderText="Attachments" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                <ItemTemplate>
                                                                    <asp:Button CssClass="btn btn-info btn-styled btnViewAttachmentsClassA btn-xs " runat="server" ID="btnAttachMents"
                                                                        Text="View" OnClick="btnAttachMentsSup_Click"></asp:Button>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <%--<asp:TemplateField HeaderText="Supplier Import Details" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                                <ItemTemplate>

                                                                                                    <asp:Button CssClass="btn-xs btn-info btn-styled " runat="server"
                                                                                                        ID="btnImportDetSu" Text="View"
                                                                                                        Style="margin-top: 3px; width: 100px;" OnClick="btnImportDetSu_Click"></asp:Button>

                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Requesting qty" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRequestingQty" CssClass="txtRequestingQty" runat="server" Width="80px" onkeyup="Calculation(this)" Type="number" min="0.00" step="any"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtQuotationIdMain" CssClass="txtQuotationIdMain" runat="server" Text='<%# Eval("QuotationId").ToString() %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td colspan="100%">
                                                                            <asp:Panel ID="pnlSupplier" runat="server" Style="margin-left: 40px; overflow-x:scroll; width: 90% ">
                                                                                <asp:GridView ID="gvItemSupllier" runat="server"
                                                                                    CssClass="table gvItems" Caption="Items "
                                                                                    GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                                                    <Columns>

                                                                                        <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true">
                                                                                            <ItemTemplate>
                                                                                                <span style="font: bold;">
                                                                                                    <%#Container.DataItemIndex + 1%>
                                                                                                </span>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="BiddingItemId"
                                                                                            HeaderText="BidItemId"
                                                                                            HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                        <asp:BoundField DataField="BidId"
                                                                                            HeaderText="BidItemId"
                                                                                            HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                        <asp:BoundField DataField="PrdId"
                                                                                            HeaderText="PRDId" HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                        <asp:BoundField DataField="ItemId"
                                                                                            HeaderText="Item Id"
                                                                                            HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                        <asp:BoundField DataField="ItemName"
                                                                                            HeaderText="Item Name" />
                                                                                        <asp:BoundField DataField="SupplierMentionedItemName" NullDisplayText="Not Found" HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" 
                                                                                            HeaderText="Supplier Mentioned Name" />
                                                                                        <asp:BoundField DataField="LastSupplierId"
                                                                                            HeaderText="LastSupplierId" HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                        <asp:BoundField DataField="LastSupplierName" NullDisplayText="Not Found"
                                                                                            HeaderText="Last Purchased Supplier" />
                                                                                        <asp:BoundField DataField="LPurchasedPrice" NullDisplayText="Not Found"
                                                                                            HeaderText="Last Purchased Price" DataFormatString="{0:N2}" />
                                                                                        <asp:TemplateField HeaderText="Quantity">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" Text='<%# decimal.Parse(Eval("Qty", "{0:N2}").ToString()).ToString() + " " + Eval("UnitShortName").ToString() %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:BoundField DataField="EstimatedPrice"
                                                                                            HeaderText="Estimated Price" DataFormatString="{0:N2}" />


                                                                                        <asp:BoundField DataField="Country"
                                                                                            HeaderText="Country" />
                                                                                        <%--<asp:BoundField DataField="CurrencyType"
                                                                                            HeaderText="Currency Type" />--%>
                                                                                         <asp:TemplateField HeaderText="Currency Type">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" Text='<%# Eval("TermName") == null ? "Not Found" :Eval("TermName").ToString() == "Local" ? "LKR" :Eval("CurrencyType") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="SupplierAgent"
                                                                                            HeaderText="Supplier Agent" />
                                                                                        <asp:BoundField DataField="ImpBrand"
                                                                                            HeaderText="Brand" />
                                                                                        <asp:BoundField DataField="Mill" 
                                                                                            HeaderText="Mill" />
                                                                                        <asp:BoundField DataField="ImpHistory" 
                                                                                            HeaderText="History" />
                                                                                        <asp:BoundField DataField="ImpRemark"
                                                                                            HeaderText="Remark" />
                                                                                        <asp:BoundField DataField="ImpValidity" 
                                                                                            HeaderText="Validity" />
                                                                                        <asp:BoundField DataField="ImpEstDelivery"
                                                                                            HeaderText="Est Delivery" />
                                                                                        <%--<asp:BoundField DataField="ImpClearing" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"
                                                                                            HeaderText="Clearing Cost" DataFormatString="{0:N2}" />
                                                                                        <asp:BoundField DataField="ImpOther" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"
                                                                                            HeaderText="Other Cost" DataFormatString="{0:N2}" />--%>
                                                                                        <asp:BoundField DataField="UnitPriceForeign" NullDisplayText="Not Found" HeaderText="Quoted Unit Price(Foreign)" />
                                                                                            <asp:BoundField DataField="UnitPriceLkr" NullDisplayText="Not Found" HeaderText="Quoted Unit Price(lkr)" />
                                                                                        <asp:BoundField DataField="TermName"
                                                                                            HeaderText="Term" />

                                                                                        <asp:BoundField DataField="QuotedPrice"
                                                                                            HeaderText="Evaluated Price" DataFormatString="{0:N2}" />


                                                                                        <%-- <asp:BoundField DataField="Description" NullDisplayText="Not Found"
                                                                                            HeaderText="itemDescription" />--%>

                                                                                        <asp:TemplateField HeaderText="Requesting qty" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtRequestingQtySu" CssClass="txtRequestingQtySu" runat="server" Text='<%# Eval("RequestingQty") %>' Width="80px" onkeyup="CalculationSupplierNew(this)" Type="number" min="0.00" step="any"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Sub-Total">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblSubTotalSu" CssClass="lblSubTotalSu" runat="server" Text='<%# Eval("SubTotal", "{0:N2}").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VAT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblVAtSup" CssClass="lblVAtSup" runat="server" Text='<%# Eval("vaT").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Net-Total">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblNetTotalSup" CssClass="lblNetTotalSup" runat="server" Text='<%# Eval("NetTotal", "{0:N2}").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                         <asp:BoundField DataField="PaymentModeId" NullDisplayText="Not Found" HeaderText="Payment Type"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                             <asp:TemplateField HeaderText="Payment Type" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label runat="server" ID="lblPaymentModeIdSu"  Text='<%# Eval("PaymentModeId").ToString() == "1" ? "Advance" : Eval("PaymentModeId").ToString() == "2" ? "On Arrival" : Eval("PaymentModeId").ToString() == "3" ? "LC at sight" : Eval("PaymentModeId").ToString() == "4" ? "L/C usance" : Eval("PaymentModeId").ToString() == "5" ? "D/A" : Eval("PaymentModeId").ToString() == "6" ? "D/P" : "0" %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                             <asp:BoundField DataField="PaymentModeDays" NullDisplayText="Not Found" HeaderText="Day No" />
                                                                                        <asp:TemplateField HeaderText=" Purchase ">
                                                                                            <ItemTemplate>

                                                                                                <asp:Button CssClass="btn-xs btn-info btn-styled " runat="server"
                                                                                                    ID="btnPurchasedSu" Text="History"
                                                                                                    Style="margin-top: 3px; width: 100px;" OnClick="btnPurchasedSup_Click"></asp:Button>

                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>


                                                                                        <%--  <asp:BoundField DataField="QuotationItemId" HeaderText="QuotaionItemId" ID="lblQuotationId"
                                                                                     HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                                                                          <asp:BoundField DataField="QuotationId"
                                                                                            HeaderText="QuotationId"
                                                                                            HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                       <%-- <asp:TemplateField HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>

                                                                                                <asp:TextBox ID="txtQuotationIdSu" CssClass="txtQuotationIdSu" runat="server" Text='<%# Eval("QuotationId").ToString() %>'></asp:TextBox>

                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>--%>

                                                                                        <asp:TemplateField HeaderText="Requested Total" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtReqTotQtySu" CssClass="txtReqTotQtySu" runat="server" Text='<%# Eval("RequestedTotalQty").ToString() %>'></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Evaluated Price" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblUnitPriceSu" CssClass="lblUnitPriceSu" runat="server" Text='<%# Eval("UnitPrice").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Has VAT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblHasVatSu" runat="server" CssClass="lblHasVatSu" Text='<%# Eval("HasVat").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Has NBT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblHasNbtSu" runat="server" CssClass="lblHasNbtSu" Text='<%# Eval("HasNbt").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="NBT Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblNBTTypeSu" runat="server" CssClass="lblNBTTypeSu" Text='<%# Eval("NbtType").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Sub-Total" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtSubTotalSu" CssClass="txtSubTotalSu" runat="server" Text='<%# Eval("SubTotal").ToString() %>'></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="NBT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtNbtSu" CssClass="txtNbtSu" runat="server" Text='<%# Eval("Nbt").ToString() %>'></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VAT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtVatSu" CssClass="txtVatSu" runat="server" Text='<%# Eval("vaT").ToString() %>'></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Net-Total" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtNetTotalSu" CssClass="txtNetTotalSu" runat="server" Text='<%# Eval("NetTotal").ToString() %>'></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Requested Total" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtReqTotQtySu1" CssClass="txtReqTotQtySu1" runat="server" Text='<%# Eval("RequestedTotalQty").ToString() %>'></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Total VAT"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtTotVATSu" CssClass="txtTotVATSu" runat="server" Text='<%# Eval("TotalVaT").ToString() %>'></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Total NBTSu" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtTotNBTSu" CssClass="txtTotNBTSu" runat="server" Text='<%# Eval("TotalNbT").ToString() %>'></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Is Selected" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtSelectedQSu" CssClass="txtSelectedQSu" runat="server" Text="0"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>


                                                                                        <asp:TemplateField HeaderText="SelectedSupplierID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblSelectedSupplierIDSu" CssClass="lblSelectedSupplierIDSu" runat="server" Text='<%# Eval("SelectedSupplierID").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="TablationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblTablationIdSu" runat="server" CssClass="lblTablationIdSu" Text='<%# Eval("TablationId").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblItemIdSu" runat="server" CssClass="lblItemIdSu" Text='<%# Eval("ItemId").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="QuotationItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblQuotationItemIdSu" runat="server" CssClass="lblQuotationItemIdSu" Text='<%# Eval("QuotationItemId").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="SupplierMentionedItemName" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblSupplierMentionedItemNameSu" runat="server" CssClass="lblSupplierMentionedItemNameSu" Text='<%#  Eval("SupplierMentionedItemName") == null? "" : Eval("SupplierMentionedItemName").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="IsSelectedTB" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblIsSelectedTBSu" runat="server" CssClass="lblIsSelectedTBSu" Text='<%# Eval("IsSelectedTB") == null ? "" : Eval("IsSelectedTB").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Term" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblTermSu" runat="server" CssClass="lblTermSu" Text='<%# Eval("Term") == null ? "" : Eval("Term").ToString() %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>

                                                                                                <asp:TextBox ID="txtQuotationIdSu" CssClass="txtQuotationIdSu" runat="server" Text='<%# Eval("QuotationId").ToString() %>'></asp:TextBox>

                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>


                                                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                                                                                            <ItemTemplate>

                                                                                                <asp:Button ID="btnSelectSup" runat="server" Text="Select" class="btn btn-primary btn-styled  btn-xs pull-left btnSelect " OnClientClick="SelectBidClick(this ,'Supplier','Imports');" Visible='<%# Eval("IsSelectedTB") == null ? false: Eval("IsSelectedTB").ToString() == "1" ? false : true %>' />
                                                                                                <asp:Label ID="lblSelected" runat="server" Text="SELECTED" Font-Bold="true" ForeColor="SeaGreen" Visible='<%# Eval("IsSelectedTB") == null ? false:Eval("IsSelectedTB").ToString() == "1" ? true : false %>'></asp:Label><br>
                                                                                                <asp:Button ID="Button1" runat="server" Text="Delete" class="btn btn-danger btn-styled  btn-xs pull-left btnDelete " OnClientClick="RemoveBidItemClick(this ,'Supplier','Imports');" Visible='<%#Eval("IsSelectedTB") == null ? false:Eval("IsSelectedTB").ToString() == "1" ? true : false %>' />


                                                                                                <%--                                                                                                <button type="button" class="btn  btn-styled btn-primary btn-xs btnSelectSu" onclick="SelectBidClick(this ,'Supplier','Imports');">Select </button>
                                                                                                <asp:ImageButton ID="btnDelete" CssClass="deleteItem pull-right hidden" ImageUrl="~/images/dlt.png" ToolTip="Delete" Style="width: 20px; height: 20px;"
                                                                                                    runat="server" OnClientClick="RemoveBidItemClick(this ,'Supplier','Imports'));" />--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>


                                                </div>
                                                <!-- End : Quotation Table -->
                                            </div>
                                        </div>
                                    </div>
                                    <!-- End : Modal Body -->

                                    <div class="box-footer">

                                        <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="pull-right hidden" style="margin-right: 10px; max-height: 40px;" />


                                        <button type="button" class="btn btn-info btn-styled   pull-right" onclick="CloseModelQuotations();">Back</button>
                                        <%-- <button type="button" class="btn btn-info btn-styled   pull-left"  > Print </button>--%>
                                        <asp:Button class="btn btn-info btn-styled" runat="server" ID="btnPrint" Text="Print" OnClick="btnPrint_Click"
                                            Style="margin-top: 3px; width: 100px; margin-right: 5px;"></asp:Button>
                                        <button type="button" class="btn  btn-styled btn-success   pull-right " style="width: 150px;" onclick="FinalizeTabulationClick('Imports');">Finalize Tabulation </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End : Modal Content -->

                    </div>
                </div>
                <!-- End : Quotations Modal -->


                <!-- Start : Purchased Items Modal -->
                <div id="mdlItems" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 60%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <%--<button type="button" class="close btnCloseLabelItem" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">--%>
                                <%--<span aria-hidden="true" style="opacity: 1;">×</span></button>--%>
                                <h4 class="modal-title">Purchased Items</h4>
                            </div>

                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->



                            <%--<div class="container">
                                <div class="row text-center">

                                    <div class="col-xs-4">
                                        <h3 style="color: blue;">MIN</h3>
                                        <asp:Label ID="lblMin" runat="server" Style="color: #1ac418; font-size: 20px; font-weight: bold"></asp:Label>
                                    </div>
                                    <div class="col-xs-6">
                                        <h3 style="color: blue;">MAX</h3>
                                        <asp:Label ID="lblMax" runat="server" Style="color: #eb0c1e; font-size: 20px; font-weight: bold"></asp:Label>
                                    </div>

                                </div>

                            </div>--%>

                            <div class="modal-body">


                                <div class="row">
                                    <div class="col-xs-12">


                                        <div>
                                            &nbsp
                                        </div>

                                        <!-- Start : Items Table -->
                                        <div style="color: black;">

                                            <asp:GridView ID="gvPurchasedItems" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No Purchase Hostory records."
                                                CssClass="table gvPurchasedItems"
                                                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>

                                                    <asp:BoundField DataField="ItemId"
                                                        HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden"
                                                        ItemStyle-CssClass="hidden" />

                                                    <asp:BoundField DataField="Department_Name" HeaderText="Company Name" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                    <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price" />
                                                    <asp:BoundField DataField="PoCode" HeaderText="PO Code" />
                                                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />

                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <!-- End : Quotation Table -->
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                            <div class="box-footer">

                                <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="pull-right hidden" style="margin-right: 10px; max-height: 40px;" />
                                <button type="button" class="btn btn-info btn-styled   pull-right" onclick="PopupGvItemsModelHis();">Close</button>

                            </div>
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Purchased Items Modal -->


                <!-- Start : Attachment Modal -->
                <div id="mdlAttachments" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">

                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                                                <h4 class="text-green text-bold">ATTACHMENT QUOTATIONS</h4>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <asp:Panel ID="pnlImages" runat="server">
                                                        <label for="fileImages">Uploded Images</label>
                                                        <asp:GridView ID="gvImages" runat="server" ShowHeader="False"
                                                            GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Image Found">
                                                            <Columns>
                                                                <asp:BoundField DataField="QuotationImageId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink runat="server" href='<%#Eval("ImagePath").ToString().Remove(0,2)%>' Target="_blank">
                                                                            <asp:Image runat="server" ImageUrl='<%#Eval("ImagePath")%>' style="max-height:50px; width:auto; margin:5px" />
                                                                        </asp:HyperLink>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Panel ID="pnlDocs" runat="server" Width="100%">
                                                        <label for="fileImages">Uploded Documents</label>
                                                        <asp:GridView ID="gvDocs" runat="server" ShowHeader="False"
                                                            GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Document Found" Width="100%">
                                                            <Columns>
                                                                <asp:BoundField DataField="QuotationFileId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                <asp:TemplateField ItemStyle-Height="30px">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton
                                                                            Text='<%#Eval("FileName")%>' runat="server" href='<%#Eval("FilePath").ToString().Remove(0,2)%>' target="_blank" Style="margin-right: 5px;" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Panel ID="pnlcondtion" runat="server" Width="100%">
                                                        <label for="fileImages">Terms And Conditons</label>
                                                        <asp:TextBox TextMode="MultiLine" Rows="10" ID="txtTermsAndConditions" Enabled="false" runat="server" CssClass="form-control text-bold"></asp:TextBox>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                            <div class="box-footer">

                                <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="pull-right hidden" style="margin-right: 10px; max-height: 40px;" />
                                <button type="button" class="btn btn-info btn-styled   pull-right" onclick="PopupGvItemsModelDoc();">Close</button>

                            </div>
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>


                <!-- End : Section -->

                <!-- Start : Rates Modal -->
                <div id="mdlRates" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 60%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close btnCloseLabelItem" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Currency Details </h4>
                            </div>

                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">

                                        <div>
                                            &nbsp
                                        </div>

                                        <!-- Start : Rates Table -->
                                        <div style="color: black;">

                                            <asp:GridView ID="gvRatss" runat="server" CssClass="table table-responsive"
                                                GridLines="None"
                                                AutoGenerateColumns="false" DataKeyNames="BidId" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <span style="font: bold;">
                                                                <%#Container.DataItemIndex + 1%>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="objCurrencyDetails.CurrencyTypeId" HeaderText="CurrencyTypeId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier" NullDisplayText="Unavailable" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="objCurrencyDetails.CurrentcyName" HeaderText="Currency" NullDisplayText="Unavailable" />

                                                    <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                        <ItemTemplate>

                                                            <asp:TextBox ID="txtRate" CssClass="txtRate" runat="server" Text='<%# Eval("objCurrencyDetails.SellingRate").ToString() %>'></asp:TextBox>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <!-- End : rates Table -->
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                            <div class="box-footer">

                                <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="pull-right hidden" style="margin-right: 10px; max-height: 40px;" />

                                <asp:Button CssClass="btn btn-info btn-styled   pull-right" runat="server"
                                    ID="btnDoneRate" Text="Done" Style="width: 100px;" OnClick="btnDoneRate_Click"></asp:Button>
                                <%--<button type="button" class="btn btn-info btn-styled   pull-right" onclick="SaveRates();"> Done</button>--%>
                            </div>
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Rates Modal -->
                <!-- Start : Purchased Items Modal -->
                <div id="mdlImportDetails" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 95%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close btnCloseLabelItem" aria-label="Close" style="opacity: 1; color: white;" onclick="CloseModelImportsModel();">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Import Details</h4>
                            </div>

                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->

                            <div class="modal-body" style="overflow: scroll;">


                                <div class="row">
                                    <div class="col-xs-12">


                                        <div>
                                            &nbsp
                                        </div>

                                        <!-- Start : Items Table -->
                                        <div style="color: black;">

                                            <asp:GridView ID="GridImportDetails" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No Import records."
                                                CssClass="table  GridImportDetails"
                                                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SNumber" HeaderText="S/ No :" />
                                                    <asp:BoundField DataField="ReferenceNumber" HeaderText="Ref " />
                                                    <asp:BoundField DataField="Currency" HeaderText="Currency Type" />
                                                    <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                                    <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                                                    <asp:BoundField DataField="Mill" HeaderText="Mill" />
                                                    <asp:BoundField DataField="Country" HeaderText="Country" />
                                                    <asp:BoundField DataField="Agent" HeaderText="Agent" />
                                                    <asp:BoundField DataField="Gsm" HeaderText="GSM" />
                                                    <asp:BoundField DataField="Term" HeaderText="Term" />
                                                    <asp:TemplateField HeaderText="TOTAL- CIF">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrCIF" CssClass="lblOrCIF" runat="server" Text='<%# Eval("OrginalCIFAmount").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TOTAL- CIF(LKR)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCIF" CssClass="lblCIF" runat="server" Text='<%# Eval("CIFAmountLKR","{0:N2}").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Duty,PAL&Other">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDuOther" CssClass="lblDuOther" runat="server" Text='<%# Eval("DuctyPALOther","{0:N2}").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cost of Chemicals">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCostChemical" CssClass="lblCostChemical" runat="server" Text='<%# Eval("CostOfChemicals","{0:N2}").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Est Landed cost-LKR">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllandCost" CssClass="lbllandCost" runat="server" Text='<%# Eval("LandedCostLKR","{0:N2}").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Est Clearing Cost /Mt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClearingCost" CssClass="lblClearingCost" runat="server" Text='<%# Eval("ClearingCost","{0:N2}").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Est. Cost/MT -LKR">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClearingCostLKR" CssClass="lblClearingCostLKR" runat="server" Text='<%# Eval("ClearingCostLKR","{0:N2}").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Validity" HeaderText="Validity" />
                                                    <asp:BoundField DataField="EstDelivery" HeaderText="Estimated Deivery" />
                                                    <asp:BoundField DataField="ImportHistory" HeaderText="Import History" />
                                                    <asp:BoundField DataField="PaymentMode" HeaderText="Payment" />
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <!-- End : Quotation Table -->
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                            <div class="box-footer">

                                <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="pull-right hidden" style="margin-right: 10px; max-height: 40px;" />
                                <button type="button" class="btn btn-info btn-styled   pull-right" onclick="CloseModelImportsModel();">Close</button>

                            </div>
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Purchased Items Modal -->



                <!-- Start : Section -->
                <section class="content ">
                    <div class="row">
                        <div class="col-xs-12">
                            <!-- Start : Box -->
                            <div class="box box-info">
                                <!-- Start : Box Header-->
                                <div class="box-header with-border">
                                    <h3 class="box-title">Purchase Request Details</h3>
                                </div>
                                <!-- End : Box Header -->
                                <!-- Start : Box Body-->
                                <div class="box-body">
                                    <div class="row">
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>PR No : </strong>
                                        <asp:Label ID="lblPRNo" runat="server" Text=""></asp:Label><br />
                                        <strong>Created On : </strong>
                                        <asp:Label ID="lblCreatedOn" runat="server" Text=""></asp:Label><br />
                                        <strong>Created By : </strong>
                                        <asp:Label ID="lblCreatedBy" runat="server" Text=""></asp:Label><br />
                                    </address>
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>Requested By : </strong>
                                        <asp:Label ID="lblRequestBy" runat="server" Text=""></asp:Label><br />
                                        <strong>Requested For : </strong>
                                        <asp:Label ID="lblRequestFor" runat="server" Text=""></asp:Label><br />
                                        <strong>Expense Type : </strong>
                                        <asp:Label ID="lblExpenseType" runat="server" Text=""></asp:Label><br />
                                    </address>
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>Warehouse : </strong>
                                        <asp:Label ID="lblWarehouse" runat="server" Text=""></asp:Label><br /> 
                                        <strong>MRN No : </strong>
                                        <asp:Label ID="lblMrnId" runat="server" Text=""></asp:Label><br />   
                                        <strong>Department : </strong>
                                        <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label><br />                                      
                                    </address>
                                </div>
                            </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvBids" runat="server" CssClass="table table-responsive"
                                                   GridLines="None"
                                                    AutoGenerateColumns="false" DataKeyNames="BidId" Caption="Bids for Purchase Request"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" OnRowDataBound="gvBids_RowDataBound">
                                                    <Columns>
                                                        
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                                    src="images/plus.png" />
                                                                
                                                                <asp:Panel ID="pnlBidItems" runat="server" Style="display: none">
                                                                    <asp:GridView ID="gvBidItems" runat="server"
                                                                        CssClass="table table-responsive ChildGrid"
                                                                        GridLines="None" AutoGenerateColumns="false"
                                                                        Caption="Items in Bid">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="BiddingItemId"
                                                                                HeaderText="BidItemId"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="BidId"
                                                                                HeaderText="BidItemId"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="PrdId"
                                                                                HeaderText="PRDId" HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="CategoryId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="CategoryName"
                                                                                HeaderText="Category Name" />
                                                                            <asp:BoundField DataField="SubCategoryId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="SubCategoryName"
                                                                                HeaderText="Sub-Category Name" />
                                                                            <asp:BoundField DataField="ItemId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                           

                                                                            <asp:BoundField DataField="ItemName"
                                                                                HeaderText="Item Name" />
                                                                            <asp:TemplateField HeaderText="Quantity">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" Text='<%# decimal.Parse(Eval("Qty").ToString()).ToString() + " " + Eval("UnitShortName").ToString() %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="EstimatedPrice"
                                                                                HeaderText="Estimated Price" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="PrId" HeaderText="PrId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="Bid Code"
                                                            HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# "B"+Eval("BidCode").ToString() %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CreatedUserName" HeaderText="Created By" />
                                                        <asp:BoundField DataField="CreateDate" HeaderText="Created Date"
                                                            DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                            DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                            DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:TemplateField HeaderText="Bid Opened For">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BidOpeningPeriod").ToString()+" Days" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bid Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BidOpenType").ToString() =="1" ? "Online":Eval("BidOpenType").ToString() =="2" ? "Manual":"Online & Manual" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NoOfQuotations" HeaderText="Quotations Count" />

                                                         <%-- <asp:BoundField DataField="NoOfRejectedQuotations" HeaderText="Rejected Tabulation Count" />
                                                        <asp:TemplateField HeaderText="Is Tabulation Selected">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsQuotationSelected").ToString() =="1" ? "Selected":"Not Selected" %>'
                                                                    ForeColor='<%# Eval("IsQuotationSelected").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                       
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>

                                                                
                                                                <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("IsTerminated").ToString() == "1" ? true : false %>'
                                                                Text="Terminated" Style="margin-right: 4px; margin-bottom: 4px; color: red;" Font-Bold="true" />
                                                                <asp:Button CssClass="btn btn-info btn-xs btn-styled TabluationBtn btnTab" runat="server"
                                                                    ID="btnView" Text="Tabulation Sheet" 
                                                                    style="width:100px;" OnClick="btnView_Click" ></asp:Button>
                                                                  
                                                                <button type="button" class="btn btn-warning btn-styled pull-right TabluationBtn btn-xs btnReopn"  style="margin-top:3px; width:100px;" onclick="ReOpenBidClick(this);"> Re-Open-Bid </button>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvRejectedBidsAtApproval" runat="server" CssClass="table table-responsive"
                                                     GridLines="None"
                                                    AutoGenerateColumns="false" DataKeyNames="BidId" Caption="Bids Rejected At Quotation Approval"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                                    src="images/plus.png" />
                                                                <asp:Panel ID="pnlBidItems" runat="server" Style="display: none">
                                                                    <asp:GridView ID="gvBidItems" runat="server"
                                                                        CssClass="table table-responsive ChildGrid"
                                                                        GridLines="None" AutoGenerateColumns="false"
                                                                        Caption="Items in Bid">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="BiddingItemId"
                                                                                HeaderText="BidItemId"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="BidId"
                                                                                HeaderText="BidItemId"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="PrdId"
                                                                                HeaderText="PRDId" HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="CategoryId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="CategoryName"
                                                                                HeaderText="Category Name" />
                                                                            <asp:BoundField DataField="SubCategoryId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="SubCategoryName"
                                                                                HeaderText="Sub-Category Name" />
                                                                            <asp:BoundField DataField="ItemId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="ItemName"
                                                                                HeaderText="Item Name" />
                                                                            <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                                                            <asp:BoundField DataField="EstimatedPrice"
                                                                                HeaderText="Estimated Price" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="PrId" HeaderText="PrId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="Bid Code"
                                                            HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# "B"+Eval("BidCode").ToString() %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CreatedUserName" HeaderText="Created By" />
                                                        <asp:BoundField DataField="CreateDate" HeaderText="Created Date"
                                                            DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                            DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                            DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:TemplateField HeaderText="Bid Opened For">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BidOpeningPeriod").ToString()+" Days" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bid Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BidOpenType").ToString() =="1" ? "Online":Eval("BidOpenType").ToString() =="2" ? "Manual":"Online & Manual" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NoOfQuotations" HeaderText="Qutations Count" />
                                                        <asp:BoundField DataField="NoOfRejectedQuotations" HeaderText="Rejected Quotations Count" />
                                                        <asp:TemplateField HeaderText="Is Quotation Selected">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsQuotationSelected").ToString() =="1" ? "Selected":"Not Selected" %>'
                                                                    ForeColor='<%# Eval("IsQuotationSelected").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quotation Status">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsQuotationApproved").ToString() =="1" ? "Selected":"Rejected" %>'
                                                                    ForeColor='<%# Eval("IsQuotationApproved").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="QuotationApprovedByName" HeaderText="Rejected By" />
                                                        <asp:BoundField DataField="QuotationApprovalDate" HeaderText="Rejected On" />
                                                        <asp:BoundField DataField="QuotationApprovalRemarks" HeaderText="Rejected Reason" />
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-xs btn-warning" runat="server"
                                                                    ID="btnReset" Text="Resubmit" 
                                                                    style="margin-top:3px; width:100px;"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvRejectedBidsAtConfirmation" runat="server" CssClass="table table-responsive"
                                                     GridLines="None"
                                                    AutoGenerateColumns="false" DataKeyNames="BidId" Caption="Bids Rejected At Quotation Confirmation"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                                    src="images/plus.png" />
                                                                <asp:Panel ID="pnlBidItems" runat="server" Style="display: none">
                                                                    <asp:GridView ID="gvBidItems" runat="server"
                                                                        CssClass="table table-responsive ChildGrid"
                                                                        GridLines="None" AutoGenerateColumns="false"
                                                                        Caption="Items in Bid">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="BiddingItemId"
                                                                                HeaderText="BidItemId"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="BidId"
                                                                                HeaderText="BidItemId"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="PrdId"
                                                                                HeaderText="PRDId" HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="CategoryId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="CategoryName"
                                                                                HeaderText="Category Name" />
                                                                            <asp:BoundField DataField="SubCategoryId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="SubCategoryName"
                                                                                HeaderText="Sub-Category Name" />
                                                                            <asp:BoundField DataField="ItemId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="ItemName"
                                                                                HeaderText="Item Name" />
                                                                            <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                                                            <asp:BoundField DataField="EstimatedPrice"
                                                                                HeaderText="Estimated Price" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="PrId" HeaderText="PrId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="Bid Code"
                                                            HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# "B"+Eval("BidCode").ToString() %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CreatedUserName" HeaderText="Created By" />
                                                        <asp:BoundField DataField="CreateDate" HeaderText="Created Date"
                                                            DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                            DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                            DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:TemplateField HeaderText="Bid Opened For">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BidOpeningPeriod").ToString()+" Days" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bid Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BidOpenType").ToString() =="1" ? "Online":Eval("BidOpenType").ToString() =="2" ? "Manual":"Online & Manual" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NoOfQuotations" HeaderText="Qutations Count" />
                                                        <asp:BoundField DataField="NoOfRejectedQuotations" HeaderText="Rejected Quotations Count" />
                                                        <asp:TemplateField HeaderText="Is Quotation Selected">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsQuotationSelected").ToString() =="1" ? "Selected":"Not Selected" %>'
                                                                    ForeColor='<%# Eval("IsQuotationSelected").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quotation Status">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsQuotationConfirmed").ToString() =="1" ? "Selected":"Rejected" %>'
                                                                    ForeColor='<%# Eval("IsQuotationConfirmed").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="QuotationConfirmedByName" HeaderText="Rejected By" />
                                                        <asp:BoundField DataField="QuotationConfirmedDate" HeaderText="Rejected On" />
                                                        <asp:BoundField DataField="QuotationConfirmationRemarks" HeaderText="Rejected Reason" />
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-xs btn-warning" runat="server"
                                                                    ID="btnReset" Text="Resubmit" 
                                                                    style="margin-top:3px; width:100px;"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End : Box Body -->
                                <!-- Start : Box Footer -->
                               <%-- <div class="box-footer">
                                     <button type="button" class="btn btn-info btn-styled   pull-right" onclick=""> Done </button>
                                </div>--%>
                                <!-- End : Box Footer -->
                            </div>
                            <!-- End : Box -->
                        </div>
                    </div>
                </section>
                <!-- End : Section -->
                <asp:HiddenField ID="hdnRejectedQuotationCount" runat="server" />
                <asp:HiddenField ID="hdnQuotationItemId" runat="server" />
                <asp:HiddenField ID="hdnQuotationId" runat="server" />
                <asp:HiddenField ID="hdnBidId" runat="server" />
                <asp:HiddenField ID="hdnBidItemId" runat="server" />
                <asp:HiddenField ID="hdnQuotationChangeDetected" runat="server" />
                <asp:HiddenField ID="hdnSelectionChangeDetected" runat="server" />
                <asp:HiddenField ID="hdnRemarks" runat="server" />
                <asp:HiddenField ID="hdnItemId" runat="server" />
                <asp:HiddenField ID="hdnSubTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNbtTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnVatTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNetTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdn_sub" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdn_vat" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdn_netTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdn_nbt" runat="server" Value="0.00" />
                <asp:HiddenField ID="PurchaseType" runat="server" />
                <asp:HiddenField ID="hdnQty" runat="server" />
                <asp:HiddenField ID="hdnTabulationId" runat="server" />
                 <asp:HiddenField ID="hdnReOpenDays" runat="server" />
                <asp:HiddenField ID="hdnReopenRemarks" runat="server" />
                <asp:HiddenField ID="hdnSupplierId" runat="server" />
                <asp:Button ID="btnApprove" runat="server" OnClick="lbtnSelect_Click" CssClass="hidden" />
                <asp:Button ID="btnRemove" runat="server" OnClick="lbtnRemove_Click" CssClass="hidden" />
                <asp:Button ID="btnReOpenNew" runat="server" OnClick="btnReOpenNew_Click" CssClass="hidden" />
                <asp:Button ID="btnFinalizeNew" runat="server" OnClick="lbtnFinalizeNew_Click" CssClass="hidden" />
                <asp:HiddenField ID="hdnUitPriceForeign" runat="server" />
                <asp:HiddenField ID="hdnUnitPriceLKR" runat="server" />
                <asp:HiddenField ID="hdnPurchaseType" runat="server" />
                <asp:HiddenField ID="hdnDayNo" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnPrint" />
                <asp:PostBackTrigger ControlID="btnPrintNew" />
            </Triggers>
        </asp:UpdatePanel>
    </form>



    <%--<script src="Scripts/LoginService.js?v=<%DateTime.Now.ToString();%>"></script>
    <script src="ViewModels/JS/PrViewModel.js?v=<%DateTime.Now.ToString();%>"></script>
    <script src="ViewModels/JS/TBViewModel.js?v=<%DateTime.Now.ToString();%>"></script>
    <script src="Scripts/Tabulation.js?v=<%DateTime.Now.ToString();%>"></script>--%>

    <script>

        function PopupGvItemsModelHis() {

            $('#mdlItems').modal('hide');
            $('.modal-backdrop').remove();
            $('body').css("overflow", "auto");
            $('body').css("padding-right", "0");

        }

        function PopupGvItemsModelDoc() {

            $('#mdlAttachments').modal('hide');
            $('.modal-backdrop').remove();
            $('body').css("overflow", "auto");
            $('body').css("padding-right", "0");

        }
        $('#mdlItems').on('hide.bs.modal', function () {
            $('.modal-backdrop').remove();
        });

        $('#mdlAttachments').on('hide.bs.modal', function () {
            $('.modal-backdrop').remove();
        });
        $('#mdlAttachments').on('shown.bs.modal', function () {
            $('#mdlAttachments').css('overflow', 'auto');
            $('body').css("overflow", "hidden");
            $('body').css("padding-right", "0");
        })

        $('#mdlAttachments').on('hidden.bs.modal', function () {
            $('body').css("overflow", "auto");
            $('body').css("padding-right", "0");
        })

        $('#mdlItems').on('shown.bs.modal', function () {
            $('#mdlItems').css('overflow', 'auto');
            $('body').css("overflow", "hidden");
            $('body').css("padding-right", "0");
        })

        $('#mdlItems').on('hidden.bs.modal', function () {
            $('body').css("overflow", "auto");
            $('body').css("padding-right", "0");
        })

        function Calculation(elmt) {
            var UnitPrice = $(elmt).closest('tr').find('.lblUnitPrice').text().replace(/,/, '');
            var RequestingQty = $(elmt).closest('tr').find('.txtRequestingQtynew').val().replace(/,/, '');
            var HasVat = $(elmt).closest('tr').find('.lblHasVat').text();
            var HasNbt = $(elmt).closest('tr').find('.lblHasNbt').text();
            //var NbtType = $(elmt).closest('tr').find('.lblNBTType').text();
            var RequestedTotQty = $(elmt).closest('tr').find('.txtReqTotQty').val().replace(/,/, '');
            //var TotNBT = $(elmt).closest('tr').find('.txtTotNBT').val();
            var TotVAT = $(elmt).closest('tr').find('.txtTotVAT').val().replace(/,/, '');
            var Term = $(elmt).closest('tr').find('.lblTerm').text();

            var Subtotal = 0;
            var vat = 0;

            var Subtotal = parseFloat(UnitPrice * RequestingQty);



            if (Term == "11" && RequestingQty != "0" && RequestingQty != "") {
                if (HasVat == "1") {
                    vat = parseFloat(TotVAT / RequestedTotQty) * RequestingQty;
                }

            }
            else if(Term != "11" && RequestingQty != "0" && RequestingQty != "") {
                if (HasVat == "1") {
                    vat = parseFloat(TotVAT);
                     //vat = parseFloat(TotVAT / RequestedTotQty) * RequestingQty;
                }

            }

            //var Nbt = 0;
            //if (HasNbt == 1) {
            //    //if (NbtType == 1) {
            //    //    Nbt = Subtotal * 0.0204
            //    //}
            //    //else if (NbtType == 2) {
            //    //    Nbt = Subtotal * 0.0200
            //    //}
            //    Nbt = (TotNBT / RequestedTotQty) * RequestingQty;
            //    Nbt = 0;

            //}
            //var NetTotal = Subtotal + Nbt + Vat;
            var NetTotal = Subtotal + vat;


            $(elmt).closest('tr').find('.lblSubTotal').text(Subtotal.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
            //$(elmt).closest('tr').find('.lblNbt').text(Nbt.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
            $(elmt).closest('tr').find('.lblVat').text(vat.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
            $(elmt).closest('tr').find('.lblNetTotal').text(NetTotal.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));

            $(elmt).closest('tr').find('.txtSubTotal').val(Subtotal.toFixed(2));
            //$(elmt).closest('tr').find('.txtNbt').val(Nbt.toFixed(2));
            $(elmt).closest('tr').find('.txtVat').val(vat);
            $(elmt).closest('tr').find('.txtNetTotal').val(NetTotal.toFixed(2));
            $(elmt).closest('tr').find('.txtSelectedQ').val("1")

        }

        function CalculationSupplierNew(elmt) {
            var UnitPrice = $(elmt).closest('tr').find('.lblUnitPriceSu').text();

            var RequestingQty = $(elmt).closest('tr').find('.txtRequestingQtySu').val();
            var HasVat = $(elmt).closest('tr').find('.lblHasVatSu').text();
            var HasNbt = $(elmt).closest('tr').find('.lblHasNbtSu').text();
            //var NbtType = $(elmt).closest('tr').find('.lblNBTTypeSu').text();
            var RequestedTotQty = $(elmt).closest('tr').find('.txtReqTotQtySu').val();
            //var TotNBT = $(elmt).closest('tr').find('.txtTotNBTSu').val();
            var TotVAT = $(elmt).closest('tr').find('.txtTotVATSu').val();
            var Term = $(elmt).closest('tr').find('.lblTermSu').text();

            var Subtotal = 0;
            var vat = 0;

            var Subtotal = parseFloat(UnitPrice * RequestingQty);

            if (Term == "11" && RequestingQty != "0" && RequestingQty != "") {
                if (HasVat == 1) {
                    // Vat = Subtotal * 0.08;
                    vat = parseFloat((TotVAT / RequestedTotQty) * RequestingQty);
                }
            }
            else if(Term != "11" && RequestingQty != "0" && RequestingQty != "") {
                if (HasVat == "1") {
                    vat = parseFloat(TotVAT);
                }

            }

            
            //var Nbt = 0.00;
            //if (HasNbt == 1) {
            //    //if (NbtType == 1) {
            //    //    Nbt = Subtotal * 0.0204
            //    //}
            //    //else if (NbtType == 2) {
            //    //    Nbt = Subtotal * 0.0200
            //    //}
            //    Nbt = (TotNBT / RequestedTotQty) * RequestingQty;


            //}
            var NetTotal = Subtotal + vat;


            $(elmt).closest('tr').find('.lblSubTotalSu').text(Subtotal.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
            //$(elmt).closest('tr').find('.lblNbt').text(Nbt.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
            $(elmt).closest('tr').find('.lblVAtSup').text(vat.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
            $(elmt).closest('tr').find('.lblNetTotalSup').text(NetTotal.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));



            $(elmt).closest('tr').find('.txtSubTotalSu').val(Subtotal.toFixed(2));
            //$(elmt).closest('tr').find('.txtNbtSu').val(Nbt.toFixed(2));
            $(elmt).closest('tr').find('.txtVatSu').val(vat.toFixed(2));
            $(elmt).closest('tr').find('.txtNetTotalSu').val(NetTotal.toFixed(2));
            $(elmt).closest('tr').find('.txtSelectedQSu').val("1")

        }


        function SelectBidClick(elmt, status, pageStatus) {
            $('#mdlQuotations').modal('hide');
            var Qty = 0;
            var InitQty = 0;
            if (status == "Items") {
                Qty = $(elmt).closest('tr').find('.txtRequestingQtynew').val();
                InitQty = $(elmt).closest('tr').find('.txtReqTotQty').val();
                $('#ContentSection_hdnQty').val(Qty);
                $('#ContentSection_hdnQuotationId').val($(elmt).closest('tr').find('td').eq(2).text());
                // $('#ContentSection_hdnQuotationItemId').val($(elmt).closest('tr').find('td').eq(1).text());
                $('#ContentSection_hdn_sub').val($(elmt).closest('tr').find('.lblSubTotal').text());
                // $('#ContentSection_hdn_nbt').val($(elmt).closest('tr').find('.lblNbt').text());
                $('#ContentSection_hdn_vat').val($(elmt).closest('tr').find('.lblVat').text());
                $('#ContentSection_hdn_netTotal').val($(elmt).closest('tr').find('.lblNetTotal').text());
                $('#ContentSection_hdnTabulationId').val($(elmt).closest('tr').find('.lblTablationId').text());
                $('#ContentSection_hdnSupplierId').val($(elmt).closest('tr').find('td').eq(4).text());
                $('#ContentSection_hdnSupplierMentionedName').val($(elmt).closest('tr').find('td').eq(7).text());
                $('#ContentSection_hdnItemId').val($(elmt).closest('tr').find('.lblItemId').text());
                $('#ContentSection_hdnUitPriceForeign').val($(elmt).closest('tr').find('td').eq(17).text());
                $('#ContentSection_hdnUnitPriceLKR').val($(elmt).closest('tr').find('td').eq(18).text());
                 $('#ContentSection_hdnPurchaseType').val($(elmt).closest('tr').find('.lblPaymentModeId').text());
                $('#ContentSection_hdnDayNo').val($(elmt).closest('tr').find('td').eq(30).text());
            } else {
                Qty = $(elmt).closest('tr').find('.txtRequestingQtySu').val();
                InitQty = $(elmt).closest('tr').find('.txtReqTotQtySu').val();

                $('#ContentSection_hdnQty').val(Qty);
                $('#ContentSection_hdnQuotationId').val($(elmt).closest('tr').find('.txtQuotationIdSu').val());
                $('#ContentSection_hdn_sub').val($(elmt).closest('tr').find('.lblSubTotalSu').text());
                // $('#ContentSection_hdn_nbt').val($(elmt).closest('tr').find('.lblNbt').text());
                $('#ContentSection_hdn_vat').val($(elmt).closest('tr').find('.lblVAtSup').text());
                $('#ContentSection_hdn_netTotal').val($(elmt).closest('tr').find('.lblNetTotalSup').text());
                $('#ContentSection_hdnTabulationId').val($(elmt).closest('tr').find('.lblTablationIdSu').text());
                $('#ContentSection_hdnItemId').val($(elmt).closest('tr').find('.lblItemIdSu').text());
                $('#ContentSection_hdnUitPriceForeign').val($(elmt).closest('tr').find('td').eq(21).text());
                $('#ContentSection_hdnUnitPriceLKR').val($(elmt).closest('tr').find('td').eq(22).text());
                 $('#ContentSection_hdnPurchaseType').val($(elmt).closest('tr').find('.lblPaymentModeIdSu').text());
                $('#ContentSection_hdnDayNo').val($(elmt).closest('tr').find('td').eq(31).text());
            }

            if (parseFloat(Qty) > parseFloat(InitQty)) {
                swal.fire({
                    title: 'Quantity Exceeded?',
                    html: "Requested Amount Exceeded ! Are you sure you want to <strong>Select</strong> the Quotation?</br></br>"
                        + "<strong id='dd'>Remarks</strong>"
                        + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                    type: 'warning',
                    cancelButtonColor: '#d33',
                    showCancelButton: true,
                    showConfirmButton: true,
                    confirmButtonText: 'Select',
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
                        $('#ContentSection_btnApprove').click();

                    } else if (result.dismiss === Swal.DismissReason.cancel) {


                    }
                });
            }
            else if (parseFloat(Qty) > 0) {
                swal.fire({
                    title: 'Are you sure?',
                    html: "Are you sure you want to <strong>Select</strong> the Quotation?</br></br>"
                        + "<strong id='dd'>Remarks</strong>"
                        + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                    type: 'warning',
                    cancelButtonColor: '#d33',
                    showCancelButton: true,
                    showConfirmButton: true,
                    confirmButtonText: 'Select',
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
                }).then((result) => {
                    if (result.value) {
                        $('#ContentSection_btnApprove').click();



                    } else if (result.dismiss === Swal.DismissReason.cancel) {


                    }
                });
            } else {

                swal.fire({
                    title: 'ERROR',
                    html: "Please enter requesting quantity value</br></br>",
                    type: 'error',
                    allowOutsideClick: false,
                    preConfirm: function () {
                    }
                }
                ).then((result) => {

                });



            }





        }

        function RemoveBidItemClick(elmt, status, pageStatus) {
            $('#mdlQuotations').modal('hide');
            if (status == "Items") {

                $('#ContentSection_hdnQuotationId').val($(elmt).closest('tr').find('td').eq(2).text());
                $('#ContentSection_hdnQuotationItemId').val($(elmt).closest('tr').find('td').eq(1).text());
                $('#ContentSection_hdn_sub').val($(elmt).closest('tr').find('.lblSubTotal').text());
                // $('#ContentSection_hdn_nbt').val($(elmt).closest('tr').find('.lblNbt').text());
                $('#ContentSection_hdn_vat').val($(elmt).closest('tr').find('.lblVat').text());
                $('#ContentSection_hdn_netTotal').val($(elmt).closest('tr').find('.lblNetTotal').text());
                $('#ContentSection_hdnTabulationId').val($(elmt).closest('tr').find('.lblTablationId').text());
                $('#ContentSection_hdnItemId').val($(elmt).closest('tr').find('.lblItemId').text());
                $('#ContentSection_hdnSupplierId').val($(elmt).closest('tr').find('td').eq(4).text());
            }
            else {
                $('#ContentSection_hdnQuotationId').val($(elmt).closest('tr').find('.txtQuotationIdSu').val());
                $('#ContentSection_hdn_sub').val($(elmt).closest('tr').find('.lblSubTotalSu').text());
                // $('#ContentSection_hdn_nbt').val($(elmt).closest('tr').find('.lblNbt').text());
                $('#ContentSection_hdn_vat').val($(elmt).closest('tr').find('.lblVAtSup').text());
                $('#ContentSection_hdn_netTotal').val($(elmt).closest('tr').find('.lblNetTotalSup').text());
                $('#ContentSection_hdnTabulationId').val($(elmt).closest('tr').find('.lblTablationIdSu').text());
                $('#ContentSection_hdnItemId').val($(elmt).closest('tr').find('.lblItemIdSu').text());
            }
            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to <strong>Remove</strong> Selected Item ?</br></br>",
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

                        return false;
                    }

                }
            }
            ).then((result) => {
                if (result.value) {

                    $('#ContentSection_btnRemove').click();

                } else if (result.dismiss === Swal.DismissReason.cancel) {


                }
            });


        }
        function FinalizeTabulationClick(status) {

            $('#mdlQuotations').modal('hide');
            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to <strong>Finalize Tabulation</strong>?</br></br>"
                    + "<strong id='dd'>Remarks</strong>"
                    + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Finalize',
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
                    $('#ContentSection_btnFinalizeNew').click();

                } else if (result.dismiss === Swal.DismissReason.cancel) {

                }
            });

        }


        function CloseModelQuotations() {
            $('#mdlQuotations').modal('hide');
            $('#ContentSection_hdnbtnEnablestatus').click();

        }

        function ReOpenBidClick(element) {
            var tableRow = $(element).closest('tr:not(:has(>td>table))')[0].cells;
            var BidId = $(tableRow).eq(1).text();
             $('#ContentSection_hdnBidId').val(BidId);
            
            swal.fire({
                title: 'Are you sure?',
                html: "Reopening Bid will discard all the selections and terminations done.</br></br>"
                    + "<strong id='dd'>Duration (Days)</strong>"
                    + "<input id='ss' type='number' value='1' min='1' class ='form-control' required='required'/></br></br>"
                    + "<strong id='ee'>Remark</strong>"
                    + "<input id='pp'  class ='form-control' required='required'/></br>",
                type: 'warning',
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'OK',
                cancelButtonText: 'Cancel',
                allowOutsideClick: false,
                preConfirm: function () {
                    if ($('#ss').val() == '' || $('#pp').val() == '') {
                        $('#dd').prop('style', 'color:red');
                        $('#ee').prop('style', 'color:red');
                        swal.showValidationError('Field Cannot Be Empty');
                        return false;
                    }
                    else {
                        $('#ContentSection_hdnReOpenDays').val($('#ss').val());
                        $('#ContentSection_hdnReopenRemarks').val($('#pp').val());
                    }
                }
            }).then((result) => {
                if (result.value) {
                     $('#ContentSection_btnReOpenNew').click();

                }
            });


        }

    </script>
</asp:Content>
