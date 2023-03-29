<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="PRInquiryReport_2.aspx.cs" Inherits="BiddingSystem.PRInquiryReport_2" %>
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
        .ChildGrid > tbody > tr > td:not(table) {
            background-color: #f5f2f2 !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
            border-bottom: 1px solid #d4d2d2;
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

        .ChildGridTwo > tbody > tr > td:not(table) {
            background-color: #f5f5f5 !important;
            color: black;
            border-bottom: 1px solid #d4d2d2;
        }


        .ChildGridTwo > tbody > tr {
            border: 1px solid #d4d2d2;
        }

        .ChildGridTwo th {
            color: white;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #525252 !important;
        }

        .ChildGridThree td {
            text-align: left;
        }

        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        #ContentSection_gvQuotations > tbody > tr:nth-child(2n+0) {
            background-color: #fbfafa !important;
            text-align: center;
        }

        #ContentSection_gvQuotations th {
            text-align: center;
        }

        #ContentSection_gvBids th, #ContentSection_gvBids td {
            text-align: center;
        }
    </style>
    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" type="text/css" />

    <section class="content-header">
        <h1>
            Purchase Request Inquiry
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li class="active">Purchase Request Inquiry </li>
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
                            <h3 class="box-title">Purchase Request</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>PR CODE</label><br>
                                        <br></br>
                                        <%--<asp:DropDownList CssClass="form-control select2" runat="server" ID="ddlPr"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlPr_SelectedIndexChanged">
                                        </asp:DropDownList>--%>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtPrCode" runat="server" CssClass="form-control" PlaceHolder="LCL1 / IMP1"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info paddingTop btnSearch" OnClick="btnSearch_Click" Text="Search" />
                                                <img id="loader" alt="" src="UserRersourses/assets/img/loader-info.gif" class="pull-right hidden paddingTop" style="margin-right:10px; max-height:30px;" />
                                            </div>
                                        </div>
                                        </br>
                                </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvPurchaseRequest"
                                            HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                                            OnRowDataBound="gvPurchaseRequest_RowDataBound" DataKeyNames="PrId"
                                            GridLines="None" CssClass="table table-responsive"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                            src="images/plus.png" />
                                                        <asp:Panel ID="pnlPrDetails" runat="server"
                                                            Style="display: none">
                                                        <asp:GridView ID="gvPrDetails" runat="server"
                                                                CssClass="table table-responsive ChildGrid"
                                                                GridLines="None" AutoGenerateColumns="false"  OnRowDataBound="gvPrDetails_RowDataBound"
                                                                DataKeyNames="PrdId" Caption="Items in Purchase Request"
                                                                EmptyDataText="No Item Found">
                                                                <Columns>

                                                                    <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                            src="images/plus.png" />
                                                          <asp:Panel ID="pnlStatusLog" runat="server"
                                                            Style="display: none">
                                                            <asp:GridView ID="gvStatusLog"
                                                                runat="server"
                                                                CssClass="table table-responsive ChildGridTwo"
                                                                GridLines="None"
                                                                AutoGenerateColumns="false"
                                                                EmptyDataText="No Log Found" HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White">
                                                                <Columns>
                                                                    <asp:BoundField
                                                                        DataField="UserName"
                                                                        HeaderText="Logged By" />
                                                                    <asp:BoundField
                                                                        DataField="LoggedDate"
                                                                        HeaderText="Logged Date and Time" />
                                                                    <asp:TemplateField  HeaderText="Current Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label
                                                        runat="server"
                                                        Text='<%# Eval("LogName").ToString()%>' CssClass="label label-info"/>
                                                                      
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
                                                                   
                                                                    <asp:BoundField DataField="EstimatedAmount"
                                                                        HeaderText="Estimated Price" />
                                                                     
                                                                    <asp:TemplateField HeaderText="Requested Quantity">
							                                        <ItemTemplate>
								                                        <asp:Label runat="server" ID="lblPrCode" Text='<%# Eval("RequestedQty")%>'></asp:Label>
                                                                        <asp:Label runat="server" Text='<%# Eval("ShortCode")%>'></asp:Label>
							                                        </ItemTemplate>
						                                            </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Current Status">
                                                                       <ItemTemplate>
                                                <asp:Label
                                                        runat="server"
                                                        Text='<%# Eval("StatusName").ToString()%>' CssClass="label label-info"/>
                                             
                                                                       
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PrId" HeaderText="PrId"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                
                                                <asp:TemplateField HeaderText="PR Code">
							                    <ItemTemplate>
								                    <asp:Label runat="server" ID="lblPrCode" Text='<%# "PR-"+Eval("PrCode")%>'></asp:Label>
							                    </ItemTemplate>
						                        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MRN Code" >
							                    <ItemTemplate>
                                                     <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                            src="images/plus.png"
								                    <asp:Label runat="server" ID="lblMrnCode" Text='<%#Eval("MrnCode")== null || Eval("MrnCode").ToString() == "" ?"0":"MRN-"+Eval("MrnCode") %>'></asp:Label>
							                   
                                                    <asp:Panel ID="pnlStatusLog" runat="server"
                                                            Style="display: none">
                                                           
                                                                <asp:GridView ID="gvMrnDetails" runat="server"
                                                                CssClass="table table-responsive ChildGrid"
                                                                GridLines="None" AutoGenerateColumns="false"  
                                                                Caption="Items in Material Request"
                                                                EmptyDataText="No Item Found">
                                                                   <Columns>
                                                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name"/>
                                                                    <asp:BoundField DataField="SubCategoryName" HeaderText="Sub Caegory Name" />
                                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"/>
                                                                    <asp:BoundField DataField="EstimatedAmount" HeaderText="Estimated Amount" />
                                                                       <asp:TemplateField HeaderText="Requested Quantity">
							                                        <ItemTemplate>
								                                        <asp:Label runat="server" ID="lblPrCode" Text='<%# Eval("RequestedQty")%>'></asp:Label>
                                                                        <asp:Label runat="server" Text='<%# Eval("MeasurementShortName")%>'></asp:Label>
							                                        </ItemTemplate>
						                                            </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Current Status">
                                                                    <ItemTemplate>
                                                 <asp:Label
                                                        runat="server"
                                                        Text='<%# Eval("StatusName").ToString()%>' CssClass="label label-info"/>
                                              
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               </Columns>
                                                              </asp:GridView>
                                                             </asp:Panel>
                                                    
                                                  </ItemTemplate>
						                        </asp:TemplateField>


                                                <asp:BoundField DataField="ExpectedDate" HeaderText="Date Of Request" />
                                                <asp:BoundField DataField="RequiredFor" HeaderText="Required For" />
                                                <asp:BoundField DataField="CreatedDateTime" HeaderText="PR Created Date"/>
                                                <asp:BoundField DataField="CreatedByName" HeaderText="Created By" />
                                                <asp:TemplateField HeaderText="Current Status">
                                                    <ItemTemplate>
                                                        <asp:Label
                                                        runat="server"
                                                        Text='<%# Eval("StatusName").ToString()%>' CssClass="label label-info"/>
                                                      
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                                        <div class="row">
                                            <div class="col-xs-12">
                                                <asp:Panel ID="pnlBidDetails" runat="server">
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
                                                                            
                                                                            <div style="color: black;">

                                                                                <asp:GridView ID="gvBidItems" runat="server"
                                                                                    CssClass="table gvItems"
                                                                                    GridLines="None" AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="gvBidItems_RowDataBound" DataKeyNames="BiddingItemId" HeaderStyle-BackColor="#67778e" HeaderStyle-ForeColor="White" Caption="Items in the Bid">
                                                                                    <Columns>

                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>

                                                                                                <tr style="color: White; background-color: #3C8DBC;">
                                                                                                    <td scope="col">#</td>
                                                                                                    <td class="hidden" scope="col">BidItemId</td>
                                                                                                    <td class="hidden" scope="col">BidItemId</td>
                                                                                                    <td class="hidden" scope="col">PRDId</td>
                                                                                                    <td class="hidden" scope="col">Item Id</td>
                                                                                                    <td scope="col">Item Name</td>
                                                                                                    <td scope="col">Unit</td>
                                                                                                    <td scope="col">Quantity</td>
                                                                                                    <td scope="col">Estimated Price</td>
                                                                                                    <td scope="col">Quotations Count</td>
                                                                                                    <td class="hidden" scope="col">LastSupplierId</td>
                                                                                                    <td scope="col">Last Purchased Supplier</td>
                                                                                                    <td scope="col">Last Purchased Price</td>
                                                                                                    <%--<td scope="col">Actions</td>
                                                                                                    <td scope="col">Performed By</td>
                                                                                                    <td scope="col">Performed On</td>--%>
                                                                                                    <td scope="col"></td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true">
                                                                                            <ItemTemplate>
                                                                                                <span style="font: bold;">
                                                                                                    <%#Container.DataItemIndex + 1%>
                                                                                                </span>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="BiddingItemId"
                                                                                            HeaderText="BidItemId"
                                                                                            HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                        <asp:BoundField DataField="BidId"
                                                                                            HeaderText="BidItemId"
                                                                                            HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                        <asp:BoundField DataField="PrdId"
                                                                                            HeaderText="PRDId" HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                        <asp:BoundField DataField="ItemId"
                                                                                            HeaderText="Item Id"
                                                                                            HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                        <asp:BoundField DataField="ItemName"
                                                                                            HeaderText="Item Name" />
                                                                                         <asp:BoundField DataField="UnitShortName" HeaderText="Unit" />
                                                                                        <asp:BoundField DataField="Qty" HeaderText="Quantity" DataFormatString="{0:N2}" />
                                                                                        <asp:BoundField DataField="EstimatedPrice" DataFormatString="{0:N2}"
                                                                                            HeaderText="Estimated Price" />
                                                                                        <asp:BoundField DataField="QuotationCount"
                                                                                            HeaderText="Quotations Count" />
                                                                                        <asp:BoundField DataField="LSupplierId"
                                                                                            HeaderText="LastSupplierId" HeaderStyle-CssClass="hidden"
                                                                                            ItemStyle-CssClass="hidden" />
                                                                                        <asp:BoundField DataField="LastSupplierName" NullDisplayText="Not Found"
                                                                                            HeaderText="Last Purchased Supplier" />
                                                                                        <asp:BoundField DataField="LPurchasedPrice" NullDisplayText="Not Found"
                                                                                            HeaderText="Last Purchased Price" />
                                                                                        <%--<asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("IsQuotationSelected").ToString() == "1" && Eval("IsTerminated").ToString() != "1" ? true : false %>'
                                                                                                    Text="QUOTATION SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: green;" Font-Bold="true" />
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("IsTerminated").ToString() == "1" ? true : false %>'
                                                                                                    Text="TERMINATED" Style="margin-right: 4px; margin-bottom: 4px; color: red;" Font-Bold="true" />
                                                                                                <br>
                                                                                                <br>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>--%>
                                                                                        <%--<asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Text='<%# Eval("IsTerminated").ToString() == "1" ? Eval("TerminatedByName").ToString() : Eval("IsQuotationSelected").ToString() == "1" ? Eval("QuotationSelectedByName").ToString(): "" %>'
                                                                                                    Style="margin-right: 4px; margin-bottom: 4px;" Font-Bold="true" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>--%>
                                                                                        <%--<asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Text='<%# Eval("IsTerminated").ToString() == "1" ? Eval("TerminatedDate").ToString() : Eval("IsQuotationSelected").ToString() == "1" ? Eval("QutationSelectionDate").ToString(): "" %>'
                                                                                                    Style="margin-right: 4px; margin-bottom: 4px;" Font-Bold="true" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>--%>
                                                                                   <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td colspan="100%">
                                                                                                        <asp:Panel ID="pnlQuotationItems" runat="server" Style="margin-left: 40px; overflow-x:auto;">
                                                                                                            <asp:GridView ID="gvQuotationItems" runat="server"
                                                                                                                CssClass="table table-responsive ChildGridTwo"
                                                                                                                GridLines="None" AutoGenerateColumns="false"
                                                                                                                Caption="Quotations" EmptyDataText="No Quotations Found" RowStyle-BackColor="#f5f2f2" HeaderStyle-BackColor="#525252" HeaderStyle-ForeColor="White">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true">
                                                                                                                        <ItemTemplate>
                                                                                                                            <span style="font: bold;">
                                                                                                                                <%#Container.DataItemIndex + 1%>
                                                                                                                            </span>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:BoundField DataField="QuotationItemId" HeaderText="QuotaionItemId"
                                                                                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId"
                                                                                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                                                    <asp:BoundField DataField="BiddingItemId" HeaderText="BidItemId"
                                                                                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                                                    <asp:BoundField DataField="SupplierId" HeaderText="Supplier"
                                                                                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier" NullDisplayText="Unavailable" />
                                                                                                                    <asp:BoundField DataField="Ratings" HeaderText="Ratings" NullDisplayText="Unavailable" ItemStyle-ForeColor="Orange" />
                                                                                                                    <asp:BoundField DataField="Description" HeaderText="Description" NullDisplayText="-" />
                                                                                                                    <asp:BoundField DataField="UnitPrice" HeaderText="Quoted Price" DataFormatString="{0:N2}"/>
                                                                                                                     <asp:BoundField DataField="ReqQty" HeaderText="Qty" DataFormatString="{0:N2}"/>
                                                                                                                    
                                                                                                                    <asp:BoundField DataField="SubTotal" HeaderText="Sub-Total" DataFormatString="{0:N2}"/>
                                                                                                                    <asp:BoundField DataField="NbtAmount" HeaderText="NBT" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                                                    <asp:BoundField DataField="VatAmount" HeaderText="VAT" DataFormatString="{0:N2}"/>
                                                                                                                    <asp:BoundField DataField="NetTotal" HeaderText="Net-Total" DataFormatString="{0:N2}"/>
                                                                                                                    <asp:BoundField DataField="SpecComply" HeaderText="Complies Specs" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>

                                                                                                                    <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label
                                                                                                                                runat="server"
                                                                                                                                Visible='<%# Eval("IsSelected").ToString() == "1" ? true : false %>'
                                                                                                                                Text="SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: green;" Font-Bold="true" />

                                                                                                                            <asp:Label
                                                                                                                                runat="server"
                                                                                                                                Visible='<%# Eval("IsSelected").ToString() == "1" ? false : true %>'
                                                                                                                                Text="NOT SELECTED" Style="margin-right: 4px; margin-bottom: 4px; color: indianred;" Font-Bold="true" />
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:BoundField DataField="ApprovalRemark" HeaderText="Selection Remark" NullDisplayText="-" />
                                                                                                                </Columns>
                                                                                                            </asp:GridView>
                                                                                                        </asp:Panel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>

                                                                            </div>
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
                                                                    DataFormatString="{0:dd-MM-yyyy}" />
                                                                <asp:BoundField DataField="StartDate"
                                                                    HeaderText="Start Date"
                                                                    DataFormatString="{0:dd-MM-yyyy}" />
                                                                <asp:BoundField DataField="EndDate"
                                                                    HeaderText="End Date"
                                                                    DataFormatString="{0:dd-MM-yyyy}" />
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
                                                                            Text='<%# Eval("IsApproved").ToString() =="0" ? "Pending":Eval("IsApproved").ToString() =="1" ? "Approved":"Rejected" %>'
                                                                            ForeColor='<%# Eval("IsApproved").ToString() =="0" ? System.Drawing.Color.DeepSkyBlue:Eval("IsApproved").ToString() =="1" ? System.Drawing.Color.Green:System.Drawing.Color.Red %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ApprovalRemarks"
                                                                    HeaderText="Remarks" NullDisplayText="-" />

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                   </asp:Panel>
                                                 </div>
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
                                                                                    <asp:BoundField DataField="PodId" HeaderText="POD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                                                    <asp:BoundField DataField="MeasurementName" HeaderText="Unit" />
                                                                                    <asp:BoundField DataField="Quantity" HeaderText="Requested QTY" />
                                                                                    <asp:BoundField DataField="ReceivedQty" HeaderText="Recieved QTY" />
                                                                                    <asp:BoundField DataField="WaitingQty" HeaderText="Waiting QTY" />
                                                                                    <asp:BoundField DataField="PendingQty" HeaderText="Pending QTY" />
                                                                                    <asp:BoundField DataField="ItemPrice" HeaderText="Price" DataFormatString="{0:N2}" />
                                                                                    <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" DataFormatString="{0:N2}" />
                                                                                    <asp:BoundField DataField="NbtAmount" HeaderText="NBT" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                    <asp:BoundField DataField="VatAmount" HeaderText="VAT" DataFormatString="{0:N2}" />
                                                                                    <asp:BoundField DataField="TotalAmount" HeaderText="NetTotal" DataFormatString="{0:N2}" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="PoID"  HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                <asp:BoundField DataField="POCode"  HeaderText="PO Code" />
                                                                <asp:BoundField DataField="SupplierName"  HeaderText="Supplier Name" />
                                                                <asp:BoundField DataField="CreatedDate"  HeaderText="Created Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}"/>
                                                                <asp:BoundField DataField="CreatedByName"  HeaderText="Created By"/>
                                                                <asp:TemplateField HeaderText="Approval Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                                                            Text="Pending" CssClass="label label-warning"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                                                            Text="APPROVED" CssClass="label label-success"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("IsApproved").ToString() == "2" ? true : false %>'
                                                                            Text="Rejected" CssClass="label label-danger"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ApprovedDate"  HeaderText="Approval Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}" NullDisplayText="Not Found"/>
                                                                <asp:BoundField DataField="ApprovedByName"  HeaderText="Approved By" NullDisplayText="Not Found"/>
                                                                <asp:TemplateField HeaderText="PO Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("IsDerived").ToString() == "0" ? true : false %>'
                                                                            Text="General PO" CssClass="label label-success"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("IsDerived").ToString() == "1" && Eval("IsDerivedType").ToString() == "1" ? true : false %>'
                                                                            Text="Modified PO" CssClass="label label-warning"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("IsDerived").ToString() == "1" && Eval("IsDerivedType").ToString() == "2" ? true : false %>'
                                                                            Text="Covering PO" CssClass="label label-info"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ParentPOCode"  HeaderText="Parent PO" NullDisplayText="None"/>
                                                                <asp:TemplateField HeaderText="Contains Derived POs">
                                                                    <ItemTemplate>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("WasDerived").ToString() == "0" ? true : false %>'
                                                                            Text="No" CssClass="label label-danger"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("WasDerived").ToString() == "1" ? true : false %>'
                                                                            Text="Yes" CssClass="label label-info"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="WarehouseName"  HeaderText="Warehouse" NullDisplayText="Not Found"/>
                                                                 <asp:TemplateField HeaderText="Cancel Status">
                                                                    <ItemTemplate>
                                                                       <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsCancelled").ToString() == "0" ? true : false %>'
                                    Text="Not Cancelled" CssClass="label label-success"/>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsCancelled").ToString() == "1" ? true : false %>'
                                    Text="Cancelled" CssClass="label label-danger"/>

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
                                                                                    <asp:BoundField DataField="MeasurementShortName"
                                                                                        HeaderText="Unit" />
                                                                                    <asp:BoundField DataField="Quantity"
                                                                                        HeaderText="Recieved Quantity" />
                                                                                    <asp:BoundField
                                                                                        DataField="VatAmount"
                                                                                        HeaderText="Vat Amount"
                                                                                        DataFormatString="{0:N2}" />
                                                                                    <asp:BoundField
                                                                                        DataField="NbtAmount"
                                                                                        HeaderText="NBT Amount"
                                                                                        DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                    <asp:BoundField
                                                                                        DataField="TotalAmount"
                                                                                        HeaderText="Total Amount"
                                                                                        DataFormatString="{0:N2}" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="GrnId"  HeaderText="GrnId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                <asp:BoundField DataField="GrnCode"  HeaderText="GRN Code" />
                                                                <asp:BoundField DataField="PoCode"  HeaderText="PO Code" />
                                                                <asp:BoundField DataField="GoodReceivedDate"  HeaderText="Good Received Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                                                <asp:BoundField DataField="CreatedDate"  HeaderText="Created Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}"/>
                                                                <asp:BoundField DataField="CreatedByName"  HeaderText="Created By"/>
                                                                <asp:TemplateField HeaderText="Approval Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                                                            Text="Pending" CssClass="label label-warning"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                                                            Text="APPROVED" CssClass="label label-success"/>
                                                                        <asp:Label
                                                                            runat="server"
                                                                            Visible='<%# Eval("IsApproved").ToString() == "2" ? true : false %>'
                                                                            Text="Rejected" CssClass="label label-danger"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ApprovedDate"  HeaderText="Approval Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}" NullDisplayText="Not Found"/>
                                                                <asp:BoundField DataField="ApprovedByName"  HeaderText="Approved By" NullDisplayText="Not Found"/>

                                                            </Columns>
                                                        </asp:GridView>
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
    <script src="AdminResources/js/autoCompleter.js"></script>

    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $(function () {

                $('.select2').select2()

            })
        });


        Sys.Application.add_load(function () {

            $(function () {
               
                $(function () {
                    $('.btnSearch').on({
                        click: function () {
                            $('#loader').removeClass('hidden');
                        }
                    })
                });
               
            });
        });

    </script>
    
    
        
   
</asp:Content>

