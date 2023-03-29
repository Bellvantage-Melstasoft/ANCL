<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewPoForAddImportDetails.aspx.cs" Inherits="BiddingSystem.ViewPoForAddImportDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
	 <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
<section class="content-header">
	<h1>
	  View/Add Import Details
		<small></small>
	  </h1>
	  <ol class="breadcrumb">
		<li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
		<li class="active">View/Add Import Details </li>
	  </ol>
	</section>
	<br />

	<form id="Form1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
	<asp:UpdatePanel ID="Updatepanel1" runat="server">
		<ContentTemplate>
			<section class="content" style="padding-top:0px">
	  <!-- SELECT2 EXAMPLE -->
	  <div class="box box-info" id="panelPurchaseRequset" runat="server">
		<div class="box-header with-border">
		  <h3 class="box-title" >Created Purchase Orders Of Type Import</h3>

		  <div class="box-tools pull-right">
			<button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
			<button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
		  </div>
		</div>
		<!-- /.box-header -->
		<div class="box-body">
		  <div class="row">
			<div class="col-md-12">
			<div class="table-responsive">
				<asp:GridView runat="server" ID="gvPurchaseOrder" 
					HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" EmptyDataText="No Records Found" GridLines="None" CssClass="table table-responsive" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPurchaseOrder_PageIndexChanging"
					AutoGenerateColumns="false">
					<Columns>
						<asp:BoundField DataField="PoID"  HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="POCode"  HeaderText="PO Code" />
						<asp:BoundField DataField="BasePr"  HeaderText="BasePr" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<%--<asp:BoundField DataField="PrCode"  HeaderText="PR Code" />--%>
                        <asp:TemplateField HeaderText="Based PR Code">
							                        <ItemTemplate>
								                        <asp:Label runat="server" Text='<%# "PR-"+Eval("PrCode").ToString() %>'></asp:Label>
							                        </ItemTemplate>
						                        </asp:TemplateField>
						<asp:TemplateField HeaderText="Department Name">
							<ItemTemplate>
								<asp:Label runat="server" ID="lbldepartmentName" Text='<%# Eval("subdepartment") ==null?"Stores":Eval("subdepartment") %>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:BoundField DataField="Description"  HeaderText="Description" />
						 <asp:BoundField DataField="CreatedDate"  HeaderText="PO Created Date"  DataFormatString='<%$ appSettings:dateTimePattern %>'/>
						 <asp:BoundField DataField="CreatedBy"  HeaderText="PO Created By"  />
						<asp:BoundField DataField="SupplierName"  HeaderText="Supplier Name" />
						<asp:BoundField DataField="ItemCount"  HeaderText="Item Count" />                 
						<asp:TemplateField>
							<ItemTemplate>
								<asp:LinkButton runat="server" ID="lbtnView" Text="Add Import Details" OnClick="btnAddImportDetail_Click"></asp:LinkButton>
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
