<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="EditMRN.aspx.cs" Inherits="BiddingSystem.EditMRN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
     #myModal .modal-dialog {
       width: 90%;
     }
     #myModalViewBom .modal-dialog {
       width: 50%;
     }
     #myModal2 .modal-dialog {
       width: 50%;
     }
     #myModalUploadedPhotos .modal-dialog{
         width: 50% 
     }
     #myModalReplacementPhotos .modal-dialog{
         width: 50% 
     }
</style>
    <style type="text/css">
    .form{
	margin: 20px 0;
    }
    form input, button{
	padding: 6px;
    font-size: 18px;
    }
    .tableCol{
        width: 100%;
        margin-bottom: 20px;
		border-collapse: collapse;
		background: #fff;
    }
    .tableCol, .thCol, .tdCol{
        border: 1px solid #cdcdcd;
        color:Black;
    }
    .tableCol .thCol, .tableCol .tdCol{
        padding: 10px;
        text-align: left;
        color:Black;
    }
	body {
		background: #ccc;
	}
	.add-row, .delete-row {
	font-size: 16px;
    font-weight: 600;
    padding: -1px 16px;
	}
</style>
    <style type="text/css">
     .TestTable {
         font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
         border-collapse: collapse;
         width: 100%;
     }
     
     .TestTable td, #TestTable th {
         border: 1px solid #ddd;
         padding: 8px;
     }
     
     .TestTable tr:nth-child(even){background-color: #f2f2f2;}
     
     .TestTable tr:hover {background-color: #ddd;}
     
     .TestTable th {
         padding-top: 12px;
         padding-bottom: 12px;
         text-align: left;
         background-color: #4CAF50;
         color: white;
     }
  </style>
    <style type="text/css">
     #myModal .modal-dialog {
       width: 60%;
     }
     #myModal2 .modal-dialog {
       width: 70%;
     }
     #myModalViewBom .modal-dialog {
       width: 50%;
     }
     #myModalUploadedPhotos .modal-dialog{
         width: 50% 
     }
     #myModalReplacementPhotos .modal-dialog{
         width: 50% 
     }
  </style>
    <style type="text/css">
      input[type="file"] {
         display: block;
       }
       .imageThumb {
         max-height: 75px;
         border: 2px solid;
         padding: 1px;
         cursor: pointer;
       }
       .pip {
         display: inline-block;
         margin: 10px 10px 0 0;
       }
       .remove {
         display: block;
         background: #fff;
         border: 1px solid #fff;
         color: red;
         text-align: center;
         cursor: pointer;
       }
       .remove:hover {
         background: white;
         color: black;
       }
  </style>
    <style type="text/css">
     #myModal .modal-dialog {
       width: 90%;
     }
     #myModal2 .modal-dialog {
       width: 50%;
     }
     #myModalViewBom .modal-dialog {
       width: 50%;
     }
     
     #myModalUploadedPhotos .modal-dialog{
         width: 50% 
     }
     #myModalReplacementPhotos .modal-dialog{
         width: 50% 
     }
</style>
    <style type="text/css">
   #myTable {
       font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
       border-collapse: collapse;
       width: 100%;
   }
   
   #myTable td, #customers th {
       border: 1px solid #ddd;
       padding: 8px;
   }
   
   #myTable tr:nth-child(even){background-color: #f2f2f2;}
   
   #myTable tr:hover {background-color: #ddd;}
   
   #myTable th {
       padding-top: 12px;
       padding-bottom: 12px;
       text-align: left;
       background-color: #4CAF50;
       color: white;
   }
</style>
    <style type="text/css">
     #TestTable {
         font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
         border-collapse: collapse;
         width: 100%;
     }
     
     #TestTable td, #TestTable th {
         border: 1px solid #ddd;
         padding: 8px;
     }
     
     #TestTable tr:nth-child(even){background-color: #f2f2f2;}
     
     #TestTable tr:hover {background-color: #ddd;}
     
     #TestTable th {
         padding-top: 12px;
         padding-bottom: 12px;
         text-align: left;
         background-color: #4CAF50;
         color: white;
     }
