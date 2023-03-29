<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ApprovedPOInvoices.aspx.cs" Inherits="BiddingSystem.ApprovedPOInvoices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
	
    <style type="text/css">
        .ui-datepicker-calendar {
            display: none;
        }
        .select2-container--default .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            color: black;
        }
    </style>
     <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>--%>

    <script src="AdminResources/googleapis/googleapis-jquery.min.js"></script>
    <link rel="stylesheet" href="AdminResources/googleapis/googleapis-jquery-ui.css">
    <script src="AdminResources/googleapis/googleapis-jquery-ui.js"></script>

    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content-header">
    <h1>
      Approve Purchase Orders
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Approve Purchase Orders </li>
      </ol>
    </section>
                <br />
                <section class="content">
      <!-- SELECT2 EXAMPLE -->
         <div class="box box-info" id="panelPurchaseRequset" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >Give Approval For PO</h3>

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
                <asp:GridView runat="server" ID="gvPurchaseOrder" EmptyDataText="No Records Found" GridLines="None" CssClass="table table-responsive"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="PoID"  HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="POCode"  HeaderText="PO Code" />
                        <asp:BoundField DataField="BasePr"  HeaderText="BasePr" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <%--<asp:BoundField DataField="PrCode"  HeaderText="Based PR Code" />--%>
                        <asp:TemplateField HeaderText="Based PR Code">
							                        <ItemTemplate>
								                        <asp:Label runat="server" Text='<%# "PR-"+Eval("PrCode").ToString() %>'></asp:Label>
							                        </ItemTemplate>
						                        </asp:TemplateField>
                        <asp:BoundField DataField="QuotationFor"  HeaderText="Quotation For" />
                        <asp:BoundField DataField="SupplierName"  HeaderText="Supplier Name" />
                        <asp:BoundField DataField="ItemCount"  HeaderText="Item Count" />
                        
                       <%-- <asp:TemplateField HeaderText="PO Type">
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
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="PO Type">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PurchaseProcedure").ToString() == "1" ? true : false %>'
                                            Text="Normal" CssClass="label label-warning"/>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PurchaseProcedure").ToString() == "3" ? true : false %>'
                                            Text="Cover" CssClass="label label-success"/>
                                         <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PurchaseProcedure").ToString() == "2" ? true : false %>'
                                            Text="Cover(GRN)" CssClass="label label-info"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                       <%-- <asp:BoundField DataField="ItemCount"  HeaderText="No of Items" />--%>
                        
                    <%--<asp:BoundField DataField="VatAmount"  HeaderText="Total Vat Amount" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="NBTAmount"  HeaderText="Total NBT Amount" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="TotalAmount"  HeaderText="Total Amount" DataFormatString="{0:N2}"/>--%>   
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
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView" Text="Review PO" OnClick="btnView_Click"></asp:LinkButton>
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
    </form>
    <script src="AdminResources/js/select2.full.min.js"></script>

</asp:Content>
