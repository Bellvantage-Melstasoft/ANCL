<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="RejectQuotations.aspx.cs" Inherits="BiddingSystem.RejectQuotations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server" ViewStateMode="Enabled">

    <style type="text/css">
       
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
           View Quotation To Reject
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Reject Quotations </li>
        </ol>
    </section>
    <br />

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>


               
                <!-- Start : Section -->
                <section class="content" style="padding-top: 0px">
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
                                                    <asp:BoundField DataField="LastSupplierId"
                                                        HeaderText="LastSupplierId" HeaderStyle-CssClass="hidden"
                                                        ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="LastSupplierName" NullDisplayText="Not Found"
                                                        HeaderText="Last Purchased Supplier" />
                                                    <asp:BoundField DataField="LastPurchasedPrice" NullDisplayText="Not Found"
                                                        HeaderText="Last Purchased Price" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                           
                                                            <asp:Button CssClass="btn btn-xs btn-info btnPurchased" runat="server"
                                                                ID="btnPurchased" Text="Purchase History"
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
                                                                                <asp:BoundField DataField="Description" HeaderText="Description" NullDisplayText="-" />
                                                                             
                                                                                <asp:BoundField DataField="UnitPrice" HeaderText="Quoted Price" />
                                                                                <asp:BoundField DataField="SubTotal" HeaderText="Sub-Total" />
                                                                                <asp:BoundField DataField="NbtAmount" HeaderText="NBT" />
                                                                                <asp:BoundField DataField="VatAmount" HeaderText="VAT" />
                                                                                <asp:BoundField DataField="NetTotal" HeaderText="Net-Total" />
                                                                                <asp:BoundField DataField="SpecComply" HeaderText="Complies Specs" />

                                                                                 <asp:TemplateField HeaderText="Attachments" HeaderStyle-Width="130px" ItemStyle-Width="130px">
                                                                                    <ItemTemplate>
                                                                                       <asp:Button CssClass="btn btn-xs btn-info btnViewAttachmentsClassA" OnClientClick="btnViewAttachmentsClassA()" runat="server"
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
                                                                                      
                                                                                        <asp:Button CssClass="btn btn-xs btn-danger" runat="server" Text="Reject" OnClick="btnReject_Click" Style="margin-right: 4px; margin-bottom: 4px;"></asp:Button>
                                                                                       
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
                                
                                <!-- End : Box Footer -->
                            </div>
                            <!-- End : Box -->

                       
                        </div>
                    </div>
                </section>
                <!-- End : Section -->

                <!-- Start : Hidden Fields -->
               
            </ContentTemplate>
           
        </asp:UpdatePanel>

    </form>

    <script type="text/javascript">
       
    </script>
   

</asp:Content>
