<%@ Control Language="C#" Inherits="Facebook.Web.Mvc.FacebookViewUserControl<Facebook.IFacebookResponse>" %>

<% if(this.Model.IsError) { %>
var content = '<div class=\"error\"><%= this.ViewData["ErrorMessage"] %>: <%= this.Model.ResponseException.Message %></div>';
<% } else { %>
var content = '<div><%= this.ViewData["SuccessMessage"] %></div>';
<% } %>

FB.UI.DomResources.addResourceDict(new FB.UI.DomResDict('<div id=\"RES_ID_<%= this.ViewData["FbDomResourceId"] %>\" style=\"padding:10px;\">' + content + '</div>'));
var popup = new FB.UI.PopupDialog('<%= this.ViewData["Title"] %>',FB.UI.DomResources.getResourceById('<%= this.ViewData["FbDomResourceId"] %>'),false,false);
popup.show();