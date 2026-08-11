<%@ Control Language="C#" Inherits="Facebook.Web.Mvc.FacebookViewUserControl<Facebook.FacebookResponse<Int64>>" %>

<% if(this.Model.IsError) { %>
var content = '<div class=\"error\">Sorry, there was an error posting your note: <%= this.Model.ResponseException.Message %></div>';
<% } else { %>
var content = '<div>Your note has been posted.</div>';
<% } %>

FB.UI.DomResources.addResourceDict(new FB.UI.DomResDict('<div id=\"RES_ID_create_note_result\" style=\"padding:10px;\">' + content + '</div>'));
var popup = new FB.UI.PopupDialog('Create Note',FB.UI.DomResources.getResourceById('create_note_result'),false,false);
popup.show();