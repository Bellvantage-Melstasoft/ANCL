<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyMrnReports.aspx.cs" Inherits="BiddingSystem.CompanyMrnReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style type="text/css">
        #myModal.modal-dialog {
            width: 90%;
        }


        table {
            color: black;
        }

        body {
            color: black;
            page-break-inside: auto !important;
        }

        #divPrintPoReport #tr {
            page-break-after: auto !important;
            page-break-inside: avoid !important;
        }

        #divPrintPoReport #table {
            page-break-after: auto !important;
            page-break-inside: avoid !important;
            background-color: aquamarine;
        }

        #hiddenPrint {
            visibility: hidden;
        }
    </style>
    <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/moment.min.js"></script>

    
<section class="content-header">
    <h1>MRN Reports <small></small></h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">MRN Reports</li>
      </ol>
</section>
    <br />
<form runat="server">
    <div id="myModal" class="modal fade" tabindex="-1" role="dialog"  aria-hidden="true">
		<div class="modal-dialog" >
		<!-- Modal content-->
		<div class="modal-content" ">
			<div class="modal-header" style="background-color:#a2bdcc;">
			<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
			<h4 class="modal-title">View PO</h4>
			</div>
			<div class="modal-body">
			<div class="login-w3ls"></div>
		    </div>
		</div>
	    </div>
    </div>



