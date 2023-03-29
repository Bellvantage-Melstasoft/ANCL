<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="CompanyViewAndApprovePO.aspx.cs" Inherits="BiddingSystem.CompanyViewAndApprovePO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style type="text/css">
        body {
        }

        .tablegv {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .tablegv td, .tablegv th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            .tablegv tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .tablegv tr:hover {
                background-color: #ddd;
            }

            .tablegv th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #3C8DBC;
                color: white;
            }

        .successMessage {
            color: #1B6B0D;
            font-size: medium;
        }

        .failMessage {
            color: #C81A34;
            font-size: medium;
        }

        table, td, tr {
            border-color: black;
        }
    </style>

    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <section class="content-header">
    <h1>
      Approve Purchase Order
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Approve Purchase Order </li>
      </ol>
    </section>
    <br />

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
        <section class="content" style="padding-top: 0px">

             <div id="modalSelectCheckBox" class="modal fade">
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
</div>
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
                <p>Are you sure you want to reject this PO?</p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="Button1" runat="server"  CssClass="btn btn-primary" OnClick="btnReject_Click"  Text="Yes" ></asp:Button>
                <button id="btnNoConfirmYesNo2"  type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>
 <div id="mdlAttachments" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Attachments Quotations</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <asp:Panel ID="pnlImages" runat="server">
                                                <label for="fileImages">Uploded Images</label>
                                                <asp:GridView ID="gvImages" runat="server" ShowHeader="False"
                                                    GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Image Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="QuotationImageId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink runat="server" href='<%#Eval("ImagePath").ToString().Remove(0,2)%>' Target="_blank">
                                                                            <asp:Image runat="server" ImageUrl='<%#Eval("ImagePath")%>' style="max-height:50px; width:auto; margin:5px" />
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group">
                                            <asp:Panel ID="pnlDocs" runat="server" Width="100%">
                                                <label for="fileImages">Uploded Documents</label>
                                                <asp:GridView ID="gvDocs" runat="server" ShowHeader="False"
                                                    GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Document Found" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="QuotationFileId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField ItemStyle-Height="30px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton
                                                                    Text='<%#Eval("FileName")%>' runat="server" href='<%#Eval("FilePath").ToString().Remove(0,2)%>' target="_blank" Style="margin-right: 5px;" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                         <div class="form-group">
                                            <asp:Panel ID="pnlcondtion" runat="server" Width="100%">
                                                <label for="fileImages">Terms And Conditons</label>
                                                <asp:TextBox TextMode="MultiLine" Rows="10"  ID="txtTermsAndConditions" Enabled="false"  runat="server" CssClass="form-control text-bold"></asp:TextBox>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
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
            <img src="AdminResources/images/logo.png" class="center-block" />
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
            <%-- <tr>
                 <td>Your Ref&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblRefNo" runat="server" Text=""></asp:Label></b></td>
             </tr>--%>
              <tr>
                 <td>PO Code&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblPOCode" runat="server" Text=""></asp:Label></b></td>
             </tr>
             <tr>
                 <td>PR Code&nbsp;</td>
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
                    
                   <tr>
                 <td>Store Keeper&nbsp;</td>
                   <td>:&nbsp;</td>
                   <td><b><asp:Label ID="lblstorekeeper" runat="server" Text=""></asp:Label></b></td>
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
            <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
               <ItemTemplate>
                   <asp:CheckBox ID="CheckBox1" runat="server" />
               </ItemTemplate>
               <HeaderTemplate>
                   <asp:CheckBox ID="CheckBox2" runat="server" onclick="CheckUncheckCheckboxes(this);"/>
               </HeaderTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemId" HeaderText="Item No" />
             <asp:TemplateField HeaderText="Attachments">
            <ItemTemplate>
                <asp:Button CssClass="btn btn-xs btn-default" OnClick="btnViewAttachments_Click" runat="server"
                    Text="View"></asp:Button>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="_AddItem.ItemName" HeaderText="Description" />
            <asp:BoundField DataField="supplierQuotationItem.Description" HeaderText="Detail" />
            <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CustomizedAmount" HeaderText="CustomizedAmount"  DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
                 <asp:Label runat="server" ID="txtApproved" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedAmount","{0:N2}") : Eval("ItemPrice","{0:N2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Quantity" HeaderText="Quantity"  />
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
                 <asp:Label runat="server" ID="txtApprovedVat" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedVat","{0:N2}") : Eval("VatAmount","{0:N2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="NBT Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
                 <asp:Label runat="server" ID="txtApprovedNbt" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedNbt","{0:N2}") : Eval("NbtAmount","{0:N2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Right">
             <ItemTemplate>
                 <asp:Label runat="server" ID="txtApprovedAmount" Text='<%#Eval("IsCustomizedAmount").ToString()=="1"? Eval("CustomizedTotalAmount","{0:N2}") : Eval("TotalAmount","{0:N2}") %>' ForeColor='<%#Eval("IsCustomizedAmount").ToString()=="1"?System.Drawing.Color.Red:System.Drawing.Color.Black %>'></asp:Label>
             </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
      <div class="row no-print">

         
          <div  class="col-md-4">
                   <label id="lblPaymentMethod">Payment Method</label><label id="lblPaymentMethodErr" style="color:red">(*)</label>
                  <%--<asp:RequiredFieldValidator ValidationGroup="btnApprove"  runat="server" ID="req1" ControlToValidate="ddlPaymentMethod" InitialValue="" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                  <asp:DropDownList runat="server" ID="ddlPaymentMethod" CssClass="form-control"  >
                     <asp:ListItem Value="">Select Payment Method</asp:ListItem>
                     <asp:ListItem Value="C">Cash</asp:ListItem>
                     <asp:ListItem Value="V">Cheque</asp:ListItem>
                  </asp:DropDownList>
          </div>

          <div  class="col-md-4">
                   <label id="Label1">Remarks</label><label id="lblRemarkErr" style="color:red">(*)</label>                 
                  <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRem" CssClass="form-control"  > </asp:TextBox>

          </div>

        <div class="col-xs-12">
             <asp:Button ID="btnReject" runat="server"  OnClientClick="return validateRemark();" Text="Reject"  style=" margin-left: 10px; "  class="btn btn-danger pull-right" ></asp:Button>
          <asp:Button ID="btnApprove" runat="server" Text="Approve" 
                class="btn btn-primary pull-right"  OnClientClick="return validataAlFields(1);" ValidationGroup="btnApprove" onclick="btnApprove_Click"></asp:Button>
         
        </div>

      </div>
    </div> 
    </section>
                </ContentTemplate>
            </asp:UpdatePanel>
    </form>
    <script language="javascript" type="text/javascript">
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


            if (status == 1) {

            var paymentMethod = $('#<%=ddlPaymentMethod.ClientID%>').val();
                if (paymentMethod != "") {
                    return true;
                }
                else {
                    $('#lblPaymentMethodErr').text('* Please Select Payment Method');
                    return false;
                }

            }
            if (status == 2) {
                $('#modalReject').modal('show');
                return false;
            }
    }
    $('#<%=ddlPaymentMethod.ClientID%>').change(function () {
            if (this.value == "") {
                $('#lblPaymentMethodErr').text('* Please Select Payment Method');
            } else {
                $('#lblPaymentMethodErr').text('(*)');
            }
        });

        $("#btnOkAlert").click(function () {
            $('#modalSelectCheckBox').modal('hide');
        });

        $("#btnOkAlertR").click(function () {
            $('#modalRejectSubmitCheckBox').modal('hide');
        });
        $("#btnNoConfirmYesNo2").click(function () {
            $('#modalReject').modal('hide');
        });

        function validateRemark() {
            if ($("#ContentSection_txtRem").val() == "") {
                $("#lblRemarkErr").text('* Please Fill This Field');
                return false;
            } else {
                $("#lblRemarkErr").text('(*)');
                $('#modalReject').modal('show');
                return false;
            }
        }
        //$("#ContentSection_btnReject").click(function () {
                  
        //});


    </script>


</asp:Content>
