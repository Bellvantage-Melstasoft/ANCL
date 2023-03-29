<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tab5.ascx.cs" Inherits="BiddingSystem.UserControls.Tab5" %>


<div class="form-horizontal col-sm-12" style="padding-top: 23px;">
    <div class="col-sm-6">
        <div class="form-group">
            <div class="col-sm-4">
                <label id="lblImportCustomCharge">Select Custom Charge</label>
            </div>
            <div class="col-sm-8">
                <asp:DropDownList ID="ddlImportCustomChargeType" class="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-4">
                <label id="lblImportSLPA">Select SLPA</label>
            </div>
            <div class="col-sm-8">
                <asp:DropDownList ID="ddlImportSLPA" class="form-control" runat="server"></asp:DropDownList>
            </div>
        </div>
    </div>
        <div class="col-sm-6">
            <div class="form-group">
                <div class="col-sm-3">
                    <label id="lblCustomChargeValue">Value</label>
                </div>
                <div class="col-sm-8">
                    <asp:TextBox runat="server" ID="txtImportCustomChargeValue" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-3">
                    <label id="lblSLPAValue">Value</label>
                </div>
                <div class="col-sm-8">
                    <asp:TextBox runat="server" ID="txtImportSLPA" class="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-sm-12">
            <asp:Button runat="server" ID="btnAddImportCustomChaDefi" CssClass="btn btn-primary pull-right" OnClick="btnAddImportCustomChaDefi_Click" Text="Add"></asp:Button>
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
                        <asp:Label runat="server" ID="lblImportRef" Text='<%# listPOImportChargesDef.Find(x=>x.ImportChargeDefId == Convert.ToInt32(Eval("ImportChargeDefId").ToString())).Name %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Custom Charge Value" />
                <asp:TemplateField HeaderText="SLPA Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblImportSLPA" Text='<%#txtImportSLPA.Text%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
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
