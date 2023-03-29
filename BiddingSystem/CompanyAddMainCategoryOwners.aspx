<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="CompanyAddMainCategoryOwners.aspx.cs" Inherits="BiddingSystem.CompanyAddMainCategoryOwners" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style type="text/css">
        .tablegv {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .tablegv td {
                border: 1px solid #ddd;
                padding: 8px;
            }

            .tablegv th{
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

        .ChildGrid{
            width:100%!important;
        }


        .button-edit {
            background-image: url(../images/document.png); /* 16px x 16px */
            background-color: transparent; /* make the button transparent */
            background-repeat: no-repeat; /* make the background image appear only once */
            background-position: 0px 0px; /* equivalent to 'top left' */
            border: none; /* assuming we don't want any borders */
            cursor: pointer; /* make the cursor like hovering over an <a> element */
            height: 16px; /* make this the size of your image */
            padding-left: 16px; /* make text start to the right of the image */
            vertical-align: middle;
        }
       
        .modal-open {
  overflow-y: scroll;
}
     
    </style>

    

    <section class="content-header">
      <h1>
       Add Category Owners
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Add Category Owners</li>
      </ol>
    </section>
    <br />
    <form id="form1" runat="server">

        <asp:ScriptManager runat="server">
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">


            <ContentTemplate>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hndAction" runat="server" Value="Save" />

                 <asp:HiddenField ID="hndEditRowId" runat="server" />
                 <asp:HiddenField ID="hndDeleteRowId" runat="server" Value="0" />
                <section class="content" style="padding-top:0px">
       
                    <div id="modalDeleteYesNo" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 id="lblTitleDeleteYesNo" class="modal-title">Confirmation</h4>
                    </div>
                    <div class="modal-body">
                        <p>Are you sure you want to delete this record ?</p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="lnkBtnDelete_Click" 
                            Text="Yes"></asp:Button>
                        <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger">
                            No</button>
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
          <h3 class="box-title" >Category details</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">
            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">Category Name</label>
                 <asp:DropDownList  ID="ddlCategoryList" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                </div>
                </div>               
          </div>
          <div class="row">
               <div class="col-md-6">
                 <div class="form-group">
                   <label style="font-weight:bold">Add Category Owners</label>
                      <hr>
                </div>
                  
                   </div>
              <div class="col-md-12" >                
                   <div class="form-group row" style="padding-top: 20px;">
                    <div class="col-md-2" >  
                        <label for="exampleInputEmail1" style="padding-left: 13px;">Select Owner Type </label> 
                    </div>
                   <div class="col-md-2" style="padding-left: 5px;">                
                        <asp:DropDownList  ID="ddlOwnerType" runat="server" CssClass="form-control"   >
                        </asp:DropDownList>
                    </div>
                        <div class="col-md-2" >  
                        <label for="exampleInputEmail1" style="padding-left: 13px;">Select User</label> 
                    </div>
                   <div class="col-md-2" style="padding-left: 5px;">                
                        <asp:DropDownList  ID="ddlUserId" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                  <div class="form-group row">
                        <div class="col-md-2" >  
                        <label for="exampleInputEmail1" style="padding-left: 13px;">Effective Date</label> 
                    </div>
                       <div class="col-md-2" style="padding-left: 5px;">    
                            
                           <asp:TextBox ID="effectiveDate" runat="server" type="date" AutoPostBack="true" CssClass="form-control customDate"  data-date="" data-date-format="DD MMM YYYY" autocomplete="off"  ></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true" ControlToValidate="effectiveDate" ValidationGroup="btnSave" style="display:none">* Fill This Field</asp:RequiredFieldValidator>
                        </div>
                  </div>
                 </div>                
          </div>
            <div class="col-md-11">
             <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Save"   OnClientClick="return SaveItemCategoryOwner()" CssClass="btn btn-primary" OnClick="btnSave_Click"></asp:Button>
               <asp:Button ID="btnClear"  runat="server" Text="Clear" AutoPostBack="true"     CssClass="btn btn-danger" OnClick="btnClear_Click" > </asp:Button>
                  </span>
              </div>
            </div>
           </div>
        <div class="box-footer">
           
          <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gvMainCategory" runat="server"  DataKeyNames="CategoryId" CssClass="table table-responsive tablegv" GridLines="None"  OnRowDataBound="OnRowDataBound" AutoGenerateColumns="False" >
        <Columns>
             <asp:TemplateField> 
                <ItemTemplate>
                        <img alt="" class="plusMark" style="cursor: pointer" src="images/plus.png" />
                        <asp:Panel ID="pnlCategoryOwner" runat="server" Style="display: none" >
                            <asp:GridView ID="gvCategoryOwnerHistory" DataKeyNames="CategoryId" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid">
                                <Columns>    
                                   <asp:BoundField DataField="Id" HeaderText="Id"  ItemStyle-CssClass="RowId Id hidden"  HeaderStyle-CssClass="hidden">
                                  </asp:BoundField> 
                                    <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden CategoryId">
                                     <ItemStyle CssClass="hidden CategoryId" />
                                  </asp:BoundField>                   
                                <asp:BoundField DataField="OwnerType" HeaderText="OwnerType" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden OwnerType">
                                     <ItemStyle CssClass="hidden OwnerType" />
                                  </asp:BoundField>  
                                    <asp:TemplateField HeaderText="Owner Type" >
                                     <ItemTemplate>
                                       <%-- <asp:Label Text='<%#Eval("OwnerType").ToString() == "SK" ? "Store Keeper" : "Purchasing Officer" %>' Font-Bold="true"  runat="server"></asp:Label>--%>
                                       <asp:Label Text='<%#listOwnerType.Find(x=>x.Id == Eval("OwnerType").ToString()).Name %>' Font-Bold="true"  runat="server"></asp:Label>
                                     </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="UserId" HeaderText="UserId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden UserId">
                                     <ItemStyle CssClass="hidden UserId" />
                                  </asp:BoundField> 
                                     <asp:TemplateField HeaderText="User" >
                                     <ItemTemplate>
                                           <%-- <asp:Label Text='<%#CompanyLoginUserList.FindAll(x=>x.UserId == Convert.ToInt32(Eval("UserId").ToString())).Username %>' Font-Bold="true"  runat="server"></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" DataFormatString='<%$ appSettings:datePattern %>'  ItemStyle-CssClass="EffectiveDate">
                                     <ItemStyle CssClass="EffectiveDate" />
                                  </asp:BoundField>
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="rowedit">
                                       <ItemTemplate>
                                           <a type="button" id="btnEdit"  onclick="EditCategoryOwnerHistoryTable(this)" style="cursor:pointer" >Edit</a>
                                      </ItemTemplate>
                                      <ItemStyle CssClass="rowedit" />
                                    </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="rowdelete">
                                      <ItemTemplate>
                                           <a id="btnDelete"  onclick="DeleteCategoryOwnerHistoryTable(this)" style="cursor:pointer"> Delete </a>
                                      </ItemTemplate>
                                      <ItemStyle CssClass="rowdelete" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                   </ItemTemplate>
             </asp:TemplateField>

             

            <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden CategoryId">
              <HeaderStyle CssClass="hidden" />
              <ItemStyle CssClass="hidden CategoryId" />
              </asp:BoundField>
            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-CssClass="CategoryName" >
              <ItemStyle CssClass="CategoryName" />
              </asp:BoundField>
             <asp:TemplateField   HeaderText="Category Owner" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden CatergoryOwnerId">
                 <ItemTemplate>
                     <asp:Literal ID="ltCatergoryOwner" runat="server"></asp:Literal>
                 </ItemTemplate>
         </asp:TemplateField>
             <asp:TemplateField   HeaderText="Store Keeper" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden StoreKeeperId">
                 <ItemTemplate>
                     <asp:Literal ID="ltStoreKeeper" runat="server"></asp:Literal>
                 </ItemTemplate>
         </asp:TemplateField>
             <asp:TemplateField   HeaderText="Purchasing Officer"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden PurchasingOfficerId">
                  <ItemTemplate>
                     <asp:Literal ID="ltPurchasingOfficer" runat="server"></asp:Literal>
                 </ItemTemplate>
         </asp:TemplateField>
             <asp:TemplateField   HeaderText="Current Catergory Owner" >
                 <ItemTemplate>
                     <asp:Literal ID="ltCatergoryOwnerName" runat="server"></asp:Literal>
                 </ItemTemplate>
         </asp:TemplateField>
              <asp:TemplateField   HeaderText="CO Effective Date" ItemStyle-CssClass="EffectiveDate" >
                  <ItemTemplate>
                     <asp:Literal ID="ltCOEffectiveDate" runat="server"  ></asp:Literal>
                 </ItemTemplate>
         </asp:TemplateField>
            <%-- <asp:TemplateField   HeaderText="Current Store Keeper" >
                 <ItemTemplate>
                     <asp:Literal ID="ltStoreKeeperName" runat="server"></asp:Literal>
                 </ItemTemplate>
         </asp:TemplateField>--%>
             <%-- <asp:TemplateField   HeaderText="SK Effective Date" ItemStyle-CssClass="EffectiveDate" >
                  <ItemTemplate>
                     <asp:Literal ID="ltSKEffectiveDate" runat="server"></asp:Literal>
                 </ItemTemplate>
         </asp:TemplateField> --%>
             <asp:TemplateField   HeaderText="CurrentPurchasing Officer" >
                  <ItemTemplate>
                     <asp:Literal ID="ltPurchasingOfficerName" runat="server"></asp:Literal>
                 </ItemTemplate>
         </asp:TemplateField>
              <asp:TemplateField   HeaderText="PO Effective Date" ItemStyle-CssClass="EffectiveDate" >
                  <ItemTemplate>
                     <asp:Literal ID="ltPOEffectiveDate" runat="server" ></asp:Literal>
                 </ItemTemplate>
         </asp:TemplateField>
    
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
      </div>
   
    </section>
            </ContentTemplate>
             
        </asp:UpdatePanel>
        <asp:HiddenField ID="hdnMainCatecoryId" runat="server" />
        <asp:HiddenField ID="hdnStatus" runat="server" />
    </form>

   

    <script type="text/javascript" src="AdminResources/js/jquery1.8.min.js"></script>
    <script type="text/javascript">

        $("[src*=plus]").live("click", function () {
            $("#divMessage").css("display", "none");
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $("#divMessage").css("display", "none");
            $(this).attr("src", "images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
     <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
       <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <script type="text/javascript" src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/moment.min.js"></script>
    <script type="text/javascript" src="AdminResources/js/autoCompleter.js"></script>
    <script type="text/javascript">

       

        function EditCategoryOwnerHistoryTable(obj) {
            var rowId = $(obj).closest('tr').find('td.Id').html();
            var categoryId = $(obj).closest('tr').find('td.CategoryId').html();
            var OwnerType = $(obj).closest('tr').find('td.OwnerType').html();
            var User = $(obj).closest('tr').find('td.UserId').html();
            var effectiveDate = $(obj).closest('tr').find('td.EffectiveDate').text().trim();
            $("#ContentSection_ddlCategoryList").val(categoryId);
            $("#ContentSection_effectiveDate").text(effectiveDate);
            $("#ContentSection_ddlOwnerType").val(OwnerType);
            $("#ContentSection_ddlUserId").val(User);


            effectiveDate = new Date(effectiveDate);
            var dateString = new Date(effectiveDate.getTime() - (effectiveDate.getTimezoneOffset() * 60000)).toISOString().split("T")[0];
            $("#ContentSection_effectiveDate").val(dateString);
            $("#ContentSection_effectiveDate").attr('data-date', moment(dateString, 'YYYY-MM-DD').format($("#ContentSection_effectiveDate").attr('data-date-format')));

            $("#ContentSection_hndEditRowId").val(rowId);
            $("#ContentSection_hndAction").val("Update");
            $("#ContentSection_btnSave").val("Update");
            $("#ContentSection_btnSave").removeClass("btn-primary")
            $("#ContentSection_btnSave").addClass("btn-success");

            document.body.scrollTop = 100;
            document.documentElement.scrollTop = 100;
        }

        
        function DeleteCategoryOwnerHistoryTable(obj) {
            var rowId = $(obj).closest('tr').find('td.Id').html();
            $("#<%=hndDeleteRowId.ClientID%>").val(rowId);
            showDeleteModal();
        }

        function hideDeleteModal() {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('hide');
            return this.false;
        }

        function showDeleteModal() {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('show');
            event.preventDefault();
            return this.false;           
        }

        // This is used  while user change date from datepicker - some times this is handle from backend
        $(".customDate").on("change", function () {
            if (this.value) {
                $(this).attr('data-date', moment(this.value, 'YYYY-MM-DD').format($(this).attr('data-date-format')));
            } else {
                $(this).attr('data-date', '');
            }
        }).trigger("change")

        Sys.Application.add_load(function () {

            //onload set date value - used to set date if page refresh or went to backend and came
            var this1 = $(".customDate");
            if (this1.val() != undefined && this1.val() != "") {
                this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
            }
            if ($("#ContentSection_hndEditRowId").val() != "") {              
                $("#ContentSection_btnSave").val("Update");
                $("#ContentSection_btnSave").removeClass("btn-primary")
                $("#ContentSection_btnSave").addClass("btn-success");
            }

            $('#modalDeleteYesNo').on('shown.bs.modal', function () {
                $(".modal-backdrop.in").hide();
            })


        });

        function SaveItemCategoryOwner() {
            if ($("#ContentSection_effectiveDate").val() == "") {
                $("#ContentSection_RequiredFieldValidator3").css("display", "block");
                $("#ContentSection_RequiredFieldValidator3").css("visibility", "visible");
                return false;
            } else {
                return true
            }
        }
    </script>
     
</asp:Content>
