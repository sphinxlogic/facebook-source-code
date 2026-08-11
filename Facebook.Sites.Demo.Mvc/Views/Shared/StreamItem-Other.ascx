<%@ Control Language="C#" Inherits="Facebook.Web.Mvc.FacebookViewUserControl<Facebook.Api.DemoSite.Models.Demo.StreamPartialPost>" %>
<li>
  <label>Type:</label><div><%= this.Model.Post.Type %></div>
  <label>Actor:</label><div><fb:name uid="<%= this.Model.Post.ActorId %>"  useyou="false" linked="true"><%= this.Model.Post.ActorId%></fb:name></div>
  <label>Like:</label><div><% if (!this.Model.Post.Likes.UserLikes)
                             { %>
                          <%= FbIFrame.ActionLink(String.Format("/demo/StreamAddLike?postId={0}", this.Model.Post.PostId), "Add Like")%>
                          <% }
                             else
                             { %>
                         <%= FbIFrame.ActionLink(String.Format("/demo/StreamRemoveLike?postId={0}", this.Model.Post.PostId), "Remove Like")%>
                             <%}%></div>
  <label>Created Time:</label><div><%= this.Model.Post.CreatedTime.ToString()%></div>
</li>
