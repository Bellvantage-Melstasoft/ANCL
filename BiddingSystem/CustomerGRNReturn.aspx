<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerGRNReturn.aspx.cs" Inherits="BiddingSystem.CustomerGRNReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <html>
    <head>
        <style>
            .activePhase {
                text-align: center;
                border-radius: 3px;
            }
        </style>
        <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    </head>

    <body>



        <section class="content-header">
    <h1>
      Return GRNs
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Return GRNs </li>
      </ol>
    </section>
        <br />

        <form runat="server">
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
                    <section class="content">
      <div class="box box-info" id="panelPurchaseRequset" runat="server">
        <div class="box-header with-border">
        <h3>For Return</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
                <asp:GridView runat="server" ID="gvGrn" EmptyDataText="No records Found" GridLines="None" CssClass="table table-responsive"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="GrnId"  HeaderText="GrnId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="GrnCode"  HeaderText="GRN Code" />
                        <asp:BoundField DataField="SupplierName"  HeaderText="Supplier Name" />
                        <asp:BoundField DataField="GoodReceivedDate"  HeaderText="Good Received Date" DataFormatString="{0:dd/MMMM/yyyy}" />
                         <asp:BoundField DataField="CreatedDate"  HeaderText="Created date" DataFormatString="{0:dd/MMMM/yyyy}"/>
                        <asp:BoundField DataField="CreatedByName"  HeaderText="Created By" />
                        <asp:TemplateField HeaderText="Approved On">
                                                                    <ItemTemplate>
                                                                        <%# (DateTime)Eval("ApprovedDate") == DateTime.MinValue ? "Not Approved" : Eval("ApprovedDate") %>
                                                                    </ItemTemplate>
                                                                     </asp:TemplateField>
                        <asp:BoundField DataField="ApprovedByName"  HeaderText="Approved By" NullDisplayText="Not Approved"/>
                         <asp:BoundField DataField="WarehouseName"  HeaderText="Warehouse"/>
                         <asp:TemplateField HeaderText="Approval Status">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text="Approved" CssClass="label label-success" Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text="Pending"  CssClass="label label-warning" Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView" Text="Return GRN" OnClick="btnView_Click"></asp:LinkButton>
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

                 
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>

    </body>
    </html>

</asp:Content>
