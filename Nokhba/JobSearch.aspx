<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="JobSearch.aspx.cs" Inherits="Nokhba.JobSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="search-bar-outer-container" style="margin-block: 3rem;">
        <div class="search-bar-inner-container">
            <asp:TextBox runat="server" ID="JobNameSearchInput" CssClass="job-name-search" placeholder="Enter Job Title" ></asp:TextBox>
            <div class="pipe">|</div>
            <asp:DropDownList CssClass="experience-search" ID="experienceDropDown" runat="server" EnableViewState="true"></asp:DropDownList>
            <div class="pipe">|</div>
            <asp:TextBox runat="server" ID="JobLocationInput" CssClass="job-name-search" placeholder="Enter a City" ></asp:TextBox>
            <asp:Button runat="server" Text="Search" CssClass="job-search-btn" OnClick="OnSearchBtnClicks" />
        </div>
    </div>


    <asp:DataList ID="JobsList" runat="server">
        <ItemTemplate>
            <div>
                <h1><%# Eval("title") %></h1>
                <p><%# Eval("employer") %></p>
            </div>
        </ItemTemplate>
    </asp:DataList>




</asp:Content>
