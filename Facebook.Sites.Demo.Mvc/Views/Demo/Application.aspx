<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Facebook.Web.Mvc.FacebookViewPage<Facebook.Api.AppInfo>" %>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">

    <h1>Application</h1>
    
    <div class="section">
        <a name="GetPublicInfo"></a>
        <h2>GetPublicInfo</h2>
        <div class="content">
            <ul class="kvp-list">
                <li>
                    <label>ApiKey</label>
                    <div><%= this.Model.ApiKey %></div>
                </li>
                
                <li>
                    <label>AppId</label>
                    <div><%= this.Model.AppId %></div>
                </li>
                
                <li>
                    <label>Canvas Name</label>
                    <div><%= this.Model.CanvasName %></div>
                </li>
                
                <li>
                    <label>Company Name</label>
                    <div><%= this.Model.CompanyName %></div>
                </li>
                
                <li>
                    <label>Daily Active Users</label>
                    <div><%= this.Model.DailyActiveUsers %></div>
                </li>
                
                <li>
                    <label>Monthly Active Users</label>
                    <div><%= this.Model.MonthlyActiveUsers %></div>
                </li>
                
                <li>
                    <label>Weekly Active Users</label>
                    <div><%= this.Model.WeeklyActiveUsers %></div>
                </li>
                
                <li>
                    <label>Description</label>
                    <div><%= this.Model.Description %></div>
                </li>
                
                <li>
                    <label>Developers</label>
                    <div>
                        <% foreach(var dev in this.Model.Developers) { %>
                        <div><%= dev.Name %></div>                    
                        <% } %>
                    </div>
                </li>
                
                <li>
                    <label>Display Name</label>
                    <div><%= this.Model.DisplayName %></div>
                </li>

                <li>
                    <label>Icon URL</label>
                    <div><img src="<%= this.Model.IconUrl %>" alt="Icon URL" /></div>
                </li>
                
                <li>
                    <label>Logo URL</label>
                    <div><img src="<%= this.Model.LogoUrl %>" alt="Logo URL" /></div>
                </li>
            </dl>
        </div>
    </div>
    
    

</asp:Content>
