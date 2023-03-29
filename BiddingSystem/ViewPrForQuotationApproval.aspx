<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewPrForQuotationApproval.aspx.cs" Inherits="BiddingSystem.ViewPrForQuotationApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
	  <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
 <section class="content-header">
	  <h1>
	   Supplier Quotation Approval
		<small></small>
	  </h1>
	  <ol class="breadcrumb">
		<li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
		<li class="active">Quotation Approval</li>
	  </ol>
	</section> 
	<br />
		<form id="Form1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
	<asp:UpdatePanel ID="Updatepanel1" runat="server">
		<ContentTemplate>
			<section class="content">
	  <!-- SELECT2 EXAMPLE -->
	  <div class="box box-info" id="panelPurchaseRequset" runat="server">
		<div class="box-header with-border">
		  <h3 class="box-title" >Quotation Selected Purchase Requests</h3>

		  <div class="box-tools pull-right">
			<button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
			<button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
		  </div>
		</div>
		<!-- /.box-header -->
			  <div class="box-body">
					<div class="box box-body">
				<div class="box-body pull-left bg-teal-gradient" >
					
			   <div class="input-group input-group-sm pull-right" style="width: 150px; margin-left: 10px">
										<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-sm btn-primary" style="margin-right:15px"  Text="Search" OnClick="btnSearch_Click"></asp:Button>
					<asp:Button runat="server" ID="btnCancel" CssClass="btn btn-sm btn-danger" style="margin-right:15px"  Text="Cancel" OnClick="btnCancel_Click"></asp:Button>
									</div> 

				<div class="input-group input-group-sm pull-right col-sm-4" style="width: 150px; margin-left: 10px">
							<asp:TextBox ID="txtSearch" CssClass="form-control pull-right" Enabled="false" runat="server" placeholder="Search"></asp:TextBox>
										
						</div>                                          
						<div class="input-group input-group-sm pull-right" style="width: 150px; margin-left: 10px">
									  
							<asp:DropDownList ID="ddlAdvancesearchitems" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAdvancesearchitems_SelectedIndexChanged" type="date" class="form-control pull-right">
									<asp:ListItem Text="Select type to search" Value="0"></asp:ListItem>
								<asp:ListItem Text="PR Code" Value="1"></asp:ListItem>
								<asp:ListItem Text="PR Creator" Value="2"></asp:ListItem>
								<asp:ListItem Text="Date Of Request" Value="3"></asp:ListItem>
							</asp:DropDownList>
						</div>
						<div class="input-group input-group-sm pull-right" style="width: 150px; margin-left: 10px">
									  
							<asp:DropDownList ID="ddlSupplier" runat="server" type="date" class="form-control pull-right">
							</asp:DropDownList>
						</div>
							<div class="input-group input-group-sm pull-right" style="width: 150px; margin-left: 10px">
									  
							<asp:DropDownList ID="ddlCategories" runat="server" type="date" class="form-control pull-right">
							</asp:DropDownList>
						</div>
								
						<div class="input-group input-group-sm pull-right" style="width: 80px; margin-left: 10px">
							<label class="form-control">Search By</label>
						</div>
					</div>
				</div>
		  <div class="row">
			<div class="col-md-12">
			<div class="table-responsive">
				<asp:GridView runat="server" ID="gvPurchaseRequest" GridLines="None" CssClass="table table-responsive" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
					AutoGenerateColumns="false" EmptyDataText="No PR Found">
					<Columns>
						<asp:BoundField DataField="PrId"  HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="PrCode"  HeaderText="PR Code" />
						<asp:BoundField DataField="DepartmentId"  HeaderText="DepartmentId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="DateOfRequest"  HeaderText="Date Of Request" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
						<asp:BoundField DataField="QuotationFor"  HeaderText="Quotation For" />
						<asp:BoundField DataField="OurReference"  HeaderText="OurReference" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="RequestedBy"  HeaderText="RequestedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="CreatedDateTime"  HeaderText="PR Created Date"  DataFormatString="{0:dd/MM/yyyy hh:mm tt}"/>
						<asp:BoundField DataField="CreatedBy"  HeaderText="CreatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="UpdatedDateTime"  HeaderText="UpdatedDateTime" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="UpdatedBy"  HeaderText="UpdatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="IsActive"  HeaderText="IsActive" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="PrIsApproved"  HeaderText="PrIsApproved" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="PrIsApprovedOrRejectedBy"  HeaderText="PrIsApprovedOrRejectedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="PrIsApprovedOeRejectDate"  HeaderText="PrIsApprovedOeRejectDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:TemplateField HeaderText="Action">
							<ItemTemplate>
								<asp:LinkButton runat="server" ID="lbtnView" Text="View Bids" OnClick="btnView_Click"></asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
				</div>
			</div>         
		  </div>
		 
		</div>
		<!-- /.box-body -->
	  </div>
	  <!-- /.box -->
	</section>
		</ContentTemplate>
	</asp:UpdatePanel>
	</form>


</asp:Content>
