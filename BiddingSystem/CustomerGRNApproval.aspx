<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerGRNApproval.aspx.cs" Inherits="BiddingSystem.CustomerGRNApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
  <style type="text/css">
      body{
                 
             }

      
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
     
      
</style>

 <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>





<section class="content-header">
    <h1>
       GRN Approval
        <small></small>
      </h1>
    <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li><a href="CustomerGRNView.aspx"> GRN View</a></li>
        <li class="active">GRN Approval</li>
      </ol>
    </section>
    <br />
     <form runat="server" id="form1">

          <div id="modalSelectCheckBox" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style="background-color:#e66657">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h2 id="lblTitle" style="color:white;" class="modal-title">Alert!!</h2>
            </div>
            <div class="modal-body" style="background-color:white">
                <h4>At least select one Item for Bid Opening</h4>
            </div>
            <div class="modal-footer" style="background-color:white">
                <button id="btnOkAlert"  type="button" class="btn btn-danger" >OK</button>
            </div>
        </div>
    </div>
</div>
          <div id="modalRejectSubmitCheckBox" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style="background-color:#e66657">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h2 id="lblTitleR" style="color:white;" class="modal-title">Alert!!</h2>
            </div>
            <div class="modal-body" style="background-color:white">
                <h4>At least select one Item for Reject Bid Opening</h4>
            </div>
            <div class="modal-footer" style="background-color:white">
                <button id="btnOkAlertR"  type="button" class="btn btn-danger" >OK</button>
            </div>
        </div>
    </div>
