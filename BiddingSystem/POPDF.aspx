<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POPDF.aspx.cs" Inherits="BiddingSystem.POPDF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>| Bidding Portal</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.7 -->
    <link href="AdminResources/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome -->
    <link href="AdminResources/fonts/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <link href="AdminResources/fonts/font-awesome/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <%--<link rel="stylesheet" type="text/css" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css">--%>

    <link rel="stylesheet" type="text/css" href="AdminResources/font-awesome/css/all.css" />
    <link rel="stylesheet" type="text/css" href="AdminResources/fonts/font-awesome/fontawesome.com.css" />

    <!-- Ionicons -->
    <link href="AdminResources/css/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="AdminResources/css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <!-- jvectormap -->
    <link href="AdminResources/css/jquery-jvectormap.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->

    <link href="AdminResources/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->

    <link href="AdminResources/css/_all-skins.min.css" rel="stylesheet" type="text/css" />

    <%--<script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.29.2/dist/sweetalert2.all.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/promise-polyfill"></script>--%>

    <script src="AdminResources/js/SweetAlert.js"></script>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

    <!-- Google Font -->
    <%--<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
    <link href='https://fonts.googleapis.com/css?family=Passion+One' rel='stylesheet' type='text/css'>
    <link href='https://fonts.googleapis.com/css?family=Oxygen' rel='stylesheet' type='text/css'>--%>

    <link rel="stylesheet" href="AdminResources/fonts/googleapis1.css">
    <link href='AdminResources/fonts/googleapis2.css' rel='stylesheet' type='text/css'>
    <link href='AdminResources/fonts/googleapis3.css' rel='stylesheet' type='text/css'>

