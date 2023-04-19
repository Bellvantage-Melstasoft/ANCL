<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CusromerPOView.aspx.cs" Inherits="BiddingSystem.CusromerPOView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">


    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <section class="content-header">
        <h1>View/Send Email Purchase Order 
        <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i>Home</a></li>
            <li class="active">Send Email Purchase Order</li>
        </ol>
    </section>
    <br />

    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content" style="padding-top: 0px">
                    <!-- SELECT2 EXAMPLE -->
                    <div class="box box-info" id="panelPurchaseRequset" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">View Purchase Orders</h3>

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
                                            HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="PoID" HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <%--<asp:BoundField DataField="POCode" HeaderText="PO Code" />--%>
                                                <asp:TemplateField HeaderText="PO Code">
                                                    <ItemTemplate>
                                                        <a href='<%# "ViewPO.aspx?PoId=" + Eval("PoID") %>'><%# Eval("POCode") %></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="PrCode"  HeaderText="PR Code"  />--%>
                                                <asp:TemplateField HeaderText="PR Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# "PR-"+Eval("PrCode").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Department Name">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lbldepartmentName" Text='<%# Eval("subdepartment") ==null?"Stores":Eval("subdepartment") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                <asp:BoundField DataField="CreatedDate" HeaderText="PO Created Date" DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                <asp:BoundField DataField="CreatedBy" HeaderText="PO Created By" />
                                                <asp:TemplateField HeaderText="Email Status">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" CssClass="label label-info" Text='<%# int.Parse(Eval("PoEmailStatus").ToString()) == 0?"Not Sent":"Email Sent" %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Purchasing Type">
                                                    <ItemTemplate>
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("PurchaseType").ToString() == "1" ? true : false %>'
                                                            Text="Local" CssClass="label label-warning" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("PurchaseType").ToString() == "2" ? true : false %>'
                                                            Text="Import" CssClass="label label-success" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

</asp:Content>
