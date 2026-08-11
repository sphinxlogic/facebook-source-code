<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="h" runat="server" ContentPlaceHolderID="head">
    <script src="/Scripts/asyncForm.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">

    <h1>Notification</h1>
    
    <div class="section">
        <a name-"Get"></a>
        <h2>Get</h2>
        <div class="content">
        Unread Messages: <%= this.ViewData["UnreadCount"] %>
        </div>
        
        <a name="Send"></a>
        <h2>Send</h2>
        <div class="content">
            <form id="send_form" action="/demo/notifications/send" method="post">
                <table class="form">
                    <tbody>
                        <tr>
                            <td><label for="send_ids">Recipients User ID<br />(comma separated)</label></td>
                            <td><input id="send_ids" name="ids" type="text" value="<%= this.ViewData["Uid"] %>" /></td>
                        </tr>
                        <tr>
                            <td><label for="send_type">Type:</label></td>
                            <td><select id="send_type" name="type">
                                    <option value="app_to_user">Application to User</option>
                                    <option value="user_to_user">User to User</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td><label for="send_notification">notification</label></td>
                            <td><textarea id="send_notification" name="notification">This notification was sent with the .Net Facebook API! Get it at http://facebook.codeplex.com!</textarea></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td></td>
                            <td><button type="submit">Send Notification</button></td>
                        </tr>
                    </tfoot>
                </table>
            </form>
        </div>
        
        <a name="SendEmail"></a>
        <h2>SendEmail</h2>
        <div class="content">
            <form id="sendEmail_form" action="/demo/notifications/sendEmail" method="post">
                <table class="form">
                    <tbody>
                        <tr>
                            <td><label for="sendEmail_ids">Recipients User ID<br />(comma separated)</label></td>
                            <td><input id="sendEmail_ids" name="ids" type="text" value="<%= this.ViewData["Uid"] %>" /></td>
                        </tr>
                        <tr>
                            <td><label for="sendEmail_subject">Subject:</label></td>
                            <td><input id="sendEmail_subject" name="subject" type="text" value="Check out the .Net Facebook API!" /></td>
                        </tr>
                        <tr>
                            <td><label for="sendEmail_notification">Notification:</label></td>
                            <td><textarea id="sendEmail_notification" name="notification">This notification was sent with the .Net Facebook API! Get it at http://facebook.codeplex.com!</textarea></td>
                        </tr>
                        <tr>
                            <td><label for="sendEmail_fbml">FBML:</label></td>
                            <td><textarea id="sendEmail_fbml" name="fbml" value=" "></textarea></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td></td>
                            <td><button type="submit">Send Notification Email</button></td>
                        </tr>
                    </tfoot>
                </table>
            </form>
        </div>
    </div>

    <script type="text/javascript">
        $('#send_form').asyncForm('script');
        $('#sendEmail_form').asyncForm('script');
    </script>
    
</asp:Content>
