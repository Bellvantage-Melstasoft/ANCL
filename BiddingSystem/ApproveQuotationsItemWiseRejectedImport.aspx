<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ApproveQuotationsItemWiseRejectedImport.aspx.cs" Inherits="BiddingSystem.ApproveQuotationsItemWiseRejectedImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server" ViewStateMode="Enabled">

    <style type="text/css">
        .ChildGrid > tbody > tr > td:not(table) {
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
            background-color: #808080 !important;
        }

        .ChildGridThree td {
            text-align: left;
        }

        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        #ContentSection_gvQuotations > tbody {
            background-color: #fbfafa !important;
            text-align: left;
        }

        #ContentSection_gvQuotations th {
            text-align: center;
        }

        #ContentSection_gvBids th, #ContentSection_gvBids td {
            text-align: center;
        }


        #ContentSection_gvQuotations > tbody > tr:nth-child(2n+1) > td:not(table) {
            border-bottom: 1px solid #555555;
            border-top: 1px solid #f8f8f8;
        }

        .greenBg {
            background: #7bf768;
            ;
            font-weight: bold;
            cursor: pointer;
        }

        .CellClick {
            font: bold;
            cursor: pointer;
            text-align: right;
            font-weight: bold;
        }

        .footer-font {
            font-weight: bold;
            background-color: yellowgreen;
            text-align: right !important;
        }

        .alignright {
            text-align: right !important;
            font-weight: bold;
        }
        .Margin {
            margin-top: 5px;
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

    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <section class="content-header">
        <h1>
            Rejected Bids
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Rejected Bids </li>
        </ol>
    </section>
    <br />

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>


                <!-- Start : Purchased Items Modal -->
                <div id="mdlItems" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 60%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">


                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->


                            <div class="modal-body">

                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="text-green text-bold">PURCHASED ITEMS</h4>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-xs-12">


                                                <div>
                                                    &nbsp
                                                </div>

                                                <!-- Start : Items Table -->
                                                <div style="color: black;">

                                                    <asp:GridView ID="gvPurchasedItems" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No Purchase History records."
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
                                                            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:MM/dd/yyyy}" />

                                                        </Columns>
                                                    </asp:GridView>

                                                </div>
                                                <!-- End : Quotation Table -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Purchased Items Modal -->

                <!-- Start : Quotations Modal -->
                <div id="mdlQuotationNew" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 95%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">SubmItted Quotations</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <!-- Start : Quotation Table -->
                                        <div style="color: black; overflow-x:scroll;">

                                            <asp:GridView ID="gvItems" runat="server"
                                                CssClass="table gvItems"
                                                GridLines="None" AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="gvItems_RowDataBound" DataKeyNames="BiddingItemId" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
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
                                                                <td scope="col">Actions</td>
                                                                <td scope="col">Performed By</td>
                                                                <td scope="col">Performed On</td>
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
                                                    <%--<asp:BoundField DataField="Qty" HeaderText="Quantity" />--%>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label
                                                                runat="server" Text='<%# Eval("Qty").ToString() +" " + Eval("UnitShortName").ToString() %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EstimatedPrice"
                                                        HeaderText="Estimated Price" />
                                                    <asp:BoundField DataField="QuotationCount"
                                                        HeaderText="Quotations Count" />
                                                    <asp:BoundField DataField="LastSupplierId"
                                                        HeaderText="LastSupplierId" HeaderStyle-CssClass="hidden"
                                                        ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="LastSupplierName" NullDisplayText="Not Found"
                                                        HeaderText="Last Purchased Supplier" />
                                                    <asp:BoundField DataField="LPurchasedPrice" NullDisplayText="Not Found"
                                                        HeaderText="Last Purchased Price" />

                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <%--<asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("IsQuotationSelected").ToString() == "1" && Eval("IsTerminated").ToString() != "1" ? true : false %>'
                                                                Text="QUOTATION SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: green;" Font-Bold="true" />
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("IsTerminated").ToString() == "1" ? true : false %>'
                                                                Text="Terminated" Style="margin-right: 4px; margin-bottom: 4px; color: red;" Font-Bold="true" />
                                                            <br>
                                                            <asp:Button CssClass="btn btn-xs btn-warning btnTerminateItemCl" runat="server" Visible='<%#Eval("IsTerminated").ToString() =="0" ?true:false%>'
                                                                Text="Terminate" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button>
                                                            <br>--%>
                                                            <asp:Button CssClass="btn btn-xs btn-info btnPurchased" runat="server"
                                                                ID="btnPurchased" Text="Purchase History" OnClick="btnViewItems_Click"
                                                                Style="margin-top: 3px; width: 100px;"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PerformedByName" NullDisplayText="Not Found"
                                                        HeaderText="Performed By" />
                                                    <asp:BoundField DataField="PerformedOn" NullDisplayText="Not Found" DataFormatString="{0:MM/dd/yyyy}"
                                                        HeaderText="Performed On" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label
                                                                runat="server"
                                                                Text='<%# Eval("IsTerminated").ToString() == "1" ? Eval("TerminatedByName").ToString() : Eval("IsQuotationSelected").ToString() == "1" ? Eval("QuotationSelectedByName").ToString(): "" %>'
                                                                Style="margin-right: 4px; margin-bottom: 4px;" Font-Bold="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label
                                                                runat="server"
                                                                Text='<%# Eval("IsTerminated").ToString() == "1" ? Eval("TerminatedDate").ToString() : Eval("IsQuotationSelected").ToString() == "1" ? Eval("QutationSelectionDate").ToString(): "" %>'
                                                                Style="margin-right: 4px; margin-bottom: 4px;" Font-Bold="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td colspan="100%">
                                                                    <asp:Panel ID="pnlQuotationItems" runat="server" Style="width: 90%; margin-left: 40px; overflow-x: scroll;">
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
                                                                                <asp:BoundField DataField="SupplierMentionedItemName" HeaderText="Supplier Mentioned Item Name" NullDisplayText="Not Found" />
                                                                                <asp:BoundField DataField="QuotationReferenceCode" HeaderText="Reference Code" NullDisplayText="Unavailable" />
                                                                                <asp:BoundField DataField="Description" HeaderText="Description" NullDisplayText="-" />
                                                                                <asp:BoundField DataField="UnitPrice" HeaderText="Evaluated Price" />
                                                                                <%--<asp:BoundField DataField="TotQty" HeaderText="Quantity" />--%>
                                                                                 <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label
                                                                                            runat="server" Text='<%# Eval("TotQty").ToString() +" " + Eval("MeasurementShortName").ToString() %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="SubTotal" HeaderText="Sub-Total" />
                                                                                <asp:BoundField DataField="NbtAmount" HeaderText="NBT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                <asp:BoundField DataField="VatAmount" HeaderText="VAT" />
                                                                                <asp:BoundField DataField="NetTotal" HeaderText="Net-Total" />
                                                                                <asp:BoundField DataField="SpecComply" HeaderText="Complies Specs" />


                                                                                <asp:TemplateField HeaderText="Is Selected" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label
                                                                                            runat="server"
                                                                                            Visible='<%# Eval("IsSelected").ToString() == "1" ? true : false %>'
                                                                                            Text="SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: green;" Font-Bold="true" />

                                                                                        <asp:Label
                                                                                            runat="server"
                                                                                            Visible='<%# Eval("IsSelected").ToString() == "0" ? true : false %>'
                                                                                            Text="NOT SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: red;" Font-Bold="true" />

                                                                                        <%--<asp:Button CssClass="btn btn-xs btn-default btnViewQuotationCl" runat="server"
                                                                                            Text="View Quotation" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button> --%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="ApprovalRemark" HeaderText="Selection Remark" NullDisplayText="-" />

                                                                                <asp:TemplateField HeaderText="Supplier Details" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button CssClass="btn btn-xs btn-info" ID="btnsupplerview" OnClick="btnsupplerview_Click" runat="server"
                                                                                            Text="view" Style="margin-right: 4px;"></asp:Button>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Attachments" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button CssClass="btn btn-xs btn-info " runat="server" OnClick="btnViewAttachments_Click"
                                                                                            Text="View"></asp:Button>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <%-- <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label
                                                                                            runat="server"
                                                                                            Visible='<%# Eval("IsSelected").ToString() == "1" ? true : false %>'
                                                                                            Text="SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: green;" Font-Bold="true" />
                                                                                        <asp:Button CssClass="btn btn-xs btn-default btnViewQuotationCl" runat="server"
                                                                                            Text="View Quotation" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button> 
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>



                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-2 topMargin">
                                                        <asp:Label runat="server" Text="Tabulation Remark"></asp:Label>

                                                    </div>
                                                    <div class="col-md-5 topMargin">
                                                        <asp:TextBox ID="lblRemark" runat="server" Width="80%" TextMode="MultiLine" Rows="4" Text=""></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-5">
                                                        <p class="lead">TABULATION SUMMARY</p>
                                                        <div class="table-responsive">
                                                            <table class="table table-striped">
                                                                <tbody>
                                                                    <colgroup>
                                                                        <col width="30">
                                                                        <col width="200">
                                                                        <tr>
                                                                            <td><b>TOTAL WITHOUT VAT</b></td>
                                                                            <td id="tdTotal" runat="server" class="text-right">
                                                                                <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>VAT</b></td>
                                                                            <td id="tdVAT" runat="server" class="text-right">
                                                                                <asp:Label ID="lblVat" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <%--<tr>
                                                                        <td><b>NBT</b></td>
                                                                        <td id="tdNbt" class="text-right" runat="server">
                                                                            <asp:Label ID="lblNbt" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>--%>
                                                                        <tr>
                                                                            <td><b>NET TOTAL</b></td>
                                                                            <td id="tdNetTotal" runat="server" class="text-right">
                                                                                <asp:Label ID="lblNetTotal" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <%--<tr>
                                                    <td><b>TABULATION REMARK</b></td>
                                                    <td id="tdRemark" class="text-right" runat="server">
                                                        <asp:TextBox ID="lblRemark" runat="server" TextMode="MultiLine" Rows="3" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>--%></col>
                                                                        </col>
                                                                    </colgroup>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <asp:Button CssClass="btn btn-primary pull-right" runat="server"
                                                ID="btnuplodDockNew" Text="Upload Docs" OnClick="btnuplodDock_Click"
                                                Style="margin-top: 50px; margin-right: 5px; width: 100px;"></asp:Button>

                                            <asp:Button CssClass="btn btn-success btnViewRecommendationsCl pull-right" runat="server"
                                                Text="Recomendation"
                                                Style="margin-top: 50px;margin-right: 5px;  width: 120px;"></asp:Button>

                                            <asp:Button Style="margin-top: 50px; margin-right: 5px; width: 100px;" runat="server" Text="Approval"
                                                CssClass="btn btn-warning btnViewApprovalsCl pull-right" />
                                            
                                            <asp:Button CssClass="btn btn-info pull-right " runat="server"
                                                ID="btnPrintNew" Text="Print" OnClick="btnPrint1_Click"
                                                Style="margin-top: 50px; margin-right: 5px; width: 100px; margin-right: 5px;"></asp:Button>



                                        </div>
                                        <!-- End : Quotation Table -->
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Quotations Modal -->


                          <!-- Start : Quotations Modal -->
                <div id="mdlRejectedQuotationNew" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 95%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Submtted Quotations</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <!-- Start : Quotation Table -->
                                        <div style="color: black; overflow:scroll">

                                            <asp:GridView ID="gvRejectedItems" runat="server"
                                                CssClass="table gvRejectedItems"
                                                GridLines="None" AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="gvRejectedItems_RowDataBound" DataKeyNames="BiddingItemId" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
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
                                                                <td scope="col">Actions</td>
                                                                <td scope="col">Performed By</td>
                                                                <td scope="col">Performed On</td>
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
                                                    <%--<asp:BoundField DataField="Qty" HeaderText="Quantity" />--%>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label
                                                                runat="server" Text='<%# Eval("Qty").ToString() +" " + Eval("UnitShortName").ToString() %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EstimatedPrice"
                                                        HeaderText="Estimated Price" />
                                                    <asp:BoundField DataField="QuotationCount"
                                                        HeaderText="Quotations Count" />
                                                    <asp:BoundField DataField="LastSupplierId"
                                                        HeaderText="LastSupplierId" HeaderStyle-CssClass="hidden"
                                                        ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="LastSupplierName" NullDisplayText="Not Found"
                                                        HeaderText="Last Purchased Supplier" />
                                                    <asp:BoundField DataField="LPurchasedPrice" NullDisplayText="Not Found"
                                                        HeaderText="Last Purchased Price" />

                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <%--<asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("IsQuotationSelected").ToString() == "1" && Eval("IsTerminated").ToString() != "1" ? true : false %>'
                                                                Text="QUOTATION SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: green;" Font-Bold="true" />
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("IsTerminated").ToString() == "1" ? true : false %>'
                                                                Text="Terminated" Style="margin-right: 4px; margin-bottom: 4px; color: red;" Font-Bold="true" />
                                                            <br>
                                                            <asp:Button CssClass="btn btn-xs btn-warning btnTerminateItemCl" runat="server" Visible='<%#Eval("IsTerminated").ToString() =="0" ?true:false%>'
                                                                Text="Terminate" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button>
                                                            <br>--%>
                                                            <asp:Button CssClass="btn btn-xs btn-info btnPurchased" runat="server"
                                                                ID="btnPurchased" Text="Purchase History" OnClick="btnViewItemsRejected_Click"
                                                                Style="margin-top: 3px; width: 100px;"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PerformedByName" NullDisplayText="Not Found"
                                                        HeaderText="Performed By" />
                                                    <asp:BoundField DataField="PerformedOn" NullDisplayText="Not Found" DataFormatString="{0:MM/dd/yyyy}"
                                                        HeaderText="Performed On" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label
                                                                runat="server"
                                                                Text='<%# Eval("IsTerminated").ToString() == "1" ? Eval("TerminatedByName").ToString() : Eval("IsQuotationSelected").ToString() == "1" ? Eval("QuotationSelectedByName").ToString(): "" %>'
                                                                Style="margin-right: 4px; margin-bottom: 4px;" Font-Bold="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label
                                                                runat="server"
                                                                Text='<%# Eval("IsTerminated").ToString() == "1" ? Eval("TerminatedDate").ToString() : Eval("IsQuotationSelected").ToString() == "1" ? Eval("QutationSelectionDate").ToString(): "" %>'
                                                                Style="margin-right: 4px; margin-bottom: 4px;" Font-Bold="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td colspan="100%">
                                                                    <asp:Panel ID="pnlQuotationItems" runat="server" Style="width: 90%; margin-left: 40px; overflow-x: auto;">
                                                                        <asp:GridView ID="gvRejectedQuotationItems" runat="server"
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
                                                                                <asp:BoundField DataField="SupplierMentionedItemName" HeaderText="Supplier Mentioned Item Name" NullDisplayText="Not Found" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                <asp:BoundField DataField="QuotationReferenceCode" HeaderText="Reference Code" NullDisplayText="Unavailable" />
                                                                                <asp:BoundField DataField="SupplierAgentName" HeaderText="Supplier Agent" NullDisplayText="-" />
                                                                                <asp:BoundField DataField="ImpRemark" HeaderText="Remark" NullDisplayText="-" />
                                                                                <asp:BoundField DataField="CountryNameImp" HeaderText="Country" NullDisplayText="-" />
                                                                                <%--<asp:BoundField DataField="ImpCurrencyName" HeaderText="Currency" NullDisplayText="-" />--%>
                                                                                  <asp:TemplateField HeaderText="Currency Type">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" Text='<%# Eval("Term").ToString() == "Local" ? "LKR" :Eval("ImpCurrencyName") %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                <asp:BoundField DataField="Mill" HeaderText="Mill" NullDisplayText="-" />
                                                                                <asp:BoundField DataField="Term" HeaderText="Term" NullDisplayText="-" />
                                                                                <asp:BoundField DataField="ImpBrand" HeaderText="Brand" NullDisplayText="-" />
                                                                                <asp:BoundField DataField="ImpClearing" HeaderText="Clearing" NullDisplayText="-" />
                                                                                <asp:BoundField DataField="ImpOther" HeaderText="Other" NullDisplayText="-" />
                                                                                <asp:BoundField DataField="ImpHistory" HeaderText="History" NullDisplayText="-" />
                                                                                <asp:BoundField DataField="ImpEstDelivery" HeaderText="Est Delivery" NullDisplayText="-" />
                                                                                <asp:BoundField DataField="ImpValidity" HeaderText="Validity" NullDisplayText="-" />

                                                                                <asp:BoundField DataField="UnitPrice" HeaderText="Evaluated Price" />
                                                                                <%--<asp:BoundField DataField="TotQty" HeaderText="Quantity" />--%>
                                                                                 <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label
                                                                                            runat="server" Text='<%# Eval("TotQty").ToString() +" " + Eval("MeasurementShortName").ToString() %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="SubTotal" HeaderText="Sub-Total" />
                                                                                <asp:BoundField DataField="NbtAmount" HeaderText="NBT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                <asp:BoundField DataField="VatAmount" HeaderText="VAT" />
                                                                                <asp:BoundField DataField="NetTotal" HeaderText="Net-Total" />
                                                                                <asp:BoundField DataField="SpecComply" HeaderText="Complies Specs" />


                                                                                <asp:TemplateField HeaderText="Is Selected" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label
                                                                                            runat="server"
                                                                                            Visible='<%# Eval("IsSelected").ToString() == "1" ? true : false %>'
                                                                                            Text="SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: green;" Font-Bold="true" />

                                                                                        <asp:Label
                                                                                            runat="server"
                                                                                            Visible='<%# Eval("IsSelected").ToString() == "0" ? true : false %>'
                                                                                            Text="NOT SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: red;" Font-Bold="true" />

                                                                                        <%--<asp:Button CssClass="btn btn-xs btn-default btnViewQuotationCl" runat="server"
                                                                                            Text="View Quotation" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button> --%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="ApprovalRemark" HeaderText="Selection Remark" NullDisplayText="-" />

                                                                                <asp:TemplateField HeaderText="Supplier Details" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button CssClass="btn btn-xs btn-info" ID="btnsupplerview" OnClick="btnsupplerview_Click" runat="server"
                                                                                            Text="view" Style="margin-right: 4px;"></asp:Button>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Attachments" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button CssClass="btn btn-xs btn-info " runat="server" OnClick="btnViewAttachmentsRejected_Click"
                                                                                            Text="View"></asp:Button>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <%-- <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label
                                                                                            runat="server"
                                                                                            Visible='<%# Eval("IsSelected").ToString() == "1" ? true : false %>'
                                                                                            Text="SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: green;" Font-Bold="true" />
                                                                                        <asp:Button CssClass="btn btn-xs btn-default btnViewQuotationCl" runat="server"
                                                                                            Text="View Quotation" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button> 
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>



                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-2 topMargin">
                                                        <asp:Label runat="server" Text="Tabulation Remark"></asp:Label>

                                                    </div>
                                                    <div class="col-md-5 topMargin">
                                                        <asp:TextBox ID="lblRemarkR" runat="server" Width="80%" TextMode="MultiLine" Rows="4" Text=""></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-5">
                                                        <p class="lead">TABULATION SUMMARY</p>
                                                        <div class="table-responsive">
                                                            <table class="table table-striped">
                                                                <tbody>
                                                                    <colgroup>
                                                                        <col width="30">
                                                                        <col width="200">
                                                                        <tr>
                                                                            <td><b>TOTAL WITHOUT VAT</b></td>
                                                                            <td id="td1" runat="server" class="text-right">
                                                                                <asp:Label ID="lblTotalR" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>VAT</b></td>
                                                                            <td id="td2" runat="server" class="text-right">
                                                                                <asp:Label ID="lblVatR" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <%--<tr>
                                                                        <td><b>NBT</b></td>
                                                                        <td id="td3" class="text-right" runat="server">
                                                                            <asp:Label ID="lblNbtR" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>--%>
                                                                        <tr>
                                                                            <td><b>NET TOTAL</b></td>
                                                                            <td id="td4" runat="server" class="text-right">
                                                                                <asp:Label ID="lblNetTotalR" runat="server" Text=""></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <%--<tr>
                                                    <td><b>TABULATION REMARK</b></td>
                                                    <td id="tdRemark" class="text-right" runat="server">
                                                        <asp:TextBox ID="lblRemark" runat="server" TextMode="MultiLine" Rows="3" Text=""></asp:TextBox>
                                                    </td>
                                                </tr>--%></col>
                                                                        </col>
                                                                    </colgroup>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            
                                            <asp:Button CssClass="btn btn-info pull-right " runat="server"
                                                ID="btnPrintNewR" Text="Print" OnClick="btnPrint1_Click"
                                                Style="margin-top: 50px; margin-right: 5px; width: 100px; margin-right: 5px;"></asp:Button>



                                        </div>
                                        <!-- End : Quotation Table -->
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Quotations Modal -->



                <!-- Start : Attachment Modal -->
                <div id="mdlAttachments" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Attachments Quotations</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
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
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Attachment Modal -->




                <!-- Start : Recommendation Modal -->
                <div id="mdlApprovals" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 90%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close closeApproval" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <%--<h4 class="modal-title">Purchasing Committee Approval</h4>--%>
                                <h4 class="modal-title">Committee Approval</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:GridView ID="gvApproval" runat="server" CssClass="ChildGrid" Width="100%"
                                                    GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Recommendations Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="ApprovalId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="DesignationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="DesignationName" HeaderText="Required From" NullDisplayText="-" />
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#Eval("IsApproved").ToString() =="1"?"Approved": Eval("IsApproved").ToString() =="2"?"Rejected": "Pending"%>' Font-Bold="true" Style='<%#Eval("IsApproved").ToString() =="1"?"color: green": Eval("IsApproved").ToString() =="2"?"color: Red": "color: gold"%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Was Overridden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#Eval("WasOverriden").ToString() =="1"?"Yes": "No"%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ApprovedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="ApprovedByName" HeaderText="Approved By" NullDisplayText="-" />
                                                        <%--<asp:BoundField DataField="ApprovedDate" HeaderText="Recommended Date" NullDisplayText="-" DataFormatString='<%$ appSettings:dateTimePattern %>' />--%>
                                                        <asp:TemplateField HeaderText="Approved Date"  >
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#(DateTime)Eval("ApprovedDate") == DateTime.MinValue ? string.Empty : Eval("ApprovedDate").ToString() %>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox Style="margin: 5px; width: 120px;" TextMode="MultiLine" ReadOnly="true" Rows="3" runat="server" Text='<%#Eval("Remarks")%>' Visible='<%#Eval("Remarks") == null? false : true%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Purchasing Committee Decision" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-xs btn-success btnOverrideRecommendationApproveCl" runat="server"
                                                                    ID="Button1" Text="Override and Approve"
                                                                    Style="margin-top: 3px; width: 150px;" Visible='<%#Eval("CanLoggedInUserOverride").ToString() =="1"?true: false%>'></asp:Button>
                                                                <asp:Button CssClass="btn btn-xs btn-danger btnOverrideRecommendationRejectCl" runat="server"
                                                                    ID="Button2" Text="Override and Reject"
                                                                    Style="margin-top: 3px; width: 150px;" Visible='<%#Eval("CanLoggedInUserOverride").ToString() =="1"?true: false%>'></asp:Button>
                                                                <asp:Button CssClass="btn btn-xs btn-success btnApproveCl" runat="server"
                                                                    ID="btnApproveQ" Text="Approve"
                                                                    Style="margin-top: 3px; width: 150px;" Visible='<%#Eval("CanLoggedInUserApprove").ToString() =="1"?true: false%>'></asp:Button>
                                                                <asp:Button CssClass="btn btn-xs btn-danger btnRejectCl" runat="server"
                                                                    ID="btnRejectQ" Text="Reject"
                                                                    Style="margin-top: 3px; width: 150px;" Visible='<%#Eval("CanLoggedInUserApprove").ToString() =="1"?true: false%>'></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                            <div class="modal-footer">
                                <asp:Button ID="btnProcDocView" runat="server" Text="View Docs" CssClass="btn btn-primary pull-right" OnClick="btnProcDocView_Click" Style="margin-right: 10px" />
                             <asp:Button ID="btnOverrideRecomnd" runat="server" Text="Override & Approve" CssClass="btn btn-success pull-right btnOverrideApprove"  Style="margin-right: 10px"  />
                                <asp:Button ID="btnOverriderejct" runat="server" Text="Override & Reject" CssClass="btn btn-danger pull-right btnOverrideReject"  Style="margin-right: 10px" />
                           <asp:Label runat="server" ID="lblTabOverridden" CssClass="label label-info pull-left" Font-Size="15px" Visible="false"></asp:Label>
                                
                            </div>
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Recommendation Modal -->

                <!-- Start : Recommendation Modal -->
                <div id="mdlRecommendations" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 90%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close closeRecommendation" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <%--<h4 class="modal-title">TEC Committee Recommendation</h4>--%>
                              <h4 class="modal-title">Committee Recommendation</h4>

                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">

                                    <div class="col-md-3 col-sm-6 col-xs-12 text-center">
                                        <label><b>Was Recommendation Overrriden</b></label><br />
                                        <asp:Label runat="server" ID="lblWasRecommendationOverriden"></asp:Label>
                                    </div>
                                    <div class="col-md-3 col-sm-6 col-xs-12 text-center">
                                        <label><b>Recommendation Overridden By</b></label><br />
                                        <asp:Label runat="server" ID="lblRecommendationOveriddenByName"></asp:Label>
                                    </div>
                                    <div class="col-md-3 col-sm-6 col-xs-12 text-center">
                                        <label><b>Recommendation Overridden On</b></label><br />
                                        <asp:Label runat="server" ID="lblRecommendationOveriddenOn"></asp:Label>
                                    </div>
                                    <div class="col-md-3 col-sm-6 col-xs-12 text-center">
                                        <label><b>Remarks on Overriding</b></label><br />
                                        <asp:Label runat="server" ID="lblRecommendationOverridingRemarks"></asp:Label>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <br />
                                        <div class="form-group">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:GridView ID="gvRecommenations" runat="server" CssClass="ChildGrid" Width="100%"
                                                    GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Recommendations Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="RecommendationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="DesignationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="DesignationName" HeaderText="Required From" NullDisplayText="-" />
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#Eval("IsRecommended").ToString() =="1"?"Recommended": Eval("IsRecommended").ToString() =="2"?"Rejected": "Pending"%>' Font-Bold="true" Style='<%#Eval("IsRecommended").ToString() =="1"?"color: green": Eval("IsRecommended").ToString() =="2"?"color: Red": "color: gold"%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Was Overridden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#Eval("WasOverriden").ToString() =="1"?"Yes": "No"%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="RecommendedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="RecommendedByName" HeaderText="Recommended By" NullDisplayText="-" />
                                                        <%--<asp:BoundField DataField="RecommendedDate" HeaderText="Recommended Date" NullDisplayText="-" DataFormatString='<%$ appSettings:dateTimePattern %>' />--%>
                                                         <asp:TemplateField HeaderText="Recommended Date"  >
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#(DateTime)Eval("RecommendedDate") == DateTime.MinValue?string.Empty: Eval("RecommendedDate").ToString()%>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox Style="margin: 5px; width: 400px;" TextMode="MultiLine" ReadOnly="true" Rows="3" runat="server" Text='<%#Eval("Remarks")%>' Visible='<%#Eval("Remarks") == null? false : true%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnTechdocView" runat="server" Text="View Docs" CssClass="btn btn-primary pull-right" OnClick="btnTechdocView_Click" Style="margin-right: 10px" />
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Recommendation Modal -->

                <!-- Start : Quotations Modal -->
                <div id="mdlRequiredQty" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close Cancelselct" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">ADD Quantity</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">

                                            <label for="exampleInputEmail1" id="itemname"></label>

                                        </div>
                                        <div class="form-group">
                                            <label id="mainQuantity" style="font-weight: bold;" for="exampleInputEmail1"></label>
                                            <input type="hidden" id="TabulationId" />
                                            <input type="hidden" id="QutatuonId" />
                                            <input type="hidden" id="BidId" />
                                            <input type="hidden" id="SupplierId" />
                                            <input type="hidden" id="ItemId" />
                                            <input type="hidden" id="selectedquanty" />
                                            <input type="hidden" id="Rowno" />
                                            <input type="hidden" id="cellno" />
                                            <input type="hidden" id="ISEditedAgian" />
                                            <input type="hidden" id="previousqty" />
                                        </div>

                                        <div class="form-group">

                                            <label for="exampleInputEmail1">Requesting Quantity </label>
                                            <input type="number" id="txtamount" class="form-control input-md" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <input type="button" id="btnadd" class=" btn btn-primary" value="Select" />
                                <input type="button" id="btncancel" class=" btn btn-primary" value="Unselect" />
                                <input type="button" id="btnclose" class="btn btn-danger " value="Cancel" />
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>

                <div id="mdlviewdocsuplod" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close CanceldocUpload" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Upload Proc Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-sm-5">
                                                <asp:FileUpload runat="server" Style="display: inline;" AllowMultiple="true" CssClass="form-control" ID="fileUpload1"></asp:FileUpload>

                                            </div>
                                            <div class="col-sm-2">
                                                <button class="btn btn-info btn-flat clear" id="clearDocs">Clear</button></div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal-footer">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary pull-right" OnClick="btnUpload_Click" Style="margin-right: 10px" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="mdlviewdocsprocCommitee" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close CanceldocView" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvbddifiles" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" EmptyDataText="No Documents Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="filename" HeaderText="File Name" />
                                                        <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="sequenceId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtnDownload" OnClick="lbtnDownload_Click">Download</asp:LinkButton>
                                                                <%--   <iframe id="downloadFrame" style="display:none"></iframe>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
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
                                <h4 class="modal-title">Selection Tabulation Sheet</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <!-- Start : Quotation Table -->
                                        <div class="table-responsive" style="color: black;">
                                            <asp:GridView runat="server" ID="gvQuotations" GridLines="Both" ShowFooter="true"
                                                CssClass="table table-responsive" AutoGenerateColumns="true" SelectedIndex="1" OnRowCreated="gvQuotations_RowCreated"
                                                DataKeyNames="TabulationId" OnRowDataBound="gvQuotations_RowDataBound" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO" ItemStyle-Font-Bold="true" ItemStyle-Width="150px" ItemStyle-CssClass=" left">
                                                        <ItemTemplate>
                                                            <span style="font: bold;">
                                                                <%#Container.DataItemIndex + 1%>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Refno" HeaderText="Tender REF. NO:" />
                                                    <asp:BoundField DataField="TabulationId" HeaderText="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="BidId" HeaderText="BidId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SupplierId" HeaderText="SupplierId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ItemStyle-Font-Bold="true" />
                                                    <asp:TemplateField HeaderText="Attachments">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-default btnViewAttachmentsCl" runat="server"
                                                                Text="View"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier Details">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-success" ID="btnsupplerview" OnClick="btnsupplerview_Click" runat="server"
                                                                Text="view" Style="margin-right: 4px;"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>


                                            </asp:GridView>

                                            <asp:GridView runat="server" ID="gvImpotsQuotations" GridLines="Both" Visible="false" HeaderStyle-Width="300px" ShowFooter="true"
                                                CssClass="table table-responsive" AutoGenerateColumns="true" SelectedIndex="1" OnRowCreated="gvImpotsQuotations_RowCreated"
                                                DataKeyNames="TabulationId" OnRowDataBound="gvImpotsQuotations_RowDataBound" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" Width="1500px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO" ItemStyle-Font-Bold="true" ItemStyle-Width="150px" ItemStyle-CssClass=" left">
                                                        <ItemTemplate>
                                                            <span style="font: bold;">
                                                                <%#Container.DataItemIndex + 1%>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Refno" HeaderText="Tender REF. NO:" />
                                                    <asp:BoundField DataField="TabulationId" HeaderText="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="BidId" HeaderText="BidId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SupplierId" HeaderText="SupplierId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ItemStyle-Font-Bold="true" />
                                                    <asp:BoundField DataField="Agent" HeaderText="Agent Name" ItemStyle-Font-Bold="true" />
                                                    <asp:BoundField DataField="Country" HeaderText="Country" ItemStyle-Font-Bold="true" />
                                                    <asp:BoundField DataField="Brand" HeaderText="Brand" ItemStyle-Font-Bold="true" />
                                                    <asp:BoundField DataField="Currency" HeaderText="Currency" ItemStyle-Font-Bold="true" />
                                                    <asp:TemplateField HeaderText="Attachments">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-default btnViewAttachmentsCl" runat="server"
                                                                Text="View"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier Details">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-success" ID="btnsupplerview" OnClick="btnsupplerview_Click" runat="server"
                                                                Text="view" Style="margin-right: 4px;"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>
                                        </div>
                                        <!-- End : Quotation Table -->
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-sm-2">
                                    <label for="exampleInputEmail1">Tabultion Remark </label>
                                </div>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ErrorMessage="Remark Is required" runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true" InitialValue="" ControlToValidate="txtareaRemark" ValidationGroup="btnUpdatefinish">*</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtareaRemark" class=" form-control" TextMode="multiline" Columns="30" Rows="5" runat="server" />
                                </div>

                            </div>
                            <div class="modal-footer">
                                <asp:Button CssClass="btn btn-xs btn-success" runat="server"
                                    ID="btnPrint" Text="print" OnClick="btnPrint_Click"
                                    Style="margin-top: 3px; width: 100px;"></asp:Button>
                                <asp:Button CssClass="btn btn-xs btn-primary" ValidationGroup="btnUpdatefinish" runat="server"
                                    ID="btnUpdatefinish" Text="Update Selection" OnClick="btnUpdatefinish_Click"
                                    Style="margin-top: 3px; width: 100px;"></asp:Button>
                                <asp:Button CssClass="btn btn-xs btn-info btnViewRecommendationsCl" runat="server"
                                    Text="Recomendation"
                                    Style="margin-top: 3px; width: 100px;"></asp:Button>
                                <asp:Button Style="margin-top: 3px; width: 100px;" runat="server" Text="Approval" CssClass="btn btn-xs btn-warning btnViewApprovalsCl" />
                                <asp:Button CssClass="btn btn-xs btn-warning" runat="server"
                                    ID="btnuplodDock" Text="Upload Docs" OnClick="btnuplodDock_Click"
                                    Style="margin-top: 3px; width: 100px;"></asp:Button>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Quotations Modal -->

                <div id="mdlRejectedTabulations" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 95%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Selection Tabulation Sheet</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <!-- Start : Quotation Table -->
                                        <div class="table-responsive" style="color: black;">
                                            <asp:GridView runat="server" ID="gvrjectedTabulationsheet" GridLines="Both"
                                                CssClass="table table-responsive" AutoGenerateColumns="true" SelectedIndex="1" ShowFooter="true"
                                                DataKeyNames="TabulationId" OnRowDataBound="gvrjectedTabulationsheet_RowDataBound" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO" ItemStyle-Font-Bold="true" ItemStyle-Width="150px" ItemStyle-CssClass=" left">
                                                        <ItemTemplate>
                                                            <span style="font: bold;">
                                                                <%#Container.DataItemIndex + 1%>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Refno" HeaderText="Tender REF. NO:" />
                                                    <asp:BoundField DataField="TabulationId" HeaderText="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="BidId" HeaderText="BidId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SupplierId" HeaderText="SupplierId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ItemStyle-Font-Bold="true" />
                                                    <asp:TemplateField HeaderText="Attachments" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-default btnViewAttachmentsCl2" runat="server"
                                                                Text="View"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier Details" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-success" ID="btnsupplerview" OnClick="btnsupplerview_Click" runat="server"
                                                                Text="view" Style="margin-right: 4px;"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>


                                            </asp:GridView>
                                        </div>
                                        <!-- End : Quotation Table -->
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button CssClass="btn btn-xs btn-success" runat="server"
                                    ID="btnreprint" Text="print" OnClick="btnreprint_Click"
                                    Style="margin-top: 3px; width: 100px;"></asp:Button>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>

                <div id="mdlviewdocs" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvViewdocsrejected" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" EmptyDataText="No Documents Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="filename" HeaderText="File Name" />
                                                        <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="sequenceId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtnDownload" OnClick="lbtnDownload_Click">Download</asp:LinkButton>
                                                                <%--   <iframe id="downloadFrame" style="display:none"></iframe>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Start : Section -->
                <section class="content" style="padding-top: 0px">
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
                                        <strong>Purchase Type : </strong>
                                        <asp:Label ID="lblPurchaseType" runat="server" Text=""></asp:Label><br />
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
                                        <asp:Panel ID="panelParentPr" runat="server" Visible ="false">
                                             <strong>Parent PR : </strong>
                                                <asp:Label ID="lblParentPr" runat="server" Text=""></asp:Label><br />
                                                  </asp:Panel>
                                    </address>
                                </div>
                            </div>
                                    
                                </div>
                                <!-- End : Box Body -->
                                <!-- Start : Box Footer -->
                                <%--<div class="box-footer">
                                    <a class="btn btn-info pull-right" href="ViewPrForQuotationConfirmation.aspx"
                                        style="margin-right:10px">Done</a>
                                </div>--%>
                                <!-- End : Box Footer -->
                            </div>
                            <!-- End : Box -->

                             <div class="box box-info">
                                <!-- Start : Box Header-->
                                <div class="box-header with-border">
                                    <h3 class="box-title"> Rejected Tabulation Sheets</h3>
                                </div>
                                <!-- End : Box Header -->
                                <!-- Start : Box Body-->
                                <div class="box-body">

                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvRejectedTabulations" runat="server" CssClass="table table-responsive" GridLines="None" 
                                                    AutoGenerateColumns="false" EmptyDataText="No Tabulation Rejected" DataKeyNames="TabulationId" Caption="Rejected Tabulation"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                    <Columns>
                                                       <asp:BoundField DataField="TabulationId" HeaderText="TabulationId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="PrId" HeaderText="PrId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="Bid Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# "B"+Eval("BidCode").ToString() %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Tabulation Recommended">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsRecommended").ToString() =="1" ? "YES":"NO" %>'
                                                                    ForeColor='<%# Eval("IsRecommended").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Recommendation Docs">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="btnRecDocs" Text="Download"  OnClick="btnRecDocs_Click"
                                                                    style="width:100px;"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Tabulation Approved">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsApproved").ToString() =="1" ? "YES":"NO" %>'
                                                                    ForeColor='<%# Eval("IsApproved").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Procurement docs">
                                                            <ItemTemplate>
                                                                 <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="btnProcDoc" Enabled='<%# Eval("IsRecommended").ToString() =="1" ? true: false %>' Text="Download" OnClick="btnProcDoc_Click"
                                                                    style="width:100px;"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CreatedByName" HeaderText="Created By" />
                                                        <asp:BoundField DataField="CreatedOn" HeaderText="Created Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>
                                                                <%--<asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="btnRejectedView" Text="Tabulation Sheet" OnClick="btnRejectedView_Click"
                                                                    style="width:100px;"></asp:Button>--%>

                                                                <asp:Button CssClass="btn btn-xs btn-primary" runat="server" 
                                                                    ID="Button4" Text="Tabulation Sheet" OnClick="btnRejectedViewNew_Click"
                                                                    style="width:100px;"></asp:Button>
                                                                 <asp:Button CssClass="btn btn-xs btn-warning Margin"  runat="server" Visible='<%#Eval("Visible").ToString()== "1"?true:false %>'
                                                                    ID="btnClone1" Text="Clone Bid" OnClientClick='<%#"Clone(event,"+Eval("BidId").ToString()+")" %>'
                                                                    style="width:100px;"></asp:Button>
                                                                 <asp:Button CssClass="btn btn-xs btn-danger Margin" runat="server" Visible='<%#Eval("Visible").ToString()== "1"?true:false %>'
                                                                    ID="btnTerminate1" Text="Terminate" OnClientClick='<%#"Terminate(event,"+Eval("BidId").ToString()+")" %>'
                                                                    style="width:100px;"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                              </div>
                        </div>
                    </div>
                </section>
                <!-- End : Section -->

                <!-- Start : Hidden Fields -->
                <asp:HiddenField ID="hdnSubTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNbtTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnVatTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNetTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnApprovalRemarks" runat="server" />
                <asp:HiddenField ID="hdnRejectRemarks" runat="server" />
                <asp:HiddenField ID="hdnBidId" runat="server" />
                <asp:HiddenField ID="hdnQuotationId" runat="server" />
                <asp:HiddenField ID="hdnTabulationId" runat="server" />
                <asp:HiddenField ID="hdnRecommendationId" runat="server" />
                <asp:HiddenField ID="PurchaseType" runat="server" />
                <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" CssClass="hidden" />
                <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" CssClass="hidden" />
                <asp:Button ID="btnOverrideRecommendationApprove" runat="server" OnClick="btnOverrideRecommendationApprove_Click" CssClass="hidden" />
                <asp:Button ID="btnOverrideRecommendationReject" runat="server" OnClick="btnOverrideRecommendationReject_Click" CssClass="hidden" />
                <asp:Button ID="btnOverrideApprove" runat="server" OnClick="btnOverrideApprove_Click" CssClass="hidden" />
                <asp:Button ID="btnOverrideReject" runat="server" OnClick="btnOverrideReject_Click" CssClass="hidden" />
                <asp:Button ID="btnViewAttachments" runat="server" OnClick="btnViewAttachments_Click" CssClass="hidden" />
                <asp:Button ID="btnViewRecommendations" runat="server" OnClick="btnViewRecommendations_Click" CssClass="hidden" />
                <asp:Button ID="btnViewApprovals" runat="server" OnClick="btnViewApprovals_Click" CssClass="hidden" />
                <asp:Button ID="btnOverrideApprove1" runat="server" OnClick="btnOverrideApprove1_Click" CssClass="hidden" />
                <asp:Button ID="btnOverrideReject1" runat="server" OnClick="btnOverrideReject1_Click" CssClass="hidden" />
                <asp:Button ID="btnTerminate" runat="server" OnClick="btnTerminate_Click" CssClass="hidden" />

                <asp:HiddenField ID="hdnrejected" runat="server" />
                <asp:HiddenField ID="hdnSelectedChanged" runat="server" />
                <asp:HiddenField ID="hdnSlectedQutations" runat="server" />
                <asp:HiddenField ID="hdnIsrejected" runat="server" />

                <asp:HiddenField ID="hpurchaseHistory" runat="server" />
                <asp:HiddenField ID="hAttachments" runat="server" />
                <asp:HiddenField ID="hdoc" runat="server" />

                <asp:Button ID="btnClone" runat="server" OnClick="btnClone_Click" CssClass="hidden" />
                <!-- End : Hidden Fields -->

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnPrint" />
                <asp:PostBackTrigger ControlID="btnUpload" />
                <asp:PostBackTrigger ControlID="btnreprint" />
                <%--<asp:PostBackTrigger ControlID="btnPrintNew" />--%>
                <asp:PostBackTrigger ControlID="btnPrintNewR" />
            </Triggers>
        </asp:UpdatePanel>

    </form>

    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $(function () {

                $('.btnViewAttachmentsCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlQuotations').modal('hide');

                        if ($('#ContentSection_PurchaseType').val() == "L") {
                            var tableRow = $('#ContentSection_gvQuotations').find('> tbody > tr > td:not(table)');
                            $('#ContentSection_hdnQuotationId').val($(tableRow).eq(3).text());
                        }
                        else {
                            var tableRow = $('#ContentSection_gvImpotsQuotations').find('> tbody > tr > td:not(table)');
                            $('#ContentSection_hdnQuotationId').val($(tableRow).eq(3).text());
                        }
                        $('#ContentSection_btnViewAttachments').click();
                    }
                });

                $('#mdlAttachments').on('hide.bs.modal', function () {
                    var hAttachments = $("#ContentSection_hAttachments").val();
                    if (hAttachments == 1) {
                        $('.modal-backdrop').remove();
                        $('#mdlQuotationNew').modal('show');
                    }
                    else if (hAttachments == 2) {
                        $('.modal-backdrop').remove();
                        $('#mdlRejectedQuotationNew').modal('show');
                    }
                });

                $('#mdlItems').on('hide.bs.modal', function () {
                    var hpurchaseHistory = $("#ContentSection_hpurchaseHistory").val();
                    if (hpurchaseHistory == 1) {
                        $('.modal-backdrop').remove();
                        $('#mdlQuotationNew').modal('show');
                    }
                    else if (hpurchaseHistory == 2) {
                        $('.modal-backdrop').remove();
                        $('#mdlRejectedQuotationNew').modal('show');
                    }
                });

                $('#mdlQuotationNew').on('shown.bs.modal', function () {
                    $('#mdlQuotationNew').css('overflow', 'auto');
                    $('body').css("overflow", "hidden");
                    $('body').css("padding-right", "0");
                })

                $('#mdlQuotationNew').on('hidden.bs.modal', function () {
                    $('body').css("overflow", "auto");
                    $('body').css("padding-right", "0");
                })

                $('#mdlRejectedQuotationNew').on('shown.bs.modal', function () {
                    $('#mdlRejectedQuotationNew').css('overflow', 'auto');
                    $('body').css("overflow", "hidden");
                    $('body').css("padding-right", "0");
                })

                $('#mdlRejectedQuotationNew').on('hidden.bs.modal', function () {
                    $('body').css("overflow", "auto");
                    $('body').css("padding-right", "0");
                })

                $('.btnViewRecommendationsCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlQuotationNew').modal('hide');


                        $('#ContentSection_btnViewRecommendations').click();
                    }
                });

                $('.closeRecommendation').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlRecommendations').modal('hide');
                       // $('#mdlQuotations').modal('show');
                        $('#mdlQuotationNew').modal('show');
                    }

                });

                $('.closeApproval').on({
                    click: function () {
                        if ($('#ContentSection_hdnIsrejected').val() == "true") {
                            event.preventDefault();
                            $('#mdlApprovals').modal('hide');
                            $('#mdlRejectedTabulations').modal('show');
                        }
                        else {
                            event.preventDefault();
                            $('#mdlApprovals').modal('hide');
                            //$('#mdlQuotations').modal('show');
                             $('#mdlQuotationNew').modal('show');
                        }
                    }

                });

                $('.btnViewApprovalsCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlQuotationNew').modal('hide');

                        $('#ContentSection_btnViewApprovals').click();
                    }
                });

                //$('#mdlRecommendations').on('hide.bs.modal', function () {
                //    $('.modal-backdrop').remove();
                //    $('#mdlQuotations').modal('show');
                //});

                $('#mdlQuotationNew').on('shown.bs.modal', function () {
                    $('#mdlQuotationNew').css('overflow', 'auto');
                    $('body').css("overflow", "hidden");
                    $('body').css("padding-right", "0");
                })

                $('#mdlQuotationNew').on('hidden.bs.modal', function () {
                    $('body').css("overflow", "auto");
                    $('body').css("padding-right", "0");
                })


                $('.btnOverrideRecommendationApproveCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlApprovals').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnRecommendationId').val($(tableRow).eq(0).text());
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Override and Approve</strong> </br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Approve!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnApprovalRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnOverrideRecommendationApprove').click();
                            } else if (
                                result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlApprovals').modal('show');

                            }
                        });


                    }
                });

                $('.btnOverrideRecommendationRejectCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlApprovals').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnRecommendationId').val($(tableRow).eq(0).text());
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Override and Reject</strong> the Recommendation?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            confirmButtonColor: '#d33',
                            cancelButtonColor: '#3085d6',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Reject!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnRejectRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnOverrideRecommendationReject').click();
                            } else if (
                                result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlApprovals').modal('show');

                            }
                        });


                    }
                });

                 $('.btnOverrideApprove').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlApprovals').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnRecommendationId').val($(tableRow).eq(0).text());
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Override and Approver</strong></br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Approve!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnApprovalRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnOverrideApprove1').click();
                            } else if (
                                result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlApprovals').modal('show');

                            }
                        });


                    }
                });

                $('.btnOverrideReject').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlApprovals').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnRecommendationId').val($(tableRow).eq(0).text());
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Override and Reject</strong> the Approve?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            confirmButtonColor: '#d33',
                            cancelButtonColor: '#3085d6',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Reject!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnRejectRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnOverrideReject1').click();
                            } else if (
                                result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlApprovals').modal('show');

                            }
                        });


                    }
                });

                $('.btnApproveCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlApprovals').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnRecommendationId').val($(tableRow).eq(0).text());
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Approve</strong> selected Quotation?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'  value='Approved'/></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Approve!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnApprovalRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnApprove').click();
                            } else if (
                                result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlApprovals').modal('show');

                            }
                        });


                    }
                });

                $('.btnRejectCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlApprovals').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnRecommendationId').val($(tableRow).eq(0).text());
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Reject</strong> selected Quotation?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            confirmButtonColor: '#d33',
                            cancelButtonColor: '#3085d6',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Reject!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnRejectRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnReject').click();
                            } else if (
                                result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlApprovals').modal('show');

                            }
                        });


                    }
                });

                $('.btnOverrideApproveCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#isSelect').val('1');
                        $('#mdlQuotations').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());
                        var supplierName = $(tableRow).eq(4).text();

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Override and Approve</strong> the quotation from <strong>" + supplierName + "</strong>?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required' value='Approved'/></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Approve!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnApprovalRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $(document).find('.txtSubTotalCl').removeAttr('disabled');
                                $(document).find('.txtNbtCl').removeAttr('disabled');
                                $(document).find('.txtVatCl').removeAttr('disabled');
                                $(document).find('.txtNetTotalCl').removeAttr('disabled');
                                $('#ContentSection_btnOverrideApprove').click();
                            } else if (
                                result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlQuotationNew').modal('show');

                            }
                        });


                    }
                });

                $('.btnOverrideRejectCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#isSelect').val('1');
                        $('#mdlQuotationNew').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());
                        var supplierName = $(tableRow).eq(4).text();

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Override and Reject</strong> the quotation from <strong>" + supplierName + "</strong>?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            confirmButtonColor: '#d33',
                            cancelButtonColor: '#3085d6',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Reject It!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {
                                if ($('#ss').val() == '') {
                                    $('#dd').prop('style', 'color:red');
                                    swal.showValidationError('Remarks Required');
                                    return false;
                                }
                                else {
                                    $('#ContentSection_hdnRejectRemarks').val($('#ss').val());
                                }
                            }
                        }).then((result) => {
                            if (result.value) {

                                $('#ContentSection_btnOverrideReject').click();

                            } else if (
                                result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlQuotationNew').modal('show');

                            }
                        });


                    }
                });


                $('.txtNegotiatePriceCl').on({
                    keyup: function () {
                        calculate(this);
                    }
                });

                $('.txtQtyCl').on({
                    keyup: function () {
                        calculate(this);
                    }
                });
                //$(document).on('keyup', '.txtNegotiatePriceCl', function () {

                //    calculate(this);
                //    event.stopPropagation();
                //});

                function calculate(elmt) {
                    var unitPrice = $(elmt).closest('tr').find('.txtNegotiatePriceCl').val();
                    $(elmt).closest('tr').find('.txtNegotiatePriceCl').value = $(elmt).closest('tr').find('.txtNegotiatePriceCl').val();
                    if (unitPrice == '' || unitPrice == null) {
                        unitPrice = 0;
                    }
                    var qty = 0;
                    if ($(elmt).closest('tr').find('.txtQtyCl').val() != '')
                        qty = parseFloat($(elmt).closest('tr').find('.txtQtyCl').val());
                    var subTot = 0;
                    var nbt = 0;
                    var vat = 0;
                    var netTot = 0;

                    subTot = unitPrice * qty;

                    var chkNbt = $(elmt).closest('tr').find('.chkNbtCl').find('input');
                    var chkVat = $(elmt).closest('tr').find('.chkVatCl').find('input');


                    var rdoNbt204 = $(elmt).closest('tr').find('.rdo204').find('input');
                    var rdoNbt2 = $(elmt).closest('tr').find('.rdo2').find('input');

                    if ($(chkNbt).prop('checked') == true) {
                        if ($(rdoNbt204).prop('checked') == true) {
                            nbt = parseFloat((subTot * 2) / 98);
                        }
                        else {
                            nbt = parseFloat((subTot * 2) / 100);
                        }

                    }

                    if ($(chkVat).prop('checked') == true) {

                        vat = parseFloat((subTot + nbt) * 0.15);
                    }

                    netTot = subTot + nbt + vat;

                    $(elmt).closest('tr').find('.txtSubTotalCl').val(subTot.toFixed(2));
                    $(elmt).closest('tr').find('.txtNbtCl').val(nbt.toFixed(2));
                    $(elmt).closest('tr').find('.txtVatCl').val(vat.toFixed(2));
                    $(elmt).closest('tr').find('.txtNetTotalCl').val(netTot.toFixed(2));

                    var tableRows = $(elmt).closest('tbody').find('> tr:not(:has(>td>table))');

                    var globSubTotal = 0;
                    var globTotalNbt = 0;
                    var globTotalVat = 0;
                    var globNetTotal = 0;

                    for (i = 1; i < tableRows.length; i++) {
                        if ($(tableRows[i]).find('.txtSubTotalCl').val() != '')
                            globSubTotal = globSubTotal + parseFloat($(tableRows[i]).find('.txtSubTotalCl').val());
                        if ($(tableRows[i]).find('.txtNbtCl').val() != '')
                            globTotalNbt = globTotalNbt + parseFloat($(tableRows[i]).find('.txtNbtCl').val());
                        if ($(tableRows[i]).find('.txtVatCl').val() != '')
                            globTotalVat = globTotalVat + parseFloat($(tableRows[i]).find('.txtVatCl').val());
                        if ($(tableRows[i]).find('.txtNetTotalCl').val() != '')
                            globNetTotal = globNetTotal + parseFloat($(tableRows[i]).find('.txtNetTotalCl').val());
                    }

                    var tableRow = $(elmt).closest('table').closest('tr').prev().find('>td:not(tr)');
                    // debugger;

                    $(tableRow).eq(5).text(globSubTotal.toFixed(2));
                    $(tableRow).eq(6).text(globTotalNbt.toFixed(2));
                    $(tableRow).eq(7).text(globTotalVat.toFixed(2));
                    $(tableRow).eq(8).text(globNetTotal.toFixed(2));

                    $('#ContentSection_hdnSubTotal').val(globSubTotal.toFixed(2));
                    $('#ContentSection_hdnNbtTotal').val(globTotalNbt.toFixed(2));
                    $('#ContentSection_hdnVatTotal').val(globTotalVat.toFixed(2));
                    $('#ContentSection_hdnNetTotal').val(globNetTotal.toFixed(2));

                }


                $('.CellClick').click(function (ev) {

                    //if ($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(4).text() == "1") {

                    $('#mdlQuotations').modal('hide');

                    if ($('#ContentSection_PurchaseType').val() == "L") {


                        var lable = $('#ContentSection_gvQuotations tr').eq(0).find('th').eq($(this).index() - 1).text();
                        $('#itemname').text("Item Name : " + lable.split('-')[1]);
                        var selectqty = $('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index()).text();
                        selectqty = selectqty.split('*')[1];

                        if (selectqty != "" && selectqty != undefined) {
                            noselct = parseInt(selectqty);
                            $("#btnadd").attr('value', 'Update');
                            var fullqty = $('#ContentSection_gvQuotations tr').eq(0).find('th').eq($(this).index()).text();

                            var ar = fullqty.split('-')
                            var qty = parseInt(ar[ar.length - 1]);

                            var selectedqty = 0

                            $('#ContentSection_gvQuotations tr td:nth-child(' + ($(this).index() + 1) + ')').each(function () {
                                var IscellSelected = $(this).hasClass("greenBg");
                                console.log($(this).text());
                                if (IscellSelected == true) {
                                    console.log($(this).text().split('*')[1]);
                                    selectedqty = selectedqty + parseInt($(this).text().split('*')[1]);
                                }


                            });
                            qty = qty - selectedqty;
                            $("#mainQuantity").text("Rquired Quantity :" + qty);
                            $("#TabulationId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(2).text());
                            $("#QutatuonId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(3).text());
                            $("#BidId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(4).text());
                            $("#SupplierId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(5).text());
                            $("#ItemId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index() - 1).text());
                            $("#selectedquanty").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(1).text());
                            $("#Rowno").val($(this).parent().index());
                            $("#cellno").val($(this).index());
                            $("#ISEditedAgian").val(1);
                            $("#txtamount").val(selectqty);
                            $("#previousqty").val(selectqty);
                            $('#mdlRequiredQty').modal('show');
                            fullqty = "";

                        }
                        else {

                            var fullqty = $('#ContentSection_gvQuotations tr').eq(0).find('th').eq($(this).index()).text();
                            $("#btnadd").attr('value', 'Add');
                            var ar = fullqty.split('-');
                            var qty = parseInt(ar[ar.length - 1]);

                            var selectedqty = 0

                            $('#ContentSection_gvQuotations tr td:nth-child(' + ($(this).index() + 1) + ')').each(function () {
                                var IscellSelected = $(this).hasClass("greenBg");
                                console.log($(this).text());
                                if (IscellSelected == true) {
                                    selectedqty = selectedqty + parseInt($(this).text().split('*')[1]);
                                }


                            });
                            qty = qty - selectedqty;
                            $("#mainQuantity").text("Rquired Quantity :" + qty);
                            $("#TabulationId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(2).text());
                            $("#QutatuonId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(3).text());
                            $("#BidId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(4).text());
                            $("#SupplierId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(5).text());
                            $("#ItemId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index() - 1).text());
                            $("#selectedquanty").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(1).text());
                            $("#Rowno").val($(this).parent().index());
                            $("#cellno").val($(this).index());
                            $("#ISEditedAgian").val(0);
                            $('#mdlRequiredQty').modal('show');
                            fullqty = "";
                        }
                    }


                    else {


                        var lable = $('#ContentSection_gvImpotsQuotations tr').eq(0).find('th').eq($(this).index() - 1).text();
                        $('#itemname').text("Item Name : " + document.getElementById("ContentSection_gvImpotsQuotations").caption.innerHTML);
                        var selectqty = $('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index()).text();
                        selectqty = selectqty.split('*')[1];

                        if (selectqty != "" && selectqty != undefined) {
                            noselct = parseInt(selectqty);
                            $("#btnadd").attr('value', 'Update');
                            var fullqty = $('#ContentSection_gvImpotsQuotations tr').eq(0).find('th').eq($(this).index()).text();

                            var ar = fullqty.split('-')
                            var qty = parseInt(ar[ar.length - 1]);

                            var selectedqty = 0

                            $('#ContentSection_gvImpotsQuotations tr td:nth-child(' + ($(this).index() + 1) + ')').each(function () {
                                var IscellSelected = $(this).hasClass("greenBg");
                                console.log($(this).text());
                                if (IscellSelected == true) {
                                    console.log($(this).text().split('*')[1]);
                                    selectedqty = selectedqty + parseInt($(this).text().split('*')[1]);
                                }


                            });
                            qty = qty - selectedqty;
                            $("#mainQuantity").text("Rquired Quantity :" + qty);
                            $("#TabulationId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(2).text());
                            $("#QutatuonId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(3).text());
                            $("#BidId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(4).text());
                            $("#SupplierId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(5).text());
                            $("#ItemId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index() - 2).text());
                            $("#selectedquanty").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(1).text());
                            $("#Rowno").val($(this).parent().index());
                            $("#cellno").val($(this).index());
                            $("#ISEditedAgian").val(1);
                            $("#txtamount").val(selectqty);
                            $("#previousqty").val(selectqty);
                            $('#mdlRequiredQty').modal('show');
                            fullqty = "";

                        }
                        else {

                            var fullqty = $('#ContentSection_gvImpotsQuotations tr').eq(0).find('th').eq($(this).index()).text();
                            $("#btnadd").attr('value', 'Add');
                            var ar = fullqty.split('-');
                            var qty = parseInt(ar[ar.length - 1]);

                            var selectedqty = 0

                            $('#ContentSection_gvImpotsQuotations tr td:nth-child(' + ($(this).index() + 1) + ')').each(function () {
                                var IscellSelected = $(this).hasClass("greenBg");
                                console.log($(this).text());
                                if (IscellSelected == true) {
                                    selectedqty = selectedqty + parseInt($(this).text().split('*')[1]);
                                }


                            });
                            qty = qty - selectedqty;
                            $("#mainQuantity").text("Rquired Quantity :" + qty);
                            $("#TabulationId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(2).text());
                            $("#QutatuonId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(3).text());
                            $("#BidId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(4).text());
                            $("#SupplierId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(5).text());
                            $("#ItemId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index() - 2).text());
                            $("#selectedquanty").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(1).text());
                            $("#Rowno").val($(this).parent().index());
                            $("#cellno").val($(this).index());
                            $("#ISEditedAgian").val(0);
                            $('#mdlRequiredQty').modal('show');
                            fullqty = "";
                        }

                    }

                    //}
                    //else {
                    //    $('#mdlQuotations').modal('hide');

                    //    swal({ type: 'error', title: 'ERROR', text: 'No Authority to Select this', showConfirmButton: true })
                    //    .then(function(isConfirm) {
                    //        if (isConfirm) {
                    //            $('#mdlQuotations').modal('show');
                    //            $('div').removeClass('modal-backdrop');
                    //        }
                    //    });



                    //}
                    ev.preventDefault();
                });




                $("#btnclose").click(function (ev) {
                    $('#mdlQuotationNew').modal('show');
                    $('#mdlRequiredQty').modal('hide');
                    $("#mainQuantity").text("");
                    $("#txtamount").val("");
                    ev.preventDefault();

                });

                $("#btnadd").click(function (ev) {

                    if ($('#ContentSection_PurchaseType').val() == "L") {


                        var isEdit = $("#ISEditedAgian").val();
                        if (isEdit != "1") {
                            var requiredqty = parseInt($("#mainQuantity").text().split(':')[1]);

                            if ($("#txtamount").val() != "") {
                                var qty = parseInt($("#txtamount").val());
                                //if (qty <= requiredqty && qty != 0) {

                                var hdnrejectval = $("#ContentSection_hdnrejected").val();
                                if (hdnrejectval != null && hdnrejectval != "") {
                                    var arr = hdnrejectval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);
                                        }
                                    }
                                    $("#ContentSection_hdnrejected").val(arr);
                                }
                                var hdnchangeval = $("#ContentSection_hdnSelectedChanged").val();
                                if (hdnchangeval != null && hdnchangeval != "") {
                                    var arr = hdnchangeval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);
                                        }
                                    }
                                    $("#ContentSection_hdnSelectedChanged").val(arr);
                                }
                                var hdnval = $("#ContentSection_hdnSlectedQutations").val();
                                if (hdnval != null && hdnval != "") {
                                    var b = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    var arr = hdnval.split(",");
                                    var conctarr = $.merge($.merge([], arr), b);
                                    $("#ContentSection_hdnSlectedQutations").val(conctarr);

                                }
                                else {
                                    var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    $("#ContentSection_hdnSlectedQutations").val(a);

                                }

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).removeClass('CellClick');
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).addClass('greenBg');

                                $('#mdlQuotationNew').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).html(val + "*" + qty);
                            }
                            else {
                                $('#mdlRequiredQty').modal('hide');
                                swal({ type: 'error', title: 'ERROR', text: 'Insert value is more than required', showConfirmButton: true })
                                    .then(function (isConfirm) {
                                        if (isConfirm) {
                                            $('#mdlRequiredQty').modal('show');
                                            $('div').removeClass('modal-backdrop');
                                        }
                                    });

                            }
                            //}
                            //else {
                            //    $('#mdlRequiredQty').modal('hide');
                            //    swal({ type: 'error', title: 'ERROR', text: 'Please enter value', showConfirmButton: true })
                            //        .then(function (isConfirm) {
                            //            if (isConfirm) {
                            //                $('#mdlRequiredQty').modal('show');
                            //                $('div').removeClass('modal-backdrop');
                            //            }
                            //        });
                            //}
                        }

                        else {
                            console.log($("#previousqty").val());
                            var requiredqty = parseInt($("#mainQuantity").text().split(':')[1]);
                            var prqty = parseInt($("#previousqty").val());
                            if ($("#txtamount").val() != "") {
                                var qty = parseInt($("#txtamount").val());
                                //if (qty <= (requiredqty + prqty) && qty != 0) {

                                var hdnselectval = $("#ContentSection_hdnSlectedQutations").val();
                                if (hdnselectval != null && hdnselectval != "") {
                                    var arr = hdnselectval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);
                                        }
                                    }
                                    $("#ContentSection_hdnSlectedQutations").val(arr);
                                }
                                var hdnrejectval = $("#ContentSection_hdnrejected").val();
                                if (hdnrejectval != null && hdnrejectval != "") {
                                    var arr = hdnrejectval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);

                                        }
                                    }
                                    $("#ContentSection_hdnrejected").val(arr);
                                }

                                var hdnval = $("#ContentSection_hdnSelectedChanged").val();
                                if (hdnval != null && hdnval != "") {

                                    var arr = hdnval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr[i + 5] = qty;
                                        }
                                    }
                                    $("#ContentSection_hdnSelectedChanged").val(arr);


                                }
                                else {
                                    var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    $("#ContentSection_hdnSelectedChanged").val(a);

                                }

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).removeClass('CellClick');
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).addClass('greenBg');

                                $('#mdlQuotationNew').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).html(val.split('*')[0] + "*" + qty);
                                //    }
                                //    else {
                                //        $('#mdlRequiredQty').modal('hide');
                                //        swal({ type: 'error', title: 'ERROR', text: 'Insert value is more than required', showConfirmButton: true })
                                //        .then(function (isConfirm) {
                                //            if (isConfirm) {
                                //                $('#mdlRequiredQty').modal('show');
                                //                $('div').removeClass('modal-backdrop');
                                //            }
                                //        });

                                //    }
                            }
                            else {
                                $('#mdlRequiredQty').modal('hide');
                                swal({ type: 'error', title: 'ERROR', text: 'Please enter value', showConfirmButton: true })
                                    .then(function (isConfirm) {
                                        if (isConfirm) {
                                            $('#mdlRequiredQty').modal('show');
                                            $('div').removeClass('modal-backdrop');
                                        }
                                    });
                            }
                        }

                    }
                    else {


                        var isEdit = $("#ISEditedAgian").val();
                        if (isEdit != "1") {
                            var requiredqty = parseInt($("#mainQuantity").text().split(':')[1]);

                            if ($("#txtamount").val() != "") {
                                var qty = parseInt($("#txtamount").val());
                                //if (qty <= requiredqty && qty != 0) {

                                var hdnrejectval = $("#ContentSection_hdnrejected").val();
                                if (hdnrejectval != null && hdnrejectval != "") {
                                    var arr = hdnrejectval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);
                                        }
                                    }
                                    $("#ContentSection_hdnrejected").val(arr);
                                }
                                var hdnchangeval = $("#ContentSection_hdnSelectedChanged").val();
                                if (hdnchangeval != null && hdnchangeval != "") {
                                    var arr = hdnchangeval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);
                                        }
                                    }
                                    $("#ContentSection_hdnSelectedChanged").val(arr);
                                }
                                var hdnval = $("#ContentSection_hdnSlectedQutations").val();
                                if (hdnval != null && hdnval != "") {
                                    var b = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    var arr = hdnval.split(",");
                                    var conctarr = $.merge($.merge([], arr), b);
                                    $("#ContentSection_hdnSlectedQutations").val(conctarr);

                                }
                                else {
                                    var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    $("#ContentSection_hdnSlectedQutations").val(a);

                                }

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).removeClass('CellClick');
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).addClass('greenBg');

                                $('#mdlQuotationNew').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).html(val + "*" + qty);
                            }
                            else {
                                $('#mdlRequiredQty').modal('hide');
                                swal({ type: 'error', title: 'ERROR', text: 'Insert value is more than required', showConfirmButton: true })
                                    .then(function (isConfirm) {
                                        if (isConfirm) {
                                            $('#mdlRequiredQty').modal('show');
                                            $('div').removeClass('modal-backdrop');
                                        }
                                    });

                            }
                            //}
                            //else {
                            //    $('#mdlRequiredQty').modal('hide');
                            //    swal({ type: 'error', title: 'ERROR', text: 'Please enter value', showConfirmButton: true })
                            //        .then(function (isConfirm) {
                            //            if (isConfirm) {
                            //                $('#mdlRequiredQty').modal('show');
                            //                $('div').removeClass('modal-backdrop');
                            //            }
                            //        });
                            //}
                        }

                        else {
                            console.log($("#previousqty").val());
                            var requiredqty = parseInt($("#mainQuantity").text().split(':')[1]);
                            var prqty = parseInt($("#previousqty").val());
                            if ($("#txtamount").val() != "") {
                                var qty = parseInt($("#txtamount").val());
                                //if (qty <= (requiredqty + prqty) && qty != 0) {

                                var hdnselectval = $("#ContentSection_hdnSlectedQutations").val();
                                if (hdnselectval != null && hdnselectval != "") {
                                    var arr = hdnselectval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);
                                        }
                                    }
                                    $("#ContentSection_hdnSlectedQutations").val(arr);
                                }
                                var hdnrejectval = $("#ContentSection_hdnrejected").val();
                                if (hdnrejectval != null && hdnrejectval != "") {
                                    var arr = hdnrejectval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);

                                        }
                                    }
                                    $("#ContentSection_hdnrejected").val(arr);
                                }

                                var hdnval = $("#ContentSection_hdnSelectedChanged").val();
                                if (hdnval != null && hdnval != "") {

                                    var arr = hdnval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr[i + 5] = qty;
                                        }
                                    }
                                    $("#ContentSection_hdnSelectedChanged").val(arr);


                                }
                                else {
                                    var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    $("#ContentSection_hdnSelectedChanged").val(a);

                                }

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).removeClass('CellClick');
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).addClass('greenBg');

                                $('#mdlQuotationNew').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).html(val.split('*')[0] + "*" + qty);
                                //    }
                                //    else {
                                //        $('#mdlRequiredQty').modal('hide');
                                //        swal({ type: 'error', title: 'ERROR', text: 'Insert value is more than required', showConfirmButton: true })
                                //        .then(function (isConfirm) {
                                //            if (isConfirm) {
                                //                $('#mdlRequiredQty').modal('show');
                                //                $('div').removeClass('modal-backdrop');
                                //            }
                                //        });

                                //    }
                            }
                            else {
                                $('#mdlRequiredQty').modal('hide');
                                swal({ type: 'error', title: 'ERROR', text: 'Please enter value', showConfirmButton: true })
                                    .then(function (isConfirm) {
                                        if (isConfirm) {
                                            $('#mdlRequiredQty').modal('show');
                                            $('div').removeClass('modal-backdrop');
                                        }
                                    });
                            }
                        }


                    }


                    ev.preventDefault();

                });

                $("#btncancel").click(function (ev) {

                    console.log($("#previousqty").val());
                    if ($("#previousqty").val() != "" && $("#previousqty").val() != undefined) {
                        var prqty = parseInt($("#previousqty").val());

                        var hdnselectval = $("#ContentSection_hdnSlectedQutations").val();
                        if (hdnselectval != null && hdnselectval != "") {
                            var arr = hdnselectval.split(",");
                            for (var i = 0; i < arr.length; i += 6) {

                                if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                    arr.splice(i, 6);
                                }
                            }
                            $("#ContentSection_hdnSlectedQutations").val(arr);
                        }

                        var hdnchangeval = $("#ContentSection_hdnSelectedChanged").val();
                        if (hdnchangeval != null && hdnchangeval != "") {
                            var arr = hdnchangeval.split(",");
                            for (var i = 0; i < arr.length; i += 6) {

                                if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                    arr.splice(i, 6);
                                }
                            }
                            $("#ContentSection_hdnSelectedChanged").val(arr);
                        }


                        var hdnval = $("#ContentSection_hdnrejected").val();
                        if (hdnval != null && hdnval != "") {

                            var arr = hdnval.split(",");
                            for (var i = 0; i < arr.length; i += 6) {

                                if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                    arr[i + 5] = qty;
                                }
                            }
                            $("#ContentSection_hdnrejected").val(arr);


                        }
                        else {
                            var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                            $("#ContentSection_hdnrejected").val(a);

                        }

                        if ($('#ContentSection_PurchaseType').val() == "L") {
                            var row = parseInt($("#Rowno").val());
                            var cell = parseInt($("#cellno").val());
                            $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).removeClass('greenBg');
                            $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).addClass('CellClick');
                            $('#mdlQuotationNew').modal('show');
                            $('#mdlRequiredQty').modal('hide');
                            $("#mainQuantity").text("");
                            $("#txtamount").val("");
                            $("#previousqty").val("");
                            $("#ISEditedAgian").val("");
                            var val = $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).text();
                            $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).html(val.split('*')[0]);
                        }
                        else {

                            var row = parseInt($("#Rowno").val());
                            var cell = parseInt($("#cellno").val());
                            $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).removeClass('greenBg');
                            $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).addClass('CellClick');
                            $('#mdlQuotationNew').modal('show');
                            $('#mdlRequiredQty').modal('hide');
                            $("#mainQuantity").text("");
                            $("#txtamount").val("");
                            $("#previousqty").val("");
                            $("#ISEditedAgian").val("");
                            var val = $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).text();
                            $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).html(val.split('*')[0]);

                        }

                    }

                    else {
                        $('#mdlRequiredQty').modal('hide');
                        swal({ type: 'error', title: 'ERROR', text: 'Cannot Cancel selection', showConfirmButton: true })
                            .then(function (isConfirm) {
                                if (isConfirm) {
                                    $('#mdlRequiredQty').modal('show');
                                    $('div').removeClass('modal-backdrop');
                                }
                            });
                    }

                    ev.preventDefault();

                });

                $('.Cancelselct').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlRequiredQty').modal('hide');
                        $('#mdlQuotationNew').modal('show');
                        $('div').removeClass('modal-backdrop');
                    }

                });


                $('#clearDocs').on({
                    click: function () {
                        event.preventDefault();
                        $('#ContentSection_fileUpload1').val("");
                    }

                });
                $('.CanceldocView').on({
                    click: function () {
                        event.preventDefault();

                        var hdoc = $("#ContentSection_hdoc").val();
                    if (hdoc == 1) {
                            $('#mdlviewdocsprocCommitee').modal('hide');
                            $('#mdlRecommendations').modal('show');
                            $('div').removeClass('modal-backdrop');

                        }
                        else if (hdoc == 2) {
                             $('#mdlviewdocsprocCommitee').modal('hide');
                            $('#mdlApprovals').modal('show');
                            $('div').removeClass('modal-backdrop');
                        }
                    }

                });
                $('.CanceldocUpload').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlviewdocsuplod').modal('hide');
                        //$('#mdlQuotations').modal('show');
                        $('#mdlQuotationNew').modal('show');
                        $('div').removeClass('modal-backdrop');
                    }

                });



            });


        });

        function Clone(e, Bidid) {
           e.preventDefault();
           $('#ContentSection_hdnBidId').val(Bidid);

           swal.fire({
               title: 'Are you sure?',
                            html: "Are you sure you want to Clone this bid?</br></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
               
           }
           ).then((result) => {
               if (result.value) {
                                $('#ContentSection_btnClone').click();
                            }
           });


       }


        function Terminate(e, Bidid) {
           e.preventDefault();
           $('#ContentSection_hdnBidId').val(Bidid);

           swal.fire({
               title: 'Are you sure?',
                            html: "Are you sure you want to Terminate this bid?</br></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
               
           }
           ).then((result) => {
               if (result.value) {
                                $('#ContentSection_btnTerminate').click();
                            }
           });


       }

    </script>

</asp:Content>