</style>
    <style type="text/css">
        input[type="file"]
        {
            display: block;
        }
        .imageThumb
        {
            max-height: 75px;
            border: 2px solid;
            margin: 10px 10px 0 0;
            padding: 1px;
        }
   </style>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
  <!-- bootstrap datepicker -->
   <section class="content-header">
      <h1>
       Material Request 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Material Request</li>
      </ol>
    </section> 
    <br />
    
    <form id="form1" runat="server" enctype="multipart/form-data">
    <body onload="viewBOM();">
    <div class="modal modal-primary fade" id="myModal">
          <div class="modal-dialog">
            <div class="modal-content" style="background-color:White;">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" style="text-align:center;">Item Specifications</h4>  <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label>
              </div>
              <div class="modal-body">
              <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                      <div class="row">
                      <div class="col-md-4">
                          <div class="form-group">
                          <label for="exampleInputEmail1" style="color:Black;">Material</label> 
                          <input type="text" id="meterial" placeholder="Material" class="form-control" autocomplete="off" />
                          </div>
                      </div>
                       <div class="col-md-4">
                          <div class="form-group">
                          <label for="exampleInputEmail1" style="color:Black;">Description</label> 
                           <input type="text" id="description" placeholder="Description" class="form-control" autocomplete="off"/>
                          </div>
                      </div>
                      <div class="col-md-4" style=" margin-left: -120px; margin-top: 30px; ">
                          <div class="form-group">
                          <label for="exampleInputEmail1" style="visibility:hidden;">Select Main Category</label> 
                             <asp:HyperLink ID="button" href="#" runat="server" Visible="true" CssClass="add-row" style="color:Red;">
                        <img src="images/PlusSign.gif" border="0" style="margin-right:4px;vertical-align: middle;" alt="Add Row" />Add Row</asp:HyperLink>
                          </div>
                      </div>
                      </div>
                     <div class="table-responsive">
                      <div class="form">
                    </div>
                       <table class="tableCol" id="tableCols">
                           <thead>
                               <tr>
                                   <th class="thCol">Action</th>
                                   <th class="thCol">Material</th>
                                   <th class="thCol">Description</th>
                               </tr> 
                           </thead>
                           <tbody class="tbodyCol" id="tbodyCol">
                              
                           </tbody>
 
                       </table>
                       <asp:HyperLink ID="HyperLink1" href="#" runat="server" Visible="true" CssClass="delete-row" style="color:Red;">
                        <img src="images/dlt.png" border="0" style="margin-right:4px;vertical-align: middle;width: 20px;margin-top: -4px;" alt="Delete Row" />Delete Row</asp:HyperLink>
                   <br />
                        <div class="col-md-12">
                            <div class="col-md-6">
                             <!-- hyper lin button for add row in above table which call the javascript function create above-->
                   <div style="margin-top: 5px;">
                       
                    </div>
                       </div>
                    </div>
       
        
                 </div>
                </div>
                <div class="col-md-2"></div>
              </div>
                
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Close</button>
              <%--  <button type="button" class="btn btn-outline">Submit</button>--%>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>

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
                 <asp:Button ID="btnYesConfirmYesNo" runat="server"  CssClass="btn btn-primary"  OnClick="btnSavePR_Click" Text="Yes" OnClientClick="return scrollToTop();" ></asp:Button>
                <button id="btnNoConfirmYesNo"  type="button" class="btn btn-danger" >No</button>
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

     <asp:ScriptManager runat="server" ID="sm">
     </asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
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
                                <p style="font-weight: bold; font-size: medium;">MRN has been created Successfully</p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnOK" runat="server"  CssClass="btn btn-info"  OnClick="btnOK_Click1" Text="OK"></asp:Button>
                                <%--<button id="btnOki" class="btn btn-success">OK</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

                
                


                <div id="myModalViewBom" class="modal modal-primary fade in" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">View Bill Of Materials</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvTempBoms" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Specifications Found" Style="border-collapse: collapse; color: black;"
                                                    GridLines="None" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="PrId" HeaderText="Pr Id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="ItemId" HeaderText="Item Id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="SeqNo" HeaderText="Seq_ID" />
                                                        <asp:BoundField DataField="Meterial" HeaderText="Material" />
                                                        <asp:BoundField DataField="Description" HeaderText="Description" />

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div>
                                            <label id="lbMailMessage1" style="margin: 3px; color: maroon; text-align: center;"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>




                <div id="myModalUploadedPhotos" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Item Photos</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvUploadedPhotos" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>' Height="80px" Width="100px" />
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
                    </div>
                </div>








                <div id="myModalReplacementPhotos" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Replacement Item Photos</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvReplacementPhotos" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>' Height="80px" Width="100px" />
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
                    </div>
                </div>

                <section class="content">

     <div class="row" style=" visible="false">
        <div class="col-sm-12">
           <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
           </strong>
    </div>
        </div>
    </div>

     <div class="box box-primary" id="topPanel">
        <div class="box-header with-border">
          <h3 class="box-title" >Basic Information</h3>

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
                    <label for="exampleInputEmail1">Expected Date</label>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="dtExpectedDate" ValidationGroup="btnSavePR1">*</asp:RequiredFieldValidator>     
                  <asp:TextBox ID="dtExpectedDate" runat="server"  CssClass="form-control date1" autocomplete="off" DataFormatString="{0:dd-MM-yyyy}" ></asp:TextBox>
                </div>
                 <div class="form-group">
                    <label for="exampleInputEmail1">Description</label><label id="lblRequestBy" style="color:red;"></label>
                   <asp:TextBox ID="txtMrnDescription" runat="server" TextMode="MultiLine"  CssClass="form-control"></asp:TextBox>
                </div>
              <!-- /.form-group -->
            </div>
            <!-- /.col -->
          </div>
          <!-- /.row -->
        </div>
        <!-- /.box-body -->
      </div>
      
     <div class="box box-danger" id="AddItemsDiv">
        <div class="box-header with-border">
          <h3 class="box-title" >Add Items</h3>
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
                    <label for="exampleInputEmail1">Main Category</label>
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlMainCateGory" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
                    <asp:DropDownList ID="ddlMainCateGory" runat="server" CssClass="form-control"  
                        AutoPostBack="true" 
                        onselectedindexchanged="ddlMainCateGory_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="exampleInputEmail1">Sub Category</label>
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlSubCategory" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
                    <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged"></asp:DropDownList>
                </div>
                 <div class="form-group">
                    <label for="exampleInputEmail1">Item Name</label>
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlItemName" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>    
                      <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control" ></asp:DropDownList>

                     
                      <%--<asp:TextBox ID="ddlItemName" CssClass="form-control input-md" AutoComplete="off" runat="server" onkeyup="SearchItemName()"/>--%>
                </div>
                 <div class="form-group">
                    <label for="exampleInputEmail1">Item Quantity</label>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtQty" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" placeholder="" type="number" min="1"></asp:TextBox>
                

