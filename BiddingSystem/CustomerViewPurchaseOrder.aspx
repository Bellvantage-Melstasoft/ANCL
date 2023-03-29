<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerViewPurchaseOrder.aspx.cs" Inherits="BiddingSystem.CustomerViewPurchaseOrder" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
	<script src="AdminResources/js/jquery-1.10.2.min.js"></script>
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
	 
	 @page { size: auto;  margin-top:50px } 
</style>
 <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
	<%--<script src="https://code.jquery.com/jquery-1.12.3.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/0.9.0rc1/jspdf.min.js"></script>--%>
    <script src="jquery-1.12.3.min.js"></script>
<script src="jspdf.min.js"></script>

	<%--<script>
		function printContent(el) {
			var restorepage = $('body').html();
			var printcontent = $('#' + el).clone();
			$('body').empty().html(printcontent);
			var doc = new jsPDF();
			doc.fromHTML(printcontent, 15, 15, {
				'width': 170});
			doc.save('sample-file.pdf');
			$('body').html(restorepage);
		}

		var doc = new jsPDF();
		var specialElementHandlers = {
			'#editor': function (element, renderer) {
				return true;
			}
		};

		$('#btnDownload').click(function () {
			doc.fromHTML($('#divPrintPo').html(), 15, 15, {
				'width': 170,
				'elementHandlers': specialElementHandlers
			});
			doc.save('sample-file.pdf');
		});

	</script>--%>
<section class="content-header">
	<h1>
	   View Purchase Order
		<small></small>
	  </h1>
	  <ol class="breadcrumb">
		<li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
		<li class="active">View Purchase Order</li>
	  </ol>
	</section>
	<br />
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
		 <%--<div id="printDiv">--%>
			 <section class="content" style="padding-top: 0px">
   <div class="content" style="position: relative;background: #fff;overflow:hidden;border: 1px solid #f4f4f4;"  id="divPrintPo" >    <!-- Main content -->
	

		   <div class="row">
			   <div class="col-xs-12">
				   <img src="AdminResources/images/logo.png" class="center-block" />
				   <h2 class="page-header" style="text-align: center;">
					   <i class="fa fa-envelope"></i>PURCHASE ORDER (PO)
				   </h2>
			   </div>
			   <!-- /.col -->
		   </div>


		   <div class="row">
			  <div class="col-xs-4">
          <address>
         <table >
             <tr>
                 <td>Date&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblDateNow" runat="server" Text=""></asp:Label></b></td>
             </tr>
           <%--  <tr>
                 <td>PO. No&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblPOCode" runat="server" Text=""></asp:Label></b></td>
             </tr>--%>
            <%-- <tr>
                 <td>Your Ref&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblRefNo" runat="server" Text=""></asp:Label></b></td>
             </tr>--%>
              <tr>
                 <td>PO Code&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblPOCode" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
                 <td>PR Code&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblBasedPr" runat="server" Text=""></asp:Label></b></td>
             </tr>
         </table>
           
          </address>
          </div>
          <div class="col-xs-4">
          <address>

               <table  >
             <tr>
                 <td>Company&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label></b></td>
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
                    
                   <tr>
                 <td>Store Keeper&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblstorekeeper" runat="server" Text=""></asp:Label></b></td>
             </tr>
         </table>

          </address>
          </div>
        <!-- /.col -->
          <div class="col-xs-4">
          <address>
              <table  >
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
	   <br />
		   <div class=" row panel-body">
			   <div class="co-xs-12">
				   <div class="table-responsive">
					   <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive table-bordered"
						   AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
						   <Columns>
							   <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
							   <asp:BoundField DataField="ItemId" HeaderText="Item No" />
							   <asp:BoundField DataField="_AddItem.ItemName" HeaderText="Description" />
								 <asp:BoundField DataField="supplierQuotationItem.Description" HeaderText="Detail" />
							   <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
							   <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
							   <asp:BoundField DataField="CustomizedAmount" HeaderText="CustomizedAmount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
							   <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
								   <ItemTemplate>
									   <asp:Label runat="server" ID="txtApproved" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedAmount","{0:N2}") : Eval("ItemPrice","{0:N2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
								   </ItemTemplate>
							   </asp:TemplateField>
							   <asp:BoundField DataField="Quantity" HeaderText="Quantity" DataFormatString="{0:N2}" />
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
									   <asp:Label runat="server" ID="txtApprovedVat" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedVat","{0:N2}") : Eval("VatAmount","{0:N2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
								   </ItemTemplate>
							   </asp:TemplateField>
							   <asp:TemplateField HeaderText="NBT Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
								   <ItemTemplate>
									   <asp:Label runat="server" ID="txtApprovedNbt" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedNbt","{0:N2}") : Eval("NbtAmount","{0:N2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
								   </ItemTemplate>
							   </asp:TemplateField>
							   <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
								   <ItemTemplate>
									   <asp:Label runat="server" ID="txtApprovedAmount" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedTotalAmount","{0:N2}") : Eval("TotalAmount","{0:N2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
								   </ItemTemplate>
							   </asp:TemplateField>
						   </Columns>
					   </asp:GridView>
				   </div>
			   </div>
		   </div>
       <div class="row">
           <div class="col-xs-12">
                     <div class="form-group">
                    <asp:Panel ID="pnlcondtion" runat="server" Width="100%">
                        <label for="fileImages">Terms And Conditons</label>
                        <asp:TextBox TextMode="MultiLine" Rows="10" BorderStyle="None"  ID="txtTermsAndConditions" Enabled="false"  runat="server" CssClass="form-control text-bold"></asp:TextBox>
                    </asp:Panel>
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
		   
		   <div class="row no-print">
			   <div class="col-xs-12">
				   <button class="btn btn-success" onclick="window.print();"><i class="fa fa-print"></i>Print</button>
				   <asp:Button ID="btnDownload" runat="server" Text="Send Email" class="btn btn-primary pull-right btnDownload" OnClick="btnDownload_Click" Style="margin-left: 10px;"></asp:Button>

				   <asp:Button ID="btnApprove" runat="server" Text="Approve"
					   class="btn btn-primary pull-right" Style="margin-left: 10px; visibility: hidden;"
					   OnClick="btnApprove_Click"></asp:Button>
				   <asp:Button ID="btnReject" runat="server" Text="Reject" Style="visibility: hidden;"
					   class="btn btn-danger pull-right" OnClick="btnReject_Click"></asp:Button>
			   </div>

		   </div>

	</div> 
			 </section>
			 <%--</div>--%>
		 <div id="editor"></div>

		 <asp:HiddenField ID="print" runat="server" />
	</form>
   
	<%--<script>
		$(function () {
			$('.btnDownload').on ({
				click: function () {
					debugger;
					var a = $('#printDiv').html();
					$('#ContentSection_print').val(a);
				}
			})
		})
	</script>--%>
	

</asp:Content>

