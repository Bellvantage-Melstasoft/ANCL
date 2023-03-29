<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tab6.ascx.cs" Inherits="BiddingSystem.UserControls.Tab6" %>

<div class="form-horizontal col-sm-12" style="padding-top: 23px;">
    <div class="col-sm-12">
        <div class="form-group">
            <div class="col-sm-2">
                <label id="lblImportShippingAgent">Select Shipping Agent</label>
            </div>
            <div class="col-sm-3">
                <asp:DropDownList ID="ddlImportShippingAgent" class="form-control" runat="server"></asp:DropDownList>
            </div>
        <div class="col-sm-2">
            <label id="lblShippingAgentValue">Value</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtImportShippingAgentValue" class="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-1">
        <asp:Button runat="server" ID="btnAddImportShippingAgentDefi" CssClass="btn btn-primary pull-right" OnClick="btnAddImportShippingAgent_Click" Text="Add"></asp:Button>
        </div>
    </div>
    </div>
   
  
    <div class="col-sm-12">
    <div class="form-group">
        <div class="col-sm-2">
            <label id="lblClearing">Clearing & Transport</label>
        </div>
        <div class="col-sm-4">
            <asp:TextBox runat="server" ID="txtClearing" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-2">
            <label id="lblClearingOther">Other</label>
        </div>
        <div class="col-sm-4">
            <asp:TextBox runat="server" ID="txtOther" class="form-control"></asp:TextBox>
        </div>
    </div>     
  </div>

    <div class="form-group col-sm-12">
        <asp:Label runat="server" ID="lblgvImportReferenceDefiError" Text="Already Exist ,Please Edit And Update" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label>
        <asp:GridView runat="server" ID="gvImportReferenceDefi" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="GridViewEmptyText"
            GridLines="None" CssClass="table table-responsive" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" ShowHeaderWhenEmpty="True"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="Order No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblOrderId" Text='<%# (Parent.FindControl("ddlOrderNumber") as DropDownList).Text%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="ImportChargeDefId" HeaderText="ImportRefDefId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblImportRef" Text='<%# listPOImportShippingAgentDef.Find(x=>x.ImportChargeDefId == Convert.ToInt32(Eval("ImportChargeDefId").ToString())).Name %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Agent Value" />
               
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btnImportRedEdit" OnClick="btnImportRedEdit_Click" Text="Edit"></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btnImportRedDelete" OnClick="btnImportRedDelete_Click" Text="Delete"></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</div>

<div class="box-footer pull-right">
    <asp:Button runat="server" ID="btnSaveTab1" class="btn btn-success" OnClick="btnSaveTab1_Click" Text="Save"></asp:Button>
</div>
