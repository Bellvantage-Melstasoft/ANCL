<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewSupplierBidBondDetails.aspx.cs" Inherits="BiddingSystem.ViewSupplierBidBondDetails" %>
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


    <section class="content-header">
        <h1>
            Update Supplier Bid Bond Details For Purchase Requests
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Approved Purchase Requests </li>
        </ol>
    </section>
    <br />
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content">
                    <div class="box box-info" id="panelPurchaseRequset" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Approved Purchase Requests</h3>

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
                                        <asp:GridView runat="server" ID="gvPurchaseRequest" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" DataKeyNames="PrId" GridLines="None" CssClass="table table-responsive" OnRowDataBound="gvPurchaseRequest_RowDataBound"
                                            AutoGenerateColumns="false" EmptyDataText="No PR Found">
                                            <Columns>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                                                        <asp:Panel ID="pnlBids" runat="server" Style="display: none">
                                                            <asp:GridView ID="gvBids" runat="server" CssClass="table table-responsive ChildGrid" OnRowDataBound="gvBids_RowDataBound"
                                                                GridLines="None" AutoGenerateColumns="false" DataKeyNames="BidId" Caption="Bids for Purchase Request">
                                                                <Columns>

                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                                                src="images/plus.png" />
                                                                            <asp:Panel ID="pnlBidItems" runat="server"
                                                                                Style="display: none">
                                                                                <asp:GridView ID="gvBidItems" runat="server"
                                                                                    CssClass="table table-responsive ChildGridTwo"
                                                                                    GridLines="None"
                                                                                    AutoGenerateColumns="false" Caption="Items in Bid">
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="BiddingItemId"
                                                                                            HeaderText="BidItemId"
                                                                                            HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                        <asp:BoundField DataField="BidId"
                                                                                            HeaderText="BidItemId"
                                                                                            HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                        <asp:BoundField DataField="PrdId"
                                                                                            HeaderText="PRDId"
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
                                                                                         <asp:TemplateField HeaderText="Quantity">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" Text='<%# decimal.Parse(Eval("Qty").ToString()).ToString() + " " + Eval("UnitShortName").ToString() %>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="EstimatedPrice"
                                                                                            HeaderText="Estimated Price" />

                                                                                        <%--<asp:TemplateField HeaderText="More Details">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="btnMoreBidItemDetails"
                                                                                                    runat="server" Text="View"
                                                                                                    OnClick="btnMoreBidItemDetails_Click" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>--%>


                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                                        HeaderStyle-CssClass="hidden"
                                                                        ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField DataField="PrId" HeaderText="PrId"
                                                                        HeaderStyle-CssClass="hidden"
                                                                        ItemStyle-CssClass="hidden" />
                                                                    <asp:TemplateField HeaderText="Bid Code"
                                                                        HeaderStyle-HorizontalAlign="Center"
                                                                        ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" Text='<%# "B"+Eval("BidCode").ToString() %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="CreatedUserName"
                                                                        HeaderText="Created By" />
                                                                    <asp:BoundField DataField="CreateDate" HeaderText="Created Date"
                                                                         DataFormatString='<%$ appSettings:datePattern %>' />
                                                                    <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                                        DataFormatString='<%$ appSettings:datePattern %>' />
                                                                    <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                                        DataFormatString='<%$ appSettings:datePattern %>' />
                                                                    <asp:TemplateField HeaderText="Bid Opened For">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" Text='<%# Eval("BidOpeningPeriod").ToString()+" Days" %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bid Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" Text='<%# Eval("BidOpenType").ToString() =="1" ? "Online":Eval("BidOpenType").ToString() =="2" ? "Manual":"Online & Manual" %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bid Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" Text='<%# Eval("IsApproved").ToString() =="0" ? "Pending":Eval("IsApproved").ToString() =="1" ? "Approved":"Rejected" %>'
                                                                                ForeColor='<%# Eval("IsApproved").ToString() =="0" ? System.Drawing.Color.DeepSkyBlue:Eval("IsApproved").ToString() =="1" ? System.Drawing.Color.Green:System.Drawing.Color.Red %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ApprovalRemarks"
                                                                        HeaderText="Remarks" NullDisplayText="-" />

                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:Button CssClass="btn btn-sm btn-primary" runat="server" ID="lbtnView"
                                                                                Text="Submit Bond Details" OnClick="btnView_Click"></asp:Button>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="PrCode" HeaderText="PR Code" />
                                                <asp:BoundField DataField="CompanyId" HeaderText="DepartmentId"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ExpectedDate" HeaderText="Date Of Request"
                                                    DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                <asp:BoundField DataField="RequiredFor" HeaderText="Quotation For" />
                                               <%-- <asp:BoundField DataField="OurReference" HeaderText="OurReference"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                             <%--   <asp:BoundField DataField="RequestedBy" HeaderText="RequestedBy"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                                <asp:BoundField DataField="CreatedDateTime" HeaderText="PR Created Date"
                                                    DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="UpdatedDateTime" HeaderText="UpdatedDateTime"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="UpdatedBy" HeaderText="UpdatedBy"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="IsActive" HeaderText="IsActive"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="PrIsApproved" HeaderText="PrIsApproved"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="PrIsApprovedOrRejectedBy" HeaderText="PrIsApprovedOrRejectedBy"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="PrIsApprovedOeRejectDate" HeaderText="PrIsApprovedOeRejectDate"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

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