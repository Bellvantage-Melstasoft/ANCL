<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tab1.ascx.cs" Inherits="BiddingSystem.UserControls.Tab1" %>

<style>
 .GridViewEmptyText{
            color:Red;
            font-weight:bold;
            font-size:14px;
        }

</style>

<div class="form-horizontal col-sm-12" style="padding-top: 23px;">
    <%--<div class="col-sm-12">
        <div class="form-group">
        <div class="col-sm-3">
            <label id="">Add & Save Import Reference</label>
        </div>
            </div>
    </div>--%>
    <div class="form-group">
        <div class="col-sm-3">
            <label id="lblLcNo">L/C No</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtLCValue" class="form-control"></asp:TextBox>
        </div>        
    </div>
    <div class="form-group">
        <div class="col-sm-3">
            <label id="lblTTNo">TT No</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtTNo" class="form-control"></asp:TextBox>
        </div>        
    </div>
    <div class="form-group">
        <div class="col-sm-3">
            <label id="lblGuranteeNo">Guarantee No</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtGuranteeNo" class="form-control"></asp:TextBox>
        </div>        
    </div>
    <div class="form-group">
        <div class="col-sm-3">
            <label id="lblHypoLoanNo">Hypo Loan Number</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtHypoLoanNo" class="form-control"></asp:TextBox>
        </div>        
    </div>
    <div class="form-group">
        <div class="col-sm-3">
            <label id="lblBLNo">B/L No</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtBLNo" class="form-control"></asp:TextBox>
        </div>        
    </div>
    <div class="form-group">
        <div class="col-sm-3">
            <label id="lblCUSDECNo">CUSDEC No</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtCUSDECNo" class="form-control"></asp:TextBox>
        </div>        
    </div>
    <div class="form-group">
        <div class="col-sm-3">
            <label id="lblCRefNo">Clearing Reference</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox runat="server" ID="txtCRefNo" class="form-control"></asp:TextBox>
        </div>        
    </div>
</div>

 <div class="box-footer pull-right">
     <asp:Button runat="server" ID="btnClearTab1" class="btn btn-danger" OnClick="btnClearTab1_Click" Text="Clear"></asp:Button>
     <asp:Button runat="server" ID="btnSaveTab1" class="btn btn-success" OnClick="btnSaveTab1_Click" Text="Save"></asp:Button>
</div>
