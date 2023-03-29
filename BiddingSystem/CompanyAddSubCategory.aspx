<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyAddSubCategory.aspx.cs" Inherits="BiddingSystem.CompanyAddSubCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <section class="content-header">
      <h1>
       Add Sub Category
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Add Sub Category</li>
      </ol>
    </section>
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

            .tablegv tr:nth-child(even) {
                background-color: #f2f2f2;
            }
            /*.tablegv tr:hover {background-color: #ddd;}*/
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

        .bottom_margin {
        margin-bottom: 10px;
}
    </style>



    <form id="form1" runat="server">


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
                        <p>Are you sure you want to deactivate this record? </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" OnClick="lnkBtnDelete_Click" Text="Yes"></asp:Button>
                        <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger">No</button>
                    </div>
                </div>
            </div>
        </div>



        <section class="content">

        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <strong>
                                <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
                            </strong>
                        </div>


    
          <div class="box box-info" style="display:none">
        <div class="box-header with-border">
          <h3 class="box-title" >Search From Master Sub Category</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">

            <div class="col-md-6">

                  <div class="form-group">
                <label for="exampleInputEmail1">Select Category</label> 
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlItemMasterCategory" InitialValue="" ValidationGroup="ddlItemMasterCategory" ID="RequiredFieldValidator4" ForeColor="Red" >*</asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlItemMasterCategory" runat="server" CssClass="form-control" CausesValidation="true" >
                </asp:DropDownList>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Sub Category Name</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFindCategortName" ValidationGroup="ddlItemMasterCategory" ForeColor="Red">*</asp:RequiredFieldValidator>
                 
                         <div class="input-group">
                      <asp:TextBox ID="txtFindCategortName" runat="server" style="display:inline-block;" CssClass="form-control" autocomplete="off"></asp:TextBox>
                    <span class="input-group-btn">
                      <asp:Button ID="btnSearch" runat="server" Text="Search" style="display:inline-block;" CssClass="btn btn-primary" ValidationGroup="ddlItemMasterCategory" OnClick="btnSearch_Click"></asp:Button> 
                    </span>
              </div>
                </div>
                </div>
          </div>
         
        </div>
        

              <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gvMasterSubCategoryList" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" OnPageIndexChanging="gvMasterSubCategoryList_PageIndexChanging" PageSize="10" AllowPaging="true" EmptyDataText="No Records Found">
        <Columns>
              
            <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="SubCategoryName" HeaderText="Sub Category Name" />
             <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
     
             
           <%--     <asp:TemplateField HeaderText="Status">
                  <ItemTemplate>
                      <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("companyId") == null ? "Available" : "Taken" %>'  />
                  </ItemTemplate>
                </asp:TemplateField>--%>
             <asp:TemplateField >
                  <ItemTemplate>
                      <asp:LinkButton ID="btnTake"  Text="Select" OnClick="btnTake_Click1" runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>





      </div>






    <asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
          <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true" UpdateMode="Conditional">
              <ContentTemplate>

      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Company Sub Category </h3>

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
                <label for="exampleInputEmail1">Select Main Category</label> 
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMainCategory" InitialValue="" ValidationGroup="btnSave" ID="RequiredFieldValidator2" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlMainCategory" runat="server" CssClass="form-control" CausesValidation="true" >
                </asp:DropDownList>
                </div>
            </div>

            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">Sub Category Name</label>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSubCategoryName"  ValidationGroup="btnSave" ID="RequiredFieldValidator3" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:TextBox ID="txtSubCategoryName" runat="server" CssClass="form-control" CausesValidation="true"></asp:TextBox>
                </div>

                <div class="col-md-6">
                <div class="form-group">
                <label for="exampleInputEmail1">Is Active</label>
                  <asp:CheckBox ID="chkIsavtive" runat="server" Checked></asp:CheckBox>                
                </div>
            </div>
            </div>
         
          </div>
         
        </div>
          

        <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Save" 
                CssClass="btn btn-primary" ValidationGroup="btnSave" onclick="btnSave_Click"></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Clear"  
                CssClass="btn btn-danger" onclick="btnClear_Click"></asp:Button>
                </span>
              </div>

          </div>
             </ContentTemplate>
              <Triggers>
                  <asp:PostBackTrigger ControlID="btnSave" />
              </Triggers>
              </asp:UpdatePanel>    
        <!-- /.box-body -->
      
      <!-- /.box -->
    </section>

    <div class="panel-body">
         <div class="box box-info">
        <div class="box-header with-border">
              <h3 class="box-title" >Sub Category Details </h3>
            <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
              <div class="box-body">
                  <div class="row">
                  <div class="col-md-12">
                  <div class="box-tools pull-right bottom_margin">
                                            <div class="input-group input-group-sm" style="width: 150px;">
                                                
                                                <asp:TextBox ID="txtSearch" runat="server" placeholder="Search Sub Category" height="30px"></asp:TextBox>
                                                <div class="input-group-btn">
                                                    <%--<button type="submit" class="btn btn-default pull-right"><i
                                                            class="fa fa-search" onserverclick="search_click"></i></button>--%>
                                                    <button class="btn btn-default pull-right" runat="server" 
                                                    onserverclick="search_click"><b><i class="fa fa-search"></i></button>
                                                </div>
                                            </div>
                                        </div>
                        </div>
                                        </div>
          <div class="co-md-12">
              
        <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gvSubCategory" runat="server" CssClass="table table-responsive tablegv" 
           GridLines="None" AutoGenerateColumns="false" OnPageIndexChanging="gvSubCategory_PageIndexChanging1" AllowPaging="true" PageSize="10">
        <Columns>
              <asp:TemplateField  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
             <ItemTemplate>
                 <asp:Label Text='<%#Eval("SubCategoryId")%>' runat="server"></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
            <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CategoryName" HeaderText="Main Category Name" />
            <asp:BoundField DataField="SubCategoryName" HeaderText="Sub Category Name" />
            <asp:BoundField DataField="CreatedDateTime" HeaderText="Created Date & Time" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="UpdatedDate" HeaderText="UpdatedDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="UpdatedBy" HeaderText="UpdatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
             <asp:BoundField DataField="IsActive"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
         <asp:TemplateField HeaderText="Active">
             <ItemTemplate>
                 <asp:Label Text='<%#Eval("IsActive").ToString()== "1"?"Yes":"No" %>' Font-Bold="true" ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
         
            <%--  <asp:TemplateField HeaderText="Edit">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnEdit" ToolTip="Edit" ImageUrl="~/images/document.png" OnClick="lnkBtnEdit_Click" style="width:26px;height:20px"
                          runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>--%>

           

                <asp:TemplateField HeaderText="Delete">
                  <ItemTemplate>
                      <asp:LinkButton ID="btnCancelRequest"  Text='<%#Eval("IsActive").ToString()== "1"?"Deactivate":"Activate" %>'  CssClass="deleteSubCategory" runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
              </div>
                  </div>
          
             </div>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField  runat="server" ID="hdnSubCategoryId"/>
     <asp:HiddenField  runat="server" ID="hdnCategoryid"/>
     <asp:HiddenField  runat="server" ID="hdnStatus"/>
    <asp:Button ID="hbtnSearch" runat="server" OnClick="search_click" CssClass="hidden" />
    </form>
    <script src="AdminResources/js/autoCompleter.js"></script>
    <script type="text/javascript">

        var subCategories = <%= getJsonSubCategoryList() %>;
        autocomplete(document.getElementById('ContentSection_txtSubCategoryName'), subCategories);
        
        $("#btnNoConfirmYesNo").on('click').click(function () {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
            return this.false;
        });
        
    </script>

    <script type="text/javascript">

         

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
            event.preventDefault();
            return this.false;
        }
    </script>

    <script>



        // Sys.Application.add_load(function() {

        $(".deleteSubCategory").click(function () {
           // debugger;
            var subcategoryId = $(this).closest('tr').find('td:first-child').text().trim();
            var categoryId =  $(this).closest('tr').find('td:nth-child(2)').text().trim();
            var status = $(this).parent().prev().children().html();
                
            $("#<%=hdnSubCategoryId.ClientID%>").val(subcategoryId);
                $("#<%=hdnCategoryid.ClientID%>").val(categoryId);
                $("#<%=hdnStatus.ClientID%>").val(status);
                showDeleteModal();
            });
            //});

              
    </script>
</asp:Content>
