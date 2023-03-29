<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ConfirmReceivedInventory.aspx.cs" Inherits="BiddingSystem.ConfirmReceivedInventory" %>

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
      View Received Inventory
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Received Inventory</li>
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


                <section class="content" style="padding-top: 0px">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelMRN" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >View Received Inventory</h3>

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
                
              <asp:GridView runat="server" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                  ID="gvDeliveredInventory" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" DataKeyNames="MrndInID" EmptyDataText="No records Found">
                    <Columns>
                        <asp:BoundField DataField="MrndInID"  HeaderText="MRNDIN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="MrndID"  HeaderText="MRND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="ItemID"  HeaderText="Item ID"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="MrnID"  HeaderText="MRN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:TemplateField HeaderText="MRN Code">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnMrn" Text='<%# "MRN-"+Eval("MrnCode").ToString() %>' OnClick="btnMrn_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department Name"/>
                        <asp:BoundField DataField="ItemName"  HeaderText="Item Name" />                        
                        <asp:BoundField DataField="IssuedQty"  HeaderText="Issued QTY"/>
                        <asp:BoundField DataField="ShortCode"  HeaderText="Unit" NullDisplayText="Not Found"/>
                        <asp:BoundField DataField="IssuedUser"  HeaderText="Issued By"/>
                        <asp:BoundField DataField="IssuedOn" HeaderText="Issued On" DataFormatString='<%$ appSettings:dateTimePattern %>'/>

                        <asp:BoundField DataField="DeliveredUser"  HeaderText="Delivered By"/>
                        <asp:BoundField DataField="DeliveredOn" HeaderText="Delivered On" DataFormatString='<%$ appSettings:dateTimePattern %>'/>

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnReceive" CssClass="btn btn-sm btn-success" Text="Confirm" OnClientClick='<%#"Confirm(event,"+Eval("MrndInID").ToString()+","+Eval("MrndID").ToString()+","+Eval("IssuedQty").ToString()+")" %>' ></asp:Button>
                            <asp:Button runat="server" ID="btnReject" CssClass="btn btn-sm btn-danger" Text="Reject" OnClientClick='<%#"Reject(event,"+Eval("MrndInID").ToString()+","+Eval("MrndID").ToString()+", "+Eval("IssuedQty").ToString()+" )" %>' ></asp:Button>
                      
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


                <section class="content" style="padding-top: 0px">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelApprovRejectMRN" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >View Confirmed Inventory</h3>

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
              <asp:GridView runat="server" ID="gvReceivedInventory" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" EmptyDataText="No records Found">
                    <Columns>
                        <asp:BoundField DataField="MrndInID"  HeaderText="MRNDIN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="MrndID"  HeaderText="MRND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="MrnID"  HeaderText="MRN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                         <asp:TemplateField HeaderText="MRN Code">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnReceivedMrn" Text='<%# "MRN-"+Eval("MrnCode").ToString() %>' OnClick="btnReceivedMrn_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ItemID"  HeaderText="Item ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="ItemName"  HeaderText="Item Name" />
                        <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department Name"/>
                        <asp:BoundField DataField="IssuedQty"  HeaderText="Issued QTY"/>
                        <asp:BoundField DataField="ShortCode"  HeaderText="Unit" NullDisplayText="Not Found"/>
                        <asp:BoundField DataField="IssuedOn" HeaderText="Issued On"/>
                        <asp:BoundField DataField="ReceivedUser"  HeaderText="Received By"/>
                        <asp:BoundField DataField="ReceivedOn" HeaderText="Received On"/>
                        <asp:BoundField DataField="ConfirmedUser"  HeaderText="Received Confirmation By"/>
                        <asp:BoundField DataField="ConfirmedOn" HeaderText="Received Confirmation On"/>
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
                <asp:Button ID="btnConfirm" runat="server" OnClick="btnReceive_Click" CssClass="hidden" />
                <asp:Button ID="lbtnReject" runat="server" OnClick="btnReject_Click" CssClass="hidden" />
                <asp:HiddenField ID="hdnMrndInID" runat="server" />
                <asp:HiddenField ID="hdnMrndID" runat="server" />
                <asp:HiddenField ID="hdnQty" runat="server" />
                <asp:HiddenField ID="hdnIssuesQty" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function Confirm(e, MrndInID, MrndID, IssuedQty) {
            e.preventDefault();
            $('#ContentSection_hdnMrndInID').val(MrndInID);
            $('#ContentSection_hdnMrndID').val(MrndID);
             $('#ContentSection_hdnIssuesQty').val(IssuedQty);
            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to <strong>Confirm</strong> the Stock?</br></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                    

                }
            }
            ).then((result) => {
                if (result.value) {


                    $('#ContentSection_btnConfirm').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {

                }
            });


        }

        function Reject(e, MrndInID, MrndID, Qty) {
            e.preventDefault();
            $('#ContentSection_hdnMrndInID').val(MrndInID);
            $('#ContentSection_hdnMrndID').val(MrndID);
            $('#ContentSection_hdnQty').val(Qty);
            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to <strong>Reject</strong> the Stock?</br></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                    

                }
            }
            ).then((result) => {
                if (result.value) {


                    $('#ContentSection_lbtnReject').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {

                }
            });


        }
    </script>
</asp:Content>

