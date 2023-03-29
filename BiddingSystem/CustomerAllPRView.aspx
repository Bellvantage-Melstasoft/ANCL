<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerAllPRView.aspx.cs" Inherits="BiddingSystem.CustomerAllPRView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
     <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
<section class="content-header">
    <h1>
      Approve Purchase 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Approve Purchase </li>
      </ol>
    </section>
    <br />

    <form runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <section class="content">


                <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Departments</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          
          <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                <%--<label for="exampleInputEmail1">Company</label><label id="validateMainCat" style="color:red"></label>--%>
                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" onselectedindexchanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true"> </asp:DropDownList>
                </div>
            </div>
         
          </div>
                   
      <br />
            <br />

      <!-- SELECT2 EXAMPLE -->
      <div class="box box-danger" id="panelPurchaseRequset" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >View Purchase Requests</h3>

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
                    AutoGenerateColumns="false" EmptyDataText="No PR Found" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPurchaseRequest_PageIndexChanging">
                    <Columns>
                        
                        <asp:BoundField DataField="PrId"  HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrCode"  HeaderText="PR Code" />
                        <asp:BoundField DataField="DepartmentId"  HeaderText="DepartmentId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="companyName"  HeaderText="Company Name" />
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
        <!-- /.box-body -->
      </div>


              </div>
      </div>

      <!-- /.box -->
    </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>

</asp:Content>
