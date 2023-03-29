<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="CompanyAddCommittee.aspx.cs" Inherits="BiddingSystem.CompanyAddCommittee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style>
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
    </style>



    <section class="content-header">
    <h1>Add Committee & members</h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Add Committee & Members</li>
        </ol>
    </section>

    <form id="form1" runat="server">

        <asp:ScriptManager runat="server">
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hndCommitteeAction" runat="server" Value="Save" />
                <asp:HiddenField ID="hndCommitteeEditRowId" runat="server" Value="0" />
                <asp:HiddenField ID="hndCommitteeDeleteRowId" runat="server" Value="0" />

                <asp:HiddenField ID="hndCommitteeMemberAction" runat="server" Value="Save" />
                <asp:HiddenField ID="hdnCommitteeMemberEditRowId" runat="server" />
                <asp:HiddenField ID="hdnCommitteeMemberDeleteRowId" runat="server" />

                <select id="ddlHiddenDropdownlist" class="form-control" style="display: none"></select>
                <section class="content">
   
    <div id="modalCommitteeDeleteYesNo" class="modal fade">
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
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="lnkBtnCommitteeDelete_Click" 
                            Text="Yes"></asp:Button>
                        <button  onclick="return hideCommitteeDeleteModal();" type="button" class="btn btn-danger">
                            No</button>
                    </div>
                </div>
            </div>
        </div>

   <div id="modalCommitteeMemberDeleteYesNo" class="modal fade">
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
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" OnClick="lnkBtnCommitteeMemberDelete_Click" 
                            Text="Yes"></asp:Button>
                        <button  onclick="return hideCommitteeMemberDeleteModal();" type="button" class="btn btn-danger">
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
        <h3 class="box-title" >Add Committee</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
        <div class="col-md-4">
            <div class="form-inline">
                <div class="col-md-2">
                <label for="exampleInputEmail1">Name</label>
                 </div>
                <div class="col-md-3">
                <asp:TextBox ID="txtCommitteeName"  runat="server" CssClass="form-control"  autocomplete="off" CausesValidation="true"></asp:TextBox>                
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="txtCommitteeName" ValidationGroup="btnCommitteeSave" ForeColor="Red" Font-Bold="true">* Required</asp:RequiredFieldValidator>
                </div>
            </div>
        </div>  


             <div class="col-md-4">
            <div class="form-inline">
                <div class="col-md-2">
                <label for="exampleInputEmail1" style="padding-left: 13px;">Type</label> 
                 </div>
                <div class="col-md-3">
                 <asp:DropDownList ID="ddlLocalImport" runat="server" CssClass="form-control" >
                               <asp:ListItem Text="Local" Value="1"></asp:ListItem>
							   <asp:ListItem Text="Import" Value="2"></asp:ListItem>
                           </asp:DropDownList>

                </div>
            </div>
        </div>  

            
        <div class="col-md-2"> 
            <span>
                <asp:Button ID="btnCommitteeSave" runat="server" Text="Save"   CssClass="btn btn-primary"  OnClick="btnCommitteeSave_Click"></asp:Button>                
            </span>  
        </div>            
        </div>
    </div>
    <div class="box-footer">
            

    <div class="panel-body">
        <div class="co-md-12">
            <div class="table-responsive">
                <asp:GridView ID="gvCommittee" runat="server"  DataKeyNames="CommitteeId" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="CommitteeId" HeaderText="CommitteeId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden CommitteeId"></asp:BoundField>
                    <asp:BoundField DataField="CommitteeName" HeaderText="Committee Name" ItemStyle-CssClass="CommitteeName" ></asp:BoundField>
                    
                    <asp:TemplateField HeaderText="Committee Type" >
                            <ItemTemplate>
                                <asp:Label Text='<%#Eval("CommitteeType").ToString() == "1" ? "Local" :"Import" %>' Font-Bold="true"  runat="server"></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>

                    <asp:TemplateField HeaderText="Created User" ItemStyle-CssClass="CreatedUser">
                            <ItemTemplate>
                                <asp:Label Text='<%#CompanyLoginUserList.Find(x=>x.UserId== Convert.ToInt32(Eval("CreatedUser").ToString())).Username  %>' Font-Bold="true"  runat="server"></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                   <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" ItemStyle-CssClass="CreatedDate" DataFormatString='<%$ appSettings:dateTimePattern %>' ></asp:BoundField>
                        <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="rowedit">
                            <ItemTemplate>
                                <a type="button" id="btnEdit"  onclick="EditCommitteeFromMainTable(this)" style="cursor:pointer" >Edit</a>
                            </ItemTemplate>
                            <ItemStyle CssClass="rowedit" />
                        </asp:TemplateField>
                  <%--  <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="rowdelete">
                            <ItemTemplate>
                                <a id="btnDelete"  onclick="DeleteCommitteeFromMainTable(this)" style="cursor:pointer"> Delete </a>
                            </ItemTemplate>
                            <ItemStyle CssClass="rowdelete" />
                        </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </div>
    
    </div>
        
    </div>


