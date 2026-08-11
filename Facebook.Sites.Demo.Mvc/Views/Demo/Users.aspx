<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Facebook.Web.Mvc.FacebookViewPage<Facebook.Api.DemoSite.Models.Demo.UsersIndexData>" %>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">

    <h1>Users</h1>

    <div class="section">
        <a name="GetInfo"></a>
        <h2>GetInfo</h2>
        <% foreach(var user in this.Model.GetInfo) { %>            
            <div class="content">            
                <label>First Name:</label><span><%= user.FirstName %></span><br />
                <label>Last Name:</label><span><%= user.LastName %></span><br />
            </div>
        <% } %>
    </div>
    
    <div class="section">
        <a name="GetLoggedInUser"></a>
        <h2>GetLoggedInUser</h2>
        <div class="content">
            <fb:profile-pic uid="<%= this.Model.GetLoggedInUser %>" />
            <fb:name uid="<%= this.Model.GetLoggedInUser %>" useyou="false" />
            <fb:user-status uid="<%= this.Model.GetLoggedInUser %>" />
        </div>
    </div>
    
    <div class="section">
        <a name="HasAppPermission"></a>
        <h2>HasAppPermission</h2>
        <div class="content">
            <label>Read Stream:</label><span><%=this.Model.HasEmailPermission%></span>
            <% if (this.Model.HasReadStreamPermission) { %> <a href="/demo/revokeextendedpermission?perm_name=read_stream">Revoke</a>
            <% } else { %> <%= FbIFrame.ActionLink("Allow", "/demo/users", "read_stream")%><% } %>
            <br />
            <label>Email:</label><span><%=this.Model.HasEmailPermission%></span>
            <% if (this.Model.HasEmailPermission) { %> <a href="/demo/revokeextendedpermission?perm_name=email">Revoke</a>
            <% } else { %> <%= FbIFrame.ActionLink("Allow", "/demo/users", "email")%><% } %>
            <br />
            
            <label>Offline Access:</label><span><%=this.Model.HasOfflineAccessPermission%></span>
            <% if (this.Model.HasOfflineAccessPermission){ %> <a href="/demo/revokeextendedpermission?perm_name=offline_access">Revoke</a>
            <% } else { %> <%= FbIFrame.ActionLink("Allow", "/demo/users", "offline_access")%><% } %>
            <br />
            
            <label>Status Update:</label><span><%=this.Model.HasStatusUpdatePermission%></span>
            <% if (this.Model.HasStatusUpdatePermission) { %> <a href="/demo/revokeextendedpermission?perm_name=status_update">Revoke</a>
            <% } else { %> <%= FbIFrame.ActionLink("Allow", "/demo/users", "status_update")%><% } %>
            <br />
            
            <label>Photo Upload:</label><span><%=this.Model.HasPhotoUploadPermission%></span>
            <% if (this.Model.HasPhotoUploadPermission){ %> <a href="/demo/revokeextendedpermission?perm_name=photo_upload">Revoke</a>
            <% } else { %> <%= FbIFrame.ActionLink("Allow", "/demo/users", "photo_upload")%><% } %>
            <br />
            
            <label>Create Listing:</label><span><%=this.Model.HasCreateListingPermission%></span>
            <% if (this.Model.HasCreateListingPermission){ %> <a href="/demo/revokeextendedpermission?perm_name=create_listing">Revoke</a>
            <% } else { %> <%= FbIFrame.ActionLink("Allow", "/demo/users", "create_listing")%><% } %>
            <br />
            
            <label>Create Event:</label><span><%=this.Model.HasCreateEventPermission%></span>
            <% if (this.Model.HasCreateEventPermission){ %> <a href="/demo/revokeextendedpermission?perm_name=create_event">Revoke</a>
            <% } else { %><%= FbIFrame.ActionLink("Allow", "/demo/users", "create_event")%><% } %>
            <br />
            
            <label>RSVP Event:</label><span><%=this.Model.HasRsvpEventPermission%></span>
            <% if (this.Model.HasRsvpEventPermission) { %> <a href="/demo/revokeextendedpermission?perm_name=rsvp_event">Revoke</a>
            <% } else { %><%= FbIFrame.ActionLink("Allow", "/demo/users", "rsvp_event")%><% } %>
            <br />
            
            <label>SMS:</label><span><%=this.Model.HasSmsPermission%></span>
            <% if (this.Model.HasSmsPermission){ %> <a href="/demo/revokeextendedpermission?perm_name=sms">Revoke</a>
            <% } else {%> <%= FbIFrame.ActionLink("Allow", "/demo/users", "sms")%> <% }%><br />
            
            <label>Share Item:</label><span><%=this.Model.HasShareItemPermission%></span>
            <% if (this.Model.HasShareItemPermission){ %> <a href="/demo/revokeextendedpermission?perm_name=share_item">Revoke</a>
            <% } else {%> <%= FbIFrame.ActionLink("Allow", "/demo/users", "share_itiem")%> <% }%><br />
        </div>
    </div>
    
    <div class="section">
        <a name="SetStatus"></a>
        <h2>SetStatus</h2>
        <div class="content">
        This method is deprecated. Please use <a href="/demo/status/#Set">Status.Set</a>
        </div>
    </div>
    
    <div class="section">
        <a name="IsVerified"></a>
        <h2>IsVerified</h2>
        <div class="content">
        </div>
    </div>

</asp:Content>
