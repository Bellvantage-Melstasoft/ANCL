<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyMRReportView.aspx.cs" Inherits="BiddingSystem.CompanyMRReportView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
  <style type="text/css">
      
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

      .tbldetail tr {
          background-color: #fafafa;
      }
</style>
     <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
<section class="content-header" style="padding-left: 27px;">
    <h1>
       MATERIAL REQUISITION (MR)
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">View Material Requisition</li>
      </ol>
    </section>
    <br />
     <form runat="server" id="form1">
     

   <div class="content" style="position: relative;background: #fff;overflow:hidden; border: 1px solid #f4f4f4;padding: 20px;margin: 10px 25px;" id="divPrintPo" >    <!-- Main content -->
   <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header" style="text-align:center;">
            <i class="fa fa-envelope"></i>  MATERIAL REQUISITION (PR)
          </h2>
        </div>
        <!-- /.col -->
      </div>
      <div class="row invoice-info">
          <div class="col-xs-4 invoice-col">
            <address>
               <table >
                 <tr>
                    <td>Company&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label></b></td>
                </tr>
                 <%--<tr>
                     <td>Our Ref&nbsp;</td>
                       <td>:&nbsp;</td>
                       <td><b><asp:Label ID="lblRef" runat="server" Text=""></asp:Label></b></td>
                 </tr>--%>
                 <tr>
                     <td>MR. No&nbsp;</td>
                       <td>:&nbsp;</td>
                       <td><b><asp:Label ID="lblMRCode" runat="server" Text=""></asp:Label></b></td>
                 </tr>
                     <tr>
                 <td>Created On&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblCreatedDate" runat="server" Text=""></asp:Label></b></td>
             </tr>
            </table>
          </address>
        </div>
        <div class="col-xs-7 invoice-col">
           <address>
               <table>
                   <tr>
                 <td>Department:&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblSubDepName" runat="server" Text=""></asp:Label></b></td>
                 </tr>
                   <tr>
                 <td>Requested Date:&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblRequestedDate" runat="server" Text=""></asp:Label></b></td>
                 </tr>
                 <tr>
                    <td>Requester Name:&nbsp;</td>
                    <td>:&nbsp;</td>
                    <td><b><asp:Label ID="lblRequesterName" runat="server" Text=""></asp:Label></b></td>
                 </tr>
                </table>
          </address>
        </div>
 
      
      </div>
   
   <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
        <asp:GridView ID="gvMRView" GridLines="None" runat="server" CssClass="table table-responsive tbldetail"  AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
        <Columns>
            <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
			<asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category"/>
			<asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName"  HeaderText="Sub-Category"  />
			<asp:BoundField ItemStyle-Width="150px" DataField="ItemName"  HeaderText="Item"  />
			<asp:BoundField ItemStyle-Width="150px" DataField="Description"  HeaderText="Description"  /> 
			<asp:BoundField ItemStyle-Width="150px" DataField="RequestedQty"  HeaderText="Requested Qty"  /> 
             <asp:TemplateField HeaderText="Replacement">
              <ItemTemplate>
                  <asp:Label runat="server" Text='<%# Eval("Replacement").ToString() =="1" ? "Yes":"No" %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>

      <br />
      <div id="tablesBom">
      
      </div>

       <div class="row no-print">
        <div class="col-xs-12">
          <button  class="btn btn-success" onclick="window.print();"><i class="fa fa-print" ></i> Print</button>
        </div>

      </div>
    </div> 
    <div id="model_table"  >

</div>
    </form>

   <script type="text/javascript">
     
   </script>
    
</asp:Content>
