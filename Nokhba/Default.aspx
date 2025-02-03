<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Nokhba.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="StyleSheet1.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-title">
        <h1>Want To Find The Best Jobs?</h1>
        <p>You're at the right place</p>
    </div>
    <div class="search-bar-outer-container">
        <div class="search-bar-inner-container">
            <asp:TextBox runat="server" ID="JobNameSearchInput" CssClass="job-name-search" placeholder="Enter Job Title" ></asp:TextBox>
            <div class="pipe">|</div>
            <asp:DropDownList CssClass="experience-search" ID="experienceDropDown" runat="server" EnableViewState="true"></asp:DropDownList>
            <div class="pipe">|</div>
            <asp:TextBox runat="server" ID="JobLocationInput" CssClass="job-name-search" placeholder="Enter a City" ></asp:TextBox>
            <asp:Button runat="server" Text="Search" CssClass="job-search-btn" OnClick="OnSearchBtnClick" />
        </div>
    </div>

    <div class="job-types-outer-container">
        <div class="job-types-inner-container">
            <div class="job-type">
                Web Development >
            </div>
            <div class="job-type">
                Mobile App Development >
            </div>
            <div class="job-type">
                Web3 Development >
            </div>
            <div class="job-type">
                UI/UX >
            </div>
        </div>

        <div class="job-types-inner-container">
            <div class="job-type">
                Data Science >
            </div>
            <div class="job-type">
                AI/ML >
            </div>
            <div class="job-type">
                DevOps >
            </div>
        </div>
    </div>

</asp:Content>