</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
           <div class="col-md-12">
            <div class="row">
                                 <div class="row">
                                <div class="col-xs-6">
                                <img src="AdminResources/images/logo.png" align="right" />
                                
                                 
                               
                                    </div>
                                <div class="col-xs-6 ">
                                    <%--<strong>COMPANY: </strong>--%>
                                    <b><asp:Label  runat="server" ID="lblCompName" Font-Size="Medium"></asp:Label></b><br>
                                    <b><asp:Label  runat="server" ID="lblcompAdd" Font-Size="Medium"></asp:Label></b><br>
                                    <strong>TP: </strong>
                                    <asp:Label runat="server" ID="lblTpNo"></asp:Label><br>
                                    <strong>FAX: </strong>
                                    <asp:Label runat="server" ID="lblFax"></asp:Label><br>
                                     <strong>VAT: </strong>
                                    <asp:Label runat="server" ID="lblCompVatNo"></asp:Label><br>
                                    </div>
                                </div>
                                <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Purchase Order</h3> 
                                 <%--<asp:Panel runat="server" ID="pnlLogo" Visible="false">
                                 <img src="AdminResources/images/ImportLogo1.png" class="center-block" height="80" width="120" /><br>
                               </asp:Panel>--%>
                                <hr>
                                </div>

             <div class="row">
                                <div class="col-md-4 col-xs-4">
                                    <strong>SUPPLIER: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblSupName"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblSupplierAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblSupplierContact"></asp:Label>
                                </div>

                                <%--<div class="col-md-3 col-xs-3">
                                    
                                    <strong>COMPANY: </strong>
                                    <asp:Label runat="server" ID="lblCompName"></asp:Label><br>
                                    <strong>VAT NO: </strong>
                                    <asp:Label runat="server" ID="lblCompVatNo"></asp:Label><br>
                                    <strong>TELEPHONE: </strong>
                                    <asp:Label runat="server" ID="lblTpNo"></asp:Label><br>
                                    <strong>FAX: </strong>
                                    <asp:Label runat="server" ID="lblFax"></asp:Label><br>
                                    <%--<strong>STORE KEEPER: </strong>
                                    <asp:Label runat="server" ID="lblSK"></asp:Label><br>--%>
                                <%--</div>--%>
                                <div class="col-md-4 col-xs-4">
                                    <strong>DELIVERING WAREHOUSE: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label>
                                </div>
                                <div class="col-md-4 col-xs-4">
                                     <strong>DATE: </strong>
                                    <asp:Label runat="server" ID="lblDate"></asp:Label><br>
                                    <strong>PO CODE: </strong>
                                    <asp:Label runat="server" ID="lblPO"></asp:Label><br>
                                    <strong>BASED PR: </strong>
                                    <asp:Label runat="server" ID="lblPrCode"></asp:Label><br>
                                    <strong>QUOTATION FOR: </strong>
                                    <asp:Label runat="server" ID="lblQuotationFor"></asp:Label><br>
                                    <%--<strong>APPROVAL STATUS: </strong>
                                    <asp:Label runat="server" ID="lblPending" CssClass="label label-warning" Visible="false" Text="Pending"></asp:Label>
                                    <asp:Label runat="server" ID="lblApproved" CssClass="label label-success" Visible="false" Text="Approved"></asp:Label>
                                    <asp:Label runat="server" ID="lblRejected" CssClass="label label-danger" Visible="false" Text="Rejected"></asp:Label><br>
                                    <strong>PO TYPE: </strong>
                                    <asp:Label runat="server" ID="lblGeneral" CssClass="label label-success" Visible="false" Text="General PO"></asp:Label>
                                    <asp:Label runat="server" ID="lblCovering" CssClass="label label-info" Visible="false" Text="Covering PO"></asp:Label>
                                    <asp:Label runat="server" ID="lblModified" CssClass="label label-warning" Visible="false" Text="Modified PO"></asp:Label><br>--%>
                                    <asp:Panel runat="server" ID="pnlPaymentMethod" Visible="false">
                                        <strong>PAYMENT METHOD: </strong>
                                        <asp:Label runat="server" ID="lblPaymentType"></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlReason" Visible="false">
                                        <strong>REMARKS: </strong>
                                        <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                                    </asp:Panel>
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
                                    
                                </div>
                            </div>

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
                                                <asp:BoundField DataField="ItemName" HeaderText="Default Item Name" />
                                                <asp:BoundField DataField="SupplierMentionedItemName" HeaderText="Supplier mentioned Item Name" NullDisplayText="Not Found"/>
                                                
                                                <asp:BoundField DataField="TermName" HeaderText="Price Term" />
                                                <asp:BoundField DataField="MeasurementName" HeaderText="Unit" NullDisplayText="Not Found"/>
                                                <asp:BoundField DataField="Quantity" HeaderText="Requested QTY" DataFormatString="{0:N2}"/>
                                                 <asp:BoundField DataField="UnitPriceForeign" HeaderText="Quoted Unit Price(Foreign)"
                                                            ItemStyle-Font-Bold="true" />
                                                 <asp:BoundField DataField="UnitPriceLocal" HeaderText="Quoted Unit Price(Local)"
                                                            ItemStyle-Font-Bold="true" />
                                                <%--<asp:BoundField DataField="ReceivedQty" HeaderText="Recieved QTY" DataFormatString="{0:N2}"/>
                                                <asp:BoundField DataField="WaitingQty" HeaderText="Waiting QTY" DataFormatString="{0:N2}"/>
                                                <asp:BoundField DataField="PendingQty" HeaderText="Pending QTY" DataFormatString="{0:N2}"/>
                                                --%>
                                                <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="NbtAmount" HeaderText="NBT" DataFormatString="{0:N2}"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="VatAmount" HeaderText="VAT" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="TotalAmount" HeaderText="NetTotal" DataFormatString="{0:N2}" />
                                                
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        <div class="row">
                            <div class="col-md-8 col-xs-8">
                                 <asp:Panel ID="PanenImports" runat="server" Visible="false">
                                     <%--<div class="col-md-2">
                                    <img src="AdminResources/images/ImportLogo1.png" class="left-block" height="100" width="100" />
                                     </div>--%>
                                      <%--<asp:Panel ID="pnlLogo" runat="server" Visible="false">--%>
                            <%--<img src="AdminResources/images/ImportLogo1.png" class="left-block" height="120" width="140" /><br>--%>
                                 <%--</asp:Panel>--%>
                                      
                                    <%-- <strong>PRICE TERMS: </strong>
                                    <asp:Label runat="server" ID="lblPriceTerms"></asp:Label><br>--%>
                                     <asp:Panel runat="server" ID="pnlLogo" Visible="false">
                                         <label>Shipping Mark : </label> 
                                 <img src="AdminResources/images/ImportLogo1.png"  height="80" width="120" /><br>
                               </asp:Panel>
                                    <strong>CURRENCY: </strong>
                                    <asp:Label runat="server" ID="lblCurrency"></asp:Label><br>
                                    <strong>PAYMENT MODE: </strong>
                                    <asp:Label runat="server" ID="lblPaymentMode"></asp:Label><br>
                            </asp:Panel>

                                </div>

                                <div class="col-md-4 col-xs-4">
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
            
                <hr />
                         <div class="row">
                                <div class="col-xs-4 col-sm-4 text-center">
                                    <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                    <%--<asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />--%>
                                    <asp:Label runat="server" ID="lblCreatedByDesignation"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                    <b>PO Created By</b>
                                    <hr style="padding-left:10px; padding-right:10px;" />
                                </div>
                                <asp:Panel ID="pnlParentApprovedByDetails" runat="server" Visible="false">
                                    <div class="col-xs-4 col-sm-4 text-center">
                                        <asp:Image ID="imgParentApprovedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                        <%--<asp:Label runat="server" ID="lblParentApprovedByName"></asp:Label><br />--%>
                                         <asp:Label runat="server" ID="lblParentApprovedByDesignation"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblParentApprovedDate"></asp:Label><br />
                                        <b id="lblParentApprovalText" runat="server"></b>
                                        <hr style="padding-left:10px; padding-right:10px;" />
                                        <strong>REMARKS</strong><br />
                                        <asp:Label runat="server" ID="lblParentApprovalRemarks" CssClass="text-left" style="padding-left:10px;"></asp:Label>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlApprovedBy" runat="server" Visible="false">
                                    <div class="col-xs-4 col-sm-4 text-center">
                                        <asp:Image ID="imgApprovedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                        <%--<asp:Label runat="server" ID="lblApprovedByName"></asp:Label><br />--%>
                                        <asp:Label runat="server" ID="lblApprovedByDesignation"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblApprovedDate"></asp:Label><br />
                                        <b id="lblApprovalText" runat="server"></b>
                                        <hr style="padding-left:10px; padding-right:10px;" />
                                        <strong>REMARKS</strong><br />
                                        <asp:Label runat="server" ID="lblApprovalRemarks" CssClass="text-left" style="padding-left:10px;"></asp:Label>
                                    </div>
                                </asp:Panel>

                        </div>

               <asp:Panel ID="pnlConditions" runat="server" >
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
                    </div>
    </form>
</body>
</html>
