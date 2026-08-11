<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Facebook.Web.Mvc.FacebookViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Set Your Facebook Status
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Set Your Facebook Status</h2>

    <p><%= this.ViewData["PreviousResult"] %></p>

    <% using(var form = Html.BeginForm("Set", "SetStatus", FormMethod.Get)) { %>
    <%= Html.TextBox("status", "is checking out the new .NET Facebook API Client at http://facebook.codeplex.com.") %>
    <button type="submit">Set</button>
    <% } %>

</asp:Content>
