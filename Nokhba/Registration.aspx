<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Nokhba.Registration" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Registration.css" />


</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="registration-container">
        <h2>Registration</h2>

        <label>Full Name</label>
        <asp:TextBox ID="nameTextbox" runat="server" CssClass="input-field" Placeholder="John Doe"></asp:TextBox>

        <label>Email</label>
        <asp:TextBox id="emailTextbox" runat="server" CssClass="input-field" Placeholder="johndoe@gmail.com"></asp:TextBox>

        <label>Password</label>
        <asp:TextBox id="passwordTextbox" runat="server" CssClass="input-field" TextMode="Password" Placeholder="********"></asp:TextBox>

        <label>Confirm Password</label>
        <asp:TextBox id="confirmPasswordTextbox" runat="server" CssClass="input-field" TextMode="Password" Placeholder="********"></asp:TextBox>

        <label>Role</label>
        <asp:DropDownList id="UserRoleDropDownList" runat="server" CssClass="role">
            <asp:ListItem Text="Select Role" Value="" Selected="True" Disabled="True"></asp:ListItem>
            <asp:ListItem Text="Job Seeker" Value="JobSeeker"></asp:ListItem>
            <asp:ListItem Text="Employer" Value="Employer"></asp:ListItem>
        </asp:DropDownList>

        <asp:Button id="RegisterButton" runat="server" CssClass="register-btn" Text="Register" OnClick="RegisterButton_Click" />
        
        <asp:Label id="lblMessage" runat="server" CssClass="message" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>

