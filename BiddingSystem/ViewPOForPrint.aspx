<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ViewPOForPrint.aspx.cs" Inherits="BiddingSystem.ViewPOForPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style type="text/css">
        body {
        }

        .tablegv {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

        @media print {

            @page {
                size: A4;
                margin: 5mm 5mm 5mm 5mm;
            }

            #divPrintPo {
                visibility: visible !important;
            }

            .print-count {
                display: block;
            }

            .noPrint {
                display: none;
            }
        }

        #divPrintPo {
            visibility: visible !important;
        }

        .tablegv td, .tablegv th {
            border: 1px solid #ddd;
            padding: 8px;
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

        table, td, tr {
            border-color: black;
        }

        ul li {
            text-align:justify;
   
        }
        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding : 7px !important;
        }
    </style>
    <%--<div class="row">
        <div class="col-xs-12">
            <img src="AdminResources/images/logo.png" class="center-block" />
          <h2 class="page-header" style="text-align:center;">
            <i class="fa fa-envelope"></i> PURCHASE ORDER (PO)
          </h2>
        </div>
        <!-- /.col -->
      </div>--%>
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
                <section class="content" style="padding-top: 0px">

          


 <%--  <div class="content" style="position: relative;background: #fff;overflow:hidden; border: 1px solid #f4f4f4;" id="divPrintPo" runat="server" >    <!-- Main content -->
 --%>     
       
       
       
           <div class="box box-info"  id="divPrintPo">
                        <%--<div class="box-header">
                            <img src="AdminResources/images/logo.png" class="center-block" />
                            <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Purchase Order</h3>
                            <p class="print-count text-right" runat="server" id="pPrintCount"></p><br />
                            <hr>
                        </div>--%>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-xs-6">
                                <img src="AdminResources/images/logo.png" align="right" />
                                
                                
                               
                                    </div>
                                <div class="col-xs-6 ">
                                    <%--<strong>COMPANY: </strong>--%>
                                    <b><asp:Label  runat="server" ID="lblCompName" Font-Size="Medium"></asp:Label></b><br>
                                    <b><asp:Label  runat="server" ID="lblcompAdd" Font-Size="Medium"></asp:Label></b><br>
                                    <strong>TP: </strong>
                                    <asp:Label runat="server" ID="lblTpNo"></asp:Label>
                                    <strong>FAX: </strong>
                                    <asp:Label runat="server" ID="lblFax"></asp:Label><br>
                                    <strong>VAT: </strong>
                                    <asp:Label runat="server" ID="lblCompVatNo"></asp:Label>
                                    </div>
                                </div>
                            <h5 class="text-center"><b>Purchase Order</b></h5> 
                             <hr>
                            <div class="row">
                                <div class="col-xs-3">
                                    <strong>SUPPLIER: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblSupName"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblSupplierAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblSupplierContact"></asp:Label>
                                </div>

                               <%-- <div class="col-xs-3">
                                    
                                    <strong>COMPANY: </strong>
                                    <asp:Label runat="server" ID="lblCompName"></asp:Label><br>
                                    <strong>VAT NO: </strong>
                                    <asp:Label runat="server" ID="lblCompVatNo"></asp:Label><br>
                                    <strong>TELEPHONE: </strong>
                                    <asp:Label runat="server" ID="lblTpNo"></asp:Label><br>
                                    <strong>FAX: </strong>
                                    <asp:Label runat="server" ID="lblFax"></asp:Label><br>
                                    <%--<strong>STORE KEEPER: </strong>
                                    <asp:Label runat="server" ID="lblSK"></asp:Label><br>
                                </div>--%>
                                <div class="col-xs-3">
                                    <strong>DELIVERING WAREHOUSE: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label>

                                    <br>
                                    <strong>STORE KEEPER: </strong>
                                    <asp:Label runat="server" ID="lblStoreKeeper"></asp:Label>
                                    <br>
                                </div>
                                <div class="col-xs-3">
                                     <strong>DATE: </strong>
                                    <asp:Label runat="server" ID="lblDate"></asp:Label><br>
                                    <strong>PO CODE: </strong>
                                    <asp:Label runat="server" ID="lblPO"></asp:Label><br>
                                    <strong>BASED PR: </strong>
                                    <asp:Label runat="server" ID="lblPrCode"></asp:Label><br>
                                    <%--<strong>QUOTATION FOR: </strong>
                                    <asp:Label runat="server" ID="lblQuotationFor"></asp:Label><br>--%>
                                    <%--<strong>APPROVAL STATUS: </strong>--%>
                                    <%--<asp:Label runat="server" ID="lblPending" CssClass="label label-warning" Visible="false" Text="Pending"></asp:Label>--%>
                                    <%--<asp:Label runat="server" ID="lblApproved" CssClass="label label-success" Visible="false" Text="Approved"></asp:Label>--%>
                                    <%--<asp:Label runat="server" ID="lblRejected" CssClass="label label-danger" Visible="false" Text="Rejected"></asp:Label><br>--%>
                                    <strong>PO TYPE: </strong>
                                    <asp:Label runat="server" ID="lblGeneral" CssClass="label label-success" Visible="false" Text="General PO"></asp:Label>
                                    <asp:Label runat="server" ID="lblCovering" CssClass="label label-info" Visible="false" Text="Covering PO"></asp:Label>
                                    <asp:Label runat="server" ID="lblModified" CssClass="label label-warning" Visible="false" Text="Modified PO"></asp:Label><br>
                                    
                                </div>

                                <div class="col-xs-3">
                                    <asp:Panel runat="server" ID="pnlPaymentMethod" Visible="false">
                                        <strong>PAYMENT METHOD: </strong>
                                        <asp:Label runat="server" ID="lblPaymentType"></asp:Label>
                                    </asp:Panel>
                                   <%-- <asp:Panel runat="server" ID="pnlReason" Visible="false">
                                        <strong>REMARKS: </strong>
                                        <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                                    </asp:Panel>--%>
                                    
                                        <strong>MRN Department: </strong>
                                        <asp:Label runat="server" ID="lblDepartment"></asp:Label><br>
                                     <strong>PR Purchase Type : </strong>
                                        <asp:Label ID="lblPurchaseType" runat="server" Text=""></asp:Label><br />
                                   <strong>PO Purchase Type : </strong>
                                        <asp:Label ID="lblPoPurchaseType" runat="server" Text=""></asp:Label><br />
                                     <strong>Agent Name : </strong>
                                        <asp:Label ID="lblAgentName" runat="server" Text=""></asp:Label><br />
                                     <asp:Panel ID="panelParentPr" runat="server" Visible ="false">
                                             <strong>Parent PR : </strong>
                                                <asp:Label ID="lblParentPr" runat="server" Text=""></asp:Label><br />
                                                  </asp:Panel>
                                   
                                    <br>
                                </div>
                            </div>

                            <div class="row" style="margin-top:3px">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                       <asp:GridView runat="server" ID="gvPoItems" AutoGenerateColumns="false" OnRowDataBound="gvPOItems_RowDataBound"
                                            CssClass="table table-responsive" HeaderStyle-BackColor="LightGray" BorderColor="LightGray" EnableViewState="true">
                                            <Columns>
                                                <asp:BoundField DataField="PodId" HeaderText="POD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Default Item Name" />
                                                <asp:BoundField DataField="SupplierMentionedItemName" HeaderText="Supplier Mentioned Item Name" NullDisplayText="Not Found" />
                                                    <%-- <asp:TemplateField HeaderText="Supplier mentioned Item Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Text='<%# Eval("SupplierMentionedItemName").ToString() == "" ? "Not Found" : Eval("SupplierMentionedItemName").ToString() %>'/>
                                                                                
                                                                                 </ItemTemplate>
                                                               </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                            <ItemTemplate>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "0" ? true : false %>'
                                                                                    Text="Awaiting Receival" CssClass="label label-warning"/>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                                                    Text="Partially Received" CssClass="label label-info"/>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                                                    Text="Fully Received" CssClass="label label-success"/>
                                                                                <asp:LinkButton 
                                                                                    runat="server" ID="btnMrn" 
                                                                                    Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                                                    Text="Terminated" CssClass="label label-danger" ></asp:LinkButton>
                                                                     </ItemTemplate>
                                                               </asp:TemplateField>
                                                <asp:BoundField DataField="TermName" HeaderText="Term" />
                                                <asp:BoundField DataField="MeasurementName" HeaderText="Unit" NullDisplayText="Not Found"/>
                                                <asp:BoundField DataField="Quantity" HeaderText="Requested QTY" />
                                                <asp:BoundField DataField="UnitPriceForeign" HeaderText="Quoted Unit Price(Foreign)"
                                                            ItemStyle-Font-Bold="true" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                        <asp:BoundField DataField="UnitPriceLocal" HeaderText="Quoted Unit Price(Local)"
                                                            ItemStyle-Font-Bold="true" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="ReceivedQty" HeaderText="Recieved QTY" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                 <asp:BoundField DataField="UnitPriceForeign" HeaderText="Quoted Unit Price(Foreign)"
                                                            ItemStyle-Font-Bold="true" HeaderStyle-Width="0px" ItemStyle-Width="0px"  />
                                                        <asp:BoundField DataField="UnitPriceLocal" HeaderText="Quoted Unit Price(Local)"
                                                            ItemStyle-Font-Bold="true" HeaderStyle-Width="0px" ItemStyle-Width="0px" />
                                                <asp:BoundField DataField="WaitingQty" HeaderText="Waiting QTY" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="PendingQty" HeaderText="Pending QTY" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                
                                                <asp:BoundField DataField="ItemPrice" HeaderText="Quoted Price" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="NbtAmount" HeaderText="NBT" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="VatAmount" HeaderText="VAT" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="TotalAmount" HeaderText="NetTotal" DataFormatString="{0:N2}" />
                                                <asp:TemplateField HeaderText="PO Purchase Type">
                                                <ItemTemplate>
                                                     <asp:Label runat="server"  Text='<%# Eval("PoPurchaseType").ToString() == "1" ? "Local":"Import" %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="SupplierAgentName" HeaderText="Agent Name"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                 <asp:BoundField DataField="SparePartNumber" HeaderText="Spare Part Number"/>
                                                <asp:TemplateField HeaderText="Attachments"  HeaderStyle-CssClass="noPrint" ItemStyle-CssClass="noPrint">
                                                <ItemTemplate>
                                                    <asp:Button CssClass="btn btn-xs btn-default"  runat="server"
                                                        Text="View"></asp:Button>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                        <div class="row">
                            <div class="col-xs-8">
                            <asp:Panel ID="PanenImports" runat="server" Visible="false">
                                 <%--<img src="AdminResources/images/ImportLogo1.png" class="left-block" height="80" width="80" /><br>
                                  --%>   
                                     <%--<strong>PRICE TERMS: </strong>
                                    <asp:Label runat="server" ID="lblPriceTerms"></asp:Label><br>--%>
                                 <asp:Panel runat="server" ID="pnlLogo" Visible="false">
                                      <label>Shipping Mark : </label>
                                 <img src="AdminResources/images/ImportLogo1.png"   height="80" width="120" /><br>
                               </asp:Panel>
                                    <strong>CURRENCY: </strong>
                                    <asp:Label runat="server" ID="lblCurrency"></asp:Label><br>
                                    <strong>PAYMENT MODE: </strong>
                                    <asp:Label runat="server" ID="lblPaymentMode"></asp:Label>
                               
                            </asp:Panel>
                                <asp:Panel ID="Remarks" runat="server" >
                                    <div class="form-group">
                                        <label>Remarks : </label>
                                        <asp:Label TextMode="MultiLine" Rows="6" runat="server" ID="txtRemarks" ></asp:Label>
                                    </div>
                                    </asp:Panel>
                     </div>

                                <div class="col-xs-4">
                                    <%--<p class="lead">SUMMARY</p>--%>
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
                                <div class="col-xs-4 col-sm-4 text-center">
                                    <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                    <%--<asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />--%>
                                    <asp:Label runat="server" ID="lblCreatedByDesignation"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                    <b>PO Created By</b>
                                    <hr style="padding-left:10px; padding-right:10px;" />
                                </div>
                                <%--<asp:Panel ID="pnlParentApprovedByDetails" runat="server" Visible="false">
                                    <div class="col-xs-4 col-sm-4 text-center">
                                        <asp:Image ID="imgParentApprovedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                        <asp:Label runat="server" ID="lblParentApprovedByName"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblParentApprovedDate"></asp:Label><br />
                                        <b id="lblParentApprovalText" runat="server"></b>
                                        <hr style="padding-left:10px; padding-right:10px;" />
                                       <%-- <strong>REMARKS</strong><br />
                                        <asp:Label runat="server" ID="lblParentApprovalRemarks" CssClass="text-left" style="padding-left:10px;"></asp:Label>--%>
                                     <%--</div>
                                </asp:Panel>--%>
                                <asp:Panel ID="pnlApprovedBy" runat="server" Visible="false">
                                    <div class="col-xs-4 col-sm-4 text-center">
                                        <asp:Image ID="imgApprovedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                        <%--<asp:Label runat="server" ID="lblApprovedByName"></asp:Label><br />--%>
                                        <asp:Label runat="server" ID="lblApprovedByDesignation"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblApprovedDate"></asp:Label><br />
                                        <b id="lblApprovalText" runat="server"></b>
                                        <hr style="padding-left:10px; padding-right:10px;" />
                                       <%-- <strong>REMARKS</strong><br />
                                        <asp:Label runat="server" ID="lblApprovalRemarks" CssClass="text-left" style="padding-left:10px;"></asp:Label>--%>
                                    </div>
                                </asp:Panel>

                        </div>
                        </div>
                        <div class="box-footer noPrint">
                            <asp:Button runat="server" ID="btnModify" CssClass="btn btn-warning" Text="Edit PO" OnClick="btnModify_Click" />
                            <%--<asp:Button runat="server" ID="btnPrint"  Text="Print PO" CssClass="btn btn-success" OnClick="btnPrint_Click" />
                           --%> <%--<div id="printerDiv"  style="display:none"></div>--%>
                        </div>

                       <%-- <hr>--%>
                        <asp:Panel ID="pnlConditions" runat="server" Visible="false">
                            <div style="border:black 2px" >
                                <h4  style="font-size:15px"> Terms & conditions <h4>


                                    <ul>
	                                    <li style="font-size:12px"><b>Please quote this purchase order no. in all correspondence, invoices & statements.</b></li>
	                                    <li style="font-size:12px">MATERIAL - All goods are to be supplied strictly in accordance with the specification given. No departure from the specification is permitted without our prior agreement in writing.</li>
	                                    <li style="font-size:12px">DELIVERY - The time quoted for completion is the essence of this order. The order is liable to cancellation if delivery is not effected on the specified date.</li>
	                                    <li style="font-size:12px">INSPECTION – We reserve the right to inspect the goods supplied against this order, but such inspection does not relieve the supplier of his responsibility for defects in material and or workmanship and for delivery of the goods in accordance with the specifications given.<b> Goods rejected will be returned to supplier at his own expense.</b></span>
	                                    <li style="font-size:12px">DESTINATION – The supplier will note the destination of the material. Demurrage or other expenses incurred owing to the supplier not complying with our instructions will be for the supplier’s account and will be declared from his invoice before payment.</li>
                                        <li style="font-size:12px">PACKING – Price are to include all packing and boxing. It is assumed that cases are non-chargeable and non-returnable unless we are otherwise informed in writing. Chargeable packing cases will be returned for full credit.</li>
                                    </ul>

       
                            </div>
                        </asp:Panel>
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
                                            <asp:TemplateField HeaderText="Approval Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
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
                                            <asp:TemplateField HeaderText="Approval Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
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
                                            <asp:TemplateField HeaderText="Approval Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
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


                    
       
       
   <%-- </div> --%>
    </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

</asp:Content>
