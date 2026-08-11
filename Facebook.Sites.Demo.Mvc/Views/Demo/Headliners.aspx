<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Facebook.Web.Mvc.FacebookViewPage" %>
<%@ Import Namespace="Facebook.Web.Mvc.SocialCash" %>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">

    <h2>SocialCash Headliners Integration</h2>    
    <%= this.FbIFrame.RenderHeadlinersAd("NETFacebookApiClientApp", "Bottom") %>    

</asp:Content>
