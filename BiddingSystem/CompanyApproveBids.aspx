<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="CompanyApproveBids.aspx.cs" Inherits="BiddingSystem.CompanyApproveBids" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
  <style>
    .example-modal .modal {
      position: relative;
      top: auto;
      bottom: auto;
      right: auto;
      left: auto;
      display: block;
      z-index: 1;
    }

    .example-modal .modal {
      background: transparent !important;
    }
  </style>
    <section class="content-header">
      <h1>
       Bid Approve Process
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Approve Bid</li>
      </ol>
    </section>
    <br />
    <form id="form1" runat="server">
     <div class="modal modal-primary fade" id="myModal">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Item Name: </h4>  <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label>
              </div>
              <div class="modal-body">
               <div class="table-responsive">
                 <asp:GridView runat="server" ID="gvSupplierResponse" GridLines="None" CssClass="table table-responsive"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="PRId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <%--    <asp:BoundField DataField="itemname" HeaderText="Item Name" />--%>
                        <asp:BoundField DataField="supplierName" HeaderText="Supplier Name" />
                        <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="estimateAmountPerUnit" DataFormatString="{0:N2}" HeaderText="Estimate Amount Per Unit" />
                        <asp:BoundField DataField="estimateDeliveryDate" HeaderText="Estimate Delivery Date" />
                         <asp:TemplateField HeaderText="Previous Orders">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkBtnPrevious" Text="Previous Prices" style="color:White;"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplier History">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkBtnHistory" Text="History" style="color:White;"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkSelect" Height="20" Width="20" OnCheckedChanged="chkSelect_OnCheckedChanged"
                                   ></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Justification">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtRemark" BackColor="White"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                   </asp:GridView>
                   </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-outline">Approve</button>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->


    <asp:ScriptManager runat="server" ID="sm">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel3" runat="server">
        <ContentTemplate>
            <section class="content" style="min-height:0px;">
        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <strong>
                                <asp:Label ID="lbMessage" runat="server"></asp:Label>
                            </strong>
                        </div>
      <!-- SELECT2 EXAMPLE -->
        <div class="box box-primary">
        <div class="box-header with-border">
          <h3 class="box-title" >Bid Type</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
                <div class="form-group">
               <label style="color: Black; font-weight: bold;">Type of Bid</label>
                <asp:DropDownList runat="server" ID="ddlTypeWise" OnSelectedIndexChanged="ddlTypeWise_OnSelectedIndexChanged" CssClass="form-control"
                    AutoPostBack="true">
                    <asp:ListItem>--Select Bid Type--</asp:ListItem>
                    <asp:ListItem>Item wise</asp:ListItem>
                    <asp:ListItem>Package wise</asp:ListItem>
                </asp:DropDownList>
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
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <section class="content">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelPurchaseRequset" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >Complted Bids</h3>

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
                        <asp:BoundField DataField="PRId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                        <asp:BoundField DataField="itemCode" HeaderText="Item Code" />
                        <asp:BoundField DataField="itemDescription" HeaderText="Item Description" />
                        <asp:BoundField DataField="estimateAmountPerUnit" HeaderText="Estimate Amount Per Unit" />
                        <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="estimateDeliveryDate" HeaderText="Estimate Delivery Date" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <button type="button" class="btn btn-group-justified" data-toggle="modal" data-target="#myModal">View</button>
                                <%--<asp:LinkButton runat="server" ID="lbtnView" Text="View" OnClientClick="lbtnView_Click()" class="btn btn-default" data-toggle="modal" data-target="#modal-default"></asp:LinkButton>--%>
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
    <br />
  
    </form>

    <%--<script type="text/javascript">
        function lbtnView_Click() {
            alert("Hi");
        }
    </script>--%>
</asp:Content>
