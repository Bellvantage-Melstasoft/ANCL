<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="MRNInquiryForm.aspx.cs" Inherits="BiddingSystem.MRNInquiryForm" %>

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
            background-color: #f5f5f5 !important;
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
            background-color: #3C8DBC !important;
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

        .bg-teal-gradient > div{
            padding:4px;
        }
        .box-body.pull-left.bg-teal-gradient.form-inline div:last-child >input {
            margin-left: 11px;
        }
        .GridViewEmptyText{
            color:Red;
            font-weight:bold;
            font-size:14px;
        }
    </style>
     <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
      <script src="AdminResources/js/moment.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" type="text/css" />

    <section class="content-header">
        <h1>
            Material Requisition Note Inquiry
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Search Material Requisition Note </li>
        </ol>
    </section>
    <br />
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content" style="padding-top:0px">
                    <div class="box box-info" id="panelMRNRequset" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Search Material Requisition Note</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                        <div class="box-body pull-left bg-teal-gradient form-inline" >
					        <div class="input-group input-group-sm">
							    <label class="form-control">Search By</label>
						    </div>
                             <div class="input-group input-group-sm">
                                <asp:DropDownList CssClass="form-control" runat="server" ID="ddlCategories"
                                    AutoPostBack="true" >
                                </asp:DropDownList>
						    </div>	
                            <div class="input-group input-group-sm" style="display:none">									  
						        <asp:DropDownList CssClass="form-control" runat="server" ID="ddlDepartment"
                                    AutoPostBack="true" >
                                </asp:DropDownList>
						    </div>	
                             <div class="input-group input-group-sm">									  
						        <asp:DropDownList ID="ddlAdvancesearchitems" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAdvancesearchitems_SelectedIndexChanged" type="date" class="form-control pull-right">
							        <asp:ListItem Text="Select type to search" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="MRN Code" Value="1"></asp:ListItem>
							        <asp:ListItem Text="MRN Creator" Value="2"></asp:ListItem>
							        <asp:ListItem Text="Date Of Request" Value="3"></asp:ListItem>
						        </asp:DropDownList>
						    </div>
                             <div class="input-group input-group-sm" style="width: 150px;">
							    <asp:TextBox ID="txtSearch" CssClass="form-control pull-right" Enabled="false" runat="server" placeholder="Search"></asp:TextBox>
						    </div> 
			                <div class="input-group input-group-sm">
								<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-sm btn-primary"  Text="Search" OnClick="btnSearch_Click"></asp:Button>
					            <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-sm btn-danger"  Text="Cancel" OnClick="btnCancel_Click"></asp:Button>
							</div> 
					    </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvMRN"
                                            HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                                            OnRowDataBound="gvMRN_RowDataBound" DataKeyNames="MrnID"
                                            GridLines="None" CssClass="table table-responsive" OnPageIndexChanging="gvMRN_PageIndexChanging"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EmptyDataText="No MRN Found" EmptyDataRowStyle-CssClass="GridViewEmptyText">
                                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="MR Item">
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                            src="images/plus.png" />
                                                        <asp:Panel ID="pnlMrnDetails2" runat="server"
                                                            Style="display: none">
                                                            <asp:GridView ID="gvMrnDetails" runat="server"
                                                                CssClass="table table-responsive ChildGrid"
                                                                GridLines="None" AutoGenerateColumns="false"
                                                                DataKeyNames="MRND_ID" Caption="Items in Material Request"
                                                                EmptyDataText="No Item Found">
                                                                <Columns>
                                                                   <asp:BoundField DataField="Mrnd_ID" HeaderText="MRNDId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="CategoryId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                            <asp:BoundField DataField="SubCategoryId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="SubCategoryName"  HeaderText="Sub-Category Name" />
                                            <asp:BoundField DataField="ItemId"  HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="ItemName"  HeaderText="Item Name" />
                                            <asp:BoundField DataField="ItemQuantity" HeaderText="Quantity" />
                                            <asp:BoundField DataField="EstimatedAmount"  HeaderText="Estimated Price" />
                                            <asp:TemplateField  HeaderText="Current Status" >
                                                <ItemTemplate>
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("STATUS").ToString() == "0" ? true : false %>'
                                                        Text="PROCESS PENDING" CssClass="label label-warning" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("STATUS").ToString() == "1" ? true : false %>'
                                                        Text="ADDED TO PR" CssClass="label label-warning" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("STATUS").ToString() == "2" ? true : false %>'
                                                        Text="PARTIALLY ISSUED" CssClass="label label-danger" />
                                                    <asp:Label runat="server"
                                                        Visible='<%# Eval("STATUS").ToString() == "3" ? true : false %>'
                                                        Text="FULLY ISSUED" CssClass="label label-warning" />
                                                                           
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MrnID" HeaderText="MrnID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="MrnID" HeaderText="MRN Code" />
                                                  <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department" />
                                                 <asp:BoundField DataField="CreatedDate" HeaderText="Created On" DataFormatString='<%$ appSettings:dateTimePattern %>' />                                                
                                                <asp:BoundField DataField="QuotationFor" HeaderText="Requested For" />     
                                                <asp:BoundField DataField="CreatedByName" HeaderText="Created By" />      
                                                <asp:BoundField DataField="ExpectedDate" HeaderText="Expected Date" DataFormatString='<%$ appSettings:datePattern %>'/>
                                                <asp:TemplateField HeaderText="MRN Type">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblmrntype" Text='<%#Eval("MrntypeId").ToString()=="7"? "Stock":"Non-Stock"%>' ForeColor='<%#Eval("MrntypeId").ToString()=="7"? System.Drawing.Color.Maroon:System.Drawing.Color.Navy%>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderText="Current Status">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server"
                                                            Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                                            Text="MRN APPROVAL PENDING" CssClass="label label-warning"/>
                                                        <asp:Label runat="server"
                                                            Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                                            Text="MRN APPROVED. VIEW ITEMS FOR MORE DETAILS" CssClass="label label-info" />                                                      
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
