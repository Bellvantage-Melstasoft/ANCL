<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerPRViewOnly.aspx.cs" Inherits="BiddingSystem.CustomerPRView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
<section class="content-header">
    <h1>
      Approve Purchase 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Approve Purchase </li>
      </ol>
    </section>
    <br />

    <form runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <section class="content">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelPurchaseRequset" runat="server">
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
                <asp:GridView runat="server" ID="gvPurchaseRequest" GridLines="None" CssClass="table table-responsive"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="PRId"  HeaderText="PR Code" />
                        <asp:BoundField DataField="departments"  HeaderText="PR Code" />
                        <asp:BoundField DataField="quotationfor"  HeaderText="Quotation For" />
                        <asp:BoundField DataField="createddate"  HeaderText="Created Date" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView" Text="View" OnClick="lbtnView_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
