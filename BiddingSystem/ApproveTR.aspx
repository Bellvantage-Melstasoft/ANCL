<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ApproveTR.aspx.cs" Inherits="BiddingSystem.ApproveTR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

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

    <style type="text/css">
        .ChildGrid td {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
        }

        .ChildGrid th {
            color: White;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #67778e !important;
            color: white;
        }
    </style>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <section class="content-header">
    <h1>
      Approve Transfer Request Notes 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Approve Transfer Request Notes </li>
      </ol>
    </section>
    <br />

    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
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
                                <p id="successMessage" style="font-weight: bold; font-size: medium;"></p>
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

                <section class="content">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelTR" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >View Pending Transfer Requests</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
              <asp:GridView runat="server" ID="gvTR" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" DataKeyNames="TRId" OnRowDataBound="gvTR_RowDataBound" EmptyDataText="No records Found">
                    <Columns>
                    <asp:TemplateField>
                    <ItemTemplate>
                    <img alt = "" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                    <asp:Panel ID="pnlTRD" runat="server" Style="display: none">
                        <asp:GridView ID="gvTRD" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName"  HeaderText="Sub-Category"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemName"  HeaderText="Item"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="RequestedQty"  HeaderText="Requested Qty"  /> 
                                <asp:BoundField ItemStyle-Width="150px" DataField="ShortName"  HeaderText="Unit" NullDisplayText="Not Found" /> 
                                <asp:BoundField ItemStyle-Width="150px" DataField="Description"  HeaderText="Description"  /> 
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    </ItemTemplate>
                    </asp:TemplateField>

                        <asp:BoundField DataField="TRId"  HeaderText="TR ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:TemplateField HeaderText="TR Code">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTRCode" Text='<%# "TR"+Eval("TrCode").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FromWarehouse"  HeaderText="From Warehouse Name" />
                        <asp:BoundField DataField="ToWarehouse"  HeaderText="To Warehouse" />
                        <asp:BoundField DataField="CreatedByName"  HeaderText="Created By" />
                        <asp:BoundField DataField="CreatedDatetime"  HeaderText="Created On"  DataFormatString="{0:dd-MM-yyyy hh:mm tt}"/>
                        <asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date"  DataFormatString="{0:dd-MM-yyyy}"/>
                        <asp:BoundField DataField="Description"  HeaderText="Description" />
                        
                       
                          <asp:TemplateField HeaderText="Action">
                           <ItemTemplate>
                                <asp:Button runat="server" ID="lbtnApprove" CssClass="btn btn-sm btn-warning btnSelectCl" style="margin-right:10px; margin-bottom:5px;" Width="100px" Text="Approve" OnClientClick='<%#"approveTR(event,"+Eval("TRId").ToString()+")" %>'></asp:Button>
                                <asp:Button runat="server" ID="lbtnReject" CssClass="btn btn-sm btn-danger" Width="100px" style="margin-right:10px; margin-bottom:5px;" Text="Reject" OnClientClick='<%#"rejectTR(event,"+Eval("TRId").ToString()+")" %>' ></asp:Button>
                                <asp:Button runat="server" ID="lbtnEdit" CssClass="btn btn-sm btn-info" Width="100px" Text="Edit" style="margin-right:10px; margin-bottom:5px;" OnClick = "btnModify_Click" ></asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>
                     

                    </Columns>
                </asp:GridView>
                </div>
            </div>         
          </div>
         
        </div>
        <!-- /.box-body -->
      </div>
      <!-- /.box -->
    </section>
                 <asp:HiddenField ID="hdnRemarks" runat="server" />
                <asp:Button ID="btnApprove" runat="server" OnClick="lbtnApprove_Click" CssClass="hidden" />
                <asp:Button ID="btnReject" runat="server" OnClick="lbtnReject_Click" CssClass="hidden" />
                <asp:HiddenField runat="server" ID="hdnTRID" />

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
   <script type="text/javascript">

       function approveTR(e, trId) {
           e.preventDefault();
           $('#ContentSection_hdnTRID').val(trId);

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Approve</strong> the TR?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Approve',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {
                                if ($('#ss').val() == '') {
                                    $('#dd').prop('style', 'color:red');
                                    swal.showValidationError('Remarks Required');
                                    return false;
                                }
                                else {
                                    $('#ContentSection_hdnRemarks').val($('#ss').val());
                                }

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                              
                                $('#ContentSection_btnApprove').click();
                              
                            } else if (result.dismiss === Swal.DismissReason.cancel) {

                            }
                        });


       }

       function rejectTR(e, trId) {
           e.preventDefault();
           $('#ContentSection_hdnTRID').val(trId);

           swal.fire({
               title: 'Are you sure?',
               html: "Are you sure you want to <strong>Reject</strong> the TR?</br></br>"
                   + "<strong id='dd'>Remarks</strong>"
                   + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
               type: 'warning',
               cancelButtonColor: '#d33',
               showCancelButton: true,
               showConfirmButton: true,
               confirmButtonText: 'Reject',
               cancelButtonText: 'No',
               allowOutsideClick: false,
               preConfirm: function () {
                   if ($('#ss').val() == '') {
                       $('#dd').prop('style', 'color:red');
                       swal.showValidationError('Remarks Required');
                       return false;
                   }
                   else {
                       $('#ContentSection_hdnRemarks').val($('#ss').val());
                   }

               }
           }
           ).then((result) => {
               if (result.value) {


                   $('#ContentSection_btnReject').click();
               } else if (result.dismiss === Swal.DismissReason.cancel) {

               }
           });


       }

          

    </script>

</asp:Content>