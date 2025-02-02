<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Nokhba.Registration" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Registration.css" />


</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-page">
        <div class="inner-div">
            <h1 class="registration-header">Registration</h1>
            <label>Fullname</label>
            <input runat="server" id="fullname" placeholder="John Doe" />
            <label>Email</label>
            <input runat="server" id="email" placeholder="johndoe@gmail.com" />
            <label>Password</label>
            <input runat="server" id="password" placeholder="********" />
            <label>Confirm Password</label>
            <input runat="server" id="confirmPassword" placeholder="********" />
            <label>Role</label>
            <asp:DropDownList ID="UserRoleDropDownList" runat="server"></asp:DropDownList>
            <asp:Button runat="server" Text="Register" />
        </div>
    </div>
</asp:Content>
