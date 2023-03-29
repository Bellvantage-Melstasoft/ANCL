﻿<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ManualBidSubmission.aspx.cs" Inherits="BiddingSystem.ManualBidSubmission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server" ViewStateMode="Enabled">

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

        table#ContentSection_gvBidItems,
        table#ContentSection_gvPreviousQuotations,
        table.gvQuotationItems {
            border: 1px solid #f8f8f8;
        }

        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        .summary-data {
            font-weight: 500;
            margin-right: 10px;
        }

        .summary-title {
            font-weight: 600;
        }

        .TestTable {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .TestTable td, #TestTable th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            .TestTable tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .TestTable tr:hover {
                background-color: #ddd;
            }

            .TestTable th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #4CAF50;
                color: white;
            }

        .modal-open {
            overflow: scroll !important;
        }

        .validity {
            position: relative;
            color: white;
        }

            .validity:before {
                position: absolute;
                content: attr(data-date);
                display: inline-block;
                color: black;
            }

            .validity::-webkit-datetime-edit, .validity::-webkit-inner-spin-button, .validity::-webkit-clear-button {
                display: none;
            }

            .validity::-webkit-calendar-picker-indicator {
                position: absolute;
                right: 5px;
                color: #555;
                opacity: 1;
                font-size: 9px;
            }

        table#ContentSection_gvBidItems tr td.ItemName {
            white-space: nowrap;
        }

        table#ContentSection_gvBidItems tr td.ItemSpecification {
            font-size: 11px;
        }

        table#ContentSection_gvPreviousQuotations tbody tr td.TermsAndCondition {
            white-space: pre-line;
        }

        table#ContentSection_gvRejectedQuotationAndItems tbody tr {
            background-color: white;
        }
    </style>

    <script src="AdminResources/js/jquery1.8.min.js" type="text/javascript"></script>
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js" type="text/javascript"> </script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <script src="AdminResources/js/moment.min.js" type="text/javascript"></script>



    <!-- Start : Form -->
    <form runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal fade" id="myModal" role="dialog" style="vertical-align: central">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Modal Header</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h4>Standard Images</h4>
                                        <div id="standardImgDiv">
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h4>Replacement Images</h4>
                                        <div id="ReplacementImgDiv">
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h4>Replacement Images</h4>
                                        <div id="SupportiveDocsDiv">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="mdlItemSpecs" class="modal modal-primary fade" tabindex="-1" role="dialog" style="z-index: 3001" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Item Specifications</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvBOMDate" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;" GridLines="None"
                                                    AutoGenerateColumns="false" EmptyDataText="No Specifications Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="PrId" HeaderText="PR Id"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="Meterial" HeaderText="Material" />
                                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div>
                                            <label id="Label1" style="margin: 3px; color: maroon; text-align: center;"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="mdlcaluculation" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close Cancelselct" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Calculations</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <input type="hidden" id="ItemValue" />
                                            <input type="hidden" id="Term" />
                                            <input type="hidden" id="Exchng" />
                                            <label for="exampleInputEmail1" id="itemvalue"></label>
                                        </div>
                                        <div class="form-group">
                                            <label id="totalcif" style="font-weight: bold;" for="exampleInputEmail1"></label>
                                        </div>
                                        <div class="form-group col-xs-3">
                                            <label for="exampleInputEmail1">RATE</label>
                                            <input type="text" id="txtrate" class="form-control input-md" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <input type="button" id="btnadd" class=" btn btn-primary" value="Callculate" />
                                <input type="button" id="btnclose" class="btn btn-danger " value="Cancel" />
                            </div>
                        </div>
                    </div>
                </div>

                <div id="mdlStandardImages" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Item Photos</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvUploadedPhotos" runat="server" CssClass="table table-responsive TestTable"
                                                    EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="PrId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="ItemId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>'
                                                                    Height="80px" Width="100px" />
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

                <div id="mdlReplacementImages" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Replacement Photos</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvViewReplacementImages" runat="server" CssClass="table table-responsive TestTable"
                                                    EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="PrId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="ItemId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>'
                                                                    Height="80px" Width="100px" />
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

                <div id="mdlSupportiveDocs" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Supportive Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvSupportiveDocuments" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" EmptyDataText="No Documents Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                                        <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Preview">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" href='<%#Eval("FilePath")%>'>View</asp:LinkButton>
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

                <div id="mdlBidMoreDetails" class="modal modal-primary fade" tabindex="-1" role="dialog" style="z-index: 3000;" aria-hidden="true">
                    <div class="modal-dialog" style="width: 80%">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Bid Details</h4>
                            </div>
                            <div class="modal-body" style="background-color: white;">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvBidMoreDetails" runat="server" CssClass="table table-responsive ChildGrid"
                                                    GridLines="None" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Replacement Images">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-default btn-sm" ID="btnViewzReplacementPhotosOfBidItem"
                                                                    runat="server" OnClick="btnViewzReplacementPhotosOfBidItem_Click"
                                                                    Text="View" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Standard Images">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-default btn-sm" ID="btnViewUploadPhotosOfBidItem"
                                                                    runat="server" OnClick="btnViewUploadPhotosOfBidItem_Click"
                                                                    Text="View" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Specifications">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-default btn-sm" runat="server"
                                                                    ID="lblViewBomOfBidItem" Text="View" OnClick="lblViewBomOfBidItem_Click"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supportive Documents">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-default btn-sm" ID="btnViewSupportiveDocumentsOfBidItem"
                                                                    runat="server" OnClick="btnViewSupportiveDocumentsOfBidItem_Click"
                                                                    Text="View" />
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

                <div id="TermCondition" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="false">
                    <div class="modal-dialog">
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="False">×</span></button>
                                <h4 class="modal-title">Add Terms & Condition</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class="table table-responsive" style="color: black">
                                            <tbody>
                                                <tr>
                                                    <td>Availability</td>
                                                    <td>
                                                        <input type="text" id="txtAvailability" class="form-control" /></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>Delivery</td>
                                                    <td>
                                                        <input id="txtDelivery" type="text" class="form-control" /></td>
                                                </tr>
                                                <tr>
                                                    <td>Credit</td>
                                                    <td>
                                                        <input id="txtCredit" type="text" class="form-control" /></td>
                                                </tr>
                                                <tr>
                                                    <td>Other</td>
                                                    <td>
                                                        <textarea id="txtOthers" cols="40" rows="5" class="form-control"></textarea>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="form-group">
                                    <input type="button" id="btnAdd1" class="btn btn-success" value="Add" onclick="addTerm(this)" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="mdlQuotationMoreDetails" class="modal modal-primary fade" tabindex="-1" role="dialog" style="z-index: 3000;" aria-hidden="true">
                    <div class="modal-dialog" style="width: 80%">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Quotation Attachments</h4>
                            </div>
                            <div class="modal-body" style="background-color: white;">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvQuotationMoreDetails" runat="server" CssClass="table table-responsive ChildGrid"
                                                    GridLines="None" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Quotation Images">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-default btn-sm" ID="btnViewQuotationImages"
                                                                    runat="server" OnClick="btnViewQuotationImages_Click"
                                                                    Text="View" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supportive Documents">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-default btn-sm" ID="btnViewQuotationSupportiveDocuments"
                                                                    runat="server" OnClick="btnViewQuotationSupportiveDocuments_Click"
                                                                    Text="View" />
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

                <div id="mdlQuotationImages" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Quotation Photos</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvQuotationImages" runat="server" CssClass="table table-responsive TestTable"
                                                    EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="QuotationId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="QuotationImageId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="ImagePath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("ImagePath") %>'
                                                                    Height="80px" Width="100px" />
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

                <div id="mdlQuotationDocs" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Quotation Supportive Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvQuotationDocs" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" EmptyDataText="No Documents Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                                        <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="Preview">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" href='<%#Eval("FilePath")%>'>View</asp:LinkButton>
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

                <div class="jumbotron text-center" style="background-color: #ECF0F5; margin-right: 18px; margin-left: 18px;">
                    <h2 runat="server" id="BidCode">BID : B2</h2>
                    <p>
                        Please Submit Your Bids Soon. This Bid Will Be Closed In
                    </p>
                    <p id="timer" style="color: red"><b>Loading..</b></p>
                    <p>
                        <small><b>NOTE : </b></small><small runat="server" id="bidNote">Quotations for this Bid can
                        only be subimitted by Registered Suppliers</small>
                    </p>
                    <div class="row" style="justify-content: center">
                        <h4>Select Supplier</h4>
                        <div class="col-md-4"></div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlSuppliers" runat="server" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged" onchange="showLoadingImage()">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlAgent" runat="server" CssClass="form-control" Visible="false">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" Visible="false">
                                </asp:DropDownList>
                            </div>
                            <asp:Image runat="server" ID="loadingImage" class="loadingImage hidden" src="AdminResources/images/Spinner-0.6s-200px.gif" Style="max-height: 40px" />
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <input type="button" style="float: left;" class="btn btn-success" onclick="redirectPage()" value="Create New Supplier" />
                            </div>
                            <div class="form-group">
                                <input type="button" style="float: left; display: none" class="btn btn-success" onclick="redirectPage()" value="Create New Agent" />
                            </div>
                        </div>

                    </div>
                </div>

                <section class="content">

                    <div id="">
                        <asp:Button ID="btnShowRejectedQuotation" runat="server" Visible="false" Text="Show Rejected Quotations" CssClass="btn btn-danger btn-sm" OnClick="btnShowRejectedQuotation_Click"/>
                        <asp:HiddenField ID="hndHideShow" runat="server" Value="0" />
                        <asp:GridView runat="server" ID="gvRejectedQuotationAndItems" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" Visible="false"
                            OnRowDataBound="gvRejectedQuotationAndItems_RowDataBound" EmptyDataText="No Quotation Found" DataKeyNames="QuotationId" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                    <img alt="" style="cursor: pointer;margin-top: -6px;"
                                        src="images/plus.png" />
                                        <asp:Panel ID="pnlQuotationItems" runat="server" Style="display: none">
                                            <asp:GridView runat="server" ID="gvQuotationItems" GridLines="None"
                                                CssClass="table table-responsive gvQuotationItems" AutoGenerateColumns="false" HeaderStyle-BackColor="#f8f8f8" HeaderStyle-ForeColor="black"
                                                DataKeyNames="QuotationItemId" Caption="Quotation Items">
                                                <Columns>
                                                    <asp:BoundField DataField="QuotationItemId" HeaderText="QuotaionItemId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="QuotationItemId hidden" />
                                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId"
                                                        HeaderStyle-CssClass="QuotationId hidden" ItemStyle-CssClass="QuotationId hidden" />
                                                    <asp:BoundField DataField="BiddingItemId" HeaderText="BidItemId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="BiddingItemId hidden" />
                                                    <asp:BoundField DataField="CategoryId" HeaderText="Category Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="CategoryId hidden" />
                                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="CategoryName hidden" />
                                                    <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategory Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="SubCategoryId hidden" />
                                                    <asp:BoundField DataField="SubCategoryName" HeaderText="Sub-Category Name"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="SubCategoryName hidden" />
                                                    <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="ItemId hidden" />
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"
                                                        ItemStyle-Width="100px" HeaderStyle-Width="100px" ItemStyle-CssClass="ItemName" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description"
                                                        ItemStyle-Width="100px" HeaderStyle-Width="100px" />
                                                    <asp:BoundField DataField="Qty" HeaderText="Quantity" ItemStyle-CssClass="Qty" />
                                                    <asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price" />
                                                    <asp:BoundField DataField="UnitPrice" HeaderText="Quoted Price" />
                                                    <asp:BoundField DataField="SubTotal" HeaderText="Subtotal" />
                                                    <asp:BoundField DataField="NbtAmount" HeaderText="NBT" />
                                                    <asp:BoundField DataField="VatAmount" HeaderText="VAT" />
                                                    <asp:BoundField DataField="TotalAmount" HeaderText="Net Total" />  
                                                    <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                         <asp:Label runat="server" Text='<%# Eval("IsQuotationItemApproved").ToString() =="0"? "Pending-Ok" :Eval("IsQuotationItemApproved").ToString() =="1" ?  "Approved" :"Rejected" %>'
                                                          ForeColor='<%# Eval("IsQuotationItemApproved").ToString() !="2" ?System.Drawing.Color.Green: System.Drawing.Color.Red %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>   
                                                    <asp:BoundField DataField="IsQuotationItemApprovalRemark" HeaderText="Remark" />                                                                                                                                                                                                             
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="QuotationId hidden" />                                    
                                    <asp:BoundField DataField="QuotationReferenceCode" HeaderText="Ref No" ItemStyle-Font-Bold="true" ItemStyle-CssClass="QuotationReferenceCode" />
                                    <asp:BoundField DataField="SubTotal" HeaderText="Sub Total" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="VatAmount" HeaderText="VAT Amount" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="NetTotal" HeaderText="Net Total" ItemStyle-Font-Bold="true" />
                                    <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                              <asp:Label runat="server" Text='<%# Eval("IsQuotationTabulationApproved").ToString()  =="0" ? "Pending-Ok":Eval("IsQuotationTabulationApproved").ToString()  =="1" ? "Approved" : "Rejected" %>'
                                                                                                ForeColor='<%# Eval("IsQuotationTabulationApproved").ToString() !="2" ?System.Drawing.Color.Green: System.Drawing.Color.Red %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                    </div>


                <div class="row" visible="false">
                    <div class="col-sm-12">
                        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <strong>
                                <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
                            </strong>
                        </div>
                    </div>
                </div>


                 <asp:Panel ID="pnlQuotations" runat="server" Visible="false">
                        <section class="content">
                    <div class="row">
                            <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">Submitted Quotation</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-xs-12"  style="overflow-x:auto">
                            <asp:GridView runat="server" ID="gvPreviousQuotations" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false"
                                 OnRowDataBound="gvPreviousQuotations_RowDataBound" EmptyDataText="No Previous Quotation Found" DataKeyNames="QuotationId" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                    <img alt="" style="cursor: pointer;margin-top: -6px;"
                                        src="images/plus.png" />
                                        <asp:Panel ID="pnlQuotationItems" runat="server" Style="display: none">
                                            <asp:GridView runat="server" ID="gvQuotationItems" GridLines="None"
                                                CssClass="table table-responsive gvQuotationItems" AutoGenerateColumns="false" HeaderStyle-BackColor="#f8f8f8" HeaderStyle-ForeColor="black"
                                                OnRowDataBound="gvQuotationItems_RowDataBound" DataKeyNames="QuotationItemId" Caption="Quotation Items">
                                                <Columns>
                                                    <asp:BoundField DataField="QuotationItemId" HeaderText="QuotaionItemId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="QuotationItemId hidden" />
                                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId"
                                                        HeaderStyle-CssClass="QuotationId hidden" ItemStyle-CssClass="QuotationId hidden" />
                                                    <asp:BoundField DataField="BiddingItemId" HeaderText="BidItemId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="BiddingItemId hidden" />
                                                    <asp:BoundField DataField="CategoryId" HeaderText="Category Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="CategoryId hidden" />
                                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="CategoryName hidden" />
                                                    <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategory Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="SubCategoryId hidden" />
                                                    <asp:BoundField DataField="SubCategoryName" HeaderText="Sub-Category Name"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="SubCategoryName hidden" />
                                                    <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="ItemId hidden" />
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"
                                                        ItemStyle-Width="100px" HeaderStyle-Width="100px" ItemStyle-CssClass="ItemName" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description"
                                                        ItemStyle-Width="100px" HeaderStyle-Width="100px" />
                                                    <asp:BoundField DataField="Qty" HeaderText="Quantity" ItemStyle-CssClass="Qty" />
                                                    <asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price" />
                                                    <asp:BoundField DataField="UnitPrice" HeaderText="Quoted Price" />
                                                    <asp:BoundField DataField="SubTotal" HeaderText="Subtotal" />
                                                    <asp:BoundField DataField="NbtAmount" HeaderText="NBT" />
                                                    <asp:BoundField DataField="VatAmount" HeaderText="VAT" />
                                                    <asp:BoundField DataField="TotalAmount" HeaderText="Net Total" />
                                                    <asp:TemplateField HeaderStyle-Width="300px"
                                                        ItemStyle-Width="300px" HeaderText="Item Specification">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="pnlSpecs" runat="server">
                                                                <asp:GridView ID="gvSpecs" runat="server" CssClass="ChildGridThree"
                                                                    ShowHeader="False" GridLines="None"
                                                                    AutoGenerateColumns="false" EmptyDataText="No Specification Found">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-Width="100px"
                                                                            ItemStyle-Width="100px">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox Enabled="false" ToolTip="Check if comply" Style="cursor: pointer"
                                                                                    ID="chkSpec" Text='<%#Eval("Material")%>'
                                                                                    runat="server" CssClass="chkSpec"
                                                                                    Checked='<%#Eval("Comply").ToString() =="1"?true:false%>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:BoundField DataField="Description"
                                                                            HeaderStyle-Width="100px"
                                                                            ItemStyle-Width="100px" />
                                                                        <asp:TemplateField HeaderStyle-Width="100px"
                                                                            ItemStyle-Width="100px">
                                                                            <ItemTemplate>
                                                                                <asp:Label Text='<%# "Remarks: " + Eval("Remarks").ToString()%>'
                                                                                    runat="server"/>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnQuoItemDelete" CssClass="btn btn-sm btn-danger" runat="server"
                                                                Text="Delete" Style="margin: 4px;" OnClick="btnQuoItemDelete_Click"></asp:Button>
                                                            </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="QuotationId hidden" />                                    
                                    <asp:BoundField DataField="QuotationReferenceCode" HeaderText="Ref No" ItemStyle-Font-Bold="true" ItemStyle-CssClass="QuotationReferenceCode" />
                                    <asp:BoundField DataField="SubTotal" HeaderText="Sub Total" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="VatAmount" HeaderText="VAT Amount" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="NetTotal" HeaderText="Net Total" ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="TermsAndCondition" HeaderText="Terms And Condition" HeaderStyle-CssClass="TermsAndCondition" ItemStyle-CssClass="TermsAndCondition" NullDisplayText="-" ItemStyle-Font-Bold="true" />
                                    <asp:TemplateField HeaderText="Attachments">
                                        <ItemTemplate>
                                            <asp:Button CssClass="btn btn-sm btn-default btnViewAttachmentsCl" runat="server"
                                                Text="View" OnClick="PreQuotationItemsView_Click"></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                    
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" CssClass="btn btn-sm btn-warning" runat="server"
                                                Text="Edit" Style="margin: 4px;" OnClick="btnEdit_Click"></asp:Button>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                        </asp:GridView> 
                                </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button ID="btnAddNewQuotation" runat="server" Text="Add New" CssClass="btn btn-primary pull-right"
                                OnClick="btnNewQuotationSubmit_Click" style="margin-right:10px" />
                        </div>
                                </div>
                    </div>
                </section></asp:Panel>

                <div class="row" id="divToScroll" runat="server">
                    <div class="col-md-12">
                        <div class="box">
                            <div class="box-header with-border">
                                <h3 class="box-title">Bid Items</h3>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-sm-8 form-group">
                                        <label class="box-title">Enter Quotation Ref Code.</label><label style="color:red;">(*)</label>
                                        <div class="col-sm-8 pull-right">
                                        <asp:TextBox ID="txtSuppQuotRefCode" runat="server" class="form-control txtQtyRefNo"/>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6"
                                            ForeColor="Red" Font-Bold="true" ControlToValidate="txtSuppQuotRefCode" ValidationGroup="btnSubmit">* Fill This Field</asp:RequiredFieldValidator>
                                            </div>
                                    </div>
                                    <div class="col-xs-12"  style="overflow-x:auto">
                                        <asp:GridView runat="server" ID="gvBidItems" GridLines="None"
                                            CssClass="table table-responsive" AutoGenerateColumns="False"
                                            OnRowDataBound="gvBidItems_RowDataBound" DataKeyNames="BiddingItemId" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                            <Columns>
                                                <asp:BoundField DataField="BiddingItemId" HeaderText="BidItemId"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PrdId" HeaderText="PRDId"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CategoryId" HeaderText="Category Id"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                                </asp:BoundField>
                                                <%--<asp:BoundField DataField="CategoryName" HeaderText="Category Name"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategory Id"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SubCategoryName" HeaderText="Sub-Category Name"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                                </asp:BoundField>
                                               <%-- <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name"
                                                    ItemStyle-Width="200px" HeaderStyle-Width="200px" ItemStyle-CssClass="ItemName" >
                                                <HeaderStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Supplier Mentioned Item Name">
                                                    <ItemTemplate>
                                                        <asp:TextBox  ID="txtSupItemName" Text="" CssClass="txtSupItemName"
                                                            type="text" value="" runat="server" Width="160px" 
                                                            autocomplete="off"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HS ID">
                                                    <ItemTemplate>
                                                        <asp:TextBox  ID="txtHSID" Text="" CssClass="txtHSID"
                                                            type="text" value='<%#Eval("HsId")%>' runat="server" Width="160px"
                                                            autocomplete="off"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Qty" HeaderText="Quantity" />

                                                <asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price"    DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                           
                                                    <asp:TemplateField HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:label  ID="lblItemId" CssClass="lblItemId" runat="server" Text='<%#Eval("ItemId")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="More Details" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Button CssClass="btn btn-default btn-xs" ID="btnMoreBidItemDetails"
                                                            runat="server" OnClick="btnMoreBidItemDetails_Click"
                                                            Text="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Tender Ref. NO:">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRefno" Text='<%#Eval("Refno")%>'
                                                            type="text" value="" runat="server" Width="80px"
                                                            autocomplete="off" CssClass="txtDescription" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:TextBox TextMode="MultiLine" Rows="4" ID="txtDescription" Text='<%#Eval("Description")%>'
                                                            type="text" value="" runat="server" Width="160px" Height ="40px"
                                                            autocomplete="off" CssClass="txtDescription" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Item Ref Code">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtItemRefCode" Text='<%#Eval("ItemReferenceCode")%>'
                                                            type="text" value="" runat="server" Width="80px"
                                                            autocomplete="off" CssClass="txtItemRefCode" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Brand">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtBrand" 
                                                            type="text" Enabled="false" Text='<%#Eval("Brand")%>' runat="server" Width="80px"
                                                            autocomplete="off" CssClass="txtDescription" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                               
                                                    <asp:TemplateField HeaderText="CurrencyId"  ItemStyle-CssClass="hidden"   HeaderStyle-CssClass="hidden" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrencyId" CssClass="lblCurrencyIdCl" Text='<%#Eval("CurrencyId")%>' runat="server"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Currency" HeaderStyle-Width="120px" ItemStyle-CssClass="hidden"   HeaderStyle-CssClass="hidden" >
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="dllCurrency" CssClass="ddlCurrency" runat="server" Width="80px"  ></asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="120px" />
                                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Exchange Rate" HeaderStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtExchangeRate" Text='<%#Eval("ExchangeRate")%>' type="number"  min="0" runat="server" Width="80px" DataFormatString="{0:N2}"
                                                                CssClass="txtExhchangeRateCl" />
                                                        <%--<asp:TextBox ID="txtExchangeRate" Text="" type="number"  min="0" runat="server" Width="80px" DataFormatString="{0:N2}" />
                                                   --%> </ItemTemplate>
                                                            <HeaderStyle Width="120px" />
                                                </asp:TemplateField>
                                                          
                                                    <asp:TemplateField HeaderText="Terms" >
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlTerms" runat="server" Enabled="false" AutoPostBack="true" Width="120px" OnSelectedIndexChanged="ddlTerms_SelectedIndexChanged"  Class="form-control ddlterms">
									                        <asp:ListItem Text="Select Term" Value="0"></asp:ListItem>
								                        <asp:ListItem Text="CIF" Value="CIF"></asp:ListItem>
								                        <asp:ListItem Text="CFR" Value="CFR"></asp:ListItem>
                                                        <asp:ListItem Text="CNF" Value="CNF"></asp:ListItem>
								                        <asp:ListItem Text="EXW" Value="EXW"></asp:ListItem>
                                                        <asp:ListItem Text="FOB" Value="FOB"></asp:ListItem>
								                        <asp:ListItem Text="CTP" Value="CTP"></asp:ListItem>
                                                        <asp:ListItem Text="CIP" Value="CIP"></asp:ListItem>
								                        <asp:ListItem Text="DDU" Value="DDU"></asp:ListItem>
								                        <asp:ListItem Text="LOCAL" Value="LOCAL"></asp:ListItem>
                                                        <asp:ListItem Text="FAS" Value="FAS"></asp:ListItem>
                                                        <asp:ListItem Text="FAC" Value="FAC"></asp:ListItem>
								                        <asp:ListItem Text="DAT" Value="DAT"></asp:ListItem>
                                                        <asp:ListItem Text="DAP" Value="DAP"></asp:ListItem>
                                                        <asp:ListItem Text="DDP" Value="DDP"></asp:ListItem>
							                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CIF">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCif" Text='<%#Eval("CIF")%>'
                                                             Enabled="false" type="number"  step="any" min="0" runat="server" Width="80px" DataFormatString="{0:N2}"
                                                            CssClass="cltxtCif" autocomplete="off"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Duty & PAL">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtdutypal" Text='<%#Eval("Duty&pal")%>'
                                                             runat="server" Width="80px" Enabled="false"
                                                            CssClass="DutypalCal"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Est Clearing /Mt">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtClearing" Text='<%#Eval("Clearing")%>'
                                                               Enabled="false"  type="number" step="any" min="0" runat="server" Width="80px" DataFormatString="{0:N2}"
                                                            CssClass="ClearingCal"  autocomplete="off"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Other">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtother" Text='<%#Eval("Other")%>'
                                                               Enabled="false" type="number" step="any" min="0" runat="server" Width="80px" DataFormatString="{0:N2}"
                                                            CssClass="othercal"  autocomplete="off"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quoted Unit Price" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtUnitPrice" Text='<%#Eval("UnitPrice")%>'
                                                            type="number" step="any" min="0" runat="server" Width="80px" DataFormatString="{0:N2}"
                                                            autocomplete="off" CssClass="txtUnitPriceCl" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sub Total">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSubTotal" Enabled="false" Text='<%#Eval("SubTotal")%>'
                                                            runat="server" Width="90px" CssClass="txtSubTotalCl" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="History">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlHistory" runat="server" Enabled="false" Width="80px">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
								                        <asp:ListItem Text="Used" Value="1"></asp:ListItem>
								                        <asp:ListItem Text="New" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Sample Checked" Value="3"></asp:ListItem>
							                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Validity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtvalidity" Text='<%#Eval("Validity")%>' type="date" data-date="" data-date-format="DD MMMM YYYY" CssClass="validity" runat="server" Width="140px" Height="22px"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EST. Delivery">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEstdelivery" Text='<%#Eval("Estdelivery")%>'
                                                            type="text" value="" runat="server" Width="80px"
                                                            autocomplete="off" CssClass="txtDescription" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemark" Text='<%#Eval("Remark")%>'
                                                            type="text" value="" runat="server" Width="80px"
                                                            autocomplete="off" CssClass="txtDescription" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Include NBT/VAT">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkNbt" Text="NBT" runat="server" style="cursor:pointer"
                                                            Checked='<%#Eval("HasNbt").ToString() =="1"?true:false%>'
                                                            CssClass="chkNbtCl" Visible='<%#Eval("HasNBTRate").ToString() =="1"?true:false%>'   />
                                                        <asp:CheckBox ID="chkVat" Text="VAT" runat="server" style="cursor:pointer"
                                                            Checked='<%#Eval("HasVat").ToString() =="1"?true:false%>'
                                                            CssClass="chkVatCl"   VatRate ='<%#Eval("VatRate").ToString()%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="NBT Percentage"  >
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rdoNbt204" GroupName="grpPercentage" style="cursor:pointer"
                                                            Text='<%#Eval("NBTRate1").ToString()%>' runat="server" Checked='<%#Eval("NbtCalculationType").ToString() =="1"?true:false%>'
                                                            CssClass="rdo204" /><br />
                                                        <asp:RadioButton ID="rdoNbt2" GroupName="grpPercentage" style="cursor:pointer"
                                                            Text='<%#Eval("NBTRate2").ToString()%>' runat="server" Checked='<%#Eval("NbtCalculationType").ToString() =="2"?true:false%>'
                                                            CssClass="rdo2" />                                                                   
                                                                
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="NBT">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNbt"  Text='<%#Eval("NbtAmount")%>'
                                                            runat="server" Width="80px" CssClass="txtNbtCl"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="VAT">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtVat" Text='<%#Eval("VatAmount")%>'
                                                            runat="server" Width="80px" CssClass="txtVatCl"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Net Total">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNetTotal" Enabled="false" Text='<%#Eval("NetTotal")%>'
                                                            runat="server" Width="90px" CssClass="txtNetTotalCl" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                             
                                                <asp:TemplateField HeaderStyle-Width="150px"
                                                    ItemStyle-Width="150px" HeaderText="Item Specification" ItemStyle-CssClass="ItemSpecification">
                                                    <ItemTemplate>
                                                        <asp:Panel ID="pnlSpecs" runat="server">
                                                            <asp:GridView ID="gvSpecs" runat="server"
                                                                ShowHeader="False" GridLines="None"
                                                                AutoGenerateColumns="false" EmptyDataText="No Specification Found">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-Width="75px"
                                                                        ItemStyle-Width="75px">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ToolTip="Check if comply" style="cursor:pointer"
                                                                                ID="chkSpec" Text='<%#Eval("Material")%>'
                                                                                runat="server" CssClass="chkSpec"
                                                                                Checked='<%#Eval("Comply").ToString() =="1"?true:false%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="Description"
                                                                        ItemStyle-HorizontalAlign="Right"
                                                                        HeaderStyle-Width="75px"
                                                                        ItemStyle-Width="75px" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-4 col-md-push-8 col-sm-4 col-sm-push-8" style="text-align:right; font-size:16px;">                                                
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <label  class="summary-title">Sub Total</label>
                                            </div>
                                            <div class="col-xs-6">
                                                <label id="lblSubTotal" class="summary-data">0.00</label>
                                            </div>
                                            <div class="col-xs-6">
                                                <label class="summary-title">Total NBT</label>
                                            </div>
                                            <div class="col-xs-6">
                                                <label id="lblNbtTotal" class="summary-data">0.00</label>
                                            </div>
                                            <div class="col-xs-6">
                                                <label class="summary-title">Total VAT</label>
                                            </div>
                                            <div class="col-xs-6">
                                                <label id="lblVatTotal" class="summary-data">0.00</label>
                                            </div>
                                            <div class="col-xs-6">
                                                <label class="summary-title">Net Total</label>
                                            </div>
                                            <div class="col-xs-6">
                                                <label id="lblNetTotal" class="summary-data">0.00</label>
                                            </div>
                                        </div>                                                
                                    </div>
                                </div>
                                <hr />

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="txtTermsAndConditions">Terms & Conditions</label>
				                            <div class="form-group col-sm-3 pull-right">
					                            <input type="button" id="btnTermCondition"  onclick="showAddTermCondition()" class="btn btn-warning btn-group-justified" value="Add" ></input>
				                            </div>
                                            <asp:TextBox TextMode="MultiLine" Rows="4" ID="txtTermsAndConditions"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="fileImages">Quotation Images</label>
                                            <asp:FileUpload ID="fileImages" runat="server" AllowMultiple="true"
                                                CssClass="form-control" accept="image/*" data-type='image'></asp:FileUpload>
                                            <%-- <input type="file" class="form-control col-sm-3"  id="files" style="width:80%" name="files" accept="image/*" data-type='image' multiple  />
                   <asp:Button runat="server" ID="btnUploadImage" OnClick="btnUploadImage_Click" Text="Upload" CssClass="uploadButton" />--%>
                                        </div>
                                        <div class="form-group">
                                            <asp:Panel ID="pnlImages" Visible="false" runat="server">
                                            <label for="fileImages">Previously Uploded Images</label>                                             
                                                <asp:GridView ID="gvImages" runat="server" ShowHeader="False"
                                                    GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Image Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="QuotationImageId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink runat="server" href='<%#Eval("ImagePath").ToString().Remove(0,2)%>' target="_blank">
                                                                    <asp:Image runat="server" ImageUrl='<%#Eval("ImagePath")%>' style="max-height:50px; width:auto; margin:5px" />
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnRemoveImage" Text="Remove" runat="server" OnClick="btnRemoveImage_Click" CssClass="btn btn-danger btn-xs"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group">
                                            <label for="fileDocs">Supportive Documents</label>
                                            <asp:FileUpload ID="fileDocs" runat="server" AllowMultiple="true"
                                                CssClass="form-control"></asp:FileUpload>
                                        </div>
                                        <div class="form-group">
                                            <asp:Panel ID="pnlDocs" Visible="false" runat="server"  Width="100%">
                                            <label for="fileImages">Previously Uploded Documents</label>                                                    
                                                <asp:GridView ID="gvDocs" runat="server" ShowHeader="False"
                                                    GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Document Found" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="QuotationFileId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                        <asp:TemplateField ItemStyle-Height="30px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton
                                                                    Text='<%#Eval("FileName")%>' runat="server" href='<%#Eval("FilePath").ToString().Remove(0,2)%>' target="_blank" style="margin-right:5px;"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnRemoveFile" Text="Remove" runat="server" OnClick="btnRemoveFile_Click" CssClass="btn btn-danger btn-xs"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                <asp:Panel ID="pnlImports" runat="server" Visible="false">

                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                        <div class="form-group">
                                                <label for="exampleInputEmail1">Currency Detail</label>
                                              <%-- <asp:DropDownList ID="ddlCurrencyDetail" runat="server" CssClass="form-control" AutoPostBack="true" onselectedindexchanged="ddlCurrencyDetail_SelectedIndexChanged">
                                               --%> 
                                            <asp:DropDownList ID="ddlCurrencyDetail" runat="server" CssClass="form-control" AutoPostBack="true" onselectedindexchanged="ddlCurrencyDetail_SelectedIndexChanged">
                                               
                                            </asp:DropDownList>
                                                </div>
                                        <div class="form-group">
                                                <label for="exampleInputEmail1">Payment Mode</label>
                                               <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control" AutoPostBack="true" >
                                                </asp:DropDownList>
                                                </div>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Clearing Agent</label>
                                               <asp:DropDownList ID="ddlClearingAgent" runat="server" CssClass="form-control"  AutoPostBack="true" >
                                                </asp:DropDownList>
                                                </div>
                                            </div>
                                       
                                        
                                    
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Mode of Transport</label>
                                               <asp:DropDownList ID="ddlTransportMode" runat="server" CssClass="form-control"  AutoPostBack="true" >
                                                </asp:DropDownList>
                                                </div>

                                            <div class="form-group">
                                                <label for="exampleInputEmail1">Container Size & Qty</label>
                                               <asp:DropDownList ID="ddlContainerSize" runat="server" CssClass="form-control"  AutoPostBack="true" >
                                                </asp:DropDownList>
                                                </div>

                                             <%--<div class="form-group">
                                                <label for="exampleInputEmail1">Price Terms</label>
                                               <asp:DropDownList ID="ddlPriceTerms" runat="server" CssClass="form-control" AutoPostBack="true" >
                                                </asp:DropDownList>
                                                </div>--%>

                                 </div>
                                        </div>
                                    </asp:Panel>

                                </div>
                                 <hr/>

                <div class="row" id="divBidBondDetail" runat="server" visible="false">
                    <div class="col-md-12">
                        <div class="form-group">
                         <label for="txtTermsAndConditions">Bid Bond details</label><label style="color:red;">(*)</label>
                        </div>
                        <div class="box" id="Div1" runat="server">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="BondNo">Bond No.</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtBondNo" >* Fill This Field</asp:RequiredFieldValidator>
                                            <asp:TextBox  ID="txtBondNo"  runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                                <label for="Bank">Bank</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtBank">* Fill This Field</asp:RequiredFieldValidator>
                                            <asp:TextBox  ID="txtBank"  runat="server" CssClass="form-control" ValidationGroup="btnSubmit"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                                <label for="ReceiptNo">Receipt No.</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtReceiptNo" >* Fill This Field</asp:RequiredFieldValidator>
                                            <asp:TextBox  ID="txtReceiptNo"  runat="server" CssClass="form-control" ValidationGroup="btnSubmit"></asp:TextBox>
                                        </div>
                                                
                                    </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="BondAmount">Bond Amount.</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtBondAmount"  >* Fill This Field</asp:RequiredFieldValidator>
                                            <asp:TextBox  ID="txtBondAmount" type="number"  runat="server" CssClass="form-control" ValidationGroup="btnSubmit"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="ExpireDOB">Expire Date of Bond.</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtExpireDOB"  >* Fill This Field</asp:RequiredFieldValidator>
                                            <asp:TextBox  ID="txtExpireDOB"  runat="server" CssClass="form-control date1"  ValidationGroup="btnSubmit"></asp:TextBox>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                            </div>
                            <div class="box-footer">
                                <a id="btnCancel" class="btn btn-danger pull-right" style="margin-right:10px" href="viewPrForManualQuotationSubmission.aspx">Cancel</a>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit Quotation" CssClass="btn btn-primary pull-right"
                                    OnClick="btnSubmit_Click" style="margin-right:10px" />
                            </div>
                        </div>
                    </div>
                </div>

            </section>

                <asp:HiddenField ID="hdnSubTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNbtTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnVatTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNetTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hndQuotationItemId" runat="server" Value="" />
                <asp:HiddenField ID="hndImport" runat="server" Value="" />
                <asp:HiddenField ID="hdnEndDate" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
                <%--                <asp:PostBackTrigger ControlID="btnTermCondition" />--%>
                <%--<asp:PostBackTrigger ControlID="btnUploadImage" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </form>

    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />

    <script type="text/javascript">
        // Set the date we're counting down to
        var countDownDate = new Date($('#ContentSection_hdnEndDate').val()).getTime();

        // Update the count down every 1 second
        var x = setInterval(function () {

            // Get todays date and time
            var now = new Date().getTime();

            // Find the distance between now and the count down date
            var distance = countDownDate - now;

            // Time calculations for days, hours, minutes and seconds
            var days = Math.floor(distance / (1000 * 60 * 60 * 24));
            var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);

            // Output the result in an element with id="demo"
            document.getElementById("timer").innerHTML = "<b>" + days + "d : " + hours + "h : "
                + minutes + "m : " + seconds + "s </b>";

            // If the count down is over, write some text 
            if (distance < 0) {
                clearInterval(x);
                document.getElementById("timer").innerHTML = "EXPIRED";
                $('#ContentSection_btnSubmit').prop("disabled", "disabled");
            }
        }, 1000);

        Sys.Application.add_load(function () {

            $(function () {
                $.datepicker.setDefaults({
                    showOn: "both",
                    buttonImageOnly: true,
                    buttonImage: "calendar.gif",
                    buttonText: "Calendar"
                });

                $('#mdlStandardImages').on('hide.bs.modal', function () {
                    $('.modal-backdrop').remove();
                    $('#mdlBidMoreDetails').modal('show');
                });
                $('#mdlSupportiveDocs').on('hide.bs.modal', function () {
                    $('.modal-backdrop').remove();
                    $('#mdlBidMoreDetails').modal('show');
                });
                $('#mdlReplacementImages').on('hide.bs.modal', function () {
                    $('.modal-backdrop').remove();
                    $('#mdlBidMoreDetails').modal('show');
                });
                $('#mdlItemSpecs').on('hide.bs.modal', function () {
                    $('.modal-backdrop').remove();
                    $('#mdlBidMoreDetails').modal('show');
                });
                $('#mdlBidMoreDetails').on('show.bs.modal', function () {
                    $('body').css("padding-right", "0");
                })

                $('#mdlQuotationDocs').on('hide.bs.modal', function () {
                    $('.modal-backdrop').remove();
                    $('#mdlQuotationMoreDetails').modal('show');
                });

                $('#mdlQuotationImages').on('hide.bs.modal', function () {
                    $('.modal-backdrop').remove();
                    $('#mdlQuotationMoreDetails').modal('show');
                });

                $('#lblSubTotal').html($('#ContentSection_hdnSubTotal').val());
                $('#lblNbtTotal').html($('#ContentSection_hdnNbtTotal').val());
                $('#lblVatTotal').html($('#ContentSection_hdnVatTotal').val());
                $('#lblNetTotal').html($('#ContentSection_hdnNetTotal').val());

                $('#ContentSection_btnSubmit').on({
                    click: function () {
                        //debugger;
                        prevent = false;
                        if ($("#<%=txtSuppQuotRefCode.ClientID%>").val() == "") {
                            swal({ type: 'error', title: 'ERROR', text: 'Please enter quotation reference no' });
                            event.preventDefault();
                        } else {
                            var netTotals = $(document).find('.txtNetTotalCl');
                            if (parseFloat($("#lblSubTotal").text()) == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please enter data and submit' });
                                event.preventDefault();
                            }
                        }

                        if (prevent) {
                            event.preventDefault();
                        }
                        else {
                            $(document).find('.txtSubTotalCl').removeAttr('disabled');
                            $(document).find('.txtNbtCl').removeAttr('disabled');
                            $(document).find('.txtVatCl').removeAttr('disabled');
                            $(document).find('.txtNetTotalCl').removeAttr('disabled');
                        }

                    }
                });

                $('.txtQtyRefNo').on({

                    keypress: function () {
                        // debugger;
                        if ($('#ContentSection_ddlSuppliers').prop('selectedIndex') == 0) {
                            swal({ type: 'error', title: 'ERROR', text: 'Please Select A Supplier' });
                            event.preventDefault();
                        }
                        else if ($('#ContentSection_hndImport').val() == 1) {
                            if ($('#ContentSection_ddlAgent').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Agent' });
                                event.preventDefault();
                            }
                            if ($('#ContentSection_ddlCountry').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Country' });
                                event.preventDefault();
                            }
                        }
                    }

                });

                $('.txtUnitPriceCl').on({

                    keyup: function () {
                        //  debugger;
                        calculate(this);
                    },

                    keypress: function () {
                        //  debugger;
                        if ($('#ContentSection_ddlSuppliers').prop('selectedIndex') == 0) {
                            swal({ type: 'error', title: 'ERROR', text: 'Please Select A Supplier' });
                            event.preventDefault();
                        }
                        else if ($('#ContentSection_hndImport').val() == 1) {
                            if ($('#ContentSection_ddlAgent').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Agent' });
                                event.preventDefault();
                            }
                            if ($('#ContentSection_ddlCountry').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Country' });
                                event.preventDefault();
                            }
                        }
                    }

                });

                $('.chkNbtCl').on({
                    change: function () {
                        calculate(this);
                    },
                    click: function () {
                        if ($('#ContentSection_ddlSuppliers').prop('selectedIndex') == 0) {
                            swal({ type: 'error', title: 'ERROR', text: 'Please Select A Supplier' });
                            event.preventDefault();
                        }
                        else if ($('#ContentSection_hndImport').val() == 1) {
                            if ($('#ContentSection_ddlAgent').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Agent' });
                                event.preventDefault();
                            }
                            if ($('#ContentSection_ddlCountry').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Country' });
                                event.preventDefault();
                            }
                        }
                    }
                });

                $('.chkSpec').on({
                    click: function () {
                        if ($('#ContentSection_ddlSuppliers').prop('selectedIndex') == 0) {
                            swal({ type: 'error', title: 'ERROR', text: 'Please Select A Supplier' });
                            event.preventDefault();
                        }
                        else if ($('#ContentSection_hndImport').val() == 1) {
                            if ($('#ContentSection_ddlAgent').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Agent' });
                                event.preventDefault();
                            }
                            if ($('#ContentSection_ddlCountry').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Country' });
                                event.preventDefault();
                            }
                        }
                    }
                });

                $('.chkVatCl').on({
                    change: function () {
                        calculate(this);
                    },
                    click: function () {
                        if ($('#ContentSection_ddlSuppliers').prop('selectedIndex') == 0) {
                            swal({ type: 'error', title: 'ERROR', text: 'Please Select A Supplier' });
                            event.preventDefault();
                        }
                        else if ($('#ContentSection_hndImport').val() == 1) {
                            if ($('#ContentSection_ddlAgent').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Agent' });
                                event.preventDefault();
                            }
                            if ($('#ContentSection_ddlCountry').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Country' });
                                event.preventDefault();
                            }
                        }
                    }
                });

                $('.rdo204').on({
                    change: function () {
                        calculate(this);
                    },
                    click: function () {
                        if ($('#ContentSection_ddlSuppliers').prop('selectedIndex') == 0) {
                            swal({ type: 'error', title: 'ERROR', text: 'Please Select A Supplier' });
                            event.preventDefault();
                        }
                        else if ($('#ContentSection_hndImport').val() == 1) {
                            if ($('#ContentSection_ddlAgent').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Agent' });
                                event.preventDefault();
                            }
                            if ($('#ContentSection_ddlCountry').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Country' });
                                event.preventDefault();
                            }
                        }
                    }
                });

                $('.rdo2').on({
                    change: function () {
                        calculate(this);
                    },
                    click: function () {
                        if ($('#ContentSection_ddlSuppliers').prop('selectedIndex') == 0) {
                            swal({ type: 'error', title: 'ERROR', text: 'Please Select A Supplier' });
                            event.preventDefault();
                        }
                        else if ($('#ContentSection_hndImport').val() == 1) {
                            if ($('#ContentSection_ddlAgent').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Agent' });
                                event.preventDefault();
                            }
                            if ($('#ContentSection_ddlCountry').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Country' });
                                event.preventDefault();
                            }
                        }
                    }
                });

                $('.txtDescription').on({
                    change: function () {
                        calculate(this);
                    },
                    click: function () {
                        if ($('#ContentSection_ddlSuppliers').prop('selectedIndex') == 0) {
                            swal({ type: 'error', title: 'ERROR', text: 'Please Select A Supplier' });
                            event.preventDefault();
                        }
                        else if ($('#ContentSection_hndImport').val() == 1) {
                            if ($('#ContentSection_ddlAgent').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Agent' });
                                event.preventDefault();
                            }
                            if ($('#ContentSection_ddlCountry').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Country' });
                                event.preventDefault();
                            }
                        }
                    }
                });
                $('.txtNbtCl').on({
                    keyup: function () {
                        calculate2(this);
                    },
                    keypress: function (evt) {
                        if ($('#ContentSection_ddlSuppliers').prop('selectedIndex') == 0) {
                            swal({ type: 'error', title: 'ERROR', text: 'Please Select A Supplier' });
                            event.preventDefault();
                        }
                        else if ($('#ContentSection_hndImport').val() == 1) {
                            if ($('#ContentSection_ddlAgent').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Agent' });
                                event.preventDefault();
                            }
                            if ($('#ContentSection_ddlCountry').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Country' });
                                event.preventDefault();
                            }
                        }
                        var theEvent = evt || window.event;
                        var key = theEvent.keyCode || theEvent.which;
                        key = String.fromCharCode(key);
                        var regex = /^\d+(\.\d+)?$/;
                        if (!regex.test(key)) {
                            theEvent.returnValue = false;
                            if (theEvent.preventDefault) theEvent.preventDefault();
                        }
                    }
                });

                $('.txtVatCl').on({

                    keyup: function () {
                        calculate2(this);
                    },

                    keypress: function () {
                        if ($('#ContentSection_ddlSuppliers').prop('selectedIndex') == 0) {
                            swal({ type: 'error', title: 'ERROR', text: 'Please Select A Supplier' });
                            event.preventDefault();
                        }
                        else if ($('#ContentSection_hndImport').val() == 1) {
                            if ($('#ContentSection_ddlAgent').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Agent' });
                                event.preventDefault();
                            }
                            if ($('#ContentSection_ddlCountry').prop('selectedIndex') == 0) {
                                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Country' });
                                event.preventDefault();
                            }
                        }
                    }

                });


                function calculate(elmt) {
                    //debugger;
                    var unitPrice = $(elmt).closest('tr').find('.txtUnitPriceCl').val();

                    if (unitPrice == '' || unitPrice == null) {
                        unitPrice = 0;
                    }
                    var qty = parseFloat($(elmt).closest('tr').find('td').eq(9).html());
                    var subTot = 0;
                    var nbt = 0;
                    var vat = 0;
                    var netTot = 0;
                    var exchangeRate = 1;
                    // if(){
                    // exchangeRate = $(elmt).closest('tr').find('.txtExhchangeRateCl').val();
                    // }
                    //  unitPrice = unitPrice * exchangeRate;
                    subTot = unitPrice * qty;

                    var chkNbt = $(elmt).closest('tr').find('.chkNbtCl').find('input');
                    var chkVat = $(elmt).closest('tr').find('.chkVatCl').find('input');


                    var rdoNbt204 = $(elmt).closest('tr').find('.rdo204').find('input');
                    var rdoNbt2 = $(elmt).closest('tr').find('.rdo2').find('input');

                    // debugger;
                    if ($(chkNbt).prop('checked') == true) {
                        if ($(rdoNbt204).prop('checked') == true) {
                            //nbt = parseFloat((subTot * 2) / 98);
                            var t = $(rdoNbt204).parent().find("label").text();
                            nbt = parseFloat(t.substring(0, t.length - 1)) / 100;
                            nbt = parseFloat(nbt * subTot);
                        }
                        else {
                            //nbt = parseFloat((subTot * 2) / 100);
                            var t = $(rdoNbt2).parent().find("label").text();
                            nbt = parseFloat(t.substring(0, t.length - 1)) / 100;
                            nbt = parseFloat(nbt * subTot);
                        }

                    }

                    if ($(chkVat).prop('checked') == true) {

                        //vat = parseFloat((subTot + nbt) * 0.15);
                        var vatRate = parseFloat($(chkVat).parent().attr('VatRate'))
                        vat = parseFloat((subTot + nbt) * vatRate);
                    }

                    netTot = subTot + nbt + vat;



                    $(elmt).closest('tr').find('.txtSubTotalCl').val(subTot.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));
                    $(elmt).closest('tr').find('.txtNbtCl').val(nbt.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));
                    $(elmt).closest('tr').find('.txtVatCl').val(vat.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));
                    $(elmt).closest('tr').find('.txtNetTotalCl').val(netTot.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));

                    var tableRows = $(elmt).closest('tbody').find('> tr:not(:has(>td>table))');

                    var globSubTotal = 0;
                    var globTotalNbt = 0;
                    var globTotalVat = 0;
                    var globNetTotal = 0;

                    for (i = 1; i < tableRows.length; i++) {
                        if ($(tableRows[i]).find('.txtSubTotalCl').val() != '') {
                            var txtSubTClx = $(tableRows[i]).find('.txtSubTotalCl').val().replace(/,/g, '');
                            globSubTotal = globSubTotal + parseFloat(txtSubTClx);
                        }
                        if ($(tableRows[i]).find('.txtNbtCl').val() != '') {
                            var txtNbTCLx = $(tableRows[i]).find('.txtNbtCl').val().replace(/,/g, '');
                            globTotalNbt = globTotalNbt + parseFloat(txtNbTCLx);
                        }
                        if ($(tableRows[i]).find('.txtVatCl').val() != '') {
                            var txtVatClx = $(tableRows[i]).find('.txtVatCl').val().replace(/,/g, '');
                            globTotalVat = globTotalVat + parseFloat(txtVatClx);
                        }
                        if ($(tableRows[i]).find('.txtNetTotalCl').val() != '') {
                            var txtNetTotalClx = $(tableRows[i]).find('.txtNetTotalCl').val().replace(/,/g, '');
                            globNetTotal = globNetTotal + parseFloat(txtNetTotalClx);
                        }
                    }

                    $('#lblSubTotal').html(globSubTotal.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));
                    $('#lblNbtTotal').html(globTotalNbt.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));
                    $('#lblVatTotal').html(globTotalVat.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));
                    $('#lblNetTotal').html(globNetTotal.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));


                    $('#ContentSection_hdnSubTotal').val(globSubTotal.toFixed(2));
                    $('#ContentSection_hdnNbtTotal').val(globTotalNbt.toFixed(2));
                    $('#ContentSection_hdnVatTotal').val(globTotalVat.toFixed(2));
                    $('#ContentSection_hdnNetTotal').val(globNetTotal.toFixed(2));
                }

                function calculate2(elmt) {
                    subTot = $(elmt).closest('tr').find('td').find('.txtSubTotalCl').val().replace(/,/g, '');
                    nbt = $(elmt).closest('tr').find('td').find('.txtNbtCl').val().replace(/,/g, '');
                    vat = $(elmt).closest('tr').find('td').find('.txtVatCl').val().replace(/,/g, '');
                    netTot = Number(subTot) + Number(nbt) + Number(vat);
                    $(elmt).closest('tr').find('td').find('.txtNbtCl').val(nbt.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));
                    $(elmt).closest('tr').find('td').find('.txtVatCl').val(vat.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));
                    $(elmt).closest('tr').find('td').find('.txtNetTotalCl').val(netTot.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));

                    var tableRows = $(elmt).closest('tbody').find('> tr:not(:has(>td>table))');

                    var globSubTotal = 0;
                    var globTotalNbt = 0;
                    var globTotalVat = 0;
                    var globNetTotal = 0;

                    for (i = 1; i < tableRows.length; i++) {
                        if ($(tableRows[i]).find('.txtNbtCl').val() != '') {
                            var txtNbTCLx = $(tableRows[i]).find('.txtNbtCl').val().replace(/,/g, '');
                            globTotalNbt = globTotalNbt + parseFloat(txtNbTCLx);
                        }
                        if ($(tableRows[i]).find('.txtVatCl').val() != '') {
                            var txtVatClx = $(tableRows[i]).find('.txtVatCl').val().replace(/,/g, '');
                            globTotalVat = globTotalVat + parseFloat(txtVatClx);
                        }
                        if ($(tableRows[i]).find('.txtNetTotalCl').val() != '') {
                            var txtNetTotalClx = $(tableRows[i]).find('.txtNetTotalCl').val().replace(/,/g, '');
                            globNetTotal = globNetTotal + parseFloat(txtNetTotalClx);
                        }
                    }


                    $('#lblNbtTotal').html(globTotalNbt.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));
                    $('#lblVatTotal').html(globTotalVat.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));
                    $('#lblNetTotal').html(globNetTotal.toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));

                    $('#ContentSection_hdnNbtTotal').val(globTotalNbt.toFixed(2));
                    $('#ContentSection_hdnVatTotal').val(globTotalVat.toFixed(2));
                    $('#ContentSection_hdnNetTotal').val(globNetTotal.toFixed(2));
                }
            });



        });

        Sys.Application.add_load(function () {
            $(".date1").datepicker({
                format: 'MM/DD/YYYY',
                changeYear: true
            });

            function calculate(elmt) {

                var unitPrice = $(elmt).closest('tr').find('.txtUnitPriceCl').val();

                if (unitPrice == '' || unitPrice == null) {
                    unitPrice = 0;
                }
                var qty = parseFloat($(elmt).closest('tr').find('td').eq(9).html());
                var subTot = 0;
                var nbt = 0;
                var vat = 0;
                var netTot = 0;
                var exchangeRate = 1;
                // if(){
                // exchangeRate = $(elmt).closest('tr').find('.txtExhchangeRateCl').val();
                // }
                //  unitPrice = unitPrice * exchangeRate;
                subTot = unitPrice * qty;

                var chkNbt = $(elmt).closest('tr').find('.chkNbtCl').find('input');
                var chkVat = $(elmt).closest('tr').find('.chkVatCl').find('input');


                var rdoNbt204 = $(elmt).closest('tr').find('.rdo204').find('input');
                var rdoNbt2 = $(elmt).closest('tr').find('.rdo2').find('input');


                if ($(chkNbt).prop('checked') == true) {
                    if ($(rdoNbt204).prop('checked') == true) {
                        //nbt = parseFloat((subTot * 2) / 98);
                        var t = $(rdoNbt204).parent().find("label").text();
                        nbt = parseFloat(t.substring(0, t.length - 1)) / 100;
                        nbt = parseFloat(nbt * subTot);
                    }
                    else {
                        //nbt = parseFloat((subTot * 2) / 100);
                        var t = $(rdoNbt2).parent().find("label").text();
                        nbt = parseFloat(t.substring(0, t.length - 1)) / 100;
                        nbt = parseFloat(nbt * subTot);
                    }

                }

                if ($(chkVat).prop('checked') == true) {

                    // vat = parseFloat((subTot + nbt) * 0.15);
                    var vatRate = parseFloat($(chkVat).parent().attr('VatRate'))
                    vat = parseFloat((subTot + nbt) * vatRate);
                }

                netTot = subTot + nbt + vat;



                $(elmt).closest('tr').find('.txtSubTotalCl').val(subTot.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));
                $(elmt).closest('tr').find('.txtNbtCl').val(nbt.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));
                $(elmt).closest('tr').find('.txtVatCl').val(vat.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));
                $(elmt).closest('tr').find('.txtNetTotalCl').val(netTot.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));

                var tableRows = $(elmt).closest('tbody').find('> tr:not(:has(>td>table))');

                var globSubTotal = 0;
                var globTotalNbt = 0;
                var globTotalVat = 0;
                var globNetTotal = 0;

                for (i = 1; i < tableRows.length; i++) {
                    if ($(tableRows[i]).find('.txtSubTotalCl').val() != '') {
                        var txtSubTClx = $(tableRows[i]).find('.txtSubTotalCl').val().replace(/,/g, '');
                        globSubTotal = globSubTotal + parseFloat(txtSubTClx);
                    }
                    if ($(tableRows[i]).find('.txtNbtCl').val() != '') {
                        var txtNbTCLx = $(tableRows[i]).find('.txtNbtCl').val().replace(/,/g, '');
                        globTotalNbt = globTotalNbt + parseFloat(txtNbTCLx);
                    }
                    if ($(tableRows[i]).find('.txtVatCl').val() != '') {
                        var txtVatClx = $(tableRows[i]).find('.txtVatCl').val().replace(/,/g, '');
                        globTotalVat = globTotalVat + parseFloat(txtVatClx);
                    }
                    if ($(tableRows[i]).find('.txtNetTotalCl').val() != '') {
                        var txtNetTotalClx = $(tableRows[i]).find('.txtNetTotalCl').val().replace(/,/g, '');
                        globNetTotal = globNetTotal + parseFloat(txtNetTotalClx);
                    }
                }

                $('#lblSubTotal').html(globSubTotal.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));
                $('#lblNbtTotal').html(globTotalNbt.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));
                $('#lblVatTotal').html(globTotalVat.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));
                $('#lblNetTotal').html(globNetTotal.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));


                $('#ContentSection_hdnSubTotal').val(globSubTotal.toFixed(2));
                $('#ContentSection_hdnNbtTotal').val(globTotalNbt.toFixed(2));
                $('#ContentSection_hdnVatTotal').val(globTotalVat.toFixed(2));
                $('#ContentSection_hdnNetTotal').val(globNetTotal.toFixed(2));
            }

            function calculate2(elmt) {
                subTot = $(elmt).closest('tr').find('td').find('.txtSubTotalCl').val().replace(/,/g, '');
                nbt = $(elmt).closest('tr').find('td').find('.txtNbtCl').val().replace(/,/g, '');
                vat = $(elmt).closest('tr').find('td').find('.txtVatCl').val().replace(/,/g, '');
                netTot = Number(subTot) + Number(nbt) + Number(vat);
                $(elmt).closest('tr').find('td').find('.txtNbtCl').val(nbt.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));
                $(elmt).closest('tr').find('td').find('.txtVatCl').val(vat.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));
                $(elmt).closest('tr').find('td').find('.txtNetTotalCl').val(netTot.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));

                var tableRows = $(elmt).closest('tbody').find('> tr:not(:has(>td>table))');

                var globSubTotal = 0;
                var globTotalNbt = 0;
                var globTotalVat = 0;
                var globNetTotal = 0;

                for (i = 1; i < tableRows.length; i++) {
                    if ($(tableRows[i]).find('.txtNbtCl').val() != '') {
                        var txtNbTCLx = $(tableRows[i]).find('.txtNbtCl').val().replace(/,/g, '');
                        globTotalNbt = globTotalNbt + parseFloat(txtNbTCLx);
                    }
                    if ($(tableRows[i]).find('.txtVatCl').val() != '') {
                        var txtVatClx = $(tableRows[i]).find('.txtVatCl').val().replace(/,/g, '');
                        globTotalVat = globTotalVat + parseFloat(txtVatClx);
                    }
                    if ($(tableRows[i]).find('.txtNetTotalCl').val() != '') {
                        var txtNetTotalClx = $(tableRows[i]).find('.txtNetTotalCl').val().replace(/,/g, '');
                        globNetTotal = globNetTotal + parseFloat(txtNetTotalClx);
                    }
                }


                $('#lblNbtTotal').html(globTotalNbt.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));
                $('#lblVatTotal').html(globTotalVat.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));
                $('#lblNetTotal').html(globNetTotal.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));

                $('#ContentSection_hdnNbtTotal').val(globTotalNbt.toFixed(2));
                $('#ContentSection_hdnVatTotal').val(globTotalVat.toFixed(2));
                $('#ContentSection_hdnNetTotal').val(globNetTotal.toFixed(2));
            }

            $(".validity").change(function () {

                if (this.value) {
                    $(this).closest('tr').find('.validity').attr('data-date', moment($(this).closest('tr').find('.validity').val(), 'YYYY-MM-DD').format($(this).closest('tr').find('.validity').attr('data-date-format')));
                } else {
                    $(this).attr('data-date', '');
                }
            });
            //duty & pal calcullation 

            $('.cltxtCif').change(function (ev) {

                var CIF = $(this).closest('tr').find('.cltxtCif').val();
                var term = $(this).closest('tr').find('.ddlterms').val();
                if (CIF == "") {
                    CIF = "0";
                    $(this).closest('tr').find('.cltxtCif').val(0);
                }

                if (term == "CNF" || term == "CFR") {

                    $('#ContentSection_gvBidItems tr').eq(1).find('td').eq(20).find('input').val(CIF * 1.01);


                    // if ($(this).closest('tr').find('.txtUnitPriceCl').attr('disabled') == 'disabled') {
                    var cif = $(this).closest('tr').find('.cltxtCif').val();
                    var exchnge = $(this).closest('tr').find('.txtExhchangeRateCl').val();
                    var duty = $(this).closest('tr').find('.DutypalCal').val();
                    var clearing = $(this).closest('tr').find('.ClearingCal').val();
                    var othercal = $(this).closest('tr').find('.othercal').val();
                    $(this).closest('tr').find('.txtUnitPriceCl').val((parseFloat(cif.replace(/,/g, '')) * parseFloat(exchnge.replace(/,/g, ''))) + parseFloat(clearing.replace(/,/g, '')) + parseFloat(duty.replace(/,/g, '')) + parseFloat(othercal.replace(/,/g, '')));
                    calculate2(this);
                    calculate(this);
                    // }
                }
                else if (term == "CIF") {
                    $('#ContentSection_gvBidItems tr').eq(1).find('td').eq(20).find('input').val(CIF);

                    // if ($(this).closest('tr').find('.txtUnitPriceCl').attr('disabled') == 'disabled') {
                    var cif = $(this).closest('tr').find('.cltxtCif').val();
                    var exchnge = $(this).closest('tr').find('.txtExhchangeRateCl').val();
                    var duty = $(this).closest('tr').find('.DutypalCal').val();
                    var clearing = $(this).closest('tr').find('.ClearingCal').val();
                    var othercal = $(this).closest('tr').find('.othercal').val();
                    $(this).closest('tr').find('.txtUnitPriceCl').val((parseFloat(cif.replace(/,/g, '')) * parseFloat(exchnge.replace(/,/g, ''))) + parseFloat(clearing.replace(/,/g, '')) + parseFloat(duty.replace(/,/g, '')) + parseFloat(othercal.replace(/,/g, '')));
                    calculate2(this);
                    calculate(this);
                    //  }
                }
                else if (term == "0") {
                    swal({ type: 'error', title: 'ERROR', text: 'Select A Term' });
                }
                else {
                }
                ev.preventDefault();
            });


            $('.DutypalCal').click(function (ev) {

                $('#ItemValue').val($(this).closest('tr').find('.cltxtCif').val());
                $('#Term').val($(this).closest('tr').find('.ddlterms').val());
                $('#Exchng').val($(this).closest('tr').find('.txtExhchangeRateCl').val());

                if (parseFloat($('#ItemValue').val()) != 0) {
                    $("#totalcif").text("CIF (LKR) :" + parseFloat($('#ItemValue').val().replace(/,/g, '') * parseFloat($('#Exchng').val().replace(/,/g, ''))).toLocaleString(undefined, {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }));
                    $('#mdlcaluculation').modal('show');
                }
                else {
                    swal({ type: 'error', title: 'ERROR', text: 'CIF is Zero' });
                }
                ev.preventDefault();
            });

            $('#btnadd').click(function (ev) {
                var value = parseFloat($('#txtrate').val().replace(/,/g, '')) * parseFloat($('#ItemValue').val().replace(/,/g, '')) * parseFloat($('#Exchng').val().replace(/,/g, ''));
                $('#ContentSection_gvBidItems tr').eq(1).find('td').eq(21).find('input').val(value.toLocaleString(undefined, {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                }));

                $('#mdlcaluculation').modal('hide');

                //   if ($(this).closest('tr').find('.txtUnitPriceCl').attr('disabled') == 'disabled') {

                $('.DutypalCal').trigger("change");
                //  }
            });
            $('.DutypalCal').change(function (ev) {
                var cif = $(this).closest('tr').find('.cltxtCif').val();
                var exchnge = $(this).closest('tr').find('.txtExhchangeRateCl').val();
                var duty = $(this).closest('tr').find('.DutypalCal').val();
                var clearing = $(this).closest('tr').find('.ClearingCal').val();
                var othercal = $(this).closest('tr').find('.othercal').val();
                $(this).closest('tr').find('.txtUnitPriceCl').val((parseFloat(cif.replace(/,/g, '')) * parseFloat(exchnge.replace(/,/g, ''))) + parseFloat(clearing.replace(/,/g, '')) + parseFloat(duty.replace(/,/g, '')) + parseFloat(othercal.replace(/,/g, '')));
                calculate2(this);
                calculate(this);
            });

            $('#btnclose').click(function (ev) {
                $('#mdlcaluculation').modal('hide');
            });



            $('.ClearingCal').keyup(function (ev) {
                var clearing = $(this).closest('tr').find('.ClearingCal').val();
                //  if ($(this).closest('tr').find('.txtUnitPriceCl').attr('disabled') == 'disabled') {

                if (clearing == "") {
                    clearing = "0";
                    $(this).closest('tr').find('.ClearingCal').val(0);
                }
                var cif = $(this).closest('tr').find('.cltxtCif').val();
                var exchnge = $(this).closest('tr').find('.txtExhchangeRateCl').val();
                var duty = $(this).closest('tr').find('.DutypalCal').val();

                var othercal = $(this).closest('tr').find('.othercal').val();
                $(this).closest('tr').find('.txtUnitPriceCl').val((parseFloat(cif.replace(/,/g, '')) * parseFloat(exchnge.replace(/,/g, ''))) + parseFloat(clearing.replace(/,/g, '')) + parseFloat(duty.replace(/,/g, '')) + parseFloat(othercal.replace(/,/g, '')));
                calculate2(this);
                calculate(this);
                //} else {
                // do that
                // }


            });

            $('.othercal').keyup(function (ev) {
                var othercal = $(this).closest('tr').find('.othercal').val();
                var cif = $(this).closest('tr').find('.cltxtCif').val();
                var exchnge = $(this).closest('tr').find('.txtExhchangeRateCl').val();
                var duty = $(this).closest('tr').find('.DutypalCal').val();
                var clearing = $(this).closest('tr').find('.ClearingCal').val();
                //if ($(this).closest('tr').find('.txtUnitPriceCl').attr('disabled') == 'disabled') {
                if (othercal == "") {
                    othercal = "0";
                    $(this).closest('tr').find('.othercal').val(0);
                }
                var unitprise = $(this).closest('tr').find('.txtUnitPriceCl').val();
                if (unitprise == "") {
                    unitprise = "0";
                }
                $(this).closest('tr').find('.txtUnitPriceCl').val(parseFloat(othercal.replace(/,/g, '')) + (parseFloat(cif.replace(/,/g, '')) * parseFloat(exchnge.replace(/,/g, ''))) + parseFloat(duty.replace(/,/g, '')) + parseFloat(clearing.replace(/,/g, '')));
                calculate2(this);
                calculate(this);
                //} else {
                // do that
                //  }
            });
        });

        // $('.date1').unbind();
        $(".date1").datepicker({
            format: 'MM/DD/YYYY',
            changeYear: true
        });

        function redirectPage() {
            window.location.href = location.protocol + "//" + location.host + "/CompanyCreateSupplier.aspx"
        }

        $("[id*=ddlCurrency]").change(function () {
            //debugger; //alert();
            if ($(this).val() == "1") {
                $(this).closest("tr").find("input.txtExhchangeRateCl").val("1");

            } else {
                $(this).closest("tr").find("input.txtExhchangeRateCl").val("");
            }
        });

        function OnEditClientclick() {
            var datetime = $('#ContentSection_gvBidItems tr').eq(1).find('td').eq(26).find('input').val();

            $('#ContentSection_gvBidItems tr').eq(1).find('td').eq(27).find('input').attr('data-date', moment($('#ContentSection_gvBidItems tr').eq(1).find('td').eq(27).find('input').val(), 'YYYY-MM-DD').format($('#ContentSection_gvBidItems tr').eq(1).find('td').eq(27).find('input').attr('data-date-format')));
        }

        function showLoadingImage(obj) {
            $(".loadingImage").removeClass("hidden")
        }

        function showAddTermCondition() {

            //debugger;
            if (validation()) {
                $('#TermCondition').modal('show');
                if ($("#ContentSection_hndImport").val() == "1") {
                    $('#ContentSection_gvBidItems tr').eq(1).find('td').eq(27).find('input').attr('data-date', moment($('#ContentSection_gvBidItems tr').eq(1).find('td').eq(27).find('input').val(), 'YYYY-MM-DD').format($('#ContentSection_gvBidItems tr').eq(1).find('td').eq(27).find('input').attr('data-date-format')));
                }
                var text = $("#ContentSection_txtTermsAndConditions").val();
                if (text != "") {
                    var res = text.split(":")
                    var txtAvailability = res[1].split(".")[0].trim()
                    var txtDelivery = res[2].split(".")[0].trim();
                    var txtCredit = res[3].split(".")[0].trim();
                    var txtOthers = res[4].split(".")[0].trim();
                    $("input#txtAvailability").val(txtAvailability);
                    $("input#txtDelivery").val(txtDelivery);
                    $("input#txtCredit").val(txtCredit);
                    $("textarea#txtOthers").val(txtOthers);
                }
            }
        }

        function addTerm(obj) {
            var txtAvailability = $("input#txtAvailability").val();
            var txtDelivery = $("input#txtDelivery").val();
            var txtCredit = $("input#txtCredit").val();
            var txtOthers = $("textarea#txtOthers").val();

            var text = "Availability : " + txtAvailability + ". \r\n" +
                "Delivery : " + txtDelivery + ". \r\n" +
                "Credit : " + txtCredit + ". \r\n" +
                "Others : " + txtOthers + ".";
            $("#ContentSection_txtTermsAndConditions").val(text);
            $('.modal-backdrop').remove();
            $('#TermCondition').modal('hide');
            $(window).scrollTop(0);
            if ($("#ContentSection_hndImport").val() == "1") {
                $('#ContentSection_gvBidItems tr').eq(1).find('td').eq(27).find('input').attr('data-date', moment($('#ContentSection_gvBidItems tr').eq(1).find('td').eq(27).find('input').val(), 'YYYY-MM-DD').format($('#ContentSection_gvBidItems tr').eq(1).find('td').eq(27).find('input').attr('data-date-format')));
            }
        }

        function validation() {
            if ($('#ContentSection_ddlSuppliers').prop('selectedIndex') == 0) {
                swal({ type: 'error', title: 'ERROR', text: 'Please Select A Supplier' });
                event.preventDefault();
                return false;
            }
            else if ($('#ContentSection_hndImport').val() == 1) {
                if ($('#ContentSection_ddlAgent').prop('selectedIndex') == 0) {
                    swal({ type: 'error', title: 'ERROR', text: 'Please Select A Agent' });
                    event.preventDefault();
                    return false;
                }
                if ($('#ContentSection_ddlCountry').prop('selectedIndex') == 0) {
                    swal({ type: 'error', title: 'ERROR', text: 'Please Select A Country' });
                    event.preventDefault();
                    return false;
                }
            }
            return true;
        }

        function fnScroll() {
            $("#ContentSection_gvBidItems").scrollTop(500);
        }
    </script>

</asp:Content>
