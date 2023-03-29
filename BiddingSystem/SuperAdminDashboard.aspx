<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSuperAdmin.Master" AutoEventWireup="true" CodeBehind="SuperAdminDashboard.aspx.cs" Inherits="BiddingSystem.SuperAdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <section class="content-header">
      <h1>
       Dashboard
        <small>Admin</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="SuperAdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Dashboard</li>
      </ol>
    </section>
    <form id="form1" runat="server">
    <!-- Main content -->
    <section class="content">
      <!-- Info boxes -->
      <div class="row">
        <div class="col-md-3 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-aqua"><i class="fa fa-copy" style=" margin-top: 24px; "></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Total PR</span>
              <span class="info-box-number">150<small></small></span>
              <span class="info-box-number"><asp:LinkButton ID="LinkButton4" runat="server">More info >></asp:LinkButton></span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-3 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-red"><i class="fa fa-copy" style=" margin-top: 24px; "></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Pending PR</span>
              <span class="info-box-number">50</span>
              <span class="info-box-number"><asp:LinkButton ID="LinkButton3" runat="server">More info >></asp:LinkButton></span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->

        <!-- fix for small devices only -->
        <div class="clearfix visible-sm-block"></div>

        <div class="col-md-3 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-green"><i class="fa fa-copy" style=" margin-top: 24px; "></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Completed Bids</span>
              <span class="info-box-number">20</span>
              <span class="info-box-number"><asp:LinkButton ID="LinkButton1" runat="server">More info >></asp:LinkButton></span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <div class="col-md-3 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-yellow"><i class="fa fa-copy" style=" margin-top: 24px; "></i></span>

            <div class="info-box-content">
              <span class="info-box-text">Pending Bids</span>
              <span class="info-box-number">120</span>
              <span class="info-box-number"><asp:LinkButton ID="LinkButton2" runat="server">More info >></asp:LinkButton></span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
      </section>
      </form>
</asp:Content>
