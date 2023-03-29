<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="PRInquiryForm.aspx.cs" Inherits="BiddingSystem.PRInquiryForm" %>

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
    
   <link href="AdminResources/css/select2.min.css" rel="stylesheet" />  <!-- This used in this page -->
     <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
     <script src="AdminResources/js/moment.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" type="text/css" />

    <section class="content-header">
        <h1>
            Purchase Request Inquiry
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Search Purchase Request</li>
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
                            <h3 class="box-title">Search Purchase Request</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                           <%-- <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>PR CODE</label>
                                        <asp:DropDownList CssClass="form-control select2" runat="server" ID="ddlPr"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlPr_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>
                                    <div class="box-body pull-left bg-teal-gradient form-inline" >
					        <div class="input-group input-group-sm">
							    <label class="form-control">Search By</label>
						    </div>
                             <div class="input-group input-group-sm">
                                <asp:DropDownList CssClass="form-control" runat="server" ID="ddlCategories"
                                    AutoPostBack="true" >
                                </asp:DropDownList>
						    </div>	
                            <div class="input-group input-group-sm">									  
						        <asp:DropDownList CssClass="form-control" runat="server" ID="ddlDepartment"
                                    AutoPostBack="true" >
                                </asp:DropDownList>
						    </div>	
                             <div class="input-group input-group-sm">									  
						        <asp:DropDownList ID="ddlAdvancesearchitems" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAdvancesearchitems_SelectedIndexChanged" type="date" class="form-control pull-right">
							        <asp:ListItem Text="Select type to search" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="PR Code" Value="1"></asp:ListItem>
							        <asp:ListItem Text="PR Creator" Value="2"></asp:ListItem>
							        <asp:ListItem Text="Date Of Request" Value="3"></asp:ListItem>
						        </asp:DropDownList>
						    </div>
                             <div class="input-group input-group-sm" style="width:150px">
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
                                        <asp:GridView runat="server" ID="gvPurchaseRequest"
                                            HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                                            OnRowDataBound="gvPurchaseRequest_RowDataBound" DataKeyNames="PrId"
                                            GridLines="None" CssClass="table table-responsive" OnPageIndexChanging="gvPurchaseRequest_PageIndexChanging"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EmptyDataText="No PR Found OR MRN has not added to PR" EmptyDataRowStyle-CssClass="GridViewEmptyText" >
                                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Pr Item">
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                            src="images/plus.png" />
                                                        <asp:Panel ID="pnlPrDetails" runat="server"
                                                            Style="display: none">
                                                            <asp:GridView ID="gvPrDetails" runat="server"
                                                                CssClass="table table-responsive ChildGrid"
                                                                GridLines="None" AutoGenerateColumns="false"
                                                                DataKeyNames="PrdId" Caption="Items in Purchase Request"
                                                                EmptyDataText="No Item Found">
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
                                                                    <asp:TemplateField HeaderText="Current Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "0" ? true : false %>'
                                                                                Text="PR APPROVAL PENDING"
                                                                                CssClass="label label-warning" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "1" ? true : false %>'
                                                                                Text="BID CREATION"
                                                                                CssClass="label label-warning" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "2" ? true : false %>'
                                                                                Text="PR REJECTED"
                                                                                CssClass="label label-danger" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "3" ? true : false %>'
                                                                                Text="BID PENDING ACCEPT & OPENING"
                                                                                CssClass="label label-warning" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "4" ? true : false %>'
                                                                                Text="QUOTATION COLLECTION"
                                                                                CssClass="label label-warning" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "5" ? true : false %>'
                                                                                Text="QUOTATION RECOMMENDATION"
                                                                                CssClass="label label-warning" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "6" ? true : false %>'
                                                                                Text="QUOTATION APPROVAL"
                                                                                CssClass="label label-warning" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "7" ? true : false %>'
                                                                                Text="PO CREATION"
                                                                                CssClass="label label-warning" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "8" ? true : false %>'
                                                                                Text="PO APPROVAL"
                                                                                CssClass="label label-warning" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "9" ? true : false %>'
                                                                                Text="GRN CREATION"
                                                                                CssClass="label label-warning" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "10" ? true : false %>'
                                                                                Text="GRN APPROVAL"
                                                                                CssClass="label label-warning" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "11" ? true : false %>'
                                                                                Text="COMPLETED"
                                                                                CssClass="label label-info" />
                                                                            <asp:Label runat="server"
                                                                                Visible='<%# Eval("CurrentStatus").ToString() == "12" ? true : false %>'
                                                                                Text="PROCUREMENT ENDED PRIOR COMPLETION"
                                                                                CssClass="label label-danger" />
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
                                                <asp:BoundField DataField="CreatedDateTime"  HeaderText="Created On" DataFormatString='<%$ appSettings:dateTimePattern %>' />                                                
                                                <asp:BoundField DataField="QuotationFor" HeaderText="Requested For" />
                                                <asp:BoundField DataField="CreatedByName" HeaderText="Created By" />
                                                <asp:BoundField DataField="RequiredDate"  HeaderText="Expected Date" DataFormatString='<%$ appSettings:dateTimePattern %>'/>                                                
                                                <asp:TemplateField HeaderText="PR Type">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblprtype" Text='<%#Eval("PrTypeid").ToString()=="7"? "Stock":"Non-Stock"%>' ForeColor='<%#Eval("PrTypeid").ToString()=="7"? System.Drawing.Color.Maroon:System.Drawing.Color.Navy%>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="Current Status">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server"
                                                            Visible='<%# Eval("CurrentStatus").ToString() == "0" ? true : false %>'
                                                            Text="PR APPROVAL PENDING" CssClass="label label-warning" />
                                                        <asp:Label runat="server"
                                                            Visible='<%# Eval("CurrentStatus").ToString() == "1" ? true : false %>'
                                                            Text="PR APPROVED. VIEW ITEMS FOR MORE DETAILS"
                                                            CssClass="label label-info" />
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

                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:Panel ID="pnlPrDetails" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>PR CODE</label>
                                                    <asp:DropDownList CssClass="form-control select2" runat="server" ID="ddlPr"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlPr_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Bids In the PR</label>
                                                    <asp:DropDownList CssClass="form-control" runat="server"
                                                        ID="ddlBids" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlBids_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:Panel ID="pnlBidDetails" runat="server" visible="false">
                                                    <h4>Bid Details</h4>
                                                    <hr />
                                                    <div class="table-responsive">
                                                        <asp:GridView runat="server" ID="gvBids" GridLines="None"
                                                            CssClass="table table-responsive ChildGrid"
                                                            HeaderStyle-BackColor="#3C8DBC"
                                                            HeaderStyle-ForeColor="White" AutoGenerateColumns="false"
                                                            DataKeyNames="BidId" OnRowDataBound="gvBids_RowDataBound"
                                                            EmptyDataText="No Record Found">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <img alt=""
                                                                            style="cursor: pointer;margin-top: -6px;"
                                                                            src="images/plus.png" />
                                                                        <asp:Panel ID="pnlBidItems" runat="server"
                                                                            Style="display: none">
                                                                            <asp:GridView ID="gvBidItems" runat="server"
                                                                                CssClass="table table-responsive ChildGridTwo"
                                                                                GridLines="None"
                                                                                AutoGenerateColumns="false"
                                                                                Caption="Items in the Bid">
                                                                                <Columns>
                                                                                    <asp:BoundField
                                                                                        DataField="BiddingItemId"
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
                                                                                    <asp:BoundField
                                                                                        DataField="CategoryId"
                                                                                        HeaderText="Item Id"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField
                                                                                        DataField="CategoryName"
                                                                                        HeaderText="Category Name" />
                                                                                    <asp:BoundField
                                                                                        DataField="SubCategoryId"
                                                                                        HeaderText="Item Id"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField
                                                                                        DataField="SubCategoryName"
                                                                                        HeaderText="Sub-Category Name" />
                                                                                    <asp:BoundField DataField="ItemId"
                                                                                        HeaderText="Item Id"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="ItemName"
                                                                                        HeaderText="Item Name" />
                                                                                    <asp:BoundField DataField="Qty"
                                                                                        HeaderText="Quantity" />
                                                                                    <asp:BoundField
                                                                                        DataField="EstimatedPrice"
                                                                                        HeaderText="Estimated Price" />


                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                                    HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:TemplateField HeaderText="Bid Code"
                                                                    HeaderStyle-HorizontalAlign="Center"
                                                                    ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server"
                                                                            Text='<%# "B"+Eval("BidCode").ToString() %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="CreatedUserName"
                                                                    HeaderText="Created By" />
                                                                <asp:BoundField DataField="CreateDate"
                                                                    HeaderText="Created Date"
                                                                     DataFormatString='<%$ appSettings:datePattern %>' />
                                                                <asp:BoundField DataField="StartDate"
                                                                    HeaderText="Start Date"
                                                                     DataFormatString='<%$ appSettings:datePattern %>' />
                                                                <asp:BoundField DataField="EndDate"
                                                                    HeaderText="End Date"
                                                                     DataFormatString='<%$ appSettings:datePattern %>'/>
                                                                <asp:TemplateField HeaderText="Bid Opened For">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server"
                                                                            Text='<%# Eval("BidOpeningPeriod").ToString()+" Days" %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bid Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server"
                                                                            Text='<%# Eval("BidOpenType").ToString() =="1" ? "Online":Eval("BidOpenType").ToString() =="2" ? "Manual":"Online & Manual" %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bid Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server"
                                                                            Text='<%# Eval("IsApproved").ToString() =="0" ? "Pending Accept":Eval("IsApproved").ToString() =="1" ? "Accepted":"Rejected" %>'
                                                                            ForeColor='<%# Eval("IsApproved").ToString() =="0" ? System.Drawing.Color.DeepSkyBlue:Eval("IsApproved").ToString() =="1" ? System.Drawing.Color.Green:System.Drawing.Color.Red %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ApprovalRemarks"
                                                                    HeaderText="Remarks" NullDisplayText="-" />

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>

                                                    <h4>Quotations Collected</h4>
                                                    <hr />

                                                    <div class="table-responsive" style="color: black;">
                                                        <asp:GridView runat="server" ID="gvQuotations" GridLines="None"
                                                            CssClass="table table-responsive ChildGrid"
                                                            AutoGenerateColumns="false"
                                                            OnRowDataBound="gvQuotations_RowDataBound"
                                                            DataKeyNames="QuotationId" HeaderStyle-BackColor="#3C8DBC"
                                                            HeaderStyle-ForeColor="White"
                                                            EmptyDataText="No Record Found">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <img alt=""
                                                                            style="cursor: pointer;margin-top: -6px;"
                                                                            src="images/plus.png" />

                                                                        <asp:Panel ID="pnlQuotationItems" runat="server"
                                                                            Style="display: none">
                                                                            <asp:GridView runat="server"
                                                                                ID="gvQuotationItems" GridLines="None"
                                                                                CssClass="table table-responsive ChildGridTwo"
                                                                                AutoGenerateColumns="false"
                                                                                DataKeyNames="QuotationItemId"
                                                                                Caption="Items in the Quotation">
                                                                                <Columns>

                                                                                    <asp:BoundField
                                                                                        DataField="QuotationItemId"
                                                                                        HeaderText="QuotaionItemId"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField
                                                                                        DataField="QuotationId"
                                                                                        HeaderText="QuotationId"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField
                                                                                        DataField="BiddingItemId"
                                                                                        HeaderText="BidItemId"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField
                                                                                        DataField="CategoryId"
                                                                                        HeaderText="Category Id"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField
                                                                                        DataField="CategoryName"
                                                                                        HeaderText="Category Name"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField
                                                                                        DataField="SubCategoryId"
                                                                                        HeaderText="SubCategory Id"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField
                                                                                        DataField="SubCategoryName"
                                                                                        HeaderText="Sub-Category Name"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="ItemId"
                                                                                        HeaderText="Item Id"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="ItemName"
                                                                                        HeaderText="Item Name"
                                                                                        ItemStyle-Width="100px"
                                                                                        HeaderStyle-Width="100px" />
                                                                                    <asp:TemplateField
                                                                                        HeaderText="Quantity">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtQty"
                                                                                                Text='<%#Eval("Qty")%>'
                                                                                                Enabled="false"
                                                                                                type="number" min="1"
                                                                                                runat="server"
                                                                                                Width="80px"
                                                                                                autocomplete="off"
                                                                                                CssClass="txtQtyCl" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField
                                                                                        DataField="EstimatedPrice"
                                                                                        HeaderText="Estimated Price" />

                                                                                    <asp:TemplateField
                                                                                        HeaderText="Quoted Price">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox
                                                                                                ID="txtUnitPrice"
                                                                                                Text='<%#Eval("UnitPrice")%>'
                                                                                                type="number" step="any"
                                                                                                min="0" runat="server"
                                                                                                Width="80px"
                                                                                                autocomplete="off"
                                                                                                CssClass="txtUnitPriceCl"
                                                                                                Enabled="false" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField
                                                                                        HeaderText="Sub Total">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox
                                                                                                ID="txtSubTotal"
                                                                                                Enabled="false"
                                                                                                Text='<%#Eval("SubTotal")%>'
                                                                                                runat="server"
                                                                                                Width="80px"
                                                                                                CssClass="txtSubTotalCl" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField
                                                                                        HeaderText="Include NBT/VAT"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkNbt"
                                                                                                Text="NBT"
                                                                                                runat="server"
                                                                                                Style="cursor: pointer"
                                                                                                Checked='<%#Eval("HasNbt").ToString() =="1"?true:false%>'
                                                                                                CssClass="chkNbtCl" />
                                                                                            <br />
                                                                                            <asp:CheckBox ID="chkVat"
                                                                                                Text="VAT"
                                                                                                runat="server"
                                                                                                Style="cursor: pointer"
                                                                                                Checked='<%#Eval("HasVat").ToString() =="1"?true:false%>'
                                                                                                CssClass="chkVatCl" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField
                                                                                        HeaderText="NBT Percentage"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden">
                                                                                        <ItemTemplate>
                                                                                            <asp:RadioButton
                                                                                                ID="rdoNbt204"
                                                                                                GroupName="grpPercentage"
                                                                                                Style="cursor: pointer"
                                                                                                Text="2.04%"
                                                                                                runat="server"
                                                                                                Checked='<%#Eval("NbtCalculationType").ToString() =="1"?true:false%>'
                                                                                                CssClass="rdo204" />
                                                                                            <br />
                                                                                            <asp:RadioButton
                                                                                                ID="rdoNbt2"
                                                                                                GroupName="grpPercentage"
                                                                                                Style="cursor: pointer"
                                                                                                Text="2.00%"
                                                                                                runat="server"
                                                                                                Checked='<%#Eval("NbtCalculationType").ToString() =="2"?true:false%>'
                                                                                                CssClass="rdo2" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="NBT">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtNbt"
                                                                                                Enabled="false"
                                                                                                Text='<%#Eval("NbtAmount")%>'
                                                                                                runat="server"
                                                                                                Width="80px"
                                                                                                CssClass="txtNbtCl" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="VAT">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtVat"
                                                                                                Enabled="false"
                                                                                                Text='<%#Eval("VatAmount")%>'
                                                                                                runat="server"
                                                                                                Width="80px"
                                                                                                CssClass="txtVatCl" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField
                                                                                        HeaderText="Net Total">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox
                                                                                                ID="txtNetTotal"
                                                                                                Enabled="false"
                                                                                                Text='<%#Eval("TotalAmount")%>'
                                                                                                runat="server"
                                                                                                Width="80px"
                                                                                                CssClass="txtNetTotalCl" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="QuotationId"
                                                                    HeaderText="QuotationId"
                                                                    HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                                    HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="SupplierId"
                                                                    HeaderText="SupplierId"
                                                                    HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="SupplierName"
                                                                    HeaderText="Supplier Name" />
                                                                <asp:BoundField DataField="SubTotal"
                                                                    HeaderText="Sub Total" />
                                                                <asp:BoundField DataField="NbtAmount"
                                                                    HeaderText="NBT Amount" />
                                                                <asp:BoundField DataField="VatAmount"
                                                                    HeaderText="VAT Amount" />
                                                                <asp:BoundField DataField="NetTotal"
                                                                    HeaderText="Net Total" />
                                                                <asp:BoundField DataField="TermsAndCondition"
                                                                    HeaderText="Terms And Condition"
                                                                    NullDisplayText="-" />

                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server"
                                                                            Text='<%#Eval("IsSelected").ToString() =="1"?"Selected": "Rejected"%>'
                                                                            Font-Bold="true"
                                                                            style='<%#Eval("IsSelected").ToString() =="1"?"color: #3C8DBC": "color: Red"%>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="SelectionRemarks"
                                                                    HeaderText="Remarks" NullDisplayText="-" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>

                                                    <h4>POs Created</h4>
                                                    <hr />
                                                    <div class="table-responsive">
                                                        <asp:GridView runat="server" ID="gvPO"
                                                            EmptyDataText="No Record Found" GridLines="None"
                                                            CssClass="table table-responsive ChildGrid"
                                                            OnRowDataBound="gvPO_RowDataBound"
                                                            AutoGenerateColumns="false" DataKeyNames="PoID">
                                                            <Columns>

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <img alt=""
                                                                            style="cursor: pointer;margin-top: -6px;"
                                                                            src="images/plus.png" />
                                                                        <asp:Panel ID="pnlPoitems" runat="server"
                                                                            Style="display: none">
                                                                            <asp:GridView ID="gvPoItems" runat="server"
                                                                                CssClass="table table-responsive ChildGridTwo"
                                                                                GridLines="None"
                                                                                AutoGenerateColumns="false"
                                                                                Caption="Items in Purchase Order">
                                                                                <Columns>
                                                                                    <asp:BoundField
                                                                                        DataField="CategoryName"
                                                                                        HeaderText="Category Name" />
                                                                                    <asp:BoundField
                                                                                        DataField="SubCategoryName"
                                                                                        HeaderText="Sub-Category Name" />
                                                                                    <asp:BoundField DataField="ItemName"
                                                                                        HeaderText="Item Name" />
                                                                                    <asp:BoundField DataField="Quantity"
                                                                                        HeaderText="Quantity" />
                                                                                    <asp:BoundField DataField="ReceivedQty"
                                                                                        HeaderText="Received Qty" />
                                                                                    <asp:BoundField
                                                                                        DataField="ItemPrice"
                                                                                        HeaderText="Item Price" />
                                                                                    <asp:BoundField
                                                                                        DataField="VatAmount"
                                                                                        HeaderText="VAT" />
                                                                                    <asp:BoundField
                                                                                        DataField="NbtAmount"
                                                                                        HeaderText="NBT" />
                                                                                    <asp:BoundField
                                                                                        DataField="TotalAmount"
                                                                                        HeaderText="Subtotal" />


                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="PoID" HeaderText="PoID"
                                                                    HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="POCode" HeaderText="PO Code"/>
                                                                <asp:BoundField DataField="BasePr" HeaderText="BasePr"
                                                                    HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="SupplierName"
                                                                    HeaderText="Supplier Name" />
                                                                <asp:BoundField DataField="CreatedByName"
                                                                    HeaderText="Created By" />
                                                                <asp:BoundField DataField="CreatedDate"
                                                                    HeaderText="Created Date" DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                                <asp:BoundField DataField="VatAmount"
                                                                    HeaderText="VAT" />
                                                                <asp:BoundField DataField="NBTAmount"
                                                                    HeaderText="NBT" />
                                                                <asp:BoundField DataField="TotalAmount"
                                                                    HeaderText="Net Total" />

                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server"
                                                                            Text='<%# Eval("IsApproved").ToString() =="0" ? "Pending":Eval("IsApproved").ToString() =="1" ? "Approved":"Rejected" %>'
                                                                            ForeColor='<%# Eval("IsApproved").ToString() =="0" ? System.Drawing.Color.DeepSkyBlue:Eval("IsApproved").ToString() =="1" ? System.Drawing.Color.Green:System.Drawing.Color.Red %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>

                                                    <h4>GRNs Created</h4>
                                                    <hr />
                                                    <div class="table-responsive">
                                                        <asp:GridView runat="server" ID="gvGrn"
                                                            EmptyDataText="No records Found" GridLines="None"
                                                            CssClass="table table-responsive ChildGrid"
                                                            AutoGenerateColumns="false" DataKeyNames="GrnId"
                                                            OnRowDataBound="gvGrn_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <img alt=""
                                                                            style="cursor: pointer;margin-top: -6px;"
                                                                            src="images/plus.png" />
                                                                        <asp:Panel ID="pnlGrnItems" runat="server"
                                                                            Style="display: none">
                                                                            <asp:GridView ID="gvGrnItems" runat="server"
                                                                                CssClass="table table-responsive ChildGridTwo"
                                                                                AutoGenerateColumns="false">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="GrnId"
                                                                                        HeaderText="GRN Id"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="PoId"
                                                                                        HeaderText="Po Id"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField
                                                                                        DataField="QuotationId"
                                                                                        HeaderText="Quotation Id"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="ItemId"
                                                                                        HeaderText="Item No"
                                                                                        HeaderStyle-CssClass="hidden"
                                                                                        ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="ItemName"
                                                                                        HeaderText="Item Name" />
                                                                                    <asp:BoundField
                                                                                        DataField="ItemPrice"
                                                                                        HeaderText="Unit Price"
                                                                                        DataFormatString="{0:N2}" />
                                                                                    <asp:BoundField DataField="Quantity"
                                                                                        HeaderText="Recieved Quantity" />
                                                                                    <asp:BoundField
                                                                                        DataField="VatAmount"
                                                                                        HeaderText="Vat Amount"
                                                                                        DataFormatString="{0:N2}" />
                                                                                    <asp:BoundField
                                                                                        DataField="NbtAmount"
                                                                                        HeaderText="NBT Amount"
                                                                                        DataFormatString="{0:N2}" />
                                                                                    <asp:BoundField
                                                                                        DataField="TotalAmount"
                                                                                        HeaderText="Total Amount"
                                                                                        DataFormatString="{0:N2}" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:BoundField DataField="GrnId" HeaderText="GRN ID"
                                                                    HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="GrnCode"
                                                                    HeaderText="GRN Code" />
                                                                <asp:BoundField DataField="PoID" HeaderText="PoID"
                                                                    HeaderStyle-CssClass="hidden"
                                                                    ItemStyle-CssClass="hidden" />
                                                                <asp:BoundField DataField="POCode"
                                                                    HeaderText="PO Code" />
                                                                <asp:BoundField DataField="SupplierName"
                                                                    HeaderText="Supplier Name" />
                                                                <asp:BoundField DataField="GoodReceivedDate"
                                                                    HeaderText="Good Received Date"
                                                                     DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                                <%--  <asp:BoundField DataField="GrnStatusCount"  HeaderText="Grn Status" />--%>
                                                                <asp:TemplateField HeaderText="GRN Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtApproved"
                                                                            Text='<%#Eval("IsApproved").ToString()=="1"?"GRN Approved":"Pending Approval" %>'
                                                                            ForeColor='<%#Eval("IsApproved").ToString()=="1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'
                                                                            runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </asp:Panel>
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
    <script src="AdminResources/js/select2.full.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $(function () {
                $('.select2').select2()
            })

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