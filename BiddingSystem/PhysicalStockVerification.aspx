<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="PhysicalStockVerification.aspx.cs" Inherits="BiddingSystem.PhysicalStockVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style type="text/css">
        .ui-datepicker-calendar {
            display: none;
        }
          .margin{
            margin-top: 25px;
        }
          .remark{
            margin-top: 70px;
        }
    </style>
    <section class="content-header">
      <h1>
       Physical Stock Verification
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Physical Stock Verification</li>
      </ol>
    </section>

    <br />
    <section class="content">
         <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>--%>

         <script src="AdminResources/googleapis/googleapis-jquery.min.js"></script>
    <link rel="stylesheet" href="AdminResources/googleapis/googleapis-jquery-ui.css">
    <script src="AdminResources/googleapis/googleapis-jquery-ui.js"></script>

        <form id="form1" runat="server">

            <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>

             <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Select Warehouse</label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlWarehouse" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                  <asp:DropDownList ID="ddlWarehouse" runat="server" CssClass="form-control"  
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4 ">
                            <div class="form-group "> 
                                <label for="exampleInputEmail1">Select Month</label>
                                  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtFDt" ValidationGroup="btnSearch">*</asp:RequiredFieldValidator>                                                                                            
                                  <asp:TextBox ID="txtFDt" runat="server" CssClass="txtFDt form-control" ></asp:TextBox>
                                </div>
                        </div>  
                                <asp:Button ID="btnRefresh" runat="server" Text="Search" class="btn btn-info btn-sm btnRefreshCl margin"  OnClick="btnRefresh_Click"></asp:Button>
                                <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="hidden margin" style="margin-right:10px; max-height:30px;" />
                            

                        
                        
                     </div>
                </div>
            </div>

            <div class="panel panel-default">
                  <div class="box-header">
                            <h3>Stock Verification</h3>
                        </div>
          <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
                <asp:GridView runat="server" ID="gvStock" EmptyDataText="No Records Found" GridLines="None" CssClass="table table-responsive"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="ItemId"  HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="ReferenceNo"  HeaderText="Item Code" />
                        <asp:BoundField DataField="ItemName"  HeaderText="Item Name"  />
                        <asp:BoundField DataField="MeasurementShortName"  HeaderText="Measurement"  />
                        <asp:BoundField DataField="AvailbleQty"  HeaderText="Available Quantity" />
                        <asp:BoundField DataField="StockValue"  HeaderText="Stock Value"/>
                        <asp:TemplateField HeaderText="Physical Stock Quantity">
                          <ItemTemplate>
                              
                             <asp:TextBox ID="txtPhysicalStQty" Enabled ='<%# Eval("CanEdit").ToString()=="1" ? true:false %>' Text='<%# Eval("IsModified").ToString()=="1" ? Eval("PhysicalAvailableQty"):"" %>'
                             type="number" step=".01" min="0" runat="server"
                             autocomplete="off" CssClass="txtQuantityCl form-control" Width="100px" T />
                          </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Physical Stock Value">
                          <ItemTemplate>
                               
                             <asp:TextBox ID="txtPhysicalStValue" Enabled ='<%# Eval("CanEdit").ToString()=="1" ? true:false %>' Text='<%# Eval("IsModified").ToString()=="1" ? Eval("PhysicalstockValue"):"" %>'
                             type="number" step=".01" min="0" runat="server"
                             autocomplete="off" CssClass="txtValueCl form-control" Width="100px" />
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                          <ItemTemplate>
                               
                             <asp:TextBox ID="txtRemarks" Enabled ='<%# Eval("CanEdit").ToString()=="1" ? true:false %>' Text='<%# Eval("IsModified").ToString()=="1" ? Eval("Remarks"):"" %>'
                             runat="server" autocomplete="off" CssClass="txtRemarksCl form-control" Width="100px" />
                          </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="PSVDId"  HeaderText="PSVD Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="IsModified"  HeaderText="Is Modified" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                    </Columns>
                </asp:GridView>
                </div>
            </div>         
          </div>

               <hr/>
                            <div class="row">
                                <asp:Panel ID="pnlCreatedBy" runat="server" Visible="false">
                                <div class="col-xs-4 col-sm-4 text-center">
                                    <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                    <asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                    <b>Created By</b>
                                   
                                </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlApprovedBy" runat="server" Visible="false">
                                    <div class="col-xs-4 col-sm-4 text-center">
                                        <asp:Image ID="imgApprovedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                        <asp:Label runat="server" ID="lblApprovedByName"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblApprovedDate"></asp:Label><br />
                                        <b id="lblApprovalText" runat="server"></b>
                                        
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlRemarks" runat="server" Visible="false">
                                    <div class="col-xs-4 col-sm-4 text-center remark ">
                                        <strong>REMARKS</strong><br/>
                                        <asp:Label runat="server" ID="lblRemarks" CssClass="text-left" style="padding-left:10px;"></asp:Label>
                                    </div>
                                </asp:Panel>

                        </div>


                </div>
                <div class="box-footer" >
                <span class="pull-right">
                <%--<asp:Button Id="btnPrint"  CssClass="btn btn-success "  runat="server" Text="Print" Visible="false" />--%>
                <asp:Button CssClass="btn btn-success " ID="btnSave" runat="server" Text="Save" Visible="false" OnClick ="btnSave_Click"/>
                <asp:Button CssClass="btn btn-warning " ID="btnUpdate" runat="server" Text="Update" Visible="false" OnClick ="btnUpdate_Click"/>
                 <asp:Button CssClass="btn btn-warning btnResubmitSelectionCl" ID="btnResubmit" runat="server" Text="Resubmit" Visible="false" OnClick="btnResubmit_Click"/>
                
                </span>
               </div>
            </div>

            </ContentTemplate>
    </asp:UpdatePanel>
         </form>
    </section>





    <script type="text/javascript">

        Sys.Application.add_load(function () {
            $(function () {
                $('.txtFDt').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    showButtonPanel: true,
                    currentText: 'Present',
                    dateFormat: 'MM yy',
                    onClose: function (dateText, inst) {

                        //Get the selected month value
                        var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();

                        //Get the selected year value
                        var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();

                        //set month value to the textbox
                        $(this).datepicker('setDate', new Date(year, month, 1));
                    }
                });

                $('.btnRefreshCl').on({
                    click: function () {
                        $('#loader').removeClass('hidden');
                    }
                })

            });

        });


    </script>





</asp:Content>
