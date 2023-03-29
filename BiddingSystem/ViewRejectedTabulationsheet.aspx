<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewRejectedTabulationsheet.aspx.cs" Inherits="BiddingSystem.ViewRejectedTabulationsheet" %>

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

        .ChildGridOne0 td {
            background-color: #f5e8e8 !important;
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
            View Rejected Quotations 
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">View Rejected Quotation</li>
        </ol>
    </section>
    <br />
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content" style="padding-top: 0px">
                    <div class="box box-info" id="panelPurchaseRequset" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">View Rejected Quotation Tabulation Sheet</h3>

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

                                                <asp:TemplateField HeaderText="Bid Item">
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                                                        <asp:Panel ID="pnlBids" runat="server" Style="display: none">
                                                            <asp:GridView ID="gvBids" runat="server" CssClass="table table-responsive ChildGrid" OnRowDataBound="gvBids_RowDataBound"
                                                                GridLines="None" AutoGenerateColumns="false" DataKeyNames="BidId" Caption="Bids for Purchase Request">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Items">
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
                                                                                        <asp:BoundField DataField="Qty"
                                                                                            HeaderText="Quantity" />
                                                                                        <asp:BoundField DataField="EstimatedPrice"
                                                                                            HeaderText="Estimated Price" />
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quotations">
                                                                        <ItemTemplate>
                                                                            <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                                                src="images/plus.png" />
                                                                            <asp:Panel ID="pnlSupplierQuotationItem" runat="server"
                                                                                Style="display: none">
                                                                                <asp:GridView runat="server" ID="gvSupplierQuotation" GridLines="None" CssClass="table table-responsive ChildGridOne0" AutoGenerateColumns="false" Caption="Quotation for bids"
                                                                                OnRowDataBound="gvSupplierQuotation_RowDataBound" EmptyDataText="No Quotation Found" DataKeyNames="QuotationId" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                                                    <Columns>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                        <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                                                            src="images/plus.png" />
                                                                                            <asp:Panel ID="pnlQuotationItems" runat="server" Style="display: none">
                                                                                                <asp:GridView runat="server" ID="gvQuotationItems" GridLines="None"
                                                                                                    CssClass="table table-responsive gvQuotationItems" AutoGenerateColumns="false" HeaderStyle-BackColor="#f8f8f8" HeaderStyle-ForeColor="black"
                                                                                                    DataKeyNames="QuotationItemId" Caption="Quotation Items">
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="QuotationItemId" HeaderText="QuotaionItemId"
                                                                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="QuotationItemId hidden" />
                                                                                                        <asp:BoundField DataField="QuotationId" HeaderText="QuotationId"
                                                                                                            HeaderStyle-CssClass="QuotationId hidden" ItemStyle-CssClass="QuotationId hidden" />
                                                                                                        <asp:BoundField DataField="BiddingItemId" HeaderText="BidItemId"
                                                                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="BiddingItemId hidden" />
                                                                                                        <asp:BoundField DataField="CategoryId" HeaderText="Category Id"
                                                                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="CategoryId hidden" />
                                                                                                        <asp:BoundField DataField="CategoryName" HeaderText="Category Name"
                                                                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="CategoryName hidden" />
                                                                                                        <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategory Id"
                                                                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="SubCategoryId hidden" />
                                                                                                        <asp:BoundField DataField="SubCategoryName" HeaderText="Sub-Category Name"
                                                                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="SubCategoryName hidden" />
                                                                                                        <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="ItemId hidden" />
                                                                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name"
                                                                                                            ItemStyle-Width="100px" HeaderStyle-Width="100px" ItemStyle-CssClass="ItemName" />
                                                                                                        <asp:BoundField DataField="Description" HeaderText="Description"
                                                                                                            ItemStyle-Width="100px" HeaderStyle-Width="100px" />
                                                                                                        <asp:BoundField DataField="Qty" HeaderText="Quantity" ItemStyle-CssClass="Qty" />
                                                                                                        <asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price" />
                                                                                                        <asp:BoundField DataField="UnitPrice" HeaderText="Quoted Price" />
                                                                                                        <asp:BoundField DataField="SubTotal" HeaderText="Subtotal" />
                                                                                                        <asp:BoundField DataField="NbtAmount" HeaderText="NBT" />
                                                                                                        <asp:BoundField DataField="VatAmount" HeaderText="VAT" />
                                                                                                        <asp:BoundField DataField="TotalAmount" HeaderText="Net Total" />  
                                                                                                       <asp:TemplateField HeaderText="Status">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label runat="server" Text='<%# Eval("IsQuotationItemApproved").ToString() =="0"? "Pending-Ok" :Eval("IsQuotationItemApproved").ToString() =="1" ?  "Approved" :"Rejected" %>'
                                                                                                                ForeColor='<%# Eval("IsQuotationItemApproved").ToString() !="2" ?System.Drawing.Color.Green: System.Drawing.Color.Red %>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>   
                                                                                                        <asp:BoundField DataField="IsQuotationItemApprovalRemark" HeaderText="Remark" /> 
                                                                                                        
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </asp:Panel>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="QuotationId hidden" />                                    
                                                                                        <asp:BoundField DataField="QuotationReferenceCode" HeaderText="Ref No" ItemStyle-Font-Bold="true" ItemStyle-CssClass="QuotationReferenceCode" />
                                                                                        <asp:BoundField DataField="SubTotal" HeaderText="Sub Total" ItemStyle-Font-Bold="true" />
                                                                                        <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount" ItemStyle-Font-Bold="true" />
                                                                                        <asp:BoundField DataField="VatAmount" HeaderText="VAT Amount" ItemStyle-Font-Bold="true" />
                                                                                        <asp:BoundField DataField="NetTotal" HeaderText="Net Total" ItemStyle-Font-Bold="true" />
                                                                                       <asp:TemplateField HeaderText="Status">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label runat="server" Text='<%# Eval("IsQuotationTabulationApproved").ToString()  =="0" ? "Pending-Ok":Eval("IsQuotationTabulationApproved").ToString()  =="1" ? "Approved" : "Rejected" %>'
                                                                                                ForeColor='<%# Eval("IsQuotationTabulationApproved").ToString() !="2" ?System.Drawing.Color.Green: System.Drawing.Color.Red %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                                </asp:Panel>
                                                                            </ItemTemplate>                                                                        
                                                                    </asp:TemplateField>

                                                                    <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                                        HeaderStyle-CssClass="BidId hidden"
                                                                        ItemStyle-CssClass="BidId hidden" />
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
                                                                     <asp:BoundField DataField="TabulationreviewApprovalRemark"  HeaderText="Rejected Reason"  ItemStyle-ForeColor="red"/>    

                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:Button CssClass="btn btn-sm btn-primary" runat="server" ID="lbtnView"
                                                                                Text="View"  OnClick="btnView_Click" OnClientClick="showLoadingImage(this)">     
                                                                            </asp:Button>
                                                                            <%--<asp:Image  runat="server" ID="loadingImage" class="loadingImage hidden"  src="AdminResources/images/Spinner-0.6s-200px.gif" style="max-height: 40px" />--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:Button CssClass="btn btn-sm btn-success" runat="server" ID="lbtnConfirmSubmission"
                                                                            Text="Confirm Submission"  OnClick="lbtnConfirmSubmission_Click" OnClientClick="showLoadingImage(this)">     
                                                                        </asp:Button>
                                                                        <asp:Image  runat="server" ID="loadingImage" class="loadingImage hidden"  src="AdminResources/images/Spinner-0.6s-200px.gif" style="max-height: 40px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                
                                                <asp:BoundField DataField="PrId"  HeaderText="PrId" HeaderStyle-CssClass="PrId hidden" ItemStyle-CssClass="PrId hidden"/>
                                                <asp:TemplateField HeaderText="PR Code">
							                        <ItemTemplate>
								                        <asp:Label runat="server" Text='<%# "PR"+Eval("PrCode").ToString() %>'></asp:Label>
							                        </ItemTemplate>
						                        </asp:TemplateField>
                                                <asp:BoundField DataField="PrCategoryId"  HeaderText="PR Category Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="WarehouseName"  HeaderText="Warehouse"/>
                                                <asp:BoundField DataField="PrCategoryName"  HeaderText="Category Name"/>
                                                 <asp:BoundField DataField="RequiredFor"  HeaderText="Description" />
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Created On"
                                                                             DataFormatString='<%$ appSettings:datePattern %>' />
                                                <asp:BoundField DataField="CreatedByName"  HeaderText="Created By" />                   
                                                 <asp:TemplateField HeaderText="PR Type">
                                                                         <ItemTemplate>
                                                                             <asp:Label runat="server" ID="lblprtype" Text='<%#Eval("PrType").ToString()=="1"? "Stock":"Non-Stock"%>' ForeColor='<%#Eval("PrType").ToString()=="1"? System.Drawing.Color.Maroon:System.Drawing.Color.Navy%>'></asp:Label>
                                                                         </ItemTemplate>
                                                                </asp:TemplateField>   
                                                <asp:TemplateField HeaderText="Purchasing Type">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PurchaseType").ToString() == "1" ? true : false %>'
                                            Text="Local" CssClass="label label-warning"/>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PurchaseType").ToString() == "2" ? true : false %>'
                                            Text="Import" CssClass="label label-success"/>
                                    </ItemTemplate>
                                </asp:TemplateField>      
                                                 <asp:TemplateField HeaderText="Expense Type">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("ExpenseType").ToString() == "1" ? true : false %>'
                                            Text="Capital" CssClass="label label-warning"/>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("ExpenseType").ToString() == "2" ? true : false %>'
                                            Text="Operational" CssClass="label label-success"/>
                                    </ItemTemplate>                                      
                                </asp:TemplateField>   
                                               <%-- <asp:BoundField DataField="IsTabulationReviewApprovalRemark"  HeaderText="Rejected Reason"  ItemStyle-ForeColor="red"/>    --%>
                                                <%--<asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button CssClass="btn btn-sm btn-success" runat="server" ID="lbtnConfirmSubmission"
                                                            Text="Confirm Submission"  OnClick="lbtnConfirmSubmission_Click" OnClientClick="showLoadingImage(this)">     
                                                        </asp:Button>
                                                        <asp:Image  runat="server" ID="loadingImage" class="loadingImage hidden"  src="AdminResources/images/Spinner-0.6s-200px.gif" style="max-height: 40px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
        function showLoadingImage(obj) {
            $(obj).parent().find("img").removeClass("hidden")
        }
    </script>
</asp:Content>
