<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="AddAfterPO.aspx.cs" Inherits="BiddingSystem.afterPO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style>
        .dynamicStyle{
            margin-top:20px;
        }
    </style>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>

    <form id="form1" runat="server" enctype="multipart/form-data" defaultbutton="btnSave">
        <div class="content-header">
            <h1>After PO<small></small></h1>
            <ol class="breadcrumb">
                <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li class="active">After PO</li>
            </ol>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content">
                    <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title" >After PO</h3>

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
                                            <label for="exampleInputEmail1">Hyper Loan</label><span class="required"> *</span>
                                            <asp:Label ID="lblHypoLoan" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="ddlHyperLoan" ValidationGroup="btnSave" >Fill this Field</asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlHyperLoan" runat="server" CssClass="form-control" CausesValidation="true" >
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <div class ="row" style="padding-left:10px">
                                            <label for="exampleInputEmail1">Dates</label><span class="required"> *</span>
                                            <asp:Label ID="lblDates" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="txtDates" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtDates" runat="server" CssClass="form-control" placeholder="YYYY/MM/DD" Text=""></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                             <asp:DropDownList ID="ddlDateType" runat="server" CssClass="form-control" CausesValidation="true" ></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btnAdd"  runat="server" Text="Add"  CssClass="btn btn-info" OnClick="btnAdd_Click"></asp:Button> 
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Panel ID="datePanel"  runat="server"></asp:Panel>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Panel ID="ddlDatePanel"  runat="server"></asp:Panel>
                                        </div>
                                    </div>

                                    <div class="row margin-bottom-10"></div>

                                   <div class="form-group">
                                        <label for="exampleInputEmail1">Vessel</label><span class="required"> *</span>
                                        <asp:Label ID="lblVessel" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" ControlToValidate="txtVessel" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        <asp:TextBox ID="txtVessel" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Shipping Agent</label><span class="required"> *</span>
                                        <asp:Label ID="lblShippingAgent" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ForeColor="Red"  ControlToValidate="txtShippingAgent" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        <asp:TextBox ID="txtShippingAgent" runat="server"  CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Insurance Company Name</label><span class="required"> *</span>
                                        <asp:Label ID="lblInsuranceCompany" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red"  ControlToValidate="txtInsuranceCompanyname" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        <asp:TextBox ID="txtInsuranceCompanyname" runat="server"  CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Insurance Date</label><span class="required"> *</span>
                                        <asp:Label ID="lblInsuranceDate" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red"  ControlToValidate="txtInsuranceDate" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        <asp:TextBox ID="txtInsuranceDate" runat="server"  CssClass="form-control" placeholder="YYYY/MM/DD" Text=""></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Insurance Policy No</label><span class="required"> *</span>
                                        <asp:Label ID="lblPolicyNo" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red"  ControlToValidate="txtPolicyNo" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        <asp:TextBox ID="txtPolicyNo" runat="server"  CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                    </div>
                            </div>

                            <div class="col-md-6">

                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Performance Bond No</label><span class="required"> *</span>
                                        <asp:Label ID="lblPerformanceBondNo" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red"  ControlToValidate="txtPerformanceBondNo" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        <asp:TextBox ID="txtPerformanceBondNo" runat="server"  CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Performance Bond Date</label><span class="required"> *</span>
                                        <asp:Label ID="lblPerformanceBonddate" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red"  ControlToValidate="txtPerformanceBondDate" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        <asp:TextBox ID="txtPerformanceBondDate" runat="server"  CssClass="form-control" placeholder="YYYY/MM/DD" Text=""></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <div class ="row" style="padding-left:10px">
                                            <label for="exampleInputEmail1">Charges</label><span class="required"> *</span>
                                            <asp:Label ID="lblCharges" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ForeColor="Red"  ControlToValidate="txtCharges" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        </div>

                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtCharges" runat="server"  CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                             <asp:DropDownList ID="ddlCharges" runat="server" CssClass="form-control" CausesValidation="true" ></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btnAddCharges"  runat="server" Text="Add"  CssClass="btn btn-info" OnClick="btnAddCharges_Click"></asp:Button> 
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Panel ID="panelChargesText"  runat="server"></asp:Panel>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Panel ID="panelChargesDDL"  runat="server"></asp:Panel>
                                        </div>
                                    </div>

                                    <div class="row margin-bottom-10"></div>

                                    <div class="form-group">
                                        <div class ="row" style="padding-left:10px">
                                            <label for="exampleInputEmail1">Custom Charges</label><span class="required"> *</span>
                                            <asp:Label ID="lblCustomCharges" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ForeColor="Red"  ControlToValidate="txtCustomCharges" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        </div>

                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtCustomCharges" runat="server"  CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                             <asp:DropDownList ID="ddlCustomCharges" runat="server" CssClass="form-control" CausesValidation="true" ></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btnAddCustomCharges"  runat="server" Text="Add"  CssClass="btn btn-info" OnClick="btnAddCustomCharges_Click"></asp:Button> 
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Panel ID="panelCustomText"  runat="server"></asp:Panel>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Panel ID="panelDllCustom"  runat="server"></asp:Panel>
                                        </div>

                                    </div>

                                    <div class="row margin-bottom-10"></div>

                                    <div class="form-group">
                                        <label for="exampleInputEmail1">SLPA</label><span class="required"> *</span>
                                        <asp:Label ID="lblSLPA" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ForeColor="Red"  ControlToValidate="txtSLPA" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        <asp:TextBox ID="txtSLPA" runat="server"  CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <div class ="row" style="padding-left:10px">
                                            <label for="exampleInputEmail1">SHIPPING</label><span class="required"> *</span>
                                            <asp:Label ID="lblShippingCharges" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ForeColor="Red"  ControlToValidate="txtShippingCharges" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        </div>

                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtShippingCharges" runat="server"  CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                             <asp:DropDownList ID="ddlShippingCharges" runat="server" CssClass="form-control" CausesValidation="true" ></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btnAddShipping"  runat="server" Text="Add"  CssClass="btn btn-info" OnClick="btnAddShipping_Click"></asp:Button> 
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Panel ID="panelShippingText"  runat="server"></asp:Panel>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Panel ID="pannelShippingDDL"  runat="server"></asp:Panel>
                                        </div>

                                    </div>

                                    <div class="row margin-bottom-10"></div>

                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Clearing & Transport Charges</label><span class="required"> *</span>
                                        <asp:Label ID="lblClearingCharges" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ForeColor="Red"  ControlToValidate="txtClearingcharges" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                        <asp:TextBox ID="txtClearingcharges" runat="server"  CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                    </div>
                            
                            </div>
                    </div>
                        <div class="box-footer">
                        <span class="pull-right">
                                <asp:Button ID="btnSave" runat="server" Text="Submit"  ValidationGroup="btnSave" CssClass="btn btn-primary " OnClick="BtnSave_Click" OnClientClick="validate();"></asp:Button>
                                <asp:Button ID="btnClear"  runat="server" Text="Clear"  CssClass="btn btn-danger" OnClick="btnClear_Click"></asp:Button>               
                        </span>
                        </div>
                    </div>
                    </div>
                </section>

                <asp:Button ID="btnSubmitNew" runat="server" OnClick="BtnSave_Click" CssClass="hidden" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
        </asp:UpdatePanel>
    </form>

</asp:Content>
