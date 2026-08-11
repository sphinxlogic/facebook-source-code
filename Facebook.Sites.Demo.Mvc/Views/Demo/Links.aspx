<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="h" runat="server" ContentPlaceHolderID="head">
    <script src="/Scripts/asyncForm.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">
    <h1>Links</h1>

    <div class="section">
        <a name="Post"></a>
        <h2>Post</h2>
        <div class="content">
            <form id="post_form" action="/demo/links/post" method="post">
                <table class="form">
                    <tbody>
                        <tr>
                            <td><label for="post_link">Link URL:</label></td>
                            <td><input id="post_link" name="link" type="text" value="http://facebook.codeplex.com" /></td>
                        </tr>
                        <tr>
                            <td><label for="post_comment">Comment:</label></td>
                            <td><textarea id="post_comment" name="comment">I'm using the new .NET Client for the Facebook API, and it is AWESOME!</textarea></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td></td>
                            <td><button type="submit">Post Link</button></td>
                        </tr>
                    </tfoot>
                </table>
            </form>
        </div>
    </div>
    
    <div class="section">
      <a name="Get"></a>
      <h2>Get</h2>
      <div class="content">
      
      </div>
    </div>
    <script type="text/javascript">
        $('#post_form').asyncForm('script');
    </script>
    
</asp:Content>
