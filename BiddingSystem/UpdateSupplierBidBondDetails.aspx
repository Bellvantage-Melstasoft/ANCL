<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="UpdateSupplierBidBondDetails.aspx.cs" Inherits="BiddingSystem.UpdateSupplierBidBondDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server" ViewStateMode="Enabled">




    <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>                
                <section class="content">
                    <div class="row" style="" visible="false">
                    <div class="col-sm-12">
                        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <strong>
                                <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
                            </strong>
                        </div>
                    </div>
                </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box" id="panelPurchaseRequset" runat="server">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Bid Bond details</h3>
                                    </div>
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label for="txtTermsAndConditions">Select Supplier</label>
                                                    <asp:RequiredFieldValidator ID="rfvDDL" runat="server"
                                                    ControlToValidate="ddlSuppliers" 
                                                    Display="Dynamic"
                                                        Font-Bold="true"
                                                    InitialValue="Select A Supplier"
                                                    ForeColor="Red" > * Select This Field
                                                    </asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlSuppliers" runat="server" CssClass="form-control" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label for="BondNo">Bond No.</label>
                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtBondNo">* Fill This Field</asp:RequiredFieldValidator>
                                                    <asp:TextBox  ID="txtBondNo"  runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                     <label for="Bank">Bank</label>
                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtBank">* Fill This Field</asp:RequiredFieldValidator>
                                                    <asp:TextBox  ID="txtBank"  runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                     <label for="ReceiptNo">Receipt No.</label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtReceiptNo" >* Fill This Field</asp:RequiredFieldValidator>
                                                    <asp:TextBox  ID="txtReceiptNo"  runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                
                                            </div>
                                             <div class="col-md-6">
                                                 <div class="form-group">
                                                     <label for="BondAmount">Bond Amount.</label>
                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtBondAmount" >* Fill This Field</asp:RequiredFieldValidator>
                                                    <asp:TextBox  ID="txtBondAmount"  runat="server" CssClass="form-control"></asp:TextBox>
                                                 </div>
                                                 <div class="form-group">
                                                     <label for="ExpireDOB">Expire Date of Bond.</label>
                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtExpireDOB" >* Fill This Field</asp:RequiredFieldValidator>
                                                    <asp:TextBox  ID="txtExpireDOB"  runat="server" CssClass="form-control date1"></asp:TextBox>
                                                 </div>
                                             </div>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <a id="btnCancel" class="btn btn-danger pull-right" style="margin-right:10px" href="ViewSupplierBidBondDetails.aspx">Cancel</a>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary pull-right"
                                            OnClick="btnSubmit_Click" style="margin-right:10px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>



    </form>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />

    <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
    <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />

    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $('.date1').unbind();
            $(".date1").datepicker({
                format: 'MM/DD/YYYY',
                changeYear: true
            });

        });

       
        $(".date1").datepicker({
            format: 'MM/DD/YYYY',
            changeYear: true
        });



    </script>
</asp:Content>
