<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="PendingManualBid.aspx.cs" Inherits="BiddingSystem.PendingManualBid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
<script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>

    <form id="Form1" runat="server">
    <section class="content-header">
    <h1>
       Monitor Manual Bids
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Monitor Manual Bids</li>
      </ol>
    </section>
    <br />


     <section class="content">
      <div class="box box-info" id="panelPurchaseRequset" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >In-Progress Manual-Bids</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
         <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gvPendingBids" runat="server" CssClass="table table-responsive tablegv"
        GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Bids Found">
        <Columns>
            <asp:BoundField DataField="PrID" HeaderText="PrID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            
            <asp:BoundField DataField="PrCode" HeaderText="PR Code" />
            <asp:BoundField DataField="StartDate" HeaderText="Bid Start Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
            <asp:BoundField DataField="EndDate" HeaderText="Bid End Date"  DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
            <asp:BoundField DataField="Total" HeaderText="Participants" />
            <asp:BoundField DataField="pending" HeaderText="Pending Bids" />
            <asp:TemplateField HeaderText="Send Reminder SMS" >
            <ItemStyle HorizontalAlign="Center"></ItemStyle>
              <ItemTemplate>
                  <asp:ImageButton  ID="btnEdit" ImageUrl="~/images/sms.png"  style="width:26px;height:20px;text-align:center;" runat="server" />
              </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
        <!-- /.box-body -->
      </div>

    <%--*******************--%>
    </section>
    <%-- ************* --%>
  </form>
</asp:Content>
