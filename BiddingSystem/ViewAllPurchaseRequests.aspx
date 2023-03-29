<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewAllPurchaseRequests.aspx.cs" Inherits="BiddingSystem.ViewAllPurchaseRequests" %>

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
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    
        <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" type="text/css" />

    <section class="content-header">
        <h1>
            View All Purchase Requests Status
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active"> View All Purchase Requests Status </li>
        </ol>
    </section>
    <br />
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content" style="padding-top:0px">
                    <div class="box box-info" id="panelPurchaseRequset" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Purchase Requests</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
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
                                                                                            HeaderText="Logged Date and Time"  DataFormatString='<%$ appSettings:dateTimePattern %>'/>
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
                                                                                                    Text="BID APPROVED" CssClass="label label-success" />
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
                                                                                                    Text="QUOTATION APPROVED"  CssClass="label label-success"/>
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "8" ? true : false %>'
                                                                                                    Text="QUOTATION REJECTED AT APPROVAL" CssClass="label label-danger" />
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
                                                <%--<asp:BoundField DataField="SubDepartmentName"  HeaderText="Department" />--%>
                                                <asp:BoundField DataField="CreatedDateTime" HeaderText="Created On"
                                                    DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                <asp:BoundField DataField="QuotationFor" HeaderText="Requested For" />
                                                <asp:BoundField DataField="CreatedByName" HeaderText="Created By" />
                                                <asp:BoundField DataField="RequiredDate" HeaderText="Expected Date"
                                                    DataFormatString='<%$ appSettings:dateTimePattern %>' />
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