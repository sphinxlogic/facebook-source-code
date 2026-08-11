<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="h" runat="server" ContentPlaceHolderID="head">
    <script src="/Scripts/asyncForm.js" type="text/javascript"></script>
    <script src="http://static.ak.facebook.com/js/api_lib/v0.4/FeatureLoader.js.php" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">


    <h1>Live Message</h1>

    <div class="section">
        <a name="Send"></a>
        <h2>Send</h2>
        <div class="content">
            <form id="send_form" action="/demo/liveMessage/send" method="post">
                <table class="form">
                    <tbody>
                        <tr>
                            <td><label for="send_uid">Recipient User ID</label></td>
                            <td><input id="send_uid" name="uid" type="text" value="<%= this.ViewData["Uid"] %>" /></td>
                        </tr>
                        <tr>
                            <td><label for="send_eventName">Event:</label></td>
                            <td><input id="send_eventName" name="eventName" type="text" value="event" /></td>
                        </tr>
                        <tr>
                            <td><label for="send_message">Message</label></td>
                            <td><textarea id="send_message" name="message">This message was sent with the .Net Facebook API! Get it at http://facebook.codeplex.com!</textarea></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td></td>
                            <td><button type="submit">Send Message</button></td>
                        </tr>
                    </tfoot>
                </table>
            </form>
        </div>
    </div>
    
    <script type="text/javascript">
        $('#send_form').asyncForm('script');
        var livemessage = new LiveMessage('event', function(data) { alert(data.name + data.msg); });
    </script>
    
</asp:Content>
