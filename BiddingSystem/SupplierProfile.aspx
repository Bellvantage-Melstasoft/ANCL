<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSupplier.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="SupplierProfile.aspx.cs" Inherits="BiddingSystem.SupplierProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .form-control {
            width:97%
        }
        
    </style>

   <div class="span9">
    <ul class="breadcrumb">
    <li><a href="SupplierIndex.aspx">Home</a> <span class="divider">/</span></li>
    <li class="active"><a href="#">Update Profile</a></li>
    </ul>	
    <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" runat="server"></asp:Label>
           </strong>
    </div>
    <asp:HiddenField  ID="hdnFileUpdate" runat="server"/>
        <asp:HiddenField  ID="hdnUploadFileId" runat="server"/>
         <asp:HiddenField  ID="hdnUploadFilePath" runat="server"/>
        <asp:HiddenField  ID="hdnClearLogo" runat="server"/>

	<div class="row">	  
			<div class="span9">
				<h3>Supplier Profile</h3>
				<hr class="soft"/>
    <form class="form-horizontal qtyFrm">
        <div class="span9">

    <div id="modalDeleteYesNo" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 id="lblTitleDeleteYesNo" class="modal-title">Confirmation</h4>
            </div>
            <div class="modal-body">
                <p>Are you sure to Delete this Record ?</p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="btnDelete" runat="server"  CssClass="btn btn-primary"  OnClick="lbtnDelete_Click" Text="Yes" ></asp:Button>
                <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>


        
         <div class="span4">
             <div class="control-group">
            <label for="exampleInputEmail1"  style="font-weight:bold;">User Name</label>
            <asp:TextBox ID="txtUserName" Enabled="false" runat="server" CssClass="form-control" placeholder="username" ></asp:TextBox>
          </div>              
          <div class="control-group">
            <label for="exampleInputEmail1"  style="font-weight:bold;">Email Address</label>
            <asp:TextBox ID="txtEmailAddress" Enabled="false" runat="server" CssClass="form-control" placeholder="Email address" ></asp:TextBox>
       
          </div>


          <div class="control-group">
                 <label for="exampleInputEmail1" style="font-weight:bold;display:inline-block">Name</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtSupplierName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
               <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSupplierName" ValidationExpression="[a-zA-Z \.]*$" ErrorMessage="Enter Valid characters" ForeColor="Red" ValidationGroup="btnSave"/>
                <asp:TextBox ID="txtSupplierName" runat="server" CssClass="form-control" placeholder="Supplier Name"></asp:TextBox>
          </div>

           <div class="control-group">
             <label for="exampleInputEmail1"  style="font-weight:bold;display:inline-block">Address1</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtAddress1" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" placeholder="Address1" ></asp:TextBox>
          </div>

          <div class="control-group">
            <label for="exampleInputEmail1"  style="font-weight:bold;display:inline-block">Address2</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator51" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtAddress2" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="Address2" ></asp:TextBox>
          </div>


       <%--    <div class="control-group">
                <label for="exampleInputEmail1"  style="font-weight:bold;">Select Companies</label>
                 <asp:ListBox ID="lstCompanyList" runat="server" SelectionMode="Multiple" CssClass="form-control" Width="50%" >
                 </asp:ListBox>  
                 </div>--%>
         
          <div class="control-group">
            <label for="exampleInputEmail1"  style="font-weight:bold;display:inline-block">Mobile No</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtMobileNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMobileNo"  ValidationGroup="btnSave" ErrorMessage="Mobile no should be in 10 Digits" ForeColor="Red" ValidationExpression="[0-9]{10}" ></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtMobileNo" runat="server" type="number" CssClass="form-control" placeholder="Mobile No" ></asp:TextBox>
          </div>

          <div class="control-group">
            <label for="exampleInputEmail1"  style="font-weight:bold;display:inline-block">Office Contact No</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtOfficeContactNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
              <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtOfficeContactNo"  ValidationGroup="btnSave" ErrorMessage="Contact no should be in 10 Digits" ForeColor="Red" ValidationExpression="[0-9]{10}" ></asp:RegularExpressionValidator>              
                <asp:TextBox ID="txtOfficeContactNo" runat="server"  type="number" CssClass="form-control" placeholder="Office No" ></asp:TextBox>
          </div>

          <div class="control-group">
            <label for="exampleInputEmail1"  style="font-weight:bold;display:inline-block">Category</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ForeColor="Red"  Font-Bold="true" ControlToValidate="lstCategory" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:ListBox ID="lstCategory" runat="server" CssClass="form-control"  SelectionMode="Multiple">
                </asp:ListBox>
          </div>

           <div class="control-group">
             <label for="exampleInputEmail1"  style="font-weight:bold;display:inline-block">Business Registration No</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtBusinesRegNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtBusinesRegNo" runat="server" CssClass="form-control" placeholder="BR No" Text=""></asp:TextBox>
          </div>

          <div class="control-group">
             <label for="exampleInputEmail1"  style="font-weight:bold;display:inline-block">Vat Registration No.</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtVatRegNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtVatRegNo" runat="server" CssClass="form-control input-large" placeholder="Vat Reg Nos"  ></asp:TextBox>
              </div>
          </div>
         <div class="span1"></div>
         <div class="span4">
            <div class="control-group">
              <label for="exampleInputEmail1"  style="font-weight:bold;display:inline-block">Company Type</label>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" CssClass="form-control"  InitialValue="Select Location" Font-Bold="true" ControlToValidate="ddlCompanyType" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="form-control" >
                <asp:ListItem Value="0">Select Company Type</asp:ListItem>
                <asp:ListItem Value="1">Sole Proprietorship</asp:ListItem>
                <asp:ListItem Value="2">Private Company</asp:ListItem>
                <asp:ListItem Value="3">Public Company</asp:ListItem>
                <asp:ListItem Value="4">Electrics</asp:ListItem>
                <asp:ListItem Value="5">Corporation</asp:ListItem>
                </asp:DropDownList>
          </div>

          <div class="control-group">
             <label for="exampleInputEmail1"  style="font-weight:bold;display:inline-block">Nature Of Business</label>
               <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red"  InitialValue="0" Font-Bold="true" ControlToValidate="ddlBusinessCategory" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:DropDownList ID="ddlBusinessCategory" runat="server" CssClass="form-control" >
                </asp:DropDownList>          </div>

          <div class="control-group">
             <asp:Label ID="Label1" runat="server" CssClass="control-label" Font-Bold="true">Supplier Logo (Jpg, Jpeg, Png, Gif)</asp:Label>

                <asp:RegularExpressionValidator ID="regexValidator" runat="server"  ControlToValidate="fileUploadLogo"  ErrorMessage="( Jpg, Jpeg, Png, Gif Only)" ForeColor="Red"  ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpeg|.jpg|.gif)$" ValidationGroup="btnSave">  </asp:RegularExpressionValidator>
                       <div class="input-group margin">
                        <asp:FileUpload runat="server" CssClass="form-control" style="border:solid 1px;padding:2px;border-color:#cccccc;border-radius: 5px;" ID="fileUploadLogo"  onchange="readURL(this);" ></asp:FileUpload>
                     <span class="input-group-btn">
                     <button class="btn btn-info btn-flat" id="clearLogo">Clear</button>
                    </span>
                     </div>

              <div class=" row">
          
                    <div class="control-group" style=" background-color:transparent;">
                    <div class="panel-body" >
                 
                      <asp:Label ID="lblFileUploadError" runat="server"></asp:Label>
                       <img alt="" src="" runat="server" id="imageid" style="margin-left:30px; margin-top:10px;width:200px; height:200px; "   /> 
                    </div>
                </div>
              </div>    
          </div>
                              
           <div class="control-group">
             <label for="exampleInputEmail1"  style="font-weight:bold;">Multiple Files (One time 10 files can be uploaded)</label>
          <label style="font-weight:bold; color:red;" id="MultipleFileError"></label>

                 <div class="input-group margin">
                          <asp:FileUpload runat="server"  ID="fileUploadDocs" CssClass="form-control input-large" style="border:solid 1px;border-radius: 5px;padding:2px;border-color:#cccccc;" AllowMultiple="true" onchange="readFilesURL(this);" />
                     <span class="input-group-btn">
                     <button class="btn btn-info btn-flat" id="clearFiles">Clear</button>
                    </span>
                     </div>


             <div class="row"  style="overflow-y:auto; overflow-x:hidden; max-height:300px;">

                  <table id="tblUpload" class="table table-hover" style="border:none;margin-left:30px;"  border="1">
                 </table>

              </div>   

          </div>

       <asp:ScriptManager runat="server"></asp:ScriptManager>
       <asp:UpdatePanel  runat="server">
           <ContentTemplate>
           <div>
               <asp:GridView runat="server" ID="gvUserDocuments" EmptyDataText="No Files Found" OnPageIndexChanging="gvUserDocuments_OnPageIndexChanged" PageIndex="3" AllowPaging="true" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive ">
             <Columns>
                    <asp:TemplateField   HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
             <ItemTemplate>
                 <asp:Label Text='<%#Eval("ImageId") %>' ID="lblImageId" runat="server"></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
                   <asp:TemplateField  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
             <ItemTemplate>
                 <asp:Label Text='<%#Eval("SupplierImagePath") %>' ID="lblImagePath" runat="server"></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
                   <asp:BoundField DataField="ImageId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                  <asp:BoundField DataField="SupplierImagePath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                 <asp:BoundField DataField="ImageFileName" HeaderText="File Name"/>
                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton runat="server" ForeColor="#0099cc" OnClick="lbtnview_Click" ID="lbtnview">View</asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton runat="server" ForeColor="Red" ID="lbtnDelete" CssClass="deleteUploadFile" >Delete</asp:LinkButton>
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
         <div class="control-group" style="margin-left:60px;">
            <span class="pull-right">
              <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"  ValidationGroup="btnSave" CssClass="btn btn-primary "></asp:Button>
              <asp:Button ID="btnCancel"  runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-danger"></asp:Button>
            </span>
         </div>
         <br />		
		</div>
       
	  </div>
	</form>
   </div>
