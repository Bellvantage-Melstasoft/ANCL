<%@ Page Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="AddImportRates.aspx.cs" Inherits="BiddingSystem.AddImportRates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style>
        #btnSection{
            padding-right:10px;
            padding-top : 20px;
        }
    </style>


    <section class="content-header">
      <h1>
       Add Import Rates
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Add Import Rates</li>
      </ol>
    </section>
    <br />

    <section class="content">

        <%--<div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <strong>
                                <asp:Label ID="lbMessage" ForeColor="White"  runat="server"></asp:Label>
                            </strong>
                        </div>--%>


      <!-- SUB HEADER -->
      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >IMPORT RATES</h3>
        </div>

        <!-- /.box-header -->
       <form id="form1" runat="server">
            <div class="box-body">
          
              <div class="row">
                <div class="col-md-6">
              
                    <div class="form-group">
                    <label for="exampleInputEmail1">Download Excel Template</label>                              
                     <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="btn btn-info" OnClick="btnDownload_Click" ValidationGroup="btnSave" style="padding-left:5px"></asp:Button>
                    </div>


                    <div class="d-flex flex-row bd-highlight mt-3">
                            <b>Please Select Excel File: </b>
                            <asp:FileUpload ID="fileuploadExcel" runat="server"/>                       
                            <%--<asp:Button ID="btnImport" runat="server" Text="Import Data" CssClass="btn btn-info pt-3" OnClick="btnImport_Click" />--%>
                    </div>
         
              </div>
                   
            </div>
            <div class="row">
                </br>
            </div>

         <div class="alert  alert-info  alert-dismissable mt-3" id="msg" runat="server" visible="false">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
            <strong>
                <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
            </strong>
        </div>
            
            <div class="row">
                <span class="pull-right" id="btnSection">
                     <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click"  ValidationGroup="btnSave" ></asp:Button>
                     <asp:Button ID="btnClear"  runat="server" Text="Clear"  
                    CssClass="btn btn-danger" onclick="btnClear_Click"></asp:Button>
                    </span>
                  </div>
     
        </form>
       </div>
      


    </section>
</asp:Content>
