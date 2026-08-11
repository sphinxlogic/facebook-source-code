<%@ Control Language="C#" Inherits="Facebook.Web.Mvc.FacebookViewUserControl<Facebook.Api.DemoSite.Models.Demo.StreamPartialPost>" %>
<li>
  <label>Type:</label><div>Link</div>
  <div><fb:name uid="<%= this.Model.Post.ActorId %>"  useyou="false" linked="true"><%= this.Model.Post.ActorId%></fb:name>
  <%= this.Model.Post.Message %></div>
  <div><a href="<%= this.Model.Post.Attachment.Href %>"><% if (this.Model.Post.Attachment.Media.Count> 0)
                                                                               { %><img src="<%= this.Model.Post.Attachment.Media.First().Src %>" /><% } %>
                                                                           <%= this.Model.Post.Attachment.Name %></a><br /><div style="font-size:10px;"><%= this.Model.Post.Attachment.Description %></div></div>

</li>
