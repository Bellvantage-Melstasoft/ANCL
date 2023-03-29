<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewPrForTabulationsheetApproval.aspx.cs" Inherits="BiddingSystem.ViewPrForTabulationsheetApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
      <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    

 <section class="content-header">
      <h1>
      Review & Approve/Reject Quotation Tabulation
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Review & Approve/Reject Quotation Tabulation</li>
      </ol>
    </section> 
    <br />
        <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <section class="content" style="padding-top:0px">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelPurchaseRequset" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >Bid Completed Purchase Requests</h3>

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
                <asp:GridView runat="server" ID="gvPurchaseRequest" GridLines="None" CssClass="table table-responsive" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                    AutoGenerateColumns="false" EmptyDataText="No PR Found">
                    <Columns>
                        <asp:BoundField DataField="PrId"  HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:TemplateField HeaderText="PR Code">
							                        <ItemTemplate>
								                        <asp:Label runat="server" Text='<%# "PR"+Eval("PrCode").ToString() %>'></asp:Label>
							                        </ItemTemplate>
						                        </asp:TemplateField>
                                                <asp:BoundField DataField="PrCategoryId"  HeaderText="PR Category Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="WarehouseName"  HeaderText="From Warehouse"/>
                                                <asp:BoundField DataField="PrCategoryName"  HeaderText="PR Category Name"/>
                                                 <asp:BoundField DataField="RequiredFor"  HeaderText="Required For" />
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Created On"
                                                                             DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                <asp:BoundField DataField="CreatedByName"  HeaderText="Created By" />                   
                                                 <asp:TemplateField HeaderText="PR Type">
                                                                         <ItemTemplate>
                                                                             <asp:Label runat="server" ID="lblprtype" Text='<%#Eval("PrType").ToString()=="1"? "Stock":"Non-Stock"%>' ForeColor='<%#Eval("PrType").ToString()=="1"? System.Drawing.Color.Maroon:System.Drawing.Color.Navy%>'></asp:Label>
                                                                         </ItemTemplate>
                                                                </asp:TemplateField>                   
                                                 <asp:TemplateField HeaderText="Expense Type">
                                                                         <ItemTemplate>
                                                                             <asp:Label runat="server" Text='<%#Eval("ExpenseType").ToString()=="1"? "Capital Expense":"Operational Expense"%>'></asp:Label>
                                                                         </ItemTemplate>
                                                                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView"  CssClass="btn btn-sm btn-info" Text="View Bids" OnClick="btnView_Click"></asp:LinkButton>
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
