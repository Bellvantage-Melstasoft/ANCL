<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSupplier.Master" AutoEventWireup="true" CodeBehind="SupplierBiddingSummery.aspx.cs" Inherits="BiddingSystem.SupplierBiddingSummery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="span9">
    <ul class="breadcrumb">
		<li><a href="index.html">Home</a> <span class="divider">/</span></li>
		<li class="active"> Supplier Bidding Summary</li>
    </ul>
	<h3>  BIDDING SUMMERY [ <small>3 Item(s) </small>]<a href="#" class="btn btn-large pull-right"><i class="icon-arrow-left"></i> Continue Bidding </a></h3>	
	<hr class="soft"/>
	<table class="table table-bordered">
              <thead>
                <tr>
                  <th>Product</th>
                  <th>Description</th>
                  <th>Quantity/Update</th>
				  <th>Price</th>
                  <th>Discount</th>
                  <th>Tax</th>
                  <th>Total</th>
				</tr>
              </thead>
              <tbody>
                <tr>
                  <td> <img width="60" src="AppResources/themes/images/products/AsusLaptop.png" alt=""/></td>
                  <td>ASUS C201PA<br/>Color : black, Material : metal</td>
				  <td>
					<div class="input-append"><input class="span1" style="max-width:34px" placeholder="1" id="appendedInputButtons" size="16" type="text"><button class="btn" type="button" style=" padding-bottom: 9px; "><i class="icon-minus"></i></button><button class="btn" type="button" style=" padding-bottom: 9px; "><i class="icon-plus"></i></button><button class="btn btn-danger" type="button" style=" padding-bottom: 9px; "><i class="icon-remove icon-white"></i></button></div>
				  </td>
                  <td>120,000.00</td>
                  <td>10,000.00</td>
                  <td>5000.00</td>
                  <td>115,000.00</td>
                </tr>
				<tr>
                  <td> <img width="60" src="AppResources/themes/images/products/8.jpg" alt=""/></td>
                  <td>MEMORY CHIP<br/>Color : blue, Material : plastic</td>
				  <td> 
					<div class="input-append"><input class="span1" style="max-width:34px" placeholder="1"  size="16" type="text"><button class="btn" type="button" style=" padding-bottom: 9px; "><i class="icon-minus"></i></button><button class="btn" type="button" style=" padding-bottom: 9px; "><i class="icon-plus"></i></button><button class="btn btn-danger" type="button" style=" padding-bottom: 9px; "><i class="icon-remove icon-white"></i></button>				</div>
				  </td>
                  <td>1000.00</td>
                  <td>--</td>
                  <td>100.00</td>
                  <td>1100.00</td>
                </tr>
				<tr>
                  <td> <img width="60" src="AppResources/themes/images/products/Wire.jpg" alt=""/></td>
                  <td>WIRE (EARTH and CURRENT<br/>Color : green,red, Material : steal</td>
				  <td>
					<div class="input-append"><input class="span1" style="max-width:34px" placeholder="1"  size="16" type="text"><button class="btn" type="button" style=" padding-bottom: 9px; "><i class="icon-minus"></i></button><button class="btn" type="button" style=" padding-bottom: 9px; "><i class="icon-plus"></i></button><button class="btn btn-danger" type="button" style=" padding-bottom: 9px; "><i class="icon-remove icon-white"></i></button>				</div>
				  </td>
                  <td>25,000.00</td>
                  <td>2500.00</td>
                  <td>1000.00</td>
                  <td>23,500.00</td>
                </tr>
				
                <tr>
                  <td colspan="6" style="text-align:right">Total Price:	</td>
                  <td> 146,000.00</td>
                </tr>
				 <tr>
                  <td colspan="6" style="text-align:right">Total Discount:	</td>
                  <td> 12,500.00</td>
                </tr>
                 <tr>
                  <td colspan="6" style="text-align:right">Total Tax:	</td>
                  <td> 6100.00</td>
                </tr>
				 <tr>
                  <td colspan="6" style="text-align:right"><strong>TOTAL (146,000.00 Rs - 12,500.00 Rs + 6100.00) =</strong></td>
                  <td class="label label-important" style="display:block"> <strong> 139,600.00 </strong></td>
                </tr>
				</tbody>
            </table>
	<a href="SupplierIndex.aspx" class="btn btn-large"><i class="icon-arrow-left"></i> Continue Bidding </a>
	<a href="#" class="btn btn-large pull-right">Next <i class="icon-arrow-right"></i></a>
	
</div>
</asp:Content>