<%--                   <div  style="display:inline-block;width:35%;"  >
                    <label for="exampleInputEmail1"></label>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlMeasurement" InitialValue="0" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
                    <asp:DropDownList ID="ddlMeasurement" runat="server" CssClass="form-control" ></asp:DropDownList>
                </div>--%>
                       </div>


                 <div class="form-group">
                    <label for="exampleInputEmail1">Item Description</label>
                     <asp:TextBox ID="txtMRNDDescription" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                </div>
                 
              <!-- /.form-group -->
            </div>

          </div>
        </div>

        <!-- /.box-body -->
        <div class="box-footer">
        <div class="form-group">
              <label for="exampleInputEmail1" style="visibility:hidden">Category Name</label>
               <asp:Button ID="btnClear" ValidationGroup="btnAdd" runat="server" Text="Clear" 
                  CssClass="btn btn-danger pull-right" onclick="btnClear_Click"/>
              <asp:Button ID="btnAdd" ValidationGroup="btnAdd" runat="server" Text="Add Item" 
                  CssClass="btn btn-primary pull-right" onclick="btnAdd_Click" style="margin-right:10px"/>
              <asp:Label runat="server" ID="lblAlertMsg" Text="" CssClass="pull-right" style="margin-right:20px;margin-top: 8px;color:  red;font-weight: bold;" ></asp:Label>
          </div>
        </div>
      </div>

     </section>

                <section class="content">
      <div class="box box-primary">
        <div class="box-header with-border">
          <h3 class="box-title" >Item Details</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
         <div class="row">
         <div class="col-md-12">
                            <asp:GridView ID="gvDatataTable" runat="server" EmptyDataText="No Items Found" AutoGenerateColumns="false" CssClass="table table-responsive table-striped"
                                GridLines="None" OnRowDeleting="gvDatataTable_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="MrndID" HeaderText="MrndID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="MainCategoryId" HeaderText="MainCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="MainCategoryName" HeaderText="Category Name" />
                                    <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="SubcategoryName" HeaderText="Sub Category Name" />
                                    <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                    <asp:BoundField DataField="ItemQuantity" HeaderText="Quantity" />
                                    <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                                   
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEditItem" ImageUrl="~/images/document.png" OnClick="btnEditItem_Click" OnClientClick="return scrollToEdit()" runat="server" style="width:25px;" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDeleteItem" ImageUrl="~/images/delete.png"  CssClass="deleteItem" OnClick="btnDeleteItem_Click1" runat="server" style="width:25px;"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
        </div>
         <div class="box-footer">
        <div class="form-group">
              <label for="exampleInputEmail1" style="visibility:hidden">Category Name</label>
              <asp:Button ID="btnSavePR"  runat="server" Text="Update Material Request"  ValidationGroup="btnSavePR1"  CssClass="btn btn-primary pull-right" OnClick="btnSavePR_Click"></asp:Button>
          </div>
        </div>
        </div>
        </div>
     </section>
                <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdd" />
                <asp:PostBackTrigger ControlID="btnClear" />
            </Triggers>
        </asp:UpdatePanel>
     <asp:HiddenField ID="hdnReqDate" runat="server"></asp:HiddenField>
     <asp:HiddenField ID="hdnItemId" runat="server"></asp:HiddenField>
     <asp:HiddenField ID="hdnField"  runat="server"></asp:HiddenField>
     <asp:HiddenField ID="HiddenField1"  runat="server"></asp:HiddenField>
    
     </body>
    </form>
   <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
   <script src="AdminResources/js/autoCompleter.js"></script>

    <script type="text/javascript">
        Sys.Application.add_load(function () {

            $(function () {
                $("#<%= dtExpectedDate.ClientID %>").datepicker();
            });
        });

    </script>
    <script type="text/javascript">

        $(function () {
            $('#<%=txtQty.ClientID%>').keypress(function (e) {
                if (e.which != 69 && e.which != 101 && e.which != 45 && e.which != 43 && e.which != 42) {
                } else {
                    return false;
                }
            });
        });


        /*function showBOMModal()
        {
            document.getElementById('myModalViewBom').style.display = "block";
        }

        function showUploadedPhotosModal() {
            document.getElementById('myModalUploadedPhotos').style.display = "block";
        }

        function showReplacementPhotosModal() {
            document.getElementById('myModalReplacementPhotos').style.display = "block";
        }*/

        function scrollToEdit()
        {
            document.getElementById("AddItemsDiv").scrollIntoView();
            return true;
        }

        function scrollToTop() {
            ScrollPageToUp();
            return true;
        }

        function SearchItemName() {
            $("#<% =ddlItemName.ClientID%>").autocomplete({
                minLength: 1,
                appendTo: "#container",
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "CompanyPurchaseRequestNote.aspx/LoadItemNames",
                        data: "{input:'" + request.term + "'}",
                        dataType: "json",
                        success: function (output) {
                            response(output.d);
                        },
                        error: function (errormsg) {
                            alert("Invalid Name");
                        }
                    });
                }
            });
        }
    </script>

    <script type="text/javascript">
        function insRow() {
            var tbl = document.getElementById('myTable');
            var len = tbl.rows.length;
            var row = tbl.insertRow(len);
            for (var i = 0; i < tbl.rows[0].cells.length - 1; i++) {
                row.insertCell(i).innerHTML = "<input type=text style=color:Black;>";
            }
            row.insertCell(tbl.rows[0].cells.length - 1).innerHTML = '<input type="button" onclick="delRow(this)" value="Delete Row" style=" background-color: red; ">';
        }

        function delRow(obj) {
            var row = obj;
            while (row.nodeName.toLowerCase() != 'tr') {
                row = row.parentNode;
            }
            var tbl = document.getElementById('myTable');
            tbl.deleteRow(row.rowIndex);

        }
	</script>

   <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
   <script src="AdminResources/js/datetimepicker/datetimepicker.js"></script>
   <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
   <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
   <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />
   <script src="AdminResources/js/daterangepicker.js" type="text/javascript"></script>

   <script type="text/javascript">



        $("#btnNoConfirmYesNo").on('click').click(function () {
                     var $confirm = $("#modalConfirmYesNo");
                     $confirm.modal('hide');
                     return this.false;
        });


        
            
       
           
      
    </script>

   <script type="text/javascript">
        window.onscroll = function () { scrollFunction() };
        function scrollFunction() {
            if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
                document.getElementById("ContentSection_btnSavePR").style.display = "block";
            } else {
                document.getElementById("ContentSection_btnSavePR").style.display = "none";
            }
        }
        function topFunction() {
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
        }
   </script>

   <%-- <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentSection_rdoEnable').change(function () {
                document.getElementById("fileReplace").disabled = false;
            });
            $('#ContentSection_rdoDisable').change(function () {
                document.getElementById("fileReplace").disabled = true;
            });
        });
    </script>--%>

    <script type="text/javascript">
        $('#btnOki').click(function () {
           // debugger;
         var $confirm = $("#SuccessAlert");
         $confirm.modal('hide');
         return this.false;
         event.preventdefault();
     });

 </script>
   
    <script type="text/javascript">
        Sys.Application.add_load(function () {
           $(".deleteItem").click(function () {
               var itemId = $(this).parent().prev().prev().prev().prev().prev().children().html();
               $("#<%=hdnItemId.ClientID%>").val(itemId);
               document.body.scrollTop = 0;
               document.documentElement.scrollTop = 0;
           });
       });
    </script>

    <script type="text/javascript">
            $(document).ready(function () {
                $(".add-row").click(function () {
                    $("#meterial").css('background-color', '#ffffff');
                    $("#description").css('background-color', '#ffffff');
                    var meterial = $("#meterial").val();
                    var description = $("#description").val();
                    if (meterial != "" && description != "")
                    {
                        var markup = "<tr><td class='tdCol'><input type='checkbox' name='record'></td><td class='tdCol'>" + meterial + "</td><td class='tdCol'>" + description + "</td></tr>";
                        $(".tableCol .tbodyCol").append(markup);
                        $("#meterial").val("");
                        $("#description").val("");
                    }
                    else
                    {
                        meterial == "" ? $("#meterial").css('background-color', '#f5cece') : $("#meterial").css('background-color', '#ffffff');
                        description == "" ? $("#description").css('background-color', '#f5cece') : $("#description").css('background-color', '#ffffff');
                    }
                    var rowCount = $('#tableCols tr').length - 1;
                    $("#itemCount").text(rowCount);
                   
                });

                // Find and remove selected table rows
                $(".delete-row").click(function () {
                    $(".tableCol .tbodyCol").find('input[name="record"]').each(function () {
                        if ($(this).is(":checked")) {
                            $(this).parents("tr").remove();
                        }
                    });
                    var rowCount = $('#tableCols tr').length - 1;
                    $("#itemCount").text(rowCount);
                });
            });    
    </script>

    <script type="text/javascript">
            function LoadData(){
                var DataList = $("#ContentSection_HiddenField2").val();
                var parsedTest = JSON.parse(DataList);

                var text = "";
                var field = "";
                for (var i = 1; i <= parsedTest.length; i++) {
                    field = parsedTest[i - 1].split('-');
                    var htmlcode =
                ' <tr> ' +
                ' <td class=tdCol> ' +
                ' <input type=checkbox name=record> ' +
                ' </td> ' +
                ' <td class=tdCol>' + field[0] + '</td> ' +
                ' <td class=tdCol>' + field[1] + '</td> ' +
                ' </tr> ' + ''
                    text += htmlcode;
                }
                document.getElementById("tbodyCol").innerHTML = text;
            }
    </script>

    <script type="text/javascript">
         function GetSelectedRow(lnk) {
         
             $("#ContentSection_HiddenField1").val("1");
             var row = lnk.parentNode.parentNode;
             var rowIndex = row.rowIndex - 1;
             var prid = row.cells[1].innerText;
             var itemid = row.cells[7].innerText;
             var ratus = prid + "-" + itemid;
             var jsonText = JSON.stringify({ data: ratus });
             var text = "";
             $.ajax({
                 type: "POST",
                 url: "CustomerPREdit.aspx/GetPRBomDetailsIds?data=" + ratus,
                 data: jsonText,
                 contentType: "application/json",
                 dataType: "json",
                 success: function (msg) {
                     if (msg.d.length > 0) {
                         for (var i = 0; i < msg.d.length; i++) {
                             var htmlcode =
                          ' <tr> ' +
                          ' <td class=tdCol> ' +
                          ' <input type=checkbox name=record> ' +
                          ' </td> ' +
                          ' <td class=tdCol>' + msg.d[i].Meterial + '</td> ' +
                          ' <td class=tdCol>' + msg.d[i].Description + '</td> ' +
                          ' </tr> ' + ''
                             text += htmlcode;
                         }
                         document.getElementById("tbodyCol").innerHTML = text;
                     }
                 },
                 error: function (result) {
                 },
                 failure: function (response) {
                     alert(response.d);
                 },
                 error: function (response) {
                     alert(response.d);
                 }
             });

             return true;

         }

         function BindToList() {

             var myTableArray = [];

             $("#tableCols tr").each(function () {
                 var arrayOfThisRow = [];
                 var tableData = $(this).find('td');
                 if (tableData.length > 0) {
                     tableData.each(function () { arrayOfThisRow.push($(this).text()); });
                     myTableArray.push(arrayOfThisRow);
                 }
             });

             $("#ContentSection_hdnField").val(myTableArray);

             // alert($("#ContentSection_hdnField").val());
             return true;
         }

    </script>

    <script type="text/javascript">
        function ClearBom() {
            document.getElementById('fileReplace').value = "";
            document.getElementById('files').value = "";
            document.getElementById('supportivefiles').value = "";
            $("#tbodyCol tr").remove();

            return true;
        }
    </script>
    <script type="text/javascript">

        function ScrollPageToUp() {
            $("#msg").scrollTop();
            return true;
        }
       
   </script>
    <script>
        $(document).ready(function () {
            $("#itemCount").text($('#tableCols tr').length-1);
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
