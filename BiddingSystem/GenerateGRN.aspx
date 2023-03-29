<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="GenerateGRN.aspx.cs" EnableViewState="true" Inherits="BiddingSystem.GenerateGRN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
  <style type="text/css">
     
      
@media print{
          body{
                
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
    }
    .tablegv tr:nth-child(even){background-color: #f2f2f2;}
    .tablegv tr:hover {background-color: #ddd;}
    .tablegv th {
        padding-top: 12px;
        padding-bottom: 12px;
        text-align: left;
        background-color: #3C8DBC;
        color: white;
    }
    .successMessage
        {
            color: #1B6B0D;
            font-size: medium;
        }
        
        .failMessage
        {
            color: #C81A34;
            font-size: medium;
        }
      table, td, tr {
      border-color:black;
      
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
     
      input[type=number]::-webkit-inner-spin-button, 
input[type=number]::-webkit-outer-spin-button { 
  -webkit-appearance: none; 
  margin: 0; 
}
</style>
  <style type="text/css">
      .textboxalign
       {
         text-align:center;
         font-family:Verdana, Arial, Helvetica, sans-serif;
       }
  </style>
    <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.js"></script>
    <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />
 <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
<section class="content-header">
    <h1>
       Generate GRN
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li><a href="CustomerViewApprovedPurchaseOrder.aspx">Approved Purchase Orders (PO)</a></li>
        <li class="active">Generate Grn</li>
      </ol>
    </section>
    <br />
     <form runat="server" id="form1">
         <asp:HiddenField runat="server" ID="hdnReceivedDate" />

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

   <div class="content" style="position: relative;background: #fff;overflow:hidden; border: 1px solid #f4f4f4;" id="divPrintPo" >    <!-- Main content -->
   
   
      <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header" style="text-align:center;">
            <i class="fa fa-envelope"></i>Good Received Note
          </h2>
        </div>
        <!-- /.col -->
      </div>
      <div class="row">

        <div class="col-sm-4">
          <address>
          <table >
             <tr>
                 <td>Date&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblDateNow" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
                 <td>PO. No&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblPOCode" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
                 <td>Your Ref&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblRefNo" runat="server" Text=""></asp:Label></b></td>
             </tr>
         </table>
          </address>
        </div>

        <div class="col-sm-4">
         <address>
         <table >
             <tr>
                 <td>Company&nbsp;</td>
                 <td>:&nbsp;</td>
                 <td><b><asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
                 <td style="vertical-align:top;" >Address&nbsp;</td>
                   <td style="vertical-align:top;">:&nbsp;</td>
                   <td ><b><asp:Label ID="lblCompanyAddress" runat="server" Text=""></asp:Label></b><br /><b><asp:Label ID="lblCity" runat="server" Text=""></asp:Label></b><br /><b><asp:Label ID="lblCountry" runat="server" Text=""></asp:Label></b></td>
             </tr>     
             <tr>
                 <td>VAT No&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblVatNo" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
                 <td>Telephone&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
               <td>Fax&nbsp;</td>
               <td>:&nbsp;</td>
               <td><b><asp:Label ID="lblFaxNo" runat="server" Text=""></asp:Label></b></td>
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
                 <td><b><asp:Label ID="lblSupplierName" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
                 <td>Address&nbsp;</td>
                 <td>:&nbsp;</td>
                 <td><b><asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></b></td>
             </tr>
             </table>
          </address>
        </div>
      
      </div>

       <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
        <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="gvPurchaseOrderItems table table-responsive table-bordered"
         AutoGenerateColumns="false" EnableViewState="true" HeaderStyle-BackColor="LightGray">
         <Columns>
                <asp:BoundField ItemStyle-Width="150px" DataField="PoID" HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName"  HeaderText="Category Name"  />
                <asp:BoundField ItemStyle-Width="150px" DataField="ItemId"  HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName"  HeaderText="Sub Category Name"  />
                <asp:BoundField ItemStyle-Width="150px" DataField="ItemName"  HeaderText="Item Name"  />
                <asp:BoundField ItemStyle-Width="150px" DataField="VatAmount"  HeaderText="Vat Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                <asp:BoundField ItemStyle-Width="150px" DataField="NbtAmount"  HeaderText="NBT Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                <asp:BoundField ItemStyle-Width="150px" DataField="TotalAmount"  HeaderText="Total Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                <asp:BoundField ItemStyle-Width="150px" DataField="CustomizedVat"  HeaderText="Customized Vat" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                <asp:BoundField ItemStyle-Width="150px" DataField="CustomizedNbt"  HeaderText="Customized Nbt" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                <asp:BoundField ItemStyle-Width="150px" DataField="CustomizedTotalAmount"  HeaderText="Customized Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                <asp:BoundField DataField="CustomizedAmount" HeaderText="CustomizedAmount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>


                <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                 <ItemTemplate>
                     <asp:Label runat="server" CssClass="unitAmount" ID="txtApproved" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedAmount","{0:F2}") : Eval("ItemPrice","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Quantity" HeaderText="Pending QTY" DataFormatString="{0:N0}"/>

              <%--<asp:TemplateField HeaderText="Quantity" >
                 <ItemTemplate>
                     <asp:Label runat="server" ID="lblQuantithy" CssClass="intialQuantity"  Text='<%#Eval("Quantity") %>'></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>--%>
               
                <asp:TemplateField HeaderText="Received Qty" ItemStyle-Width="100">          
                <ItemTemplate>
                    <asp:TextBox ID="txtQty" Text='<%# Eval("Quantity") %>' runat="server" CssClass="txtQtyChaning textboxalign" type="number"  style="width:100px;" ></asp:TextBox>
                    <asp:RequiredFieldValidator  ID="RequiredValidator1" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="txtQty" ValidationGroup="btnGenerateGrn"></asp:RequiredFieldValidator>
                 <%--<asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="*" Font-Bold="true" Font-Size="Large" ForeColor="Red" ControlToValidate="txtQty" MinimumValue="1" Type="Integer" ValidationGroup="btnGenerateGrn"></asp:RangeValidator>--%>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IsCustomizedAmount" HeaderText="IsCustomizedAmount"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                <asp:TemplateField HeaderText="Vat Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                 <ItemTemplate>
                     <asp:TextBox CssClass="ApprovedVat" runat="server" Enabled="false" ID="txtApprovedVat" style="text-align:right" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedVat","{0:F2}") : Eval("VatAmount","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:TextBox>
                 </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NBT Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                 <ItemTemplate>
                     <asp:TextBox CssClass="ApprovedNbt" EnableViewState="true" Enabled="false"  runat="server" style="text-align:right" ID="txtApprovedNbt" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedNbt","{0:F2}") : Eval("NbtAmount","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>' ></asp:TextBox> 
                 </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                 <ItemTemplate>
                     <asp:TextBox runat="server" CssClass="calculateTotalAmount" Enabled="false" ID="txtApprovedAmount" style="text-align:right" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedTotalAmount","{0:F2}") : Eval("TotalAmount","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:TextBox>
                 </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AddToGrnCount" HeaderText="AddToGrnCount"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
       <div class="col-xs-12 col-sm-12">
            <div class="col-xs-6 col-sm-8">
                </div>
            <div class="col-xs-6 col-sm-4">
          <p class="lead">Amount Details</p>

          <div class="table-responsive">
            <table class="table">
              <tr>
                <td style="width:50%">Subtotal:</td>
                <td><asp:Label runat="server" ID="lblSubtotal" CssClass="pull-right"></asp:Label></td>
              </tr>
              <tr>
                <td>Vat Total</td>
                <td><asp:Label runat="server" ID="lblVatTotal" CssClass="pull-right"></asp:Label></td>
              </tr>
              <tr>
                <td>NBT Total:</td>
                <td><asp:Label runat="server" ID="lblNbtTotal" CssClass="pull-right"></asp:Label></td>
              </tr>
              <tr>
                <td><b>Total:</b></td>
                <td><b><asp:Label runat="server" ID="lblTotal" CssClass="pull-right"></asp:Label></b></td>
              </tr>
            </table>
          </div>
        </div>
       </div>
       <div class="col-md-12">
             <div class="row">
               <div class="col-md-6">
                 <div class="form-group">
                <label style="display:inline;" for="exampleInputEmail1">Invoice No</label>  
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtInvoiceNo" ValidationGroup="btnGenerateGrn" ErrorMessage="*" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                 <asp:TextBox  runat="server" ID="txtInvoiceNo"  class=" form-control" ></asp:TextBox>
                </div>
                </div>
             <div class="col-md-6">
                 <div class="form-group">
                <label style="display:inline;" for="exampleInputEmail1">Received Date</label>  
                     <asp:RequiredFieldValidator runat="server" ID="req01" ControlToValidate="GoodreceivedDate" ValidationGroup="btnGenerateGrn" ErrorMessage="*" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                    
                <input type="text" class=" form-control date1" runat="server" id="GoodreceivedDate" autocomplete="off" /> 
                </div>
                </div>
                
              <div class="col-md-6"> 
              <div class="form-group">
                <label for="exampleInputEmail1">Remarks</label>
                <asp:TextBox ID="txtGRNote" runat="server" CssClass="form-control"  TextMode="MultiLine" Rows="5"></asp:TextBox>
                </div>
              </div>
                <%-- <td>Received Date&nbsp;</td>
                 <td>:&nbsp;</td>
                 <td style="margin:10px; padding:10px;"> <input type="text" class=" form-control date1" runat="server" id="GoodreceivedDate" /> </td>
                 <td>Remarks&nbsp;</td>
                 <td>:&nbsp;</td>
                 <td style="margin:10px; padding:10px;"><asp:TextBox ID="txtGRNote" runat="server" CssClass="form-control"  TextMode="MultiLine" Rows="5"></asp:TextBox></td>
--%>            
           </div>
           </div>

       <br />
       <div class="row pull-right">
        <div class="col-xs-12 col-sm-12" >
          <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" onclick="btnCancel_Click"></asp:Button>
          <asp:Button ID="btnGenerateGrn" runat="server" Text="Generate GRN" ValidationGroup="btnGenerateGrn"  CssClass="btn btn-primary"  OnClick="btnGenerateGrn_Click"></asp:Button>
           </div>
         <%--  onclick="btnGenerateGrn_Click"--%>
      </div>
    </div> 
    </form>
   
    

    <script type="text/javascript">



        $(document).ready(function () {
            var dateToday = new Date()
            var dateTommorow = dateToday.setDate(dateToday.getDate() + 1);
            var dtp01 = new DateTimePicker('.date1', { pickerClass: 'datetimepicker-blue', timePickerFormat: 12, format: 'd/m/y', maxDate: dateTommorow });

            var toatalAmount = 0;
            var vatamount = 0;
            var nbtAmount = 0;
            var subTotl = 0;
            var gridView = document.getElementById('<%= gvPurchaseOrderItems.ClientID %>');

            var customAmount;

            $(".gvPurchaseOrderItems tr:not(:first)").each(function () {
               
        
                var TotalAmount = $(this).children().find(".calculateTotalAmount").val();
                var TotalVattAmount = $(this).children().find(".ApprovedVat").val();
                var TotalNbtAmount = $(this).children().find(".ApprovedNbt").val();
                var quantity = $(this).children().find(".intialQuantity").html();
                var unitPrice = $(this).children().find(".unitAmount").html();

                if (!isNaN(TotalAmount)) {
                    toatalAmount = parseFloat(TotalAmount) + (toatalAmount);
                    nbtAmount = parseFloat(TotalNbtAmount) + (nbtAmount);
                    vatamount = parseFloat(TotalVattAmount) + (vatamount);
                    subTotl = parseFloat(unitPrice) * parseFloat(quantity) + (subTotl);
                }

                $("#ContentSection_lblSubtotal").text(subTotl.toFixed(2));
                $("#ContentSection_lblNbtTotal").text(nbtAmount.toFixed(2));
                $("#ContentSection_lblVatTotal").text(vatamount.toFixed(2));
                var totalAmountWithVatNbt = toatalAmount + nbtAmount + vatamount;
                $("#ContentSection_lblTotal").text(toatalAmount.toFixed(2));

            });


//            for (var i = 1; i <= gridView.rows.length - 1; i++) {

//                var TotalAmount = $("input[id*=txtApprovedAmount]").val();
//                var TotalVattAmount = $("input[id*=txtApprovedVat]").val();
//                var TotalNbtAmount = $("input[id*=txtApprovedNbt]").val();


//                if (!isNaN(TotalAmount)) {
//                    subtotalVal = parseFloat(TotalAmount) + (subtotalVal);
//                    nbtAmount = parseFloat(TotalNbtAmount) + (nbtAmount);
//                    vatamount = parseFloat(TotalVattAmount) + (vatamount);
//                }
//            }
//            $("#ContentSection_lblSubtotal").text(subtotalVal);
//            $("#ContentSection_lblNbtTotal").text(nbtAmount.toFixed(2));
//            $("#ContentSection_lblVatTotal").text(vatamount.toFixed(2));
//            var totalAmountWithVatNbt = subtotalVal + nbtAmount + vatamount;
//            $("#ContentSection_lblTotal").text(totalAmountWithVatNbt.toFixed(2));
        });

        $(".txtQtyChaning").keyup(function () {

            var rQty = $(this).val();
            var initialQuantity = $(this).parent().prev().children().html();
            var price = $(this).parent().prev().prev().children().html();

           


            if ( event.keyCode == 69 || event.keyCode == 43 || event.keyCode == 45 || event.keyCode == 107 || event.keyCode == 109 || event.keyCode == 190 || parseFloat(rQty) > parseFloat(initialQuantity) || parseFloat(rQty) < 0)
            {
                //$(this).val(initialQuantity)
                $(this).val(0)
                var v = this.id;
                var field = v.split('_');
                var valuid = field[3];
                
                var txtAmountReceive = $("input[id*=" + v + "]");


                $("#ContentSection_gvPurchaseOrderItems_txtApprovedVat_" + valuid).val(0);
                $("#ContentSection_gvPurchaseOrderItems_txtApprovedNbt_" + valuid).val(0);
                $("#ContentSection_gvPurchaseOrderItems_txtApprovedAmount_" + valuid).val(0);


                  var toatalAmount = 0;
            var vatamount = 0;
            var nbtAmount = 0;
            var subTotl = 0;
            var gridView = document.getElementById('<%= gvPurchaseOrderItems.ClientID %>');

            var customAmount;

            $(".gvPurchaseOrderItems tr:not(:first)").each(function () {
               
        
                var TotalAmount = $(this).children().find(".calculateTotalAmount").val();
                var TotalVattAmount = $(this).children().find(".ApprovedVat").val();
                var TotalNbtAmount = $(this).children().find(".ApprovedNbt").val();
                var quantity = $(this).children().find(".textboxalign").val() == "" ? "0" : $(this).children().find(".textboxalign").val();
                var unitPrice = $(this).children().find(".unitAmount").html();

                if (!isNaN(TotalAmount)) {
                    toatalAmount = parseFloat(TotalAmount) + (toatalAmount);
                    nbtAmount = parseFloat(TotalNbtAmount) + (nbtAmount);
                    vatamount = parseFloat(TotalVattAmount) + (vatamount);
                    subTotl = parseFloat(unitPrice) * parseFloat(quantity) + (subTotl);
                }

                $("#ContentSection_lblSubtotal").text(subTotl.toFixed(2));
                $("#ContentSection_lblNbtTotal").text(nbtAmount.toFixed(2));
                $("#ContentSection_lblVatTotal").text(vatamount.toFixed(2));
                var totalAmountWithVatNbt = toatalAmount + nbtAmount + vatamount;
                $("#ContentSection_lblTotal").text(toatalAmount.toFixed(2));

            });


                return false;
            }  
               
            

            else {

                var InitialvatAmount = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().html()
                var InitianbtAmount = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().html()

                var vatAmount = $(this).parent().parent().find(".ApprovedVat").val();
                var nbtAmount = $(this).parent().parent().find(".ApprovedNbt").val();


                if (parseFloat(InitialvatAmount) > 0 && parseFloat(InitianbtAmount) > 0) {
                    var nbt = rQty * price * 2 / 98;
                    var vat = ((rQty * price) + nbt) * 0.15;
                    var total = (rQty * price) + nbt + vat

                    $(this).parent().parent().find(".calculateTotalAmount").val(total.toFixed(2));
                    $(this).parent().parent().find(".ApprovedNbt").val(nbt.toFixed(2));
                    // $(this).parent().next().next().next().html(nbt.toFixed(2));
                    $(this).parent().parent().find(".ApprovedVat").val(vat.toFixed(2));


                    if ($(this).parent().next().html() == "1") {

                        $(this).parent().next().next().next().next().css("color", "red");
                        $(this).parent().next().next().next().css("color", "red");
                        $(this).parent().next().next().css("color", "red");
                    }

                    var totalAmount = 0;
                    var vatamount = 0;
                    var nbtAmount = 0;
                    var subTotl = 0;
                 
                    var gridView = document.getElementById('<%= gvPurchaseOrderItems.ClientID %>');

                    var customAmount;;

                    for (var i = 1; i <= gridView.rows.length - 1; i++) {
                      
                        var TotalAmount = gridView.rows[i].cells[18].childNodes[1].value
                        var TotalVattAmount =   gridView.rows[i].cells[16].childNodes[1].value
                        var TotalNbtAmount = gridView.rows[i].cells[17].childNodes[1].value
                        var quantity = gridView.rows[i].cells[14].childNodes[1].value == "" ? 0 : gridView.rows[i].cells[14].childNodes[1].value
                        var unitPrice = gridView.rows[i].cells[12].childNodes[1].innerText

                        if (!isNaN(TotalAmount)) {
                            totalAmount = parseFloat(TotalAmount) + (totalAmount);
                            nbtAmount = parseFloat(TotalNbtAmount) + (nbtAmount);
                            vatamount = parseFloat(TotalVattAmount) + (vatamount);
                            subTotl = parseFloat(unitPrice) * parseFloat(quantity) + (subTotl);
                        }
                    }
                    $("#ContentSection_lblSubtotal").text(subTotl.toFixed(2));
                    $("#ContentSection_lblNbtTotal").text(nbtAmount.toFixed(2));
                    $("#ContentSection_lblVatTotal").text(vatamount.toFixed(2));
                    var totalAmountWithVatNbt = totalAmount + nbtAmount + vatamount;
                    $("#ContentSection_lblTotal").text(totalAmount.toFixed(2));

                }
                else {
                    var total = rQty * price;

                    $(this).parent().parent().find(".calculateTotalAmount").val(total.toFixed(2));

                    if ($(this).parent().next().html() == "1") {

                        $(this).parent().next().next().next().next().css("color", "red");
                        $(this).parent().next().next().next().css("color", "red");
                        $(this).parent().next().next().css("color", "red");
                    }

                    var totalAmount = 0;
                    var vatamount = 0;
                    var nbtAmount = 0;
                    var subTotl = 0;
                    var gridView = document.getElementById('<%= gvPurchaseOrderItems.ClientID %>');

                    var customAmount;;

                    for (var i = 1; i <= gridView.rows.length - 1; i++) {

                        var TotalAmount = gridView.rows[i].cells[18].childNodes[1].value
                        var TotalVattAmount = gridView.rows[i].cells[16].childNodes[1].value
                        var TotalNbtAmount = gridView.rows[i].cells[17].childNodes[1].value
                        var quantity = gridView.rows[i].cells[14].childNodes[1].value == "" ? 0 : gridView.rows[i].cells[14].childNodes[1].value
                        var unitPrice = gridView.rows[i].cells[12].childNodes[1].innerText

                        if (!isNaN(TotalAmount)) {
                            totalAmount = parseFloat(TotalAmount) + (totalAmount);
                            nbtAmount = parseFloat(TotalNbtAmount) + (nbtAmount);
                            vatamount = parseFloat(TotalVattAmount) + (vatamount);
                            subTotl = parseFloat(unitPrice) * parseFloat(quantity) + (subTotl);
                        }
                    }
                    $("#ContentSection_lblSubtotal").text(subTotl.toFixed(2));
                    $("#ContentSection_lblNbtTotal").text(nbtAmount.toFixed(2));
                    $("#ContentSection_lblVatTotal").text(vatamount.toFixed(2));
                    var totalAmountWithVatNbt = totalAmount + nbtAmount + vatamount;
                    $("#ContentSection_lblTotal").text(totalAmount.toFixed(2));
                }
            }
        });

      

    </script>

    <script>

        //$(".txtQtyChaning").keypress(function ()
        //{
        //    var itemQuantity = $(this).val();
        //    var textboxQuantity = $(this).parent().prev().children().html();

        //    if (event.value == null || event.value == "") {
        //        var textboxQuantity = 0;
        //    }
        //    else {
        //        textboxQuantity = event.value;
        //    }


        //    if (event.keyCode == 101 || event.keyCode == 69 || event.keyCode == 43 || event.keyCode == 45 || parseFloat( textboxQuantity) > parseFloat( itemQuantity )) {
        //        $(event).parent().prev().prev().children().val(itemQuantity);
        //        $(event).parent().prev().text(itemQuantity)
        //        event.returnValue = false;
        //        return;
        //    }
        //    //event.returnValue = true;
        
        //});

      
    </script>
  

</asp:Content>
