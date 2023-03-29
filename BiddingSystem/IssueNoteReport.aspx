<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="IssueNoteReport.aspx.cs" Inherits="BiddingSystem.IssueNoteReport" %>

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
        .select2-container--default .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            color: black;
        }

         @media print {

            @page {
                size: A4;
                margin: 5mm 5mm 5mm 5mm;
            }

            .print-count {
                display: block;
            }
        }

        @media screen {
            .print-count {
                display: none;
            }
        }

        .paddingTop {
            margin-top: 25px;
        }

    </style>

    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <!-- bootstrap datepicker -->
    <section class="content-header">
      <h1>
       Issue Notes
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Issue Notes</li>
      </ol>
    </section>
    <br />
    <section class="content" id="divPrintPo">
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
                                    AutoPostBack="true" >
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%--<div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Select Departments</label>
                                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlDepartments" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                  <asp:ListBox ID="ddlDepartments" SelectionMode="Multiple" runat="server" CssClass="select2 form-control"   style="color:black; width:100%">
                                </asp:ListBox>
                            </div>
                        </div>--%>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Select Department</label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlDepartments" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                   <asp:ListBox ID="ddlDepartments" SelectionMode="Multiple" runat="server" CssClass="select2 form-control"   style="color:black; width:100%">
                              </asp:ListBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                       <div class="form-group">
                                <label for="exampleInputEmail1">From</label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true"  ControlToValidate="dtFrom" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>     
                                <asp:TextBox ID="dtFrom" runat="server"  CssClass="form-control date1" autocomplete="off" DataFormatString="{0:dd/MM/yyyy HH:mm}" ></asp:TextBox>
                       </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                 <label for="exampleInputEmail1">To</label>
                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="dtTo" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>     
                                 <asp:TextBox ID="dtTo" runat="server"  CssClass="form-control date1" autocomplete="off" DataFormatString="{0:dd/MM/yyyy HH:mm}" ></asp:TextBox>
                         </div>
                        </div>
                        
                    </div>
                    <div class="row">
                        
                              <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Select Category</label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlMainCateGory" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                  <asp:DropDownList ID="ddlMainCateGory" runat="server" CssClass="form-control"  
                                    AutoPostBack="true"  onselectedindexchanged="ddlMainCateGory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Select SubCategory</label>
                                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlSubCategory" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                  <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control"  
                                    AutoPostBack="true" onselectedindexchanged="ddlSubCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Select Item</label>
                                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlItem" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                  <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-3">
                            <div class="form-group">
                            <asp:Button runat="server" ID="btnSearch" ValidationGroup="btnSearch" CssClass="btn btn-primary pull-right btnSearch paddingTop" Text="Search" OnClick="btnSearch_Click" />
                             <img id="loader" alt="" src="UserRersourses/assets/img/loader-info.gif" class="pull-right hidden paddingTop" style="margin-right:10px; max-height:30px;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>




            
 <asp:Panel runat="server" class="print-count">
     <div class="row">
                        <div class="col-md-12" >
                   <asp:Label ID="Label1" Text="MRN Issued Note Report"  runat="server" Font-Size="20px" ></asp:Label></b></td>
                             </div>
                    </div>
                        <table>
                            <tr>
                                <td>Printed Date&nbsp;</td>
                                <td>:&nbsp;</td>
                                <td><b>
                            <asp:Label ID="lblPrintDate" runat="server"></asp:Label></b></td>
                            </tr>
                            <tr>
                                <td>Warehouse&nbsp;</td>
                                <td>:&nbsp;</td>
                                <td><b>
                                    <asp:Label ID="lblWarehouse" runat="server" Text=""></asp:Label></b></td>
                            </tr>
                            <tr>
                                <td>Departments&nbsp;</td>
                                <td>:&nbsp;</td>
                                <td><b>
                                    <asp:Label ID="lblDepartments" runat="server" Text=""></asp:Label></b></td>
                            </tr>
                           

                             <tr>
                                <td>Date From&nbsp;</td>
                                <td>:&nbsp;</td>
                                <td><b>
                                    <asp:Label ID="lblFrom" runat="server" Text=""></asp:Label></b></td>
                            </tr>
                             <tr>
                                <td>Date To&nbsp;</td>
                                <td>:&nbsp;</td>
                                <td><b>
                                    <asp:Label ID="lblTo" runat="server" Text=""></asp:Label></b></td>
                                    
                            </tr>
                        </table>
                        <div>
                            &nbsp
                            </div>
                  
