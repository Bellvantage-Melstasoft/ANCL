<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompareQuotations.aspx.cs" Inherits="BiddingSystem.CompareQuotations" %>

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

        #ContentSection_gvQuotationItems > tbody > tr:nth-child(2n+1) > td:not(table) {
            border-bottom: 1px solid #555555;
            border-top: 1px solid #f8f8f8;
        }

        #ContentSection_gvImpotsQuotations > tbody {
            background-color: #fbfafa !important;
            text-align: left;
            font-weight: bold;
        }

        #ContentSection_gvImpotsQuotations th {
            text-align: center;
        }


        #ContentSection_gvImpotsQuotations > tbody > tr:nth-child(2n+1) > td:not(table) {
            border-bottom: 1px solid #555555;
            border-top: 1px solid #f8f8f8;
        }

        .greenBg {
            background: #7bf768;
            font-weight: bold;
            cursor: pointer;
            text-align: right;
        }

        .CellClick {
            font-weight: bold;
            cursor: pointer;
            text-align: right;
        }

        .footer-font {
            font-weight: bold;
            background-color: yellowgreen;
            text-align: left !important;
        }

        .alignright {
            text-align: right !important;
            font-weight: bold;
        }

        caption {
            font-weight: bold;
            font-size: large;
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
           Compare Quotation To Select Bid
            <small></small>
                                                                                       
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Bid Comparison </li>
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
                                                            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />

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
                <div id="mdlQuotationsNew" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
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
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <!-- Start : Quotation Table -->
                                        <div style="color: black;">

                                            <asp:GridView ID="gvItem" runat="server"
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
                                                    <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                                    <asp:BoundField DataField="EstimatedPrice"
                                                        HeaderText="Estimated Price" />
                                                    <asp:BoundField DataField="QuotationCount"
                                                        HeaderText="Quotations Count" />
                                                    <asp:BoundField DataField="LSupplierId"
                                                        HeaderText="LastSupplierId" HeaderStyle-CssClass="hidden"
                                                        ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="LastSupplierName" NullDisplayText="Not Found"
                                                        HeaderText="Last Purchased Supplier" />
                                                    <asp:BoundField DataField="LPurchasedPrice" NullDisplayText="Not Found"
                                                        HeaderText="Last Purchased Price" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>

                                                            <asp:Button CssClass="btn btn-xs btn-info btnPurchased" runat="server"
                                                                ID="btnPurchased" Text="Purchase History" OnClick="btnViewItems_Click"
                                                                Style="margin-top: 3px; width: 100px;"></asp:Button>

                                                            <%--<asp:Button CssClass="btn btn-xs btn-warning btnTerminateItemCl" runat="server" Visible='<%#Eval("IsQuotationSelected").ToString() =="0" && Eval("IsTerminated").ToString() =="0" ?true:false%>'
                                                                Text="Terminate" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button>
                                                            <br>
                                                            
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("IsQuotationSelected").ToString() == "1" ? true : false %>'
                                                                Text="QUOTATION SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: green;" Font-Bold="true" />
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("IsTerminated").ToString() == "1" ? true : false %>'
                                                                Text="Terminated" Style="margin-right: 4px; margin-bottom: 4px; color: red;" Font-Bold="true" />
                                                            --%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td colspan="100%">
                                                                    <asp:Panel ID="pnlQuotationItems" runat="server" Style="margin-left: 40px; overflow-x: auto;">
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
                                                                                <asp:BoundField DataField="Description" HeaderText="Description" NullDisplayText="-" />

                                                                                <%--<asp:BoundField DataField="UnitPrice" HeaderText="Quoted Price" />--%>
                                                                                <asp:TemplateField HeaderText="Quoted Price">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblUnitPrice" CssClass="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice").ToString() %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Requesting qty" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtRequestingQty" CssClass="txtRequestingQty" runat="server" Width="80px" onkeyup="Calculation(this)"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sub-Total">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSubTotal" CssClass="lblSubTotal" runat="server" Text=""></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="NBT">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNbt" CssClass="lblNbt" runat="server" Text=""></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="VAT">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblVat" CssClass="lblVat" runat="server" Text=""></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Net-Total">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNetTotal" CssClass="lblNetTotal" runat="server" Text=""></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <%--<asp:BoundField DataField="SubTotal" HeaderText="Sub-Total" />
                                                                                <asp:BoundField DataField="NbtAmount" HeaderText="NBT" />
                                                                                <asp:BoundField DataField="VatAmount" HeaderText="VAT" />
                                                                                <asp:BoundField DataField="NetTotal" HeaderText="Net-Total" />--%>
                                                                                <asp:BoundField DataField="SpecComply" HeaderText="Complies Specs" />

                                                                                <asp:TemplateField HeaderText="Attachments" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button CssClass="btn btn-xs btn-info " OnClick="btnViewAttachmentsnew_Click" runat="server"
                                                                                            Text="View"></asp:Button>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Supplier Details" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button CssClass="btn btn-xs btn-info" ID="btnsupplerview" OnClick="btnsupplerview_Click" runat="server"
                                                                                            Text="view" Style="margin-right: 4px;"></asp:Button>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button CssClass="btn btn-xs btn-warning" runat="server" AutoPostback="true"
                                                                                            Text="Select" Style="margin-right: 4px; margin-bottom: 4px;" OnClientClick="SelectQuotation(this)"></asp:Button>

                                                                                          
                                                                                        
                                                                                        <%--<asp:Label
                                                                                            runat="server"
                                                                                            Visible='<%# Eval("IsSelected").ToString() == "1" ? true : false %>'
                                                                                            Text="SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: green;" Font-Bold="true" />
                                                                                        <asp:Button CssClass="btn btn-xs btn-success btnSelectCl" runat="server" Visible='<%#Eval("Actions").ToString() =="1"?true:false%>'
                                                                                            Text="Select" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button>
                                                                                        <asp:Button CssClass="btn btn-xs btn-danger btnRejectCl" runat="server" Visible='<%#Eval("Actions").ToString() =="1" && Eval("ShowReject").ToString() =="1"?true:false%>'
                                                                                            Text="Reject" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button>
                                                                                        <span style='color: red; <%#Eval("Actions").ToString() =="1" || Eval("IsBidItemSelected").ToString() =="1" ?"Display:none;": Eval("Actions").ToString() =="0" && Eval("ShowReject").ToString() =="0"?"Display:none;":""%>'><b>Reject previous to select this</b></span>
                                                                                        <asp:Button CssClass="btn btn-xs btn-default btnViewQuotationCl" runat="server"
                                                                                            Text="View Quotation" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button>
                                                                                        --%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

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

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <%-- <asp:Button CssClass="btn btn-sm btn-success btnPrintCl" runat="server" Text="Print"
                                                Style="margin-top: 3px; width: 100px;"></asp:Button>
                                            --%>
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
                                <input type="button" id="btnadd" class=" btn btn-primary" value="Add" />
                                <input type="button" id="btnclose" class="btn btn-danger " value="Cancel" />
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Attachment Modal -->
                <div id="mdlviewdocsuplod" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close CanceldocUpload" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Upload Tec Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-sm-5">
                                                <div class="form-group">

                                                    <asp:FileUpload runat="server" Style="display: inline;" AllowMultiple="true" CssClass="form-control" ID="fileUpload1"></asp:FileUpload>

                                                </div>

                                            </div>
                                            <div class="col-sm-2">
                                                <button class="btn btn-info btn-flat clear" id="clearDocs">Clear</button>
                                            </div>
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
                <!-- Start : Quotations Modal -->
                <div id="mdlQuotations" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 95%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close Cancelselct" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Submtted Quotations Tabulation Sheet</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <!-- Start : Quotation Table -->
                                        <div class="table-responsive" style="color: black;">
                                            <asp:GridView runat="server" ID="gvQuotations" GridLines="Both" Visible="false"
                                                CssClass="table table-responsive" AutoGenerateColumns="true" SelectedIndex="1" OnRowCreated="gvQuotations_RowCreated"
                                                DataKeyNames="TabulationId" OnRowDataBound="gvQuotations_RowDataBound" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO" ItemStyle-Font-Bold="true" ItemStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <span style="font: bold;">
                                                                <%#Container.DataItemIndex + 1%>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Refno" HeaderText="REF. NO:" />
                                                    <asp:BoundField DataField="TabulationId" HeaderText="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="BidId" HeaderText="BidId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SupplierId" HeaderText="SupplierId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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

                                            <asp:GridView runat="server" ID="gvImpotsQuotations" GridLines="Both" Visible="false" HeaderStyle-Width="300px"
                                                CssClass="table table-responsive" AutoGenerateColumns="true" SelectedIndex="1" OnRowCreated="gvImpotsQuotations_RowCreated"
                                                DataKeyNames="TabulationId" OnRowDataBound="gvImpotsQuotations_RowDataBound" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" Width="1500px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO" ItemStyle-Font-Bold="true" ItemStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <span style="font: bold;">
                                                                <%#Container.DataItemIndex + 1%>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Refno" HeaderText="Tender REF. NO:" />
                                                    <asp:BoundField DataField="TabulationId" HeaderText="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="BidId" HeaderText="BidId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SupplierId" HeaderText="SupplierId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                            <div class="row" style="padding-left: 76px; display: none;">
                                <div class="col-md-2" style="width: 12%;">
                                    <label id="">Total Amount </label>
                                </div>
                                <div class="col-md-2">
                                    <label id="lblTotalAmountDisplay">0.00 </label>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-2" style="padding-left: 76px; padding-top: 19px">
                                    <label for="exampleInputEmail1">Tabultion Remark </label>
                                </div>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true" InitialValue="" ControlToValidate="txtareaRemark" ValidationGroup="btnfinish">* Please Fill this Field</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtareaRemark" class=" form-control" TextMode="multiline" Columns="30" Rows="5" runat="server" />
                                </div>

                            </div>
                            <div class="modal-footer">
                                <asp:Button CssClass="btn btn-xs btn-success" runat="server"
                                    ID="btnPrint" Text="print" OnClick="btnPrint_Click"
                                    Style="margin-top: 3px; width: 100px;"></asp:Button>
                                <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                    ID="btnfinish" Text="Finish Selection" ValidationGroup="btnfinish" OnClick="btnfinish_Click"
                                    Style="margin-top: 3px; width: 100px;"></asp:Button>
                                <asp:Button CssClass="btn btn-xs btn-danger" runat="server"
                                    ID="btnclear" Text="clear" OnClick="btnclear_Click"
                                    Style="margin-top: 3px; width: 100px;"></asp:Button>
                                <%--<asp:Button CssClass="btn btn-xs btn-default" runat="server"
                                                ID="btnfileUpload" Text="Uploadfile" Visible="false" OnClick="btnfileUpload_Click"
                                                Style="margin-top: 3px; width: 100px;"></asp:Button>--%>
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
                                            <asp:GridView runat="server" ID="gvrjectedTabulationsheet" GridLines="Both" Width="1000px"
                                                CssClass="table table-responsive" AutoGenerateColumns="true" SelectedIndex="1" ShowFooter="true"
                                                DataKeyNames="TabulationId" OnRowDataBound="gvrjectedTabulationsheet_RowDataBound" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO" ItemStyle-Font-Bold="true" ItemStyle-Width="150px">
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
                    <asp:Panel ID="pnlView" runat="server">
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
                                                    OnRowDataBound="gvBids_RowDataBound" GridLines="None"
                                                    AutoGenerateColumns="false" DataKeyNames="BidId" Caption="Bids for Purchase Request"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
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
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>'/>
                                                        <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
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
                                                        <asp:BoundField DataField="NoOfRejectedQuotations" HeaderText="Rejected Tabulation Count" />
                                                        <asp:TemplateField HeaderText="Is Tabulation Selected">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsQuotationSelected").ToString() =="1" ? "Selected":"Not Selected" %>'
                                                                    ForeColor='<%# Eval("IsQuotationSelected").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>
                                                                <%--<asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="btnView" Text="Tabulation Sheet" OnClick="btnView_Click"
                                                                    style="width:100px;" Visible='<%# Eval("IsQuotationSelected").ToString() =="1" ? false:true %>'></asp:Button>
                                                              --%>
                                                                <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="Button1" Text="Tabulation Sheet" OnClick="btnSelectionQuotation_Click"
                                                                    style="width:100px;" Visible='<%# Eval("IsQuotationSelected").ToString() =="1" ? false:true %>'></asp:Button>
                                                              

                                                                 <asp:Button CssClass="btn btn-xs btn-warning btnReopenCl" runat="server" Text="Reopen"
                                                                    style="margin-top:3px; width:100px;" Visible='<%# Eval("IsQuotationSelected").ToString() =="1" ? false:true %>'></asp:Button>
                                                                
                                                                 <%-- <asp:Button CssClass="btn btn-xs btn-warning" runat="server"
                                                                   ID="btnReset" Text="Reset" OnClick="btnReset_Click"
                                                                    style="margin-top:3px; width:100px;" Visible="false"></asp:Button>--%>
                                                                  
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
                                                    OnRowDataBound="gvRejectedBidsAtApproval_RowDataBound" GridLines="None"
                                                    AutoGenerateColumns="false" DataKeyNames="BidId" Caption="Bids Rejected At Tabulation Recommendation"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
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
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
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
                                                        <asp:BoundField DataField="NoOfRejectedQuotations" HeaderText="Rejected Tabulation Count" />
                                                        <asp:TemplateField HeaderText="Is Tabulation Selected">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsQuotationSelected").ToString() =="1" ? "Selected":"Not Selected" %>'
                                                                    ForeColor='<%# Eval("IsQuotationSelected").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tabulation Status">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsQuotationApproved").ToString() =="1" ? "Selected":"Rejected" %>'
                                                                    ForeColor='<%# Eval("IsQuotationApproved").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="QuotationApprovalRemarks" HeaderText="Rejected Reason" />
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="btnView" Text="Tabulation Sheet" OnClick="btnView_Click"
                                                                    style="width:100px;" Visible='<%# Eval("IsQuotationApproved").ToString() =="1" ? false:true %>'></asp:Button>
                                                                <%--<asp:Button CssClass="btn btn-xs btn-warning" runat="server"
                                                                    ID="btnReset" Text="Resubmit" OnClick="btnReset_Click"
                                                                    style="margin-top:3px; width:100px;"></asp:Button>--%>
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
                                                    OnRowDataBound="gvRejectedBidsAtConfirmation_RowDataBound" GridLines="None"
                                                    AutoGenerateColumns="false" DataKeyNames="BidId" Caption="Bids Rejected At Tabulation Approval"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
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
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
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
                                                        <asp:BoundField DataField="NoOfRejectedQuotations" HeaderText="Rejected Tabulation Count" />
                                                        <asp:TemplateField HeaderText="Is Tabulation Selected">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsQuotationSelected").ToString() =="1" ? "Selected":"Not Selected" %>'
                                                                    ForeColor='<%# Eval("IsQuotationSelected").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tabulation Status">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsQuotationConfirmed").ToString() =="1" ? "Selected":"Rejected" %>'
                                                                    ForeColor='<%# Eval("IsQuotationConfirmed").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="QuotationConfirmationRemarks" HeaderText="Rejected Reason" />
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="btnView" Text="Tabulation Sheet" OnClick="btnView_Click"
                                                                    style="width:100px;" Visible='<%# Eval("IsQuotationConfirmed").ToString() =="1" ? false:true %>'></asp:Button>
                                                               <%-- <asp:Button CssClass="btn btn-xs btn-warning" runat="server"
                                                                    ID="btnReset" Text="Resubmit" OnClick="btnReset_Click"
                                                                    style="margin-top:3px; width:100px;"></asp:Button>--%>
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
                                <div class="box-footer">
                                    <a class="btn btn-info pull-right" href="ViewPrForQuotationComparison.aspx"
                                        style="margin-right:10px">Done</a>
                                </div>
                                <!-- End : Box Footer -->
                            </div>
                            <!-- End : Box -->

<%--                          <div class="box box-info">
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
                                                                <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="btnRejectedView" Text="Tabulation Sheet" OnClick="btnRejectedView_Click"
                                                                    style="width:100px;"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                              </div>--%>
                        </div>
                    </div>
                         </asp:Panel>

                    <asp:Panel ID="pnlQuotationSelection" runat="server">
                         <div class="box box-info">
                             <div class="box-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <!-- Start : Quotation Table -->
                                        <div style="color: black; overflow: scroll">

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
                                                    <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                                    <asp:BoundField DataField="EstimatedPrice"
                                                        HeaderText="Estimated Price" />
                                                    <asp:BoundField DataField="QuotationCount"
                                                        HeaderText="Quotations Count" />
                                                    <asp:BoundField DataField="LSupplierId"
                                                        HeaderText="LastSupplierId" HeaderStyle-CssClass="hidden"
                                                        ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="LastSupplierName" NullDisplayText="Not Found"
                                                        HeaderText="Last Purchased Supplier" />
                                                    <asp:BoundField DataField="LPurchasedPrice" NullDisplayText="Not Found"
                                                        HeaderText="Last Purchased Price" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>

                                                            <asp:Button CssClass="btn btn-xs btn-info btnPurchased" runat="server"
                                                                ID="btnPurchased" Text="Purchase History" OnClick="btnViewItems_Click"
                                                                Style="margin-top: 3px; width: 100px;"></asp:Button>

                                                           
                                                            <%--<asp:Button CssClass="btn btn-xs btn-warning btnTerminateItemCl" runat="server" Visible='<%#Eval("IsQuotationSelected").ToString() =="0" && Eval("IsTerminated").ToString() =="0" ?true:false%>'
                                                                Text="Terminate" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button>
                                                            <br>
                                                            
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("IsQuotationSelected").ToString() == "1" ? true : false %>'
                                                                Text="QUOTATION SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: green;" Font-Bold="true" />
                                                            <asp:Label
                                                                runat="server"
                                                                Visible='<%# Eval("IsTerminated").ToString() == "1" ? true : false %>'
                                                                Text="Terminated" Style="margin-right: 4px; margin-bottom: 4px; color: red;" Font-Bold="true" />
                                                       --%> 

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td colspan="100%">
                                                                    <asp:Panel ID="pnlQuotationItems" runat="server" Style="margin-left: 10px; overflow-x: auto; ">
                                                                        <asp:GridView ID="gvQuotationItems" runat="server"
                                                                            CssClass="table table-responsive childGridViewCl"
                                                                            GridLines="None" AutoGenerateColumns="false"
                                                                            Caption="Quotations" EmptyDataText="No Quotations Found" RowStyle-BackColor="#f5f2f2" HeaderStyle-BackColor="#525252" HeaderStyle-ForeColor="White" >
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
                                                                                <asp:BoundField DataField="Description" HeaderText="Description" NullDisplayText="-" />
                                                                             
                                                                                <%--<asp:BoundField DataField="UnitPrice" HeaderText="Quoted Price" />--%>
                                                                                <asp:TemplateField HeaderText="Quoted Price" >
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblUnitPrice" CssClass="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice").ToString() %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Requesting qty" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtRequestingQty" CssClass="txtRequestingQty" runat="server"  Width="80px" onkeyup="Calculation(this)" Type="number"></asp:TextBox>
                                                                                 </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sub-Total" >
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSubTotal" CssClass="lblSubTotal" runat="server" Text=""></asp:Label>
                                                                                 </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="NBT" >
                                                                                    <ItemTemplate>
                                                                                       <asp:Label ID="lblNbt" CssClass="lblNbt" runat="server" Text=""></asp:Label>
                                                                                 </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="VAT" >
                                                                                    <ItemTemplate>
                                                                                       <asp:Label ID="lblVat" CssClass="lblVat" runat="server" Text=""></asp:Label>
                                                                                 </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Net-Total">
                                                                                    <ItemTemplate>
                                                                                       <asp:Label ID="lblNetTotal" CssClass="lblNetTotal" runat="server" Text=""></asp:Label>
                                                                                 </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <%--<asp:BoundField DataField="SubTotal" HeaderText="Sub-Total" />
                                                                                <asp:BoundField DataField="NbtAmount" HeaderText="NBT" />
                                                                                <asp:BoundField DataField="VatAmount" HeaderText="VAT" />
                                                                                <asp:BoundField DataField="NetTotal" HeaderText="Net-Total" />--%>
                                                                                <asp:BoundField DataField="SpecComply" HeaderText="Complies Specs" />

                                                                                 <asp:TemplateField HeaderText="Attachments" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                       <asp:Button CssClass="btn btn-xs btn-info " OnClick="btnViewAttachmentsnew_Click" runat="server"
                                                                                        Text="View"></asp:Button>
                                                                                 </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Supplier Details" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                       <asp:Button CssClass="btn btn-xs btn-info" ID="btnsupplerview" OnClick="btnsupplerview_Click" runat="server"
                                                                                        Text="view" Style="margin-right: 4px;"></asp:Button>
                                                                                 </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                  <asp:BoundField DataField="ItemId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"
                                                                                    HeaderText="Item Id"
                                                                                    />
                                                                                <asp:BoundField DataField="TabulationId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"
                                                                                    HeaderText="TabulationId"
                                                                                    />

                                                                                <asp:TemplateField HeaderText="Is Selected" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                       <asp:Label
                                                                                            runat="server" ID="lblError" CssClass="lblSelected"
                                                                                            Text="Not Selected" Style="margin-right: 4px; margin-bottom: 4px; color: Red;" Font-Bold="true" />
                                                                                        
                                                                                 </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                         <asp:Button CssClass="btn btn-xs btn-warning" runat="server" AutoPostBack="false" ID="BtnSelect1"
                                                                                            Text="Select" Style="margin-right: 4px; margin-bottom: 4px;"  OnClientClick='<%#"SelectQuotation(this,"+Eval("ItemId").ToString()+","+Eval("TabulationId").ToString()+","+Eval("QuotationId").ToString()+","+Eval("SupplierId").ToString()+"); return false; "%>' ></asp:Button>
                                                                                        
                                                                                       
                                                                                        <%--<asp:Label
                                                                                            runat="server"
                                                                                            Visible='<%# Eval("IsSelected").ToString() == "1" ? true : false %>'
                                                                                            Text="SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: green;" Font-Bold="true" />
                                                                                        <asp:Button CssClass="btn btn-xs btn-success btnSelectCl" runat="server" Visible='<%#Eval("Actions").ToString() =="1"?true:false%>'
                                                                                            Text="Select" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button>
                                                                                        <asp:Button CssClass="btn btn-xs btn-danger btnRejectCl" runat="server" Visible='<%#Eval("Actions").ToString() =="1" && Eval("ShowReject").ToString() =="1"?true:false%>'
                                                                                            Text="Reject" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button>
                                                                                        <span style='color: red; <%#Eval("Actions").ToString() =="1" || Eval("IsBidItemSelected").ToString() =="1" ?"Display:none;": Eval("Actions").ToString() =="0" && Eval("ShowReject").ToString() =="0"?"Display:none;":""%>'><b>Reject previous to select this</b></span>
                                                                                        <asp:Button CssClass="btn btn-xs btn-default btnViewQuotationCl" runat="server"
                                                                                            Text="View Quotation" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button>
                                                                                   --%>

                                                                                 </ItemTemplate>
                                                                                </asp:TemplateField>

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
                                                                                <asp:TemplateField HeaderText="VAT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
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
                                                                              

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                           <%-- <asp:Button CssClass="btn btn-sm btn-success btnPrintCl" runat="server" Text="Print"
                                                Style="margin-top: 3px; width: 100px;"></asp:Button>
                                       --%> 

                                        </div>
                                        <!-- End : Quotation Table -->
                                    </div>
                                </div>

                                 
                              </div>
                              <div class="box-footer">
                                   
                                  <asp:Button CssClass="btn btn-primary pull-right" runat="server" Text="Back"
                                                Style="margin-top: 3px; width: 100px; margin-left: 5px;"  OnClick="Back_Click" ></asp:Button>
                                  <asp:Button CssClass="btn btn-success pull-right" runat="server"  OnClientClick ="FinalizeTabulation();" Text="Finalize Tabulation" 
                                                Style="margin-top: 3px; width: 140px;"></asp:Button>
                                  <asp:Button CssClass="btn btn-info pull-right" runat="server" 
                                    ID="btnPrintNew" Text="Print" OnClick="btnPrint1_Click"
                                    Style="margin-top: 3px; width: 100px; margin-right: 5px;"></asp:Button>


                                </div>
                         </div>
                            
                    </asp:Panel>


                </section>
                <!-- End : Section -->

                <!-- Start : Hidden Fields -->
                <asp:HiddenField ID="hdnSubTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNbtTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnVatTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNetTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnSelectRemarks" runat="server" />
                <asp:HiddenField ID="hdnRejectRemarks" runat="server" />
                <asp:HiddenField ID="hdnQuotationId" runat="server" />
                <asp:HiddenField ID="hdnTabulationId" runat="server" />
                <asp:HiddenField ID="hdnSlectedQutations" runat="server" />
                <asp:HiddenField ID="PurchaseType" runat="server" />
                <asp:HiddenField ID="hdnTotalAmountCalcu" runat="server" />
                <asp:Button ID="btnReopenBid" runat="server" OnClick="btnReopenBid_Click" CssClass="hidden" />
                <asp:HiddenField ID="hdnReOpenDays" runat="server" />
                <asp:HiddenField ID="hdnReopenRemarks" runat="server" />
                <asp:HiddenField ID="hdnBidId" runat="server" />
                <%--  <asp:Button ID="btnSelect" runat="server" OnClick="btnSelect_Click" CssClass="hidden" />
                <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" CssClass="hidden" />--%>
                <asp:Button ID="btnViewAttachments" runat="server" OnClick="btnViewAttachments_Click" CssClass="hidden" />

                <asp:HiddenField ID="hdnQuotationRemark" runat="server" />
                <asp:Button ID="BtnItemApprove" runat="server" CssClass="hidden" OnClick="BtnItemApprove_Click"/>
                <asp:Button ID="BtnFinalizeTabulation" runat="server" OnClick="SelectQuotation_Click" CssClass="hidden" />
                <asp:HiddenField ID="hdnSubTot" runat="server" />
                <asp:HiddenField ID="hdnVatVal" runat="server" />
                <asp:HiddenField ID="hdnNbtVal" runat="server" />
                <asp:HiddenField ID="hdnNetTot" runat="server" />
                <asp:HiddenField ID="hdnQty" runat="server" />
                <asp:HiddenField ID="hdnFinalizeRemark" runat="server" />
                <asp:HiddenField ID="hdnItemId" runat="server" />
                <asp:HiddenField ID="hdnSupplierId" runat="server" />
                <asp:HiddenField ID="hdnSelected" runat="server" Value="0" />
                 <asp:HiddenField ID="hdnMeasurementId" runat="server" Value="0" />
                <!-- End : Hidden Fields -->

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnPrint" />
                <asp:PostBackTrigger ControlID="btnreprint" />
                <asp:PostBackTrigger ControlID="btnPrintNew" />
                 <asp:PostBackTrigger ControlID="BtnItemApprove" />
                  
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

                        if ($('#ContentSection_PurchaseType').val() == "1") {
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

                $('.btnViewAttachmentsCl2').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlQuotations').modal('hide');

                        var tableRow = $('#ContentSection_gvrjectedTabulationsheet').find('> tbody > tr > td:not(table)');
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(2).text());

                        $('#ContentSection_btnViewAttachments').click();
                    }
                });

                $('#mdlAttachments').on('hide.bs.modal', function () {
                    $('.modal-backdrop').remove();
                    $('#mdlQuotations').modal('show');
                });


                $('#mdlQuotations').on('shown.bs.modal', function () {
                    $('#mdlQuotations').css('overflow', 'auto');
                    $('body').css("overflow", "hidden");
                    $('body').css("padding-right", "0");
                })

                $('#mdlAttachments').on('hide.bs.modal', function () {
                    $('.modal-backdrop').remove();
                    $('#mdlQuotationsNew').modal('show');
                });

                $('#mdlItems').on('hide.bs.modal', function () {
                    $('.modal-backdrop').remove();
                    $('#mdlQuotationsNew').modal('show');
                });




                $('#mdlQuotationsNew').on('shown.bs.modal', function () {
                    $('#mdlQuotationsNew').css('overflow', 'auto');
                    $('body').css("overflow", "hidden");
                    $('body').css("padding-right", "0");
                })

                $('#mdlQuotationsNew').on('hidden.bs.modal', function () {
                    $('body').css("overflow", "auto");
                    $('body').css("padding-right", "0");
                })

                $('#mdlQuotations').on('hidden.bs.modal', function () {
                    $('body').css("overflow", "auto");
                    $('body').css("padding-right", "0");
                })

                $('.btnSelectCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#isSelect').val('1');
                        $('#mdlQuotations').modal('hide');

                        var tableRow = $('#ContentSection_gvQuotations').find('> tbody > tr > td:not(table)');
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());
                        var supplierName = $(tableRow).eq(4).text();

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>select</strong> the quotation from <strong>" + supplierName + "</strong>?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Select It!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnSelectRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $(document).find('.txtSubTotalCl').removeAttr('disabled');
                                $(document).find('.txtNbtCl').removeAttr('disabled');
                                $(document).find('.txtVatCl').removeAttr('disabled');
                                $(document).find('.txtNetTotalCl').removeAttr('disabled');
                                $('#ContentSection_btnSelect').click();
                            } else if (
                                result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlQuotations').modal('show');

                            }
                        });


                    }
                });

                $('.btnRejectCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#isSelect').val('1');
                        $('#mdlQuotations').modal('hide');

                        var tableRow = $('#ContentSection_gvQuotations').find('> tbody > tr > td:not(table)');
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());
                        var supplierName = $(tableRow).eq(4).text();

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>reject</strong> the quotation from <strong>" + supplierName + "</strong>?</br></br>"
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

                                $('#ContentSection_btnReject').click();

                            } else if (
                                result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlQuotations').modal('show');

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

                    var tableRow = $('#ContentSection_gvQuotations').find('> tbody > tr > td:not(table)');

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
                    //debugger;
                    if ($('#ContentSection_PurchaseType').val() == "1") {
                        // debugger;
                        var lable = $('#ContentSection_gvQuotations tr').eq(0).find('th').eq($(this).index() - 1).text();
                        $('#itemname').text("Item Name : " + lable.split('-')[1]);
                        var selectqty = $('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index()).text();
                        selectqty = selectqty.split('*')[1];

                        if (selectqty != "" && selectqty != undefined) {
                            noselct = parseInt(selectqty);

                            var fullqty = $('#ContentSection_gvQuotations tr').eq(0).find('th').eq($(this).index()).text();
                            console.log(fullqty);
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
                            $("#BidId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(4).tt());
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
                            console.log(fullqty);
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

                            var fullqty = $('#ContentSection_gvImpotsQuotations tr').eq(0).find('th').eq($(this).index()).text();
                            console.log(fullqty);
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
                            console.log(fullqty);
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
                    $('#mdlQuotations').modal('show');
                    $('#mdlRequiredQty').modal('hide');
                    $("#mainQuantity").text("");
                    $("#txtamount").val("");
                    ev.preventDefault();

                });

                $("#btnadd").click(function (ev) {

                    if ($('#ContentSection_PurchaseType').val() == "1") {
                        //   debugger;
                        var isEdit = $("#ISEditedAgian").val();
                        if (isEdit != "1") {
                            var requiredqty = parseInt($("#mainQuantity").text().split(':')[1]);

                            if ($("#txtamount").val() != "") {
                                var qty = parseInt($("#txtamount").val());
                                //if (qty <= requiredqty && qty!=0) {
                                // debugger;

                                var hdnval = $("#ContentSection_hdnSlectedQutations").val();
                                if (hdnval != null && hdnval != "") {
                                    var b = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    var arr = hdnval.split(",");
                                    var conctarr = $.merge($.merge([], arr), b);
                                    $("#ContentSection_hdnSlectedQutations").val(conctarr);
                                    console.log(conctarr);

                                    //
                                    //var tta = $("#ContentSection_hdnTotalAmountCalcu").val().split(",");
                                    //var unp = $('#ContentSection_gvQuotations tr').eq(parseInt($("#Rowno").val())).find('td').eq(cell).text()
                                    //var tt = $.merge($.merge([], tta), [$("#txtamount").val(), unp])

                                    //
                                }
                                else {
                                    var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    $("#ContentSection_hdnSlectedQutations").val(a);
                                    //
                                    //var unp = $('#ContentSection_gvQuotations tr').eq(parseInt($("#Rowno").val())).find('td').eq(cell).text()
                                    //var gg = [$("#txtamount").val(), unp];
                                    //$("#ContentSection_hdnTotalAmountCalcu").val(gg);
                                    //
                                }

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).removeClass('CellClick');
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).addClass('greenBg');

                                $('#mdlQuotations').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).html(val + "*" + qty);
                                var totalAmountDisplay = parseFloat(val) * parseFloat(qty);
                                // $("#lblTotalAmountDisplay").text(parseFloat(totalAmountDisplay).toFixed(2));
                                //}
                                //else {
                                //    $('#mdlRequiredQty').modal('hide');
                                //    swal({ type: 'error', title: 'ERROR', text: 'Insert value is more than required', showConfirmButton: true })
                                //    .then(function (isConfirm) {
                                //        if (isConfirm) {
                                //            $('#mdlRequiredQty').modal('show');
                                //            $('div').removeClass('modal-backdrop');
                                //        }
                                //    });

                                //}
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

                        else {
                            console.log($("#previousqty").val());
                            var requiredqty = parseInt($("#mainQuantity").text().split(':')[1]);
                            var prqty = parseInt($("#previousqty").val());
                            if ($("#txtamount").val() != "") {
                                var qty = parseInt($("#txtamount").val());
                                //if (qty <= (requiredqty+prqty) && qty!=0) {


                                var hdnval = $("#ContentSection_hdnSlectedQutations").val();
                                if (hdnval != null && hdnval != "") {

                                    var arr = hdnval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr[i + 5] = qty;
                                        }
                                    }
                                    $("#ContentSection_hdnSlectedQutations").val(arr);

                                    console.log(arr);
                                }
                                else {
                                    var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    $("#ContentSection_hdnSlectedQutations").val(a);

                                }

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).removeClass('CellClick');
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).addClass('greenBg');

                                $('#mdlQuotations').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).html(val.split('*')[0] + "*" + qty);
                                //}
                                //else {
                                //    $('#mdlRequiredQty').modal('hide');
                                //    swal({ type: 'error', title: 'ERROR', text: 'Insert value is more than required', showConfirmButton: true })
                                //    .then(function (isConfirm) {
                                //        if (isConfirm) {
                                //            $('#mdlRequiredQty').modal('show');
                                //            $('div').removeClass('modal-backdrop');
                                //        }
                                //    });

                                //}
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
                                //if (qty <= requiredqty && qty!=0) {


                                var hdnval = $("#ContentSection_hdnSlectedQutations").val();
                                if (hdnval != null && hdnval != "") {
                                    var b = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    var arr = hdnval.split(",");
                                    var conctarr = $.merge($.merge([], arr), b);
                                    $("#ContentSection_hdnSlectedQutations").val(conctarr);
                                    console.log(conctarr);
                                }
                                else {
                                    var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    $("#ContentSection_hdnSlectedQutations").val(a);

                                }

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).removeClass('CellClick');
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).addClass('greenBg');

                                $('#mdlQuotations').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).html(val + "*" + qty);
                                //}
                                //else {
                                //    $('#mdlRequiredQty').modal('hide');
                                //    swal({ type: 'error', title: 'ERROR', text: 'Insert value is more than required', showConfirmButton: true })
                                //    .then(function (isConfirm) {
                                //        if (isConfirm) {
                                //            $('#mdlRequiredQty').modal('show');
                                //            $('div').removeClass('modal-backdrop');
                                //        }
                                //    });

                                //}
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

                        else {
                            console.log($("#previousqty").val());
                            var requiredqty = parseInt($("#mainQuantity").text().split(':')[1]);
                            var prqty = parseInt($("#previousqty").val());
                            if ($("#txtamount").val() != "") {
                                var qty = parseInt($("#txtamount").val());
                                //if (qty <= (requiredqty+prqty) && qty!=0) {


                                var hdnval = $("#ContentSection_hdnSlectedQutations").val();
                                if (hdnval != null && hdnval != "") {

                                    var arr = hdnval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr[i + 5] = qty;
                                        }
                                    }
                                    $("#ContentSection_hdnSlectedQutations").val(arr);

                                    console.log(arr);
                                }
                                else {
                                    var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    $("#ContentSection_hdnSlectedQutations").val(a);

                                }

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).removeClass('CellClick');
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).addClass('greenBg');

                                $('#mdlQuotations').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).html(val.split('*')[0] + "*" + qty);
                                //}
                                //else {
                                //    $('#mdlRequiredQty').modal('hide');
                                //    swal({ type: 'error', title: 'ERROR', text: 'Insert value is more than required', showConfirmButton: true })
                                //    .then(function (isConfirm) {
                                //        if (isConfirm) {
                                //            $('#mdlRequiredQty').modal('show');
                                //            $('div').removeClass('modal-backdrop');
                                //        }
                                //    });

                                //}
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

                $('.Cancelselct').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlRequiredQty').modal('hide');
                        $('#mdlQuotations').modal('show');
                        $('div').removeClass('modal-backdrop');
                    }

                });

                $('.CanceldocUpload').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlviewdocsuplod').modal('hide');
                        $('#mdlQuotations').modal('show');
                        $('div').removeClass('modal-backdrop');
                    }

                });


                $('.btnReopenCl').on({
                    click: function () {
                        //  debugger;
                        event.preventDefault();
                        var tableRow = $(this).closest('tr:not(:has(>td>table))')[0].cells;
                        $('#ContentSection_hdnBidId').val($(tableRow).eq(1).text());

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

                                $('#ContentSection_btnReopenBid').click();

                            }
                        });

                    }
                });

            });

            var itsPostBack = <%= Page.IsPostBack ? "true" : "false" %>;
           
            if (itsPostBack == true) {

                var childGridViews = $(document).find('.childGridViewCl');
                debugger;
                for (var i = 0; i < childGridViews.length; i++) {
                    var gridview = $(childGridViews).eq(i);

                    var rows = $(gridview).find('tr');

                    for (var j = 1; j < rows.length; j++) {

                        var row = $(rows).eq(j);

                        var columns = $(row).find('td');

                        var SubTot = parseFloat($(columns).find('.txtSubTotal').val()) || 0.00;
                        $(columns).find('.lblSubTotal').text(SubTot.toFixed(2));

                        var Nbt = parseFloat($(columns).find('.txtNbt').val()) || 0.00;
                        $(columns).find('.lblNbt').text(Nbt.toFixed(2));

                        var Vat = parseFloat($(columns).find('.txtVat').val()) || 0.00;
                        $(columns).find('.lblVat').text(Vat.toFixed(2));

                        var NetTotal = parseFloat($(columns).find('.txtNetTotal').val()) || 0.00;
                        $(columns).find('.lblNetTotal').text(NetTotal.toFixed(2));

                        
                        var SelectedText = parseFloat($(columns).find('.txtSelectedQ').val());
                        if (SelectedText == 1) {
                            $(columns).find('.lblSelected').text("Selected");
                            
                        }


                    }

                }
            }

        });

        //function btnViewAttachmentsClassA() {

        //            $('#mdlQuotationsNew').modal('hide');

        //            if ($('#ContentSection_PurchaseType').val() == "1") {
        //                var tableRow = $('#ContentSection_gvQuotationItems').find('> tbody > tr > td:not(table)');
        //                $('#ContentSection_hdnQuotationId').val($(tableRow).eq(2).text());
        //            }
        //            else {
        //                var tableRow = $('#ContentSection_gvQuotationItems').find('> tbody > tr > td:not(table)');
        //                $('#ContentSection_hdnQuotationId').val($(tableRow).eq(2).text());
        //            }
        //            $('#ContentSection_btnViewAttachments').click();
        //     }

        function Calculation(elmt) {
            var UnitPrice = $(elmt).closest('tr').find('.lblUnitPrice').text();
            var RequestingQty = $(elmt).closest('tr').find('.txtRequestingQty').val();
            var HasVat = $(elmt).closest('tr').find('.lblHasVat').text();
            var HasNbt = $(elmt).closest('tr').find('.lblHasNbt').text();
            var NbtType = $(elmt).closest('tr').find('.lblNBTType').text();
            var RequestedTotQty = $(elmt).closest('tr').find('.txtReqTotQty').val();
            var TotNBT = $(elmt).closest('tr').find('.txtTotNBT').val();
            var TotVAT = $(elmt).closest('tr').find('.txtTotVAT').val();


            var Subtotal = UnitPrice * RequestingQty;
            var Vat = 0.00;
            if (HasVat == 1) {
                // Vat = Subtotal * 0.08;
                Vat = (TotVAT / RequestedTotQty) * RequestingQty;
            }

            var Nbt = 0.00;
            if (HasNbt == 1) {
                //if (NbtType == 1) {
                //    Nbt = Subtotal * 0.0204
                //}
                //else if (NbtType == 2) {
                //    Nbt = Subtotal * 0.0200
                //}
                Nbt = (TotNBT / RequestedTotQty) * RequestingQty;


            }
            var NetTotal = Subtotal + Nbt + Vat;


            $(elmt).closest('tr').find('.lblSubTotal').text(Subtotal.toFixed(2));
            $(elmt).closest('tr').find('.lblNbt').text(Nbt.toFixed(2));
            $(elmt).closest('tr').find('.lblVat').text(Vat.toFixed(2));
            $(elmt).closest('tr').find('.lblNetTotal').text(NetTotal.toFixed(2));

            $(elmt).closest('tr').find('.txtSubTotal').val(Subtotal.toFixed(2));
            $(elmt).closest('tr').find('.txtNbt').val(Nbt.toFixed(2));
            $(elmt).closest('tr').find('.txtVat').val(Vat.toFixed(2));
            $(elmt).closest('tr').find('.txtNetTotal').val(NetTotal.toFixed(2));
            $(elmt).closest('tr').find('.txtSelectedQ').val("1")

        }

        var selectElm;
        
        function SelectQuotation(elmt, itemId, tabulationId, QuotationId, SupplierId) {

            selectElm = $(elmt);

            var SubTot = $(elmt).closest('tr').find('.lblSubTotal').text();
            var VatVal = $(elmt).closest('tr').find('.lblVat').text();
            var NbtVal = $(elmt).closest('tr').find('.lblNbt').text();
            var NetTot = $(elmt).closest('tr').find('.lblNetTotal').text();
            var Qty = $(elmt).closest('tr').find('.txtRequestingQty').val();

            if (Qty > 0) {
                $(elmt).closest('tr').find('.lblSelected').text("Selected");
                

                $('#ContentSection_hdnSubTot').val(SubTot);
                $('#ContentSection_hdnVatVal').val(VatVal);
                $('#ContentSection_hdnNbtVal').val(NbtVal);
                $('#ContentSection_hdnNetTot').val(NetTot);
                $('#ContentSection_hdnQty').val(Qty);



                $('#ContentSection_hdnItemId').val(itemId);
                $('#ContentSection_hdnQuotationId').val(QuotationId);
                $('#ContentSection_hdnSupplierId').val(SupplierId);
                $('#ContentSection_hdnTabulationId').val(tabulationId);


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

                            $('#ContentSection_hdnQuotationRemark').val($('#ss').val());
                        }

                    }
                }
                ).then((result) => {
                    if (result.value) {
                        $(elmt).closest('tr').find('.txtSelectedQ').val("1");
                        $('#ContentSection_BtnItemApprove').click();
                         
                        
                        //$('.textboxesCl').removeProp('disabled');

                    } else if (result.dismiss === Swal.DismissReason.cancel) {
                        $(elmt).closest('tr').find('.lblSelected').text("Not Selected");
                        // $(elmt).closest('tr').find('.txtSelectedQ').val("0");
                        
                        $(elmt).closest('tr').find('.lblSubTotal').text("0.00");
                        $(elmt).closest('tr').find('.lblVat').text("0.00");
                        $(elmt).closest('tr').find('.lblNbt').text("0.00");
                        $(elmt).closest('tr').find('.lblNetTotal').text("0.00");
                        $(elmt).closest('tr').find('.txtRequestingQty').val("0.00");

                    }
                });

            }

            else {
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

        function FinalizeTabulation() {
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
                        $('#ContentSection_hdnFinalizeRemark').val($('#ss').val());
                    }

                }
            }
            ).then((result) => {
                if (result.value) {


                    $('#ContentSection_BtnFinalizeTabulation').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {

                }
            });


        }

        function abc() {
            alert();
        }

    </script>


</asp:Content>
