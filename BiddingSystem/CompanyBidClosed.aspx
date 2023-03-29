<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="CompanyBidClosed.aspx.cs" Inherits="BiddingSystem.CompanyBidClosed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <style type="text/css">
        #myModal .modal-dialog
        {
            width: 90%;
        }
        #myModal2 .modal-dialog
        {
            width: 50%;
        }
    </style>
    <form id="Form1" runat="server">
    <section class="content-header">
    <h1>
       Monitor Bids
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Monitor Bids</li>
      </ol>
    </section>
    <br />
    <section class="content">
       <div class="box box-info" id="Div1" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >Closed Bid Details</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
         <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gVClosedBids" runat="server" CssClass="table table-responsive tablegv"
        GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Records Found">
        <Columns>
            <asp:BoundField DataField="PrId" HeaderText="PrID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="PrCode" HeaderText="PR Code" />
            <asp:BoundField DataField="EndDate" HeaderText="Bid Completed Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}"/>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnView" OnClick="lnkBtnEdit_Click" runat="server">View Submitted Bids</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
        <!-- /.box-body -->
      </div>
    </form>
</asp:Content>
