<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerGRNView.aspx.cs" Inherits="BiddingSystem.CustomerGRNView" %>

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
      View GRNs
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">View GRNs </li>
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
        <h3>For Approval</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
                <asp:GridView runat="server" ID="gvPurchaseOrder" EmptyDataText="No records Found" GridLines="None" CssClass="table table-responsive"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="GrnId"  HeaderText="GrnId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="GrnCode"  HeaderText="GRN Code" />
                        <asp:BoundField DataField="PoID"  HeaderText="PO ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="POCode"  HeaderText="PO Code"/>
                        <%--<asp:BoundField DataField="PrCode"  HeaderText="Based PR Code"/>--%>
                        <asp:TemplateField HeaderText="Based PR Code">
							                        <ItemTemplate>
								                        <asp:Label runat="server" Text='<%# "PR-"+Eval("PrCode").ToString() %>'></asp:Label>
							                        </ItemTemplate>
						                        </asp:TemplateField>
                        <asp:BoundField DataField="QuotationFor"  HeaderText="Quotation For"/>
                        <asp:BoundField DataField="SupplierName"  HeaderText="Supplier Name" />
                        <asp:BoundField DataField="GoodReceivedDate"  HeaderText="Good Received Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                        <asp:BoundField DataField="IsGrnRaised"  HeaderText="Is PO Raise" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="IsGrnApproved"  HeaderText="Is PO Approved" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="CreatedDate"  HeaderText="Created Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}"/>
                         <asp:BoundField DataField="WarehouseName"  HeaderText="Warehouse"/>
                        <%--<asp:BoundField DataField="GrnStatusCount"  HeaderText="Grn Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:TemplateField HeaderText="GRN Status">
                          <ItemTemplate>
                              <asp:Label ID="txtApproved" Text='<%#Eval("GrnStatusCount").ToString()=="1"?"GRN Approved":"Pending Approval" %>' ForeColor='<%#Eval("GrnStatusCount").ToString()=="1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                          </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView" Text="Approval" OnClick="btnView_Click"></asp:LinkButton>
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

                    <section class="hidden">
      <div class="box box-info" id="Div1" runat="server">
        <div class="box-header with-border" >
          <h3>Rejected GRN</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
                <asp:GridView runat="server" ID="gvRejectedGrns" GridLines="None" visible =" false" EmptyDataText="No records Found" CssClass="table table-responsive "
                    AutoGenerateColumns="false">
                    <Columns>
                         <asp:BoundField DataField="GrnId"  HeaderText="GrnId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PoID"  HeaderText="PO ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                           <asp:BoundField DataField="_POMaster.PoCode"  HeaderText="PO Code"/>
                      <asp:BoundField DataField="_Supplier.SupplierName"  HeaderText="Supplier Name" />
                        <asp:BoundField DataField="_POMaster.VatAmount"  HeaderText="Vat Amount" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="_POMaster.NBTAmount"  HeaderText="NBT Amount" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="GoodReceivedDate"  HeaderText="Good Received Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}"/>
                        <asp:BoundField DataField="_POMaster.TotalAmount"  HeaderText="Total Amount" DataFormatString="{0:N2}"/>
                         
                           <asp:TemplateField HeaderText="Active">
                         <ItemTemplate >
                             <asp:Label CssClass="activePhase" runat="server" ID="lblStatus" Text="Rejected" BackColor="Red"  Font-Bold="true"  ForeColor="White" ></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnEdit" Text="Edit" OnClick="btnEdit_Click"></asp:LinkButton>
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
