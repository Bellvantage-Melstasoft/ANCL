 <%@ Page Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="PurchaseRequisionReportNew.aspx.cs" Inherits="BiddingSystem.PurchaseRequisionReportNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
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
        <h1>Purchase Requisition Report</h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Purchase Requisition Report </li>
      </ol>
    </section>
    <br />
    <section class="content" id="divPrintPo">
    <div class="container-fluid">
        <form runat="server" id="frm1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" >
            <ContentTemplate>
            
            <div class="box box-info">
                    <%--<div class="box-header">
                        <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Purchase Requisition Report</h3>
                        <hr>
                    </div>--%>
                    <div class="box-body">
                        
                        <div class="row">
                            <div class="col-md-12">
                                <h4>MRN Details</h4>
                                                    <hr />
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvMRNItems" AutoGenerateColumns="false" OnRowDataBound="gvMRNItems_RowDataBound" GridLines="None"
                                        CssClass="table table-responsive ChildGrid"  EnableViewState="true">
                                        <Columns>
                                            <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                            src="images/plus.png" />
                                                        <asp:Panel ID="pnlMRNDetails" runat="server"
                                                            Style="display: none">
                                                        <asp:GridView ID="gvMrnDetails" runat="server"
                                                                CssClass="table table-responsive ChildGridTwo"
                                                                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" BorderColor="LightGray" 
                                                                Caption="Items in Material Request"
                                                                EmptyDataText="No Item Found">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                                    <asp:BoundField DataField="RequestedQty" HeaderText="Requested QTY" />
                                                                    <asp:BoundField DataField="MeasurementShortName" HeaderText="Unit" NullDisplayText="Not Found" />
                                                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                                                    <asp:BoundField DataField="ReceivedQty"
                                                                        HeaderText="Received Quantity" />
                                                                    <asp:BoundField DataField="IssuedQty"
                                                                        HeaderText="Issued Quantity" />
                                                                     </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:BoundField DataField="MrnId" HeaderText="MrnID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />  
                                            <asp:TemplateField HeaderText="MRN Code">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# "MRN-"+Eval("MRNCode").ToString()%>'/>
                                                    </ItemTemplate>
                                            </asp:TemplateField>  
                                            <asp:BoundField DataField="ExpectedDate" HeaderText="Expected Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                            <asp:TemplateField HeaderText="Expense Type">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# int.Parse(Eval("ExpenseType").ToString()) == 1 ? "Capital Expense":"Operational Expense"%>'/>
                                                    </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:BoundField DataField="MrnCategoryName" HeaderText="Category" />
                                            <asp:BoundField DataField="MrnSubCategoryName" HeaderText="Sub Category" />
                                            <asp:BoundField DataField="Location" HeaderText="Warehouse" />
                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                                            <asp:BoundField DataField="CreatedByName" HeaderText="Created By"  />
                                            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:dd MMMM yyyy}"/>

                                        
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <hr />



                        <asp:Panel ID="pnlPR" runat="server" Visible="false">
                           
                        
                        <div class="row">
                            <div class="col-md-12">
                                <h4>PR Details</h4>
                                                    <hr />
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvPRItems" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="gvPRItems_RowDataBound"  DataKeyNames="PrId"
                                        CssClass="table table-responsive ChildGrid" HeaderStyle-BackColor="#3C8DBC"
                                                            HeaderStyle-ForeColor="White" BorderColor="LightGray" EnableViewState="true">
                                        <Columns>
                                            <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                            src="images/plus.png" />
                                                        <asp:Panel ID="pnlPRDetails" runat="server"
                                                            Style="display: none">
                                                        <asp:GridView ID="gvPrDetails" runat="server"
                                                                CssClass="table table-responsive  ChildGridTwo"
                                                                GridLines="None" AutoGenerateColumns="false" 
                                                                Caption="Items in Purchase Request"
                                                                EmptyDataText="No Item Found">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                                    <asp:BoundField DataField="RequestedQty" HeaderText="Requested QTY" />
                                                                    <asp:BoundField DataField="MeasurementShortName" HeaderText="Unit" NullDisplayText="Not Found" />
                                                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                                                     <asp:BoundField DataField="AvailableQty" HeaderText="Available Stock" />
                                                                  
                                                                     </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>





                                            <asp:BoundField DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />  
                                            <asp:TemplateField HeaderText="PR Code">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# "PR-"+Eval("PrCode").ToString()%>'/>
                                                    </ItemTemplate>
                                            </asp:TemplateField>  
                                            <asp:BoundField DataField="ExpectedDate" HeaderText="Expected Date" DataFormatString="{0:dd MMMM yyyy}"/>
                                            <asp:BoundField DataField="RequiredFor" HeaderText="Required For" />
                                            <asp:TemplateField HeaderText="Expense Type">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# int.Parse(Eval("ExpenseType").ToString()) == 1 ? "Capital Expense":"Operational Expense"%>'/>
                                                    </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:BoundField DataField="PrCategoryName" HeaderText="Category" />
                                            <asp:BoundField DataField="PrSubCategoryName" HeaderText="Sub Category" />
                                            <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse" />
                                            <asp:BoundField DataField="CreatedByName" HeaderText="Created By"  />
                                            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:dd MMMM yyyy}"/>

                                        
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <hr />
</asp:Panel>



                        
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
                                                                                                                    <asp:BoundField DataField="Ratings" HeaderText="Ratings" NullDisplayText="Unavailable" ItemStyle-ForeColor="Orange"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                                                    <asp:BoundField DataField="Description" HeaderText="Description" NullDisplayText="-" />
                                                                                                                    <asp:BoundField DataField="UnitPrice" HeaderText="Quoted Price" DataFormatString="{0:N2}"/>
                                                                                                                    <asp:BoundField DataField="ReqQty" HeaderText="Qty" DataFormatString="{0:N2}"/>
                                                                                                                    
                                                                                                                    <asp:BoundField DataField="SubTotal" HeaderText="Sub-Total" DataFormatString="{0:N2}"/>
                                                                                                                    <asp:BoundField DataField="NbtAmount" HeaderText="NBT" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                                                    <asp:BoundField DataField="VatAmount" HeaderText="VAT" DataFormatString="{0:N2}"/>
                                                                                                                    <asp:BoundField DataField="NetTotal" HeaderText="Net-Total" DataFormatString="{0:N2}"/>
                                                                                                                    <asp:BoundField DataField="SpecComply" HeaderText="Complies Specs" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                                                    <asp:BoundField DataField="SelectedByName" HeaderText="Selected By" NullDisplayText="-"/>
                                                                                                                    <asp:BoundField DataField="SelectedDate" HeaderText="Selected On" DataFormatString="{0:dd MMMM yyyy}"/>
                                                                                                                    <%--<asp:TemplateField HeaderText="Selected Date" >
                                                                                                                    <ItemTemplate>
                                                                                                                        <%# (DateTime)Eval("SelectedDate") == DateTime.MinValue ? "-" : Eval("SelectedDate") %>
                                                                                                                    </ItemTemplate>
                                                                                                                     </asp:TemplateField>--%>
                
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
                                                                     DataFormatString="{0:dd MMMM yyyy}" />
                                                                <asp:BoundField DataField="StartDate"
                                                                    HeaderText="Start Date"
                                                                     DataFormatString="{0:dd MMMM yyyy}" />
                                                                <asp:BoundField DataField="EndDate"
                                                                    HeaderText="End Date"
                                                                    DataFormatString="{0:dd-MMMM-yyyy}" />
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
                                                               <%-- <asp:TemplateField HeaderText="Bid Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server"
                                                                            Text='<%# Eval("IsApproved").ToString() =="0" ? "Pending":Eval("IsApproved").ToString() =="1" ? "Approved":"Rejected" %>'
                                                                            ForeColor='<%# Eval("IsApproved").ToString() =="0" ? System.Drawing.Color.DeepSkyBlue:Eval("IsApproved").ToString() =="1" ? System.Drawing.Color.Green:System.Drawing.Color.Red %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:BoundField DataField="ApprovalRemarks"
                                                                    HeaderText="Remarks" NullDisplayText="-" />
                                                                 <asp:BoundField DataField="QuotationSelectedByName"
                                                                    HeaderText="Selection Finalized By" NullDisplayText="-" />
                                                                
                                                                <asp:TemplateField HeaderText="Selection Finalized on" >
                                                                                    <ItemTemplate>
                                                                                       <%# (DateTime)Eval("QuotationSelectionDate") == DateTime.MinValue ? "-" : Eval("QuotationSelectionDate", "{0:dd MMMM yyyy}") %>
                                                                                    </ItemTemplate>
                                                                                     </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                   </asp:Panel>
                                                 </div>
                                             </div>
                        <hr>
                           
                            <h4>Tabulation Recommendation</h4>
                                                    <hr />
                                                    <div class="table-responsive">
                                                        <asp:GridView runat="server" ID="gvRecMaster"
                                                            EmptyDataText="No Record Found" GridLines="None"
                                                            CssClass="table table-responsive ChildGrid"
                                                            OnRowDataBound="gvRecMaster_RowDataBound"
                                                            AutoGenerateColumns="false" DataKeyNames="TabulationId">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <img alt=""
                                                                            style="cursor: pointer;margin-top: -6px;"
                                                                            src="images/plus.png" />
                                                                        <asp:Panel ID="pnlTabRecDetails" runat="server"
                                                                            Style="display: none">
                                                                            <asp:GridView ID="gvTabRecDetails" runat="server"
                                                                                CssClass="table table-responsive ChildGridTwo"
                                                                                GridLines="None"
                                                                                AutoGenerateColumns="false"
                                                                                Caption="Recommandation Details">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="DesignationName" HeaderText="Commitee Member Designation" NullDisplayText="-" />
                                                                                    <%--<asp:BoundField DataField="OverridingDesignationUserName" HeaderText="Overriding Designation" NullDisplayText="-" />--%>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Overriding Designation" >
                                                                                    <ItemTemplate>
                                                                                       <asp:Label  runat="server"  Text='<%# Eval("OverridingDesignationUserName") == null ? "-" :int.Parse(Eval("WasOverriden").ToString()) == 0 ? "-":Eval("OverridingDesignationUserName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                     </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Reccomandation" >
                                                                                    <ItemTemplate>
                                                                                       <asp:Label  runat="server" Text='<%# int.Parse(Eval("IsRecommended").ToString()) == 1 ? "Recommended" :  int.Parse(Eval("IsRecommended").ToString()) == 2 ? "Rejected" :"-" %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                     </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Reccommandation Overridden" >
                                                                                    <ItemTemplate>
                                                                                       <asp:Label  runat="server" Text='<%# int.Parse(Eval("WasOverriden").ToString()) == 1 ? "Yes" :  "No" %>'></asp:Label>
                                                                                         </ItemTemplate>
                                                                                     </asp:TemplateField>
                                                                                    <asp:BoundField DataField="RecommendedByName" HeaderText="Recommended By" NullDisplayText="-" />
                                                                                    <asp:TemplateField HeaderText="Recommended On" >
                                                                                    <ItemTemplate>
                                                                                       <%# (DateTime)Eval("RecommendedDate") == DateTime.MinValue ? "-" : Eval("RecommendedDate", "{0:dd MMMM yyyy}") %>
                                                                                    </ItemTemplate>
                                                                                     </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" NullDisplayText="-" />
                                                                                    
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="TabulationId"  HeaderText="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                <asp:TemplateField HeaderText="Bid Code" >
                                                                    <ItemTemplate>
                                                                       <asp:Label  runat="server" Text='<%# "BID" + Eval("BidCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Recommendation" >
                                                                    <ItemTemplate>
                                                                       <asp:Label  runat="server" Text='<%# int.Parse(Eval("IsRecommended").ToString()) == 1 ? "Recommended" : int.Parse(Eval("IsRecommended").ToString()) == 2 ? "Rejected":"-" %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Recommendation Overridden" >
                                                                    <ItemTemplate>
                                                                       <asp:Label  runat="server" Text='<%# int.Parse(Eval("RecommendationWasOveridden").ToString()) == 1 ? "Yes" : "No" %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Recommendation Overridden Designation" >
                                                                    <ItemTemplate>
                                                                       <asp:Label  runat="server"  Text='<%# int.Parse(Eval("RecommendationWasOveridden").ToString()) == 1 ? Eval("RecommendationOverridingDesignationUserName"):"-" %>'></asp:Label>
                                                                                     
                                                                        </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Recommendation Overridden By" >
                                                                    <ItemTemplate>
                                                                       <asp:Label  runat="server" Text='<%# Eval("RecommendationOveriddenByName") == null ? "-" :Eval("RecommendationOveriddenByName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Recommendation Overridden On" >
                                                                    <ItemTemplate>
                                                                       <%# (DateTime)Eval("RecommendationOveriddenOn") == DateTime.MinValue ? "-" : Eval("RecommendationOveriddenOn", "{0:dd MMMM yyyy}") %>
                                                                    </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:BoundField DataField="RecommendationOverridingRemarks" HeaderText="Recommendation Overridden Remark" NullDisplayText=" - " />
                                                                
                                                                
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                            <hr>

                        <h4>Tabulation Approval</h4>
                                                    <hr />
                                                    <div class="table-responsive">
                                                        <asp:GridView runat="server" ID="gvAppMaster"
                                                            EmptyDataText="No Record Found" GridLines="None"
                                                            CssClass="table table-responsive ChildGrid"
                                                            OnRowDataBound="gvAppMaster_RowDataBound"
                                                            AutoGenerateColumns="false" DataKeyNames="TabulationId">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <img alt=""
                                                                            style="cursor: pointer;margin-top: -6px;"
                                                                            src="images/plus.png" />
                                                                        <asp:Panel ID="pnlTabAppDetails" runat="server"
                                                                            Style="display: none">
                                                                            <asp:GridView ID="gvTabAppDetails" runat="server"
                                                                                CssClass="table table-responsive ChildGridTwo"
                                                                                GridLines="None"
                                                                                AutoGenerateColumns="false"
                                                                                Caption="Approval Details">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="DesignationName" HeaderText="Commitee Member Designation" NullDisplayText="-" />
                                                                                    <%--<asp:BoundField DataField="OverridingDesignationUserName" HeaderText="Overriding Designation" NullDisplayText="-" />--%>
                                                                                    
                                                                                    <asp:TemplateField HeaderText="Overriding Designation" >
                                                                                    <ItemTemplate>
                                                                                       <%--<asp:Label  runat="server" Text='<%# Eval("OverridingDesignationName") == null ? "-" :  Eval("OverridingDesignationName") %>'></asp:Label>--%>
                                                                                       <asp:Label  runat="server"  Text='<%# Eval("OverridingDesignationUserName") == null ? "-" :int.Parse(Eval("WasOverriden").ToString()) == 0 ? "-":Eval("OverridingDesignationUserName") %>'></asp:Label>
                                                                                       
                                                                                        </ItemTemplate>
                                                                                     </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Approval" >
                                                                                    <ItemTemplate>
                                                                                       <asp:Label  runat="server" Text='<%# int.Parse(Eval("IsApproved").ToString()) == 1 ? "Approved" :  int.Parse(Eval("IsApproved").ToString()) == 2 ? "Rejected" :"-" %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                     </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Approval Overridden" >
                                                                                    <ItemTemplate>
                                                                                       <asp:Label  runat="server" Text='<%# int.Parse(Eval("WasOverriden").ToString()) == 1 ? "Yes" :  "No" %>'></asp:Label>
                                                                                         </ItemTemplate>
                                                                                     </asp:TemplateField>
                                                                                    <asp:BoundField DataField="ApprovedByName" HeaderText="Approved By" NullDisplayText="-" />
                                                                                    <asp:TemplateField HeaderText="Approved On" >
                                                                                    <ItemTemplate>
                                                                                       <%# (DateTime)Eval("ApprovedDate") == DateTime.MinValue ? "-" : Eval("ApprovedDate", "{0:dd MMMM yyyy}") %>
                                                                                    </ItemTemplate>
                                                                                     </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" NullDisplayText="-" />
                                                                                    
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="TabulationId"  HeaderText="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                <asp:TemplateField HeaderText="Bid Code" >
                                                                    <ItemTemplate>
                                                                       <asp:Label  runat="server" Text='<%# "BID" + Eval("BidCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Approval" >
                                                                    <ItemTemplate>
                                                                       <asp:Label  runat="server" Text='<%# int.Parse(Eval("IsApproved").ToString()) == 1 ? "Approved" : int.Parse(Eval("IsApproved").ToString()) == 2 ? "Rejected":"-" %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Approval Overridden" >
                                                                    <ItemTemplate>
                                                                       <asp:Label  runat="server" Text='<%# int.Parse(Eval("ApprovalWasOveridden").ToString()) == 1 ? "Yes" : "No" %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Approval Overridden Designation" >
                                                                    <ItemTemplate>
                                                                       <asp:Label  runat="server"  Text='<%# int.Parse(Eval("ApprovalWasOveridden").ToString()) == 1 ? Eval("ApprovalOverridingDesignationUserName"):"-" %>'></asp:Label>
                                                                                     
                                                                        </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Approval Overridden By" >
                                                                    <ItemTemplate>
                                                                       <asp:Label  runat="server" Text='<%# Eval("ApprovalOverriddenByName") == null ? "-" :Eval("ApprovalOverriddenByName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Approval Overridden On" >
                                                                    <ItemTemplate>
                                                                       <%# (DateTime)Eval("ApprovalOverriddenOn") == DateTime.MinValue ? "-" : Eval("ApprovalOverriddenOn", "{0:dd MMMM yyyy}") %>
                                                                    </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:BoundField DataField="ApprovalOverridingRemarks" HeaderText="Approval Overridden Remark" NullDisplayText=" - " />
                                                                
                                                                
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                            <hr>


                                        <div class="row">
                                            <div class="col-xs-12">
                                                 <h4>POs Created</h4>
                                                    <hr />
                                                    <div class="table-responsive">
                                                        <asp:GridView runat="server" ID="gvPO"
                                                            EmptyDataText="No Record Found" GridLines="None"
                                                            CssClass="table table-responsive ChildGrid" HeaderStyle-BackColor="#3C8DBC"
                                                            HeaderStyle-ForeColor="White" 
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
                                                                <asp:BoundField DataField="CreatedDate"  HeaderText="Created Date"    DataFormatString="{0:dd MMMM yyyy}"/>
                                                                <asp:BoundField DataField="CreatedByName"  HeaderText="Created By"/>
                                                                <asp:TemplateField HeaderText="Approval Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
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
                                                                <%--<asp:BoundField DataField="ApprovedDate"  HeaderText="Approval Date"    DataFormatString="{0:dd MMMM yyyy}" NullDisplayText="Not Found"/>--%>
                                                                <asp:TemplateField HeaderText="Approval Date" >
                                                                    <ItemTemplate>
                                                                        <%# (DateTime)Eval("ApprovedDate") == DateTime.MinValue ? "-" : Eval("ApprovedDate", "{0:dd MMMM yyyy}") %>
                                                                    </ItemTemplate>
                                                                     </asp:TemplateField>
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

                                                 </div>
                                             </div>
                       <%--  <hr>--%>
                                      <%--  <div class="row">
                                            <div class="col-xs-12">
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
                                                                <asp:BoundField DataField="GoodReceivedDate"  HeaderText="Good Received Date"  DataFormatString="{0:dd MMMM yyyy}" />
                                                                <asp:BoundField DataField="CreatedDate"  HeaderText="Created Date"    DataFormatString="{0:dd MMMM yyyy}"/>
                                                                <asp:BoundField DataField="CreatedByName"  HeaderText="Created By"/>
                                                                <asp:TemplateField HeaderText="Approval Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
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
                                                                <%--<asp:BoundField DataField="ApprovedDate"  HeaderText="Approval Date"    DataFormatString="{0:dd MMMM yyyy}" NullDisplayText="Not Found"/>--%>
                                                               <%-- <asp:TemplateField HeaderText="Approval Date" >
                                                                    <ItemTemplate>
                                                                        <%# (DateTime)Eval("ApprovedDate") == DateTime.MinValue ? "-" : Eval("ApprovedDate", "{0:dd MMMM yyyy}") %>
                                                                    </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                <asp:BoundField DataField="ApprovedByName"  HeaderText="Approved By" NullDisplayText="Not Found"/>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
  </div>
                                             </div>--%>
                    
                </div>
                
               <div class="box-footer">
                            <asp:Button runat="server" ID="btnPrint"  Text="Print" CssClass="btn btn-success pull-right" OnClick="btnPrint_Click" />
                           <%--<div id="printerDiv"  style="display:none"></div>--%>
                        </div>






            </div>  
               
            </ContentTemplate>
        </asp:UpdatePanel>
        </form>
    </div>
    </section>
   
   
</asp:Content>
