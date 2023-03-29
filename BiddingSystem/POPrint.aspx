<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POPrint.aspx.cs" Inherits="BiddingSystem.POPrint" %>

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

    <style type="text/css">
        body {
            /*-webkit-print-color-adjust:exact !important;*/
        }

        table {
            page-break-inside: auto !important;
        }

        tr {
            page-break-after: auto !important;
            page-break-inside: avoid !important;
        }

        #ContentSection_gvPurchaseOrderItems table {
            page-break-inside: auto !important;
        }

        #ContentSection_gvPurchaseOrderItems {
            table tr;

        {
            page-break-after: auto !important;
            page-break-inside: avoid !important;
            background-color: aliceblue;
        }

        }


        @media print {

            #ContentSection_gvPurchaseOrderItems table {
                page-break-inside: auto !important;
            }

            #ContentSection_gvPurchaseOrderItems table {
                page-break-inside: auto !important;
            }


            body {
                visibility: hidden;
                margin: unset;
            }

            body {
                overflow: hidden !important;
            }

            .main-footer {
                visibility: hidden !important;
            }

            #divPrintPo {
                visibility: visible !important;
            }

            #divPrintPoReport {
                visibility: visible !important;
            }


            .content-wrapper {
                overflow: hidden !important;
            }

            .wrapper {
                overflow: hidden !important;
            }

            body {
                overflow: hidden !important;
            }

            @page {
                size: A4 portrait;
                max-height: 200px;
                page-break-before: always !important;
                page-break-after: always !important;
            }
        }

        .content-wrapper {
            min-height: 200px;
        }

        .wrapper {
            min-height: 200px;
        }

        .swal2-popup {
            font-size: 1.5rem !important;
        }
    </style>
</head>
<body>
    
     <form runat="server" id="form1">
     <div id="myModal" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
						<h4 class="modal-title">Attachment</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvUploadFiles" runat="server" CssClass="table table-responsive tablegv" style="border-collapse:collapse;color:  black;"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="PrId" HeaderText="PrId" />
            <asp:BoundField DataField="FilePath" HeaderText="FilePath"/>
            <asp:BoundField DataField="FileName" HeaderText="FileName" />
         
              <asp:TemplateField HeaderText="View">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnEdit" ImageUrl="~/images/view-icon-614x460.png"  style="width:39px;height:26px"
                          runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnCancelRequest" ImageUrl="~/images/Downloads2.png"  style="width:26px;height:20px;margin-top:4px;"
                          runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>               
    </div> 
     <div>
          <label id="lbMailMessage"  style="margin:3px; color:maroon; text-align:center;"></label>
     </div>
                </div>	
			</div>
		  </div>
		</div>
	  </div>
     </div>
         <div id="printDiv">
   <div class="content" style="position: relative;background: #fff;overflow:hidden; padding: 20px;margin: 10px 25px;"  id="divPrintPo" >    <!-- Main content -->
       <div >

           <div class="row">
               <div class="col-xs-12">
                   <h2 class="page-header" style="text-align: center;">
                       <i class="fa fa-envelope"></i>PURCHASE ORDER (PO)
                   </h2>
               </div>
               <!-- /.col -->
           </div>


           <div class="row">
               <div class="col-sm-4">

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
               <div class="col-sm-4">

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
               <div class="col-sm-4">
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
                       <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive table-bordered"
                           AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
                           <Columns>
                               <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                               <asp:BoundField DataField="ItemId" HeaderText="Item No" />
                               <asp:BoundField DataField="_AddItem.ItemName" HeaderText="Description" />
                               <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                               <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                               <asp:BoundField DataField="CustomizedAmount" HeaderText="CustomizedAmount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                               <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="txtApproved" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedAmount","{0:F2}") : Eval("ItemPrice","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                               <asp:BoundField DataField="VatAmount" HeaderText="Vat Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                               <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                               <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                               <asp:BoundField DataField="CustomizedVat" HeaderText="Vat Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                               <asp:BoundField DataField="CustomizedNbt" HeaderText="NBT Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                               <asp:BoundField DataField="CustomizedAmount" HeaderText="Total Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                               <asp:BoundField DataField="CustomizedTotalAmount" HeaderText="CustomizedTotalAmount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                               <asp:BoundField DataField="IsCustomizedAmount" HeaderText="IsCustomizedAmount" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                               <asp:TemplateField HeaderText="Vat Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="txtApprovedVat" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedVat","{0:F2}") : Eval("VatAmount","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="NBT Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="txtApprovedNbt" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedNbt","{0:F2}") : Eval("NbtAmount","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
                                   <ItemTemplate>
                                       <asp:Label runat="server" ID="txtApprovedAmount" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedTotalAmount","{0:F2}") : Eval("TotalAmount","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                           </Columns>
                       </asp:GridView>
                   </div>
               </div>
           </div>

           <div class="col-xs-12 col-sm-12">
               <div class="col-xs-4 col-sm-4">

                   <div class="table-responsive">
                       <table class="table no-border">
                           <tr>
                               <td style="width: 50%; font-weight: bold">Payment Method : </td>
                               <td style="text-align: left; width: 50%;">
                                   <asp:Label runat="server" ID="lblPaymentMethod"></asp:Label></td>
                           </tr>
                           <tr>
                               <td style="width: 50%; font-weight: bold">Approved By : </td>
                               <td style="text-align: left; width: 50%;">
                                   <asp:Label runat="server" ID="lblApprovedByName"></asp:Label></td>
                           </tr>
                       </table>
                   </div>

               </div>
               <div class="col-xs-4 col-sm-4">
               </div>
               <div class="col-xs-4 col-sm-4">
                   <p class="lead">Amount Details</p>

                   <div class="table-responsive">
                       <table class="table">
                           <tr>
                               <td style="width: 50%">Subtotal:</td>
                               <td style="text-align: right">
                                   <asp:Label runat="server" ID="lblSubtotal"></asp:Label></td>
                           </tr>
                           <tr>
                               <td>Vat Total</td>
                               <td style="text-align: right">
                                   <asp:Label runat="server" ID="lblVatTotal"></asp:Label></td>
                           </tr>
                           <tr>
                               <td>NBT Total:</td>
                               <td style="text-align: right">
                                   <asp:Label runat="server" ID="lblNbtTotal"></asp:Label></td>
                           </tr>
                           <tr>
                               <td><b>Total:</b></td>
                               <td style="text-align: right"><b>
                                   <asp:Label runat="server" ID="lblTotal"></asp:Label></b></td>
                           </tr>
                       </table>
                   </div>
               </div>

           </div>

       </div>
       
    </div> 
             </div>
         <div id="editor"></div>

    </form>
</body>
</html>
