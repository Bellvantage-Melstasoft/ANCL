<%@ Page Title="" Language="C#" MasterPageFile="~/SupplierLoadingWebUIInner.Master" AutoEventWireup="true" CodeBehind="SupplierContactUsInner.aspx.cs" Inherits="BiddingSystem.SupplierContactUsInner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="services-breadcrumb" style="background-color: #005383">
		<div class="agile_inner_breadcrumb">
			<div class="container">
				<ul class="w3_short">
					<li>
						<a href="SupplierInitialFrontView.aspx">Home</a>
						<i style="color:White">|</i>
					</li>
					<li>contact Us</li>
				</ul>
			</div>
		</div>
	</div>

    <div class="contact-w3l">
		<div class="container">
			<!-- tittle heading -->
			<h3 class="tittle-w3l">Contact Us
				<span class="heading-style">
					<i></i>
					<i></i>
					<i></i>
				</span>
			</h3>
			<!-- //tittle heading -->
			<!-- contact -->
			<div class="contact agileits">
				<div class="contact-agileinfo" >
					<div class="contact-form wthree" style="background-color: white;">
						<form action="#" method="post">
							<div class="">
								<input type="text" name="name" placeholder="Name" required="">
							</div>
							<div class="">
								<input class="text" type="text" name="subject" placeholder="Subject" required="">
							</div>
							<div class="">
								<input class="email" type="email" name="email" placeholder="Email" required="">
							</div>
							<div class="">
								<textarea placeholder="Message" name="message" required=""></textarea>
							</div>
							<input type="submit" value="Submit">
						</form>
					</div>
					<div class="contact-right wthree">
						<div class="col-xs-7 contact-text w3-agileits">
							<h4>GET IN TOUCH :</h4>
							<p style="color:Black">
								<i class="fa fa-map-marker"></i> No: 63, Norris Canal Road, Colombo 10 Sri Lanka</p>
							<p style="color:Black">
								<i class="fa fa-phone" ></i> Telephone : (94) 115 33 44 55</p>
							<p style="color:Black">
								<i class="fa fa-fax" ></i> FAX : </p>
							<p style="color:Black">
								<i class="fa fa-envelope-o" ></i> Email :
								<a href="#">support@bellvantage.com</a>
							</p>
						</div>
						<div class="col-xs-5 contact-agile">
							<img src="SupplierUIResources/images/contact2.jpg" alt="">
						</div>
						<div class="clearfix"> </div>
					</div>
				</div>
			</div>
			<!-- //contact -->
		</div>
	</div>
</asp:Content>
