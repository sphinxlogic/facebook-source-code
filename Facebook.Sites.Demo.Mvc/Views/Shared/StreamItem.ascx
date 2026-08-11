<%@ Control Language="C#" Inherits="Facebook.Web.Mvc.FacebookViewUserControl<Facebook.Api.DemoSite.Models.Demo.StreamPartialPost>" %>
<li>
  <div><%= this.Model.Post.CreatedTime.ToString()%> | 
  <% if (this.Model.Post.ActorId == this.FbContext.Session.Uid && this.Model.Post.AppId==this.FbContext.Application.GetPublicInfoByApiKey(this.FbSession.ApiKey).Value.AppId)
     { %>
   <a href="/demo/StreamRemove?postId=<%= this.Model.Post.PostId %>">Remove</a> 
   <% } %>
     <% if (!this.Model.Post.Likes.UserLikes)
                             { %>
                          <%= FbIFrame.ActionLink(String.Format("/demo/StreamAddLike?postId={0}", this.Model.Post.PostId), "Like")%>
                          <% }
                             else
                             { %>
                         <%= FbIFrame.ActionLink(String.Format("/demo/StreamRemoveLike?postId={0}", this.Model.Post.PostId), "Unlike")%>
                             <%}%></div>
   <% if (this.Model.Post.Type != null)
      { %>
             <form id="addcomment<%= this.Model.Post.PostId %>_form" action="/demo/stream/addComment" method="post">
                <table class="form">
                    <tbody>
                        <tr><td></td>
                            <td><input id="addcomment<%= this.Model.Post.PostId %>_comment" name="comment" type="text" value="I love http://facebook.codeplex.com" /></td>
                            <input id="addcomment<%= this.Model.Post.PostId %>_postId" name="postId" type="text" style="display:none;" value="<%= this.Model.Post.PostId %>" />
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td></td>
                            <td><button type="submit">Add Comment</button></td>
                        </tr>
                    </tfoot>
                </table>
            </form>
     <%} %>
       <% if (this.Model.Post.Comments != null &&  this.Model.Post.Comments.Count > 0) { %>
       <label>Comments:</label><div><ul><% foreach (Facebook.Api.Comment comment in this.Model.Post.Comments.Posts)
                                      { %>
                                        <li><fb:name uid="<%= comment.Fromid %>" Linked="true"><%= comment.Fromid %></fb:name> - <%= comment.Text %> - <span style="font-size:6px"><%= comment.Time.ToString() %></span></li>
                                   <% } %>
                                </ul></div>
                                 <% } %>
     <script type="text/javascript">
        $("#addcomment<%= this.Model.Post.PostId %>_form").asyncForm('script');
    </script>

</li>
