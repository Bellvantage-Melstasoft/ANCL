<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerPRView.aspx.cs" Inherits="BiddingSystem.CustomerPRView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
	  <script src="AdminResources/js/jquery1.8.min.js"></script>
      <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>

     <style type="text/css">
        .ChildGrid td {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
        }

        .ChildGrid th {
            color: White;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #67778e !important;
            color: white;
        }

        .ChildGridTwo td {
            background-color: #dcd4d4 !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
        }

        .ChildGridTwo th {
            color: White;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #56585b !important;
            color: white;
        }
    </style>
     <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/moment.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
     <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" type="text/css" />
<section class="content-header">
	<h1>
	  Confirm Purchase 
		<small></small>
	  </h1>
	  <ol class="breadcrumb">
		<li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
		<li class="active">Confirm Purchase Requisition</li>
	  </ol>
	</section>
	<br />

	<form runat="server">
	<asp:ScriptManager runat="server"></asp:ScriptManager>
	<asp:UpdatePanel ID="Updatepanel1" runat="server">
		<ContentTemplate>
			<section class="content" style="padding-top:0px">
	  <!-- SELECT2 EXAMPLE -->
	  <div class="box box-info" id="panelPurchaseRequset" runat="server">
		<div class="box-header with-border">
		  <h3 class="box-title" >View Purchase Requests</h3>

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
									  
							<asp:DropDownList ID="ddlDepartments" runat="server" type="date" class="form-control pull-right">
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
				 <asp:GridView runat="server" ID="gvPurchaseRequest"
                    HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                    DataKeyNames="PrId" GridLines="None" CssClass="table table-responsive"
                    OnRowDataBound="gvPurchaseRequest_RowDataBound" AutoGenerateColumns="false"
                    EmptyDataText="No PR Found" AllowPaging="true">
                    <PagerStyle HorizontalAlign = "Center" CssClass = "GridPager" />
					<Columns>
                        <asp:TemplateField HeaderText="PR Item">
                            <ItemTemplate>
                                <img alt="" style="cursor: pointer;margin-top: -6px;"
                                    src="images/plus.png" />
                                <asp:Panel ID="pnlPrDetails" runat="server"
                                    Style="display: none">
                                   <asp:GridView ID="gvPrDetails" runat="server"
                                    CssClass="table table-responsive ChildGrid"
                                    GridLines="None" AutoGenerateColumns="false"
                                    DataKeyNames="PrdId"
                                    Caption="Items in Purchase Request" OnRowDataBound="gvPrDetails_RowDataBound" EmptyDataText="No Item Found">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Log">
                                                <ItemTemplate>
                                                    <img alt=""
                                                        style="cursor: pointer;margin-top: -6px;"
                                                        src="images/plus.png" />
                                                    <asp:Panel ID="pnlStatusLog" runat="server"
                                                        Style="display: none">
                                                        <asp:GridView ID="gvStatusLog"
                                                            runat="server"
                                                            CssClass="table table-responsive ChildGridTwo"
                                                            GridLines="None"
                                                            AutoGenerateColumns="false"
                                                            DataKeyNames="PrdId"
                                                            Caption="Purchase Request Item Log" EmptyDataText="No Log Found">
                                                            <Columns>
                                                                <asp:BoundField
                                                                    DataField="UserName"
                                                                    HeaderText="Logged By" />
                                                                <asp:BoundField
                                                                    DataField="LoggedDate"
                                                                    HeaderText="Logged Date and Time" DataFormatString='<%$ appSettings:dateTimePattern %>'/>

                                                                <asp:TemplateField  HeaderText="Current Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                                            Text="PR APPROVED" CssClass="label label-success"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                                            Text="PR REJECTED" CssClass="label label-danger" />
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                                            Text="BID CREATED" CssClass="label label-info" />
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "4" ? true : false %>'
                                                                            Text="BID ACCEPTED & OPENED" CssClass="label label-success" />
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "5" ? true : false %>'
                                                                            Text="BID REJECTED" CssClass="label label-danger" />
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "6" ? true : false %>'
                                                                            Text="QUOTATION SELECTED"  CssClass="label label-info"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "7" ? true : false %>'
                                                                            Text="QUOTATION RECOMMENDED"  CssClass="label label-success"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "8" ? true : false %>'
                                                                            Text="QUOTATION REJECTED AT RECOMMENDATION" CssClass="label label-danger" />
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "9" ? true : false %>'
                                                                            Text="QUOTATION APPROVED"  CssClass="label label-success"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "10" ? true : false %>'
                                                                            Text="QUOTATION REJECTED AT APPROVAL" CssClass="label label-danger" />
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "11" ? true : false %>'
                                                                            Text="PO CREATED"  CssClass="label label-info"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "12" ? true : false %>'
                                                                            Text="PO APPROVED"  CssClass="label label-success"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "13" ? true : false %>'
                                                                            Text="PO REJECTED" CssClass="label label-danger" />
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "14" ? true : false %>'
                                                                            Text="GRN CREATED"  CssClass="label label-info"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "15" ? true : false %>'
                                                                            Text="GRN APPROVED"  CssClass="label label-success"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("Status").ToString() == "16" ? true : false %>'
                                                                            Text="GRN REJECTED" CssClass="label label-danger" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PrdId" HeaderText="PRDId"
                                                HeaderStyle-CssClass="hidden"
                                                ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="CategoryId"
                                                HeaderText="Item Id"
                                                HeaderStyle-CssClass="hidden"
                                                ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="CategoryName"
                                                HeaderText="Category Name" />
                                            <asp:BoundField DataField="SubCategoryId"
                                                HeaderText="Item Id"
                                                HeaderStyle-CssClass="hidden"
                                                ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="SubCategoryName"
                                                HeaderText="Sub-Category Name" />
                                            <asp:BoundField DataField="ItemId"
                                                HeaderText="Item Id"
                                                HeaderStyle-CssClass="hidden"
                                                ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="ItemName"
                                                HeaderText="Item Name" />
                                            <asp:BoundField DataField="ItemDescription"
                                                HeaderText="Description" />
                                            <asp:BoundField DataField="ItemQuantity"
                                                HeaderText="Quantity" />
                                            <asp:BoundField DataField="EstimatedAmount"
                                                HeaderText="Estimated Price" />
                                            <asp:TemplateField  HeaderText="Current Status" >
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "0" ? true : false %>'
                                                        Text="PR APPROVAL PENDING" CssClass="label label-warning" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "1" ? true : false %>'
                                                        Text="BID CREATION" CssClass="label label-warning" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "2" ? true : false %>'
                                                        Text="PR REJECTED" CssClass="label label-danger" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "3" ? true : false %>'
                                                        Text="BID PENDING ACCEPT & OPENING" CssClass="label label-warning" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "4" ? true : false %>'
                                                        Text="QUOTATION COLLECTION" CssClass="label label-warning" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "5" ? true : false %>'
                                                        Text="QUOTATION RECOMMENDATION" CssClass="label label-warning" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "6" ? true : false %>'
                                                        Text="QUOTATION APPROVAL" CssClass="label label-warning" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "7" ? true : false %>'
                                                        Text="PO CREATION" CssClass="label label-warning" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "8" ? true : false %>'
                                                        Text="PO APPROVAL" CssClass="label label-warning" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "9" ? true : false %>'
                                                        Text="GRN CREATION" CssClass="label label-warning" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "10" ? true : false %>'
                                                        Text="GRN APPROVAL" CssClass="label label-warning" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "11" ? true : false %>'
                                                        Text="COMPLETED" CssClass="label label-info" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("CurrentStatus").ToString() == "12" ? true : false %>'
                                                        Text="PROCUREMENT ENDED PRIOR COMPLETION" CssClass="label label-danger" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
						<asp:BoundField DataField="PrId"  HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="PrCode"  HeaderText="PR Code" />
                         <asp:TemplateField HeaderText="Department">
						<ItemTemplate>
							<asp:Label runat="server" ID="lbldepartmentName" Text='<%# Eval("MRNRefNumber")== null || Eval("MRNRefNumber").ToString() == "" ?"Stores":Eval("departmentName") %>'></asp:Label>
						</ItemTemplate>
						</asp:TemplateField>
