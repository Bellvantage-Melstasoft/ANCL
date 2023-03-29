<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewQuotations.aspx.cs" Inherits="BiddingSystem.ViewQuotations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server" ViewStateMode="Enabled">

    <style type="text/css">
        .ChildGrid > tbody > tr > td:not(table) {
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
            background-color: #808080 !important;
        }

        .ChildGridThree td {
            text-align: left;
        }

        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        #ContentSection_gvQuotations > tbody {
            background-color: #fbfafa !important;
            text-align: left;
        }

        #ContentSection_gvQuotations th {
            text-align: center;
        }

        #ContentSection_gvBids th, #ContentSection_gvBids td {
            text-align: center;
        }


        #ContentSection_gvQuotations > tbody > tr:nth-child(2n+1) > td:not(table) {
            border-bottom: 1px solid #555555;
            border-top: 1px solid #f8f8f8;
        }

        #ContentSection_gvQuotationItems > tbody > tr:nth-child(2n+1) > td:not(table) {
            border-bottom: 1px solid #555555;
            border-top: 1px solid #f8f8f8;
        }

        #ContentSection_gvImpotsQuotations > tbody {
            background-color: #fbfafa !important;
            text-align: left;
            font-weight: bold;
        }

        #ContentSection_gvImpotsQuotations th {
            text-align: center;
        }


        #ContentSection_gvImpotsQuotations > tbody > tr:nth-child(2n+1) > td:not(table) {
            border-bottom: 1px solid #555555;
            border-top: 1px solid #f8f8f8;
        }

        .greenBg {
            background: #7bf768;
            font-weight: bold;
            cursor: pointer;
            text-align: right;
        }

        .CellClick {
            font-weight: bold;
            cursor: pointer;
            text-align: right;
        }

        .footer-font {
            font-weight: bold;
            background-color: yellowgreen;
            text-align: left !important;
        }

        .alignright {
            text-align: right !important;
            font-weight: bold;
        }

        caption {
            font-weight: bold;
            font-size: large;
        }
        .btnMargin {
                margin-right: 10px;
            }
    </style>

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

    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <section class="content-header">
        <h1>
           View Quotation To Approve Tabulaton Sheet
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">View Quotation </li>
        </ol>
    </section>
    <br />

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>


                 <!-- Start : Purchased Items Modal -->
                <div id="mdlItems" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 60%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                           

                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->


                            <div class="modal-body">

                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="text-green text-bold">PURCHASED ITEMS</h4>
                                            </div>
                                        </div>
                                        <hr />     
                                <div class="row">
                                    <div class="col-xs-12">


                                        <div>
                                            &nbsp
                                        </div>

                                        <!-- Start : Items Table -->
                                        <div style="color: black;">

                                            <asp:GridView ID="gvPurchasedItems" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No Purchase History records."
                                                CssClass="table gvPurchasedItems"
                                                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>

                                                    <asp:BoundField DataField="ItemId"
                                                        HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden"
                                                        ItemStyle-CssClass="hidden" />

                                                    <asp:BoundField DataField="Department_Name" HeaderText="Company Name" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                    <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price" />
                                                    <asp:BoundField DataField="PoCode" HeaderText="PO Code" />
                                                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />

                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                        <!-- End : Quotation Table -->
                                    </div>
                                </div>
                                        </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Purchased Items Modal -->


                <!-- Start : Attachment Modal -->
                <div id="mdlAttachments" class="modal fade" role="dialog" >
                    <div class="modal-dialog">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                           
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                  <div class="panel panel-default">
                                    <div class="panel-body"> 
                                        <div class="row">
                                            <div class="col-md-12">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="text-green text-bold">ATTACHMENT QUOTATIONS</h4>
                                            </div>
                                        </div>
                                        <hr />     
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <asp:Panel ID="pnlImages" runat="server">
                                                <label for="fileImages">Uploded Images</label>
                                                <asp:GridView ID="gvImages" runat="server" ShowHeader="False"
                                                    GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Image Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="QuotationImageId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink runat="server" href='<%#Eval("ImagePath").ToString().Remove(0,2)%>' Target="_blank">
                                                                            <asp:Image runat="server" ImageUrl='<%#Eval("ImagePath")%>' style="max-height:50px; width:auto; margin:5px" />
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group">
                                            <asp:Panel ID="pnlDocs" runat="server" Width="100%">
                                                <label for="fileImages">Uploded Documents</label>
                                                <asp:GridView ID="gvDocs" runat="server" ShowHeader="False"
                                                    GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Document Found" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="QuotationFileId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField ItemStyle-Height="30px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton
                                                                    Text='<%#Eval("FileName")%>' runat="server" href='<%#Eval("FilePath").ToString().Remove(0,2)%>' target="_blank" Style="margin-right: 5px;" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group">
                                            <asp:Panel ID="pnlcondtion" runat="server" Width="100%">
                                                <label for="fileImages">Terms And Conditons</label>
                                                <asp:TextBox TextMode="MultiLine" Rows="10" ID="txtTermsAndConditions" Enabled="false" runat="server" CssClass="form-control text-bold"></asp:TextBox>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                        </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>





                <section class="content" style="padding-top: 0px">
                    <asp:Panel ID="pnlPR" runat="server">
                    <div class="row">
                        <div class="col-xs-12">
                            <!-- Start : Box -->
                            <div class="box box-info">
                                <!-- Start : Box Header-->
                                <div class="box-header with-border">
                                    <h3 class="box-title">Purchase Request Details</h3>
                                </div>
                                <!-- End : Box Header -->
                                <!-- Start : Box Body-->
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
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvBids" runat="server" CssClass="table table-responsive"
                                                    OnRowDataBound="gvBids_RowDataBound" GridLines="None"
                                                    AutoGenerateColumns="false" DataKeyNames="BidId" Caption="Bids for Purchase Request"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                                    src="images/plus.png" />
                                                                <asp:Panel ID="pnlBidItems" runat="server" Style="display: none">
                                                                    <asp:GridView ID="gvBidItems" runat="server"
                                                                        CssClass="table table-responsive ChildGrid"
                                                                        GridLines="None" AutoGenerateColumns="false"
                                                                        Caption="Items in Bid">
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
                                                                                HeaderText="PRDId" HeaderStyle-CssClass="hidden"
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
                                                                            <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                                                            <asp:BoundField DataField="EstimatedPrice"
                                                                                HeaderText="Estimated Price" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="PrId" HeaderText="PrId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="Bid Code"
                                                            HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# "B"+Eval("BidCode").ToString() %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CreatedUserName" HeaderText="Created By" />
                                                        <asp:BoundField DataField="CreateDate" HeaderText="Created Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>'/>
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
                                                        <asp:BoundField DataField="NoOfQuotations" HeaderText="Qutations Count" />
                                                        <asp:BoundField DataField="NoOfRejectedQuotations" HeaderText="Rejected Tabulation Count" />
                                                        <asp:TemplateField HeaderText="Is Tabulation Selected">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsQuotationSelected").ToString() =="1" ? "Selected":"Not Selected" %>'
                                                                    ForeColor='<%# Eval("IsQuotationSelected").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>
                                                                
                                                                 <asp:Button CssClass="btn btn-xs btn-info" runat="server" Text="View"
                                                                    style="margin-top:3px; width:100px;" OnClick="btnView_Click"></asp:Button>
                                                                
                                                                 <%-- <asp:Button CssClass="btn btn-xs btn-warning" runat="server"
                                                                   ID="btnReset" Text="Reset" OnClick="btnReset_Click"
                                                                    style="margin-top:3px; width:100px;" Visible="false"></asp:Button>--%>
                                                                  
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    
                                </div>
                                <!-- End : Box Body -->
                                <!-- Start : Box Footer -->
                                <div class="box-footer">
                                    <asp:Button ID="btnReject2" runat="server" Text="Reject" CssClass="btn btn-danger  pull-right " OnClientClick="reject()"></asp:Button>
                                    <asp:Button ID="btnapprove1" runat="server" Text="Approve" CssClass="btn btn-success pull-right btnMargin" OnClientClick="approve()" ></asp:Button>
                                    
                                     </div>
                                <!-- End : Box Footer -->
                            </div>
                            <!-- End : Box -->

                       
                        </div>
                    </div>
                        </asp:Panel>

                    <asp:Panel ID="pnlTabulationSheet" runat="server">
                        <div class="row">
                        <div class="col-xs-12">
                            <!-- Start : Box -->
                            <div class="box box-info">
                                <!-- Start : Box Header-->
                                <div class="box-header with-border">
                                    <h3 class="box-title">Purchase Request Details</h3>
                                </div>
                                <div class="box-body">
                            
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-responsive">
                                                
                                            <asp:GridView ID="gvItems" runat="server"
                                                CssClass="table gvItems"
                                                GridLines="None" AutoGenerateColumns="false" ShowHeader="false" OnRowDataBound="gvItems_RowDataBound" DataKeyNames="BiddingItemId" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
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
                                                                <td scope="col">Quantity</td>
                                                                <td scope="col">Estimated Price</td>
                                                                <td scope="col">Quotations Count</td>
                                                                <td class="hidden" scope="col">LastSupplierId</td>
                                                                <td scope="col">Last Purchased Supplier</td>
                                                                <td scope="col">Last Purchased Price</td>
                                                                <td scope="col">Actions</td>
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
                                                    <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                                    <asp:BoundField DataField="EstimatedPrice"
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
                                                           
                                                            <asp:Button CssClass="btn btn-xs btn-info btnPurchased" runat="server"
                                                                ID="btnPurchased" Text="Purchase History" OnClick="btnViewItems_Click"
                                                                Style="margin-top: 3px; width: 100px;"></asp:Button>
                                                            

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td colspan="100%">

                                                                    <asp:Panel ID="pnlQuotationItems" runat="server" Style="margin-left: 40px; overflow-x: auto;">
                                                                        <asp:GridView ID="gvQuotationItems" runat="server"
                                                                            CssClass="table table-responsive"
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
                                                                                <asp:BoundField DataField="ReferenceNo" HeaderText="Reference Code" NullDisplayText="Unavailable" />
                                                                                <asp:BoundField DataField="Description" HeaderText="Description" NullDisplayText="-" />
                                                                             
                                                                                <asp:BoundField DataField="UnitPrice" HeaderText="Quoted Price" />
                                                                                <asp:BoundField DataField="SubTotal" HeaderText="Sub-Total" />
                                                                                <asp:BoundField DataField="NbtAmount" HeaderText="NBT" />
                                                                                <asp:BoundField DataField="VatAmount" HeaderText="VAT" />
                                                                                <asp:BoundField DataField="NetTotal" HeaderText="Net-Total" />
                                                                                <asp:BoundField DataField="SpecComply" HeaderText="Complies Specs" />

                                                                                 <asp:TemplateField HeaderText="Attachments" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                       <asp:Button CssClass="btn btn-xs btn-info btnViewAttachmentsClassA" OnClick="btnViewAttachments_Click" runat="server"
                                                                                        Text="View"></asp:Button>
                                                                                 </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Supplier Details" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                       <asp:Button CssClass="btn btn-xs btn-info" ID="btnsupplerview" OnClick="btnsupplerview_Click" runat="server"
                                                                                        Text="view" Style="margin-right: 4px;"></asp:Button>
                                                                                 </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                      
                                                                                        <asp:Button CssClass="btn btn-xs btn-danger" runat="server" Text="Select to Reject" OnClientClick='<%#"ItemReject(event,"+Eval("QuotationItemId").ToString()+")" %>' Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button>
                                                                                       
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
                                        </div>
                                    </div>
                                   
                                </div>
                                <div class="box-footer">
                                   
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary  pull-right btnMargin" OnClick="btnBack_Click"></asp:Button>
                                </div>
                                
                                <!-- End : Box Footer -->
                            </div>
                            <!-- End : Box -->

                       
                        </div>
                    </div>
                        </asp:Panel>
                </section>
                <!-- End : Section -->

                <!-- Start : Hidden Fields -->
              
                <asp:HiddenField ID="hdnRejectedQuotationCount" runat="server" />
                <asp:HiddenField ID="hdnQuotationItemId" runat="server" />
                <asp:HiddenField ID="hdnQuotationItemRejectRemark" runat="server" />
                <asp:Button ID="BtnItemReject" runat="server" OnClick="btnReject_Click" CssClass="hidden" />
                <asp:HiddenField ID="hdnRemarks" runat="server" />
                 <asp:Button ID="btnApproveQ" runat="server" OnClick="lbtnApprove_Click" CssClass="hidden" />
                <asp:Button ID="btnRejectQ" runat="server" OnClick="lbtnReject_Click" CssClass="hidden" />
                <%--  <asp:Button ID="btnSelect" runat="server" OnClick="btnSelect_Click" CssClass="hidden" />
                <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" CssClass="hidden" />--%>
                
                <!-- End : Hidden Fields -->

            </ContentTemplate>
            
        </asp:UpdatePanel>

    </form>

    <script type="text/javascript">
        Sys.Application.add_load(function () {


        });



        function approve() {

            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to <strong>Approve</strong> the Bid?</br>"
                + "<strong id='dd'>Remarks</strong>"
                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Approve',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                    if ($('#ss').val() == '') {
                        $('#dd').prop('style', 'color:red');
                        swal.showValidationError('Remarks Required');
                        return false;
                    }
                    else {
                        $('#ContentSection_hdnRemarks').val($('#ss').val());
                    }

                }
            }
            ).then((result) => {
                if (result.value) {

                    $('#ContentSection_btnApproveQ').click();

                } else if (result.dismiss === Swal.DismissReason.cancel) {

                }
            });


        }

        function reject() {

            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to <strong>Reject</strong> the Bid?</br></br>"
                + "You have Selected <strong>" + $("#ContentSection_hdnRejectedQuotationCount").val() + "</strong> quotations to reject</br></br>"
                + "<strong id='dd'>Remarks</strong>"
                + "<input id='ss' type='text' class ='form-control' value='Rejected' required='required'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Reject',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                    if ($('#ss').val() == '') {
                        $('#dd').prop('style', 'color:red');
                        swal.showValidationError('Remarks Required');
                        return false;
                    }
                    else {
                        $('#ContentSection_hdnRemarks').val($('#ss').val());
                    }

                }
            }
            ).then((result) => {
                if (result.value) {


                    $('#ContentSection_btnRejectQ').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {

                }
            });


        }

        function ItemReject(e, QuotationItemId) {
            e.preventDefault();
            $('#ContentSection_hdnQuotationItemId').val(QuotationItemId);

            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to <strong>Reject</strong> the Quotation?</br></br>"
                + "<strong id='dd'>Remarks</strong>"
                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Reject',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                    if ($('#ss').val() == '') {
                        $('#dd').prop('style', 'color:red');
                        swal.showValidationError('Remarks Required');
                        return false;
                    }
                    else {
                        $('#ContentSection_hdnQuotationItemRejectRemark').val($('#ss').val());
                    }

                }
            }
            ).then((result) => {
                if (result.value) {


                    $('#ContentSection_BtnItemReject').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {

                }
            });


        }


    </script>
   

</asp:Content>
