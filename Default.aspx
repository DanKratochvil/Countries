<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Countries._Default" %>
<%@ Register Src="~/Countries.ascx" TagPrefix="uc1" TagName="Countries" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:Countries runat="server" ID="Countries"/>   
</asp:Content>
