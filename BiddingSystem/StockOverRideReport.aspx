﻿<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="StockOverRideReport.aspx.cs" Inherits="BiddingSystem.StockOverRideReport" %>

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
    <!-- bootstrap datepicker -->
    <section class="content-header">
        <h1>Stock Overridden Notes
        <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Stock Overridden Notes</li>
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Select Warehouse</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true" ControlToValidate="ddlWarehouse" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlWarehouse" runat="server" CssClass="form-control"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">From</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true" ControlToValidate="dtFrom" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="dtFrom" runat="server" CssClass="form-control date1" autocomplete="off" DataFormatString="{0:dd/MM/yyyy HH:mm}"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">To</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true" ControlToValidate="dtTo" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="dtTo" runat="server" CssClass="form-control date1" autocomplete="off" DataFormatString="{0:dd/MM/yyyy HH:mm}"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Select Category</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true" ControlToValidate="ddlMainCateGory" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlMainCateGory" runat="server" CssClass="form-control"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlMainCateGory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Select SubCategory</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true" ControlToValidate="ddlSubCategory" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Select Item</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" Font-Bold="true" ControlToValidate="ddlItem" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>




                            </div>

                            <div class="col">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnSearch" ValidationGroup="btnSearch" CssClass="btn btn-primary btnSearch " Text="Search" OnClick="btnSearch_Click" />
                                    <img id="loader" alt="" src="UserRersourses/assets/img/loader-info.gif" class="pull-right hidden paddingTop" style="margin-right: 10px; max-height: 30px;" />
                                    <button runat="server" id="btnRun" onserverclick="btnRun_ServerClick" class="btn btn-success" title="Export To Excel">
                                        <i class="fa fa-file-export" style="margin-right: 10px"></i>Export To Excel
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>


                    <asp:Panel runat="server" class="print-count">

                        <table>
                            <tr>
                                <td>Printed Date&nbsp;</td>
                                <td>:&nbsp;</td>
                                <td><b>
                                    <asp:Label ID="lblPrintDate" runat="server"></asp:Label></b></td>
                            </tr>
                            <tr>
                                <td>Warehouse&nbsp;</td>
                                <td>:&nbsp;</td>
                                <td><b>
                                    <asp:Label ID="lblWarehouse" runat="server" Text=""></asp:Label></b></td>
                            </tr>
                            <%--<tr>
                                <td>Departments&nbsp;</td>
                                <td>:&nbsp;</td>
                                <td><b>
                                    <asp:Label ID="lblDepartments" runat="server" Text=""></asp:Label></b></td>
                            </tr>
                            --%>

                            <%--<tr>
                                <td>Date From&nbsp;</td>
                                <td>:&nbsp;</td>
                                <td><b>
                                    <asp:Label ID="lblFrom" runat="server" Text=""></asp:Label></b></td>
                            </tr>
                             <tr>
                                <td>Date To&nbsp;</td>
                                <td>:&nbsp;</td>
                                <td><b>
                                    <asp:Label ID="lblTo" runat="server" Text=""></asp:Label></b></td>
                                    
                            </tr>--%>
                        </table>
                        <div>
                            &nbsp
                        </div>

                    </asp:Panel>

                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Report</h3>
                        </div>
                        <div class="box-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvItems" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" EmptyDataText="No records Found" OnRowDataBound="gvItems_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer; margin-top: -6px;" src="images/plus.png" class='<%# Eval("StockMaintainingType").ToString() =="1"?"hidden":"" %>' />
                                                        <asp:Panel ID="pnlMRNBatchDetails" runat="server" Style="display: none">
                                                            <asp:GridView ID="gvBatchDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No records Found" Caption="Inventory Batches"
                                                                CssClass="ChildGrid" Width="100%">
                                                                <Columns>
                                                                    <%--<asp:BoundField DataField="BatchId" HeaderText="Btach Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>--%>
                                                                    <asp:TemplateField HeaderText="Batch Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblBatchCode"
                                                                                Text='<%# "Batch-"+Eval("BatchCode").ToString() %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Existed Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" Text='<%#Eval("ExistedQty", "{0:N2}").ToString() %>'></asp:Label>
                                                                            <asp:Label runat="server" Text='<%#Eval("ShortCode").ToString() %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="ExistedStockValue" HeaderText="Existed Stock Value" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" />
                                                                    <asp:TemplateField HeaderText="Overridden Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" Text='<%#Eval("OverridingQty", "{0:N2}").ToString() %>'></asp:Label>
                                                                            <asp:Label runat="server" Text='<%#Eval("ShortCode").ToString() %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="OverridingStockValue" HeaderText="Overridden Stock Value" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" />
                                                                    <asp:BoundField DataField="BatchExpiryDate" HeaderText="Batch Expiry date" DataFormatString="{0:dd/MM/yyyy}" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="LogId" HeaderText="Override Log ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                                <asp:BoundField DataField="ItemId" HeaderText="Item ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                                <asp:BoundField DataField="SubCategoryName" HeaderText="Sub Category Name" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                <asp:BoundField DataField="Location" HeaderText="Warehouse" />
                                                <asp:BoundField DataField="MeasurementShortName" HeaderText="Unit" />
                                                <asp:BoundField DataField="ExistedQty" HeaderText="Existed Qty" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="ExistedStockValue" HeaderText="Existed Stock Value" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="OverridingQty" HeaderText="Overriding Qty" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="OverridingStockValue" HeaderText="Overriding Stock Value" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="OverriddenOn" HeaderText="Overriding On" />
                                                <asp:BoundField DataField="UpdatedUser" HeaderText="Overriding By" />
                                                <asp:BoundField DataField="Remark" HeaderText="Remark" />

                                                <asp:TemplateField HeaderText="Over Ridden">
                                                    <ItemTemplate>
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("OverriddingType").ToString() == "1" ? true : false %>'
                                                            Text="Manage Stock" CssClass="label label-info" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("OverriddingType").ToString() == "2" ? true : false %>'
                                                            Text="" CssClass="label label-info" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("OverriddingType").ToString() == "3" ? true : false %>'
                                                            Text="PR Expense Approval" CssClass="label label-success" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 text-right">
                                    <h3 style="display: inline-block;">
                                        <asp:Label ID="lblvalue" runat="server" Text="Total Stock Value" ForeColor="Gray"></asp:Label></h3>
                                    <h3 style="display: inline-block; margin-left: 100px;">
                                        <asp:Label ID="lblSumValue" runat="server" Text="" ForeColor="Gray"></asp:Label></h3>

                                </div>

                            </div>

                        </div>


                        <div class="box-footer no-print">
                            <div>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-success btnprintcl" OnClientClick="printPage()" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="btnRun" />
                </Triggers>
            </asp:UpdatePanel>
        </form>
    </section>

    <script src="AdminResources/js/select2.full.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.js"></script>
    <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />
    <script src="AdminResources/js/daterangepicker.js" type="text/javascript"></script>
    <script type="text/javascript">


        Sys.Application.add_load(function () {
            $(function () {
                $('.select2').select2();
            })
        });


    </script>
    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>
    <script src="AdminResources/js/select2.full.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(function () {

            $(function () {
                $("#<%= dtTo.ClientID %>").datepicker();
                $("#<%= dtFrom.ClientID %>").datepicker();
                $(function () {
                    $('.btnSearch').on({
                        click: function () {
                            $('#loader').removeClass('hidden');
                        }
                    })
                });

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
