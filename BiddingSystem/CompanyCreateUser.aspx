<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="CompanyCreateUser.aspx.cs" Inherits="BiddingSystem.CompanyCreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <html>
    <head>
        <style type="text/css">
            .tablegv
            {
                font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
                border-collapse: collapse;
                width: 100%;
            }
            .tablegv td, .tablegv th
            {
                border: 1px solid #ddd;
                padding: 8px;
            }
            .tablegv tr:nth-child(even)
            {
                background-color: #f2f2f2;
            }
            .tablegv tr:hover
            {
                background-color: #ddd;
            }
            .tablegv th
            {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #3C8DBC;
                color: white;
            }
            .successMessage
            {
                color: white;
                font-size: medium;
            }
            
            .failMessage
            {
                color: white;
                font-size: medium;
            }
        </style>
    </head>
    <body>
        <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
        <form runat="server">
        <div id="modalConfirmYesNo" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 id="lblTitleConfirmYesNo" class="modal-title">
                            Confirmation</h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            Are you sure to submit your details ?</p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnYesConfirmYesNo" runat="server" CssClass="btn btn-primary" OnClick="BtnSave_Click"
                            Text="Yes"></asp:Button>
                        <button id="btnNoConfirmYesNo" type="button" class="btn btn-danger">
                            No</button>
                    </div>
                </div>
            </div>
        </div>
        <section class="content-header">
      <h1>
       Create User
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Create User</li>
      </ol>
    </section>
        <section class="content">
    <asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
          <asp:UpdatePanel runat="server" ID="Updatepanel1">
              <ContentTemplate>

                  
                <div id="SuccessAlert" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">Success</h4>
                            </div>
                            <div class="modal-body">
                                <p style="font-weight: bold; font-size: medium;">PR has been created Successfully</p>
                            </div>
                            <div class="modal-footer">
                                <span class="btn btn-info" data-dismiss="modal" aria-label="Close">OK</span>
                                <%--<button id="btnOki" class="btn btn-success">OK</button>--%>
                            </div>
                        </div>
                    </div>
                </div>
                  
        <div id="errorAlert" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #ff0000;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">ERROR</h4>
                            </div>
                            <div class="modal-body">
                                <p id="errorMessage" style="font-weight: bold; font-size: medium;"></p>
                            </div>
                            <div class="modal-footer">
                                <span class="btn btn-danger" data-dismiss="modal" aria-label="Close">OK</span>
                                <%--<button id="btnOki" class="btn btn-success">OK</button>--%>
                            </div>
                        </div>
                    </div>
                </div>



    
   <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
           </strong>
         </div>

      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >User Registration</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>

        <div class="box-body">
          <div class="row">
          <div class="col-md-12">
            <div class="col-md-6">
              

                 <div class="form-group">
                <label for="exampleInputEmail1">Name</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtName" ValidationExpression="[a-zA-Z \.]*$" ErrorMessage="Enter Valid characters" ForeColor="Red" ValidationGroup="btnSave"/>
                   <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Email Address</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtEmilAddress" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                 <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmilAddress" ValidationGroup="btnSave" ForeColor="Red" Font-Bold="true" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                 <asp:TextBox ID="txtEmilAddress" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>

               

                <div class="form-group">
                <label for="exampleInputEmail1">User name</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtusername" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>  
                    <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtusername" ID="RegularExpressionValidator3" ValidationGroup="btnSave" ForeColor="Red" ValidationExpression = "^[\s\S]{4,15}$" runat="server" ErrorMessage="Minimum 4 and Maximum 15 characters required."></asp:RegularExpressionValidator>   
                <asp:TextBox ID="txtusername" type="text" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                <label for="exampleInputEmail1">Employee No</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtEmpNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>  
                    <asp:TextBox ID="txtEmpNo" type="text" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Contatct No</label>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtContactNo" ValidationGroup="btnContactInfo"
                                ErrorMessage="Maximum 10 Digits" ForeColor="Red" ValidationExpression="[0-9]{10}" Display="Dynamic"></asp:RegularExpressionValidator>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtContactNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>  
                    <asp:TextBox ID="txtContactNo" type="number" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                
                </div>
            <div class="col-md-6">
         <%--    <div class="form-group">
                <label for="exampleInputEmail1">User Type</label>
                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddluserType" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>                   
                 <asp:DropDownList ID="ddluserType" runat="server" CssClass="form-control"  >
                     <asp:ListItem Value="" Text="Select User Type" ></asp:ListItem>
                     <asp:ListItem Value="A">Admin</asp:ListItem>
                      <asp:ListItem Value="M">Manager</asp:ListItem>
                       <asp:ListItem Value="U">User</asp:ListItem>
                 </asp:DropDownList>
                </div>--%>
                  <div class="form-group">
                <label for="exampleInputEmail1">Is Active</label>
                 <asp:CheckBox ID="chkIsActive" runat="server" CssClass="form-control" TextMode="Password"></asp:CheckBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">User Role</label>
                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator29"  ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlUserZRole" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                 <asp:DropDownList ID="ddlUserZRole" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="form-group">
                <label for="lblDesigntion">Designation</label>
                
               <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlDesignation" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                         <div class="input-group margin">
                          <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" 
                        onselectedindexchanged="ddlDesignation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <span class="input-group-btn">
                         <asp:Button runat="server" ID="btnCreateNewDesignation"   CssClass="btn btn-info"  
                                 Text="Create New Designation" onclick="btnCreateNewDesignation_Click"/>
                                  <%--<asp:Button runat="server" ID="btnEditDesignation"   CssClass="btn btn-info"  
                                 Text="Edit Designation" onclick="btnEditDesignation_Click" Enabled="false" />--%>
                    </span>
              </div>
       
                </div>
              
          
             <div class="form-group">
                <label for="exampleInputEmail1">Password</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtpassword" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                 <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtpassword" ID="RegularExpressionValidator1" ValidationGroup="btnSave" ForeColor="Red" ValidationExpression = "^[\s\S]{4,}$" runat="server" ErrorMessage="Minimum 4 characters required"></asp:RegularExpressionValidator>
                      <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" placeholder=""  TextMode="Password"></asp:TextBox>
                </div>
                
                 <div class="form-group">
                <label for="exampleInputEmail1">Confirm Password</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtConfirmPassword" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:CompareValidator runat="server" ID="CompareValidator2" ForeColor="Red" Font-Bold="true" ControlToCompare="txtpassword"  ControlToValidate="txtConfirmPassword"  ValidationGroup="btnSave">Password doesn't match</asp:CompareValidator>     
                     <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control"  TextMode="Password"></asp:TextBox>
                </div>
                
          </div>
             <%--   <div id="dvDesignation" class="col-md-12" runat="server" visible="false">
                    <div class="col-md-12">
                        <div class="form-group">
                        <label for="lbl1">Designation Name</label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtDesignationName" ValidationGroup="btnSaveDesignation">*</asp:RequiredFieldValidator>     
                           
                            <asp:TextBox ID="txtDesignationName" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                    <div class="box-footer">
                        <span class="pull-right">
                            <asp:Button ID="btnSaveDesignation" runat="server" Text="Save Designation"  
                            ValidationGroup="btnSaveDesignation" CssClass="btn btn-primary" 
                            onclick="btnSaveDesignation_Click" Visible="true"></asp:Button>
                               <asp:Button ID="btnUpdateDesignation" runat="server" Text="Update Designation"  
                            ValidationGroup="btnUpdateDesignation" CssClass="btn btn-primary" 
                            onclick="btnUpdateDesignation_Click" Visible="false"></asp:Button>
                            <asp:Button ID="btnCancelDesignation"  runat="server" Text="Cancel"  
                            CssClass="btn btn-danger" onclick="btnCancelDesignation_Click" ></asp:Button>
                        </span>
                    </div>
                    </div>
                </div>--%>
            </div>
            </div>




            
             <div class="row">
                                        <div class="col-md-6">
                                            <label for="exampleInputEmail1">Department Information</label>
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvdepartments" runat="server" CssClass="table table-responsive TestTable"
                                                   Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" GridLines="None">
                                                    <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                      <ItemTemplate>
                                                      <asp:CheckBox runat="server" ID="chkDepartments"/>
                                                      </ItemTemplate>
                                                      </asp:TemplateField>
                                                     <asp:BoundField DataField="SubDepartmentId" HeaderText="SubDepartmentId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"  />
                                                    <asp:BoundField DataField="SubDepartmentName" HeaderText="Deparment" />
                                                    <asp:TemplateField HeaderText="Is Head" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerHeaderText">
                                                       <ItemTemplate>
                                                       <asp:CheckBox runat="server" ID="chkIsHead"/>
                                                       </ItemTemplate>
                                                       </asp:TemplateField>
                                                </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                  <div class="col-md-6">
                                           <label for="exampleInputEmail1">Warehouse Information</label>
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvwarehouse" runat="server" CssClass="table table-responsive TestTable"
                                                   Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" GridLines="None" >
                                                    <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                      <ItemTemplate>
                                                      <asp:CheckBox runat="server" ID="chkwarehouse"/>
                                                      </ItemTemplate>
                                                      </asp:TemplateField>
                                                    <asp:BoundField DataField="WarehouseId" HeaderText="WarehouseId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="Location" HeaderText="Warehouse" />

                                                        <asp:TemplateField HeaderText="User Type" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerHeaderText">
                                                       <ItemTemplate>
                                                      <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control" > 
                                                         <asp:ListItem Value="1">Head of Warehouse</asp:ListItem>  
                                                         <asp:ListItem Value="2">Store Keeper</asp:ListItem>  
                                                       
                                                     </asp:DropDownList>
                                                       </ItemTemplate>
                                                       </asp:TemplateField>


                                                </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
             </div>





           </div>
        
        </div>
       

        <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Submit"  ValidationGroup="btnSave" CssClass="btn btn-primary" OnClick="BtnSave_Click" ></asp:Button>
                 <asp:Button ID="btnCancel"  runat="server" Text="Clear"  CssClass="btn btn-danger" OnClick="btnCancel_Click"></asp:Button>
               
            </span>
                 </div>
                
             </div>
    <div id="myModalAddDesignation" class="modal modal-primary fade in" tabindex="-1" role="dialog" aria-hidden="false">
                                <div class="modal-dialog">
                                    <!-- Modal content-->
                                    <div class="modal-content" style="background-color: #a2bdcc;">
                                        <div class="modal-header" style="background-color: #7bd47dfa;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="False">×</span></button>
                                            <h4 class="modal-title">Create Designation</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="login-3lsw">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                       
                    <div class="col-md-12">
                        <div class="form-group">
                        <label for="lbl1" style="color:Black;">Designation Name</label>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtDesignationName" ValidationGroup="btnSaveDesignation">*</asp:RequiredFieldValidator>     
                           
                            <asp:TextBox ID="txtDesignationName" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
              
                        <span class="pull-right">

                            <asp:Button ID="btnSaveDesignation" runat="server" Text="Save Designation"  OnClientClick ="RemoveBackdrop();"
                            ValidationGroup="btnSaveDesignation" CssClass="btn btn-primary" 
                            onclick="btnSaveDesignation_Click" Visible="true" ></asp:Button>

                               <asp:Button ID="btnUpdateDesignation" runat="server" Text="Update Designation"  OnClientClick ="RemoveBackdrop();"
                            ValidationGroup="btnUpdateDesignation" CssClass="btn btn-primary" 
                            onclick="btnUpdateDesignation_Click" Visible="false" ></asp:Button>
                  
                        </span>
               
                    </div>

                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                  </ContentTemplate>
              <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnCancel" />
            </Triggers>
              </asp:UpdatePanel>
   </section>
        </form>
        <script type="text/javascript">

        $(function () {

            $('[id*=ContentSection_ddlcategory]').multiselect({
                includeSelectAllOption: true
            
            });
           });
       
        $("#btnNoConfirmYesNo").on('click').click(function () {
                     var $confirm = $("#modalConfirmYesNo");
                     $confirm.modal('hide');
                     return this.false;
        });

       function RemoveBackdrop() {
            
            $('#myModalAddDesignation').hide();
            $('.modal-backdrop').remove();
            $('body').css("overflow", "auto");
            $('body').css("padding-right", "0");
        }
        </script>
    </body>
    </html>
</asp:Content>
