<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ItemPoReport.aspx.cs" Inherits="BiddingSystem.ItemPoReport" %>

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
            <h1>Item PO Reports 
        <small></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li class="active">Item PO Reports</li>
            </ol>
        </section>
        <br />

        <form runat="server">


            <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content ">
                        <div class="modal-header" style="background-color: #a2bdcc;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title">View PO</h4>
                        </div>
                        <div class="modal-body">
                            <div class="login-w3ls">
                                <div style="position: relative; background: #fff; overflow: hidden; border: 1px solid #f4f4f4; padding: 20px; margin: 10px 25px;" id="divPrintPoReport">
                                    <!-- Main content -->


                                    <div class="row">
                                        <div class="col-xs-12">
                                            <h2 class="page-header" style="text-align: center;">
                                                <i class="fa fa-envelope"></i>PURCHASE ORDER (PO)
                                            </h2>
                                        </div>
                                        <!-- /.col -->
                                    </div>


                                    <div class="row invoice-info">

                                        <div class="col-sm-4 invoice-col">
                                            <address>
                                                <table>
                                                    <tr>
                                                        <td>Date&nbsp;</td>
                                                        <td>:&nbsp;</td>
                                                        <td><b>
                                                            <asp:Label ID="lblDateNow" runat="server" Text=""></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td>PO. No&nbsp;</td>
                                                        <td>:&nbsp;</td>
                                                        <td><b>
                                                            <asp:Label ID="lblPOCode" runat="server" Text=""></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Your Ref&nbsp;</td>
                                                        <td>:&nbsp;</td>
                                                        <td><b>
                                                            <asp:Label ID="lblRefNo" runat="server" Text=""></asp:Label></b></td>
                                                    </tr>
                                                </table>

                                            </address>



                                        </div>

                                        <div class="col-sm-4 invoice-col">

                                            <address>
                                                <table>
                                                    <tr>
                                                        <td>Company&nbsp;</td>
                                                        <td>:&nbsp;</td>
                                                        <td><b>
                                                            <asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td>VAT No&nbsp;</td>
                                                        <td>:&nbsp;</td>
                                                        <td><b>
                                                            <asp:Label ID="lblVatNo" runat="server" Text=""></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Telephone&nbsp;</td>
                                                        <td>:&nbsp;</td>
                                                        <td><b>
                                                            <asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Fax&nbsp;</td>
                                                        <td>:&nbsp;</td>
                                                        <td><b>
                                                            <asp:Label ID="lblFaxNo" runat="server" Text=""></asp:Label></b></td>
                                                    </tr>
                                                </table>

                                            </address>
                                        </div>
                                        <!-- /.col -->
                                        <div class="col-sm-4 invoice-col">
                                            <address>


                                                <table>
                                                    <tr>
                                                        <td>Supplier Name&nbsp;</td>
                                                        <td>:&nbsp;</td>
                                                        <td><b>
                                                            <asp:Label ID="lblSupplierName" runat="server" Text=""></asp:Label></b></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Address&nbsp;</td>
                                                        <td>:&nbsp;</td>
                                                        <td><b>
                                                            <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></b></td>
                                                    </tr>

                                                </table>

                                            </address>
                                        </div>

                                    </div>

                                    <div class="panel-body">
                                        <div class="co-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvItemPoReportsss" runat="server" CssClass="table table-responsive"
                                                    AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
                                                    <Columns>
                                                        <asp:BoundField DataField="SupplierId" HeaderText="Supplier Id" />
                                                        <asp:BoundField DataField="TotalAMount" HeaderText="Total Amount" />
                                                        <asp:BoundField DataField="CreatedDate" HeaderText="Date" DataFormatString="{0:d}" />
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
                                                        <%--<asp:BoundField DataField="Description" HeaderText="Description" />
                                                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                        <asp:BoundField DataField="CreatedDate" HeaderText="PO Created Date" DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                        <asp:BoundField DataField="CreatedBy" HeaderText="PO Created By" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-xs-12 col-sm-12">
                                        <div class="col-xs-6 col-sm-6">
                                        </div>
                                        <div class="col-xs-6 col-sm-6">
                                            <p class="lead">Amount Details</p>

                                            <div class="table-responsive">
                                                <table class="table">
                                                    <tr>
                                                        <td style="width: 50%">Subtotal:</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblSubtotal"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Vat Total</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblVatTotal"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>NBT Total:</td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblNbtTotal"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>Total:</b></td>
                                                        <td><b>
                                                            <asp:Label runat="server" ID="lblTotal"></asp:Label></b></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row no-print">
                                        <div class="col-xs-12">
                                            <button class="btn btn-success" onclick="window.print();"><i class="fa fa-print"></i>Print</button>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <section class="content">
                <div class="box box-info" id="panelPurchaseRequset" runat="server">
                    <div class="box-header with-border" id="viewPOSection">


                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    <label>Category</label>
                                    <div class="input-group margin">
                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <%--  <span class="input-group-btn">
                                            <asp:Button runat="server" ID="btnPoStatusSearch" OnClick="btnPoStatusSearch_Click" ValidationGroup="btnPoStatusSearch" CssClass="btn btn-info" Text="Search" />
                                        </span>--%>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <label>Sub Category</label>

                                    <div class="input-group margin">
                                        <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                        <%--  <span class="input-group-btn">
                                            <asp:Button runat="server" ID="btnPoStatusSearch" OnClick="btnPoStatusSearch_Click" ValidationGroup="btnPoStatusSearch" CssClass="btn btn-info" Text="Search" />
                                        </span>--%>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <label>Date</label>
                                    <div class="input-group margin">
                                        <asp:TextBox ID="txtStartDate" type="date" Width="50%" Text="Start Date" runat="server" data-date="" data-date-format="DD MMMM YYYY" CssClass="form-control" placeholder="from"></asp:TextBox>

                                        <asp:TextBox ID="txtEndDate" type="date" Width="50%" Text="End Date" runat="server" data-date="" data-date-format="DD MMMM YYYY" CssClass="form-control" placeholder="to"></asp:TextBox>

                                        <%-- <span class="input-group-btn">
                                            <asp:Button runat="server" ID="btnPoDateSearch" ValidationGroup="btnPoDateSearch" OnClick="btnPoDateSearch_Click" CssClass="btn btn-info" Text="Search" />
                                        </span>--%>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- Next Filter Row --%>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                    <label>Status</label>
                                    <div class="input-group margin">
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control select2">
                                            <asp:ListItem Value="">-Please Select-</asp:ListItem>
                                            <asp:ListItem Value="0">Pending</asp:ListItem>
                                            <asp:ListItem Value="1">Approved</asp:ListItem>
                                            <asp:ListItem Value="2">Rejected</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <label>Supplier</label>

                                    <div class="input-group margin">
                                        <asp:DropDownList ID="ddlSupplier" runat="server" class="form-control select2">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <label>Item</label>
                                    <div class="input-group margin">
                                        <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control">
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

                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>

                    <asp:ScriptManager runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="Updatepanel1" runat="server">
                        <ContentTemplate>

                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView runat="server" ID="gvItemPoReport" EmptyDataText="No Records Found" GridLines="None" CssClass="table table-responsive"
                                                AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:BoundField DataField="SupplierId" HeaderText="Supplier Id" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                    <asp:BoundField DataField="TotalAMount" HeaderText="Total Amount" />
                                                    <asp:BoundField DataField="CreatedDate" HeaderText="Date" DataFormatString="{0:d}" />
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
                                                    <%--<asp:BoundField DataField="Description" HeaderText="Description" />
                                                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                        <asp:BoundField DataField="CreatedDate" HeaderText="PO Created Date" DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                        <asp:BoundField DataField="CreatedBy" HeaderText="PO Created By" />--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </section>


            <div id="hiddenPrint">
                <asp:GridView ID="gvPurchaseOrderItems1" runat="server" CssClass="table table-responsive"
                    AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
                    <Columns>
                        <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="QuotationId" HeaderText="Quotation Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="ItemId" HeaderText="Item No" />
                        <asp:BoundField DataField="_AddItem.ItemName" HeaderText="Description" />
                        <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="VatAmount" HeaderText="Vat Amount" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:N2}" />
                    </Columns>
                </asp:GridView>
            </div>
        </form>
    </body>

    </html>
    <script type="text/javascript">
        $(".Calander").change(function () {
            if (this.value) {
                $(this).attr('data-date', moment($(this).val(), 'YYYY-MM-DD').format($(this).attr('data-date-format')));
            } else {
                $(this).attr('data-date', '');
            }
        });
    </script>
</asp:Content>
