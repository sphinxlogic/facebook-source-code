<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Facebook.Web.Mvc.FacebookViewPage<Facebook.Api.DemoSite.Models.Demo.FeedIndexData>" %>

<asp:Content ID="h" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="/Scripts/asyncForm.js"></script>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">
    
    <h1>Feed</h1>
    
    <div class="section">
        <h2>Feed Publishing</h2>
        <div class="content">
            <%= FbIFrame.FeedForm("DemoStory", "Publish") %>
        </div>
    </div>
    
    <div class="section">
        <h2>Feed Publishing with Custom Tokens</h2>
        <div class="content">
          <%= FbIFrame.FeedForm("DemoTokens", "Publish", new String[] {"token1","token2"}) %>
        </div>
    </div>

</asp:Content>
