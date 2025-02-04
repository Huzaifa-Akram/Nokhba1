<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="JobSearch.aspx.cs" Inherits="Nokhba.JobSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="~/JobSearch.css" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="search-bar-outer-container" style="margin-block: 3rem;">
        <div class="search-bar-inner-container">
            <asp:TextBox runat="server" ID="JobNameSearchInput" CssClass="job-name-search" placeholder="Enter Job Title"></asp:TextBox>
            <div class="pipe">|</div>
            <asp:DropDownList CssClass="experience-search" ID="experienceDropDown" runat="server" EnableViewState="true"></asp:DropDownList>
            <div class="pipe">|</div>
            <asp:TextBox runat="server" ID="JobLocationInput" CssClass="job-name-search" placeholder="Enter a City"></asp:TextBox>
            <asp:Button runat="server" Text="Search" CssClass="job-search-btn" OnClick="OnSearchBtnClicks" />
        </div>
    </div>


    <asp:DataList ID="JobsList" runat="server" CssClass="jobs-list">
        <ItemTemplate>
            <a href="Registration.aspx" class="job-container">
                <h2 class="job-title"><%# Eval("title") %></h2>
                <p class="job-employer"><%# Eval("employer") %></p>
                <p class="job-salary">USD <%# Eval("salary") %> per month</p>
                <p class="job-description"><%# Eval("JobDescription") %></p>
                <p class="job-date"><%# Eval("DatePosted ") %></p>
            </a>
        </ItemTemplate>
    </asp:DataList>




</asp:Content>
