<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerViewPurchaseRequisition_1.aspx.cs" Inherits="BiddingSystem.CustomerViewPurchaseRequisition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
<section class="content-header">
    <h1>
       Purchase Requisition
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">View Purchase Requisition</li>
      </ol>
    </section>
    <br />

   <section class="content">    <!-- Main content -->
    <section class="invoice">
      <!-- title row -->
      <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header">
            <i class="fa fa-copy"></i> PURCHASE REQUISITION (PR)
            <small class="pull-right">Date: 27th Apr 2018</small>
          </h2>
        </div>
        <!-- /.col -->
      </div>
      <!-- info row -->
      <div class="row invoice-info">
        <div class="col-sm-4 invoice-col">
          
          <address>
            <strong>Department</strong><br />
            User Department : Melstacorp<br />
            Our Ref. :<br />
            PR. No : PR0001<br />
            Date : 17/04/2018<br />
            User Ref : MC/00021/16/IT
          </address>
        </div>
        <!-- /.col -->
        <div class="col-sm-4 invoice-col">
          <address>
            <strong>Requester</strong><br />
             <br />
          <b>Requester Name:</b> Navaruban Nithyanantham<br />
          </address>
        </div>
        <!-- /.col -->
        <div class="col-sm-4 invoice-col" style="visibility:hidden">
          <b>Requester </b><br />
          <br />
          <b>Requester Name:</b> Navaruban Nithyanantham<br />
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->

      <!-- Table row -->
      <div class="row">
        <div class="col-xs-12 table-responsive">
          <table class="table table-striped">
            <thead>
            <tr>
              <th>Item</th>
              <th>Description</th>
              <th>Attachements</th>
              <th>Purpose</th>
              <th>Quantity Required</th>
              <th>Replacement</th>
            </tr>
            </thead>
            <tbody>
            <tr>
              <td>Laptop</td>
              <td>HP 15 Laptops (Model: HP 15 - AY103)</td>
              <td><button type="button" data-toggle="modal" data-target="#myModal" class="btn btn-small btn-primary pull-right"> View Images / Attcahements <i class=" icon-copy"></i></button></td>
              <td>Department use</td>
              <td>10</td>
              <td>No</td>
            </tr>
            </tbody>
          </table>
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->

      <div class="row">
        <!-- accepted payments column -->
        <div class="col-xs-6">
          <p class="lead">Remarks:</p>
          <p class="text-muted well well-sm no-shadow" style="margin-top: 10px;">
            HP 15 Laptops (Model: HP 15 - AY103), Free Delivery before 2018/05/25. 
            
          </p>
        </div>
        <!-- /.col -->
        <div class="col-xs-6">
          <p class="lead"></p>
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->

      <!-- this row will not appear when printing -->
      <div class="row no-print">
        

      </div>
    </section>
    </section> 
</asp:Content>
