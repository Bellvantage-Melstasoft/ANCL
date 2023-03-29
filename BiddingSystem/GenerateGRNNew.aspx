<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="GenerateGRNNew.aspx.cs" EnableViewState="true" Inherits="BiddingSystem.GenerateGRNNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            margin: 0;
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
    </style>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <div class="container-fluid">
        <form runat="server" id="frm1">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="Updatepanel1" runat="server">
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
                                                                        <asp:Label ID="Label1" runat="server" Text="Invoice Date" Style="color: black"></asp:Label>
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
                                                                        <asp:Label ID="Label2" runat="server" Text="Payment Type" Style="color: black"></asp:Label>
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

                                                                        <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Rows="3" Style="color: black" CssClass="form-control"></asp:TextBox>

                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary pull-right margin" Text="Add" OnClick="btnAdd_Click" ValidationGroup="btnAdd" />
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
                                                                        <asp:Label ID="Label3" ForeColor="Black" runat="server" Text="Invoice Images"></asp:Label>

                                                                        <asp:FileUpload ID="fileImages" runat="server" AllowMultiple="true"
                                                                            CssClass="form-control"></asp:FileUpload>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="box-footer">

                                                            <asp:Button ID="btnDone" runat="server" CssClass="btn btn-success pull-right margin right" Text="Done" OnClick="btnDone_Click" />
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
                                                        CssClass="table table-responsive tablegv" EnableViewState="true" EmptyDataText="No Records">
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


                    <div class="box" style="margin-top: 50px;">
                        <div class="box-header">
                            <h3 class="text-center"><i class="fa fa-truck"></i>&nbsp;&nbsp;&nbsp;GENERATE GRN</h3>
                            <hr>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <strong>SUPPLIER: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblsupplierName"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblSupplierAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblSupplierContact"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:Panel ID="pnlwarehouse" runat="server">
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
                                <div class="col-md-4">
                                    <strong>PO CODE: </strong>
                                    <asp:Label runat="server" ID="lblPOCode"></asp:Label><br>
                                    <strong>BASED PR: </strong>
                                    <asp:Label runat="server" ID="lblPrCode"></asp:Label><br>
                                   <%-- <strong>QUOTATION FOR: </strong>
                                    <asp:Label runat="server" ID="lblQuotationFor"></asp:Label><br>--%>
                                    <strong>DATE: </strong>
                                    <asp:Label runat="server" ID="lblDate"></asp:Label><br>
                                    <strong>PAYMENT TYPE: </strong>
                                    <asp:Label runat="server" ID="lblPaymenttype"></asp:Label><br>
                                    <strong>PR PURCHASE TYPE: </strong>
                                    <asp:Label runat="server" ID="lblPurchaseType"></asp:Label><br>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <br />
                                    <br />
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvPoItems" AutoGenerateColumns="false"
                                            CssClass="table table-responsive" HeaderStyle-BackColor="LightGray" BorderColor="LightGray" EnableViewState="true">
                                            <Columns>
                                                <%--<asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="CheckBox1" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" onclick="CheckBoxChecked(this);" />
                                                    </HeaderTemplate>
                                                </asp:TemplateField>--%>
                                                 <asp:TemplateField HeaderText="PO Purchase Type">
                                                    <ItemTemplate>
                                                         <asp:Label runat="server"  Text='<%# Eval("PoPurchaseType").ToString() == "1" ? "Local":"Import" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:BoundField DataField="PodId" HeaderText="POD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                <asp:BoundField DataField="SupplierMentionedItemName" HeaderText="Supplier Mentioned Item Name" NullDisplayText="Not Found" />
                                                <asp:BoundField DataField="MeasurementShortName" HeaderText="Measurement" NullDisplayText="Not Found" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Requested QTY" />
                                                <asp:BoundField DataField="PendingQty" HeaderText="Pending QTY" />
                                                <asp:TemplateField HeaderText="Received QTY">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" Text='<%# Eval("PendingQty") %>'
                                                            type="number" step=".01" min="0" runat="server"
                                                            autocomplete="off" CssClass="txtQuantityCl" Width="80px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Free QTY">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtFreeQuantity" Text='0.00'
                                                            type="number" step=".01" min="0" runat="server"
                                                            autocomplete="off" Width="80px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Expiry Date">
                                                    <ItemTemplate>
                                                        <%--<asp:TextBox runat="server" ID="txtExpiryDate" type="date" CssClass="form-control"
                                                             Visible='<%#Eval("StockMaintainingType").ToString()=="1"?false:true %>'></asp:TextBox>--%>
                                                        <asp:TextBox runat="server" ID="txtExpiryDate" type="date" CssClass="form-control"
                                                            Visible="true">'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ItemPrice" HeaderText="Price" DataFormatString="{0:N2}" />
                                                <asp:TemplateField HeaderText="Sub Total">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSubTotal" Text='<%# Eval("SubTotal") %>'
                                                            type="number" step=".01" min="0" runat="server" Enabled="false"
                                                            autocomplete="off" CssClass="txtSubTotalCl" Width="120px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="HasNbt" HeaderText="Has NBT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="NbtCalculationType" HeaderText="NBT Calculation Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField HeaderText="NBT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNbt" Text='<%# Eval("NbtAmount") %>'
                                                            type="number" step=".01" min="0" runat="server" Enabled="false"
                                                            autocomplete="off" CssClass="txtNbtCl" Width="120px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="HasVat" HeaderText="Has VAT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField HeaderText="VAT">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtVat" Text='<%# Eval("VatAmount") %>'
                                                            type="number" step=".01" min="0" runat="server" Enabled="false"
                                                            autocomplete="off" CssClass="txtVatCl" Width="120px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTotal" Text='<%# Eval("TotalAmount") %>'
                                                            type="number" step=".01" min="0" runat="server" Enabled="false"
                                                            autocomplete="off" CssClass="txtTotalCl" Width="120px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MeasurementId" HeaderText="MeasurementId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                               

                                                <%-- <asp:TemplateField HeaderText="Free QTY">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtFreeQuantity" Text=''
                                                            type="number" step=".01" min="0" runat="server"
                                                            autocomplete="off" Width="80px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Expiry Date">
                                                    <ItemTemplate>
                                                         <asp:TextBox runat="server" ID="txtExpiryDate" type="date" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Received Date</label>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtReceivedDate" ValidationGroup="btnGenerate" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtReceivedDate" type="date" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Supportive Documents</label>
                                        <asp:FileUpload ID="fuDocs" AllowMultiple="true" runat="server" CssClass="form-control" />
                                    </div>

                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRemarks" ValidationGroup="btnGenerate" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        <asp:TextBox TextMode="MultiLine" Rows="5" runat="server" ID="txtRemarks" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4 col-md-push-2">
                                    <p class="lead">SUMMARY</p>
                                    <div class="table-responsive">
                                        <table class="table table-striped">
                                            <tbody>
                                                <tr>
                                                    <td><b>SUBTOTAL</b></td>
                                                    <td id="tdSubTotal" class="text-right" runat="server"></td>
                                                </tr>
                                                <%-- <tr>
                                                    <td><b>NBT</b></td>
                                                    <td id="tdNbt" class="text-right" runat="server"></td>
                                                </tr>--%>
                                                <tr>
                                                    <td><b>VAT</b></td>
                                                    <td id="tdVat" class="text-right" runat="server"></td>
                                                </tr>
                                                <tr>
                                                    <td><b>NETTOTAL</b></td>
                                                    <td id="tdNetTotal" class="text-right" runat="server"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="box-footer">

                            <asp:Button CssClass="btn btn-danger pull-right btnTerminateCl" Style="margin-left: 10px;" runat="server" Text="Terminate" />
                            <asp:Button CssClass="btn btn-warning pull-right btnGenerateGRNCl" Style="margin-left: 10px;" ID="btnGenerateGRN" runat="server" Text="Generate GRN" />
                            <asp:Button runat="server" CssClass="btn btn-info pull-right" Text="Add Invoice Details" ID="btnInvoice" OnClick="btnInvoice_Click" />
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary pull-right" Style="margin-right: 10px;" Text="Edit Invoices" OnClick="btnEdit_Click" />


                        </div>
                    </div>

                    <asp:HiddenField runat="server" ID="hdnSubTotal" />
                    <asp:HiddenField runat="server" ID="hdnNbt" />
                    <asp:HiddenField runat="server" ID="hdnVat" Value="0.00" />
                    <asp:HiddenField runat="server" ID="hdnNetTotal" />
                    <asp:HiddenField runat="server" ID="hdnHasMore" />
                    <asp:HiddenField runat="server" ID="hdnRemarks" />
                    <asp:Button runat="server" ID="btnGenerate" OnClick="btnGenerate_Click" ValidationGroup="btnGenerate" CssClass="hidden" />
                    <asp:Button runat="server" ID="btnTerminate" OnClick="btnTerminate_Click" CssClass="hidden" />

                    <asp:HiddenField ID="hdnVatRate" runat="server" Value="0.00" />
                    <asp:HiddenField ID="hdnNbtRate1" runat="server" Value="0.00" />
                    <asp:HiddenField ID="hdnNbtRate2" runat="server" Value="0.00" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnGenerate" />
                    <asp:PostBackTrigger ControlID="btnDone" />
                </Triggers>
            </asp:UpdatePanel>
        </form>
    </div>

    <script type="text/javascript">

        function CheckBoxChecked(CheckBox) {
            //get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvPoItems.ClientID %>');
            var TargetChildControl = "CheckBox1";

            //get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0) {
                    Inputs[n].checked = CheckBox.checked;
                }
                else {

                }
        }

        function Done() {
            $('div').removeClass('modal-backdrop');
            $('#mdlInvDetails').modal('hide');
        }
        Sys.Application.add_load(function () {
            $(function () {


                $('.txtQuantityCl').on({
                    keyup: function () {
                        calculate(this);
                    },
                    change: function () {
                        calculate(this);
                    }
                })

                function calculate(elmt) {

                    var row = $(elmt).closest('tr').find('td');

                    var qty = 0;

                    if ($(elmt).val() != "") {
                        qty = parseFloat($(elmt).val());
                    }
                    var itemPrice = parseFloat($(row).eq(11).html().replace(',', ''))

                    var subTotal = qty * itemPrice;

                    var nbt = 0;
                    var vat = 0;
                    var vatRate = $('#ContentSection_hdnVatRate').val();
                    var nbtRate1 = $('#ContentSection_hdnNbtRate1').val();
                    var nbtRate2 = $('#ContentSection_hdnNbtRate2').val();

                    if ($(row).eq(13).html() == "1") {
                        if ($(row).eq(14).html() == "1") {
                            // nbt = (subTotal * 2) / 98;
                            nbt = parseFloat((subTotal * nbtRate1));

                        }
                        else {
                            //nbt = (subTotal * 2) / 100;
                            nbt = parseFloat((subTotal * nbtRate2));
                        }
                    }

                    if ($(row).eq(16).html() == "1") {
                        // vat = (subTotal + nbt) * 0.08;
                        //vat = parseFloat((subTotal + nbt) * vatRate);
                        vat = parseFloat((subTotal) * vatRate);
                    }

                    $(row).eq(12).find('.txtSubTotalCl').val(subTotal.toFixed(2));
                    $(row).eq(15).find('.txtNbtCl').val(nbt.toFixed(2));
                    $(row).eq(17).find('.txtVatCl').val(vat.toFixed(2));
                    //$(row).eq(18).find('.txtTotalCl').val((subTotal + nbt + vat).toFixed(2));
                    $(row).eq(18).find('.txtTotalCl').val((subTotal + vat).toFixed(2));

                    var tableRows = $(elmt).closest('tbody').find('tr');

                    var sumSubTotal = 0;
                    var sumNbt = 0;
                    var sumVat = 0;
                    var sumNetTotal = 0;

                    for (var i = 1; i < tableRows.length; i++) {
                        var currentRow = (tableRows).eq(i).find('td');

                        sumSubTotal += parseFloat($(currentRow).eq(12).find('.txtSubTotalCl').val());
                        sumNbt += parseFloat($(currentRow).eq(15).find('.txtNbtCl').val());
                        sumVat += parseFloat($(currentRow).eq(17).find('.txtVatCl').val());
                        sumNetTotal += parseFloat($(currentRow).eq(18).find('.txtTotalCl').val());
                    }

                    $('#ContentSection_tdSubTotal').html(sumSubTotal.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));
                    $('#ContentSection_tdNbt').html(sumNbt.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));
                    $('#ContentSection_tdVat').html(sumVat.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));
                    $('#ContentSection_tdNetTotal').html(sumNetTotal.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));

                    $('#ContentSection_hdnSubTotal').val(sumSubTotal.toFixed(2));
                    $('#ContentSection_hdnNbt').val(sumNbt.toFixed(2));
                    $('#ContentSection_hdnVat').val(sumVat.toFixed(2));
                    $('#ContentSection_hdnNetTotal').val(sumNetTotal.toFixed(2));
                }


                $('.btnGenerateGRNCl').on({
                    click: function () {

                        event.preventDefault();
                        var hasMore = false;

                        var rows = $('#ContentSection_gvPoItems').find('tr');

                        for (var i = 1; i < rows.length; i++) {
                            var row = $(rows).eq(i).find('td');

                            var pendingQty = parseFloat($(row).eq(7).html());
                            var receivedQty = parseFloat($(row).eq(8).find('.txtQuantityCl').val());

                            if (receivedQty > pendingQty) {
                                hasMore = true;
                                break;

                            }
                        }

                        if (hasMore) {
                            swal.fire({
                                title: 'Quantity Exceeding',
                                html: "You have exceeded the requested quantity of one or more items. New PR as Covering PR will be generated for those items. Are you sure you want to continue?",
                                type: 'warning',
                                cancelButtonColor: '#d33',
                                showCancelButton: true,
                                showConfirmButton: true,
                                confirmButtonText: 'Yes',
                                cancelButtonText: 'Discard',
                                allowOutsideClick: false
                            }
                            ).then((result) => {
                                if (result.value) {
                                    $(document).find('.txtSubTotalCl').removeAttr('disabled');
                                    $(document).find('.txtNbtCl').removeAttr('disabled');
                                    $(document).find('.txtVatCl').removeAttr('disabled');
                                    $(document).find('.txtTotalCl').removeAttr('disabled');

                                    $('#ContentSection_hdnHasMore').val('1');
                                    $('#ContentSection_btnGenerate').click();
                                } else if (result.dismiss === Swal.DismissReason.cancel) {
                                    $('#ContentSection_hdnHasMore').val('0');
                                }
                            });
                        } else {
                            swal.fire({
                                title: 'Confirmation',
                                html: "Are you sure you want to continue?",
                                type: 'warning',
                                cancelButtonColor: '#d33',
                                showCancelButton: true,
                                showConfirmButton: true,
                                confirmButtonText: 'Yes',
                                cancelButtonText: 'Discard',
                                allowOutsideClick: false
                            }
                            ).then((result) => {
                                if (result.value) {
                                    $(document).find('.txtSubTotalCl').removeAttr('disabled');
                                    $(document).find('.txtNbtCl').removeAttr('disabled');
                                    $(document).find('.txtVatCl').removeAttr('disabled');
                                    $(document).find('.txtTotalCl').removeAttr('disabled');

                                    $('#ContentSection_hdnHasMore').val('0');
                                    $('#ContentSection_btnGenerate').click();
                                } else if (result.dismiss === Swal.DismissReason.cancel) {
                                    $('#ContentSection_hdnHasMore').val('0');
                                }
                            });
                        }

                    }
                })


                $('.btnTerminateCl').on({
                    click: function () {
                        event.preventDefault();

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to Terminate this Item?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {
                                if ($('#ss').val() == '') {
                                    $('#dd').prop('style', 'color:red');
                                    swal.showValidationError('Remarks Required');
                                    return false;
                                }
                                else {
                                    $('#ContentSection_hdnRemarks').val($('#ss').val());
                                }

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnTerminate').click();
                            }
                        });
                    }
                });
            });
        });
    </script>

</asp:Content>

