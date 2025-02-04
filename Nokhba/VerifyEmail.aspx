<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="VerifyEmail.aspx.cs" Inherits="Nokhba.VerifyEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="VerifyEmail.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="verification-container">
    <h2>Email Verification</h2>
    <p>We have sent a 6-digit verification code to <strong><asp:Label ID="lblUserEmail" runat="server"></asp:Label></strong></p>
    <p>Please enter the code below to verify your account.</p>
    <asp:TextBox ID="txtVerificationCode" runat="server" placeholder="Enter verification code" CssClass="input-box"></asp:TextBox>
    <asp:Button ID="btnVerify" runat="server" Text="Verify Email" CssClass="btn-primary" OnClick="btnVerify_Click" />
    <p class="resend-text">Didn't receive the code? <asp:LinkButton ID="btnResendCode" runat="server" CssClass="resend-link" OnClick="btnResendCode_Click">Resend Code</asp:LinkButton></p>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
</div>

</asp:Content>
