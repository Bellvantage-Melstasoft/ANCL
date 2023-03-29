<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ApproveForBidOpening.aspx.cs" Inherits="BiddingSystem.ApproveForBidOpening" %>
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
   .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height:200%;
            text-align:center;
        }
        .ChildGrid th
        {
            color: White;
            font-size: 10pt;
            line-height:200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #67778e !important;
            color: white;
        }
  </style>
 <section class="content-header">
      <h1>
       Approve for Bid Opening
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active"> Approve for Bid Opening</li>
      </ol>
    </section>
    <br />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>

     <form id="form1" runat="server">
       <section class="content">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <section class="content">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelPurchaseRequset" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >Open For Bid</h3>

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
                <asp:GridView runat="server" ID="gvPurchaseRequest" GridLines="None" CssClass="table table-responsive"
                    AutoGenerateColumns="false" DataKeyNames="prId" OnRowDataBound="OnRowDataBound" EmptyDataText="No PR Found">
                    <Columns>
                    <asp:TemplateField>
                    <ItemTemplate>
                    <img alt = "" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                        <asp:GridView ID="gvPRDetails" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category Name" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName" HeaderText="Sub Category Name" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemName" HeaderText="Name" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemDescription" HeaderText="Description" />
                                <asp:BoundField ItemStyle-Width="100px" DataField="ItemQuantity" HeaderText="Quantity"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="Purpose" HeaderText="Purpose" />
                                <asp:BoundField ItemStyle-Width="200px" DataField="BidTypeMaualOrBid" HeaderText="Bid Type Manual/Online Bid" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:TemplateField HeaderText="Bid Type Manual/Online Bid">
                                 <ItemTemplate>
                                     <asp:Label ID="Label1" runat="server" Text='<%# Eval("BidTypeMaualOrBid").ToString() =="1" ? "Supplier Online Bid":"Supplier Manual Bid" %>' Font-Bold="true" ForeColor='<%#Eval("BidTypeMaualOrBid").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                 </ItemTemplate>
                               </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
                        <asp:BoundField DataField="PrId"  HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrCode"  HeaderText="PR Code" />
                        <asp:BoundField DataField="DateOfRequest"  HeaderText="Date Of Request" DataFormatString="{0:dd/MM/yyyy hh:mm tt}"/>
                        <asp:BoundField DataField="QuotationFor"  HeaderText="Quotation For" />
                        <asp:BoundField DataField="OurReference"  HeaderText="Our Reference" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="RequestedBy"  HeaderText="Requested By" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="CreatedDateTime"  HeaderText="PR Created Date"  DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                        <asp:BoundField DataField="CreatedBy"  HeaderText="Created By" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:TemplateField HeaderText="Approve for Bid" >
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton  ID="btnEdit" OnClick="lnkBtnEdit_Click"  runat="server"  Text="View"/>
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
       </section>
     </form>
  <%--   <script src="AdminResources/js/jquery-1.10.2.min.js"></script>--%>
     <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    
</asp:Content>
