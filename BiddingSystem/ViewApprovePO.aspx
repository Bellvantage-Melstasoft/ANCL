<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ViewApprovePO.aspx.cs" Inherits="BiddingSystem.ViewApprovePO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
        .margin {
            margin-top: 10px;
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

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>


                <div id="mdlPrevInvoices" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
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
                                                        <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" DataFormatString="{0:dd-MMMM-yyyy}" />
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
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInvNo" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator2" ForeColor="Red">* Please Fil this Field</asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtInvNo" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDate" runat="server" Text="Invoice Date" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator1" ForeColor="Red">* Please Fil this Field</asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtDate" type="date" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblAmount" runat="server" Text="Invoice Amount" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAmount" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator3" ForeColor="Red">* Please Fil this Field</asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtAmount" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVatNo" runat="server" Text="VAT Number" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtVatNo" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator4" ForeColor="Red">* Please Fil this Field</asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtVatNo" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblPayment" runat="server" Text="Is Payment Settled" Style="color: black"></asp:Label>
                                                                    <asp:CheckBox ID="ChkPayment" runat="server" CssClass="form-control" />
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary pull-right margin" Text="Add" OnClick="btnAdd_Click" />
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
                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
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
                                        <strong>PR Purchase Type : </strong>
                                        <asp:Label ID="lblPurchaseType" runat="server" Text=""></asp:Label><br />
                                        <asp:Panel runat="server" ID="pnlPaymentMethod" Visible="false">
                                            <strong>PAYMENT METHOD: </strong>
                                            <asp:Label runat="server" ID="lblPaymentMethod"></asp:Label>
                                        </asp:Panel>


                                        <strong>PO Purchase Type : </strong>
                                        <asp:Label ID="lblPoPurchaseType" runat="server" Text=""></asp:Label><br />
                                     <strong>Agent Name : </strong>
                                        <asp:Label ID="lblAgentName" runat="server" Text=""></asp:Label><br />
                                        <asp:Panel ID="panelParentPr" runat="server" Visible ="false">
                                             <strong>Parent PR : </strong>
                                                <asp:Label ID="lblParentPr" runat="server" Text=""></asp:Label><br />
                                                  </asp:Panel>
                                       <%-- <asp:Panel runat="server" ID="pnlReason" Visible="false">
                                            <strong>REMARKS: </strong>
                                            <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                                        </asp:Panel>--%>
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
                                          <asp:Panel ID="pnlSelectPaymentMethod" runat="server" Visible="false">
                                               <label>Payment Method</label>
                                              <asp:RequiredFieldValidator ValidationGroup="btnApprove" runat="server" ID="req1" ControlToValidate="ddlPaymentMethod" InitialValue="" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                              <asp:DropDownList runat="server" ID="ddlPaymentMethod" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged ="ddlPaymentMethod_SelectedIndexChanged" >
                                                 <%--<asp:ListItem Value="0">None</asp:ListItem>--%>
                                                   <asp:ListItem Value="">Select Payment method</asp:ListItem>
                                                 <asp:ListItem Value="1">Cash</asp:ListItem>
                                                 <asp:ListItem Value="2">Cheque</asp:ListItem>
                                                 <asp:ListItem Value="3">Credit</asp:ListItem>
                                                  <asp:ListItem Value="4">Advanced Payment</asp:ListItem>
                                                  <asp:ListItem Value="5">None</asp:ListItem>
                                                  <%--<asp:ListItem Value="4">Advanced Payment</asp:ListItem>--%>
                                              </asp:DropDownList>
                                          </asp:Panel>
                                      </div>
                                           <div class="row">
                                          <asp:Panel ID="pnlSelectPaymentMethodForeign" runat="server" Visible="false">
                                               <label>Payment Method</label>
                                              <asp:RequiredFieldValidator ValidationGroup="btnApprove" runat="server" ID="RequiredFieldValidator5" ControlToValidate="ddlPaymentMethodForeign" InitialValue="" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                              <asp:DropDownList runat="server" ID="ddlPaymentMethodForeign" CssClass="form-control"  >
                                                 
                                              </asp:DropDownList>
                                          </asp:Panel>
                                      </div>
                                          <br>
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
                                        <asp:TextBox TextMode="MultiLine" Rows="6" runat="server" ID="txtRemarks" Width="100%" ></asp:TextBox>
                                    </div>
                                    </asp:Panel>
                                          </div>
                                </div>

                                <hr />
                                <div class="row">
                                    <div class="col-xs-4 col-sm-4 text-center">
                                        <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                        <%--<asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />--%>
                                        <asp:Label runat="server" ID="lblCreatedByDesignation"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                        <b>PO Created By</b>
                                    </div>
                                    <asp:Panel ID="pnlParentApprovedByDetails" runat="server" Visible="false">
                                        <div class="col-xs-4 col-sm-4 text-center">
                                            <asp:Image ID="imgParentApprovedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                            <%--<asp:Label runat="server" ID="lblParentApprovedByName"></asp:Label><br />--%>
                                            <asp:Label runat="server" ID="lblParentApprovedByDesignation"></asp:Label><br />
                                            <asp:Label runat="server" ID="lblParentApprovedDate"></asp:Label><br />
                                            <b id="lblParentApprovalText" runat="server"></b>
                                        </div>
                                        <div class="col-xs-4 col-sm-4">
                                            <div style="width: 100px; height:50px;"></div>
                                            <strong style="margin-top:50px;">REMARKS: </strong>
                                            <asp:Label runat="server" ID="lblParentApprovalRemarks"></asp:Label>
                                        </div>
                                    </asp:Panel>
                                </div>

                            </div>
                        <div class="box-footer">
                            <asp:Button runat="server" CssClass="btn btn-warning btnApproveCl" Text="Approve PO"/>
                            <asp:Button runat="server" CssClass="btn btn-danger btnRejectCl" Text="Reject PO" />
                            <%--<asp:Button runat="server" CssClass="btn btn-info " Text="Add Invoice Details" id="btnInvoice" Visible="false" OnClick="btnInvoice_Click"/>--%>
                        </div>
                    </div>
                    

                    <asp:Panel ID="pnlDerivedFrom" runat="server" Visible="false">
                        <div class="box box-info">
                            <div class="box-header with-border">
                              <h3 class="box-title">Derrived From Purchase Orders (Parent)</h3>
                              <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                              </div>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                              <div class="row">
                                <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvDerivedFrom" EmptyDataText="No Records Found" GridLines="None" CssClass="table table-responsive"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="PoID"  HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                            <asp:BoundField DataField="POCode"  HeaderText="PO Code" />
                                            <asp:BoundField DataField="CreatedDate"  HeaderText="Created Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}"/>
                                            <asp:BoundField DataField="CreatedByName"  HeaderText="Created By"/>
                                            <asp:TemplateField HeaderText="Approval Status">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                                        Text="Pending" CssClass="label label-warning"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                                        Text="APPROVED" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "2" ? true : false %>'
                                                        Text="Rejected" CssClass="label label-danger"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ApprovedDate"  HeaderText="Approval Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}" NullDisplayText="Not Found"/>
                                            <asp:BoundField DataField="ApprovedByName"  HeaderText="Approved By" NullDisplayText="Not Found"/>
                                            <asp:TemplateField HeaderText="PO Type">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsDerived").ToString() == "0" ? true : false %>'
                                                        Text="General PO" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsDerived").ToString() == "1" && Eval("IsDerivedType").ToString() == "1" ? true : false %>'
                                                        Text="Modified PO" CssClass="label label-warning"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsDerived").ToString() == "1" && Eval("IsDerivedType").ToString() == "2" ? true : false %>'
                                                        Text="Covering PO" CssClass="label label-info"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contains Derived POs">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("WasDerived").ToString() == "0" ? true : false %>'
                                                        Text="No" CssClass="label label-danger"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("WasDerived").ToString() == "1" ? true : false %>'
                                                        Text="Yes" CssClass="label label-info"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtnViewPO" Text="View" OnClick="lbtnViewPO_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                </div>         
                              </div>
         
                            </div>
                            <!-- /.box-body -->
                          </div>
                    </asp:Panel>
                    

                    <asp:Panel ID="pnlDerrivedPOs" runat="server" Visible="false">
                        <div class="box box-info">
                            <div class="box-header with-border">
                              <h3 class="box-title" >Derrived Purchase Orders (Child)</h3>
                              <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                              </div>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                              <div class="row">
                                <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvDerivedPOs" EmptyDataText="No Records Found" GridLines="None" CssClass="table table-responsive"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="PoID"  HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                            <asp:BoundField DataField="POCode"  HeaderText="PO Code" />
                                            <asp:BoundField DataField="CreatedDate"  HeaderText="Created Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}"/>
                                            <asp:BoundField DataField="CreatedByName"  HeaderText="Created By"/>
                                            <asp:TemplateField HeaderText="Approval Status">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                                        Text="Pending" CssClass="label label-warning"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                                        Text="APPROVED" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "2" ? true : false %>'
                                                        Text="Rejected" CssClass="label label-danger"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ApprovedDate"  HeaderText="Approval Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}" NullDisplayText="Not Found"/>
                                            <asp:BoundField DataField="ApprovedByName"  HeaderText="Approved By" NullDisplayText="Not Found"/>
                                            <asp:TemplateField HeaderText="PO Type">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsDerived").ToString() == "0" ? true : false %>'
                                                        Text="General PO" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsDerived").ToString() == "1" && Eval("IsDerivedType").ToString() == "1" ? true : false %>'
                                                        Text="Modified PO" CssClass="label label-warning"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsDerived").ToString() == "1" && Eval("IsDerivedType").ToString() == "2" ? true : false %>'
                                                        Text="Covering PO" CssClass="label label-info"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contains Derived POs">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("WasDerived").ToString() == "0" ? true : false %>'
                                                        Text="No" CssClass="label label-danger"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("WasDerived").ToString() == "1" ? true : false %>'
                                                        Text="Yes" CssClass="label label-info"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtnViewPO" Text="View" OnClick="lbtnViewPO_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                </div>         
                              </div>
         
                            </div>
                            <!-- /.box-body -->
                          </div>
                    </asp:Panel>
                    

                    <asp:Panel ID="pnlGeneratedGRNs" runat="server" Visible="false">
                        <div class="box box-info">
                            <div class="box-header with-border">
                              <h3 class="box-title" >Generated Good Received Notes</h3>
                              <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                              </div>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                              <div class="row">
                                <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvGRNs" EmptyDataText="No records Found" GridLines="None" CssClass="table table-responsive"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="GrnId"  HeaderText="GrnId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                            <asp:BoundField DataField="GrnCode"  HeaderText="GRN Code" />
                                            <asp:BoundField DataField="PoCode"  HeaderText="PO Code" />
                                            <asp:BoundField DataField="GoodReceivedDate"  HeaderText="Good Received Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                            <asp:BoundField DataField="CreatedDate"  HeaderText="Created Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}"/>
                                            <asp:BoundField DataField="CreatedByName"  HeaderText="Created By"/>
                                            <asp:TemplateField HeaderText="Approval Status">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                                        Text="Pending" CssClass="label label-warning"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                                        Text="APPROVED" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "2" ? true : false %>'
                                                        Text="Rejected" CssClass="label label-danger"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="ApprovedDate"  HeaderText="Approval Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}" NullDisplayText="Not Found"/>
                                            --%>
                                             <asp:TemplateField HeaderText="Approval Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# (DateTime)Eval("ApprovedDate") == DateTime.MinValue ? "Not Found" : Eval("ApprovedDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                              
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                            
                                            
                                            
                                            <asp:BoundField DataField="ApprovedByName"  HeaderText="Approved By" NullDisplayText="Not Found"/>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtnViewGrn" Text="View" OnClick="lbtnViewGrn_Click"
                                                        ></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                </div>         
                              </div>
         
                            </div>
                            <!-- /.box-body -->
                          </div>
                    </asp:Panel>

                    <asp:HiddenField ID="hdnRemarks" runat="server"/>
                    <asp:HiddenField ID="hdnRejectionAction" runat="server"/>
                    <asp:HiddenField ID="hdnPoType" runat="server"/>
                    <asp:Button runat="server" ID="btnApprove" OnClick="btnApprove_Click" CssClass="hidden" />
                    <asp:Button runat="server" ID="btnReject" OnClick="btnReject_Click" CssClass="hidden" />
    </div> 
    </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <script>
        Sys.Application.add_load(function () {

            $(function () {
                $('body').css("overflow", "auto");
                $('body').css("padding-right", "0");
            });


            $(function () {
                $('.btnApproveCl').on({
                    click: function () {
                        event.preventDefault();
                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to approve this PO?</br></br>"
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

                                $('#ContentSection_btnApprove').click();
                            }
                        });
                    }
                });

                $('.btnRejectCl').on({
                    click: function () {
                        event.preventDefault();

                        var popupContent = `Are you sure you want to reject this PO?</br></br>`;

                        if ($('#ContentSection_hdnPoType').val() == "1") {
                            popupContent += `<strong id='wht'>What Next?</strong></br>
                                        <input id='rdoEnd' type='radio' name='whatNext' value='1' checked> End Procurement<br>
                                        <input id='rdoRevert' type='radio' name='whatNext' value='2'> Revert To Previous PO<br></br>`
                        }



                        popupContent += `   <strong id='dd'>Remarks</strong>
                                        <input id='ss' type='text' class ='form-control' required='required'/></br>`;

                        swal.fire({
                            title: 'Are you sure?',
                            html: popupContent,
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

                                    if ($("#rdoEnd:checked").length > 0) {
                                        $('#ContentSection_hdnRejectionAction').val('1');
                                    }

                                    if ($("#rdoRevert:checked").length > 0) {
                                        $('#ContentSection_hdnRejectionAction').val('2');
                                    }
                                }

                            }
                        }
                        ).then((result) => {
                            if (result.value) {

                                $('#ContentSection_btnReject').click();
                            }
                        });
                    }
                });
            })
        });
        function Done() {
            $('#mdlInvDetails').modal('show');

        }
    </script>


</asp:Content>
