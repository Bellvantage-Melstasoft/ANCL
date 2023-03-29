<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ApproveMRN.aspx.cs" Inherits="BiddingSystem.ApproveMRN" %>
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

      .template{
            max-width: 160px;
        } 

	.ChildGrid td
		{
			background-color: #eee !important;
			color: black;
			font-size: 10pt;
			line-height:200%;
			text-align:center;
		}
		.ChildGrid th
		{
			color: White;
			font-size: 10pt;
			line-height:200%;
			padding-top: 12px;
			padding-bottom: 12px;
			text-align: center;
			background-color: #67778e !important;
			color: white;
		}
  </style>
     <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
  <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/moment.min.js"></script>
<section class="content-header" >
	<h1>
	  Approve Material Request Notes 
		<small></small>
	  </h1>
	  <ol class="breadcrumb">
		<li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
		<li class="active">Approve Material Request Notes </li>
	  </ol>
	</section>
	<br />

	<form runat="server">
	<asp:ScriptManager runat="server"></asp:ScriptManager>
	<asp:UpdatePanel ID="Updatepanel1" runat="server">
		<ContentTemplate>

			<div id="SuccessAlert" class="modal fade">
					<div class="modal-dialog">
						<div class="modal-content">
							<div class="modal-header" style="background-color: #3c8dbc;">
								<button type="button"
									class="close" data-dismiss="modal" aria-label="Close">
									<span aria-hidden="true">&times;</span>
								</button>
								<h4 class="modal-title" style="color:white; font-weight: bold;">Success</h4>
							</div>
							<div class="modal-body">
								<p id="successMessage" style="font-weight: bold; font-size: medium;"></p>
							</div>
							<div class="modal-footer">
								<span class="btn btn-info" data-dismiss="modal" aria-label="Close">OK</span>
								<%--<button id="btnOki" class="btn btn-success">OK</button>--%>
							</div>
						</div>
					</div>
				</div>

				<div id="errorAlert" class="modal fade">
					<div class="modal-dialog">
						<div class="modal-content">
							<div class="modal-header" style="background-color: #ff0000;">
								<button type="button"
									class="close" data-dismiss="modal" aria-label="Close">
									<span aria-hidden="true">&times;</span>
								</button>
								<h4 class="modal-title" style="color:white; font-weight: bold;">ERROR</h4>
							</div>
							<div class="modal-body">
								<p id="errorMessage" style="font-weight: bold; font-size: medium;"></p>
							</div>
							<div class="modal-footer">
								<span class="btn btn-danger" data-dismiss="modal" aria-label="Close">OK</span>
								<%--<button id="btnOki" class="btn btn-success">OK</button>--%>
							</div>
						</div>
					</div>
				</div>

			<section class="content" style="padding-top:0px">
	  <!-- SELECT2 EXAMPLE -->
	  <div class="box box-info" id="panelMRN" runat="server" >
		<div class="box-header with-border">
		  <h3 class="box-title" >View Approval Pending Material Requests</h3>

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
										<asp:TextBox ID="txtSearch"   CssClass="form-control pull-right" Enabled="false" runat="server" placeholder="Search"></asp:TextBox>
										
									</div>                                          
									<div class="input-group input-group-sm pull-right" style="width: 150px; margin-left: 10px">
									  
										<asp:DropDownList ID="ddlAdvancesearchitems" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAdvancesearchitems_SelectedIndexChanged" type="date" class="form-control pull-right">
											  <asp:ListItem Text="Select type to search" Value="0"></asp:ListItem>
											<asp:ListItem Text="MRN REF No" Value="1"></asp:ListItem>
											<asp:ListItem Text="MRN Creator" Value="2"></asp:ListItem>
											<asp:ListItem Text="Date Of Request" Value="3"></asp:ListItem>
										</asp:DropDownList>
									</div>
									<div class="input-group input-group-sm pull-right" style="width: 150px; margin-left: 10px;display:none">
									  
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
			  <asp:GridView runat="server" ID="gvMRN" GridLines="None" CssClass="table table-responsive"
                   AutoGenerateColumns="false" DataKeyNames="MrnID" OnRowDataBound="gvMRN_RowDataBound" EmptyDataText="No records Found"
                  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" >
					<Columns>
					<asp:TemplateField  HeaderText="MR Item">
					<ItemTemplate>
					<img alt = "" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
					<asp:Panel ID="pnlMRND" runat="server" Style="display: none">
						<asp:GridView ID="gvMRND" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
							<Columns>
								<asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category"/>
								<asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName"  HeaderText="Sub-Category"  />
								<asp:BoundField ItemStyle-Width="150px" DataField="ItemName"  HeaderText="Item"  />
								<asp:BoundField ItemStyle-Width="150px" DataField="RequestedQty"  HeaderText="Requested Qty"  /> 
								<asp:BoundField ItemStyle-Width="150px" DataField="Description"  HeaderText="Description"  /> 
							</Columns>
						</asp:GridView>
					</asp:Panel>
					</ItemTemplate>
					</asp:TemplateField>

						<asp:BoundField DataField="MrnID"  HeaderText="MRN ID" />
                        <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department" />
                        <asp:BoundField DataField="CreatedDate"  HeaderText="Created On" DataFormatString='<%$ appSettings:dateTimePattern %>'/>
                        <asp:BoundField DataField="QuotationFor"  HeaderText="Requested For" />
						<asp:BoundField DataField="Fullname"  HeaderText="Created By" />						
						<asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date" DataFormatString='<%$ appSettings:datePattern %>'/>						
                          <asp:TemplateField HeaderText="MRN Type">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="lblmrntype" Text='<%#Eval("MrntypeId").ToString()=="7"? "Stock":"Non-Stock"%>' ForeColor='<%#Eval("MrntypeId").ToString()=="7"? System.Drawing.Color.Maroon:System.Drawing.Color.Navy%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
						<asp:BoundField DataField="MrntypeId"  HeaderText="MrntypeId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="ItemCatrgoryId"  HeaderText="ItemCatrgoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						  <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="template">
							<ItemTemplate>
								<asp:Button runat="server" ID="lbtnApprove" CssClass="btn btn-sm btn-info" style="margin-right:15px;padding: 5px;" Width="60px" Text="Approve" OnClick="lbtnApprove_Click" ></asp:Button>
								<asp:Button runat="server" ID="lbtnReject" CssClass="btn btn-sm btn-danger" style="padding: 5px;" Width="60px" Text="Reject" OnClick="lbtnReject_Click" ></asp:Button>
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

