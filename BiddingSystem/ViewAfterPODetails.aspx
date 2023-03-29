<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewAfterPODetails.aspx.cs" Inherits="BiddingSystem.ViewAfterPODetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
 
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
     <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>--%>

    <script src="AdminResources/googleapis/googleapis-jquery.min.js"></script>
    <link rel="stylesheet" href="AdminResources/googleapis/googleapis-jquery-ui.css">
    <script src="AdminResources/googleapis/googleapis-jquery-ui.js"></script>

    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="content-header">
            <h1>After PO<small></small></h1>
            <ol class="breadcrumb">
                <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li class="active">After PO</li>
            </ol>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content">
                    <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title" >After PO</h3>

                        <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                   <div class="panel panel-default" id="panelGridview" runat="server">

                   <div class="panel-heading" style="margin-bottom:20px">
                        <h3 class="panel-title">After PO Details</h3>
                   </div>



                   <div class="panel panel-default" id="panelMRNBasicSearch" runat="server">
                    <div class="panel-heading">
                        <h3 class="panel-title">Basic Search
                            <a class="arrowdown"  data-target="#basicSearch" data-toggle="collapse"  aria-expanded="false">
                            <span class="expand_caret caret" ></span>
                            </a>
                        </h3>
                    </div>

                    <div class="panel-body collapse" id="basicSearch">
                        <div class="row">
                            <div class="col-md-6">
                                <%--<asp:RadioButton ID="rdbMonth" runat="server" Checked="true" GroupName="basicSearch"></asp:RadioButton>--%>
                                <b>Search by Month</b><label class="lblerror hidden" style="color:red;" >*Fill this field</label>
                                <br>
                                <asp:TextBox ID="txtFDt" runat="server" CssClass="txtFDt form-control"></asp:TextBox>         
                            </div>
                           <%--<div class="col-md-6">
                            <asp:RadioButton ID="rdbCode" runat="server" GroupName="basicSearch"></asp:RadioButton>
                              <b> Search by MRN Code</b><label class="lblerror hidden" style="color:red;" >*Fill this field</label>
                               <asp:TextBox ID="txtMrnCode" runat="server" CssClass="form-control"  ></asp:TextBox>
                            </div>--%>
                        </div>

                         <div class="row">
                            <div class="col-md-11">
                                <asp:Image  runat="server" ID="loadingImage1" class="loadingImage pull-right hidden"   src="AdminResources/images/Spinner-0.6s-200px.gif" style="margin-top:5px;max-height: 40px;" />                                
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnBasicSearch" ValidationGroup="btnBasicSearch" OnClick="btnBasicSearch_Click" runat="server" Text="Search" style="margin-top: 10px;" CssClass ="btn btn-info pull-right" ></asp:Button>
                            </div>
                          </div>
                    </div>
                </div>




                   <div class="panel-body">
                       <div class="table-responsive">
                        <asp:GridView runat="server" ID="gvAfterPO" GridLines="None"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" CssClass="table table-responsive table-bordered  fixed" AutoGenerateColumns="false" DataKeyNames="ID" EmptyDataText="No records Found">
                             <Columns>
                                <asp:TemplateField HeaderText="HYPER_LOAN">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("HyperLoan").ToString() == "1" ? true : false %>'
                                            Text="Yes" />
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("HyperLoan").ToString() == "2" ? true : false %>'
                                            Text="No" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="MRN Code">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# "MRN"+ Eval("MrnCode").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="LC_OPENING"  HeaderText="LC_OPENING" />
                                <asp:BoundField DataField="LDS"  HeaderText="LDS"/>
                               <%-- <asp:BoundField DataField="RequiredFor"  HeaderText="Description" ItemStyle-CssClass="Description" />--%>
                                <asp:BoundField DataField="Expiry"  HeaderText="EXPIRY" />
                                <asp:BoundField DataField="ETD"  HeaderText="ETD"/>
                                <asp:BoundField DataField="ETA"  HeaderText="ETA" />
                                 <asp:BoundField DataField="Vessel"  HeaderText="VESSEL" />
                                <asp:BoundField DataField="ShippingAgent"  HeaderText="SHIPPING_AGENT"/>
                                <asp:BoundField DataField="InsuranceCompany"  HeaderText="INSURANCE_COMPANY" />
                                 <%--<asp:BoundField DataField="InsuranceDate"  HeaderText="INSURANCE_DATE" DataFormatString="{0:d}" />--%>

                                 <asp:TemplateField HeaderText="INSURANCE_DATE">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            Text='<%# Convert.ToDateTime(Eval("InsuranceDate")).ToShortDateString() %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:BoundField DataField="PolicyNo"  HeaderText="INSURANCE_POLICY_NO"/>
                                <asp:BoundField DataField="PerformanceBondNo"  HeaderText="PERFORMANCE_BOND_NO" />
                                 <%--<asp:BoundField DataField="PerformanceDate"  HeaderText="PERFORMANCE_DATE" DataFormatString="{0:d}" />--%>

                                 <asp:TemplateField HeaderText="PERFORMANCE_DATE">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            Text='<%# Convert.ToDateTime(Eval("PerformanceDate")).ToShortDateString() %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="ChargeValue"  HeaderText="CHARGE_VALUE"/>
                                <asp:BoundField DataField="InsuranceCharge"  HeaderText="INSURANCE_CHARGE"  />
                                 <asp:BoundField DataField="Freight"  HeaderText="FREIGHT" />
                                <asp:BoundField DataField="Duty"  HeaderText="DUTY"/>
                                <asp:BoundField DataField="CustomDuty"  HeaderText="CUSTOM_DUTY" />
                                 <asp:BoundField DataField="CESS"  HeaderText="CESS" />
                                <asp:BoundField DataField="PAL"  HeaderText="PAL"/>
                                <asp:BoundField DataField="VAT"  HeaderText="VAT" />
                                 <asp:BoundField DataField="NBT"  HeaderText="NBT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField DataField="OtherCustomCharges"  HeaderText="OTHER_CUSTOM_CHARGES"/>
                                <asp:BoundField DataField="RANK_CONTAINER"  HeaderText="RANK_CONTAINER" />
                                 <asp:BoundField DataField="TERMINAL"  HeaderText="TERMINAL" />
                                <asp:BoundField DataField="WeighingCharges"  HeaderText="WEIGHING_CHARGES"/>
                                <asp:BoundField DataField="SLPA"  HeaderText="SLPA" />
                                 <asp:BoundField DataField="AgentDoCharges"  HeaderText="AGENT_DO_CHARGE" />
                                <asp:BoundField DataField="ContainerDeposit"  HeaderText="CONTAINER_DEPOSIT"/>
                                <asp:BoundField DataField="WashingCharges"  HeaderText="WASHING_CHARGES" />
                                 <asp:BoundField DataField="ClearingAndTransportCharges"  HeaderText="CLEARING_AND_TRANSPORT" />
                                
                            </Columns>
                        </asp:GridView>
                           </div>
                   </div>
                </div>
                    </div>
                    </div>
                </section>

                <%--<asp:Button ID="btnSubmitNew" runat="server" OnClick="BtnSave_Click" CssClass="hidden" />--%>
            </ContentTemplate>
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </form>

    <script>
        
           Sys.Application.add_load(function () {

               $(function () {
                   $('.select2').select2();
               });

               $('.collapse').on('show.bs.collapse', function () {
                   $('.collapse.in').each(function () {
                       $(this).collapse('hide');
                   });
               });

               $("#basicSearch").collapse('show');
               var customDates = $(".customDate");
               for (x = 0 ; x < customDates.length ; ++x) {
                   if ($(customDates[x]).val() != "") {
                       $(customDates[x]).attr('data-date', moment($(customDates[x]).val(), 'YYYY-MM-DD').format($(customDates[x]).attr('data-date-format')));
                   }
               }

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

               });
           });
    </script>

</asp:Content>

