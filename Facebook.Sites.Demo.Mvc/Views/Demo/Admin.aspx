<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Facebook.Api.DemoSite.Models.Demo.AdminIndexData>" %>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">

    <h1>Admin</h1>
    
    <div id="admin-getallocation" class="section">
        <a name="GetAllocation"></a>
        <h2>GetAllocation</h2>
        <div class="content">
            <ul class="kvp-list">
                <li>
                    <label>Notifications Per Day</label>
                    <div><%= this.Model.NotificationsPerDay %></div>
                </li>
                <li>
                    <label>Announcement Notifications Per Week</label>
                    <div><%= this.Model.AnnoucementNotificationsPerWeek %></div>
                </li>
                <li>
                    <label>Requests Per Day</label>
                    <div><%= this.Model.RequestsPerDay %></div>
                </li>
                <li>
                    <label>E-mails Per Day</label>
                    <div><%= this.Model.EmailsPerDay %></div>
                </li>
                <li>
                    <label>E-mail Disable Message Location</label>
                    <div><%= this.Model.EmailDisableMessageLocation %></div>
                </li>
            </ul>
        </div>
    </div>
    
    <div id="admin-getappproperties" class="section">
        <a name="GetAppProperties"></a>
        <h2>GetAppProperties</h2>
        <div class="content">
            <ul class="kvp-list">
                <%-- foreach(var property in this.Model.AppProperties.Keys) { %>
                <li>
                    <label><%= property %></label>
                    <div><%= this.Model.AppProperties[property] %></div>
                </li>
                <% } --%>
            </ul>
        </div>
    </div>
    
    <div id="admin-getmetrics" class="section">
        <a name="GetMetrics"></a>
        <h2>GetMetrics</h2>
        <div class="content">
            <% foreach(var metric in this.Model.Metrics) { %>
            <h3>Metrics Ending <%= metric.EndTime %></h3>
            <div class="content">
                <h4>Facebook Metrics</h4>
                <ul class="kvp-list">                    
                    <li>
                        <label>Active Users</label>
                        <div><%= metric.ActiveUsers %></div>
                    </li>
                    <li>
                        <label>API Calls</label>
                        <div><%= metric.ApiCalls %></div>
                    </li>
                    <li>
                        <label>Unique API Calls</label>
                        <div><%= metric.UniqueApiCalls %></div>
                    </li>
                    <li>
                        <label>Canvas Page Views</label>
                        <div><%= metric.CanvasPageViews %></div>
                    </li>
                    <li>
                        <label>Unique Canvas Page Views</label>
                        <div><%= metric.UniqueCanvasPageViews %></div>
                    </li>
                    <li>
                        <label>Average Canvas HTTP Request Time</label>
                        <div><%= metric.CanvasHttpRequestTimeAvg %></div>
                    </li>
                    <li>
                        <label>Average Canvas FBML Render Time</label>
                        <div><%= metric.CanvasFbmlRenderTimeAvg %></div>
                    </li>
                    <li>
                        <label>Unique Blocks</label>
                        <div><%= metric.UniqueBlocks %></div>
                    </li>
                    <li>
                        <label>Unique Unblocks</label>
                        <div><%= metric.UniqueUnblocks %></div>
                    </li>
                </ul>
                <h4>Canvas View HTTP Code Metrics</h4>
                <ul class="kvp-list">
                    <li>
                        <label>HTTP 0</label>
                        <div><%= metric.CanvasPageViewsHttpCode_0 %></div>
                    </li>
                    <li>
                        <label>HTTP 100</label>
                        <div><%= metric.CanvasPageViewsHttpCode_100 %></div>
                    </li>
                    <li>
                        <label>HTTP 200</label>
                        <div><%= metric.CanvasPageViewsHttpCode_200 %></div>
                    </li>
                    <li>
                        <label>HTTP 301</label>
                        <div><%= metric.CanvasPageViewsHttpCode_301 %></div>
                    </li>
                    <li>
                        <label>HTTP 302</label>
                        <div><%= metric.CanvasPageViewsHttpCode_302 %></div>
                    </li>
                    <li>
                        <label>HTTP 303</label>
                        <div><%= metric.CanvasPageViewsHttpCode_303 %></div>
                    </li>
                    <li>
                        <label>HTTP 400</label>
                        <div><%= metric.CanvasPageViewsHttpCode_400 %></div>
                    </li>
                    <li>
                        <label>HTTP 401</label>
                        <div><%= metric.CanvasPageViewsHttpCode_401 %></div>
                    </li>
                    <li>
                        <label>HTTP 403</label>
                        <div><%= metric.CanvasPageViewsHttpCode_403 %></div>
                    </li>
                    <li>
                        <label>HTTP 404</label>
                        <div><%= metric.CanvasPageViewsHttpCode_404 %></div>
                    </li>
                    <li>
                        <label>HTTP 405</label>
                        <div><%= metric.CanvasPageViewsHttpCode_405 %></div>
                    </li>
                    <li>
                        <label>HTTP 413</label>
                        <div><%= metric.CanvasPageViewsHttpCode_413 %></div>
                    </li>
                    <li>
                        <label>HTTP 422</label>
                        <div><%= metric.CanvasPageViewsHttpCode_422 %></div>
                    </li>
                    <li>
                        <label>HTTP 500</label>
                        <div><%= metric.CanvasPageViewsHttpCode_500 %></div>
                    </li>
                    <li>
                        <label>HTTP 502</label>
                        <div><%= metric.CanvasPageViewsHttpCode_502 %></div>
                    </li>
                    <li>
                        <label>HTTP 503</label>
                        <div><%= metric.CanvasPageViewsHttpCode_503 %></div>
                    </li>
                    <li>
                        <label>HTTP 505</label>
                        <div><%= metric.CanvasPageViewsHttpCode_505 %></div>
                    </li>
                </ul>
            </div>
            <% } %>
        </div>
        
        <div class="section">
            <a name="GetRestrictionInfo"></a>
            <h2>GetRestrictionInfo</h2>
            <div class="content">
                <%= this.Model.RestrictionInfo %>
            </div>
        </div>
    </div>
</asp:Content>
