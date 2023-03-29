<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewPRRequestsExpenseApprove.aspx.cs" Inherits="BiddingSystem.ViewPRRequestsExpenseApprove" %>

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
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" type="text/css" />

    <section class="content-header">
        <h1>
         View - Add Availability / Approve Expense PR
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Add Availability / Approve Expense PR  </li>
        </ol>
    </section>
    <br />
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content" style="padding-top: 0px;">
                    <div class="box box-info" id="panelMRNRequset" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">View Material Requests</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                        
                            <%-- add here if search bar needed--%>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvPurchaseRequest"
                                            HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                                            DataKeyNames="PrId" GridLines="None" CssClass="table table-responsive"
                                            OnRowDataBound="gvPurchaseRequest_RowDataBound" AutoGenerateColumns="false"
                                            EmptyDataText="No PR Found" AllowPaging="true" OnPageIndexChanging="gvPurchaseRequest_PageIndexChanging" PageSize="10">
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
                                                                Caption="Items in Purchase Request"  EmptyDataText="No Item Found">
                                                                <Columns>                                                                    
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
                                                                                Text="BID APPROVAL" CssClass="label label-warning" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "4" ? true : false %>'
                                                                                Text="QUOTATION COLLECTION" CssClass="label label-warning" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "5" ? true : false %>'
                                                                                Text="QUOTATION APPROVAL" CssClass="label label-warning" />
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
                                                <asp:BoundField DataField="PrId" HeaderText="PrId"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="PrCode" HeaderText="PR Code" />
                                                 <asp:TemplateField HeaderText="Department">
							                    <ItemTemplate>
								                    <asp:Label runat="server" ID="lbldepartmentName" Text='<%# Eval("MRNRefNumber")== null || Eval("MRNRefNumber").ToString() == "" ?"Stores":Eval("SubDepartmentName") %>'></asp:Label>
							                    </ItemTemplate>
						                        </asp:TemplateField>
                                               <%-- <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department" />--%>
                                                <asp:BoundField DataField="DateOfRequest" HeaderText="Date Of Request"
                                                    DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                <asp:BoundField DataField="QuotationFor" HeaderText="Requested For" />
                                                <asp:BoundField DataField="CreatedDateTime" HeaderText="PR Created Date"
                                                    DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                <asp:BoundField DataField="CreatedByName" HeaderText="Created By" />
                                                <asp:TemplateField  HeaderText="Current Status">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server"
                                                            Visible='<%# Eval("CurrentStatus").ToString() == "0" ? true : false %>'
                                                            Text="PR APPROVAL PENDING" CssClass="label label-warning"/>
                                                        <asp:Label runat="server"
                                                            Visible='<%# Eval("CurrentStatus").ToString() == "1" ? true : false %>'
                                                            Text="PR APPROVED. VIEW ITEMS FOR MORE DETAILS" CssClass="label label-info" />
                                                        <asp:Label runat="server"
                                                            Visible='<%# Eval("CurrentStatus").ToString() == "2" ? true : false %>'
                                                            Text="PR REJECTED" CssClass="label label-danger" />
                                                        <asp:Label runat="server"
                                                            Visible='<%# Eval("CurrentStatus").ToString() == "3" ? true : false %>'
                                                            Text="COMPLETED" CssClass="label label-success" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderText="Action">
                                                    <ItemTemplate>
                                                       <asp:Button runat="server" ID="btnView" CssClass="btn btn-sm btn-warning" Text="View" OnClick="btnView_Click"></asp:Button>                                                    
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


    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    
   
</asp:Content>
