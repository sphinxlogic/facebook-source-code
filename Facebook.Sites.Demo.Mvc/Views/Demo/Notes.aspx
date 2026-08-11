<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="h" runat="server" ContentPlaceHolderID="head">
    <script src="/Scripts/asyncForm.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">
    
    <h1>Notes</h1>

    <div class="section">
        <a name="Create"></a>
        <h2>Create</h2>
        <div class="content">
            <form id="create_form" action="/demo/notes/create" method="post">
                <table class="form">
                    <tbody>
                        <tr>
                            <td><label for="create_title">Title:</label></td>
                            <td><input id="create_title" name="title" type="text" value=".NET Client for the Facebook API" /></td>
                        </tr>
                        <tr>
                            <td><label for="create_content">Content:</label></td>
                            <td><textarea id="create_content" name="content">I'm using the new .NET Client for the Facebook API, and it is AWESOME! &lt;a href=&quot;http://facebook.codeplex.com&quot; target=&quot;_blank&quot;&gt;Check it out on CodePlex!&lt;/a&gt;</textarea></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td></td>
                            <td><button type="submit">Create Note</button></td>
                        </tr>
                    </tfoot>
                </table>
            </form>
        </div>
    </div>
    
    <script type="text/javascript">
        $('#create_form').asyncForm('script');
    </script>

</asp:Content>
