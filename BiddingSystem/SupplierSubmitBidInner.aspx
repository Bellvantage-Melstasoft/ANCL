<%@ Page Title="" Language="C#" MasterPageFile="~/SupplierLoadingWebUIInner.Master" AutoEventWireup="true" CodeBehind="SupplierSubmitBidInner.aspx.cs" Inherits="BiddingSystem.SupplierSubmitBidInner" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <style type="text/css">
      .customers {
          font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
          border-collapse: collapse;
          width: 100%;
      }
      
      .customers td, .customers th {
          border: 1px solid #ddd;
          padding: 8px;
      }
      
      .customers tr:nth-child(even){background-color: #f2f2f2;}
      
      .customers tr:hover {background-color: #ddd;}
      
      .customers th {
          padding-top: 12px;
          padding-bottom: 12px;
          text-align: center;
          background-color: #467394;
          color: white;
      }
      .customers td {
          padding-top: 12px;
          padding-bottom: 12px;
          text-align: center;
          color :  White;
      }
      input[type=number]::-webkit-inner-spin-button, 
               input[type=number]::-webkit-outer-spin-button { 
               -webkit-appearance: none; 
                margin: 0; 
               }
    </style>
   <form runat="server" id="form1">
   <div id="modalConfirmYesNo" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 id="lblTitleConfirmYesNo" class="modal-title">Confirmation</h4>
            </div>
            <div class="modal-body">
                <p>Are you sure to submit your details ?</p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="btnSubmit" runat="server"  CssClass="btn btn-primary"   onclick="btnSubmit_Click" Text="Yes" ></asp:Button>
                <button id="btnNoConfirmYesNo"  type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
    </div>

   <div id="SuccessAlert" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header"  style="background-color:#3c8dbc;color:White;font-weight:bold;">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4  class="modal-title">Success</h4>
            </div>
            <div class="modal-body">
                <p style="font-weight:bold; font-size:medium;">Bid has been Submitted Successfully</p>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnOk" runat="server"  OnClick="btnOK_Click" CssClass="btn btn-info" Text="OK" ></asp:Button>
            </div>
        </div>
    </div>
</div>
   
    <div class="services-breadcrumb" style="background-color: #005383">
		<div class="agile_inner_breadcrumb">
			<div class="container">
				<ul class="w3_short">
					<li>
						<a href="SupplierInitialFrontViewInner.aspx" style="color:White">Home</a>
						<i style="color:White">|</i>
                        <a href="#" style="color:Yellow">Bid Details</a>
					</li>
				</ul>
			</div>
		</div>
	</div>
  
     <div class="faqs-w3l" style="background-color:White">
         <div class="container">
			<!-- //tittle heading -->
            <div class="row">
            <div class="col-md-12">
     
    <div class="row" style="margin-right:30px;"  visible="false">
           <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" ForeColor="Red" runat="server"></asp:Label>
           </strong>
        </div>
   </div>

	<div class="row">  
     <div class="col-md-12">
      <div class="col-md-6" style="margin-left: -17px;">
      <div id="" style="margin-left: 60px;margin-top: 0px;" class="span9">
      <div class="span3" style="margin-left: 0px;">	  
          <h4 style="color:Black">Item :&nbsp;&nbsp;<asp:Label ID="lblItemName" runat="server" Text=""></asp:Label> </h4>
          <br />
          <h4 style="color:Black"> Quantity :&nbsp;&nbsp;<asp:Label ID="lblQuontity" runat="server" Text=""></asp:Label></h4>
          <br />
          <h4 style="color:Black">Company :&nbsp;&nbsp;<asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label> </h4>
          <br />
      </div>
      <div class="span3" style="margin-left: 0px;">
         
      </div>
      </div>
      </div>
      <div class="col-md-6" style="margin-left: 0px;">
          
          <h4 style="color:Red;font-weight:bold;" id="demo" class="pull-right"></h4>
          <h4 id="H1" style="color: red;font-weight:bold" class="pull-right">
          </h4>
          <br />
          <br />
          <div id="image" class="pull-right"></div>
      </div>
   
    <div class="col-md-6">
    
    </div>
   </div>
    <hr class="span9" style="border-bottom-color:  black;"/>
    <div class="span1"></div>
    <div class="span8">
       <div class="row">
        <div class="col-xs-12 table-responsive">
          <table class="table table-striped" style="background-color:#ecfbeb;">
            <tr>
                <td style="color:Black;font-weight:bold;">
                    Unit Price:
                </td>
                <td>
                    <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="color:Black;font-weight:bold;">
                    Total Price:
                </td>
                <td>
                    <asp:TextBox ID="txtTotalPrice" runat="server" CssClass="form-control" laceholder=""></asp:TextBox>
                </td>
                <td style="color:Black;font-weight:bold;">
                     VAT/NBT Inclusive
                </td>
                <td>
                    <asp:CheckBox ID="chkVatNbt" runat="server"/>
                </td>
            </tr>
            <tr>
                <td style="color:Black;font-weight:bold;">
                    NBT:
                </td>
                <td>
                    <asp:TextBox ID="txtNBT" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="color:Black;font-weight:bold;">
                    VAT:
                </td>
                <td>
                    <asp:TextBox ID="txtVAT" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
             <tr>
                <td style="color:Black;font-weight:bold;">
                    Sub Total:
                </td>
                <td>
                    <asp:TextBox ID="txtSubTotal" runat="server" CssClass="form-control"  placeholder=""></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        </div>
        <!-- /.col -->
      </div>
	</div>
	<div class="span9">
	<form class="form-horizontal qtyFrm">
	<div class="control-group"> 
    <br />
	<div class="controls">
      <h4 class="span2"style="margin-left: 0px;">BOM (Bill Of Material)</h4>
	  <br/>
    </div>
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gvBOM" runat="server" CssClass="table table-responsive customers" GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderStyle-ForeColor="White" ItemStyle-ForeColor="Black" DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField HeaderStyle-ForeColor="White" ItemStyle-ForeColor="Black" DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField HeaderStyle-ForeColor="White" ItemStyle-ForeColor="Black" DataField="SeqId" HeaderText="Seq Id" />
            <asp:BoundField HeaderStyle-ForeColor="White" ItemStyle-ForeColor="Black" DataField="Meterial" HeaderText="Material" />
            <asp:BoundField HeaderStyle-ForeColor="White" ItemStyle-ForeColor="Black" DataField="Description" HeaderText="Description" />
            <asp:TemplateField HeaderText="Comply"  HeaderStyle-ForeColor="White">
              <ItemTemplate>
                  <asp:RadioButton ID="RadioButtonYes" style="color:Black;"  runat="server" GroupName="Comply" Text="Yes" /> &nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:RadioButton ID="RadioButtonNo"  style="color:Black;" runat="server" GroupName="Comply" Text="No"/>
              </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Remarks" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                 <asp:TextBox ID="txtRemarks" style="color:Black;" runat="server" TextMode="MultiLine"></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
	</div>
	</form>
    <br/>
	<hr class="soft clr" style="border-bottom-color:  black;"/>	
   <div class="span9"style="margin-left: 0px;">
     <h4 class="span3" style="margin-left: 0px;color:Black;"> Supplier terms and conditions </h4><br />
       <asp:TextBox ID="txtTermsConditions" runat="server" CssClass="span6" Rows="4" Columns="150" style="margin-left: 30px;" TextMode="MultiLine"></asp:TextBox>
   </div>
   <div class="span9"style=" margin-left: 0px;">
         <br/>
                                 
           <div class="control-group">
             <label for="exampleInputEmail1"  style="font-weight:bold;">Attachments(One time 10 files can be uploaded)</label>
          <label style="font-weight:bold; color:red;" id="MultipleFileError"></label>

                 <div class="input-group margin">
                          <asp:FileUpload runat="server"  ID="FileUpload1" CssClass="form-control input-large" style="border:solid 1px;border-radius: 5px;padding:2px;border-color:#cccccc;" AllowMultiple="true" onchange="readFilesURL(this);" />
                     <span class="input-group-btn">
                     <button class="btn btn-info btn-flat" id="clearFiles">Clear</button>
                    </span>
                     </div>


             <div class="row"  style="overflow-y:auto; overflow-x:hidden; max-height:300px;">

                  <table id="tblUpload" class="table table-hover" style="border:none;margin-left:30px;"  border="1">
                 </table>

              </div>   

          </div>

     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
           <ContentTemplate>
           <div>
               <asp:GridView runat="server" ID="gvUserDocuments" EmptyDataText="No Files Found" OnPageIndexChanging="gvUserDocuments_OnPageIndexChanged" PageIndex="3" AllowPaging="true" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive ">
             <Columns>
                 <asp:BoundField DataField="SupplierId" HeaderText="SupplierId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                 <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                 <asp:BoundField DataField="PrId" HeaderText="PrId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                 <asp:BoundField DataField="ItemId" HeaderText="ItemId"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                 <asp:BoundField DataField="FileName" HeaderText="FilePath" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                 <asp:BoundField DataField="FilePath" HeaderText="FileName"  />
                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton runat="server" ForeColor="#0099cc"  OnClick="lbtnview_Click" ID="lbtnview">View</asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton runat="server" ForeColor="Red" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete this item?');" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton runat="server" ForeColor="Blue" ID="lbtnDownload" OnClick="lbtnDownload_Click" >Download</asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
          </asp:GridView>
            </div>
        </ContentTemplate>
           </asp:UpdatePanel>   
    </div>
    <hr class="span9"style=" margin-left: 0px;border-bottom-color:  black;"/>
    <div class="span9"style=" margin-left: 0px;">
    <h5 class="span5"style="margin-left: 0px;"> </h5>
    <button type="button" onclick="return validate();"  style="padding: 11px;padding-left: 40px;padding-right: 40px;" class="btn btn-primary pull-right">Submit</button>
    <label id="lblError" style="color:red;"></label>
   <br/>
   </div>
   </div>
   </div>
   </div>
   </div>
   </div>
   </div>
   </form>
     <script src="AppResources/themes/js/jquery.js" type="text/javascript"></script>

     <script type="text/javascript">
     var Image = <%= getJsonItemImagePath() %>
     LoadImage();
     function LoadImage(){
        var text = "";
        var imagePath = "";
        var BidImagePath =  "";
         imagePath = Image.split('~')
         BidImagePath = imagePath[1];
            var htmlcode =
				  '<div class="thumbnail" style="background-color:#27671f;">' +
				  '<a  href="#"><img src='+BidImagePath+'  alt="" style="height:136px"/></a>' +
				  '</div>' + '' 
        document.getElementById("image").innerHTML = htmlcode;
    }
    </script>

     <script type="text/javascript">

        var EndDate = <%= getJsonEndDateTime() %>
        var Quontity = <%= getJsonItemQuontity() %>
        var endDate = new Date(EndDate);
        var d = endDate;
        var time01   =  (
                ("00" + (d.getMonth() + 1)).slice(-2) + " " + 
                ("00" + d.getDate()).slice(-2) + " ," + 
                d.getFullYear() + " " + 
                ("00" + d.getHours()).slice(-2) + ":" + 
                ("00" + d.getMinutes()).slice(-2) + ":" + 
                ("00" + d.getSeconds()).slice(-2)
            );
        // Set the date we're counting down to
        var countDownDate = new Date(time01).getTime();
        // Update the count down every 1 second
        var x = setInterval(function () {
            // Get todays date and time
            var now = new Date().getTime();
            // Find the distance between now an the count down date
            var distance = countDownDate - now;
            // Time calculations for days, hours, minutes and seconds
            var days = Math.floor(distance / (1000 * 60 * 60 * 24));
            var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);
            // Display the result in the element with id="demo"
            document.getElementById("demo").innerHTML = "Expired On : " + days + "d " + hours + "h " + minutes + "m " + seconds + "s ";
            // If the count down is finished, write some text 
            if (distance < 0) {
                clearInterval(x);
                document.getElementById("demo").innerHTML = "EXPIRED";
            }
        }, 1000);

        $("#ContentPlaceHolder1_txtUnitPrice").keyup(function () {
        if ($(this).val()!="") {
        debugger;
            var price = parseFloat($(this).val());
            var quontity = parseFloat(Quontity);
            var totalPrice = quontity * price;
            $('#ContentPlaceHolder1_txtTotalPrice').val(totalPrice);
            if($("#ContentPlaceHolder1_txtTotalPrice").val() != "" && $("#ContentPlaceHolder1_txtUnitPrice").val() != ""){
                $('#ContentPlaceHolder1_txtSubTotal').val(totalPrice.toFixed(2));
            }
        }
        else {
            $("#ContentPlaceHolder1_txtTotalPrice").val("");
            $("#ContentPlaceHolder1_txtUnitPrice").val("");
            $('#ContentPlaceHolder1_txtNBT').val("");
            $('#ContentPlaceHolder1_txtVAT').val("");
        }
       });

       $("#ContentPlaceHolder1_chkVatNbt").change(function() {
            if(this.checked) {
                var totPrice = parseFloat($('#ContentPlaceHolder1_txtTotalPrice').val());
                var NbtAmount = parseFloat((totPrice * 2)/98);

                if($("#ContentPlaceHolder1_txtTotalPrice").val() != "" && $("#ContentPlaceHolder1_txtUnitPrice").val() != ""){
                $('#ContentPlaceHolder1_txtNBT').val((NbtAmount).toFixed(2));
                var VatAmount = parseFloat((totPrice + NbtAmount)*0.15);
                $('#ContentPlaceHolder1_txtVAT').val((VatAmount).toFixed(2));
                var SubTotalVal = (totPrice + NbtAmount + VatAmount);
                $('#ContentPlaceHolder1_txtSubTotal').val(SubTotalVal.toFixed(2));
                }
                if($("#ContentPlaceHolder1_txtTotalPrice").val() == "" && $("#ContentPlaceHolder1_txtUnitPrice").val() == ""){
                  $('#ContentPlaceHolder1_txtSubTotal').val("");
                   $("#ContentPlaceHolder1_txtTotalPrice").val("");
                   $("#ContentPlaceHolder1_txtUnitPrice").val("");
                   $('#ContentPlaceHolder1_txtNBT').val("");
                   $('#ContentPlaceHolder1_txtVAT').val("");
                }
            }
            else{
                $('#ContentPlaceHolder1_txtNBT').val("");
                $('#ContentPlaceHolder1_txtVAT').val("");
                if($("#ContentPlaceHolder1_txtTotalPrice").val() == "" && $("#ContentPlaceHolder1_txtUnitPrice").val() == ""){
                   $('#ContentPlaceHolder1_txtSubTotal').val("");
                }
                if($("#ContentPlaceHolder1_txtTotalPrice").val() != "" && $("#ContentPlaceHolder1_txtUnitPrice").val() != ""){
                   var TotatlPriceWithoutNbtVat = parseFloat($('#ContentPlaceHolder1_txtTotalPrice').val());
                   $('#ContentPlaceHolder1_txtSubTotal').val(TotatlPriceWithoutNbtVat.toFixed(2));
                }
            }
       });



          $("#ContentPlaceHolder1_txtNBT").keyup(function () {
            if ($("#ContentPlaceHolder1_chkVatNbt").prop('checked') == true && $("#ContentPlaceHolder1_txtUnitPrice").val() != "") {

                if ($(this).val() != "") {

                    var price = parseFloat($("#ContentPlaceHolder1_txtUnitPrice").val() == "" ? 0 : $("#ContentPlaceHolder1_txtUnitPrice").val());
                    var nbtAmount = parseFloat($("#ContentPlaceHolder1_txtNBT").val() == "" ? 0 : $("#ContentPlaceHolder1_txtNBT").val());
                    var vatAmount = parseFloat($("#ContentPlaceHolder1_txtVAT").val() == "" ? 0 : $("#ContentPlaceHolder1_txtVAT").val());
                    var quontity = parseFloat(Quontity);
                    var totalPrice = (quontity * price) + nbtAmount + vatAmount;
                    $('#ContentPlaceHolder1_txtSubTotal').val(totalPrice.toFixed(2));
                }
                else {

                    var price = parseFloat($("#ContentPlaceHolder1_txtUnitPrice").val() == "" ? 0 : $("#ContentPlaceHolder1_txtUnitPrice").val());
                    var nbtAmount = parseFloat($("#ContentPlaceHolder1_txtNBT").val() == "" ? 0 : $("#ContentPlaceHolder1_txtNBT").val());
                    var vatAmount = parseFloat($("#ContentPlaceHolder1_txtVAT").val() == "" ? 0 : $("#ContentPlaceHolder1_txtVAT").val());
                    var quontity = parseFloat(Quontity);
                    var totalPrice = (quontity * price) + nbtAmount + vatAmount;
                    $('#ContentPlaceHolder1_txtSubTotal').val(totalPrice.toFixed(2));
                }
            }
            else { $("#ContentPlaceHolder1_txtNBT").val(""); }
        });



         $("#ContentPlaceHolder1_txtVAT").keyup(function () {
             if ($("#ContentPlaceHolder1_chkVatNbt").prop('checked') == true && $("#ContentPlaceHolder1_txtUnitPrice").val() != "") {

                 if ($(this).val() != "") {

                     var price = parseFloat($("#ContentPlaceHolder1_txtUnitPrice").val() == "" ? 0 : $("#ContentPlaceHolder1_txtUnitPrice").val());
                     var nbtAmount = parseFloat($("#ContentPlaceHolder1_txtNBT").val() == "" ? 0 : $("#ContentPlaceHolder1_txtNBT").val());
                     var vatAmount = parseFloat($("#ContentPlaceHolder1_txtVAT").val() == "" ? 0 : $("#ContentPlaceHolder1_txtVAT").val());
                     var quontity = parseFloat(Quontity);
                     var totalPrice = (quontity * price) + nbtAmount + vatAmount;
                     $('#ContentPlaceHolder1_txtSubTotal').val(totalPrice.toFixed(2));
                 }
                 else {

                     var price = parseFloat($("#ContentPlaceHolder1_txtUnitPrice").val() == "" ? 0 : $("#ContentPlaceHolder1_txtUnitPrice").val());
                     var nbtAmount = parseFloat($("#ContentPlaceHolder1_txtNBT").val() == "" ? 0 : $("#ContentPlaceHolder1_txtNBT").val());
                     var vatAmount = parseFloat($("#ContentPlaceHolder1_txtVAT").val() == "" ? 0 : $("#ContentPlaceHolder1_txtVAT").val());
                     var quontity = parseFloat(Quontity);
                     var totalPrice = (quontity * price) + nbtAmount + vatAmount;
                     $('#ContentPlaceHolder1_txtSubTotal').val(totalPrice.toFixed(2));
                 }
             }
              else { $("#ContentPlaceHolder1_txtVAT").val(""); }
       });


         $("#ContentPlaceHolder1_txtUnitPrice").keyup(function () {
             if ($(this).val() != "") {
                 if ($(this).val() > 0) {

                   var price = parseFloat($(this).val());
                   var quontity = parseFloat(Quontity);
                   var totalPrice = quontity * price;
                   $('#ContentPlaceHolder1_txtTotalPrice').val(totalPrice);
                   if($("#ContentPlaceHolder1_txtTotalPrice").val() != "" && $("#ContentPlaceHolder1_txtUnitPrice").val() != ""){
                   $('#ContentPlaceHolder1_txtSubTotal').val(totalPrice.toFixed(2));
                     }

                     if ($("#ContentPlaceHolder1_chkVatNbt").prop('checked'))
                     {
                         var totPrice = parseFloat($('#ContentPlaceHolder1_txtTotalPrice').val());
                         var NbtAmount = parseFloat((totPrice * 2)/98);
                         $('#ContentPlaceHolder1_txtNBT').val((NbtAmount).toFixed(2));
                         var VatAmount = parseFloat((totPrice + NbtAmount) * 0.15);
                         $('#ContentPlaceHolder1_txtVAT').val((VatAmount).toFixed(2));
                         var SubTotalVal = (totPrice + NbtAmount + VatAmount);
                         $('#ContentPlaceHolder1_txtSubTotal').val(SubTotalVal.toFixed(2));
                     }
                     else
                     {
                         $('#ContentPlaceHolder1_txtNBT').val("");
                          $('#ContentPlaceHolder1_txtVAT').val("");
                     }
                 }
                 else
                 {
                     $("#ContentPlaceHolder1_txtTotalPrice").val("");
                     $('#ContentPlaceHolder1_txtNBT').val("");
                     $('#ContentPlaceHolder1_txtVAT').val("");
                     $('#ContentPlaceHolder1_txtUnitPrice').val("");
                      $('#ContentPlaceHolder1_txtSubTotal').val("");

                 }
        }
        else {
         
          $("#ContentPlaceHolder1_txtTotalPrice").val("");
          $('#ContentPlaceHolder1_txtNBT').val("");
          $('#ContentPlaceHolder1_txtVAT').val("");
          $('#ContentPlaceHolder1_txtSubTotal').val("");
        }
        });

        // ---------------------------------------------Start- Separate (1,000)----------------------------
        function thousandsSeparatorAdd(input) {
            var output = input
            if (parseFloat(input)) {
                input = new String(input);
                var parts = input.split(".");
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1,").split("").reverse().join("");
                output = parts.join(".");
            }
            return output;
        }
        function thousandsSeparatorRemove(input) {
            input = input.replace(/,/g, '');
            return input;
        }
      // ---------------------------------------------End--- Separate (1,000)----------------------------
    </script>


     <script type="text/javascript">
         //---------------------------------Validate Form------------------------------------------
        $("#btnNoConfirmYesNo").on('click').click(function () {
                     var $confirm = $("#modalConfirmYesNo");
                     $confirm.modal('hide');
                     return this.false;
        });
    </script>

     <script type="text/javascript">
        function validate()
        {
            var icount = 0;
            var vatValidate = false;
            var gv = document.getElementById("<%= gvBOM.ClientID %>");    
            if( gv  != null)
            {
                for (var i = 1; i < gv.rows.length; i++) {
                    var row = gv.rows[i];
                    var targetcell = row.cells[5];
                    var inputs = targetcell.getElementsByTagName("input");

                    var radioCheck = 0;
                    for (var j = 0; j < inputs.length; j++)
                        if (inputs[j].checked) {
                            icount++;
                            radioCheck++;
                            break;
                        }
                    if (radioCheck == 0) {
                        gv.rows[i].style.backgroundColor = 'Bisque';
                    }
                    else
                    {
                        gv.rows[i].style.backgroundColor = 'white';
                    }
                }
            }
            if (<%=chkVatNbt.ClientID%>.checked == true)
            {
                if ( <%=txtNBT.ClientID%>.value != "" && <%=txtVAT.ClientID%>.value != "" )
                {
                       <%=txtNBT.ClientID%>.style.backgroundColor = "white";
                      <%=txtVAT.ClientID%>.style.backgroundColor = "white";
                    vatValidate = true;
                }
                else
                {
                    if ( <%=txtNBT.ClientID%>.value == "")
                        <%=txtNBT.ClientID%>.style.backgroundColor = "Bisque";
                    else
                         <%=txtNBT.ClientID%>.style.backgroundColor = "white";

                      if ( <%=txtVAT.ClientID%>.value == "")
                        <%=txtVAT.ClientID%>.style.backgroundColor = "Bisque";
                      else
                            <%=txtVAT.ClientID%>.style.backgroundColor = "white";
                     vatValidate = false;
                }
            }
            else
            {
                vatValidate = true;
            }


            if (vatValidate == true &&  $('#ContentPlaceHolder1_txtSubTotal').val() !="")
            {
                if(gv != null)
                {
                    if(icount == (gv.rows.length - 1))
                    {
                        var $confirm = $("#modalConfirmYesNo");
                        $confirm.modal('show');
                        $("#lblError").text("");
                        return true;
                    }
                }
                else
                {
                    var $confirm = $("#modalConfirmYesNo");
                    $confirm.modal('show');
                    $("#lblError").text("");
                    return true;
                }

               
            }
            else { 
                $("#lblError").text("Some Fields are Required!!");
                return false;
            }
        }
      //------------------------------End Validate Form----------------------------------
    </script>

     <script  type="text/javascript">
       $(document).ready(function () {
           var alert = <%= getJsonSuccessAlert()%>;
           if (alert != "")
           {
                 var $confirm = $("#SuccessAlert");
                 $confirm.modal('show');
           }
       });  

   </script>
    
     <script type="text/javascript">
         function readFilesURL(input) {
             var output = document.getElementById('tblUpload');
             output.innerHTML = '<tr>';
             output.innerHTML += '<th style="background-color:#e8e8e8;"><b>(' + input.files.length + ') Files has been Selected </b></th>';
             for (var i = 0; i < input.files.length; ++i) {
                 output.innerHTML += '<td >' + input.files.item(i).name + '</td>';

             }
             output.innerHTML += '</tr>';
         }
     </script>
</asp:Content>
