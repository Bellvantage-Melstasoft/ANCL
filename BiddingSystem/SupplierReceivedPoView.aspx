<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSupplier.Master" AutoEventWireup="true" CodeBehind="SupplierReceivedPoView.aspx.cs" Inherits="BiddingSystem.SupplierReceivedPoView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="span9">
    <ul class="breadcrumb">
		<li><a href="index.html">Home</a> <span class="divider">/</span></li>
		<li class="active">Products Compairsition</li>
    </ul>
	<h3> Products Compairsition <small class="pull-right"> 2 products are compaired </small></h3>	
	<hr class="soft"/>

	<table id="compairTbl" class="table table-bordered">
              <thead>
                <tr>
                  <th>Comapny Name</th>
                  <th>PO Number </th>
                  <th>View</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>Melstacorp</td>
                  <td style="text-align:center">
					<p class="justify">
		                PO0001
					</p>
				</td>
                  <td>
				  <p class="justify">
					<input type="button" value="view"/>
				</p>
				<img src="AppResources/themes/images/products/3.jpg" alt=""/>
				
				<form class="form-horizontal qtyFrm">
				<h3> $190.00</h3>
				<br/>
				 <a href="#" class="btn btn-large btn-primary"> Add to <i class=" icon-shopping-cart"></i></a>
				 <a href="#" class="btn btn-large"><i class="icon-zoom-in"></i></a>
				</form>
				  </td>
                </tr>
                <tr>
                  <td>Height</td>
                  <td>2"</td>
                  <td>2"</td>
                </tr>
                <tr>
                  <td>Deepth</td>
                  <td>5"</td>
                  <td>5"</td>
                </tr>
				<tr>
                  <td>Dimension</td>
                  <td>--</td>
                  <td>--</td>
                </tr>
				<tr>
                  <td>Width</td>
                  <td>6.5"</td>
                  <td>6.5"</td>
                </tr>
				<tr>
                  <td>Weight</td>
                  <td>0.5kg</td>
                  <td>0.5kg</td>
                </tr>
              </tbody>
            </table>		
	<a href="products.html" class="btn btn-large pull-right">Back Products Page</a>
	
	
</div>
</asp:Content>
