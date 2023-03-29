﻿<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="PrintMRNReturn.aspx.cs" Inherits="BiddingSystem.PrintMRNReturn" %>

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

            .tablegv tr:hover {
                background-color: #ddd;
            }

            .tablegv th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #3C8DBC;
                color: white;
            }
                  
        #divPrintPo {
            visibility: visible !important;
        }
       
    </style>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <section class="content-header">
    <h1>
      View Returned Inventory
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Inventory Returned to Warehouse</li>
      </ol>
    </section>
    <br />

    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>


                <section class="content" style="padding-top: 0px" id="divPrintPo" >
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info"  runat="server" >
        <div class="box-header with-border">
          <h3 class="box-title" >View Returned Inventory</h3>

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
                  ID="gvDeliveredInventory" GridLines="None" CssClass="table table-responsive tablegv" AutoGenerateColumns="false" DataKeyNames="MrndInID" EmptyDataText="No records Found">
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

                         <asp:BoundField DataField="RejectedUser"  HeaderText="Rejected By"/>
                        <asp:BoundField DataField="RejectedOn" HeaderText="Rejected On" DataFormatString='<%$ appSettings:dateTimePattern %>'/>
                        <asp:BoundField DataField="WarehouseID"  HeaderText="Wareouse Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        
                        
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
    
    </section>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">

    </script>
</asp:Content>

