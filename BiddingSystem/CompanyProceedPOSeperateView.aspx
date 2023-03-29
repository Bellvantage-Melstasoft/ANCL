<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyProceedPOSeperateView.aspx.cs" Inherits="BiddingSystem.CompanyProceedPOSeperateView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
<style type="text/css">
      body{}
    .tablegv {
        font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
        border-collapse: collapse;
        width: 100%;
    }
    .tablegv td, .tablegv th {
        border: 1px solid #ddd;
        padding: 8px;
    }
    .tablegv tr:nth-child(even){background-color: #f2f2f2;}
    .tablegv tr:hover {background-color: #ddd;}
    .tablegv th {
        padding-top: 12px;
        padding-bottom: 12px;
        text-align: left;
        background-color: #3C8DBC;
        color: white;
    }
    .successMessage
        {
            color: #1B6B0D;
            font-size: medium;
        }
        
        .failMessage
        {
            color: #C81A34;
            font-size: medium;
        }
      table, td, tr {
      border-color:black;
      
      }
  </style>
<script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>

    <section class="content-header">
    <h1>
       Raise Purchase Order
        <small></small>
      </h1>
    <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li><a href="CompanyProceedPOSeperate.aspx">Proceed PO</a></li>
        <li class="active">Raise Purchase Order</li>
      </ol>
    </section>
    <br />

     <form runat="server" id="form1">
      <section class="content">

  <%-- <div id="modalSelectCheckBox" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style="background-color:#e66657">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h2 id="lblTitle" style="color:white;" class="modal-title">Alert!!</h2>
            </div>
            <div class="modal-body" style="background-color:white">
                <h4>Atleast select one Item to Approve PO</h4>
            </div>
            <div class="modal-footer" style="background-color:white">
                <button id="btnOkAlert"  type="button" class="btn btn-danger" >OK</button>
            </div>
        </div>
    </div>
</div>--%>
   <div id="modalRejectSubmitCheckBox" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style="background-color:#e66657">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h2 id="lblTitleR" style="color:white;" class="modal-title">Alert!!</h2>
            </div>
            <div class="modal-body" style="background-color:white">
                <h4>Atleast select one Item to Reject PO</h4>
            </div>
            <div class="modal-footer" style="background-color:white">
                <button id="btnOkAlertR"  type="button" class="btn btn-danger" >OK</button>
            </div>
        </div>
    </div>
</div>
   <div id="modalReject" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 id="lblTitleConfirmYesNo2" class="modal-title">Confirmation</h4>
            </div>
            <div class="modal-body">
                <p>Are you sure you want to reject this PO? Or ? </p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="Button1" runat="server"  CssClass="btn btn-primary" OnClick="btnReject_Click"  Text="Yes" ></asp:Button>
                <button id="btnNoConfirmYesNo2"  type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>
   <div id="myModal" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
						<h4 class="modal-title">Attachment</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvUploadFiles" runat="server" CssClass="table table-responsive tablegv" style="border-collapse:collapse;color:  black;"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="PrId" HeaderText="PrId" />
            <asp:BoundField DataField="FilePath" HeaderText="FilePath"/>
            <asp:BoundField DataField="FileName" HeaderText="FileName" />
         
              <asp:TemplateField HeaderText="View">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnEdit" ImageUrl="~/images/view-icon-614x460.png"  style="width:39px;height:26px"
                          runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnCancelRequest" ImageUrl="~/images/Downloads2.png"  style="width:26px;height:20px;margin-top:4px;"
                          runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>               
    </div> 
     <div>
          <label id="lbMailMessage"  style="margin:3px; color:maroon; text-align:center;"></label>
     </div>
                </div>	
			</div>
		  </div>
		</div>
	  </div>
     </div>
   <div class="content" style="position: relative;background: #fff;overflow:hidden; border: 1px solid #f4f4f4;" id="divPrintPo" runat="server" >    <!-- Main content -->
      <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header" style="text-align:center;">
            <i class="fa fa-envelope"></i> PURCHASE ORDER (PO)
          </h2>
        </div>
        <!-- /.col -->
      </div>
      <div class="row">
          <div class="col-sm-4">
          <address>
         <table >
             <tr>
                 <td>Date&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblDateNow" runat="server" Text=""></asp:Label></b></td>
             </tr>
           <%--  <tr>
                 <td>PO. No&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblPOCode" runat="server" Text=""></asp:Label></b></td>
             </tr>--%>
             <tr>
                 <td>Your Ref&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblRefNo" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
                 <td>Based PR&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblBasedPr" runat="server" Text=""></asp:Label></b></td>
             </tr>
         </table>
           
          </address>
          </div>
          <div class="col-sm-4">
          <address>

               <table >
             <tr>
                 <td>Company&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
                 <td>VAT No&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblVatNo" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
                 <td>Telephone&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label></b></td>
             </tr>
                     <tr>
                 <td>Fax&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblFaxNo" runat="server" Text=""></asp:Label></b></td>
             </tr>
         </table>

          </address>
          </div>
        <!-- /.col -->
          <div class="col-sm-4">
          <address>
              <table >
             <tr>
                 <td>Supplier Name&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblSupplierName" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
                 <td>Address&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></b></td>
             </tr>
             
         </table>
        
          </address>
          </div>
      </div>
      <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
        <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive table-bordered"
         AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
        <Columns>
            <asp:TemplateField HeaderStyle-CssClass="hidden" Visible="false">
               <ItemTemplate>
                   <asp:CheckBox ID="CheckBox1" runat="server" CssClass="hidden" />
               </ItemTemplate>
               <HeaderTemplate>
                   <asp:CheckBox ID="CheckBox2" runat="server" CssClass="hidden" onclick="CheckUncheckCheckboxes(this);"/>
               </HeaderTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemId" HeaderText="Item No" />
            <asp:BoundField DataField="_AddItem.ItemName" HeaderText="Description" />
            <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CustomizedAmount" HeaderText="CustomizedAmount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
                 <asp:Label runat="server" ID="txtApproved" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedAmount","{0:F2}") : Eval("ItemPrice","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>

             <asp:BoundField DataField="Quantity" HeaderText="Quantity" />

           
            <asp:BoundField DataField="VatAmount" HeaderText="Vat Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CustomizedVat" HeaderText="Vat Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CustomizedNbt" HeaderText="NBT Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CustomizedAmount" HeaderText="Total Amount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>

            <asp:BoundField DataField="CustomizedTotalAmount" HeaderText="CustomizedTotalAmount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>

            <asp:BoundField DataField="IsCustomizedAmount" HeaderText="IsCustomizedAmount"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:TemplateField HeaderText="Vat Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
                 <asp:Label runat="server" ID="txtApprovedVat" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedVat","{0:F2}") : Eval("VatAmount","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NBT Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
                 <asp:Label runat="server" ID="txtApprovedNbt" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedNbt","{0:F2}") : Eval("NbtAmount","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
                 <asp:Label runat="server" ID="txtApprovedAmount" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedTotalAmount","{0:F2}") : Eval("TotalAmount","{0:F2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
      <div class="row no-print">

         


          <div class="">
               <div class="col-md-6">
                   <div class="form-group">
                <label for="exampleInputEmail1">Remarks</label><label id="Label2" style="color:red"></label>
               
                <asp:TextBox runat="server" TextMode="MultiLine" Rows="3" ID="txtReferenceNo" CssClass="form-control"></asp:TextBox>
                </div>
               
                </div>
          </div>
          
           
     



        <div class="col-xs-12">
          <asp:Button ID="btnApprove" runat="server" Text="Proceed PO" 
                class="btn btn-primary pull-right" style=" margin-left: 10px; "  OnClientClick="return validataAlFields(1);" ValidationGroup="btnApprove" onclick="btnApprove_Click"></asp:Button>
          <asp:Button ID="btnReject" runat="server" Text="Reject"  class="btn btn-danger pull-right" Visible="false" ></asp:Button>
        </div>

      </div>
    </div> 
    </section>
    </form>
     <script language = "javascript" type = "text/javascript">
         function CheckUncheckCheckboxes(CheckBox) {
             //get target base & child control.
             var TargetBaseControl = document.getElementById('<%= this.gvPurchaseOrderItems.ClientID %>');
             var TargetChildControl = "CheckBox1";

             //get all the control of the type INPUT in the base control.
             var Inputs = TargetBaseControl.getElementsByTagName("input");

             for (var n = 0; n < Inputs.length; ++n)
                 if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0) {
                     Inputs[n].checked = CheckBox.checked;
                 }
                 else {

                 }
         }
   </script>

    <script>

        function validataAlFields(status) {
            var isValid = false;
            var count = 0;
            var gridView = document.getElementById('<%= gvPurchaseOrderItems.ClientID %>');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null && inputs[0] != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            count++;

                        }
                    }
                }
            }

//            if (count > 0) {
//                if (status == 1) {


//                }
//                if (status == 2) {
//                    $('#modalReject').modal('show');
//                    return false;
//                }

//            }

//            else {
//                if (status == 1) {
//                    $('#modalSelectCheckBox').modal('show');
//                }
//                if (status == 2) {
//                    $('#modalRejectSubmitCheckBox').modal('show');
//                }

//                return false;
//            }
        }



//        $("#btnOkAlert").click(function () {
//            $('#modalSelectCheckBox').modal('hide');
//        });

//        $("#btnOkAlertR").click(function () {
//            $('#modalRejectSubmitCheckBox').modal('hide');
//        });
//        $("#btnNoConfirmYesNo2").click(function () {
//            $('#modalReject').modal('hide');
//        });
//        $("#ContentSection_btnReject").click(function () {
//            $('#modalReject').modal('show');
//            return false;
//        });

        
    </script>



</asp:Content>
