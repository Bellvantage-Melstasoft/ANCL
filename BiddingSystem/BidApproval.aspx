<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="BidApproval.aspx.cs" Inherits="BiddingSystem.BidApproval" %>

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
    </style>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <section class="content-header">
        <h1>
            Accept Bids & Open For Bidding
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active"> Accept Bids/Open For Bidding</li>
        </ol>
    </section>
    <br />


    <form runat="server">
        <asp:ScriptManager runat="server" ID="SM1"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
            <ContentTemplate>

                <div id="mdlItemSpecs" class="modal modal-primary fade" tabindex="-1" role="dialog" style="z-index: 3001" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->

                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close clmdlItemSpecs" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Item Specifications</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvBOMDate" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;" GridLines="None"
                                                    AutoGenerateColumns="false" EmptyDataText="No Specifications Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="Material" HeaderText="Material" />
                                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div>
                                            <label id="Label1" style="margin: 3px; color: maroon; text-align: center;"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="mdlStandardImages" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close clmdlStandardImages" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Item Photos</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvUploadedPhotos" runat="server" CssClass="table table-responsive TestTable"
                                                    EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>'
                                                                    Height="80px" Width="100px" />
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
                    </div>
                </div>

                <div id="mdlReplacementImages" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close clReplacementImg" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Replacement Photos</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvViewReplacementImages" runat="server" CssClass="table table-responsive TestTable"
                                                    EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>'
                                                                    Height="80px" Width="100px" />
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
                    </div>
                </div>

                <div id="mdlSupportiveDocs" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close clmdlSupportiveDocs" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Supportive Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvSupportiveDocuments" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" EmptyDataText="No Documents Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                                        <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Preview">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server"  href='<%#Eval("FilePath")%>'>View</asp:LinkButton>
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
                    </div>
                </div>

                <div id="mdlBidMoreDetails" class="modal modal-primary fade" tabindex="-1" role="dialog" style="z-index: 3000;" aria-hidden="true">
                    <div class="modal-dialog" style="width: 80%">
                        <!-- Modal content-->

                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Bid Details</h4>
                            </div>
                            <div class="modal-body" style="background-color: white;">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">

                                                <asp:GridView ID="gvBidMoreDetails" runat="server" CssClass="table table-responsive ChildGrid"
                                                    GridLines="None" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                        <asp:BoundField DataField="Description" HeaderText="Item Description" />
                                                        <asp:BoundField DataField="RequestedQty" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQty"
                                                                    runat="server" Text='<%# decimal.Parse(Eval("Qty").ToString()) + Eval("MeasurementShortName").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Replacement">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Replacement").ToString() =="1" ? "Yes":"No" %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText=" File Sample Provided">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSampleProvided" runat="server" Text='<%# Eval("FileSampleProvided").ToString() =="1" ? "Yes":"No" %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Replacement Images">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnViewzReplacementPhotosOfBidItem" runat="server" OnClick="btnViewzReplacementPhotosOfBidItem_Click" 
                                                                    Text="View" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Standard Images">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnViewUploadPhotosOfBidItem" runat="server" OnClick="btnViewUploadPhotosOfBidItem_Click" 
                                                                    Text="View" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Specifications">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lblViewBomOfBidItem" Text="View" OnClick="lblViewBomOfBidItem_Click" ></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Supportive Documents">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnViewSupportiveDocumentsOfBidItem" runat="server" OnClick="btnViewSupportiveDocumentsOfBidItem_Click" 
                                                                    Text="View" />
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
                    </div>
                </div>

                <div id="mdlBiddingplan" class="modal fade" role="dialog">
                    <div class="modal-dialog" style="width: 63%;">
                        <div class="modal-content">
                            <div class="modal-body">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="text-green text-bold">BIDDING PLAN</h4>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="dvBiddingplan" runat="server" CssClass="table table-responsive TestTable"
                                                        Style="border-collapse: collapse; color: black;" GridLines="None"
                                                        AutoGenerateColumns="false" EmptyDataText="No Bidding Plan Found">
                                                        <Columns>
                                                            <asp:BoundField DataField="BidId" HeaderText="PR Id"
                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="PlanId" HeaderText="Plan Id"
                                                                HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="Planname" HeaderText="Plan" />
                                                            <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                            <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                            <asp:BoundField DataField="WithTime" HeaderText="With Time" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIscompleted" runat="server" Text='<%# Eval("Iscompleted").ToString() =="1" ? "Completed":"Pending" %>'></asp:Label>
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
                        </div>
                    </div>
                </div>










                <section class="content" style="padding-top: 0px">
                    <div class="box box-info" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Purchase Request Note - Procurement Plan</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>PR No : </strong>
                                        <asp:Label ID="lblPRNo" runat="server" Text=""></asp:Label><br />
                                        <strong>Created On : </strong>
                                        <asp:Label ID="lblCreatedOn" runat="server" Text=""></asp:Label><br />
                                        <strong>Created By : </strong>
                                        <asp:Label ID="lblCreatedBy" runat="server" Text=""></asp:Label><br />
                                        <strong>PR Expected Date : </strong>
                                        <asp:Label ID="lblPrExpecteddate" runat="server" Text=""></asp:Label><br />
                                    </address>
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>Requested By : </strong>
                                        <asp:Label ID="lblRequestBy" runat="server" Text=""></asp:Label><br />
                                        <strong>Requested For : </strong>
                                        <asp:Label ID="lblRequestFor" runat="server" Text=""></asp:Label><br />
                                        <strong>Expense Type : </strong>
                                        <asp:Label ID="lblExpenseType" runat="server" Text=""></asp:Label><br />
                                        <strong>Purchase Type : </strong>
                                        <asp:Label ID="lblPurchaseType" runat="server" Text=""></asp:Label><br />
                                    </address>
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>Warehouse : </strong>
                                        <asp:Label ID="lblWarehouse" runat="server" Text=""></asp:Label><br /> 
                                        <strong>MRN No : </strong>
                                        <asp:Label ID="lblMrnId" runat="server" Text=""></asp:Label><br />   
                                        <strong>Department : </strong>
                                        <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label><br />                                      
                                    </address>
                                </div>
                            </div>
                            <div class="box box-info" id="divPrDetails" runat="server">

                                <div class="box-header with-border">
                                    <h3 class="box-title" style="margin-left:5px">Purchase Request Items</h3>
                                </div>
                                
                                <div class="box-body">
                                <div class="panel-body">
                                    <div class="co-md-12">
                                        <div class="table-responsive">
                                            
                                        <asp:GridView runat="server" ID="gvBidsForApproval" GridLines="None" CssClass="table table-responsive table-striped "
                                            AutoGenerateColumns="false" DataKeyNames="BidId" OnRowDataBound="gvBidsForApproval_RowDataBound"
                                            EmptyDataText="No records Found">
                                            <Columns>
                                                
                                                <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="CheckBox2" runat="server" onclick="CheckBoxChecked(this);" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                                                        <asp:Panel ID="pnlBidItems" runat="server" Style="display: none">
                                            <asp:GridView ID="gvBidItems" runat="server" CssClass="table table-responsive ChildGrid"
                                                GridLines="None" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="BiddingItemId" HeaderText="BidItemId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="BidId" HeaderText="BidItemId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="PrdId" HeaderText="PRDId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="CategoryId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                                    <asp:BoundField DataField="SubCategoryId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SubCategoryName" HeaderText="Sub-Category Name" />
                                                    <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:label ID="lblInventory"  type="text" runat="server" Text='<%# decimal.Parse(Eval("Qty").ToString()).ToString() + " " + Eval("UnitShortName").ToString() %>'></asp:label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="Qty" HeaderText="Quantity"/>
                                                    <asp:BoundField DataField="UnitShortName" HeaderText="Unit Measurement"/>--%>
                                                    <asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price(Unit)"/>

                                                    <asp:TemplateField HeaderText="More Details">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnMoreBidItemDetails" runat="server" Text="View" OnClick="btnMoreBidItemDetails_Click"/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField HeaderText="Bid Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# "B"+Eval("BidCode").ToString() %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:BoundField DataField="CreatedUserName" HeaderText="Created By" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="Created Date"
                                                     DataFormatString='<%$ appSettings:datePattern %>' />
                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                     DataFormatString='<%$ appSettings:datePattern %>'/>
                                                <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                     DataFormatString='<%$ appSettings:datePattern %>'/>
                                                <asp:TemplateField HeaderText="Bid Opened For">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("BidOpeningPeriod").ToString()+" Days" %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bid Type">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("BidOpenType").ToString() =="1" ? "Online":Eval("BidOpenType").ToString() =="2" ? "Manual":"Online & Manual" %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bid Status">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("IsApproved").ToString() =="0" ? "Pending":Eval("IsApproved").ToString() =="1" ? "Approved":"Rejected" %>' ForeColor='<%# Eval("IsApproved").ToString() =="0" ? System.Drawing.Color.DeepSkyBlue:Eval("IsApproved").ToString() =="1" ? System.Drawing.Color.Green:System.Drawing.Color.Red %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bidding Plan">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnBiddingPlan" runat="server" OnClick="BtnBiddingPlan_Click"
                                                                Text="View" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                </div>
                                <div class="box-footer"  id="div1" runat="server">
                            <div class="row">
                                <div class="col-xs-6 pull-right">
                            <div class="form-group">
                                                <label for="txtRemarks">Remarks</label> <asp:Label ID="RemarksLabel" runat="server" style="color:red" Text="*Mandatory When Rejecting"></asp:Label>
                                                <asp:TextBox TextMode="MultiLine" Rows="4" ID="txtRemarks"
                                                    runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                    </div>
                                
                            <div class="col-xs-12">
                            <div class="form-group">
                                
                                <asp:Button ID="btnCancel" runat="server" Text="Reject"
                                     CssClass="btn btn-danger pull-right" style="margin-left:4px" OnClick="btnCancel_Click"></asp:Button>
                                <asp:Button ID="btnSubmitBid" runat="server" ValidationGroup="btnSubmitBid" Text="Accept" CssClass="btn btn-primary pull-right" OnClick="btnSubmitBid_Click"
                                    ></asp:Button>
                            </div>
                                </div>
                                </div>
                        </div>
                            </div>

                        </div>

                    </div>
                    <div class="box box-info" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Previously Submitted Bids</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvBids" GridLines="None" CssClass="table table-responsive table-striped "
                                            AutoGenerateColumns="false" DataKeyNames="BidId" OnRowDataBound="gvBids_RowDataBound"
                                            EmptyDataText="No records Found">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                                                        <asp:Panel ID="pnlBidItems" runat="server" Style="display: none">
                                            <asp:GridView ID="gvBidItems" runat="server" CssClass="table table-responsive ChildGrid"
                                                GridLines="None" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="BiddingItemId" HeaderText="BidItemId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="BidId" HeaderText="BidItemId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="PrdId" HeaderText="PRDId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="CategoryId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                                    <asp:BoundField DataField="SubCategoryId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SubCategoryName" HeaderText="Sub-Category Name" />
                                                    <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                   <%-- <asp:BoundField DataField="Qty" HeaderText="Quantity"/>
                                                    <asp:BoundField DataField="UnitShortName" HeaderText="Unit Measurement"/>--%>
                                                   
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:label ID="lblInventory"  type="text" runat="server" Text='<%# decimal.Parse(Eval("Qty").ToString()).ToString() + " " + Eval("UnitShortName").ToString() %>'></asp:label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price(Per Unit)"/>

                                                    <asp:TemplateField HeaderText="More Details">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnMoreBidItemDetails" runat="server" Text="View" OnClick="btnMoreBidItemDetails_Click"/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField HeaderText="Bid Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# "B"+Eval("BidCode").ToString() %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:BoundField DataField="CreatedUserName" HeaderText="Created By" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="Created Date"
                                                     DataFormatString='<%$ appSettings:datePattern %>' />
                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                     DataFormatString='<%$ appSettings:datePattern %>' />
                                                <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                    DataFormatString='<%$ appSettings:datePattern %>' />
                                                <asp:TemplateField HeaderText="Bid Opened For">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("BidOpeningPeriod").ToString()+" Days" %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bid Type">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("BidOpenType").ToString() =="1" ? "Online":Eval("BidOpenType").ToString() =="2" ? "Manual":"Online & Manual" %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bid Status">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("IsApproved").ToString() =="0" ? "Pending":Eval("IsApproved").ToString() =="1" ? "Approved":"Rejected" %>' ForeColor='<%# Eval("IsApproved").ToString() =="0" ? System.Drawing.Color.DeepSkyBlue:Eval("IsApproved").ToString() =="1" ? System.Drawing.Color.Green:System.Drawing.Color.Red %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:BoundField DataField="ApprovalRemarks" HeaderText="Remarks"  NullDisplayText="-" />
                                                
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>

            </ContentTemplate>

            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmitBid" />
                <asp:PostBackTrigger ControlID="btnCancel" />
            </Triggers>
        </asp:UpdatePanel>
    </form>


    <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.js"></script>
    <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />
    <script src="AdminResources/js/daterangepicker.js" type="text/javascript"></script>

    <script type="text/javascript">



        function CheckBoxChecked(CheckBox) {
            //get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvBidsForApproval.ClientID %>');
            var TargetChildControl = "CheckBox1";

            //get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0) {
                    Inputs[n].checked = CheckBox.checked;
                }
                else {

                }
        }

       
        Sys.Application.add_load(function () {
            $(function () {
                
                $('.clReplacementImg').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlReplacementImages').modal('hide');
                        //$('#mdlQuotations').modal('show');
                        $('#mdlBidMoreDetails').modal('show');
                        $('div').removeClass('modal-backdrop');
                    }

                });

                $('.clmdlSupportiveDocs').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlSupportiveDocs').modal('hide');
                        //$('#mdlQuotations').modal('show');
                        $('#mdlBidMoreDetails').modal('show');
                        $('div').removeClass('modal-backdrop');
                    }

                });

                $('.clmdlItemSpecs').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlItemSpecs').modal('hide');
                        //$('#mdlQuotations').modal('show');
                        $('#mdlBidMoreDetails').modal('show');
                        $('div').removeClass('modal-backdrop');
                    }

                });

                $('.clmdlStandardImages').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlStandardImages').modal('hide');
                        //$('#mdlQuotations').modal('show');
                        $('#mdlBidMoreDetails').modal('show');
                        $('div').removeClass('modal-backdrop');
                    }

                });
            });
        });


    </script>
</asp:Content>
