<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ApproveQuotations.aspx.cs" Inherits="BiddingSystem.ApproveQuotations" %>

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


        #ContentSection_gvQuotations > tbody > tr:nth-child(2n+1) > td:not(table) {
            border-bottom: 1px solid #555555;
            border-top: 1px solid #f8f8f8;
        }

        a.disabled {
            pointer-events: none!important;
            cursor: default!important;
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
            Quotation Aprroval
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
            <li class="active">Bid Comparison </li>
        </ol>
    </section>
    <br />



    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="modal modal-primary fade" id="ApproveModel">
                    <div class="modal-dialog">
                        <div class="modal-content" style="background-color: White; width: 80%">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" style="text-align: center;">Approval By Authorities</h4>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                                <div class="row">
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">
                                    Close</button>
                                <%--  <button type="button" class="btn btn-outline">Submit</button>--%>
                            </div>
                        </div>
                    </div>
                </div>




                <!-- Start : Attachment Modal -->
                <div id="mdlAttachments" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Attachments Quotations</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
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
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Attachment Modal -->

                <!-- Start : Quotations Modal -->
                <div id="mdlQuotations" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 95%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Submtted Quotations</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <!-- Start : Quotation Table -->
                                        <div class="table-responsive" style="color: black;">
                                            <asp:GridView runat="server" ID="gvQuotations" GridLines="None"
                                                CssClass="table table-responsive" AutoGenerateColumns="false"
                                                OnRowDataBound="gvQuotations_RowDataBound" DataKeyNames="QuotationId" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <span style="font: bold;">
                                                                <%#Container.DataItemIndex + 1%>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="BidId" HeaderText="BidId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SupplierId" HeaderText="SupplierId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ItemStyle-Font-Bold="true" />
                                                    <asp:BoundField DataField="SubTotal" HeaderText="Sub Total" ItemStyle-Font-Bold="true" />
                                                    <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount" ItemStyle-Font-Bold="true" />
                                                    <asp:BoundField DataField="VatAmount" HeaderText="VAT Amount" ItemStyle-Font-Bold="true" />
                                                    <asp:BoundField DataField="NetTotal" HeaderText="Net Total" ItemStyle-Font-Bold="true" />
                                                    <asp:BoundField DataField="TermsAndCondition" HeaderText="Terms And Condition" NullDisplayText="-" ItemStyle-Font-Bold="true" />
                                                       <asp:TemplateField HeaderText="Tech Committee Recommendation ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltecrec" runat="server" Text='<%#(Eval("IsUploadeded").ToString()=="1" &&Eval("RecomondationType").ToString()=="2"&&Eval("IsSelected").ToString()=="1")?"Tech Committee Recommendation Uploaded":(Eval("IsUploadeded").ToString()=="1" &&Eval("RecomondationType").ToString()=="2"&&Eval("IsSelected").ToString()=="1")?"Tech Committee Recommendation Should Upload":"Not need of Documents Upload"%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Attachments">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-default btnViewAttachmentsCl" runat="server"
                                                                Text="View"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Supplier Details">
                                                    <ItemTemplate>
                                                        <asp:Button CssClass="btn btn-xs btn-success btnSelectCl" ID="btnsupplerview" OnClick="btnsupplerview_Click" runat="server" 
                                                    Text="view" Style="margin-right: 4px;"></asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-sm btn-success btnSelectCl" runat="server" Visible='<%#Eval("Actions").ToString() =="1"?true:false%>'
                                                                Text="Select" Style="margin-right: 4px;"></asp:Button>
                                                            <asp:Button CssClass="btn btn-sm btn-danger btnRejectCl" runat="server" Visible='<%#Eval("Actions").ToString() =="1" && Eval("ShowDelete").ToString() =="1"?true:false%>'
                                                                Text="Reject"></asp:Button>
                                                            <span style='color: red; <%#Eval("Actions").ToString() =="1"?"Display:none;": ""%>'><b>Reject previous to select this</b></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#Eval("IsSelected").ToString() =="1"?"Selected": "Rejected"%>' Font-Bold="true" Style='<%#Eval("IsSelected").ToString() =="1"?"color: #3C8DBC": "color: Red"%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SelectionRemarks" HeaderText="Remarks" NullDisplayText="-" ItemStyle-Font-Bold="true" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td colspan="100%">
                                                                    <asp:Panel ID="pnlQuotationItems" runat="server" Style="margin-left: 40px;">
                                                                        <asp:GridView runat="server" ID="gvQuotationItems" GridLines="None"
                                                                            CssClass="table table-responsive ChildGridTwo" AutoGenerateColumns="false"
                                                                            OnRowDataBound="gvQuotationItems_RowDataBound" DataKeyNames="QuotationItemId" Caption="Quotation Items">
                                                                            <Columns>

                                                                                <asp:BoundField DataField="QuotationItemId" HeaderText="QuotaionItemId"
                                                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                <asp:BoundField DataField="QuotationId" HeaderText="QuotationId"
                                                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                <asp:BoundField DataField="BiddingItemId" HeaderText="BidItemId"
                                                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                <asp:BoundField DataField="CategoryId" HeaderText="Category Id"
                                                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                <asp:BoundField DataField="CategoryName" HeaderText="Category Name"
                                                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategory Id"
                                                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                <asp:BoundField DataField="SubCategoryName" HeaderText="Sub-Category Name"
                                                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name"
                                                                                    ItemStyle-Width="100px" HeaderStyle-Width="100px" />
                                                                                <asp:TemplateField HeaderText="Quantity">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtQty" Text='<%#Eval("Qty")%>' Enabled='<%#Eval("EnableFields").ToString() =="1"?true:false%>'
                                                                                            type="number" min="1" runat="server" Width="80px"
                                                                                            autocomplete="off" CssClass="txtQtyCl" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price" />

                                                                                <asp:TemplateField HeaderText="Quoted Price">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtUnitPrice" Text='<%#Eval("UnitPrice")%>'
                                                                                            type="number" step="any" min="0" runat="server" Width="80px"
                                                                                            autocomplete="off" CssClass="txtUnitPriceCl" Enabled="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Negotiate Price">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtNegotiatePrice" Text='<%#Eval("UnitPrice")%>' Enabled='<%#Eval("EnableFields").ToString() =="1"?true:false%>'
                                                                                            type="number" step="any" min="0" runat="server" Width="80px"
                                                                                            autocomplete="off" CssClass="txtNegotiatePriceCl" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Sub Total">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtSubTotal" Enabled="false" Text='<%#Eval("SubTotal")%>'
                                                                                            runat="server" Width="80px" CssClass="txtSubTotalCl" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Include NBT/VAT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkNbt" Text="NBT" runat="server" Style="cursor: pointer"
                                                                                            Checked='<%#Eval("HasNbt").ToString() =="1"?true:false%>'
                                                                                            CssClass="chkNbtCl" /><br />
                                                                                        <asp:CheckBox ID="chkVat" Text="VAT" runat="server" Style="cursor: pointer"
                                                                                            Checked='<%#Eval("HasVat").ToString() =="1"?true:false%>'
                                                                                            CssClass="chkVatCl" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="NBT Percentage" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                    <ItemTemplate>
                                                                                        <asp:RadioButton ID="rdoNbt204" GroupName="grpPercentage" Style="cursor: pointer"
                                                                                            Text="2.04%" runat="server" Checked='<%#Eval("NbtCalculationType").ToString() =="1"?true:false%>'
                                                                                            CssClass="rdo204" /><br />
                                                                                        <asp:RadioButton ID="rdoNbt2" GroupName="grpPercentage" Style="cursor: pointer"
                                                                                            Text="2.00%" runat="server" Checked='<%#Eval("NbtCalculationType").ToString() =="2"?true:false%>'
                                                                                            CssClass="rdo2" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="NBT">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtNbt" Enabled="false" Text='<%#Eval("NbtAmount")%>'
                                                                                            runat="server" Width="80px" CssClass="txtNbtCl" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="VAT">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtVat" Enabled="false" Text='<%#Eval("VatAmount")%>'
                                                                                            runat="server" Width="80px" CssClass="txtVatCl" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Net Total">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtNetTotal" Enabled="false" Text='<%#Eval("NetTotal")%>'
                                                                                            runat="server" Width="80px" CssClass="txtNetTotalCl" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderStyle-Width="200px"
                                                                                    ItemStyle-Width="200px" HeaderText="Item Specification">
                                                                                    <ItemTemplate>
                                                                                        <asp:Panel ID="pnlSpecs" runat="server">
                                                                                            <asp:GridView ID="gvSpecs" runat="server" CssClass="ChildGridThree"
                                                                                                ShowHeader="False" GridLines="None"
                                                                                                AutoGenerateColumns="false" EmptyDataText="No Specification Found">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderStyle-Width="100px"
                                                                                                        ItemStyle-Width="100px">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:CheckBox Enabled="false" ToolTip="Check if comply" Style="cursor: pointer"
                                                                                                                ID="chkSpec" Text='<%#Eval("Material")%>'
                                                                                                                runat="server" CssClass="chkSpec"
                                                                                                                Checked='<%#Eval("Comply").ToString() =="1"?true:false%>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                   
                                                                                                    <asp:BoundField DataField="Description"
                                                                                                        ItemStyle-HorizontalAlign="Right"
                                                                                                        HeaderStyle-Width="100px"
                                                                                                        ItemStyle-Width="100px" />
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </asp:Panel>
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
                                        <!-- End : Quotation Table -->
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Quotations Modal -->

                <!-- Start : Section -->
                <section class="content">
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
                                                            DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                            DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                            DataFormatString="{0:dd-MM-yyyy}" />
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
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="btnView" Text="View Quotations" OnClick="btnView_Click"
                                                                    style="width:120px;"></asp:Button>
                                                                <asp:Button CssClass="btn btn-xs btn-success btnApproveCl" runat="server"
                                                                    ID="btnApproveQ" Text="Approve Selection"
                                                                    style="margin-top:3px; width:120px;"></asp:Button>
                                                                <asp:Button CssClass="btn btn-xs btn-danger btnRejectCl" runat="server"
                                                                    ID="btnRejectQ" Text="Reject Selection"
                                                                    style="margin-top:3px; width:120px;"></asp:Button>
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
                                    <a class="btn btn-info pull-right" href="ViewPrForQuotationApproval.aspx"
                                        style="margin-right:10px">Done</a>
                                </div>
                                <!-- End : Box Footer -->
                            </div>
                            <!-- End : Box -->
                        </div>
                    </div>
                </section>
                <!-- End : Section -->

                <!-- Start : Hidden Fields -->
                <asp:HiddenField ID="hdnSubTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNbtTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnVatTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNetTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnApprovalRemarks" runat="server" />
                <asp:HiddenField ID="hdnRejectRemarks" runat="server" />
                <asp:HiddenField ID="hdnBidId" runat="server" />
                <asp:HiddenField ID="hdnQuotationId" runat="server" />
                <asp:HiddenField ID="hndDesignationId" runat="server" />
                  <asp:HiddenField ID="hndUserId" runat="server" />
                <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" CssClass="hidden" />
                <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" CssClass="hidden" />
                <asp:Button ID="btnViewAttachments" runat="server" OnClick="btnViewAttachments_Click" CssClass="hidden" />
                <!-- End : Hidden Fields -->

            </ContentTemplate>
        </asp:UpdatePanel>

    </form>

    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $(function () {

                $('.btnViewAttachmentsCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlQuotations').modal('hide');

                        var tableRow = $('#ContentSection_gvQuotations').find('> tbody > tr > td:not(table)');
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());

                        $('#ContentSection_btnViewAttachments').click();
                    }
                });

                $('#mdlAttachments').on('hide.bs.modal', function () {
                    $('.modal-backdrop').remove();
                    $('#mdlQuotations').modal('show');
                });

                $('#mdlQuotations').on('shown.bs.modal', function () {
                    $('#mdlQuotations').css('overflow', 'auto');
                    $('body').css("overflow", "hidden");
                    $('body').css("padding-right", "0");
                })

                $('#mdlQuotations').on('hidden.bs.modal', function () {
                    $('body').css("overflow", "auto");
                    $('body').css("padding-right", "0");
                })

                $('.btnApproveCl').on({
                    click: function () {
                        event.preventDefault();
                        debugger;
                        var tableRow = $(this).closest('tr').find('> td:not(table)');

                        $('#ContentSection_hdnBidId').val((tableRow).eq(1).html());

                        //swal.fire({
                        //    title: 'Are you sure?',
                        //    html: "Are you sure you want to <strong>Approve</strong> selected quotation for the bid?</br></br>"
                        //        + "<strong id='dd'>Remarks</strong>"
                        //        + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                        //    type: 'warning',
                        //    cancelButtonColor: '#d33',
                        //    showCancelButton: true,
                        //    showConfirmButton: true,
                        //    confirmButtonText: 'Yes. Select It!',
                        //    cancelButtonText: 'No',
                        //    allowOutsideClick: false,
                        //    preConfirm: function () {
                        //        debugger;
                        //        $('#ContentSection_hdnApprovalRemarks').val($('#ss').val());
                        //    }
                        //}
                        //).then((result) => {
                        //    debugger;
                        //    if (result.value) {
                        //        debugger;
                        //        $('#ContentSection_btnApprove').click();
                        //    }
                        //});

                        //
                        var bidId = parseInt((tableRow).eq(1).html());
                        var formdata = {
                            BidId: bidId,
                        }
                        $.ajax({
                            type: 'POST',
                            url: "ApproveQuotations.aspx/GetApprovalAuthorities",
                            data: JSON.stringify(formdata),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            async: false,
                            success: function (result) {
                                debugger;
                                if (result.d.ErrorMessage == undefined) {

                                    $("#ApproveModel .modal-body").empty();
                                    var row = "";
                                    var className = "";
                                    $.each(result.d.ListAssignAuthorities, function (key, val) {
                                        debugger;
                                        className = "";
                                        var designation = "";  // when less than 1 million(see backend) , designation is username
                                        if (val.DesignationId != 0) {
                                            designation = $.grep(result.d.ListDesignation, function (e) { return e.DesignationId == val.DesignationId; })[0].DesignationName;
                                            if (val.DesignationId != parseInt($('#ContentSection_hndDesignationId').val())) {
                                                className = "disabled";
                                            }
                                        } else {
                                            designation = result.d.ListUser.Username // $.grep(result.d.ListUser, function (e) { return e.UserId == val.UserId; })[0].Username;
                                            if (result.d.ListUser.UserId != parseInt($('#ContentSection_hndUserId').val())) {
                                                className = "disabled";
                                            }
                                        }
                                       
                                        if (val.Count > 1) {
                                            for (var x = 1 ; x <= val.Count ; ++x) {
                                                row = row + "<div class='row' style='padding-left: 13px;'> <div class='col-md-5'>";
                                                row = row + "<label  style='color: Black;'>" + designation + "  ( " + x + ") </label></div>";
                                                if (val.IsApproved == 0) {
                                                    row = row + "<div class='col-md-4'> <a class = '" + className + "' id=" + val.DesignationId + " onclick='approveBidItem(" + val.DesignationId + " , " + bidId + " , " + val.CategoryId + ")' style='cursor:pointer'> To Approve</a></div>";
                                                } else {
                                                    row = row + "<div class='col-md-4'> <a style='cursor:default'> Approved </a></div>";
                                                    row = row + "<div class='col-md-1'> <img src='images/tick.png' /></div>";
                                                }
                                                row = row + "</div>";
                                            }
                                        } else {
                                            row = row + "<div class='row' style='padding-left: 13px;'> <div class='col-md-5'>";
                                            row = row + "<label  style='color: Black;'>" + designation + " </label></div>";
                                            if (val.IsApproved == 0) {
                                                row = row + "<div class='col-md-4'> <a  class = '" + className + "' id=" + val.DesignationId + " onclick='approveBidItem(" + val.DesignationId + " ," + bidId + " , " + val.CategoryId + ")' style='cursor:pointer'> To Approve</a></div>";
                                            }
                                            else {
                                                row = row + "<div class='col-md-4'> <a style='cursor:default'> Approved </a></div>";
                                                row = row + "<div class='col-md-1'> <img src='images/tick.png' /></div>";
                                            }
                                            row = row + "</div>";
                                        }
                                        
                                    });
                                    $("#ApproveModel .modal-body").append(row);
                                } else {

                                }
                            }, complete: function () {

                            }
                        });

                        var $confirm = $("#ApproveModel");
                        $confirm.modal('show');

                    }
                });



                $('.btnRejectCl').on({
                    click: function () {
                        event.preventDefault();

                        var tableRow = $(this).closest('tr').find('> td:not(table)');

                        $('#ContentSection_hdnBidId').val((tableRow).eq(1).html());

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Reject</strong> selected quotation for the bid?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            confirmButtonColor: '#d33',
                            cancelButtonColor: '#3085d6',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Reject It!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {
                                if ($('#ss').val() == '') {
                                    $('#dd').prop('style', 'color:red');
                                    swal.showValidationError('Remarks Required');
                                    return false;
                                }
                                else {
                                    $('#ContentSection_hdnRejectRemarks').val($('#ss').val());
                                }
                            }
                        }).then((result) => {
                            if (result.value) {

                                $('#ContentSection_btnReject').click();

                            }
                        });


                    }
                });


                $('.txtNegotiatePriceCl').on({
                    keyup: function () {
                        calculate(this);
                    }
                });

                $('.txtQtyCl').on({
                    keyup: function () {
                        calculate(this);
                    }
                });
                //$(document).on('keyup', '.txtNegotiatePriceCl', function () {

                //    calculate(this);
                //    event.stopPropagation();
                //});

                function calculate(elmt) {
                    var unitPrice = $(elmt).closest('tr').find('.txtNegotiatePriceCl').val();
                    $(elmt).closest('tr').find('.txtNegotiatePriceCl').value = $(elmt).closest('tr').find('.txtNegotiatePriceCl').val();
                    if (unitPrice == '' || unitPrice == null) {
                        unitPrice = 0;
                    }
                    var qty = 0;
                    if ($(elmt).closest('tr').find('.txtQtyCl').val() != '')
                        qty = parseFloat($(elmt).closest('tr').find('.txtQtyCl').val());
                    var subTot = 0;
                    var nbt = 0;
                    var vat = 0;
                    var netTot = 0;

                    subTot = unitPrice * qty;

                    var chkNbt = $(elmt).closest('tr').find('.chkNbtCl').find('input');
                    var chkVat = $(elmt).closest('tr').find('.chkVatCl').find('input');


                    var rdoNbt204 = $(elmt).closest('tr').find('.rdo204').find('input');
                    var rdoNbt2 = $(elmt).closest('tr').find('.rdo2').find('input');

                    if ($(chkNbt).prop('checked') == true) {
                        if ($(rdoNbt204).prop('checked') == true) {
                            nbt = parseFloat((subTot * 2) / 98);
                        }
                        else {
                            nbt = parseFloat((subTot * 2) / 100);
                        }

                    }

                    if ($(chkVat).prop('checked') == true) {

                        vat = parseFloat((subTot + nbt) * 0.15);
                    }

                    netTot = subTot + nbt + vat;

                    $(elmt).closest('tr').find('.txtSubTotalCl').val(subTot.toFixed(2));
                    $(elmt).closest('tr').find('.txtNbtCl').val(nbt.toFixed(2));
                    $(elmt).closest('tr').find('.txtVatCl').val(vat.toFixed(2));
                    $(elmt).closest('tr').find('.txtNetTotalCl').val(netTot.toFixed(2));

                    var tableRows = $(elmt).closest('tbody').find('> tr:not(:has(>td>table))');

                    var globSubTotal = 0;
                    var globTotalNbt = 0;
                    var globTotalVat = 0;
                    var globNetTotal = 0;

                    for (i = 1; i < tableRows.length; i++) {
                        if ($(tableRows[i]).find('.txtSubTotalCl').val() != '')
                            globSubTotal = globSubTotal + parseFloat($(tableRows[i]).find('.txtSubTotalCl').val());
                        if ($(tableRows[i]).find('.txtNbtCl').val() != '')
                            globTotalNbt = globTotalNbt + parseFloat($(tableRows[i]).find('.txtNbtCl').val());
                        if ($(tableRows[i]).find('.txtVatCl').val() != '')
                            globTotalVat = globTotalVat + parseFloat($(tableRows[i]).find('.txtVatCl').val());
                        if ($(tableRows[i]).find('.txtNetTotalCl').val() != '')
                            globNetTotal = globNetTotal + parseFloat($(tableRows[i]).find('.txtNetTotalCl').val());
                    }

                    var tableRow = $(elmt).closest('table').closest('tr').prev().find('>td:not(tr)');
                    debugger;

                    $(tableRow).eq(5).text(globSubTotal.toFixed(2));
                    $(tableRow).eq(6).text(globTotalNbt.toFixed(2));
                    $(tableRow).eq(7).text(globTotalVat.toFixed(2));
                    $(tableRow).eq(8).text(globNetTotal.toFixed(2));

                    $('#ContentSection_hdnSubTotal').val(globSubTotal.toFixed(2));
                    $('#ContentSection_hdnNbtTotal').val(globTotalNbt.toFixed(2));
                    $('#ContentSection_hdnVatTotal').val(globTotalVat.toFixed(2));
                    $('#ContentSection_hdnNetTotal').val(globNetTotal.toFixed(2));

                }

            });


        });


        function approveBidItem(designationId, bidId, categoryId) {

            var formdata = {
                BidId: bidId,
                CategoryId: categoryId,
                DesignationId: designationId
            }
            $.ajax({
                type: 'POST',
                url: "ApproveQuotations.aspx/ApprovalBidItem",
                data: JSON.stringify(formdata),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                async: false,
                success: function (result) {
                    debugger;
                    $("#ApproveModel .modal-body a#" + designationId + "").text("Approved");
                    //$("#ApproveModel .modal-body a#" + designationId + "").after("<div class=col-md-4' id='Reject'> <a onclick='rejectBidItem(" + val.DesignationId + " ," + bidId + " , " + val.CategoryId + ")' style='cursor:pointer'> To Approve</a></div>");
                    $("#ApproveModel .modal-body a#" + designationId + "").parent().after("<div class=col-md-1'> <img src='images/tick.png' /></div>");
                    if (result.d.Message == "AllApproved") {
                        swal.fire({
                            title: 'Are you sure?',
                            html: "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Select It!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {
                                debugger;
                                $('#ContentSection_hdnApprovalRemarks').val($('#ss').val());
                            }
                        }
                        ).then((result) => {
                            debugger;
                            if (result.value) {
                                debugger;
                                $('#ContentSection_btnApprove').click();
                            }
                        });
                    }
                }
            });
        }

    </script>

</asp:Content>
