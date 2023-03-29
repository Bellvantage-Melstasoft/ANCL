<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="EditRejectedGRN.aspx.cs" Inherits="BiddingSystem.EditRejectedGRN" %>
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

   <div class="content" style="position: relative;background: #fff;overflow:hidden; border: 1px solid #f4f4f4;padding: 20px;margin: 10px 25px;" id="divPrintPo" >    <!-- Main content -->
   
   
      <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header" style="text-align:center;">
            <i class="fa fa-envelope"></i>Good Received Note
          </h2>
        </div>
        <!-- /.col -->
      </div>
    

      <div class="row invoice-info">

          <div class="col-sm-4 invoice-col">
          
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

        <div class="col-sm-4 invoice-col">
          
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
        <div class="col-sm-4 invoice-col">
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
        <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive"
         AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
        <Columns>
            <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="QuotationId" HeaderText="Quotation Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
               <asp:BoundField DataField="ItemId" HeaderText="Item No" />
                <asp:BoundField DataField="_AddItem.ItemName" HeaderText="Description" />
             <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price"  DataFormatString="{0:N2}"/>
             <asp:BoundField DataField="Quantity" HeaderText="Quantity"  />
             <asp:BoundField DataField="VatAmount" HeaderText="Vat Amount"  DataFormatString="{0:N2}" />
             <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount"  DataFormatString="{0:N2}" />
             <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount"  DataFormatString="{0:N2}" />
          
            
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
                <td style="width:50%">Subtotal:</td>
                <td><asp:Label runat="server" ID="lblSubtotal"></asp:Label></td>
              </tr>
              <tr>
                <td>Vat Total</td>
                <td><asp:Label runat="server" ID="lblVatTotal"></asp:Label></td>
              </tr>
              <tr>
                <td>NBT Total:</td>
                <td><asp:Label runat="server" ID="lblNbtTotal"></asp:Label></td>
              </tr>
              <tr>
                <td><b>Total:</b></td>
                <td><b><asp:Label runat="server" ID="lblTotal"></asp:Label></b></td>
              </tr>
            </table>
          </div>
        </div>

           <div class="row">
          <table >
             <tr style="width:100px;">
                 <td>Received Date&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td style="margin:10px; padding:10px;"> <asp:TextBox CssClass="form-control date1" runat="server" ID="GoodreceivedDate" ></asp:TextBox> </td>
             </tr>
             <tr style="width:100px;"> 
                 <td>Remarks&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td style="margin:10px; padding:10px;"><asp:TextBox ID="txtGRNote" runat="server" CssClass="form-control"  TextMode="MultiLine" Rows="5"></asp:TextBox></td>
             </tr>
             
         </table>
           </div>

       </div>

       <br>
       <div class="row pull-right">
        <div class="col-xs-12" >
          <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" onclick="btnCancel_Click"></asp:Button>
          <asp:Button ID="btnUpdateGrn" runat="server" Text="Update GRN" CssClass="btn btn-warning" onclick="btnUpdateGrn_Click"></asp:Button>
           </div>

      </div>
    </div> 
    </form>
   
    <script type="text/javascript">
        var DataList = <%=GetDateTime() %>
            $(document).ready(function () {
                $('#ContentSection_GoodreceivedDate').val(DataList); 
        });

    </script>

    <script type="text/javascript">
        var dtp01 = new DateTimePicker('.date1', { pickerClass: 'datetimepicker-blue', timePicker: true, timePickerFormat: 12, format: 'Y/m/d h:i', allowEmpty: true });
    </script>
</asp:Content>
