<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="false" CodeBehind="AgeAnalysisReport.aspx.cs" Inherits="BiddingSystem.AgeAnalysisReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <html>
    <head>

        <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
        <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
        <script src="AdminResources/js/moment.min.js"></script>

        <style type="text/css">
            #myModal.modal-dialog {
                width: 90%;
            }

            table {
                color: black;
            }

            body {
                color: black;
                page-break-inside: auto !important;
            }

            #divPrintPoReport #tr {
                page-break-after: auto !important;
                page-break-inside: avoid !important;
            }

            #divPrintPoReport #table {
                page-break-after: auto !important;
                page-break-inside: avoid !important;
                background-color: aquamarine;
            }

            #hiddenPrint {
                visibility: hidden;
            }

            .Calander {
                position: relative;
                color: white;
            }

                .Calander:before {
                    position: absolute;
                    content: attr(data-date);
                    display: inline-block;
                    color: black;
                }

                .Calander::-webkit-datetime-edit, .Calander::-webkit-inner-spin-button, .Calander::-webkit-clear-button {
                    display: none;
                }

                .Calander::-webkit-calendar-picker-indicator {
                    position: absolute;
                    /*top: 3px;*/
                    right: 5px;
                    color: #555;
                    opacity: 1;
                    font-size: 9px;
                }
        </style>
    </head>
    <body>

        <section class="content-header">
            <h1>Age Analysis Report 
        <small></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li class="active">Age Analysis Report</li>
            </ol>
        </section>

        <%-- Next Filter Row --%>
        <form runat="server">
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
                    <section class="content">
                        <div class="box box-info" id="panelPurchaseRequset" runat="server">
                            <div class="box-header with-border">

                                <%--first Filter Row--%>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <label>PO Code</label>
                                            <div class="input-group margin">
                                                <asp:TextBox ID="txtPoCode" runat="server" CssClass="form-control" PlaceHolder="PO1"></asp:TextBox>
                                                <%--   <span class="input-group-btn">
                                            <asp:Button runat="server" ID="btnPoCodeSearch" ValidationGroup="btnPoCodeSearch" OnClick="btnPoCodeSearch_Click" CssClass="btn btn-info" Text="Search" />

                                        </span>--%>
                                            </div>
                                        </div>



                                        <div class="col-sm-4">
                                            <label>Date</label>
                                            <div class="input-group margin">
                                                <asp:TextBox ID="txtStartDate" type="date" Width="50%" Text="Start Date" runat="server" data-date="" data-date-format="DD MMMM YYYY" CssClass="form-control" placeholder="from"></asp:TextBox>

                                                <asp:TextBox ID="txtEndDate" type="date" Width="50%" Text="End Date" runat="server" data-date="" data-date-format="DD MMMM YYYY" CssClass="form-control" placeholder="to"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--2nd Filter row--%>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <label>
                                                Sub Department
                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlSubdep" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label>Supplier</label>

                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlsupplier" runat="server" class="form-control">
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label>Purchasing Officer</label>
                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlPurchasingOfficer" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--Button search--%>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="Search" OnClick="btnSearch_Click" />
                                            <asp:Button runat="server" ID="btnSearchAll" CssClass="btn btn-primary" Text="Get All" OnClick="btnSearchAll_Click" />
                                            <button runat="server" id="btnRun" onserverclick="btnRun_ServerClick" class="btn btn-success" title="Export To Excel">
                                                <i class="fa fa-file-export" style="margin-right: 10px"></i>Export To Excel
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-body with-border">
                                <div class="row">
                                    <div class="col-md-12" style="color: black; overflow-x: scroll;">
                                        <asp:GridView runat="server" ID="gvAgeAnalysis" EmptyDataText="No Data To Show!"
                                            CssClass="table table-responsive tablegv" AutoGenerateColumns="false"
                                            GridLines="None" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" OnRowDataBound="gvAgeAnalysis_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="POCode" HeaderText="PO Code" />
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
                                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name  " DataFormatString="{0:d}" />
                                                <asp:BoundField DataField="ItemPrice" HeaderText="Item Price" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                <asp:BoundField DataField="ReceivedQuantity" HeaderText="Received Quantity" />
                                                <asp:BoundField DataField="WaitingQuantity" HeaderText="Waiting Quantity" />
                                                <asp:BoundField HeaderText="Waiting Days" />
                                                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                                                <asp:BoundField DataField="PurchasingOfficerId" HeaderText="Purchasing Officer" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </section>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnRun" />
                </Triggers>
            </asp:UpdatePanel>
        </form>
    </body>
</asp:Content>
