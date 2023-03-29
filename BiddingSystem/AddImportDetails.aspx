<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="AddImportDetails.aspx.cs" Inherits="BiddingSystem.AddImportDetails" %>

<%@ Register Src="~/UserControls/Tab1.ascx" TagPrefix="uc1" TagName="Tab1" %>
<%@ Register Src="~/UserControls/Tab2.ascx" TagPrefix="uc1" TagName="Tab2" %>
<%@ Register src="~/UserControls/Tab3.ascx" tagprefix="uc1" tagname="Tab3"  %>
<%@ Register Src="~/UserControls/Tab4.ascx" TagPrefix="uc1" TagName="Tab4" %>
<%@ Register Src="~/UserControls/Tab5.ascx" TagPrefix="uc1" TagName="Tab5" %>
<%@ Register Src="~/UserControls/Tab6.ascx" TagPrefix="uc1" TagName="Tab6" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    
    <style>
        .tabs {
            position: relative;
            top: 1px;
            z-index: 2;
        }

        .tab {
            border: 1px solid black;
            background-repeat: repeat-x;
            color: black;
            padding: 5px 40px 5px 40px !important;
            font-size: 15px;
            background-color: #ecf0f5;
        }

        .selectedtab {
            background: none;
            background-repeat: repeat-x;
            color: black;
        }

        .tabcontents {
            border: 1px solid #ecf0f5;
            padding: 10px;
            width: auto;
            min-height:900px;
            overflow: hidden;
            background-color: white;
        }

        .selected {
            color: white !important;
            background-color: #3c8dbc !important;
        }
    </style>

    <section class="content-header">
    <h1>Add/Update Import Details</h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Add Import Details</li>
        </ol>
    </section>

    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
                <section class="content">
    <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
        <strong>
            <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
        </strong>
        </div>

    <div class="box box-info" >
    <div class="box-header with-border">
        <h3 class="box-title" ></h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
        </div>
    </div>
  
    <div class="box-body">
        <div class="co-md-12">
            <div class="form-horizontal">
            <div class="form-group">
                <div class="col-sm-3">
                    <label id="lblImportRequisitionNo">Import Requisition No</label>
                </div>
                <div class="col-sm-3">
                    <asp:TextBox runat="server" ID="txtRequisitionNo" class="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-1">
                    <label id="lblPrId">PR CODE</label>
                </div>
                <div class="col-sm-3">
                    <asp:TextBox runat="server" ID="txtPrCode" readonly="true" class="form-control"></asp:TextBox>
                </div>
            </div>
            
            <div class="form-group">
                <div class="col-sm-3">
                    <label id="lblOrderIndentNo">Order Indent No</label>
                </div>
                <div class="col-sm-3">
                    <asp:TextBox runat="server" ID="txtOrderIndentNo" readonly="true" class="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-1">
                    <label id="lblPoId">Po Code</label>
                </div>
                <div class="col-sm-3">
                    <asp:TextBox runat="server" ID="txtPOCode" readonly="true" class="form-control"></asp:TextBox>
                </div>
            </div>  
                  
            <div class="form-group">
                <div class="col-sm-3">
                    <label id="lblOrderNo">Order No</label>
                </div>
                <div class="col-sm-3">
                    <asp:DropDownList ID="ddlOrderNumber" ClientIDMode="static" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderNumber_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-sm-2">
                    <asp:Button runat="server" ID="btnAddNewOrderImportRefe" class="btn btn-primary" OnClick="btnAddNewOrderImportRefe_Click" Text="Add New Order"></asp:Button>
                </div>     
            </div>    
    </div>

           <asp:Menu ID="Menu1" Orientation="Horizontal"  StaticMenuItemStyle-CssClass="tab"
         StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server"  onmenuitemclick="Menu1_MenuItemClick">
            <Items>        
            <asp:MenuItem  Text="L/C Detail" Value="0" Selected="true"></asp:MenuItem>
            <asp:MenuItem Text="Payment Mode" Value="1"></asp:MenuItem> 
                 <asp:MenuItem Text="Other Details" Value="2"></asp:MenuItem>  
                 <asp:MenuItem Text="Charges" Value="3"></asp:MenuItem>  
                 <asp:MenuItem Text="Custom Charges" Value="4"></asp:MenuItem>
                <asp:MenuItem Text="Shipping Agent & Clearing" Value="5"></asp:MenuItem>                   
            </Items>
        </asp:Menu>

        <div class="tabcontents">
        <asp:MultiView ID="MultiView1" runat="server">
            
            <asp:View ID="View1" runat="server">
              <uc1:Tab1 runat="server" id="Tab1" />
            </asp:View>

            <asp:View ID="View2" runat="server">                
            <uc1:Tab2 runat="server" id="Tab2" />
            </asp:View>       

            <asp:View ID="View3" runat="server">
                <uc1:Tab3 ID="Tab3" runat="server" />
            </asp:View>

             <asp:View ID="View4" runat="server">
                    <uc1:Tab4 runat="server" id="Tab4" />
            </asp:View>

              <asp:View ID="View5" runat="server">
                    <uc1:Tab5 runat="server" id="Tab5" />
            </asp:View>
             <asp:View ID="View6" runat="server">
                   <uc1:Tab6 runat="server" id="Tab6" />
            </asp:View>
        </asp:MultiView>

        </div>
    </div>
    </div>

   
        

    </div>

    </section>
            </ContentTemplate>

        </asp:UpdatePanel>

    </form>
      <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>
     <script src="AdminResources/js/moment.min.js"></script>
    <script src="UserControls/Scripts/Tab2.js"></script>
    <script src="UserControls/Scripts/Tab3.js"></script>
</asp:Content>
