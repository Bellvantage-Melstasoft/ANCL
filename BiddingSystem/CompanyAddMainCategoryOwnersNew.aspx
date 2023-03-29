<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyAddMainCategoryOwnersNew.aspx.cs" Inherits="BiddingSystem.CompanyAddMainCategoryOwnersNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style>
    .select2-container--default .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            color: black;
        }

    </style>
    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />

     <script src="AdminResources/js/jquery1.8.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>

    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <section class="content-header">
      <%--<h1>
       Add Category Owners
        <small></small>
      </h1>--%>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Sub category Owners</li>
      </ol>
    </section>
    <br/>


      <form id="form1" runat="server">

       <asp:ScriptManager runat="server">
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">


            <ContentTemplate>
                 <section class="content" style="padding-top:0px">

                     <br>
                      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Category details</h3>

         <%-- <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>--%>
        </div>
        <div class="box-body">
          <div class="row">
            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">Main Category</label>
                 <asp:DropDownList  ID="ddlMainCategory" runat="server" CssClass="form-control" onselectedindexchanged="ddlMainCategory_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Sub Category</label>
                 <asp:DropDownList  ID="ddlSubcategory" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                </div>

                 <div class="form-group">
                    <label for="exampleInputEmail1">Users</label>
                     <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlHeadOfWarehouse" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     --%>
                     <asp:ListBox ID="ddlUsers" SelectionMode="Multiple" runat="server" CssClass="select2 form-control"   style="color:black; width:100%">
                    </asp:ListBox>
                    <%--<asp:DropDownList ID="ddlHeadOfWarehouse" runat="server" CssClass="form-control">
                    </asp:DropDownList>--%>
                </div>


                <div class="form-group row">
                       <div class="col-md-6">
                        <label for="exampleInputEmail1" style="padding-left: 13px;">Effective Date</label> 
                   
                              <asp:TextBox ID="txtEffectiveDate" runat="server"  AutoPostBack="true" type="date"  onchange="dateChange(this)" CssClass="form-control customDate"  data-date="" data-date-format="DD MMM YYYY" autocomplete="off"  ></asp:TextBox>
                           
                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true" ControlToValidate="effectiveDate" ValidationGroup="btnSave" style="display:none">* Fill This Field</asp:RequiredFieldValidator>--%>
                       </div>

                </div>               
          </div>
               </div>
           </div>
                        
         
              <div class="box-footer">
             
             <div class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Save"   OnClientClick="return SaveItemCategoryOwner()" CssClass="btn btn-primary" OnClick="btnSave_Click"></asp:Button>
               <asp:Button ID="btnClear"  runat="server" Text="Clear" AutoPostBack="true"     CssClass="btn btn-danger" OnClick="btnClear_Click" > </asp:Button>
                 </div>
                  </div>
           </div>




 <div class="box box-info">

     <div class="box-header">
          Store Keeper details

        </div>

      <div class="box-body">

    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView runat="server" ID="gvCurrentSK" GridLines="None"
                                                CssClass="table table-responsive" AutoGenerateColumns="false" enablesortingandpagingcallbacks="False"
                                                HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" OnRowDataBound="OnRowDataBound" DataKeyNames="SubCategoryId" >
                                                <Columns>
                                                       




                                                    <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                                    src="images/plus.png" />
                                                                <asp:Panel ID="pnlSK" runat="server" Style="display: none">
                                                                    <asp:GridView ID="gvUsers" runat="server"
                                                                        CssClass="table table-responsive ChildGrid"
                                                                        GridLines="None" AutoGenerateColumns="false">
                                                                         <Columns>
                                                                             <asp:BoundField DataField="SubCategoryId"
                                                                                HeaderText="Sub Category Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />

                                                                            <asp:BoundField DataField="CategoryId"
                                                                                HeaderText="Category Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                             <asp:BoundField DataField="UserId"
                                                                                HeaderText="User Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                             <asp:BoundField DataField="UserName"
                                                                                HeaderText="User Name" />
                                                                              <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date"
                                                                                DataFormatString="{0:dd-MM-yyyy}" />
                                                                              <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>
                                                               
                                                                <asp:Button CssClass="btn btn-xs btn-warning" runat="server" Text="Edit" OnClick="btnEdit_Click"
                                                                    style="margin-top:3px; width:100px;"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                                       <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>
                                                                 <asp:Button CssClass="btn btn-xs btn-danger" runat="server" Text="Delete" OnClick="btnDelete_Click"
                                                                    style="margin-top:3px; width:100px;"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                            
                                                                         </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                                            <asp:BoundField DataField="SubCategoryId"
                                                                                HeaderText="Sub Category Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />

                                                                            <asp:BoundField DataField="CategoryId"
                                                                                HeaderText="Category Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            
                                                                            <asp:BoundField DataField="CategoryName"
                                                                                HeaderText="Category Name" />
                                                                            <asp:BoundField DataField="SubCategoryName"
                                                                                HeaderText="Sub Category Name" />
                                                                             <asp:BoundField DataField="UserName" NullDisplayText="Not Available"
                                                                                HeaderText="Current Store Keeper" />
                                                   

            </Columns>


    </asp:GridView>
            </div>
        </div>
     
     </div>
     </div>



                    
                  </section>
            </ContentTemplate>
             
        </asp:UpdatePanel>
       
    </form>
    <script src="adminresources/js/jquery.min.js" type="text/javascript"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>

    <script src="AdminResources/js/select2.full.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.js"></script>
    <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />
    <script src="AdminResources/js/daterangepicker.js" type="text/javascript"></script>
   
    <script src="AdminResources/js/moment.min.js"></script>
       <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
       <script type="text/javascript">
           Sys.Application.add_load(function () {
               $(function () {
                   $('.select2').select2();
               })
               //onload set date value
               var this1 = $("#ContentSection_txtEffectiveDate");
               if (this1.val() != "") {
                   $("#ContentSection_txtEffectiveDate").attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
               }

           });

           //$(document).ready(function () {
           //    //onload set date value
           //    var this1 = $("#ContentSection_txtEffectiveDate");
           //    if (this1.val() != "") {
           //        $("#ContentSection_txtEffectiveDate").attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
           //    }
           //});


           $(".customDate").on("change", function () {
               if (this.value) {
                   $(this).attr('data-date', moment(this.value, 'YYYY-MM-DD').format($(this).attr('data-date-format')));
               } else {
                   $(this).attr('data-date', '');
               }
           }).trigger("change")

           function dateChange(obj) {
               if (obj.value) {
                   $(obj).attr('data-date', moment(obj.value, 'YYYY-MM-DD').format($(obj).attr('data-date-format')));
               } else {
                   $(obj).attr('data-date', '');
               }
           }

           </script>
   
</asp:Content>
