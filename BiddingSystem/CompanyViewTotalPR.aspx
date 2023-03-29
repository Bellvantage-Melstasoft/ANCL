<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyViewTotalPR.aspx.cs" Inherits="BiddingSystem.CompanyViewTotalPR" %>
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
        /*body
        {
            font-family: Arial;
            font-size: 10pt;
        }*/
        .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height:200%;
            border: 1px solid #ddd;
            text-align:center;
        }
        .Grid th
        {
            background-color: #3c8dbc;
            color: White;
            font-size: 10pt;
            line-height:200%;
            text-align :center;
            border: 1px solid #ddd;
            padding: 8px;
        }
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
        .AlgRgh
        {
          text-align:center;
        }
    </style>
    <section class="content-header">
    <h1>
       Total Bids
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Approve Purchase</li>
      </ol>
    </section>
    <br />
    
    <form id="form1" runat="server">
    
     <section class="content">
      <%-- ******************************** --%>
      <div class="box box-danger" id="Div3" runat="server">
        <div class="box-header with-border">
         <h3 class="box-title" >Pending PR</h3>
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
                    AutoGenerateColumns="false" EmptyDataText="No PR Found">
                    <Columns>
                        <asp:BoundField DataField="PrId"  HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrCode"  HeaderText="PR Code" />
                        <asp:BoundField DataField="DepartmentId"  HeaderText="DepartmentId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="DateOfRequest"  HeaderText="Date Of Request"  DataFormatString="{0:dd/MM/yyyy hh:mm tt}"/>
                        <asp:BoundField DataField="QuotationFor"  HeaderText="Quotation For" />
                        <asp:BoundField DataField="OurReference"  HeaderText="OurReference" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="RequestedBy"  HeaderText="RequestedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="CreatedDateTime"  HeaderText="PR Created Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                        <asp:BoundField DataField="CreatedBy"  HeaderText="CreatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="UpdatedDateTime"  HeaderText="UpdatedDateTime" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="UpdatedBy"  HeaderText="UpdatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="IsActive"  HeaderText="IsActive" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrIsApproved"  HeaderText="PrIsApproved" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrIsApprovedOrRejectedBy"  HeaderText="PrIsApprovedOrRejectedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrIsApprovedOeRejectDate"  HeaderText="PrIsApprovedOeRejectDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView" Text="View" OnClick="btnView_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
            </div>         
          </div>
         
        </div>
      </div>
    </section>

     <section class="content">
      <%-- ******************************** --%>
      <div class="box box-danger" id="Div1" runat="server">
        <div class="box-header with-border">
         <h3 class="box-title" >Approved PR</h3>
          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
       
            </div>         
          </div>
         
        </div>

        <!-- /.box-body -->
         <div class="box-footer">
         <div class="col-md-12">
        <div class="form-group">
              <label for="exampleInputEmail1" style="visibility:hidden">Raise PO</label>
            <%--  <button class="btn btn-primary pull-right">Raise PO</button>--%>
              <asp:GridView ID="gvPRMastre" runat="server" AutoGenerateColumns="false" CssClass="Grid table table-responsive"
        DataKeyNames="prId" OnRowDataBound="OnRowDataBound1">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <img alt = "" style="cursor: pointer" src="images/plus.png" />
                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                        <asp:GridView ID="gvPRApprovedDetails" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category Name" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName" HeaderText="SubCategoryName" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemName" HeaderText="Name" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemDescription" HeaderText="Description" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemQuantity" HeaderText="Quantity"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="Purpose" HeaderText="Purpose" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField  DataField="PrId" HeaderText="PrID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField  DataField="PrCode" HeaderText="PR CODE" />
            <asp:BoundField ItemStyle-Width="150px"  DataField="RequestedBy" HeaderText="RequestedBy" />
            <asp:BoundField ItemStyle-Width="150px"  DataField="DateOfRequest" HeaderText="DateOfRequest" />
            <asp:BoundField ItemStyle-Width="150px"  DataField="QuotationFor" HeaderText="QuotationFor" />
        </Columns>
    </asp:GridView>
          </div>
          </div>
        </div>
        
      </div>
    </section>

    <section class="content">
      <%-- ******************************** --%>
      <div class="box box-danger" id="Div2" runat="server">
        <div class="box-header with-border">
         <h3 class="box-title" >Rejected PR</h3>
          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
       
            </div>         
          </div>
         
        </div>

        <!-- /.box-body -->
         <div class="box-footer">
         <div class="col-md-12">
        <div class="form-group">
              <label for="exampleInputEmail1" style="visibility:hidden">Raise PO</label>
            <%--  <button class="btn btn-primary pull-right">Raise PO</button>--%>
              <asp:GridView ID="gvPRMastre2" runat="server" AutoGenerateColumns="false" CssClass="Grid table table-responsive"
        DataKeyNames="prId" OnRowDataBound="OnRowDataBound2">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <img alt = "" style="cursor: pointer" src="images/plus.png" />
                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                        <asp:GridView ID="gvRejectedPRDetails" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category Name" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName" HeaderText="SubCategoryName" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemName" HeaderText="Name" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemDescription" HeaderText="Description" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemQuantity" HeaderText="Quantity"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="Purpose" HeaderText="Purpose" />
                                <asp:BoundField  DataField="PrId" HeaderText="PrID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />         
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField  DataField="PrCode" HeaderText="PR CODE" />
            <asp:BoundField ItemStyle-Width="150px"  DataField="RequestedBy" HeaderText="RequestedBy" />
            <asp:BoundField ItemStyle-Width="150px"  DataField="DateOfRequest" HeaderText="DateOfRequest" />
            <asp:BoundField ItemStyle-Width="150px"  DataField="QuotationFor" HeaderText="QuotationFor" />
            <asp:BoundField ItemStyle-Width="150px"  DataField="RejectedReason" HeaderText="RejectedReason" />
        </Columns>
    </asp:GridView>
          </div>
          </div>
        </div>
        
      </div>
    </section>
    </form>

        <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    
</asp:Content>