</asp:Panel>

            <div class="box box-info">
                <div class="box-header with-border">
                  <h3 class="box-title" >Report</h3>
                </div>
                <div class="box-body">
                    
                  <div class="row">
                    <div class="col-md-12">
                    <div class="table-responsive">
                      <asp:GridView runat="server" ID="gvItems" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" EmptyDataText="No records Found"  onrowdatabound="gvItems_RowDataBound" >
                            <Columns>
                                <asp:TemplateField>
                                <ItemTemplate>
                                    <img alt="" style="cursor: pointer;margin-top: -6px;" src="images/plus.png"  class='<%# Eval("StockMaintainingType").ToString() =="1"?"hidden":"" %>'/>
                                    <asp:Panel ID="pnlMRNBatchDetails" runat="server" Style="display: none" >
                                        <asp:Gridview ID="gvMRNBatchDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No records Found" caption="Issue Note Inventory Batches"
                                            CssClass="ChildGrid" Width="100%" >
                                            <Columns>
                                               <%--<asp:BoundField DataField="BatchId" HeaderText="Btach Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>--%>
                                                <asp:TemplateField HeaderText="Batch Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblBatchCode"
                                                            Text='<%# "Batch-"+Eval("BatchCode").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IssuedQty" HeaderText="Issued Qty" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N2}"/>
                                                <asp:BoundField DataField="IssuedStockValue" HeaderText="Issued Stock Value" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N2}"/>
                                               <asp:BoundField DataField="BatchExpiryDate" HeaderText="Batch Expiry date" DataFormatString = "{0:dd/MM/yyyy}"/>
                                            </Columns>
                                    </asp:Gridview>
                                        </asp:Panel>
                                </ItemTemplate>
                                     </asp:TemplateField>

                                <asp:BoundField DataField="MrndInID"  HeaderText="MRNDIN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                     <asp:BoundField DataField="MrndID"  HeaderText="MRND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                     <asp:TemplateField HeaderText="MRN Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblMrnCode" Text='<%#Eval("MrnCode") == null?"MRN0": "MRN-"+Eval("MrnCode").ToString() %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>          
                                
                                                                    <asp:BoundField DataField="CategoryName"  HeaderText="Category Name"/>
                                                                     <asp:BoundField DataField="ItemName"  HeaderText="Item Name"/>
                                                                     <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department Name"/>
                                                                     <asp:BoundField DataField="WarehouseLocation"  HeaderText="Warehouse"/>
                                                                     <asp:BoundField DataField="IssuedUser"  HeaderText="Issued By"/>
                                                                     <asp:BoundField DataField="IssuedOn"  HeaderText="Issued On" />
                                                                     <asp:BoundField DataField="DeliveredUser"  HeaderText="Delivered By" NullDisplayText="Not Delivered"/>
                                                                     
                                                                     <asp:TemplateField HeaderText="Delivered On">
                                                                    <ItemTemplate>
                                                                        <%# (DateTime)Eval("DeliveredOn") == DateTime.MinValue ? "Not Delivered" : Eval("DeliveredOn") %>
                                                                    </ItemTemplate>
                                                                     </asp:TemplateField>
                                                                     <asp:BoundField DataField="ReceivedUser"  HeaderText="Received By" NullDisplayText="Not Received"/>
                                                                     <asp:TemplateField HeaderText="Received On">
                                                                    <ItemTemplate>
                                                                        <%# (DateTime)Eval("ReceivedOn") == DateTime.MinValue ? "Not Received" : Eval("ReceivedOn") %>
                                                                    </ItemTemplate>
                                                                     </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                                                    Text="Issued" CssClass="label label-info"/>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                                                    Text="Delivered" CssClass="label label-info"/>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                                                    Text="Received" CssClass="label label-success"/>
                                                                     </ItemTemplate>
                                                               </asp:TemplateField>
                                                                     <asp:BoundField DataField="IssuedQty"  HeaderText="Issued QTY" DataFormatString="{0:N2}"/>
                                                                     <asp:BoundField DataField="measurementShortName"  HeaderText="Unit" NullDisplayText="Not Found"/>
                                                                     <asp:BoundField DataField="StValue"  HeaderText="Stock Value" DataFormatString="{0:N2}"/>
                                                                     
                            </Columns>
                        </asp:GridView>

                        </div>
                    </div>         
                  </div>
                    <div class="row">
                <div class="col-xs-12 text-right">
                            <h3 style="display:inline-block;"><asp:Label ID="lblvalue" runat="server" Text="Total Stock Value" ForeColor ="Gray" ></asp:Label></h3>
                        <h3 style="display:inline-block; margin-left:100px;"><asp:Label ID="lblSumValue" runat="server" Text="" ForeColor ="Gray"></asp:Label ></h3>
                    
                    </div>
                       
                     </div>
                    
                </div>
            

            <div class ="box-footer no-print">
            <div>
                <asp:button id="btnPrint" runat="server" text="Print" class="btn btn-success btnprintcl" OnClientClick="printPage()"/>
            </div>
             </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
         </form>
    </section>

    <script src="AdminResources/js/select2.full.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.js"></script>
    <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />
    <script src="AdminResources/js/daterangepicker.js" type="text/javascript"></script>
    <script type="text/javascript">


Sys.Application.add_load(function () {
 $(function () {
$('.select2').select2();
})
});


    </script>
    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>
    <script src="AdminResources/js/select2.full.min.js"></script>
    <script type="text/javascript">
        Sys.Application.add_load(function () {

            $(function () {
                $("#<%= dtTo.ClientID %>").datepicker();
                 $("#<%= dtFrom.ClientID %>").datepicker();

                $(function () {
                    $('.btnSearch').on({
                        click: function () {
                            $('#loader').removeClass('hidden');
                        }
                    })
                });
               
            });
        });

        
        

    </script>

    <script type="text/javascript">
        function printPage() {
            var date = new Date();
            var val = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
            $('#ContentSection_lblPrintDate').text(val);

            window.print();




           
        }
              </script>


</asp:Content>
