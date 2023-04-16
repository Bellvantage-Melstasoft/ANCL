<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="comparisionToLastYear.aspx.cs" Inherits="BiddingSystem.comparisionToLastYear" %>

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
            <h1>Comparision To Last Year Report 
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
                                            <label>Expense Type</label>

                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlPRType" runat="server" class="form-control">
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                        <%-- <div class="col-sm-4">
                                            <label>Status</label>
                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlPurchaseType" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <label>
                                                Purchase Type
                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlPurchasingType" runat="server" class="form-control select2">
                                                    <asp:ListItem Value="">-Please Select-</asp:ListItem>
                                                    <asp:ListItem Value="1">Local</asp:ListItem>
                                                    <asp:ListItem Value="2">Import</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label>PR Type</label>

                                            <div class="input-group margin">
                                                <asp:DropDownList ID="DropDownList2" runat="server" class="form-control">
                                                    <asp:ListItem Value="">-Please Select-</asp:ListItem>
                                                    <asp:ListItem Value="1">Stock</asp:ListItem>
                                                    <asp:ListItem Value="2">Non-Stock</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label>Department Type</label>
                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <%--Button search--%>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <asp:Button runat="server" ID="btnSearchPoTable" CssClass="btn btn-info" Text="Search" OnClick="btnSearchPoTable_Click" />
                                            <%--<asp:Button runat="server" ID="btnSearchAll" CssClass="btn btn-primary" Text="Get All" OnClick="btnSearchAll_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-body with-border">
                                <div class="row">
                                    <div class="col-md-12" style="color: black; overflow-x: scroll;">
                                        <asp:Table ID="tblPOReport" runat="server" CssClass="table table-responsive"></asp:Table>

                                    </div>
                                </div>

                            </div>

                            <%---------------------------------Table Comparison to supplier--------------------------------%>

                            <div class="box-body with-border mt-5">
                                <div class="row">
                                    <div class="col-md-12" style="color: black; overflow-x: scroll;">
                                        <asp:Table ID="tblSupplierReport" runat="server" CssClass="table table-bordered"></asp:Table>

                                    </div>
                                </div>

                            </div>

                            <%---------------------------------End Table Comparison to supplier--------------------------------%>

                            <%---------------------------------Table Comparison to Item--------------------------------%>

                            <div class="box-body with-border mt-5">
                                <div class="row">
                                    <div class="col-md-12" style="color: black; overflow-x: scroll;">
                                        <asp:Table ID="tblItemReport" runat="server" CssClass="table table-bordered"></asp:Table>

                                    </div>
                                </div>

                            </div>

                            <%---------------------------------End Table Comparison to Item--------------------------------%>
                        </div>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </body>
</asp:Content>
