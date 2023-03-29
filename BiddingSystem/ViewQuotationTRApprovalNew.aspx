<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewQuotationTRApprovalNew.aspx.cs" Inherits="BiddingSystem.ViewQuotationTRApprovalNew" %>

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
         .margin {
         margin-bottom: 5px;
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
            Tabulation Review / Approve
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li class="active"> Tabulation Review / Approve </li>
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
                    <div class="modal-dialog" style="width: 85%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">

                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;" onclick="RemoveBackdrop();">
                                    <span aria-hidden="true" style="opacity: 1;" >×</span></button>
                                <h4 class="modal-title">Submitted Quotations</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <div class="col-xs-12">
                                <div class="process">
                                    <div class="process-row nav nav-tabs">
                                        <div class="process-step">
                                            <button type="button" id="btnBasic" class="btn btn-info btn-circle" data-toggle="tab"
                                                href="#ItemWise" onclick="showItemWise();">
                                                <i class="fa fa-info fa-3x"></i>
                                            </button>
                                            <p><b>ITEM WISE</b></p>
                                        </div>
                                        <div class="process-step">
                                            <button type="button" id="btnItem" class="btn btn-default btn-circle " data-toggle="tab"
                                                href="#SupplierWise" onclick="showQuotWise();">
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
                                                    <div style="color: black; overflow-x: scroll;">

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
                                                                            <td class="hidden" scope="col">Supplier Mentioned Name </td>
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
                                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="BidId"
                                                                    HeaderText="BidItemId"
                                                                    HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="PrdId"
                                                                    HeaderText="PRDId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                                                                    HeaderText="Estimated Price" DataFormatString="{0:N2}"/>
                                                                <asp:BoundField DataField="QuotationCount"
                                                                    HeaderText="Quotations Count" />
                                                                <asp:BoundField DataField="LastSupplierId"
                                                                    HeaderText="LastSupplierId" HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="LastSupplierName" NullDisplayText="Not Found"
                                                                    HeaderText="Last Purchased Supplier" />
                                                                <asp:BoundField DataField="LPurchasedPrice" NullDisplayText="Not Found"
                                                                    HeaderText="Last Purchased Price" />
                                                                 <asp:BoundField DataField="SupplierMentionedItemName" NullDisplayText="Not Found"
                                                                    HeaderText="Supplier Mentioned Name" />
                                                               
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                       
                                                                        <asp:Button CssClass="btn-xs btn-info btn-styled" runat="server"
                                                                            ID="btnPurchased" Text="History"
                                                                            Style="margin-top: 3px; width: 100px;" OnClick="btnPurchasedItem_Click"></asp:Button>
                                                                      
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="QuotationItemId" HeaderText="QuotaionItemId"
                                                                                     HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td colspan="100%">
                                                                                <asp:Panel ID="pnlQuotationItems" runat="server" Style="margin-left: 40px; overflow-x:scroll; width: 90% ">
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
                                                                                            <asp:BoundField DataField="Ratings" HeaderText="Ratings" NullDisplayText="Unavailable" ItemStyle-ForeColor="Orange" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                            <asp:BoundField DataField="Description" HeaderText="Description" NullDisplayText="-" />
                                                                                            <asp:TemplateField HeaderText="Actual Price" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label runat="server" Text='<%# Eval("ActualPrice","{0:N}").ToString() != "0" ? Eval("ActualPrice","{0:N}").ToString() : Eval("UnitPrice","{0:N}").ToString() %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="UnitPrice" HeaderText="Quoted Price" DataFormatString="{0:N2}"/>
                                                                                            <asp:BoundField DataField="SubTotal" HeaderText="Sub-Total" DataFormatString="{0:N2}"/>
                                                                                            <asp:BoundField DataField="NbtAmount" HeaderText="NBT" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                            <asp:BoundField DataField="VatAmount" HeaderText="VAT" DataFormatString="{0:N2}"/>
                                                                                            <asp:BoundField DataField="NetTotal" HeaderText="Net-Total" DataFormatString="{0:N2}"/>
                                                                                            <asp:BoundField DataField="SpecComply" HeaderText="Complies Specs" />
                                                                                             <asp:BoundField DataField="SupplierMentionedItemName" NullDisplayText="Not Found"
                                                                                                HeaderText="Supplier Mentioned Name"    />
                                                                                            <asp:TemplateField HeaderText="Attachments" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button CssClass="btn btn-info btn-styled btnViewAttachmentsClassA btn-xs" runat="server" ID="btnAttachMents"
                                                                                                        Text="View" OnClick="btnAttachMentsItem_Click"></asp:Button>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Supplier Details" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Button CssClass="btn btn-info btn-styled btn-xs " ID="btnsupplerview"  runat="server"
                                                                                                        Text="view" Style="margin-right: 4px;" OnClick="btnsupplerview_Click"></asp:Button>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                          <%--  <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                                <ItemTemplate>--%>
                                                                                                  
                                                                                                   <%-- <asp:Button CssClass="btn btn-xs btn-danger btnRejectCl" runat="server" 
                                                                                                        Text="Reject" Style="margin-right: 4px; margin-bottom: 4px;" OnClientClick="TabulationRejectSupplier('<%# Eval("QuotationItemId").ToString() %>');"></asp:Button>
                                                                                                    <span style='color: red; <%#Eval("Actions").ToString() =="1" || Eval("IsBidItemSelected").ToString() =="1" ?"Display:none;": Eval("Actions").ToString() =="0" && Eval("ShowReject").ToString() =="0"?"Display:none;":""%>'><b>Reject previous to select this</b></span>--%>
                                                                                                    <%--<button type="button" class="btn btn-info btn-styled btn-danger btn-xs" onclick="TabulationRejectSupplier(<%# Eval("QuotationItemId").ToString() %>,'Item');" >Reject</button>--%>
                                                                                               <%-- </ItemTemplate>
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

                                                        
                                                    </div>
                                                    <!-- End : Quotation Table -->
                                                </div>
                                            </div>
                                        </div>
                                        <!-- End : Modal Body -->

                                        <div class="box-footer">

                                            <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="pull-right hidden" style="margin-right: 10px; max-height: 40px;" />

                                            <button type="button" class="btn btn-info btn-styled   pull-right" onclick="CloseModelQuotations();">Back</button>
                                        </div>
                                    </div>
                                </div>
                                <div id="SupplierWise" class="tab-pane fade">
                                    <!-- Start : Modal Body -->
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <!-- Start : Quotation Table -->
                                                <div style="color: black; overflow-x: scroll;">

                                                    <asp:GridView ID="gvQuotationItemsSup" runat="server"
                                                        CssClass="table table-responsive"
                                                        GridLines="None" AutoGenerateColumns="false" ShowHeader="false"
                                                        Caption="Supplier" EmptyDataText="No Quotations Found" RowStyle-BackColor="#f5f2f2" HeaderStyle-BackColor="#525252" HeaderStyle-ForeColor="White" OnRowDataBound="gvQuotationItemsSup_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                                        <ItemTemplate>

                                                                                            <tr style="color: White; background-color: #525252"">
                                                                                                <td scope="col">#</td>
                                                                                                <td class="hidden" scope="col">QuotationItemId</td>
                                                                                                <td class="hidden" scope="col">QuotationId</td>
                                                                                                <td class="hidden" scope="col">BiddingItemId</td>
                                                                                                <td class="hidden" scope="col">SupplierId</td>
                                                                                                <td scope="col">Supplier</td>
                                                                                                <td class="hidden" scope="col">Quotations Count</td>
                                                                                                <td class="hidden" scope="col">Complies Specs </td>
                                                                                                <td scope="col">Attachments</td>
                                                                                                <td scope="col">Supplier Details</td>
                                                                                                <td class="hidden" scope="col">Requesting qty</td>
                                                                                                <td class="hidden" scope="col">Requesting Unit</td>
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
                                                            <%--<asp:BoundField DataField="QuotationItemId" HeaderText="QuotaionItemId"
                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                                            <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="QuotationId" HeaderText="QuotationId"
                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                            <%--<asp:BoundField DataField="BiddingItemId" HeaderText="BidItemId"
                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                                            <asp:BoundField DataField="SupplierId" HeaderText="Supplier"
                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="SupplierName" HeaderText="Supplier" NullDisplayText="Unavailable" />
                                                           <%-- <asp:BoundField DataField="Ratings" HeaderText="Ratings" NullDisplayText="Unavailable" ItemStyle-ForeColor="Orange" />--%>
                                                            <%--<asp:BoundField DataField="Description" HeaderText="Description" NullDisplayText="-" />--%>
                                                            <%--<asp:TemplateField HeaderText="Actual Price" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("ActualPrice").ToString() != "0" ? Eval("ActualPrice").ToString() : Eval("UnitPrice").ToString() %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <%--<asp:BoundField DataField="UnitPrice" HeaderText="Quoted Price" DataFormatString="{0:N2}"/>--%>
                                                            <%--<asp:BoundField DataField="SubTotal" HeaderText="Sub-Total" />
                                                            <asp:BoundField DataField="NbtAmount" HeaderText="NBT" />
                                                            <asp:BoundField DataField="VatAmount" HeaderText="VAT" />
                                                            <asp:BoundField DataField="NetTotal" HeaderText="Net-Total" />--%>
                                                            <%--<asp:BoundField DataField="SubTotal_Sup" HeaderText="Sub-Total" DataFormatString="{0:N2}"/>
                                                            <asp:BoundField DataField="Nbt_sup" HeaderText="NBT" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                            <asp:BoundField DataField="vat_sup" HeaderText="VAT" DataFormatString="{0:N2}"/>
                                                            <asp:BoundField DataField="NetTotal_sup" HeaderText="Net-Total" DataFormatString="{0:N2}"/>
                                                            <asp:BoundField DataField="SpecComply" HeaderText="Complies Specs" />--%>

                                                            <asp:TemplateField HeaderText="Attachments" >
                                                                <ItemTemplate>
                                                                    <asp:Button CssClass="btn btn-info btn-styled btnViewAttachmentsClassA btn-xs" runat="server" ID="btnAttachMentsSu"
                                                                        Text="View" OnClick="btnAttachMents_Click"></asp:Button>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Supplier Details" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                <ItemTemplate>
                                                                    <asp:Button CssClass="btn btn-info btn-styled btn-xs " ID="btnsupplerviewSu" runat="server"
                                                                        Text="view" Style="margin-right: 4px;" OnClick="btnsupplerview_Click"></asp:Button>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                 <tr>
                                                                    <td colspan="100%">
                                                                 <asp:Panel ID="pnlSupplier" runat="server"  Style="margin-left: 40px; overflow-x:scroll; width: 90% ">
                                                                      <asp:GridView ID="gvItemSupllier" runat="server"
                                                                                CssClass="table gvItems" Caption="Items"
                                                                                GridLines="None" AutoGenerateColumns="false"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" >
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
                                                                                        HeaderText="PRDId" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="ItemId"
                                                                                        HeaderText="Item Id"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="ItemName"
                                                                                        HeaderText="Item Name" />
                                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label runat="server" Text='<%# decimal.Parse((Eval("Qty").ToString())).ToString() + " " + (Eval("UnitShortName").ToString()) %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="EstimatedPrice"
                                                                                        HeaderText="Estimated Price" DataFormatString="{0:N2}" />
                                                                                    <asp:BoundField DataField="QuotationCount"
                                                                                        HeaderText="Quotations Count"  HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden"/>
                                                                                    <asp:BoundField DataField="LastSupplierId"
                                                                                        HeaderText="LastSupplierId" HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="LastSupplierName" NullDisplayText="Not Found"
                                                                                        HeaderText="Last Purchased Supplier" />
                                                                                    <asp:BoundField DataField="LPurchasedPrice" NullDisplayText="Not Found"
                                                                                        HeaderText="Last Purchased Price" DataFormatString="{0:N2}"/>

                                                                                     <asp:BoundField DataField="SupplierMentionedItemName" NullDisplayText="Not Found"
                                                                                       HeaderText="Supplier Mentioned Name" />
                                                                                    <asp:BoundField DataField="Description" HeaderText="Description" NullDisplayText="-" />
                                                                                    <asp:BoundField DataField="UnitPrice" HeaderText="Quoted Price" DataFormatString="{0:N2}"/>
                                                                                    <asp:BoundField DataField="SubTotal_Sup" HeaderText="Sub-Total" DataFormatString="{0:N2}"/>
                                                                                    <asp:BoundField DataField="Nbt_sup" HeaderText="NBT" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                    <asp:BoundField DataField="vat_sup" HeaderText="VAT" DataFormatString="{0:N2}"/>
                                                                                    <asp:BoundField DataField="NetTotal_sup" HeaderText="Net-Total" DataFormatString="{0:N2}"/>
                                                                                    <asp:BoundField DataField="SpecComply" HeaderText="Complies Specs" />
                                                                                    <asp:TemplateField HeaderText="Purchase History">
                                                                                        <ItemTemplate>
                                                                                           
                                                                                            <asp:Button CssClass="btn-xs btn-info btn-styled" runat="server"
                                                                                                ID="btnPurchasedSu" Text="History"
                                                                                                Style="margin-top: 3px; width: 100px;" OnClick="btnPurchased_Click"></asp:Button>
                                                                                           
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                     <asp:BoundField DataField="QuotationItemId" HeaderText="QuotaionItemId"
                                                                                     HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                    <%--<asp:TemplateField HeaderText="Actions" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                        <ItemTemplate>
                                                                                            <button type="button" class="btn btn-info btn-styled btn-danger btn-xs" onclick="TabulationRejectSupplier(<%# Eval("QuotationItemId").ToString() %>,'Supplier');">Reject</button>

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

                                                  
                                                </div>
                                                <!-- End : Quotation Table -->
                                            </div>
                                        </div>
                                    </div>
                                    <!-- End : Modal Body -->
                                    
                                        <div class="box-footer">

                                            <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="pull-right hidden" style="margin-right: 10px; max-height: 40px;" />

                                            <button type="button" class="btn btn-info btn-styled   pull-right" onclick="CloseModelQuotations();">Back</button>
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
                                <button type="button" class="close btnCloseLabelItem" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;" onclick="RemoveBackdrop();">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
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
                                  
                                    <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="pull-right hidden" style="margin-right:10px; max-height:40px;" />
                                      <button type="button" class="btn btn-info btn-styled   pull-right" onclick="PopupGvItemsModelHis();" >Back</button>
                                
                                </div>
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Purchased Items Modal -->


               <!-- Start : Attachment Modal -->
                <div id="mdlAttachments" class="modal fade" role="dialog" >
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
                                                <button type="button" class="close" data-dismiss="modal" onclick="RemoveBackdrop();">&times;</button>
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
                                 
                                    <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="pull-right hidden" style="margin-right:10px; max-height:40px;" />
                                      <button type="button" class="btn btn-info btn-styled   pull-right" onclick="PopupGvItemsModelDoc();" >Back</button>
                                 
                                </div>
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>


                <!-- End : Section -->

                <!-- Start : Section -->
                <section class="content">
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
                                        <div class="col-md-3 col-sm-3 col-xs-3">
                                            <address>
                                                <strong>PR Code : </strong>
                                                <asp:Label ID="lblPRNo" runat="server" Text=""></asp:Label><br />
                                                <strong>Created On : </strong>
                                                <asp:Label ID="lblCreatedOn" runat="server" Text=""></asp:Label><br />
                                                <strong>Created By : </strong>
                                                <asp:Label ID="lblCreatedBy" runat="server" Text=""></asp:Label><br />
                                                 
                                            </address>
                                        </div>
                                         <div class="col-md-3 col-sm-3 col-xs-3">
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
                                         <div class="col-md-3 col-sm-3 col-xs-3">
                                             <strong>MRN Code : </strong>
                                        <asp:Label ID="lblMrnId" runat="server" Text=""></asp:Label><br />  
                                             <strong>MRN Created By : </strong>
                                                <asp:Label ID="lblMrnCreatedBy" runat="server" Text=""></asp:Label><br />
                                                <strong>MRN Approved By : </strong>
                                                <asp:Label ID="lblMRNApprovedBy" runat="server" Text=""></asp:Label><br />
                                             <asp:Panel ID="panelParentPr" runat="server" Visible ="false">
                                             <strong>Parent PR : </strong>
                                                <asp:Label ID="lblParentPr" runat="server" Text=""></asp:Label><br />
                                                  </asp:Panel>
                                                
                                              </div>
                                         <div class="col-md-3 col-sm-3 col-xs-3">
                                         
                                          <address>
                                            <strong>Warehouse</strong><br />
                                               <asp:Panel ID="pnlWarehouse" runat="server">
                                               Name: <asp:Label ID="lblWhName" runat="server" Text=""></asp:Label><br />
                                               Address: <asp:Label ID="lblWhAddress" runat="server" Text=""></asp:Label><br />
                                               Contact No: <asp:Label ID="lblWhContactNo" runat="server" Text=""></asp:Label>
                                            </asp:Panel>
                                                <asp:Panel ID="pnlNotFound" runat="server">
                                                  <asp:Label ID="lblwhNotFound" runat="server" Text="Not Found"></asp:Label><br />
                                                 </asp:Panel>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
                                              <address>
                                              </address>
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
                                                                                    <asp:Label runat="server" Text='<%# decimal.Parse(Eval("Qty").ToString()).ToString() + " " + (Eval("UnitShortName").ToString()) %>' />
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
                                                            DataFormatString="{0:dd-MMMM-yyyy}" />
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                            DataFormatString="{0:dd-MMMM-yyyy}" />
                                                        <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                            DataFormatString="{0:dd-MMMM-yyyy}" />
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
                                                        <%--<asp:TemplateField HeaderText="Is Tabulaation Selected">
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
                                                                <asp:Button CssClass="btn btn-info btn-xs btn-styled TabluationBtn" runat="server"
                                                                    ID="btnView" Text="View Quotations" 
                                                                    style="width:100px;" OnClick="btnView_Click"></asp:Button>

                                                                <asp:Button CssClass="btn btn-success btn-styled btn-xs margin" runat="server"
                                                                    ID="btnApprove" Text="Proceed Bid" Enabled='<%# Convert.ToInt32(Eval("Visibility")) == 1 ? true : false %>' Visible='<%# Convert.ToInt32(Eval("NoOfQuotations")) > 0 ? true : false %>'
                                                                    style="width:100px;" OnClientClick='<%#"TabulationApprove(event,"+Eval("BidId").ToString()+")" %>' ></asp:Button>

                                                                <asp:Button CssClass="btn btn-info btn-styled btn-xs btn-warning" runat="server"  Visible='<%# Convert.ToInt32(Eval("IsClnedPr")) == 1 ? false : true %>'
                                                                    ID="btnReject" Text="Reopen Bid" Enabled='<%# Convert.ToInt32(Eval("Visibility")) == 1 ? true : false %>'
                                                                    style="width:100px;" OnClientClick="ReOpenBidClick(this);"></asp:Button>


<%--                                                                    <button type="button" class="btn btn-success btn-styled btn-xs margin " onclick="TabulationApprove();" disabled >Approve</button>
                                                               <button type="button" class="btn btn-info btn-styled btn-xs btn-danger  " onclick="TabulationReject();" disabled >Reject</button>
                                                              --%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 col-sm-6 col-xs-6">
                                            <asp:Label runat="server" Text="Proceed Remark" /> <br>
                                            <asp:TextBox runat="server" TextMode="MultiLine" Rows="4" ID="txtRemark" CssClass="txtRemark" Width="800px" />
                                                      
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
                                <div class="box-footer">
                                    <%--<a class="btn btn-info pull-right" href="ViewPrForQuotationComparison.aspx"
                                        style="margin-right:10px">Done</a>--%>
                                    <%--<img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="pull-right hidden" style="margin-right:10px; max-height:40px;" />
                                     <button type="button" class="btn btn-info btn-styled btn-danger  pull-right" onclick="TabulationReject();" >Reject</button>
                                     <button type="button" class="btn btn-info btn-styled pull-right" onclick="TabulationApprove();" >Approve</button>--%>
<%--                                    <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-info btn-styled pull-right"  style="margin-right:10px" Text="Approve" OnClick="btnApprove_Click"/>--%>
        
                                    
                                </div>
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
                <asp:HiddenField ID="hdnReOpenDays" runat="server" />
                <asp:HiddenField ID="hdnItemId" runat="server" />
             
            </ContentTemplate>
            <Triggers>
                <%--.//<asp:PostBackTrigger ControlID="btnPrint" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </form>



    <script src="Scripts/LoginService.js?v=<%DateTime.Now.ToString();%>"></script>
    <script src="ViewModels/JS/PrViewModel.js?v=<%DateTime.Now.ToString();%>"></script>
      <script src="ViewModels/JS/TBViewModel.js?v=<%DateTime.Now.ToString();%>"></script>
    <script src="Scripts/Tabulation.js?v=<%DateTime.Now.ToString();%>"></script>

    <script type="text/javascript">
        function RemoveBackdrop() {
            $('.modal-backdrop').remove();
        }

        function showItemWise() {
            $('#btnBasic').removeClass('btn btn-default btn-circle');
            $('#btnBasic').addClass('btn btn-info btn-circle');

            $('#btnItem').removeClass('btn btn-info btn-circle');
            $('#btnItem').addClass('btn btn-default btn-circle');

            window.scrollTo({ top: 0, behavior: 'smooth' });
        }
        
        function showQuotWise() {
            $('#btnItem').removeClass('btn btn-default btn-circle');
            $('#btnItem').addClass('btn btn-info btn-circle');

            $('#btnBasic').removeClass('btn btn-info btn-circle');
            $('#btnBasic').addClass('btn btn-default btn-circle');

            window.scrollTo({ top: 0, behavior: 'smooth' });
        }
    </script>
</asp:Content>
