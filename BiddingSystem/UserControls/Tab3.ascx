<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tab3.ascx.cs" Inherits="BiddingSystem.UserControls.Tab3" %>

<div class="form-horizontal col-sm-12" style="padding-top: 23px;">


    <div class="form-group">
        <div class="col-sm-3">
            <label id="">Transport Mode</label>
        </div>
        <div class="col-sm-3">
            <asp:DropDownList ID="ddlTransportMode" class="form-control" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
            <label id="">Add Vessel</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtOVessel" class="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-3">
            <asp:Button ID="btnVessel" runat="server" CssClass="btn btn-info" OnClick="btnVessel_Click" Text="Add" />
            <asp:Button ID="btnVesselClear" runat="server" CssClass="btn btn-danger" OnClick="btnVesselClear_Click" Text="Clear" />
        </div>
    </div>
    <div class="form-group" id="divVessel" runat="server" visible="false">
        <asp:Label runat="server" ID="lblgvVessel" Text="Already Exist" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label>
        <div class="table-responsive" style="width: 50%;">
            <asp:GridView ID="gvVessel" runat="server" CssClass="table table-responsive"
                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="ReferenceId" HeaderText="Id" ItemStyle-CssClass="hidden Id" HeaderStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderText="Transport Mode Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTransportId" ItemStyle-CssClass="transportModeId" Text='<%#ddlTransportMode.SelectedValue%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transport Mode">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#ddlTransportMode.SelectedItem.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Vessel Name" ItemStyle-CssClass="vesselName" />
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button CssClass="btn btn-default btn-xs" ID="Delete"
                                runat="server" OnClick="gvVesselDelete_Click"
                                Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
            <label id="">Add Shipping Agent </label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtOShippingAgent" class="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-3">
            <asp:Button ID="btnShippingAgent" runat="server" CssClass="btn btn-info" OnClick="btnShippingAgent_Click" Text="Add" />
            <asp:Button ID="btnShippingAgentClear" runat="server" CssClass="btn btn-danger" OnClick="btnShippingAgentClear_Click" Text="Clear" />
        </div>
    </div>
    <div class="form-group" id="divShippingAgent" runat="server" visible="false">
        <asp:Label runat="server" ID="lblgvShippingAgent" Text="Already Exist" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label>
        <div class="table-responsive" style="width: 50%;">
            <asp:GridView ID="gvShippingAgent" runat="server" CssClass="table table-responsive"
                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                <Columns>

                    <asp:BoundField DataField="ReferenceId" HeaderText="Id" ItemStyle-CssClass="hidden Id" HeaderStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderText="Transport Mode Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTransportId" ItemStyle-CssClass="transportModeId" Text='<%#ddlTransportMode.SelectedValue%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transport Mode">
                        <ItemTemplate>
                            <asp:Label runat="server" ItemStyle-CssClass="transportMode" Text='<%#ddlTransportMode.SelectedItem.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Shipping Agent Name" ItemStyle-CssClass="shippingAgentName" />
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button CssClass="btn btn-default btn-xs" ID="Delete"
                                runat="server" OnClick="gvShippingAgentDelete_Click"
                                Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="form-group">
        <div class="col-sm-3">
            <label id="">Add Clearing Agent</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtOClearingAgent" class="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-3">
            <asp:Button ID="btnClearingAgent" runat="server" CssClass="btn btn-info" OnClick="btnClearingAgent_Click" Text="Add" />
            <asp:Button ID="btnClearingAgentClear" runat="server" CssClass="btn btn-danger" OnClick="btnClearingAgentClear_Click" Text="Clear" />
        </div>
    </div>

    <div class="form-group" id="divClearingAgent" runat="server" visible="false">
        <asp:Label runat="server" ID="lblgvClearingAgent" Text="Already Exist" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label>
        <div class="table-responsive" style="width: 50%;">
            <asp:GridView ID="gvClearingAgent" runat="server" CssClass="table table-responsive"
                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                <Columns>

                    <asp:BoundField DataField="ReferenceId" HeaderText="Id" ItemStyle-CssClass="hidden Id" HeaderStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderText="Transport Mode Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTransportId" ItemStyle-CssClass="transportModeId" Text='<%#ddlTransportMode.SelectedValue%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transport Mode">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#ddlTransportMode.SelectedItem.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Clearing Agent Name" ItemStyle-CssClass="clearingAgentName" />
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button CssClass="btn btn-default btn-xs" ID="Delete"
                                runat="server" OnClick="gvClearingAgentDelete_Click"
                                Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="form-group">
        <div class="col-sm-3">
            <label id="">Insurance Company</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtInsuranceCompany" class="form-control"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
            <label id="">Date</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date" runat="server" ID="txtOInsuranceDate" data-date="" data-date-format="DD MMMM YYYY" onchange="dateChange(this)" class="form-control customDate"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
            <label id="">Policy No</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtPolicyNo" class="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-3">
            <asp:Button ID="btnAddInsuranceDetail" runat="server" CssClass="btn btn-info" OnClick="btnAddInsuranceDetail_Click" Text="Add" />
            <asp:Button ID="btnAddInsuranceDetailClear" runat="server" CssClass="btn btn-danger" OnClick="btnAddInsuranceDetailClear_Click" Text="Clear" />
        </div>
    </div>

    <div class="form-group" id="divInsurance" runat="server" visible="false">
        <asp:Label runat="server" ID="lblgvInsuranceCompanyPolicy" Text="Already Exist" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label>
        <div class="table-responsive" style="width: 50%;">
            <asp:GridView ID="gvInsurance" runat="server" CssClass="table table-responsive"
                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="ReferenceId" HeaderText="Id" ItemStyle-CssClass="hidden Id" HeaderStyle-CssClass="hidden" />
                    <asp:TemplateField HeaderText="Transport Mode Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTransportId" ItemStyle-CssClass="transportModeId" Text='<%#ddlTransportMode.SelectedValue%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transport Mode">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#ddlTransportMode.SelectedItem.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Insurance Company Name" ItemStyle-CssClass="insuranceCompanyName" />
                    <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-CssClass="date" />
                    <asp:BoundField DataField="ReferenceNo" HeaderText="Policy No" ItemStyle-CssClass="policyNo" />
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button CssClass="btn btn-default btn-xs" ID="Delete"
                                runat="server" OnClick="gvInsuranceDelete_Click"
                                Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="form-group">
        <div class="col-sm-3">
            <label id="">Quantity</label>
        </div>
        <div class="col-sm-3">
            <asp:DropDownList ID="ddlMeasurement" class="form-control" runat="server"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtOQty" class="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-3">
            <asp:Button ID="btnAddOQty" runat="server" CssClass="btn btn-info" OnClick="btnAddOQty_Click" Text="Add" />
            <asp:Button ID="btnOQtyClear" runat="server" CssClass="btn btn-danger" OnClick="btnOQtyClear_Click" Text="Clear" />
        </div>
    </div>

    <div class="form-group" id="divQty" runat="server" visible="false">
        <div class="table-responsive">
            <asp:GridView ID="gvQty" runat="server" CssClass="table table-responsive"
                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:TemplateField HeaderText="Measurment" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#ddlTransportMode.SelectedItem.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transport Mode">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#ddlTransportMode.SelectedItem.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Measurment">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#ddlTransportMode.SelectedItem.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#txtOQty.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button CssClass="btn btn-default btn-xs" ID="Delete"
                                runat="server" OnClick="gvOQtyDelete_Click"
                                Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="form-group">
        <div class="col-sm-3">
            <label id="">Container Size</label>
        </div>
        <div class="col-sm-3">
            <asp:DropDownList ID="ddlContainerSize" class="form-control" runat="server"></asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtOContainerSize" class="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-3">
            <asp:Button ID="btnAddContainerSize" runat="server" CssClass="btn btn-info" OnClick="btnAddContainerSize_Click" Text="Add" />
            <asp:Button ID="btnContainerSizeClear" runat="server" CssClass="btn btn-danger" OnClick="btnContainerSizeClear_Click" Text="Clear" />
        </div>
    </div>

    <div class="form-group" id="divContainerSize" runat="server" visible="false">
        <div class="table-responsive">
            <asp:GridView ID="gvCOntainerSize" runat="server" CssClass="table table-responsive"
                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#ddlContainerSize.SelectedItem.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transport Mode">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#ddlTransportMode.SelectedItem.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Size">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#ddlContainerSize.SelectedItem.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#txtOContainerSize.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button CssClass="btn btn-default btn-xs" ID="Delete"
                                runat="server" OnClick="gvContainerSizeDelete_Click"
                                Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="form-group">
        <div class="col-sm-3">
            <label id="">HS Code</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtHSCode" class="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-3">
            <asp:Button ID="btnHSCode" runat="server" CssClass="btn btn-info" OnClick="btnAddHSCode_Click" Text="Add" />
            <asp:Button ID="btnHSCodeClear" runat="server" CssClass="btn btn-danger" OnClick="btnHSCodeClear_Click" Text="Clear" />
        </div>
    </div>

    <div class="form-group" id="divHSCode" runat="server" visible="false">
        <div class="table-responsive">
            <asp:GridView ID="gvHsCode" runat="server" CssClass="table table-responsive"
                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <%-- <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#ddlContainerSize.SelectedItem.Text%>'/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%-- <asp:TemplateField HeaderText="Transport Mode">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#ddlTransportMode.SelectedItem.Text%>'/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="HS Code">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#txtHSCode.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button CssClass="btn btn-default btn-xs" ID="Delete"
                                runat="server" OnClick="gvHSCodeDelete_Click"
                                Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="form-group">
        <div class="col-sm-3">
            <label id="">Perfomance Bond No</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtPerfromanceBondNo" class="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-1">
            <label id="">Date</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date" runat="server" ID="txtPerformanceDate" data-date="" data-date-format="DD MMMM YYYY" onchange="dateChange(this)" class="form-control customDate"></asp:TextBox>
        </div>
        <div class="col-sm-2">
            <asp:Button ID="btnAddPerformanceBond" runat="server" CssClass="btn btn-info" OnClick="btnAddPerformanceBond_Click" Text="Add" />
            <asp:Button ID="btnPerformanceBondClear" runat="server" CssClass="btn btn-danger" OnClick="btnPerformanceBondClear_Click" Text="Clear" />
        </div>
    </div>

    <div class="form-group" id="divPerformanceBond" runat="server" visible="false">
        <div class="table-responsive">
            <asp:GridView ID="gvPerformanceBond" runat="server" CssClass="table table-responsive"
                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:TemplateField HeaderText="Performance Bond No">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#txtPerfromanceBondNo.Text%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:Button CssClass="btn btn-default btn-xs" ID="Delete"
                                runat="server" OnClick="gvPerformanceBondDelete_Click"
                                Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="box-footer pull-right">
        <asp:Button runat="server" ID="btnSaveTab1" class="btn btn-success" OnClick="btnSaveTab1_Click" Text="Save"></asp:Button>
    </div>