</div>
</div>
   <script src="AdminResources/js/jquery.min.js"></script>
   <link href="LoginResources/css/bootstrap-multiselect.css" rel="stylesheet" />
   <script src="LoginResources/js/bootstrap-multiselect.js"></script>
   <%-- <link href="LoginResources/css/mullt.min.css" rel="stylesheet" />--%>

     <script type="text/javascript">
    function readURL1(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('<%= imageid.ClientID %>').src = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
         }

        <%-- $(function () {
        
        $('<%= lstCompanyList.ClientID %>').multiselect({
            includeSelectAllOption: true
             });

             $('[id*=ContentPlaceHolder1_lstCompanyList]').multiselect({
            includeSelectAllOption: true
             });

               $('[id*=ContentPlaceHolder1_lstCategory]').multiselect({
            includeSelectAllOption: true
        });

       
        });--%>

</script>

      <script type="text/javascript">

       
        function hideModal()
        {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
            return this.false;
        }
        function hideDeleteModal()
        {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('hide');
            return this.false;
        }
        function showDeleteModal()
        {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('show');
            return this.false;
        }
    </script>


    <script>

        Sys.Application.add_load(function() {

            $(".deleteUploadFile").click(function () {
                var imageId = $(this).parent().prev().prev().prev().prev().prev().prev().children().html();
                var imagePath = $(this).parent().prev().prev().prev().prev().prev().children().html();
                $("#<%=hdnUploadFileId.ClientID%>").val(imageId);
                $("#<%=hdnUploadFilePath.ClientID%>").val(imagePath);
                showDeleteModal();
            });
        });
    </script>


       <script>
        
           $("#clearLogo").click(function () {
               $("#<%=hdnClearLogo.ClientID%>").val(1);
               $("#<%=hdnFileUpdate.ClientID%>").val("");
           });
            
    </script>

    <script>
                  function readURL(input) {

            if (input.files && input.files[0]) {
                   var filePath = input.value;
                   var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;

                if(!allowedExtensions.exec(filePath))
                {

                    $("<%=fileUploadLogo.ClientID%>").remove();
                       $("#<%=fileUploadLogo.ClientID %>").css('border-color', 'red');
                    document.getElementById('<%= imageid.ClientID %>').src = 'LoginResources/images/noPerson.png';
                }
                else
                {
                     var reader = new FileReader();
                     reader.onload = function (e) {
                         document.getElementById('<%= imageid.ClientID %>').src = e.target.result;
                             $("#<%=fileUploadLogo.ClientID %>").css('border-color', '#d2d6de');
                }
                     reader.readAsDataURL(input.files[0]);
                }
                }
        }
            </script>

    <script type="text/javascript">
        $("#<%=btnUpdate.ClientID %>").click(function () {

            var fileInput = document.getElementById("ContentPlaceHolder1_fileUploadLogo");
            var fileInputDocs = document.getElementById("ContentPlaceHolder1_fileUploadDocs");
            
                        if (fileInput.files.length != 0) {
                            var filePath = fileInput.value;
                            var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;

                            if (!allowedExtensions.exec(filePath)) {
                                    $("#<%=fileUploadLogo.ClientID %>").css('border-color', 'red');
                                return false;
                            }
                            
                        }
                        else {
                              $("#<%=fileUploadLogo.ClientID %>").css('border-color', '#d2d6de');

                            if (fileInputDocs.files.length <= 10)
                            {
                                $('#MultipleFileError').text('');
                                return true;
                            }
                            else
                            {
                                $('#MultipleFileError').text('No of Files exceeded than 10');
                                return false;
                            }


                          
                          
                        }
        });
    </script>

 
   <script>
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
