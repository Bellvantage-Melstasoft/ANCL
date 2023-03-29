<%@ Page Title="" Language="C#" MasterPageFile="~/SupplierLoadingWebUIInner.Master" AutoEventWireup="true" CodeBehind="SupplierRequestCompanyInner.aspx.cs" Inherits="BiddingSystem.SupplierRequestCompanyInner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
        .form-control {
            width:97%
        }
        
    </style>

    <style type="text/css">
      .customers {
          font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
          border-collapse: collapse;
          width: 100%;
      }
      
      .customers td, .customers th {
          border: 1px solid #ddd;
          padding: 8px;
      }
      
      .customers tr:nth-child(even){background-color: #f2f2f2;}
      
      .customers tr:hover {background-color: #ddd;}
      
      .customers th {
          padding-top: 12px;
          padding-bottom: 12px;
          text-align: center;
          background-color: #467394;
          color: white;
      }
      .customers td {
          padding-top: 12px;
          padding-bottom: 12px;
          text-align: center;
      }
      input[type=number]::-webkit-inner-spin-button, 
               input[type=number]::-webkit-outer-spin-button { 
               -webkit-appearance: none; 
                margin: 0; 
               }
    </style>

 <div class="services-breadcrumb" style="background-color: #005383">
		<div class="agile_inner_breadcrumb">
			<div class="container">
				<ul class="w3_short">
					<li>
						<a href="SupplierInitialFrontViewInner.aspx" style="color:White">Home</a>
						<i style="color:White">|</i>
                        <a href="#" style="color:Yellow">Request Companies</a>
					</li>
				</ul>
			</div>
		</div>
	</div>


    <form runat="server" id="form1">

        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
       <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
       <strong><asp:Label ID="lbMessage" runat="server"></asp:Label>
       </strong>
    </div>
     <div id="modalViewTermsAndConditions" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 id="lblViewTermsAndConditions" class="modal-title">Terms & Conditions</h4>
            </div>
            <div class="modal-body">
                <p id="lblBodyCondition"></p>
            </div>
            <div class="modal-footer">
                <button id="btnOk" onclick="return hideTermModal();" type="button" class="btn btn-danger" >OK</button>
            </div>
        </div>
    </div>
</div>

 <div class="faqs-w3l" style="background-color:White">
         <div class="container">
			<!-- //tittle heading -->
            <div class="row">
            <div class="col-md-12">	
 <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
        <asp:GridView ID="gvRequestCompany" CssClass="table customers" runat="server" AutoGenerateColumns="false"  >
                <Columns>
                  <asp:TemplateField ItemStyle-Width="20px">
                    <ItemTemplate>
                         <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("isTermsAgreed").ToString() == "1" ? true : false%>' Enabled='<%# Eval("isApproved").ToString() == "1" ? false : true%>'  onclick="checkingBox();"/>
                   </ItemTemplate>
                 </asp:TemplateField>
               <asp:BoundField HeaderStyle-ForeColor="White" DataField="DepartmentID"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                <asp:BoundField HeaderStyle-ForeColor="White" ItemStyle-ForeColor="Black" DataField="DepartmentName" HeaderText="Department Name" ItemStyle-Width="150px" />
                      <asp:BoundField DataField="TermConditionpath" HeaderText="Terms & Conditions" ItemStyle-Width="150px" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                    <asp:TemplateField HeaderText="Terms & Conditions" HeaderStyle-ForeColor="White">
                        <ItemTemplate>
                            <asp:Button ID="btnTermsConditions" CssClass="btn btn-info"  runat="server" Text="view" OnClientClick="return GetSelectedRow(this)"></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>


                     <asp:TemplateField  HeaderText="Follow Status" HeaderStyle-ForeColor="White">
                    <ItemTemplate>
                     <asp:Label ID="lblFollow" style="color:Black" CssClass="control-label"  runat="server" Text='<%# Eval("isSupplierFollow").ToString() == "1" ? "Follow" : "unfollow"%>' ></asp:Label>
                   </ItemTemplate>
                 </asp:TemplateField>

                       <asp:TemplateField  HeaderText="Approval Status" HeaderStyle-ForeColor="White">
                    <ItemTemplate>
                     <asp:Label ID="lblApprovalStatus" style="color:Black" CssClass="control-label"  runat="server" Text='<%# Eval("isApproved").ToString() == "1" ? "Approved" : "Pending"%>' ></asp:Label>
                   </ItemTemplate>
                 </asp:TemplateField>

                     <asp:TemplateField >
                    <ItemTemplate>
                            <asp:Button ID="btnFollow" CssClass="btn btn-danger"  runat="server" Text="unfollow" Enabled='<%# Eval("isApproved").ToString() == "1" ? false : true%>' Visible='<%# Eval("isSupplierFollow").ToString() == "1" ? true : false%>' OnClick="btnFollow_Click"></asp:Button>
                         </ItemTemplate>
                 </asp:TemplateField>

              </Columns>
       
            </asp:GridView>

           <input type="checkbox" name="checkbox" value="check"  style="display:inline;"  />&nbsp;<label  style="display:inline" id="lblterms"> I agree with all checked company's Terms and Conditions</label>
                 <br />
                 <br />
           <asp:Button ID="btnSaveRequests" CssClass="btn btn-primary" runat="server" Text="Save Request"  OnClick="btnSaveRequests_Click" />
    </div>
    </div>
    </div>
    </div>
    </div>
    </div>
    
    </div>
 </form>

   <script src="AdminResources/js/jquery.min.js"></script>
   <link href="LoginResources/css/bootstrap-multiselect.css" rel="stylesheet" />
   <script src="LoginResources/js/bootstrap-multiselect.js"></script>
   <%-- <link href="LoginResources/css/mullt.min.css" rel="stylesheet" />--%>



      <script type="text/javascript">

       
       
        function hideTermModal()
        {
            var $confirm = $("#modalViewTermsAndConditions");
            $confirm.modal('hide');
            return this.false;
        }
      
    </script>

    <script>

        function GetSelectedRow(lnk) {
            //Reference the GridView Row.
            var row = lnk.parentNode.parentNode;

            //Determine the Row Index.
            var message = "Row Index: " + (row.rowIndex - 1);

            var termAndCondition = row.cells[3].innerHTML;
            if (termAndCondition != "") {
                var termsFileUrl = termAndCondition.substring(2);
                window.open(termsFileUrl, '_blank');
            }
            return false;
        }

        function GetSelectedTerms() {
            var gridView = document.getElementById('<%= gvRequestCompany.ClientID %>');
            var cell = gridView.rows[1].cells[2];
            var departmentId = cell.childNodes[0].text;

            var BidOpeningId = gridView.rows[1].cells[9].childNodes[0];
            var x = SupplierId;
            var ItemId = $('#ContentSection_lblItemids').text();
            var PrID = $('#ContentSection_lblPr').text();
            var customAmount; ;
            for (var i = 0; i < gridView.rows.length - 1; i++) {
                var CustomizeAmount = $("input[id*=txtCuztomizeAmount]")
                if (CustomizeAmount[i].value != '') {
                    customAmount = (CustomizeAmount[i].value);
                }
            }

            var supplierQuotation = {};
            supplierQuotation.SupplierId = parseInt(x.textContent);
            supplierQuotation.ItemId = ItemId;
            supplierQuotation.PrID = PrID;
            supplierQuotation.BidOpeningId = BidOpeningId.data;
            supplierQuotation.CustomizeAmount = customAmount;
            $.ajax({
                type: "POST",
                url: "CompanyComparisionSheet.aspx/GetSupplierId",
                data: '{supplierQuotation: ' + JSON.stringify(supplierQuotation) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $('#SuccessAlert').modal('show');
                    // window.location.href = "CompanyComparisionSheet.aspx?PrId=" + PrID;
                    event.preventDefault();
                    return false;
                }
            });
            event.preventDefault();
            return false;
        }
    </script>
  
    <script>

        $("#ContentPlaceHolder1_btnSaveRequests").click(function () {
            if (!this.form.checkbox.checked) {
                $('#lblterms').css("color", "Red");
                return false;

            }
            else {
                $('#lblterms').css("color", "#333333");
                return true;
            }


        })
    </script>
  
       <script>
 <%--  $( document ).ready(function() {
      
           var isValid = false;
           var count = 0;
           var gridView = document.getElementById('<%= gvRequestCompany.ClientID %>');
           for (var i = 1; i < gridView.rows.length; i++) {
               var inputs = gridView.rows[i].getElementsByTagName('input');
               if (inputs != null && inputs[0] != null) {
                   if (inputs[0].type == "checkbox") {
                       if (inputs[0].checked) {
                           count++;

                       }
                   }
               }
           }

           if (count > 0) {
               $("#<%=btnSaveRequests.ClientID%>").prop('disabled', false);
               return false;
           }

           else {
               $("#<%=btnSaveRequests.ClientID%>").prop('disabled', true);
               return false;
           }
});--%>
    </script>
    <script>
         <%-- function checkingBox() {
           var isValid = false;
           var count = 0;
           var gridView = document.getElementById('<%= gvRequestCompany.ClientID %>');
           for (var i = 1; i < gridView.rows.length; i++) {
               var inputs = gridView.rows[i].getElementsByTagName('input');
               if (inputs != null && inputs[0] != null) {
                   if (inputs[0].type == "checkbox" ) {
                       if (inputs[0].checked) {
                           count++;

                       }
                   }
               }
           }

           if (count > 0) {
               $("#<%=btnSaveRequests.ClientID%>").prop('disabled', false);
           }

           else {
               $("#<%=btnSaveRequests.ClientID%>").prop('disabled', true);
           }
       }--%>
    </script>

</asp:Content>
