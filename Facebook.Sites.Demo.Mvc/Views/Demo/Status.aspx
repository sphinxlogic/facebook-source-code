<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Facebook.Web.Mvc.FacebookViewPage<Facebook.Api.DemoSite.Models.Demo.StatusIndexData>" %>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">

    <h1>Status</h1>

    <div class="section">
        <a name="Set"></a>
        <h2>Set</h2>
        <div class="content">
            <form method="post" name="status" action="/dotnetapi/demo/setstatus">
                <input type="text" name="status" id="status" />
                <input type="submit" />
            </form>
        </div>
    </div>
    
    <div class="section">
        <a name="Get"></a>
        <h2>Get</h2>
        <div class="content"><%= this.Model.StatusMessageFromGet %></div>
    </div>
    
    <div class="section">
        <a name="GetByUid"></a>
        <h2>GetByUid</h2>
        <div class="content"><%= this.Model.StatusMessageFromGetByUid %></div>
    </div>


</asp:Content>
