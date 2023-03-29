<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerApproveSupplier.aspx.cs" Inherits="BiddingSystem.CustomerApproveSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
 <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
                <link href="LoginResources/css/bootstrap-multiselect.css" rel="stylesheet" />
                <script src="LoginResources/js/bootstrap-multiselect.js"></script>
<section class="content-header">
      <h1>
       Approve Supplier Request
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Approve Supplier Request</li>
      </ol>
    </section>
    <br />
     <style type="text/css">
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
</style>
<form id="form1" runat="server">


 

<section class="content">

    <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
           </strong>
    </div>

      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Edit Supplier Details</h3>

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
                <label for="exampleInputEmail1">Supplier ID</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtSupplierId" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtSupplierId" runat="server" Enabled="false" CssClass="form-control" placeholder="S00240"></asp:TextBox>
                </div>

                

                 <div class="form-group">
                <label for="exampleInputEmail1">Name</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtSupplierName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtSupplierName" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Address1</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtAddress1" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Address2</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtAddress2" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Email Address</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtEmailAddress" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>
                 <div class="form-group">
                <label for="exampleInputEmail1">User Name</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtEmailAddress" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>
                 <div class="form-group">
                <label for="exampleInputEmail1">Password</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtEmailAddress" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="" ></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Confirm Password</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtEmailAddress" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Office Contact No</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtOfficeContactNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtOfficeContactNo" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Mobile No</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtMobileNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Category</label>
                  <asp:ListBox ID="lstCompanyCategory" runat="server" CssClass="form-control"  SelectionMode="Multiple">
              
                </asp:ListBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Requested Date</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtRequestedDate" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtRequestedDate" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Business Registration No.</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtBusinesRegNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtBusinesRegNo" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>
                </div>
              <!-- /.form-group -->
  
            <!-- /.col -->
            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">Vat Registration No.</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtVatRegNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtVatRegNo" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Company Type</label>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ForeColor="Red"  InitialValue="Select Location" Font-Bold="true" ControlToValidate="ddlCompanyType" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="form-control" >
                <asp:ListItem Value="0">Select Company Type</asp:ListItem>
                <asp:ListItem Value="1">Sole Proprietorship</asp:ListItem>
                <asp:ListItem Value="2">Private Company</asp:ListItem>
                <asp:ListItem Value="3">Public Company</asp:ListItem>
                <asp:ListItem Value="4">Electrics</asp:ListItem>
                <asp:ListItem Value="5">Corporation</asp:ListItem>
                </asp:DropDownList>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Business Category</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator17" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtBusinessCategory" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtBusinessCategory" runat="server" CssClass="form-control" placeholder="Business Category" Text=""></asp:TextBox>
                </div>
               

                   <div class="form-group">
                      <asp:Label ID="Label1" runat="server" CssClass="control-label" Font-Bold="true">Supplier Logo</asp:Label>
               <%--       <asp:FileUpload runat="server" CssClass="form-control" ID="fileUploadLogo" onchange="readURL(this);" ></asp:FileUpload>--%>
                      <div class="col-sm-12 row">

                             <div class="col-sm-6">
                                  <div class="panel" style=" background-color:transparent;">
                                  <div class="panel-body" >
                                <div>
                                    <asp:Label ID="lblFileUploadError" runat="server"></asp:Label>
                                     <img alt="" src="" runat="server" id="imageid" style="margin-top:10px;width:200px; height:200px; "   /> 
                                 </div>
                                  </div>
                                </div>
                          </div>
                      </div>
                    </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Supplier Documents</label>
                <%-- <asp:FileUpload runat="server" ID="fileUploadDocs" CssClass="form-control" AllowMultiple="true"/>--%>
              <%--  <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" placeholder="Business Category" Text=""></asp:TextBox>--%>
                    <div>
<asp:GridView runat="server" ID="gvUserDocuments" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive" EmptyDataText="No Documents Found" HeaderStyle-CssClass="hidden">
    <Columns>
        <asp:BoundField DataField="ImageId" HeaderText="ImageId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
         <asp:BoundField DataField="SupplierImagePath" HeaderText="Path" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
        <asp:BoundField DataField="ImageFileName" HeaderStyle-CssClass="hidden"/>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton runat="server" ID="lbtnview" OnClick="lbtnview_Click">View</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
         <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton runat="server" ID="lbtnDownload" OnClick="lbtnDownload_Click" >Download</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
                    </div>
                </div>
            <!-- /.col -->
          </div>
           </div>



                </div>

        <!-- /.box-body -->

           <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnApprovee" runat="server" Text="Approve" OnClick="btnApprovee_Click"  ValidationGroup="btnSave1" CssClass="btn btn-primary "></asp:Button>
                 <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click"  ValidationGroup="btnSave2" CssClass="btn btn-warning "></asp:Button>
                 <asp:Button ID="btnCancel"  runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-danger"></asp:Button>
               
            </span>
                 </div>
      </div>
      <!-- /.box -->
    </section>
</form>
<script type="text/javascript">
   

     $(function () {
        
               $('[id*=ContentSection_lstCompanyCategory]').multiselect({
            includeSelectAllOption: true
        });

       
    });

     document.getElementById("ContentSection_txtGroupName").disabled = true;

</script>

</asp:Content>