<%--                    <asp:BoundField DataField="departmentName"  HeaderText="Department Name" />--%>
						<asp:BoundField DataField="DepartmentId"  HeaderText="DepartmentId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="CreatedDateTime"  HeaderText="Created On" DataFormatString='<%$ appSettings:dateTimePattern %>' />                        
						<asp:BoundField DataField="QuotationFor"  HeaderText="Requested For" />
                        <asp:BoundField DataField="RequestedBy"  HeaderText="Created By"/>
                        <asp:BoundField DataField="CreatedBy"  HeaderText="CreatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="RequiredDate"  HeaderText="Expected Date" DataFormatString='<%$ appSettings:dateTimePattern %>'/>
						<asp:BoundField DataField="OurReference"  HeaderText="OurReference" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="RequestedBy"  HeaderText="RequestedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>						
						<asp:TemplateField HeaderText="PR Type">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblprtype" Text='<%#Eval("PrTypeid").ToString()=="7"? "Stock":"Non-Stock"%>' ForeColor='<%#Eval("PrTypeid").ToString()=="7"? System.Drawing.Color.Maroon:System.Drawing.Color.Navy%>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField> 
						<asp:BoundField DataField="UpdatedDateTime"  HeaderText="UpdatedDateTime" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="UpdatedBy"  HeaderText="UpdatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="IsActive"  HeaderText="IsActive" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="PrIsApproved"  HeaderText="PrIsApproved" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="PrIsApprovedOrRejectedBy"  HeaderText="PrIsApprovedOrRejectedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="PrIsApprovedOeRejectDate"  HeaderText="PrIsApprovedOeRejectDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:TemplateField>
							<ItemTemplate>
								<asp:LinkButton runat="server" ID="lbtnView" Text="View" OnClick="btnView_Click"></asp:LinkButton>
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

	<script type="text/javascript">
        Sys.Application.add_load(function () {
        //onload set date value - used to set date if page refresh or went to backend and came
        var this1 = $(".customDate");
        if (this1.val() != undefined && this1.val() != "") {
            this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
        }
        });
        // This is used  while user change date from datepicker - some times this is handle from backend			 
        function dateChange(obj) {
            if (obj.value) {
                $(obj).attr('data-date', moment(obj.value, 'YYYY-MM-DD').format($(obj).attr('data-date-format')));
            } else {
                $(obj).attr('data-date', '');
            }
        }
    </script>
</asp:Content>
