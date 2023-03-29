<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyViewApprovedGrnDetailsView.aspx.cs" Inherits="BiddingSystem.CompanyViewApprovedGrnDetailsView" %>
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
</style>
  <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>

  <section class="content-header">
    <h1>
       View GRN Report
        <small></small>
      </h1>
    <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li><a href="CustomerGRNApproval.aspx"> GRN View</a></li>
        <li class="active">GRN Detailed View</li>
      </ol>
    </section>
    <br />
   <form runat="server" id="form1">
   <section class="content" style="background: #fff;" >    <!-- Main content -->
          <div class="content" style="position: relative;background: #fff;overflow:hidden; border: 1px solid #f4f4f4;padding: 20px;margin: 10px 25px;" id="divPrintPo" >    <!-- Main content -->

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
        <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive table-bordered"   HeaderStyle-BackColor="LightGray" AutoGenerateColumns="false"   >
        <Columns>
           <%--  <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" onclick="CheckUncheckCheckboxes(this);"/>
                </HeaderTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="GrnId" HeaderText="GRN Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="QuotationId" HeaderText="Quotation Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemId" HeaderText="Item No" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
            <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price"  DataFormatString="{0:N2}"/>
            <asp:BoundField DataField="Quantity" HeaderText="Recieved Quantity"  />
            <asp:BoundField DataField="VatAmount" HeaderText="Vat Amount"  DataFormatString="{0:N2}" />
            <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount"  DataFormatString="{0:N2}" />
            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount"  DataFormatString="{0:N2}" />
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
                <td style="text-align:right"> <asp:Label runat="server" ID="lblSubtotal"></asp:Label></td>
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
          <button  class="btn btn-success" onclick="window.print();"><i class="fa fa-print" ></i> Print</button>
        </div>

      </div>
              </div>
    </section> 
    </form>
</asp:Content>
