<%@ Control Language="C#" Inherits="Facebook.Web.Mvc.FacebookViewUserControl<Facebook.Api.DemoSite.Models.Demo.StreamPartialPost>" %>
<li>
  <label>Type:</label><div>Video</div>
  <div><fb:name uid="<%= this.Model.Post.ActorId %>"  useyou="false" linked="true"><%= this.Model.Post.ActorId%></fb:name>
  <%= this.Model.Post.Message %></div>
  <div><% foreach (Facebook.Api.StreamMedia media in this.Model.Post.Attachment.Media)
                               { %>
                               <img alt="<%= media.Alt %>" src="<%= media.Src %>" /><br />
  <%} %>
  </div>
  <div><a href="<%= this.Model.Post.Attachment.Href %>"><%= this.Model.Post.Attachment.Name %></a><br /><div style="font-size:8px;"><%= this.Model.Post.Attachment.Description %></div></div>
</li>