<div class="box box-info">
    <div class="box-header with-border">
        <h3 class="box-title" >Add Committee Members</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
             <div class="col-md-6">
                  <div class="form-group col-md-12">
                       <div class="col-md-4">
                <label for="exampleInputEmail1">Select Committee</label>
                              </div>
                    <div class="col-md-5">
                 <asp:DropDownList  ID="ddlCommittee" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        </div>
                </div>
                <div class="form-group col-md-12">
                       <div class="col-md-4">
                <label for="exampleInputEmail1">Allowed Approval Count</label>
                              </div>
                    <div class="col-md-5">
                 <asp:TextBox  Type="number" value="1" ID="txtAllowedApprovalCount" runat="server" CssClass="form-control"  CausesValidation="true"></asp:TextBox>
                        </div>
                </div>
                  <div class="form-group col-md-12">
                        <div class="col-md-4" >  
                        <label for="exampleInputEmail1">Effective Date</label> 
                       </div>
                       <div class="col-md-5" > 
                           <asp:TextBox ID="effectiveDate" AutoPostBack="true" runat="server" type="date"  CssClass="form-control customDate"  data-date="" data-date-format="DD MMMM YYYY" autocomplete="off" ></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true" ControlToValidate="effectiveDate" ValidationGroup="btnCommitteeMemberSave" style="display:none">* Fill This Field</asp:RequiredFieldValidator>
                        </div>
                     </div>
                 <div class="form-group col-md-12" id="divSequence" runat="server" Visible ="false">
                       <div class="col-md-4">
                <label for="exampleInputEmail1">Sequence</label>
                              </div>
                    <div class="col-md-5">
                 <asp:TextBox  Type="number" value="0" ID="txtSequence" runat="server" CssClass="form-control"  CausesValidation="true"></asp:TextBox>
                        </div>
                </div>
                 
             </div>
             <div class="col-md-6"> 
                <div class="form-group col-md-12">
                       <div class="col-md-4">
                        <label for="exampleInputEmail1">Designation</label>
                       </div>
                    <div class="col-md-5">
                  <asp:DropDownList  ID="ddlDesignation" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                </div>
                </div>

                  <div class="form-group col-md-12">
                       <div class="col-md-4">
                        <label for="exampleInputEmail1">Can Overide</label>
                       </div>
                  <div class="col-md-5">
                  <asp:DropDownList  ID="ddlOveride" runat="server" AutoPostBack="true" CssClass="form-control"  OnSelectedIndexChanged="ddlOveride_SelectedIndexChanged">
                      <asp:ListItem Text="Yes" Value="1"> </asp:ListItem>
                      <asp:ListItem Text="NO" Value="0"> </asp:ListItem>
                        </asp:DropDownList>
                        </div>
                  </div>
                 <div class="form-group col-md-12"  id="divOverideDesignation" runat="server" style="display:none">
                       <div class="col-md-4">
                        <label for="exampleInputEmail1">Overide Designation</label>
                       </div>
                    <div class="col-md-5">
                  <asp:DropDownList  ID="ddlOverRideDesignation" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                </div>
                </div>
        </div>   
        </div>

        <div class="row">
               <div class="form-group col-md-10">
                    <span class="pull-right">
                         <asp:Button ID="btnCommitteeMemberSave" runat="server" Text="Save"   CssClass="btn btn-primary"  OnClientClick="return SaveCommitteeMember()"  OnClick="btnCommitteeMemberSave_Click"></asp:Button>               
                    <asp:Button ID="btnCommitteeMemberClear"  runat="server" Text="Clear"   CssClass="btn btn-danger"  OnClick="btnCommitteeMemberClear_Click" />
                    </span>  
                </div>
        </div>
        
        
    </div>
    <div class="box-footer">
            

    <div class="panel-body">
        <div class="co-md-12">
            <div class="table-responsive">
                <asp:GridView ID="gvCommitteeMemberMaster" runat="server"  DataKeyNames="CommitteeId" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="False"  OnPageIndexChanging="gvCommitteeMemberMaster_PageIndexChanging"  OnRowDataBound="gvCommitteeMemberMaster_OnRowDataBound" AllowPaging="True" PageSize="5" >
                    <Columns>
                         <asp:TemplateField> 
                            <ItemTemplate>
                                <img alt="" class="plusMark1" style="cursor: pointer" src="images/plus.png" />
                                <asp:Panel ID="pnlCommitteeMemeber" runat="server" Style="display: none" >
                                    <asp:GridView ID="gvCommitteeMember"  runat="server"  DataKeyNames="CommitteeId" CssClass="ChildGrid" GridLines="None" AutoGenerateColumns="False"  >
                                    <Columns>
                                         <asp:BoundField DataField="Id" HeaderText="Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden Id">
                                        </asp:BoundField>

                                        <asp:BoundField DataField="CommitteeId" HeaderText="CommitteeId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden CommitteeId">
                                        </asp:BoundField>
                                          <asp:TemplateField HeaderText="Committee" >
                                        <ItemTemplate>
                                            <asp:Label Text='<%#ListCommittee.Find(x=>x.CommitteeId == Convert.ToInt32(Eval("CommitteeId").ToString())).CommitteeName %>' Font-Bold="true"  runat="server"></asp:Label>
                                        </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:BoundField DataField="DesignationId" HeaderText="Designation" HeaderStyle-CssClass="hidden"  ItemStyle-CssClass="hidden DesignationId" ></asp:BoundField>
                                        <asp:TemplateField HeaderText="Designation" >
                                        <ItemTemplate>
                                            <asp:Label Text='<%#listDesignation.Find(x=>x.DesignationId == Convert.ToInt32(Eval("DesignationId").ToString())).DesignationName %>' Font-Bold="true"  runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                    
                                        <asp:BoundField ItemStyle-Width="150px" DataField="SequenceOfApproval" HeaderText="Sequence No"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden Sequence"/>
                                          <asp:BoundField ItemStyle-Width="150px" DataField="AllowedApprovalCount" HeaderText="No. Of People Can Approve" ItemStyle-CssClass="Count"/>
                                        <asp:BoundField ItemStyle-Width="150px" DataField="CanOveride" HeaderText="Can Overide" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden CanOveride"/>
                                         <asp:TemplateField HeaderText="Can Overide" >
                                        <ItemTemplate>
                                            <asp:Label Text='<%#Eval("CanOveride").ToString() == "1" ? "Yes" :"NO" %>' Font-Bold="true"  runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                        <asp:BoundField ItemStyle-Width="150px" DataField="OverideDesignationId" HeaderStyle-CssClass="hidden" HeaderText="Overide Desination Id" ItemStyle-CssClass="hidden OverideDesignationId"/>
                                         <asp:TemplateField HeaderText="Overide Desination" >
                                        <ItemTemplate>
                                            <asp:Label Text='<%#Eval("OverideDesignationId").ToString() != "0" ? listDesignation.Find(x=>x.DesignationId == Convert.ToInt32(Eval("OverideDesignationId").ToString())).DesignationName : "" %>' Font-Bold="true"  runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                        <asp:BoundField ItemStyle-Width="150px" DataField="EffectiveDate" DataFormatString='<%$ appSettings:datePattern %>' HeaderText="Effective Date" ItemStyle-CssClass="EffectiveDate"/> 
                    
                                         <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="rowedit" >
                                                <ItemTemplate>
                                                    <a type="button" id="btnEdit"   onclick="EditCommitteeMemberFromMainTable(this)"  style="cursor:pointer" >Edit</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="rowdelete">
                                                <ItemTemplate>
                                                    <a id="btnDelete"  onclick="DeleteCommitteeMemberFromMainTable(this)" style="cursor:pointer"> Delete </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>                  
                                    </Columns>
                                </asp:GridView>

                                </asp:Panel>
                            </ItemTemplate>
                         </asp:TemplateField>                         
                    <asp:BoundField DataField="CommitteeId" HeaderText="CommitteeId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden CommitteeId">
                    </asp:BoundField>
                   <asp:TemplateField HeaderText="Committee" >
                    <ItemTemplate>
                        <asp:Label Text='<%#ListCommittee.Find(x=>x.CommitteeId == Convert.ToInt32(Eval("CommitteeId").ToString())).CommitteeName %>' Font-Bold="true"  runat="server"></asp:Label>
                    </ItemTemplate>
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="Committee Type" >
                            <ItemTemplate>
                                <asp:Label Text='<%#Eval("CommitteeType").ToString() == "1" ? "Local" :"Import" %>' Font-Bold="true"  runat="server"></asp:Label>
                            </ItemTemplate>
                     </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </div>
    </div>
    </div>
    
    </div>
        
    </div>
    </section>
            </ContentTemplate>
        </asp:UpdatePanel>

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

      <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script type="text/javascript" src="AdminResources/js/jquery-1.10.2.min.js"></script>
     <script src="AdminResources/js/moment.min.js"></script>
    <script type="text/javascript" src="AdminResources/js/autoCompleter.js"></script>
    <script type="text/javascript">

       
        function EditCommitteeFromMainTable(obj) {
            var rowId = $(obj).closest('tr').find('td.CommitteeId').html();
            var committeeName = $(obj).closest('tr').find('td.CommitteeName').html();
            $("#ContentSection_txtCommitteeName").val(committeeName);
            $("#ContentSection_hndCommitteeEditRowId").val(rowId);
            $("#ContentSection_hndCommitteeAction").val("Update");
            $("#ContentSection_btnCommitteeSave").val("Update");
            $("#ContentSection_btnCommitteeSave").removeClass("btn-primary")
            $("#ContentSection_btnCommitteeSave").addClass("btn-success");
        }

        function DeleteCommitteeFromMainTable(obj) {
            var rowId = $(obj).closest('tr').find('td.CommitteeId').html();
            $("#<%=hndCommitteeDeleteRowId.ClientID%>").val(rowId);
             showCommitteeDeleteModal();
         }

         function hideCommitteeDeleteModal() {
             var $confirm = $("#modalCommitteeDeleteYesNo");
             $confirm.modal('hide');
             return this.false;
         }

         function showCommitteeDeleteModal() {
             var $confirm = $("#modalCommitteeDeleteYesNo");
             $confirm.modal('show');
             event.preventDefault();
             return this.false;
         }

         function EditCommitteeMemberFromMainTable(obj) {
             var rowId = $(obj).closest('tr').find('td.Id').html();
             var committeeId = $(obj).closest('tr').find('td.CommitteeId').html();
             var designationId = $(obj).closest('tr').find('td.DesignationId').html();
             var sequence = $(obj).closest('tr').find('td.Sequence').html();
             var allowedApprovalCount = $(obj).closest('tr').find('td.Count').html();
             var effectiveDate = $(obj).closest('tr').find('td.EffectiveDate').text().trim();
             var canOveride = $(obj).closest('tr').find('td.CanOveride').html();
             var overideDesignationId = $(obj).closest('tr').find('td.OverideDesignationId').html();

             $("#ContentSection_ddlCommittee").val(committeeId);
             $("#ContentSection_ddlDesignation").val(designationId);
             $("#ContentSection_txtSequence").val(sequence);
             $("#ContentSection_txtAllowedApprovalCount").val(allowedApprovalCount);
             $("#ContentSection_ddlOveride").val(canOveride);
             //$("#ContentSection_ddlOveride").trigger('change');
             if (canOveride == "1") {
                 $("#ContentSection_divOverideDesignation").css("display", "block")
             } else {
                 $("#ContentSection_divOverideDesignation").css("display", "none")
             }
             $("#ContentSection_ddlOverRideDesignation").val(overideDesignationId);
             effectiveDate = new Date(effectiveDate);
             var dateString = new Date(effectiveDate.getTime() - (effectiveDate.getTimezoneOffset() * 60000)).toISOString().split("T")[0];
             $("#ContentSection_effectiveDate").val(dateString);
             $("#ContentSection_effectiveDate").attr('data-date', moment(dateString, 'YYYY-MM-DD').format($("#ContentSection_effectiveDate").attr('data-date-format')));
            

             $("#ContentSection_hndCommitteeMemberAction").val("Update");
             $("#ContentSection_hdnCommitteeMemberEditRowId").val(rowId);
             $("#ContentSection_btnCommitteeMemberSave").val("Update");
             $("#ContentSection_btnCommitteeMemberSave").removeClass("btn-primary")
             $("#ContentSection_btnCommitteeMemberSave").addClass("btn-success");
         }

         function DeleteCommitteeMemberFromMainTable(obj) {
             var rowId = $(obj).closest('tr').find('td.Id').html();
             $("#<%=hdnCommitteeMemberDeleteRowId.ClientID%>").val(rowId);
              showCommitteeMemberDeleteModal();
          }

          function hideCommitteeMemberDeleteModal() {
              var $confirm = $("#modalCommitteeMemberDeleteYesNo");
              $confirm.modal('hide');
              return this.false;
          }

          function showCommitteeMemberDeleteModal() {
              var $confirm = $("#modalCommitteeMemberDeleteYesNo");
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
              if ($("#ContentSection_hdnCommitteeMemberEditRowId").val() != "") {
                  $("#ContentSection_btnCommitteeMemberSave").val("Update");
                  $("#ContentSection_btnCommitteeMemberSave").removeClass("btn-primary")
                  $("#ContentSection_btnCommitteeMemberSave").addClass("btn-success");
              }
          });
        
          function SaveCommitteeMember() {
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
