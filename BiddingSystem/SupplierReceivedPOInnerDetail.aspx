<%@ Page Title="" Language="C#" MasterPageFile="~/SupplierLoadingWebUIInner.Master" AutoEventWireup="true" CodeBehind="SupplierReceivedPOInnerDetail.aspx.cs" Inherits="BiddingSystem.SupplierReceivedPOInnerDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="services-breadcrumb" style="background-color: #005383">
		<div class="agile_inner_breadcrumb">
			<div class="container">
				<ul class="w3_short">
					<li>
						<a href="SupplierInitialFrontViewInner.aspx" style="color:White">Home</a>
						<i style="color:White">|</i>
                        <a href="SupplierReceivedPOInner.aspx" style="color:Yellow">Received Purchase Orders</a>
                        <i style="color:White">|</i>
                        <a href="#" style="color:Yellow">Purchase Order Detials</a>
					</li>
				</ul>
			</div>
		</div>
	</div>
    <form runat="server" id="form1">
 <div class="faqs-w3l" style="background-color:White">
         <div class="container">
			<!-- //tittle heading -->
            <div class="row">
            <div class="col-md-12">	
 <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
        <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive table-bordered"
         AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray" >
        <Columns>
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" DataField="QuotationId" HeaderText="Quotation Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" ItemStyle-ForeColor="Black" DataField="ItemId" HeaderText="Item No" />
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" ItemStyle-ForeColor="Black" DataField="_AddItem.ItemName" HeaderText="Description" />
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" DataField="ItemPrice" HeaderText="Unit Price"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" DataField="Quantity" HeaderText="Quantity"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" DataField="CustomizedAmount" HeaderText="CustomizedAmount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:TemplateField HeaderStyle-BackColor="#005383" HeaderText="Unit Price" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
                 <asp:Label style="color:Black" runat="server" ID="txtApproved" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedAmount","{0:F2}") : Eval("ItemPrice","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" ItemStyle-ForeColor="Black" DataField="Quantity" HeaderText="Quantity"  />
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" DataField="VatAmount" HeaderText="Vat Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" DataField="NbtAmount" HeaderText="NBT Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" DataField="TotalAmount" HeaderText="Total Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" DataField="CustomizedVat" HeaderText="Vat Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" DataField="CustomizedNbt" HeaderText="NBT Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#005383" DataField="CustomizedAmount" HeaderText="Total Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>

            <asp:BoundField DataField="CustomizedTotalAmount" HeaderStyle-BackColor="#005383" HeaderText="CustomizedTotalAmount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>

            <asp:BoundField DataField="IsCustomizedAmount" HeaderStyle-BackColor="#005383" HeaderText="IsCustomizedAmount"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:TemplateField HeaderStyle-BackColor="#005383" HeaderText="Vat Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
                 <asp:Label runat="server" ID="txtApprovedVat" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedVat","{0:F2}") : Eval("VatAmount","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-BackColor="#005383" HeaderText="NBT Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
                 <asp:Label runat="server" ID="txtApprovedNbt" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedNbt","{0:F2}") : Eval("NbtAmount","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderStyle-BackColor="#005383" HeaderText="Total Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate >
                 <asp:Label runat="server" ID="txtApprovedAmount" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedTotalAmount","{0:F2}") : Eval("TotalAmount","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
    </div>
    </div>
    </div>
    
    </div>
    
    <div class="" style="background-color:White; color:Black;"> 
         <div class="container" style="margin-top: -55px;">
			<!-- //tittle heading -->
            <div class="row">
       
              <div class="col-md-6"></div>
              <div class="col-md-6">
         
          <p class="lead" style="color:Black;">Amount Details</p>

          <div class="table-responsive">
            <table class="table">
              <tr>
                <td style="width:50%;color:Black;">Subtotal:</td>
                <td style="text-align:right;color:Black;"><asp:Label runat="server" ID="lblSubtotal"></asp:Label></td>
              </tr>
              <tr>
                <td style="color:Black;">Vat Total</td>
                <td style="text-align:right;color:Black;"><asp:Label runat="server" ID="lblVatTotal"></asp:Label></td>
              </tr>
              <tr>
                <td style="color:Black;">NBT Total:</td>
                <td style="text-align:right;color:Black;"><asp:Label runat="server" ID="lblNbtTotal"></asp:Label></td>
              </tr>
              <tr>
                <td style="color:Black;"><b>Total:</b></td>
                <td style="text-align:right;color:Black;"><b><asp:Label runat="server" ID="lblTotal"></asp:Label></b></td>
              </tr>
            </table>
          </div>


              </div>
            </div>
       
            </div>
            </div>
       </form>
</asp:Content>
