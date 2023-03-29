<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tab4.ascx.cs" Inherits="BiddingSystem.UserControls.Tab4" %>


<div class="form-horizontal col-sm-12" style="padding-top: 23px;">
    <div class="col-sm-6">
        <div class="form-group">
            <div class="col-sm-4">
                <label id="lblImportReference">Select Type</label>
            </div>
            <div class="col-sm-8">
                <asp:DropDownList ID="ddlChargesType" class="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-4">
                <label id="lblCurrency">Select Currency</label>
            </div>
            <div class="col-sm-8">
                <asp:DropDownList ID="ddlCurrency" class="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="col-sm-6">
        <div class="form-group">
            <div class="col-sm-4">
                <label id="lblValue">Value</label>
            </div>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtImportChargesValue" class="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-4">
                <label id="lblExchangeRate">Exchange Rate</label>
            </div>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtExchangeRate" class="form-control" Text="1"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="col-sm-12">
        <asp:Button runat="server" ID="btnAddImportChargeDefi" CssClass="btn btn-primary pull-right" OnClick="btnAddImportChargeDefi_Click" Text="Add"></asp:Button>
    </div>


    <div class="form-group col-sm-12" style="padding-top: 20px;">
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

                <asp:BoundField DataField="ImportChargeDefId" HeaderText="ImportChargeDefId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblImportRef" Text='<%# listPOImportChargesDef.Find(x=>x.ImportChargeDefId == Convert.ToInt32(Eval("ImportChargeDefId").ToString())).Name %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Value" />
                <asp:BoundField DataField="Currency" HeaderText="Currency" />
                <asp:BoundField DataField="ExchangeRate" HeaderText="Exchange Rate" />
                <%--   <asp:TemplateField HeaderText="Currecny">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblImportCurrency" Text='<%# listPOImportChargesDef.Find(x=>x.I == Convert.ToInt32(Eval("ImportChargeDefId").ToString())).Name %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
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