</div>
          <div id="modalReject" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 id="lblTitleConfirmYesNo2" class="modal-title">Confirmation</h4>
            </div>
            <div class="modal-body">
                <p>Are you sure you want to reject this Grn? Or ?</p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="Button1" runat="server"  CssClass="btn btn-primary" OnClick="btnRejectBtn_Click"  Text="Yes" ></asp:Button>
                <button id="btnNoConfirmYesNo2"  type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>

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

   <section class="content" style="background: #fff;" >    <!-- Main content -->
      <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header" style="text-align:center;">
            <i class="fa fa-envelope"></i> Good Recieved Note (GRN)
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
             <tr>
                 <td>Invoice No&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblInvoiceNo" runat="server" Text=""></asp:Label></b></td>
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
                   
            <%-- <tr>
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
             </tr>--%>
         </table>

          </address>
        </div>
        <!-- /.col -->
        <div class="col-sm-4">
          <address>
              <table >
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
        <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive table-bordered"
         AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
        <Columns>
             <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" onclick="CheckUncheckCheckboxes(this);"/>
                </HeaderTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GrnId" HeaderText="GRN Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="QuotationId" HeaderText="Quotation Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemId" HeaderText="Item No" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
            <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price"  DataFormatString="{0:N2}"/>
            <asp:BoundField DataField="Quantity" HeaderText="Quantity"  />
            <asp:BoundField DataField="VatAmount" HeaderText="Vat Amount"  DataFormatString="{0:N2}" />
            <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount"  DataFormatString="{0:N2}" />
            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount"  DataFormatString="{0:N2}" />
            <asp:TemplateField  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblUnitPrice" Text='<%#Eval("ItemPrice") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblQuantity" Text='<%#Eval("Quantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblVatAmount" Text='<%#Eval("VatAmount") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblNbtAmount" Text='<%#Eval("NbtAmount") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblTotalAmount" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
      <div class="col-xs-12 col-sm-12">
            <div class="col-xs-8 col-sm-8">
                </div>
            <div class="col-xs-4 col-sm-4">
          <p class="lead">Amount Details</p>

          <div class="table-responsive">
            <table class="table">
              <tr>
                <td style="width:50%">Subtotal:</td>
                <td style="text-align:right"><asp:Label runat="server" ID="lblSubtotal"></asp:Label></td>
              </tr>
              <tr>
                <td>Vat Total:</td>
                <td style="text-align:right"><asp:Label runat="server" ID="lblVatTotal"></asp:Label></td>
              </tr>
              <tr>
                <td>NBT Total:</td>
                <td style="text-align:right"><asp:Label runat="server" ID="lblNbtTotal"></asp:Label></td>
              </tr>
              <tr>
                <td><b>Total:</b></td>
                <td style="text-align:right"><b><asp:Label runat="server" ID="lblTotal"></asp:Label></b></td>
              </tr>
            </table>
          </div>
        </div>

            <div class="row">
          <table style="width:50%;">
             <tr style="width:100%;">
                 <td>Received Date&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td style="margin:10px; padding:10px;"><label id="lblReceiveddate" runat="server" class="form-control"></label> </td>
             </tr>
             <tr style="width:100%;"> 
                 <td>Remarks&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td style="margin:10px; padding:10px;"><label id="lblgrnComment" runat="server" class="form-control"></label></td>
             </tr>
             
         </table>
           </div>

       </div>
      <div class="row no-print">
        <div class="col-xs-12">
         <%-- <button  class="btn btn-success" onclick="window.print();"><i class="fa fa-print" ></i> Print</button>--%>
          <asp:Button ID="btnApprove" runat="server" Text="Approve" class="btn btn-primary pull-right" style=" margin-left: 10px; " OnClientClick="return validataAlFields(1);" onclick="btnApprove_Click"></asp:Button>
          <asp:Button ID="btnReject" runat="server" Text="Reject"  class="btn btn-danger pull-right" onclick="btnReject_Click" OnClientClick="return validataAlFields(2);"></asp:Button>
        </div>

      </div>

       <br />
      <div id="rejectedReason" runat="server" visible="false">
      <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <label for="exampleInputEmail1">Reason for Reject</label> <label id="lblrejectReason" style="color:red;font-weight:bolder"></label>
                <asp:TextBox ID="txtRejectReason" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
            </div>
        </div>
      </div>
      <div class="row">
      <div class="col-md-12">
        <div class="form-group">
        <asp:Button id="btnRejectBtn" ValidationGroup="btnRejectBtn"  runat="server" class="btn btn-danger pull-right"  Text="Proceed Reject"  OnClientClick="return validataAlFieldsForProceedReject();"></asp:Button>
        </div>
        </div>
        </div>
      </div>
    </section> 
    </form>
   
     <script language = "javascript" type = "text/javascript">
     function CheckUncheckCheckboxes(CheckBox)
     {
         var message = "";
         var subtotal = 0;
         var vattotal = 0;
         var nbttotal = 0;
         var Alltotal = 0;

            var TargetBaseControl = document.getElementById('<%= this.gvPurchaseOrderItems.ClientID %>');
            var TargetChildControl = "CheckBox1";
            var Inputs = TargetBaseControl.getElementsByTagName("input");  
            var headCheckBox = Inputs[0].checked;
           

            for(var n = 1; n < Inputs.length; ++n)
                if(Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl,0) >= 0)
                {
                    if (headCheckBox == true)
                    {
                        Inputs[n].checked = true;
                        var rows = TargetBaseControl.getElementsByTagName("tr");
                        var unitprice = parseFloat(GetChildControl(rows[n], "lblUnitPrice").innerHTML);
                        var quantity = parseFloat(GetChildControl(rows[n], "lblQuantity").innerHTML);
                        var eachsub = unitprice * quantity;
                        var eachvat = parseFloat(GetChildControl(rows[n], "VatAmount").innerHTML);
                        var eachnbt = parseFloat(GetChildControl(rows[n], "NbtAmount").innerHTML);
                        var eachtotal = eachsub + eachvat + eachnbt;

                        subtotal = subtotal + eachsub;
                        vattotal = vattotal + eachvat;
                        nbttotal = nbttotal + eachnbt;
                        Alltotal = Alltotal + eachtotal;
                    }
                    else
                    {
                        Inputs[n].checked = false;
                        $("#<%=lblSubtotal.ClientID%>").text('0.00');
                        $("#<%=lblVatTotal.ClientID%>").text('0.00');
                        $("#<%=lblNbtTotal.ClientID%>").text('0.00');
                        $("#<%=lblTotal.ClientID%>").text('0.00');
                    }
                }
         $("#<%=lblSubtotal.ClientID%>").text(thousands_separators(subtotal.toFixed(2)));
         $("#<%=lblVatTotal.ClientID%>").text(thousands_separators(vattotal.toFixed(2)));
         $("#<%=lblNbtTotal.ClientID%>").text(thousands_separators(nbttotal.toFixed(2)));
         $("#<%=lblTotal.ClientID%>").text(thousands_separators(Alltotal.toFixed(2)));
     }

         function GetChildControl(element, id) {
             var child_elements = element.getElementsByTagName("*");
             for (var i = 0; i < child_elements.length; i++) {
                 if (child_elements[i].id.indexOf(id) != -1) {
                     return child_elements[i];
                 }
             }
         };
   </script> 



     <script type="text/javascript">
         $(function () {
           //  debugger;
            $('[id*=CheckBox1]').on('change', function () {
                var subtotal = 0;
                var vattotal = 0;
                var nbttotal = 0;
                var Alltotal = 0;
                $('[id*=CheckBox1]:checked').each(function () {
                    var row = $(this).closest('tr');
                    var eachsub = parseFloat(row.find('[id*=lblUnitPrice]').html()) * parseInt(row.find('[id*=lblQuantity]').html());
                    var eachvat = parseFloat(row.find('[id*=VatAmount]').html());
                    var eachnbt = parseFloat(row.find('[id*=NbtAmount]').html());
                    var eachtotal = eachsub + eachvat + eachnbt;

                    subtotal = subtotal + eachsub;
                    vattotal = vattotal + eachvat;
                    nbttotal = nbttotal + eachnbt;
                    Alltotal = Alltotal + eachtotal;
                });
              
                $("#<%=lblSubtotal.ClientID%>").text(thousands_separators(subtotal.toFixed(2)));
                $("#<%=lblVatTotal.ClientID%>").text(thousands_separators(vattotal.toFixed(2)));
                $("#<%=lblNbtTotal.ClientID%>").text(thousands_separators(nbttotal.toFixed(2)));
                $("#<%=lblTotal.ClientID%>").text(thousands_separators(Alltotal.toFixed(2)));
            });
        });
    </script>
    <script>
        function thousands_separators(num) {
            var num_parts = num.toString().split(".");
            num_parts[0] = num_parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            return num_parts.join(".");
        }
    </script>

    <script>

