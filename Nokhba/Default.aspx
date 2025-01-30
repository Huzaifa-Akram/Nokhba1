<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Nokhba.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="StyleSheet1.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-title">
        <h1>Want To Find The Best Jobs?</h1>
        <p style="font-size:1.2rem;">You're at the right place</p>
    </div>
    <div class="search-bar-outer-container">
        <div class="search-bar-inner-container">
            <input type="text" class="job-name-search" placeholder="Enter Job Title" />
            <div class="pipe">|</div>
            <asp:DropDownList CssClass="experience-search" ID="experienceDropDown" runat="server" EnableViewState="true"></asp:DropDownList>
            <div class="pipe">|</div>
            <input type="text" class="job-location-search" placeholder="Enter a City" />
            <asp:Button runat="server" Text="Search" CssClass="job-search-btn" />
        </div>
    </div>

</asp:Content>
