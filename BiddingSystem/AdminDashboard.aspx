<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="BiddingSystem.AdminDashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <%--<script src='https://cdnjs.cloudflare.com/ajax/libs/Chart.js/1.1.1/Chart.js'></script>
    <script src="https://canvasjs.com/assets/script/canvasjs.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>--%>

    <script src='cdnjs.cloudflare.1.1.1.js'></script>
    <script src="canvasjs.min.js"></script>
    <script src="cloudflare.2.5.0.Chart.min.js"></script>

    <style>
        .head {
            display: inline-block;
            width: 40px;
            text-align: center;
            background-color: #3c8dbc;
            color: #fff;
            border-radius: 50%;
            padding:5px;
        }
        .mydropdownlist
            {
            color: #fff;
            font-size: 12px;
            padding: 5px 10px;
            border-radius: 5px;
            background-color: #3c8dbc;
            font-weight: bold;
            }

        .chart-legend li span{
         margin-top: 10px;
    display: inline-block;
    width: 12px;
    height: 12px;
    margin-right: 5px;
}

        .chart-legend li {
            display:inline-block;
   margin-right:20px;
    list-style-type: none;
}

    </style>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
     <form id="form1" runat="server">
    <section class="content-header">
      <h1>
       Dashboard
        <small>Admin</small>
          
      </h1> 
        


      <ol class="breadcrumb">
          
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Dashboard</li>
           
      </ol>
        
        <%--<label for="year">Select Year</label> --%>
        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control mydropdownlist" style="padding-top: 2px;padding-bottom: 2px;height: 22px;width: 72px;"  AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>

    </section>
   
    <!-- Main content -->
    <section class="content">
                          <div class="row">

        <div class="col-md-4 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-purple"><i class="fa fa-user" style=" margin-top: 24px; "></i></span>

            <div class="info-box-content">
              <span class="info-box-text"  style="font-weight:bold;"><a href="CompanyUpdatingAndRatingSupplier.aspx">Company Users</a></span>
              <span class="info-box-number"><h3 id="noOfCompanyUsers" runat="server"></h3></span>
            </div>
          </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12">
          <div class="info-box">
            <span class="info-box-icon bg-aqua"><i class="fa fa-users" style=" margin-top: 24px; "></i></span>

            <div class="info-box-content">
              <span class="info-box-text" style="font-weight:bold;"><a href="CompanyUpdatingAndRatingSupplier.aspx">NO Of Suppliers</a></span>
              <span class="info-box-number"><h3 id="noOfSupplier" runat="server"></h3></span>
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
              <span class="info-box-number"><h3 id="successTransaction" runat="server">0</h3></span>
            </div>
            <!-- /.info-box-content -->
          </div>
          <!-- /.info-box -->
        </div>

       <%-- <div class="col-md-4 col-sm-6 col-xs-12" id="divQuotationApproval">
          <div class="info-box">
            <span class="info-box-icon bg-red"><i class="fa fa-user" style=" margin-top: 24px; "></i></span>

            <div class="info-box-content">
              <span class="info-box-text"  style="font-weight:bold;">
                  <a id="linkOfPendingQuotationApproval" href="">Pending Quotation Approval</a></span>
                 <span class="info-box-number"><h3 id="noOfPendingQuotationApproval" runat="server">0</h3></span>
            </div>
          </div>
        </div>--%>

        <%--<div class="col-md-4 col-sm-6 col-xs-12" id="divPoApproval">
            <div class="info-box">
            <span class="info-box-icon bg-red"><i class="fa fa-user" style=" margin-top: 24px; "></i></span>

            <div class="info-box-content">
                <span class="info-box-text"  style="font-weight:bold;">
                    <a id="linkOfPendingPOApproval" href="">Pending PO Approval</a></span>
                <span class="info-box-number"><h3 id="noOfPendingPOApproval" runat="server">0</h3></span>
            </div>
            </div>
         </div>--%>
        <!-- /.col -->
        <!-- /.col -->
      </div>

         <div class="row">
            <div class="col-sm-9 col-md-6">
                <div class="box box-solid">
                    <div class="box-body">
                       <a href="ApproveMRN.aspx"> <h4 style="background-color:#3c8dbc;color:#fff;border-radius:5px;font-weight:bold;  font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                          Pending MRN Approval
                        </h4></a>
                             <br />
                    			<div class="col-md-12">
			<div class="table-responsive">
			  <asp:GridView runat="server" ID="gvMRN" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" DataKeyNames="MrnID"  EmptyDataText="No records Found">
					<Columns>
						<asp:BoundField DataField="MrnID"  HeaderText="MRN ID" />
                        <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department" />
                        <%--<asp:BoundField DataField="CreatedDate"  HeaderText="Created On" DataFormatString='<%$ appSettings:dateTimePattern %>'/>--%>
                        <asp:BoundField DataField="QuotationFor"  HeaderText="Description" />
						<asp:BoundField DataField="Fullname"  HeaderText="Created By" />						
						<asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date" DataFormatString='<%$ appSettings:datePattern %>'/>						
                          <asp:TemplateField HeaderText="MRN Type">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="lblmrntype" Text='<%#Eval("MrntypeId").ToString()=="7"? "Stock":"Non-Stock"%>' ForeColor='<%#Eval("MrntypeId").ToString()=="7"? System.Drawing.Color.Maroon:System.Drawing.Color.Navy%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
						<asp:BoundField DataField="MrntypeId"  HeaderText="MrntypeId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="ItemCatrgoryId"  HeaderText="ItemCatrgoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						 <%-- <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="template">
							<ItemTemplate>
								<asp:Button runat="server" ID="lbtnApprove" CssClass="btn btn-sm btn-info" style="margin-right:15px;padding: 5px;" Width="60px" Text="Approve" OnClick="lbtnApprove_Click" ></asp:Button>
								<asp:Button runat="server" ID="lbtnReject" CssClass="btn btn-sm btn-danger" style="padding: 5px;" Width="60px" Text="Reject" OnClick="lbtnReject_Click" ></asp:Button>
							</ItemTemplate>
						</asp:TemplateField>--%>

					</Columns>
				</asp:GridView>
				</div>
			</div>

                </div>
                </div>
                </div>
               <div class="col-sm-9 col-md-6">
                <div class="box box-solid">
                    <div class="box-body">
                        <a href="CustomerPRView.aspx">  <h4 style="background-color:#3c8dbc;color:#fff;border-radius:5px;font-weight:bold;  font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                           Pending PR Approval
                        </h4></a>
                             <br />
                    <div class="col-md-12">
			<div class="table-responsive">
				<asp:GridView runat="server" ID="gvPurchaseRequest" GridLines="None" CssClass="table table-responsive"
					AutoGenerateColumns="false" EmptyDataText="No PR Found">
					<Columns>
						<asp:BoundField DataField="PrId"  HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="PrCode"  HeaderText="PR Code" />
                        <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbldepartmentName" Text='<%# Eval("departmentName") ==null?"Stores":Eval("departmentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="QuotationFor"  HeaderText="Description" />
						<asp:BoundField DataField="DateOfRequest"  HeaderText="Date Of Request" DataFormatString='<%$ appSettings:dateTimePattern %>'/>
						<asp:TemplateField>
							<ItemTemplate>
								<asp:LinkButton runat="server" ID="lbtnViewPRPending" Text="View" OnClick="lbtnViewPRPending_Click"></asp:LinkButton>
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

        <div class="row">
            <div class="col-sm-9 col-md-6">
                <div class="box box-solid">
                    <div class="box-body">
                       <a href="CompanySupplierDepViewPR.aspx"> <h4 style="background-color:#3c8dbc;color:#fff;border-radius:5px;font-weight:bold;  font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                          Pending For Submitting of Bids
                        </h4></a>
                             <br />
                    			<div class="col-md-12">
			<div class="table-responsive">
			  <asp:GridView runat="server" ID="gvsubmitforbid" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" DataKeyNames="PrId"  EmptyDataText="No records Found">
				 <Columns>
                        <asp:BoundField DataField="PrId"  HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/><asp:BoundField DataField="PrCode"  HeaderText="PR Code" />
                        <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbldepartmentName" Text='<%# Eval("departmentName") ==null?"Stores":Eval("departmentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <asp:BoundField DataField="QuotationFor"  HeaderText="Description" />
                        <asp:BoundField DataField="DateOfRequest"  HeaderText="Date Of Request" DataFormatString='<%$ appSettings:dateTimePattern %>'/>
                       
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnViewssumitbids" Text="	Create Bid" OnClick="lbtnViewssumitbids_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
				</asp:GridView>
				</div>
			</div>

                </div>
                </div>
                </div>
               <div class="col-sm-9 col-md-6">
                <div class="box box-solid">
                    <div class="box-body">
                        <a href="SumittedMRNPRView.aspx">  <h4 style="background-color:#3c8dbc;color:#fff;border-radius:5px;font-weight:bold;  font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                           Pending for Accept & Bid Opening
                        </h4></a>
                             <br />
                    <div class="col-md-12">
			<div class="table-responsive">
				<asp:GridView runat="server" ID="gvacceptbids" GridLines="None" CssClass="table table-responsive"
					AutoGenerateColumns="false" EmptyDataText="No PR Found">
					  <Columns>
                        <asp:BoundField DataField="PrId"  HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrCode"  HeaderText="PR Code" />
                       <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbldepartmentName" Text='<%# Eval("departmentName") ==null?"Stores":Eval("departmentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:BoundField DataField="QuotationFor"  HeaderText="Description" />
                           <asp:BoundField DataField="CreatedBy"  HeaderText="CreatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="DateOfRequest"  HeaderText="Date Of Request"  DataFormatString='<%$ appSettings:dateTimePattern %>' />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnViewSumittedbids" Text="View Bids" OnClick="lbtnViewSumittedbids_Click"></asp:LinkButton>
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

        <div class="row">
            <div class="col-sm-9 col-md-6">
                <div class="box box-solid">
                    <div class="box-body">
                        <a href="CustomerApprovePO.aspx">  <h4 style="background-color:#3c8dbc;color:#fff;border-radius:5px;font-weight:bold;  font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                           Pending PO Approval
                        </h4></a>
                             <br />
                    <div class="col-md-12">
            <div class="table-responsive">
                <asp:GridView runat="server" ID="gvPurchaseOrder" EmptyDataText="No Records Found" GridLines="None" CssClass="table table-responsive"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="PoID"  HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="POCode"  HeaderText="PO Code" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="BasePr"  HeaderText="BasePr" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrCode"  HeaderText="Based PR Code" />
                        <asp:TemplateField HeaderText="Department Name">
							<ItemTemplate>
								<asp:Label runat="server" ID="lbldepartmentName" Text='<%# Eval("subdepartment") ==null?"Stores":Eval("subdepartment") %>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:BoundField DataField="Description"  HeaderText="Description" />
                        <asp:BoundField DataField="SupplierName"  HeaderText="Supplier Name" />
                        <asp:BoundField DataField="ItemCount"  HeaderText="Item Count" />                  
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnViewPoapproval" Text="Review PO" OnClick="lbtnViewPoapproval_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
            </div> 

                </div>
                </div>
                </div>
               <div class="col-sm-9 col-md-6">
                <div class="box box-solid">
                    <div class="box-body">
                     <a href="CustomerGRNView.aspx">     <h4 style="background-color:#3c8dbc;color:#fff;border-radius:5px;font-weight:bold;  font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                          Pending GRN Approval
                        </h4></a>
                             <br />
                     <div class="col-md-12">
            <div class="table-responsive">
                <asp:GridView runat="server" ID="gvGRN" EmptyDataText="No records Found" GridLines="None" CssClass="table table-responsive"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="GrnId"  HeaderText="GRN ID" />
                        <asp:BoundField DataField="PoID"  HeaderText="PO ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="POCode"  HeaderText="PO Code"/>
                        <asp:BoundField DataField="PrCode"  HeaderText="Based PR Code"/>
                        <asp:TemplateField HeaderText="Department Name">
							<ItemTemplate>
								<asp:Label runat="server" ID="lbldepartmentName" Text='<%# Eval("subdepartment") ==null?"Stores":Eval("subdepartment") %>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:BoundField DataField="Description"  HeaderText="Description" />
                        <asp:BoundField DataField="SupplierName"  HeaderText="Supplier Name" />
                        <%--<asp:BoundField DataField="GoodReceivedDate"  HeaderText="Good Received Date"  DataFormatString='<%$ appSettings:dateTimePattern %>' />--%>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnViewgrn" Text="Approval" OnClick="lbtnViewgrn_Click"></asp:LinkButton>
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
            <div class="row">
                <div class="col-sm-6 col-md-4">
                <div class="box box-solid">
                    <div class="box-body">
                        <h4 style="background-color:#3c8dbc;color:#fff;border-radius:5px; font-weight:bold; font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                            PURCHASE REQUISTION
                        </h4>
                        <div class="media">
                            <div class="media-left">
                            </div>
                            <div class="media-body">
                                <div class="clearfix">
                                     <table style="width:100%;">
                                         <tr>
                                             <td></td>
                                             <td ><h4 style="display:flex; justify-content:center; "><b>LOCAL</b></h4></td>
                                             <td ><h4 style="display:flex; justify-content:center; "><b>IMPORTS</b></h4></td>
                                         </tr>
                                          <tr>
                                              <td><h5 style="display:inline;">Total&nbsp;PR</h5></td>
                                              <td class="text-center">&nbsp;<h4  class="head"><b id="totalPr" runat="server">0</b></h4></td>
                                            <td class="text-center">&nbsp;<h4  class="head"><b id="totalPrI" runat="server">0</b></h4></td>
                                              <%--<td style="width:100%"><p class="pull-right" style="display:inline;" ><a href="CompanyViewTotalPR.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>--%>
                                           </tr>

                                           <tr>
                                              <td><h5 style="display:inline;">Pending&nbsp;PR</h5></td>
                                              <td class="text-center">&nbsp;<h4  class="head"><b id="pendingPR" runat="server">0</b></h4></td>
                                             <td class="text-center">&nbsp;<h4  class="head"><b id="pendingPRI" runat="server">0</b></h4></td>
                                              <%--<td  style="width:100%"><p class="pull-right" style="display:inline;"  ><a href="CustomerPRView.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>--%>
                                           </tr>
                                         <tr>
                                              <td><h5 style="display:inline;">Approved&nbsp;PR</h5></td>
                                              <td class="text-center">&nbsp;<h4  class="head"><b id="ApprovePr" runat="server">0</b></h4></td>
                                              <td class="text-center">&nbsp;<h4  class="head"><b id="ApprovePrI" runat="server">0</b></h4></td>
                                              <%--<td  style="width:100%"><p class="pull-right" style="display:inline;"  ><a href="CompanyViewApprovedPR.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>--%>
                                           </tr>
                                         <tr>
                                              <td><h5 style="display:inline;">Rejected&nbsp;PR</h5></td>
                                              <td class="text-center">&nbsp;<h4  class="head"><b id="RejectePr" runat="server">0</b></h4></td>
                                              <td class="text-center">&nbsp;<h4  class="head"><b id="RejectePrI" runat="server">0</b></h4></td>
                                              <%--<td  style="width:100%"><p class="pull-right" style="display:inline;"  ><a href="CompanyViewRejectedPR.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>--%>
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
                    <div class="box-body">
                        <h4 style="background-color:#3c8dbc;color:#fff;border-radius:5px;font-weight:bold;  font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                           BIDS
                        </h4>
                        <div class="media">
                            <div class="media-left">
                            </div>
                            <div class="media-body">
                               <div class="clearfix">

                                     <table style="width:100%;">
                                         
                                         <tr>
                                             <td></td>
                                             <td><h4 style="display:flex; justify-content:center; "><b>LOCAL</b></h4></td>
                                             <td><h4 style="display:flex; justify-content:center; "><b>IMPORTS</b></h4></td>
                                         </tr>
                                          <tr>
                                           <td><h5 style="display:inline;">Total</h5></td>
                                           <td class="text-center">&nbsp;<h4  class="head" ><b id="totalBids" runat="server">0</b></h4></td>
                                            <td class="text-center">&nbsp;<h4  class="head" ><b id="totalBidsI" runat="server">0</b></h4></td>
                                           <%--<td style="width:100%">  <p class="pull-right" style="display:inline;"  ><a href="#" class="btn btn-success btn-sm ad-click-event">More Info>></a></p></td>--%>
                                           </tr>
                                          
                                           <tr>
                                           <td><h5 style="display:inline;">Pending&nbsp;Bid&nbsp;Creation</h5></td>
                                           <td class="text-center">&nbsp;<h4  class="head"><b id="pendingBidCreation" runat="server">0</b></h4></td>
                                          <td class="text-center">&nbsp;<h4  class="head"><b id="pendingBidCreationI" runat="server">0</b></h4></td>
                                           <%--<td style="width:100%">  <p class="pull-right" style="display:inline;"  ><a href="CompanySupplierDepViewPR.aspx" class="btn btn-success btn-sm ad-click-event">More Info>></a></p></td>--%>
                                           </tr>
                                          <tr>
                                           <td><h5 style="display:inline;">Pending&nbsp;Approval</h5></td>
                                           <td class="text-center">&nbsp;<h4  class="head" ><b id="pendingApprovalBids" runat="server">0</b></h4></td>
                                           <td class="text-center">&nbsp;<h4  class="head" ><b id="pendingApprovalBidsI" runat="server">0</b></h4></td>
                                           <%--<td style="width:100%">  <p class="pull-right" style="display:inline;"  ><a href="ApproveForBidOpening.aspx" class="btn btn-success btn-sm ad-click-event">More Info>></a></p></td>--%>
                                           </tr>
                                       
                                          <tr>
                                           <td> <h5 style="display:inline;">In&nbsp;Progeress</h5></td>
                                           <td class="text-center">&nbsp;<h4  class="head" ><b id="inprogressBids" runat="server">0</b></h4></td>
                                                   <td class="text-center">&nbsp;<h4  class="head" ><b id="inprogressBidsI" runat="server">0</b></h4></td>
                                           <%--<td  style="width:100%"> <p class="pull-right" style="display:inline;"  ><a href="CompanyMonitorBids.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>--%>
                                           </tr>

                                          <tr>
                                           <td><h5 style="display:inline;">Closed</h5></td>
                                           <td class="text-center">&nbsp;<h4  class="head"><b id="closedBids" runat="server">0</b></h4></td>
                                           <td class="text-center">&nbsp;<h4  class="head"><b id="closedBidsI" runat="server">0</b></h4></td>
                                           <%--<td  style="width:100%"><p class="pull-right" style="display:inline;"  ><a href="CompanyClosedBids.aspx" class="btn btn-success btn-sm ad-click-event">More Info>></a></p></td>--%>
                                           </tr>
                                           <%--<tr>
                                           <td><h5 style="display:inline;">Rejected</h5></td>
                                           <td>&nbsp;<h4  class="head" id="rejectedBid"></h4></td>
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
                    <div class="box-body box-warning">
                        <h4 style="background-color:#3c8dbc;color:#fff; border-radius:5px;font-weight:bold;  font-size: 18px; text-align: center; padding: 7px 10px; margin-top: 0;">
                            PURCHASE ORDER
                        </h4>
                        <div class="media">
                            <div class="media-left">
                            </div>
                            <div class="media-body">
                                <div class="clearfix">
                                    <table style="width:100%;">
                                         
                                         <tr>
                                             <td></td>
                                             <td><h4 style="display:flex; justify-content:center; "><b>LOCAL</b></h4></td>
                                             <td><h4 style="display:flex; justify-content:center;  "><b>IMPORTS</b></h4></td>
                                         </tr>
                                        <tr>
                                           <td><h5 style="display:inline;">Total</h5></td>
                                           <td class="text-center">&nbsp;<h4 class="head" ><b id="totalPO" runat="server">0</b></h4></td>
                                          <td class="text-center">&nbsp;<h4 class="head" ><b id="totalPOI" runat="server">0</b></h4></td>
                                           <%--<td style="width:100%">  <p class="pull-right" style="display:inline;"  ><a href="ApproveForBidOpening.aspx" class="btn btn-success btn-sm ad-click-event">More Info>></a></p></td>--%>
                                           </tr>
                                       
                                          <tr>
                                           <td> <h5 style="display:inline;">Pending Approval</h5></td>
                                           <td class="text-center">&nbsp;<h4  class="head"><b  id="pendingPO" runat="server">0</b></h4></td>
                                            <td class="text-center">&nbsp;<h4 class="head" ><b id="pendingPOI" runat="server">0</b></h4></td>
                                           <%--<td  style="width:100%"> <p class="pull-right" style="display:inline;"  ><a href="CompanyMonitorBids.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>--%>
                                           </tr>
                                       
                                          <tr>
                                           <td> <h5 style="display:inline;">Approved</h5></td>
                                           <td class="text-center">&nbsp;<h4  class="head"><b  id="approvedPo" runat="server">0</b></h4></td>
                                           <td class="text-center">&nbsp;<h4 class="head" ><b id="approvedPoI" runat="server">0</b></h4></td>
                                           <%--<td  style="width:100%"> <p class="pull-right" style="display:inline;"  ><a href="CompanyMonitorBids.aspx" class="btn btn-success btn-sm ad-click-event"> More Info>></a></p></td>--%>
                                           </tr>

                                          <tr>
                                           <td><h5 style="display:inline;">Rejected</h5></td>
                                           <td class="text-center">&nbsp;<h4 class="head"><b id="rejectedPo" runat="server">0</b></h4></td>
                                            <td class="text-center">&nbsp;<h4 class="head" ><b id="rejectedPoI" runat="server">0</b></h4></td>
                                           <%--<td  style="width:100%"><p class="pull-right" style="display:inline;"  ><a href="CompanyClosedBids.aspx" class="btn btn-success btn-sm ad-click-event">More Info>></a></p></td>--%>
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
                           Age Analysis Chart
                        </h4>
                   
                        Count
                             <br />
                       <canvas id="chartContainer" style="margin-left: 10px;"></canvas>
		  <div id="js-legend" class="chart-legend"></div>

                </div>
                </div>
                </div>
                <%--<div class="col-sm-9 col-md-6">
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
                </div>--%>
      
      <%-- <asp:Button ID="btnMakeId" runat="server" Text="Update"  CssClass="btn btn-warning"
              onclick="btnMakeId_Click"></asp:Button>--%>
      </div>
  

      <br />
       <div class="row">
      <div class="col-md-12">
      <asp:Label ID="lblBiddingExpireMsg" runat="server" Text="" style="color:Green; font-weight:bold;"></asp:Label>
      </div>
      </div>
      <!-- /.row -->
      </section>
      </form>

      <script type="text/javascript">
        
        $(document).ready(function () {

            $(function () {
              
                FirsttimeCharthistory();
            });

           function FirsttimeCharthistory() {
            $.ajax({
                type: "POST",
                url: "AdminDashboard.aspx/GetMRNandPRcountList",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response1) {
                    var aData = response1.d;
                    var aLabels = aData[0];
                    var aDatasets1 = aData[1];
                    var aDatasets2 = aData[2];
                    

                    var data = {
                        labels: aLabels,
                        datasets: [{
                            label: "MRN",
                            fillColor: "#6495ED",
                            strokeColor: "#6495ED",
                            pointColor: "#207515d4",
                            pointStrokeColor: "#f7f5f5ba",
                            pointHighlightFill: "#207515d4",
                            pointHighlightStroke: "#255f1d",
                            data: aDatasets1
                        },
               
                    {
                        label: "PR",
                        fillColor: "#90EE90",
                        strokeColor: "#90EE90",
                        pointColor: "#6f3333a6",
                        pointStrokeColor: "#6f3333a6",
                        pointHighlightFill: "#fff",
                        pointHighlightStroke: "#420c0c",
                        data: aDatasets2
                    }]


                   };

                   var options = {
                        scales: {
                            xAxes: [{
                                barPercentage: 0.5,
                                barThickness: 6,
                                maxBarThickness: 8,
                                minBarLength: 2
                            
                            }]
                        }


                    };
                 
                    var ctx2 = $("#chartContainer").get(0).getContext('2d');
                    ctx2.canvas.height = 490;  
                    ctx2.canvas.width = 480; 
                    var lineChart = new Chart(ctx2).Bar(data, options);
              

                    document.getElementById('js-legend').innerHTML = lineChart.generateLegend();

                }
            });
        }

        });


</script>


</asp:Content>