function validataAlFields(status) {
    var isValid = false;
    var count = 0;
    var gridView = document.getElementById('<%= gvPurchaseOrderItems.ClientID %>');
    for (var i = 1; i < gridView.rows.length; i++) {
        var inputs = gridView.rows[i].getElementsByTagName('input');
        if (inputs != null && inputs[0] != null) {
            if (inputs[0].type == "checkbox") {
                if (inputs[0].checked) {
                    count++;
                    
                }
            }
        }
    }

    if (count > 0) {
        if (status == 1) {
            return true;
        }
        if (status == 2) {
            return true;
        }
        
    }

    else {
        if (status == 1)
        {
            $('#modalSelectCheckBox').modal('show');
        }
        if (status == 2) {
            $('#modalRejectSubmitCheckBox').modal('show');
        }
      
        return false;
    }
}


        function validataAlFieldsForProceedReject() {
    var isValid = false;
    var count = 0;
    var gridView = document.getElementById('<%= gvPurchaseOrderItems.ClientID %>');
    for (var i = 1; i < gridView.rows.length; i++) {
        var inputs = gridView.rows[i].getElementsByTagName('input');
        if (inputs != null && inputs[0] != null) {
            if (inputs[0].type == "checkbox") {
                if (inputs[0].checked) {
                    count++;
                    
                }
            }
        }
    }

    if (count > 0) {
       
        if ($("#<%=txtRejectReason.ClientID%>").val() != "")
        {
            $('#modalReject').modal('show');
        }
        else
        {
            $("#lblrejectReason").text('*');
        }
      
            return false;
    }

    else {
        $('#modalRejectSubmitCheckBox').modal('show');
        return false;
    }
}



        $("#btnOkAlert").click(function () {
            $('#modalSelectCheckBox').modal('hide');
        });

        $("#btnOkAlertR").click(function () {
            $('#modalRejectSubmitCheckBox').modal('hide');
        });
        $("#btnNoConfirmYesNo2").click(function () {
            $('#modalReject').modal('hide');
        });
        <%--$("#<%=btnRejectBtn.ClientID%>").click(function () {
            $('#modalReject').modal('show');
            return false;
        });--%>

        
    </script>
</asp:Content>
