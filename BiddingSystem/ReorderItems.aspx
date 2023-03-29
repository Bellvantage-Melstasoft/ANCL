<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ReorderItems.aspx.cs" Inherits="BiddingSystem.ReorderItems" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
        .margin{
            margin-top: 25px;
        }
        </style>
     <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
<section class="content-header">
      <h1>
       Reorder Items
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Reorder Items</li>
      </ol>
    </section>
    <br />

        <section class="content">
        <form id="form1" runat="server">

            <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">

        <ContentTemplate>

             
            <div class="panel panel-default no-print">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Select Warehouse</label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlWarehouse" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                  <asp:DropDownList ID="ddlWarehouse" runat="server" CssClass="form-control"  
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                </div>
                            </div>
                                <div class="col-md-3 margin">
                                 <asp:Button runat="server" ID="btnSearch" ValidationGroup="btnSearch" CssClass="btn btn-primary btnSearch" Text="Search"  OnClick="btnSearch_Click"/>
                            </div>
                        
                     
                    </div>

                </div>
            </div>


            <asp:Panel ID="pnlItems" runat="server">
            <div class="box box-info">
                <div class="box-header with-border">
                  <h3 class="box-title" >Items</h3>
                </div>
                <div class="box-body">
                    
                  <div class="row">
                    <div class="col-md-12">
                    <div class="table-responsive">
                      <asp:GridView runat="server" ID="gvItems" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" EmptyDataText="No records Found">
                            <Columns>
                                <asp:BoundField DataField="CategoryID" HeaderText="MainCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                <asp:BoundField DataField="SubCategoryID" HeaderText="SubCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField DataField="SubCategoryName" HeaderText="Sub Category Name" />
                                <asp:BoundField DataField="ItemID" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField DataField="ItemName"  HeaderText="Item Name" />
                                <asp:BoundField DataField="Location"  HeaderText="Location" />
                                <asp:BoundField DataField="measurementShortName"  HeaderText="Unit of Measurement" NullDisplayText="Not Found"  />
                                <asp:BoundField DataField="AvailableQty"  HeaderText="Available" ItemStyle-ForeColor="Red" />
                                <asp:BoundField DataField="HoldedQty"  HeaderText="Holded"  />
                                <asp:BoundField DataField="ReorderLevel"  HeaderText="Reorder Level" ItemStyle-ForeColor="Red" />
                                <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnAddToPR" CssClass="btn btn-xs btn-info btnSelectCl" Width="120px" style="margin:5px" Text="Add To PR" ></asp:Button>
                                   </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        </div>
                    </div>         
                  </div>
                  
                </div>
            
            </div>
            </asp:Panel>

            <asp:Panel ID="pnlAddedItems" runat="server">
            <div class="box box-info">
                <div class="box-header with-border">
                  <h3 class="box-title" >Added Items</h3>
                </div>
                <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
              <asp:GridView runat="server" ID="gvAddToPR" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" EmptyDataText="No records Found">
                    <Columns>

                                <asp:BoundField DataField="MainCategoryId" HeaderText="MainCategory Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="MainCategoryName" HeaderText="Category Name" />
                                    <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="SubcategoryName" HeaderText="Sub Category Name" />
                                    <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                    <asp:BoundField DataField="ItemQuantity" HeaderText="Quantity"  DataFormatString="{0:N2}"/>
                                    <asp:BoundField DataField="EstimatedAmount" HeaderText="Estimated Unit Price" DataFormatString="{0:N2}"/>
                            <asp:BoundField DataField="WarehouseName" HeaderText="For Warehouse" /> 
                               
                    </Columns>
                </asp:GridView>
                </div>
            </div>         
          </div>
         
        </div>
                <div class="box-footer">
        <div class="form-group">
              <asp:Button ID="btnCreatePR"  runat="server" Text="Create PR" CssClass="btn btn-primary pull-right" OnClick="btnCreatePR_Click" ></asp:Button>
          </div>
        </div>
                </div>
         </asp:Panel>
        </div>



             <asp:HiddenField ID="hdnCategoryId" runat="server" />
             <asp:HiddenField ID="hdnSubCategoryId" runat="server" />
             <asp:HiddenField ID="hdnItemId" runat="server" />
            <asp:HiddenField ID="hdnQuantity" runat="server" />
             <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="hidden" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
        </Triggers>
        
    </asp:UpdatePanel>
         </form>
    </section>


      <script type="text/javascript">
          Sys.Application.add_load(function () {
              $(function () {


                  $('.btnSelectCl').on({
                      click: function () {
                          event.preventDefault();
                       

                          var tableRow = $(this).closest('tr').find('td');
                          $('#ContentSection_hdnCategoryId').val($(tableRow).eq(0).text());
                          $('#ContentSection_hdnSubCategoryId').val($(tableRow).eq(2).text());
                          $('#ContentSection_hdnItemId').val($(tableRow).eq(4).text());

                          swal.fire({
                              title: 'Add item to PR?',
                              html: "Are you sure you want to add selected item to PR? </br></br>"
                                  + "<strong id='dd'>Quantity</strong>"
                                  + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                                  
                              type: 'warning',
                              cancelButtonColor: '#d33',
                              showCancelButton: true,
                              showConfirmButton: true,
                              confirmButtonText: 'Add to PR',
                              cancelButtonText: 'Cancel',
                              allowOutsideClick: false,
                              preConfirm: function () {
                                  if ($('#ss').val() == '') {
                                      $('#dd').prop('style', 'color:red');
                                      swal.showValidationError('Required');
                                      
                                      return false;
                                  }
                                  else {
                                      $('#ContentSection_hdnQuantity').val($('#ss').val());
                                  }
                                 
                              }
                          }
                          ).then((result) => {
                              if (result.value) {
                                  $('#ContentSection_btnAdd').click();
                              }

                          });


                      }
                  });

               });
        });
    </script>



</asp:Content>
