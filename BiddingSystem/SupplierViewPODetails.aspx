<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSupplier.Master" AutoEventWireup="true" CodeBehind="SupplierViewPODetails.aspx.cs" Inherits="BiddingSystem.SupplierViewPODetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="span9">	
 <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
        <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive table-bordered"
         AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray" >
        <Columns>
            <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="QuotationId" HeaderText="Quotation Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemId" HeaderText="Item No" />
            <asp:BoundField DataField="_AddItem.ItemName" HeaderText="Description" />
            <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="Quantity" HeaderText="Quantity"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CustomizedAmount" HeaderText="CustomizedAmount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
                 <asp:Label runat="server" ID="txtApproved" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedAmount","{0:F2}") : Eval("ItemPrice","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Quantity" HeaderText="Quantity"  />
            <asp:BoundField DataField="VatAmount" HeaderText="Vat Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CustomizedVat" HeaderText="Vat Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CustomizedNbt" HeaderText="NBT Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CustomizedAmount" HeaderText="Total Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>

            <asp:BoundField DataField="CustomizedTotalAmount" HeaderText="CustomizedTotalAmount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>

            <asp:BoundField DataField="IsCustomizedAmount" HeaderText="IsCustomizedAmount"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
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
    </div>
     <div class="span4"></div>
     <div class="span5">	
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
                <td>Vat Total</td>
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
       </div>
       </div>
</asp:Content>
