<%@ Control Language="C#" Inherits="Facebook.Web.Mvc.FacebookViewUserControl<Facebook.Api.DemoSite.Models.Demo.StreamPartialPost>" %>
<li>
  <label>Type:</label><div>Wall Post</div>
  <div><fb:name uid="<%= this.Model.Post.ActorId %>"  useyou="false" linked="true"><%= this.Model.Post.ActorId%></fb:name>  >> 
  <fb:name uid="<%= this.Model.Post.TargetId %>" useyou="false" linked="true"><%= this.Model.Post.TargetId%></fb:name>  <%= this.Model.Post.Message%></div>
</li>
