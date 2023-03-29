<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewPrForPoCreation.aspx.cs" Inherits="BiddingSystem.ViewPrForPoCreation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
      <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
 <section class="content-header">
      <h1>
      Create Purchase Order
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Create Purchase Order</li>
      </ol>
    </section> 
    <br />
        <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>

              <div id="mdlLog" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;" aria-hidden="true">
                <div class="modal-dialog" style="width: 60%;">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #3C8DBC; color: white">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                <span aria-hidden="true" style="opacity: 1;">×</span></button>
                            <h4 class="modal-title">Actions Log</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvStatusLog" runat="server" CssClass="table table-responsive" GridLines="None" AutoGenerateColumns="false"
                                            EmptyDataText="No Log Found" HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White">
                                            <Columns>
                                                <asp:BoundField
                                                    DataField="UserName"
                                                    HeaderText="Logged By" />
                                                <asp:BoundField
                                                    DataField="LoggedDate"
                                                    HeaderText="Logged Date and Time" />
                                                <asp:TemplateField  HeaderText="Current Status">
                                                    <ItemTemplate>
                                                       <asp:Label
                                                            runat="server"
                                                            Text='<%# Eval("LogName")%>' CssClass="label label-info"/>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <section class="content" style="padding-top:0px">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelPurchaseRequset" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >Quotation Selected Purchase Requests</h3>

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
                        <asp:BoundField DataField="CompanyId"  HeaderText="DepartmentId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:TemplateField HeaderText="Department Name">
							<ItemTemplate>
								<asp:Label runat="server" ID="lbldepartmentName" Text='<%# Eval("SubDepartmentName") ==null?"Stores":Eval("SubDepartmentName") %>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>
                         <asp:BoundField DataField="RequiredFor"  HeaderText="Description" />
                        <asp:BoundField DataField="ExpectedDate"  HeaderText="Date Of Request"  DataFormatString="{0:dd MMM yyyy}" />
                        <%--<asp:BoundField DataField="OurReference"  HeaderText="OurReference" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>--%>
                        <%--<asp:BoundField DataField="RequestedBy"  HeaderText="RequestedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>--%>
                        <asp:BoundField DataField="CreatedDateTime"  HeaderText="PR Created Date"  DataFormatString="{0:dd MMM yyyy}"/>
                        <asp:BoundField DataField="CreatedBy"  HeaderText="CreatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="UpdatedDateTime"  HeaderText="UpdatedDateTime" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" DataFormatString='<%$ appSettings:dateTimePattern %>'/>
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
                        <asp:BoundField DataField="PurchaseType"  HeaderText="Purchase Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
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
                                                                    Text="Cover(GRN)" CssClass="label label-info"/>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView" Text="View Quotations" OnClick="btnView_Click"></asp:LinkButton>
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
