<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="SupplierItemReport.aspx.cs" Inherits="BiddingSystem.SupplierItemReport" %>

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
            <h1>Supplier Item Report 
        <small></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li class="active">Po Reports</li>
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


                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <label>
                                                Category
                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlCategory" runat="server" class="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label>Sub Category</label>

                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlSubCategory" runat="server" class="form-control">
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label>Status</label>
                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="">-Please Select-</asp:ListItem>
                                                    <asp:ListItem Value="0">Pending</asp:ListItem>
                                                    <asp:ListItem Value="1">Approved</asp:ListItem>
                                                    <asp:ListItem Value="2">Rejected</asp:ListItem>

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
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-body with-border">
                                <div class="row">
                                    <div class="col-md-12" style="color: black; overflow-x: scroll;">
                                        <asp:GridView runat="server" ID="gvSupplierItemReport" EmptyDataText="No Data To Show!"
                                            CssClass="table table-responsive tablegv" AutoGenerateColumns="false"
                                            GridLines="None" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                            <Columns>
                                                <asp:BoundField DataField="SupplierId" HeaderText="SUPPLIER ID" />
                                                <asp:BoundField DataField="SupplierName" HeaderText="SUPPLIER NAME " />
                                                <asp:BoundField DataField="ItemName" HeaderText="ITEM NAME" />
                                                <asp:BoundField DataField="CreatedDate" HeaderText="CREATED DATE " DataFormatString="{0:d}" />
                                                <asp:BoundField DataField="CategoryId" HeaderText="CATEGORY ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="SubCategoryId" HeaderText="SUB CATEGORY ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="GrnCode" HeaderText="GRN CODE " />
                                                <asp:TemplateField HeaderText="Approval Status">
                                                    <ItemTemplate>
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                                            Text="Pending" CssClass="label label-warning" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                                            Text="APPROVED" CssClass="label label-success" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("IsApproved").ToString() == "2" ? true : false %>'
                                                            Text="Rejected" CssClass="label label-danger" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </body>
</asp:Content>
