<%@ Control Language="C#" Inherits="Facebook.Web.Mvc.FacebookViewUserControl<Facebook.Api.DemoSite.Models.Demo.StreamPartialPost>" %>
<li>
  <label>Type:</label><div>Status</div>
  <div><fb:name uid="<%= this.Model.Post.ActorId %>"  useyou="false" linked="true"><%= this.Model.Post.ActorId%></fb:name>
  <%= this.Model.Post.Message%></div>
</li>
