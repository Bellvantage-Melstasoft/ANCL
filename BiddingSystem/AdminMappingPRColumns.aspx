<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSuperAdmin.Master" AutoEventWireup="true" CodeBehind="AdminMappingPRColumns.aspx.cs" Inherits="BiddingSystem.AdminMappingPRColumns" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
<section class="content-header">
      <h1>
       PR Type Map with Columns
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">PR Type Map with Columns</li>
      </ol>
    </section>
    <br />
    <style type="text/css">
        .tablegv {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }
        .tablegv td, .tablegv th {
            border: 1px solid #ddd;
            padding: 8px;
        }
        .tablegv tr:nth-child(even){background-color: #f2f2f2;}
        /*.tablegv tr:hover {background-color: #ddd;}*/
        .tablegv th {
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: left;
            background-color: #3C8DBC;
            color: white;
        }
        .successMessage
                {
                    color: #1B6B0D;
                    font-size: medium;
                }
        
        .failMessage
        {
            color: #C81A34;
            font-size: medium;
        }
</style>
     <form runat="server" id="form1">

     </form>
</asp:Content>
