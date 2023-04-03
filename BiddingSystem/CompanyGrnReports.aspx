<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyGrnReports.aspx.cs" Inherits="BiddingSystem.CompanyGrnReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <html>
    <head>
        <style>
            .activePhase {
                text-align: center;
                border-radius: 3px;
            }

            .hide {
                display: none;
            }

            .show {
                display: block;
            }
        </style>
        <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
        <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
        <script src="AdminResources/js/datetimepicker/datetimepicker.js"></script>
        <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
        <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
        <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />
    </head>

    <body>

        <section class="content-header">
            <h1>GRN Reports
        <small></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">GRN Reports</li>
            </ol>
        </section>
        <br />

        <form runat="server">

            <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
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
                                                <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive"
                                                    AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
                                                    <Columns>
                                                        <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="QuotationId" HeaderText="Quotation Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="ItemId" HeaderText="Item No" />
                                                        <asp:BoundField DataField="_AddItem.ItemName" HeaderText="Description" />
                                                        <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price" DataFormatString="{0:N2}" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                        <asp:BoundField DataField="VatAmount" HeaderText="Vat Amount" DataFormatString="{0:N2}" />
                                                        <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:N2}" />


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
                                                    <%--<tr>
                <td>NBT Total:</td>
                <td><asp:Label runat="server" ID="lblNbtTotal"></asp:Label></td>
              </tr>--%>
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


            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
                    <section class="content">
                        <div class="box box-info" id="panelPurchaseRequset" runat="server">
                            <div class="box-header with-border">

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <label>GRN Code</label>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPoCode"  ValidationGroup="btnGrnCodeSearch" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                            <div class="input-group margin">
                                                <asp:TextBox ID="txtGrnCode" runat="server" CssClass="form-control" placeholder="Ex: GRN01"></asp:TextBox>
                                                <%--   <span class="input-group-btn">
                                                    <asp:Button runat="server" ID="btnGrnCodeSearch" ValidationGroup="btnPoCodeSearch" OnClick="btnGrnCodeSearch_Click" OnClientClick="onButtonClick()" CssClass="btn btn-info" Text="Search" />

                                                </span>--%>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label>PO Code</label>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPoCode"  ValidationGroup="btnGrnCodeSearch" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                            <div class="input-group margin">
                                                <asp:TextBox ID="txtPOCode" runat="server" CssClass="form-control" placeholder="Ex: PO01"></asp:TextBox>
                                                <%-- <span class="input-group-btn">
                                                    <asp:Button runat="server" ID="btnPOCodeSearch" ValidationGroup="btnPoCodeSearch" OnClick="btnPoCodeSearch_Click" OnClientClick="onButtonClick()" CssClass="btn btn-info" Text="Search" />

                                                </span>--%>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label>Status</label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="" ControlToValidate="ddlStatus" ValidationGroup="btnGrnStatusSearch" ForeColor="Red">*</asp:RequiredFieldValidator>

                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="">-Please Select-</asp:ListItem>
                                                    <asp:ListItem Value="0">Pending</asp:ListItem>
                                                    <asp:ListItem Value="1">Approved</asp:ListItem>
                                                    <asp:ListItem Value="2">Rejected</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- <span class="input-group-btn">
                                                    <asp:Button runat="server" ID="btnGrnStatusSearch" OnClick="btnGrnStatusSearch_Click" OnClientClick="onButtonClick()" ValidationGroup="btnGrnStatusSearch" CssClass="btn btn-info" Text="Search" />
                                                </span>--%>
                                            </div>
                                        </div>

                                        <%--<div class="col-sm-4">  
                          <label>Date</label>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStartDate"  ValidationGroup="btnGrnDateSearch" ForeColor="Red">*</asp:RequiredFieldValidator>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEndDate"  ValidationGroup="btnGrnDateSearch" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <div class="input-group margin">
                        <asp:TextBox ID="txtStartDate" runat="server" Width="50%" CssClass="form-control  date1" placeholder="from" ></asp:TextBox>
                              
                        <asp:TextBox ID="txtEndDate" runat="server" Width="50%"  CssClass="form-control date1"  placeholder="to" ></asp:TextBox>  
                            
                    <span class="input-group-btn">
                         <asp:Button runat="server" ID="btnGrnDateSearch" ValidationGroup="btnGrnDateSearch" OnClick="btnGrnDateSearch_Click" OnClientClick="onButtonClick()" CssClass="btn btn-info"  Text="Search"/>
                    </span>
              </div>
                    </div>--%>
                                    </div>
                                </div>

                                <%-- Next Filter Row --%>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <label>PR Type</label>
                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlPRType" runat="server" class="form-control select2">
                                                    <asp:ListItem Value="">-Please Select-</asp:ListItem>
                                                    <asp:ListItem Value="1">Stock</asp:ListItem>
                                                    <asp:ListItem Value="2">Non-Stock</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <label>Purchase Type</label>

                                            <div class="input-group margin">
                                                <asp:DropDownList ID="ddlPurchasingType" runat="server" class="form-control select2">
                                                    <asp:ListItem Value="">-Please Select-</asp:ListItem>
                                                    <asp:ListItem Value="1">Local</asp:ListItem>
                                                    <asp:ListItem Value="2">Import</asp:ListItem>
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
                                            <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="Search" />
                                            <asp:Button runat="server" ID="btnSearchAll" CssClass="btn btn-primary" Text="Get All" />


                                        </div>
                                    </div>
                                </div>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                                </div>
                            </div>
                            <div class="box-body">


                                <div class="row">
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-6">
                                        <img id="imgMinMaxLoader" class="hide" src="UserRersourses/assets/img/loader-info.gif" style="height: 40px;" />
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView runat="server" ID="gvPurchaseOrder" EmptyDataText="No records Found" GridLines="None" CssClass="table table-responsive"
                                                AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="GrnId" HeaderText="GrnId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="PoID" HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="GrnCode" HeaderText="GRN Code" />
                                                    <asp:BoundField DataField="PoCode" HeaderText="PoCode" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                    <asp:BoundField DataField="GoodReceivedDate" HeaderText="Good Received Date" />
                                                    <asp:BoundField DataField="TotalVat" HeaderText="Vat Amount" DataFormatString="{0:N2}" />
                                                    <asp:BoundField DataField="TotalNbt" HeaderText="NBT Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:N2}" />
                                                    <%-- <asp:TemplateField HeaderText="Approval Status">
                         <ItemTemplate >
                             <asp:Label CssClass="activePhase" runat="server" ID="lblStatus" Text="Pending" BackColor="Gold" Font-Bold="true" ForeColor="White" ></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>--%>

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
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lbtnView" Text="View" OnClick="btnView_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>


                </ContentTemplate>
            </asp:UpdatePanel>
        </form>

    </body>
    </html>
    <script type="text/javascript">
        var dtp01 = new DateTimePicker('.date1', { pickerClass: 'datetimepicker-blue', timePicker: true, timePickerFormat: 12, format: 'Y/m/d h:i', allowEmpty: true });
        function TimeChange() {
            $('#ContentSection_hdnReceivedDate').val($('.date1').val());
            alert($('#ContentSection_hdnReceivedDate').val());
        }

        function onButtonClick() {
            document.getElementById('imgMinMaxLoader').className = "show";
        }
    </script>
</asp:Content>

