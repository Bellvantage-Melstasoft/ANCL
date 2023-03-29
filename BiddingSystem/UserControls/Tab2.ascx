<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tab2.ascx.cs" Inherits="BiddingSystem.UserControls.Tab2" %>



<div class="form-horizontal col-sm-12" style="padding-top: 23px;">
    <div class="form-group">
        <div class="col-sm-3">
            <label id="">Payment No</label>
        </div>
         <div class="col-sm-3">
            <asp:TextBox  runat="server" ID="txtPaymentNo" type="number" value="1" class="form-control"></asp:TextBox>
        </div>  
         <div class="col-sm-2">
            <asp:Button runat="server" ID="btnAddNewPaymentNo" class="btn btn-primary" OnClick="btnAddNewPaymentNo_Click" Text="Add New Payment"></asp:Button>
        </div> 
     </div>   
    <div class="form-group">
        <div class="col-sm-3">
             <label id="">Price Terms</label> </div>
        <div class="col-sm-3">
            <asp:DropDownList ID="ddlPriceTerms" class="form-control" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
            <label id="lblPaymentMode">Payment Mode</label>
        </div>
        <div class="col-sm-3">
            <asp:DropDownList ID="ddlPaymentMode" class="form-control" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
             <label id="lblDate1">LC Opening /TT Payment</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date"  AutoPostBack="true" runat="server"  ID="txtDateValue1" data-date="" data-date-format="DD MMMM YYYY"  class="form-control customDate"></asp:TextBox>
        </div>
        </div>
    <div class="form-group">
        <div class="col-sm-3">
             <label id="">Latest date of shipment(LDS)</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date"  AutoPostBack="true" runat="server" ID="txtDateValue2" data-date="" data-date-format="DD MMMM YYYY"  class="form-control customDate"></asp:TextBox>
        </div>
        </div>
    <div class="form-group">
        <div class="col-sm-3">
             <label id="">LC Expiry date </label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date"  AutoPostBack="true" runat="server" ID="txtDateValue3" data-date="" data-date-format="DD MMMM YYYY"  class="form-control customDate"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
             <label id="">Expected date of dispatch(ETD)</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date"  AutoPostBack="true" runat="server" ID="txtDateValue4" data-date="" data-date-format="DD MMMM YYYY"  class="form-control customDate"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
             <label id="">Expected date of arrival(ETA)</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date"  AutoPostBack="true" runat="server" ID="txtDateValue5" data-date="" data-date-format="DD MMMM YYYY"  class="form-control customDate"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
             <label id="">ORGINAL DOCUMENTS COLLECTED DATE/BANK ENDORSEMENT</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date"  AutoPostBack="true" runat="server" ID="txtDateValue6" data-date="" data-date-format="DD MMMM YYYY"  class="form-control customDate"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
             <label id="">DELIVERY ORDER COLLECTED DATE</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date"  AutoPostBack="true" runat="server" ID="txtDateValue7" data-date="" data-date-format="DD MMMM YYYY"  class="form-control customDate"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
             <label id="">GOODS CLEARED ON</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date"  AutoPostBack="true" runat="server" ID="txtDateValue8" data-date="" data-date-format="DD MMMM YYYY"  class="form-control customDate"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
             <label id="">USANCE DUE DATE</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date"  AutoPostBack="true" runat="server" ID="txtDateValue9" data-date="" data-date-format="DD MMMM YYYY"  class="form-control customDate"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
             <label id="">HYPO LOAN DUE DATE</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date"  AutoPostBack="true" runat="server" ID="txtDateValue10" data-date="" data-date-format="DD MMMM YYYY"  class="form-control customDate"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-3">
             <label id="">SHIPPING GURANTEE SETTLE DATE</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox type="date"  AutoPostBack="true" runat="server" ID="txtDateValue11" data-date="" data-date-format="DD MMMM YYYY"  class="form-control customDate"></asp:TextBox>
        </div>       
    </div>

    <div class="form-group">
        <div class="col-sm-3">
            <label for="lblHypoload">Hypo  Loan</label>
        </div>
        <div class="col-sm-3">
            <div class="input-group">
                <span class="input-group-addon">
                <asp:RadioButton ID="rdoHypoLoanYes" runat="server"   GroupName="HypoLoan"  Checked  >
                </asp:RadioButton>
                </span>
                <asp:TextBox ID="txtHypoLoanYes" disabled="disabled" runat="server"  class="form-control" Text="Yes" ></asp:TextBox>
            </div>
        </div>   
        <div class="col-sm-3">
            <div class="input-group">
                <span class="input-group-addon">
                <asp:RadioButton ID="rdoHypoLoanNo"  runat="server"  GroupName="HypoLoan"  ></asp:RadioButton>
                </span>
            <asp:TextBox ID="txtHypoLoanNo" disabled="disabled" runat="server" class="form-control" Text="No"></asp:TextBox>
        </div>
        </div>         
    </div>
       
    <div class="form-group">
        <div class="col-sm-3">
            <label for="">Refund</label>
        </div>
        <div class="col-sm-3">
            <div class="input-group">
                <span class="input-group-addon">
                <asp:RadioButton ID="rdoRefundYes"  runat="server" OnCheckedChanged="rdoRefundYes_CheckedChanged" AutoPostBack="true" GroupName="Refund"    >
                </asp:RadioButton>
                </span>
                <asp:TextBox ID="txtRefundYes" disabled="disabled" runat="server"  class="form-control" Text="Yes" ></asp:TextBox>
            </div>
        </div>   
        <div class="col-sm-3">
            <div class="input-group">
                <span class="input-group-addon">
                <asp:RadioButton ID="rdoRefundNo" runat="server" OnCheckedChanged="rdoRefundNo_CheckedChanged" AutoPostBack="true" GroupName="Refund" Checked ></asp:RadioButton>
                </span>
            <asp:TextBox ID="txtRefundNo" disabled="disabled" runat="server" class="form-control" Text="No"></asp:TextBox>
        </div>
        </div>         
    </div>
     <div id="divRefund" runat="server" visible="false">
         <div class="form-group">
        <div class="col-sm-3">
            <label for="">Refund Amount</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox ID="txtRefundAmount" runat="server" type="number"  class="form-control" value="0" ></asp:TextBox>
        </div>   
             </div>
         <div class="form-group">
        <div class="col-sm-3">
            <label for="">Refund Remarks</label>
        </div>
        <div class="col-sm-3">
            <asp:TextBox ID="txtRefundRemark"  runat="server" TextMode="MultiLine"  class="form-control" Text="" ></asp:TextBox>
        </div>       
             </div>  
    </div>
</div>

 <div class="box-footer pull-right">
     <asp:Button runat="server" ID="btnClearTab2" class="btn btn-danger" OnClick="btnClearTab2_Click" Text="Clear"></asp:Button>
    <asp:Button runat="server" ID="btnSaveTab2" class="btn btn-success" OnClick="btnSaveTab2_Click" Text="Save"></asp:Button>     
</div>
           
