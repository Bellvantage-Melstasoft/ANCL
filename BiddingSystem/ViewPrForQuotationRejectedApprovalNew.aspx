<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewPrForQuotationRejectedApprovalNew.aspx.cs" Inherits="BiddingSystem.ViewPrForQuotationRejectedApprovalNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
      <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
 <section class="content-header">
      <h1>
Rejected bids        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <%--<li class="active"> Sub/Procurement Committee Approval</li>--%>
          <li class="active"> Rejected Bids</li>
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
          <h3 class="box-title" >View rejected Bids</h3>

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
                        <%--<asp:BoundField DataField="PrCode"  HeaderText="PR Code" />--%>
                         <asp:TemplateField HeaderText="PR Code">
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# "PR-"+Eval("PrCode").ToString() %>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
                         <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbldepartmentName" Text='<%# Eval("SubDepartmentName") ==null?"Stores":Eval("SubDepartmentName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SubDepartmentId"  HeaderText="DepartmentId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                         <asp:BoundField DataField="PurchaseType"  HeaderText="PurchaseType" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                       
                        <asp:BoundField DataField="ExpectedDate"  HeaderText="Date Of Request" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="RequiredFor"  HeaderText="Description" />
                        <%--<asp:BoundField DataField="OurReference"  HeaderText="OurReference" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>--%>
                        <asp:BoundField DataField="CreatedBy"  HeaderText="RequestedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="CreatedDateTime"  HeaderText="PR Created Date"  DataFormatString='<%$ appSettings:dateTimePattern %>'/>
                        <asp:BoundField DataField="CreatedBy"  HeaderText="CreatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="UpdatedDateTime"  HeaderText="UpdatedDateTime" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataFormatString="{0:N2}"/>
                        <asp:BoundField DataField="UpdatedBy"  HeaderText="UpdatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="IsActive"  HeaderText="IsActive" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrIsApproved"  HeaderText="PrIsApproved" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrIsApprovedOrRejectedBy"  HeaderText="PrIsApprovedOrRejectedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrIsApprovedOeRejectDate"  HeaderText="PrIsApprovedOeRejectDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
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
                         <asp:TemplateField HeaderText="Purchase Procedure">
                            <ItemTemplate>
                                 <asp:Label
                                  runat="server"
                                  Visible='<%# Eval("PurchaseProcedure").ToString() == "1" ? true : false %>'
                                  Text="General" CssClass="label label-warning"/>
                               <asp:Label
                                 runat="server"
                                  Visible='<%# Eval("PurchaseProcedure").ToString() == "3" ? true : false %>'
                                   Text="Covering" CssClass="label label-success"/>
                                <asp:Label
                                 runat="server"
                                  Visible='<%# Eval("PurchaseProcedure").ToString() == "2" ? true : false %>'
                                   Text="Covering(GRN)" CssClass="label label-info"/>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ImportItemType"
                                                        HeaderText="ImportItemType" HeaderStyle-CssClass="hidden"
                                                        ItemStyle-CssClass="hidden" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView" Text="View Bids" OnClick="btnView_Click"></asp:LinkButton>
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