<section  class="content" style="padding-top:0px">
 
      <div class="box box-info" id="panelmrnreport" runat="server">
        <div class="box-header with-border" id="viewPOSection">
            <div class="row">
                <div class="col-sm-12">
                    <div class="col-sm-4">
                          <label>MRN Code</label>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMrnCode"  ValidationGroup="btnPoCodeSearch" ForeColor="Red">*</asp:RequiredFieldValidator>
                         <div class="input-group margin">
                        <asp:TextBox ID="txtMrnCode" runat="server" CssClass="form-control" PlaceHolder="LCL1 / IMP1"></asp:TextBox>
                    <span class="input-group-btn">
                         <asp:Button runat="server" ID="btnMrnCodeSearch" ValidationGroup="btnMrnCodeSearch" OnClick="btnMrnCodeSearch_Click"  CssClass="btn btn-info"  Text="Search"/>
                    </span>
                 </div>
                    </div>

            <%--<div class="col-sm-4">
                    <label>Status</label> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="" ControlToValidate="ddlStatus"   ValidationGroup="btnPoStatusSearch" ForeColor="Red">*</asp:RequiredFieldValidator>

                    <div class="input-group margin">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" >
                    <asp:ListItem Value="">-Please Select-</asp:ListItem>
                    <asp:ListItem Value="0">Pending</asp:ListItem>
                    <asp:ListItem Value="1">Approved</asp:ListItem>
                    <asp:ListItem Value="2">Rejected</asp:ListItem>
                    </asp:DropDownList>
                <span class="input-group-btn">
                    <asp:Button runat="server" ID="btnMrnStatusSearch" OnClick="btnMrnStatusSearch_Click"  ValidationGroup="btnMrnStatusSearch" CssClass="btn btn-info"  Text="Search" />
                </span>
            </div>
            </div>--%>

            <div class="col-sm-4">  
                <label>Date</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" style="display:none" runat="server" ControlToValidate="txtStartDate"  ValidationGroup="btnMrnDateSearch" ForeColor="Red">* Please Select Dates</asp:RequiredFieldValidator>
                    
                <div class="input-group margin">
                <asp:TextBox ID="txtStartDate" runat="server" Width="50%" CssClass="form-control customDate" type="date" data-date="" data-date-format="DD MMMM YYYY" onchange="dateChange(this)"  placeholder="from" ></asp:TextBox>
                <asp:TextBox ID="txtEndDate" runat="server" Width="50%"  CssClass="form-control customDate" type="date" data-date="" data-date-format="DD MMMM YYYY"  onchange="dateChange(this)"  placeholder="to" ></asp:TextBox>  
                <span class="input-group-btn">
                        <asp:Button runat="server" ID="btnMrnDateSearch" ValidationGroup="btnMrnDateSearch" OnClick="btnMrnDateSearch_Click" OnClientClick="return DateValidate()" CssClass="btn btn-info"  Text="Search"/>
                </span>
            </div>
            </div>
                </div>
            </div>
          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
    
          <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>

        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
                 			  <asp:GridView runat="server" ID="gvMRN" GridLines="None" CssClass="table table-responsive"
                   AutoGenerateColumns="false" DataKeyNames="MrnID" EmptyDataText="No records Found"
                  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" >
					<Columns>
						<asp:BoundField DataField="MrnID"  HeaderText="MRN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                       <asp:TemplateField HeaderText="MRN Code">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="lblmrnCode" Text='<%#"MRN-" + Eval("MrnCode").ToString()%>' ForeColor='<%#Eval("MrntypeId").ToString()=="7"? System.Drawing.Color.Maroon:System.Drawing.Color.Navy%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department" />
                        <asp:BoundField DataField="Location"  HeaderText="Warehouse" />
                        <asp:BoundField DataField="CreatedDate"  HeaderText="Created On" DataFormatString='<%$ appSettings:dateTimePattern %>'/>
                        <asp:BoundField DataField="RequiredFor"  HeaderText="Description" />
						<asp:BoundField DataField="CreatedByName"  HeaderText="Created By" />						
						<asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date" DataFormatString='<%$ appSettings:datePattern %>'/>						
                          <asp:TemplateField HeaderText="MRN Type">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="lblmrntypE" Text='<%#Eval("MrntypeId").ToString()=="7"? "Stock":"Non-Stock"%>' ForeColor='<%#Eval("MrntypeId").ToString()=="7"? System.Drawing.Color.Maroon:System.Drawing.Color.Navy%>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
						<asp:BoundField DataField="MrntypeId"  HeaderText="MrntypeId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						<asp:BoundField DataField="ItemCatrgoryId"  HeaderText="ItemCatrgoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
						 <asp:TemplateField HeaderText="Purchasing Type">
                                                            <ItemTemplate>
                                                                <asp:Label
                                                                    runat="server"
                                                                    Visible='<%# Eval("PurchaseType").ToString() == "1" ? true : false %>'
                                                                    Text="Local" CssClass="label label-warning"/>
                                                                <asp:Label
                                                                    runat="server"
                                                                    Visible='<%# Eval("PurchaseType").ToString() == "2" ? true : false %>'
                                                                    Text="Import" CssClass="label label-success"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="llSt" Text='<%#Eval("StatusName") == null ? "":Eval("StatusName").ToString()%>' CssClass="label label-info"></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView" Text="View" OnClick="btnView_Click" ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

					</Columns>
				</asp:GridView>
                </div>
            </div>         
          </div>
         
        </div>
         </ContentTemplate>
    </asp:UpdatePanel>
      </div>
    </section>
      

        
    </form>
       
 <script type="text/javascript">

      Sys.Application.add_load(function () {
            //onload set date value
          var this1 = $("#ContentSection_txtStartDate");
          if (this1.val() != undefined && this1.val() != "") {
                this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
          }
          this1 = $("#ContentSection_txtEndDate");
          if (this1.val() != undefined && this1.val() != "") {
              this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
          }
        });

      function dateChange(obj) {
          if (obj.value) {
              $(obj).attr('data-date', moment(obj.value, 'YYYY-MM-DD').format($(obj).attr('data-date-format')));
          } else {
              $(obj).attr('data-date', '');
          }
      }

      function DateValidate(){
          if ($("#ContentSection_txtStartDate").val() == "" || $("#ContentSection_txtEndDate").val() == "") {
              $("#ContentSection_RequiredFieldValidator2").css("display", "block");
              $("#ContentSection_RequiredFieldValidator2").css("visibility", "visible");
              return false
          } else {
              return true
          }
      }

      $(".customDate").on("change", function () {
          if (this.value) {
              $(this).attr('data-date', moment(this.value, 'YYYY-MM-DD').format($(this).attr('data-date-format')));
          } else {
              $(this).attr('data-date', '');
          }
      }).trigger("change")
    </script>
</asp:Content>
