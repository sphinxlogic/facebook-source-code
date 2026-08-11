<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Facebook.Web.Mvc.FacebookViewPage" %>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">
    
    <h2>Welcome, <fb:name uid="loggedinuser" useyou="false" firstnameonly="true" linked="false">Application Developer</fb:name>!</h2>
    <p>You have <fb:IfIsAppUser runat="server"><fb:Else runat="server"><strong><u>not</u></strong></fb:Else></fb:IfIsAppUser> authorized this application.
        <fb:IfIsAppUser runat="server"><fb:Else runat="server">            
            [<%= FbIFrame.ActionLink("<strong>Authorize</strong>", ".", true) %>]
        </fb:Else></fb:IfIsAppUser>
    </p>

</asp:Content>
