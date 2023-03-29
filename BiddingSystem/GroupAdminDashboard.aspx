<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="GroupAdminDashboard.aspx.cs" Inherits="BiddingSystem.GroupAdminDashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    
    <section class="content-header">
      <h1>
       Dashboard
        <small>Group Admin</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="GroupAdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Dashboard</li>
      </ol>
    </section>
    <form id="form1" runat="server">
    <!-- Main content -->
        <div class="row" style="padding-top:20px;padding-left:400px">
    <div class=" col-sm-6">
          <div class="info-box">
            <span class="info-box-icon bg-success"><i class="fa fa-bank" style=" margin-top: 24px; "></i></span>

            <div class="info-box-content">
               <div class="form-group">
                <label for="exampleInputEmail1"  style="font-weight:bold;display:inline-block">Select A Company</label>
            
                <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" >
                </asp:DropDownList> 
          </div>
                </div>
            </div>
          </div>
          
        </div>
          <hr />
        <asp:Panel ID="PanelDashboaerd" runat="server" Enabled="false"  Visible="false">

    <section class="content">
                  <div class="row">

        <div class="col-md-4 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-red"><i class="fa fa-user" style=" margin-top: 24px; "></i></span>

            <div class="info-box-content">
              <span class="info-box-text"  style="font-weight:bold;"><a href="CompanyUpdatingAndRatingSupplier.aspx">Company Users</a></span>
              <span class="info-box-number"><h3 id="noOfCompanyUsers"></h3></span>
            </div>
          </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-aqua"><i class="fa fa-users" style=" margin-top: 24px; "></i></span>

            <div class="info-box-content">
              <span class="info-box-text" style="font-weight:bold;"><a href="CompanyUpdatingAndRatingSupplier.aspx">NO Of Suppliers</a></span>
              <span class="info-box-number"><h3 id="noOfSupplier"></h3></span>
            </div>
          </div>
        </div>
       
        <!-- fix for small devices only -->
        <div class="clearfix visible-sm-block"></div>

        <div class="col-md-4 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-yellow"><i class="fa fa-support" style=" margin-top: 24px; "></i></span>

            <div class="info-box-content">
             <span class="info-box-text" style="font-weight:bold;"><a href="#">Success Transaction</a></span>
              <span class="info-box-number"><h3 id="teste">0</h3></span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>
        <!-- /.col -->
        <!-- /.col -->
      </div>

            <div class="row">
                <div class="col-sm-6 col-md-4">
                <div class="box box-solid">
                    <div class="box-body" style="height: 275px">
                        <h4 style="background-color:#3c8dbc;color:#fff;border-radius:5px; font-weight:bold; font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                            PURCHASE REQUISTION
                        </h4>
                        <div class="media">
                            <div class="media-left">
                            </div>
                            <div class="media-body">
                                <br />
                                <div class="clearfix">
                                     <table>
                                          <tr>
                                              <td><h5 style="display:inline;">Total&nbsp;PR</h5></td>
                                              <td>&nbsp;<h3  style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="totalPr"></h3></td>
                                              <%--<td style="width:100%"><p class="pull-right" style="display:inline;" ><a href="CompanyViewTotalPR.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>--%>
                                           </tr>

                                           <tr>
                                              <td><h5 style="display:inline;">Pending&nbsp;PR</h5></td>
                                              <td>&nbsp;<h3 style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="pendingPR"></h3></td>
                                              <td  style="width:100%"><p class="pull-right" style="display:inline;"  ><a href="CustomerPRView.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>
                                           </tr>
                                         <tr>
                                              <td><h5 style="display:inline;">Approved&nbsp;PR</h5></td>
                                              <td>&nbsp;<h3 style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="ApprovePr"></h3></td>
                                              <td  style="width:100%"><p class="pull-right" style="display:inline;"  ><a href="CompanyViewApprovedPR.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>
                                           </tr>
                                         <tr>
                                              <td><h5 style="display:inline;">Rejected&nbsp;PR</h5></td>
                                              <td>&nbsp;<h3 style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="RejectePr"></h3></td>
                                              <td  style="width:100%"><p class="pull-right" style="display:inline;"  ><a href="CompanyViewRejectedPR.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>
                                           </tr>
                                     </table>
                                </div>  
                            </div>
                        </div>
                </div>
                </div>
                </div>

                <div class="col-sm-6 col-md-4">
                <div class="box box-solid">
                    <div class="box-body" style="height: 275px">
                        <h4 style="background-color:#3c8dbc;color:#fff;border-radius:5px;font-weight:bold;  font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                           BIDS
                        </h4>
                        <div class="media">
                            <div class="media-left">
                            </div>
                            <div class="media-body">
                                <br />
                               <div class="clearfix">

                                     <table>
                                          <tr>
                                           <td><h5 style="display:inline;">Total&nbsp;Bids</h5></td>
                                           <td>&nbsp;<h3 style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="totalBids"></h3></td>
                                           <%--<td style="width:100%">  <p class="pull-right" style="display:inline;"  ><a href="#" class="btn btn-success btn-sm ad-click-event">More Info>></a></p></td>--%>
                                           </tr>
                                          
                                           <tr>
                                           <td><h5 style="display:inline;">Pending&nbsp;Approval&nbsp;Bids</h5></td>
                                           <td>&nbsp;<h3 style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="submittedBids"></h3></td>
                                           <td style="width:100%">  <p class="pull-right" style="display:inline;"  ><a href="CompanySupplierDepViewPR.aspx" class="btn btn-success btn-sm ad-click-event">More Info>></a></p></td>
                                           </tr>
                                          <tr>
                                           <td><h5 style="display:inline;">Bids&nbsp;Opening&nbsp;Approval</h5></td>
                                           <td>&nbsp;<h3 style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="openingApprovalBids"></h3></td>
                                           <td style="width:100%">  <p class="pull-right" style="display:inline;"  ><a href="ViewPrForBidApproval.aspx" class="btn btn-success btn-sm ad-click-event">More Info>></a></p></td>
                                           </tr>
                                       
                                          <tr>
                                           <td> <h5 style="display:inline;">In&nbsp;Progeress&nbsp;Bids</h5></td>
                                           <td>&nbsp;<h3 style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="progressBids"></h3></td>
                                           <td  style="width:100%"> <p class="pull-right" style="display:inline;"  ><a href="CompanyMonitorBids.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>
                                           </tr>

                                          <tr>
                                           <td><h5 style="display:inline;">Closed&nbsp;Bids</h5></td>
                                           <td>&nbsp;<h3 style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="closedBidd"></h3></td>
                                           <td  style="width:100%"><p class="pull-right" style="display:inline;"  ><a href="CompanyClosedBids.aspx" class="btn btn-success btn-sm ad-click-event">More Info>></a></p></td>
                                           </tr>
                                           <%--<tr>
                                           <td><h5 style="display:inline;">Rejected&nbsp;Bids</h5></td>
                                           <td>&nbsp;<h3 style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="rejectedBid"></h3></td>
                                           <td  style="width:100%"><p class="pull-right" style="display:inline;"  ><a href="#" class="btn btn-success btn-sm ad-click-event">More Info>></a></p></td>
                                           </tr>--%>
                                      </table>
                                </div>
                              
                            </div>
                        </div>
                </div>
                </div>
                </div>
    
                <div class="col-sm-6 col-md-4">
                <div class="box box-solid">
                    <div class="box-body box-warning" style="height: 275px">
                        <h4 style="background-color:#3c8dbc;color:#fff; border-radius:5px;font-weight:bold;  font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                            PURCHASE ORDER
                        </h4>
                        <div class="media">
                            <div class="media-left">
                            </div>
                            <div class="media-body">
                                <br />
                                <div class="clearfix">
                                    <table>
                                    
                                        <tr>
                                            <td> <h5 style="display:inline;">Total&nbsp;PO</h5></td>
                                            <td>&nbsp;<h3  style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="totalPO"></h3></td>
                                            <td style="width:100%"> <p class="pull-right" style="display:inline;" > <a href="CusromerPOView.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>
                                         <td><br /></td>
                                        </tr>
                                         <tr>
                                            <td>  <h5 style="display:inline;">Approved&nbsp;PO</h5></td>
                                             <td>&nbsp;<h3  style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="ApprovePOs"></h3></td>
                                             <td> <p class="pull-right" style="float:right !important: display:inline;"  > <a href="CustomerViewApprovedPurchaseOrder.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>

                                        </tr>
                                         <tr>
                                            <td><h5 style="display:inline;">Rejected&nbsp;PO</h5></td>
                                             <td>&nbsp;<h3  style="display:inline; background-color:#e4ecef;border-radius:50%; padding:0px 9px;" id="RejectePO"></h3></td>
                                             <td> <p class="pull-right" style="display:inline;"  ><a href="CustomerViewRejectedPurchaseOrder.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>
                                        </tr>
                                    </table>
                                    
                                </div>
                           
                            </div>
                        </div>
                </div>
                </div>
                </div>
     
        </div>
         
            <div class="row">
            <div class="col-sm-9 col-md-6">
                <div class="box box-solid">
                    <div class="box-body">
                        <h4 style="background-color:#3c8dbc;color:#fff;border-radius:5px;font-weight:bold;  font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                           PR CHART
                        </h4>
                        <div class="media">
                            <div class="media-left">
                            </div>
                            <div class="media-body">
                                <br />
                               <div class="clearfix">

                                     <asp:Chart ID="chrtPR" runat=server Height="500px" Width="500px"></asp:Chart> 
                                </div>
                              
                            </div>
                        </div>
                </div>
                </div>
                </div>
                <div class="col-sm-9 col-md-6">
                <div class="box box-solid">
                    <div class="box-body">
                        <h4 style="background-color:#3c8dbc;color:#fff;border-radius:5px;font-weight:bold;  font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                           BIDS CHART
                        </h4>
                        <div class="media">
                            <div class="media-left">
                            </div>
                            <div class="media-body">
                                <br />
                               <div class="clearfix">
                               <asp:Chart ID="chrtBid" runat=server Height="500px" Width="500px"></asp:Chart> 
                                </div>
                              
                            </div>
                        </div>
                </div>
                </div>
                </div>
      <div class="col-md-12">
      <asp:Button ID="btnExpierBid" runat="server" Text="Expire Bids"  CssClass="btn btn-warning"
              onclick="btnExpierBid_Click"></asp:Button>
      </div>
      <%-- <asp:Button ID="btnMakeId" runat="server" Text="Update"  CssClass="btn btn-warning"
              onclick="btnMakeId_Click"></asp:Button>--%>
      </div>
  

          <div class="row" style="text-align:center;">
            <img src="AdminResources/images/load.gif" id="loadingImage" style="height:100px;width:100px;" />
        </div>

      <br />
       <div class="row">
      <div class="col-md-12">
      <asp:Label ID="lblBiddingExpireMsg" runat="server" Text="" style="color:Green; font-weight:bold;"></asp:Label>
      </div>
      </div>
      <!-- /.row -->
      </section>
            </asp:Panel>
      </form>

  <script type="text/javascript">
      $(document).ready(function () {
          if (<%=ComapnyId%> != "0") {

         
          getMainDetails();
          getBidDetails();
          getPrDetails();
          getPODetails();
          }
      });

      function getMainDetails() {
          var postData = JSON.stringify({
          });
          $.ajax({
              type: "POST",
              url: "GroupAdminDashboard.aspx/GetMainSummary",
              data: postData,
              contentType: "application/json; charset=utf-8",
              success: function (result) {
                  response = result.d;
                  if (response.length > 0) {

                      for (var i = 1; i <= response.length; i++) {

                          var field = response[i - 1].split('~');
                          if (field[0] != "") {
                              document.getElementById('noOfCompanyUsers').innerHTML = field[0];
                          }
                          if (field[1] != "") {
                              document.getElementById('noOfSupplier').innerHTML = field[1];
                          }

                          
                      }
                  }
                  else {
                      alert('Emty value');
                  }
              },
          });
      }

      function getPrDetails()
      {
          var postData = JSON.stringify({
          });
          $.ajax({
              type: "POST",
              url: "GroupAdminDashboard.aspx/GetPRSummary",
              data: postData,
              contentType: "application/json; charset=utf-8",
              success: function (result) {
                  response = result.d;
                  if (response.length > 0) {

                      for (var i = 1; i <= response.length; i++) {

                          var field = response[i - 1].split('~');
                          if (field[0] != "") {
                              document.getElementById('totalPr').innerHTML = field[0];
                          }
                          if (field[1] != "") {
                              document.getElementById('ApprovePr').innerHTML = field[1];
                          }
                          if (field[2] != "") {
                              document.getElementById('RejectePr').innerHTML = field[2];
                          }
                          if (field[3] != "") {
                              document.getElementById('pendingPR').innerHTML = field[3];
                          }
                          
                      }
                  }
                  else {
                      alert('Emty value');
                  }
              },
          });
      }

      function getBidDetails() {
          var postData = JSON.stringify({
          });
          $.ajax({
              type: "POST",
              url: "GroupAdminDashboard.aspx/GetBidSummary",
              data: postData,
              contentType: "application/json; charset=utf-8",
              success: function (result) {
                  response = result.d;
                  if (response.length > 0)
                  {
                     
                      for (var i = 1; i <= response.length; i++) {

                          var field = response[i - 1].split('~');
                          if (field[0] != "") {
                           
                              document.getElementById('totalBids').innerHTML = field[0];
                          }
                          if (field[1] != "") {
                              document.getElementById('submittedBids').innerHTML = field[1];
                         
                          }
                           if (field[2] != "") {
                           
                              document.getElementById('progressBids').innerHTML = field[2];
                          }
                          if (field[3] != "") {
                              document.getElementById('closedBidd').innerHTML = field[3];
                         
                          }
//                          if (field[4] != "") {
//                              document.getElementById('rejectedBid').innerHTML = field[4];
//                         
//                          }
                          if (field[4] != "") {
                              document.getElementById('openingApprovalBids').innerHTML = field[4];
                         
                          }
                         
                      }
                  }
                  else {
                      alert('Emty value');
                  }
              },
          });
      }

      function getPODetails() {
          var postData = JSON.stringify({
          });
          $.ajax({
              type: "POST",
              url: "GroupAdminDashboard.aspx/GetPOSummary",
              data: postData,
              contentType: "application/json; charset=utf-8",
              success: function (result) {
                  response = result.d;
                  if (response.length > 0) {

                      for (var i = 1; i <= response.length; i++) {

                          var field = response[i - 1].split('~');
                          if (field[0] != "") {
                              document.getElementById('totalPO').innerHTML = field[0];
                          }
                          if (field[1] != "") {
                              document.getElementById('ApprovePOs').innerHTML = field[1];
                          }
                          if (field[2] != "") {
                              document.getElementById('RejectePO').innerHTML = field[2];
                          }
                      }
                  }
                  else
                  {
                      alert('Emty value');
                  }
              },

              complete: function () {
                  $('#loadingImage').hide();
              }
          });
      }
  </script>


</asp:Content>
