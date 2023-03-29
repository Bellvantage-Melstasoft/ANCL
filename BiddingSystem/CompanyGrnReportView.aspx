<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyGrnReportView.aspx.cs" Inherits="BiddingSystem.CompanyGrnReportView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
        .margin {
            margin-bottom: 10px;
        }

        .centerHeaderText {
            text-align: center;
        }

        .rightHeaderText {
            text-align: right;
        }

        body {
        }


        @media print {
            body {
            }
        }

        .tablegv {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .tablegv td, .tablegv th {
                border: 1px solid #ddd;
                padding: 8px;
                color: black;
            }

            .tablegv tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .tablegv tr:hover {
                background-color: #ddd;
            }

            .tablegv th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #3C8DBC;
                color: white;
            }

        .successMessage {
            color: #1B6B0D;
            font-size: medium;
        }

        .failMessage {
            color: #C81A34;
            font-size: medium;
        }

        /*th{

          background-color:lightgray;
      }*/

        /*@page{
          size:A4;
          margin:0;
          size:portrait;
          -webkit-print-color-adjust:exact !important
      }*/
    </style>

    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <section class="content-header">
    <h1>
       GOOD RECEIVED NOTE
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">View GRN</li>
      </ol>
    </section>
    <br />



    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>

                <div id="mdlInvDetails" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Add Invoice Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">

                                                <div class="box box-info">
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblInvNo" runat="server" Text="Invoice Number" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInvNo" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator2" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                    <asp:TextBox ID="txtInvNo" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDate" runat="server" Text="Invoice Date" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator7" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                    <asp:TextBox ID="txtDate" type="date" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblAmount" runat="server" Text="Invoice Amount" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtremark" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator3" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                    <asp:TextBox ID="txtAmount" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblnewDate" runat="server" Text="Date" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNewDate" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator6" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                    <asp:TextBox ID="txtNewDate" type="date" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVatNo" runat="server" Text="VAT Number" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtVatNo" InitialValue="" ValidationGroup="btnSave" ID="RequiredFieldValidator4" ForeColor="Red">* Please Fil this Field</asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtVatNo" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblPaymentType" runat="server" Text="Payment Type" Style="color: black"></asp:Label>
                                                                    <asp:DropDownList runat="server" ID="ddlPaymentMethod" CssClass="form-control">
                                                                        <%--<asp:ListItem Value="">Select Payment method</asp:ListItem>--%>
                                                                        <asp:ListItem Value="1">Cash</asp:ListItem>
                                                                        <asp:ListItem Value="2">Cheque</asp:ListItem>
                                                                        <asp:ListItem Value="3">Credit</asp:ListItem>
                                                                        <asp:ListItem Value="4">Advanced Payment</asp:ListItem>
                                                                        <asp:ListItem Value="5">None</asp:ListItem>

                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblPayment" runat="server" Text="Is Payment Settled" Style="color: black"></asp:Label>
                                                                    <asp:CheckBox ID="ChkPayment" runat="server" CssClass="form-control" />
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblRemark" runat="server" Text="Remark" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtremark" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator5" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                    <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Rows="3" Style="color: black" CssClass="form-control" ></asp:TextBox>

                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary pull-right margin" Text="Add" OnClick="btnAdd_Click" ValidationGroup="btnAdd"/>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">

                                                                <div class="table-responsive">
                                                                    <asp:GridView runat="server" ID="gvAddedInvDetails" AutoGenerateColumns="false" Visible="false"
                                                                        CssClass="table table-responsive tablegv">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="num" HeaderText="Random Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                                            <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" DataFormatString="{0:dd MMMM yyyy}" />
                                                                            <asp:BoundField DataField="InvoiceAmount" HeaderText="Invoice Amount" DataFormatString="{0:N2}" />
                                                                            <asp:BoundField DataField="VatNo" HeaderText="Vat No" />
                                                                            <asp:BoundField DataField="IsPaymentSettled" HeaderText="Is Payment Settled" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                            <asp:TemplateField HeaderText="Is Payment Settled">
                                                                                <ItemTemplate>
                                                                                    <asp:Label
                                                                                        runat="server"
                                                                                        Text='<%# Eval("IsPaymentSettled").ToString() == "0" ? "No" : "Yes" %>' />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Payment Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label
                                                                                        runat="server"
                                                                                        Text='<%# Eval("PaymentType").ToString() == "1" ? "Cash" : Eval("PaymentType").ToString() == "2" ? "Cheque" :Eval("PaymentType").ToString() == "3" ? "Credit" :Eval("PaymentType").ToString() == "4" ? "Advanced Payment" :Eval("PaymentType").ToString() == "5" ? "None" : "Not Found" %>' />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                                                            <asp:BoundField DataField="RemarkOn" HeaderText="Remark On" DataFormatString="{0:dd MMMM yyyy}" />
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                                <hr>
                                                            <div class="form-group">
                                                                <asp:Label ID="Label1" ForeColor="Black" runat="server" Text="Invoice Images"></asp:Label>
                                                           
                                                            <asp:FileUpload ID="fileImages" runat="server" AllowMultiple="true"
                                                                    CssClass="form-control" ></asp:FileUpload>
                                                        </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="box-footer">

                                                        <asp:Button ID="btnDone" runat="server" CssClass="btn btn-primary pull-right margin right" Text="Done" OnClick="btnDone_Click" OnClientClick="Done()" Visible="false" />
                                                        <asp:Button ID="btnPrevInv" runat="server" CssClass="btn btn-primary pull-right margin right" Text="Previous Invoices" OnClick="btnPrevInv_Click" />
                                                        
                                                         </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="mdlPrevInvoices" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog" style="width: 60%">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Previous Invoice Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView runat="server" ID="gvPrevInvoices" AutoGenerateColumns="false"
                                                    CssClass="table table-responsive tablegv" EnableViewState="true">
                                                    <Columns>
                                                        <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                        <%--<asp:BoundField DataField="InvoiceDate" HeaderText=" Date" DataformatString="{0:dd-MMMM-yyyy}" />--%>
                                                        <asp:TemplateField HeaderText="Invoice Date">
                                                            <ItemTemplate>
                                                                <%--<asp:Label runat="server" ID="lblExpDate" CssClass="lblExpDate" Text='<%# Eval("ExpiryDate", "{0:dd/MM/yyyy}").ToString() %>'></asp:Label>--%>
                                                                <asp:Label runat="server" Text='<%# (DateTime)Eval("InvoiceDate") == DateTime.MinValue ? "Not Found" : Eval("InvoiceDate", "{0:dd MMM yyyy}") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="InvoiceAmount" HeaderText="Invoice Amount" />
                                                        <asp:BoundField DataField="VatNo" HeaderText="Vat No" />
                                                        <asp:TemplateField HeaderText="Payment Type">
                                                            <ItemTemplate>
                                                                <asp:Label
                                                                    runat="server"
                                                                    Text='<%# Eval("PaymentType").ToString() == "1" ? "Cash" : Eval("PaymentType").ToString() == "2" ? "Cheque" :Eval("PaymentType").ToString() == "3" ? "Credit" :Eval("PaymentType").ToString() == "4" ? "Advanced Payment" :Eval("PaymentType").ToString() == "5" ? "None" : "Not Found" %>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Is Payment Settled">
                                                            <ItemTemplate>
                                                                <asp:Label
                                                                    runat="server"
                                                                    Text='<%# Eval("IsPaymentSettled").ToString() == "1" ? "Yes" : "No" %>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                                        <asp:TemplateField HeaderText="Remark On">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# (DateTime)Eval("RemarkOn") == DateTime.MinValue ? "Not Found" : Eval("RemarkOn", "{0:dd MMM yyyy}") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Visible='<%#int.Parse(Eval("IsActive").ToString()) != 1 ? true:false %>' Text="Terminated" CssClass="label label-danger" />
                                                                <asp:Label runat="server" Visible='<%#int.Parse(Eval("IsActive").ToString()) == 1 ? true:false %>' Text="Not Terminated" CssClass="label label-info" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Button runat="server" CssClass="btn btn-info pull-right " Text="Back" ID="btnBack" OnClick="btnBack_Click" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div id="mdlGrnFiles" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 30%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">GRN Files</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <!-- Start : Quotation Table -->
                                        <div style="color: black;">

                                            <asp:GridView ID="gvFiles" runat="server"
                                                CssClass="table gvFiles"
                                                GridLines="None" AutoGenerateColumns="false" ShowHeader="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>

                                                    <asp:BoundField DataField="GrnfId"
                                                        HeaderText="GRNF ID"
                                                        HeaderStyle-CssClass="hidden"
                                                        ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="GrnId"
                                                        HeaderText="GRN ID"
                                                        HeaderStyle-CssClass="hidden"
                                                        ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="FileName"
                                                        HeaderText="File Name" />
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                        <ItemTemplate>
                                                            <a href='<%# Eval("Location").ToString() %>' target="_blank" class="btn btn-xs btn-default btnViewFiles" runat="server"
                                                                style="margin-right: 4px; margin-bottom: 4px;">View</a>
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
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>

                <div class="content" style="position: relative; background: #fff; overflow: hidden; border: 1px solid #f4f4f4; padding: 20px; margin: 10px 25px;" id="divPrintPo">
                    <!-- Main content -->


                    <div class="row">
                        <div class="col-xs-12">
                            <h2 class="page-header" style="text-align: center;">
                                <i class="fa fa-envelope"></i>Good Recieved Note (GRN)
                            </h2>
                        </div>
                        <!-- /.col -->
                    </div>


                    <div class="row invoice-info">

                        <div class="row">
                            <div class="col-xs-4">
                                <strong>SUPPLIER: </strong>
                                <br>
                                <asp:Label runat="server" ID="lblsupplierName"></asp:Label><br>
                                <asp:Label runat="server" ID="lblSupplierAddress"></asp:Label><br>
                                <asp:Label runat="server" ID="lblSupplierContact"></asp:Label>
                            </div>
                            <div class="col-xs-4">
                                <asp:Panel ID="PnlWarehouse" runat="server">
                                <strong>DELIVERING WAREHOUSE: </strong>
                                <br>
                                <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label>

                                <br>
                                    </asp:Panel>
                                <strong>STORE KEEPER: </strong>
                                <asp:Label runat="server" ID="lblStoreKeeper"></asp:Label><br>
                                <br>
                            </div>
                            <div class="col-xs-4">
                                <strong>GRN CODE: </strong>
                                <asp:Label runat="server" ID="lblGrnCode"></asp:Label><br>
                                <strong>PO CODE: </strong>
                                <asp:Label runat="server" ID="lblPOCode"></asp:Label><br>
                                <strong>BASED PR: </strong>
                                <asp:Label runat="server" ID="lblPrCode"></asp:Label><br>
                               <%-- <strong>QUOTATION FOR: </strong>
                                <asp:Label runat="server" ID="lblQuotationfor"></asp:Label><br>--%>
                                <strong>RECEIVED DATE: </strong>
                                <asp:Label runat="server" ID="lblReceiveddate"></asp:Label><br>
                                <strong>PAYMENT TYPE: </strong>
                                <asp:Label runat="server" ID="lblPaymentm"></asp:Label><br>
                                 <strong>PURCHASE TYPE: </strong>
                                    <asp:Label runat="server" ID="lblPurchaseType"></asp:Label><br>
                                <strong>APPROVAL STATUS: </strong>
                                <asp:Label runat="server" ID="lblPending" CssClass="label label-warning" Visible="false" Text="Pending"></asp:Label>
                                <asp:Label runat="server" ID="lblApproved" CssClass="label label-success" Visible="false" Text="Approved"></asp:Label>
                                <asp:Label runat="server" ID="lblRejected" CssClass="label label-danger" Visible="false" Text="Rejected"></asp:Label>


                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="co-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive"
                                        AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
                                        <Columns>
                                            <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <%--  <asp:BoundField DataField="QuotationId" HeaderText="Quotation Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>--%>
                                            <asp:BoundField DataField="ItemId" HeaderText="Item No" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="ReferenceNo" HeaderText="Item Code" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                            <asp:BoundField DataField="SuppliermentionedItemName" HeaderText="Supplier Mentioned Item Name" NullDisplayText="Not Found" />
                                            <asp:BoundField DataField="MeasurementShortName" HeaderText="Measurement" NullDisplayText="Not Found" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerHeaderText" />
                                            <asp:BoundField DataField="FreeQty" HeaderText="Free Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerHeaderText" />
                                            <asp:TemplateField HeaderText="Expiry Date">
                                                <ItemTemplate>
                                                    <%# (DateTime)Eval("ExpiryDate") == DateTime.MinValue ? "Not Defined" : string.Format("{0:MM-dd-yyyy}", (DateTime)Eval("ExpiryDate")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                            <asp:BoundField DataField="SubTotal" HeaderText="Sub Total" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                            <asp:BoundField DataField="VatAmount" HeaderText="Vat Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                            <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />


                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="col-xs-12 col-sm-12">
                            <div class="col-xs-8 col-sm-8">
                                <strong>GRN Note: </strong>
                                <asp:Label runat="server" ID="lblgrnNote"></asp:Label><br>
                            </div>
                            <div class="col-xs-4">
                                <p class="lead">Amount Details</p>

                                <div class="table-responsive">
                                    <table class="table">
                                        <tr>
                                            <td style="width: 50%">Subtotal:</td>
                                            <td class="text-right">
                                                <asp:Label runat="server" ID="lblSubtotal"></asp:Label></td>
                                        </tr>
                                        
                                        <tr>

                                            <td>Vat Total</td>
                                            <td class="text-right">
                                                <asp:Label runat="server" ID="lblVatTotal"></asp:Label></td>
                                        </tr>
                                            
                                        <%-- <tr>
                                <td>NBT Total:</td>
                                <td  class="text-right">
                                    <asp:Label runat="server" ID="lblNbtTotal"></asp:Label></td>
                            </tr>--%>
                                        <tr>
                                            <td><b>Total:</b></td>
                                            <td class="text-right"><b>
                                                <asp:Label runat="server" ID="lblTotal"></asp:Label></b></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>


                            <%--                <div class="row">
                    <table style="width: 50%;">
                        <tr style="width: 100%;">
                            <td>Received Date&nbsp;</td>
                            <td>:&nbsp;</td>
                            <td style="margin: 10px; padding: 10px;">
                                <label id="lblReceiveddate" runat="server" class="form-control"></label>
                            </td>
                        </tr>
                        <tr style="width: 100%;">
                            <td>Remarks&nbsp;</td>
                            <td>:&nbsp;</td>
                            <td style="margin: 10px; padding: 10px;">
                                <label id="lblgrnComment" runat="server" class="form-control"></label>
                            </td>
                        </tr>

                    </table>
                </div>--%>
                        </div>


                        <hr />
                        <div class="row">
                            <div class="col-xs-4 col-sm-4  text-center">

                                <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height: 50px;" runat="server" /><br />
                                <asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />
                                <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                <b>GRN Created By</b>
                            </div>

                            <div class="col-xs-4 col-sm-4  text-center">
                                <asp:Panel ID="pnlApprovedBy" runat="server">
                                    <asp:Image ID="imgApprovedBySignature" Style="width: 100px; height: 50px;" runat="server" /><br />
                                    <asp:Label runat="server" ID="lblApprovedByName"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblApprovedDate"></asp:Label><br />
                                    <b id="grnApprovalText" runat="server"></b>
                                </asp:Panel>
                            </div>


                            <div class="col-xs-4 col-sm-4">
                                <asp:Panel ID="pnlRemark" runat="server">
                                    <div style="height: 50px; width: 100px;"></div>
                                    <strong>Remarks: </strong>
                                    <asp:Label runat="server" ID="lblgrnComment" Text=""></asp:Label>
                                </asp:Panel>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-xs-12 text-center">
                                <hr />
                                <small>This is a computer-generated Goods Received Note</small>

                            </div>
                        </div>

                        <div class="row no-print">
                            <div class="col-xs-12">
                                <button class="btn btn-success" onclick="window.print();"><i class="fa fa-print"></i>Print</button>
                                <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                    ID="btnView" Text="Attachments" OnClick="btnView_Click"
                                    Style="width: 100px; height: 35px"></asp:Button>
                                <asp:Button runat="server" CssClass="btn btn-info " Text="Add Invoice Details" ID="btnInvoice" Visible="false" OnClick="btnInvoice_Click" />
                             <asp:Button id="btnEdit" runat="server" CssClass="btn btn-primary " Text="Edit Invoices" OnClick="btnEdit_Click" />
                                                   
                            </div>

                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnDone" />
               
            </Triggers>
        </asp:UpdatePanel>
    </form>


</asp:Content>
