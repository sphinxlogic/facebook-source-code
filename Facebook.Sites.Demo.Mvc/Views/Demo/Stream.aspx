<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Facebook.Web.Mvc.FacebookViewPage<Facebook.Api.DemoSite.Models.Demo.StreamIndexData>" %>

<asp:Content ID="h" runat="server" ContentPlaceHolderID="head">
    <script src="/Scripts/asyncForm.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">

    <h1>Stream</h1>

    <div class="section">
        <a name="Get"></a>
        <h2>Get</h2>
        <div class="content">  
            <ul class="kvp-list">
        <% foreach (Facebook.Api.StreamPost post in this.Model.Stream.Posts)
           { %>  
          <% if (post.Type != null)
             {
                 %><div style="border: 1px solid;">
                 <%
              switch (int.Parse(post.Type))
              {
                  case (int)Facebook.Api.StreamType.Status: Html.RenderPartial("StreamItem-Status", new Facebook.Api.DemoSite.Models.Demo.StreamPartialPost { Post = post }); break;
                  case (int)Facebook.Api.StreamType.Link: Html.RenderPartial("StreamItem-Link", new Facebook.Api.DemoSite.Models.Demo.StreamPartialPost { Post = post }); break;
                  case (int)Facebook.Api.StreamType.Video: Html.RenderPartial("StreamItem-Video", new Facebook.Api.DemoSite.Models.Demo.StreamPartialPost { Post = post }); break;
                  case (int)Facebook.Api.StreamType.FanPage: Html.RenderPartial("Streamitem-FanPage", new Facebook.Api.DemoSite.Models.Demo.StreamPartialPost { Post = post }); break;
                  case (int)Facebook.Api.StreamType.Feed: Html.RenderPartial("StreamItem-Feed", new Facebook.Api.DemoSite.Models.Demo.StreamPartialPost { Post = post }); break;
                  case (int)Facebook.Api.StreamType.Photo: Html.RenderPartial("StreamItem-Photo", new Facebook.Api.DemoSite.Models.Demo.StreamPartialPost { Post = post }); break;
                  case (int)Facebook.Api.StreamType.Wall: Html.RenderPartial("StreamItem-Wall", new Facebook.Api.DemoSite.Models.Demo.StreamPartialPost { Post = post }); break;
                  default: Html.RenderPartial("StreamItem-Other", new Facebook.Api.DemoSite.Models.Demo.StreamPartialPost { Post = post }); break;
              }

              Html.RenderPartial("StreamItem", new Facebook.Api.DemoSite.Models.Demo.StreamPartialPost { Post = post });%>
             </div>    <% }%>  
          <% } %>
          </ul>
        </div>
    </div>
    
    <div class="section">
      <a name="Publish"></a>
      <h2>Publish</h2>
      <div class="content">
                  <form id="publish_form" action="/demo/stream/publish" method="post">
                <table class="form">
                    <tbody>
                        <tr>
                            <td><label for="publish_message">Message:</label></td>
                            <td><input id="publish_message" name="message" type="text" value="I love http://facebook.codeplex.com" /></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td></td>
                            <td><button type="submit">Publish to Stream</button></td>
                        </tr>
                    </tfoot>
                </table>
            </form>
      </div>
    </div>

    <script type="text/javascript">
        $('#publish_form').asyncForm('script');
    </script>
    
</asp:Content>
