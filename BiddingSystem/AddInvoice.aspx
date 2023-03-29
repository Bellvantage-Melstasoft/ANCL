<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddInvoice.aspx.cs" Inherits="BiddingSystem.AddInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
        .margin {
            margin-top: 50px;
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
                color: Black;
            }
    </style>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <section class="content-header">
    <h1>
      View Purchase Order
        <small></small>
      </h1>
      <ol class="breadcrumb">
          
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">View Purchase Order </li>
      </ol>
    </section>
    <br />
    <script src="AdminResources/js/jquery1.8.min.js" type="text/javascript"></script>
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js" type="text/javascript"> </script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <script src="AdminResources/js/moment.min.js" type="text/javascript"></script>


    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>


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
                                                        <%--<asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" DataformatString="{0:dd-MMMM-yyyy}" />--%>
                                                        <asp:TemplateField HeaderText="Invoice Date">
                                                            <ItemTemplate>
                                                                <%--<asp:Label runat="server" ID="lblExpDate" CssClass="lblExpDate" Text='<%# Eval("ExpiryDate", "{0:dd/MM/yyyy}").ToString() %>'></asp:Label>--%>
                                                                <asp:Label runat="server" Text='<%# (DateTime)Eval("InvoiceDate") == DateTime.MinValue ? "Not Found" : Eval("InvoiceDate", "{0:dd MMM yyyy}") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="InvoiceAmount" HeaderText="Invoice Amount" DataFormatString="{0:N2}" />

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
                                                                <%--<asp:Label runat="server" ID="lblExpDate" CssClass="lblExpDate" Text='<%# Eval("ExpiryDate", "{0:dd/MM/yyyy}").ToString() %>'></asp:Label>--%>
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

                <div id="mdlInvDetails" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Invoice Details</h4>
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
                                                                    <asp:TextBox ID="txtInvNo" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDate" runat="server" Text="Invoice Date" Style="color: black"></asp:Label>
                                                                    <asp:TextBox ID="txtDate" type="date" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblAmount" runat="server" Text="Invoice Amount" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAmount" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator2" ForeColor="Red" >*</asp:RequiredFieldValidator>

                                                                    <asp:TextBox ID="txtAmount" runat="server" type="number" Style="color: black" step="any" min ="0" CssClass="form-control"></asp:TextBox>

                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblRemark" runat="server" Text="Remark" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtremark" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator1" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                    <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Rows="3" Style="color: black" CssClass="form-control"></asp:TextBox>

                                                                </div>


                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVatNo" runat="server" Text="VAT Number" Style="color: black"></asp:Label>
                                                                    <asp:TextBox ID="txtVatNo" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblPayment" runat="server" Text="Is Payment Settled" Style="color: black"></asp:Label>
                                                                    <asp:CheckBox ID="ChkPayment" runat="server" CssClass="form-control" />
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblnewDate" runat="server" Text="Date" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNewDate" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator6" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                    <asp:TextBox ID="txtNewDate" type="date" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

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
                                                                            <%--<asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date"   DataFormatString="{0:dd MMMM yyyy}"/>--%>
                                                                            <asp:TemplateField HeaderText="Invoice Date">
                                                                                <ItemTemplate>
                                                                                    <%--<asp:Label runat="server" ID="lblExpDate" CssClass="lblExpDate" Text='<%# Eval("ExpiryDate", "{0:dd/MM/yyyy}").ToString() %>'></asp:Label>--%>
                                                                                    <asp:Label runat="server" Text='<%# (DateTime)Eval("InvoiceDate") == DateTime.MinValue ? "Not Found" : Eval("InvoiceDate", "{0:dd MMM yyyy}") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
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
                                                                            <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                                                            <asp:BoundField DataField="RemarkOn" HeaderText="Remark On" DataFormatString="{0:dd MMM yyyy}" />
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
                                                                        CssClass="form-control"></asp:FileUpload>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="box-footer">
                                                        <asp:Button ID="btnDone" runat="server" CssClass="btn btn-primary pull-right margin right " Text="Done" OnClick="btnDone_Click" OnClientClick="Done()" Visible="false" />
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




                <section class="content" style="padding-top: 0px">
   <div class="content" style="position: relative;background: #fff;overflow:hidden; border: 1px solid #f4f4f4;" id="divPrintPo" runat="server" >    <!-- Main content -->
      
       
       
       
          <div class="box box-info">
                        <%--<div class="box-header">
                            <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Purchase Order</h3>
                            <hr>
                        </div>--%>
                            <div class="box-body">
                                
                                <div class="row">
                                   
                                <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Purchase Order</h3> 
                                 
                                <hr>
                                </div>

                                <div class="row">
                                    
                                    <div class="col-md-4">
                                        <strong>SUPPLIER: </strong>
                                        <br>
                                        <asp:Label runat="server" ID="lblsupplierName"></asp:Label><br>
                                        <asp:Label runat="server" ID="lblSupplierAddress"></asp:Label><br>
                                        <asp:Label runat="server" ID="lblSupplierContact"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <strong>DELIVERING WAREHOUSE: </strong>
                                        <br>
                                        <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                        <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                        <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label>

                                         <br>
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
                                        <strong>PO TYPE: </strong>
                                        <asp:Label runat="server" ID="lblGeneral" CssClass="label label-success" Visible="false" Text="General PO"></asp:Label>
                                        <asp:Label runat="server" ID="lblCovering" CssClass="label label-info" Visible="false" Text="Covering PO"></asp:Label>
                                        <asp:Label runat="server" ID="lblModified" CssClass="label label-warning" Visible="false" Text="Modified PO"></asp:Label><br>
                                        <strong>APPROVAL STATUS: </strong>
                                        <asp:Label runat="server" ID="lblPending" CssClass="label label-warning" Visible="false" Text="Pending"></asp:Label>
                                        <asp:Label runat="server" ID="lblApproved" CssClass="label label-success" Visible="false" Text="Approved"></asp:Label>
                                        <asp:Label runat="server" ID="lblRejected" CssClass="label label-danger" Visible="false" Text="Rejected"></asp:Label><br>
                                        <strong>PR PURCHASE TYPE: </strong>
                                        <asp:Label runat="server" ID="lblPurchaseType"></asp:Label><br>
                                      <%--  <asp:Panel runat="server" ID="pnlReason" Visible="false">
                                            <strong>REMARKS: </strong>
                                            <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                                        </asp:Panel>--%>
                                        <strong>PO Purchase Type : </strong>
                                        <asp:Label ID="lblPoPurchaseType" runat="server" Text=""></asp:Label><br />
                                     <strong>Agent Name : </strong>
                                        <asp:Label ID="lblAgentName" runat="server" Text=""></asp:Label><br />

                                    </div>

                                </div>

                                 <%--<asp:Panel ID="PanenImports" runat="server" Visible="false">
                                 <div class="row">
                                     <div class="col-md-3">
                                    <img src="AdminResources/images/ImportLogo1.png" class="left-block" height="100" width="100" /><br>
                                     
                                     <strong>PRICE TERMS: </strong>
                                    <asp:Label runat="server" ID="lblPriceTerms"></asp:Label><br>
                                    <strong>CURRENCY: </strong>
                                    <asp:Label runat="server" ID="lblCurrency"></asp:Label><br>
                                    <strong>PAYMENT MODE: </strong>
                                    <asp:Label runat="server" ID="lblPaymentMode"></asp:Label><br>
                                </div>
                                     </div>
                            </asp:Panel>--%>

                                <div class="row">
                                    <div class="col-md-12">
                                        <br />
                                        <br />
                                        <div class="table-responsive">
                                            <asp:GridView runat="server" ID="gvPoItems" AutoGenerateColumns="false" OnRowDataBound="gvPOItems_RowDataBound"
                                                CssClass="table table-responsive" HeaderStyle-BackColor="LightGray" BorderColor="LightGray" EnableViewState="true">
                                                <Columns>
                                                    <asp:BoundField DataField="PodId" HeaderText="POD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemName" HeaderText=" Default Item Name" />
                                                    <asp:BoundField DataField="SupplierMentionedItemName" HeaderText=" Supplier mentioned Item Name" />
                                                          
                                                    <%--<asp:TemplateField HeaderText="Supplier mentioned Item Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Text='<%# Eval("SupplierMentionedItemName").ToString() == "" ? "Not Found" : Eval("SupplierMentionedItemName").ToString() %>'/>
                                                                                
                                                                                 </ItemTemplate>
                                                               </asp:TemplateField>--%>
                                                    <asp:BoundField DataField="TermName" HeaderText="Term" />
                                                   <asp:BoundField DataField="MeasurementName" HeaderText="Measurement" NullDisplayText="Not Found" />
                                                    <asp:BoundField DataField="Quantity" HeaderText="QTY" />
                                                    <asp:BoundField DataField="UnitPriceForeign" HeaderText="Quoted Unit Price(Foreign)"
                                                            ItemStyle-Font-Bold="true" />
                                                        <asp:BoundField DataField="UnitPriceLocal" HeaderText="Quoted Unit Price(Local)"
                                                            ItemStyle-Font-Bold="true" />
                                               <%--  <asp:BoundField DataField="ActualPrice" HeaderText="Actual Price" DataFormatString="{0:N2}" />
                                                --%><asp:BoundField DataField="ItemPrice" HeaderText="Quoted Price" DataFormatString="{0:N2}" />
                                                    <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" DataFormatString="{0:N2}" />
                                                    <asp:BoundField DataField="NbtAmount" HeaderText="NBT" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="VatAmount" HeaderText="VAT" DataFormatString="{0:N2}" />
                                                    <asp:BoundField DataField="TotalAmount" HeaderText="NetTotal" DataFormatString="{0:N2}" />
                                                   <asp:TemplateField HeaderText="PO Purchase Type"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                         <asp:Label runat="server"  Text='<%# Eval("PoPurchaseType").ToString() == "1" ? "Local":"Import" %>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SupplierAgentName" HeaderText="Agent Name"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                 <asp:BoundField DataField="SparePartNumber" HeaderText="Spare Part Number"/>
                                                    </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="row">
                                      <div  class="col-md-4">
                                         
                                          <div class="row">
                                              <asp:Panel ID="PanenImports" runat="server" Visible="false">

                                                   <asp:Panel runat="server" ID="pnlLogo" Visible="false">
                                                       <label>Shipping Mark : </label> 
                                                     <img src="AdminResources/images/ImportLogo1.png"  height="80" width="120" />
                                                   </asp:Panel>
                                 
                                     <strong>PRICE TERMS: </strong>
                                    <%--<asp:Label runat="server" ID="lblPriceTerms"></asp:Label><br>
                                    <strong>CURRENCY: </strong>--%>
                                    <asp:Label runat="server" ID="lblCurrency"></asp:Label><br>
                                    <strong>PAYMENT MODE: </strong>
                                    <asp:Label runat="server" ID="lblPaymentMode"></asp:Label><br>
                                        
                            </asp:Panel>
                                           </div>
                                          </div>
                                    <div class="col-md-4 col-md-push-4">
                                        <p class="lead">SUMMARY</p>
                                        <div class="table-responsive">
                                            <table class="table table-striped">
                                                <tbody>
                                                    <tr>
                                                        <td><b>TOTAL</b></td>
                                                        <td id="tdSubTotal" class="text-right" runat="server"></td>
                                                    </tr>
                                                    <%--<tr>
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

                                <div class="row">
                                      <div  class="col-md-8">
                                          <asp:Panel ID="Remarks" runat="server" >
                                    <div class="form-group">
                                        <label>Remarks : </label><br>
                                        <asp:Label TextMode="MultiLine" Rows="6" runat="server" ID="txtRemarks" Width="100%" ></asp:Label>
                                    </div>
                                    </asp:Panel>
                                          </div>
                                </div>

                               <%-- <hr />
                                <div class="row">
                                    <div class="col-xs-4 col-sm-4 text-center">
                                        <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                        <asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                        <b>PO Created By</b>
                                    </div>
                                    <asp:Panel ID="pnlParentApprovedByDetails" runat="server" Visible="false">
                                        <div class="col-xs-4 col-sm-4 text-center">
                                            <asp:Image ID="imgParentApprovedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                            <asp:Label runat="server" ID="lblParentApprovedByName"></asp:Label><br />
                                            <asp:Label runat="server" ID="lblParentApprovedDate"></asp:Label><br />
                                            <b id="lblParentApprovalText" runat="server"></b>
                                        </div>
                                        <div class="col-xs-4 col-sm-4">
                                            <div style="width: 100px; height:50px;"></div>
                                            <strong style="margin-top:50px;">REMARKS: </strong>
                                            <asp:Label runat="server" ID="lblParentApprovalRemarks"></asp:Label>
                                        </div>
                                    </asp:Panel>
                                </div>--%>

                            </div>
                        <div class="box-footer">
                             <asp:Button runat="server" CssClass="btn btn-info " Text="Add Invoice Details" id="btnInvoice"  OnClick="btnInvoice_Click"/>
                            <asp:Button id="btnEdit" runat="server" CssClass="btn btn-primary " Text="Edit Invoices" OnClick="btnEdit_Click" />
                        
                        </div>
                    </div>
                    

                    <asp:HiddenField ID="hdnRemarks" runat="server"/>
                    <asp:HiddenField ID="hdnRejectionAction" runat="server"/>
                    <asp:HiddenField ID="hdnPoType" runat="server"/>
                    <%--<asp:Button runat="server" ID="btnApprove" OnClick="btnApprove_Click" CssClass="hidden" />
                    <asp:Button runat="server" ID="btnReject" OnClick="btnReject_Click" CssClass="hidden" />--%>
    </div> 
    </section>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnDone" />

            </Triggers>
        </asp:UpdatePanel>
    </form>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />

    <script>
        Sys.Application.add_load(function () {

            $(function () {
                $('body').css("overflow", "auto");
                $('body').css("padding-right", "0");
            });


            $(function () {
                //$('.btnApproveCl').on({
                //    click: function () {
                //        event.preventDefault();
                //        swal.fire({
                //            title: 'Are you sure?',
                //            html: "Are you sure you want to approve this PO?</br></br>"
                //                + "<strong id='dd'>Remarks</strong>"
                //                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                //            type: 'warning',
                //            cancelButtonColor: '#d33',
                //            showCancelButton: true,
                //            showConfirmButton: true,
                //            confirmButtonText: 'Yes',
                //            cancelButtonText: 'No',
                //            allowOutsideClick: false,
                //            preConfirm: function () {
                //                if ($('#ss').val() == '') {
                //                    $('#dd').prop('style', 'color:red');
                //                    swal.showValidationError('Remarks Required');
                //                    return false;
                //                }
                //                else {
                //                    $('#ContentSection_hdnRemarks').val($('#ss').val());
                //                }

                //            }
                //        }
                //        ).then((result) => {
                //            if (result.value) {

                //                $('#ContentSection_btnApprove').click();
                //            }
                //        });
                //    }
                //});

                //$('.btnRejectCl').on({
                //    click: function () {
                //        event.preventDefault();

                //        var popupContent = `Are you sure you want to reject this PO?</br></br>`;

                //        if ($('#ContentSection_hdnPoType').val() == "1") {
                //            popupContent += `<strong id='wht'>What Next?</strong></br>
                //                            <input id='rdoEnd' type='radio' name='whatNext' value='1' checked> End Procurement<br>
                //                            <input id='rdoRevert' type='radio' name='whatNext' value='2'> Revert To Previous PO<br></br>`
                //        }



                //        popupContent += `   <strong id='dd'>Remarks</strong>
                //                            <input id='ss' type='text' class ='form-control' required='required'/></br>`;

                //        swal.fire({
                //            title: 'Are you sure?',
                //            html: popupContent,
                //            type: 'warning',
                //            cancelButtonColor: '#d33',
                //            showCancelButton: true,
                //            showConfirmButton: true,
                //            confirmButtonText: 'Yes',
                //            cancelButtonText: 'No',
                //            allowOutsideClick: false,
                //            preConfirm: function () {
                //                if ($('#ss').val() == '') {
                //                    $('#dd').prop('style', 'color:red');
                //                    swal.showValidationError('Remarks Required');
                //                    return false;
                //                }
                //                else {
                //                    $('#ContentSection_hdnRemarks').val($('#ss').val());

                //                    if ($("#rdoEnd:checked").length > 0) {
                //                        $('#ContentSection_hdnRejectionAction').val('1');
                //                    }

                //                    if ($("#rdoRevert:checked").length > 0) {
                //                        $('#ContentSection_hdnRejectionAction').val('2');
                //                    }
                //                }

                //            }
                //        }
                //        ).then((result) => {
                //            if (result.value) {

                //                $('#ContentSection_btnReject').click();
                //            }
                //        });
                //    }
                //});
            })
        });
        function Done() {
            $('#mdlInvDetails').modal('show');

        }
    </script>


</asp:Content>
